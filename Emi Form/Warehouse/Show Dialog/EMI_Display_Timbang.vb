Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Display_Timbang
    Dim arrcari As New ArrayList
    Dim Jenis = "ETA"

    Public Property filter_tambahan As String
    Public Property asal As String



    Dim LvNoLoading As String
    Dim LvLokasi As String
    Dim LvNama As String
    Dim LvSJ As String
    Dim LvSupir As String
    Dim LvPlat As String
    Dim LvKdSup As String

    Dim ItemNoLoading As Integer = 0
    Dim ItemLokasi As Integer = 1
    Dim ItemNama As Integer = 2
    Dim ItemSJ As Integer = 3
    Dim ItemSupir As Integer = 4
    Dim ItemPlat As Integer = 5
    Dim ItemKdSup As Integer = 6

    Dim jenisMasuk As String = ""

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If

        Return hasil
    End Function

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvNoLoading = Lv_ListKendaraan.Items(NoIndex).SubItems(ItemNoLoading).Text
        LvLokasi = Lv_ListKendaraan.Items(NoIndex).SubItems(ItemLokasi).Text
        LvNama = Lv_ListKendaraan.Items(NoIndex).SubItems(ItemNama).Text
        LvSJ = Lv_ListKendaraan.Items(NoIndex).SubItems(ItemSJ).Text
        LvSupir = Lv_ListKendaraan.Items(NoIndex).SubItems(ItemSupir).Text
        LvPlat = Lv_ListKendaraan.Items(NoIndex).SubItems(ItemPlat).Text
        LvKdSup = Lv_ListKendaraan.Items(NoIndex).SubItems(ItemKdSup).Text

    End Sub

    Private Sub Popup_Timbang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Popup_Timbang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            'filter_tambahan = "timbang_keluar='Y'"

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            'Label1.Text = Base_Language.Lang_Global_ListKendaraan

            If filter_tambahan = "timbang_masuk='Y'" Then
                Label1.Text = "Display - Kendaraan Masuk"
                jenisMasuk = "MASUK"
            Else
                Label1.Text = "Display - Kendaraan Keluar"
                jenisMasuk = "KELUAR"
            End If

            Lv_ListKendaraan.Columns.Clear()

            Lv_ListKendaraan.Columns.Add("No Loading", 120, HorizontalAlignment.Left) '0
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Lokasi, 200, HorizontalAlignment.Left).DisplayIndex = 0 '1
            Lv_ListKendaraan.Columns.Add(Base_Language.lang_global_Nama_Supplier, 250, HorizontalAlignment.Left) '2
            Lv_ListKendaraan.Columns.Add("No. SJ", 200, HorizontalAlignment.Left) '3
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Supir, 150, HorizontalAlignment.Left) '4
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_PlatNomor, 150, HorizontalAlignment.Left) '5
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Kode_Supplier, 0, HorizontalAlignment.Left) '6

            Lv_ListKendaraan.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Public Sub kosong()
        Get_Timbangan_Unloading()
    End Sub

    Private Sub Get_Timbangan_Unloading()
        Try
            OpenConn()

            Lv_ListKendaraan.Items.Clear()
            Lv_ListKendaraan.View = View.Details

            SQL = ";with cte as( SELECT a.lokasi, a.Kode_Supplier, b.Nama, a.No_SJ, a.ETA, a.driver AS Supir, "
            SQL = SQL & "a.no_plat AS Plat_Number,a.flag_proses_loading, a.No_faktur AS no_loading, a.ID_Jenis_Muatan, 'JNE' as Nama_Ekspedisi, "

            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x where x.no_faktur=a.no_faktur and "
            SQL = SQL & "x.flag_timbang_masuk is null and a.Flag_Proses_loading is null ORDER BY x.no_faktur),'-') as Timbang_Masuk, "
            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x where x.no_faktur=a.no_faktur and "
            SQL = SQL & "x.flag_sudah_bongkar_android is null and x.flag_timbang_masuk='Y' and a.Flag_Proses_Loading is null ORDER BY x.no_faktur),'-') as Unloading, "
            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x where x.no_faktur=a.no_faktur and "
            SQL = SQL & "x.flag_sudah_bongkar_android ='Y' and a.Flag_timbang_keluar is null and a.Flag_Proses_Loading='Y' ORDER BY x.no_faktur),'-') as Timbang_Keluar, "

            SQL = SQL & "isnull((select top(1) No_Faktur from emi_timbang_unloading x where x.no_loading=a.no_faktur and "
            SQL = SQL & "x.flag_selesai is null ORDER BY x.no_loading),'-') as No_Timbangan "
            SQL = SQL & "FROM EMI_Pembelian_Loading a, Suppliers b WHERE "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.Status IS NULL "
            SQL = SQL & "and flag_security='Y' and Flag_Qc_Pertama='Y') "
            SQL = SQL & "select * from cte "
            SQL = SQL & "where " & filter_tambahan & " "

            SQL = SQL & "ORDER BY ETA DESC; "

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_ListKendaraan.Items.Add(dr("no_loading"))
                    Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("nama"))
                    Lvw.SubItems.Add(dr("No_SJ"))
                    Lvw.SubItems.Add(dr("Supir"))
                    Lvw.SubItems.Add(dr("Plat_Number"))
                    Lvw.SubItems.Add(dr("Kode_Supplier"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles Lv_ListKendaraan.DoubleClick


        Get_Isi_ListView(Lv_ListKendaraan.FocusedItem.Index)

        If asal = "Unloading_Barang" Then
            EMI_Timbang_Unloading.kosong()
            If jenisMasuk = "MASUK" Then
                EMI_Timbang_Unloading.TxtNo_Loading.Text = LvNoLoading
                EMI_Timbang_Unloading.jenisMasuk = "MASUK"

            ElseIf jenisMasuk = "KELUAR" Then
                EMI_Timbang_Unloading.TxtNo_Loading.Text = LvNoLoading
                EMI_Timbang_Unloading.jenisMasuk = "KELUAR"
            Else
                MessageBox.Show("jenis masuk salah . ! !")
                Exit Sub
            End If

            EMI_Timbang_Unloading.ShowDialog()
        ElseIf asal = "QC_BAHAN" Then
            'EMI_QC_Bahan.TxtNoLoading.Text = LvNoLoading
            'EMI_QC_Bahan.txtNoSJ.Text = LvNoSJ
            'EMI_QC_Bahan.txtNomorPlat.Text = LvPlatNomor
            'EMI_QC_Bahan.ShowDialog()

        ElseIf asal = "Barang_Masuk" Then
            'Emi_Barang_Masuk.kosong()


            'Emi_Barang_Masuk.txtKodeSupp.Text = LvKdSupplier
            'Emi_Barang_Masuk.TxtBarangMasuk_NmSupplier.Text = LvNmSupplier
            'Emi_Barang_Masuk.TxtBarangMasuk_NoPO.Text = ""

            'Dim gudang As String = ""
            'Try
            '    OpenConn()
            '    SQL = "select Kode_Stock_Owner_gudang from Binding_Lokasi_Gudang where kode_perusahaan = '" & KodePerusahaan & "' and Gudang_Default = 'Y' and Kode_Stock_Owner='" & LvLokasi & "' "
            '    Using dr = OpenTrans(SQL)
            '        If dr.Read Then
            '            gudang = dr("Kode_Stock_Owner_gudang")
            '        Else
            '            dr.Close()
            '            CloseConn()
            '            MessageBox.Show(Base_Language.lang_global_Error_LokasiTidakAda, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        End If
            '    End Using

            '    CloseConn()
            'Catch ex As Exception
            '    CloseConn()
            '    MessageBox.Show(ex.Message)
            '    Exit Sub
            'End Try

            'Emi_Barang_Masuk.txtBarangMasuk_LokasiGudang.Text = gudang
            'Emi_Barang_Masuk.CmbBarangMasuk_Lokasi.Text = LvLokasi
            'Emi_Barang_Masuk.TxtBarang_Masuk_NoNota.Text = LvNoSJ
            'Emi_Barang_Masuk.TxtBarangMasuk_NoPlat.Text = LvPlatNomor

            'Emi_Barang_Masuk.TxtBarang_Masuk_NoNota.Focus()
            'Emi_Barang_Masuk.LvBarangMasuk_DataPO.Items.Clear()

            'Emi_Barang_Masuk.TxtBarangMasuk_KdBarang.Clear()
            'Emi_Barang_Masuk.TxtBarangMasuk_NmBarang.Clear()
            'Emi_Barang_Masuk.TxtBarangMasuk_Jml.Clear()

            'Emi_Barang_Masuk.ShowDialog()
        Else
            MessageBox.Show(Base_Language.Lang_Global_FormAsal & " . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

    End Sub

End Class
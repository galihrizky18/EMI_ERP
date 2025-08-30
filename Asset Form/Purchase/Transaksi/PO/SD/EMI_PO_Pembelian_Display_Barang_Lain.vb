Public Class EMI_PO_Pembelian_Display_Barang_Lain
    Dim arrcariLocal, arrcariImport As New ArrayList
    Dim Jenis = "Lokasi_PO"
    Dim LvNo_PoLocal As String
    Dim LvLokasiLocal As String
    Dim LvTanggalLocal As String
    Dim LvKd_SupplierLocal As String
    Dim LvNm_SupplierLocal As String
    Dim LvLokasiGudangLocal As String
    Dim LvKeteranganLocal As String
    Dim LvIDLocal As String
    Dim LvKategoriPO As String

    Dim CellNo_PoLocal As Integer = 0
    Dim CellLokasiLocal As Integer = 1
    Dim CellTanggalLocal As Integer = 2
    Dim CellKd_SupplierLocal As Integer = 3
    Dim CellNm_SupplierLocal As Integer = 4
    Dim CellLokasiGudangLocal As Integer = 5
    Dim CellKeteranganLocal As Integer = 6
    Dim CellIDLocal As Integer = 7
    Dim CellKategoriPO As Integer = 8

    Dim LvNo_PoImport As String
    Dim LvLokasiImport As String
    Dim LvTanggalImport As String
    Dim LvKd_SupplierImport As String
    Dim LvNm_SupplierImport As String
    Dim LvLokasiGudangImport As String
    Dim LvKeteranganImport As String
    Dim LvIDImport As String

    Dim CellNo_PoImport As Integer = 0
    Dim CellLokasiImport As Integer = 1
    Dim CellTanggalImport As Integer = 2
    Dim CellKd_SupplierImport As Integer = 3
    Dim CellNm_SupplierImport As Integer = 4
    Dim CellLokasiGudangImport As Integer = 5
    Dim CellKeteranganImport As Integer = 6
    Dim CellIDImport As Integer = 7


    Dim ID_Prepare As String = "0"
    Dim ID_PO As String = "1"
    Dim ID_ETD As String = "2"
    Dim ID_ETA As String = "3"
    Dim ID_Timbang As String = "4"

    Public filter_tambahan As String
    Public Property asal As String

    Private Sub Get_Isi_ListviewLocal(ByVal No_Index As Integer)
        LvNo_PoLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellNo_PoLocal).Value
        LvLokasiLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellLokasiLocal).Value
        LvTanggalLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellTanggalLocal).Value
        LvKd_SupplierLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellKd_SupplierLocal).Value
        LvNm_SupplierLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellNm_SupplierLocal).Value
        LvLokasiGudangLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellLokasiGudangLocal).Value
        LvKeteranganLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellKeteranganLocal).Value
        LvIDLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellIDLocal).Value
        LvKategoriPO = DgvPO_DataLocal.Rows(No_Index).Cells(CellKategoriPO).Value
    End Sub

    Private Sub SD_Pilih_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try

            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Label1.Text = Base_Language.Lang_Lokasi_PO_Judul_Display
            Label4.Text = Base_Language.Lang_Lokasi_PO_Kolom

            DgvPO_DataLocal.Columns(CellNo_PoLocal).HeaderText = Base_Language.Lang_Global_NoFaktur
            DgvPO_DataLocal.Columns(CellLokasiLocal).HeaderText = Base_Language.Lang_Global_Lokasi
            DgvPO_DataLocal.Columns(CellTanggalLocal).HeaderText = Base_Language.Lang_Global_Tanggal
            DgvPO_DataLocal.Columns(CellKd_SupplierLocal).HeaderText = Base_Language.Lang_Global_Kode_Supplier
            DgvPO_DataLocal.Columns(CellNm_SupplierLocal).HeaderText = Base_Language.Lang_Global_Supplier
            DgvPO_DataLocal.Columns(CellLokasiGudangLocal).HeaderText = Base_Language.Lang_Global_LokasiGudang
            DgvPO_DataLocal.Columns(CellKeteranganLocal).HeaderText = Base_Language.lang_global_keterangan
            DgvPO_DataLocal.Columns(CellKategoriPO).HeaderText = "Kategori PO"
            DgvPO_DataLocal.Columns(CellKategoriPO).DisplayIndex = 3

            ComboBox1.Items.Clear() : arrcariLocal.Clear() : arrcariImport.Clear()
            ComboBox1.Items.Add(Base_Language.Lang_Global_NoFaktur) : arrcariLocal.Add("a.No_Faktur") : arrcariImport.Add("ro.id_rencana")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Lokasi) : arrcariLocal.Add("a.Lokasi") : arrcariImport.Add("ro.Lokasi")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Kode_Supplier) : arrcariLocal.Add("c.Kode_Supplier") : arrcariImport.Add("s.Kode_Supplier")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Supplier) : arrcariLocal.Add("c.Nama") : arrcariImport.Add("s.Nama")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cari("Y")
    End Sub

    Private Sub SD_Pilih_PO_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Public Sub Cari(ByVal semua As String)
        Try
            OpenConn()


            DgvPO_DataLocal.Rows.Clear()


            Dim ind As Integer = 0
            'SQL = "select a.No_Faktur,a.Lokasi, a.Tanggal,c.Kode_Supplier,c.Nama, NULL as ETA from "
            'SQL = SQL & "EMI_Prepare_Bahan_Baku a, EMI_Prepare_Bahan_Baku_Det_Order b, Suppliers c, emi_master_Penawaran d, Suppliers_Kategori e "
            'SQL = SQL & "where a.Kode_Perusahaan =b.Kode_Perusahaan and a.No_Faktur=b.No_Faktur and a.Status is null and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            'SQL = SQL & "and b.Kode_Perusahaan =d.Kode_Perusahaan and b.No_Penawaran=d.No_Penawaran and c.Kode_Perusahaan=d.Kode_Perusahaan "
            'SQL = SQL & "and c.Kode_Supplier=d.Kode_Supplier and Flag_Selesai is null and c.ID_Kategori_Suppliers= e.ID_Kategori_Suppliers and e.Flag_Jenis_Lokal='Y' "
            'If semua = "T" Then
            '    SQL = SQL & " and " & arrcariLocal.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
            'Else
            '    SQL = SQL & " "
            'End If
            'SQL = SQL & " group by a.No_Faktur,a.Lokasi, a.Tanggal,c.Kode_Supplier,c.Nama "
            'SQL = SQL & "order by no_faktur "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read

            '        DgvPO_DataLocal.Rows.Add(1)


            '        DgvPO_DataLocal.Rows(ind).Cells(CellNo_PoLocal).Value = dr("No_Faktur")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellLokasiLocal).Value = dr("Lokasi")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellTanggalLocal).Value = Format(dr("Tanggal"), "dd MMMM yyyy")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellKd_SupplierLocal).Value = dr("Kode_Supplier")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellNm_SupplierLocal).Value = dr("Nama")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellLokasiGudangLocal).Value = "" 'dr("Kode_Stock_Owner")

            '        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : On Process"
            '        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.Coral

            '        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_Prepare
            '        ind += 1
            '    Loop
            'End Using



            SQL = "select a.No_Faktur, a.Lokasi, a.Tanggal, c.Kode_Supplier, c.Nama, 1 as ID, ETD, Flag_ETD, Flag_Release "

            SQL = SQL & ",isnull((select top(1) 'T' from EMI_Pembelian_PO_Detail_Induk_Barang_Lain x "
            SQL = SQL & "where x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Faktur=a.no_Faktur and x.Flag_loading is null),'Y') as Selesai_ETA "

            SQL = SQL & ",isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail_Barang_Lain x, EMI_Pembelian_Loading_Barang_Lain y "
            SQL = SQL & "where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.No_Faktur and y.status is null "
            SQL = SQL & "and x.Kode_Perusahaan=a.Kode_Perusahaan and x.no_PO=a.no_Faktur),null) as Flag_ETA "

            SQL = SQL & ",isnull((select top(1) ETA from EMI_Pembelian_Loading_detail_Barang_Lain x, EMI_Pembelian_Loading_Barang_Lain y "
            SQL = SQL & "where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.No_Faktur and y.status is null "
            SQL = SQL & "and x.Kode_Perusahaan=a.Kode_Perusahaan and x.no_PO=a.no_Faktur),null) as ETA, "

            SQL = SQL & "a.Flag_Import "


            SQL = SQL & "from EMI_Pembelian_PO_Induk_Barang_Lain a, Suppliers c, Suppliers_Kategori d where Selesai is null and Status is null and "
            SQL = SQL & "a.Kode_Perusahaan=c.Kode_Perusahaan and a.Kode_Supplier=c.Kode_Supplier and a.Kode_Perusahaan='" & KodePerusahaan & "' and "
            'SQL = SQL & "c.ID_Kategori_Suppliers=d.ID_Kategori_Suppliers and (d.Flag_Jenis_Lokal='Y' or d.Flag_Jenis_import='Y' ) and a.flag_pembelian is null and Flag_Selesai_SubPO is null "
            SQL = SQL & "c.ID_Kategori_Suppliers=d.ID_Kategori_Suppliers and (d.Flag_Jenis_Lokal='Y' or d.Flag_Jenis_import='Y' ) and Flag_Selesai_SubPO is null "
            If semua = "T" Then
                SQL = SQL & " and " & arrcariLocal.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
            Else
                SQL = SQL & " "
            End If
            SQL = SQL & "order by no_faktur "
            Using dr = OpenTrans(SQL)
                Do While dr.Read

                    DgvPO_DataLocal.Rows.Add(1)


                    DgvPO_DataLocal.Rows(ind).Cells(CellNo_PoLocal).Value = dr("No_Faktur")
                    DgvPO_DataLocal.Rows(ind).Cells(CellLokasiLocal).Value = dr("Lokasi")
                    DgvPO_DataLocal.Rows(ind).Cells(CellTanggalLocal).Value = Format(dr("Tanggal"), "dd MMMM yyyy")
                    DgvPO_DataLocal.Rows(ind).Cells(CellKd_SupplierLocal).Value = dr("Kode_Supplier")
                    DgvPO_DataLocal.Rows(ind).Cells(CellNm_SupplierLocal).Value = dr("Nama")
                    DgvPO_DataLocal.Rows(ind).Cells(CellLokasiGudangLocal).Value = "" 'dr("Kode_Stock_Owner")

                    If General_Class.CekNULL(dr("Flag_Import")) = "Y" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKategoriPO).Value = "IMPORT"
                    Else
                        DgvPO_DataLocal.Rows(ind).Cells(CellKategoriPO).Value = "LOKAL"
                    End If

                    If General_Class.CekNULL(dr("Flag_Release")) = "" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : Submit  Tanggal : " & Format(dr("Tanggal"), "dd MMMM yyyy")
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_PO
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGoldenrodYellow
                    ElseIf General_Class.CekNULL(dr("Flag_ETD")) = "" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : Realease  Tanggal : " & Format(dr("Tanggal"), "dd MMMM yyyy")
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_PO
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGoldenrodYellow
                    ElseIf General_Class.CekNULL(dr("Flag_ETA")) = "" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : Persiapan Pengiriman  Tanggal : " & Format(dr("ETD"), "dd MMMM yyyy")
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_ETD
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGoldenrodYellow
                    ElseIf General_Class.CekNULL(dr("Flag_ETA")) = "Y" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : On The Way  Tanggal : " & Format(dr("ETA"), "dd MMMM yyyy")
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_ETA
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGoldenrodYellow
                        'ElseIf General_Class.CekNULL(dr("Flag_Timbangan_Unloading")) = "Y" Then
                        '    DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "Barang Masuk" & Environment.NewLine & "Status : Timbang " '  Tanggal : " & Format(dr("ETA"), "dd MMMM yyyy")
                        '    DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_Timbang
                        '    DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGreen
                    End If


                    ind += 1
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Public Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        ComboBox1.SelectedIndex = -1
        TextBox3.Text = ""

        Cari("Y")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnNewPO.Click
        'EMI_PO_Pembelian.kosong()
        'EMI_PO_Pembelian.TxtPO_KdSupplier.Enabled = True
        'EMI_PO_Pembelian.TxtPO_NmSupplier.Enabled = True
        'EMI_PO_Pembelian.TxtPO_NmSupplier.Focus()
        EMI_PO_Pembelian_Barang_Lain.Asal = ""
        EMI_PO_Pembelian_Barang_Lain.ShowDialog()

    End Sub

    Private Sub BtnSelisih_Click(sender As Object, e As EventArgs) Handles BtnSelisih.Click
        'EMI_Exclude_PO_Display.kosong()
        'EMI_Exclude_PO_Display.Edit = "Y"
        'EMI_Exclude_PO_Display.ShowDialog()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        EMI_PO_Pembelian_Display_View_Barang_Lain.ShowDialog()
    End Sub

    Private Sub DgvPO_DataLocal_DoubleClick(sender As Object, e As EventArgs) Handles DgvPO_DataLocal.DoubleClick
        Dim currentRow = DgvPO_DataLocal.CurrentRow.Index
        Get_Isi_ListviewLocal(currentRow)


        EMI_PO_Pembelian_Barang_Lain.kosong()
        EMI_PO_Pembelian_Barang_Lain.TxtPO_NoFaktur.Text = LvNo_PoLocal
        EMI_PO_Pembelian_Barang_Lain.CmbPO_Lokasi.Text = LvLokasiLocal
        EMI_PO_Pembelian_Barang_Lain.TxtPO_NoFaktur_Leave(Me, e)
        EMI_PO_Pembelian_Barang_Lain.Asal = "edit"
        EMI_PO_Pembelian_Barang_Lain.ShowDialog()




        'If LvIDLocal = ID_Prepare Then
        '    If asal = "PO_Bahan" Then
        '        EMI_PO_Pembelian.kosong()
        '        EMI_PO_Pembelian.TxtPO_NoPO.Text = LvNo_PoLocal
        '        EMI_PO_Pembelian.CmbPO_Lokasi.Text = LvLokasiLocal
        '        EMI_PO_Pembelian.TxtPO_NmSupplier.Text = LvNm_SupplierLocal
        '        EMI_PO_Pembelian.TxtPO_KdSupplier.Text = LvKd_SupplierLocal

        '        EMI_PO_Pembelian.TxtPO_KdSupplier.Enabled = False
        '        EMI_PO_Pembelian.TxtPO_NmSupplier.Enabled = False
        '        Dim gudang As String = ""
        '        Try
        '            OpenConn()
        '            SQL = "select Kode_Stock_Owner_gudang from Binding_Lokasi_Gudang where kode_perusahaan = '" & KodePerusahaan & "' and Gudang_Default = 'Y' and Kode_Stock_Owner='" & LvLokasiLocal & "' "
        '            Using dr = OpenTrans(SQL)
        '                If dr.Read Then
        '                    gudang = dr("Kode_Stock_Owner_gudang")
        '                End If
        '            End Using

        '            CloseConn()
        '        Catch ex As Exception
        '            CloseConn()
        '            MessageBox.Show(ex.Message)
        '            Exit Sub
        '        End Try

        '        EMI_PO_Pembelian.CmbPO_LokasiGudang.Text = gudang
        '        EMI_PO_Pembelian.Ambil_Data()

        '        EMI_PO_Pembelian.ShowDialog()
        '    Else
        '        MessageBox.Show(Base_Language.Lang_Global_FormAsal & " . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If
        '''ElseIf LvIDLocal = ID_PO Then
        '''    EMI_PO_Pembelian_SD_ETD.TxtNoPO.Text = LvNo_PoLocal
        '''    EMI_PO_Pembelian_SD_ETD.ShowDialog()
        '''ElseIf LvIDLocal = ID_ETD Then
        '''    EMI_PO_Pembelian_SD_ETA.Lokasi = LvLokasiLocal
        '''    EMI_PO_Pembelian_SD_ETA.Kd_sup = LvKd_SupplierLocal
        '''    EMI_PO_Pembelian_SD_ETA.ShowDialog()
       ' End If

    End Sub
End Class
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class EMI_PO_Pembelian_Display2
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

    Dim CellNo_PoLocal As Integer = 0
    Dim CellLokasiLocal As Integer = 1
    Dim CellTanggalLocal As Integer = 2
    Dim CellKd_SupplierLocal As Integer = 3
    Dim CellNm_SupplierLocal As Integer = 4
    Dim CellLokasiGudangLocal As Integer = 5
    Dim CellKeteranganLocal As Integer = 6
    Dim CellIDLocal As Integer = 7


    Dim ID_Prepare As String = "0"
    Dim ID_PO As String = "1"
    Dim ID_ETD As String = "2"
    Dim ID_ETA As String = "3"
    Dim ID_Timbang As String = "4"
    Dim ID_Selisih As String = "5"
    Dim ID_Pembelian As String = "6"
    Public Property filter_tambahan As String
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
            SQL = "select a.No_Faktur, a.Lokasi, a.Tanggal, c.Kode_Supplier, c.Nama, 1 as ID, ETD, Flag_ETD, Flag_BM, Flag_Val_Selisih_BM, "

            SQL = SQL & "Flag_Val_BM, Flag_Selisih_BM, A.FLAG_TIMBANG "

            SQL = SQL & "from EMI_Pembelian_PO a, Suppliers c, Suppliers_Kategori d where Selesai is null and Status is null and "
            SQL = SQL & "a.Kode_Perusahaan=c.Kode_Perusahaan and a.Kode_Supplier=c.Kode_Supplier and a.Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "c.ID_Kategori_Suppliers=d.ID_Kategori_Suppliers and d.Flag_Jenis_Lokal='Y' " & filter_tambahan
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

                    If General_Class.CekNULL(dr("Flag_Val_Selisih_BM")) = "Y" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "Pembelian" & Environment.NewLine & "Status : Penyelesaian Selisih "
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_Pembelian
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(dr("Flag_Val_BM")) = "Y" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "Selisih Barang Masuk" & Environment.NewLine & "Status : Penyelesaian Selisih "
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_Selisih
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGreen

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

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub DgvPO_DataLocal_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPO_DataLocal.CellContentClick

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        ComboBox1.SelectedIndex = -1
        TextBox3.Text = ""

        Cari("Y")
    End Sub

    Private Sub DgvPO_DataLocal_DoubleClick(sender As Object, e As EventArgs) Handles DgvPO_DataLocal.DoubleClick
        If DgvPO_DataLocal.Rows.Count = 0 Then
            Exit Sub
        End If
        Dim currentRow = DgvPO_DataLocal.CurrentRow.Index
        Get_Isi_ListviewLocal(currentRow)

        If LvIDLocal = ID_Selisih Then
            EMI_Selisih_Barang_Masuk.kosong()
            EMI_Selisih_Barang_Masuk.txtNoPO.Text = LvNo_PoLocal
            EMI_Selisih_Barang_Masuk.dtpTanggalPO.Value = LvTanggalLocal
            EMI_Selisih_Barang_Masuk.txtKodeSupplier.Text = LvKd_SupplierLocal
            EMI_Selisih_Barang_Masuk.txtNamaSupplier.Text = LvNm_SupplierLocal
            'EMI_Selisih_Barang_Masuk.get_data()

            EMI_Selisih_Barang_Masuk.ShowDialog()

        ElseIf LvIDLocal = ID_Pembelian Then
            EMI_Pembelian.Kosong()
            Try
                OpenConn()

                SQL = "select a.No_Faktur,a.No_Nota,a.Tanggal,a.Kode_Supplier as Supplier,b.Nama,a.Jenis_Pembayaran,"
                SQL = SQL & "a.Mata_Uang,a.Kurs,a.Cara_Bayar,isnull((select c.keterangan from cara_bayar c where "
                SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.Lokasi = c.Lokasi and a.Cara_Bayar = c.Kode_CB "
                SQL = SQL & "),'') as keterangan,a.PPN, "
                SQL = SQL & "a.Total_MUA,a.Total_IDR,a.Grand_Sebelum_PPN,a.Grand "
                SQL = SQL & "from EMI_Pembelian_PO a,Suppliers b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & LvNo_PoLocal & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        EMI_Pembelian.TxtPembelian_NoPO.Text = dr("No_Faktur")
                        'EMI_Pembelian.DtpPembelian_Tgl.Value = Format(dr("Tanggal"), "dd MMMM yyyy")
                        EMI_Pembelian.TxtPembelian_NoNota.Text = dr("No_Nota")
                        EMI_Pembelian.TxtPembelian_KdSupplier.Text = dr("Supplier")
                        EMI_Pembelian.TxtPembelian_NmSupplier.Text = dr("Nama")
                        If dr("Jenis_Pembayaran") = "T" Then
                            EMI_Pembelian.CmbPembelian_JnsBayar.SelectedIndex = 0
                        Else
                            EMI_Pembelian.CmbPembelian_JnsBayar.SelectedIndex = 1
                        End If
                        EMI_Pembelian.CmbPembelian_MataUang.Text = dr("Mata_Uang")
                        EMI_Pembelian.TxtPembelian_Kurs.Text = Format(dr("Kurs"), "N2")
                        EMI_Pembelian.CmbPembelian_CaraBayar.Text = dr("Keterangan")
                        If dr("PPN") > 0 Then
                            EMI_Pembelian.TxtPembelian_PersenPPN.Text = Format(dr("PPN"), "N2")
                            EMI_Pembelian.ChkPembelian_PPN.Checked = True
                        Else
                            EMI_Pembelian.TxtPembelian_PersenPPN.Text = 0
                            EMI_Pembelian.ChkPembelian_PPN.Checked = False
                        End If

                        Dim nilai_ppn As Double = 0
                        nilai_ppn = dr("Grand_Sebelum_PPN") * dr("PPN") / 100
                        EMI_Pembelian.TxtPembelian_TotalMUA.Text = Format(dr("Total_MUA"), "N2")
                        EMI_Pembelian.TxtPembelian_TotalIDR.Text = Format(dr("Total_IDR"), "N2")
                        EMI_Pembelian.TxtPembelian_TotalSblmPPN.Text = Format(dr("Grand_Sebelum_PPN"), "N2")
                        EMI_Pembelian.TxtPembelian_NilaiPPN.Text = Format(nilai_ppn, "N2")
                        EMI_Pembelian.TxtPembelian_GrandTotal.Text = Format(dr("Grand"), "N2")
                    End If
                End Using
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            EMI_Pembelian.TxtPembelian_NoPO_Leave(DgvPO_DataLocal, e)
            EMI_Pembelian.ShowDialog()
        End If

    End Sub
End Class
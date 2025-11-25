Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class SD_Pilih_BM
    Public filter_tambahan As String
    Public asal As String
    Dim arrcari, arrIdPenanggungJawab As New ArrayList
    Dim Jenis = "Tampil_Inquiry"

    Dim lvFaktur As String
    Dim lvGudang As String
    Dim LvNoPo As String
    Dim LvNoNota As String
    Dim lvKodeSupp As String
    Dim lvTanggal As String
    Dim lvNmSupp As String




    Dim cellFaktur As Integer = 0
    Dim cellLokasi As Integer = 1
    Dim cellNoPo As Integer = 2
    Dim cellNoNota As Integer = 3
    Dim cellKodeSupplier As Integer = 4
    Dim cellNamaSupp As Integer = 5
    Dim cellTanggal As Integer = 6




    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        lvFaktur = dgvBarangMasuk.Rows(No_Index).Cells(cellFaktur).Value.ToString
        lvGudang = dgvBarangMasuk.Rows(No_Index).Cells(cellLokasi).Value.ToString
        LvNoPo = dgvBarangMasuk.Rows(No_Index).Cells(cellNoPo).Value.ToString
        LvNoNota = dgvBarangMasuk.Rows(No_Index).Cells(cellNoNota).Value.ToString
        lvKodeSupp = dgvBarangMasuk.Rows(No_Index).Cells(cellKodeSupplier).Value.ToString
        lvNmSupp = dgvBarangMasuk.Rows(No_Index).Cells(cellNamaSupp).Value.ToString
        lvTanggal = dgvBarangMasuk.Rows(No_Index).Cells(cellTanggal).Value.ToString

    End Sub
    Private Sub Get_Data()

        If cmbLokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Harus dipilih . . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbLokasi.Focus()
            Exit Sub
        End If

        Try
            OpenConn()


            dgvBarangMasuk.Rows.Clear()
            Dim index As Integer = 0
            SQL = "select a.No_Faktur,a.Kode_Stock_Owner as lokasi_gudang,b.No_PO, c.No_Nota, b.Kode_Supplier, d.Nama,c.Tanggal "
            SQL = SQL & "From EMI_BM_Per_Gudang_VS_Gudang_Tujuan a, EMI_Pembelian_Lokasi_Tujuan b, EMI_Pembelian_PO c, Suppliers d,emi_pembelian_barang_masuk e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Lokasi_Tujuan = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_PO = c.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Supplier = d.Kode_Supplier  "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Faktur = e.No_Faktur "
            SQL = SQL & "and e.status is null and a.flag_proses is null "

            If cmbLokasi.SelectedIndex = 0 Then
                SQL = SQL & " and a.kode_stock_owner IN("
                Dim list_Lokasi As String = ""
                For x As Integer = 1 To cmbLokasi.Items.Count - 1
                    list_Lokasi = list_Lokasi & "'" & cmbLokasi.Items(x).ToString & "', "
                Next

                list_Lokasi = Strings.Left(list_Lokasi, Len(list_Lokasi) - 2)

                SQL = SQL & list_Lokasi & ")"
            Else
                SQL = SQL & "and a.kode_stock_owner = '" & cmbLokasi.Text & "' "
            End If

            If txtFaktur.Text.Trim.Length <> 0 Then
                SQL = SQL & "and a.no_faktur like '%" & txtFaktur.Text & "%' "
            End If
            SQL = SQL & " group by  a.No_Faktur,a.Kode_Stock_Owner ,b.No_PO, c.No_Nota, b.Kode_Supplier,d.Nama,c.Tanggal"

            Using dr = OpenTrans(SQL)
                Do While dr.Read

                    dgvBarangMasuk.Rows.Add(1)
                    dgvBarangMasuk.Rows(index).Cells(cellFaktur).Value = dr("No_faktur")
                    dgvBarangMasuk.Rows(index).Cells(cellLokasi).Value = dr("lokasi_gudang")
                    dgvBarangMasuk.Rows(index).Cells(cellNoPo).Value = dr("no_po")
                    dgvBarangMasuk.Rows(index).Cells(cellNoNota).Value = dr("no_nota")

                    dgvBarangMasuk.Rows(index).Cells(cellKodeSupplier).Value = dr("Kode_Supplier")
                    dgvBarangMasuk.Rows(index).Cells(cellNamaSupp).Value = dr("Nama")
                    dgvBarangMasuk.Rows(index).Cells(cellTanggal).Value = Format(dr("tanggal"), "dd MMM yyyy")
                    index += 1
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub BtnInquiry_Cari_Click(sender As Object, e As EventArgs) Handles BtnInquiry_Cari.Click
        Get_Data()
    End Sub

    Private Sub BtnInquiry_Refresh_Click(sender As Object, e As EventArgs) Handles BtnInquiry_Refresh.Click
        ''CmbInquiry_Lokasi.SelectedIndex = 0
        ''TxtInquiry_Cari.Text = ""
        Get_Data()
    End Sub

    Private Sub Transaksi_Formulator_Pilih_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()
        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Try
            OpenConn()
            cmbLokasi.Items.Clear()
            cmbLokasi.Items.Add("--Seluruh--")
            SQL = "select kode_stock_owner_gudang from Binding_Lokasi_Gudang a,stock_owner b where  "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' " 'and a.kode_stock_owner = '" & EMI_Barang_Masuk_selisih.CmbSelisihBrgMsk_Lokasi.Text & "' "

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbLokasi.Items.Add(dr("kode_stock_owner_gudang"))
                Loop
            End Using

            cmbLokasi.SelectedIndex = 0
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        LblInquiry_Judul.Text = Base_Language.Lang_Pmb_Barang_Masuk_SD_Judul_Pili_PO
        LblInquiry_Lokasi.Text = Base_Language.Lang_Global_Lokasi
        lblNoFaktur.Text = Base_Language.Lang_Global_NoFaktur
        BtnInquiry_Cari.Text = Base_Language.Lang_Global_Cari


        dgvBarangMasuk.Columns(0).HeaderText = Base_Language.Lang_Global_NoFaktur
        dgvBarangMasuk.Columns(1).HeaderText = Base_Language.Lang_Global_LokasiGudang
        dgvBarangMasuk.Columns(2).HeaderText = Base_Language.Lang_Global_No_PO
        dgvBarangMasuk.Columns(3).HeaderText = Base_Language.Lang_Global_No_Nota
        dgvBarangMasuk.Columns(5).HeaderText = Base_Language.Lang_Global_Supplier
        dgvBarangMasuk.Columns(6).HeaderText = Base_Language.Lang_Global_Tanggal_PO



        Get_Data()

    End Sub

    Private Sub dgvBarangMasuk_DoubleClick(sender As Object, e As EventArgs) Handles dgvBarangMasuk.DoubleClick
        If dgvBarangMasuk.Rows.Count = 0 Or dgvBarangMasuk.SelectedRows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim currentRow = dgvBarangMasuk.CurrentRow.Index
        Dim currentCell = dgvBarangMasuk.CurrentCellAddress.X
        EMI_Selisih_Barang_Masuk.kosong()
        EMI_Selisih_Barang_Masuk.txtNoBM.Text = dgvBarangMasuk.Rows(currentRow).Cells(cellFaktur).Value
        EMI_Selisih_Barang_Masuk.txtNoPO.Text = dgvBarangMasuk.Rows(currentRow).Cells(cellNoPo).Value
        EMI_Selisih_Barang_Masuk.dtpTanggalPO.Value = dgvBarangMasuk.Rows(currentRow).Cells(cellTanggal).Value
        EMI_Selisih_Barang_Masuk.txtKodeSupplier.Text = dgvBarangMasuk.Rows(currentRow).Cells(cellKodeSupplier).Value
        EMI_Selisih_Barang_Masuk.txtNamaSupplier.Text = dgvBarangMasuk.Rows(currentRow).Cells(cellNamaSupp).Value
        EMI_Selisih_Barang_Masuk.get_data()

        EMI_Selisih_Barang_Masuk.ShowDialog()

        'Me.Close()
    End Sub

    Private Sub dgvBarangMasuk_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBarangMasuk.CellContentClick

    End Sub

    Private Sub TxtInquiry_Cari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFaktur.KeyPress
        If e.KeyChar = Chr(13) Then
            BtnInquiry_Cari_Click(Me, Nothing)
        End If
    End Sub


End Class
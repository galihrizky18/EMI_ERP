Imports System.IO

Public Class N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku

    Private WithEvents TypingTimer As New Timer()
    Dim random As New Random()

    Dim Lv_LvSplit_NoSplit, Lv_LvSplit_NoPo, Lv_LvSplit_NoFormula, Lv_LvSplit_Tanggal, Lv_LvSplit_Jam, Lv_LvSplit_Keterangan, Lv_LvSplit_KodeBarang, Lv_LvSplit_NamaBarang, Lv_LvSplit_Jumlah, Lv_LvSplit_Satuan As String

    Dim Item_LvSplit_NoSplit As Integer = 0
    Dim Item_LvSplit_NoPO As Integer = 1
    Dim Item_LvSplit_NoFormula As Integer = 2
    Dim Item_LvSplit_Tanggal As Integer = 3
    Dim Item_LvSplit_Jam As Integer = 4
    Dim Item_LvSplit_Keterangan As Integer = 5
    Dim Item_LvSplit_KodeBarang As Integer = 6
    Dim Item_LvSplit_NamaBarang As Integer = 7
    Dim Item_LvSplit_Jumlah As Integer = 8
    Dim Item_LvSplit_Satuan As Integer = 9

    Dim Lv_LvBahan_KdSo, Lv_LvBahan_KdBahan, Lv_LvBahan_NmBahan, Lv_LvBahan_Jumlah, Lv_LvBahan_Satuan As String

    Dim Item_LvBahan_KdSo As Integer = 0
    Dim Item_LvBahan_KdBahan As Integer = 1
    Dim Item_LvBahan_NmBahan As Integer = 2
    Dim Item_LvBahan_Jumlah As Integer = 3
    Dim Item_LvBahan_Satuan As Integer = 4


    Dim DataComboboxFilter As New List(Of (Value As String, Label As String)) From {
        ("a.No_Transaksi", "No Split"),
        ("a.No_PO", "No PO"),
        ("c.Kode_Formula", "No Formula"),
        ("a.Kode_Barang", "Kode Barang"),
        ("b.Nama", "Nama Barang"),
        ("c.Keterangan", "Keterangan PO")
    }

    Dim SelectedFormula As String

#Region "Section Validation"

    Dim Validation_SelectedSplit, Validation_SelectedFormula As String

    Dim Validation_Arr_Gudang As New List(Of (NoRandom As String, Lokasi As String, Status As String))

    Dim Validation_Selected_Gudang, Validation_Selected_NoRandom As New ArrayList

    Dim Dgv_DgvValidation_NoSplit, Dgv_DgvValidation_NoFormula, Dgv_DgvValidation_NoRandom, Dgv_DgvValidation_ChkBox, Dgv_DgvValidation_Lokasi, Dgv_DgvValidation_Status, Dgv_DgvValidation_Jumlah, Dgv_DgvValidation_Satuan As String

    Dim Item_DgvValidation_NoSplit As Integer = 0
    Dim Item_DgvValidation_NoFormula As Integer = 1
    Dim Item_DgvValidation_NoRandom As Integer = 2
    Dim Item_DgvValidation_ChkBox As Integer = 3
    Dim Item_DgvValidation_Lokasi As Integer = 4
    Dim Item_DgvValidation_Status As Integer = 5
    Dim Item_DgvValidation_Jumlah As Integer = 6
    Dim Item_DgvValidation_Satuan As Integer = 7


#End Region

    Private Sub N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data_Split.Columns.Clear()
        Lv_Data_Split.Columns.Add("No Split", 130, HorizontalAlignment.Left) '0
        Lv_Data_Split.Columns.Add("No PO", 130, HorizontalAlignment.Left) '1
        Lv_Data_Split.Columns.Add("No Formula", 130, HorizontalAlignment.Left) '2
        Lv_Data_Split.Columns.Add("Tanggal", 100, HorizontalAlignment.Center) '3
        Lv_Data_Split.Columns.Add("Jam", 90, HorizontalAlignment.Center) '4
        Lv_Data_Split.Columns.Add("Keterangan PO", 0, HorizontalAlignment.Left) '5
        Lv_Data_Split.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '6
        Lv_Data_Split.Columns.Add("Nama Barang", 220, HorizontalAlignment.Left) '7
        Lv_Data_Split.Columns.Add("Jumlah", 110, HorizontalAlignment.Right) '8
        Lv_Data_Split.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '9
        Lv_Data_Split.View = View.Details

        Lv_Detail_Bahan.Columns.Clear()
        Lv_Detail_Bahan.Columns.Add("Lokasi", 120, HorizontalAlignment.Left) '0
        Lv_Detail_Bahan.Columns.Add("Kode Bahan", 120, HorizontalAlignment.Left) '1
        Lv_Detail_Bahan.Columns.Add("Nama Bahan", 200, HorizontalAlignment.Left) '2
        Lv_Detail_Bahan.Columns.Add("Jumlah", 130, HorizontalAlignment.Right) '3
        Lv_Detail_Bahan.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '4
        Lv_Detail_Bahan.View = View.Details

        Lv_Validation_Detail_Bahan.Columns.Clear()
        Lv_Validation_Detail_Bahan.Columns.Add("Lokasi", 120, HorizontalAlignment.Left)
        Lv_Validation_Detail_Bahan.Columns.Add("Kode Bahan", 120, HorizontalAlignment.Left)
        Lv_Validation_Detail_Bahan.Columns.Add("Nama Bahan", 180, HorizontalAlignment.Left)
        Lv_Validation_Detail_Bahan.Columns.Add("Jumlah", 110, HorizontalAlignment.Right)
        Lv_Validation_Detail_Bahan.Columns.Add("Satuan", 70, HorizontalAlignment.Center)
        Lv_Validation_Detail_Bahan.View = View.Details

        Lv_Batch.Columns.Clear()
        Lv_Batch.Columns.Add("Batch", 100, HorizontalAlignment.Center)
        Lv_Batch.View = View.Details


        Cmb_Filter.Items.Clear()
        For Each item In DataComboboxFilter
            Cmb_Filter.Items.Add(item.Label)
        Next

        Try
            OpenConn()

            '==========================
            '=     CEK AKSES USER     =
            '==========================
            TabControl1.TabPages.Remove(TabPage2)
            If CekButtonRole("Akses_Validasi_Penyediaan_Bahan_Baku_Trial") = "Y" Then
                TabControl1.TabPages.Insert(1, TabPage2)
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Kosong()

    End Sub


    Private Sub Kosong()

        Lv_Data_Split.Items.Clear()
        Lv_Detail_Bahan.Items.Clear()
        Lv_Batch.Items.Clear()

        Cmb_Filter.SelectedIndex = -1
        Txt_Filter.Text = ""
        Txt_Filter.Enabled = False
        Txt_Filter.BackColor = Color.FromArgb(235, 235, 235)

        Txt_No_Split.Text = ""
        Txt_Kode_Barang.Text = ""
        Txt_Nama_Barang.Text = ""

        SelectedFormula = ""


        TypingTimer.Enabled = False
        Btn_Cetak_Faktur.Enabled = False

        LoadDataSplit()

    End Sub



    Private Sub Kosong_Validation()
        Validation_SelectedSplit = ""
        Validation_SelectedFormula = ""
        Txt_ScanBarcode.Text = ""
        Txt_Validation_No_Split.Text = ""
        Txt_Validation_Lokasi_Gudang.Text = ""

        Dgv_Detail_Gudang.Rows.Clear()
        Validation_Arr_Gudang.Clear()
        Validation_Selected_Gudang.Clear()
        Validation_Selected_NoRandom.Clear()
        Lv_Validation_Detail_Bahan.Items.Clear()

    End Sub


    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        LoadDataSplit()
    End Sub

    Private Sub GetDataSplit(ByVal index As Integer)
        Lv_LvSplit_NoSplit = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_NoSplit).Text
        Lv_LvSplit_NoPo = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_NoPO).Text
        Lv_LvSplit_NoFormula = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_NoFormula).Text
        Lv_LvSplit_Tanggal = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_Tanggal).Text
        Lv_LvSplit_Jam = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_Jam).Text
        Lv_LvSplit_Keterangan = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_Keterangan).Text
        Lv_LvSplit_KodeBarang = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_KodeBarang).Text
        Lv_LvSplit_NamaBarang = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_NamaBarang).Text
        Lv_LvSplit_Jumlah = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_Jumlah).Text
        Lv_LvSplit_Satuan = Lv_Data_Split.Items(index).SubItems(Item_LvSplit_Satuan).Text

    End Sub



    Private Sub GetDetailBahan(ByVal index As Integer)
        Lv_LvBahan_KdSo = Lv_Detail_Bahan.Items(index).SubItems(Item_LvBahan_KdSo).Text
        Lv_LvBahan_KdBahan = Lv_Detail_Bahan.Items(index).SubItems(Item_LvBahan_KdBahan).Text
        Lv_LvBahan_NmBahan = Lv_Detail_Bahan.Items(index).SubItems(Item_LvBahan_NmBahan).Text
        Lv_LvBahan_Jumlah = Lv_Detail_Bahan.Items(index).SubItems(Item_LvBahan_Jumlah).Text
        Lv_LvBahan_Satuan = Lv_Detail_Bahan.Items(index).SubItems(Item_LvBahan_Satuan).Text

    End Sub



    Private Sub LoadDataSplit()
        Try
            OpenConn()


            Lv_Data_Split.Items.Clear() : Lv_Detail_Bahan.Items.Clear() : Lv_Batch.Items.Clear()
            Txt_No_Split.Text = ""
            Txt_Kode_Barang.Text = ""
            Txt_Nama_Barang.Text = ""

            SQL = "select a.No_Transaksi, a.No_PO, c.Keterangan, a.Tanggal, a.Jam, c.Kode_Formula, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, a.Jumlah, a.Satuan "
            SQL &= $"from N_EMI_Transaksi_Trial_Split_Production_Order a "
            SQL &= $"inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL &= $"inner join N_EMI_Transaksi_Trial_Order_Produksi c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_PO = c.No_Faktur and c.Status is null "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.Status is null "
            SQL &= $"and a.Flag_Penyediaan_Bahan_Baku is null "
            If Cmb_Filter.SelectedIndex <> -1 Then
                SQL &= $"and {DataComboboxFilter(Cmb_Filter.SelectedIndex).Value} like '%{Txt_Filter.Text.Trim}%' "
            End If

            SQL &= $"order by a.Tanggal, a.Jam, a.No_Transaksi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data_Split.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("No_PO"))
                    Lv.SubItems.Add(Dr("Kode_Formula"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Lv_Data_Split_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data_Split.DoubleClick
        If Lv_Data_Split.Items.Count = 0 Or Lv_Data_Split.FocusedItem Is Nothing Then Exit Sub

        Dim SelectedSplit As String = Lv_Data_Split.FocusedItem.SubItems(Item_LvSplit_NoSplit).Text

        Try
            OpenConn()

            Dim Kode_Barang As String = Lv_Data_Split.FocusedItem.SubItems(Item_LvSplit_KodeBarang).Text
            Dim Nama_Barang As String = Lv_Data_Split.FocusedItem.SubItems(Item_LvSplit_NamaBarang).Text

            Txt_No_Split.Text = SelectedSplit
            Txt_Kode_Barang.Text = Kode_Barang
            Txt_Nama_Barang.Text = Nama_Barang
            SelectedFormula = Lv_Data_Split.FocusedItem.SubItems(Item_LvSplit_NoFormula).Text

            '==========================
            '=     GET DATA BATCH     =
            '==========================
            Dim arrBatchInsert As New ArrayList
            SQL = "select distinct Batch "
            SQL &= $"from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku "
            SQL &= $"where Status is null and Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and No_Split = '{SelectedSplit.Trim}' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    arrBatchInsert.Add(Dr("Batch"))
                Loop
            End Using


            Lv_Batch.Items.Clear()
            SQL = "select Jumlah_Batch from N_EMI_Transaksi_Trial_Split_Production_Order "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Transaksi = '{SelectedSplit.Trim}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dim JumlahBatch As Integer = Dr("Jumlah_Batch")
                    For i As Integer = 1 To JumlahBatch
                        If Not arrBatchInsert.Contains(i) Then
                            Lv_Batch.Items.Add(i)
                        End If
                    Next

                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show($"No Split {SelectedSplit.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Btn_Cetak_Faktur.Enabled = True


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.SelectedIndex = -1 Then
            Txt_Filter.Enabled = False
            Txt_Filter.BackColor = Color.FromArgb(235, 235, 235)
        Else
            Txt_Filter.Enabled = True
            Txt_Filter.BackColor = Color.White
            Txt_Filter.Focus()
        End If
        Txt_Filter.Text = ""
    End Sub

    Private Sub Btn_Get_Detail_Bahan_Click(sender As Object, e As EventArgs) Handles Btn_Get_Detail_Bahan.Click
        If Txt_No_Split.Text.Trim.Length = 0 Then
            Exit Sub
        ElseIf Lv_Batch.CheckedItems.Count = 0 Then
            Lv_Detail_Bahan.Items.Clear()
            Exit Sub
        End If


        Try
            OpenConn()

            Dim TotalBatch As Integer = Lv_Batch.CheckedItems.Count

            '============================
            '=     GET DETAIL BAHAN     =
            '============================
            Lv_Detail_Bahan.Items.Clear()
            SQL = "select a.No_Transaksi, e.Kode_Stock_Owner, b.Kode_Barang, c.Nama as Nama_Barang, b.Satuan, "
            SQL &= $"isnull(( select ((a.Qty_Batch * {TotalBatch}) /  "
            SQL &= $"(select dbo.ubah_satuan(z.Kode_Perusahaan, 'masa', z.Kode_Barang, r.Satuan_Hasil, x.satuan, r.Hasil) "
            SQL &= $"from Emi_Transaksi_Formulator r "
            SQL &= $"where r.Kode_Perusahaan = x.Kode_Perusahaan And r.No_Faktur = x.No_Faktur)) * x.jumlah "
            SQL &= $"from N_EMI_Transaksi_Trial_Order_Produksi z, EMI_Transaksi_Formulator_Detail_Bahan x "
            SQL &= $"where z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL &= $"and z.Kode_Formula = x.No_Faktur "
            SQL &= $"and z.Status is null "
            SQL &= $"and a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL &= $"and a.No_PO = z.No_Faktur "
            SQL &= $"and x.Kode_Barang = b.Kode_Barang "
            SQL &= $"), 0) as JumlahKebutuhan "
            SQL &= $"from N_EMI_Transaksi_Trial_Split_Production_Order a "
            SQL &= $"inner join N_EMI_Transaksi_Trial_Split_Production_Order_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            SQL &= $"inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"inner join EMI_Kategori_Gudang_PerLokasi d on c.kode_perusahaan = d.kode_perusahaan and c.id_kategori_gudang = d.id_kategori_gudang "
            SQL &= $"inner join Stock_Owner_Gudang e on d.kode_perusahaan = e.kode_perusahaan and d.lokasi_gudang = e.kode_Stock_owner "
            SQL &= $"where a.Status is null "
            SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{Txt_No_Split.Text.Trim}' "
            SQL &= $"order by b.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Bahan.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("JumlahKebutuhan"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Btn_Cetak_Faktur_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Faktur.Click
        If Txt_No_Split.Text.Trim.Length = 0 Then
            MessageBox.Show("Harap Pilih Split Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_Split.Focus()
            Exit Sub
        ElseIf Lv_Batch.CheckedItems.Count = 0 Then
            MessageBox.Show("Harap Pilih Minimal 1 Batch", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_Batch.Focus()
            Exit Sub
        ElseIf Lv_Detail_Bahan.Items.Count = 0 Then
            MessageBox.Show("Terjadi Kesalahan Detail Bahan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_Batch.Focus()
            Exit Sub
        End If

        If MessageBox.Show("Yakin Ingin Melakukan Cetak Split ini?", "Cetak Faktur Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

        Dim SelectedSplit As String = Txt_No_Split.Text.Trim
        Dim TotalBatch As Integer = Lv_Batch.CheckedItems.Count

        get_jam()

        Btn_Cetak_Faktur.Enabled = False
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Insert_Penyediaan_Bahan_Baku_Trial") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Request Material Penyediaan Bahan Baku", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Btn_Cetak_Faktur.Enabled = True
                Exit Sub
            End If




            If SelectedFormula.Trim.Length = 0 Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Terjadi Kesalahan, Kode Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Btn_Cetak_Faktur.Enabled = True
                Exit Sub
            End If

            '=====================
            '=     CEK SPLIT     =
            '=====================
            SQL = "select Status from N_EMI_Transaksi_Trial_Split_Production_Order "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Transaksi = '{SelectedSplit}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Data No Split {SelectedSplit} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Btn_Cetak_Faktur.Enabled = True
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data No Split {SelectedSplit} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Btn_Cetak_Faktur.Enabled = True
                    Exit Sub
                End If
            End Using

            '=================================================================
            '=     CEK APAKAH NO SPLIT SUDAH ADA DI TRANSAKSI SEBELUMNYA     =
            '=================================================================
            SQL = "select 1 from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku a "
            SQL &= $"inner join N_EMI_Transaksi_Trial_Split_Production_Order b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Split = b.No_Transaksi "
            SQL &= $"where a.Status is null "
            SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Split = '{SelectedSplit}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Terjadi Kesalahan, No Split {SelectedSplit} Sudah Pernah Request Material", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Btn_Cetak_Faktur.Enabled = True
                    Exit Sub
                End If
            End Using


            '============================
            '=     GENERATE BARCODE     =
            '============================


            '=======================
            '=     INSERT DATA     =
            '=======================
            Dim TempKdSo As String = ""
            Dim TempBatch As Integer = 0
            Dim BarcodeTrial As String = ""
            Dim NoRandom As String = ""
            SQL = "select a.No_Transaksi, e.Kode_Stock_Owner, b.Kode_Barang, c.Nama as Nama_Barang, b.Satuan, "
            SQL &= $"isnull(( select ((a.Qty_Batch) /  "
            SQL &= $"(select dbo.ubah_satuan(z.Kode_Perusahaan, 'masa', z.Kode_Barang, r.Satuan_Hasil, x.satuan, r.Hasil) "
            SQL &= $"from Emi_Transaksi_Formulator r "
            SQL &= $"where r.Kode_Perusahaan = x.Kode_Perusahaan And r.No_Faktur = x.No_Faktur)) * x.jumlah "
            SQL &= $"from N_EMI_Transaksi_Trial_Order_Produksi z, EMI_Transaksi_Formulator_Detail_Bahan x "
            SQL &= $"where z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL &= $"and z.Kode_Formula = x.No_Faktur "
            SQL &= $"and z.Status is null "
            SQL &= $"and a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL &= $"and a.No_PO = z.No_Faktur "
            SQL &= $"and x.Kode_Barang = b.Kode_Barang "
            SQL &= $"), 0) as JumlahKebutuhan "
            SQL &= $"from N_EMI_Transaksi_Trial_Split_Production_Order a "
            SQL &= $"inner join N_EMI_Transaksi_Trial_Split_Production_Order_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            SQL &= $"inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"inner join EMI_Kategori_Gudang_PerLokasi d on c.kode_perusahaan = d.kode_perusahaan and c.id_kategori_gudang = d.id_kategori_gudang "
            SQL &= $"inner join Stock_Owner_Gudang e on d.kode_perusahaan = e.kode_perusahaan and d.lokasi_gudang = e.kode_Stock_owner "
            SQL &= $"where a.Status is null "
            SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{Txt_No_Split.Text.Trim}' "
            SQL &= $"order by e.Kode_Stock_Owner, b.Kode_Barang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For j As Integer = 0 To Lv_Batch.Items.Count - 1

                            For i As Integer = 0 To .Rows.Count - 1

                                If Not Lv_Batch.Items(j).Checked Then
                                    Continue For
                                End If

                                Dim currentKdSo As String = .Rows(i).Item("Kode_Stock_Owner").ToString.Trim
                                Dim currentBatch As Integer = Lv_Batch.Items(j).Text

                                'If TempKdSo.Trim.Length = 0 OrElse TempKdSo <> currentKdSo OrElse TempBatch <> currentBatch Then
                                If TempKdSo.Trim.Length = 0 OrElse currentKdSo <> TempKdSo OrElse currentBatch <> TempBatch Then

                                    TempKdSo = currentKdSo
                                    TempBatch = currentBatch

                                    NoRandom = GenerateFakturRandom(15)
                                    Dim Kode_unik_print As String = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")
                                    Dim paramName As String = "@newBarcodeRMTrial" & Kode_unik_print

                                    Using ImgBarcode2 As Image = Generate_QR_NoPadding(NoRandom)
                                        Using ms2 As New MemoryStream()
                                            ImgBarcode2.Save(ms2, Imaging.ImageFormat.Png)
                                            Dim rawData2 As Byte() = ms2.ToArray()

                                            Cmd.Parameters.Add(paramName, SqlDbType.Image).Value = rawData2
                                        End Using
                                    End Using

                                    BarcodeTrial = paramName
                                End If

                                Dim Kode_Stock_Owner As String = .Rows(i).Item("Kode_Stock_Owner")
                                Dim Kode_Barang As String = .Rows(i).Item("Kode_Barang")
                                Dim Jumlah As Double = Val(HilangkanTanda(.Rows(i).Item("JumlahKebutuhan")))
                                Dim Satuan As String = .Rows(i).Item("Satuan")

                                SQL = "insert into N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku (Kode_Perusahaan, No_Split, No_Formula, No_Random, Tanggal, Jam, Kode_stock_Owner, Kode_Barang, Jumlah, Satuan, Barcode, Batch) "
                                SQL &= $"values ('{KodePerusahaan}', '{SelectedSplit.Trim}', '{SelectedFormula.Trim}', '{NoRandom.Trim}', "
                                SQL &= $"'{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
                                SQL &= $"'{Kode_Stock_Owner.Trim}', '{Kode_Barang.Trim}', "
                                SQL &= $"'{Jumlah}', '{Satuan.Trim}', {BarcodeTrial}, '{currentBatch}') "
                                ExecuteTrans(SQL)

                            Next

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Detail Bahan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Btn_Cetak_Faktur.Enabled = True
                        Exit Sub
                    End If
                End With
            End Using

            '=======================
            '=     UPDATE FLAG     =
            '=======================
#Region "Kode Lama"
            'SQL = "select 1 from N_EMI_Transaksi_Trial_Split_Production_Order "
            'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            'SQL &= $"and Status is NULL and No_Transaksi = '{SelectedSplit}' "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then

            '        Dr.Close()
            '        SQL = "update N_EMI_Transaksi_Trial_Split_Production_Order SET Flag_Penyediaan_Bahan_Baku = 'Y', "
            '        SQL &= $"Tanggal_Penyediaan_Bahan_Baku = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Penyediaan_Bahan_Baku = '{Format(tgl_skg, "HH:mm:ss")}', "
            '        SQL &= $"User_Penyediaan_Bahan_Baku = '{UserID}'  "
            '        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            '        SQL &= $"and Status is NULL and No_Transaksi = '{SelectedSplit}' "
            '        ExecuteTrans(SQL)

            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show($"Terjadi Kesalahan, No Split {SelectedSplit} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Btn_Cetak_Faktur.Enabled = True
            '        Exit Sub
            '    End If
            'End Using

#End Region
            SQL = "select a.Jumlah_Batch, isnull(( "
            SQL &= $"select count(1) from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku z where a.Kode_Perusahaan = z.kode_perusahaan "
            SQL &= $"and a.No_Transaksi = z.no_split and z.status is null and z.Flag_Validasi ='Y' "
            SQL &= $"),0) as Jumlah_Sudah_Penyediaan "
            SQL &= $"from N_EMI_Transaksi_Trial_Split_Production_Order a "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.Status is null "
            SQL &= $"and a.No_Transaksi = '{SelectedSplit}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If Dr("Jumlah_Batch") = Dr("Jumlah_Sudah_Penyediaan") Then

                        Dr.Close()
                        SQL = "update N_EMI_Transaksi_Trial_Split_Production_Order SET Flag_Penyediaan_Bahan_Baku = 'Y', "
                        SQL &= $"Tanggal_Penyediaan_Bahan_Baku = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Penyediaan_Bahan_Baku = '{Format(tgl_skg, "HH:mm:ss")}', "
                        SQL &= $"User_Penyediaan_Bahan_Baku = '{UserID}'  "
                        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"and Status is NULL and No_Transaksi = '{SelectedSplit}' "
                        ExecuteTrans(SQL)

                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Terjadi Kesalahan, No Split {SelectedSplit} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Btn_Cetak_Faktur.Enabled = True
                    Exit Sub
                End If
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Btn_Cetak_Faktur.Enabled = True
            Exit Sub
        End Try

        Cetak_Faktur(SelectedSplit)

        Kosong()

    End Sub

    Private Sub Cetak_Faktur(ByVal Split As String)
        Try
            OpenConn()

            Dim arrGudang As New ArrayList
            SQL = "select distinct e.Kode_Stock_Owner "
            SQL &= $"from N_EMI_Transaksi_Trial_Split_Production_Order a "
            SQL &= $"inner join N_EMI_Transaksi_Trial_Split_Production_Order_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            SQL &= $"inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"inner join EMI_Kategori_Gudang_PerLokasi d on c.kode_perusahaan = d.kode_perusahaan and c.id_kategori_gudang = d.id_kategori_gudang "
            SQL &= $"inner join Stock_Owner_Gudang e on d.kode_perusahaan = e.kode_perusahaan and d.lokasi_gudang = e.kode_Stock_owner "
            SQL &= $"where a.Status is null "
            SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{Split}' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    arrGudang.Add(Dr("Kode_Stock_Owner").ToString.Trim)
                Loop
            End Using

            If arrGudang.Count <> 0 Then

                Dim CrDoc As New Object

                Dim TesPrinter As String = "HP959D98 (HP Smart Tank 510 series)"

                For Each item In arrGudang

                    Dim SelectionFormula As String = ""
                    Dim kertas As String = "Faktur"

                    For j As Integer = 0 To Lv_Batch.Items.Count - 1
                        If Not Lv_Batch.Items(j).Checked Then
                            Continue For
                        End If

                        Dim CurrBatch As Integer = Lv_Batch.Items(j).Text

                        SQL = "SELECT * FROM N_EMI_View_Faktur_Penyediaan_Bahan_Baku "
                        SQL &= $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"AND No_Split = '{Split}' "
                        SQL &= $"AND Kode_stock_Owner = '{item}' "
                        SQL &= $"and Batch = {CurrBatch} "

                        SelectionFormula = "{N_EMI_View_Faktur_Penyediaan_Bahan_Baku.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                        SelectionFormula &= "AND {N_EMI_View_Faktur_Penyediaan_Bahan_Baku.No_Split} = '" & Split & "' "
                        SelectionFormula &= "AND {N_EMI_View_Faktur_Penyediaan_Bahan_Baku.Kode_stock_Owner} = '" & item & "' "
                        SelectionFormula &= "AND {N_EMI_View_Faktur_Penyediaan_Bahan_Baku.Batch} = " & CurrBatch & " "

                        Cetak_Barcode(New N_EMI_CR_Faktur_Penyediaan_Bahan_Baku, $"Faktur Request Material Gudang {item}", SQL, SelectionFormula, PrinterNameSPB, kertas)
                        'Cetak_Barcode(New N_EMI_CR_Faktur_Penyediaan_Bahan_Baku, $"Faktur Request Material Gudang {item}", SQL, SelectionFormula, TesPrinter, kertas)

                    Next

                Next
            Else
                CloseConn()
                MessageBox.Show("Terjadi Kesalahan Saat Cetak Faktur, Gudang Request Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            CloseConn()
            MessageBox.Show("Berhasil Cetak Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub





    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            Kosong()
        ElseIf TabControl1.SelectedIndex = 1 Then
            Kosong_Validation()
        End If
    End Sub


    Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click
        If Txt_ScanBarcode.Text.Trim.Length = 0 Then
            MessageBox.Show("Lakukan Scan Barcode Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_ScanBarcode.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            '====================================
            '=     CEK DATA DI DATAGRIDVIEW     =
            '====================================
            Dim Scansplit As String = ""
            Dim ScanJumlah As Double = 0
            Dim ScanSatuan As String = ""
            SQL = "select No_Split, No_Random, sum(Jumlah) as Jumlah, Satuan "
            SQL &= $"from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and status is null "
            SQL &= $"and No_Random = '{Txt_ScanBarcode.Text.Trim}' "
            SQL &= $"group by No_Split, No_Random, Satuan "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Scansplit = Dr("No_Split")
                    ScanJumlah = Format(Dr("Jumlah"), "N4")
                    ScanSatuan = Dr("Satuan")

                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim hasData As Boolean = False
            Dim foundIndex As Integer = -1
            For i As Integer = 0 To Dgv_Detail_Gudang.Rows.Count - 1
                If Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_NoSplit).Value.ToString.Trim = Scansplit.Trim _
                    AndAlso Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_NoRandom).Value.ToString.Trim = Txt_ScanBarcode.Text.Trim Then
                    foundIndex = i
                    hasData = True

                    Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_ChkBox).Value = True
                    Exit For
                End If
            Next


            If hasData Then

                Dgv_Detail_Gudang.Rows(foundIndex).Cells(Item_DgvValidation_Status).Value = "VALIDATED"
                Dgv_Detail_Gudang.Rows(foundIndex).Cells(Item_DgvValidation_Jumlah).Value = Format(ScanJumlah, "N4")
                Dgv_Detail_Gudang.Rows(foundIndex).Cells(Item_DgvValidation_Satuan).Value = ScanSatuan
                Dgv_Detail_Gudang.Rows(foundIndex).DefaultCellStyle.BackColor = Color.LightGreen

                For i As Integer = 0 To Validation_Arr_Gudang.Count - 1
                    If Validation_Arr_Gudang(i).NoRandom.Trim = Txt_ScanBarcode.Text.Trim Then
                        Dim item = Validation_Arr_Gudang(i)
                        item.Status = "VALIDATED"
                        Validation_Arr_Gudang(i) = item
                    End If
                Next

            Else

                '===========================
                '=     GET DETAIL DATA     =
                '===========================
                Validation_Arr_Gudang.Clear()
                Dim HasDataScan As Boolean = False
                Dim tempKdUnik As String = ""
                SQL = "select a.No_Split, a.No_Formula, a.No_Random, a.Kode_stock_Owner, sum(a.Jumlah) as jumlah, a.Satuan, a.Flag_Validasi "
                SQL &= $"from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku a "
                SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
                SQL &= $"and a.Status is null "
                SQL &= $"and a.No_Split = ( "
                SQL &= $"select distinct z.No_Split "
                SQL &= $"from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku z "
                SQL &= $"where z.No_Random = '{Txt_ScanBarcode.Text.Trim}' "
                SQL &= $"and a.Kode_Perusahaan = z.Kode_Perusahaan) "
                SQL &= $"group by a.No_Split, a.No_Formula, a.No_Random, a.Kode_stock_Owner, a.Satuan, a.Flag_Validasi "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                Dim CurrentSplit As String = .Rows(i).Item("No_Split")

                                If General_Class.CekNULL(.Rows(i).Item("Flag_Validasi")) = "Y" Then
                                    If .Rows(i).Item("No_Random").ToString.Trim = Txt_ScanBarcode.Text.Trim Then
                                        CloseConn()
                                        MessageBox.Show($"Kode Unik {Txt_ScanBarcode.Text.Trim} Sudah Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                    Continue For
                                End If

                                HasDataScan = True

                                If tempKdUnik.Trim <> .Rows(i).Item("No_Random").ToString.Trim Then
                                    tempKdUnik = .Rows(i).Item("No_Random").ToString.Trim
                                Else
                                    CloseConn()
                                    MessageBox.Show($"Terjadi Kesalahan, Terdapat Data Double", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                If Validation_SelectedSplit.Trim.Length = 0 Then
                                    Validation_SelectedSplit = CurrentSplit
                                Else
                                    If Validation_SelectedSplit.Trim <> CurrentSplit.Trim Then
                                        CloseConn()
                                        If MessageBox.Show("Data Trial Berbeda, Apakah ingin Reset?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbNo Then
                                            Txt_ScanBarcode.Text = ""
                                        Else
                                            Kosong_Validation()
                                        End If
                                        Exit Sub
                                    End If
                                End If

                                Dim n As Integer = Dgv_Detail_Gudang.Rows.Add()
                                Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_NoSplit).Value = .Rows(i).Item("No_Split")
                                Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_NoFormula).Value = .Rows(i).Item("No_Formula")
                                Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_NoRandom).Value = .Rows(i).Item("No_Random")
                                Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_Lokasi).Value = .Rows(i).Item("Kode_stock_Owner")

                                Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_ChkBox).Value = False


                                If .Rows(i).Item("No_Random").ToString.Trim = Txt_ScanBarcode.Text.Trim Then
                                    Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_Status).Value = "VALIDATED"
                                    Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_Jumlah).Value = Format(.Rows(i).Item("jumlah"), "N4")
                                    Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_Satuan).Value = .Rows(i).Item("Satuan")

                                    Validation_Arr_Gudang.Add((.Rows(i).Item("No_Random").ToString.Trim, .Rows(i).Item("Kode_stock_Owner").ToString.Trim, "VALIDATED"))

                                    Dgv_Detail_Gudang.Rows(n).DefaultCellStyle.BackColor = Color.LightGreen

                                    Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_ChkBox).Value = True
                                Else
                                    Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_Status).Value = "ON PROCESS"
                                    Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_Jumlah).Value = "-"
                                    Dgv_Detail_Gudang.Rows(n).Cells(Item_DgvValidation_Satuan).Value = "-"
                                    Dgv_Detail_Gudang.Rows(n).DefaultCellStyle.BackColor = Color.LightYellow
                                    Validation_Arr_Gudang.Add((.Rows(i).Item("No_Random").ToString.Trim, .Rows(i).Item("Kode_stock_Owner").ToString.Trim, "ON PROCESS"))
                                End If


                            Next
                        End If
                    End With
                End Using

                If Not HasDataScan Then
                    CloseConn()
                    MessageBox.Show($"Data Kode Unik {Txt_ScanBarcode.Text.Trim} Tidak Ditemukan atau Split Sudah Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Txt_ScanBarcode.Text = ""

        ShowDataDetailBahan()

    End Sub



    Private Sub Dgv_Detail_Gudang_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Detail_Gudang.CellEndEdit
        If Dgv_Detail_Gudang.Rows.Count = 0 Then Exit Sub

        Dim SelectedSplit As String = Dgv_Detail_Gudang.CurrentRow.Cells(Item_DgvValidation_NoSplit).Value
        Dim SelectedRandom As String = Dgv_Detail_Gudang.CurrentRow.Cells(Item_DgvValidation_NoRandom).Value
        Dim SelectedLokasi As String = Dgv_Detail_Gudang.CurrentRow.Cells(Item_DgvValidation_Lokasi).Value
        Dim SelectedChekbox As Boolean = Dgv_Detail_Gudang.CurrentRow.Cells(Item_DgvValidation_ChkBox).Value
        Dim SelectedStatus As String = Dgv_Detail_Gudang.CurrentRow.Cells(Item_DgvValidation_Status).Value

        If Dgv_Detail_Gudang.CurrentCell.ColumnIndex = Item_DgvValidation_ChkBox Then

            If SelectedChekbox Then
                If SelectedStatus.Trim <> "VALIDATED" Then
                    Dgv_Detail_Gudang.CurrentRow.Cells(Item_DgvValidation_ChkBox).Value = False
                    Exit Sub
                End If


                Txt_Validation_No_Split.Text = SelectedSplit

                If Not Validation_Selected_Gudang.Contains(SelectedLokasi) Then
                    Validation_Selected_Gudang.Add(SelectedLokasi.Trim)
                    Validation_Selected_NoRandom.Add(SelectedRandom.Trim)
                End If


                If Validation_Selected_Gudang.Count = 0 Then
                    Txt_Validation_Lokasi_Gudang.Text = SelectedLokasi
                Else
                    Dim Gudang As String = String.Join(", ", Validation_Selected_Gudang.ToArray())
                    Txt_Validation_Lokasi_Gudang.Text = Gudang
                End If

            Else

                If Validation_Selected_Gudang.Contains(SelectedLokasi) Then
                    Validation_Selected_Gudang.Remove(SelectedLokasi)
                    Validation_Selected_NoRandom.Remove(SelectedRandom)
                End If

                If Validation_Selected_Gudang.Count = 0 Then
                    Txt_Validation_Lokasi_Gudang.Text = ""
                    Txt_Validation_No_Split.Text = ""
                    Lv_Validation_Detail_Bahan.Items.Clear()
                Else
                    Txt_Validation_No_Split.Text = SelectedSplit
                    Dim Gudang As String = String.Join(", ", Validation_Selected_Gudang.ToArray())
                    Txt_Validation_Lokasi_Gudang.Text = Gudang
                End If


            End If

        End If



    End Sub

    Private Sub ShowDataDetailBahan()
        If Dgv_Detail_Gudang.Rows.Count = 0 Then
            Txt_Validation_No_Split.Text = ""
            Exit Sub
        End If

        For i As Integer = 0 To Dgv_Detail_Gudang.Rows.Count - 1

            Dim SelectedSplit As String = Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_NoSplit).Value
            Dim SelectedRandom As String = Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_NoRandom).Value
            Dim SelectedLokasi As String = Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_Lokasi).Value
            Dim SelectedChekbox As Boolean = Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_ChkBox).Value
            Dim SelectedStatus As String = Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_Status).Value

            Txt_Validation_No_Split.Text = SelectedSplit

            If SelectedChekbox Then
                If SelectedStatus.Trim <> "VALIDATED" Then
                    Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_ChkBox).Value = False
                    Exit Sub
                End If


                Txt_Validation_No_Split.Text = SelectedSplit

                If Not Validation_Selected_Gudang.Contains(SelectedLokasi) Then
                    Validation_Selected_Gudang.Add(SelectedLokasi.Trim)
                    Validation_Selected_NoRandom.Add(SelectedRandom.Trim)
                End If


                If Validation_Selected_Gudang.Count = 0 Then
                    Txt_Validation_Lokasi_Gudang.Text = SelectedLokasi
                Else
                    Dim Gudang As String = String.Join(", ", Validation_Selected_Gudang.ToArray())
                    Txt_Validation_Lokasi_Gudang.Text = Gudang
                End If

            Else

                If Validation_Selected_Gudang.Contains(SelectedLokasi) Then
                    'Validation_Selected_Gudang.Remove(SelectedLokasi)
                    'Validation_Selected_NoRandom.Remove(SelectedRandom)
                End If

                If Validation_Selected_Gudang.Count = 0 Then
                    Txt_Validation_Lokasi_Gudang.Text = ""
                    'Txt_Validation_No_Split.Text = ""
                    Lv_Validation_Detail_Bahan.Items.Clear()
                Else
                    Txt_Validation_No_Split.Text = SelectedSplit
                    Dim Gudang As String = String.Join(", ", Validation_Selected_Gudang.ToArray())
                    Txt_Validation_Lokasi_Gudang.Text = Gudang
                End If


            End If


        Next

        ShowValidationDetailBahan(Validation_Selected_NoRandom)
    End Sub

    Private Sub ShowValidationDetailBahan(ByVal ArrNoRandom As ArrayList)
        If Txt_Validation_No_Split.Text.Trim.Length = 0 Then
            Lv_Validation_Detail_Bahan.Items.Clear()
            Exit Sub
        End If

        Try
            OpenConn()

            Dim DataRandom As String = String.Join(", ", Validation_Selected_NoRandom.Cast(Of Object)().Select(Function(x) $"'{x}'"))

            Lv_Validation_Detail_Bahan.Items.Clear()
            SQL = "select a.No_Split, a.No_Random, a.Kode_stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan "
            SQL &= $"from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku a "
            SQL &= $"inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.Status is null "
            SQL &= $"and a.No_Random in ({DataRandom}) "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Validation_Detail_Bahan.Items.Add(Dr("Kode_stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Btn_Validasi_Click(sender As Object, e As EventArgs) Handles Btn_Validasi.Click
        If Txt_Validation_No_Split.Text.Trim.Length = 0 Then
            MessageBox.Show("Lakukan Scan Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Validation_No_Split.Focus()
            Exit Sub
        End If

        '==========================================
        '=     CEK APAKAH SEMUA SUDAH DI SCAN     =
        '==========================================
        For i As Integer = 0 To Dgv_Detail_Gudang.Rows.Count - 1
            If Not Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_ChkBox).Value Then
                MessageBox.Show("Terdapat Kode Unik yang Belum DI Validasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim SelectedSplit As String = Txt_Validation_No_Split.Text.Trim

            '==========================
            '=     CEK ROLE AKSES     =
            '==========================
            If CekButtonRole("Akses_Validasi_Penyediaan_Bahan_Baku_Trial") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Validasi Penyediaan Bahan Baku", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Btn_Cetak_Faktur.Enabled = True
                Exit Sub
            End If

            '==========================================================
            '=     GET JUMLAH DATA DI DATAGRIDVIEW BY KODE RANDOM     =
            '==========================================================
            Dim TotDgvKodeRandom As Integer = 0
            Dim DgvCurKodeRandom As String = ""
            For i As Integer = 0 To Dgv_Detail_Gudang.Rows.Count - 1
                If Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_ChkBox).Value Then
                    If Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_NoRandom).Value.ToString.Trim <> DgvCurKodeRandom Then
                        TotDgvKodeRandom += 1
                        DgvCurKodeRandom = Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_NoRandom).Value.ToString.Trim
                    End If
                End If
            Next

            '=======================================
            '=     CEK APAKAH SPLIT DIBATALKAN     =
            '=======================================
            SQL = "select a.Status as Status_Split, b.Status as Status_Penyediaan "
            SQL &= $"from N_EMI_Transaksi_Trial_Split_Production_Order a "
            SQL &= $"inner join N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Split "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{SelectedSplit}' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            If General_Class.CekNULL(.Rows(i).Item("Status_Split")) = "Y" Then
                                CloseConn()
                                CloseTrans()
                                MessageBox.Show("Data Split Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(.Rows(i).Item("Status_Penyediaan")) = "Y" Then
                                CloseConn()
                                CloseTrans()
                                MessageBox.Show("Data Penyediaan Bahan Baku Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Next
                    Else
                        CloseConn()
                        CloseTrans()
                        MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

#Region "Kode Lama"

            'If Validation_Selected_NoRandom.Count <> 0 Then
            '    Dim TotDBKodeRandom As Integer = 0
            '    Dim DBCurKodeRandom As String = ""
            '    For i As Integer = 0 To Validation_Selected_NoRandom.Count - 1


            '        Dim KodeUnik As String = Validation_Selected_NoRandom(i)

            '        '==================================
            '        '=     CEK DATA PER KODE UNIK     =
            '        '==================================
            '        SQL = "select No_Split, No_Formula, No_Random, Kode_stock_Owner, Kode_Barang, Jumlah, Satuan "
            '        SQL &= $"from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku "
            '        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            '        SQL &= $"and No_Split = '{SelectedSplit}' "
            '        SQL &= $"and No_Random = '{KodeUnik}' "
            '        SQL &= $"and Flag_Validasi is null "
            '        Using Dr = OpenTrans(SQL)
            '            If Dr.Read Then

            '                Dr.Close()
            '                SQL = $"update N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku set Flag_Validasi = 'Y', "
            '                SQL &= $"Tanggal_Validasi = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi = '{Format(tgl_skg, "HH:mm:ss")}', User_Validasi = '{UserID}' "
            '                SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            '                SQL &= $"and No_Split = '{SelectedSplit}' "
            '                SQL &= $"and No_Random = '{KodeUnik}' "
            '                SQL &= $"and Flag_Validasi is null "
            '                ExecuteTrans(SQL)

            '            Else
            '                Dr.Close()
            '                CloseConn()
            '                CloseTrans()
            '                MessageBox.Show("Terjadi Kesalahan, Data Kode Unik Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                Exit Sub
            '            End If
            '        End Using

            '        If KodeUnik.Trim <> DBCurKodeRandom.Trim Then
            '            TotDBKodeRandom += 1
            '            DBCurKodeRandom = KodeUnik
            '        End If


            '    Next

            '    If TotDBKodeRandom <> TotDgvKodeRandom Then
            '        CloseConn()
            '        CloseTrans()
            '        MessageBox.Show("Terjadi Kesalahan, Data pada DB data Form berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If


            'Else
            '    CloseConn()
            '    CloseTrans()
            '    MessageBox.Show("Terjadi Kesalahan, Kode Unik Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

#End Region

            For i As Integer = 0 To Dgv_Detail_Gudang.Rows.Count - 1

                Dim KodeUnik As String = Dgv_Detail_Gudang.Rows(i).Cells(Item_DgvValidation_NoRandom).Value
                '==================================
                '=     CEK DATA PER KODE UNIK     =
                '==================================
                SQL = "select No_Split, No_Formula, No_Random, Kode_stock_Owner, Kode_Barang, Jumlah, Satuan "
                SQL &= $"from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku "
                SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                SQL &= $"and No_Split = '{SelectedSplit}' "
                SQL &= $"and No_Random = '{KodeUnik}' "
                SQL &= $"and Flag_Validasi is null "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        SQL = $"update N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku set Flag_Validasi = 'Y', "
                        SQL &= $"Tanggal_Validasi = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi = '{Format(tgl_skg, "HH:mm:ss")}', User_Validasi = '{UserID}' "
                        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"and No_Split = '{SelectedSplit}' "
                        SQL &= $"and No_Random = '{KodeUnik}' "
                        SQL &= $"and Flag_Validasi is null "
                        ExecuteTrans(SQL)

                    Else
                        Dr.Close()
                        CloseConn()
                        CloseTrans()
                        MessageBox.Show("Terjadi Kesalahan, Data Kode Unik Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            Next


            Cmd.Transaction.Commit()
            CloseConn()
            CloseTrans()
            MessageBox.Show("Data Berhasil Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            CloseTrans()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong_Validation()

    End Sub

    '==========================================================================================================================================================
    '=     UTILITY
    '==========================================================================================================================================================

    Private Function GenerateFakturRandom(ByVal Length As Integer) As String
        Dim karakter As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim random As New Random()
        Dim hasil As String = ""

        For i As Integer = 1 To Length
            Dim index As Integer = random.Next(0, karakter.Length)
            hasil &= karakter(index)
        Next

        Return hasil
    End Function

    Private Function Generate_QR_NoPadding(ByVal isi As String)

        Dim options As New ZXing.QrCode.QrCodeEncodingOptions()

        options.DisableECI = True
        options.CharacterSet = "UTF-8"
        options.Width = 80
        options.Height = 80
        options.Margin = 0
        options.PureBarcode = True

        Dim qr As New ZXing.BarcodeWriter()
        qr.Format = ZXing.BarcodeFormat.QR_CODE
        qr.Options = options

        Dim result As New Bitmap(qr.Write(isi))
        Return result
    End Function


    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub Lv_Data_Split_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data_Split.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Data_Split.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            ' Mouse sedang berada di atas row
            Lv_Data_Split.Cursor = Cursors.Hand
        Else
            ' Mouse tidak mengenai row
            Lv_Data_Split.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Lv_Data_Split_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data_Split.MouseLeave
        Lv_Data_Split.Cursor = Cursors.Default
    End Sub

    Private Sub Lv_Batch_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles Lv_Batch.ItemChecked
        TypingTimer.Stop()
        TypingTimer.Start()
    End Sub

    Private Sub TypingTimer_Tick(sender As Object, e As EventArgs) Handles TypingTimer.Tick
        TypingTimer.Stop()

        Btn_Get_Detail_Bahan_Click(Nothing, Nothing)
        'Btn_Get_Detail_Bahan().PerformClick()

    End Sub

End Class
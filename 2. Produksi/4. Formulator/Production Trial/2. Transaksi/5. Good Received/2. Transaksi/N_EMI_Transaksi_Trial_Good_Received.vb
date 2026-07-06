Public Class N_EMI_Transaksi_Trial_Good_Received


    Dim PageSize As Integer = 20
    Dim CurrentPage As Integer = 1
    Dim totalpage As Integer = 0
    Dim totalPages As Integer = 0


    Dim arrFilter As New ArrayList

    Private Sub N_EMI_Transaksi_Trial_Good_Received_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Lv_GR.Columns.Clear()
        Lv_GR.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("No PO", 130, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_GR.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_GR.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Barang", 300, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Batch", 90, HorizontalAlignment.Center)
        Lv_GR.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_GR.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_GR.View = View.Details

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("No Transaksi") : arrFilter.Add("a.No_Transaksi")
        Cmb_Filter.Items.Add("No PO") : arrFilter.Add("a.No_PO")
        Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
        Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("c.nama")



        Kosong()

    End Sub

    Public Sub Kosong()

        Cmb_Filter.SelectedIndex = -1
        Lv_GR.Items.Clear()


        LoadData()

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Filter Dahulu", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cmb_Filter.DroppedDown = True
            Cmb_Filter.Focus()
            Exit Sub
        Else
            If Txt_FilterValue.Text.Trim = "" Then
                MessageBox.Show("Masukkan Nilai Filter Dahulu", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Txt_FilterValue.Focus()
                Exit Sub
            End If
        End If


        LoadData()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub LoadData(Optional ByVal page As Integer = 1)
        Try
            OpenConn()

            CurrentPage = page
            Dim Tot_Data As Double = 0
            SQL = "SELECT COUNT(1) AS Total_Data "
            SQL &= "FROM N_EMI_Transaksi_Trial_Split_Production_Order a "
            SQL &= "INNER JOIN N_EMI_Transaksi_Trial_Order_Produksi b ON a.kode_perusahaan = b.Kode_Perusahaan AND a.No_PO = b.No_Faktur AND b.Status IS NULL "
            SQL &= "INNER JOIN barang c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND a.Kode_Stock_Owner = c.Kode_Stock_Owner AND a.Kode_Barang = c.Kode_Barang "
            SQL &= "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL &= "AND a.Status IS NULL "
            SQL &= "AND a.Flag_Hasil_Produksi_GR IS NULL "

            If Cmb_Filter.SelectedIndex <> -1 AndAlso Txt_FilterValue.Text.Trim <> "" Then
                SQL &= "AND " & arrFilter(Cmb_Filter.SelectedIndex) & " LIKE '%" & Txt_FilterValue.Text.Trim & "%' "
            End If

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Tot_Data = Convert.ToDouble(Dr("Total_Data"))
                End If
            End Using

            totalPages = Math.Ceiling(Tot_Data / PageSize)
            If totalPages = 0 Then totalPages = 1
            Dim offset As Integer = (page - 1) * PageSize

            Lv_GR.Items.Clear()
            SQL = "SELECT a.No_Transaksi, a.No_PO, a.Tanggal, a.Jam, a.No_Batch, a.Kode_Barang, c.nama AS Nama_Barang, a.Jumlah, a.Satuan, a.Jumlah_Batch "
            SQL &= "FROM N_EMI_Transaksi_Trial_Split_Production_Order a "
            SQL &= "INNER JOIN N_EMI_Transaksi_Trial_Order_Produksi b ON a.kode_perusahaan = b.Kode_Perusahaan AND a.No_PO = b.No_Faktur AND b.Status IS NULL "
            SQL &= "INNER JOIN barang c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND a.Kode_Stock_Owner = c.Kode_Stock_Owner AND a.Kode_Barang = c.Kode_Barang "
            SQL &= "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL &= "AND a.Status IS NULL "
            SQL &= "AND a.Flag_Hasil_Produksi_GR IS NULL "
            If Cmb_Filter.SelectedIndex <> -1 AndAlso Txt_FilterValue.Text.Trim <> "" Then
                SQL &= "AND " & arrFilter(Cmb_Filter.SelectedIndex) & " LIKE '%" & Txt_FilterValue.Text.Trim & "%' "
            End If
            SQL &= "ORDER BY a.Tanggal, a.Jam, a.No_Transaksi, a.Kode_Barang "
            SQL &= "OFFSET " & offset & " ROWS FETCH NEXT " & PageSize & " ROWS ONLY"
            Using Dr = OpenTrans(SQL)
                Lv_GR.BeginUpdate()
                Do While Dr.Read
                    Dim Lv As ListViewItem = Lv_GR.Items.Add(Dr("No_Transaksi").ToString())
                    Lv.SubItems.Add(Dr("No_PO").ToString())
                    Lv.SubItems.Add(If(IsDBNull(Dr("Tanggal")), "", Format(Dr("Tanggal"), "dd MMM yyyy")))
                    Lv.SubItems.Add(Dr("Jam").ToString())
                    Lv.SubItems.Add(Dr("Kode_Barang").ToString())
                    Lv.SubItems.Add(Dr("Nama_Barang").ToString())
                    Lv.SubItems.Add(Dr("Jumlah_Batch").ToString())
                    Lv.SubItems.Add(Dr("Jumlah").ToString())
                    Lv.SubItems.Add(Dr("Satuan").ToString())
                Loop
                Lv_GR.EndUpdate()
            End Using

            Txt_Pages_1.Text = page & " of " & totalPages
            UpdateNavButtons() '



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.Items.Count = 0 Then Exit Sub

        If Cmb_Filter.SelectedIndex <> -1 Then
            Txt_FilterValue.Enabled = True
        Else
            Txt_FilterValue.Enabled = False
        End If

        Txt_FilterValue.Text = ""

    End Sub


    Private Sub Lv_GR_DoubleClick(sender As Object, e As EventArgs) Handles Lv_GR.DoubleClick
        If Lv_GR.Items.Count = 0 Or Lv_GR.FocusedItem Is Nothing Then Exit Sub

        Dim NoSplit As String = Lv_GR.FocusedItem.Text

        Try
            OpenConn()


            If CekButtonRole("Input_Trial_Good_Received_1") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses untuk melakukan input Godd Received Trial ! !")
                Exit Sub
            End If

            '============================================
            '=     CEK APAKAH DATA ADA / DIBATALKAN     =
            '============================================
            SQL = "select Status from N_EMI_Transaksi_Trial_Split_Production_Order "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and No_Transaksi = '{NoSplit}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show($"No Split {NoSplit} Sudah Dibatkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show($"Data pada No Split {NoSplit} Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '==========================================================
            '=     CEK APAKAH DATA SUDAH REQUEST PENYEDIAAN BAHAN     =
            '==========================================================
            Dim hasDataPenyediaanBahan As Boolean = False
            SQL = "select Kode_Barang, Flag_Validasi from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and No_Split = '{NoSplit}' "
            SQL &= $"and Status is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim KdBahan As String = Dr("Kode_Barang")
                    If General_Class.CekNULL(Dr("Flag_Validasi")) = "" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show($"Terjadi Kesalagan Kode Bahan {KdBahan} Belum Divalidasi Penyediaan Bahan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    hasDataPenyediaanBahan = True
                Loop
            End Using

            If Not hasDataPenyediaanBahan Then
                CloseConn()
                MessageBox.Show($"No Split {NoSplit} Belum melakukan request penyediaan bahan baku", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        N_EMI_SD_Trial_Good_Received.Txt_NoSplit.Text = NoSplit
        N_EMI_SD_Trial_Good_Received.ShowDialog()



    End Sub



    '===============================================================================================================================================================================================================
    '=     UTILITIES
    '===============================================================================================================================================================================================================

    Private Sub UpdateNavButtons()
        BtnPrev_GI.Enabled = (CurrentPage > 1)
        BtnNext_GI.Enabled = (CurrentPage < totalPages)
    End Sub


    Private Sub BtnFirst_GI_Click(sender As Object, e As EventArgs) Handles BtnFirst_GI.Click
        CurrentPage = 1
        LoadData(CurrentPage)
    End Sub

    Private Sub BtnPrev_GI_Click(sender As Object, e As EventArgs) Handles BtnPrev_GI.Click
        If CurrentPage > 1 Then LoadData(CurrentPage - 1)
    End Sub

    Private Sub BtnNext_GI_Click(sender As Object, e As EventArgs) Handles BtnNext_GI.Click
        If CurrentPage < totalPages Then LoadData(CurrentPage + 1)
    End Sub

    Private Sub Lv_GR_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_GR.MouseMove
        Dim info As ListViewHitTestInfo = Lv_GR.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            ' Mouse sedang berada di atas row
            Lv_GR.Cursor = Cursors.Hand
        Else
            ' Mouse tidak mengenai row
            Lv_GR.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Lv_GR_MouseLeave(sender As Object, e As EventArgs) Handles Lv_GR.MouseLeave
        Lv_GR.Cursor = Cursors.Default
    End Sub


End Class
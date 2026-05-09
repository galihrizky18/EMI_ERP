Public Class N_EMI_Transaksi_Validasi_Formula_BOD
    Private ArrFilter() As String = {
        "a.No_Faktur",
        "a.Kode_Barang",
        "b.Nama"
    }

    Private Sub N_EMI_Transaksi_Validasi_Formula_BOD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Faktur", 100, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 280, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Status", 125, HorizontalAlignment.Center)
        Lv_Data.View = View.Details

        Load_Lv_Data()
    End Sub

    Private Sub Load_Lv_Data()
        Try
            OpenConn()

            Lv_Data.Items.Clear()

            Dim Filter As String = ""

            If Cmb_Kolom_Filter.SelectedIndex <> -1 AndAlso Txt_Value_Filter.Text.Trim <> "" Then
                Dim kolom As String = ArrFilter(Cmb_Kolom_Filter.SelectedIndex)
                Dim value As String = Txt_Value_Filter.Text.Trim

                Filter = $" AND {kolom} LIKE '%{value}%'"
            End If

            SQL = $"
                SELECT 
                    a.No_Faktur, 
                    a.Kode_Barang, 
                    b.Nama AS Nama_Barang, 
                    a.Hasil, 
                    a.Satuan_Hasil, 
                    a.Tanggal,
                    'SIAP PRODUKSI' as Status
                FROM Emi_Transaksi_Formulator a
                INNER JOIN barang b 
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan 
                    AND a.Kode_Stock_Owner = b.Kode_Stock_Owner 
                    AND a.Kode_Barang = b.Kode_Barang_Inq
                WHERE 
                    a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.Status IS NULL
                    AND a.Flag_Lanjut_Produksi = 'Y'
                    AND a.Flag_Validasi_Formula_Produksi_BOD IS NULL
                    {Filter}
                ORDER BY a.Tanggal, a.Jam
            "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Hasil"))), "N4"))
                    Lv.SubItems.Add(Dr("Satuan_Hasil"))
                    Lv.SubItems.Add(Dr("Status"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal mendapatkan data: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Load_Lv_Data()
    End Sub

    Private Sub Txt_Value_Filter_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Value_Filter.KeyDown
        If e.KeyCode = Keys.Enter Then
            Load_Lv_Data()
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Cmb_Kolom_Filter.SelectedIndex = -1
        Txt_Value_Filter.Clear()
        Load_Lv_Data()
    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Dim No_faktur As String = Lv_Data.FocusedItem.SubItems(0).Text

        Dim frm As New N_EMI_SD_Transaksi_Validasi_Formula_BOD With {
            .No_Faktur = No_faktur
        }
        frm.ShowDialog()

        Load_Lv_Data()
    End Sub
End Class
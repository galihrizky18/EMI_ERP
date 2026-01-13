Public Class Tes_Form_Loading


    Private Sub Tes_Form_Loading_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ListView1.Columns.Clear()
        ListView1.Columns.Add("Nomor", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Serial number", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Jumlah Awal", 130, HorizontalAlignment.Right)
        ListView1.Columns.Add("Jumlah Akhri", 130, HorizontalAlignment.Right)
        ListView1.View = View.Details


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pesanError As String = String.Empty
        Dim dt As New DataTable()

        Using frmLoad As New Tes_SD_Loading()
            Task.Run(Sub()
                         Try
                             ' 1. Ambil Data (Background Thread) - Tidak bikin macet
                             OpenConn()
                             SQL = "select Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Jumlah_Update "
                             SQL &= $"from N_EMI_LOG_Barang_SN "
                             Using Dr = OpenTrans(SQL)
                                 dt.Load(Dr)
                             End Using
                             CloseConn()

                             ' 2. Masukkan ke ListView (UI Thread) - Loading masih TAMPIL
                             ' Kita gunakan Me.Invoke agar aman mengakses ListView dari dalam Task
                             Me.Invoke(Sub()
                                           ListView1.BeginUpdate()
                                           ListView1.Items.Clear()

                                           Dim noUrut As Integer = 1
                                           For Each row As DataRow In dt.Rows
                                               Dim LV As ListViewItem
                                               LV = ListView1.Items.Add(noUrut.ToString())
                                               LV.SubItems.Add(row("Kode_Stock_Owner").ToString())
                                               LV.SubItems.Add(row("Kode_Barang").ToString())
                                               LV.SubItems.Add(row("Serial_Number").ToString())
                                               LV.SubItems.Add(row("Jumlah_Awal").ToString())
                                               LV.SubItems.Add(row("Jumlah_Update").ToString())
                                               noUrut += 1
                                           Next
                                           ListView1.EndUpdate()
                                       End Sub)

                         Catch ex As Exception
                             pesanError = ex.Message
                             CloseConn()
                         Finally
                             ' 3. Tutup Loading setelah SEMUA selesai (Data ditarik & ListView terisi)
                             If Not frmLoad.IsDisposed Then
                                 frmLoad.Invoke(Sub() frmLoad.Close())
                             End If
                         End Try
                     End Sub)

            frmLoad.ShowDialog(Me)
        End Using

        ' 4. Cukup tampilkan pesan status
        If Not String.IsNullOrEmpty(pesanError) Then
            MessageBox.Show("Terjadi Kesalahan: " & pesanError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show("Proses Selesai!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


End Class
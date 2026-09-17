Public Class N_EMI_Display_RFID_Readers
    Private Sub N_EMI_Master_Data_RFID_Readers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FetchRFIDReaders()
    End Sub

    Private Sub FetchRFIDReaders()
        Try
            OpenConn()
            DGV_RFID_Readers.Rows.Clear()

            Dim SQL As String = "SELECT IP_Address, Kode_Perangkat, Keterangan " &
                           "FROM N_EMI_Master_Data_RFID_Readers"

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim rowIndex As Integer = DGV_RFID_Readers.Rows.Add()
                    DGV_RFID_Readers.Rows(rowIndex).Cells("IP_Address").Value = Dr("IP_Address")
                    DGV_RFID_Readers.Rows(rowIndex).Cells("Kode_Perangkat").Value = Dr("Kode_Perangkat")
                    DGV_RFID_Readers.Rows(rowIndex).Cells("Keterangan").Value = If(IsDBNull(Dr("Keterangan")), "", Dr("Keterangan"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_Search_Click(sender As Object, e As EventArgs) Handles Btn_Search.Click
        Dim searchValue As String = Txt_Search.Text.Trim()

        If String.IsNullOrWhiteSpace(searchValue) Then
            MessageBox.Show("Masukkan RFID Tag yang ingin dicari", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Search.Focus()
            Exit Sub
        End If

        Try
            OpenConn()
            DGV_RFID_Readers.Rows.Clear()

            Dim SQL As String = "SELECT IP_Address, Kode_Perangkat, Keterangan " &
                           "FROM N_EMI_Master_Data_RFID_Readers " &
                           "WHERE IP_Address LIKE '%" & searchValue & "%' " &
                           "OR Kode_Perangkat LIKE '%" & searchValue & "%' " &
                           "OR Keterangan LIKE '%" & searchValue & "%' "

            Using Dr = OpenTrans(SQL)
                If Dr.HasRows Then
                    Do While Dr.Read
                        Dim rowIndex As Integer = DGV_RFID_Readers.Rows.Add()
                        DGV_RFID_Readers.Rows(rowIndex).Cells("IP_Address").Value = Dr("IP_Address")
                        DGV_RFID_Readers.Rows(rowIndex).Cells("Kode_Perangkat").Value = Dr("Kode_Perangkat")
                        DGV_RFID_Readers.Rows(rowIndex).Cells("Keterangan").Value = Dr("Keterangan")
                    Loop
                Else
                    MessageBox.Show("Data tidak ditemukan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Txt_Search.Text = ""
        FetchRFIDReaders()
    End Sub
End Class
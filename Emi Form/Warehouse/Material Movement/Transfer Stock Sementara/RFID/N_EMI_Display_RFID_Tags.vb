Imports System.Data.SqlClient
Imports QRCoder

Public Class N_EMI_Display_RFID_Tags
    Private Sub N_EMI_Display_RFID_Tags_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FetchRFIDTags()
    End Sub

    Public Sub FetchRFIDTags()
        Try
            OpenConn()
            DGV_RFID_Tags.Rows.Clear()

            Dim SQL As String = "SELECT RFID_Tag, Status, RFID_Label FROM N_EMI_Master_Data_RFID_Tags ORDER BY Tanggal DESC, Jam DESC"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim rowIndex As Integer = DGV_RFID_Tags.Rows.Add()
                    DGV_RFID_Tags.Rows(rowIndex).Cells("RFID_Tag").Value = Dr("RFID_Tag")
                    DGV_RFID_Tags.Rows(rowIndex).Cells("RFID_Label").Value = Dr("RFID_Label")
                    Dim statusValue As String = If(IsDBNull(Dr("Status")) OrElse String.IsNullOrWhiteSpace(Dr("Status")), "Tidak Terpakai", "Terpakai")
                    DGV_RFID_Tags.Rows(rowIndex).Cells("Status").Value = statusValue
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
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
            DGV_RFID_Tags.Rows.Clear()

            Dim SQL As String = "SELECT RFID_Tag, Status, RFID_Label " &
                           "FROM N_EMI_Master_Data_RFID_Tags " &
                           "WHERE RFID_Tag LIKE '%" & searchValue & "%' " &
                           "OR RFID_Label LIKE '%" & searchValue & "%' " &
                           "OR Status LIKE '%" & searchValue & "%' " &
                           "ORDER BY Tanggal DESC, Jam DESC"

            Using Dr = OpenTrans(SQL)
                If Dr.HasRows Then
                    Do While Dr.Read
                        Dim rowIndex As Integer = DGV_RFID_Tags.Rows.Add()
                        DGV_RFID_Tags.Rows(rowIndex).Cells("RFID_Tag").Value = Dr("RFID_Tag")
                        DGV_RFID_Tags.Rows(rowIndex).Cells("RFID_Label").Value = Dr("RFID_Label")

                        Dim statusValue As String = If(IsDBNull(Dr("Status")) OrElse String.IsNullOrWhiteSpace(Dr("Status")), "Tidak Terpakai", "Terpakai")
                        DGV_RFID_Tags.Rows(rowIndex).Cells("Status").Value = statusValue
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
        FetchRFIDTags()
    End Sub
End Class
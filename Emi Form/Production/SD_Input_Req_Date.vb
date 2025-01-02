Imports System.Net.Security
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class SD_Input_Req_Date
    Public Baris As Integer
    Private Sub SD_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Transaksi_Permintaan_BB_Produksi")
            CloseConn()

            get_jam()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Label2.Text = Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Req_Date
        Label5.Text = Base_Language.Lang_Global_Catatan
        Btn_Cari.Text = "OK"
        TextBox3.Text = "-"

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error2, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus() : Exit Sub
        ElseIf Format(DateTimePicker1.Value, "yyyy-MM-dd") < Format(tgl_skg, "yyyy-MM-dd") Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error13, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Focus() : Exit Sub
        End If

        Transaksi_Permintaan_BB_Produksi.DataGridView1.Rows(Baris).Cells(5).Value = Format(DateTimePicker1.Value, "yyyy-MM-dd")
        Transaksi_Permintaan_BB_Produksi.DataGridView1.Rows(Baris).Cells(6).Value = TextBox3.Text
        Me.Close()
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub
End Class
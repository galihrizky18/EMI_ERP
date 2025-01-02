Public Class Display_Kurs
    Public indexbrp As Integer

    Private Sub Display_Kurs_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ComboBox1.Focus()
    End Sub

    Private Sub Display_Kurs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            ComboBox1.Items.Clear()
            SQL = "Select Kode_mata_uang From mata_uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_mata_uang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Kode_Mata_Uang"))
                Loop
            End Using

            ComboBox1.SelectedIndex = -1
            TextBox2.Text = ""

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub


    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Kurs Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
            'ElseIf Val(TextBox2.Text) = 0 Then
            '    MessageBox.Show("Nilai MUA Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox2.Focus()
            '    Exit Sub
        End If

        Transaksi_Biaya_import3.DataGridView1.Rows(indexbrp).Cells(Transaksi_Biaya_import3.cellMataUangBilling).Value = ComboBox1.Text 
        Transaksi_Biaya_import3.DataGridView1.Rows(indexbrp).Cells(Transaksi_Biaya_import3.cellBiayaBilling).Value = Format(Val(TextBox2.Text), "N2")

        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class

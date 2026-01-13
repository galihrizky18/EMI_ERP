
Public Class SD_Ubah_Harga
    Dim Arr1, Arr2, Arr3, arrbatal As New ArrayList
    Dim Clr_Selesai As Color = Color.LightBlue
    Dim Clr_Batal As Color = Color.Black
    Dim Clr_Default As Color = Color.Blue
    Public dari_mana As String
    Public filter_tambahan As String

    Dim varTanggalTarik As String
    Dim varTanggalTarikReal As String
    Dim beratTimbanganBeaCukai As Decimal

    Public indexFocused As Integer


    Private Sub Simpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Simpan.Click


        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah harus di isi")
        End If

        Try
            OpenConn()

            Pembelian_Pry.ubah_listview(TextBox2.Text, TextBox1.Text)

            Me.Close()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub TanggalTarik_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    End Sub

    'Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If e.KeyChar = Chr(13) Then Simpan.Focus()
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    'End Sub

    Private Sub Display_Tanggal_Tarik_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Simpan_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Simpan.Click

    End Sub

    Private Sub TextBox1_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then Button2.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub
End Class
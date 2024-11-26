Public Class TesUnit
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Label1.Text = Tambah(2, 3)
        MessageBox.Show(Tambah(2, 3))


    End Sub


    Public Function Tambah(a, b) As String
        Return a + b
    End Function


End Class
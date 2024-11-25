Public Class TesAsync
    Private Sub TesAsync_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Await TesAsync()

        function2()

        MessageBox.Show("Masuk Syncroous")

    End Sub



    Private Async Function TesAsync() As Task


        Await Task.Delay(5000)

        MessageBox.Show("Async Selesai")

    End Function

    Private Sub function2()

    End Sub

End Class
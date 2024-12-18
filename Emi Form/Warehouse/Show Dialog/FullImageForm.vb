Public Class FullImageForm

    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const DROPSHADOW = &H20000
            Dim cParam As CreateParams = MyBase.CreateParams
            cParam.ClassStyle = cParam.ClassStyle Or DROPSHADOW
            Return cParam
        End Get
    End Property

    Private Sub FullImageForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.StartPosition = FormStartPosition.CenterScreen

    End Sub

    Private Sub FullImageForm_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        Me.Dispose()
    End Sub

End Class

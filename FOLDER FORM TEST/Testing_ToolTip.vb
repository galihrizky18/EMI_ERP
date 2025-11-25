Public Class Testing_ToolTip


    Private Sub Testing_ToolTip_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim tip As New ToolTip()

        tip.AutoPopDelay = 5000  ' durasi tampil (ms)
        tip.InitialDelay = 500    ' jeda sebelum muncul
        tip.ReshowDelay = 2000
        tip.ShowAlways = True     ' tampil walau form tidak fokus

        ' Tentukan tooltip untuk tombol
        tip.SetToolTip(Button1, "INI BUTTON")
        tip.SetToolTip(TextBox1, "INI TEXTBOX")
        tip.SetToolTip(DataGridView1, "INI DGV")
        tip.SetToolTip(ListView1, "INI LISTVIEW")

    End Sub


End Class
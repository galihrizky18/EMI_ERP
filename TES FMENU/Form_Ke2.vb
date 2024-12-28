Imports System.ComponentModel

Public Class Form_Ke2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Form_KE3.Show()
        Exit Sub
    End Sub


    Private Sub Form_Ke2_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Form_Awal.Show()

    End Sub

    Private Sub Form_Ke2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = Me.GetType().FullName
    End Sub


    Protected Friend Sub TesLoad(ByVal tes As String)
        TextBox1.Text = tes
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = "asdsadas"
    End Sub
End Class
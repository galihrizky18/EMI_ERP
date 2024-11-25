Imports Newtonsoft.Json.Linq

Public Class TEs

    Private Sub TEs_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub TEs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit

        Dim value1 As String = DataGridView1.CurrentRow.Cells(0).Value
        Dim value2 As String = DataGridView1.CurrentRow.Cells(1).Value

        Dim nilai As Decimal = Decimal.Parse(value1)
        Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

        DataGridView1.CurrentRow.Cells(0).Value = formattedValue

    End Sub

    Private Sub DataGridView1_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellLeave

        If DataGridView1.CurrentCell.ColumnIndex = 0 Then
            Dim value1 As String = DataGridView1.CurrentRow.Cells(0).Value

            If Not String.IsNullOrEmpty(value1) Then

                Dim nilai As Decimal = Decimal.Parse(value1)
                Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

                DataGridView1.CurrentRow.Cells(0).Value = formattedValue
            End If
        End If



    End Sub

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter

        If DataGridView1.CurrentCell.ColumnIndex = 0 Then

            Dim asdada As String = DataGridView1.CurrentCell.Value

            If asdada = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = asdada.Replace(",", "") ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            DataGridView1.CurrentCell.Value = nilai
        End If
    End Sub

    Private Sub TesGit()
        MessageBox.Show("Tes Git 1")
        MessageBox.Show("Tes Git from other device")
        MessageBox.Show("Tes Git from other device 2")
    End Sub

End Class

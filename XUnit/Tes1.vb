Imports Xunit

Public Class Tes1

    Private ReadOnly TesUnit As New TesUnit


    <Fact>
    Public Sub Tes1()

        'arrange (Initial Data)
        Dim a As Integer = 2
        Dim b As Integer = 3

        'act (Action)
        Dim result As String = TesUnit.Tambah(a, b)

        'assert
        Assert.Equal("5", result)

    End Sub








End Class

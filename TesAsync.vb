Imports System.Threading
Imports System.Web.Configuration

Public Class TesAsync

    Dim prosesAsync As Boolean = False

    Private Sub TesAsync_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "-"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Label1.Text = "-"

        BuatTeh()

    End Sub


    Private Async Sub BuatTeh()

        If prosesAsync Then
            Debug.WriteLine("Teh Sedang Dibuat Harap Tunggu")
            Label1.Text = "Teh Sedang Dibuat Harap Tunggu"
            Exit Sub
        End If

        Label1.Text = "Sedang Buat Teh"

        Dim AirPanas = MemanaskanAir()

        prosesAsync = True

        Debug.WriteLine("Menyiapkan Cangkir")

        Dim AirSudahPanas = Await AirPanas

        Debug.WriteLine($"Memasukan Air Panas kedalam Cangkir")

        Debug.WriteLine("Menyeduh Teh")

        Debug.WriteLine("Teh Sudah Siap")

        Label1.Text = "Tes Siap"

        prosesAsync = False

    End Sub


    Private Async Function MemanaskanAir() As Task(Of String)

        Debug.WriteLine("Menyiapkan Panci")

        Debug.WriteLine("Menghidupkan Kompor")

        Debug.WriteLine("Menunggu Air Panas")

        Await Task.Delay(5000)

        Debug.WriteLine("Air Sudah Panas")

        Return "Air Panas"


    End Function


End Class
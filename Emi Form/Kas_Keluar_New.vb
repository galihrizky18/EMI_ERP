Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Kas_Keluar_New

    Private Sub Kas_Keluar_New_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ListView1.Columns.Add("Kode Acc", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Perkiraan", 200) '1
        ListView1.Columns.Add("Keterangan", 200) '2
        ListView1.Columns.Add("Debit", 120, HorizontalAlignment.Right) '3
        ListView1.Columns.Add("Kredit", 120, HorizontalAlignment.Right) '4
        ListView1.Columns.Add("Pakai Budget", 0, HorizontalAlignment.Right) '5
        ListView1.Columns.Add("Lama Budget Harian", 0, HorizontalAlignment.Right) '6
        ListView1.Columns.Add("Lama Budget Bulanan", 0, HorizontalAlignment.Right) '7
        ListView1.Columns.Add("Budget Harian", 0, HorizontalAlignment.Right) '8
        ListView1.Columns.Add("Budget Bulanan", 0, HorizontalAlignment.Right) '9
        ListView1.Columns.Add("Kena Harian", 0, HorizontalAlignment.Right) '10
        ListView1.Columns.Add("Kena Bulanan", 0, HorizontalAlignment.Right) '11
        ListView1.Columns.Add("Terpakai Harian", 0, HorizontalAlignment.Right) '12
        ListView1.Columns.Add("Terpakai Bulanan", 0, HorizontalAlignment.Right) '13
        ListView1.View = View.Details

        'ListView2.Columns.Add("Kode Account", 150, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Keterangan", 400)
        'ListView2.Columns.Add("Posisi", 130, HorizontalAlignment.Center)
        'ListView2.View = View.Details

        'ComboBox2.Items.Clear()
        'ComboBox2.Items.Add("Kas Masuk")
        'ComboBox2.Items.Add("Kas Keluar")
        'ComboBox2.Items.Add("Bukti Bank Masuk")
        'ComboBox2.Items.Add("Bukti Bank Keluar")
        'ComboBox2.Items.Add("Jurnal")
        'ComboBox2.SelectedIndex = 1

        'ComboBox1.Items.Clear()
        'ComboBox1.Items.Add("Debit")
        'ComboBox1.Items.Add("Kredit")
        'ComboBox1.SelectedIndex = 0

        'ComboBox3.Items.Clear()
        'ComboBox3.Items.Add("Kode Account")
        'ComboBox3.Items.Add("Keterangan")
    End Sub
End Class
Public Class TesLoading


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Start_Loading(Me)


        For i As Integer = 1 To 10
            ' Proses yang ingin dijalankan
            System.Threading.Thread.Sleep(20) ' Memberikan delay
            'Application.DoEvents() ' Membuat UI tetap responsif
            'Application.DoEvents()
        Next

        End_Loading(Me)

    End Sub


    Private Sub DisableControlsLoading(targetForm As Form)
        ' Nonaktifkan seluruh kontrol kecuali lblLoading
        For Each ctrl As Control In targetForm.Controls
            ' Cek jika kontrol bukan label dengan nama "lblLoading"
            If Not (TypeOf ctrl Is Label AndAlso ctrl.Name = "lblLoading") Then
                ctrl.Enabled = False
            End If
        Next

        ' Mengubah kursor menjadi wait
        targetForm.Cursor = Cursors.WaitCursor
    End Sub

    Private Sub End_Loading(targetForm As Form)

        lblLoading.Dispose()

        ' Aktifkan kembali seluruh kontrol
        For Each ctrl As Control In targetForm.Controls
            ctrl.Enabled = True
        Next

        ' Kembalikan cursor ke normal
        targetForm.Cursor = Cursors.Default

        ' Update UI setelah proses selesai
        Application.DoEvents()
    End Sub

    Private Sub Start_Loading(targetForm As Form)
        ' Membuat label loading
        lblLoading = New Label()

        ' Properti dasar
        lblLoading.Name = "lblLoading"
        lblLoading.Text = "Loading..."
        lblLoading.Font = New Font("Arial", 14, FontStyle.Bold)

        ' Mengatur ukuran label secara manual
        lblLoading.Size = New Size(200, 40)

        ' Styling label
        lblLoading.BackColor = Color.White ' Warna background
        lblLoading.ForeColor = Color.Blue ' Warna text
        lblLoading.BorderStyle = BorderStyle.FixedSingle ' Menambahkan border
        lblLoading.TextAlign = ContentAlignment.MiddleCenter ' Text di tengah label

        ' Menambahkan label ke target form
        targetForm.Controls.Add(lblLoading)

        ' Posisi di tengah
        lblLoading.Left = (targetForm.ClientSize.Width - lblLoading.Width) / 2
        lblLoading.Top = (targetForm.ClientSize.Height - lblLoading.Height) / 2

        ' Membuat label tetap di tengah saat form diresize
        AddHandler targetForm.Resize, Sub()
                                          lblLoading.Left = (targetForm.ClientSize.Width - lblLoading.Width) / 2
                                          lblLoading.Top = (targetForm.ClientSize.Height - lblLoading.Height) / 2
                                      End Sub

        lblLoading.BringToFront()

        ' Update UI setelah proses selesai
        Application.DoEvents()

        ' Memanggil fungsi DisableControlsLoading pada form yang bersangkutan
        DisableControlsLoading(targetForm)
    End Sub

    Private Sub TesLoading_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TesAsync.Show()
        Exit Sub
    End Sub
End Class
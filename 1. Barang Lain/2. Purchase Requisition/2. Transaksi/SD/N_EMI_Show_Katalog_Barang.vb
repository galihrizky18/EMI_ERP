Public Class N_EMI_Show_Katalog_Barang

    Public path As String
    Public blob As String
    Public kode_barang As String
    Public nama As String

    Private Sub Show_Katalog_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = kode_barang
        Label2.Text = nama

        Dim container As String = "katalog-barang-operasional"

        Try
            OpenConn()

            Dim blobnamePath As String

            Dim url As String = ""


            SQL = "select nama from barang_lain where kode_barang = '" & kode_barang & "' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Label2.Text = Dr("nama")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Kode barang tidak tersedia!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.Close()
                    Exit Sub
                End If
            End Using


            SQL = "select top(1) a.kode_barang,a.blob_storage  From N_EMI_Katalog_Barang_Lain a where a.kode_barang = '" & kode_barang & "' "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "

            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        blobnamePath = Dr("blob_storage")

            '        url = AzureHelper_EMI.DownloadFromAzure(container, blobnamePath)
            '    End If
            'End Using


            'If url = "Gagal" OrElse String.IsNullOrWhiteSpace(url) Then
            '    url = "https://it.evomanufacturingindonesia.id/public/operasionals/gambar_tidak_tersedia.png"
            'End If

            Dim result As (Success As Boolean, Message As String, Url As String)

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    blobnamePath = Dr("blob_storage")

                    result = AzureHelper_EMI.DownloadFromAzure(
            container,
            blobnamePath
        )
                End If
            End Using

            If Not result.Success OrElse String.IsNullOrWhiteSpace(result.Url) Then
                url = "https://it.evomanufacturingindonesia.id/public/operasionals/gambar_tidak_tersedia.png"
            Else
                url = result.Url
            End If

            PictureBox1.SizeMode = PictureBoxSizeMode.Zoom


            PictureBox1.BorderStyle = BorderStyle.FixedSingle

            PictureBox1.Load(url)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub
End Class
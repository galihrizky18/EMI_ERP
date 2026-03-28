Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports Google.Apis.Auth.OAuth2
Imports Google.Cloud.Storage.V1

Public Class GCSHelper
    ' Path ke file JSON credential di folder debug
    Private Shared credentialPath As String = Application.StartupPath & "\majestic-legend-464212-g0-c0fa26d25450.json"

    Private Shared signer As UrlSigner

    ' inisialisasi credential sekali saja
    Shared Sub New()

        Try

            Dim jsonBytes As Byte() = My.Resources.service_account_credentials
            Dim jsonKey As String = Encoding.UTF8.GetString(jsonBytes)

            Dim credential As GoogleCredential = GoogleCredential.FromJson(jsonKey)

            signer = UrlSigner.FromCredential(credential)

        Catch ex As Exception

            MessageBox.Show("Gagal load credential: " & ex.Message)

        End Try

    End Sub

    'Shared Sub New()
    '    If Not File.Exists(credentialPath) Then
    '        MessageBox.Show("File credential tidak ditemukan: " & credentialPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End If

    '    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath)
    'End Sub


    Public Shared Function GenerateSignedUrl(bucket As String, objectName As String,
                                             Optional minutes As Integer = 5) As String

        Return signer.Sign(
            bucket,
            objectName,
            TimeSpan.FromMinutes(minutes),
            HttpMethod.Get
        )

    End Function


    ' ambil file sebagai byte
    Public Shared Function GetFileBytes(bucket As String, objectName As String,
                                        Optional minutes As Integer = 5) As Byte()

        Dim url As String = GenerateSignedUrl(bucket, objectName, minutes)

        Using wc As New WebClient()
            Return wc.DownloadData(url)
        End Using

    End Function


    ' ambil sebagai image
    Public Shared Function GetImage(bucket As String, objectName As String,
                                    Optional minutes As Integer = 5) As Image

        Dim bytes As Byte() = GetFileBytes(bucket, objectName, minutes)

        Using ms As New MemoryStream(bytes)
            Return Image.FromStream(ms)
        End Using

    End Function


    Public Shared Function UploadFile(filePath As String, bucketName As String, objectName As String) As Boolean
        Try
            If Not File.Exists(filePath) Then
                MessageBox.Show("File yang mau di-upload tidak ditemukan: " & filePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Dim storage = StorageClient.Create()

            Using f = File.OpenRead(filePath)
                storage.UploadObject(bucketName, objectName, Nothing, f)
            End Using

            '  MessageBox.Show("Upload berhasil ke: " & bucketName & "/" & objectName, "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show("Upload gagal: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Shared Function DownloadFile(bucketName As String, objectName As String, destinationPath As String) As Boolean
        Try
            Dim storage = StorageClient.Create()

            ' Pastikan folder tujuan ada
            Dim folderPath As String = Path.GetDirectoryName(destinationPath)
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            Using outputFile = File.OpenWrite(destinationPath)
                storage.DownloadObject(bucketName, objectName, outputFile)
            End Using

            '   MessageBox.Show("Download berhasil ke: " & destinationPath, "Download", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show("Gagal download: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
End Class

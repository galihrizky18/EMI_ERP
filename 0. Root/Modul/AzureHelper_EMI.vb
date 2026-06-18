Imports System.IO
Imports System.Text.RegularExpressions
Imports Azure
Imports Azure.Storage
Imports Azure.Storage.Blobs
Imports Azure.Storage.Sas

Public Class AzureHelper_EMI

	Public Shared Function UploadToAzure(
	containerName As String,
	blobName As String,
	stream As Stream
) As (Success As Boolean, Message As String, Url As String)

		Try
			Dim blobClient As New BlobClient(
			ConnectionStringAzure,
			containerName,
			blobName
		)

			blobClient.Upload(stream, overwrite:=True)

			Return (True, "", blobClient.Uri.ToString())
		Catch ex As RequestFailedException
			Dim userMessage As String = "Terjadi kesalahan saat memproses file. "

			If ex.Message.Contains("ContainerNotFound") Then
				userMessage = "Folder penyimpanan tidak ditemukan."
			ElseIf ex.Message.Contains("BlobNotFound") Then
				userMessage = "File tidak ditemukan di storage."
			ElseIf ex.Message.Contains("AuthorizationFailure") Then
				userMessage = "Akses ke storage ditolak."
			End If

			Return (False, userMessage, "")
		Catch ex As Exception
			Return (False, ex.Message, "")
		End Try

	End Function

	Public Shared Function DeleteFromAzure(
	containerName As String,
	blobName As String
) As (Success As Boolean, Message As String)

		Try
			Dim blobClient As New BlobClient(
			ConnectionStringAzure,
			containerName,
			blobName
		)

			Dim deleted = blobClient.DeleteIfExists()

			If deleted Then
				Return (True, "")
			Else
				Return (False, "File tidak ditemukan di Azure")
			End If
		Catch ex As RequestFailedException
			Dim userMessage As String = "Gagal menghapus file."

			Select Case ex.ErrorCode
				Case "ContainerNotFound"
					userMessage = "Folder penyimpanan tidak ditemukan."
				Case "BlobNotFound"
					userMessage = "File tidak ditemukan di penyimpanan."
				Case "AuthorizationFailure"
					userMessage = "Tidak memiliki izin untuk menghapus file."
				Case Else
					userMessage = "Terjadi kesalahan pada penyimpanan file."
			End Select

			' log internal (optional tapi disarankan)
			Debug.WriteLine(ex.ToString())

			Return (False, userMessage)
		Catch ex As Exception
			Return (False, ex.Message)
		End Try

	End Function

	Public Shared Function GenerateSasUrl(containerName As String, blobName As String) As String
		Try
			Dim accountName As String = storage_name ' ganti sesuai storage
			Dim accountKey As String = keyAzure     ' ganti sesuai key Azure

			' Buat credential
			Dim credential As New StorageSharedKeyCredential(accountName, accountKey)

			' Encode nama blob
			Dim encodedBlobName = Uri.EscapeDataString(blobName)
			Dim blobUri = New Uri($"https://{accountName}.blob.core.windows.net/{containerName}/{encodedBlobName}")

			' Bikin SAS builder
			Dim sasBuilder As New BlobSasBuilder With {
			.BlobContainerName = containerName,
			.BlobName = blobName,
			.Resource = "b",
			.StartsOn = DateTimeOffset.UtcNow.AddMinutes(-1),
			.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5)
		}

			sasBuilder.SetPermissions(BlobSasPermissions.Read)

			' Generate SAS token
			Dim sasToken = sasBuilder.ToSasQueryParameters(credential).ToString()

			' Gabung URL + SAS
			Dim finalUrl = blobUri.ToString() & "?" & sasToken

			Return finalUrl
		Catch ex As Exception
			Console.WriteLine("❌ Generate SAS error: " & ex.Message)
			Return "Gagal"
		End Try
	End Function

	Public Shared Function DownloadFromAzure(
	containerName As String,
	blobName As String
) As (Success As Boolean, Message As String, Url As String)

		Try
			' validasi awal
			If String.IsNullOrWhiteSpace(blobName) Then
				Return (False, "Nama file kosong", "")
			End If

			Dim accountName As String = storage_name
			Dim accountKey As String = keyAzure

			Dim credential As New StorageSharedKeyCredential(accountName, accountKey)
			Dim encodedBlobName = Uri.EscapeDataString(blobName)

			Dim blobUri As New Uri(
			$"https://{accountName}.blob.core.windows.net/{containerName}/{encodedBlobName}"
		)

			Dim blobClient As New BlobClient(blobUri, credential)

			' ===== CEK ADA / TIDAK =====
			Try
				blobClient.GetProperties()
			Catch ex As Azure.RequestFailedException
				If ex.Status = 404 Then
					Return (False, "File tidak ditemukan di Azure", "")
				Else
					Return (False, $"Azure error {ex.Status}: {ex.ErrorCode}", "")
				End If
			End Try

			' ===== BUAT SAS =====
			Dim sasBuilder As New BlobSasBuilder With {
			.BlobContainerName = containerName,
			.BlobName = blobName,
			.Resource = "b",
			.StartsOn = DateTimeOffset.UtcNow.AddMinutes(-1),
			.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5)
		}

			sasBuilder.SetPermissions(BlobSasPermissions.Read)

			Dim sasToken = sasBuilder.ToSasQueryParameters(credential).ToString()
			Dim finalUrl = blobUri.ToString() & "?" & sasToken

			Return (True, "", finalUrl)
		Catch ex As Exception
			Return (False, ex.Message, "")
		End Try

	End Function

	Public Shared Function ExtractDriveFileId(ByVal url As String) As String
		Dim pattern As String = "[-\w]{10,}"
		Dim match = Regex.Match(url, pattern)
		If match.Success Then
			Return match.Value
		Else
			Return ""
		End If
	End Function

End Class
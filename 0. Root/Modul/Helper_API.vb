Imports System.IO
Imports System.Net
Imports System.Text

Public Class Helper_API

	Public Shared Function CallAPI(url As String, method As String, token As String, Optional payload As String = "") As String
		Try
			Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
			request.Method = method.ToUpper()
			request.ContentType = "application/json"

			If Not String.IsNullOrEmpty(token) Then
				request.Headers.Add("Authorization", token)
			End If

			' Kalau ada payload (POST/PUT)
			If (method.ToUpper() = "POST" OrElse method.ToUpper() = "PUT") AndAlso Not String.IsNullOrEmpty(payload) Then
				Dim byteArray As Byte() = Encoding.UTF8.GetBytes(payload)
				request.ContentLength = byteArray.Length
				Using dataStream As Stream = request.GetRequestStream()
					dataStream.Write(byteArray, 0, byteArray.Length)
				End Using
			End If

			' Ambil response
			Using response As WebResponse = request.GetResponse()
				Using dataStream As Stream = response.GetResponseStream()
					Using reader As New StreamReader(dataStream)
						Return reader.ReadToEnd()
					End Using
				End Using
			End Using
		Catch ex As WebException
			If ex.Response IsNot Nothing Then
				Using reader As New StreamReader(ex.Response.GetResponseStream())
					Return "API Error: " & reader.ReadToEnd()
				End Using
			Else
				Return "Error: " & ex.Message
			End If
		End Try
	End Function

	Public Shared Function CallAPIFile(
		url As String,
		method As String,
		Optional payload As Object = Nothing,
		Optional headers As Dictionary(Of String, String) = Nothing,
		Optional contentType As String = "application/json"
	) As MemoryStream
		Try
			Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)

			request.Method = method.ToUpper()
			request.ContentType = contentType
			request.Accept = "*/*"
			request.UserAgent = "Mozilla/5.0"

			' Header tambahan
			If headers IsNot Nothing Then
				For Each item In headers
					request.Headers.Add(item.Key, item.Value)
				Next
			End If

			' Body request
			If payload IsNot Nothing Then
				Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
				Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(json)

				request.ContentLength = bytes.Length

				Using stream = request.GetRequestStream()
					stream.Write(bytes, 0, bytes.Length)
				End Using
			End If

			' Response file
			Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

			Dim ms As New MemoryStream()

			Using responseStream = response.GetResponseStream()
				responseStream.CopyTo(ms)
			End Using

			ms.Position = 0

			Return ms
		Catch ex As Exception
			Return Nothing
		End Try
	End Function

End Class
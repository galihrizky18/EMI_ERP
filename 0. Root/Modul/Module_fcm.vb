Imports System.IO
Imports System.Net
Imports System.Text



Module Module_FCM
    ' Path ke file JSON service account Anda
    Private Const serviceAccountJsonPath As String = "path-to-your-service-account-file.json"

    ' Google OAuth 2.0 token URL
    Private Const googleOauthTokenUrl As String = "https://oauth2.googleapis.com/token"

    ' Scope untuk Firebase Cloud Messaging
    Private Const fcmScope As String = "https://www.googleapis.com/auth/firebase.messaging"

    Sub Main()
        Dim token = GetAccessToken().Result
        If Not String.IsNullOrEmpty(token) Then
            Console.WriteLine("Access Token: " & token)

            'SendNotification(token)
        Else
            Console.WriteLine("Failed to get access token.")
        End If
    End Sub

    ' Mendapatkan access token menggunakan JWT dan OAuth 2.0 Token URL
    Async Function GetAccessToken() As Task(Of String)
        '        Try

        '            ' Tentukan path ke file JSON Service Account
        '            Dim projectDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        '            Dim jsonFilePath As String = Path.Combine(projectDirectory, "fcmmessage-586ff-firebase-adminsdk-5bo1c-17e31e4959.json")

        '            ' Membaca service account JSON
        '            ' Dim serviceAccountJson = File.ReadAllText(serviceAccountJsonPath)
        '            Dim serviceAccount As Dictionary(Of String, String) = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(File.ReadAllText(jsonFilePath))

        '            ' Membuat JWT Header
        '            Dim header As New Dictionary(Of String, String)
        '            header.Add("alg", "RS256")
        '            header.Add("typ", "JWT")

        '            ' Membuat JWT Claim Set
        '            Dim iat = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        '            Dim exp = iat + 3600 ' Token berlaku selama 1 jam
        '            Dim claimSet As New Dictionary(Of String, Object)
        '            claimSet.Add("iss", serviceAccount("client_email"))
        '            claimSet.Add("scope", fcmScope)
        '            claimSet.Add("aud", googleOauthTokenUrl)
        '            claimSet.Add("iat", iat)
        '            claimSet.Add("exp", exp)


        '            ' Base64 encoding JWT Header dan Claim Set
        '            Dim jwtHeader = Base64UrlEncode(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header)))
        '            Dim jwtClaimSet = Base64UrlEncode(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(claimSet)))

        '            ' JWT tanpa tanda tangan
        '            Dim jwtUnsigned = $"{jwtHeader}.{jwtClaimSet}"

        '            ' Mengimpor kunci privat dari service account menggunakan BouncyCastle
        '            Dim privateKeyPem As String = serviceAccount("private_key") ' Pastikan private_key dalam format PEM
        '            Dim reader As New StringReader(privateKeyPem)
        '            Dim pemReader As New PemReader(reader)
        '            Dim rsaPrivateKey = CType(pemReader.ReadObject(), RsaPrivateCrtKeyParameters) ' Cast langsung ke RsaPrivateCrtKeyParameters

        '            ' Konversi BouncyCastle RSA key ke .NET RSA
        '            Dim rsa As RSA = DotNetUtilities.ToRSA(rsaPrivateKey)

        '            ' Menandatangani JWT dengan kunci privat
        '            Dim signature = rsa.SignData(Encoding.UTF8.GetBytes(jwtUnsigned), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)
        '            Dim jwtSigned = $"{jwtUnsigned}.{Base64UrlEncode(signature)}"

        '            ' Mempersiapkan request body
        '            Dim requestBody = New Dictionary(Of String, String) From {
        '            {"grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer"},
        '            {"assertion", jwtSigned}
        '}

        '            ' Mengirim request ke OAuth 2.0 token endpoint
        '            Using httpClient As New HttpClient()
        '                Dim content = New FormUrlEncodedContent(requestBody)
        '                Dim response = Await httpClient.PostAsync(googleOauthTokenUrl, content).ConfigureAwait(False)
        '                Dim responseString = Await response.Content.ReadAsStringAsync()

        '                Console.WriteLine("Mengirim request ke Google OAuth... " & responseString)
        '                '  MessageBox.Show(responseString)
        '                Console.WriteLine("Request terkirim, menunggu response...")

        '                If response.IsSuccessStatusCode Then
        '                    Dim tokenResponse = JsonConvert.DeserializeObject(Of TokenResponse)(responseString)
        '                    Return tokenResponse.access_token
        '                Else
        '                    Console.WriteLine("Error response from OAuth server: " & responseString)
        '                    Return Nothing
        '                End If
        '            End Using
        '        Catch ex As Exception
        '            Console.WriteLine("Error: " & ex.Message)
        '            MessageBox.Show(ex.Message)
        '            Return Nothing
        '        End Try
    End Function

    ' Base64 URL Encode (sesuai RFC 7515)
    Private Function Base64UrlEncode(input As Byte()) As String
        Return Convert.ToBase64String(input).TrimEnd("="c).Replace("+"c, "-"c).Replace("/"c, "_"c)
    End Function

    ' Mengirim notifikasi ke FCM
    Sub SendNotification(token As String, title As String, pesan As String)
        ' FCM URL (replace with your project ID)
        Dim fcmUrl As String = "https://fcm.googleapis.com/v1/projects/emi-erp-468cd/messages:send"

        ' Membuat request ke FCM
        Dim request As HttpWebRequest = CType(WebRequest.Create(fcmUrl), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"
        request.Headers.Add($"Authorization: Bearer {token}")

        Dim tokenAndroid As String = "evhRo0FOTduNwSqhCO5Knb:APA91bEwg9gRd-s_zv-Xr3v8Qtt-MsHGQDv2EsZwYnjS1Scq9UzRnDEZ65rJ07iCl1eN0hdRzCoWedCNovTWVhY_6NSy-lVmodxx1IxzaN4U75WHebpgT-4"

        ' Membuat payload JSON
        Dim payload As String = "{
            ""message"": {
                ""token"": """ & tokenAndroid & """,
                ""notification"": {
                    ""title"": """ & title & """,
                    ""body"": """ & pesan & """
                }
            }
        }"

        ' Mengirimkan payload ke FCM
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(payload)
        request.ContentLength = byteArray.Length
        Using dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
        End Using

        ' Menerima response dari FCM
        Try
            Dim response As WebResponse = request.GetResponse()
            Using dataStream As Stream = response.GetResponseStream()
                Using reader As New StreamReader(dataStream)
                    Dim responseFromServer As String = reader.ReadToEnd()
                    Console.WriteLine(responseFromServer)
                End Using
            End Using
        Catch ex As WebException
            Using stream As Stream = ex.Response.GetResponseStream()
                Using reader As New StreamReader(stream)
                    Dim errorMessage As String = reader.ReadToEnd()
                    Console.WriteLine("Error: " & errorMessage)
                End Using
            End Using
        End Try
    End Sub

    ' Kelas untuk mendeserialisasi service account JSON
    Class ServiceAccount
        Public Property private_key As String
        Public Property client_email As String
    End Class

    ' Kelas untuk mendeserialisasi token response
    Class TokenResponse
        Public Property access_token As String
        Public Property expires_in As Integer
        Public Property token_type As String
    End Class
End Module

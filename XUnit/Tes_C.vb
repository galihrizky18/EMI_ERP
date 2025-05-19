Public Class Tes_C
    Private Sub Tampil_Kamera()
        StreamPlayerControl1.Show()
        StreamPlayerControl2.Show()

        Try

            'If StreamPlayerControl1.IsPlaying = True Then
            '    StreamPlayerControl1.Stop()
            '    StreamPlayerControl2.Stop()
            'End If

            'SQL = "select User_IPCAM, Password_IPCAM, IPPORT_CAM from Emi_CAM"
            'Using dr = OpenTrans(SQL)
            '    Dim stream As Integer = 1
            '    Do While dr.Read
            '        Dim controlName As String = "StreamPlayerControl" & stream
            '        Dim control As Object = Me.GetType().GetField(controlName, Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(Me)

            '        If control IsNot Nothing Then
            '            control.StartPlay((New Uri("rtsp://" & dr("User_IPCAM") & ":" & dr("Password_IPCAM") & "@" & dr("IPPORT_CAM") & "/Streaming/channels/102/")))
            '            stream += 1
            '        End If

            '    Loop
            'End Using

            Dim user1 As String = "" : Dim pass1 As String = "" : Dim ipaddr1 As String = ""
            Dim user2 As String = "" : Dim pass2 As String = "" : Dim ipaddr2 As String = ""

            Try
                OpenConn()
                SQL = "select UserName, Password, IP_Address, CAM_Number from Emi_CAM"
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        If dr("CAM_Number") = "CAM 1" Then
                            user1 = dr("UserName")
                            pass1 = dr("Password")
                            ipaddr1 = dr("IP_Address")

                        ElseIf dr("CAM_Number") = "CAM 2" Then
                            user2 = dr("UserName")
                            pass2 = dr("Password")
                            ipaddr2 = dr("IP_Address")
                        End If
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
            End Try

            StreamPlayerControl1.StartPlay((New Uri("rtsp://" & user1 & ":" & pass1 & "@" & ipaddr1 & "/Streaming/channels/102/")))
            StreamPlayerControl2.StartPlay((New Uri("rtsp://" & user2 & ":" & pass2 & "@" & ipaddr2 & "/Streaming/channels/102/")))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Tes_C_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tampil_Kamera()
    End Sub
End Class
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Master_CAM

    Dim lv_KodePerusahaan, lv_UserName, lv_Password, lv_IP_Address, lv_CamNumber As String
    Dim KodePerusahaan As String = "001"

    Dim itemKodePerusahaan As Integer = 0
    Dim itemUsername As Integer = 1
    Dim itemPassword As Integer = 2
    Dim itemIPAddress As Integer = 3
    Dim itemCamNumber As Integer = 4

    Private Sub Master_CAM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Btn_Simpan.Tag = "&SIMPAN"
        kosong()

        Dim camSource As New List(Of String) From {"CAM 1", "CAM 2"}

        For Each item In camSource
            Cb_CamNumber.Items.Add(item)
        Next

    End Sub

    Private Sub kosong()
        Lv_Cam.Clear()
        Txt_UserIP.Text = String.Empty
        Txt_PasswordIP.Text = String.Empty
        Txt_IPPort.Text = String.Empty
        Cb_CamNumber.SelectedItem = -1
        Cb_CamNumber.Text = String.Empty

        initialListView()
        loadCamDB()
    End Sub

    Private Sub initialListView()
        Lv_Cam.Columns.Add("Kode Perusahaan", 0, HorizontalAlignment.Left)
        Lv_Cam.Columns.Add("Username", 0, HorizontalAlignment.Left)
        Lv_Cam.Columns.Add("Password", 0, HorizontalAlignment.Left)
        Lv_Cam.Columns.Add("IP_Address", 200, HorizontalAlignment.Left)
        Lv_Cam.Columns.Add("CAM_Number", 200, HorizontalAlignment.Left)
        Lv_Cam.View = View.Details
    End Sub

    Private Sub Get_Isi_ListView(ByVal noIndex As Integer)
        lv_KodePerusahaan = Lv_Cam.Items(noIndex).SubItems(itemKodePerusahaan).Text
        lv_UserName = Lv_Cam.Items(noIndex).SubItems(itemUsername).Text
        lv_Password = Lv_Cam.Items(noIndex).SubItems(itemPassword).Text
        lv_IP_Address = Lv_Cam.Items(noIndex).SubItems(itemIPAddress).Text
        lv_CamNumber = Lv_Cam.Items(noIndex).SubItems(itemCamNumber).Text
    End Sub

    Private Sub loadCamDB()
        Try
            OpenConn()

            SQL = "select Kode_Perusahaan, UserName, Password, IP_Address, CAM_Number from EMI_CAM"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_Cam.Items.Add(dr("Kode_Perusahaan"))
                    Lvw.SubItems.Add(dr("UserName"))
                    Lvw.SubItems.Add(dr("Password"))
                    Lvw.SubItems.Add(dr("IP_Address"))
                    Lvw.SubItems.Add(dr("CAM_Number"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Try
            OpenConn()

            Dim username = Txt_UserIP.Text.Trim
            Dim password = Txt_PasswordIP.Text.Trim
            Dim ipaddress = Txt_IPPort.Text.Trim
            Dim camNumber = Cb_CamNumber.SelectedItem.ToString

            If Btn_Simpan.Tag = "&SIMPAN" Then
                If Not Cb_CamNumber.SelectedIndex = -1 Then
                    If Not String.IsNullOrWhiteSpace(username) AndAlso Not String.IsNullOrWhiteSpace(password) AndAlso Not String.IsNullOrWhiteSpace(ipaddress) Then

                        SQL = "insert into EMI_CAM (Kode_Perusahaan, UserName, Password, IP_Address, CAM_Number) values "
                        SQL = SQL & "('" & KodePerusahaan & "', '" & username & "', '" & password & "', '" & ipaddress & "', '" & camNumber & "')"

                        ExecuteTrans(SQL)

                    End If
                End If
            End If

            If Btn_Simpan.Tag = "&UPDATE" Then
                If Not String.IsNullOrWhiteSpace(username) AndAlso Not String.IsNullOrWhiteSpace(password) AndAlso Not String.IsNullOrWhiteSpace(ipaddress) Then

                    SQL = "update EMI_CAM set Kode_Perusahaan = '" & KodePerusahaan & "', UserName='" & username & "', "
                    SQL = SQL & "Password='" & password & "', IP_Address='" & ipaddress & "', CAM_Number='" & camNumber & "' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and UserName='" & lv_UserName & "' and "
                    SQL = SQL & "Password='" & lv_Password & "' and IP_Address='" & lv_IP_Address & "' and CAM_Number='" & lv_CamNumber & "'"

                    ExecuteTrans(SQL)

                End If
            End If

            CloseConn()
            kosong()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click
        Try
            OpenConn()

            Dim userIP = Txt_UserIP.Text.Trim
            Dim passwordIP = Txt_PasswordIP.Text.Trim
            Dim IPPort = Txt_IPPort.Text.Trim


            If Not String.IsNullOrWhiteSpace(userIP) AndAlso Not String.IsNullOrWhiteSpace(passwordIP) AndAlso Not String.IsNullOrWhiteSpace(IPPort) Then

                SQL = "delete from EMI_CAM where UserName='" & userIP & "' and Password='" & passwordIP & "' "
                SQL = SQL & "and IP_Address='" & IPPort & "' and CAM_Number='" & lv_CamNumber & "'"
                ExecuteTrans(SQL)

            End If

            CloseConn()
            kosong()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Cam_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Cam.DoubleClick

        Get_Isi_ListView(Lv_Cam.FocusedItem.Index)

        Cb_CamNumber.SelectedItem = lv_CamNumber
        Txt_UserIP.Text = lv_UserName
        Txt_PasswordIP.Text = lv_Password
        Txt_IPPort.Text = lv_IP_Address


        Btn_Simpan.Tag = "&UPDATE"

    End Sub

    Private Sub Txt_UserIP_TextChanged(sender As Object, e As EventArgs) Handles Txt_UserIP.TextChanged
        If Txt_UserIP.Text.Trim = "" Then
            Btn_Simpan.Tag = "&SIMPAN"
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub


End Class
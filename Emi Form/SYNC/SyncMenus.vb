Public Class SyncMenus

    Private Cn1 As SqlClient.SqlConnection
    Private Cmd1 As SqlClient.SqlCommand
    Private Da1 As SqlClient.SqlDataAdapter
    Private Dr1 As SqlClient.SqlDataReader
    Private Ds1 As DataSet
    Private SQL1 As String

    Private Cn2 As SqlClient.SqlConnection
    Private Cmd2 As SqlClient.SqlCommand
    Private Da2 As SqlClient.SqlDataAdapter
    Private Dr2 As SqlClient.SqlDataReader
    Private Ds2 As DataSet
    Private SQL2 As String

    Private CServer1, CDatabase1, CUserId1, CPassword1 As String
    Private CServer2, CDatabase2, CUserId2, CPassword2 As String

    Dim arr_DbAwal, arr_DbAkhir As New ArrayList
    Dim arrMainMenuId As New ArrayList

    Dim judulForm As String = "Transfer Table"

    Private Sub TransferTableDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()

    End Sub

    Private Sub Kosong()



        Cmb_Database_Awal.Items.Clear() : arr_DbAwal.Clear()
        Cmb_Database_Awal.Items.Add("Emi TM Demo") : arr_DbAwal.Add("emi_tm_demo")
        Cmb_Database_Awal.Items.Add("Graha Web") : arr_DbAwal.Add("grahaweb_tm")
        Cmb_Database_Awal.SelectedIndex = -1

        Cmb_DatabaseTujuan.Items.Clear() : arr_DbAkhir.Clear()
        Cmb_DatabaseTujuan.Items.Add("Emi TM Demo") : arr_DbAkhir.Add("emi_tm_demo")
        Cmb_DatabaseTujuan.Items.Add("Graha Web") : arr_DbAkhir.Add("grahaweb_tm")
        Cmb_DatabaseTujuan.SelectedIndex = -1

        Cmb_Menus.Items.Clear() : arrMainMenuId.Clear()
        Cmb_Menus.Text = ""

        Cmb_Menus.Enabled = False
        Btn_Transfer.Enabled = False

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Cmb_Database_Awal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Database_Awal.SelectedIndexChanged
        If Cmb_Database_Awal.SelectedIndex = -1 Then Exit Sub

        If Cmb_Database_Awal.SelectedIndex = Cmb_DatabaseTujuan.SelectedIndex Then
            MessageBox.Show("Database Awal tidak Boleh sama dengan Database Akhir", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Database_Awal.SelectedIndex = -1
            Cmb_Database_Awal.Focus()
            Exit Sub
        End If

        Select Case Cmb_Database_Awal.SelectedIndex
            Case 0
                CServer1 = "team311.dyndns.info"
                CDatabase1 = "emi_tm_demo"
                CUserId1 = "sqlserver"
                CPassword1 = "**H0L4H0L4hola**"

            Case 1
                CServer1 = "team311.dyndns.info"
                CDatabase1 = "grahaweb_tm"
                CUserId1 = "sqlserver"
                CPassword1 = "**H0L4H0L4hola**"

        End Select

    End Sub

    Private Sub Cmb_DatabaseTujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_DatabaseTujuan.SelectedIndexChanged
        If Cmb_DatabaseTujuan.SelectedIndex = -1 Then Exit Sub

        If Cmb_DatabaseTujuan.SelectedIndex = Cmb_Database_Awal.SelectedIndex Then
            MessageBox.Show("Database Akhir tidak Boleh sama dengan Database Awal", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_DatabaseTujuan.SelectedIndex = -1
            Cmb_DatabaseTujuan.Focus()
            Exit Sub
        End If

        Select Case Cmb_DatabaseTujuan.SelectedIndex
            Case 0
                CServer2 = "team311.dyndns.info"
                CDatabase2 = "emi_tm_demo"
                CUserId2 = "sqlserver"
                CPassword2 = "**H0L4H0L4hola**"

            Case 1
                CServer2 = "team311.dyndns.info"
                CDatabase2 = "grahaweb_tm"
                CUserId2 = "sqlserver"
                CPassword2 = "**H0L4H0L4hola**"

        End Select
    End Sub

    Private Sub Btn_Set_Click(sender As Object, e As EventArgs) Handles Btn_Set.Click

        If Cmb_Database_Awal.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Database Awal", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Database_Awal.Focus()
            Exit Sub
        ElseIf Cmb_DatabaseTujuan.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Database Akhir", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_DatabaseTujuan.Focus()
            Exit Sub
        End If

        Cmb_Menus.Enabled = True
        Btn_Transfer.Enabled = True

        Try
            OpenConn1()

            Cmb_Menus.Items.Clear() : arrMainMenuId.Clear()
            SQL = "select MainMenuID, TItle from MainMenu order by urut "
            Using Dr1 = OpenTrans1(SQL)
                Do While Dr1.Read
                    Cmb_Menus.Items.Add(Dr1("TItle")) : arrMainMenuId.Add(Dr1("MainMenuID"))
                Loop
            End Using

            CloseConn1()
        Catch ex As Exception
            CloseConn1()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Btn_Transfer_Click(sender As Object, e As EventArgs) Handles Btn_Transfer.Click

        If Cmb_Database_Awal.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Database Awal", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Database_Awal.Focus()
            Exit Sub
        ElseIf Cmb_DatabaseTujuan.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Database Akhir", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_DatabaseTujuan.Focus()
            Exit Sub
        End If

        If Cmb_Menus.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Menu Dahulu", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Menus.Focus()
            Exit Sub
        End If

        Dim tableAwal As String = Cmb_Menus.Text

        Try
            OpenConn1()
            OpenConn2()
            Cmd1.Transaction = Cn1.BeginTransaction
            Cmd2.Transaction = Cn2.BeginTransaction

            '===================================================
            '=     CEK APAKAH MENU SUDAH ADA DI DATABASE 2     =
            '===================================================
            SQL2 = "select top 1 * from MainMenu where MainMenuID = '" & arrMainMenuId(Cmb_Menus.SelectedIndex) & "' "
            Using Ds2 = BindingTrans2(SQL2)
                With Ds2.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        CloseTrans1()
                        CloseTrans2()
                        CloseConn1()
                        CloseConn2()
                        MessageBox.Show("Menu Sudah Ada")
                        Exit Sub

                    Else

                        Dim MainMenuId As String = ""
                        Dim MenuId As String = ""
                        Dim SubMenuId As String = ""
                        Dim SubMenuLv1Id As String = ""
                        Dim SubMenuLv2Id As String = ""
                        Dim SubMenuLv3Id As String = ""
                        '========================
                        '=     ADD MAINMENU     =
                        '========================
                        SQL1 = "select MainMenuID, ImagePath, TItle, urut from MainMenu where MainMenuID = '" & arrMainMenuId(Cmb_Menus.SelectedIndex) & "' "
                        Using Ds1 = BindingTrans1(SQL1)
                            If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count = 0

                                    MainMenuId = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("MainMenuID"))
                                    Dim ImagePath As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("ImagePath"))
                                    Dim title As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("TItle"))
                                    Dim urut As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("urut"))

                                    SQL2 = "insert into MainMenu (MainMenuID, ImagePath, TItle, urut) values "
                                    SQL2 = SQL2 & "(" & MainMenuId & ", " & ImagePath & ", " & title & ", " & urut & ")"
                                    ExecuteTrans2(SQL2)

                                Next
                            End If
                        End Using

                        '====================
                        '=     ADD MENU     =
                        '====================
                        If Not MainMenuId = "" Then
                            SQL1 = "select MenuID, MainMenuID, MenuName, MenuOrder, MenuParent from Menus where MainMenuID = " & MainMenuId & " "
                            Using Ds1 = BindingTrans1(SQL1)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count = 0

                                        MenuId = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("MenuID"))
                                        Dim MenuName As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("MenuName"))
                                        Dim MenuOrder As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("MenuOrder"))

                                        SQL2 = "insert into menus (MenuID, MainMenuID, MenuName, MenuOrder, MenuParent) values "
                                        SQL2 = SQL2 & "(" & MenuId & ", " & MainMenuId & ", " & MenuName & ", " & MenuOrder & ", NULL)"
                                        ExecuteTrans2(SQL2)

                                    Next
                                End If
                            End Using

                        Else
                            CloseTrans1()
                            CloseTrans2()
                            CloseConn1()
                            CloseConn2()
                            MessageBox.Show("Main Menu Tidak Ada")
                            Exit Sub
                        End If


                        '=======================
                        '=     ADD SUBMENU     =
                        '=======================
                        If Not MenuId = "" Then
                            SQL1 = "select SubMenuID, SubMenuName, MenuID, SubMenuOrder, form, Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3 from SubMenus where MenuID = " & MenuId & " "
                            Using Ds1 = BindingTrans1(SQL1)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count = 0

                                        SubMenuId = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuID"))
                                        Dim MenuName As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuName"))
                                        Dim MenuOrder As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuOrder"))
                                        Dim Form As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("form"))
                                        Dim Variabel As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel"))
                                        Dim Isi_Variabel As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel"))
                                        Dim Variabel2 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel2"))
                                        Dim Isi_Variabel2 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel2"))
                                        Dim Variabel3 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel3"))
                                        Dim Isi_Variabel3 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel3"))

                                        SQL2 = "insert into SubMenus (SubMenuID, SubMenuName, MenuID, SubMenuOrder, form, Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                        SQL2 = SQL2 & "(" & SubMenuId & ", " & MenuName & ", " & MenuId & ", " & MenuOrder & ", " & Form & ", " & Variabel & ", " & Isi_Variabel & ", "
                                        SQL2 = SQL2 & "" & Variabel2 & ", " & Isi_Variabel2 & ", " & Variabel3 & ", " & Isi_Variabel3 & ")"
                                        ExecuteTrans2(SQL2)

                                    Next
                                End If
                            End Using

                        Else
                            CloseTrans1()
                            CloseTrans2()
                            CloseConn1()
                            CloseConn2()
                            MessageBox.Show("Menu Tidak Ada")
                            Exit Sub
                        End If

                        '==========================
                        '=     ADD SUBMENULv1     =
                        '==========================
                        If Not SubMenuId = "" Then
                            SQL1 = "select SubMenuLv1ID, SubMenuID, SubMenuLv1Name, SubMenuLv1Order, form, Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3 from SubMenuLv1 where SubMenuID = " & SubMenuId & " "
                            Using Ds1 = BindingTrans1(SQL1)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count = 0

                                        SubMenuLv1Id = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuLv1ID"))
                                        Dim MenuName As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuLv1Name"))
                                        Dim MenuOrder As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuLv1Order"))
                                        Dim Form As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("form"))
                                        Dim Variabel As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel"))
                                        Dim Isi_Variabel As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel"))
                                        Dim Variabel2 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel2"))
                                        Dim Isi_Variabel2 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel2"))
                                        Dim Variabel3 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel3"))
                                        Dim Isi_Variabel3 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel3"))

                                        SQL2 = "insert into SubMenuLv1 (SubMenuLv1ID, SubMenuID, SubMenuLv1Name, SubMenuLv1Order, form, Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                        SQL2 = SQL2 & "(" & SubMenuLv1Id & ", " & SubMenuId & ", " & MenuName & ", " & MenuOrder & ", " & Form & ", " & Variabel & ", " & Isi_Variabel & ", "
                                        SQL2 = SQL2 & "" & Variabel2 & ", " & Isi_Variabel2 & ", " & Variabel3 & ", " & Isi_Variabel3 & ")"
                                        ExecuteTrans2(SQL2)

                                    Next
                                End If
                            End Using

                        Else
                            CloseTrans1()
                            CloseTrans2()
                            CloseConn1()
                            CloseConn2()
                            MessageBox.Show("SubMenu Tidak Ada")
                            Exit Sub
                        End If

                        '==========================
                        '=     ADD SUBMENULv2     =
                        '==========================
                        If Not SubMenuLv1Id = "" Then
                            SQL1 = "select SubMenuLv2ID, SubMenuLv1ID, SubMenuLv2Name, SubMenuLv2Order, form, Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3 from SubMenuLv2 where SubMenuLv1ID = " & SubMenuLv1Id & " "
                            Using Ds1 = BindingTrans1(SQL1)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count = 0

                                        SubMenuLv2Id = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuLv2ID"))
                                        Dim MenuName As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuLv2Name"))
                                        Dim MenuOrder As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuLv2Order"))
                                        Dim Form As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("form"))
                                        Dim Variabel As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel"))
                                        Dim Isi_Variabel As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel"))
                                        Dim Variabel2 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel2"))
                                        Dim Isi_Variabel2 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel2"))
                                        Dim Variabel3 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel3"))
                                        Dim Isi_Variabel3 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel3"))

                                        SQL2 = "insert into SubMenuLv2 (SubMenuLv2ID, SubMenuLv1ID, SubMenuLv2Name, SubMenuLv2Order, form, Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                        SQL2 = SQL2 & "(" & SubMenuLv2Id & ", " & SubMenuLv1Id & ", " & MenuName & ", " & MenuOrder & ", " & Form & ", " & Variabel & ", " & Isi_Variabel & ", "
                                        SQL2 = SQL2 & "" & Variabel2 & ", " & Isi_Variabel2 & ", " & Variabel3 & ", " & Isi_Variabel3 & ")"
                                        ExecuteTrans2(SQL2)

                                    Next
                                End If
                            End Using

                        Else
                            CloseTrans1()
                            CloseTrans2()
                            CloseConn1()
                            CloseConn2()
                            MessageBox.Show("SubMenuLv1 Tidak Ada")
                            Exit Sub
                        End If

                        '==========================
                        '=     ADD SUBMENULv3     =
                        '==========================
                        If Not SubMenuLv1Id = "" Then
                            SQL1 = "select SubMenuLv3ID, SubMenuLv2ID, SubMenuLv3Name, SubMenuLv3Order, form, Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3 from SubMenuLv3 where SubMenuLv2ID = " & SubMenuLv1Id & " "
                            Using Ds1 = BindingTrans1(SQL1)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count = 0

                                        SubMenuLv3Id = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuLv3ID"))
                                        Dim MenuName As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuLv3Name"))
                                        Dim MenuOrder As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("SubMenuLv3Order"))
                                        Dim Form As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("form"))
                                        Dim Variabel As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel"))
                                        Dim Isi_Variabel As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel"))
                                        Dim Variabel2 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel2"))
                                        Dim Isi_Variabel2 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel2"))
                                        Dim Variabel3 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Variabel3"))
                                        Dim Isi_Variabel3 As String = cekEmptyString(Ds1.Tables("MyTable").Rows(i).Item("Isi_Variabel3"))

                                        SQL2 = "insert into SubMenuLv3 (SubMenuLv3ID, SubMenuLv2ID, SubMenuLv3Name, SubMenuLv3Order, form, Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                        SQL2 = SQL2 & "(" & SubMenuLv3Id & ", " & SubMenuLv2Id & ", " & MenuName & ", " & MenuOrder & ", " & Form & ", " & Variabel & ", " & Isi_Variabel & ", "
                                        SQL2 = SQL2 & "" & Variabel2 & ", " & Isi_Variabel2 & ", " & Variabel3 & ", " & Isi_Variabel3 & ")"
                                        ExecuteTrans2(SQL2)

                                    Next
                                End If
                            End Using

                        Else
                            CloseTrans1()
                            CloseTrans2()
                            CloseConn1()
                            CloseConn2()
                            MessageBox.Show("SubMenuLv2 Tidak Ada")
                            Exit Sub
                        End If

                    End If
                End With
            End Using





            Cmd1.Transaction.Commit()
            Cmd2.Transaction.Commit()
            CloseTrans1()
            CloseTrans2()
            CloseConn1()
            CloseConn2()
            MessageBox.Show("Data Berhasil Disimpan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Catch ex As Exception
            CloseTrans1()
            CloseTrans2()
            CloseConn1()
            CloseConn2()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub







    Private Function cekEmptyString(ByVal str As String) As String

        If str Is Nothing OrElse str = "" Then
            Return "NULL"
        Else
            Return "'" & str & "'"
        End If

    End Function

    '=====================
    '=       MODUL       =
    '=====================
#Region "Modul"

#Region "Modul 1"

    Public Sub OpenConn1()
        If CServer1 = "" Or CDatabase1 = "" Or CUserId1 = "" Or CPassword1 = "" Then
            MessageBox.Show("Gagal Membuka Koneksi")
            Exit Sub
        End If

        General_Class.SetConnectionString(CServer1, CDatabase1, CUserId1, CPassword1)
        Cn1 = New SqlClient.SqlConnection
        Cn1.ConnectionString = "Data Source=" & CServer1 & ";Initial Catalog=" & CDatabase1 &
                        ";User Id=" & CUserId1 & ";Password=" & CPassword1 & ";" &
                        ";Connect Timeout=800;Max Pool Size=400"
        Cn1.Open()
        Cmd1 = New SqlClient.SqlCommand
        Cmd1.Connection = Cn1
        Cmd1.CommandType = CommandType.Text
        Cmd1.CommandTimeout = 300000
    End Sub

    Public Sub CloseConn1()
        If Not Cn1 Is Nothing Then
            Cn1.Close()
            Cn1 = Nothing
        End If
    End Sub

    Public Sub ExecuteTrans1(ByVal Query1 As String)
        Cmd1.CommandText = Query1
        Cmd1.ExecuteNonQuery()
    End Sub

    Public Function OpenTrans1(ByVal Query1 As String) As SqlClient.SqlDataReader
        Cmd1.CommandText = Query1
        Return Cmd1.ExecuteReader
    End Function

    Public Sub CloseTrans1()
        If Not (Cmd1.Transaction Is Nothing) Then
            Cmd1.Transaction.Rollback()
        End If
    End Sub

    Public Sub CloseDr1()
        If Not Dr1 Is Nothing Then
            Dr1.Close()
            Dr1 = Nothing
        End If
    End Sub

    Public Function BindingTrans1(ByVal Query1 As String) As DataSet
        Cmd1.CommandText = Query1
        Da1 = New SqlClient.SqlDataAdapter
        Da1.SelectCommand = Cmd1
        BindingTrans1 = New DataSet
        Da1.Fill(BindingTrans1, "MyTable")
    End Function



#End Region

#Region "Modul 2"

    Public Sub OpenConn2()
        If CServer2 = "" Or CDatabase2 = "" Or CUserId2 = "" Or CPassword2 = "" Then
            MessageBox.Show("Gagal Membuka Koneksi")
            Exit Sub
        End If

        General_Class.SetConnectionString(CServer2, CDatabase2, CUserId2, CPassword2)
        Cn2 = New SqlClient.SqlConnection
        Cn2.ConnectionString = "Data Source=" & CServer2 & ";Initial Catalog=" & CDatabase2 &
                        ";User Id=" & CUserId2 & ";Password=" & CPassword2 & ";" &
                        ";Connect Timeout=800;Max Pool Size=400"
        Cn2.Open()
        Cmd2 = New SqlClient.SqlCommand
        Cmd2.Connection = Cn2
        Cmd2.CommandType = CommandType.Text
        Cmd2.CommandTimeout = 300000
    End Sub

    Public Sub CloseConn2()
        If Not Cn2 Is Nothing Then
            Cn2.Close()
            Cn2 = Nothing
        End If
    End Sub

    Public Sub ExecuteTrans2(ByVal Query2 As String)
        Cmd2.CommandText = Query2
        Cmd2.ExecuteNonQuery()
    End Sub

    Public Function OpenTrans2(ByVal Query2 As String) As SqlClient.SqlDataReader
        Cmd2.CommandText = Query2
        Return Cmd2.ExecuteReader
    End Function

    Public Sub CloseTrans2()
        If Not (Cmd2.Transaction Is Nothing) Then
            Cmd2.Transaction.Rollback()
        End If
    End Sub

    Public Sub CloseDr2()
        If Not Dr2 Is Nothing Then
            Dr2.Close()
            Dr2 = Nothing
        End If
    End Sub

    Public Function BindingTrans2(ByVal Query2 As String) As DataSet
        Cmd2.CommandText = Query2
        Da2 = New SqlClient.SqlDataAdapter
        Da2.SelectCommand = Cmd2
        BindingTrans2 = New DataSet
        Da2.Fill(BindingTrans2, "MyTable")
    End Function

#End Region

#End Region

End Class
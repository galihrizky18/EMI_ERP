Imports System.Data.SqlClient

Public Class FR_Main

    Private Property _id_karyawan As String
    Private Property _KaryawanLogin As New DataTable

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        _KaryawanLogin.Clear()

        If Not String.IsNullOrWhiteSpace(_id_karyawan) Then
            Dim UserLogin As DataTable = LoadDataKaryawan(_id_karyawan)
            If UserLogin.Rows.Count > 0 Then
                _KaryawanLogin = UserLogin
            Else
                _KaryawanLogin.Clear()
            End If

        Else
            MessageBox.Show("Data Karyawan Kosong")

        End If


    End Sub

    Public Sub New(ByVal KaryawanID As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._id_karyawan = KaryawanID

    End Sub

    Private Function LoadDataKaryawan(ByVal KaryawanId As String) As DataTable

        Dim Data As New DataTable()

        Try
            OpenConn()
            Dim Sql As String

            Sql = "SELECT TOP 1 KaryawanID, Nama, JenisKelamin,TanggalLahir, Alamat, Email, "
            Sql = Sql & "NoTelpn, RoleId FROM Karyawans WHERE KaryawanID='" & KaryawanId & "'"

            Using Cmd As New SqlCommand(Sql, Cn)
                Using dr = OpenTrans(Sql)
                    If dr.HasRows Then
                        Data.Load(dr)
                    End If

                End Using
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try

        Return Data

    End Function

    Private Sub LoadMenuStrip()

        Dim MenuStrip As New MenuStrip()

        'create menu in menu strip
        Dim Menu1 As New ToolStripMenuItem("Menu 1")

        'create sub menu
        Dim SubMenu1 As New ToolStripMenuItem("Sub 1")
        Dim SubMenu2 As New ToolStripMenuItem("Sub 2")

        'create sub menu to Main Menu
        Menu1.DropDownItems.Add(SubMenu1)
        Menu1.DropDownItems.Add(SubMenu2)

        MenuStrip.Items.Add(Menu1)

        Me.Controls.Add(MenuStrip)


    End Sub

    Private Sub FR_Main_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Hide()
        FR_Login.Show()
        FR_Login.Focus()
    End Sub
End Class

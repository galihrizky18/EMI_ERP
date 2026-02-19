'Imports MySql.Data.MySqlClient

Module General_Module_B2B
    Public CnB2B As SqlClient.SqlConnection
    Public CmdB2B As SqlClient.SqlCommand
    Public DaB2B As SqlClient.SqlDataAdapter
    Public DrB2B As SqlClient.SqlDataReader
    Public DsB2B As DataSet
    Public SQLB2B As String

    Public CServerB2B As String = "team311.dyndns.info"
    Public Const CDatabaseB2B As String = "grahaweb_tm"
    Public Const CUserIdB2B As String = "sqlserver"
    'Public Const CPasswordB2B As String = "**H0L4H0L4hola**"
    Public Const CPasswordB2B As String = "MakanEnak301%"

    Public Sub OpenConnB2B()
        General_Class.SetConnectionString(CServerB2B, CDatabaseB2B, CUserIdB2B, CPasswordB2B)
        CnB2B = New SqlClient.SqlConnection
        CnB2B.ConnectionString = "Data Source=" & CServerB2B & ";Initial Catalog=" & CDatabaseB2B &
                        ";User Id=" & CUserIdB2B & ";Password=" & CPasswordB2B & ";" &
                        ";Connect Timeout=800;Max Pool Size=400"
        CnB2B.Open()
        CmdB2B = New SqlClient.SqlCommand
        CmdB2B.Connection = CnB2B
        CmdB2B.CommandType = CommandType.Text
        CmdB2B.CommandTimeout = 300000
    End Sub

    Public Sub CloseConnB2B()
        If Not CnB2B Is Nothing Then
            CnB2B.Close()
            CnB2B = Nothing
        End If
    End Sub

    Public Sub ExecuteTransB2B(ByVal QueryB2B As String)
        CmdB2B.CommandText = QueryB2B
        CmdB2B.ExecuteNonQuery()
    End Sub

    Public Function OpenTransB2B(ByVal QueryB2B As String) As SqlClient.SqlDataReader
        CmdB2B.CommandText = QueryB2B
        Return CmdB2B.ExecuteReader
    End Function

    Public Sub CloseTransB2B()
        If Not (CmdB2B.Transaction Is Nothing) Then
            CmdB2B.Transaction.Rollback()
        End If
    End Sub

    Public Sub CloseDrB2B()
        If Not DrB2B Is Nothing Then
            DrB2B.Close()
            DrB2B = Nothing
        End If
    End Sub

    Public Function BindingTransB2B(ByVal QueryB2B As String) As DataSet
        CmdB2B.CommandText = QueryB2B
        DaB2B = New SqlClient.SqlDataAdapter
        DaB2B.SelectCommand = CmdB2B
        BindingTransB2B = New DataSet
        DaB2B.Fill(BindingTransB2B, "MyTable")
    End Function
End Module


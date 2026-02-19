Imports MySql.Data

Module Module_MySQL


    Public CnMySQL As MySqlClient.MySqlConnection
    Public Cn1MySQL As MySqlClient.MySqlConnection
    Public CmdMySQL As MySqlClient.MySqlCommand
    Public DaMySQL As MySqlClient.MySqlDataAdapter
    Public DrMySQL As MySqlClient.MySqlDataReader
    Public DsMySQL As DataSet



    Public CServerMySQL As String = "103.7.8.230"
    Public Const CDatabaseMySQL As String = "project_test"
    Public Const CUserIdMySQL As String = "project_testing_db"
    Public Const CPasswordMySQL As String = "C@p519d4z"



    Public Sub OpenConnMySQL()
        General_Class.SetConnectionString(CServerMySQL, CDatabaseMySQL, CUserIdMySQL, CPasswordMySQL)
        CnMySQL = New MySqlClient.MySqlConnection
        CnMySQL.ConnectionString = "Data Source=" & CServerMySQL & ";Initial Catalog=" & CDatabaseMySQL &
                        ";User Id=" & CUserIdMySQL & ";Password=" & CPasswordMySQL & ";" &
                        ";Connect Timeout=30;Max Pool Size=400"
        CnMySQL.Open()
        CmdMySQL = New MySqlClient.MySqlCommand
        CmdMySQL.Connection = CnMySQL
        CmdMySQL.CommandType = CommandType.Text
    End Sub

    Public Sub CloseConnMySQL()
        If Not CnMySQL Is Nothing Then
            CnMySQL.Close()
            CnMySQL = Nothing
        End If
    End Sub

    Public Sub ExecuteTransMySQL(ByVal Query As String)
        CmdMySQL.CommandText = Query
        CmdMySQL.ExecuteNonQuery()
        'Cmd = Nothing
    End Sub

    Public Function OpenTransMySQL(ByVal Query As String) As MySqlClient.MySqlDataReader
        CmdMySQL.CommandText = Query
        Return CmdMySQL.ExecuteReader
    End Function

    Public Sub CloseTransMySQL()
        If Not (CmdMySQL.Transaction Is Nothing) Then
            CmdMySQL.Transaction.Rollback()
        End If
    End Sub

    Public Function BindingTransMySQL(ByVal Query As String) As DataSet
        CmdMySQL.CommandText = Query
        DaMySQL = New MySqlClient.MySqlDataAdapter
        DaMySQL.SelectCommand = CmdMySQL
        BindingTransMySQL = New DataSet
        DaMySQL.Fill(BindingTransMySQL, "MyTable")
    End Function

End Module


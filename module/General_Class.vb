Public Class General_Class
    Public Shared Cn As SqlClient.SqlConnection
    Public Shared Cmd As SqlClient.SqlCommand
    Public Shared Dr As SqlClient.SqlDataReader
    Public Shared Da As SqlClient.SqlDataAdapter
    Public Shared Ds As DataSet

    Private Shared _Server As String
    Private Shared _Database As String
    Private Shared _UserID As String
    Private Shared _Password As String

    Public Shared Property Server() As String
        Get
            Return _Server
        End Get
        Set(ByVal value As String)
            _Server = value
        End Set
    End Property

    Public Shared Property Database() As String
        Get
            Return _Database
        End Get
        Set(ByVal value As String)
            _Database = value
        End Set
    End Property

    Public Shared Property UserID() As String
        Get
            Return _UserID
        End Get
        Set(ByVal value As String)
            _UserID = value
        End Set
    End Property

    Public Shared Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property


    Public Shared Sub SetConnectionString(ByVal xServer As String, ByVal xDatabase As String, _
                    ByVal xUserID As String, ByVal xPassword As String)
        Server = xServer
        Database = xDatabase
        UserID = xUserID
        Password = xPassword
    End Sub

    Public Shared Sub OpenConn()
        Try
            Cn = New SqlClient.SqlConnection
            Cn.ConnectionString = "Data Source=" & Server & ";Initial Catalog=" & Database &
                                    ";integrated security = true"
            Cn.Open()

            Cmd = New SqlClient.SqlCommand
            Cmd.CommandType = CommandType.Text
            Cmd.CommandTimeout = 0
            Cmd.Connection = Cn

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub CloseConn()
        Try
            If Not Cn Is Nothing Then
                Cn.Close()
                Cn = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub Execute(ByVal Query As String)
        Try
            OpenConn()

            Cmd.CommandTimeout = 0
            Cmd.CommandText = Query
            Cmd.ExecuteNonQuery()
            Cmd = Nothing

            CloseConn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Function Open(ByVal Query As String) As SqlClient.SqlDataReader
        Try
            OpenConn()

            Cmd.CommandText = Query
            'Cmd.CommandTimeout = 0
            Return Cmd.ExecuteReader
            Cmd = Nothing

            'CloseConn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function Open_Without_Connection(ByVal Query As String) As SqlClient.SqlDataReader
        Try
            Cmd = New SqlClient.SqlCommand
            Cmd.CommandType = CommandType.Text
            Cmd.CommandTimeout = 0
            Cmd.Connection = Cn

            Cmd.CommandText = Query
            Return Cmd.ExecuteReader

            Cmd = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Cmd = Nothing
        End Try
    End Function

    Public Shared Function Binding(ByVal Query As String) As DataSet
        Try
            OpenConn()

            Cmd.CommandText = Query
            Cmd.CommandTimeout = 0
            Da = New SqlClient.SqlDataAdapter
            Da.SelectCommand = Cmd
            Binding = New DataSet
            Da.Fill(Binding, "MyTable")
            Da = Nothing

            CloseConn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Binding = Nothing
        End Try
    End Function

    Public Shared Function Binding_Without_Connection(ByVal Query As String) As DataSet
        Try
            Cmd = New SqlClient.SqlCommand
            Cmd.Connection = Cn
            Cmd.CommandText = Query
            Cmd.CommandTimeout = 0
            Da = New SqlClient.SqlDataAdapter
            Da.SelectCommand = Cmd
            Binding_Without_Connection = New DataSet
            Da.Fill(Binding_Without_Connection, "MyTable")
            Da = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Sub Fill_Combo(ByVal xCombo As System.Windows.Forms.ComboBox, _
                ByVal TableName As String, _
                ByVal FieldToOrder As String, _
                ByVal FieldToDisplay1 As String, _
                ByVal FieldToDisplay2 As String, _
                Optional ByVal ConditionField1 As String = "Default", _
                Optional ByVal ConditionValue1 As String = "Default", _
                Optional ByVal xOperator As String = "And", _
                Optional ByVal ConditionField2 As String = "Default", _
                Optional ByVal ConditionValue2 As String = "Default")
        Try
            Dim SQL As String

            OpenConn()

            SQL = "Select * From " & TableName
            If Not ConditionField1 = "Default" Then
                SQL &= " Where " & ConditionField1 & " = '" & ConditionValue1 & "'"
            End If
            If Not ConditionField2 = "Default" Then
                SQL &= " " & xOperator & " " & ConditionField2 & " = '" & ConditionValue2 & "'"
            End If
            SQL &= " order by " & FieldToOrder

            Dr = Open(SQL)
            xCombo.Items.Clear() : xCombo.Text = ""
            Do While Dr.Read
                xCombo.Items.Add(Dr(FieldToDisplay1) & " - " & Dr(FieldToDisplay2))
            Loop
            Dr.Close()

            CloseConn()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Function Get_Last_Number(ByVal TableName As String, _
                ByVal FieldToFind As String, _
                ByVal DigitCount As Integer, _
                Optional ByVal FieldParameter1 As String = "", _
                Optional ByVal Parameter1 As String = "", _
                Optional ByVal xOperator As String = "And", _
                Optional ByVal FieldParameter2 As String = "", _
                Optional ByVal Parameter2 As String = "") _
        As String

        Try
            Dim LastNumber As Integer = 1
            Dim StrLastNumber As String
            Dim SQL As String

            OpenConn()

            SQL = "Select top 1 " & FieldToFind & " from " & TableName & " "
            If Not FieldParameter1 = "" Then
                SQL = SQL & "Where " & FieldParameter1 & " = '" & Parameter1 & "' "
            End If
            If Not FieldParameter2 = "" Then
                SQL = SQL & xOperator & " " & FieldParameter2 & " = '" & Parameter2 & "' "
            End If
            SQL = SQL & "order by " & FieldToFind & " desc"

            Dr = Open(SQL)

            Dim xxx As String 'No Terakhir
            If Dr.Read Then

                xxx = Strings.Right(Dr("" & FieldToFind & ""), DigitCount)
                LastNumber = Val(xxx) + 1
                Select Case LastNumber
                    Case Is <= 10
                        StrLastNumber = ("0000000" & Trim(Str(LastNumber)))
                    Case Is <= 100
                        StrLastNumber = ("000000" & Trim(Str(LastNumber)))
                    Case Is <= 1000
                        StrLastNumber = ("00000" & Trim(Str(LastNumber)))
                    Case Is <= 10000
                        StrLastNumber = ("0000" & Trim(Str(LastNumber)))
                    Case Is <= 100000
                        StrLastNumber = ("000" & Trim(Str(LastNumber)))
                    Case Is <= 1000000
                        StrLastNumber = ("00" & Trim(Str(LastNumber)))
                    Case Is <= 10000000
                        StrLastNumber = ("0" & Trim(Str(LastNumber)))
                    Case Else
                        StrLastNumber = Trim((Str(LastNumber)))
                End Select
            Else
                StrLastNumber = "00000001"
            End If
            Dr.Close()

            CloseConn()

            Return Right(StrLastNumber, DigitCount)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Public Shared Function Get_Last_Number2(ByVal TableName As String, _
              ByVal FieldToFind As String, _
              ByVal DigitCount As Integer, _
              Optional ByVal FieldParameter1 As String = "", _
              Optional ByVal Parameter1 As String = "", _
              Optional ByVal xOperator As String = "And", _
              Optional ByVal FieldParameter2 As String = "", _
              Optional ByVal Parameter2 As String = "") _
      As String

        Dim LastNumber As Integer = 1
        Dim StrLastNumber As String
        Dim SQL As String

        'OpenConn()

        SQL = "Select top 1 " & FieldToFind & " from " & TableName & " "
        If Not FieldParameter1 = "" Then
            SQL = SQL & "Where " & FieldParameter1 & " = '" & Parameter1 & "' "
        End If
        If Not FieldParameter2 = "" Then
            SQL = SQL & xOperator & " " & FieldParameter2 & " = '" & Parameter2 & "' "
        End If
        SQL = SQL & "order by " & FieldToFind & " desc"

        Dr = OpenTrans(SQL)

        Dim xxx As String 'No Terakhir
        If Dr.Read Then

            xxx = Strings.Right(Dr("" & FieldToFind & ""), DigitCount)
            LastNumber = Val(xxx) + 1
            Select Case LastNumber
                Case Is <= 10
                    StrLastNumber = ("0000000" & Trim(Str(LastNumber)))
                Case Is <= 100
                    StrLastNumber = ("000000" & Trim(Str(LastNumber)))
                Case Is <= 1000
                    StrLastNumber = ("00000" & Trim(Str(LastNumber)))
                Case Is <= 10000
                    StrLastNumber = ("0000" & Trim(Str(LastNumber)))
                Case Is <= 100000
                    StrLastNumber = ("000" & Trim(Str(LastNumber)))
                Case Is <= 1000000
                    StrLastNumber = ("00" & Trim(Str(LastNumber)))
                Case Is <= 10000000
                    StrLastNumber = ("0" & Trim(Str(LastNumber)))
                Case Else
                    StrLastNumber = Trim((Str(LastNumber)))
            End Select
        Else
            StrLastNumber = "00000001"
        End If
        Dr.Close()

        'CloseConn()

        Return Right(StrLastNumber, DigitCount)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return ""
        'End Try
    End Function

    Public Shared Function CekNULL(ByVal xNullString As Object) As String
        Try
            If IsDBNull(xNullString) Then
                Return ""
            Else
                Return xNullString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Public Shared Function CekZERO(ByVal xNolString As Object) As Double
        Try
            If IsDBNull(xNolString) Then
                Return "0"
            Else
                Return xNolString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "0"
        End Try
    End Function

    Public Shared Function CekDatePicker(ByVal xNullDate As Object) As Date
        Try
            If IsDBNull(xNullDate) Then
                Return Now.Date
            Else
                Return xNullDate
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Public Shared Function BulanToStrBulan(ByVal StrBulan As String) As String
        Try
            Select Case StrBulan
                Case "01"
                    Return "Januari"
                Case "02"
                    Return "Februari"
                Case "03"
                    Return "Maret"
                Case "04"
                    Return "April"
                Case "05"
                    Return "Mei"
                Case "06"
                    Return "Juni"
                Case "07"
                    Return "Juli"
                Case "08"
                    Return "Agustus"
                Case "09"
                    Return "September"
                Case "10"
                    Return "Oktober"
                Case "11"
                    Return "November"
                Case "12"
                    Return "Desember"
                Case Else
                    Return "Error"
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "Error"
        End Try
    End Function

    Public Shared Function StrBulanToBulan(ByVal StrBulan As String) As String
        Try
            Select Case StrBulan.ToUpper
                Case "JANUARI"
                    Return "01"
                Case "FEBRUARI"
                    Return "02"
                Case "MARET"
                    Return "03"
                Case "APRIL"
                    Return "04"
                Case "MEI"
                    Return "05"
                Case "JUNI"
                    Return "06"
                Case "JULI"
                    Return "07"
                Case "AGUSTUS"
                    Return "08"
                Case "SEPTEMBER"
                    Return "09"
                Case "OKTOBER"
                    Return "10"
                Case "NOVEMBER"
                    Return "11"
                Case "DESEMBER"
                    Return "12"
                Case Else
                    Return "00"
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "00"
        End Try
    End Function

    Public Shared Function SayRupiah(ByVal StrNilai As String)
        Try
            Dim Terbilang As String
            Dim Huruf(8) As String
            Dim Kata(5) As String
            Dim strRibu As String
            Dim strJuta As String
            Dim strMilyar As String
            Dim strTrilyun As String
            Dim Nilai As String
            Dim Nilai_Atas As String
            Dim Nilai_Bawah As String
            Dim Nilai_tmp As String
            Dim Koma As Integer
            Dim Ln As Integer
            Dim nLn As Integer
            Dim cnt As Integer
            Dim varA, varB, varC, varD, varE, varF, varG As Integer

            ' Constanta nilai terbilang
            If Val(StrNilai) > 0 Then
                Huruf(0) = "SATU "
                Huruf(1) = "DUA "
                Huruf(2) = "TIGA "
                Huruf(3) = "EMPAT "
                Huruf(4) = "LIMA "
                Huruf(5) = "ENAM "
                Huruf(6) = "TUJUH "
                Huruf(7) = "DELAPAN "
                Huruf(8) = "SEMBILAN "

                strRibu = "RIBU "
                strJuta = "JUTA "
                strMilyar = "MILYAR "
                strTrilyun = "TRILYUN "

                'Proses pengambilan nilai atas dan nilai bawah
                Nilai = Trim(StrNilai)
                Koma = InStr(Nilai, ",")
                If Koma > 0 Then
                    Nilai_Atas = Mid(Nilai, 1, Koma - 1)
                    Nilai_Bawah = Mid(Nilai, Koma + 1, Len(Nilai) - Koma)
                Else
                    Nilai_Atas = Nilai
                    Nilai_Bawah = "000"
                End If

                'Ambil panjang nilai atas

                Ln = Len(Nilai_Atas)
                Select Case Ln
                    Case 1 To 3
                        nLn = 1
                    Case 4 To 6
                        nLn = 2
                    Case 7 To 9
                        nLn = 3
                    Case 10 To 12
                        nLn = 4
                    Case Else
                        nLn = 5
                End Select

                'Menambahkan huruf 0 (nol) di depan Nilai Atas supaya genap 3-3 digit

                For cnt = 1 To (nLn * 3) - Ln
                    Nilai_Atas = "0" & Nilai_Atas
                Next
                varG = 0
                varA = 1
                varC = 2
                While varA <= nLn
                    varB = 1
                    varD = varC
                    varE = 2
                    varF = 2
                    While varB < 3
                        Nilai_tmp = Mid(Nilai_Atas, varD, varF)
                        If varB = 1 Then
                            If Nilai_tmp = "00" Then
                                Kata(varG) = " "
                            ElseIf Nilai_tmp = "01" Then
                                'Mengambil angka 1 sebagai "SE" atau "SATU "
                                If nLn > 1 And Mid(Nilai_Atas, varD - 1, 1) = "0" And _
                                    Mid(Nilai_Atas, varD + 5, 1) = "" Then
                                    Kata(varG) = "SE"
                                Else
                                    Kata(varG) = "SATU "
                                End If
                            ElseIf Mid(Nilai_tmp, 1, 1) = "0" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 2, 1) - 1))
                            ElseIf Nilai_tmp = "10" Then
                                Kata(varG) = "SEPULUH "
                            ElseIf Mid(Nilai_tmp, 2, 1) = "0" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 1, 1) - 1)) & "PULUH "
                            ElseIf Nilai_tmp = "11" Then
                                Kata(varG) = "SEBELAS "
                            ElseIf Mid(Nilai_tmp, 1, 1) = "1" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 2, 1) - 1)) & "BELAS "
                            ElseIf Mid(Nilai_tmp, 1, 1) > "1" And Mid(Nilai_tmp, 2, 1) > "0" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 1, 1) - 1)) & "PULUH " & _
                                    Huruf(Val(Mid(Nilai_tmp, 2, 1) - 1))
                            End If
                        Else
                            If Nilai_tmp = "1" Then
                                Kata(varG) = "SERATUS " & Kata(varG)
                            ElseIf Nilai_tmp > 0 Then
                                Kata(varG) = Huruf(Val(Nilai_tmp) - 1) & "RATUS " & Kata(varG)
                            End If
                        End If
                        varB = varB + 1
                        varD = varC - 1
                        varF = 1
                    End While
                    varG = varG + 1
                    varC = varC + 3
                    varA = varA + 1
                End While

                Select Case nLn
                    Case 2
                        Terbilang = Kata(0) & strRibu & Kata(1)
                    Case 3
                        Terbilang = Kata(0) & strJuta
                        If Kata(1) <> " " Then
                            Terbilang = Terbilang & Kata(1) & strRibu
                        Else
                            Terbilang = Terbilang
                        End If
                        Terbilang = Terbilang & Kata(2)
                    Case 4
                        Terbilang = Kata(0) & strMilyar
                        If Kata(1) <> " " Then
                            Terbilang = Terbilang & Kata(1) & strJuta
                        Else
                            Terbilang = Terbilang
                        End If
                        If Kata(2) <> " " Then
                            Terbilang = Terbilang & Kata(2) & strRibu
                        Else
                            Terbilang = Terbilang
                        End If
                        Terbilang = Terbilang & Kata(3)
                    Case 5
                        Terbilang = Kata(0) & strTrilyun
                        If Kata(1) <> " " Then
                            Terbilang = Terbilang & Kata(1) & strMilyar
                        Else
                            Terbilang = Terbilang
                        End If
                        If Kata(2) <> " " Then
                            Terbilang = Terbilang & Kata(2) & strJuta
                        Else
                            Terbilang = Terbilang
                        End If
                        If Kata(3) <> " " Then
                            Terbilang = Terbilang & Kata(3) & strRibu
                        Else
                            Terbilang = Terbilang
                        End If
                        Terbilang = Terbilang & Kata(4)
                    Case Else
                        Terbilang = Kata(0)
                End Select
                SayRupiah = Chr(34) & Terbilang & "RUPIAH" & Chr(34)
            Else
                SayRupiah = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""

        End Try
    End Function

    Public Shared Function SayTerbilang(ByVal StrNilai As String)
        Try
            Dim Terbilang As String
            Dim Huruf(8) As String
            Dim Kata(5) As String
            Dim strRibu As String
            Dim strJuta As String
            Dim strMilyar As String
            Dim strTrilyun As String
            Dim Nilai As String
            Dim Nilai_Atas As String
            Dim Nilai_Bawah As String
            Dim Nilai_tmp As String
            Dim Koma As Integer
            Dim Ln As Integer
            Dim nLn As Integer
            Dim cnt As Integer
            Dim varA, varB, varC, varD, varE, varF, varG As Integer

            ' Constanta nilai terbilang
            If Val(StrNilai) > 0 Then
                Huruf(0) = "SATU "
                Huruf(1) = "DUA "
                Huruf(2) = "TIGA "
                Huruf(3) = "EMPAT "
                Huruf(4) = "LIMA "
                Huruf(5) = "ENAM "
                Huruf(6) = "TUJUH "
                Huruf(7) = "DELAPAN "
                Huruf(8) = "SEMBILAN "

                strRibu = "RIBU "
                strJuta = "JUTA "
                strMilyar = "MILYAR "
                strTrilyun = "TRILYUN "

                'Proses pengambilan nilai atas dan nilai bawah
                Nilai = Trim(StrNilai)
                Koma = InStr(Nilai, ",")
                If Koma > 0 Then
                    Nilai_Atas = Mid(Nilai, 1, Koma - 1)
                    Nilai_Bawah = Mid(Nilai, Koma + 1, Len(Nilai) - Koma)
                Else
                    Nilai_Atas = Nilai
                    Nilai_Bawah = "000"
                End If

                'Ambil panjang nilai atas

                Ln = Len(Nilai_Atas)
                Select Case Ln
                    Case 1 To 3
                        nLn = 1
                    Case 4 To 6
                        nLn = 2
                    Case 7 To 9
                        nLn = 3
                    Case 10 To 12
                        nLn = 4
                    Case Else
                        nLn = 5
                End Select

                'Menambahkan huruf 0 (nol) di depan Nilai Atas supaya genap 3-3 digit

                For cnt = 1 To (nLn * 3) - Ln
                    Nilai_Atas = "0" & Nilai_Atas
                Next
                varG = 0
                varA = 1
                varC = 2
                While varA <= nLn
                    varB = 1
                    varD = varC
                    varE = 2
                    varF = 2
                    While varB < 3
                        Nilai_tmp = Mid(Nilai_Atas, varD, varF)
                        If varB = 1 Then
                            If Nilai_tmp = "00" Then
                                Kata(varG) = " "
                            ElseIf Nilai_tmp = "01" Then
                                'Mengambil angka 1 sebagai "SE" atau "SATU "
                                If nLn > 1 And Mid(Nilai_Atas, varD - 1, 1) = "0" And _
                                    Mid(Nilai_Atas, varD + 5, 1) = "" Then
                                    Kata(varG) = "SE"
                                Else
                                    Kata(varG) = "SATU "
                                End If
                            ElseIf Mid(Nilai_tmp, 1, 1) = "0" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 2, 1) - 1))
                            ElseIf Nilai_tmp = "10" Then
                                Kata(varG) = "SEPULUH "
                            ElseIf Mid(Nilai_tmp, 2, 1) = "0" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 1, 1) - 1)) & "PULUH "
                            ElseIf Nilai_tmp = "11" Then
                                Kata(varG) = "SEBELAS "
                            ElseIf Mid(Nilai_tmp, 1, 1) = "1" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 2, 1) - 1)) & "BELAS "
                            ElseIf Mid(Nilai_tmp, 1, 1) > "1" And Mid(Nilai_tmp, 2, 1) > "0" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 1, 1) - 1)) & "PULUH " & _
                                    Huruf(Val(Mid(Nilai_tmp, 2, 1) - 1))
                            End If
                        Else
                            If Nilai_tmp = "1" Then
                                Kata(varG) = "SERATUS " & Kata(varG)
                            ElseIf Nilai_tmp > 0 Then
                                Kata(varG) = Huruf(Val(Nilai_tmp) - 1) & "RATUS " & Kata(varG)
                            End If
                        End If
                        varB = varB + 1
                        varD = varC - 1
                        varF = 1
                    End While
                    varG = varG + 1
                    varC = varC + 3
                    varA = varA + 1
                End While

                Select Case nLn
                    Case 2
                        Terbilang = Kata(0) & strRibu & Kata(1)
                    Case 3
                        Terbilang = Kata(0) & strJuta
                        If Kata(1) <> " " Then
                            Terbilang = Terbilang & Kata(1) & strRibu
                        Else
                            Terbilang = Terbilang
                        End If
                        Terbilang = Terbilang & Kata(2)
                    Case 4
                        Terbilang = Kata(0) & strMilyar
                        If Kata(1) <> " " Then
                            Terbilang = Terbilang & Kata(1) & strJuta
                        Else
                            Terbilang = Terbilang
                        End If
                        If Kata(2) <> " " Then
                            Terbilang = Terbilang & Kata(2) & strRibu
                        Else
                            Terbilang = Terbilang
                        End If
                        Terbilang = Terbilang & Kata(3)
                    Case 5
                        Terbilang = Kata(0) & strTrilyun
                        If Kata(1) <> " " Then
                            Terbilang = Terbilang & Kata(1) & strMilyar
                        Else
                            Terbilang = Terbilang
                        End If
                        If Kata(2) <> " " Then
                            Terbilang = Terbilang & Kata(2) & strJuta
                        Else
                            Terbilang = Terbilang
                        End If
                        If Kata(3) <> " " Then
                            Terbilang = Terbilang & Kata(3) & strRibu
                        Else
                            Terbilang = Terbilang
                        End If
                        Terbilang = Terbilang & Kata(4)
                    Case Else
                        Terbilang = Kata(0)
                End Select
                SayTerbilang = Terbilang 'Chr(34) & 
            Else
                SayTerbilang = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""

        End Try
    End Function

    Public Shared Function SayMUA(ByVal StrNilai As String, ByVal StrMUA As String)
        Try
            Dim TerbilangMUA As String = ""
            SQL = "select Terbilang from mata_uang where "
            SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Mata_uang='" & StrMUA & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TerbilangMUA = dr("Terbilang")
                End If
            End Using

            Dim Terbilang As String
            Dim Huruf(8) As String
            Dim Kata(5) As String
            Dim strRibu As String
            Dim strJuta As String
            Dim strMilyar As String
            Dim strTrilyun As String
            Dim Nilai As String
            Dim Nilai_Atas As String
            Dim Nilai_Bawah As String
            Dim Nilai_tmp As String
            Dim Koma As Integer
            Dim Ln As Integer
            Dim nLn As Integer
            Dim cnt As Integer
            Dim varA, varB, varC, varD, varE, varF, varG As Integer

            ' Constanta nilai terbilang
            If Val(StrNilai) > 0 Then
                Huruf(0) = "SATU "
                Huruf(1) = "DUA "
                Huruf(2) = "TIGA "
                Huruf(3) = "EMPAT "
                Huruf(4) = "LIMA "
                Huruf(5) = "ENAM "
                Huruf(6) = "TUJUH "
                Huruf(7) = "DELAPAN "
                Huruf(8) = "SEMBILAN "

                strRibu = "RIBU "
                strJuta = "JUTA "
                strMilyar = "MILYAR "
                strTrilyun = "TRILYUN "

                'Proses pengambilan nilai atas dan nilai bawah
                Nilai = Trim(StrNilai)
                Koma = InStr(Nilai, ",")
                If Koma > 0 Then
                    Nilai_Atas = Mid(Nilai, 1, Koma - 1)
                    Nilai_Bawah = Mid(Nilai, Koma + 1, Len(Nilai) - Koma)
                Else
                    Nilai_Atas = Nilai
                    Nilai_Bawah = "000"
                End If

                'Ambil panjang nilai atas

                Ln = Len(Nilai_Atas)
                Select Case Ln
                    Case 1 To 3
                        nLn = 1
                    Case 4 To 6
                        nLn = 2
                    Case 7 To 9
                        nLn = 3
                    Case 10 To 12
                        nLn = 4
                    Case Else
                        nLn = 5
                End Select

                'Menambahkan huruf 0 (nol) di depan Nilai Atas supaya genap 3-3 digit

                For cnt = 1 To (nLn * 3) - Ln
                    Nilai_Atas = "0" & Nilai_Atas
                Next
                varG = 0
                varA = 1
                varC = 2
                While varA <= nLn
                    varB = 1
                    varD = varC
                    varE = 2
                    varF = 2
                    While varB < 3
                        Nilai_tmp = Mid(Nilai_Atas, varD, varF)
                        If varB = 1 Then
                            If Nilai_tmp = "00" Then
                                Kata(varG) = " "
                            ElseIf Nilai_tmp = "01" Then
                                'Mengambil angka 1 sebagai "SE" atau "SATU "
                                If nLn > 1 And Mid(Nilai_Atas, varD - 1, 1) = "0" And _
                                    Mid(Nilai_Atas, varD + 5, 1) = "" Then
                                    Kata(varG) = "SE"
                                Else
                                    Kata(varG) = "SATU "
                                End If
                            ElseIf Mid(Nilai_tmp, 1, 1) = "0" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 2, 1) - 1))
                            ElseIf Nilai_tmp = "10" Then
                                Kata(varG) = "SEPULUH "
                            ElseIf Mid(Nilai_tmp, 2, 1) = "0" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 1, 1) - 1)) & "PULUH "
                            ElseIf Nilai_tmp = "11" Then
                                Kata(varG) = "SEBELAS "
                            ElseIf Mid(Nilai_tmp, 1, 1) = "1" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 2, 1) - 1)) & "BELAS "
                            ElseIf Mid(Nilai_tmp, 1, 1) > "1" And Mid(Nilai_tmp, 2, 1) > "0" Then
                                Kata(varG) = Huruf(Val(Mid(Nilai_tmp, 1, 1) - 1)) & "PULUH " & _
                                    Huruf(Val(Mid(Nilai_tmp, 2, 1) - 1))
                            End If
                        Else
                            If Nilai_tmp = "1" Then
                                Kata(varG) = "SERATUS " & Kata(varG)
                            ElseIf Nilai_tmp > 0 Then
                                Kata(varG) = Huruf(Val(Nilai_tmp) - 1) & "RATUS " & Kata(varG)
                            End If
                        End If
                        varB = varB + 1
                        varD = varC - 1
                        varF = 1
                    End While
                    varG = varG + 1
                    varC = varC + 3
                    varA = varA + 1
                End While

                Select Case nLn
                    Case 2
                        Terbilang = Kata(0) & strRibu & Kata(1)
                    Case 3
                        Terbilang = Kata(0) & strJuta
                        If Kata(1) <> " " Then
                            Terbilang = Terbilang & Kata(1) & strRibu
                        Else
                            Terbilang = Terbilang
                        End If
                        Terbilang = Terbilang & Kata(2)
                    Case 4
                        Terbilang = Kata(0) & strMilyar
                        If Kata(1) <> " " Then
                            Terbilang = Terbilang & Kata(1) & strJuta
                        Else
                            Terbilang = Terbilang
                        End If
                        If Kata(2) <> " " Then
                            Terbilang = Terbilang & Kata(2) & strRibu
                        Else
                            Terbilang = Terbilang
                        End If
                        Terbilang = Terbilang & Kata(3)
                    Case 5
                        Terbilang = Kata(0) & strTrilyun
                        If Kata(1) <> " " Then
                            Terbilang = Terbilang & Kata(1) & strMilyar
                        Else
                            Terbilang = Terbilang
                        End If
                        If Kata(2) <> " " Then
                            Terbilang = Terbilang & Kata(2) & strJuta
                        Else
                            Terbilang = Terbilang
                        End If
                        If Kata(3) <> " " Then
                            Terbilang = Terbilang & Kata(3) & strRibu
                        Else
                            Terbilang = Terbilang
                        End If
                        Terbilang = Terbilang & Kata(4)
                    Case Else
                        Terbilang = Kata(0)
                End Select
                SayMUA = Chr(34) & Terbilang & TerbilangMUA & Chr(34)
            Else
                SayMUA = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Public Shared Function MasihKosong(ByVal The_Textbox As System.Windows.Forms.TextBox, _
                        ByVal Keterangan As String) As Boolean
        Try
            If The_Textbox.Text.ToString.Trim.Length = 0 Then
                System.Windows.Forms.MessageBox.Show(Keterangan & " harus diisi . . ! !", "Warning..!!", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
                The_Textbox.Focus()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function MasihKosong(ByVal The_Combobox As System.Windows.Forms.ComboBox, _
                    ByVal Keterangan As String) As Boolean
        Try
            If The_Combobox.Text.ToString.Trim.Length = 0 Then
                System.Windows.Forms.MessageBox.Show(Keterangan & " harus diisi . . ! !", "Warning..!!", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
                The_Combobox.Focus()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function MasihKosong(ByVal The_Listview As System.Windows.Forms.ListView, _
                    ByVal Keterangan As String) As Boolean
        Try
            If The_Listview.Items.Count = 0 Then
                System.Windows.Forms.MessageBox.Show(Keterangan & " harus diisi minimal 1 item . . ! !", "Warning..!!", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
                The_Listview.Focus()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
End Class


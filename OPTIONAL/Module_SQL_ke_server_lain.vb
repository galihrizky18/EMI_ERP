'Imports MySql.Data.MySqlClient
Imports LovePdf.Core
Imports LovePdf.Core.Sign
Imports LovePdf.Model.Task
Imports LovePdf.Model.TaskParams
Imports LovePdf.Model.TaskParams.Sign.Elements

Module Module_SQL_Pengajuan
    Public CnSQL As SqlClient.SqlConnection
    Public CmdSQL As SqlClient.SqlCommand
    Public DaSQL As SqlClient.SqlDataAdapter
    Public DrSQL As SqlClient.SqlDataReader
    Public DsSQL As DataSet
    Public SQLSQL As String

    Public Data_User_App, Data_Nama_App, Data_Email_App, Data_Jabatan_App, Data_Divisi_App As New ArrayList

    Dim PublicKey As String = "project_public_e4c20d1ff42102c0a1b1986880530890_TZwLt2e5b1568f99c733dcbe14f9a7f45c281"
    Dim SecretKey As String = "secret_key_2b16efaf46968a5011fda3ec2b914606_dwxr02fab3732f1f08d3aac26fcf8d60e6c32"

    Dim FilePath As String = "F:\PEKERJAAN\MAIN\FormatHC\3_Format_Pengajuan_CnB_untuk_Pak_Hendri_IT.pdf"
    Dim AccessCode As String = "3V0J4Y4"


    Private CServerSQL As String = "35.240.215.51,59114\team"
    Private Const CDatabaseSQL As String = "tes_absen"
    ''Public Const CDatabaseSQL As String = "grahaweb_tm"
    Private Const CUserIdSQL As String = "sa2"
    Private Const CPasswordSQL As String = "P@ssword99000"


    Public Sub OpenConnSQL()
        General_Class.SetConnectionString(CServerSQL, CDatabaseSQL, CUserIdSQL, CPasswordSQL)
        CnSQL = New SqlClient.SqlConnection
        CnSQL.ConnectionString = "Data Source=" & CServerSQL & ";Initial Catalog=" & CDatabaseSQL & _
                        ";User Id=" & CUserIdSQL & ";Password=" & CPasswordSQL & ";" & _
                        ";Connect Timeout=800;Max Pool Size=400"
        CnSQL.Open()
        CmdSQL = New SqlClient.SqlCommand
        CmdSQL.Connection = CnSQL
        CmdSQL.CommandType = CommandType.Text
        CmdSQL.CommandTimeout = 300000
    End Sub

    Public Sub CloseConnSQL()
        If Not CnSQL Is Nothing Then
            CnSQL.Close()
            CnSQL = Nothing
        End If
    End Sub

    Public Sub ExecuteTransSQL(ByVal QuerySQL As String)
        CmdSQL.CommandText = QuerySQL
        CmdSQL.ExecuteNonQuery()
    End Sub

    Public Function OpenTransSQL(ByVal QuerySQL As String) As SqlClient.SqlDataReader
        CmdSQL.CommandText = QuerySQL
        Return CmdSQL.ExecuteReader
    End Function

    Public Sub CloseTransSQL()
        If Not (CmdSQL.Transaction Is Nothing) Then
            CmdSQL.Transaction.Rollback()
        End If
    End Sub

    Public Sub CloseDrSQL()
        If Not DrSQL Is Nothing Then
            DrSQL.Close()
            DrSQL = Nothing
        End If
    End Sub

    Public Function BindingTransSQL(ByVal QuerySQL As String) As DataSet
        CmdSQL.CommandText = QuerySQL
        DaSQL = New SqlClient.SqlDataAdapter
        DaSQL.SelectCommand = CmdSQL
        BindingTransSQL = New DataSet
        DaSQL.Fill(BindingTransSQL, "MyTable")
    End Function



    Public Function Approval_Hierarchy(ByVal _jenis As String, ByVal _idlevel As Integer, ByVal _iddivisi As Integer) As Integer
        Data_User_App.Clear() : Data_Nama_App.Clear() : Data_Email_App.Clear()
        Data_Jabatan_App.Clear() : Data_Divisi_App.Clear()

        Dim ada_data As Integer = 0
        SQL = "select a.UserID_Approval ,b.ID_Level_Jabatan, b.ID_Divisi_Sub_Divisi, d.Keterangan "
        SQL = SQL & "from HRIS_UserID_Approval_Karyawan a,Karyawan b,HRIS_Level_Jabatan c,HRIS_Level d "
        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.UserID_Approval = b.UserID "
        SQL = SQL & "and b.ID_Level_Jabatan = c.ID_Level_Jabatan and c.ID_Level = d.ID_Level "
        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and a.Jenis_Approval = '" & _jenis & "' "
        SQL = SQL & "and a.ID_Level = '" & _idlevel & "' "
        SQL = SQL & "and a.ID_Divisi = '" & _iddivisi & "' order by d.Level_Hierarchy "
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                Data_User_App.Add(Dr("UserID_Approval"))
                Data_Nama_App.Add(Dr("UserID_Approval"))
                Data_Email_App.Add(Dr("UserID_Approval"))
                Data_Jabatan_App.Add(Dr("UserID_Approval"))
                Data_Divisi_App.Add(Dr("UserID_Approval"))

            Loop
        End Using

        If ada_data > 0 Then
            Return 1
        Else
            Return 0
        End If

    End Function

    Public Async Function Send_Approval_Email(ByVal noFaktur As String) As Task
        Try
            OpenConn()
            OpenConnSQL()
            Cmd.Transaction = Cn.BeginTransaction
            CmdSQL.Transaction = CnSQL.BeginTransaction

            Dim hasApproveData As Boolean = False

            ''=================================
            ''==    ADD APPROVAL I LOVE PDF  ==
            ''=================================

            'Dim lovePdfAPi = New LovePdfApi(PublicKey, SecretKey)

            'Dim task As SignTask = lovePdfAPi.CreateTask(Of SignTask)()

            '' Tambahkan file PDF yang akan ditandatangani
            'Dim file = task.AddFile(FilePath)

            'Dim signParams = New SignParams()
            'signParams.SubjectSigner = "C&B Karyawan Baru"
            'signParams.MessageSigner = "C&B Karyawan Baru"

            'signParams.SignerReminderDaysCycle = 2
            'signParams.SignerReminders = True
            'signParams.ExpirationDays = 30

            'signParams.VerifyEnabled = True
            'signParams.LockOrder = True
            'signParams.UuidVisible = True

            'Dim positionAwalX As String = "140" 'Fix Position 
            'Dim positionAwalY As String = "-547" 'Fix Position 


            ''ADD SIGN USER PENGAJUAN 

            'SQLSQL = "select a.nama, a.Email, d.Keterangan as Jabatan, c.Keterangan as divisi "
            'SQLSQL = SQLSQL & "from karyawan a, HRIS_Divisi_Sub_Divisi b, HRIS_Divisi c, HRIS_Jabatan d "
            'SQLSQL = SQLSQL & "where a.ID_Divisi_Sub_Divisi=b.ID_Divisi_Sub_Divisi "
            'SQLSQL = SQLSQL & "and b.ID_Divisi=c.ID_Divisi and a.ID_Level_Jabatan=d.ID_Jabatan "
            'SQLSQL = SQLSQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            ''SQL = SQL & "and a.UserID='" & UserID & "'"
            'Using DrSQL = OpenTransSQL(SQLSQL)
            '    If DrSQL.Read Then

            '        Dim email As String = "galihrizkycode@gmail.com"
            '        Dim jabatan As String = DrSQL("Jabatan") & " " & DrSQL("divisi")

            '        Dim signerPengajuan = signParams.AddSigner(DrSQL("nama"), email)
            '        signerPengajuan.AccessCode = AccessCode

            '        Dim signerFilePengajuan = signerPengajuan.AddFile(file.ServerFileName)

            '        ' Tambah elemen tanda tangan untuk signer pertama
            '        Dim signatureElement As SignatureElement = signerFilePengajuan.AddSignature()
            '        signatureElement.Position = New Position(positionAwalX, positionAwalY)
            '        signatureElement.Pages = "1"
            '        signatureElement.Size = 30

            '        'UNTUK PENAMBAHAN ELEMEN TEKS PENDEKUNG SEPERTI MENYETUJI, NAMA, JABATAN
            '        Dim generalTextElement As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText("Diajukan Oleh,")
            '        generalTextElement.Position = New Position(positionAwalX, (Val(positionAwalY) + 847).ToString)
            '        generalTextElement.Size = 17
            '        generalTextElement.Pages = "1"

            '        Dim generaElementNama As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText(DrSQL("nama"))
            '        generaElementNama.Position = New Position(positionAwalX, (Val(positionAwalY) + 767).ToString)
            '        generaElementNama.Size = 15
            '        generaElementNama.Pages = "1"

            '        Dim generaElementJabatan As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText(jabatan)
            '        generaElementJabatan.Position = New Position(positionAwalX, (Val(positionAwalY) + 757).ToString) ' Atur posisi sesuai kebutuhan
            '        generaElementJabatan.Size = 15
            '        generaElementJabatan.Pages = "1"

            '        hasApproveData = True
            '    End If

            'End Using

            'SQLSQL = "select a.No_Faktur, a.User_ID as user_approve, b.Nama, b.Telepon, b.Email, e.Keterangan as Divisi, f.Keterangan as Jabatan "
            'SQLSQL = SQLSQL & "from HRIS_Transaksi_Rekrutmen_Approval a, Karyawan b, HRIS_Divisi_Sub_Divisi c, HRIS_Divisi e, HRIS_Jabatan f "
            'SQLSQL = SQLSQL & "where b.Kode_Perusahaan = f.Kode_Perusahaan and a.User_ID = b.Kode_Karyawan and b.ID_Divisi_Sub_Divisi=c.ID_Divisi_Sub_Divisi "
            'SQLSQL = SQLSQL & "and c.ID_Divisi=e.ID_Divisi and b.ID_Level_Jabatan=f.ID_Jabatan and Acc='Y' and Tolak is null "
            'SQLSQL = SQLSQL & "and a.No_Faktur='" & noFaktur & "' "
            'SQLSQL = SQLSQL & "order by a.Level_Hierarchy "
            'Using DrSQL1 = BindingTransSQL(SQLSQL)
            '    With DrSQL1.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1
            '                'Dim Nama As String = If(IsDBNull(DrSQL("Nama")), String.Empty, DrSQL("Nama").ToString())
            '                'Dim Email As String = If(IsDBNull(DrSQL("Email")), String.Empty, DrSQL("Email").ToString())
            '                'userApprov2(index.ToString) = New List(Of String) From {Nama, Email, jabatan1, AccessCode}
            '                'index = index + 1

            '                Dim nama As String = .Rows(i).Item("Nama")
            '                Dim email1 As String = .Rows(i).Item("Email")
            '                Dim jabata1 As String = .Rows(i).Item("Jabatan") & " " & .Rows(i).Item("Divisi")


            '                positionAwalX = (Val(positionAwalX) + 140).ToString


            '                Dim signer As Signer = signParams.AddSigner(nama, email1)
            '                signer.AccessCode = AccessCode

            '                Dim signerFile1 As SignerFile = signer.AddFile(file.ServerFileName)

            '                ' Tambah elemen tanda tangan untuk signer pertama
            '                Dim signatureElement1 As SignatureElement = signerFile1.AddSignature()
            '                signatureElement1.Position = New Position(positionAwalX, positionAwalY)
            '                signatureElement1.Pages = "1"
            '                signatureElement1.Size = 30

            '                'UNTUK PENAMBAHAN ELEMEN TEKS PENDEKUNG SEPERTI MENYETUJI, NAMA, JABATAN
            '                Dim generalTextElement1 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText("Menyetujui,")
            '                generalTextElement1.Position = New Position(positionAwalX, (Val(positionAwalY) + 847).ToString)
            '                generalTextElement1.Size = 17
            '                generalTextElement1.Pages = "1"

            '                Dim generalTextElement2 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(nama)
            '                generalTextElement2.Position = New Position(positionAwalX, (Val(positionAwalY) + 767).ToString) ' Atur posisi sesuai kebutuhan
            '                generalTextElement2.Size = 15
            '                generalTextElement2.Pages = "1"

            '                Dim generalTextElement3 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(jabata1)
            '                generalTextElement3.Position = New Position(positionAwalX, (Val(positionAwalY) + 757).ToString) ' Atur posisi sesuai kebutuhan
            '                generalTextElement3.Size = 15
            '                generalTextElement3.Pages = "1"


            '                If positionAwalX = "420" Then
            '                    positionAwalX = "0"
            '                    positionAwalY = "-680"
            '                End If
            '            Next
            '        End If
            '    End With

            'End Using


            'If hasApproveData = True Then
            '    Dim signatureResponse As SignatureResponse = Await task.RequestSignatureAsync(signParams)

            '    ' Periksa apakah permintaan tanda tangan berhasil
            '    If signatureResponse Is Nothing Then
            '        CloseTrans()
            '        CloseTransSQL()
            '        CloseConn()
            '        CloseConnSQL()
            '        MessageBox.Show("Permintaan tanda tangan gagal.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If

            'End If

            Dim lovePdfAPi = New LovePdfApi(PublicKey, SecretKey)

            Dim task As SignTask = lovePdfAPi.CreateTask(Of SignTask)()

            ' Tambahkan file PDF yang akan ditandatangani
            Dim file = task.AddFile(FilePath)

            Dim signParams = New SignParams()
            signParams.SubjectSigner = "C&B Karyawan Baru"
            signParams.MessageSigner = "C&B Karyawan Baru"

            signParams.SignerReminderDaysCycle = 2
            signParams.SignerReminders = True
            signParams.ExpirationDays = 30

            signParams.VerifyEnabled = True
            signParams.LockOrder = True
            signParams.UuidVisible = True

            Dim positionAwalX As String = "140" 'Fix Position 
            Dim positionAwalY As String = "-547" 'Fix Position 


            Dim email As String = "galihrizkycode@gmail.com"
            Dim jabatan As String = "Rix"

            Dim signerPengajuan = signParams.AddSigner("Rix", email)
            signerPengajuan.AccessCode = AccessCode

            Dim signerFilePengajuan = signerPengajuan.AddFile(file.ServerFileName)

            ' Tambah elemen tanda tangan untuk signer pertama
            Dim signatureElement As SignatureElement = signerFilePengajuan.AddSignature()
            signatureElement.Position = New Position(positionAwalX, positionAwalY)
            signatureElement.Pages = "1"
            signatureElement.Size = 30

            'UNTUK PENAMBAHAN ELEMEN TEKS PENDEKUNG SEPERTI MENYETUJI, NAMA, JABATAN
            Dim generalTextElement As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText("Diajukan Oleh,")
            generalTextElement.Position = New Position(positionAwalX, (Val(positionAwalY) + 847).ToString)
            generalTextElement.Size = 17
            generalTextElement.Pages = "1"

            Dim generaElementNama As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText("Riz")
            generaElementNama.Position = New Position(positionAwalX, (Val(positionAwalY) + 767).ToString)
            generaElementNama.Size = 15
            generaElementNama.Pages = "1"

            Dim generaElementJabatan As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText(jabatan)
            generaElementJabatan.Position = New Position(positionAwalX, (Val(positionAwalY) + 757).ToString) ' Atur posisi sesuai kebutuhan
            generaElementJabatan.Size = 15
            generaElementJabatan.Pages = "1"


            Dim signatureResponse As SignatureResponse = Await task.RequestSignatureAsync(signParams)

            ' Periksa apakah permintaan tanda tangan berhasil
            If signatureResponse Is Nothing Then
                CloseTrans()
                CloseTransSQL()
                CloseConn()
                CloseConnSQL()
                MessageBox.Show("Permintaan tanda tangan gagal.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function
            End If





            Cmd.Transaction.Commit()
            CmdSQL.Transaction.Commit()
            CloseConn()
            CloseConnSQL()
        Catch ex As Exception
            CloseTrans()
            CloseTransSQL()
            CloseConn()
            CloseConnSQL()
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
    End Function

    Private Async Sub asdasdas()


    End Sub

End Module

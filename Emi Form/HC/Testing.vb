Imports CrystalDecisions.CrystalReports.ViewerObjectModel
Imports LovePdf
Imports LovePdf.Sign

Imports LovePdf.Sign.Signature
Imports LovePdf.Sign.SignatureElement
Imports LovePdf.Model.TaskParams.Sign.Elements
Imports LovePdf.Model.Task
Imports LovePdf.Core
Imports LovePdf.Model.TaskParams
Imports LovePdf.Core.Sign
Imports Microsoft.VisualBasic.ApplicationServices
Imports LovePdf.Model.TaskParams.Sign.Signers
Imports System.Text.RegularExpressions

Public Class Testing

    Dim arr As New ArrayList

    Dim PublicKey As String = "project_public_e4c20d1ff42102c0a1b1986880530890_TZwLt2e5b1568f99c733dcbe14f9a7f45c281"
    Dim SecretKey As String = "secret_key_2b16efaf46968a5011fda3ec2b914606_dwxr02fab3732f1f08d3aac26fcf8d60e6c32"

    'Dim PublicKey As String = "project_public_359975c88d0924734303ef91556255ab_wqTQYb8d591fb3b6d9c655ec7271a5fd64465"
    'Dim SecretKey As String = "secret_key_bc1b2ae6d361b74aa57c66b58718be1b_3Cnev65e98fa75dd7e2045e73164844da43ee"

    Dim FilePath As String = "F:\PEKERJAAN\MAIN\FormatHC\3_Format_Pengajuan_CnB_untuk_Pak_Hendri_IT.pdf"






    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Send_Approval_Email()


    End Sub

    Private Async Sub Send_Approval_Email()

        Dim bigUser As New List(Of List(Of String))
        Dim filePerNoFak As New List(Of List(Of String))

        get_jam()

        Try
            OpenConnSQL()

            'CmdSQL.Transaction = CnSQL.BeginTransaction

            'SQLSQL = "select a.No_Faktur, a.User_ID as user_approve, b.Nama, b.Telepon, b.Email, e.Keterangan as Divisi, f.Keterangan as Jabatan, a.pin, a.Level_Hierarchy, a.Flag_Khusus "
            'SQLSQL = SQLSQL & "from HRIS_Transaksi_Rekrutmen_Approval a, Karyawan b, HRIS_Divisi_Sub_Divisi c, HRIS_Divisi e, HRIS_Jabatan f, HRIS_Rekrutmen_Karyawan g "
            'SQLSQL = SQLSQL & "where b.Kode_Perusahaan = f.Kode_Perusahaan and a.User_ID = b.Kode_Karyawan and b.ID_Divisi_Sub_Divisi=c.ID_Divisi_Sub_Divisi and a.No_Faktur=g.No_Faktur  "
            'SQLSQL = SQLSQL & "and c.ID_Divisi=e.ID_Divisi and b.ID_Level_Jabatan=f.ID_Jabatan "
            ''SQLSQL = SQLSQL & "and Acc='Y' "
            'SQLSQL = SQLSQL & "and Tolak is null and g.flag_kirim_email is null "
            ''SQLSQL = SQLSQL & "and a.No_Faktur='" & TxtNoFaktur.Text & "' "
            'SQLSQL = SQLSQL & "order by "
            'SQLSQL = SQLSQL & "CASE WHEN a.Flag_Khusus = 'Y' THEN 1 ELSE 0 END ASC, "
            'SQLSQL = SQLSQL & "a.Level_Hierarchy"



            'JGN UPA UBAH WHERE 
            'Kode Backup
            'SQLSQL = "select no_faktur, nama, status_karyawan from HRIS_Rekrutmen_Karyawan where kode_Perusahaan = '" & KodePerusahaan & "' and flag_kirim_email is null and No_Faktur='R241015008' order by tanggal"

            SQLSQL = "SELECT  a.no_faktur, a.nama, a.status_karyawan, d.Keterangan as golongan, f.Keterangan as posisi, g.Gaji_Pokok "
            SQLSQL = SQLSQL & "FROM HRIS_Rekrutmen_Karyawan a, HRIS_Level_Jabatan b, HRIS_Golongan_Sub_Golongan c, HRIS_Golongan d, HRIS_Divisi_Sub_Divisi e, HRIS_Divisi f, HRIS_Transaksi_Rekrutmen_Gaji g "
            SQLSQL = SQLSQL & "where a.Kode_Perusahaan=d.Kode_Perusahaan and a.Kode_Perusahaan = g.Kode_Perusahaan "
            SQLSQL = SQLSQL & "and a.ID_Level_Jabatan = b.ID_Level_Jabatan "
            SQLSQL = SQLSQL & "and b.ID_Golongan_Sub_Golongan = c.ID_Golongan_Sub_Golongan "
            SQLSQL = SQLSQL & "and c.ID_Golongan = d.ID_Golongan "
            SQLSQL = SQLSQL & "and a.ID_Divisi_Sub_Divisi = e. ID_Divisi_Sub_Divisi "
            SQLSQL = SQLSQL & "and e.ID_Divisi = f.ID_Divisi "
            SQLSQL = SQLSQL & "And a.No_Faktur = g.No_Faktur "
            SQLSQL = SQLSQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SQLSQL = SQLSQL & "and a.flag_kirim_email is null "
            SQLSQL = SQLSQL & "and a.No_Faktur='R241015008' "
            SQLSQL = SQLSQL & "order by a.tanggal"

            Using ds = BindingTrans(SQLSQL)
                If ds.Tables("MyTable").Rows.Count <> 0 Then
                    For ff As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
                        CmdSQL.Transaction = CnSQL.BeginTransaction

                        '=================================
                        '==    ADD APPROVAL I LOVE PDF  ==
                        '=================================

                        Dim lovePdfAPi = New LovePdfApi(PublicKey, SecretKey)

                        Dim task As SignTask = lovePdfAPi.CreateTask(Of SignTask)()

                        ' Tambahkan file PDF yang akan ditandatangani
                        Dim file = task.AddFile(FilePath)


                        Dim signParams = New SignParams()
                        signParams.SubjectSigner = "Approval C & B " & ds.Tables("MyTable").Rows(0).Item("no_faktur")
                        signParams.MessageSigner = "Approval C & B " & ds.Tables("MyTable").Rows(0).Item("no_faktur")

                        signParams.SignerReminderDaysCycle = 1
                        signParams.SignerReminders = True
                        signParams.ExpirationDays = 2

                        signParams.VerifyEnabled = True
                        signParams.LockOrder = True
                        signParams.UuidVisible = True

                        Dim positionAwalX As String = "120" 'Fix Position 
                        Dim positionAwalY As String = "-546" 'Fix Position 

                        Dim alreadyUserSubmission As Boolean = False
                        Dim alreadyAdd1User As Boolean = False
                        Dim BarisKedua As Boolean = False

                        SQLSQL = "select a.No_Faktur, a.User_ID as user_approve, b.Nama, b.Telepon, b.Email, e.Keterangan as Divisi, f.Keterangan as Jabatan, a.pin, a.Level_Hierarchy, a.Flag_Khusus "
                        SQLSQL = SQLSQL & "from HRIS_Transaksi_Rekrutmen_Approval a, Karyawan b, HRIS_Divisi_Sub_Divisi c, HRIS_Divisi e, HRIS_Jabatan f, HRIS_Rekrutmen_Karyawan g, HRIS_Level_Jabatan h "
                        SQLSQL = SQLSQL & "where "

                        SQLSQL = SQLSQL & "a.User_ID = b.UserID " 'HRIS_Transaksi_Rekrutmen_Approval = Karyawan
                        SQLSQL = SQLSQL & "and b.ID_Divisi_Sub_Divisi = c.ID_Divisi_Sub_Divisi " ' Karyawan = HRIS_Divisi_Sub_Divisi
                        SQLSQL = SQLSQL & "and a.No_Faktur = g.No_Faktur " 'HRIS_Transaksi_Rekrutmen_Approval = HRIS_Rekrutmen_Karyawan
                        SQLSQL = SQLSQL & "and c.ID_Divisi = e.ID_Divisi " ' HRIS_Divisi_Sub_Divisi = HRIS_Divisi
                        SQLSQL = SQLSQL & "and b.ID_Level_Jabatan = h.ID_Level_Jabatan " ' Karyawan = HRIS_Level_Jabatan
                        SQLSQL = SQLSQL & "and h.ID_Jabatan = f.ID_Jabatan " ' HRIS_Level_Jabatan = HRIS_Jabatan

                        'SQLSQL = SQLSQL & "and Acc='Y' "
                        SQLSQL = SQLSQL & "and g.flag_kirim_email is null "
                        SQLSQL = SQLSQL & "and a.No_Faktur = '" & ds.Tables("MyTable").Rows(ff).Item("no_faktur") & "' "
                        SQLSQL = SQLSQL & "order by "
                        SQLSQL = SQLSQL & "CASE WHEN a.Flag_Khusus = 'Y' THEN 1 ELSE 0 END ASC, "
                        SQLSQL = SQLSQL & "a.Level_Hierarchy"

                        Using Ds2 = BindingTransSQL(SQLSQL)
                            With Ds2.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For i As Integer = 0 To .Rows.Count - 1


                                        Dim user As String = .Rows(i).Item("user_approve")
                                        Dim nama As String = .Rows(i).Item("Nama")
                                        Dim email As String = .Rows(i).Item("Email")
                                        Dim jabatan As String = .Rows(i).Item("Jabatan") & " " & .Rows(i).Item("Divisi")
                                        Dim accessCode As String = .Rows(i).Item("pin")

                                        If Not IsValidEmail(email) Then
                                            CloseTransSQL()
                                            CloseConnSQL()
                                            MessageBox.Show("Terdapat Kesalahan Pada Email User Menyetujui!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        'CEK APAKAH BIG BOSS?
                                        If General_Class.CekNULL(.Rows(i).Item("Flag_khusus")) = "Y" Then
                                            Dim subArr As New List(Of String) From {
                                                    nama,
                                                    email,
                                                    jabatan,
                                                    accessCode
                                                }
                                            bigUser.Add(subArr)
                                            Continue For
                                        End If

                                        'If .Rows(i).Item("Level_Hierarchy") = "1" Then

                                        '    'ADD SIGN USER PENGAJUAN 
                                        '    If alreadyUserSubmission = False Then

                                        '        Dim signerPengajuan = signParams.AddSigner(nama, email)
                                        '        signerPengajuan.AccessCode = accessCode

                                        '        Dim signerFilePengajuan = signerPengajuan.AddFile(file.ServerFileName)

                                        '        ' Tambah elemen tanda tangan untuk signer pertama
                                        '        Dim signatureElement As SignatureElement = signerFilePengajuan.AddSignature()
                                        '        signatureElement.Position = New Position(positionAwalX, positionAwalY)
                                        '        signatureElement.Pages = "1"
                                        '        signatureElement.Size = 30

                                        '        'UNTUK PENAMBAHAN ELEMEN TEKS PENDEKUNG SEPERTI MENYETUJI, NAMA, JABATAN
                                        '        Dim generalTextElement As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText("Diajukan Oleh,")
                                        '        generalTextElement.Position = New Position(positionAwalX, (Val(positionAwalY) + 852).ToString)
                                        '        generalTextElement.Size = 17
                                        '        generalTextElement.Pages = "1"

                                        '        Dim generaElementNama As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText(nama)
                                        '        generaElementNama.Position = New Position(positionAwalX, (Val(positionAwalY) + 767).ToString)
                                        '        generaElementNama.Size = 15
                                        '        generaElementNama.Pages = "1"

                                        '        Dim generaElementJabatan As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText(jabatan)
                                        '        generaElementJabatan.Position = New Position(positionAwalX, (Val(positionAwalY) + 755).ToString) ' Atur posisi sesuai kebutuhan
                                        '        generaElementJabatan.Size = 15
                                        '        generaElementJabatan.Pages = "1"


                                        '        alreadyUserSubmission = True
                                        '        Continue For
                                        '    End If
                                        'End If


                                        If alreadyUserSubmission = False And alreadyAdd1User = False Then
                                            positionAwalX = "-20"
                                        End If

                                        positionAwalX = (Val(positionAwalX) + 140).ToString
                                        alreadyAdd1User = True

                                        Dim signer As Signer = signParams.AddSigner(nama, email)
                                        signer.AccessCode = accessCode

                                        Dim signerFile1 As SignerFile = signer.AddFile(file.ServerFileName)

                                        ' Tambah elemen tanda tangan untuk signer pertama
                                        Dim signatureElement1 As SignatureElement = signerFile1.AddSignature()
                                        signatureElement1.Position = New Position(positionAwalX, positionAwalY)
                                        signatureElement1.Pages = "1"
                                        signatureElement1.Size = 30

                                        'UNTUK PENAMBAHAN ELEMEN TEKS PENDEKUNG SEPERTI MENYETUJI, NAMA, JABATAN
                                        Dim generalTextElement1 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText("Menyetujui,")
                                        generalTextElement1.Position = New Position(positionAwalX, (Val(positionAwalY) + 852).ToString)
                                        generalTextElement1.Size = 17
                                        generalTextElement1.Pages = "1"

                                        Dim generalTextElement2 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(nama)
                                        generalTextElement2.Position = New Position(positionAwalX, (Val(positionAwalY) + 767).ToString) ' Atur posisi sesuai kebutuhan
                                        generalTextElement2.Size = 15
                                        generalTextElement2.Pages = "1"

                                        Dim generalTextElement3 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(jabatan)
                                        generalTextElement3.Position = New Position(positionAwalX, (Val(positionAwalY) + 755).ToString) ' Atur posisi sesuai kebutuhan
                                        generalTextElement3.Size = 15
                                        generalTextElement3.Pages = "1"


                                        'NO SURAT
                                        Dim NoSurat As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(ds.Tables("MyTable").Rows(0).Item("no_faktur"))
                                        NoSurat.Position = New Position("175", "676")
                                        NoSurat.Size = 15
                                        NoSurat.Pages = "1"

                                        Dim Dari As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(jabatan)
                                        Dari.Position = New Position("175", "648") ' Atur posisi sesuai kebutuhan
                                        Dari.Size = 15
                                        Dari.Pages = "1"

                                        Dim Perihal As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText("Approval C & B " & ds.Tables("MyTable").Rows(0).Item("no_faktur"))
                                        Perihal.Position = New Position("175", "634") ' Atur posisi sesuai kebutuhan
                                        Perihal.Size = 15
                                        Perihal.Pages = "1"

                                        Dim NamaKaryawan As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(ds.Tables("MyTable").Rows(0).Item("nama"))
                                        NamaKaryawan.Position = New Position("224", "557") ' Atur posisi sesuai kebutuhan
                                        NamaKaryawan.Size = 15
                                        NamaKaryawan.Pages = "1"

                                        Dim Posisi As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(ds.Tables("MyTable").Rows(0).Item("posisi"))
                                        Posisi.Position = New Position("224", "535") ' Atur posisi sesuai kebutuhan
                                        Posisi.Size = 15
                                        Posisi.Pages = "1"

                                        Dim Golongan As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(ds.Tables("MyTable").Rows(0).Item("golongan"))
                                        Golongan.Position = New Position("224", "515") ' Atur posisi sesuai kebutuhan
                                        Golongan.Size = 15
                                        Golongan.Pages = "1"

                                        Dim Status As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(ds.Tables("MyTable").Rows(0).Item("status_karyawan"))
                                        Status.Position = New Position("224", "495") ' Atur posisi sesuai kebutuhan
                                        Status.Size = 15
                                        Status.Pages = "1"

                                        Dim GajiPokok As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(Format(ds.Tables("MyTable").Rows(0).Item("Gaji_Pokok"), "N2"))
                                        GajiPokok.Position = New Position("224", "475") ' Atur posisi sesuai kebutuhan
                                        GajiPokok.Size = 15
                                        GajiPokok.Pages = "1"


                                        ''TUNJANGAN 
                                        Dim posisiXJabatan As String = "87"
                                        Dim posisiYJabatan As String = "475"
                                        Dim total As Double = 0
                                        SQLSQL = "select a.No_Faktur, a.Kode_Komponen, a.Tunjangan "
                                        SQLSQL = SQLSQL & "from HRIS_Transaksi_Rekrutmen_Tunjangan a "
                                        SQLSQL = SQLSQL & "where a.No_Faktur='" & ds.Tables("MyTable").Rows(0).Item("no_faktur") & "' "
                                        Using Ds3 = BindingTransSQL(SQLSQL)
                                            If Ds3.Tables("MyTable").Rows.Count <> 0 Then
                                                For fff As Integer = 0 To Ds3.Tables("MyTable").Rows.Count - 1

                                                    posisiYJabatan = (Val(posisiYJabatan) - 20).ToString

                                                    Dim JudulTunjangan As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(Ds3.Tables("MyTable").Rows(fff).Item("Kode_Komponen"))
                                                    JudulTunjangan.Position = New Position(posisiXJabatan, posisiYJabatan) ' Atur posisi sesuai kebutuhan
                                                    JudulTunjangan.Size = 15
                                                    JudulTunjangan.Pages = "1"

                                                    Dim Titik2 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(":")
                                                    Titik2.Position = New Position((Val(posisiXJabatan) + 129).ToString, posisiYJabatan) ' Atur posisi sesuai kebutuhan
                                                    Titik2.Size = 15
                                                    Titik2.Pages = "1"

                                                    Dim NominalTunjangan As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(Format(Ds3.Tables("MyTable").Rows(fff).Item("Tunjangan"), "N2"))
                                                    NominalTunjangan.Position = New Position((Val(posisiXJabatan) + 137).ToString, posisiYJabatan) ' Atur posisi sesuai kebutuhan
                                                    NominalTunjangan.Size = 15
                                                    NominalTunjangan.Pages = "1"

                                                    total = total + Val(Ds3.Tables("MyTable").Rows(fff).Item("Tunjangan"))
                                                Next

                                                'TOTAL TUNJANGAN
                                                Dim LabelTotal As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText("Total")
                                                LabelTotal.Position = New Position(posisiXJabatan, (Val(posisiYJabatan) - 20).ToString) ' Atur posisi sesuai kebutuhan
                                                LabelTotal.Size = 15
                                                LabelTotal.Pages = "1"

                                                Dim Titik3 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(":")
                                                Titik3.Position = New Position((Val(posisiXJabatan) + 129).ToString, (Val(posisiYJabatan) - 20).ToString) ' Atur posisi sesuai kebutuhan
                                                Titik3.Size = 15
                                                Titik3.Pages = "1"

                                                Dim TotalTunjangan As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(Format(total, "N2"))
                                                TotalTunjangan.Position = New Position((Val(posisiXJabatan) + 137).ToString, (Val(posisiYJabatan) - 20).ToString) ' Atur posisi sesuai kebutuhan
                                                TotalTunjangan.Size = 15
                                                TotalTunjangan.Pages = "1"
                                            End If
                                        End Using


                                        If positionAwalX >= "400" Then
                                            positionAwalX = "-20"
                                            positionAwalY = "-680"
                                            BarisKedua = True
                                        End If


                                    Next


                                    'Add Sign Big Boss jika ada
                                    'If bigUser.Count > 0 Then
                                    '    If BarisKedua = False Then positionAwalX = "-20" : positionAwalY = "-680"
                                    '    For j As Integer = 0 To bigUser.Count - 1

                                    '        positionAwalX = (Val(positionAwalX) + 140).ToString

                                    '        Dim jabatanBig As String = bigUser(j)(2).ToString
                                    '        Dim emailBig As String = bigUser(j)(1).ToString
                                    '        Dim namaBig As String = bigUser(j)(0).ToString
                                    '        Dim AccessCodeBig As String = bigUser(j)(3).ToString

                                    '        Dim signerBig = signParams.AddSigner(namaBig, emailBig)
                                    '        signerBig.AccessCode = AccessCodeBig

                                    '        Dim signerFileBig = signerBig.AddFile(file.ServerFileName)

                                    '        ' Tambah elemen tanda tangan untuk signer pertama
                                    '        Dim signatureElement4 As SignatureElement = signerFileBig.AddSignature()
                                    '        signatureElement4.Position = New Position(positionAwalX, positionAwalY)
                                    '        signatureElement4.Pages = "1"
                                    '        signatureElement4.Size = 30

                                    '        'UNTUK PENAMBAHAN ELEMEN TEKS PENDEKUNG SEPERTI MENYETUJI, NAMA, JABATAN
                                    '        Dim generalTextElement4 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFileBig.AddText("Menyetujui,")
                                    '        generalTextElement4.Position = New Position(positionAwalX, (Val(positionAwalY) + 852).ToString)
                                    '        generalTextElement4.Size = 17
                                    '        generalTextElement4.Pages = "1"

                                    '        Dim generaElementNama4 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFileBig.AddText(namaBig)
                                    '        generaElementNama4.Position = New Position(positionAwalX, (Val(positionAwalY) + 767).ToString)
                                    '        generaElementNama4.Size = 15
                                    '        generaElementNama4.Pages = "1"

                                    '        Dim generaElementJabatan4 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFileBig.AddText(jabatanBig)
                                    '        generaElementJabatan4.Position = New Position(positionAwalX, (Val(positionAwalY) + 755).ToString) ' Atur posisi sesuai kebutuhan
                                    '        generaElementJabatan4.Size = 15
                                    '        generaElementJabatan4.Pages = "1"



                                    '    Next
                                    '    bigUser.Clear()
                                    'End If
                                Else
                                    'CloseTransSQL()
                                    'CloseConnSQL()
                                    'Exit Sub
                                    Continue For
                                End If
                            End With

                        End Using

                        Dim signatureResponse As SignatureResponse = Await task.RequestSignatureAsync(signParams)

                        ' Periksa apakah permintaan tanda tangan berhasil
                        If signatureResponse Is Nothing Then

                            CloseTransSQL()
                            CloseConnSQL()
                            MessageBox.Show("Permintaan tanda tangan gagal.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If


                        'UPDATE FLAG KIRIM EMAIL
                        'SQLSQL = "update HRIS_Rekrutmen_Karyawan set flag_kirim_email = 'Y', tgl_email = '" & Format(tgl_skg, "yyyy-MM-dd") & "', jam_email = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                        'SQLSQL = SQLSQL & "uuid = '" & signatureResponse.Uuid & "', token_requester = '" & signatureResponse.TokenRequester & "' "
                        'SQLSQL = SQLSQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & ds.Tables("MyTable").Rows(ff).Item("No_Faktur") & "' "
                        'ExecuteTransSQL(SQLSQL)

                        CmdSQL.Transaction.Commit()
                    Next 'tutup ff
                End If

            End Using


            'CmdSQL.Transaction.Commit()
            'CloseTransSQL()
            CloseConnSQL()
        Catch ex As Exception

            CloseTransSQL()
            CloseConnSQL()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Function IsValidEmail(email As String) As Boolean
        ' Pola RegEx untuk memvalidasi format email
        Dim pattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Dim regex As New Regex(pattern)

        ' Mengembalikan true jika format email valid, false jika tidak
        Return regex.IsMatch(email)
    End Function


End Class
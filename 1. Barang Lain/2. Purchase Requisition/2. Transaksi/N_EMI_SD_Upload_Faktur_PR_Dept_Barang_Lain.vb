Imports System.IO
Imports System.Security.Policy
Imports System.Web

Public Class N_EMI_SD_Upload_Faktur_PR_Dept_Barang_Lain

    Public nopr As String
    Public lokasi As String
    Private SelectedFilePath As String
    Dim enablePraRelease As Boolean = False
    Private Sub N_EMI_SD_Upload_Faktur_PR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label4.AutoSize = False
        Label4.Width = 200
        Label4.Height = 100
        Label4.TextAlign = ContentAlignment.MiddleCenter

        Label2.AutoSize = False
        Label2.Width = 200
        Label2.Height = 40
        Label2.TextAlign = ContentAlignment.MiddleCenter

        Label2.Text = nopr


        Label4.Text = "Pilih File untuk di upload"

        Button1.Enabled = False
        btnClear.Visible = False



        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = $"
            select 1 from N_EMI_Purchase_Requisition_Barang_Lain_Departement a
                join N_EMI_Master_Kategori_Gudang_Barang_Lain b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Kategori_Gudang = b.Kode_Kategori_Gudang
                where b.Flag_Kirim_Web IS NULL AND a.No_Faktur = '{nopr}' AND a.Kode_Perusahaan = '{KodePerusahaan}'
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    enablePraRelease = True
                Else
                    Dr.Close()
                    enablePraRelease = False
                End If
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub
    Private Sub LabelNamaFile_Click(sender As Object, e As EventArgs) Handles Label4.Click
        If SelectedFilePath <> "" AndAlso IO.File.Exists(SelectedFilePath) Then
            Process.Start(New ProcessStartInfo(SelectedFilePath) With {.UseShellExecute = True})
        Else
            MessageBox.Show("File tidak ditemukan")
        End If
    End Sub
    Sub get_data()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "PDF Files (*.pdf)|*.pdf"
            ofd.Title = "Pilih file PDF"

            If ofd.ShowDialog() = DialogResult.OK Then

                ' Validasi ekstensi
                Dim ext = System.IO.Path.GetExtension(ofd.FileName).ToLower()
                If ext <> ".pdf" Then
                    MessageBox.Show("File harus PDF")
                    Exit Sub
                End If

                ' Validasi ukuran (max 3 MB)
                Dim fi As New System.IO.FileInfo(ofd.FileName)
                If fi.Length > 3 * 1024 * 1024 Then
                    MessageBox.Show("Ukuran file maksimal 3 MB")
                    Exit Sub
                End If

                ' Simpan path
                SelectedFilePath = ofd.FileName

                ' Tampilkan nama file ke label
                Label4.Text = System.IO.Path.GetFileName(ofd.FileName)
                Button1.Enabled = True
                btnClear.Visible = True


            End If
        End Using

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        clear()

    End Sub

    Sub clear()
        SelectedFilePath = ""
        Label4.Text = "Pilih File untuk di upload"
        Button1.Enabled = False
        btnClear.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If SelectedFilePath = "" Then
            MessageBox.Show("Silahkan upload file untuk melanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        get_jam()

        Dim url As String = ""
        Dim namaFileAzure As String = ""

        Try
            OpenConn()
            OpenConnMySQL()
            Cmd.Transaction = Cn.BeginTransaction
            CmdMySQL.Transaction = CnMySQL.BeginTransaction

            Dim gudang_whs As String = ""

            SQL = "select kode_kategori_gudang,status, Flag_Release from N_EMI_Purchase_Requisition_Barang_Lain_Departement where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & nopr & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("status")) <> "" Then
                        CloseTransMySQL()
                        CloseConnMySQL()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No PR Sudah dibatalkan sebelumnya, silahkan refresh data!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    If General_Class.CekNULL(Dr("kode_kategori_gudang")) = "" Then
                        gudang_whs = ""
                    Else
                        gudang_whs = Dr("kode_kategori_gudang")
                    End If
                Else
                    CloseTransMySQL()
                    CloseConnMySQL()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pr tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            Dim folderName As String = Format(tgl_skg, "yyyy") & "/" & Format(tgl_skg, "MMM")

            namaFileAzure =
                 gudang_whs & "/" & folderName & "/" & Path.GetFileNameWithoutExtension(Label4.Text) &
                "_" & Format(tgl_skg, "dd-MM-yyyy_HH-mm-ss") &
                "_" & nopr &
                ".pdf"



            Dim result As (Success As Boolean, Message As String, Url As String)

            Using fs As New FileStream(SelectedFilePath, FileMode.Open, FileAccess.Read)
                result = AzureHelper_EMI.UploadToAzure(
                container_pr_dept_barang_lain,
                namaFileAzure,
                fs
            )
            End Using

            If Not result.Success Then

                CloseTransMySQL()
                CloseConnMySQL()
                CloseTrans()
                CloseConn()

                MessageBox.Show(
                "Upload ke Azure gagal." & vbCrLf & result.Message,
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )


                Exit Sub
            End If

            url = result.Url


            SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Attachment "
            SQL = SQL & "(Kode_Perusahaan,No_Faktur,Tanggal,jam,Userid,Path_File,Container_File)  values ( "
            SQL = SQL & "'" & KodePerusahaan & "', '" & nopr & "' , '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:ii:ss") & "', '" & UserID & "' ,"
            SQL = SQL & "'" & namaFileAzure & "' ,'" & container_pr_dept_barang_lain & "')"
            ExecuteTrans(SQL)


            Dim Tanggal As String = Format(tgl_skg, "yyyy-MM-dd")
            Dim Jam As String = Format(tgl_skg, "HH:mm:ss")

            SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set flag_release = 'Y', "
            SQL = SQL & "tanggal_release = '" & Tanggal & "', "
            SQL = SQL & "Jam_Release = '" & Jam & "',"
            SQL = SQL & "User_Release = '" & UserID & "' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & nopr & "'"
            ExecuteTrans(SQL)

            If Not enablePraRelease Then
                SQLMySQL = $"UPDATE purchase_requisition_barang_lain_departement
                    SET Flag_Release = 'Y', Tanggal_Release = '{Tanggal}', Jam_Release = '{Jam}', User_Release = '{UserID}'
                    WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Faktur = '{nopr}'
                "
                ExecuteTransMySQL(SQLMySQL)
            End If


            CmdMySQL.Transaction.Commit()
            Cmd.Transaction.Commit()

            CloseConnMySQL()
            CloseConn()
            MessageBox.Show("Berhasil upload data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            clear()
            N_EMI_Purchase_Requsition_Departement_Attachment_Barang_Lain.kosong()
            Me.Close()
        Catch ex As Exception
            ' 🔥 ROLLBACK FILE AZURE
            If url <> "" And namaFileAzure <> "" Then
                AzureHelper_EMI.DeleteFromAzure(
                container_pr_dept_barang_lain,
                namaFileAzure
            )
            End If

            CloseTransMySQL()
            CloseTrans()
            CloseConnMySQL()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class
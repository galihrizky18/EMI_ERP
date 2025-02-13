Imports System.Diagnostics.Eventing.Reader
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Master_Kategori_PO_Role
    Dim arrcari, arrDivisiMesin As New ArrayList
    Dim Jenis = "Master_Mesin"

    Dim LvKategori As String
    Dim LvID_Kategori As String

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvKategori = Lv_Data_MasterMesin.Items(No_Index).Text
        LvID_Kategori = Lv_Data_MasterMesin.Items(No_Index).SubItems(1).Text
    End Sub
    Private Sub Master_Role_Kategori_PO_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Role_Kategori_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Btn_Simpan.Tag = "&Simpan"
            Btn_Hapus.Enabled = False

            'Lbl_Judul.Text = Base_Language.Lang_Mesin_Judul

            Lv_Data_MasterMesin.Columns.Clear()
            Lv_Data_MasterMesin.Columns.Add("Kategori PO", 450, HorizontalAlignment.Left)
            Lv_Data_MasterMesin.Columns.Add("Id_kategori", 0, HorizontalAlignment.Left)
            Lv_Data_MasterMesin.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

        kosong()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        If Cmb_Divisi.Text.Trim.Length = 0 Then
            MessageBox.Show("Pilih dahulu users yang mau dihapus......!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If Hapus1 = vbYes Then
                SQL = "delete from Emi_Role_Kategori_PO where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & Cmb_Divisi.Text & "'"
                ExecuteTrans(SQL)
            Else
                MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Cmb_Divisi.Text.Trim.Length = 0 Then
            MessageBox.Show("User belum diisi......!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim fInsert As Boolean = False
        For a As Integer = 0 To Lv_Data_MasterMesin.Items.Count - 1
            If Lv_Data_MasterMesin.Items(a).Checked = True Then
                fInsert = True
                Exit For
            Else
                fInsert = False
            End If
        Next

        If fInsert = False Then
            MessageBox.Show("Pilih dahulu data yang mau disimpan....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag = "&Simpan" Then
                For a As Integer = 0 To Lv_Data_MasterMesin.Items.Count - 1
                    Get_Isi_Listview(a)
                    If Lv_Data_MasterMesin.Items(a).Checked = True Then
                        SQL = "insert into Emi_Role_Kategori_PO(Kode_Perusahaan,UserID,Kategori_PO) values("
                        SQL = SQL & "'" & KodePerusahaan & "','" & Cmb_Divisi.Text & "','" & LvID_Kategori & "')"
                        ExecuteTrans(SQL)
                    End If
                Next
            Else
                SQL = "delete from Emi_Role_Kategori_PO where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & Cmb_Divisi.Text & "'"
                ExecuteTrans(SQL)

                For a As Integer = 0 To Lv_Data_MasterMesin.Items.Count - 1
                    Get_Isi_Listview(a)
                    If Lv_Data_MasterMesin.Items(a).Checked = True Then
                        SQL = "insert into Emi_Role_Kategori_PO(Kode_Perusahaan,UserID,Kategori_PO) values("
                        SQL = SQL & "'" & KodePerusahaan & "','" & Cmb_Divisi.Text & "','" & LvID_Kategori & "')"
                        ExecuteTrans(SQL)
                    End If
                Next
            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub kosong()
        Cmb_Divisi.SelectedIndex = -1

        Try
            OpenConn()

            Lv_Data_MasterMesin.Items.Clear()
            SQL = "select Id_Kategori_PO, Keterangan from EMI_Kategori_PO "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "Order By Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Data_MasterMesin.Items.Add(Dr("Keterangan"))
                    lvw.SubItems.Add(Dr("Id_Kategori_PO"))
                Loop
            End Using

            For a As Integer = 0 To Lv_Data_MasterMesin.Items.Count - 1
                Lv_Data_MasterMesin.Items(a).Checked = False
            Next

            Cmb_Divisi.Items.Clear() : Cmb_Divisi.Focus()
            SQL = "select UserID from Users where Kode_Perusahaan = '" & KodePerusahaan & "' order by UserID "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Divisi.Items.Add(Dr("UserID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Simpan.Tag = "&Simpan"
        Btn_Simpan.Enabled = True : Btn_Hapus.Enabled = False

    End Sub

    Private Sub Cmb_Divisi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Divisi.SelectedIndexChanged
        If Cmb_Divisi.Text.Trim.Length = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            For a As Integer = 0 To Lv_Data_MasterMesin.Items.Count - 1
                Lv_Data_MasterMesin.Items(a).Checked = False
            Next
            Dim ada_data As String = ""
            SQL = "select Kategori_PO from Emi_Role_Kategori_PO where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and UserID = '" & Cmb_Divisi.Text & "' order by Kategori_PO "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    For a As Integer = 0 To Lv_Data_MasterMesin.Items.Count - 1
                        ada_data = "Y"
                        Get_Isi_Listview(a)
                        If Dr("Kategori_PO") = LvID_Kategori Then
                            Lv_Data_MasterMesin.Items(a).Checked = True

                            Exit For
                        End If
                    Next
                Loop
            End Using
            If ada_data = "Y" Then
                Btn_Simpan.Tag = "&Update"
                Btn_Hapus.Enabled = True
            Else
                Btn_Simpan.Tag = "&Simpan"
                Btn_Hapus.Enabled = False
            End If
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class EMI_Syarat_Produksi_Display
    Dim arrcari As New ArrayList
    Dim Jenis = "Master_Jenis_Hewan"

    Private Sub TabPage2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel19_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel27_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ListView3_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Master_Gudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Rows.Add(1)
        DataGridView1.Rows(0).Cells(0).Value = "PO-150124/00012"
        DataGridView1.Rows(0).Cells(1).Value = "PT. MULTI FOOD"
        DataGridView1.Rows(0).Cells(2).Value = "PR00015"
        DataGridView1.Rows(0).Cells(3).Value = "LIFE CAT KITTEN TUNA 85GR"
        DataGridView1.Rows(0).Cells(4).Value = "DALAM PEMESANAN"
        DataGridView1.Rows(0).Cells(4).Style.BackColor = Color.Yellow
        DataGridView1.Rows(0).Cells(5).Value = "DALAM PEMESANAN"
        DataGridView1.Rows(0).Cells(5).Style.BackColor = Color.Yellow
        DataGridView1.Rows(0).Cells(6).Value = ""

        DataGridView1.Rows.Add(1)
        DataGridView1.Rows(1).Cells(0).Value = "PO-150124/00013"
        DataGridView1.Rows(1).Cells(1).Value = "MIAW FOOD"
        DataGridView1.Rows(1).Cells(2).Value = "PR00048"
        DataGridView1.Rows(1).Cells(3).Value = "MIAW ALL STAGE CHICKEN 400GR"
        DataGridView1.Rows(1).Cells(4).Value = "COMPLETE"
        DataGridView1.Rows(1).Cells(4).Style.BackColor = Color.LightGreen
        DataGridView1.Rows(1).Cells(5).Value = "DALAM PEMESANAN"
        DataGridView1.Rows(1).Cells(5).Style.BackColor = Color.Yellow
        DataGridView1.Rows(1).Cells(6).Value = ""

        DataGridView1.Rows.Add(1)
        DataGridView1.Rows(2).Cells(0).Value = "PO-150124/00027"
        DataGridView1.Rows(2).Cells(1).Value = "MIAW FOOD"
        DataGridView1.Rows(2).Cells(2).Value = "PR00052"
        DataGridView1.Rows(2).Cells(3).Value = "MIAW ADULT CHICKEN TUNA 400GR"
        DataGridView1.Rows(2).Cells(4).Value = "COMPLETE"
        DataGridView1.Rows(2).Cells(4).Style.BackColor = Color.LightGreen
        DataGridView1.Rows(2).Cells(5).Value = "COMPLETE"
        DataGridView1.Rows(2).Cells(5).Style.BackColor = Color.LightGreen
        DataGridView1.Rows(2).Cells(6).Value = "DALAM PROSES"
        DataGridView1.Rows(2).Cells(6).Style.BackColor = Color.Yellow

        DataGridView1.Rows.Add(1)
        DataGridView1.Rows(3).Cells(0).Value = "PO-150124/00039"
        DataGridView1.Rows(3).Cells(1).Value = "PT. MULTI FOOD"
        DataGridView1.Rows(3).Cells(2).Value = "PR00021"
        DataGridView1.Rows(3).Cells(3).Value = "LIFE DOG ALL STAGE LAMB 400GR"
        DataGridView1.Rows(3).Cells(4).Value = "NOT READY"
        DataGridView1.Rows(3).Cells(4).Style.BackColor = Color.OrangeRed
        DataGridView1.Rows(3).Cells(5).Value = "NOT READY"
        DataGridView1.Rows(3).Cells(5).Style.BackColor = Color.OrangeRed
        DataGridView1.Rows(3).Cells(6).Value = ""
        'DataGridView1.Rows(3).Cells(6).Style.BackColor = Color.White

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
    ''Private Sub kosong()
    ''    TextBox1.Text = ""
    ''    TextBox2.Text = ""



    ''    ComboBox1.Items.Clear() : arrcari.Clear()
    ''    ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Kode) : arrcari.Add("kode_jenis_hewan")
    ''    ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Keterangan) : arrcari.Add("keterangan")
    ''    TextBox3.Text = ""

    ''    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
    ''    Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
    ''    'Btn_Cari.Text = Base_Language.Lang_Global_Cari
    ''    Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
    ''    Btn_Simpan.Tag = "&Simpan"
    ''    Btn_Hapus.Enabled = False

    ''End Sub

    ''Private Sub Cari(ByVal semua As String)
    ''    Try

    ''        OpenConn()

    ''        ListView1.Items.Clear()
    ''        SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan where kode_perusahaan = '" & KodePerusahaan & "' "
    ''        If semua = "T" Then
    ''            SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
    ''            SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
    ''        Else
    ''            SQL = SQL & "order by nama"
    ''        End If
    ''        Using dr = OpenTrans(SQL)
    ''            Do While dr.Read
    ''                Dim Lvw As ListViewItem
    ''                Lvw = ListView1.Items.Add(dr("kode_jenis_hewan"))
    ''                Lvw.SubItems.Add(dr("keterangan"))
    ''            Loop
    ''        End Using

    ''        CloseConn()

    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub
    ''Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    ''    My.Application.ChangeCulture("en-us")
    ''    My.Application.ChangeUICulture("en-us")
    ''End Sub

    ''Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ''    My.Application.ChangeCulture("en-us")
    ''    My.Application.ChangeUICulture("en-us")

    ''    Try
    ''        OpenConn()

    ''        Base_Language.Get_Languages_Global(Bahasa_Pilihan)

    ''        Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

    ''        Label1.Text = Base_Language.Lang_Jenis_Hewan_Judul
    ''        Label2.Text = Base_Language.Lang_Jenis_Hewan_Kode
    ''        Label3.Text = Base_Language.Lang_Jenis_Hewan_Keterangan
    ''        Label4.Text = Base_Language.Lang_Jenis_Hewan_Kolom

    ''        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Kode, 150, HorizontalAlignment.Left)
    ''        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Keterangan, 725, HorizontalAlignment.Left)
    ''        ListView1.View = View.Details

    ''        kosong()

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub

    ''    End Try


    ''End Sub

    ''Private Sub TextBox1_Leave(sender As Object, e As EventArgs)
    ''    If TextBox1.Text.Trim.Length = 0 Then Exit Sub

    ''    Try

    ''        OpenConn()

    ''        SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan Where "
    ''        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
    ''        SQL = SQL & "kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    ''        Using Dr = OpenTrans(SQL)
    ''            If Dr.Read Then
    ''                TextBox1.Text = Dr("kode_jenis_hewan")
    ''                TextBox2.Text = Dr("keterangan")

    ''                Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
    ''                Btn_Simpan.Tag = "&Update"
    ''            Else
    ''                TextBox2.Text = ""

    ''                Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
    ''                Btn_Simpan.Tag = "&Simpan"
    ''            End If
    ''        End Using

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub

    ''Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs)
    ''    If TextBox1.Text.Trim.Length = 0 Then
    ''        MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Kode, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        TextBox1.Focus() : Exit Sub
    ''    ElseIf TextBox2.Text.Trim.Length = 0 Then
    ''        MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Nama, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        TextBox2.Focus() : Exit Sub
    ''    End If

    ''    Try

    ''        OpenConn()

    ''        Cmd.Transaction = Cn.BeginTransaction

    ''        If Btn_Simpan.Tag = "&Simpan" Then
    ''            SQL = "Insert Into emi_jenis_hewan(Kode_Perusahaan, kode_jenis_hewan, keterangan) "
    ''            SQL = SQL & "Values('" & KodePerusahaan & "', "
    ''            SQL = SQL & "'" & TextBox1.Text.Trim & "', '" & TextBox2.Text.Trim & "')"
    ''            ExecuteTrans(SQL)
    ''        Else
    ''            SQL = "Update emi_jenis_hewan Set keterangan = '" & TextBox2.Text.Trim & "' "
    ''            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    ''            ExecuteTrans(SQL)
    ''        End If

    ''        Cmd.Transaction.Commit()

    ''        CloseConn()

    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try

    ''    kosong()
    ''    TextBox1.Focus()
    ''End Sub

    ''Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs)
    ''    Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    ''    If Hapus1 = vbYes Then

    ''        Try

    ''            OpenConn()

    ''            Cmd.Transaction = Cn.BeginTransaction

    ''            SQL = "Delete From emi_jenis_hewan where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    ''            ExecuteTrans(SQL)

    ''            Cmd.Transaction.Commit()

    ''            CloseConn()
    ''        Catch ex As Exception
    ''            CloseTrans()
    ''            CloseConn()
    ''            MessageBox.Show(ex.Message)
    ''            Exit Sub
    ''        End Try

    ''    Else
    ''        MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''    End If

    ''    kosong()
    ''    TextBox1.Focus()
    ''End Sub

    ''    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
    ''        kosong()
    ''    End Sub

    ''    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    ''    End Sub

    ''    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    ''    End Sub

    ''    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    ''    End Sub

    ''    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    ''    End Sub

    ''    Private Sub TabPage4_Click(sender As Object, e As EventArgs) Handles TabPage4.Click

    ''    End Sub

    ''    Private Sub Label35_Click(sender As Object, e As EventArgs)

    ''    End Sub

    ''    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    ''    End Sub

    ''    Private Sub TabPage6_Click(sender As Object, e As EventArgs) Handles TabPage6.Click

    ''    End Sub

    ''    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    ''    End Sub

    ''    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click

    ''    End Sub
End Class
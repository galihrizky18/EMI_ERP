Public Class N_EMI_Master_Kategori_Jenis_Sub_Kategori
    Dim arrcari, arrid, arrcmbsatuan, arrcari2, arrkateogori As New ArrayList
    Dim arrid2, arridsub2 As New ArrayList
    Dim arrid3, arridsub3, arrid2sub3 As New ArrayList
    Public arrid4, arridsub4, arrid2sub4, arrid3sub4 As New ArrayList
    Dim arrcari3, arrkateogori3, arrsubkateogori3 As New ArrayList
    Dim arrcari4, arrkateogori4, arrsubkateogori4, arrsub1kateogori4 As New ArrayList
    Public arrcari5, arrkateogori5, arrsubkateogori5, arrsub1kateogori5, arrsub2kateogori5 As New ArrayList
    Dim xid_kategori As String
    Dim xid_sub_kategori As String
    Dim xid_sub_kategori1 As String
    Dim xid_sub_kategori2 As String
    Dim xid_sub_kategori3 As String
    Dim xprefix1, xprefix2, xprefix3, xprefix4, xprefix5 As String
    Public Asal_proses As String
    Public xurut_departement, xid_cost, xid_gedung, xlink As String



    Private Sub get_no_prefix()
        SQL = "SELECT RIGHT('0' + CAST(ISNULL(MAX(CAST(Prefix AS INT)), 0) + 1 AS VARCHAR(1)), 1) AS NextPrefix "
        SQL = SQL & "FROM N_EMI_Master_Kategori_Jenis WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                xprefix1 = Dr("NextPrefix")
            End If
        End Using
    End Sub

    Private Sub get_no_prefix2()
        SQL = "SELECT RIGHT('00' + CAST(ISNULL(MAX(CAST(Prefix AS INT)), 0) + 1 AS VARCHAR(2)), 2) AS NextPrefix "
        SQL = SQL & "FROM N_EMI_Master_Sub_Kategori_Jenis WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "AND Id_Kategori_Jenis = '" & arrid.Item(ComboBox1.SelectedIndex) & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                xprefix2 = Dr("NextPrefix")
            End If
        End Using
    End Sub

    Private Sub get_no_prefix3()
        SQL = "SELECT RIGHT('00' + CAST(ISNULL(MAX(CAST(Prefix AS INT)), 0) + 1 AS VARCHAR(2)), 2) AS NextPrefix "
        SQL = SQL & "FROM N_EMI_Master_Sub_Kategori_Jenis_1 WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "AND Id_Sub_Kategori_Jenis = '" & arridsub2.Item(ComboBox4.SelectedIndex) & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                xprefix3 = Dr("NextPrefix")
            End If
        End Using
    End Sub

    Private Sub get_no_prefix4()
        SQL = "SELECT RIGHT('00' + CAST(ISNULL(MAX(CAST(Prefix AS INT)), 0) + 1 AS VARCHAR(2)), 2) AS NextPrefix "
        SQL = SQL & "FROM N_EMI_Master_Sub_Kategori_Jenis_2 WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "AND Id_Sub_Kategori_Jenis_1 = '" & arrid2sub3.Item(ComboBox8.SelectedIndex) & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                xprefix4 = Dr("NextPrefix")
            End If
        End Using
    End Sub

    Private Sub get_no_prefix5()
        SQL = "SELECT RIGHT('000' + CAST(ISNULL(MAX(CAST(Prefix AS INT)), 0) + 1 AS VARCHAR(3)), 3) AS NextPrefix "
        SQL = SQL & "FROM N_EMI_Master_Sub_Kategori_Jenis_3 WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "AND Id_Sub_Kategori_Jenis_2 = '" & arrid3sub4.Item(CmbSK3_JenisSub2.SelectedIndex) & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                xprefix5 = Dr("NextPrefix")
            End If
        End Using
    End Sub
    Private Sub BtnSatuan_Cari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
        If CmbSatuan_Kolom.Text.Trim.Length = 0 Then Exit Sub
        If TxtSatuan_Value.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select Kode_Kategori_Jenis, Keterangan, Prefix, Id_Kategori_Jenis, Flag_Aktif from N_EMI_Master_Kategori_Jenis "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " "
            Else
                SQL = SQL & "order by Keterangan"
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    Lvw.SubItems.Add(dr("Prefix"))
                    Lvw.SubItems.Add(dr("Flag_Aktif"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cari2(ByVal semua As String)
        Try
            OpenConn()

            ListView2.Items.Clear()
            SQL = "select b.Kode_Kategori_Jenis, b.Keterangan as Kategori_Jenis, a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "a.Prefix, a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari2.Item(ComboBox2.SelectedIndex) & " like '%" & TextBox5.Text & "%' "
                SQL = SQL & "order by " & arrcari2.Item(ComboBox2.SelectedIndex) & " "
            Else
                SQL = SQL & "order by a.Keterangan "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Prefix"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cari3(ByVal semua As String)
        Try
            OpenConn()

            ListView3.Items.Clear()
            SQL = "select  c.Kode_Kategori_Jenis, c.Keterangan as Kategori_Jenis, b.Kode_Sub_Kategori_Jenis, b.Keterangan as Sub_Kategori_Jenis,"
            SQL = SQL & "a.Kode_Sub_Kategori_Jenis_1, a.Keterangan as Sub_Kategori_Jenis1, a.Prefix, "
            SQL = SQL & "a.Id_Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_1 a, N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari3.Item(ComboBox5.SelectedIndex) & " like '%" & TextBox11.Text & "%' "
                SQL = SQL & "order by " & arrcari3.Item(ComboBox5.SelectedIndex) & " "
            Else
                SQL = SQL & "order by a.Keterangan "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView3.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis1"))
                    Lvw.SubItems.Add(dr("Prefix"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cari4(ByVal semua As String)
        Try
            OpenConn()

            ListView4.Items.Clear()
            SQL = "select d.Kode_Kategori_Jenis, d.Keterangan as Kategori_Jenis, c.Kode_Sub_Kategori_Jenis, c.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "b.Kode_Sub_Kategori_Jenis_1, b.Keterangan as Sub_Kategori_Jenis_1, a.Kode_Sub_Kategori_Jenis_2, a.Keterangan as Sub_Kategori_Jenis_2, a.Prefix, "
            SQL = SQL & "a.Id_Sub_Kategori_Jenis_2, a.Id_Sub_Kategori_Jenis_1, b.Id_Sub_Kategori_Jenis, c.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_2 a, N_EMI_Master_Sub_Kategori_Jenis_1 b, "
            SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis c, N_EMI_Master_Kategori_Jenis d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_1 = b.Id_Sub_Kategori_Jenis_1 "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Jenis = d.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari4.Item(ComboBox9.SelectedIndex) & " like '%" & TextBox15.Text & "%' "
                SQL = SQL & "order by " & arrcari4.Item(ComboBox9.SelectedIndex) & " "
            Else
                SQL = SQL & "order by a.Keterangan "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView4.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Prefix"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cari5(ByVal semua As String)
        Try
            OpenConn()

            ListView5.Items.Clear()
            SQL = "select e.Kode_Kategori_Jenis, e.Keterangan as Kategori_Jenis, d.Kode_Sub_Kategori_Jenis, d.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "c.Kode_Sub_Kategori_Jenis_1, c.Keterangan as Sub_Kategori_Jenis_1, b.Kode_Sub_Kategori_Jenis_2, b.Keterangan as Sub_Kategori_Jenis_2, "
            SQL = SQL & "a.Kode_Sub_Kategori_Jenis_3, a.Keterangan as Sub_Kategori_Jenis_3, a.Prefix, "
            SQL = SQL & "a.Id_Sub_Kategori_Jenis_3, a.Id_Sub_Kategori_Jenis_2, b.Id_Sub_Kategori_Jenis_1, c.Id_Sub_Kategori_Jenis, d.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a, N_EMI_Master_Sub_Kategori_Jenis_2 b, N_EMI_Master_Sub_Kategori_Jenis_1 c, "
            SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis d, N_EMI_Master_Kategori_Jenis e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_2 = b.Id_Sub_Kategori_Jenis_2 "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis_1 = c.Id_Sub_Kategori_Jenis_1 "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari5.Item(ComboBox15.SelectedIndex) & " like '%" & TextBox19.Text & "%' "

            Else
                SQL = SQL & "order by a.Keterangan "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView5.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_3"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_3"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_3"))
                    Lvw.SubItems.Add(dr("Prefix"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub N_EMI_Master_Kategori_Jenis_Sub_Kategori_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView1.Columns.Add("id", 0, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Keterangan", 400, HorizontalAlignment.Left)
        ListView1.Columns.Add("Prefix", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Flag Aktif", 110, HorizontalAlignment.Left)

        ListView2.Columns.Add("id Kategori", 0, HorizontalAlignment.Left)
        ListView2.Columns.Add("kode Kategori", 0, HorizontalAlignment.Left)
        ListView2.Columns.Add("Kategori", 140, HorizontalAlignment.Left)
        ListView2.Columns.Add("id sub Kategori", 0, HorizontalAlignment.Left)
        ListView2.Columns.Add("Kode Sub Kategori", 140, HorizontalAlignment.Left)
        ListView2.Columns.Add("Sub Kategori", 490, HorizontalAlignment.Left)
        ListView2.Columns.Add("Prefix", 140, HorizontalAlignment.Left)

        ListView3.Columns.Add("id Kategori", 0, HorizontalAlignment.Left)
        ListView3.Columns.Add("kode Kategori", 0, HorizontalAlignment.Left)
        ListView3.Columns.Add("Kategori", 140, HorizontalAlignment.Left)
        ListView3.Columns.Add("id sub Kategori", 0, HorizontalAlignment.Left)
        ListView3.Columns.Add("Kode Sub Kategori", 0, HorizontalAlignment.Left)
        ListView3.Columns.Add("Sub Kategori", 140, HorizontalAlignment.Left)
        ListView3.Columns.Add("id sub Kategori 1", 0, HorizontalAlignment.Left)
        ListView3.Columns.Add("Kode Sub Kategori 1", 140, HorizontalAlignment.Left)
        ListView3.Columns.Add("Sub Kategori 1", 350, HorizontalAlignment.Left)
        ListView3.Columns.Add("Prefix", 140, HorizontalAlignment.Left)

        ListView4.Columns.Add("id Kategori", 0, HorizontalAlignment.Left)
        ListView4.Columns.Add("kode Kategori", 0, HorizontalAlignment.Left)
        ListView4.Columns.Add("Kategori", 140, HorizontalAlignment.Left)
        ListView4.Columns.Add("id sub Kategori", 0, HorizontalAlignment.Left)
        ListView4.Columns.Add("Kode Sub Kategori", 0, HorizontalAlignment.Left)
        ListView4.Columns.Add("Sub Kategori", 140, HorizontalAlignment.Left)
        ListView4.Columns.Add("id sub Kategori 1", 0, HorizontalAlignment.Left)
        ListView4.Columns.Add("Kode Sub Kategori 1", 0, HorizontalAlignment.Left)
        ListView4.Columns.Add("Sub Kategori 1", 140, HorizontalAlignment.Left)
        ListView4.Columns.Add("id sub Kategori 2", 0, HorizontalAlignment.Left)
        ListView4.Columns.Add("Kode Sub Kategori 2", 140, HorizontalAlignment.Left)
        ListView4.Columns.Add("Sub Kategori 2", 250, HorizontalAlignment.Left)
        ListView4.Columns.Add("Prefix", 100, HorizontalAlignment.Left)

        ListView5.Columns.Add("id Kategori", 0, HorizontalAlignment.Left)
        ListView5.Columns.Add("kode Kategori", 0, HorizontalAlignment.Left)
        ListView5.Columns.Add("Kategori", 140, HorizontalAlignment.Left)
        ListView5.Columns.Add("id sub Kategori", 0, HorizontalAlignment.Left)
        ListView5.Columns.Add("Kode Sub Kategori", 0, HorizontalAlignment.Left)
        ListView5.Columns.Add("Sub Kategori", 140, HorizontalAlignment.Left)
        ListView5.Columns.Add("id sub Kategori 1", 0, HorizontalAlignment.Left)
        ListView5.Columns.Add("Kode Sub Kategori 1", 0, HorizontalAlignment.Left)
        ListView5.Columns.Add("Sub Kategori 1", 140, HorizontalAlignment.Left)
        ListView5.Columns.Add("id sub Kategori 2", 0, HorizontalAlignment.Left)
        ListView5.Columns.Add("Kode Sub Kategori 2", 0, HorizontalAlignment.Left)
        ListView5.Columns.Add("Sub Kategori 2", 250, HorizontalAlignment.Left)
        ListView5.Columns.Add("id sub Kategori 3", 0, HorizontalAlignment.Left)
        ListView5.Columns.Add("Kode Sub Kategori 3", 140, HorizontalAlignment.Left)
        ListView5.Columns.Add("Sub Kategori 3", 250, HorizontalAlignment.Left)
        ListView5.Columns.Add("Prefix", 100, HorizontalAlignment.Left)

        kosong()
        kosong2()
        kosong3()
        kosong4()

        If Asal_proses = "" Then

            CmbSK3_Jenis.Enabled = True
            CmbSK3_JenisSub.Enabled = True
            CmbSK3_JenisSub1.Enabled = True
            CmbSK3_JenisSub2.Enabled = True

            TabControl1.SelectedIndex = 0

            TabControl1.TabPages(0).Show()
            TabControl1.TabPages(1).Show()
            TabControl1.TabPages(2).Show()
            TabControl1.TabPages(3).Show()

            xurut_departement = ""
            xid_cost = ""
            xid_gedung = ""
            xlink = ""

            kosong5()

        ElseIf Asal_proses = "N_EMI_SD_Tambah_PR_Barang_Lain_Departement" Then
            CmbSK3_Jenis.Enabled = True
            CmbSK3_JenisSub.Enabled = True
            CmbSK3_JenisSub1.Enabled = True
            CmbSK3_JenisSub2.Enabled = True

            TabControl1.SelectedIndex = 1

            kosong5()

        ElseIf Asal_proses = "pengajuan_barang_baru" Then
            CmbSK3_Jenis.Enabled = True
            CmbSK3_JenisSub.Enabled = True
            CmbSK3_JenisSub1.Enabled = True
            CmbSK3_JenisSub2.Enabled = True

            TabControl1.SelectedIndex = 1

            TabControl1.TabPages(0).Hide()
            TabControl1.TabPages(1).Show()
            TabControl1.TabPages(2).Show()
            TabControl1.TabPages(3).Show()

            xurut_departement = ""
            xid_cost = ""
            xid_gedung = ""
            xlink = ""

            kosong5()


        Else
            CmbSK3_Jenis.Enabled = False
            CmbSK3_JenisSub.Enabled = False
            CmbSK3_JenisSub1.Enabled = False
            CmbSK3_JenisSub2.Enabled = False

            xurut_departement = ""
            xid_cost = ""
            xid_gedung = ""
            xlink = ""

            TabControl1.SelectedIndex = 4

            TabControl1.TabPages(0).Hide()
            TabControl1.TabPages(1).Hide()
            TabControl1.TabPages(2).Hide()
            TabControl1.TabPages(3).Hide()
        End If

    End Sub

    Private Sub kosong()
        Try
            OpenConn()

            TextBox1.Enabled = True
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TxtSatuan_Value.Text = ""
            xid_kategori = ""
            xprefix1 = ""

            CmbSatuan_Kolom.Items.Clear() : arrcari.Clear()
            CmbSatuan_Kolom.Items.Add("Kode") : arrcari.Add("Kode_Kategori_Jenis")
            CmbSatuan_Kolom.Items.Add("Keterangan") : arrcari.Add("Keterangan")
            CmbSatuan_Kolom.Items.Add("Prefix") : arrcari.Add("Prefix")
            CmbSatuan_Kolom.SelectedIndex = -1

            ComboBox10.Items.Clear()
            ComboBox10.Items.Add("Y")
            ComboBox10.Items.Add("T")
            ComboBox10.SelectedIndex = -1

            BtnSimpan.Text = "&Simpan"
            BtnSimpan.Tag = "&Simpan"
            BtnHapus.Enabled = False

            ListView1.Items.Clear()
            SQL = "select Kode_Kategori_Jenis, Keterangan, Prefix, Id_Kategori_Jenis, Flag_Aktif from N_EMI_Master_Kategori_Jenis "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    Lvw.SubItems.Add(dr("Prefix"))
                    Lvw.SubItems.Add(dr("Flag_Aktif"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong2()
        Try
            OpenConn()

            TextBox7.Enabled = True
            TextBox7.Text = ""
            TextBox6.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            xid_sub_kategori = ""
            xprefix2 = ""

            ComboBox2.Items.Clear() : arrcari2.Clear()
            ComboBox2.Items.Add("Kode kategori") : arrcari2.Add("b.Kode_Kategori_Jenis")
            ComboBox2.Items.Add("Keterangan Kategori") : arrcari2.Add("b.Keterangan")
            ComboBox2.Items.Add("Kode Sub kategori") : arrcari2.Add("a.Kode_Sub_Kategori_Jenis")
            ComboBox2.Items.Add("Keterangan Sub") : arrcari2.Add("a.Keterangan")
            ComboBox2.Items.Add("Prefix") : arrcari2.Add("a.Prefix")
            ComboBox2.SelectedIndex = -1

            BtnSimpan2.Text = "&Simpan"
            BtnSimpan2.Tag = "&Simpan"
            BtnHapus2.Enabled = False

            ListView2.Items.Clear()
            SQL = "select b.Kode_Kategori_Jenis, b.Keterangan as Kategori_Jenis, a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "a.Prefix, a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Prefix"))
                Loop
            End Using

            ComboBox1.Items.Clear() : arrkateogori.Clear() : arrid.Clear()
            SQL = "select Kode_Kategori_Jenis, Keterangan, Id_Kategori_Jenis from N_EMI_Master_Kategori_Jenis "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Keterangan"))
                    arrkateogori.Add(dr("Kode_Kategori_Jenis"))
                    arrid.Add(dr("Id_Kategori_Jenis"))
                Loop
            End Using
            ComboBox1.SelectedIndex = -1
            ComboBox1.Enabled = True

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong3()
        Try
            OpenConn()

            TextBox8.Enabled = True
            TextBox8.Text = ""
            TextBox9.Text = ""
            TextBox10.Text = ""
            TextBox11.Text = ""
            xid_sub_kategori1 = ""
            xprefix3 = ""

            ComboBox5.Items.Clear() : arrcari3.Clear()
            ComboBox5.Items.Add("Kode kategori") : arrcari3.Add("c.Kode_Kategori_Jenis")
            ComboBox5.Items.Add("Keterangan Kategori") : arrcari3.Add("c.Keterangan")
            ComboBox5.Items.Add("Kode Sub kategori") : arrcari3.Add("b.Kode_Sub_Kategori_Jenis")
            ComboBox5.Items.Add("Keterangan Sub") : arrcari3.Add("b.Keterangan")
            ComboBox5.Items.Add("Kode Sub kategori 1") : arrcari3.Add("a.Kode_Sub_Kategori_Jenis_1")
            ComboBox5.Items.Add("Keterangan Sub 1") : arrcari3.Add("a.Keterangan")
            ComboBox5.Items.Add("Prefix") : arrcari3.Add("a.Prefix")
            ComboBox5.SelectedIndex = -1

            BtnSimpan3.Text = "&Simpan"
            BtnSimpan3.Tag = "&Simpan"
            BtnHapus3.Enabled = False

            ListView3.Items.Clear()
            SQL = "select  c.Kode_Kategori_Jenis, c.Keterangan as Kategori_Jenis, b.Kode_Sub_Kategori_Jenis, b.Keterangan as Sub_Kategori_Jenis,"
            SQL = SQL & "a.Kode_Sub_Kategori_Jenis_1, a.Keterangan as Sub_Kategori_Jenis1, a.Prefix, "
            SQL = SQL & "a.Id_Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_1 a, N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView3.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis1"))
                    Lvw.SubItems.Add(dr("Prefix"))
                Loop
            End Using

            ComboBox3.Items.Clear() : arrkateogori3.Clear() : arrid2.Clear()
            ComboBox4.Items.Clear() : arrsubkateogori3.Clear() : arridsub2.Clear()
            SQL = "select Kode_Kategori_Jenis, Keterangan, Id_Kategori_Jenis from N_EMI_Master_Kategori_Jenis "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox3.Items.Add(dr("Keterangan"))
                    arrkateogori3.Add(dr("Kode_Kategori_Jenis"))
                    arrid2.Add(dr("Id_Kategori_Jenis"))
                Loop
            End Using
            ComboBox3.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.SelectedText = -1
            ComboBox4.Enabled = True

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong4()
        Try
            OpenConn()

            TextBox12.Enabled = True
            TextBox12.Text = ""
            TextBox13.Text = ""
            TextBox14.Text = ""
            TextBox15.Text = ""
            xid_sub_kategori2 = ""
            xprefix4 = ""

            ComboBox9.Items.Clear() : arrcari4.Clear()
            ComboBox9.Items.Add("Kode kategori") : arrcari4.Add("d.Kode_Kategori_Jenis")
            ComboBox9.Items.Add("Keterangan Kategori") : arrcari4.Add("d.Keterangan")
            ComboBox9.Items.Add("Kode Sub kategori") : arrcari4.Add("c.Kode_Sub_Kategori_Jenis")
            ComboBox9.Items.Add("Keterangan Sub") : arrcari4.Add("c.Keterangan")
            ComboBox9.Items.Add("Kode Sub kategori 1") : arrcari4.Add("b.Kode_Sub_Kategori_Jenis_1")
            ComboBox9.Items.Add("Keterangan Sub 1") : arrcari4.Add("b.Keterangan")
            ComboBox9.Items.Add("Kode Sub kategori 2") : arrcari4.Add("a.Kode_Sub_Kategori_Jenis_2")
            ComboBox9.Items.Add("Keterangan Sub 2") : arrcari4.Add("a.Keterangan")
            ComboBox9.Items.Add("Prefix") : arrcari4.Add("a.Prefix")
            ComboBox9.SelectedIndex = -1

            BtnSimpan4.Text = "&Simpan"
            BtnSimpan4.Tag = "&Simpan"
            BtnHapus4.Enabled = False

            ListView4.Items.Clear()
            SQL = "select d.Kode_Kategori_Jenis, d.Keterangan as Kategori_Jenis, c.Kode_Sub_Kategori_Jenis, c.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "b.Kode_Sub_Kategori_Jenis_1, b.Keterangan as Sub_Kategori_Jenis_1, a.Kode_Sub_Kategori_Jenis_2, a.Keterangan as Sub_Kategori_Jenis_2, a.Prefix, "
            SQL = SQL & "a.Id_Sub_Kategori_Jenis_2, a.Id_Sub_Kategori_Jenis_1, b.Id_Sub_Kategori_Jenis, c.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_2 a, N_EMI_Master_Sub_Kategori_Jenis_1 b, "
            SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis c, N_EMI_Master_Kategori_Jenis d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_1 = b.Id_Sub_Kategori_Jenis_1 "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Jenis = d.Id_Kategori_Jenis "
            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView4.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Prefix"))
                Loop
            End Using

            ComboBox6.Items.Clear() : arrkateogori4.Clear() : arrid3.Clear()
            ComboBox7.Items.Clear() : arrsubkateogori4.Clear() : arridsub3.Clear()
            ComboBox8.Items.Clear() : arrsub1kateogori4.Clear() : arrid2sub3.Clear()
            SQL = "select Kode_Kategori_Jenis, Keterangan, Id_Kategori_Jenis from N_EMI_Master_Kategori_Jenis "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("Keterangan"))
                    arrkateogori4.Add(dr("Kode_Kategori_Jenis"))
                    arrid3.Add(dr("Id_Kategori_Jenis"))
                Loop
            End Using
            ComboBox6.SelectedIndex = -1
            ComboBox6.Enabled = True
            ComboBox7.SelectedText = -1
            ComboBox7.Enabled = True
            ComboBox8.SelectedText = -1
            ComboBox8.Enabled = True

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub kosong5()
        Try
            OpenConn()

            TextBox16.Enabled = True
            TextBox16.Text = ""
            TextBox13.Text = ""
            TextBox17.Text = ""
            TextBox18.Text = ""
            TextBox19.Text = ""
            xid_sub_kategori3 = ""
            xprefix5 = ""

            ComboBox15.Items.Clear() : arrcari5.Clear()
            ComboBox15.Items.Add("Kode kategori") : arrcari5.Add("3.Kode_Kategori_Jenis")
            ComboBox15.Items.Add("Keterangan Kategori") : arrcari5.Add("3.Keterangan")
            ComboBox15.Items.Add("Kode Sub kategori") : arrcari5.Add("d.Kode_Sub_Kategori_Jenis")
            ComboBox15.Items.Add("Keterangan Sub") : arrcari5.Add("d.Keterangan")
            ComboBox15.Items.Add("Kode Sub kategori 1") : arrcari5.Add("c.Kode_Sub_Kategori_Jenis_1")
            ComboBox15.Items.Add("Keterangan Sub 1") : arrcari5.Add("c.Keterangan")
            ComboBox15.Items.Add("Kode Sub kategori 2") : arrcari5.Add("b.Kode_Sub_Kategori_Jenis_2")
            ComboBox15.Items.Add("Keterangan Sub 2") : arrcari5.Add("b.Keterangan")
            ComboBox15.Items.Add("Kode Sub kategori 3") : arrcari5.Add("a.Kode_Sub_Kategori_Jenis_3")
            ComboBox15.Items.Add("Keterangan Sub 3") : arrcari5.Add("a.Keterangan")
            ComboBox15.Items.Add("Prefix") : arrcari5.Add("a.Prefix")
            ComboBox15.SelectedIndex = -1

            BtnSimpan5.Text = "&Simpan"
            BtnSimpan5.Tag = "&Simpan"
            BtnHapus5.Enabled = False

            ListView5.Items.Clear()
            SQL = "select e.Kode_Kategori_Jenis, e.Keterangan as Kategori_Jenis, d.Kode_Sub_Kategori_Jenis, d.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "c.Kode_Sub_Kategori_Jenis_1, c.Keterangan as Sub_Kategori_Jenis_1, b.Kode_Sub_Kategori_Jenis_2, b.Keterangan as Sub_Kategori_Jenis_2, "
            SQL = SQL & "a.Kode_Sub_Kategori_Jenis_3, a.Keterangan as Sub_Kategori_Jenis_3, a.Prefix, "
            SQL = SQL & "a.Id_Sub_Kategori_Jenis_3, a.Id_Sub_Kategori_Jenis_2, b.Id_Sub_Kategori_Jenis_1, c.Id_Sub_Kategori_Jenis, d.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a, N_EMI_Master_Sub_Kategori_Jenis_2 b, N_EMI_Master_Sub_Kategori_Jenis_1 c, "
            SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis d, N_EMI_Master_Kategori_Jenis e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_2 = b.Id_Sub_Kategori_Jenis_2 "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis_1 = c.Id_Sub_Kategori_Jenis_1 "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis "
            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView5.Items.Add(dr("Id_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_1"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_2"))
                    Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_3"))
                    Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_3"))
                    Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_3"))
                    Lvw.SubItems.Add(dr("Prefix"))
                Loop
            End Using

            CmbSK3_Jenis.Items.Clear() : arrkateogori5.Clear() : arrid4.Clear()
            CmbSK3_JenisSub.Items.Clear() : arrsubkateogori5.Clear() : arridsub4.Clear()
            CmbSK3_JenisSub1.Items.Clear() : arrsub1kateogori5.Clear() : arrid2sub4.Clear()
            CmbSK3_JenisSub2.Items.Clear() : arrsub2kateogori5.Clear() : arrid3sub4.Clear()
            SQL = "select Kode_Kategori_Jenis, Keterangan, Id_Kategori_Jenis from N_EMI_Master_Kategori_Jenis "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSK3_Jenis.Items.Add(dr("Keterangan"))
                    arrkateogori5.Add(dr("Kode_Kategori_Jenis"))
                    arrid4.Add(dr("Id_Kategori_Jenis"))
                Loop
            End Using
            CmbSK3_Jenis.SelectedIndex = -1
            CmbSK3_Jenis.Enabled = True
            CmbSK3_JenisSub.SelectedText = -1
            CmbSK3_JenisSub.Enabled = True
            CmbSK3_JenisSub1.SelectedText = -1
            CmbSK3_JenisSub1.Enabled = True
            CmbSK3_JenisSub2.SelectedText = -1
            CmbSK3_JenisSub2.Enabled = True

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub BtnSimpan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BtnSimpan.KeyPress
        If e.KeyChar = Chr(13) Then BtnHapus.Focus()
    End Sub

    Private Sub BtnHapus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BtnHapus.KeyPress
        If e.KeyChar = Chr(13) Then BtnRefresh.Focus()
    End Sub

    Private Sub CmbSatuan_Kolom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSatuan_Kolom.KeyPress
        If e.KeyChar = Chr(13) Then TxtSatuan_Value.Focus()
    End Sub

    Private Sub TxtSatuan_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtSatuan_Value.KeyPress
        If e.KeyChar = Chr(13) Then BtnCari.Focus()
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        kosong()
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        ElseIf TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus() : Exit Sub
            'ElseIf TextBox3.Text.Trim.Length <> 1 Then
            '    MessageBox.Show("Prefix Harus 1 Digit Angka", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox3.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            If BtnSimpan.Tag = "&Simpan" Then
                SQL = "select Kode_Kategori_Jenis, Keterangan, Id_Kategori_Jenis from N_EMI_Master_Kategori_Jenis "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and upper(Kode_Kategori_Jenis) = '" & TextBox1.Text.Trim.ToUpper & "' "
                SQL = SQL & "and upper(Keterangan) = '" & TextBox2.Text.Trim.ToUpper & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then

                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("kode ketegori sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                get_no_prefix()
                If xprefix1 = "*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix1 > 9 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                TextBox3.Text = xprefix1

                SQL = "insert into N_EMI_Master_Kategori_Jenis(Kode_Perusahaan, Kode_Kategori_Jenis, Keterangan, Prefix, Flag_Aktif) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & TextBox1.Text.Trim.ToUpper & "', '" & TextBox2.Text.Trim.ToUpper & "', "
                SQL = SQL & "'" & TextBox3.Text.Trim & "', '" & ComboBox10.Text & "' ) "
                ExecuteTrans(SQL)
            Else
                SQL = "select Kode_Kategori_Jenis, Keterangan from N_EMI_Master_Kategori_Jenis "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and upper(Keterangan) = '" & TextBox2.Text.Trim.ToUpper & "' and Id_Kategori_Jenis <> '" & xid_kategori & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("ketegori sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'SQL = "select Id_Kategori_Jenis from N_EMI_Master_Sub_Kategori_Jenis "
                'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and Id_Kategori_Jenis = '" & xid_kategori & "' "
                'Using dr = OpenTrans(SQL)
                '    If dr.Read Then
                '        dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("ketegori sudah pernah dipakai di tabel N_EMI_Master_Sub_Kategori_Jenis ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                SQL = "update N_EMI_Master_Kategori_Jenis set "
                SQL = SQL & "Keterangan = '" & TextBox2.Text.Trim.ToUpper & "', "
                'SQL = SQL & "Prefix = '" & TextBox3.Text.Trim & "', "
                SQL = SQL & "Flag_Aktif = '" & ComboBox10.Text & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Kategori_Jenis = '" & xid_kategori & "' "
                'SQL = SQL & "and Kode_Kategori_Jenis = '" & TextBox1.Text & "' "
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles BtnHapus.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        ElseIf TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus() : Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show("Mau Hapus data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction

                SQL = "select Id_Kategori_Jenis from N_EMI_Master_Sub_Kategori_Jenis "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Kategori_Jenis = '" & xid_kategori & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak bisa dihapus karena """ & TextBox1.Text & """ sudah terdaftar pada sub kategori ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "DELETE FROM N_EMI_Master_Kategori_Jenis WHERE "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "AND Kode_Kategori_Jenis = '" & TextBox1.Text & "' "
                SQL = SQL & "and Id_Kategori_Jenis = '" & xid_kategori & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show("Data berhasil dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        kosong2()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox2.Text.Trim.Length = 0 Then Exit Sub
        If TextBox5.Text.Trim.Length = 0 Then Exit Sub

        Cari2("T")
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then
            Exit Sub
            'ElseIf xid_kategori = "" Then
            '    Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select Kode_Kategori_Jenis, Keterangan, Prefix, Id_Kategori_Jenis, Flag_Aktif from N_EMI_Master_Kategori_Jenis "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Id_Kategori_Jenis = '" & xid_kategori & "' "
            'SQL = SQL & "and Kode_Kategori_Jenis = '" & TextBox1.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox2.Text = dr("Keterangan")
                    TextBox3.Text = dr("Prefix")
                    TextBox1.Enabled = False
                    ComboBox10.Text = dr("Flag_Aktif")

                    BtnSimpan.Text = "&Update"
                    BtnHapus.Enabled = True
                    BtnSimpan.Tag = "&Update"
                Else
                    xid_kategori = ""
                    TextBox1.Enabled = True
                    TextBox2.Text = ""
                    ComboBox10.SelectedIndex = -1
                    'TextBox3.Text = ""
                    BtnSimpan.Text = "&Simpan"
                    BtnHapus.Enabled = False
                    BtnSimpan.Tag = "&Simpan"
                End If
            End Using

            If BtnSimpan.Tag = "&Simpan" Then
                get_no_prefix()
                If xprefix1 = "*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix1 > 9 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                TextBox3.Text = xprefix1
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        xid_kategori = ListView1.FocusedItem.SubItems(0).Text
        TextBox1.Text = ListView1.FocusedItem.SubItems(1).Text
        TextBox1_Leave(ListView1, e)
    End Sub

    Private Sub N_EMI_Master_Kategori_Jenis_Sub_Kategori_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox10.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Chr(13) Then TextBox6.Focus()
    End Sub

    Private Sub BtnSimpan2_Click(sender As Object, e As EventArgs) Handles BtnSimpan2.Click
        If TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus() : Exit Sub
        ElseIf TextBox6.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan sub kategori Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox6.Focus() : Exit Sub
        ElseIf ComboBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        ElseIf TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus() : Exit Sub
        ElseIf TextBox4.Text.Trim.Length <> 2 Then
            MessageBox.Show("Prefix Harus 2 Digit Angka", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            If BtnSimpan2.Tag = "&Simpan" Then
                SQL = "select Kode_Sub_Kategori_Jenis, Keterangan from N_EMI_Master_Sub_Kategori_Jenis "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and  (upper(Kode_Sub_Kategori_Jenis) = '" & TextBox7.Text.Trim.ToUpper & "' "
                '    SQL = SQL & "and upper(Keterangan) = '" & TextBox6.Text.Trim.ToUpper & "' "
                SQL = SQL & "and id_kategori_jenis  = '" & arrid.Item(ComboBox1.SelectedIndex) & "' or "
                SQL = SQL & " id_kategori_jenis  = '" & arrid.Item(ComboBox1.SelectedIndex) & "' and prefix = '" & TextBox4.Text.Trim & "' ) "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("kode sub ketegori sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                get_no_prefix2()
                If xprefix2 = "0*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix2 > 99 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                TextBox4.Text = xprefix2

                SQL = "insert into N_EMI_Master_Sub_Kategori_Jenis(Kode_Perusahaan, Id_Kategori_Jenis, Kode_Sub_Kategori_Jenis, Keterangan, Prefix) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & arrid.Item(ComboBox1.SelectedIndex) & "', '" & TextBox7.Text.Trim.ToUpper & "', '" & TextBox6.Text.Trim.ToUpper & "', "
                SQL = SQL & "'" & TextBox4.Text.Trim & "' ) "
                ExecuteTrans(SQL)
            Else
                SQL = "select Kode_Sub_Kategori_Jenis, Keterangan from N_EMI_Master_Sub_Kategori_Jenis "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and upper(Keterangan) = '" & TextBox6.Text.Trim.ToUpper & "' and Id_Sub_Kategori_Jenis <> '" & xid_sub_kategori & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("sub ketegori sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'SQL = "select Id_Sub_Kategori_Jenis from N_EMI_Master_Sub_Kategori_Jenis_1 "
                'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and Id_Sub_Kategori_Jenis = '" & xid_sub_kategori & "' "
                'Using dr = OpenTrans(SQL)
                '    If dr.Read Then
                '        dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("sub ketegori sudah pernah dipakai di tabel N_EMI_Master_Sub_Kategori_Jenis_1 ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                SQL = "update N_EMI_Master_Sub_Kategori_Jenis set "
                SQL = SQL & "Keterangan = '" & TextBox6.Text.Trim.ToUpper & "' "
                'SQL = SQL & "Prefix = '" & TextBox4.Text.Trim & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis = '" & xid_sub_kategori & "' "
                'SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori.Item(ComboBox1.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & TextBox7.Text & "' "
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong2()
    End Sub

    Private Sub BtnHapus2_Click(sender As Object, e As EventArgs) Handles BtnHapus2.Click
        If TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus() : Exit Sub
        ElseIf TextBox6.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox6.Focus() : Exit Sub
        ElseIf ComboBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        ElseIf TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus() : Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show("Mau Hapus data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction

                SQL = "select Id_Sub_Kategori_Jenis from N_EMI_Master_Sub_Kategori_Jenis_1 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis = '" & xid_sub_kategori & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak bisa di hapus karena """ & arrkateogori.Item(ComboBox1.SelectedIndex) & """ sudah di pakai oleh Sub Kategori 1 ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "DELETE FROM N_EMI_Master_Sub_Kategori_Jenis "
                SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis = '" & xid_sub_kategori & "' "
                'SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori.Item(ComboBox1.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & TextBox7.Text & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show("Data berhasil dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong2()
    End Sub

    Private Sub BtnRefresh3_Click(sender As Object, e As EventArgs) Handles BtnRefresh3.Click
        kosong3()
    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Chr(13) Then TextBox4.Focus()
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan2.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub BtnSimpan2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BtnSimpan2.KeyPress
        If e.KeyChar = Chr(13) Then BtnHapus2.Focus()
    End Sub

    Private Sub BtnHapus2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BtnHapus2.KeyPress
        If e.KeyChar = Chr(13) Then Button2.Focus()
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        ComboBox1.Text = ListView2.FocusedItem.SubItems(2).Text
        xid_sub_kategori = ListView2.FocusedItem.SubItems(3).Text
        TextBox7.Text = ListView2.FocusedItem.SubItems(4).Text
        TextBox7_Leave(ListView2, e)
    End Sub

    Private Sub TextBox7_Leave(sender As Object, e As EventArgs) Handles TextBox7.Leave
        If TextBox7.Text.Trim.Length = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select b.Kode_Kategori_Jenis, b.Keterangan as Kategori_Jenis, a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "a.Prefix, a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Sub_Kategori_Jenis = '" & xid_sub_kategori & "' "
            'SQL = SQL & "and a.Kode_Kategori_Jenis = '" & arrkateogori.Item(ComboBox1.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis = '" & TextBox7.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox6.Text = dr("Sub_Kategori_Jenis")
                    TextBox4.Text = dr("Prefix")
                    TextBox7.Enabled = False
                    ComboBox1.Enabled = False

                    BtnSimpan2.Text = "&Update"
                    BtnHapus2.Enabled = True
                    BtnSimpan2.Tag = "&Update"
                Else
                    TextBox7.Enabled = True
                    ComboBox1.Enabled = True
                    TextBox6.Text = ""
                    'TextBox4.Text = ""
                    xid_sub_kategori = ""
                    BtnSimpan2.Text = "&Simpan"
                    BtnHapus2.Enabled = False
                    BtnSimpan2.Tag = "&Simpan"
                End If
            End Using

            If BtnSimpan2.Tag = "&Simpan" Then
                get_no_prefix2()
                If xprefix2 = "0*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix2 > 99 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'If xprefix2 > 99 Then
                '    CloseConn()
                '    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                TextBox4.Text = xprefix2
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnCari3_Click(sender As Object, e As EventArgs) Handles BtnCari3.Click
        If ComboBox5.Text.Trim.Length = 0 Then Exit Sub
        If TextBox11.Text.Trim.Length = 0 Then Exit Sub

        Cari3("T")
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            ComboBox4.Items.Clear() : arrsubkateogori3.Clear() : arridsub2.Clear() : TextBox10.Text = ""
            SQL = "select a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, a.Id_Sub_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Id_Kategori_Jenis = '" & arrid2.Item(ComboBox3.SelectedIndex) & "' "
            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("Sub_Kategori_Jenis"))
                    arrsubkateogori3.Add(dr("Kode_Sub_Kategori_Jenis"))
                    arridsub2.Add(dr("Id_Sub_Kategori_Jenis"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnRefresh4_Click(sender As Object, e As EventArgs) Handles BtnRefresh4.Click
        kosong4()
    End Sub

    Private Sub BtnCari4_Click(sender As Object, e As EventArgs) Handles BtnCari4.Click
        If ComboBox9.Text.Trim.Length = 0 Then Exit Sub
        If TextBox15.Text.Trim.Length = 0 Then Exit Sub

        Cari4("T")
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox4.Focus()
    End Sub

    Private Sub BtnSimpan3_Click(sender As Object, e As EventArgs) Handles BtnSimpan3.Click
        If TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus() : Exit Sub
        ElseIf TextBox9.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan sub kategori Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox9.Focus() : Exit Sub
        ElseIf ComboBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub
        ElseIf ComboBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus() : Exit Sub
        ElseIf TextBox10.Text.Trim.Length <> 2 Then
            MessageBox.Show("Prefix Harus 2 Digit Angka", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            If BtnSimpan3.Tag = "&Simpan" Then
                SQL = "select Kode_Sub_Kategori_Jenis_1, Keterangan from N_EMI_Master_Sub_Kategori_Jenis_1 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and ( upper(Kode_Sub_Kategori_Jenis_1) = '" & TextBox8.Text.Trim.ToUpper & "' "
                '  SQL = SQL & "and upper(Keterangan) = '" & TextBox9.Text.Trim.ToUpper & "' "
                SQL = SQL & "AND upper(id_sub_kategori_jenis) = '" & arridsub2.Item(ComboBox4.SelectedIndex) & "' or "
                SQL = SQL & "upper(id_sub_kategori_jenis) = '" & arridsub2.Item(ComboBox4.SelectedIndex) & "' and "
                SQL = SQL & "prefix = '" & TextBox10.Text.Trim & "' ) "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("kode sub ketegori 1 sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                If BtnSimpan.Tag = "&Simpan" Then
                    get_no_prefix3()
                    If xprefix3 = "0*" Then
                        CloseConn()
                        MessageBox.Show("Jumlah Prefix untuk sub kategori 1 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf xprefix3 > 99 Then
                        CloseConn()
                        MessageBox.Show("Jumlah Prefix untuk sub kategori 1 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    TextBox10.Text = xprefix3
                End If

                SQL = "insert into N_EMI_Master_Sub_Kategori_Jenis_1"
                SQL = SQL & "(Kode_Perusahaan,Id_Sub_Kategori_Jenis,Kode_Sub_Kategori_Jenis_1,Keterangan,Prefix) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & arridsub2.Item(ComboBox4.SelectedIndex) & "', "
                SQL = SQL & "'" & TextBox8.Text.Trim.ToUpper & "', '" & TextBox9.Text.Trim.ToUpper & "', '" & TextBox10.Text.Trim & "' )"
                ExecuteTrans(SQL)
            Else
                SQL = "select Kode_Sub_Kategori_Jenis_1, Keterangan from N_EMI_Master_Sub_Kategori_Jenis_1 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and upper(Keterangan) = '" & TextBox9.Text.Trim.ToUpper & "' and Id_Sub_Kategori_Jenis_1 <> '" & xid_sub_kategori1 & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("sub ketegori 1 sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'SQL = "select Id_Sub_Kategori_Jenis_1 from N_EMI_Master_Sub_Kategori_Jenis_2 "
                'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and Id_Sub_Kategori_Jenis_1 = '" & xid_sub_kategori1 & "' "
                'Using dr = OpenTrans(SQL)
                '    If dr.Read Then
                '        dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("sub ketegori 1 sudah pernah dipakai di tabel N_EMI_Master_Sub_Kategori_Jenis_2 ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                SQL = "update N_EMI_Master_Sub_Kategori_Jenis_1 set "
                SQL = SQL & "Keterangan = '" & TextBox9.Text.ToUpper & "' "
                'SQL = SQL & "Prefix = '" & TextBox10.Text.Trim & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis_1 = '" & xid_sub_kategori1 & "' "
                'SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori3.Item(ComboBox3.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & arrsubkateogori3.Item(ComboBox4.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_1 = '" & TextBox8.Text & "' "
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong3()
    End Sub

    Private Sub BtnCari5_Click(sender As Object, e As EventArgs) Handles BtnCari5.Click
        If ComboBox15.Text.Trim.Length = 0 Then Exit Sub
        If TextBox19.Text.Trim.Length = 0 Then Exit Sub

        Cari5("T")
    End Sub

    Private Sub BtnHapus3_Click(sender As Object, e As EventArgs) Handles BtnHapus3.Click
        If TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus() : Exit Sub
        ElseIf TextBox9.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox9.Focus() : Exit Sub
        ElseIf ComboBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub
        ElseIf ComboBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus() : Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show("Mau Hapus data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction
                SQL = "select Id_Sub_Kategori_Jenis_1 from N_EMI_Master_Sub_Kategori_Jenis_2 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis_1 = '" & xid_sub_kategori1 & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak bisa dihapus karena """ & TextBox8.Text & """ sudah dipakai di Sub Kategori 2", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "DELETE FROM N_EMI_Master_Sub_Kategori_Jenis_1 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis_1 = '" & xid_sub_kategori1 & "' "
                'SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori3.Item(ComboBox3.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & arrsubkateogori3.Item(ComboBox4.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_1 = '" & TextBox8.Text & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show("Data berhasil dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong3()
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then TextBox8.Focus()
    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Chr(13) Then TextBox9.Focus()
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        If e.KeyChar = Chr(13) Then TextBox10.Focus()
    End Sub

    Private Sub TextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan3.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub BtnSimpan5_Click(sender As Object, e As EventArgs) Handles BtnSimpan5.Click
        If TextBox16.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox16.Focus() : Exit Sub
        ElseIf TextBox17.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan sub kategori Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox17.Focus() : Exit Sub
        ElseIf CmbSK3_Jenis.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSK3_Jenis.Focus() : Exit Sub
        ElseIf CmbSK3_JenisSub.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSK3_JenisSub.Focus() : Exit Sub
        ElseIf CmbSK3_JenisSub1.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis 1 Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSK3_JenisSub1.Focus() : Exit Sub
        ElseIf CmbSK3_JenisSub2.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis 2 Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSK3_JenisSub2.Focus() : Exit Sub
        ElseIf TextBox18.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox18.Focus() : Exit Sub
        ElseIf TextBox18.Text.Trim.Length <> 3 Then
            MessageBox.Show("Prefix Harus 3 Digit Angka", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox18.Focus() : Exit Sub
        End If

        get_jam()
        Dim id As Integer = 0
        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            If BtnSimpan5.Tag = "&Simpan" Then
                SQL = "select Kode_Sub_Kategori_Jenis_3, Keterangan from N_EMI_Master_Sub_Kategori_Jenis_3 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and (upper(Kode_Sub_Kategori_Jenis_3) = '" & TextBox16.Text.Trim.ToUpper & "' "
                SQL = SQL & "and id_sub_kategori_jenis_2 = '" & arrid3sub4.Item(CmbSK3_JenisSub2.SelectedIndex) & "' "

                SQL = SQL & " or  "
                SQL = SQL & " id_sub_kategori_jenis_2 = '" & arrid3sub4.Item(CmbSK3_JenisSub2.SelectedIndex) & "' "
                SQL = SQL & "and prefix = '" & TextBox18.Text.Trim & "' )"
                ' SQL = SQL & "and upper(Keterangan) = '" & TextBox17.Text.Trim.ToUpper & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        TextBox16.Text = ""
                        TextBox16.Focus()

                        MessageBox.Show("kode sub ketegori 3 sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                get_no_prefix5()
                If xprefix5 = "00*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 3 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix5 > 999 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 3 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                TextBox14.Text = xprefix4

                SQL = "insert into N_EMI_Master_Sub_Kategori_Jenis_3"
                SQL = SQL & "(Kode_Perusahaan,Id_Sub_Kategori_Jenis_2,Kode_Sub_Kategori_Jenis_3,Keterangan,Prefix) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & arrid3sub4.Item(CmbSK3_JenisSub2.SelectedIndex) & "', "
                SQL = SQL & "'" & TextBox16.Text.Trim.ToUpper & "', '" & TextBox17.Text.Trim.ToUpper & "', '" & TextBox18.Text.Trim & "' )"
                ExecuteTrans(SQL)

                SQL = "select IDENT_CURRENT('N_EMI_Master_Sub_Kategori_Jenis_3') as urut"
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        id = Dr1("urut")
                    End If
                End Using

                SQL = "select Kode_Sub_Kategori_Jenis_3, Keterangan from N_EMI_Master_Sub_Kategori_Jenis_3 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and upper(Kode_Sub_Kategori_Jenis_3) = '" & TextBox16.Text.Trim.ToUpper & "' "
                SQL = SQL & "and id_sub_kategori_jenis_2 = '" & arrid3sub4.Item(CmbSK3_JenisSub2.SelectedIndex) & "'"
                SQL = SQL & "and id_sub_kategori_jenis_3 = '" & id & "'"
                Using dr = OpenTrans(SQL)
                    If Not dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan ulangi Transaksi ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                If Asal_proses = "N_EMI_SD_Tambah_PR_Barang_Lain_Departement" Then
                    SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail_Log(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan, Link, "
                    SQL = SQL & "Id_Kategori_Jenis, Id_Sub_Kategori_Jenis, Id_Sub_Kategori_Jenis_1 ,Id_Sub_Kategori_Jenis_2 ,Id_Sub_Kategori_Jenis_3, No_urut) "
                    SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah,Satuan, Tanggal_Delivery, keterangan, Link, "
                    SQL = SQL & "Id_Kategori_Jenis, Id_Sub_Kategori_Jenis, Id_Sub_Kategori_Jenis_1 ,Id_Sub_Kategori_Jenis_2 ,Id_Sub_Kategori_Jenis_3, No_urut "
                    SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail where kode_perusahaan = '" & KodePerusahaan & "' and no_Urut = '" & xurut_departement & "' "
                    ExecuteTrans(SQL)

                    SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set "
                    SQL = SQL & "Flag_Ajukan = 'Y', "
                    SQL = SQL & "Id_Cost_Center = " & xid_cost & ", "
                    SQL = SQL & "ID_Gedung = " & xid_gedung & ", "
                    SQL = SQL & "Link = '" & xlink & "', "
                    SQL = SQL & "Id_Kategori_Jenis = '" & arrid4.Item(CmbSK3_Jenis.SelectedIndex) & "', "
                    SQL = SQL & "Id_Sub_Kategori_Jenis = '" & arridsub4.Item(CmbSK3_JenisSub.SelectedIndex) & "', "
                    SQL = SQL & "Id_Sub_Kategori_Jenis_1 = '" & arrid2sub4.Item(CmbSK3_JenisSub1.SelectedIndex) & "', "
                    SQL = SQL & "Id_Sub_Kategori_Jenis_2 = '" & arrid3sub4.Item(CmbSK3_JenisSub2.SelectedIndex) & "', "
                    SQL = SQL & "Id_Sub_Kategori_Jenis_3 = '" & id & "' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and no_Urut = '" & xurut_departement & "' "
                    ExecuteTrans(SQL)
                ElseIf Asal_proses = "pengajuan_barang_baru" Then
                    Dim noFakturPengajuan As String = ""
                    noFakturPengajuan = fPengajuanBrgBru & Format(tgl_skg, "MMyy") & "-" &
                                 General_Class.Get_Last_Number2("N_EMI_Pengajuan_Barang_Baru_Lain", "no_Faktur", 5,
                                 "Kode_perusahaan", KodePerusahaan,
                                 "And", "substring(no_Faktur, 1, " & Len(fPengajuanBrgBru) + 4 & ")", fPengajuanBrgBru & Format(tgl_skg, "MMyy"))

                    SQL = "insert into N_EMI_Pengajuan_Barang_Baru_Lain(Kode_Perusahaan,No_Faktur,Tanggal,jam,Userid,Id_Sub_Kategori_Jenis_3) values ( "
                    SQL = SQL & "'" & KodePerusahaan & "', '" & noFakturPengajuan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "',"
                    SQL = SQL & "'" & UserID & "', '" & id & "' "
                    SQL = SQL & ")"
                    ExecuteTrans(SQL)

                End If

            Else
                SQL = "select Kode_Sub_Kategori_Jenis_3, Keterangan from N_EMI_Master_Sub_Kategori_Jenis_3 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and upper(Keterangan) = '" & TextBox17.Text.Trim.ToUpper & "' and Id_Sub_Kategori_Jenis_3 <> '" & xid_sub_kategori3 & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("sub ketegori 3 sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'SQL = "select Id_Sub_Kategori_Jenis_2 from Barang_Lain "
                'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and Id_Sub_Kategori_Jenis_2 = '" & xid_sub_kategori2 & "' "
                'Using dr = OpenTrans(SQL)
                '    If dr.Read Then
                '        dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("sub ketegori 2 sudah pernah dipakai di tabel barang ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                SQL = "update N_EMI_Master_Sub_Kategori_Jenis_3 set "
                SQL = SQL & "Keterangan = '" & TextBox17.Text.ToUpper & "' "
                'SQL = SQL & "Prefix = '" & TextBox14.Text.Trim & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis_3 = '" & xid_sub_kategori3 & "' "
                'SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori4.Item(ComboBox6.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & arrsubkateogori4.Item(ComboBox7.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_1 = '" & arrsub1kateogori4.Item(ComboBox8.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_2 = '" & TextBox12.Text & "' "
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If Asal_proses = "" Then
            kosong5()
        ElseIf Asal_proses = "N_EMI_SD_Tambah_PR_Barang_Lain_Departement" Then
            N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Close()
            N_EMI_Display_Request_Departement_Barang_Lain.BtnCari_Click(BtnSimpan5, e)
            Me.Close()
        ElseIf Asal_proses = "pengajuan_barang_baru" Then
            kosong5()
            N_EMI_Display_Request_Departement_Barang_Lain.Button2_Click(BtnSimpan5, e)
            Me.Close()
        Else
            Master_Barang_Lain.ComboBox23_SelectedIndexChanged(BtnSimpan5, e)

            Dim IdSub3 = Master_Barang_Lain.arrSubKategoriJenis3.IndexOf(id)
            Master_Barang_Lain.CmbKatJenisSub3.SelectedIndex = IdSub3

            Master_Barang_Lain.CmbKatJenisSub3_SelectedIndexChanged(BtnSimpan5, e)
            Me.Close()

        End If


    End Sub

    Private Sub BtnHapus5_Click(sender As Object, e As EventArgs) Handles BtnHapus5.Click
        If TextBox16.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox16.Focus() : Exit Sub
        ElseIf TextBox17.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan sub kategori Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox17.Focus() : Exit Sub
        ElseIf CmbSK3_Jenis.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSK3_Jenis.Focus() : Exit Sub
        ElseIf CmbSK3_JenisSub.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSK3_JenisSub.Focus() : Exit Sub
        ElseIf CmbSK3_JenisSub1.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis 1 Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSK3_JenisSub1.Focus() : Exit Sub
        ElseIf CmbSK3_JenisSub2.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis 2 Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSK3_JenisSub2.Focus() : Exit Sub
        ElseIf TextBox18.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox18.Focus() : Exit Sub
            'ElseIf TextBox18.Text.Trim.Length <> 3 Then
            '    MessageBox.Show("Prefix Harus 3 Digit Angka", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox18.Focus() : Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show("Mau Hapus data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction
                SQL = "select Id_Sub_Kategori_Jenis_3 from Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis_3 = '" & xid_sub_kategori3 & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak bisa dihapus karena """ & TextBox12.Text & """ sudah pernah dipakai dibarang! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "DELETE FROM N_EMI_Master_Sub_Kategori_Jenis_3 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis_3 = '" & xid_sub_kategori3 & "' "
                'SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori4.Item(ComboBox6.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & arrsubkateogori4.Item(ComboBox7.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_1 = '" & arrsub1kateogori4.Item(ComboBox8.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_2 = '" & TextBox12.Text & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show("Data berhasil dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong5()
    End Sub

    Private Sub BtnRefresh5_Click(sender As Object, e As EventArgs) Handles BtnRefresh5.Click
        kosong5()
    End Sub

    Private Sub ListView3_DoubleClick(sender As Object, e As EventArgs) Handles ListView3.DoubleClick
        ComboBox3.Text = ListView3.FocusedItem.SubItems(2).Text
        ComboBox4.Text = ListView3.FocusedItem.SubItems(5).Text
        xid_sub_kategori1 = ListView3.FocusedItem.SubItems(6).Text
        TextBox8.Text = ListView3.FocusedItem.SubItems(7).Text

        TextBox8_Leave(ListView3, e)
    End Sub

    Private Sub BtnSimpan4_Click(sender As Object, e As EventArgs) Handles BtnSimpan4.Click
        If TextBox12.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox12.Focus() : Exit Sub
        ElseIf TextBox13.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan sub kategori Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox13.Focus() : Exit Sub
        ElseIf ComboBox6.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus() : Exit Sub
        ElseIf ComboBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox7.Focus() : Exit Sub
        ElseIf ComboBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis 1 Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox8.Focus() : Exit Sub
        ElseIf TextBox14.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox14.Focus() : Exit Sub
        ElseIf TextBox14.Text.Trim.Length <> 2 Then
            MessageBox.Show("Prefix Harus 2 Digit Angka", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox14.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            If BtnSimpan4.Tag = "&Simpan" Then
                SQL = "select Kode_Sub_Kategori_Jenis_2, Keterangan from N_EMI_Master_Sub_Kategori_Jenis_2 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and ( upper(Kode_Sub_Kategori_Jenis_2) = '" & TextBox12.Text.Trim.ToUpper & "' "
                ' SQL = SQL & "and upper(Keterangan) = '" & TextBox13.Text.Trim.ToUpper & "' "
                SQL = SQL & "and upper(id_sub_kategori_jenis_1) = '" & arrid2sub3.Item(ComboBox8.SelectedIndex) & "' "
                SQL = SQL & "or upper(id_sub_kategori_jenis_1) = '" & arrid2sub3.Item(ComboBox8.SelectedIndex) & "'   "
                SQL = SQL & "and prefix = '" & TextBox14.Text.Trim & "' ) "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("kode sub ketegori 2 sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                get_no_prefix4()
                If xprefix4 = "0*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 2 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix4 > 99 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 2 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                TextBox14.Text = xprefix4

                SQL = "insert into N_EMI_Master_Sub_Kategori_Jenis_2"
                SQL = SQL & "(Kode_Perusahaan,Id_Sub_Kategori_Jenis_1,Kode_Sub_Kategori_Jenis_2,Keterangan,Prefix) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & arrid2sub3.Item(ComboBox8.SelectedIndex) & "', "
                SQL = SQL & "'" & TextBox12.Text.Trim.ToUpper & "', '" & TextBox13.Text.Trim.ToUpper & "', '" & TextBox14.Text.Trim & "' )"
                ExecuteTrans(SQL)
            Else
                SQL = "select Kode_Sub_Kategori_Jenis_2, Keterangan from N_EMI_Master_Sub_Kategori_Jenis_2 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and upper(Keterangan) = '" & TextBox13.Text.Trim.ToUpper & "' and Id_Sub_Kategori_Jenis_2 <> '" & xid_sub_kategori2 & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("sub ketegori 2 sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'SQL = "select Id_Sub_Kategori_Jenis_2 from Barang_Lain "
                'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and Id_Sub_Kategori_Jenis_2 = '" & xid_sub_kategori2 & "' "
                'Using dr = OpenTrans(SQL)
                '    If dr.Read Then
                '        dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("sub ketegori 2 sudah pernah dipakai di tabel barang ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                SQL = "update N_EMI_Master_Sub_Kategori_Jenis_2 set "
                SQL = SQL & "Keterangan = '" & TextBox13.Text.ToUpper & "' "
                'SQL = SQL & "Prefix = '" & TextBox14.Text.Trim & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis_2 = '" & xid_sub_kategori2 & "' "
                'SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori4.Item(ComboBox6.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & arrsubkateogori4.Item(ComboBox7.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_1 = '" & arrsub1kateogori4.Item(ComboBox8.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_2 = '" & TextBox12.Text & "' "
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong4()
    End Sub

    Private Sub BtnHapus4_Click(sender As Object, e As EventArgs) Handles BtnHapus4.Click
        If TextBox12.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox12.Focus() : Exit Sub
        ElseIf TextBox13.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox13.Focus() : Exit Sub
        ElseIf ComboBox6.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus() : Exit Sub
        ElseIf ComboBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox7.Focus() : Exit Sub
        ElseIf ComboBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori 1 Jenis Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox8.Focus() : Exit Sub
        ElseIf TextBox14.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox14.Focus() : Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show("Mau Hapus data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction
                SQL = "select Id_Sub_Kategori_Jenis_2 from N_EMI_Master_Sub_Kategori_Jenis_3 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis_2 = '" & xid_sub_kategori2 & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak bisa dihapus karena Sub Kategori """ & TextBox12.Text & """ sudah dipakai oleh Sub Kategori 3.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "DELETE FROM N_EMI_Master_Sub_Kategori_Jenis_2 "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Sub_Kategori_Jenis_2 = '" & xid_sub_kategori2 & "' "
                'SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori4.Item(ComboBox6.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & arrsubkateogori4.Item(ComboBox7.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_1 = '" & arrsub1kateogori4.Item(ComboBox8.SelectedIndex) & "' "
                'SQL = SQL & "and Kode_Sub_Kategori_Jenis_2 = '" & TextBox12.Text & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show("Data berhasil dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong4()
    End Sub

    Private Sub TextBox8_Leave(sender As Object, e As EventArgs) Handles TextBox8.Leave
        If TextBox8.Text.Trim.Length = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select  a.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c, N_EMI_Master_Role_Sub_Kategori g "
            SQL = SQL & "where b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = g.Kode_Perusahaan and b.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
            SQL = SQL & "and b.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Sub_Kategori_Jenis_1 = '" & xid_sub_kategori1 & "' "

            SQL = "select  c.Kode_Kategori_Jenis, c.Keterangan as Kategori_Jenis, b.Kode_Sub_Kategori_Jenis, b.Keterangan as Sub_Kategori_Jenis,"
            SQL = SQL & "a.Kode_Sub_Kategori_Jenis_1, a.Keterangan as Sub_Kategori_Jenis1, a.Prefix, "
            SQL = SQL & "a.Id_Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_1 a, N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c, N_EMI_Master_Role_Sub_Kategori g "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = g.Kode_Perusahaan and b.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
            SQL = SQL & "and b.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Sub_Kategori_Jenis_1 = '" & xid_sub_kategori1 & "' "
            'SQL = SQL & "and a.Kode_Kategori_Jenis = '" & arrkateogori3.Item(ComboBox3.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis = '" & arrsubkateogori3.Item(ComboBox4.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = '" & TextBox8.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox9.Text = dr("Sub_Kategori_Jenis1")
                    TextBox10.Text = dr("Prefix")
                    TextBox8.Enabled = False
                    ComboBox3.Enabled = False
                    ComboBox4.Enabled = False

                    BtnSimpan3.Text = "&Update"
                    BtnHapus3.Enabled = True
                    BtnSimpan3.Tag = "&Update"
                Else
                    TextBox8.Enabled = True
                    ComboBox3.Enabled = True
                    ComboBox4.Enabled = True
                    TextBox9.Text = ""
                    'TextBox10.Text = ""
                    xid_sub_kategori1 = ""
                    BtnSimpan3.Text = "&Simpan"
                    BtnHapus3.Enabled = False
                    BtnSimpan3.Tag = "&Simpan"
                End If
            End Using

            If BtnSimpan3.Tag = "&Simpan" Then
                get_no_prefix3()
                If xprefix3 = "0*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 1 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix3 > 99 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 1 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                TextBox10.Text = xprefix3
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox6.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox7.Focus()
    End Sub

    Private Sub ComboBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox7.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox8.Focus()
    End Sub

    Private Sub ComboBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox8.KeyPress
        If e.KeyChar = Chr(13) Then TextBox12.Focus()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

        If Asal_proses = "" Then
            If TabControl1.SelectedTab Is TabPage1 Then

                kosong()

            ElseIf TabControl1.SelectedTab Is TabPage2 Then

                kosong2()

            ElseIf TabControl1.SelectedTab Is TabPage3 Then

                kosong3()

            ElseIf TabControl1.SelectedTab Is TabPage4 Then

                kosong4()

            ElseIf TabControl1.SelectedTab Is TabPage5 Then

                kosong5()
            End If

        End If

    End Sub

    Private Sub ComboBox14_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSK3_JenisSub2.SelectedIndexChanged

    End Sub

    Private Sub TextBox12_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox12.KeyPress
        If e.KeyChar = Chr(13) Then TextBox13.Focus()
    End Sub

    Private Sub TextBox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox13.KeyPress
        If e.KeyChar = Chr(13) Then TextBox14.Focus()
    End Sub

    Private Sub TextBox14_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox14.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan4.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox12_Leave(sender As Object, e As EventArgs) Handles TextBox12.Leave
        If TextBox12.Text.Trim.Length = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select d.Kode_Kategori_Jenis, d.Keterangan as Kategori_Jenis, c.Kode_Sub_Kategori_Jenis, c.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "b.Kode_Sub_Kategori_Jenis_1, b.Keterangan as Sub_Kategori_Jenis_1, a.Kode_Sub_Kategori_Jenis_2, a.Keterangan as Sub_Kategori_Jenis_2, a.Prefix, "
            SQL = SQL & "a.Id_Sub_Kategori_Jenis_2, a.Id_Sub_Kategori_Jenis_1, b.Id_Sub_Kategori_Jenis, c.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_2 a, N_EMI_Master_Sub_Kategori_Jenis_1 b, "
            SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis c, N_EMI_Master_Kategori_Jenis d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_1 = b.Id_Sub_Kategori_Jenis_1 "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Jenis = d.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Sub_Kategori_Jenis_2 = '" & xid_sub_kategori2 & "' "
            'SQL = SQL & "and a.Kode_Kategori_Jenis = '" & arrkateogori4.Item(ComboBox6.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis = '" & arrsubkateogori4.Item(ComboBox7.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = '" & arrsub1kateogori4.Item(ComboBox8.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_2 = '" & TextBox12.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox13.Text = dr("Sub_Kategori_Jenis_2")
                    TextBox14.Text = dr("Prefix")
                    TextBox12.Enabled = False
                    ComboBox6.Enabled = False
                    ComboBox7.Enabled = False
                    ComboBox8.Enabled = False

                    BtnSimpan4.Text = "&Update"
                    BtnHapus4.Enabled = True
                    BtnSimpan4.Tag = "&Update"
                Else
                    TextBox12.Enabled = True
                    ComboBox6.Enabled = True
                    ComboBox7.Enabled = True
                    ComboBox8.Enabled = True
                    xid_sub_kategori2 = ""
                    TextBox13.Text = ""
                    'TextBox14.Text = ""
                    BtnSimpan4.Text = "&Simpan"
                    BtnHapus4.Enabled = False
                    BtnSimpan4.Tag = "&Simpan"
                End If
            End Using

            If BtnSimpan4.Tag = "&Simpan" Then
                get_no_prefix4()
                If xprefix4 = "0*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 2 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix4 > 99 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 2 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                TextBox14.Text = xprefix4
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView4_DoubleClick(sender As Object, e As EventArgs) Handles ListView4.DoubleClick
        ComboBox6.Text = ListView4.FocusedItem.SubItems(2).Text
        ComboBox7.Text = ListView4.FocusedItem.SubItems(5).Text
        ComboBox8.Text = ListView4.FocusedItem.SubItems(8).Text
        xid_sub_kategori2 = ListView4.FocusedItem.SubItems(9).Text
        TextBox12.Text = ListView4.FocusedItem.SubItems(10).Text

        TextBox12_Leave(ListView4, e)
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            ComboBox7.Items.Clear() : arrsubkateogori4.Clear() : arridsub3.Clear()
            ComboBox8.Items.Clear() : arrsub1kateogori4.Clear() : arrid2sub3.Clear() : TextBox14.Text = ""
            SQL = "select a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, a.Id_Sub_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Id_Kategori_Jenis = '" & arrid3.Item(ComboBox6.SelectedIndex) & "' "
            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox7.Items.Add(dr("Sub_Kategori_Jenis"))
                    arrsubkateogori4.Add(dr("Kode_Sub_Kategori_Jenis"))
                    arridsub3.Add(dr("Id_Sub_Kategori_Jenis"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        If ComboBox7.Text.Trim.Length = 0 Then Exit Sub
        If ComboBox6.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select  b.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c, N_EMI_Master_Role_Sub_Kategori g "
            SQL = SQL & "where b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = g.Kode_Perusahaan and b.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
            SQL = SQL & "and b.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "
            SQL = SQL & "and b.Kode_Perusahaan =  '" & KodePerusahaan & "' and b.Id_Kategori_Jenis = '" & arrid3.Item(ComboBox6.SelectedIndex) & "' "
            SQL = SQL & "and b.Id_Sub_Kategori_Jenis = '" & arridsub3.Item(ComboBox7.SelectedIndex) & "' "
            Using dr = OpenTrans(SQL)
                If Not dr.Read Then
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("anda tidak Memiliki akses ke kategori dan sub kategori ini ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ComboBox7.SelectedIndex = -1 : Exit Sub
                End If
            End Using

            ComboBox8.Items.Clear() : arrsub1kateogori4.Clear() : arrid2sub3.Clear() : TextBox14.Text = ""
            SQL = "select a.Kode_Sub_Kategori_Jenis_1, a.Keterangan as Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis_1 "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_1 a, N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Sub_Kategori_Jenis = '" & arridsub3.Item(ComboBox7.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Kategori_Jenis = '" & arrkateogori4.Item(ComboBox6.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis = '" & arrsubkateogori4.Item(ComboBox7.SelectedIndex) & "' "
            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox8.Items.Add(dr("Sub_Kategori_Jenis_1"))
                    arrsub1kateogori4.Add(dr("Kode_Sub_Kategori_Jenis_1"))
                    arrid2sub3.Add(dr("Id_Sub_Kategori_Jenis_1"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox10.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox4.Text = ""

        If TextBox7.Text.Trim.Length <> 0 Then
            TextBox7_Leave(ComboBox1, e)
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        TextBox10.Text = ""
        If ComboBox3.SelectedIndex = -1 Then
            Exit Sub
        ElseIf ComboBox4.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select  b.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c, N_EMI_Master_Role_Sub_Kategori g "
            SQL = SQL & "where b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = g.Kode_Perusahaan and b.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
            SQL = SQL & "and b.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "
            SQL = SQL & "and b.Kode_Perusahaan =  '" & KodePerusahaan & "' and b.Id_Kategori_Jenis = '" & arrid2.Item(ComboBox3.SelectedIndex) & "' "
            SQL = SQL & "and b.Id_Sub_Kategori_Jenis = '" & arridsub2.Item(ComboBox4.SelectedIndex) & "' "
            Using dr = OpenTrans(SQL)
                If Not dr.Read Then
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("anda tidak Memiliki akses ke kategori dan sub kategori ini ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ComboBox4.SelectedIndex = -1 : Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If TextBox8.Text.Trim.Length <> 0 Then
            TextBox8_Leave(ComboBox4, e)
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        TextBox14.Text = ""

        If TextBox12.Text.Trim.Length <> 0 Then
            TextBox12_Leave(ComboBox8, e)
        End If
    End Sub

    Public Sub ComboBox11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSK3_Jenis.SelectedIndexChanged
        If CmbSK3_Jenis.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            CmbSK3_JenisSub.Items.Clear() : arrsubkateogori5.Clear() : arridsub4.Clear()
            CmbSK3_JenisSub1.Items.Clear() : arrsub1kateogori5.Clear() : arrid2sub4.Clear() : TextBox18.Text = ""
            CmbSK3_JenisSub2.Items.Clear() : arrsub2kateogori5.Clear() : arrid3sub4.Clear()
            SQL = "select a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, a.Id_Sub_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Id_Kategori_Jenis = '" & arrid4.Item(CmbSK3_Jenis.SelectedIndex) & "' "
            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSK3_JenisSub.Items.Add(dr("Sub_Kategori_Jenis"))
                    arrsubkateogori5.Add(dr("Kode_Sub_Kategori_Jenis"))
                    arridsub4.Add(dr("Id_Sub_Kategori_Jenis"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub ComboBox12_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSK3_JenisSub.SelectedIndexChanged
        If CmbSK3_JenisSub.Text.Trim.Length = 0 Then Exit Sub
        If CmbSK3_Jenis.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select  b.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c, N_EMI_Master_Role_Sub_Kategori g "
            SQL = SQL & "where b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = g.Kode_Perusahaan and b.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
            SQL = SQL & "and b.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "
            SQL = SQL & "and b.Kode_Perusahaan =  '" & KodePerusahaan & "' and b.Id_Kategori_Jenis = '" & arrid4.Item(CmbSK3_Jenis.SelectedIndex) & "' "
            SQL = SQL & "and b.Id_Sub_Kategori_Jenis = '" & arridsub4.Item(CmbSK3_JenisSub.SelectedIndex) & "' "
            Using dr = OpenTrans(SQL)
                If Not dr.Read Then
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("anda tidak Memiliki akses ke kategori dan sub kategori ini ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    CmbSK3_JenisSub.SelectedIndex = -1 : Exit Sub
                End If
            End Using

            CmbSK3_JenisSub1.Items.Clear() : arrsub1kateogori5.Clear() : arrid2sub4.Clear() : TextBox18.Text = ""
            CmbSK3_JenisSub2.Items.Clear() : arrsub2kateogori5.Clear() : arrid3sub4.Clear()
            SQL = "select a.Kode_Sub_Kategori_Jenis_1, a.Keterangan as Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis_1 "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_1 a, N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Sub_Kategori_Jenis = '" & arridsub4.Item(CmbSK3_JenisSub.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Kategori_Jenis = '" & arrkateogori4.Item(ComboBox6.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis = '" & arrsubkateogori4.Item(ComboBox7.SelectedIndex) & "' "
            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSK3_JenisSub1.Items.Add(dr("Sub_Kategori_Jenis_1"))
                    arrsub1kateogori5.Add(dr("Kode_Sub_Kategori_Jenis_1"))
                    arrid2sub4.Add(dr("Id_Sub_Kategori_Jenis_1"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub ComboBox13_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSK3_JenisSub1.SelectedIndexChanged
        If CmbSK3_JenisSub1.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            CmbSK3_JenisSub2.Items.Clear() : arrsub2kateogori5.Clear() : arrid3sub4.Clear() : TextBox18.Text = ""
            SQL = "select a.Kode_Sub_Kategori_Jenis_2, a.Keterangan as Sub_Kategori_Jenis_2, a.Id_Sub_Kategori_Jenis_2 "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_2 a, N_EMI_Master_Sub_Kategori_Jenis_1 b, N_EMI_Master_Sub_Kategori_Jenis c, N_EMI_Master_Kategori_Jenis d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_1 = b.Id_Sub_Kategori_Jenis_1 "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Jenis = d.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Sub_Kategori_Jenis_1 = '" & arrid2sub4.Item(CmbSK3_JenisSub1.SelectedIndex) & "' "
            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSK3_JenisSub2.Items.Add(dr("Sub_Kategori_Jenis_2"))
                    arrsub2kateogori5.Add(dr("Kode_Sub_Kategori_Jenis_2"))
                    arrid3sub4.Add(dr("Id_Sub_Kategori_Jenis_2"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox16_Leave(sender As Object, e As EventArgs) Handles TextBox16.Leave
        If TextBox16.Text.Trim.Length = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select e.Kode_Kategori_Jenis, e.Keterangan as Kategori_Jenis, d.Kode_Sub_Kategori_Jenis, d.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "c.Kode_Sub_Kategori_Jenis_1, c.Keterangan as Sub_Kategori_Jenis_1, b.Kode_Sub_Kategori_Jenis_2, b.Keterangan as Sub_Kategori_Jenis_2, "
            SQL = SQL & "a.Kode_Sub_Kategori_Jenis_3, a.Keterangan as Sub_Kategori_Jenis_3, a.Prefix, "
            SQL = SQL & "a.Id_Sub_Kategori_Jenis_3, a.Id_Sub_Kategori_Jenis_2, b.Id_Sub_Kategori_Jenis_1, c.Id_Sub_Kategori_Jenis, d.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a, N_EMI_Master_Sub_Kategori_Jenis_2 b, N_EMI_Master_Sub_Kategori_Jenis_1 c, "
            SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis d, N_EMI_Master_Kategori_Jenis e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_2 = b.Id_Sub_Kategori_Jenis_2 "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis_1 = c.Id_Sub_Kategori_Jenis_1 "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis "
            SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Sub_Kategori_Jenis_3 = '" & xid_sub_kategori3 & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox17.Text = dr("Sub_Kategori_Jenis_3")
                    TextBox18.Text = dr("Prefix")
                    TextBox16.Enabled = False
                    CmbSK3_Jenis.Enabled = False
                    CmbSK3_JenisSub.Enabled = False
                    CmbSK3_JenisSub1.Enabled = False
                    CmbSK3_JenisSub2.Enabled = False

                    BtnSimpan5.Text = "&Update"
                    BtnHapus5.Enabled = True
                    BtnSimpan5.Tag = "&Update"
                Else
                    TextBox16.Enabled = True
                    CmbSK3_Jenis.Enabled = True
                    CmbSK3_JenisSub.Enabled = True
                    CmbSK3_JenisSub1.Enabled = True
                    CmbSK3_JenisSub2.Enabled = True
                    xid_sub_kategori3 = ""
                    TextBox17.Text = ""
                    'TextBox18.Text = ""
                    BtnSimpan5.Text = "&Simpan"
                    BtnHapus5.Enabled = False
                    BtnSimpan5.Tag = "&Simpan"
                End If
            End Using

            If BtnSimpan5.Tag = "&Simpan" Then
                get_no_prefix5()
                If xprefix5 = "00*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 3 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix5 > 999 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori 3 sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                TextBox18.Text = xprefix5
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView5_DoubleClick(sender As Object, e As EventArgs) Handles ListView5.DoubleClick
        CmbSK3_Jenis.Text = ListView5.FocusedItem.SubItems(2).Text
        CmbSK3_JenisSub.Text = ListView5.FocusedItem.SubItems(5).Text
        CmbSK3_JenisSub1.Text = ListView5.FocusedItem.SubItems(8).Text
        CmbSK3_JenisSub2.Text = ListView5.FocusedItem.SubItems(11).Text
        xid_sub_kategori3 = ListView5.FocusedItem.SubItems(12).Text
        TextBox16.Text = ListView5.FocusedItem.SubItems(13).Text

        TextBox16_Leave(ListView5, e)
    End Sub

    Private Sub ComboBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSK3_Jenis.KeyPress
        If e.KeyChar = Chr(13) Then CmbSK3_JenisSub.Focus()
    End Sub

    Private Sub ComboBox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSK3_JenisSub1.KeyPress
        If e.KeyChar = Chr(13) Then CmbSK3_JenisSub2.Focus()
    End Sub

    Private Sub ComboBox12_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSK3_JenisSub.KeyPress
        If e.KeyChar = Chr(13) Then CmbSK3_JenisSub1.Focus()
    End Sub

    Private Sub ComboBox14_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSK3_JenisSub2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox16.Focus()
    End Sub

    Private Sub TextBox16_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox16.KeyPress
        If e.KeyChar = Chr(13) Then TextBox17.Focus()
    End Sub

    Private Sub TextBox17_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox17.KeyPress
        If e.KeyChar = Chr(13) Then TextBox18.Focus()
    End Sub

    Private Sub TextBox18_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox18.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan5.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        'If Asal_proses <> "" Then
        '    ' Jika tab yang dipilih bukan tab ke-4, batalkan perpindahan
        '    If e.TabPageIndex <> 4 Then
        '        e.Cancel = True
        '    End If

        'End If
        If Asal_proses = "pengajuan_barang_baru" Then
            If e.TabPageIndex = 0 Then
                e.Cancel = True
            End If


        ElseIf Not (Asal_proses = "" Or Asal_proses = "N_EMI_SD_Tambah_PR_Barang_Lain_Departement") Then
            If e.TabPageIndex <> 4 Then
                e.Cancel = True
            End If

        End If
    End Sub
End Class
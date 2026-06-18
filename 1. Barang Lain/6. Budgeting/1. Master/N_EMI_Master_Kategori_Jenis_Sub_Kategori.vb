Imports System.IO

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

	Dim SelectedFilePath As String
	Dim url As String = ""
	Dim namaFileAzure As String = ""
	Dim namaFIleAsli As String = ""
	Dim gambarBerubah As Boolean = False

	Dim isCheck_2, isCheck_3, isCheck_4, isCheck_5 As Boolean
	Dim namaFileGambar As String = ""

	Dim PageSize_1 As Integer = 50
	Dim CurrentPage_1 As Integer = 1
	Dim totalpage_1 As Integer

	Dim PageSize_2 As Integer = 50
	Dim CurrentPage_2 As Integer = 1
	Dim totalpage_2 As Integer

	Dim PageSize_3 As Integer = 50
	Dim CurrentPage_3 As Integer = 1
	Dim totalpage_3 As Integer

	Dim PageSize_4 As Integer = 50
	Dim CurrentPage_4 As Integer = 1
	Dim totalpage_4 As Integer

	Dim PageSize_5 As Integer = 50
	Dim CurrentPage_5 As Integer = 1
	Dim totalpage_5 As Integer

	Dim harusInsert As Boolean = False

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
		If CmbSatuan_Kolom.SelectedIndex = -1 Then
			MessageBox.Show("ComboBox Filter Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			CmbSatuan_Kolom.DroppedDown = True
			CmbSatuan_Kolom.Focus()
			Exit Sub
		ElseIf TxtSatuan_Value.Text.Trim.Length = 0 Then
			MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtSatuan_Value.Focus()
			Exit Sub
		End If

		Load_Data_Tab_1()

		'If CmbSatuan_Kolom.Text.Trim.Length = 0 Then Exit Sub
		'If TxtSatuan_Value.Text.Trim.Length = 0 Then Exit Sub

		'Cari("T")
	End Sub

	Private Sub Cari_X(ByVal semua As String)
		'Try
		'    OpenConn()

		'    ListView1.Items.Clear()
		'    SQL = "select Kode_Kategori_Jenis, Keterangan, Prefix, Id_Kategori_Jenis, Flag_Aktif from N_EMI_Master_Kategori_Jenis "
		'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
		'    If semua = "T" Then
		'        SQL = SQL & "and " & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
		'        SQL = SQL & "order by " & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " "
		'    Else
		'        SQL = SQL & "order by Keterangan"
		'    End If
		'    Using dr = OpenTrans(SQL)
		'        Do While dr.Read
		'            Dim Lvw As ListViewItem
		'            Lvw = ListView1.Items.Add(dr("Id_Kategori_Jenis"))
		'            Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
		'            Lvw.SubItems.Add(dr("Keterangan"))
		'            Lvw.SubItems.Add(dr("Prefix"))
		'            Lvw.SubItems.Add(dr("Flag_Aktif"))
		'        Loop
		'    End Using

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try
	End Sub

	Private Sub Load_Data_Tab_1(Optional ByVal page As Integer = 1)
		Try
			OpenConn()

			'==========================
			'=     GET TOTAL DATA     =
			'==========================
			Dim Tot_Data As Integer = 0
			SQL = "select COUNT(*) AS TotalData "
			SQL = SQL & "FROM N_EMI_Master_Kategori_Jenis "
			SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
			If CmbSatuan_Kolom.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
			End If
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Tot_Data = Dr("TotalData")
				End If
			End Using

			'==========================
			'=     SET PAGINATION     =
			'==========================
			Dim totalPages As Integer = Math.Ceiling(Tot_Data / PageSize_1)
			Dim offset As Integer = (page - 1) * PageSize_1
			totalpage_1 = totalPages
			Txt_Pages_1.Text = $"{page} of {totalPages}"

			If totalpage_1 = 1 Then
				BtnPrev_1.Enabled = False
				BtnNext_1.Enabled = False
			Else
				BtnPrev_1.Enabled = True
				BtnNext_1.Enabled = True
			End If

			ListView1.Items.Clear()
			SQL = "select Kode_Kategori_Jenis, Keterangan, Prefix, Id_Kategori_Jenis, Flag_Aktif from N_EMI_Master_Kategori_Jenis "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			If CmbSatuan_Kolom.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
			End If
			SQL = SQL & "order by Keterangan "
			SQL = SQL & "OFFSET " & offset & " ROWS "
			SQL = SQL & "FETCH NEXT " & PageSize_1 & " ROWS ONLY "
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

	'Private Sub Cari2(ByVal semua As String)
	'    Try
	'        OpenConn()

	'        ListView2.Items.Clear()
	'        SQL = "select b.Kode_Kategori_Jenis, b.Keterangan as Kategori_Jenis, a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, "
	'        SQL = SQL & "a.Prefix, a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
	'        SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
	'        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
	'        SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
	'        If semua = "T" Then
	'            SQL = SQL & "and " & arrcari2.Item(ComboBox2.SelectedIndex) & " like '%" & TextBox5.Text & "%' "
	'            SQL = SQL & "order by " & arrcari2.Item(ComboBox2.SelectedIndex) & " "
	'        Else
	'            SQL = SQL & "order by a.Keterangan "
	'        End If
	'        Using dr = OpenTrans(SQL)
	'            Do While dr.Read
	'                Dim Lvw As ListViewItem
	'                Lvw = ListView2.Items.Add(dr("Id_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Prefix"))
	'            Loop
	'        End Using

	'        CloseConn()
	'    Catch ex As Exception
	'        CloseConn()
	'        MessageBox.Show(ex.Message)
	'        Exit Sub
	'    End Try
	'End Sub

	Private Sub Load_Data_Tab_2(Optional ByVal page As Integer = 1)
		Try
			OpenConn()

			'==========================
			'=     GET TOTAL DATA     =
			'==========================
			Dim Tot_Data As Integer = 0
			'SQL = "select COUNT(*) AS TotalData "
			'SQL = SQL & "FROM N_EMI_Master_Sub_Kategori_Jenis "
			'SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = "select COUNT(*) AS TotalData "
			SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
			If ComboBox2.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari2.Item(ComboBox2.SelectedIndex) & " like '%" & TextBox5.Text & "%' "
			End If
			If isCheck_2 Then
				SQL = SQL & "and a.Id_Kategori_Jenis = '" & arrid(ComboBox1.SelectedIndex) & "' "
			End If
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Tot_Data = Dr("TotalData")
				End If
			End Using

			'==========================
			'=     SET PAGINATION     =
			'==========================
			Dim totalPages As Integer = Math.Ceiling(Tot_Data / PageSize_2)
			Dim offset As Integer = (page - 1) * PageSize_2
			totalpage_2 = totalPages
			Txt_Pages_2.Text = $"{page} of {totalPages}"

			If totalpage_2 = 1 Then
				BtnPrev_2.Enabled = False
				BtnNext_2.Enabled = False
			Else
				BtnPrev_2.Enabled = True
				BtnNext_2.Enabled = True
			End If

			ListView2.Items.Clear()
			SQL = "select b.Kode_Kategori_Jenis, b.Keterangan as Kategori_Jenis, a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, "
			SQL = SQL & "a.Prefix, a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
			SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
			If ComboBox2.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari2.Item(ComboBox2.SelectedIndex) & " like '%" & TextBox5.Text & "%' "
			End If
			If isCheck_2 Then
				SQL = SQL & "and a.Id_Kategori_Jenis = '" & arrid(ComboBox1.SelectedIndex) & "' "
			End If
			SQL = SQL & "order by a.Keterangan "
			SQL = SQL & "OFFSET " & offset & " ROWS "
			SQL = SQL & "FETCH NEXT " & PageSize_2 & " ROWS ONLY "

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

	'Private Sub Cari3(ByVal semua As String)
	'    Try
	'        OpenConn()

	'        ListView3.Items.Clear()
	'        SQL = "select  c.Kode_Kategori_Jenis, c.Keterangan as Kategori_Jenis, b.Kode_Sub_Kategori_Jenis, b.Keterangan as Sub_Kategori_Jenis,"
	'        SQL = SQL & "a.Kode_Sub_Kategori_Jenis_1, a.Keterangan as Sub_Kategori_Jenis1, a.Prefix, "
	'        SQL = SQL & "a.Id_Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
	'        SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_1 a, N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c "
	'        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.Id_Sub_Kategori_Jenis "
	'        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
	'        SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
	'        If semua = "T" Then
	'            SQL = SQL & "and " & arrcari3.Item(ComboBox5.SelectedIndex) & " like '%" & TextBox11.Text & "%' "
	'            SQL = SQL & "order by " & arrcari3.Item(ComboBox5.SelectedIndex) & " "
	'        Else
	'            SQL = SQL & "order by a.Keterangan "
	'        End If
	'        Using dr = OpenTrans(SQL)
	'            Do While dr.Read
	'                Dim Lvw As ListViewItem
	'                Lvw = ListView3.Items.Add(dr("Id_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis1"))
	'                Lvw.SubItems.Add(dr("Prefix"))
	'            Loop
	'        End Using

	'        CloseConn()
	'    Catch ex As Exception
	'        CloseConn()
	'        MessageBox.Show(ex.Message)
	'        Exit Sub
	'    End Try
	'End Sub

	Private Sub Load_Data_Tab_3(Optional ByVal page As Integer = 1)
		Try
			OpenConn()

			'==========================
			'=     GET TOTAL DATA     =
			'==========================
			Dim Tot_Data As Integer = 0
			'SQL = "select COUNT(*) AS TotalData "
			'SQL = SQL & "FROM N_EMI_Master_Sub_Kategori_Jenis "
			'SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "

			SQL = "select COUNT(*) AS TotalData "
			SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_1 a, N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.Id_Sub_Kategori_Jenis "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
			If ComboBox5.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari3.Item(ComboBox5.SelectedIndex) & " like '%" & TextBox11.Text & "%' "
			End If
			If isCheck_3 Then
				If ComboBox3.SelectedIndex <> -1 Then
					SQL = SQL & "AND b.Id_Kategori_Jenis = '" & arrid2(ComboBox3.SelectedIndex) & "' "
				End If
				If ComboBox4.SelectedIndex <> -1 Then
					SQL = SQL & "AND a.Id_Sub_Kategori_Jenis = '" & arridsub2(ComboBox4.SelectedIndex) & "' "
				End If
			End If
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Tot_Data = Dr("TotalData")
				End If
			End Using

			'==========================
			'=     SET PAGINATION     =
			'==========================
			Dim totalPages As Integer = Math.Ceiling(Tot_Data / PageSize_3)
			Dim offset As Integer = (page - 1) * PageSize_3
			totalpage_3 = totalPages
			Txt_Pages_3.Text = $"{page} of {totalPages}"

			If totalpage_3 = 1 Then
				BtnPrev_3.Enabled = False
				BtnNext_3.Enabled = False
			Else
				BtnPrev_3.Enabled = True
				BtnNext_3.Enabled = True
			End If

			ListView3.Items.Clear()
			SQL = "select  c.Kode_Kategori_Jenis, c.Keterangan as Kategori_Jenis, b.Kode_Sub_Kategori_Jenis, b.Keterangan as Sub_Kategori_Jenis,"
			SQL = SQL & "a.Kode_Sub_Kategori_Jenis_1, a.Keterangan as Sub_Kategori_Jenis1, a.Prefix, "
			SQL = SQL & "a.Id_Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
			SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_1 a, N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.Id_Sub_Kategori_Jenis "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
			If ComboBox5.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari3.Item(ComboBox5.SelectedIndex) & " like '%" & TextBox11.Text & "%' "
			End If
			If isCheck_3 Then
				If ComboBox3.SelectedIndex <> -1 Then
					SQL = SQL & "AND b.Id_Kategori_Jenis = '" & arrid2(ComboBox3.SelectedIndex) & "' "
				End If
				If ComboBox4.SelectedIndex <> -1 Then
					SQL = SQL & "AND a.Id_Sub_Kategori_Jenis = '" & arridsub2(ComboBox4.SelectedIndex) & "' "
				End If
			End If
			SQL = SQL & "order by a.Keterangan "
			SQL = SQL & "OFFSET " & offset & " ROWS "
			SQL = SQL & "FETCH NEXT " & PageSize_3 & " ROWS ONLY "
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

	'Private Sub Cari4(ByVal semua As String)
	'    Try
	'        OpenConn()

	'        ListView4.Items.Clear()
	'        SQL = "select d.Kode_Kategori_Jenis, d.Keterangan as Kategori_Jenis, c.Kode_Sub_Kategori_Jenis, c.Keterangan as Sub_Kategori_Jenis, "
	'        SQL = SQL & "b.Kode_Sub_Kategori_Jenis_1, b.Keterangan as Sub_Kategori_Jenis_1, a.Kode_Sub_Kategori_Jenis_2, a.Keterangan as Sub_Kategori_Jenis_2, a.Prefix, "
	'        SQL = SQL & "a.Id_Sub_Kategori_Jenis_2, a.Id_Sub_Kategori_Jenis_1, b.Id_Sub_Kategori_Jenis, c.Id_Kategori_Jenis "
	'        SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_2 a, N_EMI_Master_Sub_Kategori_Jenis_1 b, "
	'        SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis c, N_EMI_Master_Kategori_Jenis d "
	'        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_1 = b.Id_Sub_Kategori_Jenis_1 "
	'        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis "
	'        SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Jenis = d.Id_Kategori_Jenis "
	'        SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
	'        If semua = "T" Then
	'            SQL = SQL & "and " & arrcari4.Item(ComboBox9.SelectedIndex) & " like '%" & TextBox15.Text & "%' "
	'            SQL = SQL & "order by " & arrcari4.Item(ComboBox9.SelectedIndex) & " "
	'        Else
	'            SQL = SQL & "order by a.Keterangan "
	'        End If
	'        Using dr = OpenTrans(SQL)
	'            Do While dr.Read
	'                Dim Lvw As ListViewItem
	'                Lvw = ListView4.Items.Add(dr("Id_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_1"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_2"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_2"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_2"))
	'                Lvw.SubItems.Add(dr("Prefix"))

	'            Loop
	'        End Using

	'        CloseConn()
	'    Catch ex As Exception
	'        CloseConn()
	'        MessageBox.Show(ex.Message)
	'        Exit Sub
	'    End Try
	'End Sub

	Private Sub Load_Data_Tab_4(Optional ByVal page As Integer = 1)
		Try
			OpenConn()

			'==========================
			'=     GET TOTAL DATA     =
			'==========================
			Dim Tot_Data As Integer = 0
			'SQL = "select COUNT(*) AS TotalData "
			'SQL = SQL & "FROM N_EMI_Master_Sub_Kategori_Jenis "
			'SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "

			SQL = "select COUNT(*) AS TotalData "
			SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_2 a, N_EMI_Master_Sub_Kategori_Jenis_1 b, "
			SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis c, N_EMI_Master_Kategori_Jenis d "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_1 = b.Id_Sub_Kategori_Jenis_1 "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis "
			SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Jenis = d.Id_Kategori_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
			If ComboBox9.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari4.Item(ComboBox9.SelectedIndex) & " like '%" & TextBox15.Text & "%' "
			End If
			If isCheck_4 Then
				If ComboBox6.SelectedIndex <> -1 Then
					SQL = SQL & "AND c.Id_Kategori_Jenis = '" & arrid3(ComboBox6.SelectedIndex) & "' "
				End If
				If ComboBox7.SelectedIndex <> -1 Then
					SQL = SQL & "AND b.Id_Sub_Kategori_Jenis = '" & arridsub3(ComboBox7.SelectedIndex) & "' "
				End If
				If ComboBox8.SelectedIndex <> -1 Then
					SQL = SQL & "AND a.Id_Sub_Kategori_Jenis_1 = '" & arrid2sub3(ComboBox8.SelectedIndex) & "' "
				End If
			End If
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Tot_Data = Dr("TotalData")
				End If
			End Using

			'==========================
			'=     SET PAGINATION     =
			'==========================
			Dim totalPages As Integer = Math.Ceiling(Tot_Data / PageSize_4)
			Dim offset As Integer = (page - 1) * PageSize_4
			totalpage_4 = totalPages
			Txt_Pages_4.Text = $"{page} of {totalPages}"

			If totalpage_4 = 1 Then
				BtnPrev_4.Enabled = False
				BtnNext_4.Enabled = False
			Else
				BtnPrev_4.Enabled = True
				BtnNext_4.Enabled = True
			End If

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

			If ComboBox9.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari4.Item(ComboBox9.SelectedIndex) & " like '%" & TextBox15.Text & "%' "
			End If
			If isCheck_4 Then
				If ComboBox6.SelectedIndex <> -1 Then
					SQL = SQL & "AND c.Id_Kategori_Jenis = '" & arrid3(ComboBox6.SelectedIndex) & "' "
				End If
				If ComboBox7.SelectedIndex <> -1 Then
					SQL = SQL & "AND b.Id_Sub_Kategori_Jenis = '" & arridsub3(ComboBox7.SelectedIndex) & "' "
				End If
				If ComboBox8.SelectedIndex <> -1 Then
					SQL = SQL & "AND a.Id_Sub_Kategori_Jenis_1 = '" & arrid2sub3(ComboBox8.SelectedIndex) & "' "
				End If
			End If
			SQL = SQL & "order by a.Keterangan "
			SQL = SQL & "OFFSET " & offset & " ROWS "
			SQL = SQL & "FETCH NEXT " & PageSize_4 & " ROWS ONLY "
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

	'Private Sub Cari5(ByVal semua As String)
	'    Try
	'        OpenConn()

	'        ListView5.Items.Clear()
	'        SQL = "select e.Kode_Kategori_Jenis, e.Keterangan as Kategori_Jenis, d.Kode_Sub_Kategori_Jenis, d.Keterangan as Sub_Kategori_Jenis, "
	'        SQL = SQL & "c.Kode_Sub_Kategori_Jenis_1, c.Keterangan as Sub_Kategori_Jenis_1, b.Kode_Sub_Kategori_Jenis_2, b.Keterangan as Sub_Kategori_Jenis_2, "
	'        SQL = SQL & "a.Kode_Sub_Kategori_Jenis_3, a.Keterangan as Sub_Kategori_Jenis_3, a.Prefix, "
	'        SQL = SQL & "a.Id_Sub_Kategori_Jenis_3, a.Id_Sub_Kategori_Jenis_2, b.Id_Sub_Kategori_Jenis_1, c.Id_Sub_Kategori_Jenis, d.Id_Kategori_Jenis, "
	'        SQL = SQL & "a.Satuan, a.Stock_Minimum, a.Berat, a.Berat_Kotor, a.Panjang, a.Lebar, a.Tinggi, a.Metode_Pengeluaran_Stok "
	'        SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a, N_EMI_Master_Sub_Kategori_Jenis_2 b, N_EMI_Master_Sub_Kategori_Jenis_1 c, "
	'        SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis d, N_EMI_Master_Kategori_Jenis e "
	'        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_2 = b.Id_Sub_Kategori_Jenis_2 "
	'        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis_1 = c.Id_Sub_Kategori_Jenis_1 "
	'        SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis "
	'        SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis "
	'        SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
	'        If semua = "T" Then
	'            SQL = SQL & "and " & arrcari5.Item(ComboBox15.SelectedIndex) & " like '%" & TextBox19.Text & "%' "

	'        Else
	'            SQL = SQL & "order by a.Keterangan "
	'        End If
	'        Using dr = OpenTrans(SQL)
	'            Do While dr.Read
	'                Dim Lvw As ListViewItem
	'                Lvw = ListView5.Items.Add(dr("Id_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_1"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_2"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_2"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_2"))
	'                Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_3"))
	'                Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_3"))
	'                Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_3"))
	'                Lvw.SubItems.Add(dr("Prefix"))
	'                If General_Class.CekNULL(dr("Satuan")) = "" Then
	'                    Lvw.SubItems.Add("-")
	'                Else
	'                    Lvw.SubItems.Add(dr("Satuan"))
	'                End If

	'                If General_Class.CekNULL(dr("Stock_Minimum")) = "" Then
	'                    Lvw.SubItems.Add("-")
	'                Else
	'                    Lvw.SubItems.Add(dr("Stock_Minimum"))
	'                End If

	'                If General_Class.CekNULL(dr("Berat")) = "" Then
	'                    Lvw.SubItems.Add("-")
	'                Else
	'                    Lvw.SubItems.Add(dr("Berat"))
	'                End If

	'                If General_Class.CekNULL(dr("Berat_Kotor")) = "" Then
	'                    Lvw.SubItems.Add("-")
	'                Else
	'                    Lvw.SubItems.Add(dr("Berat_Kotor"))
	'                End If

	'                If General_Class.CekNULL(dr("Panjang")) = "" Then
	'                    Lvw.SubItems.Add("-")
	'                Else
	'                    Lvw.SubItems.Add(dr("Panjang") & " X " & dr("Lebar") & " X " & dr("Tinggi"))
	'                End If

	'                If General_Class.CekNULL(dr("Metode_Pengeluaran_Stok")) = "" Then
	'                    Lvw.SubItems.Add("-")
	'                Else
	'                    Lvw.SubItems.Add(dr("Metode_Pengeluaran_Stok"))
	'                End If
	'            Loop
	'        End Using

	'        CloseConn()
	'    Catch ex As Exception
	'        CloseConn()
	'        MessageBox.Show(ex.Message)
	'        Exit Sub
	'    End Try
	'End Sub

	Private Sub Load_Data_Tab_5(Optional ByVal page As Integer = 1)
		Try
			OpenConn()

			'==========================
			'=     GET TOTAL DATA     =
			'==========================
			Dim Tot_Data As Integer = 0
			'SQL = "select COUNT(*) AS TotalData "
			'SQL = SQL & "FROM N_EMI_Master_Sub_Kategori_Jenis "
			'SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "

			SQL = "select COUNT(*) AS TotalData "
			SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a, N_EMI_Master_Sub_Kategori_Jenis_2 b, N_EMI_Master_Sub_Kategori_Jenis_1 c, "
			SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis d, N_EMI_Master_Kategori_Jenis e "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_2 = b.Id_Sub_Kategori_Jenis_2 "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis_1 = c.Id_Sub_Kategori_Jenis_1 "
			SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis "
			SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "

			If ComboBox15.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari5.Item(ComboBox15.SelectedIndex) & " like '%" & TextBox19.Text & "%' "
			End If

			If isCheck_5 Then
				If CmbSK3_Jenis.SelectedIndex <> -1 Then
					SQL = SQL & "AND d.Id_Kategori_Jenis = '" & arrid4(CmbSK3_Jenis.SelectedIndex) & "' "
				End If
				If CmbSK3_JenisSub.SelectedIndex <> -1 Then
					SQL = SQL & "AND c.Id_Sub_Kategori_Jenis = '" & arridsub4(CmbSK3_JenisSub.SelectedIndex) & "' "
				End If
				If CmbSK3_JenisSub1.SelectedIndex <> -1 Then
					SQL = SQL & "AND b.Id_Sub_Kategori_Jenis_1 = '" & arrid2sub4(CmbSK3_JenisSub1.SelectedIndex) & "' "
				End If
				If CmbSK3_JenisSub2.SelectedIndex <> -1 Then
					SQL = SQL & "AND a.Id_Sub_Kategori_Jenis_2 = '" & arrid3sub4(CmbSK3_JenisSub2.SelectedIndex) & "' "
				End If
			End If
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Tot_Data = Dr("TotalData")
				End If
			End Using

			'==========================
			'=     SET PAGINATION     =
			'==========================
			Dim totalPages As Integer = Math.Ceiling(Tot_Data / PageSize_5)
			Dim offset As Integer = (page - 1) * PageSize_5
			totalpage_5 = totalPages
			Txt_Pages_5.Text = $"{page} of {totalPages}"

			If totalpage_5 = 1 Then
				BtnPrev_5.Enabled = False
				BtnNext_5.Enabled = False
			Else
				BtnPrev_5.Enabled = True
				BtnNext_5.Enabled = True
			End If

			ListView5.Items.Clear()
			SQL = "select e.Kode_Kategori_Jenis, e.Keterangan as Kategori_Jenis, d.Kode_Sub_Kategori_Jenis, d.Keterangan as Sub_Kategori_Jenis, "
			SQL = SQL & "c.Kode_Sub_Kategori_Jenis_1, c.Keterangan as Sub_Kategori_Jenis_1, b.Kode_Sub_Kategori_Jenis_2, b.Keterangan as Sub_Kategori_Jenis_2, "
			SQL = SQL & "a.Kode_Sub_Kategori_Jenis_3, a.Keterangan as Sub_Kategori_Jenis_3, a.Prefix, "
			SQL = SQL & "a.Id_Sub_Kategori_Jenis_3, a.Id_Sub_Kategori_Jenis_2, b.Id_Sub_Kategori_Jenis_1, c.Id_Sub_Kategori_Jenis, d.Id_Kategori_Jenis, "
			SQL = SQL & "a.Satuan, a.Stock_Minimum, a.Berat, a.Berat_Kotor, a.Panjang, a.Lebar, a.Tinggi, a.Metode_Pengeluaran_Stok "
			SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a, N_EMI_Master_Sub_Kategori_Jenis_2 b, N_EMI_Master_Sub_Kategori_Jenis_1 c, "
			SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis d, N_EMI_Master_Kategori_Jenis e "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_2 = b.Id_Sub_Kategori_Jenis_2 "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis_1 = c.Id_Sub_Kategori_Jenis_1 "
			SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis "
			SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "

			If ComboBox15.SelectedIndex <> -1 Then
				SQL = SQL & "and " & arrcari5.Item(ComboBox15.SelectedIndex) & " like '%" & TextBox19.Text & "%' "
			End If

			If isCheck_5 Then
				If CmbSK3_Jenis.SelectedIndex <> -1 Then
					SQL = SQL & "AND d.Id_Kategori_Jenis = '" & arrid4(CmbSK3_Jenis.SelectedIndex) & "' "
				End If
				If CmbSK3_JenisSub.SelectedIndex <> -1 Then
					SQL = SQL & "AND c.Id_Sub_Kategori_Jenis = '" & arridsub4(CmbSK3_JenisSub.SelectedIndex) & "' "
				End If
				If CmbSK3_JenisSub1.SelectedIndex <> -1 Then
					SQL = SQL & "AND b.Id_Sub_Kategori_Jenis_1 = '" & arrid2sub4(CmbSK3_JenisSub1.SelectedIndex) & "' "
				End If
				If CmbSK3_JenisSub2.SelectedIndex <> -1 Then
					SQL = SQL & "AND a.Id_Sub_Kategori_Jenis_2 = '" & arrid3sub4(CmbSK3_JenisSub2.SelectedIndex) & "' "
				End If
			End If

			SQL = SQL & "order by a.Keterangan "
			SQL = SQL & "OFFSET " & offset & " ROWS "
			SQL = SQL & "FETCH NEXT " & PageSize_5 & " ROWS ONLY "
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
					If General_Class.CekNULL(dr("Satuan")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("Satuan"))
					End If

					If General_Class.CekNULL(dr("Stock_Minimum")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("Stock_Minimum"))
					End If

					If General_Class.CekNULL(dr("Berat")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("Berat"))
					End If

					If General_Class.CekNULL(dr("Berat_Kotor")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("Berat_Kotor"))
					End If

					If General_Class.CekNULL(dr("Panjang")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("Panjang") & " X " & dr("Lebar") & " X " & dr("Tinggi"))
					End If

					If General_Class.CekNULL(dr("Metode_Pengeluaran_Stok")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("Metode_Pengeluaran_Stok"))
					End If
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
		ListView2.Columns.Add("Prefix", 139, HorizontalAlignment.Left)

		ListView3.Columns.Add("id Kategori", 0, HorizontalAlignment.Left)
		ListView3.Columns.Add("kode Kategori", 0, HorizontalAlignment.Left)
		ListView3.Columns.Add("Kategori", 140, HorizontalAlignment.Left)
		ListView3.Columns.Add("id sub Kategori", 0, HorizontalAlignment.Left)
		ListView3.Columns.Add("Kode Sub Kategori", 0, HorizontalAlignment.Left)
		ListView3.Columns.Add("Sub Kategori", 140, HorizontalAlignment.Left)
		ListView3.Columns.Add("id sub Kategori 1", 0, HorizontalAlignment.Left)
		ListView3.Columns.Add("Kode Sub Kategori 1", 140, HorizontalAlignment.Left)
		ListView3.Columns.Add("Sub Kategori 1", 350, HorizontalAlignment.Left)
		ListView3.Columns.Add("Prefix", 139, HorizontalAlignment.Left)

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
		ListView4.Columns.Add("Prefix", 99, HorizontalAlignment.Left)

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
		ListView5.Columns.Add("Prefix", 99, HorizontalAlignment.Left)
		ListView5.Columns.Add("Satuan", 0, HorizontalAlignment.Left)
		ListView5.Columns.Add("Stock Min", 0, HorizontalAlignment.Left)
		ListView5.Columns.Add("Berat Bersih", 0, HorizontalAlignment.Left)
		ListView5.Columns.Add("Berat Kotor", 0, HorizontalAlignment.Left)
		ListView5.Columns.Add("Ukuran", 0, HorizontalAlignment.Left)
		ListView5.Columns.Add("Metode Pot Stock", 0, HorizontalAlignment.Left)

		kosong()
		kosong2()
		kosong3()
		kosong4()

		If Asal_proses = "" Then

			CmbSK3_Jenis.Enabled = True
			CmbSK3_JenisSub.Enabled = True
			CmbSK3_JenisSub1.Enabled = True
			CmbSK3_JenisSub2.Enabled = True

			TabControl1.SelectedIndex = 2

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

			TabControl1.SelectedIndex = 2

			kosong5()

		ElseIf Asal_proses = "pengajuan_barang_baru" Then
			CmbSK3_Jenis.Enabled = True
			CmbSK3_JenisSub.Enabled = True
			CmbSK3_JenisSub1.Enabled = True
			CmbSK3_JenisSub2.Enabled = True

			TabControl1.SelectedIndex = 2

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

			CurrentPage_1 = 1

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

			'ListView1.Items.Clear()
			'SQL = "select Kode_Kategori_Jenis, Keterangan, Prefix, Id_Kategori_Jenis, Flag_Aktif from N_EMI_Master_Kategori_Jenis "
			'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        Dim Lvw As ListViewItem
			'        Lvw = ListView1.Items.Add(dr("Id_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Keterangan"))
			'        Lvw.SubItems.Add(dr("Prefix"))
			'        Lvw.SubItems.Add(dr("Flag_Aktif"))
			'    Loop
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Load_Data_Tab_1()

	End Sub

	Private Sub kosong2()
		Try
			OpenConn()

			isCheck_2 = False
			CurrentPage_2 = 1

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

			'ListView2.Items.Clear()
			'SQL = "select b.Kode_Kategori_Jenis, b.Keterangan as Kategori_Jenis, a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, "
			'SQL = SQL & "a.Prefix, a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
			'SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' order by a.Keterangan "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        Dim Lvw As ListViewItem
			'        Lvw = ListView2.Items.Add(dr("Id_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Prefix"))
			'    Loop
			'End Using

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

		Load_Data_Tab_2()

	End Sub

	Private Sub kosong3()
		Try
			OpenConn()

			isCheck_3 = False
			CurrentPage_3 = 1

			TextBox8.Enabled = True
			TextBox8.Text = ""
			TextBox9.Text = ""
			TextBox10.Text = ""
			TextBox11.Text = ""
			xid_sub_kategori1 = ""
			xprefix3 = ""
			RD_Kategori_1_Tidak.Checked = True

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

			'ListView3.Items.Clear()
			'SQL = "select  c.Kode_Kategori_Jenis, c.Keterangan as Kategori_Jenis, b.Kode_Sub_Kategori_Jenis, b.Keterangan as Sub_Kategori_Jenis,"
			'SQL = SQL & "a.Kode_Sub_Kategori_Jenis_1, a.Keterangan as Sub_Kategori_Jenis1, a.Prefix, "
			'SQL = SQL & "a.Id_Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
			'SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_1 a, N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.Id_Sub_Kategori_Jenis "
			'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis order by a.Keterangan "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        Dim Lvw As ListViewItem
			'        Lvw = ListView3.Items.Add(dr("Id_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis1"))
			'        Lvw.SubItems.Add(dr("Prefix"))
			'    Loop
			'End Using

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

		Load_Data_Tab_3()
	End Sub

	Private Sub kosong4()
		Try
			OpenConn()

			isCheck_4 = False
			CurrentPage_4 = 1

			TextBox12.Enabled = True
			TextBox12.Text = ""
			TextBox13.Text = ""
			TextBox14.Text = ""
			TextBox15.Text = ""
			xid_sub_kategori2 = ""
			xprefix4 = ""

			btnUpload.Enabled = True

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

			'ListView4.Items.Clear()
			'SQL = "select d.Kode_Kategori_Jenis, d.Keterangan as Kategori_Jenis, c.Kode_Sub_Kategori_Jenis, c.Keterangan as Sub_Kategori_Jenis, "
			'SQL = SQL & "b.Kode_Sub_Kategori_Jenis_1, b.Keterangan as Sub_Kategori_Jenis_1, a.Kode_Sub_Kategori_Jenis_2, a.Keterangan as Sub_Kategori_Jenis_2, a.Prefix, "
			'SQL = SQL & "a.Id_Sub_Kategori_Jenis_2, a.Id_Sub_Kategori_Jenis_1, b.Id_Sub_Kategori_Jenis, c.Id_Kategori_Jenis "
			'SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_2 a, N_EMI_Master_Sub_Kategori_Jenis_1 b, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis c, N_EMI_Master_Kategori_Jenis d "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_1 = b.Id_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis "
			'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Jenis = d.Id_Kategori_Jenis "
			'SQL = SQL & "order by a.Keterangan "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        Dim Lvw As ListViewItem
			'        Lvw = ListView4.Items.Add(dr("Id_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_1"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_2"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_2"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_2"))
			'        Lvw.SubItems.Add(dr("Prefix"))
			'    Loop
			'End Using

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

		Load_Data_Tab_4()
	End Sub

	Public Sub kosong5()
		Try
			OpenConn()

			isCheck_5 = False
			CurrentPage_5 = 1

			btnUpload.Enabled = True
			labelnote.Visible = False

			TextBox16.Enabled = True
			TextBox16.Text = ""
			TextBox13.Text = ""
			TextBox17.Text = ""
			TextBox18.Text = ""
			TextBox19.Text = ""
			xid_sub_kategori3 = ""
			xprefix5 = ""

			namaFileGambar = ""
			PictureBox1.Image = Nothing
			SelectedFilePath = ""
			gambarBerubah = False

			TextBox20.Text = ""
			TextBox21.Text = ""
			TextBox22.Text = ""
			TextBox23.Text = ""
			TextBox24.Text = ""
			TextBox25.Text = ""

			cmbSatuan.Items.Clear()
			SQL = "select satuan from emi_satuan where kode_perusahaan = '" & KodePerusahaan & "' And Flag_Barang='Y' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					cmbSatuan.Items.Add(Dr("satuan"))
				Loop
			End Using
			cmbSatuan.SelectedIndex = -1

			ComboBox11.Items.Clear()
			ComboBox11.Items.Add("FIFO")
			ComboBox11.Items.Add("FEFO")
			ComboBox11.SelectedIndex = -1

			ComboBox15.Items.Clear() : arrcari5.Clear()
			ComboBox15.Items.Add("Kode kategori") : arrcari5.Add("e.Kode_Kategori_Jenis")
			ComboBox15.Items.Add("Keterangan Kategori") : arrcari5.Add("e.Keterangan")
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

			'ListView5.Items.Clear()
			'SQL = "select e.Kode_Kategori_Jenis, e.Keterangan as Kategori_Jenis, d.Kode_Sub_Kategori_Jenis, d.Keterangan as Sub_Kategori_Jenis, "
			'SQL = SQL & "c.Kode_Sub_Kategori_Jenis_1, c.Keterangan as Sub_Kategori_Jenis_1, b.Kode_Sub_Kategori_Jenis_2, b.Keterangan as Sub_Kategori_Jenis_2, "
			'SQL = SQL & "a.Kode_Sub_Kategori_Jenis_3, a.Keterangan as Sub_Kategori_Jenis_3, a.Prefix, "
			'SQL = SQL & "a.Id_Sub_Kategori_Jenis_3, a.Id_Sub_Kategori_Jenis_2, b.Id_Sub_Kategori_Jenis_1, c.Id_Sub_Kategori_Jenis, d.Id_Kategori_Jenis, "
			'SQL = SQL & "a.Satuan, a.Stock_Minimum, a.Berat, a.Berat_Kotor, a.Panjang, a.Lebar, a.Tinggi, a.Metode_Pengeluaran_Stok "
			'SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a, N_EMI_Master_Sub_Kategori_Jenis_2 b, N_EMI_Master_Sub_Kategori_Jenis_1 c, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis d, N_EMI_Master_Kategori_Jenis e "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_2 = b.Id_Sub_Kategori_Jenis_2 "
			'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis_1 = c.Id_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis "
			'SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis "
			'SQL = SQL & "order by a.Keterangan "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        Dim Lvw As ListViewItem
			'        Lvw = ListView5.Items.Add(dr("Id_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kode_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_1"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_1"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_1"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_2"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_2"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_2"))
			'        Lvw.SubItems.Add(dr("Id_Sub_Kategori_Jenis_3"))
			'        Lvw.SubItems.Add(dr("Kode_Sub_Kategori_Jenis_3"))
			'        Lvw.SubItems.Add(dr("Sub_Kategori_Jenis_3"))
			'        Lvw.SubItems.Add(dr("Prefix"))
			'        If General_Class.CekNULL(dr("Satuan")) = "" Then
			'            Lvw.SubItems.Add("-")
			'        Else
			'            Lvw.SubItems.Add(dr("Satuan"))
			'        End If

			'        If General_Class.CekNULL(dr("Stock_Minimum")) = "" Then
			'            Lvw.SubItems.Add("-")
			'        Else
			'            Lvw.SubItems.Add(dr("Stock_Minimum"))
			'        End If

			'        If General_Class.CekNULL(dr("Berat")) = "" Then
			'            Lvw.SubItems.Add("-")
			'        Else
			'            Lvw.SubItems.Add(dr("Berat"))
			'        End If

			'        If General_Class.CekNULL(dr("Berat_Kotor")) = "" Then
			'            Lvw.SubItems.Add("-")
			'        Else
			'            Lvw.SubItems.Add(dr("Berat_Kotor"))
			'        End If

			'        If General_Class.CekNULL(dr("Panjang")) = "" Then
			'            Lvw.SubItems.Add("-")
			'        Else
			'            Lvw.SubItems.Add(dr("Panjang") & " X " & dr("Lebar") & " X " & dr("Tinggi"))
			'        End If

			'        If General_Class.CekNULL(dr("Metode_Pengeluaran_Stok")) = "" Then
			'            Lvw.SubItems.Add("-")
			'        Else
			'            Lvw.SubItems.Add(dr("Metode_Pengeluaran_Stok"))
			'        End If

			'    Loop
			'End Using

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

		Load_Data_Tab_5()
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

		get_jam()

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

				SQL = "insert into N_EMI_Master_Kategori_Jenis(Kode_Perusahaan, Kode_Kategori_Jenis, Keterangan, Prefix, "
				SQL = SQL & "Flag_Aktif, UserId, Tanggal, Jam) values("
				SQL = SQL & "'" & KodePerusahaan & "', '" & TextBox1.Text.Trim.ToUpper & "', '" & TextBox2.Text.Trim.ToUpper & "', "
				SQL = SQL & "'" & TextBox3.Text.Trim & "', '" & ComboBox10.Text & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

				Dim x_ident_current As Integer = 0
				SQL = "select IDENT_CURRENT('N_EMI_Master_Kategori_Jenis') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_ident_current = Dr("urutan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Kategori_Jenis', '" & x_ident_current & "', "
				SQL = SQL & "'INSERT', '" & TextBox2.Text.Trim.ToUpper & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
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

				Dim x_ket As String = ""
				SQL = "select Keterangan from N_EMI_Master_Kategori_Jenis "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Kategori_Jenis = '" & xid_kategori & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Kategori_Jenis', '" & xid_kategori & "', "
				SQL = SQL & "'UPDATE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

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

		get_jam()

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

				Dim x_ket As String = ""
				SQL = "select Keterangan from N_EMI_Master_Kategori_Jenis "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Kategori_Jenis = '" & xid_kategori & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Kategori_Jenis', '" & xid_kategori & "', "
				SQL = SQL & "'DELETE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

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
		If ComboBox2.SelectedIndex = -1 Then
			MessageBox.Show("ComboBox Filter Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox2.DroppedDown = True
			ComboBox2.Focus()
			Exit Sub
		ElseIf TextBox5.Text.Trim.Length = 0 Then
			MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox5.Focus()
			Exit Sub
		End If

		isCheck_2 = False
		Load_Data_Tab_2()

		'If ComboBox2.Text.Trim.Length = 0 Then Exit Sub
		'If TextBox5.Text.Trim.Length = 0 Then Exit Sub

		'Cari2("T")
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

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction() = Cn.BeginTransaction

			If BtnSimpan2.Tag = "&Simpan" Then
				'SQL = "select Kode_Sub_Kategori_Jenis, Keterangan from N_EMI_Master_Sub_Kategori_Jenis "
				'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and  (upper(Kode_Sub_Kategori_Jenis) = '" & TextBox7.Text.Trim.ToUpper & "' "
				''    SQL = SQL & "and upper(Keterangan) = '" & TextBox6.Text.Trim.ToUpper & "' "
				'SQL = SQL & "and id_kategori_jenis  = '" & arrid.Item(ComboBox1.SelectedIndex) & "' or "
				'SQL = SQL & " id_kategori_jenis  = '" & arrid.Item(ComboBox1.SelectedIndex) & "' and prefix = '" & TextBox4.Text.Trim & "' ) "

				SQL = $"
                    SELECT Kode_Sub_Kategori_Jenis, Keterangan
                    FROM N_EMI_Master_Sub_Kategori_Jenis
                    WHERE Kode_Perusahaan = '{KodePerusahaan}'
                    AND id_kategori_jenis = '{arrid.Item(ComboBox1.SelectedIndex)}'
                    AND (
	                    Kode_Sub_Kategori_Jenis = '{TextBox7.Text.Trim}' OR prefix = '{TextBox4.Text.Trim}'
                    )
                "
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

				SQL = "insert into N_EMI_Master_Sub_Kategori_Jenis(Kode_Perusahaan, Id_Kategori_Jenis, Kode_Sub_Kategori_Jenis, Keterangan, Prefix, UserID, Tanggal, Jam ) values("
				SQL = SQL & "'" & KodePerusahaan & "', '" & arrid.Item(ComboBox1.SelectedIndex) & "', '" & TextBox7.Text.Trim.ToUpper & "', '" & TextBox6.Text.Trim.ToUpper & "', "
				SQL = SQL & "'" & TextBox4.Text.Trim & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

				'awal stenly 15-01-2026
				Dim x_ident_current As Integer = 0
				SQL = "select IDENT_CURRENT('N_EMI_Master_Sub_Kategori_Jenis') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_ident_current = Dr("urutan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis', '" & x_ident_current & "', "
				SQL = SQL & "'INSERT', '" & TextBox6.Text.Trim.ToUpper & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)
				'akhir stenly 15-01-2026
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

				Dim x_ket As String = ""
				SQL = "select Keterangan from N_EMI_Master_Sub_Kategori_Jenis "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis = '" & xid_sub_kategori & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis', '" & xid_sub_kategori & "', "
				SQL = SQL & "'UPDATE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

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

		get_jam()

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

				Dim x_ket As String = ""
				SQL = "select Keterangan from N_EMI_Master_Sub_Kategori_Jenis "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis = '" & xid_sub_kategori & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis', '" & xid_sub_kategori & "', "
				SQL = SQL & "'DELETE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

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
		If ComboBox5.SelectedIndex = -1 Then
			MessageBox.Show("ComboBox Filter Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox2.DroppedDown = True
			ComboBox2.Focus()
			Exit Sub
		ElseIf TextBox11.Text.Trim.Length = 0 Then
			MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox5.Focus()
			Exit Sub
		End If

		isCheck_3 = False
		Load_Data_Tab_3()

		'If ComboBox5.Text.Trim.Length = 0 Then Exit Sub
		'If TextBox11.Text.Trim.Length = 0 Then Exit Sub

		'Cari3("T")
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
		If ComboBox9.SelectedIndex = -1 Then
			MessageBox.Show("ComboBox Filter Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox9.DroppedDown = True
			ComboBox9.Focus()
			Exit Sub
		ElseIf TextBox15.Text.Trim.Length = 0 Then
			MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox15.Focus()
			Exit Sub
		End If

		isCheck_4 = False
		Load_Data_Tab_4()

		'If ComboBox9.Text.Trim.Length = 0 Then Exit Sub
		'If TextBox15.Text.Trim.Length = 0 Then Exit Sub

		'Cari4("T")
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
		ElseIf RD_Kategori_1_Ya.Checked = False And RD_Kategori_1_Tidak.Checked = False Then
			MessageBox.Show("Pilih Dahulu Budget?", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			RD_Kategori_1_Ya.Focus() : Exit Sub
		End If

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction() = Cn.BeginTransaction

			Dim IsBudget As String = If(RD_Kategori_1_Ya.Checked, "'Y'", "NULL")

			If BtnSimpan3.Tag = "&Simpan" Then
				SQL = "select Kode_Sub_Kategori_Jenis_1, Keterangan from N_EMI_Master_Sub_Kategori_Jenis_1 "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "'  "
				SQL = SQL & "and ( upper(Kode_Sub_Kategori_Jenis_1) = '" & TextBox8.Text.Trim.ToUpper & "' "
				'  SQL = SQL & "and upper(Keterangan) = '" & TextBox9.Text.Trim.ToUpper & "' "
				SQL = SQL & "AND upper(id_sub_kategori_jenis) = '" & arridsub2.Item(ComboBox4.SelectedIndex) & "' or "
				SQL = SQL & "upper(id_sub_kategori_jenis) = '" & arridsub2.Item(ComboBox4.SelectedIndex) & "' and "
				SQL = SQL & "prefix = '" & TextBox10.Text.Trim & "' ) "

				'SQL = $"
				'    SELECT Kode_Sub_Kategori_Jenis_1, Keterangan
				'    FROM N_EMI_Master_Sub_Kategori_Jenis_1
				'    WHERE Kode_Perusahaan = '{KodePerusahaan}'
				'    AND id_sub_kategori_jenis = '{arridsub2.Item(ComboBox4.SelectedIndex)}'
				'    AND (
				'     Kode_Sub_Kategori_Jenis_1 = '{TextBox8.Text.Trim}'
				'     OR prefix = '{TextBox10.Text.Trim}'
				'    )
				'"
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

				SQL = "insert into N_EMI_Master_Sub_Kategori_Jenis_1 "
				SQL = SQL & "(Kode_Perusahaan,Id_Sub_Kategori_Jenis,Kode_Sub_Kategori_Jenis_1,Keterangan,Prefix, UserID, Tanggal, Jam, Flag_Is_Budget ) values("
				SQL = SQL & "'" & KodePerusahaan & "', '" & arridsub2.Item(ComboBox4.SelectedIndex) & "', "
				SQL = SQL & "'" & TextBox8.Text.Trim.ToUpper & "', '" & TextBox9.Text.Trim.ToUpper & "', '" & TextBox10.Text.Trim & "', "
				SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', " & IsBudget & ") "
				ExecuteTrans(SQL)

				Dim x_ident_current As Integer = 0
				SQL = "select IDENT_CURRENT('N_EMI_Master_Sub_Kategori_Jenis_1') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_ident_current = Dr("urutan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis_1', '" & x_ident_current & "', "
				SQL = SQL & "'INSERT', '" & TextBox9.Text.Trim.ToUpper & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
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

				Dim x_ket As String = ""
				SQL = "select Keterangan from N_EMI_Master_Sub_Kategori_Jenis_1 "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis_1 = '" & xid_sub_kategori1 & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis_1', '" & xid_sub_kategori1 & "', "
				SQL = SQL & "'UPDATE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

				SQL = "update N_EMI_Master_Sub_Kategori_Jenis_1 set "
				SQL = SQL & "Keterangan = '" & TextBox9.Text.ToUpper & "', "
				SQL = SQL & "Flag_Is_Budget = " & IsBudget & " "
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

		If ComboBox15.SelectedIndex = -1 Then
			MessageBox.Show("ComboBox Filter Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox15.DroppedDown = True
			ComboBox15.Focus()
			Exit Sub
		ElseIf TextBox19.Text.Trim.Length = 0 Then
			MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox19.Focus()
			Exit Sub
		End If

		isCheck_5 = False
		Load_Data_Tab_5()

		'If ComboBox15.Text.Trim.Length = 0 Then Exit Sub
		'If TextBox19.Text.Trim.Length = 0 Then Exit Sub

		'Cari5("T")
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

		get_jam()

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

				Dim x_ket As String = ""
				SQL = "select Keterangan from N_EMI_Master_Sub_Kategori_Jenis_1 "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis_1 = '" & xid_sub_kategori1 & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis_1', '" & xid_sub_kategori1 & "', "
				SQL = SQL & "'DELETE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

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

		If SelectedFilePath.Trim.Length = 0 Or SelectedFilePath.Trim = "NULL" Then
			MessageBox.Show("Gambar tidak boleh kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

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
		ElseIf ComboBox11.Text.Trim.Length = 0 Then
			MessageBox.Show("Metode Potong Stock Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox11.Focus() : Exit Sub
		ElseIf cmbSatuan.Text.Trim.Length = 0 Then
			MessageBox.Show("Satuan Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			cmbSatuan.Focus() : Exit Sub
		ElseIf TextBox20.Text.Trim.Length = 0 Then
			MessageBox.Show("Stock Minimum Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox20.Focus() : Exit Sub
		ElseIf TextBox21.Text.Trim.Length = 0 Then
			MessageBox.Show("Berat Bersih Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox21.Focus() : Exit Sub
		ElseIf TextBox22.Text.Trim.Length = 0 Then
			MessageBox.Show("Berat Kotor Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox22.Focus() : Exit Sub
		ElseIf TextBox23.Text.Trim.Length = 0 Then
			MessageBox.Show("Ukuran Panjang Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox23.Focus() : Exit Sub
		ElseIf TextBox24.Text.Trim.Length = 0 Then
			MessageBox.Show("Ukuran Lebar Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox24.Focus() : Exit Sub
		ElseIf TextBox25.Text.Trim.Length = 0 Then
			MessageBox.Show("Ukuran Tinggi Belum diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox25.Focus() : Exit Sub
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
				SQL = SQL & "(Kode_Perusahaan, Id_Sub_Kategori_Jenis_2, Kode_Sub_Kategori_Jenis_3, Keterangan, Prefix, "
				SQL = SQL & "Satuan, Stock_Minimum, Berat, Berat_Kotor, Panjang, Lebar, Tinggi, Metode_Pengeluaran_Stok, UserID, Tanggal, Jam ) "
				SQL = SQL & "values('" & KodePerusahaan & "', '" & arrid3sub4.Item(CmbSK3_JenisSub2.SelectedIndex) & "', "
				SQL = SQL & "'" & TextBox16.Text.Trim.ToUpper & "', '" & TextBox17.Text.Trim.ToUpper & "', '" & TextBox18.Text.Trim & "', "
				SQL = SQL & "'" & cmbSatuan.Text.Trim & "', '" & TextBox20.Text.Trim & "', '" & TextBox21.Text.Trim & "', "
				SQL = SQL & "'" & TextBox22.Text.Trim & "', '" & TextBox23.Text.Trim & "', '" & TextBox24.Text.Trim & "', "
				SQL = SQL & "'" & TextBox25.Text.Trim & "', '" & ComboBox11.Text.Trim & "','" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

				SQL = "select IDENT_CURRENT('N_EMI_Master_Sub_Kategori_Jenis_3') as urut"
				Using Dr1 = OpenTrans(SQL)
					If Dr1.Read Then
						id = Dr1("urut")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam, "
				SQL = SQL & "Satuan, Stock_Minimum, Berat, Berat_Kotor, Panjang, Lebar, Tinggi, Metode_Pengeluaran_Stok) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis_3', '" & id & "', "
				SQL = SQL & "'INSERT', '" & TextBox17.Text.Trim.ToUpper & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "'" & cmbSatuan.Text.Trim & "', '" & TextBox20.Text.Trim & "', '" & TextBox21.Text.Trim & "', "
				SQL = SQL & "'" & TextBox22.Text.Trim & "', '" & TextBox23.Text.Trim & "', '" & TextBox24.Text.Trim & "', "
				SQL = SQL & "'" & TextBox25.Text.Trim & "', '" & ComboBox11.Text.Trim & "') "
				ExecuteTrans(SQL)

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

					Dim noFakturPengajuan As String = ""
					noFakturPengajuan = fPengajuanBrgBru & Format(tgl_skg, "MMyy") & "-" &
								 General_Class.Get_Last_Number2("N_EMI_Pengajuan_Barang_Baru_Lain", "no_Faktur", 5,
								 "Kode_perusahaan", KodePerusahaan,
								 "And", "substring(no_Faktur, 1, " & Len(fPengajuanBrgBru) + 4 & ")", fPengajuanBrgBru & Format(tgl_skg, "MMyy"))

					SQL = "insert into N_EMI_Pengajuan_Barang_Baru_Lain(Kode_Perusahaan,No_Faktur,Tanggal,jam,Userid,Id_Sub_Kategori_Jenis_3, Urut_Departement) values ( "
					SQL = SQL & "'" & KodePerusahaan & "', '" & noFakturPengajuan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "',"
					SQL = SQL & "'" & UserID & "', '" & id & "', '" & xurut_departement & "' "
					SQL = SQL & ")"
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
				Dim kode_Barang As String = ""

				SQL = "select  kode_sub_kategori_jenis ,kode_sub_kategori_jenis_1, kode_sub_kategori_jenis_2,kode_sub_kategori_jenis_3,  "
				SQL = SQL & "Id_Sub_Kategori_Jenis_3,"
				SQL = SQL & "Prefix_Kategori_Jenis + Prefix_Sub_Kategori_Jenis + Prefix_Sub_Kategori_Jenis_1 + Prefix_Sub_Kategori_Jenis_2 + Prefix_Sub_Kategori_Jenis_3 as Kode_Barang "
				SQL = SQL & "From view_kategori_turunan where  "
				SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and id_sub_kategori_jenis_3 = '" & id & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						kode_Barang = Dr("kode_barang")
						namaFileAzure = Dr("kode_sub_kategori_jenis") & "/" & Dr("kode_sub_kategori_jenis_1") & " " & Dr("kode_sub_kategori_jenis_2") & " " & Dr("kode_sub_kategori_jenis_3") & "_" & Dr("kode_barang") & "_" & Dr("Id_Sub_Kategori_Jenis_3") & Path.GetExtension(SelectedFilePath)
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Kategori turunan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "insert into N_EMI_Katalog_Barang_lain  (Kode_Perusahaan ,User_Id, "
				SQL = SQL & "Tanggal,Jam,Aktif,Blob_Storage,Container,Id_Sub_Kategori_Jenis_3,nama_file ,kode_barang) "
				SQL = SQL & "values('" & KodePerusahaan & "','" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "','Y', "
				SQL = SQL & "'" & namaFileAzure & "', '" & container_katalog_barang & "', '" & id & "', '" & Path.GetFileName(SelectedFilePath) & "', '" & kode_Barang & "')"
				ExecuteTrans(SQL)

				SQL = "insert into N_EMI_Katalog_Barang_Lain_Log  (Kode_Perusahaan ,User_Id, "
				SQL = SQL & "Tanggal,Jam,Aktif,Blob_Storage,Container,Id_Sub_Kategori_Jenis_3,nama_file, kode_barang, ket_log) "
				SQL = SQL & "values('" & KodePerusahaan & "','" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "','Y', "
				SQL = SQL & "'" & namaFileAzure & "', '" & container_katalog_barang & "', '" & id & "', '" & Path.GetFileName(SelectedFilePath) & "', '" & kode_Barang & "', 'INSERT')"
				ExecuteTrans(SQL)

				Using fs As New FileStream(SelectedFilePath, FileMode.Open, FileAccess.Read)
					Dim result = AzureHelper_EMI.UploadToAzure(
					container_katalog_barang, ' container khusus foto
					namaFileAzure,
					fs
				)

					If Not result.Success Then
						MessageBox.Show(
							result.Message,
							Judul,
							MessageBoxButtons.OK,
							MessageBoxIcon.Error
					)
						Exit Sub
					End If

					url = result.Url
				End Using
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

				Dim x_ket As String = ""
				Dim x_satuan As String = ""
				Dim x_stock As Integer = 0
				Dim x_bb As Double = 0
				Dim x_bk As Double = 0
				Dim x_p As Double = 0
				Dim x_l As Double = 0
				Dim x_t As Double = 0
				Dim x_metode As String = ""
				SQL = "select ISNULL(Keterangan,'') as Keterangan, ISNULL(Satuan,'') as Satuan, ISNULL(Stock_Minimum,'') as Stock_Minimum, "
				SQL = SQL & "ISNULL(Berat,'') as Berat, ISNULL(Berat_Kotor,'') as Berat_Kotor, ISNULL(Panjang,'') as Panjang, "
				SQL = SQL & "ISNULL(Lebar,'') as Lebar, ISNULL(Tinggi,'') as Tinggi, ISNULL(Metode_Pengeluaran_Stok,'') as Metode_Pengeluaran_Stok "
				SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis_3 = '" & xid_sub_kategori3 & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
						x_satuan = dr("Satuan")
						x_stock = dr("Stock_Minimum")
						x_bb = dr("Berat")
						x_bk = dr("Berat_Kotor")
						x_p = dr("Panjang")
						x_l = dr("Lebar")
						x_t = dr("Tinggi")
						x_metode = dr("Metode_Pengeluaran_Stok")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam, "
				SQL = SQL & "Satuan, Stock_Minimum, Berat, Berat_Kotor, Panjang, Lebar, Tinggi, Metode_Pengeluaran_Stok) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis_3', '" & xid_sub_kategori3 & "', "
				SQL = SQL & "'UPDATE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "'" & x_satuan & "', '" & x_stock & "', '" & x_bb & "', "
				SQL = SQL & "'" & x_bk & "', '" & x_p & "', '" & x_l & "', "
				SQL = SQL & "'" & x_t & "', '" & x_metode & "') "
				ExecuteTrans(SQL)

				SQL = "update N_EMI_Master_Sub_Kategori_Jenis_3 set "
				SQL = SQL & "Keterangan = '" & TextBox17.Text.ToUpper & "', "
				SQL = SQL & "Satuan = '" & cmbSatuan.Text & "', "
				SQL = SQL & "Stock_Minimum = '" & TextBox20.Text & "', "
				SQL = SQL & "Berat = '" & TextBox21.Text & "', "
				SQL = SQL & "Berat_Kotor = '" & TextBox22.Text & "', "
				SQL = SQL & "Panjang = '" & TextBox23.Text & "', "
				SQL = SQL & "Lebar = '" & TextBox24.Text & "', "
				SQL = SQL & "Tinggi = '" & TextBox25.Text & "', "
				SQL = SQL & "Metode_Pengeluaran_Stok = '" & ComboBox11.Text & "' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis_3 = '" & xid_sub_kategori3 & "' "
				ExecuteTrans(SQL)
				Dim blob_storag1 As String = "-"
				Dim namaFilediAzureUpdate As String = "-"
				'balek lagi
				If gambarBerubah Then
					SQL = "select kode_barang,nama_file,container,blob_storage from N_EMI_Katalog_Barang_lain where  "
					SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "id_sub_kategori_jenis_3 = '" & xid_sub_kategori3 & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then

							Dim kodeBarang As String = "-"
							Dim nama_file1 As String = "-"
							Dim container1 As String = "-"

							If General_Class.CekNULL(Dr("kode_barang")) = "" Then
								kodeBarang = "-"
							Else
								kodeBarang = Dr("kode_barang")
							End If

							If General_Class.CekNULL(Dr("nama_file")) = "" Then
								nama_file1 = "-"
							Else
								nama_file1 = Dr("nama_file")
							End If

							If General_Class.CekNULL(Dr("container")) = "" Then
								container1 = "-"
							Else
								container1 = Dr("container")
							End If

							If General_Class.CekNULL(Dr("blob_storage")) = "" Then
								blob_storag1 = "-"
								namaFilediAzureUpdate = "-"
							Else
								blob_storag1 = Path.GetFileNameWithoutExtension(Dr("blob_storage")) & Path.GetExtension(SelectedFilePath)
								namaFilediAzureUpdate = Dr("blob_storage")
							End If

							Dr.Close()

							SQL = "insert into N_EMI_Katalog_Barang_lain_log  (Kode_Perusahaan ,User_Id, "
							SQL = SQL & "Tanggal,Jam,Aktif,Blob_Storage,Container,Id_Sub_Kategori_Jenis_3,nama_file, ket_log, kode_barang) "
							SQL = SQL & "values('" & KodePerusahaan & "','" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "','Y', "
							SQL = SQL & "'" & blob_storag1 & "', '" & container1 & "', '" & xid_sub_kategori3 & "', '" & Path.GetFileName(SelectedFilePath) & "' , 'UPDATE' , '" & kodeBarang & "')"
							ExecuteTrans(SQL)

						End If
					End Using

					If blob_storag1 = "-" Then
						Dim kode_barang As String = ""
						SQL = "select  kode_sub_kategori_jenis ,kode_sub_kategori_jenis_1, kode_sub_kategori_jenis_2,kode_sub_kategori_jenis_3,  "
						SQL = SQL & "Id_Sub_Kategori_Jenis_3,"
						SQL = SQL & "Prefix_Kategori_Jenis + Prefix_Sub_Kategori_Jenis + Prefix_Sub_Kategori_Jenis_1 + Prefix_Sub_Kategori_Jenis_2 + Prefix_Sub_Kategori_Jenis_3 as Kode_Barang "
						SQL = SQL & "From view_kategori_turunan where  "
						SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "and id_sub_kategori_jenis_3 = '" & xid_sub_kategori3 & "' "
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								kode_barang = Dr("kode_barang")
								blob_storag1 = Dr("kode_sub_kategori_jenis") & "/" & Dr("kode_sub_kategori_jenis_1") & " " & Dr("kode_sub_kategori_jenis_2") & " " & Dr("kode_sub_kategori_jenis_3") & "_" & Dr("kode_barang") & "_" & Dr("Id_Sub_Kategori_Jenis_3") & Path.GetExtension(SelectedFilePath)
							Else
								CloseTrans()
								CloseConn()
								MessageBox.Show("Kategori turunan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End Using

						SQL = "insert into N_EMI_Katalog_Barang_lain  (Kode_Perusahaan ,User_Id, "
						SQL = SQL & "Tanggal,Jam,Aktif,Blob_Storage,Container,Id_Sub_Kategori_Jenis_3,nama_file ,kode_barang) "
						SQL = SQL & "values('" & KodePerusahaan & "','" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "','Y', "
						SQL = SQL & "'" & blob_storag1 & "', '" & container_katalog_barang & "', '" & xid_sub_kategori3 & "', '" & Path.GetFileName(SelectedFilePath) & "', '" & kode_barang & "')"
						ExecuteTrans(SQL)
					Else

						SQL = "update N_EMI_Katalog_Barang_lain set  "
						SQL = SQL & "blob_storage = '" & blob_storag1 & "', "
						'SQL = SQL & "container = '" & container_katalog_barang & "', "
						SQL = SQL & "nama_file = '" & Path.GetFileName(SelectedFilePath) & "' ,"
						SQL = SQL & "tanggal = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
						SQL = SQL & "jam = '" & Format(tgl_skg, "HH:mm:ss") & "' ,"
						SQL = SQL & "user_id = '" & UserID & "' "
						SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "and id_sub_kategori_jenis_3 = '" & xid_sub_kategori3 & "'"

					End If

					Dim result = AzureHelper_EMI.DeleteFromAzure(
						container_katalog_barang,
						namaFilediAzureUpdate
					)

					If result.Success = False Then
						MessageBox.Show(
					"Gambar gagal dihapus." & vbCrLf & result.Message,
					"Warning",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				)
					End If

					'   If result.Success = True Then
					Dim result2 As (Success As Boolean, Message As String, Url As String)

					Using fs As New FileStream(SelectedFilePath, FileMode.Open, FileAccess.Read)
						result2 = AzureHelper_EMI.UploadToAzure(
							container_katalog_barang,
							blob_storag1,
							fs
						)
					End Using

					If Not result2.Success Then
						MessageBox.Show(
						"Gambar gagal di upload file. " & vbCrLf & result.Message,
						Judul,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					)

					End If
					'   End If

				End If
			End If

			Cmd.Transaction.Commit()
			CloseConn()

			MessageBox.Show("Data berhasil di update", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Catch ex As Exception

			CloseTrans()
			CloseConn()
			' 🔥 ROLLBACK FILE AZURE
			If url <> "" And namaFileAzure <> "" Then
				Dim result = AzureHelper_EMI.DeleteFromAzure(
					container_katalog_barang,
					namaFileAzure
				)

				If result.Success = False Then
					MessageBox.Show(
					"Gambar gagal dihapus." & vbCrLf & result.Message,
					"Warning",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
	)
				End If
			End If
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

		get_jam()

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

				Dim x_ket As String = ""
				Dim x_satuan As String = ""
				Dim x_stock As Integer = 0
				Dim x_bb As Double = 0
				Dim x_bk As Double = 0
				Dim x_p As Double = 0
				Dim x_l As Double = 0
				Dim x_t As Double = 0
				Dim x_metode As String = ""
				SQL = "select Keterangan, Satuan, Stock_Minimum, Berat, Berat_Kotor, Panjang, Lebar, Tinggi, Metode_Pengeluaran_Stok "
				SQL = SQL & " from N_EMI_Master_Sub_Kategori_Jenis_3 "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis_3 = '" & xid_sub_kategori3 & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
						x_satuan = dr("Satuan")
						x_stock = dr("Stock_Minimum")
						x_bb = dr("Berat")
						x_bk = dr("Berat_Kotor")
						x_p = dr("Panjang")
						x_l = dr("Lebar")
						x_t = dr("Tinggi")
						x_metode = dr("Metode_Pengeluaran_Stok")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam, "
				SQL = SQL & "Satuan, Stock_Minimum, Berat, Berat_Kotor, Panjang, Lebar, Tinggi, Metode_Pengeluaran_Stok) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis_3', '" & xid_sub_kategori3 & "', "
				SQL = SQL & "'DELETE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "'" & x_satuan & "', '" & x_stock & "', '" & x_bb & "', "
				SQL = SQL & "'" & x_bk & "', '" & x_p & "', '" & x_l & "', "
				SQL = SQL & "'" & x_t & "', '" & x_metode & "') "
				ExecuteTrans(SQL)

				'hapus dulu

				SQL = "DELETE FROM N_EMI_Master_Sub_Kategori_Jenis_3 "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis_3 = '" & xid_sub_kategori3 & "' "
				'SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori4.Item(ComboBox6.SelectedIndex) & "' "
				'SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & arrsubkateogori4.Item(ComboBox7.SelectedIndex) & "' "
				'SQL = SQL & "and Kode_Sub_Kategori_Jenis_1 = '" & arrsub1kateogori4.Item(ComboBox8.SelectedIndex) & "' "
				'SQL = SQL & "and Kode_Sub_Kategori_Jenis_2 = '" & TextBox12.Text & "' "
				ExecuteTrans(SQL)

				SQL = "select kode_barang,nama_file,container,blob_storage from N_EMI_Katalog_Barang_lain where  "
				SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "id_sub_kategori_jenis_3 = '" & xid_sub_kategori3 & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						Dim kodeBarang As String = "-"
						Dim nama_file1 As String = "-"
						Dim container1 As String = "-"
						Dim blob_storag1 As String = "-"

						If General_Class.CekNULL(Dr("kode_barang")) = "" Then
							kodeBarang = "-"
						Else
							kodeBarang = Dr("kode_barang")
						End If

						If General_Class.CekNULL(Dr("nama_file")) = "" Then
							nama_file1 = "-"
						Else
							nama_file1 = Dr("nama_file")
						End If

						If General_Class.CekNULL(Dr("container")) = "" Then
							container1 = "-"
						Else
							container1 = Dr("container")
						End If

						If General_Class.CekNULL(Dr("blob_storage")) = "" Then
							blob_storag1 = "-"
						Else
							blob_storag1 = Dr("blob_storage")
						End If

						Dr.Close()

						SQL = "insert into N_EMI_Katalog_Barang_lain_log  (Kode_Perusahaan ,User_Id, "
						SQL = SQL & "Tanggal,Jam,Aktif,Blob_Storage,Container,Id_Sub_Kategori_Jenis_3,nama_file, ket_log, kode_barang) "
						SQL = SQL & "values('" & KodePerusahaan & "','" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "','Y', "
						SQL = SQL & "'" & blob_storag1 & "', '" & container1 & "', '" & xid_sub_kategori3 & "', " & namaFIleAsli & " , 'DELETE' , '" & kodeBarang & "')"
						ExecuteTrans(SQL)

					End If
				End Using

				SQL = "delete from  n_emi_katalog_barang_lain where kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and id_sub_kategori_jenis_3 = '" & xid_sub_kategori3 & "' "
				ExecuteTrans(SQL)

				Dim result = AzureHelper_EMI.DeleteFromAzure(
					container_katalog_barang,
					SelectedFilePath
				)

				If result.Success = False Then
					CloseTrans()
					CloseConn()

					MessageBox.Show(
					"Gambar gagal dihapus." & vbCrLf & result.Message,
					"Warning",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
					)

					Exit Sub

				End If

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

		get_jam()

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
				SQL = SQL & "(Kode_Perusahaan,Id_Sub_Kategori_Jenis_1,Kode_Sub_Kategori_Jenis_2,Keterangan,Prefix, UserID, Tanggal, Jam ) "
				SQL = SQL & "values('" & KodePerusahaan & "', '" & arrid2sub3.Item(ComboBox8.SelectedIndex) & "', "
				SQL = SQL & "'" & TextBox12.Text.Trim.ToUpper & "', '" & TextBox13.Text.Trim.ToUpper & "', '" & TextBox14.Text.Trim & "', "
				SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

				Dim x_ident_current As Integer = 0
				SQL = "select IDENT_CURRENT('N_EMI_Master_Sub_Kategori_Jenis_2') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_ident_current = Dr("urutan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis_2', '" & x_ident_current & "', "
				SQL = SQL & "'INSERT', '" & TextBox13.Text.Trim.ToUpper & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
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

				Dim x_ket As String = ""
				SQL = "select Keterangan from N_EMI_Master_Sub_Kategori_Jenis_2 "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis_2 = '" & xid_sub_kategori2 & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis_2', '" & xid_sub_kategori2 & "', "
				SQL = SQL & "'UPDATE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

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

		get_jam()

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

				Dim x_ket As String = ""
				SQL = "select Keterangan from N_EMI_Master_Sub_Kategori_Jenis_2 "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Sub_Kategori_Jenis_2 = '" & xid_sub_kategori2 & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						x_ket = dr("Keterangan")
					End If
				End Using

				SQL = "insert into N_EMI_Master_Kategori_Jenis_Sub_Kategori_Jenis_Log("
				SQL = SQL & "Kode_Perusahaan, Tabel_Asal, Id, Jenis, Keterangan, UserID, Tanggal, Jam ) "
				SQL = SQL & "values( '" & KodePerusahaan & "', 'N_EMI_Master_Sub_Kategori_Jenis_2', '" & xid_sub_kategori2 & "', "
				SQL = SQL & "'DELETE', '" & x_ket & "', '" & UserID & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
				ExecuteTrans(SQL)

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

			'SQL = "select  a.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
			'SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c, N_EMI_Master_Role_Sub_Kategori g "
			'SQL = SQL & "where b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
			'SQL = SQL & "and b.Kode_Perusahaan = g.Kode_Perusahaan and b.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
			'SQL = SQL & "and b.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "
			'SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.Id_Sub_Kategori_Jenis_1 = '" & xid_sub_kategori1 & "' "

			SQL = "select  c.Kode_Kategori_Jenis, c.Keterangan as Kategori_Jenis, b.Kode_Sub_Kategori_Jenis, b.Keterangan as Sub_Kategori_Jenis,"
			SQL = SQL & "a.Kode_Sub_Kategori_Jenis_1, a.Keterangan as Sub_Kategori_Jenis1, a.Prefix, "
			SQL = SQL & "a.Id_Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis, Flag_Is_Budget "
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

					If General_Class.CekNULL(dr("Flag_Is_Budget")).Trim = "Y" Then
						RD_Kategori_1_Ya.Checked = True
					Else
						RD_Kategori_1_Tidak.Checked = True
					End If

					BtnSimpan3.Text = "&Update"
					BtnHapus3.Enabled = True
					BtnSimpan3.Tag = "&Update"
				Else
					TextBox8.Enabled = True
					ComboBox3.Enabled = True
					ComboBox4.Enabled = True
					RD_Kategori_1_Tidak.Checked = True
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

			'SQL = "select  b.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
			'SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c, N_EMI_Master_Role_Sub_Kategori g "
			'SQL = SQL & "where b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
			'SQL = SQL & "and b.Kode_Perusahaan = g.Kode_Perusahaan and b.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
			'SQL = SQL & "and b.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "
			'SQL = SQL & "and b.Kode_Perusahaan =  '" & KodePerusahaan & "' and b.Id_Kategori_Jenis = '" & arrid3.Item(ComboBox6.SelectedIndex) & "' "
			'SQL = SQL & "and b.Id_Sub_Kategori_Jenis = '" & arridsub3.Item(ComboBox7.SelectedIndex) & "' "

			If Asal_proses IsNot Nothing Then
				If Asal_proses.ToUpper <> "MASTER" AndAlso Not (Asal_proses = "" OrElse Asal_proses = "N_EMI_SD_Tambah_PR_Barang_Lain_Departement") Then
					SQL = "select a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
					SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a "
					SQL = SQL & "inner JOIN N_EMI_Master_Kategori_Jenis b on a.kode_perusahaan = b.Kode_Perusahaan AND a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
					SQL = SQL & "inner JOIN N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain c ON a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Jenis = c.id_kategori_jenis and a.Id_Sub_Kategori_Jenis = c.id_sub_kategori_jenis "
					SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "AND c.user_id = '" & UserID & "' "
					SQL = SQL & "and a.Id_Kategori_Jenis = '" & arrid3.Item(ComboBox6.SelectedIndex) & "' "
					SQL = SQL & "and a.Id_Sub_Kategori_Jenis = '" & arridsub3.Item(ComboBox7.SelectedIndex) & "' "
					Using dr = OpenTrans(SQL)
						If Not dr.Read Then
							dr.Close()
							CloseConn()
							MessageBox.Show("anda tidak Memiliki akses ke kategori dan sub kategori ini ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							ComboBox7.SelectedIndex = -1 : Exit Sub
						End If
					End Using
				End If
			End If

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

			'SQL = "select  b.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
			'SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c, N_EMI_Master_Role_Sub_Kategori g "
			'SQL = SQL & "where b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
			'SQL = SQL & "and b.Kode_Perusahaan = g.Kode_Perusahaan and b.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
			'SQL = SQL & "and b.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "
			'SQL = SQL & "and b.Kode_Perusahaan =  '" & KodePerusahaan & "' and b.Id_Kategori_Jenis = '" & arrid2.Item(ComboBox3.SelectedIndex) & "' "
			'SQL = SQL & "and b.Id_Sub_Kategori_Jenis = '" & arridsub2.Item(ComboBox4.SelectedIndex) & "' "

			If Asal_proses IsNot Nothing Then
				If Asal_proses.ToUpper <> "MASTER" AndAlso Not (Asal_proses = "" OrElse Asal_proses = "N_EMI_SD_Tambah_PR_Barang_Lain_Departement") Then
					SQL = "select a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
					SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a "
					SQL = SQL & "inner JOIN N_EMI_Master_Kategori_Jenis b on a.kode_perusahaan = b.Kode_Perusahaan AND a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
					SQL = SQL & "inner JOIN N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain c ON a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Jenis = c.id_kategori_jenis and a.Id_Sub_Kategori_Jenis = c.id_sub_kategori_jenis "
					SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "AND c.user_id = '" & UserID & "' "
					SQL = SQL & "and a.Id_Kategori_Jenis = '" & arrid2.Item(ComboBox3.SelectedIndex) & "' "
					SQL = SQL & "and a.Id_Sub_Kategori_Jenis = '" & arridsub2.Item(ComboBox4.SelectedIndex) & "' "
					Using dr = OpenTrans(SQL)
						If Not dr.Read Then
							dr.Close()
							CloseConn()
							MessageBox.Show("anda tidak Memiliki akses ke kategori dan sub kategori ini ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							ComboBox4.SelectedIndex = -1 : Exit Sub
						End If
					End Using
				End If
			End If

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

			'SQL = "select  b.Id_Sub_Kategori_Jenis, b.Id_Kategori_Jenis "
			'SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis b, N_EMI_Master_Kategori_Jenis c, N_EMI_Master_Role_Sub_Kategori g "
			'SQL = SQL & "where b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
			'SQL = SQL & "and b.Kode_Perusahaan = g.Kode_Perusahaan and b.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
			'SQL = SQL & "and b.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "
			'SQL = SQL & "and b.Kode_Perusahaan =  '" & KodePerusahaan & "' and b.Id_Kategori_Jenis = '" & arrid4.Item(CmbSK3_Jenis.SelectedIndex) & "' "
			'SQL = SQL & "and b.Id_Sub_Kategori_Jenis = '" & arridsub4.Item(CmbSK3_JenisSub.SelectedIndex) & "' "
			'Using dr = OpenTrans(SQL)
			'    If Not dr.Read Then
			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("anda tidak Memiliki akses ke kategori dan sub kategori ini ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        CmbSK3_JenisSub.SelectedIndex = -1 : Exit Sub
			'    End If
			'End Using

			If Asal_proses IsNot Nothing Then
				If Asal_proses.ToUpper <> "MASTER" AndAlso Not (Asal_proses = "" OrElse Asal_proses = "N_EMI_SD_Tambah_PR_Barang_Lain_Departement") Then
					SQL = "select a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
					SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a "
					SQL = SQL & "inner JOIN N_EMI_Master_Kategori_Jenis b on a.kode_perusahaan = b.Kode_Perusahaan AND a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
					SQL = SQL & "inner JOIN N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain c ON a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Jenis = c.id_kategori_jenis and a.Id_Sub_Kategori_Jenis = c.id_sub_kategori_jenis "
					SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "AND c.user_id = '" & UserID & "' "
					SQL = SQL & "and a.Id_Kategori_Jenis = '" & arrid4.Item(CmbSK3_Jenis.SelectedIndex) & "' "
					SQL = SQL & "and a.Id_Sub_Kategori_Jenis = '" & arridsub4.Item(CmbSK3_JenisSub.SelectedIndex) & "' "
					Using dr = OpenTrans(SQL)
						If Not dr.Read Then
							dr.Close()
							CloseConn()
							MessageBox.Show("anda tidak Memiliki akses ke kategori dan sub kategori ini ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							CmbSK3_JenisSub.SelectedIndex = -1 : Exit Sub
						End If
					End Using
				End If
			End If

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

			'SQL = "select e.Kode_Kategori_Jenis, e.Keterangan as Kategori_Jenis, d.Kode_Sub_Kategori_Jenis, d.Keterangan as Sub_Kategori_Jenis, "
			'SQL = SQL & "c.Kode_Sub_Kategori_Jenis_1, c.Keterangan as Sub_Kategori_Jenis_1, b.Kode_Sub_Kategori_Jenis_2, b.Keterangan as Sub_Kategori_Jenis_2, "
			'SQL = SQL & "a.Kode_Sub_Kategori_Jenis_3, a.Keterangan as Sub_Kategori_Jenis_3, a.Prefix, "
			'SQL = SQL & "a.Id_Sub_Kategori_Jenis_3, a.Id_Sub_Kategori_Jenis_2, b.Id_Sub_Kategori_Jenis_1, c.Id_Sub_Kategori_Jenis, d.Id_Kategori_Jenis, "
			'SQL = SQL & "a.Satuan, a.Stock_Minimum, a.Berat, a.Berat_Kotor, a.Panjang, a.Lebar, a.Tinggi, a.Metode_Pengeluaran_Stok,f.Blob_Storage, f.Container, f.nama_file "
			'SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a, N_EMI_Master_Sub_Kategori_Jenis_2 b, N_EMI_Master_Sub_Kategori_Jenis_1 c, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis d, N_EMI_Master_Kategori_Jenis e,N_EMI_Katalog_Barang_lain f "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_2 = b.Id_Sub_Kategori_Jenis_2 "
			'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis_1 = c.Id_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis "
			'SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.Id_Sub_Kategori_Jenis_3 = '" & xid_sub_kategori3 & "' "
			'SQL = SQL & "and f.Kode_Perusahaan = a.Kode_Perusahaan and f.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3 "

			SQL = "select e.Kode_Kategori_Jenis, e.Keterangan as Kategori_Jenis, d.Kode_Sub_Kategori_Jenis, d.Keterangan as Sub_Kategori_Jenis, "
			SQL = SQL & "c.Kode_Sub_Kategori_Jenis_1, c.Keterangan as Sub_Kategori_Jenis_1, b.Kode_Sub_Kategori_Jenis_2, b.Keterangan as Sub_Kategori_Jenis_2, "
			SQL = SQL & "a.Kode_Sub_Kategori_Jenis_3, a.Keterangan as Sub_Kategori_Jenis_3, a.Prefix, "
			SQL = SQL & "a.Id_Sub_Kategori_Jenis_3, a.Id_Sub_Kategori_Jenis_2, b.Id_Sub_Kategori_Jenis_1, c.Id_Sub_Kategori_Jenis, d.Id_Kategori_Jenis, "
			SQL = SQL & "a.Satuan, a.Stock_Minimum, a.Berat, a.Berat_Kotor, a.Panjang, a.Lebar, a.Tinggi, a.Metode_Pengeluaran_Stok, "

			' ===== subquery katalog barang =====
			SQL = SQL & "ISNULL((select top 1 f.Blob_Storage from N_EMI_Katalog_Barang_lain f "
			SQL = SQL & "where f.Kode_Perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "and f.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3),NULL) as Blob_Storage, "

			SQL = SQL & "ISNULL((select top 1 f.Container from N_EMI_Katalog_Barang_lain f "
			SQL = SQL & "where f.Kode_Perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "and f.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3),NULL) as Container, "

			SQL = SQL & "ISNULL((select top 1 f.nama_file from N_EMI_Katalog_Barang_lain f "
			SQL = SQL & "where f.Kode_Perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "and f.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3),NULL) as nama_file "

			' ===== FROM =====
			SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a, "
			SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_2 b, "
			SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_1 c, "
			SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis d, "
			SQL = SQL & "N_EMI_Master_Kategori_Jenis e "

			' ===== WHERE =====
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.Id_Sub_Kategori_Jenis_2 = b.Id_Sub_Kategori_Jenis_2 "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and b.Id_Sub_Kategori_Jenis_1 = c.Id_Sub_Kategori_Jenis_1 "
			SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan "
			SQL = SQL & "and c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis "
			SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
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

					If General_Class.CekNULL(dr("Satuan")) = "" Then
						cmbSatuan.SelectedIndex = -1
					Else
						cmbSatuan.Text = dr("Satuan")
					End If

					If General_Class.CekNULL(dr("Metode_Pengeluaran_Stok")) = "" Then
						ComboBox11.SelectedIndex = -1
					Else
						ComboBox11.Text = dr("Metode_Pengeluaran_Stok")
					End If

					If General_Class.CekNULL(dr("Stock_Minimum")) = "" Then
						TextBox20.Text = ""
					Else
						TextBox20.Text = dr("Stock_Minimum")
					End If

					If General_Class.CekNULL(dr("Berat")) = "" Then
						TextBox21.Text = ""
					Else
						TextBox21.Text = dr("Berat")
					End If

					If General_Class.CekNULL(dr("Berat_Kotor")) = "" Then
						TextBox22.Text = ""
					Else
						TextBox22.Text = dr("Berat_Kotor")
					End If

					If General_Class.CekNULL(dr("Panjang")) = "" Then
						TextBox23.Text = ""
					Else
						TextBox23.Text = dr("Panjang")
					End If

					If General_Class.CekNULL(dr("Lebar")) = "" Then
						TextBox24.Text = ""
					Else
						TextBox24.Text = dr("Lebar")
					End If

					If General_Class.CekNULL(dr("Tinggi")) = "" Then
						TextBox25.Text = ""
					Else
						TextBox25.Text = dr("Tinggi")
					End If

					Dim blobnamePath As String
					Dim containerPath As String
					Dim url As String = ""

					If General_Class.CekNULL(dr("Blob_Storage")) = "" Then
						blobnamePath = ""
					Else
						blobnamePath = dr("Blob_Storage")
					End If

					If General_Class.CekNULL(dr("container")) = "" Then
						containerPath = ""
					Else
						containerPath = dr("container")
					End If

					Dim result = AzureHelper_EMI.DownloadFromAzure(
						containerPath,
						blobnamePath
					)

					If Not result.Success Then
						' ===== GAGAL / TIDAK ADA DATA =====
						PictureBox1.Image = Nothing

						SelectedFilePath = "NULL"
						namaFIleAsli = "NULL"

						btnUpload.Enabled = True

						' OPTIONAL: kalau mau tau kenapa gagal
						MessageBox.Show(result.Message)
					Else
						' ===== ADA DATA =====
						PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
						PictureBox1.BorderStyle = BorderStyle.FixedSingle

						PictureBox1.Image = Nothing
						PictureBox1.Refresh()
						PictureBox1.LoadAsync(result.Url)

						btnUpload.Enabled = False
						gambarBerubah = False

						SelectedFilePath = dr("Blob_Storage")

						If General_Class.CekNULL(dr("nama_file")) = "" Then
							namaFIleAsli = "NULL"
						Else
							namaFIleAsli = "'" & dr("nama_file") & "'"
						End If
					End If

					'url = AzureHelper_EMI.DownloadFromAzure(containerPath, blobnamePath)

					'If String.IsNullOrWhiteSpace(url) Then
					'    ' ===== TIDAK ADA DATA =====
					'    PictureBox1.Image = Nothing

					'    SelectedFilePath = ""
					'    namaFIleAsli = "NULL"

					'ElseIf url = "Gagal" Then
					'    PictureBox1.Image = Nothing

					'    SelectedFilePath = "NULL"
					'    namaFIleAsli = "NULL"
					'Else
					'    PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

					'    PictureBox1.BorderStyle = BorderStyle.FixedSingle

					'    PictureBox1.Image = Nothing
					'    PictureBox1.Refresh()
					'    PictureBox1.LoadAsync(url)

					'    SelectedFilePath = dr("Blob_Storage")

					'    If General_Class.CekNULL(dr("nama_file")) = "" Then
					'        namaFIleAsli = "NULL"
					'    Else
					'        namaFIleAsli = "'" & dr("nama_file") & "'"
					'    End If
					'End If

					labelnote.Visible = True

					BtnSimpan5.Text = "&Update"
					BtnHapus5.Enabled = True
					BtnSimpan5.Tag = "&Update"
				Else
					btnUpload.Enabled = True
					labelnote.Visible = False
					TextBox16.Enabled = True
					CmbSK3_Jenis.Enabled = True
					CmbSK3_JenisSub.Enabled = True
					CmbSK3_JenisSub1.Enabled = True
					CmbSK3_JenisSub2.Enabled = True
					xid_sub_kategori3 = ""
					TextBox17.Text = ""
					'TextBox18.Text = ""

					cmbSatuan.SelectedIndex = -1
					ComboBox11.SelectedIndex = -1
					TextBox20.Text = ""
					TextBox21.Text = ""
					TextBox22.Text = ""
					TextBox23.Text = ""
					TextBox24.Text = ""
					TextBox25.Text = ""

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

	'Private Sub ComboBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSK3_Jenis.KeyPress
	'    If e.KeyChar = Chr(13) Then CmbSK3_JenisSub.Focus()
	'End Sub

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
		If e.KeyChar = Chr(13) Then ComboBox11.Focus()
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnUpload.Click

		Dim ofd As New OpenFileDialog()
		ofd.Title = "Pilih Gambar"
		ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.webp"
		ofd.Multiselect = False

		If ofd.ShowDialog() = DialogResult.OK Then
			Dim filePath As String = ofd.FileName
			Dim fileInfo As New IO.FileInfo(filePath)

			SelectedFilePath = ofd.FileName

			' Max 2 MB
			Dim maxSize As Long = 2 * 1024 * 1024 ' 2MB

			If fileInfo.Length > maxSize Then
				MessageBox.Show("Ukuran gambar maksimal 2 MB!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End If

			' Tampilkan gambar
			Using fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)
				PictureBox1.Image = Image.FromStream(fs)
			End Using

			PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

			Dim namaFile As String = IO.Path.GetFileName(filePath)

			namaFileGambar = namaFile

			gambarBerubah = True

		End If
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		' 1. Hapus gambar dari PictureBox
		PictureBox1.Image = Nothing

		' 2. Hapus file fisik jika ada
		If namaFileGambar <> "" Then
			Dim pathImg As String = Application.StartupPath & "\Images\" & namaFileGambar
			If IO.File.Exists(pathImg) Then
				IO.File.Delete(pathImg)
			End If
		End If

		' 3. Kosongkan variabel
		namaFileGambar = ""
		namaFileAzure = ""
		gambarBerubah = True
		SelectedFilePath = ""

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

			If e.TabPageIndex < 2 Then
				e.Cancel = True
			End If

			'If e.TabPageIndex = 0 Then
			'    e.Cancel = True
			'End If

		ElseIf Asal_proses = "N_EMI_SD_Tambah_PR_Barang_Lain_Departement" Then

			If e.TabPageIndex < 2 Then
				e.Cancel = True
			End If
		ElseIf Not (Asal_proses = "" Or Asal_proses = "N_EMI_SD_Tambah_PR_Barang_Lain_Departement") Then
			If e.TabPageIndex <> 4 Then
				e.Cancel = True
			End If
		Else
			If e.TabPageIndex < 2 Then
				e.Cancel = True
			End If

		End If
	End Sub

	Private Sub ComboBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox11.KeyPress
		If e.KeyChar = Chr(13) Then cmbSatuan.Focus()
	End Sub

	Private Sub cmbSatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSatuan.KeyPress
		If e.KeyChar = Chr(13) Then TextBox20.Focus()
	End Sub

	Private Sub TextBox20_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox20.KeyPress
		If e.KeyChar = Chr(13) Then TextBox21.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub TextBox21_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox21.KeyPress
		If e.KeyChar = Chr(13) Then TextBox22.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub TextBox22_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox22.KeyPress
		If e.KeyChar = Chr(13) Then TextBox23.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub TextBox23_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox23.KeyPress
		If e.KeyChar = Chr(13) Then TextBox24.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub TextBox24_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox24.KeyPress
		If e.KeyChar = Chr(13) Then TextBox25.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub TextBox25_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox25.KeyPress
		If e.KeyChar = Chr(13) Then BtnSimpan5.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	'=========================================================================================================================================================
	'=     HANDLE BUTTON CHECK
	'=========================================================================================================================================================
	Private Sub Btn_Check_2_Click(sender As Object, e As EventArgs) Handles Btn_Check_2.Click
		If ComboBox1.SelectedIndex = -1 Then
			MessageBox.Show("Harap Pilih Dahulu Kode Kategori", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox1.DroppedDown = True
			ComboBox1.Focus()
			Exit Sub
		End If

		isCheck_2 = True
		ComboBox2.SelectedIndex = -1
		TextBox5.Text = ""
		Load_Data_Tab_2()

	End Sub

	Private Sub Btn_Check_3_Click(sender As Object, e As EventArgs) Handles Btn_Check_3.Click
		If ComboBox3.SelectedIndex = -1 Then
			MessageBox.Show("Harap Pilih Dahulu Kode Kategori", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox3.DroppedDown = True
			ComboBox3.Focus()
			Exit Sub
		End If

		isCheck_3 = True
		ComboBox5.SelectedIndex = -1
		TextBox11.Text = ""
		Load_Data_Tab_3()
	End Sub

	Private Sub Btn_Check_4_Click(sender As Object, e As EventArgs) Handles Btn_Check_4.Click
		If ComboBox6.SelectedIndex = -1 Then
			MessageBox.Show("Harap Pilih Dahulu Kode Kategori", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox6.DroppedDown = True
			ComboBox6.Focus()
			Exit Sub
		End If

		isCheck_4 = True
		ComboBox9.SelectedIndex = -1
		TextBox15.Text = ""
		Load_Data_Tab_4()
	End Sub

	Private Sub Btn_Check_5_Click(sender As Object, e As EventArgs) Handles Btn_Check_5.Click
		If CmbSK3_Jenis.SelectedIndex = -1 Then
			MessageBox.Show("Harap Pilih Dahulu Kode Kategori", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			CmbSK3_Jenis.DroppedDown = True
			CmbSK3_Jenis.Focus()
			Exit Sub
		End If

		isCheck_5 = True
		ComboBox15.SelectedIndex = -1
		TextBox19.Text = ""
		Load_Data_Tab_5()
	End Sub

	'=========================================================================================================================================================
	'=     HANDLE PAGINATION
	'=========================================================================================================================================================
	Private Sub BtnNext_GI_Click(sender As Object, e As EventArgs) Handles BtnNext_1.Click

		If CurrentPage_1 < totalpage_1 Then
			CurrentPage_1 += 1
			Load_Data_Tab_1(CurrentPage_1)

		End If

		If totalpage_1 = CurrentPage_1 Then
			BtnNext_1.Enabled = False
		Else
			BtnNext_1.Enabled = True
		End If

		If 1 = CurrentPage_1 Then
			BtnPrev_1.Enabled = False
		Else
			BtnPrev_1.Enabled = True
		End If

	End Sub

	Private Sub BtnPrev_1_Click(sender As Object, e As EventArgs) Handles BtnPrev_1.Click

		If CurrentPage_1 > 1 Then
			CurrentPage_1 -= 1
			Load_Data_Tab_1(CurrentPage_1)
		End If

		If totalpage_1 = CurrentPage_1 Then
			BtnNext_1.Enabled = False
		Else
			BtnNext_1.Enabled = True
		End If

		If 1 = CurrentPage_1 Then
			BtnPrev_1.Enabled = False
		Else
			BtnPrev_1.Enabled = True
		End If
	End Sub

	Private Sub BtnFirst_1_Click(sender As Object, e As EventArgs) Handles BtnFirst_1.Click

		CurrentPage_1 = 1
		Load_Data_Tab_1(CurrentPage_1)

		If totalpage_1 = CurrentPage_1 Then
			BtnNext_1.Enabled = False
		Else
			BtnNext_1.Enabled = True
		End If

		If 1 = CurrentPage_1 Then
			BtnPrev_1.Enabled = False
		Else
			BtnPrev_1.Enabled = True
		End If
	End Sub

	Private Sub BtnNext_2_Click(sender As Object, e As EventArgs) Handles BtnNext_2.Click
		If CurrentPage_2 < totalpage_2 Then
			CurrentPage_2 += 1
			Load_Data_Tab_2(CurrentPage_2)

		End If

		If totalpage_2 = CurrentPage_2 Then
			BtnNext_2.Enabled = False
		Else
			BtnNext_2.Enabled = True
		End If

		If 1 = CurrentPage_2 Then
			BtnPrev_2.Enabled = False
		Else
			BtnPrev_2.Enabled = True
		End If
	End Sub

	Private Sub BtnPrev_2_Click(sender As Object, e As EventArgs) Handles BtnPrev_2.Click
		If CurrentPage_2 > 1 Then
			CurrentPage_2 -= 1
			Load_Data_Tab_2(CurrentPage_2)
		End If

		If totalpage_2 = CurrentPage_2 Then
			BtnNext_2.Enabled = False
		Else
			BtnNext_2.Enabled = True
		End If

		If 1 = CurrentPage_2 Then
			BtnPrev_2.Enabled = False
		Else
			BtnPrev_2.Enabled = True
		End If
	End Sub

	Private Sub BtnFirst_2_Click(sender As Object, e As EventArgs) Handles BtnFirst_2.Click
		CurrentPage_2 = 1
		Load_Data_Tab_2(CurrentPage_2)

		If totalpage_2 = CurrentPage_2 Then
			BtnNext_2.Enabled = False
		Else
			BtnNext_2.Enabled = True
		End If

		If 1 = CurrentPage_2 Then
			BtnPrev_2.Enabled = False
		Else
			BtnPrev_2.Enabled = True
		End If
	End Sub

	Private Sub BtnNext_3_Click(sender As Object, e As EventArgs) Handles BtnNext_3.Click
		If CurrentPage_3 < totalpage_3 Then
			CurrentPage_3 += 1
			Load_Data_Tab_3(CurrentPage_3)

		End If

		If totalpage_3 = CurrentPage_3 Then
			BtnNext_3.Enabled = False
		Else
			BtnNext_3.Enabled = True
		End If

		If 1 = CurrentPage_3 Then
			BtnPrev_3.Enabled = False
		Else
			BtnPrev_3.Enabled = True
		End If
	End Sub

	Private Sub BtnPrev_3_Click(sender As Object, e As EventArgs) Handles BtnPrev_3.Click
		If CurrentPage_3 > 1 Then
			CurrentPage_3 -= 1
			Load_Data_Tab_3(CurrentPage_3)
		End If

		If totalpage_3 = CurrentPage_3 Then
			BtnNext_3.Enabled = False
		Else
			BtnNext_3.Enabled = True
		End If

		If 1 = CurrentPage_3 Then
			BtnPrev_3.Enabled = False
		Else
			BtnPrev_3.Enabled = True
		End If
	End Sub

	Private Sub BtnFirst_3_Click(sender As Object, e As EventArgs) Handles BtnFirst_3.Click
		CurrentPage_3 = 1
		Load_Data_Tab_3(CurrentPage_3)

		If totalpage_3 = CurrentPage_3 Then
			BtnNext_3.Enabled = False
		Else
			BtnNext_3.Enabled = True
		End If

		If 1 = CurrentPage_3 Then
			BtnPrev_3.Enabled = False
		Else
			BtnPrev_3.Enabled = True
		End If
	End Sub

	Private Sub BtnNext_4_Click(sender As Object, e As EventArgs) Handles BtnNext_4.Click
		If CurrentPage_4 < totalpage_4 Then
			CurrentPage_4 += 1
			Load_Data_Tab_4(CurrentPage_4)

		End If

		If totalpage_4 = CurrentPage_4 Then
			BtnNext_4.Enabled = False
		Else
			BtnNext_4.Enabled = True
		End If

		If 1 = CurrentPage_4 Then
			BtnPrev_4.Enabled = False
		Else
			BtnPrev_4.Enabled = True
		End If
	End Sub

	Private Sub BtnPrev_4_Click(sender As Object, e As EventArgs) Handles BtnPrev_4.Click
		If CurrentPage_4 > 1 Then
			CurrentPage_4 -= 1
			Load_Data_Tab_4(CurrentPage_4)
		End If

		If totalpage_4 = CurrentPage_4 Then
			BtnNext_4.Enabled = False
		Else
			BtnNext_4.Enabled = True
		End If

		If 1 = CurrentPage_4 Then
			BtnPrev_4.Enabled = False
		Else
			BtnPrev_4.Enabled = True
		End If
	End Sub

	Private Sub BtnFirst_4_Click(sender As Object, e As EventArgs) Handles BtnFirst_4.Click
		CurrentPage_4 = 1
		Load_Data_Tab_4(CurrentPage_4)

		If totalpage_4 = CurrentPage_4 Then
			BtnNext_4.Enabled = False
		Else
			BtnNext_4.Enabled = True
		End If

		If 1 = CurrentPage_4 Then
			BtnPrev_4.Enabled = False
		Else
			BtnPrev_4.Enabled = True
		End If
	End Sub

	Private Sub BtnNext_5_Click(sender As Object, e As EventArgs) Handles BtnNext_5.Click
		If CurrentPage_5 < totalpage_5 Then
			CurrentPage_5 += 1
			Load_Data_Tab_5(CurrentPage_5)

		End If

		If totalpage_5 = CurrentPage_5 Then
			BtnNext_5.Enabled = False
		Else
			BtnNext_5.Enabled = True
		End If

		If 1 = CurrentPage_5 Then
			BtnPrev_5.Enabled = False
		Else
			BtnPrev_5.Enabled = True
		End If
	End Sub

	Private Sub BtnPrev_5_Click(sender As Object, e As EventArgs) Handles BtnPrev_5.Click
		If CurrentPage_5 > 1 Then
			CurrentPage_5 -= 1
			Load_Data_Tab_5(CurrentPage_5)
		End If

		If totalpage_5 = CurrentPage_5 Then
			BtnNext_5.Enabled = False
		Else
			BtnNext_5.Enabled = True
		End If

		If 1 = CurrentPage_5 Then
			BtnPrev_5.Enabled = False
		Else
			BtnPrev_5.Enabled = True
		End If
	End Sub

	Private Sub BtnFirst_5_Click(sender As Object, e As EventArgs) Handles BtnFirst_5.Click
		CurrentPage_5 = 1
		Load_Data_Tab_5(CurrentPage_5)

		If totalpage_5 = CurrentPage_5 Then
			BtnNext_5.Enabled = False
		Else
			BtnNext_5.Enabled = True
		End If

		If 1 = CurrentPage_5 Then
			BtnPrev_5.Enabled = False
		Else
			BtnPrev_5.Enabled = True
		End If
	End Sub

End Class
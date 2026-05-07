Public Class N_EMI_SD_Trial_Production_Order_Pilih_Formula

	Dim AksesNamaBahan As Boolean = False

	Dim arrcari, arrJenis, arrInisialFaktur As New ArrayList
	Public lokasi_asal As String

	Dim SwitchAutoComplete As Boolean = False

	Dim NoFakturIndependent As String

	Private Sub N_EMI_SD_Trial_Production_Order_Pilih_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Try
			OpenConn()

			'====================
			'=     CEK ROLE     =
			'====================
			If CekButtonRole("Akses_Nama_Barang") = "Y" Then
				AksesNamaBahan = True
			Else
				AksesNamaBahan = False
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Lv_Data_Formula_Pending.Columns.Clear()
		Lv_Data_Formula_Pending.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left) '0
		Lv_Data_Formula_Pending.Columns.Add("Jumlah", 90, HorizontalAlignment.Right) '1
		Lv_Data_Formula_Pending.Columns.Add("Satuan", 70, HorizontalAlignment.Center) '2
		Lv_Data_Formula_Pending.View = View.Details

		Lv_Data_Formula_Completed.Columns.Clear()
		Lv_Data_Formula_Completed.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left) '0
		Lv_Data_Formula_Completed.Columns.Add("Jumlah", 90, HorizontalAlignment.Right) '1
		Lv_Data_Formula_Completed.Columns.Add("Satuan", 70, HorizontalAlignment.Center) '2
		Lv_Data_Formula_Completed.View = View.Details

		Lv_Detail_Bahan.Columns.Clear()
		Lv_Detail_Bahan.Columns.Add("Kode Bahan", If(AksesNamaBahan, 130, 200), HorizontalAlignment.Left) '0
		Lv_Detail_Bahan.Columns.Add("Nama Bahan", If(AksesNamaBahan, 300, 0), HorizontalAlignment.Left) '1
		Lv_Detail_Bahan.Columns.Add("Persentase", If(AksesNamaBahan, 130, 150), HorizontalAlignment.Center) '2
		Lv_Detail_Bahan.Columns.Add("Jumlah", If(AksesNamaBahan, 150, 180), HorizontalAlignment.Right) '3
		Lv_Detail_Bahan.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '4
		Lv_Detail_Bahan.View = View.Details

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '0
		Lv_Barang.Columns.Add("Nama Barang", 380, HorizontalAlignment.Left) '1
		Lv_Barang.View = View.Details

		Try
			OpenConn()

			Cmb_Satuan.Items.Clear()
			SQL = "select Satuan from EMI_Satuan "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Satuan.Items.Add(Dr("Satuan"))
				Loop
			End Using

			arrInisialFaktur.Clear() : cmb_Lokasi_Init_Faktur.Items.Clear()
			SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & lokasi_asal & "' order by Kode_Stock_Owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					cmb_Lokasi_Init_Faktur.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()

	End Sub

	Private Sub get_no_faktur()
		Dim fIO = "IT"
		NoFakturIndependent = fIO & arrInisialFaktur.Item(cmb_Lokasi_Init_Faktur.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
									 General_Class.Get_Last_Number2("N_EMI_Transaksi_Trial_Independent_Order", "no_faktur", Jumlah_Digit,
									 "Kode_perusahaan", KodePerusahaan,
									 "And", "substring(no_faktur,1," & Len(fIO) + Len(arrInisialFaktur.Item(cmb_Lokasi_Init_Faktur.SelectedIndex)) + 6 & ")", fIO & arrInisialFaktur.Item(cmb_Lokasi_Init_Faktur.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))
	End Sub

	Private Sub Kosong()

		NoFakturIndependent = ""

		SwitchAutoComplete = True
		Txt_KdBarang.Text = ""
		Txt_NmBarang.Text = ""
		SwitchAutoComplete = False

		Lv_Data_Formula_Pending.Items.Clear()
		Lv_Data_Formula_Completed.Items.Clear()
		Lv_Detail_Bahan.Items.Clear()
		Txt_Jumlah.Text = ""
		Txt_Keterangan.Text = ""
		Cmb_Satuan.SelectedIndex = -1

		Txt_TanggaFormula.Text = ""

		cmb_Lokasi_Init_Faktur.SelectedIndex = 0

		Txt_KdBarang.Focus()

		TabControl1.SelectedIndex = 0

	End Sub

	Private Sub Txt_Kd_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
		If SwitchAutoComplete Then Exit Sub

		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(1090, 81)
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(146, 81)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			SQL = "select a.Kode_Barang_inq,a.Nama, a.Satuan, c.lokasi_gudang  "
			SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
			SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
			SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
			SQL = SQL & "and a.Kode_Barang_inq like '%" & Txt_KdBarang.Text & "%' and aktif = 'Y' and flag_finished_good= 'Y' "
			SQL = SQL & "group by a.Kode_Barang_inq,a.Nama, a.Satuan,c.lokasi_gudang "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang_inq"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Nm_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
		If SwitchAutoComplete Then Exit Sub

		If Txt_NmBarang.Text.Trim.Length = 0 Then
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(1090, 81)
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(146, 81)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			SQL = "select a.Kode_Barang_inq,a.Nama, a.Satuan, c.lokasi_gudang  "
			SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
			SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
			SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
			SQL = SQL & "and a.Nama like '%" & Txt_NmBarang.Text & "%' and aktif = 'Y' and flag_finished_good= 'Y' "
			SQL = SQL & "group by a.Kode_Barang_inq,a.Nama, a.Satuan,c.lokasi_gudang "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang_inq"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
		If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Barang.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_KdBarang.Text = OpsiSeluruh Then

				SQL = "select a.Kode_Barang_inq,a.Nama, a.Satuan, c.lokasi_gudang  "
				SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
				SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
				SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
				SQL = SQL & "and a.Kode_Barang_inq = '" & Txt_KdBarang.Text & "' and aktif = 'Y' and flag_finished_good= 'Y' "
				SQL = SQL & "group by a.Kode_Barang_inq,a.Nama, a.Satuan,c.lokasi_gudang "
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_KdBarang.Text = Dr("Kode_Barang_inq")
						Txt_NmBarang.Text = Dr("Nama")
						Btn_Get_Formula.Focus()
					Else
						MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_KdBarang.Text = ""
						Txt_NmBarang.Text = ""
						Txt_KdBarang.Focus()
					End If

					Lv_Barang.Visible = False
					Lv_Barang.Location = New Point(1090, 81)
				End Using
			Else
				Btn_Get_Formula.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
			Txt_KdBarang_Leave(Txt_KdBarang, e)

			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(1090, 81)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_KdBarang_Leave(Txt_NmBarang, e)

			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(1090, 81)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick, ListView1.DoubleClick
		If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

		SwitchAutoComplete = True
		Txt_KdBarang.Text = KdBarang
		Txt_NmBarang.Text = NmKdBarang
		SwitchAutoComplete = False

		Lv_Barang.Visible = False
		Lv_Barang.Location = New Point(1090, 81)

		Btn_Get_Formula.Focus()
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown, ListView1.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	Private Sub Btn_Get_Formula_Click(sender As Object, e As EventArgs) Handles Btn_Get_Formula.Click
		If Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Harap Pilih Dahulu Barang Yang Ingin Di Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus()
			Exit Sub
		End If

		TabControl1_SelectedIndexChanged(sender, New EventArgs)

	End Sub

	Private Sub GetDataFormulaPending()

		Try
			OpenConn()

			Dim Kode_Barang As String = Txt_KdBarang.Text.Trim

			Lv_Data_Formula_Pending.Items.Clear() : Lv_Detail_Bahan.Items.Clear()
			Txt_Jumlah.Text = "" : Txt_Keterangan.Text = ""
			Txt_TanggaFormula.Text = ""
			'Cmb_Satuan.SelectedIndex = -1
			SQL = "select a.No_Faktur, a.Kode_Barang, b.nama as Nama_Barang, a.Hasil, a.Satuan_Hasil "
			SQL &= $"from Emi_Transaksi_Formulator a "
			SQL &= $"inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Status is NULL "
			SQL &= $"and a.Kode_Barang = '{Kode_Barang}' "
			SQL &= $"and a.Flag_Validasi_Main = 'Y' "
			SQL &= $"and a.hasTrial is null "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Lv As ListViewItem
					Lv = Lv_Data_Formula_Pending.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Format(Dr("Hasil"), "N0"))
					Lv.SubItems.Add(Dr("Satuan_Hasil"))

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub GetDataFormulaCompleted()

		Try
			OpenConn()

			Dim Kode_Barang As String = Txt_KdBarang.Text.Trim

			Lv_Data_Formula_Completed.Items.Clear() : Lv_Detail_Bahan.Items.Clear()
			Txt_Jumlah.Text = "" : Txt_Keterangan.Text = ""
			Txt_TanggaFormula.Text = ""
			'Cmb_Satuan.SelectedIndex = -1
			SQL = "select a.No_Faktur, a.Kode_Barang, b.nama as Nama_Barang, a.Hasil, a.Satuan_Hasil "
			SQL &= $"from Emi_Transaksi_Formulator a "
			SQL &= $"inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Status is NULL "
			SQL &= $"and a.Kode_Barang = '{Kode_Barang}' "
			SQL &= $"and a.Flag_Validasi_Main = 'Y' "
			SQL &= $"and a.hasTrial = 'Y' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Lv As ListViewItem
					Lv = Lv_Data_Formula_Completed.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Format(Dr("Hasil"), "N0"))
					Lv.SubItems.Add(Dr("Satuan_Hasil"))

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Lv_Data_Formula_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data_Formula_Pending.SelectedIndexChanged
		If Lv_Data_Formula_Pending.Items.Count = 0 Or Lv_Data_Formula_Pending.FocusedItem.Index = -1 Then Exit Sub

		Try
			OpenConn()

			Dim SelectedNoFaktur As String = Lv_Data_Formula_Pending.FocusedItem.SubItems(0).Text

			Txt_TanggaFormula.Text = ""
			SQL = "Select status, tanggal From Emi_Transaksi_Formulator "
			SQL &= $"where kode_perusahaan = {KodePerusahaan} "
			SQL &= $"and no_faktur = '{SelectedNoFaktur}' "
			SQL &= $"and hasTrial is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("status")) = "Y" Then
						Dr.Close()
						CloseConn()
						MessageBox.Show($"Formula Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					Txt_TanggaFormula.Text = Format(Dr("Tanggal"), "dd MMM yyyy")
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Lv_Detail_Bahan.Items.Clear()
			Txt_Jumlah.Text = "" : Txt_Keterangan.Text = ""
			'Cmb_Satuan.SelectedIndex = -1
			SQL = "select b.Kode_Barang as Kode_Bahan, c.nama as Nama_Bahan, b.Persentase, b.Jumlah, b.satuan, a.Satuan_Hasil, d.Satuan as Satuan_Barang, a.Tanggal "
			SQL &= $"from Emi_Transaksi_Formulator a "
			SQL &= $"inner join EMI_Transaksi_Formulator_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			SQL &= $"inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
			SQL &= $"inner join barang d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and a.Kode_Barang = d.Kode_Barang_inq "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Status is NULL "
			SQL &= $"and a.Flag_Validasi = 'Y' "
			SQL &= $"and a.hasTrial is null "
			SQL &= $"and a.No_Faktur = '{SelectedNoFaktur}' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Detail_Bahan.Items.Add(Dr("Kode_Bahan"))
					Lv.SubItems.Add(If(AksesNamaBahan, Dr("Nama_Bahan"), "X"))
					Lv.SubItems.Add(Format(Dr("Persentase"), "N2"))
					Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
					Lv.SubItems.Add(Dr("satuan"))

					Cmb_Satuan.SelectedItem = Dr("Satuan_Barang").ToString.Trim
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Txt_Jumlah_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Jumlah.KeyDown
		Dim txt As TextBox = CType(sender, TextBox)

		'=====================================
		'=     BLOKIR CTRL + V (PASTE)       =
		'=====================================
		If e.Control AndAlso e.KeyCode = Keys.V Then
			e.SuppressKeyPress = True
			Return
		End If

		'=====================================
		'=     IZINKAN ARROW DAN CONTROL     =
		'=====================================
		If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
		   e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
		   e.KeyCode = Keys.Home OrElse e.KeyCode = Keys.End OrElse
		   e.KeyCode = Keys.Tab OrElse e.Control Then
			Return
		End If

		'===============================
		'=     IZINKAN INPUT ANGKA     =
		'===============================
		If (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9) OrElse
		   (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9) Then
			Return
		End If

		If e.KeyCode = Keys.Oemcomma OrElse e.KeyCode = Keys.OemPeriod OrElse
		   e.KeyCode = Keys.Decimal Then

			'===========================================================
			'=     CEK APAKAH SUDAH ADA KOMA ATAU TITIK DI TEXTBOX     =
			'===========================================================
			If txt.Text.Contains(",") OrElse txt.Text.Contains(".") Then
				e.SuppressKeyPress = True
			End If
			Return
		End If
		e.SuppressKeyPress = True
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_Jumlah.Text.Trim.Length = 0 Then
			MessageBox.Show("Input Jumlah Produksi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Jumlah.Focus()
			Exit Sub
		ElseIf Cmb_Satuan.SelectedIndex = -1 Then
			MessageBox.Show("Satuan Tidak Ditmeukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Keterangan.Focus()
			Exit Sub
		End If

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			get_no_faktur()

			Dim SelectedFormulaFaktur As String = ""
			If TabControl1.SelectedIndex = 0 Then
				SelectedFormulaFaktur = Lv_Data_Formula_Pending.FocusedItem.SubItems(0).Text
			ElseIf TabControl1.SelectedIndex = 1 Then
				SelectedFormulaFaktur = Lv_Data_Formula_Completed.FocusedItem.SubItems(0).Text
			Else
				CloseTrans()
				CloseConn()
				MessageBox.Show("Terjadi Kesalahan, Harap Pilih No Formula Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'=================================================
			'=     CONVERT KD BARANG IQ+NQ = KODE BARANG     =
			'=================================================
			Dim kode_barang_biasa As String = ""
			SQL = $"select top 1 Kode_Barang from Barang where Kode_Perusahaan = '{KodePerusahaan}' and (Kode_Barang_Inq = '{Txt_KdBarang.Text.Trim}' or Kode_Barang = '{Txt_KdBarang.Text.Trim}')"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					kode_barang_biasa = Dr("Kode_Barang")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Kode Barang {Txt_KdBarang.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "insert into N_EMI_Transaksi_Trial_Independent_Order(kode_perusahaan, no_faktur, lokasi, tanggal, jam, userid, keterangan, No_Faktur_Formula) values ("
			SQL = SQL & "'" & KodePerusahaan & "', '" & NoFakturIndependent & "', '" & lokasi_asal & "' , '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
			SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "',"
			SQL = SQL & "'" & UserID & "', '" & Txt_Keterangan.Text.Trim & "', '" & SelectedFormulaFaktur & "' )"
			ExecuteTrans(SQL)

			Dim lokasi_barang As String
			SQL = " select b.Kode_Stock_Owner_Gudang from stock_owner a , Binding_Lokasi_Gudang b where "
			SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.Kode_Stock_Owner = '" & lokasi_asal & "' and b.Gudang_Default = 'Y' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					lokasi_barang = dr("kode_stock_owner_gudang")
				Else
					dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show(Base_Language.Lang_Global_Error_Lokasi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Dim jenis_produk As String = "NULL"
			SQL = "select top 1 c.Id_Jenis_Produk "
			SQL &= $"from barang a "
			SQL &= $"inner join EMI_Varian b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Varian = b.Id_Varian "
			SQL &= $"inner join EMI_Jenis_Produk c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Jenis_Produk = c.Id_Jenis_Produk "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Kode_Barang = '{kode_barang_biasa.Trim}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					jenis_produk = Dr("Id_Jenis_Produk")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Jenis Produk untuk barang {Txt_KdBarang.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "insert into N_EMI_Transaksi_Trial_Independent_Order_Detail(kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah_produksi,satuan,id_jenis_produk) values ( "
			SQL = SQL & "'" & KodePerusahaan & "', '" & NoFakturIndependent & "', '" & lokasi_barang & "' ,'" & kode_barang_biasa.Trim & "',  "
			SQL = SQL & "'" & HilangkanTanda(Txt_Jumlah.Text) & "', '" & Cmb_Satuan.Text & "'," & jenis_produk & ") "
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Me.Close()
		N_EMI_Transaksi_Trial_Production_Order.Button1_Click_1(Btn_Simpan, e)

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Lv_Data_Formula_Completed_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data_Formula_Completed.SelectedIndexChanged
		If Lv_Data_Formula_Completed.Items.Count = 0 Or Lv_Data_Formula_Completed.FocusedItem.Index = -1 Then Exit Sub

		Try
			OpenConn()

			Dim SelectedNoFaktur As String = Lv_Data_Formula_Completed.FocusedItem.SubItems(0).Text

			Txt_TanggaFormula.Text = ""
			SQL = "Select status, tanggal From Emi_Transaksi_Formulator "
			SQL &= $"where kode_perusahaan = {KodePerusahaan} "
			SQL &= $"and no_faktur = '{SelectedNoFaktur}' "
			SQL &= $"and hasTrial = 'Y' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("status")) = "Y" Then
						Dr.Close()
						CloseConn()
						MessageBox.Show($"Formula Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					Txt_TanggaFormula.Text = Format(Dr("Tanggal"), "dd MMM yyyy")
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Lv_Detail_Bahan.Items.Clear()
			Txt_Jumlah.Text = "" : Txt_Keterangan.Text = ""
			'Cmb_Satuan.SelectedIndex = -1
			SQL = "select b.Kode_Barang as Kode_Bahan, c.nama as Nama_Bahan, b.Persentase, b.Jumlah, b.satuan, a.Satuan_Hasil, d.Satuan as Satuan_Barang, a.Tanggal "
			SQL &= $"from Emi_Transaksi_Formulator a "
			SQL &= $"inner join EMI_Transaksi_Formulator_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			SQL &= $"inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
			SQL &= $"inner join barang d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and a.Kode_Barang = d.Kode_Barang_inq "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Status is NULL "
			SQL &= $"and a.Flag_Validasi = 'Y' "
			SQL &= $"and a.hasTrial = 'Y' "
			SQL &= $"and a.No_Faktur = '{SelectedNoFaktur}' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Detail_Bahan.Items.Add(Dr("Kode_Bahan"))
					Lv.SubItems.Add(If(AksesNamaBahan, Dr("Nama_Bahan"), "X"))
					Lv.SubItems.Add(Format(Dr("Persentase"), "N2"))
					Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
					Lv.SubItems.Add(Dr("satuan"))

					Cmb_Satuan.SelectedItem = Dr("Satuan_Barang").ToString.Trim
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
		If TabControl1.SelectedIndex = -1 Or Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub

		If TabControl1.SelectedIndex = 0 Then

			GetDataFormulaPending()

		ElseIf TabControl1.SelectedIndex = 1 Then

			GetDataFormulaCompleted()

		End If

	End Sub

	'========================================================================================================================================================================================
	'=     UTILITY
	'========================================================================================================================================================================================
	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub Lv_Data_Formula_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data_Formula_Pending.MouseMove, Lv_Data_Formula_Completed.MouseMove
		Dim info As ListViewHitTestInfo = Lv_Data_Formula_Pending.HitTest(e.Location)

		If info.Item IsNot Nothing Then
			' Mouse sedang berada di atas row
			Lv_Data_Formula_Pending.Cursor = Cursors.Hand
		Else
			' Mouse tidak mengenai row
			Lv_Data_Formula_Pending.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Lv_Data_Formula_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data_Formula_Pending.MouseLeave, Lv_Data_Formula_Completed.MouseLeave
		Lv_Data_Formula_Pending.Cursor = Cursors.Default
	End Sub

	Private Sub Lv_Detail_Bahan_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Detail_Bahan.MouseMove
		Dim info As ListViewHitTestInfo = Lv_Detail_Bahan.HitTest(e.Location)

		If info.Item IsNot Nothing Then
			' Mouse sedang berada di atas row
			Lv_Detail_Bahan.Cursor = Cursors.Hand
		Else
			' Mouse tidak mengenai row
			Lv_Detail_Bahan.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Lv_Detail_Bahan_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Detail_Bahan.MouseLeave
		Lv_Detail_Bahan.Cursor = Cursors.Default
	End Sub

End Class
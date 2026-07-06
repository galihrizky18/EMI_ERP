Public Class N_EMI_Transaksi_Reformulasi_Formula_Trial

	Private WithEvents TypingTimer As New Timer()

	Dim JenisForm_Create As String = "Create Formula"
	Dim JenisForm_Reformulasi As String = "Reformulasi Formula"
	Dim JenisForm As New ArrayList From {JenisForm_Create, JenisForm_Reformulasi}

	Dim ListBahanBaku As New List(Of BahanBaku)
	Dim BS_BahanBaku As New BindingSource

	Dim SelectedFormula As String

	Dim arrDataCombobox As New List(Of DataCombobox)

	Dim switchAutoComplete As Boolean = False

	Dim Berat_PCS_Barang_Jadi As Double = 0

	Dim arrIdPenanggungJawab, arrFilter As New ArrayList

	Dim dgvDetailBahan_KlasifikasiBahan3, dgvDetailBahan_IDKlasifikasiBahan3, dgvDetailBahan_KodeBarang, dgvDetailBahan_NamaBarang,
		dgvDetailBahan_Kuantity, dgvDetailBahan_Satuan, dgvDetailBahan_NilaiPengali, dgvDetailBahan_SatuanBarang,
		dgvDetailBahan_Persentase, dgvDetailBahan_Harga, dgvDetailBahan_EstHargaPcs, dgvDetailBahan_SatuanHasil, dgvDetailBahan_NilaiSatuanHasil As String

	Dim cellDetailBahan_KlasifikasiBahan3 As Integer = 0
	Dim cellDetailBahan_IDKlasifikasiBahan3 As Integer = 1
	Dim cellDetailBahan_KodeBarang As Integer = 2
	Dim cellDetailBahan_NamaBarang As Integer = 3
	Dim cellDetailBahan_Kuantity As Integer = 4
	Dim cellDetailBahan_Satuan As Integer = 5
	Dim cellDetailBahan_NilaiPengali As Integer = 6
	Dim cellDetailBahan_SatuanBarang As Integer = 7
	Dim cellDetailBahan_Persentase As Integer = 8
	Dim cellDetailBahan_Harga As Integer = 9
	Dim cellDetailBahan_EstHargaPcs As Integer = 10
	Dim cellDetailBahan_SatuanHasil As Integer = 11
	Dim cellDetailBahan_NilaiSatuanHasil As Integer = 12

	Dim Lv_Moisture_ID, Lv_Moisture_Kode_Analisa, Lv_Moisture_Jenis_Analisa, Lv_Moisture_Flag_Perhitungan, Lv_Moisture_Aktivitas, Lv_Moisture_Range_Awal, Lv_Moisture_Range_Akhir As String

	Dim Item_Moisture_ID As Integer = 0
	Dim Item_Moisture_Kode_Analisa As Integer = 1
	Dim Item_Moisture_Jenis_Analisa As Integer = 2
	Dim Item_Moisture_Flag_Perhitungan As Integer = 3
	Dim Item_Moisture_Kode_Aktivitas As Integer = 4
	Dim Item_Moisture_Kode_Kategori As Integer = 5
	Dim Item_Moisture_Combobox As Integer = 6
	Dim Item_Moisture_Range_Awal As Integer = 7
	Dim Item_Moisture_Range_Akhir As Integer = 8

	Private Sub N_EMI_Transaksi_Reformulasi_Formula_Trial_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Dgv_Detail_Bahan.AutoGenerateColumns = False

		TypingTimer.Interval = 500
		TypingTimer.Enabled = False

		BS_BahanBaku.DataSource = ListBahanBaku
		Dgv_Detail_Bahan.DataSource = BS_BahanBaku

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 220, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details

		Lv_Barang.Location = New Point(815, 115)

		Lv_Moisture_Content.Columns.Clear()
		Lv_Moisture_Content.Columns.Add("Id", 0, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Kode Analisa", 130, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Jenis Analisa", 330, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Flag_Perhitungan", 0, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Kode_Aktivitas", 0, HorizontalAlignment.Left)
		Lv_Moisture_Content.View = View.Details

		Cmb_Filter.Items.Clear() : arrFilter.Clear()
		Cmb_Filter.Items.Add("Klasifikasi Bahan 3") : arrFilter.Add("b.Keterangan")
		Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
		Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("a.Nama")

		Try
			OpenConn()

			Cmb_Jenis_Form.Items.Clear()

			For Each Jenis In JenisForm
				Cmb_Jenis_Form.Items.Add(Jenis)
			Next

			Cmb_Lokasi_HO.Items.Clear()
			SQL = $"
				select Kode_Stock_Owner
				from stock_owner
				where Aktif='Y'
				and kode_perusahaan = '{KodePerusahaan}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Lokasi_HO.Items.Add(Dr("Kode_Stock_Owner"))
				Loop
			End Using
			Cmb_Lokasi_HO.Text = Lokasi

			Cmb_Lokasi_Barang.Items.Clear()
			SQL = $"
				select Kode_Stock_Owner
				from view_lokasi_Stock
				where Aktif='Y'
				and kode_perusahaan = '{KodePerusahaan}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Lokasi_Barang.Items.Add(Dr("Kode_Stock_Owner"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		kosong()
	End Sub

	Private Sub Get_No_Faktur_Create()
		TxtFormulator_NoFaktur.Text = fTransFormula & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("Emi_Transaksi_Formulator", "no_Faktur", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(no_Faktur, 1, " & Len(fTransFormula) + 4 & ")", fTransFormula & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub get_no_faktur_binding()
		TxtFormulator_NoFaktur_Binding.Text = fTransFormulaBinding & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("EMI_Transaksi_Formulator_Binding", "No_Faktur", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(no_Faktur, 1, " & Len(fTransFormulaBinding) + 4 & ")", fTransFormulaBinding & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub Get_No_Faktur_Reformulasi(ByVal NoFaktur As String)

		Dim NoFakturFinal As String = Microsoft.VisualBasic.Left(NoFaktur, 13)

		SQL = $"
			select count(kode_Perusahaan) as Jumlah
			from Emi_Transaksi_Formulator
			where kode_Perusahaan='{KodePerusahaan}'
			and No_Faktur like '{NoFakturFinal.Trim}%'
		"
		Using Dr = OpenTrans(SQL)
			If Dr.Read Then
				Dim NoUrut As Integer = If(Val(HilangkanTanda(Dr("Jumlah"))) = 1, Dr("Jumlah"), Dr("Jumlah"))
				TxtFormulator_NoFaktur.Text = NoFakturFinal.Trim & "-" & NoUrut
			End If
		End Using

	End Sub

	Private Sub Get_Data_DGV_Detail_Bahan(ByVal index As Integer)
		dgvDetailBahan_KlasifikasiBahan3 = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_KlasifikasiBahan3).Value
		dgvDetailBahan_IDKlasifikasiBahan3 = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_IDKlasifikasiBahan3).Value
		dgvDetailBahan_KodeBarang = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_KodeBarang).Value
		dgvDetailBahan_NamaBarang = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_NamaBarang).Value
		dgvDetailBahan_Kuantity = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_Kuantity).Value
		dgvDetailBahan_Satuan = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_Satuan).Value
		dgvDetailBahan_NilaiPengali = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_NilaiPengali).Value
		dgvDetailBahan_SatuanBarang = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_SatuanBarang).Value
		dgvDetailBahan_Persentase = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_Persentase).Value
		dgvDetailBahan_Harga = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_Harga).Value
		dgvDetailBahan_EstHargaPcs = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_EstHargaPcs).Value
		dgvDetailBahan_SatuanHasil = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_SatuanHasil).Value
		dgvDetailBahan_NilaiSatuanHasil = Dgv_Detail_Bahan.Rows(index).Cells(cellDetailBahan_NilaiSatuanHasil).Value
	End Sub

	Private Sub Get_SD_Moisture(ByVal index As Integer)
		Lv_Moisture_ID = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_ID).Value
		Lv_Moisture_Kode_Analisa = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Kode_Analisa).Value
		Lv_Moisture_Jenis_Analisa = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Jenis_Analisa).Value
		Lv_Moisture_Flag_Perhitungan = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Flag_Perhitungan).Value
		Lv_Moisture_Aktivitas = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Kode_Aktivitas).Value
		Lv_Moisture_Range_Awal = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Range_Awal).Value
		Lv_Moisture_Range_Akhir = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Range_Akhir).Value

	End Sub

	Private Sub Kosong_Isi_General()
		Dtp_Tanggal.Value = Date.Now

		Lv_Barang.Visible = False
		Cmb_Filter.Enabled = False

		SelectedFormula = ""
		Txt_Filter.Text = ""
		Cmb_Filter.SelectedIndex = -1

		TxtFormulator_NoFaktur.Text = ""
		TxtFormulator_NoFaktur_Binding.Text = ""
		Berat_PCS_Barang_Jadi = 0

		Cmb_Lokasi_Barang.SelectedIndex = -1
		Cmb_Penangung_Jawab.Items.Clear()
		Cmb_Satuan_Hasil.Items.Clear()
		arrIdPenanggungJawab.Clear()

		Dgv_Detail_Bahan.Rows.Clear()

		Cmb_Penangung_Jawab.Text = ""
		Cmb_Satuan_Hasil.Text = ""
		switchAutoComplete = True
		Txt_Kd_Barang.Text = ""
		Txt_Nm_Barang.Text = ""
		switchAutoComplete = False
		Txt_Hasil.Text = ""

		Txt_Total_Persen.Text = ""
		Txt_Total_Hpp_Pcs.Text = ""

		Txt_Kode_Analisa.Text = ""
		TXt_Jenis_Analisa.Text = ""
		Dgv_Moisture_Content.Rows.Clear()
		arrDataCombobox.Clear()

	End Sub

	Private Sub kosong()

		Kosong_Isi_General()

		Txt_Kd_Barang.Enabled = False
		Txt_Nm_Barang.Enabled = False
		Txt_Hasil.Enabled = False
		Cmb_Lokasi_Barang.Enabled = False
		Cmb_Penangung_Jawab.Enabled = False
		Cmb_Satuan_Hasil.Enabled = False

		Cmb_Jenis_Form.SelectedIndex = -1
		Btn_Show_Formula.Visible = False
		Btn_Get_Data.Visible = False

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		kosong()
	End Sub

	Private Sub Btn_Reset_Filter_Click(sender As Object, e As EventArgs) Handles Btn_Reset_Filter.Click
		Cmb_Filter.SelectedIndex = -1
		Txt_Filter.Text = ""
		BS_BahanBaku.DataSource = ListBahanBaku
	End Sub

	Private Sub Kosong_Jenis_Form_Create()
		Kosong_Isi_General()

		Txt_Kd_Barang.Enabled = True
		Txt_Nm_Barang.Enabled = True
		Txt_Hasil.Enabled = True
		Cmb_Penangung_Jawab.Enabled = True
		Cmb_Satuan_Hasil.Enabled = True

		Btn_Show_Formula.Visible = False
		Btn_Get_Data.Visible = True

		Try
			OpenConn()

			Get_No_Faktur_Create()

			Dim LokasiGudangDefault As String = ""
			SQL = $"
				Select Kode_Stock_Owner_Gudang
				from Binding_Lokasi_Gudang
				where Gudang_Default='Y'
				and Kode_Stock_Owner='{Lokasi}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					LokasiGudangDefault = Dr("Kode_Stock_Owner_Gudang")
				End If
			End Using
			Cmb_Lokasi_Barang.Text = LokasiGudangDefault

			Cmb_Penangung_Jawab.Items.Clear() : arrIdPenanggungJawab.Clear()
			SQL = $"
				select Nama,Id_Karyawan
				from Emi_Karyawan a, Emi_Jabatan_Internal b
				where a.id_jabatan=b.id_jabatan
				and b.flag_Tampil_Formulator='Y'
				and a.kode_perusahaan = '{KodePerusahaan}'
				order by nama
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Penangung_Jawab.Items.Add(Dr("Nama")) : arrIdPenanggungJawab.Add(Dr("Id_Karyawan"))
				Loop
			End Using

			Cmb_Penangung_Jawab.SelectedIndex = -1

			Cmb_Satuan_Hasil.Items.Clear()
			SQL = $"
				select Satuan
				from EMI_Satuan
				where Flag_Tampil_Berat='Y'
				and kode_perusahaan = '{KodePerusahaan}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Satuan_Hasil.Items.Add(Dr("Satuan"))
				Loop
			End Using
			Cmb_Satuan_Hasil.SelectedIndex = -1

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Txt_Kd_Barang.Focus()

	End Sub

	Private Sub Kosong_Jenis_Form_Reformulasi()
		Kosong_Isi_General()

		Txt_Kd_Barang.Enabled = False
		Txt_Nm_Barang.Enabled = False
		Txt_Hasil.Enabled = False
		Cmb_Lokasi_Barang.Enabled = False
		Cmb_Penangung_Jawab.Enabled = False
		Cmb_Satuan_Hasil.Enabled = False

		Btn_Show_Formula.Visible = True
		Btn_Get_Data.Visible = False
	End Sub

	Private Sub Cmb_Jenis_Form_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Form.SelectedIndexChanged
		If Cmb_Jenis_Form.SelectedIndex = -1 Then Exit Sub

		If JenisForm(Cmb_Jenis_Form.SelectedIndex) = JenisForm_Create Then
			Kosong_Jenis_Form_Create()
		ElseIf JenisForm(Cmb_Jenis_Form.SelectedIndex) = JenisForm_Reformulasi Then
			Kosong_Jenis_Form_Reformulasi()
		End If

	End Sub

	Private Sub Txt_Kd_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_Kd_Barang.Text.Trim.Length = 0 Then
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(1200, 115)
			Txt_Kd_Barang.Text = ""
			Txt_Nm_Barang.Text = ""
			Exit Sub
		Else
			Lv_Barang.Location = New Point(815, 115)
			Lv_Barang.Visible = True
		End If

		Try
			OpenConn()

			Lv_Barang.BeginUpdate()
			Lv_Barang.Items.Clear()
			Dim Lv As ListViewItem
			'SQL = $"
			'	select a.Kode_Barang_inq,a.Nama, a.Satuan, c.lokasi_gudang
			'	from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c
			'	where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis
			'	and a.Kode_Perusahaan='{KodePerusahaan}'
			'	and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang
			'	and aktif = 'Y' and flag_finished_good= 'Y'
			'	and a.Kode_Barang_inq like '%{Txt_Kd_Barang.Text.Trim}%'
			'	group by a.Kode_Barang_inq,a.Nama, a.Satuan,c.lokasi_gudang
			'"
			SQL = "select a.kode_barang, a.nama from barang a, emi_group_jenis b  where "
			SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi_Barang.Text & "' and "
			SQL = SQL & "a.kode_barang like '%" & Txt_Kd_Barang.Text & "%' and a.id_group_jenis =b.id_group_jenis and (Flag_Finished_Good='Y' or Flag_Tampil_Inquiry='Y' or flag_semi_fg='Y' )"
			SQL = SQL & "order by a.kode_barang"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					'Lv = Lv_Barang.Items.Add(Dr("Kode_Barang_inq"))
					Lv = Lv_Barang.Items.Add(Dr("kode_barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using
			Lv_Barang.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Kd_Barang_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.Leave
		If Txt_Kd_Barang.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Barang.Focused = True Then Exit Sub

		Try
			OpenConn()

			'SQL = $"
			'		select a.Kode_Barang_inq,a.Nama, a.Satuan, c.lokasi_gudang
			'		from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c
			'		where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis
			'		and a.Kode_Perusahaan='{KodePerusahaan}'
			'		and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang
			'		and aktif = 'Y' and flag_finished_good= 'Y'
			'		and a.Kode_Barang_inq = '{Txt_Kd_Barang.Text.Trim}'
			'		group by a.Kode_Barang_inq,a.Nama, a.Satuan,c.lokasi_gudang
			'	"

			SQL = "select a.kode_barang, a.nama from barang a, emi_group_jenis b  where "
			SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi_Barang.Text & "' and "
			SQL = SQL & "a.kode_barang = '" & Txt_Kd_Barang.Text & "' and a.id_group_jenis =b.id_group_jenis and (Flag_Finished_Good='Y' or Flag_Tampil_Inquiry='Y' or flag_semi_fg='Y' )"
			SQL = SQL & "order by a.kode_barang"
			Using Dr = Open(SQL)
				If Dr.Read Then
					Txt_Kd_Barang.Text = Dr("kode_barang")
					Txt_Nm_Barang.Text = Dr("Nama")
					Cmb_Penangung_Jawab.DroppedDown = True
					Cmb_Penangung_Jawab.Focus()
				Else
					MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_Kd_Barang.Text = ""
					Txt_Nm_Barang.Text = ""
					Txt_Kd_Barang.Focus()
				End If

				Lv_Barang.Visible = False
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Kd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Barang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_Kd_Barang.Text.Trim.Length = 0 Then Txt_Kd_Barang.Focus()
			Txt_Kd_Barang_Leave(Txt_Kd_Barang, e)

			Lv_Barang.Visible = False

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_Kd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Barang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_Nm_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nm_Barang.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_Nm_Barang.Text.Trim.Length = 0 Then
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(1200, 140)
			Txt_Kd_Barang.Text = ""
			Txt_Nm_Barang.Text = ""
			Exit Sub
		Else
			Lv_Barang.Location = New Point(815, 140)
			Lv_Barang.Visible = True
		End If

		Try
			OpenConn()

			Lv_Barang.BeginUpdate()
			Lv_Barang.Items.Clear()
			Dim Lv As ListViewItem
			'SQL = $"
			'	select a.Kode_Barang_inq,a.Nama, a.Satuan, c.lokasi_gudang
			'	from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c
			'	where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis
			'	and a.Kode_Perusahaan='{KodePerusahaan}'
			'	and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang
			'	and aktif = 'Y' and flag_finished_good= 'Y'
			'	and a.Nama like '%{Txt_Nm_Barang.Text.Trim}%'
			'	group by a.Kode_Barang_inq,a.Nama, a.Satuan,c.lokasi_gudang
			'"
			SQL = "select a.kode_barang, a.nama from barang a, emi_group_jenis b  where "
			SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi_Barang.Text & "' and "
			SQL = SQL & "a.nama like '%" & Txt_Nm_Barang.Text & "%' and a.id_group_jenis =b.id_group_jenis and (Flag_Finished_Good='Y' or Flag_Tampil_Inquiry='Y' or flag_semi_fg='Y' )"
			SQL = SQL & "order by a.kode_barang"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("kode_barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using
			Lv_Barang.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Nm_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Nm_Barang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_Nm_Barang.Text.Trim.Length = 0 Then Txt_Nm_Barang.Focus()
			Txt_Kd_Barang_Leave(Txt_Nm_Barang, e)

			Lv_Barang.Visible = False

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_Nm_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Nm_Barang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Then Exit Sub

		Dim Kode As String = Lv_Barang.FocusedItem.Text
		Dim Nama As String = Lv_Barang.FocusedItem.SubItems(1).Text

		switchAutoComplete = True
		Txt_Kd_Barang.Text = Kode
		Txt_Nm_Barang.Text = Nama
		switchAutoComplete = False

		Lv_Barang.Visible = False

		Cmb_Penangung_Jawab.DroppedDown = True
		Cmb_Penangung_Jawab.Focus()

	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	Private Sub Cmb_Penangung_Jawab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Penangung_Jawab.SelectedIndexChanged
		Txt_Hasil.Text = ""
		Cmb_Satuan_Hasil.SelectedIndex = -1
		Txt_Hasil.Focus()
	End Sub

	Private Sub Btn_Get_Data_Click(sender As Object, e As EventArgs) Handles Btn_Get_Data.Click
		If Cmb_Lokasi_Barang.SelectedIndex = -1 Then
			MessageBox.Show("Lokasi Barang belum dipilih . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_Kd_Barang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang belum dipilih . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kd_Barang.Focus()
			Exit Sub
		ElseIf Txt_Hasil.Text.Trim.Length = 0 Then
			MessageBox.Show("Hasil belum diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Hasil.Focus()
			Exit Sub
		ElseIf Cmb_Satuan_Hasil.SelectedIndex = -1 Then
			MessageBox.Show("Satuan Hasil belum dipilih . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Satuan_Hasil.DroppedDown = True
			Cmb_Satuan_Hasil.Focus()
			Exit Sub
		End If

		Try
			OpenConn()

			'================================
			'=     Hitung Berat Per PCS     =
			'================================
			SQL = $"
				select Berat from barang
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Stock_Owner = '{Cmb_Lokasi_Barang.Text.Trim}'
				and Kode_Barang = '{Txt_Kd_Barang.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Berat_PCS_Barang_Jadi = Val(HilangkanTanda(Dr("Berat")))
				End If
			End Using

			'Dgv_Detail_Bahan.Rows.Clear()
			SQL = $"
				select a.Kode_Barang, a.Nama, a.Id_Klasifikasi_Bahan3, b.Keterangan as Klasifikasi_Bahan3, a.Good_Stock,
					isnull((select x.Satuan from barang_Detail_Satuan x where x.Kode_Barang= a.Kode_Barang and x.Flag_Tampil_Display = 'Y'), NULL) as Satuan,
					case when exists(
							select 1 from Barang_SN z where a.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0
						) then (
							select top 1 dbo.get_hpp(z.serial_number) from Barang_SN z where a.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0
						order by z.tgl_masuk DESC)
						else a.estimasi_harga
					end Est_HPP,
					isnull((
						select z.Nilai_Pengali from emi_satuan_detail_perhitungan z
						where z.Kode_Perusahaan=a.Kode_Perusahaan
						and z.Satuan_Awal= (select x.Satuan from barang_Detail_Satuan x where x.Kode_Barang= a.Kode_Barang and x.Flag_Tampil_Display = 'Y')
						and z.satuan_akhir= a.Satuan
						and z.Jenis = 'MASA'
					), 0) as Nilai_Pengali,
					a.Satuan as Satuan_Barang
				from barang a
					inner join N_EMI_Master_Klasifikasi_Bahan_3 b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Klasifikasi_Bahan3 = b.Id_Klasifikasi_Bahan3
					inner join barang_Detail_Satuan c on a.Kode_Barang= c.Kode_Barang and c.Flag_Tampil_Display = 'Y'
					inner join EMI_Group_Jenis d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.Id_Group_Jenis = d.Id_Group_Jenis and d.flag_finished_good <> 'Y'
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Kode_Stock_Owner = '{Cmb_Lokasi_Barang.Text.Trim}'
				and a.Satuan is not null
				order by a.Id_Klasifikasi_Bahan3
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

#Region "Cara Lama Tanpa BindingSource"

							'Dgv_Detail_Bahan.Rows.Add()

							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_KlasifikasiBahan3).Value = .Rows(i).Item("Klasifikasi_Bahan3")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_IDKlasifikasiBahan3).Value = .Rows(i).Item("Id_Klasifikasi_Bahan3")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_KodeBarang).Value = .Rows(i).Item("Kode_Barang")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_NamaBarang).Value = .Rows(i).Item("Nama")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Value = Format(0, "N4")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Satuan).Value = .Rows(i).Item("Satuan")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_NilaiPengali).Value = .Rows(i).Item("Nilai_Pengali")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_SatuanBarang).Value = .Rows(i).Item("Satuan_Barang")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Persentase).Value = Format(0, "N2")

							'If General_Class.CekNULL(.Rows(i).Item("Est_HPP")) = "" Then
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Harga).Value = Format(0, "N2")
							'Else
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Harga).Value = Format(.Rows(i).Item("Est_HPP"), "N2")
							'End If
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_EstHargaPcs).Value = Format(0, "N2")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_SatuanHasil).Value = ""
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_NilaiSatuanHasil).Value = Format(0, "N2")

#End Region

							Dim Harga As Double = If(General_Class.CekNULL(.Rows(i).Item("Est_HPP")) = "", 0, .Rows(i).Item("Est_HPP"))

							ListBahanBaku.Add(
								New BahanBaku With {
									.klasifikasi_bahan_3 = Ds.Tables("MyTable").Rows(i).Item("Klasifikasi_Bahan3"),
									.id_klasifikasi_bahan3 = Ds.Tables("MyTable").Rows(i).Item("Id_Klasifikasi_Bahan3"),
									.kode_barang = Ds.Tables("MyTable").Rows(i).Item("Kode_Barang"),
									.nama = Ds.Tables("MyTable").Rows(i).Item("Nama"),
									.kuantity = Format(0, "N4"),
									.satuan = Ds.Tables("MyTable").Rows(i).Item("Satuan"),
									.nilai_pengali = Ds.Tables("MyTable").Rows(i).Item("Nilai_Pengali"),
									.satuan_barang = Ds.Tables("MyTable").Rows(i).Item("Satuan_Barang"),
									.persentase = Format(0, "N2"),
									.harga = Format(Harga, "N2"),
									.est_hpp_pcs = Format(0, "N2"),
									.satuan_hasil = Cmb_Satuan_Hasil.Text.Trim,
									.nilai_satuan_hasil = Format(0, "N2")
								}
							)

						Next
					End If
				End With
			End Using

			BS_BahanBaku.DataSource = Nothing
			BS_BahanBaku.DataSource = ListBahanBaku
			Dgv_Detail_Bahan.DataSource = BS_BahanBaku

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Cmb_Filter.Enabled = True

	End Sub

	Private Sub Dgv_Detail_Bahan_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Detail_Bahan.CellEndEdit
		If Dgv_Detail_Bahan.Rows.Count = 0 Or e.RowIndex < 0 Then Exit Sub

		Dim row As DataGridViewRow = Dgv_Detail_Bahan.Rows(e.RowIndex)
		Dim cell As DataGridViewCell = row.Cells(e.ColumnIndex)

		If cell.ColumnIndex = cellDetailBahan_Kuantity Then
			Get_Data_DGV_Detail_Bahan(row.Index)

			Dim KdBarang As String = row.Cells(cellDetailBahan_KodeBarang).Value

			Dim Kuantity As Double = Val(HilangkanTanda(cell.Value))

			If Not IsNumeric(dgvDetailBahan_Kuantity) Then
				cell.Value = Format(0, "N4")
				Dgv_Detail_Bahan.Rows(row.Index).Cells(cellDetailBahan_Persentase).Value = Format(0, "N2")
				Dgv_Detail_Bahan.Rows(row.Index).Cells(cellDetailBahan_EstHargaPcs).Value = Format(0, "N2")
				GetTotalDetailBarang()
				Exit Sub
			End If

			If cell.Value = 0 Or String.IsNullOrEmpty(cell.Value) Then
				cell.Value = Format(0, "N4")
				Dgv_Detail_Bahan.Rows(row.Index).Cells(cellDetailBahan_Persentase).Value = Format(0, "N2")
				Dgv_Detail_Bahan.Rows(row.Index).Cells(cellDetailBahan_EstHargaPcs).Value = Format(0, "N2")
				Dgv_Detail_Bahan.Rows(row.Index).Cells(cellDetailBahan_Kuantity).Style.BackColor = Color.White
				GetTotalDetailBarang()
				Exit Sub
			Else
				Dgv_Detail_Bahan.Rows(row.Index).Cells(cellDetailBahan_Kuantity).Style.BackColor = Color.LightGreen
			End If

			If dgvDetailBahan_Kuantity.Contains(",") Then
				CloseConn()
				MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				cell.Value = Format(0, "N4")
				GetTotalDetailBarang()
				Exit Sub
			End If

			Try
				OpenConn()

				Dim nilai_satuan_hasil As Double = 0
				SQL = $"select dbo.Ubah_Satuan('{KodePerusahaan}', 'MASA', '{dgvDetailBahan_KodeBarang}',
					'{dgvDetailBahan_Satuan}', '{Cmb_Satuan_Hasil.Text.Trim}', '{Val(HilangkanTanda(cell.Value))}') as Hasil
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						nilai_satuan_hasil = Dr("Hasil")
					End If
				End Using

				Dim Persentase As Double = (nilai_satuan_hasil * 100) / Val(Txt_Hasil.Text)

				'==========================
				'=     GET HPP PERPCS     =
				'==========================
				Dim HppPerPcs As Double = 0
				SQL = $"
					select a.Kode_Barang, a.Nama,  a.estimasi_harga,
						CASE WHEN EXISTS (
							SELECT 1 FROM Barang_SN z WHERE a.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0
						) THEN ISNULL(
							dbo.ubah_satuan(a.kode_perusahaan, 'masa', a.kode_barang, 'gram', a.satuan, ({Berat_PCS_Barang_Jadi} * (cast({Persentase} as float) / 100)))
							* (SELECT TOP 1 dbo.get_hpp(z.serial_number)
							FROM Barang_SN z
							WHERE A.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 ORDER BY z.tgl_masuk DESC), 0)
						ELSE
							ISNULL(dbo.ubah_satuan(a.kode_perusahaan, 'masa', a.kode_barang, 'gram', a.satuan, ({Berat_PCS_Barang_Jadi} * (cast({Persentase} as float) / 100))) * a.estimasi_harga, 0)
						END AS Est_HPP_Pcs
					from Barang a
					where a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.Kode_Stock_Owner = '{Cmb_Lokasi_Barang.Text.Trim}'
					and a.Kode_Barang = '{KdBarang}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						HppPerPcs = Val(HilangkanTanda(Dr("Est_HPP_Pcs")))
					End If
				End Using

				Dgv_Detail_Bahan.Rows(row.Index).Cells(cellDetailBahan_NilaiSatuanHasil).Value = Format(nilai_satuan_hasil, "N2")
				Dgv_Detail_Bahan.Rows(row.Index).Cells(cellDetailBahan_Persentase).Value = Format(Persentase, "N2")
				Dgv_Detail_Bahan.Rows(row.Index).Cells(cellDetailBahan_EstHargaPcs).Value = Format(HppPerPcs, "N2")

				cell.Value = Format(Kuantity, "N4")

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		End If

		GetTotalDetailBarang()
	End Sub

	Private Sub Txt_Kode_Analisa_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kode_Analisa.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_Kode_Analisa.Text.Trim.Length = 0 Then
			Lv_Moisture_Content.Visible = False
			Lv_Moisture_Content.Location = New Point(1200, 287)
			Txt_Kode_Analisa.Text = ""
			TXt_Jenis_Analisa.Text = ""
			Exit Sub
		Else
			Lv_Moisture_Content.Location = New Point(133, 287)
			Lv_Moisture_Content.Visible = True
		End If

		Try
			OpenConn()

			Lv_Moisture_Content.Items.Clear()
			SQL = "select id, Kode_Analisa, Jenis_Analisa, Flag_Perhitungan, Kode_Aktivitas_Lab "
			SQL &= $"from N_EMI_LAB_Jenis_Analisa "
			SQL &= $"where Kode_Analisa like '%{Txt_Kode_Analisa.Text.Trim}%' "
			SQL &= $"order by Jenis_Analisa "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Moisture_Content.Items.Add(Dr("id"))
					Lv.SubItems.Add(Dr("Kode_Analisa"))
					Lv.SubItems.Add(Dr("Jenis_Analisa"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Flag_Perhitungan")) = "", "T", Dr("Flag_Perhitungan")))
					Lv.SubItems.Add(Dr("Kode_Aktivitas_Lab"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Kode_Analisa_Leave(sender As Object, e As EventArgs) Handles Txt_Kode_Analisa.Leave
		If Txt_Kode_Analisa.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Moisture_Content.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_Kode_Analisa.Text = OpsiSeluruh Then

				SQL = "select id, Kode_Analisa, Jenis_Analisa, Flag_Perhitungan, Kode_Aktivitas_Lab "
				SQL &= $"from N_EMI_LAB_Jenis_Analisa "
				SQL &= $"where Kode_Analisa = '{Txt_Kode_Analisa.Text.Trim}' "
				SQL &= $"order by Jenis_Analisa "
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_Kode_Analisa.Text = Dr("Kode_Analisa")
						TXt_Jenis_Analisa.Text = Dr("Jenis_Analisa")
						Btn_Moisture_Insert.Focus()
					Else
						MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_Kode_Analisa.Text = ""
						TXt_Jenis_Analisa.Text = ""
						Txt_Kode_Analisa.Focus()
					End If

					Lv_Moisture_Content.Visible = False
					Lv_Moisture_Content.Location = New Point(1200, 287)
				End Using
			Else
				Btn_Moisture_Insert.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Kode_Analisa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kode_Analisa.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_Kode_Analisa.Text.Trim.Length = 0 Then Txt_Kode_Analisa.Focus()
			Txt_Kode_Analisa_Leave(Txt_Kode_Analisa, e)

			Lv_Moisture_Content.Visible = False
			Lv_Moisture_Content.Location = New Point(1200, 287)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_Kode_Analisa_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kode_Analisa.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Moisture_Content.Focus()
	End Sub

	Private Sub TXt_Jenis_Analisa_TextChanged(sender As Object, e As EventArgs) Handles TXt_Jenis_Analisa.TextChanged
		If switchAutoComplete Then Exit Sub

		If TXt_Jenis_Analisa.Text.Trim.Length = 0 Then
			Lv_Moisture_Content.Visible = False
			Lv_Moisture_Content.Location = New Point(1200, 287)
			Txt_Kode_Analisa.Text = ""
			TXt_Jenis_Analisa.Text = ""
			Exit Sub
		Else
			Lv_Moisture_Content.Visible = True
			Lv_Moisture_Content.Location = New Point(133, 287)
		End If

		Try
			OpenConn()

			Lv_Moisture_Content.Items.Clear()
			SQL = "select id, Kode_Analisa, Jenis_Analisa, Flag_Perhitungan, Kode_Aktivitas_Lab "
			SQL &= $"from N_EMI_LAB_Jenis_Analisa "
			SQL &= $"where Jenis_Analisa like '%{TXt_Jenis_Analisa.Text.Trim}%' "
			SQL &= $"order by Jenis_Analisa "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Moisture_Content.Items.Add(Dr("id"))
					Lv.SubItems.Add(Dr("Kode_Analisa"))
					Lv.SubItems.Add(Dr("Jenis_Analisa"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Flag_Perhitungan")) = "", "T", Dr("Flag_Perhitungan")))
					Lv.SubItems.Add(Dr("Kode_Aktivitas_Lab"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub TXt_Jenis_Analisa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXt_Jenis_Analisa.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_Kode_Analisa_Leave(TXt_Jenis_Analisa, e)

			Lv_Moisture_Content.Visible = False
			Lv_Moisture_Content.Location = New Point(1200, 287)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub TXt_Jenis_Analisa_KeyDown(sender As Object, e As KeyEventArgs) Handles TXt_Jenis_Analisa.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Moisture_Content.Focus()
	End Sub

	Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
		If Cmb_Filter.SelectedIndex = -1 Then
			Txt_Filter.Enabled = False
			Txt_Filter.BackColor = Color.FromArgb(235, 235, 235)
		Else
			Txt_Filter.Enabled = True
			Txt_Filter.BackColor = Color.White
		End If

		Txt_Filter.Text = ""
	End Sub

	Private Sub Lv_Moisture_Content_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Moisture_Content.DoubleClick
		If Lv_Moisture_Content.Items.Count = 0 Or Lv_Moisture_Content.FocusedItem.Index = -1 Then Exit Sub

		Dim KodeAnalisa As String = Lv_Moisture_Content.FocusedItem.SubItems(Item_Moisture_Kode_Analisa).Text
		Dim JenisAnalisa As String = Lv_Moisture_Content.FocusedItem.SubItems(Item_Moisture_Jenis_Analisa).Text

		switchAutoComplete = True
		Txt_Kode_Analisa.Text = KodeAnalisa
		TXt_Jenis_Analisa.Text = JenisAnalisa
		switchAutoComplete = False

		Lv_Moisture_Content.Visible = False
		Lv_Moisture_Content.Location = New Point(1200, 287)

		Btn_Moisture_Insert.Focus()
	End Sub

	Private Sub Lv_Moisture_Content_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Moisture_Content.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Moisture_Content_DoubleClick(Lv_Moisture_Content, e)
		End If
	End Sub

	Private Sub Btn_Moisture_Insert_Click(sender As Object, e As EventArgs) Handles Btn_Moisture_Insert.Click
		If Txt_Kode_Analisa.Text.Trim.Length = 0 Then
			MessageBox.Show("Harap Pilih Dahulu Jenis Analisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kode_Analisa.Focus()
			Exit Sub
		End If

		Dim SelectedKodeAnalisa As String = Txt_Kode_Analisa.Text.Trim
		'===========================================================
		'=     CEK APAKAH JENIS ANALISA SUDAH ADA DIDALAM LIST     =
		'===========================================================
		If Dgv_Moisture_Content.Rows.Count <> 0 Then
			For i As Integer = 0 To Dgv_Moisture_Content.Rows.Count - 1
				If Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Kode_Analisa).Value.ToString.Trim = SelectedKodeAnalisa.Trim Then
					MessageBox.Show($"Kode Analisa {SelectedKodeAnalisa} Sudah Ada didalam list", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_Kode_Analisa.Focus()
					Exit Sub
				End If
			Next
		End If

		Btn_Moisture_Insert.Enabled = False

		Try
			OpenConn()

			SQL = "select id, Kode_Analisa, Jenis_Analisa, Flag_Perhitungan, Kode_Aktivitas_Lab "
			SQL &= $"from N_EMI_LAB_Jenis_Analisa "
			SQL &= $"where Kode_Analisa = '{SelectedKodeAnalisa}' "
			SQL &= $"order by Jenis_Analisa "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dim n As Integer = Dgv_Moisture_Content.Rows.Add()

							Dim Id_Jenis_Analisa As String = .Rows(i).Item("Id")
							Dim Kode_Analisa As String = .Rows(i).Item("Kode_Analisa")
							Dim Jenis_Analisa As String = .Rows(i).Item("Jenis_Analisa")
							Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(.Rows(i).Item("Flag_Perhitungan")) = "", "T", .Rows(i).Item("Flag_Perhitungan"))
							Dim Kode_Aktivitas_Analisa As String = .Rows(i).Item("Kode_Aktivitas_Lab")

							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_ID).Value = Id_Jenis_Analisa
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Kode_Analisa).Value = Kode_Analisa
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Jenis_Analisa).Value = Jenis_Analisa
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Flag_Perhitungan).Value = Flag_Perhitungan_Analisa
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Kode_Aktivitas).Value = Kode_Aktivitas_Analisa
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Kode_Kategori).Value = If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan")

							If Flag_Perhitungan_Analisa = "Y" Then

								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).Style.BackColor = Color.LightYellow
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).Style.BackColor = Color.LightYellow
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).ReadOnly = False
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).ReadOnly = False
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Combobox).ReadOnly = True
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Combobox).Style.BackColor = Color.LightGray ' Warna Abu (Terkunci)

							ElseIf Flag_Perhitungan_Analisa = "T" Then

								Dim arrKriteria As New List(Of (id_switch As String, value As String, Label As String))
								SQL = "select distinct a.id as Id_Jenis_Analisa, a.Kode_Analisa, a.Jenis_Analisa, c.Id_QC_Formula, d.Id_Switch, d.Keterangan, d.Label_Keterangan "
								SQL &= $"from N_EMI_LAB_Jenis_Analisa a "
								SQL &= $"inner join N_EMI_LAB_Binding_Jenis_Analisa b on a.id = b.Id_Jenis_Analisa "
								SQL &= $"inner join EMI_Quality_Control c on b.Id_Quality_Control = c.Id_QC_Formula "
								SQL &= $"inner join EMI_Switch d on c.Id_QC_Formula = d.Id_QC_Formula "
								SQL &= $"where c.Kode_Perusahaan = '{KodePerusahaan}' "
								SQL &= $"and a.id = '{Id_Jenis_Analisa}' "
								Using Dr = OpenTrans(SQL)
									Do While Dr.Read
										arrKriteria.Add((Dr("Id_Switch"), Dr("Keterangan"), Dr("Label_Keterangan")))
									Loop
								End Using

								Dim dataCombo As New DataCombobox With {
									.ID_Analisa = Id_Jenis_Analisa,
									.Kode_Analisa = Kode_Analisa,
									.Datas = arrKriteria
								}
								arrDataCombobox.Add(dataCombo)

								' Set sebagai ComboBox
								Dim CellCmb As New DataGridViewComboBoxCell()
								CellCmb.Items.AddRange(arrKriteria.Select(Function(x) x.Label).ToArray())
								CellCmb.Style.BackColor = Color.LightYellow ' Warna Kuning (Wajib Pilih)
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Combobox) = CellCmb

								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Combobox).ReadOnly = False

								' Set Kolom 2 sebagai TextBox ReadOnly (Tidak Terpakai)
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).ReadOnly = True
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).ReadOnly = True
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).Style.BackColor = Color.LightGray ' Warna Abu (Terkunci)
								Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).Style.BackColor = Color.LightGray ' Warna Abu (Terkunci)
							Else
								CloseConn()
								MessageBox.Show("Terjadi Kesalahan Kategori Jenis Analisa Tidak Terdefinisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

						Next
					Else
						CloseConn()
						MessageBox.Show($"Kode Analisa {SelectedKodeAnalisa} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			Txt_Kode_Analisa.Text = ""
			TXt_Jenis_Analisa.Text = ""

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Btn_Moisture_Insert.Enabled = True
	End Sub

	Private Sub Dgv_Moisture_Content_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Moisture_Content.CellEndEdit
		If Dgv_Moisture_Content.Rows.Count = 0 Then Exit Sub

		If Dgv_Moisture_Content.CurrentRow.Cells(Item_Moisture_Flag_Perhitungan).Value = "Y" Then

			If Not IsNumeric(Dgv_Moisture_Content.CurrentRow.Cells(Item_Moisture_Range_Awal).Value) _
				AndAlso Dgv_Moisture_Content.CurrentRow.Cells(Item_Moisture_Range_Awal).Value IsNot Nothing Then

				Dgv_Moisture_Content.CurrentRow.Cells(Item_Moisture_Range_Awal).Value = ""
				Exit Sub

			End If

			If Not IsNumeric(Dgv_Moisture_Content.CurrentRow.Cells(Item_Moisture_Range_Akhir).Value) _
				AndAlso Dgv_Moisture_Content.CurrentRow.Cells(Item_Moisture_Range_Akhir).Value IsNot Nothing Then

				Dgv_Moisture_Content.CurrentRow.Cells(Item_Moisture_Range_Akhir).Value = ""
				Exit Sub

			End If

		End If
	End Sub

	Private Sub Dgv_Moisture_Content_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Moisture_Content.CellClick
		If Dgv_Moisture_Content.Rows.Count = 0 Then Exit Sub
		If Dgv_Moisture_Content.Rows.Count = 0 Then Exit Sub
		If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

		If Dgv_Moisture_Content.Rows(e.RowIndex).Cells(Item_Moisture_Flag_Perhitungan).Value = "T" Then

			If e.ColumnIndex = Item_Moisture_Combobox Then

				Dgv_Moisture_Content.BeginEdit(True)

				Me.BeginInvoke(New MethodInvoker(Sub()
													 Dim combo As ComboBox = TryCast(Dgv_Moisture_Content.EditingControl, ComboBox)

													 If combo IsNot Nothing Then
														 combo.DroppedDown = True
													 End If

												 End Sub))

			End If

		End If
	End Sub

	Private Sub Dgv_Moisture_Content_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgv_Moisture_Content.KeyDown
		If Dgv_Moisture_Content.Rows.Count = 0 Then Exit Sub

		If e.KeyCode = Keys.Delete Then

			If MessageBox.Show("Apakah Anda Yakin Ingin Menghapus Data Ini ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

			Dim idToDelete As String = Dgv_Moisture_Content.CurrentRow.Cells(Item_Moisture_ID).Value.ToString()

			Dim itemToRemove = arrDataCombobox.FirstOrDefault(Function(x) x.ID_Analisa = idToDelete)
			If itemToRemove IsNot Nothing Then
				arrDataCombobox.Remove(itemToRemove)
			End If

			Dgv_Moisture_Content.Rows.Remove(Dgv_Moisture_Content.CurrentRow)

		End If

	End Sub

	Private Sub Btn_Refresh_Moisture_Click(sender As Object, e As EventArgs) Handles Btn_Refresh_Moisture.Click
		Txt_Kode_Analisa.Text = ""
		TXt_Jenis_Analisa.Text = ""
		Btn_Moisture_Insert.Enabled = True
		Dgv_Moisture_Content.Rows.Clear()
		arrDataCombobox.Clear()
	End Sub

	Private Sub Btn_Show_Formula_Click(sender As Object, e As EventArgs) Handles Btn_Show_Formula.Click

		N_EMI_SD_Transaksi_Reformulasi_Formula_Trial.ShowDialog()
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Cmb_Lokasi_HO.SelectedIndex = -1 Then
			MessageBox.Show("Lokasi HO Harap Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Cmb_Lokasi_Barang.SelectedIndex = -1 Then
			MessageBox.Show("Lokasi Barang Harap Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_Kd_Barang.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Barang Harap Diisi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_Nm_Barang.Text.Trim.Length = 0 Then
			MessageBox.Show("Nama Barang Harap Diisi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Cmb_Penangung_Jawab.SelectedIndex = -1 Then
			MessageBox.Show("Penanggung Jawab Harap Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_Hasil.Text.Trim.Length = 0 Then
			MessageBox.Show("Hasil Harap Diisi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Cmb_Satuan_Hasil.SelectedIndex = -1 Then
			MessageBox.Show("Satuan Hasil Harap Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Dgv_Detail_Bahan.Rows.Count = 0 Then
			MessageBox.Show("Detail Bahan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Dgv_Moisture_Content.Rows.Count = 0 Then
			MessageBox.Show("Moisture Content Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show("Yakin Ingin Melakukan Simpan Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		BS_BahanBaku.DataSource = ListBahanBaku
		GetTotalDetailBarang()

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim NoFormulaAwal As String = "NULL"
			Dim FlagReformulasi As String = "NULL"
			get_no_faktur_binding()
			If JenisForm(Cmb_Jenis_Form.SelectedIndex) = JenisForm_Create Then
				Get_No_Faktur_Create()
				NoFormulaAwal = "NULL"
				FlagReformulasi = "NULL"
			ElseIf JenisForm(Cmb_Jenis_Form.SelectedIndex) = JenisForm_Reformulasi Then
				If SelectedFormula.Trim.Length = 0 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("No Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
				Get_No_Faktur_Reformulasi(SelectedFormula)
				NoFormulaAwal = $"'{SelectedFormula.Trim}'"
				FlagReformulasi = "'Y'"
			End If

			If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Terjadi Kesalahan Saat Load No Faktur, Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End If

			If Val(Txt_Total_Persen.Text) <> 100 Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Total Persentase Bahan Baku Harus 100%", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End If

			'===============================
			'=     GET KODE BARANG INQ     =
			'===============================
			Dim kd_barangINq As String = ""
			SQL = "select top(1) Kode_Barang_inq from barang "
			SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' "
			SQL = SQL & "and Kode_Barang ='" & Txt_Kd_Barang.Text & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					kd_barangINq = dr("Kode_Barang_inq")
				Else
					dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Kode Barang {Txt_Kd_Barang.Text.Trim} tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Exit Sub
				End If
			End Using

			'=========================
			'=     INSERT PARENT     =
			'=========================
			SQL = $"
				INSERT INTO Emi_Transaksi_Formulator
					(Kode_Perusahaan, No_Faktur, No_Formula_Awal, UserID, Tanggal, Jam, Kode_Stock_Owner,
					Kode_Barang, Penanggung_Jawab, Lokasi, Hasil, Satuan_Hasil, Flag_Reformulasi)
				VALUES ('{KodePerusahaan}', '{TxtFormulator_NoFaktur.Text.Trim}', {NoFormulaAwal}, '{UserID}',
				'{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{Cmb_Lokasi_Barang.Text.Trim}',
				'{kd_barangINq.Trim}', '{arrIdPenanggungJawab(Cmb_Penangung_Jawab.SelectedIndex)}',
				'{Cmb_Lokasi_HO.Text.Trim}', '{Val(HilangkanTanda(Txt_Hasil.Text))}', '{Cmb_Satuan_Hasil.Text.Trim}', {FlagReformulasi})
			"
			ExecuteTrans(SQL)

			'=========================
			'=     INSERT DETAIL     =
			'=========================
			Dim NoStep As Integer = 1

#Region "Kode Lama Tanpa Binding"

			'For i As Integer = 0 To Dgv_Detail_Bahan.Rows.Count - 1
			'	Get_Data_DGV_Detail_Bahan(i)
			'	If Val(HilangkanTanda(dgvDetailBahan_Kuantity)) = 0 Then Continue For

			'	'==============================
			'	'=     INSERT DETAIL STEP     =
			'	'==============================
			'	SQL = $"
			'		INSERT INTO EMI_Transaksi_Formulator_Detail_Step
			'		(Kode_Perusahaan, No_Faktur, No_step, Kode, Deskripsi, Jumlah, Satuan, Persentase, Nilai_Pengali, Nilai_Barang, Satuan_barang, Tipe)
			'		VALUES('{KodePerusahaan}', '{TxtFormulator_NoFaktur.Text.Trim}', '{NoStep}', '{dgvDetailBahan_KodeBarang}', '{dgvDetailBahan_NamaBarang}',
			'		'{Val(HilangkanTanda(dgvDetailBahan_Kuantity))}', '{dgvDetailBahan_Satuan}', '{Val(HilangkanTanda(dgvDetailBahan_Persentase))}',
			'		'{Val(HilangkanTanda(dgvDetailBahan_NilaiPengali))}', '{Val(HilangkanTanda(dgvDetailBahan_Kuantity))}', '{dgvDetailBahan_SatuanBarang}', '')
			'	"
			'	ExecuteTrans(SQL)

			'	'===============================
			'	'=     INSERT DETAIL BAHAN     =
			'	'===============================
			'	SQL = $"
			'		INSERT INTO EMI_Transaksi_Formulator_Detail_Bahan
			'		(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah, Persentase, satuan,
			'		Nilai_Pengali, Nilai_Barang, Satuan_barang, Est_HPP, Est_HPP_Per_Pcs)
			'		VALUES('{KodePerusahaan}', '{TxtFormulator_NoFaktur.Text.Trim}', '{Cmb_Lokasi_Barang.Text.Trim}',
			'		'{dgvDetailBahan_KodeBarang.Trim}', '{Val(HilangkanTanda(dgvDetailBahan_Kuantity))}', '{Val(HilangkanTanda(dgvDetailBahan_Persentase))}',
			'		'{dgvDetailBahan_Satuan.Trim}', '{Val(HilangkanTanda(dgvDetailBahan_NilaiPengali))}', '{Val(HilangkanTanda(dgvDetailBahan_Kuantity))}',
			'		'{dgvDetailBahan_Satuan.Trim}', '{Val(HilangkanTanda(dgvDetailBahan_Harga))}', '{Val(HilangkanTanda(dgvDetailBahan_EstHargaPcs))}')
			'	"
			'	ExecuteTrans(SQL)

			'	NoStep += 1
			'Next

#End Region

			For Each item As BahanBaku In ListBahanBaku
				If Val(HilangkanTanda(item.kuantity)) = 0 Then Continue For

				'==============================
				'=     INSERT DETAIL STEP     =
				'==============================
				SQL = $"
					INSERT INTO EMI_Transaksi_Formulator_Detail_Step
					(Kode_Perusahaan, No_Faktur, No_step, Kode, Deskripsi, Jumlah, Satuan, Persentase, Nilai_Pengali, Nilai_Barang, Satuan_barang, Tipe)
					VALUES('{KodePerusahaan}', '{TxtFormulator_NoFaktur.Text.Trim}', '{NoStep}', '{item.kode_barang}', '{item.nama}',
					'{Val(HilangkanTanda(item.kuantity))}', '{item.satuan}', '{Val(HilangkanTanda(item.persentase))}',
					'{Val(HilangkanTanda(item.nilai_pengali))}', '{Val(HilangkanTanda(item.kuantity))}', '{item.satuan}', '')
				"
				ExecuteTrans(SQL)

				'===============================
				'=     INSERT DETAIL BAHAN     =
				'===============================
				SQL = $"
					INSERT INTO EMI_Transaksi_Formulator_Detail_Bahan
					(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah, Persentase, satuan,
					Nilai_Pengali, Nilai_Barang, Satuan_barang, Est_HPP, Est_HPP_Per_Pcs)
					VALUES('{KodePerusahaan}', '{TxtFormulator_NoFaktur.Text.Trim}', '{Cmb_Lokasi_Barang.Text.Trim}',
					'{item.kode_barang.Trim}', '{Val(HilangkanTanda(item.kuantity))}', '{Val(HilangkanTanda(item.persentase))}',
					'{item.satuan.Trim}', '{Val(HilangkanTanda(item.nilai_pengali))}', '{Val(HilangkanTanda(item.kuantity))}',
					'{item.satuan.Trim}', '{Val(HilangkanTanda(item.harga))}', '{Val(HilangkanTanda(item.est_hpp_pcs))}')
				"
				ExecuteTrans(SQL)

				NoStep += 1
			Next

			'===============================
			'=     INSERT DATA BINDING     =
			'===============================
			SQL = $"
				insert into EMI_Transaksi_Formulator_Binding (
				Kode_Perusahaan, No_Faktur, Tanggal, Jam, UserID, Kode_Barang, Kode_Formula, Aktif)
				values('{KodePerusahaan}', '{TxtFormulator_NoFaktur_Binding.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}',
				'{UserID}', '{kd_barangINq}', '{TxtFormulator_NoFaktur.Text.Trim}', 'Y')
			"
			ExecuteTrans(SQL)

			'===================================
			'=     INSERT MOISTURE CONTENT     =
			'===================================
			For i As Integer = 0 To Dgv_Moisture_Content.Rows.Count - 1
				Get_SD_Moisture(i)

				If Lv_Moisture_Flag_Perhitungan.Trim = "Y" Then
					If Lv_Moisture_Range_Awal.Trim.Length = 0 Then
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Harap Pilih Dahulu Value Kode Analisa {Lv_Moisture_Kode_Analisa}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					ElseIf Lv_Moisture_Range_Akhir.Trim.Length = 0 Then
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Harap Pilih Dahulu Value Kode Analisa {Lv_Moisture_Kode_Analisa}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					SQL = "insert into N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang(Kode_Perusahaan, No_Formula, Id_Jenis_Analisa, Kode_Barang, Range_Awal, Range_Akhir, Tanggal, Jam, User_ID, Kode_Role) "
					SQL &= $"values('{KodePerusahaan}', '{TxtFormulator_NoFaktur.Text.Trim}', '{Lv_Moisture_ID}', '{Txt_Kd_Barang.Text.Trim}', "
					SQL &= $"'{HilangkanTanda(Lv_Moisture_Range_Awal.Trim)}', '{HilangkanTanda(Lv_Moisture_Range_Akhir.Trim)}', '{Format(tgl_skg, "yyyy-MM-dd")}', "
					SQL &= $"'{Format(tgl_skg, "HH:mm:ss")}', '{UserID}', 'FLM') "
					ExecuteTrans(SQL)

				ElseIf Lv_Moisture_Flag_Perhitungan.Trim = "T" Then

					Dim cell = TryCast(Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Combobox), DataGridViewComboBoxCell)
					Dim selectedIndex As Integer = -1
					Dim NilaiKriteria As String = ""
					Dim KeteranganKriteria As String = ""

					If cell?.Value IsNot Nothing Then
						selectedIndex = cell.Items.IndexOf(cell.Value)

						Dim itemAnalisa = arrDataCombobox.FirstOrDefault(Function(x) x.ID_Analisa = Lv_Moisture_ID)

						If itemAnalisa IsNot Nothing AndAlso itemAnalisa.Datas.Count > selectedIndex Then
							NilaiKriteria = itemAnalisa.Datas(selectedIndex).Value
							KeteranganKriteria = itemAnalisa.Datas(selectedIndex).Label
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show($"Nilai Kriteria untuk kode analisa {Lv_Moisture_Kode_Analisa} tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Harap Pilih Dahulu Value Kode Analisa {Lv_Moisture_Kode_Analisa}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					SQL = "insert into N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan(Kode_Perusahaan, No_Formula, Id_Jenis_Analisa, Kode_Barang, "
					SQL &= $"Nilai_Kriteria, Keterangan_Kriteria, Flag_Aktif, Tanggal, Jam, User_ID, Kode_Role) "
					SQL &= $"values('{KodePerusahaan}', '{TxtFormulator_NoFaktur.Text.Trim}', '{Lv_Moisture_ID}', '{Txt_Kd_Barang.Text.Trim}', "
					SQL &= $"'{NilaiKriteria}', '{KeteranganKriteria}', 'Y', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{UserID}', 'FLM') "
					ExecuteTrans(SQL)
				Else
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Kode Analisa {Lv_Moisture_Kode_Analisa} kategori perhitungan ambigu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

			Next

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Formula Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		kosong()

	End Sub

	Public Sub LoadDataDetailBahanByFormula(ByVal NoFormula As String)

		SelectedFormula = ""

		Try
			OpenConn()

			Get_No_Faktur_Reformulasi(NoFormula)

			Cmb_Penangung_Jawab.Items.Clear() : arrIdPenanggungJawab.Clear()
			SQL = $"
				select Nama,Id_Karyawan
				from Emi_Karyawan a, Emi_Jabatan_Internal b
				where a.id_jabatan=b.id_jabatan
				and b.flag_Tampil_Formulator='Y'
				and a.kode_perusahaan = '{KodePerusahaan}'
				order by nama
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Penangung_Jawab.Items.Add(Dr("Nama")) : arrIdPenanggungJawab.Add(Dr("Id_Karyawan"))
				Loop
			End Using

			Cmb_Penangung_Jawab.SelectedIndex = -1

			Cmb_Satuan_Hasil.Items.Clear()
			SQL = $"
				select Satuan
				from EMI_Satuan
				where Flag_Tampil_Berat='Y'
				and kode_perusahaan = '{KodePerusahaan}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Satuan_Hasil.Items.Add(Dr("Satuan"))
				Loop
			End Using
			Cmb_Satuan_Hasil.SelectedIndex = -1

			'============================
			'=     LOAD DATA PARENT     =
			'============================
			Dim Selected_Lokasi As String = ""
			Dim Selected_KdBarang As String = ""
			Dim Selected_NmBarang As String = ""
			Dim Selected_IdPenanggungJawab As Integer = 0
			Dim Selected_KaryawanPenanggungJawab As String = ""
			Dim Selected_Hasil As Double = 0
			Dim Selected_SatuanHasil As String = ""
			SQL = $"
				select a.Status, a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, b.Nama, a.Penanggung_Jawab, c.Nama as NamaKaryawan, a.Hasil, a.Satuan_Hasil
				from Emi_Transaksi_Formulator a
					inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq
					inner join Emi_Karyawan c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Penanggung_Jawab = c.Id_Karyawan
				where  a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{NoFormula.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan No Formula {NoFormula.Trim} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					Selected_Lokasi = Dr("Kode_Stock_Owner").ToString.Trim
					Selected_KdBarang = Dr("Kode_Barang").ToString.Trim
					Selected_NmBarang = Dr("Nama").ToString.Trim
					Selected_IdPenanggungJawab = Dr("Penanggung_Jawab").ToString.Trim
					Selected_KaryawanPenanggungJawab = Dr("NamaKaryawan").ToString.Trim
					Selected_Hasil = Dr("Hasil")
					Selected_SatuanHasil = Dr("Satuan_Hasil").ToString.Trim

					SelectedFormula = NoFormula.Trim
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan No Formula {NoFormula.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Cmb_Lokasi_Barang.SelectedItem = Selected_Lokasi
			switchAutoComplete = True
			Txt_Kd_Barang.Text = Selected_KdBarang
			Txt_Nm_Barang.Text = Selected_KdBarang
			switchAutoComplete = False
			Cmb_Penangung_Jawab.SelectedItem = Selected_KaryawanPenanggungJawab.Trim
			Txt_Hasil.Text = Format(Selected_Hasil, "N4")
			Cmb_Satuan_Hasil.SelectedItem = Selected_SatuanHasil

			'================================
			'=     Hitung Berat Per PCS     =
			'================================
			SQL = $"
				select Berat from barang
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Stock_Owner = '{Selected_Lokasi.Trim}'
				and Kode_Barang = '{Selected_KdBarang.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Berat_PCS_Barang_Jadi = Val(HilangkanTanda(Dr("Berat")))
				End If
			End Using

			'=============================
			'=     LOAD DETAIL BAHAN     =
			'=============================
			Dgv_Detail_Bahan.Rows.Clear()
			SQL = $"
				select a.Kode_Barang, a.Nama, a.Id_Klasifikasi_Bahan3, b.Keterangan as Klasifikasi_Bahan3, a.Good_Stock,
					isnull((select x.Satuan from barang_Detail_Satuan x where x.Kode_Barang= a.Kode_Barang and x.Flag_Tampil_Display = 'Y'), NULL) as Satuan,
					case when exists(
						select 1 from Barang_SN z where a.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0
					) then (
						select top 1 dbo.get_hpp(z.serial_number) from Barang_SN z where a.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0
						order by z.tgl_masuk DESC)
					else a.estimasi_harga
					end Est_HPP,
					isnull((
						select z.Nilai_Pengali from emi_satuan_detail_perhitungan z
						where z.Kode_Perusahaan=a.Kode_Perusahaan
							and z.Satuan_Awal= (select x.Satuan from barang_Detail_Satuan x where x.Kode_Barang= a.Kode_Barang and x.Flag_Tampil_Display = 'Y')
							and z.satuan_akhir= a.Satuan
							and z.Jenis = 'MASA'
					), 0) as Nilai_Pengali,
					a.Satuan as Satuan_Barang,
					isnull((
						select x.jumlah
						from Emi_Transaksi_Formulator z
							inner join EMI_Transaksi_Formulator_Detail_Bahan x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
						where z.Kode_Perusahaan = a.kode_perusahaan
						and z.status is null
						and z.No_Faktur = '{NoFormula.Trim}'
						and x.Kode_Barang = a.kode_barang
					), 0) as Jumlah_By_Formula
				from barang a
					inner join N_EMI_Master_Klasifikasi_Bahan_3 b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Klasifikasi_Bahan3 = b.Id_Klasifikasi_Bahan3
					inner join barang_Detail_Satuan c on a.Kode_Barang= c.Kode_Barang and c.Flag_Tampil_Display = 'Y'
					inner join EMI_Group_Jenis d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.Id_Group_Jenis = d.Id_Group_Jenis and d.flag_finished_good <> 'Y'
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Kode_Stock_Owner = '{Cmb_Lokasi_Barang.Text.Trim}'
				and a.Satuan is not null
				order by
					case when isnull((
						select x.jumlah
						from Emi_Transaksi_Formulator z
							inner join EMI_Transaksi_Formulator_Detail_Bahan x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
						where z.Kode_Perusahaan = a.kode_perusahaan
						and z.status is null
						and z.No_Faktur = '{NoFormula.Trim}'
						and x.Kode_Barang = a.kode_barang
						), 0) <> 0 then 0 else 1 end,
				a.Id_Klasifikasi_Bahan3
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

#Region "Kode Lama Tanpa Binding Source"

							'Dgv_Detail_Bahan.Rows.Add()

							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_KlasifikasiBahan3).Value = .Rows(i).Item("Klasifikasi_Bahan3")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_IDKlasifikasiBahan3).Value = .Rows(i).Item("Id_Klasifikasi_Bahan3")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_KodeBarang).Value = .Rows(i).Item("Kode_Barang")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_NamaBarang).Value = .Rows(i).Item("Nama")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Satuan).Value = .Rows(i).Item("Satuan")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_NilaiPengali).Value = .Rows(i).Item("Nilai_Pengali")
							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_SatuanBarang).Value = .Rows(i).Item("Satuan_Barang")

							'If General_Class.CekNULL(.Rows(i).Item("Est_HPP")) = "" Then
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Harga).Value = Format(0, "N2")
							'Else
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Harga).Value = Format(.Rows(i).Item("Est_HPP"), "N2")
							'End If

							'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_SatuanHasil).Value = ""

							'If .Rows(i).Item("Jumlah_By_Formula") <> 0 Then

							'	'============================
							'	'=     GET SATUAN HASIL     =
							'	'============================
							'	Dim nilai_satuan_hasil As Double = 0
							'	SQL = $"select dbo.Ubah_Satuan('{KodePerusahaan}', 'MASA', '{ .Rows(i).Item("Kode_Barang")}',
							'		'{ .Rows(i).Item("Satuan")}', '{Cmb_Satuan_Hasil.Text.Trim}', '{Val(HilangkanTanda(.Rows(i).Item("Jumlah_By_Formula")))}') as Hasil
							'	"
							'	Using Dr = OpenTrans(SQL)
							'		If Dr.Read Then
							'			nilai_satuan_hasil = Dr("Hasil")
							'		End If
							'	End Using

							'	Dim Persentase As Double = (nilai_satuan_hasil * 100) / Val(Txt_Hasil.Text)

							'	'==========================
							'	'=     GET HPP PERPCS     =
							'	'==========================
							'	Dim HppPerPcs As Double = 0
							'	SQL = $"
							'		select a.Kode_Barang, a.Nama,  a.estimasi_harga,
							'			CASE WHEN EXISTS (
							'				SELECT 1 FROM Barang_SN z WHERE a.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0
							'			) THEN ISNULL(
							'				dbo.ubah_satuan(a.kode_perusahaan, 'masa', a.kode_barang, 'gram', a.satuan, ({Berat_PCS_Barang_Jadi} * (cast({Persentase} as float) / 100)))
							'				* (SELECT TOP 1 dbo.get_hpp(z.serial_number)
							'				FROM Barang_SN z
							'				WHERE A.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 ORDER BY z.tgl_masuk DESC), 0)
							'			ELSE
							'				ISNULL(dbo.ubah_satuan(a.kode_perusahaan, 'masa', a.kode_barang, 'gram', a.satuan, ({Berat_PCS_Barang_Jadi} * (cast({Persentase} as float) / 100))) * a.estimasi_harga, 0)
							'			END AS Est_HPP_Pcs
							'		from Barang a
							'		where a.Kode_Perusahaan = '{KodePerusahaan}'
							'		and a.Kode_Stock_Owner = '{Cmb_Lokasi_Barang.Text.Trim}'
							'		and a.Kode_Barang = '{ .Rows(i).Item("Kode_Barang")}'
							'	"
							'	Using Dr = OpenTrans(SQL)
							'		If Dr.Read Then
							'			HppPerPcs = Val(HilangkanTanda(Dr("Est_HPP_Pcs")))
							'		End If
							'	End Using

							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Persentase).Value = Format(Persentase, "N2")
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah_By_Formula"))), "N4")
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_EstHargaPcs).Value = Format(HppPerPcs, "N2")
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_NilaiSatuanHasil).Value = Format(nilai_satuan_hasil, "N2")

							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Style.BackColor = Color.LightGreen
							'Else
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Persentase).Value = Format(0, "N2")
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Value = Format(0, "N4")
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_EstHargaPcs).Value = Format(0, "N2")
							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_NilaiSatuanHasil).Value = Format(0, "N2")

							'	Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Style.BackColor = Color.White
							'End If

#End Region

							Dim Harga As Double = If(General_Class.CekNULL(.Rows(i).Item("Est_HPP")) = "", 0, .Rows(i).Item("Est_HPP"))
							Dim nilai_satuan_hasil As Double = 0
							Dim Persentase As Double = 0
							Dim HppPerPcs As Double = 0
							Dim Kuantity As Double = 0

							If .Rows(i).Item("Jumlah_By_Formula") <> 0 Then

								'============================
								'=     GET SATUAN HASIL     =
								'============================
								SQL = $"select dbo.Ubah_Satuan('{KodePerusahaan}', 'MASA', '{ .Rows(i).Item("Kode_Barang")}',
									'{ .Rows(i).Item("Satuan")}', '{Cmb_Satuan_Hasil.Text.Trim}', '{Val(HilangkanTanda(.Rows(i).Item("Jumlah_By_Formula")))}') as Hasil
								"
								Using Dr = OpenTrans(SQL)
									If Dr.Read Then
										nilai_satuan_hasil = Dr("Hasil")
									End If
								End Using

								Persentase = (nilai_satuan_hasil * 100) / Val(Txt_Hasil.Text)

								'==========================
								'=     GET HPP PERPCS     =
								'==========================
								SQL = $"
									select a.Kode_Barang, a.Nama,  a.estimasi_harga,
										CASE WHEN EXISTS (
											SELECT 1 FROM Barang_SN z WHERE a.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0
										) THEN ISNULL(
											dbo.ubah_satuan(a.kode_perusahaan, 'masa', a.kode_barang, 'gram', a.satuan, ({Berat_PCS_Barang_Jadi} * (cast({Persentase} as float) / 100)))
											* (SELECT TOP 1 dbo.get_hpp(z.serial_number)
											FROM Barang_SN z
											WHERE A.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 ORDER BY z.tgl_masuk DESC), 0)
										ELSE
											ISNULL(dbo.ubah_satuan(a.kode_perusahaan, 'masa', a.kode_barang, 'gram', a.satuan, ({Berat_PCS_Barang_Jadi} * (cast({Persentase} as float) / 100))) * a.estimasi_harga, 0)
										END AS Est_HPP_Pcs
									from Barang a
									where a.Kode_Perusahaan = '{KodePerusahaan}'
									and a.Kode_Stock_Owner = '{Cmb_Lokasi_Barang.Text.Trim}'
									and a.Kode_Barang = '{ .Rows(i).Item("Kode_Barang")}'
								"
								Using Dr = OpenTrans(SQL)
									If Dr.Read Then
										HppPerPcs = Val(HilangkanTanda(Dr("Est_HPP_Pcs")))
									End If
								End Using

								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Persentase).Value = Format(Persentase, "N2")
								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah_By_Formula"))), "N4")
								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_EstHargaPcs).Value = Format(HppPerPcs, "N2")
								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_NilaiSatuanHasil).Value = Format(nilai_satuan_hasil, "N2")

								Kuantity = Val(HilangkanTanda(.Rows(i).Item("Jumlah_By_Formula")))

								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Style.BackColor = Color.LightGreen
							Else
								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Persentase).Value = Format(0, "N2")
								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Value = Format(0, "N4")
								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_EstHargaPcs).Value = Format(0, "N2")
								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_NilaiSatuanHasil).Value = Format(0, "N2")

								'Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Style.BackColor = Color.White

								Kuantity = 0
							End If

							ListBahanBaku.Add(
								New BahanBaku With {
									.klasifikasi_bahan_3 = Ds.Tables("MyTable").Rows(i).Item("Klasifikasi_Bahan3"),
									.id_klasifikasi_bahan3 = Ds.Tables("MyTable").Rows(i).Item("Id_Klasifikasi_Bahan3"),
									.kode_barang = Ds.Tables("MyTable").Rows(i).Item("Kode_Barang"),
									.nama = Ds.Tables("MyTable").Rows(i).Item("Nama"),
									.kuantity = Format(Val(HilangkanTanda(Kuantity)), "N4"),
									.satuan = Ds.Tables("MyTable").Rows(i).Item("Satuan"),
									.nilai_pengali = Ds.Tables("MyTable").Rows(i).Item("Nilai_Pengali"),
									.satuan_barang = Ds.Tables("MyTable").Rows(i).Item("Satuan_Barang"),
									.persentase = Format(Persentase, "N2"),
									.harga = Format(Harga, "N2"),
									.est_hpp_pcs = Format(HppPerPcs, "N2"),
									.satuan_hasil = Cmb_Satuan_Hasil.Text.Trim,
									.nilai_satuan_hasil = Format(nilai_satuan_hasil, "N2")
								}
							)

						Next
					End If
				End With
			End Using

			BS_BahanBaku.DataSource = Nothing
			BS_BahanBaku.DataSource = ListBahanBaku
			Dgv_Detail_Bahan.DataSource = BS_BahanBaku

			'=====================================
			'=     GET DATA MOISTURE CONTENT     =
			'=====================================
			Txt_Kode_Analisa.Text = ""
			TXt_Jenis_Analisa.Text = ""
			Dgv_Moisture_Content.Rows.Clear()
			SQL = $"
				select b.id, b.Kode_Analisa, '-' as Value_Combobox, a.Range_Awal, a.Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{NoFormula.Trim}'
				union all
				select b.id, b.Kode_Analisa, c.label_keterangan as Value_Combobox, '' as Range_Awal, '' as Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				inner join EMI_Switch c on a.Kode_Perusahaan = c.kode_perusahaan and a.nilai_kriteria = c.keterangan
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{NoFormula.Trim}'
			"
			Using Ds999 = BindingTrans(SQL)
				If Ds999.Tables("MyTable").Rows.Count <> 0 Then
					For z As Integer = 0 To Ds999.Tables("MyTable").Rows.Count - 1

						Dim KodeAnalisa As String = Ds999.Tables("MyTable").Rows(z).Item("Kode_Analisa")
						Dim ValueCombobox As String = Ds999.Tables("MyTable").Rows(z).Item("Value_Combobox")
						Dim RangeAwal As String = Ds999.Tables("MyTable").Rows(z).Item("Range_Awal")
						Dim RangeAkhir As String = Ds999.Tables("MyTable").Rows(z).Item("Range_Akhir")

						LoadMoistureContent(KodeAnalisa, ValueCombobox, RangeAwal, RangeAkhir)

					Next
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		GetTotalDetailBarang()
		Cmb_Filter.Enabled = True
	End Sub

	Private Sub Txt_Filter_TextChanged(sender As Object, e As EventArgs) Handles Txt_Filter.TextChanged
		If Txt_Filter.Text.Trim.Length = 0 Then
			Handle_Search_Detail_Bahan("")
			Exit Sub
		End If
		TypingTimer.Stop()
		TypingTimer.Start()

	End Sub

	Private Sub TypingTimer_Tick(sender As Object, e As EventArgs) Handles TypingTimer.Tick
		TypingTimer.Stop()

		Dim keyword As String = Txt_Filter.Text.Trim()

		'==============================================
		'=     FUNGSI RELOAD TAMPILKAN DATA ULANG     =
		'==============================================
		Handle_Search_Detail_Bahan(keyword)

	End Sub

	Private Sub Txt_Hasil_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Hasil.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Satuan_Hasil.DroppedDown = True
			Cmb_Satuan_Hasil.Focus()
		End If
	End Sub

	'================================================================================================================================================================
	'=     UTILITY
	'================================================================================================================================================================

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub GetTotalDetailBarang()
		If Dgv_Detail_Bahan.Rows.Count = 0 Then
			Txt_Total_Persen.Text = Format(0, "N2")
			Txt_Total_Hpp_Pcs.Text = Format(0, "N2")
			Exit Sub
		End If

		Dim TotalPersen As Double = 0
		Dim HPPPcs As Double = 0
		For i As Integer = 0 To Dgv_Detail_Bahan.Rows.Count - 1

			If Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Kuantity).Value = 0 Then Continue For

			TotalPersen += Val(HilangkanTanda(Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_Persentase).Value))
			HPPPcs += Val(HilangkanTanda(Dgv_Detail_Bahan.Rows(i).Cells(cellDetailBahan_EstHargaPcs).Value))
		Next

		Txt_Total_Persen.Text = Format(TotalPersen, "N2")
		Txt_Total_Hpp_Pcs.Text = Format(HPPPcs, "N2")
	End Sub

	Private Class DataCombobox
		Public Property ID_Analisa As String
		Public Property Kode_Analisa As String
		Public Property Datas As New List(Of (ID_Switch As String, Value As String, Label As String))
	End Class

	Private Sub LoadMoistureContent(ByVal KodeAnalisa As String, ByVal ValueCombo As String, ByVal RangeAwal As String, ByVal RangeAkhir As String)
		SQL = "select id, Kode_Analisa, Jenis_Analisa, Flag_Perhitungan, Kode_Aktivitas_Lab "
		SQL &= $"from N_EMI_LAB_Jenis_Analisa "
		SQL &= $"where Kode_Analisa = '{KodeAnalisa}' "
		SQL &= $"order by Jenis_Analisa "
		Using Ds = BindingTrans(SQL)
			With Ds.Tables("MyTable")
				If .Rows.Count <> 0 Then
					For i As Integer = 0 To .Rows.Count - 1

						Dim n As Integer = Dgv_Moisture_Content.Rows.Add()

						Dim Id_Jenis_Analisa As String = .Rows(i).Item("Id")
						Dim Kode_Analisa As String = .Rows(i).Item("Kode_Analisa")
						Dim Jenis_Analisa As String = .Rows(i).Item("Jenis_Analisa")
						Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(.Rows(i).Item("Flag_Perhitungan")) = "", "T", .Rows(i).Item("Flag_Perhitungan"))
						Dim Kode_Aktivitas_Analisa As String = .Rows(i).Item("Kode_Aktivitas_Lab")

						Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_ID).Value = Id_Jenis_Analisa
						Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Kode_Analisa).Value = Kode_Analisa
						Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Jenis_Analisa).Value = Jenis_Analisa
						Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Flag_Perhitungan).Value = Flag_Perhitungan_Analisa
						Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Kode_Aktivitas).Value = Kode_Aktivitas_Analisa
						Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Kode_Kategori).Value = If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan")

						If Flag_Perhitungan_Analisa = "Y" Then

							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).Style.BackColor = Color.LightYellow
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).Style.BackColor = Color.LightYellow
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).ReadOnly = False
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).ReadOnly = False
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Combobox).ReadOnly = True
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Combobox).Style.BackColor = Color.LightGray ' Warna Abu (Terkunci)

							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).Value = RangeAwal
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).Value = RangeAkhir

						ElseIf Flag_Perhitungan_Analisa = "T" Then

							Dim arrKriteria As New List(Of (id_switch As String, value As String, Label As String))
							SQL = "select distinct a.id as Id_Jenis_Analisa, a.Kode_Analisa, a.Jenis_Analisa, c.Id_QC_Formula, d.Id_Switch, d.Keterangan, d.Label_Keterangan "
							SQL &= $"from N_EMI_LAB_Jenis_Analisa a "
							SQL &= $"inner join N_EMI_LAB_Binding_Jenis_Analisa b on a.id = b.Id_Jenis_Analisa "
							SQL &= $"inner join EMI_Quality_Control c on b.Id_Quality_Control = c.Id_QC_Formula "
							SQL &= $"inner join EMI_Switch d on c.Id_QC_Formula = d.Id_QC_Formula "
							SQL &= $"where c.Kode_Perusahaan = '{KodePerusahaan}' "
							SQL &= $"and a.id = '{Id_Jenis_Analisa}' "
							Using Dr = OpenTrans(SQL)
								Do While Dr.Read
									arrKriteria.Add((Dr("Id_Switch"), Dr("Keterangan"), Dr("Label_Keterangan")))
								Loop
							End Using

							Dim dataCombo As New DataCombobox With {
								.ID_Analisa = Id_Jenis_Analisa,
								.Kode_Analisa = Kode_Analisa,
								.Datas = arrKriteria
							}
							arrDataCombobox.Add(dataCombo)

							' Set sebagai ComboBox
							Dim CellCmb As New DataGridViewComboBoxCell()
							CellCmb.Items.AddRange(arrKriteria.Select(Function(x) x.Label).ToArray())
							CellCmb.Style.BackColor = Color.LightYellow ' Warna Kuning (Wajib Pilih)
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Combobox) = CellCmb

							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Combobox).ReadOnly = False

							' Set Kolom 2 sebagai TextBox ReadOnly (Tidak Terpakai)
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).ReadOnly = True
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).ReadOnly = True
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).Style.BackColor = Color.LightGray ' Warna Abu (Terkunci)
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).Style.BackColor = Color.LightGray ' Warna Abu (Terkunci)

							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Awal).Value = ""
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Range_Akhir).Value = ""
							Dgv_Moisture_Content.Rows(n).Cells(Item_Moisture_Combobox).Value = ValueCombo
						Else
							CloseConn()
							MessageBox.Show("Terjadi Kesalahan Kategori Jenis Analisa Tidak Terdefinisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If

					Next
				Else
					CloseConn()
					MessageBox.Show($"Kode Analisa {KodeAnalisa} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End With
		End Using
	End Sub

	Private Sub Dgv_Detail_Bahan_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_Detail_Bahan.CellFormatting
		If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return

		Dim row As DataGridViewRow = Dgv_Detail_Bahan.Rows(e.RowIndex)

		If Dgv_Detail_Bahan.Columns(e.ColumnIndex).Index = cellDetailBahan_Kuantity Then
			If Not IsNumeric(e.Value) Then
				e.CellStyle.BackColor = Color.White
				e.Value = $"{Format(0, "N4")} {Dgv_Detail_Bahan.CurrentRow.Cells(cellDetailBahan_Satuan).Value}"
				Return
			End If

			If e.Value IsNot Nothing AndAlso CDbl(e.Value) = 0 Then
				e.CellStyle.BackColor = Color.White
			Else
				e.CellStyle.BackColor = Color.LightGreen
			End If

			Dim satuan As String = row.Cells(cellDetailBahan_Satuan).Value?.ToString()
			e.Value = $"{Format(Val(HilangkanTanda(e.Value)), "N4")} {satuan}"

		ElseIf Dgv_Detail_Bahan.Columns(e.ColumnIndex).Index = cellDetailBahan_Persentase Then
			e.Value = $"{e.Value} %"
		End If
	End Sub

	Private Sub Handle_Search_Detail_Bahan(ByVal keyword As String)

		If String.IsNullOrEmpty(keyword) Then
			BS_BahanBaku.DataSource = ListBahanBaku
		Else

			Dim hasilFilter = ListBahanBaku.Where(Function(b)
													  Select Case Cmb_Filter.SelectedIndex
														  Case 0 ' Klasifikasi Bahan 3
															  Return b.klasifikasi_bahan_3.ToLower().Contains(keyword)
														  Case 1 ' Kode Barang
															  Return b.kode_barang.ToLower().Contains(keyword)
														  Case 2 ' Nama Barang
															  Return b.nama.ToLower().Contains(keyword)
													  End Select
												  End Function).ToList()

			BS_BahanBaku.DataSource = hasilFilter
		End If

		' 5. Paksa Grid untuk menggambar ulang tampilan dengan data terbaru
		BS_BahanBaku.ResetBindings(False)
		GetTotalDetailBarang()
	End Sub

	Private Class BahanBaku
		Public Property klasifikasi_bahan_3 As String
		Public Property id_klasifikasi_bahan3 As String
		Public Property kode_barang As String
		Public Property nama As String
		Public Property kuantity As String
		Public Property satuan As String
		Public Property nilai_pengali As String
		Public Property satuan_barang As String
		Public Property persentase As String
		Public Property harga As String
		Public Property est_hpp_pcs As String
		Public Property satuan_hasil As String
		Public Property nilai_satuan_hasil As String
	End Class

End Class
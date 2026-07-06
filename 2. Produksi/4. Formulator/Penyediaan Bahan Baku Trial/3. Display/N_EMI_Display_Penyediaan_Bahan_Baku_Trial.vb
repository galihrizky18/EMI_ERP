Public Class N_EMI_Display_Penyediaan_Bahan_Baku_Trial

	Dim ArrTanggal, ArrParamLainLama As New ArrayList

	Dim ArrID As New List(Of List(Of Integer))

	Dim Dgv_Parent_NoSplit, Dgv_Parent_Status, Dgv_Parent_NoFormula, Dgv_Parent_Taggal, Dgv_Parent_Jam, Dgv_Parent_KdBarang, Dgv_Parent_NmBarang, Dgv_Parent_KeteranganSplit, Dgv_Parent_JumlahBatch, Dgv_Parent_BatchInput As String

	Dim Cell_Parent_NoSplit As Integer = 0
	Dim Cell_Parent_Status As Integer = 1
	Dim Cell_Parent_NoFormula As Integer = 2
	Dim Cell_Parent_Tanggal As Integer = 3
	Dim Cell_Parent_Jam As Integer = 4
	Dim Cell_Parent_KdBarang As Integer = 5
	Dim Cell_Parent_NmBarang As Integer = 6
	Dim Cell_Parent_KeteranganSplit As Integer = 7
	Dim Cell_Parent_JumlahBatch As Integer = 8
	Dim Cell_Parent_BatchInput As Integer = 9

	Dim Dgv_Bahan_KodeUnik, Dgv_Bahan_Gudang, Dgv_Bahan_KdBahan, Dgv_Bahan_NmBahan, Dgv_Bahan_Jumlah, Dgv_Bahan_Satuan As String

	Dim Cell_Bahan_KodeUnik As Integer = 0
	Dim Cell_Bahan_Gudang As Integer = 1
	Dim Cell_Bahan_KdBahan As Integer = 2
	Dim Cell_Bahan_NmBahan As Integer = 3
	Dim Cell_Bahan_Jumlah As Integer = 4
	Dim Cell_Bahan_Satuan As Integer = 5

	Private Sub N_EMI_Display_Penyediaan_Bahan_Baku_Trial_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBufferDGV(Dgv_Formula_Parent)

		Cmb_ParamTgl.Items.Clear() : ArrTanggal.Clear()
		Cmb_ParamTgl.Items.Add("Tanggal Transaksi") : ArrTanggal.Add("a.Tanggal")

		Cmb_ParamLain.Items.Clear() : ArrParamLainLama.Clear()
		Cmb_ParamLain.Items.Add("No Split") : ArrParamLainLama.Add("a.No_Split")
		Cmb_ParamLain.Items.Add("No Formula") : ArrParamLainLama.Add("a.No_Formula")
		Cmb_ParamLain.Items.Add("Kode Barang") : ArrParamLainLama.Add("b.Kode_Barang")
		Cmb_ParamLain.Items.Add("Nama Barang") : ArrParamLainLama.Add("c.Nama")

		Kosong()
	End Sub

	Private Sub Kosong()

		Dgv_Formula_Parent.Rows.Clear()

		Cmb_ParamLain.SelectedIndex = -1
		Txt_ParamValue.Text = ""

		ArrID.Clear()

		Chk_TransaksiHrIni.Checked = True

	End Sub

	Private Sub GetDataParent(ByVal index As Integer)
		Dgv_Parent_NoSplit = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_NoSplit).Value
		Dgv_Parent_Status = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_Status).Value
		Dgv_Parent_NoFormula = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_NoFormula).Value
		Dgv_Parent_Taggal = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_Tanggal).Value
		Dgv_Parent_Jam = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_Jam).Value
		Dgv_Parent_KdBarang = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_KdBarang).Value
		Dgv_Parent_NmBarang = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_NmBarang).Value
		Dgv_Parent_KeteranganSplit = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_KeteranganSplit).Value
		Dgv_Parent_JumlahBatch = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_JumlahBatch).Value
		Dgv_Parent_BatchInput = Dgv_Formula_Parent.Rows(index).Cells(Cell_Parent_BatchInput).Value
	End Sub

	Private Sub LoadDataParent()
		Try
			OpenConn()

			Dim Filter As String = " "
			If Cmb_Lokasi.SelectedIndex > 0 Then
				Filter &= "AND b.lokasi = '" & Cmb_Lokasi.Text & "' "
			End If

			If Chk_TransaksiHrIni.Checked Then
				Filter &= "AND a.Tanggal between '" & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now.AddDays(1), "yyyy-MM-dd") & "' "
			End If

			If Chk_ParamTgl.Checked Then
				Filter &= "AND " & ArrTanggal(Cmb_ParamTgl.SelectedIndex) & " between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value.AddDays(1), "yyyy-MM-dd") & "' "
			End If

			If Chk_ParamLain.Checked Then
				Filter &= "AND " & ArrParamLainLama(Cmb_ParamLain.SelectedIndex) & " like '%" & Txt_ParamValue.Text & "%' "
			End If

			Dgv_Formula_Parent.Rows.Clear() : Dgv_Batch.Rows.Clear() : Dgv_Detail_Bahan.Rows.Clear() : ArrID.Clear()
			SQL = $"
				select a.Status, a.No_Split, a.No_Formula, a.Tanggal, a.Jam, b.Tanggal as Tanggal_Split, b.Jam As Jam_Split, b.No_Batch as Keterangan_Split, b.Jumlah_Batch, count(distinct a.Batch) as Batch_Input,
				b.Kode_Barang, c.Nama as Nama_Barang
				from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku a
					inner join N_EMI_Transaksi_Trial_Split_Production_Order b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Split = b.No_Transaksi and b.Status is null
					inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				group by a.Status, a.No_Split, a.No_Formula, a.Tanggal, a.Jam, b.Tanggal, b.Jam, b.No_Batch, b.Jumlah_Batch, b.Kode_Barang, c.Nama
				order by a.Tanggal, a.Jam
			"
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then
					For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1

						Dim noSplit As String = Ds.Tables("MyTable").Rows(i).Item("No_Split").ToString()
						Dim Row As Integer = i

						With Dgv_Formula_Parent.Rows(Dgv_Formula_Parent.Rows.Add())
							.Cells(Cell_Parent_NoSplit).Value = Ds.Tables("MyTable").Rows(i).Item("No_Split")
							.Cells(Cell_Parent_Status).Value = If(General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Status")) = "", "Valid", "Dibatalkan")
							.Cells(Cell_Parent_NoFormula).Value = Ds.Tables("MyTable").Rows(i).Item("No_Formula")
							.Cells(Cell_Parent_Tanggal).Value = Format(Ds.Tables("MyTable").Rows(i).Item("Tanggal"), "dd MMM yyyy")
							.Cells(Cell_Parent_Jam).Value = Ds.Tables("MyTable").Rows(i).Item("Jam")
							.Cells(Cell_Parent_KdBarang).Value = Ds.Tables("MyTable").Rows(i).Item("Kode_Barang")
							.Cells(Cell_Parent_NmBarang).Value = Ds.Tables("MyTable").Rows(i).Item("Nama_Barang")
							.Cells(Cell_Parent_KeteranganSplit).Value = Ds.Tables("MyTable").Rows(i).Item("Keterangan_Split")
							.Cells(Cell_Parent_JumlahBatch).Value = Ds.Tables("MyTable").Rows(i).Item("Jumlah_Batch")
							.Cells(Cell_Parent_BatchInput).Value = Ds.Tables("MyTable").Rows(i).Item("Batch_Input")
							.DefaultCellStyle.BackColor = If(General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Status")) = "", Color.White, Color.DarkRed)
							.DefaultCellStyle.ForeColor = If(General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Status")) = "", Color.Black, Color.White)
						End With

						Dim arrTemp As New List(Of Integer)
						SQL = $"
							select ID, Status, Tanggal, Jam from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku
							where Kode_Perusahaan = '{KodePerusahaan}'
							and No_Split = '{Ds.Tables("MyTable").Rows(i).Item("No_Split")}'
							and Tanggal = '{Ds.Tables("MyTable").Rows(i).Item("Tanggal")}'
							and jam = '{Ds.Tables("MyTable").Rows(i).Item("Jam")}'
							order by Status DESC
						"
						Using Dr = OpenTrans(SQL)
							Do While Dr.Read
								arrTemp.Add(Dr("ID"))
							Loop
						End Using

						ArrID.Add(arrTemp)

					Next
				End If

			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Dgv_Formula_Parent_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Formula_Parent.CellClick
		If Dgv_Formula_Parent.Rows.Count = 0 Or Dgv_Formula_Parent.CurrentRow Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim SelectedSplit As String = Dgv_Formula_Parent.CurrentRow.Cells(Cell_Parent_NoSplit).Value

			Dim targetList As List(Of Integer) = ArrID(Dgv_Formula_Parent.CurrentRow.Index)
			Dim ListOfID As String = ""
			If targetList IsNot Nothing Then
				ListOfID = String.Join(",", targetList.Select(Function(x) $"'{x}'"))
			End If

			Dgv_Batch.Rows.Clear() : Dgv_Detail_Bahan.Rows.Clear()
			SQL = $"
				select distinct a.Batch
				from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku a
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Split = '{SelectedSplit}'
				and a.ID in ({ListOfID})
				order by a.Batch
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					With Dgv_Batch.Rows(Dgv_Batch.Rows.Add)
						.Cells(0).Value = Dr("Batch")
					End With
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Dgv_Batch_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Batch.CellClick
		If Dgv_Batch.Rows.Count = 0 Or Dgv_Batch.CurrentRow Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim SelectedSplit As String = Dgv_Formula_Parent.CurrentRow.Cells(Cell_Parent_NoSplit).Value
			Dim SelectedBatch As String = Dgv_Batch.CurrentRow.Cells(0).Value

			Dim targetList As List(Of Integer) = ArrID(Dgv_Formula_Parent.CurrentRow.Index)
			Dim ListOfID As String = ""
			If targetList IsNot Nothing Then
				ListOfID = String.Join(",", targetList.Select(Function(x) $"'{x}'"))
			End If

			Dgv_Detail_Bahan.Rows.Clear()
			SQL = $"
				select a.No_Random, a.Kode_stock_Owner, a.Kode_Barang, b.Nama as Nama_Bahan, a.Jumlah, a.Satuan
				from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku a
					inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Split = '{SelectedSplit}'
				and a.Batch = '{SelectedBatch}'
				and a.ID in ({ListOfID})
				order by a.Kode_stock_Owner, b.Nama
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					With Dgv_Detail_Bahan.Rows(Dgv_Detail_Bahan.Rows.Add)
						.Cells(Cell_Bahan_KodeUnik).Value = Dr("No_Random").ToString.Trim
						.Cells(Cell_Bahan_Gudang).Value = Dr("Kode_stock_Owner").ToString.Trim
						.Cells(Cell_Bahan_KdBahan).Value = Dr("Kode_Barang").ToString.Trim
						.Cells(Cell_Bahan_NmBahan).Value = Dr("Nama_Bahan").ToString.Trim
						.Cells(Cell_Bahan_Jumlah).Value = Format(Dr("Jumlah"), "N4")
						.Cells(Cell_Bahan_Satuan).Value = Dr("Satuan").ToString.Trim
					End With
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub SalinNoSplitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoSplitToolStripMenuItem.Click
		If Dgv_Formula_Parent.Rows.Count = 0 Or Dgv_Formula_Parent.CurrentRow Is Nothing Then
			MessageBox.Show("Pilih Dahulu Transaksi Yang Ingin Disalin", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Dgv_Formula_Parent.CurrentRow.Cells(Cell_Parent_NoSplit).Value)
	End Sub

	Private Sub SalinNoFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFormulaToolStripMenuItem.Click
		If Dgv_Formula_Parent.Rows.Count = 0 Or Dgv_Formula_Parent.CurrentRow Is Nothing Then
			MessageBox.Show("Pilih Dahulu Transaksi Yang Ingin Disalin", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Dgv_Formula_Parent.CurrentRow.Cells(Cell_Parent_NoFormula).Value)
	End Sub

	Private Sub AlinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlinToolStripMenuItem.Click
		If Dgv_Detail_Bahan.Rows.Count = 0 Or Dgv_Detail_Bahan.CurrentRow Is Nothing Then
			MessageBox.Show("Pilih Dahulu Kode Unik Yang Ingin Disalin", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Dgv_Detail_Bahan.CurrentRow.Cells(Cell_Bahan_KodeUnik).Value)
	End Sub

	Private Sub CetakFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakFakturToolStripMenuItem.Click
		If Dgv_Detail_Bahan.Rows.Count = 0 Or Dgv_Detail_Bahan.CurrentRow Is Nothing Then
			MessageBox.Show("Pilih Dahulu Gudang Yang ingin Dicetak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show("Yakin Ingin Cetak Ulang Faktur Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Dim SelectedSplit As String = Dgv_Formula_Parent.CurrentRow.Cells(Cell_Parent_NoSplit).Value
		Dim SelectedBatch As String = Dgv_Batch.CurrentRow.Cells(0).Value
		Dim SelectedKodeUnik As String = Dgv_Detail_Bahan.CurrentRow.Cells(Cell_Bahan_KodeUnik).Value

		Try
			OpenConn()

			'======================================
			'=     CEK APAKAH DATA DIBATALKAN     =
			'======================================
			SQL = $"
				select 1
				from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Split = '{SelectedSplit}'
				and No_Random = '{SelectedKodeUnik}'
				and Status = 'Y'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Gagal Cetak Ulang, Karena Data Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================
			'=     CETAK ULANG     =
			'=======================
			Dim SelectionFormula As String = ""
			Dim kertas As String = "A4"

			SQL = "SELECT * FROM N_EMI_View_Faktur_Penyediaan_Bahan_Baku "
			SQL &= $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"AND No_Split = '{SelectedSplit}' "
			SQL &= $"AND No_Random = '{SelectedKodeUnik}' "
			SQL &= $"and Batch = {SelectedBatch} "

			SelectionFormula = "{N_EMI_View_Faktur_Penyediaan_Bahan_Baku.Kode_Perusahaan} = '" & KodePerusahaan & "' "
			SelectionFormula &= "AND {N_EMI_View_Faktur_Penyediaan_Bahan_Baku.No_Split} = '" & SelectedSplit & "' "
			SelectionFormula &= "AND {N_EMI_View_Faktur_Penyediaan_Bahan_Baku.No_Random} = '" & SelectedKodeUnik & "' "
			SelectionFormula &= "AND {N_EMI_View_Faktur_Penyediaan_Bahan_Baku.Batch} = " & SelectedBatch & " "

			Cetak_Barcode(New N_EMI_CR_Faktur_Penyediaan_Bahan_Baku, $"Faktur Request Material Gudang ", SQL, SelectionFormula, PrinterNameSPB, kertas, True)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Chk_TransaksiHrIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_TransaksiHrIni.CheckedChanged
		If Chk_TransaksiHrIni.Checked = True Then
			Chk_ParamTgl.Checked = False
			Btn_Cari_Click(Chk_TransaksiHrIni, e)
		End If
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Not Chk_TransaksiHrIni.Checked And Not Chk_ParamTgl.Checked And Not Chk_ParamLain.Checked Then
			MessageBox.Show("Pilih Dahulu Parameter Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Chk_TransaksiHrIni.Focus()
			Exit Sub
		End If

		If Chk_ParamTgl.Checked Then
			If Cmb_ParamTgl.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Tanggal Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_ParamTgl.DroppedDown = True : Cmb_ParamTgl.Focus()
				Exit Sub
			End If
		End If

		If Chk_ParamLain.Checked Then
			If Cmb_ParamLain.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_ParamLain.DroppedDown = True : Cmb_ParamLain.Focus()
				Exit Sub
			Else
				If Txt_ParamValue.Text.Trim.Length = 0 Then
					MessageBox.Show("Value Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_ParamValue.Focus()
					Exit Sub
				End If
			End If
		End If

		LoadDataParent()
	End Sub

	Private Sub Chk_ParamTgl_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamTgl.CheckedChanged
		If Chk_ParamTgl.Checked Then
			Cmb_ParamTgl.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
			Chk_TransaksiHrIni.Checked = False
		Else
			Cmb_ParamTgl.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
			Cmb_ParamTgl.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
		End If
	End Sub

	Private Sub Chk_ParamLain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamLain.CheckedChanged
		If Chk_ParamLain.Checked Then
			Cmb_ParamLain.Enabled = True : Txt_ParamValue.Enabled = True
		Else
			Cmb_ParamLain.Enabled = False : Txt_ParamValue.Enabled = False
			Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamValue.Text = ""
		End If
	End Sub

	Private Sub Cmb_ParamTgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamTgl.KeyPress
		If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
	End Sub

	Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
		If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
	End Sub

	Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
		If e.KeyChar = Chr(13) Then Chk_ParamLain.Focus()
	End Sub

	Private Sub Cmb_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamLain.KeyPress
		If e.KeyChar = Chr(13) Then Txt_ParamValue.Focus()
	End Sub

	Private Sub Txt_ParamValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamValue.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
	End Sub

	Private Sub Chk_ParamTgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_ParamTgl.KeyPress
		If e.KeyChar = Chr(13) Then
			If Chk_ParamTgl.Checked Then
				Cmb_ParamTgl.DroppedDown = True
				Cmb_ParamTgl.Focus()
			End If
		End If
	End Sub

	Private Sub Chk_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_ParamLain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Chk_ParamLain.Checked Then
				Cmb_ParamLain.DroppedDown = True
				Cmb_ParamLain.Focus()
			End If
		End If
	End Sub

	Private Sub BatalkanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanToolStripMenuItem.Click
		If Dgv_Formula_Parent.Rows.Count = 0 Or Dgv_Formula_Parent.CurrentRow Is Nothing Then Exit Sub

		Dim SelectedSplit As String = Dgv_Formula_Parent.CurrentRow.Cells(Cell_Parent_NoSplit).Value.ToString.Trim
		Dim SelectedTanggal As String = Dgv_Formula_Parent.CurrentRow.Cells(Cell_Parent_Tanggal).Value
		Dim SelectedJams As String = Dgv_Formula_Parent.CurrentRow.Cells(Cell_Parent_Jam).Value

		If MessageBox.Show($"Yakin Ingin Membatalkan Transaksi Split {SelectedSplit} ini???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'====================
			'=     CEK ROLE     =
			'====================
			If CekButtonRole("Batal_Transaksi_Penyediaan_Bahan_Baku") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Penyediaan Bahan Baku", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim targetList As List(Of Integer) = ArrID(Dgv_Formula_Parent.CurrentRow.Index)
			Dim ListOfID As String = ""
			If targetList IsNot Nothing Then
				ListOfID = String.Join(",", targetList.Select(Function(x) $"'{x}'"))
			Else
				CloseTrans()
				CloseConn()
				MessageBox.Show($"Terjadi Kesalahan Detail Data Tidak Ditmeukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'=========================
			'=     CEK TRANSAKSI     =
			'=========================
			SQL = $"
				select ID
				from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku
				WHERE Kode_Perusahaan = '{KodePerusahaan}'
				and No_Split = '{SelectedSplit}'
				and ID in ({ListOfID})
				and Status = 'Y'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					'Dim ID As String = Dr("ID")
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan Data Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'============================
			'=     CEK STATUS SPLIT     =
			'============================
			SQL = $"
				select Flag_Penyediaan_Bahan_Baku, Status
				from N_EMI_Transaksi_Trial_Split_Production_Order
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Transaksi = '{SelectedSplit}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Data No Split {SelectedSplit} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					If General_Class.CekNULL(Dr("Flag_Penyediaan_Bahan_Baku")) = "" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Data No Split {SelectedSplit} Belum Melakukan Penyediaan Bahan Baku", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Data No Split {SelectedSplit} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=====================================================
			'=     CEK APAKAH ADA DATA YANG SUDAH DIVALIDASI     =
			'=====================================================
			SQL = $"
				select distinct No_Random, Flag_Validasi
				from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku
				WHERE Kode_Perusahaan = '{KodePerusahaan}'
				and No_Split = '{SelectedSplit}'
				and ID in ({ListOfID})
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dim KdUnik As String = Dr("No_Random")

					If General_Class.CekNULL(Dr("Flag_Validasi")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Pembatalan Tidak Dapat Dilakukan Karena Terdapat Data pada No Split {SelectedSplit} Sudah Divalidasi Penyediaan Bahan Baku", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'====================
			'=     ROLLBACK     =
			'====================
			SQL = $"
				update N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku set Status = 'Y',
				UserID_Batal = '{UserID}', Tanggal_Batal = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Batal = '{Format(tgl_skg, "HH:mm:ss")}'
				WHERE Kode_Perusahaan = '{KodePerusahaan}'
				and No_Split = '{SelectedSplit}'
				and ID in ({ListOfID})
			"
			ExecuteTrans(SQL)

			SQL = $"
				update N_EMI_Transaksi_Trial_Split_Production_Order set Flag_Penyediaan_Bahan_Baku = NULL,
				Tanggal_Penyediaan_Bahan_Baku = NULL, Jam_Penyediaan_Bahan_Baku = NULL, User_Penyediaan_Bahan_Baku = NULL
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Transaksi = '{SelectedSplit}'

			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Chk_TransaksiHrIni.Checked = True
		Btn_Cari.PerformClick()

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

	Private Sub Dgv_Formula_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Formula_Parent.MouseMove
		HandleDataGridViewHover(Dgv_Formula_Parent, e)
	End Sub

	Private Sub Dgv_Batch_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Batch.MouseMove
		HandleDataGridViewHover(Dgv_Batch, e)
	End Sub

	Private Sub Dgv_Detail_Bahan_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Detail_Bahan.MouseMove
		HandleDataGridViewHover(Dgv_Detail_Bahan, e)
	End Sub

	Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
		If Dgv_Formula_Parent.Rows.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If
		Dim mousePos As Point = Dgv_Formula_Parent.PointToClient(Control.MousePosition)
		Dim info As DataGridView.HitTestInfo = Dgv_Formula_Parent.HitTest(mousePos.X, mousePos.Y)

		If info.RowIndex < 0 Then
			e.Cancel = True
			Exit Sub
		End If
		Dgv_Formula_Parent.ClearSelection()
		Dgv_Formula_Parent.Rows(info.RowIndex).Selected = True

		Dgv_Formula_Parent.CurrentCell = Dgv_Formula_Parent.Rows(info.RowIndex).Cells(info.ColumnIndex)
	End Sub

	Private Sub ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip2.Opening
		If Dgv_Detail_Bahan.Rows.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If
		Dim mousePos As Point = Dgv_Detail_Bahan.PointToClient(Control.MousePosition)
		Dim info As DataGridView.HitTestInfo = Dgv_Detail_Bahan.HitTest(mousePos.X, mousePos.Y)

		If info.RowIndex < 0 Then
			e.Cancel = True
			Exit Sub
		End If
		Dgv_Detail_Bahan.ClearSelection()
		Dgv_Detail_Bahan.Rows(info.RowIndex).Selected = True

		Dgv_Detail_Bahan.CurrentCell = Dgv_Detail_Bahan.Rows(info.RowIndex).Cells(info.ColumnIndex)
	End Sub

	Private Sub HandleDataGridViewHover(dgv As DataGridView, e As MouseEventArgs)

		Dim hit As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

		' 👉 Cursor hanya jika benar di CELL
		If hit.Type = DataGridViewHitTestType.Cell Then
			dgv.Cursor = Cursors.Hand
		Else
			dgv.Cursor = Cursors.Default
		End If

		' 👉 Jika bukan cell, stop di sini
		If hit.Type <> DataGridViewHitTestType.Cell Then Exit Sub

		Dim rowIndex As Integer = hit.RowIndex
		If rowIndex < 0 Then Exit Sub

		Dim lastRowIndex As Integer = -1
		If dgv.Tag IsNot Nothing Then
			lastRowIndex = CInt(dgv.Tag)
		End If

		Dim currentRow = dgv.Rows(rowIndex)

		If currentRow.DefaultCellStyle.BackColor <> Color.White AndAlso
	   currentRow.DefaultCellStyle.BackColor <> Color.Empty Then
			Exit Sub
		End If

		If lastRowIndex <> rowIndex Then

			If lastRowIndex >= 0 AndAlso lastRowIndex < dgv.Rows.Count Then
				Dim lastRow = dgv.Rows(lastRowIndex)

				If lastRow.DefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235) Then
					lastRow.DefaultCellStyle.BackColor = Color.White
				End If
			End If

			currentRow.DefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235)

			dgv.Tag = rowIndex
		End If
	End Sub

	Private Sub EnableDoubleBufferDGV(dgv As DataGridView)
		Dim t As Type = dgv.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(dgv, True, Nothing)
	End Sub

End Class
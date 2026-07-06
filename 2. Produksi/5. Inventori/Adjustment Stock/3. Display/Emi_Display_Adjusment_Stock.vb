Public Class Emi_Display_Adjusment_Stock

	Dim arrLokasi, arrTanggal, arrParamLain As New ArrayList

	Dim Cell_Parent_NoFaktur As Integer = 0
	Dim Cell_Parent_Lokasi As Integer = 1
	Dim Cell_Parent_Jenis As Integer = 2
	Dim Cell_Parent_KodeSo As Integer = 3
	Dim Cell_Parent_Keterangan As Integer = 4
	Dim Cell_Parent_Tanggal As Integer = 5
	Dim Cell_Parent_User As Integer = 6

	Dim Cell_Detail_KodeBarang As Integer = 0
	Dim Cell_Detail_Nama As Integer = 1
	Dim Cell_Detail_TotTambah As Integer = 2
	Dim Cell_Detail_TotKurang As Integer = 3
	Dim Cell_Detail_TotBagsTambah As Integer = 4
	Dim Cell_Detail_TotBagsKurang As Integer = 5
	Dim Cell_Detail_Satuan As Integer = 6
	Dim Cell_Detail_Urut As Integer = 7

	Dim Cell_Detail_Pallet_Barcode As Integer = 0
	Dim Cell_Detail_Pallet_Rak As Integer = 1
	Dim Cell_Detail_Pallet_Jumlah As Integer = 2
	Dim Cell_Detail_Pallet_JumlahBags As Integer = 3
	Dim Cell_Detail_Pallet_Satuan As Integer = 4
	Dim Cell_Detail_Pallet_Kualitas As Integer = 5

	Private lastIndex As Integer = -1
	Private originalColor As Color

	Private Sub Emi_Display_Adjusment_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		' Fungsi Kode ini untuk menghilangkan efek flicker
		EnableDoubleBufferDGV(Dgv_Parent)
		EnableDoubleBufferDGV(Dgv_Detail)
		EnableDoubleBufferDGV(Dgv_Detail_Pallet)

		Kosong()
	End Sub

	Private Sub Kosong()

		Try
			OpenConn()

			'============================
			'=     LOAD DATA FILTER     =
			'============================
			Cmb1.Items.Clear() : arrLokasi.Clear()
			SQL = "select kode_stock_owner, keterangan from Stock_Owner "
			Using Dr = OpenTrans(SQL)
				Cmb1.Items.Add("--- SELURUH ---") : arrLokasi.Add("--- SELURUH ---")
				Do While Dr.Read
					Cmb1.Items.Add(Dr("keterangan")) : arrLokasi.Add(Dr("kode_stock_owner"))
				Loop
				Cmb1.SelectedIndex = 0
			End Using

			Cmb_Tanggal.Items.Clear() : arrTanggal.Clear() : DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
			Cmb_Tanggal.Items.Add("Tanggal") : arrTanggal.Add("a.Tanggal")

			Cmb_ParamLain.Items.Clear() : arrParamLain.Clear() : Txt_ParamLain.Text = ""
			Cmb_ParamLain.Items.Add("No Faktur") : arrParamLain.Add("a.No_Faktur")
			Cmb_ParamLain.Items.Add("Lokasi") : arrParamLain.Add("a.Lokasi")
			Cmb_ParamLain.Items.Add("Jenis") : arrParamLain.Add("a.Jenis_Adjustment")
			Cmb_ParamLain.Items.Add("Gudang") : arrParamLain.Add("a.Kode_Stock_Owner")
			Cmb_ParamLain.Items.Add("User ID") : arrParamLain.Add("a.UserID")
			Cmb_ParamLain.Items.Add("Keterangan") : arrParamLain.Add("a.Keterangan")

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Chk_HariIni.Checked = True
		Rd_Semua.Checked = True

		Btn_Cari_Click(Btn_Cari, New EventArgs)

	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		Try
			OpenConn()

			Dim Filter As String = ""
			If Not Cmb1.SelectedIndex = 0 Then
				Filter &= "and a.Lokasi = '" & arrLokasi(Cmb1.SelectedIndex) & "' "
			End If

			If Chk_HariIni.Checked = True Then

				Filter &= "and a.Tanggal Between '"
				Filter &= Format(Now, "yyyy-MM-dd") & "' and '" & Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") & "' "

			End If

			If Chk_Tanggal.Checked = True Then

				Filter &= "and a.Tanggal between '"
				Filter &= Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
			End If

			If Chk_ParamLain.Checked Then

				Filter &= "and " & arrParamLain.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
			End If

			If Rd_Semua.Checked <> True Then
				If Rd_Validasi.Checked Then
					Filter &= "and a.Flag_Validation_Accounting = 'Y' "
				ElseIf Rd_Belum_Validasi.Checked Then
					Filter &= "and a.Flag_Validation_Accounting is null "
				End If
			End If

			Dgv_Parent.Rows.Clear() : Dgv_Detail.Rows.Clear() : Dgv_Detail_Pallet.Rows.Clear()
			SQL = $"
				Select a.Kode_Perusahaan, a.Status, a.Lokasi, a.No_Faktur, a.Jenis_Adjustment,
					a.Kode_Stock_Owner, a.Tanggal, a.Jam, a.Keterangan, a.UserID, a.Flag_Validation_Accounting
				from Emi_Adjustment_Stock a
				where  a.kode_perusahaan = '{KodePerusahaan}'
				{Filter}
				order by a.Tanggal, a.Jam
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					With Dgv_Parent.Rows(Dgv_Parent.Rows.Add())
						.Cells(Cell_Parent_NoFaktur).Value = Dr("No_Faktur")
						.Cells(Cell_Parent_Jenis).Value = Dr("Jenis_Adjustment")
						.Cells(Cell_Parent_Lokasi).Value = Dr("Lokasi")
						.Cells(Cell_Parent_KodeSo).Value = Dr("Kode_Stock_Owner")
						.Cells(Cell_Parent_Keterangan).Value = Dr("Keterangan")
						.Cells(Cell_Parent_Tanggal).Value = Format(Dr("Tanggal"), "dd MMM yyyy")
						.Cells(Cell_Parent_User).Value = Dr("UserID")
						.DefaultCellStyle.Padding = New Padding(5, 0, 2, 0)

						If General_Class.CekNULL(Dr("Status")).Trim = "Y" Then
							.DefaultCellStyle.BackColor = Color.DarkRed
							.DefaultCellStyle.ForeColor = Color.White
						ElseIf General_Class.CekNULL(Dr("Flag_Validation_Accounting")).Trim = "Y" Then
							.DefaultCellStyle.BackColor = Color.LightGreen
							.DefaultCellStyle.ForeColor = Color.Black
						End If

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

	Private Sub Dgv_Parent_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellClick
		If Dgv_Parent.Rows.Count = 0 OrElse Dgv_Parent.CurrentRow Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim NoTransaksi As String = Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value

			Dgv_Detail.Rows.Clear() : Dgv_Detail_Pallet.Rows.Clear()

			SQL = $"
				select a.No_Faktur, b.Kode_Barang, c.Nama, b.Total_Tambah, b.Total_Minus, b.Total_Bags_Tambah, b.Total_Bags_Minus, b.Satuan, b.Urut
				from Emi_Adjustment_Stock a, Emi_Adjustment_Stock_Detail b, barang c
				where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.Kode_Perusahaan
				and a.No_Faktur = b.No_Faktur
				and a.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{NoTransaksi}'
				and a.Status is null
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					With Dgv_Detail.Rows(Dgv_Detail.Rows.Add)
						.Cells(Cell_Detail_KodeBarang).Value = Dr("Kode_Barang")
						.Cells(Cell_Detail_Nama).Value = Dr("Nama")
						.Cells(Cell_Detail_TotTambah).Value = Format(Dr("Total_Tambah"), "N4")
						.Cells(Cell_Detail_TotKurang).Value = Format(Dr("Total_Minus"), "N4")
						.Cells(Cell_Detail_TotBagsTambah).Value = Format(Dr("Total_Bags_Tambah"), "N4")
						.Cells(Cell_Detail_TotBagsKurang).Value = Format(Dr("Total_Bags_Minus"), "N4")
						.Cells(Cell_Detail_Satuan).Value = Dr("Satuan")
						.Cells(Cell_Detail_Urut).Value = Dr("Urut")
						.DefaultCellStyle.Padding = New Padding(5, 0, 2, 0)
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

	Private Sub Dgv_Detail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Detail.CellClick
		If Dgv_Detail.Rows.Count = 0 OrElse Dgv_Detail.CurrentRow Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim NoTransaksi As String = Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value
			Dim Urut As String = Dgv_Detail.CurrentRow.Cells(Cell_Detail_Urut).Value

			Dgv_Detail_Pallet.Rows.Clear()
			SQL = $"
				select a.No_Faktur, c.Id_Wms, d.Keterangan as Rak, c.Jumlah, c.Jumlah_Bags, e.Keterangan as Kualitas, b.Satuan, (f.Qr_Code+'-'+f.Kode_Unik_Berjalan) as Barcode
				from Emi_Adjustment_Stock a, Emi_Adjustment_Stock_Detail b, Emi_Adjustment_Stock_Det c, View_Warehouse_Position d, EMI_Master_Warna e, Barang_SN f
				where a.kode_perusahaan = b.kode_perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan
					and a.Kode_Perusahaan = f.Kode_Perusahaan
				and a.No_Faktur = b.No_Faktur
				and b.No_Faktur = c.No_Faktur and b.Urut = c.Urut_Adj
				and c.Id_Wms = d.Id_WMS_Warehouse_Position
				and c.Warna = e.Kode_Warna
				and a.Kode_Stock_Owner = f.Kode_Stock_Owner and b.Kode_Barang = f.Kode_Barang and c.Serial_Number = f.Serial_Number
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{NoTransaksi}'
				and b.Urut = '{Urut}'
				and a.Status is null
				order by a.No_Faktur, c.Id_Wms, c.Warna
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					With Dgv_Detail_Pallet.Rows(Dgv_Detail_Pallet.Rows.Add)
						.Cells(Cell_Detail_Pallet_Barcode).Value = Dr("Barcode")
						.Cells(Cell_Detail_Pallet_Rak).Value = Dr("Rak")
						.Cells(Cell_Detail_Pallet_Jumlah).Value = Format(Dr("Jumlah"), "N4")
						.Cells(Cell_Detail_Pallet_JumlahBags).Value = Format(Dr("Jumlah_Bags"), "N4")
						.Cells(Cell_Detail_Pallet_Satuan).Value = Dr("Satuan")
						.Cells(Cell_Detail_Pallet_Kualitas).Value = Dr("Kualitas")
						.DefaultCellStyle.Padding = New Padding(5, 0, 2, 0)
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

	Private Sub Chk_HariIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_HariIni.CheckedChanged
		If Chk_HariIni.Checked = True Then
			Chk_Tanggal.Checked = False
			Btn_Cari_Click(Chk_Tanggal, e)
		End If
	End Sub

	Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged

		If Chk_Tanggal.Checked Then
			Cmb_Tanggal.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
			Chk_HariIni.Checked = False
		Else
			Cmb_Tanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
			Cmb_Tanggal.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
		End If
	End Sub

	Private Sub Chk_ParamLain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamLain.CheckedChanged
		If Chk_ParamLain.Checked = True Then
			Cmb_ParamLain.Enabled = True : Txt_ParamLain.Enabled = True
		Else
			Cmb_ParamLain.Enabled = False : Txt_ParamLain.Enabled = False
			Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamLain.Text = ""
		End If
	End Sub

	Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then
			MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value)
	End Sub

	Private Sub BatalkanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanToolStripMenuItem.Click
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then Exit Sub

		Dim SelectedFaktur As String = Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value

		If MessageBox.Show($"Yakin Ingin Membatalkan Tranaksi {SelectedFaktur}??", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===========================
			'=     CEK ROLE BUTTON     =
			'===========================
			If CekButtonRole("Batal_Transaksi_Adjustment_Stock") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Pembatalkan Adjustment Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'==================================================
			'=     CEK APAKAH NO FAKTUR SUDAH DIBATALKAN     =
			'==================================================
			SQL = $"
				select Status, Flag_Validation_Accounting from Emi_Adjustment_Stock
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur = '{SelectedFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Status")).Trim = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Pembatalan Tidak Dapat Dilakukan Karena No Faktur {SelectedFaktur} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					If General_Class.CekNULL(Dr("Flag_Validation_Accounting")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Pembatalan Tidak Dapat Dilakukan Karena No Faktur {SelectedFaktur} Sudah Divalidasi Accounting", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Pembatalan Tidak Dapat Dilakukan Karena No Faktur {SelectedFaktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=============================
			'=     PROSES PEMBATALAN     =
			'=============================
			SQL = $"
				update Emi_Adjustment_Stock set Status = 'Y',
				UserID_Batal = '{UserID}', Tanggal_Batal = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Batal = '{Format(tgl_skg, "HH:mm:ss")}'
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur = '{SelectedFaktur}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"No Faktur {SelectedFaktur} Berhasil Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Chk_TransaksiHrIni.Checked = True
		Btn_Cari.PerformClick()
	End Sub

	'======================================================================================================================================================
	'=     UTILITY
	'======================================================================================================================================================

	Protected Overrides Sub WndProc(ByRef m As Message)
		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub Dgv_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Parent.MouseMove, Dgv_Detail.MouseMove, Dgv_Detail_Pallet.MouseMove
		HandleDataGridViewHover(sender, e)
	End Sub

	Private Sub EnableDoubleBufferDGV(dgv As DataGridView)
		Dim t As Type = dgv.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(dgv, True, Nothing)
	End Sub

	Private Sub HandleDataGridViewHover(dgv As DataGridView, e As MouseEventArgs)
		Dim hit As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

		dgv.Cursor = If(hit.Type = DataGridViewHitTestType.Cell, Cursors.Hand, Cursors.Default)

		If hit.RowIndex <> lastIndex Then

			If lastIndex >= 0 AndAlso lastIndex < dgv.Rows.Count Then
				dgv.Rows(lastIndex).DefaultCellStyle.BackColor = originalColor
			End If

			If hit.Type = DataGridViewHitTestType.Cell AndAlso hit.RowIndex >= 0 Then
				lastIndex = hit.RowIndex

				Dim currentRow = dgv.Rows(lastIndex)

				originalColor = currentRow.DefaultCellStyle.BackColor

				Dim displayColor As Color = currentRow.InheritedStyle.BackColor

				Dim amount As Integer = 23 ' Semakin kecil angka ini, semakin halus efeknya
				currentRow.DefaultCellStyle.BackColor = Color.FromArgb(
					Math.Max(0, displayColor.R - amount),
					Math.Max(0, displayColor.G - amount),
					Math.Max(0, displayColor.B - amount)
				)
			Else
				lastIndex = -1
			End If
		End If
	End Sub

	Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
		If Dgv_Parent.Rows.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If

		Dim mousePos As Point = Dgv_Parent.PointToClient(Control.MousePosition)

		Dim info As DataGridView.HitTestInfo = Dgv_Parent.HitTest(mousePos.X, mousePos.Y)

		If info.RowIndex < 0 Then
			e.Cancel = True
			Exit Sub
		End If

		Dgv_Parent.Rows(info.RowIndex).Selected = True

		Dgv_Parent.CurrentCell = Dgv_Parent.Rows(info.RowIndex).Cells(info.ColumnIndex)
	End Sub

	Private Sub Dgv_Detail_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_Detail.CellFormatting
		Try
			If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub

			If e.ColumnIndex = Cell_Detail_TotTambah Or e.ColumnIndex = Cell_Detail_TotKurang Then

				Dim jumlah As Double = Val(HilangkanTanda(e.Value.ToString()))
				Dim satuan As String = Dgv_Detail.Rows(e.RowIndex).Cells(Cell_Detail_Satuan).Value?.ToString()

				e.Value = $"{Format(jumlah, "N4")} {satuan}"
				e.FormattingApplied = True

			End If
		Catch ex As Exception
			Debug.WriteLine("Error di CellFormatting " & ex.Message)
		End Try
	End Sub

	Private Sub Dgv_Detail_Pallet_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_Detail_Pallet.CellFormatting
		Try
			If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub

			If e.ColumnIndex = Cell_Detail_Pallet_Jumlah Then

				Dim jumlah As Double = Val(HilangkanTanda(e.Value.ToString()))
				Dim satuan As String = Dgv_Detail_Pallet.Rows(e.RowIndex).Cells(Cell_Detail_Pallet_Satuan).Value?.ToString()

				e.Value = $"{Format(jumlah, "N4")} {satuan}"
				e.FormattingApplied = True

			End If
		Catch ex As Exception
			Debug.WriteLine("Error di CellFormatting " & ex.Message)
		End Try
	End Sub

End Class
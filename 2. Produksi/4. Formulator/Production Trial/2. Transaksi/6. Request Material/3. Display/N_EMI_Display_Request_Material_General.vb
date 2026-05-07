Public Class N_EMI_Display_Request_Material_General

	Dim ArrTanggal, ArrParamLainLama As New ArrayList

	Dim Dgv_Parent_NoFaktur, Dgv_Parent_Status, Dgv_Parent_GudangReq, Dgv_Parent_Jenis, Dgv_Parent_Tanggal, Dgv_Parent_Jam, Dgv_Parent_Keterangan, Dgv_Parent_UserCreate As String

	Dim Cell_Parent_NoFaktur As Integer = 0
	Dim Cell_Parent_Status As Integer = 1
	Dim Cell_Parent_GudangReq As Integer = 2
	Dim Cell_Parent_Jenis As Integer = 3
	Dim Cell_Parent_Tanggal As Integer = 4
	Dim Cell_Parent_Jam As Integer = 5
	Dim Cell_Parent_Keterangan As Integer = 6
	Dim Cell_Parent_UserCreate As Integer = 7

	Dim Dgv_Detail_Barang_GudangAsal, Dgv_Detail_Barang_KodeBarang, Dgv_Detail_Barang_NmBarang, Dgv_Detail_Barang_Keterangan, Dgv_Detail_Barang_Jumlah, Dgv_Detail_Barang_SatuSan As String
	Dim Cell_Detail_Barang_GudangAsal As Integer = 0
	Dim Cell_Detail_Barang_KodeBarang As Integer = 1
	Dim Cell_Detail_Barang_NmBarang As Integer = 2
	Dim Cell_Detail_Barang_Keterangan As Integer = 3
	Dim Cell_Detail_Barang_Jumlah As Integer = 4
	Dim Cell_Detail_Barang_Satuan As Integer = 5

	Private lastIndex As Integer = -1
	Private originalColor As Color

	Private Sub N_EMI_Display_Request_Material_General_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBufferDGV(Dgv_Parent)
		EnableDoubleBufferDGV(Dgv_Detail_Barang)

		Cmb_ParamTgl.Items.Clear() : ArrTanggal.Clear()
		Cmb_ParamTgl.Items.Add("Tanggal") : ArrTanggal.Add("a.Tanggal")

		Cmb_ParamLain.Items.Clear() : ArrParamLainLama.Clear()
		Cmb_ParamLain.Items.Add("No Faktur") : ArrParamLainLama.Add("a.No_Faktur")
		Cmb_ParamLain.Items.Add("Gudang Request") : ArrParamLainLama.Add("a.Kode_Stock_Owner_Req")
		Cmb_ParamLain.Items.Add("Keterangan") : ArrParamLainLama.Add("a.Keterangan")
		Cmb_ParamLain.Items.Add("User Create") : ArrParamLainLama.Add("a.UserId")

		Kosong()

	End Sub

	Private Sub Kosong()

		Chk_TransaksiHrIni.Checked = True
		Btn_Cari_Click(Btn_Cari, New EventArgs)
	End Sub

	Private Sub KosongDetail()
		Txt_Detail_NoFaktur.Text = ""
		Txt_Detail_NoFormula.Text = ""
		Txt_Detail_StatusTransaksi.Text = ""
		Txt_Detail_TanggalTerpenuhi.Text = ""
		Txt_Detail_JamTerpenuhi.Text = ""
		Txt_Detail_UsetTerpenuhi.Text = ""
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

		LoadParent()

	End Sub

	Private Sub Get_Data_Parent(ByVal index As Integer)
		Dgv_Parent_NoFaktur = Dgv_Parent.Rows(index).Cells(Cell_Parent_NoFaktur).Value
		Dgv_Parent_Status = Dgv_Parent.Rows(index).Cells(Cell_Parent_Status).Value
		Dgv_Parent_GudangReq = Dgv_Parent.Rows(index).Cells(Cell_Parent_GudangReq).Value
		Dgv_Parent_Jenis = Dgv_Parent.Rows(index).Cells(Cell_Parent_Jenis).Value
		Dgv_Parent_Tanggal = Dgv_Parent.Rows(index).Cells(Cell_Parent_Tanggal).Value
		Dgv_Parent_Jam = Dgv_Parent.Rows(index).Cells(Cell_Parent_Jam).Value
		Dgv_Parent_Keterangan = Dgv_Parent.Rows(index).Cells(Cell_Parent_Keterangan).Value
		Dgv_Parent_UserCreate = Dgv_Parent.Rows(index).Cells(Cell_Parent_UserCreate).Value
	End Sub

	Private Sub LoadParent()
		get_jam()

		Try
			OpenConn()

			Dim Filter As String = " "
			If Cmb_Lokasi.SelectedIndex > 0 Then
				'Filter &= "AND b.lokasi = '" & Cmb_Lokasi.Text & "' "
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

			KosongDetail()
			Dgv_Parent.Rows.Clear() : Dgv_Detail_Barang.Rows.Clear()
			SQL = $"
				select a.No_Faktur, a.Status, a.Kode_Stock_Owner_Req, a.Tanggal, a.Jam, a.Keterangan, a.UserId, a.Flag_Terpenuhi, a.Flag_Trial_Formula
				from Emi_Material_Requisition_General a
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				order by a.Tanggal, a.Jam
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					With Dgv_Parent.Rows(Dgv_Parent.Rows.Add())
						.Cells(Cell_Parent_NoFaktur).Value = Dr("No_Faktur")
						.Cells(Cell_Parent_Status).Value = If(General_Class.CekNULL(Dr("Status")) = "", "T", "Y")
						.Cells(Cell_Parent_GudangReq).Value = Dr("Kode_Stock_Owner_Req")
						.Cells(Cell_Parent_Jenis).Value = If(General_Class.CekNULL(Dr("Flag_Trial_Formula")) = "", "General", "Trial Formula")
						.Cells(Cell_Parent_Tanggal).Value = Format(Dr("Tanggal"), "dd MMM yyyy")
						.Cells(Cell_Parent_Jam).Value = Dr("Jam")
						.Cells(Cell_Parent_Keterangan).Value = Dr("Keterangan")
						.Cells(Cell_Parent_UserCreate).Value = Dr("UserId")
						.DefaultCellStyle.Padding = New Padding(2, 0, 2, 0)

						If General_Class.CekNULL(Dr("Status")) = "Y" Then
							.DefaultCellStyle.BackColor = Color.DarkRed
							.DefaultCellStyle.ForeColor = Color.White
						ElseIf General_Class.CekNULL(Dr("Flag_Terpenuhi")) = "Y" Then
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

	Private Sub Dgv_Formula_Parent_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellClick
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim SelectedFaktur As String = Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value

			'=========================
			'=     DETAIL FAKTUR     =
			'=========================
			SQL = $"
				select a.No_Faktur, a.Flag_Trial_Formula, a.No_Transaksi_Formula,
					a.Flag_Terpenuhi, a.Tanggal_Terpenuhi, a.Jam_Terpenuhi, a.User_Terpenuhi
				from Emi_Material_Requisition_General a
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Txt_Detail_NoFaktur.Text = Dr("No_Faktur")
					Txt_Detail_NoFormula.Text = If(General_Class.CekNULL(Dr("No_Transaksi_Formula")) = "", "-", Dr("No_Transaksi_Formula"))
					Txt_Detail_StatusTransaksi.Text = If(General_Class.CekNULL(Dr("Flag_Terpenuhi")) = "", "Belum Terpenuhi", "Sudah Terpenuhi")

					If General_Class.CekNULL(Dr("Flag_Terpenuhi")) = "Y" Then
						Txt_Detail_TanggalTerpenuhi.Text = If(General_Class.CekNULL(Dr("Tanggal_Terpenuhi")) = "", "-", Format(Dr("Tanggal_Terpenuhi"), "dd MMM yyyy"))
						Txt_Detail_JamTerpenuhi.Text = If(General_Class.CekNULL(Dr("Jam_Terpenuhi")) = "", "-", Dr("Jam_Terpenuhi"))
						Txt_Detail_UsetTerpenuhi.Text = If(General_Class.CekNULL(Dr("User_Terpenuhi")) = "", "-", Dr("User_Terpenuhi"))
						Txt_Detail_StatusTransaksi.BackColor = Color.LightGreen
					Else
						Txt_Detail_TanggalTerpenuhi.Text = "-"
						Txt_Detail_JamTerpenuhi.Text = "-"
						Txt_Detail_UsetTerpenuhi.Text = "-"
						Txt_Detail_StatusTransaksi.BackColor = Color.White

					End If
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Request Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					KosongDetail()
					Exit Sub
				End If
			End Using

			'=========================
			'=     DETAIL BARANG     =
			'=========================
			Dgv_Detail_Barang.Rows.Clear()
			SQL = $"
				select b.Gudang_Asal, b.Kode_Barang, c.Nama as Nama_Barang, b.Jumlah, b.Satuan, b.Keterangan, b.Flag_Terpenuhi
				from Emi_Material_Requisition_General a
					inner join Emi_Material_Requisition_General_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner_Req = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					With Dgv_Detail_Barang.Rows(Dgv_Detail_Barang.Rows.Add())
						.Cells(Cell_Detail_Barang_GudangAsal).Value = Dr("Gudang_Asal")
						.Cells(Cell_Detail_Barang_KodeBarang).Value = Dr("Kode_Barang")
						.Cells(Cell_Detail_Barang_NmBarang).Value = Dr("Nama_Barang")
						.Cells(Cell_Detail_Barang_Keterangan).Value = Dr("Keterangan")
						.Cells(Cell_Detail_Barang_Jumlah).Value = Dr("Jumlah")
						.Cells(Cell_Detail_Barang_Satuan).Value = Dr("Satuan")
						.DefaultCellStyle.Padding = New Padding(5, 0, 5, 0)
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

	Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then
			MessageBox.Show("Pilih Dahulu Transaksi Yang Ingin Disalin", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value)
	End Sub

	Private Sub BatalkanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanToolStripMenuItem.Click
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then Exit Sub

		Dim SelectedFaktur As String = Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value.ToString.Trim

		If MessageBox.Show($"Yakin Ingin Membatalkan Transaksi Faktur {SelectedFaktur} ini???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'====================
			'=     CEK ROLE     =
			'====================
			If CekButtonRole("Batal_Transaksi_Request_Material_General") = "T" Then
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Transakasi Request Material", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return
			End If

			'================================
			'=     CEK STATUS TRANSAKSI     =
			'================================
			SQL = $"
				select a.Status
				from Emi_Material_Requisition_General a
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						MessageBox.Show($"Terjadi Kesalahan, Data Request Sudah Dibatalkan Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Return
					End If
				Else
					Dr.Close()
					MessageBox.Show($"Terjadi Kesalahan, Data Request Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			End Using

			'========================================
			'=     CEK APAKAH SUDAH DI TRANSFER     =
			'========================================

			'CEK TRANSFER STOCK
			SQL = $"
				select top 1 1
				from Tf_Stock_Parent a
					inner join Tf_Stock b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Status is null
				and b.Urut_Material_Requisition_Convert in (
					select x.Urut_Oto
					from Emi_Material_Requisition_General z
						inner join Emi_Material_Requisition_General_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
					where z.Kode_Perusahaan = a.Kode_Perusahaan
					and z.No_Faktur = '{SelectedFaktur}'
				)
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					MessageBox.Show("Pembatalan Tidak Dapat Dilakukan Karena Data Sudah Ditransfer Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			End Using

			'CEK TRANSFER STOCK
			SQL = $"
				select top 1 1
				from Tf_Stock_QC a
					inner join Tf_Stock_QC_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Status is null
				and b.Urut_Material_Requisition_Convert in (
					select x.Urut_Oto
					from Emi_Material_Requisition_General z
						inner join Emi_Material_Requisition_General_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
					where z.Kode_Perusahaan = a.Kode_Perusahaan
					and z.No_Faktur = '{SelectedFaktur}'
				)
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					MessageBox.Show("Pembatalan Tidak Dapat Dilakukan Karena Data Sudah Ditransfer Stock Premix", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			End Using

			'=========================
			'=     ROLLBACK DATA     =
			'=========================
			SQL = $"
				update Emi_Material_Requisition_General set Status = 'Y',
					Tanggal_Batal = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Batal = '{Format(tgl_skg, "HH:mm:ss")}', UserID_Batal = '{UserID}'
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur = '{SelectedFaktur}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			MessageBox.Show("Transaksi Berhasil Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		Finally
			CloseTrans()
			CloseConn()
			SetCellDefaultColor(Dgv_Parent)
		End Try

		Btn_Cari.PerformClick()

	End Sub

	'======================================================================================================================================================
	'=     HANDLE FILTER
	'======================================================================================================================================================
	Private Sub Chk_TransaksiHrIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_TransaksiHrIni.CheckedChanged
		If Chk_TransaksiHrIni.Checked = True Then
			Chk_ParamTgl.Checked = False
			Btn_Cari_Click(Chk_ParamTgl, e)
		End If
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
		If Chk_ParamLain.Checked = True Then
			Cmb_ParamLain.Enabled = True : Txt_ParamValue.Enabled = True
		Else
			Cmb_ParamLain.Enabled = False : Txt_ParamValue.Enabled = False
			Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamValue.Text = ""
		End If
	End Sub

	Private Sub Cmb_ParamLain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_ParamLain.SelectedIndexChanged
		If Cmb_ParamLain.SelectedIndex = -1 Then
			Txt_ParamValue.Enabled = False
		Else
			Txt_ParamValue.Enabled = True
		End If
		Txt_ParamValue.Text = ""
	End Sub

	Private Sub Dgv_Detail_Barang_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_Detail_Barang.CellFormatting
		Try
			If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub

			If e.ColumnIndex = Cell_Detail_Barang_Jumlah Then

				Dim jumlah As Double = Val(HilangkanTanda(e.Value.ToString()))
				Dim satuan As String = Dgv_Detail_Barang.Rows(e.RowIndex).Cells(Cell_Detail_Barang_Satuan).Value?.ToString()

				e.Value = $"{Format(jumlah, "N4")} {satuan}"
				e.FormattingApplied = True

			End If
		Catch ex As Exception
			Debug.WriteLine("Error di CellFormatting " & ex.Message)
		End Try
	End Sub

	'======================================================================================================================================================
	'=     HELPER
	'======================================================================================================================================================
	Private Sub Dgv_Formula_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Parent.MouseMove, Dgv_Detail_Barang.MouseMove
		HandleDataGridViewHover(sender, e)
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

		Dgv_Parent.ClearSelection()
		Dgv_Parent.Rows(info.RowIndex).Selected = True

		Dgv_Parent.CurrentCell = Dgv_Parent.Rows(info.RowIndex).Cells(info.ColumnIndex)
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

	Private Sub SetCellDefaultColor(dgv As DataGridView)
		Dim currentRow = dgv.Rows(lastIndex)
		currentRow.DefaultCellStyle.BackColor = originalColor
	End Sub

	Private Sub Dgv_Parent_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Dgv_Parent.CellPainting, Dgv_Detail_Barang.CellPainting

		If e.RowIndex = -1 Then
			e.Paint(e.ClipBounds, DataGridViewPaintParts.Background Or DataGridViewPaintParts.ContentForeground Or DataGridViewPaintParts.SelectionBackground)

			Using p As New Pen(Color.Gray, 1)
				e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1)
			End Using

			e.Handled = True
		End If
	End Sub

End Class
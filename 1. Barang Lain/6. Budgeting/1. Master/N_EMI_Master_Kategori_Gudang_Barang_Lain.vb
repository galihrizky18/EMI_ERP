Public Class N_EMI_Master_Kategori_Gudang_Barang_Lain

	Dim arrGudang, arrJenisGudang As New ArrayList

	Dim Lv_Kd_Kategori_Gudang, Lv_Keterangan, Lv_Gudang, Lv_Kd_So_Gudang As String

	Dim item_Kd_Kategori_Gudang As Integer = 0
	Dim item_Keterangan As Integer = 1
	Dim item_Gudang As Integer = 2
	Dim item_Kd_So_Gudang As Integer = 3

	Private Sub N_EMI_Master_Kategori_Gudang_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Lv_Data.Columns.Clear()
		Lv_Data.Columns.Add("Kode Kategori Gudang", 178, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Keterangan", 350, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Gudang", 200, HorizontalAlignment.Left)
		'Hide
		Lv_Data.Columns.Add("Kd_So_Gudang", 0, HorizontalAlignment.Left)
		Lv_Data.View = View.Details

		Cmb_Jenis_Gudang.Items.Clear() : arrJenisGudang.Clear()
		Cmb_Jenis_Gudang.Items.Add("Warehouse") : arrJenisGudang.Add("Warehouse")
		Cmb_Jenis_Gudang.Items.Add("Department") : arrJenisGudang.Add("Department")

		Try
			OpenConn()

			Cmb_Gudang.Items.Clear() : arrGudang.Clear()
			SQL = $"
				select Kode_Stock_Owner, Keterangan
				from Stock_Owner_Gudang_Lain
				where Kode_Perusahaan = '{KodePerusahaan}'
				order by Kode_Stock_Owner
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Gudang.Items.Add(Dr("Keterangan")) : arrGudang.Add(Dr("Kode_Stock_Owner"))
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

	Private Sub Get_Data_Lv(ByVal index As Integer)
		Lv_Kd_Kategori_Gudang = Lv_Data.Items(index).SubItems(item_Kd_Kategori_Gudang).Text
		Lv_Keterangan = Lv_Data.Items(index).SubItems(item_Keterangan).Text
		Lv_Gudang = Lv_Data.Items(index).SubItems(item_Gudang).Text
		Lv_Kd_So_Gudang = Lv_Data.Items(index).SubItems(item_Kd_So_Gudang).Text
	End Sub

	Private Sub Kosong()

		Txt_Kd_Kategori.Text = String.Empty
		Txt_Keterangan.Text = String.Empty

		Cmb_Gudang.SelectedIndex = -1
		Cmb_Jenis_Gudang.SelectedIndex = -1

		Btn_Simpan.Tag = "SIMPAN"
		Btn_Simpan.Text = "&Simpan"

		Txt_Kd_Kategori.Enabled = True
		Txt_Kd_Kategori.BackColor = Color.White

		Txt_Kd_Kategori.Focus()

		LoadData()

	End Sub

	Private Sub LoadData()

		Try
			OpenConn()

			Lv_Data.Items.Clear()
			SQL = $"
				select a.kode_kategori_gudang, a.kode_Stock_owner_gudang, b.Keterangan as Gudang, a.keterangan
				from N_EMI_Master_Kategori_Gudang_Barang_Lain a
					inner join Stock_Owner_Gudang_Lain b on a.kode_perusahaan = b.kode_perusahaan and a.kode_Stock_owner_gudang = b.kode_stock_owner
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.status is null
				order by a.tanggal, a.jam
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Data.Items.Add(Dr("kode_kategori_gudang"))
					Lv.SubItems.Add(Dr("keterangan"))
					Lv.SubItems.Add(Dr("Gudang"))
					Lv.SubItems.Add(Dr("kode_Stock_owner_gudang"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Txt_Kd_Kategori_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Kategori.Leave
		If Txt_Kd_Kategori.Text.Trim.Length = 0 Then Exit Sub

		Try
			OpenConn()

			SQL = $"
				select Kode_Kategori_Gudang, Kode_Stock_Owner_Gudang, Keterangan, Jenis_Gudang
				from N_EMI_Master_Kategori_Gudang_Barang_Lain
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Status is null
				and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Txt_Kd_Kategori.Text = Dr("Kode_Kategori_Gudang")
					Cmb_Gudang.SelectedIndex = arrGudang.IndexOf(Dr("Kode_Stock_Owner_Gudang"))
					Cmb_Jenis_Gudang.SelectedIndex = arrJenisGudang.IndexOf(Dr("Jenis_Gudang"))
					Txt_Keterangan.Text = Dr("Keterangan")

					Txt_Kd_Kategori.Enabled = False
					Txt_Kd_Kategori.BackColor = Color.FromArgb(235, 235, 235)

					Btn_Simpan.Tag = "UPDATE"
					Btn_Simpan.Text = "&Update"
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_Kd_Kategori.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Kategori Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kd_Kategori.Focus()
			Exit Sub

		ElseIf Cmb_Gudang.SelectedIndex = -1 Then
			MessageBox.Show("Gudang Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Gudang.DroppedDown = True
			Cmb_Gudang.Focus()
			Exit Sub

		ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Keterangan.Focus()
			Exit Sub

		ElseIf Cmb_Jenis_Gudang.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Gudang Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Jenis_Gudang.DroppedDown = True
			Cmb_Jenis_Gudang.Focus()
			Exit Sub
		End If

		Dim Action As String = ""

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If Btn_Simpan.Tag.ToString.ToUpper = "SIMPAN" Then

				'===============================================
				'=      CEK APAKAH KODE KATEGORI SUDAH ADA     =
				'===============================================
				SQL = $"
					select Kode_Perusahaan
					from N_EMI_Master_Kategori_Gudang_Barang_Lain
					where Kode_Perusahaan = '{KodePerusahaan}'
					and status is null
					and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Kategori Gudang {Txt_Kd_Kategori.Text.Trim} Sudah Ada. Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = $"
					insert into N_EMI_Master_Kategori_Gudang_Barang_Lain (Kode_Perusahaan, Kode_Kategori_Gudang, Kode_Stock_Owner_Gudang, Keterangan, Tanggal, Jam, Jenis_Gudang)
					values ('{KodePerusahaan}', '{Txt_Kd_Kategori.Text}', '{arrGudang(Cmb_Gudang.SelectedIndex)}',
					'{Txt_Keterangan.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{arrJenisGudang(Cmb_Jenis_Gudang.SelectedIndex)}')
				"
				ExecuteTrans(SQL)

				Action = "Simpan"

			ElseIf Btn_Simpan.Tag.ToString.ToUpper = "UPDATE" Then

				'===============================================
				'=      CEK APAKAH KODE KATEGORI SUDAH ADA     =
				'===============================================
				SQL = $"
					select Kode_Perusahaan
					from N_EMI_Master_Kategori_Gudang_Barang_Lain
					where Kode_Perusahaan = '{KodePerusahaan}'
					and status is null
					and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Kategori Gudang {Txt_Kd_Kategori.Text.Trim} Tidak Ada. Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = $"
					update N_EMI_Master_Kategori_Gudang_Barang_Lain
					set Kode_Stock_Owner_Gudang = '{arrGudang(Cmb_Gudang.SelectedIndex)}', Keterangan = '{Txt_Keterangan.Text.Trim}', Jenis_Gudang = '{arrJenisGudang(Cmb_Jenis_Gudang.SelectedIndex)}'
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Status is null
					and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
				"
				ExecuteTrans(SQL)

				Action = "Update"

			End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		MessageBox.Show($"Data Berhasil Di {Action} ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Kosong()

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click
		If Txt_Kd_Kategori.Text.Trim.Length = 0 Then Exit Sub

		If MessageBox.Show("Yakin Ingin Menghapus Data Kategori Ini??", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If CekButtonRole("Hapus_Kategori_Gudang_Barang_Lain") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Menghapus Kategori Gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'===================================
			'=     CEK APAKAH KATEGORI ADA     =
			'===================================
			SQL = $"
				select Kode_Perusahaan
				from N_EMI_Master_Kategori_Gudang_Barang_Lain
				where Kode_Perusahaan = '{KodePerusahaan}'
				and status is null
				and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Kode Kategori {Txt_Kd_Kategori.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'================================================
			'=     CEK APAKAH KATEGORI SUDAH DI BINDING     =
			'================================================
			SQL = $"
                select Kode_Perusahaan
                from N_EMI_Master_Binding_Kategori_Gudang_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
            "
			Using Dr = OpenTransSQL(SQL)
				If Dr.Read Then
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Kode Kategori {Txt_Kd_Kategori.Text.Trim} Sudah Di Binding, Tidak Bisa Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = $"
				delete
				from N_EMI_Master_Kategori_Gudang_Barang_Lain
				where Kode_Perusahaan = '{KodePerusahaan}'
				and status is null
				and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
			"
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

		MessageBox.Show($"Data {Txt_Kd_Kategori.Text.Trim} Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Kosong()

	End Sub

	Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
		If Lv_Data.Items.Count = 0 Then Exit Sub

		Get_Data_Lv(Lv_Data.FocusedItem.Index)

		Txt_Kd_Kategori.Text = Lv_Kd_Kategori_Gudang
		Txt_Kd_Kategori_Leave(sender, New EventArgs)

	End Sub

	Private Sub Txt_Kd_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Kategori.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			'Cmb_Gudang.DroppedDown = True
			Cmb_Gudang.Focus()
		End If
	End Sub

	Private Sub Cmb_Gudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Gudang.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			Txt_Keterangan.Focus()
		End If
	End Sub

	Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			Cmb_Jenis_Gudang.Focus()
		End If
	End Sub

	Private Sub Cmb_Jenis_Gudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Gudang.KeyPress
		If e.KeyChar = Chr(13) Then
			Btn_Simpan.Focus()
		End If
	End Sub

	'==========================================================================================================================================================================
	'=     CEGAH MAXIMIZED DOUBLE KLIK TITLE BAR
	'==========================================================================================================================================================================

	Protected Overrides Sub WndProc(ByRef m As Message)

		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove
		Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

		If info.Item IsNot Nothing Then
			Lv_Data.Cursor = Cursors.Hand
		Else
			Lv_Data.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Lv_Data_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data.MouseLeave
		Lv_Data.Cursor = Cursors.Default
	End Sub

End Class
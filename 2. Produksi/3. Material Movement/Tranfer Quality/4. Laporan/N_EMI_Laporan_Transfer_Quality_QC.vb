Public Class N_EMI_Laporan_Transfer_Quality_QC

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim Arr_Kode_Warna, Arr_Kode_SO As New ArrayList

	Dim FilterLain As New List(Of (Value_Cmb As String, SQL As String)) From {
		(OpsiSeluruh, OpsiSeluruh),
		("Keterangan", "Keterangan")
	}

	Dim SwitchAutoComplete As Boolean

	Private Sub N_EMI_Laporan_Transfer_Quality_QC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv_NoFaktur)
		EnableDoubleBuffer(Lv_Barang)

		Lv_NoFaktur.Columns.Clear()
		Lv_NoFaktur.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
		Lv_NoFaktur.Columns.Add("Tangagl", 110, HorizontalAlignment.Center)
		Lv_NoFaktur.Columns.Add("Keterangan", 220, HorizontalAlignment.Left)
		Lv_NoFaktur.View = View.Details

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 305, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details

		Lv_NoFaktur.Location = New Point(113, 189)
		Lv_Barang.Location = New Point(113, 216)

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Cmb_Kualitas_Awal.Items.Clear() : Cmb_Kualitas_Akhir.Items.Clear() : Arr_Kode_Warna.Clear()
			Cmb_Kualitas_Awal.Items.Add(OpsiSeluruh)
			Cmb_Kualitas_Akhir.Items.Add(OpsiSeluruh)
			Arr_Kode_Warna.Add(OpsiSeluruh)
			SQL = $"
				select Kode_Warna, Keterangan
				from EMI_Master_Warna
				where Kode_Perusahaan = '{KodePerusahaan}'
				order by Keterangan
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Cmb_Kualitas_Awal.Items.Add(Dr("Keterangan"))
						Cmb_Kualitas_Akhir.Items.Add(Dr("Keterangan"))
						Arr_Kode_Warna.Add(Dr("Kode_Warna"))
					Loop While Dr.Read
				End If
			End Using

			Cmb_Lokasi.Items.Clear() : Arr_Kode_SO.Clear()
			Cmb_Lokasi.Items.Add(OpsiSeluruh) : Arr_Kode_SO.Add(OpsiSeluruh)
			SQL = $"
				select Kode_Stock_Owner, Keterangan
				from Stock_Owner_Gudang
				where Kode_Perusahaan = '{KodePerusahaan}'
				order by Kode_Stock_Owner
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Cmb_Lokasi.Items.Add(Dr("Keterangan")) : Arr_Kode_SO.Add(Dr("Kode_Stock_Owner"))
					Loop While Dr.Read
				End If
			End Using

			Cmb_Lain.Items.Clear()
			For Each item In FilterLain
				Cmb_Lain.Items.Add(item.Value_Cmb)
			Next

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()

	End Sub

	Private Sub Kosong()

		Lv_NoFaktur.Visible = False
		Lv_Barang.Visible = False
		Lv_NoFaktur.Items.Clear() : Lv_Barang.Items.Clear()

		Tgl1.Value = DateTime.Today
		Tgl2.Value = DateTime.Today

		Cmb_Kualitas_Awal.SelectedIndex = 0
		Cmb_Kualitas_Akhir.SelectedIndex = 0
		Cmb_Lokasi.SelectedIndex = 0
		Cmb_Lain.SelectedIndex = 0

		SwitchAutoComplete = True
		Txt_NoFaktur.Text = OpsiSeluruh : Txt_Keterangan.Text = OpsiSeluruh
		Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh
		SwitchAutoComplete = False

	End Sub

	Private Sub Cmb_Lain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lain.SelectedIndexChanged
		If Cmb_Lain.SelectedIndex = 0 Then
			Txt_Lain.Text = OpsiSeluruh
			Txt_Lain.Enabled = False
			BtnCetak.Focus()
		Else
			Txt_Lain.Text = ""
			Txt_Lain.Enabled = True
			Txt_Lain.Focus()
		End If

	End Sub

	Private Sub Txt_NoFaktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_NoFaktur.TextChanged
		If SwitchAutoComplete Then Exit Sub

		If Txt_NoFaktur.Text.Trim.Length = 0 Then
			Me.Size = New Size(651, 359)
			Lv_NoFaktur.Visible = False
			Txt_NoFaktur.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(651, 440)
			Lv_NoFaktur.Visible = True
		End If

		Try
			OpenConn()

			Lv_NoFaktur.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_NoFaktur.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			SQL = $"
				select No_Faktur, Tanggal, keterangan
				from Emi_TF_Quality
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur like '%{Txt_NoFaktur.Text.Trim}%'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_NoFaktur.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("keterangan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_NoFaktur_Leave(sender As Object, e As EventArgs) Handles Txt_NoFaktur.Leave
		If Txt_NoFaktur.Text.Trim.Length = 0 Then Exit Sub
		If Lv_NoFaktur.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_NoFaktur.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL = $"
					select No_Faktur, Tanggal, keterangan
					from Emi_TF_Quality
					where Kode_Perusahaan = '{KodePerusahaan}'
					and No_Faktur like '%{Txt_NoFaktur.Text.Trim}%'
				"
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_NoFaktur.Text = Dr("No_Faktur")
						Txt_Keterangan.Text = Dr("keterangan")
						Txt_KdBarang.Focus()
					Else
						MessageBox.Show("No Faktur ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_NoFaktur.Text = ""
						Txt_Keterangan.Text = ""
						Txt_NoFaktur.Focus()
					End If

					Me.Size = New Size(651, 359)
					Lv_NoFaktur.Visible = False
				End Using
			Else
				Txt_KdBarang.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_NoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoFaktur.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_NoFaktur.Text.Trim.Length = 0 Then Txt_NoFaktur.Focus()
			Txt_NoFaktur_Leave(Txt_NoFaktur, e)

			Me.Size = New Size(651, 359)
			Lv_NoFaktur.Visible = False

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_NoFaktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NoFaktur.KeyDown
		If e.KeyCode = Keys.Down Then Lv_NoFaktur.Focus()
	End Sub

	Private Sub Lv_NoFaktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_NoFaktur.DoubleClick
		If Lv_NoFaktur.Items.Count = 0 Or Lv_NoFaktur.FocusedItem.Index = -1 Then Exit Sub

		Dim NoFaktur As String = Lv_NoFaktur.FocusedItem.SubItems(0).Text
		Dim Keterangan As String = Lv_NoFaktur.FocusedItem.SubItems(2).Text

		SwitchAutoComplete = True
		Txt_NoFaktur.Text = NoFaktur
		Txt_Keterangan.Text = Keterangan
		SwitchAutoComplete = False

		Me.Size = New Size(651, 359)
		Lv_NoFaktur.Visible = False

		Txt_KdBarang.Focus()
	End Sub

	Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
		Me.Close()
	End Sub

	Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("No Fakturharus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_NoFaktur.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		ElseIf Cmb_Kualitas_Awal.SelectedIndex = -1 Then
			MessageBox.Show("Kualitas Awal Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Kualitas_Awal.Focus() : Exit Sub
		ElseIf Cmb_Kualitas_Akhir.SelectedIndex = -1 Then
			MessageBox.Show("Kualitas Akhir Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Kualitas_Akhir.Focus() : Exit Sub
		ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
			MessageBox.Show("Lokasi Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lokasi.Focus() : Exit Sub
		End If

		If Cmb_Lain.SelectedIndex <> 0 Then
			If Txt_Lain.Text.Trim.Length = 0 Then
				MessageBox.Show("Value Lain Tidak Boleh Kosong!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_Lain.Focus() : Exit Sub
			End If
		End If

		Try
			OpenConn()

			Dim SF As String = ""
			Dim Filter As String = ""

			SF = "{N_EMI_View_Laporan_Transfer_Quality.Kode_Perusahaan} = '" & KodePerusahaan & "' "
			SF = SF & "and {N_EMI_View_Laporan_Transfer_Quality.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
			SF = SF & "{N_EMI_View_Laporan_Transfer_Quality.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

			If Cmb_Kualitas_Awal.SelectedIndex <> 0 Then
				Filter &= $"and Quality_Awal = '{Arr_Kode_Warna(Cmb_Kualitas_Awal.SelectedIndex)}' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Quality.Quality_Awal} = '" & Arr_Kode_Warna(Cmb_Kualitas_Awal.SelectedIndex) & "'"
			End If
			If Cmb_Kualitas_Akhir.SelectedIndex <> 0 Then
				Filter &= $"and Quality_Tujuan = '{Arr_Kode_Warna(Cmb_Kualitas_Akhir.SelectedIndex)}' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Quality.Quality_Tujuan} = '" & Arr_Kode_Warna(Cmb_Kualitas_Akhir.SelectedIndex) & "'"
			End If
			If Cmb_Lokasi.SelectedIndex <> 0 Then
				Filter &= $"and Lokasi = '{Arr_Kode_SO(Cmb_Lokasi.SelectedIndex)}' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Quality.Lokasi} = '" & Arr_Kode_SO(Cmb_Lokasi.SelectedIndex) & "'"
			End If
			If Txt_NoFaktur.Text.Trim.Length > 0 And Not Txt_NoFaktur.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
				Filter &= $"and No_Faktur = '{Txt_NoFaktur.Text.Trim}' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Quality.No_Faktur} = '" & Txt_NoFaktur.Text.Trim & "'"
			End If
			If Txt_KdBarang.Text.Trim.Length > 0 And Not Txt_KdBarang.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
				Filter &= $"and Kode_Barang = '{Txt_KdBarang.Text.Trim}' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Quality.Kode_Barang} = '" & Txt_KdBarang.Text.Trim & "'"
			End If
			If Cmb_Lain.SelectedIndex <> 0 Then
				If Not Txt_Lain.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					Filter &= $"and {FilterLain(Cmb_Lain.SelectedIndex).SQL} like '%{Txt_Lain.Text.Trim}%' "
					SF = SF & "And {N_EMI_View_Laporan_Transfer_Quality." & FilterLain(Cmb_Lain.SelectedIndex).SQL & "} like '*" & Txt_Lain.Text.Trim & "*'"
				End If
			End If

			SQL = $"
				select Kode_Perusahaan
				from N_EMI_View_Laporan_Transfer_Quality
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Tanggal between '{Format(Tgl1.Value, "yyyy-MM-dd")}' and '{Format(Tgl2.Value, "yyyy-MM-dd")}'
				{Filter}
			"
			Using DS = BindingTrans(SQL)
				With DS.Tables("MyTable")
					If .Rows.Count <> 0 Then

						Dim CrDoc As New N_EMI_CR_Laporan_Transfer_Quality

						CrDoc.SetDataSource(DS)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
																			Format(Tgl2.Value, "dd/MMM/yyyy")
						CrDoc.RecordSelectionFormula = SF

						With A_Place_For_Printing2
							.Text = "Laporan Transfer Quality"
							.CrystalReportViewer1.ReportSource = CrDoc
							.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
							.Refresh()
							.Show()
						End With
					Else

						CloseConn()
						MessageBox.Show("Data Transfer Quality Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub

					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Lv_NoFaktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_NoFaktur.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_NoFaktur_DoubleClick(Lv_NoFaktur, e)
		End If
	End Sub

	Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
		If SwitchAutoComplete Then Exit Sub

		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(651, 359)
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(651, 470)
			Lv_Barang.Visible = True
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			SQL = "select Distinct a.Kode_Barang, a.Nama "
			SQL = SQL & "from barang a, EMI_Group_Jenis b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Kode_Barang like '%" & Txt_KdBarang.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
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

	Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
		If SwitchAutoComplete Then Exit Sub

		If Txt_NmBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(651, 359)
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(651, 470)
			Lv_Barang.Visible = True
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select Distinct a.Kode_Barang, a.Nama "
			SQL = SQL & "from barang a, EMI_Group_Jenis b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Nama like '%" & Txt_NmBarang.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
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

				SQL = "select Distinct a.Kode_Barang, a.Nama "
				SQL = SQL & "from barang a, EMI_Group_Jenis b "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_KdBarang.Text = Dr("Kode_Barang")
						Txt_NmBarang.Text = Dr("Nama")
						Cmb_Lain.DroppedDown = True
						Cmb_Lain.Focus()
					Else
						MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_KdBarang.Text = ""
						Txt_NmBarang.Text = ""
						Txt_KdBarang.Focus()
					End If

					Me.Size = New Size(651, 359)
					Lv_Barang.Visible = False
				End Using
			Else
				Cmb_Lain.DroppedDown = True
				Cmb_Lain.Focus()
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

			Me.Size = New Size(651, 359)
			Lv_Barang.Visible = False

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_KdBarang_Leave(Txt_NmBarang, e)

			Me.Size = New Size(651, 359)
			Lv_Barang.Visible = False

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

		SwitchAutoComplete = True
		Txt_KdBarang.Text = KdBarang
		Txt_NmBarang.Text = NmKdBarang
		SwitchAutoComplete = False

		Me.Size = New Size(651, 359)
		Lv_Barang.Visible = False

		BtnCetak.Focus()
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Lv_NoFaktur_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_NoFaktur.MouseMove, Lv_Barang.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
	End Sub

	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then Tgl2.Focus()
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Kualitas_Awal.DroppedDown = True
			Cmb_Kualitas_Awal.Focus()
		End If
	End Sub

	Private Sub Cmb_Kualitas_Awal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Kualitas_Awal.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Kualitas_Akhir.DroppedDown = True
			Cmb_Kualitas_Akhir.Focus()
		End If
	End Sub

	Private Sub Cmb_Kualitas_Akhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Kualitas_Akhir.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Lokasi.DroppedDown = True
			Cmb_Lokasi.Focus()
		End If
	End Sub

	Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
		If e.KeyChar = Chr(13) Then Txt_NoFaktur.Focus()
	End Sub

	Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Lain.SelectedIndex = 0 Then
				BtnCetak.Focus()
				Txt_Lain.Text = OpsiSeluruh
				Txt_Lain.Enabled = False
			Else
				Txt_Lain.Enabled = True
				Txt_Lain.Text = ""
				Txt_Lain.Focus()
			End If
		End If
	End Sub

	Private Sub HandleListViewHover(lvw As ListView, e As MouseEventArgs)
		Dim hit As ListViewHitTestInfo = lvw.HitTest(e.Location)

		lvw.Cursor = If(hit.Item IsNot Nothing, Cursors.Hand, Cursors.Default)

		If hit.Item IsNot lastHoverItem Then
			lvw.BeginUpdate()

			If lastHoverItem IsNot Nothing Then
				lastHoverItem.BackColor = originalItemColor
			End If

			If hit.Item IsNot Nothing AndAlso hit.Item.Tag Is Nothing Then
				lastHoverItem = hit.Item
				originalItemColor = lastHoverItem.BackColor

				Dim amt As Integer = 10
				lastHoverItem.BackColor = Color.FromArgb(
				Math.Max(0, originalItemColor.R - amt),
				Math.Max(0, originalItemColor.G - amt),
				Math.Max(0, originalItemColor.B - amt)
			)
			Else
				lastHoverItem = Nothing
			End If

			lvw.EndUpdate()
		End If
	End Sub

End Class
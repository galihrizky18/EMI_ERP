Public Class N_EMI_Laporan_Transfer_Stock_Sementara

	Dim arrFaktur, arrLain, arrLokasi As New ArrayList

	Dim switchAutoComplete As Boolean = False

	Private Sub N_EMI_Laporan_Transfer_Stock_Sementara_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Lv_NoFaktur.Columns.Clear()
		Lv_NoFaktur.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
		Lv_NoFaktur.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
		Lv_NoFaktur.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
		Lv_NoFaktur.View = View.Details

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details

		Try
			OpenConn()

			Cmb_Jenis_Faktur.Items.Clear() : arrFaktur.Clear()
			Cmb_Jenis_Faktur.Items.Add(OpsiSeluruh) : arrFaktur.Add(OpsiSeluruh)
			Cmb_Jenis_Faktur.Items.Add("Faktur Transfer") : arrFaktur.Add("No_Faktur")
			'Cmb_Jenis_Faktur.Items.Add("Faktur Split Order") : arrFaktur.Add("No_Split")
			'Cmb_Jenis_Faktur.Items.Add("Faktur Request Material") : arrFaktur.Add("No_Faktur_RM")

			Cmb_Lain.Items.Clear() : arrLain.Clear()
			Cmb_Lain.Items.Add(OpsiSeluruh) : arrLain.Add(OpsiSeluruh)
			'Cmb_Lain.Items.Add("") : arrLain.Add("")

			Cmb_Lokasi.Items.Clear() : arrLokasi.Clear()
			Cmb_Lokasi.Items.Add(OpsiSeluruh) : arrLokasi.Add(OpsiSeluruh)
			Cmb_Lokasi.Items.Add("Lokasi Awal") : arrLokasi.Add("So_Awal")
			Cmb_Lokasi.Items.Add("Lokasi Tujuan") : arrLokasi.Add("So_Tujuan")

			Cmb_KdSO.Items.Clear()
			Cmb_KdSO.Items.Add(OpsiSeluruh)
			SQL = "select Kode_Stock_Owner from stock_owner_gudang "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"order by Kode_Stock_Owner "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_KdSO.Items.Add(Dr("Kode_Stock_Owner"))
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

	Private Sub Kosong()

		Tgl1.Value = DateTime.Today
		Tgl2.Value = DateTime.Today

		Cmb_Lokasi.SelectedIndex = 0
		Cmb_KdSO.SelectedIndex = 0
		Cmb_Jenis_Faktur.SelectedIndex = 0
		Cmb_Lain.SelectedIndex = 0

		switchAutoComplete = True
		Txt_No_Faktur.Text = OpsiSeluruh
		Txt_KdBarang.Text = OpsiSeluruh
		Txt_NmBarang.Text = OpsiSeluruh
		Txt_Lain.Text = OpsiSeluruh
		switchAutoComplete = False

		Me.Size = New Size(646, 326)
		Tgl1.Focus()

	End Sub

	Private Sub Txt_No_Faktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Faktur.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_No_Faktur.Text.Trim.Length = 0 Then
			Me.Size = New Size(646, 326)
			Lv_NoFaktur.Visible = False
			Lv_NoFaktur.Location = New Point(635, 161)
			Txt_No_Faktur.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(646, 413)
			Lv_NoFaktur.Visible = True
			Lv_NoFaktur.Location = New Point(114, 161)
		End If

		Try
			OpenConn()

			Lv_NoFaktur.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_NoFaktur.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			If Cmb_Jenis_Faktur.SelectedIndex = 1 Then
				SQL = "SELECT No_Faktur, Tanggal, Keterangan "
				SQL &= $"from N_EMI_Transaksi_Transfer_Stock_Sementara "
				SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and status is null "
				SQL &= $"and No_Faktur like '%{Txt_No_Faktur.Text.Trim}%' "
				SQL &= $"order by No_Faktur "
			ElseIf Cmb_Jenis_Faktur.SelectedIndex = 2 Then
				SQL = "select No_Transaksi as no_faktur, Tanggal, No_Batch as Keterangan from Emi_Split_Production_Order "
				SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and status is null "
				SQL &= $"and No_Transaksi like '%{Txt_No_Faktur.Text.Trim}%' "
				SQL &= $"order by No_Transaksi "
			ElseIf Cmb_Jenis_Faktur.SelectedIndex = 3 Then
				SQL = "select No_Faktur, Tanggal, Keterangan from Emi_Material_Requisition "
				SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and status is null "
				SQL &= $"and No_Faktur like '%{Txt_No_Faktur.Text.Trim}%' "
				SQL &= $"order by No_Faktur "
			End If
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_NoFaktur.Items.Add(Dr("no_faktur"))
					Lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Keterangan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_No_Faktur_Leave(sender As Object, e As EventArgs) Handles Txt_No_Faktur.Leave
		If Txt_No_Faktur.Text.Trim.Length = 0 Then Exit Sub
		If Lv_NoFaktur.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_No_Faktur.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				If Cmb_Jenis_Faktur.SelectedIndex = 1 Then
					SQL = "SELECT No_Faktur, Tanggal, Keterangan "
					SQL &= $"from N_EMI_Transaksi_Transfer_Stock_Sementara "
					SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and status is null "
					SQL &= $"and No_Faktur = '{Txt_No_Faktur.Text.Trim}' "
					SQL &= $"order by No_Faktur "
				ElseIf Cmb_Jenis_Faktur.SelectedIndex = 2 Then
					SQL = "select No_Transaksi as no_faktur, Tanggal, No_Batch as Keterangan from Emi_Split_Production_Order "
					SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and status is null "
					SQL &= $"and No_Transaksi = '{Txt_No_Faktur.Text.Trim}' "
					SQL &= $"order by No_Transaksi "
				ElseIf Cmb_Jenis_Faktur.SelectedIndex = 3 Then
					SQL = "select No_Faktur, Tanggal, Keterangan from Emi_Material_Requisition "
					SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and status is null "
					SQL &= $"and No_Faktur = '{Txt_No_Faktur.Text.Trim}' "
					SQL &= $"order by No_Faktur "
				End If
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_No_Faktur.Text = Dr("no_faktur")
						Txt_KdBarang.Focus()
					Else
						MessageBox.Show("No Transaksi ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_No_Faktur.Text = ""
						Txt_No_Faktur.Focus()
					End If

					Me.Size = New Size(646, 326)
					Lv_NoFaktur.Visible = False
					Lv_NoFaktur.Location = New Point(635, 161)
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

	Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(646, 326)
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(635, 188)
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(646, 443)
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(114, 188)
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

					Me.Size = New Size(646, 326)
					Lv_Barang.Visible = False
					Lv_Barang.Location = New Point(635, 188)
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

	Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_NmBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(646, 326)
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(635, 188)
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(646, 443)
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(114, 188)
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

	'==========================================================================================================================================================
	'==========================================================================================================================================================

	Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		End If

		If Cmb_Lokasi.SelectedIndex <> 0 Then
			If Cmb_KdSO.SelectedIndex = -1 Then
				MessageBox.Show("Lokasi Gudang Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_Lokasi.Focus() : Exit Sub
			End If
		End If

		If Cmb_Jenis_Faktur.SelectedIndex <> 0 Then
			If Txt_No_Faktur.Text.Trim.Length = 0 Then
				MessageBox.Show("No Faktur harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_No_Faktur.Focus() : Exit Sub
			End If
		End If

		If Cmb_Lain.SelectedIndex <> 0 Then
			If Txt_Lain.Text.Trim.Length = 0 Then
				MessageBox.Show("Value Lain harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_No_Faktur.Focus() : Exit Sub
			End If
		End If

		Try
			OpenConn()

			Dim SF As String = ""

			SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Transfer_Stock_Sementara "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

			SF = "{N_EMI_View_Laporan_Transfer_Stock_Sementara.Kode_Perusahaan} = '" & KodePerusahaan & "' "
			SF = SF & "and {N_EMI_View_Laporan_Transfer_Stock_Sementara.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
			SF = SF & "{N_EMI_View_Laporan_Transfer_Stock_Sementara.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

			If Cmb_Lokasi.SelectedIndex <> 0 Then
				SQL = SQL & "and " & arrLokasi(Cmb_Lokasi.SelectedIndex) & " = '" & Cmb_KdSO.Text.Trim & "' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Stock_Sementara." & arrLokasi(Cmb_Lokasi.SelectedIndex) & "} = '" & Cmb_KdSO.Text.Trim & "'"
			End If

			If Cmb_Jenis_Faktur.SelectedIndex <> 0 Then
				If Not Txt_No_Faktur.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					SQL = SQL & "and " & arrFaktur(Cmb_Jenis_Faktur.SelectedIndex) & " = '" & Txt_No_Faktur.Text.Trim & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transfer_Stock_Sementara." & arrFaktur(Cmb_Jenis_Faktur.SelectedIndex) & "} = '" & Txt_No_Faktur.Text.Trim & "'"
				End If
			End If

			If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
				SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Stock_Sementara.Kode_Barang} = '" & Txt_KdBarang.Text & "'"
			End If

			If Cmb_Lain.SelectedIndex <> 0 Then
				If Not Txt_Lain.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					SQL = SQL & "and " & arrLain(Cmb_Lain.SelectedIndex) & " = '" & Txt_Lain.Text.Trim & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transfer_Stock_Sementara." & arrLain(Cmb_Lain.SelectedIndex) & "} = '" & Txt_Lain.Text.Trim & "'"
				End If
			End If

			Using DS = BindingTrans(SQL)
				With DS.Tables("MyTable")
					If .Rows.Count <> 0 Then

						Dim CrDoc As New N_EMI_CR_Laporan_Transfer_Stock_Sementara

						CrDoc.SetDataSource(DS)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
																			Format(Tgl2.Value, "dd/MMM/yyyy")
						CrDoc.RecordSelectionFormula = SF

						With A_Place_For_Printing2
							.Text = "Laporan Transfer Stock Sementara"
							.CrystalReportViewer1.ReportSource = CrDoc
							.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
							.Refresh()
							.Show()
						End With
					Else

						CloseConn()
						MessageBox.Show("Data Transfer Stock Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

	Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
		Me.Close()
	End Sub

	'==========================================================================================================================================================
	'==========================================================================================================================================================

	Private Sub Cmb_Lokasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lokasi.SelectedIndexChanged
		If Cmb_Lokasi.SelectedIndex = 0 Then
			Cmb_KdSO.Enabled = False
		Else
			Cmb_KdSO.Enabled = True
		End If
		Cmb_KdSO.SelectedIndex = 0
	End Sub

	Private Sub Cmb_Jenis_Faktur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Faktur.SelectedIndexChanged
		If Cmb_Jenis_Faktur.SelectedIndex = 0 Then
			Txt_No_Faktur.Enabled = False
		Else
			Txt_No_Faktur.Enabled = True
		End If
		switchAutoComplete = True
		Txt_No_Faktur.Text = OpsiSeluruh
		switchAutoComplete = False
	End Sub

	Private Sub Cmb_Lain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lain.SelectedIndexChanged
		If Cmb_Lain.SelectedIndex = 0 Then
			Txt_Lain.Enabled = False
		Else
			Txt_Lain.Enabled = True
		End If
		Txt_Lain.Text = OpsiSeluruh
	End Sub

	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then Tgl2.Focus()
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Lokasi.DroppedDown = True
			Cmb_Lokasi.Focus()
		End If
	End Sub

	Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Lokasi.SelectedIndex = 0 Then
				Cmb_Lokasi.Focus()
				Cmb_KdSO.Enabled = False
			Else
				Cmb_KdSO.Enabled = True
				Cmb_KdSO.DroppedDown = True
				Cmb_KdSO.Focus()
			End If
		End If

		Cmb_KdSO.SelectedIndex = 0
	End Sub

	Private Sub Cmb_KdSO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KdSO.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_KdSO.SelectedIndex = 0 Then
				Cmb_KdSO.Focus()
			Else
				Cmb_Jenis_Faktur.DroppedDown = True
				Cmb_Jenis_Faktur.Focus()
			End If
		End If

		Cmb_Jenis_Faktur.SelectedIndex = 0
	End Sub

	Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Lain.SelectedIndex = 0 Then
				Cmb_Lain.Focus()
				Txt_Lain.Enabled = False
			Else
				Txt_Lain.Enabled = True
				Txt_Lain.Focus()
			End If

			Txt_Lain.Text = OpsiSeluruh
		End If
	End Sub

	Private Sub Cmb_Jenis_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Faktur.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Jenis_Faktur.SelectedIndex = 0 Then
				Cmb_Jenis_Faktur.Focus()
				Txt_No_Faktur.Enabled = False
			Else
				Txt_No_Faktur.Enabled = True
				Txt_No_Faktur.Focus()
			End If

			switchAutoComplete = True
			Txt_No_Faktur.Text = OpsiSeluruh
			switchAutoComplete = True
		End If

	End Sub

	Private Sub Txt_No_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Faktur.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_No_Faktur.Text.Trim.Length = 0 Then Txt_No_Faktur.Focus()
			Txt_No_Faktur_Leave(Txt_No_Faktur, e)

			Me.Size = New Size(646, 326)
			Lv_NoFaktur.Visible = False
			Lv_NoFaktur.Location = New Point(635, 161)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_No_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Faktur.KeyDown
		If e.KeyCode = Keys.Down Then Lv_NoFaktur.Focus()
	End Sub

	Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
			Txt_KdBarang_Leave(Txt_KdBarang, e)

			Me.Size = New Size(646, 326)
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(635, 188)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_KdBarang_Leave(Txt_NmBarang, e)

			Me.Size = New Size(646, 326)
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(635, 188)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Lain.KeyPress
		If e.KeyChar = Chr(13) Then BtnCetak.Focus()
	End Sub

	Private Sub Lv_NoFaktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_NoFaktur.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_NoFaktur_DoubleClick(Lv_NoFaktur, e)
		End If
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

		switchAutoComplete = True
		Txt_KdBarang.Text = KdBarang
		Txt_NmBarang.Text = NmKdBarang
		switchAutoComplete = False

		Me.Size = New Size(646, 326)
		Lv_Barang.Visible = False
		Lv_Barang.Location = New Point(635, 188)

		BtnCetak.Focus()
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	Private Sub Lv_NoFaktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_NoFaktur.DoubleClick
		If Lv_NoFaktur.Items.Count = 0 Or Lv_NoFaktur.FocusedItem.Index = -1 Then Exit Sub

		Dim NoFaktur As String = Lv_NoFaktur.FocusedItem.SubItems(0).Text

		switchAutoComplete = True
		Txt_No_Faktur.Text = NoFaktur
		switchAutoComplete = False

		Me.Size = New Size(646, 326)
		Lv_NoFaktur.Visible = False
		Lv_NoFaktur.Location = New Point(635, 161)

		Txt_KdBarang.Focus()
	End Sub

End Class
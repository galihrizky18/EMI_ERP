Public Class N_EMI_Laporan_Transfer_Stock_Barang_Lain

	Dim arrSO, arrParamLain, arrParamLainSF As New ArrayList
	Dim JudulForm As String = "Laporan Transfer Stock"

	Private Sub N_EMI_Laporan_Transfer_Stock_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Kosong()

	End Sub

	Private Sub Kosong()

		Tgl1.Value = Date.Now : Tgl2.Value = Date.Now

		Txt_KdBarang.Text = "--- SELURUH ---" : Txt_NmBarang.Text = "--- SELURUH ---"

		Cmb_ParamLain.Items.Clear() : arrParamLain.Clear() : arrParamLainSF.Clear()
		Cmb_ParamLain.Items.Add("--- SELURUH ---") : arrParamLain.Add("--- SELURUH ---") : arrParamLainSF.Add("--- SELURUH ---")
		Cmb_ParamLain.Items.Add("User ID") : arrParamLain.Add("UserId") : arrParamLainSF.Add("{N_EMI_View_Laporan_Transfer_Stock_Barang_Lain.UserId}")
		Cmb_ParamLain.SelectedIndex = 0
		Txt_ParamLain.Text = ""

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details

		Try
			OpenConn()

			'================ ==================================
			'======= ambil dulu lokasi gudang yg di izinkan ====
			'===================================================
			Dim listGudang As New List(Of String)
			SQL = "select  b.Kode_Stock_Owner_Gudang  "
			SQL = SQL & "from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a   "
			SQL = SQL & "inner join N_EMI_Master_Kategori_Gudang_Barang_Lain b on a.kode_perusahaan = b.kode_perusahaan and a.id_kategori_gudang = b.urut_oto  "
			SQL = SQL & " where a.status is null and b.status is null "
			SQL = SQL & "and a.User_ID = '" & UserID & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "group by b.Kode_Stock_Owner_Gudang "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					listGudang.Add("'" & Dr("Kode_Stock_Owner_Gudang").ToString() & "'")
				Loop
			End Using

			' Jika kosong, kasih nilai palsu biar IN() tidak error
			If listGudang.Count = 0 Then
				listGudang.Add("'0'")
			End If

			' Gabungkan hasil jadi 1 string
			Dim inGudang As String = String.Join(",", listGudang)

			Cmb_SOAwal.Items.Clear()
			Cmb_SOTujuan.Items.Clear() : arrSO.Clear()
			Cmb_SOAwal.Items.Add("--- SELURUH ---") : Cmb_SOTujuan.Items.Add("--- SELURUH ---") : arrSO.Add("--- SELURUH ---")
			SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang_Lain where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "
			SQL = SQL & "and kode_stock_owner in (" & inGudang & ")"
			SQL = SQL & "order by kode_stock_owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_SOAwal.Items.Add(dr("Keterangan")) : arrSO.Add(dr("kode_stock_owner"))
					Cmb_SOTujuan.Items.Add(dr("Keterangan"))
				Loop
			End Using
			Cmb_SOAwal.SelectedIndex = 0 : Cmb_SOTujuan.SelectedIndex = 0

			Cmb_GroupJenis.Items.Clear()
			Cmb_GroupJenis.Items.Add("--- SELURUH ---")
			SQL = "select Kode_Group_Jenis from EMI_Group_Jenis_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Group_Jenis"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_GroupJenis.Items.Add(Dr("Kode_Group_Jenis"))
				Loop
			End Using
			Cmb_GroupJenis.SelectedIndex = 0

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Cmb_JenisTransfer.Items.Clear()
		Cmb_JenisTransfer.Items.Add("--- SELURUH ---")
		Cmb_JenisTransfer.Items.Add("Antar Rak")
		Cmb_JenisTransfer.Items.Add("Antar Gudang")
		Cmb_JenisTransfer.SelectedIndex = 0

		Lv_Barang.Visible = False
		Lbl_Panah.Visible = False
		Cmb_SOTujuan.Visible = False

		Me.Size = New Size(645, 366)
	End Sub

	Private Sub Cmb_JenisTransfer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_JenisTransfer.SelectedIndexChanged
		If Cmb_JenisTransfer.Items.Count = 0 Or Cmb_JenisTransfer.SelectedIndex = -1 Then Exit Sub

		If Cmb_JenisTransfer.SelectedIndex = 2 Then
			Lbl_Panah.Visible = True
			Cmb_SOTujuan.Visible = True
			Cmb_SOTujuan.SelectedIndex = 0
		Else
			Lbl_Panah.Visible = False
			Cmb_SOTujuan.Visible = False
			Cmb_SOTujuan.SelectedIndex = 0
		End If

	End Sub

	'============================================================================================================================================================================================================
	'=     HANDLE TEXT CHANGE
	'============================================================================================================================================================================================================
	Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(645, 366)
			Lv_Barang.Location = New Point(650, 234)
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(645, 486)
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(124, 234)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add("--- SELURUH ---")
			Lv.SubItems.Add("--- SELURUH ---")
			SQL = "select distinct Kode_Barang, Nama from barang_lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang like '%" & Txt_KdBarang.Text & "%'"
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
		If Txt_NmBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(645, 366)
			Lv_Barang.Location = New Point(650, 234)
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(645, 486)
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(124, 234)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add("--- SELURUH ---")
			Lv.SubItems.Add("--- SELURUH ---")
			SQL = "select distinct Kode_Barang, Nama from barang_lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_NmBarang.Text & "%'"
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

	'============================================================================================================================================================================================================
	'=     LEAVE
	'============================================================================================================================================================================================================
	Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
		If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Barang.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_KdBarang.Text = "--- SELURUH ---" Then

				SQL = "select distinct Kode_Barang, Nama from barang_lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Txt_KdBarang.Text & "'"
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_KdBarang.Text = Dr("Kode_Barang")
						Txt_NmBarang.Text = Dr("Nama")
						Cmb_ParamLain.DroppedDown = True
						Cmb_ParamLain.Focus()
					Else
						MessageBox.Show("Barang tidak ditemukan . . ! !", JudulForm)
						Txt_KdBarang.Text = ""
						Txt_NmBarang.Text = ""
						Txt_KdBarang.Focus()
					End If

					Me.Size = New Size(645, 366)
					Lv_Barang.Location = New Point(650, 234)
					Lv_Barang.Visible = False
				End Using
			Else
				Cmb_ParamLain.DroppedDown = True
				Cmb_ParamLain.Focus()

			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	'============================================================================================================================================================================================================
	'=     LISTVIEW
	'============================================================================================================================================================================================================
	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

		Txt_KdBarang.Text = KdBarang
		Txt_NmBarang.Text = NmBarang

		Me.Size = New Size(645, 366)
		Lv_Barang.Location = New Point(650, 234)
		Lv_Barang.Visible = False

		Cmb_ParamLain.Focus()
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	'============================================================================================================================================================================================================
	'=     KEYPRESS & KEYDOWN
	'============================================================================================================================================================================================================
	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then Tgl2.Focus()
	End Sub

	Private Sub Cmb_Status_KeyPress(sender As Object, e As KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then
			Cmb_JenisTransfer.DroppedDown = True
			Cmb_JenisTransfer.Focus()
		End If
	End Sub

	Private Sub Cmb_SOAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_SOAwal.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_JenisTransfer.SelectedIndex = 2 Then
				Cmb_SOTujuan.DroppedDown = True
				Cmb_SOTujuan.Focus()
			Else
				Cmb_GroupJenis.DroppedDown = True
				Cmb_GroupJenis.Focus()
			End If
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
			Txt_KdBarang_Leave(Txt_KdBarang, e)

			Me.Size = New Size(645, 366)
			Lv_Barang.Location = New Point(650, 234)
			Lv_Barang.Visible = False

			'Cmb_ParamLain.Focus()
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_KdBarang_Leave(Txt_NmBarang, e)

			Me.Size = New Size(645, 366)
			Lv_Barang.Location = New Point(650, 234)
			Lv_Barang.Visible = False

			'Cmb_ParamLain.Focus()
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Cmb_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamLain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_ParamLain.SelectedIndex = 0 Then
				BtnCetak.Focus()
			Else
				Txt_ParamLain.Focus()
			End If
		End If
	End Sub

	Private Sub Txt_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamLain.KeyPress
		If e.KeyChar = Chr(13) Then BtnCetak.Focus()
	End Sub

	Private Sub Cmb_ParamLain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_ParamLain.SelectedIndexChanged
		If Cmb_ParamLain.SelectedIndex = 0 Then
			Txt_ParamLain.Text = ""
			Txt_ParamLain.Enabled = False
		Else
			Txt_ParamLain.Text = ""
			Txt_ParamLain.Enabled = True
		End If
	End Sub

	Private Sub Cmb_JenisTransfer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_JenisTransfer.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_SOAwal.DroppedDown = True
			Cmb_SOAwal.Focus()
		End If
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_JenisTransfer.DroppedDown = True
			Cmb_JenisTransfer.Focus()
		End If
	End Sub

	Private Sub Cmb_SOTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_SOTujuan.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_GroupJenis.DroppedDown = True
			Cmb_GroupJenis.Focus()
		End If
	End Sub

	Private Sub Cmb_GroupJenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_GroupJenis.KeyPress
		If e.KeyChar = Chr(13) Then Txt_KdBarang.Focus()
	End Sub

	'============================================================================================================================================================================================================
	'=     BUTTON
	'============================================================================================================================================================================================================
	Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		ElseIf Txt_NmBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Nama Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_NmBarang.Focus() : Exit Sub
		ElseIf Cmb_GroupJenis.SelectedIndex = -1 Then
			MessageBox.Show("Group Jenis harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_GroupJenis.Focus() : Exit Sub
		ElseIf Cmb_SOAwal.SelectedIndex <> 0 Or Cmb_SOTujuan.SelectedIndex <> 0 Then
			If Cmb_SOAwal.SelectedIndex = Cmb_SOTujuan.SelectedIndex Then
				MessageBox.Show("Lokasi Awal dan Tujuang Tidak Boleh Sama", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_SOAwal.Focus() : Exit Sub
			End If
		End If

		If Cmb_ParamLain.SelectedIndex <> 0 Then
			If Txt_ParamLain.Text.Trim.Length = 0 Then
				MessageBox.Show("Parameter lain harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_ParamLain.Focus() : Exit Sub
			End If
		End If

		Try
			OpenConn()

			Dim SF As String = ""

			SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Transfer_Stock_Barang_Lain "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

			SF = "{N_EMI_View_Laporan_Transfer_Stock_Barang_Lain.Kode_Perusahaan} = '" & KodePerusahaan & "' "
			SF = SF & "and {N_EMI_View_Laporan_Transfer_Stock_Barang_Lain.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
			SF = SF & "{N_EMI_View_Laporan_Transfer_Stock_Barang_Lain.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

			If Not Cmb_JenisTransfer.SelectedIndex = 0 Then
				SQL = SQL & "and Jenis_Transfer = '" & Cmb_JenisTransfer.SelectedItem & "' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Stock_Barang_Lain.Jenis_Transfer} = '" & Cmb_JenisTransfer.SelectedItem & "' "
			End If

			If Not Cmb_SOAwal.SelectedIndex = 0 Then
				SQL = SQL & "and Lokasi_Awal = '" & Cmb_SOAwal.SelectedItem & "' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Stock_Barang_Lain.Lokasi_Awal} = '" & Cmb_SOAwal.SelectedItem & "' "
			End If

			If Not Cmb_SOTujuan.SelectedIndex = 0 Then
				SQL = SQL & "and Lokasi_Tujuan = '" & Cmb_SOTujuan.SelectedItem & "' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Stock_Barang_Lain.Lokasi_Tujuan} = '" & Cmb_SOTujuan.SelectedItem & "' "
			End If

			If Not Cmb_GroupJenis.SelectedIndex = 0 Then
				SQL = SQL & "and Kode_Group_Jenis = '" & Cmb_GroupJenis.SelectedItem & "' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Stock_Barang_Lain.Kode_Group_Jenis} = '" & Cmb_GroupJenis.SelectedItem & "' "
			End If

			If Not Txt_KdBarang.Text = "--- SELURUH ---" Then
				SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
				SF = SF & "And {N_EMI_View_Laporan_Transfer_Stock_Barang_Lain.Kode_Barang} = '" & Txt_KdBarang.Text & "'"
			End If

			If Not Cmb_ParamLain.SelectedIndex = 0 Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
				If Not Strings.Right(UCase(SF), 6) = "WHERE " Then SF = SF & "AND "

				SQL = SQL & arrParamLain.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
				SF = SF & arrParamLainSF.Item(Cmb_ParamLain.SelectedIndex) & " like '*" & Trim(Txt_ParamLain.Text) & "*' "
			End If
			Using DS = BindingTrans(SQL)
				With DS.Tables("MyTable")
					If .Rows.Count <> 0 Then

						Dim CrDoc As New N_EMI_CR_Laporan_Transfer_Stock_Barang_Lain

						CrDoc.SetDataSource(DS)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
																			Format(Tgl2.Value, "dd/MMM/yyyy")
						CrDoc.RecordSelectionFormula = SF

						With A_Place_For_Printing2
							.Text = JudulForm
							.CrystalReportViewer1.ReportSource = CrDoc
							.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
							.Refresh()
							.Show()
						End With
					Else

						CloseConn()
						MessageBox.Show("Tranfer Stock Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

	Private Sub N_EMI_Laporan_Transfer_Stock_Barang_Lain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

End Class
Public Class N_EMI_Laporan_Stock_Quality
	Dim arrJenis, arrKualitas, arrLokasi As New ArrayList

	Dim switchAutoComplete As Boolean = False

	Private Sub N_EMI_View_Laporan_Stock_Quality_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Lv_Jenis.Columns.Clear()
		Lv_Jenis.Columns.Add("Id Group jenis", 130, HorizontalAlignment.Left)
		Lv_Jenis.Columns.Add("Kode Group jenis", 110, HorizontalAlignment.Center)
		Lv_Jenis.View = View.Details

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details

		Try
			OpenConn()

			Cmb_Tgl.Items.Clear()
			Cmb_Tgl.Items.Add("Tanggal Masuk")
			Cmb_Tgl.Items.Add("Tanggal Expired")
			Cmb_Tgl.SelectedIndex = 0

			Cmb_Kualitas.Items.Clear()
			arrKualitas.Clear()

			Cmb_Kualitas.Items.Add(OpsiSeluruh)
			arrKualitas.Add(OpsiSeluruh)

			SQL = "SELECT Kode_Warna, Keterangan "
			SQL &= "FROM EMI_Master_Warna "
			SQL &= $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= "ORDER BY Keterangan "

			Using Dr = OpenTrans(SQL)
				While Dr.Read
					Cmb_Kualitas.Items.Add(Dr("Keterangan").ToString)
					arrKualitas.Add(Dr("Kode_Warna").ToString)
				End While
			End Using

			Cmb_Lokasi.Items.Clear()
			arrLokasi.Clear()

			Cmb_Lokasi.Items.Add(OpsiSeluruh)
			arrLokasi.Add(OpsiSeluruh)
			SQL = "select Kode_Stock_Owner from stock_owner_gudang "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"order by Kode_Stock_Owner "

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Lokasi.Items.Add(Dr("Kode_Stock_Owner"))
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
		Tgl1.Value = Date.Today
		Tgl2.Value = Date.Today

		Cmb_Tgl.SelectedIndex = 0
		Cmb_Lokasi.SelectedIndex = 0
		Cmb_Kualitas.SelectedIndex = 0

		switchAutoComplete = True

		txt_IdJenis.Text = OpsiSeluruh
		Txt_Jenis.Text = OpsiSeluruh
		Txt_KdBarang.Text = OpsiSeluruh
		Txt_NmBarang.Text = OpsiSeluruh

		switchAutoComplete = False

		Tgl1.Focus()
	End Sub

	Private Sub Txt_Jenis_TextChanged(sender As Object, e As EventArgs) Handles Txt_Jenis.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(625, 307)
			Lv_Jenis.Visible = False
			Lv_Jenis.Location = New Point(635, 161)
			Txt_Jenis.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(625, 343)
			Lv_Jenis.Visible = True
			Lv_Jenis.Location = New Point(88, 183)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Jenis.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "SELECT Id_Group_Jenis, Kode_Group_Jenis " &
			  "FROM EMI_Group_Jenis " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND Kode_Group_Jenis LIKE '%{Txt_Jenis.Text.Trim}%' " &
			  "ORDER BY Kode_Group_Jenis "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Item As New ListViewItem(Dr("Id_Group_Jenis").ToString)

						Item.SubItems.Add(Dr("Kode_Group_Jenis").ToString)

						Lv_Jenis.Items.Add(Item)
					Loop While Dr.Read
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'If OpsiSeluruh.ToUpper.StartsWith(Txt_Jenis.Text.Trim.ToUpper) Then

		'	Lv_Jenis.Items.Clear()

		'	Dim Lv As ListViewItem
		'	Lv = Lv_Jenis.Items.Add(OpsiSeluruh)
		'	Lv.SubItems.Add(OpsiSeluruh)
		'	Lv.SubItems.Add(OpsiSeluruh)

		'	Lv.Selected = True

		'	Lv_Jenis.Visible = True

		'	Exit Sub

		'End If

	End Sub

	Private Sub Txt_Jenis_Leave(sender As Object, e As EventArgs) Handles Txt_Jenis.Leave
		If Txt_Jenis.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Jenis.Focused = True Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If Not Txt_Jenis.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL = "SELECT Id_Group_Jenis, Kode_Group_Jenis " &
			  "FROM EMI_Group_Jenis " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND Kode_Group_Jenis = '{Txt_Jenis.Text.Trim}' "

				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Do
							Txt_Jenis.Text = Dr("Kode_Group_Jenis").ToString
							Txt_KdBarang.Focus()
						Loop While Dr.Read
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Group Jenis tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_Jenis.Text = ""
						Txt_Jenis.Focus()
						Exit Sub
					End If
				End Using
			Else
				Txt_KdBarang.Focus()
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
	End Sub

	Private Sub Lv_Jenis_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Jenis.DoubleClick
		If Lv_Jenis.Items.Count = 0 Or Lv_Jenis.FocusedItem.Index = -1 Then Exit Sub

		Dim Id_Jenis As String = Lv_Jenis.FocusedItem.SubItems(0).Text
		Dim Jenis_Kemasan As String = Lv_Jenis.FocusedItem.SubItems(1).Text

		switchAutoComplete = True
		txt_IdJenis.Text = Id_Jenis
		Txt_Jenis.Text = Jenis_Kemasan
		switchAutoComplete = False

		Me.Size = New Size(625, 309)
		Lv_Jenis.Visible = False
		Lv_Jenis.Location = New Point(635, 161)

		Txt_KdBarang.Focus()
	End Sub

	Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(625, 309)
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(625, 376)
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(88, 215)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			SQL = "SELECT DISTINCT Kode_Barang, Nama " &
			  "FROM Barang " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND Nama LIKE '%{Txt_NmBarang.Text.Trim}%' " &
			  "ORDER BY Nama "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Lv = Lv_Barang.Items.Add(Dr("Kode_Barang").ToString)
						Lv.SubItems.Add(Dr("Nama").ToString)
					Loop While Dr.Read

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Then Exit Sub
		If Lv_Barang.FocusedItem Is Nothing Then Exit Sub

		Dim KodeBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim NamaBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

		switchAutoComplete = True

		Txt_KdBarang.Text = KodeBarang
		Txt_NmBarang.Text = NamaBarang

		switchAutoComplete = False

		Me.Size = New Size(625, 309)
		Lv_Barang.Visible = False
		Lv_Barang.Location = New Point(635, 161)

		BtnCetak.Focus()
	End Sub

	Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(625, 307)
			Lv_Barang.Visible = False
			Exit Sub
		Else
			Me.Size = New Size(625, 376)
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(88, 215)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			SQL = "SELECT DISTINCT Kode_Barang, Nama " &
			  "FROM Barang " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND Kode_Barang LIKE '%{Txt_KdBarang.Text.Trim}%' " &
			  "ORDER BY Kode_Barang "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Lv = Lv_Barang.Items.Add(Dr("Kode_Barang").ToString)
						Lv.SubItems.Add(Dr("Nama").ToString)
					Loop While Dr.Read
				End If
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
			Cmd.Transaction = Cn.BeginTransaction

			If Not Txt_KdBarang.Text = OpsiSeluruh Then

				SQL = "SELECT Kode_Barang, Nama "
				SQL &= "FROM Barang "
				SQL &= $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"AND Kode_Barang = '{Txt_KdBarang.Text.Trim}' "

				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Do
							switchAutoComplete = True
							Txt_KdBarang.Text = Dr("Kode_Barang").ToString
							Txt_NmBarang.Text = Dr("Nama").ToString
							switchAutoComplete = False
							Cmb_Kualitas.Focus()
						Loop While Dr.Read
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()

						MessageBox.Show("Barang tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_KdBarang.Text = ""
						Txt_NmBarang.Text = ""
						Txt_KdBarang.Focus()
						Exit Sub
					End If
				End Using
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

	End Sub

	Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_Jenis.Text.Trim.Length = 0 Then
			MessageBox.Show("Jenis Kemasan harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Jenis.Focus() : Exit Sub
		End If
		If Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		End If

		Try

			OpenConn()

			Dim SF As String = ""

			SQL = "SELECT Kode_Perusahaan " &
			  "FROM N_EMI_View_Laporan_Stock_Quality " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "

			If Cmb_Tgl.SelectedIndex = 0 Then

				SQL &= $"AND Tanggal_Masuk >= '{Format(Tgl1.Value, "yyyy-MM-dd")}' "
				SQL &= $"AND Tanggal_Masuk <= '{Format(Tgl2.Value, "yyyy-MM-dd")}' "

				SF = "{N_EMI_View_Laporan_Stock_Quality.Kode_Perusahaan} = '" & KodePerusahaan & "' "
				SF &= "And {N_EMI_View_Laporan_Stock_Quality.Tanggal_Masuk} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# "
				SF &= "And {N_EMI_View_Laporan_Stock_Quality.Tanggal_Masuk} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "
			Else

				SQL &= $"AND Tanggal_Expired >= '{Format(Tgl1.Value, "yyyy-MM-dd")}' "
				SQL &= $"AND Tanggal_Expired <= '{Format(Tgl2.Value, "yyyy-MM-dd")}' "

				SF = "{N_EMI_View_Laporan_Stock_Quality.Kode_Perusahaan} = '" & KodePerusahaan & "' "
				SF &= "And {N_EMI_View_Laporan_Stock_Quality.Tanggal_Expired} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# "
				SF &= "And {N_EMI_View_Laporan_Stock_Quality.Tanggal_Expired} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

			End If

			' Lokasi
			If Cmb_Lokasi.SelectedIndex <> 0 Then

				SQL &= $"AND Lokasi_Gudang = '{Cmb_Lokasi.Text}' "

				SF &= "And {N_EMI_View_Laporan_Stock_Quality.Lokasi_Gudang} = '" &
				  Cmb_Lokasi.Text & "' "

			End If

			' Jenis
			If Not Txt_Jenis.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL &= $"AND Id_Group_Jenis = '{txt_IdJenis.Text}' "

				SF &= "And {N_EMI_View_Laporan_Stock_Quality.Id_Group_Jenis} = " &
				  txt_IdJenis.Text & " "

			End If

			' Barang
			If Not Txt_KdBarang.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL &= $"AND Kode_Barang = '{Txt_KdBarang.Text.Trim}' "

				SF &= "And {N_EMI_View_Laporan_Stock_Quality.Kode_Barang} = '" &
				  Txt_KdBarang.Text.Trim & "' "

			End If

			' Kualitas
			If Cmb_Kualitas.SelectedIndex <> 0 Then

				SQL &= $"AND Kode_Warna = '{arrKualitas(Cmb_Kualitas.SelectedIndex)}' "

				SF &= "And {N_EMI_View_Laporan_Stock_Quality.Kode_Warna} = '" &
				  arrKualitas(Cmb_Kualitas.SelectedIndex) & "' "

			End If

			Using DS = BindingTrans(SQL)

				With DS.Tables("MyTable")

					If .Rows.Count <> 0 Then

						Dim CrDoc As New N_EMI_CR_Laporan_Stock_Quality

						CrDoc.SetDataSource(DS)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)

						CrDoc.SummaryInfo.ReportTitle =
						"Periode : " &
						Format(Tgl1.Value, "dd/MMM/yyyy") &
						" s/d " &
						Format(Tgl2.Value, "dd/MMM/yyyy")

						CrDoc.RecordSelectionFormula = SF

						With A_Place_For_Printing2

							.Text = "Laporan Stock Quality"

							.CrystalReportViewer1.ReportSource = CrDoc

							.CrystalReportViewer1.ToolPanelView =
							CrystalDecisions.Windows.Forms.ToolPanelViewType.None

							.Refresh()
							.Show()

						End With
					Else

						CloseConn()

						MessageBox.Show("Data Stock Quality Tidak Ditemukan",
									Judul,
									MessageBoxButtons.OK,
									MessageBoxIcon.Exclamation)

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

	Private Sub Cmb_Tgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tgl.KeyPress
		If e.KeyChar = Chr(13) Then
			Tgl1.Focus()
		End If
	End Sub

	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then
			Tgl2.Focus()
		End If
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Lokasi.Focus()
			Cmb_Lokasi.DroppedDown = True
		End If
	End Sub

	Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Kualitas.Focus()
			Cmb_Kualitas.DroppedDown = True
		End If
	End Sub

	Private Sub Cmb_Kualitas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Kualitas.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Kualitas.SelectedIndex = 0 Then
				Cmb_Kualitas.Focus()
				Txt_Jenis.Enabled = False
			Else
				Txt_Jenis.Enabled = True
				Txt_Jenis.Focus()
			End If

			switchAutoComplete = True
			Txt_Jenis.Text = OpsiSeluruh
			switchAutoComplete = False
		End If
	End Sub

	Private Sub Txt_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Jenis.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_Jenis_Leave(Txt_NmBarang, e)

			Me.Size = New Size(651, 359)
			Lv_Jenis.Visible = False

			'Txt_Jenis.Focus()
		End If
	End Sub

	Private Sub Txt_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Jenis.KeyDown
		If e.KeyCode = Keys.Right Then Lv_Jenis.Focus()
	End Sub

	Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then
			e.SuppressKeyPress = True
			Lv_Barang.Focus()
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
		'If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Lv_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Jenis.KeyDown
		' Menggunakan Enter (bukan Keys.Down) agar navigasi panah atas-bawah di listview tidak rusak
		'If e.KeyCode = Keys.Enter Then
		'	e.Handled = True
		'	Lv_Jenis_DoubleClick(sender, e)
		'End If
	End Sub

	Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
		Exit Sub
		If e.KeyChar = Chr(13) Then
			BtnCetak.Focus()
		End If
	End Sub

End Class
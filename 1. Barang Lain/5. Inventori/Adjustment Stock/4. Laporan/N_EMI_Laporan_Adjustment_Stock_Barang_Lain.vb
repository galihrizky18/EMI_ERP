Public Class N_EMI_Laporan_Adjustment_Stock_Barang_Lain

	Dim Switch_Auto_Complete As Boolean = False

	Dim arrLain As New ArrayList

	Private Sub N_EMI_Laporan_Adjustment_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Try
			OpenConn()

			Cmb_KdSO.Items.Clear()
			Cmb_KdSO.Items.Add(OpsiSeluruh)
			SQL = $"
				select Kode_Stock_Owner
				from Stock_Owner_Gudang_Lain
				where Kode_Perusahaan = '{KodePerusahaan}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_KdSO.Items.Add(Dr("Kode_Stock_Owner"))
				Loop
			End Using

			Cmb_Lain.Items.Clear() : arrLain.Clear()
			Cmb_Lain.Items.Add(OpsiSeluruh) : arrLain.Add(OpsiSeluruh)
			Cmb_Lain.Items.Add("Jenis") : arrLain.Add("Jenis_Adjustment")
			Cmb_Lain.Items.Add("Keterangan") : arrLain.Add("Keterangan")
			Cmb_Lain.Items.Add("Barcode") : arrLain.Add("Barcode")

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Lv_Faktur.Columns.Clear()
		Lv_Faktur.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
		Lv_Faktur.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
		Lv_Faktur.Columns.Add("Keterangan", 230, HorizontalAlignment.Left)
		Lv_Faktur.View = View.Details

		Lv_Faktur.Visible = False
		Lv_Faktur.Location = New Point(151, 187)

		Kosong()
	End Sub

	Private Sub Kosong()

		Me.Size = New Point(686, 324)

		Tgl1.Value = Date.Now
		Tgl2.Value = Date.Now
		Cmb_KdSO.SelectedIndex = 0
		Cmb_Lain.SelectedIndex = 0
		Txt_Lain.Text = OpsiSeluruh

		Switch_Auto_Complete = True
		Txt_No_Faktur.Text = OpsiSeluruh
		Txt_Ket_Faktur.Text = OpsiSeluruh
		Switch_Auto_Complete = False

		Rd_Status_Seluruh.Checked = True
		Tgl1.Focus()

	End Sub

	Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Cmb_KdSO.SelectedIndex = -1 Then
			MessageBox.Show("Kd So Harus Dipilih Dahulu!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_KdSO.Focus() : Exit Sub
		ElseIf Txt_No_Faktur.Text.Trim.Length = 0 Then
			MessageBox.Show("No Faktur harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_No_Faktur.Focus() : Exit Sub

		ElseIf Cmb_Lain.SelectedIndex <> 0 Then
			If Txt_Lain.Text.Trim.Length = 0 Then
				MessageBox.Show("value Lain harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_Lain.Focus() : Exit Sub
			End If
		End If

		Try
			OpenConn()

			Dim Filter As String = ""
			Dim SF As String = ""

			SF = "{N_EMI_View_Laporan_Adjustment_Stock_Barang_Lain.Kode_Perusahaan} = '" & KodePerusahaan & "' "
			SF = SF & "and {N_EMI_View_Laporan_Adjustment_Stock_Barang_Lain.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
			SF = SF & "{N_EMI_View_Laporan_Adjustment_Stock_Barang_Lain.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

			If Not Rd_Status_Seluruh.Checked Then

				If Rd_Status_Validasi.Checked Then
					Filter &= "and Flag_Validation_Accounting = 'Y' "
					SF = SF & "And {N_EMI_View_Laporan_Adjustment_Stock_Barang_Lain.Flag_Validation_Accounting} = 'Y' "
				ElseIf Rd_Status_Belum_Validasi.Checked Then
					Filter &= "and Flag_Validation_Accounting is null "
					SF = SF & "And IsNull({N_EMI_View_Laporan_Adjustment_Stock_Barang_Lain.Flag_Validation_Accounting}) "
				End If

			End If

			If Cmb_KdSO.SelectedIndex <> 0 Then
				Filter &= "and Kode_Stock_Owner = '" & Cmb_KdSO.Text.Trim & "' "
				SF = SF & "And {N_EMI_View_Laporan_Adjustment_Stock_Barang_Lain.Kode_Stock_Owner} = '" & Cmb_KdSO.Text.Trim & "' "
			End If

			If Not Txt_No_Faktur.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
				SQL = SQL & "and No_Faktur = '" & Txt_No_Faktur.Text.Trim & "' "
				SF = SF & "And {N_EMI_View_Laporan_Adjustment_Stock_Barang_Lain.No_Faktur} = '" & Txt_No_Faktur.Text.Trim & "'"
			End If

			If Cmb_Lain.SelectedIndex <> 0 Then
				If Txt_Lain.Text.ToUpper.Trim <> OpsiSeluruh.ToUpper.Trim Then
					Dim Kolom As String = arrLain(Cmb_Lain.SelectedIndex).ToString()
					SQL = SQL & "and " & Kolom & " = '" & Txt_Lain.Text & "' "
					SF = SF & "And {N_EMI_View_Laporan_Adjustment_Stock_Barang_Lain." & Kolom & "} = '" & Txt_Lain.Text & "'"
				End If
			End If

			SQL = $"
				select kode_perusahaan
				from N_EMI_View_Laporan_Adjustment_Stock_Barang_Lain
				where kode_perusahaan = '{KodePerusahaan}'
				and Tanggal BETWEEN '{Format(Tgl1.Value, "yyyy-MM-dd")}' and '{Format(Tgl2.Value, "yyyy-MM-dd")}'
				{Filter}
			"
			Using DS = BindingTrans(SQL)
				With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New N_EMI_CR_Laporan_Adjustment_Stock_Barang_Lain

                        CrDoc.SetDataSource(DS)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
																			Format(Tgl2.Value, "dd/MMM/yyyy")
						CrDoc.RecordSelectionFormula = SF

						With A_Place_For_Printing2
							.Text = "Laporan Adjustment Stock"
							.CrystalReportViewer1.ReportSource = CrDoc
							.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
							.Refresh()
							.Show()
						End With
					Else

						CloseConn()
						MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

	Private Sub Txt_No_Faktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Faktur.TextChanged
		If Switch_Auto_Complete Then Exit Sub

		If Txt_No_Faktur.Text.Trim.Length = 0 Then
			Me.Size = New Size(686, 324)
			Lv_Faktur.Visible = False
			Txt_No_Faktur.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(686, 400)
			Lv_Faktur.Visible = True
		End If

		Try
			OpenConn()

			Lv_Faktur.BeginUpdate()
			Lv_Faktur.Items.Clear()
			Dim Lv As ListViewItem
			Lv = Lv_Faktur.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = $"
				select No_Faktur, Tanggal, Keterangan
				from Emi_Adjustment_Stock_Barang_Lain
				where Status is null
				and Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur like '%{Txt_No_Faktur.Text.Trim}%'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Faktur.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal")) = "", "-", Format(Dr("Tanggal"), "dd MMM yyyy")))
					Lv.SubItems.Add(Dr("Keterangan"))
				Loop
			End Using
			Lv_Faktur.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_No_Faktur_Leave(sender As Object, e As EventArgs) Handles Txt_No_Faktur.Leave
		If Txt_No_Faktur.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Faktur.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_No_Faktur.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL = $"
					select No_Faktur, Tanggal, Keterangan
					from Emi_Adjustment_Stock_Barang_Lain
					where Status is null
					and Kode_Perusahaan = '{KodePerusahaan}'
					and No_Faktur = '{Txt_No_Faktur.Text.Trim}'
				"
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_No_Faktur.Text = Dr("No_Faktur")
						Cmb_Lain.DroppedDown = True
						Cmb_Lain.Focus()
					Else
						MessageBox.Show("No Faktur Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_No_Faktur.Text = ""
						Txt_No_Faktur.Focus()
					End If

					Me.Size = New Size(686, 324)
					Lv_Faktur.Visible = False
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

	Private Sub Cmb_Lain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lain.SelectedIndexChanged
		If Cmb_Lain.Items.Count = 0 Or Cmb_Lain.SelectedIndex = -1 Then Exit Sub

		If Cmb_Lain.SelectedIndex = 0 Then
			Txt_Lain.Enabled = False
			Txt_Lain.Text = OpsiSeluruh
		Else
			Txt_Lain.Enabled = True
			Txt_Lain.Text = ""
		End If
	End Sub

	Private Sub Lv_Faktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Faktur.DoubleClick
		If Lv_Faktur.Items.Count = 0 Or Lv_Faktur.FocusedItem.Index = -1 Then Exit Sub

		Dim NoFaktur As String = Lv_Faktur.FocusedItem.SubItems(0).Text
		Dim Ket As String = Lv_Faktur.FocusedItem.SubItems(2).Text

		Switch_Auto_Complete = True
		Txt_No_Faktur.Text = NoFaktur
		Txt_Ket_Faktur.Text = Ket
		Switch_Auto_Complete = False

		Me.Size = New Size(686, 324)
		Lv_Faktur.Visible = False

		Cmb_Lain.DroppedDown = True
		Cmb_Lain.Focus()
	End Sub

	Private Sub Lv_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Faktur.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Faktur_DoubleClick(Lv_Faktur, e)
		End If
	End Sub

	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then Tgl2.Focus()
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then
			Rd_Status_Seluruh.Focus()
		End If
	End Sub

	Private Sub Rd_Status_Seluruh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Rd_Status_Seluruh.KeyPress, Rd_Status_Validasi.KeyPress, Rd_Status_Belum_Validasi.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_KdSO.DroppedDown = True
			Cmb_KdSO.Focus()
		End If
	End Sub

	Private Sub Cmb_KdSO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KdSO.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_No_Faktur.Focus()
		End If
	End Sub

	Private Sub Txt_No_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Faktur.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_No_Faktur.Text.Trim.Length = 0 Then Txt_No_Faktur.Focus()
			Txt_No_Faktur_Leave(Txt_No_Faktur, e)

			Me.Size = New Size(686, 324)
			Lv_Faktur.Visible = False

		End If
	End Sub

	Private Sub Txt_No_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Faktur.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Faktur.Focus()
	End Sub

	Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Lain.SelectedIndex = 0 Then
				BtnCetak.Focus()
			Else
				Txt_Lain.Focus()
			End If
		End If
	End Sub

	Private Sub Txt_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Lain.KeyPress
		If e.KeyChar = Chr(13) Then
			BtnCetak.Focus()
		End If
	End Sub

	Protected Overrides Sub WndProc(ByRef m As Message)
		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
	End Sub

End Class
Public Class SD_Pilih_Barang
	Public filter_tambahan, filter_kdSupplier As String
	Public asal As String
	Dim arrcari As New ArrayList
	Dim Jenis = "Tampil_Barang"

	Private Sub kosong()
		TxtPilihBarang_KodeBarang.Text = ""
		TxtPilihBarang_NamaBarang.Text = ""
		TxtPilihBarang_Satuan.Text = ""

		CmbPilihBarang_Satuan.Items.Clear()

		TxtPilihBarang_KodeBarang.Focus()

	End Sub

	Public Sub lokasi()
		Try
			OpenConn()

			CmbPilihBarang_Lokasi.Items.Clear()

			SQL = "select Kode_Stock_Owner from view_lokasi_stock where Aktif='Y' "
			SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbPilihBarang_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try

	End Sub

	Private Sub SD_Pilih_Barang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub SD_Pilih_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Try
			OpenConn()

			Base_Language.Get_Languages_Global(Bahasa_Pilihan)

			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			LblPilihBarang_Judul.Text = Base_Language.Lang_TampilBarang_Judul
			LblPilihBarang_Lokasi.Text = Base_Language.Lang_Global_Lokasi
			LblPilihBarang_KodeBarang.Text = Base_Language.Lang_Global_KodeBarang
			LblPilihBarang_NamaBarang.Text = Base_Language.Lang_Global_NamaBarang
			LblPilihBarang_Satuan.Text = Base_Language.Lang_Global_Satuan

			BtnPilihBarang_Simpan.Text = Base_Language.Lang_Global_Simpan
			BtnPilihBarang_Refresh.Text = Base_Language.Lang_Global_Refresh

			kosong()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try

		LvPilihBarang_DataBarang.Visible = False
		LvPilihBarang_DataBarang.Location = New Point(163, 124)

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
		kosong()
	End Sub

	Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then TxtPilihBarang_NamaBarang.Focus()
	End Sub

	Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then BtnPilihBarang_Simpan.Focus()
	End Sub

	Private Sub TxtKode_TextChanged(sender As Object, e As EventArgs) Handles TxtPilihBarang_KodeBarang.TextChanged
		If TxtPilihBarang_KodeBarang.Text.Length >= 3 Then
			If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
				LvPilihBarang_DataBarang.Visible = False : Exit Sub
			Else
				LvPilihBarang_DataBarang.Visible = True
			End If

			If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
				LvPilihBarang_DataBarang.Visible = False : Exit Sub
			Else
				LvPilihBarang_DataBarang.Visible = True
			End If

			Try
				OpenConn()

				LvPilihBarang_DataBarang.Items.Clear()
				Dim Lvw As ListViewItem

				SQL = "select a.Kode_Barang, a.Kode_Stock_Owner,a.Nama, a.Satuan, b.Kode_Group_Jenis "
				SQL = SQL & "from barang a, EMI_Group_Jenis b where "
				SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis "
				SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & CmbPilihBarang_Lokasi.Text & "' "
				SQL = SQL & "and Nama like '%" & TxtPilihBarang_KodeBarang.Text & "%' and aktif = 'Y' " & filter_tambahan & " "
				Using dr = OpenTrans(SQL)
					Do While dr.Read
						Lvw = LvPilihBarang_DataBarang.Items.Add(dr("kode_stock_owner"))
						Lvw.SubItems.Add(dr("kode_barang"))
						Lvw.SubItems.Add(dr("Nama"))
						Lvw.SubItems.Add(dr("satuan"))
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		Else
			LvPilihBarang_DataBarang.Visible = False
		End If
	End Sub

	Private Sub TxtKode_Leave(sender As Object, e As EventArgs) Handles TxtPilihBarang_KodeBarang.Leave
		If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then Exit Sub
		If LvPilihBarang_DataBarang.Focused = True Then Exit Sub

		If CmbPilihBarang_Lokasi.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Lokasi & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtPilihBarang_KodeBarang.Text = "" : CmbPilihBarang_Lokasi.Focus()
			Exit Sub
		End If

		Try
			OpenConn()

			SQL = "select a.Kode_Barang, a.Kode_Stock_Owner,a.Nama, a.Satuan, b.Kode_Group_Jenis "
			SQL = SQL & "from barang a, EMI_Group_Jenis b where "
			SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & CmbPilihBarang_Lokasi.Text & "' "
			SQL = SQL & "and kode_barang = '" & Trim(TxtPilihBarang_KodeBarang.Text) & "' and aktif = 'Y' " & filter_tambahan & " "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					TxtPilihBarang_KodeBarang.Text = dr("kode_barang")
					TxtPilihBarang_NamaBarang.Text = dr("nama")
					TxtPilihBarang_Satuan.Text = dr("Satuan")
					dr.Close()
					Dim satuan As String = ""
					CmbPilihBarang_Satuan.Items.Clear()
					SQL = "select Satuan, Flag_Tampil_Display from barang_Detail_Satuan where Kode_Barang= '" & Trim(TxtPilihBarang_KodeBarang.Text) & "' "
					Using dr2 = OpenTrans(SQL)
						Do While dr2.Read

							CmbPilihBarang_Satuan.Items.Add(dr2("satuan"))

							If General_Class.CekNULL(dr2("Flag_Tampil_Display")) = "Y" Then
								satuan = dr2("satuan")
							End If
						Loop
					End Using

					CmbPilihBarang_Satuan.Text = satuan
					CmbPilihBarang_Satuan.Enabled = False

					BtnPilihBarang_Simpan.Focus()

					LvPilihBarang_DataBarang.Visible = False
				Else
					TxtPilihBarang_KodeBarang.Text = ""
					TxtPilihBarang_NamaBarang.Text = ""
					TxtPilihBarang_Satuan.Text = ""
					CmbPilihBarang_Satuan.Items.Clear()
					TxtPilihBarang_KodeBarang.Focus()
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub TxtKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPilihBarang_KodeBarang.KeyPress
		If e.KeyChar = Chr(13) Then BtnPilihBarang_Simpan.Focus()

	End Sub

	Private Sub TxtKode_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPilihBarang_KodeBarang.KeyDown
		If e.KeyCode = Keys.Down Then
			If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub
			LvPilihBarang_DataBarang.Focus()
		End If
	End Sub

	Private Sub Lv2_DoubleClick(sender As Object, e As EventArgs) Handles LvPilihBarang_DataBarang.DoubleClick
		If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub

		TxtPilihBarang_KodeBarang.Text = LvPilihBarang_DataBarang.FocusedItem.SubItems(1).Text
		TxtPilihBarang_KodeBarang.Focus() : BtnPilihBarang_Simpan.Focus()
		LvPilihBarang_DataBarang.Visible = False
	End Sub

	Private Sub Lv2_KeyDown(sender As Object, e As KeyEventArgs) Handles LvPilihBarang_DataBarang.KeyDown
		If e.KeyCode = Keys.Enter Then Lv2_DoubleClick(LvPilihBarang_DataBarang, e)
	End Sub

	Private Sub Btn_Simpan_Click_1(sender As Object, e As EventArgs) Handles BtnPilihBarang_Simpan.Click
		If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			TxtPilihBarang_KodeBarang.Focus()
			Exit Sub
		ElseIf TxtPilihBarang_NamaBarang.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_NamaBarang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			TxtPilihBarang_NamaBarang.Focus()
			Exit Sub
		ElseIf CmbPilihBarang_Lokasi.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Lokasi & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			CmbPilihBarang_Lokasi.Focus()
			Exit Sub
		ElseIf CmbPilihBarang_Satuan.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			CmbPilihBarang_Satuan.Focus()
			Exit Sub
		End If

		Dim pengali As Double = 0
		Try
			OpenConn()

			SQL = "select Nilai_Pengali from emi_satuan_detail_perhitungan "
			SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and "
			SQL = SQL & "Satuan_Awal='" & CmbPilihBarang_Satuan.Text & "' and satuan_akhir='" & TxtPilihBarang_Satuan.Text & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					pengali = dr("Nilai_Pengali")
				Else
					dr.Close()
					CloseConn()
					MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Exit Sub
				End If
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If asal = "Transaksi_Formulator" Then

			For i = 0 To ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows.Count - 1
				If ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows(i).Cells(ERP_EMI.Transaksi_Formula.cellKdBarang).Value = TxtPilihBarang_KodeBarang.Text Then
					MessageBox.Show("Barang Sudah ada")
					Exit Sub
				End If
			Next
			Dim index As Integer = ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows.Count - 1
			'Dim a As Integer = Transaksi_Formula.cellQty
			ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows.Add(1)
			ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows(index).Cells(ERP_EMI.Transaksi_Formula.cellQty).Value = ""
			ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows(index).Cells(ERP_EMI.Transaksi_Formula.cellPersentase).Value = ""
			ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows(index).Cells(ERP_EMI.Transaksi_Formula.cellQty_SatHasil).Value = ""
			ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows(index).Cells(ERP_EMI.Transaksi_Formula.cellKdBarang).Value = TxtPilihBarang_KodeBarang.Text
			ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows(index).Cells(ERP_EMI.Transaksi_Formula.cellNama).Value = TxtPilihBarang_NamaBarang.Text
			ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows(index).Cells(ERP_EMI.Transaksi_Formula.cellSatuan).Value = CmbPilihBarang_Satuan.Text
			ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows(index).Cells(ERP_EMI.Transaksi_Formula.cellPengali).Value = pengali
			ERP_EMI.Transaksi_Formula.DgvFormulator_StepFormulator.Rows(index).Cells(ERP_EMI.Transaksi_Formula.cellSatuanBarang).Value = TxtPilihBarang_Satuan.Text
			ERP_EMI.Transaksi_Formula.isi_barang = True
		ElseIf asal = "Master_Penawaran" Then

			'For index = 0 To Transaksi_Penawaran.DgvMaster_Penawaran.Rows.Count - 1
			'    If Transaksi_Penawaran.DgvMaster_Penawaran.Rows(index).Cells(Transaksi_Penawaran.cellKdBrg).Value = TxtPilihBarang_KodeBarang.Text Then
			'        MessageBox.Show("Barang Sudah ada")
			'        Exit Sub
			'    End If
			'Next

			'Dim rows As Double = Transaksi_Penawaran.DgvMaster_Penawaran.Rows.Count
			'Transaksi_Penawaran.DgvMaster_Penawaran.Rows.Add(1)
			'Transaksi_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Transaksi_Penawaran.cellKdBrg).Value = TxtPilihBarang_KodeBarang.Text
			'Transaksi_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Transaksi_Penawaran.cellNmBrg).Value = TxtPilihBarang_NamaBarang.Text

			Dim dgvcc As DataGridViewComboBoxCell
			'dgvcc = Transaksi_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Transaksi_Penawaran.cellSatuan)
			dgvcc.Items.Clear()

			Try
				OpenConn()

				SQL = "select satuan from barang_detail_Satuan where Kode_Barang ='" & TxtPilihBarang_KodeBarang.Text & "'  and Kode_Perusahaan='" & KodePerusahaan & "' "
				Using dr2 = OpenTrans(SQL)
					Do While dr2.Read
						dgvcc.Items.Add(dr2("satuan"))
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			'JANGAN LUPA UNCOMMENT

			'Transaksi_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Transaksi_Penawaran.cellSatuan).Value = CmbPilihBarang_Satuan.Text
			'Transaksi_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Transaksi_Penawaran.cellSatuan).ReadOnly = False

			'Transaksi_Penawaran.Lbl_GetKdBrg.Text = TxtPilihBarang_KodeBarang.Text
			'Transaksi_Penawaran.Lbl_NmBrg.Text = TxtPilihBarang_NamaBarang.Text
			'Transaksi_Penawaran.Lbl_SatuanBrg.Text = CmbPilihBarang_Satuan.Text
		Else
			MessageBox.Show(Base_Language.Lang_Global_FormAsal & " . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If
		Me.Close()
	End Sub

	Private Sub Btn_Refresh_Click_1(sender As Object, e As EventArgs) Handles BtnPilihBarang_Refresh.Click
		kosong()
	End Sub

	Private Sub CmbPilihBarang_Satuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPilihBarang_Satuan.KeyPress
		If e.KeyChar = Chr(13) Then BtnPilihBarang_Simpan.Focus()
	End Sub

End Class
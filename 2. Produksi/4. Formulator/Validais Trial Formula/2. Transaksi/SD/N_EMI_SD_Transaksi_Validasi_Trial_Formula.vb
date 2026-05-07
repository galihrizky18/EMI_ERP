Public Class N_EMI_SD_Transaksi_Validasi_Trial_Formula

	Dim IsFirst As Boolean = True

	Dim Size_NormalForm As Point = New Point(574, 311)
	Dim Size_NormalGroupBox As Point = New Point(516, 65)
	Dim Location_LabelSusunan As Point = New Point(15, 29)
	Dim Location_CmbSusunan As Point = New Point(131, 27)
	Dim Location_BtnSimpan As Point = New Point(22, 220)
	Dim Location_BtnRefresh As Point = New Point(198, 220)

	Dim Size_NormalForm_1 As Point = New Point(574, 342)
	Dim Size_NormalGroupBox_1 As Point = New Point(516, 94)
	Dim Location_LabelSusunan_1 As Point = New Point(15, 59)
	Dim Location_CmbSusunan_1 As Point = New Point(131, 57)
	Dim Location_BtnSimpan_1 As Point = New Point(22, 250)
	Dim Location_BtnRefresh_1 As Point = New Point(198, 250)

	Private Sub N_EMI_SD_Transaksi_Validasi_Formula_Main_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Try
			OpenConn()

			Cmb_Susuan.Items.Clear() : Cmb_Susuan_Awal.Items.Clear()
			SQL = $"
				select Kode_Hierarki
				from N_EMI_Master_Hierarki_Formula
				where Kode_Perusahaan = '{KodePerusahaan}'
				order by Urutan
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Susuan.Items.Add(Dr("Kode_Hierarki"))
					Cmb_Susuan_Awal.Items.Add(Dr("Kode_Hierarki"))
				Loop
			End Using

			'===================================================
			'=     CEK APAKAH FORMULA SUSUNAN PERTAMA KALI     =
			'===================================================
			SQL = $"
			select Kode_Hierarki from Emi_Transaksi_Formulator
			where Kode_Perusahaan = '{KodePerusahaan}'
			and No_Faktur = '{Txt_NoFormula.Text.Trim}'
			and Status is null
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("Kode_Hierarki")) = "" Then
						IsFirst = True

						Cmb_Susuan_Awal.Visible = False
						Label4.Visible = False

						Me.Size = Size_NormalForm
						GroupBox1.Size = Size_NormalGroupBox
						Label3.Location = Location_LabelSusunan
						Cmb_Susuan.Location = Location_CmbSusunan
						Btn_Simpan.Location = Location_BtnSimpan
						Btn_Refresh.Location = Location_BtnRefresh

						Cmb_Susuan_Awal.SelectedIndex = -1
					Else
						IsFirst = False

						Cmb_Susuan_Awal.Visible = True
						Label4.Visible = True

						Me.Size = Size_NormalForm_1
						GroupBox1.Size = Size_NormalGroupBox_1
						Label3.Location = Location_LabelSusunan_1
						Cmb_Susuan.Location = Location_CmbSusunan_1
						Btn_Simpan.Location = Location_BtnSimpan_1
						Btn_Refresh.Location = Location_BtnRefresh_1

						Cmb_Susuan_Awal.SelectedItem = Dr("Kode_Hierarki")

					End If
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show($"No Formula {Txt_NoFormula.Text.Trim} Tidak Ada Didalam Database", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
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
		Cmb_Susuan.SelectedIndex = -1

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_NoFormula.Text.Trim.Length = 0 Then
			MessageBox.Show("Terjadi Kesalahan No Formula Tidak Ditemukan, Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub

		ElseIf Cmb_Susuan.SelectedIndex = -1 Then
			MessageBox.Show("Harap Pilih Dahulu Susunan Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Susuan.DroppedDown = True
			Exit Sub
		End If

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim NoFormula As String = Txt_NoFormula.Text.Trim

			'=======================
			'=     CEK FORMULA     =
			'=======================
			SQL = "select Status, Flag_Validasi, Flag_Validasi_Main from Emi_Transaksi_Formulator "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{NoFormula}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Faktur {NoFormula} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					ElseIf General_Class.CekNULL(Dr("Flag_Validasi")) <> "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Faktur {NoFormula} Belum Divalidasi Formulator", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					ElseIf General_Class.CekNULL(Dr("Flag_Validasi_Main")) <> "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Faktur {NoFormula} Belum Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"No Faktur {NoFormula} Tidak Ditemukan pada Transaksi Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = $"update Emi_Transaksi_Formulator set Flag_Validasi_Formula_Produksi = 'Y', Kode_Hierarki = '{Cmb_Susuan.Text}' "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{NoFormula}' "
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		N_EMI_Transaksi_Validasi_Trial_Formula.kosong()
		Me.Close()

	End Sub

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

End Class
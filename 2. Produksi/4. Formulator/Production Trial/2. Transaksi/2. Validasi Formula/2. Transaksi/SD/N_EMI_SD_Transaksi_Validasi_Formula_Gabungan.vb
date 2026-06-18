Public Class N_EMI_SD_Transaksi_Validasi_Formula_Gabungan
	Public StatusDariList As String = ""
	Dim arrcari, arrIdPenanggungJawab As New ArrayList
	Dim Jenis = "Transaksi_Formulator"
	Dim FakturTrialFormula As String = ""

	Dim FlagBypassGlobal As Boolean = False
	Dim FlagBypassTrial As Boolean = False

	Private Sub N_EMI_SD_Transaksi_Validasi_Formula_Gabungan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Public Sub N_EMI_SD_Transaksi_Validasi_Formula_Gabungan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		TxtStatus.Text = StatusDariList
		FakturTrialFormula = ""

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			LblFormulator_KodeBarang.Text = Base_Language.Lang_Global_KodeBarang
			LblFormulator_NamaBarang.Text = Base_Language.Lang_Global_NamaBarang
			LblFormulator_NoFaktur.Text = Base_Language.Lang_Global_NoFaktur
			LblFormulator_PenanggungJawab.Text = Base_Language.Lang_TransFormula_PenanggungJawab
			LblFormulator_Tanggal.Text = Base_Language.Lang_Global_Tanggal
			LblFormulator_LokasiInquiry.Text = Base_Language.Lang_Global_Lokasi
			LblFormulator_Hasil.Text = Base_Language.Lang_Global_Hasil

			'=================================================
			'= LOKASI INQUIRY
			'=================================================
			CmbFormulator_LokasiInquiry.Items.Clear()
			SQL = $"
				SELECT Kode_Stock_Owner
				FROM stock_owner
				WHERE Aktif = 'Y'
				  AND kode_perusahaan = '{KodePerusahaan}'
			"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbFormulator_LokasiInquiry.Items.Add(dr("Kode_Stock_Owner"))
				Loop
			End Using
			CmbFormulator_LokasiInquiry.Text = Lokasi

			'=================================================
			'= LOKASI BARANG
			'=================================================
			CmbFormulator_LokasiBarang.Items.Clear()
			SQL = $"
				SELECT Kode_Stock_Owner
				FROM view_lokasi_Stock
				WHERE Aktif = 'Y'
				  AND kode_perusahaan = '{KodePerusahaan}'
			"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbFormulator_LokasiBarang.Items.Add(dr("Kode_Stock_Owner"))
				Loop
			End Using

			'=================================================
			'= GUDANG DEFAULT
			'=================================================
			Dim lokasi_gudang As String = ""
			SQL = $"
				SELECT Kode_Stock_Owner_Gudang
				FROM Binding_Lokasi_Gudang
				WHERE Kode_Perusahaan = '{KodePerusahaan}' AND Kode_Stock_Owner = '{Lokasi}'
				  AND Gudang_Default = 'Y'
			"
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					lokasi_gudang = General_Class.CekNULL(dr("Kode_Stock_Owner_Gudang"))
				End If
			End Using
			CmbFormulator_LokasiBarang.Text = lokasi_gudang

			'=================================================
			'= CEK BYPASS GLOBAL
			'=================================================
			SQL = $"
				SELECT Flag_Bypass_Trial
				FROM Init
				WHERE Kode_Perusahaan = '{KodePerusahaan}'
			"
			Using dr = OpenTrans(SQL)
				If dr.Read AndAlso General_Class.CekNULL(dr("Flag_Bypass_Trial")) = "Y" Then
					BtnTrialKitchen.Enabled = False
					BtnTrialProduksi.Enabled = False
					BtnProduksi.Enabled = True

					FlagBypassGlobal = True
				End If
			End Using

			'=================================================
			'= CEK BYPASS TRIAL
			'=================================================
			If Not FlagBypassGlobal Then
				SQL = $"
					SELECT Flag_Bypass_Trial
					FROM EMI_Transaksi_Formulator
					WHERE Kode_Perusahaan = '{KodePerusahaan}'
					  AND No_Faktur = '{TxtFormulator_NoFaktur.Text.Trim}'
				"
				Using dr = OpenTrans(SQL)
					If dr.Read AndAlso General_Class.CekNULL(dr("Flag_Bypass_Trial")) = "Y" Then
						BtnTrialKitchen.Enabled = False
						BtnTrialProduksi.Enabled = False
						BtnProduksi.Enabled = True

						FlagBypassTrial = True
					End If
				End Using
			End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Private Sub Get_NoFaktur_TrialProduksi()
		Dim prefix As String = "POTP"
		Dim periode As String = Format(tgl_skg, "MMyy")
		Dim kodeAwal As String = $"{prefix}{periode}"

		Dim nomorUrut As String = General_Class.Get_Last_Number2(
			"N_EMI_Transaksi_PO_Trial_Produksi",
			"No_Transaksi",
			5,
			"Kode_Perusahaan",
			KodePerusahaan,
			"And",
			$"SUBSTRING(No_Transaksi, 1, {prefix.Length + 4})",
			kodeAwal
		)

		FakturTrialFormula = $"{kodeAwal}-{nomorUrut}"
	End Sub

	Public Sub TxtFormulator_NoFaktur_Leave(sender As Object, e As EventArgs) Handles TxtFormulator_NoFaktur.Leave
		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
			Exit Sub
		End If

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim noFaktur As String = TxtFormulator_NoFaktur.Text.Trim

			'=================================================
			'= PENANGGUNG JAWAB
			'=================================================
			CmbFormulator_PenanggungJawab.Items.Clear()
			arrIdPenanggungJawab.Clear()
			SQL = $"
				SELECT
					Nama,
					Id_Karyawan
				FROM Emi_Karyawan a
				INNER JOIN Emi_Jabatan_Internal b
					ON a.Id_Jabatan = b.Id_Jabatan
				WHERE b.Flag_Tampil_Formulator = 'Y'
				  AND a.Kode_Perusahaan = '{KodePerusahaan}'
				ORDER BY Nama
			"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbFormulator_PenanggungJawab.Items.Add(dr("Nama"))
					arrIdPenanggungJawab.Add(CInt(dr("Id_Karyawan")))
				Loop
			End Using

			'=================================================
			'= SATUAN HASIL
			'=================================================
			CmbFormulator_SatuanHasil.Items.Clear()
			SQL = $"
				SELECT
					Satuan
				FROM EMI_Satuan
				WHERE Flag_Tampil_Berat = 'Y'
				  AND Kode_Perusahaan = '{KodePerusahaan}'
			"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbFormulator_SatuanHasil.Items.Add(dr("Satuan"))
				Loop
			End Using

			'=================================================
			'= HEADER FORMULA
			'=================================================
			SQL = $"
				SELECT
					a.Tanggal,
					a.Lokasi,
					a.Kode_Stock_Owner,
					a.Kode_Barang,
					b.Nama AS Nama_Barang,
					a.Hasil,
					a.Satuan_Hasil,
					a.Penanggung_Jawab
				FROM EMI_Transaksi_Formulator a
				INNER JOIN Barang b
					ON a.Kode_Perusahaan = b.Kode_Perusahaan
				   AND a.Kode_Stock_Owner = b.Kode_Stock_Owner
				   AND a.Kode_Barang = b.Kode_Barang_Inq
				WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
				  AND a.Status IS NULL
				  AND a.No_Faktur = '{noFaktur}'
			"
			Using ds = BindingTrans(SQL)
				If ds.Tables("MyTable").Rows.Count = 0 Then
					CloseTrans()
					CloseConn()

					MessageBox.Show("Detail Bahan Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Me.Close()
					Exit Sub
				End If

				Dim row = ds.Tables("MyTable").Rows(0)

				DtpFormulator_Tanggal.Value = CDate(row("Tanggal"))
				CmbFormulator_LokasiInquiry.Text = row("Lokasi").ToString()
				CmbFormulator_LokasiBarang.Text = row("Kode_Stock_Owner").ToString()
				TxtFormulator_KodeBarang.Text = row("Kode_Barang").ToString()
				TxtFormulator_NamaBarang.Text = row("Nama_Barang").ToString()
				TxtFormulator_Hasil.Text = Format(row("Hasil"), "N4")
				CmbFormulator_SatuanHasil.SelectedItem =
				row("Satuan_Hasil").ToString().Trim()

				Dim idPenanggungJawab As Integer =
				CInt(HilangkanTanda(row("Penanggung_Jawab").ToString().Trim()))

				Dim index As Integer =
				arrIdPenanggungJawab.IndexOf(idPenanggungJawab)

				CmbFormulator_PenanggungJawab.SelectedIndex = index

			End Using

			'=================================================
			'= CEK BINDING FORMULA
			'=================================================
			SQL = $"
				SELECT
					No_Faktur
				FROM EMI_Transaksi_Formulator_Binding
				WHERE Kode_Perusahaan = '{KodePerusahaan}'
				  AND Kode_Formula = '{noFaktur}'
			"
			Using ds = BindingTrans(SQL)
				If ds.Tables("MyTable").Rows.Count = 0 Then

					CloseTrans()
					CloseConn()

					MessageBox.Show("No Faktur Formula Binding Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Me.Close()
					Exit Sub

				End If

				Txt_No_Faktur_Binding.Text = ds.Tables("MyTable").Rows(0).Item("No_Faktur").ToString()
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()

			MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try

		RefreshButtonState(TxtFormulator_NoFaktur.Text.Trim)
	End Sub

	Private Sub RefreshButtonState(noFaktur As String)
		Dim flagValidasiMain As String = ""
		Dim flagTrialKitchen As String = ""
		Dim flagTrialProduksi As String = ""
		Dim flagProduksi As String = ""

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			SQL = $"
            SELECT
				Flag_Validasi_Main,
                Flag_Lanjut_Trial_Kitchen,
                Flag_Lanjut_Trial_Produksi,
                Flag_Lanjut_Produksi
            FROM EMI_Transaksi_Formulator
            WHERE Kode_Perusahaan = '{KodePerusahaan}'
              AND No_Faktur = '{noFaktur}'
        "

			Using dr = OpenTrans(SQL)
				If dr.Read Then

					flagValidasiMain = General_Class.CekNULL(dr("Flag_Validasi_Main"))
					flagTrialKitchen = General_Class.CekNULL(dr("Flag_Lanjut_Trial_Kitchen"))
					flagTrialProduksi = General_Class.CekNULL(dr("Flag_Lanjut_Trial_Produksi"))
					flagProduksi = General_Class.CekNULL(dr("Flag_Lanjut_Produksi"))

				End If
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()

			MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try

		ApplyButtonState(
			flagValidasiMain,
			flagTrialKitchen,
			flagTrialProduksi,
			flagProduksi
		)
	End Sub

	Private Sub ApplyButtonState(
		flagValidasiMain As String,
		flagTrialKitchen As String,
		flagTrialProduksi As String,
		flagProduksi As String
	)
		Dim isRejected As Boolean =
		flagValidasiMain = "T" OrElse
		flagTrialKitchen = "T" OrElse
		flagTrialProduksi = "T" OrElse
		flagProduksi = "T"

		If isRejected Then
			BtnTrialKitchen.Enabled = False
			BtnTrialProduksi.Enabled = False
			BtnProduksi.Enabled = False
			Btn_Tolak.Enabled = False
			Exit Sub
		End If

		BtnTrialKitchen.Enabled = String.IsNullOrEmpty(flagTrialKitchen) And Not FlagBypassGlobal And Not FlagBypassTrial
		BtnTrialProduksi.Enabled = String.IsNullOrEmpty(flagTrialProduksi) And Not FlagBypassGlobal And Not FlagBypassTrial
		BtnProduksi.Enabled = String.IsNullOrEmpty(flagProduksi)

		Btn_Tolak.Enabled = True
	End Sub

	Private Sub Handle_Trial_Produksi()
		'===========================
		'=     CEK ROLE BUTTON     =
		'===========================
		If CekButtonRole("Simpan_Formula_PO_Trial_Produksi") = "T" Then
			Throw New Exception("Anda Tidak Memiliki Akses Untuk Simpan Formula Trial Produksi Ini")
		End If

		Dim noFaktur As String = TxtFormulator_NoFaktur.Text.Trim

		'=======================
		'=     CEK FORMULA     =
		'=======================
		SQL = $"
			SELECT Status
			FROM EMI_Transaksi_Formulator
			WHERE
				Kode_Perusahaan = '{KodePerusahaan}'
				AND No_Faktur = '{noFaktur}'
		"
		Using dr = OpenTrans(SQL)
			If Not dr.Read Then
				Throw New Exception($"Terjadi Kesalahan No Faktur {noFaktur} Tidak Ditemukan")
			End If

			If General_Class.CekNULL(dr("Status")) = "Y" Then
				Throw New Exception($"Terjadi Kesalahan No Faktur {noFaktur} Sudah Dibatalkan")
			End If
		End Using

		'==============================================
		'=     CEK APAKAH ADA NO FAKTUR YANG SAMA     =
		'==============================================
		SQL = $"
			SELECT TOP 1 1
			FROM N_EMI_Transaksi_PO_Trial_Produksi
			WHERE
				Kode_Perusahaan = '{KodePerusahaan}'
				AND No_Transaksi = '{FakturTrialFormula.Trim}'
		"
		Using dr = OpenTrans(SQL)
			If dr.Read Then
				Throw New Exception("Terjadi Kesalahan, No Faktur Sudah Ada Harap Ulangi Transaksi")
			End If
		End Using

		'=======================
		'=     SIMPAN DATA     =
		'=======================
		SQL = $"
			INSERT INTO N_EMI_Transaksi_PO_Trial_Produksi
			(
				Kode_Perusahaan,
				No_Transaksi,
				No_Faktur_Formula,
				Keterangan,
				Kode_Barang,
				Nama_Barang,
				Tanggal,
				Jam,
				UserID
			)
			VALUES
			(
				'{KodePerusahaan}',
				'{FakturTrialFormula.Trim}',
				'{noFaktur}',
				'Trial Produksi Otomatis Formula {noFaktur}',
				'{TxtFormulator_KodeBarang.Text.Trim}',
				'{TxtFormulator_NamaBarang.Text.Trim}',
				'{Format(tgl_skg, "yyyy-MM-dd")}',
				'{Format(tgl_skg, "HH:mm:ss")}',
				'{UserID}'
			)
		"
		ExecuteTrans(SQL)

		SQL = $"
			UPDATE EMI_Transaksi_Formulator
			SET Flag_Input_Trial_Produksi = 'Y'
			WHERE
				Kode_Perusahaan = '{KodePerusahaan}'
				AND Status IS NULL
				AND No_Faktur = '{noFaktur}'
		"
		ExecuteTrans(SQL)

		MessageBox.Show(
			$"Formula {noFaktur} Berhasil Disimpan.",
			Judul,
			MessageBoxButtons.OK,
			MessageBoxIcon.Information
		)
	End Sub


	Private Sub Btn_Tolak_Click(sender As Object, e As EventArgs) Handles Btn_Tolak.Click
		Dim noFaktur As String = TxtFormulator_NoFaktur.Text.Trim

		If noFaktur.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show(
			"Yakin ingin menolak formula ini?",
			Judul,
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question
		) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'==========================
			'= CEK STATUS FORMULA
			'==========================
			SQL = $"
				SELECT
					Status,
					Flag_Validasi,
					Flag_Validasi_Main
				FROM EMI_Transaksi_Formulator
				WHERE
					Kode_Perusahaan = '{KodePerusahaan}'
					AND No_Faktur = '{noFaktur}'
			"
			Using dr = OpenTrans(SQL)
				If Not dr.Read Then
					CloseTrans()
					CloseConn()

					MessageBox.Show(
						$"No Faktur {noFaktur} Tidak Ditemukan pada Transaksi Formula",
						Judul,
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation
					)
					Exit Sub
				End If

				If General_Class.CekNULL(dr("Status")) = "Y" Then
					CloseTrans()
					CloseConn()

					MessageBox.Show(
						$"No Faktur {noFaktur} Sudah Dibatalkan",
						Judul,
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation
					)
					Exit Sub
				End If

				If General_Class.CekNULL(dr("Flag_Validasi")) <> "Y" Then
					CloseTrans()
					CloseConn()

					MessageBox.Show(
						$"No Faktur {noFaktur} Belum Divalidasi Formulator",
						Judul,
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation
					)
					Exit Sub
				End If

			End Using

			'==========================
			'= CEK FLOW FORMULA
			'==========================
			Dim flagField As String = ""
			Dim tanggalField As String = ""
			Dim jamField As String = ""
			Dim userField As String = ""
			SQL = $"
				SELECT
					Flag_Lanjut_Trial_Kitchen,
					Flag_Selesai_Trial_Kitchen,
					Flag_Lanjut_Trial_Produksi,
					Flag_Selesai_Trial_Produksi,
					Flag_Lanjut_Produksi,
					Flag_Selesai_Produksi
				FROM EMI_Transaksi_Formulator
				WHERE
					Kode_Perusahaan = '{KodePerusahaan}'
					AND No_Faktur = '{noFaktur}'
			"
			Using dr = OpenTrans(SQL)
				If Not dr.Read Then
					CloseTrans()
					CloseConn()

					MessageBox.Show(
						"Status flow formula tidak ditemukan",
						Judul,
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation
					)
					Exit Sub
				End If

				Dim flagLanjutTrialKitchen As String = General_Class.CekNULL(dr("Flag_Lanjut_Trial_Kitchen"))
				Dim flagSelesaiTrialKitchen As String = General_Class.CekNULL(dr("Flag_Selesai_Trial_Kitchen"))
				Dim flagLanjutTrialProduksi As String = General_Class.CekNULL(dr("Flag_Lanjut_Trial_Produksi"))
				Dim flagSelesaiTrialProduksi As String = General_Class.CekNULL(dr("Flag_Selesai_Trial_Produksi"))
				Dim flagLanjutProduksi As String = General_Class.CekNULL(dr("Flag_Lanjut_Produksi"))
				Dim flagSelesaiProduksi As String = General_Class.CekNULL(dr("Flag_Selesai_Produksi"))

				'==========================================
				'= BELUM MASUK TAHAP APAPUN
				'==========================================
				If flagLanjutTrialKitchen = "" Then

					flagField = "Flag_Lanjut_Trial_Kitchen"
					tanggalField = "Tanggal_Lanjut_Trial_Kitchen"
					jamField = "Jam_Lanjut_Trial_Kitchen"
					userField = "UserID_Lanjut_Trial_Kitchen"

					'==========================================
					'= SETELAH TRIAL KITCHEN
					'==========================================
				ElseIf flagLanjutTrialKitchen = "Y" AndAlso
				   flagSelesaiTrialKitchen = "Y" AndAlso
				   flagLanjutTrialProduksi = "" Then

					flagField = "Flag_Lanjut_Trial_Produksi"
					tanggalField = "Tanggal_Lanjut_Trial_Produksi"
					jamField = "Jam_Lanjut_Trial_Produksi"
					userField = "UserID_Lanjut_Trial_Produksi"

					'==========================================
					'= SETELAH TRIAL PRODUKSI
					'==========================================
				ElseIf flagLanjutTrialProduksi = "Y" AndAlso
				   flagSelesaiTrialProduksi = "Y" AndAlso
				   flagLanjutProduksi = "" Then

					flagField = "Flag_Lanjut_Produksi"
					tanggalField = "Tanggal_Lanjut_Produksi"
					jamField = "Jam_Lanjut_Produksi"
					userField = "UserID_Lanjut_Produksi"

					'==========================================
					'= SUDAH SELESAI PRODUKSI
					'==========================================
				ElseIf flagLanjutProduksi = "Y" AndAlso
				   flagSelesaiProduksi = "Y" Then

					CloseTrans()
					CloseConn()

					MessageBox.Show(
						"Formula sudah selesai sampai tahap produksi",
						Judul,
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation
					)
					Exit Sub
				Else
					CloseTrans()
					CloseConn()

					MessageBox.Show(
						"Status flow formula tidak valid",
						Judul,
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation
					)
					Exit Sub
				End If
			End Using

			'==========================
			'= UPDATE PENOLAKAN
			'==========================
			SQL = $"
				UPDATE EMI_Transaksi_Formulator
				SET
					{flagField} = 'T',
					{tanggalField} = '{Format(tgl_skg, "yyyy-MM-dd")}',
					{jamField} = '{Format(tgl_skg, "HH:mm:ss")}',
					{userField} = '{UserID}'
				WHERE
					Kode_Perusahaan = '{KodePerusahaan}'
					AND No_Faktur = '{noFaktur}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()

			MessageBox.Show(
				"Data Berhasil Ditolak",
				Judul,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			)
		Catch ex As Exception
			CloseTrans()
			CloseConn()

			MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try

		Me.Close()
	End Sub

	Private Sub BtnTrialKitchen_Click(sender As Object, e As EventArgs) Handles BtnTrialKitchen.Click
		ProsesFlag("Trial Kitchen", "flag_lanjut_trial_kitchen")
	End Sub

	Private Sub BtnTrialProduksi_Click(sender As Object, e As EventArgs) Handles BtnTrialProduksi.Click
		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show($"Yakin ingin lanjut ke Trial Produksi?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Get_NoFaktur_TrialProduksi()
			ProsesFlag_TrialProduksi("Trial Produksi", "flag_lanjut_trial_produksi")
			Handle_Trial_Produksi()

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End Try

		N_EMI_Dashboard_Formula.Load_Formula()
		Me.Close()
	End Sub

	Private Sub BtnProduksi_Click(sender As Object, e As EventArgs) Handles BtnProduksi.Click
		ProsesFlag("Produksi", "flag_lanjut_produksi")
	End Sub

	Private Sub ProsesFlag(namaProses As String, namaKolom As String)
		Dim noFaktur As String = TxtFormulator_NoFaktur.Text.Trim

		If noFaktur.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show(
			$"Yakin ingin lanjut ke {namaProses} ?",
			Judul,
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question
		) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim flagLanjutTrialKitchen As String = ""
			Dim flagSelesaiTrialKitchen As String = ""
			Dim flagLanjutTrialProduksi As String = ""
			Dim flagSelesaiTrialProduksi As String = ""
			Dim keteranganBypass As String = ""
			Dim jenisBypass As String = ""

			SQL = $"
				SELECT
					Flag_Lanjut_Trial_Kitchen,
					Flag_Selesai_Trial_Kitchen,
					Flag_Lanjut_Trial_Produksi,
					Flag_Selesai_Trial_Produksi
				FROM EMI_Transaksi_Formulator
				WHERE
					Kode_Perusahaan = '{KodePerusahaan}'
					AND No_Faktur = '{noFaktur}'
			"
			Using dr = OpenTrans(SQL)
				If Not dr.Read Then
					Throw New Exception("Formula tidak ditemukan")
				End If

				flagLanjutTrialKitchen = General_Class.CekNULL(dr("Flag_Lanjut_Trial_Kitchen"))
				flagSelesaiTrialKitchen = General_Class.CekNULL(dr("Flag_Selesai_Trial_Kitchen"))

				flagLanjutTrialProduksi = General_Class.CekNULL(dr("Flag_Lanjut_Trial_Produksi"))
				flagSelesaiTrialProduksi = General_Class.CekNULL(dr("Flag_Selesai_Trial_Produksi"))
			End Using

			Dim isBypassTrial As Boolean = FlagBypassGlobal OrElse FlagBypassTrial

			If namaKolom = "flag_lanjut_produksi" Then
				If flagLanjutTrialKitchen <> "Y" AndAlso flagLanjutTrialProduksi <> "Y" AndAlso (FlagBypassGlobal OrElse FlagBypassTrial) Then
					Dim result As DialogResult = MessageBox.Show(
						"Formula ini belum melalui Trial Kitchen maupun Trial Produksi." & vbCrLf & vbCrLf &
						"Dengan melanjutkan ke Produksi Komersial, seluruh proses trial akan dilewati (bypass)." & vbCrLf & vbCrLf &
						"Apakah Anda yakin ingin melanjutkan?",
						Judul,
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question
					)

					If result = DialogResult.No Then
						CloseTrans()
						CloseConn()
						Exit Sub
					End If

					Using frm As New N_EMI_SD_Konfirmasi_Bypass_Formula

						frm.NoFormula = noFaktur
						frm.KodeBarang = TxtFormulator_KodeBarang.Text.Trim
						frm.NamaBarang = TxtFormulator_NamaBarang.Text.Trim
						frm.QtyHasil = TxtFormulator_Hasil.Text.Trim
						frm.SatuanHasil = CmbFormulator_SatuanHasil.Text.Trim

						If frm.ShowDialog() <> DialogResult.OK Then
							CloseTrans()
							CloseConn()
							Exit Sub
						End If

						If frm.KeteranganBypass.Trim = "" Then
							CloseTrans()
							CloseConn()

							MessageBox.Show(
									"Keterangan bypass wajib diisi.",
									Judul,
									MessageBoxButtons.OK,
									MessageBoxIcon.Warning
								)
							Exit Sub
						End If

						keteranganBypass = frm.KeteranganBypass.Trim
						jenisBypass = "TANPA_TRIAL"
					End Using
				ElseIf flagLanjutTrialProduksi = "Y" AndAlso flagSelesaiTrialProduksi <> "Y" Then
					Dim result As DialogResult = MessageBox.Show(
							"Trial Produksi masih berjalan." & vbCrLf & vbCrLf &
							"Jika dilanjutkan ke Produksi Komersial maka Trial Produksi yang sedang berjalan tetap akan berlangsung hingga selesai." & vbCrLf & vbCrLf &
							"Apakah Anda yakin ingin melanjutkan?",
							Judul,
							MessageBoxButtons.YesNo,
							MessageBoxIcon.Question
						)

					If result = DialogResult.No Then
						CloseTrans()
						CloseConn()
						Exit Sub
					End If

					Using frm As New N_EMI_SD_Konfirmasi_Bypass_Formula

						frm.NoFormula = noFaktur
						frm.KodeBarang = TxtFormulator_KodeBarang.Text.Trim
						frm.NamaBarang = TxtFormulator_NamaBarang.Text.Trim
						frm.QtyHasil = TxtFormulator_Hasil.Text.Trim
						frm.SatuanHasil = CmbFormulator_SatuanHasil.Text.Trim

						If frm.ShowDialog() <> DialogResult.OK Then
							CloseTrans()
							CloseConn()
							Exit Sub
						End If

						If frm.KeteranganBypass.Trim = "" Then
							CloseTrans()
							CloseConn()

							MessageBox.Show(
									"Keterangan bypass wajib diisi.",
									Judul,
									MessageBoxButtons.OK,
									MessageBoxIcon.Warning
								)
							Exit Sub
						End If

						keteranganBypass = frm.KeteranganBypass.Trim
						jenisBypass = "DENGAN_TRIAL_PRODUKSI"
					End Using
				Else
					MessageBox.Show(
						"Formula Tidak Bisa Dibypass." & vbCrLf & vbCrLf &
						"Silakan lakukan proses Trial Kitchen dan/atau Trial Produksi terlebih dahulu sebelum melanjutkan ke Produksi Komersial.",
						Judul,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning
					)

					CloseTrans()
					CloseConn()
					Exit Sub
				End If
			End If

			Dim setSQL As New List(Of String)
			Dim SetFlag As Action(Of String, String) =
			Sub(flagField As String, value As String)
				Dim suffix As String = flagField.Replace("flag_", "")

				setSQL.Add($"{flagField} = CASE WHEN {flagField} IS NULL THEN '{value}' ELSE {flagField} END")
				setSQL.Add($"Tanggal_{suffix} = CASE WHEN {flagField} IS NULL THEN '{Format(tgl_skg, "yyyy-MM-dd")}' ELSE Tanggal_{suffix} END")
				setSQL.Add($"Jam_{suffix} = CASE WHEN {flagField} IS NULL THEN '{Format(tgl_skg, "HH:mm:ss")}' ELSE Jam_{suffix} END")
				setSQL.Add($"UserID_{suffix} = CASE WHEN {flagField} IS NULL THEN '{UserID}' ELSE UserID_{suffix} END")

			End Sub
			SetFlag(namaKolom, "Y")

			Select Case namaKolom
				Case "flag_lanjut_produksi"
					Dim trialProduksiSedangBerjalan As Boolean =
					flagLanjutTrialProduksi = "Y" AndAlso
					flagSelesaiTrialProduksi <> "Y"

					If isBypassTrial AndAlso Not trialProduksiSedangBerjalan Then
						SetFlag("flag_lanjut_trial_kitchen", "B")
						SetFlag("flag_selesai_trial_kitchen", "B")

						SetFlag("flag_lanjut_trial_produksi", "B")
						SetFlag("flag_selesai_trial_produksi", "B")
					End If

					If keteranganBypass <> "" AndAlso jenisBypass = "TANPA_TRIAL" Then
						setSQL.Add($"Keterangan_Bypass_trial = '{keteranganBypass}'")
					ElseIf keteranganBypass <> "" AndAlso jenisBypass = "DENGAN_TRIAL_PRODUKSI" Then
						setSQL.Add("Flag_Bypass_Trial_Produksi_On_Process = 'Y'")
						setSQL.Add($"Keterangan_Bypass_Trial_Produksi_On_Process = '{keteranganBypass}'")
					End If
			End Select

			SQL = $"
				UPDATE EMI_Transaksi_Formulator
				SET {String.Join(",", setSQL)}
				WHERE
					Kode_Perusahaan = '{KodePerusahaan}'
					AND No_Faktur = '{noFaktur}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()

			MessageBox.Show(
				$"Berhasil Lanjut ke {namaProses}",
				Judul,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			)
		Catch ex As Exception
			CloseTrans()
			CloseConn()

			MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try

		N_EMI_Dashboard_Formula.Load_Formula()
		Me.Close()
	End Sub

	Private Sub ProsesFlag_TrialProduksi(namaProses As String, namaKolom As String)
		Dim noFaktur As String = TxtFormulator_NoFaktur.Text.Trim

		Dim flagLanjutTrialKitchen As String = ""
		Dim flagSelesaiTrialKitchen As String = ""

		SQL = $"
			SELECT
				Flag_Lanjut_Trial_Kitchen,
				Flag_Selesai_Trial_Kitchen
			FROM EMI_Transaksi_Formulator
			WHERE
				Kode_Perusahaan = '{KodePerusahaan}'
				AND No_Faktur = '{noFaktur}'
		"
		Using dr = OpenTrans(SQL)
			If Not dr.Read Then
				Throw New Exception("Formula tidak ditemukan")
			End If

			flagLanjutTrialKitchen = General_Class.CekNULL(dr("Flag_Lanjut_Trial_Kitchen"))
			flagSelesaiTrialKitchen = General_Class.CekNULL(dr("Flag_Selesai_Trial_Kitchen"))
		End Using

		Dim isBypassTrial As Boolean =
		FlagBypassGlobal OrElse FlagBypassTrial
		If Not isBypassTrial Then
			If flagLanjutTrialKitchen = "Y" AndAlso
				flagSelesaiTrialKitchen <> "Y" Then

				Throw New Exception("Trial Kitchen masih berlangsung dan belum selesai.")
			End If
		End If

		Dim setSQL As New List(Of String)
		Dim suffix As String = namaKolom.Replace("flag_", "")

		setSQL.Add($"{namaKolom} = 'Y'")
		setSQL.Add($"Tanggal_{suffix} = '{Format(tgl_skg, "yyyy-MM-dd")}'")
		setSQL.Add($"Jam_{suffix} = '{Format(tgl_skg, "HH:mm:ss")}'")
		setSQL.Add($"UserID_{suffix} = '{UserID}'")

		If isBypassTrial Then
			setSQL.Add("Flag_Lanjut_Trial_Kitchen = 'B'")
			setSQL.Add("Flag_Selesai_Trial_Kitchen = 'B'")

			setSQL.Add($"Tanggal_Lanjut_Trial_Kitchen = '{Format(tgl_skg, "yyyy-MM-dd")}'")
			setSQL.Add($"Jam_Lanjut_Trial_Kitchen = '{Format(tgl_skg, "HH:mm:ss")}'")
			setSQL.Add($"UserID_Lanjut_Trial_Kitchen = '{UserID}'")

			setSQL.Add($"Tanggal_Selesai_Trial_Kitchen = '{Format(tgl_skg, "yyyy-MM-dd")}'")
			setSQL.Add($"Jam_Selesai_Trial_Kitchen = '{Format(tgl_skg, "HH:mm:ss")}'")
			setSQL.Add($"UserID_Selesai_Trial_Kitchen = '{UserID}'")
		End If

		SQL = $"
			UPDATE EMI_Transaksi_Formulator
			SET {String.Join(",", setSQL)}
			WHERE
				Kode_Perusahaan = '{KodePerusahaan}'
				AND No_Faktur = '{noFaktur}'
		"
		ExecuteTrans(SQL)
	End Sub

	Public Sub Kosong()
		TxtFormulator_Total.Text = ""
		TxtFormulator_KodeBarang.Text = ""
		TxtFormulator_NamaBarang.Text = ""
		TxtFormulator_NoFaktur.Text = ""
		TxtFormulator_Hasil.Text = ""
		Txt_No_Faktur_Binding.Text = ""

		CmbFormulator_PenanggungJawab.SelectedIndex = -1
		CmbFormulator_SatuanHasil.SelectedIndex = -1

		CmbFormulator_LokasiInquiry.Focus()

		FlagBypassGlobal = False
		FlagBypassTrial = False
		BtnTrialKitchen.Enabled = True
		BtnTrialProduksi.Enabled = True
		BtnProduksi.Enabled = True
		Btn_Tolak.Enabled = True
	End Sub
End Class
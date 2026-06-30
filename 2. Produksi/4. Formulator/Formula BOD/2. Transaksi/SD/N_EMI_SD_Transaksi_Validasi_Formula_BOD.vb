Public Class N_EMI_SD_Transaksi_Validasi_Formula_BOD
	Public Property No_Faktur As String
	Public Property StatusBypass As String = "NORMAL"
	Public Property KeteranganBypass As String = "NORMAL"

	Dim lvNo As String
	Dim lvTipe As String
	Dim lvKdBarang As String
	Dim lvNama As String
	Dim lvQty As String
	Dim lvSatuan As String
	Dim lvPengali As String
	Dim lvSatuanBarang As String
	Dim lvNilaiBarang As String
	Dim lvPersentase As String
	Dim lvKet As String
	Dim lvQty_SatHasil As String
	Dim lvSatHasil As String
	Dim lvEstHPP As String
	Dim lvEstHPPPcs As String

	Dim Item_Moisture_ID As Integer = 0
	Dim Item_Moisture_Kode_Analisa As Integer = 1
	Dim Item_Moisture_Jenis_Analisa As Integer = 2
	Dim Item_Moisture_Flag_Perhitungan As Integer = 3
	Dim Item_Moisture_Kode_Aktivitas As Integer = 4
	Dim Item_Moisture_Kategori As Integer = 5
	Dim Item_Moisture_Combobox As Integer = 6
	Dim Item_Moisture_Range_Awal As Integer = 7
	Dim Item_Moisture_Range_Akhir As Integer = 8
	Dim Item_Moisture_Value As Integer = 9

	Dim cellNo As Integer = 0
	Public cellKdBarang As Integer = 1
	Public cellNama As Integer = 2
	Public Property cellQty As Integer = 3
	Public cellSatuan As Integer = 4
	Public cellPengali As Integer = 5
	Public cellSatuanBarang As Integer = 6
	Public cellNilaiBarang As Integer = 7
	Public Property cellPersentase As Integer = 8
	Dim cellKet As Integer = 9
	Public cellQty_SatHasil As Integer = 10
	Dim cellSatHasil As Integer = 11
	Dim cellEstHPP As Integer = 12
	Dim cellEstHPPPcs As Integer = 13

	Private ArrNoFakturBinding As New List(Of String)
	Private ArrHierarkiBinding As New List(Of String)
	Private ArrKeteranganBinding As New List(Of String)
	Private Sub N_EMI_SD_Transaksi_Validasi_Formula_BOD_Load(sender As Object, e As EventArgs) Handles Me.Load
		TxtFormulator_NoFaktur.Text = No_Faktur

		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
			Exit Sub
		Else
		End If

		If StatusBypass = "NORMAL" Then
			TB_StatusBypass.Text = "NORMAL - TANPA BYPASS"
			TB_KeteranganBypass.Text = ""

			LB_StatusBypass.Visible = True
			LB_KeteranganBypass.Visible = True
			TB_StatusBypass.Visible = True
			TB_KeteranganBypass.Visible = True

		ElseIf StatusBypass = "BYPASS_TRIAL" Then
			TB_StatusBypass.Text = "BYPASS - TANPA TRIAL"
			TB_KeteranganBypass.Text = KeteranganBypass
			LB_StatusBypass.Visible = True
			LB_KeteranganBypass.Visible = True
			TB_StatusBypass.Visible = True
			TB_KeteranganBypass.Visible = True

		ElseIf StatusBypass = "BYPASS_TRIAL_PRODUKSI_ON_PROCESS" Then
			TB_StatusBypass.Text = "BYPASS - TRIAL PRODUKSI BERLANGSUNG"
			TB_KeteranganBypass.Text = KeteranganBypass
			LB_StatusBypass.Visible = True
			LB_KeteranganBypass.Visible = True
			TB_StatusBypass.Visible = True
			TB_KeteranganBypass.Visible = True
		End If

		With LvBindingFormula
			.View = View.Details
			.FullRowSelect = True
			.GridLines = True
			.MultiSelect = False
			.HideSelection = False
			.Columns.Clear()

			.Columns.Add("No Formula", 100)
			.Columns.Add("Posisi", 120)
			.Columns.Add("Keterangan", 250)
		End With

		Load_Detail()

		Try
			OpenConn()

			Cmb_Susuan_Awal.Items.Clear()
			Cmb_Susuan_Awal.Items.Add("--- PILIH HIERARKI DEFAULT ---")
			SQL = $"
				select Kode_Hierarki
				from N_EMI_Master_Hierarki_Formula
				where Kode_Perusahaan = '{KodePerusahaan}'
				order by Urutan
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Susuan_Awal.Items.Add(Dr("Kode_Hierarki"))
				Loop
			End Using
			Cmb_Susuan_Awal.SelectedIndex = 0

			Dim noFakturAktif As String = ""

			SQL = $"
                    SELECT TOP 1 No_Faktur
                    FROM N_EMI_Transaksi_Formulator_Binding
                    WHERE 
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{TxtFormulator_KodeBarang.Text.Trim}'
                        AND Status IS NULL
                        AND Flag_Validasi_Main = 'Y'
                    ORDER BY Tanggal DESC, Jam DESC
                "

			Using drFaktur = OpenTrans(SQL)
				If drFaktur.Read() Then
					noFakturAktif = General_Class.CekNULL(drFaktur("No_Faktur"))
				End If
			End Using

			GB_BindingFormula.Text = $"Faktur Binding Aktif: {noFakturAktif}"

			SQL = $"
                    SELECT 
                        D.No_Faktur,
                        D.No_Formulator,
                        D.No_Prioritas,
                        D.Kode_Hierarki,
                        D.Keterangan
                    FROM N_EMI_Transaksi_Formulator_Binding_Detail D
                    JOIN EMI_Transaksi_Formulator F ON F.Kode_Perusahaan = D.Kode_Perusahaan AND F.No_Faktur = D.No_Formulator
                    WHERE 
                        D.Kode_Perusahaan = '{KodePerusahaan}'
                        AND D.No_Faktur = '{noFakturAktif}'
                        AND F.Status IS NULL
                    ORDER BY D.No_Prioritas
                "

			LvBindingFormula.BeginUpdate()
			LvBindingFormula.Items.Clear()

			' array master ini jadi sumber data yang dipakai ulang oleh
			' Cmb_PosisiBinding_SelectedIndexChanged untuk re-render LvBindingFormula
			' setiap kali posisi binding berganti, tanpa perlu query ulang ke DB
			ArrNoFakturBinding.Clear()
			ArrHierarkiBinding.Clear()
			ArrKeteranganBinding.Clear()

			Using Dr = OpenTrans(SQL)
				While Dr.Read()
					Dim noFormula As String = General_Class.CekNULL(Dr("No_Formulator"))
					Dim keterangan As String = General_Class.CekNULL(Dr("Keterangan"))
					Dim kodeHierarki As String = General_Class.CekNULL(Dr("Kode_Hierarki"))

					ArrNoFakturBinding.Add(noFormula)
					ArrHierarkiBinding.Add(kodeHierarki)
					ArrKeteranganBinding.Add(keterangan)

					Dim item As New ListViewItem(noFormula)
					item.SubItems.Add(kodeHierarki)
					item.SubItems.Add(keterangan)

					LvBindingFormula.Items.Add(item)
				End While
			End Using

			'If LvBindingFormula.Items.Count > 0 Then
			'	LvBindingFormula.Items(0).BackColor = Color.LightGreen
			'End If

			LvBindingFormula.EndUpdate()

			Dim totalDetail As Integer = 0
			Cmb_PosisiBinding.Items.Clear()

			SQL = "
				SELECT COUNT(*) AS Total_Detail
				FROM N_EMI_Transaksi_Formulator_Binding_Detail D
				JOIN EMI_Transaksi_Formulator F
					ON F.Kode_Perusahaan = D.Kode_Perusahaan
					AND F.No_Faktur = D.No_Formulator
				WHERE
					D.Kode_Perusahaan = '" & KodePerusahaan & "'
					AND D.No_Faktur = '" & noFakturAktif & "'
					AND F.Status IS NULL
			"

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					totalDetail = CInt(Dr("Total_Detail"))
				End If
			End Using

			Cmb_PosisiBinding.Items.Add("--- PILIH POSISI BINDING ---")
			For i As Integer = 0 To totalDetail
				If i = 0 Then
					Cmb_PosisiBinding.Items.Add("FORMULA UTAMA")
				Else
					Cmb_PosisiBinding.Items.Add("CADANGAN " & i)
				End If
			Next
			Cmb_PosisiBinding.SelectedIndex = 0

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Load_Detail()
		Try
			OpenConn()

			SQL = $"
                SELECT a.Nama, a.Id_Karyawan
                FROM Emi_Karyawan a
                INNER JOIN Emi_Transaksi_Formulator b ON a.Id_Karyawan = b.Penanggung_Jawab
                WHERE b.Kode_Perusahaan = '{KodePerusahaan}' AND b.No_Faktur = '{No_Faktur}'
            "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CmbFormulator_PenanggungJawab.Items.Clear()
					CmbFormulator_PenanggungJawab.Items.Add(Dr("Nama"))
					CmbFormulator_PenanggungJawab.SelectedIndex = 0
				End If
			End Using

			Dim Nomor As Integer = 1
			SQL = $"
                SELECT
                    a.No_Faktur, a.Tanggal, a.Lokasi, a.Kode_Stock_Owner, a.Kode_Barang,
                    b.Nama as Nama_Barang, a.UserID, a.Hasil, a.Satuan_Hasil, a.Penanggung_Jawab,
                    c.Kode_Barang as Kode_Bahan, d.Nama as Nama_Bahan, c.Jumlah, c.satuan,
                    c.Nilai_Pengali, c.Satuan_barang, c.Nilai_Barang, c.Persentase

                FROM Emi_Transaksi_Formulator a
                INNER JOIN Barang b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Kode_Stock_Owner = b.Kode_Stock_Owner AND a.Kode_Barang = b.Kode_Barang_inq
                INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND a.No_Faktur = c.No_Faktur
                INNER JOIN Barang d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Stock_Owner = d.Kode_Stock_Owner AND c.Kode_Barang = d.Kode_Barang
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
                AND a.Status IS NULL
                AND a.No_Faktur = '{No_Faktur}'
                ORDER BY d.nama
            "

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count = 0 Then
						CloseConn()
						MessageBox.Show("Detail Bahan Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Me.Close()
						Exit Sub
					End If

					For i As Integer = 0 To .Rows.Count - 1
						Dim row = .Rows(i)

						DtpFormulator_Tanggal.Value = row("Tanggal")
						TxtFormulator_KodeBarang.Text = row("Kode_Barang")
						TxtFormulator_NamaBarang.Text = row("Nama_Barang")
						TxtFormulator_Hasil.Text = Format(row("Hasil"), "N4")

						Dim lokasi As String = row("Lokasi").ToString.Trim
						Dim lokasiBarang As String = row("Kode_Stock_Owner").ToString.Trim
						Dim satuan As String = row("Satuan_Hasil").ToString.Trim

						If Not CmbFormulator_LokasiInquiry.Items.Contains(lokasi) Then
							CmbFormulator_LokasiInquiry.Items.Add(lokasi)
						End If

						If Not CmbFormulator_LokasiBarang.Items.Contains(lokasiBarang) Then
							CmbFormulator_LokasiBarang.Items.Add(lokasiBarang)
						End If

						If Not CmbFormulator_SatuanHasil.Items.Contains(satuan) Then
							CmbFormulator_SatuanHasil.Items.Add(satuan)
						End If

						CmbFormulator_LokasiInquiry.SelectedItem = lokasi
						CmbFormulator_LokasiBarang.SelectedItem = lokasiBarang
						CmbFormulator_SatuanHasil.SelectedItem = satuan
					Next
				End With
			End Using

			CloseConn()

		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal mendapatkan data: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try
	End Sub

	Private Function CekNothing(ByVal str As String) As String
		Dim hasil As String = ""

		If str Is Nothing Then
			hasil = ""
		Else
			hasil = str
		End If

		Return hasil
	End Function

	Private Function get_no_faktur_binding() As String
		Dim NoFaktur = fBindingFormula & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("N_EMI_Transaksi_Formulator_Binding", "No_Faktur", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Faktur, 1, " & Len(fBindingFormula) + 4 & ")", fBindingFormula & Format(tgl_skg, "MMyy"))

		Return NoFaktur
	End Function

	Private Sub Btn_Validasi_Click(sender As Object, e As EventArgs) Handles Btn_Validasi.Click
		If Cmb_Susuan_Awal.SelectedIndex = 0 Then
			MessageBox.Show("Hierarki default harus dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Try
			OpenConn()

			If CekButtonRole("Validasi_Formula_BOD") = "T" Then
				MessageBox.Show("Anda tidak memiliki akses untuk memvalidasi formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal memvalidasi role button: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		Dim result As DialogResult = MessageBox.Show(
			"Apakah Anda yakin ingin memvalidasi formula ini?",
			Judul,
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question
		)

		If result <> DialogResult.Yes Then
			Exit Sub
		End If

		Dim FlagPermanent As String = "NULL"
		Dim FlagExpired As String = "NULL"
		Dim StartExpired As String = "NULL"
		Dim EndExpired As String = "NULL"
		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If Cmb_PosisiBinding.SelectedIndex <> 0 Then
				If Cmb_PosisiBinding.SelectedIndex = 1 Then
					Dim NoFormula As String = If(LvBindingFormula.Items.Count = 0, "", LvBindingFormula.Items(0).SubItems(0).Text.Trim())
					Dim frmExp As New N_EMI_SD_Pengaturan_Expired_Formula

					SQL = $"
						SELECT TOP 1
							f.No_Faktur,
							f.Hasil,
							f.Satuan_Hasil
						FROM EMI_Transaksi_Formulator f
						WHERE
							f.Kode_Perusahaan = '{KodePerusahaan}'
							AND f.No_Faktur = '{NoFormula}'
					"
					Using Dr = OpenTrans(SQL)
						If Dr.Read() Then
							With frmExp
								.NoFormula = General_Class.CekNULL(Dr("No_Faktur")).Trim()
								.KodeBarang = TxtFormulator_KodeBarang.Text.Trim
								.NamaBarang = TxtFormulator_NamaBarang.Text.Trim
								.QtyHasil = General_Class.CekNULL(Dr("Hasil")).Trim()
								.SatuanHasil = General_Class.CekNULL(Dr("Satuan_Hasil")).Trim()
							End With

							If frmExp.ShowDialog() <> DialogResult.OK Then
								Dr.Close()
								CloseTrans()
								CloseConn()
								Exit Sub
							End If

							If frmExp.JenisExpired = "Permanen" Then
								FlagPermanent = "'Y'"
							ElseIf frmExp.JenisExpired = "Sementara" Then
								FlagExpired = "'Y'"
								StartExpired = $"'{Format(frmExp.startExp, "yyyy-MM-dd HH:mm:ss")}'"
								EndExpired = $"'{Format(frmExp.endExp, "yyyy-MM-dd HH:mm:ss")}'"
							End If
						Else
							CloseTrans()
							CloseConn()

							MessageBox.Show(
							"Data formula tidak ditemukan.",
							Judul,
							MessageBoxButtons.OK,
							MessageBoxIcon.Exclamation)

							Exit Sub
						End If
					End Using
				End If

				Dim NoFaktur = get_no_faktur_binding()
				Dim KodeBarang = TxtFormulator_KodeBarang.Text.Trim
				Dim Tanggal = Format(tgl_skg, "yyyy-MM-dd")
				Dim Jam = Format(tgl_skg, "HH:mm:ss")

				SQL = $"UPDATE 
						N_EMI_Transaksi_Formulator_Binding 
					SET Status = 'Y', Tanggal_Validasi_Main = '{Tanggal}', Jam_Validasi_Main = '{Jam}', User_Validasi_Main = 'SYSTEM'
					WHERE Kode_Perusahaan = '{KodePerusahaan}' AND kode_barang = '{KodeBarang}' and status is null and flag_validasi_main is null"
				ExecuteTrans(SQL)

				SQL = $"
					INSERT INTO N_EMI_Transaksi_Formulator_Binding
					(Kode_Perusahaan, No_Faktur, Kode_Barang, Tanggal, Jam, UserID, Status, Flag_Validasi_Main, Tanggal_Validasi_Main, Jam_Validasi_Main, User_Validasi_Main)
					VALUES
					('{KodePerusahaan}', '{NoFaktur}', '{KodeBarang}', '{Tanggal}', '{Jam}', '{UserID}', NULL, 'Y', '{Tanggal}', '{Jam}', '{UserID}')
				"
				ExecuteTrans(SQL)
				For i = 0 To LvBindingFormula.Items.Count - 1
					Dim item = LvBindingFormula.Items(i)
					Dim NoFormulator = item.SubItems(0).Text
					Dim KodeHierarki = item.SubItems(1).Text
					Dim Keterangan = item.SubItems(2).Text
					Dim Prioritas = i + 1

					Dim sqlFlagPermanent As String = "NULL"
					Dim sqlFlagExpired As String = "NULL"
					Dim sqlStartExpired As String = "NULL"
					Dim sqlEndExpired As String = "NULL"

					'Setting expired hanya untuk formula prioritas pertama
					If Prioritas = 1 Then
						sqlFlagPermanent = FlagPermanent
						sqlFlagExpired = FlagExpired
						sqlStartExpired = StartExpired
						sqlEndExpired = EndExpired
					End If

					SQL = $"
						INSERT INTO N_EMI_Transaksi_Formulator_Binding_Detail
						(Kode_Perusahaan, No_Faktur, No_Formulator, No_Prioritas, Kode_Hierarki, Tanggal, Jam, UserID, Keterangan,Flag_Permanent,Flag_Expired,Start_Expired,End_Expired)
						VALUES
						('{KodePerusahaan}', '{NoFaktur}', '{NoFormulator}', '{Prioritas}', '{KodeHierarki}', '{Tanggal}', '{Jam}', '{UserID}', '{Keterangan}', {sqlFlagPermanent},  {sqlFlagExpired}, {sqlStartExpired}, {sqlEndExpired})
					"
					ExecuteTrans(SQL)
				Next
			End If

			SQL = $"
				UPDATE Emi_Transaksi_Formulator
				SET Kode_Hierarki = '{Cmb_Susuan_Awal.Text}', Flag_Validasi_Formula_Produksi_BOD = 'Y', Tanggal_Validasi_Formula_Produksi_BOD = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi_Formula_Produksi_BOD = '{Format(tgl_skg, "HH:mm:ss")}', UserID_Validasi_Formula_Produksi_BOD = '{UserID}'
				WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Faktur = '{No_Faktur}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()

			MessageBox.Show("Berhasil memvalidasi formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try

		Me.Close()
	End Sub

	Private Sub Btn_Tolak_Click(sender As Object, e As EventArgs) Handles Btn_Tolak.Click
		Try
			OpenConn()

			If CekButtonRole("Tolak_Formula_BOD") = "T" Then
				MessageBox.Show("Anda tidak memiliki akses untuk menolak formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal memvalidasi role button: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		Dim result As DialogResult = MessageBox.Show(
			"Apakah Anda yakin ingin menolak formula ini?",
			Judul,
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question
		)

		If result <> DialogResult.Yes Then
			Exit Sub
		End If

		get_jam()

		Try
			OpenConn()

			SQL = $"
                UPDATE Emi_Transaksi_Formulator
                SET Flag_Validasi_Formula_Produksi_BOD = 'T', Tanggal_Validasi_Formula_Produksi_BOD = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi_Formula_Produksi_BOD = '{Format(tgl_skg, "HH:mm:ss")}', UserID_Validasi_Formula_Produksi_BOD = '{UserID}'
                WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Faktur = '{No_Faktur}'
            "

			ExecuteTrans(SQL)

			CloseConn()

			MessageBox.Show("Berhasil menolak formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal menolak formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		Me.Close()
	End Sub

	Private Sub Cmb_PosisiBinding_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_PosisiBinding.SelectedIndexChanged
		If Cmb_PosisiBinding.SelectedIndex = 0 OrElse Cmb_PosisiBinding.SelectedIndex = Cmb_PosisiBinding.Items.Count - 1 Then
			Btn_Compare.Visible = False
		Else
			Btn_Compare.Visible = True
		End If

		Dim posisi As Integer = Cmb_PosisiBinding.SelectedIndex - 1

		LvBindingFormula.BeginUpdate()
		LvBindingFormula.Items.Clear()

		For i As Integer = 0 To ArrNoFakturBinding.Count
			If i = posisi Then
				Dim itemBaru As New ListViewItem(TxtFormulator_NoFaktur.Text.Trim)
				itemBaru.SubItems.Add("")
				itemBaru.SubItems.Add("")
				LvBindingFormula.Items.Add(itemBaru)
			End If

			If i < ArrNoFakturBinding.Count Then
				Dim item As New ListViewItem(ArrNoFakturBinding(i))
				item.SubItems.Add(ArrHierarkiBinding(i))
				item.SubItems.Add(ArrKeteranganBinding(i))
				LvBindingFormula.Items.Add(item)
			End If
		Next

		LvBindingFormula.EndUpdate()

		'If LvBindingFormula.Items.Count > 0 Then
		'	LvBindingFormula.Items(0).BackColor = Color.LightGreen
		'End If
	End Sub

	Private Sub Btn_Compare_Click(sender As Object, e As EventArgs) Handles Btn_Compare.Click
		Dim ListNoFakturBinding As New List(Of String)
		Dim ListHierarki As New List(Of String)
		Dim ListKeterangan As New List(Of String)

		For Each item As ListViewItem In LvBindingFormula.Items
			' Skip item yang merupakan formula aktif itu sendiri
			' (item sisipan dari Cmb_PosisiBinding_SelectedIndexChanged)
			If item.Text.Trim = TxtFormulator_NoFaktur.Text.Trim Then
				Continue For
			End If

			ListNoFakturBinding.Add(item.Text)
			ListHierarki.Add(item.SubItems(1).Text)
			ListKeterangan.Add(item.SubItems(2).Text)
		Next

		Dim SD As New N_EMI_SD_Compare_Formulator With {
			.NoFaktur = TxtFormulator_NoFaktur.Text.Trim,
			.ArrNoFaktur = ListNoFakturBinding,
			.ArrHierarki = ListHierarki,
			.ArrKeterangan = ListKeterangan,
			.PosisiTujuan = Cmb_PosisiBinding.SelectedIndex,
			.OnlyCompare = True
		}

		SD.ShowDialog()
	End Sub
End Class
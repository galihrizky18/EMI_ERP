Imports System.IO
Imports System.Net

Public Class N_EMI_SD_Transaksi_Validasi_Formula_Gabungan
	Public isi_barang As Boolean = False
	Public StatusDariList As String = ""
	Dim arrcari, arrIdPenanggungJawab As New ArrayList
	Dim Jenis = "Transaksi_Formulator"

	Dim Lv_Moisture_ID, Lv_Moisture_Kode_Analisa, Lv_Moisture_Jenis_Analisa, Lv_Moisture_Flag_Perhitungan, Lv_Moisture_Aktivitas, Lv_Moisture_Range_Awal, Lv_Moisture_Range_Akhir As String

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

	Dim lv2KdUji As String
	Dim lv2Ket As String
	Dim lv2Satuan As String
	Dim lv2Hasil As String

	Public cell2KdUji As Integer = 0
	Public cell2Ket As Integer = 1
	Public cell2Satuan As Integer = 2
	Public cell2Hasil As Integer = 3

	Private isBulletMode As Boolean = False

	Dim _tsBold As ToolStripButton
	Dim _tsItalic As ToolStripButton
	Dim _tsUnderline As ToolStripButton
	Dim _tsBullet As ToolStripButton

	'Private Sub get_no_faktur()
	'    TxtFormulator_NoFaktur.Text = fTransFormula & Format(tgl_skg, "MMyy") & "-" &
	'                         General_Class.Get_Last_Number2("Emi_Transaksi_Formulator", "no_Faktur", 5,
	'                         "Kode_perusahaan", KodePerusahaan,
	'                         "And", "substring(no_Faktur, 1, " & Len(fTransFormula) + 4 & ")", fTransFormula & Format(tgl_skg, "MMyy"))
	'End Sub

	'Private Sub get_no_faktur_binding()
	'    Txt_No_Faktur_Binding.Text = fTransFormulaBinding & Format(tgl_skg, "MMyy") & "-" &
	'                         General_Class.Get_Last_Number2("EMI_Transaksi_Formulator_Binding", "No_Faktur", 5,
	'                         "Kode_perusahaan", KodePerusahaan,
	'                         "And", "substring(no_Faktur, 1, " & Len(fTransFormulaBinding) + 4 & ")", fTransFormulaBinding & Format(tgl_skg, "MMyy"))
	'End Sub

	Private Sub InitRTBToolbar()
		For Each ctrl As Control In RTBCookingStep.Parent.Controls
			If TypeOf ctrl Is ToolStrip AndAlso ctrl.Tag?.ToString() = "RTBToolbar" Then Return
		Next

		Dim ts As New ToolStrip()
		ts.GripStyle = ToolStripGripStyle.Hidden
		ts.BackColor = Color.WhiteSmoke
		ts.Tag = "RTBToolbar"

		_tsBold = New ToolStripButton("B") With {
			.Font = New Font("Georgia", 10, FontStyle.Bold),
			.ToolTipText = "Bold (Ctrl+B)",
			.CheckOnClick = True,
			.AutoSize = False,
			.Width = 30,
			.Height = 26
		}

		_tsItalic = New ToolStripButton("I") With {
			.Font = New Font("Georgia", 10, FontStyle.Italic),
			.ToolTipText = "Italic (Ctrl+I)",
			.CheckOnClick = True,
			.AutoSize = False,
			.Width = 30,
			.Height = 26
		}

		_tsUnderline = New ToolStripButton("U") With {
			.Font = New Font("Georgia", 10, FontStyle.Underline),
			.ToolTipText = "Underline (Ctrl+U)",
			.CheckOnClick = True,
			.AutoSize = False,
			.Width = 30,
			.Height = 26
		}

		_tsBullet = New ToolStripButton("• List") With {
			.ToolTipText = "Bullet List",
			.CheckOnClick = True,
			.AutoSize = True,
			.Height = 26
		}

		Dim btnTab As New ToolStripButton("⇥ Tab") With {
			.ToolTipText = "Indent (Tab)",
			.AutoSize = True,
			.Height = 26
		}

		AddHandler _tsBold.Click, Sub(s, e) ToggleBold()
		AddHandler _tsItalic.Click, Sub(s, e) ToggleItalic()
		AddHandler _tsUnderline.Click, Sub(s, e) ToggleUnderline()
		AddHandler _tsBullet.Click, Sub(s, e) ToggleBullet()
		AddHandler btnTab.Click, Sub(s, e) IndentRTB(True)

		ts.Items.AddRange({_tsBold, _tsItalic, _tsUnderline,
					   New ToolStripSeparator(), _tsBullet, btnTab})

		Dim parent = RTBCookingStep.Parent

		Dim rtbTop As Integer = RTBCookingStep.Top
		Dim rtbLeft As Integer = RTBCookingStep.Left
		Dim rtbWidth As Integer = RTBCookingStep.Width
		Dim rtbHeight As Integer = RTBCookingStep.Height

		Const TS_HEIGHT As Integer = 28

		ts.Location = New Point(rtbLeft, rtbTop - TS_HEIGHT)
		ts.Width = rtbWidth
		ts.Height = TS_HEIGHT

		RTBCookingStep.Top = rtbTop
		RTBCookingStep.Height = rtbHeight - TS_HEIGHT

		parent.Controls.Add(ts)
		ts.BringToFront()
	End Sub

	Private Sub Transaksi_Formula_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Public Sub Transaksi_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		TxtStatus.Text = StatusDariList
		InitRTBToolbar()

		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			'TabPage1.Text = Base_Language.Lang_Global_Bahan
			ListView1.Location = New Point(777, 122)
			LblFormulator_Total.Text = Base_Language.Lang_Global_Total
			LblFormulator_TotalPersen.Text = Base_Language.Lang_Global_TotalPersen
			'LblFormulator_Judul.Text = Base_Language.Lang_TransFormula_Judul
			LblFormulator_KodeBarang.Text = Base_Language.Lang_Global_KodeBarang
			LblFormulator_NamaBarang.Text = Base_Language.Lang_Global_NamaBarang
			LblFormulator_NoFaktur.Text = Base_Language.Lang_Global_NoFaktur
			LblFormulator_PenanggungJawab.Text = Base_Language.Lang_TransFormula_PenanggungJawab
			LblFormulator_Tanggal.Text = Base_Language.Lang_Global_Tanggal
			LblFormulator_LokasiInquiry.Text = Base_Language.Lang_Global_Lokasi
			LblFormulator_LokasiBarang.Text = Base_Language.Lang_Global_LokasiGudang
			LblFormulator_Hasil.Text = Base_Language.Lang_Global_Hasil

			BtnFormulator_Refresh.Text = Base_Language.Lang_Global_Refresh
			BtnFormulator_Simpan.Text = Base_Language.Lang_Global_Simpan

			DgvFormulator_StepFormulator.Columns(cellNo).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_No
			'DgvFormulator_StepFormulator.Columns(cellTipe).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Tipe
			'DgvFormulator_StepFormulator.Columns(cellKdBarang).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_KdBarang
			DgvFormulator_StepFormulator.Columns(cellNama).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Nama
			DgvFormulator_StepFormulator.Columns(cellQty).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Qty
			DgvFormulator_StepFormulator.Columns(cellSatuan).HeaderText = Base_Language.Lang_Global_Satuan
			DgvFormulator_StepFormulator.Columns(cellPengali).HeaderText = Base_Language.Lang_Global_Nilai_Pengali
			DgvFormulator_StepFormulator.Columns(cellSatuanBarang).HeaderText = Base_Language.Lang_Global_Satuan_Barang
			DgvFormulator_StepFormulator.Columns(cellNilaiBarang).HeaderText = Base_Language.Lang_Global_Nilai_Barang
			DgvFormulator_StepFormulator.Columns(cellPersentase).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Persentase
			DgvFormulator_StepFormulator.Columns(cellKet).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Keterangan

			'CmbFormulator_PenanggungJawab.Items.Clear() : arrIdPenanggungJawab.Clear()
			'SQL = "select Nama,Id_Karyawan from Emi_Karyawan a, Emi_Jabatan_Internal b where a.id_jabatan=b.id_jabatan "
			'SQL = SQL & "and b.flag_Tampil_Formulator='Y' and a.kode_perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "order by nama "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        CmbFormulator_PenanggungJawab.Items.Add(dr("Nama")) : arrIdPenanggungJawab.Add(dr("Id_Karyawan"))
			'    Loop
			'End Using

			CmbFormulator_LokasiInquiry.Items.Clear()

			SQL = "select Kode_Stock_Owner from stock_owner where Aktif='Y' "
			SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbFormulator_LokasiInquiry.Items.Add(dr("Kode_Stock_Owner"))
				Loop
			End Using

			CmbFormulator_LokasiInquiry.Text = Lokasi

			CmbFormulator_LokasiBarang.Items.Clear()

			SQL = "select Kode_Stock_Owner from view_lokasi_Stock where Aktif='Y' "
			SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbFormulator_LokasiBarang.Items.Add(dr("Kode_Stock_Owner"))
				Loop
			End Using

			Dim lokasi_gudang As String = ""
			SQL = "Select Kode_Stock_Owner_Gudang from Binding_Lokasi_Gudang where "
			SQL = SQL & "Kode_Stock_Owner='" & Lokasi & "' and Gudang_Default='Y' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					lokasi_gudang = dr("Kode_Stock_Owner_Gudang")
				End If
			End Using
			CmbFormulator_LokasiBarang.Text = lokasi_gudang

			'CmbFormulator_SatuanHasil.Items.Clear()
			'SQL = "select Satuan from EMI_Satuan where Flag_Tampil_Berat='Y' "
			'SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        CmbFormulator_SatuanHasil.Items.Add(dr("Satuan"))
			'    Loop
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Kosong()
	End Sub

	Private Sub Get_SD_Moisture(ByVal index As Integer)
		Lv_Moisture_ID = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_ID).Value
		Lv_Moisture_Kode_Analisa = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Kode_Analisa).Value
		Lv_Moisture_Jenis_Analisa = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Jenis_Analisa).Value
		Lv_Moisture_Flag_Perhitungan = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Flag_Perhitungan).Value
		Lv_Moisture_Aktivitas = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Kode_Aktivitas).Value
		Lv_Moisture_Range_Awal = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Range_Awal).Value
		Lv_Moisture_Range_Akhir = Dgv_Moisture_Content.Rows(index).Cells(Item_Moisture_Range_Akhir).Value

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

	Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
		lvNo = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellNo).Value)
		lvKdBarang = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellKdBarang).Value)
		lvNama = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellNama).Value)
		lvQty = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellQty).Value)
		lvSatuan = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellSatuan).Value)
		lvPengali = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellPengali).Value)
		lvSatuanBarang = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellSatuanBarang).Value)
		lvNilaiBarang = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellNilaiBarang).Value)
		lvPersentase = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellPersentase).Value)
		lvKet = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellKet).Value)
		lvQty_SatHasil = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellQty_SatHasil).Value)
		lvEstHPP = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellEstHPP).Value)
		lvEstHPPPcs = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellEstHPPPcs).Value)
	End Sub

	Public Sub Kosong()
		DgvFormulator_StepFormulator.Rows.Clear()
		'DgvFormulator_StepFormulator.Rows.Add(1)
		TxtFormulator_Total.Text = ""
		TxtFormulator_TotalPersen.Text = ""
		TxtFormulator_KodeBarang.Text = ""
		TxtFormulator_NamaBarang.Text = ""
		TxtFormulator_NoFaktur.Text = ""
		TxtFormulator_Hasil.Text = ""
		Txt_No_Faktur_Binding.Text = ""
		Txt_Total_Hpp.Text = ""
		Txt_Total_Hpp_Pcs.Text = ""

		CmbFormulator_PenanggungJawab.SelectedIndex = -1
		'CmbFormulator_LokasiInquiry.SelectedIndex = -1
		'CmbFormulator_LokasiBarang.SelectedIndex = -1
		CmbFormulator_SatuanHasil.SelectedIndex = -1
		ListView1.Visible = False

		get_jam()

		Try
			OpenConn()

			'get_no_faktur()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		CmbFormulator_LokasiInquiry.Focus()
	End Sub

	Private Sub Rekap()

		Dim jumlah_Stage As Integer = 1
		For index = 0 To DgvFormulator_StepFormulator.Rows.Count - 1
			Get_Isi_Listview(index)
			DgvFormulator_StepFormulator.Rows(index).Cells(cellNo).Value = index + 1

			If lvTipe = "Stage" Then
				DgvFormulator_StepFormulator.Rows(index).Cells(cellNama).Value = "Step " & jumlah_Stage
				jumlah_Stage += 1
			End If
		Next
	End Sub

	Private Sub Total()

		Dim Total As Double = 0
		Dim TotalPersen As Double = 0
		Dim Total_Hpp As Double = 0
		Dim Total_Hpp_Pcs As Double = 0

		For index = 0 To DgvFormulator_StepFormulator.Rows.Count - 1
			Get_Isi_Listview(index)

			If IsNumeric(lvQty) = True Then
				Total += Val(HilangkanTanda(lvQty_SatHasil))
				TotalPersen += Val(HilangkanTanda(lvPersentase))
				Total_Hpp += Val(HilangkanTanda(lvEstHPP))
				Total_Hpp_Pcs += Val(HilangkanTanda(lvEstHPPPcs))
			End If
		Next

		'TxtFormulator_Total.Text = Format(Total, "N0")
		'TxtFormulator_TotalPersen.Text = Format(TotalPersen, "N0")
		TxtFormulator_Total.Text = Format(Total, "N4")
		TxtFormulator_TotalPersen.Text = Format(TotalPersen, "N2")
		Txt_Total_Hpp.Text = Format(Total_Hpp, "N2")
		Txt_Total_Hpp_Pcs.Text = Format(Total_Hpp_Pcs, "N2")
	End Sub

	Private Sub DgvFormulator_StepFormulator_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFormulator_StepFormulator.CellEndEdit
		'If DgvFormulator_StepFormulator.Rows.Count = 0 Then
		'    Exit Sub
		'End If

		'Dim currentRow = DgvFormulator_StepFormulator.CurrentRow.Index
		'Dim currentCell = DgvFormulator_StepFormulator.CurrentCellAddress.X

		'Dim data = DgvFormulator_StepFormulator.Rows(currentRow).Cells(currentCell)

		'If currentCell = cellQty Or currentCell = cellPersentase Then
		'    If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
		'        MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
		'        If currentRow <> DgvFormulator_StepFormulator.Rows.Count - 1 Then
		'            BeginInvoke(New MethodInvoker(Sub() DgvFormulator_StepFormulator.Rows.RemoveAt(e.RowIndex)))
		'        End If
		'        TxtFormulator_KodeBarang.Focus()
		'        Exit Sub
		'    ElseIf TxtFormulator_Hasil.Text.Trim.Length = 0 Then
		'        MessageBox.Show(Base_Language.Lang_Global_Hasil & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
		'        If currentRow <> DgvFormulator_StepFormulator.Rows.Count - 1 Then
		'            BeginInvoke(New MethodInvoker(Sub() DgvFormulator_StepFormulator.Rows.RemoveAt(e.RowIndex)))
		'        End If
		'        TxtFormulator_Hasil.Focus()
		'        Exit Sub
		'    End If
		'End If

		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKdBarang).Value = ""
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNama).Value = ""
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Value = ""
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuan).Value = ""
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPengali).Value = ""
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuanBarang).Value = ""
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).Value = ""
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value = ""
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKet).Value = ""

		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKdBarang).ReadOnly = True
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNama).ReadOnly = True
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).ReadOnly = False
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuan).ReadOnly = True
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPengali).ReadOnly = True
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuanBarang).ReadOnly = True
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).ReadOnly = True
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).ReadOnly = True
		''DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKet).ReadOnly = False

		'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKdBarang).Style.BackColor = Color.White
		'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNama).Style.BackColor = Color.White
		'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Style.BackColor = Color.LightGray
		'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuan).Style.BackColor = Color.White
		'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPengali).Style.BackColor = Color.White
		'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuanBarang).Style.BackColor = Color.White
		'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).Style.BackColor = Color.White
		'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Style.BackColor = Color.White
		'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKet).Style.BackColor = Color.LightGray

		''isi_barang = False
		''SD_Pilih_Barang.lokasi()
		''SD_Pilih_Barang.asal = Jenis
		''SD_Pilih_Barang.filter_tambahan = " and b.Flag_Tampil_Formulator='Y' "
		''SD_Pilih_Barang.CmbPilihBarang_Lokasi.Text = CmbFormulator_LokasiBarang.Text
		''SD_Pilih_Barang.ShowDialog()

		''If isi_barang = False Then

		''    BeginInvoke(New MethodInvoker(Sub() DgvFormulator_StepFormulator.Rows.RemoveAt(e.RowIndex)))

		''End If

		'If currentCell = cellQty Or currentCell = cellPersentase Then
		'    Get_Isi_Listview(currentRow)
		'    If IsNumeric(lvQty) = False Or Val(lvQty) < 0 Then
		'        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Value = ""
		'        Exit Sub
		'    End If

		'    'If IsNumeric(lvPersentase) = False Or Val(lvPersentase) < 0 Then
		'    '    DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value = ""
		'    '    Exit Sub
		'    'End If

		'    Get_Isi_Listview(currentRow)

		'    If currentCell = cellQty Then
		'        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).Value = Val(lvQty) * Val(lvPengali)

		'        Dim nilai_satuan_hasil As Double = 0
		'        Try
		'            OpenConn()

		'            If lvQty.Contains(",") Then
		'                CloseConn()
		'                MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                DgvFormulator_StepFormulator.CurrentRow.Cells(cellQty).Value = ""
		'                Exit Sub
		'            End If

		'            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
		'            SQL = SQL & "'" & lvSatuan & "','" & CmbFormulator_SatuanHasil.Text & "',"
		'            SQL = SQL & "'" & lvQty & "') as Hasil "
		'            Using dr = OpenTrans(SQL)
		'                If dr.Read Then
		'                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
		'                        If dr("Hasil") = 0 Then
		'                            CloseConn()
		'                            MessageBox.Show("Satuan " & lvSatuan & " Ke " & CmbFormulator_SatuanHasil.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                            DgvFormulator_StepFormulator.CurrentRow.Cells(cellQty).Value = ""
		'                            Exit Sub
		'                        Else
		'                            nilai_satuan_hasil = dr("hasil")
		'                        End If
		'                    Else
		'                        CloseConn()
		'                        MessageBox.Show("Satuan " & lvSatuan & " Ke " & CmbFormulator_SatuanHasil.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                        Exit Sub
		'                    End If
		'                End If
		'            End Using
		'            CloseConn()
		'        Catch ex As Exception
		'            CloseConn()
		'            MessageBox.Show(ex.Message)
		'            Exit Sub
		'        End Try

		'        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty_SatHasil).Value = Format(nilai_satuan_hasil, "N5")
		'        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty_SatHasil).Value = Format(nilai_satuan_hasil, "N2")
		'        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value = Format((nilai_satuan_hasil * 100) / Val(TxtFormulator_Hasil.Text), "N5")
		'        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value = Format((nilai_satuan_hasil * 100) / Val(TxtFormulator_Hasil.Text), "N2")
		'    End If

		'    'If currentCell = cellPersentase Then
		'    '    Dim nilai_satuan_hasil As Double = 0
		'    '    Try
		'    '        OpenConn()

		'    '        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
		'    '        SQL = SQL & "'" & lvSatuan & "','" & CmbFormulator_SatuanHasil.Text & "',"
		'    '        SQL = SQL & "1) as Hasil "
		'    '        Using dr = OpenTrans(SQL)
		'    '            If dr.Read Then
		'    '                If General_Class.CekNULL(dr("Hasil")) <> "" Then
		'    '                    If dr("Hasil") = 0 Then
		'    '                        MessageBox.Show("Satuan " & lvSatuan & " Ke " & CmbFormulator_SatuanHasil.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    '                        Exit Sub
		'    '                    Else
		'    '                        nilai_satuan_hasil = dr("hasil")
		'    '                    End If
		'    '                Else
		'    '                    MessageBox.Show("Satuan " & lvSatuan & " Ke " & CmbFormulator_SatuanHasil.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    '                    Exit Sub
		'    '                End If
		'    '            End If
		'    '        End Using
		'    '        CloseConn()
		'    '    Catch ex As Exception
		'    '        CloseConn()
		'    '        MessageBox.Show(ex.Message)
		'    '        Exit Sub
		'    '    End Try

		'    '    Dim Persen As Decimal
		'    '    Dim Qty As Double

		'    '    Persen = Val(HilangkanTanda(DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value))

		'    '    DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Value = Format(Persen * Val(TxtFormulator_Hasil.Text) / (100 * (nilai_satuan_hasil)), "N0")

		'    '    Qty = Val(HilangkanTanda(DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Value))

		'    '    DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty_SatHasil).Value = Format(Qty * nilai_satuan_hasil, "N5")
		'    '    DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).Value = Qty * Val(lvPengali)
		'    'End If
		'End If

		'Dim kuantity As String = DgvFormulator_StepFormulator.CurrentRow.Cells(cellQty).Value

		'Dim nilai As Decimal = Decimal.Parse(kuantity)
		'Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

		'DgvFormulator_StepFormulator.CurrentRow.Cells(cellQty).Value = formattedValue

		'Rekap()
		'Total()
	End Sub

	Private Sub DgvFormulator_StepFormulator_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DgvFormulator_StepFormulator.CellPainting
		If DgvFormulator_StepFormulator.Rows.Count = 0 Or e.RowIndex = -1 Then
			Exit Sub
		End If

		If (e.ColumnIndex = cellKdBarang) Then
			e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
		End If

		If (e.ColumnIndex = cellNama) Then
			e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
		End If

		If (e.ColumnIndex = cellQty) Then
			e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
		End If

		If (e.ColumnIndex = cellSatuan) Then
			e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
		End If

		If (e.ColumnIndex = cellPengali) Then
			e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
		End If
		If (e.ColumnIndex = cellSatuanBarang) Then
			e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
		End If
		If (e.ColumnIndex = cellNilaiBarang) Then
			e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
		End If

		If (e.ColumnIndex = cellPersentase) Then
			e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
		End If
	End Sub

	Private Sub DgvFormulator_StepFormulator_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs)
		Rekap()
	End Sub

	Private Sub ComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Dim combo As ComboBox = CType(sender, ComboBox)
		Dim rowI As Integer = DgvFormulator_StepFormulator.CurrentCell.RowIndex

	End Sub

	Private Sub BtnFormulator_Simpan_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Simpan.Click

		If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			TxtFormulator_KodeBarang.Focus() : Exit Sub
		ElseIf CmbFormulator_LokasiInquiry.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Lokasi & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			CmbFormulator_LokasiInquiry.Focus() : Exit Sub
		ElseIf CmbFormulator_PenanggungJawab.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_TransFormula_PenanggungJawab & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			CmbFormulator_PenanggungJawab.Focus() : Exit Sub
		ElseIf DgvFormulator_StepFormulator.Rows.Count = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			DgvFormulator_StepFormulator.Focus() : Exit Sub
		ElseIf TxtFormulator_Hasil.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Hasil & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			TxtFormulator_Hasil.Focus() : Exit Sub
		ElseIf CmbFormulator_SatuanHasil.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			CmbFormulator_SatuanHasil.Focus() : Exit Sub
		End If

		get_jam()

		Try

			OpenConn()

			Cmd.Transaction = Cn.BeginTransaction

			'get_no_faktur()
			'get_no_faktur_binding()

			Dim sample As String = ""

			sample = "NULL"

			If Val(TxtFormulator_TotalPersen.Text) <> 100 Then
				CloseTrans()
				CloseConn()
				MessageBox.Show(Base_Language.Lang_Global_Persen_harus_100 & ". . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End If

			Dim kd_barangINq As String = ""
			SQL = "select top(1) Kode_Barang_inq from barang "
			SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' "
			SQL = SQL & "and Kode_Barang ='" & TxtFormulator_KodeBarang.Text & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					kd_barangINq = dr("Kode_Barang_inq")
				Else
					dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Exit Sub
				End If
			End Using
			SQL = "INSERT INTO Emi_Transaksi_Formulator "
			SQL = SQL & "(Kode_Perusahaan, No_Faktur, UserID, Tanggal, Jam,Flag_Sample, "
			SQL = SQL & "Kode_Barang, Penanggung_Jawab, Lokasi, Kode_Stock_Owner, Hasil, Satuan_Hasil) VALUES ( "
			SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & UserID & "', "
			SQL = SQL & "'" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', " & sample & ", "
			SQL = SQL & "'" & kd_barangINq & "', "
			SQL = SQL & "'" & arrIdPenanggungJawab.Item(CmbFormulator_PenanggungJawab.SelectedIndex) & "', '" & CmbFormulator_LokasiInquiry.Text & "', '" & CmbFormulator_LokasiBarang.Text & "', "
			SQL = SQL & "'" & HilangkanTanda(TxtFormulator_Hasil.Text) & "', '" & CmbFormulator_SatuanHasil.Text & "') "
			ExecuteTrans(SQL)

			For index = 0 To DgvFormulator_StepFormulator.Rows.Count - 2 'Karna data terakhir itu default dgv
				Get_Isi_Listview(index)

				lvQty = Val(HilangkanTanda(lvQty))
				lvPersentase = Val(HilangkanTanda(lvPersentase))

				SQL = "INSERT INTO EMI_Transaksi_Formulator_Detail_Step "
				SQL = SQL & "(Kode_Perusahaan, No_Faktur, No_Step, Tipe, Kode "
				SQL = SQL & ",Deskripsi,Jumlah, Satuan, Persentase, Nilai_Pengali, Satuan_barang, Nilai_Barang) VALUES( "
				SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & lvNo & "', "
				SQL = SQL & "'" & lvTipe & "', '" & lvKdBarang & "', '" & lvNama & "', "
				SQL = SQL & "'" & lvQty & "', '" & lvSatuan & "', '" & lvPersentase & "','" & lvPengali & "', '" & lvSatuanBarang & "', '" & lvNilaiBarang & "') "
				ExecuteTrans(SQL)

				'If lvTipe = "Raw" Then

				If lvQty = "" Or lvQty = "0" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show(Base_Language.Lang_TransFormula_DgvStepFormula_Qty & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Exit Sub
				ElseIf lvPersentase = "" Or lvQty = "0" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show(Base_Language.Lang_TransFormula_DgvStepFormula_Persentase & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Exit Sub
				End If

				SQL = "select Kode_Barang, Satuan from EMI_Transaksi_Formulator_Detail_Bahan "
				SQL = SQL & "where No_Faktur='" & TxtFormulator_NoFaktur.Text & "' and kode_Perusahaan='" & KodePerusahaan & "' "
				SQL = SQL & "and Kode_Barang ='" & lvKdBarang & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then

						If dr("Satuan") <> lvSatuan Then
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show(Base_Language.Lang_TransFormula_DgvStepFormula_Qty & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
							Exit Sub
						End If

						dr.Close()
						SQL = "update EMI_Transaksi_Formulator_Detail_Bahan set Jumlah=Jumlah+" & lvQty & ", Nilai_Barang=Nilai_Barang+" & lvNilaiBarang & ", "
						SQL = SQL & "persentase=persentase+" & HilangkanTanda(lvPersentase) & " "
						SQL = SQL & "where Kode_Barang='" & lvKdBarang & "'"
						ExecuteTrans(SQL)
					Else
						dr.Close()
						SQL = "INSERT INTO EMI_Transaksi_Formulator_Detail_Bahan "
						SQL = SQL & "(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang "
						SQL = SQL & ",Jumlah, Satuan, Persentase, Nilai_Pengali, Satuan_barang, Nilai_Barang, est_hpp, est_hpp_per_pcs) VALUES( "
						SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & CmbFormulator_LokasiBarang.Text & "', "
						SQL = SQL & "'" & lvKdBarang & "', '" & lvQty & "', '" & lvSatuan & "', '" & lvPersentase & "','" & lvPengali & "', '" & lvSatuanBarang & "', '" & lvNilaiBarang & "', "
						SQL = SQL & "'" & Val(HilangkanTanda(lvEstHPP)) & "', '" & Val(HilangkanTanda(lvEstHPPPcs)) & "') "
						ExecuteTrans(SQL)
					End If
				End Using
				'End If

			Next

			'===============================
			'=     SET BINDING FORMULA     =
			'===============================
			''Binding
			SQL = "update EMI_Transaksi_Formulator_Binding set aktif='T' "
			SQL = SQL & "where Kode_Barang='" & kd_barangINq & "' and Kode_Perusahaan='" & KodePerusahaan & "' and aktif='Y' "
			ExecuteTrans(SQL)

			SQL = "Insert into EMI_Transaksi_Formulator_Binding ("
			SQL = SQL & "Kode_Perusahaan, No_faktur, "
			SQL = SQL & "Kode_Customer, No_Inquiry, "
			SQL = SQL & "Tanggal, Jam, UserID, Kode_barang, Kode_formula, Aktif) "
			SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_No_Faktur_Binding.Text & "', "
			SQL = SQL & "NULL, NULL, "
			SQL = SQL & "'" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
			SQL = SQL & "'" & UserID & "','" & kd_barangINq & "', '" & TxtFormulator_NoFaktur.Text & "','Y')"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()

			CloseConn()
			MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()

	End Sub

	Private Sub TxtFormulator_Hasil_TextChanged(sender As Object, e As EventArgs) Handles TxtFormulator_Hasil.TextChanged

		DgvFormulator_StepFormulator.Rows.Clear()
		'DgvFormulator_StepFormulator.Rows.Add(1)

	End Sub

	Private Sub DgvFormulator_StepFormulator_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvFormulator_StepFormulator.KeyDown
		'If DgvFormulator_StepFormulator.Rows.Count = 0 Or DgvFormulator_StepFormulator.SelectedCells.Count = 0 Then
		'    Exit Sub
		'End If

		'Dim currentRow = DgvFormulator_StepFormulator.CurrentRow.Index
		'Dim currentCell = DgvFormulator_StepFormulator.CurrentCellAddress.X

		'If e.KeyCode = Keys.F1 Then
		'    SD_Pilih_Barang.lokasi()
		'    SD_Pilih_Barang.asal = Jenis
		'    SD_Pilih_Barang.filter_tambahan = " and b.Flag_Tampil_Formulator='Y' "
		'    SD_Pilih_Barang.CmbPilihBarang_Lokasi.Text = CmbFormulator_LokasiBarang.Text
		'    SD_Pilih_Barang.ShowDialog()
		'ElseIf e.KeyCode = Keys.Insert Then

		'    If currentRow <> DgvFormulator_StepFormulator.Rows.Count - 1 Then
		'        DgvFormulator_StepFormulator.Rows.Insert(currentRow + 1)
		'    End If
		'ElseIf e.KeyCode = Keys.Delete Then

		'    If currentRow <> DgvFormulator_StepFormulator.Rows.Count - 1 Then
		'        BeginInvoke(New MethodInvoker(Sub() DgvFormulator_StepFormulator.Rows.RemoveAt(currentRow)))
		'    End If
		'End If

		'Total()
	End Sub

	Private Sub TxtFormulator_Hasil_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFormulator_Hasil.KeyPress
		If e.KeyChar = Chr(13) Then CmbFormulator_SatuanHasil.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub CmbFormulator_PenanggungJawab_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbFormulator_PenanggungJawab.KeyPress
		If e.KeyChar = Chr(13) Then DgvFormulator_StepFormulator.Focus()
	End Sub

	Private Sub BtnFormulator_Refresh_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Refresh.Click
		Kosong()
	End Sub

	Private Sub TxtFormulator_KodeBarang_TextChanged(sender As Object, e As EventArgs) Handles TxtFormulator_KodeBarang.TextChanged
		'If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
		'    ListView1.Visible = False : Exit Sub
		'Else
		'    ListView1.Visible = True
		'    ListView1.Top = TxtFormulator_KodeBarang.Top + TxtFormulator_KodeBarang.Height + 6
		'    ListView1.Left = TxtFormulator_KodeBarang.Left
		'End If

		'ListView1.Items.Clear()

		'Try

		'    OpenConn()

		'    Dim Lvw2 As ListViewItem

		'    SQL = "select a.kode_barang, a.nama from barang a, emi_group_jenis b  where "
		'    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbFormulator_LokasiBarang.Text & "' and "
		'    SQL = SQL & "nama like '%" & TxtFormulator_KodeBarang.Text & "%' and a.id_group_jenis =b.id_group_jenis and (Flag_Finished_Good='Y' or Flag_Tampil_Inquiry='Y' or flag_semi_fg='Y' )"
		'    SQL = SQL & "order by a.kode_barang"
		'    Using Dr = OpenTrans(SQL)
		'        Do While Dr.Read
		'            Lvw2 = ListView1.Items.Add(Dr("kode_barang"))
		'            Lvw2.SubItems.Add(Dr("nama"))
		'        Loop
		'    End Using

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try
	End Sub

	Private Sub TxtFormulator_KodeBarang_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtFormulator_KodeBarang.KeyDown
		If e.KeyCode = Keys.Down Then ListView1.Focus()
	End Sub

	Private Sub TxtFormulator_KodeBarang_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFormulator_KodeBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
				ListView1.Visible = False : Exit Sub
			End If
			TxtFormulator_KodeBarang_Leave(TxtFormulator_KodeBarang, e)
		End If
	End Sub

	Private Sub TxtFormulator_KodeBarang_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFormulator_KodeBarang.Leave

		'    If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
		'        ListView1.Visible = False : Exit Sub
		'    Else
		'        ListView1.Visible = True
		'    End If
		'    If ListView1.Focused = True Then Exit Sub

		'    Try

		'        OpenConn()

		'        SQL = "select a.kode_barang, a.nama from barang a, emi_group_jenis b  where "
		'        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbFormulator_LokasiBarang.Text & "' and "
		'        SQL = SQL & "kode_barang like '%" & TxtFormulator_KodeBarang.Text & "%' and a.id_group_jenis =b.id_group_jenis and (Flag_Finished_Good='Y' or Flag_Tampil_Inquiry='Y'  or flag_semi_fg='Y' )"
		'        SQL = SQL & "order by a.kode_barang"
		'        Using Dr = OpenTrans(SQL)
		'            If Dr.Read Then
		'                TxtFormulator_KodeBarang.Text = Dr("kode_barang")
		'                TxtFormulator_NamaBarang.Text = Dr("nama")
		'                CmbFormulator_PenanggungJawab.Focus()
		'            Else
		'                TxtFormulator_KodeBarang.Text = "" : TxtFormulator_NamaBarang.Text = ""
		'                TxtFormulator_KodeBarang.Focus()
		'            End If
		'            ListView1.Visible = False
		'        End Using

		'        CloseConn()

		'    Catch ex As Exception
		'        CloseConn()
		'        MessageBox.Show(ex.Message)
		'        Exit Sub
		'    End Try
	End Sub

	Private Sub ListView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyDown
		If e.KeyCode = Keys.Enter Then
			ListView1_DoubleClick(ListView1, e)
		End If
	End Sub

	Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
		If ListView1.Items.Count = 0 Then Exit Sub

		Dim Kode As String = ListView1.FocusedItem.Text
		Dim Nama As String = ListView1.FocusedItem.SubItems(1).Text

		TxtFormulator_KodeBarang.Text = Kode
		TxtFormulator_NamaBarang.Text = Nama
		ListView1.Visible = False
	End Sub

	Private Sub CmbFormulator_LokasiBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFormulator_LokasiBarang.SelectedIndexChanged
		'TxtFormulator_KodeBarang.Text = ""
		'TxtFormulator_NamaBarang.Text = ""
	End Sub

	Private Sub DgvFormulator_StepFormulator_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvFormulator_StepFormulator.CellFormatting
		Try
			If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub

			' Logika untuk Kolom Qty
			If e.ColumnIndex = cellQty Then

				Dim jumlah As Double = Val(HilangkanTanda(e.Value.ToString()))
				Dim satuan As String = DgvFormulator_StepFormulator.Rows(e.RowIndex).Cells(cellSatuan).Value?.ToString()

				e.Value = $"{Format(jumlah, "N4")} {satuan}"
				e.FormattingApplied = True

			End If

			' Logika untuk Kolom Persentase
			If e.ColumnIndex = cellPersentase Then
				Dim persentase As Double = Val(HilangkanTanda(e.Value.ToString()))

				e.Value = $"{Format(persentase, "N2")} %"
				e.FormattingApplied = True
			End If
		Catch ex As Exception
			Debug.WriteLine("Error di CellFormatting: " & ex.Message)
		End Try
	End Sub

	Private Sub CmbFormulator_SatuanHasil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFormulator_SatuanHasil.SelectedIndexChanged
		DgvFormulator_StepFormulator.Rows.Clear()
		'DgvFormulator_StepFormulator.Rows.Add(1)
	End Sub

	Private Sub DgvFormulator_StepFormulator_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFormulator_StepFormulator.CellClick

		'If DgvFormulator_StepFormulator.CurrentCell.ColumnIndex = cellQty Then
		'    Dim cellKuantity As String = DgvFormulator_StepFormulator.CurrentCell.Value

		'    If cellKuantity = "" Then
		'        Exit Sub
		'    End If

		'    Dim cleanedStr As String = HilangkanTanda(cellKuantity)
		'    Dim nilai As Decimal = Decimal.Parse(cleanedStr)

		'    DgvFormulator_StepFormulator.CurrentCell.Value = nilai
		'End If

	End Sub

	Private Sub DgvFormulator_StepFormulator_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFormulator_StepFormulator.CellLeave

		'If DgvFormulator_StepFormulator.CurrentCell.ColumnIndex = cellQty Then
		'    Dim cellKuantity As String = DgvFormulator_StepFormulator.CurrentCell.Value

		'    If Not String.IsNullOrEmpty(cellKuantity) Then

		'        Dim nilai As Decimal = Decimal.Parse(cellKuantity)
		'        Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

		'        DgvFormulator_StepFormulator.CurrentCell.Value = formattedValue
		'    End If
		'End If

	End Sub

	Public Sub TxtFormulator_NoFaktur_Leave(sender As Object, e As EventArgs) Handles TxtFormulator_NoFaktur.Leave
		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
			Exit Sub
		End If

		Try
			OpenConn()

			Dim No_Faktur As String = TxtFormulator_NoFaktur.Text.Trim

			CmbFormulator_PenanggungJawab.Items.Clear() : arrIdPenanggungJawab.Clear()
			SQL = "select Nama,Id_Karyawan from Emi_Karyawan a, Emi_Jabatan_Internal b where a.id_jabatan=b.id_jabatan "
			SQL = SQL & "and b.flag_Tampil_Formulator='Y' and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "order by nama "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbFormulator_PenanggungJawab.Items.Add(dr("Nama")) : arrIdPenanggungJawab.Add(dr("Id_Karyawan"))
				Loop
			End Using

			CmbFormulator_SatuanHasil.Items.Clear()
			SQL = "select Satuan from EMI_Satuan where Flag_Tampil_Berat='Y' "
			SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbFormulator_SatuanHasil.Items.Add(dr("Satuan"))
				Loop
			End Using

			Dim Nomor As Integer = 1
			SQL = "select a.No_Faktur, a.Tanggal, a.Lokasi, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, a.UserID, a.Hasil, a.Satuan_Hasil, a.Penanggung_Jawab, "
			SQL &= $"c.Kode_Barang as Kode_Bahan, d.Nama as Nama_Bahan, c.Jumlah, c.satuan, c.Nilai_Pengali, c.Satuan_barang, c.Nilai_Barang, c.Persentase, "
			SQL &= $"case when exists( "
			SQL &= $"select 1 from Barang_SN z where c.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0 "
			SQL &= $") then ( "
			SQL &= $"select top 1 dbo.get_hpp(z.serial_number) from Barang_SN z where c.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0 "
			SQL &= $"order by z.tgl_masuk DESC) "
			SQL &= $"else d.estimasi_harga end Est_HPP, "

			SQL &= $"CASE WHEN EXISTS ( SELECT 1 FROM Barang_SN z WHERE  c.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 "
			SQL &= $") THEN ISNULL( dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100)))  "
			SQL &= $"* (SELECT TOP 1 dbo.get_hpp(z.serial_number)  "
			SQL &= $"FROM Barang_SN z  "
			SQL &= $"WHERE c.kode_barang = z.kode_barang  AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 ORDER BY z.tgl_masuk DESC), 0) "
			SQL &= $"ELSE ISNULL(dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100))) * d.estimasi_harga, 0) "
			SQL &= $"END AS Est_HPP_Pcs "

			SQL &= $"from Emi_Transaksi_Formulator a "
			SQL &= $"inner join Barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq "
			SQL &= $"inner join EMI_Transaksi_Formulator_Detail_Bahan c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Faktur "
			SQL &= $"inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Status is NULL "
			SQL &= $"and a.No_Faktur = '{No_Faktur}' "
			SQL &= $"order by d.nama "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							DtpFormulator_Tanggal.Value = .Rows(i).Item("Tanggal")
							CmbFormulator_LokasiInquiry.Text = .Rows(i).Item("Lokasi")
							CmbFormulator_LokasiBarang.Text = .Rows(i).Item("Kode_Stock_Owner")
							TxtFormulator_KodeBarang.Text = .Rows(i).Item("Kode_Barang")
							TxtFormulator_NamaBarang.Text = .Rows(i).Item("Nama_Barang")
							TxtFormulator_Hasil.Text = Format(.Rows(i).Item("Hasil"), "N4")
							CmbFormulator_SatuanHasil.SelectedItem = .Rows(i).Item("Satuan_Hasil").ToString.Trim

							Dim idPJ As Integer = CInt(HilangkanTanda(.Rows(i).Item("Penanggung_Jawab").ToString.Trim))
							Dim index As Integer = arrIdPenanggungJawab.IndexOf(idPJ)
							CmbFormulator_PenanggungJawab.SelectedIndex = index

							DgvFormulator_StepFormulator.Rows.Add(1)
							DgvFormulator_StepFormulator.Rows(i).Cells(cellNo).Value = Nomor
							DgvFormulator_StepFormulator.Rows(i).Cells(cellKdBarang).Value = .Rows(i).Item("Kode_Bahan")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellNama).Value = .Rows(i).Item("Nama_Bahan")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellQty).Value = Format(.Rows(i).Item("Jumlah"), "N4")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellSatuan).Value = .Rows(i).Item("satuan")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellPengali).Value = Format(.Rows(i).Item("Nilai_Pengali"), "N4")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellSatuanBarang).Value = .Rows(i).Item("Satuan_barang")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellNilaiBarang).Value = Format(.Rows(i).Item("Nilai_Barang"), "N4")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellPersentase).Value = Format(.Rows(i).Item("Persentase"), "N2")

							If General_Class.CekNULL(.Rows(i).Item("Est_HPP")) = "" Then
								DgvFormulator_StepFormulator.Rows(i).Cells(cellEstHPP).Value = 0
							Else
								DgvFormulator_StepFormulator.Rows(i).Cells(cellEstHPP).Value = Format(.Rows(i).Item("Est_HPP"), "N2")
							End If

							If General_Class.CekNULL(.Rows(i).Item("Est_HPP_Pcs")) = "" Then
								DgvFormulator_StepFormulator.Rows(i).Cells(cellEstHPPPcs).Value = 0
							Else
								DgvFormulator_StepFormulator.Rows(i).Cells(cellEstHPPPcs).Value = Format(.Rows(i).Item("Est_HPP_Pcs"), "N2")
							End If

							Nomor += 1

						Next
					Else
						CloseConn()
						MessageBox.Show($"Detail Bbahan Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Me.Close()
						Exit Sub
					End If
				End With
			End Using

			SQL = "select No_Faktur from EMI_Transaksi_Formulator_Binding "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and Kode_Formula = '{No_Faktur}' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					Txt_No_Faktur_Binding.Text = Ds.Tables("MyTable").Rows(0).Item("No_Faktur")
				Else
					CloseConn()
					MessageBox.Show($"No Faktur Formula Binding Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Me.Close()
					Exit Sub
				End If
			End Using

			'======================================
			'=     LOAD DATA MOISTURE CONTENT     =
			'======================================
			Dgv_Moisture_Content.Rows.Clear()
			SQL = "select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, '-' as Value_Combobox, a.Range_Awal, a.Range_Akhir "
			SQL &= $"from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a "
			SQL &= $"inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.No_Formula = '{No_Faktur}' "
			SQL &= $"union all "
			SQL &= $"select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, c.label_keterangan as Value_Combobox, '' as Range_Awal, '' as Range_Akhir "
			SQL &= $"from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a "
			SQL &= $"inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id "
			SQL &= $"inner join EMI_Switch c on a.Kode_Perusahaan = c.kode_perusahaan and a.nilai_kriteria = c.keterangan "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.No_Formula = '{No_Faktur}' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dgv_Moisture_Content.Rows.Add()

							Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(.Rows(i).Item("Flag_Perhitungan")) = "", "T", .Rows(i).Item("Flag_Perhitungan"))

							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_ID).Value = .Rows(i).Item("Id")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Kode_Analisa).Value = .Rows(i).Item("Kode_Analisa")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Jenis_Analisa).Value = .Rows(i).Item("Jenis_Analisa")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Flag_Perhitungan).Value = Flag_Perhitungan_Analisa
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Kode_Aktivitas).Value = .Rows(i).Item("Kode_Aktivitas_Lab")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Kategori).Value = If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Combobox).Value = .Rows(i).Item("Value_Combobox")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Range_Awal).Value = .Rows(i).Item("Range_Awal")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Range_Akhir).Value = .Rows(i).Item("Range_Akhir")

							If .Rows(i).Item("Value_Combobox").ToString.Trim = "-" Then
								Dim Text As String = $"{ .Rows(i).Item("Range_Awal")} Sampai { .Rows(i).Item("Range_Akhir")}"
								Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Value).Value = Text
							Else
								Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Value).Value = .Rows(i).Item("Value_Combobox")
							End If

						Next
					End If
				End With
			End Using

			RTBCookingStep.Clear()
			SQL = "select top 1 Cooking_Step from Emi_Transaksi_Formulator_Cooking_Steps "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and No_Faktur = '{No_Faktur}' "
			SQL &= $"and Status is null "
			SQL &= $"order by Tanggal desc, Jam desc "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Cooking_Step")) <> "" Then
						RTBCookingStep.Rtf = Dr("Cooking_Step")
					End If
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		LoadAndArrangeButtons(TxtFormulator_NoFaktur.Text.Trim)
		Total()

	End Sub

	Private Sub BtnTrialKitchen_Click(sender As Object, e As EventArgs) Handles BtnTrialKitchen.Click
		ProsesFlag("Trial Kitchen", "flag_lanjut_trial_kitchen")
	End Sub

	Private Sub BtnTrialProduksi_Click(sender As Object, e As EventArgs) Handles BtnTrialProduksi.Click
		ProsesFlag("Trial Produksi", "flag_lanjut_trial_produksi")
	End Sub

	Private Sub BtnProduksi_Click(sender As Object, e As EventArgs) Handles BtnProduksi.Click
		ProsesFlag("Produksi", "flag_lanjut_produksi")
	End Sub

	Private Sub ProsesFlag(namaProses As String, namaKolom As String, Optional namaKolomSelesai As String = "")
		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show($"Yakin ingin lanjut ke {namaProses}?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim extraFlag As String = ""
			If namaKolomSelesai <> "" Then
				Dim flagSebelumnya As String = ""
				SQL = $"select {namaKolomSelesai.Replace("flag_selesai", "flag_lanjut")} from Emi_Transaksi_Formulator where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{TxtFormulator_NoFaktur.Text.Trim}'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						flagSebelumnya = General_Class.CekNULL(Dr(0).ToString())
					End If
				End Using

				If flagSebelumnya = "Y" Then
					extraFlag = $", {namaKolomSelesai} = 'Y'"
				End If
			End If

			SQL = $"update Emi_Transaksi_Formulator set {namaKolom} = 'Y'{extraFlag} where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{TxtFormulator_NoFaktur.Text.Trim}'"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show($"Berhasil Lanjut ke {namaProses}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'N_EMI_Transaksi_Validasi_Formula_Main.Kosong()
		'N_EMI_Dashboard_Formula.Kosong()
		Me.Close()
	End Sub

	Private Sub Btn_Validasi_Click(sender As Object, e As EventArgs) Handles Btn_Validasi.Click, Btn_Tolak.Click
		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf DgvFormulator_StepFormulator.Rows.Count = 0 Then
			MessageBox.Show("Detail Bahan Gagal Memuat ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf RTBCookingStep.Text.Trim.Length = 0 Then
			MessageBox.Show("Harap Input Cooking Step Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim Action As String = ""
		Dim Flag_Validasi As String = ""
		If sender Is Btn_Validasi Then
			Action = "validasi"
			Flag_Validasi = "'Y'"
		ElseIf sender Is Btn_Tolak Then
			Action = "tolak"
			Flag_Validasi = "'T'"
		End If

		'==============================================
		'=     CEK MINIMAL ADA 1 MOISTURE CONTENT     =
		'==============================================
		If Dgv_Moisture_Content.Rows.Count = 0 Then
			MessageBox.Show("Data Moisture Content Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show($"Yakin ingin {Action} faktur ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===========================
			'=     CEK ROLE BUTTON     =
			'===========================
			If CekButtonRole("Validasi_Hpp_Formula") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Validasi Hpp Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'=======================
			'=     CEK FORMULA     =
			'=======================
			SQL = "select Status, Flag_Validasi, Flag_Validasi_Main from Emi_Transaksi_Formulator "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{TxtFormulator_NoFaktur.Text.Trim}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Faktur {TxtFormulator_NoFaktur.Text.Trim} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					ElseIf General_Class.CekNULL(Dr("Flag_Validasi")) <> "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Faktur {TxtFormulator_NoFaktur.Text.Trim} Belum Divalidasi Formulator", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"No Faktur {TxtFormulator_NoFaktur.Text.Trim} Tidak Ditemukan pada Transaksi Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = $"insert into Emi_Transaksi_Formulator_Cooking_Steps (Kode_Perusahaan, No_Faktur, Status, UserID, Tanggal, Jam, Cooking_Step) values ('{KodePerusahaan}', '{TxtFormulator_NoFaktur.Text.Trim}', NULL, '{UserID}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{RTBCookingStep.Rtf.Replace("'", "''")}')"
			ExecuteTrans(SQL)

			Dim FlagField As String = ""
			Dim TanggalField As String = ""
			Dim JamField As String = ""
			Dim UserField As String = ""

			SQL = $"
				SELECT
					Flag_Lanjut_Trial_Kitchen,
					Flag_Lanjut_Trial_Produksi,
					Flag_Lanjut_Produksi
				FROM Emi_Transaksi_Formulator
				WHERE
					Kode_Perusahaan = '{KodePerusahaan}'
					AND No_Faktur = '{TxtFormulator_NoFaktur.Text.Trim}'
			"

			Using DR = OpenTrans(SQL)
				If DR.Read Then
					If IsDBNull(DR("Flag_Lanjut_Produksi")) OrElse DR("Flag_Lanjut_Produksi").ToString.Trim = "" Then
						FlagField = "Flag_Lanjut_Produksi"
						TanggalField = "Tanggal_Lanjut_Produksi"
						JamField = "Jam_Lanjut_Produksi"
						UserField = "UserID_Lanjut_Produksi"

					ElseIf IsDBNull(DR("Flag_Lanjut_Trial_Produksi")) OrElse DR("Flag_Lanjut_Trial_Produksi").ToString.Trim = "" Then
						FlagField = "Flag_Lanjut_Trial_Produksi"
						TanggalField = "Tanggal_Lanjut_Trial_Produksi"
						JamField = "Jam_Lanjut_Trial_Produksi"
						UserField = "UserID_Lanjut_Trial_Produksi"

					ElseIf IsDBNull(DR("Flag_Lanjut_Trial_Kitchen")) OrElse DR("Flag_Lanjut_Trial_Kitchen").ToString.Trim = "" Then
						FlagField = "Flag_Lanjut_Trial_Kitchen"
						TanggalField = "Tanggal_Lanjut_Trial_Kitchen"
						JamField = "Jam_Lanjut_Trial_Kitchen"
						UserField = "UserID_Lanjut_Trial_Kitchen"
					End If
				End If
			End Using

			SQL = $"
				UPDATE Emi_Transaksi_Formulator
				SET
					{FlagField} = 'T',
					{TanggalField} = '{Format(tgl_skg, "yyyy-MM-dd")}',
					{JamField} = '{Format(tgl_skg, "HH:mm:ss")}',
					{UserField} = '{UserID}'
				WHERE
					Kode_Perusahaan = '{KodePerusahaan}'
					AND No_Faktur = '{TxtFormulator_NoFaktur.Text.Trim}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Data Berhasil Di{Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'N_EMI_Transaksi_Validasi_Formula_Main.Kosong()
		'Me.Close()

		'N_EMI_Dashboard_Formula.Kosong()
		Me.Close()

	End Sub

	Private Sub RTBCookingStep_KeyDown(sender As Object, e As KeyEventArgs) Handles RTBCookingStep.KeyDown
		If e.Control AndAlso e.KeyCode = Keys.B Then
			ToggleBold() : e.SuppressKeyPress = True : Return
		End If
		If e.Control AndAlso e.KeyCode = Keys.I Then
			ToggleItalic() : e.SuppressKeyPress = True : Return
		End If
		If e.Control AndAlso e.KeyCode = Keys.U Then
			ToggleUnderline() : e.SuppressKeyPress = True : Return
		End If
		If e.KeyCode = Keys.Tab AndAlso Not e.Shift Then
			IndentRTB(True) : e.SuppressKeyPress = True : Return
		End If
		If e.KeyCode = Keys.Back Then
			Dim lineIndex = RTBCookingStep.GetLineFromCharIndex(RTBCookingStep.SelectionStart)
			Dim lineStart = RTBCookingStep.GetFirstCharIndexFromLine(lineIndex)
			Dim cursorPos = RTBCookingStep.SelectionStart
			If cursorPos = lineStart AndAlso RTBCookingStep.SelectionIndent > 0 Then
				IndentRTB(False)
				e.SuppressKeyPress = True : Return
			End If
		End If
	End Sub

	Private Sub RTBCookingStep_SelectionChanged(sender As Object, e As EventArgs) Handles RTBCookingStep.SelectionChanged
		If RTBCookingStep.SelectionFont Is Nothing Then Return
		If _tsBold Is Nothing Then Return

		_tsBold.Checked = RTBCookingStep.SelectionFont.Bold
		_tsItalic.Checked = RTBCookingStep.SelectionFont.Italic
		_tsUnderline.Checked = RTBCookingStep.SelectionFont.Underline

		Dim lineIdx As Integer = RTBCookingStep.GetLineFromCharIndex(RTBCookingStep.SelectionStart)
		If lineIdx >= 0 AndAlso lineIdx < RTBCookingStep.Lines.Length Then
			_tsBullet.Checked = RTBCookingStep.Lines(lineIdx).StartsWith("• ")
		End If
	End Sub

	Private Sub IndentRTB(addIndent As Boolean)
		Const INDENT As Integer = 40
		Dim firstLine = RTBCookingStep.GetLineFromCharIndex(RTBCookingStep.SelectionStart)
		Dim lastLine = RTBCookingStep.GetLineFromCharIndex(RTBCookingStep.SelectionStart + RTBCookingStep.SelectionLength)
		Dim savedStart = RTBCookingStep.SelectionStart
		Dim savedLen = RTBCookingStep.SelectionLength
		For i = firstLine To lastLine
			RTBCookingStep.SelectionStart = RTBCookingStep.GetFirstCharIndexFromLine(i)
			RTBCookingStep.SelectionLength = 0
			RTBCookingStep.SelectionIndent = Math.Max(0, RTBCookingStep.SelectionIndent + If(addIndent, INDENT, -INDENT))
		Next
		RTBCookingStep.SelectionStart = savedStart
		RTBCookingStep.SelectionLength = savedLen
	End Sub

	Private Sub ToggleBold()
		If RTBCookingStep.SelectionFont Is Nothing Then Return
		Dim s = RTBCookingStep.SelectionFont.Style
		RTBCookingStep.SelectionFont = New Font(RTBCookingStep.SelectionFont,
		If((s And FontStyle.Bold) = FontStyle.Bold, s And Not FontStyle.Bold, s Or FontStyle.Bold))
	End Sub

	Private Sub ToggleItalic()
		If RTBCookingStep.SelectionFont Is Nothing Then Return
		Dim s = RTBCookingStep.SelectionFont.Style
		RTBCookingStep.SelectionFont = New Font(RTBCookingStep.SelectionFont,
		If((s And FontStyle.Italic) = FontStyle.Italic, s And Not FontStyle.Italic, s Or FontStyle.Italic))
	End Sub

	Private Sub ToggleUnderline()
		If RTBCookingStep.SelectionFont Is Nothing Then Return
		Dim s = RTBCookingStep.SelectionFont.Style
		RTBCookingStep.SelectionFont = New Font(RTBCookingStep.SelectionFont,
		If((s And FontStyle.Underline) = FontStyle.Underline, s And Not FontStyle.Underline, s Or FontStyle.Underline))
	End Sub

	Private Sub ToggleBullet()
		Dim selStart As Integer = RTBCookingStep.SelectionStart
		Dim selLen As Integer = RTBCookingStep.SelectionLength
		Dim firstLine As Integer = RTBCookingStep.GetLineFromCharIndex(selStart)
		Dim lastLine As Integer = RTBCookingStep.GetLineFromCharIndex(selStart + selLen)
		Dim firstLineStart As Integer = RTBCookingStep.GetFirstCharIndexFromLine(firstLine)
		Dim firstLineText As String = ""
		If firstLine < RTBCookingStep.Lines.Length Then
			firstLineText = RTBCookingStep.Lines(firstLine)
		End If
		Dim sudahBullet As Boolean = firstLineText.StartsWith("• ")

		RTBCookingStep.SuspendLayout()

		For i As Integer = firstLine To lastLine
			Dim lineStart As Integer = RTBCookingStep.GetFirstCharIndexFromLine(i)
			If lineStart < 0 Then Continue For
			Dim lineText As String = ""
			If i < RTBCookingStep.Lines.Length Then
				lineText = RTBCookingStep.Lines(i)
			End If

			If sudahBullet Then
				If lineText.StartsWith("• ") Then
					RTBCookingStep.SelectionStart = lineStart
					RTBCookingStep.SelectionLength = 2
					RTBCookingStep.SelectedText = ""
					RTBCookingStep.SelectionStart = RTBCookingStep.GetFirstCharIndexFromLine(i)
					RTBCookingStep.SelectionLength = 0
					RTBCookingStep.SelectionIndent = 0
					RTBCookingStep.SelectionHangingIndent = 0
				End If
			Else
				RTBCookingStep.SelectionStart = lineStart
				RTBCookingStep.SelectionLength = 0
				Dim f As Font = If(RTBCookingStep.SelectionFont, RTBCookingStep.Font)
				RTBCookingStep.SelectedText = "• "
				RTBCookingStep.SelectionStart = lineStart
				RTBCookingStep.SelectionLength = 2
				RTBCookingStep.SelectionFont = f
				RTBCookingStep.SelectionIndent = 10
				RTBCookingStep.SelectionHangingIndent = 0
				RTBCookingStep.SelectionStart = lineStart + 2
				RTBCookingStep.SelectionLength = 0
			End If
		Next

		If firstLine <> lastLine Then
			Dim lastLineStart As Integer = RTBCookingStep.GetFirstCharIndexFromLine(lastLine)
			If lastLineStart >= 0 AndAlso lastLine < RTBCookingStep.Lines.Length Then
				RTBCookingStep.SelectionStart = lastLineStart + RTBCookingStep.Lines(lastLine).Length
				RTBCookingStep.SelectionLength = 0
			End If
		End If

		RTBCookingStep.ResumeLayout()

		If Not sudahBullet AndAlso firstLine = lastLine Then
			Dim pos As Integer = RTBCookingStep.GetFirstCharIndexFromLine(firstLine)
			If pos >= 0 Then
				RTBCookingStep.SelectionStart = pos + 2
				RTBCookingStep.SelectionLength = 0
			End If
		End If

		If _tsBullet IsNot Nothing Then
			_tsBullet.Checked = Not sudahBullet
		End If
		RTBCookingStep.Focus()
	End Sub

	Private Sub Btn_Validasi_Produksi_Click(sender As Object, e As EventArgs) Handles Btn_Validasi_Produksi.Click
		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf DgvFormulator_StepFormulator.Rows.Count = 0 Then
			MessageBox.Show("Detail Bahan Gagal Memuat ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		'==============================================
		'=     CEK MINIMAL ADA 1 MOISTURE CONTENT     =
		'==============================================
		If Dgv_Moisture_Content.Rows.Count = 0 Then
			MessageBox.Show("Data Moisture Content Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show($"Yakin ingin Validasi Produksi Faktur Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Dim listSatuan As New List(Of String)
		Try
			OpenConn()

			listSatuan.Clear()
			SQL = "select Satuan from EMI_Satuan where Flag_Tampil_Berat='Y' "
			SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					listSatuan.Add(dr("Satuan").ToString())
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		With N_EMI_SD_Transaksi_Validasi_Formula_Main_Produksi
			.Txt_NoFormula.Text = TxtFormulator_NoFaktur.Text.Trim
			.Txt_Kd_Barang.Text = TxtFormulator_KodeBarang.Text.Trim
			.Txt_NmBarang.Text = TxtFormulator_NamaBarang.Text.Trim
			.Txt_Hasil.Text = Format(Val(HilangkanTanda(TxtFormulator_Hasil.Text)), "N4")
			.Cmb_Satuan.Items.Clear()
			.Cmb_Satuan.Items.AddRange(listSatuan.ToArray)
			.Cmb_Satuan.SelectedItem = CmbFormulator_SatuanHasil.SelectedItem
			.ShowDialog()
		End With

	End Sub

	Private Sub CmbFormulator_SatuanHasil_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbFormulator_SatuanHasil.KeyPress
		If e.KeyChar = Chr(13) Then BtnFormulator_Simpan.Focus()
	End Sub

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub LoadAndArrangeButtons(No_Faktur As String)
		Dim flagValidasiMain As String = ""
		Dim flagTrialKitchen As String = ""
		Dim flagTrialProduksi As String = ""
		Dim flagProduksi As String = ""

		Try
			OpenConn()
			SQL = "select Flag_Validasi_Main, Flag_Lanjut_Trial_Kitchen, Flag_Lanjut_Trial_Produksi, Flag_Lanjut_Produksi "
			SQL &= $"from Emi_Transaksi_Formulator "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{No_Faktur}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					flagValidasiMain = General_Class.CekNULL(Dr("Flag_Validasi_Main"))
					flagTrialKitchen = General_Class.CekNULL(Dr("Flag_Lanjut_Trial_Kitchen"))
					flagTrialProduksi = General_Class.CekNULL(Dr("Flag_Lanjut_Trial_Produksi"))
					flagProduksi = General_Class.CekNULL(Dr("Flag_Lanjut_Produksi"))
				End If
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		ArrangeButtons(flagValidasiMain, flagTrialKitchen, flagTrialProduksi, flagProduksi)
	End Sub

	Private Sub ArrangeButtons(flagValidasiMain As String, flagTrialKitchen As String,
						  flagTrialProduksi As String, flagProduksi As String)

		If flagTrialProduksi = "Y" Then
			BtnTrialKitchen.Enabled = False
		Else
			BtnTrialKitchen.Enabled = Not (flagTrialKitchen = "Y")
		End If
		BtnTrialProduksi.Enabled = Not (flagTrialProduksi = "Y")
		BtnProduksi.Enabled = Not (flagProduksi = "Y")
		Btn_Tolak.Enabled = Not (flagProduksi = "Y")
	End Sub

	Private Sub Load_Laporan()
		Try
			OpenConn()

			SQL = $"
                SELECT No_Transaksi, No_Faktur, Tanggal
                FROM N_EMI_View_Laporan_formula_rpt
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                AND No_Faktur = '{TxtFormulator_NoFaktur.Text.Trim}'
                GROUP BY No_Transaksi, No_Faktur, Tanggal
            "

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					DGVLaporan.Rows.Clear()

					For i As Integer = 0 To .Rows.Count - 1
						Dim row = .Rows(i)

						DGVLaporan.Rows.Add()
						With DGVLaporan.Rows(i).Cells
							.Item(0).Value = row("No_Transaksi")
							.Item(1).Value = "Trial Kitchen"
							.Item(2).Value = Format(row("Tanggal"), "yyyy-MM-dd")
							.Item(3).Value = "Cetak Laporan"
						End With

						DGVLaporan.Rows(i).Tag = row("No_Faktur")
					Next
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal mendapatkan laporan: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try
	End Sub

	Private Sub DGVLaporan_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVLaporan.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		If e.ColumnIndex = 3 Then
			Dim No_Faktur As String = DGVLaporan.Rows(e.RowIndex).Tag.ToString()

			Me.Cursor = Cursors.WaitCursor
			Try
				Application.DoEvents()

				Dim no_split As String = DGVLaporan.Rows(e.RowIndex).Cells(0).Value.ToString()

				Dim pdfStream As MemoryStream = GetPdfStream(Url_Api_Laporan_Formulator, no_split)

				Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

				Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
					pdfStream.CopyTo(file)
				End Using

				Dim frm As New N_EMI_PDF_Viewer(tempPath)
				frm.WindowState = FormWindowState.Maximized
				frm.Show()

				Me.Cursor = Cursors.Default
			Catch ex As Exception
				MessageBox.Show($"Gagal mendapatkan laporan {No_Faktur}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
				Me.Cursor = Cursors.Default
				Exit Sub
			End Try
		Else
		End If
	End Sub

	Public Function GetPdfStream(url As String, no_split As String) As MemoryStream
		' ----------------------------------------------------------------
		' 1. Query bahan & hpp dari DB
		' ----------------------------------------------------------------
		Dim bahan As New List(Of Dictionary(Of String, Object))
		Dim hpp As New Dictionary(Of String, Object)
		Dim namaFormula As String = ""
		Dim kategoriProduk As String = ""
		Dim tanggalUji As String = ""

		Try
			OpenConn()

			SQL = $"SELECT TOP 1 kode_formula, nama_produk, FORMAT(tanggal, 'dd MMM yyyy') AS tanggal_uji FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					namaFormula = Dr("kode_formula").ToString()
					kategoriProduk = Dr("nama_produk").ToString()
					tanggalUji = Dr("tanggal_uji").ToString()
				End If
			End Using

			' Query bahan
			SQL = $"SELECT Nama_bahan, Jumlah, Persentase FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}' GROUP BY kode_formula, nama_produk, tanggal, No_Transaksi, Nama_bahan, Jumlah, Persentase"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read()
					bahan.Add(New Dictionary(Of String, Object) From {
					{"Nama_bahan", Dr("Nama_bahan")},
					{"Jumlah", Dr("Jumlah")},
					{"Persentase", Dr("Persentase")}
				})
				Loop
			End Using

			' Query hpp
			SQL = $"SELECT SUM(Est_HPP_Per_Pcs) AS hpp_bahan_baku, HPP_Packaging, HPP_produksi, 'Per ' + satuan AS satuan FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}' GROUP BY kode_formula, nama_produk, tanggal, No_Transaksi, satuan, HPP_Produksi, HPP_Packaging"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					hpp("hpp_bahan_baku") = Dr("hpp_bahan_baku")
					hpp("hpp_packaging") = Dr("hpp_packaging")
					hpp("hpp_produksi") = Dr("hpp_produksi")
					hpp("satuan") = Dr("satuan")
				End If
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Return Nothing
		End Try

		' ----------------------------------------------------------------
		' 2. Serialize ke JSON & kirim ke API
		' ----------------------------------------------------------------
		Dim payload As New Dictionary(Of String, Object) From {
			{"no_split", no_split},
			{"nama_formula", namaFormula},
			{"kategori_produk", kategoriProduk},
			{"tanggal_uji", tanggalUji},
			{"bahan", bahan},
			{"hpp", hpp}
		}

		Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
		Dim signature As String = GenerateHmac(json, Secret_Api_Laporan_Formulator)

		Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
		request.Method = "POST"
		request.ContentType = "application/json"
		request.Accept = "*/*"
		request.UserAgent = "Mozilla/5.0"
		request.Headers.Add("X-Signature", signature)

		Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(json)
		request.ContentLength = bytes.Length
		Using stream = request.GetRequestStream()
			stream.Write(bytes, 0, bytes.Length)
		End Using

		Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
		Dim ms As New MemoryStream()
		Using responseStream = response.GetResponseStream()
			responseStream.CopyTo(ms)
		End Using
		ms.Position = 0
		Return ms
	End Function

End Class
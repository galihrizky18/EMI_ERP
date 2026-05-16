Imports System.IO
Imports System.Net

Public Class N_EMI_Dashboard_Formula
	Private ActiveMenuKomponen As Label
	Private ActiveMenuTrackingProgress As Label

	Dim CellParent_NoFormula As Integer = 0
	Dim CellParent_TanggalFormula As Integer = 1
	Dim CellParent_KdBarang As Integer = 2
	Dim CellParent_NmBarang As Integer = 3
	Dim CellParent_HPPMin As Integer = 4
	Dim CellParent_HPPMax As Integer = 5
	Dim CellParent_Jumlah As Integer = 6
	Dim CellParent_Satuan As Integer = 7
	Dim CellParent_JenisFormula As Integer = 8
	Dim CellParent_PosisiBinding As Integer = 9
	Dim CellParent_StatusFormula As Integer = 10
	Dim CellParent_Deskripsi As Integer = 11
	Dim CellParent_BtnValidasi As Integer = 12

	Dim UserPosition As String = ""

	Dim Status_HeadDept As String() = {"Belum Diproses", "Selesai Trial Kitchen", "Selesai Trial Produksi"}
	Dim Status_BOD As String() = {"Menunggu Validasi BOD", "Proses Produksi Komersial"}

	Dim DgvParent_NoFormula, DgvParent_TanggalFormula, DgvParent_KdBarang, DgvParent_NmBarang, DgvParent_HPPMin, DgvParent_HPPMax, DgvParent_Jumlah, DgvParent_Satuan,
		DgvParent_JenisFormula, DgvParent_PosisiBinding, DgvParent_StatusFormula, DgvParent_Deskripsi, DgvParent_BtnValidasi As String

	Dim list As New List(Of PalatRow)
	Dim headerSet As New HashSet(Of String)

	Private Sub N_EMI_Dashboard_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.Dock = DockStyle.Fill

		'===========================
		'=     CEK BUTTON ROLE     =
		'===========================
		Try
			OpenConn()

			If CekButtonRole("User_Formula_Position_Staff") = "Y" Then
				UserPosition = "STAFF"
				CloseTrans()
				CloseConn()
				Exit Try
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal cek role user: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try

		Try
			OpenConn()

			If CekButtonRole("User_Formula_Position_HeadDept") = "Y" Then
				UserPosition = "HEADDEPT"
				CloseTrans()
				CloseConn()
				Exit Try
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal cek role user: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try

		Try
			OpenConn()

			If CekButtonRole("User_Formula_Position_CLevel") = "Y" Then
				UserPosition = "CLEVEL"
				CloseTrans()
				CloseConn()
				Exit Try
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal cek role user: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try

		If String.IsNullOrWhiteSpace(UserPosition) Then
			MessageBox.Show("Terjadi Kesalahan, Posisi User Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
		End If

		Cmb_Filter_Status.SelectedIndex = 0

		Try
			OpenConn()

			If CekButtonRole("Tampil_HPP_Min_Max") = "T" Then
				DGV_Formula.Columns("HPP_Min").Visible = False
				DGV_Formula.Columns("HPP_Max").Visible = False
			Else
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal cek akses HPP: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		Load_Formula()
		Init_Komponen_Menu()
		Init_Tracking_Progress_Menu()
	End Sub

	Private Sub Init_Komponen_Menu()
		TLP_Komponen.Controls.Clear()

		Dim menus() As String = {
			"HPP Sementara",
			"Bahan Material",
			"Moisture Content",
			"Cooking Step",
			"Daftar Split"
		}

		Try
			OpenConn()

			If CekButtonRole("Tampil_Semua_Komponen_Formula") = "T" Then
				Dim menuList As New List(Of String)(menus)

				menuList.Remove("HPP Sementara")
				menuList.Remove("Bahan Material")
				menuList.Remove("Cooking Step")

				menus = menuList.ToArray()
			Else
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal cek akses HPP: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		Dim rowPercent As Single = If(menus.Length < 5, 20, 100 / menus.Length)

		TLP_Komponen.ColumnCount = 1
		TLP_Komponen.RowCount = 5
		TLP_Komponen.RowStyles.Clear()
		TLP_Komponen.ColumnStyles.Clear()
		TLP_Komponen.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

		For i As Integer = 0 To 4
			TLP_Komponen.RowStyles.Add(New RowStyle(SizeType.Percent, 20))
		Next

		For i As Integer = 0 To menus.Length - 1
			TLP_Komponen.RowStyles.Add(New RowStyle(SizeType.Percent, rowPercent))

			Dim lbl As New Label()

			With lbl
				.Text = menus(i)
				.Dock = DockStyle.Fill
				.TextAlign = ContentAlignment.MiddleLeft
				.Padding = New Padding(10, 0, 0, 0)
				.Cursor = Cursors.Hand
				.Margin = New Padding(1)
				.Font = New Font("Work Sans", 8, FontStyle.Regular)
				.BackColor = Color.White
				.ForeColor = Color.Black
			End With

			AddHandler lbl.Click, AddressOf Komponen_Menu_Click

			TLP_Komponen.Controls.Add(lbl, 0, i)

			If i = 0 Then
				Set_Active_Menu_Komponen(lbl)

				If lbl.Text = "HPP Sementara" Then
					Load_Komponen_HPP_Sementara("HPP Sementara")
				ElseIf lbl.Text = "Bahan Material" Then
					Load_Komponen_Bahan_Material()
				ElseIf lbl.Text = "Moisture Content" Then
					Load_Komponen_Moisture_Content()
				ElseIf lbl.Text = "Cooking Step" Then
					Load_Komponen_Cooking_Step()
				ElseIf lbl.Text = "Daftar Split" Then
					Load_Komponen_Daftar_Split()
				End If
			End If
		Next
	End Sub

	Private Sub Init_Tracking_Progress_Menu()
		TLP_TrackingProgress.Controls.Clear()

		Dim menus() As String = {
			"Look View",
			"Analisa Lab",
			"Palatabilitas"
		}

		TLP_TrackingProgress.ColumnCount = 1
		TLP_TrackingProgress.RowCount = 5
		TLP_TrackingProgress.RowStyles.Clear()
		TLP_TrackingProgress.ColumnStyles.Clear()
		TLP_TrackingProgress.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

		For i As Integer = 0 To 4
			TLP_TrackingProgress.RowStyles.Add(New RowStyle(SizeType.Percent, 20))
		Next

		For i As Integer = 0 To menus.Length - 1
			Dim lbl As New Label()

			With lbl
				.Text = menus(i)
				.Dock = DockStyle.Fill
				.TextAlign = ContentAlignment.MiddleLeft
				.Padding = New Padding(10, 0, 0, 0)
				.Cursor = Cursors.Hand
				.Margin = New Padding(1)
				.Font = New Font("Work Sans", 8, FontStyle.Regular)
				.BackColor = Color.White
				.ForeColor = Color.Black
			End With

			AddHandler lbl.Click, AddressOf Tracking_Progress_Menu_Click
			TLP_TrackingProgress.Controls.Add(lbl, 0, i)
		Next

		TLP_TrackingProgress.Enabled = False
	End Sub

	Private Sub Init_Filter()
	End Sub

	Private Sub Komponen_Menu_Click(sender As Object, e As EventArgs)
		If TB_NoFormula.Text.Trim = "" Then
			MessageBox.Show("Silahkan pilih formula terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			Exit Sub
		End If

		Dim lbl As Label = CType(sender, Label)

		If lbl.Text = "HPP Sementara" Then
			Load_Komponen_HPP_Sementara("HPP Sementara")
		ElseIf lbl.Text = "Bahan Material" Then
			Load_Komponen_Bahan_Material()
		ElseIf lbl.Text = "Moisture Content" Then
			Load_Komponen_Moisture_Content()
		ElseIf lbl.Text = "Cooking Step" Then
			Load_Komponen_Cooking_Step()
		ElseIf lbl.Text = "Daftar Split" Then
			Load_Komponen_Daftar_Split()
		End If

		If lbl.Text <> "Daftar Split" Then
			Reset_Active_Menu_Tracking_Progress()
			DGV_Detail_Pengujian.Rows.Clear()
		Else
		End If

		Set_Active_Menu_Komponen(lbl)
	End Sub

	Private Sub Set_Active_Menu_Komponen(menuLabel As Label)
		For Each ctrl As Control In TLP_Komponen.Controls
			If TypeOf ctrl Is Label Then
				Dim lbl As Label = CType(ctrl, Label)

				lbl.BackColor = Color.White
				lbl.ForeColor = Color.Black
				lbl.Font = New Font("Work Sans", 8, FontStyle.Regular)
			End If
		Next

		menuLabel.BackColor = Color.LightBlue
		menuLabel.ForeColor = Color.Black
		menuLabel.Font = New Font("Work Sans", 8, FontStyle.Bold)

		ActiveMenuKomponen = menuLabel
	End Sub

	Private Sub Reset_Active_Menu_Komponen()
		For Each ctrl As Control In TLP_Komponen.Controls
			If TypeOf ctrl Is Label Then
				Dim lbl As Label = CType(ctrl, Label)

				lbl.BackColor = Color.White
				lbl.ForeColor = Color.Black
				lbl.Font = New Font("Work Sans", 8, FontStyle.Regular)
			End If
		Next

		ActiveMenuKomponen = Nothing
	End Sub

	Private Sub Tracking_Progress_Menu_Click(sender As Object, e As EventArgs)
		Dim lbl As Label = CType(sender, Label)

		If lbl.Text = "Look View" Then
			Load_Tracking_Progress_Look_View()
		ElseIf lbl.Text = "Analisa Lab" Then
			Load_Tracking_Progress_Analisa_Lab()
		ElseIf lbl.Text = "Palatabilitas" Then
			Load_Tracking_Progress_Palatabilitas()
		End If

		Set_Active_Menu_Tracking_Progress(lbl)
	End Sub

	Private Sub Set_Active_Menu_Tracking_Progress(menuLabel As Label)
		For Each ctrl As Control In TLP_TrackingProgress.Controls
			If TypeOf ctrl Is Label Then
				Dim lbl As Label = CType(ctrl, Label)

				lbl.BackColor = Color.White
				lbl.ForeColor = Color.Black
				lbl.Font = New Font("Work Sans", 8, FontStyle.Regular)
			End If
		Next

		menuLabel.BackColor = Color.LightBlue
		menuLabel.ForeColor = Color.Black
		menuLabel.Font = New Font("Work Sans", 8, FontStyle.Bold)

		ActiveMenuTrackingProgress = menuLabel
	End Sub

	Private Sub Reset_Active_Menu_Tracking_Progress()
		For Each ctrl As Control In TLP_TrackingProgress.Controls
			If TypeOf ctrl Is Label Then
				Dim lbl As Label = CType(ctrl, Label)

				lbl.BackColor = Color.White
				lbl.ForeColor = Color.Black
				lbl.Font = New Font("Work Sans", 8, FontStyle.Regular)
			End If
		Next

		ActiveMenuTrackingProgress = Nothing
	End Sub

	Private Sub Load_Formula()
		Try
			OpenConn()

			Dim filterStatus As String = ""
			Dim filterParameter As String = ""
			Dim valueCari As String = Tb_Value.Text.Trim.Replace("'", "''")
			Dim filterTanggal As String = ""
			Dim filterTanggalParameter As String = ""

			If Cb_Transaksi_Hari_Ini.Checked Then
				filterTanggal = " AND CONVERT(DATE, a.Tanggal_Validasi) = CONVERT(DATE, GETDATE()) "
			End If

			Select Case Cmb_Filter_Status.Text.Trim()
				Case "Formula Butuh Tindakan"
					filterStatus = "
                        AND (
                            a.Flag_Selesai_Trial_Kitchen = 'Y'
                            OR a.Flag_Selesai_Trial_Produksi = 'Y'
                            OR (
                                a.Flag_Lanjut_Produksi = 'Y'
                                AND a.Flag_Validasi_Formula_Produksi_BOD IS NULL
                            )
                        )
                    "
				Case "Formula Sedang Diproses"
					filterStatus = "
                        AND (
                            a.Flag_Lanjut_Trial_Kitchen = 'Y'
                            OR a.Flag_Lanjut_Trial_Produksi = 'Y'
                            OR a.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                        )
                    "
				Case Else
					filterStatus = ""
			End Select

			If Cmb_Parameter_Lain.Text.Trim <> "" Then

				If valueCari = "" Then
					MessageBox.Show("Silahkan isi value pencarian", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Tb_Value.Focus()
					Exit Sub
				End If

				Select Case Cmb_Parameter_Lain.Text.Trim()

					Case "No. Formula"
						filterParameter = $" AND a.No_Faktur LIKE '%{valueCari}%' "

					Case "Kode Barang"
						filterParameter = $" AND b.Kode_Barang_Inq LIKE '%{valueCari}%' "

					Case "Nama Barang"
						filterParameter = $" AND b.Nama LIKE '%{valueCari}%' "

					Case "Jenis Formula"
						filterParameter = $" AND ISNULL(a.Kode_Hierarki, '') LIKE '%{valueCari}%' "

					Case "Posisi Binding"
						filterParameter = $"
                AND EXISTS (
                    SELECT 1
                    FROM N_EMI_Transaksi_Formulator_Binding x
                    JOIN N_EMI_Transaksi_Formulator_Binding_Detail y
                        ON y.Kode_Perusahaan = x.Kode_Perusahaan
                        AND y.No_Faktur = x.No_Faktur
                    WHERE y.No_Formulator = a.No_Faktur
                        AND x.Status IS NULL
                        AND x.Flag_Validasi_Main = 'Y'
                        AND CAST(y.No_Prioritas AS VARCHAR) LIKE '%{valueCari}%'
                )
            "

					Case "Status Formula"

						filterParameter = $"
                AND (
                    CASE
                        WHEN a.Flag_Lanjut_Produksi = 'Y'
                             AND a.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                        THEN 'Produksi'

                        WHEN a.Flag_Lanjut_Produksi = 'Y'
                             AND a.Flag_Validasi_Formula_Produksi_BOD IS NULL
                        THEN 'Menunggu Validasi BOD'

                        WHEN a.Flag_Selesai_Produksi = 'Y'
                        THEN 'Selesai Produksi Komersial'

                        WHEN a.Flag_Lanjut_Produksi = 'Y'
                        THEN 'Proses Produksi Komersial'

                        WHEN a.Flag_Selesai_Trial_Produksi = 'Y'
                        THEN 'Selesai Trial Produksi'

                        WHEN a.Flag_Lanjut_Trial_Produksi = 'Y'
                        THEN 'Proses Trial Produksi'

                        WHEN a.Flag_Selesai_Trial_Kitchen = 'Y'
                        THEN 'Selesai Trial Kitchen'

                        WHEN a.Flag_Lanjut_Trial_Kitchen = 'Y'
                        THEN 'Proses Trial Kitchen'
                        ELSE 'Belum Diproses'
                    END
                ) LIKE '%{valueCari}%'
            "

				End Select
			End If

			If Cb_Parameter_Tanggal.Checked Then

				If DTP_Start.Value.Date > DTP_End.Value.Date Then
					MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal akhir.",
						Judul,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning)

					DTP_Start.Focus()
					Exit Sub
				End If

				Select Case Cmb_Paramater_Tanggal.Text.Trim()

					Case "Tanggal Formula"
						filterTanggalParameter = $"
                        AND CAST(a.Tanggal_Validasi AS DATE)
                        BETWEEN '{Format(DTP_Start.Value, "yyyy-MM-dd")}'
                        AND '{Format(DTP_End.Value, "yyyy-MM-dd")}'
                    "

				End Select
			End If

			SQL = $"
                SELECT
                    a.No_Faktur,
                    FORMAT(a.Tanggal_Validasi, 'dd MMM yyyy') AS Tanggal_Validasi,
                    b.Kode_Barang_Inq,
                    b.Nama,
                    0 AS HPP_Min,
                    0 AS HPP_Max,
                    a.Hasil,
                    a.Satuan_Hasil,
                    a.Kode_Hierarki,
                    ISNULL(fb.No_Prioritas, NULL) AS Posisi_Binding,
                    CASE
                        WHEN a.Flag_Lanjut_Produksi = 'Y' AND a.Flag_Validasi_Formula_Produksi_BOD = 'Y' THEN 'Produksi'
                        WHEN a.Flag_Lanjut_Produksi = 'Y' AND a.Flag_Validasi_Formula_Produksi_BOD IS NULL THEN 'Menunggu Validasi BOD'
                        WHEN a.Flag_Selesai_Produksi = 'Y' THEN 'Selesai Produksi Komersial'
                        WHEN a.Flag_Lanjut_Produksi = 'Y' THEN 'Proses Produksi Komersial'
                        WHEN a.Flag_Selesai_Trial_Produksi = 'Y' THEN 'Selesai Trial Produksi'
                        WHEN a.Flag_Lanjut_Trial_Produksi = 'Y' THEN 'Proses Trial Produksi'
                        WHEN a.Flag_Selesai_Trial_Kitchen = 'Y' THEN 'Selesai Trial Kitchen'
                        WHEN a.Flag_Lanjut_Trial_Kitchen = 'Y' THEN 'Proses Trial Kitchen'
                        ELSE 'Belum Diproses'
                    END AS Status_Formula,
                    CASE
                        WHEN a.Flag_Lanjut_Trial_Kitchen = 'Y'
                             OR a.Flag_Selesai_Trial_Kitchen = 'Y'
                        THEN ISNULL(tk.Deskripsi, '-')
                        WHEN a.Flag_Lanjut_Trial_Produksi = 'Y'
                             OR a.Flag_Selesai_Trial_Produksi = 'Y'
                        THEN ISNULL(tp.Deskripsi, '-')
                        ELSE '-'
                    END AS Deskripsi,
                    'Validasi' AS Validasi
                FROM EMI_Transaksi_Formulator a
                JOIN Barang b ON b.Kode_Perusahaan = a.Kode_Perusahaan AND b.Kode_Barang_Inq = a.Kode_Barang AND b.Kode_Stock_Owner = a.Kode_Stock_Owner
                OUTER APPLY (
                    SELECT TOP 1 y.Deskripsi
                    FROM N_EMI_Transaksi_Trial_Order_Produksi x
                    JOIN N_EMI_Transaksi_Trial_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                    WHERE x.Kode_Formula = a.No_Faktur
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) tk
                OUTER APPLY (
                    SELECT TOP 1 y.Deskripsi
                    FROM EMI_Order_Produksi x
                    JOIN Emi_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                    WHERE x.Kode_Formula = a.No_Faktur
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) tp
                OUTER APPLY (
                    SELECT TOP 1 y.No_Prioritas
                    FROM N_EMI_Transaksi_Formulator_Binding x
                    JOIN N_EMI_Transaksi_Formulator_Binding_Detail y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_Faktur = x.No_Faktur
                    WHERE y.No_Formulator = a.No_Faktur AND x.Status IS NULL AND x.Flag_Validasi_Main = 'Y'
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) fb
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.Flag_Validasi = 'Y'
                    AND a.Status IS NULL
                {filterStatus}
                {filterParameter}
                {filterTanggal}
                {filterTanggalParameter}
                ORDER BY a.Tanggal_Validasi DESC
            "

			DGV_Formula.Rows.Clear()

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							DGV_Formula.Rows.Add()

							Dim statusFormula As String = .Rows(i).Item("Status_Formula").ToString()
							Dim posisiBinding As String = If(IsDBNull(.Rows(i).Item("Posisi_Binding")), "", .Rows(i).Item("Posisi_Binding").ToString())

							If posisiBinding = "1" Then
								posisiBinding = "FORMULA UTAMA"
							ElseIf IsNumeric(posisiBinding) AndAlso CInt(posisiBinding) > 1 Then
								posisiBinding = $"CADANGAN {CInt(posisiBinding) - 1}"
							Else
								posisiBinding = "-"
							End If

							DGV_Formula.Rows(i).Cells(CellParent_NoFormula).Value = .Rows(i).Item("No_Faktur")
							DGV_Formula.Rows(i).Cells(CellParent_TanggalFormula).Value = .Rows(i).Item("Tanggal_Validasi")
							DGV_Formula.Rows(i).Cells(CellParent_KdBarang).Value = .Rows(i).Item("Kode_Barang_Inq")
							DGV_Formula.Rows(i).Cells(CellParent_NmBarang).Value = .Rows(i).Item("Nama")
							DGV_Formula.Rows(i).Cells(CellParent_HPPMin).Value = Format(.Rows(i).Item("HPP_Min"), "N2")
							DGV_Formula.Rows(i).Cells(CellParent_HPPMax).Value = Format(.Rows(i).Item("HPP_Max"), "N2")
							DGV_Formula.Rows(i).Cells(CellParent_Jumlah).Value = Format(.Rows(i).Item("Hasil"), "N3")
							DGV_Formula.Rows(i).Cells(CellParent_Satuan).Value = .Rows(i).Item("Satuan_Hasil")
							DGV_Formula.Rows(i).Cells(CellParent_JenisFormula).Value = If(General_Class.CekNULL(.Rows(i).Item("Kode_Hierarki")) = "", "-", .Rows(i).Item("Kode_Hierarki"))

							DGV_Formula.Rows(i).Cells(CellParent_PosisiBinding).Value = posisiBinding

							DGV_Formula.Rows(i).Cells(CellParent_StatusFormula).Value = statusFormula
							DGV_Formula.Rows(i).Cells(CellParent_Deskripsi).Value = .Rows(i).Item("Deskripsi")
							DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = .Rows(i).Item("Validasi")

							Select Case statusFormula
								Case "Produksi", "Menunggu Validasi BOD", "Selesai Produksi Komersial", "Proses Produksi Komersial"
									DGV_Formula.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
								Case "Selesai Trial Produksi", "Proses Trial Produksi"
									DGV_Formula.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
								Case "Selesai Trial Kitchen", "Proses Trial Kitchen"
									DGV_Formula.Rows(i).DefaultCellStyle.BackColor = Color.LightYellow
							End Select

							If UserPosition.Trim = "STAFF" Then
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi) = New DataGridViewTextBoxCell()
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.BackColor = Color.White
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.ForeColor = Color.Black
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = ""
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).ReadOnly = True
							Else
								Dim btnCell As New DataGridViewButtonCell()
								btnCell.FlatStyle = FlatStyle.Flat

								Dim currentStatus As String = .Rows(i).Item("Status_Formula").ToString.Trim

								Dim isButton As Boolean = False
								Dim userPos As String = UserPosition.Trim

								If userPos = "HEADDEPT" Then
									isButton = Status_HeadDept.Contains(currentStatus)
								ElseIf userPos = "CLEVEL" Then
									isButton = Status_BOD.Contains(currentStatus)
								End If

								If isButton Then
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi) = btnCell
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = .Rows(i).Item("Validasi")

									With DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style
										.BackColor = Color.FromArgb(15, 86, 122)
										.ForeColor = Color.White
									End With
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).ReadOnly = False
								Else
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi) = New DataGridViewTextBoxCell()

									'With DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style
									'    .BackColor = Color.White
									'    .ForeColor = Color.Black
									'End With

									Select Case statusFormula
										Case "Produksi", "Menunggu Validasi BOD", "Selesai Produksi Komersial", "Proses Produksi Komersial"
											DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.BackColor = Color.LightGreen
										Case "Selesai Trial Produksi", "Proses Trial Produksi"
											DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.BackColor = Color.LightBlue
										Case "Selesai Trial Kitchen", "Proses Trial Kitchen"
											DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.BackColor = Color.LightYellow
									End Select

									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = ""
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).ReadOnly = True
								End If

							End If
						Next
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal loading formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Private Sub Load_Komponen_HPP_Sementara(type As String)
		DGV_Komponen.Visible = True
		RTB_Komponen.Visible = False

		With DGV_Komponen
			.AutoGenerateColumns = False
			.Columns.Clear()

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "HS_Komponen",
				.HeaderText = "Komponen",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				 .Name = "HS_Nilai",
				 .HeaderText = "Nilai",
				 .Width = 200,
				 .[ReadOnly] = True,
				 .SortMode = DataGridViewColumnSortMode.NotSortable,
				 .DefaultCellStyle = New DataGridViewCellStyle With {
					 .Alignment = DataGridViewContentAlignment.MiddleRight,
					 .Format = "N2"
				 }
			 })

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "HS_Satuan",
				.HeaderText = "Satuan",
				.[ReadOnly] = True,
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})
		End With

		If TB_SplitType.Text.Trim = "TRIAL_KITCHEN" Then
			Try
				OpenConn()

				SQL = $"
                WITH cte_bahan AS (
                    SELECT
                        a.Kode_Barang,
                        a.Kode_Bahan,
                        a.Jumlah_Barang,
                        ISNULL((
                            SELECT TOP (1) dbo.get_hpp(x.serial_number)
                            FROM barang_sn x
                            WHERE x.kode_barang = a.kode_bahan
                              AND dbo.get_hpp(x.serial_number) <> 0
                            ORDER BY x.Tgl_masuk DESC
                        ), 0) / a.Jumlah_Barang AS hpp
                    FROM Barang_Detail_Bahan_Penolong a
                ),

                cte_wc AS (
                    SELECT
                        a.Kode_Perusahaan,
                        a.Kode_Jenis_Biaya_Produksi,
                        ISNULL((
                            SELECT TOP (1) x.no_faktur
                            FROM Emi_Transaksi_Work_Center x
                            WHERE x.status IS NULL
                              AND x.Kode_Perusahaan = a.Kode_Perusahaan
                              AND x.jenis_biaya = a.Kode_Jenis_Biaya_Produksi
                            ORDER BY x.id DESC
                        ), NULL) AS Faktur_WC
                    FROM Emi_Jenis_Biaya_Produksi a
                ),

                cte_produksi AS (
                    SELECT
                        c.Id_Routing,
                        c.id_work_center,
                        MAX(c.Nilai_Per_pcs) AS Nilai_Per_Kg
                    FROM cte_wc a
                    JOIN Emi_Transaksi_Work_Center b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                       AND a.Faktur_WC = b.No_Faktur
                    JOIN Emi_Transaksi_Work_Center_detail c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                       AND b.No_Faktur = c.No_Faktur
                    GROUP BY c.Id_Routing, c.id_work_center
                )

                SELECT
                    a.No_Faktur,
                    a.Kode_Formula,
                    b.Kode_Barang,
                    d.Nama AS Nama_Produk,
                    d.Satuan,

                    ISNULL((
                        SELECT SUM(hpp)
                        FROM cte_bahan x
                        WHERE x.Kode_Barang = b.Kode_Barang
                    ), 0) AS HPP_Packaging,

                    ISNULL((
                        SELECT SUM(Nilai_Per_Kg) / 1000 * d.Berat
                        FROM cte_produksi x
                        WHERE d.Id_Routing = x.Id_Routing
                    ), 0) AS HPP_Produksi,

                    SUM(c.Est_HPP_Per_Pcs) AS HPP_Bahan_Baku

                FROM N_EMI_Transaksi_Trial_Order_Produksi a
                JOIN Emi_Transaksi_Formulator b
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan
                   AND a.Kode_Formula = b.No_Faktur

                JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                    ON b.Kode_Perusahaan = c.Kode_Perusahaan
                   AND b.No_Faktur = c.No_Faktur

                JOIN Barang d
                    ON b.Kode_Perusahaan = d.Kode_Perusahaan
                   AND b.Kode_Barang = d.Kode_Barang_inq

                WHERE a.Kode_Formula = '{TB_NoFormula.Text.Trim}'

                GROUP BY
                    a.No_Faktur,
                    a.Kode_Formula,
                    b.Kode_Barang,
                    d.Nama,
                    d.Satuan,
                    d.Berat,
                    d.Id_Routing;
                "

				Using Dr = OpenTrans(SQL)
					If Dr.Read() Then

						Dim hppBahanBaku As Decimal = If(IsDBNull(Dr("HPP_Bahan_Baku")), 0, CDec(Dr("HPP_Bahan_Baku")))
						Dim hppPackaging As Decimal = If(IsDBNull(Dr("HPP_Packaging")), 0, CDec(Dr("HPP_Packaging")))
						Dim hppProduksi As Decimal = If(IsDBNull(Dr("HPP_Produksi")), 0, CDec(Dr("HPP_Produksi")))
						Dim satuan As String = If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString())

						Dim totalHPP As Decimal = hppBahanBaku + hppPackaging + hppProduksi

						DGV_Komponen.Rows.Clear()

						DGV_Komponen.Rows.Add(
							"HPP Bahan Baku",
							"Rp " & Format(hppBahanBaku, "N2") & ",-",
							satuan
						)

						DGV_Komponen.Rows.Add(
							"HPP Packaging",
							"Rp " & Format(hppPackaging, "N2") & ",-",
							satuan
						)

						DGV_Komponen.Rows.Add(
							"HPP Produksi",
							"Rp " & Format(hppProduksi, "N2") & ",-",
							satuan
						)

						DGV_Komponen.Rows.Add(
							"Total HPP Sementara",
							"Rp " & Format(totalHPP, "N2") & ",-",
							satuan
						)

						DGV_Komponen.Rows(DGV_Komponen.Rows.Count - 1).DefaultCellStyle.Font = New Font(DGV_Komponen.Font, FontStyle.Bold)
					Else
						DGV_Komponen.Rows.Clear()

						DGV_Komponen.Rows.Add(
							"HPP Bahan Baku",
							"-",
							"-"
						)

						DGV_Komponen.Rows.Add(
							"HPP Packaging",
							"-",
							"-"
						)

						DGV_Komponen.Rows.Add(
							"HPP Produksi",
							"-",
							"-"
						)

						DGV_Komponen.Rows.Add(
							"Total HPP Sementara",
							"-",
							"-"
						)

						DGV_Komponen.Rows(DGV_Komponen.Rows.Count - 1).DefaultCellStyle.Font = New Font(DGV_Komponen.Font, FontStyle.Bold)
					End If
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load hpp sementara: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		ElseIf TB_SplitType.Text.Trim = "TRIAL_PRODUKSI" Then
			Try
				OpenConn()

				SQL = $"
                    WITH cte_bahan AS (
                        SELECT
                            a.Kode_Barang,
                            a.Kode_Bahan,
                            a.Jumlah_Barang,
                            ISNULL((
                                SELECT TOP (1) dbo.get_hpp(x.serial_number)
                                FROM barang_sn x
                                WHERE x.kode_barang = a.kode_bahan
                                  AND dbo.get_hpp(x.serial_number) <> 0
                                ORDER BY x.Tgl_masuk DESC
                            ), 0) / a.Jumlah_Barang AS hpp
                        FROM Barang_Detail_Bahan_Penolong a
                    ),

                    cte_wc AS (
                        SELECT
                            a.Kode_Perusahaan,
                            a.Kode_Jenis_Biaya_Produksi,
                            ISNULL((
                                SELECT TOP (1) x.no_faktur
                                FROM Emi_Transaksi_Work_Center x
                                WHERE x.status IS NULL
                                  AND x.Kode_Perusahaan = a.Kode_Perusahaan
                                  AND x.jenis_biaya = a.Kode_Jenis_Biaya_Produksi
                                ORDER BY x.id DESC
                            ), NULL) AS Faktur_WC
                        FROM Emi_Jenis_Biaya_Produksi a
                    ),

                    cte_produksi AS (
                        SELECT
                            c.Id_Routing,
                            c.id_work_center,
                            MAX(c.Nilai_Per_pcs) AS Nilai_Per_Kg
                        FROM cte_wc a
                        JOIN Emi_Transaksi_Work_Center b
                            ON a.Kode_Perusahaan = b.Kode_Perusahaan
                           AND a.Faktur_WC = b.No_Faktur
                        JOIN Emi_Transaksi_Work_Center_detail c
                            ON b.Kode_Perusahaan = c.Kode_Perusahaan
                           AND b.No_Faktur = c.No_Faktur
                        GROUP BY c.Id_Routing, c.id_work_center
                    )

                    SELECT
                        a.No_Faktur,
                        a.Kode_Formula,
                        b.Kode_Barang,
                        d.Nama AS Nama_Produk,
                        d.Satuan,

                        ISNULL((
                            SELECT SUM(hpp)
                            FROM cte_bahan x
                            WHERE x.Kode_Barang = b.Kode_Barang
                        ), 0) AS HPP_Packaging,

                        ISNULL((
                            SELECT SUM(Nilai_Per_Kg) / 1000 * d.Berat
                            FROM cte_produksi x
                            WHERE d.Id_Routing = x.Id_Routing
                        ), 0) AS HPP_Produksi,

                        SUM(c.Est_HPP_Per_Pcs) AS HPP_Bahan_Baku

                    FROM EMI_Order_Produksi a
                    JOIN Emi_Transaksi_Formulator b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                       AND a.Kode_Formula = b.No_Faktur AND a.Flag_Trial_Produksi = 'Y'

                    JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                       AND b.No_Faktur = c.No_Faktur

                    JOIN Barang d
                        ON b.Kode_Perusahaan = d.Kode_Perusahaan
                       AND b.Kode_Barang = d.Kode_Barang_inq

                    WHERE a.Kode_Formula = '{TB_NoFormula.Text.Trim}'

                    GROUP BY
                        a.No_Faktur,
                        a.Kode_Formula,
                        b.Kode_Barang,
                        d.Nama,
                        d.Satuan,
                        d.Berat,
                        d.Id_Routing;
                "

				Using Dr = OpenTrans(SQL)
					If Dr.Read() Then

						Dim hppBahanBaku As Decimal = If(IsDBNull(Dr("HPP_Bahan_Baku")), 0, CDec(Dr("HPP_Bahan_Baku")))
						Dim hppPackaging As Decimal = If(IsDBNull(Dr("HPP_Packaging")), 0, CDec(Dr("HPP_Packaging")))
						Dim hppProduksi As Decimal = If(IsDBNull(Dr("HPP_Produksi")), 0, CDec(Dr("HPP_Produksi")))
						Dim satuan As String = If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString())

						Dim totalHPP As Decimal = hppBahanBaku + hppPackaging + hppProduksi

						DGV_Komponen.Rows.Clear()

						DGV_Komponen.Rows.Add(
							"HPP Bahan Baku",
							"Rp " & Format(hppBahanBaku, "N2") & ",-",
							satuan
						)

						DGV_Komponen.Rows.Add(
							"HPP Packaging",
							"Rp " & Format(hppPackaging, "N2") & ",-",
							satuan
						)

						DGV_Komponen.Rows.Add(
							"HPP Produksi",
							"Rp " & Format(hppProduksi, "N2") & ",-",
							satuan
						)

						DGV_Komponen.Rows.Add(
							"Total HPP Sementara",
							"Rp " & Format(totalHPP, "N2") & ",-",
							satuan
						)

						DGV_Komponen.Rows(DGV_Komponen.Rows.Count - 1).DefaultCellStyle.Font = New Font(DGV_Komponen.Font, FontStyle.Bold)
					Else
						DGV_Komponen.Rows.Clear()

						DGV_Komponen.Rows.Add(
							"HPP Bahan Baku",
							"-",
							"-"
						)

						DGV_Komponen.Rows.Add(
							"HPP Packaging",
							"-",
							"-"
						)

						DGV_Komponen.Rows.Add(
							"HPP Produksi",
							"-",
							"-"
						)

						DGV_Komponen.Rows.Add(
							"Total HPP Sementara",
							"-",
							"-"
						)

						DGV_Komponen.Rows(DGV_Komponen.Rows.Count - 1).DefaultCellStyle.Font = New Font(DGV_Komponen.Font, FontStyle.Bold)
					End If
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load hpp sementara: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		End If
	End Sub

	Private Sub Load_Komponen_Bahan_Material()
		RTB_Komponen.Visible = False
		DGV_Komponen.Visible = True

		Reset_Active_Menu_Tracking_Progress()
		DGV_Detail_Pengujian.Rows.Clear()

		With DGV_Komponen
			.AutoGenerateColumns = False
			.Columns.Clear()

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "BM_Nomor",
				.HeaderText = "No",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "BM_Kode_Barang",
				.HeaderText = "Kode Barang",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "BM_Nama_Barang",
				.HeaderText = "Deskripsi",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				 .Name = "BM_Jumlah",
				 .HeaderText = "Jumlah (Kg)",
				 .Width = 100,
				 .[ReadOnly] = True,
				 .SortMode = DataGridViewColumnSortMode.NotSortable,
				 .DefaultCellStyle = New DataGridViewCellStyle With {
					 .Alignment = DataGridViewContentAlignment.MiddleRight,
					 .Format = "N2"
				 }
			 })

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "BM_Persentase",
				.HeaderText = "Persentase (%)",
				.Width = 100,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable,
				.DefaultCellStyle = New DataGridViewCellStyle With {
					 .Alignment = DataGridViewContentAlignment.MiddleRight,
					 .Format = "N2"
				}
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "BM_Harga",
				.HeaderText = "Harga",
				.Width = 100,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable,
				.DefaultCellStyle = New DataGridViewCellStyle With {
					 .Alignment = DataGridViewContentAlignment.MiddleRight,
					 .Format = "N2"
				}
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "BM_Est_HPP_Pcs",
				.HeaderText = "Est. HPP Pcs",
				.Width = 100,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable,
				.DefaultCellStyle = New DataGridViewCellStyle With {
					 .Alignment = DataGridViewContentAlignment.MiddleRight,
					 .Format = "N2"
				}
			})
		End With

		If TB_SplitType.Text = "TRIAL_KITCHEN" Then
			Try
				OpenConn()

				SQL = $"
                    SELECT
                        Kode_Bahan,
                        Nama_Bahan,
                        Jumlah,
                        Persentase,
                        Est_HPP,
                        Est_HPP_Per_Pcs
                    FROM N_EMI_View_Laporan_Formula_Rpt
                    WHERE No_Transaksi = '{TB_NoSplitFormula.Text.Trim}'
                    GROUP BY
                        Kode_Bahan,
                        Nama_Bahan,
                        Jumlah,
                        Persentase,
                        Est_HPP,
                        Est_HPP_Per_Pcs
                "

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								Dim jumlah As Decimal = Val(.Rows(i).Item("Jumlah"))
								Dim persentase As Decimal = Val(.Rows(i).Item("Persentase"))
								Dim esthpp As Decimal = Val(.Rows(i).Item("Est_HPP"))
								Dim esthpppcs As Decimal = Val(.Rows(i).Item("Est_HPP_Per_Pcs"))

								DGV_Komponen.Rows.Add()
								DGV_Komponen.Rows(i).Cells(0).Value = i + 1
								DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Bahan")
								DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Nama_Bahan")
								DGV_Komponen.Rows(i).Cells(3).Value = $"{jumlah:N2} Kg"
								DGV_Komponen.Rows(i).Cells(4).Value = $"{persentase:N2} %"
								DGV_Komponen.Rows(i).Cells(5).Value = $"{esthpp:N2}"
								DGV_Komponen.Rows(i).Cells(6).Value = $"{esthpppcs:N2}"
							Next
						End If
					End With
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load bahan material: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		ElseIf TB_SplitType.Text = "TRIAL_PRODUKSI" Then
			Try
				OpenConn()

				SQL = $"
                    WITH cte AS (
                        SELECT
                            a.Kode_Barang,
                            a.Kode_Bahan,
                            a.Jumlah_Barang,

                            ISNULL((
                                SELECT TOP (1)
                                    dbo.get_hpp(x.Serial_Number)
                                FROM Barang_SN x
                                WHERE
                                    x.Kode_Barang = a.Kode_Bahan
                                    AND dbo.get_hpp(x.Serial_Number) <> 0
                                ORDER BY x.Tgl_Masuk DESC
                            ), 0) / a.Jumlah_Barang AS hpp

                        FROM Barang_Detail_Bahan_Penolong a
                    ),

                    cte_b AS (
                        SELECT
                            a.Kode_Perusahaan,
                            a.Id_Jenis_Biaya_Produksi,
                            a.Kode_Jenis_Biaya_Produksi,

                            ISNULL((
                                SELECT TOP (1)
                                    No_Faktur
                                FROM Emi_Transaksi_Work_Center x
                                WHERE
                                    x.Status IS NULL
                                    AND x.Kode_Perusahaan = a.Kode_Perusahaan
                                    AND x.Jenis_Biaya = a.Kode_Jenis_Biaya_Produksi
                                ORDER BY x.Id DESC
                            ), NULL) AS Faktur_WC

                        FROM Emi_Jenis_Biaya_Produksi a
                    ),

                    cte_c AS (
                        SELECT
                            c.Id_Routing,
                            a.Kode_Jenis_Biaya_Produksi,
                            c.Id_Work_Center,
                            MAX(c.Nilai_Per_Pcs) AS Nilai_Per_Kg

                        FROM cte_b a
                        INNER JOIN Emi_Transaksi_Work_Center b
                            ON a.Kode_Perusahaan = b.Kode_Perusahaan
                            AND a.Faktur_WC = b.No_Faktur

                        INNER JOIN Emi_Transaksi_Work_Center_Detail c
                            ON b.Kode_Perusahaan = c.Kode_Perusahaan
                            AND b.No_Faktur = c.No_Faktur

                        GROUP BY
                            c.Id_Routing,
                            a.Kode_Jenis_Biaya_Produksi,
                            c.Id_Work_Center
                    )

                    SELECT
                        a.Kode_Perusahaan,
                        ab.No_Transaksi,
                        a.Kode_Formula,

                        b.No_Faktur,
                        b.Tanggal,
                        b.Jam,
                        b.Kode_Barang,

                        d.Nama AS Nama_Produk,

                        c.Kode_Barang AS Kode_Bahan,
                        e.Nama AS Nama_Bahan,

                        c.Jumlah,

                        ISNULL(c.Est_HPP, 0) AS Est_HPP,
                        ISNULL(c.Est_HPP_Per_Pcs, 0) AS Est_HPP_Per_Pcs,

                        c.Persentase,
                        d.Satuan,

                        ISNULL((
                            SELECT SUM(x.hpp)
                            FROM cte x
                            WHERE b.Kode_Barang = x.Kode_Barang
                        ), 0) AS HPP_Packaging,

                        ISNULL((
                            SELECT
                                SUM(x.Nilai_Per_Kg) / 1000 * d.Berat
                            FROM cte_c x
                            WHERE d.Id_Routing = x.Id_Routing
                        ), 0) AS HPP_Produksi

                    FROM EMI_Order_Produksi a

                    INNER JOIN Emi_Split_Production_Order ab
                        ON a.Kode_Perusahaan = ab.Kode_Perusahaan
                        AND a.No_Faktur = ab.No_PO

                    INNER JOIN Emi_Transaksi_Formulator b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                        AND a.Kode_Formula = b.No_Faktur

                    INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                        AND b.No_Faktur = c.No_Faktur

                    INNER JOIN Barang d
                        ON b.Kode_Perusahaan = d.Kode_Perusahaan
                        AND b.Kode_Stock_Owner = d.Kode_Stock_Owner
                        AND b.Kode_Barang = d.Kode_Barang_Inq

                    INNER JOIN Barang e
                        ON c.Kode_Perusahaan = e.Kode_Perusahaan
                        AND c.Kode_Stock_Owner = e.Kode_Stock_Owner
                        AND c.Kode_Barang = e.Kode_Barang

                    WHERE
                        a.Status IS NULL
                        AND a.Flag_Trial_Produksi = 'Y'
                        AND ab.Status IS NULL
                        AND a.Kode_Perusahaan = '{KodePerusahaan}'
                        AND ab.No_Transaksi = '{TB_NoSplitFormula.Text.Trim}'
                        AND a.Kode_Formula = '{TB_NoFormula.Text.Trim}'
                "

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								Dim jumlah As Decimal = Val(.Rows(i).Item("Jumlah"))
								Dim persentase As Decimal = Val(.Rows(i).Item("Persentase"))
								Dim esthpp As Decimal = Val(.Rows(i).Item("Est_HPP"))
								Dim esthpppcs As Decimal = Val(.Rows(i).Item("Est_HPP_Per_Pcs"))

								DGV_Komponen.Rows.Add()
								DGV_Komponen.Rows(i).Cells(0).Value = i + 1
								DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Bahan")
								DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Nama_Bahan")
								DGV_Komponen.Rows(i).Cells(3).Value = $"{jumlah:N2} Kg"
								DGV_Komponen.Rows(i).Cells(4).Value = $"{persentase:N2} %"
								DGV_Komponen.Rows(i).Cells(5).Value = $"{esthpp:N2}"
								DGV_Komponen.Rows(i).Cells(6).Value = $"{esthpppcs:N2}"
							Next
						End If
					End With
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load bahan material: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		Else
			Try
				OpenConn()

				SQL = $"
                    WITH cte AS (
                        SELECT
                            a.Kode_Barang,
                            a.Kode_Bahan,
                            a.Jumlah_Barang,

                            ISNULL((
                                SELECT TOP (1)
                                    dbo.get_hpp(x.Serial_Number)
                                FROM Barang_SN x
                                WHERE
                                    x.Kode_Barang = a.Kode_Bahan
                                    AND dbo.get_hpp(x.Serial_Number) <> 0
                                ORDER BY x.Tgl_Masuk DESC
                            ), 0) / a.Jumlah_Barang AS hpp

                        FROM Barang_Detail_Bahan_Penolong a
                    ),

                    cte_b AS (
                        SELECT
                            a.Kode_Perusahaan,
                            a.Id_Jenis_Biaya_Produksi,
                            a.Kode_Jenis_Biaya_Produksi,

                            ISNULL((
                                SELECT TOP (1)
                                    No_Faktur
                                FROM Emi_Transaksi_Work_Center x
                                WHERE
                                    x.Status IS NULL
                                    AND x.Kode_Perusahaan = a.Kode_Perusahaan
                                    AND x.Jenis_Biaya = a.Kode_Jenis_Biaya_Produksi
                                ORDER BY x.Id DESC
                            ), NULL) AS Faktur_WC

                        FROM Emi_Jenis_Biaya_Produksi a
                    ),

                    cte_c AS (
                        SELECT
                            c.Id_Routing,
                            a.Kode_Jenis_Biaya_Produksi,
                            c.Id_Work_Center,
                            MAX(c.Nilai_Per_Pcs) AS Nilai_Per_Kg

                        FROM cte_b a
                        INNER JOIN Emi_Transaksi_Work_Center b
                            ON a.Kode_Perusahaan = b.Kode_Perusahaan
                            AND a.Faktur_WC = b.No_Faktur

                        INNER JOIN Emi_Transaksi_Work_Center_Detail c
                            ON b.Kode_Perusahaan = c.Kode_Perusahaan
                            AND b.No_Faktur = c.No_Faktur

                        GROUP BY
                            c.Id_Routing,
                            a.Kode_Jenis_Biaya_Produksi,
                            c.Id_Work_Center
                    )

                    SELECT
                        b.No_Faktur,
                        b.Tanggal,
                        b.Jam,
                        b.Kode_Barang,

                        d.Nama AS Nama_Produk,

                        c.Kode_Barang AS Kode_Bahan,
                        e.Nama AS Nama_Bahan,

                        c.Jumlah,

                        ISNULL(c.Est_HPP, 0) AS Est_HPP,
                        ISNULL(c.Est_HPP_Per_Pcs, 0) AS Est_HPP_Per_Pcs,

                        c.Persentase,
                        d.Satuan,

                        ISNULL((
                            SELECT SUM(x.hpp)
                            FROM cte x
                            WHERE b.Kode_Barang = x.Kode_Barang
                        ), 0) AS HPP_Packaging,

                        ISNULL((
                            SELECT
                                SUM(x.Nilai_Per_Kg) / 1000 * d.Berat
                            FROM cte_c x
                            WHERE d.Id_Routing = x.Id_Routing
                        ), 0) AS HPP_Produksi

                    FROM Emi_Transaksi_Formulator b

                    INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                        AND b.No_Faktur = c.No_Faktur

                    INNER JOIN Barang d
                        ON b.Kode_Perusahaan = d.Kode_Perusahaan
                        AND b.Kode_Stock_Owner = d.Kode_Stock_Owner
                        AND b.Kode_Barang = d.Kode_Barang_Inq

                    INNER JOIN Barang e
                        ON c.Kode_Perusahaan = e.Kode_Perusahaan
                        AND c.Kode_Stock_Owner = e.Kode_Stock_Owner
                        AND c.Kode_Barang = e.Kode_Barang

                    WHERE
                        b.Status IS NULL
                        AND b.Kode_Perusahaan = '{KodePerusahaan}'
                        AND b.No_Faktur = '{TB_NoFormula.Text.Trim}'
                "

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								Dim jumlah As Decimal = Val(.Rows(i).Item("Jumlah"))
								Dim persentase As Decimal = Val(.Rows(i).Item("Persentase"))
								Dim esthpp As Decimal = Val(.Rows(i).Item("Est_HPP"))
								Dim esthpppcs As Decimal = Val(.Rows(i).Item("Est_HPP_Per_Pcs"))

								DGV_Komponen.Rows.Add()
								DGV_Komponen.Rows(i).Cells(0).Value = i + 1
								DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Bahan")
								DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Nama_Bahan")
								DGV_Komponen.Rows(i).Cells(3).Value = $"{jumlah:N2} Kg"
								DGV_Komponen.Rows(i).Cells(4).Value = $"{persentase:N2} %"
								DGV_Komponen.Rows(i).Cells(5).Value = $"{esthpp:N2}"
								DGV_Komponen.Rows(i).Cells(6).Value = $"{esthpppcs:N2}"
							Next
						End If
					End With
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load bahan material: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		End If
	End Sub

	Private Sub Load_Komponen_Moisture_Content()
		RTB_Komponen.Visible = False
		DGV_Komponen.Visible = True

		Reset_Active_Menu_Tracking_Progress()
		DGV_Detail_Pengujian.Rows.Clear()

		With DGV_Komponen
			.AutoGenerateColumns = False
			.Columns.Clear()

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "MC_Kode_Analisa",
				.HeaderText = "Kode Analisa",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				 .Name = "MC_Jenis_Analisa",
				 .HeaderText = "Jenis Analisa",
				 .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
				 .[ReadOnly] = True,
				 .SortMode = DataGridViewColumnSortMode.NotSortable
			 })

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "MC_Kategori",
				.HeaderText = "Kategori",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "MC_Kriteria",
				.HeaderText = "Kriteria",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "MC_Range_Awal",
				.HeaderText = "Range Awal",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "MC_Range_Akhir",
				.HeaderText = "Range Akhir",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})
		End With

		If TB_SplitType.Text = "TRIAL_KITCHEN" Then
			Try
				OpenConn()

				SQL = $"
	                SELECT
		                b.Kode_Analisa,
		                b.Jenis_Analisa,
		                CASE
			                WHEN ISNULL(b.Flag_Perhitungan, 'T') = 'Y'
				                THEN 'Perhitungan'
			                ELSE 'Non Perhitungan'
		                END AS Kategori,
		                '-' AS Kriteria,
		                a.Range_Awal,
		                a.Range_Akhir
	                FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
	                INNER JOIN N_EMI_LAB_Jenis_Analisa b
		                ON a.Id_Jenis_Analisa = b.id
	                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
	                AND a.No_Formula = '{TB_NoFormula.Text.Trim}'

	                UNION ALL

	                SELECT
		                b.Kode_Analisa,
		                b.Jenis_Analisa,
		                'Non Perhitungan' AS Kategori,
		                c.Label_Keterangan AS Kriteria,
		                '' AS Range_Awal,
		                '' AS Range_Akhir
	                FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
	                INNER JOIN N_EMI_LAB_Jenis_Analisa b
		                ON a.Id_Jenis_Analisa = b.id
	                INNER JOIN EMI_Switch c
		                ON a.Kode_Perusahaan = c.kode_perusahaan
		                AND a.nilai_kriteria = c.keterangan
	                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
	                AND a.No_Formula = '{TB_NoFormula.Text.Trim}'

	                ORDER BY Kode_Analisa, Jenis_Analisa
                "

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								DGV_Komponen.Rows.Add()
								DGV_Komponen.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Analisa")
								DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Jenis_Analisa")
								DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Kategori")
								DGV_Komponen.Rows(i).Cells(3).Value = .Rows(i).Item("Kriteria")
								DGV_Komponen.Rows(i).Cells(4).Value = .Rows(i).Item("Range_Awal")
								DGV_Komponen.Rows(i).Cells(5).Value = .Rows(i).Item("Range_Akhir")
							Next
						End If
					End With
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load moisture content: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		ElseIf TB_SplitType.Text = "TRIAL_PRODUKSI" Then
			Try
				OpenConn()

				SQL = $"
	                SELECT
		                b.Kode_Analisa,
		                b.Jenis_Analisa,
		                CASE
			                WHEN ISNULL(b.Flag_Perhitungan, 'T') = 'Y'
				                THEN 'Perhitungan'
			                ELSE 'Non Perhitungan'
		                END AS Kategori,
		                '-' AS Kriteria,
		                a.Range_Awal,
		                a.Range_Akhir
	                FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
	                INNER JOIN N_EMI_LAB_Jenis_Analisa b
		                ON a.Id_Jenis_Analisa = b.id
	                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
	                AND a.No_Formula = '{TB_NoFormula.Text.Trim}'

	                UNION ALL

	                SELECT
		                b.Kode_Analisa,
		                b.Jenis_Analisa,
		                'Non Perhitungan' AS Kategori,
		                c.Label_Keterangan AS Kriteria,
		                '' AS Range_Awal,
		                '' AS Range_Akhir
	                FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
	                INNER JOIN N_EMI_LAB_Jenis_Analisa b
		                ON a.Id_Jenis_Analisa = b.id
	                INNER JOIN EMI_Switch c
		                ON a.Kode_Perusahaan = c.kode_perusahaan
		                AND a.nilai_kriteria = c.keterangan
	                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
	                AND a.No_Formula = '{TB_NoFormula.Text.Trim}'

	                ORDER BY Kode_Analisa, Jenis_Analisa
                "

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								DGV_Komponen.Rows.Add()
								DGV_Komponen.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Analisa")
								DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Jenis_Analisa")
								DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Kategori")
								DGV_Komponen.Rows(i).Cells(3).Value = .Rows(i).Item("Kriteria")
								DGV_Komponen.Rows(i).Cells(4).Value = .Rows(i).Item("Range_Awal")
								DGV_Komponen.Rows(i).Cells(5).Value = .Rows(i).Item("Range_Akhir")
							Next
						End If
					End With
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load moisture content: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		Else
			Try
				OpenConn()

				SQL = $"
	                SELECT
		                b.Kode_Analisa,
		                b.Jenis_Analisa,
		                CASE
			                WHEN ISNULL(b.Flag_Perhitungan, 'T') = 'Y'
				                THEN 'Perhitungan'
			                ELSE 'Non Perhitungan'
		                END AS Kategori,
		                '-' AS Kriteria,
		                a.Range_Awal,
		                a.Range_Akhir
	                FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
	                INNER JOIN N_EMI_LAB_Jenis_Analisa b
		                ON a.Id_Jenis_Analisa = b.id
	                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
	                AND a.No_Formula = '{TB_NoFormula.Text.Trim}'

	                UNION ALL

	                SELECT
		                b.Kode_Analisa,
		                b.Jenis_Analisa,
		                'Non Perhitungan' AS Kategori,
		                c.Label_Keterangan AS Kriteria,
		                '' AS Range_Awal,
		                '' AS Range_Akhir
	                FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
	                INNER JOIN N_EMI_LAB_Jenis_Analisa b
		                ON a.Id_Jenis_Analisa = b.id
	                INNER JOIN EMI_Switch c
		                ON a.Kode_Perusahaan = c.kode_perusahaan
		                AND a.nilai_kriteria = c.keterangan
	                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
	                AND a.No_Formula = '{TB_NoFormula.Text.Trim}'

	                ORDER BY Kode_Analisa, Jenis_Analisa
                "

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								DGV_Komponen.Rows.Add()
								DGV_Komponen.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Analisa")
								DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Jenis_Analisa")
								DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Kategori")
								DGV_Komponen.Rows(i).Cells(3).Value = .Rows(i).Item("Kriteria")
								DGV_Komponen.Rows(i).Cells(4).Value = .Rows(i).Item("Range_Awal")
								DGV_Komponen.Rows(i).Cells(5).Value = .Rows(i).Item("Range_Akhir")
							Next
						End If
					End With
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load moisture content: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		End If
	End Sub

	Private Sub Load_Komponen_Cooking_Step()
		DGV_Komponen.Visible = False
		RTB_Komponen.Visible = True

		Try
			OpenConn()

			RTB_Komponen.Clear()

			SQL = $"
                SELECT TOP 1 Cooking_Step
                FROM Emi_Transaksi_Formulator_Cooking_Steps
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                  AND No_Faktur = '{TB_NoFormula.Text.Trim}'
                  AND Status IS NULL
                ORDER BY Tanggal DESC, Jam DESC
            "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Cooking_Step")) <> "" Then
						RTB_Komponen.Rtf = Dr("Cooking_Step")
					End If
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal load cooking step: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Private Sub Load_Komponen_Daftar_Split()
		RTB_Komponen.Visible = False
		DGV_Komponen.Visible = True

		Reset_Active_Menu_Tracking_Progress()
		DGV_Detail_Pengujian.Rows.Clear()

		With DGV_Komponen
			.AutoGenerateColumns = False
			.Columns.Clear()

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "DS_No_Split",
				.HeaderText = "No. Split",
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				 .Name = "DS_Tanggal_Split",
				 .HeaderText = "Tanggal Split",
				 .Width = 100,
				 .[ReadOnly] = True,
				 .SortMode = DataGridViewColumnSortMode.NotSortable
			 })

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "DS_Tanggal_Validasi",
				.HeaderText = "Tanggal Validasi",
				.Width = 100,
				.[ReadOnly] = True,
				.SortMode = DataGridViewColumnSortMode.NotSortable
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "DS_Jumlah",
				.HeaderText = "Jumlah",
				.[ReadOnly] = True,
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.SortMode = DataGridViewColumnSortMode.NotSortable,
				.DefaultCellStyle = New DataGridViewCellStyle With {
					 .Alignment = DataGridViewContentAlignment.MiddleRight,
					 .Format = "N2"
				}
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "DS_Satuan",
				.HeaderText = "Satuan",
				.[ReadOnly] = True,
				.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				.SortMode = DataGridViewColumnSortMode.NotSortable,
				.DefaultCellStyle = New DataGridViewCellStyle With {
					 .Alignment = DataGridViewContentAlignment.MiddleCenter
				}
			})

			.Columns.Add(New DataGridViewTextBoxColumn With {
				.Name = "DS_Status_Split",
				.HeaderText = "Status Split",
				.[ReadOnly] = True,
				.Width = 125,
				.SortMode = DataGridViewColumnSortMode.NotSortable,
				.DefaultCellStyle = New DataGridViewCellStyle With {
					 .Alignment = DataGridViewContentAlignment.MiddleCenter
				}
			})

			.Columns.Add(New DataGridViewButtonColumn With {
				.Name = "DS_Cetak",
				.HeaderText = "Cetak",
				.Text = "Cetak",
				.UseColumnTextForButtonValue = True,
				.Width = 125,
				.SortMode = DataGridViewColumnSortMode.NotSortable,
				.FlatStyle = FlatStyle.Flat,
				.DefaultCellStyle = New DataGridViewCellStyle With {
					.BackColor = Color.FromArgb(15, 86, 122),
					.ForeColor = Color.White,
					.SelectionBackColor = Color.FromArgb(15, 86, 122),
					.SelectionForeColor = Color.White,
					.Alignment = DataGridViewContentAlignment.MiddleCenter
				}
			})
		End With

		Try
			OpenConn()

			SQL = $"
                SELECT *
                FROM (
                    SELECT TOP 1
                        y.No_Transaksi,
                        FORMAT(y.Tanggal, 'dd MMM yyyy') AS Tanggal,
                        FORMAT(y.Tanggal_Validasi, 'dd MMM yyyy') AS Tanggal_Validasi,
                        y.Jumlah,
                        y.Satuan,
                        'Trial Kitchen' AS Status
                    FROM N_EMI_Transaksi_Trial_Order_Produksi x
                    JOIN N_EMI_Transaksi_Trial_Split_Production_Order y
                        ON y.Kode_Perusahaan = x.Kode_Perusahaan
                        AND y.No_PO = x.No_Faktur
                    WHERE x.Kode_Perusahaan = '{KodePerusahaan}' AND x.Kode_Formula = '{TB_NoFormula.Text.Trim}'
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) a

                UNION ALL

                SELECT *
                FROM (
                    SELECT TOP 1
                        y.No_Transaksi,
                        FORMAT(y.Tanggal, 'dd MMM yyyy') AS Tanggal,
                        FORMAT(y.Tanggal, 'dd MMM yyyy') AS Tanggal_Validasi,
                        y.Jumlah,
                        y.Satuan,
                        'Trial Produksi' AS Status
                    FROM EMI_Order_Produksi x
                    JOIN Emi_Split_Production_Order y
                        ON y.Kode_Perusahaan = x.Kode_Perusahaan
                        AND y.No_PO = x.No_Faktur
                    WHERE x.Kode_Perusahaan = '{KodePerusahaan}' AND x.Kode_Formula = '{TB_NoFormula.Text.Trim}'
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) b
            "

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							DGV_Komponen.Rows.Add()
							DGV_Komponen.Rows(i).Cells(0).Value = .Rows(i).Item("No_Transaksi")
							DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Tanggal")
							DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Tanggal_Validasi")
							DGV_Komponen.Rows(i).Cells(3).Value = Format(.Rows(i).Item("Jumlah"), "N2")
							DGV_Komponen.Rows(i).Cells(4).Value = .Rows(i).Item("Satuan")
							DGV_Komponen.Rows(i).Cells(5).Value = .Rows(i).Item("Status")
							DGV_Komponen.Rows(i).Cells(6).Value = "Cetak Laporan"
						Next
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal load daftar split: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Private Sub Load_Tracking_Progress_Look_View()
		DGV_Detail_Pengujian.Rows.Clear()

		With DGV_Detail_Pengujian
			.AutoGenerateColumns = False
			.Columns.Clear()

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Nama_Mesin",
					.HeaderText = "Nama Mesin",
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.[ReadOnly] = True,
					.SortMode = DataGridViewColumnSortMode.NotSortable
				})

			.Columns.Add(New DataGridViewTextBoxColumn With {
					 .Name = "PLT_Jenis_Analisa",
					 .HeaderText = "Jenis Analisa",
					 .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
					 .[ReadOnly] = True,
					 .SortMode = DataGridViewColumnSortMode.NotSortable
				 })

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Standar_Min",
					.HeaderText = "Standar Min",
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.[ReadOnly] = True,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleRight,
						 .Format = "N2"
					}
				})

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Standar_Max",
					.HeaderText = "Standar Max",
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.[ReadOnly] = True,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleRight,
						 .Format = "N2"
					}
				})

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Avg_Hasil",
					.HeaderText = "Avg Hasil",
					.[ReadOnly] = True,
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleCenter
					}
				})

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Hasil_Uji",
					.HeaderText = "Hasil Uji",
					.[ReadOnly] = True,
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleCenter
					}
				})
		End With

		If TB_SplitTypeKomponen.Text = "TRIAL_KITCHEN" Then
			Try
				OpenConn()

				SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            ROUND(AVG(b.Hasil), 2) AS Avg_Hasil,

                            ISNULL(e.Keterangan_Kriteria, '-') AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'
                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_LIMS_PO_Sampel a

                        JOIN N_EMI_LIMS_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LIMS_Uji_Pra_Final upf
                            ON b.No_Po_Sampel = upf.No_Sampel

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON b.Id_Jenis_Analisa = e.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan IS NULL
                            AND CAST(b.Hasil AS VARCHAR(50)) = CAST(e.Nilai_Kriteria AS VARCHAR(50))

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            b.Flag_Approval = 'Y'
                            AND a.Status IS NULL
                            AND b.Flag_Selesai = 'Y'
                            AND b.Status IS NULL

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                        AND Kode_Aktivitas_Lab = 'LCKV'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
							.Cells(0).Value = Dr("Nama_Mesin")
							.Cells(1).Value = Dr("Jenis_Analisa")
							.Cells(2).Value = Format(Val(HilangkanTanda(Dr("Std_Min"))), "N4")
							.Cells(3).Value = Format(Val(HilangkanTanda(Dr("Std_Max"))), "N4")
							.Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
							.Cells(4).Value = Dr("keterangan_kriteria")
							.Cells(5).Value = Dr("Hasil_Uji")
						End With
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load tracking progress look view: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		ElseIf TB_SplitType.Text = "TRIAL_PRODUKSI" Then
			Try
				OpenConn()

				SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            ROUND(AVG(b.Hasil), 2) AS Avg_Hasil,

                            ISNULL(e.Keterangan_Kriteria, '-') AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'
                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,

                            CASE

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_EMI_LAB_PO_Sampel a

                        JOIN N_EMI_LAB_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON b.Id_Jenis_Analisa = e.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan IS NULL
                            AND CAST(b.Hasil AS VARCHAR(50)) = CAST(e.Nilai_Kriteria AS VARCHAR(50))

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            a.Status IS NULL
                            AND b.Status IS NULL
                            AND a.Flag_Trial_Produksi = 'Y'

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                        AND Kode_Aktivitas_Lab = 'LCKV'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
							.Cells(0).Value = Dr("Nama_Mesin")
							.Cells(1).Value = Dr("Jenis_Analisa")
							.Cells(2).Value = Format(Val(HilangkanTanda(Dr("Std_Min"))), "N4")
							.Cells(3).Value = Format(Val(HilangkanTanda(Dr("Std_Max"))), "N4")
							.Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
							.Cells(4).Value = Dr("keterangan_kriteria")
							.Cells(5).Value = Dr("Hasil_Uji")
						End With
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load tracking progress look view: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		End If
	End Sub

	Private Sub Load_Tracking_Progress_Analisa_Lab()
		DGV_Detail_Pengujian.Rows.Clear()

		With DGV_Detail_Pengujian
			.AutoGenerateColumns = False
			.Columns.Clear()

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Nama_Mesin",
					.HeaderText = "Nama Mesin",
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.[ReadOnly] = True,
					.SortMode = DataGridViewColumnSortMode.NotSortable
				})

			.Columns.Add(New DataGridViewTextBoxColumn With {
					 .Name = "PLT_Jenis_Analisa",
					 .HeaderText = "Jenis Analisa",
					 .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
					 .[ReadOnly] = True,
					 .SortMode = DataGridViewColumnSortMode.NotSortable
				 })

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Standar_Min",
					.HeaderText = "Standar Min",
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.[ReadOnly] = True,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleRight,
						 .Format = "N2"
					}
				})

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Standar_Max",
					.HeaderText = "Standar Max",
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.[ReadOnly] = True,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleRight,
						 .Format = "N2"
					}
				})

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Avg_Hasil",
					.HeaderText = "Avg Hasil",
					.[ReadOnly] = True,
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleCenter
					}
				})

			.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Hasil_Uji",
					.HeaderText = "Hasil Uji",
					.[ReadOnly] = True,
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleCenter
					}
				})
		End With

		If TB_SplitTypeKomponen.Text = "TRIAL_KITCHEN" Then
			Try
				OpenConn()

				SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            ROUND(AVG(b.Hasil), 2) AS Avg_Hasil,

                            ISNULL(e.Keterangan_Kriteria, '-') AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'
                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_LIMS_PO_Sampel a

                        JOIN N_EMI_LIMS_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LIMS_Uji_Pra_Final upf
                            ON b.No_Po_Sampel = upf.No_Sampel

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON b.Id_Jenis_Analisa = e.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan IS NULL
                            AND CAST(b.Hasil AS VARCHAR(50)) = CAST(e.Nilai_Kriteria AS VARCHAR(50))

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            b.Flag_Approval = 'Y'
                            AND a.Status IS NULL
                            AND b.Flag_Selesai = 'Y'
                            AND b.Status IS NULL

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                        AND Kode_Aktivitas_Lab = 'ANL'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
							.Cells(0).Value = Dr("Nama_Mesin")
							.Cells(1).Value = Dr("Jenis_Analisa")
							.Cells(2).Value = Format(Val(HilangkanTanda(Dr("Std_Min"))), "N4")
							.Cells(3).Value = Format(Val(HilangkanTanda(Dr("Std_Max"))), "N4")
							.Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
							.Cells(4).Value = Dr("keterangan_kriteria")
							.Cells(5).Value = Dr("Hasil_Uji")
						End With
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load tracking progress look view: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		ElseIf TB_SplitType.Text = "TRIAL_PRODUKSI" Then
			Try
				OpenConn()

				SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            ROUND(AVG(b.Hasil), 2) AS Avg_Hasil,

                            ISNULL(e.Keterangan_Kriteria, '-') AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'
                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,

                            CASE

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_EMI_LAB_PO_Sampel a

                        JOIN N_EMI_LAB_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON b.Id_Jenis_Analisa = e.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan IS NULL
                            AND CAST(b.Hasil AS VARCHAR(50)) = CAST(e.Nilai_Kriteria AS VARCHAR(50))

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            a.Status IS NULL
                            AND b.Status IS NULL
                            AND a.Flag_Trial_Produksi = 'Y'

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                        AND Kode_Aktivitas_Lab = 'ANL'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
							.Cells(0).Value = Dr("Nama_Mesin")
							.Cells(1).Value = Dr("Jenis_Analisa")
							.Cells(2).Value = Format(Val(HilangkanTanda(Dr("Std_Min"))), "N4")
							.Cells(3).Value = Format(Val(HilangkanTanda(Dr("Std_Max"))), "N4")
							.Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
							.Cells(4).Value = Dr("keterangan_kriteria")
							.Cells(5).Value = Dr("Hasil_Uji")
						End With
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load tracking progress look view: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		End If
	End Sub

	Private Sub Load_Tracking_Progress_Palatabilitas()
		DGV_Detail_Pengujian.Rows.Clear()

		If TB_SplitTypeKomponen.Text = "TRIAL_KITCHEN" Then

			With DGV_Detail_Pengujian
				.AutoGenerateColumns = False
				.Columns.Clear()

				.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Nama_Mesin",
					.HeaderText = "Nama Mesin",
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.[ReadOnly] = True,
					.SortMode = DataGridViewColumnSortMode.NotSortable
				})

				.Columns.Add(New DataGridViewTextBoxColumn With {
					 .Name = "PLT_Jenis_Analisa",
					 .HeaderText = "Jenis Analisa",
					 .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
					 .[ReadOnly] = True,
					 .SortMode = DataGridViewColumnSortMode.NotSortable
				 })

				.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Standar_Min",
					.HeaderText = "Standar Min",
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.[ReadOnly] = True,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleRight,
						 .Format = "N2"
					}
				})

				.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Standar_Max",
					.HeaderText = "Standar Max",
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.[ReadOnly] = True,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleRight,
						 .Format = "N2"
					}
				})

				.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Avg_Hasil",
					.HeaderText = "Avg Hasil",
					.[ReadOnly] = True,
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleCenter
					}
				})

				.Columns.Add(New DataGridViewTextBoxColumn With {
					.Name = "PLT_Hasil_Uji",
					.HeaderText = "Hasil Uji",
					.[ReadOnly] = True,
					.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					.SortMode = DataGridViewColumnSortMode.NotSortable,
					.DefaultCellStyle = New DataGridViewCellStyle With {
						 .Alignment = DataGridViewContentAlignment.MiddleCenter
					}
				})
			End With

			Try
				OpenConn()

				SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            ROUND(AVG(b.Hasil), 2) AS Avg_Hasil,

                            ISNULL(e.Keterangan_Kriteria, '-') AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '0')
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'
                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'LCKV'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = 'ANL'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'
                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_LIMS_PO_Sampel a

                        JOIN N_EMI_LIMS_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LIMS_Uji_Pra_Final upf
                            ON b.No_Po_Sampel = upf.No_Sampel

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON b.Id_Jenis_Analisa = e.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan IS NULL
                            AND CAST(b.Hasil AS VARCHAR(50)) = CAST(e.Nilai_Kriteria AS VARCHAR(50))

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            b.Flag_Approval = 'Y'
                            AND a.Status IS NULL
                            AND b.Flag_Selesai = 'Y'
                            AND b.Status IS NULL

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                        AND Kode_Aktivitas_Lab = 'PLT'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
							.Cells(0).Value = Dr("Nama_Mesin")
							.Cells(1).Value = Dr("Jenis_Analisa")
							.Cells(2).Value = Format(Val(HilangkanTanda(Dr("Std_Min"))), "N4")
							.Cells(3).Value = Format(Val(HilangkanTanda(Dr("Std_Max"))), "N4")
							.Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
							.Cells(4).Value = Dr("keterangan_kriteria")
							.Cells(5).Value = Dr("Hasil_Uji")
						End With
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load tracking progress look view: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		ElseIf TB_SplitType.Text = "TRIAL_PRODUKSI" Then

			DGV_Detail_Pengujian.Columns.Clear()

			Try
				OpenConn()

				Dim SQL As String = $"
                    WITH CTE AS (
                        SELECT
                            d.Jenis_Analisa AS Metric,
                            a.No_Sampel,
                            f.Nama_Mesin AS Mesin,
                            e.Keterangan AS Header,
                            FORMAT(c.Value_Parameter, '0.################') AS Header_Value,
                            b.Id_Jenis_Analisa,
                            e.Id_QC_Formula,
                            ROW_NUMBER() OVER (
                                PARTITION BY d.Jenis_Analisa, a.No_Sampel, e.Keterangan, c.Value_Parameter
                                ORDER BY b.Id_Jenis_Analisa
                            ) AS rn
                        FROM N_EMI_LAB_PO_Sampel a
                        JOIN N_EMI_LAB_Uji_Sampel b
                            ON b.Kode_Perusahaan = a.Kode_Perusahaan
                            AND a.No_Sampel = b.No_Po_Sampel
                        LEFT JOIN N_EMI_LAB_Uji_Sampel_Detail c
                            ON c.Kode_Perusahaan = b.Kode_Perusahaan
                            AND b.No_Faktur = c.No_Faktur_Uji_Sample
                        JOIN N_EMI_LAB_Jenis_Analisa d
                            ON d.id = b.Id_Jenis_Analisa
                            AND d.Kode_Aktivitas_Lab = 'PLT'
                        JOIN EMI_Quality_Control e
                            ON e.Id_QC_Formula = c.Id_Quality_Control
                        JOIN EMI_Master_Mesin f
                            ON f.Kode_Perusahaan = a.Kode_Perusahaan
                            AND f.Id_Master_Mesin = a.Id_Mesin
                        WHERE
                            a.No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                            AND a.Flag_Trial_Produksi = 'Y'
                    )
                    SELECT Metric, No_Sampel, Mesin, Header, Header_Value
                    FROM CTE
                    WHERE rn = 1
                    ORDER BY Id_Jenis_Analisa, Id_QC_Formula
                "

				Dim list As New List(Of PalatRow)
				Dim headerSet As New HashSet(Of String)

				Using Dr = OpenTrans(SQL)

					While Dr.Read()

						Dim metric = Dr("Metric").ToString()
						Dim noSample = Dr("No_Sampel").ToString()
						Dim mesin = Dr("Mesin").ToString()
						Dim header = Dr("Header").ToString()
						Dim value = Dr("Header_Value").ToString()

						headerSet.Add(header)

						Dim key = metric & "|" & noSample & "|" & mesin

						Dim row = list.FirstOrDefault(Function(x) x.Metric & "|" & x.No_Sample & "|" & x.Mesin = key)

						If row Is Nothing Then
							row = New PalatRow With {
								.Metric = metric,
								.No_Sample = noSample,
								.Mesin = mesin,
								.Headers = New Dictionary(Of String, String)
							}
							list.Add(row)
						End If

						If Not row.Headers.ContainsKey(header) Then
							row.Headers(header) = value
						End If

					End While

				End Using

				CloseConn()

				DGV_Detail_Pengujian.Columns.Add("Metric", "Metric")
				DGV_Detail_Pengujian.Columns.Add("No_Sample", "No Sample")
				DGV_Detail_Pengujian.Columns.Add("Mesin", "Mesin")

				For Each h In headerSet
					DGV_Detail_Pengujian.Columns.Add(h, h)
				Next

				For Each item In list

					Dim idx = DGV_Detail_Pengujian.Rows.Add()
					Dim r = DGV_Detail_Pengujian.Rows(idx)

					r.Cells("Metric").Value = item.Metric
					r.Cells("No_Sample").Value = item.No_Sample
					r.Cells("Mesin").Value = item.Mesin

					For Each h In headerSet

						Dim val As String = "-"

						If item.Headers.ContainsKey(h) Then
							val = item.Headers(h)
						End If

						r.Cells(h).Value = val
						r.Cells(h).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
					Next
				Next
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal load trial produksi palatabilitas: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try
		End If
	End Sub

	Private Sub Load_Laporan_Trial_Kitchen()
		Try
			Me.Cursor = Cursors.WaitCursor
			Application.DoEvents()

			Dim no_split As String = TB_NoSplitKomponen.Text.Trim
			Dim pdfStream As MemoryStream = Get_Pdf_Stream_Laporan_Trial_Kitchen(Url_Api_Laporan_Formulator, no_split)
			Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

			Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
				pdfStream.CopyTo(file)
			End Using

			Dim frm As New N_EMI_PDF_Viewer(tempPath, "Laporan Formula Trial Kitchen")
			frm.ShowDialog()

			Me.Cursor = Cursors.Default
		Catch ex As Exception
			Me.Cursor = Cursors.Default
			MessageBox.Show("Gagal load laporan trial kitchen: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Public Function Get_Pdf_Stream_Laporan_Trial_Kitchen(url As String, no_split As String) As MemoryStream
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

	Private Sub Load_Laporan_Trial_Produksi()
		Try
			Me.Cursor = Cursors.WaitCursor
			Application.DoEvents()

			Dim no_split As String = TB_NoSplitKomponen.Text.Trim
			Dim pdfStream As MemoryStream = Get_Pdf_Stream_Laporan_Trial_Produksi(Url_Api_Laporan_Formulator_Trial_Produksi, no_split)
			Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

			Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
				pdfStream.CopyTo(file)
			End Using

			Dim frm As New N_EMI_PDF_Viewer(tempPath, "Laporan Formula Trial Produksi")
			frm.ShowDialog()

			Me.Cursor = Cursors.Default
		Catch ex As Exception
			Me.Cursor = Cursors.Default
			MessageBox.Show("Gagal load laporan trial produksi: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Public Function Get_Pdf_Stream_Laporan_Trial_Produksi(url As String, no_split As String) As MemoryStream
		Dim bahan As New List(Of Dictionary(Of String, Object))
		Dim hpp As New Dictionary(Of String, Object)
		Dim namaFormula As String = ""
		Dim kategoriProduk As String = ""
		Dim tanggalUji As String = ""

		Try
			OpenConn()

			SQL = $"select c.No_Faktur as kode_formula, b.Nama as nama_produk, format(c.Tanggal, 'dd MMM yyyy') as tanggal_uji
				from N_EMI_LAB_PO_Sampel a
				cross apply (
					select top 1 *
					from Barang b
					where a.Kode_Perusahaan = b.Kode_Perusahaan
					and a.Kode_Barang = b.Kode_Barang
					order by b.Kode_Stock_Owner
				) b
				join Emi_Transaksi_Formulator c
					on c.Kode_Perusahaan = a.Kode_Perusahaan
					and c.Kode_Barang = b.Kode_Barang_Inq
					--and c.Status is null
				where a.No_Split_Po = '{no_split}'
			"

			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					namaFormula = Dr("kode_formula").ToString()
					kategoriProduk = Dr("nama_produk").ToString()
					tanggalUji = Dr("tanggal_uji").ToString()
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Return Nothing
		End Try

		Dim payload As New Dictionary(Of String, Object) From {
			{"no_split", no_split},
			{"nama_formula", namaFormula},
			{"kategori_produk", kategoriProduk},
			{"tanggal_uji", tanggalUji}
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

	Private Sub DGV_Formula_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Formula.CellClick
		If e.RowIndex < 0 Then Exit Sub

		TB_NoFormula.Text = DGV_Formula.CurrentRow.Cells(0).Value?.ToString()
		TB_NoSplitKomponen.Text = ""
		TB_SplitTypeKomponen.Text = ""

		TLP_TrackingProgress.Enabled = False
		Reset_Active_Menu_Tracking_Progress()

		Try
			OpenConn()

			SQL = $"
                    SELECT
                        a.No_Faktur,
                        tk.No_Transaksi AS No_Split_Trial_Kitchen,
                        tp.No_Transaksi AS NO_Split_Trial_Produksi
                    FROM EMI_Transaksi_Formulator a
                    OUTER APPLY (
                        SELECT TOP 1 y.No_Transaksi
                        FROM N_EMI_Transaksi_Trial_Order_Produksi x
                        JOIN N_EMI_Transaksi_Trial_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                        WHERE x.Kode_Formula = a.No_Faktur
                        ORDER BY y.Tanggal DESC, y.Jam DESC
                    ) tk
                    OUTER APPLY (
                        SELECT TOP 1 y.No_Transaksi
                        FROM EMI_Order_Produksi x
                        JOIN Emi_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                        WHERE x.Kode_Formula = a.No_Faktur
                        ORDER BY y.Tanggal DESC, y.Jam DESC
                    ) tp
                    WHERE a.No_Faktur = '{TB_NoFormula.Text.Trim}'
                "

			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					Dim noSplitProduksi As String = If(General_Class.CekNULL(Dr("NO_Split_Trial_Produksi")) = "", "", Dr("NO_Split_Trial_Produksi").ToString())
					Dim noSplitKitchen As String = If(General_Class.CekNULL(Dr("No_Split_Trial_Kitchen")) = "", "", Dr("No_Split_Trial_Kitchen").ToString())

					If noSplitProduksi <> "" Then
						TB_NoSplitFormula.Text = noSplitProduksi
						TB_SplitType.Text = "TRIAL_PRODUKSI"
					ElseIf noSplitKitchen <> "" Then
						TB_NoSplitFormula.Text = noSplitKitchen
						TB_SplitType.Text = "TRIAL_KITCHEN"
					Else
						TB_NoSplitFormula.Text = ""
						TB_SplitType.Text = ""
					End If
				Else
					TB_NoSplitFormula.Text = ""
					TB_SplitType.Text = ""
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal load no split: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try

		If ActiveMenuKomponen IsNot Nothing Then
			If ActiveMenuKomponen.Text = "HPP Sementara" Then
				Load_Komponen_HPP_Sementara("HPP Sementara")
			ElseIf ActiveMenuKomponen.Text = "Bahan Material" Then
				Load_Komponen_Bahan_Material()
			ElseIf ActiveMenuKomponen.Text = "Moisture Content" Then
				Load_Komponen_Moisture_Content()
			ElseIf ActiveMenuKomponen.Text = "Cooking Step" Then
				Load_Komponen_Cooking_Step()
			ElseIf ActiveMenuKomponen.Text = "Daftar Split" Then
				Load_Komponen_Daftar_Split()
			End If
		End If
	End Sub

	Private Sub DGV_Komponen_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Komponen.CellClick
		If e.RowIndex < 0 Then Exit Sub

		If ActiveMenuKomponen.Text = "Daftar Split" Then
			TB_NoSplitKomponen.Text = DGV_Komponen.Rows(e.RowIndex).Cells(0).Value?.ToString()
			TB_SplitTypeKomponen.Text = DGV_Komponen.Rows(e.RowIndex).Cells(5).Value?.ToString().ToUpper().Replace(" ", "_")
			TLP_TrackingProgress.Enabled = True

			If DGV_Komponen.Columns(e.ColumnIndex).Name = "DS_Cetak" Then
				If TB_SplitTypeKomponen.Text = "TRIAL_KITCHEN" Then
					Load_Laporan_Trial_Kitchen()
				ElseIf TB_SplitTypeKomponen.Text = "TRIAL_PRODUKSI" Then
					Load_Laporan_Trial_Produksi()
				End If
			Else
				If ActiveMenuTrackingProgress IsNot Nothing Then
					If ActiveMenuTrackingProgress.Text = "Look View" Then
						Load_Tracking_Progress_Look_View()
					ElseIf ActiveMenuTrackingProgress.Text = "Analisa Lab" Then
						Load_Tracking_Progress_Analisa_Lab()
					ElseIf ActiveMenuTrackingProgress.Text = "Palatabilitas" Then
						Load_Tracking_Progress_Palatabilitas()
					End If
				Else
					Dim firstMenu As Label = TryCast(TLP_TrackingProgress.GetControlFromPosition(0, 0), Label)

					If firstMenu IsNot Nothing Then
						Set_Active_Menu_Tracking_Progress(firstMenu)
						Load_Tracking_Progress_Look_View()
					End If
				End If
			End If
		End If
	End Sub

	Private Sub Get_Data_DGV_Parent(ByVal index As Integer)
		DgvParent_NoFormula = DGV_Formula.Rows(index).Cells(CellParent_NoFormula).Value
		DgvParent_TanggalFormula = DGV_Formula.Rows(index).Cells(CellParent_TanggalFormula).Value
		DgvParent_KdBarang = DGV_Formula.Rows(index).Cells(CellParent_KdBarang).Value
		DgvParent_NmBarang = DGV_Formula.Rows(index).Cells(CellParent_NmBarang).Value
		DgvParent_HPPMin = DGV_Formula.Rows(index).Cells(CellParent_HPPMin).Value
		DgvParent_HPPMax = DGV_Formula.Rows(index).Cells(CellParent_HPPMax).Value
		DgvParent_Jumlah = DGV_Formula.Rows(index).Cells(CellParent_Jumlah).Value
		DgvParent_Satuan = DGV_Formula.Rows(index).Cells(CellParent_Satuan).Value
		DgvParent_JenisFormula = DGV_Formula.Rows(index).Cells(CellParent_JenisFormula).Value
		DgvParent_PosisiBinding = DGV_Formula.Rows(index).Cells(CellParent_PosisiBinding).Value
		DgvParent_StatusFormula = DGV_Formula.Rows(index).Cells(CellParent_StatusFormula).Value
		DgvParent_Deskripsi = DGV_Formula.Rows(index).Cells(CellParent_Deskripsi).Value
		DgvParent_BtnValidasi = DGV_Formula.Rows(index).Cells(CellParent_BtnValidasi).Value
	End Sub

	Private Sub DGV_Formula_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Formula.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		If e.ColumnIndex = CellParent_BtnValidasi Then
			Get_Data_DGV_Parent(e.RowIndex)

			Dim currentStatus As String = DgvParent_StatusFormula.Trim

			Dim userPos As String = UserPosition.Trim

			If userPos = "HEADDEPT" Then
				If Status_HeadDept.Contains(currentStatus) Then

					ShowFormHeadDept(DgvParent_NoFormula, DgvParent_StatusFormula)

				End If
			ElseIf userPos = "CLEVEL" Then
				If Status_BOD.Contains(currentStatus) Then
					ShowFormBOD(DgvParent_NoFormula)
				End If
			End If

		End If
	End Sub

	Private Sub ShowFormHeadDept(ByVal NoFaktur As String, ByVal Status As String)
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.Kosong()
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.TxtFormulator_NoFaktur.Text = NoFaktur
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.StatusDariList = Status
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.TxtFormulator_NoFaktur_Leave(DGV_Formula, New EventArgs)
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.ShowDialog()
	End Sub

	Private Sub ShowFormBOD(ByVal NoFaktur As String)
		Dim frm As New N_EMI_SD_Transaksi_Validasi_Formula_BOD With {
			.No_Faktur = NoFaktur
		}
		frm.ShowDialog()
	End Sub

	Private Sub Cmb_Filter_Status_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter_Status.SelectedIndexChanged
		DGV_Formula.Rows.Clear()
		Load_Formula()
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Cmb_Filter_Status.SelectedIndex = 0
		Reset_Active_Menu_Komponen()
		Reset_Active_Menu_Tracking_Progress()
		DGV_Komponen.Rows.Clear()
		DGV_Detail_Pengujian.Rows.Clear()
		Cb_Transaksi_Hari_Ini.Checked = False
		Cb_Parameter_Tanggal.Checked = False
		Cb_Parameter_Lain.Checked = False
		Cmb_Parameter_Lain.SelectedIndex = -1
		Cmb_Paramater_Tanggal.SelectedIndex = -1
		Tb_Value.Clear()

		Load_Formula()
	End Sub

	Private Sub Cb_Parameter_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Parameter_Tanggal.CheckedChanged
		If Cb_Parameter_Tanggal.Checked Then
			Cb_Transaksi_Hari_Ini.Checked = False
		Else
		End If
	End Sub

	Private Sub Cb_Transaksi_Hari_Ini_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Transaksi_Hari_Ini.CheckedChanged
		If Cb_Transaksi_Hari_Ini.Checked Then
			Cb_Parameter_Tanggal.Checked = False
			Load_Formula()
		Else
		End If
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		Load_Formula()
	End Sub

End Class

Public Class PalatRow
	Public Property Metric As String
	Public Property No_Sample As String
	Public Property Mesin As String
	Public Property Headers As Dictionary(Of String, String)
End Class
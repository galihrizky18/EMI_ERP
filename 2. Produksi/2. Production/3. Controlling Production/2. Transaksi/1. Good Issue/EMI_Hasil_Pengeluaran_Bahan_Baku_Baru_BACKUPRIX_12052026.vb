Imports System.Globalization

Public Class EMI_Hasil_Pengeluaran_Bahan_Baku_Baru_BACKUPRIX_12052026
	Private currentCell As DataGridViewCell = Nothing

	Public fno_po, fso As String
	Public Property asal As String = ""
	Public RFID_SelectedSplit, RFID_SelectedBatch As String

	Dim Arr_Detail_Biaya As New List(Of (akun As String, keterangan As String, nilai As Double, kd_so As String, kd_barang As String))

	Dim arrBarcodeScan As New List(Of (Kd_Barang As String, Serial_Number As String))

	Dim LvKode_Bahan As String
	Dim LvKode_Bahan_Pckg As String
	Dim LvKode_So As String
	Dim LvKode_So_Pckg As String
	Dim LvNilai_Formula As String
	Dim LvNilai_Formula_Pckg As String
	Dim LvNilai_Produksi As String
	Dim LvNilai_Produksi_Pckg As String
	Dim LvPotStokBhn As String
	Dim LvPotStokPckg As String
	Dim LvSatuan As String
	Dim LvSatuan_Pckg As String
	Dim LvStandarPrice As String
	Dim LvStandarPricePckg As String
	Dim LvJumlahInput As String
	Dim LvStatus As String
	Dim LvJumlahKebutuhan As String

	Dim CellKode_So As Integer = 0
	Dim CellKode_So_Pckg As Integer = 0
	Dim CellKode_Bahan As Integer = 1
	Dim CellKode_Bahan_Pckg As Integer = 1
	Dim CellNilai_Formula As Integer = 2
	Dim CellNilai_Formula_Pckg As Integer = 2
	Dim CellNilai_Produksi As Integer = 3
	Dim CellNilai_Produksi_Pckg As Integer = 3
	Dim CellPotStokBhn As Integer = 5
	Dim CellPotStokPckg As Integer = 5
	Dim CellSatuan As Integer = 4
	Dim CellSatuan_Pckg As Integer = 4
	Dim CellStandarPrice As Integer = 6
	Dim CellStandarPricePckg As Integer = 6
	Dim CellJumlahInput As Integer = 7
	Dim CellStatus As Integer = 8
	Dim CellJumlahKebutuhan As Integer = 9
	Dim CellNamaBahan As Integer = 10

	Dim Jenis = "Display_Production_Order"

	Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
		LvKode_So = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_So).Value
		LvKode_Bahan = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_Bahan).Value
		LvNilai_Formula = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Formula).Value
		LvNilai_Produksi = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Produksi).Value
		LvSatuan = Dgv_HslProduction.Rows(No_Index).Cells(CellSatuan).Value
		LvPotStokBhn = Dgv_HslProduction.Rows(No_Index).Cells(CellPotStokBhn).Value
		LvStandarPrice = Dgv_HslProduction.Rows(No_Index).Cells(CellStandarPrice).Value
		LvJumlahInput = Dgv_HslProduction.Rows(No_Index).Cells(CellJumlahInput).Value
		LvStatus = Dgv_HslProduction.Rows(No_Index).Cells(CellStatus).Value
		LvJumlahKebutuhan = Dgv_HslProduction.Rows(No_Index).Cells(CellJumlahKebutuhan).Value
	End Sub

	Public Sub Get_Isi_Listview_Pckg(ByVal No_Index As Integer)

		LvKode_So_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_So_Pckg).Value
		LvKode_Bahan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_Bahan_Pckg).Value
		LvNilai_Formula_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Formula_Pckg).Value
		LvNilai_Produksi_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Produksi_Pckg).Value
		LvSatuan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellSatuan_Pckg).Value
		LvPotStokPckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellPotStokPckg).Value
		LvStandarPricePckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellStandarPricePckg).Value

	End Sub

	Private Sub cekdata(ByVal index As Integer)
		If Dgv_HslProduction.Rows.Count = 0 Then
			Exit Sub
		End If

		'Try
		'    OpenConn()

		'For index = 0 To Dgv_HslProduction.Rows.Count - 1
		Get_Isi_Listview(index)

		Dim Jumlah_Dosing As Double = 0
		Dim Selesai_Dosing As String = ""
		SQL = "select top 1 a.No_Transaksi, a.No_Production_Order, b.Proses, isnull(b.Selesai,'T') as Selesai, b.urut, b.Nilai_Produksi "
		SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b "
		SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
		SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
		SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
		SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
		SQL = SQL & "and b.Kode_Stock_Owner = '" & LvKode_So & "' "
		SQL = SQL & "and b.Kode_Barang = '" & LvKode_Bahan & "' "
		SQL = SQL & "order by b.proses desc "
		Using Dr = OpenTrans(SQL)
			If Dr.Read Then

				Jumlah_Dosing = Val(HilangkanTanda(Format(Dr("Nilai_Produksi"), "N4")))

			End If
		End Using

		Dim Toleransi_timbang_Min As Double = 0
		Dim Toleransi_timbang_Max As Double = 0

		Dim nilai_dosing As Double = Val(HilangkanTanda(LvNilai_Produksi))

		Dim Flag_NonBarcode As String = ""
		SQL = "Select top(1) Toleransi_Timbang_Min, Toleransi_Timbang_Max, Flag_Non_Barcode from "
		SQL = SQL & "barang a where "
		SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
		SQL = SQL & "Kode_barang='" & LvKode_Bahan & "' "
		Using dr = OpenTrans(SQL)
			If dr.Read Then
				Flag_NonBarcode = General_Class.CekNULL(dr("Flag_Non_Barcode"))
				Toleransi_timbang_Min = dr("Toleransi_Timbang_Min")
				Toleransi_timbang_Max = dr("Toleransi_Timbang_Max")
			Else
				dr.Close()
				CloseTrans()
				CloseConn()
				MessageBox.Show("Kode barang tidak ditemukan . . ! !")
				Exit Sub
			End If
		End Using

		Dim Nilai_Formula As Double = 0
		SQL = "Select c.Kode_Barang, "
		SQL = SQL & "isnull(( "

		SQL = SQL & "round( "

		SQL = SQL & "(c.Jumlah / (select dbo.Ubah_Satuan(a.Kode_Perusahaan, 'masa', c.Kode_Barang, z.Satuan_Hasil, c.satuan, z.Hasil) from Emi_Transaksi_Formulator z "
		SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur) "
		SQL = SQL & ") "

		SQL = SQL & "* "

		SQL = SQL & "(ISNULL((a.Qty_Batch), 0)),4)), 0) as Nilai_Formula "
		SQL = SQL & "From Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
		SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan "
		SQL = SQL & "And a.No_PO = b.No_Faktur "
		SQL = SQL & "And b.Kode_Formula = c.No_Faktur "
		SQL = SQL & "And a.No_Transaksi = '" & Txt_No_Split_PO.Text & "' "
		SQL = SQL & "And c.Kode_Barang = '" & LvKode_Bahan & "' "
		Using dr = OpenTrans(SQL)
			If dr.Read Then
				Nilai_Formula = Val(HilangkanTanda(Format(dr("Nilai_Formula"), "N4")))
			Else
				dr.Close()
				CloseTrans()
				CloseConn()
				MessageBox.Show("Nilai Formula Tidak di temukan . . ! !")
				Exit Sub
			End If
		End Using

		If (Jumlah_Dosing + Val(HilangkanTanda(LvNilai_Produksi))) > Nilai_Formula + ((Toleransi_timbang_Max / 100) * Nilai_Formula) Then
			Dgv_HslProduction.Rows(index).DefaultCellStyle.BackColor = Color.Red
		Else

			If (Jumlah_Dosing + Val(HilangkanTanda(LvNilai_Produksi))) > Nilai_Formula - ((Toleransi_timbang_Min / 100) * Nilai_Formula) Then
				Dgv_HslProduction.Rows(index).DefaultCellStyle.BackColor = Color.LightGreen
			Else
				Dgv_HslProduction.Rows(index).DefaultCellStyle.BackColor = Color.LightYellow
			End If

		End If

		'Next

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

	End Sub

	Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.Dock = DockStyle.Fill

		AddHandler Keypad1.ValueChanged, AddressOf Keypad1_ValueChanged_Handler
		AddHandler Me.MouseDown, AddressOf Form_MouseDown
		AddHandler Dgv_HslProduction.MouseDown, AddressOf Form_MouseDown

		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			Dgv_HslProduction.Columns(CellStatus).DisplayIndex = 3
			Dgv_HslProduction.Columns(CellStatus).Width = 150
			Dgv_HslProduction.Columns(CellJumlahKebutuhan).DisplayIndex = 4
			Dgv_HslProduction.Columns(CellJumlahInput).DisplayIndex = 5
			Dgv_HslProduction.Columns(CellJumlahInput).SortMode = DataGridViewColumnSortMode.NotSortable
			Dgv_HslProduction.Columns(CellNamaBahan).DisplayIndex = 2
			Dgv_HslProduction.Columns(CellNamaBahan).Width = 325
			Dgv_HslProduction.Columns(CellNamaBahan).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
			Dgv_HslProduction.Columns(CellKode_Bahan).Width = 150

			Label1.Text = "Transaksi - Pengeluaran Bahan Baku (Dosing)"
			' Label8.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi
			Label6.Text = Base_Language.Lang_Global_NoFaktur
			'Label7.Text = Base_Language.Lang_Global_Tanggal_Produksi
			'Label2.Text = Base_Language.Lang_Global_Jam
			Label10.Text = Base_Language.Lang_Global_NamaBarang
			'  Label9.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi2
			Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

			'ListView2.Columns.Clear()
			'ListView2.Columns.Add(Base_Language.Lang_Global_No_PO, 140, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 140, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 200, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 130, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 220, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center)
			'ListView2.Columns.Add(Base_Language.Lang_Global_Satuan, 90, HorizontalAlignment.Center)
			'ListView2.View = View.Details

			Dgv_HslProduction.Columns(0).HeaderText = Base_Language.Lang_Global_Lokasi
			Dgv_HslProduction.Columns(1).HeaderText = Base_Language.Lang_Global_Kode_Bahan
			' Dgv_HslProduction.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
			Dgv_HslProduction.Columns(2).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
			'Dgv_HslProduction.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
			Dgv_HslProduction.Columns(4).HeaderText = Base_Language.Lang_Global_Satuan

			Dgv_Hasil_Production_Packaging.Columns(0).HeaderText = Base_Language.Lang_Global_Lokasi
			Dgv_Hasil_Production_Packaging.Columns(1).HeaderText = Base_Language.Lang_Global_Kode_Bahan
			' Dgv_Hasil_Production_Packaging.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
			Dgv_Hasil_Production_Packaging.Columns(2).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
			'Dgv_Hasil_Production_Packaging.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
			Dgv_Hasil_Production_Packaging.Columns(4).HeaderText = Base_Language.Lang_Global_Satuan

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If asal = "CONTROLLING" Then
			Txt_QR.Enabled = False
			Txt_QR.BackColor = Color.FromArgb(235, 235, 235)
			Btn_Scan.Enabled = False
			Dgv_HslProduction.Columns(CellNilai_Produksi).ReadOnly = False

		ElseIf asal = "INDEPENDENT" Then
			Txt_QR.Enabled = True
			Txt_QR.BackColor = Color.White
			Btn_Scan.Enabled = True
			Dgv_HslProduction.Columns(CellNilai_Produksi).ReadOnly = True
		ElseIf asal = "DISPLAY_RFID" Then
			Txt_QR.Enabled = False
			Txt_QR.BackColor = Color.FromArgb(235, 235, 235)
			Btn_Scan.Enabled = False
			Dgv_HslProduction.Columns(CellNilai_Produksi).ReadOnly = False
		End If

		Kosong()
	End Sub

	Private Sub Dgv_HslProduction_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_HslProduction.CellEnter
		If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then
			Keypad1.Visible = False
			currentCell = Nothing
			Return
		End If

		currentCell = Dgv_HslProduction.Rows(e.RowIndex).Cells(e.ColumnIndex)

		If currentCell.ColumnIndex = Dgv_HslProduction.Columns("Column5").Index AndAlso Not currentCell.ReadOnly Then
			Dim cellRect As Rectangle = Dgv_HslProduction.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
			Dim dgvScreenPoint As Point = Dgv_HslProduction.PointToScreen(cellRect.Location)
			Dim formPoint As Point = Me.PointToClient(dgvScreenPoint)

			Dim cellBottomLeftX As Integer = formPoint.X
			Dim cellBottomLeftY As Integer = formPoint.Y + cellRect.Height

			Dim x As Integer = cellBottomLeftX - Keypad1.Width
			Dim y As Integer = cellBottomLeftY - Keypad1.Height

			If x < 0 Then x = 0
			If y < 0 Then y = 0
			If x + Keypad1.Width > Me.ClientSize.Width Then x = Me.ClientSize.Width - Keypad1.Width
			If y + Keypad1.Height > Me.ClientSize.Height Then y = Me.ClientSize.Height - Keypad1.Height

			Keypad1.Location = New Point(x, y)
			Keypad1.Value = currentCell.Value?.ToString()
			Keypad1.Visible = True
			Keypad1.BringToFront()

			Dgv_HslProduction.CurrentCell = currentCell
			Dgv_HslProduction.BeginEdit(True)
		Else
			Keypad1.Visible = False
			currentCell = Nothing
		End If
	End Sub

	Private Sub Keypad1_ValueChanged_Handler(sender As Object, e As EventArgs)
		If currentCell IsNot Nothing Then
			If Not Dgv_HslProduction.IsCurrentCellInEditMode Then
				Dgv_HslProduction.CurrentCell = currentCell
				Dgv_HslProduction.BeginEdit(True)
			End If

			Dim tb As DataGridViewTextBoxEditingControl = TryCast(Dgv_HslProduction.EditingControl, DataGridViewTextBoxEditingControl)
			If tb IsNot Nothing Then
				tb.Text = Keypad1.Value
				tb.SelectionStart = tb.Text.Length
				tb.SelectionLength = 0
			Else
				Dim currentCell As DataGridViewCell = Dgv_HslProduction.CurrentCell
				If currentCell IsNot Nothing Then
					currentCell.Value = Keypad1.Value
				End If
			End If

		End If
	End Sub

	Private Sub Form_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
		Dim clickPoint As Point = Me.PointToClient(Cursor.Position)
		If Keypad1.Visible AndAlso Not Keypad1.Bounds.Contains(clickPoint) Then
			Keypad1.Visible = False

			If currentCell IsNot Nothing Then
				Dgv_HslProduction.EndEdit()
				Dgv_HslProduction.ClearSelection()
				Dgv_HslProduction.CurrentCell = Nothing
				currentCell = Nothing
			End If
		End If
	End Sub

	Private Sub Kosong()
		Try
			OpenConn()

			Dgv_HslProduction.Rows.Clear()
			Dgv_Hasil_Production_Packaging.Rows.Clear()

			get_no_faktur()

			Arr_Detail_Biaya.Clear()
			arrBarcodeScan.Clear()

			If asal = "CONTROLLING" Then

				'SQL = "select c.Kode_Stock_Owner,c.Kode_Barang,d.Nama,c.Jumlah,c.Persentase,e.Satuan from "
				'SQL = SQL & "Emi_Order_Produksi_Detail a,Emi_Transaksi_Formulator b,EMI_Transaksi_Formulator_Detail_Bahan c,Barang d,Barang_Detail_Satuan e "
				'SQL = SQL & "where a.No_Formula = b.No_Faktur and b.Status is null and b.No_Faktur = c.No_Faktur and a.Kode_Perusahaan = b.Kode_Perusahaan "
				'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
				'SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Barang = e.Kode_barang and e.Flag_Tampil_Display = 'Y' and "
				'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox4.Text & "' and a.Urut = '" & fUrut & "' "

				SQL = "select a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah ,b.Satuan, c.flag_potong_stok, isnull(c.standar_price,0) as standar_price from  "
				SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Bahan b, barang c "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
				SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & Txt_No_Split_PO.Text & "' "
				SQL = SQL & "order by c.nama"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								Dgv_HslProduction.Rows.Add()
								Dgv_HslProduction.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16)

								If Dgv_HslProduction.Columns("Column5") IsNot Nothing Then
									Dgv_HslProduction.Rows(i).Cells("Column5").Style.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
								End If

								'Dgv_HslProduction.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Nama")
								Dgv_HslProduction.Rows.Item(i).Cells(CellNamaBahan).Value = "X"
								Dgv_HslProduction.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
								Dgv_HslProduction.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")
								' Dgv_HslProduction.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
								' Dim nhasil As Double = 0
								' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
								Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N4")
								Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
								Dgv_HslProduction.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")

								If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
									Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = ""
								Else
									Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = .Rows(i).Item("flag_potong_stok")
								End If

								Dgv_HslProduction.Rows.Item(i).Cells(CellStandarPrice).Value = .Rows(i).Item("standar_price")

							Next
						End If
					End With
				End Using

				SQL = "select a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah, b.Satuan, c.flag_potong_stok,isnull(c.standar_price,0) as standar_price from  "
				SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
				SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & Txt_No_Split_PO.Text & "' "
				SQL = SQL & "order by c.nama"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								Dgv_Hasil_Production_Packaging.Rows.Add()
								Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
								Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")
								'Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
								' Dim nhasil As Double = 0
								' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
								Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N4")
								Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
								Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")

								If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
									Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellPotStokPckg).Value = ""
								Else
									Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellPotStokPckg).Value = .Rows(i).Item("flag_potong_stok")
								End If

								Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellStandarPricePckg).Value = .Rows(i).Item("standar_price")
							Next
						End If
					End With
				End Using

			ElseIf asal = "INDEPENDENT" Then
				If Dgv_HslProduction.Rows.Count = 0 Then
					Txt_No_Split_PO.Text = ""
					Txt_Nm_Barang.Text = ""
					Txt_Jam_Split.Text = ""
					Txt_Batch.Text = ""
					DateTimePicker1.Value = Date.Now
				End If

			ElseIf asal = "DISPLAY_RFID" Then

				If String.IsNullOrEmpty(RFID_SelectedSplit) Or String.IsNullOrEmpty(RFID_SelectedBatch) Then
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan No Split dan Batch Tidak Ada. Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Me.Close()
					Exit Sub
				End If

				'============================
				'=     LOAD DATA PARENT     =
				'============================
				SQL = "select a.Status, a.No_Transaksi, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama "
				SQL = SQL & "from Emi_Split_Production_Order a "
				SQL = SQL & "inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
				SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.No_Transaksi = '" & RFID_SelectedSplit & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						If General_Class.CekNULL(Dr("Status")) = "Y" Then
							Dr.Close()
							CloseConn()
							MessageBox.Show($"Terjadi Kesalahan No Split {RFID_SelectedSplit} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Me.Close()
							Exit Sub
						End If

						Txt_No_Split_PO.Text = Dr("No_Transaksi")
						Txt_Nm_Barang.Text = Dr("Nama")
						DateTimePicker1.Value = If(IsDate(Dr("Tanggal")), CDate(Dr("Tanggal")), Date.Now)
						Txt_Jam_Split.Text = Dr("Jam")
						Txt_Batch.Text = RFID_SelectedBatch.Trim
					Else
						Dr.Close()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan No Split {RFID_SelectedSplit} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Me.Close()
						Exit Sub
					End If
				End Using

				'===========================
				'=     LOAD DATA BAHAN     =
				'===========================
				SQL = $"
					;with DataTF as (
						select z.Kode_Perusahaan, x.Kode_Barang, sum(k.Jumlah) as Jumlah
						from Tf_Stock_Parent z
							inner join Tf_Stock x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
							inner join Tf_Stock_det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF
							inner join TF_Stock_Det2 k on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur and y.Urut_Oto = k.Urut_Det
						where z.Status is NULL
						and x.Urut_Material_Requisition_Convert in (
							select r.Urut_Oto
							from Emi_Material_Requisition q
								inner join Emi_Material_Requisition_Det w on q.Kode_Perusahaan = w.Kode_Perusahaan and q.No_Faktur = w.No_Faktur
								inner join Emi_Material_Requisition_Det_Convert r on w.Kode_Perusahaan = r.Kode_Perusahaan and w.No_Faktur = r.No_Faktur and w.Urut_Oto = r.No_Urut_Det
							where q.Status is NULL
							and q.Kode_Perusahaan = z.Kode_Perusahaan
							and q.No_Faktur_Order = '{RFID_SelectedSplit}'
							and q.Batch = '{RFID_SelectedBatch}'
						)
						group by z.Kode_Perusahaan, x.Kode_Barang

						union all

						select r.Kode_Perusahaan, r.Kode_Barang, sum(r.Jumlah) as Jumlah
						from N_EMI_Transaksi_Material_Requisition_QC z
							inner join N_EMI_Transaksi_Material_Requisition_QC_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
							inner join N_EMI_Transaksi_Material_Requisition_QC_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_Detail
							inner join N_EMI_Transaksi_Material_Requisition_QC_Validasi r on y.Kode_Perusahaan = r.Kode_Perusahaan and y.No_Faktur = r.No_Faktur_RM and y.Urut_Oto = r.Urut_Det_RM
						where z.Status is NULL and r.Status is NULL
						and z.No_Faktur_Order = '{RFID_SelectedSplit}'
						and x.Batch = '{RFID_SelectedBatch}'
						group by r.Kode_Perusahaan, r.Kode_Barang
					), Data_Transfer_Stock as (
						select Kode_Perusahaan, Kode_Barang, sum(Jumlah) as Jumlah
						from DataTF
						group by Kode_Perusahaan, Kode_Barang
					)
					select a.No_Transaksi as No_Split, b.Kode_Stock_Owner as Lokasi_Bahan, b.Kode_Barang as Kode_Bahan, c.Nama as Nama_Bahan,
						b.Jumlah as Jumlah_Formula, b.Satuan as Satuan_Bahan, c.flag_potong_stok, isnull(c.standar_price,0) as standar_price, c.Flag_Non_Barcode,

						isnull((
							select ((x.Jumlah) /
								(select dbo.Ubah_Satuan(z.Kode_Perusahaan, 'masa', x.Kode_Barang, r.Satuan_Hasil, x.satuan, r.Hasil) from Emi_Transaksi_Formulator r
								where r.Kode_Perusahaan = x.Kode_Perusahaan And r.No_Faktur = x.No_Faktur)
							) * a.Qty_Batch
							from EMI_Order_Produksi z, EMI_Transaksi_Formulator_Detail_Bahan x
							where z.Kode_Perusahaan = x.Kode_Perusahaan
								and z.Kode_Formula = x.No_Faktur
								and z.Status is null
								and a.Kode_Perusahaan = z.Kode_Perusahaan
								and a.No_PO = z.No_Faktur
								and x.Kode_Barang = b.Kode_Barang
						), 0) as JumlahKebutuhan,

						d.Jumlah as Jumlah_Transfer
					from Emi_Split_Production_Order a
						inner join Emi_Split_Production_Order_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur
						inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
						left join Data_Transfer_Stock d on b.Kode_Perusahaan = d.kode_perusahaan and b.Kode_Barang = d.Kode_Barang
					where a.Status is NULL
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.No_Transaksi = '{RFID_SelectedSplit}'
				"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1

								Dgv_HslProduction.Rows.Add()
								Dgv_HslProduction.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 12)

								If Dgv_HslProduction.Columns("Column5") IsNot Nothing Then
									Dgv_HslProduction.Rows(i).Cells("Column5").Style.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
								End If

								'Dgv_HslProduction.Rows.Item(i).Cells(CellNamaBahan).Value = .Rows(i).Item("Nama_Bahan")
								Dgv_HslProduction.Rows.Item(i).Cells(CellNamaBahan).Value = "X"
								Dgv_HslProduction.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Lokasi_Bahan")
								Dgv_HslProduction.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Bahan")

								Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("Jumlah_Formula"), "N4")
								Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Value = Format(.Rows(i).Item("Jumlah_Transfer"), "N4")
								Dgv_HslProduction.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan_Bahan")

								If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
									Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = ""
								Else
									Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = .Rows(i).Item("flag_potong_stok")
								End If

								Dgv_HslProduction.Rows.Item(i).Cells(CellStandarPrice).Value = .Rows(i).Item("standar_price")

								If General_Class.CekNULL(.Rows(i).Item("Flag_Non_Barcode")) = "Y" Then
									Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).ReadOnly = False
									Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Style.BackColor = Color.LightGray
								Else
									Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).ReadOnly = True
									Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Style.BackColor = Color.White
								End If

								'Dgv_HslProduction.Rows.Item(i).Cells(CellJumlahInput).Value = .Rows(i).Item("Jumlah_Transfer")
								Dgv_HslProduction.Rows.Item(i).Cells(CellJumlahInput).Value = 0
								Dgv_HslProduction.Rows.Item(i).Cells(CellJumlahKebutuhan).Value = Format(.Rows(i).Item("JumlahKebutuhan"), "N4")

								Dgv_HslProduction.Rows.Item(i).Cells(CellStatus).Value = "Belum Terpenuhi"
								Dgv_HslProduction.Rows(i).DefaultCellStyle.BackColor = Color.White

								'If General_Class.CekNULL(.Rows(i).Item("Status")) = "Y" Then
								'	Dgv_HslProduction.Rows.Item(i).Cells(CellStatus).Value = "Terpenuhi"
								'	Dgv_HslProduction.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
								'Else
								'	Dgv_HslProduction.Rows.Item(i).Cells(CellStatus).Value = "Belum Terpenuhi"
								'	Dgv_HslProduction.Rows(i).DefaultCellStyle.BackColor = Color.White
								'End If

							Next
						End If
					End With
				End Using

				'===============================
				'=     INSERT DATA BARCODE     =
				'===============================
				arrBarcodeScan.Clear()
				SQL = $"
					select x.Kode_Barang, k.Serial_Number
					from Tf_Stock_Parent z
						inner join Tf_Stock x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
						inner join Tf_Stock_det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF
						inner join TF_Stock_Det2 k on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur and y.Urut_Oto = k.Urut_Det
					where z.Status is NULL and z.kode_perusahaan = '{KodePerusahaan}'
					and x.Urut_Material_Requisition_Convert in (
						select r.Urut_Oto
						from Emi_Material_Requisition q
							inner join Emi_Material_Requisition_Det w on q.Kode_Perusahaan = w.Kode_Perusahaan and q.No_Faktur = w.No_Faktur
							inner join Emi_Material_Requisition_Det_Convert r on w.Kode_Perusahaan = r.Kode_Perusahaan and w.No_Faktur = r.No_Faktur and w.Urut_Oto = r.No_Urut_Det
						where q.Status is NULL
						and q.Kode_Perusahaan = z.Kode_Perusahaan
						and q.No_Faktur_Order = '{RFID_SelectedSplit}'
						and q.Batch = '{RFID_SelectedBatch}'
					)
					group by z.Kode_Perusahaan, x.Kode_Barang, k.Serial_Number

					union all

					select r.Kode_Barang, r.SN_Baru as Serial_Number
					from N_EMI_Transaksi_Material_Requisition_QC z
						inner join N_EMI_Transaksi_Material_Requisition_QC_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
						inner join N_EMI_Transaksi_Material_Requisition_QC_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_Detail
						inner join N_EMI_Transaksi_Material_Requisition_QC_Validasi r on y.Kode_Perusahaan = r.Kode_Perusahaan and y.No_Faktur = r.No_Faktur_RM and y.Urut_Oto = r.Urut_Det_RM
					where z.Status is NULL and r.Status is NULL
					and z.kode_perusahaan = '{KodePerusahaan}'
					and z.No_Faktur_Order = '{RFID_SelectedSplit}'
					and x.Batch = '{RFID_SelectedBatch}'
					group by r.Kode_Perusahaan, r.Kode_Barang, r.SN_Baru
				"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							'arrBarcodeScan.RemoveAll(Function(x) x.Kd_Barang = .Rows(0).Item("Kode_Barang").ToString.Trim)
							For i As Integer = 0 To .Rows.Count - 1
								arrBarcodeScan.Add((Kd_barang:= .Rows(i).Item("Kode_Barang").ToString.Trim, Serial_Number:= .Rows(i).Item("Serial_Number").ToString.Trim))

							Next
						Else
							CloseConn()
							MessageBox.Show($"Terjadi Kesalaham, Detail SN Bahan Baku Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End With
				End Using

			End If

			Txt_QR.Text = ""

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If String.IsNullOrEmpty(Txt_No_Split_PO.Text) Then
			Return
		End If

		'Dim SD As New SD_Pengeluaran_Bahan_Baku_PIN With {
		'    .StartPosition = FormStartPosition.CenterScreen
		'}

		'Dim result As DialogResult = SD.ShowDialog()

		'If result <> DialogResult.OK Then
		'    Return
		'End If

		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtFormulator_NoFaktur.Focus() : Exit Sub
			'ElseIf TextBox5.Text.Trim.Length = 0 Then
			'    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox5.Focus() : Exit Sub
			'ElseIf TextBox8.Text.Trim.Length = 0 Then
			'    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty2, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox8.Focus() : Exit Sub
			'ElseIf TextBox7.Text.Trim.Length = 0 Then
			'    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty3, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox7.Focus() : Exit Sub
			'ElseIf Dgv_HslProduction.CurrentRow.Cells(CellNilai_Produksi).Value = "0" Then
			'    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty4, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    Exit Sub
		End If
		If Dgv_HslProduction.Rows.Count = 0 Then
			MessageBox.Show("Tidak Ada Data yang bisa Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtFormulator_NoFaktur.Focus() : Exit Sub
		End If

		get_jam()

		Dim IsBatchComplete As Boolean = False
		Dim IsAutomaticValidation As Boolean = True

		Try
			OpenConn()

			'   get_no_faktur()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Kd_So As String = ""
			Dim Kd_Brg As String = ""
			SQL = "Select b.Status, b.Selesai, b.Kode_Stock_Owner, b.Kode_Barang "
			SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b "
			SQL = SQL & "where a.No_PO = b.No_Faktur "
			SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Transaksi = '" & Txt_No_Split_PO.Text & "'"
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					Kd_So = dr("Kode_Stock_Owner")
					Kd_Brg = dr("Kode_Barang")
					If General_Class.CekNULL(dr("Status")) <> "" Then
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show(Base_Language.Lang_Global_NoFaktur & " " & Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End If
			End Using

			'Cek apakah sudah ada data di DGV
			Dim ada_data As Boolean = False
			For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
				Get_Isi_Listview(a)
				If Val(HilangkanTanda(LvNilai_Produksi)) <> 0 Then
					ada_data = True
				End If
			Next

			If ada_data = False Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Tidak ada Data yang di input ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim proses As Integer
			SQL = "select no_transaksi, "
			SQL = SQL & "isnull((select top(1) proses from ( "
			SQL = SQL & "Select proses from Emi_Production_Results_Detail x where "
			SQL = SQL & "a.Kode_Perusahaan = x.Kode_Perusahaan And a.No_Transaksi = x.No_Transaksi "
			SQL = SQL & "union all "
			SQL = SQL & "Select proses from Emi_Production_Results_Packaging_Detail x where "
			SQL = SQL & "a.Kode_Perusahaan = x.Kode_Perusahaan And a.No_Transaksi = x.No_Transaksi "
			SQL = SQL & ") as Data order by proses desc ),0) as proses "
			SQL = SQL & "from Emi_Production_Results a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Order = '" & Txt_No_Split_PO.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					TxtFormulator_NoFaktur.Text = Dr("no_transaksi")
					'proses = Dr("proses") + 1
				Else
					Dr.Close()

					get_no_faktur()

					SQL = "INSERT INTO Emi_Production_Results(Kode_Perusahaan,No_Transaksi,No_Production_Order,Tanggal,Jam,UserID"
					SQL = SQL & ") VALUES('" & KodePerusahaan & "',"
					SQL = SQL & "'" & TxtFormulator_NoFaktur.Text & "','" & Txt_No_Split_PO.Text.Trim & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
					SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "')"
					ExecuteTrans(SQL)

					'proses = 1
				End If
			End Using

			Dim Nilai_Bahan As Double = 0
			Dim Nilai_Packaging As Double = 0
			Dim Nilai_loss_production As Double = 0
			Dim arr_biaya_Produksi, arrID_Work_Center, arrJenis_Biaya As New ArrayList
			Dim Hpp_Work_Center_total As Double = 0

			Arr_Detail_Biaya.Clear()
			'Insert Bahan Sesuai yg di input

#Region "Insert Bahan"

			For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
				Get_Isi_Listview(a)

				'If Val(HilangkanTanda(LvNilai_Produksi)) > 0 Then

				'======                              =========='
				'======   Awal convert satuan barang =========='
				'=========                           =========='

				If LvStatus = "Terpenuhi" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Kode Barang " & LvKode_Bahan & " Sudah Terpenuhi . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				Dim convertKeSatuanAsli_bhn As String = ""
				Dim jumlahConvertBhn As Double = 0

				SQL = "select satuan From barang where Kode_barang = '" & LvKode_Bahan & "' "
				SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & LvKode_So & "' "
				Using Dr3 = OpenTrans(SQL)
					If Dr3.Read Then

						convertKeSatuanAsli_bhn = Dr3("satuan")
						SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvKode_Bahan & "',"
						SQL = SQL & "'" & LvSatuan & "','" & Dr3("satuan") & "',"
						SQL = SQL & HilangkanTanda(LvNilai_Produksi) & ") as Hasil "
						Dr3.Close()

						Using dr4 = OpenTrans(SQL)
							If dr4.Read Then
								If General_Class.CekNULL(dr4("Hasil")) <> "" Then
									'If dr4("Hasil") = 0 Then
									'    dr4.Close()
									'    CloseTrans()
									'    CloseConn()
									'    MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									'    Exit Sub
									'Else
									jumlahConvertBhn = Val(HilangkanTanda(Format(dr4("hasil"), "N4")))

									'End If
								Else
									dr4.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End If
						End Using
					Else
						Dr3.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=========================
				'=    CEK URUT PROSES    =
				'=========================
				Dim Urut_Proses As Integer = 0
				Dim Jumlah_Dosing As Double = 0
				Dim Selesai_Dosing As String = ""
				SQL = "select top 1 a.No_Transaksi, a.No_Production_Order, b.Proses, isnull(b.Selesai,'T') as Selesai, b.urut, b.Nilai_Produksi "
				SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
				SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
				SQL = SQL & "and b.Kode_Stock_Owner = '" & LvKode_So & "' "
				SQL = SQL & "and b.Kode_Barang = '" & LvKode_Bahan & "' "
				SQL = SQL & "order by b.proses desc "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						If Dr("Selesai") = "Y" Then
							proses = Val(HilangkanTanda(Dr("Proses"))) + 1
						Else
							proses = Val(HilangkanTanda(Dr("Proses")))
							Urut_Proses = Dr("urut")
							Jumlah_Dosing = Val(HilangkanTanda(Format(Dr("Nilai_Produksi"), "N4")))
						End If

						Selesai_Dosing = Dr("Selesai")
					Else
						Dr.Close()

						proses = 1

						Selesai_Dosing = "Y"

					End If
				End Using

				SQL = "Select Kode_Perusahaan from "
				SQL = SQL & "Emi_Production_Results_HPP a where "
				SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
				SQL = SQL & "No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and "
				SQL = SQL & "proses='" & proses & "' "
				Using dr = OpenTrans(SQL)
					If Not dr.Read Then
						dr.Close()

						SQL = "insert into Emi_Production_Results_HPP "
						SQL = SQL & "(Kode_Perusahaan, No_Transaksi, Proses) "
						SQL = SQL & "Values('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "')"
						ExecuteTrans(SQL)
					End If
				End Using

				'======                              =========='
				'======   Akhir convert satuan barang =========='
				'=========                           =========='

				Dim Toleransi_timbang_Min As Double = 0
				Dim Toleransi_timbang_Max As Double = 0

				Dim Flag_NonBarcode As String = ""
				SQL = "Select top(1) Toleransi_Timbang_Min, Toleransi_Timbang_Max, Flag_Non_Barcode from "
				SQL = SQL & "barang a where "
				SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
				SQL = SQL & "Kode_barang='" & LvKode_Bahan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						Flag_NonBarcode = General_Class.CekNULL(dr("Flag_Non_Barcode"))
						Toleransi_timbang_Min = dr("Toleransi_Timbang_Min")
						Toleransi_timbang_Max = dr("Toleransi_Timbang_Max")
					Else
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Kode barang tidak ditemukan . . ! !")
						Exit Sub
					End If
				End Using

				Dim Nilai_Formula As Double = 0
				SQL = "Select c.Kode_Barang, "
				SQL = SQL & "isnull(( "

				SQL = SQL & "round( "

				SQL = SQL & "(c.Jumlah / (select dbo.Ubah_Satuan(a.Kode_Perusahaan, 'masa', c.Kode_Barang, z.Satuan_Hasil, c.satuan, z.Hasil) from Emi_Transaksi_Formulator z "
				SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur) "
				SQL = SQL & ") "

				SQL = SQL & "* "

				SQL = SQL & "(ISNULL((a.Qty_Batch), 0)),4)), 0) as Nilai_Formula "
				SQL = SQL & "From Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
				SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan "
				SQL = SQL & "And a.No_PO = b.No_Faktur "
				SQL = SQL & "And b.Kode_Formula = c.No_Faktur "
				SQL = SQL & "And a.No_Transaksi = '" & Txt_No_Split_PO.Text & "' "
				SQL = SQL & "And c.Kode_Barang = '" & LvKode_Bahan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						Nilai_Formula = Val(HilangkanTanda(Format(dr("Nilai_Formula"), "N4")))
					Else
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Nilai Formula Tidak di temukan . . ! !")
						Exit Sub
					End If
				End Using

				Dim Proses_selesai As String = "NULL"
				If (Jumlah_Dosing + Val(HilangkanTanda(LvNilai_Produksi))) > Nilai_Formula + ((Toleransi_timbang_Max / 100) * Nilai_Formula) Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Nilai pada barang " & LvKode_Bahan & " melebihi Toleransi . . ! !")
					Exit Sub
				Else
					If (Jumlah_Dosing + Val(HilangkanTanda(LvNilai_Produksi))) > Nilai_Formula - ((Toleransi_timbang_Min / 100) * Nilai_Formula) Then
						Proses_selesai = "'Y'"
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Nilai pada barang " & LvKode_Bahan & " Tidak Memenuhi Toleransi . . ! !")
						Exit Sub
					End If
				End If

				If Selesai_Dosing = "Y" Then
					SQL = "INSERT INTO Emi_Production_Results_Detail(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan,proses,"
					SQL = SQL & "nilai_barang,satuan_barang,userid,tanggal,jam, Selesai ) "
					SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & LvKode_So & "','" & LvKode_Bahan & "',"
					SQL = SQL & "'" & Nilai_Formula & "','" & HilangkanTanda(LvNilai_Produksi) & "','" & LvSatuan & "' , '" & proses & "', "
					SQL = SQL & "'" & jumlahConvertBhn & "', '" & convertKeSatuanAsli_bhn & "', '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
					SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', " & Proses_selesai & " "
					SQL = SQL & ")"
					ExecuteTrans(SQL)
				Else
					SQL = "update Emi_Production_Results_Detail set Nilai_Produksi+=" & HilangkanTanda(LvNilai_Produksi) & ", nilai_barang+=" & HilangkanTanda(LvNilai_Produksi) & ", Selesai=" & Proses_selesai & " "
					SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Urut= '" & Urut_Proses & "' "
					ExecuteTrans(SQL)
				End If

				Dim x_ident_currentBahan As Integer = 0
				SQL = "select IDENT_CURRENT('Emi_Production_Results_Detail') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_ident_currentBahan = Dr("urutan")
					End If
				End Using

#Region "Potong Stock Bahan"

				SQL = "select round(good_stock,4) as good_stock, flag_ppn from barang where "
				SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
				SQL = SQL & "kode_barang = '" & LvKode_Bahan & "'"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							'If LvPotStokBhn = "Y" Then
							If Val(HilangkanTanda(Format(.Rows(0).Item("good_stock"), "N4"))) - Val(jumlahConvertBhn) < BolehNegatif Then
								CloseTrans()
								CloseConn()
								MessageBox.Show("Proses membuat stock menjadi negatif untuk kode barang " & LvKode_Bahan & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							Else
								SQL = "Update barang set good_stock = good_stock - " & jumlahConvertBhn & " where "
								SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
								SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
								SQL = SQL & "kode_barang = '" & LvKode_Bahan & "'"
								ExecuteTrans(SQL)
							End If
							'End If
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
							Exit Sub
						End If
					End With
				End Using

				Dim lewatin As String = "T"
				SQL = "select isnull(round(sum(jumlah),4), 0) as stock from barang_sn where "
				SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
				SQL = SQL & "kode_barang = '" & LvKode_Bahan & "' and jumlah <> 0 "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						If Val(HilangkanTanda(Format(Dr("stock"), "N4"))) < Val(jumlahConvertBhn) Then
							lewatin = "Y"
						Else
							lewatin = "T"
						End If
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'==================================================================================
				'======================  CHECK APAKAH FLAG POTONG STOK NYA Y atau T ================
				'==================================================================================
				'If LvPotStokBhn = "Y" Then
				If lewatin = "T" Then
					Dim sisa As Double = 0
					Dim serialList As String = ""

					serialList = String.Join(", ", arrBarcodeScan.
						Where(Function(x) x.Kd_Barang = LvKode_Bahan).
						Select(Function(x) $"'{x.Serial_Number}'")
)
					SQL = "select kode_stock_owner, kode_barang, serial_number, dbo.get_hpp(Serial_Number) as HPP, round(jumlah,4) as jumlah from barang_sn where "
					SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
					SQL = SQL & "kode_barang = '" & LvKode_Bahan & "' and jumlah <> 0 "
					If Flag_NonBarcode <> "Y" Then
						SQL = SQL & "and serial_number in (" & serialList & ")"
					End If
					SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								sisa = Val(jumlahConvertBhn)
								For h As Integer = 0 To .Rows.Count - 1
									If sisa = 0 Then
										Exit For
									ElseIf sisa < 0 Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If

									Dim hpp As Double = .Rows(h).Item("HPP")
									Dim JumlahInsert As Double = 0

									'===========================
									'=     GET DETAIL AKUN     =
									'===========================
									Dim Kd_Akun_Biaya As String = ""
									Dim Ket_Group_Jenis As String = ""
									SQL = "select a.id_group_jenis, c.Akun_Persediaan, a.Kode_Group_Jenis "
									SQL = SQL & "from emi_group_jenis a, barang b, emi_group_jenis_akun c "
									SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
									SQL = SQL & "and a.id_group_jenis = b.id_group_jenis and b.kode_barang='" & .Rows(h).Item("kode_barang") & "' "
									SQL = SQL & "and a.id_group_jenis = c.id_group_jenis and b.kode_stock_owner='" & .Rows(h).Item("kode_stock_owner") & "' "
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											Kd_Akun_Biaya = Dr("Akun_Persediaan")
											Ket_Group_Jenis = Dr("Kode_Group_Jenis")
										Else
											Dr.Close()
											CloseTrans()
											CloseConn()
											MessageBox.Show("Barang detail jenis tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using

									If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
										SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
										SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
										SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
										SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
										SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
										ExecuteTrans(SQL)

										SQL = "INSERT INTO Emi_Production_Results_det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
										SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
										SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
										SQL = SQL & "" & sisa & ",'" & .Rows(h).Item("serial_number") & "', '" & x_ident_currentBahan & "')"
										ExecuteTrans(SQL)

										JumlahInsert = sisa

										Nilai_Bahan = Nilai_Bahan + (Math.Round(hpp * sisa, 0))
										sisa = 0
									ElseIf sisa > .Rows(h).Item("jumlah") Then
										SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
										SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
										SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
										SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
										SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
										ExecuteTrans(SQL)

										SQL = "INSERT INTO Emi_Production_Results_det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
										SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
										SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
										SQL = SQL & "" & .Rows(h).Item("jumlah") & ",'" & .Rows(h).Item("serial_number") & "', '" & x_ident_currentBahan & "')"
										ExecuteTrans(SQL)

										JumlahInsert = .Rows(h).Item("jumlah")

										Nilai_Bahan = Nilai_Bahan + (Math.Round(hpp * .Rows(h).Item("jumlah"), 0))
										sisa = sisa - .Rows(h).Item("jumlah")
									Else
										CloseTrans()
										CloseConn()
										MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If

									'TODO : CEK GROUP BY KODE BARANG, TAMBAH ARRAY DETAIL

									Dim kdBarang As String = .Rows(h).Item("kode_barang")
									Dim kdSO As String = .Rows(h).Item("kode_stock_owner")

									Dim existingItemIndex As Integer = Arr_Detail_Biaya.FindIndex(Function(x) x.akun = Kd_Akun_Biaya)

									If existingItemIndex >= 0 Then
										Dim currentItem = Arr_Detail_Biaya(existingItemIndex)

										Arr_Detail_Biaya(existingItemIndex) = (
												akun:=currentItem.akun,
												keterangan:=currentItem.keterangan,
												nilai:=currentItem.nilai + Math.Round((hpp * JumlahInsert), 0),
												kd_so:=currentItem.kd_so,
												kd_barang:=currentItem.kd_barang
											)
									Else
										Arr_Detail_Biaya.Add((
												akun:=Kd_Akun_Biaya,
												keterangan:=Ket_Group_Jenis,
												nilai:=Math.Round((hpp * JumlahInsert), 0),
												kd_so:=kdSO,
												kd_barang:=kdBarang
											))
									End If

									If Math.Round(sisa, 4) <> 0 And h = .Rows.Count - 1 Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If

								Next ' for barang sn
							End If 'count <> 0
						End With
					End Using
				Else
					CloseTrans()
					CloseConn()
					MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

#End Region

				Dim asdaasda As String = LvKode_So
				Dim asda As String = LvKode_Bahan

				Dim ToleransiFormulaMin As Double = 0
				Dim ToleransiFormulaMax As Double = 0
				'=====================================
				'=    GET NILAI TOLERANSI FORMULA    =
				'=====================================

#Region "Kode Lama"

				'SQL = "select Toleransi_Formula_GI_Min, Toleransi_Formula_GI_Max from init "
				'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
				'Using Dr = OpenTrans(SQL)
				'	If Dr.Read Then

				'		If General_Class.CekNULL(Dr("Toleransi_Formula_GI_Min")) = "" Then
				'			Dr.Close()
				'			CloseTrans()
				'			CloseConn()
				'			MessageBox.Show("Terjadi Kesalahan Pada Tabel Init, Nilai Toleransi Formula GI Min Belum Diset", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'			Exit Sub
				'		ElseIf General_Class.CekNULL(Dr("Toleransi_Formula_GI_Max")) = "" Then
				'			Dr.Close()
				'			CloseTrans()
				'			CloseConn()
				'			MessageBox.Show("Terjadi Kesalahan Pada Tabel Init, Nilai Toleransi Formula GI Max Belum Diset", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'			Exit Sub
				'		End If

				'		ToleransiFormulaMin = Math.Abs(Val(HilangkanTanda(Dr("Toleransi_Formula_GI_Min"))))
				'		ToleransiFormulaMax = Math.Abs(Val(HilangkanTanda(Dr("Toleransi_Formula_GI_Max"))))
				'	Else
				'		Dr.Close()
				'		CloseTrans()
				'		CloseConn()
				'		MessageBox.Show("Terjadi Kesalahan Pada Tabel Init", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'		Exit Sub
				'	End If
				'End Using

#End Region

				SQL = "select b.Toleransi_Formula_GI_Min, b.Toleransi_Formula_GI_Max "
				SQL &= $"from barang a "
				SQL &= $"inner join N_EMI_Master_Klasifikasi_Bahan_3 b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Klasifikasi_Bahan3 = b.Id_Klasifikasi_Bahan3 "
				SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and a.Kode_Stock_Owner = '{LvKode_So.Trim}' "
				SQL &= $"and a.Kode_Barang = '{LvKode_Bahan.Trim}' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						If General_Class.CekNULL(Dr("Toleransi_Formula_GI_Min")) = "" Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Terjadi Kesalahan, Nilai Toleransi Formula GI Min Belum Diset", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						ElseIf General_Class.CekNULL(Dr("Toleransi_Formula_GI_Max")) = "" Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Terjadi Kesalahan , Nilai Toleransi Formula GI Max Belum Diset", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If

						ToleransiFormulaMin = Math.Abs(Val(HilangkanTanda(Dr("Toleransi_Formula_GI_Min"))))
						ToleransiFormulaMax = Math.Abs(Val(HilangkanTanda(Dr("Toleransi_Formula_GI_Max"))))
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terjadi Kesalahan, data Klasifikasi Bahan 3 untuk bahan ini tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================================
				'=     CEK RANGE TOLERANSI FORMULA     =
				'=======================================
				If IsAutomaticValidation Then

					Dim Batas_Bawah_Toleransi_Formula As Double = Nilai_Formula - ((ToleransiFormulaMin / 100) * Nilai_Formula)
					Dim Batas_Atas_Toleransi_Formula As Double = Nilai_Formula + ((ToleransiFormulaMax / 100) * Nilai_Formula)

					Dim NilaiInput As Double = Jumlah_Dosing + Val(HilangkanTanda(LvNilai_Produksi))

					If NilaiInput < Batas_Bawah_Toleransi_Formula OrElse NilaiInput > Batas_Atas_Toleransi_Formula Then
						IsAutomaticValidation = False
					End If

				End If

			Next

#End Region

			' Cek Apakah  ada Proses yg sudah selesai
			SQL = "Select Proses from "
			SQL = SQL & "Emi_Production_Results_HPP a where "
			SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
			SQL = SQL & "a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and "
			SQL = SQL & "a.tanggal is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For index = 0 To .Rows.Count - 1

							Dim proses_temp As Integer = .Rows(index).Item("Proses")
							Dim selesai As Boolean = True

							'cek apakah bahan per formula sudah selesai
							SQL = "Select b.Kode_Barang, "
							SQL = SQL & "isnull((select 'Y' from Emi_Production_Results x, Emi_Production_Results_Detail y "
							SQL = SQL & "where x.kode_Perusahaan = y.kode_perusahaan And x.No_Transaksi = y.No_Transaksi And x.status Is null "
							SQL = SQL & "And x.Kode_Perusahaan = a.Kode_Perusahaan And x.No_Production_Order = a.No_Transaksi And "
							SQL = SQL & "y.Kode_Barang = b.Kode_Barang And y.Proses = '" & proses_temp & "' and y.status is null and y.selesai='Y'),'T') as Terpenuhi "
							SQL = SQL & "From Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Bahan b Where "
							SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Faktur And a.status Is null "
							SQL = SQL & " And a.no_transaksi ='" & Txt_No_Split_PO.Text & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
							Using dr = OpenTrans(SQL)
								Do While dr.Read
									If dr("Terpenuhi") = "T" Then
										selesai = False
									End If
								Loop
							End Using

							'kalo sudah insert, insert packaging dan tentukan hpp
							If selesai = True Then
								Dim Jumlah_Dosing As Double = 0
								Dim Jumlah_Dosing_Pcs As Double = 0
								Dim satuan_dosing As String = ""

								SQL = "Select sum(nilai_Produksi) As Total, b.satuan from "
								SQL = SQL & "Emi_Production_Results a, Emi_Production_Results_Detail b where "
								SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
								SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' "
								SQL = SQL & "and a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' "
								SQL = SQL & "and b.proses='" & proses_temp & "' "
								SQL = SQL & "and b.status is null "
								SQL = SQL & "group by b.satuan "
								Using dr = OpenTrans(SQL)
									If dr.Read Then
										Jumlah_Dosing = Val(HilangkanTanda(Format(dr("Total"), "N4")))
										satuan_dosing = dr("satuan")
									End If
								End Using

								Dim Kd_barang As String = ""
								Dim Kd_barang_inq As String = ""
								Dim satuan_barang As String = ""

								Dim No_Production_Order As String = ""
								SQL = "Select a.Kode_Barang, b.kode_barang_inq, b.satuan, a.No_PO "
								SQL = SQL & "From Emi_Split_Production_Order a, barang b Where "
								SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And "
								SQL = SQL & "a.Kode_Barang = b.Kode_barang And "
								SQL = SQL & "a.kode_stock_owner=b.kode_stock_owner "
								SQL = SQL & "and a.no_transaksi ='" & Txt_No_Split_PO.Text & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
								Using dr = OpenTrans(SQL)
									If dr.Read Then
										Kd_barang = dr("Kode_Barang")
										Kd_barang_inq = dr("kode_barang_inq")
										satuan_barang = dr("satuan")
										No_Production_Order = dr("No_PO")
									End If
								End Using

								Dim ID_Routing As String = ""
								SQL = "Select Id_Routing "
								SQL = SQL & "From EMI_Order_Produksi a Where "
								SQL = SQL & "a.no_faktur ='" & No_Production_Order & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
								Using dr = OpenTrans(SQL)
									If dr.Read Then
										ID_Routing = dr("Id_Routing")
									End If
								End Using

								SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kd_barang & "',"
								SQL = SQL & "'" & satuan_dosing & "','" & satuan_barang & "',"
								SQL = SQL & "" & Jumlah_Dosing & ") as Hasil "

								Using dr4 = OpenTrans(SQL)
									If dr4.Read Then
										If General_Class.CekNULL(dr4("Hasil")) <> "" Then

											Jumlah_Dosing_Pcs = Math.Floor(dr4("hasil"))
										Else
											dr4.Close()
											CloseTrans()
											CloseConn()

											MessageBox.Show("Satuan " & satuan_dosing & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End If
								End Using

								Dim satuan_bahan As String = ""

								Dim Hpp_Packaging_Total As Double = 0

								Dim Hpp_Bahan_baku_Total As Double = 0

								Dim Persen_loss_production As Double = 0

								SQL = "Select isnull(sum(nilai * dbo.get_hpp(c.Serial_Number)),0) As Total, b.satuan_barang  "
								SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b, Emi_Production_Results_det c where "
								SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null And "
								SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Transaksi = c.No_Transaksi And b.Urut = c.No_Urut_detail "
								SQL = SQL & "and b.status is null "
								SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and b.proses='" & proses_temp & "' "
								SQL = SQL & "group by satuan_barang "
								Using dr = OpenTrans(SQL)
									If dr.Read Then
										satuan_bahan = dr("satuan_barang")
										Hpp_Bahan_baku_Total = Val(HilangkanTanda(Format(dr("Total"), "N0")))
									End If
								End Using

								SQL = "select Nilai_Persen from "
								SQL = SQL & "Emi_Budgeting_Loss_Production where "
								SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' "
								SQL = SQL & "order by Urut desc "
								Using dr = OpenTrans(SQL)
									If dr.Read Then
										Persen_loss_production = dr("Nilai_Persen")

									End If
								End Using

								'Nilai_loss_production = Math.Round(Hpp_Bahan_baku_Total * Persen_loss_production / 100, 0)

								'SQL = "Select isnull(sum(nilai * dbo.get_hpp(c.Serial_Number)),0) As Total from "
								'SQL = SQL & "Emi_Production_Results a, Emi_Production_Results_packaging_detail b, Emi_Production_Results_packaging_det c where "
								'SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null And "
								'SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Transaksi = c.No_Transaksi And b.Urut = c.No_Urut_detail "
								'SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and b.proses='" & proses_temp & "' "
								'Using dr = OpenTrans(SQL)
								'    If dr.Read Then
								'        Hpp_Packaging_Total = Val(HilangkanTanda(Format(dr("Total"), "N4")))
								'    End If
								'End Using

								' Hpp_Packaging_Pcs = Math.Round(Hpp_Packaging_Total / Jumlah_Dosing_Pcs, 2)

								''Dim bulan As String = Format(tgl_skg, "MM")
								''Dim tahun As String = Format(tgl_skg, "yyyy")

								''SQL = ";with cte as ( "
								''SQL = SQL & "Select a.kode_perusahaan, a.Id_Jenis_Biaya_Produksi, a.Kode_Jenis_Biaya_Produksi, "
								''SQL = SQL & "isnull((select top(1) no_faktur from Emi_Transaksi_Work_Center x where x.status Is null "
								''SQL = SQL & "And x.Kode_Perusahaan=a.Kode_Perusahaan And x.jenis_biaya=a.Kode_Jenis_Biaya_Produksi order by id desc),NULL) as Faktur_WC "
								''SQL = SQL & "From Emi_Jenis_Biaya_Produksi a "
								''SQL = SQL & ")select a.kode_jenis_biaya_produksi, c.id_work_center, max(c.Nilai_Per_pcs) as Nilai_Per_pcs "
								''SQL = SQL & "From cte a, Emi_Transaksi_Work_Center b, Emi_Transaksi_Work_Center_detail c Where "
								''SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.faktur_WC = b.No_Faktur And "
								''SQL = SQL & "b.kode_perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur And c.Id_Routing = '" & ID_Routing & "' "
								''SQL = SQL & "group by a.kode_jenis_biaya_produksi, c.id_work_center "
								''Using Dss = BindingTrans(SQL)
								''    If Dss.Tables("MyTable").Rows.Count <> 0 Then

								''        For indxx = 0 To Dss.Tables("MyTable").Rows.Count - 1

								''            Dim id_WC As String = Dss.Tables("MyTable").Rows(indxx).Item("id_work_center")
								''            Dim Jenis_biaya As String = Dss.Tables("MyTable").Rows(indxx).Item("kode_jenis_biaya_produksi")
								''            Dim Nilai_WC As Double = Dss.Tables("MyTable").Rows(indxx).Item("Nilai_Per_pcs")

								''            If Nilai_WC <> 0 Then
								''                arrID_Work_Center.Add(id_WC)
								''                arrJenis_Biaya.Add(Jenis_biaya)
								''                arr_biaya_Produksi.Add(Math.Round(Nilai_WC * Jumlah_Dosing))
								''                Hpp_Work_Center_total += Math.Round(Nilai_WC * Jumlah_Dosing)

								''                SQL = "insert into Emi_Production_Results_HPP_Detail_Work_Center ("
								''                SQL = SQL & "kode_Perusahaan, No_Transaksi, proses, ID_Work_Center, Kode_Jenis_Biaya, Nilai) values( "
								''                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses_temp & "', "
								''                SQL = SQL & "'" & id_WC & "', '" & Jenis_biaya & "', '" & Nilai_WC & "') "
								''                ExecuteTrans(SQL)
								''            End If

								''        Next

								''    Else
								''        CloseTrans()
								''        CloseConn()
								''        MessageBox.Show("Biaya Belum di tambahkan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								''        Exit Sub
								''    End If

								''End Using

								SQL = "Update Emi_Production_Results_HPP set "
								SQL = SQL & "tanggal ='" & Format(tgl_skg, "yyyy-MM-dd") & "', jam='" & Format(tgl_skg, "HH:mm:ss") & "', "
								SQL = SQL & "Jumlah_Formula='" & 1 & "', jumlah_dosing='" & Jumlah_Dosing & "', "
								SQL = SQL & "satuan='" & satuan_dosing & "', Jumlah_Dosing_Pcs='" & Jumlah_Dosing_Pcs & "', "
								SQL = SQL & "Total_Bahan_Baku='" & Hpp_Bahan_baku_Total & "', Total_Packaging='" & Hpp_Packaging_Total & "', "
								SQL = SQL & "Total_Biaya_Produksi='" & Hpp_Work_Center_total & "', Nilai_Loss_Production='" & Nilai_loss_production & "', "
								SQL = SQL & "Persen_Loss_Production='" & Persen_loss_production & "', jumlah_terpakai=0 "
								SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and "
								SQL = SQL & "No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and Proses='" & proses_temp & "' "
								ExecuteTrans(SQL)

							End If
						Next

					End If
				End With
			End Using

			'===================================================
			'=     CEEK APAKAH SEMUA BATCH SUDAH TERPENUHI     =
			'===================================================

#Region "Cek Semua Batch Sudah Terpenuhi"

			Dim jumlah_batch As Integer = 0
			Dim jumlah_batch_selesai As Integer = 0
			SQL = "select jumlah_batch from Emi_Split_Production_Order a "
			SQL = SQL & "where a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & Txt_No_Split_PO.Text.Trim & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					jumlah_batch = dr("jumlah_batch")
				End If
			End Using

			SQL = "Select count(b.Kode_Perusahaan) as Jumlah_selesai from Emi_Production_Results a, Emi_Production_Results_HPP b  "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
			SQL = SQL & "And a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Production_Order='" & Txt_No_Split_PO.Text.Trim & "' and b.Tanggal is not null "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					jumlah_batch_selesai = dr("Jumlah_selesai")
				End If
			End Using

			If jumlah_batch_selesai <> jumlah_batch Then
				IsBatchComplete = False
			Else
				IsBatchComplete = True
			End If

#End Region

			'=================================
			'=     VALIDASI GI OTOMASTIS     =
			'=================================
			If IsBatchComplete Then
				If IsAutomaticValidation Then

					'=======================================
					'=     CEK APAKAH PO SUDAH SELESAI     =
					'=======================================
					SQL = "select Flag_Hasil_Produksi_GI from Emi_Split_Production_Order "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Txt_No_Split_PO.Text.Trim & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							If General_Class.CekNULL(Dr("Flag_Hasil_Produksi_GI")) = "Y" Then
								Dr.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("GI Sudah Selesai, Tidak Bisa Diselesaikan Lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						Else
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using

					SQL = "update Emi_Split_Production_Order set Flag_Hasil_Produksi_GI = 'Y', UserID_Selesai_GI = 'SYNC', "
					SQL = SQL & "Tgl_Hasil_Produksi_GI = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Hasil_Produksi_GI = '" & Format(tgl_skg, "HH:mm:ss") & "'  "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Txt_No_Split_PO.Text.Trim & "' "
					ExecuteTrans(SQL)

				End If
			End If

			'awal stenly jurnal

#Region "JURNAL"

			Dim inisial_faktur_dari As String = ""

			SQL = "Select b.Inisial_Faktur,a.Kode_Stock_Owner from Emi_Split_Production_Order a,Stock_Owner_Gudang b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL = SQL & "And a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Txt_No_Split_PO.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					inisial_faktur_dari = Dr("inisial_faktur")
					fso = Dr("Kode_Stock_Owner")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Dim akun_Loss_production As String = ""
			Dim akun_persedian_brg_dlm_proses As String = ""

			Dim ket_loss_production As String = ""
			Dim ket_persedian_brg_dlm_proses As String = ""

			'awal persediaan barang dalam proses
			SQL = "select Persediaan_Barang_Dalam_Proses, Penyusutan_Barang_Dalam_Proses from stock_owner_gudang "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					akun_persedian_brg_dlm_proses = Dr("Persediaan_Barang_Dalam_Proses")
					ket_persedian_brg_dlm_proses = "Persediaan Barang Dalam Proses "

					akun_Loss_production = Dr("Penyusutan_Barang_Dalam_Proses")
					ket_loss_production = "Penyusutan Barang Dalam Proses "
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Dim Kode_voucher As String = ""
			Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
			Dim pagenumber As Integer = 1

			SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
			SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
			SQL = SQL & "'" & Kode_voucher & "', "
			SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
			SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
			SQL = SQL & "'" & KodeProyek & "', 'Pengeluaran Bahan Baku " & TxtFormulator_NoFaktur.Text & "', '', "
			SQL = SQL & "'-', '" & UserID & "')"
			ExecuteTrans(SQL)

			Dim ftotal_barang_Dalam_Proses As Double = 0

			Dim ket_packaging As String = ""
			Dim akun_kredit_packaging As String = ""
			Dim lok_packaging As String = ""

			Nilai_Bahan = Math.Round(Nilai_Bahan, 0)
			ftotal_barang_Dalam_Proses = Nilai_Bahan + Nilai_Packaging + Nilai_loss_production + Hpp_Work_Center_total

			If ftotal_barang_Dalam_Proses = 0 Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("tidak ada data yang di jurnal...!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persedian_brg_dlm_proses, 1),
					 Strings.Mid(akun_persedian_brg_dlm_proses, 2, 1),
					 Strings.Mid(Ganti(akun_persedian_brg_dlm_proses), 3),
					 KodePerusahaan, KodeProyek, ket_persedian_brg_dlm_proses & TxtFormulator_NoFaktur.Text, ftotal_barang_Dalam_Proses, "0", pagenumber, fso, Bahasa_Pilihan, Ket_Cost_Center_HO)
			ExecuteTrans(SQL)
			pagenumber = pagenumber + 1

			Dim Temp_Nilai_Bahan As Double = 0
			If Nilai_Bahan <> 0 Then

				Dim ket_material As String = ""
				Dim akun_kredit_material As String = ""
				Dim lok_material As String = ""

				SQL = "select top(1) "
				SQL = SQL & "b.Id_Group_Jenis, b.kode_stock_owner, c.akun_persediaan, Kode_Group_Jenis "
				SQL = SQL & "from Emi_Production_Results_det a, Barang b, EMI_Group_Jenis_Akun c, "
				SQL = SQL & "Emi_Production_Results_Detail e, EMI_Group_Jenis f  "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
				SQL = SQL & "and b.Kode_Perusahaan = f.Kode_Perusahaan and b.Id_Group_Jenis = f.Id_Group_Jenis "
				SQL = SQL & "and f.Kode_Perusahaan = c.Kode_Perusahaan and f.Id_Group_Jenis = c.Id_Group_Jenis "
				SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
				SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Transaksi "
				SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
				SQL = SQL & "and a.No_Urut_Detail = e.Urut "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
				SQL = SQL & "and e.status is null "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For h As Integer = 0 To .Rows.Count - 1

								lok_material = .Rows(h).Item("kode_stock_owner")
								akun_kredit_material = .Rows(h).Item("akun_persediaan")
								ket_material = "Persediaan " + .Rows(h).Item("Kode_Group_Jenis")

							Next
						End If
					End With
				End Using

				'TODO : Insert dengan cara loop array detail
				For i As Integer = 0 To Arr_Detail_Biaya.Count - 1
					SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Detail_Biaya(i).akun, 1),
						Strings.Mid(Arr_Detail_Biaya(i).akun, 2, 1),
						Strings.Mid(Ganti(Arr_Detail_Biaya(i).akun), 3),
						KodePerusahaan, KodeProyek, $"Persediaan {Arr_Detail_Biaya(i).keterangan}" & " " & TxtFormulator_NoFaktur.Text, "0", Math.Round(Arr_Detail_Biaya(i).nilai, 0), pagenumber, Arr_Detail_Biaya(i).kd_so, Bahasa_Pilihan, Ket_Cost_Center_HO)
					ExecuteTrans(SQL)

					Temp_Nilai_Bahan += Math.Round(Arr_Detail_Biaya(i).nilai, 0)
				Next

				'SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit_material, 1),
				'  Strings.Mid(akun_kredit_material, 2, 1),
				'  Strings.Mid(Ganti(akun_kredit_material), 3),
				'  KodePerusahaan, KodeProyek, ket_material & TxtFormulator_NoFaktur.Text, "0", Nilai_Bahan, pagenumber, lok_material, Bahasa_Pilihan, Ket_Cost_Center_HO)
				'ExecuteTrans(SQL)
				pagenumber = pagenumber + 1
			End If

			If Nilai_Bahan <> Math.Round(Temp_Nilai_Bahan, 0) Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Terjadi Kesalahan, Nilai Bahan Tidak Sama !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			If Nilai_Packaging <> 0 Then
				SQL = "select top(1) "
				SQL = SQL & "b.Id_Group_Jenis, b.kode_stock_owner, c.akun_persediaan, Kode_Group_Jenis "
				SQL = SQL & "from Emi_Production_Results_Packaging_Det a, Barang b, EMI_Group_Jenis_Akun c, "
				SQL = SQL & "Emi_Production_Results_Packaging_Detail e, EMI_Group_Jenis f where "
				SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
				SQL = SQL & "and b.Kode_Perusahaan = f.Kode_Perusahaan and b.Id_Group_Jenis = f.Id_Group_Jenis "
				SQL = SQL & "and f.Kode_Perusahaan = c.Kode_Perusahaan and f.Id_Group_Jenis = c.Id_Group_Jenis "
				SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
				SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Transaksi "
				SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
				SQL = SQL & "and a.No_Urut_Detail = e.Urut "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
				SQL = SQL & "and e.status is null "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For h As Integer = 0 To .Rows.Count - 1

								lok_packaging = .Rows(h).Item("kode_stock_owner")
								akun_kredit_packaging = .Rows(h).Item("akun_persediaan")
								ket_packaging = "Persediaan " + .Rows(h).Item("Kode_Group_Jenis")

							Next
						End If
					End With
				End Using

				SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit_packaging, 1),
				   Strings.Mid(akun_kredit_packaging, 2, 1),
				   Strings.Mid(Ganti(akun_kredit_packaging), 3),
				   KodePerusahaan, KodeProyek, ket_packaging & TxtFormulator_NoFaktur.Text, "0", Nilai_Packaging, pagenumber, lok_packaging, Bahasa_Pilihan, Ket_Cost_Center_HO)
				ExecuteTrans(SQL)
				pagenumber = pagenumber + 1

			End If

			If Nilai_loss_production <> 0 Then

				SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Loss_production, 1),
						Strings.Mid(akun_Loss_production, 2, 1),
						Strings.Mid(Ganti(akun_Loss_production), 3),
						KodePerusahaan, KodeProyek, ket_loss_production & TxtFormulator_NoFaktur.Text, "0", Nilai_loss_production, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
				ExecuteTrans(SQL)
				pagenumber = pagenumber + 1

			End If

			For index = 0 To arrID_Work_Center.Count - 1
				SQL = "Select Kode_Akun_Biaya, Kode_Akun_Budget, a.keterangan "
				SQL = SQL & "From Emi_Jenis_Biaya_Produksi a where "
				SQL = SQL & " a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and kode_jenis_biaya_Produksi = '" & arrJenis_Biaya.Item(index) & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For h As Integer = 0 To .Rows.Count - 1

								Dim akun As String = .Rows(h).Item("Kode_Akun_Budget")
								Dim ket As String = .Rows(h).Item("keterangan")

								SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
									Strings.Mid(akun, 2, 1),
									Strings.Mid(Ganti(akun), 3),
									KodePerusahaan, KodeProyek, ket & " " & TxtFormulator_NoFaktur.Text, "0", arr_biaya_Produksi.Item(index), pagenumber, Lokasi, Bahasa_Pilihan, arrID_Work_Center.Item(index))
								ExecuteTrans(SQL)
								pagenumber = pagenumber + 1

							Next
						End If
					End With
				End Using
			Next

			SQL = "select round(sum(debit),2) as debit, round(sum(kredit),2) as kredit from detail_jurnal where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If Dr("debit") <> Dr("kredit") Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "insert into Emi_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses, Jenis) values ("
			SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher & "',"
			SQL = SQL & "'" & proses & "', 'GI') "
			ExecuteTrans(SQL)

#End Region

			'akhir stenly jurnal

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show("Transaksi Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If IsBatchComplete Then
			If IsAutomaticValidation Then
				MessageBox.Show("Transaksi Berhasil Divalidasi GI Otomatis", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			End If
		End If

		If asal = "CONTROLLING" Then
			'EMI_Display_Pengeluaran_Bahan_Baku.Button1_Click(Btn_Simpan, e)
			'EMI_Controlling_Produksi.Kosong()
			Me.Close()
		ElseIf asal = "INDEPENDENT" Then
			Txt_No_Split_PO.Text = ""
			Txt_Nm_Barang.Text = ""
			Txt_Jam_Split.Text = ""
			Txt_QR.Text = ""
			Txt_Batch.Text = ""
			DateTimePicker1.Value = Date.Now
			Dgv_HslProduction.Rows.Clear()

		ElseIf asal = "DISPLAY_RFID" Then
			Me.Close()
		End If
	End Sub

	Private Sub Dgv_HslProduction_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_HslProduction.CellEndEdit
		Get_Isi_Listview(Dgv_HslProduction.CurrentRow.Index)

		If Not Dgv_HslProduction.Rows.Count = 0 Then
			'======================
			'=     SET FORMAT     =
			'======================
			Dim culture As CultureInfo = CultureInfo.CurrentCulture

			If Dgv_HslProduction.CurrentCell.ColumnIndex = CellNilai_Produksi Then

				Dim status As String = Dgv_HslProduction.CurrentRow.Cells(CellStatus).Value.ToString()
				Dim cellKuantity As String = Dgv_HslProduction.CurrentCell.Value.ToString()

				If Not IsNumeric(cellKuantity) Then
					Dgv_HslProduction.CurrentCell.Value = 0
					Exit Sub
				End If

				If cellKuantity.Contains(",") Then
					MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Dgv_HslProduction.CurrentCell.Value = 0
					Exit Sub
				End If

				If Val(HilangkanTanda(cellKuantity)) <> 0 Then
					If status = "Terpenuhi" Then
						MessageBox.Show("Jumlah Sudah Terpenuhi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Dgv_HslProduction.CurrentCell.Value = 0
						Exit Sub
					End If
				End If

				'Dim nilai As Decimal = Decimal.Parse(cellKuantity)
				'Dim formattedValue As String = nilai.ToString("N2", culture)

				Dgv_HslProduction.CurrentCell.Value = Format(Val(HilangkanTanda(cellKuantity)), "N4")
			End If
		End If

		Try
			OpenConn()
			cekdata(Dgv_HslProduction.CurrentRow.Index)
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub get_no_faktur()
		Dim FPro_Results As String = "PRS"
		TxtFormulator_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("Emi_Production_Results", "No_Transaksi", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Transaksi, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub BtnFormulator_Refresh_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Refresh.Click
		Kosong()
	End Sub

	Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click

		If Txt_QR.Text.Trim.Length = 0 Then
			MessageBox.Show("Harap Lakukan Scan Barcode Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_QR.Focus() : Exit Sub
		End If

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If asal = "INDEPENDENT" Then

				Dim NoSplit As String = ""
				Dim Kd_Barang_Scan As String = ""
				Dim SN As String = ""
				Dim Qty As Double = 0

				'===================================
				'=     JIKA SCAN BARCODE BATCH     =
				'===================================
				Dim isBarcodeBatch As Boolean = False
				Dim arrDataBarcodeBatch As New List(Of (Kd_Barang_Scan As String, SN As String, Qty As Double))
				arrDataBarcodeBatch.Clear()
				SQL = "select a.No_Faktur_Order, c.Kode_Barang, d.SN_Baru as SN, d.Jumlah_Barang as Qty, f.Tgl_Produksi "
				SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c, "
				SQL = SQL & "N_EMI_Transaksi_Material_Requisition_QC_Validasi d, Barang_SN e, Emi_Split_Production_Order f "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = f.Kode_Perusahaan "
				SQL = SQL & "and a.No_Faktur = b.No_Faktur "
				SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
				SQL = SQL & "and c.No_Faktur = d.No_Faktur_RM and c.Urut_Oto = d.Urut_Det_RM "
				SQL = SQL & "and d.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang and d.SN_Baru = e.Serial_Number "
				SQL = SQL & "and a.No_Faktur_Order = f.No_Transaksi "
				SQL = SQL & "and a.Status is null and f.Status is null "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and b.Qr_Code = '" & Txt_QR.Text.Trim & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then

							isBarcodeBatch = True
							NoSplit = .Rows(0).Item("No_Faktur_Order")

							For i As Integer = 0 To .Rows.Count - 1
								Kd_Barang_Scan = .Rows(i).Item("Kode_Barang")
								SN = .Rows(i).Item("SN")
								Qty = .Rows(i).Item("Qty")

								If Txt_No_Split_PO.Text.Trim.Length = 0 Then
									Txt_No_Split_PO.Text = .Rows(i).Item("No_Faktur_Order")
									DateTimePicker1.Value = If(IsDate(.Rows(i).Item("Tgl_Produksi")), CDate(.Rows(i).Item("Tgl_Produksi")), Date.Now)

									Dgv_HslProduction.Rows.Clear()
								Else
									If Txt_No_Split_PO.Text.ToString.Trim.ToUpper <> .Rows(i).Item("No_Faktur_Order").ToString.Trim.ToUpper Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Keselahan, No Split pada Barcode Tidak Sama", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End If

								arrDataBarcodeBatch.Add((Kd_Barang_Scan:=Kd_Barang_Scan, SN:=SN, Qty:=Qty))
							Next
						Else
							isBarcodeBatch = False
						End If
					End With
				End Using

				If Not isBarcodeBatch Then
					'=========================
					'=      GET NO SPLIT     =
					'=========================
					SQL = "select a.No_Faktur_Order, c.Kode_Barang, d.Tgl_Produksi, "

					SQL = SQL & "isnull((select w.serial_number "
					SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_det y, Tf_Stock_det2 w, Barang_SN r "
					SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
					SQL = SQL & "and w.Kode_Perusahaan = r.Kode_Perusahaan "
					SQL = SQL & "and z.No_Faktur = x.No_Faktur "
					SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
					SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
					SQL = SQL & "and w.Serial_Number = r.Serial_Number "
					SQL = SQL & "and z.Status is null "
					SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
					SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
					SQL = SQL & "and (r.Qr_Code +'-'+ r.Kode_Unik_Berjalan) = '" & Txt_QR.Text.Trim & "'), '-') as SN, "

					SQL = SQL & "isnull((select w.jumlah "
					SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_det y, Tf_Stock_det2 w, Barang_SN r "
					SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
					SQL = SQL & "and w.Kode_Perusahaan = r.Kode_Perusahaan "
					SQL = SQL & "and z.No_Faktur = x.No_Faktur "
					SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
					SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
					SQL = SQL & "and w.Serial_Number = r.Serial_Number "
					SQL = SQL & "and z.Status is null "
					SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
					SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
					SQL = SQL & "and (r.Qr_Code +'-'+ r.Kode_Unik_Berjalan) = '" & Txt_QR.Text.Trim & "'), 0) as Qty "

					SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c, Emi_Split_Production_Order d "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
					SQL = SQL & "and a.No_Faktur = b.No_Faktur "
					SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
					SQL = SQL & "and a.Status is null and d.Status is null "
					SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.No_Faktur_Order = d.No_Transaksi "
					SQL = SQL & "and c.Urut_Oto = ( "
					SQL = SQL & "select x.Urut_Material_Requisition_Convert "
					SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_det y, Tf_Stock_det2 w, Barang_SN r "
					SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
					SQL = SQL & "and w.Kode_Perusahaan = r.Kode_Perusahaan "
					SQL = SQL & "and z.No_Faktur = x.No_Faktur "
					SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
					SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
					SQL = SQL & "and w.Serial_Number = r.Serial_Number "
					SQL = SQL & "and z.Status is null "
					SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
					SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
					SQL = SQL & "and (r.Qr_Code +'-'+ r.Kode_Unik_Berjalan) = '" & Txt_QR.Text.Trim & "') "

					SQL = SQL & "Union All "

					SQL = SQL & "select a.No_Faktur_Order, c.Kode_Barang, d.Tgl_Produksi, "

					SQL = SQL & "isnull((select w.serial_number "
					SQL = SQL & "from Tf_Stock_QC z, Tf_Stock_QC_Detail x, Tf_Stock_QC_det y, Tf_Stock_QC_det2 w, Barang_SN r "
					SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
					SQL = SQL & "and w.Kode_Perusahaan = r.Kode_Perusahaan "
					SQL = SQL & "and z.No_Faktur = x.No_Faktur "
					SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
					SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
					SQL = SQL & "and w.Serial_Number = r.Serial_Number "
					SQL = SQL & "and z.Status is null "
					SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
					SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
					SQL = SQL & "and (r.Qr_Code +'-'+ r.Kode_Unik_Berjalan) = '" & Txt_QR.Text.Trim & "'), '-') as SN, "

					SQL = SQL & "isnull((select w.jumlah "
					SQL = SQL & "from Tf_Stock_QC z, Tf_Stock_QC_Detail x, Tf_Stock_QC_det y, Tf_Stock_QC_det2 w, Barang_SN r "
					SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
					SQL = SQL & "and w.Kode_Perusahaan = r.Kode_Perusahaan "
					SQL = SQL & "and z.No_Faktur = x.No_Faktur "
					SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
					SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
					SQL = SQL & "and w.Serial_Number = r.Serial_Number "
					SQL = SQL & "and z.Status is null "
					SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
					SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
					SQL = SQL & "and (r.Qr_Code +'-'+ r.Kode_Unik_Berjalan) = '" & Txt_QR.Text.Trim & "'), 0) as Qty "

					SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c, Emi_Split_Production_Order d "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
					SQL = SQL & "and a.No_Faktur = b.No_Faktur "
					SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
					SQL = SQL & "and a.Status is null and d.Status is null "
					SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.No_Faktur_Order = d.No_Transaksi "
					SQL = SQL & "and c.Urut_Oto = ( "
					SQL = SQL & "select x.Urut_Material_Requisition_Convert "
					SQL = SQL & "from Tf_Stock_QC z, Tf_Stock_QC_Detail x, Tf_Stock_QC_det y, Tf_Stock_QC_det2 w, Barang_SN r "
					SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
					SQL = SQL & "and w.Kode_Perusahaan = r.Kode_Perusahaan "
					SQL = SQL & "and z.No_Faktur = x.No_Faktur "
					SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
					SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
					SQL = SQL & "and w.Serial_Number = r.Serial_Number "
					SQL = SQL & "and z.Status is null "
					SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
					SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
					SQL = SQL & "and (r.Qr_Code +'-'+ r.Kode_Unik_Berjalan) = '" & Txt_QR.Text.Trim & "') "

					SQL = SQL & "union all "

					SQL = SQL & "select a.No_Faktur_Order, c.Kode_Barang, f.Tgl_Produksi, d.SN_Baru as SN, d.Jumlah_Barang as Qty "
					SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c, "
					SQL = SQL & "N_EMI_Transaksi_Material_Requisition_QC_Validasi d, Barang_SN e, Emi_Split_Production_Order f "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = f.Kode_Perusahaan "
					SQL = SQL & "and a.No_Faktur = b.No_Faktur "
					SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
					SQL = SQL & "and c.No_Faktur = d.No_Faktur_RM and c.Urut_Oto = d.Urut_Det_RM "
					SQL = SQL & "and d.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang and d.SN_Baru = e.Serial_Number "
					SQL = SQL & "and a.Status is null and f.Status is null and d.status is null "
					SQL = SQL & "and a.No_Faktur_Order = f.No_Transaksi "
					SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and (e.Qr_Code+'-'+e.Kode_Unik_Berjalan) = '" & Txt_QR.Text.Trim & "' "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								If .Rows.Count > 1 Then
									CloseConn()
									MessageBox.Show("Terjadi Kesalahan, Barcode Berada Di 2 Lokasi Berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Txt_QR.Text = ""
									Exit Sub
								End If

								For i As Integer = 0 To .Rows.Count - 1
									NoSplit = .Rows(i).Item("No_Faktur_Order")
									Kd_Barang_Scan = .Rows(i).Item("Kode_Barang")
									SN = .Rows(i).Item("SN")
									Qty = .Rows(i).Item("Qty")

									If Txt_No_Split_PO.Text.Trim.Length = 0 Then
										Txt_No_Split_PO.Text = .Rows(i).Item("No_Faktur_Order")
										DateTimePicker1.Value = If(IsDate(.Rows(i).Item("Tgl_Produksi")), CDate(.Rows(i).Item("Tgl_Produksi")), Date.Now)

										Dgv_HslProduction.Rows.Clear()
									Else
										If Txt_No_Split_PO.Text.ToString.Trim.ToUpper <> .Rows(i).Item("No_Faktur_Order").ToString.Trim.ToUpper Then
											CloseTrans()
											CloseConn()
											MessageBox.Show("Terjadi Keselahan, No Split pada Barcode Tidak Sama", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End If
								Next
							Else
								CloseTrans()
								CloseConn()
								MessageBox.Show("Terjadi Kesalahan, No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Txt_QR.Text = ""
								Exit Sub
							End If
						End With
					End Using
				End If

				Dim isFirstData As Boolean = If(Dgv_HslProduction.Rows.Count = 0, True, False)

				If isFirstData Then

					'SQL = "select a.No_Transaksi, b.Kode_Barang, b.Nama, a.Tgl_Produksi, a.Jam_Produksi from  "
					'SQL = SQL & "Emi_Split_Production_Order a, barang b "
					'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
					'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & NoSplit & "' "
					'SQL = SQL & "order by b.nama"

					SQL = "select a.No_Transaksi, b.Kode_Barang, b.Nama, a.Tgl_Produksi, a.Jam_Produksi, "
					SQL = SQL & "case when d.Proses is null then 1 else cast(d.proses as float) + 1 end as Batch "
					SQL = SQL & "from Emi_Split_Production_Order a "
					SQL = SQL & "inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
					SQL = SQL & "left join Emi_Production_Results c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Transaksi = c.No_Production_Order and c.Status is null "
					SQL = SQL & "left join Emi_Production_Results_HPP d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Transaksi = d.No_Transaksi and d.Tanggal is not null "
					SQL = SQL & "where a.Status is null and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & NoSplit & "' "
					SQL = SQL & "order by b.nama "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								For i As Integer = 0 To .Rows.Count - 1
									Txt_Nm_Barang.Text = .Rows(i).Item("Nama")
									DateTimePicker1.Value = .Rows(i).Item("Tgl_Produksi")
									Txt_Jam_Split.Text = .Rows(i).Item("Jam_Produksi")
									Txt_Batch.Text = .Rows(i).Item("Batch")
								Next
							End If
						End With
					End Using

					Dgv_HslProduction.Rows.Clear()
					'SQL = "select a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah ,b.Satuan, c.flag_potong_stok, isnull(c.standar_price,0) as standar_price, Flag_Non_Barcode from  "
					'SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Bahan b, barang c "
					'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
					'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
					'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & NoSplit & "' "
					'SQL = SQL & "order by c.nama"

					SQL = "select a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah ,b.Satuan, c.flag_potong_stok, isnull(c.standar_price,0) as standar_price, Flag_Non_Barcode, "
					SQL = SQL & "isnull(f.Selesai, 'T') as Status, "
					SQL = SQL & "isnull(f.Nilai_Produksi, 0) as JumlahInput, "
					'SQL = SQL & "isnull(f.Nilai_Formula, 0) as JumlahKebutuhan "

					SQL = SQL & "isnull(( "
					SQL = SQL & "select ((x.Jumlah) /  "
					SQL = SQL & "(select r.Hasil from Emi_Transaksi_Formulator r "
					SQL = SQL & "where r.Kode_Perusahaan = x.Kode_Perusahaan And r.No_Faktur = x.No_Faktur)) * "
					SQL = SQL & "a.Qty_Batch "
					SQL = SQL & "from EMI_Order_Produksi z, EMI_Transaksi_Formulator_Detail_Bahan x "
					SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
					SQL = SQL & "and z.Kode_Formula = x.No_Faktur "
					SQL = SQL & "and z.Status is null "
					SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan "
					SQL = SQL & "and a.No_PO = z.No_Faktur "
					SQL = SQL & "and x.Kode_Barang = b.Kode_Barang "
					SQL = SQL & "), 0) as JumlahKebutuhan "

					SQL = SQL & "from Emi_Split_Production_Order a "
					SQL = SQL & "inner join Emi_Split_Production_Order_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
					SQL = SQL & "inner join barang c on  b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
					SQL = SQL & "left join Emi_Production_Results d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Production_Order and d.Status is null "
					SQL = SQL & "left join Emi_Production_Results_HPP e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.No_Transaksi = e.No_Transaksi and e.Tanggal is not null and e.Proses = " & Txt_Batch.Text & " "
					SQL = SQL & "left join Emi_Production_Results_detail f on d.kode_perusahaan = f.kode_perusahaan and d.No_Transaksi = f.No_Transaksi and f.Kode_Barang = b.Kode_Barang and f.Proses = " & Txt_Batch.Text & " "
					SQL = SQL & "where a.Status is null "
					SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & NoSplit & "' "
					SQL = SQL & "order by c.nama "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								For i As Integer = 0 To .Rows.Count - 1
									Dgv_HslProduction.Rows.Add()
									Dgv_HslProduction.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 12)

									If Dgv_HslProduction.Columns("Column5") IsNot Nothing Then
										Dgv_HslProduction.Rows(i).Cells("Column5").Style.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
									End If

									'Dgv_HslProduction.Rows.Item(i).Cells(CellNamaBahan).Value = .Rows(i).Item("Nama")
									Dgv_HslProduction.Rows.Item(i).Cells(CellNamaBahan).Value = "X"
									Dgv_HslProduction.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
									Dgv_HslProduction.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")

									Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N4")
									Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
									Dgv_HslProduction.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")

									If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
										Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = ""
									Else
										Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = .Rows(i).Item("flag_potong_stok")
									End If

									Dgv_HslProduction.Rows.Item(i).Cells(CellStandarPrice).Value = .Rows(i).Item("standar_price")

									If General_Class.CekNULL(.Rows(i).Item("Flag_Non_Barcode")) = "Y" Then
										Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).ReadOnly = False
										Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Style.BackColor = Color.LightGray
									Else
										Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).ReadOnly = True
										Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Style.BackColor = Color.White
									End If

									Dgv_HslProduction.Rows.Item(i).Cells(CellJumlahInput).Value = .Rows(i).Item("JumlahInput")
									Dgv_HslProduction.Rows.Item(i).Cells(CellJumlahKebutuhan).Value = Format(.Rows(i).Item("JumlahKebutuhan"), "N4")

									If General_Class.CekNULL(.Rows(i).Item("Status")) = "Y" Then
										Dgv_HslProduction.Rows.Item(i).Cells(CellStatus).Value = "Terpenuhi"
										Dgv_HslProduction.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
									Else
										Dgv_HslProduction.Rows.Item(i).Cells(CellStatus).Value = "Belum Terpenuhi"
										Dgv_HslProduction.Rows(i).DefaultCellStyle.BackColor = Color.White
									End If

								Next
							End If
						End With
					End Using

				End If

				'===============================
				'=     GET NILAI TOLERANSI     -
				'===============================
				Dim Toleransi_Min As Double = 0
				Dim Toleransi_Max As Double = 0
				SQL = "Select top(1) Toleransi_Timbang_Min, Toleransi_Timbang_Max, Flag_Non_Barcode "
				SQL = SQL & "from barang a "
				SQL = SQL & "where a.Kode_Perusahaan ='" & KodePerusahaan & "' "
				SQL = SQL & "and Kode_barang='" & Kd_Barang_Scan & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Toleransi_Min = Dr("Toleransi_Timbang_Min")
						Toleransi_Max = Dr("Toleransi_Timbang_Max")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Nilai Toleransi untuk barang {Kd_Barang_Scan} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=============================
				'=     GET NILAI FORMULA     =
				'=============================
				Dim Nilai_Formula As Double = 0
				SQL = "Select c.Kode_Barang, "
				SQL = SQL & "isnull(( "
				SQL = SQL & "round( "
				SQL = SQL & "(c.Jumlah / (select dbo.Ubah_Satuan(a.Kode_Perusahaan, 'masa', c.Kode_Barang, z.Satuan_Hasil, c.satuan, z.Hasil) from Emi_Transaksi_Formulator z "
				SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur) "
				SQL = SQL & ") "
				SQL = SQL & "* "
				SQL = SQL & "(ISNULL((a.Qty_Batch), 0)),4)), 0) as Nilai_Formula "
				SQL = SQL & "From Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
				SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan "
				SQL = SQL & "And a.No_PO = b.No_Faktur "
				SQL = SQL & "And b.Kode_Formula = c.No_Faktur "
				SQL = SQL & "And a.No_Transaksi = '" & NoSplit & "' "
				SQL = SQL & "And c.Kode_Barang = '" & Kd_Barang_Scan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						Nilai_Formula = Val(HilangkanTanda(Format(dr("Nilai_Formula"), "N4")))
					Else
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Nilai Formula Tidak di temukan . . ! !")
						Exit Sub
					End If
				End Using

				'==============================
				'=     LAKUKAN PENCECEKAN     -
				'==============================
				For i As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
					If Dgv_HslProduction.Rows(i).Cells(CellKode_Bahan).Value = Kd_Barang_Scan Then

						If arrBarcodeScan.Any(Function(x) x.Serial_Number = SN) Then
							CloseTrans()
							CloseConn()
							MessageBox.Show("Barcode Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Txt_QR.Text = ""
							Exit Sub
						End If

						If Dgv_HslProduction.Rows(i).Cells(CellStatus).Value.ToString.Trim = "Terpenuhi" Then
							CloseTrans()
							CloseConn()
							MessageBox.Show("Barang Sudah Terpenuhi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Txt_QR.Text = ""
							Exit Sub
						End If

						Dim TotalInput As Double = Val(HilangkanTanda(Dgv_HslProduction.Rows(i).Cells(CellNilai_Produksi).Value)) + Qty

						Dim Nilai_Min As Double = Nilai_Formula - ((Toleransi_Min / 100) * Nilai_Formula)
						Dim Nilai_Max As Double = Nilai_Formula + ((Toleransi_Max / 100) * Nilai_Formula)

						If TotalInput < Nilai_Min Then
							' If MessageBox.Show("Jumlah Kurang Dari Toleransi Min, Apakah Ingin Dilanjutkan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub
						ElseIf TotalInput > Nilai_Max Then
							CloseTrans()
							CloseConn()
							MessageBox.Show("Nilai pada barang " & Kd_Barang_Scan & " melebihi Toleransi . . ! !")
							Exit Sub
						End If

						'Dgv_HslProduction.Rows(i).Cells(CellNilai_Produksi).Value = Val(HilangkanTanda(Dgv_HslProduction.Rows(i).Cells(CellNilai_Produksi).Value)) + Qty

					End If
				Next

				Dim dataHasFound As Boolean = False
				If isBarcodeBatch Then
					If arrDataBarcodeBatch.Count <> 0 Then

						'========================================================================
						'=     LOOP UNTUK CEK APAKAH ADA DATA YANG SUDAH DIINPUT SEBELUMNYA     =
						'========================================================================
						For i As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
							Dim Lv_KdBarang As String = Dgv_HslProduction.Rows(i).Cells(CellKode_Bahan).Value
							Dim Data = arrDataBarcodeBatch.FirstOrDefault(Function(x) x.Kd_Barang_Scan.IndexOf(Lv_KdBarang, StringComparison.OrdinalIgnoreCase) >= 0)

							'If Dgv_HslProduction.Rows(i).Cells(CellKode_Bahan).Value = Data.Kd_Barang_Scan Then
							'    CloseTrans()
							'    CloseConn()
							'    MessageBox.Show($"Barcode Barang {Lv_KdBarang} Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'    Txt_QR.Text = ""
							'    Exit Sub
							'End If

							' LINQ Untuk Cek Apakah Ada Data Yang Sudah Diinput Sebelumnya
							Dim adaDuplikat As Boolean = arrDataBarcodeBatch.Any(Function(a) arrBarcodeScan.Any(Function(b) b.Kd_Barang = a.Kd_Barang_Scan))

							If adaDuplikat Then
								CloseTrans()
								CloseConn()
								MessageBox.Show("Terdapat Barcode Yang Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Txt_QR.Text = ""
								Exit Sub
							End If

						Next

						'==============================================
						'=     CEK APAKAH BARCODE SUDAH DIGUNAKAN     =
						'==============================================
						For i As Integer = 0 To arrDataBarcodeBatch.Count - 1

							SQL = "select Serial_Number, Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and Serial_Number = '" & arrDataBarcodeBatch(i).SN & "' "
							Using Ds = BindingTrans(SQL)
								With Ds.Tables("MyTable")
									If .Rows.Count <> 0 Then
										For J As Integer = 0 To .Rows.Count - 1
											If Val(HilangkanTanda(.Rows(J).Item("Jumlah"))) <= 0 Then
												CloseTrans()
												CloseConn()
												MessageBox.Show("Terjadi Kesalahan, Stock Pada Barcode Tidak Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Txt_QR.Text = ""
												Exit Sub
											End If

											SQL = "select c.Kode_Perusahaan "
											SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b, Emi_Production_Results_Det c "
											SQL = SQL & "where a.Kode_Perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan "
											SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
											SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Urut = c.No_Urut_Detail "
											SQL = SQL & "and a.Status is null "
											SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
											SQL = SQL & "and a.No_Production_Order = '" & Txt_No_Split_PO.Text & "' "
											SQL = SQL & "and c.Serial_Number = '" & .Rows(J).Item("Serial_Number") & "' "
											Using Dr = OpenTrans(SQL)
												If Dr.Read Then
													Dr.Close()
													CloseTrans()
													CloseConn()
													MessageBox.Show("Terjadi Kesalahan, Terdapat Barcode yang Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Txt_QR.Text = ""
													Exit Sub
												End If
											End Using

										Next
									Else
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Kesalahan, Data Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Txt_QR.Text = ""
										Exit Sub
									End If
								End With
							End Using
						Next

						For i As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
							Dim Lv_KdBarang As String = Dgv_HslProduction.Rows(i).Cells(CellKode_Bahan).Value
							Dim Lv_Status As String = Dgv_HslProduction.Rows(i).Cells(CellStatus).Value.ToString.Trim

							'Dim Data = arrDataBarcodeBatch.FirstOrDefault(Function(x) x.Kd_Barang_Scan.IndexOf(Lv_KdBarang, StringComparison.OrdinalIgnoreCase) >= 0)

							Dim ListData = arrDataBarcodeBatch _
											.Where(Function(x) x.Kd_Barang_Scan.IndexOf(Lv_KdBarang, StringComparison.OrdinalIgnoreCase) >= 0) _
											.ToList()

							If ListData.Any() Then

								For Each Datas In ListData

									If arrBarcodeScan.Any(Function(x) x.Serial_Number = Datas.SN) Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Barcode Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Txt_QR.Text = ""
										Exit Sub
									End If

									arrBarcodeScan.Add((Kd_Barang:=Datas.Kd_Barang_Scan, Serial_Number:=Datas.SN))
									dataHasFound = True

									If Dgv_HslProduction.Rows(i).Cells(CellStatus).Value.ToString.Trim = "Terpenuhi" Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Barang Sudah Terpenuhi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Txt_QR.Text = ""
										Exit Sub
									End If

									Dgv_HslProduction.Rows(i).Cells(CellNilai_Produksi).Value = Val(HilangkanTanda(Dgv_HslProduction.Rows(i).Cells(CellNilai_Produksi).Value)) + Datas.Qty

									cekdata(i)
								Next

								'Else
								'CloseTrans()
								'CloseConn()
								'MessageBox.Show($"Barcode Barang {Lv_KdBarang} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'Txt_QR.Text = ""
								'Exit Sub

							End If

							'If Dgv_HslProduction.Rows(i).Cells(CellKode_Bahan).Value = Data.Kd_Barang_Scan Then

							'    If arrBarcodeScan.Any(Function(x) x.Serial_Number = Data.SN) Then
							'        CloseTrans()
							'        CloseConn()
							'        MessageBox.Show("Barcode Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'        Txt_QR.Text = ""
							'        Exit Sub
							'    End If

							'    arrBarcodeScan.Add((Kd_Barang:=Data.Kd_Barang_Scan, Serial_Number:=Data.SN))
							'    dataHasFound = True

							'    If Dgv_HslProduction.Rows(i).Cells(CellStatus).Value.ToString.Trim = "Terpenuhi" Then
							'        CloseTrans()
							'        CloseConn()
							'        MessageBox.Show("Barang Sudah Terpenuhi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'        Txt_QR.Text = ""
							'        Exit Sub
							'    End If

							'    Dgv_HslProduction.Rows(i).Cells(CellNilai_Produksi).Value = Val(HilangkanTanda(Dgv_HslProduction.Rows(i).Cells(CellNilai_Produksi).Value)) + Data.Qty

							'End If

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terjadi Kesalahan, Tidak ada Data di dalam Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_QR.Text = ""
						Exit Sub
					End If

					Dim adasda As Integer = arrBarcodeScan.Count
					Dim Vasdada As Integer = arrDataBarcodeBatch.Count
				Else

					'==============================================
					'=     CEK APAKAH BARCODE SUDAH DIGUNAKAN     =
					'==============================================
					SQL = "select Serial_Number, Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and (Qr_Code+'-'+Kode_Unik_Berjalan) = '" & Txt_QR.Text.Trim & "' "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								For i As Integer = 0 To .Rows.Count - 1
									If Val(HilangkanTanda(.Rows(i).Item("Jumlah"))) <= 0 Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Kesalahan, Stock Pada Barcode Tidak Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Txt_QR.Text = ""
										Exit Sub
									End If

									SQL = "select c.Kode_Perusahaan "
									SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b, Emi_Production_Results_Det c "
									SQL = SQL & "where a.Kode_Perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan "
									SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
									SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Urut = c.No_Urut_Detail "
									SQL = SQL & "and a.Status is null "
									SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "and a.No_Production_Order = '" & Txt_No_Split_PO.Text & "' "
									SQL = SQL & "and c.Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											Dr.Close()
											CloseTrans()
											CloseConn()
											MessageBox.Show("Terjadi Kesalahan, Barcode Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Txt_QR.Text = ""
											Exit Sub
										End If
									End Using

								Next
							Else
								CloseTrans()
								CloseConn()
								MessageBox.Show("Terjadi Kesalahan, Data Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Txt_QR.Text = ""
								Exit Sub
							End If
						End With
					End Using

					For i As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
						If Dgv_HslProduction.Rows(i).Cells(CellKode_Bahan).Value = Kd_Barang_Scan Then

							If arrBarcodeScan.Any(Function(x) x.Serial_Number = SN) Then
								CloseTrans()
								CloseConn()
								MessageBox.Show("Barcode Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Txt_QR.Text = ""
								Exit Sub
							End If
							arrBarcodeScan.Add((Kd_Barang:=Kd_Barang_Scan, Serial_Number:=SN))
							dataHasFound = True

							If Dgv_HslProduction.Rows(i).Cells(CellStatus).Value.ToString.Trim = "Terpenuhi" Then
								CloseTrans()
								CloseConn()
								MessageBox.Show("Barang Sudah Terpenuhi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Txt_QR.Text = ""
								Exit Sub
							End If

							Dgv_HslProduction.Rows(i).Cells(CellNilai_Produksi).Value = Val(HilangkanTanda(Dgv_HslProduction.Rows(i).Cells(CellNilai_Produksi).Value)) + Qty

							cekdata(i)
						End If
					Next
				End If

				If Not dataHasFound Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

#Region "Kode Lama"

				''=========================
				''=      GET NO SPLIT     =
				''=========================
				'Dim NoSplit As String = ""
				'Dim Kd_Barang_Scan As String = ""
				'SQL = "select a.No_Faktur_Order, c.Kode_Barang "
				'SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
				'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
				'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
				'SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
				'SQL = SQL & "and a.Status is null "
				'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'SQL = SQL & "and c.Urut_Oto = ( "
				'SQL = SQL & "select x.Urut_Material_Requisition_Convert "
				'SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_det y, Tf_Stock_det2 w, Barang_SN r "
				'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
				'SQL = SQL & "and w.Kode_Perusahaan = r.Kode_Perusahaan "
				'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
				'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
				'SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
				'SQL = SQL & "and w.Serial_Number = r.Serial_Number "
				'SQL = SQL & "and z.Status is null "
				'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
				'SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
				'SQL = SQL & "and (r.Qr_Code +'-'+ r.Kode_Unik_Berjalan) = '" & Txt_QR.Text.Trim & "') "
				'Using Dr = OpenTrans(SQL)
				'    If Dr.Read Then
				'        NoSplit = Dr("No_Faktur_Order")
				'        Kd_Barang_Scan = Dr("Kode_Barang")
				'        If TextBox4.Text.Trim.Length = 0 Then
				'            TextBox4.Text = Dr("No_Faktur_Order")
				'            Dgv_HslProduction.Rows.Clear()
				'        Else
				'            If TextBox4.Text.ToString.Trim.ToUpper <> Dr("No_Faktur_Order").ToString.Trim.ToUpper Then
				'                Dr.Close()
				'                CloseConn()
				'                MessageBox.Show("Terjadi Keselahan, No Split pada Barcode Tidak Sama", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                Exit Sub
				'            End If
				'        End If
				'    Else
				'        Dr.Close()
				'        CloseConn()
				'        MessageBox.Show("Terjadi Keselahan, No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'        Txt_QR.Text = ""
				'        Exit Sub
				'    End If
				'End Using

				''=======================
				''=      GET DETAIL     =
				''=======================

				'SQL = "select a.No_Transaksi, a.no_po, a.Tgl_Produksi, a.Jam_Produksi, b.nama "
				'SQL = SQL & "from Emi_Split_Production_Order a, barang b "
				'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
				'SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
				'SQL = SQL & "and a.Status is null "
				'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'SQL = SQL & "and a.no_transaksi = '" & TextBox4.Text & "' "
				'Using Dr = OpenTrans(SQL)
				'    If Dr.Read Then
				'        TextBox6.Text = Dr("nama")
				'        DateTimePicker1.Value = Dr("Tgl_Produksi")
				'        TextBox1.Text = Dr("Jam_Produksi")
				'        fno_po = Dr("no_po")
				'    Else
				'        Dr.Close()
				'        CloseConn()
				'        MessageBox.Show("Terjadi Keselahan, Data No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'        Exit Sub
				'    End If
				'End Using

				''========================
				''=      CEK BARCODE     =
				''========================
				'Dim arr_SN As New ArrayList
				'Dim current_Kd_Barang As String = ""
				'Dim Jumlah_Stock As Double = 0
				'Dim Jumlah_Stock_Bags As Double = 0
				'SQL = "select Jumlah, Jumlah_Bags, Kode_Barang, Serial_Number "
				'SQL = SQL & "from Barang_SN "
				'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				'SQL = SQL & "and (Qr_Code +'-'+Kode_Unik_Berjalan) = '" & Txt_QR.Text & "' "
				'SQL = SQL & "and Jumlah <> 0 "
				'Using Dr = OpenTrans(SQL)
				'    Do While Dr.Read
				'        If current_Kd_Barang = "" Then
				'            current_Kd_Barang = Dr("Kode_Barang")
				'        Else
				'            If current_Kd_Barang <> Dr("Kode_Barang") Then
				'                Dr.Close()
				'                CloseConn()
				'                MessageBox.Show("Terjadi Keselahan, Data di dalam Barcode Terdapat Kode Barang yang Berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                Exit Sub
				'            End If
				'        End If

				'        arr_SN.Add(Dr("Serial_Number"))
				'        Jumlah_Stock += Dr("Jumlah")
				'        Jumlah_Stock_Bags += Dr("Jumlah_Bags")
				'    Loop
				'End Using

				''================================================
				''=      CEK APAKAH BARANG ADA DI DALAM LIST     =
				''================================================
				'Dim hasData As Boolean = False
				'Dim Kd_SO As String = ""
				'Dim Index As Integer = -1
				'Dim lastIndex As Integer = Dgv_HslProduction.Rows.Count
				'For i As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
				'    Get_Isi_Listview(i)
				'    If LvKode_Bahan.ToString.ToUpper = Kd_Barang_Scan.ToString.ToUpper Then
				'        hasData = True
				'        Kd_SO = LvKode_So
				'        Index = i
				'        Exit For
				'    End If
				'Next

				'If hasData Then
				'    CloseConn()
				'    MessageBox.Show("Barang Sudah ada di dalam List", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'    Txt_QR.Text = ""
				'    Exit Sub
				'End If

				''====================================
				''=      INSERT DATA KE LISTVIEW     =
				''====================================
				'SQL = "select a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah ,b.Satuan, c.flag_potong_stok, isnull(c.standar_price,0) as standar_price from  "
				'SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Bahan b, barang c "
				'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
				'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
				'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & TextBox4.Text & "' and b.Kode_Barang = '" & Kd_Barang_Scan & "' "
				'SQL = SQL & "order by c.nama"
				'Using Ds = BindingTrans(SQL)
				'    With Ds.Tables("MyTable")
				'        If .Rows.Count <> 0 Then
				'            For i As Integer = 0 To .Rows.Count - 1

				'                Dgv_HslProduction.Rows.Add(1)
				'                Dgv_HslProduction.Rows.Item(lastIndex).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
				'                Dgv_HslProduction.Rows.Item(lastIndex).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")
				'                Dgv_HslProduction.Rows.Item(lastIndex).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N4")
				'                Dgv_HslProduction.Rows.Item(lastIndex).Cells(CellNilai_Produksi).Value = Format(Jumlah_Stock, "N4")
				'                Dgv_HslProduction.Rows.Item(lastIndex).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")

				'                If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
				'                    Dgv_HslProduction.Rows.Item(lastIndex).Cells(CellPotStokBhn).Value = ""
				'                Else
				'                    Dgv_HslProduction.Rows.Item(lastIndex).Cells(CellPotStokBhn).Value = .Rows(i).Item("flag_potong_stok")
				'                End If

				'                Dgv_HslProduction.Rows.Item(lastIndex).Cells(CellStandarPrice).Value = .Rows(i).Item("standar_price")

				'            Next
				'        Else
				'            CloseConn()
				'            MessageBox.Show("Terjadi Keselahan, Data GI Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'            Exit Sub
				'        End If
				'    End With
				'End Using

				'Txt_QR.Text = ""

				'==========================================================================================================================================================================

				''================================================
				''=      CEK APAKAH BARANG ADA DI DALAM LIST     =
				''================================================
				'Dim hasData As Boolean = False
				'Dim Kd_SO As String = ""
				'Dim Index As Integer = -1
				'For i As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
				'    Get_Isi_Listview(i)
				'    If LvKode_Bahan.ToString.ToUpper = current_Kd_Barang.ToString.ToUpper Then
				'        hasData = True
				'        Kd_SO = LvKode_So
				'        Index = i
				'        Exit For
				'    End If
				'Next

				'If hasData Then

				'    '=============================
				'    '=      GET NO TRANSAKSI     =
				'    '=============================
				'    Dim hasData2 As Boolean = False
				'    Dim No_Transaksi As String = ""
				'    SQL = "select no_transaksi, "
				'    SQL = SQL & "isnull((select top(1) proses from ( "
				'    SQL = SQL & "Select proses from Emi_Production_Results_Detail x where "
				'    SQL = SQL & "a.Kode_Perusahaan = x.Kode_Perusahaan And a.No_Transaksi = x.No_Transaksi "
				'    SQL = SQL & "union all "
				'    SQL = SQL & "Select proses from Emi_Production_Results_Packaging_Detail x where "
				'    SQL = SQL & "a.Kode_Perusahaan = x.Kode_Perusahaan And a.No_Transaksi = x.No_Transaksi "
				'    SQL = SQL & ") as Data order by proses desc ),0) as proses "
				'    SQL = SQL & "from Emi_Production_Results a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Order = '" & TextBox4.Text & "' "
				'    Using Dr = OpenTrans(SQL)
				'        If Dr.Read Then
				'            hasData2 = True
				'            No_Transaksi = Dr("no_transaksi")
				'        End If
				'    End Using

				'    '==============================
				'    '=      GET NILAI FORMULA     =
				'    '==============================
				'    Dim Nilai_Formula As Double = 0
				'    SQL = "Select c.Kode_Barang, "
				'    SQL = SQL & "isnull((round((c.Jumlah / (select z.Hasil from Emi_Transaksi_Formulator z where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur) "
				'    SQL = SQL & ")*(ISNULL((a.Qty_Batch), 0)),4)), 0) as Nilai_Formula "
				'    SQL = SQL & "From Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
				'    SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan "
				'    SQL = SQL & "And a.No_PO = b.No_Faktur "
				'    SQL = SQL & "And b.Kode_Formula = c.No_Faktur "
				'    SQL = SQL & "And a.No_Transaksi = '" & TextBox4.Text & "' "
				'    SQL = SQL & "And c.Kode_Barang = '" & current_Kd_Barang & "' "
				'    Using Dr = OpenTrans(SQL)
				'        If Dr.Read Then
				'            Nilai_Formula = Dr("Nilai_Formula")
				'        End If
				'    End Using

				'    '========================================
				'    '=      GET NILAI YANG SUDAH DOSING     =
				'    '========================================
				'    Dim Nilai_Yang_Sudah_Dosing As Double = 0
				'    Dim hasSelesai As Boolean = False
				'    If hasData2 Then

				'        SQL = "select top 1 a.No_Transaksi, a.No_Production_Order, b.Proses, isnull(b.Selesai,'T') as Selesai, b.urut, b.Nilai_Produksi "
				'        SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b "
				'        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
				'        SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
				'        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'        SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
				'        SQL = SQL & "and b.Kode_Stock_Owner = '" & Kd_SO & "' "
				'        SQL = SQL & "and b.Kode_Barang = '" & current_Kd_Barang & "' "
				'        SQL = SQL & "order by b.proses desc "
				'        Using Dr = OpenTrans(SQL)
				'            If Dr.Read Then
				'                If General_Class.CekNULL(Dr("Selesai")) = "" Or General_Class.CekNULL(Dr("Selesai")) <> "Y" Then
				'                    Nilai_Yang_Sudah_Dosing = Val(HilangkanTanda(Format(Dr("Nilai_Produksi"), "N4")))
				'                End If

				'                If General_Class.CekNULL(Dr("Selesai")) = "Y" Then
				'                    MessageBox.Show("Proses Sudah Selesai, Penginputan akan dilanjutkan untuk Batch Selanjutnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
				'                    hasSelesai = True
				'                End If
				'            End If
				'        End Using

				'    End If

				'    '=================================
				'    '=      GET NILAI TOLEREANSI     =
				'    '=================================
				'    Dim Toleransi_timbang_Min As Double = 0
				'    Dim Toleransi_timbang_Max As Double = 0
				'    SQL = "Select top(1) Toleransi_Timbang_Min, Toleransi_Timbang_Max from "
				'    SQL = SQL & "barang a where "
				'    SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
				'    SQL = SQL & "Kode_barang='" & current_Kd_Barang & "' "
				'    Using dr = OpenTrans(SQL)
				'        If dr.Read Then
				'            Toleransi_timbang_Min = dr("Toleransi_Timbang_Min")
				'            Toleransi_timbang_Max = dr("Toleransi_Timbang_Max")
				'        Else
				'            dr.Close()
				'            CloseTrans()
				'            CloseConn()
				'            MessageBox.Show("Kode barang tidak ditemukan . . ! !")
				'            Exit Sub
				'        End If
				'    End Using

				'    'If (Nilai_Yang_Sudah_Dosing + Val(HilangkanTanda(Jumlah_Stock))) > Nilai_Formula + ((Toleransi_timbang_Max / 100) * Nilai_Formula) Then
				'    '    CloseTrans()
				'    '    CloseConn()
				'    '    MessageBox.Show("Nilai Stock melebihi Toleransi . . ! !")
				'    '    Exit Sub
				'    'End If

				'    Dgv_HslProduction.Rows(Index).Cells(CellNilai_Produksi).Value = Format(Jumlah_Stock, "N0")

				'Else
				'    CloseConn()
				'    MessageBox.Show("Tidak Ada Kode Barang " & current_Kd_Barang & " Dalam List GI", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'    Exit Sub
				'End If

				'Txt_QR.Text = ""

#End Region

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

		Txt_QR.Text = ""

	End Sub

	Private Sub Txt_QR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_QR.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_QR.Text = Trim(Txt_QR.Text)

			If Txt_QR.Text.Trim.Length <> 0 Then
				Btn_Scan_Click(Me, Nothing)
			End If
		Else
			'If Char.IsLetterOrDigit(e.KeyChar) OrElse Char.IsSymbol(e.KeyChar) OrElse e.KeyChar = "-"c Then
			'    ValueBarcode &= e.KeyChar.ToString.Trim
			'End If

		End If
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		'FMenu.Show()
		Me.Close()
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Dim SD As New EMI_Controlling_Produksi With {
			.StartPosition = FormStartPosition.CenterScreen,
			.asal = "HASIL_PENGELUARAN_BAHAN_BAKU"
		}

		SD.ShowDialog()
	End Sub

	Private Sub Dgv_HslProduction_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_HslProduction.CellFormatting
		Try
			If e.ColumnIndex = CellJumlahInput OrElse e.ColumnIndex = CellNilai_Produksi OrElse e.ColumnIndex = CellJumlahKebutuhan Then

				If e.RowIndex >= 0 Then
					Dim jumlah As Double = 0
					Dim satuan As String = ""

					If Dgv_HslProduction.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing Then

						jumlah = Val(HilangkanTanda(Dgv_HslProduction.Rows(e.RowIndex).Cells(e.ColumnIndex).Value))
					End If

					If Dgv_HslProduction.Rows(e.RowIndex).Cells(CellSatuan).Value IsNot Nothing Then
						satuan = Dgv_HslProduction.Rows(e.RowIndex).Cells(CellSatuan).Value
					End If

					' Format tampilan, misal: "10 kg"
					e.Value = $"{Format(jumlah, "N4")} {satuan}"
					e.FormattingApplied = True
				End If
			End If
		Catch ex As Exception
			'jika error pesan error akan masuk ke debug
			Debug.WriteLine("Error di CellFormatting: " & ex.Message)
		End Try
	End Sub

	'Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
	'    If TextBox5.Text.Trim.Length = 0 Then
	'        Exit Sub
	'    ElseIf TextBox8.Text.Trim.Length = 0 Then
	'        Exit Sub
	'    End If
	'    Dim a As Double = 0
	'    a = Val(HilangkanTanda(TextBox5.Text)) - Val(HilangkanTanda(TextBox8.Text))
	'    TextBox7.Text = Format(a, "N2")
	'End Sub

	'Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
	'    If e.KeyChar = Chr(13) Then TextBox8.Focus()
	'    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
	'End Sub

	'Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
	'    If e.KeyChar = Chr(13) Then Dgv_HslProduction.Focus()
	'    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
	'End Sub

End Class
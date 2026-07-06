Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Transactions

'Imports Microsoft.SqlServer.Server

Public Class N_EMI_Transaksi_Request_Material_QC_Validasi
	Dim arrcari As New ArrayList
	Dim Jenis = "Transaksi_Timbang_Kosong"
	Public Txt_Ekspedisi As String = ""
	Dim Random As New Random()
	Dim ReadThread As Thread

	Dim PersenToleransiMIN As Double = 0
	Dim PersenToleransiMAX As Double = 0

	Dim tahunMulaiProduksi As String = ""
	Private Is2ndPrint As Boolean = False

	Private imageBytes1 As Byte = Nothing
	Private FileSize1 As UInt32
	Private rawData1() As Byte
	Private fs1 As FileStream

	Private imageBytes2 As Byte = Nothing
	Private FileSize2 As UInt32
	Private rawData2() As Byte
	Private fs2 As FileStream

	Private imageBytes3 As Byte = Nothing
	Private FileSize3 As UInt32
	Private rawData3() As Byte
	Private fs3 As FileStream

	Dim arrIdJenisMuatan, arrMetodeTruckScale, arrid_Jenis_alas As New ArrayList
	Dim arrNamaBarang, arrKodeBarang, arrUrutPO As New ArrayList
	Dim selectedBarang, selectedUrutPO As New ArrayList
	Dim arrKdUnikPrint, arrNoFakturRM As New ArrayList

	Dim Lv_NoSJ, Lv_NoPO, Lv_NoFaktur, Lv_NoUrut, Lv_JumlahMasuk, Lv_KodeBarang, lv_NamaBarang As String
	Dim idJenisMuatan As String
	Dim jumlahMasuk As Integer
	Dim Netto As Integer
	Dim totalMasuk As Integer

	Dim itemFaktur As Integer = 0
	Dim itemNoUrut As Integer = 1
	Dim itemKodeBarang As Integer = 2
	Dim itemNoSJ As Integer = 3

	Dim itemNoPO As Integer = 4
	Dim itemNamaBarang As Integer = 5
	Dim itemJmlhMasuk As Integer = 6

	Public filterDetailBarang As String = ""
	Public MetodeTimbang As String = ""

	Dim kode_unik_print As String = ""
	Public WithEvents SerialPort As New SerialPort

	Private TimbanganPorts As New Dictionary(Of String, SerialPort)
	Private TimbanganThreads As New Dictionary(Of String, Thread)
	Private TimbanganPortNames As String() = {Port_Timbangan1, Port_Timbangan2, Port_Timbangan3}
	Private isClosing As Boolean = False
	Private lastCheckedCheckBoxIndex As Integer = -1

	Private Cn_Premix As SqlClient.SqlConnection
	Private Cmd_Premix As SqlClient.SqlCommand
	Private Da_Premix As SqlClient.SqlDataAdapter
	Private Dr_Premix As SqlClient.SqlDataReader
	Private Ds_Premix As DataSet

	Dim SelectedSplit As String = ""
	Dim SelectedBatch As String = ""
	Dim FlagSelesaiSemua As Boolean = False
	Dim RFID_Non_Aktif As Boolean = False

	Private Function CobaKoneksi(portName As String) As Boolean
		Try
			Dim sp As New SerialPort(portName, 9600, Parity.Even, DataBits_Timbangan, StopBits.One)
			sp.Handshake = Handshake.None
			sp.NewLine = vbLf
			sp.Open()
			TimbanganPorts(portName) = sp

			Dim t As New Thread(Sub() BacaDataTimbangan(sp))
			t.IsBackground = True
			TimbanganThreads(portName) = t
			t.Start()
			Return True
		Catch ex As Exception
			MsgBox($"{portName} gagal dibuka: {ex.Message}")
			Return False
		End Try
	End Function

	Private Sub TutupKoneksi(portName As String)
		Try
			If TimbanganThreads.ContainsKey(portName) Then
				Dim t = TimbanganThreads(portName)
				If t.IsAlive Then t.Join(300)
				TimbanganThreads.Remove(portName)
			End If

			If TimbanganPorts.ContainsKey(portName) Then
				Dim sp = TimbanganPorts(portName)
				If sp.IsOpen Then sp.Close()
				sp.Dispose()
				TimbanganPorts.Remove(portName)
			End If
		Catch ex As Exception
			MsgBox($"{portName} gagal ditutup: {ex.Message}")
		End Try
	End Sub

	Private Sub BacaDataTimbangan(sp As SerialPort)
		While Not isClosing
			Try
				If sp.IsOpen AndAlso sp.BytesToRead > 0 Then
					Dim receivedData As String = sp.ReadLine().Trim()
					Dim nilai_data As String = ""
					Dim satuan_berat_data As String = ""

					If receivedData.StartsWith("ST,") Then
						Dim dataPart As String = receivedData.Substring(3).Trim()

						dataPart = dataPart.Replace("?", "").Replace("+", "").Trim()

						If dataPart.ToLower().EndsWith("kg") Then
							satuan_berat_data = "KG"
							dataPart = dataPart.Substring(0, dataPart.Length - 2).Trim()
						ElseIf dataPart.ToLower().EndsWith("g") Then
							satuan_berat_data = "G"
							dataPart = dataPart.Substring(0, dataPart.Length - 1).Trim()
						End If

						dataPart = New String(dataPart.Where(Function(c) Char.IsDigit(c) OrElse c = "."c).ToArray())

						Dim weightValue As Double
						If Double.TryParse(dataPart, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, weightValue) Then
							If satuan_berat_data = "G" Then
								weightValue = Math.Round(weightValue / 1000, 4)
								satuan_berat_data = "KG"
							End If

							nilai_data = weightValue.ToString("N4")
						End If
					End If

					Dim tbIndex As Integer = Array.IndexOf(TimbanganPortNames, sp.PortName)
					Me.Invoke(Sub()
								  TxtOriginal_Data_FloorScale.Text = receivedData
								  Txt_Timbangan.Text = nilai_data.Replace(",", ".")
								  txt_Jumlah_Timbang.Text = nilai_data.Replace(",", ".")
								  TxtSatuan_FloorScale.Text = satuan_berat_data
								  Lb_ActiveTb.Text = $"Timbangan {tbIndex + 1}"
							  End Sub)
				End If
			Catch ex As Exception
				MsgBox($"Error di {sp.PortName}: {ex.Message}")
				Exit While
			End Try
			Thread.Sleep(50)
		End While
	End Sub

	Private Sub ChkTimbangan_CheckedChanged(sender As Object, e As EventArgs) _
	Handles Cb_Tb1.CheckedChanged, Cb_Tb2.CheckedChanged, Cb_Tb3.CheckedChanged

		Dim chk As CheckBox = CType(sender, CheckBox)
		Dim index As Integer = Integer.Parse(chk.Name.Replace("Cb_Tb", "")) - 1
		Dim portName As String = TimbanganPortNames(index)

		If chk.Checked Then
			lastCheckedCheckBoxIndex = index
			For Each ctrl As Control In chk.Parent.Controls
				If TypeOf ctrl Is CheckBox AndAlso ctrl IsNot chk Then
					CType(ctrl, CheckBox).Checked = False
				End If
			Next

			If Not TimbanganPorts.ContainsKey(portName) Then
				If Not CobaKoneksi(portName) Then
					chk.Checked = False
				End If
			End If
		Else
			TutupKoneksi(portName)
		End If
	End Sub

	Private Function GetCheckBoxByIndex(i As Integer) As CheckBox
		Select Case i
			Case 0 : Return Cb_Tb1
			Case 1 : Return Cb_Tb2
			Case 2 : Return Cb_Tb3
			Case Else : Return Nothing
		End Select
	End Function

	Public Sub BukaKoneksiTimbangan()
		SerialPort.PortName = "COM5" 'Port_Timbangan
		SerialPort.BaudRate = BaudRate_Timbangan
		SerialPort.Parity = Parity.Even
		SerialPort.DataBits = DataBits_Timbangan
		SerialPort.StopBits = StopBits.One
		SerialPort.Handshake = Handshake.None
		SerialPort.NewLine = vbLf

		Try
			If SerialPort.IsOpen = False Then
				SerialPort.Open()
				isClosingTimbangan = False
				isErrorTimbangan = False

				' Pastikan thread lama mati sebelum membuat thread baru
				If ReadThread IsNot Nothing AndAlso ReadThread.IsAlive Then
					ReadThread.Join(500) ' Tunggu thread lama berhenti
				End If

				' Jalankan thread baru untuk membaca data
				isClosingTimbangan = False
				ReadThread = New Thread(AddressOf ReadSerialData)
				ReadThread.IsBackground = True
				ReadThread.Start()
			Else
				isErrorTimbangan = False
			End If
		Catch ex As Exception
			isErrorTimbangan = True
		End Try

	End Sub

	Private Sub TutupKoneksiTimbangan()
		isClosingTimbangan = True ' Tandai bahwa form sedang ditutup

		If ReadThread IsNot Nothing AndAlso ReadThread.IsAlive Then
			ReadThread.Join(500) ' Tunggu maksimal 500ms agar thread berhenti
		End If

		If SerialPort.IsOpen Then
			Try
				SerialPort.DiscardInBuffer() ' Hapus buffer agar tidak ada data tertinggal
				SerialPort.Close() ' Tutup port dengan aman
				SerialPort.Dispose() ' Hapus objek SerialPort dari memori
				isErrorTimbangan = False
			Catch ex As Exception
				isErrorTimbangan = True
			End Try
		End If
	End Sub

	Private Sub ReadSerialData()
		While Not isClosingTimbangan
			Try
				If SerialPort.IsOpen AndAlso SerialPort.BytesToRead > 0 Then
					Dim receivedData As String = SerialPort.ReadLine()
					Me.Invoke(Sub()
								  Dim nilai_data As String = Strings.Mid(Trim(receivedData), 7, 99)
								  nilai_data = Strings.Left(nilai_data, Len(nilai_data) - 3)

								  Dim satuan_berat_data As String = Strings.Right(Trim(receivedData), 3)

								  If satuan_berat_data.Trim.ToString.ToUpper <> "KG" Then
									  nilai_data = Math.Round(Val(nilai_data) / 1000, 4)
								  End If

								  Txt_Timbangan.Text = nilai_data
								  TxtOriginal_Data_FloorScale.Text = receivedData

								  If Strings.Left(receivedData, 2) = "ST" Then
									  txt_Jumlah_Timbang.Text = Val(nilai_data)
									  TxtSatuan_FloorScale.Text = "KG"
								  Else
									  txt_Jumlah_Timbang.Text = "0"
									  TxtSatuan_FloorScale.Text = "KG"
								  End If

							  End Sub)
				End If
			Catch ex As Exception
				Exit While ' Keluar dari loop jika terjadi error
			End Try
			Thread.Sleep(50) ' Hindari penggunaan CPU yang berlebihan
		End While
	End Sub

	'Private Sub SerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort.DataReceived
	'    If isClosingTimbangan Then Exit Sub

	'    Try
	'        Dim receivedData As String = SerialPort.ReadLine()
	'        Me.Invoke(Sub()
	'                      Dim nilai_data As String = Strings.Mid(Trim(receivedData), 7, 99)
	'                      nilai_data = Strings.Left(nilai_data, Len(nilai_data) - 3)

	'                      Dim satuan_berat_data As String = Strings.Right(Trim(receivedData), 3)

	'                      Txt_Timbangan.Text = Val(nilai_data)
	'                      TxtOriginal_Data_FloorScale.Text = receivedData

	'                      If Strings.Left(receivedData, 2) = "ST" Then
	'                          txt_Jumlah_Timbang.Text = Val(nilai_data)
	'                          TxtSatuan_FloorScale.Text = satuan_berat_data
	'                      Else
	'                          txt_Jumlah_Timbang.Text = "0"
	'                          TxtSatuan_FloorScale.Text = satuan_berat_data
	'                      End If

	'                      'TextBox1.AppendText(receivedData & Environment.NewLine)
	'                  End Sub)
	'    Catch ex As Exception
	'        MessageBox.Show("Error membaca data: " & ex.Message)
	'    End Try
	'End Sub

	Private Sub get_no_faktur()
		Txt_NoFaktur.Text = fValidasiRMQC & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("N_EMI_Transaksi_Material_Requisition_QC_Validasi", "No_Faktur", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Faktur, 1, " & Len(fValidasiRMQC) + 4 & ")", fValidasiRMQC & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub Transaksi_Timbang_Unloading_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Transaksi_Timbang_Unloading_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
			Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
			'Lbl_Judul.Text = "Validasi - Transfer Request Material Quality Control"
			lblLokasi.Text = "Lokasi"

			'===========================================

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try

		kosong()
		'BukaKoneksiTimbangan()

		For i As Integer = 0 To TimbanganPortNames.Length - 1
			Dim portName = TimbanganPortNames(i)

			If CobaKoneksi(portName) Then
				GetCheckBoxByIndex(i).Checked = True
				Exit For
			Else
				GetCheckBoxByIndex(i).Checked = False
			End If
		Next
	End Sub

	Public Sub kosong()
		Try
			OpenConn()

			get_no_faktur()

			CmbSatuan.Items.Clear()
			SQL = "select satuan from emi_satuan where kode_Perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbSatuan.Items.Add(dr("satuan"))
				Loop
			End Using

			Dim satuan_timbang As String = ""
			SQL = "select Satuan_Timbang from init where kode_Perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					satuan_timbang = dr("Satuan_Timbang")
				End If
			End Using

			CmbJenisAlas.Items.Clear() : arrid_Jenis_alas.Clear()
			SQL = "select Id,Kode_Jenis_Alas,Keterangan,Berat,Satuan, Flag_Default from Emi_Master_Jenis_Alas where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbJenisAlas.Items.Add(dr("Keterangan")) : arrid_Jenis_alas.Add(dr("ID"))

				Loop
			End Using

			SQL = "select Id from Emi_Master_Jenis_Alas where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Default = 'Y' order by Keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read

					CmbJenisAlas.SelectedIndex = arrid_Jenis_alas.IndexOf(dr("ID"))

				Loop
			End Using

			CmbSatuan.Text = "KG"
			CmbSatuan.Enabled = False

			TxtSatuanBerat.Text = "KG"
			TxtSatuanBeratBersih.Text = "KG"
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Txt_Barcode.Text = ""

		KunciSemuaData()
		Txt_Barcode.Focus()

		arrKdUnikPrint.Clear()

		CmbJenisAlas_SelectedIndexChanged(CmbJenisAlas, Nothing)

	End Sub

	Private Sub KosongSebagian()
		Try
			OpenConn()

			get_no_faktur()

			CmbSatuan.Items.Clear()
			SQL = "select satuan from emi_satuan where kode_Perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbSatuan.Items.Add(dr("satuan"))
				Loop
			End Using

			Dim satuan_timbang As String = ""
			SQL = "select Satuan_Timbang from init where kode_Perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					satuan_timbang = dr("Satuan_Timbang")
				End If
			End Using

			CmbJenisAlas.Items.Clear() : arrid_Jenis_alas.Clear()
			SQL = "select Id,Kode_Jenis_Alas,Keterangan,Berat,Satuan from Emi_Master_Jenis_Alas where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbJenisAlas.Items.Add(dr("Keterangan")) : arrid_Jenis_alas.Add(dr("ID"))
				Loop
			End Using

			SQL = "select Id from Emi_Master_Jenis_Alas where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Default = 'Y' order by Keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read

					CmbJenisAlas.SelectedIndex = arrid_Jenis_alas.IndexOf(dr("ID"))

				Loop
			End Using

			CmbJenisAlas_SelectedIndexChanged(CmbJenisAlas, Nothing)

			CmbSatuan.Text = satuan_timbang
			CmbSatuan.Enabled = False

			TxtSatuanBerat.Text = satuan_timbang
			TxtSatuanBeratBersih.Text = satuan_timbang
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		BukaKunciSemuaData()
		LoadDataRM()

		CmbJenisAlas_SelectedIndexChanged(CmbJenisAlas, Nothing)

	End Sub

	Private Sub KunciSemuaData()

		Cmb_Faktur_RM.Items.Clear() : Cmb_Faktur_RM.Text = "" : arrNoFakturRM.Clear()
		'CmbJenisAlas.SelectedIndex = -1 : CmbJenisAlas.Text = ""
		' CmbSatuan.SelectedIndex = -1 : CmbSatuan.Text = ""

		Txt_Timbangan.Text = "0"
		txt_Jumlah_Timbang.Text = "0"
		TxtJumlahBagsDetail.Text = "0"
		Txt_Timbangan.Text = "0"
		TxtOriginal_Data_FloorScale.Text = "0"
		Txt_Berat_Bags_Bersih.Text = ""
		TxtSatuan_FloorScale.Text = ""
		Txt_Bags_Sisa_Bersih.Text = ""
		Txt_Jumlah_Sisa_Bersih.Text = ""
		Txt_JmlhKebutuhan.Text = ""
		Txt_StockBarang.Text = ""
		Txt_StockBagsBarang.Text = ""
		Txt_NmBarang.Text = ""
		Txtsatuanbarang.Text = ""
		Txt_KdSOBarang.Text = ""
		txt_Jml_Estimasi.Text = ""
		TxtJumlahBags.Text = ""
		'TxtBeratAlas.Text = ""
		'TxtBeratAlasBersih.Text = ""
		Txt_Sisa_Jumlah.Text = ""
		Txt_Sisa_Bags.Text = ""
		TxtJumlahBagsDetail.Text = ""
		TxtBeratBersih.Text = ""
		TxtSelisihKebutuhan.Text = ""
		TxtSatuan_FloorScale.Text = ""
		Txt_KDBarang.Text = ""
		Txt_SORequest.Text = ""
		Txt_Urut_DetRM.Text = ""
		TXt_Batch.Text = ""
		Txt_NMBarangProduksi.Text = ""
		Txt_KDBarangProduksi.Text = ""
		TxtBeratBags.Text = ""
		Txt_NoSplitProduction.Text = ""
		Txt_UrutDetail.Text = ""
		PersenToleransiMIN = 0
		PersenToleransiMAX = 0

		CmbJenisAlas.Text = ""
		TxtBeratAlas.Text = ""
		Txt_Sisa_Jumlah.Text = ""
		txt_Jumlah_Timbang.Text = ""
		TxtBeratBersih.Text = ""
		TxtSelisihKebutuhan.Text = ""
		CmbSatuan.Text = ""

		TxtSatuanBerat.Text = ""
		TxtSatuanBeratBersih.Text = ""

		Cmb_Faktur_RM.Enabled = False
		CmbJenisAlas.Enabled = False
		Txt_JmlhKebutuhan.Enabled = False
		Txt_StockBarang.Enabled = False
		Txt_StockBagsBarang.Enabled = False
		Txt_NmBarang.Enabled = False
		Txt_KdSOBarang.Enabled = False
		txt_Jml_Estimasi.Enabled = False
		TxtJumlahBags.Enabled = False
		TxtBeratAlas.Enabled = False
		Txt_Sisa_Jumlah.Enabled = False
		Txt_Sisa_Bags.Enabled = False
		txt_Jumlah_Timbang.Enabled = False
		TxtJumlahBagsDetail.Enabled = False
		TxtBeratBersih.Enabled = False
		TxtOriginal_Data_FloorScale.Enabled = False
		TxtSatuan_FloorScale.Enabled = False
		CmbSatuan.Enabled = False

		CmbJenisAlas_SelectedIndexChanged(CmbJenisAlas, Nothing)
	End Sub

	Private Sub BukaKunciSemuaData()

		Cmb_Faktur_RM.Items.Clear() : Cmb_Faktur_RM.Text = "" : arrNoFakturRM.Clear()
		' CmbJenisAlas.SelectedIndex = -1 : CmbJenisAlas.Text = ""
		'CmbSatuan.SelectedIndex = -1 : CmbSatuan.Text = ""

		Txt_Timbangan.Text = "0"
		txt_Jumlah_Timbang.Text = "0"
		TxtJumlahBagsDetail.Text = "0"
		Txt_Timbangan.Text = "0"
		TxtOriginal_Data_FloorScale.Text = "0"
		Txt_Berat_Bags_Bersih.Text = ""
		TxtSatuan_FloorScale.Text = ""
		Txt_Bags_Sisa_Bersih.Text = ""
		Txt_Jumlah_Sisa_Bersih.Text = ""
		Txt_JmlhKebutuhan.Text = ""
		Txt_StockBarang.Text = ""
		Txt_StockBagsBarang.Text = ""
		Txt_NmBarang.Text = ""
		Txtsatuanbarang.Text = ""
		Txt_KdSOBarang.Text = ""
		txt_Jml_Estimasi.Text = ""
		TxtJumlahBags.Text = ""
		'TxtBeratAlas.Text = ""
		'TxtBeratAlasBersih.Text = ""
		Txt_Sisa_Jumlah.Text = ""
		Txt_Sisa_Bags.Text = ""
		TxtJumlahBagsDetail.Text = ""
		TxtBeratBersih.Text = ""
		TxtSelisihKebutuhan.Text = ""
		TxtSatuan_FloorScale.Text = ""
		Txt_KDBarang.Text = ""
		Txt_SORequest.Text = ""
		Txt_Urut_DetRM.Text = ""
		TXt_Batch.Text = ""
		Txt_NMBarangProduksi.Text = ""
		Txt_KDBarangProduksi.Text = ""
		TxtBeratBags.Text = ""
		Txt_NoSplitProduction.Text = ""
		Txt_UrutDetail.Text = ""
		PersenToleransiMIN = 0
		PersenToleransiMAX = 0

		Cmb_Faktur_RM.Enabled = True
		CmbJenisAlas.Enabled = True
		txt_Jml_Estimasi.Enabled = True
		TxtJumlahBags.Enabled = True
		txt_Jumlah_Timbang.Enabled = True
		TxtJumlahBagsDetail.Enabled = True
		TxtOriginal_Data_FloorScale.Enabled = True
		TxtSatuan_FloorScale.Enabled = True

		CmbJenisAlas_SelectedIndexChanged(CmbJenisAlas, Nothing)
	End Sub

	Private Sub txt_Jumlah_Timbang_Leave(sender As Object, e As EventArgs) Handles txt_Jumlah_Timbang.Leave
		If txt_Jumlah_Timbang.Text.Trim.Length = 0 Then
			TxtJumlahBagsDetail.Text = 0
			Txt_Sisa_Bags.Text = Txt_StockBagsBarang.Text

			Dim berat_net As Double = 0
			berat_net = Val(HilangkanTanda(txt_Jumlah_Timbang.Text)) - (Val(HilangkanTanda(TxtBeratAlasBersih.Text))) '+ Val(HilangkanTanda(TxtBeratBagsBersih.Text)))
			TxtBeratBersih.Text = Format(berat_net, "N4")

			Exit Sub
		End If

		If Not IsNumeric(txt_Jumlah_Timbang.Text) Then
			txt_Jumlah_Timbang.Text = 0
			Exit Sub
		End If

		'If Val(HilangkanTanda(TxtBeratBersih.Text)) > Val(HilangkanTanda(Txt_Sisa.Text)) Then
		'    MessageBox.Show("Jumlah Timbang Tidak Boleh Lebih Dari Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    txt_Jumlah_Timbang.Text = 0
		'End If

		'======================================
		'=     CEK APAKAH LEWAT TOLERANSI     =
		'======================================
		Dim NilaiSisaMIN As Double = Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) - (Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) * (PersenToleransiMIN / 100))
		Dim NilaiSisaMAX As Double = Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) + (Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) * (PersenToleransiMAX / 100))

		Try
			OpenConn()

			'===============================
			'=     GET BERAT PER 1 BAG     =
			'===============================
			Dim BeratIsiPerBag As Double = 0
			Dim SatuanBags As String = ""
			SQL = "select top 1 isnull(Isi_Per_Bags,0) as Isi_Per_Bags, isnull(Satuan_Isi_Bags,'') as Satuan_Isi_Bags from barang "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and Kode_Stock_Owner = '" & Txt_KdSOBarang.Text & "' "
			SQL = SQL & "and kode_barang = '" & Txt_KDBarang.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					BeratIsiPerBag = Dr("Isi_Per_Bags")
					SatuanBags = Dr("Satuan_Isi_Bags")
				End If
			End Using

			'=======================
			'=     UBAH SATUAN     =
			'=======================
			Dim BeratIsiBag As Double = Val(HilangkanTanda(BeratIsiPerBag))
			'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KDBarang.Text & "',"
			'SQL = SQL & "'" & SatuanBags & "','" & CmbSatuan.Text & "',"
			'SQL = SQL & "" & HilangkanTanda(BeratIsiPerBag) & ") as Hasil "
			'Using dr3 = OpenTrans(SQL)
			'    If dr3.Read Then
			'        If General_Class.CekNULL(dr3("Hasil")) <> "" Then
			'            BeratIsiBag = dr3("Hasil")
			'        Else
			'            MessageBox.Show("Satuan " & SatuanBags & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            Exit Sub
			'        End If
			'    End If
			'End Using

			'=====================================
			'=     HITUNG JUMLAH POTONG BAGS     =
			'=====================================
			'Dim BagsTerpakai As Double = Math.Floor(Math.Max(0, Val(HilangkanTanda(TxtBeratBersih.Text))) / BeratIsiBag)
			'TxtJumlahBagsDetail.Text = Format(BagsTerpakai, "N0")
			TxtJumlahBagsDetail.Text = 0

			'============================
			'=     HITUNG SISA BAGS     =
			'============================
			If Val(HilangkanTanda(Txt_StockBagsBarang.Text)) - Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) < 0 Then
				CloseConn()
				MessageBox.Show("Jumlah Bags Tidak Cukup", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				txt_Jumlah_Timbang.Text = 0
				TxtJumlahBagsDetail.Text = 0
				Exit Sub
			Else
				Txt_Sisa_Bags.Text = Val(HilangkanTanda(Txt_StockBagsBarang.Text)) - Val(HilangkanTanda(TxtJumlahBagsDetail.Text))
			End If

			Dim JumlahValidasi As Double = 0
			SQL = "select isnull(sum(Jumlah), 0) as Jumlah from N_EMI_Transaksi_Material_Requisition_QC_Validasi a "
			SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Flag_Retur is null and a.no_Faktur_RM = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
			SQL = SQL & "and a.kode_Stock_Owner_Tujuan = '" & Txt_SORequest.Text & "' "
			SQL = SQL & "and a.kode_barang = '" & Txt_KDBarang.Text & "' "
			SQL = SQL & "and a.Urut_Det_RM = '" & Txt_Urut_DetRM.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					JumlahValidasi = Val(HilangkanTanda(Dr("Jumlah")))
				End If
			End Using

			If Val(HilangkanTanda(TxtBeratBersih.Text)) + JumlahValidasi < NilaiSisaMIN Then

				Dim result As DialogResult = MessageBox.Show("Jumlah Timbang Kurang Dari Sisa, Apakah Ingin Tetap Melanjutkan Proses Penimbangan ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				If result = DialogResult.No Then txt_Jumlah_Timbang.Text = 0

			End If
			If Val(HilangkanTanda(TxtBeratBersih.Text)) + JumlahValidasi > NilaiSisaMAX Then

				MessageBox.Show("Jumlah Timbang Tidak Boleh Lebih Besar Dari Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				txt_Jumlah_Timbang.Text = 0

			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		'Txt_Timbangan.Text = "0"
		kosong()
	End Sub

	Private Sub Txt_Barcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Barcode.KeyPress

		If e.KeyChar = Chr(13) Then

			If Txt_Barcode.Text.Trim.Length = 0 Then
				MessageBox.Show("HArap Scan Barcode Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			BukaKunciSemuaData()

			LoadDataRM()

		End If

	End Sub

	Private Sub LoadDataRM()

		Try
			OpenConn()

			'===============================================
			'=     GET DATA BARANG BERDASARKAN BARCODE     =
			'===============================================
			Dim KdSO As String = ""
			Dim KdBarang As String = ""
			Dim SN As String = ""

			SQL = "select b.Kode_stock_Owner, b.Kode_Barang, a.Serial_Number, b.good_stock, a.Jumlah_Bags, "
			SQL = SQL & " a.Jumlah  as Jumlah_Stock, "
			SQL = SQL & "a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, b.Metode_Pengeluaran_Stok,a.Tgl_Masuk, a.Blok_SN, b.satuan  "
			SQL = SQL & "from barang_sn a, barang b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
			SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			SQL = SQL & "and round(a.Jumlah,4) <> 0 "
			SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & Txt_Barcode.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("Blok_SN")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("SN Pada Pallet di Block, Validasi di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						kosong()
						Exit Sub
					End If

					KdSO = Dr("Kode_stock_Owner")
					KdBarang = Dr("Kode_Barang")
					SN = Dr("Serial_Number")

					Txt_KDBarang.Text = KdBarang
					Txt_NmBarang.Text = Dr("Nama")
					Txt_KdSOBarang.Text = KdSO
					Txt_StockBarang.Text = Format(Val(HilangkanTanda(Dr("Jumlah_Stock"))), "N4")
					Txtsatuanbarang.Text = Dr("satuan")
					Txt_StockBagsBarang.Text = Dr("Jumlah_Bags")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					kosong()
					Exit Sub
				End If
			End Using

			Cmb_Faktur_RM.Items.Clear() : arrNoFakturRM.Clear()
			SQL = "select distinct a.No_Faktur, a.Keterangan, a.no_faktur_order "
			SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c, emi_split_production_order d "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "and b.No_Faktur  = c.No_Faktur and b.Urut_Oto = c.Urut_Detail  "
			SQL = SQL & "and a.kode_perusahaan=d.kode_perusahaan and a.no_faktur_order=d.no_transaksi and d.flag_hasil_produksi_gi is null "
			SQL = SQL & "and b.Flag_Terpenuhi is null and a.Status is null and c.Flag_Terpenuhi is null and a.Flag_Selesai is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and c.Kode_Stock_Owner_Tujuan  = '" & KdSO & "' "
			SQL = SQL & "and c.Kode_Barang = '" & KdBarang & "' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Faktur_RM.Items.Add(Dr("No_Faktur") & " - " & Dr("No_Faktur_order") & " - " & Dr("Keterangan")) : arrNoFakturRM.Add(Dr("No_Faktur"))
				Loop
			End Using

			'===============================
			'=     GET BERAT PER 1 BAG     =
			'===============================
			Dim BeratIsiPerBag As Double = 0
			Dim SatuanBags As String = ""
			SQL = "select top 1 Isi_Per_Bags, Satuan_Isi_Bags from barang "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and Kode_Stock_Owner = '" & Txt_KdSOBarang.Text & "' "
			SQL = SQL & "and kode_barang = '" & Txt_KDBarang.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					BeratIsiPerBag = If(General_Class.CekNULL(Dr("Isi_Per_Bags")) = "", 0, Dr("Isi_Per_Bags"))
					SatuanBags = Dr("Satuan_Isi_Bags")
				End If
			End Using

			'=======================
			'=     UBAH SATUAN     =
			'=======================
			Dim BeratIsiBag As Double = Val(HilangkanTanda(BeratIsiPerBag))
			'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KDBarang.Text & "',"
			'SQL = SQL & "'" & SatuanBags & "','" & CmbSatuan.Text & "',"
			'SQL = SQL & "" & HilangkanTanda(BeratIsiPerBag) & ") as Hasil "
			'Using dr3 = OpenTrans(SQL)
			'    If dr3.Read Then
			'        If General_Class.CekNULL(dr3("Hasil")) <> "" Then
			'            BeratIsiBag = dr3("Hasil")
			'        Else
			'            MessageBox.Show("Satuan " & SatuanBags & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            Exit Sub
			'        End If
			'    End If
			'End Using

			TxtBeratBags.Text = BeratIsiBag & " " & CmbSatuan.Text
			TxtBeratBagsBersih.Text = BeratIsiBag

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Cmb_Faktur_RM.DroppedDown = True
		Cmb_Faktur_RM.Focus()

	End Sub

	Private Sub CmbJenisAlas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJenisAlas.SelectedIndexChanged
		If CmbJenisAlas.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			Dim nberat As Double = 0
			Dim convertKeSatuanAsli_bhn As String = ""
			SQL = "SELECT a.Id, a.Kode_Jenis_Alas, a.Keterangan, a.Berat, a.Satuan, "
			SQL &= "dbo.Ubah_Satuan_Baru(a.Kode_Perusahaan, '" & Txt_KDBarang.Text & "', a.Satuan, 'KG', a.Berat, 'masa') AS Berat2 "
			SQL &= "FROM Emi_Master_Jenis_Alas a "
			SQL &= "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL &= "AND a.Id = '" & arrid_Jenis_alas.Item(CmbJenisAlas.SelectedIndex) & "'"
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					convertKeSatuanAsli_bhn = dr("Satuan")
					'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KDBarang.Text & "',"
					'SQL = SQL & "'" & dr("satuan") & "','" & CmbSatuan.Text & "',"
					'SQL = SQL & "" & HilangkanTanda(dr("Berat")) & ") as Hasil "
					'dr.Close()
					'Using dr4 = OpenTrans(SQL)
					'    If dr4.Read Then
					'        If General_Class.CekNULL(dr4("Hasil")) <> "" Then

					'            nberat = dr4("hasil")
					'            TxtBeratAlas.Text = Format(nberat, "N4") & " " & CmbSatuan.Text
					'            TxtBeratAlasBersih.Text = Format(nberat, "N4")
					'            TxtBeratAlas_Bersih.Text = Format(nberat, "N4")

					'        Else
					'            dr4.Close()
					'            CloseTrans()
					'            CloseConn()
					'            MessageBox.Show("Satuan " & convertKeSatuanAsli_bhn & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					'            Exit Sub
					'        End If
					'    End If
					'End Using

					nberat = Val(HilangkanTanda(dr("Berat")))
					TxtBeratAlas.Text = Format(nberat, "N4") & " " & CmbSatuan.Text
					TxtBeratAlasBersih.Text = Format(nberat, "N4")
					TxtBeratAlas_Bersih.Text = Format(nberat, "N4")
				Else
					dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Dim berat_net As Double = 0
		berat_net = Val(HilangkanTanda(txt_Jumlah_Timbang.Text)) - (Val(HilangkanTanda(TxtBeratAlasBersih.Text))) '+ Val(HilangkanTanda(TxtBeratBagsBersih.Text)))
		TxtBeratBersih.Text = Format(berat_net, "N4")
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Cb_Tb1.Checked Then Cb_Tb1.Checked = False
		If Cb_Tb2.Checked Then Cb_Tb2.Checked = False
		If Cb_Tb3.Checked Then Cb_Tb3.Checked = False

		If CmbJenisAlas.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Alas Belum dipilih . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
				GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
			End If
			Exit Sub
		End If

		If txt_Jumlah_Timbang.Text.Trim.Length = 0 Then
			MessageBox.Show("Jumlah timbang tidak boleh kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
				GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
			End If
			Exit Sub
		End If

		TxtSatuan_FloorScale.Text = "KG"

		If txt_Jumlah_Timbang.Text.Trim.Length = 0 Or Val(txt_Jumlah_Timbang.Text) = 0 Or Val(txt_Jumlah_Timbang.Text) < 0 Then
			MessageBox.Show("Berat Timbang Tidak Boleh Kosong atau 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
				GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
			End If
			Exit Sub
			'ElseIf TxtSatuan_FloorScale.Text.Trim.ToUpper <> CmbSatuan.Text.ToUpper Then
			'    MessageBox.Show("Satuan timbang berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    Exit Sub
			'ElseIf Strings.Left(TxtOriginal_Data_FloorScale.Text.Trim.ToUpper, 2) <> "ST" Then
			'    MessageBox.Show("Terjadi kesalahan pada timbangan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    Exit Sub
		End If

		'TxtJumlahBagsDetail.Text = 2

		get_jam()

		Try

			Dim txOpt As New TransactionOptions With {
				.IsolationLevel = IsolationLevel.Serializable,
				.Timeout = TimeSpan.FromMinutes(3)
			}

			Using scope As New TransactionScope(
		TransactionScopeOption.Required,
		txOpt,
		TransactionScopeAsyncFlowOption.Enabled)

				Try
					OpenConn_Premix()
					Cmd_Premix.Transaction = Cn_Premix.BeginTransaction

					get_no_faktur()

					'=========================
					'=     GET DATA AWAL     =
					'=========================
					Dim QrLama, batchLama, SN_Awal, expDate, tglMsk, metodePengeluaranStock As String

					Dim sw1 As New Stopwatch()
					sw1.Start()

#Region "Get Data Detail Barcode"

					'Ambil Data SN Berdasar Barcode
					SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.tgl_masuk, b.Metode_Pengeluaran_Stok, a.Blok_SN "
					SQL = SQL & "from barang_sn a, barang b "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
					SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
					SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
					SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
					SQL = SQL & "and round(a.Jumlah,4) <> 0 "
					SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & Txt_Barcode.Text & "' "
					Using Dr_Premix = OpenTrans_Premix(SQL)
						If Dr_Premix.Read Then

							If General_Class.CekNULL(Dr_Premix("Blok_SN")) = "Y" Then
								Dr_Premix.Close()
								CloseTrans_Premix()
								CloseConn_Premix()
								MessageBox.Show("Barang Tidak Bisa Di Pakai, Karena SN Telah Di Blok", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
									GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
								End If
								Exit Sub
							End If

							QrLama = General_Class.CekNULL(Dr_Premix("Qr_Code"))
							batchLama = General_Class.CekNULL(Dr_Premix("Batch_Number"))
							SN_Awal = Dr_Premix("serial_number")
							expDate = General_Class.CekNULL(Dr_Premix("Tgl_Expired"))
							tglMsk = General_Class.CekNULL(Dr_Premix("tgl_masuk"))
							metodePengeluaranStock = Dr_Premix("Metode_Pengeluaran_Stok")
						Else
							Dr_Premix.Close()
							CloseTrans_Premix()
							CloseConn_Premix()
							MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
								GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
							End If
							Exit Sub
						End If
					End Using

#End Region

					sw1.Stop()
					SQL = $"
						                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Get Data Detail Barcode', '{sw1.Elapsed.TotalMilliseconds} ms')
						            "
					ExecuteTrans_Premix(SQL)

					'============================
					'=     CEK DATA SN AWAL     =
					'============================
					SQL = "SELECT Kode_Perusahaan from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' AND Serial_Number = '" & SN_Awal & "'"
					Using Dr_Premix = OpenTrans_Premix(SQL)
						If Not Dr_Premix.Read Then
							Dr_Premix.Close()
							CloseTrans_Premix()
							CloseConn_Premix()
							MessageBox.Show("Data SN Awal Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
								GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
							End If
							Exit Sub
						End If
					End Using

					'===============================
					'=     GET BERAT PER 1 BAG     =
					'===============================
					Dim BeratIsiPerBag As Double = 0
					SQL = "select top 1 isnull(Isi_Per_Bags,0) as Isi_Per_Bags from barang "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and Kode_Stock_Owner = '" & Txt_KdSOBarang.Text & "' "
					SQL = SQL & "and kode_barang = '" & Txt_KDBarang.Text & "'"
					Using Dr_Premix = OpenTrans_Premix(SQL)
						If Dr_Premix.Read Then
							BeratIsiPerBag = Dr_Premix("Isi_Per_Bags")
						End If
					End Using

					'===================================
					'=     DELETE DATA TABEL CETAK     =
					'===================================

					Dim sw2 As New Stopwatch()
					sw2.Start()

#Region "Delete Data Tabel Tempt Cetak Barcode"

					Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

					SQL = "delete from N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak where Kode_Perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
					ExecuteTrans_Premix(SQL)

#End Region

					sw2.Stop()
					SQL = $"
						                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Delete Data Tabel Temp Cetak Barcode', '{sw2.Elapsed.TotalMilliseconds} ms')
						            "
					ExecuteTrans_Premix(SQL)

					'=======================================
					'=     CEK APAKAH RM TELAH SELESAI     =
					'=======================================
					SQL = "select a.Kode_Perusahaan, a.No_Faktur, No_Faktur_Order, c.Kode_Stock_Owner, c.Kode_Barang, c.Kebutuhan, b.Batch, c.Jumlah_Per_Batch, c.Jumlah_Tambah, "
					SQL = SQL & "c.Jumlah_Tambah as Jumlah_Tambah_Kecil, "
					SQL = SQL & "c.Jumlah_Barang, c.Satuan, c.Satuan_Barang, C.Urut_Oto, "
					SQL = SQL & "isnull((select sum(z.Jumlah_Barang) from N_EMI_Transaksi_Material_Requisition_QC_Validasi z "
					SQL = SQL & "where a.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Faktur_RM = c.No_Faktur and z.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and z.Flag_Retur is null "
					SQL = SQL & "and c.Kode_Barang = z.Kode_Barang and c.Urut_Oto = z.Urut_Det_RM and z.status is null "
					SQL = SQL & "), 0) as Jumlah_SdhInput "
					SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan 	 "
					SQL = SQL & "and a.No_Faktur = b.No_Faktur "
					SQL = SQL & "and b.No_Faktur  = c.No_Faktur and b.Urut_Oto = c.Urut_Detail  "
					SQL = SQL & "and b.Flag_Terpenuhi is null "
					SQL = SQL & "and c.Flag_Terpenuhi is null "
					SQL = SQL & "and a.Status is null "
					SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
					SQL = SQL & "and c.Kode_Barang  = '" & Txt_KDBarang.Text & "' "
					SQL = SQL & "and c.Kode_Stock_Owner = '" & Txt_SORequest.Text & "' "
					SQL = SQL & "and b.Batch = '" & HilangkanTanda(TXt_Batch.Text) & "' "
					SQL = SQL & "order by b.Batch ASC "
					Using Ds_Premix = BindingTrans_Premix(SQL)
						With Ds_Premix.Tables("MyTable")
							If .Rows.Count <> 0 Then

								Dim JumlahInput As Double = Val(HilangkanTanda(TxtBeratBersih.Text))

								'=====================================
								'=     UBAH MENJADI SATUAN KECIL     =
								'=====================================
								Dim JumlahInputKecil As Double = Val(HilangkanTanda(TxtBeratBersih.Text))
								'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KDBarang.Text & "',"
								'SQL = SQL & "'" & CmbSatuan.Text & "','" & .Rows(0).Item("Satuan_Barang") & "',"
								'SQL = SQL & "" & HilangkanTanda(TxtBeratBersih.Text) & ") as Hasil "
								'Using dr3 = OpenTrans(SQL)
								'    If dr3.Read Then
								'        If General_Class.CekNULL(dr3("Hasil")) <> "" Then
								'            JumlahInputKecil = dr3("Hasil")
								'        Else
								'            MessageBox.Show("Satuan " & CmbSatuan.Text & " Ke " & .Rows(0).Item("Satuan_Barang") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'            Exit Sub
								'        End If
								'    End If
								'End Using

								Dim JumlahInsert As Double = JumlahInputKecil

								Dim sw3 As New Stopwatch()
								sw3.Start()
								Dim JumlahValidasi As Double = 0
								Dim JumlahValidasiBesar As Double = 0
								SQL = "select  sum(isnull(round(a.jumlah,4), 0)) as Jumlah, sum(isnull(round(a.jumlah,4), 0)) as JumlahBesar "
								SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC_Validasi a "
								SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Flag_Retur is null and a.status is null and a.No_Faktur_RM = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
								SQL = SQL & "and a.Kode_Stock_Owner_Tujuan = '" & Txt_SORequest.Text & "' "
								SQL = SQL & "and a.Kode_Barang = '" & Txt_KDBarang.Text & "' and a.Urut_Det_RM = '" & Txt_Urut_DetRM.Text & "' "
								SQL = SQL & "group by a.Kode_Perusahaan, a.Kode_Barang, a.Satuan, a.Satuan_Barang "
								Using Dr_Premix = OpenTrans_Premix(SQL)
									If Dr_Premix.Read Then
										JumlahValidasi = Val(HilangkanTanda(If(General_Class.CekNULL(Dr_Premix("Jumlah")) = "", 0, General_Class.CekNULL(Dr_Premix("Jumlah")))))
										JumlahValidasiBesar = Val(HilangkanTanda(If(General_Class.CekNULL(Dr_Premix("JumlahBesar")) = "", 0, General_Class.CekNULL(Dr_Premix("JumlahBesar")))))
									End If
								End Using

								sw3.Stop()
								SQL = $"
						                            insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                            values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Get Jumlah Sudah Validasi', '{sw3.Elapsed.TotalMilliseconds} ms')
						                        "
								ExecuteTrans_Premix(SQL)

								For i As Integer = 0 To .Rows.Count - 1

									Dim NoSplit As String = Ds_Premix.Tables("MyTable").Rows(i).Item("No_Faktur_Order")

									If JumlahInsert <= 0 Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show("Terjadi Kesalahan Saat Pembagian Batch", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									ElseIf JumlahInsert = 0 Then
										Exit For
									End If

									Dim Batch As String = .Rows(i).Item("Batch")
									Dim JumlahTambah As Double = Val(HilangkanTanda(If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Tambah_Kecil")) = "", 0, .Rows(i).Item("Jumlah_Tambah_Kecil"))))
									Dim JumlahPerDosing As Double = Val(HilangkanTanda(If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Barang")) = "", 0, .Rows(i).Item("Jumlah_Barang")))) + JumlahTambah
									Dim SatuanBesar As String = .Rows(i).Item("Satuan")
									Dim SatuanKecil As String = .Rows(i).Item("Satuan_Barang")
									Dim UrutDetRM As String = .Rows(i).Item("Urut_Oto")

									Dim JumlahInputDB As Double = 0

									'If JumlahInsert > JumlahPerDosing Then
									'    JumlahInputDB = JumlahPerDosing
									'    JumlahInsert -= JumlahPerDosing
									'Else
									'    JumlahInputDB = JumlahInsert
									'    JumlahInsert -= JumlahInsert
									'End If

									JumlahInputDB = JumlahInsert
									'======================================
									'=     CEK APAKAH LEWAT TOLERANSI     =
									'======================================

									'If JumlahInsert < ((JumlahPerDosing - JumlahValidasi) - ((JumlahPerDosing - JumlahValidasi) * (PersenToleransiMIN / 100))) Then
									'    CloseTrans()
									'    CloseConn()
									'    MessageBox.Show("Jumlah Input Tidak Boleh Lebih Dari Kebutuhan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									'    txt_Jumlah_Timbang.Focus()
									'    Exit Sub
									'End If

									If Math.Round(JumlahInsert + JumlahValidasi, 4) > Math.Round(((JumlahPerDosing) + ((JumlahPerDosing) * (PersenToleransiMAX / 100))), 4) Then
										Dim abs As Double = Math.Round(((JumlahPerDosing) + ((JumlahPerDosing) * (PersenToleransiMAX / 100))), 4)
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show("Jumlah Input Tidak Boleh Lebih Dari Kebutuhan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										txt_Jumlah_Timbang.Focus()
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									If Math.Round(JumlahInsert + JumlahValidasi, 4) < Math.Round(((JumlahPerDosing) - ((JumlahPerDosing) * (PersenToleransiMIN / 100))), 4) Then
										'Dim hasil As DialogResult = MessageBox.Show("Jumlah Input Lebih Kecil dari Jumlah Kebutuhan, Apakah Ingin Tetap Simpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

										'If hasil = DialogResult.No Then
										'    CloseTrans_Premix()
										'    CloseConn_Premix()
										'    If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
										'        GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										'    End If
										'    Exit Sub
										'End If
									End If

									If JumlahInsert = 0 Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show("Jumlah Input Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										txt_Jumlah_Timbang.Focus()
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									'=====================================
									'=     HITUNG JUMLAH POTONG BAGS     =
									'=====================================
									'Dim BagsTerpakai As Double = Math.Floor(JumlahInputDB / BeratIsiPerBag)
									'Dim BagsTerpakai As Double = Math.Floor(Val(HilangkanTanda(TxtJumlahBagsDetail.Text)))
									Dim BagsTerpakai As Double = 0

									'==========================
									'=     GET RAK KOSONG     =
									'==========================
									Dim available_Id_Warehouse As String = ""
									Dim available_NoPallet As String = ""
									SQL = "select top 1 id_wms_warehouse_position, 0 as nomor_urut from view_warehouse_position where "
									SQL = SQL & "kode_stock_Owner='" & Txt_SORequest.Text & "' "
									Using Dr2 = OpenTrans_Premix(SQL)
										Do While Dr2.Read
											available_Id_Warehouse = Dr2("id_wms_warehouse_position")
											available_NoPallet = Dr2("nomor_urut")
										Loop
									End Using

									'===========================================
									'=       GET STOCK SEBELUM DIPOTONG       =
									'===========================================
									Dim Stock_SblmPotong As Double = 0
									Dim Stock_SN_SblmPotong As Double = 0
									SQL = "select isnull(sum(Good_Stock), 0) as Stock from barang WHERE kode_perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "AND Kode_Stock_Owner = '" & Txt_KdSOBarang.Text & "' and kode_barang = '" & Txt_KDBarang.Text & "' "
									Using Dr_Premix = OpenTrans_Premix(SQL)
										If Dr_Premix.Read Then
											Stock_SblmPotong = Math.Round(Dr_Premix("Stock"), 4)
										End If
									End Using

									SQL = "select isnull(sum(Jumlah), 0) as Stock_SN from barang_sn WHERE kode_perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "AND Kode_Stock_Owner = '" & Txt_KdSOBarang.Text & "' and kode_barang = '" & Txt_KDBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											Stock_SN_SblmPotong = Math.Round(Dr("Stock_SN"), 4)
										End If
									End Using

									If Stock_SblmPotong <> Stock_SN_SblmPotong Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show($"Jumlah Stock {Txt_KDBarang.Text} pada Gudang {Txt_KdSOBarang.Text} Tidak Sesuai Sebelum Dipotong", Judul,
														MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									'============================
									'=       POTONG STOCK       =
									'============================

#Region "Potong Stock"

									Dim Nama As String = ""
									SQL = "select Nama,round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Txt_KdSOBarang.Text & "' "
									SQL = SQL & "and Kode_Barang='" & Txt_KDBarang.Text & "' "
									Using dr = OpenTrans_Premix(SQL)
										If dr.Read Then
											Nama = dr("nama")
											If dr("good_stock") < JumlahInputDB Then
												dr.Close()
												CloseTrans_Premix()
												CloseConn_Premix()
												MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
												If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
													GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
												End If
												Exit Sub
											ElseIf dr("Jumlah_Bags") < Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) Then
												dr.Close()
												CloseTrans_Premix()
												CloseConn_Premix()
												MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
												If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
													GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
												End If
												Exit Sub
											Else

												Dim jmlhbgs As Double = dr("Jumlah_Bags")
												Dim jmlh1 As Double = dr("good_stock")
												dr.Close()

												Dim sw4 As New Stopwatch()
												sw4.Start()

												SQL = "update barang set Good_Stock = ROUND(Good_Stock - " & JumlahInputDB & ", 4), "
												SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & BagsTerpakai & " "
												SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Txt_KdSOBarang.Text & "' "
												SQL = SQL & " and Kode_Barang='" & Txt_KDBarang.Text & "'"
												ExecuteTrans_Premix(SQL)

												SQL = "insert INTO N_EMI_Log_Transaksi_Request_Material_QC_Validasi (Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, "
												SQL = SQL & "Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
												SQL = SQL & "VALUES ('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
												SQL = SQL & "'POTONG STOCK; Barang', '" & Txt_KdSOBarang.Text & "', '" & Txt_KDBarang.Text & "', '-', "
												SQL = SQL & "'" & jmlh1 & "', '" & jmlhbgs & "', '" & JumlahInputDB & "', '" & BagsTerpakai & "')"
												ExecuteTrans_Premix(SQL)

												sw4.Stop()
												SQL = $"
						                                            insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                                            values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Potong Stock Barang', '{sw4.Elapsed.TotalMilliseconds} ms')
						                                        "
												ExecuteTrans_Premix(SQL)
											End If
										Else
											dr.Close()
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

									SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Txt_KdSOBarang.Text & "' "
									SQL = SQL & "and Kode_Barang='" & Txt_KDBarang.Text & "' "
									SQL = SQL & "and Serial_Number='" & SN_Awal & "'"
									Using dr = OpenTrans_Premix(SQL)
										If dr.Read Then
											If dr("jumlah") < JumlahInputDB Then
												dr.Close()
												CloseTrans_Premix()
												CloseConn_Premix()
												MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
												If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
													GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
												End If
												Exit Sub
											ElseIf dr("Jumlah_Bags") < Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) Then
												dr.Close()
												CloseTrans_Premix()
												CloseConn_Premix()
												MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
												If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
													GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
												End If
												Exit Sub
											Else
												Dim jmlhbags As Double = dr("Jumlah_Bags")
												Dim jmlh1 As Double = dr("jumlah")
												dr.Close()

												Dim sw20 As New Stopwatch()
												sw20.Start()

												SQL = "update barang_sn set jumlah = ROUND(jumlah - " & JumlahInputDB & ", 4) , "
												SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & BagsTerpakai & " "
												SQL = SQL & "where Kode_Stock_Owner='" & Txt_KdSOBarang.Text & "' and Kode_Barang='" & Txt_KDBarang.Text & "' "
												SQL = SQL & "and Serial_Number='" & SN_Awal & "'"
												'ExecuteTrans_Premix(SQL)
												ExecuteTransNew(SQL)

												SQL = "insert INTO N_EMI_Log_Transaksi_Request_Material_QC_Validasi (Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, "
												SQL = SQL & "Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
												SQL = SQL & "VALUES ('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
												SQL = SQL & "'POTONG STOCK; Barang SN', '" & Txt_KdSOBarang.Text & "', '" & Txt_KDBarang.Text & "', '" & SN_Awal & "', "
												SQL = SQL & "'" & jmlh1 & "', '" & jmlhbags & "', '" & JumlahInputDB & "', '" & BagsTerpakai & "')"
												'ExecuteTrans_Premix(SQL)
												ExecuteTransNew(SQL)

												sw20.Stop()
												SQL = $"
						                                            insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                                            values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Potong Stock Barang SN', '{sw20.Elapsed.TotalMilliseconds} ms')
						                                        "
												ExecuteTrans_Premix(SQL)

											End If
										Else
											dr.Close()
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

									'====================================
									'=       CEK KESESUAIAN STOCK       =
									'====================================
									SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
									SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
									SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
									SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
									SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
									SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
									SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Txt_KdSOBarang.Text & "' "
									SQL = SQL & "AND a.Kode_Barang = '" & Txt_KDBarang.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
									SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
									Using Ds1 = BindingTrans_Premix(SQL)
										If Ds1.Tables("MyTable").Rows.Count <> 0 Then
											If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
												CloseTrans_Premix()
												CloseConn_Premix()
												MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
													GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
												End If
												Exit Sub
											End If
										Else
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

#End Region

									'=======================================
									'=     CEK STOCK SETELAH DI POTONG     =
									'=======================================
									Dim Stock_Setelah_Potong As Double = 0
									Dim Stock_SN_Setelah_Potong As Double = 0
									SQL = "select isnull(sum(Good_Stock), 0) as Stock from barang WHERE kode_perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "AND Kode_Stock_Owner = '" & Txt_KdSOBarang.Text & "' and kode_barang = '" & Txt_KDBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											Stock_Setelah_Potong = Math.Round(Dr("Stock"), 4)
										End If
									End Using

									SQL = "select isnull(sum(Jumlah), 0) as Stock_SN from barang_sn WHERE kode_perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "AND Kode_Stock_Owner = '" & Txt_KdSOBarang.Text & "' and kode_barang = '" & Txt_KDBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											Stock_SN_Setelah_Potong = Math.Round(Dr("Stock_SN"), 4)
										End If
									End Using

									If Stock_Setelah_Potong <> Stock_SN_Setelah_Potong Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show($"Jumlah Stock {Txt_KDBarang.Text} pada Gudang {Txt_KdSOBarang.Text} Tidak Sesuai Setelah Di Potong", Judul,
														MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									If Math.Round((Stock_Setelah_Potong + JumlahInputDB), 4) <> Stock_SblmPotong Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show($"Jumlah Stock {Txt_KDBarang.Text} pada Gudang {Txt_KdSOBarang.Text} Tidak Sesuai Berdasarkan Stock DiPotong", Judul,
														MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									If Math.Round((Stock_SN_Setelah_Potong + JumlahInputDB), 4) <> Stock_SN_SblmPotong Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show($"Jumlah Stock SN {Txt_KDBarang.Text} pada Gudang {Txt_KdSOBarang.Text} Tidak Sesuai Berdasarkan Stock DiPotong", Judul,
														MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									'===========================================
									'=       GET STOCK SEBELUM DIINSERT       =
									'===========================================
									Dim Stock_Sebelum_Insert As Double = 0
									Dim Stock_SN_Sebelum_Insert As Double = 0
									Dim Bags_Sebelum_Insert As Double = 0
									Dim Bags_SN_Sebelum_Insert As Double = 0
									SQL = "select isnull(sum(Good_Stock), 0) as Stock, isnull(sum(Jumlah_Bags),0) as Stock_Bags from barang WHERE kode_perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "AND Kode_Stock_Owner = '" & Txt_SORequest.Text & "' and kode_barang = '" & Txt_KDBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											Stock_Sebelum_Insert = Math.Round(Dr("Stock"), 4)
											Bags_Sebelum_Insert = Math.Round(Dr("Stock_Bags"), 4)
										End If
									End Using

									SQL = "select isnull(sum(Jumlah), 0) as Stock_SN, isnull(sum(Jumlah_Bags),0) as Stock_Bags_SN from barang_sn WHERE kode_perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "AND Kode_Stock_Owner = '" & Txt_SORequest.Text & "' and kode_barang = '" & Txt_KDBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											Stock_SN_Sebelum_Insert = Math.Round(Dr("Stock_SN"), 4)
											Bags_SN_Sebelum_Insert = Math.Round(Dr("Stock_Bags_SN"), 4)
										End If
									End Using

									If Stock_Sebelum_Insert <> Stock_SN_Sebelum_Insert Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show($"Jumlah Stock {Txt_KDBarang.Text} pada Gudang {Txt_SORequest.Text} Tidak Sesuai Sebelum Diinsert", Judul,
														MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									'============================
									'=       TAMBAH STOCK       =
									'============================
									Dim sw5 As New Stopwatch()
									sw5.Start()

#Region "Tambah Stock"

#Region "Tambah Stock"

									SQL = "update barang set Good_Stock= Good_Stock + " & JumlahInputDB & ", Jumlah_Bags = Jumlah_Bags + " & BagsTerpakai & " "
									SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Txt_SORequest.Text & "' "
									SQL = SQL & " and Kode_Barang='" & Txt_KDBarang.Text & "'"
									'ExecuteTrans_Premix(SQL)
									ExecuteTransNew(SQL)

									SQL = "insert INTO N_EMI_Log_Transaksi_Request_Material_QC_Validasi (Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, "
									SQL = SQL & "Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
									SQL = SQL & "VALUES ('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
									SQL = SQL & "'TAMBAH STOCK; Barang', '" & Txt_SORequest.Text & "', '" & Txt_KDBarang.Text & "', '-', "
									SQL = SQL & "'" & Stock_Sebelum_Insert & "', '" & Bags_Sebelum_Insert & "', '" & JumlahInputDB & "', '" & BagsTerpakai & "')"
									'ExecuteTrans_Premix(SQL)
									ExecuteTransNew(SQL)

									sw5.Stop()
									SQL = $"
						                                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Tambah Stock Barang', '{sw5.Elapsed.TotalMilliseconds} ms')
						                            "
									ExecuteTrans_Premix(SQL)

									'==============================
									'=       INSERT SN BARU       =
									'==============================

									Dim hargaIsn As String = ""
									Dim namaBarang As String = ""
									Dim warnaLama As String = ""

									'Ambil Data Lama
									SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
									SQL = SQL & "from barang_sn a, barang b "
									SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
									SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
									SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
									SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
									SQL = SQL & "and a.Kode_Stock_Owner='" & Txt_KdSOBarang.Text & "' "
									SQL = SQL & "and a.Kode_Barang ='" & Txt_KDBarang.Text & "' "
									SQL = SQL & "and a.Serial_Number='" & SN_Awal & "' "
									'SQL = SQL & "and a.Jumlah <> 0 "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
											QrLama = General_Class.CekNULL(Dr("Qr_Code"))
											batchLama = General_Class.CekNULL(Dr("Batch_Number"))
											namaBarang = General_Class.CekNULL(Dr("Nama"))
											expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
											warnaLama = General_Class.CekNULL(Dr("warna"))
										Else
											Dr.Close()
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Data SN Awal Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

									'GENERATE SN BARU
									Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
									Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
									Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

									Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

									Dim sw21 As New Stopwatch()
									sw21.Start()
									'INSERT BARANG SN BARU
									SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
									SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN, No_Reservasi) "
									SQL = SQL & "select Kode_Perusahaan, '" & Txt_SORequest.Text & "', Kode_Barang, '" & SN_Baru & "', '" & JumlahInputDB & "', " & BagsTerpakai & ", "
									SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & available_Id_Warehouse & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
									SQL = SQL & "Kode_Unik_Asal, '" & available_NoPallet & "', batch_number, '" & warnaLama & "', Tgl_Masuk, NULL, '" & NoSplit & "' "
									SQL = SQL & "from Barang_SN "
									SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
									SQL = SQL & "and Kode_Stock_Owner='" & Txt_KdSOBarang.Text & "' "
									SQL = SQL & "and Kode_Barang='" & Txt_KDBarang.Text & "' "
									SQL = SQL & "and Serial_Number='" & SN_Awal & "' "
									'ExecuteTrans_Premix(SQL)
									ExecuteTransNew(SQL)

									SQL = "insert INTO N_EMI_Log_Transaksi_Request_Material_QC_Validasi (Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, "
									SQL = SQL & "Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update, No_Reservasi) "
									SQL = SQL & "VALUES ('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
									SQL = SQL & "'TAMBAH STOCK; Barang SN', '" & Txt_SORequest.Text & "', '" & Txt_KDBarang.Text & "', '" & SN_Baru & "', "
									SQL = SQL & "'" & Stock_SN_Sebelum_Insert & "', '" & Bags_SN_Sebelum_Insert & "', '" & JumlahInputDB & "', '" & BagsTerpakai & "', '" & NoSplit & "')"
									'ExecuteTrans_Premix(SQL)
									ExecuteTransNew(SQL)

									sw21.Stop()
									SQL = $"
						                                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Tambah Stock Barang SN', '{sw21.Elapsed.TotalMilliseconds} ms')
						                            "
									ExecuteTrans_Premix(SQL)

									'====================================
									'=       CEK KESESUAIAN STOCK       =
									'====================================
									SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
									SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
									SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
									SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
									SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
									SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
									SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Txt_SORequest.Text & "' "
									SQL = SQL & "AND a.Kode_Barang = '" & Txt_KDBarang.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
									SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
									Using Ds1 = BindingTrans_Premix(SQL)
										If Ds1.Tables("MyTable").Rows.Count <> 0 Then
											If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
												CloseTrans_Premix()
												CloseConn_Premix()
												MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
													GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
												End If
												Exit Sub
											End If
										Else
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

#End Region

									'=======================
									'=     CEK SN BARU     =
									'=======================
									SQL = "SELECT Kode_Perusahaan from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' AND Serial_Number = '" & SN_Baru & "'"
									Using Dr = OpenTrans_Premix(SQL)
										If Not Dr.Read Then
											Dr.Close()
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Data SN Tujuan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

									'=======================================
									'=     CEK STOCK SETELAH DIINSERT     =
									'=======================================
									Dim Stock_Setelah_Insert As Double = 0
									Dim Stock_SN_Setelah_Insert As Double = 0
									SQL = "select isnull(sum(Good_Stock), 0) as Stock from barang WHERE kode_perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "AND Kode_Stock_Owner = '" & Txt_SORequest.Text & "' and kode_barang = '" & Txt_KDBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											Stock_Setelah_Insert = Math.Round(Dr("Stock"), 4)
										End If
									End Using

									SQL = "select isnull(sum(Jumlah), 0) as Stock_SN from barang_sn WHERE kode_perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "AND Kode_Stock_Owner = '" & Txt_SORequest.Text & "' and kode_barang = '" & Txt_KDBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											Stock_SN_Setelah_Insert = Math.Round(Dr("Stock_SN"), 4)
										End If
									End Using

									If Stock_Setelah_Insert <> Stock_SN_Setelah_Insert Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show($"Jumlah Stock {Txt_KDBarang.Text} pada Gudang {Txt_KdSOBarang.Text} Tidak Sesuai Setelah Diinsert", Judul,
														MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									If Math.Round((Stock_Setelah_Insert - JumlahInputDB), 4) <> Stock_Sebelum_Insert Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show($"Jumlah Stock {Txt_KDBarang.Text} pada Gudang {Txt_KdSOBarang.Text} Tidak Sesuai Berdasarkan Stock DiInsert", Judul,
														MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									If Math.Round((Stock_SN_Setelah_Insert - JumlahInputDB), 4) <> Stock_SN_Sebelum_Insert Then
										CloseTrans_Premix()
										CloseConn_Premix()
										MessageBox.Show($"Jumlah Stock SN {Txt_KDBarang.Text} pada Gudang {Txt_KdSOBarang.Text} Tidak Sesuai Berdasarkan Stock DiInsert", Judul,
														MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
											GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
										End If
										Exit Sub
									End If

									SQL = $"
						                                SELECT isnull(sum(Jumlah),0) AS Jumlah, isnull(sum(Jumlah_Bags),0) AS Bags FROM Barang_SN
						                                WHERE Kode_Perusahaan = '{KodePerusahaan}'
						                                AND Kode_Stock_Owner = '{Txt_SORequest.Text}'
						                                and kode_barang = '{Txt_KDBarang.Text}'
						                                AND Serial_Number = '{SN_Baru}'
						                            "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											If Val(HilangkanTanda(Dr("Jumlah"))) <> Val(HilangkanTanda(TxtBeratBersih.Text)) Then
												Dr.Close()
												CloseTrans_Premix()
												CloseConn_Premix()
												MessageBox.Show("Jumlah Insert Tidak Sesuai . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
													GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
												End If
												Exit Sub
											End If
										End If
									End Using

									'======================
									'=       JURNAL       =
									'======================
									Dim sw6 As New Stopwatch()
									sw6.Start()

#Region "JURNAL"

									Dim fRaw_Material_dari As String = ""
									Dim fFinished_Good_dari As String = ""
									Dim fSemi_FG_dari As String = ""
									Dim fScrap_dari As String = ""
									Dim fPackaging_dari As String = ""
									Dim akun_persediaan_dari As String = ""

									Dim fRaw_Material_tujuan As String = ""
									Dim fFinished_Good_tujuan As String = ""
									Dim fSemi_FG_tujuan As String = ""
									Dim fScrap_tujuan As String = ""
									Dim akun_persediaan_tujuan As String = ""
									Dim fPackaging_tujuan As String = ""
									Dim inisial_faktur_dari As String = ""

									SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
									SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Txt_KdSOBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											'akun_persediaan_dari = Dr("persediaan")
											inisial_faktur_dari = Dr("inisial_faktur")
										Else
											Dr.Close()
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

									SQL = "select c.akun_Persediaan "
									SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
									SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
									SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
									SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "and b.kode_stock_owner = '" & Txt_KdSOBarang.Text & "' and b.Kode_Barang='" & Txt_KDBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											akun_persediaan_dari = Dr("akun_Persediaan")
										Else
											Dr.Close()
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

									SQL = "select c.akun_Persediaan "
									SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
									SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
									SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
									SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "and b.kode_stock_owner = '" & Txt_SORequest.Text & "' and b.Kode_Barang='" & Txt_KDBarang.Text & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											akun_persediaan_tujuan = Dr("akun_Persediaan")
										Else
											Dr.Close()
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

									Dim nilai_persediaan_min As Double = 0
									SQL = "select round(dbo.get_hpp(serial_number) * " & JumlahInputDB & ", 2) as rp_persediaan_min from barang_sn where "
									SQL = SQL & "Kode_Stock_Owner='" & Txt_KdSOBarang.Text & "' and Kode_Barang='" & Txt_KDBarang.Text & "' "
									SQL = SQL & "and Serial_Number='" & SN_Awal & "'"
									Using dr = OpenTrans_Premix(SQL)
										If dr.Read Then
											nilai_persediaan_min = dr("rp_persediaan_min")
										Else
											dr.Close()
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
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
									SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & Txt_NoFaktur.Text & "', '', "
									SQL = SQL & "'-', '" & UserID & "')"
									ExecuteTrans_Premix(SQL)

									SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
										  Strings.Mid(akun_persediaan_dari, 2, 1),
										  Strings.Mid(Ganti(akun_persediaan_dari), 3),
										  KodePerusahaan, KodeProyek, "Persedian " & Txt_NoFaktur.Text, "0", nilai_persediaan_min, pagenumber, Txt_KdSOBarang.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
									ExecuteTrans_Premix(SQL)
									pagenumber = pagenumber + 1

									SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
										 Strings.Mid(akun_persediaan_tujuan, 2, 1),
										 Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
										 KodePerusahaan, KodeProyek, "Persedian " & Txt_NoFaktur.Text, nilai_persediaan_min, "0", pagenumber, Txt_SORequest.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
									ExecuteTrans_Premix(SQL)
									pagenumber = pagenumber + 1

									SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
									SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
									SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											If Dr("debit") <> Dr("kredit") Then
												Dr.Close()
												CloseTrans_Premix()
												CloseConn_Premix()
												MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
													GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
												End If
												Exit Sub
											End If
										Else
											Dr.Close()
											CloseTrans_Premix()
											CloseConn_Premix()
											MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
												GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
											End If
											Exit Sub
										End If
									End Using

#End Region

									sw6.Stop()
									SQL = $"
						                                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Jurnal', '{sw6.Elapsed.TotalMilliseconds} ms')
						                            "
									ExecuteTrans_Premix(SQL)

									'=====================================
									'=     UBAH MENJADI SATUAN BESAR     =
									'=====================================
									Dim JumlahBesar As Double = Val(HilangkanTanda(JumlahInputDB))
									'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KDBarang.Text & "',"
									'SQL = SQL & "'" & SatuanKecil & "','" & SatuanBesar & "',"
									'SQL = SQL & "" & HilangkanTanda(JumlahInputDB) & ") as Hasil "
									'Using dr3 = OpenTrans(SQL)
									'    If dr3.Read Then
									'        If General_Class.CekNULL(dr3("Hasil")) <> "" Then
									'            JumlahBesar = dr3("Hasil")
									'        Else
									'            CloseTrans()
									'            CloseConn()
									'            MessageBox.Show("Satuan " & CmbSatuan.Text & " Ke " & .Rows(0).Item("Satuan_Barang") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									'            Exit Sub
									'        End If
									'    End If
									'End Using

									'====================================
									'=       INSERT DATA VALIDASI       =
									'====================================
									Dim sw7 As New Stopwatch()
									sw7.Start()

#Region "Insert Data Validasi"

									Dim rnd As New Random()
									Dim TextBarcodePSS As String = ""

									For k As Integer = 1 To 10
										Dim randomChar As Char = Chr(rnd.Next(65, 91)) ' ASCII 65–90 = A–Z
										TextBarcodePSS &= randomChar
									Next
									SQL = "INSERT INTO N_EMI_Transaksi_Material_Requisition_QC_Validasi "
									SQL = SQL & "(Kode_Perusahaan, No_Faktur, No_Faktur_RM, Tanggal, Jam, Kode_Stock_Owner, Kode_Stock_Owner_Tujuan, Kode_Barang, SN_Lama, SN_Baru, Jumlah, "
									SQL = SQL & "Satuan, Jumlah_Barang, Satuan_Barang, Kode_Voucher, Urut_Det_RM, Barcode_PSS, Jumlah_Bags) "
									SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
									SQL = SQL & "'" & Txt_KdSOBarang.Text & "', '" & Txt_SORequest.Text & "', '" & Txt_KDBarang.Text & "', '" & SN_Awal & "', '" & SN_Baru & "', " & JumlahBesar & ", "
									SQL = SQL & "'" & SatuanBesar & "', " & JumlahInputDB & ", '" & SatuanKecil & "', '" & Kode_voucher & "', '" & UrutDetRM & "', '" & TextBarcodePSS & "', '" & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & "'); "
									ExecuteTransNew(SQL)

#End Region

									sw7.Stop()
									SQL = $"
						                                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Insert Data Validasi Premix', '{sw7.Elapsed.TotalMilliseconds} ms')
						                            "
									ExecuteTrans_Premix(SQL)

									'=====================================
									'=       GENERATE BARCODE BARU       =
									'=====================================
									Dim sw8 As New Stopwatch()
									sw8.Start()

#Region "Generate Barcode dan Barcode PSS"

									kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
									Dim fullNewQr As String = QrLama & "-" & newKodeUnikBerjalan

									Cmd_Premix.Parameters.Clear()
									Using ImgBarcode1 As Image = Generate_QR_QC(fullNewQr)
										Using ms1 As New MemoryStream()
											ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
											Dim rawData1 As Byte() = ms1.ToArray()

											Dim param1 As String = "@newBarcode" & kode_unik_print
											Cmd_Premix.Parameters.Add(param1, SqlDbType.Image).Value = rawData1
										End Using
									End Using

									Using ImgBarcode2 As Image = Generate_QR_QC(TextBarcodePSS)
										Using ms2 As New MemoryStream()
											ImgBarcode2.Save(ms2, Imaging.ImageFormat.Jpeg)
											Dim rawData2 As Byte() = ms2.ToArray()

											Dim param2 As String = "@newBarcodePSS" & kode_unik_print
											Cmd_Premix.Parameters.Add(param2, SqlDbType.Image).Value = rawData2
										End Using
									End Using

									Dim barcode As String = "@newBarcode" & kode_unik_print
									Dim barcodePSS As String = "@newBarcodePSS" & kode_unik_print

#Region "Kode Lama"

									'Barcode.Image = Generate_QR_QC(fullNewQr)
									'Barcode_PSS.Image = Generate_QR_QC(TextBarcodePSS)

									'Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeValidasiRMQC" & kode_unik_print & ".jpg")
									'Dim FileToSaveAs2 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeValidasiRMQCPSS" & kode_unik_print & ".jpg")
									''If Not (System.IO.File.Exists(FileToSaveAs1)) Then
									'Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
									'Barcode_PSS.Image.Save(FileToSaveAs2, System.Drawing.Imaging.ImageFormat.Jpeg)
									''End If

									'fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
									'FileSize1 = fs1.Length
									'rawData1 = New Byte(FileSize1) {}
									'fs1.Read(rawData1, 0, FileSize1)
									'fs1.Close()
									'Cmd.Parameters.Add("@newBarcode" & kode_unik_print, SqlDbType.Image).Value = rawData1

									'fs2 = New FileStream(FileToSaveAs2, FileMode.Open, FileAccess.Read)
									'FileSize2 = fs2.Length
									'rawData2 = New Byte(FileSize2) {}
									'fs2.Read(rawData2, 0, FileSize2)
									'fs2.Close()
									'Cmd.Parameters.Add("@newBarcodePSS" & kode_unik_print, SqlDbType.Image).Value = rawData2

#End Region

									'==================================
									'=       GET URUTAN BARCODE       =
									'==================================
									Dim UrutBarcode As Double = 0
									SQL = "select count(*) as urut from N_EMI_Transaksi_Material_Requisition_QC_Validasi "
									SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Retur is null and status is null and No_Faktur_RM = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
									SQL = SQL & "and Kode_Barang = '" & Txt_KDBarang.Text & "' and Urut_Det_RM = '" & UrutDetRM & "' "
									Using Dr = OpenTrans_Premix(SQL)
										If Dr.Read Then
											If Val(Dr("urut")) = 0 Then
												UrutBarcode = 1
											Else

												UrutBarcode = Dr("urut")
											End If
											Dr.Close()
										End If
									End Using

									'===================================
									'=       INSERT BARCODE BARU       =
									'===================================
									SQL = "INSERT INTO N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak "
									SQL = SQL & "(Kode_Perusahaan, Barcode, Barcode_PSS, Kode_Bahan, NamaBahan, NamaBarang, Tgl_Input, Jam_Input, Batch_Number, Jumlah_Input, Satuan_Input, "
									SQL = SQL & "No, Dari, QrUtuh, Qr, Tanggal_Cetak, Kode_Unik_Print, No_Split, Batch) "
									SQL = SQL & "VALUES('" & KodePerusahaan & "', " & barcode & ", " & barcodePSS & ",'" & Txt_KDBarang.Text & "', '" & Txt_NmBarang.Text & "', "
									SQL = SQL & "'" & Txt_NMBarangProduksi.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & batchLama & "', "
									SQL = SQL & "" & HilangkanTanda(JumlahBesar) & ", '" & SatuanBesar & "', " & UrutBarcode & ", 0, "
									SQL = SQL & "'" & fullNewQr & "', '" & QrLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & kode_unik_print & "', "
									SQL = SQL & "'" & Txt_NoSplitProduction.Text & "', '" & TXt_Batch.Text & "'); "
									ExecuteTrans_Premix(SQL)

									arrKdUnikPrint.Add(kode_unik_print)

#End Region

									sw8.Stop()
									SQL = $"
						                                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Generate Barcode Baru dan Barcode PSS', '{sw8.Elapsed.TotalMilliseconds} ms')
						                            "
									ExecuteTrans_Premix(SQL)

									'==========================================
									'=       CEK APAKAH SUDAH TERPENUHI       =
									'==========================================
									Dim sw9 As New Stopwatch()
									sw9.Start()

#Region "Pengecekan RM Terpenuhi"

									Dim JumlahSdhValidasi As Double = 0
									SQL = "select isnull(sum(Jumlah), 0) as Jumlah from N_EMI_Transaksi_Material_Requisition_QC_Validasi a "
									SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Flag_Retur is null and a.no_Faktur_RM = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
									SQL = SQL & "and a.kode_Stock_Owner_Tujuan = '" & Txt_SORequest.Text & "' "
									SQL = SQL & "and a.kode_barang = '" & Txt_KDBarang.Text & "' "
									SQL = SQL & "and a.Urut_Det_RM = '" & UrutDetRM & "' "
									Using Ds1 = BindingTrans_Premix(SQL)

										If Ds1.Tables("MyTable").Rows.Count <> 0 Then

											'===============================================
											'=       GET JUMLAH MAX DOSING PER BATCH       =
											'===============================================
											Dim JumlahMAXPerBatch As Double = 0
											Dim JumlahMAXTambah As Double = 0
											SQL = "select Jumlah_Per_Batch, isnull(Jumlah_Tambah, 0) as Jumlah_Tambah from N_EMI_Transaksi_Material_Requisition_QC_Det "
											SQL = SQL & "where No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' and Urut_Oto = '" & UrutDetRM & "' "
											Using Dr = OpenTrans_Premix(SQL)
												If Dr.Read Then
													JumlahMAXPerBatch = Dr("Jumlah_Per_Batch")
													JumlahMAXTambah = Dr("Jumlah_Tambah")
												Else
													Dr.Close()
													CloseTrans_Premix()
													CloseConn_Premix()
													MessageBox.Show("Data Request Det Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
														GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
													End If
													Exit Sub
												End If
											End Using

											JumlahSdhValidasi = Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah")))

											Dim SisaInput As Double = Format((Val(HilangkanTanda(Txt_JmlhKebutuhan.Text))) - Val(HilangkanTanda(JumlahSdhValidasi)), "N4")

											Dim NilaiToleransiMIN As Double = (JumlahMAXPerBatch + JumlahMAXTambah) - ((JumlahMAXPerBatch + JumlahMAXTambah) * (PersenToleransiMIN / 100))
											Dim NilaiToleransiMAX As Double = (JumlahMAXPerBatch + JumlahMAXTambah) + ((JumlahMAXPerBatch + JumlahMAXTambah) * (PersenToleransiMAX / 100))

											'If JumlahSdhValidasi < NilaiToleransiMIN Then
											'    CloseTrans()
											'    CloseConn()
											'    MessageBox.Show("Jumlah Validasi Kurang Dari Toleransi Minimum", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											'    Exit Sub
											'End If

											If JumlahSdhValidasi > NilaiToleransiMAX Then
												CloseTrans_Premix()
												CloseConn_Premix()
												MessageBox.Show("Jumlah Validasi Melebihi Dari Toleransi Maximum", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
													GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
												End If
												Exit Sub
											End If

											If JumlahSdhValidasi >= NilaiToleransiMIN And JumlahSdhValidasi <= NilaiToleransiMAX Then
												SQL = "Update N_EMI_Transaksi_Material_Requisition_QC_Det Set "
												SQL = SQL & "Flag_Terpenuhi = 'Y' "
												SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
												SQL = SQL & "and Kode_Barang = '" & Txt_KDBarang.Text & "' and Urut_Oto = '" & UrutDetRM & "' "
												ExecuteTrans_Premix(SQL)

											End If

										End If
									End Using

#End Region

									sw9.Stop()
									SQL = $"
						                                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Pengecekan Rm Terpenuhi?', '{sw9.Elapsed.TotalMilliseconds} ms')
						                            "
									ExecuteTrans_Premix(SQL)

								Next
							Else
								CloseTrans_Premix()
								CloseConn_Premix()
								MessageBox.Show("Data Request Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
									GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
								End If
								Exit Sub
							End If
						End With
					End Using

					'============================================
					'=     CEK APAKAH BATCH SUDAH TERPENUHI     =
					'============================================
					Dim sw10 As New Stopwatch()
					sw10.Start()

#Region "Pengecekan Apakah Batch Terpenuhi"

					SQL = "select Kode_Perusahaan from N_EMI_Transaksi_Material_Requisition_QC_Detail "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "'  "
					SQL = SQL & "and Urut_Oto = '" & Txt_UrutDetail.Text & "' and Flag_Terpenuhi is null "
					Using Ds = BindingTrans_Premix(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then

								SQL = "select isnull(( select top 1 'T' "
								SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c "
								SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
								SQL = SQL & "and a.No_Faktur = b.No_Faktur "
								SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
								SQL = SQL & "and a.Status is null and c.Flag_Terpenuhi is null "
								SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								SQL = SQL & "and a.No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
								SQL = SQL & "and c.Urut_Detail = '" & Txt_UrutDetail.Text & "' "
								SQL = SQL & "), 'Y') as selesai "
								Using Dr = OpenTrans_Premix(SQL)
									If Dr.Read Then
										If Dr("selesai") = "Y" Then

											Dr.Close()

											'=====================================
											'=       GENERATE BARCODE BATCH       =
											'=====================================
											Dim sw11 As New Stopwatch()
											sw11.Start()

#Region "generate Barcode Batch"

											Dim rnd As New Random()
											Dim TextBarcodeBatch As String = ""

											For k As Integer = 1 To 10
												Dim randomChar As Char = Chr(rnd.Next(65, 91)) ' ASCII 65–90 = A–Z
												TextBarcodeBatch &= randomChar
											Next

											kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
											Dim fullNewQr As String = TextBarcodeBatch

											Cmd_Premix.Parameters.Clear()
											Using ImgBarcode1 As Image = Generate_QR_QC(fullNewQr)
												Using ms1 As New MemoryStream()
													ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
													Dim rawData1 As Byte() = ms1.ToArray()

													Dim param1 As String = "@newBarcodeBatch" & kode_unik_print
													Cmd_Premix.Parameters.Add(param1, SqlDbType.Image).Value = rawData1
												End Using
											End Using

											Dim barcodeBatch As String = "@newBarcodeBatch" & kode_unik_print

											SQL = "update N_EMI_Transaksi_Material_Requisition_QC_Detail set Flag_Terpenuhi = 'Y', "
											SQL = SQL & "Qr_Code = '" & fullNewQr & "', Barcode_Batch = " & barcodeBatch & " "
											SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
											SQL = SQL & "and No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' and Urut_Oto = '" & Txt_UrutDetail.Text & "'  "
											ExecuteTrans_Premix(SQL)

#End Region

											sw11.Stop()
											SQL = $"
						                                        insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                                        values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Generate Barcode Batch', '{sw11.Elapsed.TotalMilliseconds} ms')
						                                    "
											ExecuteTrans_Premix(SQL)

#Region "Generate Lama"

											'Dim rnd As New Random()
											'Dim TextBarcodeBatch As String = ""

											'For k As Integer = 1 To 10
											'    Dim randomChar As Char = Chr(rnd.Next(65, 91)) ' ASCII 65–90 = A–Z
											'    TextBarcodeBatch &= randomChar
											'Next

											'kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
											'Dim fullNewQr As String = TextBarcodeBatch

											'Barcode_Batch.Image = Generate_QR_QC(fullNewQr)

											'Dim FileToSaveAs3 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeValidasiBatch" & kode_unik_print & ".jpg")

											''If Not (System.IO.File.Exists(FileToSaveAs1)) Then
											'Barcode_Batch.Image.Save(FileToSaveAs3, System.Drawing.Imaging.ImageFormat.Jpeg)
											''End If

											'fs3 = New FileStream(FileToSaveAs3, FileMode.Open, FileAccess.Read)
											'FileSize3 = fs3.Length
											'rawData3 = New Byte(FileSize3) {}
											'fs3.Read(rawData3, 0, FileSize3)
											'fs3.Close()
											'Cmd.Parameters.Add("@newBarcodeBatch" & kode_unik_print, SqlDbType.Image).Value = rawData3

#End Region

										End If
									End If
								End Using
							Else
								CloseTrans_Premix()
								CloseConn_Premix()
								MessageBox.Show("Terjadi Kesalahan, Data Request Detail Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
									GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
								End If
								Exit Sub
							End If
						End With
					End Using

#End Region

					sw10.Stop()
					SQL = $"
						                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Pengecekan Batch Terpenuhi?', '{sw10.Elapsed.TotalMilliseconds} ms')
						            "
					ExecuteTrans_Premix(SQL)

					FlagSelesaiSemua = False

					Dim sw12 As New Stopwatch()
					sw12.Start()

#Region "Apakah RM Sudah Selesai Semua?"

					SQL = "select a.Kode_Perusahaan, a.No_Faktur_Order from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
					SQL = SQL & "and a.No_Faktur = b.No_Faktur "
					SQL = SQL & "and a.Status is null "
					SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
					Using Ds = BindingTrans_Premix(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								SelectedSplit = Ds.Tables("MyTable").Rows(0).Item("No_Faktur_Order")

								SQL = "select a.Kode_Perusahaan from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b "
								SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
								SQL = SQL & "and a.No_Faktur = b.No_Faktur "
								SQL = SQL & "and a.Status is null and b.Flag_Terpenuhi is null "
								SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								SQL = SQL & "and a.No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
								Using Dr = OpenTrans_Premix(SQL)
									If Not Dr.Read Then

										Dr.Close()

										SQL = "update N_EMI_Transaksi_Material_Requisition_QC set Flag_Selesai = 'Y' "
										SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and  No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
										ExecuteTrans_Premix(SQL)

										FlagSelesaiSemua = True

									End If
								End Using
							Else
								CloseTrans_Premix()
								CloseConn_Premix()
								MessageBox.Show("Terjadi Kesalahan, Data Request Detail Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
									GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
								End If
								Exit Sub
							End If
						End With
					End Using

#End Region

					sw12.Stop()
					SQL = $"
						                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
						                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Pengecekan Apakah RM Selesai Semua?', '{sw12.Elapsed.TotalMilliseconds} ms')
						            "
					ExecuteTrans_Premix(SQL)

					Cmd_Premix.Transaction.Commit()

					' ✅ COMMIT SEBENARNYA
					scope.Complete()

					MessageBox.Show("Data Berhasil Di Simpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
				Catch ex As Exception
					CloseTrans_Premix()
					CloseConn_Premix()
					MessageBox.Show(ex.Message)
					If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
						GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
					End If
					Exit Sub

					Throw
				Finally
					CloseConn()
				End Try

			End Using

#End Region

		Catch ex As Exception
			MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

		'If FlagSelesaiSemua Then
		'    ' Cek apakah sudah ada pairing PREMIX
		'    Dim SudahPairing As Boolean = False
		'    Try
		'        OpenConn()

		'        SQL = $"
		'            SELECT TOP 1 1
		'            FROM N_EMI_Transaksi_Material_Requisition_QC a
		'            JOIN N_EMI_Transaksi_Material_Requisition_QC_Detail b
		'                ON a.Kode_Perusahaan = b.Kode_Perusahaan
		'                AND a.No_Faktur = b.No_Faktur
		'            WHERE a.No_Faktur_Order = '{SelectedSplit}'
		'              AND b.Batch = '{TXt_Batch.Text}'
		'              AND b.Flag_Sudah_Pairing_Premix = 'Y'
		'        "

		'        Using Dr = OpenTrans(SQL)
		'            If Dr.Read() Then SudahPairing = True
		'        End Using

		'        CloseConn()

		'    Catch ex As Exception
		'        CloseConn()
		'        MessageBox.Show(ex.Message)
		'        Exit Sub
		'    End Try

		'    'Cek apakah RFID PREMIX non-aktif
		'    Try
		'        OpenConn()

		'        SQL = $"select Flag_Premix_RFID_Mati, Tag_RFID_Default_Premix from Init where Kode_Perusahaan = '{KodePerusahaan}'"
		'        Using Dr = OpenTrans(SQL)
		'            If Dr.Read Then
		'                If General_Class.CekNULL(Dr("Flag_Premix_RFID_Mati")) = "Y" Then
		'                    If General_Class.CekNULL(Dr("Tag_RFID_Default_Premix")) = "" Then
		'                        Dr.Close()
		'                        CloseConn()
		'                        MessageBox.Show("Terjadi Kesalahan, RFID Default Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
		'                        Exit Sub
		'                    End If
		'                    RFID_Non_Aktif = True

		'                Else
		'                    RFID_Non_Aktif = False
		'                End If
		'            Else
		'                Dr.Close()
		'                CloseConn()
		'                MessageBox.Show("Terjadi Kesalahan, Data Init Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
		'                Exit Sub
		'            End If
		'        End Using

		'        CloseConn()
		'    Catch ex As Exception
		'        CloseConn()
		'        MessageBox.Show(ex.Message)
		'        Exit Sub
		'    End Try

		'    If SudahPairing Then
		'        CetakLabelPremix()
		'    ElseIf RFID_Non_Aktif Then
		'        ' Belum pairing tapi RFID non-aktif, auto buat pairing dummy lalu cetak
		'        get_jam()

		'        Try
		'            OpenConn()

		'            Dim defaultTag As String = ""
		'            SQL = $"SELECT Tag_RFID_Default_Premix FROM Init WHERE Kode_Perusahaan = '{KodePerusahaan}'"
		'            Using Dr = OpenTrans(SQL)
		'                If Dr.Read() Then defaultTag = General_Class.CekNULL(Dr("Tag_RFID_Default_Premix"))
		'            End Using

		'            If defaultTag = "" Then
		'                CloseConn()
		'                MessageBox.Show("Tag RFID Default tidak ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
		'                Exit Sub
		'            End If

		'            Dim urutTerakhir As Integer = 0
		'            SQL = $"
		'                SELECT ISNULL(MAX(Urut_Pairing), 0)
		'                FROM N_EMI_Pairing_RFID_Log
		'                WHERE No_Split_Production_Order = '{SelectedSplit}'
		'                  AND Batch = '{TXt_Batch.Text}'
		'            "
		'            Using Dr = OpenTrans(SQL)
		'                If Dr.Read() Then urutTerakhir = Val(Dr(0))
		'            End Using

		'            urutTerakhir += 1
		'            SQL = $"
		'                INSERT INTO N_EMI_Pairing_RFID_Log
		'                (
		'                    Kode_Perusahaan,
		'                    No_Split_Production_Order,
		'                    Kode_Stock_Owner,
		'                    RFID_Tag,
		'                    Tanggal_Pairing,
		'                    Jam_Pairing,
		'                    UserID_Pairing,
		'                    Lokasi_Pairing,
		'                    Urut_Pairing,
		'                    Batch
		'                )
		'                VALUES
		'                (
		'                    '{KodePerusahaan}',
		'                    '{SelectedSplit}',
		'                    'PREMIX',
		'                    '{defaultTag}',
		'                    '{Format(tgl_skg, "yyyy-MM-dd")}',
		'                    '{Format(tgl_skg, "HH:mm:ss")}',
		'                    '{UserID}',
		'                    'PREMIX',
		'                    '{urutTerakhir}',
		'                    '{TXt_Batch.Text}'
		'                )
		'            "
		'            ExecuteTrans(SQL)

		'            CloseConn()

		'            MessageBox.Show("Pairing RFID dummy berhasil dibuat otomatis.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		'        Catch ex As Exception
		'            CloseConn()
		'            MessageBox.Show(ex.Message)
		'            Exit Sub
		'        End Try

		'        CetakLabelPremix()

		'    Else
		'        ' Belum pairing, RFID aktif — wajib pairing dulu
		'        MessageBox.Show(
		'            "Semua batch telah selesai namun belum dilakukan pairing RFID PREMIX. Silakan lakukan pairing terlebih dahulu.",
		'            Judul,
		'            MessageBoxButtons.OK,
		'            MessageBoxIcon.Warning
		'        )
		'        Dim frm As New N_EMI_SD_Pairing_RFID_Premix
		'        frm.SelectedSplit = SelectedSplit
		'        frm.SelectedBatch = TXt_Batch.Text
		'        frm.ShowDialog()
		'    End If
		'End If

#Region "BAGIAN CETAK BARCODE"

		CetakBarcode()
		'CetakLabelPremix()

		Dim result As DialogResult
		result = MessageBox.Show("Apakah Ingin Melanjutkan Proses Penimbangan ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

		If result = DialogResult.Yes Then
			KosongSebagian()
		Else
			'CetakBarcode()
			kosong()
		End If

#End Region

		If lastCheckedCheckBoxIndex >= 0 AndAlso lastCheckedCheckBoxIndex <= 2 Then
			GetCheckBoxByIndex(lastCheckedCheckBoxIndex).Checked = True
		End If

	End Sub

	Private Sub CetakBarcode()
		Try
			OpenConn()

			'=================================
			'=     CETAK FAKTUR TF STOCK     =
			'=================================
			Dim CrDoc As New Object
			Dim kertas As String = ""

			'=================================
			'=     CETAK FAKTUR BARCODE     =
			'=================================
			If arrKdUnikPrint.Count <> 0 Then

				Dim kertasBarcode As String = ""
				kertasBarcode = "BarcodeQC"

				Dim sw13 As New Stopwatch()
				sw13.Start()

				For i As Integer = 0 To arrKdUnikPrint.Count - 1
					SQL = "select Kode_Perusahaan from N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Unik_Print='" & arrKdUnikPrint(i) & "'"
					Using Ds = BindingTrans(SQL)
						If Ds.Tables("MyTable").Rows.Count <> 0 Then

							CrDoc = New N_EMI_CR_Transaksi_Request_Material_QC_Barcode
							CrDoc.SetDataSource(Ds)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.RecordSelectionFormula = "{N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Unik_Print} = '" & arrKdUnikPrint(i) & "' "

							CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

							Dim doctoprint As New System.Drawing.Printing.PrintDocument()
							doctoprint.PrinterSettings.PrinterName = PrinterBarcodeQC

							Dim rawKind As Integer
							CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
							For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
								If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
									rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
									CrDoc.PrintOptions.PaperSize = rawKind
									Exit For
								End If
							Next

							If rawKind = Nothing Or rawKind = 0 Then
								CloseConn()
								MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Kertas Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							CrDoc.PrintToPrinter(1, False, 1, 2500)

						End If
					End Using
				Next

				sw13.Stop()
				SQL = $"
                    insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
                    values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Cetak Barcode Baru dan Barcode PSS', '{sw13.Elapsed.TotalMilliseconds} ms')
                "
				ExecuteTrans(SQL)
			Else
				CloseConn()
				MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Data Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub CetakLabelPremix()
		Try
			OpenConn()

			'============================================
			'=     CEK APAKAH BATCH SUDAH TERPENUHI     =
			'============================================
			Dim Terpenuhi As Boolean = False
			SQL = "select Kode_Perusahaan from N_EMI_Transaksi_Material_Requisition_QC_Detail "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "'  "
			SQL = SQL & "and Urut_Oto = '" & Txt_UrutDetail.Text & "' and Flag_Terpenuhi ='Y' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					Terpenuhi = True
				End If
			End Using

			Dim sw14 As New Stopwatch()
			sw14.Start()
			If Terpenuhi Then

				Dim CrDoc As New Object
				Dim kertasBarcode As String = ""
				kertasBarcode = "BarcodeFG"

				Dim SF As String = ""

				SQL = "select kode_perusahaan from N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
				SQL = SQL & "and urut_detail = '" & Txt_UrutDetail.Text & "' "

				SF = "{N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed.kode_perusahaan} = '" & KodePerusahaan & "' "
				SF = SF & "and {N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed.No_Faktur} = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "'"
				SF = SF & "and {N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed.urut_detail} = " & Val(Txt_UrutDetail.Text) & ""
				Using Ds = BindingTrans(SQL)
					If Ds.Tables("MyTable").Rows.Count <> 0 Then

						CrDoc = New N_EMI_CR_Transaksi_Request_Material_QC_Premix_Label

						'With A_Place_For_Printing2
						'    CrDoc.SetDataSource(Ds)
						'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						'    CrDoc.PrintOptions.PrinterName = ""
						'    CrDoc.RecordSelectionFormula = SF
						'    CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
						'    .Text = "Faktur Premix Label"
						'    .CrystalReportViewer1.ReportSource = CrDoc
						'    .Refresh()
						'    .Show()
						'End With

						'==============================================================================
						CrDoc.SetDataSource(Ds)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.RecordSelectionFormula = SF

						CrDoc.PrintOptions.PrinterName = PrinterBarcode

						Dim doctoprint As New System.Drawing.Printing.PrintDocument()
						doctoprint.PrinterSettings.PrinterName = PrinterBarcode

						Dim rawKind As Integer
						CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
						For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
							If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
								rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
								CrDoc.PrintOptions.PaperSize = rawKind
								Exit For
							End If
						Next

						If rawKind = Nothing Or rawKind = 0 Then
							CloseConn()
							MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Kertas Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If

						CrDoc.PrintToPrinter(1, False, 1, 2500)

					End If
				End Using

			End If
			sw14.Stop()
			SQL = $"
                insert into N_EMI_Temp_Waktu_Timbang_Premix (Kode_Perusahaan, No_faktur, Tanggal, Jam, Keterangan, Waktu)
                values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 'Cetak Label Premix', '{sw14.Elapsed.TotalMilliseconds} ms')
            "
			ExecuteTrans(SQL)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Public Shared Function isNull(ByVal xNullString As Object) As String
		Try
			If IsDBNull(xNullString) Then
				Return "0"
			Else
				Return xNullString
			End If
		Catch ex As Exception
			MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return "0"
		End Try
	End Function

	Private Sub Cmb_Faktur_RM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Faktur_RM.SelectedIndexChanged
		If Cmb_Faktur_RM.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			Dim JumlahKebutuhan As Double = 0
			CmbSatuan.Items.Clear()
			TxtSatuanBerat.Text = ""
			TxtSatuanBeratBersih.Text = ""
			SQL = "select top 1 a.Kode_Perusahaan, a.No_Faktur, No_Faktur_Order, c.Kode_Stock_Owner, c.Kode_Barang, c.Kebutuhan, b.Batch, c.Jumlah_Per_Batch, isnull(c.Jumlah_Tambah, 0) as Jumlah_Tambah, "
			SQL = SQL & " isnull(c.Jumlah_Tambah, 0) as Jumlah_Tambah_Kecil, "
			SQL = SQL & "c.Jumlah_Barang, c.Satuan, c.Satuan_Barang, c.Urut_Oto, d.Kode_Barang as Kode_Barang_Produksi, d.Nama as Nama_Barang, b.Urut_Oto as Urut_detail "
			SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c, barang d "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "and b.No_Faktur  = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
			SQL = SQL & "and b.Flag_Terpenuhi is null and c.Flag_Terpenuhi is null "
			SQL = SQL & "and a.Kode_Stock_Owner = d.Kode_Stock_Owner and a.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "and a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
			SQL = SQL & "and c.Kode_Barang  = '" & Txt_KDBarang.Text & "' "
			SQL = SQL & "order by b.Batch ASC "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = i To .Rows.Count - 1
							Txt_SORequest.Text = .Rows(i).Item("Kode_Stock_Owner")
							TXt_Batch.Text = .Rows(i).Item("Batch")
							CmbSatuan.Items.Add(.Rows(i).Item("Satuan"))
							CmbSatuan.SelectedItem = .Rows(i).Item("Satuan")
							TxtSatuanBerat.Text = .Rows(i).Item("Satuan")
							TxtSatuanBeratBersih.Text = .Rows(i).Item("Satuan")
							JumlahKebutuhan = Format((Val(HilangkanTanda(.Rows(i).Item("Jumlah_Per_Batch"))) + Val(HilangkanTanda(.Rows(i).Item("Jumlah_Tambah")))), "N4")
							Txt_Urut_DetRM.Text = .Rows(i).Item("Urut_Oto")
							Txt_KDBarangProduksi.Text = .Rows(i).Item("Kode_Barang_Produksi")
							Txt_NMBarangProduksi.Text = .Rows(i).Item("Nama_Barang")
							Txt_NoSplitProduction.Text = .Rows(i).Item("No_Faktur_Order")
							Txt_UrutDetail.Text = .Rows(i).Item("Urut_detail")
						Next
					Else
						CloseConn()
						MessageBox.Show("Data Request Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			'= GET JUMLAH SUDAH INPUT
			Dim JumlahcValidasi As Double = 0
			SQL = "select sum(isnull(a.jumlah, 0)) as Jumlah from N_EMI_Transaksi_Material_Requisition_QC_Validasi a "
			SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Flag_Retur is null "
			SQL = SQL & "and a.No_Faktur_RM = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
			SQL = SQL & "and a.Kode_Stock_Owner_Tujuan = '" & Txt_SORequest.Text & "' "
			SQL = SQL & "and a.Kode_Barang = '" & Txt_KDBarang.Text & "' and a.Urut_Det_RM = '" & Txt_Urut_DetRM.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					JumlahcValidasi = Val(HilangkanTanda(If(General_Class.CekNULL(Dr("Jumlah")) = "", 0, General_Class.CekNULL(Dr("Jumlah")))))
				End If
			End Using

			Txt_JmlhKebutuhan.Text = Format(JumlahKebutuhan, "N4")

			'====================
			'=     GET SISA     =
			'====================
			Dim JumlahValidasi As Double = 0
			SQL = "select isnull(sum(Jumlah), 0) as Jumlah from N_EMI_Transaksi_Material_Requisition_QC_Validasi a "
			SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Flag_Retur is null and a.no_Faktur_RM = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
			SQL = SQL & "and a.kode_Stock_Owner_Tujuan = '" & Txt_SORequest.Text & "' "
			SQL = SQL & "and a.kode_barang = '" & Txt_KDBarang.Text & "' "
			SQL = SQL & "and a.Urut_Det_RM = '" & Txt_Urut_DetRM.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					JumlahValidasi = Val(HilangkanTanda(Dr("Jumlah")))
				End If
			End Using

			Txt_Sisa.Text = Format(JumlahKebutuhan - JumlahcValidasi, "N4")
			Txt_Sisa_Jumlah.Text = Format(JumlahKebutuhan - JumlahcValidasi, "N4")

			'=========================================
			'=     GET NILAI TOLERANSI MIN & MAX     =
			'=========================================
			SQL = "select Toleransi_MIN, Toleransi_MAX from init where Kode_Perusahaan = '" & KodePerusahaan & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					PersenToleransiMIN = Dr("Toleransi_MIN")
					PersenToleransiMAX = Dr("Toleransi_MAX")
				Else
					CloseConn()
					MessageBox.Show("Nilai Toleransi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_ShowList_Click(sender As Object, e As EventArgs) Handles Btn_ShowList.Click

		N_EMI_SD_Request_Material_QC_Validasi.ShowDialog()

	End Sub

	Private Sub txt_Jumlah_Timbang_TextChanged(sender As Object, e As EventArgs) Handles txt_Jumlah_Timbang.TextChanged

		If Not txt_Jumlah_Timbang.Text = "0" Or Val(txt_Jumlah_Timbang.Text) > 0 Then
			If CmbJenisAlas.SelectedIndex = -1 Then
				MessageBox.Show("Pilih Dahulu Jenis Alas", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				txt_Jumlah_Timbang.Text = 0
				CmbJenisAlas.Focus() : Exit Sub
			End If
		End If

		Dim berat_net As Double = 0
		berat_net = Val(HilangkanTanda(txt_Jumlah_Timbang.Text)) - (Val(HilangkanTanda(TxtBeratAlasBersih.Text))) '+ Val(HilangkanTanda(TxtBeratBagsBersih.Text)))
		TxtBeratBersih.Text = Format(berat_net, "N4")

		Dim JumlahValidasi As Double = 0

		Try
			OpenConn()

			'====================
			'=     GET SISA     =
			'====================
			If Cmb_Faktur_RM.SelectedIndex <> -1 Then
				SQL = "select isnull(sum(Jumlah), 0) as Jumlah from N_EMI_Transaksi_Material_Requisition_QC_Validasi a "
				SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Flag_Retur is null and a.no_Faktur_RM = '" & arrNoFakturRM(Cmb_Faktur_RM.SelectedIndex) & "' "
				SQL = SQL & "and a.kode_Stock_Owner_Tujuan = '" & Txt_SORequest.Text & "' "
				SQL = SQL & "and a.kode_barang = '" & Txt_KDBarang.Text & "' "
				SQL = SQL & "and a.Urut_Det_RM = '" & Txt_Urut_DetRM.Text & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						JumlahValidasi = Val(HilangkanTanda(Dr("Jumlah")))
					End If
				End Using
			End If

			Txt_Sisa.Text = Format(Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) - Val(HilangkanTanda(JumlahValidasi)), "N4")

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		TxtSelisihKebutuhan.Text = Format(Val(HilangkanTanda(Txt_Sisa.Text)) - Val(HilangkanTanda(TxtBeratBersih.Text)), "N4")

		Dim NilaiSisaMIN As Double = Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) - (Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) * (PersenToleransiMIN / 100))
		Dim NilaiSisaMAX As Double = Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) + (Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) * (PersenToleransiMAX / 100))

		If Val(HilangkanTanda(TxtBeratBersih.Text)) + JumlahValidasi < NilaiSisaMIN Then

			TxtSelisihKebutuhan.BackColor = Color.LightYellow

		ElseIf Val(HilangkanTanda(TxtBeratBersih.Text)) + JumlahValidasi > NilaiSisaMAX Then

			TxtSelisihKebutuhan.BackColor = Color.LightCoral
		Else

			TxtSelisihKebutuhan.BackColor = Color.LightGreen

		End If

		'If Not txt_Jumlah_Timbang.Text = "" Or Val(txt_Jumlah_Timbang.Text) = 0 Then
		'    Txt_Sisa_Jumlah.Text = Format((Val(HilangkanTanda(Txt_JmlhKebutuhan.Text)) - Math.Max(0, Val(HilangkanTanda(TxtBeratBersih.Text))) - JumlahValidasi), "N4")
		'Else
		'    Txt_Sisa_Jumlah.Text = Txt_Sisa.Text
		'End If

	End Sub

	Private Sub EMI_Timbang_Floor_Scale_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
		Dim xxxx As String = ""
	End Sub

	'Private Sub EMI_Timbang_Floor_Scale_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
	'    Dim xxxx As String = ""
	'    TutupKoneksiTimbangan()
	'End Sub

	'Private Sub EMI_Timbang_Floor_Scale_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
	'    Dim xxxx As String = ""
	'    TutupKoneksiTimbangan()
	'End Sub

	Private Sub EMI_Timbang_Floor_Scale_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
		Dim xxxx As String = ""
		'TutupKoneksiTimbangan()

		isClosing = True
		For Each portName In TimbanganPorts.Keys.ToList()
			TutupKoneksi(portName)
		Next
	End Sub

	Private Sub Txt_Barcode_TextChanged(sender As Object, e As EventArgs) Handles Txt_Barcode.TextChanged

	End Sub

	'Private Sub EMI_Timbang_Floor_Scale_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
	'    Dim xxxx As String = ""
	'    TutupKoneksiTimbangan()
	'End Sub

	Private Sub CmbJenisAlas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbJenisAlas.KeyPress
		If e.KeyChar = Chr(13) Then txt_Jumlah_Timbang.Focus()
	End Sub

	Private Sub txt_Jumlah_Timbang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Jumlah_Timbang.KeyPress
		If e.KeyChar = Chr(13) Then TxtJumlahBagsDetail.Focus()
	End Sub

	Private Sub TxtJumlahBagsDetail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJumlahBagsDetail.KeyPress
		If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
			e.Handled = True
			Exit Sub
		End If

		If e.KeyChar = Chr(13) Then
			Btn_Simpan.Focus()
		End If
	End Sub

	Private Sub Cmb_Faktur_RM_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Faktur_RM.KeyPress
		If e.KeyChar = Chr(13) Then
			' CmbJenisAlas.DroppedDown = True
			CmbJenisAlas.Focus()
		End If
	End Sub

	Private Function Generate_QR_QC(ByVal isi As String)

		Dim options As New ZXing.QrCode.QrCodeEncodingOptions()

		options.DisableECI = True
		options.CharacterSet = "UTF-8"
		options.Width = 80
		options.Height = 80
		options.Margin = 0

		Dim qr As New ZXing.BarcodeWriter()
		qr.Format = ZXing.BarcodeFormat.QR_CODE
		qr.Options = options

		Dim result As New Bitmap(qr.Write(isi))
		Return result
	End Function

	'===============================================================================================================================================================
	'=     KONEKSI DATABASE KE 2
	'===============================================================================================================================================================

	Private Sub OpenConn_Premix()
		General_Class.SetConnectionString(CServer, CDatabase, CUserId, CPassword)
		Cn_Premix = New SqlClient.SqlConnection
		Cn_Premix.ConnectionString = "Data Source=" & CServer & ";Initial Catalog=" & CDatabase &
						";User Id=" & CUserId & ";Password=" & CPassword & ";" &
						";Connect Timeout=30;Max Pool Size=400"
		Cn_Premix.Open()
		Cmd_Premix = New SqlClient.SqlCommand
		Cmd_Premix.Connection = Cn_Premix
		Cmd_Premix.CommandType = CommandType.Text
		Cmd_Premix.CommandTimeout = 300000
	End Sub

	Private Sub ExecuteTrans_Premix(ByVal Query As String)
		Cmd_Premix.CommandText = Query
		Cmd_Premix.ExecuteNonQuery()
		'Cmd = Nothing
	End Sub

	Public Sub CloseConn_Premix()
		If Not Cn_Premix Is Nothing Then
			Cn_Premix.Close()
			Cn_Premix = Nothing
		End If
	End Sub

	Public Function OpenTrans_Premix(ByVal Query As String) As SqlClient.SqlDataReader
		Cmd_Premix.CommandText = Query
		Return Cmd_Premix.ExecuteReader
	End Function

	Public Function BindingTrans_Premix(ByVal Query As String) As DataSet
		Cmd_Premix.CommandText = Query
		Da_Premix = New SqlClient.SqlDataAdapter
		Da_Premix.SelectCommand = Cmd_Premix
		BindingTrans_Premix = New DataSet
		BindingTrans_Premix.Clear()
		Da_Premix.Fill(BindingTrans_Premix, "MyTable")
	End Function

	Public Sub CloseTrans_Premix()
		If Not (Cmd_Premix.Transaction Is Nothing) Then
			Cmd_Premix.Transaction.Rollback()
		End If
	End Sub

	Public Function ExecuteTransNew(ByVal Query As String) As Integer
		Cmd_Premix.CommandText = Query

		Dim affectedRows As Integer = Cmd_Premix.ExecuteNonQuery()

		If affectedRows <> 1 Then
			Throw New Exception("ExecuteTrans gagal: tidak ada data yang ter-update.")
		End If

		Return affectedRows
	End Function

End Class
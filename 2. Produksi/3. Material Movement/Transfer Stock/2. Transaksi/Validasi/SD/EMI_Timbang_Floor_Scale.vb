Imports System.ComponentModel
Imports System.IO
Imports System.IO.Ports
Imports System.Threading
'Imports Microsoft.SqlServer.Server



Public Class EMI_Timbang_Floor_Scale
    Dim arrcari As New ArrayList
    Dim Jenis = "Transaksi_Timbang_Kosong"
    Public Txt_Ekspedisi As String = ""
    Dim Random As New Random()
    Dim ReadThread As Thread


    Dim tahunMulaiProduksi As String = ""
    Private Is2ndPrint As Boolean = False

    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Dim arrIdJenisMuatan, arrMetodeTruckScale, arrid_Jenis_alas As New ArrayList
    Dim arrNamaBarang, arrKodeBarang, arrUrutPO As New ArrayList
    Dim selectedBarang, selectedUrutPO As New ArrayList

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

    Public Sub BukaKoneksiTimbangan()
        SerialPort.PortName = "COM3" 'Port_Timbangan
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

                                  Txt_Timbangan.Text = Val(nilai_data)
                                  TxtOriginal_Data_FloorScale.Text = receivedData

                                  If Strings.Left(receivedData, 2) = "ST" Then
                                      txt_Jumlah_Timbang.Text = Val(nilai_data)
                                      TxtSatuan_FloorScale.Text = satuan_berat_data
                                  Else
                                      txt_Jumlah_Timbang.Text = "0"
                                      TxtSatuan_FloorScale.Text = satuan_berat_data
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

    Private Sub Transaksi_Timbang_Unloading_Activated(sender As Object, e As EventArgs) Handles Me.Activated
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
            Lbl_Judul.Text = "Transaksi - Floor Scale"
            lblLokasi.Text = "Lokasi"
            lblBarang.Text = "Nama Barang"



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
        'kosong()

        If CmbJenisTimbang.Text = "BARANG MASUK" Then
            get_data_BM()
        End If

        BukaKoneksiTimbangan()
    End Sub

    Private Sub txt_Jumlah_Timbang_Leave(sender As Object, e As EventArgs) Handles txt_Jumlah_Timbang.Leave
        If txt_Jumlah_Timbang.Text.Trim.Length = 0 Then Exit Sub

        Dim jumlahEstimasi As Double = Val(HilangkanTanda(txt_Jml_Estimasi.Text))
        Dim jumlahBagsEstimas As Double = Val(HilangkanTanda(TxtJumlahBags.Text))

        Dim isiPerBags As Double = 0
        If CmbJenisTimbang.Text = "BARANG MASUK" Then
            isiPerBags = 0
        Else
            isiPerBags = jumlahEstimasi / jumlahBagsEstimas
        End If

        TxtJumlahBagsDetail.Text = Val(HilangkanTanda(TxtBeratBersih.Text)) * isiPerBags


        If Not IsNumeric(txt_Jumlah_Timbang.Text) Then
            txt_Jumlah_Timbang.Text = 0
            Exit Sub
        End If
    End Sub

    Private Sub get_data_BM()
        Try
            OpenConn()
            Dim jumlah_bags As Double = 0
            Dim berat_bags As Double = 0
            Dim Satuan_bags As String
            SQL = "select a.jumlah_bags, b.Berat_bags, satuan_berat_bags from "
            SQL = SQL & "emi_barang_masuk_perpallet a, barang b "
            SQL = SQL & "where no_faktur='" & txtKodeTransfer.Text & "' "
            SQL = SQL & "And a.kode_Barang = b.kode_Barang And a.Kode_stock_owner = b.Kode_stock_Owner "
            SQL = SQL & " And a.kode_Perusahaan ='" & KodePerusahaan & "' and a.Kode_Barang='" & TxtKdBarang.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    jumlah_bags = dr("jumlah_bags")
                    berat_bags = dr("jumlah_bags") * dr("Berat_bags")
                    Satuan_bags = dr("satuan_berat_bags")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak di Temukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim nilai As Double = berat_bags
            'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKdBarang.Text & "', '" & Satuan_bags & "',"
            'SQL = SQL & "'" & CmbSatuan.SelectedItem.ToString & "', '" & berat_bags & "' ) as hasil"
            'Using Dr1 = OpenTrans(SQL)
            '    If Dr1.Read Then
            '        If General_Class.CekNULL(Dr1("hasil")) = "" Then
            '            Dr1.Close()
            '            CloseConn()
            '            MessageBox.Show("data konversi satuan kirim tidak ada ")
            '            Exit Sub
            '        End If

            '        nilai = Dr1("hasil")
            '    Else
            '        Dr1.Close()
            '        CloseConn()
            '        MessageBox.Show("data konversi satuan kirim tidak ada ")
            '        Exit Sub
            '    End If
            'End Using

            TxtJumlahBags.Text = Format(jumlah_bags, "N2")
            TxtBeratBags.Text = Format(nilai, "N2")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_data_TF()
        Try
            OpenConn()
            Dim jumlah_bags As Double = 0
            Dim berat_bags As Double = 0
            Dim Satuan_bags As String
            SQL = "select a.jumlah_bags, b.Berat_bags, satuan_berat_bags from "
            SQL = SQL & "Tf_Stock_det a, barang b "
            SQL = SQL & "where no_faktur='" & txtKodeTransfer.Text & "' and urut_oto = '" & txtUrutOto.Text & "' "
            SQL = SQL & "And a.kode_Barang = b.kode_Barang And a.Kode_stock_owner = b.Kode_stock_Owner "
            SQL = SQL & " And a.kode_Perusahaan ='" & KodePerusahaan & "' and a.Kode_Barang='" & TxtKdBarang.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    jumlah_bags = dr("jumlah_bags")
                    berat_bags = dr("jumlah_bags") * dr("Berat_bags")
                    Satuan_bags = dr("satuan_berat_bags")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak di Temukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim nilai As Double = berat_bags
            'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKdBarang.Text & "', '" & Satuan_bags & "',"
            'SQL = SQL & "'" & CmbSatuan.SelectedItem.ToString & "', '" & berat_bags & "' ) as hasil"
            'Using Dr1 = OpenTrans(SQL)
            '    If Dr1.Read Then
            '        If General_Class.CekNULL(Dr1("hasil")) = "" Then
            '            Dr1.Close()
            '            CloseConn()
            '            MessageBox.Show("data konversi satuan kirim tidak ada ")
            '            Exit Sub
            '        End If

            '        nilai = Dr1("hasil")
            '    Else
            '        Dr1.Close()
            '        CloseConn()
            '        MessageBox.Show("data konversi satuan kirim tidak ada ")
            '        Exit Sub
            '    End If
            'End Using

            TxtJumlahBags.Text = Format(jumlah_bags, "N2")
            TxtBeratBags.Text = Format(nilai, "N2")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub kosong()
        CmbJenisTimbang.Items.Clear()
        CmbJenisTimbang.Items.Add("BARANG MASUK")
        CmbJenisTimbang.Items.Add("TRANSFER STOCK")
        CmbJenisTimbang.Items.Add("PEMUSNAHAN BARANG")

        Txt_Timbangan.Text = "0"

        txt_lokasi.Text = ""
        txt_barang.Text = ""
        'Txt_Ekspedisi.Text = ""
        Txt_Ekspedisi = ""
        txt_barang.Text = ""

        txt_Jml_Estimasi.Text = ""
        txt_Jumlah_Timbang.Text = ""
        Txt_Berat_Bags_Bersih.Text = ""
        TxtSatuan_FloorScale.Text = ""
        Txt_Sisa_Jumlah.Text = ""
        Txt_Sisa_Bags.Text = ""

        Txt_Bags_Sisa_Bersih.Text = ""
        Txt_Jumlah_Sisa_Bersih.Text = ""

        'txt_Jml_Estimasi.Enabled = True
        ' txt_Jumlah_Timbang.Enabled = True

        txt_Jumlah_Timbang.Text = "0" 'Txt_Timbangan.Text
        TxtOriginal_Data_FloorScale.Text = "0"
        Try
            OpenConn()

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

            CmbSatuan.Text = satuan_timbang
            CmbSatuan.Enabled = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Private Sub kosong_sebagian()
        txt_Jumlah_Timbang.Text = ""
        TxtJumlahBagsDetail.Text = ""
        TxtBeratBersih.Text = ""
        Txt_Jumlah_Sisa_Bersih.Text = ""
        Txt_Bags_Sisa_Bersih.Text = ""
        Txt_Sisa_Bags.Text = ""
        Txt_Sisa_Jumlah.Text = ""
        GetSisaTransfer()
    End Sub

    'Data PO berdasarkan Supplier

    Public Sub GetSisaTransfer()
        Try
            OpenConn()

            Dim jumlahSimpan As Double = 0
            Dim jumlahBagsSimpan As Double = 0
            Dim jumlahTF As Double = 0
            Dim BagsTF As Double = 0

            SQL = "select Jumlah_Barang, Jumlah_Bags from N_EMI_Transaksi_Transfer_Waste_Det where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & txtKodeTransfer.Text & "' and Urut_Oto = '" & txtUrutOto.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    jumlahTF = Val(HilangkanTanda(dr("Jumlah_Barang")))
                    BagsTF = Val(HilangkanTanda(dr("Jumlah_Bags")))
                Else
                    jumlahTF = 0
                    BagsTF = 0
                End If
            End Using

            SQL = "select sum(a.jumlah) as jumlah_simpan, sum(a.jumlah_bags) as Jumlah_Bags_Simpan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Det2 a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & txtKodeTransfer.Text & "' and a.Urut_Det = '" & txtUrutOto.Text & "' "
            SQL = SQL & "group by a.jumlah, a.Kode_Perusahaan, a.No_Faktur, a.Urut_Det"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    jumlahSimpan = Val(HilangkanTanda(Dr("jumlah_simpan")))
                    jumlahBagsSimpan = Val(HilangkanTanda(Dr("Jumlah_Bags_Simpan")))

                Else
                    jumlahSimpan = 0
                    jumlahBagsSimpan = 0
                End If
            End Using


            Dim Sisa_Jumlah As Double = jumlahTF - jumlahSimpan

            '====================================
            '=       CONVERT SATUAN KECIL       =
            '====================================
            Dim sisaJumlah_Kecil As Double = Val(HilangkanTanda(Sisa_Jumlah))
            'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKdBarang.Text & "', '" & Txt_SatuanKecil.Text & "',"
            'SQL = SQL & "'" & CmbSatuan.SelectedItem.ToString & "', '" & HilangkanTanda(Sisa_Jumlah) & "' ) as hasil"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If General_Class.CekNULL(Dr("hasil")) = "" Then
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("data konversi satuan kirim tidak ada ")
            '            Exit Sub
            '        End If

            '        sisaJumlah_Kecil = Dr("hasil")
            '    Else
            '        Dr.Close()
            '        CloseConn()
            '        MessageBox.Show("data konversi satuan kirim tidak ada ")
            '        Exit Sub
            '    End If
            'End Using

            Dim sisa_Bags As Double = BagsTF - jumlahBagsSimpan



            Txt_Sisa_Jumlah.Text = Format(sisaJumlah_Kecil, "N2") & " " & CmbSatuan.SelectedItem.ToString
            Txt_Sisa_Bags.Text = Format(sisa_Bags, "N0")

            Txt_Jumlah_Sisa_Bersih.Text = Format(sisaJumlah_Kecil, "N2")
            Txt_Bags_Sisa_Bersih.Text = Format(sisa_Bags, "N0")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then txt_barang.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Txt_Timbangan.Text = "0"
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If CmbJenisAlas.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Alas Belum dipilih . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If txt_Jumlah_Timbang.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah timbang tidak boleh kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        TxtSatuan_FloorScale.Text = "KG"

        If txt_Jumlah_Timbang.Text.Trim.Length = 0 Or Val(txt_Jumlah_Timbang.Text) = 0 Or Val(txt_Jumlah_Timbang.Text) < 0 Then
            MessageBox.Show("Berat Timbang Tidak Boleh Kosong atau 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TxtSatuan_FloorScale.Text.Trim.ToUpper <> CmbSatuan.Text.ToUpper Then
            MessageBox.Show("Satuan timbang berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
            'ElseIf Strings.Left(TxtOriginal_Data_FloorScale.Text.Trim.ToUpper, 2) <> "ST" Then
            '    MessageBox.Show("Terjadi kesalahan pada timbangan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
        End If
        get_jam()


        Dim batchLama As String = ""
        Dim kode_unik_print As String

        If CmbJenisTimbang.Text.Trim.ToUpper = "BARANG MASUK" Then

            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction


                Dim jumlah_masuk_Barang As Double = Val(HilangkanTanda(TxtBeratBersih.Text))
                Dim Satuan_Barang As String = ""

                SQL = "select distinct Satuan from Barang where "
                SQL = SQL & "Kode_Barang='" & TxtKdBarang.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                Using dr2 = OpenTrans(SQL)
                    If dr2.Read Then
                        Satuan_Barang = dr2("Satuan")
                    Else
                        dr2.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang Tidak ditemukan . . ! !")
                        Exit Sub
                    End If
                End Using


                'UBAH KE SATUAN PO
                'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & TxtKdBarang.Text & "',"
                'SQL = SQL & "'" & CmbSatuan.Text & "','" & Satuan_Barang & "',"
                'SQL = SQL & "" & HilangkanTanda(TxtBeratBersih.Text) & ") as Hasil "
                'Using dr3 = OpenTrans(SQL)
                '    If dr3.Read Then
                '        If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                '            jumlah_masuk_Barang = dr3("Hasil")
                '        Else
                '            MessageBox.Show("Satuan " & CmbSatuan.Text & " Ke " & Satuan_Barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    End If
                'End Using

                SQL = "update EMI_Barang_Masuk_Perpallet set  "
                SQL = SQL & "Flag_Timbang = 'Y', "
                SQL = SQL & "jumlah = '" & HilangkanTanda(TxtBeratBersih.Text) & "', "
                SQL = SQL & "Nilai_Barang = '" & jumlah_masuk_Barang & "', "
                SQL = SQL & "tanggal_Timbang = '" & Format(CDate(tgl_skg), "yyyy-MM-dd") & "', "
                SQL = SQL & "jam_Timbang = '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
                SQL = SQL & "user_Timbang = '" & UserID & "', "
                SQL = SQL & "Id_Jenis_Alas = '" & arrid_Jenis_alas(CmbJenisAlas.SelectedIndex) & "', "
                SQL = SQL & "Jumlah_Gross = '" & HilangkanTanda(txt_Jumlah_Timbang.Text) & "', "
                SQL = SQL & "Satuan_Gross = '" & CmbSatuan.Text & "', "
                SQL = SQL & "Jumlah_Alas = '" & HilangkanTanda(TxtBeratAlas_Bersih.Text) & "', "
                SQL = SQL & "Satuan_Alas= '" & CmbSatuan.Text & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_faktur = '" & txtKodeTransfer.Text & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseTrans()
                CloseConn()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



        ElseIf CmbJenisTimbang.Text.Trim.ToUpper = "TRANSFER STOCK" Then

            Dim QrLama As String = ""
            Dim expDate As String = ""
            Dim tglMsk As String = ""
            Dim metodePengeluaranStock As String = ""
            Dim GetDataKodeTransfer, GetDataLokasi, GetDataKdBrg, GetDataNmBrg, GetDataBrgSN, GetDataJmlEstimasi, GetDataSatuanBesar, GetDataSatuanKecil, GetDataUrutOto As String
            Dim GetJumlahBags, GetSoAwal, GetSoTujuan, GetSnAwal, GetRakTujuan, GetPalletTujuan, GetWarna As String
            Dim SN As String = ""


            Dim Kd_Soo As String = ""
            Dim Kd_Barangg As String = ""
            Dim Urut_Det_Convert As String = ""
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction


                Dim arr_Sn As New ArrayList

                Dim ada_data As Boolean = False
                SQL = "Select c.serial_number from tf_stock_parent a, tf_stock_det b, barang_sn c where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur "
                SQL = SQL & "And a.status Is null And b.selesai Is null  "
                SQL = SQL & "And b.kode_perusahaan=c.kode_Perusahaan And b.serial_number_awal=c.serial_number "
                SQL = SQL & "And c.kode_perusahaan='" & KodePerusahaan & "' and c.qr_code+'-'+kode_unik_berjalan='" & TxtBarcode.Text & "' "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        ada_data = True
                        arr_Sn.Add(dr("serial_number"))
                    Loop
                End Using


                If ada_data = False Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Barcode Tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    kosong()
                    Exit Sub
                End If

                Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)
                Dim namaBarang As String = ""

                For Indxx = 0 To arr_Sn.Count - 1

                    Dim Id_Jenis_Kategori_Produksi As String = ""
                    'Ambil Data SN Berdasar Barcode
                    SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.tgl_masuk, b.Metode_Pengeluaran_Stok, a.id_jenis_kategori_produksi "
                    SQL = SQL & "from barang_sn a, barang b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                    SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Jumlah <> 0 "
                    'SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & TxtBarcode.Text & "' "
                    SQL = SQL & "and a.Serial_Number ='" & arr_Sn.Item(Indxx) & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                            batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                            SN = Dr("serial_number")
                            expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                            tglMsk = General_Class.CekNULL(Dr("tgl_masuk"))
                            metodePengeluaranStock = Dr("Metode_Pengeluaran_Stok")

                            If General_Class.CekNULL(Dr("id_jenis_kategori_produksi")) = "" Then
                                Id_Jenis_Kategori_Produksi = "NULL"
                            Else
                                Id_Jenis_Kategori_Produksi = $"'{Dr("id_jenis_kategori_produksi")}'"
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            kosong()
                            Exit Sub
                        End If
                    End Using



                    'Cek data YG Mau di TF, Berdasar SN dr Barcode
                    SQL = "Select a.no_faktur, a.lokasi, a.so_awal, a.so_tujuan, c.urut_Oto, b.kode_Barang, "
                    SQL = SQL & "d.nama, b.Total, b.satuan, b.Satuan_Barang, c.serial_number_awal, "
                    SQL = SQL & "c.jumlah, c.Jumlah_Bags, c.Id_Wms_Tujuan, c.Warna, "

                    SQL = SQL & "isnull((select x.Labeling_WMS_Position from View_Warehouse_Position x where "
                    SQL = SQL & "x.Kode_Perusahaan = c.Kode_Perusahaan And x.Id_WMS_Warehouse_Position = c.Id_Wms_Awal), null) As Rak_Awal, "

                    SQL = SQL & "b.Urut_Material_Requisition_Convert "

                    SQL = SQL & "From tf_stock_parent a, tf_stock b, tf_stock_det c, barang d Where "
                    SQL = SQL & "a.kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And "
                    SQL = SQL & "b.kode_Perusahaan = c.kode_Perusahaan And b.no_faktur = c.no_faktur And b.Urut_Oto = c.urut_TF "
                    SQL = SQL & "And b.Kode_Barang=d.Kode_Barang And a.so_awal=d.kode_stock_Owner And b.kode_Perusahaan=d.Kode_Perusahaan "
                    SQL = SQL & "And a.status Is null And b.Flag_Timbang ='Y' and c.selesai is null "
                    SQL = SQL & "And c.Serial_Number_Awal = '" & SN & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            GetDataKodeTransfer = Dr("No_faktur")
                            GetDataLokasi = Dr("SO_Awal")
                            GetDataKdBrg = Dr("Kode_Barang")
                            GetDataNmBrg = Dr("Nama")
                            GetDataBrgSN = Dr("Serial_Number_Awal")
                            GetDataJmlEstimasi = HilangkanTanda(Format(Dr("Total"), "N2"))
                            GetDataSatuanKecil = Dr("Satuan_Barang")
                            GetDataSatuanBesar = Dr("Satuan")
                            GetDataUrutOto = Dr("urut_oto")

                            GetJumlahBags = Dr("Jumlah_Bags")
                            GetSoAwal = Dr("SO_Awal")
                            GetSoTujuan = Dr("SO_Tujuan")
                            GetSnAwal = Dr("Serial_Number_Awal")
                            GetRakTujuan = Dr("Id_Wms_Tujuan")
                            'GetPalletTujuan = Dr("No_Pallet_Tujuan")
                            GetWarna = Dr("Warna")

                            Kd_Soo = Dr("SO_Awal")
                            Kd_Barangg = Dr("Kode_Barang")
                            Urut_Det_Convert = Dr("Urut_Material_Requisition_Convert")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            kosong()
                            Exit Sub
                        End If
                    End Using

                    SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
                    SQL = SQL & "from tf_stock_parent a, tf_stock b, Tf_Stock_det c "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
                    SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' and c.urut_oto = '" & GetDataUrutOto & "'  "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            If General_Class.CekNULL(Dr("status")) <> "" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "T" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using



                    'SQL = "Select Top(1) nomor_urut from dbo.N_EMI_Wharehouse_Position_Fn('" & KodePerusahaan & "','" & GetSoTujuan & "', " & GetRakTujuan & ") where "
                    'SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null "
                    'SQL = SQL & "order by nomor_urut "
                    'Using dr = OpenTrans(SQL)
                    '    If dr.Read Then
                    '        GetPalletTujuan = dr("nomor_urut")
                    '    Else
                    '        dr.Close()
                    '        CloseTrans()
                    '        CloseConn()
                    '        MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                    '        Exit Sub
                    '    End If
                    'End Using



                    '====================================
                    '=       CONVERT SATUAN KECIL       =
                    '====================================
                    Dim nilai_kecildetail As Double = Val(HilangkanTanda(TxtBeratBersih.Text))
                    'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & GetDataKdBrg & "', '" & CmbSatuan.SelectedItem.ToString & "',"
                    'SQL = SQL & "'" & GetDataSatuanKecil & "', '" & HilangkanTanda(TxtBeratBersih.Text) & "' ) as hasil"
                    'Using Dr1 = OpenTrans(SQL)
                    '    If Dr1.Read Then
                    '        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                    '            Dr1.Close()
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("data konversi satuan kirim tidak ada ")
                    '            Exit Sub
                    '        End If

                    '        nilai_kecildetail = Dr1("hasil")
                    '    Else
                    '        Dr1.Close()
                    '        CloseTrans()
                    '        CloseConn()
                    '        MessageBox.Show("data konversi satuan kirim tidak ada ")
                    '        Exit Sub
                    '    End If
                    'End Using


                    '============================
                    '=       POTONG STOCK       =
                    '============================

#Region "POTONG STOCK"

                    '======================================
                    '=     GET STOCK SEBELUM DIPOTONG     =
                    '======================================
                    Dim Stock_SblmPotong As Double = 0
                    Dim Stock_SN_SblmPotong As Double = 0
                    SQL = "select isnull(sum(Good_Stock), 0) as Stock from barang WHERE kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND Kode_Stock_Owner = '" & GetSoAwal & "' and kode_barang = '" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Stock_SblmPotong = Math.Round(Dr("Stock"), 4)
                        End If
                    End Using

                    SQL = "select isnull(sum(Jumlah), 0) as Stock_SN from barang_sn WHERE kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND Kode_Stock_Owner = '" & GetSoAwal & "' and kode_barang = '" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Stock_SN_SblmPotong = Math.Round(Dr("Stock_SN"), 4)
                        End If
                    End Using

                    If Stock_SblmPotong <> Stock_SN_SblmPotong Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jumlah Stock {GetDataKdBrg} pada Gudang {GetSoAwal} Tidak Sesuai Sebelum Dipotong", Judul,
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Dim nilai_persediaan_min As Double = 0
                    SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
                    SQL = SQL & "Kode_Stock_Owner='" & GetSoAwal & "' and Kode_Barang='" & GetDataKdBrg & "' "
                    SQL = SQL & "and Serial_Number='" & GetSnAwal & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            nilai_persediaan_min = dr("rp_persediaan_min")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim Nama As String = ""
                    'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
                    SQL = "select Nama,round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetSoAwal & "' "
                    SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            Nama = dr("nama")
                            If dr("good_stock") < nilai_kecildetail Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf dr("Jumlah_Bags") < Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                dr.Close()
                                SQL = "update barang set Good_Stock = Good_Stock - Round(" & nilai_kecildetail & ",4), "
                                SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & " "
                                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetSoAwal & "' "
                                SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
                                ExecuteTrans(SQL)

                                SQL = "insert into N_EMI_LOG_Transaksi_Validasi_Transfer_Stock_Floor_Scale "
                                SQL &= $"(Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
                                SQL &= $"values ('{KodePerusahaan}', '{GetDataKodeTransfer}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
                                SQL &= $"'POTONG STOCK BARANG', '{GetSoAwal}', '{GetDataKdBrg}', '-', '{Stock_SblmPotong}', 0, '{nilai_kecildetail}', 0) "
                                ExecuteTrans(SQL)

                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetSoAwal & "' "
                    SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                    SQL = SQL & "and Serial_Number='" & GetSnAwal & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If dr("jumlah") < nilai_kecildetail Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf dr("Jumlah_Bags") < Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                dr.Close()
                                SQL = "update barang_sn set jumlah = jumlah - Round(" & nilai_kecildetail & ",4), "
                                SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & " "
                                SQL = SQL & "where Kode_Stock_Owner='" & GetSoAwal & "' and Kode_Barang='" & GetDataKdBrg & "' "
                                SQL = SQL & "and Serial_Number='" & GetSnAwal & "'"
                                ExecuteTrans(SQL)

                                SQL = "insert into N_EMI_LOG_Transaksi_Validasi_Transfer_Stock_Floor_Scale "
                                SQL &= $"(Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
                                SQL &= $"values ('{KodePerusahaan}', '{GetDataKodeTransfer}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
                                SQL &= $"'POTONG STOCK BARANG SN', '{GetSoAwal}', '{GetDataKdBrg}', '{GetSnAwal}', '{Stock_SblmPotong}', 0, '{nilai_kecildetail}', 0) "
                                ExecuteTrans(SQL)
                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetSoAwal & "' "
                    SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using


                    '=======================================
                    '=     CEK STOCK SETELAH DI POTONG     =
                    '=======================================
                    Dim Stock_Setelah_Potong As Double = 0
                    Dim Stock_SN_Setelah_Potong As Double = 0
                    SQL = "select isnull(sum(Good_Stock), 0) as Stock from barang WHERE kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND Kode_Stock_Owner = '" & GetSoAwal & "' and kode_barang = '" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Stock_Setelah_Potong = Math.Round(Dr("Stock"), 4)
                        End If
                    End Using

                    SQL = "select isnull(sum(Jumlah), 0) as Stock_SN from barang_sn WHERE kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND Kode_Stock_Owner = '" & GetSoAwal & "' and kode_barang = '" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Stock_SN_Setelah_Potong = Math.Round(Dr("Stock_SN"), 4)
                        End If
                    End Using

                    If Stock_Setelah_Potong <> Stock_SN_Setelah_Potong Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jumlah Stock {GetDataKdBrg} pada Gudang {GetSoAwal} Tidak Sesuai Setelah Di Potong", Judul,
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If Math.Round((Stock_SblmPotong - Stock_Setelah_Potong), 4) <> nilai_kecildetail Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jumlah Stock Sebelum dan Sesudah Di potong Pada Gudang {GetSoAwal}, Barang {GetDataKdBrg} Tidak Sesuai Setelah Di Potong", Judul,
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If Math.Round((Stock_SN_SblmPotong - Stock_SN_Setelah_Potong), 4) <> nilai_kecildetail Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jumlah Stock Sebelum dan Sesudah Di potong Pada Gudang {GetSoAwal}, Barang {GetDataKdBrg} Tidak Sesuai Setelah Di Potong", Judul,
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

#End Region

                    '==========================================
                    '=       GET NOMOR REQUEST MATERIAL       =
                    '==========================================
                    Dim No_Reservasi_Split As String = "NULL"
                    SQL = "select a.No_Faktur, a.No_Faktur_Order "
                    SQL = SQL & "from Emi_Material_Requisition a "
                    SQL = SQL & "inner join Emi_Material_Requisition_Det b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "inner join Emi_Material_Requisition_Det_Convert c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Status is null "
                    SQL = SQL & "and c.Urut_Oto = '" & Urut_Det_Convert & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            No_Reservasi_Split = $"'{Dr("No_Faktur_Order")}'"
                        End If
                    End Using


                    '==============================
                    '=       INSERT SN BARU       =
                    '==============================

#Region "INSERT SN BARU"

                    '===========================================
                    '=       GET STOCK SEBELUM DIINSERT       =
                    '===========================================
                    Dim Stock_Sebelum_Insert As Double = 0
                    Dim Stock_SN_Sebelum_Insert As Double = 0
                    Dim Bags_Sebelum_Insert As Double = 0
                    Dim Bags_SN_Sebelum_Insert As Double = 0
                    SQL = "select isnull(sum(Good_Stock), 0) as Stock, sum(Jumlah_Bags) as Stock_Bags from barang WHERE kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND Kode_Stock_Owner = '" & GetSoTujuan & "' and kode_barang = '" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Stock_Sebelum_Insert = Math.Round(Dr("Stock"), 4)
                            Bags_Sebelum_Insert = Math.Round(Dr("Stock_Bags"), 4)
                        End If
                    End Using

                    SQL = "select isnull(sum(Jumlah), 0) as Stock_SN, sum(Jumlah_Bags) as Stock_Bags_SN from barang_sn WHERE kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND Kode_Stock_Owner = '" & GetSoTujuan & "' and kode_barang = '" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Stock_SN_Sebelum_Insert = Math.Round(Dr("Stock_SN"), 4)
                            Bags_SN_Sebelum_Insert = Math.Round(Dr("Stock_Bags_SN"), 4)
                        End If
                    End Using

                    If Stock_Sebelum_Insert <> Stock_SN_Sebelum_Insert Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jumlah Stock {GetDataKdBrg} pada Gudang {GetSoTujuan} Tidak Sesuai Sebelum Diinsert", Judul,
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Dim hargaIsn As String = ""
                    Dim warnaLama As String = ""

                    'Ambil Data Lama
                    SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                    SQL = SQL & "from barang_sn a, barang b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                    SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Kode_Stock_Owner='" & GetSoAwal & "' "
                    SQL = SQL & "and a.Kode_Barang ='" & GetDataKdBrg & "' "
                    SQL = SQL & "and a.Serial_Number='" & GetSnAwal & "' "
                    'SQL = SQL & "and a.Jumlah <> 0 "
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                            QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                            batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                            namaBarang = General_Class.CekNULL(Dr("Nama"))
                            expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                            warnaLama = General_Class.CekNULL(Dr("warna"))
                        Loop
                    End Using

                    'GENERATE SN BARU
                    Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                    Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                    Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")



                    'INSERT BARANG SN BARU  
                    SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                    SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN, id_jenis_kategori_produksi, No_Reservasi) "
                    SQL = SQL & "select Kode_Perusahaan, '" & GetSoTujuan & "', Kode_Barang, '" & SN_Baru & "', '" & Val(HilangkanTanda(Format(nilai_kecildetail, "N4"))) & "', " & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & ", "
                    SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & GetRakTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                    SQL = SQL & "Kode_Unik_Asal, '" & GetPalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, NULL, " & Id_Jenis_Kategori_Produksi & ", " & No_Reservasi_Split & " "
                    SQL = SQL & "from Barang_SN "
                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Stock_Owner='" & GetSoAwal & "' "
                    SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                    SQL = SQL & "and Serial_Number='" & GetSnAwal & "' "
                    ExecuteTrans(SQL)

                    SQL = "insert into N_EMI_LOG_Transaksi_Validasi_Transfer_Stock_Floor_Scale "
                    SQL &= $"(Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
                    SQL &= $"values ('{KodePerusahaan}', '{GetDataKodeTransfer}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
                    SQL &= $"'INSERT STOCK BARANG SN', '{GetSoTujuan}', '{GetDataKdBrg}', '{SN_Baru}', '{Stock_Sebelum_Insert}', 0, '{nilai_kecildetail}', 0) "
                    ExecuteTrans(SQL)

                    '============================
                    '=       TAMBAH STOCK       =
                    '============================
                    SQL = "update barang set Good_Stock= Good_Stock + Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & " "
                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetSoTujuan & "' "
                    SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
                    ExecuteTrans(SQL)

                    SQL = "insert into N_EMI_LOG_Transaksi_Validasi_Transfer_Stock_Floor_Scale "
                    SQL &= $"(Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
                    SQL &= $"values ('{KodePerusahaan}', '{GetDataKodeTransfer}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
                    SQL &= $"'INSERT STOCK BARANG', '{GetSoTujuan}', '{GetDataKdBrg}', '-', '{Stock_Sebelum_Insert}', 0, '{nilai_kecildetail}', 0) "
                    ExecuteTrans(SQL)


                    'CEK KESESUAIAN STOCK
                    SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                    SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                    SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                    SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                    SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                    SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetSoTujuan & "' "
                    SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using

                    '=======================
                    '=     CEK SN BARU     =
                    '=======================
                    SQL = "SELECT Kode_Perusahaan from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' AND Serial_Number = '" & SN_Baru & "'"
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data SN Baru Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    '=======================================
                    '=     CEK STOCK SETELAH DIINSERT     =
                    '=======================================
                    Dim Stock_Setelah_Insert As Double = 0
                    Dim Stock_SN_Setelah_Insert As Double = 0
                    SQL = "select isnull(sum(Good_Stock), 0) as Stock from barang WHERE kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND Kode_Stock_Owner = '" & GetSoTujuan & "' and kode_barang = '" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Stock_Setelah_Insert = Math.Round(Dr("Stock"), 4)
                        End If
                    End Using

                    SQL = "select isnull(sum(Jumlah), 0) as Stock_SN from barang_sn WHERE kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND Kode_Stock_Owner = '" & GetSoTujuan & "' and kode_barang = '" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Stock_SN_Setelah_Insert = Math.Round(Dr("Stock_SN"), 4)
                        End If
                    End Using

                    If Stock_Setelah_Insert <> Stock_SN_Setelah_Insert Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jumlah Stock {GetDataKdBrg} pada Gudang {GetSoTujuan} Tidak Sesuai Setelah Diinsert", Judul,
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If


                    If Math.Round((Stock_Setelah_Insert - Stock_Sebelum_Insert), 4) <> nilai_kecildetail Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jumlah Stock Sebelum dan Sesudah Di Insert Pada Gudang {GetSoTujuan}, Barang {GetDataKdBrg} Tidak Sesuai Setelah Di Potong", Judul,
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If Math.Round((Stock_SN_Setelah_Insert - Stock_SN_Sebelum_Insert), 4) <> nilai_kecildetail Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jumlah Stock Sebelum dan Sesudah Di Insert Pada Gudang {GetSoTujuan}, Barang {GetDataKdBrg} Tidak Sesuai Setelah Di Potong", Judul,
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If


#End Region

#Region "JURNAL"

                    'dari
                    Dim inisial_faktur_dari As String = ""
                    Dim akun_persediaan_dari As String = ""
                    Dim akun_persediaan_tujuan As String = ""

                    SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & GetSoAwal & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            'akun_persediaan_dari = Dr("persediaan")
                            inisial_faktur_dari = Dr("inisial_faktur")

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select c.akun_Persediaan "
                    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and b.kode_stock_owner = '" & GetSoAwal & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            akun_persediaan_dari = Dr("akun_Persediaan")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select c.akun_Persediaan "
                    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and b.kode_stock_owner = '" & GetSoTujuan & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            akun_persediaan_tujuan = Dr("akun_Persediaan")
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
                    SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & txtKodeTransfer.Text & "', '', "
                    SQL = SQL & "'-', '" & UserID & "')"
                    ExecuteTrans(SQL)

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                      Strings.Mid(akun_persediaan_dari, 2, 1),
                      Strings.Mid(Ganti(akun_persediaan_dari), 3),
                      KodePerusahaan, KodeProyek, "Persedian " & txtKodeTransfer.Text, "0", nilai_persediaan_min, pagenumber, GetSoAwal, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                     Strings.Mid(akun_persediaan_tujuan, 2, 1),
                     Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                     KodePerusahaan, KodeProyek, "Persedian " & txtKodeTransfer.Text, nilai_persediaan_min, "0", pagenumber, GetSoTujuan, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
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


#End Region

                    'PERHATIKAN VOUCHER DIBAWAH INI
                    'Dim Kode_voucher As String = "TESVOUCHER"

                    SQL = "insert into Tf_Stock_det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
                    SQL = SQL & "Serial_Number, Jumlah, jumlah_bags, UserID, Tanggal, Jam, Kode_Voucher) values( "
                    SQL = SQL & "'" & KodePerusahaan & "', '" & GetDataKodeTransfer & "', '" & GetDataUrutOto & "', "
                    SQL = SQL & "'" & GetPalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', '" & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & "', "
                    SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                    SQL = SQL & "'" & Kode_voucher & "') "
                    ExecuteTrans(SQL)

                    SQL = "update Tf_Stock_det set  "
                    SQL = SQL & "Selesai = 'Y' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and urut_oto = '" & GetDataUrutOto & "' "
                    ExecuteTrans(SQL)

                Next


                '================================
                '=     CEK REQUEST MATERIAL     =
                '================================
#Region "Request Material"

                '=======================================================
                '=     CEK APAKAH DATA RM DAN CEK JUMLAH KEBUTUHAN     =
                '=======================================================
                Dim Jumlah_Kebutuhan_Request As Double = 0
                Dim isDataRequest As Boolean = False
                SQL = "select c.Jumlah as Jumlah_Kebutuhan "
                SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
                SQL = SQL & "and a.status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and c.Urut_Oto = ( "
                'SQL = SQL & "select x.Urut_Material_Requisition_Convert "
                'SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_Det y, barang_sn r "
                'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.kode_perusahaan = r.kode_perusahaan "
                'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
                'SQL = SQL & "and y.serial_number_awal = r.serial_number "
                'SQL = SQL & "and z.Status is null "
                'SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
                'SQL = SQL & "and y.Selesai is null "
                'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                'SQL = SQL & "and (r.Qr_Code+'-'+r.Kode_Unik_Berjalan) = '" & TxtBarcode.Text & "') "
                SQL = SQL & "and c.Urut_Oto = " & Urut_Det_Convert & " "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Jumlah_Kebutuhan_Request = Dr("Jumlah_Kebutuhan")
                        isDataRequest = True
                    Else
                        isDataRequest = False
                    End If
                End Using

                If isDataRequest Then
                    '================================
                    '=     CEK APAKAH LAST DATA     =
                    '================================
                    Dim isLastData As Boolean = False
                    SQL = "select c.Serial_Number_Awal, (d.Qr_Code+'-'+d.Kode_Unik_Berjalan) as barcode "
                    SQL = SQL & "from Tf_Stock_Parent a, Tf_Stock b, Tf_Stock_Det c, barang_sn d "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                    SQL = SQL & "and c.Serial_Number_Awal = d.Serial_Number "
                    SQL = SQL & "and a.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    'SQL = SQL & "and a.No_Faktur = ( "
                    'SQL = SQL & "select z.No_Faktur "
                    'SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_Det y, barang_sn r "
                    'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.kode_perusahaan = r.kode_perusahaan "
                    'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                    'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
                    'SQL = SQL & "and y.serial_number_awal = r.serial_number "
                    'SQL = SQL & "and z.Status is null "
                    'SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
                    'SQL = SQL & "and y.Selesai is null "
                    'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                    'SQL = SQL & "and (r.Qr_Code+'-'+r.Kode_Unik_Berjalan) = '" & TxtBarcode.Text & "') "

                    SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' "

                    SQL = SQL & "and not exists ( "
                    SQL = SQL & "select 1 from TF_Stock_Det2 z where z.kode_perusahaan = c.Kode_Perusahaan and z.no_faktur = c.No_Faktur and z.Urut_Det = c.Urut_Oto) "
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            isLastData = True
                        End If
                    End Using


                    '=====================================
                    '=     CEK JUMLAH SUDAH TRANSFER     =
                    '=====================================
                    Dim Jumlah_Sudah_Transfer As Double = 0
                    SQL = "select isnull(sum(d.Jumlah), 0) as Jumlah_Transfer "
                    SQL = SQL & "from Tf_Stock_Parent a, Tf_Stock b, Tf_Stock_Det c, Tf_Stock_Det2 d "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                    SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
                    SQL = SQL & "and a.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    'SQL = SQL & "and b.Urut_Material_Requisition_Convert = ( "
                    'SQL = SQL & "select x.Urut_Material_Requisition_Convert "
                    'SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_Det y, barang_sn r "
                    'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.kode_perusahaan = r.kode_perusahaan "
                    'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                    'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
                    'SQL = SQL & "and y.serial_number_awal = r.serial_number "
                    'SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
                    'SQL = SQL & "and y.Selesai is null "
                    'SQL = SQL & "and z.Status is null "
                    'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                    'SQL = SQL & "and (r.Qr_Code+'-'+r.Kode_Unik_Berjalan) = '" & TxtBarcode.Text & "') "
                    SQL = SQL & "and b.Urut_Material_Requisition_Convert = " & Urut_Det_Convert & " "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Jumlah_Sudah_Transfer = Dr("Jumlah_Transfer")
                        End If
                    End Using

                    '===========================================
                    '=     GET NILAI TOLERANSI MIN DAN MAX     =
                    '===========================================
                    Dim Persen_Toleransi_Min As Double = 0
                    Dim Persen_Toleransi_Max As Double = 0
                    SQL = "select Toleransi_Tf_Min, Toleransi_Tf_Max from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Stock_Owner = '" & Kd_Soo & "' and Kode_Barang = '" & Kd_Barangg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Persen_Toleransi_Min = Dr("Toleransi_Tf_Min")
                            Persen_Toleransi_Max = Dr("Toleransi_Tf_Max")
                        End If
                    End Using

                    Dim nilai_toleransi_min As Double = Jumlah_Kebutuhan_Request - (Jumlah_Kebutuhan_Request * (Persen_Toleransi_Min / 100))
                    Dim nilai_toleransi_max As Double = Jumlah_Kebutuhan_Request + (Jumlah_Kebutuhan_Request * (Persen_Toleransi_Max / 100))


                    If Jumlah_Sudah_Transfer > nilai_toleransi_max Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jumlah Sudah Transfer Lebih Dari Jumlah Request", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If isLastData Then

                        If Jumlah_Sudah_Transfer < nilai_toleransi_min Then
                            MessageBox.Show("Jumlah Sudah Transfer Kurang Dari Jumlah Request", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Else


                            '================================
                            '=       UPDATE DATA FLAG       =
                            '================================
                            SQL = "select a.Kode_Perusahaan "
                            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
                            SQL = SQL & "and a.Status is null "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and c.Urut_Oto = '" & Urut_Det_Convert & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dr.Close()
                                    SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Urut_Det_Convert & "' "
                                    ExecuteTrans(SQL)

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using



                        End If
                    Else
                        If Jumlah_Sudah_Transfer >= nilai_toleransi_min And Jumlah_Sudah_Transfer <= nilai_toleransi_max Then
                            '================================
                            '=       UPDATE DATA FLAG       =
                            '================================
                            SQL = "select a.Kode_Perusahaan "
                            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
                            SQL = SQL & "and a.Status is null "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and c.Urut_Oto = '" & Urut_Det_Convert & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dr.Close()
                                    SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Urut_Det_Convert & "' "
                                    ExecuteTrans(SQL)

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using
                        End If


                    End If


                End If




                '===============================================================
                '=     CEK APAKAH DATA RM GENERAL DAN CEK JUMLAH KEBUTUHAN     =
                '===============================================================
                Dim IsRequestGeneral As Boolean = False
                Dim Jumlah_Kebutuhan_Request_General As Double = 0
                Dim NoRequestGeneral As String = ""
                SQL = "select b.Jumlah as Jumlah_Kebutuhan, a.No_Faktur "
                SQL &= $"from Emi_Material_Requisition_General a "
                SQL &= $"inner join Emi_Material_Requisition_General_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                SQL &= $"where a.Status is NULL "
                SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
                SQL &= $"and b.Urut_Oto = '{Urut_Det_Convert}' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Jumlah_Kebutuhan_Request_General = Val(HilangkanTanda(Dr("Jumlah_Kebutuhan")))
                        NoRequestGeneral = Dr("No_Faktur")
                        IsRequestGeneral = True
                    Else
                        IsRequestGeneral = False
                    End If
                End Using

                If IsRequestGeneral Then
                    '=====================================
                    '=     CEK JUMLAH SUDAH TRANSFER     =
                    '=====================================
                    Dim Jumlah_Sudah_Transfer_General As Double = 0
                    SQL = "select isnull(sum(d.Jumlah), 0) as Jumlah_Transfer "
                    SQL = SQL & "from Tf_Stock_Parent a, Tf_Stock b, Tf_Stock_Det c, Tf_Stock_Det2 d "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                    SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
                    SQL = SQL & "and a.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and b.Urut_Material_Requisition_Convert = " & Urut_Det_Convert & " "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Jumlah_Sudah_Transfer_General = Val(HilangkanTanda(Dr("Jumlah_Transfer")))
                        End If
                    End Using

                    '================================
                    '=     CEK APAKAH LAST DATA     =
                    '================================
                    Dim isLastData_General As Boolean = False
                    SQL = "select c.Serial_Number_Awal, (d.Qr_Code+'-'+d.Kode_Unik_Berjalan) as barcode "
                    SQL = SQL & "from Tf_Stock_Parent a, Tf_Stock b, Tf_Stock_Det c, barang_sn d "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                    SQL = SQL & "and c.Serial_Number_Awal = d.Serial_Number "
                    SQL = SQL & "and a.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' "

                    SQL = SQL & "and not exists ( "
                    SQL = SQL & "select 1 from TF_Stock_Det2 z where z.kode_perusahaan = c.Kode_Perusahaan and z.no_faktur = c.No_Faktur and z.Urut_Det = c.Urut_Oto) "
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            isLastData_General = True
                        End If
                    End Using

                    If Jumlah_Sudah_Transfer_General >= Jumlah_Kebutuhan_Request_General Then

                        '================================
                        '=       UPDATE DATA FLAG       =
                        '================================
                        SQL = "select a.Kode_Perusahaan "
                        SQL &= $"from Emi_Material_Requisition_General a "
                        SQL &= $"inner join Emi_Material_Requisition_General_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                        SQL &= $"where a.Status is NULL "
                        SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"and b.Urut_Oto = '{Urut_Det_Convert}' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                Dr.Close()
                                SQL = "update Emi_Material_Requisition_General_Detail set flag_terpenuhi = 'Y', "
                                SQL = SQL & "tanggal_terpenuhi = '" & Format(tgl_skg, "yyyy-MM-dd") & "', jam_terpenuhi = '" & Format(tgl_skg, "HH:mm:ss") & "', user_terpenuhi = '" & UserID & "' "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Urut_Det_Convert & "' "
                                ExecuteTrans(SQL)

                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using



                    End If

                    '================================================
                    '=       CEK APAKAH SUDAH TERPENUHI SEMUA       =
                    '================================================
                    SQL = "select a.Kode_Perusahaan "
                    SQL &= $"from Emi_Material_Requisition_General a "
                    SQL &= $"inner join Emi_Material_Requisition_General_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                    SQL &= $"where a.Status is NULL and b.Flag_Terpenuhi is null "
                    SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
                    SQL &= $"and a.No_Faktur = '{NoRequestGeneral}' "
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then

                            Dr.Close()
                            SQL = "update Emi_Material_Requisition_General set flag_terpenuhi = 'Y', flag_selesai = 'Y', "
                            SQL = SQL & "tanggal_terpenuhi = '" & Format(tgl_skg, "yyyy-MM-dd") & "', jam_terpenuhi = '" & Format(tgl_skg, "HH:mm:ss") & "', user_terpenuhi = '" & UserID & "' "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoRequestGeneral & "' "
                            ExecuteTrans(SQL)

                        End If
                    End Using

                End If



#End Region




                '=====================================
                '=       GENERATE BARCODE BARU       =
                '=====================================
                kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
                Dim fullNewQr As String = QrLama & "-" & newKodeUnikBerjalan

                Barcode.Image = Generate_QR(fullNewQr)

                Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")
                'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                'End If

                fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                FileSize1 = fs1.Length
                rawData1 = New Byte(FileSize1) {}
                fs1.Read(rawData1, 0, FileSize1)
                fs1.Close()
                Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1


                '===================================
                '=       INSERT BARCODE BARU       =
                '===================================
                Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

                SQL = "delete from Cetak_TransferStock where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
                ExecuteTrans(SQL)

                SQL = "insert into Cetak_TransferStock (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print,tanggal_masuk,metode_pengeluaran_stok) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtKdBarang.Text & "', @newBarcode, '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
                SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "',  "
                SQL = SQL & "'" & tglMsk & "', '" & metodePengeluaranStock & "')"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseTrans()
                CloseConn()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try


        ElseIf CmbJenisTimbang.Text.Trim.ToUpper = "PEMUSNAHAN BARANG" Then

            Dim QrLama As String = ""
            Dim expDate As String = ""
            Dim tglMsk As String = ""
            Dim metodePengeluaranStock As String = ""
            Dim GetDataKodeTransfer, GetDataLokasi, GetDataKdBrg, GetDataNmBrg, GetDataBrgSN, GetDataJmlEstimasi, GetDataSatuanBesar, GetDataSatuanKecil, GetDataUrutOto As String
            Dim GetJumlahBags, GetRakTujuan, GetPalletTujuan, GetWarna As String
            Dim SN As String = ""

            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction
                'Ambil Data SN Berdasar Barcode
                SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.tgl_masuk, b.Metode_Pengeluaran_Stok "
                SQL = SQL & "from barang_sn a, barang b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and a.Jumlah <> 0 "
                SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & TxtBarcode.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                        batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                        SN = Dr("serial_number")
                        expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                        tglMsk = General_Class.CekNULL(Dr("tgl_masuk"))
                        metodePengeluaranStock = Dr("Metode_Pengeluaran_Stok")
                    End If
                End Using


                'Cek data YG Mau di TF, Berdasar SN dr Barcode
                SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, d.nama as Nama_Barang, c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Serial_Number_Awal, "
                SQL = SQL & "b.Satuan_Barang, c.Urut_Oto, c.Warna, c.Id_Wms_Tujuan "
                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, Barang d "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                SQL = SQL & "and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.kode_barang "
                SQL = SQL & "and a.status is null and a.Flag_Validasi is null "
                SQL = SQL & "and b.Flag_Timbang = 'Y' and c.Selesai is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and c.Serial_Number_Awal = '" & SN & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        GetDataKodeTransfer = Dr("No_faktur")
                        GetDataLokasi = Dr("Kode_Stock_Owner")
                        GetDataKdBrg = Dr("Kode_Barang")
                        GetDataNmBrg = Dr("Nama_Barang")
                        GetDataBrgSN = Dr("Serial_Number_Awal")
                        GetDataJmlEstimasi = HilangkanTanda(Format(Dr("Jumlah"), "N2"))
                        GetJumlahBags = Dr("Jumlah_Bags")
                        GetDataSatuanKecil = Dr("Satuan_Barang")
                        GetDataSatuanBesar = Dr("Satuan")
                        GetWarna = Dr("Warna")
                        GetDataUrutOto = Dr("urut_oto")
                        GetRakTujuan = Dr("Id_Wms_Tujuan")

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        kosong()
                        Exit Sub
                    End If
                End Using

                SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
                SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
                SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' and c.urut_oto = '" & GetDataUrutOto & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        If General_Class.CekNULL(Dr("status")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "T" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                'SQL = "Select Top(1) nomor_urut from dbo.N_EMI_Wharehouse_Position_Fn('" & KodePerusahaan & "','" & GetDataLokasi & "', " & GetRakTujuan & ") where "
                'SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null "
                'SQL = SQL & "order by nomor_urut "

                'Using dr = OpenTrans(SQL)
                '    If dr.Read Then
                '        GetPalletTujuan = dr("nomor_urut")
                '    Else
                '        dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                '        Exit Sub
                '    End If
                'End Using



                '====================================
                '=       CONVERT SATUAN KECIL       =
                '====================================
                Dim nilai_kecildetail As Double = Val(HilangkanTanda(TxtBeratBersih.Text))
                'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & GetDataKdBrg & "', '" & CmbSatuan.SelectedItem.ToString & "',"
                'SQL = SQL & "'" & GetDataSatuanKecil & "', '" & HilangkanTanda(TxtBeratBersih.Text) & "' ) as hasil"
                'Using Dr1 = OpenTrans(SQL)
                '    If Dr1.Read Then
                '        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                '            Dr1.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("data konversi satuan kirim tidak ada ")
                '            Exit Sub
                '        End If

                '        nilai_kecildetail = Dr1("hasil")
                '    Else
                '        Dr1.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("data konversi satuan kirim tidak ada ")
                '        Exit Sub
                '    End If
                'End Using


                '============================
                '=       POTONG STOCK       =
                '============================

                Dim nilai_persediaan_min As Double = 0
                SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
                SQL = SQL & "Kode_Stock_Owner='" & GetDataLokasi & "' and Kode_Barang='" & GetDataKdBrg & "' "
                SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        nilai_persediaan_min = dr("rp_persediaan_min")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim Nama As String = ""
                'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
                SQL = "select Nama,round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Nama = dr("nama")
                        If dr("good_stock") < nilai_kecildetail Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf dr("Jumlah_Bags") < Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        Else
                            dr.Close()
                            SQL = "update barang set Good_Stock = Good_Stock - Round(" & nilai_kecildetail & ",4), "
                            SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & " "
                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                            SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
                            ExecuteTrans(SQL)
                        End If
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If dr("jumlah") < nilai_kecildetail Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf dr("Jumlah_Bags") < Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        Else
                            dr.Close()
                            SQL = "update barang_sn set jumlah = jumlah - Round(" & nilai_kecildetail & ",4), "
                            SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & " "
                            SQL = SQL & "where Kode_Stock_Owner='" & GetDataLokasi & "' and Kode_Barang='" & GetDataKdBrg & "' "
                            SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
                            ExecuteTrans(SQL)
                        End If
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetDataLokasi & "' "
                SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using



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
                SQL = SQL & "and a.Kode_Stock_Owner='" & GetDataLokasi & "' "
                SQL = SQL & "and a.Kode_Barang ='" & GetDataKdBrg & "' "
                SQL = SQL & "and a.Serial_Number='" & GetDataBrgSN & "' "
                'SQL = SQL & "and a.Jumlah <> 0 "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                        QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                        batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                        namaBarang = General_Class.CekNULL(Dr("Nama"))
                        expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                        warnaLama = General_Class.CekNULL(Dr("warna"))
                    Loop
                End Using

                'GENERATE SN BARU
                Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

                'INSERT BARANG SN BARU  
                SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
                SQL = SQL & "select Kode_Perusahaan, '" & GetDataLokasi & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & ", "
                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & GetRakTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                SQL = SQL & "Kode_Unik_Asal, '" & GetPalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, 'Y' "
                SQL = SQL & "from Barang_SN "
                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Stock_Owner='" & GetDataLokasi & "' "
                SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "' "
                ExecuteTrans(SQL)

                '============================
                '=       TAMBAH STOCK       =
                '============================

                SQL = "update barang set Good_Stock= Good_Stock + Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & " "
                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
                ExecuteTrans(SQL)

                'CEK KESESUAIAN STOCK
                SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetDataLokasi & "' "
                SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

#Region "JURNAL"

                'dari
                Dim inisial_faktur_dari As String = ""
                Dim akun_persediaan_dari As String = ""
                Dim akun_persediaan_tujuan As String = ""

                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & GetDataLokasi & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        'akun_persediaan_dari = Dr("persediaan")
                        inisial_faktur_dari = Dr("inisial_faktur")

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select c.akun_Persediaan "
                SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.kode_stock_owner = '" & GetDataLokasi & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        akun_persediaan_dari = Dr("akun_Persediaan")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select c.akun_Persediaan "
                SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.kode_stock_owner = '" & GetDataLokasi & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        akun_persediaan_tujuan = Dr("akun_Persediaan")
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
                SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & txtKodeTransfer.Text & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                      Strings.Mid(akun_persediaan_dari, 2, 1),
                      Strings.Mid(Ganti(akun_persediaan_dari), 3),
                      KodePerusahaan, KodeProyek, "Persedian " & txtKodeTransfer.Text, "0", nilai_persediaan_min, pagenumber, GetDataLokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                     Strings.Mid(akun_persediaan_tujuan, 2, 1),
                     Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                     KodePerusahaan, KodeProyek, "Persedian " & txtKodeTransfer.Text, nilai_persediaan_min, "0", pagenumber, GetDataLokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
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


#End Region

                'PERHATIKAN VOUCHER DIBAWAH INI
                'Dim Kode_voucher As String = "TESVOUCHER"

                SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
                SQL = SQL & "Serial_Number, Jumlah, jumlah_bags, UserID, Tanggal, Jam, Kode_Voucher) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & GetDataKodeTransfer & "', '" & GetDataUrutOto & "', "
                SQL = SQL & "'" & GetPalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', '" & Val(HilangkanTanda(TxtJumlahBagsDetail.Text)) & "', "
                SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "'" & Kode_voucher & "') "
                ExecuteTrans(SQL)

                ''=====================================
                ''=       GENERATE BARCODE BARU       =
                ''=====================================
                'kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
                'Dim fullNewQr As String = QrLama & "-" & newKodeUnikBerjalan

                'Barcode.Image = Generate_QR(fullNewQr)

                'Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")
                ''If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                'Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                ''End If

                'fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                'FileSize1 = fs1.Length
                'rawData1 = New Byte(FileSize1) {}
                'fs1.Read(rawData1, 0, FileSize1)
                'fs1.Close()
                'Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1


                ''===================================
                ''=       INSERT BARCODE BARU       =
                ''===================================
                'Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

                'SQL = "delete from Cetak_TransferStock where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
                'ExecuteTrans(SQL)

                'SQL = "insert into Cetak_TransferStock (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print,tanggal_masuk,metode_pengeluaran_stok) values "
                'SQL = SQL & "('" & KodePerusahaan & "', '" & TxtKdBarang.Text & "', @newBarcode, '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
                'SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "',  "
                'SQL = SQL & "'" & tglMsk & "', '" & metodePengeluaranStock & "')"
                'ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseTrans()
                CloseConn()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If


        If CmbJenisTimbang.Text.Trim.ToUpper = "BARANG MASUK" Then
            cetakmasuk()
            Emi_Display_Timbang_FloorScale.kosong()
            Me.Close()
        ElseIf CmbJenisTimbang.Text.Trim.ToUpper = "TRANSFER STOCK" Then


            'Dim result As DialogResult
            'result = MessageBox.Show("Apakah Proses Penimbangan akan di Lanjutkan ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ''If result = DialogResult.Yes Then
            '' Tindakan jika memilih Yes
            'kosong_sebagian()
            '    If Val(HilangkanTanda(Txt_Sisa_Jumlah.Text)) = 0 Then
            '        MessageBox.Show("Transfer sudah terpenuhi", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Try
            '            OpenConn()

            '            SQL = "update Tf_Stock_det set  "
            '            SQL = SQL & "Selesai = 'Y' "
            '            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            '            SQL = SQL & "and urut_oto = '" & txtUrutOto.Text & "' "
            '            ExecuteTrans(SQL)


            '            CloseConn()
            '        Catch ex As Exception
            '            CloseConn()
            '            MessageBox.Show(ex.Message)
            '            Exit Sub
            '        End Try

            '        cetaktransfer(kode_unik_print)
            '        Emi_Display_Transfer.kosong()
            '        Me.Close()
            '        Exit Sub
            '    End If

            '    cetaktransfer(kode_unik_print)

            'Else

            Try
                OpenConn()

                SQL = "update Tf_Stock_det set  "
                SQL = SQL & "Selesai = 'Y' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and urut_oto = '" & txtUrutOto.Text & "' "
                ExecuteTrans(SQL)

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            cetaktransfer(kode_unik_print)
            Emi_Display_Transfer.kosong()
            Me.Close()

            TutupKoneksiTimbangan()
            'End If


        ElseIf CmbJenisTimbang.Text.Trim.ToUpper = "PEMUSNAHAN BARANG" Then

            Dim result As DialogResult
            result = MessageBox.Show("Apakah Proses Penimbangan akan di Lanjutkan ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Tindakan jika memilih Yes
                kosong_sebagian()
                If Val(HilangkanTanda(Txt_Sisa_Jumlah.Text)) = 0 Then
                    MessageBox.Show("Transfer sudah terpenuhi", "Pemusnahan Barang", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Try
                        OpenConn()

                        SQL = "update N_EMI_Transaksi_Transfer_Waste_Det set  "
                        SQL = SQL & "Selesai = 'Y' "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and urut_oto = '" & txtUrutOto.Text & "' "
                        ExecuteTrans(SQL)


                        CloseConn()
                    Catch ex As Exception
                        CloseConn()
                        MessageBox.Show(ex.Message)
                        Exit Sub
                    End Try

                    'cetaktransfer(kode_unik_print)
                    Emi_Display_Transfer.kosong()
                    Me.Close()
                    Exit Sub
                End If

                'cetaktransfer(kode_unik_print)

            Else

                Try
                    OpenConn()

                    SQL = "update N_EMI_Transaksi_Transfer_Waste_Det set  "
                    SQL = SQL & "Selesai = 'Y' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and urut_oto = '" & txtUrutOto.Text & "' "
                    ExecuteTrans(SQL)

                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                'cetaktransfer(kode_unik_print)
                EMI_Display_Transfer.kosong()
                Me.Close()

                TutupKoneksiTimbangan()
            End If

        End If




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

    Private Sub cetaktransfer(ByVal kode_unikPrintBarcode As String)
        Try
            OpenConn()


            If CmbJenisTimbang.Text.Trim.ToUpper = "TRANSFER STOCK" Then

                '=================================
                '=     CETAK FAKTUR TF STOCK     =
                '=================================
                Dim CrDoc As New Object
                Dim kertas As String = ""


                Dim Selesai As String = ""
                SQL = "Select a.Kode_Perusahaan from Tf_Stock_Parent a, Tf_Stock_det b where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And "
                SQL = SQL & "a.No_Faktur = b.No_Faktur And a.Status Is null And "
                SQL = SQL & "b.selesai Is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & txtKodeTransfer.Text & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Selesai = "T"
                    Else
                        Selesai = "Y"
                    End If
                End Using

                If Selesai = "Y" Then
                    SQL = "select a.Kode_Perusahaan "
                    SQL = SQL & "from Vw_tf_stock_detail a "
                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Faktur = '" & txtKodeTransfer.Text & "' "
                    Using Ds = BindingTrans(SQL)
                        If Ds.Tables("MyTable").Rows.Count <> 0 Then

                            CrDoc = New Rpt_EMI_Faktur_Transfer_Stock_Detail
                            kertas = "Faktur"

                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = "{Vw_tf_stock_detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_tf_stock_detail.No_Faktur}='" & txtKodeTransfer.Text & "' "
                            '    CrDoc.SummaryInfo.ReportTitle = "TF"
                            '    .Text = "TF"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '============================================================================================================================================
                            '============================================================================================================================================
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = PrinterNameTS
                            CrDoc.RecordSelectionFormula = "{Vw_tf_stock_detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_tf_stock_detail.No_Faktur}='" & txtKodeTransfer.Text & "' "
                            'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            doctoprint.PrinterSettings.PrinterName = PrinterNameTS
                            doctoprint.DefaultPageSettings.Landscape = True
                            Dim rawKind As Integer
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                    CrDoc.PrintOptions.PaperSize = rawKind
                                    Exit For
                                End If
                            Next

                            CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                            CrDoc.PrintToPrinter(1, False, 1, 99)

                            MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                        End If
                    End Using
                End If


                '=================================
                '=     CETAK FAKTUR BARCODE     =
                '=================================
                Dim kertasBarcode As String = ""
                SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & kode_unikPrintBarcode & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New NewBarcodeTransferStock
                        kertasBarcode = "BarcodeFG"
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unikPrintBarcode & "' "

                        CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcode Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        CrDoc.PrintToPrinter(1, False, 1, 2500)


                    End If
                End Using

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub

    Private Sub cetakmasuk()

        'Dim tanya As String = MessageBox.Show("Yakin ingin mencetak data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        'If tanya = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()

            Dim kolom_1 As Integer = 1
            Dim kolom_2 As Integer = 2
            Dim sql1 As String = ""
            Dim sql2 As String = ""
            Dim sudah_execute As String = "belum"
            Dim X As String = ""



            SQL = "delete Cetak_Barang_Masuk_Perpallet "
            ExecuteTrans(SQL)

            SQL = "select Tahun_Mulai_Produksi from Init"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    tahunMulaiProduksi = If(General_Class.CekNULL(dr("Tahun_Mulai_Produksi")) = "", "0", dr("Tahun_Mulai_Produksi"))
                End If


            End Using

            Dim sudahCetak As Boolean = False

            'SQL = "Select a.no_faktur, a.No_Pembelian_Loading, b.kode_stock_owner, b.Kode_Barang, c.Nama, b.Tgl_Produksi, b.Tgl_Expired, "
            'SQL = SQL & "b.Jumlah, b.Satuan, b.Jumlah_Bags, b.Nilai_Pengali, b.Nilai_Barang, b.Satuan_Barang, b.urut_oto, "
            'SQL = SQL & "a.no_sj, a.no_plat, b.Urut_Loading, a.kode_supplier, a.Sdh_Cetak, a.Metode_Timbang, a.Flag_Timbang "
            'SQL = SQL & "From EMI_Barang_Masuk_Perpallet a, EMI_Barang_Masuk_Perpallet_Detail b, Barang c "
            'SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "And a.No_Faktur = b.No_Faktur And b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            'SQL = SQL & "And b.Kode_Barang = c.Kode_Barang and a.no_faktur = '" & txtKodeTransfer.Text & "' "
            'SQL = SQL & "order by urut_oto "
            SQL = "Select a.no_faktur, a.No_Pembelian_Loading, b.kode_stock_owner, b.Kode_Barang, c.Nama, b.Tgl_Produksi, a.Tgl_Expired_Real as Tgl_Expired, "
            SQL = SQL & "b.Jumlah, b.Satuan, b.Jumlah_Bags, b.Nilai_Pengali, b.Nilai_Barang, b.Satuan_Barang, b.urut_oto, "
            SQL = SQL & "a.no_sj, a.no_plat, b.Urut_Loading, a.kode_supplier, a.Sdh_Cetak, a.Metode_Timbang, a.Flag_Timbang, "
            SQL = SQL & "c.Metode_Pengeluaran_Stok, d.Tanggal as Tanggal_Masuk "
            SQL = SQL & "From EMI_Barang_Masuk_Perpallet a,EMI_Barang_Masuk_Perpallet_Detail b, Barang c, EMI_Register_Kendaraan_BM d "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "And a.No_Faktur = b.No_Faktur And b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Pembelian_Loading =  d.No_Fak_Loading_Barang "
            SQL = SQL & "And b.Kode_Barang = c.Kode_Barang and a.no_faktur = '" & txtKodeTransfer.Text & "' "
            SQL = SQL & "order by urut_oto "
            Using Ds = BindingTrans(SQL)

                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    'Cmd = New SqlClient.SqlCommand
                    'Cmd.Connection = Cn
                    'Cmd.CommandType = CommandType.Text

                    Dim batch As String = ""
                    Dim Qr As String = ""

                    For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
                        Dim kodeUnikBerjalan As String = ""
                        Dim kodeUnikAsal As String = ""

                        '======================================
                        '=       CEK APAKAH FLOORSCALE      =
                        '======================================

                        If General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Metode_Timbang")) = "FLOOR SCALE" Then
                            If General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Flag_Timbang")) <> "Y" Then
                                CloseConn()
                                MessageBox.Show("Harap Timbang Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                        End If


                        '======================================
                        '=       CEK SUDAH PERNAH CETAK?      =
                        '======================================
                        If General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Sdh_Cetak")) = "Y" Then
                            sudahCetak = True

                        End If

                        '==================================
                        '=       CEK PO LOADING DET       =
                        '==================================

                        SQL = "select  "
                        SQL = SQL & "ISNULL((sum(b.Tot_Batch_Masuk)), 0) as Batch_Masuk, "
                        SQL = SQL & "a.Kode_Supplier, a.Tanggal_Masuk, c.Tgl_Expired_Real as Tanggal_Expired, b.Kode_Barang, c.Kode_Unik_Berjalan "
                        SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b, EMI_Barang_Masuk_Perpallet c "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.No_Faktur = c.No_Pembelian_Loading "
                        SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                        SQL = SQL & "and a.Status is null "
                        SQL = SQL & "and c.No_Faktur='" & Ds.Tables("MyTable").Rows(i).Item("no_faktur") & "' "
                        SQL = SQL & "and b.Kode_Barang='" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "' "
                        SQL = SQL & "and b.Urut_OTO='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "' "
                        SQL = SQL & "group by a.Kode_Supplier, a.Tanggal_Masuk, c.Tgl_Expired_Real, b.Kode_Barang, c.Kode_Unik_Berjalan "
                        Using Ds2 = BindingTrans(SQL)
                            With Ds2.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For j As Integer = 0 To .Rows.Count - 1

                                        Dim expDate As String = ""
                                        Dim tanggalDatang As DateTime = Ds2.Tables("MyTable").Rows(j).Item("Tanggal_Masuk")
                                        Dim SupplierKode As String = Ds2.Tables("MyTable").Rows(j).Item("Kode_Supplier").ToString
                                        Dim tanggalMasuk As Integer = tanggalDatang.Day
                                        Dim bulanMasuk As Integer = tanggalDatang.Month
                                        Dim tahunMasuk As Integer = (tanggalDatang.Year - tahunMulaiProduksi) Mod 9

                                        If tahunMasuk = 0 Then tahunMasuk = 9

                                        'Dim expDate As DateTime = Format(Ds2.Tables("MyTable").Rows(j).Item("Tanggal_Expired"), "yyy-MM-dd")
                                        Dim barangKode As String = Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang").ToString

                                        SQL = "select metode_pengeluaran_Stok from barang "
                                        SQL = SQL & "where kode_barang='" & Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang") & "' "
                                        SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                        SQL = SQL & "group by metode_pengeluaran_Stok"
                                        Using Dr = OpenTrans(SQL)
                                            Do While Dr.Read
                                                If General_Class.CekNULL(Dr("metode_pengeluaran_Stok")) = "FIFO" Then
                                                    expDate = "000000"
                                                Else
                                                    expDate = Format(Ds2.Tables("MyTable").Rows(j).Item("Tanggal_Expired"), "ddMMyy").ToString()
                                                End If
                                            Loop
                                        End Using


                                        If .Rows(i).Item("Batch_Masuk") = "0" Then

                                            kodeUnikBerjalan = Generate_Random_Kode(10).ToUpper
                                            kodeUnikAsal = kodeUnikBerjalan

                                            '==============================================
                                            '=       CEK SELURUH TRANSAKSI HARI INI       =
                                            '==============================================
                                            SQL = "select isnull(count(Tot_Batch_Masuk),0) as Jmlh_Masuk_Hari_ini "
                                            SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b "
                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                                            SQL = SQL & "and b.Kode_Barang='" & Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang") & "' "
                                            SQL = SQL & "and a.Tanggal_Masuk='" & Format(tgl_skg, "yyyy-MM-dd") & "' "
                                            SQL = SQL & "and a.Kode_Supplier = '" & Ds2.Tables("MyTable").Rows(j).Item("Kode_Supplier") & "' "
                                            SQL = SQL & "and a.Status is null "
                                            SQL = SQL & "and Tot_Batch_Masuk is not null "
                                            Using Ds3 = BindingTrans(SQL)

                                                If .Rows.Count <> 0 Then
                                                    For k As Integer = 0 To .Rows.Count - 1

                                                        '==========================
                                                        '=      UPDATE DATA       =
                                                        '==========================
                                                        SQL = "update emi_pembelian_loading_detail set Tot_Batch_Masuk=" & Ds3.Tables("MyTable").Rows(k).Item("Jmlh_Masuk_Hari_ini") & " + 1 "
                                                        SQL = SQL & "where No_Faktur='" & Ds.Tables("MyTable").Rows(i).Item("No_Pembelian_Loading") & "' and Urut_Oto='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "'"
                                                        ExecuteTrans(SQL)

                                                        Dim SupOrder As Integer = Val(Ds3.Tables("MyTable").Rows(k).Item("Jmlh_Masuk_Hari_ini")) + 1

                                                        batch = Generate_Batch_Bahan(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
                                                        Qr = Generate_QR_Batch(barangKode, batch)


                                                    Next
                                                End If

                                            End Using
                                        Else
                                            If sudahCetak = True Then
                                                kodeUnikBerjalan = .Rows(j).Item("Kode_Unik_Berjalan")
                                                kodeUnikAsal = kodeUnikBerjalan
                                                Dim SupOrder As Integer = Val(.Rows(j).Item("Batch_Masuk"))

                                                batch = Generate_Batch_Bahan(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
                                                Qr = Generate_QR_Batch(barangKode, batch)
                                            Else
                                                kodeUnikBerjalan = Generate_Random_Kode(10).ToUpper
                                                kodeUnikAsal = kodeUnikBerjalan
                                                Dim SupOrder As Integer = Val(.Rows(j).Item("Batch_Masuk"))

                                                batch = Generate_Batch_Bahan(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
                                                Qr = Generate_QR_Batch(barangKode, batch)
                                            End If

                                        End If
                                    Next

                                Else
                                    Exit Sub
                                End If
                            End With
                        End Using


                        Barcode.Image = Generate_QR(Qr + "-" + kodeUnikBerjalan)

                        Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, Ds.Tables("MyTable").Rows(i).Item("urut_oto") & "_barang1433.jpg")
                        'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                        Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                        'End If

                        fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                        FileSize1 = fs1.Length
                        rawData1 = New Byte(FileSize1) {}
                        fs1.Read(rawData1, 0, FileSize1)
                        fs1.Close()
                        Cmd.Parameters.Add("@foto1" & Ds.Tables("MyTable").Rows(i).Item("urut_oto"), SqlDbType.Image).Value = rawData1

                        kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")

                        '=================================
                        '=      INSERT TABEL CETAK       =
                        '=================================
                        SQL = "insert into Cetak_barang_Masuk_Perpallet (Kode_Perusahaan, No_Barang_Masuk_Per_Pallet, Kode_Barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, Tanggal_Cetak, "
                        SQL = SQL & "Kode_Unik_Print,tanggal_masuk,metode_pengeluaran_stok ) values "
                        SQL = SQL & "('" & KodePerusahaan & "', '" & txtKodeTransfer.Text & "', '" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "', @foto1" & Ds.Tables("MyTable").Rows(i).Item("urut_oto") & ", "
                        SQL = SQL & "'" & Ds.Tables("MyTable").Rows(i).Item("Nama") & "', '" & Qr & "-" & kodeUnikBerjalan & "', '" & Qr & "', " & Ds.Tables("MyTable").Rows(i).Item("Tgl_Expired") & ", "
                        SQL = SQL & "'" & batch & "', " & Format(tgl_skg, "yyyy-MM-dd") & ", '" & kode_unik_print & "' ,'" & Ds.Tables("MyTable").Rows(i).Item("tanggal_masuk") & "', "
                        SQL = SQL & "'" & Ds.Tables("MyTable").Rows(i).Item("metode_pengeluaran_stok") & "' )"
                        ExecuteTrans(SQL)


                        'SQL = "insert into Cetak_Barang_Masuk_Perpallet(kode_perusahaan, no_barang_masuk_per_pallet, [" & kolom_1 & "], [" & kolom_1 & "a], "
                        'SQL = SQL & "[" & kolom_2 & "], [" & kolom_2 & "a], userid, Qr) values "
                        'SQL = SQL & "('" & KodePerusahaan & "', '" & Lv_BM_PerPallet.FocusedItem.Text & "', "
                        'SQL = SQL & "'" & batch & "', @foto1" & Ds.Tables("MyTable").Rows(i).Item("urut_oto") & ", "
                        ''SQL = SQL & "'" & batch & "', @foto2" & .Rows(i).Item("urut_oto") & ", "
                        'SQL = SQL & "null, null, "
                        'SQL = SQL & "'" & UserID & "', '" & Qr & "-" & kodeUnikBerjalan & "')"
                        'ExecuteTrans(SQL)

                        ''''update
                        If Is2ndPrint = False Then
                            SQL = "update EMI_Barang_Masuk_Perpallet set Sdh_Cetak = 'Y', "
                            SQL = SQL & "batch_number='" & batch & "', QR_Code='" & Qr & "', "
                            SQL = SQL & "kode_unik_berjalan='" & kodeUnikBerjalan & "', kode_unik_asal='" & kodeUnikAsal & "' "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtKodeTransfer.Text & "' "
                            'SQL = SQL & "and userid = '" & UserID & "' "
                            ExecuteTrans(SQL)
                        End If
                    Next

                Else
                    CloseConn()
                    MessageBox.Show("Data pembelian tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()
            'Dim CrDoc As New Object

            SQL = "select kode_perusahaan from Cetak_Barang_Masuk_Perpallet "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_barang_masuk_per_pallet = '" & txtKodeTransfer.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then


                    Dim CrDoc As New BM_PerPallet

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Barang_Masuk_Perpallet.no_barang_masuk_per_pallet} = '" & txtKodeTransfer.Text & "'  and {Cetak_Barang_Masuk_Perpallet.Kode_Unik_Print} = '" & kode_unik_print & "' "

                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    CrDoc.PrintToPrinter(1, False, 1, 2500)




                    ' KODE UNTUK VIEW
                    'Dim CrDoc = New BM_PerPallet
                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Barang_Masuk_Perpallet.no_barang_masuk_per_pallet} = '" & txtKodeTransfer.Text & "'" 'and IsNull({EMI_Barang_Masuk_Perpallet.Sdh_Cetak}) "
                    '    CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
                    '    .Text = "Barang Masuk Per Pallet"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJenisAlas.SelectedIndexChanged
        'If ComboBox1.SelectedItem = -1 Then
        '    TextBox2.Text = ""
        '    TextBox3.Text = ""
        '    Exit Sub
        'End If

        Try
            OpenConn()

            Dim nberat As Double = 0
            Dim convertKeSatuanAsli_bhn As String = ""
            SQL = "select Id,Kode_Jenis_Alas,Keterangan,Berat,Satuan from Emi_Master_Jenis_Alas where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Id = '" & arrid_Jenis_alas.Item(CmbJenisAlas.SelectedIndex) & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    'convertKeSatuanAsli_bhn = dr("Satuan")
                    'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & TxtKdBarang.Text & "',"
                    'SQL = SQL & "'" & dr("satuan") & "','" & CmbSatuan.Text & "',"
                    'SQL = SQL & "" & HilangkanTanda(dr("Berat")) & ") as Hasil "
                    'dr.Close()
                    'Using dr4 = OpenTrans(SQL)
                    '    If dr4.Read Then
                    '        If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                    '            If dr4("Hasil") = 0 Then
                    '                dr4.Close()
                    '                CloseTrans()
                    '                CloseConn()
                    '                MessageBox.Show("Satuan " & convertKeSatuanAsli_bhn & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                Exit Sub
                    '            Else
                    '                nberat = dr4("hasil")
                    '                TxtBeratAlas.Text = Format(nberat, "N2") & " " & CmbSatuan.Text
                    '                TxtBeratAlas_Bersih.Text = Format(nberat, "N2")
                    '            End If
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
                    TxtBeratAlas.Text = Format(nberat, "N2") & " " & CmbSatuan.Text
                    TxtBeratAlas_Bersih.Text = Format(nberat, "N2")
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
        berat_net = Val(HilangkanTanda(txt_Jumlah_Timbang.Text)) - (Val(HilangkanTanda(TxtBeratAlas.Text)) + Val(HilangkanTanda(TxtBeratBags.Text)))
        TxtBeratBersih.Text = Format(berat_net, "N2")
    End Sub

    Private Sub txt_Jumlah_Timbang_TextChanged(sender As Object, e As EventArgs) Handles txt_Jumlah_Timbang.TextChanged
        Dim berat_net As Double = 0
        berat_net = Val(HilangkanTanda(txt_Jumlah_Timbang.Text)) - (Val(HilangkanTanda(TxtBeratAlas.Text))) '+ Val(HilangkanTanda(TxtBeratBags.Text)))
        TxtBeratBersih.Text = Format(berat_net, "N2")
    End Sub

    Private Sub EMI_Timbang_Floor_Scale_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Dim xxxx As String = ""
    End Sub

    Private Sub EMI_Timbang_Floor_Scale_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Dim xxxx As String = ""
        TutupKoneksiTimbangan()
    End Sub

    Private Sub EMI_Timbang_Floor_Scale_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim xxxx As String = ""
        TutupKoneksiTimbangan()
    End Sub

    Private Sub EMI_Timbang_Floor_Scale_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Dim xxxx As String = ""
        TutupKoneksiTimbangan()
    End Sub

    Private Sub EMI_Timbang_Floor_Scale_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim xxxx As String = ""
        TutupKoneksiTimbangan()
    End Sub
End Class
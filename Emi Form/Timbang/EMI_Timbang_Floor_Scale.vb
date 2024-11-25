Imports System.Deployment.Internal
Imports System.IO
Imports System.IO.Ports
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar
Imports Azure.Storage.Blobs
Imports Azure.Storage.Blobs.Models
Imports Microsoft.VisualBasic.ApplicationServices
Imports WebEye.Controls.WinForms.StreamPlayerControl
Imports ZXing.QrCode


Public Class EMI_Timbang_Floor_Scale
    Dim arrcari As New ArrayList
    Dim Jenis = "Transaksi_Timbang_Kosong"
    Public Txt_Ekspedisi As String = ""
    Dim Random As New Random()

    Dim tahunMulaiProduksi As String = ""
    Private Is2ndPrint As Boolean = False

    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Dim arrIdJenisMuatan, arrMetodeTruckScale As New ArrayList
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
            'GroupBox1.text = "Data Timbang"
            lblLokasi.Text = "Lokasi"
            'Lbl_Ekspedisi.Text = Base_Language.Lang_Global_Ekspedisi
            lblBarang.Text = "Nama Barang"

            'lblJumlahEstimasi.Text = "Timbang 1"
            'lblJumlahTimbang.Text = "Timbang 2"



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
        'kosong()

    End Sub



    Public Sub kosong()
        CmbJenisTimbang.Items.Clear()
        CmbJenisTimbang.Items.Add("BARANG MASUK")
        CmbJenisTimbang.Items.Add("TRANSFER STOCK")

        Txt_Timbangan.Text = "99999"


        txt_lokasi.Text = ""
        txt_barang.Text = ""
        'Txt_Ekspedisi.Text = ""
        Txt_Ekspedisi = ""
        txt_barang.Text = ""

        txt_Jml_Estimasi.Text = ""
        txt_Jumlah_Timbang.Text = ""

        'txt_Jml_Estimasi.Enabled = True
        ' txt_Jumlah_Timbang.Enabled = True

        txt_Jumlah_Timbang.Text = Txt_Timbangan.Text

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

            CmbSatuan.Text = satuan_timbang
            CmbSatuan.Enabled = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    'Data PO berdasarkan Supplier



    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then txt_barang.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Txt_Timbangan.Text = "99999"
    End Sub



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If txt_Jumlah_Timbang.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah timbang tidak boleh kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()

        Dim kode_unik_print As String = ""
        Dim batchLama As String = ""

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CmbJenisTimbang.Text.Trim.ToUpper = "BARANG MASUK" Then

                Dim jumlah_masuk_Barang As Double = 0
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
                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & TxtKdBarang.Text & "',"
                SQL = SQL & "'" & CmbSatuan.Text & "','" & Satuan_Barang & "',"
                SQL = SQL & "" & HilangkanTanda(txt_Jumlah_Timbang.Text) & ") as Hasil "
                Using dr3 = OpenTrans(SQL)
                    If dr3.Read Then
                        If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                            jumlah_masuk_Barang = dr3("Hasil")
                        Else
                            MessageBox.Show("Satuan " & CmbSatuan.Text & " Ke " & Satuan_Barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If
                End Using

                SQL = "update EMI_Barang_Masuk_Perpallet set  "
                SQL = SQL & "Flag_Timbang = 'Y', "
                SQL = SQL & "jumlah = '" & HilangkanTanda(txt_Jumlah_Timbang.Text) & "', "
                SQL = SQL & "Nilai_Barang = '" & jumlah_masuk_Barang & "', "
                SQL = SQL & "tanggal_Timbang = '" & Format(CDate(tgl_skg), "yyyy-MM-dd") & "', "
                SQL = SQL & "jam_Timbang = '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
                SQL = SQL & "user_Timbang = '" & UserID & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_faktur = '" & txtKodeTransfer.Text & "' "
                ExecuteTrans(SQL)


                cetak()

            ElseIf CmbJenisTimbang.Text.Trim.ToUpper = "TRANSFER STOCK" Then

                'HAPUS TABEL SEMENTARA
                'SQL = "truncate table Cetak_TransferStock "
                'ExecuteTrans(SQL)

                Dim idWarehousTujuan As String = ""
                Dim noPalletTujaun As String = ""
                Dim jumlahBags As String = ""
                Dim SoTujuan As String = ""

                SQL = "select a.Status,b.Selesai,b.Flag_Pot_Stock, b.Id_Wms_Tujuan, b.No_Pallet_Tujuan, b.Jumlah_Bags, a.SO_Tujuan "
                SQL = SQL & "from tf_stock a, Tf_Stock_det b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Transfer = b.No_Faktur "
                SQL = SQL & "and a.kode_transfer = '" & txtKodeTransfer.Text & "' "
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
                        ElseIf General_Class.CekNULL(Dr("flag_pot_stock")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan, barang sudah pernah ditimbang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        idWarehousTujuan = isNull(Dr("Id_Wms_Tujuan"))
                        noPalletTujaun = isNull(Dr("No_Pallet_Tujuan"))
                        jumlahBags = isNull(Dr("Jumlah_Bags"))
                        SoTujuan = isNull(Dr("SO_Tujuan"))

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using




                '====================================
                '=       CONVERT SATUAN KECIL       =
                '====================================
                Dim nilai_kecildetail As Double = 0
                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKdBarang.Text & "', '" & CmbSatuan.SelectedItem.ToString & "',"
                SQL = SQL & "'" & Txt_SatuanKecil.Text & "', '" & txt_Jumlah_Timbang.Text.ToString & "' ) as hasil"
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If

                        nilai_kecildetail = Dr1("hasil")
                    Else
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using

                '============================
                '=       POTONG STOCK       =
                '============================

                Dim nilai_persediaan_min As Double = 0
                SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
                SQL = SQL & "Kode_Stock_Owner='" & txt_lokasi.Text & "' and Kode_Barang='" & TxtKdBarang.Text & "' "
                SQL = SQL & "and Serial_Number='" & txt_Barang_SN.Text & "'"
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
                SQL = "select Nama,round(good_stock,2) as good_stock,Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & txt_lokasi.Text & "' "
                SQL = SQL & "and Kode_Barang='" & TxtKdBarang.Text & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Nama = dr("nama")
                        If dr("good_stock") < nilai_kecildetail Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf dr("Jumlah_Bags") < jumlahBags Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        Else
                            dr.Close()
                            SQL = "update barang set Good_Stock = Good_Stock - " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags - " & jumlahBags & " "
                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & txt_lokasi.Text & "' "
                            SQL = SQL & " and Kode_Barang='" & TxtKdBarang.Text & "'"
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

                SQL = "select round(jumlah,2) as jumlah,Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & txt_lokasi.Text & "' "
                SQL = SQL & "and Kode_Barang='" & TxtKdBarang.Text & "' "
                SQL = SQL & "and Serial_Number='" & txt_Barang_SN.Text & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If dr("jumlah") < nilai_kecildetail Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf dr("Jumlah_Bags") < jumlahBags Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        Else
                            dr.Close()
                            SQL = "update barang_sn set jumlah = jumlah - " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags - " & jumlahBags & " "
                            SQL = SQL & "where Kode_Stock_Owner='" & txt_lokasi.Text & "' and Kode_Barang='" & TxtKdBarang.Text & "' "
                            SQL = SQL & "and Serial_Number='" & txt_Barang_SN.Text & "'"
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
                SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & txt_lokasi.Text & "' "
                SQL = SQL & "AND a.Kode_Barang = '" & TxtKdBarang.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
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
                Dim QrLama As String = ""
                Dim namaBarang As String = "" '
                Dim expDate As String = ""

                'Ambil Data Lama
                SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired "
                SQL = SQL & "from barang_sn a, barang b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner='" & txt_lokasi.Text & "' "
                SQL = SQL & "and a.Kode_Barang ='" & TxtKdBarang.Text & "' "
                SQL = SQL & "and a.Serial_Number='" & txt_Barang_SN.Text & "' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                        QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                        batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                        namaBarang = General_Class.CekNULL(Dr("Nama"))
                        expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                    Loop
                End Using

                'GENERATE SN BARU
                Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

                'INSERT BARANG SN BARU  
                SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number) "
                SQL = SQL & "select Kode_Perusahaan, '" & SoTujuan & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & jumlahBags & ", "
                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & idWarehousTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                SQL = SQL & "Kode_Unik_Asal, '" & noPalletTujaun & "', batch_number "
                SQL = SQL & "from Barang_SN "
                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Stock_Owner='" & txt_lokasi.Text & "' "
                SQL = SQL & "and Kode_Barang='" & TxtKdBarang.Text & "' "
                SQL = SQL & "and Serial_Number='" & txt_Barang_SN.Text & "' "
                ExecuteTrans(SQL)

                '============================
                '=       TAMBAH STOCK       =
                '============================

                SQL = "update barang set Good_Stock= Good_Stock + " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags + " & jumlahBags & " "
                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & SoTujuan & "' "
                SQL = SQL & " and Kode_Barang='" & TxtKdBarang.Text & "'"
                ExecuteTrans(SQL)

                'CEK KESESUAIAN STOCK
                SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & SoTujuan & "' "
                SQL = SQL & "AND a.Kode_Barang = '" & TxtKdBarang.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
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

                SQL = "update Tf_Stock_det set  "
                SQL = SQL & "Flag_Pot_Stock = 'Y', "
                SQL = SQL & "jumlah_pot_Stock = '" & HilangkanTanda(txt_Jumlah_Timbang.Text) & "', "
                SQL = SQL & "tanggal_pot_stock = '" & Format(CDate(tgl_skg), "yyyy-MM-dd") & "', "
                SQL = SQL & "jam_pot_stock = '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
                SQL = SQL & "userid_pot_stock = '" & UserID & "', "
                SQL = SQL & "serial_number_akhir='" & SN_Baru & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_faktur = '" & txtKodeTransfer.Text & "' "
                ExecuteTrans(SQL)


                'dari
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

                SQL = "select a.Flag_Raw_Material,a.Flag_Finished_Good,a.Flag_Semi_FG,a.Flag_Scrap, a.Flag_Packaging "
                SQL = SQL & "from Barang b,EMI_Group_Jenis a where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.kode_stock_owner = '" & txt_lokasi.Text & "' and b.Kode_Barang='" & TxtKdBarang.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        fRaw_Material_dari = Dr("Flag_Raw_Material")
                        fFinished_Good_dari = Dr("Flag_Finished_Good")
                        fSemi_FG_dari = Dr("Flag_Semi_FG")
                        fScrap_dari = Dr("Flag_Scrap")
                        fPackaging_dari = Dr("Flag_Packaging")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & txt_lokasi.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        'akun_persediaan_dari = Dr("persediaan")
                        inisial_faktur_dari = Dr("inisial_faktur")
                        If fRaw_Material_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan_Bahan_Baku")
                        ElseIf fFinished_Good_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan")
                        ElseIf fSemi_FG_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan_Bahan_Setengah_Jadi")
                        ElseIf fScrap_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan_Scrap")
                        ElseIf fPackaging_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan_Packaging")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select a.Flag_Raw_Material,a.Flag_Finished_Good,a.Flag_Semi_FG,a.Flag_Scrap, a.Flag_Packaging "
                SQL = SQL & "from Barang b,EMI_Group_Jenis a where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.kode_stock_owner = '" & SoTujuan & "' and b.Kode_Barang='" & TxtKdBarang.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        fRaw_Material_tujuan = Dr("Flag_Raw_Material")
                        fFinished_Good_tujuan = Dr("Flag_Finished_Good")
                        fSemi_FG_tujuan = Dr("Flag_Semi_FG")
                        fScrap_tujuan = Dr("Flag_Scrap")
                        fPackaging_tujuan = Dr("Flag_Packaging")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & SoTujuan & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        'akun_persediaan_dari = Dr("persediaan")
                        If fRaw_Material_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan_Bahan_Baku")
                        ElseIf fFinished_Good_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan")
                        ElseIf fSemi_FG_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan_Bahan_Setengah_Jadi")
                        ElseIf fScrap_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan_Scrap")
                        ElseIf fPackaging_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan_Packaging")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
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
                      KodePerusahaan, KodeProyek, "Persedian " & txtKodeTransfer.Text, "0", nilai_persediaan_min, pagenumber, "TSSS")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                     Strings.Mid(akun_persediaan_tujuan, 2, 1),
                     Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                     KodePerusahaan, KodeProyek, "Persedian " & txtKodeTransfer.Text, nilai_persediaan_min, "0", pagenumber, "TSSS")
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



                SQL = "update Tf_Stock set kode_voucher = '" & Kode_voucher & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Transfer = '" & txtKodeTransfer.Text & "' "
                ExecuteTrans(SQL)

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

                SQL = "insert into Cetak_TransferStock (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtKdBarang.Text & "', @newBarcode, '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
                SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' ) "
                ExecuteTrans(SQL)


                'Cmd.Transaction.Commit()
                'CloseTrans()
                'CloseConn()
                ''MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


            End If




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



        Try
            OpenConn()


            If CmbJenisTimbang.Text.Trim.ToUpper = "TRANSFER STOCK" Then
                SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & kode_unik_print & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        Dim CrDoc As New Object
                        CrDoc = New NewBarcodeTransferStock
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "' and {Cetak_TransferStock.batch} = '" & batchLama & "' "
                            CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock"
                            .Text = "New Barcode Transfer Stock"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With

                    End If
                End Using

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If CmbJenisTimbang.Text.Trim.ToUpper = "BARANG MASUK" Then
            EMI_Display_Pallet_Masuk.kosong()
            Me.Close()
        ElseIf CmbJenisTimbang.Text.Trim.ToUpper = "TRANSFER STOCK" Then
            EMI_Display_Transfer.kosong()
            Me.Close()
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


    Private Sub cetak()

        Dim tanya As String = MessageBox.Show("Yakin ingin mencetak data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()

            '''Using Ds = Binding("select * from EMI_Barang_Masuk_Perpallet where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "'")
            '''    If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '''        Dim CrDoc As New BM_PerPallet     'Nama file CR
            '''        With A_Place_For_Printing2
            '''            CrDoc.SetDataSource(Ds)
            '''            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '''            'CrDoc.PrintOptions.PrinterName = PrinterName
            '''            CrDoc.RecordSelectionFormula = "{EMI_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Barang_Masuk_Perpallet.No_faktur} = '" & Lv_BM_PerPallet.FocusedItem.Text & "'"
            '''            CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
            '''            .Text = "Barang Masuk Per Pallet"
            '''            .CrystalReportViewer1.ReportSource = CrDoc
            '''            '.CrystalReportViewer1.DisplayGroupTree = False
            '''            .Refresh()
            '''            .Show()
            '''        End With
            '''    End If
            '''End Using

            Dim kolom_1 As Integer = 1
            Dim kolom_2 As Integer = 2
            Dim sql1 As String = ""
            Dim sql2 As String = ""
            Dim sudah_execute As String = "belum"
            Dim X As String = ""



            SQL = "truncate table Cetak_Barang_Masuk_Perpallet "
            ExecuteTrans(SQL)

            SQL = "select Tahun_Mulai_Produksi from Init"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    tahunMulaiProduksi = If(General_Class.CekNULL(dr("Tahun_Mulai_Produksi")) = "", "0", dr("Tahun_Mulai_Produksi"))
                End If


            End Using

            Dim sudahCetak As Boolean = False

            SQL = "Select a.no_faktur, a.No_Pembelian_Loading, b.kode_stock_owner, b.Kode_Barang, c.Nama, b.Tgl_Produksi, b.Tgl_Expired, "
            SQL = SQL & "b.Jumlah, b.Satuan, b.Jumlah_Bags, b.Nilai_Pengali, b.Nilai_Barang, b.Satuan_Barang, b.urut_oto, "
            SQL = SQL & "a.no_sj, a.no_plat, b.Urut_Loading, a.kode_supplier, a.Sdh_Cetak, a.Metode_Timbang, a.Flag_Timbang "
            SQL = SQL & "From EMI_Barang_Masuk_Perpallet a, EMI_Barang_Masuk_Perpallet_Detail b, Barang c "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "And a.No_Faktur = b.No_Faktur And b.Kode_Stock_Owner = c.Kode_Stock_Owner "
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
                        SQL = SQL & "a.Kode_Supplier, a.Tanggal_Masuk, b.Tanggal_Expired, b.Kode_Barang, c.Kode_Unik_Berjalan "
                        SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b, EMI_Barang_Masuk_Perpallet c "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.No_Faktur = c.No_Pembelian_Loading "
                        SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                        SQL = SQL & "and a.Status is null "
                        SQL = SQL & "and c.No_Faktur='" & Ds.Tables("MyTable").Rows(i).Item("no_faktur") & "' "
                        SQL = SQL & "and b.Kode_Barang='" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "' "
                        SQL = SQL & "and b.Urut_OTO='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "' "
                        SQL = SQL & "group by a.Kode_Supplier, a.Tanggal_Masuk, b.Tanggal_Expired, b.Kode_Barang, c.Kode_Unik_Berjalan "
                        Using Ds2 = BindingTrans(SQL)
                            With Ds2.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For j As Integer = 0 To .Rows.Count - 1

                                        Dim expDate As String = ""
                                        Dim tanggalDatang As DateTime = .Rows(j).Item("Tanggal_Masuk")
                                        Dim SupplierKode As String = .Rows(j).Item("Kode_Supplier").ToString
                                        Dim tanggalMasuk As Integer = tanggalDatang.Day
                                        Dim bulanMasuk As Integer = tanggalDatang.Month
                                        Dim tahunMasuk As Integer = (tanggalDatang.Year - tahunMulaiProduksi) Mod 9

                                        If tahunMasuk = 0 Then tahunMasuk = 9

                                        'Dim expDate As DateTime = Format(Ds2.Tables("MyTable").Rows(j).Item("Tanggal_Expired"), "yyy-MM-dd")
                                        Dim barangKode As String = .Rows(j).Item("Kode_Barang").ToString

                                        SQL = "select metode_pengeluaran_Stok from barang "
                                        SQL = SQL & "where kode_barang='" & .Rows(j).Item("Kode_Barang") & "' "
                                        SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                        SQL = SQL & "group by metode_pengeluaran_Stok"
                                        Using Dr = OpenTrans(SQL)
                                            Do While Dr.Read
                                                If General_Class.CekNULL(Dr("metode_pengeluaran_Stok")) = "FIFO" Then
                                                    expDate = "000000"
                                                Else
                                                    expDate = Format(.Rows(j).Item("Tanggal_Expired"), "ddMMyy").ToString()
                                                End If
                                            Loop
                                        End Using


                                        If .Rows(i).Item("Batch_Masuk") = "0" Then

                                            kodeUnikBerjalan = Generate_Random_Kode(10).ToUpper
                                            kodeUnikAsal = kodeUnikBerjalan

                                            '==============================================
                                            '=       CEK SELURUH TRANSAKSI HARI INI       =
                                            '==============================================
                                            SQL = "select isnull(sum(Tot_Batch_Masuk),0) as Jmlh_Masuk_Hari_ini "
                                            SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b "
                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                            SQL = SQL & "and b.Kode_Barang='" & .Rows(j).Item("Kode_Barang") & "' "
                                            SQL = SQL & "and a.Tanggal_Masuk='" & Format(tgl_skg, "yyyy-MM-dd") & "' "
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


                        '=================================
                        '=      INSERT TABEL CETAK       =
                        '=================================
                        SQL = "insert into Cetak_barang_Masuk_Perpallet (Kode_Perusahaan, No_Barang_Masuk_Per_Pallet, Kode_Barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, Tanggal_Cetak, "
                        SQL = SQL & "Kode_Unik_Print) values "
                        SQL = SQL & "('" & KodePerusahaan & "', '" & txtKodeTransfer.Text & "', '" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "', @foto1" & Ds.Tables("MyTable").Rows(i).Item("urut_oto") & ", "
                        SQL = SQL & "'" & Ds.Tables("MyTable").Rows(i).Item("Nama") & "', '" & Qr & "-" & kodeUnikBerjalan & "', '" & Qr & "', " & Ds.Tables("MyTable").Rows(i).Item("Tgl_Expired") & ", "
                        SQL = SQL & "'" & batch & "', " & Format(tgl_skg, "yyyy-MM-dd") & ", 'Null')"
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
            '''SQL = "select a.kode_perusahaan, a.userid, b.no_faktur, b.sdh_cetak "
            '''SQL = SQL & "from cetak_barang_masuk_Perpallet a, EMI_Barang_Masuk_Perpallet b "
            '''SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.no_barang_masuk_per_pallet = b.No_Faktur "
            '''SQL = SQL & "and b.Sdh_Cetak is null and a.no_barang_masuk_per_pallet = '" & Lv_BM_PerPallet.FocusedItem.Text & "'"
            '''SQL = SQL & "and a.userid = '" & UserID & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then


                    Dim CrDoc As New BM_PerPallet

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Barang_Masuk_Perpallet.no_barang_masuk_per_pallet} = '" & txtKodeTransfer.Text & "'"

                    CrDoc.PrintOptions.PrinterName = "ZDesigner ZD230-203dpi ZPL"

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = "ZDesigner ZD230-203dpi ZPL"

                    CrDoc.PrintToPrinter(1, False, 1, 2500)




                    'KODE LAMA
                    'CrDoc = New BM_PerPallet
                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Barang_Masuk_Perpallet.no_barang_masuk_per_pallet} = '" & Lv_BM_PerPallet.FocusedItem.Text & "'" 'and IsNull({EMI_Barang_Masuk_Perpallet.Sdh_Cetak}) "
                    '    CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
                    '    .Text = "Barang Masuk Per Pallet"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '''CrDoc.SetDataSource(Ds)
                    '''CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '''CrDoc.PrintOptions.PrinterName = ""
                    '''Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    '''doctoprint.PrinterSettings.PrinterName = ""
                    '''A_Place_For_Printing2.CrystalReportViewer1.ReportSource = CrDoc
                    '''A_Place_For_Printing2.Refresh()
                    '''A_Place_For_Printing2.Show()
                End If
            End Using

            'Using Ds = Binding("select * from EMI_Barang_Masuk_Perpallet where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "'")
            '    If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '        Dim CrDoc As New BM_PerPallet     'Nama file CR
            '        With A_Place_For_Printing2
            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            'CrDoc.PrintOptions.PrinterName = PrinterName
            '            CrDoc.RecordSelectionFormula = "{EMI_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Barang_Masuk_Perpallet.No_faktur} = '" & Lv_BM_PerPallet.FocusedItem.Text & "'"
            '            CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
            '            .Text = "Barang Masuk Per Pallet"
            '            .CrystalReportViewer1.ReportSource = CrDoc
            '            '.CrystalReportViewer1.DisplayGroupTree = False
            '            .Refresh()
            '            .Show()
            '        End With
            '    End If
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub



End Class
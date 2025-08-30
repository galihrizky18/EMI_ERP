Public Class SD_Expedisi_PO_Barang_Lain

    Public NoSubPO As String

    Dim arrFilter As New ArrayList

    Dim LvBiaya_NoFak, LvBiaya_NoPenawarn, LvBiaya_TglMulai, LvBiaya_TglSelesai, LvBiaya_JnsPengiriman, LvBiaya_PerusahaanBiayaImport, LvBiaya_MataUang, LvBiaya_KdBiaya, LvBiaya_LokasiAwal, LvBiaya_LokasiTujuan, LvBiaya_Urut As String
    Dim LvBiaya_JnsKendaraan, LvBiaya_KapastiasKendaraan, LvBiaya_Tarif, LvBiaya_BiayaLain, LvBiaya_Total, LvBiaya_IdJnsPengiriman, LvBiaya_IdKdPerusaahanBiayaImport, LvBiaya_IdJnsKendaraan As String

    Dim LvBiaya2_NoFak, LvBiaya2_NoPenawarn, LvBiaya2_TglMulai, LvBiaya2_TglSelesai, LvBiaya2_JnsPengiriman, LvBiaya2_PerusahaanBiayaImport, LvBiaya2_MataUang, LvBiaya2_KdBiaya, LvBiaya2_LokasiAwal, LvBiaya2_LokasiTujuan, LvBiaya2_Urut As String
    Dim LvBiaya2_JnsKendaraan, LvBiaya2_KapastiasKendaraan, LvBiaya2_Tarif, LvBiaya2_BiayaLain, LvBiaya2_Total, LvBiaya2_IdJnsPengiriman, LvBiaya2_IdKdPerusaahanBiayaImport, LvBiaya2_IdJnsKendaraan As String

    Dim item_NoFaktur As Integer = 0
    Dim item_NoPenawarn As Integer = 1
    Dim item_TglMulai As Integer = 2
    Dim item_TglSelesai As Integer = 3
    Dim item_JnsPengiriman As Integer = 4
    Dim item_PerusahaanBiayaImport As Integer = 5
    Dim item_KdBiaya As Integer = 6
    Dim item_MataUang As Integer = 7
    Dim item_JnsKendaraan As Integer = 8
    Dim item_KapasitasKendaraan As Integer = 9
    Dim item_Tarif As Integer = 10
    Dim item_BiayaLain As Integer = 11
    Dim item_Total As Integer = 12
    Dim item_IdJnsPengiriman As Integer = 13
    Dim item_IdKdPerusahaanBiayaImport As Integer = 14
    Dim item_JndKendaraan As Integer = 15
    Dim item_LokasiAwal As Integer = 16
    Dim item_LokasiTujuan As Integer = 17
    Dim item_Urut As Integer = 18

    Dim item2_NoFaktur As Integer = 0
    Dim item2_NoPenawarn As Integer = 1
    Dim item2_TglMulai As Integer = 2
    Dim item2_TglSelesai As Integer = 3
    Dim item2_JnsPengiriman As Integer = 4
    Dim item2_PerusahaanBiayaImport As Integer = 5
    Dim item2_KdBiaya As Integer = 6
    Dim item2_MataUang As Integer = 7
    Dim item2_JnsKendaraan As Integer = 8
    Dim item2_KapasitasKendaraan As Integer = 9
    Dim item2_Tarif As Integer = 10
    Dim item2_BiayaLain As Integer = 11
    Dim item2_Total As Integer = 12
    Dim item2_IdJnsPengiriman As Integer = 13
    Dim item2_IdKdPerusahaanBiayaImport As Integer = 14
    Dim item2_JndKendaraan As Integer = 15
    Dim item2_LokasiAwal As Integer = 16
    Dim item2_LokasiTujuan As Integer = 17
    Dim item2_Urut As Integer = 18



    Private Sub SD_Expedisi_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Lv_BiayaLokal.Columns.Clear()
        Lv_BiayaLokal.Columns.Add("No Faktur", 0, HorizontalAlignment.Left) '0
        Lv_BiayaLokal.Columns.Add("No Penawaran", 130, HorizontalAlignment.Left) '1
        Lv_BiayaLokal.Columns.Add("Tanggal Mulai", 0, HorizontalAlignment.Center) '2
        Lv_BiayaLokal.Columns.Add("Tanggal Selesai", 0, HorizontalAlignment.Center) '3
        Lv_BiayaLokal.Columns.Add("Jenis Pengiriman", 130, HorizontalAlignment.Center) '4
        Lv_BiayaLokal.Columns.Add("Perusahaan Biaya Import", 250, HorizontalAlignment.Left) '5
        Lv_BiayaLokal.Columns.Add("Biaya", 250, HorizontalAlignment.Left) '6
        Lv_BiayaLokal.Columns.Add("Mata Uang", 100, HorizontalAlignment.Center) '7
        Lv_BiayaLokal.Columns.Add("Jenis Kendaraan", 180, HorizontalAlignment.Left) '8
        Lv_BiayaLokal.Columns.Add("Kapasitas Kendaraan", 120, HorizontalAlignment.Center) '9
        Lv_BiayaLokal.Columns.Add("Tarif", 150, HorizontalAlignment.Right) '10
        Lv_BiayaLokal.Columns.Add("Biaya Lain", 150, HorizontalAlignment.Right) '11
        Lv_BiayaLokal.Columns.Add("Total", 150, HorizontalAlignment.Right) '12
        'Hide
        Lv_BiayaLokal.Columns.Add("jnsPengiriman", 0, HorizontalAlignment.Left) '13
        Lv_BiayaLokal.Columns.Add("kdPerusahaanBiayaImport", 0, HorizontalAlignment.Left) '14
        Lv_BiayaLokal.Columns.Add("jnsKendaraan", 0, HorizontalAlignment.Left) '15
        Lv_BiayaLokal.Columns.Add("Lokasi Awal", 300, HorizontalAlignment.Left) '16
        Lv_BiayaLokal.Columns.Add("Lokasi Tujuan", 300, HorizontalAlignment.Left) '17
        Lv_BiayaLokal.Columns.Add("urut", 0, HorizontalAlignment.Left) '18
        Lv_BiayaLokal.View = View.Details

        Lv_BiayaLokal.Columns(item_LokasiAwal).DisplayIndex = 6
        Lv_BiayaLokal.Columns(item_LokasiTujuan).DisplayIndex = 7


        Lv_DataInput.Columns.Clear()
        Lv_DataInput.Columns.Add("No Faktur", 0, HorizontalAlignment.Left) '0
        Lv_DataInput.Columns.Add("No Penawaran", 130, HorizontalAlignment.Left) '1
        Lv_DataInput.Columns.Add("Tanggal Mulai", 0, HorizontalAlignment.Center) '2
        Lv_DataInput.Columns.Add("Tanggal Selesai", 0, HorizontalAlignment.Center) '3
        Lv_DataInput.Columns.Add("Jenis Pengiriman", 130, HorizontalAlignment.Center) '4
        Lv_DataInput.Columns.Add("Perusahaan Biaya Import", 250, HorizontalAlignment.Left) '5
        Lv_DataInput.Columns.Add("Biaya", 250, HorizontalAlignment.Left) '6
        Lv_DataInput.Columns.Add("Mata Uang", 100, HorizontalAlignment.Center) '7
        Lv_DataInput.Columns.Add("Jenis Kendaraan", 180, HorizontalAlignment.Left) '8
        Lv_DataInput.Columns.Add("Kapasitas Kendaraan", 120, HorizontalAlignment.Center) '9
        Lv_DataInput.Columns.Add("Tarif", 150, HorizontalAlignment.Right) '10
        Lv_DataInput.Columns.Add("Biaya Lain", 150, HorizontalAlignment.Right) '11
        Lv_DataInput.Columns.Add("Total", 150, HorizontalAlignment.Right) '12
        'Hide
        Lv_DataInput.Columns.Add("jnsPengiriman", 0, HorizontalAlignment.Left) '13
        Lv_DataInput.Columns.Add("kdPerusahaanBiayaImport", 0, HorizontalAlignment.Left) '14
        Lv_DataInput.Columns.Add("jnsKendaraan", 0, HorizontalAlignment.Left) '15
        Lv_DataInput.Columns.Add("Lokasi Awal", 300, HorizontalAlignment.Left) '16
        Lv_DataInput.Columns.Add("Lokasi Tujuan", 300, HorizontalAlignment.Left) '17
        Lv_DataInput.Columns.Add("urut", 0, HorizontalAlignment.Left) '18
        Lv_DataInput.View = View.Details

        Lv_DataInput.Columns(item2_LokasiAwal).DisplayIndex = 6
        Lv_DataInput.Columns(item2_LokasiTujuan).DisplayIndex = 7

        kosong()
    End Sub

    Private Sub kosong()


        Txt_TotTarif.Text = ""
        Txt_TotBiayaLain.Text = ""
        Txt_GrandTotal.Text = ""

        CmbFilter_KodeBiaya.Items.Clear()
        CmbFilter_Lokasi.Items.Clear()

        Try
            OpenConn()

            CmbFilter_KodeBiaya.Items.Clear()
            SQL = "select Kode_Biaya from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                CmbFilter_KodeBiaya.Items.Add("--- SEMUA ---")
                Do While Dr.Read
                    CmbFilter_KodeBiaya.Items.Add(Dr("Kode_Biaya"))
                Loop
                CmbFilter_KodeBiaya.SelectedIndex = 0
            End Using

            arrFilter.Clear()

            CmbFilter_Lokasi.Items.Clear() : arrFilter.Clear()
            CmbFilter_Lokasi.Items.Add("--- SEMUA ---") : arrFilter.Add("--- SEMUA ---")
            CmbFilter_Lokasi.Items.Add("Lokasi Awal") : arrFilter.Add("(f.nama_provinsi + ' - ' + h.nama_kabupaten_kota + ' - ' + j.nama_kecamatan)")
            CmbFilter_Lokasi.Items.Add("Lokasi Tujuan") : arrFilter.Add("(g.nama_provinsi + ' - ' + i.nama_kabupaten_kota + ' - ' + k.nama_kecamatan)")
            CmbFilter_Lokasi.Items.Add("Perusahaan Biaya Import") : arrFilter.Add("d.Nama")
            CmbFilter_Lokasi.Items.Add("Biaya") : arrFilter.Add("a.Kode_Biaya")
            CmbFilter_Lokasi.Items.Add("Mata Uang") : arrFilter.Add("a.Mata_Uang")
            CmbFilter_Lokasi.Items.Add("Jenis Kendaraan") : arrFilter.Add("e.Keterangan")
            CmbFilter_Lokasi.Items.Add("Kapasitas Kendaraan") : arrFilter.Add("(CAST(e.Kapasitas as Varchar)  +' ' + e.Satuan_Kapasitas)")
            CmbFilter_Lokasi.SelectedIndex = 0

            Txt_ValueKeterangan.Text = ""

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        LoadData(False)
        CekSementara()

    End Sub


    Private Sub Get_Data_Lv(ByVal index As Integer)

        LvBiaya_NoFak = Lv_BiayaLokal.Items(index).SubItems(item_NoFaktur).Text
        LvBiaya_NoPenawarn = Lv_BiayaLokal.Items(index).SubItems(item_NoPenawarn).Text
        LvBiaya_TglMulai = Lv_BiayaLokal.Items(index).SubItems(item_TglMulai).Text
        LvBiaya_TglSelesai = Lv_BiayaLokal.Items(index).SubItems(item_TglSelesai).Text
        LvBiaya_JnsPengiriman = Lv_BiayaLokal.Items(index).SubItems(item_JnsPengiriman).Text
        LvBiaya_PerusahaanBiayaImport = Lv_BiayaLokal.Items(index).SubItems(item_PerusahaanBiayaImport).Text
        LvBiaya_KdBiaya = Lv_BiayaLokal.Items(index).SubItems(item_KdBiaya).Text
        LvBiaya_MataUang = Lv_BiayaLokal.Items(index).SubItems(item_MataUang).Text
        LvBiaya_JnsKendaraan = Lv_BiayaLokal.Items(index).SubItems(item_JnsKendaraan).Text
        LvBiaya_KapastiasKendaraan = Lv_BiayaLokal.Items(index).SubItems(item_KapasitasKendaraan).Text
        LvBiaya_Tarif = Lv_BiayaLokal.Items(index).SubItems(item_Tarif).Text
        LvBiaya_BiayaLain = Lv_BiayaLokal.Items(index).SubItems(item_BiayaLain).Text
        LvBiaya_Total = Lv_BiayaLokal.Items(index).SubItems(item_Total).Text
        LvBiaya_IdJnsPengiriman = Lv_BiayaLokal.Items(index).SubItems(item_IdJnsPengiriman).Text
        LvBiaya_IdKdPerusaahanBiayaImport = Lv_BiayaLokal.Items(index).SubItems(item_IdKdPerusahaanBiayaImport).Text
        LvBiaya_IdJnsKendaraan = Lv_BiayaLokal.Items(index).SubItems(item_JndKendaraan).Text
        LvBiaya_LokasiAwal = Lv_BiayaLokal.Items(index).SubItems(item_LokasiAwal).Text
        LvBiaya_LokasiTujuan = Lv_BiayaLokal.Items(index).SubItems(item_LokasiTujuan).Text
        LvBiaya_Urut = Lv_BiayaLokal.Items(index).SubItems(item_Urut).Text
    End Sub

    Private Sub Get_Data_Lv2(ByVal index As Integer)
        LvBiaya2_NoFak = Lv_DataInput.Items(index).SubItems(item2_NoFaktur).Text
        LvBiaya2_NoPenawarn = Lv_DataInput.Items(index).SubItems(item2_NoPenawarn).Text
        LvBiaya2_TglMulai = Lv_DataInput.Items(index).SubItems(item2_TglMulai).Text
        LvBiaya2_TglSelesai = Lv_DataInput.Items(index).SubItems(item2_TglSelesai).Text
        LvBiaya2_JnsPengiriman = Lv_DataInput.Items(index).SubItems(item2_JnsPengiriman).Text
        LvBiaya2_PerusahaanBiayaImport = Lv_DataInput.Items(index).SubItems(item2_PerusahaanBiayaImport).Text
        LvBiaya2_KdBiaya = Lv_DataInput.Items(index).SubItems(item2_KdBiaya).Text
        LvBiaya2_MataUang = Lv_DataInput.Items(index).SubItems(item2_MataUang).Text
        LvBiaya2_JnsKendaraan = Lv_DataInput.Items(index).SubItems(item2_JnsKendaraan).Text
        LvBiaya2_KapastiasKendaraan = Lv_DataInput.Items(index).SubItems(item2_KapasitasKendaraan).Text
        LvBiaya2_Tarif = Lv_DataInput.Items(index).SubItems(item2_Tarif).Text
        LvBiaya2_BiayaLain = Lv_DataInput.Items(index).SubItems(item2_BiayaLain).Text
        LvBiaya2_Total = Lv_DataInput.Items(index).SubItems(item2_Total).Text
        LvBiaya2_IdJnsPengiriman = Lv_DataInput.Items(index).SubItems(item2_IdJnsPengiriman).Text
        LvBiaya2_IdKdPerusaahanBiayaImport = Lv_DataInput.Items(index).SubItems(item2_IdKdPerusahaanBiayaImport).Text
        LvBiaya2_IdJnsKendaraan = Lv_DataInput.Items(index).SubItems(item2_JndKendaraan).Text
        LvBiaya2_LokasiAwal = Lv_DataInput.Items(index).SubItems(item2_LokasiAwal).Text
        LvBiaya2_LokasiTujuan = Lv_DataInput.Items(index).SubItems(item2_LokasiTujuan).Text
        LvBiaya2_Urut = Lv_DataInput.Items(index).SubItems(item2_Urut).Text
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        LoadData(True)

    End Sub

    Private Sub LoadData(ByVal filter As Boolean)

        get_jam()

        Try
            OpenConn()

            Lv_BiayaLokal.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.No_Penawaran, a.Tanggal_Mulai, a.Tanggal_Selesai, c.Keterangan as Jenis_Pengiriman, d.Nama as Perusahaan_Biaya_Import, a.Kode_Biaya, a.Mata_Uang, "
            SQL = SQL & "e.Keterangan as Jenis_Kendaraan, (CAST(e.Kapasitas as Varchar)  +' ' + e.Satuan_Kapasitas) as Kapasitas_Kendaraan, b.Tarif, b.Biaya_Lain, b.Total, "
            SQL = SQL & "a.Jenis_Pengiriman, a.Kode_Perusahaan_Biaya_import, b.Jenis_Kendaraan, b.No_Urut, "
            SQL = SQL & "(f.nama_provinsi + ' - ' + h.nama_kabupaten_kota + ' - ' + j.nama_kecamatan) as Lokasi_Asal, "
            SQL = SQL & "(g.nama_provinsi + ' - ' + i.nama_kabupaten_kota + ' - ' + k.nama_kecamatan) as Lokasi_Tujuan "
            SQL = SQL & "from Emi_Expedition_PO_Barang_Lain a, Emi_Expedition_PO_Detail_Barang_Lain b, EMI_Cara_Kirim c, perusahaan_biaya_import d, Master_Kendaraan e, "
            SQL = SQL & "tbl_provinsi f, tbl_provinsi  g, tbl_kabupaten_kota h, tbl_kabupaten_kota i, tbl_kecamatan j, tbl_kecamatan k "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan    "
            SQL = SQL & "and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan_Biaya_import = d.Kode_Perusahaan_Biaya_import "
            SQL = SQL & "and a.Jenis_Pengiriman = c.Id_Cara_Kirim "
            SQL = SQL & "and b.Jenis_Kendaraan = e.Id_Kendaraan "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and b.Provinsi_Asal = f.id_provinsi and b.Provinsi_Tujuan = g.id_provinsi "
            SQL = SQL & "and b.Kota_Asal = h.id_kabupaten_kota and b.Kota_Tujuan = i.id_kabupaten_kota "
            SQL = SQL & "and b.Kecamatan_Asal = j.id_kecamatan and b.Kecamatan_Tujuan = k.id_kecamatan "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "AND a.Tanggal_Mulai <= '" & Format(tgl_skg, "yyyy-MM-dd") & "' AND a.Tanggal_Selesai >= '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            If filter Then

                If CmbFilter_KodeBiaya.SelectedIndex > 0 Then
                    SQL = SQL & "and a.Kode_Biaya = '" & CmbFilter_KodeBiaya.SelectedIndex & "' "
                End If

                If Not CmbFilter_Lokasi.SelectedIndex = 0 Then
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & arrFilter.Item(CmbFilter_Lokasi.SelectedIndex) & " like '%" & Trim(Txt_ValueKeterangan.Text) & "%' "
                End If

            End If
            SQL = SQL & "order by d.Nama, a.Tanggal_Mulai "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_BiayaLokal.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Penawaran"))
                    Lv.SubItems.Add(Format(Dr("Tanggal_Mulai"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Format(Dr("Tanggal_Selesai"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jenis_Pengiriman"))
                    Lv.SubItems.Add(Dr("Perusahaan_Biaya_Import"))
                    Lv.SubItems.Add(Dr("Kode_Biaya"))
                    Lv.SubItems.Add(Dr("Mata_Uang"))
                    Lv.SubItems.Add(Dr("Jenis_Kendaraan"))
                    Lv.SubItems.Add(Dr("Kapasitas_Kendaraan"))
                    Lv.SubItems.Add(Format(Dr("Tarif"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Biaya_Lain"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Total"), "N0"))
                    'HIDE
                    Lv.SubItems.Add(Dr("Jenis_Pengiriman"))
                    Lv.SubItems.Add(Dr("Kode_Perusahaan_Biaya_import"))
                    Lv.SubItems.Add(Dr("Jenis_Kendaraan"))

                    Lv.SubItems.Add(Dr("Lokasi_Asal"))
                    Lv.SubItems.Add(Dr("Lokasi_Tujuan"))
                    Lv.SubItems.Add(Dr("No_Urut"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub CekSementara()

        If UserID.Trim.Length = 0 Then
            MessageBox.Show("Faktur Sub PO Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
        End If

        Try
            OpenConn()
            Lv_DataInput.Items.Clear()
            SQL = "select Kode_Perusahaan, No_Faktur_Ekspedisi, No_Penawaran, Kode_Biaya "
            SQL = SQL & "from Emi_Expedition_PO_Sementara_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and No_FakturSubPO = '" & UserID & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim TotBiayaLain As Double = 0
                        Dim TotTarif As Double = 0
                        Dim TotGrand As Double = 0

                        For i As Integer = 0 To .Rows.Count - 1


                            SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.No_Penawaran, a.Tanggal_Mulai, a.Tanggal_Selesai, c.Keterangan as Jenis_Pengiriman, d.Nama as Perusahaan_Biaya_Import, a.Kode_Biaya, a.Mata_Uang, "
                            SQL = SQL & "e.Keterangan as Jenis_Kendaraan, (CAST(e.Kapasitas as Varchar)  +' ' + e.Satuan_Kapasitas) as Kapasitas_Kendaraan, b.Tarif, b.Biaya_Lain, b.Total, "
                            SQL = SQL & "a.Jenis_Pengiriman, a.Kode_Perusahaan_Biaya_import, b.Jenis_Kendaraan, b.No_Urut, "
                            SQL = SQL & "(f.nama_provinsi + ' - ' + h.nama_kabupaten_kota + ' - ' + j.nama_kecamatan) as Lokasi_Asal, "
                            SQL = SQL & "(g.nama_provinsi + ' - ' + i.nama_kabupaten_kota + ' - ' + k.nama_kecamatan) as Lokasi_Tujuan "
                            SQL = SQL & "from Emi_Expedition_PO_Barang_Lain a, Emi_Expedition_PO_Detail_Barang_Lain b, EMI_Cara_Kirim c, Perusahaan_biaya_import d, Master_Kendaraan e, "
                            SQL = SQL & "tbl_provinsi f, tbl_provinsi  g, tbl_kabupaten_kota h, tbl_kabupaten_kota i, tbl_kecamatan j, tbl_kecamatan k "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan    "
                            SQL = SQL & "and b.Kode_Perusahaan = e.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                            SQL = SQL & "and a.Kode_Perusahaan_Biaya_import = d.Kode_Perusahaan_Biaya_import "
                            SQL = SQL & "and a.Jenis_Pengiriman = c.Id_Cara_Kirim "
                            SQL = SQL & "and b.Jenis_Kendaraan = e.Id_Kendaraan "
                            SQL = SQL & "and a.status is null "
                            SQL = SQL & "and b.Provinsi_Asal = f.id_provinsi and b.Provinsi_Tujuan = g.id_provinsi "
                            SQL = SQL & "and b.Kota_Asal = h.id_kabupaten_kota and b.Kota_Tujuan = i.id_kabupaten_kota "
                            SQL = SQL & "and b.Kecamatan_Asal = j.id_kecamatan and b.Kecamatan_Tujuan = k.id_kecamatan "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "AND a.No_Faktur = '" & .Rows(i).Item("No_Faktur_Ekspedisi") & "' and a.No_Penawaran = '" & .Rows(i).Item("No_Penawaran") & "' and a.Kode_Biaya = '" & .Rows(i).Item("Kode_Biaya") & "' "
                            SQL = SQL & "AND a.Tanggal_Mulai <= '" & Format(tgl_skg, "yyyy-MM-dd") & "' AND a.Tanggal_Selesai >= '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
                            SQL = SQL & "order by d.Nama, a.Tanggal_Mulai "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then


                                    Dim Lv As ListViewItem
                                    Lv = Lv_DataInput.Items.Add(Dr("No_Faktur"))
                                    Lv.SubItems.Add(Dr("No_Penawaran"))
                                    Lv.SubItems.Add(Format(Dr("Tanggal_Mulai"), "dd MMM yyyy"))
                                    Lv.SubItems.Add(Format(Dr("Tanggal_Selesai"), "dd MMM yyyy"))
                                    Lv.SubItems.Add(Dr("Jenis_Pengiriman"))
                                    Lv.SubItems.Add(Dr("Perusahaan_Biaya_Import"))
                                    Lv.SubItems.Add(Dr("Kode_Biaya"))
                                    Lv.SubItems.Add(Dr("Mata_Uang"))
                                    Lv.SubItems.Add(Dr("Jenis_Kendaraan"))
                                    Lv.SubItems.Add(Dr("Kapasitas_Kendaraan"))
                                    Lv.SubItems.Add(Format(Dr("Tarif"), "N0"))
                                    Lv.SubItems.Add(Format(Dr("Biaya_Lain"), "N0"))
                                    Lv.SubItems.Add(Format(Dr("Total"), "N0"))
                                    'HIDE
                                    Lv.SubItems.Add(Dr("Jenis_Pengiriman"))
                                    Lv.SubItems.Add(Dr("Kode_Perusahaan_Biaya_import"))
                                    Lv.SubItems.Add(Dr("Jenis_Kendaraan"))

                                    Lv.SubItems.Add(Dr("Lokasi_Asal"))
                                    Lv.SubItems.Add(Dr("Lokasi_Tujuan"))
                                    Lv.SubItems.Add(Dr("No_Urut"))


                                    TotBiayaLain += Val(HilangkanTanda(Dr("Biaya_Lain")))
                                    TotTarif += Val(HilangkanTanda(Dr("Tarif")))
                                    TotGrand += Val(HilangkanTanda(Dr("Total")))

                                End If
                            End Using


                        Next

                        Txt_TotBiayaLain.Text = Format(TotBiayaLain, "N0")
                        Txt_TotTarif.Text = Format(TotBiayaLain, "N0")
                        Txt_GrandTotal.Text = Format(TotGrand, "N0")

                    End If
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_DataInput_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_DataInput.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Lv_DataInput.SelectedItems.Count > 0 Then
                Dim result = MessageBox.Show("Hapus data yang dipilih?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    For Each item As ListViewItem In Lv_DataInput.SelectedItems
                        Lv_DataInput.Items.Remove(item)
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub

    Private Sub BtnPO_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPO_Simpan.Click


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            SQL = "Delete Emi_Expedition_PO_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "'"
            ExecuteTrans(SQL)


            For i As Integer = 0 To Lv_DataInput.Items.Count - 1
                Get_Data_Lv2(i)

                SQL = "select Kode_Perusahaan from Emi_Expedition_PO_Sementara_Barang_Lain  "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and UserID = '" & UserID.Trim & "' and No_Faktur_Ekspedisi = '" & LvBiaya2_NoFak & "' "
                SQL = SQL & "and No_Penawaran = '" & LvBiaya2_NoPenawarn & "' and Kode_Biaya = '" & LvBiaya2_KdBiaya & "' "
                SQL = SQL & "and urut_Penawaran = '" & LvBiaya2_Urut & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count = 0 Then


                            SQL = "insert into Emi_Expedition_PO_Sementara_Barang_Lain (Kode_Perusahaan, No_FakturSubPO, No_Faktur_Ekspedisi, No_Penawaran, Kode_Biaya, Mata_Uang, Tarif, BiayaLain, Total, userid, Urut_Penawaran) "
                            SQL = SQL & "values ('" & KodePerusahaan & "', '" & UserID.Trim & "', '" & LvBiaya2_NoFak & "', "
                            SQL = SQL & "'" & LvBiaya2_NoPenawarn & "', '" & LvBiaya2_KdBiaya & "', '" & LvBiaya2_MataUang & "', "
                            SQL = SQL & Val(HilangkanTanda(LvBiaya2_Tarif)) & ", '" & Val(HilangkanTanda(LvBiaya2_BiayaLain)) & "', '" & Val(HilangkanTanda(LvBiaya2_Total)) & "', '" & UserID & "', '" & LvBiaya2_Urut & "') "
                            ExecuteTrans(SQL)


                        End If
                    End With
                End Using
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            CloseTrans()
        Catch ex As Exception
            CloseConn()
            CloseTrans()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

        EMI_PO_Pembelian_Sub.CekEkspedisiPO()
        Me.Close()

    End Sub


    Private Sub HitungTotal()
        If Lv_DataInput.Items.Count = 0 Then Exit Sub


        Dim TotTarif As Double = 0
        Dim TotBiayaLain As Double = 0
        Dim Grand As Double = 0

        For i As Integer = 0 To Lv_DataInput.Items.Count - 1
            Get_Data_Lv2(i)

            TotTarif += Val(HilangkanTanda(LvBiaya2_Tarif))
            TotBiayaLain += Val(HilangkanTanda(LvBiaya2_BiayaLain))
            Grand += Val(HilangkanTanda(LvBiaya2_Total))

        Next


        Txt_TotTarif.Text = Format(TotTarif, "N0")
        Txt_TotBiayaLain.Text = Format(TotBiayaLain, "N0")
        Txt_GrandTotal.Text = Format(Grand, "N0")

    End Sub

    Private Sub Lv_BiayaLokal_DoubleClick(sender As Object, e As EventArgs) Handles Lv_BiayaLokal.DoubleClick

        If Lv_BiayaLokal.Items.Count = 0 Or Lv_BiayaLokal.FocusedItem.Index = -1 Then Exit Sub

        Get_Data_Lv(Lv_BiayaLokal.FocusedItem.Index)


        '==================================================
        '=     CEK APAKAH DATA SUDAH PERNAH DI SELECT     =
        '==================================================
        For i As Integer = 0 To Lv_DataInput.Items.Count - 1
            If Lv_DataInput.Items(i).SubItems(item2_NoFaktur).Text = LvBiaya_NoFak Or Lv_DataInput.Items(i).SubItems(item2_Urut).Text = LvBiaya_Urut Then
                MessageBox.Show("Ekspedisi sudah ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        Dim Lv As ListViewItem
        Lv = Lv_DataInput.Items.Add(LvBiaya_NoFak)
        Lv.SubItems.Add(LvBiaya_NoPenawarn)
        Lv.SubItems.Add(LvBiaya_TglMulai)
        Lv.SubItems.Add(LvBiaya_TglSelesai)
        Lv.SubItems.Add(LvBiaya_JnsPengiriman)
        Lv.SubItems.Add(LvBiaya_PerusahaanBiayaImport)
        Lv.SubItems.Add(LvBiaya_KdBiaya)
        Lv.SubItems.Add(LvBiaya_MataUang)
        Lv.SubItems.Add(LvBiaya_JnsKendaraan)
        Lv.SubItems.Add(LvBiaya_KapastiasKendaraan)
        Lv.SubItems.Add(LvBiaya_Tarif)
        Lv.SubItems.Add(LvBiaya_BiayaLain)
        Lv.SubItems.Add(LvBiaya_Total)
        'HIDE
        Lv.SubItems.Add(LvBiaya_IdJnsPengiriman)
        Lv.SubItems.Add(LvBiaya_IdKdPerusaahanBiayaImport)
        Lv.SubItems.Add(LvBiaya_IdJnsKendaraan)

        Lv.SubItems.Add(LvBiaya_LokasiAwal)
        Lv.SubItems.Add(LvBiaya_LokasiTujuan)
        Lv.SubItems.Add(LvBiaya_Urut)

        HitungTotal()

    End Sub


    Private Sub Lv_BiayaLokal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lv_BiayaLokal.KeyPress
        If e.KeyChar = Chr(13) Then
            Lv_BiayaLokal_DoubleClick(Lv_BiayaLokal, e)
        End If
    End Sub

End Class
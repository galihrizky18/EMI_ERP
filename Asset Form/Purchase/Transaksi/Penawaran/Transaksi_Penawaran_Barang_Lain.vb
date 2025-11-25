Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Transaksi_Penawaran_Barang_Lain

    Dim arrcari, arrPembayaran As New ArrayList
    Public MinHari As Integer = 300

    Dim publicFlagRelease = "T"
    Dim noPenawaran As String = ""
    Dim kodeSupplier As String = ""

    Public arrIdProvAsal, arrIdKabKotaAsal, arrIdKecAsal, arrIdKelAsal, arrIdLokasiAwal As New ArrayList
    Public arrIdProvTujuan, arrIdKabKotaTujuan, arrIdKecTujuan, arrIdKelTujuan, arrIdLokasiTujuan As New ArrayList
    Public arrCaraKirim, arrMediaKirim, arrUkuran, arrSatuan As New ArrayList

    Public arrKdBarangPilihBarang, arrNamaBarangPilihBarang, arrSatuanPilihBarang, arrPPN As New ArrayList

    Dim Jenis = "Master_Penawaran_Lain"
    Dim Jenis2 = "Master_Ongkir_Lain"

    Dim lvKdBrg, lvNmBrg, lvMinOrder, lvSatuan, lvHrgSatuan, lvsisahari, lvMUA As String

    'Public cellCheckbox As Integer = 0
    Public cellKdBrg As Integer = 0

    Public cellNmBrg As Integer = 1
    Public cellPPN As Integer = 2
    Public cellMinOrder As Integer = 3
    Public cellSatuan As Integer = 4
    Public cellMUA As Integer = 5
    Public cellHrgSatuan As Integer = 6
    Public cellSisaHari As Integer = 7

    Private Sub get_no_faktur()
        TxtPenawaran_NoFaktur.Text = fMasterPenawaranLain & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Master_Penawaran_Barang_Lain", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fMasterPenawaranLain) + 4 & ")", fMasterPenawaranLain & Format(tgl_skg, "MMyy"))
    End Sub

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        lvKdBrg = CekNothing(DgvMaster_Penawaran.Rows(No_Index).Cells(cellKdBrg).Value)
        lvNmBrg = CekNothing(DgvMaster_Penawaran.Rows(No_Index).Cells(cellNmBrg).Value)
        lvMinOrder = CekNothing(DgvMaster_Penawaran.Rows(No_Index).Cells(cellMinOrder).Value)
        lvSatuan = CekNothing(DgvMaster_Penawaran.Rows(No_Index).Cells(cellSatuan).Value)
        lvHrgSatuan = CekNothing(DgvMaster_Penawaran.Rows(No_Index).Cells(cellHrgSatuan).Value)
        'lvCheckbox = CekNothing(DgvMaster_Penawaran.Rows(No_Index).Cells(cellCheckbox).Value)
        lvMUA = CekNothing(DgvMaster_Penawaran.Rows(No_Index).Cells(cellMUA).Value)
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

    Private Sub Get_Binding_Lokasi_Gudang()
        Try
            OpenConn()

            SQL = "SELECT * From Binding_Lokasi_Gudang_Lain "
            SQL = SQL & "Where Kode_stock_owner = '" & Lokasi & "' and "
            SQL = SQL & "Gudang_Default = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Lbl_BindingLokasiGudang.Text = Dr("Kode_Stock_Owner_Gudang")
                End If
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Transaksi_Penawaran_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Penawaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis2)

            Label1.Text = Base_Language.Lang_Penawaran_Judul
            lbl_NoFaktur.Text = Base_Language.Lang_Global_NoFaktur
            Lbl_Supplier.Text = Base_Language.Lang_Global_Supplier
            Lbl_NoPenawaran.Text = Base_Language.Lang_Penawaran_NoPenawaran
            Lbl_TglPenawaranHrg.Text = Base_Language.Lang_global_Periode_Awal
            Lbl_PeriodeAkhirPenawaran.Text = Base_Language.Lang_global_Periode_Akhir
            LblOngkir_PeriodeAkhirPenawaran.Text = Base_Language.Lang_global_Periode_Akhir

            DgvMaster_Penawaran.Columns(cellKdBrg).HeaderText = Base_Language.Lang_Global_KodeBarang
            DgvMaster_Penawaran.Columns(cellNmBrg).HeaderText = Base_Language.Lang_Global_NamaBarang
            DgvMaster_Penawaran.Columns(cellMinOrder).HeaderText = Base_Language.Lang_Global_MinOrder
            DgvMaster_Penawaran.Columns(cellSatuan).HeaderText = Base_Language.Lang_Global_Satuan
            DgvMaster_Penawaran.Columns(cellHrgSatuan).HeaderText = Base_Language.Lang_Global_HargaSatuan
            DgvMaster_Penawaran.Columns(cellSisaHari).HeaderText = "Sisa Hari"
            DgvMaster_Penawaran.Columns(cellMUA).HeaderText = Base_Language.Lang_Global_MataUang

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh

            LvAutoCompleteSupplier.Location = New Point(673, 90)

            LvAutoCompleteSupplier.Hide()

            LvAutoCompleteSupplier.Columns.Clear()
            LvAutoCompleteSupplier.Columns.Add(Base_Language.Lang_Global_Supplier, 130, HorizontalAlignment.Left)
            LvAutoCompleteSupplier.Columns.Add(Base_Language.Lang_Global_Nama, 175, HorizontalAlignment.Left)
            'LvAutoCompleteSupplier.View = View.Details

            'SECTION - ONGKIR
            Lv_AutoCompleteNmEkspedisi.Location = New Point(213, 70)
            Lv_AutoCompleteNmEkspedisi.Hide()

            Lv_AutoCompleteNmEkspedisi.Columns.Clear()
            Lv_AutoCompleteNmEkspedisi.Columns.Add("Id", 0, HorizontalAlignment.Left) '0
            Lv_AutoCompleteNmEkspedisi.Columns.Add("Kode", 80, HorizontalAlignment.Left) '1
            Lv_AutoCompleteNmEkspedisi.Columns.Add(Base_Language.Lang_Ongkir_NmEkspedisi, 170, HorizontalAlignment.Left) '2
            'Lv_AutoCompleteNmEkspedisi.View = View.Details

            'Lbl_OngkirJudul.Text = Base_Language.Lang_Ongkir_Lain_Judul
            LblOngkir_NmEkspedisi.Text = Base_Language.Lang_Ongkir_NmEkspedisi
            LblOngkir_NoPenawaran.Text = Base_Language.Lang_Penawaran_NoPenawaran
            LblOngkir_TglPenawaranHrg.Text = Base_Language.Lang_global_Periode_Awal
            LblOngkir_ProvAsal.Text = Base_Language.Lang_Ongkir_ProvAsal
            LblOngkir_ProvTujuan.Text = Base_Language.Lang_Ongkir_ProvTujuan
            LblOngkir_KabKotaAsal.Text = Base_Language.Lang_Ongkir_KabKotaAsal
            LblOngkir_KabKotTujuan.Text = Base_Language.Lang_Ongkir_KabKotaTujuan
            LblOngkir_LokasiAwal.Text = Base_Language.Lang_Global_Lokasi_Awal
            LblOngkir_LokasiTujuan.Text = Base_Language.Lang_Global_Lokasi_Tujuan
            LblOngkir_CaraKirim.Text = Base_Language.Lang_Global_Cara_Kirim
            LblOngkir_MediaKirim.Text = Base_Language.Lang_Ongkir_MediaKirim
            LblOngkir_Satuan.Text = Base_Language.Lang_Global_Satuan
            'LblOngkir_Ukuran.Text = Base_Language.Lang_Ongkir_Lain_Ukuran 
            LblOngkir_Hrg.Text = Base_Language.Lang_Ongkir_Hrg
            BtnOngkir_Simpan.Text = Base_Language.Lang_Global_Simpan
            BtnOngkir_Refresh.Text = Base_Language.Lang_Global_Refresh

            Lbl_Panjang.Text = Base_Language.Lang_Global_P
            Lbl_Lebar.Text = Base_Language.Lang_Global_L
            Lbl_Tinggi.Text = Base_Language.Lang_Global_T
            'Lbl_SatuanPanjang.Text = "Length Unit"
            'Lbl_SatuanVolume.Text = Base_Language.Lang_Global_SatuanVolume
            Lbl_Berat.Text = Base_Language.Lang_Global_Berat
            'Lbl_SatuanBerat.Text = Base_Language.Lang_Global_SatuanBerat
            lblOngkir_uk.Text = Base_Language.Lang_Global_Ukuran

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub Txt_Panjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Panjang.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Panjang.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Txt_Lebar.Focus()
    End Sub

    Private Sub Txt_Panjang_Leave(sender As Object, e As EventArgs) Handles Txt_Panjang.Leave
        If Txt_Panjang.Text.Length = 0 Then
            Exit Sub
        End If
        Hitung_Volume()
    End Sub

    Private Sub Txt_Lebar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Lebar.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Lebar.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Txt_Tinggi.Focus()
    End Sub

    Private Sub Txt_Lebar_Leave(sender As Object, e As EventArgs) Handles Txt_Lebar.Leave
        If Txt_Lebar.Text.Length = 0 Then
            Exit Sub
        End If
        Hitung_Volume()
    End Sub

    Private Sub Txt_Tinggi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Tinggi.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Tinggi.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Cmb_SatuanPanjang.Focus()
    End Sub

    Private Sub Txt_Tinggi_Leave(sender As Object, e As EventArgs) Handles Txt_Tinggi.Leave
        If Txt_Tinggi.Text.Length = 0 Then
            Exit Sub
        End If
        Hitung_Volume()
    End Sub

    Private Sub Hitung_Volume()
        Dim getVolume As Double = 0
        getVolume = Val(Txt_Panjang.Text) * Val(Txt_Lebar.Text) * Val(Txt_Tinggi.Text)
        Txt_Volume.Text = getVolume
    End Sub

    Private Sub Txt_Berat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Berat.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Berat.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Cmb_SatuanBerat.Focus()
    End Sub

    Private Sub Cmb_SatuanPanjang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_SatuanPanjang.SelectedIndexChanged
        If Cmb_SatuanPanjang.SelectedIndex = -1 Then Exit Sub
        Get_Satuan_Volume()
    End Sub

    Private Sub Get_Satuan_Panjang()
        Try
            OpenConn()
            Cmb_SatuanPanjang.Items.Clear()
            SQL = "Select Satuan "
            SQL = SQL & "From EMI_Satuan "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Flag_Tampil_Panjang = 'Y' "
            SQL = SQL & "Order By Satuan Desc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_SatuanPanjang.Items.Add(dr("Satuan"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Satuan_Volume()

        Try
            OpenConn()

            Cmb_SatuanVolume.Items.Clear()
            SQL = "select a.Satuan, b.Satuan_Volume  "
            SQL = SQL & "from EMI_Satuan a, EMI_Satuan_Turunan b "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Satuan = b.Satuan "
            SQL = SQL & "and a.Satuan = '" & Cmb_SatuanPanjang.SelectedItem & "' "
            SQL = SQL & "Order By a.Satuan Desc"

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_SatuanVolume.Items.Add(dr("Satuan_Volume"))
                Loop
            End Using

            Cmb_SatuanVolume.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Satuan_Berat()
        Try
            OpenConn()
            Cmb_SatuanBerat.Items.Clear()
            SQL = "Select Satuan "
            SQL = SQL & "From EMI_Satuan "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Flag_Tampil_Berat = 'Y' "
            SQL = SQL & "Order By Satuan Desc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_SatuanBerat.Items.Add(dr("Satuan"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub LoadDataPenawaran()

        'DgvMaster_Penawaran.Rows.Add(1)
        Dim index As Integer = DgvMaster_Penawaran.Rows.Count - 1

        For i As Integer = 0 To arrKdBarangPilihBarang.Count - 1

            DgvMaster_Penawaran.Rows.Add(1)

            DgvMaster_Penawaran.Rows(index).Cells(cellKdBrg).Value = arrKdBarangPilihBarang(i)
            DgvMaster_Penawaran.Rows(index).Cells(cellNmBrg).Value = arrNamaBarangPilihBarang(i)
            DgvMaster_Penawaran.Rows(index).Cells(cellSatuan).Value = arrSatuanPilihBarang(i)
            DgvMaster_Penawaran.Rows(index).Cells(cellPPN).Value = arrPPN(i)

            DgvMaster_Penawaran.Rows(index).Cells(cellKdBrg).ReadOnly = True
            DgvMaster_Penawaran.Rows(index).Cells(cellNmBrg).ReadOnly = True
            DgvMaster_Penawaran.Rows(index).Cells(cellSatuan).ReadOnly = True

            DgvMaster_Penawaran.Rows(index).Cells(cellMinOrder).Value = 0
            DgvMaster_Penawaran.Rows(index).Cells(cellHrgSatuan).Value = 0

            DgvMaster_Penawaran.Rows(index).DefaultCellStyle.BackColor = Color.LightYellow

            index = index + 1

        Next

        'If index = 0 Then
        '    DgvMaster_Penawaran.Rows.RemoveAt(0)
        'Else
        '    DgvMaster_Penawaran.Rows.RemoveAt(index)
        'End If

    End Sub

    Private Sub Get_Provinsi_Awal()

        Try
            OpenConn()
            Cmb_ProvAsal.Items.Clear() : arrIdProvAsal.Clear()
            SQL = "Select * From "
            SQL = SQL & "tbl_provinsi "
            SQL = SQL & "order by nama_provinsi"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_ProvAsal.Items.Add(dr("nama_provinsi")) : arrIdProvAsal.Add(dr("Id_Provinsi"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Provinsi_Tujuan()

        Try
            OpenConn()

            Cmb_ProvTujuan.Items.Clear() : arrIdProvTujuan.Clear()
            SQL = "Select * From "
            SQL = SQL & "tbl_provinsi "
            SQL = SQL & "order by nama_provinsi"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_ProvTujuan.Items.Add(dr("nama_provinsi")) : arrIdProvTujuan.Add(dr("Id_Provinsi"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Public Sub Get_KabupatenKota_Asal()
        Try
            OpenConn()

            Cmb_KabKotaAwal.Items.Clear() : arrIdKabKotaAsal.Clear()
            SQL = "select b.id_kabupaten_kota, b.provinsi_id, b.nama_kabupaten_kota "
            SQL = SQL & "from tbl_provinsi a, tbl_kabupaten_kota b "
            SQL = SQL & "where a.id_provinsi = b.provinsi_id and a.nama_provinsi = '" & Cmb_ProvAsal.SelectedItem & "' "
            SQL = SQL & "order by b.nama_kabupaten_kota"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_KabKotaAwal.Items.Add(dr("nama_kabupaten_kota")) : arrIdKabKotaAsal.Add(dr("id_kabupaten_kota"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Public Sub Get_KabupatenKota_Tujuan()
        Try
            OpenConn()

            Cmb_KabKotaTujuan.Items.Clear() : arrIdKabKotaTujuan.Clear()
            SQL = "select b.id_kabupaten_kota, b.provinsi_id, b.nama_kabupaten_kota "
            SQL = SQL & "from tbl_provinsi a, tbl_kabupaten_kota b "
            SQL = SQL & "where a.id_provinsi = b.provinsi_id and a.nama_provinsi = '" & Cmb_ProvTujuan.SelectedItem & "' "
            SQL = SQL & "order by b.nama_kabupaten_kota"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_KabKotaTujuan.Items.Add(dr("nama_kabupaten_kota")) : arrIdKabKotaTujuan.Add(dr("id_kabupaten_kota"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Public Sub Get_Kecamatan_Asal()
        Try
            OpenConn()

            Cmb_KecAsal.Items.Clear() : arrIdKecAsal.Clear()
            SQL = "Select b.Id_Kecamatan, b.Kabupaten_Kota_Id, b.nama_kecamatan "
            SQL = SQL & "From tbl_kabupaten_kota a, tbl_kecamatan b "
            SQL = SQL & "Where a.Id_Kabupaten_Kota = b.Kabupaten_Kota_Id and a.nama_kabupaten_kota = '" & Cmb_KabKotaAwal.SelectedItem & "' "
            SQL = SQL & "Order By b.nama_kecamatan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_KecAsal.Items.Add(dr("nama_kecamatan")) : arrIdKecAsal.Add(dr("id_kecamatan"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Get_Kecamatan_Tujuan()
        Try
            OpenConn()

            Cmb_KecTujuan.Items.Clear() : arrIdKecTujuan.Clear()
            SQL = "Select b.Id_Kecamatan, b.Kabupaten_Kota_Id, b.nama_kecamatan "
            SQL = SQL & "From tbl_kabupaten_kota a, tbl_kecamatan b "
            SQL = SQL & "Where a.Id_Kabupaten_Kota = b.Kabupaten_Kota_Id and a.nama_kabupaten_kota = '" & Cmb_KabKotaTujuan.SelectedItem & "' "
            SQL = SQL & "Order By b.nama_kecamatan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_KecTujuan.Items.Add(dr("nama_kecamatan")) : arrIdKecTujuan.Add(dr("id_kecamatan"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Get_Kelurahan_Asal()
        Try
            OpenConn()

            Cmb_KelAsal.Items.Clear() : arrIdKelAsal.Clear()
            SQL = "Select b.Id_Kelurahan, b.Kecamatan_Id, b.nama_kelurahan "
            SQL = SQL & "From tbl_kecamatan a, tbl_kelurahan b "
            SQL = SQL & "Where a.Id_Kecamatan = b.Kecamatan_Id and a.nama_kecamatan = '" & Cmb_KecAsal.SelectedItem & "' "
            SQL = SQL & "Order By b.nama_kelurahan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_KelAsal.Items.Add(dr("nama_kelurahan")) : arrIdKelAsal.Add(dr("id_kelurahan"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Get_Kelurahan_Tujuan()
        Try
            OpenConn()

            Cmb_KelTujuan.Items.Clear() : arrIdKelTujuan.Clear()
            SQL = "Select b.Id_Kelurahan, b.Kecamatan_Id, b.nama_kelurahan "
            SQL = SQL & "From tbl_kecamatan a, tbl_kelurahan b "
            SQL = SQL & "Where a.Id_Kecamatan = b.Kecamatan_Id and a.nama_kecamatan = '" & Cmb_KecTujuan.SelectedItem & "' "
            SQL = SQL & "Order By b.nama_kelurahan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_KelTujuan.Items.Add(dr("nama_kelurahan")) : arrIdKelTujuan.Add(dr("id_kelurahan"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Get_Cara_Kirim()
        Try
            OpenConn()
            Cmb_CaraKirim.Items.Clear() : arrCaraKirim.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Cara_Kirim "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_CaraKirim.Items.Add(dr("Keterangan")) : arrCaraKirim.Add(dr("Id_Cara_Kirim"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Get_Media_Kirim()
        Try
            OpenConn()
            Cmb_MediaKirim.Items.Clear() : arrMediaKirim.Clear()
            SQL = "Select b.Id_Media_Kirim, b.Keterangan From "
            SQL = SQL & "EMI_Cara_Kirim a, EMI_Media_Kirim b "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cara_Kirim = b.Id_Cara_Kirim "
            SQL = SQL & "and a.Keterangan = '" & Cmb_CaraKirim.SelectedItem & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_MediaKirim.Items.Add(dr("Keterangan")) : arrMediaKirim.Add(dr("Id_Media_Kirim"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Satuan_Pengiriman()
        Try
            OpenConn()
            CmbOngkir_Satuan.Items.Clear() : arrSatuan.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Satuan_Pengiriman "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbOngkir_Satuan.Items.Add(dr("Satuan")) : arrSatuan.Add(dr("Id_Satuan_Pengiriman"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Private Sub Get_Ukuran_Kontainer()
    '    Try
    '        OpenConn()

    '        Cmb_UkuranKontainer.Items.Clear() : arrUkuran.Clear()
    '        SQL = "Select * From "
    '        SQL = SQL & "kontainer where "
    '        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
    '        SQL = SQL & "order by kode_kontainer"
    '        Using dr = OpenTrans(SQL)
    '            Do While dr.Read
    '                Cmb_UkuranKontainer.Items.Add(dr("kode_kontainer")) : arrUkuran.Add(dr("Id_Kontainer"))
    '            Loop
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    Public Sub kosong()
        get_jam()
        DgvMaster_Penawaran.Rows.Clear()

        'Get_Satuan()
        Get_Binding_Lokasi_Gudang()

        TxtPO_KdSupplier.Text = ""
        TxtPO_NmSupplier.Text = ""
        Txt_NoPenawaran.Text = ""
        Txt_NoUrut.Text = ""
        cmbJenisPengiriman.SelectedIndex = -1
        txtJatuhTempo.Text = ""

        cmbJenisPengiriman.Enabled = False
        txtJatuhTempo.Enabled = False
        publicFlagRelease = "T"

        cmb_JenisBayar.Items.Clear() : arrPembayaran.Clear()
        cmb_JenisBayar.Items.Add("Tunai") : arrPembayaran.Add("T")
        cmb_JenisBayar.Items.Add("Non-Tunai") : arrPembayaran.Add("N")
        cmb_JenisBayar.SelectedIndex = -1

        Dtp_Tgl.Value = tgl_skg
        Dtp_PeriodAkhir.Value = tgl_skg

        Txt_NoPenawaran.ReadOnly = False
        TxtPO_KdSupplier.ReadOnly = False
        TxtPO_NmSupplier.ReadOnly = False
        TxtPO_KdSupplier.ReadOnly = False
        TxtPO_NmSupplier.ReadOnly = False

        DgvMaster_Penawaran.ReadOnly = False
        Btn_PilihBarang.Enabled = True

        Txt_NoPenawaran.ReadOnly = False
        Dtp_Tgl.Enabled = True
        Dtp_PeriodAkhir.Enabled = True

        Btn_Release.Visible = False
        Btn_Simpan.Tag = "&Simpan"
        Btn_Simpan.Text = "&Simpan"
        Btn_Simpan.Enabled = True

        DtpOngkir_TglPenawaranHrg.Value = DateTime.Now
        DtpOngkir_PeriodeAkhirPenawaran.Value = DateTime.Now

        Try
            OpenConn()
            Column3.Items.Clear()
            SQL = "select Kode_mata_uang from Mata_Uang where Kode_Perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Column3.Items.Add(dr("Kode_mata_uang"))
                Loop
            End Using

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        arrKdBarangPilihBarang.Clear()
        arrNamaBarangPilihBarang.Clear()
        arrSatuanPilihBarang.Clear()

        '---------- SECTION ONGKIR
        Txt_NmEkspedisi.Text = ""
        TxtOngkir_NoPenawaran.Text = ""
        Cmb_ProvAsal.SelectedIndex = -1
        Cmb_ProvTujuan.SelectedIndex = -1
        Cmb_KabKotaAwal.SelectedIndex = -1
        Cmb_KabKotaTujuan.SelectedIndex = -1
        Txt_LokasiAwal.Text = "-"
        TxtOngkir_LokasiTujuan.Text = "-"
        Cmb_CaraKirim.SelectedIndex = -1
        Cmb_MediaKirim.SelectedIndex = -1
        CmbOngkir_Satuan.SelectedIndex = -1
        'Cmb_UkuranKontainer.SelectedIndex = -1
        TxtOngkir_Hrg.Text = ""

        Cmb_KecAsal.SelectedIndex = -1
        Cmb_KelAsal.SelectedIndex = -1
        Cmb_KecTujuan.SelectedIndex = -1
        Cmb_KelTujuan.SelectedIndex = -1

        Txt_Panjang.Text = ""
        Txt_Lebar.Text = ""
        Txt_Tinggi.Text = ""
        Txt_Volume.Text = ""
        Cmb_SatuanPanjang.SelectedIndex = -1
        Cmb_SatuanVolume.SelectedIndex = -1
        Txt_Berat.Text = ""
        Cmb_SatuanBerat.SelectedIndex = -1

        DgvMaster_Penawaran.Rows.Clear()
        DgvMaster_Penawaran.Rows.Add(1)
        LoadDataPenawaran()

        'tampil_bahan_baku()
        'tampil_packaging()
        Get_Provinsi_Awal()
        Get_Provinsi_Tujuan()
        Get_Cara_Kirim()
        Get_Satuan_Pengiriman()
        'Get_Ukuran_Kontainer()

        Get_Satuan_Panjang()
        Get_Satuan_Berat()

        Txt_Berat.Enabled = True
        Cmb_SatuanBerat.Enabled = True

        'DgvMaster_Penawaran.Cell(cellCheckbox).Value = "True"
        'DgvMaster_Penawaran.Rows(0).Cells(cellCheckbox).Value = True
    End Sub

    Private Sub Txt_Supplier_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Down Then LvAutoCompleteSupplier.Focus()
    End Sub

    Private Sub Btn_Simpan_Click_1(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If DgvMaster_Penawaran.RowCount = 0 Then
            MessageBox.Show("Tidak ada Data yang Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim saveFaktur = TxtPenawaran_NoFaktur.Text
        Dim saveSupplier = Lbl_KdSupplier.Text
        Dim saveNoPenawaran = Txt_NoPenawaran.Text

        If TxtPenawaran_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_NoFaktur & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtPenawaran_NoFaktur.Focus() : Exit Sub
        ElseIf TxtPO_KdSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Supplier & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtPO_KdSupplier.Focus() : Exit Sub
        ElseIf Txt_NoPenawaran.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Penawaran_NoPenawaran & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_NoPenawaran.Focus() : Exit Sub
        End If

        If Format(Dtp_Tgl.Value, "yyyy-MM-dd") > Format(Dtp_PeriodAkhir.Value, "yyyy-MM-dd") Then
            'MessageBox.Show("Tgl Penawaran Harga tidak boleh lebih dari Periode Akhir Penawaran!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show(Base_Language.Lang_Penawaran_TglPenawaranHrg + " " + Base_Language.Lang_Global_TidakBolehLebihDari + " " + Base_Language.Lang_Penawaran_PeriodeAkhir, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_Tgl.Focus()
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim hasDataToInsert As Boolean = False

            If Btn_Simpan.Tag = "&Simpan" Then

                get_no_faktur()

                'Save Master Penawaran
                SQL = "Insert into EMI_Master_Penawaran_Barang_Lain "
                SQL = SQL & "(Kode_Perusahaan, No_Faktur, No_Penawaran, Tgl_Penawaran_Hrg, Periode_Akhir_Penawaran, Kode_Supplier, lokasi, tanggal, jam, iduser) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', '" & saveFaktur & "', '" & saveNoPenawaran & "', "
                SQL = SQL & "'" & Format(Dtp_Tgl.Value, "yyyy-MM-dd") & "', '" & Format(Dtp_PeriodAkhir.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & saveSupplier & "', '" & Lokasi & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "' "
                SQL = SQL & ")"
                ExecuteTrans(SQL)


                SQL = "insert into EMI_Master_Penawaran_Jatuh_Tempo_Barang_Lain(Kode_Perusahaan,No_Faktur,No_Penawaran,Jenis_Pembayaran,Tempo_Pembayaran,Lama_Pembayaran) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & saveFaktur & "', '" & saveNoPenawaran & "', '" & arrPembayaran.Item(cmb_JenisBayar.SelectedIndex) & "',"
                SQL = SQL & "'" & cmbJenisPengiriman.Text & "', '" & txtJatuhTempo.Text & "') "
                ExecuteTrans(SQL)

                'Save Master Penawaran Detail
                For index = 0 To DgvMaster_Penawaran.Rows.Count - 1
                    Get_Isi_Listview(index)

                    If DgvMaster_Penawaran.Rows(index).Cells(cellMinOrder).Value = "" Or DgvMaster_Penawaran.Rows(index).Cells(cellMinOrder).Value = 0 Then
                        Continue For
                    End If

                    hasDataToInsert = True

                    'If DgvMaster_Penawaran.Rows(index).Cells(cellCheckbox).Value = True Then
                    If DgvMaster_Penawaran.Rows(index).Cells(cellMinOrder).Value <> "" Or DgvMaster_Penawaran.Rows(index).Cells(cellMinOrder).Value <> 0 Then

                        If lvMUA = "" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_MataUang + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf lvKdBrg = "" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_KodeBarang + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf lvNmBrg = "" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_NamaBarang + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf lvMinOrder = "" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_MinOrder + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf lvSatuan = "" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_Satuan + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf lvHrgSatuan = "" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_HargaSatuan + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        Dim Satuan_Barang As String = ""
                        SQL = "select top(1) satuan from Barang_Lain where kode_barang ='" & lvKdBrg & "'"
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                Satuan_Barang = dr("satuan")
                            End If
                        End Using

                        Dim harga_satuan_kecil As Double = 0
                        SQL = "select dbo.ubah_satuan_lain('" & KodePerusahaan & "','UANG','" & lvKdBrg & "',"
                        SQL = SQL & "'" & lvSatuan & "','" & Satuan_Barang & "', "
                        SQL = SQL & "'" & HilangkanTanda(lvHrgSatuan) & "') as Hasil "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                harga_satuan_kecil = dr("hasil")
                            End If
                        End Using

                        SQL = "Insert into EMI_Master_Penawaran_Detail_Barang_Lain "
                        SQL = SQL & "(Kode_Perusahaan, No_Faktur, Kode_Barang, "
                        SQL = SQL & "Min_Order, Satuan, Harga_Satuan, Nilai_Barang, Satuan_Barang, Mata_Uang) "
                        SQL = SQL & "Values ('" & KodePerusahaan & "', '" & saveFaktur & "', '" & lvKdBrg & "', "
                        SQL = SQL & "'" & HilangkanTanda(lvMinOrder) & "', '" & lvSatuan & "', '" & HilangkanTanda(lvHrgSatuan) & "', "
                        SQL = SQL & " '" & harga_satuan_kecil & "','" & Satuan_Barang & "', '" & lvMUA & "') "
                        ExecuteTrans(SQL)

                    End If
                Next

            ElseIf Btn_Simpan.Tag = "&Update" Then

                '================================
                '=     CEK APAKAH ADA DATA?     =
                '================================
                SQL = "select No_Faktur from EMI_Master_Penawaran_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & saveFaktur & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            '=================================
                            '=    UPDATE DATA INDUK LAMA     =
                            '=================================
                            SQL = "update EMI_Master_Penawaran_Barang_Lain set Tgl_Penawaran_Hrg = '" & Format(Dtp_Tgl.Value, "yyyy-MM-dd") & "', Periode_Akhir_Penawaran = '" & Format(Dtp_PeriodAkhir.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "No_Penawaran = '" & Txt_NoPenawaran.Text & "', Kode_Supplier = '" & TxtPO_KdSupplier.Text & "' "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtPenawaran_NoFaktur.Text & "' and NoUrut = '" & Txt_NoUrut.Text & "' "
                            ExecuteTrans(SQL)

                            For i As Integer = 0 To .Rows.Count - 1

                                '==================================
                                '=     DELETE DATA SEBELUMNYA     =
                                '==================================

                                SQL = "delete from EMI_Master_Penawaran_Detail_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & saveFaktur & "' "
                                ExecuteTrans(SQL)

                                For index = 0 To DgvMaster_Penawaran.Rows.Count - 1
                                    Get_Isi_Listview(index)

                                    If DgvMaster_Penawaran.Rows(index).Cells(cellMinOrder).Value = 0 Or DgvMaster_Penawaran.Rows(index).Cells(cellHrgSatuan).Value = 0 Then
                                        Continue For
                                    End If

                                    hasDataToInsert = True

                                    If lvMUA = "" Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Base_Language.Lang_Global_MataUang + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf lvKdBrg = "" Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Base_Language.Lang_Global_KodeBarang + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf lvNmBrg = "" Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Base_Language.Lang_Global_NamaBarang + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf lvMinOrder = "" Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Base_Language.Lang_Global_MinOrder + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf lvSatuan = "" Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Base_Language.Lang_Global_Satuan + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf lvHrgSatuan = "" Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Base_Language.Lang_Global_HargaSatuan + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    Dim Satuan_Barang As String = ""
                                    SQL = "select top(1) satuan from Barang_Lain where kode_barang ='" & lvKdBrg & "'"
                                    Using dr = OpenTrans(SQL)
                                        If dr.Read Then
                                            Satuan_Barang = dr("satuan")
                                        End If
                                    End Using

                                    Dim harga_satuan_kecil As Double = 0
                                    SQL = "select dbo.ubah_satuan_lain('" & KodePerusahaan & "','UANG','" & lvKdBrg & "',"
                                    SQL = SQL & "'" & lvSatuan & "','" & Satuan_Barang & "', "
                                    SQL = SQL & "'" & HilangkanTanda(lvHrgSatuan) & "') as Hasil "
                                    Using dr = OpenTrans(SQL)
                                        If dr.Read Then
                                            harga_satuan_kecil = dr("hasil")
                                        End If
                                    End Using



                                    '============================
                                    '=     INSERT DATA BARU     =
                                    '============================
                                    SQL = "Insert into EMI_Master_Penawaran_Detail_Barang_Lain "
                                    SQL = SQL & "(Kode_Perusahaan, No_Faktur, Kode_Barang, "
                                    SQL = SQL & "Min_Order, Satuan, Harga_Satuan, Nilai_Barang, Satuan_Barang, Mata_Uang) "
                                    SQL = SQL & "Values ('" & KodePerusahaan & "', '" & saveFaktur & "', '" & lvKdBrg & "', "
                                    SQL = SQL & "'" & HilangkanTanda(lvMinOrder) & "', '" & lvSatuan & "', '" & HilangkanTanda(lvHrgSatuan) & "', "
                                    SQL = SQL & " '" & harga_satuan_kecil & "', '" & Satuan_Barang & "', '" & lvMUA & "') "
                                    ExecuteTrans(SQL)

                                Next

                            Next

                        End If
                    End With
                End Using

            End If

            If hasDataToInsert = False Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Tidak Ada Data yang Di Insert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub Btn_Release_Click(sender As Object, e As EventArgs) Handles Btn_Release.Click

        If DgvMaster_Penawaran.Rows.Count = 0 Then
            MessageBox.Show("Tidak ada Data yang Akan di Release", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim saveFaktur = TxtPenawaran_NoFaktur.Text
            Dim saveSupplier = TxtPO_KdSupplier.Text
            Dim saveNoPenawaran = Txt_NoPenawaran.Text

            Dim flag_kategori_Supplier As String = ""
            Dim kode_supplier As String = saveSupplier

            SQL = "select b.kode_supplier, b.ID_Kategori_Suppliers, c.flag_jenis_import  from  Suppliers b, Suppliers_Kategori c "
            SQL = SQL & "where "
            SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan and b.id_kategori_suppliers = c.id_kategori_suppliers "
            SQL = SQL & "and b.kode_perusahaan = '" & KodePerusahaan & "'  and b.Kode_Supplier = '" & saveSupplier & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    flag_kategori_Supplier = General_Class.CekNULL(Dr("flag_jenis_import"))
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_GLOBAL_Kategori_Supplier & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            For index = 0 To DgvMaster_Penawaran.Rows.Count - 1

                Get_Isi_Listview(index)

                If DgvMaster_Penawaran.Rows(index).Cells(cellMinOrder).Value = 0 Or DgvMaster_Penawaran.Rows(index).Cells(cellHrgSatuan).Value = 0 Then
                    Continue For
                End If

                If lvMUA = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_MataUang + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf lvKdBrg = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_KodeBarang + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf lvNmBrg = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_NamaBarang + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf lvMinOrder = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_MinOrder + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf lvSatuan = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_Satuan + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf lvHrgSatuan = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_HargaSatuan + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If flag_kategori_Supplier = "Y" Then


                    Dim satuan_kirim As String = ""
                    Dim nilai_kirim As Double = 0
                    SQL = "select satuan from barang_Detail_Satuan_lain where kode_barang='" & lvKdBrg & "' "
                    SQL = SQL & "and kode_Perusahaan='" & KodePerusahaan & "' and flag_kirim='Y' "
                    Using dr3 = OpenTrans(SQL)
                        If dr3.Read Then
                            satuan_kirim = dr3("satuan")
                        Else
                            dr3.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data satuan kirim tidak ada ")
                            Exit Sub
                        End If
                    End Using

                    Dim nama As String = ""
                    SQL = "select top(1) nama from Barang_Lain where kode_barang ='" & lvKdBrg & "' "
                    Using Dr1 = OpenTrans(SQL)
                        If Dr1.Read Then
                            nama = Dr1("nama")
                        End If
                    End Using


                    SQL = "select kode_Perusahaan from komposisi_barang_jadi_lain where kode_barang='" & lvKdBrg & "' "
                    SQL = SQL & "and kode_Perusahaan='" & KodePerusahaan & "' "
                    Using dr33 = OpenTrans(SQL)
                        If Not dr33.Read Then
                            dr33.Close()
                            SQL = "insert into komposisi_barang_jadi_lain(kode_perusahaan, "
                            SQL = SQL & "kode_barang, Qty) Values ("
                            SQL = SQL & " '" & KodePerusahaan & "', '" & lvKdBrg & "', "
                            SQL = SQL & "'1')"
                            ExecuteTrans(SQL)

                            SQL = "insert into detail_komposisi_barang_jadi_lain(kode_perusahaan, "
                            SQL = SQL & "kode_barang, Kode_Bahan, Qty_Bahan) Values("
                            SQL = SQL & "'" & KodePerusahaan & "', '" & lvKdBrg & "', "
                            SQL = SQL & "'" & lvKdBrg & "', '1')"
                            ExecuteTrans(SQL)
                        End If
                    End Using

                    SQL = "select kode_Perusahaan from bahan_import_lain where kode_bahan='" & lvKdBrg & "' "
                    SQL = SQL & "and kode_Perusahaan='" & KodePerusahaan & "' "
                    Using dr33 = OpenTrans(SQL)
                        If Not dr33.Read Then
                            dr33.Close()
                            SQL = "select kode_stock_owner_import from stock_owner_import_lain where kode_perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "order by kode_stock_owner_import"
                            Using Dsm = BindingTrans(SQL)

                                If Dsm.Tables("MyTable").Rows.Count <> 0 Then
                                    For iii As Integer = 0 To Dsm.Tables("MyTable").Rows.Count - 1
                                        SQL = "Insert Into bahan_import_lain(Kode_Perusahaan, Kode_STock_Owner_Import, kode_bahan,Kode_supplier, "
                                        SQL = SQL & "nama_bahan,kategori,mata_uang,harga,satuan, Flag_Potong_Stock) Values("
                                        SQL = SQL & "'" & KodePerusahaan & "', '" & Dsm.Tables("MyTable").Rows(iii).Item("kode_stock_owner_import") & "', "
                                        SQL = SQL & "'" & lvKdBrg & "','" & kode_supplier & "',"
                                        SQL = SQL & "'" & nama & "', '" & "Utama" & "', '" & lvMUA & "', "
                                        SQL = SQL & "'" & lvHrgSatuan & "','" & satuan_kirim & "', '" & "T" & "')"
                                        ExecuteTrans(SQL)
                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data lokasi import tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                            End Using
                        End If
                    End Using

                    SQL = "update EMI_Master_Penawaran_Detail_Barang_Lain set "
                    SQL = SQL & "flag_baru = 'Y' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_barang='" & lvKdBrg & "' "
                    SQL = SQL & "and no_faktur='" & TxtPenawaran_NoFaktur.Text & "' "
                    ExecuteTrans(SQL)
                End If

            Next

            'UPDATE FLAG RELEASE
            SQL = "update EMI_Master_Penawaran_Barang_Lain set "
            SQL = SQL & "flag_release = 'Y', "
            SQL = SQL & "Tanggal_Release = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "jam_release = '" & Format(tgl_skg, "HH:mm:ss") & "' , "
            SQL = SQL & "iduser_release = '" & UserID & "' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur='" & saveFaktur & "' "
            SQL = SQL & "and no_penawaran='" & saveNoPenawaran & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil di Release", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()

    End Sub

    Private Sub Cmb_ProvAsal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_ProvAsal.SelectedIndexChanged
        Get_KabupatenKota_Asal()
    End Sub

    Private Sub Cmb_ProvTujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_ProvTujuan.SelectedIndexChanged
        Get_KabupatenKota_Tujuan()
    End Sub

    Private Sub BtnOngkir_Simpan_Click(sender As Object, e As EventArgs) Handles BtnOngkir_Simpan.Click
        'Dim ongkirNmEkspedisi = Txt_NmEkspedisi.Text
        'Get_Satuan_Pengiriman()
        Dim ongkirIdEkspedisi = Lbl_IdEkspedisi.Text
        Dim ongkirNmEkspedisi = Lbl_NmEkspedisi.Text
        Dim ongkirNoPenawaran = TxtOngkir_NoPenawaran.Text
        Dim ongkirProvAsal = Cmb_ProvAsal.SelectedItem
        Dim ongkirProvTujuan = Cmb_ProvTujuan.SelectedItem
        Dim ongkirKabKotaAsal = Cmb_KabKotaAwal.SelectedItem
        Dim ongkirKabkotaTujuan = Cmb_KabKotaTujuan.SelectedItem
        Dim ongkirKecamatanAsal = Cmb_KecAsal.SelectedItem
        Dim ongkirKecamatanTujuan = Cmb_KecTujuan.SelectedItem
        Dim ongkirKelurahanAsal = Cmb_KelAsal.SelectedItem
        Dim ongkirKelurahanTujuan = Cmb_KelTujuan.SelectedItem
        Dim ongkirLokasiAwal = Txt_LokasiAwal.Text
        Dim ongkirLokasiTujuan = TxtOngkir_LokasiTujuan.Text
        Dim ongkirCaraKirim = Cmb_CaraKirim.SelectedItem
        Dim ongkirMediaKirim = Cmb_MediaKirim.SelectedItem
        Dim ongkirSatuan = CmbOngkir_Satuan.SelectedItem
        'Dim ongkirUkuranKontainer = Cmb_UkuranKontainer.SelectedItem
        Dim ongkirHarga = TxtOngkir_Hrg.Text

        Dim panjang = Txt_Panjang.Text
        Dim lebar = Txt_Lebar.Text
        Dim tinggi = Txt_Tinggi.Text
        Dim volume = Txt_Volume.Text
        Dim satuanPanjang = Cmb_SatuanPanjang.SelectedItem
        Dim satuanVolume = Cmb_SatuanVolume.SelectedItem
        Dim berat = Txt_Berat.Text
        Dim satuanBerat = Cmb_SatuanBerat.SelectedItem

        If Txt_NmEkspedisi.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Ongkir_NmEkspedisi & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NmEkspedisi.Focus() : Exit Sub
        ElseIf ongkirNoPenawaran.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Penawaran_NoPenawaran & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoPenawaran.Focus() : Exit Sub
        ElseIf Cmb_ProvAsal.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Ongkir_ProvAsal & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_ProvAsal.Focus() : Exit Sub
        ElseIf Cmb_KabKotaAwal.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Ongkir_KabKotaAsal & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_KabKotaAwal.Focus() : Exit Sub
        ElseIf Cmb_ProvTujuan.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Ongkir_ProvTujuan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_ProvTujuan.Focus() : Exit Sub
        ElseIf Cmb_KabKotaTujuan.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Ongkir_KabKotaTujuan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_KabKotaTujuan.Focus() : Exit Sub
        ElseIf ongkirLokasiAwal.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Lokasi_Awal & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_LokasiAwal.Focus() : Exit Sub
        ElseIf ongkirLokasiTujuan.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Lokasi_Tujuan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtOngkir_LokasiTujuan.Focus() : Exit Sub
        ElseIf Cmb_CaraKirim.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Cara_Kirim & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_CaraKirim.Focus() : Exit Sub
        ElseIf Cmb_MediaKirim.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Ongkir_MediaKirim & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_MediaKirim.Focus() : Exit Sub
        ElseIf CmbOngkir_Satuan.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbOngkir_Satuan.Focus() : Exit Sub
        ElseIf Cmb_SatuanPanjang.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Satuan_Panjang & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_SatuanPanjang.Focus() : Exit Sub
        ElseIf Cmb_SatuanBerat.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_SatuanBerat & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_SatuanBerat.Focus() : Exit Sub
            'ElseIf Cmb_UkuranKontainer.SelectedIndex = -1 Then
            '    MessageBox.Show(Base_Language.Lang_Ongkir_Lain_Ukuran  & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Cmb_UkuranKontainer.Focus() : Exit Sub
        ElseIf ongkirHarga.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Ongkir_Hrg & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtOngkir_Hrg.Focus() : Exit Sub
        End If

        If Format(DtpOngkir_TglPenawaranHrg.Value, "yyyy-MM-dd") > Format(DtpOngkir_PeriodeAkhirPenawaran.Value, "yyyy-MM-dd") Then
            ' MessageBox.Show("Tgl Penawaran Harga tidak boleh lebih dari Periode Akhir Penawaran!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show(Base_Language.Lang_Penawaran_TglPenawaranHrg + " " + Base_Language.Lang_Global_TidakBolehLebihDari + " " + Base_Language.Lang_Penawaran_PeriodeAkhir, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DtpOngkir_TglPenawaranHrg.Focus()
            Exit Sub
        End If

        'get_jam()

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            SQL = "INSERT INTO EMI_Master_Ongkir_Lain (Kode_Perusahaan, Id_Ekspedisi, Nama_Ekspedisi, No_Penawaran, Tgl_Penawaran_Hrg, Periode_Akhir_Penawaran, "
            SQL = SQL & "Id_Provinsi_Asal, Provinsi_Asal, "
            SQL = SQL & "Id_Provinsi_Tujuan, Provinsi_Tujuan, "
            SQL = SQL & "Id_Kab_Kota_Asal, Kab_Kota_Asal, "
            SQL = SQL & "Id_Kab_Kota_Tujuan, Kab_Kota_Tujuan, "
            SQL = SQL & "Id_Kecamatan_Asal, Kecamatan_Asal, "
            SQL = SQL & "Id_Kecamatan_Tujuan, Kecamatan_Tujuan, "
            SQL = SQL & "Id_Kelurahan_Asal, Kelurahan_Asal, "
            SQL = SQL & "Id_Kelurahan_Tujuan, Kelurahan_Tujuan, "
            SQL = SQL & "Lokasi_Asal, Lokasi_Tujuan, "
            SQL = SQL & "Id_Cara_Kirim, Cara_Kirim, "
            SQL = SQL & "Id_Media_Kirim, Media_Kirim, "
            '
            SQL = SQL & "P, L, T, Volume, "
            SQL = SQL & "Satuan_Panjang, Satuan_Volume, "
            SQL = SQL & "Berat, Satuan_Berat, "
            '
            'SQL = SQL & "Id_Ukuran, Ukuran, "
            SQL = SQL & "Id_Satuan, Satuan, "
            SQL = SQL & "Harga) "
            SQL = SQL & "VALUES ('" & KodePerusahaan & "', '" & ongkirIdEkspedisi.Trim & "', "
            SQL = SQL & "'" & ongkirNmEkspedisi.Trim & "', "
            SQL = SQL & "'" & ongkirNoPenawaran.Trim & "', "
            SQL = SQL & "'" & Format(DtpOngkir_TglPenawaranHrg.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(DtpOngkir_PeriodeAkhirPenawaran.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & arrIdProvAsal.Item(Cmb_ProvAsal.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirProvAsal.Trim.ToString & "', "
            SQL = SQL & "'" & arrIdProvTujuan.Item(Cmb_ProvTujuan.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirProvTujuan.Trim.ToString & "', "
            SQL = SQL & "'" & arrIdKabKotaAsal.Item(Cmb_KabKotaAwal.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirKabKotaAsal.Trim.ToString & "', "
            SQL = SQL & "'" & arrIdKabKotaTujuan.Item(Cmb_KabKotaTujuan.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirKabkotaTujuan.Trim.ToString & "', "
            SQL = SQL & "'" & arrIdKecAsal.Item(Cmb_KecAsal.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirKecamatanAsal.Trim.ToString & "', "
            SQL = SQL & "'" & arrIdKecTujuan.Item(Cmb_KecTujuan.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirKecamatanTujuan.Trim.ToString & "', "
            SQL = SQL & "'" & arrIdKelAsal.Item(Cmb_KelAsal.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirKelurahanAsal.Trim.ToString & "', "
            SQL = SQL & "'" & arrIdKelTujuan.Item(Cmb_KelTujuan.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirKelurahanTujuan.Trim.ToString & "', "
            SQL = SQL & "'" & ongkirLokasiAwal.Trim & "', "
            SQL = SQL & "'" & ongkirLokasiTujuan.Trim & "', "
            SQL = SQL & "'" & arrCaraKirim.Item(Cmb_CaraKirim.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirCaraKirim.Trim.ToString & "', "
            SQL = SQL & "'" & arrMediaKirim.Item(Cmb_MediaKirim.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirMediaKirim.Trim.ToString & "', "
            '
            SQL = SQL & "'" & panjang.Trim & "', '" & lebar.Trim & "', '" & tinggi.Trim & "', "
            SQL = SQL & "'" & volume.Trim & "', '" & satuanPanjang.Trim.ToString & "', "
            SQL = SQL & "'" & satuanVolume.Trim.ToString & "', "
            SQL = SQL & "'" & berat.Trim & "', '" & satuanBerat.Trim.ToString & "', "
            '
            'SQL = SQL & " '" & arrUkuran.Item(Cmb_UkuranKontainer.SelectedIndex) & "', "
            'SQL = SQL & "'" & ongkirUkuranKontainer.Trim.ToString & "', "
            SQL = SQL & "'" & arrSatuan.Item(CmbOngkir_Satuan.SelectedIndex) & "', "
            SQL = SQL & "'" & ongkirSatuan.Trim.ToString & "', "
            SQL = SQL & " '" & ongkirHarga.Trim & "') "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
        Txt_NmEkspedisi.Focus()
    End Sub

    Private Sub Cmb_CaraKirim_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_CaraKirim.SelectedIndexChanged
        Get_Media_Kirim()
    End Sub

    Private Sub BtnOngkir_Refresh_Click(sender As Object, e As EventArgs) Handles BtnOngkir_Refresh.Click
        kosong()
        Txt_NmEkspedisi.Focus()
    End Sub

    Private Sub Txt_NmEkspedisi_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmEkspedisi.TextChanged
        If Txt_NmEkspedisi.Text.Trim.Length = 0 Then
            Lv_AutoCompleteNmEkspedisi.Visible = False : Exit Sub
        Else
            Lv_AutoCompleteNmEkspedisi.Visible = True
        End If

        Lv_AutoCompleteNmEkspedisi.Items.Clear()

        Try
            OpenConn()

            SQL = "SELECT Id_Ekspedisi, Kode_Ekspedisi, Nama_Ekspedisi "
            SQL = SQL & "From EMI_Master_Ekspedisi "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Nama_Ekspedisi LIKE '" & Txt_NmEkspedisi.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_AutoCompleteNmEkspedisi.Items.Add(Dr("Id_Ekspedisi"))
                    lv.SubItems.Add(Dr("Kode_Ekspedisi"))
                    lv.SubItems.Add(Dr("Nama_Ekspedisi"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvAutoCompleteSupplier_DoubleClick(sender As Object, e As EventArgs) Handles LvAutoCompleteSupplier.DoubleClick
        If LvAutoCompleteSupplier.Items.Count = 0 Then Exit Sub
        Dim Kode As String = LvAutoCompleteSupplier.FocusedItem.Text
        Dim Nama As String = LvAutoCompleteSupplier.FocusedItem.SubItems(1).Text

        TxtPO_KdSupplier.Text = Kode
        TxtPO_NmSupplier.Text = Nama
        Lbl_KdSupplier.Text = Kode
        Lbl_NmSupplier.Text = Nama

        LvAutoCompleteSupplier.Visible = False
        Txt_NoPenawaran.Focus()
    End Sub

    Private Sub Btn_Refresh_Click_1(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Cmb_KabKotaAwal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KabKotaAwal.SelectedIndexChanged
        Get_Kecamatan_Asal()
    End Sub

    Private Sub Cmb_KabKotaTujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KabKotaTujuan.SelectedIndexChanged
        Get_Kecamatan_Tujuan()
    End Sub

    Private Sub Txt_NmEkspedisi_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmEkspedisi.KeyDown
        If e.KeyCode = Keys.Down Then Lv_AutoCompleteNmEkspedisi.Focus()
    End Sub

    Private Sub Txt_NmEkspedisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmEkspedisi.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_NmEkspedisi.Text.Trim.Length = 0 Then
                Lv_AutoCompleteNmEkspedisi.Visible = False
            End If
            'Txt_Supplier_Leave(Txt_Supplier, e)
        End If

        If e.KeyChar = Chr(13) Then TxtOngkir_NoPenawaran.Focus()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Display_Penawaran_SD_Ongkir_Barang_Lain.ShowDialog()
    End Sub

    Private Sub TxtPO_KdSupplier_TextChanged(sender As Object, e As EventArgs) Handles TxtPO_KdSupplier.TextChanged
        If TxtPO_KdSupplier.Text.Trim.Length = 0 Then
            LvAutoCompleteSupplier.Visible = False : Exit Sub
        Else
            LvAutoCompleteSupplier.Visible = True
        End If

        LvAutoCompleteSupplier.Items.Clear()
        Dim lv As New ListViewItem

        Try

            OpenConn()

            SQL = "Select b.kode_supplier, b.nama "
            SQL = SQL & "from suppliers b "
            SQL = SQL & "where  "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_supplier like '%" & TxtPO_KdSupplier.Text & "%' "

            'If CmbLokasi.SelectedIndex = 0 Then
            '    SQL = SQL & " and a.lokasi in("
            '    Dim list_kota As String = ""
            '    For x As Integer = 1 To CmbLokasi.Items.Count - 1
            '        list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
            '    Next

            '    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            '    SQL = SQL & list_kota & ")"
            'Else
            '    SQL = SQL & " and a.lokasi = '" & CmbLokasi.Text & "'"
            'End If

            SQL = SQL & "order by b.kode_supplier"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvAutoCompleteSupplier.Items.Add(Dr("kode_supplier"))
                    lv.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtPenawaran_NoFaktur_Leave(sender As Object, e As EventArgs) Handles TxtPenawaran_NoFaktur.Leave

        If TxtPenawaran_NoFaktur.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            publicFlagRelease = "T"
            Btn_Release.Visible = True
            DgvMaster_Penawaran.Rows.Clear()

            Dim ada_data As String = ""
            Dim flag_release_fix As String = ""
            Dim IndexTambahan As Integer = DgvMaster_Penawaran.Rows.Count

            '=======================
            '=     IS RELEASE?     =
            '=======================
            SQL = "select a.No_Faktur, a.No_Penawaran, a.Kode_Supplier, b.nama as nama_supplier, a.flag_release, a.NoUrut, a.Tgl_Penawaran_Hrg, a.Periode_Akhir_Penawaran "
            SQL = SQL & "from EMI_Master_Penawaran_Barang_Lain a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & TxtPenawaran_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    ada_data = "Y"

                    TxtPenawaran_NoFaktur.Text = Dr("No_Faktur")
                    Txt_NoPenawaran.Text = Dr("No_Penawaran")
                    TxtPO_KdSupplier.Text = Dr("Kode_Supplier")
                    Txt_NoUrut.Text = Dr("NoUrut")

                    noPenawaran = Dr("No_Penawaran")
                    kodeSupplier = Dr("Kode_Supplier")

                    Dim tglPeriodeAwal As DateTime = Dr("Tgl_Penawaran_Hrg")
                    Dim tglPeriodeAkhir As DateTime = Dr("Periode_Akhir_Penawaran")

                    Dtp_Tgl.Value = tglPeriodeAwal
                    Dtp_PeriodAkhir.Value = tglPeriodeAkhir

                    'TxtPO_NmSupplier.Text = Dr("nama_supplier")
                    'Lbl_KdSupplier.Text = Dr("Kode_Supplier")
                    'Lbl_NmSupplier.Text = Dr("nama_supplier")

                    If General_Class.CekNULL(Dr("flag_release")) = "Y" Then
                        publicFlagRelease = "Y"
                    Else
                        publicFlagRelease = "T"
                    End If

                    TxtPO_KdSupplier_Leave(sender, e)
                Else
                    CloseConn()
                    MessageBox.Show("Data Penawaran Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    kosong()
                    Exit Sub
                End If
            End Using

            If Not String.IsNullOrEmpty(noPenawaran) And Not String.IsNullOrEmpty(kodeSupplier) Then
                SQL = "select top 1 b.Jenis_Pembayaran, b.Tempo_Pembayaran, b.Lama_Pembayaran  "
                SQL = SQL & "from EMI_Master_Penawaran_Barang_Lain a, EMI_Master_Penawaran_Jatuh_Tempo_Barang_Lain b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & TxtPenawaran_NoFaktur.Text & "' and a.No_Penawaran = '" & noPenawaran & "' "
                SQL = SQL & "and a.Kode_Supplier = '" & kodeSupplier & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                If .Rows(i).Item("Jenis_Pembayaran") = "N" Then
                                    If Not .Rows(i).Item("Tempo_Pembayaran") = "" Then

                                        cmb_JenisBayar.SelectedIndex = 1
                                        cmbJenisPengiriman.SelectedItem = .Rows(i).Item("Tempo_Pembayaran")
                                        txtJatuhTempo.Text = .Rows(i).Item("Lama_Pembayaran")

                                    End If
                                End If
                            Next
                        End If
                    End With

                End Using

            End If

            '==============================
            '=     CEK APAKAH ADA DATA    =
            '==============================
            If ada_data = "Y" Then

                '====================================
                '=     CEK APAKAH SUDAH RELEASE?    =
                '====================================
                If publicFlagRelease = "Y" Then
                    Btn_PilihBarang.Enabled = False
                    Btn_Simpan.Enabled = False
                    DgvMaster_Penawaran.ReadOnly = True
                    Btn_Release.Visible = False

                    Txt_NoPenawaran.ReadOnly = True
                    TxtPO_KdSupplier.ReadOnly = True
                    TxtPO_NmSupplier.ReadOnly = True
                    Dtp_Tgl.Enabled = False
                    Dtp_PeriodAkhir.Enabled = False

                    cmb_JenisBayar.Enabled = False
                    cmbJenisPengiriman.Enabled = False
                    txtJatuhTempo.ReadOnly = True
                Else
                    Btn_PilihBarang.Enabled = True
                    Btn_Simpan.Enabled = True
                    DgvMaster_Penawaran.ReadOnly = False
                    Btn_Release.Visible = True

                    Txt_NoPenawaran.ReadOnly = False
                    TxtPO_KdSupplier.ReadOnly = False
                    TxtPO_NmSupplier.ReadOnly = False
                    Dtp_Tgl.Enabled = True
                    Dtp_PeriodAkhir.Enabled = True

                    cmb_JenisBayar.Enabled = True
                    cmbJenisPengiriman.Enabled = True
                    txtJatuhTempo.ReadOnly = False

                End If

                '===========================
                '=     LOAD BAHAN BAKU     =
                '===========================
                SQL = "select a.Kode_Perusahaan, a.No_Faktur, b.Kode_Barang, c.Nama, d.Kode_Group_Jenis, c.Flag_PPN, "
                SQL = SQL & "b.Min_Order, b.Satuan, b.Mata_Uang, b.Harga_Satuan "
                SQL = SQL & "from EMI_Master_Penawaran_Barang_Lain a, EMI_Master_Penawaran_Detail_Barang_Lain b, Barang_Lain c, EMI_Group_Jenis_Lain d "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "and c.Id_Group_Jenis = d.Id_Group_Jenis "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and d.Flag_Raw_Material = 'Y' "
                SQL = SQL & "and a.No_Faktur = '" & TxtPenawaran_NoFaktur.Text & "' "
                SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur,b.Kode_Barang, c.Nama, d.Kode_Group_Jenis, c.Flag_PPN, "
                SQL = SQL & "b.Min_Order, b.Satuan, b.Mata_Uang, b.Harga_Satuan "
                Using ds = BindingTrans(SQL)
                    With ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                DgvMaster_Penawaran.Rows.Add(1)
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellKdBrg).Value = .Rows(i).Item("kode_barang")
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellNmBrg).Value = .Rows(i).Item("nama")

                                If .Rows(i).Item("Flag_PPN") = "Y" Then
                                    DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellPPN).Value = "PPN"
                                Else
                                    DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellPPN).Value = "No PPN"
                                End If

                                SQL = "select satuan, flag_tampil_display from barang_Detail_Satuan_lain where Kode_Barang ='" & .Rows(i).Item("kode_barang") & "'  and Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and Flag_Tampil_Display = 'Y'"
                                Using Ds2 = BindingTrans(SQL)

                                    For indexBaru As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                                        DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellSatuan).Value = Ds2.Tables("MyTable").Rows(indexBaru).Item("satuan")
                                    Next
                                End Using

                                'Load Isian
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellMinOrder).Value = Format(.Rows(i).Item("Min_Order"), "N2")
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellMUA).Value = .Rows(i).Item("Mata_Uang")
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellHrgSatuan).Value = Format(.Rows(i).Item("Harga_Satuan"), "N2")

                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellKdBrg).ReadOnly = True
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellNmBrg).ReadOnly = True
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellSatuan).ReadOnly = True

                                DgvMaster_Penawaran.Rows(IndexTambahan).DefaultCellStyle.BackColor = Color.LightYellow

                                IndexTambahan = IndexTambahan + 1

                            Next

                        End If
                    End With
                End Using

                '==========================
                '=     LOAD PACKAGING     =
                '==========================
                SQL = "select a.Kode_Perusahaan, a.No_Faktur, b.Kode_Barang, c.Nama, d.Kode_Group_Jenis, c.Flag_PPN, "
                SQL = SQL & "b.Min_Order, b.Satuan, b.Mata_Uang, b.Harga_Satuan "
                SQL = SQL & "from EMI_Master_Penawaran_Barang_Lain a, EMI_Master_Penawaran_Detail_Barang_Lain b, Barang_Lain c, EMI_Group_Jenis_Lain d "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "and c.Id_Group_Jenis = d.Id_Group_Jenis "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and d.flag_packaging = 'Y'  "
                SQL = SQL & "and a.No_Faktur = '" & TxtPenawaran_NoFaktur.Text & "' "
                SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur,b.Kode_Barang, c.Nama, d.Kode_Group_Jenis, c.Flag_PPN, "
                SQL = SQL & "b.Min_Order, b.Satuan, b.Mata_Uang, b.Harga_Satuan "
                Using ds = BindingTrans(SQL)
                    With ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                DgvMaster_Penawaran.Rows.Add(1)
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellKdBrg).Value = .Rows(i).Item("kode_barang")
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellNmBrg).Value = .Rows(i).Item("nama")

                                If .Rows(i).Item("Flag_PPN") = "Y" Then
                                    DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellPPN).Value = "PPN"
                                Else
                                    DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellPPN).Value = "No PPN"
                                End If

                                SQL = "select satuan, flag_tampil_display from barang_Detail_Satuan_lain where Kode_Barang ='" & .Rows(i).Item("kode_barang") & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and Flag_Tampil_Display = 'Y' "
                                Using Ds2 = BindingTrans(SQL)

                                    For indexBaru As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                                        DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellSatuan).Value = Ds2.Tables("MyTable").Rows(indexBaru).Item("satuan")
                                    Next

                                End Using

                                'Load Isian
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellMinOrder).Value = .Rows(i).Item("Min_Order")
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellMUA).Value = .Rows(i).Item("Mata_Uang")
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellHrgSatuan).Value = .Rows(i).Item("Harga_Satuan")

                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellKdBrg).ReadOnly = True
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellNmBrg).ReadOnly = True
                                DgvMaster_Penawaran.Rows(IndexTambahan).Cells(cellSatuan).ReadOnly = True

                                DgvMaster_Penawaran.Rows(IndexTambahan).DefaultCellStyle.BackColor = Color.LightYellow

                                IndexTambahan = IndexTambahan + 1

                            Next

                        End If
                    End With
                End Using

            End If

            If Not publicFlagRelease = "Y" Then
                DgvMaster_Penawaran.Rows.Add(1)
            End If

            'Set Button Simpan menjadi Update
            Btn_Simpan.Tag = "&Update"
            Btn_Simpan.Text = "&Update"

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_AutoCompleteNmEkspedisi_DoubleClick(sender As Object, e As EventArgs) Handles Lv_AutoCompleteNmEkspedisi.DoubleClick
        If Lv_AutoCompleteNmEkspedisi.Items.Count = 0 Then Exit Sub
        Dim ID As String = Lv_AutoCompleteNmEkspedisi.FocusedItem.Text
        Dim Kode As String = Lv_AutoCompleteNmEkspedisi.FocusedItem.SubItems(1).Text
        Dim Nama As String = Lv_AutoCompleteNmEkspedisi.FocusedItem.SubItems(2).Text

        'Txt_Supplier.Text = Kode + " (" + Nama + ")"
        'Lbl_KdSupplier.Text = Kode
        Lbl_IdEkspedisi.Text = ID
        Lbl_KdEkspedisi.Text = Kode
        Lbl_NmEkspedisi.Text = Nama
        Txt_NmEkspedisi.Text = Kode + " (" + Nama + ")"

        Lv_AutoCompleteNmEkspedisi.Visible = False
        TxtOngkir_NoPenawaran.Focus()
    End Sub

    Private Sub TxtOngkir_NoPenawaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtOngkir_NoPenawaran.KeyPress
        If e.KeyChar = Chr(13) Then DtpOngkir_TglPenawaranHrg.Focus()
    End Sub

    Private Sub Cmb_KecAsal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KecAsal.SelectedIndexChanged
        Get_Kelurahan_Asal()
    End Sub

    Private Sub Cmb_KecTujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KecTujuan.SelectedIndexChanged
        Get_Kelurahan_Tujuan()
    End Sub

    Private Sub DtpOngkir_TglPenawaranHrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DtpOngkir_TglPenawaranHrg.KeyPress
        If e.KeyChar = Chr(13) Then DtpOngkir_PeriodeAkhirPenawaran.Focus()
    End Sub

    Private Sub Cmb_ProvAsal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ProvAsal.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KabKotaAwal.Focus()
    End Sub

    Private Sub Cmb_ProvTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ProvTujuan.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KabKotaTujuan.Focus()
    End Sub

    Private Sub CmbOngkir_Satuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbOngkir_Satuan.SelectedIndexChanged
        If CmbOngkir_Satuan.SelectedIndex = -1 Then
            Exit Sub
        End If
        If CmbOngkir_Satuan.Text.ToUpper = "TONASE" Then
            Txt_Berat.Enabled = False
            Cmb_SatuanBerat.Enabled = False
            Txt_Berat.Text = "1"
            Cmb_SatuanBerat.Text = "TON"
        Else

            Txt_Berat.Enabled = True
            Cmb_SatuanBerat.Enabled = True

            Txt_Berat.Text = ""
            Cmb_SatuanBerat.SelectedIndex = -1
        End If
    End Sub

    Private Sub Cmb_KabKotaAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KabKotaAwal.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KecAsal.Focus()
    End Sub

    Private Sub Cmb_KabKotaTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KabKotaTujuan.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KecTujuan.Focus()
    End Sub

    Private Sub Txt_LokasiAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_LokasiAwal.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_ProvTujuan.Focus()
    End Sub

    Private Sub TxtOngkir_LokasiTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtOngkir_LokasiTujuan.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_CaraKirim.Focus()
    End Sub

    Private Sub Cmb_CaraKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_CaraKirim.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_MediaKirim.Focus()
    End Sub

    Private Sub Cmb_MediaKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_MediaKirim.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Panjang.Focus()
    End Sub

    'Private Sub Cmb_UkuranKontainer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_UkuranKontainer.KeyPress
    '    If e.KeyChar = Chr(13) Then CmbOngkir_Satuan.Focus()
    'End Sub

    Private Sub TxtOngkir_Hrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtOngkir_Hrg.KeyPress
        If e.KeyChar = Chr(13) Then BtnOngkir_Simpan.Focus()
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Txt_NoPenawaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoPenawaran.KeyPress
        'If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
        '    e.Handled = True
        'End If

        If e.KeyChar = Chr(13) Then TxtPO_KdSupplier.Focus()
    End Sub

    Private Sub DtpOngkir_PeriodeAkhirPenawaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DtpOngkir_PeriodeAkhirPenawaran.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_ProvAsal.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Master_Penawaran_SD_Barang_Lain.lokasi()
        Master_Penawaran_SD_Barang_Lain.asal = Jenis
        Master_Penawaran_SD_Barang_Lain.filter_kdSupplier = " and a.Kode_Supplier = '" & Lbl_KdSupplier.Text & "' "
        Master_Penawaran_SD_Barang_Lain.CmbPilihBarang_Lokasi.Text = Lbl_BindingLokasiGudang.Text

        Master_Penawaran_SD_Barang_Lain.ShowDialog()
    End Sub

    Private Sub Btn_PilihBarang_Click(sender As Object, e As EventArgs) Handles Btn_PilihBarang.Click
        Emi_Display_Barang_Penawaran_Barang_Lain.dari = "Transaksi Penawaran Lain"
        Emi_Display_Barang_Penawaran_Barang_Lain.ShowDialog()
    End Sub

    Private Sub DgvMaster_Penawaran_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvMaster_Penawaran.CellEndEdit
        Get_Isi_Listview(DgvMaster_Penawaran.CurrentRow.Index)

        If lvKdBrg = "" Then
            DgvMaster_Penawaran.CurrentRow.Cells(cellMinOrder).Value = ""
            DgvMaster_Penawaran.CurrentRow.Cells(cellMUA).Value = ""
            DgvMaster_Penawaran.CurrentRow.Cells(cellHrgSatuan).Value = ""
            Exit Sub
        End If

        If IsNumeric(lvMinOrder) = False Or Val(lvMinOrder) < 0 Then
            DgvMaster_Penawaran.CurrentRow.Cells(cellMinOrder).Value = 0
            DgvMaster_Penawaran.CurrentRow.Cells(cellMUA).Value = ""
            DgvMaster_Penawaran.CurrentRow.Cells(cellHrgSatuan).Value = 0
            Exit Sub
        End If

        If IsNumeric(lvHrgSatuan) = False Or Val(lvHrgSatuan) < 0 Then
            DgvMaster_Penawaran.CurrentRow.Cells(cellHrgSatuan).Value = 0
            DgvMaster_Penawaran.CurrentRow.Cells(cellMUA).Value = ""
            DgvMaster_Penawaran.CurrentRow.Cells(cellMinOrder).Value = 0
            Exit Sub
        End If

        If DgvMaster_Penawaran.CurrentCell.ColumnIndex = cellMinOrder Then
            If DgvMaster_Penawaran.CurrentCell.Value.Contains(",") Then
                CloseConn()
                MessageBox.Show("Min Order Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DgvMaster_Penawaran.CurrentRow.Cells(cellMinOrder).Value = 0
                DgvMaster_Penawaran.CurrentRow.Cells(cellMUA).Value = ""
                DgvMaster_Penawaran.CurrentRow.Cells(cellHrgSatuan).Value = 0
                Exit Sub
            End If

        ElseIf DgvMaster_Penawaran.CurrentCell.ColumnIndex = cellMinOrder Then
            If DgvMaster_Penawaran.CurrentCell.Value.Contains(",") Then
                CloseConn()
                MessageBox.Show("Harga Satuan Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DgvMaster_Penawaran.CurrentRow.Cells(cellHrgSatuan).Value = 0
                DgvMaster_Penawaran.CurrentRow.Cells(cellMUA).Value = ""
                DgvMaster_Penawaran.CurrentRow.Cells(cellMinOrder).Value = 0
                Exit Sub
            End If
        End If

        Dim value1 As String = DgvMaster_Penawaran.CurrentRow.Cells(cellMinOrder).Value
        Dim value2 As String = DgvMaster_Penawaran.CurrentRow.Cells(cellHrgSatuan).Value

        Dim nilai1 As Decimal = Decimal.Parse(value1)
        Dim formattedValue1 As String = nilai1.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))
        Dim nilai2 As Decimal = Decimal.Parse(value2)
        Dim formattedValue2 As String = nilai2.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

        DgvMaster_Penawaran.CurrentRow.Cells(cellMinOrder).Value = formattedValue1
        DgvMaster_Penawaran.CurrentRow.Cells(cellHrgSatuan).Value = formattedValue2

    End Sub

    Private Sub DgvMaster_Penawaran_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvMaster_Penawaran.CellEnter

        If DgvMaster_Penawaran.CurrentCell.ColumnIndex = cellMinOrder Or DgvMaster_Penawaran.CurrentCell.ColumnIndex = cellHrgSatuan Then
            Dim CekCell As String = DgvMaster_Penawaran.CurrentCell.Value

            If CekCell = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = CekCell.Replace(",", "") ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            DgvMaster_Penawaran.CurrentCell.Value = nilai
        End If

    End Sub

    Private Sub DgvMaster_Penawaran_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DgvMaster_Penawaran.CellLeave

        If DgvMaster_Penawaran.CurrentCell.ColumnIndex = cellMinOrder Or DgvMaster_Penawaran.CurrentCell.ColumnIndex = cellHrgSatuan Then

            Dim value1 As String = DgvMaster_Penawaran.CurrentRow.Cells(cellMinOrder).Value
            Dim value2 As String = DgvMaster_Penawaran.CurrentRow.Cells(cellHrgSatuan).Value

            If Not String.IsNullOrEmpty(value1) Then

                Dim nilai1 As Decimal = Decimal.Parse(value1)
                Dim formattedValue1 As String = nilai1.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

                DgvMaster_Penawaran.CurrentRow.Cells(cellMinOrder).Value = formattedValue1

            End If

            If Not String.IsNullOrEmpty(value2) Then
                Dim nilai2 As Decimal = Decimal.Parse(value2)
                Dim formattedValue2 As String = nilai2.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

                DgvMaster_Penawaran.CurrentRow.Cells(cellHrgSatuan).Value = formattedValue2
            End If
        End If
    End Sub

    Private Sub DgvMaster_Penawaran_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DgvMaster_Penawaran.EditingControlShowing
        Dim comboBox As System.Windows.Forms.ComboBox = TryCast(e.Control, System.Windows.Forms.ComboBox)
        If comboBox IsNot Nothing Then
            ' Buka dropdown ComboBox langsung
            Dim tmr As New Timer()
            tmr.Interval = 100 ' Atur ke 100ms (waktu yang cukup singkat)
            AddHandler tmr.Tick, Sub()
                                     tmr.Stop()
                                     comboBox.DroppedDown = True
                                 End Sub
            tmr.Start()
        End If
    End Sub

    Private Sub DgvMaster_Penawaran_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DgvMaster_Penawaran.CellValidating
        'If e.ColumnIndex = cellMinOrder Then
        '    If DgvMaster_Penawaran.IsCurrentCellDirty Then
        '        If Not IsNumeric(e.FormattedValue) Then
        '            e.Cancel = True
        '            MessageBox.Show("Hanya Input Angka" & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub DgvMaster_Penawaran_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvMaster_Penawaran.KeyDown

        If DgvMaster_Penawaran.Rows.Count = 0 Or DgvMaster_Penawaran.SelectedCells.Count = 0 Then Exit Sub

        Dim currentRow = DgvMaster_Penawaran.CurrentRow.Index

        If Not publicFlagRelease = "Y" Then
            If e.KeyCode = Keys.F1 Then

                Master_Penawaran_SD_Barang_Lain.lokasi()
                Master_Penawaran_SD_Barang_Lain.asal = Jenis
                Master_Penawaran_SD_Barang_Lain.filter_kdSupplier = " and a.Kode_Supplier = '" & Lbl_KdSupplier.Text & "' "
                Master_Penawaran_SD_Barang_Lain.CmbPilihBarang_Lokasi.Text = Lbl_BindingLokasiGudang.Text

                Master_Penawaran_SD_Barang_Lain.ShowDialog()

            ElseIf e.KeyCode = Keys.Insert Then

                If currentRow <> DgvMaster_Penawaran.Rows.Count - 1 Then
                    DgvMaster_Penawaran.Rows.Insert(currentRow + 1)
                End If
            ElseIf e.KeyCode = Keys.Delete Then

                If DgvMaster_Penawaran.Rows.Count <> 0 Then
                    If Not DgvMaster_Penawaran.CurrentRow.Cells(0).Value = "" Then

                        BeginInvoke(New MethodInvoker(Sub() DgvMaster_Penawaran.Rows.RemoveAt(currentRow)))
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub CmbPO_JnsBayar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisBayar.SelectedIndexChanged
        If cmb_JenisBayar.SelectedIndex = 0 Then

            cmbJenisPengiriman.Items.Clear()
            cmbJenisPengiriman.Enabled = False
            cmbJenisPengiriman.SelectedIndex = -1
            txtJatuhTempo.Enabled = False
            txtJatuhTempo.Text = ""
        Else

            cmbJenisPengiriman.Enabled = True

            txtJatuhTempo.Enabled = True
            txtJatuhTempo.Text = ""

            cmbJenisPengiriman.Items.Clear()
            cmbJenisPengiriman.Items.Add("ETA")
            cmbJenisPengiriman.Items.Add("ETD")

            cmbJenisPengiriman.SelectedIndex = 0

        End If
    End Sub

    Private Sub TxtPenawaran_NoFaktur_TextChanged(sender As Object, e As EventArgs) Handles TxtPenawaran_NoFaktur.TextChanged

    End Sub

    Private Sub Cmb_KecAsal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KecAsal.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KelAsal.Focus()
    End Sub

    Private Sub Cmb_KelAsal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KelAsal.KeyPress
        If e.KeyChar = Chr(13) Then Txt_LokasiAwal.Focus()
    End Sub

    Private Sub Cmb_KecTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KecTujuan.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KelTujuan.Focus()
    End Sub

    Private Sub Cmb_KelTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KelTujuan.KeyPress
        If e.KeyChar = Chr(13) Then TxtOngkir_LokasiTujuan.Focus()
    End Sub

    Private Sub Cmb_SatuanPanjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_SatuanPanjang.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Berat.Focus()
    End Sub

    Private Sub CmbOngkir_Satuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbOngkir_Satuan.KeyPress
        If e.KeyChar = Chr(13) Then TxtOngkir_Hrg.Focus()
    End Sub

    Private Sub Cmb_SatuanBerat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_SatuanBerat.KeyPress
        If e.KeyChar = Chr(13) Then CmbOngkir_Satuan.Focus()
    End Sub

    Private Sub TxtPO_KdSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_KdSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtPO_KdSupplier.Text.Trim.Length = 0 Then
                LvAutoCompleteSupplier.Visible = False : TxtPO_NmSupplier.Focus() : Exit Sub
            End If
            TxtPO_KdSupplier_Leave(TxtPO_KdSupplier, e)
            Btn_Simpan.Focus()
        End If
    End Sub

    Private Sub TxtPO_NmSupplier_TextChanged(sender As Object, e As EventArgs) Handles TxtPO_NmSupplier.TextChanged
        If TxtPO_NmSupplier.Text.Trim.Length = 0 Then
            LvAutoCompleteSupplier.Visible = False : Exit Sub
        Else
            LvAutoCompleteSupplier.Visible = True
        End If

        LvAutoCompleteSupplier.Items.Clear()
        Dim lv As New ListViewItem

        Try

            OpenConn()

            SQL = "Select b.kode_supplier, b.nama "
            SQL = SQL & "from suppliers b "
            SQL = SQL & "where  "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.nama like '%" & TxtPO_NmSupplier.Text & "%' "

            'If CmbLokasi.SelectedIndex = 0 Then
            '    SQL = SQL & " and a.lokasi in("
            '    Dim list_kota As String = ""
            '    For x As Integer = 1 To CmbLokasi.Items.Count - 1
            '        list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
            '    Next

            '    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            '    SQL = SQL & list_kota & ")"
            'Else
            '    SQL = SQL & " and a.lokasi = '" & CmbLokasi.Text & "'"
            'End If

            SQL = SQL & "order by b.kode_supplier"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvAutoCompleteSupplier.Items.Add(Dr("kode_supplier"))
                    lv.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtPO_NmSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_NmSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtPO_KdSupplier.Text.Trim.Length = 0 Then TxtPO_NmSupplier.Text = "" : LvAutoCompleteSupplier.Visible = False ': Exit Sub
            Btn_Simpan.Focus()
        End If
    End Sub

    Private Sub TxtPO_KdSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPO_KdSupplier.KeyDown
        If e.KeyCode = Keys.Down Then
            LvAutoCompleteSupplier.Focus()
        End If
    End Sub

    Private Sub TxtPO_KdSupplier_Leave(sender As Object, e As EventArgs) Handles TxtPO_KdSupplier.Leave
        If TxtPO_KdSupplier.ReadOnly = True Then Exit Sub

        If TxtPO_KdSupplier.Text.Trim.Length = 0 Then
            LvAutoCompleteSupplier.Visible = False : Exit Sub
        Else
            LvAutoCompleteSupplier.Visible = True
        End If
        If LvAutoCompleteSupplier.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "SELECT Kode_Supplier, nama FROM suppliers WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and kode_supplier like '" & TxtPO_KdSupplier.Text & "%' ORDER BY nama"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtPO_KdSupplier.Text = Dr("kode_supplier")
                    TxtPO_NmSupplier.Text = Dr("nama")
                Else
                    TxtPO_KdSupplier.Text = ""
                    TxtPO_NmSupplier.Text = ""
                    TxtPO_KdSupplier.Focus()
                End If
                LvAutoCompleteSupplier.Visible = False
            End Using



            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
    End Sub

    Private Sub TxtPO_NmSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPO_NmSupplier.KeyDown
        If e.KeyCode = Keys.Down Then
            LvAutoCompleteSupplier.Focus()
        End If
    End Sub

    Private Sub TxtPO_NmSupplier_Leave(sender As Object, e As EventArgs) Handles TxtPO_NmSupplier.Leave
        If LvAutoCompleteSupplier.Focused = True Then Exit Sub
        If TxtPO_NmSupplier.ReadOnly = True Then Exit Sub
        TxtPO_KdSupplier.Text = "" : TxtPO_NmSupplier.Text = ""
    End Sub

    Private Sub Dtp_Tgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dtp_Tgl.KeyPress
        If e.KeyChar = Chr(13) Then Dtp_PeriodAkhir.Focus()
    End Sub

    Private Sub Dtp_PeriodAkhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dtp_PeriodAkhir.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NoPenawaran.Focus()
    End Sub

    Private Sub DgvMaster_Penawaran_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvMaster_Penawaran.CellFormatting
        If TypeOf DgvMaster_Penawaran.Columns(e.ColumnIndex) Is DataGridViewComboBoxColumn AndAlso e.RowIndex >= 0 Then
            ' Mengubah warna background dan teks untuk cell ComboBox
            e.CellStyle.BackColor = Color.LightYellow
            e.CellStyle.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TxtPenawaran_NoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPenawaran_NoFaktur.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NoPenawaran.Focus()
    End Sub

    Private Sub LvAutoCompleteSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles LvAutoCompleteSupplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvAutoCompleteSupplier_DoubleClick(LvAutoCompleteSupplier, e)
        End If
    End Sub

    Private Sub Lv_AutoCompleteNmEkspedisi_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_AutoCompleteNmEkspedisi.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_AutoCompleteNmEkspedisi_DoubleClick(Lv_AutoCompleteNmEkspedisi, e)
        End If
    End Sub

    Private Sub LvAutoCompleteSupplier_ParentChanged(sender As Object, e As EventArgs) Handles LvAutoCompleteSupplier.ParentChanged

    End Sub


    'Private Sub DgvMaster_Penawaran_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvMaster_Penawaran.CellClick
    '    If DgvMaster_Penawaran.Rows.Count = 0 Then
    '        Exit Sub
    '    End If

    '    Dim currentRow = DgvMaster_Penawaran.CurrentRow.Index
    '    Dim currentCell = DgvMaster_Penawaran.CurrentCellAddress.X

    '    Dim data = DgvMaster_Penawaran.Rows(currentRow).Cells(currentCell)

    '    If currentCell = cellMUA Or currentCell = cellSatuan Then
    '        DgvMaster_Penawaran.BeginEdit(True)  ' Memaksa cell untuk langsung masuk mode edit
    '    End If
    'End Sub

    'Private Sub Get_Nama_Barang()
    '    Try
    '        OpenConn()

    '        SQL = "SELECT Nama From Barang_Lain "
    '        SQL = SQL & "Where Kode_Barang = '" & Lbl_GetKdBrg.Text & "' and "
    '        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
    '        Using Dr = OpenTrans(SQL)
    '            If Dr.Read() Then
    '                DgvMaster_Penawaran.CurrentRow.Cells(cellNmBrg).Value = Dr("Nama")
    '            End If
    '        End Using
    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

End Class
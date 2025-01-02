Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Hasil_ProductionFG
    Dim Jenis = "Display_Production_Order"
    Public fno_po As String

    Dim arrSO, arrInisialFaktur As New ArrayList

    Dim LvKode_So As String
    Dim LvKode_Bahan As String
    ''Dim LvNama_Bahan As String
    Dim LvNilai_Formula As String
    Dim LvNilai_Produksi As String
    Dim LvSatuan As String

    Dim CellKode_So As Integer = 0
    Dim CellKode_Bahan As Integer = 1
    ''Dim CellNama_Bahan As Integer = 2
    Dim CellNilai_Formula As Integer = 3
    Dim CellNilai_Produksi As Integer = 4
    Dim CellSatuan As Integer = 5


    Dim LvKode_So_Pckg As String
    Dim LvKode_Bahan_Pckg As String
    ''Dim LvNama_Bahan_Pckg As String
    Dim LvNilai_Formula_Pckg As String
    Dim LvNilai_Produksi_Pckg As String
    Dim LvSatuan_Pckg As String

    Dim CellKode_So_Pckg As Integer = 0
    Dim CellKode_Bahan_Pckg As Integer = 1
    ''Dim CellNama_Bahan_Pckg As Integer = 2
    Dim CellNilai_Formula_Pckg As Integer = 3
    Dim CellNilai_Produksi_Pckg As Integer = 4
    Dim CellSatuan_Pckg As Integer = 5

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        'LvKode_So = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_So).Value
        'LvKode_Bahan = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_Bahan).Value
        'LvNama_Bahan = Dgv_HslProduction.Rows(No_Index).Cells(CellNama_Bahan).Value
        'LvNilai_Formula = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Formula).Value
        'LvNilai_Produksi = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Produksi).Value
        'LvSatuan = Dgv_HslProduction.Rows(No_Index).Cells(CellSatuan).Value

    End Sub

    Public Sub Get_Isi_Listview_Pckg(ByVal No_Index As Integer)

        'LvKode_So_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_So_Pckg).Value
        'LvKode_Bahan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_Bahan_Pckg).Value
        'LvNama_Bahan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNama_Bahan_Pckg).Value
        'LvNilai_Formula_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Formula_Pckg).Value
        'LvNilai_Produksi_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Produksi_Pckg).Value
        'LvSatuan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellSatuan_Pckg).Value

    End Sub

    Private Sub Get_data()
        Try
            OpenConn()

            TxtGoodStock.Text = ""
            TxtBadStock.Text = ""
            TxtJmlScrap.Text = ""
            Txt_Keterangan.Text = ""

            SQL = "select a.No_Transaksi, a.no_po, a.lokasi, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.jumlah, a.satuan,a.Tgl_Produksi, a.jam_Produksi "
            SQL = SQL & "from Emi_Split_Production_Order a, barang b where "
            SQL = SQL & "a.kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner And a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_transaksi = '" & TxtNoSplit.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    DateTimePicker1.Value = Dr("Tgl_Produksi")
                    TxtJam.Text = Dr("jam_Produksi")
                    TextBox2.Text = Dr("jumlah")
                    TxtNama.Text = Dr("Nama")
                    fno_po = Dr("no_po")
                    TxtKodeBarang.Text = Dr("Kode_Barang")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data Tidak ditemukan . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select isnull(sum(Jumlah),0) as Jumlah from Emi_Produksi_Hasil_Perpallet a "
            SQL = SQL & "where kode_barang='" & TxtKodeBarang.Text & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_split = '" & TxtNoSplit.Text & "' and flag_simpan_pallet is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtHasilProduksi.Text = Dr("jumlah")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data Tidak ditemukan . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim goodstock As Double = 0
            Dim warningstock As Double = 0
            Dim badstock As Double = 0

            SQL = "select isnull(sum(Jumlah),0) as Jumlah from Emi_Produksi_Hasil_Perpallet a "
            SQL = SQL & "where kode_barang='" & TxtKodeBarang.Text & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_split = '" & TxtNoSplit.Text & "' and flag_simpan_pallet is null and jenis='HIJAU' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    goodstock = Dr("jumlah")
                End If
            End Using

            SQL = "select isnull(sum(Jumlah),0) as Jumlah from Emi_Produksi_Hasil_Perpallet a "
            SQL = SQL & "where kode_barang='" & TxtKodeBarang.Text & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_split = '" & TxtNoSplit.Text & "' and flag_simpan_pallet is null and jenis='KUNING' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    warningstock = Dr("jumlah")
                End If
            End Using

            SQL = "select isnull(sum(Jumlah),0) as Jumlah from Emi_Produksi_Hasil_Perpallet a "
            SQL = SQL & "where kode_barang='" & TxtKodeBarang.Text & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_split = '" & TxtNoSplit.Text & "' and flag_simpan_pallet is null and jenis='MERAH' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    badstock = Dr("jumlah")
                End If
            End Using


            TxtBadStock.Text = badstock
            TxtGoodStock.Text = goodstock + warningstock
            TxtBadStock.Enabled = False

            Dim kode_barang_scrap As String = ""
            Dim kode_barang As String = TxtKodeBarang.Text

            SQL = "select kode_barang_Scrap from emi_binding_scrap where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and kode_barang = '" & LvKdBrg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kode_barang_scrap = Dr("kode_barang_scrap")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kode Barang Scrap belum dibinding!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim satuan_display_scrap As String = ""
            SQL = "select top(1) a.satuan, b.satuan as Satuan_Kecil from barang_detail_satuan a, barang b where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_barang = '" & kode_barang_scrap & "' and a.kode_Barang=b.kode_barang "
            SQL = SQL & "and a.flag_tampil_display = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtSatuanScrap.Text = Dr("satuan")
                    TxtSatKecilScrap.Text = Dr("Satuan_Kecil")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang detail satuan tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim satuanKodeBarang As String = ""
            SQL = "select top(1) a.satuan, b.satuan as Satuan_Kecil from barang_detail_satuan a, barang b where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_barang = '" & kode_barang & "' and a.kode_Barang=b.kode_barang "
            SQL = SQL & "and a.flag_tampil_display = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtSatuanQty.Text = Dr("satuan")
                    TxtSatKecilProduksi.Text = Dr("Satuan_Kecil")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang detail satuan tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            ListView1.Items.Clear()

            SQL = "select jumlah,satuan,batch_number, QR_CODE + '-' + Kode_Unik_Berjalan as QR_Code , jenis, tgl_produksi, Tgl_Expired,b.Keterangan "
            SQL = SQL & "from Emi_Produksi_Hasil_Perpallet a,  EMI_Master_Warna b "
            SQL = SQL & "where kode_barang='" & TxtKodeBarang.Text & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_split = '" & TxtNoSplit.Text & "' and flag_simpan_pallet is null "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Jenis = b.Kode_Warna "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read


                    Dim lvw As ListViewItem

                    lvw = ListView1.Items.Add(Dr("qr_code"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lvw.SubItems.Add(Dr("satuan"))

                    lvw.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("tgl_expired"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Dr("keterangan"))
                Loop
            End Using

            SQL = "select count(a.Kode_Perusahaan) as jumlah "
            SQL = SQL & "from Emi_Produksi_Hasil_Perpallet a,  EMI_Master_Warna b "
            SQL = SQL & "where kode_barang='" & TxtKodeBarang.Text & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_split = '" & TxtNoSplit.Text & "' and flag_simpan_pallet is null "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Jenis = b.Kode_Warna "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("jumlah")
                End If
            End Using

            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = "Transaksi - Penerimaan Barang"
            Label8.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi
            Label6.Text = Base_Language.Lang_Global_No_Transaksi
            Label7.Text = Base_Language.Lang_Global_Tanggal_Produksi
            'Label2.Text = Base_Language.Lang_Global_Jam
            Label10.Text = Base_Language.Lang_Global_KodeBarang
            Label9.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi2
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan


            '      ListView1.Clear()


            ListView1.Columns.Clear()
            ListView1.Columns.Add("QR Code", 250, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 110, HorizontalAlignment.Right)
            ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 110, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal_Produksi, 100, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal_Expired, 100, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.lang_global_keterangan, 160, HorizontalAlignment.Left)
            ListView1.View = View.Details

            'Dgv_HslProduction.Columns(0).HeaderText = Base_Language.Lang_Global_Lokasi
            'Dgv_HslProduction.Columns(1).HeaderText = Base_Language.Lang_Global_Kode_Bahan
            'Dgv_HslProduction.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
            'Dgv_HslProduction.Columns(3).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
            'Dgv_HslProduction.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
            'Dgv_HslProduction.Columns(5).HeaderText = Base_Language.Lang_Global_Satuan

            'Dgv_Hasil_Production_Packaging.Columns(0).HeaderText = Base_Language.Lang_Global_Lokasi
            'Dgv_Hasil_Production_Packaging.Columns(1).HeaderText = Base_Language.Lang_Global_Kode_Bahan
            'Dgv_Hasil_Production_Packaging.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
            'Dgv_Hasil_Production_Packaging.Columns(3).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
            'Dgv_Hasil_Production_Packaging.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
            'Dgv_Hasil_Production_Packaging.Columns(5).HeaderText = Base_Language.Lang_Global_Satuan

            'Dgv_HslProduction.Rows.Clear()
            'Dgv_Hasil_Production_Packaging.Rows.Clear()
            'SQL = "select c.Kode_Stock_Owner,c.Kode_Barang,d.Nama,c.Jumlah,c.Persentase,e.Satuan from "
            'SQL = SQL & "Emi_Order_Produksi_Detail a,Emi_Transaksi_Formulator b,EMI_Transaksi_Formulator_Detail_Bahan c,Barang d,Barang_Detail_Satuan e "
            'SQL = SQL & "where a.No_Formula = b.No_Faktur and b.Status is null and b.No_Faktur = c.No_Faktur and a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Barang = e.Kode_barang and e.Flag_Tampil_Display = 'Y' and "
            'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox4.Text & "' and a.Urut = '" & fUrut & "' "

            'SQL = "select a.No_Transaksi, b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah,b.Satuan from  "
            'SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Bahan b, barang c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
            'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & TextBox4.Text & "' "
            'SQL = SQL & "order by c.nama"
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1
            '                Dgv_HslProduction.Rows.Add()
            '                Dgv_HslProduction.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
            '                Dgv_HslProduction.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")
            '                Dgv_HslProduction.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
            '                ' Dim nhasil As Double = 0
            '                ' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
            '                Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N2")
            '                Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
            '                Dgv_HslProduction.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")
            '            Next
            '        End If
            '    End With
            'End Using

            'SQL = "select a.No_Transaksi, b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah,b.Satuan from  "
            'SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
            'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & TextBox4.Text & "' "
            'SQL = SQL & "order by c.nama"
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1
            '                Dgv_Hasil_Production_Packaging.Rows.Add()
            '                Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
            '                Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")
            '                Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
            '                ' Dim nhasil As Double = 0
            '                ' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
            '                Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N2")
            '                Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
            '                Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")
            '            Next
            '        End If
            '    End With
            'End Using

            get_no_faktur()


            'Load Combo Lokasi
            Cmb_LokasiSimpan.Items.Clear() : arrSO.Clear()
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and Flag_Penyimpanan='Y' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_LokasiSimpan.Items.Add(dr("Keterangan")) : arrSO.Add(dr("kode_stock_owner"))
                    arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Get_data()
    End Sub

    Private Sub get_no_faktur()
        Dim FPro_Results As String = "PRS"
        TxtFormulator_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Production_Results", "No_Transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Transaksi, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub get_no_fakturTF()
        Dim FPro_Results As String = "TS-"
        Txt_NoFak_TF.Text = FPro_Results & arrInisialFaktur.Item(Cmb_LokasiSimpan.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("Tf_Stock", "kode_transfer", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(kode_transfer,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(Cmb_LokasiSimpan.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(Cmb_LokasiSimpan.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFormulator_NoFaktur.Focus() : Exit Sub
        ElseIf TxtHasilProduksi.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtHasilProduksi.Focus() : Exit Sub
        ElseIf TxtBadStock.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty2, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtBadStock.Focus() : Exit Sub
        ElseIf TxtGoodStock.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty3, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtGoodStock.Focus() : Exit Sub
            'ElseIf Dgv_HslProduction.CurrentRow.Cells(CellNilai_Produksi).Value = "0" Then
            '    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty4, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
        ElseIf Cmb_LokasiSimpan.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Tidak Boleh Kosong", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_LokasiSimpan.Focus() : Exit Sub
            'ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Keterangan Tidak Boleh Kosong", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Cmb_LokasiSimpan.Focus() : Exit Sub
        End If
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            'get_no_faktur()

            get_no_fakturTF()

            'set stock owner nya menjadi production

            Dim SoProduction As String

            SQL = "select Kode_Stock_Owner From Stock_Owner_Gudang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Produksi = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    SoProduction = Dr("kode_stock_owner")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim proses As Integer
            SQL = "select TOP(1) no_transaksi, "
            SQL = SQL & "isnull((select top(1) proses from Emi_Production_Results_Detail_barang x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = x.No_Transaksi order by proses desc "
            SQL = SQL & "),0) as proses "
            SQL = SQL & "from Emi_Production_Results a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Orderr = '" & TxtNoSplit.Text & "' order by proses desc "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtFormulator_NoFaktur.Text = Dr("no_transaksi")
                    proses = Dr("proses") + 1
                Else
                    Dr.Close()

                    get_no_faktur()
                    SQL = "INSERT INTO Emi_Production_Results(Kode_Perusahaan,No_Transaksi,No_Production_Order,Tanggal,Jam,UserID"
                    SQL = SQL & ",no_production_orderr) VALUES('" & KodePerusahaan & "',"
                    SQL = SQL & "'" & TxtFormulator_NoFaktur.Text & "','" & TxtNoSplit.Text.Trim & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "', '" & TxtNoSplit.Text.Trim & "')"
                    ExecuteTrans(SQL)
                    proses = 1
                End If
            End Using

            Dim kode_barang_scrap As String = ""
            SQL = "select kode_barang_Scrap from emi_binding_scrap where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and kode_barang = '" & LvKdBrg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kode_barang_scrap = Dr("kode_barang_scrap")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Kode Barang Scrap belum dibinding!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If TxtJmlScrap.Text = "" Then TxtJmlScrap.Text = "0"

            SQL = "insert into emi_production_results_detail_barang(kode_perusahaan,no_transaksi,proses,tanggal,jam,userid,kode_Stock_owner,"
            SQL = SQL & "kode_barang, qty_hasil_produksi, qty_good_stock, qty_bad_stock, satuan, qty_scrap, satuan_scrap, Kode_Barang_Scrap) values("
            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
            SQL = SQL & "'" & SoProduction & "', '" & TxtKodeBarang.Text & "','" & TxtHasilProduksi.Text & "', '" & TxtGoodStock.Text & "', "
            SQL = SQL & "'" & TxtBadStock.Text & "', '" & txtSatuanQty.Text & "', '" & TxtJmlScrap.Text & "', '" & txtSatuanScrap.Text & "' ,'" & kode_barang_scrap & "') "
            ExecuteTrans(SQL)


            'Get data Barang berdasarkan NoSplit
            Dim Kd_So As String = ""
            Dim Kd_Brg As String = ""
            SQL = "Select b.Status,b.Selesai,b.Kode_Stock_Owner,b.Kode_Barang "
            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b "
            SQL = SQL & "where a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & TxtNoSplit.Text & "'"
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



            Dim fbulan As String = Format(tgl_skg, "MM")
            Dim ftahun As String = Format(tgl_skg, "yyyy")

            Dim nilai_Produksi As Double = 0

            'SQL = "select isnull(SUM(b.Nilai_Per_Pcs),0) as nilai from Emi_Transaksi_Cost_Center a,Emi_Transaksi_Cost_Center_Detail b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & fbulan & "' and a.Tahun = '" & ftahun & "' "
            'SQL = SQL & "and  b.Kode_Barang = '" & Kd_Brg & "' "
            'Using dr = OpenTrans(SQL)
            '    If dr.Read Then
            '        nilai_Produksi = dr("nilai")
            '    End If
            'End Using

            'Dim nilai_Per_pcs_Bahan As Double = Math.Round(Nilai_Bahan / Val(HilangkanTanda(TextBox2.Text)), 2)
            'Dim nilai_Per_pcs_packaging As Double = Math.Round(Nilai_Packaging / Val(HilangkanTanda(TextBox2.Text)), 2)
            Dim nilai_HPP As Double = 0 'Math.Round(nilai_Produksi, 0)




            Dim TotalTf As Double = 0

#Region "INSERT PRODUKSI"


            '=============================
            '=    INSERT KE PRODUKSI     =
            '=============================

            ''GET DATA DI Emi_Produksi_Hasil_Perpallet
            SQL = "select a.No_Split, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal, a.Qr_Code, a.Jumlah, a.Satuan, a.Batch_Number, a.Kode_Barang, a.ID as urut_oto, a.jenis, a.Serial_Number, Tanggal, Tgl_Expired "
            SQL = SQL & "from Emi_Produksi_Hasil_Perpallet a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Split='" & TxtNoSplit.Text & "' and flag_simpan_pallet is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Warna_stock As String = ""

                            'Dim jenis_stockSN As String = ""
                            'If .Rows(i).Item("Jenis") = "good_stock" Then
                            '    jenis_stockSN = "jumlah"
                            'Else
                            '    jenis_stockSN = .Rows(i).Item("Jenis")
                            'End If


                            Warna_stock = .Rows(i).Item("Jenis")


                            '====================================
                            '=       CONVERT SATUAN KECIL       =
                            '====================================
                            Dim nilai_kecildetail As Double = Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), .Rows(i).Item("Satuan"), TxtSatKecilProduksi.Text, .Rows(i).Item("Jumlah"))


                            ''GET ID_WAREHOUSE YG KOSONG
                            Dim available_Id_Warehouse As String = ""
                            Dim available_NoPallet As String = ""

                            SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                            SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                            SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                            SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Kode_Stock_Owner='" & Kd_So & "' and b.Kode_Barang is null"
                            Using Dr2 = OpenTrans(SQL)
                                Do While Dr2.Read
                                    available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                                    available_NoPallet = Dr2("nomor_urut")
                                Loop
                            End Using


                            'Generate Sn Baru
                            Dim Rand As New Random
                            Dim str As String = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                            Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                            Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & nilai_HPP & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                            SQL = "Update barang set "
                            SQL = SQL & "Good_Stock = Good_Stock + " & nilai_kecildetail & " "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & Kd_Brg & "'"
                            ExecuteTrans(SQL)

                            'Cek Apakah Sn baru sama dengan Sn pada Barang Split
                            SQL = "select kode_barang from barang_sn where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and "
                            SQL = SQL & "kode_barang = '" & Kd_Brg & "' and serial_number = '" & SN & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi kesalahan pada Barang . . !, Silahkan Ulangi Transaksi . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                Else

                                    'Insert Barang SN
                                    SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                                    SQL = SQL & "serial_number, Jumlah, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
                                    SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
                                    SQL = SQL & "'" & Kd_So & "', '" & Kd_Brg & "', "
                                    SQL = SQL & "'" & SN & "', " & nilai_kecildetail & ", '" & Warna_stock & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', '" & .Rows(i).Item("Qr_Code") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "','" & .Rows(i).Item("Tanggal") & "','" & .Rows(i).Item("Tgl_Expired") & "')"
                                    Dr.Close()
                                    ExecuteTrans(SQL)
                                End If
                            End Using

                            SQL = "insert into Emi_Production_Results_Detail_Pallet (Kode_Perusahaan, No_Transaksi, Kode_Unik_Berjalan, Kode_Unik_Asal, Qr_Code, Jumlah, Satuan, NIlai_Barang, "
                            SQL = SQL & "Satuan_Barang, Batch_Number, Id_Warehouse, Nomor_Pallet, proses, serial_number, Jenis, Tgl_Produksi, Tgl_Expired) values "
                            SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', '" & nilai_kecildetail & "', '" & TxtSatKecilProduksi.Text & "', "
                            SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', '" & proses & "', '" & SN & "', '" & .Rows(i).Item("Jenis") & "','" & .Rows(i).Item("Tanggal") & "','" & .Rows(i).Item("Tgl_Expired") & "') "
                            ExecuteTrans(SQL)


                            TotalTf = TotalTf + nilai_kecildetail

                        Next

                        '===================================
                        '=    CEK APAKAH JUMLAH SESUAI     =
                        '===================================
                        SQL = "SELECT "
                        SQL = SQL & "ROUND(SUM(good_stock), 2) AS good_stock, "
                        SQL = SQL & "ISNULL((SELECT ROUND(SUM(jumlah), 2) FROM Barang_sn x WHERE a.kode_Barang = x.kode_Barang AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.kode_Perusahaan = x.kode_Perusahaan), 0) AS Jumlah_sn, "
                        SQL = SQL & "ISNULL(ROUND(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                        SQL = SQL & "ISNULL((SELECT ROUND(SUM(Jumlah_Bags), 2) FROM Barang_sn y WHERE a.kode_Barang = y.kode_Barang AND a.Kode_Stock_Owner = y.Kode_Stock_Owner AND a.kode_Perusahaan = y.Kode_Perusahaan), 0) AS jumlah_bags_sn "
                        SQL = SQL & "FROM "
                        SQL = SQL & "barang a "
                        SQL = SQL & "WHERE "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' And a.Kode_Stock_Owner = '" & Kd_So & "' AND a.Kode_Barang = '" & Kd_Brg & "' "
                        SQL = SQL & "GROUP BY "
                        SQL = SQL & "a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan"
                        Using Ds2 = BindingTrans(SQL)
                            If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                Dim Stock_Barang As String = Ds2.Tables("MyTable").Rows(0).Item("good_stock")
                                Dim Stock_Sn As String = Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn")
                                Dim Bags_Barang As String = Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang")
                                Dim Bags_Sn As String = Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn")

                                If Stock_Barang <> Stock_Sn Or Bags_Barang <> Bags_Sn Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub

                            End If

                        End Using

                    End If
                End With
            End Using

#End Region


            '===================================
            '=    INSERT KE TRANSFER STOCK     =
            '===================================
            Dim TotalKecil As Double = Ubah_Angka_Kecil(Kd_Brg, txtSatuanQty.Text, TxtSatKecilProduksi.Text, TotalTf)
            SQL = "insert into Tf_Stock (Kode_Perusahaan, Kode_Transfer, SO_Awal, SO_Tujuan ,Kode_Barang, "
            SQL = SQL & "Tanggal, Jam, UserID, Jenis_Transfer, Total, Satuan, Keterangan, Total_Barang, Satuan_Barang, Total_Transfer_Bags) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFak_TF.Text & "', '" & Kd_So & "', '" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "', '" & Kd_Brg & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', 'Antar Gudang', '" & TotalTf & "', '" & txtSatuanQty.Text & "', "
            SQL = SQL & "'" & Txt_Keterangan.Text & "', '" & TotalKecil & "', '" & TxtSatKecilProduksi.Text & "', '0')"
            ExecuteTrans(SQL)


            '==============================================================
            '=    GET Emi_Production_Results_Detail_Pallet SEBELUMNYA     =
            '==============================================================
            SQL = "select a.No_Transaksi, a.Proses, a.Jumlah, a.NIlai_Barang, a.Satuan, a.Satuan_Barang, a.Id_Warehouse, a.Serial_Number, a.Jenis, a.Nomor_Pallet, a.Proses, a.urut_oto, Kode_Unik_Berjalan, Kode_Unik_Asal, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired "
            SQL = SQL & "from Emi_Production_Results_Detail_Pallet a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi ='" & TxtFormulator_NoFaktur.Text & "' "
            SQL = SQL & "and a.Proses = '" & proses & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim SnLama As String = .Rows(i).Item("Serial_Number")
                            Dim WarnaLama As String = .Rows(i).Item("Jenis")
                            Dim HargaHpp2 As String = Get_Harga_SN(SnLama)

                            'Generate Sn Baru
                            Dim Rand2 As New Random
                            Dim sts2 As String = Format(Rand2.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                            Dim Kode_Unik2 As String = sts2.Substring(0, 5) & Chr(64 + sts2.Substring(6, 1)) & sts2.Substring(6, Len(sts2) - 6)
                            Dim SN2 As String = Kode_Unik2 & Tanda_SN & "01" & Tanda_SN & HargaHpp2 & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                            '===============================
                            '=    POTONG SN SEBELUMNYA     =
                            '===============================
                            SQL = "update Barang_SN set Jumlah = Jumlah - " & .Rows(i).Item("NIlai_Barang") & " where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SnLama & "' "
                            SQL = SQL & "and Warna = '" & WarnaLama & "'"
                            ExecuteTrans(SQL)

                            SQL = "Update barang set "
                            SQL = SQL & "Good_Stock = Good_Stock - " & .Rows(i).Item("NIlai_Barang") & " "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & Kd_Brg & "'"
                            ExecuteTrans(SQL)

                            SQL = "SELECT "
                            SQL = SQL & "ROUND(SUM(good_stock), 2) AS good_stock, "
                            SQL = SQL & "ISNULL((SELECT ROUND(SUM(jumlah), 2) FROM Barang_sn x WHERE a.kode_Barang = x.kode_Barang AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.kode_Perusahaan = x.kode_Perusahaan), 0) AS Jumlah_sn, "
                            SQL = SQL & "ISNULL(ROUND(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "ISNULL((SELECT ROUND(SUM(Jumlah_Bags), 2) FROM Barang_sn y WHERE a.kode_Barang = y.kode_Barang AND a.Kode_Stock_Owner = y.Kode_Stock_Owner AND a.kode_Perusahaan = y.Kode_Perusahaan), 0) AS jumlah_bags_sn "
                            SQL = SQL & "FROM "
                            SQL = SQL & "barang a "
                            SQL = SQL & "WHERE "
                            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' And a.Kode_Stock_Owner = '" & Kd_So & "' AND a.Kode_Barang = '" & Kd_Brg & "' "
                            SQL = SQL & "GROUP BY "
                            SQL = SQL & "a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan"
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                    Dim Stock_Barang As String = Ds2.Tables("MyTable").Rows(0).Item("good_stock")
                                    Dim Stock_Sn As String = Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn")
                                    Dim Bags_Barang As String = Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang")
                                    Dim Bags_Sn As String = Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn")

                                    If Stock_Barang <> Stock_Sn Or Bags_Barang <> Bags_Sn Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub

                                End If

                            End Using

                            '=========================
                            '=    CEK SN HARUS 0     =
                            '=========================
                            SQL = "select Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SnLama & "' and Warna = '" & WarnaLama & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(Dr("Jumlah")) > 0 Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                            End Using

                            SQL = "update Emi_Production_Results_Detail_Pallet set SN_Baru ='" & SN2 & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and urut_oto = '" & .Rows(i).Item("urut_oto") & "'"
                            ExecuteTrans(SQL)

                            '===============================
                            '=    GET WAREHOUSE KOSONG     =
                            '===============================
                            Dim available_Id_Warehouse2 As String = ""
                            Dim available_NoPallet2 As String = ""

                            SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                            SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                            SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                            SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "' and b.Kode_Barang is null"
                            Using Dr2 = OpenTrans(SQL)
                                If Dr2.Read Then
                                    available_Id_Warehouse2 = Dr2("id_wms_warehouse_position")
                                    available_NoPallet2 = Dr2("nomor_urut")
                                Else
                                    Dr2.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Rak sudah Penuh")
                                    Exit Sub
                                End If
                            End Using

                            SQL = "insert into barang_sn_sementara(kode_perusahaan, kode_stock_owner, kode_barang, "
                            SQL = SQL & "serial_number, Jumlah, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
                            SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Flag_Produksi, Flag_QI, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
                            SQL = SQL & "'" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "', '" & Kd_Brg & "', "
                            SQL = SQL & "'" & SN2 & "', " & .Rows(i).Item("NIlai_Barang") & ", '" & WarnaLama & "', "
                            SQL = SQL & "'" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', '" & .Rows(i).Item("Qr_Code") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse2 & "', '" & available_NoPallet2 & "', 'Y', 'Y', '" & .Rows(i).Item("Tgl_Produksi") & "', '" & .Rows(i).Item("Tgl_Expired") & "')"

                            ExecuteTrans(SQL)

                            '=======================================
                            '=    INSERT KE TRANSFER STOCK DET     =
                            '=======================================
                            SQL = "insert into Tf_Stock_det (Kode_Perusahaan, No_Faktur, Serial_Number_Awal, Serial_Number_Akhir, "
                            SQL = SQL & "Id_Wms_Awal, Id_Wms_Tujuan, Jumlah, Jumlah_Bags, Satuan ,No_Pallet_Awal, No_Pallet_Tujuan, Warna) "
                            SQL = SQL & "values "
                            SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(Txt_NoFak_TF.Text) & "', "
                            SQL = SQL & "'" & SnLama & "', '" & SN2 & "', '" & .Rows(i).Item("Id_Warehouse") & "', '" & available_Id_Warehouse2 & "', '" & .Rows(i).Item("NIlai_Barang") & "', "
                            SQL = SQL & "'0', '" & .Rows(i).Item("Satuan_Barang") & "', '" & .Rows(i).Item("Nomor_Pallet") & "', '" & available_NoPallet2 & "', '" & WarnaLama & "') "
                            ExecuteTrans(SQL)

                        Next
                    End If
                End With
            End Using



            If Val(TxtJmlScrap.Text) <> 0 Then
                Dim nilai_kecildetail As Double = 0
                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa', '" & kode_barang_scrap & "', '" & txtSatuanScrap.Text & "', "
                SQL = SQL & "'" & TxtSatKecilScrap.Text & " ', '" & TxtJmlScrap.Text & "' ) as hasil "
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

                Dim available_Id_Warehouse As String = ""
                Dim available_NoPallet As String = ""

                SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner='" & Kd_So & "' and b.Kode_Barang is null"
                Using Dr2 = OpenTrans(SQL)
                    Do While Dr2.Read
                        available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                        available_NoPallet = Dr2("nomor_urut")
                    Loop
                End Using

                Dim Rand As New Random
                Dim str As String = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & nilai_HPP & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")


                SQL = "Update barang set "
                SQL = SQL & "good_stock = good_stock + " & nilai_kecildetail & " "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & kode_barang_scrap & "'"
                ExecuteTrans(SQL)

                SQL = "select kode_barang from barang_sn where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and "
                SQL = SQL & "kode_barang = '" & kode_barang_scrap & "' and serial_number = '" & SN & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi kesalahan pada Barang . . !, Silahkan Ulangi Transaksi . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                        SQL = SQL & "serial_number, jumlah, Kode_Unik_Berjalan, Kode_Unik_Asal, "
                        SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet) values('" & KodePerusahaan & "', "
                        SQL = SQL & "'" & Kd_So & "', '" & kode_barang_scrap & "', "
                        SQL = SQL & "'" & SN & "', " & nilai_kecildetail & ", '" & "X" & "', "
                        SQL = SQL & "'" & "X" & "', '" & "X" & "', "
                        SQL = SQL & "'" & "X" & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "')"
                        Dr.Close()
                        ExecuteTrans(SQL)
                    End If
                End Using

                SQL = "insert into Emi_Production_Results_Detail_Scrap (Kode_Perusahaan, No_Transaksi, Kode_Unik_Berjalan, Kode_Unik_Asal, Qr_Code, Jumlah, Satuan, NIlai_Barang, "
                SQL = SQL & "Satuan_Barang, Batch_Number, Id_Warehouse, Nomor_Pallet,proses, serial_number) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & "X" & "', '" & "X" & "', "
                SQL = SQL & "'" & "X" & "', '" & TxtJmlScrap.Text & "', '" & txtSatuanScrap.Text & "', '" & nilai_kecildetail & "', '" & TxtSatKecilScrap.Text & "', "
                SQL = SQL & "'" & "X" & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', '" & proses & "', '" & SN & "') "
                ExecuteTrans(SQL)
            End If


            SQL = "update Emi_Produksi_Hasil_Perpallet set flag_simpan_pallet = 'Y'  where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Split = '" & TxtNoSplit.Text & "' "
            ExecuteTrans(SQL)


            'buka lagi dari bawah

            'SQL = "update Emi_Split_Production_Order set Flag_Hasil_Produksi = 'Y',Tgl_Hasil_Produksi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            'SQL = SQL & "Jam_Hasil_Produksi = '" & Format(tgl_skg, "HH:mm:ss") & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and No_Transaksi = '" & TextBox4.Text & "' "
            'ExecuteTrans(SQL)

            'SQL = "select b.Jumlah as jml_po,"
            'SQL = SQL & "ISNULL((select SUM(a.Jumlah) from Emi_Split_Production_Order a where "
            'SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur "
            'SQL = SQL & "and a.Flag_Produksi = 'Y' and a.Flag_Selesai_Produksi = 'Y' "
            'SQL = SQL & "and a.Flag_Hasil_Produksi ='Y'),0) as jml_split "
            'SQL = SQL & "from EMI_Order_Produksi b where b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and b.No_Faktur = '" & fno_po & "' "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("jml_po") = Dr("jml_split") Then
            '            Dr.Close()
            '            SQL = "Update EMI_Order_Produksi set Selesai = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
            '            SQL = SQL & "and No_Faktur = '" & fno_po & "' "
            '            ExecuteTrans(SQL)
            '        End If
            '    End If
            'End Using

            'akhir tutup

            Cmd.Transaction.Commit()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_Display_Hasil_ProduksiFG.Button1_Click(Btn_Simpan, e)
        Me.Close()
    End Sub

    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    'Private Sub Dgv_HslProduction_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
    '    Get_Isi_Listview(Dgv_HslProduction.CurrentRow.Index)
    '    If IsNumeric(LvNilai_Produksi) = False Or Val(LvNilai_Produksi) < 0 Then
    '        Dgv_HslProduction.CurrentRow.Cells(CellNilai_Produksi).Value = 0
    '    End If
    'End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TxtBadStock.TextChanged
        If TxtHasilProduksi.Text.Trim.Length = 0 Then
            Exit Sub

        End If
        Dim a As Double = 0
        a = Val(HilangkanTanda(TxtHasilProduksi.Text)) - Val(HilangkanTanda(TxtBadStock.Text))


        If TxtBadStock.Text.Trim.Length = 0 Then
            TxtGoodStock.Text = ""
        Else
            TxtGoodStock.Text = Format(a, "N2")
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtHasilProduksi.KeyPress
        If e.KeyChar = Chr(13) Then TxtBadStock.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    'Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
    '    If e.KeyChar = Chr(13) Then Dgv_HslProduction.Focus()
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    'End Sub

    Private Function Ubah_Angka_Kecil(ByVal kodeBarang As String, ByVal satuanBesar As String, ByVal satuanKecil As String, ByVal jumlahConvert As String) As Double

        Dim total_kecil As Double = 0
        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & kodeBarang & "', '" & satuanBesar & "',"
        SQL = SQL & "'" & satuanKecil & "', '" & HilangkanTanda(jumlahConvert) & "' ) as hasil"
        Using Dr1 = OpenTrans(SQL)
            If Dr1.Read Then
                If General_Class.CekNULL(Dr1("hasil")) = "" Then
                    Dr1.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                End If

                total_kecil = Dr1("hasil")
            Else
                Dr1.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("data konversi satuan kirim tidak ada ")
            End If
        End Using

        Return total_kecil
    End Function

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtBadStock.KeyPress
        If e.KeyChar = Chr(13) Then
            TxtJmlScrap.Focus()
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub BtnFormulator_Refresh_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Refresh.Click

    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJmlScrap.KeyPress
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub


End Class
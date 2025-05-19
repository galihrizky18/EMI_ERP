Public Class EMI_PO_Pembelian_Display_Sub_Barang_Lain
    Dim arrcariLocal, arrcariImport As New ArrayList
    Dim Jenis = "Lokasi_PO"
    Dim LvNo_PoLocal As String
    Dim LvLokasiLocal As String
    Dim LvTanggalLocal As String
    Dim LvKd_SupplierLocal As String
    Dim LvNm_SupplierLocal As String
    Dim LvLokasiGudangLocal As String
    Dim LvKeteranganLocal As String
    Dim LvIDLocal As String

    Dim CellNo_PoLocal As Integer = 0
    Dim CellLokasiLocal As Integer = 1
    Dim CellTanggalLocal As Integer = 2
    Dim CellKd_SupplierLocal As Integer = 3
    Dim CellNm_SupplierLocal As Integer = 4
    Dim CellLokasiGudangLocal As Integer = 5
    Dim CellKeteranganLocal As Integer = 6
    Dim CellIDLocal As Integer = 7

    Dim LvNo_PoImport As String
    Dim LvLokasiImport As String
    Dim LvTanggalImport As String
    Dim LvKd_SupplierImport As String
    Dim LvNm_SupplierImport As String
    Dim LvLokasiGudangImport As String
    Dim LvKeteranganImport As String
    Dim LvIDImport As String

    Dim CellNo_PoImport As Integer = 0
    Dim CellLokasiImport As Integer = 1
    Dim CellTanggalImport As Integer = 2
    Dim CellKd_SupplierImport As Integer = 3
    Dim CellNm_SupplierImport As Integer = 4
    Dim CellLokasiGudangImport As Integer = 5
    Dim CellKeteranganImport As Integer = 6
    Dim CellIDImport As Integer = 7


    Dim ID_Prepare As String = "0"
    Dim ID_PO As String = "1"
    Dim ID_ETD As String = "2"
    Dim ID_ETA As String = "3"
    Dim ID_Timbang As String = "4"

    Public filter_tambahan As String
    Public Property asal As String

    Private Sub Get_Isi_ListviewLocal(ByVal No_Index As Integer)
        LvNo_PoLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellNo_PoLocal).Value
        LvLokasiLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellLokasiLocal).Value
        LvTanggalLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellTanggalLocal).Value
        LvKd_SupplierLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellKd_SupplierLocal).Value
        LvNm_SupplierLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellNm_SupplierLocal).Value
        LvLokasiGudangLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellLokasiGudangLocal).Value
        LvKeteranganLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellKeteranganLocal).Value
        LvIDLocal = DgvPO_DataLocal.Rows(No_Index).Cells(CellIDLocal).Value
    End Sub

    Private Sub Get_Isi_ListviewImport(ByVal No_Index As Integer)
        LvNo_PoImport = DgvPO_DataImport.Rows(No_Index).Cells(CellNo_PoImport).Value
        LvLokasiImport = DgvPO_DataImport.Rows(No_Index).Cells(CellLokasiImport).Value
        LvTanggalImport = DgvPO_DataImport.Rows(No_Index).Cells(CellTanggalImport).Value
        LvKd_SupplierImport = DgvPO_DataImport.Rows(No_Index).Cells(CellKd_SupplierImport).Value
        LvNm_SupplierImport = DgvPO_DataImport.Rows(No_Index).Cells(CellNm_SupplierImport).Value
        LvLokasiGudangImport = DgvPO_DataImport.Rows(No_Index).Cells(CellLokasiGudangImport).Value
        LvKeteranganImport = DgvPO_DataImport.Rows(No_Index).Cells(CellKeteranganImport).Value
        LvIDImport = DgvPO_DataImport.Rows(No_Index).Cells(CellIDImport).Value
    End Sub
    Private Sub SD_Pilih_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try

            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Label1.Text = Base_Language.Lang_Lokasi_PO_Judul_Display
            Label4.Text = Base_Language.Lang_Lokasi_PO_Kolom

            DgvPO_DataLocal.Columns(CellNo_PoLocal).HeaderText = Base_Language.Lang_Global_NoFaktur
            DgvPO_DataLocal.Columns(CellLokasiLocal).HeaderText = Base_Language.Lang_Global_Lokasi
            DgvPO_DataLocal.Columns(CellTanggalLocal).HeaderText = Base_Language.Lang_Global_Tanggal
            DgvPO_DataLocal.Columns(CellKd_SupplierLocal).HeaderText = Base_Language.Lang_Global_Kode_Supplier
            DgvPO_DataLocal.Columns(CellNm_SupplierLocal).HeaderText = Base_Language.Lang_Global_Supplier
            DgvPO_DataLocal.Columns(CellLokasiGudangLocal).HeaderText = Base_Language.Lang_Global_LokasiGudang
            DgvPO_DataLocal.Columns(CellKeteranganLocal).HeaderText = Base_Language.lang_global_keterangan

            DgvPO_DataImport.Columns(CellNo_PoImport).HeaderText = Base_Language.Lang_Global_NoFaktur
            DgvPO_DataImport.Columns(CellLokasiImport).HeaderText = Base_Language.Lang_Global_Lokasi
            DgvPO_DataImport.Columns(CellTanggalImport).HeaderText = Base_Language.Lang_Global_Tanggal
            DgvPO_DataImport.Columns(CellKd_SupplierImport).HeaderText = Base_Language.Lang_Global_Kode_Supplier
            DgvPO_DataImport.Columns(CellNm_SupplierImport).HeaderText = Base_Language.Lang_Global_Supplier
            DgvPO_DataImport.Columns(CellLokasiGudangImport).HeaderText = Base_Language.Lang_Global_LokasiGudang
            DgvPO_DataImport.Columns(CellKeteranganImport).HeaderText = Base_Language.lang_global_keterangan

            ComboBox1.Items.Clear() : arrcariLocal.Clear() : arrcariImport.Clear()
            ComboBox1.Items.Add(Base_Language.Lang_Global_NoFaktur) : arrcariLocal.Add("a.No_Faktur") : arrcariImport.Add("ro.id_rencana")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Lokasi) : arrcariLocal.Add("a.Lokasi") : arrcariImport.Add("ro.Lokasi")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Kode_Supplier) : arrcariLocal.Add("c.Kode_Supplier") : arrcariImport.Add("s.Kode_Supplier")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Supplier) : arrcariLocal.Add("c.Nama") : arrcariImport.Add("s.Nama")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cari("Y")
    End Sub

    Private Sub SD_Pilih_PO_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Public Sub Cari(ByVal semua As String)
        Try
            OpenConn()


            DgvPO_DataLocal.Rows.Clear()
            DgvPO_DataImport.Rows.Clear()


            Dim ind As Integer = 0
            'SQL = "select a.No_Faktur,a.Lokasi, a.Tanggal,c.Kode_Supplier,c.Nama, NULL as ETA from "
            'SQL = SQL & "EMI_Prepare_Bahan_Baku a, EMI_Prepare_Bahan_Baku_Det_Order b, Suppliers c, EMI_Master_Penawaran_Barang_Lain d, Suppliers_Kategori e "
            'SQL = SQL & "where a.Kode_Perusahaan =b.Kode_Perusahaan and a.No_Faktur=b.No_Faktur and a.Status is null and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            'SQL = SQL & "and b.Kode_Perusahaan =d.Kode_Perusahaan and b.No_Penawaran=d.No_Penawaran and c.Kode_Perusahaan=d.Kode_Perusahaan "
            'SQL = SQL & "and c.Kode_Supplier=d.Kode_Supplier and Flag_Selesai is null and c.ID_Kategori_Suppliers= e.ID_Kategori_Suppliers and e.Flag_Jenis_Lokal='Y' "
            'If semua = "T" Then
            '    SQL = SQL & " and " & arrcariLocal.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
            'Else
            '    SQL = SQL & " "
            'End If
            'SQL = SQL & " group by a.No_Faktur,a.Lokasi, a.Tanggal,c.Kode_Supplier,c.Nama "
            'SQL = SQL & "order by no_faktur "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read

            '        DgvPO_DataLocal.Rows.Add(1)


            '        DgvPO_DataLocal.Rows(ind).Cells(CellNo_PoLocal).Value = dr("No_Faktur")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellLokasiLocal).Value = dr("Lokasi")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellTanggalLocal).Value = Format(dr("Tanggal"), "dd MMMM yyyy")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellKd_SupplierLocal).Value = dr("Kode_Supplier")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellNm_SupplierLocal).Value = dr("Nama")
            '        DgvPO_DataLocal.Rows(ind).Cells(CellLokasiGudangLocal).Value = "" 'dr("Kode_Stock_Owner")

            '        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : On Process"
            '        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.Coral

            '        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_Prepare
            '        ind += 1
            '    Loop
            'End Using



            SQL = "select a.No_Faktur, a.Lokasi, a.Tanggal, c.Kode_Supplier, c.Nama, 1 as ID, ETD, Flag_ETD, Flag_Release "

            SQL = SQL & ",isnull((select top(1) 'T' from EMI_Pembelian_PO_Detail_Barang_Lain x "
            SQL = SQL & "where x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Faktur=a.no_Faktur and x.Flag_loading is null),'Y') as Selesai_ETA "

            SQL = SQL & ",isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x, EMI_Pembelian_Loading y "
            SQL = SQL & "where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.No_Faktur and y.status is null "
            SQL = SQL & "and x.Kode_Perusahaan=a.Kode_Perusahaan and x.no_PO=a.no_Faktur),null) as Flag_ETA "

            SQL = SQL & ",isnull((select top(1) ETA from EMI_Pembelian_Loading_detail x, EMI_Pembelian_Loading y "
            SQL = SQL & "where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.No_Faktur and y.status is null "
            SQL = SQL & "and x.Kode_Perusahaan=a.Kode_Perusahaan and x.no_PO=a.no_Faktur),null) as ETA "

            'SQL = SQL & ",isnull((select top(1) flag_timbang from EMI_Pembelian_ETA_detail_PO x, EMI_Pembelian_ETA y "
            'SQL = SQL & "where x.Kode_Perusahaan=y.Kode_Perusahaan and x.NO_SJ=y.No_SJ and y.status is null "
            'SQL = SQL & "and x.Kode_Perusahaan=a.Kode_Perusahaan and x.no_PO=a.no_Faktur),null) as Flag_Timbangan_Unloading "

            SQL = SQL & "from EMI_Pembelian_PO_Barang_Lain a, Suppliers c, Suppliers_Kategori d where Selesai is null and Status is null and "
            SQL = SQL & "a.Kode_Perusahaan=c.Kode_Perusahaan and a.Kode_Supplier=c.Kode_Supplier and a.Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "c.ID_Kategori_Suppliers=d.ID_Kategori_Suppliers and (d.Flag_Jenis_Lokal='Y' or(d.Flag_Jenis_import='Y' and a.Flag_Release is null)) and a.flag_pembelian is null "
            If semua = "T" Then
                SQL = SQL & " and " & arrcariLocal.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
            Else
                SQL = SQL & " "
            End If
            SQL = SQL & "order by no_faktur "
            Using dr = OpenTrans(SQL)
                Do While dr.Read

                    DgvPO_DataLocal.Rows.Add(1)


                    DgvPO_DataLocal.Rows(ind).Cells(CellNo_PoLocal).Value = dr("No_Faktur")
                    DgvPO_DataLocal.Rows(ind).Cells(CellLokasiLocal).Value = dr("Lokasi")
                    DgvPO_DataLocal.Rows(ind).Cells(CellTanggalLocal).Value = Format(dr("Tanggal"), "dd MMMM yyyy")
                    DgvPO_DataLocal.Rows(ind).Cells(CellKd_SupplierLocal).Value = dr("Kode_Supplier")
                    DgvPO_DataLocal.Rows(ind).Cells(CellNm_SupplierLocal).Value = dr("Nama")
                    DgvPO_DataLocal.Rows(ind).Cells(CellLokasiGudangLocal).Value = "" 'dr("Kode_Stock_Owner")

                    If General_Class.CekNULL(dr("Flag_Release")) = "" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : Submit  Tanggal : " & Format(dr("Tanggal"), "dd MMMM yyyy")
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_PO
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGoldenrodYellow
                    ElseIf General_Class.CekNULL(dr("Flag_ETD")) = "" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : Realease  Tanggal : " & Format(dr("Tanggal"), "dd MMMM yyyy")
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_PO
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGoldenrodYellow
                    ElseIf General_Class.CekNULL(dr("Flag_ETA")) = "" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : Persiapan Pengiriman  Tanggal : " & Format(dr("ETD"), "dd MMMM yyyy")
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_ETD
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGoldenrodYellow
                    ElseIf General_Class.CekNULL(dr("Flag_ETA")) = "Y" Then
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "PO" & Environment.NewLine & "Status : On The Way  Tanggal : " & Format(dr("ETA"), "dd MMMM yyyy")
                        DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_ETA
                        DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGoldenrodYellow
                        'ElseIf General_Class.CekNULL(dr("Flag_Timbangan_Unloading")) = "Y" Then
                        '    DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Value = "Barang Masuk" & Environment.NewLine & "Status : Timbang " '  Tanggal : " & Format(dr("ETA"), "dd MMMM yyyy")
                        '    DgvPO_DataLocal.Rows(ind).Cells(CellIDLocal).Value = ID_Timbang
                        '    DgvPO_DataLocal.Rows(ind).Cells(CellKeteranganLocal).Style.BackColor = Color.LightGreen
                    End If


                    ind += 1
                Loop
            End Using
            '---------------------------------------------------------------------------------------------------------
            Dim ind2 As Integer = 0
            SQL = "SELECT ro.id_rencana, ro.lokasi, ro.kode_supplier, s.nama nama_supplier, "
            SQL = SQL & "ro.no_po, ro.tanggal_po, ro.userid, CAST(ro.rv AS BIGINT) rv, "
            SQL = SQL & "ro.kode_kontainer, ro.total_persen, ro.no_po_pembelian, ro.status, "
            SQL = SQL & "ro.selesai,ro.Flag_Submit_PO,ro.Flag_Loading_Barang,ro.Flag_OTW,"
            SQL = SQL & "ro.Flag_Draft,ro.Flag_Final,ro.Flag_Kirim,ro.Flag_Finish,"
            SQL = SQL & "ro.Flag_SPPB,ro.Flag_Penjaluran,ro.Flag_Kapal_Tiba,ro.Flag_Tarik_Kontainer,"
            SQL = SQL & "ro.Flag_Bongkar,ro.Flag_Sudah_Transaksi,ro.Flag_Sudah_Transaksi3,"
            SQL = SQL & "ro.Flag_Lokasi_Tujuan,ro.Flag_Billing,ro.Flag_HPP,"
            SQL = SQL & "ro.Flag_Barang_Masuk,ro.Flag_Pembelian "
            SQL = SQL & "FROM rencana_order ro, suppliers s, EMI_Pembelian_PO_Barang_Lain c "
            SQL = SQL & "WHERE ro.kode_perusahaan = s.kode_perusahaan AND "
            SQL = SQL & "ro.kode_Perusahaan=c.kode_Perusahaan and ro.no_Po=c.no_faktur and c.flag_Pembelian is null and "
            SQL = SQL & "ro.kode_supplier = s.kode_supplier AND ro.kode_perusahaan = '" & KodePerusahaan & "' and ro.status is null and ro.selesai is null  "
            If semua = "T" Then
                SQL = SQL & " and " & arrcariImport.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcariImport.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & " "
            End If
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index = 0 To .Rows.Count - 1

                        Dim Penjaluran As String = ""
                        SQL = "select count(kode_perusahaan) as count from penjaluran_import where id_rencana ='" & .Rows(index).Item("id_rencana") & "' and kode_perusahaan = '" & KodePerusahaan & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                If Dr("count") > 0 Then
                                    Penjaluran = "Y"
                                Else
                                    Penjaluran = ""
                                End If
                            End If
                            Dr.Close()
                        End Using

                        SQL = ""
                        Dim warna As String = ""
                        If General_Class.CekNULL(.Rows(index).Item("Flag_Submit_PO")) = "" Then
                            SQL = " SELECT ('Status : On Process ' ) as keterangan, 'PO' as status"
                            SQL = SQL & " FROM rencana_order ro WHERE "
                            SQL = SQL & " ro.kode_perusahaan = '" & KodePerusahaan & "' AND ro.id_rencana = '" & .Rows(index).Item("id_rencana") & "'  "

                            warna = "RED"

                        End If

                        If General_Class.CekNULL(.Rows(index).Item("Flag_Submit_PO")) = "Y" Or General_Class.CekNULL(.Rows(index).Item("Flag_Loading_Barang")) = "Y" Then
                            SQL = " SELECT ('Status : Submit  Tgl Submit : '+ format(bi.tanggal,'dd MMM yyyy') ) as keterangan, 'PO' as status"
                            SQL = SQL & " FROM submit_PO bi, rencana_order ro WHERE bi.id_rencana = ro.id_rencana and bi.status is null and "
                            SQL = SQL & " ro.kode_perusahaan = '" & KodePerusahaan & "' AND ro.id_rencana = '" & .Rows(index).Item("id_rencana") & "'  "

                            warna = "RED"
                        End If

                        If General_Class.CekNULL(.Rows(index).Item("Flag_OTW")) = "Y" Then
                            SQL = " select ('ETD : '+format(b.etd,'dd MMM yyyy')+' | ETA :'+format(b.eta,'dd MMM yyyy') ) as keterangan,  "
                            SQL = SQL & " 'OTW' as status "
                            SQL = SQL & " from rencana_order a, ubah_status_otw b  "
                            SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.id_rencana = b.id_rencana and a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & .Rows(index).Item("id_rencana") & "'  "

                            warna = "YELLOW"
                        End If

                        If General_Class.CekNULL(.Rows(index).Item("Flag_Draft")) = "Y" Then
                            SQL = " SELECT ('Status : ( '+ case when b.BL = 'Y' and b.Flag_Final is null and b.Kirim_Jam is null and b.finish_jam is null  then 'Draft' Else '' End +"
                            SQL = SQL & " case when b.BL = 'Y' and b.Flag_Final is not null and b.Kirim_Jam is null and b.finish_jam is null  then 'Final' Else '' End +"
                            SQL = SQL & " case when b.BL = 'Y' and b.Flag_Final is not null and b.Kirim_Jam is not null and b.finish_jam is null  then ' Kirim' Else '' End +"
                            SQL = SQL & " case when  b.BL = 'Y' and b.Flag_Final is not null and b.Kirim_Jam is not null and b.finish_jam is not null  then ' Terkirim' Else '' End +'  )') AS keterangan,"
                            SQL = SQL & " 'Tracking Dokumen' AS status"
                            SQL = SQL & " from Rencana_Order a, Tracking_Dokumen b  "
                            SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.id_rencana = b.id_rencana and a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & .Rows(index).Item("id_rencana") & "'  "

                            warna = "YELLOW"
                        End If

                        If General_Class.CekNULL(.Rows(index).Item("Flag_Kapal_Tiba")) = "Y" Then
                            SQL = " SELECT ('Tgl Tiba : '+ format(kt.tanggal_tiba,'dd MMM yyyy') )as keterangan, 'Kapal Tiba' as status"
                            SQL = SQL & " FROM kapal_tiba_import kt, rencana_order ro , pelabuhan p "
                            SQL = SQL & " WHERE kt.id_rencana = ro.id_rencana and kt.kode_pelabuhan = p.kode_pelabuhan and ro.kode_perusahaan = '" & KodePerusahaan & "'"
                            SQL = SQL & " AND ro.id_rencana = '" & .Rows(index).Item("id_rencana") & "' "

                            warna = "YELLOW"
                        End If

                        If Penjaluran = "Y" Then
                            SQL = "  select ('Status Warna : '+ b.warna) as keterangan"
                            SQL = SQL & " , 'Penjaluran' as status from Rencana_Order a,penjaluran_import b  "
                            SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.id_rencana = b.id_rencana and "
                            SQL = SQL & " a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & .Rows(index).Item("id_rencana") & "'  "

                            warna = "YELLOW"
                        End If

                        If General_Class.CekNULL(.Rows(index).Item("Flag_SPPB")) = "Y" Then
                            SQL = " SELECT ('Status : ( '+ case when b.ok <> 'T' then 'OK | Tanggal : ' + format(b.Tanggal_Ok,'dd MMM yyyy' ) Else '' End +"
                            SQL = SQL & " case when b.nhi <> '' then '| NHI | Tanggal : ' + format(b.Tanggal_NHI,'dd MMM yyyy' )  Else '' End +"
                            SQL = SQL & " case when b.hico <> '' then ' | HICO - Tanggal : ' + format(b.Tanggal_HICO,'dd MMM yyyy' )  Else '' End +"
                            SQL = SQL & " ' )'  "
                            SQL = SQL & " ) as Keterangan , 'SPPB' as status from  rencana_order a ,sppb_import b "
                            SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.id_rencana = b.id_rencana and "
                            SQL = SQL & " a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Rencana = '" & .Rows(index).Item("id_rencana") & "' "

                            warna = "YELLOW"
                        End If

                        If General_Class.CekNULL(.Rows(index).Item("Flag_Tarik_Kontainer")) = "Y" Then
                            SQL = " SELECT top 1 ('Tgl Tarik Terakhir : '+ format(ISNULL((select top 1 tgl_tarik from tarik_kontainer where id_rencana ='" & .Rows(index).Item("id_rencana") & "' and kode_perusahaan ='" & KodePerusahaan & "'),"
                            SQL = SQL & " (select top 1 tgl_tarik from tarik_kontainer where id_rencana ='" & .Rows(index).Item("id_rencana") & "' and kode_perusahaan ='" & KodePerusahaan & "')),'dd MMM yyyy')"
                            SQL = SQL & " )as keterangan, 'Tarik Kontainer' as status "
                            SQL = SQL & " FROM tarik_kontainer tk, rencana_order ro WHERE tk.id_rencana = ro.id_rencana and "
                            SQL = SQL & " ro.kode_perusahaan = '" & KodePerusahaan & "' AND ro.id_rencana = '" & .Rows(index).Item("id_rencana") & "' "

                            warna = "YELLOW"
                        End If


                        If General_Class.CekNULL(.Rows(index).Item("Flag_Bongkar")) = "Y" Or General_Class.CekNULL(.Rows(index).Item("Flag_sudah_Transaksi")) = "Y" _
                            Or General_Class.CekNULL(.Rows(index).Item("Flag_sudah_Transaksi3")) = "Y" Or General_Class.CekNULL(.Rows(index).Item("Flag_Lokasi_Tujuan")) = "Y" _
                            Or General_Class.CekNULL(.Rows(index).Item("Flag_Billing")) = "Y" Or General_Class.CekNULL(.Rows(index).Item("Flag_HPP")) = "Y" Then

                            SQL = " SELECT ('Tgl Bongkar : '+ format(bi.tanggal_bongkar,'dd MMM yyyy') ) as keterangan, 'Bongkar' as status"
                            SQL = SQL & " FROM bongkar_import bi, rencana_order ro WHERE bi.id_rencana = ro.id_rencana and  "
                            SQL = SQL & " ro.kode_perusahaan = '" & KodePerusahaan & "' AND ro.id_rencana = '" & .Rows(index).Item("id_rencana") & "'  "

                            warna = "GREEN"
                        End If

                        If SQL <> "" Then
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then

                                    DgvPO_DataImport.Rows.Add(1)


                                    DgvPO_DataImport.Rows(ind2).Cells(CellNo_PoImport).Value = .Rows(index).Item("id_rencana")
                                    DgvPO_DataImport.Rows(ind2).Cells(CellLokasiImport).Value = .Rows(index).Item("Lokasi")
                                    DgvPO_DataImport.Rows(ind2).Cells(CellTanggalImport).Value = Format(.Rows(index).Item("Tanggal_po"), "dd MMMM yyyy")
                                    DgvPO_DataImport.Rows(ind2).Cells(CellKd_SupplierImport).Value = .Rows(index).Item("Kode_Supplier")
                                    DgvPO_DataImport.Rows(ind2).Cells(CellNm_SupplierImport).Value = .Rows(index).Item("nama_supplier")
                                    DgvPO_DataImport.Rows(ind2).Cells(CellLokasiGudangImport).Value = "" '.Rows(index).Item("Kode_Stock_Owner")
                                    DgvPO_DataImport.Rows(ind2).Cells(CellKeteranganImport).Value = dr("status") & Environment.NewLine & dr("keterangan")

                                    If warna = "RED" Then
                                        DgvPO_DataImport.Rows(ind2).Cells(CellKeteranganImport).Style.BackColor = Color.Coral
                                    ElseIf warna = "YELLOW" Then
                                        DgvPO_DataImport.Rows(ind2).Cells(CellKeteranganImport).Style.BackColor = Color.LightGoldenrodYellow
                                    ElseIf warna = "GREEN" Then
                                        DgvPO_DataImport.Rows(ind2).Cells(CellKeteranganImport).Style.BackColor = Color.LightGreen
                                    End If

                                    ind2 += 1
                                End If


                            End Using
                        End If


                    Next
                End With
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Public Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        ComboBox1.SelectedIndex = -1
        TextBox3.Text = ""

        Cari("Y")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnNewPO.Click
        'EMI_PO_Pembelian_Barang_Lain.kosong()
        'EMI_PO_Pembelian_Barang_Lain.TxtPO_KdSupplier.Enabled = True
        'EMI_PO_Pembelian_Barang_Lain.TxtPO_NmSupplier.Enabled = True
        'EMI_PO_Pembelian_Barang_Lain.TxtPO_NmSupplier.Focus()
        EMI_PO_Pembelian_Sub_Barang_Lain.Asal = ""
        EMI_PO_Pembelian_Sub_Barang_Lain.Txt_Faktur_Induk.Text = ""
        EMI_PO_Pembelian_Sub_Barang_Lain.kosong()
        EMI_PO_Pembelian_Sub_Barang_Lain.ShowDialog()

    End Sub

    Private Sub BtnSelisih_Click(sender As Object, e As EventArgs) Handles BtnSelisih.Click
        'EMI_Exclude_PO_Display.kosong()
        'EMI_Exclude_PO_Display.Edit = "Y"
        'EMI_Exclude_PO_Display.ShowDialog()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        EMI_PO_Pembelian_Display_View_Barang_Lain.ShowDialog()
    End Sub

    Private Sub DgvPO_DataLocal_DoubleClick(sender As Object, e As EventArgs)
        Dim currentRow = DgvPO_DataLocal.CurrentRow.Index
        Get_Isi_ListviewLocal(currentRow)


        EMI_PO_Pembelian_Sub_Barang_Lain.kosong()
        EMI_PO_Pembelian_Sub_Barang_Lain.TxtPO_NoFaktur.Text = LvNo_PoLocal
        EMI_PO_Pembelian_Sub_Barang_Lain.CmbPO_Lokasi.Text = LvLokasiLocal
        EMI_PO_Pembelian_Sub_Barang_Lain.TxtPO_NoFaktur_Leave(Me, e)
        EMI_PO_Pembelian_Sub_Barang_Lain.Asal = "edit"
        EMI_PO_Pembelian_Sub_Barang_Lain.ShowDialog()




        'If LvIDLocal = ID_Prepare Then
        '    If asal = "PO_Bahan" Then
        '        EMI_PO_Pembelian_Barang_Lain.kosong()
        '        EMI_PO_Pembelian_Barang_Lain.TxtPO_NoPO.Text = LvNo_PoLocal
        '        EMI_PO_Pembelian_Barang_Lain.CmbPO_Lokasi.Text = LvLokasiLocal
        '        EMI_PO_Pembelian_Barang_Lain.TxtPO_NmSupplier.Text = LvNm_SupplierLocal
        '        EMI_PO_Pembelian_Barang_Lain.TxtPO_KdSupplier.Text = LvKd_SupplierLocal

        '        EMI_PO_Pembelian_Barang_Lain.TxtPO_KdSupplier.Enabled = False
        '        EMI_PO_Pembelian_Barang_Lain.TxtPO_NmSupplier.Enabled = False
        '        Dim gudang As String = ""
        '        Try
        '            OpenConn()
        '            SQL = "select Kode_Stock_Owner_gudang from Binding_Lokasi_Gudang where kode_perusahaan = '" & KodePerusahaan & "' and Gudang_Default = 'Y' and Kode_Stock_Owner='" & LvLokasiLocal & "' "
        '            Using dr = OpenTrans(SQL)
        '                If dr.Read Then
        '                    gudang = dr("Kode_Stock_Owner_gudang")
        '                End If
        '            End Using

        '            CloseConn()
        '        Catch ex As Exception
        '            CloseConn()
        '            MessageBox.Show(ex.Message)
        '            Exit Sub
        '        End Try

        '        EMI_PO_Pembelian_Barang_Lain.CmbPO_LokasiGudang.Text = gudang
        '        EMI_PO_Pembelian_Barang_Lain.Ambil_Data()

        '        EMI_PO_Pembelian_Barang_Lain.ShowDialog()
        '    Else
        '        MessageBox.Show(Base_Language.Lang_Global_FormAsal & " . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If
        '''ElseIf LvIDLocal = ID_PO Then
        '''    EMI_PO_Pembelian_SD_ETD.TxtNoPO.Text = LvNo_PoLocal
        '''    EMI_PO_Pembelian_SD_ETD.ShowDialog()
        '''ElseIf LvIDLocal = ID_ETD Then
        '''    EMI_PO_Pembelian_SD_ETA.Lokasi = LvLokasiLocal
        '''    EMI_PO_Pembelian_SD_ETA.Kd_sup = LvKd_SupplierLocal
        '''    EMI_PO_Pembelian_SD_ETA.ShowDialog()
       ' End If

    End Sub
End Class
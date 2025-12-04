Imports System.IO

Public Class CutOFF_PremixSN

    Dim arrInisialFaktur As New ArrayList

    Dim itemNoFak As Integer = 0
    Dim itemNoPO As Integer = 1
    Dim itemNoNota As Integer = 2

    Dim isError As Boolean = False

    Dim Random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Private Sub FormTest_UpdateJurnal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Reservasi", 120, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No Split", 120, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Stock Owner", 160, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 140, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("User Input", 100, HorizontalAlignment.Left)
        Lv_Data.View = View.Details


        Kosong()

    End Sub

    Private Sub Kosong()

        Lv_Data.Items.Clear()
        Txt_NoReservasi.Text = ""

        Load_Data()

    End Sub

    Private Sub cr_Click(sender As Object, e As EventArgs) Handles cr.Click
        Load_Data()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Load_Data()

        Try
            OpenConn()

            '         Lv_Data.Items.Clear()
            '         SQL = $"select a.No_Faktur_Order, c.Kode_Barang, d.SN_Baru as SN, d.Jumlah_Barang as Qty, f.Tgl_Produksi, e.Jumlah from 
            'N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c,
            'N_EMI_Transaksi_Material_Requisition_QC_Validasi d, Barang_SN e, Emi_Split_Production_Order f where
            'a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan
            'and a.Kode_Perusahaan = f.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail 
            'and c.No_Faktur = d.No_Faktur_RM and c.Urut_Oto = d.Urut_Det_RM and d.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and d.Kode_Barang 
            '= e.Kode_Barang and d.SN_Baru = e.Serial_Number and a.No_Faktur_Order = f.No_Transaksi and a.Status is null and f.Status is null and
            'a.Kode_Perusahaan = '001' and a.no_faktur='{Txt_NoReservasi.Text}' and e.Jumlah<>d.Jumlah_Barang "
            '         Using Dr = OpenTrans(SQL)
            '             Do While Dr.Read
            '                 Dim Lv As ListViewItem
            '                 Lv = Lv_Data.Items.Add(Dr("SN"))
            '                 Lv.SubItems.Add(Dr("Kode_Barang"))
            '                 Lv.SubItems.Add(Dr("Qty"))
            '                 Lv.SubItems.Add(Dr("Jumlah"))
            '                 Lv.SubItems.Add(Dr("Qty") - Dr("Jumlah"))
            '             Loop
            '         End Using


            Lv_Data.Items.Clear()
            SQL = $"
                select distinct a.no_faktur, a.No_Faktur_Order, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Kode_Barang, a.Nama, a.Keterangan, a.UserId
                from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c,
	                N_EMI_Transaksi_Material_Requisition_QC_Validasi d, Barang_SN e, Emi_Split_Production_Order f 
                where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = f.Kode_Perusahaan 
                and a.No_Faktur = b.No_Faktur 
                and b.No_Faktur = c.No_Faktur 
                and b.Urut_Oto = c.Urut_Detail 
                and c.No_Faktur = d.No_Faktur_RM 
                and c.Urut_Oto = d.Urut_Det_RM 
                and d.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner 
                and d.Kode_Barang = e.Kode_Barang 
                and d.SN_Baru = e.Serial_Number 
                and a.No_Faktur_Order = f.No_Transaksi 
                and a.Status is null and f.Status is null 
                and e.Jumlah<>d.Jumlah_Barang 
                and a.Kode_Perusahaan = '{KodePerusahaan}'
            "
            If Txt_NoReservasi.Text.Trim.Length > 0 Then
                SQL &= $" AND a.no_faktur='{Txt_NoReservasi.Text.Trim}'"
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("no_faktur"))
                    Lv.SubItems.Add(Dr("No_Faktur_Order"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("UserId"))

                Loop
            End Using




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Dim NoReservasi As String = Lv_Data.FocusedItem.Text

        If MessageBox.Show($"Yakin Ingin Retur {NoReservasi} ini ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '====================
            '=     GET DATA     =
            '====================
            '     SQL = $"Select a.No_Faktur_Order, c.Kode_Barang, d.SN_Baru As SN, d.Jumlah_Barang As Qty, f.Tgl_Produksi, e.Jumlah from 
            'N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c,
            '         N_EMI_Transaksi_Material_Requisition_QC_Validasi d, Barang_SN e, Emi_Split_Production_Order f where
            '         a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan And c.Kode_Perusahaan = d.Kode_Perusahaan
            'And a.Kode_Perusahaan = f.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And b.No_Faktur = c.No_Faktur And b.Urut_Oto = c.Urut_Detail 
            'And c.No_Faktur = d.No_Faktur_RM And c.Urut_Oto = d.Urut_Det_RM And d.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner And d.Kode_Barang 
            '= e.Kode_Barang And d.SN_Baru = e.Serial_Number And a.No_Faktur_Order = f.No_Transaksi And a.Status Is null And f.Status Is null And
            'a.Kode_Perusahaan = '001' and a.no_faktur='{Txt_NoReservasi.Text}' and e.Jumlah<>d.Jumlah_Barang 
            '     "

            '=================================
            '=     GET DETAIL DATA RM QC     =
            '=================================
            SQL = $"
                Select a.No_Faktur_Order, c.Kode_Barang, d.SN_Baru As SN, d.Jumlah_Barang As Qty, f.Tgl_Produksi, e.Jumlah
                from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c,
	                N_EMI_Transaksi_Material_Requisition_QC_Validasi d, Barang_SN e, Emi_Split_Production_Order f 
                where a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan And c.Kode_Perusahaan = d.Kode_Perusahaan And a.Kode_Perusahaan = f.Kode_Perusahaan 
                And a.No_Faktur = b.No_Faktur 
                And b.No_Faktur = c.No_Faktur 
                And b.Urut_Oto = c.Urut_Detail 
                And c.No_Faktur = d.No_Faktur_RM 
                And c.Urut_Oto = d.Urut_Det_RM 
                And d.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner 
                And d.Kode_Barang = e.Kode_Barang 
                And d.SN_Baru = e.Serial_Number 
                And a.No_Faktur_Order = f.No_Transaksi 
                And a.Status Is null And f.Status Is null 
                and e.Jumlah <> d.Jumlah_Barang 
                And a.Kode_Perusahaan = '{KodeProyek}' 
                and a.no_faktur='{NoReservasi}' 
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Sn = .Rows(i).Item("SN")
                            Dim kdbarang = .Rows(i).Item("Kode_Barang")
                            Dim NoSplit As String = .Rows(i).Item("No_Faktur_Order")

                            '======================================================
                            '=     CEK SN YANG ADA DI RESULT DET (Dipakai GI)     =
                            '======================================================
                            SQL = $"
                                select b.kode_perusahaan, b.no_transaksi, kode_stock_owner, kode_barang, Nilai, b.urut,
                                    dbo.get_hpp(b.Serial_Number) as HPP
                                from Emi_Production_Results a, Emi_Production_Results_det b
                                where a.No_Transaksi=b.No_Transaksi and a.Status is null and b.Serial_Number='{Sn}'   
                            "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                        Dim Nilai_GI As Double = Ds2.Tables("MyTable").Rows(j).Item("Nilai")
                                        Dim Urut_GI As Integer = Ds2.Tables("MyTable").Rows(j).Item("urut")
                                        Dim HPP_GI As Double = Ds2.Tables("MyTable").Rows(j).Item("HPP")
                                        Dim So_GI As String = Ds2.Tables("MyTable").Rows(j).Item("kode_stock_owner")

                                        Dim JumlahPotong As Double = 0

                                        '=================================================
                                        '=     GET STOCK BARANG DI GUDANG PRODUCTION     =
                                        '=================================================
                                        SQL = $"
                                                select a.Serial_number, a.Jumlah, a.kode_stock_owner, a.kode_barang,
                                                    dbo.get_hpp(a.Serial_Number) as HPP
                                                from barang_sn a, barang b
                                                where a.kode_barang=b.kode_barang and a.kode_stock_owner=b.kode_stock_owner and b.id_group_jenis=1
                                                and round(jumlah,4)<>0 and a.kode_stock_owner='WET PRODUCTION'
                                                and a.no_reservasi is null 
                                                and a.Blok_SN is null
                                                and a.kode_barang='{kdbarang}'
                                                and dbo.get_hpp(a.Serial_Number) = {Val(HilangkanTanda(HPP_GI))}
                                                order by a.Tgl_Expired ASC
                                                "
                                        Using Ds3 = BindingTrans(SQL)
                                            If Ds3.Tables("MyTable").Rows.Count <> 0 Then
                                                Dim sisa As Double = Val(HilangkanTanda(Nilai_GI))
                                                For k As Integer = 0 To Ds3.Tables("MyTable").Rows.Count - 1

                                                    If sisa < Ds3.Tables("MyTable").Rows(k).Item("Jumlah") Or sisa = Ds3.Tables("MyTable").Rows(k).Item("Jumlah") Then
                                                        SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_stock_owner = '" & Ds3.Tables("MyTable").Rows(k).Item("kode_stock_owner") & "' and "
                                                        SQL = SQL & "kode_barang = '" & Ds3.Tables("MyTable").Rows(k).Item("kode_barang") & "' and "
                                                        SQL = SQL & "serial_number = '" & Ds3.Tables("MyTable").Rows(k).Item("Serial_number") & "'"
                                                        ExecuteTrans(SQL)



                                                        '=================================
                                                        '=       INSERT RESULT DET       =
                                                        '=================================
                                                        SQL = $"
                                                                insert into Emi_Production_Results_det (Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang, Nilai, Serial_Number, No_Urut_Detail )
                                                                select Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang, {sisa}, 
                                                                {Ds3.Tables("MyTable").Rows(k).Item("Serial_number")}, No_Urut_Detail
                                                                from Emi_Production_Results_det
                                                                where Kode_Perusahaan = '{KodePerusahaan}' 
                                                                and Urut = '{Urut_GI}'
                                                            "
                                                        ExecuteTrans(SQL)


                                                        JumlahPotong = Val(HilangkanTanda(sisa))

                                                        'Nilai_Bahan = Nilai_Bahan + (Math.Round(HPP * sisa, 0))
                                                        sisa = 0
                                                    ElseIf sisa > Ds3.Tables("MyTable").Rows(k).Item("Jumlah") Then
                                                        SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_stock_owner = '" & Ds3.Tables("MyTable").Rows(k).Item("kode_stock_owner") & "' and "
                                                        SQL = SQL & "kode_barang = '" & Ds3.Tables("MyTable").Rows(k).Item("kode_barang") & "' and "
                                                        SQL = SQL & "serial_number = '" & Ds3.Tables("MyTable").Rows(k).Item("Serial_number") & "'"
                                                        ExecuteTrans(SQL)


                                                        '=================================
                                                        '=       INSERT RESULT DET       =
                                                        '=================================
                                                        SQL = $"
                                                                insert into Emi_Production_Results_det (Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang, Nilai, Serial_Number, No_Urut_Detail )
                                                                select Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang, {Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(k).Item("Jumlah")))}, 
                                                                {Ds3.Tables("MyTable").Rows(k).Item("Serial_number")}, No_Urut_Detail
                                                                from Emi_Production_Results_det
                                                                where Kode_Perusahaan = '{KodePerusahaan}' 
                                                                and Urut = '{Urut_GI}'
                                                            "
                                                        ExecuteTrans(SQL)

                                                        JumlahPotong = Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(k).Item("Jumlah")))

                                                        'Nilai_Bahan = Nilai_Bahan + (Math.Round(HPP * .Rows(h).Item("jumlah"), 0))
                                                        sisa = sisa - Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(k).Item("Jumlah")))
                                                    Else
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & Ds3.Tables("MyTable").Rows(k).Item("kode_barang") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If

                                                Next
                                            End If

                                        End Using

                                        '====================================
                                        '=       CEK KESESUAIAN STOCK       =
                                        '====================================
                                        SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                        SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                        SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                        SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                                        SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
                                        SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                        SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = 'WET PRODUCTION' "
                                        SQL = SQL & "AND a.Kode_Barang = '" & kdbarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                        SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                        Using Ds9 = BindingTrans(SQL)

                                            If Ds9.Tables("MyTable").Rows.Count <> 0 Then
                                                If Ds9.Tables("MyTable").Rows(0).Item("good_stock") <> Ds9.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
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
                                        End Using

                                        'Cek Jumlah Potong apakah sesuai dengan jumlah GI
                                        If Val(HilangkanTanda(JumlahPotong)) <> Val(HilangkanTanda(Nilai_GI)) Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Jumlah Potong Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        'Dim Id_WarehouseTujuan, NoPalletTujuan As String
                                        'SQL = "SELECT TOP(1) "
                                        'SQL = SQL & "a.id_wms_warehouse_position, b.nomor_urut "
                                        'SQL = SQL & "FROM view_warehouse_position a, view_warehouse_position_detail b "
                                        'SQL = SQL & "WHERE a.Id_WMS_Warehouse_Position = b.Id_WMS_Warehouse_Position "
                                        'SQL = SQL & "AND a.kode_Perusahaan = b.kode_Perusahaan "
                                        'SQL = SQL & "AND a.kode_Perusahaan = '" & KodePerusahaan & "' "
                                        'SQL = SQL & "AND a.Kode_Stock_Owner = '" & So_GI & "' "
                                        'SQL = SQL & "AND b.Kode_Barang IS NULL;"
                                        'Using Dr = OpenTrans(SQL)
                                        '    If Dr.Read Then
                                        '        Id_WarehouseTujuan = Dr("id_wms_warehouse_position")
                                        '        NoPalletTujuan = Dr("nomor_urut")
                                        '    Else
                                        '        Dr.Close()
                                        '        CloseTrans()
                                        '        CloseConn()
                                        '        MessageBox.Show("Pallet Kosong Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        '        Exit Sub
                                        '    End If
                                        'End Using


                                        'Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                        'Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                        'Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & HPP_GI & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                        'Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

                                        ''==============================
                                        ''=       INSERT SN BARU       =
                                        ''==============================
                                        'SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                                        'SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN, id_jenis_kategori_produksi, No_Reservasi) "
                                        'SQL = SQL & "select Kode_Perusahaan, '" & So_GI & "', Kode_Barang, '" & SN_Baru & "', '" & Nilai_GI & "', 0, "
                                        'SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & Id_WarehouseTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                                        'SQL = SQL & "Kode_Unik_Asal, '" & NoPalletTujuan & "', batch_number, Warna, Tgl_Masuk, NULL, id_jenis_kategori_produksi, " & NoSplit & " "
                                        'SQL = SQL & "from Barang_SN "
                                        'SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                                        'SQL = SQL & "and Kode_Stock_Owner='" & So_GI & "' "
                                        'SQL = SQL & "and Kode_Barang='" & kdbarang & "' "
                                        'SQL = SQL & "and Serial_Number='" & Sn & "' "
                                        'ExecuteTrans(SQL)


                                        '=================================
                                        '=       UPDATE RESULT DET       =
                                        '=================================
                                        SQL = $"
                                            update Emi_Production_Results_det set Nilai = 0
                                            where Kode_Perusahaan = '{KodePerusahaan}' 
                                            and Urut = '{Urut_GI}'
                                        "
                                        ExecuteTrans(SQL)


                                        '=========================================================
                                        '=       UPDATE STOCK BARANG YG DI TRANSFER PREMIX       =
                                        '=========================================================
                                        SQL = $"
                                            update Barang_SN set jumlah += {Val(HilangkanTanda(Nilai_GI))}
                                            where kode_perusahaan = '{KodePerusahaan}'
                                            and kode_barang = '{kdbarang}'
                                            and serial_number = '{Sn}'
                                        "
                                        ExecuteTrans(SQL)



                                    Next
                                End If

                            End Using

                        Next
                    End If
                End With
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil di Retur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        '        Try
        '            OpenConn()
        '            Cmd.Transaction = Cn.BeginTransaction


        '            '====================
        '            '=     GET DATA     =
        '            '====================
        '            SQL = $"Select a.No_Faktur_Order, c.Kode_Barang, d.SN_Baru As SN, d.Jumlah_Barang As Qty, f.Tgl_Produksi, e.Jumlah from 
        '			    N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c,
        '                N_EMI_Transaksi_Material_Requisition_QC_Validasi d, Barang_SN e, Emi_Split_Production_Order f where
        '                a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan And c.Kode_Perusahaan = d.Kode_Perusahaan
        '			    And a.Kode_Perusahaan = f.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And b.No_Faktur = c.No_Faktur And b.Urut_Oto = c.Urut_Detail 
        '			    And c.No_Faktur = d.No_Faktur_RM And c.Urut_Oto = d.Urut_Det_RM And d.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner And d.Kode_Barang 
        '			    = e.Kode_Barang And d.SN_Baru = e.Serial_Number And a.No_Faktur_Order = f.No_Transaksi And a.Status Is null And f.Status Is null And
        '			    a.Kode_Perusahaan = '001' and a.no_faktur='{Txt_NoReservasi.Text}' and e.Jumlah<>d.Jumlah_Barang 

        '            "
        '            Using Ds = BindingTrans(SQL)
        '                With Ds.Tables("MyTable")
        '                    If .Rows.Count <> 0 Then
        '                        For i As Integer = 0 To .Rows.Count - 1

        '                            Dim Sn = .Rows(i).Item("SN")
        '                            Dim kdbarang = .Rows(i).Item("Kode_Barang")

        '                            SQL = $"
        '                            select b.kode_perusahaan, b.no_transaksi, kode_stock_owner, kode_barang, Nilai, b.urut 
        '                            from Emi_Production_Results a, Emi_Production_Results_det b where
        '                            a.No_Transaksi=b.No_Transaksi and a.Status is null and Serial_Number='{Sn}'   
        '                                "
        '                            Using Ds2 = BindingTrans(SQL)
        '                                If .Rows.Count <> 0 Then
        '                                    For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

        '                                        SQL = $"
        '                                                select a.Serial_number, Jumlah
        '                                                from barang_sn a, barang b where
        '                                                a.kode_barang=b.kode_barang and a.kode_stock_owner=b.kode_stock_owner and b.id_group_jenis=1
        '                                                and round(jumlah,4)<>0 and a.kode_stock_owner='WET PRODUCTION'
        '                                                and no_reservasi is null and kode_barang='{kdbarang}'

        '                                                fifo
        '                                                "
        '                                        Using Ds3 = BindingTrans(SQL)
        '                                            If .Rows.Count <> 0 Then
        '                                                For k As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
        '                                                    'no Split update no servesi


        '                                                    'Cek FG


        '                                                Next
        '                                            End If

        '                                        End Using


        '                                    Next
        '                                End If

        '                            End Using


        '                        Next
        '                    End If
        '                End With
        '            End Using


        '#Region "JURNAL PERJALANAN"

        '            ''Get Data Perjalanan Lokal
        '            'Dim Arr_Biaya_Lokal_Master As New ArrayList
        '            'Dim Arr_Biaya_Lokal_Kategori As New ArrayList
        '            'Dim Arr_Biaya_Lokal As New ArrayList
        '            'Dim Arr_Akun1 As New ArrayList
        '            'Dim Arr_Akun2 As New ArrayList

        '            'SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, "
        '            'SQL = SQL & "round(sum(b.total), 0) as Biaya, "

        '            'SQL = SQL & "isnull(("
        '            'SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
        '            'SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
        '            'SQL = SQL & "x.lokasi = '" & Lokasi & "' "
        '            'SQL = SQL & "),0) as Akun_1, "

        '            'SQL = SQL & "isnull(("
        '            'SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
        '            'SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
        '            'SQL = SQL & "x.lokasi = '" & Lokasi & "' "
        '            'SQL = SQL & "),0) as Akun_2 "

        '            'SQL = SQL & "from transaksi_biaya_Lokal a, transaksi_biaya_Lokal_detail b, Master_Kategori_Biaya_Import c where "
        '            'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
        '            'SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
        '            'SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
        '            'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.no_Po = '" & noPembelianPO & "' " 'and C.Flag_Gabungan = 'Y' "
        '            'SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
        '            'SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
        '            'Using ds = BindingTrans(SQL)
        '            '    With ds.Tables("MyTable")
        '            '        For index3 As Integer = 0 To .Rows.Count - 1

        '            '            Arr_Biaya_Lokal_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
        '            '            Arr_Biaya_Lokal_Kategori.Add(.Rows(index3).Item("kode_kategori_biaya_import"))
        '            '            Arr_Biaya_Lokal.Add(Val(HilangkanTanda(Format(.Rows(index3).Item("Biaya"), "N0"))))
        '            '            Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
        '            '            Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

        '            '            Biaya_Lokal_Total = Biaya_Lokal_Total + Val(HilangkanTanda(Format(.Rows(index3).Item("Biaya"), "N0")))

        '            '        Next
        '            '    End With

        '            'End Using

        '            'For indexKategoriImport As Integer = 0 To Arr_Biaya_Lokal_Master.Count - 1

        '            '    SQL = "select* from Detail_Account_Master where "
        '            '    SQL = SQL & "Lokasi = '" & Lokasi & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Lokal_Master.Item(indexKategoriImport) & "' "
        '            '    SQL = SQL & "and Akun_1 ='" & Arr_Akun1.Item(indexKategoriImport) & "'  and Akun_2 = '" & Arr_Akun2.Item(indexKategoriImport) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
        '            '    Using Dr = OpenTrans(SQL)
        '            '        If Not Dr.Read Then
        '            '            Dr.Close()
        '            '            CloseTrans()
        '            '            CloseConn()
        '            '            MessageBox.Show("Akun " & Arr_Biaya_Lokal_Master.Item(indexKategoriImport) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            '            Exit Sub
        '            '        End If
        '            '    End Using

        '            'Next

        '            ''INsert Jurnal
        '            'For indexKategoriImport As Integer = 0 To Arr_Biaya_Lokal_Master.Count - 1
        '            '    If Val(Arr_Biaya_Lokal.Item(indexKategoriImport)) <> 0 Then

        '            '        'Jurnal Kategori Biaya Lokal
        '            '        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
        '            '        SQL = SQL & "kode_voucher = '" & kode_voucher & "' and "
        '            '        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2.Item(indexKategoriImport) & "' and kredit <> 0 "
        '            '        SQL = SQL & "and keterangan ='Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "; " & noPembelian & "'"
        '            '        Using Dr = OpenTrans(SQL)
        '            '            If Dr.Read Then
        '            '                Dr.Close()
        '            '                'update

        '            '                SQL = "update detail_jurnal set kredit = kredit+ " & Arr_Biaya_Lokal.Item(indexKategoriImport) & " where "
        '            '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '            '                SQL = SQL & "kode_voucher = '" & kode_voucher & "' and "
        '            '                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2.Item(indexKategoriImport) & "' and kredit <> 0 "
        '            '                SQL = SQL & "and keterangan ='Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "; " & noPembelian & "'"
        '            '                ExecuteTrans(SQL)
        '            '            Else
        '            '                Dr.Close()
        '            '                'insert

        '            '                SQL = Get_Detail_Jurnal(kode_voucher, Strings.Left(Arr_Akun2.Item(indexKategoriImport), 1),
        '            '                            Strings.Mid(Arr_Akun2.Item(indexKategoriImport), 2, 1),
        '            '                            Strings.Mid(Ganti(Arr_Akun2.Item(indexKategoriImport)), 3),
        '            '                            KodePerusahaan, KodeProyek, "Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "; " & noPembelian, "0", Arr_Biaya_Lokal.Item(indexKategoriImport), pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
        '            '                ExecuteTrans(SQL)
        '            '                pagenumber = pagenumber + 1

        '            '            End If
        '            '        End Using

        '            '        'Data Kategori Biaya Lokal
        '            '        SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
        '            '        SQL = SQL & "No_Faktur = '" & noPembelian.Trim & "' and "
        '            '        SQL = SQL & "Kode = '" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "' and "
        '            '        SQL = SQL & "Kode_Akun = '" & Arr_Akun2.Item(indexKategoriImport) & "'  "
        '            '        Using Dr = OpenTrans(SQL)
        '            '            If Dr.Read Then
        '            '                Dr.Close()
        '            '                'update

        '            '                SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Lokal.Item(indexKategoriImport) & " where "
        '            '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '            '                SQL = SQL & "No_Faktur = '" & noPembelian.Trim & "' and "
        '            '                SQL = SQL & "Kode = '" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "' and "
        '            '                SQL = SQL & "Kode_Akun = '" & Arr_Akun2.Item(indexKategoriImport) & "'  "
        '            '                ExecuteTrans(SQL)
        '            '            Else
        '            '                Dr.Close()
        '            '                'insert

        '            '                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
        '            '                SQL = SQL & "values('" & KodePerusahaan & "', '" & noPembelian.Trim & "', "
        '            '                SQL = SQL & "'" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "', '" & Arr_Biaya_Lokal.Item(indexKategoriImport) & "', "
        '            '                SQL = SQL & "'" & Arr_Akun2.Item(indexKategoriImport) & "')"
        '            '                ExecuteTrans(SQL)

        '            '            End If
        '            '        End Using
        '            '    End If
        '            'Next

        '#End Region

        '            'SELISIH PEmbualatan


        '            Cmd.Transaction.Commit()
        '            CloseTrans()
        '            CloseConn()
        '        Catch ex As Exception
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show(ex.Message)
        '            Exit Sub
        '        End Try

    End Sub

    Private Sub Jurnal_Lokal(ByVal noPembelian As String, ByVal noPembelianPO As String, ByVal Lokasi As String)

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            '==================================
            '=     HAPUS DATA LAMA DETAIL Jurnal     =
            '==================================
            Dim kode_voucher As String = ""

            SQL = "select Kode_voucher from emi_pembelian where Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & noPembelian & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        kode_voucher = .Rows(0).Item("Kode_voucher")

                        SQL = "delete detail_Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_voucher = '" & .Rows(0).Item("Kode_voucher") & "' "
                        ExecuteTrans(SQL)
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data tidak ditemukan!.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '===============================================
            '=     HAPUS DATA LAMA Pelunasan_Pembelian     =
            '===============================================
            SQL = "select Kode_Perusahaan from Pelunasan_Pembelian where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noPembelian & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        SQL = "Delete Pelunasan_Pembelian where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noPembelian & "'"
                        ExecuteTrans(SQL)
                    End If
                End With
            End Using


            Dim inisial_faktur_dari As String = ""
            Dim lokasi_Barang As String = ""
            Dim persen_PPN As Integer = 0
            SQL = "select inisial_faktur from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
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

            SQL = "select PPN from emi_pembelian_po "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noPembelianPO & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    persen_PPN = Dr("PPN")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Dim pagenumber As Integer = 1

            Dim Total_Persediaan As Double = 0
            Dim Total_PPN As Double = 0
            Dim Total_Perjalanan As Double = 0
            Dim Total_Hutang As Double = 0
            Dim Total_SelisihHutang As Double = 0
            Dim Total_selisih As Double = 0
            Dim Biaya_Lokal_Total As Double = 0
            Dim akun_Selisih_pembulatan As String = ""

            Dim TotDebit As Double = 0
            Dim TotKredit As Double = 0

            '====================
            '=     GET DATA     =
            '====================
            SQL = $"Select a.No_Faktur_Order, c.Kode_Barang, d.SN_Baru As SN, d.Jumlah_Barang As Qty, f.Tgl_Produksi, e.Jumlah from 
			    N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c,
                N_EMI_Transaksi_Material_Requisition_QC_Validasi d, Barang_SN e, Emi_Split_Production_Order f where
                a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan And c.Kode_Perusahaan = d.Kode_Perusahaan
			    And a.Kode_Perusahaan = f.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And b.No_Faktur = c.No_Faktur And b.Urut_Oto = c.Urut_Detail 
			    And c.No_Faktur = d.No_Faktur_RM And c.Urut_Oto = d.Urut_Det_RM And d.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner And d.Kode_Barang 
			    = e.Kode_Barang And d.SN_Baru = e.Serial_Number And a.No_Faktur_Order = f.No_Transaksi And a.Status Is null And f.Status Is null And
			    a.Kode_Perusahaan = '001' and a.no_faktur='{Txt_NoReservasi.Text}' and e.Jumlah<>d.Jumlah_Barang 
            
            "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim akun_persediaan_dari As String = ""
                            Dim akun_ppn As String = ""
                            Dim akun_hutang_sup As String = ""
                            Dim akun_hutang_ppn As String = ""

                            SQL = "select c.akun_Persediaan "
                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and b.kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and b.Kode_Barang='" & .Rows(i).Item("Kode_Barang") & "' "
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

                            SQL = "select Hutang_Supplier, Hutang_Perjalanan, Hutang_PPN, PPN_Pembelian, Selisih_Hutang_Import "
                            SQL = SQL & "from stock_owner "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    akun_hutang_sup = Dr("Hutang_Supplier")
                                    'akun_hutang_perjalanan = Dr("Hutang_Perjalanan")
                                    akun_hutang_ppn = Dr("Hutang_Supplier")
                                    akun_ppn = Dr("PPN_Pembelian")
                                    akun_Selisih_pembulatan = Dr("PPN_Pembelian")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            Dim HargaAkhir As Double = Val(HilangkanTanda(.Rows(i).Item("Harga_Akhir")))
                            Dim HargaPO As Double = Val(HilangkanTanda(.Rows(i).Item("HargaPO")))
                            Dim JumlahMasuk As Double = Val(HilangkanTanda(.Rows(i).Item("Jumlah_Masuk")))
                            Dim JumlahHutang As Double = Val(HilangkanTanda(.Rows(i).Item("jumlah_hutang")))
                            Dim PersenPPN As Double = Val(HilangkanTanda(.Rows(i).Item("Persen_PPN")))



                            'PERHITUNGAN
                            Dim NilaiPersediaan As Double = Math.Round(JumlahMasuk * HargaAkhir)
                            Dim NilaiPPN As Double = Math.Round((JumlahHutang * HargaPO) * (PersenPPN / 100))
                            'Dim NilaiPerjalanan As Double = Math.Round((HargaAkhir - HargaPO) * JumlahMasuk)
                            Dim NilaiHutang As Double = Math.Round((JumlahHutang * HargaPO) + NilaiPPN)
                            Dim selisih As Double = Math.Round(JumlahHutang - JumlahMasuk)
                            Dim NilaiSelisihHutang As Double = Math.Round(selisih * HargaPO)
                            'Dim TotalPO As Double = Math.Round(JumlahMasuk * HargaPO)


                            Total_Persediaan += NilaiPersediaan
                            Total_PPN += NilaiPPN
                            'Total_Perjalanan += NilaiPerjalanan
                            Total_Hutang += NilaiHutang
                            Total_SelisihHutang += NilaiSelisihHutang



                            '1
                            'JURNAL PERSEDIAAN
                            If NilaiPersediaan <> 0 Then
                                'Tambah Lokasi 
                                If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), kode_voucher, akun_persediaan_dari, "Persediaan", NilaiPersediaan, pagenumber, .Rows(i).Item("Kode_Stock_Owner"), "D") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Gagal Insert Nilai Persediaan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If

                            'JURNAL PPN
                            If NilaiPPN <> 0 Then
                                'RAgu pake akun yg mana :)
                                If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), kode_voucher, akun_ppn, "PPN", NilaiPPN, pagenumber, Lokasi, "D") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Gagal Insert PPN!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If

                            'JURNAL HUTANG SUPPLIER
                            If NilaiHutang <> 0 Then
                                If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), kode_voucher, akun_hutang_sup, "Hutang", NilaiHutang, pagenumber, Lokasi, "K") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Gagal Insert Hutang Supplier!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If




                            'JURNAL SELISIH HUTANG 
                            If selisih <> 0 Then
                                Dim akun_hutang_sup_selisih As String = ""
                                Total_selisih += Math.Round(selisih * HargaPO)

                                SQL = "select Akun_Bahan, Akun_Perjalanan from EMI_Pembelian_Selisih_Barang_Masuk "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur_bm = '" & .Rows(i).Item("No_loading") & "' and status is null "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        akun_hutang_sup_selisih = Dr("Akun_Perjalanan")
                                        'akun_hutang_perjalanan_selisih = Dr("Akun_Perjalanan")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                If selisih > 0 Then
                                    If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), kode_voucher, akun_hutang_sup_selisih, "Selisih Hutang", NilaiSelisihHutang, pagenumber, Lokasi, "D") Then
                                        CloseTrans()
                                        CloseConn()
                                        Exit Sub
                                    End If
                                ElseIf selisih < 0 Then
                                    If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), kode_voucher, akun_hutang_sup_selisih, "Selisih Hutang", Math.Abs(NilaiSelisihHutang), pagenumber, Lokasi, "K") Then
                                        CloseTrans()
                                        CloseConn()
                                        Exit Sub
                                    End If
                                End If
                            End If

                        Next
                    End If
                End With
            End Using


#Region "JURNAL PERJALANAN"

            'Get Data Perjalanan Lokal
            Dim Arr_Biaya_Lokal_Master As New ArrayList
            Dim Arr_Biaya_Lokal_Kategori As New ArrayList
            Dim Arr_Biaya_Lokal As New ArrayList
            Dim Arr_Akun1 As New ArrayList
            Dim Arr_Akun2 As New ArrayList

            SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, "
            SQL = SQL & "round(sum(b.total), 0) as Biaya, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi & "' "
            SQL = SQL & "),0) as Akun_1, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi & "' "
            SQL = SQL & "),0) as Akun_2 "

            SQL = SQL & "from transaksi_biaya_Lokal a, transaksi_biaya_Lokal_detail b, Master_Kategori_Biaya_Import c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.no_Po = '" & noPembelianPO & "' " 'and C.Flag_Gabungan = 'Y' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Arr_Biaya_Lokal_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                        Arr_Biaya_Lokal_Kategori.Add(.Rows(index3).Item("kode_kategori_biaya_import"))
                        Arr_Biaya_Lokal.Add(Val(HilangkanTanda(Format(.Rows(index3).Item("Biaya"), "N0"))))
                        Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
                        Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

                        Biaya_Lokal_Total = Biaya_Lokal_Total + Val(HilangkanTanda(Format(.Rows(index3).Item("Biaya"), "N0")))

                    Next
                End With

            End Using

            For indexKategoriImport As Integer = 0 To Arr_Biaya_Lokal_Master.Count - 1

                SQL = "select* from Detail_Account_Master where "
                SQL = SQL & "Lokasi = '" & Lokasi & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Lokal_Master.Item(indexKategoriImport) & "' "
                SQL = SQL & "and Akun_1 ='" & Arr_Akun1.Item(indexKategoriImport) & "'  and Akun_2 = '" & Arr_Akun2.Item(indexKategoriImport) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Akun " & Arr_Biaya_Lokal_Master.Item(indexKategoriImport) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End Using

            Next

            'INsert Jurnal
            For indexKategoriImport As Integer = 0 To Arr_Biaya_Lokal_Master.Count - 1
                If Val(Arr_Biaya_Lokal.Item(indexKategoriImport)) <> 0 Then

                    'Jurnal Kategori Biaya Lokal
                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2.Item(indexKategoriImport) & "' and kredit <> 0 "
                    SQL = SQL & "and keterangan ='Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "; " & noPembelian & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update

                            SQL = "update detail_jurnal set kredit = kredit+ " & Arr_Biaya_Lokal.Item(indexKategoriImport) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & kode_voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2.Item(indexKategoriImport) & "' and kredit <> 0 "
                            SQL = SQL & "and keterangan ='Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "; " & noPembelian & "'"
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = Get_Detail_Jurnal(kode_voucher, Strings.Left(Arr_Akun2.Item(indexKategoriImport), 1),
                                        Strings.Mid(Arr_Akun2.Item(indexKategoriImport), 2, 1),
                                        Strings.Mid(Ganti(Arr_Akun2.Item(indexKategoriImport)), 3),
                                        KodePerusahaan, KodeProyek, "Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "; " & noPembelian, "0", Arr_Biaya_Lokal.Item(indexKategoriImport), pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                        End If
                    End Using

                    'Data Kategori Biaya Lokal
                    SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & noPembelian.Trim & "' and "
                    SQL = SQL & "Kode = '" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "' and "
                    SQL = SQL & "Kode_Akun = '" & Arr_Akun2.Item(indexKategoriImport) & "'  "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update

                            SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Lokal.Item(indexKategoriImport) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "No_Faktur = '" & noPembelian.Trim & "' and "
                            SQL = SQL & "Kode = '" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "' and "
                            SQL = SQL & "Kode_Akun = '" & Arr_Akun2.Item(indexKategoriImport) & "'  "
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                            SQL = SQL & "values('" & KodePerusahaan & "', '" & noPembelian.Trim & "', "
                            SQL = SQL & "'" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "', '" & Arr_Biaya_Lokal.Item(indexKategoriImport) & "', "
                            SQL = SQL & "'" & Arr_Akun2.Item(indexKategoriImport) & "')"
                            ExecuteTrans(SQL)

                        End If
                    End Using
                End If
            Next

#End Region

            'SELISIH PEmbualatan
            Dim nilai_selisih As Double = (Total_Persediaan + Total_PPN + Total_SelisihHutang) - (Biaya_Lokal_Total + Total_Hutang)

            If nilai_selisih <> 0 Then
                If nilai_selisih < 0 Then
                    SQL = Get_Detail_Jurnal(kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
                            Strings.Mid(akun_Selisih_pembulatan, 2, 1),
                            Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
                            KodePerusahaan, KodeProyek, "Selisih Pembulatan; " & noPembelian, Math.Abs(nilai_selisih), "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1
                Else
                    SQL = Get_Detail_Jurnal(kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
                            Strings.Mid(akun_Selisih_pembulatan, 2, 1),
                            Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
                            KodePerusahaan, KodeProyek, "Selisih Pembulatan; " & noPembelian, "0", nilai_selisih, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & noPembelian.Trim & "', "
                SQL = SQL & "'Selisih Pembulatan', '" & nilai_selisih & "', "
                SQL = SQL & "'" & akun_Selisih_pembulatan & "')"
                ExecuteTrans(SQL)
            End If

            '==========================================================
            '=     UNTUK TES / LIAT DEBIT KREDIT BENAR ATAU SALAH     =
            '==========================================================
            SQL = "select * from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & noPembelian & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        'CloseTrans()
                        'CloseConn()
                        'Exit Sub
                    Else
                        'CloseTrans()
                        'CloseConn()
                        'Exit Sub
                    End If
                End With
            End Using

            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & kode_voucher & "'"
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

            SQL = "Update emi_pembelian "
            SQL = SQL & "Set Kode_Voucher = '" & kode_voucher & "' "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & noPembelian & "' "
            ExecuteTrans(SQL)



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        isError = True
    End Sub


    Private Function Insert_Jurnal(ByVal noPembelian As String, ByVal Kode_voucher As String, ByVal akun As String, ByVal Kode_Akun_Pembelian As String, ByVal Nilai As String, ByVal pagenumber As Integer, ByVal lks As String, ByVal Type As String) As Boolean

        Try

            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun & "'  "
            If Type = "D" Then
                SQL = SQL & "and debit <> 0"
            ElseIf Type = "K" Then
                SQL = SQL & "and kredit <> 0"
            End If
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update


                    If Type = "D" Then
                        SQL = "update detail_jurnal set debit = debit+ " & Nilai & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun & "'  "
                        SQL = SQL & "and debit <> 0"

                    ElseIf Type = "K" Then
                        SQL = "update detail_jurnal set kredit = kredit+ " & Nilai & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun & "'  "
                        SQL = SQL & "and kredit <> 0"
                    End If
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert
                    If Type = "D" Then
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
                              Strings.Mid(akun, 2, 1),
                              Strings.Mid(Ganti(akun), 3),
                              KodePerusahaan, KodeProyek, Kode_Akun_Pembelian & "; " & noPembelian, Nilai, "0", pagenumber, lks, Bahasa_Pilihan, Ket_Cost_Center_HO)

                    ElseIf Type = "K" Then
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
                              Strings.Mid(akun, 2, 1),
                              Strings.Mid(Ganti(akun), 3),
                              KodePerusahaan, KodeProyek, Kode_Akun_Pembelian & "; " & noPembelian, "0", Nilai, pagenumber, lks, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    End If
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & noPembelian & "' and "
            SQL = SQL & "Kode = '" & Kode_Akun_Pembelian & "' and "
            SQL = SQL & "Kode_Akun = '" & akun & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Nilai & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & noPembelian & "' and "
                    SQL = SQL & "Kode = '" & Kode_Akun_Pembelian & "' and "
                    SQL = SQL & "Kode_Akun = '" & akun & "'  "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & noPembelian & "', "
                    SQL = SQL & "'" & Kode_Akun_Pembelian & "', '" & Nilai & "', "
                    SQL = SQL & "'" & akun & "')"
                    ExecuteTrans(SQL)

                End If
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function


End Class

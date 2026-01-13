Imports System.IO

Public Class EMI_Validasi_Tf_Material_To_Material

    Dim JudulForm As String = "Validasi Transfer Material To Material"

    Dim Random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Dim kode_unik_print As String


    Private Sub EMI_Validasi_Tf_Material_To_Material_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub EMI_Validasi_Tf_Material_To_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()

    End Sub


    Private Sub Kosong()


        Try
            OpenConn()

            Txt_Barcode.Text = ""
            Barcode.ResetText()


            Lv_List.Columns.Clear()
            Lv_List.Columns.Add("Kode Transfer", 0, HorizontalAlignment.Left)
            Lv_List.Columns.Add("Lokasi", 200, HorizontalAlignment.Left)
            Lv_List.Columns.Add("Kode Barang", 200, HorizontalAlignment.Left)
            Lv_List.Columns.Add("Rak", 250, HorizontalAlignment.Left)
            Lv_List.Columns.Add("Nama", 0, HorizontalAlignment.Left)
            Lv_List.Columns.Add("Jumlah", 200, HorizontalAlignment.Right)
            Lv_List.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
            Lv_List.View = View.Details

            '========================
            '=     Load Data LV     =
            '========================
            Lv_List.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.Kode_Transfer, a.Tanggal, a.Kode_Stock_Owner, a.Kode_Barang_Awal, d.Nama, e.Keterangan as RAK, "
            SQL = SQL & "dbo.Ubah_Satuan(a.Kode_Perusahaan, 'masa', a.Kode_Barang_Awal, b.Satuan_Barang, a.Satuan, b.Jumlah_Barang) as Total, a.Total_Bags, a.Satuan "
            SQL = SQL & "from EMI_TF_Material_To_Material a, EMI_TF_Material_To_Material_Detail b , barang_sn c, barang d, View_Warehouse_Position e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.kode_transfer = b.kode_transfer "
            SQL = SQL & "and b.Serial_Number_Awal = c.Serial_Number "
            SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and b.Id_Warehouse_Awal = e.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.status is null  "
            SQL = SQL & "and b.Flag_Validasi is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Dim Lv As ListViewItem
                    Lv = Lv_List.Items.Add(Dr("Kode_Transfer"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang_Awal"))
                    Lv.SubItems.Add(Dr("RAK"))
                    Lv.SubItems.Add("X")
                    Lv.SubItems.Add(Format(Val(Dr("total")), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Txt_Barcode.Focus()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click

        If Txt_Barcode.Text.Trim.Length = 0 Then
            MessageBox.Show("Scan Dahulu QR", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Barcode.Focus() : Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim arr_Sn As New ArrayList

            Dim SerialNumberAwal As String = ""
            Dim SN_Baru As String = ""
            Dim id_WarehusTujuan As String = ""
            Dim No_PalletTujuan As String = ""
            Dim JumlahTambah As Double = 0
            Dim Kode_Transfer As String = ""
            Dim tglMsk As Date

            Dim ada_data As Boolean = False

            SQL = "Select c.serial_number "
            SQL = SQL & "from EMI_TF_Material_To_Material a, EMI_TF_Material_To_Material_Detail b, barang_sn c  "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan And a.Kode_Transfer = b.Kode_Transfer "
            SQL = SQL & "And a.status Is null And b.Flag_Validasi is null "
            SQL = SQL & "And b.kode_perusahaan=c.kode_Perusahaan And b.serial_number_awal=c.serial_number "
            SQL = SQL & "And c.kode_perusahaan='001' and c.qr_code+'-'+kode_unik_berjalan='" & Txt_Barcode.Text & "' "
            SQL = SQL & "order by c.Tgl_Expired "
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
                Kosong()
                Exit Sub
            End If

            Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)
            Dim namaBarang As String = ""
            'Genereate QR Baru
            Dim NewQR As String = ""
            Dim BerhasilInput As Boolean = False
            Dim SoAwal As String = ""
            Dim KdBarangAwal As String = ""
            Dim KdBarangTujuan As String = ""
            Dim QrLama As String = ""
            Dim batchLama As String = ""
            Dim expDate As String = ""
            Dim hargaIsn As String = ""

            For Indxx = 0 To arr_Sn.Count - 1

                'Ambil Data SN Berdasar Barcode
                SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired,b.Metode_Pengeluaran_Stok,a.Tgl_Masuk "
                SQL = SQL & "from barang_sn a, barang b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and a.Jumlah <> 0 "
                'SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & Txt_Barcode.Text & "' "
                SQL = SQL & "and a.Serial_Number = '" & arr_Sn(Indxx) & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read() Then
                        SerialNumberAwal = Dr("Serial_Number")

                        If General_Class.CekNULL(Dr("tgl_masuk")) = "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tanggal Masuk Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        tglMsk = General_Class.CekNULL(Dr("tgl_masuk"))

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                Dim total As Double = 0


                Dim warnaLama As String = ""
                Dim JmlhBarang As Double
                Dim harga_new As Double
                '====================
                '=     CEK DATA     =
                '====================
                SQL = "select a.Kode_Perusahaan, a.Kode_Transfer, a.Kode_Stock_Owner, a.Kode_Barang_Awal, a.Kode_Barang_Tujuan, "
                SQL = SQL & "b.Serial_Number_Awal, b.Jumlah_Barang, b.Jumlah_Bags, a.satuan, b.Satuan_Barang "
                SQL = SQL & "from EMI_TF_Material_To_Material a, EMI_TF_Material_To_Material_Detail b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Transfer = b.Kode_Transfer "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Status is null "
                SQL = SQL & "and a.Flag_Validasi is null "
                SQL = SQL & "and b.Serial_Number_Awal = '" & SerialNumberAwal & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                Kode_Transfer = .Rows(i).Item("Kode_Transfer")
                                SoAwal = .Rows(i).Item("Kode_Stock_Owner")
                                KdBarangAwal = .Rows(i).Item("Kode_Barang_Awal")
                                KdBarangTujuan = .Rows(i).Item("Kode_Barang_Tujuan")
                                Dim SN_Awal As String = .Rows(i).Item("Serial_Number_Awal")
                                JmlhBarang = Val(HilangkanTanda(.Rows(i).Item("Jumlah_Barang")))
                                Dim JmlhBags As Double = Val(HilangkanTanda(.Rows(i).Item("Jumlah_Bags")))
                                Dim SatuanBesar As String = .Rows(i).Item("satuan")
                                Dim SatuanKecil As String = .Rows(i).Item("Satuan_Barang")

                                Dim NmBarang As String = ""

                                '=======================
                                '=     GET FORMULA     =
                                '=======================
                                SQL = "select Id_Flever, Jumlah_Barang_Awal, Jumlah_Barang_Akhir "
                                SQL = SQL & "from EMI_Master_Flever "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Barang_Min = '" & KdBarangAwal & "' "
                                SQL = SQL & "and Kode_Barang_Plus = '" & KdBarangTujuan & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read() Then
                                        JumlahTambah = (Val(HilangkanTanda(Dr("Jumlah_Barang_Akhir"))) / Val(HilangkanTanda(Dr("Jumlah_Barang_Awal")))) * JmlhBarang
                                    End If
                                End Using

                                '========================
                                '=     POTONG STOCK     =
                                '========================
                                'POTONG STOCK BARANG
                                SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & SoAwal & "' "
                                SQL = SQL & "and Kode_Barang='" & KdBarangAwal & "' "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        NmBarang = dr("Kode_Barang")
                                        If dr("good_stock") < JmlhBarang Then
                                            dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & NmBarang & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                        ElseIf dr("Jumlah_Bags") < JmlhBags Then
                                            dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & NmBarang & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                        Else
                                            dr.Close()
                                            SQL = "update barang set Good_Stock = Good_Stock - Round(" & JmlhBarang & ",4), Jumlah_Bags = Jumlah_Bags - " & JmlhBags & " "
                                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & SoAwal & "' "
                                            SQL = SQL & " and Kode_Barang='" & KdBarangAwal & "'"
                                            ExecuteTrans(SQL)
                                        End If
                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang " & NmBarang & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                'POTONG STOCK BARANG_SN
                                SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & SoAwal & "' "
                                SQL = SQL & "and Kode_Barang='" & KdBarangAwal & "' "
                                SQL = SQL & "and Serial_Number='" & SN_Awal & "'"
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        If dr("jumlah") < JmlhBarang Then
                                            dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & NmBarang & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                        ElseIf dr("Jumlah_Bags") < JmlhBags Then
                                            dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & NmBarang & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                        Else
                                            dr.Close()
                                            SQL = "update barang_sn set jumlah = jumlah - Round(" & JmlhBarang & ",4), Jumlah_Bags = Jumlah_Bags - " & JmlhBags & " "
                                            SQL = SQL & "where Kode_Stock_Owner='" & SoAwal & "' and Kode_Barang='" & KdBarangAwal & "' "
                                            SQL = SQL & "and Serial_Number='" & SN_Awal & "'"
                                            ExecuteTrans(SQL)
                                        End If
                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang " & NmBarang & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                'CEK KESESUAIAN STOCK
                                SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & SoAwal & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & KdBarangAwal & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
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


                                '========================
                                '=     TAMBAH STOCK     =
                                '========================


                                'Ambil Data Lama
                                SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                                SQL = SQL & "from barang_sn a, barang b "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                                SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and a.Kode_Stock_Owner='" & SoAwal & "' "
                                SQL = SQL & "and a.Kode_Barang ='" & KdBarangAwal & "' "
                                SQL = SQL & "and a.Serial_Number='" & SN_Awal & "' "
                                'SQL = SQL & "and a.Jumlah <> 0 "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read() Then
                                        hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                                        QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                                        batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                                        namaBarang = General_Class.CekNULL(Dr("Nama"))
                                        expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                                        warnaLama = General_Class.CekNULL(Dr("warna"))

                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                harga_new = Math.Round((hargaIsn * JmlhBarang) / JumlahTambah, 0)


                                'GENERATE SN BARU
                                Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                SN_Baru = Kode_Unik & Tanda_SN & "01" & Tanda_SN & harga_new & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                'Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

                                NewQR = Generate_NewQR(KdBarangTujuan, batchLama)

                                'GET RAK DAN PALLET TUJUAN
                                'GET WAREHOUSE KOSONG
                                Dim Id_WarehouseTujuan, NoPalletTujuan As String
                                SQL = "SELECT TOP(1) "
                                SQL = SQL & "a.id_wms_warehouse_position, b.nomor_urut "
                                SQL = SQL & "FROM view_warehouse_position a, view_warehouse_position_detail b "
                                SQL = SQL & "WHERE a.Id_WMS_Warehouse_Position = b.Id_WMS_Warehouse_Position "
                                SQL = SQL & "AND a.kode_Perusahaan = b.kode_Perusahaan "
                                SQL = SQL & "AND a.kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "AND a.Kode_Stock_Owner = '" & SoAwal & "' "
                                SQL = SQL & "AND b.Kode_Barang IS NULL;"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Id_WarehouseTujuan = Dr("id_wms_warehouse_position") : id_WarehusTujuan = Dr("id_wms_warehouse_position")
                                        NoPalletTujuan = Dr("nomor_urut") : NoPalletTujuan = Dr("nomor_urut")

                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Pallet Kosong Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                'INSERT BARANG SN BARU  
                                SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk) "
                                SQL = SQL & "select Kode_Perusahaan, '" & SoAwal & "', '" & KdBarangTujuan & "', '" & SN_Baru & "', '" & JumlahTambah & "', " & JmlhBags & ", "
                                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & Id_WarehouseTujuan & "', id_Susunan , '" & NewQR & "', '" & newKodeUnikBerjalan & "', "
                                SQL = SQL & "Kode_Unik_Asal, '" & NoPalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk "
                                SQL = SQL & "from Barang_SN "
                                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner='" & SoAwal & "' "
                                SQL = SQL & "and Kode_Barang='" & KdBarangAwal & "' "
                                SQL = SQL & "and Serial_Number='" & SN_Awal & "' "
                                ExecuteTrans(SQL)

                                '============================
                                '=       TAMBAH STOCK       =
                                '============================

                                SQL = "update barang set Good_Stock= Good_Stock + Round(" & JumlahTambah & ",4), Jumlah_Bags = Jumlah_Bags + " & JmlhBags & " "
                                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & SoAwal & "' "
                                SQL = SQL & " and Kode_Barang='" & KdBarangTujuan & "'"
                                ExecuteTrans(SQL)

                                'CEK KESESUAIAN STOCK
                                SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & SoAwal & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & KdBarangTujuan & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
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





                                BerhasilInput = True

                            Next
                        Else

                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang  tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & SoAwal & "' "
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
                SQL = SQL & "and b.kode_stock_owner = '" & SoAwal & "' and b.Kode_Barang='" & KdBarangAwal & "' "
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
                SQL = SQL & "and b.kode_stock_owner = '" & SoAwal & "' and b.Kode_Barang='" & KdBarangTujuan & "' "
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


                Dim akun_Selisih_pembulatan As String = ""
                SQL = "select Hutang_Supplier, Hutang_Perjalanan, Hutang_PPN, PPN_Pembelian, Selisih_Pembulatan "
                SQL = SQL & "from stock_owner "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        akun_Selisih_pembulatan = Dr("Selisih_Pembulatan")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim Total_Min As Double = Math.Round(hargaIsn * JmlhBarang, 0)
                Dim Total_Plus As Double = Math.Round(harga_new * JumlahTambah, 0)

                Dim selisih_pembulatan As Double = Math.Round(Total_Plus - Total_Min, 0)
                Dim Kode_voucher As String = ""
                Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                Dim pagenumber As Integer = 1



                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_voucher & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & Kode_Transfer & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                'Jurnal Barang Baru
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                     Strings.Mid(akun_persediaan_tujuan, 2, 1),
                     Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                     KodePerusahaan, KodeProyek, "Persedian " & Kode_Transfer, Total_Plus, "0", pagenumber, SoAwal, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                'Jurnal Barang Lama
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                      Strings.Mid(akun_persediaan_dari, 2, 1),
                      Strings.Mid(Ganti(akun_persediaan_dari), 3),
                      KodePerusahaan, KodeProyek, "Persedian " & Kode_Transfer, "0", Total_Min, pagenumber, SoAwal, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                If selisih_pembulatan <> 0 Then

                    If selisih_pembulatan < 0 Then
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
                            Strings.Mid(akun_Selisih_pembulatan, 2, 1),
                            Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
                            KodePerusahaan, KodeProyek, "Selisih Pembulatan; " & Kode_Transfer, Math.Abs(selisih_pembulatan), "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1
                        Else
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
                            Strings.Mid(akun_Selisih_pembulatan, 2, 1),
                            Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
                            KodePerusahaan, KodeProyek, "Selisih Pembulatan; " & Kode_Transfer, "0", selisih_pembulatan, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                    End If
                End If




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

                If BerhasilInput Then
                    '==================
                    '=     UPDATE     =
                    '==================
                    SQL = "update EMI_TF_Material_To_Material_Detail set Serial_Number_Akhir = '" & SN_Baru & "', Id_Warehouse_Tujuan = '" & id_WarehusTujuan & "',  "
                    SQL = SQL & "Nomor_Pallet_Tujuan = '" & No_PalletTujuan & "', Flag_Validasi = 'Y', Jumlah_Akhir = '" & JumlahTambah & "', Kode_Voucher = '" & Kode_voucher & "', "
                    SQL = SQL & "Tanggal_Validasi='" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Validasi='" & Format(tgl_skg, "HH:mm:ss") & "', UserID_Validasi='" & UserID & "'"
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Transfer = '" & Kode_Transfer & "' and Serial_Number_Awal = '" & SerialNumberAwal & "'  "
                    ExecuteTrans(SQL)


                    '====================================
                    '=     CEK APAKAH SUDAH SELESAI     =
                    '====================================
                    SQL = "select Kode_Perusahaan from EMI_TF_Material_To_Material_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Transfer = '" & Kode_Transfer & "' and flag_validasi is null "
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            Dr.Close()
                            SQL = "update EMI_TF_Material_To_Material set Flag_Validasi = 'Y', Kode_Voucher = 'X' where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Transfer = '" & Kode_Transfer & "'"
                            ExecuteTrans(SQL)
                        End If
                    End Using
                End If

#End Region

            Next

            '=====================================
            '=       GENERATE BARCODE BARU       =
            '=====================================
            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
            Dim fullNewQr As String = NewQR & "-" & newKodeUnikBerjalan

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
            Dim metodePengeluaranStock As String = ""

            SQL = "select Metode_Pengeluaran_Stok from barang where Kode_Stock_Owner = '" & SoAwal & "' and Kode_Barang = '" & KdBarangTujuan & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    metodePengeluaranStock = Dr("Metode_Pengeluaran_Stok")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "delete from Cetak_MaterialtoMaterial where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            ExecuteTrans(SQL)

            SQL = "insert into Cetak_MaterialtoMaterial (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print,tanggal_masuk,metode_pengeluaran_stok) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & KdBarangTujuan & "', @newBarcode, '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
            SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' , "
            SQL = SQL & "'" & Format(tglMsk, "yyyy-MM-dd") & "', '" & metodePengeluaranStock & "' ) "
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

        Cetak_Barcode(kode_unik_print)

        MessageBox.Show("Data Berhasil DiValidasi", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Kosong()
    End Sub

    Private Sub Cetak_Barcode(ByVal kodeUnikPrint As String)
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = "BarcodeFG"

            Dim PrinterBarcode As String = "TSC TE210"

            SQL = "select Kode_Perusahaan from Cetak_MaterialtoMaterial where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & kodeUnikPrint & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New NewBarcodeTransferMaterial
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = "{Cetak_MaterialtoMaterial.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_MaterialtoMaterial.kode_unik_print} = '" & kodeUnikPrint & "' "
                        CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Material"
                        .Text = "New Barcode Transfer Material"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

                    '======================================================================================================================================================================================================================================================================

                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.RecordSelectionFormula = "{Cetak_MaterialtoMaterial.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_MaterialtoMaterial.kode_unik_print} = '" & kodeUnikPrint & "' "
                    'CrDoc.PrintOptions.PrinterName = PrinterBarcode
                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next
                    'CrDoc.PrintToPrinter(1, False, 1, 2500)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub




    Private Function Generate_NewQR(ByVal KodeBarang As String, ByVal BatchCode As String) As String

        'Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        'Dim UnixCode As New StringBuilder()

        'For i As Integer = 1 To 10
        '    Dim index As Integer = random.Next(0, chars.Length)
        '    UnixCode.Append(chars(index))
        'Next

        Dim Qr As String = ""
        Qr = KodeBarang & "-" & BatchCode

        Return Qr
    End Function

    Private Sub Txt_Barcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Barcode.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Barcode.Text.Trim.Length <> 0 Then
                Btn_Scan_Click(Me, Nothing)
            End If
        End If
    End Sub
End Class

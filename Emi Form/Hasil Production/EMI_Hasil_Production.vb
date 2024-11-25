Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Hasil_Production
    Dim Jenis = "Display_Production_Order"
    Public fno_po As String

    Dim LvKode_So As String
    Dim LvKode_Bahan As String
    Dim LvNama_Bahan As String
    Dim LvNilai_Formula As String
    Dim LvNilai_Produksi As String
    Dim LvSatuan As String

    Dim CellKode_So As Integer = 0
    Dim CellKode_Bahan As Integer = 1
    Dim CellNama_Bahan As Integer = 2
    Dim CellNilai_Formula As Integer = 3
    Dim CellNilai_Produksi As Integer = 4
    Dim CellSatuan As Integer = 5


    Dim LvKode_So_Pckg As String
    Dim LvKode_Bahan_Pckg As String
    Dim LvNama_Bahan_Pckg As String
    Dim LvNilai_Formula_Pckg As String
    Dim LvNilai_Produksi_Pckg As String
    Dim LvSatuan_Pckg As String

    Dim CellKode_So_Pckg As Integer = 0
    Dim CellKode_Bahan_Pckg As Integer = 1
    Dim CellNama_Bahan_Pckg As Integer = 2
    Dim CellNilai_Formula_Pckg As Integer = 3
    Dim CellNilai_Produksi_Pckg As Integer = 4
    Dim CellSatuan_Pckg As Integer = 5

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvKode_So = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_So).Value
        LvKode_Bahan = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_Bahan).Value
        LvNama_Bahan = Dgv_HslProduction.Rows(No_Index).Cells(CellNama_Bahan).Value
        LvNilai_Formula = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Formula).Value
        LvNilai_Produksi = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Produksi).Value
        LvSatuan = Dgv_HslProduction.Rows(No_Index).Cells(CellSatuan).Value

    End Sub

    Public Sub Get_Isi_Listview_Pckg(ByVal No_Index As Integer)

        LvKode_So_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_So_Pckg).Value
        LvKode_Bahan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_Bahan_Pckg).Value
        LvNama_Bahan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNama_Bahan_Pckg).Value
        LvNilai_Formula_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Formula_Pckg).Value
        LvNilai_Produksi_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Produksi_Pckg).Value
        LvSatuan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellSatuan_Pckg).Value

    End Sub

    Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Display_Production_Order_Judul2
            Label8.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi
            Label6.Text = Base_Language.Lang_Global_NoFaktur
            Label7.Text = Base_Language.Lang_Global_Tanggal_Produksi
            Label2.Text = Base_Language.Lang_Global_Jam
            Label10.Text = Base_Language.Lang_Global_NamaBarang
            Label9.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi2
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
            Dgv_HslProduction.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
            Dgv_HslProduction.Columns(3).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
            Dgv_HslProduction.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
            Dgv_HslProduction.Columns(5).HeaderText = Base_Language.Lang_Global_Satuan

            Dgv_Hasil_Production_Packaging.Columns(0).HeaderText = Base_Language.Lang_Global_Lokasi
            Dgv_Hasil_Production_Packaging.Columns(1).HeaderText = Base_Language.Lang_Global_Kode_Bahan
            Dgv_Hasil_Production_Packaging.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
            Dgv_Hasil_Production_Packaging.Columns(3).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
            Dgv_Hasil_Production_Packaging.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
            Dgv_Hasil_Production_Packaging.Columns(5).HeaderText = Base_Language.Lang_Global_Satuan

            Dgv_HslProduction.Rows.Clear()
            Dgv_Hasil_Production_Packaging.Rows.Clear()
            'SQL = "select c.Kode_Stock_Owner,c.Kode_Barang,d.Nama,c.Jumlah,c.Persentase,e.Satuan from "
            'SQL = SQL & "Emi_Order_Produksi_Detail a,Emi_Transaksi_Formulator b,EMI_Transaksi_Formulator_Detail_Bahan c,Barang d,Barang_Detail_Satuan e "
            'SQL = SQL & "where a.No_Formula = b.No_Faktur and b.Status is null and b.No_Faktur = c.No_Faktur and a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Barang = e.Kode_barang and e.Flag_Tampil_Display = 'Y' and "
            'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox4.Text & "' and a.Urut = '" & fUrut & "' "

            SQL = "select a.No_Transaksi, b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah,b.Satuan from  "
            SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Bahan b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & TextBox4.Text & "' "
            SQL = SQL & "order by c.nama"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_HslProduction.Rows.Add()
                            Dgv_HslProduction.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
                            Dgv_HslProduction.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_HslProduction.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
                            ' Dim nhasil As Double = 0
                            ' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
                            Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N2")
                            Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
                            Dgv_HslProduction.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")
                        Next
                    End If
                End With
            End Using

            SQL = "select a.No_Transaksi, b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah,b.Satuan from  "
            SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & TextBox4.Text & "' "
            SQL = SQL & "order by c.nama"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Hasil_Production_Packaging.Rows.Add()
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
                            ' Dim nhasil As Double = 0
                            ' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N2")
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")
                        Next
                    End If
                End With
            End Using

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
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

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFormulator_NoFaktur.Focus() : Exit Sub
        ElseIf TextBox5.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox5.Focus() : Exit Sub
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty2, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus() : Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty3, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus() : Exit Sub
        ElseIf Dgv_HslProduction.CurrentRow.Cells(CellNilai_Produksi).Value = "0" Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty4, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        get_jam()

        Try
            OpenConn()

            get_no_faktur()

            Dim Kd_So As String = ""
            Dim Kd_Brg As String = ""
            SQL = "Select b.Status,b.Selesai,b.Kode_Stock_Owner,b.Kode_Barang "
            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b "
            SQL = SQL & "where a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & TextBox4.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Kd_So = dr("Kode_Stock_Owner")
                    Kd_Brg = dr("Kode_Barang")
                    If General_Class.CekNULL(dr("Status")) <> "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_NoFaktur & " " & Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
                Get_Isi_Listview(a)
                If Val(HilangkanTanda(LvNilai_Produksi)) = 0 Then

                    CloseConn()
                    MessageBox.Show("nilai produksi untuk " & LvNama_Bahan & " di bahan baku harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next

            For a As Integer = 0 To Dgv_Hasil_Production_Packaging.Rows.Count - 1
                Get_Isi_Listview_Pckg(a)
                If Val(HilangkanTanda(LvNilai_Produksi_Pckg)) = 0 Then

                    CloseConn()
                    MessageBox.Show("nilai produksi untuk " & LvNama_Bahan_Pckg & " di packaging harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next

            Dim Rand As New Random
            Dim str As String = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
            Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
            Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & "10000" & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

            SQL = "INSERT INTO Emi_Production_Results(Kode_Perusahaan,No_Transaksi,No_Production_Order,Tanggal,Jam,UserID,"
            SQL = SQL & "Qty_Hasil_Produksi,Qty_Good_Stock,Qty_Bad_Stock,Serial_Number) VALUES('" & KodePerusahaan & "',"
            SQL = SQL & "'" & TxtFormulator_NoFaktur.Text & "','" & TextBox4.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "','" & HilangkanTanda(TextBox5.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TextBox7.Text) & "','" & HilangkanTanda(TextBox8.Text) & "','" & SN & "')"
            ExecuteTrans(SQL)


            '==========================================================
            '=     INSERT KE Emi_Production_Results_Detail_Pallet     =
            '==========================================================

            'GET ID_WAREHOUSE YG KOSONG

            Dim available_Id_Warehouse As String = ""
            Dim available_NoPallet As String = ""

            SQL = "select top(1) id_wms_warehouse_position, nomor_urut from view_warehouse_position_detail where kode_barang is null "
            Using Dr2 = OpenTrans(SQL)
                Do While Dr2.Read
                    available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                    available_NoPallet = Dr2("nomor_urut")
                Loop
            End Using

            'GET DATA DI Emi_Produksi_Hasil_Perpallet
            SQL = "select a.No_Split, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal, a.Qr_Code, a.Jumlah, a.Satuan, a.Batch_Number, a.Kode_Barang, a.ID as urut_oto "
            SQL = SQL & "from Emi_Produksi_Hasil_Perpallet a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Split='" & TextBox4.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            '====================================
                            '=       CONVERT SATUAN KECIL       =
                            '====================================
                            Dim nilai_kecildetail As Double = 0
                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa', '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Satuan") & "', "
                            SQL = SQL & "'Gram', '" & .Rows(i).Item("Jumlah") & "' ) as hasil"
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


                            SQL = "insert into Emi_Production_Results_Detail_Pallet (Kode_Perusahaan, No_Transaksi, Kode_Unik_Berjalan, Kode_Unik_Asal, Qr_Code, Jumlah, Satuan, NIlai_Barang, "
                            SQL = SQL & "Satuan_Barang, Batch_Number, Id_Warehouse, Nomor_Pallet, Urut_Oto) values "
                            SQL = SQL & "('" & KodePerusahaan & "', '" & TextBox4.Text & "', '" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', '" & nilai_kecildetail & "', 'Gram', "
                            SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', '" & .Rows(i).Item("urut_oto") & "') "
                            ExecuteTrans(SQL)

                        Next
                    End If
                End With
            End Using



            SQL = "Update barang set "
            SQL = SQL & "good_stock = good_stock + " & HilangkanTanda(TextBox7.Text) & " "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & Kd_Brg & "'"
            ExecuteTrans(SQL)

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

                    SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                    SQL = SQL & "serial_number, jumlah) values('" & KodePerusahaan & "', "
                    SQL = SQL & "'" & Kd_So & "', '" & Kd_Brg & "', "
                    SQL = SQL & "'" & SN & "', " & HilangkanTanda(TextBox7.Text) & ")"
                    Dr.Close()
                    ExecuteTrans(SQL)
                End If
            End Using

            For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
                Get_Isi_Listview(a)
                SQL = "INSERT INTO Emi_Production_Results_Detail(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan) "
                SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & LvKode_So & "','" & LvKode_Bahan & "',"
                SQL = SQL & "'" & HilangkanTanda(LvNilai_Formula) & "','" & HilangkanTanda(LvNilai_Produksi) & "','" & LvSatuan & "')"
                ExecuteTrans(SQL)

                SQL = "select round(good_stock,2) as good_stock, flag_ppn from barang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
                SQL = SQL & "kode_barang = '" & LvKode_Bahan & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            If .Rows(0).Item("good_stock") - HilangkanTanda(LvNilai_Produksi) < BolehNegatif Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses membuat stock menjadi negatif untuk barang " & LvNama_Bahan & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                SQL = "Update barang set good_stock = good_stock - " & HilangkanTanda(LvNilai_Produksi) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
                                SQL = SQL & "kode_barang = '" & LvKode_Bahan & "'"
                                ExecuteTrans(SQL)
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                            Exit Sub
                        End If
                    End With
                End Using

                Dim lewatin As String = "T"
                SQL = "select isnull(round(sum(jumlah),2), 0) as stock from barang_sn where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
                SQL = SQL & "kode_barang = '" & LvKode_Bahan & "' and jumlah <> 0 "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("stock") < Val(HilangkanTanda(LvNilai_Produksi)) Then
                            lewatin = "Y"
                        Else
                            lewatin = "T"
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & LvNama_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                If lewatin = "T" Then
                    Dim sisa As Double = 0
                    SQL = "select kode_stock_owner, kode_barang, serial_number, round(jumlah,2) as jumlah from barang_sn where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
                    SQL = SQL & "kode_barang = '" & LvKode_Bahan & "' and jumlah <> 0 "
                    SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                sisa = Val(HilangkanTanda(LvNilai_Produksi))
                                For h As Integer = 0 To .Rows.Count - 1
                                    If sisa = 0 Then
                                        Exit For
                                    ElseIf sisa < 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
                                        SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                        SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                        ExecuteTrans(SQL)

                                        SQL = "INSERT INTO Emi_Production_Results_det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                                        SQL = SQL & "Nilai,Serial_Number) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                        SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                        SQL = SQL & "" & sisa & ",'" & .Rows(h).Item("serial_number") & "')"
                                        ExecuteTrans(SQL)

                                        sisa = 0
                                    ElseIf sisa > .Rows(h).Item("jumlah") Then
                                        SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                        SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                        ExecuteTrans(SQL)

                                        SQL = "INSERT INTO Emi_Production_Results_det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                                        SQL = SQL & "Nilai,Serial_Number) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                        SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                        SQL = SQL & "" & .Rows(h).Item("jumlah") & ",'" & .Rows(h).Item("serial_number") & "')"
                                        ExecuteTrans(SQL)

                                        sisa = sisa - .Rows(h).Item("jumlah")

                                        If sisa <> 0 And h = .Rows.Count - 1 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Jumlah stock tidak mencukupi untuk barang " & LvNama_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & LvNama_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Next ' for barang sn
                            End If 'count <> 0
                        End With
                    End Using
                Else
                    MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & LvNama_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next


            ' awal buat packaging 

            For a As Integer = 0 To Dgv_Hasil_Production_Packaging.Rows.Count - 1
                Get_Isi_Listview_Pckg(a)
                SQL = "INSERT INTO Emi_Production_Results_Packaging_Detail(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan) "
                SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & LvKode_So_Pckg & "','" & LvKode_Bahan_Pckg & "',"
                SQL = SQL & "'" & HilangkanTanda(LvNilai_Formula_Pckg) & "','" & HilangkanTanda(LvNilai_Produksi_Pckg) & "','" & LvSatuan_Pckg & "')"
                ExecuteTrans(SQL)

                SQL = "select round(good_stock,2) as good_stock, flag_ppn from barang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & LvKode_So_Pckg & "' and "
                SQL = SQL & "kode_barang = '" & LvKode_Bahan_Pckg & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            If .Rows(0).Item("good_stock") - HilangkanTanda(LvNilai_Produksi_Pckg) < BolehNegatif Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses membuat stock menjadi negatif untuk barang " & LvNama_Bahan_Pckg & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                SQL = "Update barang set good_stock = good_stock - " & HilangkanTanda(LvNilai_Produksi_Pckg) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & LvKode_So_Pckg & "' and "
                                SQL = SQL & "kode_barang = '" & LvKode_Bahan_Pckg & "'"
                                ExecuteTrans(SQL)
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                            Exit Sub
                        End If
                    End With
                End Using

                Dim lewatin As String = "T"
                SQL = "select isnull(round(sum(jumlah),2), 0) as stock from barang_sn where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & LvKode_So_Pckg & "' and "
                SQL = SQL & "kode_barang = '" & LvKode_Bahan_Pckg & "' and jumlah <> 0 "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("stock") < Val(HilangkanTanda(LvNilai_Produksi_Pckg)) Then
                            lewatin = "Y"
                        Else
                            lewatin = "T"
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & LvNama_Bahan_Pckg & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                If lewatin = "T" Then
                    Dim sisa As Double = 0
                    SQL = "select kode_stock_owner, kode_barang, serial_number, round(jumlah,2) as jumlah from barang_sn where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & LvKode_So_Pckg & "' and "
                    SQL = SQL & "kode_barang = '" & LvKode_Bahan_Pckg & "' and jumlah <> 0 "
                    SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                sisa = Val(HilangkanTanda(LvNilai_Produksi_Pckg))
                                For h As Integer = 0 To .Rows.Count - 1
                                    If sisa = 0 Then
                                        Exit For
                                    ElseIf sisa < 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
                                        SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                        SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                        ExecuteTrans(SQL)

                                        SQL = "INSERT INTO Emi_Production_Results_Packaging_Det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                                        SQL = SQL & "Nilai,Serial_Number) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                        SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                        SQL = SQL & "" & sisa & ",'" & .Rows(h).Item("serial_number") & "')"
                                        ExecuteTrans(SQL)

                                        sisa = 0
                                    ElseIf sisa > .Rows(h).Item("jumlah") Then
                                        SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                        SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                        ExecuteTrans(SQL)

                                        SQL = "INSERT INTO Emi_Production_Results_Packaging_Det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                                        SQL = SQL & "Nilai,Serial_Number) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                        SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                        SQL = SQL & "" & .Rows(h).Item("jumlah") & ",'" & .Rows(h).Item("serial_number") & "')"
                                        ExecuteTrans(SQL)

                                        sisa = sisa - .Rows(h).Item("jumlah")

                                        If sisa <> 0 And h = .Rows.Count - 1 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Jumlah stock tidak mencukupi untuk barang " & LvNama_Bahan_Pckg & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & LvNama_Bahan_Pckg & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Next ' for barang sn
                            End If 'count <> 0
                        End With
                    End Using
                Else
                    MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & LvNama_Bahan_Pckg & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next


            'akhri packaging

            SQL = "update Emi_Split_Production_Order set Flag_Hasil_Produksi = 'Y',Tgl_Hasil_Produksi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            SQL = SQL & "Jam_Hasil_Produksi = '" & Format(tgl_skg, "HH:mm:ss") & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Transaksi = '" & TextBox4.Text & "' "
            ExecuteTrans(SQL)

            SQL = "select b.Jumlah as jml_po,"
            SQL = SQL & "ISNULL((select SUM(a.Jumlah) from Emi_Split_Production_Order a where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Flag_Produksi = 'Y' and a.Flag_Selesai_Produksi = 'Y' "
            SQL = SQL & "and a.Flag_Hasil_Produksi ='Y'),0) as jml_split "
            SQL = SQL & "from EMI_Order_Produksi b where b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & fno_po & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("jml_po") = Dr("jml_split") Then
                        Dr.Close()
                        SQL = "Update EMI_Order_Produksi set Selesai = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and No_Faktur = '" & fno_po & "' "
                        ExecuteTrans(SQL)
                    End If
                End If
            End Using










            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_Display_Hasil_Produksi.Button1_Click(Btn_Simpan, e)
        Me.Close()
    End Sub

    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Dgv_HslProduction_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_HslProduction.CellEndEdit
        Get_Isi_Listview(Dgv_HslProduction.CurrentRow.Index)
        If IsNumeric(LvNilai_Produksi) = False Or Val(LvNilai_Produksi) < 0 Then
            Dgv_HslProduction.CurrentRow.Cells(CellNilai_Produksi).Value = 0
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If TextBox5.Text.Trim.Length = 0 Then
            Exit Sub
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            Exit Sub
        End If
        Dim a As Double = 0
        a = Val(HilangkanTanda(TextBox5.Text)) - Val(HilangkanTanda(TextBox8.Text))
        TextBox7.Text = Format(a, "N2")
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox8.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Chr(13) Then Dgv_HslProduction.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub



End Class
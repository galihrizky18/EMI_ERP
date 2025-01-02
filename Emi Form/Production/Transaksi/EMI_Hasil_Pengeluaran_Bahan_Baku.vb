Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Org.BouncyCastle.Asn1

Public Class EMI_Hasil_Pengeluaran_Bahan_Baku
    Dim Jenis = "Display_Production_Order"
    Public fno_po, fso As String

    Dim LvKode_So As String
    Dim LvKode_Bahan As String
    Dim LvNilai_Formula As String
    Dim LvNilai_Produksi As String
    Dim LvSatuan As String
    Dim LvPotStokBhn As String
    Dim LvStandarPrice As String

    Dim CellKode_So As Integer = 0
    Dim CellKode_Bahan As Integer = 1
    '   Dim CellNama_Bahan As Integer = 2
    Dim CellNilai_Formula As Integer = 2
    Dim CellNilai_Produksi As Integer = 3
    Dim CellSatuan As Integer = 4
    Dim CellPotStokBhn As Integer = 5
    Dim CellStandarPrice As Integer = 6


    Dim LvKode_So_Pckg As String
    Dim LvKode_Bahan_Pckg As String
    '  Dim LvNama_Bahan_Pckg As String
    Dim LvNilai_Formula_Pckg As String
    Dim LvNilai_Produksi_Pckg As String
    Dim LvSatuan_Pckg As String
    Dim LvPotStokPckg As String
    Dim LvStandarPricePckg As String

    Dim CellKode_So_Pckg As Integer = 0
    Dim CellKode_Bahan_Pckg As Integer = 1
    ' Dim CellNama_Bahan_Pckg As Integer = 2
    Dim CellNilai_Formula_Pckg As Integer = 2
    Dim CellNilai_Produksi_Pckg As Integer = 3
    Dim CellSatuan_Pckg As Integer = 4
    Dim CellPotStokPckg As Integer = 5
    Dim CellStandarPricePckg As Integer = 6

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvKode_So = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_So).Value
        LvKode_Bahan = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_Bahan).Value
        LvNilai_Formula = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Formula).Value
        LvNilai_Produksi = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Produksi).Value
        LvSatuan = Dgv_HslProduction.Rows(No_Index).Cells(CellSatuan).Value
        LvPotStokBhn = Dgv_HslProduction.Rows(No_Index).Cells(CellPotStokBhn).Value
        LvStandarPrice = Dgv_HslProduction.Rows(No_Index).Cells(CellStandarPrice).Value
    End Sub

    Public Sub Get_Isi_Listview_Pckg(ByVal No_Index As Integer)

        LvKode_So_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_So_Pckg).Value
        LvKode_Bahan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_Bahan_Pckg).Value

        LvNilai_Formula_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Formula_Pckg).Value
        LvNilai_Produksi_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Produksi_Pckg).Value
        LvSatuan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellSatuan_Pckg).Value
        LvPotStokPckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellPotStokPckg).Value
        LvStandarPricePckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellStandarPricePckg).Value
    End Sub

    Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = "Transaksi - Pengeluaran Bahan Baku"
            ' Label8.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi
            Label6.Text = Base_Language.Lang_Global_NoFaktur
            Label7.Text = Base_Language.Lang_Global_Tanggal_Produksi
            Label2.Text = Base_Language.Lang_Global_Jam
            Label10.Text = Base_Language.Lang_Global_NamaBarang
            '  Label9.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi2
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
            ' Dgv_HslProduction.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
            Dgv_HslProduction.Columns(2).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
            'Dgv_HslProduction.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
            Dgv_HslProduction.Columns(4).HeaderText = Base_Language.Lang_Global_Satuan

            Dgv_Hasil_Production_Packaging.Columns(0).HeaderText = Base_Language.Lang_Global_Lokasi
            Dgv_Hasil_Production_Packaging.Columns(1).HeaderText = Base_Language.Lang_Global_Kode_Bahan
            ' Dgv_Hasil_Production_Packaging.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
            Dgv_Hasil_Production_Packaging.Columns(2).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
            'Dgv_Hasil_Production_Packaging.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
            Dgv_Hasil_Production_Packaging.Columns(4).HeaderText = Base_Language.Lang_Global_Satuan

            Dgv_HslProduction.Rows.Clear()
            Dgv_Hasil_Production_Packaging.Rows.Clear()
            'SQL = "select c.Kode_Stock_Owner,c.Kode_Barang,d.Nama,c.Jumlah,c.Persentase,e.Satuan from "
            'SQL = SQL & "Emi_Order_Produksi_Detail a,Emi_Transaksi_Formulator b,EMI_Transaksi_Formulator_Detail_Bahan c,Barang d,Barang_Detail_Satuan e "
            'SQL = SQL & "where a.No_Formula = b.No_Faktur and b.Status is null and b.No_Faktur = c.No_Faktur and a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Barang = e.Kode_barang and e.Flag_Tampil_Display = 'Y' and "
            'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox4.Text & "' and a.Urut = '" & fUrut & "' "

            SQL = "select a.No_Transaksi, b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah,b.Satuan, c.flag_potong_stok, c.standar_price from  "
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
                            ' Dgv_HslProduction.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
                            ' Dim nhasil As Double = 0
                            ' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
                            Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N2")
                            Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
                            Dgv_HslProduction.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")

                            If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
                                Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = ""
                            Else
                                Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = .Rows(i).Item("flag_potong_stok")
                            End If

                            Dgv_HslProduction.Rows.Item(i).Cells(CellStandarPrice).Value = .Rows(i).Item("standar_price")

                        Next
                    End If
                End With
            End Using

            SQL = "select a.No_Transaksi, b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah,b.Satuan, c.flag_potong_stok,c.standar_price from  "
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
                            'Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
                            ' Dim nhasil As Double = 0
                            ' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N2")
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")

                            If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
                                Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellPotStokPckg).Value = ""
                            Else
                                Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellPotStokPckg).Value = .Rows(i).Item("flag_potong_stok")
                            End If

                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellStandarPricePckg).Value = .Rows(i).Item("standar_price")
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
            'ElseIf TextBox5.Text.Trim.Length = 0 Then
            '    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox5.Focus() : Exit Sub
            'ElseIf TextBox8.Text.Trim.Length = 0 Then
            '    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty2, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox8.Focus() : Exit Sub
            'ElseIf TextBox7.Text.Trim.Length = 0 Then
            '    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty3, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox7.Focus() : Exit Sub
            'ElseIf Dgv_HslProduction.CurrentRow.Cells(CellNilai_Produksi).Value = "0" Then
            '    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty4, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
        End If
        get_jam()

        Try
            OpenConn()

            '   get_no_faktur()
            Cmd.Transaction = Cn.BeginTransaction

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

            'For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
            '    Get_Isi_Listview(a)
            '    If Val(HilangkanTanda(LvNilai_Produksi)) = 0 Then

            '        CloseConn()
            '        MessageBox.Show("nilai produksi untuk " & LvNama_Bahan & " di bahan baku harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'Next

            'For a As Integer = 0 To Dgv_Hasil_Production_Packaging.Rows.Count - 1
            '    Get_Isi_Listview_Pckg(a)
            '    If Val(HilangkanTanda(LvNilai_Produksi_Pckg)) = 0 Then

            '        CloseConn()
            '        MessageBox.Show("nilai produksi untuk " & LvNama_Bahan_Pckg & " di packaging harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'Next


            ' cek apakah no produksi ini sudah pernah di generate sebelumnya


            Dim proses As Integer
            SQL = "select no_transaksi,"
            SQL = SQL & "isnull((select top(1) proses from Emi_Production_Results_Detail x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = x.No_Transaksi order by proses desc "
            SQL = SQL & "),0) as proses "
            SQL = SQL & "from Emi_Production_Results a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Orderr = '" & TextBox4.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtFormulator_NoFaktur.Text = Dr("no_transaksi")
                    proses = Dr("proses") + 1
                Else
                    Dr.Close()

                    get_no_faktur()

                    SQL = "INSERT INTO Emi_Production_Results(Kode_Perusahaan,No_Transaksi,No_Production_Order,Tanggal,Jam,UserID"
                    SQL = SQL & ",no_production_orderr) VALUES('" & KodePerusahaan & "',"
                    SQL = SQL & "'" & TxtFormulator_NoFaktur.Text & "','" & TextBox4.Text.Trim & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "', '" & TextBox4.Text.Trim & "')"
                    ExecuteTrans(SQL)
                    proses = 1
                End If
            End Using


            ' awal cek untuk bahan
            Dim Nilai_Bahan As Double = 0
            For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
                Get_Isi_Listview(a)


                If LvNilai_Produksi > 0 Then

                    '======                              =========='
                    '======   Awal convert satuan barang =========='
                    '=========                           =========='

                    Dim convertKeSatuanAsli_bhn As String = ""
                    Dim jumlahConvertBhn As Double = 0

                    SQL = "select satuan From barang where Kode_barang = '" & LvKode_Bahan & "' "
                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & LvKode_So & "' "
                    Using Dr3 = OpenTrans(SQL)
                        If Dr3.Read Then
                            convertKeSatuanAsli_bhn = Dr3("satuan")
                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvKode_Bahan & "',"
                            SQL = SQL & "'" & LvSatuan & "','" & Dr3("satuan") & "',"
                            SQL = SQL & "" & HilangkanTanda(LvNilai_Produksi) & ") as Hasil "
                            Dr3.Close()

                            Using dr4 = OpenTrans(SQL)
                                If dr4.Read Then
                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                        If dr4("Hasil") = 0 Then
                                            dr4.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            jumlahConvertBhn = dr4("hasil")

                                        End If
                                    Else
                                        dr4.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using
                        Else
                            Dr3.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    '======                              =========='
                    '======   Akhir convert satuan barang =========='
                    '=========                           =========='

                    SQL = "INSERT INTO Emi_Production_Results_Detail(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan,proses,"
                    SQL = SQL & "nilai_barang,satuan_barang,userid,tanggal,jam ) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & LvKode_So & "','" & LvKode_Bahan & "',"
                    SQL = SQL & "'" & HilangkanTanda(LvNilai_Formula) & "','" & HilangkanTanda(LvNilai_Produksi) & "','" & LvSatuan & "' , '" & proses & "', "
                    SQL = SQL & "'" & jumlahConvertBhn & "', '" & convertKeSatuanAsli_bhn & "', '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "'"
                    SQL = SQL & ")"
                    ExecuteTrans(SQL)

                    Dim x_ident_currentBahan As Integer = 0
                    SQL = "select IDENT_CURRENT('Emi_Production_Results_Detail') as urutan"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            x_ident_currentBahan = Dr("urutan")
                        End If
                    End Using



                    SQL = "select round(good_stock,2) as good_stock, flag_ppn from barang where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
                    SQL = SQL & "kode_barang = '" & LvKode_Bahan & "'"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then

                                If LvPotStokBhn = "Y" Then
                                    If .Rows(0).Item("good_stock") - jumlahConvertBhn < BolehNegatif Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses membuat stock menjadi negatif untuk kode barang " & LvKode_Bahan & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        SQL = "Update barang set good_stock = good_stock - " & jumlahConvertBhn & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
                                        SQL = SQL & "kode_barang = '" & LvKode_Bahan & "'"
                                        ExecuteTrans(SQL)
                                    End If
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
                            If Dr("stock") < Val(jumlahConvertBhn) Then
                                lewatin = "Y"
                            Else
                                lewatin = "T"
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    '==================================================================================
                    '======================  CHECK APAKAH FLAG POTONG STOK NYA Y atau T ================
                    '==================================================================================
                    If LvPotStokBhn = "Y" Then
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
                                        sisa = Val(jumlahConvertBhn)
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
                                                SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                                SQL = SQL & "" & sisa & ",'" & .Rows(h).Item("serial_number") & "', '" & x_ident_currentBahan & "')"
                                                ExecuteTrans(SQL)

                                                Nilai_Bahan = Nilai_Bahan + (Get_Harga_SN(.Rows(h).Item("serial_number")) * sisa)
                                                sisa = 0
                                            ElseIf sisa > .Rows(h).Item("jumlah") Then
                                                SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                                SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                                SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                                ExecuteTrans(SQL)

                                                SQL = "INSERT INTO Emi_Production_Results_det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                                                SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                                SQL = SQL & "" & .Rows(h).Item("jumlah") & ",'" & .Rows(h).Item("serial_number") & "', '" & x_ident_currentBahan & "')"
                                                ExecuteTrans(SQL)

                                                Nilai_Bahan = Nilai_Bahan + (Get_Harga_SN(.Rows(h).Item("serial_number")) * .Rows(h).Item("jumlah"))
                                                sisa = sisa - .Rows(h).Item("jumlah")


                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                            If sisa <> 0 And h = .Rows.Count - 1 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                        Next ' for barang sn
                                    End If 'count <> 0
                                End With
                            End Using
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else

                        SQL = "INSERT INTO Emi_Production_Results_det2(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                            SQL = SQL & "Nilai,harga,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                            SQL = SQL & "'" & LvKode_So & "','" & LvKode_Bahan & "',"
                            SQL = SQL & "" & jumlahConvertBhn & ",'" & LvStandarPrice & "', '" & x_ident_currentBahan & "')"
                            ExecuteTrans(SQL)

                    End If


                End If


                'batas akhir if cek lvnilai_produksi > 0
            Next

            '''' akhir cek untuk bahan


            ' awal buat packaging 

            Dim Nilai_Packaging As Double = 0

            For a As Integer = 0 To Dgv_Hasil_Production_Packaging.Rows.Count - 1
                Get_Isi_Listview_Pckg(a)

                If LvNilai_Produksi_Pckg > 0 Then


                    '======                              =========='
                    '======   Awal convert satuan barang =========='
                    '=========                           =========='

                    Dim convertKeSatuanAsli_pckg As String = ""
                    Dim jumlahConvertPckg As Double = 0

                    SQL = "select satuan From barang where Kode_barang = '" & LvKode_Bahan_Pckg & "' "
                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & LvKode_So_Pckg & "' "
                    Using Dr3 = OpenTrans(SQL)
                        If Dr3.Read Then
                            convertKeSatuanAsli_pckg = Dr3("satuan")
                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvKode_Bahan_Pckg & "',"
                            SQL = SQL & "'" & LvSatuan_Pckg & "','" & Dr3("satuan") & "',"
                            SQL = SQL & "" & HilangkanTanda(LvNilai_Produksi_Pckg) & ") as Hasil "
                            Dr3.Close()

                            Using dr4 = OpenTrans(SQL)
                                If dr4.Read Then
                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                        If dr4("Hasil") = 0 Then
                                            dr4.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Satuan " & LvSatuan_Pckg & " Ke " & convertKeSatuanAsli_pckg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            jumlahConvertPckg = dr4("hasil")

                                        End If
                                    Else
                                        dr4.Close()
                                        CloseTrans()
                                        CloseConn()

                                        MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_pckg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using
                        Else
                            Dr3.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    '======                              =========='
                    '======   Akhir convert satuan barang =========='
                    '=========                           =========='


                    SQL = "INSERT INTO Emi_Production_Results_Packaging_Detail(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan,proses,"
                    SQL = SQL & "nilai_barang,satuan_barang,userid,tanggal,jam) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & LvKode_So_Pckg & "','" & LvKode_Bahan_Pckg & "',"
                    SQL = SQL & "'" & HilangkanTanda(LvNilai_Formula_Pckg) & "','" & HilangkanTanda(LvNilai_Produksi_Pckg) & "','" & LvSatuan_Pckg & "', '" & proses & "',"
                    SQL = SQL & "'" & jumlahConvertPckg & "', '" & convertKeSatuanAsli_pckg & "', '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "'"
                    SQL = SQL & ") "
                    ExecuteTrans(SQL)

                    Dim x_ident_currentPackaging As Integer = 0
                    SQL = "select IDENT_CURRENT('Emi_Production_Results_Packaging_Detail') as urutan"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            x_ident_currentPackaging = Dr("urutan")
                        End If
                    End Using

                    SQL = "select round(good_stock,2) as good_stock, flag_ppn from barang where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & LvKode_So_Pckg & "' and "
                    SQL = SQL & "kode_barang = '" & LvKode_Bahan_Pckg & "'"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                If .Rows(0).Item("good_stock") - jumlahConvertPckg < BolehNegatif Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Proses membuat stock menjadi negatif untuk kode barang " & LvKode_Bahan_Pckg & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                Else
                                    SQL = "Update barang set good_stock = good_stock - " & jumlahConvertPckg & " where "
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
                            If Dr("stock") < Val(jumlahConvertPckg) Then
                                lewatin = "Y"
                            Else
                                lewatin = "T"
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan_Pckg & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    '==================================================================================
                    '======================  CHECK APAKAH FLAG POTONG STOK NYA Y atau T ================
                    '==================================================================================
                    If LvPotStokPckg = "Y" Then
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
                                        sisa = Val(jumlahConvertPckg)
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
                                                SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                                SQL = SQL & "" & sisa & ",'" & .Rows(h).Item("serial_number") & "', '" & x_ident_currentPackaging & "')"
                                                ExecuteTrans(SQL)

                                                Nilai_Packaging = Nilai_Packaging + (Get_Harga_SN(.Rows(h).Item("serial_number")) * sisa)
                                                sisa = 0
                                            ElseIf sisa > .Rows(h).Item("jumlah") Then
                                                SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                                SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                                SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                                ExecuteTrans(SQL)

                                                SQL = "INSERT INTO Emi_Production_Results_Packaging_Det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                                                SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                                SQL = SQL & "" & .Rows(h).Item("jumlah") & ",'" & .Rows(h).Item("serial_number") & "', '" & x_ident_currentPackaging & "')"
                                                ExecuteTrans(SQL)

                                                Nilai_Packaging = Nilai_Packaging + (Get_Harga_SN(.Rows(h).Item("serial_number")) * .Rows(h).Item("jumlah"))
                                                sisa = sisa - .Rows(h).Item("jumlah")


                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan_Pckg & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                            If sisa <> 0 And h = .Rows.Count - 1 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & LvKode_Bahan_Pckg & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        Next ' for barang sn
                                    End If 'count <> 0
                                End With
                            End Using
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan_Pckg & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else

                        SQL = "INSERT INTO Emi_Production_Results_Packaging_Det2(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                        SQL = SQL & "Nilai,harga,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                        SQL = SQL & "'" & LvKode_So_Pckg & "','" & LvKode_Bahan_Pckg & "',"
                        SQL = SQL & "" & jumlahConvertPckg & ",'" & LvStandarPricePckg & "', '" & x_ident_currentPackaging & "')"
                        ExecuteTrans(SQL)
                    End If


                    'akhir lewatin
                End If


                'akhir if lvnilai_produksi_pckg > 0
            Next

            'akhri packaging

            Dim fbulan As String = Format(tgl_skg, "MM")
            Dim ftahun As String = Format(tgl_skg, "yyyy")

            Dim nilai_Produksi As Double = 0
            SQL = "select isnull(SUM(b.Nilai_Per_Pcs),0) as nilai from Emi_Transaksi_Cost_Center a,Emi_Transaksi_Cost_Center_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & fbulan & "' and a.Tahun = '" & ftahun & "' "
            SQL = SQL & "and  b.Kode_Barang = '" & Kd_Brg & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    nilai_Produksi = dr("nilai")
                End If
            End Using




            'awal stenly jurnal

            Dim inisial_faktur_dari As String = ""

            SQL = "select b.Inisial_Faktur,a.Kode_Stock_Owner from Emi_Split_Production_Order a,Stock_Owner_Gudang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & TextBox4.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    inisial_faktur_dari = Dr("inisial_faktur")
                    fso = Dr("Kode_Stock_Owner")
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
            SQL = SQL & "'" & KodeProyek & "', 'Pengeluaran Bahan Baku " & TxtFormulator_NoFaktur.Text & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            Dim ftotal As Double = 0

            Dim fFlag_Raw_Material As String = ""
            Dim fFlag_Finished_Good As String = ""
            Dim fFlag_Semi_FG As String = ""
            Dim fFlag_Scrap As String = ""
            Dim fFlag_Packaging As String = ""

            Dim ket As String = ""

            Dim akun_kredit As String = ""
            SQL = "select sum(round(dbo.get_hpp(a.Serial_Number) * a.Nilai , 2)) as hpp,b.Id_Group_Jenis,"
            SQL = SQL & "c.Flag_Raw_Material,c.Flag_Finished_Good,c.Flag_Semi_FG,c.Flag_Scrap,c.Flag_Packaging,e.Proses "
            SQL = SQL & "from Emi_Production_Results_det a, Barang b, EMI_Group_Jenis c,Emi_Production_Results_Detail e where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Transaksi "
            SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.No_Urut_Detail = e.Urut "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
            SQL = SQL & "and e.Proses = '" & proses & "' "
            SQL = SQL & "group by b.Id_Group_Jenis,c.Flag_Raw_Material,c.Flag_Finished_Good,c.Flag_Semi_FG,c.Flag_Scrap,c.Flag_Packaging,e.Proses "
            SQL = SQL & "order by b.Id_Group_Jenis "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1
                            ftotal = ftotal + .Rows(h).Item("hpp")

                            fFlag_Raw_Material = .Rows(h).Item("Flag_Raw_Material")
                            fFlag_Finished_Good = .Rows(h).Item("Flag_Finished_Good")
                            fFlag_Semi_FG = .Rows(h).Item("Flag_Semi_FG")
                            fFlag_Scrap = .Rows(h).Item("Flag_Scrap")
                            fFlag_Packaging = .Rows(h).Item("Flag_Packaging")

                            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,"
                            SQL = SQL & "Persediaan_Scrap,Persediaan_Packaging from stock_owner_gudang "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    'akun_persediaan_dari = Dr("persediaan")
                                    If fFlag_Raw_Material = "Y" Then
                                        akun_kredit = Dr("Persediaan_Bahan_Baku")
                                        ket = "Persediaan Bahan Baku "
                                    ElseIf fFlag_Finished_Good = "Y" Then
                                        akun_kredit = Dr("Persediaan")
                                        ket = "Persediaan "
                                    ElseIf fFlag_Semi_FG = "Y" Then
                                        akun_kredit = Dr("Persediaan_Bahan_Setengah_Jadi")
                                        ket = "Persediaan Bahan Setengah Jadi "
                                    ElseIf fFlag_Scrap = "Y" Then
                                        akun_kredit = Dr("Persediaan_Scrap")
                                        ket = "Persediaan Scrap "
                                    ElseIf fFlag_Packaging = "Y" Then
                                        akun_kredit = Dr("Persediaan_Packaging")
                                        ket = "Persediaan Packaging "
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
                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit, 1),
                                  Strings.Mid(akun_kredit, 2, 1),
                                  Strings.Mid(Ganti(akun_kredit), 3),
                                  KodePerusahaan, KodeProyek, ket & TxtFormulator_NoFaktur.Text, "0", .Rows(h).Item("hpp"), pagenumber, "TSSS")
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1
                        Next
                    End If
                End With
            End Using

            SQL = "select sum(round(dbo.get_hpp(a.Serial_Number) * a.Nilai , 2)) as hpp,b.Id_Group_Jenis,"
            SQL = SQL & "c.Flag_Raw_Material,c.Flag_Finished_Good,c.Flag_Semi_FG,c.Flag_Scrap,c.Flag_Packaging,e.Proses "
            SQL = SQL & "from Emi_Production_Results_Packaging_Det a, Barang b, EMI_Group_Jenis c,Emi_Production_Results_Packaging_Detail e where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Transaksi "
            SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.No_Urut_Detail = e.Urut "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
            SQL = SQL & "and e.Proses = '" & proses & "' "
            SQL = SQL & "group by b.Id_Group_Jenis,c.Flag_Raw_Material,c.Flag_Finished_Good,c.Flag_Semi_FG,c.Flag_Scrap,c.Flag_Packaging,e.Proses "
            SQL = SQL & "order by b.Id_Group_Jenis "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1
                            ftotal = ftotal + .Rows(h).Item("hpp")

                            fFlag_Raw_Material = .Rows(h).Item("Flag_Raw_Material")
                            fFlag_Finished_Good = .Rows(h).Item("Flag_Finished_Good")
                            fFlag_Semi_FG = .Rows(h).Item("Flag_Semi_FG")
                            fFlag_Scrap = .Rows(h).Item("Flag_Scrap")
                            fFlag_Packaging = .Rows(h).Item("Flag_Packaging")

                            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,"
                            SQL = SQL & "Persediaan_Scrap,Persediaan_Packaging from stock_owner_gudang "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    'akun_persediaan_dari = Dr("persediaan")
                                    If fFlag_Raw_Material = "Y" Then
                                        akun_kredit = Dr("Persediaan_Bahan_Baku")
                                        ket = "Persediaan Bahan Baku "
                                    ElseIf fFlag_Finished_Good = "Y" Then
                                        akun_kredit = Dr("Persediaan")
                                        ket = "Persediaan "
                                    ElseIf fFlag_Semi_FG = "Y" Then
                                        akun_kredit = Dr("Persediaan_Bahan_Setengah_Jadi")
                                        ket = "Persediaan Bahan Setengah Jadi "
                                    ElseIf fFlag_Scrap = "Y" Then
                                        akun_kredit = Dr("Persediaan_Scrap")
                                        ket = "Persediaan Scrap "
                                    ElseIf fFlag_Packaging = "Y" Then
                                        akun_kredit = Dr("Persediaan_Packaging")
                                        ket = "Persediaan Packaging "
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
                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit, 1),
                                  Strings.Mid(akun_kredit, 2, 1),
                                  Strings.Mid(Ganti(akun_kredit), 3),
                                  KodePerusahaan, KodeProyek, ket & TxtFormulator_NoFaktur.Text, "0", .Rows(h).Item("hpp"), pagenumber, "TSSS")
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1
                        Next
                    End If
                End With
            End Using

            If ftotal = 0 Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("tidak ada data yang di jurnal...!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim akun_debit As String = ""

            SQL = "select Persediaan_Barang_Dalam_Proses from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_debit = Dr("Persediaan_Barang_Dalam_Proses")
                    ket = "Persediaan Barang Dalam Proses "
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_debit, 1),
                     Strings.Mid(akun_debit, 2, 1),
                     Strings.Mid(Ganti(akun_debit), 3),
                     KodePerusahaan, KodeProyek, ket & TxtFormulator_NoFaktur.Text, ftotal, "0", pagenumber, "TSSS")
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

            SQL = "insert into Emi_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses) values ("
            SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher & "',"
            SQL = SQL & "'" & proses & "') "
            ExecuteTrans(SQL)

            'akhir stenly jurnal



            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_Display_Pengeluaran_Bahan_Baku.Button1_Click(Btn_Simpan, e)
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

    'Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
    '    If TextBox5.Text.Trim.Length = 0 Then
    '        Exit Sub
    '    ElseIf TextBox8.Text.Trim.Length = 0 Then
    '        Exit Sub
    '    End If
    '    Dim a As Double = 0
    '    a = Val(HilangkanTanda(TextBox5.Text)) - Val(HilangkanTanda(TextBox8.Text))
    '    TextBox7.Text = Format(a, "N2")
    'End Sub

    'Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
    '    If e.KeyChar = Chr(13) Then TextBox8.Focus()
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    'End Sub

    'Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
    '    If e.KeyChar = Chr(13) Then Dgv_HslProduction.Focus()
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    'End Sub



End Class
Public Class EMI_Hasil_ProductionGR
    Dim CrDoc As Object
    Dim Jenis = "Display_Production_Order"
    Public fno_po As String

    Dim LvKode_So As String
    Dim LvKode_Bahan As String
    'Dim LvNama_Bahan As String
    Dim LvNilai_Formula As String
    Dim LvNilai_Produksi As String
    Dim LvSatuan As String

    Dim CellKode_So As Integer = 0
    Dim CellKode_Bahan As Integer = 1
    'Dim CellNama_Bahan As Integer = 2
    Dim CellNilai_Formula As Integer = 2
    Dim CellNilai_Produksi As Integer = 3
    Dim CellSatuan As Integer = 4

    Dim LvKode_So_Pckg As String
    Dim LvKode_Bahan_Pckg As String
    'Dim LvNama_Bahan_Pckg As String
    Dim LvNilai_Formula_Pckg As String
    Dim LvNilai_Produksi_Pckg As String
    Dim LvSatuan_Pckg As String

    Dim CellKode_So_Pckg As Integer = 0
    Dim CellKode_Bahan_Pckg As Integer = 1
    'Dim CellNama_Bahan_Pckg As Integer = 2
    Dim CellNilai_Formula_Pckg As Integer = 2
    Dim CellNilai_Produksi_Pckg As Integer = 3
    Dim CellSatuan_Pckg As Integer = 4


    Public Sub Get_Isi_Listview_Pckg(ByVal No_Index As Integer)

        LvKode_So_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_So_Pckg).Value
        LvKode_Bahan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_Bahan_Pckg).Value
        'LvNama_Bahan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNama_Bahan_Pckg).Value
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

            Label1.Text = "Transaksi - Hasil Produksi"
            Label8.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi
            Label6.Text = Base_Language.Lang_Global_NoFaktur
            Label7.Text = Base_Language.Lang_Global_Tanggal_Produksi
            Label2.Text = Base_Language.Lang_Global_Jam
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



            Dgv_Hasil_Production_Packaging.Columns(CellKode_So_Pckg).HeaderText = Base_Language.Lang_Global_Lokasi
            Dgv_Hasil_Production_Packaging.Columns(CellKode_Bahan_Pckg).HeaderText = Base_Language.Lang_Global_Kode_Bahan
            ''Dgv_Hasil_Production_Packaging.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
            Dgv_Hasil_Production_Packaging.Columns(CellNilai_Formula_Pckg).HeaderText = "Nilai Formula"
            Dgv_Hasil_Production_Packaging.Columns(CellNilai_Produksi_Pckg).HeaderText = "Nilai Aktual"
            'Dgv_Hasil_Production_Packaging.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
            Dgv_Hasil_Production_Packaging.Columns(CellSatuan_Pckg).HeaderText = Base_Language.Lang_Global_Satuan

            SQL = "select a.No_Transaksi, a.No_PO, a.Lokasi, a.Tgl_produksi, a.Jam, a.Kode_Stock_Owner, "
            SQL = SQL & "a.Kode_Barang, b.nama, a.Jumlah, a.Satuan, "
            SQL = SQL & "isnull((select sum(f.Qty_Hasil_Produksi) from Emi_Production_Results e, EMI_Production_Results_Detail_Barang f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi) "
            SQL = SQL & ",0) as Qty_Hasil_Produksi, "
            SQL = SQL & "isnull((select sum(f.Qty_Good_Stock) from Emi_Production_Results e, EMI_Production_Results_Detail_Barang f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi) "
            SQL = SQL & ",0) as Qty_Good_Stock, "
            SQL = SQL & "isnull((select sum(f.Qty_Bad_Stock) from Emi_Production_Results e, EMI_Production_Results_Detail_Barang f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi) "
            SQL = SQL & ",0) as Qty_Bad_Stock, "
            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select sum(f.jumlah) from Emi_Production_Results e, EMI_Production_Results_Detail_Scrap f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi "
            SQL = SQL & "), 0) as Qty_Scrap "
            SQL = SQL & "from Emi_Split_Production_Order a, barang b where "
            SQL = SQL & "a.Kode_Barang=b.Kode_Barang and a.Kode_Stock_Owner=b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan=b.Kode_Perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null and a.No_Transaksi='" & TextBox4.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    DateTimePicker1.Value = dr("Tgl_produksi")
                    TextBox1.Text = dr("Jam")
                    TextBox2.Text = dr("Jumlah")
                    ''TextBox6.Text = dr("nama")
                    fno_po = dr("No_PO")
                    Txt_QtyHslProduksi.Text = Format(dr("Qty_Hasil_Produksi"), "N2")
                    Txt_QtyGoodStock.Text = Format(dr("Qty_Good_Stock"), "N2")
                    Txt_QtyBadStock.Text = Format(dr("Qty_Bad_Stock"), "N2")
                    Txt_QtyScrap.Text = Format(dr("Qty_Scrap"), "N2")
                End If
            End Using

            Dgv_Hasil_Production_Packaging.Rows.Clear()
            SQL = "select a.No_Transaksi, b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah, "
            SQL = SQL & "isnull((select sum(f.Nilai_Produksi) from Emi_Production_Results e, Emi_Production_Results_Packaging_Detail f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi "
            SQL = SQL & "and f.Kode_Barang = b.Kode_Barang) "
            SQL = SQL & ",0) as Nilai_Produksi, "
            SQL = SQL & "b.Satuan from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & TextBox4.Text & "' and a.Flag_Hasil_Produksi_GI is null "
            SQL = SQL & "order by c.nama"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Hasil_Production_Packaging.Rows.Add()
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_So_Pckg).Value = .Rows(i).Item("Kode_Stock_Owner")
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_Bahan_Pckg).Value = .Rows(i).Item("Kode_Barang")
                            ''Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
                            ' Dim nhasil As Double = 0
                            ' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
                            'Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Formula_Pckg).Value = Format(.Rows(i).Item("jumlah"), "N2")
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Formula_Pckg).Value = Format(0, "N2")
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Produksi_Pckg).Value = Format(.Rows(i).Item("Nilai_Produksi"), "N2")
                            Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellSatuan_Pckg).Value = .Rows(i).Item("Satuan")
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
        ElseIf Txt_QtyHslProduksi.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_QtyHslProduksi.Focus() : Exit Sub
        ElseIf Txt_QtyBadStock.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty2, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_QtyBadStock.Focus() : Exit Sub
        ElseIf Txt_QtyGoodStock.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty3, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_QtyGoodStock.Focus() : Exit Sub
            'ElseIf Dgv_HslProduction.CurrentRow.Cells(CellNilai_Produksi).Value = "0" Then
            'ElseIf Dgv_HslProduction.CurrentRow.Cells(CellNilai_Produksi).Value = "" Then
            '    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty4, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
        End If
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            get_no_faktur()

            Dim Kd_So As String = ""
            Dim Kd_Brg As String = ""

            Dim ada_data As Boolean = False


            For index = 0 To Dgv_Hasil_Production_Packaging.Rows.Count - 1
                Get_Isi_Listview_Pckg(index)

                If Val(HilangkanTanda(LvNilai_Produksi_Pckg)) > 0 Then
                    ada_data = True
                End If

            Next

            If ada_data = False Then
                Dim tanya As String = MessageBox.Show("Belum ada Data Bahan yang di input ! ! !, Tetap Simpan . . ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If tanya = vbYes Then


                Else
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If

            End If


            If Val(HilangkanTanda(Txt_QtyHslProduksi.Text)) + Val(HilangkanTanda(Txt_QtyScrap.Text)) = 0 Then
                Dim tanya As String = MessageBox.Show("Belum ada Data Barang jadi yang di input ! ! !, Tetap Simpan . . ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If tanya = vbYes Then


                Else
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If

            End If

            SQL = "Select b.Status,b.Selesai,b.Kode_Stock_Owner,b.Kode_Barang "
            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b "
            SQL = SQL & "where a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & TextBox4.Text & "' and Flag_Hasil_Produksi_GR is null "
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


            SQL = "update Emi_Split_Production_Order set Flag_Hasil_Produksi_GR = 'Y', Tgl_Hasil_Produksi_GR = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "Jam_Hasil_Produksi_GR = '" & Format(tgl_skg, "HH:mm:ss") & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Transaksi = '" & TextBox4.Text & "' "
            ExecuteTrans(SQL)

            SQL = "select b.Jumlah as jml_po, "
            SQL = SQL & "ISNULL((select SUM(a.Jumlah) from Emi_Split_Production_Order a where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Flag_Produksi = 'Y' "
            SQL = SQL & "and a.Flag_Hasil_Produksi_GI ='Y'),0) as jml_splitGI, "

            SQL = SQL & "ISNULL((select SUM(a.Jumlah) from Emi_Split_Production_Order a where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Flag_Produksi = 'Y' "
            SQL = SQL & "and a.Flag_Hasil_Produksi_GR ='Y'),0) as jml_splitGR "

            SQL = SQL & "from EMI_Order_Produksi b where b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & fno_po & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("jml_po") = Dr("jml_splitGI") And Dr("jml_po") = Dr("jml_splitGR") Then
                        Dr.Close()
                        SQL = "Update EMI_Order_Produksi set Selesai = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and No_Faktur = '" & fno_po & "' "
                        ExecuteTrans(SQL)
                    End If
                End If
            End Using

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

        Dim Nanya As String = MessageBox.Show("Anda akan lakukan pencetakan laporan ini . . ? ?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Nanya = vbYes Then
            cetak()
            EMI_Display_Hasil_ProduksiGR.Button1_Click(Btn_Simpan, e)
            Me.Close()
        Else
            EMI_Display_Hasil_ProduksiGR.Button1_Click(Btn_Simpan, e)
            Me.Close()
        End If


    End Sub

    Private Sub cetak()
        Try
            OpenConn()
            Dim kertas As String = ""
            Dim SF As String = ""
            Dim SF2 As String = ""
            Dim SF3 As String = ""
            SQL = "select a.Kode_Perusahaan from VW_Laporan_Hasil_Production a where "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "

            SF = "{VW_Laporan_Hasil_Production.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.No_Transaksi = '" & TextBox4.Text & "' "
            SF = SF & "{VW_Laporan_Hasil_Production.No_Transaksi} = '" & TextBox4.Text & "' "


            SF2 = "{Vw_Laporan_Production_Lost_GI_GR.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
            SF2 = SF2 & "{Vw_Laporan_Production_Lost_GI_GR.No_Production_Order} = '" & TextBox4.Text & "' "



            SF3 = "{Vw_Laporan_Perfaktur_GI_GR.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
            SF3 = SF3 & "{Vw_Laporan_Perfaktur_GI_GR.No_Transaksi} = '" & TextBox4.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    'CrDoc = New Rpt_Laporan_Hasil_Production
                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.RecordSelectionFormula = SF
                    '    .Text = "Laporan Hasil Produksi"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .CrystalReportViewer1.DisplayGroupTree = False
                    '    .Refresh()
                    '    .Show()
                    'End With

                    'CrDoc = New Laporan_Production_Lost
                    'With A_Place_For_Printing3
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.RecordSelectionFormula = SF2
                    '    .Text = "Laporan Production lost"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .CrystalReportViewer1.DisplayGroupTree = False
                    '    .Refresh()
                    '    .Show()
                    'End With

                    CrDoc = New Laporan_Perfaktur_GI_GR
                    With A_Place_For_Printing
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = SF3
                        .Text = "Laporan GI GR"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                    End With

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'CrDoc = New Laporan_Perfaktur_GI_GR
                    'kertas = "A4"


                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.PrintOptions.PrinterName = PrinterQC
                    'CrDoc.RecordSelectionFormula = SF3
                    ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterQC
                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next

                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    'CrDoc.PrintToPrinter(1, False, 1, 99)

                    'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    MessageBox.Show("Data tidak ditemukan . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
    End Sub

    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub



    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles Txt_QtyBadStock.TextChanged
        If Txt_QtyHslProduksi.Text.Trim.Length = 0 Then
            Exit Sub
        ElseIf Txt_QtyBadStock.Text.Trim.Length = 0 Then
            Exit Sub
        End If
        Dim a As Double = 0
        a = Val(HilangkanTanda(Txt_QtyHslProduksi.Text)) - Val(HilangkanTanda(Txt_QtyBadStock.Text))
        Txt_QtyGoodStock.Text = Format(a, "N2")
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_QtyHslProduksi.KeyPress
        If e.KeyChar = Chr(13) Then Txt_QtyBadStock.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

End Class
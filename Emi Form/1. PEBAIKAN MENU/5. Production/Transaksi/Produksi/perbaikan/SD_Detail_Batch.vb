Public Class SD_Detail_Batch

    Public noSplit As String = ""
    Public asal As String = ""

    Dim Lv_Batch, Lv_KdBarang, Lv_NmBarang, Lv_NilaiFormula, Lv_NilaiProduksi, Lv_Satuan, Lv_NoTransaksi As String

    Dim item_Batch As Integer = 0
    Dim item_KdBarang As Integer = 1
    Dim item_NmBarang As Integer = 2
    Dim item_NilaiFormula As Integer = 3
    Dim item_NilaiProduksi As Integer = 4
    Dim item_Satuan As Integer = 5
    Dim item_NoFaktur As Integer = 6


    Private Sub SD_Detail_Batch_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Kosong()
    End Sub


    Public Sub Kosong()

        Txt_TotNilaiFormula.Text = ""
        Txt_TotNilaiPRoduksi.Text = ""

        If asal = "GI" Then
            Panel_GI.Visible = True
            Panel_GR.Visible = False

            Panel_GI.Location = New Point(13, 67)
            Panel_GR.Location = New Point(300, 67)

            '============================================================================
            '=     PANEL 1     
            '============================================================================
            Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
            Lv_Data.Columns.Add("", 0, HorizontalAlignment.Right)
            Lv_Data.Columns.Add("Batch", 90, HorizontalAlignment.Center)
            Lv_Data.Columns.Add("Tanggal", 140, HorizontalAlignment.Center)
            Lv_Data.Columns.Add("Jam", 140, HorizontalAlignment.Center)
            Lv_Data.Columns.Add("Kode Barang", 180, HorizontalAlignment.Left)
            Lv_Data.Columns.Add("Nilai Produksi", 210, HorizontalAlignment.Right)
            Lv_Data.Columns.Add("Satuan", 150, HorizontalAlignment.Center)
            Lv_Data.Columns.Add("Selisih %", 210, HorizontalAlignment.Right)

            'HIDE   
            Lv_Data.Columns.Add("no_Faktur", 0, HorizontalAlignment.Left)
            Lv_Data.View = View.Details



            Try
                OpenConn()

                Cmb_Filter_Batch_Pn1.Items.Clear()
                SQL = "select max(e.Proses) as Proses "
                SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c, Emi_Production_Results d, Emi_Production_Results_HPP e, Barang f  "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan  "
                SQL = SQL & "and a.No_PO = b.No_Faktur  "
                SQL = SQL & "and a.No_Transaksi = d.No_Production_Order  "
                SQL = SQL & "and d.No_Transaksi = e.No_Transaksi  "
                SQL = SQL & "and b.Kode_Formula = c.No_Faktur  "
                SQL = SQL & "and c.Kode_Stock_Owner = f.Kode_Stock_Owner and c.Kode_Barang = f.Kode_Barang  "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Status is null  "
                SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dim JumlahBatch As Double = Val(HilangkanTanda(Dr("Proses")))
                        Cmb_Filter_Batch_Pn1.Items.Add("--- SELURUH ---")
                        For i As Integer = 1 To JumlahBatch
                            Cmb_Filter_Batch_Pn1.Items.Add(i)
                        Next

                        Cmb_Filter_Batch_Pn1.SelectedIndex = 0
                    End If
                End Using

                Cmb_KdBarang_Pn1.Items.Clear()
                SQL = "select distinct c.Kode_Barang "
                SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c, Emi_Production_Results d, Emi_Production_Results_HPP e, Barang f  "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan  "
                SQL = SQL & "and a.No_PO = b.No_Faktur  "
                SQL = SQL & "and a.No_Transaksi = d.No_Production_Order  "
                SQL = SQL & "and d.No_Transaksi = e.No_Transaksi  "
                SQL = SQL & "and b.Kode_Formula = c.No_Faktur  "
                SQL = SQL & "and c.Kode_Stock_Owner = f.Kode_Stock_Owner and c.Kode_Barang = f.Kode_Barang  "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Status is null  "
                SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "
                Using Dr = OpenTrans(SQL)
                    Cmb_KdBarang_Pn1.Items.Add("--- SELURUH ---")
                    Do While Dr.Read
                        Cmb_KdBarang_Pn1.Items.Add(Dr("Kode_Barang"))
                    Loop
                    Cmb_KdBarang_Pn1.SelectedIndex = 0
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try


            Btn_Cari_Pn1_Click(Btn_Cari_Pn1, New EventArgs)

        Else
            Panel_GI.Visible = False
            Panel_GR.Visible = True

            Panel_GR.Location = New Point(13, 67)
            Panel_GI.Location = New Point(300, 67)

            Lv_DataGr.Columns.Clear() : Lv_DataGr.Items.Clear()
            Lv_DataGr.Columns.Add("", 0, HorizontalAlignment.Center)
            Lv_DataGr.Columns.Add("Batch", 120, HorizontalAlignment.Center)
            Lv_DataGr.Columns.Add("Jumlah Dosing", 245, HorizontalAlignment.Right)
            Lv_DataGr.Columns.Add("Jumlah Selesai", 245, HorizontalAlignment.Right)
            Lv_DataGr.Columns.Add("Selisih", 245, HorizontalAlignment.Right)
            Lv_DataGr.Columns.Add("Satuan", 120, HorizontalAlignment.Center)
            'Hide
            Lv_DataGr.Columns.Add("Lv_NoResult", 0, HorizontalAlignment.Left)
            Lv_DataGr.View = View.Details

            Lv_DetailGr.Columns.Clear() : Lv_DetailGr.Items.Clear()
            Lv_DetailGr.Columns.Add("", 0, HorizontalAlignment.Center)
            Lv_DetailGr.Columns.Add("Batch Number", 150, HorizontalAlignment.Center)
            Lv_DetailGr.Columns.Add("Tanggal", 115, HorizontalAlignment.Center)
            Lv_DetailGr.Columns.Add("Lokasi", 180, HorizontalAlignment.Left)
            Lv_DetailGr.Columns.Add("Barcode", 300, HorizontalAlignment.Left)
            Lv_DetailGr.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
            Lv_DetailGr.Columns.Add("Jumlah", 180, HorizontalAlignment.Right)
            Lv_DetailGr.Columns.Add("Satuan", 120, HorizontalAlignment.Center)
            Lv_DetailGr.View = View.Details

            '============================================================================
            '=     PANEL 2     
            '============================================================================

            Try
                OpenConn()

                Lv_DataGr.Items.Clear()
                SQL = "select a.No_Transaksi, a.No_PO, b.No_Transaksi as No_Result, c.Proses as Batch_Number, c.Jumlah_Dosing, c.Jumlah_Terpakai as Jumlah_Selesai, "
                SQL = SQL & "(c.Jumlah_Dosing - c.Jumlah_Terpakai) as Selisih, c.Satuan "
                SQL = SQL & "from Emi_Split_Production_Order a , Emi_Production_Results b, Emi_Production_Results_HPP c  "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan  "
                SQL = SQL & "and a.No_Transaksi = No_Production_Order  "
                SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
                SQL = SQL & "and a.status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and c.Tanggal is not null "
                SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "
                SQL = SQL & "order by c.Proses "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = Lv_DataGr.Items.Add("")
                        Lv.SubItems.Add(Dr("Batch_Number"))
                        Lv.SubItems.Add(Format(Dr("Jumlah_Dosing"), "N4"))
                        Lv.SubItems.Add(Format(Dr("Jumlah_Selesai"), "N4"))
                        Lv.SubItems.Add(Format(Dr("Selisih"), "N4"))
                        Lv.SubItems.Add(Dr("Satuan"))
                        'Hdide
                        Lv.SubItems.Add(Dr("No_Result"))
                    Loop
                End Using

                Lv_DetailGr.Items.Clear()
                SQL = "Select a.Kode_Perusahaan, e.No_PO, a.No_Production_Order as No_Split, a.No_Transaksi as No_Result, b.Tanggal, b.Jam, c.Batch_Number, c.Qr_Code+'-'+c.Kode_Unik_Berjalan as Barcode,  "
                SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang, d.Nama as Barang, sum(c.Jumlah) as jumlah, b.Satuan  "
                SQL = SQL & "From Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, Emi_Production_Results_Detail_Pallet c, barang d, Emi_Split_Production_Order e "
                SQL = SQL & "Where a.kode_perusahaan = b.kode_Perusahaan And b.kode_perusahaan = c.kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "and a.No_Production_Order = e.No_Transaksi "
                SQL = SQL & "And a.No_Transaksi = b.no_transaksi "
                SQL = SQL & "And b.No_Transaksi = c.no_transaksi And b.Proses = c.Proses "
                SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
                SQL = SQL & "And a.status Is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
                SQL = SQL & "group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam, c.Batch_Number, "
                SQL = SQL & "(c.Qr_Code+'-'+c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner, b.Kode_Barang, d.Nama, b.Satuan "

                SQL = SQL & "union all "

                SQL = SQL & "Select a.Kode_Perusahaan, e.No_PO, a.No_Production_Order as No_Split, a.No_Transaksi as No_Result, b.Tanggal, b.Jam, c.Batch_Number, c.Qr_Code+'-'+c.Kode_Unik_Berjalan as Barcode,  "
                SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang_scrap as Kode_Barang, d.Nama as Barang, sum(c.Jumlah) as jumlah, b.Satuan_scrap as Satuan "
                SQL = SQL & "From Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, EMI_Production_Results_Detail_Scrap c, barang d, Emi_Split_Production_Order e "
                SQL = SQL & "Where a.kode_perusahaan = b.kode_Perusahaan And b.kode_perusahaan = c.kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "and a.No_Production_Order = e.No_Transaksi "
                SQL = SQL & "And a.No_Transaksi = b.no_transaksi "
                SQL = SQL & "And b.No_Transaksi = c.no_transaksi And b.Proses = c.Proses "
                SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang_scrap = d.Kode_Barang "
                SQL = SQL & "And a.status Is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
                SQL = SQL & "group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam, c.Batch_Number, "
                SQL = SQL & "(c.Qr_Code+'-'+c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner, b.Kode_Barang_scrap, d.Nama, b.Satuan_scrap "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = Lv_DetailGr.Items.Add("")
                        Lv.SubItems.Add(Dr("Batch_Number"))
                        Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                        Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                        Lv.SubItems.Add(Dr("Barcode"))
                        Lv.SubItems.Add(Dr("Kode_Barang"))
                        Lv.SubItems.Add(Format(Dr("jumlah"), "N4"))
                        Lv.SubItems.Add(Dr("Satuan"))

                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try






        End If



    End Sub





    Private Sub Btn_Cari_Pn1_Click(sender As Object, e As EventArgs) Handles Btn_Cari_Pn1.Click
        Try
            OpenConn()
            Dim Total_Formula As Double = 0
            Dim Total_Produksi As Double = 0

            Lv_Data.Items.Clear()
            SQL = "select a.No_Transaksi, e.Tanggal, e.Jam, e.Proses, c.Kode_Stock_Owner, c.Kode_Barang, f.nama, c.satuan, "

            SQL = SQL & " "
            SQL = SQL & "round( "
            SQL = SQL & "(c.Jumlah / (select z.Hasil from Emi_Transaksi_Formulator z "
            SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur) "
            SQL = SQL & ") "
            SQL = SQL & " * "
            SQL = SQL & "isnull(a.Qty_Batch,2),4) as Nilai_Formula, "

            SQL = SQL & "ISNULL(( select z.Nilai_Produksi from Emi_Production_Results_Detail z  where z.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = d.No_Transaksi and z.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and z.Proses = e.Proses ), 0) as Nilai_Produksi "

            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c, Emi_Production_Results d, Emi_Production_Results_HPP e, Barang f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.No_Transaksi = d.No_Production_Order "
            SQL = SQL & "and d.No_Transaksi = e.No_Transaksi "
            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            SQL = SQL & "and c.Kode_Stock_Owner = f.Kode_Stock_Owner and c.Kode_Barang = f.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "

            If Not Cmb_Filter_Batch_Pn1.SelectedIndex = 0 Then
                SQL = SQL & "and e.Proses = '" & Cmb_Filter_Batch_Pn1.Text & "' "
            End If

            If Not Cmb_KdBarang_Pn1.SelectedIndex = 0 Then
                SQL = SQL & "and c.Kode_Barang = '" & Cmb_KdBarang_Pn1.Text & "' "
            End If

            SQL = SQL & "order by e.Proses, c.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add("")
                    Lv.SubItems.Add(Dr("Proses"))

                    If General_Class.CekNULL(Dr("Tanggal")) = "" Then
                        Lv.SubItems.Add("-")
                    Else
                        Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    End If

                    If General_Class.CekNULL(Dr("Jam")) = "" Then
                        Lv.SubItems.Add("-")
                    Else
                        Lv.SubItems.Add(Dr("Jam"))
                    End If

                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Produksi"))), "N4"))
                    Lv.SubItems.Add(Dr("satuan"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda((Dr("Nilai_Formula") - Dr("Nilai_Produksi")) / Dr("Nilai_Formula") * 100)), "N4"))


                    Total_Formula += Dr("Nilai_Formula")
                    Total_Produksi += Dr("Nilai_Produksi")
                Loop
            End Using

            Txt_TotNilaiFormula.Text = Format(Total_Formula, "N4")
            Txt_TotNilaiPRoduksi.Text = Format(Total_Produksi, "N4")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub






    '================================================================================================================================================================
    '=     HANDLE KEY
    '================================================================================================================================================================

    Private Sub Cmb_Filter_Batch_Pn1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter_Batch_Pn1.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KdBarang_Pn1.Focus()
    End Sub
    Private Sub Cmb_KdBarang_Pn1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KdBarang_Pn1.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari_Pn1.Focus()
    End Sub


End Class
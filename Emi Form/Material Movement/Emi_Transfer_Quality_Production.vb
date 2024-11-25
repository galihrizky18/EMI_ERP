Imports System.CodeDom.Compiler
Imports System.Text

Public Class Emi_Transfer_Quality_Production

    Private random As New Random()

    Dim arrInisialFaktur, arrJenis, arrSo As New ArrayList

    Dim arr2JEnis As New List(Of List(Of String))

    Public Lokasi As String = ""

    Dim Dgv_NoTransaksi, Dgv_IDWarehouse, Dgv_Rak, Dgv_Jenis, Dgv_JenisFix, Dgv_Jumlah, Dgv_Satuan, Dgv_JumlahFix, Dgv_NilaiBarang, Dgv_SatuanKecil, Dgv_Proses, Dgv_UrutPallet, Dgv_Sample As String
    Dim Dgv_QrCode, Dgv_BatchNumber, Dgv_Sn As String

    Dim Cell_NoTrans As Integer = 0
    Dim Cell_IDWareHouse As Integer = 1
    Dim Cell_Rak As Integer = 2
    Dim Cell_Kategori As Integer = 3
    Dim Cell_Jumlah As Integer = 4
    Dim Cell_Satuan As Integer = 5
    Dim Cell_JenisFix As Integer = 6
    Dim Cell_JumlahFix As Integer = 7
    Dim Cell_Sample As Integer = 8
    Dim Cell_NilaiBarang As Integer = 9
    Dim Cell_SatuanKecil As Integer = 10
    Dim Cell_Proses As Integer = 11
    Dim Cell_UrutPallet As Integer = 12
    Dim Cell_QrCode As Integer = 13
    Dim Cell_BatchNumber As Integer = 14
    Dim Cell_SN As Integer = 15

    Private Sub Emi_Transfer_Quality_Production_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Transfer_Quality_Production_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub

    Public Sub kosong()

        TxtNo_Transaksi.Enabled = False
        Txt_NoSplit.Enabled = False
        Txt_NoPO.Enabled = False
        Txt_KdBarang.Enabled = False
        Txt_NmBarang.Enabled = False
        Txt_Stock.Enabled = False
        Txt_Satuan.Enabled = False

        Try
            OpenConn()



            CmbSO.Items.Clear() : CmbSO.SelectedIndex = -1
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSO.Items.Add(dr("Keterangan")) : arrInisialFaktur.Add(dr("inisial_faktur")) : arrSo.Add(dr("kode_stock_owner"))
                Loop
            End Using

            CmbSO.SelectedIndex = arrSo.IndexOf(Lokasi)


            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        arrJenis.Clear()
        arrJenis.Add("Good Stock")
        arrJenis.Add("Warning Stock")
        arrJenis.Add("Bad Stock")

    End Sub

    Private Sub get_no_faktur()

        TxtNo_Transaksi.Text = FTFP & arrInisialFaktur.Item(CmbSO.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("EMI_Production_Results_QC_Barang", "No_Transaksi", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(No_Transaksi,1," & Len(FTFP) + Len(arrInisialFaktur.Item(CmbSO.SelectedIndex)) + 6 & ")", FTFP & arrInisialFaktur.Item(CmbSO.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub

    Private Sub Get_Dgv_Data(ByVal index As Integer)

        Dgv_NoTransaksi = DGV_Data_TF.Rows(index).Cells(Cell_NoTrans).Value
        Dgv_IDWarehouse = DGV_Data_TF.Rows(index).Cells(Cell_IDWareHouse).Value
        Dgv_Rak = DGV_Data_TF.Rows(index).Cells(Cell_Rak).Value
        Dgv_Jenis = DGV_Data_TF.Rows(index).Cells(Cell_Kategori).Value
        Dgv_Jumlah = DGV_Data_TF.Rows(index).Cells(Cell_Jumlah).Value
        Dgv_Satuan = DGV_Data_TF.Rows(index).Cells(Cell_Satuan).Value
        Dgv_JenisFix = DGV_Data_TF.Rows(index).Cells(Cell_JenisFix).Value
        Dgv_JumlahFix = DGV_Data_TF.Rows(index).Cells(Cell_JumlahFix).Value
        Dgv_NilaiBarang = DGV_Data_TF.Rows(index).Cells(Cell_NilaiBarang).Value
        Dgv_SatuanKecil = DGV_Data_TF.Rows(index).Cells(Cell_SatuanKecil).Value
        Dgv_Proses = DGV_Data_TF.Rows(index).Cells(Cell_Proses).Value
        Dgv_UrutPallet = DGV_Data_TF.Rows(index).Cells(Cell_UrutPallet).Value
        Dgv_QrCode = DGV_Data_TF.Rows(index).Cells(Cell_QrCode).Value
        Dgv_BatchNumber = DGV_Data_TF.Rows(index).Cells(Cell_BatchNumber).Value
        Dgv_Sn = DGV_Data_TF.Rows(index).Cells(Cell_SN).Value
        Dgv_Sample = DGV_Data_TF.Rows(index).Cells(Cell_Sample).Value

    End Sub

    Public Sub Load_Dgv()
        If Txt_NoSplit.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            DGV_Data_TF.Rows.Clear()
            SQL = "select  a.No_Transaksi, b.Proses, c.Id_Warehouse, e.Keterangan as rak, c.Jenis as kategori, c.Jumlah, c.Satuan, c.Nilai_Barang, c.Satuan_Barang, c.Urut_Oto as urut_pallet, c.Qr_Code, c.Batch_Number, c.Serial_Number  "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_barang b,EMI_Production_Results_Detail_pallet c, barang d, View_Warehouse_Position e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Id_Warehouse = e.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_production_order='" & Txt_NoSplit.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            Dim subArr As New List(Of String)

                            DGV_Data_TF.Rows.Add(1)
                            DGV_Data_TF.Rows(i).Cells(Cell_NoTrans).Value = .Rows(i).Item("No_Transaksi").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_IDWareHouse).Value = .Rows(i).Item("Id_Warehouse").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_Rak).Value = .Rows(i).Item("rak").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_Kategori).Value = .Rows(i).Item("kategori").ToString.Replace("_", " ")
                            DGV_Data_TF.Rows(i).Cells(Cell_Jumlah).Value = .Rows(i).Item("Jumlah").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_Satuan).Value = .Rows(i).Item("Satuan").ToString


                            Dim dgvCmbValueJenis As DataGridViewComboBoxCell
                            dgvCmbValueJenis = DGV_Data_TF.Rows(i).Cells(Cell_JenisFix)
                            dgvCmbValueJenis.Items.Clear()

                            For j As Integer = 0 To arrJenis.Count - 1
                                dgvCmbValueJenis.Items.Add(arrJenis(j)) : subArr.Add(arrJenis(j))
                            Next

                            DGV_Data_TF.Rows(i).Cells(Cell_NilaiBarang).Value = .Rows(i).Item("Nilai_Barang").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_SatuanKecil).Value = .Rows(i).Item("Satuan_Barang").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_Proses).Value = .Rows(i).Item("Proses").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_UrutPallet).Value = .Rows(i).Item("urut_pallet").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_QrCode).Value = .Rows(i).Item("Qr_Code").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_BatchNumber).Value = .Rows(i).Item("Batch_Number").ToString
                            DGV_Data_TF.Rows(i).Cells(Cell_SN).Value = .Rows(i).Item("Serial_Number").ToString

                            DGV_Data_TF.Rows(i).Cells(Cell_JumlahFix).Style.BackColor = Color.LightGray
                            DGV_Data_TF.Rows(i).Cells(Cell_Sample).Style.BackColor = Color.LightGray

                            'Default
                            'DGV_Data_TF.Rows(i).Cells(Cell_JumlahFix).Value = "0"
                            'DGV_Data_TF.Rows(i).Cells(Cell_Sample).Value = "0"
                            'DGV_Data_TF.Rows(i).Cells(Cell_JenisFix).Value = .Rows(i).Item("kategori").ToString.Replace("_", " ")


                            arr2JEnis.Add(subArr)
                        Next
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

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
        Load_Dgv()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If DGV_Data_TF.RowCount = 0 Or Txt_NoSplit.Text.Trim.Length = 0 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            '====================
            '=     CEK DATA     =
            '====================
            Dim isUpdate As Boolean = False

            For i As Integer = 0 To DGV_Data_TF.RowCount - 1

                Get_Dgv_Data(i)

                If Dgv_JenisFix <> "" Or Dgv_JumlahFix <> "" Or Dgv_Sample <> "" Then

                    isUpdate = True

                    If Dgv_JenisFix <> "" Then
                        If Dgv_JumlahFix = "" Then
                            MessageBox.Show("Jumlah Berubah Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        ElseIf Dgv_Sample = "" Then
                            MessageBox.Show("Jumlah Sample Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If

                    If Dgv_JumlahFix <> "" Then
                        If Dgv_JenisFix = "" Then
                            MessageBox.Show("Pilih Dahulu Jenis Berubah", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        ElseIf Dgv_Sample = "" Then
                            MessageBox.Show("Jumlah Sample Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If

                    If Dgv_Sample <> "" Then
                        If Dgv_JenisFix = "" Then
                            MessageBox.Show("Pilih Dahulu Jenis Berubah", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        ElseIf Dgv_JumlahFix = "" Then
                            MessageBox.Show("Jumlah Berubah Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If

                    SQL = "select (Jumlah - " & (Val(HilangkanTanda(Dgv_JumlahFix)) + Val(HilangkanTanda(Dgv_Sample))) & ") as hasil from EMI_Production_Results_Detail_pallet where Kode_Perusahaan ='" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Transaksi='" & Dgv_NoTransaksi & "' and Proses = '" & Dgv_Proses & "' and Urut_Oto ='" & Dgv_UrutPallet & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Val(Dr("hasil")) < 0 Then
                                Dr.Close()
                                MessageBox.Show("Stock akan Menjadi Negatif pada Baris : " & i + 1, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                CloseTrans()
                                CloseConn()
                                Exit Sub
                            End If
                        End If
                    End Using
                End If



            Next


            'If isUpdate = False Then
            '    MessageBox.Show("Tidak Ada Data yang Diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    CloseTrans()
            '    CloseConn()
            '    Exit Sub
            'End If


            '====================================================
            '=     CEK APAKAH NOSPLIT SUDAH PERNAH INPUT QC     =
            '====================================================
            SQL = "select Kode_Perusahaan from Emi_Production_Results_Qc_Barang where Kode_Perusahaan='" & KodePerusahaan & "' and No_Split_Production = '" & Txt_NoSplit.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    MessageBox.Show("Data Sudah Pernah Di Input", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If
            End Using

            Dim Jumlah_Good As Double = 0
            Dim Jumlah_Warning As Double = 0
            Dim Jumlah_Bad As Double = 0
            Dim Jumlah_Sample As Double = 0


            For i As Integer = 0 To DGV_Data_TF.RowCount - 1

                Get_Dgv_Data(i)

                'Default
                Dgv_JenisFix = If(Dgv_JenisFix = "", Dgv_Jenis, Dgv_JenisFix)
                Dgv_JumlahFix = If(Dgv_JumlahFix = "", "0", Dgv_JumlahFix)
                Dgv_Sample = If(Dgv_Sample = "", "0", Dgv_Sample)


                'If Dgv_JenisFix <> "" And Dgv_JumlahFix <> "" Then

                '==========================
                '=     SUM JUMLAH FIX     =
                '==========================
                If Dgv_JenisFix.Replace(" ", "_").ToUpper = "GOOD_STOCK" Then

                    Jumlah_Good = Jumlah_Good + Val(HilangkanTanda(Dgv_JumlahFix))

                ElseIf Dgv_JenisFix.Replace(" ", "_").ToUpper = "WARNING_STOCK" Then

                    Jumlah_Warning = Jumlah_Warning + Val(HilangkanTanda(Dgv_JumlahFix))

                ElseIf Dgv_JenisFix.Replace(" ", "_").ToUpper = "BAD_STOCK" Then

                    Jumlah_Bad = Jumlah_Bad + Val(HilangkanTanda(Dgv_JumlahFix))

                End If

                If Not Dgv_Sample = "" Then
                    Jumlah_Sample = Jumlah_Sample + Val(HilangkanTanda(Dgv_Sample))
                End If

                'End If

            Next




            '=========================
            '=     GET DATA LAMA     =
            '=========================

            Dim Sn_Lama As String = ""
            Dim Tgl_ProduksiLama As String = ""
            Dim Tgl_ExpiredLama As String = ""
            Dim Id_SusunanLama As String = ""
            Dim Batch_NumeberLama As String = ""
            Dim Qr_KodeLama As String = ""

            SQL = "select top 1 Serial_Number, Tgl_Produksi, Tgl_Expired, id_Susunan, Batch_Number, Qr_Code from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & CmbSO.SelectedItem & "' "
            SQL = SQL & "and kode_barang='" & Txt_KdBarang.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Sn_Lama = CekIsNULL(Dr("Serial_Number"))
                    Tgl_ProduksiLama = CekIsNULL(Dr("Tgl_Produksi"))
                    Tgl_ExpiredLama = CekIsNULL(Dr("Tgl_Expired"))
                    Id_SusunanLama = CekIsNULL(Dr("id_Susunan"))
                    Batch_NumeberLama = CekIsNULL(Dr("Batch_Number"))
                    Qr_KodeLama = CekIsNULL(Dr("Qr_Code"))
                End If
            End Using


            '=============================
            '=    INSERT TABEL INDUK     =
            '=============================
            SQL = "insert into Emi_Production_Results_Qc_Barang (Kode_Perusahaan, No_Transaksi, No_Split_Production, Tanggal, Jam, Kode_Stock_Owner, Kode_Barang, Jumlah_Good, Jumlah_Warning, Jumlah_Bad, Sample, Satuan, UserID) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & Txt_NoSplit.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
            SQL = SQL & "'" & CmbSO.SelectedIndex & "', '" & Txt_KdBarang.Text & "', '" & Jumlah_Good & "', '" & Jumlah_Warning & "', '" & Jumlah_Bad & "', '" & Jumlah_Sample & "', '" & Dgv_Satuan & "', '" & UserID & "')"
            ExecuteTrans(SQL)



            '=================================
            '=     CEK KAPASITAS SUSUNAN     =
            '=================================
            Dim KapasistasMax As Double = 0
            SQL = "select Total from Barang_Detail_Susunan where Kode_Perusahaan='" & KodePerusahaan & "' and Flag_Default = 'Y' and Kode_Barang='" & Txt_KdBarang.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    KapasistasMax = Dr("Total")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Max Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '============================
            '=     INSERT DETAIL QC     =
            '============================
            For i As Integer = 0 To DGV_Data_TF.RowCount - 1

                Get_Dgv_Data(i)

                'Default
                Dgv_JenisFix = If(Dgv_JenisFix = "", Dgv_Jenis, Dgv_JenisFix)
                Dgv_JumlahFix = If(Dgv_JumlahFix = "", "0", Dgv_JumlahFix)
                Dgv_Sample = If(Dgv_Sample = "", "0", Dgv_Sample)


                'If Dgv_JenisFix <> "" And Dgv_JumlahFix <> "" Then

                '========================
                '=     POTONG STOCK     =
                '========================
                Dim nilai_kecil As Double = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, Dgv_JumlahFix)
                Dim sample_kecil As Double = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, Dgv_Sample)
                SQL = "update Barang_SN set "

                If Dgv_Jenis.Replace(" ", "_") = "Good_Stock" Then
                    SQL = SQL & "Jumlah = Jumlah - " & (nilai_kecil + sample_kecil) & " "
                Else
                    SQL = SQL & Dgv_Jenis.Replace(" ", "_") & " = " & Dgv_Jenis.Replace(" ", "_") & " - " & (nilai_kecil + sample_kecil) & " "
                End If

                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & CmbSO.SelectedItem & "' and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SQL = SQL & "and Serial_Number = '" & Dgv_Sn & "'"
                ExecuteTrans(SQL)


                '============================
                '=     INSERT DETAIL QC     =
                '============================
                SQL = "insert into Emi_Production_Results_Qc_Barang_Detail (Kode_Perusahaan, No_Transaksi, Qr_Code, Jenis, Jumlah, Jenis_Berubah, Jumlah_Good, Jumlah_Warning, Jumlah_Bad, Sample, Satuan, Urut_Pallet) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & Dgv_QrCode & "', '" & Dgv_Jenis.Replace(" ", "_") & "', '" & HilangkanTanda(Dgv_Jumlah) & "', '" & Dgv_JenisFix.Replace(" ", "_") & "', "


                Dim FinalStock As Double = Val(HilangkanTanda(Dgv_Jumlah)) - (Val(HilangkanTanda(Dgv_JumlahFix)) - Val(HilangkanTanda(Dgv_Sample)))
                Dim GoodStock, WarningStock, BadStock As String
                GoodStock = "0" : WarningStock = "0" : BadStock = "0"

                Select Case Dgv_Jenis.Replace(" ", "_").ToUpper
                    Case "GOOD_STOCK"
                        Select Case Dgv_JenisFix.Replace(" ", "_").ToUpper
                            Case "WARNING_STOCK"
                                GoodStock = FinalStock.ToString()
                                WarningStock = HilangkanTanda(Dgv_JumlahFix)
                            Case "BAD_STOCK"
                                GoodStock = FinalStock.ToString()
                                BadStock = HilangkanTanda(Dgv_JumlahFix)
                        End Select

                    Case "WARNING_STOCK"
                        Select Case Dgv_JenisFix.Replace(" ", "_").ToUpper
                            Case "GOOD_STOCK"
                                GoodStock = HilangkanTanda(Dgv_JumlahFix)
                                WarningStock = FinalStock.ToString()
                            Case "BAD_STOCK"
                                WarningStock = FinalStock.ToString()
                                BadStock = HilangkanTanda(Dgv_JumlahFix)
                        End Select

                    Case "BAD_STOCK"
                        Select Case Dgv_JenisFix.Replace(" ", "_").ToUpper
                            Case "GOOD_STOCK"
                                GoodStock = HilangkanTanda(Dgv_JumlahFix)
                                BadStock = FinalStock.ToString()
                            Case "WARNING_STOCK"
                                WarningStock = HilangkanTanda(Dgv_JumlahFix)
                                BadStock = FinalStock.ToString()
                        End Select
                End Select

                SQL = SQL & "'" & GoodStock & "', '" & WarningStock & "', '" & BadStock & "', "

                SQL = SQL & "'" & HilangkanTanda(Dgv_Sample) & "', '" & Dgv_Satuan & "', '" & Dgv_UrutPallet & "') "
                ExecuteTrans(SQL)



                'Get Oto Terakhir
                Dim Last_QcDetailOto As Integer = 0
                SQL = "SELECT IDENT_CURRENT('Emi_Production_Results_Qc_Barang_detail') AS Last_Urut_Detail "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Last_QcDetailOto = Dr("Last_Urut_Detail")
                    End If
                End Using


                '=========================
                '=     INSERT DET QC     =
                '=========================
                SQL = "insert into Emi_Production_Results_Qc_Barang_Det (Kode_Perusahaan, No_Transaksi, Urut_Detail, Jenis, Jenis_Stock, Serial_Number, Jumlah, Satuan) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & Last_QcDetailOto & "', 'MIN', '" & Dgv_Jenis & "', '" & Dgv_Sn & "', '" & HilangkanTanda(Dgv_JumlahFix) & "', '" & Dgv_Satuan & "')"
                ExecuteTrans(SQL)


                '================================
                '=     INSERT DET QC RESULT     =
                '================================
                If Not Dgv_Sample = "" Then
                    SQL = "insert into Emi_Production_Results_Qc_Barang_Det (Kode_Perusahaan, No_Transaksi, Urut_Detail, Jenis, Jenis_Stock, Serial_Number, Jumlah, Satuan) values "
                    SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & Last_QcDetailOto & "', 'SAMPLE', '" & Dgv_Jenis & "', '" & Dgv_Sn & "', '" & HilangkanTanda(Dgv_Sample) & "', '" & Dgv_Satuan & "')"
                    ExecuteTrans(SQL)
                End If


                'End If

            Next








#Region "CREATE NEW BARANG_SN"

            ''==========================
            ''=     INSERT DATA SN     =
            ''==========================
            'If Jumlah_Good <> 0 Then


            '    Dim sisa As Double = Jumlah_Good
            '    '===========================
            '    '=     INSERT SN GOOD      =
            '    '===========================
            '    Do While sisa > 0

            '        Dim TotalKecil As Double = 0

            '        If sisa > KapasistasMax Then
            '            TotalKecil = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, KapasistasMax)
            '            sisa = sisa - KapasistasMax
            '        Else
            '            TotalKecil = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, sisa)
            '        End If


            '        Dim RakKosong = Get_Rak_Kosong()
            '        Dim available_Id_Warehouse As String = RakKosong.Item1
            '        Dim available_NoPallet As String = RakKosong.Item2

            '        '=================================
            '        '=     INSERT BARANG_SN BARU     =
            '        '=================================
            '        Dim SnBaru As String = Generate_New_Sn(Sn_Lama)
            '        Dim New_KodeUnikBerjalan As String = Generate_Random_Kode(10)
            '        Dim New_KodeUnikAsal As String = Generate_Random_Kode(10)

            '        SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Warning_Stock, Bad_Stock, Tgl_Expired, Tgl_Produksi, "
            '        SQL = SQL & "Id_Warehouse, id_Susunan, Batch_Number, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet ) values "
            '        SQL = SQL & "('" & KodePerusahaan & "', '" & CmbSO.SelectedItem & "', '" & Txt_KdBarang.Text & "', '" & SnBaru & "', '" & TotalKecil & "', '0', '0', "
            '        SQL = SQL & "'" & Tgl_ExpiredLama & "', '" & Tgl_ProduksiLama & "', '" & available_Id_Warehouse & "', '" & Id_SusunanLama & "', '" & Batch_NumeberLama & "', "
            '        SQL = SQL & "'" & Qr_KodeLama & "', '" & New_KodeUnikBerjalan & "', '" & New_KodeUnikAsal & "', '" & available_NoPallet & "')"
            '        ExecuteTrans(SQL)

            '    Loop

            'End If

            ''=============================
            ''=     INSERT SN WARNING     =
            ''=============================
            'If Jumlah_Warning <> 0 Then

            '    '=============================
            '    '=    INSERT TABEL INDUK     =
            '    '=============================
            '    Dim WarningKecil As Double = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, Jumlah_Warning)
            '    SQL = "insert into EMI_Production_Results_QC_Barang (Kode_Perusahaan, No_Transaksi, No_Split_Production, Tanggal, Jam, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, Jumlah_Barang, Satuan_Barang, Jenis, UserID) values  "
            '    SQL = "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & Txt_NoSplit.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
            '    SQL = SQL & "'" & CmbSO.SelectedIndex & "', '" & Txt_KdBarang.Text & "', '" & Jumlah_Warning & "', '" & Dgv_Satuan & "', '" & WarningKecil & "', '" & Dgv_SatuanKecil & "', 'Warning_Stock', '" & UserID & "') "
            '    ExecuteTrans(SQL)


            '    Dim sisa As Double = Jumlah_Warning
            '    '================================
            '    '=     INSERT SN WARNING        =
            '    '================================
            '    Do While sisa > 0

            '        Dim TotalKecil As Double = 0

            '        If sisa > KapasistasMax Then
            '            TotalKecil = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, KapasistasMax)
            '            sisa = sisa - KapasistasMax
            '        Else
            '            TotalKecil = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, sisa)
            '        End If


            '        Dim RakKosong = Get_Rak_Kosong()
            '        Dim available_Id_Warehouse As String = RakKosong.Item1
            '        Dim available_NoPallet As String = RakKosong.Item2

            '        '=================================
            '        '=     INSERT BARANG_SN BARU     =
            '        '=================================
            '        Dim SnBaru As String = Generate_New_Sn(Sn_Lama)
            '        Dim New_KodeUnikBerjalan As String = Generate_Random_Kode(10)
            '        Dim New_KodeUnikAsal As String = Generate_Random_Kode(10)

            '        SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Warning_Stock, Bad_Stock, Tgl_Expired, Tgl_Produksi, "
            '        SQL = SQL & "Id_Warehouse, id_Susunan, Batch_Number, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet ) values "
            '        SQL = SQL & "('" & KodePerusahaan & "', '" & CmbSO.SelectedItem & "', '" & Txt_KdBarang.Text & "', '" & SnBaru & "', '0', '" & TotalKecil & "', '0', "
            '        SQL = SQL & "'" & Tgl_ExpiredLama & "', '" & Tgl_ProduksiLama & "', '" & available_Id_Warehouse & "', '" & Id_SusunanLama & "', '" & Batch_NumeberLama & "', "
            '        SQL = SQL & "'" & Qr_KodeLama & "', '" & New_KodeUnikBerjalan & "', '" & New_KodeUnikAsal & "', '" & available_NoPallet & "')"
            '        ExecuteTrans(SQL)

            '    Loop

            'End If


            ''=========================
            ''=     INSERT SN BAD     =
            ''=========================
            'If Jumlah_Bad <> 0 Then

            '    '=============================
            '    '=    INSERT TABEL INDUK     =
            '    '=============================
            '    Dim BadKecil As Double = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, Jumlah_Bad)
            '    SQL = "insert into EMI_Production_Results_QC_Barang (Kode_Perusahaan, No_Transaksi, No_Split_Production, Tanggal, Jam, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, Jumlah_Barang, Satuan_Barang, Jenis, UserID) values  "
            '    SQL = "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & Txt_NoSplit.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
            '    SQL = SQL & "'" & CmbSO.SelectedIndex & "', '" & Txt_KdBarang.Text & "', '" & Jumlah_Bad & "', '" & Dgv_Satuan & "', '" & BadKecil & "', '" & Dgv_SatuanKecil & "', 'Bad_Stock', '" & UserID & "') "
            '    ExecuteTrans(SQL)


            '    Dim sisa As Double = Jumlah_Bad
            '    '============================
            '    '=     INSERT SN BAD        =
            '    '============================
            '    Do While sisa > 0

            '        Dim TotalKecil As Double = 0

            '        If sisa > KapasistasMax Then
            '            TotalKecil = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, KapasistasMax)
            '            sisa = sisa - KapasistasMax
            '        Else
            '            TotalKecil = Ubah_Angka_Kecil(Txt_KdBarang.Text, Dgv_Satuan, Dgv_SatuanKecil, sisa)
            '        End If


            '        Dim RakKosong = Get_Rak_Kosong()
            '        Dim available_Id_Warehouse As String = RakKosong.Item1
            '        Dim available_NoPallet As String = RakKosong.Item2

            '        '=================================
            '        '=     INSERT BARANG_SN BARU     =
            '        '=================================
            '        Dim SnBaru As String = Generate_New_Sn(Sn_Lama)
            '        Dim New_KodeUnikBerjalan As String = Generate_Random_Kode(10)
            '        Dim New_KodeUnikAsal As String = Generate_Random_Kode(10)

            '        SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Warning_Stock, Bad_Stock, Tgl_Expired, Tgl_Produksi, "
            '        SQL = SQL & "Id_Warehouse, id_Susunan, Batch_Number, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet ) values "
            '        SQL = SQL & "('" & KodePerusahaan & "', '" & CmbSO.SelectedItem & "', '" & Txt_KdBarang.Text & "', '" & SnBaru & "', '0', '0', '" & TotalKecil & "', "
            '        SQL = SQL & "'" & Tgl_ExpiredLama & "', '" & Tgl_ProduksiLama & "', '" & available_Id_Warehouse & "', '" & Id_SusunanLama & "', '" & Batch_NumeberLama & "', "
            '        SQL = SQL & "'" & Qr_KodeLama & "', '" & New_KodeUnikBerjalan & "', '" & New_KodeUnikAsal & "', '" & available_NoPallet & "')"
            '        ExecuteTrans(SQL)

            '    Loop

            'End If

#End Region









            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
            Me.Close()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub






    '=== FUNCTION SHORT ===
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

    Private Function Get_Rak_Kosong() As (String, String)

        Dim available_Id_Warehouse As String = ""
        Dim available_NoPallet As String = ""

        SQL = "select top(1) id_wms_warehouse_position, nomor_urut from view_warehouse_position_detail where kode_barang is null "
        Using Dr2 = OpenTrans(SQL)
            Do While Dr2.Read
                available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                available_NoPallet = Dr2("nomor_urut")
            Loop
        End Using

        Return (available_Id_Warehouse, available_NoPallet)
    End Function






    '=== FUNCTION UTILITY ===

    Private Sub DGV_Data_TF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellEndEdit

        If DGV_Data_TF.RowCount = 0 Then Exit Sub

        Dim cellJumlah As String = DGV_Data_TF.CurrentRow.Cells(Cell_JumlahFix).Value
        Dim cellSample As String = DGV_Data_TF.CurrentRow.Cells(Cell_Sample).Value

        If Not IsNumeric(cellJumlah) Then
            DGV_Data_TF.CurrentRow.Cells(Cell_JumlahFix).Value = ""
        End If
        If Not IsNumeric(cellSample) Then
            DGV_Data_TF.CurrentRow.Cells(Cell_Sample).Value = ""
        End If

        If DGV_Data_TF.CurrentRow.Cells(Cell_Kategori).Value = DGV_Data_TF.CurrentRow.Cells(Cell_JenisFix).Value Then
            Dim comboBoxCell As DataGridViewComboBoxCell = CType(DGV_Data_TF.CurrentRow.Cells(Cell_JenisFix), DataGridViewComboBoxCell)
            comboBoxCell.Value = DBNull.Value
            MessageBox.Show("Jenis Berubah Tidak Boleh Sama dengan Jenis Awal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub

        End If

    End Sub

    Private Sub DGV_Data_TF_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellClick
        If e.ColumnIndex = jenis_fix.Index Then
            DGV_Data_TF.CurrentCell = DGV_Data_TF.Rows(e.RowIndex).Cells(e.ColumnIndex)
            DGV_Data_TF.BeginEdit(True)
        End If
    End Sub

    Private Function Generate_Random_Kode(ByVal length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim result As New StringBuilder()

        For i As Integer = 1 To length
            Dim index As Integer = random.Next(0, chars.Length)
            result.Append(chars(index))
        Next

        Return result.ToString()
    End Function

    Private Function Generate_New_Sn(ByVal SerialNumberOld As String) As String
        'GENERATE SN BARU
        Dim hargaSnOld As String = Get_Harga_SN(SerialNumberOld)

        Dim str As String = Format(random.Next(0, 999), "000") & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HHmmss")
        Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
        Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaSnOld & Tanda_SN & "02" & Tanda_SN & Format(DateTime.Now, "yyyy-MM-dd")

        Return SN_Baru
    End Function
    Public Shared Function CekIsNULL(ByVal xNullString As Object) As String
        Try
            If IsDBNull(xNullString) Then
                Return "NULL"
            Else
                Return xNullString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "NULL"
        End Try
    End Function
End Class
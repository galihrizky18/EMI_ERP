Public Class N_EMI_Transaksi_Validasi_Adjustment_Stock

    Private ReadOnly BodyAlignments, BodyAlignments2, BodyAlignments3 As New Dictionary(Of Integer, StringAlignment)
    Dim arrFilter As New ArrayList

    Dim Lv_Lokasi, Lv_No_Faktur, Lv_Kd_So, Lv_Tanggal, Lv_Jam, Lv_Jenis_Adjustment, Lv_Keterangan, Lv_User As String

    Dim Item_Lokasi As Integer = 0
    Dim Item_No_Faktur As Integer = 1
    Dim Item_Kd_So As Integer = 2
    Dim Item_Tanggal As Integer = 3
    Dim Item_Jam As Integer = 4
    Dim Item_Jenis_Adjustment As Integer = 5
    Dim Item_Keterangan As Integer = 6
    Dim Item_User As Integer = 7

    Dim Lv2_Kd_Barang, Lv2_Nm_Barang, Lv2_Tot_Tambah, Lv2_Tot_Minus, Lv2_Satuan, Lv2_Urut As String

    Dim item2_Kd_Barang As Integer = 0
    Dim item2_Nm_Barang As Integer = 1
    Dim item2_Tot_Tambah As Integer = 2
    Dim item2_Tot_Minus As Integer = 3
    Dim item2_Satuan As Integer = 4
    Dim item2_Urut As Integer = 5





    Private Sub N_EMI_Transaksi_Validasi_Adjustment_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Lokasi", 120) : BodyAlignments(0) = StringAlignment.Center
        Lv_Data.Columns.Add("No Faktur", 140) : BodyAlignments(1) = StringAlignment.Near
        Lv_Data.Columns.Add("Kode Stock Owner", 130) : BodyAlignments(2) = StringAlignment.Near
        Lv_Data.Columns.Add("Tanggal", 110) : BodyAlignments(3) = StringAlignment.Center
        Lv_Data.Columns.Add("Jam", 100) : BodyAlignments(4) = StringAlignment.Center
        Lv_Data.Columns.Add("Jenis Adjustment", 130) : BodyAlignments(5) = StringAlignment.Center
        Lv_Data.Columns.Add("Keterangan", 260) : BodyAlignments(6) = StringAlignment.Near
        Lv_Data.Columns.Add("User", 130) : BodyAlignments(7) = StringAlignment.Near
        Lv_Data.View = View.Details

        Lv_Detail_Barang.Columns.Clear()
        Lv_Detail_Barang.Columns.Add("Kode Barang", 110) : BodyAlignments2(0) = StringAlignment.Near
        Lv_Detail_Barang.Columns.Add("Nama Barang", 250) : BodyAlignments2(1) = StringAlignment.Near
        Lv_Detail_Barang.Columns.Add("Total Tambah", 130) : BodyAlignments2(2) = StringAlignment.Far
        Lv_Detail_Barang.Columns.Add("Total Minus", 100) : BodyAlignments2(3) = StringAlignment.Far
        Lv_Detail_Barang.Columns.Add("Satuan", 90) : BodyAlignments2(4) = StringAlignment.Center
        'Hide
        Lv_Detail_Barang.Columns.Add("Urut", 0) : BodyAlignments2(5) = StringAlignment.Center
        Lv_Detail_Barang.View = View.Details

        Lv_Detail_Barcode.Columns.Clear()
        Lv_Detail_Barcode.Columns.Add("Barcode", 340) : BodyAlignments3(0) = StringAlignment.Near
        Lv_Detail_Barcode.Columns.Add("Jumlah", 130) : BodyAlignments3(1) = StringAlignment.Far
        Lv_Detail_Barcode.Columns.Add("Satuan", 80) : BodyAlignments3(2) = StringAlignment.Center
        Lv_Detail_Barcode.View = View.Details

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("Lokasi") : arrFilter.Add("a.Lokasi")
        Cmb_Filter.Items.Add("No Faktur") : arrFilter.Add("a.No_Faktur")
        Cmb_Filter.Items.Add("Kode Stock Owner") : arrFilter.Add("a.Kode_Stock_Owner")
        Cmb_Filter.Items.Add("Jenis Adjustment") : arrFilter.Add("a.Jenis_Adjustment")
        Cmb_Filter.Items.Add("User") : arrFilter.Add("a.UserID")


        Kosong()

    End Sub

    Private Sub Kosong()

        Txt_NoFaktur.Text = ""
        Cmb_Filter.SelectedIndex = -1


        Load_Data_Parent()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Filter Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoFaktur.Focus()
            Exit Sub
        ElseIf Txt_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No Faktur Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoFaktur.Focus()
            Exit Sub
        End If

        Load_Data_Parent()
    End Sub

    Private Sub Load_Data_Parent()
        Try
            OpenConn()

            Lv_Data.BeginUpdate()
            Lv_Data.Items.Clear() : Lv_Detail_Barang.Items.Clear() : Lv_Detail_Barcode.Items.Clear()
            SQL = "select a.Lokasi, a.No_Faktur, a.Kode_Stock_Owner, a.Tanggal, a.Jam, a.Jenis_Adjustment, a.Keterangan, a.UserID "
            SQL &= $"from Emi_Adjustment_Stock a "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.status is NULL "
            SQL &= $"and a.Flag_Validation_Accounting is NULL "
            If Cmb_Filter.SelectedIndex <> -1 Then
                SQL &= $" and {arrFilter(Cmb_Filter.SelectedIndex)} like '%" & Txt_NoFaktur.Text.Trim & "%' "
            End If
            SQL &= $"order by a.Tanggal, a.Jam, a.No_Faktur "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jenis_Adjustment")) = "", "-", Dr("Jenis_Adjustment")))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("UserID"))
                Loop

                Lv_Data.EndUpdate()
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Data_Parent(ByVal index As Integer)
        Lv_Lokasi = Lv_Data.Items(index).SubItems(Item_Lokasi).Text
        Lv_No_Faktur = Lv_Data.Items(index).SubItems(Item_No_Faktur).Text
        Lv_Kd_So = Lv_Data.Items(index).SubItems(Item_Kd_So).Text
        Lv_Tanggal = Lv_Data.Items(index).SubItems(Item_Tanggal).Text
        Lv_Jam = Lv_Data.Items(index).SubItems(Item_Jam).Text
        Lv_Jenis_Adjustment = Lv_Data.Items(index).SubItems(Item_Jenis_Adjustment).Text
        Lv_Keterangan = Lv_Data.Items(index).SubItems(Item_Keterangan).Text
        Lv_User = Lv_Data.Items(index).SubItems(Item_User).Text
    End Sub

    Private Sub Get_Data_Detail_Barang(ByVal index As Integer)
        Lv2_Kd_Barang = Lv_Detail_Barang.Items(index).SubItems(item2_Kd_Barang).Text
        Lv2_Nm_Barang = Lv_Detail_Barang.Items(index).SubItems(item2_Nm_Barang).Text
        Lv2_Tot_Tambah = Lv_Detail_Barang.Items(index).SubItems(item2_Tot_Tambah).Text
        Lv2_Tot_Minus = Lv_Detail_Barang.Items(index).SubItems(item2_Tot_Minus).Text
        Lv2_Satuan = Lv_Detail_Barang.Items(index).SubItems(item2_Satuan).Text
        Lv2_Urut = Lv_Detail_Barang.Items(index).SubItems(item2_Urut).Text
    End Sub


    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data.SelectedIndexChanged
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Data.FocusedItem.SubItems(Item_No_Faktur).Text

            Lv_Detail_Barang.BeginUpdate()
            Lv_Detail_Barang.Items.Clear() : Lv_Detail_Barcode.Items.Clear()
            SQL = "select b.Kode_Barang, c.nama as nama_barang, b.Total_Tambah, b.Total_Minus, b.Satuan, b.Urut "
            SQL &= $"from Emi_Adjustment_Stock a "
            SQL &= $"inner join Emi_Adjustment_Stock_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.status is NULL "
            SQL &= $"and a.Flag_Validation_Accounting is NULL "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"order by b.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("nama_barang"))
                    Lv.SubItems.Add(Format(Dr("Total_Tambah"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Total_Minus"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Urut"))
                Loop
                Lv_Detail_Barang.EndUpdate()
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Lv_Detail_Barang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Detail_Barang.SelectedIndexChanged
        If Lv_Detail_Barang.Items.Count = 0 Or Lv_Detail_Barang.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Data.FocusedItem.SubItems(Item_No_Faktur).Text
            Dim SelectedKdBarang As String = Lv_Detail_Barang.FocusedItem.SubItems(item2_Kd_Barang).Text
            Dim SelectedUrutDetail As String = Lv_Detail_Barang.FocusedItem.SubItems(item2_Urut).Text

            Lv_Detail_Barcode.BeginUpdate()
            Lv_Detail_Barcode.Items.Clear()
            SQL = "select (d.Qr_Code+'-'+d.Kode_Unik_Berjalan) as Barcode, c.Jumlah, b.Satuan "
            SQL &= $"from Emi_Adjustment_Stock a "
            SQL &= $"inner join Emi_Adjustment_Stock_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join Emi_Adjustment_Stock_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut = c.Urut_Adj "
            SQL &= $"inner join barang_sn d on c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and c.Serial_Number = d.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.status is NULL "
            SQL &= $"and a.Flag_Validation_Accounting is NULL "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"and c.Urut_Adj = '{SelectedUrutDetail}' "
            SQL &= $"order by b.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Barcode.Items.Add(Dr("Barcode"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
                Lv_Detail_Barcode.EndUpdate()
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Data.FocusedItem.Text)
    End Sub



    Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        If MessageBox.Show("Yakin ingin melakukan validasi No Faktur ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim SelectedFaktur As String = Lv_Data.FocusedItem.SubItems(Item_No_Faktur).Text

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Validasi_Adjustment_Stock_Accounting") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Validasi Adjustment Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '===================================================
            '=     CEK APAKAH SEMUA DATA SUDAH DI VALIDASI     =
            '===================================================
            Dim Kd_SO_Parent As String = ""
            SQL = "select Flag_Validation_Accounting, Kode_Stock_Owner from Emi_Adjustment_Stock "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and status is null "
            SQL &= $"and no_faktur = '{SelectedFaktur}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Flag_Validation_Accounting")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Terjadi Kesalahan, No Faktur {SelectedFaktur} Sudah Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Kd_SO_Parent = Dr("Kode_Stock_Owner")

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"No Faktur {SelectedFaktur} Tidak Ditemukan, Harap Hubungi Tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim inisial_faktur_dari As String = ""
            Dim akun_persediaan As String = ""
            Dim akun_adj_plus As String = ""
            Dim akun_adj_min As String = ""

            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging, Adjustment_Stock_Kurang, Adjustment_Stock_Tambah from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Kd_SO_Parent & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'akun_persediaan_dari = Dr("persediaan")
                    inisial_faktur_dari = Dr("inisial_faktur")
                    akun_adj_plus = Dr("Adjustment_Stock_Tambah")
                    akun_adj_min = Dr("Adjustment_Stock_Kurang")
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
            SQL = SQL & "'" & KodeProyek & "', 'Adjustment Stock; " & Trim(SelectedFaktur) & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)


            Dim nilai_adjustplus As Double = 0
            Dim nilai_adjustmin As Double = 0
            '=====================================
            '=     GET DETAIL DATA PENGAJUAN     =
            '=====================================
            SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, c.Serial_Number, c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Urut_Oto "
            SQL &= $"from Emi_Adjustment_Stock a "
            SQL &= $"inner join Emi_Adjustment_Stock_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join Emi_Adjustment_Stock_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut = c.Urut_Adj "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.status is NULL "
            SQL &= $"and a.Flag_Validation_Accounting is NULL "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"order by b.Kode_Barang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If SelectedFaktur.Trim <> .Rows(i).Item("No_Faktur").Trim Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan, No Faktur Berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            Dim NoTransaksi As String = .Rows(i).Item("No_Faktur")
                            Dim Kd_So As String = .Rows(i).Item("Kode_Stock_Owner")
                            Dim Kd_Barang As String = .Rows(i).Item("Kode_Barang")
                            Dim SN As String = .Rows(i).Item("Serial_Number")
                            Dim Jumlah As Double = Val(HilangkanTanda(.Rows(i).Item("Jumlah")))
                            Dim Jumlah_Bags As Double = Val(HilangkanTanda(.Rows(i).Item("Jumlah_Bags")))
                            Dim Satuan As String = .Rows(i).Item("Satuan")
                            Dim Urut_Det As String = .Rows(i).Item("Urut_Oto")


                            '===============================
                            '=     CEK KESESUAIAN DATA     =
                            '===============================
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Kd_So & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Jumlah Stock pada Kode Barang " & Kd_Barang & " Tidak Sesuai Sebelum Potong Stock ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            '========================
                            '=     POTONG STOCK     =
                            '========================

                            Dim Nama As String = ""
                            'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
                            SQL = "select Nama,round(good_stock,2) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Kd_So & "' "
                            SQL = SQL & "and Kode_Barang='" & Kd_Barang & "' "
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    Nama = dr("nama")

                                    dr.Close()

                                    Dim GoodTemp As Double = Math.Abs(Jumlah)
                                    Dim BagsTemp As Double = Math.Abs(Val(HilangkanTanda(Jumlah_Bags)))

                                    If Val(HilangkanTanda(Jumlah.ToString)) < 0 Then

                                        If dr("good_stock") < Jumlah Then
                                            dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                            'ElseIf dr("Jumlah_Bags") < dgv_detail_JumlahBags Then
                                            '    dr.Close()
                                            '    CloseTrans()
                                            '    CloseConn()
                                            '    MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            '    Exit Sub
                                        End If


                                        SQL = "update barang set Good_Stock = Good_Stock - " & GoodTemp & ", Jumlah_Bags = Jumlah_Bags - 0 "
                                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Kd_So & "' "
                                        SQL = SQL & " and Kode_Barang='" & Kd_Barang & "'"
                                        ExecuteTrans(SQL)

                                    Else

                                        SQL = "update barang set Good_Stock = Good_Stock + " & GoodTemp & ", Jumlah_Bags = Jumlah_Bags + 0 "
                                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Kd_So & "' "
                                        SQL = SQL & " and Kode_Barang='" & Kd_Barang & "'"
                                        ExecuteTrans(SQL)

                                    End If

                                Else
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select round(jumlah,2) as jumlah, Jumlah_Bags, dbo.get_HPP(serial_number) as Harga from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Kd_So & "' "
                            SQL = SQL & "and Kode_Barang='" & Kd_Barang & "' "
                            SQL = SQL & "and Serial_Number='" & SN & "'"
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    Dim harga As Double = dr("Harga")
                                    Dim jumlah_sn As Double = dr("jumlah")
                                    Dim jumlah_bags_sn As Double = dr("Jumlah_Bags")
                                    dr.Close()

                                    Dim GoodTemp As Double = Math.Abs(Jumlah)
                                    Dim BagsTemp As Double = Math.Abs(Val(HilangkanTanda(Jumlah_Bags)))

                                    Dim nilai_jurnal As Double = Math.Round(GoodTemp * harga, 0)
                                    If Val(HilangkanTanda(Jumlah.ToString)) < 0 Then

                                        If jumlah_sn < Jumlah Then
                                            dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                        End If

                                        SQL = "update barang_sn set jumlah = jumlah - " & GoodTemp & ", Jumlah_Bags = Jumlah_Bags - 0 "
                                        SQL = SQL & "where Kode_Stock_Owner='" & Kd_So & "' and Kode_Barang='" & Kd_Barang & "' "
                                        SQL = SQL & "and Serial_Number='" & SN & "'"
                                        ExecuteTrans(SQL)


                                        nilai_adjustmin += nilai_jurnal


                                        SQL = "select c.akun_Persediaan "
                                        SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                                        SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                                        SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and b.kode_stock_owner = '" & Kd_So & "' and b.Kode_Barang='" & Kd_Barang & "' "
                                        Using Dr4 = OpenTrans(SQL)
                                            If Dr4.Read Then
                                                akun_persediaan = Dr4("akun_Persediaan")
                                            Else
                                                Dr4.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using


                                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_persediaan & "' "
                                        SQL = SQL & "and kredit <> 0 "
                                        Using Dr4 = OpenTrans(SQL)
                                            If Dr4.Read Then
                                                Dr4.Close()
                                                'update
                                                SQL = "update detail_jurnal set kredit = kredit+ " & nilai_jurnal & " where "
                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_persediaan & "'  "
                                                SQL = SQL & "and kredit <> 0"
                                                ExecuteTrans(SQL)
                                            Else
                                                Dr4.Close()
                                                'insert
                                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan, 1),
                                                      Strings.Mid(akun_persediaan, 2, 1),
                                                      Strings.Mid(Ganti(akun_persediaan), 3),
                                                      KodePerusahaan, KodeProyek, "Persediaan " & "; " & NoTransaksi.Trim, "0", nilai_jurnal, pagenumber, Kd_So, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                                ExecuteTrans(SQL)
                                                pagenumber = pagenumber + 1

                                            End If
                                        End Using

                                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_adj_min & "' "
                                        SQL = SQL & "and debit <> 0 "
                                        Using Dr4 = OpenTrans(SQL)
                                            If Dr4.Read Then
                                                Dr4.Close()
                                                'update
                                                SQL = "update detail_jurnal set debit = debit+ " & nilai_jurnal & " where "
                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_adj_min & "'  "
                                                SQL = SQL & "and debit <> 0"
                                                ExecuteTrans(SQL)
                                            Else
                                                Dr4.Close()
                                                'insert
                                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_adj_min, 1),
                                                      Strings.Mid(akun_adj_min, 2, 1),
                                                      Strings.Mid(Ganti(akun_adj_min), 3),
                                                      KodePerusahaan, KodeProyek, "Adjustment " & "; " & NoTransaksi.Trim, nilai_jurnal, "0", pagenumber, Kd_So, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                                ExecuteTrans(SQL)
                                                pagenumber = pagenumber + 1

                                            End If
                                        End Using

                                    Else
                                        SQL = "update barang_sn set jumlah = jumlah + " & GoodTemp & ", Jumlah_Bags = Jumlah_Bags + 0 "
                                        SQL = SQL & "where Kode_Stock_Owner='" & Kd_So & "' and Kode_Barang='" & Kd_Barang & "' "
                                        SQL = SQL & "and Serial_Number='" & SN & "'"
                                        ExecuteTrans(SQL)

                                        nilai_adjustplus += nilai_jurnal

                                        SQL = "select c.akun_Persediaan "
                                        SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                                        SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                                        SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and b.kode_stock_owner = '" & Kd_So & "' and b.Kode_Barang='" & Kd_Barang & "' "
                                        Using Dr4 = OpenTrans(SQL)
                                            If Dr4.Read Then
                                                akun_persediaan = Dr4("akun_Persediaan")
                                            Else
                                                Dr4.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using


                                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_persediaan & "' "
                                        SQL = SQL & "and debit <> 0 "
                                        Using Dr4 = OpenTrans(SQL)
                                            If Dr4.Read Then
                                                Dr4.Close()
                                                'update
                                                SQL = "update detail_jurnal set debit = debit+ " & nilai_jurnal & " where "
                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_persediaan & "'  "
                                                SQL = SQL & "and debit <> 0"
                                                ExecuteTrans(SQL)
                                            Else
                                                Dr4.Close()
                                                'insert
                                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan, 1),
                                                      Strings.Mid(akun_persediaan, 2, 1),
                                                      Strings.Mid(Ganti(akun_persediaan), 3),
                                                      KodePerusahaan, KodeProyek, "Persediaan " & "; " & NoTransaksi.Trim, nilai_jurnal, "0", pagenumber, Kd_So, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                                ExecuteTrans(SQL)
                                                pagenumber = pagenumber + 1

                                            End If
                                        End Using

                                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_adj_plus & "' "
                                        SQL = SQL & "and kredit <> 0 "
                                        Using Dr4 = OpenTrans(SQL)
                                            If Dr4.Read Then
                                                Dr4.Close()
                                                'update
                                                SQL = "update detail_jurnal set kredit = kredit+ " & nilai_jurnal & " where "
                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_adj_plus & "'  "
                                                SQL = SQL & "and kredit <> 0"
                                                ExecuteTrans(SQL)
                                            Else
                                                Dr4.Close()
                                                'insert
                                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_adj_plus, 1),
                                                      Strings.Mid(akun_adj_plus, 2, 1),
                                                      Strings.Mid(Ganti(akun_adj_plus), 3),
                                                      KodePerusahaan, KodeProyek, "Adjustment " & "; " & NoTransaksi, "0", nilai_jurnal, pagenumber, Kd_So, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                                ExecuteTrans(SQL)
                                                pagenumber = pagenumber + 1

                                            End If
                                        End Using

                                    End If

                                Else
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Kd_So & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using D2 = BindingTrans(SQL)
                                If D2.Tables("MyTable").Rows.Count <> 0 Then
                                    If D2.Tables("MyTable").Rows(0).Item("good_stock") <> D2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or D2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> D2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
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


                            '==========================
                            '=       UPDATE DET       =
                            '==========================
                            SQL = "select Kode_Perusahaan from Emi_Adjustment_Stock_Det "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and No_Faktur = '{SelectedFaktur}' "
                            SQL &= $"and Urut_Oto = '{Urut_Det}' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dr.Close()
                                    SQL = "update Emi_Adjustment_Stock_Det set Flag_Validation_Accounting = 'Y' "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and No_Faktur = '{SelectedFaktur}' "
                                    SQL &= $"and Urut_Oto = '{Urut_Det}' "
                                    ExecuteTrans(SQL)

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalahan, Data Det Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using



                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Terjadi Kesalahan, Data Pengajuan {SelectedFaktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

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

            '==================================================
            '=       CEK APAKAH SEMUA SUDAH DI VALIDASI       =
            '==================================================
            SQL = "select (b.Qr_Code+'-'+b.Kode_Unik_Berjalan) as Barcode "
            SQL &= $"from Emi_Adjustment_Stock_Det a "
            SQL &= $"inner join barang_sn b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Serial_Number = b.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"and a.Flag_Validation_Accounting is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        Dim BarcodeGagal As String = .Rows(0).Item("Barcode")
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Terjadi Kesalahan, Barcode {BarcodeGagal} Gagal Divalidasi.{vbCrLf & vbCrLf}Harap Ulangi Transaksi atau Hubungi Tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        SQL = $"update Emi_Adjustment_Stock set Kode_Voucher = '{Kode_voucher}', Flag_Validation_Accounting = 'Y', "
                        SQL &= $"Tanggal_Validasi = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi = '{Format(tgl_skg, "HH:mm:ss")}', user_Validasi = '{UserID}' "
                        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"and No_Faktur = '{SelectedFaktur}' "
                        ExecuteTrans(SQL)


                    End If
                End With
            End Using






            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Faktur Berhasil Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub























    '==================================================================================================================================================
    '=     UTILITY
    '==================================================================================================================================================
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &HA3 Then
            Return
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub Lv_Data_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Data.DrawColumnHeader

        ' Background gradient
        Using bgBrush As New Drawing2D.LinearGradientBrush(
            e.Bounds,
            Color.FromArgb(245, 245, 245),
            Color.FromArgb(220, 220, 220),
            Drawing2D.LinearGradientMode.Vertical)

            e.Graphics.FillRectangle(bgBrush, e.Bounds)
        End Using

        ' Border bawah (lebih modern dari full border)
        Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
            e.Graphics.DrawLine(
                borderPen,
                e.Bounds.Left,
                e.Bounds.Bottom - 1,
                e.Bounds.Right,
                e.Bounds.Bottom - 1)
        End Using

        ' Teks header
        Using sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            sf.Trimming = StringTrimming.EllipsisCharacter

            ' Padding teks
            Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

            e.Graphics.DrawString(
                e.Header.Text,
                Lv_Data.Font,
                Brushes.Black,
                textRect,
                sf)
        End Using
    End Sub

    Private Sub Lv_Data_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Data.DrawSubItem
        Using sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Center

            If BodyAlignments.ContainsKey(e.ColumnIndex) Then
                sf.Alignment = BodyAlignments(e.ColumnIndex)
            Else
                sf.Alignment = StringAlignment.Near ' default
            End If

            If e.Item.Selected Then
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds)
                e.Graphics.DrawString(e.SubItem.Text, Lv_Data.Font, SystemBrushes.HighlightText, e.Bounds, sf)
            Else
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds)
                e.Graphics.DrawString(e.SubItem.Text, Lv_Data.Font, Brushes.Black, e.Bounds, sf)
            End If
        End Using
    End Sub

    Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_Data.Cursor = Cursors.Hand
        Else
            Lv_Data.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Lv_Data_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data.MouseLeave
        Lv_Data.Cursor = Cursors.Default
    End Sub

    Private Sub Lv_Detail_Barang_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Detail_Barang.DrawColumnHeader
        ' Background gradient
        Using bgBrush As New Drawing2D.LinearGradientBrush(
            e.Bounds,
            Color.FromArgb(245, 245, 245),
            Color.FromArgb(220, 220, 220),
            Drawing2D.LinearGradientMode.Vertical)

            e.Graphics.FillRectangle(bgBrush, e.Bounds)
        End Using

        ' Border bawah (lebih modern dari full border)
        Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
            e.Graphics.DrawLine(
                borderPen,
                e.Bounds.Left,
                e.Bounds.Bottom - 1,
                e.Bounds.Right,
                e.Bounds.Bottom - 1)
        End Using

        ' Teks header
        Using sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            sf.Trimming = StringTrimming.EllipsisCharacter

            ' Padding teks
            Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

            e.Graphics.DrawString(
                e.Header.Text,
                Lv_Detail_Barang.Font,
                Brushes.Black,
                textRect,
                sf)
        End Using
    End Sub

    Private Sub Lv_Detail_Barang_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Detail_Barang.DrawSubItem
        Using sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Center

            If BodyAlignments2.ContainsKey(e.ColumnIndex) Then
                sf.Alignment = BodyAlignments2(e.ColumnIndex)
            Else
                sf.Alignment = StringAlignment.Near ' default
            End If

            If e.Item.Selected Then
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds)
                e.Graphics.DrawString(e.SubItem.Text, Lv_Detail_Barang.Font, SystemBrushes.HighlightText, e.Bounds, sf)
            Else
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds)
                e.Graphics.DrawString(e.SubItem.Text, Lv_Detail_Barang.Font, Brushes.Black, e.Bounds, sf)
            End If
        End Using
    End Sub

    Private Sub Lv_Detail_Barang_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Detail_Barang.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_Detail_Barang.Cursor = Cursors.Hand
        Else
            Lv_Detail_Barang.Cursor = Cursors.Default
        End If
    End Sub
    Private Sub Lv_Detail_Barang_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Detail_Barang.MouseLeave
        Lv_Detail_Barang.Cursor = Cursors.Default
    End Sub

    Private Sub Lv_Detail_Barcode_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Detail_Barcode.DrawColumnHeader
        ' Background gradient
        Using bgBrush As New Drawing2D.LinearGradientBrush(
            e.Bounds,
            Color.FromArgb(245, 245, 245),
            Color.FromArgb(220, 220, 220),
            Drawing2D.LinearGradientMode.Vertical)

            e.Graphics.FillRectangle(bgBrush, e.Bounds)
        End Using

        ' Border bawah (lebih modern dari full border)
        Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
            e.Graphics.DrawLine(
                borderPen,
                e.Bounds.Left,
                e.Bounds.Bottom - 1,
                e.Bounds.Right,
                e.Bounds.Bottom - 1)
        End Using

        ' Teks header
        Using sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            sf.Trimming = StringTrimming.EllipsisCharacter

            ' Padding teks
            Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

            e.Graphics.DrawString(
                e.Header.Text,
                Lv_Detail_Barcode.Font,
                Brushes.Black,
                textRect,
                sf)
        End Using
    End Sub

    Private Sub Lv_Detail_Barcode_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Detail_Barcode.DrawSubItem
        Using sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Center

            If BodyAlignments3.ContainsKey(e.ColumnIndex) Then
                sf.Alignment = BodyAlignments3(e.ColumnIndex)
            Else
                sf.Alignment = StringAlignment.Near ' default
            End If

            If e.Item.Selected Then
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds)
                e.Graphics.DrawString(e.SubItem.Text, Lv_Detail_Barcode.Font, SystemBrushes.HighlightText, e.Bounds, sf)
            Else
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds)
                e.Graphics.DrawString(e.SubItem.Text, Lv_Detail_Barcode.Font, Brushes.Black, e.Bounds, sf)
            End If
        End Using
    End Sub
    Private Sub Lv_Detail_Barcode_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Detail_Barcode.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_Detail_Barcode.Cursor = Cursors.Hand
        Else
            Lv_Detail_Barcode.Cursor = Cursors.Default
        End If
    End Sub
    Private Sub Lv_Detail_Barcode_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Detail_Barcode.MouseLeave
        Lv_Detail_Barcode.Cursor = Cursors.Default
    End Sub



End Class
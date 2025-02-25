Public Class EMI_Display_Transaksi_Biaya_Lokal

    '=====================
    '=      CATATAN      =
    '=====================
    '= BM = Barang Masuk =
    '= MM = Mobil Masuk  =
    '=====================

    Dim arrFilter As New ArrayList

    Dim Lv_NoPO, Lv_NmSupplier, Lv_Keterangan, Lv_Tanggal, Lv_Jam, Lv_User, Lv_Lokasi, Lv_KdSupplier As String

    Dim itemPO_NoFak As Integer = 0
    Dim itemPO_NmSupplier As Integer = 1
    Dim itemPO_Keterangan As Integer = 2
    Dim itemPO_Tanggal As Integer = 3
    Dim itemPO_Jam As Integer = 4
    Dim itemPO_User As Integer = 5
    Dim itemPO_Lokasi As Integer = 6
    Dim itemPO_KdSupplier As Integer = 7

    Private Sub EMI_Display_Transaksi_Biaya_Lokal_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Display_Transaksi_Biaya_Lokal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Initial_Lv()
        Kosong()

    End Sub

    Private Sub Initial_Lv()

        'Listview PO
        Lv_PO.Columns.Clear()
        Lv_PO.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Supplier", 150, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Keterangan", 400, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("User", 130, HorizontalAlignment.Left)
        'Hide
        Lv_PO.Columns.Add("Lokasi", 0, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("KdSupplier", 0, HorizontalAlignment.Left)
        Lv_PO.View = View.Details

        'Listview PO
        Lv_BM.Columns.Clear()
        Lv_BM.Columns.Add("Lokasi", 120, HorizontalAlignment.Left)
        Lv_BM.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
        Lv_BM.Columns.Add("Nama", 270, HorizontalAlignment.Left)
        Lv_BM.Columns.Add("Jumlah PO", 110, HorizontalAlignment.Right)
        Lv_BM.Columns.Add("Jumlah Masuk", 110, HorizontalAlignment.Right)
        Lv_BM.Columns.Add("Selisih", 110, HorizontalAlignment.Right)
        Lv_BM.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_BM.View = View.Details

        'Listview PO
        Lv_Mm.Columns.Clear()
        Lv_Mm.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)
        Lv_Mm.Columns.Add("No SJ", 120, HorizontalAlignment.Left)
        Lv_Mm.Columns.Add("No Plat", 100, HorizontalAlignment.Left)
        Lv_Mm.Columns.Add("Driver", 170, HorizontalAlignment.Left)
        Lv_Mm.Columns.Add("Tanggal Masuk", 100, HorizontalAlignment.Center)
        Lv_Mm.Columns.Add("Jam Masuk", 100, HorizontalAlignment.Center)
        Lv_Mm.View = View.Details

    End Sub

    Private Sub Get_Data_PO(ByVal index As Integer)

        Lv_NoPO = Lv_PO.Items(index).SubItems(itemPO_NoFak).Text
        Lv_NmSupplier = Lv_PO.Items(index).SubItems(itemPO_NmSupplier).Text
        Lv_Keterangan = Lv_PO.Items(index).SubItems(itemPO_Keterangan).Text
        Lv_Tanggal = Lv_PO.Items(index).SubItems(itemPO_Tanggal).Text
        Lv_Jam = Lv_PO.Items(index).SubItems(itemPO_Jam).Text
        Lv_User = Lv_PO.Items(index).SubItems(itemPO_User).Text
        Lv_Lokasi = Lv_PO.Items(index).SubItems(itemPO_Lokasi).Text
        Lv_KdSupplier = Lv_PO.Items(index).SubItems(itemPO_KdSupplier).Text
    End Sub

    Public Sub Kosong()

        'Filter
        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Txt_ValueFilter.Enabled = False : Txt_ValueFilter.Text = ""
        Cmb_Filter.Items.Add("No Faktur") : arrFilter.Add("a.No_Faktur")
        Cmb_Filter.Items.Add("Supplier") : arrFilter.Add("b.Nama")
        Cmb_Filter.Items.Add("Keterangan") : arrFilter.Add("a.No_Nota")
        Cmb_Filter.Items.Add("User ID") : arrFilter.Add("a.UserID")

        Lv_PO.Items.Clear()
        Lv_BM.Items.Clear()
        Lv_Mm.Items.Clear()

        Load_Data_PO()

    End Sub

    Private Sub Load_Data_PO(ByVal Optional Filter As Boolean = False)
        Try
            OpenConn()

            '===================
            '=     LOAD PO     =
            '===================

            Lv_PO.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Faktur, b.Nama as Supplier, a.No_Nota as Keterangan, a.Tanggal, a.Jam, a.UserID, a.Lokasi, a.Kode_Supplier "
            SQL = SQL & "from emi_pembelian_PO a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and ( a.Flag_Import is null or a.Flag_Import <> 'Y') "
            'Todo : Jangan Lupa Di Uncomment
            'SQL = SQL & "and a.Flag_Selisih_BM = 'Y' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Flag_Biaya is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If Filter Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilter.Item(Cmb_Filter.SelectedIndex) & " like '%" & Trim(Txt_ValueFilter.Text) & "%' "
            End If
            SQL = SQL & "order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_PO.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Supplier"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("UserID"))
                    Lv.SubItems.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("Kode_Supplier"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_PO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PO.SelectedIndexChanged
        If Lv_PO.Items.Count = 0 Or Lv_PO.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFakturPO As String = Lv_PO.FocusedItem.SubItems(0).Text

            '=============================
            '=     LOAD BARANG MASUK     =
            '=============================
            Lv_BM.Items.Clear()

            SQL = "Select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, d.Nama, b.Harga, b.Satuan, "

            SQL = SQL & "sum(b.Jumlah) As Jumlah_PO, "

            SQL = SQL & "(dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, b.Satuan_Barang, b.Satuan, sum(c.Jumlah_Masuk) )) as Jumlah_Masuk, "

            SQL = SQL & "ISNULL(((dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, b.Satuan_Barang, b.Satuan, sum(c.Jumlah_Masuk) )) - sum(b.Jumlah)), 0) as Selisih_Barang "

            SQL = SQL & "From emi_pembelian_PO a, EMI_Pembelian_PO_Detail b, EMI_Pembelian_Loading_Detail c, barang d, EMI_Pembelian_Loading e "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
            SQL = SQL & "And b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Barang = c.Kode_Barang And b.No_Urut = c.Urut_PO And b.No_Faktur = c.No_PO "
            SQL = SQL & "And b.Kode_Perusahaan = d.Kode_Perusahaan And b.Kode_Stock_Owner = d.Kode_Stock_Owner And b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "And c.kode_Perusahaan=e.kode_Perusahaan And c.no_faktur=e.no_faktur "
            SQL = SQL & "And a.Status Is null And e.status Is null "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & SelectedFakturPO & "' "
            SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, d.Nama, b.Harga, b.Satuan, b.Satuan_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_BM.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_PO"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Masuk"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Selisih_Barang"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))

                Loop
            End Using

            '============================
            '=     LOAD MOBIL MASUK     =
            '============================
            Lv_Mm.Items.Clear()
            SQL = "select a.No_Faktur, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal_Masuk, a.Jam_Masuk "
            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.no_po= '" & SelectedFakturPO & "' "
            SQL = SQL & "group by a.No_Faktur, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal_Masuk, a.Jam_Masuk "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Mm.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_SJ"))
                    Lv.SubItems.Add(Dr("No_Plat"))
                    Lv.SubItems.Add(Dr("Driver"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Masuk")) = "", "-", Format(Dr("Tanggal_Masuk"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Masuk")) = "", "-", Dr("Jam_Masuk")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Jenis Filter Dahulu", "Transaksi Biaya Lokal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Filter.Focus() : Exit Sub
        Else
            If Txt_ValueFilter.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Tidak Boleh Kosong", "Transaksi Biaya Lokal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_ValueFilter.Focus() : Exit Sub
            End If
        End If

        Lv_PO.Items.Clear()
        Lv_BM.Items.Clear()
        Lv_Mm.Items.Clear()

        Load_Data_PO(True)
    End Sub

    Private Sub Lv_PO_DoubleClick(sender As Object, e As EventArgs) Handles Lv_PO.DoubleClick
        If Lv_PO.Items.Count = 0 Then Exit Sub

        If Lv_BM.Items.Count = 0 Then
            MessageBox.Show("Data Tidak Ada", "Transaksi Biaya Lokal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim selectedIndex As Integer = Lv_PO.FocusedItem.Index

        Get_Data_PO(selectedIndex)

        Dim JumlahPO As Double = 0
        Dim TotalBerat As Double = 0
        Dim JumlahMobil As Double = 0
        Try
            OpenConn()

            '========================================
            '=     CEK APAKAH PO SUDAH BERJALAN     =
            '========================================
            SQL = "select top 1 Kode_Perusahaan from EMI_Pembelian_Loading_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' and No_PO = '" & Lv_NoPO & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("PO Belum Berjalan", "Display Biaya Lokal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select sum(Jumlah) as Jumlah_PO , Satuan "
            SQL = SQL & "from EMI_Pembelian_PO_Detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & Lv_NoPO & "' "
            SQL = SQL & "group by Satuan "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    JumlahPO = Val(HilangkanTanda(Dr("Jumlah_PO")))
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Ada Masalah Terhadap PO")
                    Exit Sub
                End If
            End Using

            SQL = "select ROUND( sum(((ISNULL((c.Jumlah_Masuk), 0) * ISNULL((d.Berat), 1)) / 1000)), 2) as Total "
            SQL = SQL & "from emi_pembelian_PO a, EMI_Pembelian_PO_Detail b, EMI_Pembelian_Loading_Detail c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.No_Faktur = c.No_PO and b.No_Urut = c.Urut_PO "
            SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Lv_NoPO & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TotalBerat = Val(HilangkanTanda(Dr("Total")))
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Ada Masalah Saat Hitung Berat")
                    Exit Sub
                End If
            End Using

            SQL = "select count(distinct a.no_faktur) as Jumlah "
            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.no_po= '" & Lv_NoPO & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    JumlahMobil = Val(HilangkanTanda(Dr("Jumlah")))
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Ada Masalah Saat Hitung Berat")
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Transaksi_Biaya_Lokal.Kosong()
        Transaksi_Biaya_Lokal.Cmb_Lokasi.Text = Lv_Lokasi
        Transaksi_Biaya_Lokal.TxtNo_PO.Text = Lv_NoPO
        Transaksi_Biaya_Lokal.Txt_Keterangan.Text = Lv_Keterangan
        Transaksi_Biaya_Lokal.Txt_Kd_Supplier.Text = Lv_KdSupplier
        Transaksi_Biaya_Lokal.Txt_NmSupllier.Text = Lv_NmSupplier
        Transaksi_Biaya_Lokal.Txt_TglPO.Text = Lv_Tanggal
        Transaksi_Biaya_Lokal.Txt_User.Text = Lv_User
        Transaksi_Biaya_Lokal.Txt_JumlahPO.Text = Format(JumlahPO, "N2")
        Transaksi_Biaya_Lokal.Txt_Berat.Text = Format(TotalBerat, "N2")
        Transaksi_Biaya_Lokal.TxtJumlahMobil.Text = JumlahMobil
        Transaksi_Biaya_Lokal.ShowDialog()

    End Sub

    '=====================================================

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.SelectedIndex = -1 Then Exit Sub

        Txt_ValueFilter.Enabled = True : Txt_ValueFilter.Text = ""

        Transaksi_Biaya_Lokal.Kosong()
        Transaksi_Biaya_Lokal.Cmb_Lokasi.Text = ""

    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Kosong()
    End Sub

    Private Sub TanpaBiayaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TanpaBiayaToolStripMenuItem.Click

        If Lv_PO.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim selectedIndex As Integer = Lv_PO.FocusedItem.Index
            Get_Data_PO(selectedIndex)



            '====================================
            '=     UPATE HPP SATUAN DISPLAY     =
            '====================================
            SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.No_Urut, b.Nilai_Barang, b.Harga_Barang, b.Total "
            SQL = SQL & "from emi_pembelian_PO a, EMI_Pembelian_PO_Detail b, EMI_Pembelian_Loading_Detail c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_PO and b.No_Urut = c.Urut_PO "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Lv_NoPO & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "order by b.No_Urut"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim NoPO As String = .Rows(i).Item("No_Faktur")
                            Dim UrutPo As Integer = .Rows(i).Item("No_Urut")
                            Dim Biaya As Double = Val(HilangkanTanda(.Rows(i).Item("Total")))

                            SQL = "update EMI_Pembelian_Loading_Detail set HPP_Satuan_Display = '" & Math.Round(Biaya, 0) & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_PO = '" & NoPO & "' and Urut_PO = '" & UrutPo & "' "
                            ExecuteTrans(SQL)

                        Next

                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("PO Belum Berjalan", "Display Biaya Lokal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Kosong()
                        Exit Sub
                    End If
                End With
            End Using

            '============================
            '=     UPATE FLAG_BIAYA     =
            '============================
            SQL = "update emi_pembelian_PO set Flag_Biaya = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Lv_NoPO & "' "
            ExecuteTrans(SQL)


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Di Simpan", "DIspplay Biaya Lokal", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

End Class
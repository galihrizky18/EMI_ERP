Public Class EMI_Validasi_Pengajuan_Selesai_PR_Barang_Lain

    Dim arrCari As New ArrayList

    Dim Lv_NoPR, Lv_KdSo, Lv_KdBarang, Lv_NmBarang, Lv_Sisa, Lv_Satuan, Lv_UrutPR, Lv_TglDelivery, Lv_TglEstimasi As String

    Dim item_NoPR As Integer = 0
    Dim item_KdSO As Integer = 1
    Dim item_KdBarang As Integer = 2
    Dim item_NmBarang As Integer = 3
    Dim item_Sisa As Integer = 4
    Dim item_Satuan As Integer = 5
    Dim item_UrutPR As Integer = 6
    Dim item_TglDelivery As Integer = 7
    Dim item_TglEstimasi As Integer = 8

    Private Sub EMI_Validasi_Pengajuan_Selesai_PR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Kosong()
    End Sub

    Private Sub Kosong()

        Cmb_Order.Items.Clear()
        Cmb_Order.Items.Add("No PR") : arrCari.Add("no_faktur")
        Cmb_Order.Items.Add("Tanggal Delivery") : arrCari.Add("tanggal_delivery")
        Cmb_Order.Items.Add("Nama") : arrCari.Add("nama")

        Cari()

    End Sub

    Private Sub Cmb_Order_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Order.SelectedIndexChanged
        Cari()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Kosong()
    End Sub

    Private Sub Cari()

        get_jam()

        Try
            OpenConn()

            Dgv_Pr.Rows.Clear()

            SQL = "With cte As ( "
            SQL = SQL & "Select a.No_Faktur,b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,c.satuan As satuan_kecil_barang, "
            SQL = SQL & "b.Satuan, b.tanggal_delivery, b.no_urut, b.Jumlah, "

            SQL = SQL & "isnull((select  sum(y.Jumlah) from  EMI_Pembelian_PO_Induk_Barang_Lain x, EMI_Pembelian_PO_Det_Induk_Barang_Lain y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And "
            SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan And y.no_urut_pr = b.No_Urut And x.status Is null And x.Flag_Release Is null ),0) As jumlah_Sementara, "

            SQL = SQL & "isnull((select  sum(y.Jumlah) from  EMI_Pembelian_PO_Induk_Barang_Lain x, EMI_Pembelian_PO_Det_Induk_Barang_Lain y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And "
            SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan And y.no_urut_pr = b.No_Urut And x.status Is null And x.Flag_Release ='Y' ),0) as jumlah_Release, "

            SQL = SQL & "ISNULL((select waktu_pabrikasi from emi_detail_proses_pengiriman_po_Barang_Lain x, Suppliers y where "
            SQL = SQL & "b.Kode_Perusahaan = x.Kode_Perusahaan And b.Kode_Barang = x.kode_barang And "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.Id_Kategori_Supplier = y.ID_Kategori_Suppliers "
            SQL = SQL & "And y.Kode_Supplier = '" & EMI_PO_Pembelian.TxtPO_KdSupplier.Text & "'),0) as Waktu_Pabrikasi,  "

            SQL = SQL & "ISNULL((select  Waktu_Pengiriman from emi_detail_proses_pengiriman_po_Barang_Lain x, Suppliers y "
            SQL = SQL & "where b.Kode_Perusahaan = x.Kode_Perusahaan And b.Kode_Barang = x.kode_barang And "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.Id_Kategori_Supplier = y.ID_Kategori_Suppliers "
            SQL = SQL & "And y.Kode_Supplier = '" & EMI_PO_Pembelian.TxtPO_KdSupplier.Text & "' ),0) as Waktu_Pengiriman "

            SQL = SQL & "From EMI_Purchase_Requisition_Barang_Lain a, EMI_Purchase_Requisition_Barang_Lain_Detail b , barang_Lain c, Emi_Role_Kategori_PO d "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Barang = c.Kode_Barang And "
            SQL = SQL & "b.Kode_Stock_Owner = c.Kode_Stock_Owner And a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null "
            SQL = SQL & " And flag_release = 'Y' and c.kode_Perusahaan=d.kode_Perusahaan and "
            SQL = SQL & "c.id_kategori_PO = d.kategori_po And d.userid = '" & UserID & "' and b.flag_sudah_po is null and b.Flag_Pengajuan_Selesai = 'Y' "
            SQL = SQL & ") "

            SQL = SQL & "Select No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama, satuan_kecil_barang, Satuan, Tanggal_Delivery, No_Urut, "
            SQL = SQL & "jumlah-(jumlah_Sementara + jumlah_Release) As Jumlah, Waktu_Pabrikasi, Waktu_Pengiriman, "
            SQL = SQL & "DateDiff(Day, Tanggal_Delivery, DateAdd(Day, Waktu_Pabrikasi + Waktu_Pengiriman, '" & Format(tgl_skg, "yyyy-MM-dd") & "') ) as  Waktu_Proses_Pengiriman, "
            SQL = SQL & "DateAdd(Day, Waktu_Pabrikasi + Waktu_Pengiriman, '" & Format(tgl_skg, "yyyy-MM-dd") & "') as tanggal_actual_delivery "

            SQL = SQL & "From cte "
            SQL = SQL & "Where jumlah - (jumlah_Sementara + jumlah_Release) <> 0 "
            If Cmb_Order.SelectedIndex = -1 Then
                SQL = SQL & "order by no_faktur"
            Else
                SQL = SQL & "order by " & arrCari.Item(Cmb_Order.SelectedIndex) & " "
            End If
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dgv_Pr.Rows.Add(1)

                        Dgv_Pr.Rows(i).Cells(item_NoPR).Value = .Rows(i).Item("No_Faktur")
                        Dgv_Pr.Rows(i).Cells(item_KdSO).Value = .Rows(i).Item("Kode_Stock_Owner")
                        Dgv_Pr.Rows(i).Cells(item_KdBarang).Value = .Rows(i).Item("Kode_Barang")
                        Dgv_Pr.Rows(i).Cells(item_NmBarang).Value = .Rows(i).Item("Nama")
                        Dgv_Pr.Rows(i).Cells(item_Sisa).Value = Format(.Rows(i).Item("jumlah"), "N2")
                        Dgv_Pr.Rows(i).Cells(item_Satuan).Value = .Rows(i).Item("Satuan")
                        Dgv_Pr.Rows(i).Cells(item_UrutPR).Value = .Rows(i).Item("No_Urut")
                        Dgv_Pr.Rows(i).Cells(item_TglDelivery).Value = Format(.Rows(i).Item("Tanggal_Delivery"), "dd MMM yyyy")
                        Dgv_Pr.Rows(i).Cells(item_TglEstimasi).Value = Format(.Rows(i).Item("tanggal_actual_delivery"), "dd MMM yyyy")

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

    Private Sub GetLvData(ByVal index As Integer)

        Lv_NoPR = Dgv_Pr.Rows(index).Cells(item_NoPR).Value
        Lv_KdSo = Dgv_Pr.Rows(index).Cells(item_KdSO).Value
        Lv_KdBarang = Dgv_Pr.Rows(index).Cells(item_KdBarang).Value
        Lv_NmBarang = Dgv_Pr.Rows(index).Cells(item_NmBarang).Value
        Lv_Sisa = Dgv_Pr.Rows(index).Cells(item_Sisa).Value
        Lv_Satuan = Dgv_Pr.Rows(index).Cells(item_Satuan).Value
        Lv_UrutPR = Dgv_Pr.Rows(index).Cells(item_UrutPR).Value
        Lv_TglDelivery = Dgv_Pr.Rows(index).Cells(item_TglDelivery).Value
        Lv_TglEstimasi = Dgv_Pr.Rows(index).Cells(item_TglEstimasi).Value

    End Sub

    Private Sub TerimaPengajuanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TerimaPengajuanToolStripMenuItem.Click
        If Dgv_Pr.Rows.Count = 0 OrElse Dgv_Pr.CurrentRow Is Nothing OrElse Dgv_Pr.CurrentRow.Index = -1 Then Exit Sub

        Dim selectedIndex As Integer = Dgv_Pr.CurrentRow.Index

        GetLvData(selectedIndex)

        get_jam()
        Try
            OpenConn()

            If CekButtonRole("Validasi_Batal_PR_Barang_Lain") = "T" Then
                CloseConn()
                MessageBox.Show("User Tidak Ada Akses Validasi Pengajuan Selesai PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SQL = "update EMI_Purchase_Requisition_Barang_Lain_Detail set "
            SQL = SQL & "Flag_Sudah_PO = 'Y', Flag_Pengajuan_Selesai = NULL, Tgl_Validasi_Pengajuan = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "Jam_Validasi_Pengajuan = '" & Format(tgl_skg, "HH:mm:ss") & "', User_Validasi_Pengajuan = '" & UserID & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & Lv_NoPR & "' "
            SQL = SQL & "and No_Urut= '" & Lv_UrutPR & "' "
            ExecuteTrans(SQL)

            CloseConn()
            MessageBox.Show("Data Telah diSimpan", "Validasi Penyelesaian PR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Kosong()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

End Class
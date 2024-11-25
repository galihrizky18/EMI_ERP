Public Class Emi_Display_Request_Material
    Dim Jenis = "Emi_Display_Request_Material"
    Public lokasi_kirim As String
    Dim lv_kodeSO, lv_KdBrg, lv_NmBrg, lv_Jenis, lv_TglPermintaan, lv_JamPermintaan, lv_Jumlah, lv_Satuan, lv_UserInput, lv_Warna, lv_GoodStock, lv_SatuanBesar, lv_SatuanDisplay, lv_JmlBags, lv_SatuanBags, Lv_Oto As String

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        lv_kodeSO = Lv_Data.Items(NoIndex).Text
        lv_KdBrg = Lv_Data.Items(NoIndex).SubItems(1).Text
        lv_NmBrg = Lv_Data.Items(NoIndex).SubItems(2).Text
        lv_Jenis = Lv_Data.Items(NoIndex).SubItems(3).Text
        lv_TglPermintaan = Lv_Data.Items(NoIndex).SubItems(4).Text
        lv_JamPermintaan = Lv_Data.Items(NoIndex).SubItems(5).Text
        lv_Jumlah = Lv_Data.Items(NoIndex).SubItems(6).Text
        lv_Satuan = Lv_Data.Items(NoIndex).SubItems(7).Text
        lv_UserInput = Lv_Data.Items(NoIndex).SubItems(8).Text
        lv_Warna = Lv_Data.Items(NoIndex).SubItems(9).Text
        lv_GoodStock = Lv_Data.Items(NoIndex).SubItems(10).Text
        lv_SatuanBesar = Lv_Data.Items(NoIndex).SubItems(11).Text
        lv_SatuanDisplay = Lv_Data.Items(NoIndex).SubItems(12).Text
        lv_JmlBags = Lv_Data.Items(NoIndex).SubItems(13).Text
        lv_SatuanBags = Lv_Data.Items(NoIndex).SubItems(14).Text
        Lv_Oto = Lv_Data.Items(NoIndex).SubItems(15).Text
    End Sub

    Private Sub Emi_Display_Request_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Barang", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Nama Barang", 250, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jenis", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tanggal Permintaan", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam Permintaan", 0, HorizontalAlignment.Center) 'HIDE
        Lv_Data.Columns.Add("Jumlah", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User Input", 0, HorizontalAlignment.Center) 'HIDE
        Lv_Data.Columns.Add("Warna", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Stock", 0, HorizontalAlignment.Center) 'HIDE
        Lv_Data.Columns.Add("Satuan Besar", 0, HorizontalAlignment.Center) 'HIDE
        Lv_Data.Columns.Add("Satuan Display", 0, HorizontalAlignment.Center) 'HIDE
        Lv_Data.Columns.Add("Jumlah Bags", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Satuan Bags", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Urut Oto", 0, HorizontalAlignment.Center) 'HIDE
        Lv_Data.View = View.Details

        Kosong()
    End Sub

    Private Sub Kosong()
        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select c.Kode_Stock_Owner, c.Kode_Barang, d.Nama, b.Kode_Group_Jenis, a.Tanggal, a.Jam, c.Jumlah, c.Satuan, a.UserId, c.warna, "
            SQL = SQL & "dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, d.satuan, c.satuan, d.good_stock) as Good_Stock, d.Satuan, c.Satuan as Satuan_Display, "
            SQL = SQL & "ISNULL(d.Jumlah_Bags, 0) as Jumlah_Bags, d.Satuan_Isi_Bags, c.Urut_Oto "
            SQL = SQL & "from Emi_Material_Requisition a, EMI_Group_Jenis b, Emi_Material_Requisition_Det_Convert c, barang d  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Faktur = c.No_Faktur "
            SQL = SQL & "and c.kode_barang = d.kode_barang and d.kode_stock_owner='" & lokasi_kirim & "' "
            SQL = SQL & "and a.Flag_Process = 'Y' and a.status is null "
            SQL = SQL & "and c.Flag_Transfer is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_Data.Items.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("Kode_Group_Jenis"))
                    lv.SubItems.Add(Dr("Tanggal"))
                    lv.SubItems.Add(Dr("Jam"))
                    lv.SubItems.Add(Dr("Jumlah"))
                    lv.SubItems.Add(Dr("Satuan"))
                    lv.SubItems.Add(Dr("UserId"))
                    lv.SubItems.Add(Dr("warna"))
                    lv.SubItems.Add(Dr("Good_Stock"))
                    lv.SubItems.Add(Dr("Satuan"))
                    lv.SubItems.Add(Dr("Satuan_Display"))
                    lv.SubItems.Add(Dr("Jumlah_Bags"))
                    If General_Class.CekNULL(Dr("Satuan_Isi_Bags")) = "" Then
                        lv.SubItems.Add("-")
                    Else
                        lv.SubItems.Add(Dr("Satuan_Isi_Bags"))
                    End If
                    lv.SubItems.Add(Dr("Urut_Oto"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data.SelectedIndexChanged

    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        'Transfer_Stock_3.kosong()
        Transfer_Stock_3.CmbJnsTransfer.Enabled = False
        Transfer_Stock_3.CmbSO_Asal.Enabled = False
        Transfer_Stock_3.CmbSo_Tujuan.Enabled = False

        Transfer_Stock_3.TxtKd_Barang.Text = String.Empty
        Transfer_Stock_3.Txt_SO.Text = String.Empty
        Transfer_Stock_3.TxtNm_Barang.Text = String.Empty

        Get_Isi_ListView(Lv_Data.FocusedItem.Index)
        Transfer_Stock_3.asal = Jenis
        Transfer_Stock_3.Lv_DetBarang.Visible = False
        Transfer_Stock_3.TxtKd_Barang.Enabled = False
        'Transfer_Stock_3.Btn_GetData.Enabled = False
        Transfer_Stock_3.TxtKd_Barang.Text = lv_KdBrg
        Transfer_Stock_3.TxtNm_Barang.Text = lv_NmBrg
        Transfer_Stock_3.Txt_SO.Text = lv_kodeSO
        Transfer_Stock_3.TxtSatuanKecil.Text = lv_SatuanDisplay
        Transfer_Stock_3.Txt_Warna.Text = lv_Warna
        Transfer_Stock_3.TxtStock.Text = lv_GoodStock
        Transfer_Stock_3.TxtSatuan.Text = lv_SatuanBesar
        Transfer_Stock_3.TxtBags.Text = lv_JmlBags
        Transfer_Stock_3.Cmb_Warna.SelectedItem = lv_Warna

        Transfer_Stock_3.Txt_JumlahPermintaan.Text = lv_Jumlah
        Transfer_Stock_3.Txt_SatuanPermintaan.Text = lv_Satuan
        Transfer_Stock_3.Txt_OtoMaterial_req.Text = Lv_Oto
        'Transfer_Stock_3.Btn_Insert_Click(Lv_Data, e)
        Transfer_Stock_3.DGV_Data_TF.Rows.Clear()
        Me.Close()
    End Sub
End Class
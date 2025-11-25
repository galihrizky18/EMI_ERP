Public Class N_EMI_SD_List_Keep_Stock_Barang_Lain
    Dim Jenis = "Emi_Display_Request_Material"
    Public lokasi_kirim, asal As String

    Dim Lv_NO_PR, Lv_No_Keep, Lv_KdSO, Lv_KdBarang, Lv_NmBarang, Lv_Keterangan, Lv_TglKeep, Lv_Jumlah, Lv_JumlahTf, Lv_Satuan, Lv_UrutKeep, lv_MetPotStock, Lv_JnsKemasan, Lv_Stock As String

    Dim item_NoPR As Integer = 0
    Dim item_No_Keep As Integer = 1
    Dim item_KDSO As Integer = 2
    Dim item_KdBarang As Integer = 3
    Dim item_NmBarang As Integer = 4
    Dim item_Keterangan As Integer = 5
    Dim item_Tgl_Keep As Integer = 6
    Dim item_Jumlah As Integer = 7
    Dim item_JumlahTf As Integer = 8
    Dim item_Satuan As Integer = 9
    'Hide
    Dim item_UrutKeep As Integer = 10
    Dim item_MetPotStock As Integer = 11
    Dim item_jnsKemasan As Integer = 12
    Dim item_Stock As Integer = 13




    Dim Flag_Opname As Boolean

    Dim DefaultLimit As Double = 20

    Dim arrFilter As New ArrayList

    ' Variabel Global
    Dim PageSize As Integer = 5
    Dim CurrentPage As Integer = 1
    Dim TotalRows As Integer
    Dim totalpage As Integer = 10



    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click

        Dim filter As Boolean = False
        If Cmb_Filter.SelectedIndex > 0 Then
            If Txt_Value_Filter.Text.Trim.Length <> 0 Then
                filter = True
            End If
        End If

        If CurrentPage < totalpage Then
            CurrentPage += 1
            Kosong(filter, CurrentPage)


        End If

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If

    End Sub



    Private Sub BtnPrev_Click(sender As Object, e As EventArgs) Handles BtnPrev.Click

        Dim filter As Boolean = False
        If Cmb_Filter.SelectedIndex > 0 Then
            If Txt_Value_Filter.Text.Trim.Length <> 0 Then
                filter = True
            End If
        End If

        If CurrentPage > 1 Then
            CurrentPage -= 1
            Kosong(filter, CurrentPage)
        End If

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If

    End Sub

    Private Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles BtnFirst.Click


        Dim filter As Boolean = False
        If Cmb_Filter.SelectedIndex > 0 Then
            If Txt_Value_Filter.Text.Trim.Length <> 0 Then
                filter = True
            End If
        End If

        CurrentPage = 1
        Kosong(filter, CurrentPage)

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If

    End Sub


    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)

        Lv_NO_PR = Lv_Data.Items(NoIndex).SubItems(item_NoPR).Text
        Lv_No_Keep = Lv_Data.Items(NoIndex).SubItems(item_No_Keep).Text
        Lv_KdSO = Lv_Data.Items(NoIndex).SubItems(item_KdSO).Text
        Lv_KdBarang = Lv_Data.Items(NoIndex).SubItems(item_KdBarang).Text
        Lv_NmBarang = Lv_Data.Items(NoIndex).SubItems(item_NmBarang).Text
        Lv_Keterangan = Lv_Data.Items(NoIndex).SubItems(item_Keterangan).Text
        Lv_TglKeep = Lv_Data.Items(NoIndex).SubItems(item_Tgl_Keep).Text
        Lv_Jumlah = Lv_Data.Items(NoIndex).SubItems(item_Jumlah).Text
        Lv_JumlahTf = Lv_Data.Items(NoIndex).SubItems(item_JumlahTF).Text
        Lv_Satuan = Lv_Data.Items(NoIndex).SubItems(item_Satuan).Text
        Lv_UrutKeep = Lv_Data.Items(NoIndex).SubItems(item_UrutKeep).Text
        lv_MetPotStock = Lv_Data.Items(NoIndex).SubItems(item_MetPotStock).Text
        Lv_JnsKemasan = Lv_Data.Items(NoIndex).SubItems(item_jnsKemasan).Text
        Lv_Stock = Lv_Data.Items(NoIndex).SubItems(item_Stock).Text

    End Sub

    Private Sub Emi_Display_Request_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No PR", 150, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("No Keep Stock", 0, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Lokasi", 120, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '3
        Lv_Data.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left) '4
        Lv_Data.Columns.Add("Keterangan", 300, HorizontalAlignment.Left) '5
        Lv_Data.Columns.Add("Tanggal Keep", 130, HorizontalAlignment.Center) '6
        Lv_Data.Columns.Add("Jumlah", 110, HorizontalAlignment.Right) '7
        Lv_Data.Columns.Add("Jumlah TF", 110, HorizontalAlignment.Right) '8
        Lv_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center) '9
        'HIDE
        Lv_Data.Columns.Add("Urut Keep", 0, HorizontalAlignment.Center) '10
        Lv_Data.Columns.Add("MetPotSotck", 0, HorizontalAlignment.Center) '11
        Lv_Data.Columns.Add("JnsKemasan", 0, HorizontalAlignment.Center) '12
        Lv_Data.Columns.Add("Stock", 0, HorizontalAlignment.Center) '13

        Lv_Data.View = View.Details

        Txt_Limit.Text = DefaultLimit

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add(OpsiSeluruh) : arrFilter.Add(OpsiSeluruh)
        Cmb_Filter.Items.Add("No Split") : arrFilter.Add("c.No_Faktur")
        Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
        Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("e.Nama")
        Cmb_Filter.SelectedIndex = 0

        CmbOrder.Items.Clear()
        CmbOrder.Items.Add("ASC")
        CmbOrder.Items.Add("DESC")
        CmbOrder.SelectedIndex = 0

        Kosong()
    End Sub

    Private Sub Kosong(Optional ByVal filter As Boolean = False, Optional ByVal page As Integer = 1)

        If filter = True Then
            If Cmb_Filter.SelectedIndex <> 1 Then
                PageSize = 22
            Else
                PageSize = 5
            End If
        End If


        Try
            OpenConn()

            SQL = "select Flag_Opname from init where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Opname")) = "Y" Then
                        Flag_Opname = True
                    Else
                        Flag_Opname = False
                    End If
                End If
            End Using
            Lv_Data.Items.Clear()

            Dim offset As Integer = (page - 1) * PageSize

            Dim Filtered As String = ""
            If Not Chk_Belum_Selesai.Checked Then
                If filter Then
                    If Cmb_Filter.SelectedIndex <> 0 Then
                        Filtered = "and " & arrFilter(Cmb_Filter.SelectedIndex) & " like '%" & Txt_Value_Filter.Text.Trim & "%'  "
                    End If
                End If
            End If


            SQL = $"
                ;with cte as (
	                 select a.Kode_Perusahaan, a.user_id, b.Kode_Stock_Owner_Gudang, d.id_sub_kategori_jenis, d.id_kategori_jenis  
	                from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a  
	                 inner join N_EMI_Master_Kategori_Gudang_Barang_Lain b on a.kode_perusahaan = b.kode_perusahaan and a.id_kategori_gudang = b.urut_oto  
	                 inner join N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain c on b.kode_perusahaan = c.kode_perusahaan and c.id_kategori_gudang = b.urut_oto  
	                 inner join N_EMI_Master_Sub_Kategori_Jenis d on c.kode_perusahaan = d.kode_perusahaan and c.id_sub_kategori_jenis = d.id_sub_kategori_jenis  
	                where a.status is null and b.status is null and c.status is null
                 )
                select c.No_Faktur as No_PR, a.No_Faktur as No_KeepStock, a.Kode_Stock_Owner, a.Kode_Barang, e.Nama as Nama_Barang, c.Keterangan, a.Tanggal as Tanggal_Keep, a.Jumlah, a.Satuan, a.Urut_Oto,
                    e.Good_Stock, e.Metode_Pengeluaran_Stok, e.Jenis_Kemasan, 
                    isnull((
		                    select sum(r.Jumlah)
		                    from N_EMI_Transfer_Stock_Barang_Lain z
			                    inner join N_EMI_Transfer_Stock_Barang_Lain_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
			                    inner join N_EMI_Transfer_Stock_Barang_Lain_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.urut_tf
			                    inner join N_EMI_Transfer_Stock_Barang_Lain_Det2 r on y.kode_perusahaan = r.Kode_Perusahaan and y.No_Faktur = r.No_Faktur and y.Urut_Oto = r.Urut_Det
		                    where z.Kode_Perusahaan = a.Kode_Perusahaan
		                    and z.Status is null
		                    and z.so_awal = a.Kode_Stock_Owner
		                    and x.Kode_Barang = a.Kode_Barang
		                    and x.Urut_Keep_Stock = a.Urut_Oto
	                    ), 0) as Jumlah_TF
                from N_EMI_Keep_Stock_Barang_Lain_Departement a
	                inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Departement = b.No_Urut
	                inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and c.Status is null
	                inner join Barang_Lain e on a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang
                    inner join View_Kategori_Turunan f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.Id_Sub_Kategori_Jenis_3 = f.Id_Sub_Kategori_Jenis_3
	                inner join cte g on f.Kode_Perusahaan = g.Kode_Perusahaan and f.Id_Kategori_Jenis = g.Id_Kategori_Jenis and f.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.Kode_Stock_Owner_Gudang = e.Kode_Stock_Owner
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Status is null
                and a.Flag_Selesai_Pengeluaran_Barang is null
                and a.Kode_Stock_Owner = '{lokasi_kirim}'
                and g.User_ID = '{UserID}'
                {Filtered}
                order by a.Tanggal DESC, a.Jam ASC
                OFFSET {offset} ROWS
                FETCH NEXT {PageSize} ROWS ONLY
            "
            Using Dr = OpenTrans(SQL)

                ' Gunakan list buffer agar AddRange() bisa dipakai
                Dim items As New List(Of ListViewItem)

                Do While Dr.Read
                    Dim lv As New ListViewItem(Dr("No_PR").ToString()) '0
                    lv.SubItems.Add(Dr("No_KeepStock").ToString()) '1
                    lv.SubItems.Add(Dr("Kode_Stock_Owner").ToString()) '2
                    lv.SubItems.Add(Dr("Kode_Barang").ToString()) '3
                    lv.SubItems.Add(Dr("Nama_Barang").ToString()) '4
                    lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")).ToString()) '5
                    lv.SubItems.Add(Format(Dr("Tanggal_Keep"), "dd MMM yyyy")) '6
                    lv.SubItems.Add(Format(Dr("Jumlah"), "N4")) '7
                    lv.SubItems.Add(Format(Dr("Jumlah_TF"), "N4")) '8
                    lv.SubItems.Add(Dr("Satuan").ToString()) '9

                    ' Kolom tambahan (hidden / internal)
                    lv.SubItems.Add(Dr("Urut_Oto").ToString()) '10
                    lv.SubItems.Add(Dr("Metode_Pengeluaran_Stok").ToString()) '11
                    lv.SubItems.Add(Dr("Jenis_Kemasan").ToString()) '12
                    lv.SubItems.Add(Dr("Good_Stock").ToString()) '13

                    ' Tambahkan ke buffer
                    items.Add(lv)
                Loop

                ' --- tampilkan sekaligus di ListView ---
                Lv_Data.BeginUpdate()
                Lv_Data.Items.Clear()
                Lv_Data.Items.AddRange(items.ToArray())
                Lv_Data.EndUpdate()

            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If

    End Sub

    Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Limit.Text.Trim.Length = 0 Then
            MessageBox.Show("Limit Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Limit.Focus()
            Exit Sub
        End If

        Chk_Belum_Selesai.Checked = False
        Cmb_Filter.SelectedIndex = 0

        Kosong()

        Lv_Data.Focus()
    End Sub
    Private Sub Btn_cari_Click(sender As Object, e As EventArgs) Handles Btn_cari.Click
        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Harap Pilih Filter Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        If Cmb_Filter.SelectedIndex > 0 Then
            If Txt_Value_Filter.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Harus Diisi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Value_Filter.Focus()
                Exit Sub
            End If
        End If

        CurrentPage = 1
        Kosong(True, CurrentPage)

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If
    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If asal = "TF_Stock" Then

            'N_EMI_Transfer_Stock_Barang_Lain.kosong()
            N_EMI_Transfer_Stock_Barang_Lain.CmbJnsTransfer.Enabled = False
            N_EMI_Transfer_Stock_Barang_Lain.CmbSO_Asal.Enabled = False
            N_EMI_Transfer_Stock_Barang_Lain.CmbSo_Tujuan.Enabled = False

            N_EMI_Transfer_Stock_Barang_Lain.TxtKd_Barang.Text = String.Empty
            N_EMI_Transfer_Stock_Barang_Lain.Txt_SO.Text = String.Empty
            N_EMI_Transfer_Stock_Barang_Lain.TxtNm_Barang.Text = String.Empty

            Get_Isi_ListView(Lv_Data.FocusedItem.Index)
            N_EMI_Transfer_Stock_Barang_Lain.asal = Jenis
            N_EMI_Transfer_Stock_Barang_Lain.Lv_DetBarang.Visible = False
            N_EMI_Transfer_Stock_Barang_Lain.TxtKd_Barang.Enabled = False
            'N_EMI_Transfer_Stock_Barang_Lain.Btn_GetData.Enabled = False
            N_EMI_Transfer_Stock_Barang_Lain.TxtKd_Barang.Text = Lv_KdBarang
            N_EMI_Transfer_Stock_Barang_Lain.TxtNm_Barang.Text = Lv_NmBarang
            N_EMI_Transfer_Stock_Barang_Lain.Txt_SO.Text = Lv_KdSO
            N_EMI_Transfer_Stock_Barang_Lain.TxtSatuanKecil.Text = Lv_Satuan
            N_EMI_Transfer_Stock_Barang_Lain.Txt_Warna.Text = "-"
            N_EMI_Transfer_Stock_Barang_Lain.TxtStock.Text = Format(Lv_Stock, "N4")
            N_EMI_Transfer_Stock_Barang_Lain.TxtSatuan.Text = Lv_Satuan
            N_EMI_Transfer_Stock_Barang_Lain.TxtBags.Text = Format(Val(HilangkanTanda(0)), "N4")

            N_EMI_Transfer_Stock_Barang_Lain.TxtJenisBags.Text = Lv_JnsKemasan

            'N_EMI_Transfer_Stock_Barang_Lain.Cmb_Warna.SelectedItem = lv_Warna
            N_EMI_Transfer_Stock_Barang_Lain.Cmb_Warna.SelectedIndex = 2


            N_EMI_Transfer_Stock_Barang_Lain.TxtStockDisplay.Text = Format(Val(HilangkanTanda(Lv_Stock)), "N4") + " " + Lv_Satuan

            Dim Jumlah_Permintaan As Double = Lv_Jumlah - lv_JumlahTF

            N_EMI_Transfer_Stock_Barang_Lain.Txt_JumlahPermintaan.Text = Format(Jumlah_Permintaan, "N4")
            N_EMI_Transfer_Stock_Barang_Lain.Txt_SatuanPermintaan.Text = Lv_Satuan
            N_EMI_Transfer_Stock_Barang_Lain.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4") + " " + Lv_Satuan
            N_EMI_Transfer_Stock_Barang_Lain.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4"))
            N_EMI_Transfer_Stock_Barang_Lain.Txt_OtoMaterial_req.Text = Lv_UrutKeep
            N_EMI_Transfer_Stock_Barang_Lain.Txt_Jenis_Transfer.Text = "PRODUKSI"
            N_EMI_Transfer_Stock_Barang_Lain.TxtMetPotStok.Text = lv_MetPotStock
            N_EMI_Transfer_Stock_Barang_Lain.Txt_Urut_Request.Text = Lv_UrutKeep


            'Try
            '    OpenConn()

            '    '======================
            '    '=     GET SATUAN     =
            '    '======================
            '    N_EMI_Transfer_Stock_Barang_Lain.Cmb_Satuan_Barang.Items.Clear()
            '    SQL = "select Satuan, Flag_Default from N_EMI_Master_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & lv_KdBrg & "' order by Satuan"
            '    Using Dr = OpenTrans(SQL)
            '        Do While Dr.Read
            '            N_EMI_Transfer_Stock_Barang_Lain.Cmb_Satuan_Barang.Items.Add(Dr("Satuan"))
            '            If General_Class.CekNULL(Dr("Flag_Default")) = "Y" Then
            '                N_EMI_Transfer_Stock_Barang_Lain.Cmb_Satuan_Barang.Text = Dr("Satuan")
            '            End If
            '        Loop
            '    End Using

            '    CloseConn()
            'Catch ex As Exception
            '    CloseConn()
            '    MessageBox.Show(ex.Message)
            '    Exit Sub
            'End Try

            'N_EMI_Transfer_Stock_Barang_Lain.Cmb_Satuan_Barang.DroppedDown = True
            'N_EMI_Transfer_Stock_Barang_Lain.Cmb_Satuan_Barang.Focus()




            'N_EMI_Transfer_Stock_Barang_Lain.Btn_Insert_Click(Lv_Data, e)
            'N_EMI_Transfer_Stock_Barang_Lain.DGV_Data_TF.Rows.Clear()

            N_EMI_Transfer_Stock_Barang_Lain.isProduction = True
            N_EMI_Transfer_Stock_Barang_Lain.Btn_Insert_Click(Lv_Data, e)

        ElseIf asal = "Split_Stock" Then


            ''N_EMI_Transfer_Stock_Barang_Lain.kosong()
            'Emi_Split_Stock_QC.CmbJnsTransfer.Enabled = False
            'Emi_Split_Stock_QC.CmbSO_Asal.Enabled = False
            'Emi_Split_Stock_QC.CmbSo_Tujuan.Enabled = False

            'Emi_Split_Stock_QC.TxtKd_Barang.Text = String.Empty
            'Emi_Split_Stock_QC.Txt_SO.Text = String.Empty
            'Emi_Split_Stock_QC.TxtNm_Barang.Text = String.Empty

            'Get_Isi_ListView(Lv_Data.FocusedItem.Index)
            'Emi_Split_Stock_QC.asal = Jenis
            'Emi_Split_Stock_QC.Lv_DetBarang.Visible = False
            'Emi_Split_Stock_QC.TxtKd_Barang.Enabled = False
            ''N_EMI_Transfer_Stock_Barang_Lain.Btn_GetData.Enabled = False
            'Emi_Split_Stock_QC.TxtKd_Barang.Text = lv_KdBrg
            'Emi_Split_Stock_QC.TxtNm_Barang.Text = lv_NmBrg
            'Emi_Split_Stock_QC.Txt_SO.Text = lv_kodeSO
            'Emi_Split_Stock_QC.TxtSatuanKecil.Text = lv_Satuan
            'Emi_Split_Stock_QC.Txt_Warna.Text = lv_Warna
            'Emi_Split_Stock_QC.TxtStock.Text = Format(lv_GoodStock, "N4")
            'Emi_Split_Stock_QC.TxtSatuan.Text = lv_SatuanDisplay
            'Emi_Split_Stock_QC.TxtBags.Text = Format(Val(HilangkanTanda(lv_JmlBags)), "N4")
            'Emi_Split_Stock_QC.TxtJenisBags.Text = LvJenisKemasan
            'Emi_Split_Stock_QC.TxtMetPotStok.Text = Lv_MetPotStok
            'Emi_Split_Stock_QC.Cmb_Warna.SelectedItem = lv_Warna
            'Emi_Split_Stock_QC.TxtStockDisplay.Text = Format(Val(HilangkanTanda(lv_GoodStock)), "N4") + " " + lv_SatuanDisplay

            'Dim Jumlah_Permintaan As Double = lv_Jumlah - lv_JumlahTF

            'Emi_Split_Stock_QC.Txt_JumlahPermintaan.Text = Format(Jumlah_Permintaan, "N4")
            'Emi_Split_Stock_QC.Txt_SatuanPermintaan.Text = lv_SatuanDisplay
            'Emi_Split_Stock_QC.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4") + " " + lv_SatuanDisplay
            'Emi_Split_Stock_QC.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4"))
            'Emi_Split_Stock_QC.Txt_OtoMaterial_req.Text = Lv_Oto
            'Emi_Split_Stock_QC.Txt_Jenis_Transfer.Text = "PRODUKSI"
            'Emi_Split_Stock_QC.Txt_Urut_Request.Text = Lv_Oto

            'Try
            '    OpenConn()

            '    '======================
            '    '=     GET SATUAN     =
            '    '======================
            '    Emi_Split_Stock_QC.Cmb_Satuan_Barang.Items.Clear()
            '    SQL = "select Satuan, flag_dasar from N_EMI_Master_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & lv_KdBrg & "' order by Satuan"
            '    Using Dr = OpenTrans(SQL)
            '        Do While Dr.Read
            '            Emi_Split_Stock_QC.Cmb_Satuan_Barang.Items.Add(Dr("Satuan"))
            '            If General_Class.CekNULL(Dr("flag_dasar")) = "Y" Then
            '                Emi_Split_Stock_QC.Cmb_Satuan_Barang.Text = Dr("Satuan")
            '            End If
            '        Loop
            '    End Using

            '    CloseConn()
            'Catch ex As Exception
            '    CloseConn()
            '    MessageBox.Show(ex.Message)
            '    Exit Sub
            'End Try

            ''Emi_Split_Stock_QC.Cmb_Satuan_Barang.DroppedDown = True
            ''Emi_Split_Stock_QC.Cmb_Satuan_Barang.Focus()


            ''Emi_Split_Stock_QC.Btn_Insert_Click(Lv_Data, e)
            ''N_EMI_Transfer_Stock_Barang_Lain.DGV_Data_TF.Rows.Clear()




        End If



        Me.Close()


    End Sub



    Private Sub Txt_Limit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Limit.KeyPress


        If e.KeyChar = Chr(13) Then
            Btn_Simpan.PerformClick()
            Return
        End If

        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            Return
        End If

        Chk_Belum_Selesai.Checked = False

        Dim txt As TextBox = DirectCast(sender, TextBox)

        Dim futureText As String = txt.Text.Substring(0, txt.SelectionStart) & e.KeyChar & txt.Text.Substring(txt.SelectionStart + txt.SelectionLength)

        If futureText.Length > 1 AndAlso futureText.StartsWith("0") Then
            e.Handled = True
            Return
        End If

        Dim value As Integer
        If Integer.TryParse(futureText, value) Then
            If value < 0 OrElse value > 10000 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If



    End Sub


    Private Sub Txt_Limit_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Limit.KeyDown
        If Txt_Limit.Text.Trim() = "" Then Exit Sub

        Dim nilai As Integer
        If Not Integer.TryParse(Txt_Limit.Text, nilai) Then
            Txt_Limit.Text = "0"
        ElseIf nilai < 0 Then
            Txt_Limit.Text = "0"
        ElseIf nilai > 10000 Then
            Txt_Limit.Text = "10000"
        End If
    End Sub

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.SelectedIndex = 0 Then
            Txt_Value_Filter.Enabled = False
        Else
            Txt_Value_Filter.Enabled = True
            Chk_Belum_Selesai.Checked = False
        End If
        Txt_Value_Filter.Text = ""
    End Sub

    Private Sub Txt_Value_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value_Filter.KeyPress
        If e.KeyChar = Chr(13) Then
            Btn_cari.Focus()
        End If
    End Sub
    Private Sub Chk_Belum_Selesai_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Belum_Selesai.CheckedChanged
        If Chk_Belum_Selesai.Checked Then
            Cmb_Filter.SelectedIndex = 0
        End If

        Btn_cari.PerformClick()

    End Sub


    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub


End Class
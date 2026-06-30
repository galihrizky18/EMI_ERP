Public Class N_EMI_SD_List_Keep_Stock_Barang_Lain
    Dim Jenis = "Emi_Display_Request_Material"
    Public lokasi_kirim, asal As String
    Public lokasi_Terima As String
    Public _tabIndex As Integer = 0

    Dim Lv_Ket_No_PR, Lv_NO_PR, Lv_No_Keep, Lv_KdSO, Lv_Sisa, Lv_KdBarang, Lv_NmBarang, Lv_Keterangan, Lv_TglKeep, Lv_Jumlah, Lv_JumlahTf, Lv_Satuan, Lv_UrutKeep, Lv_Urut_Pr_Dept, lv_MetPotStock, Lv_JnsKemasan, Lv_Stock, LvKso As String

    Dim Lv_NO_PR_2, Lv_No_Keep_2, Lv_KdSO_2, Lv_Sisa_2, Lv_KdBarang_2, Lv_NmBarang_3, Lv_Keterangan_4, Lv_TglKeep_2, Lv_Jumlah_2, Lv_JumlahTf_2, Lv_Satuan_2, Lv_Urut_2, Lv_Urut_Pr_Dept_2, lv_MetPotStock_2, Lv_JnsKemasan_2, Lv_Stock_2, LV_Kso_2 As String

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
    Dim item_Sisa As Integer = 14
    Dim item_Urut_PR_Dept As Integer = 15
    Dim item_KSO As Integer = 16
    Dim item_NoPR_Dept As Integer = 17


    Dim item_NoPR_2 As Integer = 0
 
    Dim item_KDSO_2 As Integer = 1
    Dim item_KdBarang_2 As Integer = 2
    Dim item_NmBarang_2 As Integer = 3
    Dim item_Keterangan_2 As Integer =4



    Dim item_Tgl_Keep_2 As Integer = 5

  

    Dim item_Jumlah_2 As Integer = 6
    Dim item_JumlahTf_2 As Integer = 7
    Dim item_Satuan_2 As Integer = 8
    'Hide
    Dim item_urut_2 As Integer = 9
 
  
    Dim item_Stock_2 As Integer = 10
    Dim item_Sisa_2 As Integer = 11

    Dim item_kso_2 As Integer = 12



    Dim Flag_Opname As Boolean

    Dim DefaultLimit As Double = 20

    Dim arrFilter As New ArrayList
    Dim arrFilterPRDEPT As New ArrayList

    ' Variabel Global
    Dim PageSize As Integer = 15

    Private Sub cmbFilter_Pr_Dept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilter_Pr_Dept.SelectedIndexChanged
        If cmbFilter_Pr_Dept.SelectedIndex = 0 Then
            txt_value_pr_dept.Enabled = False
        Else
            txt_value_pr_dept.Enabled = True
            Chk_Belum_Selesai.Checked = False
        End If
        txt_value_pr_dept.Text = ""
    End Sub

    Private Sub btn_pr_dept_Click(sender As Object, e As EventArgs) Handles btn_pr_dept.Click
        get_pr_departement()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        ' Cek berdasarkan index (Page 2 = index 1)
        If TabControl1.SelectedIndex = 0 Then
            Kosong()
        ElseIf TabControl1.SelectedIndex = 1 Then
            get_pr_departement()
        End If
    End Sub

    Private Sub Lv_PRDEPT_DoubleClick(sender As Object, e As EventArgs) Handles Lv_PRDEPT.DoubleClick
        If Lv_PRDEPT.Items.Count = 0 Or Lv_PRDEPT.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If asal = "TF_Stock" Then


            For i As Integer = 0 To N_EMI_Transfer_Stock_Barang_Lain.Dgv_DataRekap.Rows.Count - 1


                If N_EMI_Transfer_Stock_Barang_Lain.Dgv_DataRekap.Rows(i).Cells(9).Value <> "PR DEPT" Then
                    MessageBox.Show("Jenis Transfer tidak boleh berbeda, saat ini sedang di transfer PR DEPT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If


            Next



            'N_EMI_Transfer_Stock_Barang_Lain.kosong()
            N_EMI_Transfer_Stock_Barang_Lain.CmbJnsTransfer.Enabled = False
            N_EMI_Transfer_Stock_Barang_Lain.CmbSO_Asal.Enabled = False
            N_EMI_Transfer_Stock_Barang_Lain.CmbSo_Tujuan.Enabled = False

            N_EMI_Transfer_Stock_Barang_Lain.TxtKd_Barang.Text = String.Empty
            N_EMI_Transfer_Stock_Barang_Lain.Txt_SO.Text = String.Empty
            N_EMI_Transfer_Stock_Barang_Lain.TxtNm_Barang.Text = String.Empty

            Get_Isi_ListView_prdept(Lv_PRDEPT.FocusedItem.Index)

            N_EMI_Transfer_Stock_Barang_Lain.asal = Jenis
            N_EMI_Transfer_Stock_Barang_Lain.Lv_DetBarang.Visible = False
            N_EMI_Transfer_Stock_Barang_Lain.TxtKd_Barang.Enabled = False
            'N_EMI_Transfer_Stock_Barang_Lain.Btn_GetData.Enabled = False
            N_EMI_Transfer_Stock_Barang_Lain.TxtKd_Barang.Text = Lv_KdBarang_2
            N_EMI_Transfer_Stock_Barang_Lain.TxtNm_Barang.Text = Lv_NmBarang_3
            N_EMI_Transfer_Stock_Barang_Lain.Txt_SO.Text = Lv_KdSO_2
            N_EMI_Transfer_Stock_Barang_Lain.TxtSatuanKecil.Text = Lv_Satuan_2
            N_EMI_Transfer_Stock_Barang_Lain.Txt_Warna.Text = "-"
            N_EMI_Transfer_Stock_Barang_Lain.TxtStock.Text = Format(Lv_Stock_2, "N4")
            N_EMI_Transfer_Stock_Barang_Lain.TxtSatuan.Text = Lv_Satuan_2
            N_EMI_Transfer_Stock_Barang_Lain.TxtBags.Text = Format(Val(HilangkanTanda(0)), "N4")

            N_EMI_Transfer_Stock_Barang_Lain.TxtJenisBags.Text = "-"

            'N_EMI_Transfer_Stock_Barang_Lain.Cmb_Warna.SelectedItem = lv_Warna
            N_EMI_Transfer_Stock_Barang_Lain.Cmb_Warna.SelectedIndex = 2


            N_EMI_Transfer_Stock_Barang_Lain.TxtStockDisplay.Text = Format(Val(HilangkanTanda(Lv_Stock_2)), "N4") + " " + Lv_Satuan_2

            Dim Jumlah_Permintaan_2 As Double = Lv_Jumlah_2 - Lv_JumlahTf_2

            N_EMI_Transfer_Stock_Barang_Lain.Txt_JumlahPermintaan.Text = Format(Jumlah_Permintaan_2, "N4")
            N_EMI_Transfer_Stock_Barang_Lain.Txt_SatuanPermintaan.Text = Lv_Satuan_2
            N_EMI_Transfer_Stock_Barang_Lain.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(Jumlah_Permintaan_2)), "N4") + " " + Lv_Satuan_2
            N_EMI_Transfer_Stock_Barang_Lain.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(Jumlah_Permintaan_2)), "N4"))
            N_EMI_Transfer_Stock_Barang_Lain.Txt_OtoMaterial_req.Text = Lv_Urut_2
            N_EMI_Transfer_Stock_Barang_Lain.Txt_Jenis_Transfer.Text = "PR DEPT"
            N_EMI_Transfer_Stock_Barang_Lain.TxtMetPotStok.Text = "-"
            N_EMI_Transfer_Stock_Barang_Lain.Txt_Urut_Request.Text = Lv_Urut_2
            N_EMI_Transfer_Stock_Barang_Lain.Urut_Pr_Dept.Text = Lv_Urut_2

            N_EMI_Transfer_Stock_Barang_Lain.txtDariPR.Text = Lv_NO_PR_2
            N_EMI_Transfer_Stock_Barang_Lain.TxtDariPRDepartement.Text = LV_Kso_2




            '    N_EMI_Transfer_Stock_Barang_Lain.isProduction = True
            N_EMI_Transfer_Stock_Barang_Lain.Btn_Insert_Click(Lv_Data, e)






        End If



        Me.Close()
    End Sub

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
        Lv_KdSO = Lv_Data.Items(NoIndex).SubItems(item_KDSO).Text
        Lv_KdBarang = Lv_Data.Items(NoIndex).SubItems(item_KdBarang).Text
        Lv_NmBarang = Lv_Data.Items(NoIndex).SubItems(item_NmBarang).Text
        Lv_Keterangan = Lv_Data.Items(NoIndex).SubItems(item_Keterangan).Text
        Lv_TglKeep = Lv_Data.Items(NoIndex).SubItems(item_Tgl_Keep).Text
        Lv_Jumlah = Lv_Data.Items(NoIndex).SubItems(item_Jumlah).Text
        Lv_JumlahTf = Lv_Data.Items(NoIndex).SubItems(item_JumlahTf).Text
        Lv_Satuan = Lv_Data.Items(NoIndex).SubItems(item_Satuan).Text
        Lv_UrutKeep = Lv_Data.Items(NoIndex).SubItems(item_UrutKeep).Text
        lv_MetPotStock = Lv_Data.Items(NoIndex).SubItems(item_MetPotStock).Text
        Lv_JnsKemasan = Lv_Data.Items(NoIndex).SubItems(item_jnsKemasan).Text
        Lv_Stock = Lv_Data.Items(NoIndex).SubItems(item_Stock).Text
        Lv_Sisa = Lv_Data.Items(NoIndex).SubItems(item_Sisa).Text
        Lv_Urut_Pr_Dept = Lv_Data.Items(NoIndex).SubItems(item_Urut_PR_Dept).Text
        LvKso = Lv_Data.Items(NoIndex).SubItems(item_KSO).Text

    End Sub

    Private Sub Get_Isi_ListView_prdept(ByVal NoIndex As Integer)



        Lv_NO_PR_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_NoPR_2).Text

        Lv_KdSO_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_KDSO_2).Text
        Lv_KdBarang_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_KdBarang_2).Text
        Lv_NmBarang_3 = Lv_PRDEPT.Items(NoIndex).SubItems(item_NmBarang_2).Text
        Lv_Keterangan_4 = Lv_PRDEPT.Items(NoIndex).SubItems(item_Keterangan_2).Text
        Lv_TglKeep_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_Tgl_Keep_2).Text
        Lv_Jumlah_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_Jumlah_2).Text
        Lv_JumlahTf_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_JumlahTf_2).Text
        Lv_Satuan_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_Satuan_2).Text
        Lv_Urut_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_urut_2).Text

        Lv_Stock_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_Stock_2).Text
        Lv_Sisa_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_Sisa_2).Text
        LV_Kso_2 = Lv_PRDEPT.Items(NoIndex).SubItems(item_kso_2).Text
    End Sub

    Private Sub Emi_Display_Request_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No PR", 110, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("No Keep Stock", 0, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Lokasi", 0, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '3
        Lv_Data.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left) '4
        Lv_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '5
        Lv_Data.Columns.Add("Tanggal Keep", 110, HorizontalAlignment.Center) '6
        Lv_Data.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '7
        Lv_Data.Columns.Add("Jumlah TF", 210, HorizontalAlignment.Right) '8
        Lv_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center) '9
        'HIDE
        Lv_Data.Columns.Add("Urut Keep", 0, HorizontalAlignment.Center) '10
        Lv_Data.Columns.Add("MetPotSotck", 0, HorizontalAlignment.Center) '11
        Lv_Data.Columns.Add("JnsKemasan", 0, HorizontalAlignment.Center) '12
        Lv_Data.Columns.Add("Stock", 0, HorizontalAlignment.Center) '13
        Lv_Data.Columns.Add("Sisa Bisa TF", 100, HorizontalAlignment.Right).DisplayIndex = 10 '14
        Lv_Data.Columns.Add("Urut_Dept", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Departement", 100, HorizontalAlignment.Left).DisplayIndex = 2


        Lv_PRDEPT.Columns.Clear()
        Lv_PRDEPT.Columns.Add("No PR", 110, HorizontalAlignment.Left) '0
        Lv_PRDEPT.Columns.Add("Lokasi", 120, HorizontalAlignment.Left) '1
        Lv_PRDEPT.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '2
        Lv_PRDEPT.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left) '3
        Lv_PRDEPT.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '4
        Lv_PRDEPT.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '6
        Lv_PRDEPT.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '5
        Lv_PRDEPT.Columns.Add("Jumlah TF", 120, HorizontalAlignment.Right) '6
        Lv_PRDEPT.Columns.Add("Satuan", 100, HorizontalAlignment.Center) '7
        'HIDE
        Lv_PRDEPT.Columns.Add("Urut", 0, HorizontalAlignment.Center) '8
        Lv_PRDEPT.Columns.Add("Stock", 0, HorizontalAlignment.Center) '9
        Lv_PRDEPT.Columns.Add("Sisa Bisa TF", 100, HorizontalAlignment.Right).DisplayIndex =7 '10
        Lv_PRDEPT.Columns.Add("SO", 0, HorizontalAlignment.Right) '11
        Lv_PRDEPT.Columns.Add("Jumlah TF Belum Validasi", 100, HorizontalAlignment.Right).DisplayIndex = 9 '12

        Lv_PRDEPT.View = View.Details



        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add(OpsiSeluruh) : arrFilter.Add(OpsiSeluruh)
        Cmb_Filter.Items.Add("No PR") : arrFilter.Add("c.No_Faktur")
        Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
        Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("e.Nama")
        Cmb_Filter.SelectedIndex = 0



        cmbFilter_Pr_Dept.Items.Clear() : arrFilterPRDEPT.Clear()
        cmbFilter_Pr_Dept.Items.Add(OpsiSeluruh) : arrFilterPRDEPT.Add(OpsiSeluruh)
        cmbFilter_Pr_Dept.Items.Add("No PR") : arrFilterPRDEPT.Add("a.No_Faktur")
        cmbFilter_Pr_Dept.Items.Add("Kode Barang") : arrFilterPRDEPT.Add("b.Kode_Barang")
        cmbFilter_Pr_Dept.Items.Add("Nama Barang") : arrFilterPRDEPT.Add("e.Nama")
        cmbFilter_Pr_Dept.SelectedIndex = 0




        Kosong()
    End Sub

    Private Sub get_pr_departement(Optional ByVal filter As Boolean = False, Optional ByVal page As Integer = 1)
        If filter = True Then
            If cmbFilter_Pr_Dept.SelectedIndex <> 1 Then
                PageSize = 22
            Else
                PageSize = 15
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
            Lv_PRDEPT.Items.Clear()

            Dim offset As Integer = (page - 1) * PageSize

            Dim Filtered2 As String = ""
            'If Not Chk_Belum_Selesai.Checked Then
            'If filter Then
            If cmbFilter_Pr_Dept.SelectedIndex <> 0 Then
                Filtered2 = "and " & arrFilterPRDEPT(cmbFilter_Pr_Dept.SelectedIndex) & " like '%" & txt_value_pr_dept.Text.Trim & "%'  "
            End If
            'End If
            'End If

            SQL = $"

                ;WITH cte_user AS (
                                    SELECT 
                                        a.Kode_Perusahaan,
                                        b.Kode_Stock_Owner_Gudang,
                                        a.User_ID,
                                        d.Id_Sub_Kategori_Jenis,
                                        d.Id_Kategori_Jenis
                                    FROM N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a
                                    INNER JOIN N_EMI_Master_Kategori_Gudang_Barang_Lain b
                                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                                       AND a.Id_Kategori_Gudang = b.Urut_Oto
                                    INNER JOIN N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain c
                                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                                       AND c.Id_Kategori_Gudang = b.Urut_Oto
                                    INNER JOIN N_EMI_Master_Sub_Kategori_Jenis d
                                        ON c.Kode_Perusahaan = d.Kode_Perusahaan
                                       AND c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis
                                    WHERE a.Status IS NULL
                                      AND b.Status IS NULL
                                      AND c.Status IS NULL
                                ),
                                cte_transfer_stock AS (
                                    SELECT
                                        z.Kode_Perusahaan,
                                        x.Kode_Barang,
                                        x.Urut_PR_Dept AS No_Urut,
                                        z.so_awal AS Kode_Stock_Owner,
                                        SUM(r.Jumlah) AS Jumlah_TF
                                    FROM N_EMI_Transfer_Stock_Barang_Lain z
                                    INNER JOIN N_EMI_Transfer_Stock_Barang_Lain_Detail x
                                        ON z.Kode_Perusahaan = x.Kode_Perusahaan
                                       AND z.No_Faktur = x.No_Faktur
                                    INNER JOIN N_EMI_Transfer_Stock_Barang_Lain_Det y
                                        ON x.Kode_Perusahaan = y.Kode_Perusahaan
                                       AND x.No_Faktur = y.No_Faktur
                                       AND x.Urut_Oto = y.Urut_TF
                                    INNER JOIN N_EMI_Transfer_Stock_Barang_Lain_Det2 r
                                        ON y.Kode_Perusahaan = r.Kode_Perusahaan
                                       AND y.No_Faktur = r.No_Faktur
                                       AND y.Urut_Oto = r.Urut_Det
                                    WHERE z.Status IS NULL
                                     and y.Selesai = 'Y'
                                    GROUP BY
                                        z.Kode_Perusahaan,
                                        x.Kode_Barang,
                                        x.Urut_PR_Dept,
                                        z.so_awal
                                ),

                                cte_transfer_stock_belum_Validasi AS (
                                    SELECT
                                        z.Kode_Perusahaan,
                                        x.Kode_Barang,
                                        x.Urut_PR_Dept AS No_Urut,
                                        z.so_awal AS Kode_Stock_Owner,
                                        SUM(y.Jumlah) AS Jumlah_TF_Belum_Validasi
                                    FROM N_EMI_Transfer_Stock_Barang_Lain z
                                    INNER JOIN N_EMI_Transfer_Stock_Barang_Lain_Detail x
                                        ON z.Kode_Perusahaan = x.Kode_Perusahaan
                                       AND z.No_Faktur = x.No_Faktur
                                    INNER JOIN N_EMI_Transfer_Stock_Barang_Lain_Det y
                                        ON x.Kode_Perusahaan = y.Kode_Perusahaan
                                       AND x.No_Faktur = y.No_Faktur
                                       AND x.Urut_Oto = y.Urut_TF
                                       
                                    WHERE z.Status IS NULL
                                    and y.selesai is null
                                    GROUP BY
                                        z.Kode_Perusahaan,
                                        x.Kode_Barang,
                                        x.Urut_PR_Dept,
                                        z.so_awal
                                ),
                            

                                cte_keep_stock AS (
                                    SELECT
                                        zr.Kode_Perusahaan,
                                        zr.Urut_Departement,                -- JOIN ke b.No_Urut
                                        SUM(zr.Jumlah) AS Total_Keep
                                    FROM N_EMI_Keep_Stock_Barang_Lain_Departement zr
                                    WHERE zr.Status IS NULL
                                      AND zr.Flag_Selesai_Pengeluaran_Barang IS NULL
                                    GROUP BY
                                        zr.Kode_Perusahaan,
                                        zr.Urut_Departement
                                ),
                                cte_gudang_tujuan as (
                                   select Kode_Perusahaan,Kode_Stock_Owner_Gudang,Kode_Kategori_Gudang
                                   From N_EMI_Master_Kategori_Gudang_Barang_Lain       
                                )

                                SELECT 
                                    a.No_Faktur AS No_PR,
                                
                                    ISNULL(a.Kode_Kategori_Gudang, '-') AS Kode_Stock_Owner,
                                    b.Kode_Barang,
                                    e.Nama AS Nama_Barang,
                                    b.Keterangan,
                                    a.Tanggal,
                                    b.kode_stock_owner as KSO,


                                    b.Jumlah - ISNULL(ks.Total_Keep, 0) AS Jumlah,

                                    b.Satuan,
                                    b.No_Urut,
                                     isnull((select e.Good_Stock - sum(xyz.total) from N_EMI_Transfer_Stock_Barang_Lain_Detail_Sementara xyz
                                    where xyz.kode_perusahaan = e.kode_perusahaan and xyz.kode_barang = e.Kode_Barang
                                    and xyz.kode_stock_owner = e.Kode_Stock_Owner
                                    ),  e.Good_Stock) as Good_Stock,
                                   
                                    e.Metode_Pengeluaran_Stok,
                                    e.Jenis_Kemasan,
                                    isnull(tf.Jumlah_TF,0) as Jumlah_TF,
                                    isnull(tff.Jumlah_TF_Belum_Validasi,0) as Jumlah_TF_Belum_Validasi,
                                    ISNULL(ks.Total_Keep, 0) AS Jumlah_Keep_Stock,
                                     b.Jumlah - ISNULL(ks.Total_Keep, 0) - isnull(tf.Jumlah_TF,0) as Sisa

                                FROM N_EMI_Purchase_Requisition_Barang_Lain_Departement a
                                INNER JOIN N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b
                                    ON a.Kode_Perusahaan = b.Kode_Perusahaan
                                   AND a.No_Faktur = b.No_Faktur
                                   AND b.Flag_Pengajuan_Selesai IS NULL

                                INNER JOIN Barang_Lain e
                                    ON a.Kode_Perusahaan = e.Kode_Perusahaan
                                   AND b.Kode_Barang = e.Kode_Barang
                                   

                                INNER JOIN View_Kategori_Turunan f
                                    ON e.Kode_Perusahaan = f.Kode_Perusahaan
                                   AND e.Id_Sub_Kategori_Jenis_3 = f.Id_Sub_Kategori_Jenis_3

                                INNER JOIN cte_user g
                                    ON f.Kode_Perusahaan = g.Kode_Perusahaan
                                   AND f.Id_Kategori_Jenis = g.Id_Kategori_Jenis
                                   AND f.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis

                                LEFT JOIN cte_keep_stock ks
                                    ON ks.Kode_Perusahaan = b.Kode_Perusahaan
                                   AND ks.Urut_Departement = b.No_Urut

                                LEFT JOIN cte_transfer_stock tf
                                    ON tf.Kode_Perusahaan = b.Kode_Perusahaan
                                   AND tf.No_Urut = b.No_Urut
                             
                                


                       

                            
                              LEFT JOIN cte_transfer_stock_belum_validasi tff
                                    ON tff.Kode_Perusahaan = b.Kode_Perusahaan
                             
                                   and tff.No_Urut = b.No_Urut
                                   


                                 inner join cte_gudang_tujuan gt on
                                 gt.Kode_Perusahaan = a.Kode_Perusahaan
                                 and gt.Kode_Kategori_Gudang = a.Kode_Kategori_Gudang


                                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
                                  AND a.Status IS NULL
                                  AND b.Flag_Selesai_Transfer IS NULL
                                 and b.flag_sudah_pr = 'Y'
                                  AND a.Flag_Release = 'Y'
                                  and g.Kode_Stock_Owner_Gudang = '{lokasi_kirim}'
                                     and e.kode_stock_owner = '{lokasi_kirim}'
                                  and gt.Kode_Stock_Owner_Gudang = '{lokasi_Terima}'
                                  and g.User_ID = '{UserID}'
                                  
                                  and (b.Jumlah 
                               - ISNULL(ks.Total_Keep, 0) 
                               - ISNULL(tf.Jumlah_TF, 0)) > 0 
                                                {Filtered2}

                                ORDER BY a.Tanggal DESC, a.Jam DESC;


                "


            Using Dr = OpenTrans(SQL)

                ' Gunakan list buffer agar AddRange() bisa dipakai
                Dim items As New List(Of ListViewItem)

                Do While Dr.Read
                    Dim lv As New ListViewItem(Dr("No_PR").ToString()) '0

                    lv.SubItems.Add(Dr("Kode_Stock_Owner").ToString()) '1
                    lv.SubItems.Add(Dr("Kode_Barang").ToString()) '2
                    lv.SubItems.Add(Dr("Nama_Barang").ToString()) '3
                    lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")).ToString()) '4
                    lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy")) '5
                    lv.SubItems.Add(Format(Dr("Jumlah"), "N4")) '6
                    lv.SubItems.Add(Format(Dr("Jumlah_TF"), "N4")) '7

                    lv.SubItems.Add(Dr("Satuan").ToString()) '8


                    Dim sisa As Double = Format(Dr("jumlah") - Dr("jumlah_tf"))
                    Dim good_stock_real As Double = Format(Dr("Good_Stock") - Dr("Jumlah_TF_Belum_Validasi"))

                    ' Kolom tambahan (hidden / internal)
                    lv.SubItems.Add(Dr("no_urut").ToString()) '9

                    lv.SubItems.Add(good_stock_real.ToString()) '10
                    lv.SubItems.Add(sisa) '11
                    lv.SubItems.Add(Dr("kode_stock_owner")) '11
                    lv.SubItems.Add(Format(Dr("Jumlah_TF_Belum_Validasi"), "N4")) '7





                    ' Tambahkan ke buffer
                    items.Add(lv)
                Loop

                ' --- tampilkan sekaligus di ListView ---
                Lv_PRDEPT.BeginUpdate()
                Lv_PRDEPT.Items.Clear()
                Lv_PRDEPT.Items.AddRange(items.ToArray())
                Lv_PRDEPT.EndUpdate()

            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If totalpage = CurrentPage Then
            btn_next_pr_dept.Enabled = False
        Else
            btn_next_pr_dept.Enabled = True
        End If

        If 1 = CurrentPage Then
            btn_prev_pr_dept.Enabled = False
        Else
            btn_prev_pr_dept.Enabled = True
        End If
    End Sub

    Public Sub Kosong(Optional ByVal filter As Boolean = False, Optional ByVal page As Integer = 1)

        If filter = True Then
            If Cmb_Filter.SelectedIndex <> 1 Then
                PageSize = 22
            Else
                PageSize = 15
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
	                 select a.Kode_Perusahaan,b.Kode_Stock_Owner_Gudang, a.user_id, d.id_sub_kategori_jenis, d.id_kategori_jenis  
	                from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a  
	                 inner join N_EMI_Master_Kategori_Gudang_Barang_Lain b on a.kode_perusahaan = b.kode_perusahaan and a.id_kategori_gudang = b.urut_oto  
	                 inner join N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain c on b.kode_perusahaan = c.kode_perusahaan and c.id_kategori_gudang = b.urut_oto  
	                 inner join N_EMI_Master_Sub_Kategori_Jenis d on c.kode_perusahaan = d.kode_perusahaan and c.id_sub_kategori_jenis = d.id_sub_kategori_jenis  
	                where a.status is null and b.status is null and c.status is null
                 ), 
                 cte_gudang_tujuan as (
                                   select Kode_Perusahaan,Kode_Stock_Owner_Gudang,Kode_Kategori_Gudang
                                   From N_EMI_Master_Kategori_Gudang_Barang_Lain       
                  )

                select c.No_Faktur as No_PR, c.kode_kategori_gudang,a.urut_departement, a.No_Faktur as No_KeepStock, a.Kode_Stock_Owner, a.Kode_Barang, e.Nama as Nama_Barang, c.Keterangan, a.Tanggal as Tanggal_Keep, a.Jumlah, a.Satuan, a.Urut_Oto,
                    e.Good_Stock, e.Metode_Pengeluaran_Stok, e.Jenis_Kemasan, 
                    isnull((
		                    select sum(r.Jumlah)
		                    from N_EMI_Transfer_Stock_Barang_Lain z
			                    inner join N_EMI_Transfer_Stock_Barang_Lain_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
			                    inner join N_EMI_Transfer_Stock_Barang_Lain_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.urut_tf
			                    inner join N_EMI_Transfer_Stock_Barang_Lain_Det2 r on y.kode_perusahaan = r.Kode_Perusahaan and y.No_Faktur = r.No_Faktur and y.Urut_Oto = r.Urut_Det
		                    where z.Kode_Perusahaan = a.Kode_Perusahaan
		                    and z.Status is null
		                    and y.Selesai = 'Y'
		                    and x.Urut_Keep_Stock = a.Urut_Oto
                            and x.Urut_PR_Dept = a.Urut_Departement 
	                    ), 0) as Jumlah_TF,
                         isnull((
                            select sum(y.Jumlah)
                        from N_EMI_Transfer_Stock_Barang_Lain z
                        inner join N_EMI_Transfer_Stock_Barang_Lain_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
                    inner join N_EMI_Transfer_Stock_Barang_Lain_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.urut_tf
                        and y.Selesai is null
                        where z.Kode_Perusahaan = a.Kode_Perusahaan
                        and z.Status is null
                        and x.Urut_PR_Dept = a.Urut_Departement 
                        and x.Urut_Keep_Stock = a.Urut_Oto
							
							
                    ), 0) as Jumlah_TF_Belum_Validasi,
                        isnull((
		                    select sum(z.total)
		                    from N_EMI_Transfer_Stock_Barang_Lain_Detail_Sementara z
			                where a.Kode_Perusahaan = z.kode_perusahaan 
							and a.Kode_Barang = z.kode_barang 
							and a.Kode_Stock_Owner = z.kode_stock_owner
	                    ), 0) as Jumlah_Simpan_Temp
                from N_EMI_Keep_Stock_Barang_Lain_Departement a
	                inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Departement = b.No_Urut
	                inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and c.Status is null
	                inner join Barang_Lain e on a.Kode_Perusahaan = e.Kode_Perusahaan  and a.Kode_Barang = e.Kode_Barang
                    inner join View_Kategori_Turunan f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.Id_Sub_Kategori_Jenis_3 = f.Id_Sub_Kategori_Jenis_3
	                inner join cte g on f.Kode_Perusahaan = g.Kode_Perusahaan and f.Id_Kategori_Jenis = g.Id_Kategori_Jenis and f.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis
                    inner join cte_gudang_tujuan gt on gt.kode_perusahaan = c.kode_perusahaan and gt.kode_kategori_gudang = c.kode_kategori_gudang
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Status is null
                and a.Flag_Selesai_Pengeluaran_Barang is null
                and g.Kode_Stock_Owner_Gudang = '{lokasi_kirim}'
                 and gt.Kode_Stock_Owner_Gudang = '{lokasi_Terima}'
                 and e.kode_stock_owner = '{lokasi_kirim}'
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


                    Dim sisa As Double = Format(Dr("jumlah") - Dr("jumlah_tf"))

                    Dim goodstock_real As Double = Format(Dr("Good_Stock") - Dr("Jumlah_Simpan_Temp") - Dr("Jumlah_TF_Belum_Validasi"))


                    ' Kolom tambahan (hidden / internal)
                    lv.SubItems.Add(Dr("Urut_Oto").ToString()) '10
                    lv.SubItems.Add(Dr("Metode_Pengeluaran_Stok").ToString()) '11
                    lv.SubItems.Add(Dr("Jenis_Kemasan").ToString()) '12
                    lv.SubItems.Add(goodstock_real.ToString()) '13
                    lv.SubItems.Add(sisa) '13
                    lv.SubItems.Add(Dr("urut_departement"))

                    If General_Class.CekNULL(Dr("kode_kategori_gudang")) = "" Then
                        lv.SubItems.Add("-")
                    Else
                        lv.SubItems.Add(Dr("kode_kategori_gudang"))
                    End If


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

            For i As Integer = 0 To N_EMI_Transfer_Stock_Barang_Lain.Dgv_DataRekap.Rows.Count - 1


                If N_EMI_Transfer_Stock_Barang_Lain.Dgv_DataRekap.Rows(i).Cells(9).Value <> "KEEP STOCK" Then
                    MessageBox.Show("Jenis Transfer tidak boleh berbeda, saat ini sedang di transfer KEEP STOCK", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If


            Next

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
            N_EMI_Transfer_Stock_Barang_Lain.Txt_SO.Text = LV_Kso_2
            N_EMI_Transfer_Stock_Barang_Lain.TxtSatuanKecil.Text = Lv_Satuan
            N_EMI_Transfer_Stock_Barang_Lain.Txt_Warna.Text = "-"
            N_EMI_Transfer_Stock_Barang_Lain.TxtStock.Text = Format(Lv_Stock, "N4")
            N_EMI_Transfer_Stock_Barang_Lain.TxtSatuan.Text = Lv_Satuan
            N_EMI_Transfer_Stock_Barang_Lain.TxtBags.Text = Format(Val(HilangkanTanda(0)), "N4")

            N_EMI_Transfer_Stock_Barang_Lain.TxtJenisBags.Text = Lv_JnsKemasan

            'N_EMI_Transfer_Stock_Barang_Lain.Cmb_Warna.SelectedItem = lv_Warna
            N_EMI_Transfer_Stock_Barang_Lain.Cmb_Warna.SelectedIndex = 2


            N_EMI_Transfer_Stock_Barang_Lain.TxtStockDisplay.Text = Format(Val(HilangkanTanda(Lv_Stock)), "N4") + " " + Lv_Satuan

            Dim Jumlah_Permintaan As Double = Lv_Jumlah - Lv_JumlahTf

            N_EMI_Transfer_Stock_Barang_Lain.Txt_JumlahPermintaan.Text = Format(Jumlah_Permintaan, "N4")
            N_EMI_Transfer_Stock_Barang_Lain.Txt_SatuanPermintaan.Text = Lv_Satuan
            N_EMI_Transfer_Stock_Barang_Lain.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4") + " " + Lv_Satuan
            N_EMI_Transfer_Stock_Barang_Lain.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4"))
            N_EMI_Transfer_Stock_Barang_Lain.Txt_OtoMaterial_req.Text = Lv_UrutKeep
            N_EMI_Transfer_Stock_Barang_Lain.Txt_Jenis_Transfer.Text = "KEEP STOCK"
            N_EMI_Transfer_Stock_Barang_Lain.TxtMetPotStok.Text = lv_MetPotStock
            N_EMI_Transfer_Stock_Barang_Lain.Txt_Urut_Request.Text = Lv_UrutKeep
            N_EMI_Transfer_Stock_Barang_Lain.Urut_Pr_Dept.Text = Lv_Urut_Pr_Dept

            N_EMI_Transfer_Stock_Barang_Lain.txtDariPR.Text = Lv_NO_PR
            N_EMI_Transfer_Stock_Barang_Lain.TxtDariPRDepartement.Text = LvKso



            N_EMI_Transfer_Stock_Barang_Lain.isProduction = True
            N_EMI_Transfer_Stock_Barang_Lain.Btn_Insert_Click(Lv_Data, e)

        ElseIf asal = "Split_Stock" Then




        End If



        Me.Close()


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

    Private Sub N_EMI_SD_List_Keep_Stock_Barang_Lain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        TabControl1.SelectedIndex = _tabIndex

    End Sub
End Class
Imports System.Reflection

Public Class Purchase_Requisition_Barang_Lain

    Dim arrCari, arrKd_biaya, arrKeterangan As New ArrayList

    Dim arrInisialFaktur, arrKategoriGudang, arrKdSOGudang As New ArrayList

    Dim Jenis = "Purchase_Requisition_Barang_Lain"

    Dim no_Faktur_Sementara As String = ""

    Dim publicFlagRelease = "T"

    Private Sub Master_Biaya_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub get_no_faktur()
        Txt_NoFaktur.Text = fPurchaseRequisitionBL & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Purchase_Requisition_Barang_Lain", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fPurchaseRequisitionBL) + 4 & ")", fPurchaseRequisitionBL & Format(tgl_skg, "MMyy"))
    End Sub

    Dim LvLokasi As String
    Dim LvKdBrg As String
    Dim LvNmBrg As String
    Dim LvQty As String
    Dim LvSatuan As String
    Dim LvTglDeli As String
    Dim LvKet As String
    Dim LvSisa As String
    Dim LvQtyByForecast As String
    Dim LvIdCostCenter, LvIdGedung, LvIsStock, LvNmCostCenter, LvNmGedung, LvNmStock, LvUrut_Departement,
        LvUrut, LvLink, LvEst, LvEst_Tiba, LvFlag_Est As String


    Dim CellLokasi As Integer = 0
    Dim CellKdBrg As Integer = 1
    Dim CellNmBrg As Integer = 2
    Dim CellQty As Integer = 3
    Dim CellSatuan As Integer = 4
    Dim CellTglDeli As Integer = 5
    Dim CellKet As Integer = 6
    Dim CellSisa As Integer = 7
    Dim CellQtyByForecast As Integer = 8
    Dim CellLink As Integer = 9
    Dim CellIDCostCenter As Integer = 10
    Dim CellIDGedung As Integer = 11
    Dim CellIsStock As Integer = 12
    Dim CellNmCostCenter As Integer = 13
    Dim CellNmGedung As Integer = 14
    Dim CellNmStock As Integer = 15
    Dim CellUrut_Departement As Integer = 16
    Dim CellUrut As Integer = 17
    Dim CellEst As Integer = 18
    Dim CellEstTiba As Integer = 19
    Dim cellFlagEst As Integer = 20

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvLokasi = Dgv_DataBarang.Rows(No_Index).Cells(CellLokasi).Value
        LvKdBrg = Dgv_DataBarang.Rows(No_Index).Cells(CellKdBrg).Value
        LvNmBrg = Dgv_DataBarang.Rows(No_Index).Cells(CellNmBrg).Value
        LvQty = Dgv_DataBarang.Rows(No_Index).Cells(CellQty).Value
        LvSatuan = Dgv_DataBarang.Rows(No_Index).Cells(CellSatuan).Value
        LvTglDeli = Dgv_DataBarang.Rows(No_Index).Cells(CellTglDeli).Value
        LvKet = Dgv_DataBarang.Rows(No_Index).Cells(CellKet).Value
        LvSisa = Dgv_DataBarang.Rows(No_Index).Cells(CellSisa).Value
        LvQtyByForecast = Dgv_DataBarang.Rows(No_Index).Cells(CellQtyByForecast).Value
        LvLink = Dgv_DataBarang.Rows(No_Index).Cells(CellLink).Value
        LvIdCostCenter = Dgv_DataBarang.Rows(No_Index).Cells(CellIDCostCenter).Value
        LvIdGedung = Dgv_DataBarang.Rows(No_Index).Cells(CellIDGedung).Value
        LvIsStock = Dgv_DataBarang.Rows(No_Index).Cells(CellIsStock).Value
        LvNmCostCenter = Dgv_DataBarang.Rows(No_Index).Cells(CellNmCostCenter).Value
        LvNmGedung = Dgv_DataBarang.Rows(No_Index).Cells(CellNmGedung).Value
        LvNmStock = Dgv_DataBarang.Rows(No_Index).Cells(CellNmStock).Value
        LvUrut_Departement = Dgv_DataBarang.Rows(No_Index).Cells(CellUrut_Departement).Value
        LvUrut = Dgv_DataBarang.Rows(No_Index).Cells(CellUrut).Value
        LvEst = Dgv_DataBarang.Rows(No_Index).Cells(CellEst).Value
        LvEst_Tiba = Dgv_DataBarang.Rows(No_Index).Cells(CellEstTiba).Value
        LvFlag_Est = Dgv_DataBarang.Rows(No_Index).Cells(cellFlagEst).Value
    End Sub

    Private Sub Master_Biaya_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Lbl_Judul.Text = "Purchase Requisition"

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh

            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            cmb_lokasi.Enabled = False

            no_Faktur_Sementara = String.Empty


            Cmb_Kategori_Gudang.Items.Clear() : arrKategoriGudang.Clear() : arrKdSOGudang.Clear()
            SQL = "select a.kode_kategori_gudang, b.Kode_Stock_Owner_Gudang "
            SQL = SQL & "from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a "
            SQL = SQL & "inner JOIN N_EMI_Master_Kategori_Gudang_Barang_Lain b on a.kode_perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Urut_Oto "
            SQL = SQL & "where user_ID='" & UserID & "' and a.kode_perusahaan = '" & KodePerusahaan & "' and b.Jenis_Gudang = 'Warehouse' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Kategori_Gudang.Items.Add(Dr("kode_kategori_gudang")) : arrKategoriGudang.Add(Dr("kode_kategori_gudang"))
                    arrKdSOGudang.Add(Dr("Kode_Stock_Owner_Gudang"))
                Loop
            End Using

            If Cmb_Kategori_Gudang.Items.Count <> 0 Then
                Cmb_Kategori_Gudang.SelectedIndex = 0
            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Dgv_DataBarang.Columns(CellEstTiba).DisplayIndex = 6

        kosong()

    End Sub

    Public Sub kosong()
        Dgv_DataBarang.Enabled = True
        publicFlagRelease = "T"
        get_jam()
        Try
            OpenConn()

            arrInisialFaktur.Clear() : cmb_lokasi.Items.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur_barang_lain from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmb_lokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur_barang_lain"))
                Loop
            End Using
            cmb_lokasi.Text = Lokasi

            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Txt_Faktur_MaterialReq.Visible = False
        Label2.Visible = False

        If Cmb_Kategori_Gudang.Items.Count <> 0 Then
            Cmb_Kategori_Gudang.SelectedIndex = 0
        End If
        Cmb_Kategori_Gudang.Enabled = True

        Lbl_IDBiaya.Text = ""
        Txt_Kd.Text = ""
        Txt_Keterangan.Text = ""
        Cmb_Kolom.SelectedIndex = -1
        Txt_Value.Text = ""
        TextBox2.Text = ""
        Txt_Faktur_MaterialReq.Text = ""
        TextBox2.ReadOnly = False

        '  Cmb_Kategori_Gudang.SelectedIndex = 0

        BtnPR_Simpan.Enabled = True
        BtnPR_Release.Visible = True
        BtnPR_Release.Enabled = True
        Btn_Unrelease.Visible = False
        Btn_Unrelease.Enabled = False

        Button2.Enabled = True

        Dgv_DataBarang.Rows.Clear()
        Dgv_DataBarang.Rows.Add(1)

        Cmb_Kolom.Items.Clear() : arrCari.Clear() : Cmb_Kolom.SelectedIndex = -1
        Cmb_Kolom.Items.Add(Base_Language.Lang_Global_Kode) : arrCari.Add("Kode_Biaya")
        Cmb_Kolom.Items.Add(Base_Language.lang_global_keterangan) : arrCari.Add("Keterangan")

        Txt_Kd.Enabled = True
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False
        BtnPR_Release.Visible = False

        Dim AksesSimpanPR As String = ""
        Dim AksesReleasePR As String = ""
        Try
            OpenConn()

            If CekButtonRole("Simpan_Purchase_Requisition_Barang_Lain") = "Y" Then
                AksesSimpanPR = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




        Try
            OpenConn()

            If CekButtonRole("Release_Purchase_Requisition_Barang_Lain") = "Y" Then
                AksesReleasePR = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If AksesSimpanPR = "Y" Then
            BtnPR_Simpan.Enabled = True
        Else
            BtnPR_Simpan.Enabled = False
        End If

        If AksesReleasePR = "Y" Then
            BtnPR_Release.Enabled = True
        Else
            BtnPR_Release.Enabled = False
        End If

        Try
            OpenConn()

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Kd.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Kode & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Kd.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Keterangan.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag = "&Simpan" Then
                SQL = "Insert Into EMI_Biaya(Kode_Perusahaan, Kode_Biaya, Keterangan) "
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_Kd.Text.Trim & "',"
                SQL = SQL & " '" & Txt_Keterangan.Text.Trim & "' )"
                ExecuteTrans(SQL)
            Else
                SQL = "Update EMI_Biaya Set Keterangan =  '" & Txt_Keterangan.Text.Trim & "'"
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Biaya = '" & Lbl_IDBiaya.Text & "' "
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
        Txt_Kd.Focus()
    End Sub


    Private Sub Txt_Kd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SD_Data_PR_Barang_Lain.ShowDialog()
    End Sub

    Private Sub BtnFormulator_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPR_Simpan.Click
        If Dgv_DataBarang.Rows.Count - 1 = 0 Then
            MessageBox.Show("Tidak ada Data yang bisa di simpan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Cmb_Kategori_Gudang.SelectedIndex = -1 Then
            MessageBox.Show("Kategori Gudang Harus Dipilih Terlebih Dahulu !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Kategori_Gudang.DroppedDown = True
            Exit Sub
        End If

        For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
            Get_Isi_Listview(i)
            If Val(Dgv_DataBarang.Rows(i).Cells(3).Value) = 0 Then
                MessageBox.Show("Qty Baris Ke " & i + 1 & " Belum Diisi !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Val(Dgv_DataBarang.Rows(i).Cells(3).Value) < 0 Then
                MessageBox.Show("Qty Baris Ke " & i + 1 & " tidak boleh kurang dari 0 !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Val(Dgv_DataBarang.Rows(i).Cells(18).Value) = 0 Then
                MessageBox.Show("Est.Harga baris ke " & i + 1 & "  tidak boleh 0 !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Val(Dgv_DataBarang.Rows(i).Cells(18).Value) = 0 Then
                MessageBox.Show("Est.Harga baris ke " & i + 1 & "  tidak boleh kurang dari 0 !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            'If Val(Dgv_DataBarang.Rows(i).Cells(18).Value) = 0 Then
            '    MessageBox.Show("Estimasi Harga Baris Ke " & i + 1 & " Belum Diisi !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
        Next

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Simpan_Purchase_Requisition_Barang_Lain") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If



            For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
                Get_Isi_Listview(i)





                If LvQty = 0 And LvUrut_Departement <> "" Then
                    SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, a.Flag_Ajukan, "
                    SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
                    SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak, "
                    SQL = SQL & "isnull((select sum(e.Jumlah) from Barang_Lain_SN e where a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Stock_Owner = e.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = e.Kode_Barang),0) as stock, "
                    SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null),0) as Jumlah_Keep_Stock, "
                    SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where a.Kode_Perusahaan = f.Kode_Perusahaan and "
                    SQL = SQL & "a.Kode_Barang = f.Kode_Barang and a.Kode_Stock_Owner = f.Kode_Stock_Owner and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null),0) as Jumlah_Keep_Stock_2 "
                    SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d "
                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and d.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
                    SQL = SQL & "and a.Flag_Sudah_PR is null and a.Kode_Barang <> '-' and a.No_Urut = '" & LvUrut_Departement & "' "
                    SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If dr("Jumlah") - dr("Jmlh_PR") - dr("Jumlah_Keep_Stock") <> 0 Then
                                dr.Close()
                                MessageBox.Show("jumlah yang di input salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                CloseTrans()
                                CloseConn()
                                Exit Sub
                            End If
                        End If
                    End Using
                End If
            Next

            If Btn_Simpan.Tag = "&Simpan" Then
                get_no_faktur()



                Dim Kategori_gudang As String = arrKategoriGudang(Cmb_Kategori_Gudang.SelectedIndex)
                ''===================== Ambil gudang kode kategori gudang by user =====================
                'SQL = "select top(1) kode_kategori_gudang from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a "
                'SQL = SQL & "where user_ID='" & UserID & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        Kategori_gudang = Dr("kode_kategori_gudang")
                '    Else
                '        Dr.Close()
                '        CloseConn()
                '        MessageBox.Show("Gagal, Kategori Gudang belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using


                SQL = "insert into EMI_Purchase_Requisition_Barang_Lain(Kode_Perusahaan,No_Faktur,Lokasi,Tanggal,Jam,UserId,Keterangan, kode_kategori_gudang) values("
                SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "', '" & cmb_lokasi.Text & "', "
                SQL = SQL & "'" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "',"
                SQL = SQL & "'" & UserID & "', '" & TextBox2.Text.Trim & "', '" & Kategori_gudang & "' )"
                ExecuteTrans(SQL)

                SQL = "insert into EMI_Purchase_Requisition_Barang_Lain_log(Kode_Perusahaan, No_Faktur, Lokasi, Status, Tanggal, Jam, UserId, Keterangan) "
                SQL = SQL & "select Kode_Perusahaan,No_Faktur,Lokasi,Status,Tanggal,Jam,UserId,Keterangan from EMI_Purchase_Requisition_Barang_Lain "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

                For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
                    Get_Isi_Listview(i)

                    Dim flagest As String = ""

                    If LvFlag_Est = "T" Then
                        flagest = "NULL"
                    Else
                        flagest = "'Y'"
                    End If

                    Dim SelelctedIDCostCenter As String = ""
                    If String.IsNullOrEmpty(LvIdCostCenter) Then
                        SelelctedIDCostCenter = Ket_Cost_Center_HO_Proyek
                    Else
                        SelelctedIDCostCenter = "'" & LvIdCostCenter & "'"
                    End If

                    Dim SelelctedIDGedung As String = ""
                    If String.IsNullOrEmpty(LvIdGedung) Then
                        SelelctedIDGedung = "NULL"
                    Else
                        SelelctedIDGedung = "'" & LvIdGedung & "'"
                    End If


                    SQL = "insert into EMI_Purchase_Requisition_Barang_Lain_Detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Tanggal_Delivery,keterangan, "
                    SQL = SQL & " Id_Cost_Center, ID_Gedung, Flag_Stock "

                    If LvUrut_Departement <> "" Then
                        SQL = SQL & ", Urut_Departement "
                    End If

                    SQL = SQL & ",Link, Estimasi_Harga, Estimasi, flag_harga_pembelian_akhir) values( '" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "' ,"
                    SQL = SQL & "'" & LvLokasi & "', '" & LvKdBrg & "' ,"
                    SQL = SQL & "'" & HilangkanTanda(LvQty) & "',"
                    SQL = SQL & "'" & LvSatuan & "', '" & Format(CDate(LvTglDeli), "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & LvKet & "', " & SelelctedIDCostCenter & ", " & SelelctedIDGedung & ", '" & LvIsStock & "' "
                    If LvUrut_Departement <> "" Then
                        SQL = SQL & ", '" & LvUrut_Departement & "' "
                    End If
                    SQL = SQL & ", '" & LvLink & "', '" & HilangkanTanda(LvEst) & "', '" & LvEst_Tiba & "', " & flagest & ") "
                    ExecuteTrans(SQL)


                    'stenly
                    If LvUrut_Departement <> "" Then
                        SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set Jmlh_PR = Jmlh_PR + '" & HilangkanTanda(LvQty) & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & LvUrut_Departement & "' "
                        ExecuteTrans(SQL)

                        Dim xNo_Faktur As String = ""
                        SQL = "select a.Jumlah, a.Jmlh_PR, a.No_Faktur, "
                        SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where "
                        SQL = SQL & "a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
                        SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null "
                        SQL = SQL & "and f.Status is null),0) as Jumlah_Keep_Stock "
                        SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a "
                        SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Urut = '" & LvUrut_Departement & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                xNo_Faktur = dr("No_Faktur")

                                If dr("Jumlah") < dr("Jmlh_PR") + dr("Jumlah_Keep_Stock") Then
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("jumlah purchase requisition lebih besar dari jumlah purchase requisition departement ! !")
                                    Exit Sub
                                End If

                                If dr("Jumlah") = dr("Jmlh_PR") + dr("Jumlah_Keep_Stock") Then

                                    dr.Close()
                                    SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set Flag_Sudah_PR = 'Y' "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & LvUrut_Departement & "' "
                                    ExecuteTrans(SQL)
                                End If
                            End If
                        End Using

                        'SQL = "select sum(a.Jumlah) as Jumlah, sum(a.Jmlh_PR) as Jmlh_PR, "
                        'SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where "
                        'SQL = SQL & "a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
                        'SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null "
                        'SQL = SQL & "and f.Status is null),0) as Jumlah_Keep_Stock "
                        'SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a "
                        'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & xNo_Faktur & "' "
                        'SQL = "select sum(a.Jumlah) as Jumlah, sum(a.Jmlh_PR) as Jmlh_PR, sum(isnull(f.Jumlah,0)) as Jumlah_Keep_Stock "

                        'awal stenly 15-12
                        SQL = "with cte as(select a.Jumlah as Jumlah, a.Jmlh_PR as Jmlh_PR, sum(isnull(f.Jumlah,0)) as Jumlah_Keep_Stock "
                        SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a left join N_EMI_Keep_Stock_Barang_Lain_Departement f "
                        SQL = SQL & "on a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
                        SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement "
                        SQL = SQL & " and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null "
                        SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & xNo_Faktur & "' "
                        '   SQL = SQL & "and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null "
                        SQL = SQL & "group by a.Jumlah , a.Jmlh_PR )"
                        SQL = SQL & "select sum(jumlah) as Jumlah, sum(Jmlh_PR) as Jmlh_PR, sum(Jumlah_Keep_Stock) as Jumlah_Keep_Stock from cte"
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                If dr("Jumlah") = dr("Jmlh_PR") + dr("Jumlah_Keep_Stock") Then
                                    dr.Close()

                                    SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set Flag_PR = 'Y' "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & xNo_Faktur & "' "
                                    ExecuteTrans(SQL)
                                End If
                            End If
                        End Using
                        'akhir stenly 15-12
                    End If
                Next

                SQL = "insert into EMI_Purchase_Requisition_Barang_Lain_Detail_log(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, "
                SQL = SQL & "Kode_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan, Urut_Departement, Link, Estimasi_Harga, "
                SQL = SQL & "UserID_Ubah, Tanggal_Ubah, Jam_Ubah, flag_harga_pembelian_akhir, ETA) "
                SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah,Satuan, Tanggal_Delivery, "
                SQL = SQL & "keterangan, Urut_Departement, Link, Estimasi_Harga, "
                SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "',flag_harga_pembelian_akhir, ETA "
                SQL = SQL & "from EMI_Purchase_Requisition_Barang_Lain_Detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
                ExecuteTrans(SQL)
            Else

                SQL = "select status,flag_po,flag_release, flag_pra_release from EMI_Purchase_Requisition_Barang_Lain "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_faktur = '" & Txt_NoFaktur.Text & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If General_Class.CekNULL(dr("status")) = "Y" Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf General_Class.CekNULL(dr("flag_po")) = "Y" Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi tidak bisa dilanjutkan, karena PR sudah selesai di PO", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf General_Class.CekNULL(dr("flag_pra_release")) = "Y" Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("PR sudah pernah direlease!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Produksi Order tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'SQL = "update EMI_Purchase_Requisition_Barang_Lain set keterangan = '" & TextBox2.Text.Trim & "', "
                'SQL = SQL & "tanggal = '" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', "
                'SQL = SQL & "jam = '" & Format(tgl_skg, "HH:MM:ss") & "',"
                'SQL = SQL & "userid = '" & UserID & "' where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text.Trim & "'"
                'ExecuteTrans(SQL)


                SQL = "select Kode_Perusahaan, status from EMI_Purchase_Requisition_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text.Trim & "' "
                SQL = SQL & "and userid = '" & UserID & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        If General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena no faktur sudah di batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        Dr.Close()

                        SQL = "update EMI_Purchase_Requisition_Barang_Lain set keterangan = '" & TextBox2.Text.Trim & "' "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text.Trim & "' "
                        SQL = SQL & "and userid = '" & UserID & "' "
                        ExecuteTrans(SQL)

                    End If
                End Using

                SQL = "insert into EMI_Purchase_Requisition_Barang_Lain_log(Kode_Perusahaan, No_Faktur, Lokasi, Status, Tanggal, Jam, UserId, Keterangan) "
                SQL = SQL & "select Kode_Perusahaan,No_Faktur,Lokasi,Status,Tanggal,Jam,UserId,Keterangan from EMI_Purchase_Requisition_Barang_Lain "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

                SQL = "delete EMI_Purchase_Requisition_Barang_Lain_Detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
                ExecuteTrans(SQL)

                For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
                    Get_Isi_Listview(i)

                    Dim flagest As String = ""

                    If LvFlag_Est = "T" Then
                        flagest = "NULL"
                    Else
                        flagest = "'Y'"
                    End If


                    If LvUrut_Departement <> "" Then
                        'Dim xJumlah As Integer = 0
                        'SQL = "select sum(Jumlah) as Jumlah from EMI_Purchase_Requisition_Barang_Lain_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' "
                        'SQL = SQL & "and Urut_Departement = '" & LvUrut_Departement & "' "
                        'Using dr = OpenTrans(SQL)
                        '    If dr.Read Then
                        '        xJumlah = dr("Jumlah")
                        '    End If
                        'End Using

                        'SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set Jmlh_PR = '" & xJumlah & "' "
                        'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & LvUrut_Departement & "' "
                        'ExecuteTrans(SQL)

                        SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set Flag_Sudah_PR = NULL "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & LvUrut_Departement & "' "
                        ExecuteTrans(SQL)

                        Dim xNo_Faktur As String = ""
                        SQL = "select Jumlah, Jmlh_PR, No_Faktur from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & LvUrut_Departement & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                xNo_Faktur = dr("No_Faktur")
                            End If
                        End Using

                        SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set Flag_PR = NULL "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & xNo_Faktur & "' "
                        ExecuteTrans(SQL)
                    End If


                    Dim SelelctedIDCostCenter As String = ""
                    If String.IsNullOrEmpty(LvIdCostCenter) Then
                        SelelctedIDCostCenter = Ket_Cost_Center_HO_Proyek
                    Else
                        SelelctedIDCostCenter = "'" & LvIdCostCenter & "'"
                    End If

                    Dim SelelctedIDGedung As String = ""
                    If String.IsNullOrEmpty(LvIdGedung) Then
                        SelelctedIDGedung = "NULL"
                    Else
                        SelelctedIDGedung = "'" & LvIdGedung & "'"
                    End If


                    SQL = "insert into EMI_Purchase_Requisition_Barang_Lain_Detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Tanggal_Delivery,keterangan, "
                    SQL = SQL & " Id_Cost_Center, ID_Gedung, Flag_Stock "

                    If LvUrut_Departement <> "" Then
                        SQL = SQL & ", Urut_Departement "
                    End If

                    SQL = SQL & ", Link, Estimasi_Harga, Estimasi, flag_harga_pembelian_akhir) values( '" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "' ,"
                    SQL = SQL & "'" & LvLokasi & "', '" & LvKdBrg & "' ,"
                    SQL = SQL & "'" & HilangkanTanda(LvQty) & "',"
                    SQL = SQL & "'" & LvSatuan & "', '" & Format(CDate(LvTglDeli), "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & LvKet & "', " & SelelctedIDCostCenter & ", " & SelelctedIDGedung & ", '" & LvIsStock & "' "
                    If LvUrut_Departement <> "" Then
                        SQL = SQL & ", '" & LvUrut_Departement & "' "
                    End If
                    SQL = SQL & ", '" & LvLink & "', '" & HilangkanTanda(LvEst) & "', '" & LvEst_Tiba & "', " & flagest & ")"
                    ExecuteTrans(SQL)

                    'stenly
                    If LvUrut_Departement <> "" Then
                        Dim xJumlah As Integer = 0
                        SQL = "select sum(b.Jumlah) as Jumlah "
                        SQL = SQL & "from EMI_Purchase_Requisition_Barang_Lain a, EMI_Purchase_Requisition_Barang_Lain_detail b "
                        SQL = SQL & "where a.kode_perusahaan=b.kode_perusahaan and a.no_faktur=b.no_faktur and a.status is null and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.urut_departement = '" & LvUrut_Departement & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                xJumlah = dr("Jumlah")
                            End If
                        End Using

                        SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set Jmlh_PR = '" & xJumlah & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & LvUrut_Departement & "' "
                        ExecuteTrans(SQL)

                        Dim xNo_Faktur As String = ""
                        SQL = "select a.Jumlah, a.Jmlh_PR, a.No_Faktur, "
                        SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where "
                        SQL = SQL & "a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
                        SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null "
                        SQL = SQL & "and f.Status is null),0) as Jumlah_Keep_Stock "
                        SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a "
                        SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Urut = '" & LvUrut_Departement & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                xNo_Faktur = dr("No_Faktur")

                                If dr("Jumlah") < dr("Jmlh_PR") + dr("Jumlah_Keep_Stock") Then
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("jumlah purchase requisition lebih besar dari jumlah purchase requisition departement ! !")
                                    Exit Sub
                                End If

                                If dr("Jumlah") = dr("Jmlh_PR") + dr("Jumlah_Keep_Stock") Then
                                    dr.Close()
                                    SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set Flag_Sudah_PR = 'Y' "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & LvUrut_Departement & "' "
                                    ExecuteTrans(SQL)
                                End If
                            End If
                        End Using

                        'SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail_Log(Kode_Perusahaan, No_Faktur, "
                        'SQL = SQL & "Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan, Link, "
                        'SQL = SQL & "No_Urut, Id_Sub_Kategori_Jenis, Estimasi_Harga, "
                        'SQL = SQL & "UserID_Ubah, Tanggal_Ubah, Jam_Ubah) "
                        'SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah,Satuan, "
                        'SQL = SQL & "Tanggal_Delivery, keterangan, Link, No_Urut, Id_Sub_Kategori_Jenis, Estimasi_Harga, "
                        'SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:MM:ss") & "' "
                        'SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail where "
                        'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & LvUrut_Departement & "' "
                        'ExecuteTrans(SQL)

                        'SQL = "select sum(a.Jumlah) as Jumlah, sum(a.Jmlh_PR) as Jmlh_PR "
                        'SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where "
                        'SQL = SQL & "a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
                        'SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null "
                        'SQL = SQL & "and f.Status is null),0) as Jumlah_Keep_Stock "
                        'SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a "
                        'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & xNo_Faktur & "' "

                        'awal stenly 15-12
                        SQL = "with cte as(select a.Jumlah as Jumlah, a.Jmlh_PR as Jmlh_PR, sum(isnull(f.Jumlah,0)) as Jumlah_Keep_Stock "
                        SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a left join N_EMI_Keep_Stock_Barang_Lain_Departement f "
                        SQL = SQL & "on a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
                        SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement "
                        SQL = SQL & "and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null "
                        SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & xNo_Faktur & "' "
                        ' SQL = SQL & "and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null "
                        SQL = SQL & "group by a.Jumlah , a.Jmlh_PR )"
                        SQL = SQL & "select sum(Jumlah) as Jumlah, sum(Jmlh_PR) as Jmlh_PR, sum(Jumlah_Keep_Stock) as Jumlah_Keep_Stock from cte"
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                If dr("Jumlah") = dr("Jmlh_PR") + dr("Jumlah_Keep_Stock") Then
                                    dr.Close()

                                    SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set Flag_PR = 'Y' "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & xNo_Faktur & "' "
                                    ExecuteTrans(SQL)
                                End If
                            End If
                        End Using
                        'akhir stenly 15-12
                    End If
                Next
                SQL = "insert into EMI_Purchase_Requisition_Barang_Lain_Detail_log(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, "
                SQL = SQL & "Kode_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan, Urut_Departement, Link, Estimasi_Harga, "
                SQL = SQL & "UserID_Ubah, Tanggal_Ubah, Jam_Ubah,flag_harga_pembelian_akhir, ETA) "
                SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah,Satuan, Tanggal_Delivery, "
                SQL = SQL & "keterangan, Urut_Departement, Link, Estimasi_Harga, "
                SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', flag_harga_pembelian_akhir, ETA "
                SQL = SQL & "from EMI_Purchase_Requisition_Barang_Lain_Detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
                ExecuteTrans(SQL)
            End If

            SQL = "select no_faktur From  EMI_Purchase_Requisition_Barang_Lain_Detail where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_faktur = '" & Txt_NoFaktur.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Gagal menyimpan, barang harus di isi terlebih dahulu. refresh dan coba lagi")
                    Exit Sub
                End If
            End Using


            Cmd.Transaction.Commit()

            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub BtnFormulator_Refresh_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Refresh.Click
        kosong()
    End Sub

    Private Sub Txt_NoFaktur_Leave(sender As Object, e As EventArgs) Handles Txt_NoFaktur.Leave
        get_jam()

        Dim AksesSimpanPR As String = ""
        Dim AksesReleasePR As String = ""
        Try
            OpenConn()

            If CekButtonRole("Simpan_Purchase_Requisition_Barang_Lain") = "Y" Then
                AksesSimpanPR = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Release_Purchase_Requisition_Barang_Lain") = "Y" Then
                AksesReleasePR = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If AksesSimpanPR = "Y" Then
            BtnPR_Simpan.Enabled = True
        Else
            BtnPR_Simpan.Enabled = False
        End If

        If AksesReleasePR = "Y" Then
            BtnPR_Release.Enabled = True
        Else
            BtnPR_Release.Enabled = False
        End If

        Try
            OpenConn()
            publicFlagRelease = "T"
            TextBox2.Text = ""
            BtnPR_Release.Visible = True


            Dim ada_data As String = ""

            Dim Kategori_gudang As String = ""
            ada_data = "Y"
            '===================== Ambil gudang kode kategori gudang by user =====================
            SQL = "select kode_kategori_gudang from EMI_Purchase_Requisition_Barang_Lain "
            SQL = SQL & "where no_faktur  = '" & Txt_NoFaktur.Text.Trim & "'  and kode_perusahaan = '" & KodePerusahaan & "'  "
            '     SQL = SQL & "and kode_kategori_gudang = '" & Cmb_Kategori_Gudang.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("kode_kategori_gudang")) = "" Then
                        Dr.Close()
                        CloseConn()
                        ada_data = "T"
                        MessageBox.Show("Terjadi kesalahan pada kategori gudang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        kosong()
                        Exit Sub
                    Else

                        If Dr("kode_kategori_gudang") <> Cmb_Kategori_Gudang.Text.Trim Then
                            Dr.Close()
                            CloseConn()
                            ada_data = "T"
                            MessageBox.Show("anda Tidak ada Akses ke PR Ini !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            kosong()
                            Exit Sub
                        End If

                        Cmb_Kategori_Gudang.Text = Dr("kode_kategori_gudang")
                        Cmb_Kategori_Gudang.Enabled = False
                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    ada_data = "T"

                    kosong()
                    Exit Sub
                End If
            End Using




            '=====================================
            '=     CEK AKSES KATEGORI GUDANG     =
            '=====================================
            SQL = "select a.Kode_Perusahaan, a.Status "
            SQL = SQL & "FROM EMI_Purchase_Requisition_Barang_Lain a "
            SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status IS NULL "
            SQL = SQL & "and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
            SQL = SQL & "AND a.Kode_Kategori_Gudang in ( "
            SQL = SQL & "select z.kode_kategori_gudang "
            SQL = SQL & "from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain z "
            SQL = SQL & "inner JOIN N_EMI_Master_Kategori_Gudang_Barang_Lain x on z.kode_perusahaan = x.Kode_Perusahaan and z.Id_Kategori_Gudang = x.Urut_Oto "
            SQL = SQL & "where z.user_ID = '" & UserID & "' and z.kode_perusahaan = a.Kode_Perusahaan) "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("PR Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data tidak ditemukan atau Anda Tidak Memiliki Akses Kategori Gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            Dim flag_release_fix As String = ""
            SQL = "select no_faktur, lokasi ,tanggal, jam, userId, keterangan, flag_pra_release,status, No_Fak_Material_Requisition,kode_kategori_gudang from EMI_Purchase_Requisition_Barang_Lain where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
            SQL = SQL & "and status is null"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            'ada_data = "Y"

                            'Dim Kategori_gudang As String = ""
                            ''===================== Ambil gudang kode kategori gudang by user =====================
                            'SQL = "select top(1) kode_kategori_gudang from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a "
                            'SQL = SQL & "where user_ID='" & UserID & "' and a.kode_perusahaan = '" & KodePerusahaan & "' and "
                            'SQL = SQL & "a.kode_kategori_gudang='" & .Rows(i).Item("kode_kategori_gudang") & "' "
                            'Using Dr = OpenTrans(SQL)
                            '    If Dr.Read Then
                            '        Kategori_gudang = Dr("kode_kategori_gudang")
                            '    Else
                            '        Dr.Close()
                            '        CloseConn()
                            '        ada_data = "T"
                            '        MessageBox.Show("anda Tidak ada Akses ke PR Ini !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '        kosong()
                            '        Exit Sub
                            '    End If
                            'End Using

                            'If General_Class.CekNULL(.Rows(i).Item("kode_kategori_gudang")) <> Kategori_gudang Then
                            '    CloseConn()
                            '    ada_data = "T"
                            '    MessageBox.Show("anda Tidak ada Akses ke PR Ini !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    kosong()
                            '    Exit Sub
                            'End If

                            Cmb_Kategori_Gudang.Text = Kategori_gudang
                            Cmb_Kategori_Gudang.Enabled = False

                            Txt_NoFaktur.Text = .Rows(i).Item("no_faktur")
                            cmb_lokasi.Text = .Rows(i).Item("lokasi")
                            TextBox2.Text = .Rows(i).Item("keterangan")
                            DtpFormulator_Tanggal.Value = .Rows(i).Item("tanggal")

                            If General_Class.CekNULL(.Rows(i).Item("No_Fak_Material_Requisition")) = "" Then
                                Txt_Faktur_MaterialReq.Text = ""
                                Txt_Faktur_MaterialReq.Visible = False
                                Label2.Visible = False
                            Else
                                Txt_Faktur_MaterialReq.Visible = True
                                Label2.Visible = True
                                Txt_Faktur_MaterialReq.Text = .Rows(i).Item("No_Fak_Material_Requisition")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then
                                ada_data = "T"
                                MessageBox.Show("PR sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                kosong()

                                Exit Sub
                            End If

                            'If General_Class.CekNULL(.Rows(i).Item("flag_release")) = "Y" Then
                            '    publicFlagRelease = "Y"
                            '    BtnPR_Simpan.Enabled = False
                            '    BtnPR_Release.Visible = False
                            '    BtnPR_Release.Enabled = False
                            '    Button2.Enabled = False
                            '    TextBox2.ReadOnly = True
                            'Else
                            '    publicFlagRelease = "T"

                            '    If AksesSimpanPR = "Y" Then
                            '        BtnPR_Simpan.Enabled = True
                            '    Else
                            '        BtnPR_Simpan.Enabled = False
                            '    End If

                            '    If AksesReleasePR = "Y" Then
                            '        BtnPR_Release.Enabled = True
                            '    Else
                            '        BtnPR_Release.Enabled = False
                            '    End If

                            '    BtnPR_Release.Visible = True

                            '    Button2.Enabled = True
                            '    TextBox2.ReadOnly = False
                            'End If

                            'awal stenly 15-12
                            If General_Class.CekNULL(.Rows(i).Item("flag_pra_release")) = "Y" Or General_Class.CekNULL(.Rows(i).Item("flag_pra_release")) = "P" Then
                                publicFlagRelease = "Y"
                                BtnPR_Simpan.Enabled = False
                                BtnPR_Release.Visible = False
                                BtnPR_Release.Enabled = False

                                Btn_Unrelease.Visible = True
                                Btn_Unrelease.Enabled = True

                                Button2.Enabled = False
                                TextBox2.ReadOnly = True
                            Else
                                publicFlagRelease = "T"

                                If AksesSimpanPR = "Y" Then
                                    BtnPR_Simpan.Enabled = True
                                Else
                                    BtnPR_Simpan.Enabled = False
                                End If

                                If AksesReleasePR = "Y" Then
                                    BtnPR_Release.Enabled = True
                                Else
                                    BtnPR_Release.Enabled = False
                                End If

                                Btn_Unrelease.Visible = False
                                Btn_Unrelease.Enabled = False

                                BtnPR_Release.Visible = True

                                Button2.Enabled = True
                                TextBox2.ReadOnly = False
                            End If
                            'akhir stenly 15-12

                            'If General_Class.CekNULL(.Rows(i).Item("flag_release")) = "Y" Then
                            '    BtnFormulator_Simpan.Enabled = False
                            '    Button3.Visible = False
                            '    Button3.Enabled = False
                            '    Button2.Enabled = False

                            '    'flag_release_fix = "Y"
                            '    Dgv_DataBarang.Rows(i).Cells(0).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(1).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(2).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(3).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(4).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(5).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(6).ReadOnly = True
                            'Else
                            '    BtnFormulator_Simpan.Enabled = True
                            '    Button3.Visible = True
                            '    Button3.Enabled = True
                            '    Button2.Enabled = True

                            '    Dgv_DataBarang.Rows(i).Cells(0).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(1).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(2).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(3).ReadOnly = False
                            '    Dgv_DataBarang.Rows(i).Cells(4).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(5).ReadOnly = True
                            '    Dgv_DataBarang.Rows(i).Cells(6).ReadOnly = False
                            'End If
                        Next
                    Else

                        CloseConn()
                        kosong()
                        Exit Sub
                    End If
                End With
            End Using

            Dgv_DataBarang.Rows.Clear()
            If ada_data = "Y" Then

#Region "Kode Lama"

                'SQL = "select a.kode_perusahaan, a.kode_stock_owner, a.kode_barang, b.nama, a.jumlah, a.satuan, a.tanggal_delivery, a.keterangan, a.Link, a.Estimasi_Harga, "

                'SQL = SQL & "ISNULL((select sum(x.Nilai_PPIC) from EMI_Transaksi_Material_Requsition_detail x "
                'SQL = SQL & "where x.Kode_Perusahaan = a.Kode_Perusahaan and x.Kode_Stock_Owner = a.Kode_Stock_Owner and x.Kode_Barang = a.Kode_Barang "
                'SQL = SQL & "and x.No_Faktur = '" & Txt_Faktur_MaterialReq.Text & "' "
                'SQL = SQL & "), 0) as Nilai_PPIC, "

                ''SQL = SQL & "ISNULL(( select (SUM(z.jumlah) - a.jumlah) from EMI_Purchase_Requisition_Barang_Lain_Detail z where z.Kode_Perusahaan = a.Kode_Perusahaan "
                ''SQL = SQL & "and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang"
                ''SQL = SQL & "), 0) as jumlahPR "

                'SQL = SQL & "isnull((select "

                'If Txt_Faktur_MaterialReq.Visible = True And Not Txt_Faktur_MaterialReq.Text.Trim.Length = 0 Then
                '    SQL = SQL & "sum(y.jumlah) - a.jumlah "
                'Else
                '    SQL = SQL & "sum(y.jumlah) "
                'End If

                'SQL = SQL & "from EMI_Purchase_Requisition_Barang_Lain x, EMI_Purchase_Requisition_Barang_Lain_Detail y "
                'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and y.Kode_Perusahaan = a.Kode_Perusahaan "
                'SQL = SQL & "and y.Kode_Stock_Owner = a.Kode_Stock_Owner "
                'SQL = SQL & "and y.Kode_Barang = a.Kode_Barang and x.Status is null "
                'SQL = SQL & "and x.no_fak_material_requisition  = '" & Txt_Faktur_MaterialReq.Text & "' "
                'SQL = SQL & "), 0) as jumlahPR, "

                'SQL = SQL & "a.ID_Cost_Center, a.Id_Gedung, a.Flag_Stock, a.Urut_Departement, a.No_Urut, a.estimasi "

                'SQL = SQL & "from EMI_Purchase_Requisition_Barang_Lain_Detail a, barang_lain b where "
                'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner "
                'SQL = SQL & "and a.kode_barang = b.kode_barang "
                'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and a.no_faktur = '" & Txt_NoFaktur.Text & "'  "
                'SQL = SQL & "and b.Kode_Stock_Owner = '" & arrKdSOGudang(Cmb_Kategori_Gudang.SelectedIndex) & "' "

#End Region


                SQL = "select a.Kode_Kategori_Gudang, a.kode_perusahaan, b.kode_stock_owner, b.kode_barang, c.nama, b.jumlah, b.satuan, b.tanggal_delivery, b.keterangan, b.Link, b.Estimasi_Harga, "
                SQL = SQL & "b.ID_Cost_Center, b.Id_Gedung, b.Flag_Stock, b.Urut_Departement, b.No_Urut, b.estimasi, "
                SQL = SQL & "ISNULL(( "
                SQL = SQL & "select sum(x.Nilai_PPIC) from EMI_Transaksi_Material_Requsition_detail x "
                SQL = SQL & "where x.Kode_Perusahaan = b.Kode_Perusahaan and x.Kode_Stock_Owner = b.Kode_Stock_Owner and x.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and x.No_Faktur = '" & Txt_Faktur_MaterialReq.Text & "' "
                SQL = SQL & "), 0) as Nilai_PPIC, "
                SQL = SQL & "isnull(( "
                SQL = SQL & "SELECT "
                If Txt_Faktur_MaterialReq.Visible = True And Not Txt_Faktur_MaterialReq.Text.Trim.Length = 0 Then
                    SQL = SQL & "sum(y.jumlah) - a.jumlah "
                Else
                    SQL = SQL & "sum(y.jumlah) "
                End If
                SQL = SQL & "from EMI_Purchase_Requisition_Barang_Lain x, EMI_Purchase_Requisition_Barang_Lain_Detail y "
                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and y.Kode_Perusahaan = a.Kode_Perusahaan "
                SQL = SQL & "and y.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and y.Kode_Barang = b.Kode_Barang and x.Status is null "
                SQL = SQL & "and x.no_fak_material_requisition  = '" & Txt_Faktur_MaterialReq.Text & "' "
                SQL = SQL & "), 0) as jumlahPR , Flag_Harga_Pembelian_Akhir "
                SQL = SQL & "FROM EMI_Purchase_Requisition_Barang_Lain a "
                SQL = SQL & "inner JOIN EMI_Purchase_Requisition_Barang_Lain_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "inner JOIN barang_lain c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.kode_barang = c.Kode_Barang "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.status is null "
                SQL = SQL & "and a.no_faktur = '" & Txt_NoFaktur.Text & "' "
                SQL = SQL & "and a.Kode_Kategori_Gudang = '" & arrKategoriGudang(Cmb_Kategori_Gudang.SelectedIndex) & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                Dgv_DataBarang.Rows.Add(1)
                                Dgv_DataBarang.Rows(i).Cells(CellLokasi).Value = .Rows(i).Item("kode_stock_owner")
                                Dgv_DataBarang.Rows(i).Cells(CellKdBrg).Value = .Rows(i).Item("kode_barang")
                                Dgv_DataBarang.Rows(i).Cells(CellNmBrg).Value = .Rows(i).Item("nama")
                                Dgv_DataBarang.Rows(i).Cells(CellQty).Value = Format(.Rows(i).Item("jumlah"), "N2")
                                Dgv_DataBarang.Rows(i).Cells(CellQtyByForecast).Value = .Rows(i).Item("jumlah")
                                Dgv_DataBarang.Rows(i).Cells(CellSatuan).Value = .Rows(i).Item("satuan")
                                Dgv_DataBarang.Rows(i).Cells(CellTglDeli).Value = Format(.Rows(i).Item("tanggal_delivery"), "dd MMM yyyy")
                                Dgv_DataBarang.Rows(i).Cells(CellSisa).Value = (.Rows(i).Item("Nilai_PPIC") - .Rows(i).Item("jumlahPR"))


                                If General_Class.CekNULL(.Rows(i).Item("keterangan")) = "" Then
                                    Dgv_DataBarang.Rows(i).Cells(CellKet).Value = ""
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellKet).Value = .Rows(i).Item("keterangan")
                                End If


                                If publicFlagRelease = "T" Then
                                    Dgv_DataBarang.Rows(i).Cells(CellLokasi).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellKdBrg).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellNmBrg).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellQty).ReadOnly = False

                                    Dgv_DataBarang.Rows(i).Cells(CellSatuan).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellTglDeli).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellKet).ReadOnly = False
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellLokasi).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellKdBrg).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellNmBrg).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellQty).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellSatuan).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellTglDeli).ReadOnly = True
                                    Dgv_DataBarang.Rows(i).Cells(CellKet).ReadOnly = True

                                End If

                                Dgv_DataBarang.Rows(i).Cells(CellLink).Value = .Rows(i).Item("Link")

                                If General_Class.CekNULL(.Rows(i).Item("ID_Cost_Center")) = "" Then
                                    Dgv_DataBarang.Rows(i).Cells(CellIDCostCenter).Value = Ket_Cost_Center_HO
                                    Dgv_DataBarang.Rows(i).Cells(CellNmCostCenter).Value = "-"
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellQtyByForecast).Value = .Rows(i).Item("ID_Cost_Center")

                                    SQL = "select Keterangan from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Cost_Center = '" & .Rows(i).Item("ID_Cost_Center") & "' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dgv_DataBarang.Rows(i).Cells(CellNmCostCenter).Value = Dr("Keterangan")
                                        Else
                                            CloseConn()
                                            MessageBox.Show("Cost Center Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using



                                End If

                                If General_Class.CekNULL(.Rows(i).Item("Id_Gedung")) = "" Then
                                    Dgv_DataBarang.Rows(i).Cells(CellIDGedung).Value = ""
                                    Dgv_DataBarang.Rows(i).Cells(CellNmGedung).Value = "-"
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellIDGedung).Value = .Rows(i).Item("Id_Gedung")

                                    SQL = "select Keterangan from N_EMI_Master_Gedung_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and ID_Gedung = '" & .Rows(i).Item("Id_Gedung") & "' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dgv_DataBarang.Rows(i).Cells(CellNmGedung).Value = Dr("Keterangan")
                                        Else
                                            CloseConn()
                                            MessageBox.Show("Gedung Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using

                                End If

                                If General_Class.CekNULL(.Rows(i).Item("Flag_Stock")) = "" Then
                                    Dgv_DataBarang.Rows(i).Cells(CellIsStock).Value = "-"
                                    Dgv_DataBarang.Rows(i).Cells(CellNmStock).Value = "-"
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellIsStock).Value = .Rows(i).Item("Flag_Stock")
                                    If .Rows(i).Item("Flag_Stock") = "Y" Then
                                        Dgv_DataBarang.Rows(i).Cells(CellNmStock).Value = "Stock"
                                    Else
                                        Dgv_DataBarang.Rows(i).Cells(CellNmStock).Value = "Non Stock"
                                    End If
                                End If

                                If General_Class.CekNULL(.Rows(i).Item("Urut_Departement")) = "" Then
                                    Dgv_DataBarang.Rows(i).Cells(CellUrut_Departement).Value = ""
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellUrut_Departement).Value = .Rows(i).Item("Urut_Departement")
                                End If

                                Dgv_DataBarang.Rows(i).Cells(CellUrut).Value = .Rows(i).Item("No_Urut")

                                If General_Class.CekNULL(.Rows(i).Item("estimasi_harga")) = "" Then
                                    Dgv_DataBarang.Rows(i).Cells(CellEst).Value = ""
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellEst).Value = Format(.Rows(i).Item("Estimasi_Harga"), "N2")




                                End If

                                If General_Class.CekNULL(.Rows(i).Item("estimasi")) = "" Then
                                    Dgv_DataBarang.Rows(i).Cells(CellEstTiba).Value = ""
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellEstTiba).Value = .Rows(i).Item("estimasi")
                                End If


                                If General_Class.CekNULL(.Rows(i).Item("flag_harga_pembelian_akhir")) = "" Then
                                    Dgv_DataBarang.Rows(i).Cells(cellFlagEst).Value = "T"

                                    Dgv_DataBarang.Rows(i).Cells(CellEst).ReadOnly = False

                                Else
                                    Dgv_DataBarang.Rows(i).Cells(cellFlagEst).Value = .Rows(i).Item("flag_harga_pembelian_akhir")


                                    Dgv_DataBarang.Rows(i).Cells(CellEst).ReadOnly = True
                                End If




                            Next

                            If publicFlagRelease = "T" Then
                                Dgv_DataBarang.Rows.Add(1)
                            End If

                        End If
                    End With
                End Using
            End If

            Btn_Simpan.Tag = "&Update"

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Dgv_DataBarang_DoubleClick(sender As Object, e As EventArgs) Handles Dgv_DataBarang.DoubleClick
        'If Dgv_DataBarang.Rows.Count = 0 Then
        '    MessageBox.Show("Silahkan Pilih Barang yang akan dihapus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        'If Hapus1 = vbYes Then
        '    Dim currentRow = Dgv_DataBarang.CurrentRow.Index
        '    Dgv_DataBarang.Rows.RemoveAt(currentRow)
        'Else
        '    MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'End If

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    'awal stenly 15-12
    Private Sub Btn_Unrelease_Click(sender As Object, e As EventArgs) Handles Btn_Unrelease.Click
        If Txt_NoFaktur.Text.Trim.Length = 0 Then Exit Sub
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("UnRelease_Purchase_Requisition_Barang_Lain") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Unrelease PR Warehouse", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin akan Unrelease Purhcase Requisition ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then Exit Sub

            SQL = "select Status from EMI_Purchase_Requisition_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Txt_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Purhcase Requisition sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Requisition tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur from EMI_Purchase_Requisition_Barang_Lain a,"
            SQL = SQL & "EMI_Purchase_Requisition_Barang_Lain_Detail b, EMI_Pembelian_PO_Det_Induk_Barang_Lain c,"
            SQL = SQL & "EMI_Pembelian_PO_Induk_Barang_Lain d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur "
            SQL = SQL & "and b.No_Urut = c.No_Urut_PR and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and d.Status is null and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Requisition tidak bisa diunrelease, karena sudah masuk tahap Purhcase Order!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Path_File,Container_File from EMI_Purchase_Requisition_Barang_Lain_Attachment "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            If General_Class.CekNULL(.Rows(i).Item("path_file")) <> "" Then

                                AzureHelper_EMI.DeleteFromAzure(
                                    .Rows(i).Item("Container_File"),
                                    .Rows(i).Item("Path_File")
                                )
                            End If

                        Next
                    End If
                End With
            End Using

            'reza tambahan pra release
            SQL = "update EMI_Purchase_Requisition_Barang_Lain_Attachment set  "
            SQL = SQL & "status = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text & "' "
            ExecuteTrans(SQL)

            SQL = "update EMI_Purchase_Requisition_Barang_Lain set Flag_Release = NULL, "
            SQL = SQL & "tanggal_release = NULL, "
            SQL = SQL & "jam_release = NULL, "
            SQL = SQL & "user_release = NULL ,"
            SQL = SQL & "tanggal_pra_release = NULL, "
            SQL = SQL & "jam_pra_release = NULL, "
            SQL = SQL & "user_pra_release = NULL ,"
            SQL = SQL & "flag_pra_release = NULL "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text & "' "
            ExecuteTrans(SQL)

            SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Log_Release("
            SQL = SQL & "Kode_Perusahaan, No_Faktur, User_Id, Tanggal, Jam, Keterangan) values("
            SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & UserID & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'UNRELEASE') "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Purhcase Requisition berhasil diunrelease.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Txt_NoFaktur_Leave(Btn_Unrelease, e)
    End Sub


    'akhir stenly 15-12

    Private Sub Dgv_DataBarang_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataBarang.CellContentClick
        If Dgv_DataBarang.Columns(e.ColumnIndex).Name = "btnView" Then
            ' pastikan bukan header
            If e.RowIndex < 0 Then Exit Sub

            If Dgv_DataBarang.Rows.Count - 1 = e.RowIndex Then Exit Sub


            Dim row As DataGridViewRow = Dgv_DataBarang.Rows(e.RowIndex)


            ' ambil value dari kolom mana pun
            ' Dim id As String = row.Cells(0).Value.ToString()
            Dim kode_barang As String = row.Cells(1).Value.ToString()

            N_EMI_Show_Katalog_Barang.kode_barang = kode_barang
            N_EMI_Show_Katalog_Barang.ShowDialog()
        End If
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles BtnPR_Release.Click
        get_jam()

        Try
            OpenConn()

            If CekButtonRole("Release_Purchase_Requisition_Barang_Lain") = "T" Then
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If


            SQL = "select no_faktur From  EMI_Purchase_Requisition_Barang_Lain_Detail where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_faktur = '" & Txt_NoFaktur.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Gagal Release, barang harus di isi terlebih dahulu.")
                    Exit Sub
                End If
            End Using

            'awal stenly 15-12
            For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
                Get_Isi_Listview(i)
                SQL = "select Jumlah from EMI_Purchase_Requisition_Barang_Lain_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and No_Urut = '" & LvUrut & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If Val(HilangkanTanda(LvQty)) <> dr("Jumlah") Then
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Jumlah Purchase Requisition ada yang berbeda! " & LvNmBrg, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Purchase Requisition tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            Next
            'akhir stenly 15-12

            no_Faktur_Sementara = String.Empty

            SQL = "select status, flag_po, flag_pra_release from EMI_Purchase_Requisition_Barang_Lain "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_faktur = '" & Txt_NoFaktur.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("status")) = "Y" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("flag_po")) = "Y" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Transaksi tidak bisa dilanjutkan, karena PR sudah selesai di PO", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("flag_pra_release")) = "Y" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("PR sudah pernah direlease!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Produksi Order tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            no_Faktur_Sementara = Txt_NoFaktur.Text

            SQL = "update EMI_Purchase_Requisition_Barang_Lain set flag_pra_release = 'Y', "
            SQL = SQL & "tanggal_pra_release = '" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "jam_pra_release = '" & Format(tgl_skg, "HH:mm:ss") & "',"
            SQL = SQL & "user_pra_release = '" & UserID & "' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
            ExecuteTrans(SQL)

            SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Log_Release("
            SQL = SQL & "Kode_Perusahaan, No_Faktur, User_Id, Tanggal, Jam, Keterangan) values("
            SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & UserID & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'PRA RELEASE') "
            ExecuteTrans(SQL)

            CloseConn()
            MessageBox.Show("Data berhasil direlease ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '==========================
        '=      CETAK REPORT      =
        '==========================
        get_jam()

        Try
            OpenConn()

            SQL = "select a.No_Faktur from N_EMI_Transaksi_Purchase_Requisition_Barang_Lain_View a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & no_Faktur_Sementara & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New Faktur_Purchase_Requisition_Barang_Lain
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition Barang Lain"
                            CrDoc.RecordSelectionFormula = " {N_EMI_Transaksi_Purchase_Requisition_Barang_Lain_View.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Transaksi_Purchase_Requisition_Barang_Lain_View.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"
                            CrDoc.SetParameterValue("tanggalCetak", Format(tgl_skg, "dd MMM yyyy HH:mm:ss"))
                            .Text = "Laporan Faktur Purchase Requisition Barang Lain"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With
                    End With
                Else
                    MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Dgv_DataBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgv_DataBarang.KeyDown
        If Dgv_DataBarang.Rows.Count = 0 Or Dgv_DataBarang.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = Dgv_DataBarang.CurrentRow.Index
        Dim currentCell = Dgv_DataBarang.CurrentCellAddress.X

        If e.KeyCode = Keys.F1 Then

            If Not publicFlagRelease = "Y" Then

                SD_Tambah_PR_Barang_Lain.filter_tambahan = "and c.Kode_Stock_Owner = '" & cmb_lokasi.Text & "' "
                'SD_Tambah_PR_Barang_Lain.asal = cmb_lokasi.Text
                SD_Tambah_PR_Barang_Lain.asal = Jenis
                SD_Tambah_PR_Barang_Lain.asal = Jenis
                SD_Tambah_PR_Barang_Lain.faktur_MR = Txt_Faktur_MaterialReq.Text
                SD_Tambah_PR_Barang_Lain.SO_Kategori_Gudang_Pilih = arrKdSOGudang(Cmb_Kategori_Gudang.SelectedIndex)
                SD_Tambah_PR_Barang_Lain.TxtPilihBarang_KodeBarang.Visible = True
                SD_Tambah_PR_Barang_Lain.TxtPilihBarang_Satuan.Visible = True
                SD_Tambah_PR_Barang_Lain.TxtPilihBarang_NamaBarang.Visible = True
                SD_Tambah_PR_Barang_Lain.LblPilihBarang_NamaBarang.Visible = True
                SD_Tambah_PR_Barang_Lain.LblPilihBarang_KodeBarang.Visible = True

                SD_Tambah_PR_Barang_Lain.Lbl_PR.Visible = False
                SD_Tambah_PR_Barang_Lain.Lbl_Order.Visible = False
                SD_Tambah_PR_Barang_Lain.Lbl_Sisa.Visible = False
                SD_Tambah_PR_Barang_Lain.Txt_PR.Visible = False
                SD_Tambah_PR_Barang_Lain.Txt_Order.Visible = False
                SD_Tambah_PR_Barang_Lain.Txt_Sisa.Visible = False
                SD_Tambah_PR_Barang_Lain.ShowDialog()

            End If

        ElseIf e.KeyCode = Keys.Delete Then
            If Dgv_DataBarang.Rows.Count = 0 Then
                MessageBox.Show("Silahkan Pilih Barang yang akan dihapus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            If Not Dgv_DataBarang.CurrentRow.Cells(CellLokasi).Value = "" Then
                If Not publicFlagRelease = "Y" Then
                    Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If Hapus1 = vbYes Then
                        If Convert.ToString(Dgv_DataBarang.CurrentRow.Cells(CellUrut_Departement).Value).Trim() <> "" And Convert.ToString(Dgv_DataBarang.CurrentRow.Cells(CellUrut).Value).Trim() <> "" Then
                            Try
                                OpenConn()
                                Cmd.Transaction = Cn.BeginTransaction

                                Dim xNo_Faktur As String = ""
                                SQL = "select Jumlah, Jmlh_PR, No_Faktur from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & Dgv_DataBarang.CurrentRow.Cells(CellUrut_Departement).Value & "' "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        xNo_Faktur = dr("No_Faktur")
                                    End If
                                End Using

                                SQL = "insert into EMI_Purchase_Requisition_Barang_Lain_Detail_log(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan, Urut_Departement) "
                                SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah,Satuan, Tanggal_Delivery, keterangan, Urut_Departement "
                                SQL = SQL & "from EMI_Purchase_Requisition_Barang_Lain_Detail where kode_perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & Dgv_DataBarang.CurrentRow.Cells(CellUrut).Value & "' "
                                ExecuteTrans(SQL)

                                SQL = "delete from EMI_Purchase_Requisition_Barang_Lain_Detail where kode_perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & Dgv_DataBarang.CurrentRow.Cells(CellUrut).Value & "' "
                                ExecuteTrans(SQL)

                                SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set Flag_PR = NULL "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & xNo_Faktur & "' "
                                ExecuteTrans(SQL)

                                Dim xJumlah As Integer = 0
                                SQL = "select sum(b.Jumlah) as Jumlah from EMI_Purchase_Requisition_Barang_Lain a, EMI_Purchase_Requisition_Barang_Lain_Detail b where "
                                SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and a.no_faktur=b.no_faktur and a.status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and b.Urut_Departement = '" & Dgv_DataBarang.CurrentRow.Cells(CellUrut_Departement).Value & "' "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        If General_Class.CekNULL(dr("Jumlah")) <> "" Then
                                            xJumlah = dr("Jumlah")
                                        Else
                                            xJumlah = 0
                                        End If
                                    End If
                                End Using

                                SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set Jmlh_PR = '" & xJumlah & "', Flag_Sudah_PR = NULL "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & Dgv_DataBarang.CurrentRow.Cells(CellUrut_Departement).Value & "' "
                                ExecuteTrans(SQL)

                                Cmd.Transaction.Commit()
                                CloseConn()
                            Catch ex As Exception
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show(ex.Message)
                                Exit Sub
                            End Try
                        End If

                        Dgv_DataBarang.Rows.RemoveAt(currentRow)
                    Else
                        MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub Dgv_DataBarang_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataBarang.CellDoubleClick
        If Dgv_DataBarang.Rows.Count = 0 Then
            Exit Sub
        End If


        If publicFlagRelease = "T" Then
            Dim currentRow = Dgv_DataBarang.CurrentRow.Index
            Dim currentCell = Dgv_DataBarang.CurrentCell.ColumnIndex

            If currentRow = Dgv_DataBarang.Rows.Count - 1 Then
                Exit Sub
            End If
            If currentCell = 5 Then

                SD_Ubah_Tanggal_PR_Barang_Lain.DTP_Delivery.Value = Dgv_DataBarang.Rows(currentRow).Cells(currentCell).Value
                SD_Ubah_Tanggal_PR_Barang_Lain.rowDgv = currentRow
                SD_Ubah_Tanggal_PR_Barang_Lain.cellDgv = currentCell
                SD_Ubah_Tanggal_PR_Barang_Lain.EstTiba = Dgv_DataBarang.Rows(currentRow).Cells(CellEstTiba).Value

                SD_Ubah_Tanggal_PR_Barang_Lain.ShowDialog()

            End If
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Cmb_Kategori_Gudang.SelectedIndex = -1 Then
            MessageBox.Show("Kategori Gudang belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Kategori_Gudang.Focus()
            Exit Sub
        End If

        N_EMI_Display_Request_Departement_Barang_Lain.asal = "Purchase_Requisition_Barang_Lain"
        N_EMI_Display_Request_Departement_Barang_Lain.xCmb_Kategori_Gudang = Cmb_Kategori_Gudang.Text
        'N_EMI_Display_Request_Departement_Barang_Lain.KdSoKategori = arrKdSOGudang(Cmb_Kategori_Gudang.SelectedIndex) 'ini harusnya tidak di comment
        N_EMI_Display_Request_Departement_Barang_Lain.ShowDialog()
        'N_EMI_SD_Purchase_Requisition_Barang_Lain_Departement.ShowDialog()
    End Sub

    Private Sub Dgv_DataBarang_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataBarang.CellEndEdit
        Get_Isi_Listview(Dgv_DataBarang.CurrentRow.Index)

        If LvKdBrg = "" Then
            Dgv_DataBarang.CurrentCell.Value = ""
            Exit Sub
        End If

        If Val(LvQty) < 0 Or IsNumeric(LvQty) = False Then
            If LvQtyByForecast Is Nothing Then
                LvQtyByForecast = 0
            End If
            Dgv_DataBarang.CurrentRow.Cells(CellQty).Value = Format(Val(LvQtyByForecast), "N2")
        End If

        If Val(LvEst) < 0 Or IsNumeric(LvEst) = False Then
            If LvEst Is Nothing Then
                LvEst = 0
            End If
            Dgv_DataBarang.CurrentRow.Cells(CellEst).Value = Format(Val(LvEst), "N2")
        End If


        '===========================================
        '=     CEK APAKAH JUMLAH MELEBIHI SISA     =
        '===========================================
        If Val(LvSisa) <> 0 Then
            If Val(LvQty) > Val(LvSisa) Then
                MessageBox.Show("Jumlah Tidak Boleh Lebih dari Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_DataBarang.CurrentRow.Cells(CellQty).Value = Format(Val(LvQtyByForecast), "N2")
                Exit Sub
            End If
        End If



        '======================
        '=     SET FORMAT     =
        '======================
        If Dgv_DataBarang.CurrentCell.ColumnIndex = CellQty Then

            Dim cellKuantity As String = Dgv_DataBarang.CurrentRow.Cells(CellQty).Value

            If cellKuantity.Contains(",") Then
                'MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_DataBarang.CurrentRow.Cells(CellQty).Value = 0
                Exit Sub
            End If

            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

            Dgv_DataBarang.CurrentRow.Cells(CellQty).Value = formattedValue
        End If

        If Dgv_DataBarang.CurrentCell.ColumnIndex = CellEst Then

            Dim cellKuantity As String = Dgv_DataBarang.CurrentRow.Cells(CellEst).Value

            If cellKuantity.Contains(",") Then
                'MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_DataBarang.CurrentRow.Cells(CellEst).Value = 0
                Exit Sub
            End If

            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

            Dgv_DataBarang.CurrentRow.Cells(CellEst).Value = formattedValue
        End If
    End Sub

    Private Sub Dgv_DataBarang_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataBarang.CellEnter

        If Dgv_DataBarang.CurrentCell.ColumnIndex = CellQty Then
            Dim cellKuantity As String = Dgv_DataBarang.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If



            Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            Dgv_DataBarang.CurrentCell.Value = nilai
        End If

        If Dgv_DataBarang.CurrentCell.ColumnIndex = CellEst Then
            Dim cellKuantity As String = Dgv_DataBarang.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            Dgv_DataBarang.CurrentCell.Value = nilai
        End If
    End Sub

    Private Sub Dgv_DataBarang_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataBarang.CellLeave

        If Dgv_DataBarang.CurrentCell.ColumnIndex = CellQty Then
            Dim cellKuantity As String = Dgv_DataBarang.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If



            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

            Dgv_DataBarang.CurrentCell.Value = formattedValue

        End If

        If Dgv_DataBarang.CurrentCell.ColumnIndex = CellEst Then
            Dim cellKuantity As String = Dgv_DataBarang.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If



            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

            Dgv_DataBarang.CurrentCell.Value = formattedValue

        End If
    End Sub
    Private Sub Txt_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub Txt_NoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoFaktur.KeyPress
        If e.KeyChar = Chr(13) Then Dgv_DataBarang.Focus()
    End Sub

    Public Sub cari_pr_departement()
        Dim AksesSimpanPR As String = ""
        Dim AksesReleasePR As String = ""


        Try
            OpenConn()

            Dgv_DataBarang.Rows.Clear()
            SQL = "select a.no_faktur,a.kode_perusahaan, a.kode_stock_owner, a.kode_barang, a.nama_barang, a.jumlah, a.satuan, a.tanggal_delivery, a.keterangan, a.Link, a.Estimasi_Harga, "

            SQL = SQL & "ISNULL((select sum(x.Nilai_PPIC) from EMI_Transaksi_Material_Requsition_detail x "
            SQL = SQL & "where x.Kode_Perusahaan = a.Kode_Perusahaan and x.Kode_Stock_Owner = a.Kode_Stock_Owner and x.Kode_Barang = a.Kode_Barang "
            SQL = SQL & "and x.No_Faktur = '" & Txt_Faktur_MaterialReq.Text & "' "
            SQL = SQL & "), 0) as Nilai_PPIC, a.Jmlh_PR, "

            SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null),0) as Jumlah_Keep_Stock, "

            'SQL = SQL & "ISNULL(( select (SUM(z.jumlah) - a.jumlah) from EMI_Purchase_Requisition_Barang_Lain_Detail z where z.Kode_Perusahaan = a.Kode_Perusahaan "
            'SQL = SQL & "and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang"
            'SQL = SQL & "), 0) as jumlahPR "

            SQL = SQL & "isnull((select "

            If Txt_Faktur_MaterialReq.Visible = True And Not Txt_Faktur_MaterialReq.Text.Trim.Length = 0 Then
                SQL = SQL & "sum(y.jumlah) - a.jumlah "
            Else
                SQL = SQL & "sum(y.jumlah) "
            End If

            SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement x, N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and y.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and y.Kode_Stock_Owner = a.Kode_Stock_Owner "
            SQL = SQL & "and y.Kode_Barang = a.Kode_Barang and x.Status is null "
            SQL = SQL & "and x.no_fak_material_requisition  = '" & Txt_Faktur_MaterialReq.Text & "' "
            SQL = SQL & "), 0) as jumlahPR, "

            SQL = SQL & "a.ID_Cost_Center, a.Id_Gedung, a.Flag_Stock, a.No_Urut,a.Estimasi_Harga "

            SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Urut in (" & Label3.Text & ") "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_DataBarang.Rows.Add(1)
                            Dgv_DataBarang.Rows(i).Cells(CellLokasi).Value = .Rows(i).Item("kode_stock_owner")
                            Dgv_DataBarang.Rows(i).Cells(CellKdBrg).Value = .Rows(i).Item("kode_barang")
                            Dgv_DataBarang.Rows(i).Cells(CellNmBrg).Value = .Rows(i).Item("nama_barang")
                            Dgv_DataBarang.Rows(i).Cells(CellQty).Value = Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR") - .Rows(i).Item("Jumlah_Keep_Stock"), "N2")
                            Dgv_DataBarang.Rows(i).Cells(CellQtyByForecast).Value = .Rows(i).Item("jumlah")
                            Dgv_DataBarang.Rows(i).Cells(CellSatuan).Value = .Rows(i).Item("satuan")
                            Dgv_DataBarang.Rows(i).Cells(CellTglDeli).Value = Format(.Rows(i).Item("tanggal_delivery"), "dd MMM yyyy")
                            Dgv_DataBarang.Rows(i).Cells(CellSisa).Value = (.Rows(i).Item("Nilai_PPIC") - .Rows(i).Item("jumlahPR"))


                            If General_Class.CekNULL(.Rows(i).Item("keterangan")) = "" Then
                                Dgv_DataBarang.Rows(i).Cells(CellKet).Value = ""
                            Else
                                Dgv_DataBarang.Rows(i).Cells(CellKet).Value = .Rows(i).Item("keterangan")
                            End If


                            If publicFlagRelease = "T" Then
                                Dgv_DataBarang.Rows(i).Cells(CellLokasi).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellKdBrg).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellNmBrg).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellQty).ReadOnly = False

                                Dgv_DataBarang.Rows(i).Cells(CellSatuan).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellTglDeli).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellKet).ReadOnly = True
                            Else
                                Dgv_DataBarang.Rows(i).Cells(CellLokasi).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellKdBrg).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellNmBrg).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellQty).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellSatuan).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellTglDeli).ReadOnly = True
                                Dgv_DataBarang.Rows(i).Cells(CellKet).ReadOnly = True

                            End If

                            Dgv_DataBarang.Rows(i).Cells(CellLink).Value = .Rows(i).Item("Link")

                            If General_Class.CekNULL(.Rows(i).Item("ID_Cost_Center")) = "" Then
                                Dgv_DataBarang.Rows(i).Cells(CellIDCostCenter).Value = Ket_Cost_Center_HO
                                Dgv_DataBarang.Rows(i).Cells(CellNmCostCenter).Value = "-"
                            Else
                                Dgv_DataBarang.Rows(i).Cells(CellQtyByForecast).Value = .Rows(i).Item("ID_Cost_Center")

                                SQL = "select Keterangan from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Cost_Center = '" & .Rows(i).Item("ID_Cost_Center") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dgv_DataBarang.Rows(i).Cells(CellNmCostCenter).Value = Dr("Keterangan")
                                    Else
                                        CloseConn()
                                        MessageBox.Show("Cost Center Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Id_Gedung")) = "" Then
                                Dgv_DataBarang.Rows(i).Cells(CellIDGedung).Value = ""
                                Dgv_DataBarang.Rows(i).Cells(CellNmGedung).Value = "-"
                            Else
                                Dgv_DataBarang.Rows(i).Cells(CellIDGedung).Value = .Rows(i).Item("Id_Gedung")

                                SQL = "select Keterangan from N_EMI_Master_Gedung_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and ID_Gedung = '" & .Rows(i).Item("Id_Gedung") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dgv_DataBarang.Rows(i).Cells(CellNmGedung).Value = Dr("Keterangan")
                                    Else
                                        CloseConn()
                                        MessageBox.Show("Gedung Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Stock")) = "" Then
                                Dgv_DataBarang.Rows(i).Cells(CellIsStock).Value = "-"
                                Dgv_DataBarang.Rows(i).Cells(CellNmStock).Value = "-"
                            Else
                                Dgv_DataBarang.Rows(i).Cells(CellIsStock).Value = .Rows(i).Item("Flag_Stock")
                                If .Rows(i).Item("Flag_Stock") = "Y" Then
                                    Dgv_DataBarang.Rows(i).Cells(CellNmStock).Value = "Stock"
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellNmStock).Value = "Non Stock"
                                End If
                            End If
                            Dgv_DataBarang.Rows(i).Cells(CellUrut_Departement).Value = .Rows(i).Item("No_Urut")

                            If General_Class.CekNULL(.Rows(i).Item("Estimasi_Harga")) = "" Then
                                Dgv_DataBarang.Rows(i).Cells(CellEst).Value = ""
                            Else
                                Dgv_DataBarang.Rows(i).Cells(CellEst).Value = Format(.Rows(i).Item("Estimasi_Harga"), "N2")


                                If .Rows(i).Item("Estimasi_Harga") <> 0 Then

                                    Dgv_DataBarang.Rows(i).Cells(cellFlagEst).Value = "Y"
                                    Dgv_DataBarang.Rows(i).Cells(CellEst).ReadOnly = True
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(cellFlagEst).Value = "T"
                                    Dgv_DataBarang.Rows(i).Cells(CellEst).ReadOnly = False
                                End If
                            End If


                            SQL = "select top(1) Waktu_Pengiriman from emi_detail_proses_pengiriman_po_Barang_Lain where Kode_Barang= '" & .Rows(i).Item("kode_barang") & "' "
                            SQL = SQL & "order by Waktu_Pengiriman Desc "
                            Using dr2 = OpenTrans(SQL)
                                If dr2.Read Then
                                    Dgv_DataBarang.Rows(i).Cells(CellEstTiba).Value = dr2("Waktu_Pengiriman")
                                Else
                                    Dgv_DataBarang.Rows(i).Cells(CellEstTiba).Value = 0
                                End If
                            End Using


                            Dgv_DataBarang.Rows(i).Cells(0).ReadOnly = True
                            Dgv_DataBarang.Rows(i).Cells(1).ReadOnly = True
                            Dgv_DataBarang.Rows(i).Cells(2).ReadOnly = True
                            Dgv_DataBarang.Rows(i).Cells(3).ReadOnly = False
                            Dgv_DataBarang.Rows(i).Cells(4).ReadOnly = True
                            Dgv_DataBarang.Rows(i).Cells(5).ReadOnly = True
                            Dgv_DataBarang.Rows(i).Cells(6).ReadOnly = False

                        Next

                        If publicFlagRelease = "T" Then
                            Dgv_DataBarang.Rows.Add(1)
                        End If

                    End If
                End With
            End Using
            Cmb_Kategori_Gudang.Enabled = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub HasData_DGV()
        If Dgv_DataBarang.Rows.Count <> 0 Then
            Cmb_Kategori_Gudang.Enabled = False
        Else
            Cmb_Kategori_Gudang.Enabled = True
        End If
    End Sub



    '======================================================================================================================================================
    '=     CEGAH AGAR KETIKA TITLE BAR DOUBLE KLIK TIDAK MAXIMIZED
    '======================================================================================================================================================
    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub


End Class
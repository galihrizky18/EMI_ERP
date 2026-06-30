Imports System.Security.Policy

Public Class N_EMI_Purchase_Requisition_Barang_Lain_Departement

    Dim arrCari, arrKd_biaya, arrKeterangan As New ArrayList

    Dim arrKategoriGudang, arrKdSOGudang, arrInisialFaktur As New ArrayList

    Dim Jenis = "Purchase_Requisition_Barang_Lain_Departement"

    Dim no_Faktur_Sementara As String = ""

    Dim publicFlagRelease = "T"

    Private Sub N_EMI_Purchase_Requisition_Barang_Lain_Departement_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub get_no_faktur()
        Txt_NoFaktur.Text = fPurchaseRequisitionDP & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Purchase_Requisition_Barang_Lain_Departement", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fPurchaseRequisitionDP) + 4 & ")", fPurchaseRequisitionDP & Format(tgl_skg, "MMyy"))
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
    Dim LvLink As String
    Dim LvIdCostCenter As String
    Dim LvIdGedung As String
    Dim LvIsStock As String
    Dim LvNmCostCenter As String
    Dim LvNmGedung As String
    Dim LvNmStock As String
    Dim LvKategori As String
    Dim LvId As String
    Dim LvEst As String
    Dim LvFlagEst As String
    Dim LvEstHari As String
    Dim LvValueBtn As String


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
    Dim CellKategori As Integer = 16
    Dim CellId As Integer = 17
    Dim CellEst As Integer = 18
    Dim cellFlagEst As Integer = 19
    Dim cellEstHari As Integer = 20
    Dim cellBtnView As Integer = 21
    'Dim cellValueBtn As Integer = 21
    Dim enablePraRelease As Boolean = False
    Dim AksesSimpanPR As String = ""
    Dim AksesReleasePR As String = ""
    Dim AksesUnRelease As String = ""
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
        LvKategori = Dgv_DataBarang.Rows(No_Index).Cells(CellKategori).Value
        LvId = Dgv_DataBarang.Rows(No_Index).Cells(CellId).Value
        LvEst = Dgv_DataBarang.Rows(No_Index).Cells(CellEst).Value
        LvFlagEst = Dgv_DataBarang.Rows(No_Index).Cells(cellFlagEst).Value
        LvEstHari = Dgv_DataBarang.Rows(No_Index).Cells(cellEstHari).Value
        LvValueBtn = Dgv_DataBarang.Rows(No_Index).Cells(cellBtnView).Value
        '  LvValueBtn = Dgv_DataBarang.Rows(No_Index).Cells(cellValueBtn).Value
    End Sub

    Private Sub N_EMI_Purchase_Requisition_Barang_Lain_Departement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Lbl_Judul.Text = "Purchase Requisition Barang Lain Per Departement"

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh

            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            cmb_lokasi.Enabled = False

            no_Faktur_Sementara = String.Empty

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()

    End Sub

    Public Sub kosong()
        Sync_Data_PR_Pra_Release()


        Try
            OpenConn()

            If CekButtonRole("Simpan_Purchase_Requisition_Departement") = "Y" Then
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

            If CekButtonRole("Release_Purchase_Requisition_Departement") = "Y" Then
                AksesReleasePR = "Y"
            End If

            OpenConn()

            If CekButtonRole("UnRelease_Purchase_Requisition_Departement") = "Y" Then
                AksesUnRelease = "Y"
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

        Cmb_Kategori_Gudang.Enabled = True

        Dgv_DataBarang.Enabled = True
        publicFlagRelease = "T"
        get_jam()
        Try
            OpenConn()

            SQL = $"SELECT 1 FROM N_EMI_Master_Kategori_Gudang_Barang_Lain WHERE Kode_Kategori_Gudang = '{Cmb_Kategori_Gudang.Text.Trim}' AND Flag_Kirim_Web IS NULL"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    enablePraRelease = True

                    If AksesReleasePR = "Y" Then
                        BtnPR_Release.Enabled = True
                    Else
                        BtnPR_Release.Enabled = False
                    End If

                    If AksesUnRelease = "Y" Then
                        Btn_Unrelease.Enabled = True
                    Else
                        Btn_Unrelease.Enabled = False
                    End If
                Else
                    Dr.Close()
                    enablePraRelease = False
                    BtnPR_Release.Enabled = False
                    Btn_Unrelease.Enabled = False
                End If
            End Using

            arrInisialFaktur.Clear() : cmb_lokasi.Items.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur_barang_lain from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmb_lokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur_barang_lain"))
                Loop
            End Using
            cmb_lokasi.Text = Lokasi



            Cmb_Kategori_Gudang.Items.Clear() : arrKategoriGudang.Clear() : arrKdSOGudang.Clear()
            SQL = "select a.kode_kategori_gudang, b.Kode_Stock_Owner_Gudang "
            SQL = SQL & "from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a "
            SQL = SQL & "inner JOIN N_EMI_Master_Kategori_Gudang_Barang_Lain b on a.kode_perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Urut_Oto "
            SQL = SQL & "where user_ID='" & UserID & "' and a.kode_perusahaan = '" & KodePerusahaan & "' and Jenis_Gudang = 'Department' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Kategori_Gudang.Items.Add(Dr("kode_kategori_gudang")) : arrKategoriGudang.Add(Dr("kode_kategori_gudang"))
                    arrKdSOGudang.Add(Dr("Kode_Stock_Owner_Gudang"))
                Loop
            End Using

            If Cmb_Kategori_Gudang.Items.Count <> 0 Then
                Cmb_Kategori_Gudang.SelectedIndex = 0
            End If

            OpenConn()
            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Txt_Faktur_MaterialReq.Visible = False
        Label2.Visible = False

        Lbl_IDBiaya.Text = ""
        Txt_Kd.Text = ""
        Txt_Keterangan.Text = ""
        Cmb_Kolom.SelectedIndex = -1
        Txt_Value.Text = ""
        TextBox2.Text = ""
        Txt_Faktur_MaterialReq.Text = ""
        TextBox2.ReadOnly = False

        BtnPR_Simpan.Enabled = True
        BtnPR_Release.Visible = True
        'BtnPR_Release.Enabled = True
        'Button2.Enabled = True

        Btn_Unrelease.Visible = False
        Btn_Unrelease.Enabled = False

        Dgv_DataBarang.Rows.Clear()
        Dgv_DataBarang.Rows.Add(1)

        Cmb_Kolom.Items.Clear() : arrCari.Clear() : Cmb_Kolom.SelectedIndex = -1
        Cmb_Kolom.Items.Add(Base_Language.Lang_Global_Kode) : arrCari.Add("Kode_Biaya")
        Cmb_Kolom.Items.Add(Base_Language.lang_global_keterangan) : arrCari.Add("Keterangan")

        Txt_Kd.Enabled = True
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False
        BtnPR_Release.Visible = False

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

    Private Sub BtnFormulator_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPR_Simpan.Click
        If Dgv_DataBarang.Rows.Count - 1 = 0 Then
            MessageBox.Show("Tidak ada Data yang bisa di simpan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
            Dim value = Dgv_DataBarang.Rows(i).Cells(3).Value
            Dim estimasi_harga_value = Dgv_DataBarang.Rows(i).Cells(18).Value
            If value Is Nothing OrElse value.ToString().Trim() = "" OrElse Val(value) = 0 Then
                MessageBox.Show("Qty baris ke " & (i + 1) & " belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If value Is Nothing OrElse estimasi_harga_value.ToString().Trim() = "" OrElse Val(estimasi_harga_value) = 0 Then
                MessageBox.Show("Est. Harga baris ke " & (i + 1) & " belum tidak boleh 0!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        Sync_Data_PR_Pra_Release()
        get_jam()

        Try

            OpenConn()
            OpenConnMySQL()
            Cmd.Transaction = Cn.BeginTransaction
            CmdMySQL.Transaction = CnMySQL.BeginTransaction

            If CekButtonRole("Simpan_Purchase_Requisition_Departement") = "T" Then
                CloseTransMySQL()
                CloseConnMySQL()
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            If Btn_Simpan.Tag = "&Simpan" Then
                get_no_faktur()

                Dim Tanggal As String = Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd")
                Dim Jam As String = Format(tgl_skg, "HH:mm:ss")

                SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement(Kode_Perusahaan,No_Faktur,Lokasi,Tanggal,Jam,UserId,Keterangan, Kode_Kategori_Gudang) values("
                SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "', '" & cmb_lokasi.Text & "', "
                SQL = SQL & "'" & Tanggal & "', '" & Jam & "',"
                SQL = SQL & "'" & UserID & "', '" & TextBox2.Text.Trim & "', '" & Cmb_Kategori_Gudang.Text.Trim & "' )"
                ExecuteTrans(SQL)

                If Not enablePraRelease Then
                    SQLMySQL = $"
                        INSERT INTO purchase_requisition_barang_lain_departement
                        (Kode_Perusahaan, No_Faktur, Lokasi, Tanggal, Jam, UserId, Keterangan, Kode_Kategori_Gudang)
                        VALUES
                        (
                            '{KodePerusahaan}',
                            '{Txt_NoFaktur.Text}',
                            '{cmb_lokasi.Text}',
                            '{Tanggal}',
                            '{Jam}',
                            '{UserID}',
                            '{TextBox2.Text.Trim()}',
                            '{Cmb_Kategori_Gudang.Text.Trim()}'
                        )"
                    ExecuteTransMySQL(SQLMySQL)
                End If

                SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Log(Kode_Perusahaan, No_Faktur, Lokasi, Status, Tanggal, Jam, UserId, Keterangan) "
                SQL = SQL & "select Kode_Perusahaan,No_Faktur,Lokasi,Status,'" & Format(tgl_skg, "yyyy-MM-dd") & "',Jam,'" & UserID & "',Keterangan from N_EMI_Purchase_Requisition_Barang_Lain_Departement "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

                For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
                    Get_Isi_Listview(i)

                    Dim flagest As String = ""

                    If LvFlagEst = "T" Then
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

                    Dim SelelctedIDsub As String = ""
                    If LvId = "-" Then
                        SelelctedIDsub = "NULL"
                    Else
                        SelelctedIDsub = "'" & LvId & "'"
                    End If

                    Dim fileFix As String
                    Dim containerFix As String

                    'If LvValueBtn = "" Then
                    '    fileFix = "NULL"
                    'Else
                    '    Dim namaFile As String = IO.Path.GetFileName(LvValueBtn)

                    '    fileFix = "'" & namaFile & "_" & Format(tgl_skg, "dd-MM-yyyy-HH-ii-ss") & "_" & Txt_NoFaktur.Text & "'"
                    'End If

                    SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Nama_Barang,Jumlah,Satuan,Tanggal_Delivery,keterangan, "
                    SQL = SQL & " Id_Cost_Center, ID_Gedung, Flag_Stock, Jmlh_PR, Link, Id_Sub_Kategori_Jenis, Estimasi_Harga,Flag_Harga_Pembelian_Akhir,estimasi_hari) values("
                    SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "' ,"
                    SQL = SQL & "'" & LvLokasi & "', '" & LvKdBrg & "' ,'" & LvNmBrg & "' ,"
                    SQL = SQL & "'" & HilangkanTanda(LvQty) & "',"
                    SQL = SQL & "'" & LvSatuan & "', '" & Format(CDate(LvTglDeli), "yyyy-MM-dd") & "', '" & LvKet & "', " & SelelctedIDCostCenter & ", "
                    SQL = SQL & " " & SelelctedIDGedung & ", '" & LvIsStock & "', '0', '" & LvLink & "', " & SelelctedIDsub & ",  '" & HilangkanTanda(LvEst) & "', " & flagest & ""
                    SQL = SQL & ", '" & LvEstHari & "' )"
                    ExecuteTrans(SQL)

                    If Not enablePraRelease Then
                        SQLMySQL = $"
                            INSERT INTO purchase_requisition_barang_lain_departement_detail
                            (
                                Kode_Perusahaan,
                                No_Faktur,
                                Kode_Stock_Owner,
                                Kode_Barang,
                                Nama_Barang,
                                Jumlah,
                                Satuan,
                                Tanggal_Delivery,
                                keterangan,
                                Id_Cost_Center,
                                ID_Gedung,
                                Flag_Stock,
                                Jmlh_PR,
                                Link,
                                Id_Sub_Kategori_Jenis,
                                Estimasi_Harga,
                                Flag_Harga_Pembelian_Akhir,
                                estimasi_hari
                            )
                            VALUES
                            (
                                '{KodePerusahaan}',
                                '{Txt_NoFaktur.Text.Trim()}',
                                '{LvLokasi}',
                                '{LvKdBrg}',
                                '{LvNmBrg}',
                                '{HilangkanTanda(LvQty)}',
                                '{LvSatuan}',
                                '{Format(CDate(LvTglDeli), "yyyy-MM-dd")}',
                                '{LvKet}',
                                {SelelctedIDCostCenter},
                                {SelelctedIDGedung},
                                '{LvIsStock}',
                                '0',
                                '{LvLink}',
                                {SelelctedIDsub},
                                '{HilangkanTanda(LvEst)}',
                                {flagest},
                                '{LvEstHari}'
                            )"
                        ExecuteTransMySQL(SQLMySQL)
                    End If
                Next

                SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail_Log(Kode_Perusahaan, No_Faktur, "
                SQL = SQL & "Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan, Link, "
                SQL = SQL & "No_Urut, Id_Sub_Kategori_Jenis, Estimasi_Harga, "
                SQL = SQL & "UserID_Ubah, Tanggal_Ubah, Jam_Ubah,Flag_Harga_Pembelian_Akhir, estimasi_hari) "
                SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah,Satuan, "
                SQL = SQL & "Tanggal_Delivery, keterangan, Link, No_Urut, Id_Sub_Kategori_Jenis, Estimasi_Harga, "
                SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "',Flag_Harga_Pembelian_Akhir, estimasi_hari "
                SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
                ExecuteTrans(SQL)
            Else

                SQL = "select status,flag_pr,flag_pra_release from N_EMI_Purchase_Requisition_Barang_Lain_Departement "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_faktur = '" & Txt_NoFaktur.Text & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If General_Class.CekNULL(dr("status")) = "Y" Then
                            dr.Close()
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf General_Class.CekNULL(dr("flag_pr")) = "Y" Then
                            dr.Close()
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi tidak bisa dilanjutkan, karena PR sudah selesai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf General_Class.CekNULL(dr("flag_pra_release")) = "Y" Then
                            dr.Close()
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("PR sudah pernah direlease!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        dr.Close()
                        CloseTransMySQL()
                        CloseConnMySQL()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Produksi Order tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select Flag_Sudah_PR, Jmlh_PR from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_faktur = '" & Txt_NoFaktur.Text & "' "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        If dr("Jmlh_PR") <> 0 Then
                            dr.Close()
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi tidak bisa dilanjutkan, sudah melakukan pr", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Loop
                End Using

                'SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail_Log(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan, Link, No_Urut, Id_Sub_Kategori_Jenis, Estimasi_Harga) "
                'SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah,Satuan, Tanggal_Delivery, keterangan, Link, No_Urut, Id_Sub_Kategori_Jenis, Estimasi_Harga "
                'SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
                'ExecuteTrans(SQL)

                'SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set keterangan = '" & TextBox2.Text.Trim & "', "
                'SQL = SQL & "tanggal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                'SQL = SQL & "jam = '" & Format(tgl_skg, "HH:mm:ss") & "',"
                'SQL = SQL & "userid = '" & UserID & "' where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text.Trim & "'"
                'ExecuteTrans(SQL)

                SQL = "select Kode_Perusahaan, status from N_EMI_Purchase_Requisition_Barang_Lain_Departement "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text.Trim & "' "
                SQL = SQL & "and userid = '" & UserID & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        If General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena no faktur sudah di batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        Dr.Close()

                        SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set keterangan = '" & TextBox2.Text.Trim & "' "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text.Trim & "' "
                        SQL = SQL & "and userid = '" & UserID & "' "
                        ExecuteTrans(SQL)

                    End If
                End Using


                SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Log(Kode_Perusahaan, No_Faktur, Lokasi, Status, Tanggal, Jam, UserId, Keterangan) "
                SQL = SQL & "select Kode_Perusahaan,No_Faktur,Lokasi,Status,'" & Format(tgl_skg, "yyyy-MM-dd") & "',Jam,'" & UserID & "',Keterangan from N_EMI_Purchase_Requisition_Barang_Lain_Departement "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

                SQL = "delete N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
                ExecuteTrans(SQL)

                If Not enablePraRelease Then
                    SQLMySQL = $"DELETE FROM purchase_requisition_barang_lain_departement_detail WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Faktur = '{Txt_NoFaktur.Text}'"
                    ExecuteTransMySQL(SQLMySQL)
                End If

                For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
                    Get_Isi_Listview(i)


                    Dim flagest As String = ""

                    If LvFlagEst = "T" Then
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

                    Dim SelelctedIDsub As String = ""
                    If LvId = "-" Then
                        SelelctedIDsub = "NULL"
                    Else
                        SelelctedIDsub = "'" & LvId & "'"
                    End If

                    Dim fileFix As String

                    If LvValueBtn = "" Then
                        fileFix = "NULL"
                    Else
                        Dim namaFile As String = IO.Path.GetFileName(LvValueBtn)

                        fileFix = "'" & namaFile & "_" & Format(tgl_skg, "dd-MM-yyyy-HH-ii-ss") & "_" & Txt_NoFaktur.Text & "'"
                    End If

                    SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Nama_Barang,Jumlah,Satuan,Tanggal_Delivery,keterangan, "
                    SQL = SQL & "Id_Cost_Center, ID_Gedung, Flag_Stock, Jmlh_PR, Link, Id_Sub_Kategori_Jenis, Estimasi_Harga, Flag_Harga_Pembelian_Akhir, estimasi_hari) values( "
                    SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "' ,"
                    SQL = SQL & "'" & LvLokasi & "', '" & LvKdBrg & "' ,'" & LvNmBrg & "' ,"
                    SQL = SQL & "'" & HilangkanTanda(LvQty) & "',"
                    SQL = SQL & "'" & LvSatuan & "', '" & Format(CDate(LvTglDeli), "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & LvKet & "', " & SelelctedIDCostCenter & ", " & SelelctedIDGedung & ", '" & LvIsStock & "', '0', '" & LvLink & "', " & SelelctedIDsub & ",  '" & HilangkanTanda(LvEst) & "' , " & flagest & " , '" & LvEstHari & "' "
                    SQL = SQL & " )"
                    ExecuteTrans(SQL)

                    If Not enablePraRelease Then
                        SQLMySQL = $"
                        INSERT INTO purchase_requisition_barang_lain_departement_detail
                        (
                            Kode_Perusahaan,
                            No_Faktur,
                            Kode_Stock_Owner,
                            Kode_Barang,
                            Nama_Barang,
                            Jumlah,
                            Satuan,
                            Tanggal_Delivery,
                            keterangan,
                            Id_Cost_Center,
                            ID_Gedung,
                            Flag_Stock,
                            Jmlh_PR,
                            Link,
                            Id_Sub_Kategori_Jenis,
                            Estimasi_Harga,
                            Flag_Harga_Pembelian_Akhir,
                            estimasi_hari
                        )
                        VALUES
                        (
                            '{KodePerusahaan}',
                            '{Txt_NoFaktur.Text.Trim()}',
                            '{LvLokasi}',
                            '{LvKdBrg}',
                            '{LvNmBrg}',
                            '{HilangkanTanda(LvQty)}',
                            '{LvSatuan}',
                            '{Format(CDate(LvTglDeli), "yyyy-MM-dd")}',
                            '{LvKet}',
                            {SelelctedIDCostCenter},
                            {SelelctedIDGedung},
                            '{LvIsStock}',
                            '0',
                            '{LvLink}',
                            {SelelctedIDsub},
                            '{HilangkanTanda(LvEst)}',
                            {flagest},
                            '{LvEstHari}'
                        )"
                        ExecuteTransMySQL(SQLMySQL)
                    End If
                Next

                SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail_Log(Kode_Perusahaan, No_Faktur, "
                SQL = SQL & "Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan, Link, "
                SQL = SQL & "No_Urut, Id_Sub_Kategori_Jenis, Estimasi_Harga, "
                SQL = SQL & "UserID_Ubah, Tanggal_Ubah, Jam_Ubah, Flag_Harga_Pembelian_Akhir, estimasi_hari) "
                SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah,Satuan, "
                SQL = SQL & "Tanggal_Delivery, keterangan, Link, No_Urut, Id_Sub_Kategori_Jenis, Estimasi_Harga, "
                SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', Flag_Harga_Pembelian_Akhir, estimasi_hari "
                SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

            End If

            SQL = "select no_faktur From  N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_faktur = '" & Txt_NoFaktur.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTransMySQL()
                    CloseConnMySQL()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Gagal menyimpan, barang harus di isi terlebih dahulu. refresh dan coba lagi")
                    Exit Sub
                End If
            End Using


            CmdMySQL.Transaction.Commit()
            Cmd.Transaction.Commit()

            CloseConnMySQL()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()
        Catch ex As Exception
            CloseTransMySQL()
            CloseConnMySQL()
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
        Dim aksesUnreleasePr As String = ""
        Try
            OpenConn()

            If CekButtonRole("Simpan_Purchase_Requisition_Departement") = "Y" Then
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

            If CekButtonRole("Release_Purchase_Requisition_Departement") = "Y" Then
                AksesReleasePR = "Y"
            End If

            OpenConn()

            If CekButtonRole("UnRelease_Purchase_Requisition_Departement") = "Y" Then
                aksesUnreleasePr = "Y"
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



            Dim ada_data As String = ""

            Dim Kategori_gudang As String = ""
            ada_data = "Y"
            '===================== Ambil gudang kode kategori gudang by user =====================
            SQL = "select kode_kategori_gudang from N_EMI_Purchase_Requisition_Barang_Lain_Departement "
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

            'If cmb <> Kategori_gudang Then
            '    CloseConn()
            '    ada_data = "T"
            '    MessageBox.Show("anda Tidak ada Akses ke PR Ini !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    kosong()
            '    Exit Sub
            'End If




            BtnPR_Release.Visible = True

            Dim flag_release_fix As String = ""
            SQL = "select no_faktur, lokasi ,tanggal, jam, userId, keterangan, flag_pra_release,status, No_Fak_Material_Requisition, kode_kategori_gudang from N_EMI_Purchase_Requisition_Barang_Lain_Departement where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
            SQL = SQL & "and status is null"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

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
                            '    'Button2.Enabled = False
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

                            '    'Button2.Enabled = True
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

                                TextBox2.ReadOnly = False
                            End If
                            'akhir stenly 15-12
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
                SQL = "select a.kode_perusahaan,a.estimasi_hari, a.kode_stock_owner, a.kode_barang, a.nama_barang, a.jumlah, a.satuan, a.tanggal_delivery, a.keterangan, a.Link, a.Estimasi_Harga, "

                SQL = SQL & "ISNULL((select sum(x.Nilai_PPIC) from EMI_Transaksi_Material_Requsition_detail x "
                SQL = SQL & "where x.Kode_Perusahaan = a.Kode_Perusahaan and x.Kode_Stock_Owner = a.Kode_Stock_Owner and x.Kode_Barang = a.Kode_Barang "
                SQL = SQL & "and x.No_Faktur = '" & Txt_Faktur_MaterialReq.Text & "' "
                SQL = SQL & "), 0) as Nilai_PPIC, "

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

                SQL = SQL & "a.ID_Cost_Center, a.Id_Gedung, a.Flag_Stock, "

                SQL = SQL & "ISNULL(CAST(a.Id_Sub_Kategori_Jenis AS VARCHAR(20)), '-') as Id_Sub_Kategori_Jenis, "
                SQL = SQL & "isnull((select z.Keterangan + ' - ' + x.Keterangan as Sub_Kategori_Jenis from N_EMI_Master_Sub_Kategori_Jenis z, N_EMI_Master_Kategori_Jenis x "
                SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Id_Kategori_Jenis = x.Id_Kategori_Jenis and z.Id_Sub_Kategori_Jenis = a.Id_Sub_Kategori_Jenis "
                SQL = SQL & "),'-') as Sub_Kategori_Jenis,flag_Harga_Pembelian_Akhir  "

                SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a where "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.no_faktur = '" & Txt_NoFaktur.Text & "'  "

                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                Dgv_DataBarang.Rows.Add(1)
                                Dgv_DataBarang.Rows(i).Cells(CellLokasi).Value = .Rows(i).Item("kode_stock_owner")
                                Dgv_DataBarang.Rows(i).Cells(CellKdBrg).Value = .Rows(i).Item("kode_barang")
                                Dgv_DataBarang.Rows(i).Cells(CellNmBrg).Value = .Rows(i).Item("nama_barang")
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

                                Dgv_DataBarang.Rows(i).Cells(CellKategori).Value = .Rows(i).Item("Sub_Kategori_Jenis")
                                Dgv_DataBarang.Rows(i).Cells(CellId).Value = .Rows(i).Item("Id_Sub_Kategori_Jenis")
                                Dgv_DataBarang.Rows(i).Cells(CellEst).Value = Format(.Rows(i).Item("Estimasi_Harga"), "N2")

                                If General_Class.CekNULL(.Rows(i).Item("flag_harga_pembelian_akhir")) = "" Then

                                    Dgv_DataBarang.Rows(i).Cells(cellFlagEst).Value = "T"
                                    Dgv_DataBarang.Rows(i).Cells(CellEst).ReadOnly = False
                                Else

                                    Dgv_DataBarang.Rows(i).Cells(cellFlagEst).Value = "Y"
                                    Dgv_DataBarang.Rows(i).Cells(CellEst).ReadOnly = True

                                End If

                                Dgv_DataBarang.Rows(i).Cells(cellEstHari).Value = .Rows(i).Item("estimasi_hari")

                            Next


                            If publicFlagRelease = "T" Then
                                Dgv_DataBarang.Rows.Add(1)
                            End If

                        End If
                    End With
                End Using
            End If

            Btn_Simpan.Tag = "&Update"

            SQL = $"SELECT 1 FROM N_EMI_Master_Kategori_Gudang_Barang_Lain WHERE Kode_Kategori_Gudang = '{Cmb_Kategori_Gudang.Text.Trim}' AND Flag_Kirim_Web IS NULL"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    enablePraRelease = True

                    If AksesReleasePR = "Y" Then
                        BtnPR_Release.Enabled = True
                    Else
                        BtnPR_Release.Enabled = False
                    End If

                    If AksesUnRelease = "Y" Then
                        Btn_Unrelease.Enabled = True
                    Else
                        Btn_Unrelease.Enabled = False
                    End If
                Else
                    Dr.Close()
                    enablePraRelease = False
                    BtnPR_Release.Enabled = False
                    Btn_Unrelease.Enabled = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles BtnPR_Release.Click
        get_jam()

        Try
            OpenConn()

            If CekButtonRole("Release_Purchase_Requisition_Departement") = "T" Then
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            SQL = "select no_faktur From  N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_faktur = '" & Txt_NoFaktur.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Gagal Release, barang harus di isi terlebih dahulu")
                    Exit Sub
                End If
            End Using

            no_Faktur_Sementara = String.Empty

            SQL = "select status, flag_pr, flag_release, flag_pra_release from N_EMI_Purchase_Requisition_Barang_Lain_Departement "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_faktur = '" & Txt_NoFaktur.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("status")) = "Y" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("flag_pr")) = "Y" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Transaksi tidak bisa dilanjutkan, karena PR sudah selesai di PO", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("flag_pra_release")) = "Y" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Transaksi tidak bisa dilanjutkan, karena sudah di pra release", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("flag_release")) = "Y" Then
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

            SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set flag_pra_release = 'Y', "
            SQL = SQL & "Tanggal_Pra_Release = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "Jam_Pra_Release = '" & Format(tgl_skg, "HH:mm:ss") & "',"
            SQL = SQL & "User_Pra_Release = '" & UserID & "' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
            ExecuteTrans(SQL)

            SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Log_Release("
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
        Try
            OpenConn()

            SQL = "select a.No_Faktur from N_EMI_View_Transaksi_Purchase_Requisition_Barang_Lain_Departement a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & no_Faktur_Sementara & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New N_EMI_CR_Faktur_Purchase_Requisition_Barang_Lain_Departement
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition Barang Lain Departement"
                            CrDoc.RecordSelectionFormula = " {N_EMI_View_Transaksi_Purchase_Requisition_Barang_Lain_Departement.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Transaksi_Purchase_Requisition_Barang_Lain_Departement.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"

                            .Text = "Laporan Faktur Purchase Requisition Barang Lain Departement"
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

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.filter_tambahan = "and c.Kode_Stock_Owner = '" & cmb_lokasi.Text & "'"
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.SO_Kategori_Gudang_Pilih = arrKategoriGudang(Cmb_Kategori_Gudang.SelectedIndex)
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.faktur_MR = Txt_Faktur_MaterialReq.Text
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_KodeBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_Satuan.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_NamaBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_NamaBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_KodeBarang.Visible = True

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_PR.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Order.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Sisa.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_PR.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Order.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Sisa.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.ShowDialog()

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
                        Dgv_DataBarang.Rows.RemoveAt(currentRow)

                        If Dgv_DataBarang.Rows.Count <= 1 Then
                            Cmb_Kategori_Gudang.Enabled = True
                        Else
                            Cmb_Kategori_Gudang.Enabled = False
                        End If

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

                N_EMI_SD_Ubah_Tanggal_PR_Barang_Lain_Departement.DTP_Delivery.Value = Dgv_DataBarang.Rows(currentRow).Cells(currentCell).Value
                N_EMI_SD_Ubah_Tanggal_PR_Barang_Lain_Departement.rowDgv = currentRow
                N_EMI_SD_Ubah_Tanggal_PR_Barang_Lain_Departement.cellDgv = currentCell

                N_EMI_SD_Ubah_Tanggal_PR_Barang_Lain_Departement.ShowDialog()

            End If
        End If

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

    Private Sub Dgv_DataBarang_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataBarang.CellContentClick
        If Dgv_DataBarang.Columns(e.ColumnIndex).Name = "btnView" Then

            ' pastikan bukan header
            If e.RowIndex < 0 Then Exit Sub

            If Dgv_DataBarang.Rows.Count - 1 = e.RowIndex Then Exit Sub


            Dim row As DataGridViewRow = Dgv_DataBarang.Rows(e.RowIndex)


            ' ambil value dari kolom mana pun
            ' Dim id As String = row.Cells(0).Value.ToString()
            Dim kode_barang As String = row.Cells(1).Value.ToString()
            'Dim barang_baru As String = row.Cells(21).Value.ToString
            Dim nama As String = row.Cells(2).Value.ToString

            If kode_barang <> "-" Then
                N_EMI_Show_Katalog_Barang.kode_barang = kode_barang
                N_EMI_Show_Katalog_Barang.ShowDialog()

                'If barang_baru = "" And kode_barang <> "-" Then

                'Else
                '    N_EMI_Show_Katalog_Barang_Baru.path = barang_baru
                '    N_EMI_Show_Katalog_Barang_Baru.nama = nama
                '    N_EMI_Show_Katalog_Barang_Baru.ShowDialog()
                'End If
            Else
                MessageBox.Show("Belum ada gambar yang bisa ditampilkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


        End If
    End Sub

    Private Sub Cmb_Kategori_Gudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kategori_Gudang.SelectedIndexChanged
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = $"SELECT 1 FROM N_EMI_Master_Kategori_Gudang_Barang_Lain WHERE Kode_Kategori_Gudang = '{Cmb_Kategori_Gudang.Text.Trim}' AND Flag_Kirim_Web IS NULL"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    enablePraRelease = True

                    If AksesReleasePR = "Y" Then
                        BtnPR_Release.Enabled = True
                    Else
                        BtnPR_Release.Enabled = False
                    End If

                    If AksesUnRelease = "Y" Then
                        Btn_Unrelease.Enabled = True
                    Else
                        Btn_Unrelease.Enabled = False
                    End If
                Else
                    Dr.Close()
                    enablePraRelease = False
                    BtnPR_Release.Enabled = False
                    Btn_Unrelease.Enabled = False
                End If
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
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



    Private Sub Btn_Unrelease_Click(sender As Object, e As EventArgs) Handles Btn_Unrelease.Click
        If Txt_NoFaktur.Text.Trim.Length = 0 Then Exit Sub
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("UnRelease_Purchase_Requisition_Departement") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Unrelease PR Warehouse", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin akan Unrelease Purhcase Requisition ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then Exit Sub

            SQL = "select Status from N_EMI_Purchase_Requisition_Barang_Lain_Departement where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Txt_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Purhcase Requisition Departement sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Requisition Departement tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur from N_EMI_Purchase_Requisition_Barang_Lain_Departement a,"
            SQL = SQL & "N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b, EMI_Purchase_Requisition_Barang_Lain_Detail c,"
            SQL = SQL & "EMI_Purchase_Requisition_Barang_Lain d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur "
            SQL = SQL & "and b.No_Urut = c.Urut_Departement and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and d.Status is null and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Requisition departement tidak bisa diunrelease, karena sudah masuk tahap Purhcase Requisition warehouse!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur from N_EMI_Purchase_Requisition_Barang_Lain_Departement a, "
            SQL = SQL & "N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b, N_EMI_Keep_Stock_Barang_Lain_Departement c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and b.No_Urut = c.Urut_Departement and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.Status is null and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Requisition departement tidak bisa diunrelease, karena sudah masuk tahap Keep stock!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Path_File,Container_File from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Attachment "
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





            SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set Flag_pra_Release = NULL, "
            SQL = SQL & "tanggal_pra_release = NULL, "
            SQL = SQL & "jam_pra_release = NULL, "
            SQL = SQL & "user_pra_release = NULL, "
            SQL = SQL & "tanggal_release = NULL, "
            SQL = SQL & "jam_release = NULL, "
            SQL = SQL & "user_release = NULL, flag_release = null "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text & "' "
            ExecuteTrans(SQL)

            'reza tambahan pra release
            SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Attachment set  "
            SQL = SQL & "status = 'Y' where Kode_Perusahaan = '001' and No_Faktur = '" & Txt_NoFaktur.Text & "' "
            ExecuteTrans(SQL)

            SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Log_Release("
            SQL = SQL & "Kode_Perusahaan, No_Faktur, User_Id, Tanggal, Jam, Keterangan) values("
            SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & UserID & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'UNPRARELEASE') "
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

    Private Sub Txt_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub Txt_NoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoFaktur.KeyPress
        If e.KeyChar = Chr(13) Then Dgv_DataBarang.Focus()
    End Sub

End Class
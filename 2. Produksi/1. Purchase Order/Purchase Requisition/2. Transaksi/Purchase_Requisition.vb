Public Class Purchase_Requisition

    Dim arrCari, arrKd_biaya, arrKeterangan As New ArrayList

    Dim arrInisialFaktur As New ArrayList

    Dim Jenis = "Purchase_Requisition"

    Dim no_Faktur_Sementara As String = ""

    Dim publicFlagRelease = "T"

    Private Sub Master_Biaya_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub get_no_faktur()
        Txt_NoFaktur.Text = fPurchaseRequisition & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Purchase_Requisition", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fPurchaseRequisition) + 4 & ")", fPurchaseRequisition & Format(tgl_skg, "MMyy"))
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
    Dim LvEstTiba As String

    Dim CellLokasi As Integer = 0
    Dim CellKdBrg As Integer = 1
    Dim CellNmBrg As Integer = 2
    Dim CellQty As Integer = 3
    Dim CellSatuan As Integer = 4
    Dim CellTglDeli As Integer = 5
    Dim CellKet As Integer = 6
    Dim CellSisa As Integer = 7
    Dim CellQtyByForecast As Integer = 8
    Dim CellEstTiba As Integer = 9

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
        LvEstTiba = Dgv_DataBarang.Rows(No_Index).Cells(CellEstTiba).Value

    End Sub

    Private Sub Master_Biaya_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Lbl_Judul.Text = "Purchase Requisition"

            Dgv_DataBarang.Columns(CellEstTiba).DisplayIndex = CellSatuan

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
        Dgv_DataBarang.Enabled = True
        publicFlagRelease = "T"
        get_jam()
        Try
            OpenConn()

            arrInisialFaktur.Clear() : cmb_lokasi.Items.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmb_lokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
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

        Lbl_IDBiaya.Text = ""
        Txt_Kd.Text = ""
        Txt_Keterangan.Text = ""
        Cmb_Kolom.SelectedIndex = -1
        Txt_Value.Text = ""
        TextBox2.Text = ""
        Txt_Faktur_MaterialReq.Text = ""
        TextBox2.ReadOnly = False

        BtnPR_Simpan.Enabled = True
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

        Btn_Unrelease.Visible = False
        Btn_Unrelease.Enabled = False

        Dim AksesSimpanPR As String = ""
        Dim AksesReleasePR As String = ""
        Try
            OpenConn()

            If CekButtonRole("Simpan_Purchase_Requisition") = "Y" Then
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

            If CekButtonRole("Release_Purchase_Requisition") = "Y" Then
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
        SD_Data_PR.ShowDialog()
    End Sub

    Private Sub BtnFormulator_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPR_Simpan.Click
        If Dgv_DataBarang.Rows.Count = 0 Then
            MessageBox.Show("Tidak ada Data yang bisa di simpan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        get_jam()

        Try

            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Simpan_Purchase_Requisition") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            If Btn_Simpan.Tag = "&Simpan" Then
                get_no_faktur()

                SQL = "insert into EMI_Purchase_Requisition(Kode_Perusahaan,No_Faktur,Lokasi,Tanggal,Jam,UserId,Keterangan) values("
                SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "', '" & cmb_lokasi.Text & "', "
                SQL = SQL & "'" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', '" & Format(DtpFormulator_Tanggal.Value, "HH:MM:ss") & "',"
                SQL = SQL & "'" & UserID & "', '" & TextBox2.Text.Trim & "' )"
                ExecuteTrans(SQL)

                For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
                    Get_Isi_Listview(i)
                    SQL = "insert into emi_purchase_requisition_detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Tanggal_Delivery,keterangan ) values("
                    SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "' ,"
                    SQL = SQL & "'" & LvLokasi & "', '" & LvKdBrg & "' ,"
                    SQL = SQL & "'" & HilangkanTanda(LvQty) & "',"
                    SQL = SQL & "'" & LvSatuan & "', '" & Format(CDate(LvTglDeli), "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & LvKet & "' )"
                    ExecuteTrans(SQL)
                Next
            Else

                SQL = "select status,flag_po,flag_release from EMI_Purchase_Requisition "
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
                        ElseIf General_Class.CekNULL(dr("flag_release")) = "Y" Then
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

                SQL = "insert into EMI_Purchase_Requisition_log(Kode_Perusahaan, No_Faktur, Lokasi, Status, Tanggal, Jam, UserId, Keterangan) "
                SQL = SQL & "select Kode_Perusahaan,No_Faktur,Lokasi,Status,Tanggal,Jam,UserId,Keterangan from EMI_Purchase_Requisition "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

                SQL = "insert into EMI_Purchase_Requisition_Detail_log(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan) "
                SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah,Satuan, Tanggal_Delivery, keterangan "
                SQL = SQL & "from EMI_Purchase_Requisition_Detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
                ExecuteTrans(SQL)

                SQL = "update EMI_Purchase_Requisition set keterangan = '" & TextBox2.Text.Trim & "', "
                SQL = SQL & "tanggal = '" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "jam = '" & Format(DtpFormulator_Tanggal.Value, "HH:MM:ss") & "',"
                SQL = SQL & "userid = '" & UserID & "' where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)

                SQL = "delete EMI_Purchase_Requisition_Detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
                ExecuteTrans(SQL)

                For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
                    Get_Isi_Listview(i)
                    SQL = "insert into emi_purchase_requisition_detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang, "
                    SQL = SQL & "Jumlah,Satuan,Tanggal_Delivery,keterangan, Estimasi) values("
                    SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "' ,"
                    SQL = SQL & "'" & LvLokasi & "', '" & LvKdBrg & "' ,"
                    SQL = SQL & "'" & HilangkanTanda(LvQty) & "',"
                    SQL = SQL & "'" & LvSatuan & "', '" & Format(CDate(LvTglDeli), "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & LvKet & "', '" & LvEstTiba & "') "
                    ExecuteTrans(SQL)
                Next

            End If

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

            If CekButtonRole("Simpan_Purchase_Requisition") = "Y" Then
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

            If CekButtonRole("Release_Purchase_Requisition") = "Y" Then
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
            Dim flag_release_fix As String = ""
            SQL = "select no_faktur, lokasi ,tanggal, jam, userId, keterangan, flag_release,status, No_Fak_Material_Requisition from EMI_Purchase_Requisition where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "' "
            SQL = SQL & "and status is null"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            ada_data = "Y"

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

                            If General_Class.CekNULL(.Rows(i).Item("flag_release")) = "Y" Or General_Class.CekNULL(.Rows(i).Item("flag_release")) = "P" Then
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

                SQL = "select a.kode_perusahaan, a.kode_stock_owner, a.kode_barang, b.nama, a.jumlah, a.satuan, a.tanggal_delivery, a.keterangan, "

                SQL = SQL & "ISNULL((select sum(x.Nilai_PPIC) from EMI_Transaksi_Material_Requsition_detail x "
                SQL = SQL & "where x.Kode_Perusahaan = a.Kode_Perusahaan and x.Kode_Stock_Owner = a.Kode_Stock_Owner and x.Kode_Barang = a.Kode_Barang "
                SQL = SQL & "and x.No_Faktur = '" & Txt_Faktur_MaterialReq.Text & "' "
                SQL = SQL & "), 0) as Nilai_PPIC, "

                'SQL = SQL & "ISNULL(( select (SUM(z.jumlah) - a.jumlah) from EMI_Purchase_Requisition_Detail z where z.Kode_Perusahaan = a.Kode_Perusahaan "
                'SQL = SQL & "and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang"
                'SQL = SQL & "), 0) as jumlahPR "

                SQL = SQL & "isnull((select "

                If Txt_Faktur_MaterialReq.Visible = True And Not Txt_Faktur_MaterialReq.Text.Trim.Length = 0 Then
                    SQL = SQL & "sum(y.jumlah) - a.jumlah "
                Else
                    SQL = SQL & "sum(y.jumlah) "
                End If

                SQL = SQL & "from EMI_Purchase_Requisition x, EMI_Purchase_Requisition_Detail y "
                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and y.Kode_Perusahaan = a.Kode_Perusahaan "
                SQL = SQL & "and y.Kode_Stock_Owner = a.Kode_Stock_Owner "
                SQL = SQL & "and y.Kode_Barang = a.Kode_Barang and x.Status is null "
                SQL = SQL & "and x.no_fak_material_requisition  = '" & Txt_Faktur_MaterialReq.Text & "' "
                SQL = SQL & "), 0) as jumlahPR, isnull(a.estimasi,0) as estimasi "

                SQL = SQL & "from EMI_Purchase_Requisition_Detail a, barang b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner "
                SQL = SQL & "and a.kode_barang = b.kode_barang "
                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.no_faktur = '" & Txt_NoFaktur.Text & "'  "
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

                                Dgv_DataBarang.Rows(i).Cells(CellEstTiba).Value = .Rows(i).Item("estimasi")
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles BtnPR_Release.Click
        Try
            OpenConn()

            If CekButtonRole("Release_Purchase_Requisition") = "T" Then
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            no_Faktur_Sementara = String.Empty

            SQL = "select status, flag_po, flag_release from EMI_Purchase_Requisition "
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


            Dim Msg As String = ""
            Dim TglLewat As Boolean = False

            For i As Integer = 0 To Dgv_DataBarang.Rows.Count - 2
                Get_Isi_Listview(i)

                Dim tgldeliv As DateTime = LvTglDeli
                Dim esttiba As Integer = LvEstTiba

                Dim Diff As Integer = DateDiff(DateInterval.Day, tgl_skg, tgldeliv)


                If Diff < esttiba Then
                    TglLewat = True

                    Msg += " - " & LvNmBrg & " | Tgl Delivery: " & LvTglDeli & " | Est Butuh : " & Diff & " Hari " & vbCrLf

                    'Dim Msg As String = MessageBox.Show("Tanggal Dibutuhkan Kurang dari Estimasi Tiba,  Tetap Lanjutkan ? ", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    'If Msg = vbNo Then
                    '    DateTimePicker1.Value = DateAdd(DateInterval.Day, Val(TxtTiba.Text) + 1, tgl_skg)
                    'End If
                End If


            Next

            If TglLewat = True Then

                Dim Msg2 As String = MessageBox.Show("Barang Melewati Estimasi :  " & vbCrLf & Msg & vbCrLf & " Apakah Tetap Dilanjutkan? PR Perlu di Validasi Procurement. ", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If Msg2 = vbNo Then
                    CloseConn()
                    Exit Sub
                End If

                SQL = "update EMI_Purchase_Requisition set flag_release = 'P', "
                SQL = SQL & "tanggal_release = '" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "jam_release = '" & Format(DtpFormulator_Tanggal.Value, "HH:MM:ss") & "',"
                SQL = SQL & "user_release = '" & UserID & "', "
                SQL = SQL & "flag_Val_Proc = 'P' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
                ExecuteTrans(SQL)

            Else
                SQL = "update EMI_Purchase_Requisition set flag_release = 'Y', "
                SQL = SQL & "tanggal_release = '" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "jam_release = '" & Format(DtpFormulator_Tanggal.Value, "HH:MM:ss") & "',"
                SQL = SQL & "user_release = '" & UserID & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFaktur.Text & "'"
                ExecuteTrans(SQL)

            End If


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

            SQL = "select a.No_Faktur from EMI_Purchase_Requisition a, EMI_Purchase_Requisition_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & no_Faktur_Sementara & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New Faktur_Purchase_Requisition
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition"
                            CrDoc.RecordSelectionFormula = " {EMI_Purchase_Requisition.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Purchase_Requisition.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"

                            .Text = "Laporan Faktur Purchase Requisition"
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

                SD_Tambah_PR.filter_tambahan = "and c.Kode_Stock_Owner = '" & cmb_lokasi.Text & "'"
                'SD_Tambah_PR.asal = cmb_lokasi.Text
                SD_Tambah_PR.asal = Jenis
                SD_Tambah_PR.asal = Jenis
                SD_Tambah_PR.faktur_MR = Txt_Faktur_MaterialReq.Text
                SD_Tambah_PR.TxtPilihBarang_KodeBarang.Visible = True
                SD_Tambah_PR.TxtPilihBarang_Satuan.Visible = True
                SD_Tambah_PR.TxtPilihBarang_NamaBarang.Visible = True
                SD_Tambah_PR.LblPilihBarang_NamaBarang.Visible = True
                SD_Tambah_PR.LblPilihBarang_KodeBarang.Visible = True

                SD_Tambah_PR.Lbl_PR.Visible = False
                SD_Tambah_PR.Lbl_Order.Visible = False
                SD_Tambah_PR.Lbl_Sisa.Visible = False
                SD_Tambah_PR.Txt_PR.Visible = False
                SD_Tambah_PR.Txt_Order.Visible = False
                SD_Tambah_PR.Txt_Sisa.Visible = False
                SD_Tambah_PR.ShowDialog()

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

                SD_Ubah_Tanggal_PR.DTP_Delivery.Value = Dgv_DataBarang.Rows(currentRow).Cells(currentCell).Value
                SD_Ubah_Tanggal_PR.rowDgv = currentRow
                SD_Ubah_Tanggal_PR.cellDgv = currentCell
                SD_Ubah_Tanggal_PR.EstTiba = Dgv_DataBarang.Rows(currentRow).Cells(CellEstTiba).Value

                SD_Ubah_Tanggal_PR.ShowDialog()

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
    End Sub


    Private Sub Btn_Unrelease_Click(sender As Object, e As EventArgs) Handles Btn_Unrelease.Click

        If Txt_NoFaktur.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("UnRelease_Purchase_Requisition") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Unrelease PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin akan Unrelease Purhcase Requisition ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then Exit Sub

            SQL = "select Status from EMI_Purchase_Requisition where Kode_Perusahaan = '" & KodePerusahaan & "' and "
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

            SQL = "select a.No_Faktur from EMI_Purchase_Requisition a,"
            SQL = SQL & "EMI_Purchase_Requisition_Detail b,EMI_Pembelian_PO_Det_Induk c,EMI_Pembelian_PO_Induk d "
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


            SQL = "update EMI_Purchase_Requisition set Flag_Release = NULL, "
            SQL = SQL & "tanggal_release = NULL, "
            SQL = SQL & "jam_release = NULL, "
            SQL = SQL & "user_release = NULL "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text & "' "
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

    Private Sub Dgv_DataBarang_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataBarang.CellContentClick

    End Sub

    Private Sub Txt_NoFaktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_NoFaktur.TextChanged

    End Sub

    Private Sub Txt_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub Txt_NoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoFaktur.KeyPress
        If e.KeyChar = Chr(13) Then Dgv_DataBarang.Focus()
    End Sub

End Class
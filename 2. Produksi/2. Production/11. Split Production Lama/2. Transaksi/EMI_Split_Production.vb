
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Xml


Public Class EMI_Split_Production
    Dim arrIdLine, arrInisialFaktur, arrInisialRouting As New ArrayList
    Dim Ket_SO, Ket_Id_Routing, no_fak As String

    Dim LvA_No_PO As String
    Dim LvA_Kode_SO As String
    Dim LvA_Kode_Brg As String
    Dim LvA_Nama_Brg As String
    Dim LvA_Jumlah As String
    Dim LvA_Jumlah_Splt As String
    Dim LvA_Jumlah_Sisa As String
    Dim LvA_Satuan As String
    Dim LvA_Routing As String
    Dim LvA_Routing_Id As String

    Dim LvB_No_PO As String
    Dim LvB_Kode_SO As String
    Dim LvB_Kode_Brg As String
    Dim LvB_Nama_Brg As String
    Dim LvB_Jumlah As String
    Dim LvB_Satuan As String
    Dim LvB_Routing As String
    Dim LvB_Routing_Id As String

    Private Sub get_isi_listview_Atas(ByVal index As Integer)
        LvA_No_PO = ListView1.Items(index).SubItems(0).Text
        LvA_Kode_SO = ListView1.Items(index).SubItems(1).Text
        LvA_Kode_Brg = ListView1.Items(index).SubItems(2).Text
        LvA_Nama_Brg = ListView1.Items(index).SubItems(3).Text
        LvA_Jumlah = ListView1.Items(index).SubItems(4).Text
        LvA_Jumlah_Splt = ListView1.Items(index).SubItems(5).Text
        LvA_Jumlah_Sisa = ListView1.Items(index).SubItems(6).Text
        LvA_Satuan = ListView1.Items(index).SubItems(7).Text
        LvA_Routing = ListView1.Items(index).SubItems(8).Text
        LvA_Routing_Id = ListView1.Items(index).SubItems(9).Text
    End Sub

    Private Sub get_isi_listview_Bawah(ByVal index As Integer)
        LvB_No_PO = ListView2.Items(index).SubItems(0).Text
        LvB_Kode_SO = ListView2.Items(index).SubItems(1).Text
        LvB_Kode_Brg = ListView2.Items(index).SubItems(2).Text
        LvB_Nama_Brg = ListView2.Items(index).SubItems(3).Text
        LvB_Jumlah = ListView2.Items(index).SubItems(4).Text
        LvB_Satuan = ListView2.Items(index).SubItems(5).Text
        LvB_Routing = ListView2.Items(index).SubItems(6).Text
        LvB_Routing_Id = ListView2.Items(index).SubItems(7).Text
    End Sub

    Private Sub get_no_faktur()
        Dim fSPO As String = "SPO"
        no_fak = fSPO & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Split_Production_Order", "no_transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_transaksi, 1, " & Len(fSPO) + 4 & ")", fSPO & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub EMI_Penentu_Production_Order_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Rencana_Produksi")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Label1.Text = "Transaksi - Split Production Order"
        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

        ListView1.Columns.Add("No PO", 150, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode SO", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        ListView1.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Jumlah PO", 150, HorizontalAlignment.Right)
        ListView1.Columns.Add("Jumlah Sudah Split", 150, HorizontalAlignment.Right)
        ListView1.Columns.Add("Jumlah Belum Split", 140, HorizontalAlignment.Right)
        ListView1.Columns.Add("Satuan", 140, HorizontalAlignment.Left)
        ListView1.Columns.Add("Routing", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("id_routing", 0, HorizontalAlignment.Left)
        'ListView1.View = View.Details

        ListView2.Columns.Add("No Production Order", 150, HorizontalAlignment.Left)
        ListView2.Columns.Add("Kode SO", 0, HorizontalAlignment.Left)
        ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 450, HorizontalAlignment.Left)
        ListView2.Columns.Add(Base_Language.Lang_Global_Jumlah, 140, HorizontalAlignment.Right)
        ListView2.Columns.Add(Base_Language.Lang_Global_Satuan, 140, HorizontalAlignment.Center)
        ListView2.Columns.Add("Routing", 230, HorizontalAlignment.Center)
        ListView1.Columns.Add("id_routing", 0, HorizontalAlignment.Left)
        'ListView2.View = View.Details

        'get_jam()
        kosong()

    End Sub

    Private Sub kosong()


        Try
            OpenConn()

            'get_no_faktur()
            arrInisialFaktur.Clear() : CmbLokasi.Items.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using
            CmbLokasi.Text = Lokasi
            TxtCatatan.Text = ""

            ListView1.Items.Clear() : ListView2.Items.Clear()
            SQL = "select a.No_Faktur,a.Kode_Stock_Owner,a.Kode_Barang,c.Nama,a.Jumlah,a.Satuan,d.Keterangan,a.Id_Routing, "
            SQL = SQL & "ISNULL((select sum(z.Jumlah) from Emi_Split_Production_Order z where z.No_PO = a.No_Faktur "
            SQL = SQL & "),0) as Jml_Sdh_Split "
            SQL = SQL & "from EMI_Order_Produksi a,Barang c,EMI_Master_Routing d where "
            SQL = SQL & "a.Status is null and a.Selesai is null and a.Flag_Release = 'Y' and a.Flag_Selesai_Produksi is null "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Id_Routing = d.Id_Routing and a.Flag_Selesai_Split is null order by Tanggal"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lv As New ListViewItem
                    lv = ListView1.Items.Add(dr("No_Faktur"))
                    lv.SubItems.Add(dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(dr("Kode_Barang"))
                    lv.SubItems.Add(dr("Nama"))
                    lv.SubItems.Add(Format(dr("Jumlah"), "N2"))
                    lv.SubItems.Add(Format(dr("Jml_Sdh_Split"), "N2"))
                    Dim Sisa As Double = dr("Jumlah") - dr("Jml_Sdh_Split")
                    lv.SubItems.Add(Format(Sisa, "N2"))
                    lv.SubItems.Add(dr("Satuan"))
                    lv.SubItems.Add(dr("Keterangan"))
                    lv.SubItems.Add(dr("Id_Routing"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong_sebagian()
    End Sub

    Private Sub kosong_sebagian()
        TextBox1.Text = ""
        txtKodeBarang.Text = ""
        txtNmBarang.Text = ""
        txtRouting.Text = ""
        txtJumlah.Text = ""
        txtSatuan.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        kosong()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        kosong_sebagian()
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click

        If txtKodeBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang belum dipilih!", Judul)
        End If

        get_isi_listview_Atas(ListView1.FocusedItem.Index)

        Dim sisa As Double = 0
        If ListView2.Items.Count <> 0 Then
            For a As Integer = 0 To ListView2.Items.Count - 1
                get_isi_listview_Bawah(a)
                If LvA_No_PO = LvB_No_PO And LvA_Kode_SO = LvB_Kode_SO And LvA_Kode_Brg = LvB_Kode_Brg Then
                    sisa = sisa + Val(HilangkanTanda(LvB_Jumlah))
                End If
            Next
        End If
        sisa = sisa + Val(txtJumlah.Text)

        If sisa > Val(HilangkanTanda(LvA_Jumlah_Sisa)) Then
            MessageBox.Show("Jumlah yang diinput tidak boleh lebih besar dari jumlah production order", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            Dim lv As New ListViewItem
            lv = ListView2.Items.Add(TextBox1.Text)
            lv.SubItems.Add(Ket_SO)
            lv.SubItems.Add(txtKodeBarang.Text)
            lv.SubItems.Add(txtNmBarang.Text)
            lv.SubItems.Add(txtJumlah.Text)
            lv.SubItems.Add(txtSatuan.Text)
            lv.SubItems.Add(txtRouting.Text)
            lv.SubItems.Add(Ket_Id_Routing)
        End If
        kosong_sebagian()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If ListView2.Items.Count = 0 Then
            MessageBox.Show("Production order belum dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TxtCatatan.Text.Trim.Length = 0 Then
            MessageBox.Show("Catatan belum dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction




            For a As Integer = 0 To ListView2.Items.Count - 1
                get_isi_listview_Bawah(a)
                get_no_faktur()

                SQL = "INSERT INTO Emi_Split_Production_Order(Kode_Perusahaan,No_Transaksi,No_PO,Lokasi,Tanggal,Jam,UserID,Kode_Stock_Owner,"
                SQL = SQL & "Kode_Barang,Jumlah,Satuan,Catatan) VALUES('" & KodePerusahaan & "','" & no_fak & "','" & LvB_No_PO & "',"
                SQL = SQL & "'" & CmbLokasi.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "',"
                SQL = SQL & "'" & UserID & "','" & LvB_Kode_SO & "','" & LvB_Kode_Brg & "','" & HilangkanTanda(LvB_Jumlah) & "',"
                SQL = SQL & "'" & LvB_Satuan & "','" & TxtCatatan.Text & "')"
                ExecuteTrans(SQL)

                SQL = "select a.No_Faktur,a.Kode_Stock_Owner,a.Kode_Barang,c.Nama,a.Jumlah,a.Satuan,d.Keterangan,a.Id_Routing, "
                SQL = SQL & "ISNULL((select sum(z.Jumlah) from Emi_Split_Production_Order z where z.No_PO = a.No_Faktur "
                SQL = SQL & "),0) as Jml_Sdh_Split "
                SQL = SQL & "from EMI_Order_Produksi a,Barang c,EMI_Master_Routing d where "
                SQL = SQL & "a.Status is null and a.Selesai is null and Flag_Release = 'Y' "
                SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Id_Routing = d.Id_Routing and a.Flag_Selesai_Split is null "
                SQL = SQL & "and a.Flag_Selesai_Produksi is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & LvB_No_PO & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If dr("Jumlah") = dr("Jml_Sdh_Split") Then
                            dr.Close()
                            SQL = "update EMI_Order_Produksi set Flag_Selesai_Split = 'Y' where "
                            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & LvB_No_PO & "'"
                            ExecuteTrans(SQL)
                        End If
                    End If
                End Using


                'penentu bahan baku dan packaging


                Dim satuan_akhir_init_barang As String = ""
                Dim totalSerapan As Double = 0
                Dim nilai_production_order As Double = 0
                Dim nilaiPersentase As Double = 0

                Dim kd_barangINq As String = ""
                SQL = "select top(1) Kode_Barang_inq from barang "
                SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Barang ='" & LvB_Kode_Brg & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        kd_barangINq = dr("Kode_Barang_inq")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End Using

                SQL = "select Satuan_Berat From Init "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        satuan_akhir_init_barang = Dr("satuan_berat")
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Data tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvB_Kode_Brg & "',"
                SQL = SQL & "'" & LvB_Satuan & "','" & satuan_akhir_init_barang & "',"
                SQL = SQL & "" & LvB_Jumlah & ") as Hasil "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If General_Class.CekNULL(dr("Hasil")) <> "" Then
                            If dr("Hasil") = 0 Then
                                MessageBox.Show("Satuan " & LvB_Satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                nilai_production_order = dr("hasil")
                            End If
                        Else
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Satuan " & LvB_Satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If
                End Using

                ''=========================     'ambil kode formula============================'
                Dim kode_formula As String = ""
                Dim tanggal_formula As String = ""

                SQL = "select kode_formula,tanggal from EMI_Transaksi_Formulator_Binding where  "
                SQL = SQL & "Kode_Barang = '" & kd_barangINq & "' and Aktif = 'Y'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("kode_formula")) = "" Then
                            Dr.Close()
                            CloseConn()
                            MessageBox.Show("terjadi kesalahan, kode_formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            kode_formula = Dr("kode_formula")
                            tanggal_formula = Dr("tanggal")
                        End If
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Kode formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
                '=========================================================


                SQL = "select hasil,satuan_hasil from Emi_Transaksi_Formulator where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & kode_formula & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvB_Kode_Brg & "',"
                        SQL = SQL & "'" & Dr("satuan_hasil") & "','" & satuan_akhir_init_barang & "',"
                        SQL = SQL & "" & Dr("hasil") & ") as Hasil "
                        Dr.Close()

                        Using dr2 = OpenTrans(SQL)
                            If dr2.Read Then
                                If General_Class.CekNULL(dr2("Hasil")) <> "" Then
                                    If dr2("Hasil") = 0 Then
                                        MessageBox.Show("Satuan " & LvB_Satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        totalSerapan = dr2("hasil")
                                    End If
                                Else
                                    dr2.Close()
                                    CloseConn()
                                    MessageBox.Show("Satuan " & LvB_Satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Formula tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                'SQL = "select a.no_faktur,a.kode_stock_owner,a.kode_barang,b.nama,a.nilai_barang,a.persentase,a.satuan_barang"
                'SQL = SQL & " from EMI_Transaksi_Formulator_Detail_Bahan a, barang b and EMI_Transaksi_Formulator c where  "
                'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner "
                'SQL = SQL & "and a.kode_barang = b.kode_barang "
                'SQL = SQL & "and a.kode_perusahaan and c.kode_perusahaan and a.no_faktur = c.no_faktur  "
                'SQL = SQL & "and c.status is null  and  a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & kode_formula & "' "
                SQL = "select a.no_faktur,a.kode_stock_owner,a.kode_barang, c.nama,"
                SQL = SQL & "a.nilai_barang,a.persentase,a.satuan_barang, "
                SQL = SQL & "isnull((select sum(Good_Stock) From barang x where a.Kode_Perusahaan = x.Kode_Perusahaan and a.Kode_Barang  = x.Kode_Barang),null) as stock "
                SQL = SQL & "From EMI_Transaksi_Formulator_Detail_Bahan a, Emi_Transaksi_Formulator b,barang c  "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and b.Status is null "
                SQL = SQL & "and a.kode_perusahaan = c.kode_perusahaan and a.kode_stock_owner = c.kode_stock_owner and a.kode_barang = c.kode_barang "
                SQL = SQL & "and b.kode_perusahaan = '" & KodePerusahaan & "' and b.no_faktur = '" & kode_formula & "' "

                Using ds = BindingTrans(SQL)
                    With ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For indexFormulator As Integer = 0 To .Rows.Count - 1



                                Dim jumlah As Double = 0

                                nilaiPersentase = nilai_production_order / totalSerapan

                                jumlah = .Rows(indexFormulator).Item("nilai_barang") * nilaiPersentase

                                Dim convertKeSatuanAsli As String = ""
                                Dim jumlahBarangDibutuhkan As Double = 0

                                SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexFormulator).Item("kode_barang") & "' "
                                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                Using Dr3 = OpenTrans(SQL)
                                    If Dr3.Read Then
                                        convertKeSatuanAsli = Dr3("satuan")
                                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexFormulator).Item("kode_barang") & "',"
                                        SQL = SQL & "'" & .Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                        SQL = SQL & "" & jumlah & ") as Hasil "
                                        Dr3.Close()

                                        Using dr4 = OpenTrans(SQL)
                                            If dr4.Read Then
                                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                                    If dr4("Hasil") = 0 Then
                                                        MessageBox.Show("Satuan " & LvB_Satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    Else
                                                        jumlahBarangDibutuhkan = dr4("hasil")

                                                    End If
                                                Else
                                                    dr4.Close()
                                                    CloseConn()
                                                    MessageBox.Show("Satuan " & LvB_Satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                        End Using
                                    Else
                                        Dr3.Close()
                                        CloseConn()
                                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using



                                Dim stockConvert As Double = 0
                                Dim converKesatuanAsliBarangStok As String = ""
                                '============= convert nilai dan satuan stock barang ke tampilan display 
                                SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexFormulator).Item("kode_barang") & "' "
                                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                Using Dr3 = OpenTrans(SQL)
                                    If Dr3.Read Then
                                        converKesatuanAsliBarangStok = Dr3("satuan")
                                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexFormulator).Item("kode_barang") & "',"
                                        SQL = SQL & "'" & .Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                        SQL = SQL & "" & .Rows(indexFormulator).Item("stock") & ") as Hasil "
                                        Dr3.Close()

                                        Using dr4 = OpenTrans(SQL)
                                            If dr4.Read Then
                                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then

                                                    stockConvert = dr4("hasil")


                                                Else
                                                    dr4.Close()
                                                    CloseConn()
                                                    MessageBox.Show("Satuan " & LvB_Satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                        End Using
                                    Else
                                        Dr3.Close()
                                        CloseConn()
                                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using


                                SQL = "insert into Emi_Split_Production_Order_Detail_Bahan(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang) values( "
                                SQL = SQL & "'" & KodePerusahaan & "', '" & no_fak & "' , '" & LvB_Kode_SO & "','" & .Rows(indexFormulator).Item("kode_barang") & "', '" & HilangkanTanda(jumlahBarangDibutuhkan) & "', '" & convertKeSatuanAsli & "', "
                                SQL = SQL & "" & jumlah & ", '" & .Rows(indexFormulator).Item("satuan_barang") & "' ) "
                                ExecuteTrans(SQL)


                                'SQL = "insert into emi_order_produksi_detail_bahan(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang) values("
                                'SQL = SQL & "'" & KodePerusahaan & "','" & txtNoFaktur.Text & "' ,'" & LvBahan.Items(i).SubItems(0).Text & "', '" & LvBahan.Items(i).SubItems(1).Text & "', "
                                'SQL = SQL & "'" & HilangkanTanda(LvBahan.Items(i).SubItems(3).Text) & "' , '" & LvBahan.Items(i).SubItems(4).Text & "',  "
                                'SQL = SQL & "'" & HilangkanTanda(LvBahan.Items(i).SubItems(7).Text) & "', '" & LvBahan.Items(i).SubItems(8).Text & "' )"
                                'ExecuteTrans(SQL)




                                'lvwFormulator = LvBahan.Items.Add(.Rows(indexFormulator).Item("kode_stock_owner")) '0
                                'lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("kode_barang")) 1
                                'lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("nama")) 2
                                'lvwFormulator.SubItems.Add(Format(jumlahBarangDibutuhkan, "N2")) 3
                                'lvwFormulator.SubItems.Add(convertKeSatuanAsli) 4
                                'lvwFormulator.SubItems.Add(Format(stockConvert, "N2")) 5
                                'lvwFormulator.SubItems.Add(converKesatuanAsliBarangStok) 6
                                'lvwFormulator.SubItems.Add(jumlah) 7
                                'lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("satuan_barang")) 8
                                'lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("stock"))
                                'TextBox1.Text = kode_formula
                                'DateTimePicker3.Value = tanggal_formula

                            Next
                        Else
                            CloseConn()
                            MessageBox.Show("Formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using




                'SQL = "select a.kode_Barang,b.nama, b.Satuan as Satuan_Barang, a.Jumlah_Barang, a.Kode_Bahan, c.Nama as nama_bahan, c.satuan as satuan_bahan, A.Jumlah_Bahan, c.good_stock "
                'SQL = SQL & "from barang_detail_Bahan_Penolong a, barang b, barang c where b.Kode_barang='" & LvB_Kode_Brg & "' "
                'SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan and a.Kode_Barang=b.Kode_Barang_Inq and b.Kode_Stock_Owner='" & LvB_Kode_SO & "' "
                'SQL = SQL & " And a.kode_Perusahaan = c.kode_Perusahaan And a.Kode_Bahan = c.Kode_Barang And c.Kode_Stock_Owner ='" & LvB_Kode_SO & "' "

                SQL = "select a.kode_Barang,b.nama, b.Satuan as Satuan_Barang, a.Jumlah_Barang, a.Kode_Bahan, c.Nama as nama_bahan, "
                SQL = SQL & "c.satuan as satuan_bahan, A.Jumlah_Bahan "
                SQL = SQL & ",isnull((select sum(good_stock) from barang x where x.Kode_Barang = a.Kode_Bahan "
                SQL = SQL & "),0) as good_stock "
                SQL = SQL & "from barang_detail_Bahan_Penolong a, barang b, barang c "
                SQL = SQL & "where b.Kode_barang='" & LvB_Kode_Brg & "' and a.kode_Perusahaan=b.kode_Perusahaan and a.Kode_Barang=b.Kode_Barang_Inq and b.Kode_Stock_Owner='" & LvB_Kode_SO & "'  "
                SQL = SQL & "And a.kode_Perusahaan = c.kode_Perusahaan And a.Kode_Bahan = c.Kode_Barang And c.Kode_Stock_Owner ='" & LvB_Kode_SO & "' "

                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For indexBahan = 0 To .Rows.Count - 1


                            ' Dim lvwPackaging As ListViewItem
                            Dim satuan_barang As String = .Rows(indexBahan).Item("Satuan_Barang")
                            Dim Kode_bahan As String = .Rows(indexBahan).Item("Kode_Bahan")
                            Dim satuan_bahan As String = .Rows(indexBahan).Item("Satuan_Bahan")

                            Dim jumlah As Double = .Rows(indexBahan).Item("Jumlah_Barang")
                            Dim jumlahbahan As Double = .Rows(indexBahan).Item("Jumlah_Bahan")
                            Dim jumlahstock As Double = .Rows(indexBahan).Item("good_stock")
                            Dim jumlah_barang_satuan_barang As Double = 0

                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvB_Kode_Brg & "',"
                            SQL = SQL & "'" & LvB_Satuan & "','" & satuan_barang & "',"
                            SQL = SQL & "" & LvB_Jumlah & ") as Hasil "
                            Using dr4 = OpenTrans(SQL)
                                If dr4.Read Then
                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                        If dr4("Hasil") = 0 Then
                                            MessageBox.Show("Satuan " & LvB_Satuan & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            jumlah_barang_satuan_barang = dr4("hasil")

                                        End If
                                    Else
                                        dr4.Close()
                                        CloseConn()
                                        MessageBox.Show("Satuan " & LvB_Satuan & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            Dim jumlahBahan_Total As Double = Math.Ceiling((jumlah_barang_satuan_barang / jumlah) * jumlahbahan)


                            Dim jumlahBahan_Total_display As Double = 0
                            Dim jumlahstock_Total_display As Double = 0
                            Dim satuan_display As String = ""

                            '============= convert nilai dan satuan stock barang ke tampilan display 
                            SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & Kode_bahan & "' "
                            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                            Using Dr3 = OpenTrans(SQL)
                                If Dr3.Read Then
                                    satuan_display = Dr3("satuan")

                                    '==== Convert NIlai Bahan 
                                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kode_bahan & "',"
                                    SQL = SQL & "'" & satuan_bahan & "','" & satuan_display & "',"
                                    SQL = SQL & "" & jumlahBahan_Total & ") as Hasil "
                                    Dr3.Close()

                                    Using dr4 = OpenTrans(SQL)
                                        If dr4.Read Then
                                            If General_Class.CekNULL(dr4("Hasil")) <> "" Then

                                                jumlahBahan_Total_display = dr4("hasil")


                                            Else
                                                dr4.Close()
                                                CloseConn()
                                                MessageBox.Show("Satuan " & satuan_bahan & " Ke " & satuan_display & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End Using

                                    '==== Convert NIlai Stock
                                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kode_bahan & "',"
                                    SQL = SQL & "'" & satuan_bahan & "','" & satuan_display & "',"
                                    SQL = SQL & "" & jumlahstock & ") as Hasil "
                                    Dr3.Close()

                                    Using dr4 = OpenTrans(SQL)
                                        If dr4.Read Then
                                            If General_Class.CekNULL(dr4("Hasil")) <> "" Then

                                                jumlahstock_Total_display = dr4("hasil")


                                            Else
                                                dr4.Close()
                                                CloseConn()
                                                MessageBox.Show("Satuan " & satuan_bahan & " Ke " & satuan_display & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End Using
                                Else
                                    Dr3.Close()
                                    CloseConn()
                                    MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            SQL = "insert into Emi_Split_Production_Order_Detail_Packaging(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang) values( "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & no_fak & "' , '" & LvB_Kode_SO & "','" & Kode_bahan & "', '" & jumlahBahan_Total_display & "', '" & satuan_display & "', "
                            SQL = SQL & "" & jumlahBahan_Total & ", '" & satuan_bahan & "' ) "
                            ExecuteTrans(SQL)



                            'lvwPackaging = LvPackaging.Items.Add(lks)
                            'lvwPackaging.SubItems.Add(Kode_bahan)
                            'lvwPackaging.SubItems.Add(.Rows(indexBahan).Item("nama_bahan"))
                            'lvwPackaging.SubItems.Add(Format(jumlahBahan_Total_display, "N2"))
                            'lvwPackaging.SubItems.Add(satuan_display)
                            'lvwPackaging.SubItems.Add(Format(jumlahstock_Total_display, "N2"))
                            'lvwPackaging.SubItems.Add(satuan_display)
                            'lvwPackaging.SubItems.Add(jumlahBahan_Total)
                            'lvwPackaging.SubItems.Add(satuan_bahan)
                            'lvwPackaging.SubItems.Add(jumlahstock)


                        Next
                    End With
                End Using


                'akhir penentu bahan baku dan packaging


            Next

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
        If e.KeyChar = Chr(13) Then
            btnOk_Click(Me, Nothing)
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub EMI_Penentu_Production_Order_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        get_isi_listview_Atas(ListView1.FocusedItem.Index)

        Dim sisa As Double = 0
        If ListView2.Items.Count <> 0 Then
            For a As Integer = 0 To ListView2.Items.Count - 1
                get_isi_listview_Bawah(a)
                If LvA_No_PO = LvB_No_PO And LvA_Kode_SO = LvB_Kode_SO And LvA_Kode_Brg = LvB_Kode_Brg Then
                    sisa = sisa + Val(HilangkanTanda(LvB_Jumlah))
                End If
            Next
        End If

        Ket_SO = LvA_Kode_SO
        TextBox1.Text = LvA_No_PO
        txtKodeBarang.Text = LvA_Kode_Brg
        txtNmBarang.Text = LvA_Nama_Brg
        txtRouting.Text = LvA_Routing
        Ket_Id_Routing = LvA_Routing_Id
        txtJumlah.Text = Val(HilangkanTanda(LvA_Jumlah_Sisa)) - sisa
        txtSatuan.Text = LvA_Satuan
        txtJumlah.Focus()

    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        get_isi_listview_Bawah(ListView2.FocusedItem.Index)

        Ket_SO = LvB_Kode_SO
        TextBox1.Text = LvB_No_PO
        txtKodeBarang.Text = LvB_Kode_Brg
        txtNmBarang.Text = LvB_Nama_Brg
        txtRouting.Text = LvB_Routing
        Ket_Id_Routing = LvB_Routing_Id
        txtJumlah.Text = Val(HilangkanTanda(LvB_Jumlah))
        txtSatuan.Text = LvB_Satuan
        txtJumlah.Focus()

        ListView2.FocusedItem.Remove()
    End Sub
End Class
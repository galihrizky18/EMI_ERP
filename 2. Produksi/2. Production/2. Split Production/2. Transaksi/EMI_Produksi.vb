Public Class EMI_Produksi
    Dim arrcari, arrId_line, arrId_Karyawan, arrInisialFaktur, arrInisialRouting As New ArrayList
    Dim Jenis = "Transaksi_Produksi"
    Dim no_po, kd_so, satuan As String

    Dim Txt_NoFaktur_ReqMaterial As String


    Dim Dgv_Bahan_Lokasi_Tujuan, Dgv_Bahan_Kode_Barang, Dgv_Bahan_Nama_Barang, Dgv_Bahan_Jumlah_Kebutuhan, Dgv_Bahan_Satuan As String


    Dim Cell_Bahan_Lokasi_Tujuan As Integer = 0
    Dim Cell_Bahan_Kode_Barang As Integer = 1
    Dim Cell_Bahan_Nama_Barang As Integer = 2
    Dim Cell_Bahan_Jumlah_Kebutuhan As Integer = 3
    Dim Cell_Bahan_Satuan As Integer = 4







    Private Sub get_no_faktur(ByVal no As String)
        'Dim fTransSplitPO As String = "SPO"
        'Txt_NoFaktur.Text = fTransSplitPO & Format(tgl_skg, "MMyy") & "-" &
        '                     General_Class.Get_Last_Number2("Emi_Split_Production_Order", "no_transaksi", 5,
        '                     "Kode_perusahaan", KodePerusahaan,
        '                     "And", "substring(no_transaksi, 1, " & Len(fTransSplitPO) + 4 & ")", fTransSplitPO & Format(tgl_skg, "MMyy"))

        SQL = "select count(kode_Perusahaan) as Jumlah "
        SQL = SQL & "from Emi_Split_Production_Order where "
        SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and no_po='" & no & "' "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                Txt_NoFaktur.Text = no & "-" & (dr("Jumlah") + 1)
            End If
        End Using

    End Sub

    Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            get_jam()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Transaksi_Produksi_Judul
            'Label8.Text = Base_Language.Lang_Global_No_Transaksi
            Label6.Text = Base_Language.Lang_Transaksi_Produksi_No_Rencana
            Label7.Text = Base_Language.Lang_Global_Tanggal_Produksi
            'Label2.Text = Base_Language.Lang_Global_Jam
            Label4.Text = Base_Language.lang_global_keterangan
            Label5.Text = "Operator"
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

            Txt_BatchNo.Text = ""
            Txt_JumlahBatch.Text = ""
            Txt_QtyBatch.Text = ""
            Txt_NoFaktur_ReqMaterial = ""
            Cmb_SatuanBatch.Items.Clear()
            Cmb_SatuanBatch.Items.Add("KG")
            Cmb_SatuanBatch.SelectedIndex = 0
            Cmb_SatuanBatch.Enabled = False


            Cmb_Operator.Items.Clear() : arrId_Karyawan.Clear()
            SQL = "select a.Id_Karyawan,a.Nama from Emi_Karyawan a,Emi_Jabatan_Internal b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Id_Jabatan = b.Id_Jabatan and b.Flag_Tampil_Produksi = 'Y' "
            SQL = SQL & "order by Nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Operator.Items.Add(dr("Nama"))
                    arrId_Karyawan.Add(dr("Id_Karyawan"))
                Loop
            End Using

            arrInisialFaktur.Clear() : Cmb_Lokasi.Items.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            arrInisialRouting.Clear() : Cmb_Routing.Items.Clear()
            SQL = "select Id_Routing,Keterangan from EMI_Master_Routing where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Routing.Items.Add(dr("Keterangan")) : arrInisialRouting.Add(dr("Id_Routing"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        TextBox4_Leave(Nothing, e)

        LoadDataDGV()

    End Sub

    Private Sub Get_Dgv_Bahan_Data(ByVal index As Integer)
        Dgv_Bahan_Lokasi_Tujuan = Dgv_Data_Bahan.Rows(index).Cells(Cell_Bahan_Lokasi_Tujuan).Value
        Dgv_Bahan_Kode_Barang = Dgv_Data_Bahan.Rows(index).Cells(Cell_Bahan_Kode_Barang).Value
        Dgv_Bahan_Nama_Barang = Dgv_Data_Bahan.Rows(index).Cells(Cell_Bahan_Nama_Barang).Value
        Dgv_Bahan_Jumlah_Kebutuhan = Dgv_Data_Bahan.Rows(index).Cells(Cell_Bahan_Jumlah_Kebutuhan).Value
        Dgv_Bahan_Satuan = Dgv_Data_Bahan.Rows(index).Cells(Cell_Bahan_Satuan).Value
    End Sub



    Public Sub LoadDataDGV()

        Try
            OpenConn()


            '===============================
            '=     LOAD DATA DGV BAHAN     =
            '===============================
            Dgv_Data_Bahan.Rows.Clear()
            SQL = "select c.lokasi_gudang, a.kode_barang, b.nama, isnull(d.Flag_Gudang_Default, 'T') as Flag_Gudang_Default "
            SQL = SQL & "from Emi_Order_Produksi_Detail_Bahan a, barang b, EMI_Kategori_Gudang_PerLokasi c, stock_owner_gudang d "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.kode_stock_owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.ID_Kategori_Gudang = c.ID_Kategori_Gudang "
            SQL = SQL & "and c.kode_perusahaan = d.kode_perusahaan and c.lokasi_gudang = d.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & no_po & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Data_Bahan.Rows.Add(1)
                            Dgv_Data_Bahan.Rows(i).Cells(Cell_Bahan_Lokasi_Tujuan).Value = .Rows(i).Item("lokasi_gudang")
                            Dgv_Data_Bahan.Rows(i).Cells(Cell_Bahan_Kode_Barang).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_Data_Bahan.Rows(i).Cells(Cell_Bahan_Nama_Barang).Value = .Rows(i).Item("Nama")

                            If .Rows(i).Item("Flag_Gudang_Default") = "Y" Then
                                Dgv_Data_Bahan.Rows(i).DefaultCellStyle.BackColor = Color.LightYellow
                            End If



                        Next
                    End If
                End With
            End Using


            '===================================
            '=     LOAD DATA DGV PACKAGING     =
            '===================================
            Dgv_Data_Packaging.Rows.Clear()
            SQL = "select c.lokasi_gudang, a.kode_barang, b.nama, isnull(d.Flag_Gudang_Default, 'T') as Flag_Gudang_Default "
            SQL = SQL & "from Emi_Order_Produksi_Detail_Packaging a, barang b, EMI_Kategori_Gudang_PerLokasi c, stock_owner_gudang d "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.kode_stock_owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.ID_Kategori_Gudang = c.ID_Kategori_Gudang "
            SQL = SQL & "and c.kode_perusahaan = d.kode_perusahaan and c.lokasi_gudang = d.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & no_po & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Data_Packaging.Rows.Add(1)
                            Dgv_Data_Packaging.Rows(i).Cells(Cell_Bahan_Lokasi_Tujuan).Value = .Rows(i).Item("lokasi_gudang")
                            Dgv_Data_Packaging.Rows(i).Cells(Cell_Bahan_Kode_Barang).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_Data_Packaging.Rows(i).Cells(Cell_Bahan_Nama_Barang).Value = .Rows(i).Item("Nama")

                            If .Rows(i).Item("Flag_Gudang_Default") = "Y" Then
                                Dgv_Data_Packaging.Rows(i).DefaultCellStyle.BackColor = Color.LightYellow
                            End If


                        Next
                    End If
                End With
            End Using






            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_BatchNo.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Produksi_Error_No_Batch, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_BatchNo.Focus() : Exit Sub
        ElseIf Cmb_Operator.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Produksi_Error_Operator, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Operator.Focus() : Exit Sub
        ElseIf Txt_Qty.Text.Trim.Length = 0 Then
            MessageBox.Show("Qty", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Qty.Focus() : Exit Sub
        ElseIf Txt_QtyBatch.Text.Trim.Length = 0 Or Val(HilangkanTanda(Txt_QtyBatch.Text)) = 0 Then
            MessageBox.Show("Qty Batch", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_QtyBatch.Focus() : Exit Sub
        End If
        get_jam()


        Dim arrFakturRM As New ArrayList
        arrFakturRM.Clear()
        '--- SIMPAN 
        Try
            OpenConn()
            get_no_faktur(no_po)
            Cmd.Transaction = Cn.BeginTransaction

            '
            SQL = "INSERT INTO Emi_Split_Production_Order(Kode_Perusahaan,No_Transaksi,No_PO,Lokasi,Tanggal,Jam,UserID,Kode_Stock_Owner,"
            SQL = SQL & "Kode_Barang,Jumlah,Satuan, "
            SQL = SQL & "Flag_Produksi,Tgl_Produksi, Jam_Produksi, No_Batch, Operator, Jumlah_Batch, Qty_Batch, Satuan_Batch) "
            SQL = SQL & "Values ('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & no_po & "', "
            SQL = SQL & "'" & Cmb_Lokasi.Text & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(DateTimePicker2.Value, "HH:mm:ss") & "', "
            SQL = SQL & "'" & UserID & "', '" & kd_so & "', '" & Txt_KdBarang.Text & "', '" & Txt_Qty.Text & "', "
            SQL = SQL & "'" & satuan & "', "
            SQL = SQL & "'Y', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & Format(DateTimePicker2.Value, "HH:mm:ss") & "', "
            SQL = SQL & "'" & Txt_BatchNo.Text & "', '" & arrId_Karyawan.Item(Cmb_Operator.SelectedIndex) & "', "
            SQL = SQL & "'" & HilangkanTanda(Txt_JumlahBatch.Text) & "', '" & HilangkanTanda(Txt_QtyBatch.Text) & "', '" & Cmb_SatuanBatch.Text & "')"
            ExecuteTrans(SQL)

            '
            SQL = "select a.No_Faktur,a.Kode_Stock_Owner,a.Kode_Barang,c.Nama,a.Jumlah,a.Satuan,d.Keterangan,a.Id_Routing, "
            SQL = SQL & "ISNULL((select sum(z.Jumlah) from Emi_Split_Production_Order z where z.No_PO = a.No_Faktur and z.status is null"
            SQL = SQL & "),0) as Jml_Sdh_Split "
            SQL = SQL & "from EMI_Order_Produksi a,Barang c,EMI_Master_Routing d where "
            SQL = SQL & "a.Status is null and a.Selesai is null and Flag_Release = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Id_Routing = d.Id_Routing and a.Flag_Selesai_Split is null "
            SQL = SQL & "and a.Flag_Selesai_Produksi is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & no_po & "' "

            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("Jumlah") = dr("Jml_Sdh_Split") Then

                        dr.Close()
                        SQL = "update EMI_Order_Produksi set Flag_Selesai_Split = 'Y' where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & no_po & "'"
                        ExecuteTrans(SQL)

                    ElseIf dr("Jml_Sdh_Split") > dr("Jumlah") Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Melebihi Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
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
            SQL = SQL & "and Kode_Barang ='" & Txt_KdBarang.Text & "' "
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
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KdBarang.Text & "',"
            SQL = SQL & "'" & satuan & "','" & satuan_akhir_init_barang & "',"
            SQL = SQL & "" & Txt_Qty.Text & ") as Hasil "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                        If dr("Hasil") = 0 Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Satuan " & satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            nilai_production_order = dr("hasil")
                        End If
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Satuan " & satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            ''=========================Ambil Kode Formula============================'
            Dim kode_formula As String = ""
            Dim tanggal_formula As String = ""

            SQL = "select a.Kode_Formula, b.Tanggal from EMI_Order_Produksi a, Emi_Transaksi_Formulator b where "
            SQL = SQL & "a.no_faktur='" & no_po & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Formula=b.No_Faktur "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    kode_formula = Dr("Kode_Formula")
                    tanggal_formula = Dr("tanggal")

                Else
                    Dr.Close()
                    CloseTrans()
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

                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KdBarang.Text & "',"
                    SQL = SQL & "'" & Dr("satuan_hasil") & "','" & satuan_akhir_init_barang & "',"
                    SQL = SQL & "" & Dr("hasil") & ") as Hasil "
                    Dr.Close()

                    Using dr2 = OpenTrans(SQL)
                        If dr2.Read Then
                            If General_Class.CekNULL(dr2("Hasil")) <> "" Then
                                If dr2("Hasil") = 0 Then
                                    dr2.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Satuan " & satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                Else
                                    totalSerapan = dr2("hasil")
                                End If
                            Else
                                dr2.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Satuan " & satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End Using
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Formula tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '
            SQL = "select a.no_faktur, a.kode_stock_owner, a.kode_barang, c.nama,"
            SQL = SQL & "a.nilai_barang, a.persentase, a.satuan_barang, "
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

                            jumlah = Val(HilangkanTanda(Format(.Rows(indexFormulator).Item("nilai_barang"), "N4"))) * nilaiPersentase

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
                                                    dr4.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Satuan " & satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                Else
                                                    jumlahBarangDibutuhkan = dr4("hasil")
                                                End If
                                            Else
                                                dr4.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Satuan " & satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End Using
                                Else
                                    Dr3.Close()
                                    CloseTrans()
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
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Satuan " & satuan & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End Using
                                Else
                                    Dr3.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "insert into Emi_Split_Production_Order_Detail_Bahan(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang) values( "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "' , '" & kd_so & "','" & .Rows(indexFormulator).Item("kode_barang") & "', '" & HilangkanTanda(Format(jumlahBarangDibutuhkan, "N4")) & "', '" & convertKeSatuanAsli & "', "
                            SQL = SQL & "" & HilangkanTanda(Format(jumlah, "N4")) & ", '" & .Rows(indexFormulator).Item("satuan_barang") & "' ) "
                            ExecuteTrans(SQL)

                        Next
                    Else
                        CloseConn()
                        CloseTrans()
                        MessageBox.Show("Formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '------------------------------------------------------------
            SQL = "select d.kode_Barang, b.nama, b.Satuan as Satuan_Barang, a.Jumlah_Barang, a.Kode_barang as kode_Bahan, c.Nama as nama_bahan, "
            SQL = SQL & "c.satuan as satuan_bahan, A.Jumlah_Bahan "
            SQL = SQL & ",isnull((select sum(good_stock) from barang x where x.Kode_Barang = a.Kode_barang "
            SQL = SQL & "),0) as good_stock, a.Jenis "
            SQL = SQL & "from EMI_Order_Produksi_Detail_Packaging a, barang b, barang c, EMI_Order_Produksi d "
            SQL = SQL & "where a.kode_Perusahaan=d.kode_perusahaan and a.no_faktur=d.no_faktur and "
            SQL = SQL & "d.no_Faktur='" & no_po & "' and d.kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "d.kode_Perusahaan=b.kode_Perusahaan And d.Kode_Barang=b.Kode_Barang And b.Kode_Stock_Owner='" & kd_so & "' and "
            SQL = SQL & "a.kode_Perusahaan = c.kode_Perusahaan And a.Kode_Barang = c.Kode_Barang And c.Kode_Stock_Owner ='" & kd_so & "' "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For indexBahan = 0 To .Rows.Count - 1
                        ' Dim lvwPackaging As ListViewItem
                        Dim satuan_barang As String = .Rows(indexBahan).Item("Satuan_Barang")
                        Dim Kode_bahan As String = .Rows(indexBahan).Item("Kode_Bahan")
                        Dim satuan_bahan As String = .Rows(indexBahan).Item("Satuan_Bahan")

                        Dim Jenis_kemasan As String = .Rows(indexBahan).Item("Jenis")

                        Dim jumlah As Double = .Rows(indexBahan).Item("Jumlah_Barang")
                        Dim jumlahbahan As Double = Val(HilangkanTanda(Format(.Rows(indexBahan).Item("Jumlah_Bahan"), "N4")))
                        Dim jumlahstock As Double = Val(HilangkanTanda(Format(.Rows(indexBahan).Item("good_stock"), "N4")))
                        Dim jumlah_barang_satuan_barang As Double = 0

                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KdBarang.Text & "',"
                        SQL = SQL & "'" & satuan & "','" & satuan_barang & "',"
                        SQL = SQL & "" & Txt_Qty.Text & ") as Hasil "
                        Using dr4 = OpenTrans(SQL)
                            If dr4.Read Then
                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                    If dr4("Hasil") = 0 Then
                                        dr4.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Satuan " & satuan & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        jumlah_barang_satuan_barang = dr4("hasil")
                                    End If
                                Else
                                    dr4.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Satuan " & satuan & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using

                        Dim jumlahBahan_Total As Double = ((jumlah_barang_satuan_barang / jumlah) * jumlahbahan)

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
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Satuan " & satuan_bahan & " Ke " & satuan_display & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End Using

                                '==== Convert Nilai Stock
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
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Satuan " & satuan_bahan & " Ke " & satuan_display & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End Using
                            Else
                                Dr3.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        SQL = "insert into Emi_Split_Production_Order_Detail_Packaging(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang, jumlah_barang, Jumlah_Bahan, Jenis) values( "
                        SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "' , '" & kd_so & "','" & Kode_bahan & "', '" & HilangkanTanda(Format(jumlahBahan_Total_display, "N4")) & "', '" & satuan_display & "', "
                        SQL = SQL & "" & HilangkanTanda(Format(jumlahBahan_Total, "N4")) & ", '" & satuan_bahan & "', '" & jumlah & "', '" & jumlahbahan & "', '" & Jenis_kemasan & "' ) "
                        ExecuteTrans(SQL)

                    Next
                End With
            End Using

            'Akhir penentu bahan baku dan packaging

            '=================================
            '=     AUTO REQUEST MATERIAL     =
            '=================================


            arrFakturRM.Clear()

#Region "Auto Request Material"

            Dim Jumlah_Input_Batch As Integer = 0
            '=======================================
            '=     AUTO REQUEST MATERIAL BAHAN     =
            '=======================================
            For x As Integer = 0 To Val(HilangkanTanda(Txt_JumlahBatch.Text)) - 1

                Txt_NoFaktur_ReqMaterial = fRequestMaterial & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Material_Requisition", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(fRequestMaterial) + 4 & ")", fRequestMaterial & Format(tgl_skg, "MMyy"))

                Dim Keterangan_RM As String = "Request Material for Batch No " & (x + 1) & " from Production Order " & no_po

                '=============================
                '=     GET ID GROUP JENIS    =
                '=============================
                Dim Id_Group_Jenis As String = ""
                SQL = "Select Id_Group_Jenis from barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & kd_so & "' and Kode_Barang='" & Txt_KdBarang.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Id_Group_Jenis = Dr("Id_Group_Jenis")
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jenis Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                End Using

                '=========================
                '=     INSERT PARENT     =
                '=========================
                arrFakturRM.Add(Txt_NoFaktur_ReqMaterial)
                SQL = "insert into Emi_Material_Requisition (Kode_Perusahaan, No_Faktur, No_Faktur_Order, Kode_Stock_Owner, Kode_Barang, Id_Group_Jenis, Tanggal, Jam, Flag_Process, UserId, Status, Keterangan, Lokasi, "
                SQL = SQL & "Flag_Otomatis, Batch) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & Txt_NoFaktur.Text & "', "
                SQL = SQL & "'" & kd_so & "', '" & Txt_KdBarang.Text & "', '" & Id_Group_Jenis & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'Y', '" & UserID & "', NULL, '" & Keterangan_RM & "', '" & Cmb_Lokasi.Text & "', "
                SQL = SQL & "'Y', " & x + 1 & ")"
                ExecuteTrans(SQL)


                SQL = "Select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Nilai_Barang, a.Satuan_Barang, 'Bahan' as tipe, c.lokasi_gudang, "
                SQL = SQL & "(ISNULL( (select sum(z.jumlah) from Emi_Material_Requisition_det z, Emi_Material_Requisition x where a.Kode_Perusahaan = z.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
                SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.Status is null ) "
                SQL = SQL & ", 0)) as Jumlah_Diproduksi, "
                SQL = SQL & "(a.Jumlah - ISNULL( "
                SQL = SQL & "(select sum(z.jumlah) "
                SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
                SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
                SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null ) "
                SQL = SQL & ", 0)) as sisa, "
                SQL = SQL & "'BAHAN' as Jenis_Bahan, "
                SQL = SQL & "ISNULL(( select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Barang, a.Satuan, sum(z.Jumlah)) "
                SQL = SQL & "from Barang_SN z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang "
                SQL = SQL & "), 0) as Stock_Gudang_Produksi, "
                SQL = SQL & "ISNULL( (select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w, "
                SQL = SQL & "emi_material_requisition_det_convert m, emi_material_requisition n "
                SQL = SQL & "where x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null "
                SQL = SQL & "And y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') "
                SQL = SQL & "and z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det "
                SQL = SQL & "And m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert "
                SQL = SQL & "and y.Flag_Jenis_Request = 'PRODUKSI' "
                SQL = SQL & "and m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null "
                SQL = SQL & "and a.Kode_Perusahaan = m.Kode_Perusahaan and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan = n.Kode_Perusahaan and a.No_Faktur = n.No_Faktur_Order and n.Status is null "
                SQL = SQL & "), '0') as Total_TF, "

                SQL = SQL & "isnull(( select "
                SQL = SQL & "isnull(( ( (dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',z.Kode_Barang, z.Satuan_Batch, 'KG', z.Qty_Batch)) / " 'DISINI UNTUK KALI SESUAI BATCH
                SQL = SQL & "(select dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',z.Kode_Barang, r.Satuan_Hasil, 'KG', r.Hasil) "
                SQL = SQL & "from Emi_Transaksi_Formulator r "
                SQL = SQL & "where r.Kode_Perusahaan = x.Kode_Perusahaan and r.No_Faktur = x.Kode_Formula and z.Status is null) "
                SQL = SQL & ") * y.Jumlah ), 0) as Nilai_PerBatch "
                SQL = SQL & "from Emi_Split_Production_Order z, EMI_Order_Produksi x, EMI_Transaksi_Formulator_Detail_Bahan y "
                SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan "
                SQL = SQL & "and z.No_PO = x.No_Faktur "
                SQL = SQL & "and x.Kode_Formula = y.No_Faktur "
                SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                SQL = SQL & "and z.No_Transaksi = a.no_faktur "
                SQL = SQL & "and y.Kode_Barang = a.Kode_Barang and y.Satuan = a.satuan "
                SQL = SQL & "),0) as Nilai_Per_Batch "
                SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a, Barang b, EMI_Kategori_Gudang_PerLokasi c, Stock_Owner_Gudang d "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
                SQL = SQL & "and c.kode_perusahaan = d.kode_Perusahaan and c.lokasi_gudang = d.kode_Stock_owner "
                SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur='" & Txt_NoFaktur.Text & "' "

                'SQL = SQL & "union all "

                'SQL = SQL & "select a.No_Faktur, a.Kode_Stock_Owner, a.kode_barang, b.Nama, a.Jumlah, a.Satuan, a.Nilai_Barang, a.Satuan_Barang, 'Packaging' as tipe, c.lokasi_gudang, "
                'SQL = SQL & "(ISNULL((select sum(z.jumlah) from Emi_Material_Requisition_det z, Emi_Material_Requisition x where a.Kode_Perusahaan = z.Kode_Perusahaan "
                'SQL = SQL & "and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
                'SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null) "
                'SQL = SQL & ", 0)) as Jumlah_Diproduksi, "
                'SQL = SQL & "(a.Jumlah - ISNULL( "
                'SQL = SQL & "(select sum(z.jumlah) "
                'SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
                'SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
                'SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null) "
                'SQL = SQL & ", 0)) as sisa, "
                'SQL = SQL & "'PACKAGING' as Jenis_Bahan, "
                'SQL = SQL & "ISNULL(( select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Barang, a.Satuan, sum(z.Jumlah)) "
                'SQL = SQL & "from Barang_SN z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang) "
                'SQL = SQL & ", 0) as Stock_Gudang_Produksi, "
                'SQL = SQL & "ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w, "
                'SQL = SQL & "emi_material_requisition_det_convert m, emi_material_requisition n "
                'SQL = SQL & "where x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null "
                'SQL = SQL & "And y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') "
                'SQL = SQL & "and z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det "
                'SQL = SQL & "And m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert "
                'SQL = SQL & "and y.Flag_Jenis_Request = 'PRODUKSI' "
                'SQL = SQL & "and m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null "
                'SQL = SQL & "and a.Kode_Perusahaan = m.Kode_Perusahaan and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang "
                'SQL = SQL & "and a.Kode_Perusahaan = n.Kode_Perusahaan and a.No_Faktur = n.No_Faktur_Order and n.status is null "
                'SQL = SQL & "), '0') as Total_TF, "

                'SQL = SQL & "isnull((select ISNULL((( (dbo.ubah_satuan(z.kode_perusahaan, 'masa',z.kode_barang, 'KG', 'PCS', "
                'SQL = SQL & "(ISNULL(( select r.Qty_Batch from Emi_Split_Production_Order r where r.Kode_Perusahaan = x.Kode_Perusahaan And r.No_Transaksi = z.No_Transaksi ), 0)))) " ' DISINI UNTUK BAGI SESUAI BATCH
                'SQL = SQL & "/ "
                'SQL = SQL & "(select r.jumlah_barang from Barang_Detail_Bahan_Penolong r where r.kode_barang= z.Kode_Barang and r.kode_Bahan = x.Kode_Barang)) "
                'SQL = SQL & "* "
                'SQL = SQL & "((select r.jumlah_bahan from Barang_Detail_Bahan_Penolong r where r.kode_barang= z.Kode_Barang and r.kode_Bahan = x.Kode_Barang)) ), 0) as Nilai_PerBatch "
                'SQL = SQL & "from Emi_Split_Production_Order z, Emi_Split_Production_Order_Detail_Packaging x, EMI_Order_Produksi y "
                'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.kode_perusahaan = y.kode_perusahaan "
                'SQL = SQL & "and z.No_Transaksi = x.No_Faktur "
                'SQL = SQL & "and z.No_PO = y.No_Faktur "
                'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                'SQL = SQL & "and z.No_Transaksi = a.No_Faktur "
                'SQL = SQL & "and x.Kode_Barang = a.Kode_Barang "
                'SQL = SQL & "and x.Satuan = a.Satuan "
                'SQL = SQL & "), 0) as Nilai_Per_Batch "
                'SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a, Barang b, EMI_Kategori_Gudang_PerLokasi c, Stock_Owner_Gudang d "
                'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
                'SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
                'SQL = SQL & "and c.kode_perusahaan = d.kode_Perusahaan and c.lokasi_gudang = d.kode_Stock_owner "
                'SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and a.No_Faktur='" & Txt_NoFaktur.Text & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count - 1 Then
                            For i As Integer = 0 To .Rows.Count - 1


                                Dim KdBarang As String = .Rows(i).Item("Kode_Barang")
                                Dim SatuanBesar As String = .Rows(i).Item("Satuan")
                                Dim SatuanKecil As String = .Rows(i).Item("Satuan_Barang")
                                Dim JumlahRequest As Double = .Rows(i).Item("Nilai_Per_Batch")
                                Dim KdSo As String = .Rows(i).Item("Kode_Stock_Owner")
                                Dim JenisBahan As String = .Rows(i).Item("Jenis_Bahan")
                                Dim Jumlah_Kebutuhan As Double = .Rows(i).Item("Jumlah")
                                Dim Tipe As String = .Rows(i).Item("tipe")



                                Dim LokasiTujuan As String = .Rows(i).Item("lokasi_gudang")

                                SQL = "select isnull(flag_gudang_default, 'T') as flag_gudang_default "
                                SQL = SQL & "from stock_owner_gudang where kode_perusahaan='" & KodePerusahaan & "' and kode_stock_owner='" & LokasiTujuan & "' "
                                Using Dr1 = OpenTrans(SQL)
                                    If Dr1.Read Then

                                        If Dr1("flag_gudang_default") = "Y" Then
                                            Dr1.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Gudang Tujuan Belum di sesuaikan ! ")
                                            Exit Sub
                                        End If
                                    Else
                                        Dr1.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("data gudang tidak ada ")
                                        Exit Sub
                                    End If
                                End Using

                                '================================
                                '=     CONVERT SATUAN KECIL     =
                                '================================
                                Dim nilai_kecil As Double = 0
                                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & KdBarang & "', '" & SatuanBesar & "',"
                                SQL = SQL & "'" & SatuanKecil & "', '" & HilangkanTanda(JumlahRequest) & "' ) as hasil"
                                Using Dr1 = OpenTrans(SQL)
                                    If Dr1.Read Then
                                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                            Dr1.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                                            Exit Sub
                                        End If

                                        nilai_kecil = Val(HilangkanTanda(Format(Dr1("hasil"), "N4")))
                                    Else
                                        Dr1.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                                        Exit Sub
                                    End If
                                End Using

                                '==============================
                                '=     INSERT TABEL DET     =
                                '==============================

                                SQL = "insert into Emi_Material_Requisition_det (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Stock_Owner_Tujuan, Kode_Barang, Kebutuhan, Jumlah, Satuan, Jumlah_Barang, Satuan_Barang, Jenis_Material) values "
                                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & KdSo & "', '" & LokasiTujuan & "', '" & KdBarang & "', '" & HilangkanTanda(Jumlah_Kebutuhan) & "',  "
                                SQL = SQL & "'" & HilangkanTanda(JumlahRequest) & "', "
                                SQL = SQL & "'" & SatuanBesar & "', '" & nilai_kecil & "', '" & SatuanKecil & "', '" & Tipe & "')"
                                ExecuteTrans(SQL)


                                Dim x_ident_currentPackaging As Integer = 0
                                SQL = "select IDENT_CURRENT('Emi_Material_Requisition_det') as urutan"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        x_ident_currentPackaging = Dr("urutan")
                                    End If
                                End Using

                                SQL = "insert into Emi_Material_Requisition_det_convert(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Jumlah_Barang,Satuan_Barang,Warna,No_Urut_Det)"
                                SQL = SQL & "values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & KdSo & "', '" & KdBarang & "', "
                                SQL = SQL & "'" & HilangkanTanda(JumlahRequest) & "', "
                                SQL = SQL & "'" & SatuanBesar & "', '" & nilai_kecil & "', '" & SatuanKecil & "', 'Hijau', '" & x_ident_currentPackaging & "')"
                                ExecuteTrans(SQL)

                                '======================================
                                '=     CEK APAKAH BAHAN TERPENUHI     =
                                '======================================
                                If JenisBahan = "BAHAN" Then

                                    SQL = "select "
                                    SQL = SQL & "(a.jumlah - ISNULL(( "
                                    SQL = SQL & "select sum(x.Jumlah) "
                                    SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
                                    SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                                    SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                                    SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
                                    SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
                                    SQL = SQL & "), 0)) as Sisa "
                                    SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
                                    SQL = SQL & "and a.Kode_Barang = '" & KdBarang & "' "
                                    Using Ds1 = BindingTrans(SQL)
                                        If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                            Dim cekDataDouble As Integer = 0
                                            For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
                                                cekDataDouble = cekDataDouble + 1

                                                If cekDataDouble > 1 Then
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If

                                                If Val(.Rows(j).Item("Sisa")) = 0 Then

                                                    SQL = "update Emi_Split_Production_Order_Detail_Bahan set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
                                                    SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' and Kode_Stock_Owner = '" & kd_so & "' and Kode_Barang = '" & KdBarang & "'"
                                                    ExecuteTrans(SQL)

                                                End If
                                            Next
                                        End If
                                    End Using

                                ElseIf JenisBahan = "PACKAGING" Then

                                    SQL = "select "
                                    SQL = SQL & "(a.jumlah - ISNULL(( "
                                    SQL = SQL & "select sum(x.Jumlah) "
                                    SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
                                    SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                                    SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                                    SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
                                    SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
                                    SQL = SQL & "), 0)) as Sisa "
                                    SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
                                    SQL = SQL & "and a.Kode_Barang = '" & KdBarang & "' "
                                    Using Ds1 = BindingTrans(SQL)
                                        If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                            Dim cekDataDouble As Integer = 0
                                            For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
                                                cekDataDouble = cekDataDouble + 1

                                                If cekDataDouble > 1 Then
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If

                                                If Val(Ds1.Tables("MyTable").Rows(j).Item("Sisa")) = 0 Then

                                                    SQL = "update Emi_Split_Production_Order_Detail_Packaging set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
                                                    SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' and Kode_Stock_Owner = '" & kd_so & "' and Kode_Barang = '" & KdBarang & "'"
                                                    ExecuteTrans(SQL)

                                                End If
                                            Next
                                        End If
                                    End Using

                                End If

                            Next
                        End If
                    End With
                End Using

            Next



            '===========================================
            '=     AUTO REQUEST MATERIAL PACKAGING     =
            '===========================================
#Region "ADD RM PACKAGING"
            Txt_NoFaktur_ReqMaterial = fRequestMaterial & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Material_Requisition", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(fRequestMaterial) + 4 & ")", fRequestMaterial & Format(tgl_skg, "MMyy"))

            Dim Keterangan_RM_Pck As String = "Request Material Packaging from Production Order " & no_po

            '=============================
            '=     GET ID GROUP JENIS    =
            '=============================
            Dim Id_Group_Jenis_Pck As String = ""
            SQL = "Select Id_Group_Jenis from barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & kd_so & "' and Kode_Barang='" & Txt_KdBarang.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Id_Group_Jenis_Pck = Dr("Id_Group_Jenis")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jenis Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Using

            '=========================
            '=     INSERT PARENT     =
            '=========================
            arrFakturRM.Add(Txt_NoFaktur_ReqMaterial)
            SQL = "insert into Emi_Material_Requisition (Kode_Perusahaan, No_Faktur, No_Faktur_Order, Kode_Stock_Owner, Kode_Barang, Id_Group_Jenis, Tanggal, Jam, Flag_Process, UserId, Status, Keterangan, Lokasi, "
            SQL = SQL & "Flag_Otomatis, Batch) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & Txt_NoFaktur.Text & "', "
            SQL = SQL & "'" & kd_so & "', '" & Txt_KdBarang.Text & "', '" & Id_Group_Jenis_Pck & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'Y', '" & UserID & "', NULL, '" & Keterangan_RM_Pck & "', '" & Cmb_Lokasi.Text & "', "
            SQL = SQL & "'Y', 0)"
            ExecuteTrans(SQL)


            'SQL = "Select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Nilai_Barang, a.Satuan_Barang, 'Bahan' as tipe, c.lokasi_gudang, "
            'SQL = SQL & "(ISNULL( (select sum(z.jumlah) from Emi_Material_Requisition_det z, Emi_Material_Requisition x where a.Kode_Perusahaan = z.Kode_Perusahaan "
            'SQL = SQL & "and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            'SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.Status is null ) "
            'SQL = SQL & ", 0)) as Jumlah_Diproduksi, "
            'SQL = SQL & "(a.Jumlah - ISNULL( "
            'SQL = SQL & "(select sum(z.jumlah) "
            'SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
            'SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            'SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null ) "
            'SQL = SQL & ", 0)) as sisa, "
            'SQL = SQL & "'BAHAN' as Jenis_Bahan, "
            'SQL = SQL & "ISNULL(( select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Barang, a.Satuan, sum(z.Jumlah)) "
            'SQL = SQL & "from Barang_SN z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang "
            'SQL = SQL & "), 0) as Stock_Gudang_Produksi, "
            'SQL = SQL & "ISNULL( (select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w, "
            'SQL = SQL & "emi_material_requisition_det_convert m, emi_material_requisition n "
            'SQL = SQL & "where x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null "
            'SQL = SQL & "And y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') "
            'SQL = SQL & "and z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det "
            'SQL = SQL & "And m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert "
            'SQL = SQL & "and y.Flag_Jenis_Request = 'PRODUKSI' "
            'SQL = SQL & "and m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null "
            'SQL = SQL & "and a.Kode_Perusahaan = m.Kode_Perusahaan and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang "
            'SQL = SQL & "and a.Kode_Perusahaan = n.Kode_Perusahaan and a.No_Faktur = n.No_Faktur_Order and n.Status is null "
            'SQL = SQL & "), '0') as Total_TF, "

            'SQL = SQL & "isnull(( select "
            'SQL = SQL & "isnull(( ( (dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',z.Kode_Barang, z.Satuan_Batch, 'KG', z.Qty_Batch)) / " 'DISINI UNTUK KALI SESUAI BATCH
            'SQL = SQL & "(select dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',z.Kode_Barang, r.Satuan_Hasil, 'KG', r.Hasil) "
            'SQL = SQL & "from Emi_Transaksi_Formulator r "
            'SQL = SQL & "where r.Kode_Perusahaan = x.Kode_Perusahaan and r.No_Faktur = x.Kode_Formula and z.Status is null) "
            'SQL = SQL & ") * y.Jumlah ), 0) as Nilai_PerBatch "
            'SQL = SQL & "from Emi_Split_Production_Order z, EMI_Order_Produksi x, EMI_Transaksi_Formulator_Detail_Bahan y "
            'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan "
            'SQL = SQL & "and z.No_PO = x.No_Faktur "
            'SQL = SQL & "and x.Kode_Formula = y.No_Faktur "
            'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            'SQL = SQL & "and z.No_Transaksi = a.no_faktur "
            'SQL = SQL & "and y.Kode_Barang = a.Kode_Barang and y.Satuan = a.satuan "
            'SQL = SQL & "),0) as Nilai_Per_Batch "
            'SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a, Barang b, EMI_Kategori_Gudang_PerLokasi c, Stock_Owner_Gudang d "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
            'SQL = SQL & "and c.kode_perusahaan = d.kode_Perusahaan and c.lokasi_gudang = d.kode_Stock_owner "
            'SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur='" & Txt_NoFaktur.Text & "' "

            'SQL = SQL & "union all "

            SQL = "select a.No_Faktur, a.Kode_Stock_Owner, a.kode_barang, b.Nama, a.Jumlah, a.Satuan, a.Nilai_Barang, a.Satuan_Barang, 'Packaging' as tipe, c.lokasi_gudang, "
            SQL = SQL & "(ISNULL((select sum(z.jumlah) from Emi_Material_Requisition_det z, Emi_Material_Requisition x where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null) "
            SQL = SQL & ", 0)) as Jumlah_Diproduksi, "
            SQL = SQL & "(a.Jumlah - ISNULL( "
            SQL = SQL & "(select sum(z.jumlah) "
            SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null) "
            SQL = SQL & ", 0)) as sisa, "
            SQL = SQL & "'PACKAGING' as Jenis_Bahan, "
            SQL = SQL & "ISNULL(( select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Barang, a.Satuan, sum(z.Jumlah)) "
            SQL = SQL & "from Barang_SN z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang) "
            SQL = SQL & ", 0) as Stock_Gudang_Produksi, "
            SQL = SQL & "ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w, "
            SQL = SQL & "emi_material_requisition_det_convert m, emi_material_requisition n "
            SQL = SQL & "where x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null "
            SQL = SQL & "And y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') "
            SQL = SQL & "and z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det "
            SQL = SQL & "And m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert "
            SQL = SQL & "and y.Flag_Jenis_Request = 'PRODUKSI' "
            SQL = SQL & "and m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = m.Kode_Perusahaan and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = n.Kode_Perusahaan and a.No_Faktur = n.No_Faktur_Order and n.status is null "
            SQL = SQL & "), '0') as Total_TF, "

            SQL = SQL & "isnull((select ISNULL((( (dbo.ubah_satuan(z.kode_perusahaan, 'masa',z.kode_barang, 'KG', 'PCS', "
            SQL = SQL & "(ISNULL(( select r.Qty_Batch * " & Val(HilangkanTanda(Txt_JumlahBatch.Text)) & " from Emi_Split_Production_Order r where r.Kode_Perusahaan = x.Kode_Perusahaan And r.No_Transaksi = z.No_Transaksi ), 0)))) " ' DISINI UNTUK BAGI SESUAI BATCH
            SQL = SQL & "/ "
            SQL = SQL & "x.jumlah_barang) "
            SQL = SQL & "* "
            SQL = SQL & "(x.jumlah_bahan) ), 0) as Nilai_PerBatch "
            SQL = SQL & "from Emi_Split_Production_Order z, Emi_Split_Production_Order_Detail_Packaging x, EMI_Order_Produksi y, barang w "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.kode_perusahaan = y.kode_perusahaan "
            SQL = SQL & "and z.No_Transaksi = x.No_Faktur "
            SQL = SQL & "and z.No_PO = y.No_Faktur "
            SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = a.No_Faktur "
            SQL = SQL & "and x.Kode_Barang = a.Kode_Barang "
            SQL = SQL & "and x.Satuan = a.Satuan "
            SQL = SQL & "and z.kode_Barang=w.kode_barang and z.kode_Perusahaan = w.kode_perusahaan and z.kode_stock_owner=w.kode_stock_owner "
            SQL = SQL & "), 0) as Nilai_Per_Batch "

            SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a, Barang b, EMI_Kategori_Gudang_PerLokasi c, Stock_Owner_Gudang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
            SQL = SQL & "and c.kode_perusahaan = d.kode_Perusahaan and c.lokasi_gudang = d.kode_Stock_owner "
            SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & Txt_NoFaktur.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count - 1 Then
                        For i As Integer = 0 To .Rows.Count - 1


                            Dim KdBarang As String = .Rows(i).Item("Kode_Barang")
                            Dim SatuanBesar As String = .Rows(i).Item("Satuan")
                            Dim SatuanKecil As String = .Rows(i).Item("Satuan_Barang")
                            Dim JumlahRequest As Double = .Rows(i).Item("Nilai_Per_Batch")
                            Dim KdSo As String = .Rows(i).Item("Kode_Stock_Owner")
                            Dim JenisBahan As String = .Rows(i).Item("Jenis_Bahan")
                            Dim Jumlah_Kebutuhan As Double = .Rows(i).Item("Jumlah")
                            Dim Tipe As String = .Rows(i).Item("tipe")

                            Dim LokasiTujuan As String = .Rows(i).Item("lokasi_gudang")

                            SQL = "select isnull(flag_gudang_default, 'T') as flag_gudang_default "
                            SQL = SQL & "from stock_owner_gudang where kode_perusahaan='" & KodePerusahaan & "' and kode_stock_owner='" & LokasiTujuan & "' "
                            Using Dr1 = OpenTrans(SQL)
                                If Dr1.Read Then

                                    If Dr1("flag_gudang_default") = "Y" Then
                                        Dr1.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Gudang Tujuan Belum di sesuaikan ! ")
                                        Exit Sub
                                    End If
                                Else
                                    Dr1.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("data gudang tidak ada ")
                                    Exit Sub
                                End If
                            End Using


                            '================================
                            '=     CONVERT SATUAN KECIL     =
                            '================================
                            Dim nilai_kecil As Double = 0
                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & KdBarang & "', '" & SatuanBesar & "',"
                            SQL = SQL & "'" & SatuanKecil & "', '" & HilangkanTanda(JumlahRequest) & "' ) as hasil"
                            Using Dr1 = OpenTrans(SQL)
                                If Dr1.Read Then
                                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                        Dr1.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                                        Exit Sub
                                    End If

                                    nilai_kecil = Val(HilangkanTanda(Format(Dr1("hasil"), "N4")))
                                Else
                                    Dr1.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                                    Exit Sub
                                End If
                            End Using

                            '==============================
                            '=     INSERT TABEL DET     =
                            '==============================

                            SQL = "insert into Emi_Material_Requisition_det (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Stock_Owner_Tujuan, Kode_Barang, Kebutuhan, Jumlah, Satuan, Jumlah_Barang, Satuan_Barang, Jenis_Material) values "
                            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & KdSo & "', '" & LokasiTujuan & "', '" & KdBarang & "', '" & HilangkanTanda(Jumlah_Kebutuhan) & "',  "
                            SQL = SQL & "'" & HilangkanTanda(JumlahRequest) & "', "
                            SQL = SQL & "'" & SatuanBesar & "', '" & nilai_kecil & "', '" & SatuanKecil & "', '" & Tipe & "')"
                            ExecuteTrans(SQL)


                            Dim x_ident_currentPackaging As Integer = 0
                            SQL = "select IDENT_CURRENT('Emi_Material_Requisition_det') as urutan"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    x_ident_currentPackaging = Dr("urutan")
                                End If
                            End Using

                            SQL = "insert into Emi_Material_Requisition_det_convert(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Jumlah_Barang,Satuan_Barang,Warna,No_Urut_Det)"
                            SQL = SQL & "values("
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & KdSo & "', '" & KdBarang & "', "
                            SQL = SQL & "'" & HilangkanTanda(JumlahRequest) & "', "
                            SQL = SQL & "'" & SatuanBesar & "', '" & nilai_kecil & "', '" & SatuanKecil & "', 'Hijau', '" & x_ident_currentPackaging & "')"
                            ExecuteTrans(SQL)

                            '======================================
                            '=     CEK APAKAH BAHAN TERPENUHI     =
                            '======================================
                            If JenisBahan = "BAHAN" Then

                                SQL = "select "
                                SQL = SQL & "(a.jumlah - ISNULL(( "
                                SQL = SQL & "select sum(x.Jumlah) "
                                SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
                                SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                                SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                                SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
                                SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
                                SQL = SQL & "), 0)) as Sisa "
                                SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
                                SQL = SQL & "and a.Kode_Barang = '" & KdBarang & "' "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                        Dim cekDataDouble As Integer = 0
                                        For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
                                            cekDataDouble = cekDataDouble + 1

                                            If cekDataDouble > 1 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                            If Val(.Rows(j).Item("Sisa")) = 0 Then

                                                SQL = "update Emi_Split_Production_Order_Detail_Bahan set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' and Kode_Stock_Owner = '" & kd_so & "' and Kode_Barang = '" & KdBarang & "'"
                                                ExecuteTrans(SQL)

                                            End If
                                        Next
                                    End If
                                End Using

                            ElseIf JenisBahan = "PACKAGING" Then

                                SQL = "select "
                                SQL = SQL & "(a.jumlah - ISNULL(( "
                                SQL = SQL & "select sum(x.Jumlah) "
                                SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
                                SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                                SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                                SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
                                SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
                                SQL = SQL & "), 0)) as Sisa "
                                SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
                                SQL = SQL & "and a.Kode_Barang = '" & KdBarang & "' "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                        Dim cekDataDouble As Integer = 0
                                        For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
                                            cekDataDouble = cekDataDouble + 1

                                            If cekDataDouble > 1 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                            If Val(Ds1.Tables("MyTable").Rows(j).Item("Sisa")) = 0 Then

                                                SQL = "update Emi_Split_Production_Order_Detail_Packaging set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' and Kode_Stock_Owner = '" & kd_so & "' and Kode_Barang = '" & KdBarang & "'"
                                                ExecuteTrans(SQL)

                                            End If
                                        Next
                                    End If
                                End Using

                            End If

                        Next
                    End If
                End With
            End Using
#End Region


#End Region



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CetakFaktur(arrFakturRM)

        EMI_Display_Mulai_Produksi.Btn_Cari_Click(Btn_Simpan, e)
        Me.Close()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now
        Txt_Qty.Text = ""
        Txt_BatchNo.Text = ""
        Txt_NoFaktur_ReqMaterial = ""
        Cmb_Operator.SelectedIndex = -1
    End Sub



    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub



    Public Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles Txt_NoTransaksi.Leave
        Try
            OpenConn()

            Txt_QtyBatch.Text = ""
            SQL = "select a.no_faktur, a.Lokasi, a.Tanggal, a.Jam, a.Kode_stock_Owner, a.kode_barang, b.nama as nama_barang, d.keterangan as jenis_produk, a.jumlah, a.satuan, c.Keterangan as Routing, ISNULL(c.Qty_PerBatch, 0) as Qty_PerBatch "
            SQL = SQL & ",ISNULL((select sum(z.Jumlah) from Emi_Split_Production_Order z where z.No_PO = a.No_Faktur and z.status is null "
            SQL = SQL & "),0) as Jml_Sdh_Split, b.berat "
            SQL = SQL & "from emi_order_produksi a, barang b, emi_master_routing c, emi_jenis_produk d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.Kode_stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Id_Routing = c.Id_Routing and a.Id_Jenis_Produk = d.Id_Jenis_Produk "
            SQL = SQL & "and a.status is null and a.flag_release = 'Y' "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & Txt_NoTransaksi.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_po = dr("no_faktur")
                    Cmb_Lokasi.Text = dr("lokasi")
                    DateTimePicker1.Focus()
                    kd_so = dr("kode_stock_owner")
                    Txt_KdBarang.Text = dr("kode_barang")
                    TxtBerat.Text = Format(dr("berat"), "N0")
                    TextBox1.Text = dr("nama_barang")
                    TxtQtyPO.Text = dr("jumlah")
                    TxtQtyProduksi.Text = dr("Jml_Sdh_Split")
                    Txt_Qty.Text = ""
                    Cmb_Routing.Text = dr("routing")
                    satuan = dr("satuan")
                    TxtQtyPO_Satuan.Text = satuan
                    TxtQtyProduksi_Satuan.Text = satuan
                    Txt_DisplayQtyPO.Text = TxtQtyPO.Text + " " + TxtQtyPO_Satuan.Text
                    Txt_DisplayQtyProd.Text = TxtQtyProduksi.Text + " " + TxtQtyProduksi_Satuan.Text
                    'catatan = dr("catatan")

                    Txt_QtyBatch.Text = Val(HilangkanTanda(dr("Qty_PerBatch")))
                End If
            End Using

            Cmb_Satuan.Items.Clear()
            SQL = "select Kode_barang, Satuan from barang_detail_satuan "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Flag_Tampil_Display = 'Y' and "
            SQL = SQL & "Kode_barang = '" & Txt_KdBarang.Text & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Satuan.Items.Add(dr("Satuan"))
                Loop
            End Using
            Cmb_Satuan.SelectedIndex = 0

            '============================
            '=     GET QTY PERBATCH     =
            '============================


            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Dgv_Data_Bahan_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data_Bahan.CellContentClick

    End Sub

    Private Sub Txt_Qty_TextChanged(sender As Object, e As EventArgs) Handles Txt_Qty.TextChanged
        If Txt_Qty.Text.Trim.Length = 0 Or Txt_JumlahBatch.Text.Trim.Length = 0 Then
            Txt_QtyBatch.Text = 0
            Exit Sub
        End If

        Txt_QtyBatch.Text = ((Val(HilangkanTanda(Txt_Qty.Text)) * Val(HilangkanTanda(TxtBerat.Text))) / Val(HilangkanTanda(Txt_JumlahBatch.Text))) / 1000


    End Sub

    Private Sub Dgv_Data_Packaging_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data_Packaging.CellContentClick

    End Sub

    Private Sub Txt_JumlahBatch_TextChanged(sender As Object, e As EventArgs) Handles Txt_JumlahBatch.TextChanged
        If Txt_Qty.Text.Trim.Length = 0 Or Txt_JumlahBatch.Text.Trim.Length = 0 Then
            Txt_QtyBatch.Text = 0
            Exit Sub
        End If

        Txt_QtyBatch.Text = ((Val(HilangkanTanda(Txt_Qty.Text)) * Val(HilangkanTanda(TxtBerat.Text))) / Val(HilangkanTanda(Txt_JumlahBatch.Text))) / 1000

    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_BatchNo.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_BatchNo.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Operator.Focus()
    End Sub

    Private Sub Txt_Qty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Qty.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        ElseIf e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If


        If e.KeyChar = Chr(13) Then Txt_JumlahBatch.Focus()
    End Sub

    Private Sub Txt_JumlahBatch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_JumlahBatch.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        ElseIf e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Txt_QtyBatch.Focus()
    End Sub

    Private Sub Txt_QtyBatch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_QtyBatch.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        ElseIf e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Cmb_Routing.Focus()
    End Sub


    Private Sub Handle_Ubah_Lokasi_Tujuan_Bahan(sender As Object, e As EventArgs) Handles Dgv_Data_Bahan.DoubleClick, Dgv_Data_Packaging.DoubleClick
        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("RM_Ubah_Lokasi_Tujuan") = "T" Then
                'CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Update Lokasi Tujuan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dim dgv As DataGridView = TryCast(sender, DataGridView)
        If dgv Is Nothing Then
            MessageBox.Show("Data GridView Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If dgv.CurrentRow Is Nothing OrElse dgv.CurrentRow.Index < 0 Then
            MessageBox.Show("Tidak ada baris yang dipilih.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim KDbarang As String = dgv.CurrentRow.Cells(Cell_Bahan_Kode_Barang).Value

        If MessageBox.Show("Yakin Ingin Mengubah Lokasi Tujuan Barang " & KDbarang & " ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub
        N_EMI_SD_Produksi.no_po = no_po
        N_EMI_SD_Produksi.Kd_Barang = KDbarang
        N_EMI_SD_Produksi.ShowDialog()
    End Sub


    Private Sub CetakFaktur(ByVal arrFaktur As ArrayList)

        '========================
        '=     CETAK FAKTUR     =
        '========================
        For x As Integer = 0 To arrFaktur.Count - 1


            Try
                OpenConn()

                Dim CrDoc As New Object
                Dim SF As String = ""
                Dim kertas As String = ""

                '===========================
                '=     GET DATA GUDANG     =
                '===========================
                SQL = "select distinct b.Kode_Stock_Owner_Tujuan "
                SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and a.status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & arrFaktur(x) & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                Dim Lokasi As String = .Rows(i).Item("Kode_Stock_Owner_Tujuan")

                                SQL = "select kode_perusahaan from Vw_Laporan_Faktur_Request_Material "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                SF = "{Vw_Laporan_Faktur_Request_Material.kode_perusahaan} = '" & KodePerusahaan & "' "

                                SQL = SQL & "and no_faktur = '" & arrFaktur(x) & "' "
                                SF = SF & "And {Vw_Laporan_Faktur_Request_Material.no_faktur} = '" & arrFaktur(x) & "' "

                                SQL = SQL & "and Kode_Stock_Owner_Tujuan = '" & Lokasi & "' "
                                SF = SF & "And {Vw_Laporan_Faktur_Request_Material.Kode_Stock_Owner_Tujuan} = '" & Lokasi & "' "

                                SQL = SQL & "and no_faktur_Order = '" & Txt_NoFaktur.Text & "' "
                                SF = SF & "And {Vw_Laporan_Faktur_Request_Material.no_faktur_Order} = '" & Txt_NoFaktur.Text & "' "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                        CrDoc = New Faktur_Request_Material_EMI

                                        'With A_Place_For_Printing2
                                        '    CrDoc.SetDataSource(Ds)
                                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                                        '    'CrDoc.PrintOptions.PrinterName = ""
                                        '    CrDoc.RecordSelectionFormula = SF
                                        '    CrDoc.SummaryInfo.ReportTitle = "Faktur Request Material "
                                        '    .Text = "Faktur Request Material"
                                        '    .CrystalReportViewer1.ReportSource = CrDoc
                                        '    .Refresh()
                                        '    .Show()
                                        'End With

                                        '=====================================

                                        kertas = "Faktur"

                                        CrDoc.SetDataSource(Ds)
                                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                                        CrDoc.PrintOptions.PrinterName = PrinterNameSPB
                                        CrDoc.RecordSelectionFormula = SF
                                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                                        doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
                                        Dim rawKind As Integer
                                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                                        For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                            If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertas Then
                                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                                CrDoc.PrintOptions.PaperSize = rawKind
                                                Exit For
                                            End If
                                        Next

                                        'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

                                        '=======================================
                                        '=     CEK APAKAH KERTAS DITEMUKAN     =
                                        '=======================================
                                        If rawKind <> -1 Then
                                            CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                                        Else
                                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                                        End If

                                        CrDoc.PrintToPrinter(1, False, 1, 99)

                                        CrDoc.Close()

                                    Else
                                        CloseConn()
                                        MessageBox.Show("Laporan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using
                            Next
                        Else
                            CloseConn()
                            MessageBox.Show("No Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If
                    End With
                End Using

                '=== pastikan spooler dan Crystal Report menutup koneksi ===

                GC.Collect()
                GC.WaitForPendingFinalizers()

                ' Beri jeda 1-2 detik supaya spooler sempat menyelesaikan job
                Threading.Thread.Sleep(1000)


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Next

        MessageBox.Show("Request Material Berhasil Di Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub Dgv_Data_Bahan_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data_Bahan.CellDoubleClick

    End Sub
End Class
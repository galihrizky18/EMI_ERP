Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Produksi
    Dim arrcari, arrId_line, arrId_Karyawan, arrInisialFaktur, arrInisialRouting As New ArrayList
    Dim Jenis = "Transaksi_Produksi"
    Dim no_po, kd_so, satuan As String

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
            Label4.Text = Base_Language.Lang_Transaksi_Produksi_No_Batch
            Label5.Text = "Operator"
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

            Txt_BatchNo.Text = ""

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
    End Sub

    Private Sub Txt_NoTransaksi_TextChanged(sender As Object, e As EventArgs) Handles Txt_NoTransaksi.TextChanged

    End Sub

    Private Sub Txt_Qty_TextChanged(sender As Object, e As EventArgs) Handles Txt_Qty.TextChanged

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
        End If
        get_jam()

        '--- SIMPAN 
        Try
            OpenConn()
            get_no_faktur(no_po)
            Cmd.Transaction = Cn.BeginTransaction

            '
            SQL = "INSERT INTO Emi_Split_Production_Order(Kode_Perusahaan,No_Transaksi,No_PO,Lokasi,Tanggal,Jam,UserID,Kode_Stock_Owner,"
            SQL = SQL & "Kode_Barang,Jumlah,Satuan, "
            SQL = SQL & "Flag_Produksi,Tgl_Produksi, Jam_Produksi, No_Batch, Operator) "
            SQL = SQL & "Values ('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & no_po & "', "
            SQL = SQL & "'" & Cmb_Lokasi.Text & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(DateTimePicker2.Value, "HH:mm:ss") & "', "
            SQL = SQL & "'" & UserID & "', '" & kd_so & "', '" & Txt_KdBarang.Text & "', '" & Txt_Qty.Text & "', "
            SQL = SQL & "'" & satuan & "', "
            SQL = SQL & "'Y', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & Format(DateTimePicker2.Value, "HH:mm:ss") & "', "
            SQL = SQL & "'" & Txt_BatchNo.Text & "', '" & arrId_Karyawan.Item(Cmb_Operator.SelectedIndex) & "') "
            ExecuteTrans(SQL)

            '
            SQL = "select a.No_Faktur,a.Kode_Stock_Owner,a.Kode_Barang,c.Nama,a.Jumlah,a.Satuan,d.Keterangan,a.Id_Routing, "
            SQL = SQL & "ISNULL((select sum(z.Jumlah) from Emi_Split_Production_Order z where z.No_PO = a.No_Faktur "
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

            SQL = "select kode_formula,tanggal from EMI_Transaksi_Formulator_Binding where  "
            SQL = SQL & "Kode_Barang = '" & kd_barangINq & "' and Aktif = 'Y'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("kode_formula")) = "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("terjadi kesalahan, kode_formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        kode_formula = Dr("kode_formula")
                        tanggal_formula = Dr("tanggal")
                    End If
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
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "' , '" & kd_so & "','" & .Rows(indexFormulator).Item("kode_barang") & "', '" & HilangkanTanda(jumlahBarangDibutuhkan) & "', '" & convertKeSatuanAsli & "', "
                            SQL = SQL & "" & jumlah & ", '" & .Rows(indexFormulator).Item("satuan_barang") & "' ) "
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
            SQL = "select a.kode_Barang,b.nama, b.Satuan as Satuan_Barang, a.Jumlah_Barang, a.Kode_Bahan, c.Nama as nama_bahan, "
            SQL = SQL & "c.satuan as satuan_bahan, A.Jumlah_Bahan "
            SQL = SQL & ",isnull((select sum(good_stock) from barang x where x.Kode_Barang = a.Kode_Bahan "
            SQL = SQL & "),0) as good_stock "
            SQL = SQL & "from barang_detail_Bahan_Penolong a, barang b, barang c "
            SQL = SQL & "where b.Kode_barang='" & Txt_KdBarang.Text & "' and a.kode_Perusahaan=b.kode_Perusahaan and a.Kode_Barang=b.Kode_Barang_Inq and b.Kode_Stock_Owner='" & kd_so & "'  "
            SQL = SQL & "And a.kode_Perusahaan = c.kode_Perusahaan And a.Kode_Bahan = c.Kode_Barang And c.Kode_Stock_Owner ='" & kd_so & "' "

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

                        SQL = "insert into Emi_Split_Production_Order_Detail_Packaging(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang) values( "
                        SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "' , '" & kd_so & "','" & Kode_bahan & "', '" & jumlahBahan_Total_display & "', '" & satuan_display & "', "
                        SQL = SQL & "" & jumlahBahan_Total & ", '" & satuan_bahan & "' ) "
                        ExecuteTrans(SQL)

                    Next
                End With
            End Using

            'Akhir penentu bahan baku dan packaging

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
        EMI_Display_Mulai_Produksi.Btn_Cari_Click(Btn_Simpan, e)
        Me.Close()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now
        Txt_Qty.Text = ""
        Txt_BatchNo.Text = ""
        Cmb_Operator.SelectedIndex = -1
    End Sub

    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Public Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles Txt_NoTransaksi.Leave
        Try
            OpenConn()

            SQL = "select a.no_faktur, a.Lokasi, a.Tanggal, a.Jam, a.Kode_stock_Owner, a.kode_barang, b.nama as nama_barang, d.keterangan as jenis_produk, a.jumlah, a.satuan, c.Keterangan as Routing "
            SQL = SQL & ",ISNULL((select sum(z.Jumlah) from Emi_Split_Production_Order z where z.No_PO = a.No_Faktur "
            SQL = SQL & "),0) as Jml_Sdh_Split "
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

            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Txt_BatchNo.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_BatchNo.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Operator.Focus()
    End Sub

    Private Sub Txt_Qty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Qty.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

End Class
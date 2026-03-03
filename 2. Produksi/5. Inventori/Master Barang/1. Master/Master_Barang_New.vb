Public Class Master_Barang_New

    Dim arrkolom, arrorder, arrJenisBarang, arrJenisGudang, arrKategoriGudang, arrId_kategori_qc, arrId_kategori_PO, arrId_Klasifikasi_Bahan, arrprefix_Klasifikasi_Bahan, arrid_Routing As New ArrayList
    Dim arrId_Klasifikasi_Bahan2, arrprefix_Klasifikasi_Bahan2 As New ArrayList
    Dim hrg_jual_apa As String
    Dim text_hrg_jual_apa As String
    Dim boleh_lihat_global As Boolean

    Dim arrSatuanTurunan, arrGudangRawMaterial As New ArrayList
    Dim ArrTanggal, ArrParamLain As New ArrayList

    Dim Lv_Pengajuan_NoTransaksi, Lv_Pengajuan_Tanggal, Lv_Pengajuan_Jam, Lv_Pengajuan_KdBarang, Lv_Pengajuan_NmBarang As String
    Dim Lv_Pengajuan_Group_Jenis, Lv_Pengajuan_Klasifikasi_Bahan, Lv_Pengajuan_Klasifikasi_Bahan_2 As String

    Dim item_Pengajuan_Notransaksi As Integer = 0
    Dim item_Pengajuan_Tanggal As Integer = 1
    Dim item_Pengajuan_Jam As Integer = 2
    Dim item_Pengajuan_Kode_Barang As Integer = 3
    Dim item_Pengajuan_Nama_Barang As Integer = 4
    Dim item_Pengajuan_Group_Jenis As Integer = 5
    Dim item_Pengajuan_Klasifikasi_Bahan As Integer = 6
    Dim item_Pengajuan_Klasifikasi_Bahan_2 As Integer = 7



    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            ListView1.Items.Clear()
            Dim Lvw As ListViewItem

            'iniiiii
            Dim boleh_lihat As Boolean

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & ComboBox2.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            boleh_lihat = True
                        Else
                            boleh_lihat = False
                        End If
                    Else
                        boleh_lihat = True
                    End If
                Else
                    boleh_lihat = False
                End If
            End Using

            Dim lokasi_pergudang As String = ""

            SQL = " select  b.Kode_Stock_Owner_Gudang from stock_owner a , Binding_Lokasi_Gudang b where "
            SQL = SQL & "  a.Kode_Stock_Owner = '" & ComboBox2.Text & "'    and b.Gudang_Default = 'Y'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    lokasi_pergudang = Dr("Kode_Stock_Owner_Gudang")
                End If
            End Using

            SQL = "Select a.kode_stock_owner, a.kode_barang, a.Nama, a.satuan, a.good_stock, a.harga_beli, a.barang_sendiri ,pakai_sn,"
            SQL = SQL & "isnull((select kode_group_jenis from EMI_Group_Jenis x where "
            SQL = SQL & "a.kode_perusahaan = x.kode_perusahaan and a.id_group_jenis = x.id_group_jenis), null) as kode_group_jenis ,"
            SQL = SQL & "isnull((select kode_kategori_gudang from EMI_Kategori_Gudang x where "
            SQL = SQL & "a.kode_perusahaan = x.kode_perusahaan and a.id_kategori_gudang = x.id_kategori_gudang), null) as kode_kategori_gudang ,"
            SQL = SQL & "isnull((select keterangan from emi_master_kategori_gudang x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.id_master_kategori_gudang = x.id_master_kategori_gudang), null) as keterangan_master_kategori_gudang , "
            SQL = SQL & "isnull((select keterangan from emi_kategori_qc x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.ID_Kategori_QC = x.ID_Kategori_QC), null) as keterangan_QC , "
            SQL = SQL & "isnull((select keterangan from emi_kategori_po x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.ID_Kategori_po = x.ID_Kategori_po), null) as keterangan_PO , "

            SQL = SQL & "isnull((select keterangan from emi_klasifikasi_bahan x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.ID_Klasifikasi_Bahan = x.ID_Klasifikasi_Bahan), null) as keterangan_Bhn , "
            SQL = SQL & "isnull((select keterangan from emi_klasifikasi_bahan2 x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.ID_Klasifikasi_Bahan2 = x.ID_Klasifikasi_Bahan2), null) as keterangan_Bhn2 , "

            SQL = SQL & "isnull((select keterangan from EMI_Master_Routing x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.Id_Routing = x.Id_Routing), null) as keterangan_Routing , "
            SQL = SQL & "a.harga_jual, a.stock_minimum, a.kode_kategori, a.aktif,  a.flag_ppn, a.id_master_kategori_gudang,a.ID_Kategori_QC, a.ID_Kategori_PO, a.ID_Klasifikasi_Bahan,a.ID_Routing From barang a "
            SQL = SQL & "where a.kode_perusahaan = '001' and a.jenis = 'B' "
            ' SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_supplier = b.kode_supplier and "
            SQL = SQL & "and  a.kode_stock_owner = '" & lokasi_pergudang & "' and "
            If ComboBox7.SelectedIndex = -1 Then
                SQL = SQL & "a.aktif = 'Y'  "
            Else
                SQL = SQL & "a.aktif = '" & ComboBox7.Text & "'  "
            End If
            '  SQL = SQL & "a.kode_pembeda in(" & list_pembeda & ") "
            If semua = "T" Then
                SQL = SQL & "and " & arrkolom.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox8.Text & "%' "
            End If

            SQL = SQL & "order by " & arrorder.Item(ComboBox8.SelectedIndex)
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Lvw = ListView1.Items.Add(dr("kode_stock_owner"))
                    Lvw.SubItems.Add(dr("kode_barang"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("satuan"))

                    'iniiiii
                    If boleh_lihat = True Then
                        Lvw.SubItems.Add(Format(dr("good_stock"), "N0"))
                    Else
                        Lvw.SubItems.Add("")
                    End If
                    'iniiiii
                    Lvw.SubItems.Add(Format(dr("harga_beli"), "N0"))
                    Lvw.SubItems.Add(Format(dr("stock_minimum"), "N0"))
                    Lvw.SubItems.Add(dr("kode_group_jenis"))

                    If General_Class.CekNULL(dr("barang_sendiri")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("barang_sendiri"))
                    End If

                    If General_Class.CekNULL(dr("kode_kategori")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("kode_kategori"))
                    End If

                    If General_Class.CekNULL(dr("pakai_sn")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("pakai_sn"))
                    End If

                    If General_Class.CekNULL(dr("flag_ppn")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("flag_ppn"))
                    End If

                    If General_Class.CekNULL(dr("kode_kategori_gudang")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("kode_kategori_gudang"))
                    End If

                    If General_Class.CekNULL(dr("keterangan_master_kategori_gudang")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("keterangan_master_kategori_gudang"))
                    End If

                    If General_Class.CekNULL(dr("ID_Kategori_QC")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("keterangan_QC"))
                    End If

                    If General_Class.CekNULL(dr("ID_Kategori_QC")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("ID_Kategori_QC"))
                    End If

                    If General_Class.CekNULL(dr("ID_Kategori_PO")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("keterangan_PO"))
                    End If

                    If General_Class.CekNULL(dr("ID_Kategori_PO")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("ID_Kategori_PO"))
                    End If

                    If General_Class.CekNULL(dr("ID_Klasifikasi_Bahan")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("keterangan_bhn"))
                    End If

                    If General_Class.CekNULL(dr("keterangan_Bhn2")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("keterangan_Bhn2"))
                    End If

                    If General_Class.CekNULL(dr("Id_Routing")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("keterangan_Routing"))
                    End If

                    If General_Class.CekNULL(dr("Id_Routing")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(dr("Id_Routing"))
                    End If

                    '           Lvw.SubItems.Add(Format(dr("harga_reseller"), "N0"))
                    '          Lvw.SubItems.Add(Format(dr("stock_minimum"), "N0"))
                    '         Lvw.SubItems.Add(dr("lemari"))
                    '        Lvw.SubItems.Add(Format(dr("bad_stock"), "N0"))

                    '      Lvw.SubItems.Add(dr("kode_pembeda"))
                    '     Lvw.SubItems.Add(dr("aktif"))

                    ' Lvw.SubItems.Add(dr("kode_supplier"))
                    'Lvw.SubItems.Add(dr("nama_supplier"))
                    '    Lvw.SubItems.Add(dr("flag_ppn"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Kosong()
        boleh_lihat_global = False

        'Try
        '    OpenConn()
        '    Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
        '    Base_Language.Get_Languages(Bahasa_Pilihan, "Barang")
        '    'Base_Language.Get_Languages(Bahasa_Pilihan, "Ongkir")
        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        lblKodeBrng.Text = Base_Language.Lang_Global_KodeBarang
        lblNama.Text = Base_Language.Lang_Global_Nama
        lblSatuan.Text = Base_Language.Lang_Global_Satuan
        lblPenentuHarga.Text = Base_Language.Lang_Global_Penentu_Harga
        lblStockMin.Text = Base_Language.Lang_Global_Stok_Min
        lblJenis.Text = Base_Language.Lang_Global_Jenis
        lblBarangSendiri.Text = Base_Language.Lang_Global_Barang_Sendiri
        lblKategori.Text = Base_Language.Lang_Global_Kategori
        lblStatusAktif.Text = Base_Language.Lang_Global_Aktif
        lblflagppn.Text = Base_Language.Lang_Global_Flag_PPN
        lblBeratBersih.Text = Base_Language.Lang_Global_Berat_Bersih
        lblBeratKotor.Text = Base_Language.Lang_Global_Berat_Kotor
        lblKategoriKecil.Text = Base_Language.Lang_Global_Kategori_Kecil
        lblKategoriBesar.Text = Base_Language.Lang_Global_Kategori_Besar
        lblLebar.Text = Base_Language.Lang_Global_Lebar
        Label22.Text = Base_Language.Lang_Barang_Msg_Berat
        Label33.Text = Base_Language.Lang_Barang_Msg_Uk
        lblPanjang.Text = Base_Language.Lang_Global_Panjang
        lblTinggi.Text = Base_Language.Lang_Global_Tinggi
        lblUkuran.Text = Base_Language.Lang_Global_Ukuran
        lblJenisGudang.Text = Base_Language.Lang_Global_Jenis_Gudang
        Lbl_KategoriGudang.Text = Base_Language.Lang_Global_KategoriGudang
        lblKolom.Text = Base_Language.Lang_Global_Kolom
        lblStatusAktif.Text = Base_Language.Lang_Global_Aktif
        Button5.Text = Base_Language.Lang_Global_Cari

        Button1.Text = Base_Language.Lang_Global_Simpan
        Button3.Text = Base_Language.Lang_Global_Refresh

        ComboBox11.Enabled = False
        ComboBox19.Enabled = False

        DgvSatuanTerpilih.Rows.Clear()

        arrprefix_Klasifikasi_Bahan2.Clear()
        arrId_Klasifikasi_Bahan2.Clear()


        Try
            OpenConn()

            ComboBox2.Items.Clear()

            SQL = "Select kode_stock_owner, flag_default From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("kode_stock_owner"))
                    'If dr("flag_default") = "Y" Then
                    '    ComboBox2.Text = dr("kode_stock_owner")
                    'End If
                Loop
            End Using

            Cmb_FlagPotongStok.Items.Clear() : txtStandarPrice.Text = ""
            Cmb_FlagPotongStok.Items.Add("Y")
            Cmb_FlagPotongStok.Items.Add("T")

            ComboBox2.Text = Lokasi

            ComboBox4.Items.Clear()
            SQL = "Select kode_kategori From kategori_barang where kode_perusahaan = '" & KodePerusahaan & "' order by kode_kategori"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("kode_kategori"))
                Loop
            End Using

            ComboBox12.Items.Clear()
            SQL = "Select Kode_Kategori_Besar From Kategori_Besar where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Kategori_Besar"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox12.Items.Add(dr("Kode_Kategori_Besar"))
                Loop
            End Using
            'iniiiii
            'Dim boleh_lihat_global As Boolean

            cmbSatuan.Items.Clear()
            SQL = "select satuan from emi_satuan where kode_perusahaan = '" & KodePerusahaan & "' And Flag_Barang='Y' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cmbSatuan.Items.Add(Dr("satuan"))
                Loop
            End Using

            cmbJenis.Items.Clear() : arrJenisBarang.Clear() : arrGudangRawMaterial.Clear()
            SQL = "select id_group_jenis,kode_group_jenis, Flag_Raw_Material from EMI_GROUP_JENIS where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_group_jenis"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbJenis.Items.Add(dr("kode_group_jenis")) : arrJenisBarang.Add(dr("id_group_jenis"))

                    If dr("Flag_Raw_Material") = "Y" Then
                        arrGudangRawMaterial.Add(dr("id_group_jenis"))
                    End If
                Loop
            End Using

            CmbJnsGudanng.Items.Clear() : arrJenisGudang.Clear()
            SQL = "select id_kategori_gudang, Kode_Kategori_Gudang from emi_kategori_gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_Kategori_Gudang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbJnsGudanng.Items.Add(dr("Kode_Kategori_Gudang")) : arrJenisGudang.Add(dr("id_kategori_gudang"))
                Loop
            End Using

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & ComboBox2.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            boleh_lihat_global = True
                        Else
                            boleh_lihat_global = False
                        End If
                    Else
                        boleh_lihat_global = True
                    End If
                Else
                    boleh_lihat_global = False
                End If
            End Using

            ComboBox5.Items.Clear() : arrId_kategori_qc.Clear()
            SQL = "select ID_Kategori_QC,Keterangan from Emi_Kategori_QC where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox5.Items.Add(Dr("Keterangan"))
                    arrId_kategori_qc.Add(Dr("ID_Kategori_QC"))
                Loop
            End Using

            ComboBox6.Items.Clear() : arrId_kategori_PO.Clear()
            SQL = "select ID_Kategori_PO,Keterangan from Emi_Kategori_PO where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox6.Items.Add(Dr("Keterangan"))
                    arrId_kategori_PO.Add(Dr("ID_Kategori_PO"))
                Loop
            End Using

            ComboBox11.Items.Clear() : arrId_Klasifikasi_Bahan.Clear() : arrprefix_Klasifikasi_Bahan.Clear()
            SQL = "select ID_Klasifikasi_Bahan,Keterangan, Prefix_Klasifikasi_Bahan from Emi_Klasifikasi_Bahan where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox11.Items.Add(Dr("Keterangan"))
                    arrId_Klasifikasi_Bahan.Add(Dr("ID_Klasifikasi_Bahan"))
                    arrprefix_Klasifikasi_Bahan.Add(Dr("Prefix_Klasifikasi_Bahan"))
                Loop
            End Using

            ComboBox19.Items.Clear()
            ComboBox19.SelectedIndex = -1
            arrId_Klasifikasi_Bahan2.Clear() : arrprefix_Klasifikasi_Bahan2.Clear()

            'ComboBox5.Items.Clear()
            'ComboBox5.Items.Add("MK")
            'ComboBox5.SelectedIndex = 0
            'SQL = "Select kode_pembeda From pembeda where kode_perusahaan = '" & KodePerusahaan & "' order by kode_pembeda"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        ComboBox5.Items.Add(dr("kode_pembeda"))
            '    Loop
            'End Using

            ComboBox14.Items.Clear() : arrid_Routing.Clear() : ComboBox14.SelectedIndex = -1
            SQL = "select Id_Routing,Keterangan from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox14.Items.Add(Dr("Keterangan"))
                    arrid_Routing.Add(Dr("Id_Routing"))
                Loop
            End Using

            ComboBox17.Items.Clear()
            SQL = "select Satuan_Berat_default from Init"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox17.Items.Add(Dr("Satuan_Berat_default"))
                Loop
                ComboBox17.SelectedIndex = 0
            End Using

            ComboBox18.Items.Clear()
            'SQL = "select Satuan_Timbang from Init"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBox18.Items.Add(Dr("Satuan_Timbang"))
            '    Loop
            '    ComboBox18.SelectedIndex = 0
            'End Using

            ComboBox15.Items.Clear()
            ComboBox15.Items.Add("Original Bags")
            ComboBox15.Items.Add("Non Original Bags")

            ComboBox16.Items.Clear()
            ComboBox16.Items.Add("FIFO")
            ComboBox16.Items.Add("FEFO")


            Cmb_Est_Harga_Mata_Uang.Items.Clear()
            SQL = "select Kode_Mata_Uang from Mata_Uang "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Est_Harga_Mata_Uang.Items.Add(Dr("Kode_Mata_Uang"))
                Loop
            End Using
            Cmb_Est_Harga_Mata_Uang.SelectedItem = "RP"


            Txt_Est_Harga.Text = ""


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If My.Settings.Punya = "MS" Then
            hrg_jual_apa = "harga_jual"
            text_hrg_jual_apa = "Harga Jual Std"
        ElseIf My.Settings.Punya = "MK" Then
            hrg_jual_apa = "harga_jual_agen"
            text_hrg_jual_apa = "Harga Jual Std"
        Else
            hrg_jual_apa = "xzxzxz"
            text_hrg_jual_apa = ""
        End If

        ComboBox1.Items.Clear() : arrkolom.Clear()
        ComboBox1.Items.Add("Kode Barang") : arrkolom.Add("a.Kode_Barang")
        ComboBox1.Items.Add("Nama") : arrkolom.Add("a.Nama")
        ComboBox1.Items.Add("Satuan") : arrkolom.Add("a.Satuan")
        ComboBox1.Items.Add("Harga Beli") : arrkolom.Add("a.Harga_Beli")
        ComboBox1.Items.Add(text_hrg_jual_apa) : arrkolom.Add(hrg_jual_apa)
        ComboBox1.Items.Add("Good Stock") : arrkolom.Add("a.Good_Stock")
        ComboBox1.Items.Add("Stock Min.") : arrkolom.Add("a.Stock_Minimum")
        ComboBox1.Items.Add("Kode Kategori") : arrkolom.Add("a.Kode_Kategori")
        ComboBox1.Items.Add("Kode Supplier") : arrkolom.Add("a.Kode_Supplier")
        ComboBox1.Items.Add("Nama Supplier") : arrkolom.Add("b.nama")
        ComboBox1.Items.Add("Barang Sendiri") : arrkolom.Add("a.Flag_sendiri")
        ComboBox1.SelectedIndex = 0

        ComboBox8.Items.Clear() : arrorder.Clear()
        ComboBox8.Items.Add("Kode Barang") : arrorder.Add("Kode_Barang")
        ComboBox8.Items.Add("Nama") : arrorder.Add("Nama")
        ComboBox8.Items.Add("Kode Kategori") : arrorder.Add("Kode_Kategori")
        ComboBox8.SelectedIndex = 0

        TextBox1.Enabled = False
        TextBox1.Text = "" : TextBox2.Text = "" : cmbSatuan.SelectedIndex = -1
        TextBox5.Text = "" : TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = "" : ComboBox3.SelectedIndex = 0
        'ComboBox5.SelectedIndex = -1
        TextBox12.Text = "" : TextBox15.Text = ""
        ComboBox12.SelectedIndex = -1 : ComboBox13.SelectedIndex = -1

        TextBox16.Text = "" : TextBox17.Text = "" : TextBox18.Text = ""

        'ComboBox11.Items.Clear()
        'ComboBox11.Items.Add("Y")
        'ComboBox11.Items.Add("T")

        'ComboBox6.Items.Clear()
        'ComboBox6.Items.Add("Y") : ComboBox6.Items.Add("T")
        'ComboBox6.SelectedIndex = 0

        ComboBox7.Items.Clear()
        ComboBox7.Items.Add("Y") : ComboBox7.Items.Add("T")
        ComboBox7.SelectedIndex = 0

        ComboBox9.Items.Clear()
        ComboBox9.Items.Add("Y") : ComboBox9.Items.Add("T")
        ComboBox9.SelectedIndex = 0

        ComboBox10.Items.Clear()
        ComboBox10.Items.Add("Y") : ComboBox10.Items.Add("T")

        ComboBox3.Items.Clear()
        ComboBox3.Items.Add("Y") : ComboBox3.Items.Add("T")

        ComboBox9.Items.Clear()
        ComboBox9.Items.Add("Y") : ComboBox9.Items.Add("T")

        ComboBox15.SelectedIndex = -1
        ComboBox16.SelectedIndex = -1
        TextBox3.Text = ""
        TextBox4.Text = ""

        TextBox10.Text = ""
        TextBox11.Text = ""
        ListView10.Visible = False

        Txtket.Text = ""

        Button1.Text = "&Simpan" 'Button2.Enabled = False
        Button1.Enabled = True

        lvwSatuan.Visible = False

        TxtToleransiTimbangMin.Text = ""
        TxtToleransiTimbangMax.Text = ""
        TxtToleransiTFMin.Text = ""
        TxtToleransiTFMax.Text = ""
        TxtLifeTime.Text = ""

        Get_Kategori_Gudang()
    End Sub

    Private Sub LoadDataPengajuanBarangBaru()



        Try
            OpenConn()

            Dgv_Pengejuan_Barang_Baru.Rows.Clear()
            SQL = "select No_Transaksi, Tanggal, Jam, Kode_Group_Jenis, Klasifikasi_Bahan, Klasifikasi_Bahan_2,  "
            SQL &= $"kode_barang, Nama_Barang, Flag_Validasi_Procurement "
            SQL &= $"from N_EMI_Transaksi_Pengajuan_Barang_Baru "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Status is null "
            SQL &= $"and Flag_Validasi_Procurement is null "

            If Chk_TransaksiHrIni.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & "Tanggal between '" & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now.AddDays(1), "yyyy-MM-dd") & "' "
            End If

            If Chk_ParamTgl.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & ArrTanggal(Cmb_ParamTgl.SelectedIndex) & " between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value.AddDays(1), "yyyy-MM-dd") & "' "
            End If

            If Chk_ParamLain.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & ArrParamLain(Cmb_ParamLain.SelectedIndex) & " like '%" & Txt_ParamValue.Text & "%' "
            End If

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Pengejuan_Barang_Baru.Rows.Add(1)
                            Dgv_Pengejuan_Barang_Baru.Rows(i).Cells(item_Pengajuan_Notransaksi).Value = .Rows(i).Item("No_Transaksi")
                            Dgv_Pengejuan_Barang_Baru.Rows(i).Cells(item_Pengajuan_Tanggal).Value = Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy")
                            Dgv_Pengejuan_Barang_Baru.Rows(i).Cells(item_Pengajuan_Jam).Value = .Rows(i).Item("Jam")
                            Dgv_Pengejuan_Barang_Baru.Rows(i).Cells(item_Pengajuan_Kode_Barang).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_Pengejuan_Barang_Baru.Rows(i).Cells(item_Pengajuan_Nama_Barang).Value = .Rows(i).Item("Nama_Barang")
                            Dgv_Pengejuan_Barang_Baru.Rows(i).Cells(item_Pengajuan_Group_Jenis).Value = .Rows(i).Item("Kode_Group_Jenis")
                            Dgv_Pengejuan_Barang_Baru.Rows(i).Cells(item_Pengajuan_Klasifikasi_Bahan).Value = If(General_Class.CekNULL(.Rows(i).Item("Klasifikasi_Bahan")) = "", "-", .Rows(i).Item("Klasifikasi_Bahan"))
                            Dgv_Pengejuan_Barang_Baru.Rows(i).Cells(item_Pengajuan_Klasifikasi_Bahan_2).Value = If(General_Class.CekNULL(.Rows(i).Item("Klasifikasi_Bahan_2")) = "", "-", .Rows(i).Item("Klasifikasi_Bahan_2"))
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
    Private Sub Get_Kategori_Gudang()

        Try
            OpenConn()
            Cmb_KategoriGudang.Items.Clear() : arrKategoriGudang.Clear()
            SQL = "Select * From "
            SQL = SQL & "emi_master_kategori_gudang "
            SQL = SQL & "order by id_master_kategori_gudang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_KategoriGudang.Items.Add(dr("keterangan")) : arrKategoriGudang.Add(dr("id_master_kategori_gudang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Perusahaan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Barang")
            'Base_Language.Get_Languages(Bahasa_Pilihan, "Ongkir")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

        ListView1.Columns.Add(Base_Language.Lang_Global_Kode_SO, 0, HorizontalAlignment.Center)
        ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 100, HorizontalAlignment.Center)
        ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 225, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 70, HorizontalAlignment.Left)
        'iniiii
        If boleh_lihat_global = True Then
            ListView1.Columns.Add("Good Stock", 80, HorizontalAlignment.Right)
        Else
            ListView1.Columns.Add("Good Stock", 0, HorizontalAlignment.Right)
        End If
        'iniiii
        ListView1.Columns.Add(Base_Language.Lang_Global_Harga_Beli, 90, HorizontalAlignment.Right)
        'ListView1.Columns.Add(text_hrg_jual_apa, 90, HorizontalAlignment.Right)
        'ListView1.Columns.Add("Harga Jual Reseller", 0, HorizontalAlignment.Right)
        ListView1.Columns.Add("Stock Min.", 70, HorizontalAlignment.Right)
        'ListView1.Columns.Add("Lemari", 70, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Bad Stock", 0, HorizontalAlignment.Right)

        'ListView1.Columns.Add("Pembeda", 100, HorizontalAlignment.Left)

        'ListView1.Columns.Add("Pakai Serial Number", 0, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Kode Supplier", 120, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Supplier", 200, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Flag PPN", 60, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Harga Jual Min", 90, HorizontalAlignment.Right).DisplayIndex = 6
        'ListView1.Columns.Add("Harga Jual Max", 90, HorizontalAlignment.Right).DisplayIndex = 8
        'ListView1.Columns.Add("Harga Jual Special", 90, HorizontalAlignment.Right).DisplayIndex = 9
        ListView1.Columns.Add(Base_Language.Lang_Global_Jenis, 120, HorizontalAlignment.Center)
        ListView1.Columns.Add(Base_Language.Lang_Global_Barang_Sendiri, 60, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_Kategori, 100, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_Aktif, 60, HorizontalAlignment.Center)
        ListView1.Columns.Add(Base_Language.Lang_Global_Flag_PPN, 60, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_Jenis_Gudang, 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kategori Gudang", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kategori QC", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Id Kategori QC", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kategori PO", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Id Kategori PO", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Klasifikasi Bahan", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Klasifikasi Bahan 2", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Id Kategori Bahan", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jenis Routing", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Id Routing", 0, HorizontalAlignment.Center)

        ListView1.View = View.Details

        lvwSatuan.Items.Clear()
        lvwSatuan.Columns.Add(Base_Language.Lang_Barang_Flag_Tampil_Display, 150, HorizontalAlignment.Center)
        lvwSatuan.Columns.Add(Base_Language.Lang_Global_Satuan, 150, HorizontalAlignment.Left)

        lvwSatuan.View = View.Details

        ListView10.Columns.Add("Kode Supplier", 150, HorizontalAlignment.Left)
        ListView10.Columns.Add("Nama Supplier", 288, HorizontalAlignment.Left)
        ListView10.View = View.Details

        ListView10.Location = New Point(961, 94) 'Point(161, 194)

        Cmb_ParamTgl.Items.Clear() : ArrTanggal.Clear()
        Cmb_ParamTgl.Items.Add("Tanggal") : ArrTanggal.Add("Tanggal")


        Cmb_ParamLain.Items.Clear() : Cmb_ParamLain.Text = "" : ArrParamLain.Clear()
        Cmb_ParamLain.Items.Add("No Transaksi") : ArrParamLain.Add("No_Transaksi")
        Cmb_ParamLain.Items.Add("Kode Barang") : ArrParamLain.Add("Kode_Barang")
        Cmb_ParamLain.Items.Add("Nama Barang") : ArrParamLain.Add("Nama_Barang")
        Cmb_ParamLain.Items.Add("Kode Group Jenis") : ArrParamLain.Add("Kode_Group_Jenis")
        Cmb_ParamLain.Items.Add("Klasifikasi Bahan") : ArrParamLain.Add("Klasifikasi_Bahan")
        Cmb_ParamLain.Items.Add("Klasifikasi Bahan 2") : ArrParamLain.Add("Klasifikasi_Bahan_2")

        Cari("Y")
        TextBox1.Focus()
    End Sub

    Private Sub Get_Data_Pengajuan(ByVal index As Integer)
        Lv_Pengajuan_NoTransaksi = Dgv_Pengejuan_Barang_Baru.Rows(index).Cells(item_Pengajuan_Notransaksi).Value
        Lv_Pengajuan_Tanggal = Dgv_Pengejuan_Barang_Baru.Rows(index).Cells(item_Pengajuan_Tanggal).Value
        Lv_Pengajuan_Jam = Dgv_Pengejuan_Barang_Baru.Rows(index).Cells(item_Pengajuan_Jam).Value
        Lv_Pengajuan_KdBarang = Dgv_Pengejuan_Barang_Baru.Rows(index).Cells(item_Pengajuan_Kode_Barang).Value
        Lv_Pengajuan_NmBarang = Dgv_Pengejuan_Barang_Baru.Rows(index).Cells(item_Pengajuan_Nama_Barang).Value
        Lv_Pengajuan_Group_Jenis = Dgv_Pengejuan_Barang_Baru.Rows(index).Cells(item_Pengajuan_Group_Jenis).Value
        Lv_Pengajuan_Klasifikasi_Bahan = Dgv_Pengejuan_Barang_Baru.Rows(index).Cells(item_Pengajuan_Klasifikasi_Bahan).Value
        Lv_Pengajuan_Klasifikasi_Bahan_2 = Dgv_Pengejuan_Barang_Baru.Rows(index).Cells(item_Pengajuan_Klasifikasi_Bahan_2).Value
    End Sub
    Private Sub Get_Lv_SatuanTurunan()
        If DgvSatuanTerpilih.Rows.Count = 0 Then Exit Sub

        arrSatuanTurunan.Clear()
        For i As Integer = 0 To DgvSatuanTerpilih.RowCount - 1
            arrSatuanTurunan.Add(DgvSatuanTerpilih.Rows(i).Cells(0).Value)
        Next

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Kosong()
        TextBox8.Text = ""
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then cmbSatuan.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then TextBox5.Focus()
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then TextBox5.Focus()
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    'Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
    '    If e.KeyChar = Chr(13) Then TextBox9.Focus()
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    'End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Chr(13) Then Button5_Click(TextBox8, e)
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        If ComboBox2.Text.Trim.Length = 0 Then Exit Sub
        If TextBox1.Text.Trim.Length = 0 Then Exit Sub

        Dim boleh_edit_hj As String = ""

        Try
            OpenConn()

            If CekButtonRole("edit_harga_jual") = "T" Then
                boleh_edit_hj = "T"
            Else
                boleh_edit_hj = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            Dim lokasi_pergudang As String = ""

            SQL = " select  b.Kode_Stock_Owner_Gudang from stock_owner a , Binding_Lokasi_Gudang b where "
            SQL = SQL & "  a.Kode_Stock_Owner = '" & ComboBox2.Text & "'    and b.Gudang_Default = 'Y'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    lokasi_pergudang = Dr("Kode_Stock_Owner_Gudang")
                End If
            End Using

            lvwSatuan.Items.Clear() : cmbSatuan.SelectedIndex = -1
            SQL = "Select b.kode_group_jenis, "
            SQL = SQL & "a.kode_barang, a.satuan, a.flag_potong_stok, isnull(a.standar_price,0) as standar_price, a.Nama, a.harga_beli, a.last_hpp, a.stock_minimum, a.kode_kategori, "
            SQL = SQL & "a.flag_ppn, a.flag_sendiri, a.berat, a.berat_kotor, a.Panjang, a.Lebar, a.Tinggi, a.Kode_Kategori_Besar, a.Kode_Kategori_Kecil, "
            SQL = SQL & "a.id_group_jenis, a.id_master_kategori_gudang, a.Jenis_Kemasan, a.Metode_Pengeluaran_Stok, a.Berat_Bags, a.Isi_Per_Bags, a.Id_Kategori_Gudang, a.aktif,"

            SQL = SQL & "isnull((select kode_kategori_gudang from emi_kategori_gudang x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.id_kategori_gudang = x.id_kategori_gudang),NULL) as kode_kategori_gudang, "
            SQL = SQL & "isnull((select keterangan from emi_master_kategori_gudang x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.id_master_kategori_gudang = x.Id_Master_Kategori_Gudang),NULL) as keterangan_master_kategori_gudang,"
            SQL = SQL & "isnull((select keterangan from Emi_Kategori_QC x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.ID_Kategori_QC = x.ID_Kategori_QC),NULL) as Keterangan,"
            SQL = SQL & "isnull((select keterangan from Emi_Kategori_PO x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.ID_Kategori_PO = x.ID_Kategori_PO),NULL) as KeteranganPO,"

            SQL = SQL & "isnull((select keterangan from Emi_Klasifikasi_Bahan x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.ID_Klasifikasi_Bahan = x.ID_Klasifikasi_Bahan),NULL) as KeteranganBhn, "
            SQL = SQL & "isnull((select keterangan from emi_klasifikasi_bahan2 x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.ID_Klasifikasi_Bahan2 = x.ID_Klasifikasi_Bahan2), null) as keterangan_Bhn2 , "

            SQL = SQL & "isnull((select keterangan from EMI_Master_Routing x where a.kode_perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.ID_Routing = x.ID_Routing),NULL) as Keterangan_Routing, a.keterangan as Ket_Barang, ISNULL(a.Toleransi_Timbang_Min,0) as Toleransi_Timbang_Min,  "
            SQL = SQL & "ISNULL(a.Toleransi_Timbang_Max,0) as Toleransi_Timbang_Max, isnull(a.Toleransi_Tf_Min,0) as Toleransi_Tf_Min, isnull(a.Toleransi_Tf_Max,0) as Toleransi_Tf_Max, isnull(a.Life_Time,0) as Life_Time,  "
            SQL = SQL & "isnull(a.Estimasi_Harga,0) as Estimasi_Harga, a.Mata_Uang_Estimasi_Harga "
            SQL = SQL & "From barang a, EMI_Group_Jenis b, emi_kategori_gudang c,Emi_Kategori_PO e, Emi_Klasifikasi_Bahan f Where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.id_group_jenis = b.id_group_jenis "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.kode_stock_owner = '" & lokasi_pergudang & "' and a.kode_barang = '" & TextBox1.Text.Trim & "' "
            'SQL = SQL & "a.kode_stock_owner = '" & lokasi_pergudang & "' and a.kode_barang = '" & ListView1.FocusedItem.SubItems(1).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dim kodeBarangTemp As String = ""
                    'Dim satuanBarang As String = ""

                    kodeBarangTemp = Dr("kode_barang")
                    'satuanBarang = Dr("satuan")

                    If General_Class.CekNULL(Dr("Id_Kategori_Gudang")) = "" Then
                        CmbJnsGudanng.SelectedIndex = -1
                    Else
                        CmbJnsGudanng.Text = Dr("kode_kategori_gudang")
                    End If

                    Button1.Text = "&Update" 'Button2.Enabled = True

                    If General_Class.CekNULL(Dr("Mata_Uang_Estimasi_Harga")) = "" Then
                        Cmb_Est_Harga_Mata_Uang.SelectedItem = "RP"
                        Txt_Est_Harga.Text = ""
                    Else
                        Cmb_Est_Harga_Mata_Uang.SelectedItem = Dr("Mata_Uang_Estimasi_Harga")
                        Txt_Est_Harga.Text = Dr("Estimasi_Harga")
                    End If

                    If General_Class.CekNULL(Dr("flag_potong_stok")) = "Y" Then
                        Cmb_FlagPotongStok.SelectedIndex = 0
                        txtStandarPrice.Text = ""
                    Else
                        Cmb_FlagPotongStok.SelectedIndex = 1
                        txtStandarPrice.Text = Dr("standar_price")

                    End If

                    TextBox2.Text = Dr("Nama")

                    If General_Class.CekNULL(Dr("satuan")) = "" Then
                        cmbSatuan.SelectedItem = -1
                    Else
                        cmbSatuan.Text = Dr("satuan")
                    End If
                    'cmbSatuan.Text = Dr("satuan")

                    If General_Class.CekNULL(Dr("harga_beli")) = "" Then
                        TextBox5.Text = ""
                    Else
                        TextBox5.Text = Dr("harga_beli")
                    End If
                    'TextBox5.Text = Dr("harga_beli")

                    If General_Class.CekNULL(Dr("last_hpp")) = "" Then
                        TextBox6.Text = 0
                    Else
                        TextBox6.Text = Dr("last_hpp")
                    End If


                    If General_Class.CekNULL(Dr("stock_minimum")) = "" Then
                        TextBox7.Text = ""
                    Else
                        TextBox7.Text = Dr("stock_minimum")
                    End If
                    'TextBox7.Text = Dr("stock_minimum")

                    ComboBox4.Text = General_Class.CekNULL(Dr("kode_kategori"))

                    'baru di komen
                    'If UserLevel = "2" Then 'Kasir
                    '    Button1.Enabled = False
                    '    'Button2.Enabled = False
                    'End If
                    '  ComboBox5.Text = Dr("kode_pembeda")

                    If General_Class.CekNULL(Dr("aktif")) = "" Then
                        ComboBox3.SelectedItem = -1
                    Else
                        ComboBox3.Text = Dr("aktif")
                    End If
                    'ComboBox3.Text = Dr("aktif")

                    If General_Class.CekNULL(Dr("flag_ppn")) = "" Then
                        ComboBox9.SelectedItem = -1
                    Else
                        ComboBox9.Text = Dr("flag_ppn")
                    End If
                    'ComboBox9.Text = Dr("flag_ppn")

                    '   ComboBox6.Text = Dr("pakai_sn")

                    If General_Class.CekNULL(Dr("flag_sendiri")) = "" Then
                        ComboBox10.SelectedItem = -1
                    Else
                        ComboBox10.Text = Dr("flag_sendiri")
                    End If
                    'ComboBox10.Text = Dr("flag_sendiri")


                    'TextBox10.Text = Dr("kode_supplier")
                    'TextBox11.Text = Dr("nama_supplier")

                    If General_Class.CekNULL(Dr("berat")) = "" Then
                        TextBox12.Text = 0
                    Else
                        TextBox12.Text = Dr("berat")
                    End If

                    If General_Class.CekNULL(Dr("berat_kotor")) = "" Then
                        TextBox15.Text = 0
                    Else
                        TextBox15.Text = Dr("berat_kotor")
                    End If

                    ''ComboBox11.Text = Dr("input_csi")

                    If General_Class.CekNULL(Dr("panjang")) = "" Then
                        TextBox16.Text = 0
                    Else
                        TextBox16.Text = Dr("Panjang")
                    End If

                    If General_Class.CekNULL(Dr("lebar")) = "" Then
                        TextBox17.Text = 0
                    Else
                        TextBox17.Text = Dr("lebar")
                    End If

                    If General_Class.CekNULL(Dr("Tinggi")) = "" Then
                        TextBox18.Text = 0
                    Else
                        TextBox18.Text = Dr("Tinggi")
                    End If

                    If General_Class.CekNULL(Dr("Kode_Kategori_Besar")) = "" Then
                        ComboBox12.SelectedIndex = -1
                    Else
                        ComboBox12.Text = Dr("Kode_Kategori_Besar")
                    End If

                    If General_Class.CekNULL(Dr("Kode_Kategori_Kecil")) = "" Then
                        ComboBox13.SelectedIndex = -1
                    Else
                        ComboBox13.Text = Dr("Kode_Kategori_Kecil")
                    End If

                    If General_Class.CekNULL(Dr("id_group_jenis")) = "" Then
                        cmbJenis.SelectedIndex = -1
                    Else
                        cmbJenis.Text = Dr("kode_group_jenis")
                    End If

                    If General_Class.CekNULL(Dr("id_master_kategori_gudang")) = "" Then
                        Cmb_KategoriGudang.SelectedIndex = -1
                    Else
                        Cmb_KategoriGudang.Text = General_Class.CekNULL(Dr("keterangan_master_kategori_gudang"))
                    End If

                    If General_Class.CekNULL(Dr("Keterangan")) = "" Then
                        ComboBox5.SelectedItem = -1
                    Else
                        ComboBox5.Text = Dr("keterangan")
                    End If

                    If General_Class.CekNULL(Dr("KeteranganPO")) = "" Then
                        ComboBox6.SelectedItem = -1
                    Else
                        ComboBox6.Text = Dr("keteranganPO")
                    End If

                    If General_Class.CekNULL(Dr("KeteranganBhn")) = "" Then
                        ComboBox11.SelectedItem = -1
                    Else
                        ComboBox11.Text = Dr("KeteranganBhn")
                    End If

                    If General_Class.CekNULL(Dr("keterangan_Bhn2")) = "" Then
                        ComboBox19.SelectedItem = -1
                    Else
                        ComboBox19.Text = (Dr("keterangan_Bhn2"))
                    End If

                    If General_Class.CekNULL(Dr("Keterangan_Routing")) = "" Then
                        ComboBox14.SelectedItem = -1
                    Else
                        ComboBox14.Text = Dr("Keterangan_Routing")
                    End If

                    '===============================
                    If General_Class.CekNULL(Dr("Jenis_Kemasan")) = "" Then
                        ComboBox15.SelectedItem = -1
                    Else
                        ComboBox15.Text = Dr("Jenis_Kemasan")
                    End If

                    If General_Class.CekNULL(Dr("Metode_Pengeluaran_Stok")) = "" Then
                        ComboBox16.SelectedItem = -1
                    Else
                        ComboBox16.Text = Dr("Metode_Pengeluaran_Stok")
                    End If

                    TextBox3.Text = General_Class.CekNULL(Dr("Berat_Bags"))
                    TextBox4.Text = General_Class.CekNULL(Dr("Isi_Per_Bags"))

                    Txtket.Text = General_Class.CekNULL(Dr("ket_barang"))

                    TextBox1.Text = Dr("kode_barang")

                    If General_Class.CekNULL(Dr("Toleransi_Timbang_Min")) = "" Then
                        TxtToleransiTimbangMin.Text = ""
                    Else
                        TxtToleransiTimbangMin.Text = Dr("Toleransi_Timbang_Min")
                    End If
                    'TxtToleransiTimbangMin.Text = Dr("Toleransi_Timbang_Min")

                    If General_Class.CekNULL(Dr("Toleransi_Timbang_Max")) = "" Then
                        TxtToleransiTimbangMax.Text = ""
                    Else
                        TxtToleransiTimbangMax.Text = Dr("Toleransi_Timbang_Max")
                    End If
                    'TxtToleransiTimbangMax.Text = Dr("Toleransi_Timbang_Max")

                    If General_Class.CekNULL(Dr("Toleransi_Tf_Min")) = "" Then
                        TxtToleransiTFMin.Text = ""
                    Else
                        TxtToleransiTFMin.Text = Dr("Toleransi_Tf_Min")
                    End If
                    'TxtToleransiTFMin.Text = Dr("Toleransi_Tf_Min")

                    If General_Class.CekNULL(Dr("Toleransi_Tf_Max")) = "" Then
                        TxtToleransiTFMax.Text = ""
                    Else
                        TxtToleransiTFMax.Text = Dr("Toleransi_Tf_Max")
                    End If
                    'TxtToleransiTFMax.Text = Dr("Toleransi_Tf_Max")

                    If General_Class.CekNULL(Dr("Life_Time")) = "" Then
                        TxtLifeTime.Text = ""
                    Else
                        TxtLifeTime.Text = Dr("Life_Time")
                    End If
                    'TxtLifeTime.Text = Dr("Life_Time")

                    Dr.Close()

                    OpenConn()

                    DgvSatuanTerpilih.Rows.Clear()
                    Dim rows As Integer = 0
                    SQL = "Select a.Satuan,a.Nilai,a.Flag_Dasar, a.Flag_Default from N_EMI_Master_Satuan a , EMI_Satuan_Detail_Perhitungan b where   "
                    SQL = SQL & " a.Kode_Perusahaan = b.Kode_Perusahaan And a.Satuan = b.Satuan_Akhir And Kode_barang = '" & kodeBarangTemp & "' "
                    'SQL = SQL & "and satuan_awal = '" & satuanBarang & "' "
                    SQL = SQL & "group by a.Satuan,a.Nilai,a.Flag_Dasar,a.Flag_Default "
                    Using Dr2 = OpenTrans(SQL)
                        Do While Dr2.Read
                            DgvSatuanTerpilih.Rows.Add(1)
                            DgvSatuanTerpilih.Rows(rows).Cells(0).Value = Dr2("satuan")
                            DgvSatuanTerpilih.Rows(rows).Cells(1).Value = Dr2("nilai")

                            If General_Class.CekNULL(Dr2("flag_default")) <> "" Then
                                DgvSatuanTerpilih.Rows(rows).Cells(2).Value = True
                            End If

                            If General_Class.CekNULL(Dr2("flag_dasar")) = "Y" Then
                                DgvSatuanTerpilih.Rows(rows).Cells(1).ReadOnly = True
                                DgvSatuanTerpilih.Rows(rows).Cells(1).Style.BackColor = Color.Yellow
                            End If

                            rows = rows + 1
                        Loop
                    End Using


                    'SQL = "Select a.Satuan,a.Jumlah,a.Flag_Tampil_Display, a.Flag_Kirim, b. Flag_General from Barang_Detail_Satuan a , EMI_Satuan_Detail_Perhitungan b where   "
                    'SQL = SQL & " a.Kode_Perusahaan = b.Kode_Perusahaan And a.Satuan = b.Satuan_Akhir And Kode_barang = '" & kodeBarangTemp & "' "
                    'SQL = SQL & "and satuan_awal = '" & satuanBarang & "' "
                    'SQL = SQL & "group by a.Satuan,a.Jumlah,a.Flag_Tampil_Display,b. Flag_General, a.Flag_Kirim "
                    'Using Dr2 = OpenTrans(SQL)
                    '    Do While Dr2.Read
                    '        DgvSatuanTerpilih.Rows.Add(1)
                    '        DgvSatuanTerpilih.Rows(rows).Cells(0).Value = Dr2("satuan")
                    '        DgvSatuanTerpilih.Rows(rows).Cells(1).Value = Dr2("jumlah")

                    '        If General_Class.CekNULL(Dr2("flag_tampil_display")) <> "" Then
                    '            DgvSatuanTerpilih.Rows(rows).Cells(2).Value = True
                    '        End If

                    '        If General_Class.CekNULL(Dr2("flag_kirim")) <> "" Then
                    '            DgvSatuanTerpilih.Rows(rows).Cells(4).Value = True
                    '        End If

                    '        If General_Class.CekNULL(Dr2("flag_general")) = "" Then
                    '            DgvSatuanTerpilih.Rows(rows).Cells(3).Value = "T"
                    '        ElseIf Dr2("flag_general") = "T" Then
                    '            DgvSatuanTerpilih.Rows(rows).Cells(3).Value = "T"
                    '        Else
                    '            DgvSatuanTerpilih.Rows(rows).Cells(3).Value = "Y"
                    '        End If

                    '        'If General_Class.CekNULL(Dr2("flag_General")) = "Y" Then
                    '        '    DgvSatuanTerpilih.Rows(rows).Cells(1).ReadOnly = True
                    '        '    DgvSatuanTerpilih.Rows(rows).Cells(1).Style.BackColor = Color.Red
                    '        'End If

                    '        rows = rows + 1
                    '    Loop
                    'End Using

                    'For i As Integer = 0 To lvwSatuan.Items.Count - 1
                    '    SQL = "select flag_tampil_display from Barang_Detail_Satuan where kode_perusahaan = '" & KodePerusahaan & "'  "
                    '    SQL = SQL & "and kode_barang = '" & kodeBarangTemp & "' and satuan = '" & lvwSatuan.Items(i).SubItems(1).Text & "'"
                    '    Using Dr2 = OpenTrans(SQL)
                    '        If Dr2.Read Then
                    '            If General_Class.CekNULL(Dr2("flag_tampil_display")) <> "" Then
                    '                lvwSatuan.Items(i).Checked = True
                    '            End If

                    '        End If
                    '    End Using
                    'Next

                    ListView10.Visible = False
                    TextBox2.Focus()
                Else
                    TextBox2.Text = "" : cmbSatuan.SelectedIndex = -1 : TextBox5.Text = ""

                    TextBox6.Text = "" : TextBox7.Text = "" : ComboBox3.SelectedIndex = 0
                    ComboBox4.SelectedIndex = -1
                    TextBox10.Text = "" : TextBox11.Text = "" : ComboBox10.SelectedIndex = -1
                    ComboBox9.SelectedIndex = 0
                    TextBox12.Text = "" : TextBox15.Text = ""

                    ComboBox5.SelectedIndex = -1 : ComboBox14.SelectedIndex = -1
                    ComboBox15.SelectedIndex = -1 : ComboBox16.SelectedIndex = -1

                    TextBox3.Text = "" : TextBox4.Text = ""

                    TextBox16.Text = "" : TextBox17.Text = "" : TextBox18.Text = ""
                    '  ComboBox11.SelectedIndex = -1
                    ComboBox12.SelectedIndex = -1 : ComboBox13.SelectedIndex = -1

                    Txtket.Text = ""
                    'cmbJenis.SelectedIndex = -1

                    Button1.Text = "&Simpan" 'Button2.Enabled = False
                    Button1.Enabled = True
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Get_Lv_SatuanTurunan()
        If arrSatuanTurunan.Count > 0 Then

            ComboBox18.Items.Clear()
            For Each Satuan As String In arrSatuanTurunan
                ComboBox18.Items.Add(Satuan)
            Next

            ComboBox18.SelectedIndex = 0

        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If cmbJenis.Text.Trim.Length = 0 Then
            MessageBox.Show("Jenis barang Belum di pilih . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbJenis.Focus() : Exit Sub
        ElseIf ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Kode_Stock_Owner, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Kode_Barang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Nama_Barang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        ElseIf cmbSatuan.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Satuan_Barang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbSatuan.Focus() : Exit Sub
            'ElseIf TextBox4.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox4.Focus() : Exit Sub
        ElseIf TextBox5.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Harga_Barang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox5.Focus() : Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Stock_Min, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus() : Exit Sub

        ElseIf ComboBox4.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Kategori, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Status_Aktif, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub
        ElseIf ComboBox9.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Flag_PPN, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox9.Focus() : Exit Sub
            'ElseIf ComboBox5.SelectedIndex = -1 Then
            '    MessageBox.Show("Pembeda harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBox5.Focus() : Exit Sub
            'ElseIf ComboBox6.SelectedIndex = -1 Then
            '   MessageBox.Show("Pakai serial number harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '  ComboBox6.Focus() : Exit Sub
            'ElseIf TextBox10.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Supplier harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox10.Focus() : Exit Sub
        ElseIf ComboBox10.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Barang_Sendiri, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox10.Focus() : Exit Sub
        ElseIf TextBox12.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Berat_Bersih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox12.Focus() : Exit Sub
        ElseIf TextBox15.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Berat_Kotor, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox15.Focus() : Exit Sub
        ElseIf TextBox16.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Panjang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox16.Focus() : Exit Sub
        ElseIf TextBox17.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Lebar, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox17.Focus() : Exit Sub
        ElseIf TxtToleransiTFMin.Text.Length = 0 Then
            MessageBox.Show("Toleransi Timbang Min Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox18.Focus() : Exit Sub
        ElseIf TxtToleransiTFMax.Text.Length = 0 Then
            MessageBox.Show("Toleransi Timbang Max Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox18.Focus() : Exit Sub
        ElseIf TxtToleransiTFMin.Text.Length = 0 Then
            MessageBox.Show("Toleransi TF Min Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox18.Focus() : Exit Sub
        ElseIf TxtToleransiTFMax.Text.Length = 0 Then
            MessageBox.Show("Toleransi TF Max Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox18.Focus() : Exit Sub
        ElseIf TxtLifeTime.Text.Length = 0 Then
            MessageBox.Show("Life Time Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox18.Focus() : Exit Sub
        ElseIf TextBox18.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Tinggi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox18.Focus() : Exit Sub
        ElseIf ComboBox12.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Kategori_Besar, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox12.Focus() : Exit Sub
        ElseIf ComboBox13.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Kategori_Kecil, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox13.Focus() : Exit Sub
        ElseIf CmbJnsGudanng.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Jenis_Gudang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbJnsGudanng.Focus() : Exit Sub
            'ElseIf ComboBox11.SelectedIndex = -1 Then
            '    MessageBox.Show("Input CSI harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBox11.Focus() : Exit Sub
        ElseIf Cmb_KategoriGudang.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_KategoriGudang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_KategoriGudang.Focus() : Exit Sub
        ElseIf ComboBox5.SelectedIndex = -1 Then
            MessageBox.Show("Kategori QC harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox5.Focus() : Exit Sub
        ElseIf ComboBox6.SelectedIndex = -1 Then
            MessageBox.Show("Kategori PO harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus() : Exit Sub
        ElseIf ComboBox14.SelectedIndex = -1 Then
            MessageBox.Show("Routing harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox14.Focus() : Exit Sub
        ElseIf ComboBox15.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Kemasan harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox15.Focus() : Exit Sub
        ElseIf ComboBox16.SelectedIndex = -1 Then
            MessageBox.Show("Metode Pengeluaran Stok harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox16.Focus() : Exit Sub
        ElseIf Cmb_Est_Harga_Mata_Uang.SelectedIndex = -1 Then
            MessageBox.Show("Mata Uang Estimasi Harga harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Est_Harga_Mata_Uang.Focus() : Exit Sub
        ElseIf Txt_Est_Harga.Text.Trim.Length = 0 Then
            MessageBox.Show("Estimasi Harga harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Est_Harga.Focus() : Exit Sub
        End If

        If arrGudangRawMaterial.Contains(arrJenisBarang(cmbJenis.SelectedIndex)) Then
            If ComboBox11.SelectedIndex = -1 Then
                MessageBox.Show("Klasifikasi Bahan harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox11.Focus() : Exit Sub
            End If
        End If

        If Cmb_FlagPotongStok.SelectedIndex = -1 Then
            MessageBox.Show("Flag Potong Stok harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox16.Focus() : Exit Sub
        End If

        If Cmb_FlagPotongStok.SelectedIndex = 1 Then
            If txtStandarPrice.Text.Trim.Length = 0 Then
                MessageBox.Show("Standar Price harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox16.Focus() : Exit Sub
            End If
        End If

        Dim cekDIsplayYangDIpilih As Integer = 0
        Dim cekkirimYangDIpilih As Integer = 0
        Dim hasCheckFlagDefault As Boolean = False
        For i As Integer = 0 To DgvSatuanTerpilih.Rows.Count - 1

            'If DgvSatuanTerpilih.Rows(i).Cells(3).Value = "T" And DgvSatuanTerpilih.Rows(i).Cells(1).Value < 1 Then
            '    MessageBox.Show(Base_Language.Lang_Barang_Err_Nilai_Pengali1 & " " & DgvSatuanTerpilih.Rows(i).Cells(0).Value & " " & Base_Language.Lang_Barang_Err_Nilai_Pengali2)
            '    Exit Sub
            'End If

            If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
                hasCheckFlagDefault = True
            End If
            If DgvSatuanTerpilih.Rows(i).Cells(1).Value <= 0 Then
                MessageBox.Show(Base_Language.Lang_Barang_Err_Nilai_Pengali1 & " " & DgvSatuanTerpilih.Rows(i).Cells(0).Value & " " & Base_Language.Lang_Barang_Err_Nilai_Pengali2)
                Exit Sub
            End If

            If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
                cekDIsplayYangDIpilih = cekDIsplayYangDIpilih + 1
            End If

            If DgvSatuanTerpilih.Rows(i).Cells(4).Value = True Then
                cekkirimYangDIpilih = cekkirimYangDIpilih + 1
            End If
        Next

        If Not hasCheckFlagDefault Then
            MessageBox.Show("Harap Pilih Minimal 1 Satuan Default", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If cekDIsplayYangDIpilih <> 1 Then
            MessageBox.Show(Base_Language.Lang_Barang_Err_Flag_Tampil_Display)
            Exit Sub
        End If

        'If cekkirimYangDIpilih <> 1 Then
        '    MessageBox.Show(Base_Language.Lang_Barang_Err_Flag_Tampil_Display)
        '    Exit Sub
        'End If
        ' ComboBox5.SelectedIndex = 0

        get_jam()

        Try

            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            Dim Msg As String = ""


            '=======================================
            '=     CEK APAKAH BARANG PENGAJUAN     =
            '=======================================
            Dim IsRequestNewMaterial As Boolean = False
            SQL = "select top 1 Flag_Request_Barang_Baru, * from barang "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Kode_Barang = '{TextBox1.Text.Trim}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Request_Barang_Baru")) = "Y" Then
                        IsRequestNewMaterial = True
                    Else
                        IsRequestNewMaterial = False
                    End If

                End If
            End Using

            If Button1.Text = "&Simpan" Then

                If arrGudangRawMaterial.Contains(arrJenisBarang(cmbJenis.SelectedIndex)) Then

                    Dim No_Urut As String

                    SQL = "select top(1) substring(kode_barang, 5, 3) as no_urut from barang where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "id_klasifikasi_bahan = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "' and "
                    SQL = SQL & "id_klasifikasi_bahan2 = '" & arrId_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & "' "
                    SQL = SQL & "order by no_urut desc"

                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            'If IsDBNull(Dr("No_Urut")) Then
                            '    No_Urut = "001"
                            'Else
                            '   
                            'End If
                            No_Urut = Format(Val(Dr("No_Urut")) + 1, "000")
                        Else
                            No_Urut = "001"
                        End If
                        TextBox1.Text = arrprefix_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & arrprefix_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & No_Urut
                    End Using

                End If

                Dim lokasi_pergudang As String = ""

                SQL = " select  b.Kode_Stock_Owner_Gudang from stock_owner a , Binding_Lokasi_Gudang b where "
                SQL = SQL & "  a.Kode_Stock_Owner = '" & ComboBox2.Text & "'    and b.Gudang_Default = 'Y'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        lokasi_pergudang = Dr("Kode_Stock_Owner_Gudang")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Barang_Err_Lokasi_Sudah_Ada)
                        Exit Sub
                    End If
                End Using

                SQL = "select kode_barang from  barang where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & lokasi_pergudang & "' and "
                SQL = SQL & "kode_barang = '" & TextBox1.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Barang_Err_Kode_Barang_Sudah_Ada, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        Dr.Close()
                    End If
                End Using

                SQL = "select kode_stock_owner from View_Lokasi_Stock where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y'  "
                SQL = SQL & "order by kode_stock_owner"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                SQL = "insert into barang(Kode_Perusahaan,Kode_Stock_Owner,Kode_Barang,Kode_Barang_Inq,Nama,Nama_Inq,Satuan, "
                                SQL = SQL & "harga_beli,last_hpp,Stock_Minimum,aktif,Kode_Kategori,barang_sendiri ,id_group_jenis,Flag_PPN,berat_kotor,berat,Panjang, "
                                SQL = SQL & "Lebar,Tinggi,Kode_Kategori_Besar,Kode_Kategori_Kecil, id_kategori_gudang, id_master_kategori_gudang,ID_Kategori_QC,ID_Kategori_PO"
                                If ComboBox11.SelectedIndex = -1 Then
                                Else
                                    SQL = SQL & ",ID_Klasifikasi_Bahan"
                                End If
                                If ComboBox19.SelectedIndex = -1 Then
                                Else
                                    SQL = SQL & ",ID_Klasifikasi_Bahan2"
                                End If
                                SQL = SQL & ",ID_Routing, Jenis_Kemasan, Metode_Pengeluaran_Stok, Berat_Bags, Satuan_Berat_Bags, Isi_Per_Bags, Satuan_Isi_Bags,flag_potong_stok,standar_price, "
                                SQL = SQL & "Keterangan, Toleransi_Timbang_Min, Toleransi_Timbang_Max, Toleransi_Tf_Min, Toleransi_Tf_Max, Life_Time, Mata_Uang_Estimasi_Harga, Estimasi_Harga ) "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("kode_stock_owner") & "', "

                                SQL = SQL & "'" & TextBox1.Text.Trim & "', '" & TextBox1.Text.Trim & "', "
                                SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & TextBox2.Text.Trim & "', "
                                SQL = SQL & "'" & cmbSatuan.Text.Trim & "', '" & TextBox5.Text & "', 0, "
                                SQL = SQL & "'" & TextBox7.Text & "','" & ComboBox3.Text & "', '" & ComboBox4.Text & "', "
                                SQL = SQL & "'Y', '" & arrJenisBarang.Item(cmbJenis.SelectedIndex) & "', "
                                SQL = SQL & "'" & ComboBox9.Text & "', '" & TextBox15.Text & "' , '" & TextBox12.Text & "', "
                                SQL = SQL & "'" & TextBox16.Text & "', '" & TextBox17.Text & "','" & TextBox18.Text & "', "
                                SQL = SQL & "'" & ComboBox12.Text & "', '" & ComboBox13.Text & "', '" & arrJenisGudang.Item(CmbJnsGudanng.SelectedIndex) & "', "
                                SQL = SQL & "'" & arrKategoriGudang.Item(Cmb_KategoriGudang.SelectedIndex) & "','" & arrId_kategori_qc.Item(ComboBox5.SelectedIndex) & "', "
                                SQL = SQL & "'" & arrId_kategori_PO.Item(ComboBox6.SelectedIndex) & "' "
                                If ComboBox11.SelectedIndex = -1 Then
                                Else
                                    SQL = SQL & ",'" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "' "
                                End If
                                If ComboBox19.SelectedIndex = -1 Then
                                Else
                                    SQL = SQL & ",'" & arrId_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & "' "
                                End If

                                SQL = SQL & ",'" & arrid_Routing.Item(ComboBox14.SelectedIndex) & "', "

                                SQL = SQL & "'" & ComboBox15.SelectedItem & "', '" & ComboBox16.SelectedItem & "', "
                                If TextBox3.Text = "" Or String.IsNullOrWhiteSpace(TextBox3.Text) Then
                                    SQL = SQL & "Null, "
                                Else
                                    SQL = SQL & "'" & TextBox3.Text & "', "
                                End If

                                SQL = SQL & "'" & ComboBox17.SelectedItem & "', "

                                If TextBox4.Text = "" Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                                    SQL = SQL & "Null, "
                                Else
                                    SQL = SQL & "'" & TextBox4.Text & "', "
                                End If

                                SQL = SQL & "'" & ComboBox18.SelectedItem & "', "
                                SQL = SQL & "'" & Cmb_FlagPotongStok.Text & "' ,"
                                SQL = SQL & "'" & txtStandarPrice.Text.Trim & "', '" & Txtket.Text & "',"

                                SQL = SQL & "'" & TxtToleransiTimbangMin.Text.Trim & "',"
                                SQL = SQL & "'" & TxtToleransiTimbangMax.Text.Trim & "',"
                                SQL = SQL & "'" & TxtToleransiTFMin.Text.Trim & "',"
                                SQL = SQL & "'" & TxtToleransiTFMax.Text.Trim & "',"
                                SQL = SQL & "'" & TxtLifeTime.Text.Trim & "', "
                                SQL = SQL & "'" & Cmb_Est_Harga_Mata_Uang.Text.Trim & "', "
                                SQL = SQL & "'" & Val(HilangkanTanda(Txt_Est_Harga.Text.Trim)) & "' "

                                SQL = SQL & ")"
                                ExecuteTrans(SQL)

                            Next

                            For i As Integer = 0 To DgvSatuanTerpilih.Rows.Count - 1

                                Dim checkFlag As String = ""
                                Dim checkFlagKirim As String = ""

                                If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
                                    checkFlag = "'Y'"
                                    checkFlagKirim = "'Y'"
                                Else
                                    checkFlag = "NULL"
                                    checkFlagKirim = "NULL"
                                End If

                                'If DgvSatuanTerpilih.Rows(i).Cells(4).Value = True Then
                                '    checkFlagKirim = "'Y'"
                                'Else
                                '    checkFlagKirim = "NULL"
                                'End If

                                Dim checkFlagDasar As String = ""
                                If DgvSatuanTerpilih.Rows(i).Cells(0).Value = cmbSatuan.Text Then
                                    checkFlagDasar = "'Y'"
                                Else
                                    checkFlagDasar = "NULL"
                                End If

                                If checkFlagDasar = "'Y'" Then
                                    SQL = "insert into Barang_Detail_Satuan(kode_perusahaan,kode_barang,satuan,flag_tampil_display,jumlah,Flag_Kirim) values("
                                    SQL = SQL & "'" & KodePerusahaan & "', '" & TextBox1.Text & "', '" & DgvSatuanTerpilih.Rows(i).Cells(0).Value & "',"
                                    SQL = SQL & "'Y', '" & DgvSatuanTerpilih.Rows(i).Cells(1).Value & "','Y')"
                                    ExecuteTrans(SQL)
                                End If

                                SQL = "INSERT INTO N_EMI_Master_Satuan (kode_perusahaan, kode_barang, barang, satuan, nilai, flag_dasar, flag_default) VALUES ("
                                SQL &= "'" & KodePerusahaan & "', "
                                SQL &= "'" & TextBox1.Text & "', "
                                SQL &= "'" & TextBox2.Text & "', "
                                SQL &= "'" & DgvSatuanTerpilih.Rows(i).Cells(0).Value & "', "
                                SQL &= DgvSatuanTerpilih.Rows(i).Cells(1).Value & ", "
                                SQL &= checkFlagDasar & ", "
                                SQL &= checkFlag & ")"
                                ExecuteTrans(SQL)

                            Next

                            'For i As Integer = 0 To lvwSatuan.Items.Count - 1

                            '    Dim checkFlag As String = ""

                            '    If lvwSatuan.Items(i).Checked = True Then
                            '        checkFlag = "'Y'"
                            '    Else
                            '        checkFlag = "NULL"
                            '    End If

                            '    SQL = "insert into Barang_Detail_Satuan(kode_perusahaan,kode_barang,satuan,flag_tampil_display) values("
                            '    SQL = SQL & "'" & KodePerusahaan & "', '" & TextBox1.Text & "', '" & lvwSatuan.Items(i).SubItems(1).Text & "',"
                            '    SQL = SQL & "" & checkFlag & ")"
                            '    ExecuteTrans(SQL)

                            'Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Barang_Err_Lokasi_Tidak_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                Msg = "Barang Berhasil Disimpan"
            Else

                If CekButtonRole("update_barang") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "Update a Set a.nama = '" & TextBox2.Text.Trim & "', "
                SQL = SQL & "a.satuan = '" & cmbSatuan.Text.Trim & "', "
                SQL = SQL & "a.harga_beli = '" & TextBox5.Text & "', "
                SQL = SQL & "a.stock_minimum = '" & TextBox7.Text & "', "
                SQL = SQL & "a.aktif = '" & ComboBox3.Text & "', "
                SQL = SQL & "a.flag_ppn = '" & ComboBox9.Text & "', "
                SQL = SQL & "a.kode_kategori = '" & ComboBox4.Text & "', "
                SQL = SQL & "a.id_group_jenis = '" & arrJenisBarang(cmbJenis.SelectedIndex) & "', "

                '  SQL = SQL & "a.flag_update = 'Y', "
                SQL = SQL & "a.flag_potong_stok = '" & Cmb_FlagPotongStok.Text & "', "
                SQL = SQL & "a.standar_price = '" & txtStandarPrice.Text & "', "
                SQL = SQL & "a.flag_sendiri = '" & ComboBox10.Text & "', "
                SQL = SQL & "a.berat = '" & TextBox12.Text.Trim & "', "
                SQL = SQL & "a.berat_Kotor = '" & TextBox15.Text.Trim & "', "
                SQL = SQL & "a.Panjang = '" & TextBox16.Text.Trim & "', "
                SQL = SQL & "a.Lebar = '" & TextBox17.Text.Trim & "', "
                SQL = SQL & "a.Tinggi = '" & TextBox18.Text.Trim & "', "
                SQL = SQL & "a.Kode_Kategori_Besar = '" & ComboBox12.Text.Trim & "', "
                SQL = SQL & "a.Kode_Kategori_Kecil = '" & ComboBox13.Text.Trim & "',"
                SQL = SQL & "a.id_kategori_gudang = '" & arrJenisGudang.Item(CmbJnsGudanng.SelectedIndex) & "',"
                SQL = SQL & "a.id_master_kategori_gudang = '" & arrKategoriGudang.Item(Cmb_KategoriGudang.SelectedIndex) & "',"
                SQL = SQL & "a.ID_Kategori_QC = '" & arrId_kategori_qc.Item(ComboBox5.SelectedIndex) & "', "
                SQL = SQL & "a.ID_Kategori_PO = '" & arrId_kategori_PO.Item(ComboBox6.SelectedIndex) & "', "
                SQL = SQL & "a.ID_Routing = '" & arrid_Routing.Item(ComboBox14.SelectedIndex) & "', "

                SQL = SQL & "a.Mata_Uang_Estimasi_Harga = '" & Cmb_Est_Harga_Mata_Uang.Text.Trim & "', "
                SQL = SQL & "a.Estimasi_Harga = '" & Val(HilangkanTanda(Txt_Est_Harga.Text.Trim)) & "', "




                If ComboBox11.SelectedIndex = -1 Then
                Else
                    SQL = SQL & "a.ID_Klasifikasi_Bahan = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "', "
                End If
                If ComboBox19.SelectedIndex = -1 Then
                Else
                    SQL = SQL & "a.ID_Klasifikasi_Bahan2 = '" & arrId_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & "', "
                End If

                SQL = SQL & "a.Jenis_Kemasan='" & ComboBox15.SelectedItem & "', "
                SQL = SQL & "a.Metode_Pengeluaran_Stok='" & ComboBox16.SelectedItem & "', "
                If TextBox3.Text = "" Or String.IsNullOrWhiteSpace(TextBox3.Text) Then
                    SQL = SQL & "a.Berat_Bags = Null, "
                Else
                    SQL = SQL & "a.Berat_Bags = '" & TextBox3.Text & "', "
                End If
                SQL = SQL & "a.Satuan_Berat_Bags='" & ComboBox17.SelectedItem & "', "

                If TextBox4.Text = "" Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                    SQL = SQL & "a.Isi_Per_Bags = Null, "
                Else
                    SQL = SQL & "a.Isi_Per_Bags = '" & TextBox4.Text & "', "
                End If

                SQL = SQL & "a.Satuan_Isi_Bags = '" & ComboBox18.SelectedItem & "', "
                SQL = SQL & "a.keterangan = '" & Txtket.Text & "',"
                ' SQL = SQL & "a.input_csi = '" & ComboBox11.Text & "', "


                If TxtToleransiTimbangMin.Text = "" Or String.IsNullOrWhiteSpace(TxtToleransiTimbangMin.Text) Then
                    SQL = SQL & "a.Toleransi_Timbang_Min = Null, "
                Else
                    SQL = SQL & "a.Toleransi_Timbang_Min = '" & TxtToleransiTimbangMin.Text & "', "
                End If

                If TxtToleransiTimbangMax.Text = "" Or String.IsNullOrWhiteSpace(TxtToleransiTimbangMax.Text) Then
                    SQL = SQL & "a.Toleransi_Timbang_Max = Null, "
                Else
                    SQL = SQL & "a.Toleransi_Timbang_Max = '" & TxtToleransiTimbangMax.Text & "', "
                End If

                If TxtToleransiTFMin.Text = "" Or String.IsNullOrWhiteSpace(TxtToleransiTFMin.Text) Then
                    SQL = SQL & "a.Toleransi_Tf_Min = Null, "
                Else
                    SQL = SQL & "a.Toleransi_Tf_Min = '" & TxtToleransiTFMin.Text & "', "
                End If

                If TxtToleransiTFMax.Text = "" Or String.IsNullOrWhiteSpace(TxtToleransiTFMax.Text) Then
                    SQL = SQL & "a.Toleransi_Tf_Max = Null, "
                Else
                    SQL = SQL & "a.Toleransi_Tf_Max = '" & TxtToleransiTFMax.Text & "', "
                End If

                If TxtLifeTime.Text = "" Or String.IsNullOrWhiteSpace(TxtLifeTime.Text) Then
                    SQL = SQL & "a.Life_Time = Null "
                Else
                    SQL = SQL & "a.Life_Time = '" & TxtLifeTime.Text & "'"
                End If

                '    SQL = SQL & "a.penentu_harga_csi = '" & TextBox5.Text & "' "
                SQL = SQL & "from barang a, View_Lokasi_Stock b where "
                SQL = SQL & "a.kode_perusahaan  = b.kode_perusahaan  and "
                SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.kode_barang = '" & TextBox1.Text.Trim & "' "

                ExecuteTrans(SQL)

                SQL = "delete barang_detail_satuan where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & TextBox1.Text & "' "
                ExecuteTrans(SQL)

                SQL = "delete N_EMI_Master_Satuan where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & TextBox1.Text & "' "
                ExecuteTrans(SQL)

                For i As Integer = 0 To DgvSatuanTerpilih.Rows.Count - 1

                    Dim checkFlagDefault As String = ""

                    If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
                        checkFlagDefault = "'Y'"
                    Else
                        checkFlagDefault = "NULL"
                    End If

                    'Dim checkFlagKirim As String = ""

                    'If DgvSatuanTerpilih.Rows(i).Cells(4).Value = True Then
                    '    checkFlagKirim = "'Y'"
                    'Else
                    '    checkFlagKirim = "NULL"
                    'End If

                    Dim checkFlagDasar As String = ""
                    Debug.WriteLine(DgvSatuanTerpilih.Rows(i).Cells(0).Value)
                    Debug.WriteLine(cmbSatuan.Text)
                    If DgvSatuanTerpilih.Rows(i).Cells(0).Value = cmbSatuan.Text Then
                        checkFlagDasar = "'Y'"
                    Else
                        checkFlagDasar = "NULL"
                    End If

                    If checkFlagDasar = "'Y'" Then
                        SQL = "insert into Barang_Detail_Satuan(kode_perusahaan,kode_barang,satuan,flag_tampil_display,jumlah,Flag_Kirim) values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & TextBox1.Text & "', '" & DgvSatuanTerpilih.Rows(i).Cells(0).Value & "',"
                        SQL = SQL & "'Y', '" & DgvSatuanTerpilih.Rows(i).Cells(1).Value & "','Y')"
                        ExecuteTrans(SQL)
                    End If

                    SQL = "INSERT INTO N_EMI_Master_Satuan (kode_perusahaan, kode_barang, barang, satuan, nilai, flag_dasar, flag_default) VALUES ("
                    SQL &= "'" & KodePerusahaan & "', "
                    SQL &= "'" & TextBox1.Text & "', "
                    SQL &= "'" & TextBox2.Text & "', "
                    SQL &= "'" & DgvSatuanTerpilih.Rows(i).Cells(0).Value & "', "
                    SQL &= DgvSatuanTerpilih.Rows(i).Cells(1).Value & ", "
                    SQL &= checkFlagDasar & ", "
                    SQL &= checkFlagDefault & ")"
                    ExecuteTrans(SQL)


                    'Dim checkFlag As String = ""

                    'If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
                    '    checkFlag = "'Y'"
                    'Else
                    '    checkFlag = "NULL"
                    'End If

                    'SQL = "select * from barang_detail_satuan where kode_perusahaan = '" & KodePerusahaan & "' "
                    'SQL = SQL & "and kode_barang = '" & TextBox1.Text & "' and satuan = '" & lvwSatuan.Items(i).SubItems(1).Text & "' "
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then
                    '        Dr.Close()
                    '        SQL = "update Barang_Detail_Satuan set flag_tampil_display = " & checkFlag & "  "
                    '        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '        SQL = SQL & "kode_barang = '" & TextBox1.Text & "' and satuan = '" & DgvSatuanTerpilih.Rows(i).Cells(0).ErrorText & "'"
                    '        ExecuteTrans(SQL)
                    '    Else
                    '        Dr.Close()
                    '        SQL = "insert into Barang_Detail_Satuan(kode_perusahaan,kode_barang,satuan,flag_tampil_display) values("
                    '        SQL = SQL & "'" & KodePerusahaan & "', '" & TextBox1.Text & "', '" & lvwSatuan.Items(i).SubItems(1).Text & "',"
                    '        SQL = SQL & "" & checkFlag & ")"
                    '        ExecuteTrans(SQL)

                    '    End If
                    'End Using

                Next

                'ListView1.FindItemWithText(TextBox1.Text.Trim).Selected = True

                'ListView1.FocusedItem.SubItems(2).Text = TextBox2.Text.Trim
                'ListView1.FocusedItem.SubItems(3).Text = cmbSatuan.Text.Trim
                'ListView1.FocusedItem.SubItems(5).Text = Format(Val(TextBox5.Text.Trim), "N0")
                ''ListView1.FocusedItem.SubItems(6).Text = Format(Val(TextBox6.Text.Trim), "N0")
                'ListView1.FocusedItem.SubItems(7).Text = Format(0, "N0")
                'ListView1.FocusedItem.SubItems(8).Text = Format(Val(TextBox7.Text.Trim), "N0")
                ''     ListView1.FocusedItem.SubItems(9).Text = TextBox9.Text.Trim
                'ListView1.FocusedItem.SubItems(11).Text = ComboBox4.Text.Trim
                ''   ListView1.FocusedItem.SubItems(12).Text = ComboBox5.Text.Trim
                'ListView1.FocusedItem.SubItems(13).Text = ComboBox3.Text.Trim
                ''  ListView1.FocusedItem.SubItems(14).Text = ComboBox6.Text.Trim
                'ListView1.FocusedItem.SubItems(15).Text = TextBox10.Text.Trim
                'ListView1.FocusedItem.SubItems(16).Text = TextBox11.Text.Trim
                'ListView1.FocusedItem.SubItems(17).Text = ComboBox9.Text

                Msg = "Barang Berhasil Diupdate"
            End If


            If IsRequestNewMaterial Then

                SQL = "select 1 from N_EMI_Transaksi_Pengajuan_Barang_Baru "
                SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                SQL &= $"and Kode_Barang = '{TextBox1.Text.Trim}' "
                SQL &= $"and Flag_Validasi_Procurement is null "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        SQL = $"update N_EMI_Transaksi_Pengajuan_Barang_Baru set Flag_Validasi_Procurement = 'Y', "
                        SQL &= $"Tanggal_Validasi_Procurement = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi_Procurement = '{Format(tgl_skg, "HH:mm:ss")}', "
                        SQL &= $"User_Validasi_Procurement = '{UserID}' "
                        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"and Kode_Barang = '{TextBox1.Text.Trim}' "
                        SQL &= $"and Flag_Validasi_Procurement is null "
                        ExecuteTrans(SQL)


                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Kode Barang {TextBox1.Text.Trim} Tidak Ditemukan pada Transaksi Pengajuan Barang Baru", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select Flag_Request_Barang_Baru "
                SQL &= $"from barang "
                SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                SQL &= $"and Kode_Barang = '{TextBox1.Text.Trim}' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        SQL = "update barang set Flag_Request_Barang_Baru = NULL "
                        SQL &= $"from barang "
                        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"and Kode_Barang = '{TextBox1.Text.Trim}' "
                        ExecuteTrans(SQL)

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Kode Barang {TextBox1.Text.Trim} Tidak Ditemukan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using



            End If




            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Msg, Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If Button1.Text.ToUpper = "&SIMPAN" Then
            TextBox8.Text = ""
            Cari("Y")
        End If

        Kosong()
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Try

                OpenConn()

                'Adjustment
                Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from adjustment where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TextBox1.Text.Trim & "'")
                    If Dr1.Read Then
                        MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data adjustment", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Kosong()
                        TextBox1.Focus()
                        CloseConn()
                        Exit Sub
                    End If
                End Using

                'Detail Penjualan
                Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from detail_penjualan where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TextBox1.Text.Trim & "'")
                    If Dr1.Read Then
                        MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data penjualan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Kosong()
                        TextBox1.Focus()
                        CloseConn()
                        Exit Sub
                    End If
                End Using

                'Detail_R_Penjualan
                Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from detail_r_penjualan where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TextBox1.Text.Trim & "'")
                    If Dr1.Read Then
                        MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data retur penjualan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Kosong()
                        TextBox1.Focus()
                        CloseConn()
                        Exit Sub
                    End If
                End Using

                'Detail Pembelian
                Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from detail_pembelian where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TextBox1.Text.Trim & "'")
                    If Dr1.Read Then
                        MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data pembelian", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Kosong()
                        TextBox1.Focus()
                        CloseConn()
                        Exit Sub
                    End If
                End Using

                'Detail_R_Pembelian
                Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from detail_r_pembelian where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TextBox1.Text.Trim & "'")
                    If Dr1.Read Then
                        MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data retur pembelian", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Kosong()
                        TextBox1.Focus()
                        CloseConn()
                        Exit Sub
                    End If
                End Using

                SQL = "Delete From barang_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TextBox1.Text.Trim & "'"
                ExecuteTrans(SQL)

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        Kosong()
        Cari("T")
        TextBox1.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox8.Focus()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Cari("T")
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub

        For i As Integer = 0 To ComboBox2.Items.Count - 1
            xSplit = ComboBox2.Items(i).split("-")
            If ListView1.FocusedItem.Text = xSplit(0).Trim Then
                ComboBox2.SelectedIndex = i
                Exit For
            End If
        Next

        TextBox1.Text = ListView1.FocusedItem.SubItems(1).Text
        'ComboBox2.Text = ListView1.FocusedItem.Text
        TextBox1_Leave(ListView1, e)
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        'Cari("T")
    End Sub

    Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox9.Focus()
    End Sub

    Private Sub TextBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then ComboBox10.Focus()
    End Sub

    Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox3.Focus()
    End Sub

    Private Sub ComboBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox6.Focus()
    End Sub

    Private Sub ComboBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox6.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub TextBox10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView10.Items.Count = 0 Then Exit Sub
            ListView10.Focus()
        End If
    End Sub

    Private Sub TextBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox10.Text.Trim.Length = 0 Then
                ListView10.Visible = False : TextBox11.Focus() : Exit Sub
            End If
            TextBox10_Leave(TextBox10, e)
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox10_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox10.Leave
        If TextBox10.Text.Trim.Length = 0 Then
            ListView10.Visible = False : Exit Sub
        Else
            ListView10.Visible = True
        End If
        If ListView10.Focused = True Then Exit Sub

        'If Button2.Text = "&Update" Then Exit Sub

        OpenConn()

        Dim Lanjut As Boolean = False
        SQL = "select kode_supplier, nama from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and kode_supplier = '" & Trim(TextBox10.Text) & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                TextBox10.Text = Dr("kode_supplier")
                TextBox11.Text = Dr("nama")
                Lanjut = True
                TextBox12.Focus()
            Else
                TextBox10.Text = ""
                TextBox11.Text = ""
                Lanjut = False
                TextBox11.Focus()
            End If
            ListView10.Visible = False
        End Using

        CloseConn()
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        If TextBox10.Text.Trim.Length = 0 Then
            ListView10.Visible = False : Exit Sub
        Else
            ListView10.Visible = True
        End If

        Try
            OpenConn()

            ListView10.Items.Clear()
            Dim Lvw As ListViewItem

            SQL = "select kode_supplier, Nama, Alamat, Telepon, fax from suppliers where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_supplier like '%" & Trim(TextBox10.Text) & "%' order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lvw = ListView10.Items.Add(Dr("kode_supplier"))
                    Lvw.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView10.Items.Count = 0 Then Exit Sub
            ListView10.Focus()
        End If
    End Sub

    Private Sub TextBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox10.Text.Trim.Length = 0 Then TextBox11.Text = "" : ListView10.Visible = False ': Exit Sub
            TextBox12.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox11_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox11.Leave
        If ListView10.Focused = True Then Exit Sub
        TextBox10.Text = "" : TextBox11.Text = ""
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
        If TextBox11.Text.Trim.Length = 0 Then
            ListView10.Visible = False : Exit Sub
        Else
            ListView10.Visible = True
        End If

        Try

            OpenConn()

            ListView10.Items.Clear()
            Dim Lvw As ListViewItem

            SQL = "select kode_supplier, Nama, Alamat, Telepon, fax from suppliers where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & Trim(TextBox11.Text) & "%' order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lvw = ListView10.Items.Add(Dr("kode_supplier"))
                    Lvw.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView10_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView10.DoubleClick
        If ListView10.Items.Count = 0 Then Exit Sub
        TextBox10.Text = ListView10.FocusedItem.Text

        TextBox10.Focus()
        TextBox12.Focus()

        ListView10.Visible = False
    End Sub

    Private Sub ListView10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView10.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView10_DoubleClick(ListView10, e)
        End If
    End Sub

    Private Sub ListView10_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView10.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox9.KeyPress
        If e.KeyChar = Chr(13) Then TextBox12.Focus()
    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblKategori.Click

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged

    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblStatusAktif.Click

    End Sub

    Private Sub ComboBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox10.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox4.Focus()
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox12_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox12.KeyPress
        If e.KeyChar = Chr(13) Then TextBox15.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar <= Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    'Private Sub TextBox15_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox15.KeyPress
    '    If e.KeyChar = Chr(13) Then TextBox13.Focus()
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar <= Chr(Asc("."))) Then e.KeyChar = Chr(0)
    'End Sub

    Private Sub TextBox16_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox16.KeyPress
        If e.KeyChar = Chr(13) Then TextBox17.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox17_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox17.KeyPress
        If e.KeyChar = Chr(13) Then TextBox18.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox18_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox18.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox12.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox12.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox13.Focus()
    End Sub

    Private Sub ComboBox12_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox12.SelectedIndexChanged

        Try
            OpenConn()
            ComboBox13.Items.Clear()
            SQL = "Select Kode_Kategori_Kecil From Kategori_Kecil where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Kategori_Besar ='" & ComboBox12.Text & "' order by Kode_Kategori_Besar"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox13.Items.Add(dr("Kode_Kategori_Kecil"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub cmbSatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSatuan.KeyPress
        If e.KeyChar = Chr(13) Then Txtket.Focus()
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Chr(13) Then cmbJenis.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub cmbJenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbJenis.KeyPress
        If e.KeyChar = Chr(13) Then
            If ComboBox11.Enabled = True Then
                ComboBox11.Focus()
            Else
                TextBox1.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox15_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox15.KeyPress
        If e.KeyChar = Chr(13) Then TextBox16.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox13.KeyPress
        If e.KeyChar = Chr(13) Then CmbJnsGudanng.Focus()
    End Sub

    Private Sub CmbJnsGudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbJnsGudanng.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox10.Focus()
        ' If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles btnPilihSatuan.Click
        SD_Pilih_Satuan_Turunan.txtSatuan.Text = cmbSatuan.Text
        SD_Pilih_Satuan_Turunan.ShowDialog()
    End Sub

    Private Sub DgvSatuanTerpilih_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSatuanTerpilih.CellEndEdit
        Dim currentRow = DgvSatuanTerpilih.CurrentRow.Index
        Dim currentCell = DgvSatuanTerpilih.CurrentCellAddress.X

        If currentCell = 1 Then
            If IsNumeric(DgvSatuanTerpilih.Rows(currentRow).Cells(1).Value) = False Or Val(DgvSatuanTerpilih.Rows(currentRow).Cells(1).Value) < 0 Then
                DgvSatuanTerpilih.Rows(currentRow).Cells(1).Value = 0

                Exit Sub
            End If

        End If

        If DgvSatuanTerpilih.CurrentRow.Cells(2).Value = True Then
            For i As Integer = 0 To DgvSatuanTerpilih.Rows.Count - 1
                If i <> currentRow Then
                    If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
                        DgvSatuanTerpilih.Rows(i).Cells(2).Value = False
                    End If
                End If
            Next
        Else
            If DgvSatuanTerpilih.Rows.Count = 1 Then
                MessageBox.Show("Satuan Default Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DgvSatuanTerpilih.Rows(0).Cells(2).Value = True
                Exit Sub
            End If
        End If

    End Sub

    Private Sub DgvSatuanTerpilih_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvSatuanTerpilih.KeyDown
        If DgvSatuanTerpilih.Rows.Count = 0 Or DgvSatuanTerpilih.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DgvSatuanTerpilih.CurrentRow.Index
        Dim currentCell = DgvSatuanTerpilih.CurrentCellAddress.X

        If e.KeyCode = Keys.Delete Then
            If Not DgvSatuanTerpilih.Rows.Count = 0 Then

                If DgvSatuanTerpilih.Rows(currentRow).Cells(0).Value = cmbSatuan.Text Then
                    MessageBox.Show("Satuan awal tidak boleh dihapus ..!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                BeginInvoke(New MethodInvoker(Sub() DgvSatuanTerpilih.Rows.RemoveAt(currentRow)))

                If DgvSatuanTerpilih.Rows.Count = 2 Then
                    DgvSatuanTerpilih.Rows(0).Cells(2).Value = True
                End If

            End If
        End If
    End Sub

    Private Sub cmbSatuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSatuan.SelectedIndexChanged

        If cmbSatuan.SelectedIndex <> -1 Then
            btnPilihSatuan.Enabled = True

            Try
                OpenConn()

                DgvSatuanTerpilih.Rows.Clear()
                SQL = "select satuan_akhir,nilai_pengali, flag_general from EMI_Satuan_Detail_Perhitungan where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and satuan_awal = '" & cmbSatuan.Text & "' and satuan_akhir = '" & cmbSatuan.Text & "'  and jenis = 'masa' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        DgvSatuanTerpilih.Rows.Add(1)
                        DgvSatuanTerpilih.Rows(0).Cells(0).Value = Dr("satuan_akhir")
                        'DgvSatuanTerpilih.Rows(0).Cells(1).Value = Dr("nilai_pengali")
                        DgvSatuanTerpilih.Rows(0).Cells(1).Value = 1
                        DgvSatuanTerpilih.Rows(0).Cells(3).Value = Dr("flag_general")

                        If Dr("satuan_akhir").ToString.ToUpper = cmbSatuan.Text.ToUpper Then
                            DgvSatuanTerpilih.Rows(0).Cells(1).ReadOnly = True
                        Else
                            DgvSatuanTerpilih.Rows(0).Cells(1).ReadOnly = False
                        End If
                        DgvSatuanTerpilih.Rows(0).Cells(1).Style.BackColor = Color.Yellow

                        DgvSatuanTerpilih.Rows(0).Cells(2).Value = True
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Satuan tidak ada")
                        Exit Sub
                    End If
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            Get_Lv_SatuanTurunan()
            If arrSatuanTurunan.Count > 0 Then

                ComboBox18.Items.Clear()
                For Each Satuan As String In arrSatuanTurunan
                    ComboBox18.Items.Add(Satuan)
                Next

                ComboBox18.SelectedIndex = 0

            End If
        Else
            btnPilihSatuan.Enabled = False
        End If

        'If cmbSatuan.SelectedIndex <> -1 Then

        '    Try
        '        OpenConn()
        '        lvwSatuan.Visible = True
        '        lvwSatuan.Items.Clear()
        '        SQL = "select satuan_akhir from EMI_Satuan_Detail_Perhitungan where kode_perusahaan = '" & KodePerusahaan & "' "
        '        SQL = SQL & "and satuan_awal = '" & cmbSatuan.Text & "' and jenis = 'masa' "
        '        Using Dr = OpenTrans(SQL)
        '            Do While Dr.Read
        '                Dim lvw As ListViewItem
        '                lvw = lvwSatuan.Items.Add("")
        '                lvw.SubItems.Add(Dr("satuan_akhir"))
        '            Loop
        '        End Using

        '        CloseConn()

        '    Catch ex As Exception
        '        CloseConn()
        '        MessageBox.Show(ex.Message)
        '        Exit Sub
        '    End Try

        'End If
    End Sub

    Private Sub lvwSatuan_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lvwSatuan.ItemChecked
        If lvwSatuan.Items.Count = 0 Or lvwSatuan.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        If lvwSatuan.FocusedItem.Checked = True Then
            Dim indexcheck As Integer = lvwSatuan.FocusedItem.Index
            For index = 0 To lvwSatuan.Items.Count - 1
                If index <> indexcheck Then
                    If lvwSatuan.Items(index).Checked = True Then
                        lvwSatuan.Items(index).Checked = False
                    End If

                End If
            Next
        End If
    End Sub

    Private Sub Cmb_KategoriGudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KategoriGudang.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox5.Focus()
    End Sub

    Private Sub ComboBox19_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox19.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub ComboBox11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox11.SelectedIndexChanged
        If ComboBox11.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            ComboBox19.Text = ""
            ComboBox19.Items.Clear()
            ComboBox19.SelectedIndex = -1

            If Button1.Text = "&Simpan" Then
                TextBox1.Text = String.Empty
            End If

            arrId_Klasifikasi_Bahan2.Clear() : arrprefix_Klasifikasi_Bahan2.Clear()
            SQL = "select Id_Klasifikasi_Bahan2, Keterangan, Prefix_Klasifikasi_Bahan from EMI_Klasifikasi_Bahan2 "
            SQL = SQL & "where kode_perusahaan='" & KodePerusahaan & "' and Id_Klasifikasi_Bahan1='" & arrId_Klasifikasi_Bahan(ComboBox11.SelectedIndex) & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox19.Items.Add(Dr("Keterangan"))
                    arrId_Klasifikasi_Bahan2.Add(Dr("Id_Klasifikasi_Bahan2"))
                    arrprefix_Klasifikasi_Bahan2.Add(Dr("Prefix_Klasifikasi_Bahan"))
                Loop

            End Using

            ComboBox19.Enabled = True

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub TextBox9_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles txtStandarPrice.KeyPress, Txt_Est_Harga.KeyPress
        If e.KeyChar = Chr(13) Then
            Button1.Focus()
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Not Chk_TransaksiHrIni.Checked And Not Chk_ParamTgl.Checked And Not Chk_ParamLain.Checked Then
            MessageBox.Show("Pilih Dahulu Parameter Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Chk_TransaksiHrIni.Focus()
        End If

        If Chk_ParamTgl.Checked Then
            If Cmb_ParamTgl.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Tanggal Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_ParamTgl.DroppedDown = True : Cmb_ParamTgl.Focus()
            End If
        End If

        If Chk_ParamLain.Checked Then
            If Cmb_ParamLain.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_ParamLain.DroppedDown = True : Cmb_ParamLain.Focus()
            Else
                If Txt_ParamValue.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_ParamValue.Focus()
                End If
            End If
        End If


        LoadDataPengajuanBarangBaru()
    End Sub

    Private Sub Dgv_Pengejuan_Barang_Baru_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Pengejuan_Barang_Baru.CellDoubleClick
        If Dgv_Pengejuan_Barang_Baru.Rows.Count = 0 Or Dgv_Pengejuan_Barang_Baru.SelectedCells.Count = 0 Then Exit Sub

        Dim SelectedKdBarang As String = Dgv_Pengejuan_Barang_Baru.CurrentRow.Cells(item_Pengajuan_Kode_Barang).Value

        TextBox1.Text = SelectedKdBarang.Trim

        TextBox1_Leave(sender, New EventArgs)

        TabControl1.SelectedIndex = 0

    End Sub


    Private Sub ComboBox21_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_FlagPotongStok.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_FlagPotongStok.SelectedIndex = 0 Then
                Button1.Focus()
            Else
                txtStandarPrice.Focus()
            End If
        End If
    End Sub



    Private Sub Cmb_FlagPotongStok_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_FlagPotongStok.SelectedIndexChanged
        If Cmb_FlagPotongStok.SelectedIndex = 0 Then
            txtStandarPrice.Enabled = False
            txtStandarPrice.Text = ""
        Else
            txtStandarPrice.Enabled = True
            txtStandarPrice.Text = ""
        End If
    End Sub



    Private Sub TextBox3_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub



    Private Sub DgvSatuanTerpilih_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DgvSatuanTerpilih.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox19.Focus()
    End Sub

    Private Sub cmbJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenis.SelectedIndexChanged
        'If cmbJenis.SelectedItem = "RAW MATERIAL" Then
        If arrGudangRawMaterial.Contains(arrJenisBarang(cmbJenis.SelectedIndex)) Then
            ComboBox11.Enabled = True
            ComboBox19.Enabled = True
            ComboBox19.Text = ""
            TextBox1.Enabled = False
            ComboBox11.Focus()
        Else
            ComboBox11.Enabled = False
            ComboBox19.Enabled = False
            TextBox1.Enabled = True
            ComboBox19.Items.Clear()
            ComboBox11.SelectedIndex = -1
        End If

        If Button1.Text = "&Simpan" Then
            TextBox1.Text = String.Empty
        End If
    End Sub


    Private Sub ComboBox15_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox15.SelectedIndexChanged
        If ComboBox15.Items.Count = 0 Then Exit Sub

        If ComboBox15.SelectedIndex = 1 Then
            TextBox4.Enabled = False
        Else
            TextBox4.Enabled = True
        End If

        TextBox4.Text = ""
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 1 Then
            'Cari("Y")
        ElseIf TabControl1.SelectedIndex = 2 Then
            Chk_ParamTgl.Checked = False : Cmb_ParamTgl.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Chk_ParamLain.Checked = False : Cmb_ParamLain.Enabled = False : Txt_ParamValue.Enabled = False

            Cmb_ParamTgl.SelectedIndex = -1 : Cmb_ParamLain.SelectedIndex = -1
            DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
            Txt_ParamValue.Text = ""

            Chk_TransaksiHrIni.Checked = True

            LoadDataPengajuanBarangBaru()
        End If


    End Sub

    Private Sub TextBox3_Leave(sender As Object, e As EventArgs) Handles TextBox3.Leave
        If Not IsNumeric(TextBox3.Text) Then TextBox3.Text = ""
    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        If Not IsNumeric(TextBox4.Text) Then TextBox4.Text = ""
    End Sub

    Private Sub TextBox4_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If ComboBox15.SelectedIndex = -1 Then e.Handled = True : Exit Sub
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox11.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox19.Focus()
    End Sub

    Private Sub CmbJnsGudanng_Leave(sender As Object, e As EventArgs) Handles CmbJnsGudanng.Leave
        'If CmbJnsGudanng.SelectedIndex = 2 Then
        '    ComboBox11.Enabled = True
        '    TextBox1.Enabled = False
        '    TextBox1.Text = ""
        '    ComboBox11.Focus()
        'Else
        '    ComboBox11.Enabled = False
        '    ComboBox11.SelectedIndex = -1
        '    TextBox1.Enabled = True
        'End If
    End Sub

    Private Sub CmbJnsGudanng_TextChanged(sender As Object, e As EventArgs) Handles CmbJnsGudanng.TextChanged
        If CmbJnsGudanng.SelectedIndex = -1 Then Exit Sub

        CmbJnsGudanng_Leave(CmbJnsGudanng, e)
    End Sub

    Private Sub ComboBox19_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox19.SelectedIndexChanged
        If Button1.Text = "&Simpan" Then
            Try

                OpenConn()

#Region "KodeLama"

                'Dim No_Urut As String
                'SQL = "SELECT b.Prefix_Klasifikasi_Bahan, (MAX(RIGHT(a.Kode_Barang, 4)) + 1) AS No_Urut "
                'SQL = SQL & "FROM barang a LEFT JOIN (SELECT Prefix_Klasifikasi_Bahan, id_klasifikasi_bahan2 FROM  EMI_Klasifikasi_Bahan2 WHERE "
                'SQL = SQL & "id_klasifikasi_bahan2 = '" & arrId_Klasifikasi_Bahan2(ComboBox19.SelectedIndex) & "') b ON a.id_klasifikasi_bahan2 = b.id_klasifikasi_bahan2 WHERE SUBSTRING('20210001', 3, 2) = b.Prefix_Klasifikasi_Bahan "
                'SQL = SQL & "GROUP BY b.Prefix_Klasifikasi_Bahan "
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        If IsDBNull(Dr("No_Urut")) Then
                '            No_Urut = "0001"
                '        Else
                '            No_Urut = Format(Dr("No_Urut"), "0###")
                '        End If
                '    Else
                '        No_Urut = "0001"
                '    End If

                'End Using

                'TextBox1.Text = arrprefix_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & arrprefix_Klasifikasi_Bahan2(ComboBox19.SelectedIndex) & No_Urut

#End Region

                Dim No_Urut As String
                'SQL = " select b.Prefix_Klasifikasi_Bahan, (max(right(a.Kode_Barang,4)) + 1) as No_Urut "
                'SQL = SQL & "from barang a left join (select Prefix_Klasifikasi_Bahan, id_klasifikasi_bahan from emi_klasifikasi_bahan where id_klasifikasi_bahan = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "') b on a.id_klasifikasi_bahan = b.id_klasifikasi_bahan "
                'SQL = SQL & "where left(a.kode_barang,2) = b.Prefix_Klasifikasi_Bahan "
                'SQL = SQL & "group by b.Prefix_Klasifikasi_Bahan "


                SQL = " Select b.Prefix As Prefix_Klasifikasi_Bahan, (max(right(a.Kode_Barang,3)) + 1) As No_Urut "
                SQL = SQL & "From barang a left Join ( "
                SQL = SQL & "Select a.Prefix_Klasifikasi_Bahan +''+b.Prefix_Klasifikasi_Bahan as Prefix, b.id_klasifikasi_bahan1 "
                SQL = SQL & "From EMI_Klasifikasi_Bahan a, EMI_Klasifikasi_Bahan2 b Where "
                SQL = SQL & "a.id_klasifikasi_bahan = b.id_klasifikasi_bahan1 And "
                SQL = SQL & "b.id_klasifikasi_bahan2 = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "') b "
                SQL = SQL & "on a.id_klasifikasi_bahan = b.id_klasifikasi_bahan1 Where Left(a.kode_barang, 4) = Prefix "
                SQL = SQL & "Group By b.Prefix "

                SQL = "select top(1) substring(kode_barang, 5, 3) as no_urut from barang where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "id_klasifikasi_bahan = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "' and "
                SQL = SQL & "id_klasifikasi_bahan2 = '" & arrId_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & "' "
                SQL = SQL & "order by no_urut desc"

                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        'If IsDBNull(Dr("No_Urut")) Then
                        '    No_Urut = "001"
                        'Else
                        '   
                        'End If
                        No_Urut = Format(Val(Dr("No_Urut")) + 1, "000")
                    Else
                        No_Urut = "001"
                    End If
                    TextBox1.Text = arrprefix_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & arrprefix_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & No_Urut
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Master_Barang_New_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Txtket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txtket.KeyPress
        If e.KeyChar = Chr(13) Then TextBox5.Focus()
    End Sub

    Private Sub DgvSatuanTerpilih_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvSatuanTerpilih.CellFormatting
        If DgvSatuanTerpilih.CurrentCell IsNot Nothing AndAlso
           DgvSatuanTerpilih.CurrentCell.RowIndex = e.RowIndex AndAlso
           DgvSatuanTerpilih.CurrentCell.ColumnIndex = e.ColumnIndex AndAlso
           DgvSatuanTerpilih.IsCurrentCellInEditMode Then
            Exit Sub
        End If

        If e.ColumnIndex = DgvSatuanTerpilih.Columns("Column1").Index Then
            If e.Value IsNot Nothing AndAlso IsNumeric(e.Value) Then
                e.Value = e.Value.ToString() & " " & cmbSatuan.Text
                e.FormattingApplied = True
            End If
        End If
    End Sub



    '==========================================================================================================================================================
    '=     HANDLE KEYPRESS
    '==========================================================================================================================================================
    Private Sub Chk_TransaksiHrIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_TransaksiHrIni.CheckedChanged
        If Chk_TransaksiHrIni.Checked = True Then
            Chk_ParamTgl.Checked = False
            Btn_Cari_Click(Chk_TransaksiHrIni, e)
        End If
    End Sub

    Private Sub Chk_ParamTgl_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamTgl.CheckedChanged
        If Chk_ParamTgl.Checked Then
            Cmb_ParamTgl.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Chk_TransaksiHrIni.Checked = False
        Else
            Cmb_ParamTgl.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_ParamTgl.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub Chk_ParamLain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamLain.CheckedChanged
        If Chk_ParamLain.Checked Then
            Cmb_ParamLain.Enabled = True : Txt_ParamValue.Enabled = True
        Else
            Cmb_ParamLain.Enabled = False : Txt_ParamValue.Enabled = False
            Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamValue.Text = ""
        End If
    End Sub

















    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub


End Class
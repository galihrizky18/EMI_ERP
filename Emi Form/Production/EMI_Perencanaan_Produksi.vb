
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Xml


Public Class EMI_Perencanaan_Produksi

    Dim arrIdLine, arrInisialFaktur As New ArrayList

    Dim jumlah_po As Double = 0

    Dim lvNoAntri As String
    Dim lvNoPo As String
    Dim lvLokasi As String
    Dim lvKdCus As String
    Dim lvNmCus As String
    Dim lvKdBrg As String
    Dim lvNmBrg As String
    Dim lvJmlh As String
    Dim lvJmlhSisa As String
    Dim lvSatuan As String
    Dim lvJenisProduk As String
    Dim lvUrut As String
    Dim lvIdJenisProduk As String

    Dim cellLvNoAntri As Integer = 0
    Dim cellLvNoPo As Integer = 1
    Dim cellLvLokasi As Integer = 2
    Dim cellLvKdCus As Integer = 3
    Dim cellLvNmCus As Integer = 4
    Dim cellLvKdBrg As Integer = 5
    Dim cellLvNmBrg As Integer = 6
    Dim cellLvJmlh As Integer = 7
    Dim cellLvJmlhSisa As Integer = 8
    Dim cellLvSatuan As Integer = 9
    Dim cellLvJenisProduk As Integer = 10
    Dim cellLvUrut As Integer = 11
    Dim cellLvIdJenisProduk As Integer = 12


    Dim lvNoPoDet As String
    Dim lvLokasiDet As String
    Dim lvKdCusDet As String
    Dim lvNmCusDet As String
    Dim lvKdBrgDet As String
    Dim lvNmBrgDet As String
    Dim lvJmlhDet As String
    Dim lvSatuanDet As String
    Dim lvJenisProdukDet As String
    Dim lvIdJenisProdukDet As String

    Dim cellLvNoPoDet As Integer = 0
    Dim cellLvLokasiDet As Integer = 1
    Dim cellLvKdCusDet As Integer = 2
    Dim cellLvNmCusDet As Integer = 3
    Dim cellLvKdBrgDet As Integer = 4
    Dim cellLvNmBrgDet As Integer = 5
    Dim cellLvJmlhDet As Integer = 6
    Dim cellLvSatuanDet As Integer = 7
    Dim cellLvJnsProdukDet As Integer = 8
    Dim cellLvIdJnsPdkDet As Integer = 9

    Dim fakturBB As String
    Dim Kode_Unik As String = ""

    Private Sub get_isi_listview(ByVal index As Integer)
        lvNoAntri = ListView1.Items(index).SubItems(cellLvNoAntri).Text
        lvNoPo = ListView1.Items(index).SubItems(cellLvNoPo).Text
        lvLokasi = ListView1.Items(index).SubItems(cellLvLokasiDet).Text
        lvKdCus = ListView1.Items(index).SubItems(cellLvKdCus).Text
        lvNmCus = ListView1.Items(index).SubItems(cellLvNmCus).Text
        lvKdBrg = ListView1.Items(index).SubItems(cellLvKdBrg).Text
        lvNmBrg = ListView1.Items(index).SubItems(cellLvNmBrg).Text
        lvJmlh = ListView1.Items(index).SubItems(cellLvJmlh).Text
        lvJmlhSisa = ListView1.Items(index).SubItems(cellLvJmlhSisa).Text
        lvSatuan = ListView1.Items(index).SubItems(cellLvSatuanDet).Text
        lvIdJenisProduk = ListView1.Items(index).SubItems(cellLvIdJenisProduk).Text
    End Sub

    Private Sub get_isi_listview_detail(ByVal index As Integer)
        lvNoPoDet = ListView2.Items(index).SubItems(cellLvNoPoDet).Text
        lvLokasiDet = ListView2.Items(index).SubItems(cellLvLokasiDet).Text
        lvKdCusDet = ListView2.Items(index).SubItems(cellLvKdCusDet).Text
        lvNmCusDet = ListView2.Items(index).SubItems(cellLvNmCusDet).Text
        lvKdBrgDet = ListView2.Items(index).SubItems(cellLvKdBrgDet).Text
        lvNmBrgDet = ListView2.Items(index).SubItems(cellLvNmBrgDet).Text
        lvJmlhDet = ListView2.Items(index).SubItems(cellLvJmlhDet).Text
        lvSatuanDet = ListView2.Items(index).SubItems(cellLvSatuanDet).Text
        lvIdJenisProdukDet = ListView2.Items(index).SubItems(cellLvIdJnsPdkDet).Text
    End Sub


    Private Sub get_no_faktur()
        txtNoFaktur.Text = FRencanaProduksiBarang & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("emi_rencana_produksi", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(FRencanaProduksiBarang) + 4 & ")", FRencanaProduksiBarang & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub get_no_fakturPermintaanBB()

        fakturBB = fPBB & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Permintaan_Bahan_Baku", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fPBB) + 4 & ")", fPBB & Format(tgl_skg, "MMyy"))
    End Sub

    Public Sub get_daftar_po()

        ListView1.Items.Clear()

        SQL = "; with cte as ("
        SQL = SQL & "select *,ROW_NUMBER() OVER(ORDER BY urut asc) as id from EMI_Produksi_Terpenuhi  "
        SQL = SQL & "), cte_b as( "
        SQL = SQL & "select Urut,Kode_Perusahaan,no_PO,Kode_customer,Kode_Barang,Kode_Stock_Owner,status,jumlah,jumlah_sisa,satuan,selesai, ID, indx "
        SQL = SQL & "from cte where no_antrian is null "

        SQL = SQL & "UNION ALL "

        SQL = SQL & "select Urut,kode_perusahaan,no_PO,Kode_customer,Kode_Barang,Kode_Stock_Owner,status,jumlah,jumlah_sisa,satuan,selesai, NO_antrian as ID, indx "
        SQL = SQL & "from cte where no_antrian is not null "
        SQL = SQL & ")select   a.urut,ROW_NUMBER() OVER(ORDER BY id,indx asc) as nomor,a.kode_stock_owner ,a.No_PO,a.Kode_Customer,b.Nama as nama_cus,a.Kode_Barang, c.nama as nama_brg, "
        SQL = SQL & "a.jumlah,a.Jumlah_Sisa,a.Satuan, e.keterangan as jenis_produk, d.id_jenis_produk "
        SQL = SQL & "from cte_b a, Customers b,barang c,emi_varian d, emi_jenis_produk e  "
        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Customer = b.Kode_Customer "
        SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Barang = c.Kode_Barang and a.Kode_Stock_Owner = c.Kode_Stock_Owner "
        SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Varian = d.Id_Varian "
        SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Jenis_Produk = e.Id_Jenis_Produk "
        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null and a.Selesai is null "


        Using Dr = OpenTrans(SQL)
            Do While Dr.Read

                Dim lvw As ListViewItem
                lvw = ListView1.Items.Add(Dr("nomor"))
                lvw.SubItems.Add(Dr("no_po"))
                lvw.SubItems.Add(Dr("kode_stock_owner"))
                lvw.SubItems.Add(Dr("kode_customer"))
                lvw.SubItems.Add(Dr("nama_cus"))
                lvw.SubItems.Add(Dr("kode_barang"))
                lvw.SubItems.Add(Dr("nama_brg"))
                lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                lvw.SubItems.Add(Format(Dr("jumlah_sisa"), "N0"))
                lvw.SubItems.Add(Dr("satuan"))
                lvw.SubItems.Add(Dr("jenis_produk"))
                lvw.SubItems.Add(Dr("urut"))
                lvw.SubItems.Add(Dr("id_jenis_produk"))
            Loop
        End Using


    End Sub



    Private Sub Master_Gudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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


        Label1.Text = Base_Language.Lang_Rencana_Produksi_Judul
        'LblNoFaktur.Text = Base_Language.Lang_Global_NoFaktur
        lblTanggal.Text = Base_Language.Lang_Global_Tanggal
        lblJam.Text = Base_Language.Lang_Global_Jam

        lbltxtNoPo.Text = Base_Language.Lang_Global_No_PO
        lblTxtLokasi.Text = Base_Language.Lang_Global_Lokasi
        lblTxtKodeCustomer.Text = Base_Language.Lang_Global_KodeCustomer
        lblTxtNmCust.Text = Base_Language.Lang_Global_NamaCustomer
        lblTxtKdBrng.Text = Base_Language.Lang_Global_KodeBarang
        lblTxtNmBrng.Text = Base_Language.Lang_Global_NamaBarang
        lblTxtJumlah.Text = Base_Language.Lang_Global_Jumlah
        lblTxtSatuan.Text = Base_Language.Lang_Global_Satuan

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan




        ListView1.Columns.Add(Base_Language.Lang_Global_NO, 40, HorizontalAlignment.Center)
        ListView1.Columns.Add(Base_Language.Lang_Global_No_PO, 120, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 125, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 170, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 135, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 180, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center)
        ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah_Sisa, 100, HorizontalAlignment.Center)
        ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 80, HorizontalAlignment.Center)
        ListView1.Columns.Add(Base_Language.Lang_Global_Jenis_Satuan, 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Urut", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("id Jenis Produk", 0, HorizontalAlignment.Center)

        '  ListView1.View = View.Details


        ListView2.Columns.Add(Base_Language.Lang_Global_No_PO, 130, HorizontalAlignment.Left)
        ListView2.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left)
        ListView2.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 145, HorizontalAlignment.Left)
        ListView2.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 190, HorizontalAlignment.Left)
        ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 135, HorizontalAlignment.Left)
        ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 210, HorizontalAlignment.Left)
        ListView2.Columns.Add(Base_Language.Lang_Global_Jumlah, 110, HorizontalAlignment.Center)
        ListView2.Columns.Add(Base_Language.Lang_Global_Satuan, 110, HorizontalAlignment.Center)
        ListView2.Columns.Add(Base_Language.Lang_Global_Jenis_Produk, 110, HorizontalAlignment.Center)
        ListView2.Columns.Add("Id Jenis Produk", 0, HorizontalAlignment.Center)

        ' ListView2.View = View.Details

        get_jam()
        kosong()


    End Sub

    Private Sub kosong()

        Dim Rand As New Random
        Kode_Unik = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "ddMMyyHHmmss")

        jumlah_po = 0

        Try
            OpenConn()

            get_no_faktur()

            SQL = "update emi_produksi_terpenuhi set no_antrian = null, indx = 999 "
            ExecuteTrans(SQL)

            get_daftar_po()

            arrInisialFaktur.Clear() : CmbLokasi.Items.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using
            CmbLokasi.Text = Lokasi

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        cmbLine.Items.Clear() : arrIdLine.Clear()
        ListView2.Items.Clear()

        kosong_sebagian()
    End Sub

    Private Sub kosong_sebagian()

        jumlah_po = 0

        txtNoPo.Clear()
        txtLokasi.Clear()
        txtKodeBarang.Clear()
        txtNamaBarang.Clear()
        txtKodeCustomer.Clear()
        txtNamaCustomer.Clear()
        txtJumlah.Clear()
        txtSatuan.Clear()
        txtJenisProduk.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        kosong()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Belum_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        If ListView2.Items.Count = 0 Then
            'looping data listview 1
            For indexLv1 As Integer = 0 To ListView1.Items.Count - 1
                get_isi_listview(indexLv1)


                If ListView1.FocusedItem.SubItems(1).Text <> lvNoPo Then
                    If lvJmlhSisa <> 0 Then
                        MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Harus_Sesuai_Antrian & " " & vbNewLine & Base_Language.Lang_Rencana_Produksi_No_Antrian & " :  " & lvNoAntri & vbNewLine & Base_Language.Lang_Rencana_Produksi_No_PO & " : " & lvNoPo & vbNewLine & Base_Language.Lang_Rencana_Produksi_Sisa & ",  : " & lvJmlhSisa)
                        kosong_sebagian()
                        Exit Sub
                    End If
                End If

                Exit For
            Next
        End If


        If ListView2.Items.Count <> 0 Then
            If ListView1.FocusedItem.SubItems(cellLvJenisProduk).Text <> ListView2.Items(0).SubItems(cellLvJnsProdukDet).Text Then
                MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Jns_Produk_Beda, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            If ListView1.FocusedItem.Text <> ListView2.Items.Count + 1 Then
                MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Hrs_Sesuai)
                Exit Sub
            End If

        End If



        For indexLv2 As Integer = 0 To ListView2.Items.Count - 1
            get_isi_listview_detail(indexLv2)






            Dim check_sisa As Double = 0

            If ListView1.FocusedItem.SubItems(cellLvNoPo).Text = lvNoPoDet Then

                check_sisa = Val(HilangkanTanda(ListView1.FocusedItem.SubItems(cellLvJmlhSisa).Text)) - Val(HilangkanTanda(lvJmlhDet))



                If (check_sisa <> 0) Then
                    MessageBox.Show("Harap selesaikan semua produksi sesuai antrian." & vbNewLine & "No antrian :  " & lvNoAntri & vbNewLine & "No Po : " & lvNoPo & ", sisa : " & check_sisa)
                    kosong_sebagian()
                    Exit Sub
                End If
            Else

                For indexlv1 As Integer = 0 To ListView1.Items.Count - 1
                    get_isi_listview(indexlv1)

                    If lvNoPo = lvNoPoDet Then
                        check_sisa = Val(HilangkanTanda(lvJmlhSisa)) - Val(HilangkanTanda(lvJmlhDet))

                        If (check_sisa <> 0) Then
                            MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Hrs_Selesai_Semua & " " & vbNewLine & Base_Language.Lang_Rencana_Produksi_No_Antrian & " :  " & lvNoAntri & vbNewLine & Base_Language.Lang_Rencana_Produksi_No_PO & " : " & lvNoPo & Base_Language.Lang_Rencana_Produksi_Sisa & ", : " & check_sisa)
                            kosong_sebagian()
                            Exit Sub
                        End If
                    End If


                Next

            End If

        Next





        jumlah_po = ListView1.FocusedItem.SubItems(cellLvJmlhSisa).Text

        txtNoPo.Text = ListView1.FocusedItem.SubItems(cellLvNoPo).Text
        txtLokasi.Text = ListView1.FocusedItem.SubItems(cellLvLokasi).Text
        txtKodeCustomer.Text = ListView1.FocusedItem.SubItems(cellLvKdCus).Text
        txtNamaCustomer.Text = ListView1.FocusedItem.SubItems(cellLvNmCus).Text
        txtKodeBarang.Text = ListView1.FocusedItem.SubItems(cellLvKdBrg).Text
        txtNamaBarang.Text = ListView1.FocusedItem.SubItems(cellLvNmBrg).Text
        txtJumlah.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(cellLvJmlhSisa).Text)
        txtSatuan.Text = ListView1.FocusedItem.SubItems(cellLvSatuan).Text
        txtJenisProduk.Text = ListView1.FocusedItem.SubItems(cellLvJenisProduk).Text
        txtIdJenisProduk.Text = ListView1.FocusedItem.SubItems(cellLvIdJenisProduk).Text

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If txtNoPo.Text.Trim.Length = 0 Then Exit Sub

        If txtJumlah.Text = 0 Or txtJumlah.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Jumlah_hrs_diisi)
            Exit Sub
        End If

        For i As Integer = 0 To ListView2.Items.Count - 1
            If ListView2.Items(i).SubItems(0).Text = ListView1.FocusedItem.SubItems(1).Text Then
                MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_No_PO_Sdh_Ada, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next




        If txtJumlah.Text > jumlah_po Then
            MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Jumlah_Lebih_Besar, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If ListView2.Items.Count = 0 Then

            Try
                OpenConn()
                cmbLine.Items.Clear() : arrIdLine.Clear()
                SQL = "select a.id_line,a.kode_line,a.keterangan,a.Id_Jenis_Produk, b.Keterangan as jenis_produk from emi_line a, EMI_Jenis_Produk b "
                SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.id_jenis_produk = b.Id_Jenis_Produk "
                SQL = SQL & "and a.id_jenis_produk =  '" & txtIdJenisProduk.Text & "' "
                SQL = SQL & "order by a.kode_line "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        cmbLine.Items.Add(Dr("keterangan")) : arrIdLine.Add(Dr("id_line"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If

        Dim lvw As ListViewItem

        lvw = ListView2.Items.Add(txtNoPo.Text)
        lvw.SubItems.Add(txtLokasi.Text)
        lvw.SubItems.Add(txtKodeCustomer.Text)
        lvw.SubItems.Add(txtNamaCustomer.Text)
        lvw.SubItems.Add(txtKodeBarang.Text)
        lvw.SubItems.Add(txtNamaBarang.Text)
        lvw.SubItems.Add(Format(Val(txtJumlah.Text), "N0"))
        lvw.SubItems.Add(txtSatuan.Text)
        lvw.SubItems.Add(txtJenisProduk.Text)
        lvw.SubItems.Add(txtIdJenisProduk.Text)





        kosong_sebagian()


    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
        If e.KeyChar = Chr(13) Then
            Button2_Click(Me, Nothing)
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click



        If txtNoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Faktur, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub

            'ElseIf cmbLine.SelectedIndex = -1 Then
            '    MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Line, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
        End If

        If ListView2.Items.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Belum_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction



            'Dim tanggal As String = Format(DateTimePicker1.Value, "yyyy-MM-dd") & " " & Format(DateTimePicker2.Value, "HH:mm:ss")


            'For indexLv2 As Integer = 0 To ListView2.Items.Count - 1
            '    get_isi_listview_detail(indexLv2)

            '    SQL = "select Tanggal_Produksi,Jam_Produksi,datediff(minute, concat(tanggal_produksi,' ',jam_produksi), '" & tanggal & "' ) as jam   "
            '    SQL = SQL & "from  EMI_Rencana_Produksi a ,emi_rencana_produksi_detail b where  "
            '    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.kode_perusahaan = '" & KodePerusahaan & "' "
            '    SQL = SQL & "and kode_stock_owner = '" & lvLokasiDet & "' and no_po = '" & lvNoPoDet & "' and kode_customer = '" & lvKdCusDet & " ' "
            '    SQL = SQL & "and kode_barang = '" & lvKdBrgDet & "'  and a.line = '" & arrIdLine.Item(cmbLine.SelectedIndex) & "' "
            '    Using dr = OpenTrans(SQL)
            '        If dr.Read Then
            '            If dr("jam") < 0 Then
            '                dr.Close()
            '                CloseTrans()
            '                CloseConn()
            '                MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Tanggal_Awal, Judul, MessageBoxButtons.OK)
            '                Exit Sub

            '            End If
            '        End If
            '    End Using
            'Next





            get_isi_listview_detail(0)
            SQL = "insert into EMI_Rencana_Produksi(kode_perusahaan,no_faktur,tanggal,jam,userid,tanggal_produksi,jam_produksi,line, Keterangan, id_jenis_produk) values("
            SQL = SQL & "'" & KodePerusahaan & "', '" & txtNoFaktur.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
            SQL = SQL & "'" & UserID & "',NULL, NULL, NULL, '" & TxtCatatan.Text & "','" & lvIdJenisProdukDet & "')"
            ExecuteTrans(SQL)

            get_no_fakturPermintaanBB()

            SQL = "INSERT INTO Emi_Permintaan_Bahan_Baku(Kode_Perusahaan,No_Faktur,Tanggal, Jam, "
            SQL = SQL & "Keterangan, Lokasi, No_RencanaProduksi, UserID, Tanggal_Produksi, Jam_Produksi) VALUES("
            SQL = SQL & "'" & KodePerusahaan & "','" & fakturBB & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
            SQL = SQL & "'" & TxtCatatan.Text & "','" & CmbLokasi.Text & "', "
            SQL = SQL & "'" & txtNoFaktur.Text & "','" & UserID & "', "
            SQL = SQL & "NULL, NULL)"
            ExecuteTrans(SQL)

            ExecuteTrans("delete from EMI_Permintaan_Bahan_Baku_sementara where userID ='" & UserID & "' and jenis='Bahan Baku'")
            ExecuteTrans("delete from EMI_Permintaan_Bahan_Baku_sementara where userID ='" & UserID & "' and jenis='Bahan Penolong'")


            For i As Integer = 0 To ListView2.Items.Count - 1

                get_isi_listview_detail(i)

                Dim jumlah_sisa_temp As Double = 0
                Dim jumlah_sisa_fix As Double = 0
                Dim flag_selesai As String = ""

                SQL = "select jumlah_sisa from emi_produksi_terpenuhi where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_po = '" & lvNoPoDet & "' and kode_customer = '" & lvKdCusDet & "' and kode_barang = '" & lvKdBrgDet & "' and "
                SQL = SQL & "kode_stock_owner = '" & lvLokasiDet & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        jumlah_sisa_temp = Dr("jumlah_sisa")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_No_Tdk_Ada, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                If jumlah_sisa_temp < lvJmlhDet Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Jmlh_Sisa, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                jumlah_sisa_fix = jumlah_sisa_temp - Val(HilangkanTanda(lvJmlhDet))


                SQL = "insert into emi_rencana_produksi_detail(kode_perusahaan,no_faktur,no_po,kode_stock_owner,kode_customer,kode_barang,jumlah,satuan) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & txtNoFaktur.Text & "', '" & lvNoPoDet & "','" & lvLokasiDet & "', '" & lvKdCusDet & "', '" & lvKdBrgDet & "',  "
                SQL = SQL & "'" & HilangkanTanda(lvJmlhDet) & "' , '" & lvSatuanDet & "' ) "
                ExecuteTrans(SQL)

                If jumlah_sisa_fix = 0 Then
                    flag_selesai = "'Y'"
                Else
                    flag_selesai = "NULL"
                End If

                SQL = "update EMI_Produksi_Terpenuhi set jumlah_sisa = " & jumlah_sisa_fix & ", selesai = " & flag_selesai & " where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_po = '" & lvNoPoDet & "' and kode_customer = '" & lvKdCusDet & "'  "
                SQL = SQL & " and kode_barang = '" & lvKdBrgDet & "' and kode_stock_owner = '" & lvLokasiDet & "' "
                ExecuteTrans(SQL)

                Dim no_inq As String = ""
                SQL = "select No_Inquiry from View_PO_detail where kode_Perusahaan='" & KodePerusahaan & "' and "
                SQL = SQL & "no_faktur='" & lvNoPoDet & "' and Kode_Barang='" & lvKdBrgDet & "' "
                SQL = SQL & "and KOde_stock_Owner='" & lvLokasiDet & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        no_inq = dr("No_Inquiry")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data tidak ada @@@")
                        Exit Sub
                    End If
                End Using

                'INSERT BAHAN2 Untuk Permintaan bahan Baku
                SQL = "Select a.No_Faktur,b.Kode_Barang, b.Serapan_Bulan,b.Satuan As satuan_serapan, e.Hasil As NIlai_Formula,e.Satuan_Hasil "
                SQL = SQL & "as satuan_formula, f.Kode_Barang as Kode_Bahan, Nilai_Barang as Nilai_Bahan, Satuan_barang as satuan_bahan, "
                SQL = SQL & "G.satuan_simulasi From emi_inquiry a, Emi_Inquiry_Detail b, EMI_Transaksi_Formulator_Binding c, "
                SQL = SQL & "EMI_Transaksi_Formulator_Binding_Detail d, Emi_Transaksi_Formulator e, EMI_Transaksi_Formulator_Detail_Bahan f, Init G  Where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And a.Status Is null And "
                SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan And a.No_Faktur = c.No_Inquiry And c.Status Is null And "
                SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan And c.No_Faktur = d.No_Faktur And "
                SQL = SQL & "b.Kode_Perusahaan = d.Kode_Perusahaan And b.Kode_Barang = d.Kode_Produk And "
                SQL = SQL & "d.Kode_Perusahaan = e.Kode_Perusahaan And d.Binding_Formula = e.No_Faktur And e.Status Is null "
                SQL = SQL & "And e.Kode_Perusahaan=f.Kode_Perusahaan And e.No_Faktur=f.No_Faktur "
                SQL = SQL & "And a.Kode_Perusahaan=G.Kode_Perusahaan "
                SQL = SQL & " And a.no_faktur = '" & no_inq & "' and b.kode_barang = '" & lvKdBrgDet & "' and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For indexBahan As Integer = 0 To .Rows.Count - 1



                            Dim nilai_serapan As Double = 0
                            Dim nilai_formula As Double = 0
                            Dim nilaiBahan As Double = 0
                            Dim nilaiPembanding As Double = 0
                            Dim nilaiAkhirBahan As Double = 0

                            Dim pengali As Double = 0
                            Dim Perhitungan_satuan As Double = 0


                            SQL = "select dbo.ubah_satuan("
                            SQL = SQL & "'" & KodePerusahaan & "', 'masa', '" & .Rows(indexBahan).Item("Kode_Barang") & "', '" & .Rows(indexBahan).Item("satuan_formula") & "',"
                            SQL = SQL & "'" & .Rows(indexBahan).Item("satuan_simulasi") & "' , '" & .Rows(indexBahan).Item("NIlai_Formula") & "' "
                            SQL = SQL & ") as hasil"
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then

                                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                                        If dr("Hasil") = 0 Then
                                            MessageBox.Show("Satuan " & .Rows(indexBahan).Item("satuan_formula") & " Ke " & .Rows(indexBahan).Item("satuan_simulasi") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            nilai_formula = Math.Ceiling(dr("hasil"))
                                        End If
                                    Else
                                        MessageBox.Show("Satuan " & .Rows(indexBahan).Item("satuan_formula") & " Ke " & .Rows(indexBahan).Item("satuan_simulasi") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            SQL = "select dbo.ubah_satuan("
                            SQL = SQL & "'" & KodePerusahaan & "', 'masa', '" & lvKdBrgDet & "', '" & lvSatuanDet & "',"
                            SQL = SQL & "'" & .Rows(indexBahan).Item("satuan_simulasi") & "' , '" & HilangkanTanda(lvJmlhDet) & "' "
                            SQL = SQL & ") as hasil"
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then

                                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                                        If dr("Hasil") = 0 Then
                                            MessageBox.Show("Satuan " & lvSatuanDet & " Ke " & .Rows(indexBahan).Item("satuan_simulasi") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            nilai_serapan = Math.Ceiling(dr("hasil"))
                                        End If
                                    Else
                                        MessageBox.Show("Satuan " & lvSatuanDet & " Ke " & .Rows(indexBahan).Item("satuan_simulasi") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            Dim Satuan_Bahan As String = ""
                            nilaiPembanding = nilai_serapan / nilai_formula
                            ' nilaiPembanding = DgvData.Rows(currentRow).Cells(cellDataJmlh).Value / nilai_formula

                            nilaiBahan = .Rows(indexBahan).Item("Nilai_Bahan")
                            Satuan_Bahan = .Rows(indexBahan).Item("satuan_Bahan")
                            nilaiAkhirBahan = Val(HilangkanTanda(Format(nilaiBahan * nilaiPembanding, "N0")))

                            Dim Perhitungan As String = ""
                            Dim lokasi_gudang_bahan As String = ""

                            SQL = "select top(1) c.lokasi_gudang from EMI_Kategori_Gudang a, barang b, EMI_Kategori_Gudang_PerLokasi c  "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Id_Kategori_Gudang "
                            SQL = SQL & "and  a.Id_Kategori_Gudang = c.ID_Kategori_Gudang and a.Kode_Perusahaan = c.Kode_Perusahaan "
                            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and c.Kode_stock_Owner = '" & CmbLokasi.Text & "' "
                            SQL = SQL & "and b.kode_barang = '" & .Rows(indexBahan).Item("Kode_Bahan") & "' "
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    lokasi_gudang_bahan = dr("lokasi_gudang")
                                Else
                                    dr.Close()
                                    CloseConn()
                                    MessageBox.Show("lokasi gudang default tidak ada")
                                    Exit Sub
                                End If
                            End Using

                            SQL = "insert into EMI_Permintaan_Bahan_Baku_sementara(Kode_Perusahaan, Kode_Barang, Total,"
                            SQL = SQL & "Satuan, Kode_Bahan, Total_Bahan, Satuan_Bahan, UserID, nilai_formula, "
                            SQL = SQL & "Satuan_Formula,Nilai_Bahan, Nilai_Pembanding, No_Inquiry, Jenis,kode_stock_owner,kode_stock_owner_bahan, KD_Unik, No_PO) values( "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & .Rows(indexBahan).Item("Kode_Barang") & "', '" & nilai_serapan & "', "
                            SQL = SQL & "'" & .Rows(indexBahan).Item("Satuan_Simulasi") & "', '" & .Rows(indexBahan).Item("Kode_Bahan") & "', '" & nilaiAkhirBahan & "', "
                            SQL = SQL & "'" & Satuan_Bahan & "','" & UserID & "', '" & nilai_formula & "','" & .Rows(indexBahan).Item("Satuan_Simulasi") & "', "
                            SQL = SQL & " '" & nilaiBahan & "', '" & nilaiPembanding & "', '" & .Rows(indexBahan).Item("no_faktur") & "', 'Bahan Baku', '" & lvLokasiDet & "','" & lokasi_gudang_bahan & "', '" & Kode_Unik & "','" & lvNoPoDet & "')"
                            ExecuteTrans(SQL)

                        Next
                    End With
                End Using

                SQL = "select a.no_faktur,b.Kode_Barang,b.Serapan_Bulan,b.Satuan as satuan_serapan, c.Jumlah_Barang, d.Satuan as satuan_barang, "
                SQL = SQL & "c.Kode_Bahan,c.Jumlah_Bahan,e.Satuan as satuan_bahan, f.Satuan_Simulasi "
                SQL = SQL & "from Emi_Inquiry a, Emi_Inquiry_Detail b, EMI_Inquiry_Detail_Bahan_Penolong c, Barang d, Barang e, Init f "
                SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur=b.No_Faktur "
                SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.No_Faktur=c.No_Faktur and b.Kode_Barang=c.Kode_Barang "
                SQL = SQL & "and b.Kode_Perusahaan=d.Kode_Perusahaan and b.Kode_Stock_Owner=d.Kode_Stock_Owner and b.Kode_Barang=d.Kode_Barang "
                SQL = SQL & "and c.Kode_Perusahaan=e.Kode_Perusahaan and b.Kode_Stock_Owner=e.Kode_Stock_Owner and c.Kode_Bahan=e.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan=f.Kode_Perusahaan "
                SQL = SQL & "and a.no_faktur = '" & no_inq & "' and b.kode_barang = '" & lvKdBrgDet & "' and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For indexBahan = 0 To .Rows.Count - 1

                            Dim nilai_serapan As Double = 0
                            Dim nilai_formula As Double = 0
                            Dim nilaiBahan As Double = 0
                            Dim nilaiPembanding As Double = 0
                            Dim nilaiAkhirBahan As Double = 0

                            Dim pengali As Double = 0
                            Dim Perhitungan_satuan As Double = 0

                            If General_Class.CekNULL(.Rows(indexBahan).Item("satuan_barang")) = "" Or General_Class.CekNULL(.Rows(indexBahan).Item("satuan_barang")) = "X" Then
                                CloseConn()
                                MessageBox.Show("satuan barang tidak ada")
                                Exit Sub
                            End If
                            'Perhitungan serapan
                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexBahan).Item("Kode_Barang") & "',"
                            SQL = SQL & "'" & .Rows(indexBahan).Item("satuan_barang") & "','" & .Rows(indexBahan).Item("satuan_simulasi") & "',"
                            SQL = SQL & "" & .Rows(indexBahan).Item("Jumlah_Barang") & ") as Hasil "
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then

                                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                                        If dr("Hasil") = 0 Then
                                            MessageBox.Show("Satuan " & .Rows(indexBahan).Item("satuan_barang") & " Ke " & .Rows(indexBahan).Item("satuan_simulasi") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            nilai_formula = Math.Ceiling(dr("hasil"))
                                        End If
                                    Else
                                        MessageBox.Show("Satuan " & .Rows(indexBahan).Item("satuan_barang") & " Ke " & .Rows(indexBahan).Item("satuan_simulasi") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            'Perhitungan serapan
                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvKdBrgDet & "',"
                            SQL = SQL & "'" & lvSatuanDet & "','" & .Rows(indexBahan).Item("satuan_simulasi") & "',"
                            SQL = SQL & "" & HilangkanTanda(lvJmlhDet) & ") as Hasil "
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then

                                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                                        If dr("Hasil") = 0 Then
                                            MessageBox.Show("Satuan " & lvSatuanDet & " Ke " & .Rows(indexBahan).Item("satuan_simulasi") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            nilai_serapan = Math.Ceiling(dr("hasil"))
                                        End If
                                    Else
                                        MessageBox.Show("Satuan " & lvSatuanDet & " Ke " & .Rows(indexBahan).Item("satuan_simulasi") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using


                            Dim Satuan_Bahan As String = ""
                            nilaiPembanding = nilai_serapan / nilai_formula


                            nilaiBahan = .Rows(indexBahan).Item("Jumlah_Bahan")
                            Satuan_Bahan = .Rows(indexBahan).Item("satuan_Bahan")
                            nilaiAkhirBahan = Val(HilangkanTanda(Format(nilaiBahan * nilaiPembanding, "N0")))

                            Dim Perhitungan As String = ""
                            Dim lokasi_gudang_bahan As String = ""

                            SQL = "select top(1) c.lokasi_gudang from EMI_Kategori_Gudang a, barang b, EMI_Kategori_Gudang_PerLokasi c  "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Id_Kategori_Gudang "
                            SQL = SQL & "and  a.Id_Kategori_Gudang = c.ID_Kategori_Gudang and a.Kode_Perusahaan = c.Kode_Perusahaan "
                            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and c.Kode_stock_Owner = '" & CmbLokasi.Text & "' "
                            SQL = SQL & "and b.kode_barang = '" & .Rows(indexBahan).Item("Kode_Bahan") & "' "
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    lokasi_gudang_bahan = dr("lokasi_gudang")
                                Else
                                    dr.Close()
                                    CloseConn()
                                    MessageBox.Show("lokasi gudang default tidak ada")
                                    Exit Sub
                                End If
                            End Using

                            SQL = "insert into EMI_Permintaan_Bahan_Baku_sementara(Kode_Perusahaan, Kode_Barang, Total,"
                            SQL = SQL & "Satuan, Kode_Bahan, Total_Bahan, Satuan_Bahan, UserID, nilai_formula, "
                            SQL = SQL & "Satuan_Formula,Nilai_Bahan, Nilai_Pembanding, No_Inquiry, Jenis,kode_stock_owner,kode_stock_owner_bahan, KD_Unik, No_PO) values( "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & .Rows(indexBahan).Item("Kode_Barang") & "', '" & nilai_serapan & "', "
                            SQL = SQL & "'" & .Rows(indexBahan).Item("Satuan_Simulasi") & "', '" & .Rows(indexBahan).Item("Kode_Bahan") & "', '" & nilaiAkhirBahan & "', "
                            SQL = SQL & "'" & Satuan_Bahan & "','" & UserID & "', '" & nilai_formula & "','" & .Rows(indexBahan).Item("Satuan_Simulasi") & "', "
                            SQL = SQL & " '" & nilaiBahan & "', '" & nilaiPembanding & "', '" & .Rows(indexBahan).Item("no_faktur") & "', 'Bahan Penolong', '" & lvLokasiDet & "','" & lokasi_gudang_bahan & "','" & Kode_Unik & "','" & lvNoPoDet & "')"
                            ExecuteTrans(SQL)
                        Next
                    End With
                End Using

                SQL = "INSERT INTO Emi_Permintaan_Bahan_Baku_Detail2(Kode_Perusahaan,No_Faktur,"
                SQL = SQL & "Jenis, Kode_Stock_Owner, Kode_Barang, Jumlah_barang, Satuan_Barang, "
                SQL = SQL & "Kode_Stock_Owner_Bahan, Kode_Bahan, Jumlah, Satuan, No_PO)"
                SQL = SQL & "select kode_Perusahaan, '" & fakturBB & "', jenis, "
                SQL = SQL & "Kode_Stock_Owner, Kode_Barang, total, satuan, "
                SQL = SQL & "Kode_Stock_Owner_Bahan, Kode_Bahan, sum(total_bahan) as total, "
                SQL = SQL & "satuan_bahan, No_PO from EMI_Permintaan_Bahan_Baku_sementara where "
                SQL = SQL & "KD_Unik='" & Kode_Unik & "' and UserID='" & UserID & "' and Kode_Barang='" & lvKdBrgDet & "'"
                SQL = SQL & "group by kode_Perusahaan, jenis, Kode_Stock_Owner, Kode_Barang, total, satuan, Kode_Stock_Owner_Bahan, Kode_Bahan, satuan_bahan, No_PO "
                ExecuteTrans(SQL)
            Next


            SQL = "INSERT INTO Emi_Permintaan_Bahan_Baku_Detail(Kode_Perusahaan,No_Faktur,"
            SQL = SQL & "Jenis,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan)"
            SQL = SQL & "select kode_Perusahaan, '" & fakturBB & "', jenis, Kode_Stock_Owner_Bahan, Kode_Bahan, sum(total_bahan) as total, "
            SQL = SQL & "satuan_bahan from EMI_Permintaan_Bahan_Baku_sementara where "
            SQL = SQL & "KD_Unik='" & Kode_Unik & "' and UserID='" & UserID & "' "
            SQL = SQL & "group by kode_Perusahaan, jenis, Kode_Stock_Owner_Bahan, Kode_Bahan, satuan_bahan "
            ExecuteTrans(SQL)


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

    End Sub

    Private Sub BtnPembelian_Clear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        kosong_sebagian()
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.FocusedItem.Index <> ListView2.Items.Count - 1 Then
            MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Hapus_Temp, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        ListView2.FocusedItem.Remove()

        If ListView2.Items.Count = 0 Then
            cmbLine.Items.Clear() : arrIdLine.Clear()
        End If
    End Sub

    Private Sub UbahAntrianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UbahAntrianToolStripMenuItem.Click
        EMI_Perencanaan_Produksi_SD_Antrian.ComboBox1.Items.Clear()

        For i As Integer = 1 To ListView1.Items.Count
            EMI_Perencanaan_Produksi_SD_Antrian.ComboBox1.Items.Add(i)
        Next

        EMI_Perencanaan_Produksi_SD_Antrian.ComboBox1.Text = ListView1.FocusedItem.Text

        EMI_Perencanaan_Produksi_SD_Antrian.ShowDialog()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class
Public Class EMI_Restock

    Dim Flag_Opname As Boolean = False

    Dim JumlahOld As Double
    Dim arrInisialFaktur, Arr_COA_Persediaan, Arr_COA_Adj_Tambah, Arr_COA_Adj_Kurang As New ArrayList

    Dim MasaTahunExp As Integer = 5

    Private Sub Adjustment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()

        ListView3.Columns.Add("Stock Owner", 130, HorizontalAlignment.Center)
        ListView3.Columns.Add("Kode Barang", 130, HorizontalAlignment.Center)
        ListView3.Columns.Add("Nama", 0, HorizontalAlignment.Left)
        ListView3.Columns.Add("Good Stock", 150, HorizontalAlignment.Right)
        ListView3.Columns.Add("Satuan", 80, HorizontalAlignment.Right)
        ListView3.View = View.Details

        ListView3.Location = New Point(188, 83)
        'DateTimePicker1.Focus()
    End Sub

    Private Sub Kosong()
        GetTime()
        'JANGAN LUIPA UBAH MENJADI FMenuDev
        DateTimePicker1.Value = Date.Now 'tgl_skg
        urutan.Text = ""

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

            Cmb_Lokasi.Items.Clear() : arrInisialFaktur.Clear()
            Arr_COA_Persediaan.Clear() : Arr_COA_Adj_Tambah.Clear() : Arr_COA_Adj_Kurang.Clear()

            SQL = "select kode_stock_owner, inisial_faktur, persediaan, "
            'SQL = SQL & "adjustment_stock_tambah, adjustment_stock_kurang from stock_owner where "
            SQL = SQL & "adjustment_stock_tambah, adjustment_stock_kurang from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by kode_stock_owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("kode_stock_owner")) : arrInisialFaktur.Add(Dr("inisial_faktur"))
                    Arr_COA_Persediaan.Add(Dr("persediaan"))
                    Arr_COA_Adj_Tambah.Add(Dr("adjustment_stock_tambah"))
                    Arr_COA_Adj_Kurang.Add(Dr("adjustment_stock_kurang"))
                Loop
            End Using

            'Cmb_Lokasi.Text = Lokasi
            ' Cmb_Lokasi.Text = "RAW MATERIAL"





            'TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
            '          General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
            '          "Kode_perusahaan", KodePerusahaan,
            '          "And", "substring(kode_adjustment,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Lokasi.Focus()

        DateTimePicker1.Enabled = True
        ListView3.Visible = False
        TextBox2.Text = "" : TextBox3.Text = "" : Txt_SisaStock.Text = "" : TextBox5.Text = "" : TextBox6.Text = ""
        TextBox7.Text = ""

        SatuanBesar.Items.Clear()
        SatuanBesar.Text = ""
        Txt_SatuanKecil.Text = ""

        Dtp_TglProd.Enabled = True
        Dtp_TglEx.Enabled = True

        Dtp_TglProd.Value = DateTime.Now
        Dtp_TglEx.Value = DateTime.Now

        MetodePengeluaranStock.Text = String.Empty

        Cmb_Lokasi.Enabled = True : TextBox2.Enabled = True
        Btn_Simpan.Text = "&Simpan"
    End Sub

    Private Sub Adjustment_Dist_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub



    ''Private Sub Adjustment_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
    ''    Label13.Size = New Point(Me.Width, 33)
    ''End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub


    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView3.Items.Count = 0 Then Exit Sub
            ListView3.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox5.Focus()
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox6.Focus()
        'If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(Asc("-")) Or e.KeyChar = Chr(Asc(".")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
        If Not (e.KeyChar >= "0"c And e.KeyChar <= "9"c Or e.KeyChar = "."c Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Chr(13) Then
            Dtp_TglProd.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.Leave
        If Cmb_Lokasi.SelectedIndex = -1 Then Exit Sub
        Try
            OpenConn()

            TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                          General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
                          "Kode_perusahaan", KodePerusahaan,
                          "And", "substring(kode_adjustment,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Simpan.Click


        If Cmb_Lokasi.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode stock owner harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode barang harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        ElseIf TextBox5.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah adjustment harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox5.Focus() : Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Harga beli harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus() : Exit Sub
        ElseIf TextBox6.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox6.Focus() : Exit Sub
        ElseIf Dtp_TglProd.Text = Dtp_TglEx.Text Then
            MessageBox.Show("Tanggal Produksi Tidak Boleh Sama Dengan Tanggal Expire", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_TglProd.Focus() : Exit Sub
        ElseIf Format(Dtp_TglProd.Value, "yyyy-MM-dd") > Format(Dtp_TglEx.Value, "yyyy-MM-dd") Then
            MessageBox.Show("Tanggal Produksi Tidak Boleh Lebih Besar dari Tanggal Expired!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_TglProd.Focus() : Exit Sub
        ElseIf Format(Dtp_TglProd.Value, "yyyy-MM-dd") > Format(DateTime.Now, "yyyy-MM-dd") Then
            MessageBox.Show("Tanggal Produksi Tidak Boleh di Tanggal Maju!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_TglProd.Focus() : Exit Sub
        End If

        'Dim Tanya As String = MessageBox.Show("Anda yakin akan menyimpan data adjustment ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        'If Tanya = vbNo Then Exit Sub

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            Dim Jumlah As Double = 0
            SQL = "select a.kode_barang, a.nama, a.good_stock, a.last_hpp, a.Standar_Price, a.Metode_Pengeluaran_Stok, "
            SQL = SQL & "ISNULL(( dbo.ubah_satuan(a.Kode_Perusahaan, 'masa', a.Kode_Barang, a.Satuan, "
            SQL = SQL & "(select z.satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Flag_Tampil_Display = 'Y' and z.Kode_barang = a.Kode_Barang) "
            SQL = SQL & ", a.Good_Stock) ), '0') as Stock, "
            SQL = SQL & "a.Satuan as Satuan_Kecil, "
            SQL = SQL & "ISNULL((select Satuan from Barang_Detail_Satuan z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Barang = z.Kode_barang and z.Flag_Tampil_Display = 'Y'), '-') as Satuan_Display "
            SQL = SQL & "from barang a where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_stock_owner = '" & Cmb_Lokasi.Text & "' and "
            SQL = SQL & "a.kode_barang = '" & TextBox2.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Jumlah = Dr("jml")
                End If
            End Using


            If Btn_Simpan.Text = "&Simpan" Then
                '==========================================================
                'Cek apakah update stock akan membuat stock menjadi negatif
                '==========================================================
                Dim satuan_kecil As String = ""
                SQL = "select top(1) satuan from barang where kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and kode_Barang='" & TextBox2.Text & "' "
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        satuan_kecil = Dr1("satuan")
                    Else
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data tidak ada ")
                        Exit Sub
                    End If
                End Using

                Dim nilai_kecildetail As Double = 0
                Dim hpp_kecildetail As Double = 0
                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TextBox2.Text & "', '" & SatuanBesar.Text & "',"
                SQL = SQL & "'" & satuan_kecil & "', '" & HilangkanTanda(TextBox5.Text) & "' ) as hasil"
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If

                        nilai_kecildetail = Dr1("hasil")
                    Else
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using

                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'UANG','" & TextBox2.Text & "', '" & SatuanBesar.Text & "',"
                SQL = SQL & "'" & satuan_kecil & "', '" & HilangkanTanda(TextBox7.Text) & "' ) as hasil"
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If

                        hpp_kecildetail = Dr1("hasil")
                    Else
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using


                SQL = "select good_stock from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi.Text & "' "
                SQL = SQL & "and kode_barang = '" & TextBox2.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("good_stock") + nilai_kecildetail < 0 Then
                            MessageBox.Show("Proses adjustment akan membuat stock menjadi negatif, proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            TextBox2.Focus()
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        Exit Sub
                    End If
                End Using



                TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                          General_Class.Get_Last_Number2("EMI_Restock_Barang", "No_Faktur", JumlahDigit,
                          "Kode_perusahaan", KodePerusahaan,
                          "And", "substring(No_Faktur,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))


                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & arrInisialFaktur(Cmb_Lokasi.SelectedIndex), KodePerusahaan)

                Dim total_hpp As Double = 0

                If Val(TextBox5.Text) < 0 Then 'minus 


                Else ' kalo nambahin stock



                    Dim Rand As New Random
                    Dim str As String = Format(Rand.Next(0, 999), "000") & Format(Date.Now, "HHmmss")
                    Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)

                    Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hpp_kecildetail & Tanda_SN & "02" & Tanda_SN & Format(DateTimePicker1.Value, "yyyy-MM-dd")

                    total_hpp += hpp_kecildetail * nilai_kecildetail

                    SQL = "select kode_barang from barang_sn where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "' and "
                    SQL = SQL & "kode_barang = '" & TextBox2.Text.Trim & "' and serial_number = '" & SN & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            SQL = "Update barang_sn set jumlah = jumlah + " & nilai_kecildetail & ", rr = 'X' where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "' and kode_barang = '" & TextBox2.Text.Trim & "' and "
                            SQL = SQL & "serial_number = '" & SN & "'"
                            ExecuteTrans(SQL)
                        Else
                            'SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                            'SQL = SQL & "serial_number, jumlah, rr, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
                            'SQL = SQL & "'" & Cmb_Lokasi.Text & "', '" & TextBox2.Text.Trim & "', "
                            'SQL = SQL & "'" & SN & "', " & TextBox5.Text & ", 'X', '" & Format(Dtp_TglProd.Value, "yyyy-MM-dd") & "', '" & Format(Dtp_TglEx.Value, "yyyy-MM-dd") & "')"
                            'Dr.Close()
                            'ExecuteTrans(SQL)

                            Dr.Close()

                            Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)
                            Dim newKodeUnikAsal As String = Generate_Random_Kode(10)

                            ''GET ID_WAREHOUSE YG KOSONG
                            Dim available_Id_Warehouse As String = ""
                            Dim available_NoPallet As String = ""

                            SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                            SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                            SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                            SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Kode_Stock_Owner='" & Cmb_Lokasi.Text & "' and b.Kode_Barang is null"
                            Using Dr2 = OpenTrans(SQL)
                                Do While Dr2.Read
                                    available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                                    available_NoPallet = Dr2("nomor_urut")
                                Loop
                            End Using


                            Dim IDSusunan_Barang As String = ""
                            SQL = "Select urut from barang_detail_susunan where Kode_Barang = '" & TextBox2.Text.Trim & "' "
                            SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' and flag_default='Y' "
                            Using Dr2 = OpenTrans(SQL)
                                If Dr2.Read Then
                                    IDSusunan_Barang = Dr2("urut")
                                Else
                                    Dr2.Close()
                                    CloseConn()
                                    CloseTrans()
                                    MessageBox.Show("Susunan tidak ditemukan")
                                    Exit Sub
                                End If
                            End Using

                            SQL = "insert into Barang_SN(kode_perusahaan, kode_stock_owner, kode_barang, "
                            SQL = SQL & "serial_number, jumlah, Tgl_Produksi, Tgl_Expired, Id_Warehouse, "
                            SQL = SQL & "id_Susunan, Nomor_Pallet, Kode_Unik_Asal, Kode_Unik_Berjalan, "
                            SQL = SQL & "Jumlah_Bags, Qr_Code, Batch_Number, warna) "
                            SQL = SQL & "Values( "
                            SQL = SQL & "'" & KodePerusahaan & "','" & Cmb_Lokasi.Text & "', "
                            SQL = SQL & "'" & TextBox2.Text.Trim & "','" & SN & "', "
                            SQL = SQL & "'" & nilai_kecildetail & "','" & Format(Dtp_TglProd.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "'" & Format(Dtp_TglEx.Value, "yyyy-MM-dd") & "','" & available_Id_Warehouse & "', "
                            SQL = SQL & "'" & IDSusunan_Barang & "','" & available_NoPallet & "', "
                            SQL = SQL & "'" & newKodeUnikAsal & "','" & newKodeUnikBerjalan & "', "
                            SQL = SQL & "'0', '-', "
                            SQL = SQL & "'X', 'HIJAU') "
                            ExecuteTrans(SQL)
                        End If
                    End Using

                    SQL = "Insert Into EMI_Restock_Barang (kode_perusahaan, No_Faktur, tanggal, jam, kode_stock_owner, kode_barang, jumlah, "
                    SQL = SQL & "keterangan, userid, kode_voucher, harga_beli, Tgl_Produksi, Tgl_Expired, Satuan, Satuan_Barang, serial_number) "
                    SQL = SQL & "Values('" & KodePerusahaan & "', '" & TextBox1.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(Date.Now, "HH:mm:ss") & "', '"
                    SQL = SQL & Cmb_Lokasi.Text & "', '" & TextBox2.Text.Trim & "', " & HilangkanTanda(TextBox5.Text) & ", "
                    SQL = SQL & "'" & TextBox6.Text.Trim & "', '" & UserID & "', '" & Kode_Voucher & "', '" & TextBox7.Text & "', "
                    SQL = SQL & "'" & Format(Dtp_TglProd.Value, "yyyy-MM-dd") & "', '" & Format(Dtp_TglEx.Value, "yyyy-MM-dd") & "', '" & SatuanBesar.Text & "', "
                    SQL = SQL & "'" & satuan_kecil & "', '" & SN & "')"
                    ExecuteTrans(SQL)

                End If

                '==========================================================

                SQL = "Update barang set good_stock = good_stock + " & nilai_kecildetail & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "' and kode_Barang = '" & TextBox2.Text.Trim & "'"
                ExecuteTrans(SQL)


                '==========================================================


#Region "Jurnal"

                Dim inisial_faktur_dari As String = ""
                Dim akun_biaya As String = ""
                Dim akun_persediaan_dari As String = ""

                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging, Biaya_Pengeluaran_Barang from stock_owner_gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        akun_biaya = Dr("Biaya_Pengeluaran_Barang")
                        inisial_faktur_dari = Dr("inisial_faktur")

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select c.akun_Persediaan "
                SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.kode_stock_owner = '" & Cmb_Lokasi.Text & "' and b.Kode_Barang='" & TextBox2.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        akun_persediaan_dari = Dr("akun_Persediaan")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim pagenumber As Integer = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(Date.Now, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Adjustment Stock " & TextBox1.Text.Trim & "', '', "
                SQL = SQL & "'-', '" & UserID & "', '" & Cmb_Lokasi.Text & "')"
                ExecuteTrans(SQL)



                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akun_persediaan_dari, 1),
                              Strings.Mid(akun_persediaan_dari, 2, 1),
                              Strings.Mid(Ganti(akun_persediaan_dari), 3),
                              KodePerusahaan, KodeProyek, "Persediaan " & TextBox1.Text.Trim, total_hpp, "0", pagenumber, Cmb_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akun_biaya, 1),
                                   Strings.Mid(akun_biaya, 2, 1),
                                   Strings.Mid(Ganti(akun_biaya), 3),
                                   KodePerusahaan, KodeProyek, "Biaya " & TextBox1.Text.Trim, "0", total_hpp, pagenumber, Cmb_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                '========= 

                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("debit") <> Dr("kredit") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                'SQL = "update EMI_Restock_Barang set  "
                'SQL = SQL & "kode_Voucher = '" & Kode_Voucher & "' "
                'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and no_faktur = '" & TextBox1.Text.Trim & "' "
                'ExecuteTrans(SQL)

#End Region




            End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

#Region "Cetak Lama"


        '=================
        '=     CETAK     =
        '=================
        'Try
        '    OpenConn()
        '    Dim kertas As String = ""
        '    SQL = "select Kode_Perusahaan from View_Laporan_Actual_Biaya_Produksi where "
        '    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
        '    ' SQL = SQL & "no_faktur = '" & TxtFaktur.Text & "' "
        '    SQL = SQL & "no_faktur = 'FAB1224-00002' "
        '    Using Ds = BindingTrans(SQL)
        '        If Ds.Tables("MyTable").Rows.Count <> 0 Then


        '            Dim CrDoc = New Rpt_Laporan_Penambahan_Stock_Barang
        '            kertas = "A4"

        '            CrDoc.SetDataSource(Ds)
        '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '            CrDoc.PrintOptions.PrinterName = PrinterNameSPB
        '            CrDoc.RecordSelectionFormula = "{View_Laporan_Penambahan_Stock_Barang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_Penambahan_Stock_Barang.no_faktur} = '" & TextBox1.Text.Trim & "' "
        '            'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

        '            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        '            doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
        '            Dim rawKind As Integer
        '            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        '            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
        '                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
        '                    CrDoc.PrintOptions.PaperSize = rawKind
        '                    Exit For
        '                End If
        '            Next

        '            CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
        '            CrDoc.PrintToPrinter(1, False, 1, 99)

        '            MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Else
        '            MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        End If
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

#End Region

        '=================================
        '=     CETAK FAKTUR RESTOCK     =
        '=================================
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select Kode_Perusahaan from view_laporan_penambahan_stock_barang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TextBox1.Text.Trim & "' "

            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New Rpt_Laporan_Penambahan_Stock_Barang
                    kertas = "Faktur"

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{view_laporan_penambahan_stock_barang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {view_laporan_penambahan_stock_barang.no_faktur}='" & TextBox1.Text.Trim & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "TF"
                    '    .Text = "TF"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    CrDoc.RecordSelectionFormula = "{view_laporan_penambahan_stock_barang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {view_laporan_penambahan_stock_barang.no_faktur}='" & TextBox1.Text.Trim & "' "
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterNameTS
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)

                    MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Kosong()
        DateTimePicker1.Focus()
    End Sub



    Private Sub TextBox2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        If TextBox2.Text.Trim.Length = 0 Then Kosong() : Exit Sub
        If Btn_Simpan.Text <> "&Simpan" Then Exit Sub
        If ListView3.Focused = True Then Exit Sub


        Try

            OpenConn()

            'iniiiii
            Dim boleh_lihat As Boolean = True

            'SQL = "select flag_hide_stock, "
            'SQL = SQL & "ISNULL(("
            'SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            'SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            'SQL = SQL & "), 'T') AS boleh_lihat_stock "
            'SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "'  "
            ''SQL = SQL & "and x.kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("flag_hide_stock") = "Y" Then
            '            If Dr("boleh_lihat_stock") = "Y" Then
            '                boleh_lihat = True
            '            Else
            '                boleh_lihat = False
            '            End If
            '        Else
            '            boleh_lihat = True
            '        End If
            '    Else
            '        boleh_lihat = False
            '    End If
            'End Using

            SQL = "select a.kode_barang, a.nama, a.good_stock, a.last_hpp, a.Standar_Price, a.Metode_Pengeluaran_Stok, "
            SQL = SQL & "ISNULL(( dbo.ubah_satuan(a.Kode_Perusahaan, 'masa', a.Kode_Barang, a.Satuan, "
            SQL = SQL & "(select z.satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Flag_Tampil_Display = 'Y' and z.Kode_barang = a.Kode_Barang) "
            SQL = SQL & ", a.Good_Stock) ), '0') as Stock, "
            SQL = SQL & "a.Satuan as Satuan_Kecil, "
            SQL = SQL & "ISNULL((select Satuan from Barang_Detail_Satuan z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Barang = z.Kode_barang and z.Flag_Tampil_Display = 'Y'), '-') as Satuan_Display "
            SQL = SQL & "from barang a where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_stock_owner = '" & Cmb_Lokasi.Text & "' and "
            SQL = SQL & "a.kode_barang = '" & TextBox2.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox2.Text = Dr("kode_barang")
                    TextBox3.Text = "X" 'Dr("nama")
                    'iniiiii
                    If boleh_lihat = True Then
                        If Flag_Opname Then
                            Txt_SisaStock.Text = 0
                        Else
                            Txt_SisaStock.Text = Dr("Stock")
                        End If
                    Else
                        Txt_SisaStock.Text = ""
                    End If
                    'iniiiii
                    'TextBox4.Text = Dr("good_stock")

                    '===================
                    '=     GET HPP     =
                    '===================
                    TextBox7.Text = Dr("Standar_Price")

                    '=================================
                    '=     Cek Pengeluaran Stock     =
                    '=================================
                    MetodePengeluaranStock.Text = Dr("Metode_Pengeluaran_Stok")

                    If Dr("Metode_Pengeluaran_Stok") = "FIFO" Then

                        Dtp_TglEx.Enabled = False
                        Dtp_TglEx.Value = DateTime.Now.AddYears(MasaTahunExp)

                    ElseIf Dr("Metode_Pengeluaran_Stok") = "FEFO" Then

                        Dtp_TglProd.Enabled = True
                        Dtp_TglEx.Enabled = True

                        Dtp_TglProd.Value = DateTime.Now
                        Dtp_TglEx.Value = DateTime.Now

                    End If

                    SatuanBesar.Items.Add(Dr("Satuan_Display")) : SatuanBesar.SelectedIndex = 0
                    Txt_SatuanKecil.Text = Dr("Satuan_Kecil")


                    urutan.Text = "0"
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TextBox2.Text = "" : TextBox3.Text = "" : Txt_SisaStock.Text = "" : urutan.Text = ""
                    MessageBox.Show("Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using
            ListView3.Visible = False

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text.Trim.Length = 0 Then
            ListView3.Visible = False : Exit Sub
        Else
            ListView3.Visible = True
        End If

        Try
            OpenConn()

            Dim boleh_lihat As Boolean = True

            'SQL = "select flag_hide_stock, "
            'SQL = SQL & "ISNULL(("
            'SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            'SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            'SQL = SQL & "), 'T') AS boleh_lihat_stock "
            ''SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & " from Stock_Owner_Gudang x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "x.kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("flag_hide_stock") = "Y" Then
            '            If Dr("boleh_lihat_stock") = "Y" Then
            '                boleh_lihat = True
            '            Else
            '                boleh_lihat = False
            '            End If
            '        Else
            '            boleh_lihat = True
            '        End If
            '    Else
            '        boleh_lihat = False
            '    End If
            'End Using

            ListView3.Items.Clear()

            SQL = "Select top(20) a.kode_stock_owner, a.kode_barang, a.nama, a.good_stock, a.Satuan, "
            SQL = SQL & "ISNULL(( dbo.ubah_satuan(a.Kode_Perusahaan, 'masa', a.Kode_Barang, a.Satuan,  "
            SQL = SQL & "(select z.satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Flag_Tampil_Display = 'Y' and z.Kode_barang = a.Kode_Barang) "
            SQL = SQL & ", a.Good_Stock)  "
            SQL = SQL & "), '0') as Stock "
            SQL = SQL & "From barang a "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_stock_owner = '" & Cmb_Lokasi.Text & "' "
            SQL = SQL & "and a.kode_barang like '%" & TextBox2.Text & "%' "
            SQL = SQL & "and a.Flag_Potong_Stok = 'T' "
            SQL = SQL & "order by a.kode_stock_owner, a.kode_barang  "

            Using ds = Binding(SQL)
                With ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView3.Items.Add(.Rows(i).Item("kode_stock_owner"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_barang"))
                        Lvw.SubItems.Add("X")
                        'iniiiii
                        If boleh_lihat = True Then
                            If Flag_Opname Then
                                Lvw.SubItems.Add(0)
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("Stock"))
                            End If
                            Lvw.SubItems.Add(.Rows(i).Item("Satuan"))
                        Else
                            Lvw.SubItems.Add("-")
                            Lvw.SubItems.Add("-")
                        End If
                        'iniiiii
                        'Lvw.SubItems.Add(.Rows(i).Item("good_stock"))
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick
        If ListView3.Items.Count = 0 Then Exit Sub

        TextBox2.Text = ListView3.FocusedItem.SubItems(1).Text
        TextBox2_Leave(ListView3, e)
        TextBox2.Focus()
        TextBox5.Focus()
        ListView3.Visible = False
    End Sub

    Private Sub ListView3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView3.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView3_DoubleClick(ListView3, e)
        End If
    End Sub


    Private Sub ComboBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Lokasi.Leave
        TextBox2.Text = ""
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        For i As Integer = 0 To ListView1.Items.Count - 1

            Cmb_Lokasi.Text = ListView1.Items(i).Text
            TextBox2.Text = ListView1.Items(i).SubItems(1).Text
            TextBox2_Leave(ListView1, e)
            TextBox5.Text = Val(ListView1.Items(i).SubItems(2).Text)
            TextBox6.Text = "ADJ PLUS JAMBI"
            TextBox7.Text = ListView1.Items(i).SubItems(3).Text
            urutan.Text = ListView1.Items(i).SubItems(4).Text
            Button1_Click(ListView1, e)
        Next
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            OpenConn()

            Dim lv As New ListViewItem
            ListView1.Items.Clear()

            SQL = "select  kode_stock_owner, kode_barang,jumlah as jml, hpp, urut from EMI_Brg_Lampung where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "sudah_dist is null"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView1.Items.Add(Dr("kode_stock_owner"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("jml"))
                    lv.SubItems.Add(Dr("hpp"))
                    lv.SubItems.Add(Dr("urut"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Dtp_TglProd_ValueChanged(sender As Object, e As EventArgs) Handles Dtp_TglProd.ValueChanged
        If Not Dtp_TglProd.Checked Or MetodePengeluaranStock.Text.Trim.Length = 0 Then Exit Sub

        If MetodePengeluaranStock.Text = "FIFO" Then

            Dtp_TglEx.Value = Dtp_TglProd.Value.AddYears(MasaTahunExp)


        End If
    End Sub

    Private Sub TxtNo_Transaksi_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "select a.*,b.nama,b.good_stock from EMI_Adjustment as a inner join barang as b on a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.kode_stock_owner = b.kode_stock_owner and a.kode_barang = b.kode_barang "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_adjustment = '" & TextBox1.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("kode_adjustment")
                    DateTimePicker1.Value = Dr("tanggal")
                    For i As Integer = 0 To Cmb_Lokasi.Items.Count - 1
                        xSplit = Cmb_Lokasi.Items(i).split("-")
                        If Dr("kode_stock_owner") = xSplit(0).Trim Then
                            Cmb_Lokasi.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    TextBox2.Text = Dr("kode_barang")
                    TextBox3.Text = Dr("nama")
                    If Flag_Opname Then
                        Txt_SisaStock.Text = Dr("good_stock")
                    Else
                        Txt_SisaStock.Text = Dr("good_stock")
                    End If
                    TextBox5.Text = Dr("jumlah")
                    TextBox7.Text = Dr("harga_beli")
                    JumlahOld = Dr("jumlah")
                    TextBox6.Text = Dr("keterangan")
                    TextBox2.Enabled = False : Cmb_Lokasi.Enabled = False
                    DateTimePicker1.Enabled = False
                    Btn_Simpan.Text = "&Update"
                Else
                    Dr.Close()
                    TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(kode_adjustment,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))

                    'ComboBox1.SelectedIndex = -1
                    TextBox2.Text = "" : TextBox3.Text = "" : Txt_SisaStock.Text = "" : TextBox5.Text = "" : TextBox6.Text = ""
                    TextBox2.Enabled = True : Cmb_Lokasi.Enabled = True : DateTimePicker1.Enabled = True
                    TextBox7.Text = ""
                    Btn_Simpan.Text = "&Simpan"
                End If
            End Using
            ListView3.Visible = False

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub MetodePengeluaranStock_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Cmb_Lokasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lokasi.SelectedIndexChanged
        If Cmb_Lokasi.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                          General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
                          "Kode_perusahaan", KodePerusahaan,
                          "And", "substring(kode_adjustment,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtNo_Transaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub


    Private Sub Dtp_TglProd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dtp_TglProd.KeyPress
        If e.KeyChar = Chr(13) Then Dtp_TglEx.Focus()
    End Sub

    Private Sub Dtp_TglEx_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dtp_TglEx.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Cmb_Lokasi_MouseHover(sender As Object, e As EventArgs) Handles Cmb_Lokasi.MouseHover

    End Sub
End Class

Public Class Loading_Barang_Import

    Dim faktur As String = ""
    Dim arrInisialFaktur As String = ""

    Dim LvLokasi As String
    Dim LvKdBarang As String
    Dim LvNama As String
    Dim LvJumlahPO As String
    Dim LvJumlah As String
    Dim LvSelisih As String
    Dim LvHarga As String
    Dim Lvtotal As String
    Dim LvVolume As String
    Dim LvJmlBsr As String
    Dim LvIsiBsr As String
    Dim LvBeratBrsh As String
    Dim LvBeratKtr As String
    Dim LvTotBeratBrsh As String
    Dim LvTotBeratKtr As String
    Dim LvPjg As String
    Dim LvLbr As String
    Dim LvTinggi As String
    Dim LvUrut As String
    Dim LvMataUang As String

    Dim cellLokasi As Integer
    Dim cellKdBarang As Integer
    Dim cellNama As Integer
    Dim cellJumlahPO As Integer
    Dim cellJumlah As Integer
    Dim cellSelisih As Integer
    Dim cellHarga As Integer
    Dim celltotal As Integer
    Dim cellVolume As Integer
    Dim cellJmlBsr As Integer
    Dim cellIsiBsr As Integer
    Dim cellBeratBrsh As Integer
    Dim cellBeratKtr As Integer
    Dim cellTotBeratBrsh As Integer
    Dim cellTotBeratKtr As Integer
    Dim cellPjg As Integer
    Dim cellLbr As Integer
    Dim cellTinggi As Integer
    Dim cellUrut As Integer
    Dim cellMataUang As Integer
    Dim hitung As Long = 0

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvLokasi = DataGridView1.Rows(No_Index).Cells(0).Value.ToString : cellLokasi = 0
        LvKdBarang = DataGridView1.Rows(No_Index).Cells(1).Value.ToString : cellKdBarang = 1
        LvNama = DataGridView1.Rows(No_Index).Cells(2).Value.ToString : cellNama = 2
        LvJumlahPO = DataGridView1.Rows(No_Index).Cells(3).Value.ToString : cellJumlahPO = 3
        LvJumlah = DataGridView1.Rows(No_Index).Cells(4).Value.ToString : cellJumlah = 4
        LvSelisih = DataGridView1.Rows(No_Index).Cells(5).Value.ToString : cellSelisih = 5
        LvHarga = DataGridView1.Rows(No_Index).Cells(6).Value.ToString : cellHarga = 6
        Lvtotal = DataGridView1.Rows(No_Index).Cells(7).Value.ToString : celltotal = 7
        LvVolume = DataGridView1.Rows(No_Index).Cells(8).Value.ToString : cellVolume = 8
        LvJmlBsr = DataGridView1.Rows(No_Index).Cells(9).Value.ToString : cellJmlBsr = 9
        LvIsiBsr = DataGridView1.Rows(No_Index).Cells(10).Value.ToString : cellIsiBsr = 10
        LvBeratBrsh = DataGridView1.Rows(No_Index).Cells(11).Value.ToString : cellBeratBrsh = 11
        LvBeratKtr = DataGridView1.Rows(No_Index).Cells(12).Value.ToString : cellBeratKtr = 12
        LvTotBeratBrsh = DataGridView1.Rows(No_Index).Cells(13).Value.ToString : cellTotBeratBrsh = 13
        LvTotBeratKtr = DataGridView1.Rows(No_Index).Cells(14).Value.ToString : cellTotBeratKtr = 14
        LvPjg = DataGridView1.Rows(No_Index).Cells(15).Value.ToString : cellPjg = 15
        LvLbr = DataGridView1.Rows(No_Index).Cells(16).Value.ToString : cellLbr = 16
        LvTinggi = DataGridView1.Rows(No_Index).Cells(17).Value.ToString : cellTinggi = 17
        LvUrut = DataGridView1.Rows(No_Index).Cells(18).Value.ToString : cellUrut = 18
        LvMataUang = DataGridView1.Rows(No_Index).Cells(19).Value.ToString : cellMataUang = 19

    End Sub

    Private Sub HitungGrand()

        Dim ttl As Double = 0

        For i As Integer = 0 To ListView2.Items.Count - 1
            ttl = ttl + Val(HilangkanTanda(ListView2.Items(i).SubItems(5).Text))
        Next

        'TextBox3.Text = Format(ttl, "N2")
    End Sub

    Private Sub get_Faktur()
        faktur = FLB & arrInisialFaktur & "-" & Format(Tanggal_Sekarang, "MM/yy") & "-" & _
                            General_Class.Get_Last_Number2("Loading_Barang", "No_Faktur", JumlahDigit, _
                            "Kode_perusahaan", KodePerusahaan, _
                            "And", "substring(No_Faktur,1," & Len(FLB) + Len(arrInisialFaktur) + 6 & ")", _
                             FLB & arrInisialFaktur & "-" & Format(Tanggal_Sekarang, "MM/yy"))

    End Sub

    Public Sub get_Kontainer()
        Try
            OpenConn()

            ListView3.Items.Clear()
            SQL = "select No_Container, No_Seal, Tgl_Muat from kontainer_masuk where No_Faktur = '" & Txtfaktur.Text & "' "
            SQL = SQL & "group by No_Container, No_Seal, Tgl_Muat "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView3.Items.Add(Dr("No_Container"))
                    Lvw.SubItems.Add(Dr("No_Seal"))
                    Lvw.SubItems.Add(Format(Dr("Tgl_Muat"), "dd-MMM-yyyy"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Public Sub Cek_Bahan()
        Try
            OpenConn()


            ListView1.Items.Clear()
            ListView2.Items.Clear()

            SQL = "select c.Kode_stock_owner_import, c.nama_Bahan, a.kode_bahan, sum((Qty/d.isi_satuan_besar)*Qty_Bahan)  "
            SQL = SQL & "as tot_sat_bsr, c.Kategori, c.Flag_Potong_Stock, C.Mata_Uang from detail_komposisi_barang_jadi a, "
            SQL = SQL & "Kontainer_masuk b, detail_submit_PO d, Bahan_Import c where a.kode_perusahaan = '" & KodePerusahaan & "' and b.No_Faktur = '" & Txtfaktur.Text & "' "
            SQL = SQL & "and a.kode_perusahaan = b.Kode_perusahaan and a.kode_barang = b.Kode_Barang and "
            SQL = SQL & "a.kode_Bahan = c.Kode_Bahan and c.Kode_stock_owner_import = '" & TextBox2.Text & "' "
            SQL = SQL & "and b.no_faktur = d.No_Faktur and b.Kode_barang = d.Kode_Barang and b.Kode_Stock_Owner = d.Kode_Stock_Owner "
            SQL = SQL & "group by c.Kode_stock_owner_import, c.nama_Bahan, a.kode_bahan, c.Kategori, c.Flag_Potong_Stock, C.Mata_Uang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1

                        Dim tot As Double = 0
                        tot = HilangkanTanda(Format(.Rows(i).Item("tot_sat_bsr"), "N5"))
                        Dim sn As String = "''"


                        'If .Rows(0).Item("Mata_Uang") <> .Rows(i).Item("Mata_Uang") Then
                        '    CloseConn()
                        '    MessageBox.Show("Ada Mata Uang yang Berbeda pada Bahan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    ListView2.Items.Clear()
                        '    Exit Sub
                        'End If


                        Dim Lvw As ListViewItem
                        Lvw = ListView2.Items.Add(.Rows(i).Item("Kode_stock_owner_import"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_bahan"))
                        Lvw.SubItems.Add(.Rows(i).Item("nama_bahan"))
                        Lvw.SubItems.Add(.Rows(i).Item("tot_sat_bsr"))


                        If .Rows(i).Item("Flag_Potong_Stock") = "Y" Then


                            For k As Integer = 0 To 100

                                If tot <= 0 Then
                                    Exit For
                                End If

                                SQL = "select top(1) Jumlah, serial_number "
                                SQL = SQL & "from bahan_sn where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_bahan = '" & .Rows(i).Item("kode_bahan") & "' and jumlah <> 0 and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                                SQL = SQL & "and serial_number not in(" & sn & ") order by tgl_masuk "
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                            tot = tot - Ds2.Tables("MyTable").Rows(j).Item("Jumlah")

                                            sn = sn & ", '" & Ds2.Tables("MyTable").Rows(j).Item("serial_number") & "'"
                                        Next
                                    Else
                                        MessageBox.Show("Stok " & .Rows(i).Item("nama_bahan") & " Kurang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit For
                                    End If
                                End Using

                            Next


                            SQL = "select good_stock from bahan_import "
                            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Bahan = '" & .Rows(i).Item("kode_bahan") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    If (dr("good_stock") - HilangkanTanda(Format(.Rows(i).Item("tot_sat_bsr"), "N5"))) < 0 Then
                                        CloseConn()
                                        MessageBox.Show("Stock " & .Rows(i).Item("nama_bahan") & " tidak cukup!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        ListView2.Items.Clear()
                                        Exit Sub
                                    End If
                                Else
                                    CloseConn()
                                    MessageBox.Show(.Rows(i).Item("kode_bahan") & " tidak ditemukan!")
                                    DataGridView1.Rows.Clear()
                                    ListView1.Items.Clear()
                                    ListView2.Items.Clear()
                                    ListView3.Items.Clear()
                                    Exit Sub
                                End If
                            End Using


                            SQL = "select Mata_Uang, avg(Harga) as avg from bahan_sn"
                            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Bahan = '" & .Rows(i).Item("kode_bahan") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                            SQL = SQL & " and serial_number in (" & sn & ") group by Mata_Uang"
                            Using dss = BindingTrans(SQL)

                                If dss.Tables("MyTable").Rows.Count <> 0 Then

                                    If dss.Tables("MyTable").Rows.Count > 1 Then
                                        CloseConn()
                                        MessageBox.Show("Terdapat 2 Mata_Uang Di Bahan Yang Sama")
                                        DataGridView1.Rows.Clear()
                                        ListView1.Items.Clear()
                                        ListView2.Items.Clear()
                                        ListView3.Items.Clear()
                                        Exit Sub
                                    End If

                                    For index As Integer = 0 To dss.Tables("MyTable").Rows.Count - 1
                                        Lvw.SubItems.Add(Format(dss.Tables("MyTable").Rows(index).Item("avg"), setN))
                                        Lvw.SubItems.Add(Format((Format(dss.Tables("MyTable").Rows(index).Item("avg"), setN) * .Rows(i).Item("tot_sat_bsr")), setN))
                                        Lvw.SubItems.Add(dss.Tables("MyTable").Rows(index).Item("Mata_Uang"))
                                    Next


                                Else
                                    CloseConn()
                                    MessageBox.Show("error Pada Perhitungan")
                                    DataGridView1.Rows.Clear()
                                    ListView1.Items.Clear()
                                    ListView2.Items.Clear()
                                    ListView3.Items.Clear()
                                    Exit Sub
                                End If
                            End Using



                        ElseIf .Rows(i).Item("Flag_Potong_Stock") = "T" Then

                            SQL = "select Harga, Mata_Uang from bahan_import "
                            SQL = SQL & " where Kode_Bahan = '" & .Rows(i).Item("kode_bahan") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    Lvw.SubItems.Add(Format(dr("Harga"), setN))
                                    Lvw.SubItems.Add(Format((Format(dr("Harga"), setN) * .Rows(i).Item("tot_sat_bsr")), setN))
                                    Lvw.SubItems.Add(dr("Mata_Uang"))
                                Else
                                    CloseConn()
                                    MessageBox.Show("error Pada Perhitungan")
                                    DataGridView1.Rows.Clear()
                                    ListView1.Items.Clear()
                                    ListView2.Items.Clear()
                                    ListView3.Items.Clear()
                                    Exit Sub
                                End If
                            End Using



                        End If

                        Lvw.SubItems.Add(.Rows(i).Item("Kategori"))
                        Lvw.SubItems.Add(.Rows(i).Item("Flag_Potong_Stock"))
                    Next
                End With
            End Using


            SQL = "select b.kode_barang, a.kode_bahan, a.qty_bahan, (sum(Qty)/d.isi_satuan_besar) as Jml_Sat_Bsr, round((sum(Qty)/d.isi_satuan_besar)*Qty_Bahan,0) as tot_sat_bsr, b.Kode_Stock_Owner "
            SQL = SQL & "from detail_komposisi_barang_jadi a, Kontainer_Masuk b, detail_submit_PO d where a.kode_perusahaan = '" & KodePerusahaan & "' and b.No_faktur = '" & Txtfaktur.Text & "'  "
            SQL = SQL & "and a.kode_perusahaan = b.Kode_perusahaan and a.kode_barang = b.Kode_Barang and b.no_faktur = d.No_Faktur and b.Kode_barang = d.Kode_Barang and b.Kode_Stock_Owner = d.Kode_Stock_Owner "
            SQL = SQL & "group by b.Kode_Barang, a.Kode_Bahan, a.qty_Bahan, d.isi_satuan_besar, b.Kode_Stock_Owner "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("kode_barang"))
                    Lvw.SubItems.Add(dr("kode_bahan"))
                    Lvw.SubItems.Add(dr("qty_bahan"))
                    Lvw.SubItems.Add(Math.Ceiling(dr("Jml_Sat_Bsr")))
                    Lvw.SubItems.Add(dr("tot_sat_bsr"))
                    Lvw.SubItems.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Public Sub get_ubah(ByVal index As Integer, ByVal qty As Integer)
        Get_Isi_Listview(index)

        DataGridView1.Rows.Item(index).Cells(cellJumlah).Value = LvJumlah + qty

        Get_Isi_Listview(index)
        DataGridView1.Rows.Item(index).Cells(cellSelisih).Value = LvJumlahPO - LvJumlah
        DataGridView1.Rows.Item(index).Cells(celltotal).Value = Format(LvHarga * LvJumlah, setN)
        DataGridView1.Rows.Item(index).Cells(cellVolume).Value = Format((LvPjg * LvLbr * LvTinggi) * Math.Ceiling((LvJumlah / LvIsiBsr)), "N0")
        DataGridView1.Rows.Item(index).Cells(cellJmlBsr).Value = Math.Ceiling((LvJumlah / LvIsiBsr))
        DataGridView1.Rows.Item(index).Cells(cellTotBeratBrsh).Value = Format(LvBeratBrsh * LvJumlah, "N0")
        DataGridView1.Rows.Item(index).Cells(cellTotBeratKtr).Value = Format(LvBeratKtr * LvJumlah, "N0")


    End Sub

    Private Sub Loading_Barang_Import_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Rencana_order_Biaya_Import_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()

    End Sub

    Public Sub Kosong()
        TextBoxRV.Text = ""
        TxtContainer.Text = ""
        TxtId_Rencana.Text = ""
        Txtfaktur.Text = ""
        TxtSupplier.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TxtJumlah_conte.Text = ""
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
        DtTanggal_Po.Value = FMenu.ToolStripStatusLabel3.Text
        DateTimePicker1.Value = FMenu.ToolStripStatusLabel3.Text

        DataGridView1.Rows.Clear()
        ListView2.Clear()
        ListView2.Columns.Add("Lokasi Import", 140, HorizontalAlignment.Left)
        ListView2.Columns.Add("Kode Bahan", 130, HorizontalAlignment.Left)
        ListView2.Columns.Add("Nama Bahan", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Jumlah", 130, HorizontalAlignment.Center)
        ListView2.Columns.Add("Harga", 130, HorizontalAlignment.Right)
        ListView2.Columns.Add("Total", 130, HorizontalAlignment.Right)
        ListView2.Columns.Add("Mata Uang", 130, HorizontalAlignment.Center)
        ListView2.Columns.Add("Jenis", 130, HorizontalAlignment.Center)
        ListView2.Columns.Add("Flag_Pot_Stock", 0, HorizontalAlignment.Center)
        ListView2.View = View.Details
        ListView2.Size = New Size(998, 138)

        ListView1.Clear()
        ListView1.Columns.Add("Kode Barang", 140, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode Bahan", 140, HorizontalAlignment.Left)
        ListView1.Columns.Add("Qty Bahan", 140, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jumlah Satuan Besar", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jumlah Butuh", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Lokasi", 130, HorizontalAlignment.Center)

        ListView1.View = View.Details

        ListView3.Clear()
        ListView3.Columns.Add("No Kontainer", 120, HorizontalAlignment.Left)
        ListView3.Columns.Add("No Seal", 130, HorizontalAlignment.Left)
        ListView3.Columns.Add("Tanggal Muat", 120, HorizontalAlignment.Center)

        ListView3.View = View.Details

        ComboBox3.Items.Clear()
        ComboBox3.Items.Add("T")
        ComboBox3.Items.Add("N")
        ComboBox4.Items.Clear()
        Try
            OpenConn()

            CmbLokasi.Items.Clear()
            SQL = "Select Kode_stock_owner From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            CmbLokasi.Text = Lokasi

            ComboBox1.Items.Clear()
            SQL = "Select Kode_mata_uang From mata_uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_mata_uang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Kode_Mata_Uang"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub TxtNo_Faktur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then CmbLokasi.Focus()
    End Sub

    Private Sub TxtNo_PO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then BtnSimpan.Focus()
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Kosong()
    End Sub

    Private Sub BtCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCari.Click
        Display_PO_Loading_Barang.ShowDialog()
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        '''PERUBAHAN PADA EMI
        '''If ListView2.Items.Count = 0 Then
        '''    MessageBox.Show("Data Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''    Exit Sub
        '''ElseIf ListView1.Items.Count = 0 Then
        '''    MessageBox.Show("Data Tidak Ada!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''    Exit Sub
        '''Else

        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Mata Uang Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("No Rekening Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show("Jenis_Transaksi Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus()
            Exit Sub
        End If

        If CheckBox1.Checked = True Then
            If ComboBox4.SelectedIndex = -1 Then
                MessageBox.Show("Lokasi Transit Harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox3.Focus()
                Exit Sub
            End If
        End If

        GetTime()
        Dim GrandTotal As Double = 0
        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            SQL = "select inisial_faktur from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_stock_Owner = '" & CmbLokasi.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    arrInisialFaktur = dr("inisial_faktur")
                Else
                    CloseConn()
                    MessageBox.Show("Inisial Faktur Tidak ditemukan")
                    Exit Sub
                End If
            End Using


            get_Faktur()

            SQL = "select cast(rv as bigint) as rv, lokasi from rencana_order ro where "
            SQL = SQL & "id_rencana = '" & TxtId_Rencana.Text & "' and selesai is null and "
            SQL = SQL & "status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("rv") <> TextBoxRV.Text Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah diubah sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    ElseIf Dr("lokasi").ToString.ToUpper <> CmbLokasi.Text.ToUpper Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi kesalahan pada lokasi! Harap login ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Rencana order tidak ditemukan/sudah selesai/sudah batal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using

            Dim count_barang As Integer = DataGridView1.Rows.Count
            SQL = "select count(Kode_barang) as count from detail_rencana_Order "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Id_rencana = '" & TxtId_Rencana.Text & "' and jumlah_po <> 0 "
            'SQL = SQL & "group by Kode_Barang, Kode_Stock_Owner "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("count") <> count_barang Then
                        CloseConn()
                        MessageBox.Show("jenis Barang Tidak Sama dengan Rencana Order!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak ditemukan")
                    Exit Sub
                End If
            End Using

            SQL = "select count(distinct Kode_barang) as count from Kontainer_Masuk "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txtfaktur.Text & "' "
            'SQL = SQL & "group by Kode_Barang, Kode_Stock_Owner "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("count") <> count_barang Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("jenis Barang Tidak Sama dengan Kontainer Masuk!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak ditemukan")
                    Exit Sub
                End If
            End Using

            Dim selisih_barang As Boolean = True
            Dim ind As Integer = 0
            Dim pesan As String = ""
            SQL = "select B.nama,jumlah- isnull((select Sum(Qty) from Kontainer_Masuk Z where Z.Kode_Perusahaan = a.Kode_Perusahaan and "
            SQL = SQL & "Z.No_Faktur = a.No_Faktur and Z.Kode_Barang = A.Kode_Barang and Z.Kode_Stock_Owner = A.Kode_Stock_Owner),0) as Jumlah "
            SQL = SQL & "from detail_submit_po A, Barang B where A.no_faktur = '" & Txtfaktur.Text & "' and A.Kode_Perusahaan='" & KodePerusahaan & "'  "
            SQL = SQL & "and A.Kode_Barang= B.Kode_Barang and a.Kode_Stock_Owner=b.Kode_Stock_Owner and a.Kode_Perusahaan= b.Kode_Perusahaan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    If dr("jumlah") <> 0 Then
                        selisih_barang = False
                        If ind = 0 Then
                            pesan = pesan & "Terdapat Selisih Loading dengan Submit : " & Chr(13)
                        End If
                        pesan = pesan & "  - " & dr("nama") & ", Selisih : " & dr("jumlah") & Chr(13)
                        ind += 1
                        'CloseConn()
                        'MessageBox.Show("terdapat selisih qty pada kontainer masuk!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'Exit Sub
                    End If
                Loop
            End Using


            If selisih_barang = False Then
                Dim tny As String = MessageBox.Show(pesan & Chr(13) & "Apakah akan tetap di lanjutkan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                If tny = vbNo Then
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                Else
                    SQL = "select B.Kode_Barang, B.nama, B.Kode_Stock_Owner, a.Isi_Satuan_Besar, jumlah- isnull((select Sum(Qty) from Kontainer_Masuk Z where Z.Kode_Perusahaan = a.Kode_Perusahaan and "
                    SQL = SQL & "Z.No_Faktur = a.No_Faktur and Z.Kode_Barang = A.Kode_Barang and Z.Kode_Stock_Owner = A.Kode_Stock_Owner),0) as Jumlah "
                    SQL = SQL & "from detail_submit_po A, Barang B where A.no_faktur = '" & Txtfaktur.Text & "' and A.Kode_Perusahaan='" & KodePerusahaan & "'  "
                    SQL = SQL & "and A.Kode_Barang= B.Kode_Barang and a.Kode_Stock_Owner=b.Kode_Stock_Owner and a.Kode_Perusahaan= b.Kode_Perusahaan "
                    Using ds = BindingTrans(SQL)
                        With ds.Tables("MyTable")

                            For index As Integer = 0 To .Rows.Count - 1
                                If .Rows(index).Item("jumlah") <> 0 Then

                                    If .Rows(index).Item("jumlah") Mod .Rows(index).Item("Isi_Satuan_Besar") <> 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Selisih Tidak Bisa dibagi satuan besar!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    SQL = "select a.Kode_Perusahaan from detail_rencana_Order b, rencana_order a "
                                    SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Id_rencana = '" & TxtId_Rencana.Text & "' and jumlah_po <> 0 "
                                    SQL = SQL & "and a.status is null and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_rencana= b.Id_rencana and "
                                    SQL = SQL & "b.Kode_Barang ='" & .Rows(index).Item("Kode_Barang") & "' and b.Kode_stock_owner = '" & .Rows(index).Item("Kode_STock_Owner") & "'"
                                    Using dr = OpenTrans(SQL)
                                        If dr.Read Then
                                            dr.Close()
                                            SQL = "Update Detail_rencana_Order set Jumlah_PO = Jumlah_PO - " & .Rows(index).Item("jumlah") & " "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Id_rencana = '" & TxtId_Rencana.Text & "' and "
                                            SQL = SQL & "Kode_Barang ='" & .Rows(index).Item("Kode_Barang") & "' and Kode_stock_owner = '" & .Rows(index).Item("Kode_STock_Owner") & "' "
                                            ExecuteTrans(SQL)
                                        Else
                                            CloseConn()
                                            MessageBox.Show("Data Tidak ditemukan")
                                            Exit Sub
                                        End If
                                    End Using

                                    SQL = "select a.Kode_Perusahaan from detail_submit_PO b, submit_po a "
                                    SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Txtfaktur.Text & "' "
                                    SQL = SQL & "and a.status is null and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur= b.No_Faktur and "
                                    SQL = SQL & "b.Kode_Barang ='" & .Rows(index).Item("Kode_Barang") & "' and b.Kode_stock_owner = '" & .Rows(index).Item("Kode_STock_Owner") & "'"
                                    Using dr = OpenTrans(SQL)
                                        If dr.Read Then
                                            dr.Close()
                                            SQL = "Update detail_submit_po set Jumlah = Jumlah - " & .Rows(index).Item("jumlah") & " "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txtfaktur.Text & "' and "
                                            SQL = SQL & "Kode_Barang ='" & .Rows(index).Item("Kode_Barang") & "' and Kode_stock_owner = '" & .Rows(index).Item("Kode_STock_Owner") & "' "
                                            ExecuteTrans(SQL)

                                            SQL = "update Detail_Submit_PO set Total=jumlah*Harga_Declare, "
                                            SQL = SQL & "Jml_Satuan_Besar=round(Jumlah/Isi_Satuan_Besar,0), "
                                            SQL = SQL & "Total_Berat_Bersih=Jumlah*Berat_Bersih, Total_Berat_Kotor=Jumlah*Berat_Kotor "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txtfaktur.Text & "' and "
                                            SQL = SQL & "Kode_Barang ='" & .Rows(index).Item("Kode_Barang") & "' and Kode_stock_owner = '" & .Rows(index).Item("Kode_STock_Owner") & "' "
                                            ExecuteTrans(SQL)
                                        Else
                                            CloseConn()
                                            MessageBox.Show("Data Tidak ditemukan")
                                            Exit Sub
                                        End If
                                    End Using
                                End If

                            Next

                        End With
                    End Using
                End If

            End If


            SQL = "insert into Loading_Barang(kode_perusahaan, no_faktur, Id_rencana, tanggal, jam, UserID, Jenis_Transaksi, "
            SQL = SQL & "Mata_Uang, No_Rekening, Kode_Supplier, Kurs, Grand_total, No_Submit_PO"

            If CheckBox1.Checked = True Then
                SQL = SQL & ",lokasi_transit) "
            Else
                SQL = SQL & ") "
            End If

            SQL = SQL & "values('" & KodePerusahaan & "','" & faktur & "','" & TxtId_Rencana.Text & "', '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "',"
            SQL = SQL & "'" & UserID & "', '" & ComboBox3.Text & "', '" & ComboBox1.Text & "', "
            SQL = SQL & "'" & ComboBox2.Text & "', '" & TextBox1.Text & "', null, null, '" & Txtfaktur.Text & "'"
            If CheckBox1.Checked = True Then
                SQL = SQL & ",'" & ComboBox4.Text.Trim & "') "
            Else
                SQL = SQL & ") "
            End If
            ExecuteTrans(SQL)


            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(i)

                If LvJumlah = 0 Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Ada Barang yang belum di input!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim jns_brg As String = ""
                SQL = "select b.jenis from barang a, kategori_besar b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and "
                SQL = SQL & "a.kode_kategori_besar = b.kode_kategori_besar and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.kode_stock_owner = '" & LvLokasi & "' and "
                SQL = SQL & "a.kode_barang = '" & LvKdBarang & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If General_Class.CekNULL(dr("jenis")) = "" Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jenis barang belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            jns_brg = dr("jenis")
                        End If
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jenis barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "insert into detail_Loading_Barang (kode_perusahaan, No_faktur, Kode_Stock_Owner, Kode_Barang, Jumlah_PO, Jumlah, "
                SQL = SQL & "Harga_Declare, Total, Volume, Jml_Satuan_Besar, Isi_Satuan_Besar, Berat_Bersih, Berat_Kotor, Total_Berat_Bersih, "
                SQL = SQL & " Total_Berat_kotor, Panjang, Lebar, Tinggi, Urut_Submit_PO, Mata_Uang, jenis) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & faktur & "', "
                SQL = SQL & "'" & LvLokasi & "', '" & LvKdBarang & "', "
                SQL = SQL & "'" & LvJumlahPO & "', '" & LvJumlah & "', '" & HilangkanTanda(LvHarga) & "', "
                SQL = SQL & "'" & HilangkanTanda(Lvtotal) & "', '" & HilangkanTanda(LvVolume) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvJmlBsr) & "', '" & LvIsiBsr & "', '" & HilangkanTanda(LvBeratBrsh) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvBeratKtr) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvTotBeratBrsh) & "', '" & HilangkanTanda(LvTotBeratKtr) & "', '" & LvPjg & "', "
                SQL = SQL & "'" & LvLbr & "', '" & LvTinggi & "', '" & LvUrut & "', '" & LvMataUang & "', '" & jns_brg & "')"
                ExecuteTrans(SQL)
            Next


            SQL = "select a.no_container, b.jenis, round(sum((a.qty / b.Isi_Satuan_Besar) / d.Jumlah_Per_Konte * 100), 2) as ttl from "
            SQL = SQL & "Kontainer_Masuk a, Detail_Loading_Barang b, Loading_Barang c, Detail_Rencana_Order d, rencana_order e where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and "
            SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and "
            SQL = SQL & "b.No_Faktur = c.No_Faktur and c.Id_Rencana = e.ID_Rencana and "
            SQL = SQL & "e.ID_Rencana = d.ID_Rencana and "
            SQL = SQL & "a.no_faktur = c.no_submit_po and "
            SQL = SQL & "b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.No_Faktur = '" & Txtfaktur.Text & "' "
            SQL = SQL & "group by a.no_container, b.jenis"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            SQL = "insert into Kontainer_Masuk_Per_Jenis (kode_perusahaan, no_faktur, no_container, jenis, persentase) values("
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Txtfaktur.Text & "', '" & .Rows(i).Item("no_container") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("jenis") & "', "
                            SQL = SQL & "'" & HilangkanTanda(Format(.Rows(i).Item("ttl"), "N2")) & "')"
                            ExecuteTrans(SQL)
                        Next
                    Else

                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Persentase jenis barang per kontainer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using


            Dim pembulatan_persen_konte As Double = 0

            SQL = ";with cte as"
            SQL = SQL & "("
            SQL = SQL & "select a.no_container, b.jenis, round(sum((a.qty / b.Isi_Satuan_Besar) / d.Jumlah_Per_Konte * 100), 2) as ttl from "
            SQL = SQL & "Kontainer_Masuk a, Detail_Loading_Barang b, Loading_Barang c, Detail_Rencana_Order d, rencana_order e where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and "
            SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and "
            SQL = SQL & "b.No_Faktur = c.No_Faktur and c.Id_Rencana = e.ID_Rencana and "
            SQL = SQL & "e.ID_Rencana = d.ID_Rencana and "
            SQL = SQL & "a.no_faktur = c.no_submit_po and "
            SQL = SQL & "b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.No_Faktur = '" & Txtfaktur.Text & "' "
            SQL = SQL & "group by a.no_container, b.jenis"
            SQL = SQL & "),"

            SQL = SQL & "cte_pembulatan as"
            SQL = SQL & "("
            SQL = SQL & "select no_container, round(100 - sum(ttl), 2) as pembulatan "
            SQL = SQL & "from cte group by no_container "
            SQL = SQL & ")"

            SQL = SQL & "select * from cte_pembulatan"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            pembulatan_persen_konte = .Rows(i).Item("pembulatan")

                            If pembulatan_persen_konte <> 0 Then
                                SQL = "select top(1) * from Kontainer_Masuk_Per_Jenis where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur = '" & Txtfaktur.Text & "' and No_Container = '" & .Rows(i).Item("no_container") & "' order by no_container"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dim jenis As String = Dr("Jenis")
                                        Dr.Close()

                                        SQL = "update Kontainer_Masuk_Per_Jenis set persentase = persentase + (" & pembulatan_persen_konte & ") where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and Jenis = '" & jenis & "' and "
                                        SQL = SQL & "no_faktur = '" & Txtfaktur.Text & "' and No_Container = '" & .Rows(i).Item("no_container") & "'"
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Persentase jenis barang per kontainer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "select isnull(round(sum(persentase), 0), 0) as ttl_persentase from Kontainer_Masuk_Per_Jenis where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur = '" & Txtfaktur.Text & "' and No_Container = '" & .Rows(i).Item("no_container") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        If Dr("ttl_persentase") <> 100 Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi kesalahan pada pembulatan persentase!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Persentase jenis barang per kontainer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using
                            End If
                        Next
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pembulatan persentase tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using



            For index As Integer = 0 To ListView2.Items.Count - 1
                Dim HargaTot As Double = 0
                SQL = "select c.Kode_stock_owner_import,d.Kode_Barang, d.Kode_Stock_Owner, c.nama_Bahan, a.kode_bahan, sum((Qty/d.isi_satuan_besar)*Qty_Bahan)  "
                SQL = SQL & "as tot_sat_bsr, c.Kategori, c.Flag_Potong_Stock, C.Mata_Uang from detail_komposisi_barang_jadi a, "
                SQL = SQL & "Kontainer_masuk b, detail_submit_PO d, Bahan_Import c where b.kode_perusahaan = '" & KodePerusahaan & "' and b.No_Faktur = '" & Txtfaktur.Text & "' "
                SQL = SQL & "and a.kode_perusahaan = b.Kode_perusahaan and a.kode_barang = b.Kode_Barang and "
                SQL = SQL & "a.kode_Bahan = c.Kode_Bahan and c.Kode_stock_owner_import = '" & TextBox2.Text & "' and a.Kode_Bahan = '" & ListView2.Items(index).SubItems(1).Text & "' "
                SQL = SQL & "and b.no_faktur = d.No_Faktur and b.Kode_barang = d.Kode_Barang and b.Kode_Stock_Owner = d.Kode_Stock_Owner "
                SQL = SQL & "group by c.Kode_stock_owner_import,d.Kode_Barang, d.Kode_Stock_Owner, c.nama_Bahan, a.kode_bahan, c.Kategori, c.Flag_Potong_Stock, C.Mata_Uang "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim tot As Double = 0
                            Dim CekTotal As Double = 0
                            'Dim CekBahan As Double = 0

                            tot = .Rows(i).Item("tot_sat_bsr")
                            CekTotal = .Rows(i).Item("tot_sat_bsr")
                            Dim sn As String = ""



                            If .Rows(i).Item("Flag_Potong_Stock") = "Y" Then

                                SQL = "delete from Log_Loading_Barang where UserID = '" & UserID & "'"
                                ExecuteTrans(SQL)

                                For k As Integer = 0 To 100

                                    If tot = 0 Then
                                        Exit For
                                    End If

                                    If tot < 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Stok minus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If


                                    SQL = "select top(1) Jumlah, serial_number, Harga "
                                    SQL = SQL & "from bahan_sn where kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_bahan = '" & .Rows(i).Item("kode_bahan") & "' and jumlah <> 0 and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                                    SQL = SQL & "order by tgl_masuk "
                                    Using Ds2 = BindingTrans(SQL)
                                        If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                            For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                                If tot > Ds2.Tables("MyTable").Rows(j).Item("Jumlah") Then

                                                    tot = tot - Ds2.Tables("MyTable").Rows(j).Item("Jumlah")

                                                    'CekBahan = CekBahan + Ds2.Tables("MyTable").Rows(j).Item("Jumlah")
                                                    SQL = "insert into Log_Loading_Barang(Kode_Perusahaan, UserID, Kode_Bahan, Jumlah, Total_Awal) Values "
                                                    SQL = SQL & " ('" & KodePerusahaan & "', '" & UserID & "',  "
                                                    SQL = SQL & "'" & .Rows(i).Item("kode_bahan") & "', " & Ds2.Tables("MyTable").Rows(j).Item("Jumlah") & ", " & .Rows(i).Item("tot_sat_bsr") & ")"
                                                    ExecuteTrans(SQL)

                                                    HargaTot = HargaTot + (Ds2.Tables("MyTable").Rows(j).Item("Jumlah") * Ds2.Tables("MyTable").Rows(j).Item("Harga"))

                                                    SQL = "update bahan_sn set jumlah = jumlah - " & Ds2.Tables("MyTable").Rows(j).Item("Jumlah")
                                                    SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and  serial_number = '" & Ds2.Tables("MyTable").Rows(j).Item("serial_number") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                                                    ExecuteTrans(SQL)

                                                    SQL = "insert into det_Loading_barang(Kode_Perusahaan, No_Faktur,  Kode_Stock_Owner_Import, KOde_Barang, Kode_Stock_Owner, Kode_Bahan, Serial_number, Jumlah) Values "
                                                    SQL = SQL & " ('" & KodePerusahaan & "', '" & faktur & "', '" & .Rows(i).Item("Kode_stock_owner_import") & "', '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Kode_Stock_Owner") & "', "
                                                    SQL = SQL & "'" & .Rows(i).Item("kode_bahan") & "', '" & Ds2.Tables("MyTable").Rows(j).Item("serial_number") & "', '" & Ds2.Tables("MyTable").Rows(j).Item("Jumlah") & "')"
                                                    ExecuteTrans(SQL)

                                                ElseIf tot <= Ds2.Tables("MyTable").Rows(j).Item("Jumlah") Then

                                                    SQL = "update bahan_sn set jumlah = jumlah - " & tot
                                                    SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and serial_number = '" & Ds2.Tables("MyTable").Rows(j).Item("serial_number") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                                                    ExecuteTrans(SQL)

                                                    HargaTot = HargaTot + (tot * Ds2.Tables("MyTable").Rows(j).Item("Harga"))

                                                    SQL = "insert into det_Loading_Barang(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner_Import, Kode_Barang, Kode_Stock_Owner, Kode_Bahan, Serial_number, Jumlah) Values "
                                                    SQL = SQL & " ('" & KodePerusahaan & "', '" & faktur & "', '" & .Rows(i).Item("Kode_stock_owner_import") & "', '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Kode_Stock_Owner") & "', "
                                                    SQL = SQL & "'" & .Rows(i).Item("kode_bahan") & "', '" & Ds2.Tables("MyTable").Rows(j).Item("serial_number") & "', '" & tot & "')"
                                                    ExecuteTrans(SQL)

                                                    SQL = "insert into Log_Loading_Barang(Kode_Perusahaan, UserID, Kode_Bahan, Jumlah, Total_Awal) Values "
                                                    SQL = SQL & " ('" & KodePerusahaan & "', '" & UserID & "',  "
                                                    SQL = SQL & "'" & .Rows(i).Item("kode_bahan") & "', " & tot & ", " & .Rows(i).Item("tot_sat_bsr") & ")"
                                                    ExecuteTrans(SQL)
                                                    'CekBahan = CekBahan + tot

                                                    tot = tot - tot

                                                End If

                                            Next

                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Masih Ada Stok Kurang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using

                                Next
                                '
                                SQL = ";with cte_a as( select Total_Awal, sum(Jumlah) as Total_Akhir from Log_Loading_Barang "
                                SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Bahan = '" & .Rows(i).Item("kode_bahan") & "' and UserID = '" & UserID & "'"
                                SQL = SQL & " Group by Total_Awal )"
                                SQL = SQL & " select Total_Awal - Total_Akhir as Total from cte_a"
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then

                                        If dr("Total") <> 0 Then
                                            dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terdapat Selisih Potong Stock dengan Total!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(.Rows(i).Item("kode_bahan") & " tidak ditemukan!")
                                        Exit Sub
                                    End If
                                End Using

                                'If CekTotal <> Format(CekBahan, "N5") Then
                                '    CloseTrans()
                                '    CloseConn()
                                '    MessageBox.Show("Terdapat Selisih Potong Stock dengan Total!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '    Exit Sub
                                'End If


                                SQL = "select good_stock from bahan_import "
                                SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Bahan = '" & .Rows(i).Item("kode_bahan") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then

                                        If (dr("good_stock") - .Rows(i).Item("tot_sat_bsr")) < 0 Then
                                            dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Stock " & .Rows(i).Item("nama_bahan") & " tidak cukup!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            DataGridView1.Rows.Clear()
                                            ListView1.Items.Clear()
                                            ListView2.Items.Clear()
                                            ListView3.Items.Clear()
                                            Exit Sub
                                        End If
                                        dr.Close()
                                        SQL = "update bahan_import set Good_stock = Good_stock - " & .Rows(i).Item("tot_sat_bsr")
                                        SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Bahan = '" & .Rows(i).Item("kode_bahan") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                                        ExecuteTrans(SQL)

                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(.Rows(i).Item("kode_bahan") & " tidak ditemukan!")
                                        Exit Sub
                                    End If
                                End Using

                                Dim stock_bahan As Double = 0
                                SQL = "select good_stock from bahan_import "
                                SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Bahan = '" & .Rows(i).Item("kode_bahan") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        stock_bahan = dr("good_stock")
                                        dr.Close()

                                        SQL = "select sum(Jumlah) as jumlah "
                                        SQL = SQL & "from bahan_sn where kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_bahan = '" & .Rows(i).Item("kode_bahan") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                                        Using dr2 = OpenTrans(SQL)
                                            If dr2.Read Then

                                                If stock_bahan <> dr2("jumlah") Then
                                                    dr2.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Terdapat Selisih Bahan_SN dengan Bahan_Import!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            Else
                                                dr2.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show(.Rows(i).Item("kode_bahan") & " tidak ditemukan!")
                                                Exit Sub
                                            End If
                                        End Using
                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(.Rows(i).Item("kode_bahan") & " tidak ditemukan!")
                                        Exit Sub
                                    End If
                                End Using


                            ElseIf .Rows(i).Item("Flag_Potong_Stock") = "T" Then
                                Dim hargaPembelianPO As Double = 0
                                SQL = "select c.Harga, c.Satuan, c.Harga_Barang, c.Satuan_Barang from Rencana_Order a, EMI_Pembelian_PO b, EMI_Pembelian_PO_Detail c "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur "
                                SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur "
                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  and c.Kode_Barang = '" & .Rows(i).Item("kode_bahan") & "' "
                                SQL = SQL & "and a.Status is null and b.status is null "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        hargaPembelianPO = Dr("harga")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang tidak ada dalam PO!")
                                        Exit Sub
                                    End If
                                End Using


                                SQL = "select Harga from bahan_import "
                                SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Bahan = '" & .Rows(i).Item("kode_bahan") & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        HargaTot = HargaTot + hargaPembelianPO * .Rows(i).Item("tot_sat_bsr")
                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("error Pada Perhitungan")
                                        Exit Sub
                                    End If
                                End Using

                            End If


                            'GrandTotal = GrandTotal + HargaTot

                        Next
                    End With
                End Using
                SQL = "insert into detail_Loading_Barang2(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner_Import, Kode_Bahan, Jumlah, Total, Kategori, Flag_Potong_Stock, Mata_Uang) Values "
                SQL = SQL & " ('" & KodePerusahaan & "', '" & faktur & "', '" & ListView2.Items(index).SubItems(0).Text & "',"
                SQL = SQL & "'" & ListView2.Items(index).SubItems(1).Text & "', '" & ListView2.Items(index).SubItems(3).Text & "', '" & HargaTot & "', '" & ListView2.Items(index).SubItems(7).Text & "', '" & ListView2.Items(index).SubItems(8).Text & "', '" & ListView2.Items(index).SubItems(6).Text & "')"
                ExecuteTrans(SQL)

            Next


            For j As Integer = 0 To ListView1.Items.Count - 1
                Dim TotHrgBrg As Double = 0
                Dim Flag_Potong_Harga As String = ""

                Dim hargaPembelianPO As Double = 0
                SQL = "select c.Harga, c.Satuan, c.Harga_Barang, c.Satuan_Barang from Rencana_Order a, EMI_Pembelian_PO b, EMI_Pembelian_PO_Detail c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur "
                SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  and c.Kode_Barang = '" & ListView1.Items(j).SubItems(1).Text & "' "
                SQL = SQL & "and a.Status is null and b.status is null "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        hargaPembelianPO = Dr("harga")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang tidak ada dalam PO!")
                        Exit Sub
                    End If
                End Using

                SQL = "Select Flag_Potong_Stock, Harga from Bahan_import where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Bahan = '" & ListView1.Items(j).SubItems(1).Text & "' and Kode_Stock_Owner_Import = '" & TextBox2.Text & "'"
                Using Dr = OpenTrans(SQL)

                    If Dr.Read Then
                        Flag_Potong_Harga = Dr("Flag_Potong_Stock")
                        If Dr("Flag_Potong_Stock") = "T" Then

                            TotHrgBrg = Val(ListView1.Items(j).SubItems(4).Text) * hargaPembelianPO
                        ElseIf Dr("Flag_Potong_Stock") = "Y" Then
                            Dr.Close()
                            SQL = "Select B.KOde_Barang, B.Kode_Bahan, sum(a.Harga*b.Jumlah) as tot_harga from Bahan_SN a, Det_Loading_Barang B where "
                            SQL = SQL & "A.Serial_number = B.Serial_Number and A.Kode_Perusahaan = B.Kode_Perusahaan "
                            SQL = SQL & "and B.No_Faktur = '" & faktur & "' and B.Kode_Barang = '" & ListView1.Items(j).SubItems(0).Text & "' and B.Kode_Stock_Owner = '" & ListView1.Items(j).SubItems(5).Text & "' "
                            SQL = SQL & "and B.Kode_Bahan = '" & ListView1.Items(j).SubItems(1).Text & "' and B.Kode_Perusahaan ='" & KodePerusahaan & "' group by B.KOde_Barang, B.Kode_Bahan"
                            Using Dr2 = OpenTrans(SQL)

                                If Dr2.Read Then
                                    TotHrgBrg = Dr2("tot_harga")
                                Else
                                    Dr2.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Tidak Ada")
                                    Exit Sub
                                End If

                            End Using
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data Tidak Ada")
                            Exit Sub
                        End If

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak Ada")
                        Exit Sub
                    End If

                End Using

                SQL = "insert into Detail_Loading_Barang3 (kode_perusahaan, No_faktur, Kode_Barang, Kode_Stock_Owner, Kode_Bahan, Qty_Bahan, Jumlah, Total, Harga, Flag_Potong_Stock, Kode_Stock_owner_import) Values ("
                SQL = SQL & "'" & KodePerusahaan & "', '" & faktur & "', "
                SQL = SQL & "'" & ListView1.Items(j).SubItems(0).Text & "', '" & ListView1.Items(j).SubItems(5).Text & "', '" & ListView1.Items(j).SubItems(1).Text & "', "
                SQL = SQL & "'" & ListView1.Items(j).SubItems(2).Text & "', '" & ListView1.Items(j).SubItems(3).Text & "', "
                SQL = SQL & "'" & ListView1.Items(j).SubItems(4).Text & "', '" & TotHrgBrg & "', '" & Flag_Potong_Harga & "', '" & TextBox2.Text & "') "
                ExecuteTrans(SQL)
            Next



            SQL = "update Submit_Po set Flag_Loading_Barang = 'Y' "
            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & Txtfaktur.Text & "'"
            ExecuteTrans(SQL)

            SQL = "update rencana_order set Flag_Loading_Barang = 'Y' "
            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & TxtId_Rencana.Text & "'"
            ExecuteTrans(SQL)

            '==================================
            '=     UPDATE FLAG SELESAI PO     =
            '==================================
            SQL = "select ID_Rencana, No_PO from Rencana_Order where Kode_Perusahaan = '" & KodePerusahaan & "' and ID_Rencana = '" & TxtId_Rencana.Text.Trim & "' and Status is null"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        '==================
                        '=     UPDATE     =
                        '==================
                        SQL = "update EMI_Pembelian_PO set Flag_Selesai_PO = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & .Rows(0).Item("No_PO") & "'"
                        ExecuteTrans(SQL)

                    End If
                End With
            End Using




            Cmd.Transaction.Commit()

            CloseConn()
            MessageBox.Show("Data Tersimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()

    End Sub

    Public Sub TxtId_Rencana_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtId_Rencana.Leave
        TextBoxRV.Text = ""
        If TxtId_Rencana.Text.Trim.Length = 0 Then Exit Sub
        Try

            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction
            DataGridView1.Rows.Clear()


            Dim Mata_Uang_Declare As String = ""
            Dim ind As Integer = 0
            SQL = "Select Mata_Uang_Rek,  Mata_Uang_Declare From suppliers where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & TextBox1.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    For ind = 0 To ComboBox1.Items.Count - 1
                        If ComboBox1.Items(ind) = dr("Mata_Uang_Rek") Then
                            Exit For
                        End If
                    Next
                    Mata_Uang_Declare = dr("Mata_Uang_Declare")
                Else
                    CloseConn()
                    MessageBox.Show("Mata Uang Declare/Rekening Tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using
            ComboBox1.SelectedIndex = ind

            If ComboBox2.Items.Count = 0 Then
                CloseConn()
                MessageBox.Show("Rekening dalam mata uang itu tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            SQL = "select cast(rv as bigint) as rv from rencana_order where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana ='" & TxtId_Rencana.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBoxRV.Text = Dr("rv")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using

            Dim lokasi_po As String = ""
            SQL = "select a.Kode_Stock_Owner, a.kode_barang, b.Nama, Jumlah, a.Harga_Declare, (a.panjang*a.lebar*a.tinggi) as vl,a.isi_satuan_besar, a.Berat_bersih, "
            SQL = SQL & "a.berat_kotor, a.panjang, a.lebar, a.tinggi, a.no_urut, a.mata_uang, isnull((select Sum(Qty) from Kontainer_Masuk Z where Z.Kode_Perusahaan = a.Kode_Perusahaan and "
            SQL = SQL & "Z.No_Faktur = a.No_Faktur and Z.Kode_Barang = A.Kode_Barang and Z.Kode_Stock_Owner = A.Kode_Stock_Owner),null) as jml "
            SQL = SQL & "from detail_submit_PO a, barang b where a.kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txtfaktur.Text & "' and  a.kode_perusahaan = b.kode_perusahaan and "
            SQL = SQL & "a.kode_barang = b.kode_Barang and a.kode_stock_owner = b.kode_stock_owner  "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1
                        '''PERUBAHAN PADA EMI
                        '''CmbLokasi.Text = .Rows(index).Item("Kode_Stock_Owner")
                        '------------------------
                        If .Rows(index).Item("mata_uang") <> Mata_Uang_Declare Then
                            CloseConn()
                            MessageBox.Show("Mata Uang Declare Supplier Berbeda dengan barang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DataGridView1.Rows.Clear()
                            ListView1.Items.Clear()
                            ListView2.Items.Clear()
                            ListView3.Items.Clear()
                            Exit Sub
                        End If

                        If .Rows(0).Item("mata_uang") <> .Rows(index).Item("mata_uang") Then
                            CloseConn()
                            MessageBox.Show("Ada Mata Uang yang Berbeda pada Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DataGridView1.Rows.Clear()
                            ListView1.Items.Clear()
                            ListView2.Items.Clear()
                            ListView3.Items.Clear()
                            Exit Sub
                        End If

                        DataGridView1.Rows.Add(1)
                        DataGridView1.Rows.Item(index).Cells(0).Value = .Rows(index).Item("Kode_Stock_Owner")
                        DataGridView1.Rows.Item(index).Cells(1).Value = .Rows(index).Item("kode_barang")
                        DataGridView1.Rows.Item(index).Cells(2).Value = .Rows(index).Item("Nama")
                        DataGridView1.Rows.Item(index).Cells(3).Value = .Rows(index).Item("Jumlah")
                        DataGridView1.Rows.Item(index).Cells(6).Value = Format(.Rows(index).Item("Harga_Declare"), setN)
                        DataGridView1.Rows.Item(index).Cells(10).Value = .Rows(index).Item("isi_satuan_besar")
                        DataGridView1.Rows.Item(index).Cells(11).Value = Format(.Rows(index).Item("Berat_bersih"), "N0")
                        DataGridView1.Rows.Item(index).Cells(12).Value = Format(.Rows(index).Item("berat_kotor"), "N0")
                        DataGridView1.Rows.Item(index).Cells(15).Value = .Rows(index).Item("panjang")
                        DataGridView1.Rows.Item(index).Cells(16).Value = .Rows(index).Item("lebar")
                        DataGridView1.Rows.Item(index).Cells(17).Value = .Rows(index).Item("tinggi")
                        DataGridView1.Rows.Item(index).Cells(18).Value = .Rows(index).Item("No_Urut")
                        DataGridView1.Rows.Item(index).Cells(19).Value = .Rows(index).Item("Mata_Uang")



                        If General_Class.CekNULL(.Rows(index).Item("jml")) <> "" Then

                            DataGridView1.Rows.Item(index).Cells(4).Value = .Rows(index).Item("jml")
                            DataGridView1.Rows.Item(index).Cells(5).Value = .Rows(index).Item("Jumlah") - .Rows(index).Item("jml")
                            DataGridView1.Rows.Item(index).Cells(7).Value = Format(.Rows(index).Item("Harga_Declare") * .Rows(index).Item("jml"), setN)
                            DataGridView1.Rows.Item(index).Cells(8).Value = Format(.Rows(index).Item("vl") * Math.Ceiling((.Rows(index).Item("jml") / .Rows(index).Item("isi_satuan_besar"))), "N0")
                            DataGridView1.Rows.Item(index).Cells(9).Value = Math.Ceiling((.Rows(index).Item("jml") / .Rows(index).Item("isi_satuan_besar")))
                            DataGridView1.Rows.Item(index).Cells(13).Value = Format(.Rows(index).Item("Berat_bersih") * .Rows(index).Item("jml"), "N0")
                            DataGridView1.Rows.Item(index).Cells(14).Value = Format(.Rows(index).Item("berat_kotor") * .Rows(index).Item("jml"), "N0")
                        Else

                            DataGridView1.Rows.Item(index).Cells(4).Value = 0
                            DataGridView1.Rows.Item(index).Cells(5).Value = .Rows(index).Item("Jumlah")
                            DataGridView1.Rows.Item(index).Cells(7).Value = 0
                            DataGridView1.Rows.Item(index).Cells(8).Value = 0
                            DataGridView1.Rows.Item(index).Cells(9).Value = 0
                            DataGridView1.Rows.Item(index).Cells(13).Value = 0
                            DataGridView1.Rows.Item(index).Cells(14).Value = 0
                        End If

                        lokasi_po = .Rows(index).Item("Kode_Stock_Owner")

                        SQL = "select kode_barang from komposisi_barang_jadi a "
                        SQL = SQL & "where kode_barang = '" & .Rows(index).Item("kode_barang") & "' and  a.kode_perusahaan = '" & KodePerusahaan & "' "
                        Using dr = OpenTrans(SQL)
                            If Not dr.Read Then
                                CloseConn()
                                MessageBox.Show(.Rows(index).Item("Nama") & " tidak ada dalam komposisi barang jadi")
                                DataGridView1.Rows.Clear()
                                ListView1.Items.Clear()
                                ListView2.Items.Clear()
                                ListView3.Items.Clear()
                                Exit Sub
                            End If
                        End Using

                    Next
                End With
            End Using

            CheckBox1.Checked = True
            ComboBox4.Items.Clear()
            SQL = "select lokasi from stock_owner_group where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and kode_stock_owner = '" & lokasi_po & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("lokasi"))
                Loop
            End Using

            Cmd.Transaction.Commit()
            CloseConn()

            HitungGrand()
            ComboBox1.Focus()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtId_Rencana_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtId_Rencana.TextChanged


    End Sub

    Private Sub TxtContainer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtContainer.TextChanged

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then Exit Sub

        ComboBox2.Items.Clear()
        SQL = "Select No_Rekening From rekening_suppliers where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & TextBox1.Text & "' and Mata_Uang ='" & ComboBox1.Text & "' order by No_Rekening"
        Using dr = OpenTrans(SQL)
            Do While dr.Read
                ComboBox2.Items.Add(dr("No_Rekening"))
            Loop
        End Using

        'Try
        '    OpenConn()
        '    ComboBox2.Items.Clear()
        '    SQL = "Select No_Rekening From rekening_suppliers where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & TextBox1.Text & "' and Mata_Uang ='" & ComboBox1.Text & "' order by No_Rekening"
        '    Using dr = OpenTrans(SQL)
        '        Do While dr.Read
        '            ComboBox2.Items.Add(dr("No_Rekening"))
        '        Loop
        '    End Using
        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    'Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If e.KeyChar = Chr(13) Then BtnSimpan.Focus()
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    'End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Submit_PO_Import_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox3.Focus()
    End Sub

    Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox1.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("No Kontainer Harus Di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus()
            Exit Sub
        ElseIf TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("No Seal Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus()
            Exit Sub
        End If


        For index As Integer = 0 To ListView3.Items.Count - 1

            If ListView3.Items(index).SubItems(0).Text.Trim = TextBox3.Text.Trim And ListView3.Items(index).SubItems(1).Text = TextBox4.Text And ListView3.Items(index).SubItems(2).Text = Format(DateTimePicker1.Value, "dd-MMM-yyyy") Then
                MessageBox.Show("Data Sudah Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

        Next

        Dim Lvw As ListViewItem
        Lvw = ListView3.Items.Add(TextBox3.Text.Trim)
        Lvw.SubItems.Add(TextBox4.Text.Trim)
        Lvw.SubItems.Add(Format(DateTimePicker1.Value, "dd-MMM-yyyy"))

        TextBox3.Text = ""
        TextBox4.Text = ""
        DateTimePicker1.Value = FMenu.ToolStripStatusLabel3.Text
    End Sub

    Private Sub ListView3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.Click

        If ListView3.Items.Count = 0 Or ListView3.SelectedItems.Count = 0 Then
            ListView3.BackColor = Color.White
            ListView3.ForeColor = Color.Black
            Exit Sub
        End If

        Dim index As Integer = 0
        index = ListView3.FocusedItem.Index

        For index1 As Integer = 0 To ListView3.Items.Count - 1
            If index1 = index Then
                ListView3.Items(index1).BackColor = Color.MediumBlue
                ListView3.Items(index1).ForeColor = Color.White
            Else
                ListView3.Items(index1).BackColor = Color.White
                ListView3.Items(index1).ForeColor = Color.Black
            End If
        Next

    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If ListView3.Items.Count = 0 Or ListView3.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu Kontainer yang mau di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Input_Data_kontainer_Loading_Barang.faktur.Text = Txtfaktur.Text
        Input_Data_kontainer_Loading_Barang.Kontainer.Text = ListView3.FocusedItem.Text
        Input_Data_kontainer_Loading_Barang.Seal.Text = ListView3.FocusedItem.SubItems(1).Text
        Input_Data_kontainer_Loading_Barang.Tanggal.Value = ListView3.FocusedItem.SubItems(2).Text
        Input_Data_kontainer_Loading_Barang.Lokasi.Text = DataGridView1.CurrentRow.Cells(0).Value
        Input_Data_kontainer_Loading_Barang.kode.Text = DataGridView1.CurrentRow.Cells(1).Value
        Input_Data_kontainer_Loading_Barang.Barang.Text = DataGridView1.CurrentRow.Cells(2).Value
        Input_Data_kontainer_Loading_Barang.index = DataGridView1.CurrentRow.Index
        Input_Data_kontainer_Loading_Barang.TxtSupplier.Text = TextBox1.Text
        Input_Data_kontainer_Loading_Barang.Lokasi_utama.Text = CmbLokasi.Text


        Input_Data_kontainer_Loading_Barang.ShowDialog()
    End Sub


    Private Sub HapusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HapusToolStripMenuItem.Click
        If ListView3.Items.Count = 0 Or ListView3.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Dahulu Data yang Mau di hapus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim fak_loading As String = ""
            SQL = "select No_Faktur From EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Plat = '" & ListView3.FocusedItem.Text & "' and No_Fak_Submit_PO = '" & Txtfaktur.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    fak_loading = dr("no_faktur")
                End If
            End Using

            SQL = "select Kode_Perusahaan from kontainer_masuk where No_Faktur ='" & Txtfaktur.Text & "' and No_Container = '" & ListView3.FocusedItem.Text & "' and No_Seal = '" & ListView3.FocusedItem.SubItems(1).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    MessageBox.Show("Terdapat Data Tersimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If Hapus1 = vbYes Then

                        Dr.Close()
                        SQL = "Delete From Kontainer_Masuk where No_Faktur ='" & Txtfaktur.Text & "' and No_Container = '" & ListView3.FocusedItem.Text & "' and No_Seal = '" & ListView3.FocusedItem.SubItems(1).Text & "'"
                        ExecuteTrans(SQL)

                        SQL = "delete from EMI_Pembelian_Loading where kode_Perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & fak_loading & "' "
                        ExecuteTrans(SQL)

                        SQL = "delete from EMI_Pembelian_Loading_detail where kode_Perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & fak_loading & "' "
                        ExecuteTrans(SQL)

                        Cmd.Transaction.Commit()
                        CloseConn()
                        ListView3.FocusedItem.Remove()
                        TxtId_Rencana_Leave(HapusToolStripMenuItem, e)
                        Cek_Bahan()
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Penghapusan dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    ListView3.FocusedItem.Remove()
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub LihatDataKontainerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LihatDataKontainerToolStripMenuItem.Click
        Display_Kontainer_Masuk.TextBoxFaktur.Text = Txtfaktur.Text
        Display_Kontainer_Masuk.ShowDialog()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            ComboBox4.Enabled = True
        Else
            ComboBox4.Enabled = False
        End If
    End Sub

    Private Sub CheckBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox4.Focus()
    End Sub

    Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan.Focus()
    End Sub

    Private Sub ListView3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView3.SelectedIndexChanged

    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
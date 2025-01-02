Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class Transaksi_Permintaan_BB_Produksi
    Dim Jenis = "Transaksi_Permintaan_BB_Produksi"
    Dim arrcari, arrIdPenanggungJawab As New ArrayList
    Public kd_cus As String
    Dim lokasi_gudang As String = ""

    Dim LvJenis As String
    Dim LvKode As String
    Dim LvNama As String
    Dim LvQty As String
    Dim LvSatuan As String
    Dim LvReq_Date As String
    Dim LvCatat As String
    Dim LvLokasi As String

    Dim CellJenis As Integer = 0
    Dim CellKode As Integer = 1
    Dim CellNama As Integer = 2
    Dim CellQty As Integer = 3
    Dim CellSatuan As Integer = 4
    Dim CellReq_Date As Integer = 5
    Dim CellCatat As Integer = 6
    Dim CellLokasi As Integer = 7

    Private Sub get_isi_listview(ByVal index As Integer)
        LvJenis = DataGridView1.Rows(index).Cells(CellJenis).Value
        LvKode = DataGridView1.Rows(index).Cells(CellKode).Value
        LvNama = DataGridView1.Rows(index).Cells(CellNama).Value
        LvQty = DataGridView1.Rows(index).Cells(CellQty).Value
        LvSatuan = DataGridView1.Rows(index).Cells(CellSatuan).Value
        LvReq_Date = DataGridView1.Rows(index).Cells(CellReq_Date).Value
        LvCatat = DataGridView1.Rows(index).Cells(CellCatat).Value
        LvLokasi = DataGridView1.Rows(index).Cells(CellLokasi).Value
    End Sub
    Private Sub Transaksi_Permintaan_BB_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Judul
            Label2.Text = Base_Language.Lang_Global_No_Transaksi
            Label3.Text = Base_Language.Lang_Global_Tanggal
            Label5.Text = Base_Language.Lang_Global_Keterangan
            Label6.Text = Base_Language.Lang_Global_Customer
            Label4.Text = Base_Language.Lang_Global_NoInquiry
            Label9.Text = Base_Language.Lang_Global_Penangung_Jawab
            'Btn_Cari.Text = Base_Language.Lang_Global_Cari
            'Button1.Text = Base_Language.Lang_Global_Cari
            'Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            'Btn_Hapus.Text = Base_Language.Lang_Global_Update
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            'Button16.Text = Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Tambah

            DataGridView1.Columns(0).HeaderText = Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Jns_Brg
            DataGridView1.Columns(1).HeaderText = Base_Language.Lang_Global_KodeBarang
            DataGridView1.Columns(2).HeaderText = Base_Language.Lang_Global_NamaBarang
            DataGridView1.Columns(3).HeaderText = Base_Language.Lang_Global_jumlah_kuantiti
            DataGridView1.Columns(4).HeaderText = Base_Language.Lang_Global_Satuan
            DataGridView1.Columns(5).HeaderText = Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Req_Date
            DataGridView1.Columns(6).HeaderText = Base_Language.Lang_Global_Catatan

            ComboBox1.Items.Clear() : arrIdPenanggungJawab.Clear()
            SQL = "select Nama,Id_Karyawan from Emi_Karyawan a, Emi_Jabatan_Internal b where a.id_jabatan=b.id_jabatan "
            SQL = SQL & "and b.flag_Tampil_Formulator='Y' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Nama")) : arrIdPenanggungJawab.Add(dr("Id_Karyawan"))
                Loop
            End Using

            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using
            ComboBox2.Text = Lokasi

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
        get_data()
    End Sub
    Private Sub get_data()
        Try
            OpenConn()

            If TextBox1.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error_Inqu, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox1.Focus() : Exit Sub
            ElseIf TextBox3.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error_Cus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox3.Focus() : Exit Sub
            End If

            DataGridView1.Rows.Clear()
            ExecuteTrans("delete from Emi_Permintaan_BB_Sementara where userID ='" & UserID & "' and jenis='Bahan Baku'")

            SQL = "select Kode_Stock_Owner_Gudang from Binding_Lokasi_Gudang where Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & " and Gudang_Default = 'Y' and Kode_Stock_Owner = '" & ComboBox2.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    lokasi_gudang = dr("kode_stock_owner_gudang")
                Else
                    CloseConn()
                    MessageBox.Show(Base_Language.lang_global_Error_LokasiTidakAda, Judul, MessageBoxButtons.OK)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur, b.Kode_Barang, c.Nama, b.Serapan_Bulan, b.Satuan,b.kode_stock_owner, f.Hasil,f.Satuan_Hasil "
            SQL = SQL & "from EMI_Inquiry a, Emi_Inquiry_Detail b, Barang c, EMI_Transaksi_Formulator_Binding d, "
            SQL = SQL & "EMI_Transaksi_Formulator_Binding_detail e, Emi_Transaksi_Formulator f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.status is null "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang_inq and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan=d.Kode_Perusahaan and a.No_Faktur=d.No_inquiry and d.status is null "
            SQL = SQL & "and d.Kode_Perusahaan=e.Kode_Perusahaan and d.no_faktur=e.no_faktur "
            SQL = SQL & "and b.Kode_Perusahaan=e.Kode_Perusahaan and b.Kode_Barang=e.Kode_Produk "
            SQL = SQL & "and e.Kode_Perusahaan=f.Kode_Perusahaan and e.Binding_Formula=f.No_Faktur and f.Status is null "
            SQL = SQL & " And a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox1.Text & "' and a.flag_validasi = 'Y' and a.flag_binding_formula = 'Y' "
            SQL = SQL & "order by no_faktur "
            Using Ds = BindingTrans(SQL)
                'With Ds.Tables("MyTable")
                For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    SQL = "Select a.No_Faktur,b.Kode_Barang, "
                    SQL = SQL & "b.Serapan_Bulan,b.Satuan as satuan_serapan, "
                    SQL = SQL & "e.Hasil as NIlai_Formula,e.Satuan_Hasil as satuan_formula, "
                    SQL = SQL & "f.Kode_Barang as Kode_Bahan, Nilai_Barang as Nilai_Bahan, Satuan_barang as satuan_bahan, "
                    SQL = SQL & "G.satuan_simulasi, "
                    SQL = SQL & "I.berat as berat_barang, I.satuan as satuan_barang, H.Panjang, H.lebar "
                    SQL = SQL & "From Emi_Inquiry a, Emi_Inquiry_Detail b, EMI_Transaksi_Formulator_Binding c, "
                    SQL = SQL & "EMI_Transaksi_Formulator_Binding_Detail d, Emi_Transaksi_Formulator e, "
                    SQL = SQL & "EMI_Transaksi_Formulator_Detail_Bahan f, Init G, Barang H, EMI_Netto_Kemasan_Utama i "
                    SQL = SQL & "Where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And a.Status Is null "
                    SQL = SQL & " And a.Flag_Binding_Formula ='Y' and a.Kode_Perusahaan='" & KodePerusahaan & "' and "
                    SQL = SQL & "a.no_faktur = '" & TextBox1.Text & "' "
                    SQL = SQL & " and b.kode_barang = '" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "' "
                    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan And a.No_Faktur = c.No_Inquiry And c.Status Is null And "
                    SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan And c.No_Faktur = d.No_Faktur And "
                    SQL = SQL & "b.Kode_Perusahaan = d.Kode_Perusahaan And b.Kode_Barang = d.Kode_Produk And "
                    SQL = SQL & "d.Kode_Perusahaan = e.Kode_Perusahaan And d.Binding_Formula = e.No_Faktur And e.Status Is null And "
                    SQL = SQL & "e.Kode_Perusahaan = f.Kode_Perusahaan And e.No_Faktur = f.No_Faktur And "
                    SQL = SQL & "a.Kode_Perusahaan = G.Kode_Perusahaan And b.Kode_Perusahaan = H.Kode_Perusahaan And "
                    SQL = SQL & "b.Kode_Barang = H.Kode_Barang_Inq And b.Kode_Stock_Owner = H.Kode_Stock_Owner And "
                    SQL = SQL & "h.Id_Netto_Kemasan_Utama = i.Id_Netto_Kemasan_Utama "
                    Using Ds2 = BindingTrans(SQL)
                        With Ds2.Tables("MyTable")
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
                                        nilai_formula = dr("hasil")
                                    End If
                                End Using

                                SQL = "select dbo.ubah_satuan("
                                SQL = SQL & "'" & KodePerusahaan & "', 'masa', '" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "', '" & Ds.Tables("MyTable").Rows(i).Item("Satuan_Hasil") & "',"
                                SQL = SQL & "'" & .Rows(indexBahan).Item("satuan_simulasi") & "' , '" & Ds.Tables("MyTable").Rows(i).Item("Hasil") & "' "
                                SQL = SQL & ") as hasil"
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        nilai_serapan = dr("hasil")
                                    End If
                                End Using

                                Dim Satuan_Bahan As String = ""
                                nilaiPembanding = nilai_serapan / nilai_formula
                                ' nilaiPembanding = DgvData.Rows(currentRow).Cells(cellDataJmlh).Value / nilai_formula

                                nilaiBahan = .Rows(indexBahan).Item("Nilai_Bahan")
                                Satuan_Bahan = .Rows(indexBahan).Item("satuan_Bahan")
                                nilaiAkhirBahan = Format(nilaiBahan, "N5") * Format(nilaiPembanding, "N5")

                                Dim Perhitungan As String = ""
                                Dim lokasi_gudang_bahan As String = ""

                                SQL = "select top(1) c.lokasi_gudang from EMI_Kategori_Gudang a, barang b, EMI_Kategori_Gudang_PerLokasi c  "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Id_Kategori_Gudang "
                                SQL = SQL & "and  a.Id_Kategori_Gudang = c.ID_Kategori_Gudang and a.Kode_Perusahaan = c.Kode_Perusahaan "
                                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and c.Lokasi_Gudang = '" & lokasi_gudang & "' "
                                SQL = SQL & "and b.kode_barang = '" & .Rows(indexBahan).Item("Kode_Bahan") & "' "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        lokasi_gudang_bahan = dr("lokasi_gudang")
                                    End If
                                End Using

                                SQL = "insert into Emi_Permintaan_BB_Sementara(Kode_Perusahaan, Kode_Barang, Total,"
                                SQL = SQL & "Satuan, Kode_Bahan, Total_Bahan, Satuan_Bahan, UserID, nilai_formula, "
                                SQL = SQL & "Satuan_Formula,Nilai_Bahan, Nilai_Pembanding, No_Inquiry, Jenis,kode_stock_owner,kode_stock_owner_bahan) values( "
                                SQL = SQL & "'" & KodePerusahaan & "', '" & .Rows(indexBahan).Item("Kode_Barang") & "', '" & Ds.Tables("MyTable").Rows(i).Item("Hasil") & "', "
                                SQL = SQL & "'" & .Rows(indexBahan).Item("Satuan_Simulasi") & "', '" & .Rows(indexBahan).Item("Kode_Bahan") & "', '" & nilaiAkhirBahan & "', "
                                SQL = SQL & "'" & Satuan_Bahan & "','" & UserID & "', '" & nilai_formula & "','" & .Rows(indexBahan).Item("Satuan_Simulasi") & "', "
                                SQL = SQL & " '" & nilaiBahan & "', '" & nilaiPembanding & "', '" & .Rows(indexBahan).Item("no_faktur") & "', 'Bahan Baku', '" & lokasi_gudang & "','" & lokasi_gudang_bahan & "')"
                                ExecuteTrans(SQL)
                            Next
                        End With
                    End Using
                Next
                'End With
            End Using

            DataGridView1.Rows.Clear()
            SQL = "select a.Kode_Perusahaan,b.kode_stock_owner,a.kode_stock_owner_bahan, Kode_Bahan, b.nama as nama_bahan, sum(a.Total_Bahan) as total_bahan, "
            SQL = SQL & "a.Satuan_Bahan, b.Good_Stock, b.Satuan, b.Last_HPP,a.Jenis, "
            SQL = SQL & "isnull((select MIN(nilai_Barang) from emi_master_penawaran x, emi_master_penawaran_detail y where "
            SQL = SQL & "x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.no_faktur and x.selesai is null and "
            SQL = SQL & "y.Kode_Perusahaan=a.Kode_Perusahaan and y.Kode_Barang=a.Kode_Bahan),null) as nilai_terendah, "
            SQL = SQL & "isnull((select Max(nilai_Barang) from emi_master_penawaran x, emi_master_penawaran_detail y where "
            SQL = SQL & "x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.no_faktur and x.selesai is null and "
            SQL = SQL & "y.Kode_Perusahaan=a.Kode_Perusahaan and y.Kode_Barang=a.Kode_Bahan),null) as nilai_tertinggi, "
            SQL = SQL & "isnull((select top(1) x.no_penawaran from emi_master_penawaran x, emi_master_penawaran_detail y "
            SQL = SQL & "where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.no_faktur and x.selesai is null "
            SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.Kode_Barang=a.Kode_Bahan order by y.nilai_barang asc ),null) as kode_supplier, "
            SQL = SQL & "isnull((select top(1) s.Nama from emi_master_penawaran x, emi_master_penawaran_detail y,Suppliers s "
            SQL = SQL & "where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.no_faktur and x.selesai is null  "
            SQL = SQL & "and x.Kode_Perusahaan = s.Kode_Perusahaan and x.Kode_Supplier = s.Kode_Supplier "
            SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.Kode_Barang=a.Kode_Bahan order by y.nilai_barang asc ),null) as nama_supplier,  "
            SQL = SQL & "isnull((select min(nilai_barang) from emi_master_penawaran x, emi_master_penawaran_detail y "
            SQL = SQL & "where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.no_faktur and x.selesai is null and y.Kode_Perusahaan=a.Kode_Perusahaan "
            SQL = SQL & "and y.Kode_Barang=a.Kode_Bahan),null) as harga  "
            SQL = SQL & "from Emi_Permintaan_BB_Sementara  a, Barang b where a.Kode_Perusahaan=b.Kode_Perusahaan "
            ' SQL = SQL & "and a.Kode_Bahan=b.Kode_Barang and a.kode_stock_owner = b.kode_stock_owner and a.jenis='Bahan Baku' and a.userid = '" & userid & "'"
            SQL = SQL & "and a.Kode_Bahan=b.Kode_Barang and b.Kode_Stock_Owner='" & lokasi_gudang & "' and a.jenis='Bahan Baku' and a.userid = '" & UserID & "'"
            SQL = SQL & "group by a.Kode_Perusahaan,b.kode_stock_owner,a.kode_stock_owner_bahan, Kode_Bahan,b.nama,Satuan_Bahan, b.Good_Stock, b.Satuan,b.Last_HPP,a.jenis "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For indexTampil As Integer = 0 To .Rows.Count - 1
                        DataGridView1.Rows.Add(1)
                        DataGridView1.Rows(indexTampil).Cells(CellJenis).Value = .Rows(indexTampil).Item("jenis")
                        DataGridView1.Rows(indexTampil).Cells(CellKode).Value = .Rows(indexTampil).Item("kode_bahan")
                        DataGridView1.Rows(indexTampil).Cells(CellNama).Value = .Rows(indexTampil).Item("nama_bahan")
                        DataGridView1.Rows(indexTampil).Cells(CellQty).Value = .Rows(indexTampil).Item("total_bahan")
                        DataGridView1.Rows(indexTampil).Cells(CellSatuan).Value = .Rows(indexTampil).Item("satuan")
                        DataGridView1.Rows(indexTampil).Cells(CellReq_Date).Value = "-"
                        DataGridView1.Rows(indexTampil).Cells(CellCatat).Value = "-"
                        DataGridView1.Rows(indexTampil).Cells(CellLokasi).Value = .Rows(indexTampil).Item("kode_stock_owner_bahan")
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Kosong()
        'DataGridView1.Rows.Clear()

        TextBox2.Text = ""
        ComboBox1.SelectedIndex = -1
        TextBox4.Text = ""

        get_jam()

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

    Private Sub get_no_faktur()

        TxtBarangMasuk_NoFaktur.Text = fPBB & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Permintaan_Bahan_Baku", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fPBB) + 4 & ")", fPBB & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error1, Judul, MessageBoxButtons.OK)
            Exit Sub
        ElseIf TxtBarangMasuk_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtBarangMasuk_NoFaktur.Focus()
            Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error2, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus()
            Exit Sub
        ElseIf ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error3, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        ElseIf TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error_Cus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus()
            Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error_Inqu, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            Exit Sub
        ElseIf ComboBox1.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error4, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        End If

        For z As Integer = 0 To DataGridView1.RowCount - 1
            get_isi_listview(z)
            If LvReq_Date = "-" Then
                MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error5, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            ElseIf LvCatat = "" Then
                MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error2, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()
            SQL = "select Flag_Validasi,Flag_Binding_Formula,Flag_Simulasi_HPP,Status,"
            SQL = SQL & "Flag_Penentu_Harga_Jual,Flag_Harga_Jual_Fix,Flag_Permintaan_BB "
            SQL = SQL & "from Emi_Inquiry where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & TextBox1.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("Status")) = "Y" Then
                        CloseTrans()
                        dr.Close()
                        MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error6, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Validasi")) = "" Then
                        CloseTrans()
                        dr.Close()
                        MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error7, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Binding_Formula")) = "" Then
                        CloseTrans()
                        dr.Close()
                        MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error8, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Simulasi_HPP")) = "" Then
                        CloseTrans()
                        dr.Close()
                        MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error9, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Penentu_Harga_Jual")) = "" Then
                        CloseTrans()
                        dr.Close()
                        MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error10, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Harga_Jual_Fix")) = "" Then
                        CloseTrans()
                        dr.Close()
                        MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error11, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Permintaan_BB")) = "Y" Then
                        CloseTrans()
                        dr.Close()
                        MessageBox.Show(Base_Language.Lang_Transaksi_Permintaan_BB_Produksi_Error12, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            SQL = "INSERT INTO Emi_Permintaan_Bahan_Baku(Kode_Perusahaan,No_Faktur,Tanggal,"
            SQL = SQL & "Keterangan,Lokasi,Kode_Customer,No_Inquiry,Penanggung_Jawab) VALUES("
            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "',"
            SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
            SQL = SQL & "'" & TextBox2.Text & "','" & ComboBox2.Text & "','" & kd_cus & "',"
            SQL = SQL & "'" & TextBox1.Text & "','" & ComboBox1.Text & "')"
            ExecuteTrans(SQL)

            For z As Integer = 0 To DataGridView1.RowCount - 1
                get_isi_listview(z)
                SQL = "INSERT INTO Emi_Permintaan_Bahan_Baku_Detail(Kode_Perusahaan,No_Faktur,"
                SQL = SQL & "Jenis,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Request_Date"
                SQL = SQL & ",Catatan) VALUES('" & KodePerusahaan & "',"
                SQL = SQL & "'" & TxtBarangMasuk_NoFaktur.Text & "','" & LvJenis & "',"
                SQL = SQL & "'" & LvLokasi & "','" & LvKode & "','" & LvQty & "','" & LvSatuan & "',"
                SQL = SQL & "'" & LvReq_Date & "','" & LvCatat & "')"
                ExecuteTrans(SQL)
            Next

            SQL = "Update Emi_Inquiry   `set Flag_Permintaan_BB = 'Y' where kode_perusahaan "
            SQL = SQL & "= '" & KodePerusahaan & "' and No_Faktur = '" & TextBox1.Text & "'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        SD_Pilih_Inquiry.Get_Data()
        Me.Close()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        SD_Input_Req_Date.Baris = DataGridView1.CurrentRow.Index
        SD_Input_Req_Date.ShowDialog()
    End Sub
End Class
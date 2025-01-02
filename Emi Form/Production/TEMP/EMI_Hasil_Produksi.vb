Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar


Public Class EMI_Hasil_Produksi
    Dim arrcari, arrId_Karyawan As New ArrayList
    Dim Jenis = "Transaksi_Hasil_Produksi"
    Public no_ro As String
    Dim dtp As DateTimePicker = New DateTimePicker()
    Dim rect As Rectangle
    Dim dtp1 As DateTimePicker = New DateTimePicker()
    Dim rect1 As Rectangle
    Public Tanda_SN As String = "#"

    Dim LvKd_Cust As String
    Dim LvNm_Cust As String
    Dim LvLokasi As String
    Dim LvKd_Brg As String
    Dim LvNm_Brg As String
    Dim LvNo_Batch As String
    Dim LvTgl_Pro As String
    Dim LvTgl_Exp As String
    Dim LvAkumulasi As String
    Dim LvGagal_Pro As String
    Dim LvHasil_Pro As String

    Dim CellKd_Cust As Integer = 0
    Dim CellNm_Cust As Integer = 1
    Dim CellLokasi As Integer = 2
    Dim CellKd_Brg As Integer = 3
    Dim CellNm_Brg As Integer = 4
    Dim CellNo_Batch As Integer = 5
    Dim CellTgl_Pro As Integer = 6
    Dim CellTgl_Exp As Integer = 7
    Dim CellAkumulasi As Integer = 8
    Dim CellGagal_Pro As Integer = 9
    Dim CellHasil_Pro As Integer = 10

    Private Sub get_isi_listview(index)
        LvKd_Cust = DataGridView1.Rows(index).Cells(CellKd_Cust).Value
        LvNm_Cust = DataGridView1.Rows(index).Cells(CellNm_Cust).Value
        LvLokasi = DataGridView1.Rows(index).Cells(CellLokasi).Value
        LvKd_Brg = DataGridView1.Rows(index).Cells(CellKd_Brg).Value
        LvNm_Brg = DataGridView1.Rows(index).Cells(CellNm_Brg).Value
        LvNo_Batch = DataGridView1.Rows(index).Cells(CellNo_Batch).Value
        LvTgl_Pro = DataGridView1.Rows(index).Cells(CellTgl_Pro).Value
        LvTgl_Exp = DataGridView1.Rows(index).Cells(CellTgl_Exp).Value
        LvAkumulasi = DataGridView1.Rows(index).Cells(CellAkumulasi).Value
        LvGagal_Pro = DataGridView1.Rows(index).Cells(CellGagal_Pro).Value
        LvHasil_Pro = DataGridView1.Rows(index).Cells(CellHasil_Pro).Value
    End Sub
    Private Sub Transaksi_Hasil_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Hasil_Produksi_Judul
            'Label6.Text = Base_Language.Lang_Global_No_Transaksi
            Label7.Text = Base_Language.Lang_Hasil_Produksi_Tgl_Transaksi
            Label8.Text = Base_Language.Lang_Hasil_Produksi_Operator
            Label9.Text = Base_Language.Lang_Hasil_Produksi_Mesin
            Label18.Text = Base_Language.Lang_Global_No_Produksi
            Btn_Refresh.Text = Base_Language.Lang_Global_Simpan
            Button2.Text = Base_Language.Lang_Hasil_Produksi_Tarik_Data

            DataGridView1.Columns(0).HeaderText = Base_Language.Lang_Global_KodeCustomer
            DataGridView1.Columns(1).HeaderText = Base_Language.Lang_Global_NamaCustomer
            DataGridView1.Columns(2).HeaderText = Base_Language.Lang_Global_Lokasi
            DataGridView1.Columns(3).HeaderText = Base_Language.Lang_Global_KodeBarang
            DataGridView1.Columns(4).HeaderText = Base_Language.Lang_Global_NamaBarang
            DataGridView1.Columns(5).HeaderText = Base_Language.Lang_Hasil_Produksi_No_Batch
            DataGridView1.Columns(6).HeaderText = Base_Language.Lang_Global_Tanggal_Produksi
            DataGridView1.Columns(7).HeaderText = Base_Language.Lang_Global_Tanggal_Expired
            DataGridView1.Columns(8).HeaderText = Base_Language.Lang_Hasil_Produksi_Akumulasi
            DataGridView1.Columns(9).HeaderText = Base_Language.Lang_Hasil_Produksi_Gagal_Pro
            DataGridView1.Columns(10).HeaderText = Base_Language.Lang_Hasil_Produksi_Hasil_Pro
            DataGridView1.Controls.Add(dtp)
            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = "yyyy-MM-dd"
            dtp.Visible = False
            AddHandler dtp.TextChanged, AddressOf dtp_TextChange

            DataGridView1.Controls.Add(dtp1)
            dtp1.Format = DateTimePickerFormat.Custom
            dtp1.CustomFormat = "yyyy-MM-dd"
            dtp1.Visible = False
            AddHandler dtp1.TextChanged, AddressOf dtp1_TextChange

            ComboBox2.Items.Clear() : arrId_Karyawan.Clear()
            SQL = "select a.Id_Karyawan,a.Nama from Emi_Karyawan a,Emi_Jabatan_Internal b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Id_Jabatan = b.Id_Jabatan and b.Flag_Tampil_Produksi = 'Y' "
            SQL = SQL & "order by Nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("Nama"))
                    arrId_Karyawan.Add(dr("Id_Karyawan"))
                Loop
            End Using

            get_no_faktur()

            SQL = "select No_faktur,Tanggal from Emi_Produksi where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Rencana_Produksi = '" & no_ro & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox4.Text = dr("No_faktur")
                    DateTimePicker1.Value = Format(dr("Tanggal"), "dd MMMM yyyy")
                End If
            End Using

            DataGridView1.Rows.Clear()
            SQL = "select a.Kode_Customer,d.Nama as Cust,a.Kode_Stock_Owner,a.Kode_Barang,c.Nama,a.Jumlah,b.No_Batch "
            SQL = SQL & "from EMI_Rencana_Produksi_Detail a,Emi_Produksi b,Barang c,Customers d where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = b.No_Rencana_Produksi and "
            SQL = SQL & "a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang and "
            SQL = SQL & "a.Kode_Customer = d.Kode_Customer and a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.No_Faktur = '" & no_ro & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows.Item(i).Cells(0).Value = .Rows(i).Item("Kode_Customer")
                            DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("Cust")
                            DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Stock_Owner")
                            DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Nama")
                            DataGridView1.Rows.Item(i).Cells(5).Value = .Rows(i).Item("No_Batch")
                            DataGridView1.Rows.Item(i).Cells(6).Value = ""
                            DataGridView1.Rows.Item(i).Cells(7).Value = ""
                            DataGridView1.Rows.Item(i).Cells(8).Value = Format(.Rows(i).Item("Jumlah"), "N0")
                            DataGridView1.Rows.Item(i).Cells(9).Value = ""
                            DataGridView1.Rows.Item(i).Cells(10).Value = ""
                        Next
                    End If
                End With
            End Using

            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur()
        TxtFormulator_NoFaktur.Text = fHProduksi & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Hasil_Produksi", "No_faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_faktur, 1, " & Len(fHProduksi) + 4 & ")", fHProduksi & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Transaksi_Hasil_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub dtp_TextChange(ByVal sender As Object, ByVal e As EventArgs)
        DataGridView1.CurrentCell.Value = dtp.Text.ToString
        dtp.Visible = False
    End Sub
    Private Sub dtp1_TextChange(ByVal sender As Object, ByVal e As EventArgs)
        DataGridView1.CurrentCell.Value = dtp1.Text.ToString
        dtp1.Visible = False
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Select Case DataGridView1.Columns(e.ColumnIndex).Name
            Case "Column6"
                rect = DataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
                dtp.Size = New Size(rect.Width, rect.Height)
                dtp.Location = New Point(rect.X, rect.Y)
                dtp.Visible = True

            Case "Column8"
                rect1 = DataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
                dtp1.Size = New Size(rect1.Width, rect1.Height)
                dtp1.Location = New Point(rect1.X, rect1.Y)
                dtp1.Visible = True
            Case Else
                dtp1.Visible = False
                dtp.Visible = False
        End Select
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFormulator_NoFaktur.Focus() : Exit Sub
        ElseIf ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Hasil_Produksi_Error_Operator, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Hasil_Produksi_Error_No_Pro, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus() : Exit Sub
        End If

        For i As Integer = 0 To DataGridView1.RowCount - 1
            get_isi_listview(i)
            If LvGagal_Pro = "" Then
                MessageBox.Show(Base_Language.Lang_Hasil_Produksi_Error_Gagal, Judul, MessageBoxButtons.OK)
                Exit Sub
            ElseIf LvTgl_Pro = "" Then
                MessageBox.Show(Base_Language.Lang_Hasil_Produksi_Error_Tgl_Pro, Judul, MessageBoxButtons.OK)
                Exit Sub
            ElseIf LvTgl_Exp = "" Then
                MessageBox.Show(Base_Language.Lang_Hasil_Produksi_Error_Tgl_Exp, Judul, MessageBoxButtons.OK)
                Exit Sub
            ElseIf CDate(LvTgl_Pro) > CDate(LvTgl_Exp) Then
                MessageBox.Show(Base_Language.Lang_Hasil_Produksi_Error_Tgl_Pro2, Judul, MessageBoxButtons.OK)
                Exit Sub
            ElseIf CDate(LvTgl_Pro) > CDate(LvTgl_Exp) Then
                MessageBox.Show(Base_Language.Lang_Hasil_Produksi_Error_Gagal2, Judul, MessageBoxButtons.OK)
                Exit Sub
            End If
        Next

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "INSERT INTO Emi_Hasil_Produksi(Kode_Perusahaan,No_faktur,No_Produksi,Tanggal,Jam,Operator, UserID) "
            SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & TextBox4.Text & "',"
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "',"
            SQL = SQL & "'" & arrId_Karyawan.Item(ComboBox2.SelectedIndex) & "','" & UserID & "')"
            ExecuteTrans(SQL)

            For i As Integer = 0 To DataGridView1.RowCount - 1
                get_isi_listview(i)

                Dim NHPP As Double = 0
                SQL = "Select isnull(sum(dbo.get_hpp(c.Serial_Number) * c.jumlah),0) As Total_biaya from "
                SQL = SQL & "Emi_Permintaan_Bahan_Baku a, emi_permintaan_bahan_baku_detail2 b, Emi_Permintaan_Bahan_Baku_Detail_Stock c where "
                SQL = SQL & "a.Kode_perusahaan = b.Kode_Perusahaan And a.No_faktur = b.No_faktur And a.Status Is null "
                SQL = SQL & "And b.Kode_Perusahaan=c.Kode_Perusahaan And b.No_Urut=c.Urut_Detail2 "
                SQL = SQL & "And a.no_rencanaProduksi='" & no_ro & "' and b.Kode_Barang='" & LvKd_Brg & "' and b.kode_stock_owner = '" & LvLokasi & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If dr("Total_biaya") = "0" Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Nilai HPP 0" & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            NHPP = Math.Round(dr("Total_biaya") / LvAkumulasi, 0)
                        End If
                    End If
                End Using

                SQL = "INSERT INTO Emi_Hasil_Produksi_Detail(Kode_Perusahaan,No_faktur,"
                SQL = SQL & "Kode_Customer,Kode_Stock_Owner,Kode_Barang,No_Batch,Tgl_Produksi,"
                SQL = SQL & "Tgl_Expired,Akumulasi,Gagal_Produksi,Hasil_Produksi, Harga, satuan) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                SQL = SQL & "'" & LvKd_Cust & "','" & LvLokasi & "','" & LvKd_Brg & "',"
                SQL = SQL & "'" & LvNo_Batch & "','" & CDate(LvTgl_Pro) & "',"
                SQL = SQL & "'" & CDate(LvTgl_Exp) & "','" & HilangkanTanda(LvAkumulasi) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvGagal_Pro) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvHasil_Pro) & "', '" & NHPP & "', '')"
                ExecuteTrans(SQL)



                SQL = "select kode_perusahaan from barang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & LvLokasi & "' and "
                SQL = SQL & "kode_barang = '" & LvKd_Brg & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        SQL = "Update barang set good_stock = good_stock + " & HilangkanTanda(LvHasil_Pro) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & LvLokasi & "' and "
                        SQL = SQL & "kode_barang = '" & LvKd_Brg & "' "
                        ExecuteTrans(SQL)
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_GLOBAL_Tidak_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                Dim kd_barang As String = LvKd_Brg
                Dim lks_barang As String = LvLokasi
                Dim satuan_barang As String = "Pcs" '.Rows(Index).Item("Satuan_Barang")
                Dim Nilai_Barang As String = HilangkanTanda(LvAkumulasi)
                Dim IDSusunan_Barang As String = ""
                Dim Tgl_Produksi As String = Format(CDate(LvTgl_Pro), "yyyy-MM-dd")
                Dim Tgl_Expired As String = Format(CDate(LvTgl_Exp), "yyyy-MM-dd")

                'cek dulu jenis susunan
                SQL = "select top(1) a.Kode_Barang, a.Susunan, a.Id_WMS_Pallet, a.Pjumlah,a.Ljumlah,Tjumlah, Satuan_Jumlah, "
                SQL = SQL & "(case when Susunan='Tier' then (pjumlah+ljumlah)  else (pjumlah*ljumlah) end) as Jumlah_Per_tumpukan, "
                SQL = SQL & "Urut, Tinggi_Per_Tumpukan, Total, P,L,T from barang_detail_susunan a, EMI_WMS_Pallet b where "
                SQL = SQL & "a.Id_WMS_Pallet=b.Id_WMS_Pallet and kode_barang = '" & kd_barang & "' "
                SQL = SQL & "and flag_default='Y' "
                Using ds2 = BindingTrans(SQL)
                    With ds2.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            For index2 = 0 To ds2.Tables("MyTable").Rows.Count - 1
                                IDSusunan_Barang = ds2.Tables("MyTable").Rows(index2).Item("Urut")
                                Dim Tinggi_Pallet As Double = ds2.Tables("MyTable").Rows(index2).Item("T")
                                Dim Panjang_Pallet As Double = ds2.Tables("MyTable").Rows(index2).Item("P")
                                Dim satuan_Pallet As String = ds2.Tables("MyTable").Rows(index2).Item("Satuan_Jumlah")

                                Dim Tinggi_Per_tumpukan As Double = ds2.Tables("MyTable").Rows(index2).Item("Tinggi_Per_Tumpukan")
                                Dim jumlah_Per_tumpukan As Double = ds2.Tables("MyTable").Rows(index2).Item("Jumlah_Per_tumpukan")

                                Dim Jumlah_satuan_Besar As Double = 0

                                'ubah ke satuan pallet
                                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kd_barang & "',"
                                SQL = SQL & "'" & satuan_barang & "','" & satuan_Pallet & "',"
                                SQL = SQL & "" & Nilai_Barang & ") as Hasil "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        Jumlah_satuan_Besar = Math.Round(dr("hasil"), 0)
                                    End If
                                End Using

                                'Cek Rak Kosong
                                SQL = "Select * From View_Warehouse_Position_Detail Where Kode_Barang Is null and Kode_stock_Owner ='" & lks_barang & "' "
                                SQL = SQL & "Order By kode_stock_Owner, Kode_WMS_Area, Kode_WMS_Row, Kode_WMS_Bay, Kode_WMS_Level, Kode_WMS_Position "
                                Using ds3 = BindingTrans(SQL)
                                    For index3 = 0 To ds3.Tables("MyTable").Rows.Count - 1
                                        Dim Panjang_level As Double = ds3.Tables("MyTable").Rows(index3).Item("Panjang_level")
                                        Dim Tinggi_level As Double = ds3.Tables("MyTable").Rows(index3).Item("tinggi_level")

                                        Dim id_warehouse As String = ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Warehouse_Position")

                                        Dim Tinggi_Tumpukan As Double = Math.Floor((Tinggi_level - Tinggi_Pallet) / Tinggi_Per_tumpukan)

                                        Dim Jumlah_PerPallet As Double = jumlah_Per_tumpukan * Tinggi_Tumpukan

                                        Dim Jumlah_Masuk_Satuan_Besar As Double = 0
                                        Dim Jumlah_SatuanKecil As Double = 0

                                        'Get Jumlah Masuk Ke Pallet Dalam Satuan Besar dan Kecil
                                        If Jumlah_satuan_Besar > Jumlah_PerPallet Then
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kd_barang & "',"
                                            SQL = SQL & "'" & satuan_Pallet & "','" & satuan_barang & "',"
                                            SQL = SQL & "" & Jumlah_PerPallet & ") as Hasil "
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    Jumlah_SatuanKecil = dr("hasil")
                                                End If
                                            End Using

                                            Jumlah_Masuk_Satuan_Besar = Jumlah_PerPallet

                                        Else
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kd_barang & "',"
                                            SQL = SQL & "'" & satuan_Pallet & "','" & satuan_barang & "',"
                                            SQL = SQL & "" & Jumlah_satuan_Besar & ") as Hasil "
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    Jumlah_SatuanKecil = dr("hasil")
                                                End If
                                            End Using

                                            Jumlah_Masuk_Satuan_Besar = Jumlah_satuan_Besar

                                        End If

                                        Dim isi As Double = Jumlah_SatuanKecil

                                        'Cek sisa Level
                                        Dim Panjang_level_terisi As Double = 0
                                        SQL = "Select ISNULL(SUM(Panjang_Pallet), 0) As panjang from "
                                        SQL = SQL & "View_Warehouse_Position_Detail where Kode_Barang Is Not null And "
                                        SQL = SQL & "Kode_Stock_Owner ='" & ds3.Tables("MyTable").Rows(index3).Item("kode_stock_Owner") & "' and "
                                        SQL = SQL & "Id_WMS_Area='" & ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Area") & "' and "
                                        SQL = SQL & "Id_WMS_Row='" & ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Row") & "' and "
                                        SQL = SQL & "Id_WMS_Bay='" & ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Bay") & "' and "
                                        SQL = SQL & "Id_WMS_Level='" & ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Level") & "' "
                                        Using dr = OpenTrans(SQL)
                                            If dr.Read Then
                                                Panjang_level_terisi = dr("panjang")
                                            End If
                                        End Using

                                        Dim total_Ukuran = (Panjang_level - Panjang_level_terisi) - Panjang_Pallet

                                        'jika muat
                                        If total_Ukuran > 0 Then
                                            If isi <> 0 Then
                                                Dim Rand As New Random

                                                Dim str As String = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                                Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                                Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & NHPP & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                                SQL = "select kode_barang from barang_sn where "
                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_stock_owner = '" & lks_barang & "' and "
                                                SQL = SQL & "kode_barang = '" & kd_barang & "' and serial_number = '" & SN & "'"
                                                Using Dr = OpenTrans(SQL)
                                                    If Dr.Read Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show(Base_Language.Lang_Validasi_Barang_Masuk_Error7 & LvKd_Brg & "!")
                                                        Exit Sub
                                                    Else
                                                        Dr.Close()
                                                        SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                                                        SQL = SQL & "serial_number, jumlah, Tgl_Produksi, Tgl_Expired,Id_Warehouse,id_Susunan) values('" & KodePerusahaan & "', "
                                                        SQL = SQL & "'" & lks_barang & "', '" & kd_barang & "', "
                                                        SQL = SQL & "'" & SN & "', " & isi & ", '" & Tgl_Produksi & "', '" & Tgl_Expired & "', '" & id_warehouse & "', '" & IDSusunan_Barang & "')"
                                                        ExecuteTrans(SQL)

                                                        SQL = "insert into emi_hasil_produksi_detail_Rak(kode_perusahaan,No_Faktur, kode_stock_owner, kode_barang, "
                                                        SQL = SQL & "serial_number, jumlah, Satuan, Id_Warehouse, id_Susunan, Jumlah_Barang, Satuan_Barang) values('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', "
                                                        SQL = SQL & "'" & lks_barang & "', '" & kd_barang & "', '" & SN & "', "
                                                        SQL = SQL & "" & isi & ", '" & satuan_barang & "', '" & id_warehouse & "', '" & IDSusunan_Barang & "','" & Jumlah_Masuk_Satuan_Besar & "','" & satuan_Pallet & "')"
                                                        ExecuteTrans(SQL)

                                                        Jumlah_satuan_Besar -= Jumlah_Masuk_Satuan_Besar

                                                    End If
                                                End Using
                                            End If
                                        End If

                                        If Jumlah_Masuk_Satuan_Besar = 0 Then
                                            Exit For
                                        End If

                                    Next
                                End Using

                                If Jumlah_satuan_Besar <> 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Tempat Sudah Penuh . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                            Next

                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data tidak ditemukan . . !")
                            Exit Sub
                        End If
                    End With
                End Using


            Next

            SQL = "update EMI_Rencana_Produksi set Selesai = 'Y' where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & no_ro & "'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_Produksi_Display.Button1_Click(Btn_Refresh, e)
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        'Select Case DataGridView1.Columns(e.ColumnIndex).Name
        '    Case "Column10"
        get_isi_listview(DataGridView1.CurrentRow.Index)
        If IsNumeric(LvGagal_Pro) = False Or Val(LvGagal_Pro) < 0 Then
            DataGridView1.CurrentRow.Cells(CellGagal_Pro).Value = 0
            DataGridView1.CurrentRow.Cells(CellHasil_Pro).Value = LvAkumulasi
        Else
            DataGridView1.CurrentRow.Cells(CellHasil_Pro).Value = Val(HilangkanTanda(LvAkumulasi)) - Val(HilangkanTanda(LvGagal_Pro))
        End If
        'End Select
    End Sub

End Class
Public Class N_EMI_SD_Transaksi_Retur_Packaging

    Public Lokasi_Gudang_Transfer, Faktur_Retur As String
    Private sistemClose As Boolean = False

    Dim JumlahMaksimalRequest As Double = 0

    Dim Txt_NoFaktur_ReqMaterial As String = ""

    Private Sub N_EMI_SD_Transaksi_Retur_Packaging_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()
    End Sub


    Private Sub Kosong()

        Chk_Ajukan_Request.Checked = False
        sistemClose = False

        Cmb_Lokasi_Request.Items.Clear()
        SQL = "select kode_stock_owner from Stock_Owner_Gudang where kode_perusahaan = '" & KodePerusahaan & "' "
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                Cmb_Lokasi_Request.Items.Add(Dr("kode_stock_owner"))
            Loop
        End Using

        Dim Lokasi_Request As String = ""
        SQL = "select kode_stock_owner from Emi_Split_Production_Order where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Txt_NoSplit.Text & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Lokasi_Request = Dr("kode_stock_owner")
            End If
        End Using

        Lokasi_Gudang_Transfer = Lokasi_Request

        SQL = "select distinct b.Kode_Stock_Owner_Tujuan "
        SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b "
        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
        SQL = SQL & "and a.No_Faktur = b.No_Faktur "
        SQL = SQL & "and a.Status is null "
        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and a.No_Faktur_Order = '" & Txt_NoSplit.Text.Trim & "' "
        SQL = SQL & "and b.Kode_Stock_Owner = '" & Lokasi_Gudang_Transfer & "' "
        SQL = SQL & "and b.Kode_Barang = '" & Txt_Kd_Barang.Text.Trim & "' "
        SQL = SQL & "and a.Flag_Otomatis = 'Y' "
        Using Ds = BindingTrans(SQL)
            With Ds.Tables("MyTable")
                If .Rows.Count <> 0 Then
                    If .Rows.Count > 1 Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalaham, Gudang Request Lebih Dari 1 Gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Cmb_Lokasi_Request.SelectedItem = .Rows(0).Item("Kode_Stock_Owner_Tujuan")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Gudang Request Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End With
        End Using


        JumlahMaksimalRequest = Math.Ceiling(Val(HilangkanTanda(Txt_Jumlah_Input.Text)))

    End Sub






    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Val(HilangkanTanda(Txt_Jumlah_Request.Text)) < 0 Then
            CloseTrans()
            CloseConn()
            MessageBox.Show("Jumlah Input Tidak Boleh Minus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Not String.IsNullOrWhiteSpace(Txt_Jumlah_Request.Text) And Val(HilangkanTanda(Txt_Jumlah_Request.Text)) > 0 Then

            Txt_NoFaktur_ReqMaterial = fRequestMaterial & Format(tgl_skg, "MMyy") & "-" &
                 General_Class.Get_Last_Number2("Emi_Material_Requisition", "No_Faktur", 5,
                 "Kode_perusahaan", KodePerusahaan,
                 "And", "substring(No_Faktur, 1, " & Len(fRequestMaterial) + 4 & ")", fRequestMaterial & Format(tgl_skg, "MMyy"))


            Dim Keterangan_RM As String = "Request Retur Material from Production Order " & Txt_NoSplit.Text

            '================================
            '=     GET JUMLAH KEBUTUHAN     =
            '================================
            Dim JumlahKebutuhan As Double = 0
            Dim kode_barang_fg As String = ""
            SQL = "select b.Jumlah, a.kode_barang "
            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoSplit.Text & "' "
            SQL = SQL & "and b.Kode_Stock_Owner = '" & Lokasi_Gudang_Transfer & "' "
            SQL = SQL & "and b.Kode_Barang = '" & Txt_Kd_Barang.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    JumlahKebutuhan = Dr("Jumlah")
                    kode_barang_fg = Dr("kode_barang")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah Kebutuhan Packaging Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=============================
            '=     GET ID GROUP JENIS    =
            '=============================
            Dim Id_Group_Jenis As String = ""
            SQL = "Select Id_Group_Jenis from barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Lokasi_Gudang_Transfer & "' "
            SQL = SQL & "and Kode_Barang='" & Txt_Kd_Barang.Text & "' "
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
            SQL = "insert into Emi_Material_Requisition (Kode_Perusahaan, No_Faktur, No_Faktur_Order, Kode_Stock_Owner, Kode_Barang, Id_Group_Jenis, Tanggal, Jam, Flag_Process, UserId, Status, Keterangan, Lokasi, Flag_Retur_Packaging, No_Faktur_Retur_Packaging) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & Txt_NoSplit.Text & "', "
            SQL = SQL & "'" & Lokasi_Gudang_Transfer & "', '" & kode_barang_fg & "', '" & Id_Group_Jenis & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'Y', '" & UserID & "', NULL, '" & Keterangan_RM & "', '" & Ket_Lokasi_HO_Proyek & "', "
            SQL = SQL & "'Y', '" & Faktur_Retur & "') "
            ExecuteTrans(SQL)



            '==============================
            '=     INSERT TABEL DET     =
            '==============================

            SQL = "insert into Emi_Material_Requisition_det (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Stock_Owner_Tujuan, Kode_Barang, Kebutuhan, Jumlah, Satuan, Jumlah_Barang, Satuan_Barang, Jenis_Material) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & Lokasi_Gudang_Transfer & "', '" & Cmb_Lokasi_Request.Text & "', "
            SQL = SQL & "'" & Txt_Kd_Barang.Text & "', '" & HilangkanTanda(JumlahKebutuhan) & "', "
            SQL = SQL & "'" & HilangkanTanda(Txt_Jumlah_Request.Text) & "', "
            SQL = SQL & "'" & Cmb_Satuan_Request.Text & "', '" & HilangkanTanda(Txt_Jumlah_Request.Text) & "', '" & Cmb_Satuan_Request.Text & "', 'Packaging')"
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
            SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & Lokasi_Gudang_Transfer & "', '" & Txt_Kd_Barang.Text & "', "
            SQL = SQL & "'" & HilangkanTanda(Txt_Jumlah_Request.Text) & "', "
            SQL = SQL & "'" & Cmb_Satuan_Request.Text & "', '" & HilangkanTanda(Txt_Jumlah_Request.Text) & "', '" & Cmb_Satuan_Request.Text & "', 'Hijau', '" & x_ident_currentPackaging & "')"
            ExecuteTrans(SQL)

            '======================================
            '=     CEK APAKAH BAHAN TERPENUHI     =
            '======================================
            'SQL = "select "
            'SQL = SQL & "(a.jumlah - ISNULL(( "
            'SQL = SQL & "select sum(x.Jumlah) "
            'SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
            'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            'SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
            'SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
            'SQL = SQL & "), 0)) as Sisa "
            'SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a "
            'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur = '" & Txt_NoSplit.Text & "' "
            'SQL = SQL & "and a.Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
            'Using Ds1 = BindingTrans(SQL)
            '    If Ds1.Tables("MyTable").Rows.Count <> 0 Then

            '        Dim cekDataDouble As Integer = 0
            '        For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
            '            cekDataDouble = cekDataDouble + 1

            '            If cekDataDouble > 1 Then
            '                CloseTrans()
            '                CloseConn()
            '                MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                Exit Sub
            '            End If

            '            If Val(Ds1.Tables("MyTable").Rows(j).Item("Sisa")) = 0 Then

            '                SQL = "update Emi_Split_Production_Order_Detail_Packaging set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
            '                SQL = SQL & "and No_Faktur = '" & Txt_NoSplit.Text & "' and Kode_Stock_Owner = '" & Lokasi_Request & "' and Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "'"
            '                ExecuteTrans(SQL)

            '            End If
            '        Next
            '    End If
            'End Using

        End If

        If Val(HilangkanTanda(Txt_Jumlah_Request.Text)) = 0 Then
            If MessageBox.Show("Yakin Tidak ingin Melakukan Request Material?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbNo Then
                Txt_Jumlah_Request.Focus()
                Exit Sub
            End If
        Else
            MessageBox.Show("Request Material Berhasil Di Input", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        N_EMI_Transaksi_Retur_Packaging.AddData = True

        sistemClose = True
        Me.Close()

    End Sub







    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Chk_Ajukan_Request_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Ajukan_Request.CheckedChanged
        If Chk_Ajukan_Request.Checked Then
            Txt_Jumlah_Request.Enabled = True
            Txt_Jumlah_Request.Text = 0
            Txt_Jumlah_Request.Focus()
        Else
            Txt_Jumlah_Request.Enabled = False
            Txt_Jumlah_Request.Text = ""
        End If
    End Sub


    Private Sub Txt_Jumlah_Request_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Jumlah_Request.KeyPress
        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "."c OrElse e.KeyChar = ChrW(Keys.Back)) Then
            e.Handled = True
            Return
        End If

        Dim textBox As TextBox = TryCast(sender, TextBox)

        If e.KeyChar = "."c AndAlso textBox.Text.Contains(".") Then
            e.Handled = True
            Return
        End If

        Dim futureText As String = textBox.Text.Substring(0, textBox.SelectionStart) & e.KeyChar & textBox.Text.Substring(textBox.SelectionStart + textBox.SelectionLength)

        If futureText = "." Then
            e.Handled = True
        End If

        If Val(HilangkanTanda(futureText)) > JumlahMaksimalRequest Then
            MessageBox.Show("Jumlah Request Melebihi Batas Maximum Request", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            e.Handled = True
        End If

    End Sub


    Private Sub Btn_Keluar_Click(sender As Object, e As EventArgs) Handles Btn_Keluar.Click
        If MessageBox.Show("Yakin Tidak ingin Melanjutkan transaksi ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbNo Then Exit Sub

        sistemClose = True
        Me.Close()

    End Sub

    '======================================================================================
    '=     FUNGSI UNTUK MENCEGAH USER MELAKUKAN MAXIMIZE DENGAN DOUBLE KLIK TITLE BAR     =
    '======================================================================================
    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_NCLBUTTONDBLCLK As Integer = &HA3
        If m.Msg = WM_NCLBUTTONDBLCLK Then
            Return
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub N_EMI_SD_Transaksi_Retur_Packaging_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing AndAlso Not sistemClose Then
            e.Cancel = True
            MessageBox.Show("Gunakan tombol Simpan atau Tombol Keluar untuk menutup form.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


End Class
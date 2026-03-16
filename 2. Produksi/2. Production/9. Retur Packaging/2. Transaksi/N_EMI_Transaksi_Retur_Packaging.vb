Imports System.Drawing.Printing
Imports System.Globalization
Imports System.IO

Public Class N_EMI_Transaksi_Retur_Packaging

    Public AddData As Boolean = False
    Dim Lv_NoSplit, Lv_Tanggal, Lv_Routing, Lv_Keterangan, Lv_KdBarang, Lv_NmBarang, Lv_Jumlah, Lv_Satuan, Lv_Flag_GI As String

    Dim item_NoSplit As Integer = 0
    Dim item_Tanggal As Integer = 1
    Dim item_Routing As Integer = 2
    Dim item_Keterangan As Integer = 3
    Dim item_KdBarang As Integer = 4
    Dim item_NmBarnag As Integer = 5
    Dim item_Jumlah As Integer = 6
    Dim item_Satuan As Integer = 7
    Dim item_Flag_GI As Integer = 8

    Private random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Dim arrGudang_Packaging, arrBarang_Req_Packaging, arrBarang_Req_Packaging_Satuan, arrBarang_Scrap_Req_Packaging, arrBarang_Scrap_Satuan_Req_Packaging, arrSatuan_Req_Packaging As New ArrayList
    Dim arrFilter As New ArrayList

    Dim Lokasi_Produksi As String
    Dim Txt_NoFaktur_ReqMaterial, Txt_NoTransaksi As String

    Private Sub N_EMI_Transaksi_Retur_Packaging_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub N_EMI_Transaksi_Retur_Packaging_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        Lv_Data_Split.Columns.Clear()
        Lv_Data_Split.Columns.Add("No Split", 130, HorizontalAlignment.Left)
        Lv_Data_Split.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Data_Split.Columns.Add("Routing", 0, HorizontalAlignment.Right)
        Lv_Data_Split.Columns.Add("Keterangan", 280, HorizontalAlignment.Left)
        Lv_Data_Split.Columns.Add("Barang", 300, HorizontalAlignment.Left)
        Lv_Data_Split.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Data_Split.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Data_Split.Columns.Add("FlagGI", 0, HorizontalAlignment.Center)
        Lv_Data_Split.View = View.Details



        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add(OpsiSeluruh) : arrFilter.Add(OpsiSeluruh)
        Cmb_Filter.Items.Add("No Split") : arrFilter.Add("a.No_Transaksi")
        'Cmb_Filter.Items.Add("Routing") : arrFilter.Add("d.Keterangan")
        Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
        Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("c.Nama")
        Cmb_Filter.Items.Add("Keterangan") : arrFilter.Add("b.Keterangan")



        Try
            OpenConn()

            Cmb_Satuan_Produksi.Items.Clear()
            Cmb_Satuan_Retur.Items.Clear()
            Cmb_Satuan_Tot_Retur.Items.Clear()
            'SQL = "select Satuan from EMI_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' order by Satuan "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Cmb_Satuan_Produksi.Items.Add(Dr("Satuan"))
            '        Cmb_Satuan_Retur.Items.Add(Dr("Satuan"))
            '        Cmb_Satuan_Tot_Retur.Items.Add(Dr("Satuan"))
            '    Loop
            'End Using

            SQL = "select Kode_Stock_Owner From Stock_Owner_Gudang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Produksi = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Lokasi_Produksi = Dr("kode_stock_owner")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=================================
            '=     LOAD GUDANG PACKAGING     =
            '=================================
            ' Note : Load gudang yang hanya memiliki packaging
            arrGudang_Packaging.Clear() : Cmb_Lokasi_Retur.Items.Clear()
            SQL = "select Kode_Stock_Owner, Keterangan "
            SQL = SQL & "from Stock_Owner_Gudang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Stock_Owner in ( "
            SQL = SQL & "select distinct a.Kode_Stock_Owner "
            SQL = SQL & "from barang a, emi_group_jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.Flag_Packaging = 'Y' "
            SQL = SQL & "and a.Good_Stock <> 0) "
            SQL = SQL & "and Flag_Produksi = 'Y'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi_Retur.Items.Add(Dr("Keterangan"))
                    arrGudang_Packaging.Add(Dr("Kode_Stock_Owner"))
                Loop

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
    End Sub

    Private Sub Kosong()

        Txt_NoSplit.Text = ""
        Txt_Kd_Barang.Text = ""
        Txt_Nm_Barang.Text = ""
        Txt_Jumlah_Produksi.Text = ""
        Txt_Jumlah_Tot_Retur.Text = ""
        Txt_Jumlah_Retur.Text = ""
        Txt_Jumlah_Retur_Satuan_Scrap.Text = ""
        TxtBerat.Text = ""
        TxtJumlahPakai.Text = ""
        Txt_Jumlah_Tot_Retur.Text = ""

        Txt_NoFaktur_ReqMaterial = ""
        Txt_NoTransaksi = ""

        Cmb_Filter.SelectedIndex = 0
        Txt_Filter.Text = ""

        Cmb_Satuan_Tot_Retur.Items.Clear()

        Cmb_Satuan_Produksi.SelectedIndex = -1
        Cmb_Lokasi_Retur.SelectedIndex = -1
        Cmb_Barang_Retur.SelectedIndex = -1
        Cmb_Barang_Scrap.SelectedIndex = -1
        Cmb_Satuan_Retur.SelectedIndex = -1
        Cmb_Satuan_Tot_Retur.SelectedIndex = -1

        Txt_NoSplit.Enabled = False
        Txt_Kd_Barang.Enabled = False
        Txt_Nm_Barang.Enabled = False
        Txt_Jumlah_Produksi.Enabled = False
        Txt_Jumlah_Retur.Enabled = False

        Cmb_Satuan_Produksi.Enabled = False
        Cmb_Lokasi_Retur.Enabled = False
        Cmb_Barang_Retur.Enabled = False
        Cmb_Barang_Scrap.Enabled = False
        Cmb_Satuan_Retur.Enabled = False

        Load_Data_Split()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Txt_Jumlah_Retur_TextChanged(sender As Object, e As EventArgs) Handles Txt_Jumlah_Retur.TextChanged
        If Txt_Jumlah_Retur.Text.Trim.Length = 0 Then Exit Sub

        If Cmb_Lokasi_Retur.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Retur Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah_Retur.Text = ""
            Cmb_Lokasi_Retur.DroppedDown = True
            Cmb_Lokasi_Retur.Focus() : Exit Sub
        ElseIf Cmb_Barang_Retur.SelectedIndex = -1 Then
            MessageBox.Show("Barang Retur Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah_Retur.Text = ""
            Cmb_Barang_Retur.DroppedDown = True
            Cmb_Barang_Retur.Focus() : Exit Sub
        ElseIf Cmb_Barang_Scrap.SelectedIndex = -1 Then
            MessageBox.Show("Barang Scrap Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah_Retur.Text = ""
            Cmb_Barang_Scrap.DroppedDown = True
            Cmb_Barang_Scrap.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            ''================================
            ''=     CONVER MENJADI SCRAP     =
            ''================================
            Dim Jumlah_Convert_Scrap As Double = 0
            'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "', "
            'SQL = SQL & "'" & arrBarang_Req_Packaging_Satuan(Cmb_Barang_Retur.SelectedIndex) & "', '" & arrBarang_Scrap_Satuan_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "', '" & HilangkanTanda(Txt_Jumlah_Retur.Text) & "' ) as hasil "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Jumlah_Convert_Scrap = Dr("hasil")
            '    End If
            'End Using

            SQL = "select Berat from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Stock_Owner = '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' and Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'Jumlah_Convert_Scrap = Dr("hasil")
                    Jumlah_Convert_Scrap = Math.Round(Val(HilangkanTanda(Txt_Jumlah_Retur.Text)) * (Val(HilangkanTanda(Dr("Berat"))) / 1000), 4)
                End If
            End Using

            Txt_Jumlah_Retur_Satuan_Scrap.Text = $"{Jumlah_Convert_Scrap} {arrBarang_Scrap_Satuan_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex)}"

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)
        With Lv_Data_Split.Items(index)
            Lv_NoSplit = .SubItems(item_NoSplit).Text
            Lv_Tanggal = .SubItems(item_Tanggal).Text
            Lv_Routing = .SubItems(item_Routing).Text
            Lv_Keterangan = .SubItems(item_Keterangan).Text
            Lv_KdBarang = .SubItems(item_KdBarang).Text
            Lv_NmBarang = .SubItems(item_NmBarnag).Text
            Lv_Jumlah = .SubItems(item_Jumlah).Text
            Lv_Satuan = .SubItems(item_Satuan).Text
            Lv_Flag_GI = .SubItems(item_Flag_GI).Text
        End With
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Kategori yang Ingin Difilter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Filter.DroppedDown = True
            Cmb_Filter.Focus()
            Exit Sub
        ElseIf Cmb_Filter.SelectedIndex <> 0 Then
            If Txt_Filter.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Filter.Focus()
                Exit Sub
            End If
        End If

        Load_Data_Split()

    End Sub

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.SelectedIndex = 0 Then
            Txt_Filter.Enabled = False
        Else
            Txt_Filter.Enabled = True
        End If
        Txt_Filter.Text = ""
    End Sub



    Private Sub Load_Data_Split()
        Try
            OpenConn()

            Lv_Data_Split.Items.Clear()
            SQL = "select a.No_Transaksi, a.Tgl_Produksi, a.Jam_Produksi, d.Keterangan as Routing, a.Kode_Barang, c.Nama as Nama_Barang, a.Jumlah, a.Satuan, b.Keterangan as Keterangan, "
            SQL = SQL & "isnull(( select top 1 'Y' from Emi_Production_Results z, Emi_Production_Results_HPP x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and x.Tanggal is not null "
            SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Production_Order = a.No_Transaksi "
            SQL = SQL & "), 'T') as Flag_GI "
            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, barang c, EMI_Master_Routing d, emi_group_jenis e "
            SQL = SQL & "where a.Kode_Perusahaan = b.kode_perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and b.Id_Routing = d.Id_Routing "
            SQL = SQL & "and c.id_group_jenis=e.id_group_jenis and flag_finished_good='Y'  "
            SQL = SQL & "and a.Status is null and b.Status is null "
            SQL = SQL & "and a.Flag_Produksi = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and (a.Flag_Hasil_Produksi_GI ='Y' or a.Flag_Hasil_Produksi_GI is null) and a.Flag_Hasil_Produksi_GR2 is null "
            If Cmb_Filter.SelectedIndex <> 0 Then
                If Txt_Filter.Text.Trim.Length = 0 Then
                    CloseConn()
                    MessageBox.Show($"ValueFilter tidak boleh kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_Filter.Focus()
                    Exit Sub
                Else
                    SQL = SQL & "and " & arrFilter(Cmb_Filter.SelectedIndex).ToString.Trim & " like '%" & Txt_Filter.Text.Trim & "%' "
                End If
            End If

            'SQL = SQL & "and not exists ( select 1 from N_EMI_Validation_GR_3 z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Transaksi = z.No_Production_Order and z.Status is null) "
            SQL = SQL & "order by a.Tanggal, a.Jam, a.No_Transaksi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = Lv_Data_Split.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Routing"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Flag_GI"))

                    If Dr("Flag_GI") = "T" Then
                        Lv.BackColor = Color.LightYellow
                    Else
                        Lv.BackColor = Color.White
                    End If

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Lv_Data_Split_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data_Split.DoubleClick
        If Lv_Data_Split.Items.Count = 0 Or Lv_Data_Split.FocusedItem Is Nothing Then Exit Sub

        Get_Data_Lv(Lv_Data_Split.FocusedItem.Index)

        Cmb_Barang_Retur.Items.Clear()
        Cmb_Satuan_Tot_Retur.Items.Clear()
        Cmb_Barang_Scrap.Items.Clear()
        Cmb_Satuan_Retur.Items.Clear()

        If Lv_Flag_GI.ToUpper = "T" Then
            MessageBox.Show("No Split Belum Melakuakn Good Issue", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoSplit.Text = ""
            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Txt_Jumlah_Produksi.Text = ""
            Txt_Jumlah_Retur.Text = ""
            Txt_Jumlah_Tot_Retur.Text = ""
            Txt_Jumlah_Retur_Satuan_Scrap.Text = ""
            TxtBerat.Text = ""
            TxtJumlahPakai.Text = ""
            Txt_Jumlah_Tot_Retur.Text = ""

            Cmb_Satuan_Tot_Retur.SelectedIndex = -1
            Cmb_Satuan_Produksi.SelectedIndex = -1
            Cmb_Lokasi_Retur.SelectedIndex = -1
            Cmb_Barang_Retur.SelectedIndex = -1
            Cmb_Barang_Scrap.SelectedIndex = -1
            Cmb_Satuan_Retur.Items.Clear()
            Cmb_Satuan_Retur.SelectedIndex = -1

            Txt_NoSplit.Enabled = False
            Txt_Kd_Barang.Enabled = False
            Txt_Nm_Barang.Enabled = False
            Txt_Jumlah_Produksi.Enabled = False
            Txt_Jumlah_Retur.Enabled = False

            Cmb_Satuan_Produksi.Enabled = False
            Cmb_Lokasi_Retur.Enabled = False
            Cmb_Barang_Retur.Enabled = False
            Cmb_Barang_Scrap.Enabled = False
            Cmb_Satuan_Retur.Enabled = False
            Exit Sub
        End If

        Txt_NoSplit.Text = Lv_NoSplit
        Txt_Kd_Barang.Text = Lv_KdBarang
        Txt_Nm_Barang.Text = Lv_NmBarang
        Txt_Jumlah_Produksi.Text = Lv_Jumlah
        Cmb_Satuan_Produksi.Text = Lv_Satuan
        Txt_Jumlah_Retur_Satuan_Scrap.Text = ""
        TxtBerat.Text = ""
        TxtJumlahPakai.Text = ""
        Txt_Jumlah_Tot_Retur.Text = ""
        Cmb_Satuan_Tot_Retur.SelectedIndex = -1

        Cmb_Lokasi_Retur.Enabled = True

        Cmb_Lokasi_Retur.SelectedIndex = -1
        Cmb_Barang_Retur.SelectedIndex = -1
        Cmb_Barang_Scrap.SelectedIndex = -1
        Cmb_Satuan_Retur.Items.Clear()
        Cmb_Satuan_Retur.SelectedIndex = -1

        Txt_Jumlah_Retur.Text = ""

        Cmb_Lokasi_Retur.DroppedDown = True
        Cmb_Lokasi_Retur.Focus()

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_NoSplit.Text.Trim.Length = 0 Then
            MessageBox.Show("No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_Data_Split.Focus() : Exit Sub
        ElseIf Cmb_Lokasi_Retur.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Retur Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi_Retur.DroppedDown = True
            Cmb_Lokasi_Retur.Focus() : Exit Sub
        ElseIf Cmb_Barang_Retur.SelectedIndex = -1 Then
            MessageBox.Show("Barang Retur Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Barang_Retur.DroppedDown = True
            Cmb_Barang_Retur.Focus() : Exit Sub
        ElseIf Cmb_Barang_Scrap.SelectedIndex = -1 Then
            MessageBox.Show("Barang Scrap Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Barang_Scrap.DroppedDown = True
            Cmb_Barang_Scrap.Focus() : Exit Sub
        End If

        If Txt_Jumlah_Retur.Text.Trim.Length = 0 Or Txt_Jumlah_Retur.Text = "0" Or Val(Txt_Jumlah_Retur.Text) = 0 Then
            MessageBox.Show("Tidak Ada Retur yang Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah_Retur.Focus() : Exit Sub
        End If

        If MessageBox.Show("Yakin ingin Melakukan Retur Barang Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbNo Then Exit Sub

        get_jam()

        Dim kode_unik_print As String

        AddData = False
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            If Val(Txt_Jumlah_Retur.Text) > 0 Then

                Dim fReturPackaging As String = "RTP"
                Txt_NoTransaksi = fReturPackaging & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Production_Results_Detail_Change_Packaging", "No_Transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Transaksi, 1, " & Len(fReturPackaging) + 4 & ")", fReturPackaging & Format(tgl_skg, "MMyy"))


                Dim Jumlah_Ganti As Double = 0
                Dim Jumlah_Pada_diri As Double = 0
                Dim Satuan_Ganti As String = ""

                Jumlah_Ganti = Val(HilangkanTanda(Txt_Jumlah_Retur.Text))
                Satuan_Ganti = Cmb_Satuan_Retur.Text

                N_EMI_SD_Transaksi_Retur_Packaging.Faktur_Retur = Txt_NoTransaksi
                N_EMI_SD_Transaksi_Retur_Packaging.Txt_NoSplit.Text = Txt_NoSplit.Text
                N_EMI_SD_Transaksi_Retur_Packaging.Txt_Kd_Barang.Text = arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex)
                N_EMI_SD_Transaksi_Retur_Packaging.Txt_Nm_Barang.Text = Cmb_Barang_Retur.Text
                N_EMI_SD_Transaksi_Retur_Packaging.Txt_Jumlah_Pakai.Text = TxtJumlahPakai.Text
                N_EMI_SD_Transaksi_Retur_Packaging.Txt_Jumlah_Input.Text = Txt_Jumlah_Retur.Text

                N_EMI_SD_Transaksi_Retur_Packaging.Cmb_Satuan_Request.Items.Clear()
                SQL = "select a.No_Faktur_Order, b.Kode_Barang, c.Nama as Nama_Barang, b.Satuan "
                SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_det b, barang c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "and a.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.Jenis_Material = 'Packaging' "
                SQL = SQL & "and a.No_Faktur_Order = '" & Txt_NoSplit.Text & "' "
                SQL = SQL & "and b.Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
                SQL = SQL & "group by a.No_Faktur_Order, b.Kode_Barang, c.Nama, b.Satuan "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        N_EMI_SD_Transaksi_Retur_Packaging.Cmb_Satuan_Request.Items.Add(Dr("Satuan"))
                        N_EMI_SD_Transaksi_Retur_Packaging.Cmb_Satuan_Request.SelectedItem = Dr("Satuan")
                    Loop
                End Using

                N_EMI_SD_Transaksi_Retur_Packaging.ShowDialog()

                If AddData = False Then
                    CloseTrans()
                    CloseConn()
                    'MessageBox.Show("Proses Ganti Packaging Tidak Bisa Dilakukan, Karena Barang " & .Rows(i).Item("Nama_Barang") & " Masih Ada Sisa pada Split Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim inisial_faktur_dari As String = ""


                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        'akun_persediaan_dari = Dr("persediaan")
                        inisial_faktur_dari = Dr("inisial_faktur")

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim Kode_voucher As String = ""
                Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                Dim pagenumber As Integer = 1



                '=================================
                '=     AUTO REQUEST MATERIAL     =
                '=================================

                SQL = "insert into EMI_Production_Results_Detail_Change_Packaging (Kode_Perusahaan, No_Transaksi, No_Split, Proses, Jumlah, Satuan, Tanggal, Jam, Kode_Voucher) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoTransaksi & "', '" & Txt_NoSplit.Text & "', NULL, '" & Jumlah_Ganti & "', '" & Satuan_Ganti & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & Kode_voucher & "')"
                ExecuteTrans(SQL)

                '=============================================================
                '=     CEK APAKAH ADA SISA STOCK PADA SPLIT DIRI SENDIRI     =
                '=============================================================
                'SQL = ";with cte as ( "
                'SQL = SQL & "select a.No_Faktur_Order, b.Kode_Barang, c.Nama as Nama_Barang, sum(round(b.Jumlah, 2)) AS Jumlah_Request, "
                'SQL = SQL & "isnull(( select dbo.Ubah_Satuan(z.Kode_Perusahaan, 'masa', x.Kode_Barang, x.Satuan_Barang, x.Satuan, sum(w.Jumlah)) as Jumlah "
                'SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_Det y, Tf_Stock_Det2 w "
                'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
                'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
                'SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
                'SQL = SQL & "and z.Status is null "
                'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                'SQL = SQL & "and x.Urut_Material_Requisition_Convert = d.Urut_Oto "
                'SQL = SQL & "and x.Kode_Barang = b.Kode_Barang "
                'SQL = SQL & "group by z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang, x.Satuan "
                'SQL = SQL & "), 0) as Jumlah_Kirim, b.Satuan, "

                'SQL = SQL & "isnull((select sum(x.jumlah_request) from EMI_Production_Results_Detail_Change_Packaging z, EMI_Production_Results_Detail_Change_Packaging_Detail x "
                'SQL = SQL & "where z.kode_perusahaan = x.Kode_Perusahaan "
                'SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
                'SQL = SQL & "and z.Status is null "
                'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                'SQL = SQL & "and z.no_split = a.No_Faktur_Order "
                'SQL = SQL & "), 0) as Jumlah_Retur "

                'SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_det b, barang c, Emi_Material_Requisition_Det_Convert d "
                'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
                'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
                'SQL = SQL & "and b.No_Faktur = d.No_Faktur and b.Urut_Oto = d.No_Urut_Det "
                'SQL = SQL & "and a.Status is null "
                'SQL = SQL & "and b.Jenis_Material = 'Packaging' "
                'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and a.No_Faktur_Order = '" & Txt_NoSplit.Text & "' "
                'SQL = SQL & "and b.Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
                'SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur_Order, b.Kode_Barang, c.Nama, b.Satuan, d.Urut_Oto "
                'SQL = SQL & ") select No_Faktur_Order, Kode_Barang, Nama_Barang, sum(Jumlah_Request) as Jumlah_Request, sum(Jumlah_Kirim) as jumlah_kirim, "
                'SQL = SQL & "case when sum(Jumlah_Kirim) = 0 then 0 else ROUND((sum(Jumlah_Kirim) - (sum(Jumlah_Request) - Jumlah_Retur)), 2) end as Sisa, satuan "
                'SQL = SQL & "from cte "
                'SQL = SQL & "group by No_Faktur_Order, Kode_Barang, Nama_Barang, satuan, Jumlah_Retur "
                'SQL = SQL & "order by Kode_Barang "
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        If Val(HilangkanTanda(Dr("Sisa"))) > 0 Then
                '            If Val(HilangkanTanda(Txt_Jumlah_Retur.Text)) <= Dr("Sisa") Then
                '                MessageBox.Show("Proses Ganti Packaging Tidak Bisa Dilakukan, Karena Barang " & Dr("Nama_Barang") & " Masih Ada Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                Dr.Close()
                '                CloseTrans()
                '                CloseConn()
                '                Exit Sub
                '            Else

                '                Jumlah_Ganti = Val(HilangkanTanda(Txt_Jumlah_Retur.Text)) - Val(HilangkanTanda(Dr("Sisa")))
                '                Jumlah_Pada_diri = Val(HilangkanTanda(Txt_Jumlah_Retur.Text)) - Val(HilangkanTanda(Dr("Sisa")))
                '                Satuan_Ganti = Dr("satuan")

                '            End If

                '            'Else
                '            '    Dr.Close()
                '            '    CloseTrans()
                '            '    CloseConn()
                '            '    MessageBox.Show("Data Request Material belum selesai di transfer sepenuhnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            '    Exit Sub
                '        End If
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Data Request Material tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                ''===================================================================
                ''=     CEK APAKAH ADA SISA STOCK PADA SPLIT YANG SUDAH SELESAI     =
                ''===================================================================
                Dim foundData As Boolean = False
                'SQL = ";with cte as ( "
                'SQL = SQL & "select a.No_Faktur_Order, b.Kode_Barang, c.Nama as Nama_Barang, sum(round(b.Jumlah, 2)) AS Jumlah_Request, "
                'SQL = SQL & "isnull(( select dbo.Ubah_Satuan(z.Kode_Perusahaan, 'masa', x.Kode_Barang, x.Satuan_Barang, x.Satuan, sum(w.Jumlah)) as Jumlah "
                'SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_Det y, Tf_Stock_Det2 w "
                'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
                'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
                'SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
                'SQL = SQL & "and z.Status is null "
                'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                'SQL = SQL & "and x.Urut_Material_Requisition_Convert = d.Urut_Oto "
                'SQL = SQL & "and x.Kode_Barang = b.Kode_Barang "
                'SQL = SQL & "group by z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang, x.Satuan "
                'SQL = SQL & "), 0) as Jumlah_Kirim, b.Satuan, "

                'SQL = SQL & "isnull(( select sum(z.jumlah) from Emi_Material_Requisition_Det_2 z "
                'SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Faktur = a.No_Faktur and z.No_Split = a.No_Faktur_Order and z.Urut_Det_Convert = d.Urut_Oto "
                'SQL = SQL & "), 0) as Jumlah_Dipakai "

                'SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_det b, barang c, Emi_Material_Requisition_Det_Convert d "
                'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
                'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
                'SQL = SQL & "and b.No_Faktur = d.No_Faktur and b.Urut_Oto = d.No_Urut_Det "
                'SQL = SQL & "and a.Status is null "
                'SQL = SQL & "and b.Jenis_Material = 'Packaging' "
                'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and a.No_Faktur_Order in ( "
                'SQL = SQL & "select z.No_Transaksi from Emi_Split_Production_Order z "
                'SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Status is null and z.Flag_Hasil_Produksi_GR = 'Y' ) "
                'SQL = SQL & "and b.Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
                'SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur, a.No_Faktur_Order, b.Kode_Barang, c.Nama, b.Satuan, d.Urut_Oto "
                'SQL = SQL & ") select No_Faktur_Order, Kode_Barang, Nama_Barang, sum(Jumlah_Request) as Jumlah_Request, sum(Jumlah_Kirim) as jumlah_kirim, sum(Jumlah_Dipakai) as Jumlah_Dipakai, "
                'SQL = SQL & "case when sum(Jumlah_Kirim) = 0 then 0 else ROUND((sum(Jumlah_Kirim) - sum(Jumlah_Request)), 2) end as Sisa, satuan "
                'SQL = SQL & "from cte "
                'SQL = SQL & "group by No_Faktur_Order, Kode_Barang, Nama_Barang, satuan "
                'SQL = SQL & "order by Kode_Barang "
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then
                '            For i As Integer = 0 To .Rows.Count - 1
                '                Dim sisaPakai As Double = Val(HilangkanTanda(.Rows(i).Item("jumlah_kirim"))) - Val(HilangkanTanda(.Rows(i).Item("Jumlah_Dipakai")))
                '                If sisaPakai > 0 Then

                '                    If Val(HilangkanTanda(Txt_Jumlah_Retur.Text)) < sisaPakai + Jumlah_Pada_diri Then
                '                        foundData = True
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Proses Ganti Packaging Tidak Bisa Dilakukan, Karena Barang " & .Rows(i).Item("Nama_Barang") & " Masih Ada Sisa pada Split Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    Else
                '                        Jumlah_Ganti = Val(HilangkanTanda(Txt_Jumlah_Retur.Text)) - (sisaPakai + Jumlah_Pada_diri)
                '                        Satuan_Ganti = .Rows(i).Item("satuan")

                '                    End If

                '                End If
                '            Next
                '        End If
                '    End With
                'End Using


                If Not foundData Then




#Region "Auto Request Material"

                    'If Jumlah_Ganti <> 0 Then

                    '    Txt_NoFaktur_ReqMaterial = fRequestMaterial & Format(tgl_skg, "MMyy") & "-" &
                    '         General_Class.Get_Last_Number2("Emi_Material_Requisition", "No_Faktur", 5,
                    '         "Kode_perusahaan", KodePerusahaan,
                    '         "And", "substring(No_Faktur, 1, " & Len(fRequestMaterial) + 4 & ")", fRequestMaterial & Format(tgl_skg, "MMyy"))

                    '    Dim Keterangan_RM As String = "Request Retur Material from Production Order " & Txt_NoSplit.Text

                    '    '=============================
                    '    '=     GET ID GROUP JENIS    =
                    '    '=============================
                    '    Dim Id_Group_Jenis As String = ""
                    '    SQL = "Select Id_Group_Jenis from barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' "
                    '    SQL = SQL & "and Kode_Barang='" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
                    '    Using Dr = OpenTrans(SQL)
                    '        If Dr.Read Then
                    '            Id_Group_Jenis = Dr("Id_Group_Jenis")
                    '        Else
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Jenis Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '            Exit Sub
                    '        End If

                    '    End Using

                    '    Dim Lokasi_Request As String = ""
                    '    SQL = "select kode_stock_owner from Emi_Split_Production_Order where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Txt_NoSplit.Text & "' "
                    '    Using Dr = OpenTrans(SQL)
                    '        If Dr.Read Then
                    '            Lokasi_Request = Dr("kode_stock_owner")
                    '        End If
                    '    End Using

                    '    '=========================
                    '    '=     INSERT PARENT     =
                    '    '=========================
                    '    SQL = "insert into Emi_Material_Requisition (Kode_Perusahaan, No_Faktur, No_Faktur_Order, Kode_Stock_Owner, Kode_Barang, Id_Group_Jenis, Tanggal, Jam, Flag_Process, UserId, Status, Keterangan, Lokasi) values "
                    '    SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & Txt_NoSplit.Text & "', "
                    '    SQL = SQL & "'" & Lokasi_Request & "', '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "', '" & Id_Group_Jenis & "', "
                    '    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'Y', '" & UserID & "', NULL, '" & Keterangan_RM & "', '" & Ket_Lokasi_HO_Proyek & "')"
                    '    ExecuteTrans(SQL)

                    '    '==============================
                    '    '=     INSERT TABEL DET     =
                    '    '==============================

                    '    SQL = "insert into Emi_Material_Requisition_det (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Stock_Owner_Tujuan, Kode_Barang, Kebutuhan, Jumlah, Satuan, Jumlah_Barang, Satuan_Barang, Jenis_Material) values "
                    '    SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & Lokasi_Request & "', '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "', "
                    '    SQL = SQL & "'" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "', '" & HilangkanTanda(Jumlah_Ganti) & "', "
                    '    SQL = SQL & "'" & HilangkanTanda(Jumlah_Ganti) & "', "
                    '    SQL = SQL & "'" & arrSatuan_Req_Packaging(Cmb_Satuan_Retur.SelectedIndex) & "', '" & HilangkanTanda(Jumlah_Ganti) & "', '" & arrSatuan_Req_Packaging(Cmb_Satuan_Retur.SelectedIndex) & "', 'Packaging')"
                    '    ExecuteTrans(SQL)

                    '    Dim x_ident_currentPackaging As Integer = 0
                    '    SQL = "select IDENT_CURRENT('Emi_Material_Requisition_det') as urutan"
                    '    Using Dr = OpenTrans(SQL)
                    '        If Dr.Read Then
                    '            x_ident_currentPackaging = Dr("urutan")
                    '        End If
                    '    End Using

                    '    SQL = "insert into Emi_Material_Requisition_det_convert(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Jumlah_Barang,Satuan_Barang,Warna,No_Urut_Det)"
                    '    SQL = SQL & "values("
                    '    SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial & "', '" & Lokasi_Request & "', '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "', "
                    '    SQL = SQL & "'" & HilangkanTanda(Jumlah_Ganti) & "', "
                    '    SQL = SQL & "'" & arrSatuan_Req_Packaging(Cmb_Satuan_Retur.SelectedIndex) & "', '" & HilangkanTanda(Jumlah_Ganti) & "', '" & arrSatuan_Req_Packaging(Cmb_Satuan_Retur.SelectedIndex) & "', 'Hijau', '" & x_ident_currentPackaging & "')"
                    '    ExecuteTrans(SQL)

                    '    '======================================
                    '    '=     CEK APAKAH BAHAN TERPENUHI     =
                    '    '======================================
                    '    SQL = "select "
                    '    SQL = SQL & "(a.jumlah - ISNULL(( "
                    '    SQL = SQL & "select sum(x.Jumlah) "
                    '    SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
                    '    SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                    '    SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                    '    SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
                    '    SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
                    '    SQL = SQL & "), 0)) as Sisa "
                    '    SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a "
                    '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    '    SQL = SQL & "and a.No_Faktur = '" & Txt_NoSplit.Text & "' "
                    '    SQL = SQL & "and a.Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
                    '    Using Ds1 = BindingTrans(SQL)
                    '        If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                    '            Dim cekDataDouble As Integer = 0
                    '            For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
                    '                cekDataDouble = cekDataDouble + 1

                    '                If cekDataDouble > 1 Then
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                    Exit Sub
                    '                End If

                    '                If Val(Ds1.Tables("MyTable").Rows(j).Item("Sisa")) = 0 Then

                    '                    SQL = "update Emi_Split_Production_Order_Detail_Packaging set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
                    '                    SQL = SQL & "and No_Faktur = '" & Txt_NoSplit.Text & "' and Kode_Stock_Owner = '" & Lokasi_Request & "' and Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "'"
                    '                    ExecuteTrans(SQL)

                    '                End If
                    '            Next
                    '        End If
                    '    End Using

                    '    'Else
                    '    '    CloseTrans()
                    '    '    CloseConn()
                    '    '    MessageBox.Show("Terjadi Kesalahan, Stock Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    '    Exit Sub

                    'End If

#End Region

                    '================================
                    '=     CONVER MENJADI SCRAP     =
                    '================================
                    Dim Jumlah_Convert_Scrap As Double = 0
                    'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "', "
                    'SQL = SQL & "'" & arrBarang_Req_Packaging_Satuan(Cmb_Barang_Retur.SelectedIndex) & "', '" & arrBarang_Scrap_Satuan_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "', '" & HilangkanTanda(Txt_Jumlah_Retur.Text) & "' ) as hasil "

                    SQL = "select Berat from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Stock_Owner = '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' and Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            'Jumlah_Convert_Scrap = Dr("hasil")
                            Jumlah_Convert_Scrap = Math.Round(Val(HilangkanTanda(Txt_Jumlah_Retur.Text)) * (Val(HilangkanTanda(Dr("Berat"))) / 1000), 4)
                        End If
                    End Using

                    '========================
                    '=     TAMBAH STOCK     =
                    '========================

#Region "Tambah Stock"

                    '========================================
                    '=     GET DATA DETAIL BARANG RETUR     =
                    '========================================
                    Dim sisa As Double = 0
                    Dim Nilai_Packaging As Double = 0
                    Dim JumlahPotong As Double = 0
                    Dim HPP_Packaging As Double = 0

                    Dim Tanggal_Expired_Pertama As String = ""
                    Dim Tanggal_Produksi_Pertama As String = ""
                    Dim Tanggal_Masuk_Pertama As String = ""
                    Dim Batch_Number As String = ""
                    Dim Kode_Unik_Asal As String = ""


                    SQL = "select kode_stock_owner, kode_barang, serial_number, dbo.get_hpp(Serial_Number) as HPP, round(jumlah,4) as jumlah, Tgl_Expired, Tgl_Produksi, Batch_Number, "
                    SQL = SQL & "Kode_Unik_Asal, Tgl_Masuk "
                    SQL = SQL & "from barang_sn where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' and "
                    SQL = SQL & "kode_barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' and jumlah <> 0 "
                    SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                sisa = Val(HilangkanTanda(Txt_Jumlah_Retur.Text))

                                For i As Integer = 0 To .Rows.Count - 1
                                    If sisa = 0 Then
                                        Exit For
                                    ElseIf sisa < 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terdapat Kesalahan saat Potong Barang SN", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    Dim HppPackaging As Double = Val(HilangkanTanda(Ds.Tables("MyTable").Rows(i).Item("HPP")))
                                    HPP_Packaging = Val(HilangkanTanda(Ds.Tables("MyTable").Rows(i).Item("HPP")))

                                    Tanggal_Expired_Pertama = Ds.Tables("MyTable").Rows(i).Item("Tgl_Expired")
                                    Tanggal_Produksi_Pertama = Ds.Tables("MyTable").Rows(i).Item("Tgl_Produksi")
                                    Tanggal_Masuk_Pertama = Ds.Tables("MyTable").Rows(i).Item("Tgl_Masuk")
                                    Batch_Number = Ds.Tables("MyTable").Rows(i).Item("Batch_Number")
                                    Kode_Unik_Asal = Ds.Tables("MyTable").Rows(i).Item("Kode_Unik_Asal")

                                    If sisa < Val(Ds.Tables("MyTable").Rows(i).Item("jumlah")) Or sisa = Val(Ds.Tables("MyTable").Rows(i).Item("jumlah")) Then
                                        SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & Ds.Tables("MyTable").Rows(i).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & Ds.Tables("MyTable").Rows(i).Item("kode_barang") & "' and "
                                        SQL = SQL & "serial_number = '" & Ds.Tables("MyTable").Rows(i).Item("serial_number") & "'"
                                        ExecuteTrans(SQL)

                                        SQL = "insert into EMI_Production_Results_Detail_Change_Packaging_det (Kode_Perusahaan, No_Transaksi, Kode_stock_owner, Kode_Barang, Serial_number, Nilai) "
                                        SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoTransaksi & "', '" & Ds.Tables("MyTable").Rows(i).Item("kode_stock_owner") & "', "
                                        SQL = SQL & "'" & Ds.Tables("MyTable").Rows(i).Item("kode_barang") & "', '" & Ds.Tables("MyTable").Rows(i).Item("serial_number") & "', "
                                        SQL = SQL & "'" & sisa & "')"
                                        ExecuteTrans(SQL)

                                        Nilai_Packaging = Nilai_Packaging + (HppPackaging * sisa)
                                        JumlahPotong += sisa
                                        sisa = 0
                                    ElseIf sisa > Val(Ds.Tables("MyTable").Rows(i).Item("jumlah")) Then
                                        SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & Ds.Tables("MyTable").Rows(i).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & Ds.Tables("MyTable").Rows(i).Item("kode_barang") & "' and "
                                        SQL = SQL & "serial_number = '" & Ds.Tables("MyTable").Rows(i).Item("serial_number") & "'"
                                        ExecuteTrans(SQL)

                                        SQL = "insert into EMI_Production_Results_Detail_Change_Packaging_det (Kode_Perusahaan, No_Transaksi, Kode_stock_owner, Kode_Barang, Serial_number, Nilai) "
                                        SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoTransaksi & "', '" & Ds.Tables("MyTable").Rows(i).Item("kode_stock_owner") & "', "
                                        SQL = SQL & "'" & Ds.Tables("MyTable").Rows(i).Item("kode_barang") & "', '" & Ds.Tables("MyTable").Rows(i).Item("serial_number") & "', "
                                        SQL = SQL & "'" & Ds.Tables("MyTable").Rows(i).Item("jumlah") & "')"
                                        ExecuteTrans(SQL)

                                        Nilai_Packaging = Nilai_Packaging + (HppPackaging * Val(HilangkanTanda(Format(Ds.Tables("MyTable").Rows(i).Item("jumlah"), "N4"))))
                                        JumlahPotong += Val(HilangkanTanda(Format(Ds.Tables("MyTable").Rows(i).Item("jumlah"), "N4")))
                                        sisa = sisa - Val(HilangkanTanda(Format(Ds.Tables("MyTable").Rows(i).Item("jumlah"), "N4")))
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang " & arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If Math.Round(sisa, 4) <> 0 And i = Ds.Tables("MyTable").Rows.Count - 1 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                Next
                            End If
                        End With
                    End Using

                    '=========================
                    '=     POTONG BARANG     =
                    '=========================
                    SQL = "Update barang set "
                    SQL = SQL & "Good_Stock = Good_Stock - " & HilangkanTanda(JumlahPotong) & " ,  Jumlah_Bags = Jumlah_Bags - 0 "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' and kode_barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "'"
                    ExecuteTrans(SQL)

                    '====================================
                    '=       CEK KESESUAIAN STOCK       =
                    '====================================
                    SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                    SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                    SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                    SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                    SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
                    SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' "
                    SQL = SQL & "AND a.Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalahan . . ! !, Stock Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data tidak ditemukan . . ! !, Stock Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using

                    '===========================================
                    '=     GET WAREHOUSE DAN PALLET KOSONG     =
                    '===========================================
                    Dim available_Id_Warehouse As String = ""
                    Dim available_NoPallet As String = ""

                    SQL = "select top(1) a.id_wms_warehouse_position, 0 as nomor_urut from "
                    SQL = SQL & "view_warehouse_position a "
                    SQL = SQL & "where a.kode_Perusahaan ='" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Kode_Stock_Owner='" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' "
                    Using Dr2 = OpenTrans(SQL)
                        Do While Dr2.Read
                            available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                            available_NoPallet = Dr2("nomor_urut")
                        Loop
                    End Using



                    Dim Lks_tujuan_Nomor As String = arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex)
                    Dim kd_barang = arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex)

                    Dim JumlahKurang As Double = Val(HilangkanTanda(Jumlah_Convert_Scrap))
                    Dim TotalhppLama As Double = Math.Round(Nilai_Packaging, 0)
                    ' Dim hppSekarang As Double = Math.Round(TotalhppLama, 0)
                    Dim HppBaru As Double = (Math.Round(TotalhppLama / Jumlah_Convert_Scrap, 0))
                    Dim TotalhppBaru As Double = Math.Round(HppBaru * Jumlah_Convert_Scrap, 0)

                    'Dim HppBaru As Double = hppSekarang

                    Dim Str As String = Format(random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                    Dim Kode_Unik As String = Str.Substring(0, 5) & "BB" & Chr(64 + Str.Substring(6, 1)) & Str.Substring(6, Len(Str) - 6)
                    Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & HppBaru & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")


                    Dim newQrCode As String = Generate_QR_Batch(kd_barang, Batch_Number)
                    Dim Kode_Berjalan As String = Generate_Random_Kode(10)

                    Dim KualitasBarang As String = "HIJAU"

                    SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Jumlah_Bags, Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, "
                    SQL = SQL & "Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN, Id_Jenis_Kategori_Produksi) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & Lks_tujuan_Nomor & "', '" & kd_barang & "', '" & SN_Baru & "', "
                    SQL = SQL & "'" & HilangkanTanda(Jumlah_Convert_Scrap) & "', '0', '" & Tanggal_Expired_Pertama & "', '" & Tanggal_Produksi_Pertama & "', 0, 0, "
                    SQL = SQL & "'" & available_Id_Warehouse & "', '" & newQrCode & "', '" & Kode_Berjalan & "', '" & Kode_Unik_Asal & "-" & Kode_Berjalan & "', '" & available_NoPallet & "', "
                    SQL = SQL & "'" & Batch_Number & "', '" & KualitasBarang & "', '" & Tanggal_Masuk_Pertama & "', NULL, NULL)"
                    ExecuteTrans(SQL)

                    '=========================
                    '=     INSERT BARANG     =
                    '=========================
                    SQL = "Update barang set "
                    SQL = SQL & "Good_Stock = Good_Stock + " & HilangkanTanda(Jumlah_Convert_Scrap) & " ,  Jumlah_Bags = Jumlah_Bags + 0 "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & Lks_tujuan_Nomor & "' and kode_barang = '" & kd_barang & "'"
                    ExecuteTrans(SQL)

#End Region


                    '==================================
                    '=      GENERATE NEW BARCODE      =
                    '==================================
                    Dim Kd_Barang_Scrap As String = arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex)

                    'HAPUS TABEL SEMENTARA
                    'SQL = "truncate table Cetak_Finish_Good "
                    SQL = "delete N_EMI_Barcode_Label_Retur_Packaging "
                    ExecuteTrans(SQL)

                    kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")
                    Dim fullNewQr As String = newQrCode & "-" & Kode_Berjalan

                    Cmd.Parameters.Clear()
                    Using ImgBarcode1 As Image = Generate_QR_NoPadding(fullNewQr)
                        Using ms1 As New MemoryStream()
                            ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
                            Dim rawData1 As Byte() = ms1.ToArray()

                            Dim param1 As String = "@newBarcode" & kode_unik_print
                            Cmd.Parameters.Add(param1, SqlDbType.Image).Value = rawData1
                        End Using
                    End Using

                    Dim barcode As String = "@newBarcode" & kode_unik_print

                    '=============================
                    '=      GET NAMA BARANG      =
                    '=============================
                    Dim Nama_Scrap As String = ""
                    SQL = "select Nama from barang "
                    SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
                    SQL &= $"and kode_stock_owner = '{arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex)}' "
                    SQL &= $"and kode_barang = '{arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex)}' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Nama_Scrap = Dr("Nama")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data Barang Scrap Tidak Ditemukan di Tabel Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim ID_Routing As String = ""
                    Dim Routing As String = ""
                    SQL = "Select a.Id_Routing, c.Keterangan as Routing "
                    SQL &= $"From EMI_Order_Produksi a "
                    SQL &= $"inner join Emi_Split_Production_Order b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_PO "
                    SQL &= $"inner join EMI_Master_Routing c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Routing = c.Id_Routing "
                    SQL &= $"Where b.No_Transaksi ='{Txt_NoSplit.Text}' and a.kode_Perusahaan ='{KodePerusahaan}' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            ID_Routing = dr("Id_Routing")
                            Routing = dr("Routing")
                        End If
                    End Using

                    '===========================
                    '=     GET TOTAL SCRAP     =
                    '===========================
                    Dim TotalCountScrap As Double = 0
                    'SQL = "select distinct top(1) Nomor from Emi_Production_Results_Detail_Scrap where "
                    'SQL = SQL & "no_transaksi = '" & TxtFormulator_NoFaktur.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                    'SQL = SQL & "order by Nomor Desc "

                    SQL = "select distinct top(1) b.Nomor "
                    SQL &= $"from Emi_Production_Results a "
                    SQL &= $"inner join Emi_Production_Results_Detail_Scrap b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
                    SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
                    'SQL &= $"and a.Status is null "
                    SQL &= $"and a.No_Production_Order = '{Txt_NoSplit.Text}' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If General_Class.CekNULL(Dr("Nomor")) = "" Then
                                TotalCountScrap = 1
                            Else
                                TotalCountScrap = Dr("Nomor") + 1
                            End If
                        Else
                            TotalCountScrap = 1
                        End If
                    End Using

                    SQL = "insert into N_EMI_Barcode_Label_Retur_Packaging (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
                    SQL = SQL & "Proses, Jumlah, Satuan, Nomor, id_routing, routing, Kode_unik_print)  "
                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoSplit.Text & "', " & barcode & ", '" & Kd_Barang_Scrap & "', '" & Nama_Scrap & "', '" & fullNewQr & "', '" & newQrCode & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '0', '" & HilangkanTanda(Jumlah_Convert_Scrap) & "', '" & arrBarang_Scrap_Satuan_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "', "
                    SQL = SQL & "'" & TotalCountScrap & "', '" & ID_Routing & "', '" & Routing & "', '" & kode_unik_print & "') "
                    ExecuteTrans(SQL)



                    SQL = "insert into EMI_Production_Results_Detail_Change_Packaging_Detail "
                    SQL = SQL & "(kode_perusahaan, no_transaksi, Proses, Kode_Stock_Owner, Kode_Barang_Awal, Kode_Barang_Tujuan, Jumlah_Awal, Satuan_Awal, Jumlah_Tujuan, Satuan_Tujuan, Jumlah_Request, Qr_Code, Kode_Unik_Berjalan, SN_Scrap, Nomor_Scrap) "
                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoTransaksi & "', NULL, '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "', "
                    SQL = SQL & "'" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "', '" & arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "', "
                    SQL = SQL & Val(HilangkanTanda(Txt_Jumlah_Retur.Text)) & " ,'" & arrSatuan_Req_Packaging(Cmb_Satuan_Retur.SelectedIndex) & "', " & HilangkanTanda(Jumlah_Convert_Scrap) & ", "
                    SQL = SQL & "'" & arrBarang_Scrap_Satuan_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "', " & HilangkanTanda(Jumlah_Ganti) & ", "
                    SQL = SQL & "'" & newQrCode & "', '" & Kode_Berjalan & "', '" & SN_Baru & "', '" & TotalCountScrap & "')"
                    ExecuteTrans(SQL)




#Region "JURNAL"

                    'dari
                    Dim akun_persediaan_dari As String = ""
                    Dim akun_persediaan_tujuan As String = ""

                    SQL = "select c.akun_Persediaan "
                    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and b.kode_stock_owner = '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' "
                    SQL = SQL & "and b.Kode_Barang='" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
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

                    SQL = "Select c.akun_Persediaan "
                    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Id_Group_Jenis = b.Id_Group_Jenis And "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Id_Group_Jenis = c.Id_Group_Jenis And "
                    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and b.kode_stock_owner = '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' "
                    SQL = SQL & "and b.Kode_Barang='" & arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            akun_persediaan_tujuan = Dr("akun_Persediaan")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim akun_HPP_FG As String = ""
                    Dim Akun_Pembulatan_FG As String = ""

                    Dim keterangaN0 As String = ""
                    Dim keterangan3 As String = ""

                    SQL = "select HPP_Barang_Setengah_Jadi, Persediaan_Barang_Dalam_Proses, "
                    SQL = SQL & "HPP,Pembulatan_Finished_Good,Pembulatan_Semi_FG, Persediaan_Scrap from stock_owner_gudang "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex) & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            Akun_Pembulatan_FG = Dr("Pembulatan_Finished_Good")
                            keterangan3 = "Pembulatan HPP Barang Jadi "
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using



                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                    SQL = SQL & "'" & Kode_voucher & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    SQL = SQL & "'" & KodeProyek & "', 'Rertur Packaging : " & Txt_NoTransaksi & "', '', "
                    SQL = SQL & "'-', '" & UserID & "')"
                    ExecuteTrans(SQL)

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                          Strings.Mid(akun_persediaan_dari, 2, 1),
                          Strings.Mid(Ganti(akun_persediaan_dari), 3),
                          KodePerusahaan, KodeProyek, "Persedian " & Txt_NoTransaksi, "0", TotalhppLama, pagenumber, arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex), Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                         Strings.Mid(akun_persediaan_tujuan, 2, 1),
                         Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                         KodePerusahaan, KodeProyek, "Persedian " & Txt_NoTransaksi, TotalhppBaru, "0", pagenumber, arrGudang_Packaging(Cmb_Lokasi_Retur.SelectedIndex), Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    Dim selisih_pembulatan As Double = Math.Round(TotalhppBaru - TotalhppLama, 0)

                    If selisih_pembulatan > 0 Then
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Pembulatan_FG, 1),
                                              Strings.Mid(Akun_Pembulatan_FG, 2, 1),
                                              Strings.Mid(Ganti(Akun_Pembulatan_FG), 3),
                                              KodePerusahaan, KodeProyek, "Selisih Pembulatan " & Txt_NoTransaksi, "0", selisih_pembulatan, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1
                    Else
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Pembulatan_FG, 1),
                                              Strings.Mid(Akun_Pembulatan_FG, 2, 1),
                                              Strings.Mid(Ganti(Akun_Pembulatan_FG), 3),
                                              KodePerusahaan, KodeProyek, "Selisih Pembulatan " & Txt_NoTransaksi, Math.Abs(selisih_pembulatan), "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1
                    End If


                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
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
#End Region


                End If


            End If

            'If True Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Tahan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            '    Exit Sub
            'End If




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

        Try
            OpenConn()
            Dim CrDoc As New Object

            Dim KertasBesar As String = "BarcodeFG"

            SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Retur_Packaging where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    Dim printerDitemukan As Boolean = False
                    '==========================
                    '=     BARCODEE BESAR     =
                    '==========================
                    For Each printer As String In PrinterSettings.InstalledPrinters
                        If printer.ToLower() = PrinterBarcode.ToLower() Then
                            printerDitemukan = True
                            Exit For
                        End If
                    Next

                    printerDitemukan = True

                    CrDoc = New N_EMI_Barcode_Retur_Packaging

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Retur_Packaging.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Barang} = '" & arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Unik_Print} = '" & kode_unik_print & "'  "
                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                    '    .Text = "New Barcode Finish Good"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    If printerDitemukan Then
                        CrDoc = New N_EMI_Barcode_Retur_Packaging

                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Retur_Packaging.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Barang} = '" & arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Unik_Print} = '" & kode_unik_print & "'  "
                        '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                        '    .Text = "New Barcode Finish Good"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        '============================================

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Retur_Packaging.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Barang} = '" & arrBarang_Scrap_Req_Packaging(Cmb_Barang_Scrap.SelectedIndex) & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Unik_Print} = '" & kode_unik_print & "'  "
                        CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        CrDoc.PrintToPrinter(1, False, 1, 2500)




                    Else
                        MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End If



                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub



    Private Sub Cmb_Lokasi_Retur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lokasi_Retur.SelectedIndexChanged
        If Cmb_Lokasi_Retur.Items.Count = 0 Or Cmb_Lokasi_Retur.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cmb_Barang_Retur.Items.Clear() : arrBarang_Req_Packaging_Satuan.Clear() : arrBarang_Req_Packaging.Clear()
            SQL = "select a.No_Faktur_Order, b.Kode_Barang, c.Nama as Nama_Barang, b.Satuan "
            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_det b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.Jenis_Material = 'Packaging' "
            SQL = SQL & "and a.No_Faktur_Order = '" & Txt_NoSplit.Text & "' "
            SQL = SQL & "group by a.No_Faktur_Order, b.Kode_Barang, c.Nama, b.Satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Barang_Retur.Items.Add(Dr("Nama_Barang")) : arrBarang_Req_Packaging.Add(Dr("Kode_Barang"))
                    arrBarang_Req_Packaging_Satuan.Add(Dr("Satuan"))
                Loop
            End Using

            Cmb_Barang_Scrap.Items.Clear() : arrBarang_Scrap_Req_Packaging.Clear() : arrBarang_Scrap_Satuan_Req_Packaging.Clear()
            SQL = "select a.kode_barang_Scrap, b.nama, b.satuan as satuan_kecil, c.satuan from "
            SQL = SQL & "emi_binding_scrap a, barang b, barang_detail_satuan c "
            SQL = SQL & "where a.kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang_scrap=B.Kode_Barang "
            SQL = SQL & "and b.kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_stock_Owner='" & Lokasi_Produksi & "' "
            SQL = SQL & "and c.flag_tampil_display = 'Y' and a.Flag_Retur_Packaging='Y' "
            'SQL = SQL & "and a.Kode_Barang_Scrap <> 'SFD0001' " 'INI SEMENTARA UNTUK DEMO
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read


                    Cmb_Barang_Scrap.Items.Add(Dr("nama"))
                    arrBarang_Scrap_Req_Packaging.Add(Dr("kode_barang_Scrap"))
                    arrBarang_Scrap_Satuan_Req_Packaging.Add(Dr("satuan"))
                Loop
            End Using

            Txt_Jumlah_Retur.Text = ""
            Txt_Jumlah_Retur.Enabled = True
            Cmb_Barang_Retur.Enabled = True
            Cmb_Barang_Scrap.Enabled = True
            Txt_Jumlah_Retur.Enabled = False
            Cmb_Satuan_Retur.Enabled = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Cmb_Barang_Retur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Barang_Retur.SelectedIndexChanged
        If Cmb_Barang_Retur.Items.Count = 0 Or Cmb_Barang_Retur.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cmb_Satuan_Retur.Items.Clear()
            SQL = "select a.No_Faktur_Order, b.Kode_Barang, c.Nama as Nama_Barang, b.Satuan "
            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_det b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.Jenis_Material = 'Packaging' "
            SQL = SQL & "and a.No_Faktur_Order = '" & Txt_NoSplit.Text & "' "
            SQL = SQL & "and b.Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
            SQL = SQL & "group by a.No_Faktur_Order, b.Kode_Barang, c.Nama, b.Satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Satuan_Retur.Items.Add(Dr("Satuan")) : arrSatuan_Req_Packaging.Add(Dr("Satuan"))
                    Cmb_Satuan_Retur.SelectedItem = Dr("Satuan")
                Loop
            End Using

            Txt_Jumlah_Retur.Text = ""
            Txt_Jumlah_Retur_Satuan_Scrap.Text = ""

            Txt_Jumlah_Retur.Enabled = True
            Cmb_Satuan_Retur.Enabled = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        TxtJumlahPakai.Text = ""
        Txt_Jumlah_Tot_Retur.Text = ""

        Try
            OpenConn()

            Cmb_Satuan_Tot_Retur.Items.Clear()
            SQL = "select Satuan from EMI_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Satuan_Tot_Retur.Items.Add(Dr("Satuan"))
                Loop
            End Using

            '=========================================
            '=     GET JUMLAH YANG SUDAH DIRETUR     =
            '=========================================
            SQL = "select isnull(sum(b.Jumlah_Awal), 0) as Jumlah_Awal, isnull(sum(b.Jumlah_Request), 0) as Jumlah_Request, b.Satuan_Awal "
            SQL = SQL & "from EMI_Production_Results_Detail_Change_Packaging a, EMI_Production_Results_Detail_Change_Packaging_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Split = '" & Lv_NoSplit & "' "
            SQL = SQL & "and b.Kode_Barang_awal = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
            SQL = SQL & "group by b.Satuan_Awal "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_Jumlah_Tot_Retur.Text = Format(Dr("Jumlah_Request"), "N4")
                    Cmb_Satuan_Tot_Retur.Text = Dr("Satuan_Awal")
                Else
                    Txt_Jumlah_Tot_Retur.Text = Format(0, "N4")
                    Cmb_Satuan_Tot_Retur.SelectedIndex = -1
                End If
            End Using

            SQL = "select isnull(sum(b.Nilai_Produksi), 0) as Nilai from Emi_Production_Results a, Emi_Production_Results_Packaging_Detail b where "
            SQL = SQL & "a.No_Transaksi=b.No_Transaksi and a.Kode_Perusahaan=b.Kode_Perusahaan and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_production_order = '" & Lv_NoSplit & "' "
            SQL = SQL & "and b.Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtJumlahPakai.Text = Format(Dr("Nilai"), "N4")
                Else
                    TxtJumlahPakai.Text = Format(0, "N4")
                End If
            End Using

            SQL = "select distinct berat from barang a where "
            SQL = SQL & " a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & " a.Kode_Barang = '" & arrBarang_Req_Packaging(Cmb_Barang_Retur.SelectedIndex) & "' "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtBerat.Text = Format(Dr("berat") / 1000, "N4")
                Else
                    TxtBerat.Text = Format(0, "N4")
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Cmb_Lokasi_Retur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi_Retur.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Lokasi_Retur.SelectedIndex = -1 Then
                MessageBox.Show("Pilih Dahulu Lokasi Retur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Lokasi_Retur.DroppedDown = True
                Cmb_Lokasi_Retur.Focus()
                Exit Sub
            End If
            Cmb_Lokasi_Retur_SelectedIndexChanged(sender, e)
            Cmb_Barang_Retur.DroppedDown = True
            Cmb_Barang_Retur.Focus()

        End If
    End Sub

    Private Sub Cmb_Barang_Retur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Barang_Retur.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Barang_Retur.SelectedIndex = -1 Then
                MessageBox.Show("Pilih Dahulu Barang Retur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Barang_Retur.DroppedDown = True
                Cmb_Barang_Retur.Focus()
                Exit Sub
            End If
            Cmb_Barang_Retur_SelectedIndexChanged(sender, e)
            Cmb_Barang_Scrap.DroppedDown = True
            Cmb_Barang_Scrap.Focus()

        End If
    End Sub

    Private Sub Cmb_Barang_Scrap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Barang_Scrap.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Barang_Scrap.SelectedIndex = -1 Then
                MessageBox.Show("Pilih Dahulu Barang Scrap Retur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Barang_Scrap.DroppedDown = True
                Cmb_Barang_Scrap.Focus()
                Exit Sub
            End If

            Txt_Jumlah_Retur.Focus()

        End If
    End Sub

    Private Sub Txt_Jumlah_Retur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Jumlah_Retur.KeyPress
        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "."c OrElse Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If

        If e.KeyChar = "."c AndAlso DirectCast(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Txt_Jumlah_Retur_Leave(sender As Object, e As EventArgs) Handles Txt_Jumlah_Retur.Leave
        '======================
        '=     SET FORMAT     =
        '======================
        Dim culture As CultureInfo = CultureInfo.CurrentCulture

        Dim cellKuantity As String = Txt_Jumlah_Retur.Text.Trim

        If cellKuantity = "" Then
            Exit Sub
        End If

        Dim nilai As Decimal = Decimal.Parse(cellKuantity)
        Dim formattedValue As String = nilai.ToString("N4", culture)

        Txt_Jumlah_Retur.Text = formattedValue
    End Sub

    Private Sub Txt_Jumlah_Retur_Enter(sender As Object, e As EventArgs) Handles Txt_Jumlah_Retur.Enter
        If Txt_Jumlah_Retur.Text.Trim.Length = 0 Then Exit Sub

        '======================
        '=     SET FORMAT     =
        '======================

        Dim cellKuantity As String = Txt_Jumlah_Retur.Text.Trim

        If cellKuantity = "" Then
            Exit Sub
        End If

        Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
        Dim nilai As Decimal = Decimal.Parse(cleanedStr)

        Txt_Jumlah_Retur.Text = nilai
    End Sub

    Private Sub Cmb_Lokasi_Retur_DropDownClosed(sender As Object, e As EventArgs) Handles Cmb_Lokasi_Retur.DropDownClosed
        If Cmb_Lokasi_Retur.SelectedIndex <> -1 Then
            Cmb_Barang_Retur.DroppedDown = True
            Cmb_Barang_Retur.Focus()
        Else
            Cmb_Lokasi_Retur.SelectedIndex = -1
            Cmb_Lokasi_Retur.Focus()
        End If
    End Sub

    Private Sub Cmb_Barang_Retur_DropDownClosed(sender As Object, e As EventArgs) Handles Cmb_Barang_Retur.DropDownClosed
        If Cmb_Barang_Retur.SelectedIndex <> -1 Then
            Cmb_Barang_Scrap.DroppedDown = True
            Cmb_Barang_Scrap.Focus()
        Else
            Cmb_Barang_Retur.SelectedIndex = -1
            Cmb_Barang_Retur.Focus()
        End If
    End Sub

    Private Sub Cmb_Barang_Scrap_DropDownClosed(sender As Object, e As EventArgs) Handles Cmb_Barang_Scrap.DropDownClosed
        If Cmb_Barang_Scrap.SelectedIndex <> -1 Then
            Txt_Jumlah_Retur.Focus()
        Else
            Cmb_Barang_Scrap.SelectedIndex = -1
            Cmb_Barang_Scrap.Focus()
        End If
    End Sub





    '============================================================================================================
    '=     UTILITY
    '============================================================================================================

    Private Sub Lv_Data_Split_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data_Split.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Data_Split.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            ' Mouse sedang berada di atas row
            Lv_Data_Split.Cursor = Cursors.Hand
        Else
            ' Mouse tidak mengenai row
            Lv_Data_Split.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Lv_Data_Split_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data_Split.MouseLeave
        Lv_Data_Split.Cursor = Cursors.Default
    End Sub



    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub

    Private Function Generate_QR_NoPadding(ByVal isi As String)

        Dim options As New ZXing.QrCode.QrCodeEncodingOptions()

        options.DisableECI = True
        options.CharacterSet = "UTF-8"
        options.Width = 80
        options.Height = 80
        options.Margin = 0

        Dim qr As New ZXing.BarcodeWriter()
        qr.Format = ZXing.BarcodeFormat.QR_CODE
        qr.Options = options

        Dim result As New Bitmap(qr.Write(isi))
        Return result
    End Function
End Class


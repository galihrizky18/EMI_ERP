Imports System.IO

Public Class N_EMI_Display_Transfer_Stock_Sementara

    Dim Random As New Random()


    Dim Lv_Parent_No_faktur, Lv_Parent_No_Split, Lv_Parent_Jenis_Transfer, Lv_Parent_Lokasi_Awal, Lv_Parent_Lokasi_Tujuan, Lv_Parent_Tanggal, Lv_Parent_Jam, Lv_Parent_Keterangan, Lv_Parent_User As String

    Dim Item_Parent_No_Faktur As Integer = 0
    Dim Item_Parent_No_Split As Integer = 1
    Dim Item_Parent_Jenis_Transfer As Integer = 2
    Dim Item_Parent_Lokasi_Awal As Integer = 3
    Dim Item_Parent_Lokasi_Tujuan As Integer = 4
    Dim Item_Parent_Tanggal As Integer = 5
    Dim Item_Parent_Jam As Integer = 6
    Dim Item_Parent_Keterangan As Integer = 7
    Dim Item_Parent_User As Integer = 8
    Dim Item_Parent_Status_Validasi As Integer = 9

    Dim Lv_Detail_No_RM, Lv_Detail_Kd_Barang, Lv_Detail_Nm_Barang, Lv_Detail_Total, Lv_Detail_Satuan, Lv_Detail_Urut As String

    Dim item_Detail_No_RM As Integer = 0
    Dim item_Detail_Kd_Barang As Integer = 1
    Dim item_Detail_Nm_Barang As Integer = 2
    Dim item_Detail_Total As Integer = 3
    Dim item_Detail_Satuan As Integer = 4
    Dim item_Detail_Urut As Integer = 5


    Dim arrFilterLokasiGudang, arrFilterTanggal, arrFilterParamLain As New ArrayList()



    Private Sub N_EMI_Display_Transfer_Stock_Sementara_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Parent.Columns.Clear()
        Lv_Parent.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '0
        Lv_Parent.Columns.Add("No Split", 130, HorizontalAlignment.Left) '1
        Lv_Parent.Columns.Add("Jenis Transfer", 100, HorizontalAlignment.Center) '2
        Lv_Parent.Columns.Add("Lokasi Awal", 130, HorizontalAlignment.Left) '3
        Lv_Parent.Columns.Add("Lokasi Tujuan", 130, HorizontalAlignment.Left) '4
        Lv_Parent.Columns.Add("Tanggal", 100, HorizontalAlignment.Center) '5
        Lv_Parent.Columns.Add("Jam", 90, HorizontalAlignment.Center) '6
        Lv_Parent.Columns.Add("Keterangan", 190, HorizontalAlignment.Left) '7
        Lv_Parent.Columns.Add("User", 120, HorizontalAlignment.Left) '8
        Lv_Parent.Columns.Add("Status Validasi", 120, HorizontalAlignment.Left) '9
        Lv_Parent.View = View.Details
        Lv_Parent.Columns(Item_Parent_Status_Validasi).DisplayIndex = 2


        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("No RM", 120, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Kode Barang", 110, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Nama Barang", 150, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Total", 100, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Urut", 0, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Status", 120, HorizontalAlignment.Center)
        Lv_Detail.View = View.Details
        Lv_Detail.Columns(6).DisplayIndex = 1


        Lv_Det.Columns.Clear()
        Lv_Det.Columns.Add("Barcode Awal", 200, HorizontalAlignment.Left)
        Lv_Det.Columns.Add("Barcode Tujuan", 200, HorizontalAlignment.Left)
        Lv_Det.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_Det.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Det.View = View.Details


        Try
            OpenConn()

            '===========================================
            '=     LOAD COMBO FILTER LOKASI GUDANG     =
            '===========================================
            Cmb_Lokasi.Items.Clear() : arrFilterLokasiGudang.Clear()
            Cmb_Lokasi.Items.Add(OpsiSeluruh) : arrFilterLokasiGudang.Add(OpsiSeluruh)
            'SQL = "select Kode_Stock_Owner, Keterangan "
            'SQL &= $"from Stock_Owner_Gudang "
            'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            'SQL &= $"order by Kode_Stock_Owner "
            SQL = $"select kode_stock_Owner from stock_owner where kode_perusahaan = '{KodePerusahaan}' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("kode_stock_Owner")) : arrFilterLokasiGudang.Add(Dr("kode_stock_Owner"))
                Loop
            End Using

            If Cmb_Lokasi.Items.Count = 0 Then
                CloseConn()
                MessageBox.Show("Terjadi kesalahan, data gudang tidak ditemukan. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Cmb_Lokasi.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Tanggal.Items.Clear() : arrFilterTanggal.Clear()
        Cmb_Tanggal.Items.Add("Tanggal Transaksi") : arrFilterTanggal.Add("a.Tanggal")

        Cmb_Param_Lain.Items.Clear() : arrFilterParamLain.Clear()
        Cmb_Param_Lain.Items.Add("No Faktur") : arrFilterParamLain.Add("a.No_Faktur")
        Cmb_Param_Lain.Items.Add("No Split") : arrFilterParamLain.Add("a.No_Split")
        Cmb_Param_Lain.Items.Add("Keterangan") : arrFilterParamLain.Add("a.Keterangan")
        Cmb_Param_Lain.Items.Add("User") : arrFilterParamLain.Add("a.UserID")

        Cmb_Status_Validasi.Items.Clear()
        Cmb_Status_Validasi.Items.Add(OpsiSeluruh)
        Cmb_Status_Validasi.Items.Add("Sudah Validasi")
        Cmb_Status_Validasi.Items.Add("Belum Validasi")

        Kosong()

    End Sub



    Private Sub Kosong()


        Cmb_Status_Validasi.SelectedIndex = 0
        Cb_Hari_Ini.Checked = True

        Load_Parent()


    End Sub



    Private Sub Load_Parent()
        Try
            OpenConn()

            Lv_Parent.Items.Clear() : Lv_Detail.Items.Clear() : Lv_Det.Items.Clear()
            'SQL = "select a.No_Faktur, a.No_Split, a.SO_Awal, a.SO_Tujuan, a.Tanggal, a.Jam, a.Jenis_Transfer, a.Lokasi, a.Keterangan, a.UserID, a.Status, "
            'SQL &= $"isnull((select top 1 'Y' "
            'SQL &= $"from N_EMI_Transaksi_Transfer_Stock_Sementara_detail z "
            'SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and z.Urut_Oto = x.Urut_TF and x.Flag_Validasi = 'Y' "
            'SQL &= $"where z.Kode_Perusahaan = a.Kode_Perusahaan  "
            'SQL &= $"and z.No_Faktur = a.No_Faktur ), 'T') as Status_Validasi "
            'SQL &= $"from N_EMI_Transaksi_Transfer_Stock_Sementara a "
            'SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "

            SQL = "select a.No_Faktur, a.No_Split, a.SO_Awal, a.SO_Tujuan, a.Tanggal, a.Jam, a.Jenis_Transfer, a.Lokasi, a.Keterangan, a.UserID, a.Status, "
            SQL &= $"ISNULL(v.Status_Validasi, 'T') AS Status_Validasi "
            SQL &= $"from N_EMI_Transaksi_Transfer_Stock_Sementara a "
            SQL &= $"OUTER APPLY ( SELECT TOP 1 'Y' AS Status_Validasi "
            SQL &= $"FROM N_EMI_Transaksi_Transfer_Stock_Sementara_detail z "
            SQL &= $"INNER JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Det x "
            SQL &= $"ON z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL &= $"AND z.No_Faktur = x.No_Faktur "
            SQL &= $"AND z.Urut_Oto = x.Urut_TF "
            SQL &= $"WHERE z.Kode_Perusahaan = a.Kode_Perusahaan  "
            SQL &= $"AND z.No_Faktur = a.No_Faktur "
            SQL &= $"and x.Flag_Validasi = 'Y') v  "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "

            'If Cmb_Lokasi.SelectedIndex <> 0 Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
            '    SQL &= $" a.Kode_Stock_Owner = '{arrFilterLokasiGudang.Item(Cmb_Lokasi.SelectedIndex)}' "
            'End If

            If Cmb_Status_Validasi.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                Select Case Cmb_Status_Validasi.SelectedIndex
                    Case 1
                        SQL &= $" ISNULL(v.Status_Validasi, 'T') = 'Y' "
                    Case 2
                        SQL &= $" ISNULL(v.Status_Validasi, 'T') = 'T' "
                End Select

            End If

            If Cb_Hari_Ini.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL &= " a.Tanggal >= '" & Format(Date.Today, "yyyy-MM-dd HH:mm:ss") & "' AND a.Tanggal < '" & Format(Date.Today.AddDays(1), "yyyy-MM-dd HH:mm:ss") & "'"

            End If

            If Cb_Tanggal.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL &= arrFilterTanggal.Item(Cmb_Tanggal.SelectedIndex) & " between ' "
                SQL &= Format(Tgl_1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl_2.Value, "yyyy-MM-dd") & "' "
            End If

            If Cb_Param_Lain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL &= arrFilterParamLain.Item(Cmb_Param_Lain.SelectedIndex) & " like '%" & Trim(Txt_Param_Lain.Text) & "%' "
            End If
            SQL &= $"order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Parent.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("No_Split")) = "", "-", Dr("No_Split")))
                    Lv.SubItems.Add(Dr("Jenis_Transfer"))
                    Lv.SubItems.Add(Dr("SO_Awal"))
                    Lv.SubItems.Add(Dr("SO_Tujuan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("UserID"))

                    Dim StatusTransaksi As String = ""
                    If General_Class.CekNULL(Dr("Status_Validasi")).Trim = "Y" Then
                        StatusTransaksi = "Selesai"
                        Lv.BackColor = Color.LightGreen
                        Lv.ForeColor = Color.Black
                    Else
                        StatusTransaksi = "On Process"
                        Lv.BackColor = Color.LightYellow
                        Lv.ForeColor = Color.Black
                    End If

                    If General_Class.CekNULL(Dr("Status")).Trim = "Y" Then
                        StatusTransaksi = "Dibatalkan"
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
                    Else
                        Lv.BackColor = Color.White
                        Lv.ForeColor = Color.Black
                    End If

                    Lv.SubItems.Add(StatusTransaksi)

                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Lv_Parent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Parent.SelectedIndexChanged
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Parent.FocusedItem.SubItems(Item_Parent_No_Faktur).Text

            Lv_Detail.Items.Clear() : Lv_Det.Items.Clear()
            SQL = "select d.No_Faktur as No_Faktur_RM, b.Kode_Barang, c.Nama as Nama_Barang, b.Total, b.Satuan, b.Urut_Oto, "
            SQL &= $"case when exists ( select 1 from N_EMI_Transaksi_Transfer_Stock_Sementara_Det z "
            SQL &= $"where b.Kode_Perusahaan = z.Kode_Perusahaan and b.No_Faktur = z.No_Faktur and b.Urut_Oto = z.Urut_TF and z.Selesai is null "
            SQL &= $") then 'On Process' "
            SQL &= $"else 'Selesai' "
            SQL &= $"end as Status_Transaksi "
            SQL &= $"from N_EMI_Transaksi_Transfer_Stock_Sementara a "
            SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b on a.kode_perusahaan = b.kode_perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and a.SO_Awal = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"left join Emi_Material_Requisition_Det_Convert d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Urut_Material_Requisition_Convert = d.Urut_Oto "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(If(General_Class.CekNULL(Dr("No_Faktur_RM")) = "", "-", Dr("No_Faktur_RM")))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Total"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Urut_Oto"))
                    Lv.SubItems.Add(Dr("Status_Transaksi"))

                    Select Case General_Class.CekNULL(Dr("Status_Transaksi"))
                        Case "Selesai"
                            Lv.BackColor = Color.LightGreen
                        Case "On Process"
                            Lv.BackColor = Color.LightYellow
                        Case Else
                            Lv.BackColor = Color.White
                    End Select
                    Lv.ForeColor = Color.Black
                Loop
            End Using




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Lv_Detail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Detail.SelectedIndexChanged
        If Lv_Detail.Items.Count = 0 Or Lv_Detail.FocusedItem Is Nothing Then Exit Sub
        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Parent.FocusedItem.SubItems(Item_Parent_No_Faktur).Text
            Dim SelectedUrut As String = Lv_Detail.FocusedItem.SubItems(item_Detail_Urut).Text

            Lv_Det.Items.Clear()
            SQL = "select (e.Qr_Code+'-'+e.Kode_Unik_Berjalan) as Barcode_Awal, (f.Qr_Code+'-'+f.Kode_Unik_Berjalan) as Barcode_Tujuan, d.Jumlah, b.Satuan "
            SQL &= $"from N_EMI_Transaksi_Transfer_Stock_Sementara a "
            SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b on a.kode_perusahaan = b.kode_perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL &= $"Left join N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL &= $"Left join barang_sn e on c.Kode_Perusahaan = e.Kode_Perusahaan and a.SO_Awal = e.Kode_Stock_Owner and b.Kode_Barang = e.Kode_Barang and c.Serial_Number_Awal = e.Serial_Number "
            SQL &= $"Left join barang_sn f on d.Kode_Perusahaan = f.Kode_Perusahaan and a.SO_Tujuan = f.Kode_Stock_Owner and b.Kode_Barang = f.Kode_Barang and d.Serial_Number = f.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"and b.Urut_Oto = '{SelectedUrut}' "
            SQL &= $"order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Det.Items.Add(If(General_Class.CekNULL(Dr("Barcode_Awal")) = "", "-", Dr("Barcode_Awal")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Barcode_Tujuan")) = "", "-", Dr("Barcode_Tujuan")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah")) = "", 0, Format(Dr("Jumlah"), "N4")))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cb_Hari_Ini.Checked = False And Cb_Tanggal.Checked = False And Cb_Param_Lain.Checked = False Then
            MessageBox.Show("Check salah satu filter dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cb_Hari_Ini.Focus() : Exit Sub
        End If

        If Cb_Tanggal.Checked Then
            If Cmb_Tanggal.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Tanggal Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Tanggal.DroppedDown = True : Cmb_Tanggal.Focus() : Exit Sub
            ElseIf Tgl_1.Value > Tgl_2.Value Then
                MessageBox.Show("Periode I Tidak Boleh Lebih Dari periode II!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Tgl_1.Value = Now.Date : Tgl_2.Value = Now.Date
                Exit Sub
            End If
        End If

        If Cb_Param_Lain.Checked Then
            If Cmb_Param_Lain.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Lain Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Param_Lain.DroppedDown = True : Cmb_Param_Lain.Focus() : Exit Sub
            ElseIf Txt_Param_Lain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Param_Lain.Focus() : Exit Sub
            End If
        End If

        Load_Parent()
    End Sub


    Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
        If e.KeyChar = Chr(13) Then Cb_Hari_Ini.Focus()
    End Sub
    Private Sub Cb_Hari_Ini_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Hari_Ini.CheckedChanged
        If Cb_Hari_Ini.Checked = True Then
            Cb_Tanggal.Checked = False
            Btn_Cari_Click(Cb_Hari_Ini, e)
        End If
    End Sub
    Private Sub Cb_Hari_Ini_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Hari_Ini.KeyPress
        If e.KeyChar = Chr(13) Then Cb_Tanggal.Focus()
    End Sub
    Private Sub Cb_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Tanggal.CheckedChanged
        If Cb_Tanggal.Checked Then
            Cmb_Tanggal.Enabled = True : Tgl_1.Enabled = True : Tgl_2.Enabled = True
            Cb_Hari_Ini.Checked = False
        Else
            Cmb_Tanggal.Enabled = False : Tgl_1.Enabled = False : Tgl_2.Enabled = False
            Cmb_Tanggal.SelectedIndex = -1 : Tgl_1.Value = Now.Date : Tgl_2.Value = Now.Date
        End If
    End Sub
    Private Sub Cb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cb_Tanggal.Checked Then
                Cmb_Tanggal.DroppedDown = True
                Cmb_Tanggal.Focus()
            Else
                Cb_Param_Lain.Focus()
            End If

        End If
    End Sub

    Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then Tgl_1.Focus()
    End Sub
    Private Sub Tgl_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl_2.Focus()
    End Sub
    Private Sub Tgl_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_2.KeyPress
        If e.KeyChar = Chr(13) Then Cb_Param_Lain.Focus()
    End Sub

    Private Sub Cb_Param_Lain_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Param_Lain.CheckedChanged
        If Cb_Param_Lain.Checked Then
            Cmb_Param_Lain.Enabled = True : Txt_Param_Lain.Enabled = True
        Else
            Cmb_Param_Lain.Enabled = False : Txt_Param_Lain.Enabled = False
            Cmb_Param_Lain.SelectedIndex = -1 : Txt_Param_Lain.Text = ""
        End If
    End Sub
    Private Sub Cb_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cb_Param_Lain.Checked Then
                Cmb_Param_Lain.DroppedDown = True
                Cmb_Param_Lain.Focus()
            Else
                Btn_Cari.Focus()
            End If

        End If
    End Sub

    Private Sub Cmb_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Param_Lain.Focus()
    End Sub
    Private Sub Txt_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub


    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.SelectedItems.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Parent.FocusedItem.SubItems(Item_Parent_No_Faktur).Text)
    End Sub


    Private Sub CetakUlangBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeToolStripMenuItem.Click
        If Lv_Det.Items.Count = 0 Or Lv_Det.FocusedItem Is Nothing Then Exit Sub

        get_jam()


        Dim KdUnikPrintCetak As New ArrayList
        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Parent.FocusedItem.SubItems(Item_Parent_No_Faktur).Text
            Dim SelectedUrut As String = Lv_Detail.FocusedItem.SubItems(item_Detail_Urut).Text
            Dim SelectedBarcodeAwal As String = Lv_Det.FocusedItem.SubItems(0).Text
            Dim SelectedBarcodeTujuan As String = Lv_Det.FocusedItem.SubItems(1).Text

            If SelectedBarcodeTujuan = "-" Then
                CloseConn()
                MessageBox.Show("Cetak Barcode Tidak Dapat dilakukan Karena Belum Melakukan Timbang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '===========================================
            '=     CEK APAKAH TRANSAKSI DIBATALKAN     =
            '===========================================
            SQL = "select Status from N_EMI_Transaksi_Transfer_Stock_Sementara "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{SelectedFaktur}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Cetak Barcode Tidak Dapat Dilakukan Karena Transaksi Sudah Di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("No Faktur Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

            SQL = "delete from N_EMI_Cetak_Transfer_Stock_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            ExecuteTrans(SQL)


            '===============================
            '=       GET DETAIL DATA       =
            '===============================
            Dim NoSplit As String = Lv_Parent.FocusedItem.SubItems(Item_Parent_No_Split).Text
            Dim Kd_Barang As String = ""
            Dim namaBarang As String = ""
            Dim QrLama As String = ""
            Dim batchLama As String = ""
            Dim expDate As Date
            Dim tglCetak As Date
            Dim tglMsk As Date
            Dim metodePengeluaranStock As String = ""
            Dim Jumlah As Double = 0
            Dim Satuan As String = ""
            Dim NoBatch As String = ""
            SQL = "select b.Kode_Barang, f.Nama as Nama_Barang, e.Tgl_Expired, e.Batch_Number, a.Tanggal, e.Tgl_Masuk, f.Metode_Pengeluaran_Stok, e.Qr_Code, "
            SQL &= $"isnull(d.Jumlah, 0) as Jumlah, isnull(b.Satuan, '-') as Satuan, "
            SQL &= $"isnull((select x.Batch "
            SQL &= $"from Emi_Material_Requisition_det_Convert z "
            SQL &= $"inner join Emi_Material_Requisition x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur "
            SQL &= $"where x.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL &= $"and x.Status is null "
            SQL &= $"and z.Urut_Oto = b.Urut_Material_Requisition_Convert ), '-') as No_Batch "
            SQL &= $"from N_EMI_Transaksi_Transfer_Stock_Sementara a "
            SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b on a.kode_perusahaan = b.kode_perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL &= $"inner join barang f on a.Kode_Perusahaan = f.Kode_Perusahaan and a.SO_Awal = f.Kode_Stock_Owner and b.Kode_Barang = f.Kode_Barang "
            SQL &= $"Left join N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL &= $"Left join barang_sn e on d.Kode_Perusahaan = e.Kode_Perusahaan and a.SO_Tujuan = e.Kode_Stock_Owner and b.Kode_Barang = e.Kode_Barang and d.Serial_Number = e.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"and b.Urut_Oto = '{SelectedUrut}' "
            SQL &= $"order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Tgl_Expired")) = "" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Tgl Expired Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Batch_Number")) = "" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Batch Number Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Tgl_Masuk")) = "" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Tgl Masuk Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Kd_Barang = Dr("Kode_Barang")
                    namaBarang = Dr("Nama_Barang")
                    QrLama = Dr("Qr_Code")
                    expDate = Dr("Tgl_Expired")
                    batchLama = Dr("Batch_Number")
                    tglCetak = Dr("Tanggal")
                    tglMsk = Dr("Tgl_Masuk")
                    metodePengeluaranStock = Dr("Metode_Pengeluaran_Stok")
                    Jumlah = Val(HilangkanTanda(Dr("Jumlah")))
                    Satuan = Dr("Satuan")
                    NoBatch = Dr("No_Batch")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using





            Dim kode_unik_print As String = ""
            '=====================================
            '=       GENERATE BARCODE BARU       =
            '=====================================
            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
            Dim fullNewQr As String = SelectedBarcodeTujuan

            Cmd.Parameters.Clear()
            Using ImgBarcode1 As Image = New_Generate_QR(fullNewQr)
                Using ms1 As New MemoryStream()
                    ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
                    Dim rawData1 As Byte() = ms1.ToArray()

                    Dim param1 As String = "@newBarcode" & kode_unik_print
                    Cmd.Parameters.Add(param1, SqlDbType.Image).Value = rawData1
                End Using
            End Using

            Dim BarcodeGenerate As String = "@newBarcode" & kode_unik_print



            '===================================
            '=       INSERT BARCODE BARU       =
            '===================================

            SQL = "insert into N_EMI_Cetak_Transfer_Stock_Sementara (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, "
            SQL = SQL & "kode_unik_print,tanggal_masuk,metode_pengeluaran_stok, No_Split, Jumlah, Satuan, No_Batch) values  "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Kd_Barang & "', " & BarcodeGenerate & ", '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
            SQL = SQL & "'" & Format(expDate, "yyyy-MM-dd") & "', '" & batchLama & "', '" & Format(tglCetak, "yyyy-MM-dd") & "','" & kode_unik_print & "' , "
            SQL = SQL & "'" & Format(tglMsk, "yyyy-MM-dd") & "', '" & metodePengeluaranStock & "', '" & NoSplit & "', '" & Jumlah & "', '" & Satuan & "', '" & NoBatch & "' ) "
            ExecuteTrans(SQL)

            KdUnikPrintCetak.Add(kode_unik_print)





            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '=========================
        '=     CETAK BARCODE     =
        '=========================
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertasBarcode As String = ""


            '=========================
            '=     CETAK BARCODE     =
            '=========================

            For x As Integer = 0 To KdUnikPrintCetak.Count - 1

                SQL = "select Kode_Perusahaan from N_EMI_Cetak_Transfer_Stock_Sementara where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & KdUnikPrintCetak(x) & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New N_EMI_Barcode_Transfer_Stock_Sementara
                        kertasBarcode = "BarcodeFG"


                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Transfer_Stock_Sementara.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Cetak_Transfer_Stock_Sementara.kode_unik_print} = '" & KdUnikPrintCetak(x) & "' "
                        '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock Sementara"
                        '    .Text = "New Barcode Transfer Stock Sementara"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        '=================================================================================================================================================================

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Transfer_Stock_Sementara.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Cetak_Transfer_Stock_Sementara.kode_unik_print} = '" & KdUnikPrintCetak(x) & "' "

                        CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        Dim rawKind As Integer
                        Dim isPaperFound As Boolean = False
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcode Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                isPaperFound = True
                                Exit For
                            End If
                        Next

                        If Not isPaperFound Then
                            'CloseConn()
                            MessageBox.Show("Kertas Tidak DiTemukan, Kertas di set ke default", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            'Exit Sub
                        End If

                        CrDoc.PrintToPrinter(1, False, 1, 99)

                    End If
                End Using

            Next



            CloseConn()
            MessageBox.Show("Berhasil Cetak Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub BatalkanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanToolStripMenuItem.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem.Index = -1 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Transfer Stock Sementara"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Transfer_Stock_Sementara") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Transfer Stock Sementara", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim NoTransfer As String = Lv_Parent.FocusedItem.SubItems(Item_Parent_No_Faktur).Text

            '===================================================
            '=     CEK APAKAH NO TRANSFER SUDAH DIBATALKAN     =
            '===================================================
            SQL = "select Kode_Perusahaan from N_EMI_Transaksi_Transfer_Stock_Sementara "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoTransfer & "' and status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Transfer Stock Sementara tidak dapat dilakukan karena No Transfer Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=====================================================
            '=     CEK APAKAH SEMUA BARANG SUDAH DI VALIDASI     =
            '=====================================================
            SQL = "select a.Kode_Perusahaan from N_EMI_Transaksi_Transfer_Stock_Sementara a, N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b, N_EMI_Transaksi_Transfer_Stock_Sementara_Det c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.status is null and c.Selesai is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Transfer Stock Sementara tidak dapat dilakukan karena Barang Pada No Transfer Belum Di Validasi Sepenuhnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '========================================
            '=     CEK APAKAH SUDAH TUTUP SALDO     =
            '========================================
            Dim HasData As Boolean = False
            Dim TglTfStock As DateTime
            SQL = "select Tanggal from N_EMI_Transaksi_Transfer_Stock_Sementara where status is null and Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoTransfer & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    HasData = True
                    TglTfStock = Dr("Tanggal")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Transfer Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If HasData Then
                If CekSudahTutupSaldo(TglTfStock) = "Y" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Transfer Stock tidak dapat dilakukan karena No Transaksi Sudah Tutup Saldo", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If

            '=====================
            '=     CEK BULAN     =
            '=====================
            If Not tgl_skg.Month = TglTfStock.Month Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Pembatalan Transfer Stock tidak dapat dilakukan karena Sudah Melewati Bulan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            '==================================================
            '=     CEK APAKAH DATA SUDAH DI VALIDASI RFID     =
            '==================================================
            Dim HasValidasiRFID As Boolean = False
            SQL = "select a.Kode_Perusahaan from N_EMI_Transaksi_Transfer_Stock_Sementara a, N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b, N_EMI_Transaksi_Transfer_Stock_Sementara_Det c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.status is null and c.Flag_Validasi = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'Dr.Close()
                    'CloseTrans()
                    'CloseConn()
                    'MessageBox.Show("Pembatalan Transfer Stock Sementara tidak dapat dilakukan karena Barang Pada No Transfer Sudah Di Validasi RFID", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Exit Sub
                    HasValidasiRFID = True
                End If
            End Using


            If HasValidasiRFID Then


                '============================================
                '=     CEK APAKAH BARANG SUDAH DI PAKAI     =
                '============================================
                SQL = "select a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang, c.Serial_Number_Awal, d.Serial_Number as Serial_Number_Tujuan, d.Jumlah, d.Jumlah_Bags, "
                SQL = SQL & "ISNULL(( select z.Jumlah from Barang_SN z where d.kode_perusahaan = z.kode_perusahaan and d.Serial_Number = z.Serial_Number ), NULL) as Jumlah_SN, "
                SQL = SQL & "ISNULL (( select z.Jumlah_Bags from Barang_SN z where d.kode_perusahaan = z.kode_perusahaan and d.Serial_Number = z.Serial_Number  ), NULL) as Jumlah_Bags_SN "
                SQL = SQL & "from N_EMI_Transaksi_Transfer_Stock_Sementara a, N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b, N_EMI_Transaksi_Transfer_Stock_Sementara_Det c, N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 d "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
                SQL = SQL & "and a.Status is null and c.Selesai ='Y' "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & NoTransfer & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                If .Rows(i).Item("Jumlah") <> .Rows(i).Item("Jumlah_SN") Or .Rows(i).Item("Jumlah_Bags") <> .Rows(i).Item("Jumlah_Bags_SN") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Pembatalan Transfer Stock Sementara tidak dapat dilakukan karena Kode Barang : " & .Rows(i).Item("Kode_Barang") & " Sudah Digunakan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data Transfer Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using




                '=========================
                '=     ROLLBACK DATA     =
                '=========================
                '== CEK APAKAH DATA SEIMBANG =='
                SQL = "select a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang "
                SQL = SQL & "from N_EMI_Transaksi_Transfer_Stock_Sementara a, N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and a.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & .Rows(i).Item("SO_Tujuan") & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan, Data Tidak Seimbang . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data Transfer Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

            End If


            '== ROLLBACK DATA =='
            SQL = "select a.No_Faktur, b.Flag_Timbang, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang, c.Serial_Number_Awal, d.Serial_Number as Serial_Number_Tujuan, "
            SQL = SQL & "d.Jumlah as Jumlah_Kecil, d.Jumlah_Bags, d.Kode_Voucher, c.Urut_Oto as Urut_TF, b.Urut_Material_Requisition_Convert "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Stock_Sementara a, N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b, N_EMI_Transaksi_Transfer_Stock_Sementara_Det c, N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL = SQL & "and a.status is null and c.Selesai = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            SQL = SQL & "order by b.Flag_Timbang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim JumlahRollBack As Double = .Rows(i).Item("Jumlah_Kecil")
                            Dim JumlahBagsRollBack As Double = .Rows(i).Item("Jumlah_Bags")
                            Dim Kd_Barang As String = .Rows(i).Item("Kode_Barang")
                            Dim So_Awal As String = .Rows(i).Item("SO_Awal")
                            Dim So_Tujuan As String = .Rows(i).Item("SO_Tujuan")
                            Dim Sn_Awal As String = .Rows(i).Item("Serial_Number_Awal")

                            Dim UrutTF As Integer = .Rows(i).Item("Urut_TF")
                            Dim UrutRM As String = .Rows(i).Item("Urut_Material_Requisition_Convert")

                            '=======================================
                            '=     UPDATE MATERIAL REQUISITION     =
                            '=======================================
                            SQL = "SELECT 1 from Emi_Material_Requisition_Det_Convert "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and Kode_Stock_Owner = '{So_Tujuan}' "
                            SQL &= $"and Kode_Barang = '{Kd_Barang}' "
                            SQL &= $"and Urut_Oto = '{UrutRM}' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dr.Close()
                                    SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = NULL "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Kode_Stock_Owner = '{So_Tujuan}' "
                                    SQL &= $"and Kode_Barang = '{Kd_Barang}' "
                                    SQL &= $"and Urut_Oto = '{UrutRM}' "
                                    ExecuteTrans(SQL)


                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Material Requisition Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            If HasValidasiRFID Then

                                If General_Class.CekNULL(.Rows(i).Item("Serial_Number_Tujuan")) = "" Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Sn Tujuan tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                Dim SN_Tujuan As String = .Rows(i).Item("Serial_Number_Tujuan")
                                Dim KodeVoucher As String = .Rows(i).Item("Kode_Voucher")

                                '== ROLLBACK BARANG SN =='
                                ' PENGURANGAN STOCK
                                SQL = "select Jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Tujuan & "' "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                        If Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah"))) < JumlahRollBack Or Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah_Bags"))) < JumlahBagsRollBack Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan Saat Rollback Data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else

                                            SQL = "update Barang_SN set Jumlah = jumlah - " & JumlahRollBack & ", "
                                            SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & JumlahBagsRollBack & "  "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Tujuan & "' "
                                            ExecuteTrans(SQL)

                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '== UPDATE BARANG =='
                                SQL = "select good_stock, jumlah_bags from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & So_Tujuan & "'  and kode_barang = '" & Kd_Barang & "' "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                        If Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("good_stock"))) < JumlahRollBack Or Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags"))) < JumlahBagsRollBack Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan Saat Rollback Data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else

                                            SQL = "update Barang set good_stock = good_stock - " & JumlahRollBack & ", "
                                            SQL = SQL & "jumlah_bags = jumlah_bags - " & JumlahBagsRollBack & "  "
                                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & So_Tujuan & "'  and kode_barang = '" & Kd_Barang & "' "
                                            ExecuteTrans(SQL)

                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '====================================
                                '=       CEK KESESUAIAN STOCK       =
                                '====================================
                                SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & So_Tujuan & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds2.Tables("MyTable").Rows(0).Item("good_stock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                'PENAMBAHAN STOCK
                                SQL = "select Jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & Sn_Awal & "' "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                        SQL = "update Barang_SN set Jumlah = jumlah + " & JumlahRollBack & ", "
                                        SQL = SQL & "Jumlah_Bags = Jumlah_Bags + " & JumlahBagsRollBack & "  "
                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & Sn_Awal & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '== UPDATE BARANG =='
                                SQL = "select good_stock, jumlah_bags from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & So_Awal & "'  and kode_barang = '" & Kd_Barang & "' "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                        SQL = "update Barang set good_stock = good_stock + " & JumlahRollBack & ", "
                                        SQL = SQL & "jumlah_bags = jumlah_bags + " & JumlahBagsRollBack & "  "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & So_Awal & "'  and kode_barang = '" & Kd_Barang & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '====================================
                                '=       CEK KESESUAIAN STOCK       =
                                '====================================
                                SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & So_Awal & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds2.Tables("MyTable").Rows(0).Item("good_stock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '===========================
                                '=     ROLLBACK JURNAL     =
                                '===========================
                                SQL = "select Kode_Perusahaan from Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KodeVoucher & "' "
                                Using Ds1 = BindingTrans(SQL)
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                        SQL = "delete Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KodeVoucher & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data Jurnal Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '==================================
                                '=     UPDATE TF STOCK DETAIL     =
                                '==================================
                                SQL = "select Kode_Perusahaan from N_EMI_Transaksi_Transfer_Stock_Sementara_Det where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and No_Faktur = '" & NoTransfer & "' and urut_oto = '" & UrutTF & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        Dr.Close()
                                        SQL = "update N_EMI_Transaksi_Transfer_Stock_Sementara_Det set Selesai = NULL where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and No_Faktur = '" & NoTransfer & "' and urut_oto = '" & UrutTF & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data TF Stock Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                            End If

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Transfer Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '===========================
            '=     UPDATE TF STOCK     =
            '===========================
            SQL = "select Kode_Perusahaan from N_EMI_Transaksi_Transfer_Stock_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and No_Faktur = '" & NoTransfer & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "update N_EMI_Transaksi_Transfer_Stock_Sementara set Status = 'Y', UserID_Batal = '" & UserID & "', Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoTransfer & "' and status is null"
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Transfer Stock Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '=============================
            '=     UNBINDING RFIDTAG     =
            '=============================
#Region "UNBINDING RFIDTAG"

            '==================================
            '=     GET NO SPLIT DAN BATCH     =
            '==================================
            SQL = "select distinct e.No_Faktur_Order, e.Batch "
            SQL &= $"from N_EMI_Transaksi_Transfer_Stock_Sementara a "
            SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join Emi_Material_Requisition_Det_Convert c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_Material_Requisition_Convert = c.Urut_Oto "
            SQL &= $"inner join Emi_Material_Requisition_Det d on c.kode_perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.No_Urut_Det = d.Urut_Oto "
            SQL &= $"inner join Emi_Material_Requisition e on d.kode_perusahaan = e.Kode_Perusahaan and d.No_Faktur = e.No_Faktur and e.status is null "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{NoTransfer}' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            '=======================================
                            '=     GET DATA NO FAKTUR TRANSFER     =
                            '=======================================
                            SQL = "select distinct e.No_Faktur "
                            SQL &= $"from Emi_Material_Requisition a "
                            SQL &= $"inner join Emi_Material_Requisition_Det b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  "
                            SQL &= $"inner join Emi_Material_Requisition_Det_Convert c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
                            SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Urut_Oto = d.Urut_Material_Requisition_Convert "
                            SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara e on d.kode_perusahaan = e.Kode_Perusahaan and d.No_Faktur = e.No_Faktur and e.Status is null "
                            SQL &= $"where a.status is null "
                            SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and a.No_Faktur_Order = '{ .Rows(i).Item("No_Faktur_Order")}' "
                            SQL &= $"and a.Batch = '{ .Rows(i).Item("Batch")}'"
                            Using Dr = OpenTrans(SQL)
                                If Not Dr.Read Then

                                    Dr.Close()
                                    '==========================
                                    '=     UNBINDING RFID     =
                                    '==========================
                                    SQL = "
                                        INSERT INTO N_EMI_Pairing_RFID_Log
                                        (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag, 
                                        Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Flag_Pairing_Ulang, Urut_Pairing, Lokasi_Pairing, batch)
                                        SELECT 
                                            Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
                                            Tanggal_Pairing, Jam_Pairing, UserID_Pairing, 'Y', Urut_Pairing, Lokasi_Pairing, batch
                                        FROM N_EMI_Pairing_RFID
                                        WHERE Kode_Perusahaan = @KodePerusahaan
                                        AND No_Split_Production_Order = @NoFaktur
                                    "

                                    Cmd.Parameters.Clear()
                                    Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
                                    Cmd.Parameters.AddWithValue("NoFaktur", .Rows(i).Item("No_Faktur_Order"))
                                    ExecuteTrans(SQL)



                                    SQL = "SELECT RFID_Tag FROM N_EMI_Pairing_RFID WHERE Kode_Perusahaan = @KodePerusahaan AND No_Split_Production_Order = @NoFaktur and batch = @Batch "
                                    Cmd.Parameters.Clear()
                                    Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
                                    Cmd.Parameters.AddWithValue("NoFaktur", .Rows(i).Item("No_Faktur_Order"))
                                    Cmd.Parameters.AddWithValue("Batch", .Rows(i).Item("Batch"))
                                    Using Ds9 = BindingTrans(SQL)
                                        If Ds9.Tables("MyTable").Rows.Count <> 0 Then
                                            For l As Integer = 0 To Ds9.Tables("MyTable").Rows.Count - 1
                                                Dim rfidTag As String = Ds9.Tables("MyTable").Rows(l).Item("RFID_Tag").ToString()
                                                SQL = "UPDATE N_EMI_Master_Data_RFID_Tags SET Status = NULL, No_Production_Order = NULL, Batch = NULL WHERE RFID_Tag = @RFIDTag"
                                                Cmd.Parameters.Clear()
                                                Cmd.Parameters.AddWithValue("RFIDTag", rfidTag)
                                                ExecuteTrans(SQL)
                                            Next
                                        End If
                                    End Using

                                    SQL = "
                                        DELETE FROM N_EMI_Pairing_RFID
                                        WHERE Kode_Perusahaan = @KodePerusahaan
                                            AND No_Split_Production_Order = @NoFaktur
                                            and batch = @Batch
                                    "
                                    Cmd.Parameters.Clear()
                                    Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
                                    Cmd.Parameters.AddWithValue("NoFaktur", .Rows(i).Item("No_Faktur_Order"))
                                    Cmd.Parameters.AddWithValue("Batch", .Rows(i).Item("Batch"))
                                    ExecuteTrans(SQL)



                                End If
                            End Using





                        Next
                    End If
                End With
            End Using







#End Region




            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Transfer Stock Sementara Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cb_Hari_Ini.Checked = True
        Btn_Cari_Click(e, New EventArgs)


    End Sub


    Public Function New_Generate_QR(ByVal isi As String)
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
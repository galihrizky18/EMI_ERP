Public Class N_EMI_Display_Validasi_GR_3

    Dim ArrTanggal, ArrParamLain As New ArrayList

    Private Sub N_EMI_Display_Validasi_GR_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No Split", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Barang", 300, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("UserID", 120, HorizontalAlignment.Center)
        Lv_Data.View = View.Details

        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("Lokasi Awal", 150, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Lokasi Tujuan", 150, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Barang", 250, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Batch Number", 120, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Barcode Awal", 250, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Barcode Tujuan", 250, HorizontalAlignment.Left)
        Lv_Detail.View = View.Details



        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear()
            Cmb_Lokasi.Items.Add(OpsiSeluruh)
            SQL = "Select kode_stock_owner "
            SQL = SQL & "From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
                Loop
                Cmb_Lokasi.Text = Lokasi
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_ParamTgl.Items.Clear() : ArrTanggal.Clear()
        Cmb_ParamTgl.Items.Add("Tanggal") : ArrTanggal.Add("a.Tanggal")

        Cmb_ParamLain.Items.Clear() : Cmb_ParamLain.Text = "" : ArrParamLain.Clear()
        Cmb_ParamLain.Items.Add("No Transaksi") : ArrParamLain.Add("a.no_transaksi")
        'Cmb_ParamLain.Items.Add("No Split") : ArrParamLain.Add("a.No_Production_Order")
        Cmb_ParamLain.Items.Add("Kode Barang") : ArrParamLain.Add("d.Kode_Barang")
        Cmb_ParamLain.Items.Add("Nama Barang") : ArrParamLain.Add("e.Nama")
        Cmb_ParamLain.Items.Add("User ID") : ArrParamLain.Add("a.UserID")


        Kosong()

    End Sub



    Private Sub Kosong()

        Chk_TransaksiHrIni.Checked = False
        Chk_ParamTgl.Checked = False : Cmb_ParamTgl.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        Chk_ParamLain.Checked = False : Cmb_ParamLain.Enabled = False : Txt_ParamValue.Enabled = False

        Cmb_ParamTgl.SelectedIndex = -1 : Cmb_ParamLain.SelectedIndex = -1
        DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        Txt_ParamValue.Text = ""


        LoadData()

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


        LoadData()
    End Sub





    Private Sub LoadData()

        Try
            OpenConn()

            Lv_Data.Items.Clear() : Lv_Detail.Items.Clear()
            SQL = "select a.No_Transaksi, a.No_Production_Order, b.Kode_Barang, c.Nama as Nama_Barang, a.Keterangan, a.Tanggal, a.Jam, a.UserID, a.Status "
            SQL = SQL & "from N_EMI_Validation_GR_3 a, Emi_Split_Production_Order b, barang c "
            SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and b.kode_Stock_owner = c.kode_Stock_owner and b.kode_barang = c.kode_barang "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            If Cmb_Lokasi.SelectedIndex > 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & "b.lokasi = '" & Cmb_Lokasi.Text & "' "
            End If

            If Chk_TransaksiHrIni.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & "a.Tanggal between '" & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now.AddDays(1), "yyyy-MM-dd") & "' "
            End If

            If Chk_ParamTgl.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & ArrTanggal(Cmb_ParamTgl.SelectedIndex) & " between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value.AddDays(1), "yyyy-MM-dd") & "' "
            End If

            If Chk_ParamLain.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & ArrParamLain(Cmb_ParamLain.SelectedIndex) & " like '%" & Txt_ParamValue.Text & "%' "
            End If

            SQL = SQL & "order by a.Tanggal, a.Jam, a.No_Transaksi "



            SQL = "select distinct a.No_Transaksi, d.Kode_Barang, e.Nama as Nama_Barang, a.Keterangan, a.Tanggal, a.Jam, a.UserID, a.Status "
            SQL &= $"from N_EMI_Validation_GR_3 a "
            SQL &= $"inner join N_EMI_Validation_GR_3_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Awal = c.Kode_Stock_Owner_Tujuan and b.Serial_Number_Awal = c.Serial_Number_Akhir "
            SQL &= $"inner join Emi_Split_Production_Order d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Split = d.No_Transaksi and d.Status is null "
            SQL &= $"inner join barang e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
            SQL &= $"where a.kode_perusahaan = '{KodePerusahaan}' "

            If Cmb_Lokasi.SelectedIndex > 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & "d.lokasi = '" & Cmb_Lokasi.Text & "' "
            End If

            If Chk_TransaksiHrIni.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & "a.Tanggal between '" & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now.AddDays(1), "yyyy-MM-dd") & "' "
            End If

            If Chk_ParamTgl.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & ArrTanggal(Cmb_ParamTgl.SelectedIndex) & " between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value.AddDays(1), "yyyy-MM-dd") & "' "
            End If

            If Chk_ParamLain.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & ArrParamLain(Cmb_ParamLain.SelectedIndex) & " like '%" & Txt_ParamValue.Text & "%' "
            End If

            'SQL &= $"and a.No_Transaksi = 'VGR3-0226-00006' "
            SQL &= $"order by a.Tanggal, a.Jam, a.No_Transaksi "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("No_Production_Order"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("UserID"))

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
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

    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data.SelectedIndexChanged
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Data.FocusedItem.Text

            Lv_Detail.Items.Clear()
            'SQL = "select a.No_Transaksi, b.Kode_Stock_Owner_Awal, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang, b.Batch_Number, b.Jumlah, b.Satuan, "
            'SQL = SQL & "(d.Qr_Code + '-' + d.Kode_Unik_Berjalan) as Barcode_Awal, "
            'SQL = SQL & "(e.Qr_Code + '-' + e.Kode_Unik_Berjalan) as Barcode_Tujuan "
            'SQL = SQL & "from N_EMI_Validation_GR_3 a, N_EMI_Validation_GR_3_Detail b, barang c, Barang_SN_sementara d, Barang_SN e "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "and a.no_transaksi = b.no_transaksi "
            'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            'SQL = SQL & "and b.Kode_Stock_Owner_Awal = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and b.Serial_Number_Awal = d.Serial_Number "
            'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and b.Kode_Barang = e.Kode_Barang and b.Serial_Number_Tujuan = e.Serial_Number "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & SelectedFaktur & "' "
            'SQL = SQL & "order by b.Kode_Barang "


            SQL = "select a.No_Transaksi, b.Kode_Stock_Owner_Awal, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang, b.Batch_Number, sum(b.Jumlah) as Jumlah, b.Satuan, "
            SQL &= $"(d.Qr_Code + '-' + d.Kode_Unik_Berjalan) as Barcode_Awal, (e.Qr_Code + '-' + e.Kode_Unik_Berjalan) as Barcode_Tujuan "
            SQL &= $"from N_EMI_Validation_GR_3 a "
            SQL &= $"inner join N_EMI_Validation_GR_3_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"inner join Barang_SN d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner_Awal = d.Kode_Stock_Owner and b.Serial_Number_Awal = d.Serial_Number "
            SQL &= $"inner join Barang_SN e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and b.Serial_Number_Tujuan = e.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{SelectedFaktur}' "
            SQL &= $"group by a.No_Transaksi, b.Kode_Stock_Owner_Awal, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama, b.Batch_Number, b.Satuan, "
            SQL &= $"(d.Qr_Code + '-' + d.Kode_Unik_Berjalan), (e.Qr_Code + '-' + e.Kode_Unik_Berjalan) "
            SQL &= $"order by b.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("Kode_Stock_Owner_Awal"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner_Tujuan"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Batch_Number"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Barcode_Awal"))
                    Lv.SubItems.Add(Dr("Barcode_Tujuan"))

                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub SalinNoTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoTransaksiToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Dahulu Transaksi Yang Ingin Disalin", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Data.FocusedItem.Text)
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

    Private Sub Cmb_ParamTgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamTgl.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Chk_ParamLain.Focus()
    End Sub

    Private Sub Cmb_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then Txt_ParamValue.Focus()
    End Sub

    Private Sub Txt_ParamValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamValue.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub


    Private Sub Chk_ParamTgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_ParamTgl.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_ParamTgl.Checked Then
                Cmb_ParamTgl.DroppedDown = True
                Cmb_ParamTgl.Focus()
            End If
        End If
    End Sub



    Private Sub Chk_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_ParamLain.Checked Then
                Cmb_ParamLain.DroppedDown = True
                Cmb_ParamLain.Focus()
            End If
        End If
    End Sub












End Class
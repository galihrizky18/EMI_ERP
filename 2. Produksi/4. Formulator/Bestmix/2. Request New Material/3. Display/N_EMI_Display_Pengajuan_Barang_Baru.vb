Public Class N_EMI_Display_Pengajuan_Barang_Baru

    Dim arrLokasi, arrTanggal, arrParamLain As New ArrayList


    Private Sub N_EMI_Display_Pengajuan_Barang_Baru_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 90, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Barang", 140, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 280, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Status", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User", 120, HorizontalAlignment.Left)
        Lv_Data.View = View.Details

        Try
            OpenConn()

            '============================
            '=     LOAD DATA FILTER     =
            '============================
            Cmb_Stock_Owner.Items.Clear() : arrLokasi.Clear()
            SQL = "select kode_stock_owner, keterangan from Stock_Owner "
            Using Dr = OpenTrans(SQL)
                Cmb_Stock_Owner.Items.Add("--- SELURUH ---") : arrLokasi.Add("--- SELURUH ---")
                Do While Dr.Read
                    Cmb_Stock_Owner.Items.Add(Dr("keterangan")) : arrLokasi.Add(Dr("kode_stock_owner"))
                Loop
                Cmb_Stock_Owner.SelectedIndex = 0
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Tanggal.Items.Clear() : arrTanggal.Clear() : DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
        Cmb_Tanggal.Items.Add("Tanggal") : arrTanggal.Add("Tanggal")

        Cmb_ParamLain.Items.Clear() : arrParamLain.Clear() : Txt_ParamLain.Text = ""
        Cmb_ParamLain.Items.Add("No Transaksi") : arrParamLain.Add("No_Transaksi")
        Cmb_ParamLain.Items.Add("Kode Barang") : arrParamLain.Add("Kode_Barang")
        Cmb_ParamLain.Items.Add("Nama Barang") : arrParamLain.Add("Nama_Barang")
        Cmb_ParamLain.Items.Add("User ID") : arrParamLain.Add("UserID")




        Kosong()
    End Sub

    Private Sub Kosong()


        Txt_Kd_Barang.Text = ""
        Txt_Nm_Barang.Text = ""
        Txt_Group_Jenis.Text = ""
        Txt_Klasifikasi_Bahan.Text = ""
        Txt_Klasifikasi_Bahan_2.Text = ""
        Txt_Status_Transaksi.Text = ""
        Txt_Tgl_Validasi.Text = ""
        Txt_Jam_Validasi.Text = ""
        Txt_User_Validasi.Text = ""
        Txt_Kd_Barang.Text = ""
        Txt_Kd_Barang.Text = ""
        Txt_Status_Transaksi.BackColor = Color.FromArgb(235, 235, 235)
        Txt_Status_Transaksi.ForeColor = Color.Black



        Chk_HariIni.Checked = True
        LoadData()

    End Sub

    Private Sub LoadData()

        Txt_Kd_Barang.Text = ""
        Txt_Nm_Barang.Text = ""
        Txt_Group_Jenis.Text = ""
        Txt_Klasifikasi_Bahan.Text = ""
        Txt_Klasifikasi_Bahan_2.Text = ""
        Txt_Status_Transaksi.Text = ""
        Txt_Tgl_Validasi.Text = ""
        Txt_Jam_Validasi.Text = ""
        Txt_User_Validasi.Text = ""
        Txt_Status_Transaksi.BackColor = Color.FromArgb(235, 235, 235)
        Txt_Status_Transaksi.ForeColor = Color.Black


        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select No_Transaksi, Status, Tanggal, Jam, UserID, Kode_Barang, Nama_Barang, "
            SQL &= $"case when Flag_Validasi_Procurement is null then 'On Process' "
            SQL &= $"when Flag_Validasi_Procurement = 'T' then 'Rejected' "
            SQL &= $"when Flag_Validasi_Procurement = 'Y' then 'Validated' "
            SQL &= $"end as Status_Transaksi "
            SQL &= $"from N_EMI_Transaksi_Pengajuan_Barang_Baru "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            'If Not Cmb_Stock_Owner.SelectedIndex = 0 Then
            '    'Pasang And
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & "a.Lokasi = '" & arrLokasi(Cmb_Stock_Owner.SelectedIndex) & "' "
            'End If

            If Chk_HariIni.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "Tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") & "' "

            End If

            If Chk_Tanggal.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "


                SQL = SQL & " " & arrTanggal(Cmb_Tanggal.SelectedIndex) & " Between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If Chk_ParamLain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrParamLain.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Status_Transaksi"))
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

            Dim Selected_Tansaksi As String = Lv_Data.FocusedItem.Text

            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Txt_Group_Jenis.Text = ""
            Txt_Klasifikasi_Bahan.Text = ""
            Txt_Klasifikasi_Bahan_2.Text = ""
            Txt_Status_Transaksi.Text = ""
            Txt_Tgl_Validasi.Text = ""
            Txt_Jam_Validasi.Text = ""
            Txt_User_Validasi.Text = ""


            SQL = "select a.Kode_Barang, a.Nama_Barang, a.Id_Group_Jenis, b.Kode_Group_Jenis, a.ID_Klasifikasi_Bahan, "
            SQL &= $"isnull(c.Keterangan, '-') as Klasifikasi_Bahan, isnull(d.Keterangan, '-') as Klasifikasi_Bahan_2, "
            SQL &= $"a.Flag_Validasi_Procurement, a.Tanggal_Validasi_Procurement, a.Jam_Validasi_Procurement, a.User_Validasi_Procurement "
            SQL &= $"from N_EMI_Transaksi_Pengajuan_Barang_Baru a "
            SQL &= $"inner join EMI_Group_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL &= $"left join EMI_Klasifikasi_Bahan c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Klasifikasi_Bahan = c.Id_Klasifikasi_Bahan "
            SQL &= $"left join EMI_Klasifikasi_Bahan2 d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.ID_Klasifikasi_Bahan_2 = d.Id_Klasifikasi_Bahan2 "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{Selected_Tansaksi}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Txt_Kd_Barang.Text = Dr("Kode_Barang")
                    Txt_Nm_Barang.Text = Dr("Nama_Barang")
                    Txt_Group_Jenis.Text = Dr("Kode_Group_Jenis")
                    Txt_Klasifikasi_Bahan.Text = Dr("Klasifikasi_Bahan")
                    Txt_Klasifikasi_Bahan_2.Text = Dr("Klasifikasi_Bahan_2")

                    If General_Class.CekNULL(Dr("Flag_Validasi_Procurement")) = "Y" Then
                        Txt_Status_Transaksi.Text = "Validated"
                        Txt_Status_Transaksi.BackColor = Color.LightGreen
                        Txt_Tgl_Validasi.Text = Format(Dr("Tanggal_Validasi_Procurement"), "dd MMM yyyy")
                        Txt_Jam_Validasi.Text = Dr("Jam_Validasi_Procurement")
                        Txt_User_Validasi.Text = Dr("User_Validasi_Procurement")
                    ElseIf General_Class.CekNULL(Dr("Flag_Validasi_Procurement")) = "T" Then
                        Txt_Status_Transaksi.Text = "Rejected"
                        Txt_Status_Transaksi.BackColor = Color.DarkRed
                        Txt_Status_Transaksi.ForeColor = Color.White
                    Else
                        Txt_Status_Transaksi.Text = "On Process"
                        Txt_Status_Transaksi.BackColor = Color.Goldenrod
                        Txt_Status_Transaksi.ForeColor = Color.Black
                    End If


                End If
            End Using




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Not Chk_HariIni.Checked And Not Chk_Tanggal.Checked And Not Chk_ParamLain.Checked Then
            MessageBox.Show("Pilih Dahulu Parameter Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Chk_HariIni.Focus()
        End If

        If Chk_Tanggal.Checked Then
            If Cmb_Tanggal.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Tanggal Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Tanggal.DroppedDown = True : Cmb_Tanggal.Focus()
            End If
        End If

        If Chk_ParamLain.Checked Then
            If Cmb_ParamLain.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_ParamLain.DroppedDown = True : Cmb_ParamLain.Focus()
            Else
                If Txt_ParamLain.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_ParamLain.Focus()
                End If
            End If
        End If

        LoadData()
    End Sub







    Private Sub Chk_HariIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_HariIni.CheckedChanged
        If Chk_HariIni.Checked = True Then
            Chk_Tanggal.Checked = False
            Btn_Cari_Click(Chk_Tanggal, e)
        End If
    End Sub

    Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged

        If Chk_Tanggal.Checked Then
            Cmb_Tanggal.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Chk_HariIni.Checked = False
        Else
            Cmb_Tanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_Tanggal.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub


    Private Sub Chk_ParamLain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamLain.CheckedChanged
        If Chk_ParamLain.Checked = True Then
            Cmb_ParamLain.Enabled = True : Txt_ParamLain.Enabled = True
        Else
            Cmb_ParamLain.Enabled = False : Txt_ParamLain.Enabled = False
            Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamLain.Text = ""
        End If
    End Sub

    Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_Data.Cursor = Cursors.Hand
        Else
            Lv_Data.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Lv_Data_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data.MouseLeave
        Lv_Data.Cursor = Cursors.Default
    End Sub










    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub

End Class
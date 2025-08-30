Public Class N_EMI_Laporan_MIlitary_Sampling

    Dim arr_jenis_laporan, arr_Status, arr_Jenis_Military, arr_Filter_Lain As New ArrayList

    Dim Switch_Autocomplete As Boolean = False

    Private Sub N_EMI_Laporan_MIlitary_Sampling_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Cmb_Jenis_Laporan.Items.Clear() : arr_jenis_laporan.Clear()
        Cmb_Jenis_Laporan.Items.Add("REKAP") : arr_jenis_laporan.Add("REKAP")
        Cmb_Jenis_Laporan.Items.Add("DETAIL") : arr_jenis_laporan.Add("DETAIL")

        Cmb_Status.Items.Clear() : arr_Status.Clear()
        Cmb_Status.Items.Add(OpsiSeluruh) : arr_Status.Add(OpsiSeluruh)
        Cmb_Status.Items.Add("READY FOR PACKING") : arr_Status.Add("READY FOR PACKING")
        Cmb_Status.Items.Add("HOLD") : arr_Status.Add("HOLD")
        Cmb_Status.Items.Add("UNTESTED") : arr_Status.Add("UNTESTED")

        Cmb_Jenis_MIlitary.Items.Clear() : arr_Jenis_Military.Clear()
        Cmb_Jenis_MIlitary.Items.Add(OpsiSeluruh) : arr_Jenis_Military.Add(OpsiSeluruh)
        Cmb_Jenis_MIlitary.Items.Add("Sampling 1") : arr_Jenis_Military.Add("1")
        Cmb_Jenis_MIlitary.Items.Add("Sampling 2") : arr_Jenis_Military.Add("2")

        Cmb_Filter_Lain.Items.Clear() : arr_Filter_Lain.Clear()
        Cmb_Filter_Lain.Items.Add(OpsiSeluruh) : arr_Filter_Lain.Add(OpsiSeluruh)
        Cmb_Filter_Lain.Items.Add("User ID") : arr_Filter_Lain.Add("Userid")

        Lv_Split.Columns.Clear()
        Lv_Split.Columns.Add("No Split", 150, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Split.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
        Lv_Split.View = View.Details

        Lv_GR.Columns.Clear()
        Lv_GR.Columns.Add("No GR", 150, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_GR.Columns.Add("User ID", 250, HorizontalAlignment.Left)
        Lv_GR.View = View.Details

        Lv_Military.Columns.Clear()
        Lv_Military.Columns.Add("No Military Sampling", 150, HorizontalAlignment.Left)
        Lv_Military.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Military.Columns.Add("User ID", 250, HorizontalAlignment.Left)
        Lv_Military.View = View.Details

        kosong()
    End Sub

    Private Sub kosong()

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        Switch_Autocomplete = False
        Txt_No_Split.Text = OpsiSeluruh
        Txt_No_GR.Text = OpsiSeluruh
        Txt_No_Military.Text = OpsiSeluruh
        Switch_Autocomplete = True

        Cmb_Status.SelectedIndex = 0
        Cmb_Jenis_MIlitary.SelectedIndex = 0
        Cmb_Jenis_Laporan.SelectedIndex = 0
        Cmb_Filter_Lain.SelectedIndex = 0 : Txt_Value_Lain.Text = ""

        Tgl1.Focus()

    End Sub

    '================================================================================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '================================================================================================================================================================================================
    Private Sub Txt_No_Split_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Split.TextChanged
        If Switch_Autocomplete = False Then Exit Sub
        If Txt_No_Split.Text.Trim.Length = 0 Then
            Me.Size = New Size(595, 407)
            Lv_Split.Visible = False
            Lv_Split.Location = New Point(600, 161)
            Txt_No_Split.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(595, 407)
            Lv_Split.Location = New Point(172, 161)
            Lv_Split.Visible = True
        End If

        Try
            OpenConn()

            Lv_Split.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Split.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            SQL = "select distinct a.No_Production_Order, b.Tanggal, e.Keterangan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, N_EMI_Military_Sampling c, N_EMI_Military_Sampling_Detail d, EMI_Order_Produksi e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and a.No_Production_Order = c.No_Split "
            SQL = SQL & "and c.No_Transaksi = d.No_Transaksi "
            SQL = SQL & "and b.No_PO = e.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order like '%" & Txt_No_Split.Text & "%' "
            SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null and e.Status is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Split.Items.Add(Dr("No_Production_Order"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_GR_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_GR.TextChanged
        If Switch_Autocomplete = False Then Exit Sub
        If Txt_No_GR.Text.Trim.Length = 0 Then
            Me.Size = New Size(595, 407)
            Lv_GR.Visible = False
            Lv_GR.Location = New Point(600, 187)
            Txt_No_GR.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(595, 407)
            Lv_GR.Location = New Point(172, 187)
            Lv_GR.Visible = True
        End If

        Try
            OpenConn()

            Lv_GR.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_GR.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            SQL = "select distinct a.No_Transaksi, a.Tanggal, a.Jam, a.UserID "
            SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, N_EMI_Military_Sampling c, N_EMI_Military_Sampling_Detail d, EMI_Order_Produksi e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and a.No_Production_Order = c.No_Split "
            SQL = SQL & "and c.No_Transaksi = d.No_Transaksi "
            SQL = SQL & "and b.No_PO = e.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi like '%" & Txt_No_GR.Text & "%' "
            SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null and e.Status is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_GR.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("UserID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Military_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Military.TextChanged
        If Switch_Autocomplete = False Then Exit Sub
        If Txt_No_Military.Text.Trim.Length = 0 Then
            Me.Size = New Size(595, 405)
            Lv_Military.Visible = False
            Lv_Military.Location = New Point(600, 213)
            Txt_No_Military.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(595, 437)
            Lv_Military.Location = New Point(172, 213)
            Lv_Military.Visible = True
        End If

        Try
            OpenConn()

            Lv_Military.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Military.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            SQL = "select distinct c.No_Transaksi, c.Tanggal, c.Userid "
            SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, N_EMI_Military_Sampling c, N_EMI_Military_Sampling_Detail d, EMI_Order_Produksi e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and a.No_Production_Order = c.No_Split "
            SQL = SQL & "and c.No_Transaksi = d.No_Transaksi "
            SQL = SQL & "and b.No_PO = e.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.No_Transaksi like '%" & Txt_No_Military.Text & "%' "
            SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null and e.Status is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Military.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Userid"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    '================================================================================================================================================================================================
    '=     HANDLE LEAVE
    '================================================================================================================================================================================================
    Private Sub Txt_No_Split_Leave(sender As Object, e As EventArgs) Handles Txt_No_Split.Leave
        If Txt_No_Split.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Split.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_Split.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = "select distinct a.No_Production_Order, b.Tanggal, e.Keterangan "
                SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, N_EMI_Military_Sampling c, N_EMI_Military_Sampling_Detail d, EMI_Order_Produksi e "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
                SQL = SQL & "and a.No_Production_Order = c.No_Split "
                SQL = SQL & "and c.No_Transaksi = d.No_Transaksi "
                SQL = SQL & "and b.No_PO = e.No_Faktur "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Production_Order = '" & Txt_No_Split.Text & "' "
                SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null and e.Status is null "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_Split.Text = Dr("No_Production_Order")
                        Txt_No_GR.Focus()
                    Else
                        MessageBox.Show("No Split tidak ditemukan . . ! !", Judul)
                        Txt_No_Split.Text = ""
                        Txt_No_Split.Focus()
                    End If

                    Me.Size = New Size(595, 407)
                    Lv_Split.Visible = False
                    Lv_Split.Location = New Point(600, 161)
                End Using
            Else
                'Switch_Leave = False
                'Txt_No_GR.Focus()
                'Switch_Leave = True
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_GR_Leave(sender As Object, e As EventArgs) Handles Txt_No_GR.Leave
        If Txt_No_GR.Text.Trim.Length = 0 Then Exit Sub
        If Lv_GR.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_GR.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = "select distinct a.No_Transaksi, a.Tanggal, a.Jam, a.UserID "
                SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, N_EMI_Military_Sampling c, N_EMI_Military_Sampling_Detail d, EMI_Order_Produksi e "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
                SQL = SQL & "and a.No_Production_Order = c.No_Split "
                SQL = SQL & "and c.No_Transaksi = d.No_Transaksi "
                SQL = SQL & "and b.No_PO = e.No_Faktur "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Transaksi = '" & Txt_No_GR.Text & "' "
                SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null and e.Status is null "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_GR.Text = Dr("No_Transaksi")
                        Txt_No_Military.Focus()
                    Else
                        MessageBox.Show("No GR tidak ditemukan . . ! !", Judul)
                        Txt_No_GR.Text = ""
                        Txt_No_GR.Focus()
                    End If

                    Me.Size = New Size(595, 407)
                    Lv_GR.Visible = False
                    Lv_GR.Location = New Point(600, 187)
                End Using
            Else
                'Switch_Leave = False
                'Txt_No_Military.Focus()
                'Switch_Leave = True
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Military_Leave(sender As Object, e As EventArgs) Handles Txt_No_Military.Leave
        If Txt_No_Military.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Military.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_Military.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = "select distinct c.No_Transaksi, c.Tanggal, c.Userid "
                SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, N_EMI_Military_Sampling c, N_EMI_Military_Sampling_Detail d, EMI_Order_Produksi e "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
                SQL = SQL & "and a.No_Production_Order = c.No_Split "
                SQL = SQL & "and c.No_Transaksi = d.No_Transaksi "
                SQL = SQL & "and b.No_PO = e.No_Faktur "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and c.No_Transaksi = '" & Txt_No_Military.Text & "' "
                SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null and e.Status is null "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_Military.Text = Dr("No_Transaksi")
                        Cmb_Status.DroppedDown = True : Cmb_Status.Focus()
                    Else
                        MessageBox.Show("No Military Sampling tidak ditemukan . . ! !", Judul)
                        Txt_No_Military.Text = ""
                        Txt_No_Military.Focus()
                    End If

                    Me.Size = New Size(595, 407)
                    Lv_Military.Visible = False
                    Lv_Military.Location = New Point(600, 405)
                End Using
            Else
                'Switch_Leave = False
                'Cmb_Status.DroppedDown = True : Cmb_Status.Focus()
                'Switch_Leave = True
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    '================================================================================================================================================================================================
    '=     HANDLE LV
    '================================================================================================================================================================================================
    Private Sub Lv_Split_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Split.DoubleClick
        If Lv_Split.Items.Count = 0 Or Lv_Split.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = Lv_Split.FocusedItem.SubItems(0).Text

        Switch_Autocomplete = False
        Txt_No_Split.Text = Faktur
        Switch_Autocomplete = True

        Me.Size = New Size(595, 407)
        Lv_Split.Visible = False
        Lv_Split.Location = New Point(600, 161)
        Txt_No_GR.Focus()
    End Sub

    Private Sub Lv_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Split.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Split_DoubleClick(Lv_Split, e)
        End If
    End Sub

    Private Sub Lv_GR_DoubleClick(sender As Object, e As EventArgs) Handles Lv_GR.DoubleClick
        If Lv_GR.Items.Count = 0 Or Lv_GR.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = Lv_GR.FocusedItem.SubItems(0).Text

        Switch_Autocomplete = False
        Txt_No_GR.Text = Faktur
        Switch_Autocomplete = True

        Me.Size = New Size(595, 407)
        Lv_GR.Visible = False
        Lv_GR.Location = New Point(600, 187)
        Txt_No_Military.Focus()
    End Sub

    Private Sub Lv_GR_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_GR.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_GR_DoubleClick(Lv_GR, e)
        End If
    End Sub

    Private Sub Lv_Military_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Military.DoubleClick
        If Lv_Military.Items.Count = 0 Or Lv_Military.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = Lv_Military.FocusedItem.SubItems(0).Text

        Switch_Autocomplete = False
        Txt_No_Military.Text = Faktur
        Switch_Autocomplete = True

        Me.Size = New Size(595, 407)
        Lv_Military.Visible = False
        Lv_Military.Location = New Point(600, 213)
        Cmb_Status.DroppedDown = True : Cmb_Status.Focus()
    End Sub

    Private Sub Lv_Military_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Military.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Military_DoubleClick(Lv_Military, e)
        End If
    End Sub

    '================================================================================================================================================================================================
    '=     HANDLE BUTTON
    '================================================================================================================================================================================================

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Cmb_Jenis_Laporan.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Laporan Harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis_Laporan.DroppedDown = True : Cmb_Jenis_Laporan.Focus() : Exit Sub
        ElseIf Txt_No_Split.Text.Trim.Length = 0 Then
            MessageBox.Show("No Split harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_Split.Focus() : Exit Sub
        ElseIf Txt_No_GR.Text.Trim.Length = 0 Then
            MessageBox.Show("No Penerimaan Barang Harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_GR.Focus() : Exit Sub
        ElseIf Txt_No_Military.Text.Trim.Length = 0 Then
            MessageBox.Show("No Military Sampling harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_Military.Focus() : Exit Sub
        ElseIf Cmb_Status.SelectedIndex = -1 Then
            MessageBox.Show("Status Harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Status.DroppedDown = True : Cmb_Status.Focus() : Exit Sub
        ElseIf Cmb_Jenis_MIlitary.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Military Sampling Harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Status.DroppedDown = True : Cmb_Status.Focus() : Exit Sub
        End If

        If Cmb_Filter_Lain.SelectedIndex > 0 Then
            If Txt_Value_Lain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Parameter Lain Harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Value_Lain.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            Dim JudulForm As String = "Laporan Military Sampling"
            Dim SF As String = ""

            If arr_jenis_laporan(Cmb_Jenis_Laporan.SelectedIndex).ToString.ToUpper = "REKAP" Then
                SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Military_Sampling_Rekap "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{N_EMI_View_Laporan_Military_Sampling_Rekap.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {N_EMI_View_Laporan_Military_Sampling_Rekap.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Military_Sampling_Rekap.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_No_Split.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Production_Order = '" & Txt_No_Split.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling_Rekap.No_Production_Order} = '" & Txt_No_Split.Text & "' "
                End If

                If Not Txt_No_GR.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Transaksi_GR = '" & Txt_No_GR.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling_Rekap.No_Transaksi_GR} = '" & Txt_No_GR.Text & "' "
                End If

                If Not Txt_No_Military.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Transaksi = '" & Txt_No_Military.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling_Rekap.No_Transaksi} = '" & Txt_No_Military.Text & "' "
                End If

                If Not Cmb_Status.SelectedIndex = 0 Then
                    SQL = SQL & "and Status = '" & arr_Status(Cmb_Status.SelectedIndex) & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling_Rekap.Status} = '" & arr_Status(Cmb_Status.SelectedIndex) & "' "
                End If

                If Not Cmb_Jenis_MIlitary.SelectedIndex = 0 Then
                    SQL = SQL & "and Jenis_Military_Sampling = " & arr_Jenis_Military(Cmb_Jenis_MIlitary.SelectedIndex) & " "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling_Rekap.Jenis_Military_Sampling} = " & arr_Jenis_Military(Cmb_Jenis_MIlitary.SelectedIndex) & " "
                End If

                If Not Cmb_Filter_Lain.SelectedIndex = 0 Then
                    SQL = SQL & "and " & arr_Filter_Lain(Cmb_Filter_Lain.SelectedIndex) & " like '%" & Txt_Value_Lain.Text & "%' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling_Rekap." & arr_Filter_Lain(Cmb_Filter_Lain.SelectedIndex) & "} like '*" & Txt_Value_Lain.Text & "*' "
                End If
                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim CrDoc As New N_EMI_CR_Laporan_Military_Sampling_Summary_Rekap

                            CrDoc.SetDataSource(DS)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                                Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF

                            With A_Place_For_Printing2
                                .Text = JudulForm
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With
                        Else

                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

            ElseIf arr_jenis_laporan(Cmb_Jenis_Laporan.SelectedIndex).ToString.ToUpper = "DETAIL" Then
                SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Military_Sampling "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{N_EMI_View_Laporan_Military_Sampling.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {N_EMI_View_Laporan_Military_Sampling.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Military_Sampling.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_No_Split.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Production_Order = '" & Txt_No_Split.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling.No_Production_Order} = '" & Txt_No_Split.Text & "' "
                End If

                If Not Txt_No_GR.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Transaksi_GR = '" & Txt_No_GR.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling.No_Transaksi_GR} = '" & Txt_No_GR.Text & "' "
                End If

                If Not Txt_No_Military.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Transaksi = '" & Txt_No_Military.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling.No_Transaksi} = '" & Txt_No_Military.Text & "' "
                End If

                If Not Cmb_Status.SelectedIndex = 0 Then
                    SQL = SQL & "and Status = '" & arr_Status(Cmb_Status.SelectedIndex) & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling.Status} = '" & arr_Status(Cmb_Status.SelectedIndex) & "' "
                End If

                If Not Cmb_Jenis_MIlitary.SelectedIndex = 0 Then
                    SQL = SQL & "and Jenis_Military_Sampling = " & arr_Jenis_Military(Cmb_Jenis_MIlitary.SelectedIndex) & " "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling.Jenis_Military_Sampling} = " & arr_Jenis_Military(Cmb_Jenis_MIlitary.SelectedIndex) & " "
                End If

                If Not Cmb_Filter_Lain.SelectedIndex = 0 Then
                    SQL = SQL & "and " & arr_Filter_Lain(Cmb_Filter_Lain.SelectedIndex) & " like '%" & Txt_Value_Lain.Text & "%' "
                    SF = SF & "And {N_EMI_View_Laporan_Military_Sampling." & arr_Filter_Lain(Cmb_Filter_Lain.SelectedIndex) & "} like '*" & Txt_Value_Lain.Text & "*' "
                End If
                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim CrDoc As New N_EMI_CR_Laporan_Military_Sampling_Summary

                            CrDoc.SetDataSource(DS)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                                Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF

                            With A_Place_For_Printing2
                                .Text = JudulForm
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With
                        Else

                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    '================================================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '================================================================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Jenis_Laporan.DroppedDown = True
            Cmb_Jenis_Laporan.Focus()
        End If
    End Sub

    Private Sub Txt_No_Split_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Split.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_Split.Text.Trim.Length = 0 Then Txt_No_Split.Focus()
            Txt_No_Split_Leave(Txt_No_Split, e)

            Me.Size = New Size(595, 407)
            Lv_Split.Visible = False
            Lv_Split.Location = New Point(600, 161)

            Txt_No_GR.Focus()
        End If
    End Sub

    Private Sub Txt_No_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Split.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Split.Focus()
    End Sub

    Private Sub Txt_No_GR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_GR.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_GR.Text.Trim.Length = 0 Then Txt_No_GR.Focus()
            Txt_No_GR_Leave(Txt_No_GR, e)

            Me.Size = New Size(595, 407)
            Lv_GR.Visible = False
            Lv_GR.Location = New Point(600, 187)

            Txt_No_Military.Focus()
        End If
    End Sub

    Private Sub Txt_No_GR_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_GR.KeyDown
        If e.KeyCode = Keys.Down Then Lv_GR.Focus()
    End Sub

    Private Sub Txt_No_Military_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Military.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_Military.Text.Trim.Length = 0 Then Txt_No_Military.Focus()
            Txt_No_Military_Leave(Txt_No_Military, e)

            Me.Size = New Size(595, 407)
            Lv_Military.Visible = False
            Lv_Military.Location = New Point(600, 405)

            Cmb_Status.DroppedDown = True : Cmb_Status.Focus()
        End If
    End Sub

    Private Sub Txt_No_Military_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Military.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Military.Focus()
    End Sub

    Private Sub Cmb_Status_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Status.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Status.SelectedIndex = -1 Then
                Cmb_Status.DroppedDown = True : Cmb_Status.Focus()
            Else
                Cmb_Jenis_MIlitary.DroppedDown = True : Cmb_Jenis_MIlitary.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Jenis_Laporan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Laporan.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Jenis_Laporan.SelectedIndex = -1 Then
                Cmb_Jenis_Laporan.DroppedDown = True : Cmb_Jenis_Laporan.Focus()
            Else
                Txt_No_Split.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Jenis_MIlitary_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_MIlitary.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Status.SelectedIndex = -1 Then
                Cmb_Jenis_MIlitary.DroppedDown = True : Cmb_Jenis_MIlitary.Focus()
            Else
                Cmb_Filter_Lain.DroppedDown = True : Cmb_Filter_Lain.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Filter_Lain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter_Lain.SelectedIndexChanged
        If Cmb_Filter_Lain.SelectedIndex = 0 Then
            Txt_Value_Lain.Enabled = False
        Else
            Txt_Value_Lain.Enabled = True
        End If
        Txt_Value_Lain.Text = ""
    End Sub

    Private Sub Cmb_Filter_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Filter_Lain.SelectedIndex = 0 Then
                BtnCetak.Focus()
            Else
                Txt_Value_Lain.Focus()
            End If
        End If
    End Sub

End Class
Public Class N_EMI_Laporan_Compare_Budgeting



    Dim switch_auto_complete As Boolean

    Private Sub N_EMI_Laporan_Compare_Budgeting_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_No_Transaksi.Columns.Clear()
        Lv_No_Transaksi.Columns.Add("No Transaksi", 180, HorizontalAlignment.Left)
        Lv_No_Transaksi.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_No_Transaksi.Columns.Add("Jam", 90, HorizontalAlignment.Center)
        Lv_No_Transaksi.View = View.Details

        Lv_Jenis_Biaya.Columns.Clear()
        Lv_Jenis_Biaya.Columns.Add("Kode Jenis Biaya", 200, HorizontalAlignment.Left)
        Lv_Jenis_Biaya.Columns.Add("Jenis Biaya", 130, HorizontalAlignment.Left)
        Lv_Jenis_Biaya.View = View.Details


        Cmb_Jenis_Laporan.Items.Clear()
        Cmb_Jenis_Laporan.Items.Add("Laporan Compare Budgeting")
        Cmb_Jenis_Laporan.Items.Add("Laporan Compare Budgeting Detail Budget")
        Cmb_Jenis_Laporan.Items.Add("Laporan Compare Budgeting Detail Aktual")

        Kosong()
    End Sub

    Private Sub Kosong()
        Tgl1.Value = Date.Now : Tgl2.Value = Date.Now

        Cmb_Jenis_Laporan.SelectedIndex = 0

        switch_auto_complete = True
        Txt_No_Transaksi.Text = OpsiSeluruh
        Txt_Kd_Jenis_Biaya.Text = OpsiSeluruh
        Txt_Nm_Jenis_Biaya.Text = OpsiSeluruh
        switch_auto_complete = False

        Me.Size = New Size(661, 292)



    End Sub

    Private Sub Txt_No_Transaksi_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Transaksi.TextChanged
        If switch_auto_complete Then Exit Sub

        If Txt_No_Transaksi.Text.Trim.Length = 0 Then
            Me.Size = New Size(661, 292)
            Lv_No_Transaksi.Visible = False
            Lv_No_Transaksi.Location = New Point(650, 154)
            Txt_No_Transaksi.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(661, 412)
            Lv_No_Transaksi.Location = New Point(125, 154)
            Lv_No_Transaksi.Visible = True
        End If

        Try
            OpenConn()

            Lv_No_Transaksi.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_No_Transaksi.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
                select No_Transaksi, Tanggal, Jam from EMI_Aktualisasi_Budgeting_WorkCenter
                where Kode_Perusahaan = '{KodePerusahaan}'
                and No_Transaksi like '%{Txt_No_Transaksi.Text}%'
                and Status is null
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_No_Transaksi.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Transaksi_Leave(sender As Object, e As EventArgs) Handles Txt_No_Transaksi.Leave
        If Txt_No_Transaksi.Text.Trim.Length = 0 Then Exit Sub
        If Lv_No_Transaksi.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_Transaksi.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = $"
                    select No_Transaksi, Tanggal, Jam from EMI_Aktualisasi_Budgeting_WorkCenter
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and No_Transaksi = '{Txt_No_Transaksi.Text}'
                    and Status is null
                "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_Transaksi.Text = Dr("No_Transaksi")
                        Txt_Kd_Jenis_Biaya.Focus()
                    Else
                        MessageBox.Show("No Transaksi tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_Transaksi.Text = ""
                        Txt_No_Transaksi.Focus()
                    End If

                    Me.Size = New Size(661, 292)
                    Lv_No_Transaksi.Visible = False
                    Lv_No_Transaksi.Location = New Point(650, 154)
                End Using
            Else
                Txt_Kd_Jenis_Biaya.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Transaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Transaksi.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_Transaksi.Text.Trim.Length = 0 Then Txt_No_Transaksi.Focus()
            Txt_No_Transaksi_Leave(Txt_No_Transaksi, e)

            Me.Size = New Size(661, 292)
            Lv_No_Transaksi.Visible = False
            Lv_No_Transaksi.Location = New Point(650, 154)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_Transaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Transaksi.KeyDown
        If e.KeyCode = Keys.Down Then Lv_No_Transaksi.Focus()
    End Sub

    Private Sub Txt_Kd_Jenis_Biaya_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Jenis_Biaya.TextChanged
        If switch_auto_complete Then Exit Sub

        If Txt_Kd_Jenis_Biaya.Text.Trim.Length = 0 Then
            Me.Size = New Size(661, 292)
            Lv_Jenis_Biaya.Visible = False
            Lv_Jenis_Biaya.Location = New Point(650, 179)
            Txt_Kd_Jenis_Biaya.Text = ""
            Txt_Nm_Jenis_Biaya.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(661, 437)
            Lv_Jenis_Biaya.Location = New Point(125, 179)
            Lv_Jenis_Biaya.Visible = True
        End If

        Try
            OpenConn()

            Lv_Jenis_Biaya.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Jenis_Biaya.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
                select Kode_Jenis_Biaya_Produksi, keterangan from Emi_Jenis_Biaya_Produksi
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Kode_Jenis_Biaya_Produksi  like '%{Txt_Kd_Jenis_Biaya.Text}%'
                order by Kode_Jenis_Biaya_Produksi
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Jenis_Biaya.Items.Add(Dr("Kode_Jenis_Biaya_Produksi"))
                    Lv.SubItems.Add(Dr("keterangan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Jenis_Biaya_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Jenis_Biaya.Leave
        If Txt_Kd_Jenis_Biaya.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Jenis_Biaya.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Jenis_Biaya.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = $"
                   select Kode_Jenis_Biaya_Produksi, keterangan from Emi_Jenis_Biaya_Produksi
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and Kode_Jenis_Biaya_Produksi = '{Txt_Kd_Jenis_Biaya.Text}'
                    order by Kode_Jenis_Biaya_Produksi
                "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kd_Jenis_Biaya.Text = Dr("Kode_Jenis_Biaya_Produksi")
                        Txt_Nm_Jenis_Biaya.Text = Dr("keterangan")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Jenis Biaya tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kd_Jenis_Biaya.Text = ""
                        Txt_Nm_Jenis_Biaya.Text = ""
                        Txt_Kd_Jenis_Biaya.Focus()
                    End If

                    Me.Size = New Size(661, 292)
                    Lv_Jenis_Biaya.Visible = False
                    Lv_Jenis_Biaya.Location = New Point(650, 179)
                End Using
            Else
                BtnCetak.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Jenis_Biaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Jenis_Biaya.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Jenis_Biaya.Text.Trim.Length = 0 Then Txt_Kd_Jenis_Biaya.Focus()
            Txt_Kd_Jenis_Biaya_Leave(Txt_Kd_Jenis_Biaya, e)

            Me.Size = New Size(661, 292)
            Lv_Jenis_Biaya.Visible = False
            Lv_Jenis_Biaya.Location = New Point(650, 179)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Kd_Jenis_Biaya_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Jenis_Biaya.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Jenis_Biaya.Focus()
    End Sub

    Private Sub Lv_No_Transaksi_DoubleClick(sender As Object, e As EventArgs) Handles Lv_No_Transaksi.DoubleClick
        If Lv_No_Transaksi.Items.Count = 0 Or Lv_No_Transaksi.FocusedItem.Index = -1 Then Exit Sub

        Dim No_Transaksi As String = Lv_No_Transaksi.FocusedItem.SubItems(0).Text

        switch_auto_complete = True
        Txt_No_Transaksi.Text = No_Transaksi
        switch_auto_complete = False

        Me.Size = New Size(645, 292)
        Lv_No_Transaksi.Visible = False
        Lv_No_Transaksi.Location = New Point(650, 154)

        Txt_Kd_Jenis_Biaya.Focus()
    End Sub

    Private Sub Lv_No_Transaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_No_Transaksi.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_No_Transaksi_DoubleClick(Lv_No_Transaksi, e)
        End If
    End Sub

    Private Sub Lv_Jenis_Biaya_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Jenis_Biaya.DoubleClick
        If Lv_Jenis_Biaya.Items.Count = 0 Or Lv_Jenis_Biaya.FocusedItem.Index = -1 Then Exit Sub

        Dim Kd_Jenis_Biaya As String = Lv_Jenis_Biaya.FocusedItem.SubItems(0).Text
        Dim Jenis_Biaya As String = Lv_Jenis_Biaya.FocusedItem.SubItems(1).Text

        switch_auto_complete = True
        Txt_Kd_Jenis_Biaya.Text = Kd_Jenis_Biaya
        Txt_Nm_Jenis_Biaya.Text = Jenis_Biaya
        switch_auto_complete = False

        Me.Size = New Size(645, 292)
        Lv_Jenis_Biaya.Visible = False
        Lv_Jenis_Biaya.Location = New Point(650, 179)

        Txt_Kd_Jenis_Biaya.Focus()
    End Sub

    Private Sub Txt_Nm_Jenis_Biaya_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nm_Jenis_Biaya.TextChanged
        If switch_auto_complete Then Exit Sub

        If Txt_Nm_Jenis_Biaya.Text.Trim.Length = 0 Then
            Me.Size = New Size(661, 292)
            Lv_Jenis_Biaya.Visible = False
            Lv_Jenis_Biaya.Location = New Point(650, 179)
            Txt_Kd_Jenis_Biaya.Text = ""
            Txt_Nm_Jenis_Biaya.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(661, 437)
            Lv_Jenis_Biaya.Location = New Point(125, 179)
            Lv_Jenis_Biaya.Visible = True
        End If

        Try
            OpenConn()

            Lv_Jenis_Biaya.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Jenis_Biaya.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
                select Kode_Jenis_Biaya_Produksi, keterangan from Emi_Jenis_Biaya_Produksi
                where Kode_Perusahaan = '{KodePerusahaan}'
                and keterangan  like '%{Txt_Nm_Jenis_Biaya.Text}%'
                order by Kode_Jenis_Biaya_Produksi
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Jenis_Biaya.Items.Add(Dr("Kode_Jenis_Biaya_Produksi"))
                    Lv.SubItems.Add(Dr("keterangan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Nm_Jenis_Biaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Nm_Jenis_Biaya.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kd_Jenis_Biaya_Leave(Txt_Nm_Jenis_Biaya, e)

            Me.Size = New Size(645, 292)
            Lv_Jenis_Biaya.Visible = False
            Lv_Jenis_Biaya.Location = New Point(650, 179)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Nm_Jenis_Biaya_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Nm_Jenis_Biaya.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Jenis_Biaya.Focus()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Cmb_Jenis_Laporan.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Laoran Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis_Laporan.DroppedDown = True
            Cmb_Jenis_Laporan.Focus() : Exit Sub
        ElseIf Txt_No_Transaksi.Text.Trim.Length = 0 Then
            MessageBox.Show("No Transaksi harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_Transaksi.Focus() : Exit Sub
        ElseIf Txt_Kd_Jenis_Biaya.Text.Trim.Length = 0 Then
            MessageBox.Show("Jenis Biaya harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Jenis_Biaya.Focus() : Exit Sub
        End If


        Try
            OpenConn()

            Dim SF As String = ""

            If Cmb_Jenis_Laporan.SelectedIndex = 0 Then

                SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Compare_Budgeting "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Periode_Awal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SQL = SQL & "and Periode_Akhir between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{N_EMI_View_Laporan_Compare_Budgeting.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {N_EMI_View_Laporan_Compare_Budgeting.Periode_Awal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Compare_Budgeting.Periode_Awal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "
                SF = SF & "and {N_EMI_View_Laporan_Compare_Budgeting.Periode_Akhir} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Compare_Budgeting.Periode_Akhir} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_No_Transaksi.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Transaksi = '" & Txt_No_Transaksi.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Compare_Budgeting.No_Transaksi} = '" & Txt_No_Transaksi.Text & "'"
                End If

                If Not Txt_Kd_Jenis_Biaya.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Jenis_Biaya = '" & Txt_Kd_Jenis_Biaya.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Compare_Budgeting.Kode_Jenis_Biaya} = '" & Txt_Kd_Jenis_Biaya.Text & "'"
                End If

                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim CrDoc As New N_EMI_CR_Laporan_Compare_Budgeting

                            CrDoc.SetDataSource(DS)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                                Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF

                            With A_Place_For_Printing2
                                .Text = "Laporan Compare Budgeting"
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With

                        Else

                            CloseConn()
                            MessageBox.Show("Data Pengeluaran Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If
                    End With
                End Using


            ElseIf Cmb_Jenis_Laporan.SelectedIndex = 1 Then

                SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Compare_Budgeting_Detail_Budgeting "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Periode_Awal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SQL = SQL & "and Periode_Akhir between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{N_EMI_View_Laporan_Compare_Budgeting_Detail_Budgeting.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {N_EMI_View_Laporan_Compare_Budgeting_Detail_Budgeting.Periode_Awal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Compare_Budgeting_Detail_Budgeting.Periode_Awal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "
                SF = SF & "and {N_EMI_View_Laporan_Compare_Budgeting_Detail_Budgeting.Periode_Akhir} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Compare_Budgeting_Detail_Budgeting.Periode_Akhir} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_No_Transaksi.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Transaksi = '" & Txt_No_Transaksi.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Compare_Budgeting_Detail_Budgeting.No_Transaksi} = '" & Txt_No_Transaksi.Text & "'"
                End If

                If Not Txt_Kd_Jenis_Biaya.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Jenis_Biaya = '" & Txt_Kd_Jenis_Biaya.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Compare_Budgeting_Detail_Budgeting.Kode_Jenis_Biaya} = '" & Txt_Kd_Jenis_Biaya.Text & "'"
                End If

                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim CrDoc As New N_EMI_CR_Laporan_Compare_Budgeting_Detail_Budgeting

                            CrDoc.SetDataSource(DS)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                                Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF

                            With A_Place_For_Printing2
                                .Text = "Laporan Compare Budgeting Detail Budget"
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With

                        Else

                            CloseConn()
                            MessageBox.Show("Data Pengeluaran Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If
                    End With
                End Using


            ElseIf Cmb_Jenis_Laporan.SelectedIndex = 2 Then

                SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Compare_Budgeting_Detail_Aktual "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Periode_Awal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SQL = SQL & "and Periode_Akhir between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{N_EMI_View_Laporan_Compare_Budgeting_Detail_Aktual.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {N_EMI_View_Laporan_Compare_Budgeting_Detail_Aktual.Periode_Awal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Compare_Budgeting_Detail_Aktual.Periode_Awal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "
                SF = SF & "and {N_EMI_View_Laporan_Compare_Budgeting_Detail_Aktual.Periode_Akhir} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Compare_Budgeting_Detail_Aktual.Periode_Akhir} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_No_Transaksi.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Transaksi = '" & Txt_No_Transaksi.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Compare_Budgeting_Detail_Aktual.No_Transaksi} = '" & Txt_No_Transaksi.Text & "'"
                End If

                If Not Txt_Kd_Jenis_Biaya.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Jenis_Biaya = '" & Txt_Kd_Jenis_Biaya.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Compare_Budgeting_Detail_Aktual.Kode_Jenis_Biaya} = '" & Txt_Kd_Jenis_Biaya.Text & "'"
                End If

                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim CrDoc As New N_EMI_CR_Laporan_Compare_Budgeting_Detail_Aktual

                            CrDoc.SetDataSource(DS)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                                Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF

                            With A_Place_For_Printing2
                                .Text = "Laporan Compare Budgeting Detail Aktual"
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With

                        Else

                            CloseConn()
                            MessageBox.Show("Data Pengeluaran Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Jenis_Laporan.DroppedDown = True
            Cmb_Jenis_Laporan.Focus()
        End If
    End Sub

    Private Sub Cmb_Jenis_Laporan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Laporan.KeyPress
        If e.KeyChar = Chr(13) Then Txt_No_Transaksi.Focus()
    End Sub
End Class
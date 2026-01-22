Public Class N_EMI_Laporan_Pelunasan_Proyek_Cut_Off
    Private Sub N_EMI_Laporan_Pelunasan_Proyek_Cut_Off_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Lv_Faktur.Columns.Clear()
        Lv_Faktur.Columns.Add("No Faktur", 94, HorizontalAlignment.Left)
        Lv_Faktur.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Faktur.Columns.Add("Jam", 110, HorizontalAlignment.Center)
        Lv_Faktur.Columns.Add("User", 94, HorizontalAlignment.Center)
        Lv_Faktur.View = View.Details

        LV_Perusahaan_Import.Columns.Clear()
        LV_Perusahaan_Import.Columns.Add("Kode Supplier", 140, HorizontalAlignment.Left)
        LV_Perusahaan_Import.Columns.Add("Nama Supplier", 200, HorizontalAlignment.Left)
        LV_Perusahaan_Import.View = View.Details

        Cmb_Jenis_Laporan.Items.Clear()
        Cmb_Jenis_Laporan.Items.Add("Laporan Hutang Proyek")
        Cmb_Jenis_Laporan.Items.Add("Laporan Hutang Proyek Rekap")

        kosong()
    End Sub

    Private Sub kosong()
        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        Txt_Faktur.Text = "--- SELURUH ---"
        Txt_Kd_Perusahaan_Import.Text = "--- SELURUH ---" : Txt_Nm_Perusahaan_Import.Text = "--- SELURUH ---"

        Me.Size = New Size(585, 300)
        Lv_Faktur.Visible = False
        Lv_Faktur.Location = New Point(94, 174)

        LV_Perusahaan_Import.Visible = False
        LV_Perusahaan_Import.Location = New Point(94, 200)

        Cmb_Jenis_Laporan.SelectedIndex = 0

        Try
            OpenConn()

            ComboBox4.Items.Clear()
            ComboBox4.Items.Add("--- SELURUH ---")
            SQL = "Select kode_stock_owner From stock_owner_proyek where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox4.Text = Lokasi_Proyek

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Tgl1.Focus()
    End Sub

    Private Sub Txt_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Faktur.KeyDown
        If e.KeyCode = Keys.Down Then
            If Lv_Faktur.Items.Count = 0 Then Exit Sub
            Lv_Faktur.Focus()
        End If
    End Sub

    Private Sub Txt_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Faktur.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Kd_Perusahaan_Import.Focus()
    End Sub

    Private Sub Txt_Faktur_Leave(sender As Object, e As EventArgs) Handles Txt_Faktur.Leave
        If Txt_Faktur.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Faktur.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Faktur.Text = "--- SELURUH ---" Then

                SQL = "select No_Faktur, Tanggal, Jam, UserID from Pembelian_Proyek where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Status is null and No_Faktur = '" & Txt_Faktur.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Faktur.Text = Dr("No_Faktur")
                        Txt_Kd_Perusahaan_Import.Focus()
                    Else
                        MessageBox.Show("No Faktur tidak ditemukan . . ! !", Judul)
                        Txt_Faktur.Text = ""
                        Txt_Faktur.Focus()
                    End If

                    Me.Size = New Size(585, 300)
                    Lv_Faktur.Visible = False
                    Lv_Faktur.Location = New Point(94, 174)
                End Using

            Else
                Txt_Kd_Perusahaan_Import.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Faktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_Faktur.TextChanged
        If Txt_Faktur.Text.Length >= 1 Then

            If Txt_Faktur.Text.Trim.Length = 0 Then
                Lv_Faktur.Visible = False : Exit Sub
            Else
                Lv_Faktur.Visible = True
            End If

            Lv_Faktur.Location = New Point(94, 174)
            Lv_Faktur.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(585, 425)

            Try
                OpenConn()

                Lv_Faktur.Items.Clear()
                Dim Lv As ListViewItem
                Lv = Lv_Faktur.Items.Add("--- SELURUH ---")
                Lv.SubItems.Add("--- SELURUH ---")
                Lv.SubItems.Add("--- SELURUH ---")
                Lv.SubItems.Add("--- SELURUH ---")

                SQL = "select No_Faktur, Tanggal, Jam, UserID from Pembelian_Proyek where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Status is null and No_Faktur like '%" & Txt_Faktur.Text & "%' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Lv = Lv_Faktur.Items.Add(Dr("No_Faktur"))

                        If General_Class.CekNULL(Dr("Tanggal")) = "" Then
                            Lv.SubItems.Add("-")
                        Else
                            Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                        End If

                        If General_Class.CekNULL(Dr("Jam")) = "" Then
                            Lv.SubItems.Add("-")
                        Else
                            Lv.SubItems.Add(Dr("Jam"))
                        End If
                        Lv.SubItems.Add(Dr("UserID"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            Lv_Faktur.Visible = False
            'Lv_DetBarang.Location = New Point(803, 258)
            Lv_Faktur.Visible = False

            'BtnCetak.Location = New Point(415, 175)
            'BtnExit.Location = New Point(498, 175)

            Me.Size = New Point(585, 300)

        End If
    End Sub

    Private Sub Lv_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Faktur.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Faktur_DoubleClick(Lv_Faktur, e)
        End If
    End Sub

    Private Sub Lv_Faktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Faktur.DoubleClick
        If Lv_Faktur.Items.Count = 0 Or Lv_Faktur.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = Lv_Faktur.FocusedItem.SubItems(0).Text

        Txt_Faktur.Text = Faktur

        Me.Size = New Size(585, 300)
        Lv_Faktur.Visible = False
        Lv_Faktur.Location = New Point(94, 200)
        Txt_Kd_Perusahaan_Import.Focus()
    End Sub

    Private Sub Txt_Kd_Perusahaan_Import_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Perusahaan_Import.KeyDown
        If e.KeyCode = Keys.Down Then LV_Perusahaan_Import.Focus()
    End Sub

    Private Sub Txt_Kd_Perusahaan_Import_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Perusahaan_Import.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kd_Perusahaan_Import_Leave(Txt_Nm_Perusahaan_Import, e)

            Me.Size = New Size(585, 300)
            LV_Perusahaan_Import.Visible = False
            LV_Perusahaan_Import.Location = New Point(94, 200)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Kd_Perusahaan_Import_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Perusahaan_Import.Leave
        If Txt_Kd_Perusahaan_Import.Text.Trim.Length = 0 Then Exit Sub
        If LV_Perusahaan_Import.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Perusahaan_Import.Text = "--- SELURUH ---" Then

                SQL = "select Kode_Supplier, Nama from suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Nama = '" & Txt_Kd_Perusahaan_Import.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kd_Perusahaan_Import.Text = Dr("Kode_Supplier")
                        Txt_Nm_Perusahaan_Import.Text = Dr("Nama")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Perusahaan Biaya Import tidak ditemukan . . ! !", Judul)
                        Txt_Kd_Perusahaan_Import.Text = ""
                        Txt_Nm_Perusahaan_Import.Text = ""
                        Txt_Kd_Perusahaan_Import.Focus()
                    End If

                    Me.Size = New Size(585, 300)
                    LV_Perusahaan_Import.Visible = False
                    LV_Perusahaan_Import.Location = New Point(94, 200)
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

    Private Sub Txt_Kd_Perusahaan_Import_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Perusahaan_Import.TextChanged
        If Txt_Kd_Perusahaan_Import.Text.Length >= 1 Then

            If Txt_Kd_Perusahaan_Import.Text.Trim.Length = 0 Then
                LV_Perusahaan_Import.Visible = False : Exit Sub
            Else
                LV_Perusahaan_Import.Visible = True
            End If

            LV_Perusahaan_Import.Location = New Point(94, 200)
            LV_Perusahaan_Import.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(585, 448)

            Try
                OpenConn()

                LV_Perusahaan_Import.Items.Clear()

                Dim Lv As ListViewItem
                Lv = LV_Perusahaan_Import.Items.Add("--- SELURUH ---")
                Lv.SubItems.Add("--- SELURUH ---")

                SQL = "select Kode_Supplier, Nama from suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Nama like '%" & Txt_Kd_Perusahaan_Import.Text & "%' order by Nama "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Lv = LV_Perusahaan_Import.Items.Add(Dr("Kode_Supplier"))
                        Lv.SubItems.Add(Dr("Nama"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            LV_Perusahaan_Import.Visible = False
            'Lv_DetBarang.Location = New Point(803, 258)
            LV_Perusahaan_Import.Visible = False

            'BtnCetak.Location = New Point(415, 175)
            'BtnExit.Location = New Point(498, 175)

            Me.Size = New Point(585, 300)

        End If
    End Sub

    Private Sub LV_Perusahaan_Import_KeyDown(sender As Object, e As KeyEventArgs) Handles LV_Perusahaan_Import.KeyDown
        If e.KeyCode = Keys.Enter Then
            LV_Perusahaan_Import_DoubleClick(LV_Perusahaan_Import, e)
        End If
    End Sub

    Private Sub LV_Perusahaan_Import_DoubleClick(sender As Object, e As EventArgs) Handles LV_Perusahaan_Import.DoubleClick
        If LV_Perusahaan_Import.Items.Count = 0 Or LV_Perusahaan_Import.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = LV_Perusahaan_Import.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = LV_Perusahaan_Import.FocusedItem.SubItems(1).Text

        Txt_Kd_Perusahaan_Import.Text = Faktur
        Txt_Nm_Perusahaan_Import.Text = Keterangan

        Me.Size = New Size(585, 300)
        LV_Perusahaan_Import.Visible = False
        LV_Perusahaan_Import.Location = New Point(94, 200)
        BtnCetak.Focus()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_Faktur.Text.Trim.Length = 0 Then
            MessageBox.Show("Faktur harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Faktur.Focus() : Exit Sub
        ElseIf Txt_Kd_Perusahaan_Import.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Perusahaan Biaya Import harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Perusahaan_Import.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim Auth As String = ""
            SQL = "select Auth_SP from Users where kode_perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Auth = Dr("Auth_SP")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("User Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim NoFaktur, KdSupplier, xLokasi As String

            SQL = "EXEC N_EMI_SP_Pelunasan_Proyek_Cut_off "
            SQL = SQL & "@kode_perusahaan = '" & KodePerusahaan & "', "


            If Not Txt_Faktur.Text = "--- SELURUH ---" Then
                NoFaktur = Txt_Faktur.Text
                SQL = SQL & "@no_faktur = '" & Txt_Faktur.Text & "', "
            Else
                NoFaktur = "NULL"
                SQL = SQL & "@no_faktur = NULL, "
            End If

            If Not Txt_Kd_Perusahaan_Import.Text = "--- SELURUH ---" Then
                KdSupplier = Txt_Kd_Perusahaan_Import.Text
                SQL = SQL & "@Kd_Supplier = '" & Txt_Kd_Perusahaan_Import.Text & "', "
            Else
                KdSupplier = "NULL"
                SQL = SQL & "@Kd_Supplier = NULL, "
            End If

            SQL = SQL & "@tanggal_awal = '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "@tanggal_akhir = '" & Format(Tgl2.Value, "yyyy-MM-dd") & "', "

            If Not ComboBox4.Text = "--- SELURUH ---" Then
                xLokasi = ComboBox4.Text
                SQL = SQL & "@kode_so = '" & ComboBox4.Text & "', "
            Else
                xLokasi = "NULL"
                SQL = SQL & "@kode_so = NULL, "
            End If

            SQL = SQL & "@Auth_SP = '" & Auth & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    Dim SelectionRecord As String = ""
                    Dim CrDoc As Object
                    If Cmb_Jenis_Laporan.SelectedIndex = 0 Then
                        CrDoc = New N_EMI_CR_Laporan_Pelunasan_Proyek_Cut_Off
                    ElseIf Cmb_Jenis_Laporan.SelectedIndex = 1 Then
                        CrDoc = New N_EMI_CR_Laporan_Pelunasan_Proyek_Cut_Off_Rekap
                        SelectionRecord = "{N_EMI_SP_Pelunasan_Proyek_Cut_Off;1.sisa_pelunasan} <> 0"
                    End If

                    With A_Place_For_Printing2
                        ' Set data dan koneksi database
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)

                        CrDoc.SetParameterValue("@kode_perusahaan", KodePerusahaan)

                        If String.IsNullOrEmpty(NoFaktur) OrElse NoFaktur = "NULL" Then
                            CrDoc.SetParameterValue("@no_faktur", DBNull.Value)
                        Else
                            CrDoc.SetParameterValue("@no_faktur", NoFaktur)
                        End If

                        If String.IsNullOrEmpty(KdSupplier) OrElse KdSupplier = "NULL" Then
                            CrDoc.SetParameterValue("@Kd_Supplier", DBNull.Value)
                        Else
                            CrDoc.SetParameterValue("@Kd_Supplier", KdSupplier)
                        End If

                        CrDoc.SetParameterValue("@tanggal_awal", Format(Tgl1.Value, "yyyy-MM-dd"))
                        CrDoc.SetParameterValue("@tanggal_akhir", Format(Tgl2.Value, "yyyy-MM-dd"))

                        If String.IsNullOrEmpty(xLokasi) OrElse xLokasi = "NULL" Then
                            CrDoc.SetParameterValue("@kode_so", DBNull.Value)
                        Else
                            CrDoc.SetParameterValue("@kode_so", xLokasi)
                        End If

                        CrDoc.SetParameterValue("@Auth_SP", Auth)

                        CrDoc.SummaryInfo.ReportTitle = "Periode: s/d " & Format(Tgl2.Value, "yyyy-MM-dd") & Chr(13) & " "
                        CrDoc.RecordSelectionFormula = SelectionRecord
                        .Text = "Laporan Cutoff Pelunasan Proyek"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        '.CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                        .Focus()
                    End With
                Else
                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub N_EMI_Laporan_Pelunasan_Proyek_Cut_Off_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
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
        If e.KeyChar = Chr(13) Then
            ComboBox4.DroppedDown = True
            ComboBox4.Focus()
        End If
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Faktur.Focus()
    End Sub
End Class
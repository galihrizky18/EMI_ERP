Public Class Emi_Laporan_Barang_Masuk

    Dim JudulForm As String = "Laporan Barang Masuk"

    Dim arrFilterMobil, arrFilterSFMobil As New ArrayList

    Private Sub Emi_Laporan_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()

    End Sub

    Private Sub Kosong()

        Tgl1.Value = Now.Date
        Tgl2.Value = Now.Date

        Cmb_FilterBy.Items.Clear()
        Cmb_FilterBy.Items.Add("Per Pallet")
        Cmb_FilterBy.Items.Add("Mobil")
        Cmb_FilterBy.SelectedIndex = 0

        Cmb_FlagValidasi.Items.Clear()
        Cmb_FlagValidasi.Items.Add("--- SELURUH ---")
        Cmb_FlagValidasi.Items.Add("Validasi")
        Cmb_FlagValidasi.Items.Add("Belum Validasi")
        Cmb_FlagValidasi.SelectedIndex = 0

        Cmb_Filter_Mobil.Items.Clear() : arrFilterMobil.Clear() : arrFilterSFMobil.Clear()
        Cmb_Filter_Mobil.Items.Add("--- SELURUH ---") : arrFilterMobil.Add("--- SELURUH ---") : arrFilterSFMobil.Add("--- SELURUH ---")
        Cmb_Filter_Mobil.Items.Add("No Faktur") : arrFilterMobil.Add("No_Faktur") : arrFilterSFMobil.Add("{View_Laporan_Barang_Masuk_Loading.No_Faktur}")
        Cmb_Filter_Mobil.Items.Add("No PO") : arrFilterMobil.Add("No_PO") : arrFilterSFMobil.Add("{View_Laporan_Barang_Masuk_Loading.No_PO}")
        Cmb_Filter_Mobil.SelectedIndex = 0

        Cmb_FlagValidasi.Visible = True

        Cmb_Filter_Mobil.Visible = False
        Txt_ValueFilter.Visible = False

        Label_Status.Text = "Status Validasi"

        Lv_Supplier.Columns.Clear()
        Lv_Supplier.Columns.Add("Kode Suppleir", 150, HorizontalAlignment.Left)
        Lv_Supplier.Columns.Add("Nama Suppleir", 250, HorizontalAlignment.Left)
        Lv_Supplier.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details


        Txt_KdSupplier.Text = ""
        Txt_NmSupplier.Text = ""
        Txt_KdBarang.Text = ""
        Txt_NmBarang.Text = ""

    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Cmb_FilterBy.SelectedIndex = -1 Then
            MessageBox.Show("Fiter harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_FilterBy.Focus() : Exit Sub
        ElseIf Cmb_FlagValidasi.SelectedIndex = -1 Then
            MessageBox.Show("Status Validasi harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_FlagValidasi.Focus() : Exit Sub
        ElseIf Txt_KdSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Supplier harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdSupplier.Focus() : Exit Sub
        ElseIf Txt_NmSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Supplier harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NmSupplier.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        ElseIf Txt_NmBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NmBarang.Focus() : Exit Sub

        End If

        Try
            OpenConn()

            Dim SF As String = ""

            If Cmb_FilterBy.SelectedIndex = 0 Then

                SQL = "select a.kode_perusahaan from View_Laporan_Barang_Masuk a "
                SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.tanggal_masuk between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{View_Laporan_Barang_Masuk.kode_perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {View_Laporan_Barang_Masuk.tanggal_masuk} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{View_Laporan_Barang_Masuk.tanggal_masuk} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then
                    SQL = SQL & "and a.kode_supplier = '" & Txt_KdSupplier.Text & "' "
                    SF = SF & "And {View_Laporan_Barang_Masuk.kode_supplier} = '" & Txt_KdSupplier.Text & "'"
                End If

                If Not Txt_KdBarang.Text = "--- SELURUH ---" Then
                    SQL = SQL & "and a.kode_barang = '" & Txt_KdBarang.Text & "' "
                    SF = SF & "And {View_Laporan_Barang_Masuk.kode_barang} = '" & Txt_KdBarang.Text & "'"
                End If

                If Cmb_FlagValidasi.SelectedIndex = 1 Then
                    SQL = SQL & "and a.status = 'Validasi' "
                    SF = SF & "And {View_Laporan_Barang_Masuk.status} = 'Validasi'"
                ElseIf Cmb_FlagValidasi.SelectedIndex = 2 Then
                    SQL = SQL & "and a.status = 'Belum Di Validasi' "
                    SF = SF & "And {View_Laporan_Barang_Masuk.status} = 'Belum Di Validasi'"
                End If

                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim CrDoc As New Rpt_Emi_Laporan_Barang_Masuk

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

            ElseIf Cmb_FilterBy.SelectedIndex = 1 Then

                SQL = "select kode_perusahaan from View_Laporan_Barang_Masuk_Loading "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{View_Laporan_Barang_Masuk_Loading.kode_perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {View_Laporan_Barang_Masuk_Loading.tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{View_Laporan_Barang_Masuk_Loading.tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then
                    SQL = SQL & "and kode_supplier = '" & Txt_KdSupplier.Text & "' "
                    SF = SF & "And {View_Laporan_Barang_Masuk_Loading.kode_supplier} = '" & Txt_KdSupplier.Text & "'"
                End If

                If Not Txt_KdBarang.Text = "--- SELURUH ---" Then
                    SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text & "' "
                    SF = SF & "And {View_Laporan_Barang_Masuk_Loading.kode_barang} = '" & Txt_KdBarang.Text & "'"
                End If

                If Cmb_Filter_Mobil.SelectedIndex <> 0 Then
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                    If Not Strings.Right(UCase(SF), 6) = "WHERE " Then SF = SF & "AND "

                    SQL = SQL & arrFilterMobil.Item(Cmb_Filter_Mobil.SelectedIndex) & " like '%" & Trim(Txt_ValueFilter.Text) & "%' "
                    SF = SF & arrFilterSFMobil.Item(Cmb_Filter_Mobil.SelectedIndex) & " like '*" & Trim(Txt_ValueFilter.Text) & "*' "
                End If

                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim CrDoc As New Rpt_Emi_Laporan_Barang_Masuk_Loading

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

    Private Sub Txt_KdSupplier_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdSupplier.TextChanged

        If Txt_KdSupplier.Text.Trim.Length = 0 Then
            Me.Size = New Size(608, 333)
            Lv_Supplier.Location = New Point(600, 172)
            Lv_Supplier.Visible = False
            Txt_KdSupplier.Text = ""
            Txt_NmSupplier.Text = ""
        Else
            Me.Size = New Size(608, 421)
            Lv_Supplier.Location = New Point(124, 172)
            Lv_Supplier.Visible = True
        End If

        Try
            OpenConn()

            Lv_Supplier.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_Supplier.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Supplier like '%" & Txt_KdSupplier.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Lv = Lv_Supplier.Items.Add(Dr("Kode_Supplier"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Txt_KdSupplier_Leave(sender As Object, e As EventArgs) Handles Txt_KdSupplier.Leave
        If Txt_KdSupplier.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Supplier.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then

                SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & Txt_KdSupplier.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Txt_KdSupplier.Text = Dr("Kode_Supplier")
                        Txt_NmSupplier.Text = Dr("Nama")
                    Else
                        MessageBox.Show("Supplier tidak ditemukan . . ! !", Judul)
                        Txt_KdSupplier.Text = "" : Txt_NmSupplier.Text = ""
                        Txt_KdSupplier.Focus()

                    End If

                    Me.Size = New Size(610, 333)
                    Lv_Supplier.Location = New Point(600, 172)
                    Lv_Supplier.Visible = False
                End Using

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Txt_KdSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdSupplier.Text.Trim.Length = 0 Then Txt_KdSupplier.Focus()
            Txt_KdSupplier_Leave(Txt_KdSupplier, e)

            Me.Size = New Size(610, 333)
            Lv_Supplier.Location = New Point(600, 172)
            Lv_Supplier.Visible = False

            Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Txt_KdSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdSupplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub Txt_NmSupplier_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmSupplier.TextChanged


        If Txt_NmSupplier.Text.Trim.Length = 0 Then
            Me.Size = New Size(608, 333)
            Lv_Supplier.Location = New Point(600, 172)
            Lv_Supplier.Visible = False
            Txt_KdSupplier.Text = ""
            Txt_NmSupplier.Text = ""
        Else
            Me.Size = New Size(608, 421)
            Lv_Supplier.Location = New Point(124, 172)
            Lv_Supplier.Visible = True
        End If



        Try
            OpenConn()

            Lv_Supplier.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_Supplier.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_NmSupplier.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Lv = Lv_Supplier.Items.Add(Dr("Kode_Supplier"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NmSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmSupplier.KeyPress

        If e.KeyChar = Chr(13) Then
            Txt_KdSupplier_Leave(Txt_NmSupplier, e)
            Me.Size = New Size(610, 333)
            Lv_Supplier.Location = New Point(600, 172)
            Lv_Supplier.Visible = False

            Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Txt_NmSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmSupplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub Lv_Supplier_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Supplier.DoubleClick

        If Lv_Supplier.Items.Count = 0 Or Lv_Supplier.FocusedItem.Index = -1 Then Exit Sub

        Dim KdSupplier As String = Lv_Supplier.FocusedItem.SubItems(0).Text
        Dim NmSupplier As String = Lv_Supplier.FocusedItem.SubItems(1).Text

        Txt_KdSupplier.Text = KdSupplier
        Txt_NmSupplier.Text = NmSupplier

        Me.Size = New Size(608, 333)
        Lv_Supplier.Location = New Point(600, 172)
        Lv_Supplier.Visible = False

        Txt_KdBarang.Focus()


    End Sub


    Private Sub Lv_Supplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Supplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Supplier_DoubleClick(Lv_Supplier, e)
        End If
    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged


        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(608, 333)
            Lv_Barang.Location = New Point(600, 232)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
        Else
            Me.Size = New Size(608, 480)
            Lv_Barang.Location = New Point(124, 232)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select distinct Kode_Barang, Nama from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang like '%" & Txt_KdBarang.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
        If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then

                SQL = "select distinct Kode_Barang, Nama from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Txt_KdBarang.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("kode_barang")
                        Txt_NmBarang.Text = Dr("nama")
                    Else
                        MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul)
                        Txt_KdBarang.Text = "" : Txt_NmBarang.Text = ""
                        Txt_NmBarang.Focus()
                    End If

                    Me.Size = New Size(610, 333)
                    Lv_Barang.Location = New Point(600, 232)
                    Lv_Barang.Visible = False


                End Using
            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_NmBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)
            Me.Size = New Size(610, 333)
            Lv_Barang.Location = New Point(600, 232)
            Lv_Barang.Visible = False
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged


        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(608, 333)
            Lv_Barang.Location = New Point(600, 232)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
        Else
            Me.Size = New Size(608, 480)
            Lv_Barang.Location = New Point(124, 232)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select distinct Kode_Barang, Nama from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_NmBarang.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress

        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)
            Me.Size = New Size(610, 333)
            Lv_Barang.Location = New Point(600, 232)
            Lv_Barang.Visible = False

            'BtnCetak.Focus()
        End If
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick

        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmBarang

        Me.Size = New Size(608, 333)
        Lv_Barang.Location = New Point(600, 232)
        Lv_Barang.Visible = False

        If Cmb_FilterBy.SelectedIndex = 0 Then
            Cmb_FlagValidasi.Focus()
        Else
            Cmb_Filter_Mobil.Focus()
        End If
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub




    Private Sub Cmb_FilterBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_FilterBy.SelectedIndexChanged

        If Cmb_FilterBy.SelectedIndex = 1 Then
            Cmb_Filter_Mobil.Visible = True
            Txt_ValueFilter.Visible = True : Txt_ValueFilter.Text = ""
            Label_Status.Text = "Filter"
        Else
            Cmb_Filter_Mobil.Visible = False
            Txt_ValueFilter.Visible = False : Txt_ValueFilter.Text = ""
            Label_Status.Text = "Status Validasi"
        End If

    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_FilterBy.Focus()
    End Sub

    Private Sub Cmb_FilterBy_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_FilterBy.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_FilterBy_SelectedIndexChanged(Cmb_FilterBy, e)
            Txt_KdSupplier.Focus()
        End If
    End Sub

    Private Sub Cmb_FlagValidasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_FlagValidasi.KeyPress
        If Cmb_FlagValidasi.Items.Count <> -1 Then
            Cmb_FlagValidasi.SelectedIndex = 0
            BtnCetak.Focus()
        End If
    End Sub

    Private Sub Cmb_Filter_Mobil_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter_Mobil.KeyPress
        If Cmb_Filter_Mobil.Items.Count <> -1 Then
            Cmb_Filter_Mobil.SelectedIndex = 0
            Txt_ValueFilter.Focus()
        End If
    End Sub

    Private Sub Txt_ValueFilter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ValueFilter.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub



    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub




End Class
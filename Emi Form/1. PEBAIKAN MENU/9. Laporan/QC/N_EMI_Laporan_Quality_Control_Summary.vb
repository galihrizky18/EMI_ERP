Public Class N_EMI_Laporan_Quality_Control_Summary

    Dim JudulForm As String = "Laporan Quality Control"
    Dim arrStep, arrParamLain, arrParamLainSF As New ArrayList

    Private Sub N_EMI_Laporan_Quality_Control_Summary_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        kosong()
    End Sub

    Private Sub kosong()

        Tgl1.Value = Date.Now : Tgl2.Value = Date.Now

        Txt_NoLoading.Text = OpsiSeluruh
        Txt_KdKategori.Text = "000" : Txt_Kategori.Text = OpsiSeluruh
        Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh

        Cmb_ParamLain.Items.Clear() : arrParamLain.Clear() : arrParamLainSF.Clear()
        Cmb_ParamLain.Items.Add(OpsiSeluruh) : arrParamLain.Add(OpsiSeluruh) : arrParamLainSF.Add(OpsiSeluruh)
        Cmb_ParamLain.Items.Add("User ID") : arrParamLain.Add("UserId") : arrParamLainSF.Add("{N_EMI_Laporan_Quality_Control_Summary_View.UserId}")
        Cmb_ParamLain.SelectedIndex = 0

        Lv_Loading.Columns.Clear()
        Lv_Loading.Columns.Add("No Loading", 120, HorizontalAlignment.Left)
        Lv_Loading.Columns.Add("Supplier", 150, HorizontalAlignment.Left)
        Lv_Loading.Columns.Add("Mobil", 180, HorizontalAlignment.Left)
        Lv_Loading.View = View.Details

        Lv_Pengujian.Columns.Clear()
        Lv_Pengujian.Columns.Add("Id Formula", 80, HorizontalAlignment.Left)
        Lv_Pengujian.Columns.Add("Kode Uji", 120, HorizontalAlignment.Left)
        Lv_Pengujian.Columns.Add("Keterangan", 180, HorizontalAlignment.Left)
        Lv_Pengujian.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode_ Barang", 130, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details

        Lv_Loading.Visible = False
        Lv_Pengujian.Visible = False
        Lv_Barang.Visible = False

        Txt_ParamLain.Enabled = False

        Me.Size = New Size(645, 310)

    End Sub

    '===================================================================================================================================================================
    '=     HANDLE TEXT CHANGED
    '===================================================================================================================================================================
    Private Sub Txt_NoLoading_TextChanged(sender As Object, e As EventArgs) Handles Txt_NoLoading.TextChanged
        If Txt_NoLoading.Text.Trim.Length = 0 Then
            Me.Size = New Size(645, 310)
            Lv_Loading.Location = New Point(650, 128)
            Lv_Loading.Visible = False
            Txt_NoLoading.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(645, 380)
            Lv_Loading.Visible = True
            Lv_Loading.Location = New Point(115, 128)
        End If

        Try
            OpenConn()

            Lv_Loading.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Loading.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select a.No_Faktur, b.nama as Supplier, (a.No_SJ + '-' + a.No_Plat + '-' + a.Driver) as Mobil "
            SQL = SQL & "from EMI_Pembelian_Loading a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur like '%" & Txt_NoLoading.Text & "%' "
            SQL = SQL & "order by a.No_Faktur "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Loading.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Supplier"))
                    Lv.SubItems.Add(Dr("Mobil"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Txt_KdKategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdKategori.TextChanged
        If Txt_KdKategori.Text.Trim.Length = 0 Then
            Me.Size = New Size(645, 310)
            Lv_Pengujian.Location = New Point(650, 154)
            Lv_Pengujian.Visible = False
            Txt_KdKategori.Text = ""
            Txt_Kategori.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(645, 410)
            Lv_Pengujian.Visible = True
            Lv_Pengujian.Location = New Point(115, 154)
        End If

        Try
            OpenConn()

            Lv_Pengujian.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pengujian.Items.Add("000")
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select a.Id_QC_Formula, a.Kode_Uji, a.Keterangan "
            SQL = SQL & "from EMI_Quality_Control a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_QC_Formula like '%" & Txt_KdKategori.Text & "%' "
            SQL = SQL & "order by Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Pengujian.Items.Add(Dr("Id_QC_Formula"))
                    Lv.SubItems.Add(Dr("Kode_Uji"))
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

    Private Sub Txt_Kategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kategori.TextChanged
        If Txt_Kategori.Text.Trim.Length = 0 Then
            Me.Size = New Size(645, 310)
            Lv_Pengujian.Location = New Point(650, 154)
            Lv_Pengujian.Visible = False
            Txt_KdKategori.Text = ""
            Txt_Kategori.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(645, 410)
            Lv_Pengujian.Visible = True
            Lv_Pengujian.Location = New Point(115, 154)
        End If

        Try
            OpenConn()

            Lv_Pengujian.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pengujian.Items.Add("000")
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select a.Id_QC_Formula, a.Kode_Uji, a.Keterangan "
            SQL = SQL & "from EMI_Quality_Control a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Keterangan like '%" & Txt_Kategori.Text & "%' "
            SQL = SQL & "order by Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Pengujian.Items.Add(Dr("Id_QC_Formula"))
                    Lv.SubItems.Add(Dr("Kode_Uji"))
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

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(645, 310)
            Lv_Barang.Location = New Point(650, 180)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(645, 435)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(115, 180)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

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

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(645, 310)
            Lv_Barang.Location = New Point(650, 180)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(645, 435)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(115, 180)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

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

    '===================================================================================================================================================================
    '=     HANDLE LEAVE
    '===================================================================================================================================================================
    Private Sub Txt_NoLoading_Leave(sender As Object, e As EventArgs) Handles Txt_NoLoading.Leave
        If Txt_NoLoading.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Loading.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_NoLoading.Text = OpsiSeluruh Then

                SQL = "select a.No_Faktur, b.nama as Supplier, (a.No_SJ + '-' + a.No_Plat + '-' + a.Driver) as Mobil "
                SQL = SQL & "from EMI_Pembelian_Loading a, Suppliers b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
                SQL = SQL & "and status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & Txt_NoLoading.Text & "' "
                SQL = SQL & "order by a.No_Faktur "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_NoLoading.Text = Dr("No_Faktur")
                        Txt_KdKategori.Focus()
                    Else
                        MessageBox.Show("No Loading tidak ditemukan . . ! !", JudulForm)
                        Txt_NoLoading.Text = ""
                        Txt_NoLoading.Focus()
                    End If

                    Me.Size = New Size(645, 310)
                    Lv_Loading.Location = New Point(650, 128)
                    Lv_Loading.Visible = False
                End Using
            Else
                Txt_KdKategori.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdKategori_Leave(sender As Object, e As EventArgs) Handles Txt_KdKategori.Leave
        If Txt_KdKategori.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Pengujian.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdKategori.Text = "000" Then

                SQL = "select a.Id_QC_Formula, a.Kode_Uji, a.Keterangan "
                SQL = SQL & "from EMI_Quality_Control a "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Id_QC_Formula = '" & Txt_KdKategori.Text & "' "
                SQL = SQL & "order by Keterangan "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdKategori.Text = Dr("Id_QC_Formula")
                        Txt_Kategori.Text = Dr("Keterangan")
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("Data Pengujian tidak ditemukan . . ! !", JudulForm)
                        Txt_KdKategori.Text = ""
                        Txt_Kategori.Text = ""
                        Txt_KdKategori.Focus()
                    End If

                    Me.Size = New Size(645, 310)
                    Lv_Pengujian.Location = New Point(650, 154)
                    Lv_Pengujian.Visible = False
                End Using
            Else
                Txt_KdBarang.Focus()
            End If

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

            If Not Txt_KdBarang.Text = OpsiSeluruh Then

                SQL = "select distinct Kode_Barang, Nama from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Txt_KdBarang.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NmBarang.Text = Dr("Nama")
                        Cmb_ParamLain.DroppedDown = True
                        Cmb_ParamLain.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", JudulForm)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Me.Size = New Size(645, 310)
                    Lv_Barang.Location = New Point(650, 180)
                    Lv_Barang.Visible = False
                End Using
            Else
                Cmb_ParamLain.DroppedDown = True
                Cmb_ParamLain.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    '===================================================================================================================================================================
    '=     HANDLE LISTVIEW
    '===================================================================================================================================================================
    Private Sub Lv_Loading_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Loading.DoubleClick
        If Lv_Loading.Items.Count = 0 Or Lv_Loading.FocusedItem.Index = -1 Then Exit Sub

        Dim NoFaktur As String = Lv_Loading.FocusedItem.SubItems(0).Text

        Txt_NoLoading.Text = NoFaktur

        Me.Size = New Size(645, 310)
        Lv_Loading.Location = New Point(650, 154)
        Lv_Loading.Visible = False
        Txt_KdKategori.Focus()
    End Sub

    Private Sub Lv_Loading_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Loading.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Loading_DoubleClick(Lv_Loading, e)
        End If
    End Sub

    Private Sub Lv_Pengujian_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Pengujian.DoubleClick
        If Lv_Pengujian.Items.Count = 0 Or Lv_Pengujian.FocusedItem.Index = -1 Then Exit Sub

        Dim Id As String = Lv_Pengujian.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = Lv_Pengujian.FocusedItem.SubItems(2).Text

        Txt_KdKategori.Text = Id
        Txt_Kategori.Text = Keterangan

        Me.Size = New Size(645, 310)
        Lv_Pengujian.Location = New Point(650, 154)
        Lv_Pengujian.Visible = False
        Txt_KdBarang.Focus()
    End Sub

    Private Sub Lv_Pengujian_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Pengujian.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Pengujian_DoubleClick(Lv_Pengujian, e)
        End If
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmBarang

        Me.Size = New Size(645, 310)
        Lv_Barang.Location = New Point(650, 180)
        Lv_Barang.Visible = False
        Cmb_ParamLain.DroppedDown = True
        Cmb_ParamLain.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    '===================================================================================================================================================================
    '=     HANDLE BUTTON
    '===================================================================================================================================================================
    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_NoLoading.Text.Trim.Length = 0 Then
            MessageBox.Show("No Loading harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoLoading.Focus() : Exit Sub
        ElseIf Txt_KdKategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Id Pengujian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdKategori.Focus() : Exit Sub
        ElseIf Txt_Kategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Pengujian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kategori.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        ElseIf Txt_NmBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NmBarang.Focus() : Exit Sub
        End If

        If Cmb_ParamLain.SelectedIndex <> 0 Then
            If Txt_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Parameter lain harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_ParamLain.Focus() : Exit Sub
            End If
        End If


        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select kode_perusahaan from N_EMI_Laporan_Quality_Control_Summary_View "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{N_EMI_Laporan_Quality_Control_Summary_View.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_Laporan_Quality_Control_Summary_View.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{N_EMI_Laporan_Quality_Control_Summary_View.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_NoLoading.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and No_Fak_Loading_Barang = '" & Txt_NoLoading.Text & "' "
                SF = SF & "And {N_EMI_Laporan_Quality_Control_Summary_View.No_Fak_Loading_Barang} = '" & Txt_NoLoading.Text & "'"
            End If

            If Not Txt_KdKategori.Text.ToUpper = "000" Then
                SQL = SQL & "and Id_Quality_Control = " & Txt_KdKategori.Text & " "
                SF = SF & "And {N_EMI_Laporan_Quality_Control_Summary_View.Id_Quality_Control} = " & Txt_KdKategori.Text & " "
            End If

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {N_EMI_Laporan_Quality_Control_Summary_View.Kode_Barang} = '" & Txt_KdBarang.Text & "'"
            End If

            If Not Cmb_ParamLain.SelectedIndex = 0 Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                If Not Strings.Right(UCase(SF), 6) = "WHERE " Then SF = SF & "AND "

                SQL = SQL & arrParamLain.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
                SF = SF & arrParamLainSF.Item(Cmb_ParamLain.SelectedIndex) & " like '*" & Trim(Txt_ParamLain.Text) & "*' "
            End If

            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New N_EMI_CR_Laporan_Quality_Control_Summary

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
                        MessageBox.Show("Data Quality Control Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    End If
                End With
            End Using

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








    '===================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '===================================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NoLoading.Focus()
    End Sub

    Private Sub Cmb_Step_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Txt_NoLoading.Focus()
    End Sub

    Private Sub Txt_NoLoading_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoLoading.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_NoLoading.Text.Trim.Length = 0 Then Txt_NoLoading.Focus()
            Txt_NoLoading_Leave(Txt_NoLoading, e)

            Me.Size = New Size(645, 310)
            Lv_Loading.Location = New Point(650, 128)
            Lv_Loading.Visible = False

        End If
    End Sub

    Private Sub Txt_NoLoading_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NoLoading.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Loading.Focus()
    End Sub

    Private Sub Txt_KdKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdKategori.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdKategori.Text.Trim.Length = 0 Then
                Txt_KdKategori.Focus()
            End If

            Txt_KdKategori_Leave(Txt_KdKategori, e)

            Me.Size = New Size(645, 310)
            Lv_Pengujian.Location = New Point(650, 154)
            Lv_Pengujian.Visible = False
        End If
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            Exit Sub
        End If

    End Sub

    Private Sub Txt_KdKategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdKategori.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pengujian.Focus()
    End Sub

    Private Sub Txt_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kategori.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdKategori_Leave(Txt_Kategori, e)

            Me.Size = New Size(645, 310)
            Lv_Pengujian.Location = New Point(650, 154)
            Lv_Pengujian.Visible = False

            Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Txt_Kategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kategori.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pengujian.Focus()
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Me.Size = New Size(645, 310)
            Lv_Barang.Location = New Point(650, 180)
            Lv_Barang.Visible = False

        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Me.Size = New Size(645, 310)
            Lv_Barang.Location = New Point(650, 180)
            Lv_Barang.Visible = False

            Cmb_ParamLain.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub



    Private Sub Cmb_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_ParamLain.SelectedIndex = 0 Then
                BtnCetak.Focus()
            Else
                Txt_ParamLain.Focus()
            End If
        End If
    End Sub


    Private Sub Txt_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub Cmb_ParamLain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_ParamLain.SelectedIndexChanged
        If Cmb_ParamLain.SelectedIndex = 0 Then
            Txt_ParamLain.Text = ""
            Txt_ParamLain.Enabled = False
        Else
            Txt_ParamLain.Text = ""
            Txt_ParamLain.Enabled = True
        End If
    End Sub

End Class
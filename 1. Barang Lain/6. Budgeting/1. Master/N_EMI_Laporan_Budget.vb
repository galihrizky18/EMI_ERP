Public Class N_EMI_Laporan_Budget
    Dim arrDepartment, arrBinding As New ArrayList
    Dim switchAutoComplete As Boolean = False
    Private Sub N_EMI_Laporan_Budget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Lv_Jenis.Columns.Clear()
        Lv_Jenis.Columns.Add("Id_Kategori_jenis", 0, HorizontalAlignment.Left)
        Lv_Jenis.Columns.Add("Keterangan", 330, HorizontalAlignment.Left)
        Lv_Jenis.View = View.Details
        Lv_Jenis.FullRowSelect = True
        Lv_Jenis.GridLines = True
        Lv_Jenis.HideSelection = False

        Lv_Sub.Columns.Clear()
        Lv_Sub.Columns.Add("Id_Sub_Kategori_Jenis_1", 0, HorizontalAlignment.Left)
        Lv_Sub.Columns.Add("Keterangan", 150, HorizontalAlignment.Left)
        Lv_Sub.View = View.Details
        Lv_Sub.FullRowSelect = True
        Lv_Sub.GridLines = True
        Lv_Sub.HideSelection = False
        Try
            OpenConn()

            Cmb_Department.Items.Clear()
            arrDepartment.Clear()

            Cmb_Department.Items.Add(OpsiSeluruh)
            arrDepartment.Add(OpsiSeluruh)

            SQL = "SELECT Kode_Kategori_Gudang, Keterangan "
            SQL &= "FROM N_EMI_Master_Kategori_Gudang_Barang_Lain "
            SQL &= $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= "ORDER BY Kode_Kategori_Gudang "

            Using Dr = OpenTrans(SQL)
                While Dr.Read
                    Cmb_Department.Items.Add(Dr("Keterangan").ToString)
                    arrDepartment.Add(Dr("Kode_Kategori_Gudang").ToString)
                End While
            End Using

            Cmb_Department.SelectedIndex = 0

            Cmb_Binding.Items.Clear()
            arrBinding.Clear()

            Cmb_Binding.Items.Add(OpsiSeluruh)
            arrBinding.Add(OpsiSeluruh)

            SQL = "SELECT Kode_Binding, Keterangan "
            SQL &= "FROM N_EMI_Binding_Department "
            SQL &= $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= "ORDER BY Kode_Binding "

            Using Dr = OpenTrans(SQL)
                While Dr.Read
                    Cmb_Binding.Items.Add(Dr("Kode_Binding").ToString)
                    arrBinding.Add(Dr("Keterangan").ToString)
                End While
            End Using

            Cmb_Binding.SelectedIndex = 0
            CmbJenis2.Items.Clear()
            CmbJenis2.Items.Add(OpsiSeluruh)
            CmbJenis2.Items.Add("Alokasi Budget")
            CmbJenis2.Items.Add("PR Department")
            CmbJenis2.Items.Add("Transfer Stock")
            CmbJenis2.SelectedIndex = 0

            Cmb_Tahun.Items.Clear()
            Dim tahunMulai As Integer = 2024
            Dim tahunAkhir As Integer = Now.Year + 1
            For i As Integer = tahunMulai To tahunAkhir
                Cmb_Tahun.Items.Add(i.ToString())
            Next
            Cmb_Tahun.Text = Now.Year.ToString()

            Dim daftarBulan As String() = {"-- SELURUH --", "Januari", "Februari", "Maret", "April", "Mei", "Juni",
                               "Juli", "Agustus", "September", "Oktober", "November", "Desember"}

            ' Reset dan isi BlnAwal
            BlnAwal.Items.Clear()
            BlnAwal.Items.AddRange(daftarBulan)
            BlnAwal.SelectedIndex = 0 ' Default ke "-- Pilih Bulan --"

            ' Reset dan isi BlnAkhir
            BlnAkhir.Items.Clear()
            BlnAkhir.Items.AddRange(daftarBulan)
            BlnAkhir.SelectedIndex = 0 '

            CloseTrans()
            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
    End Sub
    Private Sub N_EMI_Laporan_Budget_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub Kosong()
        Tgl1.Value = Date.Today
        Tgl2.Value = Date.Today

        Cmb_Department.SelectedIndex = 0
        Cmb_Binding.SelectedIndex = 0

        switchAutoComplete = True
        Txt_Jenis.Text = OpsiSeluruh
        Txt_Jenis.Tag = Nothing
        Txt_Sub.Text = OpsiSeluruh
        Txt_Sub.Tag = Nothing
        switchAutoComplete = False


        Tgl1.Focus()
    End Sub
    Private Sub Chk_Periode_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Periode.CheckedChanged
        Tgl1.Enabled = Chk_Periode.Checked
        Tgl2.Enabled = Chk_Periode.Checked

        If Chk_Periode.Checked Then
            Tgl1.Focus()
        End If
    End Sub
    Private Sub Txt_Jenis_TextChanged(sender As Object, e As EventArgs) Handles Txt_Jenis.TextChanged
        If switchAutoComplete Then Exit Sub

        'ukuran Lv_Jenis sebelum mengetik
        If Txt_Jenis.Text.Trim.Length = 0 Then
            Me.Size = New Size(603, 283)
            Lv_Jenis.Visible = False
            Lv_Jenis.Location = New Point(172, 170)
            Txt_Jenis.Text = ""
            Exit Sub
        Else
            'Ukuran Lv_Jenis setelah mengetik
            Me.Size = New Size(603, 323)
            Lv_Jenis.Visible = True
            Lv_Jenis.Location = New Point(158, 170)

            If Lv_Jenis.Items.Count > 2 Then

                Lv_Jenis.Items(2).Selected = True
            End If
        End If
        Try
            OpenConn()
            Lv_Jenis.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Jenis.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "SELECT Id_Kategori_jenis, Keterangan " &
              "FROM N_EMI_Master_Kategori_Jenis " &
              $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
              $"AND (Keterangan LIKE '%{Txt_Jenis.Text.Trim}%' " &
              $"OR Id_Kategori_jenis LIKE '%{Txt_Jenis.Text.Trim}%') " &
              "ORDER BY Keterangan "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Do
                        Dim Item As New ListViewItem(Dr("Id_Kategori_jenis").ToString)
                        Item.SubItems.Add(Dr("Keterangan").ToString)
                        Lv_Jenis.Items.Add(Item)
                    Loop While Dr.Read
                Else
                    Dr.Close()
                    CloseConn()
                    Exit Sub
                End If
            End Using
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_Jenis_Leave(sender As Object, e As EventArgs) Handles Txt_Jenis.Leave
        If Txt_Jenis.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Jenis.Focused = True Then Exit Sub
        If Txt_Jenis.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
            Exit Sub
        End If
        If Txt_Jenis.Tag IsNot Nothing AndAlso Txt_Jenis.Tag.ToString.Trim.Length > 0 Then Exit Sub
        Try
            OpenConn()

            If Not Txt_Jenis.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

                SQL = "SELECT Id_Kategori_jenis, Keterangan " &
              "FROM N_EMI_Master_Kategori_Jenis " &
              $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
              $"AND (Keterangan LIKE '%{Txt_Jenis.Text.Trim}%' " &
              $"OR Id_Kategori_jenis LIKE '%{Txt_Jenis.Text.Trim}%') "

                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Do
                            Dim Item As New ListViewItem(Dr("Id_Kategori_jenis").ToString)
                            Item.SubItems.Add(Dr("Keterangan").ToString)
                            Txt_Sub.Focus()
                        Loop While Dr.Read
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Group Jenis tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Jenis.Text = ""
                        Txt_Jenis.Tag = Nothing
                        Txt_Jenis.Focus()
                        Exit Sub
                    End If
                End Using
            Else
                Txt_Sub.Focus()
            End If
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Lv_Jenis_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Jenis.DoubleClick
        If Lv_Jenis.FocusedItem Is Nothing OrElse Lv_Jenis.FocusedItem.Index = -1 Then Exit Sub

        Dim idJenis As String = Lv_Jenis.FocusedItem.Text
        Dim keterangan As String = Lv_Jenis.FocusedItem.SubItems(1).Text

        switchAutoComplete = True
        Txt_Jenis.Text = keterangan
        Txt_Jenis.Tag = idJenis
        switchAutoComplete = False

        'Lv_jenis Setelah doubleclik
        Me.Size = New Size(603, 283)
        Lv_Jenis.Visible = False
        Lv_Jenis.Location = New Point(172, 196)

        Txt_Sub.Focus()
    End Sub
    Private Sub Txt_Sub_TextChanged(sender As Object, e As EventArgs) Handles Txt_Sub.TextChanged
        If switchAutoComplete Then Exit Sub

        If Txt_Sub.Text.Trim.Length = 0 Then
            Me.Size = New Size(603, 283)
            Lv_Sub.Visible = False
            Lv_Sub.Location = New Point(635, 188)
            Txt_Sub.Text = ""
            Txt_Sub.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(603, 351)
            Lv_Sub.Visible = True
            Lv_Sub.Location = New Point(158, 196)
        End If

        Try
            OpenConn()

            Lv_Sub.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Sub.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "SELECT Id_Sub_Kategori_Jenis_1, Keterangan " &
              "FROM N_EMI_Master_Sub_Kategori_Jenis_1 " &
              $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
              $"AND (Keterangan LIKE '%{Txt_Sub.Text.Trim}%' " &
              $"OR Id_Sub_Kategori_Jenis_1 LIKE '%{Txt_Sub.Text.Trim}%') " &
              "ORDER BY Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Item As New ListViewItem(Dr("Id_Sub_Kategori_Jenis_1").ToString)
                    Item.SubItems.Add(Dr("Keterangan").ToString)
                    Lv_Sub.Items.Add(Item)
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_Sub_Leave(sender As Object, e As EventArgs) Handles Txt_Sub.Leave
        If Txt_Sub.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Sub.Focused = True Then Exit Sub
        If Txt_Sub.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
            Exit Sub
        End If
        If Txt_Sub.Tag IsNot Nothing AndAlso Txt_Sub.Tag.ToString.Trim.Length > 0 Then Exit Sub
        Try
            OpenConn()

            If Not Txt_Sub.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

                SQL = "SELECT Id_Sub_Kategori_Jenis, Keterangan " &
              "FROM N_EMI_Master_Sub_Kategori_Jenis_1 " &
              $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
              $"AND (Keterangan LIKE '%{Txt_Sub.Text.Trim}%' " &
              $"OR Id_Sub_Kategori_Jenis LIKE '%{Txt_Sub.Text.Trim}%') "

                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Do
                            Dim Item As New ListViewItem(Dr("Id_Sub_Kategori_jenis").ToString)
                            Item.SubItems.Add(Dr("Keterangan").ToString)
                            Txt_Sub.Focus()
                        Loop While Dr.Read
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Group Jenis tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Sub.Text = ""
                        Txt_Sub.Tag = Nothing
                        Txt_Sub.Focus()
                        Exit Sub
                    End If
                End Using
            Else
                CmbJenis2.Focus()
            End If
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Lv_Sub_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Sub.DoubleClick
        If Lv_Sub.FocusedItem Is Nothing OrElse Lv_Sub.FocusedItem.Index = -1 Then Exit Sub

        Dim idSub As String = Lv_Sub.FocusedItem.Text
        Dim keterangan As String = Lv_Sub.FocusedItem.SubItems(1).Text

        switchAutoComplete = True
        Txt_Sub.Text = keterangan
        Txt_Sub.Tag = idSub
        switchAutoComplete = False

        'Lv_jenis Setelah doubleclik
        Me.Size = New Size(603, 283)
        Lv_Sub.Visible = False
        Lv_Sub.Location = New Point(172, 222)

        BtnCetak.Focus()
    End Sub
    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Chk_Periode.Checked Then
            If Tgl1.Value > Tgl2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
                Tgl1.Focus() : Exit Sub
            End If
        End If
        If Txt_Jenis.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Jenis harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jenis.Focus() : Exit Sub
        End If
        If Txt_Sub.Text.Trim.Length = 0 Then
            MessageBox.Show("Sub Kategori 1 harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Sub.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim SF As String = ""

            SQL = "SELECT * FROM N_EMI_View_Laporan_Budget_Planning_Log "
            SQL &= "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "

            SF = "{N_EMI_View_Laporan_Budget_Planning_Log.Kode_Perusahaan} = '" & KodePerusahaan & "' "

            If Chk_Periode.Checked Then
                SQL &= "AND Tanggal BETWEEN '" & Format(Tgl1.Value, "MM/dd/yyyy") & "' AND '" & Format(Tgl2.Value, "MM/dd/yyyy") & "' "
                SF &= "AND {N_EMI_View_Laporan_Budget_Planning_Log.Tanggal} >= #" & Format(Tgl1.Value, "MM/dd/yyyy") & "# "
                SF &= "AND {N_EMI_View_Laporan_Budget_Planning_Log.Tanggal} <= #" & Format(Tgl2.Value, "MM/dd/yyyy") & "# "
            End If

            If Cmb_Tahun.SelectedIndex <> -1 Then
                SQL &= $"AND Tahun = '{Cmb_Tahun.Text.Trim}' "
                SF &= "And {N_EMI_View_Laporan_Budget_Planning_Log.Tahun} = " & Cmb_Tahun.Text.Trim & " "
            End If

            If BlnAwal.SelectedIndex > 0 And BlnAkhir.SelectedIndex > 0 Then
                If BlnAwal.SelectedIndex > BlnAkhir.SelectedIndex Then
                    MessageBox.Show("Bulan awal tidak boleh lebih besar dari bulan akhir!", "Perhatian")
                    Exit Sub
                End If

                SQL &= $"AND Bulan BETWEEN {BlnAwal.SelectedIndex} AND {BlnAkhir.SelectedIndex} "
                SF &= $"And {{N_EMI_View_Laporan_Budget_Planning_Log.Bulan}} >= {BlnAwal.SelectedIndex} "
                SF &= $"And {{N_EMI_View_Laporan_Budget_Planning_Log.Bulan}} <= {BlnAkhir.SelectedIndex} "

            ElseIf BlnAwal.SelectedIndex > 0 Or BlnAkhir.SelectedIndex > 0 Then
                MessageBox.Show("Silakan pilih kedua bulan (Awal dan Akhir) atau pilih 'Pilih Bulan' untuk menampilkan semua.", "Perhatian")
                Exit Sub
            End If
            If Cmb_Department.SelectedIndex <> 0 Then
                SQL &= $"AND Kode_kategori_Gudang = '{Cmb_Department.Text}' "
                SF &= "And {N_EMI_View_Laporan_Budget_Planning_Log.Kode_kategori_Gudang} = '" & Cmb_Department.Text & "' "
            End If
            If Cmb_Binding.SelectedIndex <> 0 Then
                SQL &= $"AND Kode_Binding = '{Cmb_Binding.Text}' "
                SF &= "And {N_EMI_View_Laporan_Budget_Planning_Log.Kode_Binding} = '" & Cmb_Binding.Text & "' "
            End If
            If CmbJenis2.SelectedIndex <> 0 Then
                SQL &= $"AND Jenis = '{CmbJenis2.Text}' "
                SF &= "And {N_EMI_View_Laporan_Budget_Planning_Log.Jenis} = '" & CmbJenis2.Text & "' "
            End If
            If Not Txt_Jenis.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                Dim idJenis As String = If(Txt_Jenis.Tag IsNot Nothing, Txt_Jenis.Tag.ToString.Trim, "")
                If idJenis.Length > 0 Then
                    SQL &= "AND Id_Kategori_Jenis = " & idJenis & " "
                    SF &= "And {N_EMI_View_Laporan_Budget_Planning_Log.Id_Kategori_Jenis} = " & idJenis & " "
                End If
            End If
            If Not Txt_Sub.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                Dim idSub As String = If(Txt_Sub.Tag IsNot Nothing, Txt_Sub.Tag.ToString.Trim, "")
                If idSub.Length > 0 Then
                    SQL &= "AND Id_Sub_Kategori_Jenis_1 = " & idSub & " "
                    SF &= "And {N_EMI_View_Laporan_Budget_Planning_Log.Id_Sub_Kategori_Jenis_1} = " & idSub & " "
                End If
            End If


            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New N_EMI_CR_Laporan_Budget

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan Budget"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With
                    Else

                        CloseConn()
                        MessageBox.Show("Budget Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    End If
                End With
            End Using
            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then
            Tgl2.Focus()
        End If
    End Sub
    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Tahun.Focus()
            Cmb_Tahun.DroppedDown = True
        End If
    End Sub
    Private Sub Cmb_Tahun_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tahun.KeyPress
        If e.KeyChar = Chr(13) Then
            BlnAwal.Focus()
            BlnAwal.DroppedDown = True
        End If
    End Sub

    Private Sub BlnAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BlnAwal.KeyPress
        If e.KeyChar = Chr(13) Then
            BlnAkhir.Focus()
            BlnAkhir.DroppedDown = True
        End If
    End Sub

    Private Sub BlnAkhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BlnAkhir.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Binding.Focus()
            Cmb_Binding.DroppedDown = True
        End If
    End Sub
    Private Sub Cmb_Binding_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Binding.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Jenis.Focus()
        End If
    End Sub
    'Private Sub Cmb_Department_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Department.KeyPress
    '    If e.KeyChar = Chr(13) Then
    '        Txt_Jenis.Focus()
    '    End If
    'End Sub
    Private Sub Txt_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Jenis.KeyDown
        If e.KeyCode = Keys.Down Then
            e.Handled = True
            Lv_Jenis.Focus()
        End If
    End Sub
    Private Sub Txt_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Jenis.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            Txt_Sub.Focus()
        End If
    End Sub
    Private Sub Lv_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Jenis.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Lv_Jenis_DoubleClick(sender, e)
        End If
    End Sub
    Private Sub Txt_Sub_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Sub.KeyDown
        If e.KeyCode = Keys.Down Then
            e.Handled = True
            Lv_Sub.Focus()
        End If
    End Sub
    Private Sub Txt_Sub_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Sub.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            BtnCetak.Focus()
            Exit Sub
        End If
    End Sub
    Private Sub Lv_Sub_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Sub.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Lv_Sub_DoubleClick(sender, e)
        End If
    End Sub
    Private Sub CmbJenis2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbJenis2.KeyPress
        If e.KeyChar = Chr(13) Then
            BtnCetak.Focus()
        End If
    End Sub
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub
End Class
Public Class Laporan_Penawaran

    Dim JudulForm As String = "Laporan Penawaran"

    Dim ArrRelease, arrPeriode, arrStatusPenawaran As New ArrayList

    Private Sub Laporan_Penawaran_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Cmb_Periode.Focus()
    End Sub

    Private Sub Laporan_Penawaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Supplier.Columns.Clear()
        Lv_Supplier.Columns.Add("Kode Supplier", 150, HorizontalAlignment.Left)
        Lv_Supplier.Columns.Add("Nama Supplier", 400, HorizontalAlignment.Left)
        Lv_Supplier.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 400, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details


        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
        Txt_KdSupplier.Text = "" : Txt_NmSupplier.Text = ""

        Cmb_Periode.Items.Clear() : arrPeriode.Clear()
        Cmb_Periode.Items.Add("Periode Awal") : arrPeriode.Add("Periode_Awal")
        Cmb_Periode.Items.Add("Periode Akhir") : arrPeriode.Add("Periode_Akhir")
        Cmb_Periode.SelectedIndex = 0

        Cmb_Release.Items.Clear() : ArrRelease.Clear()
        Cmb_Release.Items.Add("--- SELURUH ---") : ArrRelease.Add("--- SELURUH ---")
        Cmb_Release.Items.Add("Submitted") : ArrRelease.Add("SUBMITTED")
        Cmb_Release.Items.Add("Unsubmitted") : ArrRelease.Add("UNSUBMITTED")
        Cmb_Release.SelectedIndex = 0

        Cmb_StatusPenawaran.Items.Clear() : arrStatusPenawaran.Clear()
        Cmb_StatusPenawaran.Items.Add("--- SELURUH ---") : arrStatusPenawaran.Add("--- SELURUH ---")
        Cmb_StatusPenawaran.Items.Add("Aktif") : arrStatusPenawaran.Add("Aktif")
        Cmb_StatusPenawaran.Items.Add("Belum Aktif") : arrStatusPenawaran.Add("Belum Aktif")
        Cmb_StatusPenawaran.Items.Add("Tidak Aktif") : arrStatusPenawaran.Add("Tidak Aktif")
        Cmb_StatusPenawaran.SelectedIndex = 0

        Chk_Periode.Checked = False
        Cmb_Periode.Enabled = False
        Tgl1.Enabled = False
        Tgl2.Enabled = False

        Tgl1.Value = Date.Now : Tgl2.Value = Date.Now

        Txt_KdSupplier.Text = "--- SELURUH ---"
        Txt_NmSupplier.Text = "--- SELURUH ---"
        Txt_KdBarang.Text = "--- SELURUH ---"
        Txt_NmBarang.Text = "--- SELURUH ---"

        Lv_Supplier.Visible = False
        Lv_Barang.Visible = False
        Lv_Supplier.Visible = False



    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdSupplier.Focus()
    End Sub

    Private Sub TxtNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            TxtKdSupp_Leave(Txt_NmSupplier, e)
            Me.Size = New Size(710, 337)
            Lv_Supplier.Location = New Point(700, 141)
            Lv_Supplier.Visible = False

            'Txt_KdBarang.Focus()
        End If
    End Sub
    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)
            Me.Size = New Size(710, 337)
            Lv_Barang.Location = New Point(700, 170)
            Lv_Barang.Visible = False

            'Cmb_Release.Focus()
        End If
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub TxtKdSupp_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdSupplier.TextChanged
        If Txt_KdSupplier.Text.Trim.Length = 0 Then
            Lv_Supplier.Location = New Point(700, 141)
            Lv_Supplier.Visible = False
            Me.Size = New Size(710, 337)
            Exit Sub
        Else
            Lv_Supplier.Visible = True
            Lv_Supplier.Location = New Point(160, 141)
            Me.Size = New Size(710, 337)
        End If

        Try
            OpenConn()

            Dim lv As New ListViewItem
            Lv_Supplier.Items.Clear()

            lv = Lv_Supplier.Items.Add("--- SELURUH ---")
            lv.SubItems.Add("--- SELURUH ---")

            SQL = "select kode_supplier,nama_supplier from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_supplier like '" & Txt_KdSupplier.Text & "%' order by kode_supplier"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    lv = Lv_Supplier.Items.Add(Dr("kode_supplier"))
                    lv.SubItems.Add(Dr("nama_supplier"))
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
            Me.Size = New Size(710, 337)
            Lv_Barang.Location = New Point(700, 170)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(710, 367)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(160, 170)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select distinct Kode_Barang, Nama from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang like '%" & Txt_KdBarang.Text & "%'"
            SQL = SQL & "order by Kode_Barang"
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

    Private Sub TxtKdSupp_Leave(sender As Object, e As EventArgs) Handles Txt_KdSupplier.Leave
        If Txt_KdSupplier.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Supplier.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then

                SQL = "select kode_supplier,nama_supplier from Suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_supplier = '" & Txt_KdSupplier.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdSupplier.Text = Dr("kode_supplier")
                        Txt_NmSupplier.Text = Dr("nama_supplier")
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("Kode supplier tidak ditemukan . . ! !", Judul)
                        Txt_KdSupplier.Text = "" : Txt_NmSupplier.Text = ""
                        Txt_KdSupplier.Focus()
                    End If

                    Me.Size = New Size(710, 337)
                    Lv_Supplier.Location = New Point(700, 141)
                    Lv_Supplier.Visible = False
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

            If Not Txt_KdBarang.Text = "--- SELURUH ---" Then

                SQL = "select distinct Kode_Barang, Nama from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Txt_KdBarang.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("kode_barang")
                        Txt_NmBarang.Text = Dr("nama")
                        Cmb_Release.Focus()
                        Cmb_Release.DroppedDown = True
                    Else
                        MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul)
                        Txt_KdBarang.Text = "" : Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Me.Size = New Size(710, 337)
                    Lv_Barang.Location = New Point(700, 170)
                    Lv_Barang.Visible = False
                End Using

            Else
                Cmb_Release.Focus()
                Cmb_Release.DroppedDown = True

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKdSupp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdSupplier.Text.Trim.Length = 0 Then Txt_KdSupplier.Focus()
            TxtKdSupp_Leave(Txt_KdSupplier, e)

            Me.Size = New Size(710, 337)
            Lv_Supplier.Location = New Point(700, 141)
            Lv_Supplier.Visible = False

            'Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Me.Size = New Size(710, 337)
            Lv_Supplier.Location = New Point(700, 170)
            Lv_Supplier.Visible = False

            'Cmb_Release.Focus()
        End If
    End Sub

    Private Sub TxtKdSupp_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdSupplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub
    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub TxtNama_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmSupplier.TextChanged
        If Txt_NmSupplier.Text.Trim.Length = 0 Then
            Lv_Supplier.Location = New Point(700, 141)
            Lv_Supplier.Visible = False
            Me.Size = New Size(710, 337)
            Exit Sub
        Else
            Lv_Supplier.Visible = True
            Lv_Supplier.Location = New Point(160, 141)
            Me.Size = New Size(710, 337)
        End If

        OpenConn()

        Dim lv As New ListViewItem
        Lv_Supplier.Items.Clear()
        lv = Lv_Supplier.Items.Add("Seluruh")
        lv.SubItems.Add("Seluruh")

        SQL = "select kode_supplier,nama_supplier from Suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "nama_supplier Like '%" & Txt_NmSupplier.Text & "%' order by nama_supplier"
        Using Dr = Open(SQL)
            Do While Dr.Read
                lv = Lv_Supplier.Items.Add(Dr("kode_supplier"))
                lv.SubItems.Add(Dr("nama_supplier"))
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(710, 337)
            Lv_Barang.Location = New Point(700, 170)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(710, 367)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(160, 170)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select distinct Kode_Barang, Nama from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_NmBarang.Text & "%'"
            SQL = SQL & "order by Kode_Barang"
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

    Private Sub TxtNama_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmSupplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub LvSupp_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Supplier.DoubleClick
        If Lv_Supplier.Items.Count = 0 Or Lv_Supplier.FocusedItem.Index = -1 Then Exit Sub

        Dim Kode As String = Lv_Supplier.FocusedItem.Text
        Dim Nama As String = Lv_Supplier.FocusedItem.SubItems(1).Text

        Txt_KdSupplier.Text = Kode
        Txt_NmSupplier.Text = Nama

        Me.Size = New Size(710, 337)
        Lv_Supplier.Location = New Point(700, 141)
        Lv_Supplier.Visible = False


        Txt_KdBarang.Focus()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim Kode As String = Lv_Barang.FocusedItem.Text
        Dim Nama As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = Kode
        Txt_NmBarang.Text = Nama

        Me.Size = New Size(710, 337)
        Lv_Barang.Location = New Point(700, 170)
        Lv_Barang.Visible = False


        Cmb_Release.Focus()
        Cmb_Release.DroppedDown = True
    End Sub

    Private Sub LvSupp_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Supplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupp_DoubleClick(Lv_Supplier, e)
        End If
    End Sub
    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub
    Private Sub CmbRelease_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_StatusPenawaran.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub Cmb_Periode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Periode.KeyPress
        If e.KeyChar = Chr(13) Then Tgl1.Focus()
    End Sub

    Private Sub Chk_Periode_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Periode.CheckedChanged
        If Chk_Periode.Checked Then
            Cmb_Periode.Enabled = True
            Cmb_Periode.SelectedIndex = 0
            Tgl1.Enabled = True : Tgl2.Enabled = True
            Cmb_Periode.Focus()
        Else
            Cmb_Periode.Enabled = False
            Cmb_Periode.SelectedIndex = -1 : Cmb_Periode.Text = ""
            Tgl1.Enabled = True : Tgl2.Enabled = True
        End If
    End Sub

    Private Sub Chk_Periode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Periode.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdSupplier.Focus()
    End Sub

    Private Sub Cmb_Release_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Release.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_StatusPenawaran.Focus()
            Cmb_StatusPenawaran.DroppedDown = True
        End If
    End Sub



    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click

        If Chk_Periode.Checked Then
            If Cmb_Periode.SelectedIndex = -1 Then
                MessageBox.Show("Periode harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Periode.Focus() : Exit Sub
            End If
            If Tgl1.Value > Tgl2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
                Tgl1.Focus() : Exit Sub
            End If
        End If

        If Txt_KdSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Supplier harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdSupplier.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        ElseIf Cmb_StatusPenawaran.SelectedIndex = -1 Then
            MessageBox.Show("Status Penawaran harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_StatusPenawaran.Focus() : Exit Sub
        ElseIf Cmb_Release.SelectedIndex = -1 Then
            MessageBox.Show("Status release harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Release.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""


            SQL = "select kode_perusahaan from VW_Laporan_Penawaran where kode_perusahaan = '" & KodePerusahaan & "' "


            If Chk_Periode.Checked Then
                SQL = SQL & "and " & arrPeriode(Cmb_Periode.SelectedIndex) & " between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = "{vw_laporan_penawaran." & arrPeriode(Cmb_Periode.SelectedIndex) & "} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{vw_laporan_penawaran." & arrPeriode(Cmb_Periode.SelectedIndex) & "} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "#"
            End If

            If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then
                SQL = SQL & " and kode_supplier = '" & Txt_KdSupplier.Text & "' "
                SF = SF & " And {vw_laporan_penawaran.kode_supplier} = '" & Txt_KdSupplier.Text & "' "
            End If

            If Not Txt_KdBarang.Text = "--- SELURUH ---" Then
                SQL = SQL & " and kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & " And {vw_laporan_penawaran.kode_Barang} = '" & Txt_KdBarang.Text & "' "
            End If

            If Not Cmb_Release.SelectedIndex = 0 Then
                SQL = SQL & "and Status = '" & ArrRelease(Cmb_Release.SelectedIndex) & "' "
                SF = SF & " And {vw_laporan_penawaran.Status} = '" & ArrRelease(Cmb_Release.SelectedIndex) & "' "

            End If

            If Not Cmb_StatusPenawaran.SelectedIndex = 0 Then
                SQL = SQL & "and Status_Penawaran = '" & arrStatusPenawaran(Cmb_StatusPenawaran.SelectedIndex) & "' "
                SF = SF & " And {vw_laporan_penawaran.Status_Penawaran} = '" & arrStatusPenawaran(Cmb_StatusPenawaran.SelectedIndex) & "' "
            End If

            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New Rpt_Laporan_Penawaran

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
                        MessageBox.Show("Penawaran Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
End Class
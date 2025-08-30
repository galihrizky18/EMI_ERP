Public Class Emi_Laporan_Pembelian

    Dim JudulForm As String = "Laporan Pembelian"

    Private Sub Emi_Laporan_Pembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub

    Private Sub kosong()

        Tgl1.Value = Now.Date
        Tgl2.Value = Now.Date

        Txt_Faktur.Text = ""
        Txt_KdBarang.Text = ""
        Txt_NmBarang.Text = ""


        Lv_Faktur.Columns.Clear()
        Lv_Faktur.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_Faktur.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_Faktur.Columns.Add("User ID", 110, HorizontalAlignment.Center)
        Lv_Faktur.View = View.Details


        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details

        Lv_Supplier.Columns.Clear()
        Lv_Supplier.Columns.Add("Kode Supplier", 150, HorizontalAlignment.Left)
        Lv_Supplier.Columns.Add("Nama Supplier", 250, HorizontalAlignment.Left)
        Lv_Supplier.View = View.Details



    End Sub

    Private Sub Txt_KdSupplier_TextChanged(sender As Object, e As EventArgs) Handles Txt_Faktur.TextChanged
        If Txt_Faktur.Text.Trim.Length = 0 Then
            Me.Size = New Size(610, 300)
            Lv_Faktur.Location = New Point(600, 139)
            Lv_Faktur.Visible = False
            Txt_Faktur.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(608, 387)
            Lv_Faktur.Visible = True
            Lv_Faktur.Location = New Point(124, 139)
        End If



        Try
            OpenConn()

            Lv_Faktur.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Faktur.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select No_Faktur, Tanggal, UserID from emi_pembelian where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur like '%" & Txt_Faktur.Text & "%' and Status is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Faktur.Items.Add(Dr("No_Faktur"))
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

    Private Sub Lv_Faktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Faktur.DoubleClick
        If Lv_Faktur.Items.Count = 0 Or Lv_Faktur.FocusedItem.Index = -1 Then Exit Sub

        Dim KdSupplier As String = Lv_Faktur.FocusedItem.SubItems(0).Text

        Txt_Faktur.Text = KdSupplier

        Me.Size = New Size(610, 300)
        Lv_Faktur.Location = New Point(600, 139)
        Lv_Faktur.Visible = False
        Txt_KdSupplier.Focus()

    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged


        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(610, 300)
            Lv_Barang.Location = New Point(600, 197)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(608, 427)
            Lv_Barang.Location = New Point(124, 197)
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

    Private Sub Txt_KdSupplier_TextChanged_1(sender As Object, e As EventArgs) Handles Txt_KdSupplier.TextChanged
        If Txt_KdSupplier.Text.Trim.Length = 0 Then
            Me.Size = New Size(610, 300)
            Lv_Supplier.Location = New Point(600, 167)
            Lv_Supplier.Visible = False
            Txt_KdSupplier.Text = ""
            Txt_NmSupplier.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(608, 414)
            Lv_Supplier.Visible = True
            Lv_Supplier.Location = New Point(124, 167)
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


    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick

        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmBarang

        Me.Size = New Size(610, 300)
        Lv_Barang.Location = New Point(600, 197)
        Lv_Barang.Visible = False

        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Supplier_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Supplier.DoubleClick
        If Lv_Supplier.Items.Count = 0 Or Lv_Supplier.FocusedItem.Index = -1 Then Exit Sub

        Dim KdSupplier As String = Lv_Supplier.FocusedItem.SubItems(0).Text
        Dim NmSupplier As String = Lv_Supplier.FocusedItem.SubItems(1).Text

        Txt_KdSupplier.Text = KdSupplier
        Txt_NmSupplier.Text = NmSupplier

        Me.Size = New Size(610, 300)
        Lv_Supplier.Location = New Point(600, 167)
        Lv_Supplier.Visible = False

        Txt_KdBarang.Focus()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_Faktur.Text.Trim.Length = 0 Then
            MessageBox.Show("Faktur harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Faktur.Focus() : Exit Sub

        ElseIf Txt_KdSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Supplier harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdSupplier.Focus() : Exit Sub

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

            SQL = "select kode_perusahaan from View_Laporan_Pembelian "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{View_Laporan_Pembelian.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {View_Laporan_Pembelian.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{View_Laporan_Pembelian.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_Faktur.Text = "--- SELURUH ---" Then
                SQL = SQL & "and no_faktur = '" & Txt_Faktur.Text & "' "
                SF = SF & "And {View_Laporan_Pembelian.no_faktur} = '" & Txt_Faktur.Text & "'"
            End If

            If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then
                SQL = SQL & "and kode_supplier = '" & Txt_KdSupplier.Text & "' "
                SF = SF & "And {View_Laporan_Pembelian.kode_supplier} = '" & Txt_KdSupplier.Text & "'"
            End If

            If Not Txt_KdBarang.Text = "--- SELURUH ---" Then
                SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {View_Laporan_Pembelian.kode_barang} = '" & Txt_KdBarang.Text & "'"
            End If
            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New Rpt_Laporan_Pembelian

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

    Private Sub Lv_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Faktur.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Faktur_DoubleClick(Lv_Faktur, e)
        End If
    End Sub

    Private Sub Txt_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Faktur.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Faktur.Focus()
    End Sub

    Private Sub Txt_Faktur_Leave(sender As Object, e As EventArgs) Handles Txt_Faktur.Leave
        If Txt_Faktur.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Faktur.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Faktur.Text = "--- SELURUH ---" Then

                SQL = "select No_Faktur, Tanggal, UserID from emi_pembelian where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_Faktur.Text & "' and Status is null "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Faktur.Text = Dr("No_Faktur")
                        Txt_KdSupplier.Focus()
                    Else
                        MessageBox.Show("No Faktur tidak ditemukan . . ! !", Judul)
                        Txt_Faktur.Text = ""
                        Txt_Faktur.Focus()
                    End If

                    Me.Size = New Size(610, 300)
                    Lv_Faktur.Location = New Point(600, 139)
                    Lv_Faktur.Visible = False
                End Using

            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Txt_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Faktur.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Faktur.Text.Trim.Length = 0 Then Txt_Faktur.Focus()
            Txt_Faktur_Leave(Txt_Faktur, e)

            Me.Size = New Size(610, 300)
            Lv_Faktur.Location = New Point(600, 139)
            Lv_Faktur.Visible = False

            Txt_KdSupplier.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub
    Private Sub Txt_KdSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdSupplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
        If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then

                SQL = "select kode_barang, nama from barang where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_barang = '" & Txt_KdBarang.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("kode_barang")
                        Txt_NmBarang.Text = Dr("nama")
                    Else
                        MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul)
                        Txt_KdBarang.Text = "" : Txt_NmBarang.Text = ""
                        Txt_NmBarang.Focus()
                    End If

                    Me.Size = New Size(610, 300)
                    Lv_Barang.Location = New Point(600, 197)
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

    Private Sub Txt_KdSupplier_Leave(sender As Object, e As EventArgs) Handles Txt_KdSupplier.Leave
        If Txt_KdSupplier.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Supplier.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then


                SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & Txt_KdSupplier.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdSupplier.Text = Dr("Kode_Supplier")
                        Txt_NmSupplier.Text = Dr("Nama")
                    Else
                        MessageBox.Show("Supplier tidak ditemukan . . ! !", Judul)
                        Txt_KdSupplier.Text = "" : Txt_NmSupplier.Text = ""
                        Txt_KdSupplier.Focus()
                    End If

                    Me.Size = New Size(610, 300)
                    Lv_Supplier.Location = New Point(600, 167)
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

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_NmBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)
            Me.Size = New Size(610, 300)
            Lv_Barang.Location = New Point(600, 197)
            Lv_Barang.Visible = False
        End If


    End Sub

    Private Sub Txt_KdSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdSupplier.Text.Trim.Length = 0 Then Txt_KdSupplier.Focus()
            Txt_KdSupplier_Leave(Txt_KdSupplier, e)

            Me.Size = New Size(610, 300)
            Lv_Supplier.Location = New Point(600, 167)
            Lv_Supplier.Visible = False

            Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmSupplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)
            Me.Size = New Size(610, 300)
            Lv_Barang.Location = New Point(600, 197)
            Lv_Barang.Visible = False

            BtnCetak.Focus()
        End If
    End Sub

    Private Sub Txt_NmSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmSupplier.KeyPress

        If e.KeyChar = Chr(13) Then
            Txt_KdSupplier_Leave(Txt_NmSupplier, e)
            Me.Size = New Size(610, 300)
            Lv_Supplier.Location = New Point(600, 167)
            Lv_Supplier.Visible = False

            Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged

        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(610, 300)
            Lv_Barang.Location = New Point(600, 197)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(610, 427)
            Lv_Barang.Location = New Point(124, 197)
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

    Private Sub Txt_NmSupplier_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmSupplier.TextChanged

        If Txt_NmSupplier.Text.Trim.Length = 0 Then
            Me.Size = New Size(610, 300)
            Lv_Supplier.Location = New Point(600, 167)
            Lv_Supplier.Visible = False
            Txt_KdSupplier.Text = ""
            Txt_NmSupplier.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(610, 414)
            Lv_Supplier.Location = New Point(124, 167)
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

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub
    Private Sub Lv_Supplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Supplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Supplier_DoubleClick(Lv_Supplier, e)
        End If
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Faktur.Focus()
    End Sub


End Class
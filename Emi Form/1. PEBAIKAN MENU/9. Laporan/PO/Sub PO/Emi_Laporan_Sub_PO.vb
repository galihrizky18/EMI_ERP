Public Class Emi_Laporan_Sub_PO

    Dim arrParamLain, arrParamLainSF, arrTanggal As New ArrayList

    Dim JudulForm As String = "Laporan Purchase Order"

    Private Sub Emi_Laporan_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()
    End Sub

    Private Sub Kosong()

        Cmb_Periode.Items.Clear() : arrTanggal.Clear()
        Cmb_Periode.Items.Add("Tanggal Dibuat") : arrTanggal.Add("Tanggal_Created")
        Cmb_Periode.Items.Add("Tanggal Direlease") : arrTanggal.Add("Tanggal_Release")
        Cmb_Periode.SelectedIndex = 0

        Tgl1.Value = Now : Tgl2.Value = Now

        Cmb_Status.Items.Clear()
        Cmb_Status.Items.Add("--- SEMUA ---")
        Cmb_Status.Items.Add("COMPLETED")
        Cmb_Status.Items.Add("SUBMITED")
        Cmb_Status.Items.Add("UNSUBMITED")
        Cmb_Status.SelectedIndex = 0

        Txt_Faktur.Text = "--- SELURUH ---"
        Txt_KdSupplier.Text = "--- SELURUH ---" : Txt_NmSupplier.Text = "--- SELURUH ---"
        Txt_KdBarang.Text = "--- SELURUH ---" : Txt_NmBarang.Text = "--- SELURUH ---"

        Cmb_ParamLain.Items.Clear() : arrParamLain.Clear() : arrParamLainSF.Clear()
        Cmb_ParamLain.Items.Add("--- SEMUA ---") : arrParamLain.Add("--- SEMUA ---") : arrParamLainSF.Add("--- SEMUA ---")
        Cmb_ParamLain.Items.Add("User Created") : arrParamLain.Add("User_Created") : arrParamLainSF.Add("{View_Laporan_Sub_PO.User_Created}")
        Cmb_ParamLain.Items.Add("User Release") : arrParamLain.Add("User_Release") : arrParamLainSF.Add("{View_Laporan_Sub_PO.User_Release}")
        Cmb_ParamLain.SelectedIndex = 0
        Txt_ParamLain.Text = ""

        Lv_Faktur.Columns.Clear()
        Lv_Faktur.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_Faktur.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
        Lv_Faktur.Columns.Add("Tangal Dibuat", 150, HorizontalAlignment.Center)
        Lv_Faktur.View = View.Details

        Lv_Supplier.Columns.Clear()
        Lv_Supplier.Columns.Add("Kode Supplier", 150, HorizontalAlignment.Left)
        Lv_Supplier.Columns.Add("Nama Supplier", 400, HorizontalAlignment.Left)
        Lv_Supplier.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 400, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details

        Lv_Faktur.Visible = False
        Lv_Supplier.Visible = False
        Lv_Barang.Visible = False

        Me.Size = New Size(694, 363)

    End Sub

    '============================================================================================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '============================================================================================================================================================================================================
    Private Sub Txt_Faktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_Faktur.TextChanged
        If Txt_Faktur.Text.Trim.Length = 0 Then
            Me.Size = New Size(694, 363)
            Lv_Faktur.Location = New Point(700, 173)
            Lv_Faktur.Visible = False
            Txt_Faktur.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(694, 423)
            Lv_Faktur.Visible = True
            Lv_Faktur.Location = New Point(124, 173)
        End If

        Try
            OpenConn()

            Lv_Faktur.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Faktur.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select No_Faktur, No_Nota, Tanggal from EMI_Pembelian_PO where Kode_Perusahaan ='" & KodePerusahaan & "' and No_Faktur like '%" & Txt_Faktur.Text & "%' and Status is null "
            SQL = SQL & "order by No_Faktur"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Faktur.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Nota"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdSupplier_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdSupplier.TextChanged
        If Txt_KdSupplier.Text.Trim.Length = 0 Then
            Me.Size = New Size(694, 363)
            Lv_Supplier.Location = New Point(700, 200)
            Lv_Supplier.Visible = False
            Txt_KdSupplier.Text = ""
            Txt_NmSupplier.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(694, 452)
            Lv_Supplier.Visible = True
            Lv_Supplier.Location = New Point(124, 200)
        End If

        Try
            OpenConn()

            Lv_Supplier.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Supplier.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Supplier like '%" & Txt_KdSupplier.Text & "%' "
            SQL = SQL & "order by Kode_Supplier"
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

    Private Sub Txt_NmSupplier_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmSupplier.TextChanged
        If Txt_NmSupplier.Text.Trim.Length = 0 Then
            Me.Size = New Size(694, 363)
            Lv_Supplier.Location = New Point(700, 200)
            Lv_Supplier.Visible = False
            Txt_KdSupplier.Text = ""
            Txt_NmSupplier.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(694, 452)
            Lv_Supplier.Visible = True
            Lv_Supplier.Location = New Point(124, 200)
        End If

        Try
            OpenConn()

            Lv_Supplier.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Supplier.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_NmSupplier.Text & "%' "
            SQL = SQL & "order by Kode_Supplier"
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

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(694, 363)
            Lv_Barang.Location = New Point(700, 232)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(694, 482)
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



    '============================================================================================================================================================================================================
    '=     HANDLE LEAVE
    '============================================================================================================================================================================================================

    Private Sub Txt_Faktur_Leave(sender As Object, e As EventArgs) Handles Txt_Faktur.Leave
        If Txt_Faktur.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Faktur.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Faktur.Text = "--- SELURUH ---" Then

                SQL = "select No_Faktur, No_Nota, Tanggal from EMI_Pembelian_PO where Kode_Perusahaan ='" & KodePerusahaan & "' and No_Faktur = '" & Txt_Faktur.Text & "' and Status is null "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Faktur.Text = Dr("No_Faktur")
                        Txt_KdSupplier.Focus()
                    Else
                        MessageBox.Show("No Faktur tidak ditemukan . . ! !", Judul)
                        Txt_Faktur.Text = ""
                        Txt_Faktur.Focus()
                    End If

                    Me.Size = New Size(694, 363)
                    Lv_Faktur.Location = New Point(700, 173)
                    Lv_Faktur.Visible = False
                End Using
            Else
                Txt_KdSupplier.Focus()

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
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("Supplier tidak ditemukan . . ! !", Judul)
                        Txt_KdSupplier.Text = "" : Txt_NmSupplier.Text = ""
                        Txt_KdSupplier.Focus()
                    End If

                    Me.Size = New Size(694, 363)
                    Lv_Supplier.Location = New Point(700, 200)
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
                        Cmb_ParamLain.Focus()
                    Else
                        MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul)
                        Txt_KdBarang.Text = "" : Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Me.Size = New Size(694, 363)
                    Lv_Barang.Location = New Point(700, 232)
                    Lv_Barang.Visible = False
                End Using

            Else
                Cmb_ParamLain.Focus()

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(694, 363)
            Lv_Barang.Location = New Point(700, 232)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(694, 482)
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

    '============================================================================================================================================================================================================
    '=     HANDLE LISTVIEW
    '============================================================================================================================================================================================================
    Private Sub Lv_Faktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Faktur.DoubleClick
        If Lv_Faktur.Items.Count = 0 Or Lv_Faktur.FocusedItem.Index = -1 Then Exit Sub

        Dim NoFaktur As String = Lv_Faktur.FocusedItem.SubItems(0).Text

        Txt_Faktur.Text = NoFaktur

        Me.Size = New Size(694, 363)
        Lv_Faktur.Location = New Point(700, 173)
        Lv_Faktur.Visible = False
        Txt_KdSupplier.Focus()
    End Sub

    Private Sub Lv_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Faktur.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Faktur_DoubleClick(Lv_Faktur, e)
        End If
    End Sub

    Private Sub Lv_Supplier_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Supplier.DoubleClick
        If Lv_Supplier.Items.Count = 0 Or Lv_Supplier.FocusedItem.Index = -1 Then Exit Sub

        Dim KdSupplier As String = Lv_Supplier.FocusedItem.SubItems(0).Text
        Dim NmSupplier As String = Lv_Supplier.FocusedItem.SubItems(1).Text

        Txt_KdSupplier.Text = KdSupplier
        Txt_NmSupplier.Text = NmSupplier

        Me.Size = New Size(694, 363)
        Lv_Supplier.Location = New Point(700, 200)
        Lv_Supplier.Visible = False

        Txt_KdBarang.Focus()
    End Sub

    Private Sub Lv_Supplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Supplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Supplier_DoubleClick(Lv_Supplier, e)
        End If
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmBarang

        Me.Size = New Size(694, 363)
        Lv_Barang.Location = New Point(700, 232)
        Lv_Barang.Visible = False

        Cmb_ParamLain.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    '============================================================================================================================================================================================================
    '=     HANDLE KEY PRESS
    '============================================================================================================================================================================================================
    Private Sub Cmb_Periode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Periode.KeyPress
        If e.KeyChar = Chr(13) Then Tgl1.Focus()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Status.Focus()
    End Sub

    Private Sub Cmb_Status_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Status.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Faktur.Focus()
    End Sub

    Private Sub Txt_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Faktur.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Faktur.Text.Trim.Length = 0 Then Txt_Faktur.Focus()
            Txt_Faktur_Leave(Txt_Faktur, e)

            Me.Size = New Size(694, 363)
            Lv_Faktur.Location = New Point(700, 173)
            Lv_Faktur.Visible = False

            Txt_KdSupplier.Focus()
        End If
    End Sub

    Private Sub Txt_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Faktur.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Faktur.Focus()
    End Sub

    Private Sub Txt_KdSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdSupplier.Text.Trim.Length = 0 Then Txt_KdSupplier.Focus()
            Txt_KdSupplier_Leave(Txt_KdSupplier, e)

            Me.Size = New Size(694, 363)
            Lv_Supplier.Location = New Point(700, 200)
            Lv_Supplier.Visible = False

            Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Txt_KdSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdSupplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub Txt_NmSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdSupplier_Leave(Txt_NmSupplier, e)
            Me.Size = New Size(694, 363)
            Lv_Supplier.Location = New Point(700, 200)
            Lv_Supplier.Visible = False

            Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Txt_NmSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmSupplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)
            Me.Size = New Size(694, 363)
            Lv_Barang.Location = New Point(700, 232)
            Lv_Barang.Visible = False

        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)
            Me.Size = New Size(694, 363)
            Lv_Barang.Location = New Point(700, 232)
            Lv_Barang.Visible = False

        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Cmb_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
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



    '============================================================================================================================================================================================================
    '=     HANDLE BUTTON
    '============================================================================================================================================================================================================

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Cmb_Periode.SelectedIndex = -1 Then
            MessageBox.Show("Periode harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Periode.Focus() : Exit Sub
        ElseIf Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Cmb_Status.SelectedIndex = -1 Then
            MessageBox.Show("Status harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Status.Focus() : Exit Sub
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

        If Cmb_ParamLain.SelectedIndex <> 0 Then
            If Txt_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Parameter lain harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_ParamLain.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select kode_perusahaan from View_Laporan_Sub_PO  "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and " & arrTanggal(Cmb_Periode.SelectedIndex) & " between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{View_Laporan_Sub_PO.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {View_Laporan_Sub_PO." & arrTanggal(Cmb_Periode.SelectedIndex) & "} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{View_Laporan_Sub_PO." & arrTanggal(Cmb_Periode.SelectedIndex) & "} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Cmb_Status.SelectedIndex = 0 Then

                If Cmb_Status.SelectedIndex = 1 Then
                    SQL = SQL & "and Status_PO = 'COMPLETED' "
                    SF = SF & "And {View_Laporan_Sub_PO.Status_PO} = 'COMPLETED' "
                ElseIf Cmb_Status.SelectedIndex = 2 Then
                    SQL = SQL & "and Status_PO = 'SUBMITED' "
                    SF = SF & "And {View_Laporan_Sub_PO.Status_PO} = 'SUBMITED' "

                ElseIf Cmb_Status.SelectedIndex = 3 Then
                    SQL = SQL & "and Status_PO = 'UNSUBMITED' "
                    SF = SF & "And {View_Laporan_Sub_PO.Status_PO} = 'UNSUBMITED' "
                End If

            End If

            If Not Txt_Faktur.Text = "--- SELURUH ---" Then
                SQL = SQL & "and no_faktur = '" & Txt_Faktur.Text & "' "
                SF = SF & "And {View_Laporan_Sub_PO.no_faktur} = '" & Txt_Faktur.Text & "'"
            End If

            If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then
                SQL = SQL & "and Kode_Supplier = '" & Txt_KdSupplier.Text & "' "
                SF = SF & "And {View_Laporan_Sub_PO.Kode_Supplier} = '" & Txt_KdSupplier.Text & "'"
            End If

            If Not Txt_KdBarang.Text = "--- SELURUH ---" Then
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {View_Laporan_Sub_PO.Kode_Barang} = '" & Txt_KdBarang.Text & "'"
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

                        Dim CrDoc As New Rpt_Laporan_Sub_PO

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
                        MessageBox.Show("Purchase Order Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

End Class
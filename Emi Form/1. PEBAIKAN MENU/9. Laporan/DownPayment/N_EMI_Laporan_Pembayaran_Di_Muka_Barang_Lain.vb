Public Class N_EMI_Laporan_Pembayaran_Di_Muka_Barang_Lain


    Dim Switch_AutoComplete As Boolean = False

    Dim arr_Status_DP As New ArrayList

    Private Sub N_EMI_Laporan_Down_Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_NoPengajuan.Columns.Clear()
        Lv_NoPengajuan.Columns.Add("No Pengajuan", 180, HorizontalAlignment.Left)
        Lv_NoPengajuan.View = View.Details

        Lv_NoPO.Columns.Clear()
        Lv_NoPO.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_NoPO.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_NoPO.Columns.Add("Keterangan", 180, HorizontalAlignment.Left)
        Lv_NoPO.View = View.Details

        Lv_Supplier.Columns.Clear()
        Lv_Supplier.Columns.Add("Kode Supplier", 130, HorizontalAlignment.Left)
        Lv_Supplier.Columns.Add("Supplier", 250, HorizontalAlignment.Left)
        Lv_Supplier.View = View.Details

        Cmb_Status_DP.Items.Clear()
        Cmb_Status_DP.Items.Add(OpsiSeluruh) : arr_Status_DP.Add(OpsiSeluruh)
        Cmb_Status_DP.Items.Add("Sisa") : arr_Status_DP.Add("SISA")
        Cmb_Status_DP.Items.Add("Habis") : arr_Status_DP.Add("HABIS")

        Kosong()

    End Sub


    Private Sub Kosong()



        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        Switch_AutoComplete = False
        Txt_Pengajuan.Text = OpsiSeluruh
        Txt_No_PO.Text = OpsiSeluruh : Txt_Keterangan_PO.Text = OpsiSeluruh
        Txt_Kd_Supplier.Text = OpsiSeluruh : Txt_Supplier.Text = OpsiSeluruh
        Switch_AutoComplete = True

        Cmb_Status_DP.SelectedIndex = 0

        Tgl1.Focus()


    End Sub




    '==========================================================================================================================================================================================
    '=     HANDLE TEXT CHANGED
    '==========================================================================================================================================================================================
    Private Sub Txt_Pengajuan_TextChanged(sender As Object, e As EventArgs) Handles Txt_Pengajuan.TextChanged
        If Switch_AutoComplete = False Then Exit Sub
        If Txt_Pengajuan.Text.Trim.Length = 0 Then
            Me.Size = New Size(660, 313)
            Lv_NoPengajuan.Visible = False
            Lv_NoPengajuan.Location = New Point(650, 125)
            Txt_Pengajuan.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 380)
            Lv_NoPengajuan.Location = New Point(123, 125)
            Lv_NoPengajuan.Visible = True
        End If

        Try
            OpenConn()

            Lv_NoPengajuan.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_NoPengajuan.Items.Add(OpsiSeluruh)

            SQL = "select Distinct No_Pengajuan from EMI_Transaksi_Pembayaran_Dimuka_Asset where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null "
            SQL = SQL & "and No_Pengajuan like '%" & Txt_Pengajuan.Text.Trim & "%' order by No_Pengajuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_NoPengajuan.Items.Add(Dr("No_Pengajuan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_No_PO_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_PO.TextChanged
        If Switch_AutoComplete = False Then Exit Sub
        If Txt_No_PO.Text.Trim.Length = 0 Then
            Me.Size = New Size(660, 313)
            Lv_NoPO.Visible = False
            Lv_NoPO.Location = New Point(650, 150)
            Txt_No_PO.Text = ""
            Txt_Keterangan_PO.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 410)
            Lv_NoPO.Location = New Point(123, 150)
            Lv_NoPO.Visible = True
        End If

        Try
            OpenConn()

            Lv_NoPO.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_NoPO.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select no_faktur, tanggal, jam, no_nota from emi_pembelian_po_induk_barang_lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Status is null "
            SQL = SQL & "and No_Faktur like '%" & Txt_No_PO.Text & "%' "
            SQL = SQL & "order by no_faktur, tanggal, jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_NoPO.Items.Add(Dr("no_faktur"))
                    Lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("no_nota"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_Keterangan_PO_TextChanged(sender As Object, e As EventArgs) Handles Txt_Keterangan_PO.TextChanged
        If Switch_AutoComplete = False Then Exit Sub
        If Txt_Keterangan_PO.Text.Trim.Length = 0 Then
            Me.Size = New Size(660, 313)
            Lv_NoPO.Visible = False
            Lv_NoPO.Location = New Point(650, 150)
            Txt_No_PO.Text = ""
            Txt_Keterangan_PO.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 410)
            Lv_NoPO.Location = New Point(123, 150)
            Lv_NoPO.Visible = True
        End If

        Try
            OpenConn()

            Lv_NoPO.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_NoPO.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select no_faktur, tanggal, jam, no_nota from emi_pembelian_po_induk_barang_lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Status is null "
            SQL = SQL & "and no_nota like '%" & Txt_Keterangan_PO.Text & "%' "
            SQL = SQL & "order by no_faktur, tanggal, jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_NoPO.Items.Add(Dr("no_faktur"))
                    Lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("no_nota"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_Kd_Supplier_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Supplier.TextChanged
        If Switch_AutoComplete = False Then Exit Sub
        If Txt_Kd_Supplier.Text.Trim.Length = 0 Then
            Me.Size = New Size(660, 313)
            Lv_Supplier.Visible = False
            Lv_Supplier.Location = New Point(650, 176)
            Txt_Kd_Supplier.Text = ""
            Txt_Supplier.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 430)
            Lv_Supplier.Location = New Point(123, 176)
            Lv_Supplier.Visible = True
        End If

        Try
            OpenConn()

            Lv_Supplier.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Supplier.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Supplier like '%" & Txt_Kd_Supplier.Text & "%' order by Kode_Supplier "
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
    Private Sub Txt_Supplier_TextChanged(sender As Object, e As EventArgs) Handles Txt_Supplier.TextChanged
        If Switch_AutoComplete = False Then Exit Sub
        If Txt_Supplier.Text.Trim.Length = 0 Then
            Me.Size = New Size(660, 313)
            Lv_Supplier.Visible = False
            Lv_Supplier.Location = New Point(650, 176)
            Txt_Kd_Supplier.Text = ""
            Txt_Supplier.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 430)
            Lv_Supplier.Location = New Point(123, 176)
            Lv_Supplier.Visible = True
        End If

        Try
            OpenConn()

            Lv_Supplier.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Supplier.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Nama like '%" & Txt_Supplier.Text & "%' order by Kode_Supplier "
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




    '==========================================================================================================================================================================================
    '=     HANDLE LEAVE
    '==========================================================================================================================================================================================
    Private Sub Txt_Pengajuan_Leave(sender As Object, e As EventArgs) Handles Txt_Pengajuan.Leave
        If Txt_Pengajuan.Text.Trim.Length = 0 Then Exit Sub
        If Lv_NoPengajuan.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Pengajuan.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = "select Distinct top 1 No_Pengajuan from EMI_Transaksi_Pembayaran_Dimuka_Asset where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null "
                SQL = SQL & "and No_Pengajuan = '" & Txt_Pengajuan.Text.Trim & "' order by No_Pengajuan "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Pengajuan.Text = Dr("No_Pengajuan")

                        Txt_No_PO.Focus()
                    Else
                        MessageBox.Show("No Pengajuan tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Pengajuan.Text = ""
                        Txt_Pengajuan.Focus()
                    End If

                    Me.Size = New Size(660, 313)
                    Lv_NoPengajuan.Visible = False
                    Lv_NoPengajuan.Location = New Point(660, 125)
                End Using
            Else
                Txt_No_PO.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_No_PO_Leave(sender As Object, e As EventArgs) Handles Txt_No_PO.Leave
        If Txt_No_PO.Text.Trim.Length = 0 Then Exit Sub
        If Lv_NoPO.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_PO.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = "select no_faktur, tanggal, jam, no_nota from EMI_Pembelian_PO_Induk_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Status is null "
                SQL = SQL & "and No_Faktur = '" & Txt_No_PO.Text & "' "
                SQL = SQL & "order by no_faktur, tanggal, jam "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_PO.Text = Dr("no_faktur")
                        Txt_Keterangan_PO.Text = Dr("no_nota")

                        Txt_Kd_Supplier.Focus()
                    Else
                        MessageBox.Show("No PO tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_PO.Text = ""
                        Txt_Keterangan_PO.Text = ""
                        Txt_No_PO.Focus()
                    End If

                    Me.Size = New Size(660, 313)
                    Lv_NoPO.Visible = False
                    Lv_NoPO.Location = New Point(660, 150)
                End Using
            Else
                Txt_Kd_Supplier.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_Kd_Supplier_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Supplier.Leave
        If Txt_Kd_Supplier.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Supplier.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Supplier.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Supplier = '" & Txt_Kd_Supplier.Text & "' order by Kode_Supplier "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kd_Supplier.Text = Dr("Kode_Supplier")
                        Txt_Supplier.Text = Dr("Nama")

                        Cmb_Status_DP.DroppedDown = True
                        Cmb_Status_DP.Focus()
                    Else
                        MessageBox.Show("Supplier tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kd_Supplier.Text = ""
                        Txt_Supplier.Text = ""
                        Txt_Kd_Supplier.Focus()
                    End If

                    Me.Size = New Size(660, 313)
                    Lv_Supplier.Visible = False
                    Lv_Supplier.Location = New Point(660, 176)
                End Using
            Else
                Cmb_Status_DP.DroppedDown = True
                Cmb_Status_DP.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub








    '==========================================================================================================================================================================================
    '=     HANDLE LV
    '==========================================================================================================================================================================================
    Private Sub Lv_NoPengajuan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_NoPengajuan.DoubleClick
        If Lv_NoPengajuan.Items.Count = 0 Or Lv_NoPengajuan.FocusedItem.Index = -1 Then Exit Sub

        Dim No_Pengajuan As String = Lv_NoPengajuan.FocusedItem.SubItems(0).Text

        Switch_AutoComplete = False
        Txt_Pengajuan.Text = No_Pengajuan
        Switch_AutoComplete = True

        Me.Size = New Size(660, 313)
        Lv_NoPengajuan.Visible = False
        Lv_NoPengajuan.Location = New Point(650, 125)

        Txt_No_PO.Focus()
    End Sub
    Private Sub Lv_NoPengajuan_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_NoPengajuan.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_NoPengajuan_DoubleClick(Lv_NoPengajuan, e)
        End If
    End Sub
    Private Sub Lv_NoPO_DoubleClick(sender As Object, e As EventArgs) Handles Lv_NoPO.DoubleClick
        If Lv_NoPO.Items.Count = 0 Or Lv_NoPO.FocusedItem.Index = -1 Then Exit Sub

        Dim NO_PO As String = Lv_NoPO.FocusedItem.SubItems(0).Text
        Dim Keterangan_PO As String = Lv_NoPO.FocusedItem.SubItems(2).Text

        Switch_AutoComplete = False
        Txt_No_PO.Text = NO_PO
        Txt_Keterangan_PO.Text = Keterangan_PO
        Switch_AutoComplete = True

        Me.Size = New Size(660, 313)
        Lv_NoPO.Visible = False
        Lv_NoPO.Location = New Point(650, 150)

        Txt_Kd_Supplier.Focus()
    End Sub
    Private Sub Lv_NoPO_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_NoPO.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_NoPO_DoubleClick(Lv_NoPO, e)
        End If
    End Sub
    Private Sub Lv_Supplier_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Supplier.DoubleClick
        If Lv_Supplier.Items.Count = 0 Or Lv_Supplier.FocusedItem.Index = -1 Then Exit Sub

        Dim Kd_Supplier As String = Lv_Supplier.FocusedItem.SubItems(0).Text
        Dim Supplier As String = Lv_Supplier.FocusedItem.SubItems(1).Text

        Switch_AutoComplete = False
        Txt_Kd_Supplier.Text = Kd_Supplier
        Txt_Supplier.Text = Supplier
        Switch_AutoComplete = True

        Me.Size = New Size(660, 313)
        Lv_Supplier.Visible = False
        Lv_Supplier.Location = New Point(650, 176)

        Cmb_Status_DP.DroppedDown = True
        Cmb_Status_DP.Focus()
    End Sub
    Private Sub Lv_Supplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Supplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Supplier_DoubleClick(Lv_Supplier, e)
        End If
    End Sub


    '==========================================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '==========================================================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Pengajuan.Focus()
    End Sub

    Private Sub Txt_Pengajuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Pengajuan.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Pengajuan.Text.Trim.Length = 0 Then Txt_Pengajuan.Focus()
            Txt_Pengajuan_Leave(Txt_Pengajuan, e)

            Me.Size = New Size(660, 313)
            Lv_NoPengajuan.Visible = False
            Lv_NoPengajuan.Location = New Point(650, 125)

        End If
    End Sub

    Private Sub Txt_Pengajuan_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Pengajuan.KeyDown
        If e.KeyCode = Keys.Down Then Lv_NoPengajuan.Focus()
    End Sub

    Private Sub Txt_No_PO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_PO.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_PO.Text.Trim.Length = 0 Then Txt_No_PO.Focus()
            Txt_No_PO_Leave(Txt_No_PO, e)

            Me.Size = New Size(660, 313)
            Lv_NoPO.Visible = False
            Lv_NoPO.Location = New Point(650, 150)

        End If
    End Sub

    Private Sub Txt_No_PO_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_PO.KeyDown
        If e.KeyCode = Keys.Down Then Lv_NoPO.Focus()
    End Sub

    Private Sub Txt_Keterangan_PO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan_PO.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_No_PO_Leave(Txt_Keterangan_PO, e)

            Me.Size = New Size(660, 313)
            Lv_NoPO.Visible = False
            Lv_NoPO.Location = New Point(650, 150)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Keterangan_PO_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Keterangan_PO.KeyDown
        If e.KeyCode = Keys.Down Then Lv_NoPO.Focus()
    End Sub

    Private Sub Txt_Kd_Supplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Supplier.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Supplier.Text.Trim.Length = 0 Then Txt_Kd_Supplier.Focus()
            Txt_Kd_Supplier_Leave(Txt_Kd_Supplier, e)

            Me.Size = New Size(660, 313)
            Lv_Supplier.Visible = False
            Lv_Supplier.Location = New Point(650, 176)

        End If
    End Sub

    Private Sub Txt_Kd_Supplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Supplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub Txt_Supplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Supplier.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kd_Supplier_Leave(Txt_Supplier, e)

            Me.Size = New Size(660, 313)
            Lv_Supplier.Visible = False
            Lv_Supplier.Location = New Point(650, 176)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Supplier_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Supplier.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_Pengajuan.Text.Trim.Length = 0 Then
            MessageBox.Show("No Pengajuan harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Pengajuan.Focus() : Exit Sub
        ElseIf Txt_No_PO.Text.Trim.Length = 0 Then
            MessageBox.Show("No PO harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_PO.Focus() : Exit Sub
        ElseIf Txt_Kd_Supplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Supplier harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Supplier.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select * from N_EMI_View_Laporan_Down_Payment_Asset "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{N_EMI_View_Laporan_Down_Payment_Asset.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_View_Laporan_Down_Payment_Asset.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{N_EMI_View_Laporan_Down_Payment_Asset.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_Pengajuan.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and No_Pengajuan = '" & Txt_Pengajuan.Text & "' "
                SF = SF & "And {N_EMI_View_Laporan_Down_Payment_Asset.No_Pengajuan} = '" & Txt_Pengajuan.Text & "' "
            End If

            If Not Txt_No_PO.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and No_Fak_PO = '" & Txt_No_PO.Text & "' "
                SF = SF & "And {N_EMI_View_Laporan_Down_Payment_Asset.No_Fak_PO} = '" & Txt_No_PO.Text & "' "
            End If

            If Not Txt_Kd_Supplier.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and kode_Supplier = '" & Txt_Kd_Supplier.Text & "' "
                SF = SF & "And {N_EMI_View_Laporan_Down_Payment_Asset.kode_Supplier} = '" & Txt_Kd_Supplier.Text & "' "
            End If

            If Cmb_Status_DP.SelectedIndex > 0 Then
                If arr_Status_DP(Cmb_Status_DP.SelectedIndex) = "SISA" Then
                    SQL = SQL & "and Sisa > 0 "
                    SF = SF & "And {N_EMI_View_Laporan_Down_Payment_Asset.Sisa} > 0 "
                ElseIf arr_Status_DP(Cmb_Status_DP.SelectedIndex) = "HABIS" Then
                    SQL = SQL & "and Sisa <= 0 "
                    SF = SF & "And {N_EMI_View_Laporan_Down_Payment_Asset.Sisa} <= 0 "
                End If
            End If

            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim dt As DataTable = DS.Tables("MyTable")

                        Dim CrDoc As New N_EMI_CR_Laporan_Pembayaran_Di_Muka_Barang_Lain

                        CrDoc.SetDataSource(dt)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan Down Payment"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                    Else

                        CloseConn()
                        MessageBox.Show("Daata Down Payment Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub Cmb_Status_DP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Status_DP.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Status_DP.SelectedIndex <> -1 Then
                BtnCetak.Focus()
            End If
        End If
    End Sub
End Class

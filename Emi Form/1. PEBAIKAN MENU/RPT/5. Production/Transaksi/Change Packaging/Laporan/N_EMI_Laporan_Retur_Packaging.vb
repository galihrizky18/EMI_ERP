Public Class N_EMI_Laporan_Retur_Packaging


    Dim ArrJenisBarang As New ArrayList

    Dim Switch_Auto_Complete As Boolean = False

    Private Sub N_EMI_Laporan_Retur_Packaging_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_No_Split.Columns.Clear()
        Lv_No_Split.Columns.Add("No Split", 150, HorizontalAlignment.Left)
        Lv_No_Split.Columns.Add("No PO", 150, HorizontalAlignment.Left)
        Lv_No_Split.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_No_Split.Columns.Add("Jam", 90, HorizontalAlignment.Center)
        Lv_No_Split.View = View.Details

        Lv_No_Transaksi.Columns.Clear()
        Lv_No_Transaksi.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
        Lv_No_Transaksi.Columns.Add("No Split", 150, HorizontalAlignment.Left)
        Lv_No_Transaksi.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_No_Transaksi.Columns.Add("Jam", 90, HorizontalAlignment.Center)
        Lv_No_Transaksi.View = View.Details

        Lv_No_RM.Columns.Clear()
        Lv_No_RM.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_No_RM.Columns.Add("No Split", 150, HorizontalAlignment.Left)
        Lv_No_RM.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_No_RM.Columns.Add("Jam", 90, HorizontalAlignment.Center)
        Lv_No_RM.Columns.Add("Keterangan", 180, HorizontalAlignment.Center)
        Lv_No_RM.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 330, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details

        Cmb_Jenis_Barang.Items.Clear() : ArrJenisBarang.Clear()
        Cmb_Jenis_Barang.Items.Add(OpsiSeluruh) : ArrJenisBarang.Add(OpsiSeluruh)
        Cmb_Jenis_Barang.Items.Add("Barang Awal") : ArrJenisBarang.Add("Kode_Barang_Awal")
        Cmb_Jenis_Barang.Items.Add("Barang Tujuan") : ArrJenisBarang.Add("Kode_Barang_Tujuan")

        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear()
            Cmb_Lokasi.Items.Add(OpsiSeluruh)
            SQL = "select kode_stock_owner "
            SQL &= $"from Stock_Owner_Gudang "
            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
            SQL &= $"order by Kode_Stock_Owner "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("kode_stock_owner"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
    End Sub

    Private Sub Kosong()

        Tgl1.Value = DateTime.Today
        Tgl2.Value = DateTime.Today

        Switch_Auto_Complete = True
        Cmb_Lokasi.SelectedIndex = 0
        Txt_No_Split.Text = OpsiSeluruh
        Txt_No_Transaksi.Text = OpsiSeluruh
        Txt_No_RM.Text = OpsiSeluruh
        Txt_Ket_RM.Text = OpsiSeluruh
        Cmb_Jenis_Barang.SelectedIndex = 0
        Txt_Kd_Barang.Text = OpsiSeluruh
        Txt_Nm_Barang.Text = OpsiSeluruh
        Switch_Auto_Complete = False

        Txt_Kd_Barang.Enabled = False
        Txt_Nm_Barang.Enabled = False

        Me.Size = New Size(800, 345)
        Tgl1.Focus()

    End Sub

    Private Sub Txt_No_Split_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Split.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_No_Split.Text.Trim.Length = 0 Then
            Me.Size = New Size(800, 345)
            Lv_No_Split.Visible = False
            Lv_No_Split.Location = New Point(790, 155)
            Txt_No_Split.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(800, 417)
            Lv_No_Split.Location = New Point(150, 155)
            Lv_No_Split.Visible = True
        End If

        Try
            OpenConn()

            Lv_No_Split.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_No_Split.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select No_Transaksi, No_PO, Tanggal, Jam "
            SQL &= $"from Emi_Split_Production_Order "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and No_Transaksi like '%{Txt_No_Split.Text.Trim}%' "
            SQL &= $"order by No_Transaksi, Tanggal, Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_No_Split.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("No_PO"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("tanggal")) = "", "-", Format(Dr("tanggal"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("jam")) = "", "-", Dr("jam")))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Spli_Leave(sender As Object, e As EventArgs) Handles Txt_No_Split.Leave
        If Txt_No_Split.Text.Trim.Length = 0 Then Exit Sub
        If Lv_No_Split.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_Split.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

                SQL = "select No_Transaksi, No_PO, Tanggal, Jam "
                SQL &= $"from Emi_Split_Production_Order "
                SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                SQL &= $"and No_Transaksi = '{Txt_No_Split.Text.Trim}' "
                SQL &= $"order by No_Transaksi, Tanggal, Jam "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_Split.Text = Dr("No_Transaksi")
                        Txt_No_Transaksi.Focus()
                    Else
                        MessageBox.Show("No Split Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_Split.Text = ""
                        Txt_No_Split.Focus()
                    End If

                    Me.Size = New Size(800, 345)
                    Lv_No_Split.Visible = False
                    Lv_No_Split.Location = New Point(790, 155)
                End Using
            Else
                Txt_No_Transaksi.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Transaksi_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Transaksi.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_No_Transaksi.Text.Trim.Length = 0 Then
            Me.Size = New Size(800, 345)
            Lv_No_Transaksi.Visible = False
            Lv_No_Transaksi.Location = New Point(790, 180)
            Txt_No_Transaksi.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(800, 445)
            Lv_No_Transaksi.Location = New Point(150, 180)
            Lv_No_Transaksi.Visible = True
        End If

        Try
            OpenConn()

            Lv_No_Transaksi.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_No_Transaksi.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select No_Transaksi, No_Split, tanggal, jam "
            SQL &= $"from EMI_Production_Results_Detail_Change_Packaging "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and No_Transaksi like '%{Txt_No_Transaksi.Text.Trim}%' "
            If Txt_No_Split.Text.Trim.ToUpper <> OpsiSeluruh.ToUpper Then
                SQL &= $"and No_Split = '{Txt_No_Split.Text.Trim}' "
            End If
            SQL &= $"order by No_Transaksi, Tanggal, Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_No_Transaksi.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("No_Split"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("tanggal")) = "", "-", Format(Dr("tanggal"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("jam")) = "", "-", Dr("jam")))
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

            If Not Txt_No_Transaksi.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

                SQL = "select No_Transaksi, No_Split, tanggal, jam "
                SQL &= $"from EMI_Production_Results_Detail_Change_Packaging "
                SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                SQL &= $"and No_Transaksi = '{Txt_No_Transaksi.Text.Trim}' "
                If Txt_No_Split.Text.Trim.ToUpper <> OpsiSeluruh.ToUpper Then
                    SQL &= $"and No_Split = '{Txt_No_Split.Text.Trim}' "
                End If
                SQL &= $"order by No_Transaksi, Tanggal, Jam "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_Transaksi.Text = Dr("No_Transaksi")
                        Txt_No_RM.Focus()
                    Else
                        MessageBox.Show("No Transaksi Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_Transaksi.Text = ""
                        Txt_No_Transaksi.Focus()
                    End If

                    Me.Size = New Size(800, 345)
                    Lv_No_Transaksi.Visible = False
                    Lv_No_Transaksi.Location = New Point(790, 180)
                End Using
            Else
                Txt_No_RM.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_RM_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_RM.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_No_RM.Text.Trim.Length = 0 Then
            Me.Size = New Size(800, 345)
            Lv_No_RM.Visible = False
            Lv_No_RM.Location = New Point(790, 207)
            Txt_No_RM.Text = ""
            Txt_Ket_RM.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(800, 475)
            Lv_No_RM.Location = New Point(150, 207)
            Lv_No_RM.Visible = True
        End If

        Try
            OpenConn()

            Lv_No_RM.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_No_RM.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select No_Faktur, No_Faktur_Order, tanggal, Jam, Keterangan "
            SQL &= $"from Emi_Material_Requisition "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and No_Faktur like '%{Txt_No_RM.Text.Trim}%' "
            If Txt_No_Split.Text.Trim.ToUpper <> OpsiSeluruh.ToUpper Then
                SQL &= $"and No_Faktur_Order = '{Txt_No_Split.Text.Trim}' "
            End If
            SQL &= $"order by No_Faktur, Tanggal, Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_No_RM.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Faktur_Order"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("tanggal")) = "", "-", Format(Dr("tanggal"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("jam")) = "", "-", Dr("jam")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "-", Dr("Keterangan")))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_RM_Leave(sender As Object, e As EventArgs) Handles Txt_No_RM.Leave
        If Txt_No_RM.Text.Trim.Length = 0 Then Exit Sub
        If Lv_No_RM.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_RM.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

                SQL = "select No_Faktur, No_Faktur_Order, tanggal, Jam, Keterangan "
                SQL &= $"from Emi_Material_Requisition "
                SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                SQL &= $"and No_Faktur = '{Txt_No_RM.Text.Trim}' "
                If Txt_No_Split.Text.Trim.ToUpper <> OpsiSeluruh.ToUpper Then
                    SQL &= $"and No_Faktur_Order = '{Txt_No_Split.Text.Trim}' "
                End If
                SQL &= $"order by No_Faktur, Tanggal, Jam "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_RM.Text = Dr("No_Faktur")
                        Txt_Ket_RM.Text = Dr("Keterangan")
                        Cmb_Jenis_Barang.DroppedDown = True
                        Cmb_Jenis_Barang.Focus()
                    Else
                        MessageBox.Show("No Request Material Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_RM.Text = ""
                        Txt_Ket_RM.Text = ""
                        Txt_No_RM.Focus()
                    End If

                    Me.Size = New Size(800, 345)
                    Lv_No_RM.Visible = False
                    Lv_No_RM.Location = New Point(790, 207)
                End Using
            Else
                Cmb_Jenis_Barang.DroppedDown = True
                Cmb_Jenis_Barang.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cmb_Jenis_Barang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Barang.SelectedIndexChanged
        If Cmb_Jenis_Barang.SelectedIndex <> 0 Then
            Txt_Kd_Barang.Enabled = True
            Txt_Nm_Barang.Enabled = True
            Switch_Auto_Complete = True
            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Switch_Auto_Complete = False
            Txt_Kd_Barang.Focus()
        Else
            Txt_Kd_Barang.Enabled = False
            Txt_Nm_Barang.Enabled = False
            Switch_Auto_Complete = True
            Txt_Kd_Barang.Text = OpsiSeluruh
            Txt_Nm_Barang.Text = OpsiSeluruh
            Switch_Auto_Complete = False
        End If


    End Sub

    Private Sub Txt_Kd_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_Kd_Barang.Text.Trim.Length = 0 Then
            Me.Size = New Size(800, 345)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(790, 235)
            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(800, 500)
            Lv_Barang.Location = New Point(150, 235)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Barang like '%" & Txt_Kd_Barang.Text & "%' "
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

    Private Sub Txt_Kd_Barang_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.Leave
        If Txt_Kd_Barang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Barang.Text = OpsiSeluruh Then

                SQL = "select Distinct a.Kode_Barang, a.Nama "
                SQL = SQL & "from barang a "
                SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_Kd_Barang.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kd_Barang.Text = Dr("Kode_Barang")
                        Txt_Nm_Barang.Text = Dr("Nama")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kd_Barang.Text = ""
                        Txt_Nm_Barang.Text = ""
                        Txt_Kd_Barang.Focus()
                    End If

                    Me.Size = New Size(800, 345)
                    Lv_Barang.Visible = False
                    Lv_Barang.Location = New Point(790, 235)
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

    Private Sub Txt_Nm_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nm_Barang.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_Nm_Barang.Text.Trim.Length = 0 Then
            Me.Size = New Size(800, 345)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(790, 235)
            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(800, 500)
            Lv_Barang.Location = New Point(150, 235)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Nama like '%" & Txt_Nm_Barang.Text & "%' "
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

    '================================================================================================================================================
    '=     HANDLE KEYPRESS
    '================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Lokasi.DroppedDown = True
            Cmb_Lokasi.Focus()
        End If
    End Sub

    Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
        If e.KeyChar = Chr(13) Then Txt_No_Split.Focus()
    End Sub

    Private Sub Txt_No_Spli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Split.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_Split.Text.Trim.Length = 0 Then Txt_No_Split.Focus()
            Txt_No_Spli_Leave(Txt_No_Split, e)

            Me.Size = New Size(800, 345)
            Lv_No_Split.Visible = False
            Lv_No_Split.Location = New Point(790, 155)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_Spli_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Split.KeyDown
        If e.KeyCode = Keys.Down Then Lv_No_Split.Focus()
    End Sub

    Private Sub Txt_No_Transaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Transaksi.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_Transaksi.Text.Trim.Length = 0 Then Txt_No_Transaksi.Focus()
            Txt_No_Transaksi_Leave(Txt_No_Transaksi, e)

            Me.Size = New Size(800, 345)
            Lv_No_Transaksi.Visible = False
            Lv_No_Transaksi.Location = New Point(790, 180)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_Transaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Transaksi.KeyDown
        If e.KeyCode = Keys.Down Then Lv_No_Transaksi.Focus()
    End Sub

    Private Sub Txt_No_RM_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_RM.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_RM.Text.Trim.Length = 0 Then Txt_No_RM.Focus()
            Txt_No_RM_Leave(Txt_No_RM, e)

            Me.Size = New Size(800, 345)
            Lv_No_RM.Visible = False
            Lv_No_RM.Location = New Point(790, 207)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_RM_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_RM.KeyDown
        If e.KeyCode = Keys.Down Then Lv_No_RM.Focus()
    End Sub

    Private Sub Txt_Kd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Barang.Text.Trim.Length = 0 Then Txt_Kd_Barang.Focus()
            Txt_Kd_Barang_Leave(Txt_Kd_Barang, e)

            Me.Size = New Size(800, 345)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(790, 235)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Kd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_Nm_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Nm_Barang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kd_Barang_Leave(Txt_Nm_Barang, e)

            Me.Size = New Size(800, 345)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(790, 235)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Nm_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Nm_Barang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    '================================================================================================================================================
    '=     HANDLE BUTTON
    '================================================================================================================================================
    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.Focus() : Exit Sub
        ElseIf Txt_No_Split.Text.Trim.Length = 0 Then
            MessageBox.Show("No Split harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_Split.Focus() : Exit Sub
        ElseIf Txt_No_Transaksi.Text.Trim.Length = 0 Then
            MessageBox.Show("No Transaksi harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_Transaksi.Focus() : Exit Sub
        ElseIf Txt_No_RM.Text.Trim.Length = 0 Then
            MessageBox.Show("No Request Material harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_RM.Focus() : Exit Sub

        ElseIf Cmb_Jenis_Barang.SelectedIndex <> 0 Then
            If Txt_Kd_Barang.Text.Trim.Length = 0 Then
                MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Kd_Barang.Focus() : Exit Sub
            End If

        End If


        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Retur_Packaging "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{N_EMI_View_Laporan_Retur_Packaging.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_View_Laporan_Retur_Packaging.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{N_EMI_View_Laporan_Retur_Packaging.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Cmb_Lokasi.SelectedIndex <> 0 Then
                SQL = SQL & "and kode_stock_owner = '" & Cmb_Lokasi.Text & "' "
                SF = SF & "And {N_EMI_View_Laporan_Retur_Packaging.Kode_Stock_Owner} = '" & Cmb_Lokasi.Text & "'"
            End If

            If Not Txt_No_Split.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
                SQL = SQL & "and No_Split = '" & Txt_No_Split.Text.Trim & "' "
                SF = SF & "And {N_EMI_View_Laporan_Retur_Packaging.No_Split} = '" & Txt_No_Split.Text.Trim & "'"
            End If


            If Not Txt_No_Transaksi.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
                SQL = SQL & "and No_Transaksi = '" & Txt_No_Transaksi.Text.Trim & "' "
                SF = SF & "And {N_EMI_View_Laporan_Retur_Packaging.No_Transaksi} = '" & Txt_No_Transaksi.Text.Trim & "'"
            End If

            If Not Txt_No_RM.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
                SQL = SQL & "and No_Faktur_RM = '" & Txt_No_RM.Text.Trim & "' "
                SF = SF & "And {N_EMI_View_Laporan_Retur_Packaging.No_Faktur_RM} = '" & Txt_No_RM.Text.Trim & "'"
            End If

            If Cmb_Jenis_Barang.SelectedIndex <> 0 Then
                Dim Kolom As String = ArrJenisBarang(Cmb_Jenis_Barang.SelectedIndex).ToString()
                SQL = SQL & "and " & Kolom & " = '" & Txt_Kd_Barang.Text & "' "
                SF = SF & "And {N_EMI_View_Laporan_Retur_Packaging." & Kolom & "} = '" & Txt_Kd_Barang.Text & "'"
            End If

            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New N_EMI_CR_Laporan_Retur_Packaging

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan Retur Packaging"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                    Else

                        CloseConn()
                        MessageBox.Show("Data Retur Packaging Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    '================================================================================================================================================
    '=     HANDLE LISTVIEW
    '================================================================================================================================================
    Private Sub Lv_No_Split_DoubleClick(sender As Object, e As EventArgs) Handles Lv_No_Split.DoubleClick
        If Lv_No_Split.Items.Count = 0 Or Lv_No_Split.FocusedItem.Index = -1 Then Exit Sub

        Dim NoFaktur As String = Lv_No_Split.FocusedItem.SubItems(0).Text

        Switch_Auto_Complete = True
        Txt_No_Split.Text = NoFaktur
        Switch_Auto_Complete = False

        Me.Size = New Size(800, 345)
        Lv_No_Split.Visible = False
        Lv_No_Split.Location = New Point(790, 155)

        Txt_No_Transaksi.Focus()
    End Sub

    Private Sub Lv_No_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_No_Split.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_No_Split_DoubleClick(Lv_No_Split, e)
        End If
    End Sub

    Private Sub Lv_No_Transaksi_DoubleClick(sender As Object, e As EventArgs) Handles Lv_No_Transaksi.DoubleClick
        If Lv_No_Transaksi.Items.Count = 0 Or Lv_No_Transaksi.FocusedItem.Index = -1 Then Exit Sub

        Dim NoFaktur As String = Lv_No_Transaksi.FocusedItem.SubItems(0).Text

        Switch_Auto_Complete = True
        Txt_No_Transaksi.Text = NoFaktur
        Switch_Auto_Complete = False

        Me.Size = New Size(800, 345)
        Lv_No_Transaksi.Visible = False
        Lv_No_Transaksi.Location = New Point(790, 180)

        Txt_No_RM.Focus()
    End Sub

    Private Sub Lv_No_Transaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_No_Transaksi.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_No_Transaksi_DoubleClick(Lv_No_Transaksi, e)
        End If
    End Sub

    Private Sub Lv_No_RM_DoubleClick(sender As Object, e As EventArgs) Handles Lv_No_RM.DoubleClick
        If Lv_No_RM.Items.Count = 0 Or Lv_No_RM.FocusedItem.Index = -1 Then Exit Sub

        Dim NoFaktur As String = Lv_No_RM.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = Lv_No_RM.FocusedItem.SubItems(4).Text

        Switch_Auto_Complete = True
        Txt_No_RM.Text = NoFaktur
        Txt_Ket_RM.Text = Keterangan
        Switch_Auto_Complete = False

        Me.Size = New Size(800, 345)
        Lv_No_RM.Visible = False
        Lv_No_RM.Location = New Point(790, 207)

        Cmb_Jenis_Barang.DroppedDown = True
        Cmb_Jenis_Barang.Focus()
    End Sub

    Private Sub Lv_No_RM_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_No_RM.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_No_RM_DoubleClick(Lv_No_RM, e)
        End If
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Switch_Auto_Complete = True
        Txt_Kd_Barang.Text = KdBarang
        Txt_Nm_Barang.Text = NmBarang
        Switch_Auto_Complete = False

        Me.Size = New Size(800, 345)
        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(790, 235)

        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub


    '================================================================================================================================================
    '=     UTILITY
    '================================================================================================================================================
    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub

End Class
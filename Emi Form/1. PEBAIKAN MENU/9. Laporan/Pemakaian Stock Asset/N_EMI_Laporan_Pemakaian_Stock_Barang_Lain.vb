Public Class N_EMI_Laporan_Pemakaian_Stock_Barang_Lain

    Dim judulForm As String = "Laporan Pngeluaran Stock"

    Dim arrLokasi, arrLain As New ArrayList

    Private Sub N_EMI_Laporan_Pengeluaran_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        kosong
    End Sub

    Private Sub kosong()

        Try
            OpenConn()

            Tgl1.Value = DateTime.Today
            Tgl2.Value = DateTime.Today


            Cmb_Lokasi.Items.Clear() : arrLokasi.Clear()
            Cmb_Lokasi.Items.Add(OpsiSeluruh) : arrLokasi.Add(OpsiSeluruh)
            SQL = "select Kode_Stock_Owner, Keterangan "
            SQL = SQL & "from Stock_Owner_Gudang_Lain "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("Keterangan")) : arrLokasi.Add(Dr("Kode_Stock_Owner"))
                Loop
            End Using
            Cmb_Lokasi.SelectedIndex = 0

            Cmb_JenisPengguna.Items.Clear()
            Cmb_JenisPengguna.Items.Add(OpsiSeluruh)
            Cmb_JenisPengguna.Items.Add("PIC")
            Cmb_JenisPengguna.Items.Add("RUANGAN")
            Cmb_JenisPengguna.SelectedIndex = 0

            Lv_CostCenter.Columns.Clear()
            Lv_CostCenter.Columns.Add("Id Cost Center", 90, HorizontalAlignment.Left)
            Lv_CostCenter.Columns.Add("Cost Center", 370, HorizontalAlignment.Left)
            Lv_CostCenter.View = View.Details

            Lv_Barang.Columns.Clear()
            Lv_Barang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("Nama Barang", 330, HorizontalAlignment.Left)
            Lv_Barang.View = View.Details

            Lv_Pengguna.Columns.Clear()
            Lv_Pengguna.Columns.Add("Id Pengguna", 90, HorizontalAlignment.Left)
            Lv_Pengguna.Columns.Add("Pengguna", 370, HorizontalAlignment.Left)
            Lv_Pengguna.View = View.Details

            Cmb_ParamLain.Items.Clear() : arrLain.Clear()
            Cmb_ParamLain.Items.Add(OpsiSeluruh) : arrLain.Add(OpsiSeluruh)
            Cmb_ParamLain.Items.Add("User") : arrLain.Add("UserID")
            Cmb_ParamLain.SelectedIndex = 0


            Txt_IdCostCenter.Text = "000" : Txt_NmCostCenter.Text = OpsiSeluruh
            Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh

            Lv_CostCenter.Visible = False : Lv_Barang.Visible = False : Lv_Pengguna.Visible = False


            Me.Size = New Size(660, 365)
            Tgl1.Focus()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub




    '=============================================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '=============================================================================================================================================================
    Private Sub Txt_IdCostCenter_TextChanged(sender As Object, e As EventArgs) Handles Txt_IdCostCenter.TextChanged

        If Txt_IdCostCenter.Text.Trim.Length = 0 Then
            Me.Size = New Size(660, 365)
            Lv_CostCenter.Location = New Point(650, 148)
            Lv_CostCenter.Visible = False
            Txt_IdCostCenter.Text = ""
            Txt_NmCostCenter.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 400)
            Lv_CostCenter.Visible = True
            Lv_CostCenter.Location = New Point(125, 148)
        End If

        Try
            OpenConn()

            Lv_CostCenter.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_CostCenter.Items.Add("000")
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Id_Cost_Center, Keterangan from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Cost_Center like '%" & Txt_IdCostCenter.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_CostCenter.Items.Add(Dr("Id_Cost_Center"))
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

    Private Sub Txt_NmCostCenter_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmCostCenter.TextChanged
        If Txt_NmCostCenter.Text.Trim.Length = 0 Then
            Me.Size = New Size(660, 365)
            Lv_CostCenter.Location = New Point(650, 148)
            Lv_CostCenter.Visible = False
            Txt_IdCostCenter.Text = ""
            Txt_NmCostCenter.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 400)
            Lv_CostCenter.Visible = True
            Lv_CostCenter.Location = New Point(125, 148)
        End If

        Try
            OpenConn()

            Lv_CostCenter.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_CostCenter.Items.Add("000")
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Id_Cost_Center, Keterangan from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_NmCostCenter.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_CostCenter.Items.Add(Dr("Id_Cost_Center"))
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
            Me.Size = New Size(660, 365)
            Lv_Barang.Location = New Point(650, 230)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 485)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(125, 230)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from Barang_Lain a, EMI_Group_Jenis_Lain b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            'SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Barang like '%" & Txt_KdBarang.Text & "%' "
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
            Me.Size = New Size(660, 365)
            Lv_Barang.Location = New Point(650, 230)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 485)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(125, 230)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from Barang_Lain a, EMI_Group_Jenis_Lain b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            'SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Nama like '%" & Txt_NmBarang.Text & "%' "
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


    '=============================================================================================================================================================
    '=     HANDLE LEAVE
    '=============================================================================================================================================================
    Private Sub Txt_IdCostCenter_Leave(sender As Object, e As EventArgs) Handles Txt_IdCostCenter.Leave
        If Txt_IdCostCenter.Text.Trim.Length = 0 Then Exit Sub
        If Lv_CostCenter.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_IdCostCenter.Text = "000" Then

                SQL = "select Id_Cost_Center, Keterangan from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Cost_Center = '" & Txt_NmCostCenter.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_IdCostCenter.Text = Dr("Id_Cost_Center")
                        Txt_NmCostCenter.Text = Dr("Keterangan")
                        Cmb_JenisPengguna.DroppedDown = True
                        Cmb_JenisPengguna.Focus()
                    Else
                        MessageBox.Show("Cost Center tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_IdCostCenter.Text = ""
                        Txt_NmCostCenter.Text = ""
                        Txt_IdCostCenter.Focus()
                    End If

                    Me.Size = New Size(660, 365)
                    Lv_CostCenter.Location = New Point(660, 400)
                    Lv_CostCenter.Visible = False
                End Using
            Else
                Cmb_JenisPengguna.DroppedDown = True
                Cmb_JenisPengguna.Focus()
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

                SQL = "select Distinct a.Kode_Barang, a.Nama "
                SQL = SQL & "from Barang_Lain a, Stock_Owner_Gudang_Lain b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NmBarang.Text = Dr("Nama")
                        Cmb_ParamLain.DroppedDown = True
                        Cmb_ParamLain.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Me.Size = New Size(660, 365)
                    Lv_Barang.Location = New Point(650, 230)
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









    '=============================================================================================================================================================
    '=     HANDLE LISTVIEW
    '=============================================================================================================================================================
    Private Sub Lv_CostCenter_DoubleClick(sender As Object, e As EventArgs) Handles Lv_CostCenter.DoubleClick
        If Lv_CostCenter.Items.Count = 0 Or Lv_CostCenter.FocusedItem.Index = -1 Then Exit Sub

        Dim IdCostCenter As String = Lv_CostCenter.FocusedItem.SubItems(0).Text
        Dim NmCostCenter As String = Lv_CostCenter.FocusedItem.SubItems(1).Text

        Txt_IdCostCenter.Text = IdCostCenter
        Txt_NmCostCenter.Text = NmCostCenter

        Me.Size = New Size(660, 365)
        Lv_CostCenter.Location = New Point(650, 148)
        Lv_CostCenter.Visible = False

        Cmb_JenisPengguna.DroppedDown = True
        Cmb_JenisPengguna.Focus()
    End Sub

    Private Sub Lv_CostCenter_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_CostCenter.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_CostCenter_DoubleClick(Lv_CostCenter, e)
        End If
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmKdBarang

        Me.Size = New Size(660, 365)
        Lv_Barang.Location = New Point(650, 230)
        Lv_Barang.Visible = False

        Cmb_ParamLain.DroppedDown = True
        Cmb_ParamLain.Focus()
    End Sub



    '=============================================================================================================================================================
    '=     HANDLE KEYPRESS
    '=============================================================================================================================================================
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
        If e.KeyChar = Chr(13) Then Txt_IdCostCenter.Focus()
    End Sub

    Private Sub Txt_IdCostCenter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_IdCostCenter.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_IdCostCenter.Text.Trim.Length = 0 Then Txt_IdCostCenter.Focus()
            Txt_IdCostCenter_Leave(Txt_IdCostCenter, e)

            Me.Size = New Size(660, 365)
            Lv_CostCenter.Location = New Point(650, 148)
            Lv_CostCenter.Visible = False

            'Txt_KdKategori.Focus()
        End If

        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            Exit Sub
        End If
    End Sub

    Private Sub Txt_IdCostCenter_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_IdCostCenter.KeyDown
        If e.KeyCode = Keys.Down Then Lv_CostCenter.Focus()
    End Sub

    Private Sub Txt_NmCostCenter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmCostCenter.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_IdCostCenter_Leave(Txt_NmCostCenter, e)

            Me.Size = New Size(660, 365)
            Lv_CostCenter.Location = New Point(650, 148)
            Lv_CostCenter.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NmCostCenter_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmCostCenter.KeyDown
        If e.KeyCode = Keys.Down Then Lv_CostCenter.Focus()
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Me.Size = New Size(660, 365)
            Lv_Barang.Location = New Point(650, 230)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Me.Size = New Size(660, 365)
            Lv_Barang.Location = New Point(650, 230)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
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

    Private Sub Cmb_ParamLain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_ParamLain.SelectedIndexChanged
        If Cmb_ParamLain.SelectedIndex = 0 Then
            Txt_ParamLain.Enabled = False
        Else
            Txt_ParamLain.Enabled = True
        End If

        Txt_ParamLain.Text = ""

    End Sub



    Private Sub Txt_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub
    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub





    '=============================================================================================================================================================
    '=     HANDLE BUTTON
    '=============================================================================================================================================================
    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.Focus() : Exit Sub
        ElseIf Txt_IdCostCenter.Text.Trim.Length = 0 Then
            MessageBox.Show("Cost Center harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_IdCostCenter.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        ElseIf Cmb_JenisPengguna.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Pengguna Harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_JenisPengguna.Focus() : Exit Sub
        End If

        If Cmb_ParamLain.SelectedIndex <> 0 Then
            If Txt_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Param Lain harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_ParamLain.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select Kode_Perusahaan from N_EMI_Pemakaian_Stock_Lain_View "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{N_EMI_Pemakaian_Stock_Lain_View.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_Pemakaian_Stock_Lain_View.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{N_EMI_Pemakaian_Stock_Lain_View.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Cmb_Lokasi.SelectedIndex <> 0 Then
                SQL = SQL & "and Kode_Stock_Owner = '" & arrLokasi(Cmb_Lokasi.SelectedIndex) & "' "
                SF = SF & "And {N_EMI_Pemakaian_Stock_Lain_View.Kode_Stock_Owner} = '" & arrLokasi(Cmb_Lokasi.SelectedIndex) & "' "
            End If

            If Not Txt_IdCostCenter.Text.ToUpper = "000" Then
                SQL = SQL & "and Id_Cost_Center = '" & Txt_IdCostCenter.Text & "' "
                SF = SF & "And {N_EMI_Pemakaian_Stock_Lain_View.Id_Cost_Center} = '" & Txt_IdCostCenter.Text & "' "
            End If

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {N_EMI_Pemakaian_Stock_Lain_View.Kode_Barang} = '" & Txt_KdBarang.Text & "' "
            End If

            If Cmb_JenisPengguna.SelectedIndex > 0 Then
                SQL = SQL & "and Jenis_Pengguna = '" & Cmb_JenisPengguna.Text & "' "
                SF = SF & "And {N_EMI_Pemakaian_Stock_Lain_View.Jenis_Pengguna} = '" & Cmb_JenisPengguna.Text & "' "

                If Not Txt_IdPengguna.Text.ToUpper = "000" Then
                    SQL = SQL & "and Id_Pengguna = '" & Txt_IdPengguna.Text & "' "
                    SF = SF & "And {N_EMI_Pemakaian_Stock_Lain_View.Id_Pengguna} = " & Txt_IdPengguna.Text & " "
                End If
            End If


            If Cmb_ParamLain.SelectedIndex <> 0 Then
                SQL = SQL & "and " & arrLain(Cmb_ParamLain.SelectedIndex) & " like '%" & Txt_ParamLain.Text & "%' "
                SF = SF & "And {N_EMI_Pemakaian_Stock_Lain_View." & arrLain(Cmb_ParamLain.SelectedIndex) & "} Like '*" & Txt_ParamLain.Text & "*' "
            End If

            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then



                        Dim CrDoc As New N_EMI_CR_Laporan_Pemakaian_Stock_Barang_Lain

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan Pengeluaran Stock"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                    Else

                        CloseConn()
                        MessageBox.Show("Data Pengeluaran Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub Cmb_JenisPEngguna_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_JenisPengguna.SelectedIndexChanged
        If Cmb_JenisPengguna.SelectedIndex > 0 Then
            Txt_IdPengguna.Enabled = True : Txt_Pengguna.Enabled = True
        Else
            Txt_IdPengguna.Enabled = False : Txt_Pengguna.Enabled = False
        End If
        Txt_IdPengguna.Text = "000" : Txt_Pengguna.Text = OpsiSeluruh

        Lv_Pengguna.Visible = False
        Me.Size = New Size(660, 365)
    End Sub

    Private Sub Txt_IdPengguna_TextChanged(sender As Object, e As EventArgs) Handles Txt_IdPengguna.TextChanged

        If Txt_IdPengguna.Text.Trim.Length = 0 Then
            Me.Size = New Size(660, 365)
            Lv_Pengguna.Location = New Point(650, 205)
            Lv_Pengguna.Visible = False
            Txt_IdPengguna.Text = ""
            Txt_Pengguna.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 460)
            Lv_Pengguna.Visible = True
            Lv_Pengguna.Location = New Point(125, 205)
        End If

        Try
            OpenConn()

            Lv_Pengguna.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pengguna.Items.Add("000")
            Lv.SubItems.Add(OpsiSeluruh)

            If Cmb_JenisPengguna.Text.ToUpper = "PIC" Then

                SQL = "select Id_Karyawan, Nama from Emi_Karyawan where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Karyawan like '%" & Txt_IdPengguna.Text & "%'"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Lv = Lv_Pengguna.Items.Add(Dr("Id_Karyawan"))
                        Lv.SubItems.Add(Dr("Nama"))
                    Loop
                End Using

            ElseIf Cmb_JenisPengguna.Text.ToUpper = "RUANGAN" Then
                SQL = "select ID_Area, Keterangan from N_EMI_Master_Area_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and ID_Area like '%" & Txt_IdPengguna.Text & "%' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Lv = Lv_Pengguna.Items.Add(Dr("ID_Area"))
                        Lv.SubItems.Add(Dr("Keterangan"))
                    Loop
                End Using
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Pengguna_TextChanged(sender As Object, e As EventArgs) Handles Txt_Pengguna.TextChanged
        If Txt_Pengguna.Text.Trim.Length = 0 Then
            Me.Size = New Size(660, 365)
            Lv_Pengguna.Location = New Point(650, 205)
            Lv_Pengguna.Visible = False
            Txt_IdPengguna.Text = ""
            Txt_Pengguna.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(660, 460)
            Lv_Pengguna.Visible = True
            Lv_Pengguna.Location = New Point(125, 205)
        End If

        Try
            OpenConn()

            Lv_Pengguna.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pengguna.Items.Add("000")
            Lv.SubItems.Add(OpsiSeluruh)

            If Cmb_JenisPengguna.Text.ToUpper = "PIC" Then

                SQL = "select Id_Karyawan, Nama from Emi_Karyawan where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_Pengguna.Text & "%'"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Lv = Lv_Pengguna.Items.Add(Dr("Id_Karyawan"))
                        Lv.SubItems.Add(Dr("Nama"))
                    Loop
                End Using

            ElseIf Cmb_JenisPengguna.Text.ToUpper = "RUANGAN" Then
                SQL = "select ID_Area, Keterangan from N_EMI_Master_Area_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_Pengguna.Text & "%' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Lv = Lv_Pengguna.Items.Add(Dr("ID_Area"))
                        Lv.SubItems.Add(Dr("Keterangan"))
                    Loop
                End Using
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_IdPengguna_Leave(sender As Object, e As EventArgs) Handles Txt_IdPengguna.Leave
        If Txt_IdPengguna.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Pengguna.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_IdPengguna.Text = "000" Then

                If Cmb_JenisPengguna.Text.ToUpper = "PIC" Then

                    SQL = "select Id_Karyawan, Nama from Emi_Karyawan where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Karyawan = '" & Txt_IdPengguna.Text & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Txt_IdPengguna.Text = Dr("Id_Karyawan")
                            Txt_Pengguna.Text = Dr("Nama")
                            Txt_KdBarang.Focus()
                        Else
                            MessageBox.Show("Pengguna Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Txt_IdPengguna.Text = ""
                            Txt_Pengguna.Text = ""
                            Txt_IdPengguna.Focus()
                        End If
                    End Using

                ElseIf Cmb_JenisPengguna.Text.ToUpper = "RUANGAN" Then
                    SQL = "select ID_Area, Keterangan from N_EMI_Master_Area_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and ID_Area = '" & Txt_IdPengguna.Text & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Txt_IdPengguna.Text = Dr("ID_Area")
                            Txt_Pengguna.Text = Dr("Keterangan")
                            Txt_KdBarang.Focus()
                        Else
                            MessageBox.Show("Pengguna Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Txt_IdPengguna.Text = ""
                            Txt_Pengguna.Text = ""
                            Txt_IdPengguna.Focus()
                        End If
                    End Using
                End If

                Me.Size = New Size(660, 365)
                Lv_Pengguna.Location = New Point(660, 205)
                Lv_Pengguna.Visible = False

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

    Private Sub Txt_IdPengguna_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_IdPengguna.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_IdPengguna.Text.Trim.Length = 0 Then Txt_IdPengguna.Focus()
            Txt_IdPengguna_Leave(Txt_IdPengguna, e)

            Me.Size = New Size(660, 365)
            Lv_Pengguna.Location = New Point(650, 148)
            Lv_Pengguna.Visible = False

            'Txt_KdKategori.Focus()
        End If

        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            Exit Sub
        End If
    End Sub

    Private Sub Txt_IdPengguna_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_IdPengguna.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pengguna.Focus()
    End Sub

    Private Sub Txt_Pengguna_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Pengguna.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_IdPengguna_Leave(Txt_Pengguna, e)

            Me.Size = New Size(660, 365)
            Lv_Pengguna.Location = New Point(650, 205)
            Lv_Pengguna.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Pengguna_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Pengguna.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pengguna.Focus()
    End Sub

    Private Sub Lv_Pengguna_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Pengguna.DoubleClick
        If Lv_Pengguna.Items.Count = 0 Or Lv_Pengguna.FocusedItem.Index = -1 Then Exit Sub

        Dim IdPengguna As String = Lv_Pengguna.FocusedItem.SubItems(0).Text
        Dim NamPengguna As String = Lv_Pengguna.FocusedItem.SubItems(1).Text

        Txt_IdPengguna.Text = IdPengguna
        Txt_Pengguna.Text = NamPengguna

        Me.Size = New Size(660, 365)
        Lv_Pengguna.Location = New Point(650, 205)
        Lv_Pengguna.Visible = False

        Txt_KdBarang.Focus()
    End Sub

    Private Sub Lv_Pengguna_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Pengguna.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Pengguna_DoubleClick(Lv_Pengguna, e)
        End If
    End Sub

    Private Sub Cmb_JenisPengguna_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_JenisPengguna.KeyPress
        If e.KeyChar = Chr(13) Then Txt_IdPengguna.Focus()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub






End Class
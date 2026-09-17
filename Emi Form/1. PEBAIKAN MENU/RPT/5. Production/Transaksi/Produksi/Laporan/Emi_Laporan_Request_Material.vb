Public Class Emi_Laporan_Request_Material

    Dim JudulForm As String = "Laporan Request Material"

    Dim arrParamLain, arrParamLainSF As New ArrayList


    Private Sub Emi_Laporan_Request_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()
    End Sub



    Private Sub Kosong()

        Tgl1.Value = Now : Tgl2.Value = Now

        Txt_KdGudang.Text = "--- SELURUH ---" : Txt_NmGudang.Text = "--- SELURUH ---"
        Txt_KdBarang.Text = "--- SELURUH ---" : Txt_NmBarang.Text = "--- SELURUH ---"

        Lv_Gudang.Columns.Clear()
        Lv_Gudang.Columns.Add("Kode Lokasi Gudang", 150, HorizontalAlignment.Left)
        Lv_Gudang.Columns.Add("Lokasi Gudang", 350, HorizontalAlignment.Left)
        Lv_Gudang.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details


        Cmb_ParamLain.Items.Clear() : arrParamLain.Clear() : arrParamLainSF.Clear()
        Cmb_ParamLain.Items.Add("--- SELURUH ---") : arrParamLain.Add("--- SELURUH ---") : arrParamLainSF.Add("--- SELURUH ---")
        Cmb_ParamLain.Items.Add("User ID") : arrParamLain.Add("UserId") : arrParamLainSF.Add("{View_Laporan_Request_Material.UserId}")
        Cmb_ParamLain.SelectedIndex = 0
        Txt_ParamLain.Text = ""

        Lv_Gudang.Visible = False
        Lv_Barang.Visible = False

        Me.Size = New Size(640, 300)

    End Sub




    '============================================================================================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '============================================================================================================================================================================================================

    Private Sub Txt_KdGudang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdGudang.TextChanged

        If Txt_KdGudang.Text.Trim.Length = 0 Then
            Me.Size = New Size(640, 300)
            Lv_Gudang.Location = New Point(650, 137)
            Lv_Gudang.Visible = False
            Txt_KdGudang.Text = ""
            Txt_NmGudang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(640, 390)
            Lv_Gudang.Visible = True
            Lv_Gudang.Location = New Point(124, 137)
        End If


        Try
            OpenConn()

            Lv_Gudang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Gudang.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Stock_Owner, Keterangan from Stock_Owner_Gudang where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner like '%" & Txt_KdGudang.Text & "%' "
            SQL = SQL & "order by Kode_Stock_Owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Gudang.Items.Add(Dr("Kode_Stock_Owner"))
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

    Private Sub Txt_NmGudang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmGudang.TextChanged

        If Txt_NmGudang.Text.Trim.Length = 0 Then
            Me.Size = New Size(640, 300)
            Lv_Gudang.Location = New Point(650, 137)
            Lv_Gudang.Visible = False
            Txt_KdGudang.Text = ""
            Txt_NmGudang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(640, 390)
            Lv_Gudang.Visible = True
            Lv_Gudang.Location = New Point(124, 137)
        End If


        Try
            OpenConn()

            Lv_Gudang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Gudang.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Stock_Owner, Keterangan from Stock_Owner_Gudang where kode_perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_NmGudang.Text & "%' "
            SQL = SQL & "order by Kode_Stock_Owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Gudang.Items.Add(Dr("Kode_Stock_Owner"))
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
            Me.Size = New Size(640, 300)
            Lv_Barang.Location = New Point(650, 168)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(640, 420)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(124, 168)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang "
            SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis  "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang  "
            SQL = SQL & "and a.Kode_Barang like '%" & Txt_KdBarang.Text & "%' "
            SQL = SQL & "and b.Flag_Produksi = 'Y' "
            SQL = SQL & "and aktif = 'Y' "
            SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang  "
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
            Me.Size = New Size(640, 300)
            Lv_Barang.Location = New Point(650, 168)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(640, 420)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(124, 168)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select a.Kode_Barang, a.Nama, a.Satuan, c.lokasi_gudang "
            SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis  "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang  "
            SQL = SQL & "and a.Nama like '%" & Txt_NmBarang.Text & "%' "
            SQL = SQL & "and b.Flag_Produksi = 'Y' "
            SQL = SQL & "and aktif = 'Y' "
            SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang  "
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
    Private Sub Txt_KdGudang_Leave(sender As Object, e As EventArgs) Handles Txt_KdGudang.Leave
        If Txt_KdGudang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Gudang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdGudang.Text = "--- SELURUH ---" Then

                SQL = "select Kode_Stock_Owner, Keterangan from Stock_Owner_Gudang where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Txt_KdGudang.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdGudang.Text = Dr("Kode_Stock_Owner")
                        Txt_NmGudang.Text = Dr("Keterangan")
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("Gudang tidak ditemukan . . ! !", Judul)
                        Txt_KdGudang.Text = ""
                        Txt_NmGudang.Text = ""
                        Txt_KdGudang.Focus()
                    End If

                    Me.Size = New Size(640, 300)
                    Lv_Gudang.Location = New Point(650, 137)
                    Lv_Gudang.Visible = False
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
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NmBarang.Text = Dr("Nama")
                        Cmb_ParamLain.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Me.Size = New Size(640, 300)
                    Lv_Barang.Location = New Point(650, 168)
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









    '============================================================================================================================================================================================================
    '=     HANDLE LV
    '============================================================================================================================================================================================================
    Private Sub Lv_Gudang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Gudang.DoubleClick
        If Lv_Gudang.Items.Count = 0 Or Lv_Gudang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdGudang As String = Lv_Gudang.FocusedItem.SubItems(0).Text
        Dim NmGudang As String = Lv_Gudang.FocusedItem.SubItems(1).Text

        Txt_KdGudang.Text = KdGudang
        Txt_NmGudang.Text = NmGudang

        Me.Size = New Size(640, 300)
        Lv_Gudang.Location = New Point(650, 137)
        Lv_Gudang.Visible = False

        Txt_KdBarang.Focus()
    End Sub
    Private Sub Lv_Gudang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Gudang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Gudang_DoubleClick(Lv_Gudang, e)
        End If
    End Sub
    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmBarang

        Me.Size = New Size(640, 300)
        Lv_Barang.Location = New Point(650, 168)
        Lv_Barang.Visible = False

        Cmb_ParamLain.Focus()
    End Sub
    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub




    '============================================================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '============================================================================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdGudang.Focus()
    End Sub

    Private Sub Txt_KdGudang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdGudang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Gudang.Focus()
    End Sub
    Private Sub Txt_NmGudang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmGudang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Gudang.Focus()
    End Sub
    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub
    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_KdGudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdGudang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdGudang.Text.Trim.Length = 0 Then Txt_KdGudang.Focus()
            Txt_KdGudang_Leave(Txt_KdGudang, e)

            Me.Size = New Size(640, 300)
            Lv_Gudang.Location = New Point(650, 137)
            Lv_Gudang.Visible = False

            'Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Txt_NmGudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmGudang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdGudang_Leave(Txt_NmGudang, e)

            Me.Size = New Size(640, 300)
            Lv_Gudang.Location = New Point(650, 137)
            Lv_Gudang.Visible = False

            'Txt_KdBarang.Focus()
        End If
    End Sub


    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Me.Size = New Size(640, 300)
            Lv_Barang.Location = New Point(650, 168)
            Lv_Barang.Visible = False

            'Cmb_ParamLain.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)


            Me.Size = New Size(640, 300)
            Lv_Barang.Location = New Point(650, 168)
            Lv_Barang.Visible = False

            'Cmb_ParamLain.Focus()
        End If
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

    Private Sub Cmb_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub Txt_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub





    '============================================================================================================================================================================================================
    '=     HANDLE BUTTON
    '============================================================================================================================================================================================================
    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_KdGudang.Text.Trim.Length = 0 Then
            MessageBox.Show("Gudang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdGudang.Focus() : Exit Sub
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

            SQL = "select kode_perusahaan from View_Laporan_Request_Material "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{View_Laporan_Request_Material.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {View_Laporan_Request_Material.tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{View_Laporan_Request_Material.tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_KdGudang.Text = "--- SELURUH ---" Then
                SQL = SQL & "and Kode_Stock_Owner_Tujuan = '" & Txt_KdGudang.Text & "' "
                SF = "{View_Laporan_Request_Material.Kode_Stock_Owner_Tujuan} = '" & Txt_KdGudang.Text & "' "
            End If

            If Not Txt_KdBarang.Text = "--- SELURUH ---" Then
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {View_Laporan_Request_Material.Kode_Barang} = '" & Txt_KdBarang.Text & "'"
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

                        Dim CrDoc As New Rpt_Laporan_Request_Material

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
                        MessageBox.Show("Request Material Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
Public Class N_EMI_Laporan_Pemakaian_Bahan_Baku


    Dim Switch_Auto_Complete As Boolean = False

    Private Sub N_EMI_Laporan_Pemakaian_Bahan_Baku_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_PO.Columns.Clear()
        Lv_PO.Columns.Add("No PO", 130, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("Keterangan", 220, HorizontalAlignment.Left)
        Lv_PO.View = View.Details

        Lv_Split.Columns.Clear()
        Lv_Split.Columns.Add("No Split", 130, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Split.Columns.Add("Keterangan", 220, HorizontalAlignment.Left)
        Lv_Split.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Bahan", 130, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Bahan", 280, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details


        Kosong()

    End Sub


    Private Sub Kosong()

        Switch_Auto_Complete = True
        Tgl1.Value = Date.Now : Tgl2.Value = Date.Now
        Txt_No_PO.Text = OpsiSeluruh : Txt_Keterangan_PO.Text = OpsiSeluruh
        Txt_No_Split.Text = OpsiSeluruh : Txt_Keterangan_Split.Text = OpsiSeluruh
        Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh
        Switch_Auto_Complete = False

        Me.Size = New Size(665, 289)


        Tgl1.Focus()

    End Sub

    Private Sub Txt_No_PO_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_PO.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_No_PO.Text.Trim.Length = 0 Then
            Me.Size = New Size(665, 289)
            Lv_PO.Visible = False
            Lv_PO.Location = New Point(650, 127)
            Txt_No_PO.Text = ""
            Txt_Keterangan_PO.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(665, 382)
            Lv_PO.Location = New Point(125, 127)
            Lv_PO.Visible = True
        End If

        Try
            OpenConn()

            Lv_PO.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_PO.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
                select No_Faktur, Tanggal, Keterangan
                from Emi_Order_Produksi
                where Kode_Perusahaan = '{KodePerusahaan}'
                and status is null
                and No_Faktur like '%{Txt_No_PO.Text}%'
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_PO.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
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

    Private Sub Txt_No_PO_Leave(sender As Object, e As EventArgs) Handles Txt_No_PO.Leave
        If Txt_No_PO.Text.Trim.Length = 0 Then Exit Sub
        If Lv_PO.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_PO.Text = OpsiSeluruh Then

                SQL = $"
                    select No_Faktur, Tanggal, Keterangan
                    from Emi_Order_Produksi
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and No_Faktur = '{Txt_No_PO.Text}'
                "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_PO.Text = Dr("No_Faktur")
                        Txt_Keterangan_PO.Text = Dr("Keterangan")
                        Txt_No_Split.Focus()
                    Else
                        MessageBox.Show("No PO tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_PO.Text = ""
                        Txt_Keterangan_PO.Text = ""
                        Txt_No_PO.Focus()
                    End If

                    Me.Size = New Size(665, 289)
                    Lv_PO.Visible = False
                    Lv_PO.Location = New Point(650, 127)
                End Using
            Else
                Txt_No_Split.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_PO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_PO.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_PO.Text.Trim.Length = 0 Then Txt_No_PO.Focus()
            Txt_No_PO_Leave(Txt_No_PO, e)

            Me.Size = New Size(665, 289)
            Lv_PO.Visible = False
            Lv_PO.Location = New Point(650, 127)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_PO_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_PO.KeyDown
        If e.KeyCode = Keys.Down Then Lv_PO.Focus()
    End Sub

    Private Sub Lv_PO_DoubleClick(sender As Object, e As EventArgs) Handles Lv_PO.DoubleClick
        If Lv_PO.Items.Count = 0 Or Lv_PO.FocusedItem.Index = -1 Then Exit Sub

        Dim NoPO As String = Lv_PO.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = Lv_PO.FocusedItem.SubItems(2).Text

        Txt_No_PO.Text = NoPO
        Txt_Keterangan_PO.Text = Keterangan

        Me.Size = New Size(665, 289)
        Lv_PO.Visible = False
        Lv_PO.Location = New Point(650, 127)

        Txt_No_Split.Focus()
    End Sub

    Private Sub Lv_PO_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_PO.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_PO_DoubleClick(Lv_PO, e)
        End If
    End Sub

    Private Sub Txt_No_Split_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Split.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_No_Split.Text.Trim.Length = 0 Then
            Me.Size = New Size(665, 289)
            Lv_Split.Visible = False
            Lv_Split.Location = New Point(650, 154)
            Txt_No_Split.Text = ""
            Txt_Keterangan_Split.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(665, 415)
            Lv_Split.Location = New Point(125, 154)
            Lv_Split.Visible = True
        End If

        Try
            OpenConn()

            Lv_Split.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Split.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
                select No_Transaksi, Tanggal, No_Batch
                from Emi_Split_Production_Order a
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.status is null
                and No_Transaksi like '%{Txt_No_Split.Text}%'
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Split.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("No_Batch"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Split_Leave(sender As Object, e As EventArgs) Handles Txt_No_Split.Leave
        If Txt_No_Split.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Split.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_Split.Text = OpsiSeluruh Then

                SQL = $"
                    select No_Transaksi, Tanggal, No_Batch
                    from Emi_Split_Production_Order a
                    where a.Kode_Perusahaan = '{KodePerusahaan}'
                    and a.status is null
                    and No_Transaksi = '{Txt_No_Split.Text}'
                "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_Split.Text = Dr("No_Transaksi")
                        Txt_Keterangan_Split.Text = Dr("No_Batch")
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("No Split tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_Split.Text = ""
                        Txt_Keterangan_Split.Text = ""
                        Txt_No_Split.Focus()
                    End If

                    Me.Size = New Size(665, 289)
                    Lv_Split.Visible = False
                    Lv_Split.Location = New Point(650, 154)
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

    Private Sub Txt_No_Split_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Split.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_Split.Text.Trim.Length = 0 Then Txt_No_Split.Focus()
            Txt_No_Split_Leave(Txt_No_Split, e)

            Me.Size = New Size(665, 289)
            Lv_Split.Visible = False
            Lv_Split.Location = New Point(650, 154)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Split.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Split.Focus()
    End Sub

    Private Sub Lv_Split_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Split.DoubleClick
        If Lv_Split.Items.Count = 0 Or Lv_Split.FocusedItem.Index = -1 Then Exit Sub

        Dim NoSplit As String = Lv_Split.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = Lv_Split.FocusedItem.SubItems(2).Text

        Txt_No_Split.Text = NoSplit
        Txt_Keterangan_Split.Text = Keterangan

        Me.Size = New Size(665, 289)
        Lv_Split.Visible = False
        Lv_Split.Location = New Point(650, 154)

        Txt_KdBarang.Focus()
    End Sub

    Private Sub Lv_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Split.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Split_DoubleClick(Lv_Split, e)
        End If
    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Switch_Auto_Complete Then Exit Sub
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(665, 289)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(650, 180)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(665, 437)
            Lv_Barang.Location = New Point(125, 180)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, EMI_Group_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
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

    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
        If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdBarang.Text = OpsiSeluruh Then

                SQL = "select Distinct a.Kode_Barang, a.Nama "
                SQL = SQL & "from barang a, EMI_Group_Jenis b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NmBarang.Text = Dr("Nama")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Me.Size = New Size(645, 310)
                    Lv_Barang.Location = New Point(650, 180)
                    Lv_Barang.Visible = False
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

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Me.Size = New Size(665, 289)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(650, 180)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Switch_Auto_Complete Then Exit Sub
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(665, 289)
            Lv_Barang.Location = New Point(650, 180)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(665, 437)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(125, 180)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, EMI_Group_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
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

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Me.Size = New Size(665, 289)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(650, 180)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmKdBarang

        Me.Size = New Size(665, 289)
        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(650, 180)

        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_No_PO.Text.Trim.Length = 0 Then
            MessageBox.Show("No PO harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_PO.Focus() : Exit Sub
        ElseIf Txt_No_Split.Text.Trim.Length = 0 Then
            MessageBox.Show("No Split harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_Split.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Pemakaian_Bahan_Baku "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{N_EMI_View_Laporan_Pemakaian_Bahan_Baku.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_View_Laporan_Pemakaian_Bahan_Baku.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{N_EMI_View_Laporan_Pemakaian_Bahan_Baku.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_No_PO.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and No_PO = '" & Txt_No_PO.Text & "' "
                SF = SF & "And {N_EMI_View_Laporan_Pemakaian_Bahan_Baku.No_PO} = '" & Txt_No_PO.Text & "'"
            End If

            If Not Txt_No_Split.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and No_Split = '" & Txt_No_Split.Text & "' "
                SF = SF & "And {N_EMI_View_Laporan_Pemakaian_Bahan_Baku.No_Split} = '" & Txt_No_Split.Text & "'"
            End If

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {N_EMI_View_Laporan_Pemakaian_Bahan_Baku.Kode_Barang} = '" & Txt_KdBarang.Text & "'"
            End If

            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New N_EMI_CR_Laporan_Pemakaian_Bahan_Baku

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan Pemakaian Bahan Baku"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                    Else

                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_No_PO.Focus()
    End Sub
End Class
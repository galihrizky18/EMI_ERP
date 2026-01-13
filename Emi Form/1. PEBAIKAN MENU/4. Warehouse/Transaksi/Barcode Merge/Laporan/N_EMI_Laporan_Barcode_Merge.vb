Public Class N_EMI_Laporan_Barcode_Merge


    Dim arrLokasi As New ArrayList

    Dim switchAutoComplete As Boolean = False

    Private Sub N_EMI_Laporan_Barcode_Merge_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_NoFaktur.Columns.Clear()
        Lv_NoFaktur.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_NoFaktur.Columns.Add("Tanggal", 110, HorizontalAlignment.Left)
        Lv_NoFaktur.Columns.Add("Jam", 90, HorizontalAlignment.Left)
        Lv_NoFaktur.Columns.Add("Keterangan", 150, HorizontalAlignment.Left)
        Lv_NoFaktur.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 330, HorizontalAlignment.Left)

        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear() : arrLokasi.Clear()
            Cmb_Lokasi.Items.Add(OpsiSeluruh) : arrLokasi.Add(OpsiSeluruh)
            SQL = "select Kode_Stock_Owner, Keterangan "
            SQL = SQL & "from Stock_Owner_Gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("Keterangan")) : arrLokasi.Add(Dr("Kode_Stock_Owner"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Lv_Barang.View = View.Details


        Kosong()

    End Sub

    Private Sub Kosong()

        Tgl1.Value = DateTime.Today
        Tgl2.Value = DateTime.Today

        Cmb_Lokasi.SelectedIndex = 0

        switchAutoComplete = True
        Txt_No_Faktur.Text = OpsiSeluruh
        Txt_Ket_Faktur.Text = OpsiSeluruh
        Txt_KdBarang.Text = OpsiSeluruh
        Txt_NmBarang.Text = OpsiSeluruh
        switchAutoComplete = False


        Me.Size = New Size(648, 275)
        Tgl1.Focus()

    End Sub

    Private Sub Txt_No_Faktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Faktur.TextChanged
        If switchAutoComplete Then Exit Sub

        If Txt_No_Faktur.Text.Trim.Length = 0 Then
            Me.Size = New Size(648, 275)
            Lv_NoFaktur.Visible = False
            Lv_NoFaktur.Location = New Point(638, 151)
            Txt_No_Faktur.Text = ""
            Txt_Ket_Faktur.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(645, 408)
            Lv_NoFaktur.Visible = True
            Lv_NoFaktur.Location = New Point(114, 151)
        End If

        Try
            OpenConn()

            Lv_NoFaktur.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_NoFaktur.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select no_faktur, tanggal, jam, Keterangan from N_EMI_Transaksi_Barcode_Merge "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and status is null "
            SQL &= $"and No_Faktur like '%{Txt_No_Faktur.Text.Trim}%' "
            SQL &= $"order by No_Faktur, tanggal, Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_NoFaktur.Items.Add(Dr("no_faktur"))
                    Lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("jam"))
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

    Private Sub Txt_No_Faktur_Leave(sender As Object, e As EventArgs) Handles Txt_No_Faktur.Leave
        If Txt_No_Faktur.Text.Trim.Length = 0 Then Exit Sub
        If Lv_NoFaktur.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_Faktur.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

                SQL = "select no_faktur, tanggal, jam, Keterangan from N_EMI_Transaksi_Barcode_Merge "
                SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                SQL &= $"and status is null "
                SQL &= $"and No_Faktur = '{Txt_No_Faktur.Text.Trim}' "
                SQL &= $"order by No_Faktur, tanggal, Jam "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_Faktur.Text = Dr("no_faktur")
                        Txt_Ket_Faktur.Text = Dr("Keterangan")
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("No Transaksi ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_Faktur.Text = ""
                        Txt_Ket_Faktur.Text = ""
                        Txt_No_Faktur.Focus()
                    End If

                    Me.Size = New Size(648, 275)
                    Lv_NoFaktur.Visible = False
                    Lv_NoFaktur.Location = New Point(638, 151)
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

    Private Sub Txt_No_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Faktur.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_Faktur.Text.Trim.Length = 0 Then Txt_No_Faktur.Focus()
            Txt_No_Faktur_Leave(Txt_No_Faktur, e)

            Me.Size = New Size(648, 275)
            Lv_NoFaktur.Visible = False
            Lv_NoFaktur.Location = New Point(638, 151)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Faktur.KeyDown
        If e.KeyCode = Keys.Down Then Lv_NoFaktur.Focus()
    End Sub

    Private Sub Lv_NoFaktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_NoFaktur.DoubleClick
        If Lv_NoFaktur.Items.Count = 0 Or Lv_NoFaktur.FocusedItem.Index = -1 Then Exit Sub

        Dim NoFaktur As String = Lv_NoFaktur.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = Lv_NoFaktur.FocusedItem.SubItems(3).Text

        switchAutoComplete = True
        Txt_No_Faktur.Text = NoFaktur
        Txt_Ket_Faktur.Text = Keterangan
        switchAutoComplete = False

        Me.Size = New Size(648, 275)
        Lv_NoFaktur.Visible = False
        Lv_NoFaktur.Location = New Point(638, 151)

        Txt_KdBarang.Focus()
    End Sub

    Private Sub Lv_NoFaktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_NoFaktur.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_NoFaktur_DoubleClick(Lv_NoFaktur, e)
        End If
    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If switchAutoComplete Then Exit Sub

        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(638, 275)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(638, 178)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(638, 440)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(114, 178)
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

                    Me.Size = New Size(638, 275)
                    Lv_Barang.Visible = False
                    Lv_Barang.Location = New Point(638, 178)
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

            Me.Size = New Size(638, 275)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(638, 178)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If switchAutoComplete Then Exit Sub

        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(638, 275)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(638, 178)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(638, 440)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(114, 178)
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

            Me.Size = New Size(638, 275)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(638, 178)

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

        switchAutoComplete = True
        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmKdBarang
        switchAutoComplete = False

        Me.Size = New Size(638, 275)
        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(638, 178)

        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

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
        If e.KeyChar = Chr(13) Then Txt_No_Faktur.Focus()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.Focus() : Exit Sub
        ElseIf Txt_No_Faktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No Faktur harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_Faktur.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If


        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Barcode_Merge "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{N_EMI_View_Laporan_Barcode_Merge.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_View_Laporan_Barcode_Merge.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{N_EMI_View_Laporan_Barcode_Merge.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Cmb_Lokasi.SelectedIndex <> 0 Then
                SQL = SQL & "and Kode_Stock_Owner = '" & arrLokasi(Cmb_Lokasi.SelectedIndex) & "' "
                SF = SF & "And {N_EMI_View_Laporan_Barcode_Merge.Kode_Stock_Owner} = '" & arrLokasi(Cmb_Lokasi.SelectedIndex) & "'"
            End If

            If Not Txt_No_Faktur.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
                SQL = SQL & "and No_Faktur = '" & Txt_No_Faktur.Text.Trim & "' "
                SF = SF & "And {N_EMI_View_Laporan_Barcode_Merge.No_Faktur} = '" & Txt_No_Faktur.Text.Trim & "'"
            End If

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {N_EMI_View_Laporan_Barcode_Merge.Kode_Barang} = '" & Txt_KdBarang.Text & "'"
            End If

            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New N_EMI_CR_Laporan_Barcode_Merge

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan Penggabungan Barcode"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                    Else

                        CloseConn()
                        MessageBox.Show("Data Penggabungan Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

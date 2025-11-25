Public Class Hr_Cetak_Saldo_Akhir
    Dim CrDoc As Object

    Private Sub Hr_Cetak_Saldo_Akhir_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        CmbLokasi.Focus()
    End Sub

    Private Sub Hr_Cetak_Saldo_Akhir_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            CmbLokasi.Items.Clear()
            CmbLokasi.Items.Add("Seluruh")
            SQL = "select kode_stock_owner from stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "'"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    CmbLokasi.Items.Add(Dr("kode_stock_owner"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CmbLokasi.SelectedIndex = 0

        CmbBulan.Items.Clear()
        CmbBulan.Items.Add("Januari")
        CmbBulan.Items.Add("Februari")
        CmbBulan.Items.Add("Maret")
        CmbBulan.Items.Add("April")
        CmbBulan.Items.Add("Mei")
        CmbBulan.Items.Add("Juni")
        CmbBulan.Items.Add("Juli")
        CmbBulan.Items.Add("Agustus")
        CmbBulan.Items.Add("September")
        CmbBulan.Items.Add("Oktober")
        CmbBulan.Items.Add("November")
        CmbBulan.Items.Add("Desember")
        CmbBulan.SelectedIndex = CInt(Format(Now.Date, "MM")) - 1

        CmbTahun.Items.Clear()
        For i As Integer = 2017 To 2030
            CmbTahun.Items.Add(i)
        Next
        For i As Integer = 0 To CmbTahun.Items.Count - 1
            If CmbTahun.Items(i).ToString = Format(Now.Date, "yyyy") Then
                CmbTahun.SelectedIndex = i
                Exit For
            End If
        Next

        CmbJenis.Items.Clear()
        CmbJenis.Items.Add("Rekap")
        CmbJenis.Items.Add("Rekap Per Barang")
        CmbJenis.Items.Add("Detail")

        Cmb_FlagInspection.Items.Clear()
        Cmb_FlagInspection.Items.Add(OpsiSeluruh)
        Cmb_FlagInspection.Items.Add("Inspection")
        Cmb_FlagInspection.Items.Add("Release")
        Cmb_FlagInspection.SelectedIndex = 0

        Lv_GroupJenis.Columns.Clear() : Lv_GroupJenis.Items.Clear()
        Lv_GroupJenis.Columns.Add("ID", 80, HorizontalAlignment.Center)
        Lv_GroupJenis.Columns.Add("Group Jeis", 180, HorizontalAlignment.Left)
        Lv_GroupJenis.View = View.Details

        Txt_IdGroupJenis.Text = "000" : Txt_NmGroupJenis.Text = "Seluruh"

        Lv_GroupJenis.Visible = False

        Me.Size = New Size(512, 342)

        CmbLokasi.DroppedDown = True
        CmbLokasi.Focus()
    End Sub

    Private Sub CmbLokasi_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbLokasi.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbBulan.DroppedDown = True
            CmbBulan.Focus()
        End If
    End Sub

    Private Sub CmbBulan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbBulan.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbTahun.DroppedDown = True
            CmbTahun.Focus()
        End If
    End Sub

    Private Sub CmbTahun_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbTahun.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbJenis.DroppedDown = True
            CmbJenis.Focus()
        End If
    End Sub

    Private Sub CmbJenis_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbJenis.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_FlagInspection.DroppedDown = True
            Cmb_FlagInspection.Focus()
        End If
    End Sub
    Private Sub Cmb_FlagInspection_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_FlagInspection.KeyPress
        If e.KeyChar = Chr(13) Then Txt_IdGroupJenis.Focus()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_Cetak.Click
        If CmbLokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbLokasi.Focus() : Exit Sub
        ElseIf CmbBulan.SelectedIndex = -1 Then
            MessageBox.Show("Bulan harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbBulan.Focus() : Exit Sub
        ElseIf CmbTahun.SelectedIndex = -1 Then
            MessageBox.Show("Tahun harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbTahun.Focus() : Exit Sub
        ElseIf CmbJenis.SelectedIndex = -1 Then
            MessageBox.Show("Jenis laporan harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbJenis.Focus() : Exit Sub
        ElseIf Cmb_FlagInspection.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Inspection harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbJenis.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select top 1 kode_stock_owner "
            SQL = SQL & "from get_saldo_barang "

            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SF = "{Get_Saldo_Barang.Kode_Perusahaan} = '" & KodePerusahaan & "' "

            If CmbLokasi.SelectedIndex > 0 Then
                SQL = SQL & "and Kode_stock_owner = '" & CmbLokasi.Text & "' "
                SF &= "and {Get_Saldo_Barang.Kode_stock_owner} = '" & CmbLokasi.Text & "' "
            End If

            If Not Txt_IdGroupJenis.Text = "000" Then
                SQL = SQL & "and Id_Group_Jenis = " & Txt_IdGroupJenis.Text & " "
                SF &= "and {Get_Saldo_Barang.Id_Group_Jenis} = " & Txt_IdGroupJenis.Text & " "
            End If

            If Cmb_FlagInspection.SelectedIndex > 0 Then
                If Cmb_FlagInspection.SelectedIndex = 1 Then
                    SQL = SQL & "and (Flag_FG = 'T' OR "
                    SQL = SQL & "(Flag_FG = 'Y' AND Flag_QI = 'Y'))"
                    SF &= "and {Get_Saldo_Barang.Flag_FG} = 'T' or "
                    SF &= "({Get_Saldo_Barang.Flag_FG} = 'Y' and {Get_Saldo_Barang.Flag_QI} = 'Y') "
                ElseIf Cmb_FlagInspection.SelectedIndex = 2 Then
                    SQL = SQL & "and (Flag_FG = 'T' OR "
                    SQL = SQL & "(Flag_FG = 'Y' AND Flag_QI = 'T'))"
                    SF &= "and {Get_Saldo_Barang.Flag_FG} = 'T' or "
                    SF &= "({Get_Saldo_Barang.Flag_FG} = 'Y' and {Get_Saldo_Barang.Flag_QI} = 'T') "
                End If
            End If

            SQL = SQL & "and tahun = '" & Strings.Right("0" & CmbBulan.SelectedIndex + 1, 2) & CmbTahun.Text & "' "
            SF &= "and {Get_Saldo_Barang.Tahun} = '" & Strings.Right("0" & CmbBulan.SelectedIndex + 1, 2) & CmbTahun.Text & "' "

            Using Ds = Binding(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    If CmbJenis.SelectedIndex = 0 Then
                        CrDoc = New Hr_Cetak_Saldo_Akhir_Rpt_Rekap
                    ElseIf CmbJenis.SelectedIndex = 1 Then
                        CrDoc = New N_Emi_CR_Laporan_Cetak_Saldo_Akhir_Rekap_Per_Barang
                    ElseIf CmbJenis.SelectedIndex = 2 Then
                        CrDoc = New Hr_Cetak_Saldo_Akhir_Rpt_Detail
                    Else
                        CloseConn()
                        MessageBox.Show("Jenis laporan salah diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CmbJenis.Focus() : Exit Sub
                    End If

                    With A_Place_For_Printing
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = SF
                        CrDoc.SummaryInfo.ReportTitle = "Lokasi : " & CmbLokasi.Text & Chr(13) &
                                                            "Bulan Tahun : " & CmbBulan.Text & " " & CmbTahun.Text
                        .Text = "Laporan Saldo Stock Barang"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                        .Focus()
                    End With
                Else
                    MessageBox.Show("Data tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Btn_Exit.Click
        Me.Close()
    End Sub

    Private Sub Txt_NoSJ_TextChanged(sender As Object, e As EventArgs) Handles Txt_IdGroupJenis.TextChanged
        If Txt_IdGroupJenis.Text.Trim.Length = 0 Then
            Me.Size = New Size(512, 342)
            Lv_GroupJenis.Location = New Point(500, 237)
            Lv_GroupJenis.Visible = False
            Txt_IdGroupJenis.Text = ""
            Txt_NmGroupJenis.Text = ""
        Else
            Me.Size = New Size(512, 463)
            Lv_GroupJenis.Location = New Point(135, 237)
            Lv_GroupJenis.Visible = True
        End If

        Try
            OpenConn()

            Lv_GroupJenis.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_GroupJenis.Items.Add("000")
            Lv.SubItems.Add("Seluruh")

            SQL = "select Id_Group_Jenis, Kode_Group_Jenis from EMI_Group_Jenis where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Group_Jenis like '%" & Txt_IdGroupJenis.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Lv = Lv_GroupJenis.Items.Add(Dr("Id_Group_Jenis"))
                    Lv.SubItems.Add(Dr("Kode_Group_Jenis"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Txt_NmGroupJenis_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmGroupJenis.TextChanged
        If Txt_NmGroupJenis.Text.Trim.Length = 0 Then
            Me.Size = New Size(512, 342)
            Lv_GroupJenis.Location = New Point(500, 237)
            Lv_GroupJenis.Visible = False
            Txt_IdGroupJenis.Text = ""
            Txt_NmGroupJenis.Text = ""
        Else
            Me.Size = New Size(512, 463)
            Lv_GroupJenis.Location = New Point(135, 237)
            Lv_GroupJenis.Visible = True
        End If

        Try
            OpenConn()

            Lv_GroupJenis.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_GroupJenis.Items.Add("000")
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Id_Group_Jenis, Kode_Group_Jenis from EMI_Group_Jenis where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Group_Jenis like '%" & Txt_NmGroupJenis.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Lv = Lv_GroupJenis.Items.Add(Dr("Id_Group_Jenis"))
                    Lv.SubItems.Add(Dr("Kode_Group_Jenis"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_IdGroupJenis_Leave(sender As Object, e As EventArgs) Handles Txt_IdGroupJenis.Leave
        If Txt_IdGroupJenis.Text.Trim.Length = 0 Then Exit Sub
        If Lv_GroupJenis.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_IdGroupJenis.Text.ToUpper = "000" Then

                SQL = "select Id_Group_Jenis, Kode_Group_Jenis from EMI_Group_Jenis where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Group_Jenis = " & Txt_IdGroupJenis.Text & " "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Txt_IdGroupJenis.Text = Dr("Id_Group_Jenis")
                        Txt_NmGroupJenis.Text = Dr("Kode_Group_Jenis")

                        Btn_Cetak.Focus()
                    Else
                        MessageBox.Show("Group Jenis tidak ditemukan . . ! !", Judul)
                        Txt_IdGroupJenis.Text = "" : Txt_NmGroupJenis.Text = ""
                        Txt_IdGroupJenis.Focus()
                    End If

                    Me.Size = New Size(512, 342)
                    Lv_GroupJenis.Location = New Point(500, 237)
                    Lv_GroupJenis.Visible = False

                End Using
            Else
                Btn_Cetak.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_GroupJenis_DoubleClick(sender As Object, e As EventArgs) Handles Lv_GroupJenis.DoubleClick
        If Lv_GroupJenis.Items.Count = 0 Or Lv_GroupJenis.FocusedItem.Index = -1 Then Exit Sub

        Dim IdGroupJeis As String = Lv_GroupJenis.FocusedItem.SubItems(0).Text
        Dim NmGroupJeis As String = Lv_GroupJenis.FocusedItem.SubItems(1).Text

        Txt_IdGroupJenis.Text = IdGroupJeis
        Txt_NmGroupJenis.Text = NmGroupJeis

        Me.Size = New Size(512, 342)
        Lv_GroupJenis.Location = New Point(600, 230)
        Lv_GroupJenis.Visible = False

        Btn_Cetak.Focus()
    End Sub

    Private Sub Lv_GroupJenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_GroupJenis.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_GroupJenis_DoubleClick(Lv_GroupJenis, e)
        End If
    End Sub

    '======================================================================================================
    '=     HANDLE KEYPRESS
    '======================================================================================================
    Private Sub Txt_IdGroupJenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_IdGroupJenis.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_IdGroupJenis.Text.Trim.Length = 0 Then Txt_IdGroupJenis.Focus()
            Txt_IdGroupJenis_Leave(Txt_IdGroupJenis, e)
            Me.Size = New Size(512, 342)
            Lv_GroupJenis.Location = New Point(500, 237)
            Lv_GroupJenis.Visible = False
        End If
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Txt_IdGroupJenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_IdGroupJenis.KeyDown
        If e.KeyCode = Keys.Down Then Lv_GroupJenis.Focus()
    End Sub

    Private Sub Txt_NmGroupJenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmGroupJenis.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_IdGroupJenis_Leave(Txt_NmGroupJenis, e)
            Me.Size = New Size(512, 342)
            Lv_GroupJenis.Location = New Point(500, 237)
            Lv_GroupJenis.Visible = False

            'BtnCetak.Focus()
        End If
    End Sub

    Private Sub Txt_NmGroupJenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmGroupJenis.KeyDown
        If e.KeyCode = Keys.Down Then Lv_GroupJenis.Focus()
    End Sub


End Class
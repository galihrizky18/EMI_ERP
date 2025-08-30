Public Class N_EMI_Laporan_List_Karyawan_Per_Tahapan


    Dim SwitchAutoComplete As Boolean

    Dim arrStatusCalon, arrTahapan, arrDivisi, arrJabatan, arrLevel As New ArrayList

    Dim SelectedFaktur As String

    Private Sub N_EMI_Laporan_List_Karyawan_Per_Tahapan_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Calon.Columns.Clear()
        Lv_Calon.Columns.Add("NoFaktur", 0, HorizontalAlignment.Left)
        Lv_Calon.Columns.Add("Kode Calon", 150, HorizontalAlignment.Left)
        Lv_Calon.Columns.Add("Calon Karyawan", 400, HorizontalAlignment.Left)
        Lv_Calon.View = View.Details


        Cmb_StatusCalon.Items.Clear() : arrStatusCalon.Clear()
        Cmb_StatusCalon.Items.Add(OpsiSeluruh) : arrStatusCalon.Add(OpsiSeluruh)
        Cmb_StatusCalon.Items.Add("Selesai") : arrStatusCalon.Add("Selesai")
        Cmb_StatusCalon.Items.Add("Di Tolak") : arrStatusCalon.Add("Di Tolak")
        Cmb_StatusCalon.Items.Add("Sedang Proses") : arrStatusCalon.Add("Sedang Proses")
        Cmb_StatusCalon.SelectedIndex = 0


        Try
            OpenConn()

            Cmb_Tahapan.Items.Clear() : arrTahapan.Clear()
            Cmb_Tahapan.Items.Add(OpsiSeluruh) : arrTahapan.Add(OpsiSeluruh)
            SQL = "select Id_Tahapan, Keterangan from HRIS_Tahapan where Kode_Perusahaan ='" & KodePerusahaan & "' and Aktif = 'Y' order by Order_Tahapan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Tahapan.Items.Add(Dr("Keterangan")) : arrTahapan.Add(Dr("Id_Tahapan"))
                Loop
            End Using
            Cmb_Tahapan.SelectedIndex = 0


            Cmb_Divisi.Items.Clear() : arrDivisi.Clear()
            Cmb_Divisi.Items.Add(OpsiSeluruh) : arrDivisi.Add(OpsiSeluruh)
            SQL = "select ID_Divisi, Keterangan from HRIS_Divisi where Kode_Perusahaan = '" & KodePerusahaan & "' order by ID_Divisi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Divisi.Items.Add(Dr("Keterangan")) : arrDivisi.Add(Dr("ID_Divisi"))
                Loop
            End Using
            Cmb_Divisi.SelectedIndex = 0

            Cmb_Jabatan.Items.Clear() : arrJabatan.Clear()
            Cmb_Jabatan.Items.Add(OpsiSeluruh) : arrJabatan.Add(OpsiSeluruh)
            SQL = "select ID_Jabatan, Keterangan from HRIS_Jabatan where Kode_Perusahaan = '" & KodePerusahaan & "' order by ID_Jabatan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Jabatan.Items.Add(Dr("Keterangan")) : arrJabatan.Add(Dr("ID_Jabatan"))
                Loop
            End Using
            Cmb_Jabatan.SelectedIndex = 0


            Cmb_Level.Items.Clear() : arrLevel.Clear()
            Cmb_Level.Items.Add(OpsiSeluruh) : arrLevel.Add(OpsiSeluruh)
            SQL = "select ID_Level, Keterangan from HRIS_Level where Kode_Perusahaan = '" & KodePerusahaan & "' order by ID_Level "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Level.Items.Add(Dr("Keterangan")) : arrLevel.Add(Dr("ID_Level"))
                Loop
            End Using
            Cmb_Level.SelectedIndex = 0


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        Kosong()
    End Sub

    Private Sub Kosong()

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        Cmb_StatusCalon.SelectedIndex = 0
        Cmb_Tahapan.SelectedIndex = 0
        Cmb_Divisi.SelectedIndex = 0
        Cmb_Jabatan.SelectedIndex = 0
        Cmb_Level.SelectedIndex = 0

        SwitchAutoComplete = False
        Txt_Kode_Calon.Text = OpsiSeluruh : Text_Nama_Calon.Text = OpsiSeluruh
        SwitchAutoComplete = True
        SelectedFaktur = ""




    End Sub

    Private Sub Txt_Kode_Calon_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kode_Calon.TextChanged
        If SwitchAutoComplete = False Then Exit Sub
        If Txt_Kode_Calon.Text.Trim.Length = 0 Then
            Me.Size = New Size(790, 328)
            Lv_Calon.Location = New Point(782, 446)
            Lv_Calon.Visible = False
            Txt_Kode_Calon.Text = ""
            Text_Nama_Calon.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(790, 446)
            Lv_Calon.Visible = True
            Lv_Calon.Location = New Point(137, 192)
        End If

        Try
            OpenConn()

            Lv_Calon.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Calon.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select No_Faktur, Kode_Calon, Nama from HRIS_Rekrutmen_Karyawan where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Calon like '%" & Txt_Kode_Calon.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Calon.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Kode_Calon"))
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

    Private Sub Text_Nama_Calon_TextChanged(sender As Object, e As EventArgs) Handles Text_Nama_Calon.TextChanged
        If SwitchAutoComplete = False Then Exit Sub
        If Text_Nama_Calon.Text.Trim.Length = 0 Then
            Me.Size = New Size(790, 328)
            Lv_Calon.Location = New Point(782, 446)
            Lv_Calon.Visible = False
            Txt_Kode_Calon.Text = ""
            Text_Nama_Calon.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(790, 446)
            Lv_Calon.Visible = True
            Lv_Calon.Location = New Point(137, 192)
        End If

        Try
            OpenConn()

            Lv_Calon.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Calon.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select No_Faktur, Kode_Calon, Nama from HRIS_Rekrutmen_Karyawan where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Nama like '%" & Text_Nama_Calon.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Calon.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Kode_Calon"))
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

    Private Sub Txt_Kode_Calon_Leave(sender As Object, e As EventArgs) Handles Txt_Kode_Calon.Leave
        If Txt_Kode_Calon.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Calon.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kode_Calon.Text = OpsiSeluruh Then

                SQL = "select No_Faktur, Kode_Calon, Nama from HRIS_Rekrutmen_Karyawan where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Calon = '" & Txt_Kode_Calon.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kode_Calon.Text = Dr("Kode_Calon")
                        Text_Nama_Calon.Text = Dr("Nama")

                        SelectedFaktur = Dr("No_Faktur")
                        Cmb_Divisi.DroppedDown = True
                        Cmb_Divisi.Focus()
                    Else
                        MessageBox.Show("Calon Karyawan tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kode_Calon.Text = ""
                        Text_Nama_Calon.Text = ""

                        SelectedFaktur = ""
                        Txt_Kode_Calon.Focus()
                    End If

                    Me.Size = New Size(790, 328)
                    Lv_Calon.Visible = False
                    Lv_Calon.Location = New Point(782, 192)
                End Using
            Else
                SelectedFaktur = OpsiSeluruh
                Cmb_Divisi.DroppedDown = True
                Cmb_Divisi.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Lv_Calon_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Calon.DoubleClick
        If Lv_Calon.Items.Count = 0 Or Lv_Calon.FocusedItem.Index = -1 Then Exit Sub

        Dim NoFaktur As String = Lv_Calon.FocusedItem.SubItems(0).Text
        Dim KodeCalon As String = Lv_Calon.FocusedItem.SubItems(1).Text
        Dim Calon As String = Lv_Calon.FocusedItem.SubItems(2).Text

        SelectedFaktur = NoFaktur
        Txt_Kode_Calon.Text = KodeCalon
        Text_Nama_Calon.Text = Calon

        Me.Size = New Size(790, 328)
        Lv_Calon.Visible = False
        Lv_Calon.Location = New Point(782, 192)

        Cmb_Divisi.DroppedDown = True
        Cmb_Divisi.Focus()
    End Sub

    Private Sub Lv_Calon_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Calon.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Calon_DoubleClick(Lv_Calon, e)
        End If
    End Sub



    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click

        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Cmb_StatusCalon.SelectedIndex = -1 Then
            MessageBox.Show("Status Calon Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_StatusCalon.Focus() : Exit Sub
        ElseIf Cmb_Tahapan.SelectedIndex = -1 Then
            MessageBox.Show("Tahapan Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Tahapan.Focus() : Exit Sub
        ElseIf Cmb_Divisi.SelectedIndex = -1 Then
            MessageBox.Show("Divisi Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Divisi.Focus() : Exit Sub
        ElseIf Cmb_Jabatan.SelectedIndex = -1 Then
            MessageBox.Show("Jabatan Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jabatan.Focus() : Exit Sub
        ElseIf Cmb_Level.SelectedIndex = -1 Then
            MessageBox.Show("Level Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Level.Focus() : Exit Sub


        ElseIf Txt_Kode_Calon.Text.Trim.Length = 0 Then
            MessageBox.Show("Pelamar harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kode_Calon.Focus() : Exit Sub
        End If


        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select Kode_Perusahaan from N_EMI_View_List_Karyawan_Per_Tahapan "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{N_EMI_View_List_Karyawan_Per_Tahapan.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_View_List_Karyawan_Per_Tahapan.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{N_EMI_View_List_Karyawan_Per_Tahapan.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Cmb_StatusCalon.SelectedIndex <> 0 Then
                SQL = SQL & "and Status = '" & arrStatusCalon(Cmb_StatusCalon.SelectedIndex) & "' "
                SF = SF & "And {N_EMI_View_List_Karyawan_Per_Tahapan.Status} = '" & arrStatusCalon(Cmb_StatusCalon.SelectedIndex) & "' "
            End If

            If Cmb_Tahapan.SelectedIndex <> 0 Then
                SQL = SQL & "and ID_Tahapan = '" & arrTahapan(Cmb_Tahapan.SelectedIndex) & "' "
                SF = SF & "And {N_EMI_View_List_Karyawan_Per_Tahapan.ID_Tahapan} = '" & arrTahapan(Cmb_Tahapan.SelectedIndex) & "' "
            End If

            If Cmb_Divisi.SelectedIndex <> 0 Then
                SQL = SQL & "and ID_Divisi = '" & arrDivisi(Cmb_Divisi.SelectedIndex) & "' "
                SF = SF & "And {N_EMI_View_List_Karyawan_Per_Tahapan.ID_Divisi} = " & arrDivisi(Cmb_Divisi.SelectedIndex) & " "
            End If

            If Cmb_Jabatan.SelectedIndex <> 0 Then
                SQL = SQL & "and Id_Jabatan = '" & arrJabatan(Cmb_Jabatan.SelectedIndex) & "' "
                SF = SF & "And {N_EMI_View_List_Karyawan_Per_Tahapan.Id_Jabatan} = " & arrJabatan(Cmb_Jabatan.SelectedIndex) & " "
            End If


            If Cmb_Level.SelectedIndex <> 0 Then
                SQL = SQL & "and ID_Level = '" & arrLevel(Cmb_Level.SelectedIndex) & "' "
                SF = SF & "And {N_EMI_View_List_Karyawan_Per_Tahapan.ID_Level} = " & arrLevel(Cmb_Level.SelectedIndex) & " "
            End If


            If Not Txt_Kode_Calon.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and No_Faktur = '" & SelectedFaktur & "' "
                SF = SF & "And {N_EMI_View_List_Karyawan_Per_Tahapan.No_Faktur} = '" & SelectedFaktur & "'"
            End If

            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New N_EMI_CR_Laporan_List_Karyawan_Per_Tahapan

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan List Karyawan Per Tahapan"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                    Else

                        CloseConn()
                        MessageBox.Show("Data Karyawan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    '=============================================================================================================================================================
    '=     HANDLE KEYPRESS
    '=============================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_StatusCalon.DroppedDown = True
            Cmb_StatusCalon.Focus()
        End If
    End Sub

    Private Sub Cmb_StatusCalon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_StatusCalon.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Tahapan.DroppedDown = True
            Cmb_Tahapan.Focus()
        End If
    End Sub

    Private Sub Cmb_Tahapan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tahapan.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kode_Calon.Focus()
        End If
    End Sub



    Private Sub Cmb_Divisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Divisi.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Jabatan.DroppedDown = True
            Cmb_Jabatan.Focus()
        End If
    End Sub



    Private Sub Cmb_Jabatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jabatan.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Level.DroppedDown = True
            Cmb_Level.Focus()
        End If
    End Sub



    Private Sub Cmb_Level_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Level.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub



    Private Sub Txt_Kode_Calon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kode_Calon.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kode_Calon.Text.Trim.Length = 0 Then Txt_Kode_Calon.Focus()
            Txt_Kode_Calon_Leave(Txt_Kode_Calon, e)

            Me.Size = New Size(790, 328)
            Lv_Calon.Visible = False
            Lv_Calon.Location = New Point(782, 192)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_Kode_Calon_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kode_Calon.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Calon.Focus()
    End Sub
    Private Sub Text_Nama_Calon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Text_Nama_Calon.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kode_Calon_Leave(Text_Nama_Calon, e)

            Me.Size = New Size(790, 328)
            Lv_Calon.Visible = False
            Lv_Calon.Location = New Point(782, 192)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Text_Nama_Calon_KeyDown(sender As Object, e As KeyEventArgs) Handles Text_Nama_Calon.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Calon.Focus()
    End Sub
End Class
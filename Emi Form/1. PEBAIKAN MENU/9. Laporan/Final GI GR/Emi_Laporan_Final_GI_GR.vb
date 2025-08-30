Imports excel = Microsoft.Office.Interop.Excel


Public Class Emi_Laporan_Final_GI_GR

    Dim judulForm As String = "Laporan Final GI GR"

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Emi_Laporan_Final_GI_GR_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'My.Application.ChangeCulture("en-us")
        'My.Application.ChangeUICulture("en-us")

        'My.Application.ChangeCulture("id-ID")
        'My.Application.ChangeUICulture("id-ID")
    End Sub


    Private Sub Emi_Laporan_Final_GI_GR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'My.Application.ChangeCulture("en-us")
        'My.Application.ChangeUICulture("en-us")

        'My.Application.ChangeCulture("id-ID")
        'My.Application.ChangeUICulture("id-ID")

        Kosong()
    End Sub

    Private Sub Kosong()

        Tgl1.Value = DateTime.Today
        Tgl2.Value = DateTime.Today

        Lv_Routing.Columns.Clear()
        Lv_Routing.Columns.Add("Id Routing", 150, HorizontalAlignment.Center)
        Lv_Routing.Columns.Add("Nama Routing", 260, HorizontalAlignment.Center)
        Lv_Routing.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 260, HorizontalAlignment.Center)
        Lv_Barang.View = View.Details

        Txt_IdRouting.Text = OpsiSeluruh : Txt_NmRouting.Text = OpsiSeluruh
        Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh

        Lv_Routing.Visible = False : Lv_Barang.Visible = False

        Me.Size = New Size(620, 270)

    End Sub

    '=============================================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '=============================================================================================================================================================
    Private Sub Txt_IdRouting_TextChanged(sender As Object, e As EventArgs) Handles Txt_IdRouting.TextChanged
        If Txt_IdRouting.Text.Trim.Length = 0 Then
            Me.Size = New Size(620, 270)
            Lv_Routing.Location = New Point(650, 139)
            Lv_Routing.Visible = False
            Txt_IdRouting.Text = ""
            Txt_NmRouting.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(620, 390)
            Lv_Routing.Visible = True
            Lv_Routing.Location = New Point(136, 139)
        End If

        Try
            OpenConn()

            Lv_Routing.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Routing.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Id_Routing, Keterangan from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Routing like '%" & Txt_IdRouting.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Routing.Items.Add(Dr("Id_Routing"))
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

    Private Sub Txt_NmRouting_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmRouting.TextChanged
        If Txt_NmRouting.Text.Trim.Length = 0 Then
            Me.Size = New Size(620, 270)
            Lv_Routing.Location = New Point(650, 139)
            Lv_Routing.Visible = False
            Txt_IdRouting.Text = ""
            Txt_NmRouting.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(620, 390)
            Lv_Routing.Visible = True
            Lv_Routing.Location = New Point(136, 139)
        End If

        Try
            OpenConn()

            Lv_Routing.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Routing.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Id_Routing, Keterangan from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_NmRouting.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Routing.Items.Add(Dr("Id_Routing"))
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
            Me.Size = New Size(620, 270)
            Lv_Barang.Location = New Point(650, 168)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(620, 425)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(136, 168)
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
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
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
            Me.Size = New Size(620, 270)
            Lv_Barang.Location = New Point(650, 168)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(620, 425)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(136, 168)
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
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
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
    Private Sub Txt_IdRouting_Leave(sender As Object, e As EventArgs) Handles Txt_IdRouting.Leave
        If Txt_IdRouting.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Routing.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_IdRouting.Text = OpsiSeluruh Then

                SQL = "select Kode_Perusahaan_Biaya_Import, Nama from Perusahaan_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Perusahaan_Biaya_Import = '" & Txt_IdRouting.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_IdRouting.Text = Dr("Kode_Perusahaan_Biaya_Import")
                        Txt_NmRouting.Text = Dr("Nama")
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("Routing tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_IdRouting.Text = ""
                        Txt_NmRouting.Text = ""
                        Txt_IdRouting.Focus()
                    End If

                    Me.Size = New Size(620, 270)
                    Lv_Routing.Location = New Point(650, 139)
                    Lv_Routing.Visible = False
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

            If Not Txt_KdBarang.Text = OpsiSeluruh Then

                SQL = "select Distinct a.Kode_Barang, a.Nama "
                SQL = SQL & "from barang a, EMI_Group_Jenis b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
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

                    Me.Size = New Size(620, 270)
                    Lv_Barang.Location = New Point(650, 168)
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

    '=============================================================================================================================================================
    '     HANDLE LV
    '=============================================================================================================================================================
    Private Sub Lv_Routing_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Routing.DoubleClick
        If Lv_Routing.Items.Count = 0 Or Lv_Routing.FocusedItem.Index = -1 Then Exit Sub

        Dim IdRouting As String = Lv_Routing.FocusedItem.SubItems(0).Text
        Dim NmRouting As String = Lv_Routing.FocusedItem.SubItems(1).Text

        Txt_IdRouting.Text = IdRouting
        Txt_NmRouting.Text = NmRouting

        Me.Size = New Size(620, 270)
        Lv_Routing.Location = New Point(650, 139)
        Lv_Routing.Visible = False

        Txt_KdBarang.Focus()
    End Sub

    Private Sub Lv_Routing_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Routing.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Routing_DoubleClick(Lv_Routing, e)
        End If
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim Nmbarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = Nmbarang

        Me.Size = New Size(620, 270)
        Lv_Barang.Location = New Point(650, 168)
        Lv_Barang.Visible = False

        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    '=============================================================================================================================================================
    '=     HANDLE KEY PRESS
    '=============================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_IdRouting.Focus()
    End Sub

    Private Sub Txt_IdRouting_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_IdRouting.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_IdRouting.Text.Trim.Length = 0 Then Txt_IdRouting.Focus()
            Txt_IdRouting_Leave(Txt_IdRouting, e)

            Me.Size = New Size(620, 270)
            Lv_Routing.Location = New Point(650, 139)
            Lv_Routing.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_IdRouting_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_IdRouting.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Routing.Focus()
    End Sub

    Private Sub Txt_NmRouting_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmRouting.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_IdRouting_Leave(Txt_NmRouting, e)

            Me.Size = New Size(620, 270)
            Lv_Routing.Location = New Point(650, 139)
            Lv_Routing.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NmRouting_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmRouting.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Routing.Focus()
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Me.Size = New Size(620, 270)
            Lv_Barang.Location = New Point(650, 168)
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

            Me.Size = New Size(620, 270)
            Lv_Barang.Location = New Point(650, 168)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    '=============================================================================================================================================================
    '=     HANDLE BUTTON
    '=============================================================================================================================================================
    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
            MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_IdRouting.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select No_PO, no_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, satuan, batch, Berat_GI, Jumlah_Dosing, NilaiGR1_Pcs, NilaiGR1_KG, ScrapGR1_KG, TotalGR1_KG, Loss_Production, Loss_Production_Persen, Persen_WasteGR1, WaktuGR1, "
            SQL = SQL & "NilaiGR2_Pcs, NilaiGR2_KG, ScrapGR2_KG, TotalGR2_KG, Persen_WasteGR2, WaktuGR2, NilaiAfterGR_Pcs, NilaiAfterGR_KG, Persen_WasteGR3, WaktuGR3, NilaiGRFinal_Pcs, NilaiGRFinal_KG, ScrapGRFinal_KG, Loss_Production_Final_GR, Loss_Production_Final_GR_Persen, Total_Waste "
            SQL = SQL & "from Laporan_Akhir_GIGR "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{Laporan_Akhir_GIGR.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {Laporan_Akhir_GIGR.Tgl_Produksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{Laporan_Akhir_GIGR.Tgl_Produksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
                SF = SF & "And {Laporan_Akhir_GIGR.Id_Routing} = '" & Txt_IdRouting.Text & "'"
            End If

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {Laporan_Akhir_GIGR.Kode_Barang} = '" & Txt_KdBarang.Text & "' "
            End If
            SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        'Dim CrDoc As New Rpt_Laporan_Final_GI_GR

                        'CrDoc.SetDataSource(DS)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                        '                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                        'CrDoc.RecordSelectionFormula = SF

                        'With A_Place_For_Printing2
                        '    .Text = "Laporan Final GI GR"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                        '    .Refresh()
                        '    .Show()
                        'End With

                        Generate_Excel(SQL)

                    Else

                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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


    Private Sub Generate_Excel(ByVal sql As String)
        Try

            get_jam()

            Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

            '=======================================
            '=     CEK APAKAH EXCEL TERINSTALL     =
            '=======================================
            If xlApp Is Nothing Then
                MessageBox.Show("Excel is not properly installed!!")
                Return
            End If

            Dim JudulLaporan As String = "LAPORAN FINAL GI GR"

            Dim xlWorkBook As excel.Workbook
            Dim xlWorkSheet As excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

            'If System.IO.Directory.Exists(lokasi_file) = False Then
            '    System.IO.Directory.CreateDirectory(lokasi_file)
            'End If
            Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
            Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("Sheet1")

            '==================================
            '=     DEFINISIKAN NAMA KOLOM     =
            '==================================
#Region "Generate Coloms"

            Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jumlah"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Satuan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Batch"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Berat (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}}, 'Mulai GR 1
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}}, 'Mulai Val
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Good Received Rejected (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}}, 'Mulai Rejected
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Good Received Rejected (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}}, 'Mulai FINAL GR
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}}
            }

            Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
                {"Default", New Dictionary(Of String, Object) From {
                    {"Default", "Default"},
                    {"Kolom", 0}
                }}
            }

            For i As Integer = 0 To dataKoloms.Count - 1
                Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

                If kolom("Identifier") = "Main" Then

                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "GR1" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Line Production (Good Received Lv I)"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Val" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Quality Inspection (Good Received Lv II)"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Rejected" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Good Received Rejected"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Final Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                End If

            Next

#End Region


            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {3, 4, 9, 10, 20, 26, 30}

            Dim numberColumn As New List(Of Integer) From {8, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23, 24, 25, 27, 28, 29, 31, 32, 33, 34, 35, 36}

            Dim defaultRowIndex As Integer = 5
            Try
                OpenConn()

                ' Ambil format sesuai culture
                Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
                xlApp.UseSystemSeparators = True

                '==  AMBIL SEPARATOR DARI EXCEL =='
                'Dim decimalSep As String = xlApp.DecimalSeparator
                'Dim groupSep As String = xlApp.ThousandsSeparator

                '==  AMBIL SEPARATOR DARI SISTEM =='
                Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
                Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

                If decimalSep = "," Then
                    decimalSep = "."
                ElseIf decimalSep = "." Then
                    decimalSep = ","
                End If

                If groupSep = "." Then
                    groupSep = ","
                ElseIf groupSep = "," Then
                    groupSep = "."
                End If

                Dim templateFormat As String = "#GROUP##0DEC0000"
                Dim excelFormat As String = templateFormat _
                    .Replace("GROUP", groupSep) _
                    .Replace("DEC", decimalSep)





                Dim row As Integer = 0
                'sql = "select No_PO, no_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, satuan, batch, Jumlah_Dosing, NilaiGR1_Pcs, NilaiGR1_KG, ScrapGR1_KG, Loss_Production, Persen_WasteGR1, "
                'sql = sql & "NilaiGR2_Pcs, NilaiGR2_KG, ScrapGR2_KG, Persen_WasteGR2, NilaiAfterGR_Pcs, NilaiAfterGR_KG, Persen_WasteGR3, NilaiGRFinal_Pcs, NilaiGRFinal_KG, Total_Waste "
                'sql = sql & "from Laporan_Akhir_GIGR "
                'sql = sql & "where Kode_Perusahaan = '001' "
                'sql = sql & "and Tgl_Produksi between '2022-12-20 00:00:00.000' and '2030-12-20 00:00:00.000' "
                Using Ds = BindingTrans(sql)
                    With Ds.Tables("MyTable")

                        For i As Integer = 0 To .Rows.Count - 1

                            For colIndex As Integer = 0 To .Columns.Count - 1
                                Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                                If colIndex = 6 Then
                                    cell.NumberFormat = "@"
                                End If

                                cell.Value = General_Class.CekNULL(.Rows(i).Item(colIndex))

                                cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                                ' Format numerik (N4)
                                If numberColumn.Contains(colIndex) Then
                                    Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))

                                    cell.NumberFormat = excelFormat
                                    cell.Value = nilai

                                End If





                                '== ATUR ALIGMENT CELL =='
                                Select Case .Columns(colIndex).DataType.Name
                                    Case "String"
                                        cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignLeft)
                                    Case "DateTime"
                                        cell.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                                        cell.Value = Format(CDate(.Rows(i).Item(colIndex)), "dd MMM yyyy")
                                    Case "Int32", "Double"
                                        cell.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
                                        cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignRight)
                                End Select

                                ' BORDER
                                With cell.Borders
                                    .LineStyle = excel.XlLineStyle.xlContinuous
                                    .ColorIndex = 0
                                    .Weight = excel.XlBorderWeight.xlThin
                                End With

                                ' BG COLOR
                                Select Case colIndex
                                    Case 13 To 20
                                        If .Columns(colIndex).ColumnName = "WaktuGR1" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(252, 105, 108))
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                        End If
                                    Case 21 To 26
                                        If .Columns(colIndex).ColumnName = "WaktuGR2" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(181, 230, 162))
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                        End If
                                    Case 27 To 30
                                        If .Columns(colIndex).ColumnName = "WaktuGR3" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                        End If
                                    Case 31 To 36
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                                End Select

                                xlWorkSheet.Cells(1, 1).Interior.TintAndShade = 0.2

                            Next

                            row += 1

                        Next

                    End With
                End Using

                ' AutoFit kolom setelah semua data dimasukkan
                xlWorkSheet.Columns.AutoFit()

                '==========================
                '=     HEADER LAPORAN     =
                '==========================
                Dim panjangKolom As Integer = dataKoloms.Count

                xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

                xlWorkSheet.Cells(1, 1).Value = JudulLaporan
                xlWorkSheet.Cells(1, 1).Font.Size = 14
                xlWorkSheet.Cells(1, 1).Font.Bold = True
                xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()

                '==========================
                '=     FOOTER LAPORAN     =
                '==========================
                Dim jumlahRows As Integer = row + defaultRowIndex

                Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

                xlWorkSheet.Cells(jumlahRows + 1, 1).Value = Footer
                xlWorkSheet.Cells(jumlahRows + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(jumlahRows + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



            '=====================
            '=     SAVE FILE     =
            '=====================
            Dim saveFileDialog As New SaveFileDialog()

            ' Set File Filter
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
            saveFileDialog.Title = "Save As"


            'Tampilkan Show Dialog Save as
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim filePath As String = saveFileDialog.FileName

                    xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

                    'MessageBox.Show("File berhasil disimpan di: " & filePath)

                    ' Menutup workbook dan aplikasi Excel
                    xlWorkBook.Close()
                    xlApp.Quit()

                    ' Membebaskan objek Excel
                    releaseObject(xlWorkSheet)
                    releaseObject(xlWorkBook)
                    releaseObject(xlApp)

                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


End Class
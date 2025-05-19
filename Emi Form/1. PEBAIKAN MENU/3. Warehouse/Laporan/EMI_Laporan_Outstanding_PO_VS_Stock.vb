Public Class EMI_Laporan_Outstanding_PO_VS_Stock

    Dim arrJenis As New ArrayList


    Private Sub EMI_Laporan_Persediaan_Bahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()

        Txt_KdBarang.Text = ""
        Txt_NmBarang.Text = ""

        Lv_Barang.Columns.Clear() : Lv_Barang.Items.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details

        Lv_Jenis.Columns.Clear() : Lv_Jenis.Items.Clear()
        Lv_Jenis.Columns.Add("Id Group Jenis", 100, HorizontalAlignment.Center)
        Lv_Jenis.Columns.Add("Jenis", 250, HorizontalAlignment.Left)
        Lv_Jenis.View = View.Details


        Cmb_Jenis.Items.Clear()
        Try
            OpenConn()

            Lv_Jenis.Items.Clear() : arrJenis.Clear()

            SQL = "select Id_Group_Jenis, Kode_Group_Jenis "
            SQL = SQL & "from EMI_Group_Jenis "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Cmb_Jenis.Items.Add("---SELURUH---") : arrJenis.Add("---SELURUH---")
                Do While Dr.Read
                    Cmb_Jenis.Items.Add(Dr("Kode_Group_Jenis")) : arrJenis.Add(Dr("Kode_Group_Jenis"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub LoadBarang(ByVal isKdBarang As Boolean)

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add("SELURUH")
            Lv.SubItems.Add("SELURUH")

            SQL = "select Kode_Barang, Nama "
            SQL = SQL & "from barang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            If isKdBarang Then
                SQL = SQL & "and Kode_Barang like '%" & Txt_KdBarang.Text & "%' "
            Else
                SQL = SQL & "and nama like '%" & Txt_NmBarang.Text & "%' "
            End If
            SQL = SQL & "group by Kode_Barang, Nama"
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


    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged

        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Me.Size = New Size(627, 245)
            Lv_Barang.Location = New Point(650, 112) : Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(128, 112)
            Me.Size = New Size(627, 314)
        End If

        LoadBarang(True)
    End Sub
    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(627, 245)
            Lv_Barang.Location = New Point(650, 112)
            Lv_Barang.Visible = False
            Exit Sub
        Else
            Me.Size = New Size(627, 314)
            Lv_Barang.Location = New Point(128, 112)
            Lv_Barang.Visible = True
        End If


        LoadBarang(False)
    End Sub



    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave

        If Txt_KdBarang.Text.Trim.Length Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "select Kode_Barang, Nama "
            SQL = SQL & "from barang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
            SQL = SQL & "and Nama = '" & Txt_KdBarang.Text & "' "
            SQL = SQL & "group by Kode_Barang, Nama"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_KdBarang.Text = Dr("Kode_Barang")
                    Txt_NmBarang.Text = Dr("Nama")

                Else
                    Dr.Close()
                    Txt_KdBarang.Text = ""
                    Txt_NmBarang.Text = ""
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        Dim Kode As String = Lv_Barang.FocusedItem.Text
        Dim Nama As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = Kode
        Txt_NmBarang.Text = Nama

        Lv_Barang.Visible = False
        Me.Size = New Size(627, 245)
        'CmbRelease.Focus()
    End Sub




    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_NmBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)
            Lv_Barang.Visible = False
            Me.Size = New Size(627, 245)
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub




    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)
            Lv_Barang.Visible = False
            Me.Size = New Size(627, 245)
        End If
    End Sub


    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Txt_KdBarang.Text.Trim.Length = 0 Or Txt_NmBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        ElseIf Cmb_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select kode_perusahaan "
            SQL = SQL & "from Vw_Laporan_Persediaan_Bahan_Baku"
            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' "

            SF = "{Vw_Laporan_Persediaan_Bahan_Baku.kode_perusahaan} = '" & KodePerusahaan & "' "

            If Not Txt_KdBarang.Text = "SELURUH" Then
                SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "and {Vw_Laporan_Persediaan_Bahan_Baku.kode_barang} = '" & Txt_KdBarang.Text & "' "
            End If

            If Not Cmb_Jenis.SelectedIndex = 0 Then
                SQL = SQL & "and Kode_Group_Jenis = '" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
                SF = SF & "and {Vw_Laporan_Persediaan_Bahan_Baku.Kode_Group_Jenis} = '" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
            End If
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New Laporan_Outstanding_PO_VS_Stock

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Laporan Outstanding Purchase Order VS Stock"
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan Persediaan Bahan Baku dan Kemasan"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
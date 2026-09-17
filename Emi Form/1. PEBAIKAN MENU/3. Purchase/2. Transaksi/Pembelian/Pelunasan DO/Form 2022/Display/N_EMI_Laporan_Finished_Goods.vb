Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Google.Apis.Storage.v1.Data

Public Class N_EMI_Laporan_Finished_Goods
    Dim arrtgl As New ArrayList
    Private Sub N_EMI_Laporan_Finished_Goods_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ComboBox1.Items.Clear() : arrtgl.Clear()
        ComboBox1.Items.Add("Tanggal") : arrtgl.Add("Tanggal")
        ComboBox1.Items.Add("Tanggal Produksi") : arrtgl.Add("Tgl_Produksi")
        ComboBox1.Items.Add("Tanggal Expired") : arrtgl.Add("Tgl_Expired")
        ComboBox1.SelectedIndex = -1

        Me.Size = New Point(638, 244)

        Lv_DetBarang.Visible = False
        Lv_DetBarang.Location = New Point(72, 130)
    End Sub

    Private Sub Btn_Exit_Click(sender As Object, e As EventArgs) Handles Btn_Exit.Click
        Me.Close()
    End Sub

    Private Sub N_EMI_Laporan_Finished_Goods_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub TxtKd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then
            If Lv_DetBarang.Items.Count = 0 Then Exit Sub
            Lv_DetBarang.Focus()
        End If
    End Sub

    Private Sub TxtKd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cetak.Focus()
    End Sub

    Private Sub TxtKd_Barang_Leave(sender As Object, e As EventArgs) Handles TxtKd_Barang.Leave
        If TxtKd_Barang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_DetBarang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If TxtKd_Barang.Text = "Seluruh" Then
                Txt_NmBarang.Text = "Seluruh"
                Btn_Cetak.Focus()
                Lv_DetBarang.Visible = False
            Else
                SQL = "select Kode_Barang,Nama From barang where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and kode_barang = '" & TxtKd_Barang.Text.Trim & "' "
                SQL = SQL & "group by kode_barang,nama"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Txt_NmBarang.Text = dr("nama")
                        'BtnCetak.Location = New Point(415, 175)
                        'BtnExit.Location = New Point(498, 175)
                        Btn_Cetak.Focus()
                        Lv_DetBarang.Visible = False
                    Else
                        TxtKd_Barang.Text = ""
                        Txt_NmBarang.Text = ""
                        Lv_DetBarang.Visible = False
                        'Lv_DetBarang.Location = New Point(803, 258)
                        Lv_DetBarang.Visible = False

                        Me.Size = New Point(638, 244)
                    End If
                End Using
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged
        If TxtKd_Barang.Text.Length >= 1 Then

            If TxtKd_Barang.Text.Trim.Length = 0 Then
                TxtKd_Barang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            If TxtKd_Barang.Text.Trim.Length = 0 Then
                Lv_DetBarang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            Lv_DetBarang.Location = New Point(72, 130)
            Lv_DetBarang.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(638, 345)

            Try
                OpenConn()

                Lv_DetBarang.Items.Clear()
                Txt_NmBarang.Text = ""

                Dim lv1 As New ListViewItem

                lv1 = Lv_DetBarang.Items.Add("Seluruh")
                lv1.SubItems.Add("Seluruh")

                SQL = "select Kode_Barang,Nama From barang where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and kode_barang like '%" & TxtKd_Barang.Text.Trim & "%' "
                SQL = SQL & "group by kode_barang,nama"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As New ListViewItem
                        Lv = Lv_DetBarang.Items.Add(Dr("kode_barang"))
                        Lv.SubItems.Add(Dr("nama"))

                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            Lv_DetBarang.Visible = False
            'Lv_DetBarang.Location = New Point(803, 258)
            Lv_DetBarang.Visible = False

            'BtnCetak.Location = New Point(415, 175)
            'BtnExit.Location = New Point(498, 175)

            Me.Size = New Point(638, 244)

        End If
    End Sub

    Private Sub Lv_DetBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick
        If Lv_DetBarang.Items.Count = 0 Then Exit Sub

        TxtKd_Barang.Text = Lv_DetBarang.FocusedItem.SubItems(0).Text
        TxtKd_Barang.Focus() : Btn_Cetak.Focus()
        Lv_DetBarang.Visible = False

        Me.Size = New Point(638, 244)
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then
            If Lv_DetBarang.Items.Count = 0 Then Exit Sub
            Lv_DetBarang.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cetak.Focus()
    End Sub

    Private Sub Txt_NmBarang_Leave(sender As Object, e As EventArgs) Handles Txt_NmBarang.Leave

    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Length >= 1 Then

            If Txt_NmBarang.Text.Trim.Length = 0 Then
                Txt_NmBarang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            If Txt_NmBarang.Text.Trim.Length = 0 Then
                Lv_DetBarang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            Lv_DetBarang.Location = New Point(72, 130)
            Lv_DetBarang.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(638, 345)

            Try
                OpenConn()

                Lv_DetBarang.Items.Clear()
                Dim lv1 As New ListViewItem

                lv1 = Lv_DetBarang.Items.Add("Seluruh")
                lv1.SubItems.Add("Seluruh")

                SQL = "select Kode_Barang,Nama From barang where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and nama like '%" & Txt_NmBarang.Text.Trim & "%' "
                SQL = SQL & "group by kode_barang,nama"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As New ListViewItem
                        Lv = Lv_DetBarang.Items.Add(Dr("kode_barang"))
                        Lv.SubItems.Add(Dr("nama"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            Lv_DetBarang.Visible = False
            'Lv_DetBarang.Location = New Point(803, 258)

            'BtnCetak.Location = New Point(415, 175)
            'BtnExit.Location = New Point(498, 175)

            Me.Size = New Point(638, 244)

        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then TxtKd_Barang.Focus()
    End Sub

    Private Sub Btn_Cetak_Click(sender As Object, e As EventArgs) Handles Btn_Cetak.Click
        If TxtKd_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKd_Barang.Focus()
            Exit Sub
        ElseIf ComboBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Periode belum dipilih !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""
            SQL = "select Kode_Barang from N_EMI_View_Laporan_Finished_Goods "

            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SF = "{N_EMI_View_Laporan_Finished_Goods.Kode_Perusahaan} = '" & KodePerusahaan & "' "

            If ComboBox1.SelectedIndex = 0 Then
                SQL = SQL & "and Tanggal between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {N_EMI_View_Laporan_Finished_Goods.Tanggal} >= #" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Finished_Goods.Tanggal} <= #" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "# "
            ElseIf ComboBox1.SelectedIndex = 1 Then
                SQL = SQL & "and Tgl_Produksi between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {N_EMI_View_Laporan_Finished_Goods.Tgl_Produksi} >= #" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Finished_Goods.Tgl_Produksi} <= #" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "# "
            ElseIf ComboBox1.SelectedIndex = 2 Then
                SQL = SQL & "and Tgl_Expired between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {N_EMI_View_Laporan_Finished_Goods.Tgl_Expired} >= #" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Finished_Goods.Tgl_Expired} <= #" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "# "
            End If

            If Not TxtKd_Barang.Text.Trim.ToUpper = "SELURUH" Then
                SQL = SQL & "and kode_barang = '" & TxtKd_Barang.Text.Trim & "' "
                SF = SF & " and {N_EMI_View_Laporan_Finished_Goods.kode_barang} = '" & TxtKd_Barang.Text.Trim & "' "
            End If
            Using MyDS As DataSet = Binding(SQL)
                With MyDS.Tables(0)
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New N_EMI_CR_Laporan_Finished_Goods

                        CrDoc.SetDataSource(MyDS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)

                        CrDoc.SummaryInfo.ReportTitle = "Periode " & ComboBox1.Text & " : " & Format(DateTimePicker1.Value, "dd MMM yyyy") & " s/d " &
                                                                    Format(DateTimePicker2.Value, "dd MMM yyyy")

                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Print Form"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                    Else
                        MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
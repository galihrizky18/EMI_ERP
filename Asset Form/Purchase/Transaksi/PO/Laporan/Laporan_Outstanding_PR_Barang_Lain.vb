Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Laporan_Outstanding_PR_Barang_Lain
    Private Sub Laporan_Outstanding_PR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_DetBarang.Location = New Point(141, 135)
        Me.Size = New Size(626, 262)
        Lv_DetBarang.Visible = False
        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date : Tgl1.Focus()

        cmbtgl.Items.Clear()
        cmbtgl.Items.Add("Tanggal Delivery")
        cmbtgl.Items.Add("Tanggal Release")

    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then TxtKd_Barang.Focus()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub TxtKd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then
            If Lv_DetBarang.Items.Count = 0 Then Exit Sub
            Lv_DetBarang.Focus()
        End If
    End Sub

    Private Sub TxtKd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub TxtKd_Barang_Leave(sender As Object, e As EventArgs) Handles TxtKd_Barang.Leave
        If TxtKd_Barang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_DetBarang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If TxtKd_Barang.Text = "Seluruh" Then
                Txt_NmBarang.Text = "Seluruh"
                BtnCetak.Focus()
                Lv_DetBarang.Visible = False
            Else
                SQL = "select Kode_Barang,Nama From barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and kode_barang = '" & TxtKd_Barang.Text.Trim & "' "
                SQL = SQL & "group by kode_barang,nama"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Txt_NmBarang.Text = dr("nama")
                        'BtnCetak.Location = New Point(415, 175)
                        'BtnExit.Location = New Point(498, 175)
                        BtnCetak.Focus()
                        Lv_DetBarang.Visible = False
                    Else
                        TxtKd_Barang.Text = ""
                        Txt_NmBarang.Text = ""
                        Lv_DetBarang.Visible = False
                        'Lv_DetBarang.Location = New Point(803, 258)
                        Lv_DetBarang.Visible = False

                        Me.Size = New Point(626, 262)
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

            Lv_DetBarang.Location = New Point(141, 135)
            Lv_DetBarang.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(626, 328)

            Try
                OpenConn()

                Lv_DetBarang.Items.Clear()
                Txt_NmBarang.Text = ""

                Dim lv1 As New ListViewItem

                lv1 = Lv_DetBarang.Items.Add("Seluruh")
                lv1.SubItems.Add("Seluruh")

                SQL = "select Kode_Barang,Nama From barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "'  "
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

            Me.Size = New Point(626, 262)

        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then
            If Lv_DetBarang.Items.Count = 0 Then Exit Sub
            Lv_DetBarang.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
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

            Lv_DetBarang.Location = New Point(141, 135)
            Lv_DetBarang.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(626, 328)

            Try
                OpenConn()

                Lv_DetBarang.Items.Clear()
                Dim lv1 As New ListViewItem

                lv1 = Lv_DetBarang.Items.Add("Seluruh")
                lv1.SubItems.Add("Seluruh")

                SQL = "select Kode_Barang,Nama From barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "'  "
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
            Lv_DetBarang.Visible = False

            'BtnCetak.Location = New Point(415, 175)
            'BtnExit.Location = New Point(498, 175)

            Me.Size = New Point(626, 262)

        End If
    End Sub

    Private Sub Lv_DetBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick
        If Lv_DetBarang.Items.Count = 0 Then Exit Sub

        TxtKd_Barang.Text = Lv_DetBarang.FocusedItem.SubItems(0).Text
        TxtKd_Barang.Focus() : BtnCetak.Focus()
        Lv_DetBarang.Visible = False


        Me.Size = New Point(626, 262)
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Exit Sub
        ElseIf cmbtgl.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Tanggal Harus di isi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbtgl.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""
            SQL = "select No_Faktur From Vw_Laporan_Outstanding_PR_barang_Lain  where  "
            If cmbtgl.SelectedIndex = 0 Then

                SQL = SQL & "tanggal_delivery between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "


                SF = "{Vw_Laporan_Outstanding_PR_Barang_Lain.tanggal_delivery} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_Outstanding_PR_Barang_Lain.tanggal_delivery} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            ElseIf cmbtgl.SelectedIndex = 1 Then

                SQL = SQL & "Tanggal_Release between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "


                SF = "{Vw_Laporan_Outstanding_PR_Barang_Lain.Tanggal_Release} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_Outstanding_PR_Barang_Lain.Tanggal_Release} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            End If



            If Not TxtKd_Barang.Text.Trim.ToUpper = "SELURUH" Then
                SQL = SQL & "And kode_barang = '" & TxtKd_Barang.Text.Trim & "' "
                SF = SF & "And {Vw_Laporan_Outstanding_PR_Barang_Lain.kode_barang} = '" & TxtKd_Barang.Text.Trim & "' "
            End If

            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New Rpt_Laporan_Outstanding_PR_Barang_Lain

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
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
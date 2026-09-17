Imports System.Net.Mime.MediaTypeNames

Public Class Laporan_HPP_Per_Batch
    Dim arrWorkCenter, arrJenisBiaya As New ArrayList

    Private Sub Laporan_Purchase_Requisition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_DetBarang.Location = New Point(97, 130)
        Me.Size = New Size(607, 256)

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date




        Try
            OpenConn()

            'CmbWorkCenter.Items.Clear() : arrWorkCenter.Clear()
            'CmbWorkCenter.Items.Add("Seluruh") : arrWorkCenter.Add("Seluruh")
            'SQL = "select id_work_center,keterangan from EMI_Master_Work_Center where kode_perusahaan = '" & KodePerusahaan & "' "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        CmbWorkCenter.Items.Add(dr("keterangan")) : arrWorkCenter.Add(dr("id_work_center"))
            '    Loop
            'End Using


            'CmbJenisBiaya.Items.Clear() : arrJenisBiaya.Clear()
            'CmbJenisBiaya.Items.Add("Seluruh") : arrJenisBiaya.Add("Seluruh")
            'SQL = "select id_jenis_biaya_produksi,keterangan from Emi_Jenis_Biaya_Produksi where kode_perusahaan = '" & KodePerusahaan & "' "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        CmbJenisBiaya.Items.Add(dr("keterangan")) : arrJenisBiaya.Add(dr("id_jenis_biaya_produksi"))
            '    Loop
            'End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try






        Lv_DetBarang.Visible = False
        Tgl1.Focus()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub





    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub









    Private Sub LvSupp_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick
        If Lv_DetBarang.Items.Count = 0 Then Exit Sub

        TxtKd_Barang.Text = Lv_DetBarang.FocusedItem.SubItems(0).Text
        TxtKd_Barang.Focus() : BtnCetak.Focus()
        Lv_DetBarang.Visible = False


        Me.Size = New Point(601, 259)
    End Sub

    Private Sub LvSupp_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_DetBarang.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupp_DoubleClick(Lv_DetBarang, e)
        End If
    End Sub

    Private Sub CmbRelease_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub Laporan_Purchase_Requisition_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Tgl1.Focus()
    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged

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

            Lv_DetBarang.Location = New Point(133, 137)
            Lv_DetBarang.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(601, 320)






            Try
                OpenConn()

                Lv_DetBarang.Items.Clear()
                Txt_NmBarang.Text = ""

                Dim lv1 As New ListViewItem

                lv1 = Lv_DetBarang.Items.Add("Seluruh")
                lv1.SubItems.Add("Seluruh")

                SQL = "select Kode_Barang, Nama From barang a, EMI_Group_Jenis b  "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "and kode_barang like '%" & TxtKd_Barang.Text.Trim & "%' "
                SQL = SQL & "and b.flag_finished_good = 'Y' "

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
            Lv_DetBarang.Location = New Point(803, 258)
            Lv_DetBarang.Visible = False

            BtnCetak.Location = New Point(415, 175)
            BtnExit.Location = New Point(498, 175)

            Me.Size = New Point(601, 259)


        End If
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
                SQL = "select Kode_Barang, Nama From barang a, EMI_Group_Jenis b  "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & TxtKd_Barang.Text.Trim & "' "
                SQL = SQL & "and b.flag_finished_good = 'Y' "

                SQL = SQL & "group by kode_barang,nama"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then



                        Txt_NmBarang.Text = dr("nama")


                        BtnCetak.Location = New Point(415, 175)
                        BtnExit.Location = New Point(498, 175)



                        BtnCetak.Focus()

                        Lv_DetBarang.Visible = False
                    Else
                        TxtKd_Barang.Text = ""
                        Txt_NmBarang.Text = ""
                        Lv_DetBarang.Visible = False
                        Lv_DetBarang.Location = New Point(803, 258)
                        Lv_DetBarang.Visible = False



                        Me.Size = New Point(601, 259)
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

            Lv_DetBarang.Location = New Point(133, 137)
            Lv_DetBarang.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(601, 320)






            Try
                OpenConn()

                Lv_DetBarang.Items.Clear()
                Dim lv1 As New ListViewItem

                lv1 = Lv_DetBarang.Items.Add("Seluruh")
                lv1.SubItems.Add("Seluruh")

                SQL = "select Kode_Barang, Nama From barang a, EMI_Group_Jenis b  "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "and nama like '%" & Txt_NmBarang.Text.Trim & "%' "
                SQL = SQL & "and b.flag_finished_good = 'Y' "

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
            Lv_DetBarang.Location = New Point(803, 258)
            Lv_DetBarang.Visible = False

            BtnCetak.Location = New Point(415, 175)
            BtnExit.Location = New Point(498, 175)

            Me.Size = New Point(601, 259)


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

    Private Sub Lv_DetBarang_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles Lv_DetBarang.ItemSelectionChanged

    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Exit Sub

        End If

        OpenConn()

        Dim SF As String = ""



        SQL = "select * "

        SQL = SQL & "From Vw_Laporan_HPP_Perbatch  where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "tgl_produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

        SF = "{Vw_Laporan_HPP_Perbatch.tgl_produksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
        SF = SF & "{Vw_Laporan_HPP_Perbatch.tgl_produksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "#"





        If Not TxtKd_Barang.Text.Trim.ToUpper = "SELURUH" Then
            SQL = SQL & "And kode_barang = '" & TxtKd_Barang.Text.Trim & "'"
            SF = SF & "And {Vw_Laporan_HPP_Perbatch.kode_barang} = '" & TxtKd_Barang.Text.Trim & "'"
        End If

        'If CmbWorkCenter.SelectedIndex = 1 Then 'SUDAH RELEASE (FLAG_RELEASE = 'Y')
        '    SQL = SQL & "and a.flag_release = 'Y'"
        '    SF = SF & "and {emi_pembelian_po.Flag_Release} = 'Y'"
        'ElseIf CmbWorkCenter.SelectedIndex = 2 Then 'SUDAH RELEASE (FLAG_RELEASE = NULL)
        '    SQL = SQL & "and a.flag_release IS NULL"
        '    SF = SF & "and ISNULL({emi_pembelian_po.Flag_Release})"
        'End If

        Using MyDS As DataSet = Binding(SQL)
            With MyDS.Tables(0)
                If .Rows.Count <> 0 Then

                    Dim CrDoc As New Laporan_HPP_Per_Batch_Rpt

                    CrDoc.SetDataSource(MyDS)
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
    End Sub
End Class
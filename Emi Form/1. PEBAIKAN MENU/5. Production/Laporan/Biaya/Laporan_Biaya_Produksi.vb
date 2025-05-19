Public Class Laporan_Biaya_Produksi

    Dim JudulForm As String = "Laporan Produksi"
    Dim arrJnsBiaya, arrReport As New ArrayList
    Dim Role As Boolean = False

    Private Sub Laporan_Biaya_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()
    End Sub

    Private Sub Kosong()

        Cmb_Laporan.Items.Clear() : arrReport.Clear()

        Try
            OpenConn()

            Tgl1.Value = Date.Now
            Tgl2.Value = Date.Now

            CmbJenisBiaya.Items.Clear() : arrJnsBiaya.Clear()
            SQL = "select Kode_Jenis_Biaya_Produksi, keterangan from Emi_Jenis_Biaya_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                CmbJenisBiaya.Items.Add("--- SEMUA ---") : arrJnsBiaya.Add("SEMUA")
                Do While Dr.Read
                    CmbJenisBiaya.Items.Add(Dr("keterangan")) : arrJnsBiaya.Add(Dr("Kode_Jenis_Biaya_Produksi"))
                Loop
            End Using
            CmbJenisBiaya.SelectedIndex = 0




            'If CekButtonRole("Report_Produksi_Biaya") = "Y" Then
            '    Cmb_Laporan.Items.Add("Laporan Biaya Produksi") : arrReport.Add("Laporan_Biaya_Produksi")
            '    Cmb_Laporan.Items.Add("Laporan Biaya Detail Good Issue") : arrReport.Add("Laporan_Biaya_Detail_Good_Issue")
            '    Cmb_Laporan.Items.Add("Laporan Detail Good Issue") : arrReport.Add("Laporan_Detail_Good_Issue")
            '    Cmb_Laporan.Items.Add("Laporan Hasil Produksi") : arrReport.Add("Laporan_Hasil_Produksi")
            '    Cmb_Laporan.Items.Add("Laporan Biaya Hasil Produksi") : arrReport.Add("Laporan_Biaya_Hasil_Produksi")
            '    'GroupBox1.Size = New Size(569, 118)
            '    'BtnCetak.Location = New Point(413, 174)
            '    'BtnExit.Location = New Point(496, 174)
            '    'Me.Size = New Size(607, 254)
            '    'Role = True
            'Else
            '    Cmb_Laporan.Items.Add("Laporan Detail Good Issue") : arrReport.Add("Laporan_Detail_Good_Issue")
            '    Cmb_Laporan.Items.Add("Laporan Hasil Produksi") : arrReport.Add("Laporan_Hasil_Produksi")
            '    'Label4.Visible = False : CmbJenisBiaya.Visible = False
            '    'GroupBox1.Size = New Size(569, 81)
            '    'BtnCetak.Location = New Point(413, 137)
            '    'BtnExit.Location = New Point(496, 137)
            '    'Me.Size = New Size(607, 216)
            '    'Role = False
            'End If

            Label4.Visible = False : CmbJenisBiaya.Visible = False
            GroupBox1.Size = New Size(569, 81)
            BtnCetak.Location = New Point(413, 137)
            BtnExit.Location = New Point(496, 137)
            Me.Size = New Size(607, 216)



            Tgl1.Focus()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Try
            OpenConn()

            If CekButtonRole("Report_Produksi_Biaya") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Biaya Produksi") : arrReport.Add("Laporan_Biaya_Produksi")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Try
            OpenConn()

            If CekButtonRole("Report_Produksi_GI_Ada_HPP") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Biaya Detail Good Issue") : arrReport.Add("Laporan_Biaya_Detail_Good_Issue")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Report_Produksi_Biaya_GI_Tanpa_HPP") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Detail Good Issue") : arrReport.Add("Laporan_Detail_Good_Issue")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Report_Hasil_Produksi_Tanpa_HPP") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Hasil Produksi") : arrReport.Add("Laporan_Hasil_Produksi")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Try
            OpenConn()

            If CekButtonRole("Report_Hasil_Produksi_Ada_HPP") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Biaya Hasil Produksi") : arrReport.Add("Laporan_Biaya_Hasil_Produksi")
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try





    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date : Tgl1.Focus()
            Exit Sub
        ElseIf Cmb_Laporan.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dulu Laporan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Laporan.Focus() : Exit Sub
        End If

        If Role Then
            If arrReport(Cmb_Laporan.SelectedIndex) = "Laporan_Biaya_Produksi" Then
                If CmbJenisBiaya.SelectedIndex = -1 Then
                    MessageBox.Show("Pilih Dulu Jenis Biaya Produksi", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    CmbJenisBiaya.Focus() : Exit Sub
                End If
            End If
        End If

        Try
            OpenConn()
            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim SF As String = ""

            If arrReport(Cmb_Laporan.SelectedIndex) = "Laporan_Biaya_Produksi" Then

                SQL = "select Kode_Perusahaan from Vw_Laporan_Biaya_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SF = "{Vw_Laporan_Biaya_Produksi.Kode_Perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and TanggalMulaiProduksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {Vw_Laporan_Biaya_Produksi.TanggalMulaiProduksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_Biaya_Produksi.TanggalMulaiProduksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If CmbJenisBiaya.SelectedIndex <> 0 Then
                    SQL = SQL & "and Kode_Jenis_Biaya  = '" & arrJnsBiaya.Item(CmbJenisBiaya.SelectedIndex) & "'"
                    SF = SF & "and {Vw_Laporan_Biaya_Produksi.Kode_Jenis_Biaya} = '" & arrJnsBiaya.Item(CmbJenisBiaya.SelectedIndex) & "' "
                End If
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Rpt_Laporan_Biaya_Produksi
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF
                            .Text = JudulForm
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With
                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "Laporan_Hasil_Produksi" Then
                SQL = "select kode_perusahaan from Vw_Laporan_Hasil_Produksi where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{Vw_Laporan_Hasil_Produksi.Kode_Perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {Vw_Laporan_Hasil_Produksi.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_Hasil_Produksi.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Rpt_Laporan_Hasil_Produksi

                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF
                            .Text = JudulForm
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With

                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "Laporan_Biaya_Hasil_Produksi" Then
                SQL = "select kode_perusahaan from Vw_Laporan_Hasil_Produksi_Duit where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{Vw_Laporan_Hasil_Produksi_Duit.Kode_Perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {Vw_Laporan_Hasil_Produksi_Duit.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_Hasil_Produksi_Duit.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Rpt_Laporan_Hasil_Produksi_Nominal

                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF
                            .Text = JudulForm
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With

                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "Laporan_Biaya_Detail_Good_Issue" Then
                SQL = "select kode_perusahaan from Vw_Laporan_GI_Detail_Duit where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{Vw_Laporan_GI_Detail_duit.Kode_Perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {Vw_Laporan_GI_Detail_Duit.Tgl_Produksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_GI_Detail_Duit.Tgl_Produksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Rpt_Laporan_GI_Detail_Nominal

                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF
                            .Text = JudulForm
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With

                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "Laporan_Detail_Good_Issue" Then
                SQL = "select kode_perusahaan from Vw_Laporan_GI_Detail where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{Vw_Laporan_GI_Detail.Kode_Perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {Vw_Laporan_GI_Detail.Tgl_Produksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_GI_Detail.Tgl_Produksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Rpt_Laporan_GI_Detail

                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF
                            .Text = JudulForm
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With

                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
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

    Private Sub Cmb_Laporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Laporan.SelectedIndexChanged

        If arrReport(Cmb_Laporan.SelectedIndex) = "Laporan_Biaya_Produksi" Then
            Label4.Visible = True : CmbJenisBiaya.Visible = True
            GroupBox1.Size = New Size(569, 118)
            BtnCetak.Location = New Point(413, 174)
            BtnExit.Location = New Point(496, 174)
            Me.Size = New Size(607, 254)
        Else
            Label4.Visible = False : CmbJenisBiaya.Visible = False
            GroupBox1.Size = New Size(569, 81)
            BtnCetak.Location = New Point(413, 137)
            BtnExit.Location = New Point(496, 137)
            Me.Size = New Size(607, 216)
        End If
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub
    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then CmbJenisBiaya.Focus()
    End Sub
    Private Sub CmbJenisBiaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbJenisBiaya.KeyPress, Cmb_Laporan.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub
End Class
Public Class Laporan_Biaya_Produksi

    Dim JudulForm As String = "Laporan Produksi"
    Dim arrJnsBiaya, arrReport As New ArrayList
    Dim Role As Boolean = False

    Dim switchAutoComplete As Boolean = False

    Private Sub Laporan_Biaya_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Lokasi.Columns.Clear()
        Lv_Lokasi.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
        Lv_Lokasi.Columns.Add("Keterangan", 180, HorizontalAlignment.Left)
        Lv_Lokasi.View = View.Details

        Lv_Lokasi2.Columns.Clear()
        Lv_Lokasi2.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
        Lv_Lokasi2.Columns.Add("Keterangan", 180, HorizontalAlignment.Left)
        Lv_Lokasi2.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 180, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details

        Lv_Pack_Sekunder_NoTransaksi.Columns.Clear()
        Lv_Pack_Sekunder_NoTransaksi.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
        Lv_Pack_Sekunder_NoTransaksi.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Pack_Sekunder_NoTransaksi.Columns.Add("Keterangan", 180, HorizontalAlignment.Left)
        Lv_Pack_Sekunder_NoTransaksi.View = View.Details

        Lv_Pack_Sekunder_NoSplit.Columns.Clear()
        Lv_Pack_Sekunder_NoSplit.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
        Lv_Pack_Sekunder_NoSplit.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Pack_Sekunder_NoSplit.View = View.Details

        Lv_Pack_Sekunder_Barang.Columns.Clear()
        Lv_Pack_Sekunder_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Pack_Sekunder_Barang.Columns.Add("Barang", 180, HorizontalAlignment.Center)
        Lv_Pack_Sekunder_Barang.View = View.Details

        Lv_Pack_Sekunder_Bahan.Columns.Clear()
        Lv_Pack_Sekunder_Bahan.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Pack_Sekunder_Bahan.Columns.Add("Barang", 180, HorizontalAlignment.Center)
        Lv_Pack_Sekunder_Bahan.View = View.Details

        Kosong()
    End Sub

    Private Sub Kosong()

        Cmb_Laporan.Items.Clear() : arrReport.Clear()

        switchAutoComplete = False

        Try
            OpenConn()

            Tgl1.Value = Date.Now
            Tgl2.Value = Date.Now

            Panel_Packaging_Sekunder.Visible = False
            Panel_Packaging_Sekunder.Location = New Point(600, 127)

            CmbJenisBiaya.Items.Clear() : arrJnsBiaya.Clear()
            SQL = "select Kode_Jenis_Biaya_Produksi, keterangan from Emi_Jenis_Biaya_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                CmbJenisBiaya.Items.Add(OpsiSeluruh) : arrJnsBiaya.Add("SEMUA")
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

        Chk_NoSplit.Checked = False : Chk_Batch.Checked = False

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
                Cmb_Laporan.Items.Add("Laporan Detail Goods Issue (Nominal)") : arrReport.Add("Laporan_Biaya_Detail_Good_Issue")
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
                Cmb_Laporan.Items.Add("Laporan GR Pcs") : arrReport.Add("Laporan_Hasil_Produksi")
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
                Cmb_Laporan.Items.Add("Laporan GR Pcs (Nominal)") : arrReport.Add("Laporan_Biaya_Hasil_Produksi")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Report_Validasi_GR") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Validasi GR Pcs") : arrReport.Add("Rpt_Laporan_Validasi_GR")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Report_Validasi_GR_Ada_HPP") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Validasi GR Pcs (Nominal)") : arrReport.Add("Rpt_Laporan_Validasi_GR_Nominal")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Report_GR_3") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Good Received Warehouse") : arrReport.Add("N_EMI_CR_Laporan_Validasi_GR_3")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Report_Packaging_Sekunder") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Packaging Sekunder") : arrReport.Add("N_EMI_CR_Packaging_Sekunder")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Report_Packaging_Sekunder_Nominal") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Packaging Sekunder (Nominal)") : arrReport.Add("N_EMI_CR_Packaging_Sekunder_Nominal")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Report_GR_3_Summary") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Penerimaan Barang Warehouse") : arrReport.Add("N_EMI_CR_Transaksi_Validasi_GR_3_Summary")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Report_GR_3_Summary_Nominal") = "Y" Then
                Cmb_Laporan.Items.Add("Laporan Penerimaan Barang Warehouse (Nominal)") : arrReport.Add("N_EMI_CR_Transaksi_Validasi_GR_3_Summary_Nominal")
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
                SQL = "select * from Vw_Laporan_GI_Detail_Duit where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{Vw_Laporan_GI_Detail_duit.Kode_Perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {Vw_Laporan_GI_Detail_Duit.Tgl_Produksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_GI_Detail_Duit.Tgl_Produksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Rpt_Laporan_GI_Detail_Nominal

                        With A_Place_For_Printing3
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

            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "Rpt_Laporan_Validasi_GR" Then

                SQL = "select kode_perusahaan from Vw_Laporan_Validasi_GR where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{Vw_Laporan_Validasi_GR.kode_perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {Vw_Laporan_Validasi_GR.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_Validasi_GR.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Chk_NoSplit.Checked Then
                    If Txt_NoSplit.Text.Trim.Length = 0 Then
                        CloseConn()
                        MessageBox.Show("No Split Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        If Not Txt_NoSplit.Text.ToUpper = OpsiSeluruh.ToUpper Then
                            SQL = SQL & "and No_Production_Order = '" & Txt_NoSplit.Text.Trim & "' "
                            SF = SF & "and {Vw_Laporan_Validasi_GR.No_Production_Order} = '" & Txt_NoSplit.Text.Trim & "' "
                        End If
                    End If
                End If

                If Chk_Batch.Checked Then
                    If Txt_Batch.Text.Trim.Length = 0 Then
                        CloseConn()
                        MessageBox.Show("No Batch Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        If Not Txt_Batch.Text.ToUpper = OpsiSeluruh.ToUpper Then
                            SQL = SQL & "and Batch_Number = '" & Txt_Batch.Text.Trim & "' "
                            SF = SF & "and {Vw_Laporan_Validasi_GR.Batch_Number} = '" & Txt_Batch.Text.Trim & "' "
                        End If
                    End If
                End If

                If Not Txt_KdSoAwal.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Stock_Owner_Awal =  '" & Txt_KdSoAwal.Text.Trim & "' "
                    SF = SF & "and {Vw_Laporan_Validasi_GR.Kode_Stock_Owner_Awal} = '" & Txt_KdSoAwal.Text.Trim & "' "
                End If

                If Not Txt_KdSoTujuan.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Stock_Owner_Tujuan =   '" & Txt_KdSoTujuan.Text.Trim & "' "
                    SF = SF & "and {Vw_Laporan_Validasi_GR.Kode_Stock_Owner_Tujuan} = '" & Txt_KdSoTujuan.Text.Trim & "' "
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text.Trim & "' "
                    SF = SF & "and {Vw_Laporan_Validasi_GR.Kode_Barang} = '" & Txt_KdBarang.Text.Trim & "' "
                End If

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New Rpt_Laporan_Validasi_GR

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

            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "Rpt_Laporan_Validasi_GR_Nominal" Then

                SQL = "select kode_perusahaan from Vw_Laporan_Validasi_GR where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{Vw_Laporan_Validasi_GR.kode_perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {Vw_Laporan_Validasi_GR.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{Vw_Laporan_Validasi_GR.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_NoSplit.Text.Trim.Length = 0 Then
                    If Not Txt_NoSplit.Text.ToUpper = OpsiSeluruh.ToUpper Then
                        SQL = SQL & "and No_Production_Order = '" & Txt_NoSplit.Text.Trim & "' "
                        SF = SF & "and {Vw_Laporan_Validasi_GR.No_Production_Order} = '" & Txt_NoSplit.Text.Trim & "' "
                    End If
                End If

                If Not Txt_Batch.Text.Trim.Length = 0 Then
                    If Not Txt_Batch.Text.ToUpper = OpsiSeluruh.ToUpper Then
                        SQL = SQL & "and Batch_Number = '" & Txt_Batch.Text.Trim & "' "
                        SF = SF & "and {Vw_Laporan_Validasi_GR.Batch_Number} = '" & Txt_Batch.Text.Trim & "' "
                    End If
                End If

                If Not Txt_KdSoAwal.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Stock_Owner_Awal =  '" & Txt_KdSoAwal.Text.Trim & "' "
                    SF = SF & "and {Vw_Laporan_Validasi_GR.Kode_Stock_Owner_Awal} = '" & Txt_KdSoAwal.Text.Trim & "' "
                End If

                If Not Txt_KdSoTujuan.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Stock_Owner_Tujuan =   '" & Txt_KdSoTujuan.Text.Trim & "' "
                    SF = SF & "and {Vw_Laporan_Validasi_GR.Kode_Stock_Owner_Tujuan} = '" & Txt_KdSoTujuan.Text.Trim & "' "
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text.Trim & "' "
                    SF = SF & "and {Vw_Laporan_Validasi_GR.Kode_Barang} = '" & Txt_KdBarang.Text.Trim & "' "
                End If

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New Rpt_Laporan_Validasi_GR_Nominal

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

            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Laporan_Validasi_GR_3" Then

                SQL = "select kode_perusahaan from N_EMI_View_Laporan_Validasi_GR_3 where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{N_EMI_View_Laporan_Validasi_GR_3.kode_perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {N_EMI_View_Laporan_Validasi_GR_3.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Validasi_GR_3.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_NoSplit.Text.Trim.Length = 0 Then
                    If Not Txt_NoSplit.Text.ToUpper = OpsiSeluruh.ToUpper Then
                        SQL = SQL & "and No_Production_Order = '" & Txt_NoSplit.Text.Trim & "' "
                        SF = SF & "and {N_EMI_View_Laporan_Validasi_GR_3.No_Production_Order} = '" & Txt_NoSplit.Text.Trim & "' "
                    End If
                End If

                If Not Txt_Batch.Text.Trim.Length = 0 Then
                    If Not Txt_Batch.Text.ToUpper = OpsiSeluruh.ToUpper Then
                        SQL = SQL & "and Batch_Number = '" & Txt_Batch.Text.Trim & "' "
                        SF = SF & "and {N_EMI_View_Laporan_Validasi_GR_3.Batch_Number} = '" & Txt_Batch.Text.Trim & "' "
                    End If
                End If

                If Not Txt_KdSoAwal.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and kode_stock_owner_awal =  '" & Txt_KdSoAwal.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Validasi_GR_3.kode_stock_owner_awal} = '" & Txt_KdSoAwal.Text.Trim & "' "
                End If

                If Not Txt_KdSoTujuan.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and kode_stock_owner_tujuan =   '" & Txt_KdSoTujuan.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Validasi_GR_3.kode_stock_owner_tujuan} = '" & Txt_KdSoTujuan.Text.Trim & "' "
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Validasi_GR_3.kode_barang} = '" & Txt_KdBarang.Text.Trim & "' "
                End If

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New N_EMI_CR_Laporan_Validasi_GR_3

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


            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Packaging_Sekunder" Then

                SQL = "select kode_perusahaan from N_EMI_View_Laporan_Packaging_Sekunder where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{N_EMI_View_Laporan_Packaging_Sekunder.kode_perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Packaging_Sekunder.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "


                If Not Txt_Pack_Sekunder_NoTransaksi.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Transaksi =  '" & Txt_Pack_Sekunder_NoTransaksi.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.No_Transaksi} = '" & Txt_Pack_Sekunder_NoTransaksi.Text.Trim & "' "
                End If

                If Not Txt_Pack_Sekunder_NoSplit.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Production_Order = '" & Txt_Pack_Sekunder_NoSplit.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.No_Production_Order} = '" & Txt_Pack_Sekunder_NoSplit.Text.Trim & "' "
                End If

                If Not Txt_Pack_Sekunder_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_Pack_Sekunder_KdBarang.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.Kode_Barang} = '" & Txt_Pack_Sekunder_KdBarang.Text.Trim & "' "
                End If

                If Not Txt_Pack_Sekunder_KdBahan.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Bahan = '" & Txt_Pack_Sekunder_KdBahan.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.Kode_Bahan} = '" & Txt_Pack_Sekunder_KdBahan.Text.Trim & "' "
                End If

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New N_EMI_CR_Packaging_Sekunder

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



            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Packaging_Sekunder_Nominal" Then

                SQL = "select kode_perusahaan from N_EMI_View_Laporan_Packaging_Sekunder where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{N_EMI_View_Laporan_Packaging_Sekunder.kode_perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Packaging_Sekunder.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "


                If Not Txt_Pack_Sekunder_NoTransaksi.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Transaksi =  '" & Txt_KdSoAwal.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.No_Transaksi} = '" & Txt_Pack_Sekunder_NoTransaksi.Text.Trim & "' "
                End If

                If Not Txt_Pack_Sekunder_NoSplit.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Production_Order = '" & Txt_Pack_Sekunder_NoSplit.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.No_Production_Order} = '" & Txt_Pack_Sekunder_NoSplit.Text.Trim & "' "
                End If

                If Not Txt_Pack_Sekunder_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_Pack_Sekunder_KdBarang.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.Kode_Barang} = '" & Txt_Pack_Sekunder_KdBarang.Text.Trim & "' "
                End If

                If Not Txt_Pack_Sekunder_KdBahan.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Bahan = '" & Txt_KdBarang.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Packaging_Sekunder.Kode_Bahan} = '" & Txt_Pack_Sekunder_KdBahan.Text.Trim & "' "
                End If

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New N_EMI_CR_Packaging_Sekunder_Nominal

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

            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Transaksi_Validasi_GR_3_Summary" Or arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Transaksi_Validasi_GR_3_Summary_Nominal" Then

                SQL = "select kode_perusahaan from N_EMI_View_Laporan_Transaksi_Validasi_GR_3 where kode_perusahaan = '" & KodePerusahaan & "' "
                SF = "{N_EMI_View_Laporan_Transaksi_Validasi_GR_3.kode_perusahaan} = '" & KodePerusahaan & "' "

                SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = SF & "and {N_EMI_View_Laporan_Transaksi_Validasi_GR_3.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Transaksi_Validasi_GR_3.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "


                If Not Txt_NoSplit.Text.Trim.Length = 0 Then
                    If Not Txt_NoSplit.Text.ToUpper = OpsiSeluruh.ToUpper Then
                        SQL = SQL & "and No_Split = '" & Txt_NoSplit.Text.Trim & "' "
                        SF = SF & "and {N_EMI_View_Laporan_Transaksi_Validasi_GR_3.No_Split} = '" & Txt_NoSplit.Text.Trim & "' "
                    End If
                End If

                If Not Txt_Batch.Text.Trim.Length = 0 Then
                    If Not Txt_Batch.Text.ToUpper = OpsiSeluruh.ToUpper Then
                        SQL = SQL & "and Batch_Number = '" & Txt_Batch.Text.Trim & "' "
                        SF = SF & "and {N_EMI_View_Laporan_Transaksi_Validasi_GR_3.Batch_Number} = '" & Txt_Batch.Text.Trim & "' "
                    End If
                End If

                If Not Txt_KdSoAwal.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and kode_stock_owner_awal = '" & Txt_KdSoAwal.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Transaksi_Validasi_GR_3.kode_stock_owner_awal} = '" & Txt_KdSoAwal.Text.Trim & "' "
                End If

                If Not Txt_KdSoTujuan.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and kode_stock_owner_tujuan = '" & Txt_KdSoTujuan.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Transaksi_Validasi_GR_3.kode_stock_owner_tujuan} = '" & Txt_KdSoTujuan.Text.Trim & "' "
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text.Trim & "' "
                    SF = SF & "and {N_EMI_View_Laporan_Transaksi_Validasi_GR_3.kode_barang} = '" & Txt_KdBarang.Text.Trim & "' "
                End If

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        If arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Transaksi_Validasi_GR_3_Summary" Then
                            CrDoc = New N_EMI_CR_Transaksi_Validasi_GR_3_Summary
                        ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Transaksi_Validasi_GR_3_Summary_Nominal" Then
                            CrDoc = New N_EMI_CR_Transaksi_Validasi_GR_3_Summary_Nominal
                        End If

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

        Panel_Packaging_Sekunder.Visible = False
        Panel_Packaging_Sekunder.Location = New Point(600, 127)

        If arrReport(Cmb_Laporan.SelectedIndex) = "Laporan_Biaya_Produksi" Then
            Label4.Visible = True : CmbJenisBiaya.Visible = True
            Label4.Text = "Jenis Biaya" : Txt_NoSplit.Visible = False

            Chk_NoSplit.Visible = False : Chk_Batch.Visible = False
            Label9.Visible = False : Txt_Batch.Visible = False
            Label6.Visible = False : Txt_KdSoAwal.Visible = False : Txt_NmLokasiAwal.Visible = False
            Label7.Visible = False : Txt_KdSoTujuan.Visible = False : Txt_NmLokasiTujuan.Visible = False
            Label8.Visible = False : Txt_KdBarang.Visible = False : Txt_NmBarang.Visible = False

            Chk_NoSplit.Checked = False : Chk_Batch.Checked = False

            GroupBox1.Size = New Size(569, 118)
            BtnCetak.Location = New Point(413, 174)
            BtnExit.Location = New Point(496, 174)
            Me.Size = New Size(607, 254)

        ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "Rpt_Laporan_Validasi_GR" Or arrReport(Cmb_Laporan.SelectedIndex) = "Rpt_Laporan_Validasi_GR_Nominal" Or arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Laporan_Validasi_GR_3" Then
            Label4.Visible = True : CmbJenisBiaya.Visible = False : CmbJenisBiaya.SelectedIndex = 0
            Label4.Text = "No Split" : Txt_NoSplit.Visible = True

            Chk_NoSplit.Visible = True : Chk_Batch.Visible = True
            Label9.Visible = True : Txt_Batch.Visible = True
            Label6.Visible = True : Txt_KdSoAwal.Visible = True : Txt_NmLokasiAwal.Visible = True
            Label7.Visible = True : Txt_KdSoTujuan.Visible = True : Txt_NmLokasiTujuan.Visible = True
            Label8.Visible = True : Txt_KdBarang.Visible = True : Txt_NmBarang.Visible = True

            Chk_NoSplit.Checked = False : Chk_Batch.Checked = False

            Txt_KdSoAwal.Text = OpsiSeluruh : Txt_NmLokasiAwal.Text = OpsiSeluruh
            Txt_KdSoTujuan.Text = OpsiSeluruh : Txt_NmLokasiTujuan.Text = OpsiSeluruh
            Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh

            GroupBox1.Size = New Size(569, 221)
            BtnCetak.Location = New Point(413, 278)
            BtnExit.Location = New Point(496, 278)
            Me.Size = New Size(607, 360)

        ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Packaging_Sekunder" Or arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Packaging_Sekunder_Nominal" Then

            switchAutoComplete = True
            Txt_Pack_Sekunder_NoTransaksi.Text = OpsiSeluruh
            Txt_Pack_Sekunder_NoSplit.Text = OpsiSeluruh
            Txt_Pack_Sekunder_KdBarang.Text = OpsiSeluruh : Txt_Pack_Sekunder_NmBarang.Text = OpsiSeluruh
            Txt_Pack_Sekunder_KdBahan.Text = OpsiSeluruh : Txt_Pack_Sekunder_NmBahan.Text = OpsiSeluruh
            switchAutoComplete = False

            GroupBox1.Size = New Size(569, 221)
            Me.Size = New Size(607, 360)
            BtnCetak.Location = New Point(413, 278)
            BtnExit.Location = New Point(496, 278)

            Panel_Packaging_Sekunder.Location = New Point(17, 127)
            Panel_Packaging_Sekunder.Visible = True

        ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Transaksi_Validasi_GR_3_Summary" Or arrReport(Cmb_Laporan.SelectedIndex) = "N_EMI_CR_Transaksi_Validasi_GR_3_Summary_Nominal" Then

            Label4.Visible = True : CmbJenisBiaya.Visible = False : CmbJenisBiaya.SelectedIndex = 0
            Label4.Text = "No Split" : Txt_NoSplit.Visible = True

            Chk_NoSplit.Visible = True : Chk_Batch.Visible = True
            Label9.Visible = True : Txt_Batch.Visible = True
            Label6.Visible = True : Txt_KdSoAwal.Visible = True : Txt_NmLokasiAwal.Visible = True
            Label7.Visible = True : Txt_KdSoTujuan.Visible = True : Txt_NmLokasiTujuan.Visible = True
            Label8.Visible = True : Txt_KdBarang.Visible = True : Txt_NmBarang.Visible = True

            Chk_NoSplit.Checked = False : Chk_Batch.Checked = False

            Txt_KdSoAwal.Text = OpsiSeluruh : Txt_NmLokasiAwal.Text = OpsiSeluruh
            Txt_KdSoTujuan.Text = OpsiSeluruh : Txt_NmLokasiTujuan.Text = OpsiSeluruh
            Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh

            GroupBox1.Size = New Size(569, 221)
            BtnCetak.Location = New Point(413, 278)
            BtnExit.Location = New Point(496, 278)
            Me.Size = New Size(607, 360)


        Else

            Chk_NoSplit.Visible = False : Chk_Batch.Visible = False
            Label4.Visible = False : CmbJenisBiaya.Visible = False
            Label9.Visible = False : Txt_Batch.Visible = False
            Label6.Visible = False : Txt_KdSoAwal.Visible = False : Txt_NmLokasiAwal.Visible = False
            Label7.Visible = False : Txt_KdSoTujuan.Visible = False : Txt_NmLokasiTujuan.Visible = False
            Label8.Visible = False : Txt_KdBarang.Visible = False : Txt_NmBarang.Visible = False

            Chk_NoSplit.Checked = False : Chk_Batch.Checked = False

            GroupBox1.Size = New Size(569, 81)
            BtnCetak.Location = New Point(413, 137)
            BtnExit.Location = New Point(496, 137)
            Me.Size = New Size(607, 216)

        End If

        Txt_NoSplit.Text = "" : Txt_Batch.Text = ""
        Lv_Lokasi.Visible = False : Lv_Lokasi2.Visible = False : Lv_Barang.Visible = False

    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    '====================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '====================================================================================================================================
    Private Sub Txt_KdSoAwal_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdSoAwal.TextChanged
        If Txt_KdSoAwal.Text.Trim.Length = 0 Then
            Me.Size = New Size(607, 360)
            Lv_Lokasi.Location = New Point(600, 204)
            Lv_Lokasi.Visible = False
            Txt_KdSoAwal.Text = ""
            Txt_NmLokasiAwal.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(607, 415)
            Lv_Lokasi.Visible = True
            Lv_Lokasi.Location = New Point(132, 204)
        End If

        Try
            OpenConn()

            Lv_Lokasi.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Lokasi.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select kode_stock_owner, Keterangan from stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner like '%" & Txt_KdSoAwal.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Lokasi.Items.Add(Dr("kode_stock_owner"))
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

    Private Sub Txt_NmLokasiAwal_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmLokasiAwal.TextChanged
        If Txt_NmLokasiAwal.Text.Trim.Length = 0 Then
            Me.Size = New Size(607, 360)
            Lv_Lokasi.Location = New Point(600, 204)
            Lv_Lokasi.Visible = False
            Txt_KdSoAwal.Text = ""
            Txt_NmLokasiAwal.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(607, 415)
            Lv_Lokasi.Visible = True
            Lv_Lokasi.Location = New Point(132, 204)
        End If

        Try
            OpenConn()

            Lv_Lokasi.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Lokasi.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select kode_stock_owner, Keterangan from stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_NmLokasiAwal.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Lokasi.Items.Add(Dr("kode_stock_owner"))
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

    Private Sub Txt_KdSoTujuan_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdSoTujuan.TextChanged
        If Txt_KdSoTujuan.Text.Trim.Length = 0 Then
            Me.Size = New Size(607, 360)
            Lv_Lokasi2.Location = New Point(600, 230)
            Lv_Lokasi2.Visible = False
            Txt_KdSoTujuan.Text = ""
            Txt_NmLokasiTujuan.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(607, 445)
            Lv_Lokasi2.Visible = True
            Lv_Lokasi2.Location = New Point(132, 230)
        End If

        Try
            OpenConn()

            Lv_Lokasi2.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Lokasi2.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select kode_stock_owner, Keterangan from stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner like '%" & Txt_KdSoTujuan.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Lokasi2.Items.Add(Dr("kode_stock_owner"))
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

    Private Sub Txt_NmLokasiTujuan_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmLokasiTujuan.TextChanged
        If Txt_NmLokasiTujuan.Text.Trim.Length = 0 Then
            Me.Size = New Size(607, 360)
            Lv_Lokasi2.Location = New Point(600, 230)
            Lv_Lokasi2.Visible = False
            Txt_KdSoTujuan.Text = ""
            Txt_NmLokasiTujuan.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(607, 445)
            Lv_Lokasi2.Visible = True
            Lv_Lokasi2.Location = New Point(132, 230)
        End If

        Try
            OpenConn()

            Lv_Lokasi2.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Lokasi2.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select kode_stock_owner, Keterangan from stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_NmLokasiTujuan.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Lokasi2.Items.Add(Dr("kode_stock_owner"))
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
            Me.Size = New Size(607, 360)
            Lv_Barang.Location = New Point(600, 256)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(607, 470)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(132, 256)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct Kode_Barang, Nama from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang like '%" & Txt_KdBarang.Text & "%'"
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
            Me.Size = New Size(607, 360)
            Lv_Barang.Location = New Point(600, 256)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(607, 470)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(132, 256)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct Kode_Barang, Nama from barang where kode_perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_NmBarang.Text & "%'"
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

    '====================================================================================================================================
    '=     HANDLE LEAVE
    '====================================================================================================================================
    Private Sub Txt_KdSoAwal_Leave(sender As Object, e As EventArgs) Handles Txt_KdSoAwal.Leave
        If Txt_KdSoAwal.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Lokasi.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdSoAwal.Text = OpsiSeluruh Then

                SQL = "select kode_stock_owner, Keterangan from stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Txt_KdSoAwal.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdSoAwal.Text = Dr("kode_stock_owner")
                        Txt_NmLokasiAwal.Text = Dr("Keterangan")
                        Txt_KdSoTujuan.Focus()
                    Else
                        MessageBox.Show("Lokasi tidak ditemukan . . ! !", Judul)
                        Txt_KdSoAwal.Text = ""
                        Txt_NmLokasiAwal.Text = ""
                        Txt_KdSoAwal.Focus()
                    End If

                    Me.Size = New Size(607, 360)
                    Lv_Lokasi.Location = New Point(600, 204)
                    Lv_Lokasi.Visible = False
                End Using
            Else
                Txt_KdSoTujuan.Focus()

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Txt_KdSoTujuan_Leave(sender As Object, e As EventArgs) Handles Txt_KdSoTujuan.Leave
        If Txt_KdSoTujuan.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Lokasi2.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdSoTujuan.Text = OpsiSeluruh Then

                SQL = "select kode_stock_owner, Keterangan from stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Txt_KdSoTujuan.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdSoTujuan.Text = Dr("kode_stock_owner")
                        Txt_NmLokasiTujuan.Text = Dr("Keterangan")
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("Lokasi tidak ditemukan . . ! !", Judul)
                        Txt_KdSoTujuan.Text = ""
                        Txt_NmLokasiTujuan.Text = ""
                        Txt_KdSoTujuan.Focus()
                    End If

                    Me.Size = New Size(607, 360)
                    Lv_Lokasi2.Location = New Point(600, 230)
                    Lv_Lokasi2.Visible = False
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

                SQL = "select Kode_Barang, Nama from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & Txt_KdBarang.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NmBarang.Text = Dr("Nama")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Me.Size = New Size(607, 360)
                    Lv_Barang.Location = New Point(600, 256)
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

    '====================================================================================================================================
    '=     HANDLE LISTVIEW
    '====================================================================================================================================
    Private Sub Lv_Lokasi_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Lokasi.DoubleClick
        If Lv_Lokasi.Items.Count = 0 Or Lv_Lokasi.FocusedItem.Index = -1 Then Exit Sub

        Dim KdLokasi As String = Lv_Lokasi.FocusedItem.SubItems(0).Text
        Dim NmLokasi As String = Lv_Lokasi.FocusedItem.SubItems(1).Text

        Txt_KdSoAwal.Text = KdLokasi
        Txt_NmLokasiAwal.Text = NmLokasi

        Me.Size = New Size(607, 360)
        Lv_Lokasi.Location = New Point(600, 204)
        Lv_Lokasi.Visible = False

        Txt_KdSoTujuan.Focus()
    End Sub

    Private Sub Lv_Lokasi2_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Lokasi2.DoubleClick
        If Lv_Lokasi2.Items.Count = 0 Or Lv_Lokasi2.FocusedItem.Index = -1 Then Exit Sub

        Dim KdLokasi As String = Lv_Lokasi2.FocusedItem.SubItems(0).Text
        Dim NmLokasi As String = Lv_Lokasi2.FocusedItem.SubItems(1).Text

        Txt_KdSoTujuan.Text = KdLokasi
        Txt_NmLokasiTujuan.Text = NmLokasi

        Me.Size = New Size(607, 360)
        Lv_Lokasi2.Location = New Point(600, 230)
        Lv_Lokasi2.Visible = False

        Txt_KdBarang.Focus()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmBarang

        Me.Size = New Size(607, 360)
        Lv_Barang.Location = New Point(600, 256)
        Lv_Barang.Visible = False

        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Lokasi_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Lokasi.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Lokasi_DoubleClick(Lv_Lokasi, e)
        End If
    End Sub

    Private Sub Lv_Lokasi2_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Lokasi2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Lokasi2_DoubleClick(Lv_Lokasi2, e)
        End If
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    '====================================================================================================================================
    '=     HANDLE KEY PRESS
    '====================================================================================================================================

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Laporan.DroppedDown = True
            Cmb_Laporan.Focus()
        End If
    End Sub

    Private Sub Txt_KdSoAwal_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdSoAwal.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Lokasi.Focus()
    End Sub

    Private Sub Txt_KdSoAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdSoAwal.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdSoAwal.Text.Trim.Length = 0 Then Txt_KdSoAwal.Focus()
            Txt_KdSoAwal_Leave(Txt_KdSoAwal, e)

            Me.Size = New Size(607, 360)
            Lv_Lokasi.Location = New Point(600, 204)
            Lv_Lokasi.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdSoTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdSoTujuan.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdSoTujuan.Text.Trim.Length = 0 Then Txt_KdSoTujuan.Focus()
            Txt_KdSoTujuan_Leave(Txt_KdSoTujuan, e)

            Me.Size = New Size(607, 360)
            Lv_Lokasi2.Location = New Point(600, 230)
            Lv_Lokasi2.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Me.Size = New Size(607, 360)
            Lv_Barang.Location = New Point(600, 256)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NmLokasiAwal_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmLokasiAwal.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Lokasi.Focus()
    End Sub

    Private Sub Txt_NmLokasiTujuan_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmLokasiTujuan.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Lokasi2.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmLokasiAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmLokasiAwal.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdSoAwal_Leave(Txt_NmLokasiAwal, e)

            Me.Size = New Size(607, 360)
            Lv_Lokasi.Location = New Point(600, 204)
            Lv_Lokasi.Visible = False

            'Cmb_ParamLain.Focus()
        End If
    End Sub

    Private Sub Txt_NmLokasiTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmLokasiTujuan.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdSoTujuan_Leave(Txt_NmLokasiTujuan, e)

            Me.Size = New Size(607, 360)
            Lv_Lokasi2.Location = New Point(600, 230)
            Lv_Lokasi2.Visible = False

            'Cmb_ParamLain.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Me.Size = New Size(607, 360)
            Lv_Barang.Location = New Point(600, 256)
            Lv_Barang.Visible = False

            'Cmb_ParamLain.Focus()
        End If
    End Sub

    Private Sub Txt_KdSoTujuan_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdSoTujuan.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Lokasi2.Focus()
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub CmbJenisBiaya_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles CmbJenisBiaya.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub Txt_NoSplit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoSplit.KeyPress
        If e.KeyChar = Chr(13) Then Chk_NoSplit.Focus()
    End Sub

    Private Sub Txt_Batch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Batch.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdSoAwal.Focus()
    End Sub

    Private Sub Chk_JenisBiaya_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_NoSplit.CheckedChanged
        If Chk_NoSplit.Checked Then
            Txt_NoSplit.Enabled = True
        Else
            Txt_NoSplit.Enabled = False
        End If
        Txt_NoSplit.Text = ""
    End Sub

    Private Sub Chk_Batch_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Batch.CheckedChanged
        If Chk_Batch.Checked Then
            Txt_Batch.Enabled = True
        Else
            Txt_Batch.Enabled = False
        End If
        Txt_Batch.Text = ""
    End Sub

    Private Sub CmbJenisBiaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Laporan.KeyPress
        If e.KeyChar = Chr(13) Then

            If arrReport(Cmb_Laporan.SelectedIndex) = "Laporan_Biaya_Produksi" Then
                CmbJenisBiaya.Focus()

            ElseIf arrReport(Cmb_Laporan.SelectedIndex) = "Rpt_Laporan_Validasi_GR" Or arrReport(Cmb_Laporan.SelectedIndex) = "Rpt_Laporan_Validasi_GR_Nominal" Then
                Txt_NoSplit.Focus()
            Else
                BtnCetak.Focus()
            End If

        End If

    End Sub

    '=======================================================================================================================
    '=     PACKAGING SEKUNDER
    '=======================================================================================================================
    Private Sub Txt_Pack_Sekunder_NoTransaksi_TextChanged(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NoTransaksi.TextChanged

        If switchAutoComplete = True Then Exit Sub

        If Txt_Pack_Sekunder_NoTransaksi.Text.Trim.Length = 0 Then
            Lv_Pack_Sekunder_NoTransaksi.Visible = False
            Lv_Pack_Sekunder_NoTransaksi.Location = New Point(1120, 155)
            Txt_Pack_Sekunder_NoTransaksi.Text = ""
            Exit Sub
        Else
            Lv_Pack_Sekunder_NoTransaksi.Location = New Point(132, 155)
            Lv_Pack_Sekunder_NoTransaksi.Visible = True
        End If

        Try
            OpenConn()

            Lv_Pack_Sekunder_NoTransaksi.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pack_Sekunder_NoTransaksi.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select No_Transaksi, Tanggal, Keterangan "
            SQL = SQL & "from Emi_Production_Results_Validation "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Status is null "
            SQL = SQL & "and No_Transaksi like '%" & Txt_Pack_Sekunder_NoTransaksi.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Pack_Sekunder_NoTransaksi.Items.Add(Dr("No_Transaksi"))
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

    Private Sub Txt_Pack_Sekunder_NoTransaksi_Leave(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NoTransaksi.Leave
        If Txt_Pack_Sekunder_NoTransaksi.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Pack_Sekunder_NoTransaksi.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Pack_Sekunder_NoTransaksi.Text.ToUpper = OpsiSeluruh.ToUpper Then

                switchAutoComplete = True
                SQL = "select No_Transaksi, Tanggal, Keterangan "
                SQL = SQL & "from Emi_Production_Results_Validation "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Status is null "
                SQL = SQL & "and No_Transaksi = '" & Txt_Pack_Sekunder_NoTransaksi.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Pack_Sekunder_NoTransaksi.Text = Dr("No_Transaksi")
                        Txt_Pack_Sekunder_NoSplit.Focus()
                    Else
                        MessageBox.Show("No Transaksi tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Pack_Sekunder_NoTransaksi.Text = ""
                        Txt_Pack_Sekunder_NoTransaksi.Focus()
                    End If

                    Me.Size = New Size(607, 360)
                    Lv_Pack_Sekunder_NoTransaksi.Visible = False
                    Lv_Pack_Sekunder_NoTransaksi.Location = New Point(1120, 155)
                End Using
                switchAutoComplete = False
            Else
                Txt_Pack_Sekunder_NoSplit.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Pack_Sekunder_NoTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Pack_Sekunder_NoTransaksi.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Pack_Sekunder_NoTransaksi.Text.Trim.Length = 0 Then Txt_Pack_Sekunder_NoTransaksi.Focus()
            Txt_Pack_Sekunder_NoTransaksi_Leave(Txt_Pack_Sekunder_NoTransaksi, e)

            Me.Size = New Size(607, 360)
            Lv_Pack_Sekunder_NoTransaksi.Visible = False
            Lv_Pack_Sekunder_NoTransaksi.Location = New Point(1120, 155)

        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_NoTransaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Pack_Sekunder_NoTransaksi.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pack_Sekunder_NoTransaksi.Focus()
    End Sub

    Private Sub Lv_Pack_Sekunder_NoTransaksi_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Pack_Sekunder_NoTransaksi.DoubleClick
        If Lv_Pack_Sekunder_NoTransaksi.Items.Count = 0 Or Lv_Pack_Sekunder_NoTransaksi.FocusedItem.Index = -1 Then Exit Sub

        Dim NoTransaksi As String = Lv_Pack_Sekunder_NoTransaksi.FocusedItem.SubItems(0).Text

        switchAutoComplete = True
        Txt_Pack_Sekunder_NoTransaksi.Text = NoTransaksi
        switchAutoComplete = False

        Me.Size = New Size(607, 360)
        Lv_Pack_Sekunder_NoTransaksi.Visible = False
        Lv_Pack_Sekunder_NoTransaksi.Location = New Point(1120, 155)

        Txt_Pack_Sekunder_NoSplit.Focus()
    End Sub

    Private Sub Lv_Pack_Sekunder_NoTransaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Pack_Sekunder_NoTransaksi.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Pack_Sekunder_NoTransaksi_DoubleClick(Lv_Pack_Sekunder_NoTransaksi, e)
        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_NoSplit_TextChanged(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NoSplit.TextChanged

        If switchAutoComplete = True Then Exit Sub

        If Txt_Pack_Sekunder_NoSplit.Text.Trim.Length = 0 Then
            Lv_Pack_Sekunder_NoSplit.Visible = False
            Lv_Pack_Sekunder_NoSplit.Location = New Point(1120, 181)
            Txt_Pack_Sekunder_NoSplit.Text = ""
            Me.Size = New Size(607, 360)
            Exit Sub
        Else
            Lv_Pack_Sekunder_NoSplit.Location = New Point(132, 181)
            Lv_Pack_Sekunder_NoSplit.Visible = True
            Me.Size = New Size(607, 370)
        End If

        Try
            OpenConn()

            Lv_Pack_Sekunder_NoSplit.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pack_Sekunder_NoSplit.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select distinct a.No_Production_Order, b.Tgl_Produksi "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Split_Production_Order b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.no_production_order = b.No_Transaksi "
            SQL = SQL & "and a.Status is null and b.Status is null "
            SQL = SQL & "and a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order like '%" & Txt_Pack_Sekunder_NoSplit.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Pack_Sekunder_NoSplit.Items.Add(Dr("No_Production_Order"))
                    Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Pack_Sekunder_NoSplit_Leave(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NoSplit.Leave
        If Txt_Pack_Sekunder_NoSplit.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Pack_Sekunder_NoSplit.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Pack_Sekunder_NoSplit.Text.ToUpper = OpsiSeluruh.ToUpper Then

                switchAutoComplete = True
                SQL = "select distinct a.No_Production_Order, b.Tgl_Produksi "
                SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Split_Production_Order b "
                SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
                SQL = SQL & "and a.no_production_order = b.No_Transaksi "
                SQL = SQL & "and a.Status is null and b.Status is null "
                SQL = SQL & "and a.Status is null and b.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Production_Order = '" & Txt_Pack_Sekunder_NoSplit.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Pack_Sekunder_NoSplit.Text = Dr("No_Transaksi")
                        Txt_Pack_Sekunder_KdBarang.Focus()
                    Else
                        MessageBox.Show("No Transaksi tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Pack_Sekunder_NoSplit.Text = ""
                        Txt_Pack_Sekunder_NoSplit.Focus()
                    End If

                    Me.Size = New Size(607, 360)
                    Lv_Pack_Sekunder_NoSplit.Visible = False
                    Lv_Pack_Sekunder_NoSplit.Location = New Point(1120, 181)
                End Using
                switchAutoComplete = False
            Else
                Txt_Pack_Sekunder_KdBarang.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Pack_Sekunder_NoSplit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Pack_Sekunder_NoSplit.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Pack_Sekunder_NoSplit.Text.Trim.Length = 0 Then Txt_Pack_Sekunder_NoSplit.Focus()
            Txt_Pack_Sekunder_NoSplit_Leave(Txt_Pack_Sekunder_NoSplit, e)

            Me.Size = New Size(607, 360)
            Lv_Pack_Sekunder_NoSplit.Visible = False
            Lv_Pack_Sekunder_NoSplit.Location = New Point(1120, 181)

        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_NoSplit_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Pack_Sekunder_NoSplit.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pack_Sekunder_NoSplit.Focus()
    End Sub

    Private Sub Lv_Pack_Sekunder_NoSplit_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Pack_Sekunder_NoSplit.DoubleClick
        If Lv_Pack_Sekunder_NoSplit.Items.Count = 0 Or Lv_Pack_Sekunder_NoSplit.FocusedItem.Index = -1 Then Exit Sub

        Dim NoTransaksi As String = Lv_Pack_Sekunder_NoSplit.FocusedItem.SubItems(0).Text

        switchAutoComplete = True
        Txt_Pack_Sekunder_NoSplit.Text = NoTransaksi
        switchAutoComplete = False

        Me.Size = New Size(607, 360)
        Lv_Pack_Sekunder_NoSplit.Visible = False
        Lv_Pack_Sekunder_NoSplit.Location = New Point(1120, 181)

        Txt_Pack_Sekunder_KdBarang.Focus()
    End Sub

    Private Sub Lv_Pack_Sekunder_NoSplit_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Pack_Sekunder_NoSplit.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Pack_Sekunder_NoSplit_DoubleClick(Lv_Pack_Sekunder_NoSplit, e)
        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_KdBarang.TextChanged

        If switchAutoComplete = True Then Exit Sub

        If Txt_Pack_Sekunder_KdBarang.Text.Trim.Length = 0 Then
            Lv_Pack_Sekunder_Barang.Visible = False
            Lv_Pack_Sekunder_Barang.Location = New Point(1120, 207)
            Txt_Pack_Sekunder_KdBarang.Text = ""
            Txt_Pack_Sekunder_NmBahan.Text = ""
            Me.Size = New Size(607, 360)
            Exit Sub
        Else
            Lv_Pack_Sekunder_Barang.Location = New Point(132, 207)
            Lv_Pack_Sekunder_Barang.Visible = True
            Me.Size = New Size(607, 395)
        End If

        Try
            OpenConn()

            Lv_Pack_Sekunder_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pack_Sekunder_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, emi_group_jenis b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.Flag_Produksi = 'Y' "
            SQL = SQL & "and a.Kode_Barang like '%" & Txt_Pack_Sekunder_KdBarang.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Pack_Sekunder_Barang.Items.Add(Dr("Kode_Barang"))
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

    Private Sub Txt_Pack_Sekunder_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_KdBarang.Leave
        If Txt_Pack_Sekunder_KdBarang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Pack_Sekunder_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Pack_Sekunder_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then

                switchAutoComplete = True
                SQL = "select distinct a.Kode_Barang, a.Nama "
                SQL = SQL & "from barang a, emi_group_jenis b "
                SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.Flag_Produksi = 'Y' "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_Pack_Sekunder_KdBarang.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Pack_Sekunder_KdBarang.Text = Dr("Kode_Barang")
                        Txt_Pack_Sekunder_NmBarang.Text = Dr("Nama")
                        Txt_Pack_Sekunder_KdBarang.Focus()
                    Else
                        MessageBox.Show("Kode Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Pack_Sekunder_KdBarang.Text = ""
                        Txt_Pack_Sekunder_NmBarang.Text = ""
                        Txt_Pack_Sekunder_KdBarang.Focus()
                    End If

                    Me.Size = New Size(607, 360)
                    Lv_Pack_Sekunder_Barang.Visible = False
                    Lv_Pack_Sekunder_Barang.Location = New Point(1120, 207)
                End Using
                switchAutoComplete = False
            Else
                Txt_Pack_Sekunder_KdBahan.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Pack_Sekunder_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Pack_Sekunder_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Pack_Sekunder_KdBarang.Text.Trim.Length = 0 Then Txt_Pack_Sekunder_KdBarang.Focus()
            Txt_Pack_Sekunder_KdBarang_Leave(Txt_Pack_Sekunder_KdBarang, e)

            Me.Size = New Size(607, 360)
            Lv_Pack_Sekunder_Barang.Visible = False
            Lv_Pack_Sekunder_Barang.Location = New Point(1120, 207)

        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Pack_Sekunder_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pack_Sekunder_Barang.Focus()
    End Sub

    Private Sub Lv_Pack_Sekunder_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Pack_Sekunder_Barang.DoubleClick
        If Lv_Pack_Sekunder_Barang.Items.Count = 0 Or Lv_Pack_Sekunder_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Pack_Sekunder_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Pack_Sekunder_Barang.FocusedItem.SubItems(1).Text

        switchAutoComplete = True
        Txt_Pack_Sekunder_KdBarang.Text = KdBarang
        Txt_Pack_Sekunder_NmBarang.Text = NmBarang
        switchAutoComplete = False

        Me.Size = New Size(607, 360)
        Lv_Pack_Sekunder_Barang.Visible = False
        Lv_Pack_Sekunder_Barang.Location = New Point(1120, 207)

        Txt_Pack_Sekunder_KdBahan.Focus()
    End Sub

    Private Sub Lv_Pack_Sekunder_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Pack_Sekunder_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Pack_Sekunder_Barang_DoubleClick(Lv_Pack_Sekunder_Barang, e)
        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NmBarang.TextChanged

        If switchAutoComplete = True Then Exit Sub

        If Txt_Pack_Sekunder_NmBarang.Text.Trim.Length = 0 Then
            Lv_Pack_Sekunder_Barang.Visible = False
            Lv_Pack_Sekunder_Barang.Location = New Point(1120, 207)
            Txt_Pack_Sekunder_KdBarang.Text = ""
            Txt_Pack_Sekunder_NmBahan.Text = ""
            Me.Size = New Size(607, 360)
            Exit Sub
        Else
            Lv_Pack_Sekunder_Barang.Location = New Point(132, 207)
            Lv_Pack_Sekunder_Barang.Visible = True
            Me.Size = New Size(607, 395)
        End If

        Try
            OpenConn()

            Lv_Pack_Sekunder_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pack_Sekunder_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, emi_group_jenis b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.Flag_Produksi = 'Y' "
            SQL = SQL & "and a.Nama like '%" & Txt_Pack_Sekunder_NmBarang.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Pack_Sekunder_Barang.Items.Add(Dr("Kode_Barang"))
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

    Private Sub Txt_Pack_Sekunder_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Pack_Sekunder_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Pack_Sekunder_NmBarang.Text.Trim.Length = 0 Then Txt_Pack_Sekunder_NmBarang.Focus()
            Txt_Pack_Sekunder_KdBarang_Leave(Txt_Pack_Sekunder_NmBarang, e)

            Me.Size = New Size(607, 360)
            Lv_Pack_Sekunder_Barang.Visible = False
            Lv_Pack_Sekunder_Barang.Location = New Point(1120, 207)

        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Pack_Sekunder_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pack_Sekunder_Barang.Focus()
    End Sub

    Private Sub Txt_Pack_Sekunder_KdBahan_TextChanged(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_KdBahan.TextChanged

        If switchAutoComplete = True Then Exit Sub

        If Txt_Pack_Sekunder_KdBahan.Text.Trim.Length = 0 Then
            Lv_Pack_Sekunder_Bahan.Visible = False
            Lv_Pack_Sekunder_Bahan.Location = New Point(1120, 233)
            Txt_Pack_Sekunder_KdBahan.Text = ""
            Txt_Pack_Sekunder_NmBahan.Text = ""
            Me.Size = New Size(607, 360)
            Exit Sub
        Else
            Lv_Pack_Sekunder_Bahan.Location = New Point(132, 233)
            Lv_Pack_Sekunder_Bahan.Visible = True
            Me.Size = New Size(607, 420)
        End If

        Try
            OpenConn()

            Lv_Pack_Sekunder_Bahan.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pack_Sekunder_Bahan.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, emi_group_jenis b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.flag_packaging = 'Y' "
            SQL = SQL & "and a.Kode_Barang like '%" & Txt_Pack_Sekunder_KdBahan.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Pack_Sekunder_Bahan.Items.Add(Dr("Kode_Barang"))
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

    Private Sub Txt_Pack_Sekunder_KdBahan_Leave(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_KdBahan.Leave
        If Txt_Pack_Sekunder_KdBahan.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Pack_Sekunder_Bahan.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Pack_Sekunder_KdBahan.Text.ToUpper = OpsiSeluruh.ToUpper Then

                switchAutoComplete = True
                SQL = "select distinct a.Kode_Barang, a.Nama "
                SQL = SQL & "from barang a, emi_group_jenis b "
                SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.flag_packaging = 'Y' "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_Pack_Sekunder_KdBahan.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Pack_Sekunder_KdBahan.Text = Dr("Kode_Barang")
                        Txt_Pack_Sekunder_NmBahan.Text = Dr("Nama")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Kode Bahan tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Pack_Sekunder_KdBahan.Text = ""
                        Txt_Pack_Sekunder_NmBahan.Text = ""
                        Txt_Pack_Sekunder_KdBahan.Focus()
                    End If

                    Me.Size = New Size(607, 360)
                    Lv_Pack_Sekunder_Bahan.Visible = False
                    Lv_Pack_Sekunder_Bahan.Location = New Point(1120, 233)
                End Using
                switchAutoComplete = False
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

    Private Sub Txt_Pack_Sekunder_KdBahan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Pack_Sekunder_KdBahan.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Pack_Sekunder_KdBahan.Text.Trim.Length = 0 Then Txt_Pack_Sekunder_KdBahan.Focus()
            Txt_Pack_Sekunder_KdBahan_Leave(Txt_Pack_Sekunder_KdBahan, e)

            Me.Size = New Size(607, 360)
            Lv_Pack_Sekunder_Bahan.Visible = False
            Lv_Pack_Sekunder_Bahan.Location = New Point(1120, 233)

        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_KdBahan_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Pack_Sekunder_KdBahan.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pack_Sekunder_Bahan.Focus()
    End Sub

    Private Sub Txt_Pack_Sekunder_NmBahan_TextChanged(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NmBahan.TextChanged

        If switchAutoComplete = True Then Exit Sub

        If Txt_Pack_Sekunder_NmBahan.Text.Trim.Length = 0 Then
            Lv_Pack_Sekunder_Bahan.Visible = False
            Lv_Pack_Sekunder_Bahan.Location = New Point(1120, 233)
            Txt_Pack_Sekunder_KdBahan.Text = ""
            Txt_Pack_Sekunder_NmBahan.Text = ""
            Me.Size = New Size(607, 360)
            Exit Sub
        Else
            Lv_Pack_Sekunder_Bahan.Location = New Point(132, 233)
            Lv_Pack_Sekunder_Bahan.Visible = True
            Me.Size = New Size(607, 420)
        End If

        Try
            OpenConn()

            Lv_Pack_Sekunder_Bahan.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Pack_Sekunder_Bahan.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, emi_group_jenis b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.flag_packaging = 'Y' "
            SQL = SQL & "and a.Nama like '%" & Txt_Pack_Sekunder_NmBahan.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Pack_Sekunder_Bahan.Items.Add(Dr("Kode_Barang"))
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

    Private Sub Txt_Pack_Sekunder_NmBahan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Pack_Sekunder_NmBahan.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Pack_Sekunder_NmBahan.Text.Trim.Length = 0 Then Txt_Pack_Sekunder_NmBahan.Focus()
            Txt_Pack_Sekunder_KdBahan_Leave(Txt_Pack_Sekunder_NmBahan, e)

            Me.Size = New Size(607, 360)
            Lv_Pack_Sekunder_Bahan.Visible = False
            Lv_Pack_Sekunder_Bahan.Location = New Point(1120, 233)

        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_NmBahan_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Pack_Sekunder_NmBahan.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Pack_Sekunder_Bahan.Focus()
    End Sub

    Private Sub Lv_Pack_Sekunder_Bahan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Pack_Sekunder_Bahan.DoubleClick
        If Lv_Pack_Sekunder_Bahan.Items.Count = 0 Or Lv_Pack_Sekunder_Bahan.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Pack_Sekunder_Bahan.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Pack_Sekunder_Bahan.FocusedItem.SubItems(1).Text

        switchAutoComplete = True
        Txt_Pack_Sekunder_KdBahan.Text = KdBarang
        Txt_Pack_Sekunder_NmBahan.Text = NmBarang
        switchAutoComplete = False

        Me.Size = New Size(607, 360)
        Lv_Pack_Sekunder_Bahan.Visible = False
        Lv_Pack_Sekunder_Bahan.Location = New Point(1120, 233)

        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Pack_Sekunder_Bahan_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Pack_Sekunder_Bahan.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Pack_Sekunder_Bahan_DoubleClick(Lv_Pack_Sekunder_Bahan, e)
        End If
    End Sub

    Private Sub Txt_Pack_Sekunder_NoTransaksi_Enter(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NoTransaksi.Enter
        Txt_Pack_Sekunder_NoTransaksi.SelectAll()
    End Sub

    Private Sub Txt_Pack_Sekunder_NoSplit_Enter(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NoSplit.Enter
        Txt_Pack_Sekunder_NoSplit.SelectAll()
    End Sub

    Private Sub Txt_Pack_Sekunder_KdBarang_Enter(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_KdBarang.Enter
        Txt_Pack_Sekunder_KdBarang.SelectAll()
    End Sub

    Private Sub Txt_Pack_Sekunder_NmBarang_Enter(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NmBarang.Enter
        Txt_Pack_Sekunder_NmBarang.SelectAll()
    End Sub

    Private Sub Txt_Pack_Sekunder_KdBahan_Enter(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_KdBahan.Enter
        Txt_Pack_Sekunder_KdBahan.SelectAll()
    End Sub

    Private Sub Txt_Pack_Sekunder_NmBahan_Enter(sender As Object, e As EventArgs) Handles Txt_Pack_Sekunder_NmBahan.Enter
        Txt_Pack_Sekunder_NmBahan.SelectAll()
    End Sub




























End Class
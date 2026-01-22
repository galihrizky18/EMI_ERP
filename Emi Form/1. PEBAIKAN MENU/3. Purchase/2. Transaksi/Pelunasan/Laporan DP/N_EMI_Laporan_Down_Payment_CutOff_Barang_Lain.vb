Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class N_EMI_Laporan_Down_Payment_CutOff_Barang_Lain

    Dim Switch_AutoComplete As Boolean = False

    Private Sub N_EMI_Laporan_Down_Payment_CutOff_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Transaksi.Columns.Clear()
        Lv_Transaksi.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
        Lv_Transaksi.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
        Lv_Transaksi.View = View.Details

        Cmb_JenisLaporan.Items.Clear()
        Cmb_JenisLaporan.Items.Add("Laporan Down Payment")
        Cmb_JenisLaporan.Items.Add("Laporan Down Payment Rekap")
        Cmb_JenisLaporan.SelectedIndex = 0

        Me.Size = New Size(561, 268)

        Kosong()

    End Sub

    Private Sub Kosong()

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date


        Switch_AutoComplete = True
        Txt_NoTransaksi.Text = OpsiSeluruh
        Switch_AutoComplete = False

        Tgl1.Focus()
    End Sub

    '=====================================================================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '=====================================================================================================================================================================================

    Private Sub Txt_NoTransaksi_TextChanged(sender As Object, e As EventArgs) Handles Txt_NoTransaksi.TextChanged
        If Switch_AutoComplete Then Exit Sub

        If Txt_NoTransaksi.Text.Trim.Length = 0 Then
            Me.Size = New Size(561, 268)
            Lv_Transaksi.Visible = False
            Lv_Transaksi.Location = New Point(550, 155)
            Txt_NoTransaksi.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(561, 360)
            Lv_Transaksi.Location = New Point(140, 155)
            Lv_Transaksi.Visible = True
        End If

        Try
            OpenConn()

            Lv_Transaksi.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Transaksi.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            '==========================
            '=     FILTER SECTION     =
            '==========================
            Dim Filter As String = ""

#Region "Filter Section"

            If Txt_NoTransaksi.Text.Trim.Length > 0 Then
                Filter = " and a.No_Transaksi like '%" & Txt_NoTransaksi.Text.Trim & "%' "
            End If

#End Region

            '       SQL = "select distinct a.No_Transaksi, a.Keterangan
            '		from EMI_Transaksi_Pembayaran_Dimuka_Asset a
            '		inner join EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
            '		cross apply (
            '			select round(sum(z.Nilai), 0) as Total_Pajak from EMI_Transaksi_Pembayaran_Dimuka_Asset_Pajak z
            '				where z.Kode_Perusahaan = b.Kode_Perusahaan
            '				and z.No_Faktur = b.No_Transaksi and z.Flag_PPN is null
            '			) Pajak
            '			cross apply (
            '				select isnull(sum(z.Nilai), 0) as DP_Digunakan
            '				from EMI_Pelunasan_Detail_DP_Barang_Lain z, EMI_Pelunasan_Barang_Lain y  
            '				where z.Kode_Perusahaan = y.Kode_Perusahaan  
            '					and z.No_Val = y.No_Val   
            '					and y.Status is null  
            '					and z.Kode_Perusahaan = b.Kode_Perusahaan  
            '					and z.Urut_DP = b.No_Urut  
            '			) DP_Digunakan
            '		where a.Status is null
            '		and a.Kode_Perusahaan = '" & KodePerusahaan & "'
            '		and (b.Nilai - ISNULL(Pajak.Total_Pajak, 0) - ISNULL(DP_Digunakan.DP_Digunakan,0)) <> 0 
            '		" & Filter & "
            '		order by a.No_Transaksi;
            '"

            SQL = "select distinct a.No_Transaksi, a.Keterangan
                    from EMI_Transaksi_Pembayaran_Dimuka_Asset a
	                    inner join EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
                    where a.Status is null
                    and a.Kode_Perusahaan = '" & KodePerusahaan & "'
                    " & Filter & "
                    order by a.No_Transaksi;
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Transaksi.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "-", Dr("Keterangan")))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NoTransaksi_Leave(sender As Object, e As EventArgs) Handles Txt_NoTransaksi.Leave
        If Txt_NoTransaksi.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Transaksi.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_NoTransaksi.Text.ToUpper = OpsiSeluruh.ToUpper Then

                '             SQL = "select distinct a.No_Transaksi, a.Keterangan
                '		from EMI_Transaksi_Pembayaran_Dimuka_Asset a
                '		inner join EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
                '		cross apply (
                '			select round(sum(z.Nilai), 0) as Total_Pajak from EMI_Transaksi_Pembayaran_Dimuka_Asset_Pajak z
                '				where z.Kode_Perusahaan = b.Kode_Perusahaan
                '				and z.No_Faktur = b.No_Transaksi and z.Flag_PPN is null
                '			) Pajak
                '			cross apply (
                '				select isnull(sum(z.Nilai), 0) as DP_Digunakan
                '				from EMI_Pelunasan_Detail_DP_Barang_Lain z, EMI_Pelunasan_Barang_Lain y  
                '				where z.Kode_Perusahaan = y.Kode_Perusahaan  
                '					and z.No_Val = y.No_Val   
                '					and y.Status is null  
                '					and z.Kode_Perusahaan = b.Kode_Perusahaan  
                '					and z.Urut_DP = b.No_Urut  
                '			) DP_Digunakan
                '		where a.Status is null
                '		and a.Kode_Perusahaan = '" & KodePerusahaan & "'
                '		and (b.Nilai - ISNULL(Pajak.Total_Pajak, 0) - ISNULL(DP_Digunakan.DP_Digunakan,0)) <> 0 
                '		and a.No_Transaksi = '" & Txt_NoTransaksi.Text.Trim & "'
                '		order by a.No_Transaksi
                '"

                SQL = "select distinct a.No_Transaksi, a.Keterangan
                    from EMI_Transaksi_Pembayaran_Dimuka_Asset a
	                    inner join EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
                    where a.Status is null
                    and a.Kode_Perusahaan = '" & KodePerusahaan & "'
                    and a.No_Transaksi = '" & Txt_NoTransaksi.Text.Trim & "'
                    order by a.No_Transaksi;
                "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_NoTransaksi.Text = Dr("No_Transaksi")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Down Payment Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_NoTransaksi.Text = ""
                        Txt_NoTransaksi.Focus()
                    End If

                    Me.Size = New Size(561, 268)
                    Lv_Transaksi.Visible = False
                    Lv_Transaksi.Location = New Point(550, 155)
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

    Private Sub Txt_NoTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoTransaksi.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_NoTransaksi.Text.Trim.Length = 0 Then Txt_NoTransaksi.Focus()
            Txt_NoTransaksi_Leave(Txt_NoTransaksi, e)

            Me.Size = New Size(561, 268)
            Lv_Transaksi.Visible = False
            Lv_Transaksi.Location = New Point(550, 155)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NoTransaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NoTransaksi.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Transaksi.Focus()
    End Sub

    Private Sub Lv_Transaksi_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Transaksi.DoubleClick
        If Lv_Transaksi.Items.Count = 0 Or Lv_Transaksi.FocusedItem.Index = -1 Then Exit Sub

        Dim IdCostCenter As String = Lv_Transaksi.FocusedItem.SubItems(0).Text
        Dim NmCostCenter As String = Lv_Transaksi.FocusedItem.SubItems(1).Text

        Switch_AutoComplete = True
        Txt_NoTransaksi.Text = IdCostCenter
        Switch_AutoComplete = False

        Me.Size = New Size(561, 268)
        Lv_Transaksi.Visible = False
        Lv_Transaksi.Location = New Point(550, 155)

        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Transaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Transaksi.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Transaksi_DoubleClick(Lv_Transaksi, e)
        End If
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_JenisLaporan.DroppedDown = True
            Cmb_JenisLaporan.Focus()
        End If
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub

        ElseIf Cmb_JenisLaporan.Text.Trim.Length = 0 Then
            MessageBox.Show("Jenis Laporan Harus Dipilih Dahulu", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_JenisLaporan.Focus() : Exit Sub
        ElseIf Txt_NoTransaksi.Text.Trim.Length = 0 Then
            MessageBox.Show("No Transaksi Tidak Boleh Kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoTransaksi.Focus() : Exit Sub
        End If

        Dim TglDari = Format(Tgl1.Value, "yyyy-MM-dd")
        Dim TglSampai = Format(Tgl2.Value, "yyyy-MM-dd")

        Try
            OpenConn()


            Dim Auth As String = ""
            SQL = "select Auth_SP from Users where kode_perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Auth = Dr("Auth_SP")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("User Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Filter As String = ""
            Dim Filter_SP As String = ""

            If Txt_NoTransaksi.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                Filter = "NULL "
                Filter_SP = "NULL"
            Else
                Filter = "'" & Txt_NoTransaksi.Text.Trim & "' "
                Filter_SP = Txt_NoTransaksi.Text.Trim
            End If

            SQL = "exec N_EMI_SP_Down_Payment_Cut_Off_Barang_Lain @Kode_Perusahaan='" & KodePerusahaan & "', "
            SQL = SQL & "@Tanggal_Awal_Input='" & TglDari & "', @Tanggal_Akhir_Input='" & TglSampai & "', "
            SQL = SQL & "@UserID='" & UserID & "', @Auth_SP='" & Auth & "', @No_DP=" & Filter
            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim SelectionRecord As String = ""
                        Dim CrDoc As Object
                        If Cmb_JenisLaporan.SelectedIndex = 0 Then
                            CrDoc = New N_EMI_CR_Laporan_Down_Payment_CutOff_Barang_Lain
                        Else
                            CrDoc = New N_EMI_CR_Laporan_Down_Payment_CutOff_Rekap_Barang_Lain
                            SelectionRecord = "{N_EMI_SP_Down_Payment_Cut_Off_Barang_Lain.Sisa} <> 0"
                        End If

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        With A_Place_For_Printing2
                            'CrDoc.SetDataSource(DS)
                            'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            'CrDoc.SetParameterValue("@Kode_Perusahaan", KodePerusahaan)
                            'CrDoc.SetParameterValue("@Tanggal_Awal_Input", TglDari)
                            'CrDoc.SetParameterValue("@Tanggal_Akhir_Input", TglSampai)
                            'CrDoc.SetParameterValue("@UserID", UserID)
                            'CrDoc.SetParameterValue("@Auth_SP", Auth)
                            'CrDoc.SetParameterValue("@No_DP", Filter)



                            Dim crParameterDiscreteValue As ParameterDiscreteValue
                            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
                            Dim crParameterFieldLocation As ParameterFieldDefinition
                            Dim crParameterValues As ParameterValues

                            crParameterFieldDefinitions = CrDoc.DataDefinition.ParameterFields

                            crParameterFieldLocation = crParameterFieldDefinitions.Item("@Kode_Perusahaan")
                            crParameterValues = crParameterFieldLocation.CurrentValues
                            crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                            crParameterDiscreteValue.Value = KodePerusahaan
                            crParameterValues.Add(crParameterDiscreteValue)
                            crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                            crParameterFieldLocation = crParameterFieldDefinitions.Item("@Tanggal_Awal_Input")
                            crParameterValues = crParameterFieldLocation.CurrentValues
                            crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                            crParameterDiscreteValue.Value = Format(Tgl1.Value, "yyyy-MM-dd")
                            crParameterValues.Add(crParameterDiscreteValue)
                            crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                            crParameterFieldLocation = crParameterFieldDefinitions.Item("@Tanggal_Akhir_Input")
                            crParameterValues = crParameterFieldLocation.CurrentValues
                            crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                            crParameterDiscreteValue.Value = Format(Tgl2.Value, "yyyy-MM-dd")
                            crParameterValues.Add(crParameterDiscreteValue)
                            crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                            crParameterFieldLocation = crParameterFieldDefinitions.Item("@UserID")
                            crParameterValues = crParameterFieldLocation.CurrentValues
                            crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                            crParameterDiscreteValue.Value = UserID
                            crParameterValues.Add(crParameterDiscreteValue)
                            crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                            crParameterFieldLocation = crParameterFieldDefinitions.Item("@Auth_SP")
                            crParameterValues = crParameterFieldLocation.CurrentValues
                            crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                            crParameterDiscreteValue.Value = Auth
                            crParameterValues.Add(crParameterDiscreteValue)
                            crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                            crParameterFieldLocation = crParameterFieldDefinitions.Item("@No_DP")
                            crParameterValues = crParameterFieldLocation.CurrentValues
                            crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                            If Filter_SP = "NULL" Then
                                crParameterDiscreteValue.Value = DBNull.Value
                            Else
                                crParameterDiscreteValue.Value = Filter_SP
                            End If
                            crParameterValues.Add(crParameterDiscreteValue)
                            crParameterFieldLocation.ApplyCurrentValues(crParameterValues)





                            CrDoc.SummaryInfo.ReportTitle = "Periode: " & TglDari & " s/d " & TglSampai
                            CrDoc.RecordSelectionFormula = SelectionRecord

                            .Text = "Laporan Down Payment Cut Off"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.DisplayGroupTree = False
                            .Refresh()
                            .Show()
                            .Focus()
                        End With

                    Else

                        CloseConn()
                        MessageBox.Show("Data Pengeluaran Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub Cmb_JenisLaporan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_JenisLaporan.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NoTransaksi.Focus()
    End Sub
End Class
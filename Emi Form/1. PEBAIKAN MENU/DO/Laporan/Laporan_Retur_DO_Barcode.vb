
Public Class Laporan_Retur_DO_Barcode
    Dim arrKd As New ArrayList
    Dim CrDoc As Object

    Private Sub Laporan_Retur_DO_Barcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Seluruh")
        ComboBox1.Items.Add("Retur diBulan sama dengan PI")
        ComboBox1.Items.Add("Retur lewat Bulan dengan PI")
        ComboBox1.SelectedIndex = 0

        DateTimePicker1.Value = FMenuDevFix.ToolStripStatusLabel3.Text
        DateTimePicker2.Value = FMenuDevFix.ToolStripStatusLabel3.Text

        Try
            OpenConn()

            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("-- Seluruh --")

            xSplit = CekKotaRole().Split(",")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)

            SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            ComboBox2.Text = Lokasi

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Laporan_Retur_Proforma_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim TglDari = Format(DateTimePicker1.Value, "yyyy-MM-dd")
        Dim TglSampai = Format(DateTimePicker2.Value, "yyyy-MM-dd")
        Dim JenisRetur = ComboBox1.SelectedIndex
        Dim List_Kota As String = ""

        If TglDari > TglSampai Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
            DateTimePicker1.Focus() : Exit Sub
        ElseIf JenisRetur = -1 Then
            MessageBox.Show("Jenis Retur harus diisi.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            If ComboBox2.SelectedIndex = 0 Then
                For x As Integer = 1 To ComboBox2.Items.Count - 1
                    List_Kota = List_Kota & "" & ComboBox2.Items(x).ToString & ", "
                Next
                List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)
            Else
                List_Kota = ComboBox2.Text
            End If

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

            Dim bolehliathpp As String = "T"
            If CekButtonRole("hpp_di_laporan_retur_do") = "Y" Then
                bolehliathpp = "Y"
            Else
                bolehliathpp = "T"
            End If

            SQL = "EXEC sp_tampil_retur_do @kode_perusahaan = '" & KodePerusahaan & "', "
            SQL = SQL & "@tanggal_awal = '" & TglDari & "', @tanggal_akhir = '" & TglSampai & "', "
            SQL = SQL & "@jenis = '" & JenisRetur & "',@arr_lokasi = '" & List_Kota & "',"
            SQL = SQL & "@userid = '" & UserID & "', @Auth_SP ='" & Auth & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    If bolehliathpp = "Y" Then
                        CrDoc = New Rpt_Laporan_Retur_DO_Barcode
                    ElseIf bolehliathpp = "T" Then
                        CrDoc = New Rpt_Laporan_Retur_DO_Tdk_Hpp_Barcode
                    End If

                    With A_Place_For_Printing
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SetParameterValue("@kode_perusahaan", KodePerusahaan)
                        CrDoc.SetParameterValue("@Tanggal_Awal", TglDari)
                        CrDoc.SetParameterValue("@Tanggal_Akhir", TglSampai)
                        CrDoc.SetParameterValue("@Jenis", JenisRetur)
                        CrDoc.SetParameterValue("@Arr_Lokasi", List_Kota)
                        CrDoc.SetParameterValue("@Userid", UserID)
                        CrDoc.SetParameterValue("@Auth_SP", Auth)
                        CrDoc.SummaryInfo.ReportTitle = "Periode: " & TglDari & " s/d " & TglSampai

                        .Text = "Laporan Retur DO"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                        .Focus()
                    End With
                Else
                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox1.Focus()
    End Sub

End Class
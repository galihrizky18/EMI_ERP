
Public Class Rpt_Good_Issue_Per_Split
    Private Sub Rpt_Good_Issue_Per_Split_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        DateTimePicker2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)

        CmbRelease.Items.Clear()
        CmbRelease.Items.Add("Y")
        CmbRelease.Items.Add("T")
        CmbRelease.SelectedIndex = -1
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then CmbRelease.Focus()
    End Sub

    Private Sub CmbRelease_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbRelease.KeyPress
        If e.KeyChar = Chr(13) Then BtCetak.Focus()
    End Sub

    Private Sub BtCetak_Click(sender As Object, e As EventArgs) Handles BtCetak.Click
        Try
            OpenConn()

            Dim CRSF As String
            Dim StrRelease As String

            CRSF = "{View_Detail_Good_Issue_Per_Split.Tanggal_Produksi} >= #" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "# AND "
            CRSF = CRSF & "{View_Detail_Good_Issue_Per_Split.Tanggal_Produksi} <= #" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "# "
            If CmbRelease.Text = "Y" Then
                CRSF = CRSF & "AND {View_Detail_Good_Issue_Per_Split.Flag_Release} = 'Y' "
                StrRelease = "SUDAH RELEASE"
            Else
                CRSF = CRSF & ""
                StrRelease = "SEMUA"
            End If

            SQL = "Select No_Production_Order From View_Detail_Good_Issue_Per_Split Where "
            SQL = SQL & "Tanggal_Produksi >= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND "
            SQL = SQL & "Tanggal_Produksi <= '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            If CmbRelease.Text = "Y" Then
                SQL = SQL & "And Flag_Release = 'Y' "
            Else
                SQL = SQL & ""
            End If
            Using DSCetak = Binding(SQL)
                If DSCetak.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As Object
                    CrDoc = New Report_Good_Issue_Per_Split        'Nama file CR
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(DSCetak)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "REPORT GOOD ISSUE PER SPLIT (" & StrRelease & ")" & Chr(13) & "TGL : " & Format(DateTimePicker1.Value, "dd/MM/yyyy") & " S.D " & Format(DateTimePicker2.Value, "dd/MM/yyyy")
                        CrDoc.RecordSelectionFormula = CRSF
                        .Text = "Good Issue Per Split"

                        .CrystalReportViewer1.ReportSource = CrDoc
                        '.CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                    End With
                Else
                    MessageBox.Show("Tidak ada data yang dapat dicetak . . ! !")
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class
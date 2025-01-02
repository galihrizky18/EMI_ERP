Public Class Laporan_GI_GR
    Private Sub Laporan_Transfer_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
        Tgl1.Focus()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        End If

        Dim SF As String = ""

        OpenConn()

        SQL = "SELECT No_Production_Order FROM View_GI_GR "

        SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "Tanggal_Production_Order between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '"
        SQL = SQL & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

        SF = "{View_GI_GR.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
        SF = SF & "{View_GI_GR.Tanggal_Production_Order} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
        SF = SF & "{View_GI_GR.Tanggal_Production_Order} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "#"

        Using MyDS As DataSet = Binding(SQL)
            With MyDS.Tables(0)
                If .Rows.Count <> 0 Then

                    Dim CrDoc As New Laporan_GI_GR_Rpt

                    CrDoc.SetDataSource(MyDS)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                    CrDoc.RecordSelectionFormula = SF

                    With Print_Form
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
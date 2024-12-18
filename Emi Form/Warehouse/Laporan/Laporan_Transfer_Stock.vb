Public Class Laporan_Transfer_Stock
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

        SQL = "SELECT dbo.Tf_Stock.Kode_Transfer "

        SQL = SQL & "FROM dbo.Tf_Stock INNER JOIN "
        SQL = SQL & "dbo.Tf_Stock_det ON dbo.Tf_Stock.Kode_Perusahaan = dbo.Tf_Stock_det.Kode_Perusahaan AND dbo.Tf_Stock.Kode_Transfer = dbo.Tf_Stock_det.No_Faktur INNER JOIN "
        SQL = SQL & "dbo.Barang_SN ON dbo.Tf_Stock.Kode_Perusahaan = dbo.Barang_SN.Kode_Perusahaan AND dbo.Tf_Stock.SO_Awal = dbo.Barang_SN.Kode_Stock_Owner AND "
        SQL = SQL & "dbo.Tf_Stock_det.Serial_Number_Awal = dbo.Barang_SN.Serial_Number AND dbo.Tf_Stock.Kode_Barang = dbo.Barang_SN.Kode_Barang INNER JOIN "
        SQL = SQL & "dbo.Barang_SN AS Barang_SN_1 ON dbo.Tf_Stock.Kode_Perusahaan = Barang_SN_1.Kode_Perusahaan AND dbo.Tf_Stock.SO_Tujuan = Barang_SN_1.Kode_Stock_Owner AND "
        SQL = SQL & "dbo.Tf_Stock_det.Serial_Number_Akhir = Barang_SN_1.Serial_Number AND dbo.Tf_Stock.Kode_Barang = Barang_SN_1.Kode_Barang INNER JOIN "
        SQL = SQL & "dbo.View_Warehouse_Position ON dbo.Tf_Stock_det.Id_Wms_Awal = dbo.View_Warehouse_Position.Id_WMS_Warehouse_Position AND "
        SQL = SQL & "dbo.Tf_Stock_det.Kode_Perusahaan = dbo.View_Warehouse_Position.Kode_Perusahaan INNER JOIN "
        SQL = SQL & "dbo.View_Warehouse_Position AS View_Warehouse_Position_1 ON dbo.Tf_Stock_det.Kode_Perusahaan = View_Warehouse_Position_1.Kode_Perusahaan AND "
        SQL = SQL & "dbo.Tf_Stock_det.Id_Wms_Tujuan = View_Warehouse_Position_1.Id_WMS_Warehouse_Position INNER JOIN "
        SQL = SQL & "dbo.Barang ON dbo.Tf_Stock.Kode_Perusahaan = dbo.Barang.Kode_Perusahaan AND dbo.Tf_Stock.Kode_Barang = dbo.Barang.Kode_Barang "

        SQL = SQL & "WHERE dbo.Tf_Stock.Kode_Perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "dbo.Tf_Stock.Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '"
        SQL = SQL & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

        SF = "{Tf_Stock.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
        SF = SF & "{Tf_Stock.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
        SF = SF & "{Tf_Stock.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "#"

        Using MyDS As DataSet = Binding(SQL)
            With MyDS.Tables(0)
                If .Rows.Count <> 0 Then

                    Dim CrDoc As New Laporan_Transfer_Stock_Rpt

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
        
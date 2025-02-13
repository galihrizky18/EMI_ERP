Imports System.Net.Mime.MediaTypeNames

Public Class Laporan_Purchase_Requisition

    Private Sub Laporan_Purchase_Requisition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        CmbRelease.Items.Clear()
        CmbRelease.Items.Add("Seluruh")
        CmbRelease.Items.Add("Sudah Release")
        CmbRelease.Items.Add("Belum Release")

        Tgl1.Focus()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then CmbRelease.Focus()
    End Sub

    Private Sub CmbRelease_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbRelease.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub Laporan_Purchase_Requisition_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Tgl1.Focus()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf CmbRelease.SelectedIndex = -1 Then
            MessageBox.Show("Status release harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbRelease.Focus() : Exit Sub
        End If

        OpenConn()

        Dim SF As String = ""

        SQL = "SELECT top 1 a.no_faktur,a.lokasi,a.Tanggal,a.jam,a.userid,a.Tanggal_Release,a.jam,a.User_Release,a.keterangan,"
        SQL = SQL & "a.Flag_Release,b.kode_stock_owner,b.kode_barang,c.nama,b.jumlah,b.satuan,b.Tanggal_Delivery,"
        SQL = SQL & "b.Keterangan as Keterangan_Barang "

        SQL = SQL & "FROM emi_purchase_requisition a, emi_purchase_requisition_detail b, barang c "

        SQL = SQL & "WHERE a.kode_Perusahaan=b.kode_Perusahaan and a.no_faktur=b.no_faktur and "
        SQL = SQL & "a.status is null and b.Kode_Barang=c.Kode_Barang and b.Kode_Stock_Owner=c.Kode_Stock_Owner and "
        SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "a.tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

        SF = "{emi_purchase_requisition.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
        SF = SF & "{emi_purchase_requisition.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

        If CmbRelease.SelectedIndex = 1 Then 'SUDAH RELEASE (FLAG_RELEASE = 'Y')
            SQL = SQL & "and a.flag_release = 'Y'"
            SF = SF & "and {emi_purchase_requisition.Flag_Release} = 'Y'"
        ElseIf CmbRelease.SelectedIndex = 2 Then 'SUDAH RELEASE (FLAG_RELEASE = NULL)
            SQL = SQL & "and a.flag_release IS NULL"
            SF = SF & "and ISNULL({emi_purchase_requisition.Flag_Release})"

        End If

        Using MyDS As DataSet = Binding(SQL)
            With MyDS.Tables(0)
                If .Rows.Count <> 0 Then

                    Dim CrDoc As New Laporan_Purchase_Requsition_Rpt

                    CrDoc.SetDataSource(MyDS)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                    CrDoc.RecordSelectionFormula = SF

                    'With Print_Form
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
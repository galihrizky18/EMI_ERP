Imports System.Net.Mime.MediaTypeNames

Public Class Laporan_Actual_Biaya_Produksi
    Dim arrWorkCenter, arrJenisBiaya As New ArrayList

    Private Sub Laporan_Purchase_Requisition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LvSupp.Location = New Point(97, 130)
        Me.Size = New Size(607, 256)

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        Try
            OpenConn()

            CmbWorkCenter.Items.Clear() : arrWorkCenter.Clear()
            CmbWorkCenter.Items.Add("Seluruh") : arrWorkCenter.Add("Seluruh")
            SQL = "select id_work_center,keterangan from EMI_Master_Work_Center where kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbWorkCenter.Items.Add(dr("keterangan")) : arrWorkCenter.Add(dr("id_work_center"))
                Loop
            End Using


            CmbJenisBiaya.Items.Clear() : arrJenisBiaya.Clear()
            CmbJenisBiaya.Items.Add("Seluruh") : arrJenisBiaya.Add("Seluruh")
            SQL = "select id_jenis_biaya_produksi,keterangan from Emi_Jenis_Biaya_Produksi where kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbJenisBiaya.Items.Add(dr("keterangan")) : arrJenisBiaya.Add(dr("id_jenis_biaya_produksi"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try






        LvSupp.Visible = False
        Tgl1.Focus()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub





    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub









    Private Sub LvSupp_DoubleClick(sender As Object, e As EventArgs) Handles LvSupp.DoubleClick
        Dim Kode As String = LvSupp.FocusedItem.Text
        Dim Nama As String = LvSupp.FocusedItem.SubItems(1).Text

        ' TxtKdSupp.Text = Kode
        ' TxtNama.Text = Nama

        LvSupp.Visible = False
        Me.Size = New Size(607, 256)
        CmbWorkCenter.Focus()
    End Sub

    Private Sub LvSupp_KeyDown(sender As Object, e As KeyEventArgs) Handles LvSupp.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupp_DoubleClick(LvSupp, e)
        End If
    End Sub

    Private Sub CmbRelease_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbWorkCenter.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub Laporan_Purchase_Requisition_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Tgl1.Focus()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Exit Sub
        ElseIf CmbJenisBiaya.Text.Trim.Length = 0 Then
            MessageBox.Show("Biaya Harus di pilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf CmbWorkCenter.Text.Trim.Length = 0 Then
            MessageBox.Show("Work Center harus di pilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        OpenConn()

        Dim SF As String = ""

        'SQL = "SELECT a.no_faktur,a.No_Nota,a.Tanggal,a.Jam,a.userid,a.kode_supplier,d.nama,a.lokasi,a.Jenis_Pembayaran,"
        'SQL = SQL & "a.Cara_Bayar,a.Tgl_Jatuh_Tempo,a.Grand_Sebelum_PPN,a.PPN,a.Grand,a.Flag_Release,a.Jam_Release,a.User_Release,"
        'SQL = SQL & "b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah,b.Satuan,b.Harga,b.Total "

        'SQL = SQL & "FROM emi_pembelian_po a, emi_pembelian_po_detail b, barang c, suppliers d "

        'SQL = SQL & "WHERE a.kode_Perusahaan=b.kode_Perusahaan and a.no_faktur=b.no_faktur And "
        'SQL = SQL & "a.status Is null And b.Kode_Barang = c.Kode_Barang And b.Kode_Stock_Owner = c.Kode_Stock_Owner And "
        'SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan And a.Kode_Perusahaan=d.Kode_Perusahaan and a.Kode_Supplier=d.Kode_Supplier And "
        'SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' and "

        SQL = "select kode_perusahaan,no_faktur,tanggal,kode_jenis_biaya_produksi, "
        SQL = SQL & "jenis_biaya,work_center,jumlah,satuan,kode_barang,nama_barang "
        SQL = SQL & "From view_laporan_actual_biaya_produksi where "
        SQL = SQL & "tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

        SF = "{view_laporan_actual_biaya_produksi.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
        SF = SF & "{view_laporan_actual_biaya_produksi.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "#"


        If CmbWorkCenter.SelectedIndex <> 0 Then
            SQL = SQL & "and id_work_center  = '" & arrWorkCenter.Item(CmbWorkCenter.SelectedIndex) & "'"
            SF = SF & "and {view_laporan_actual_biaya_produksi.id_work_center} = '" & arrWorkCenter.Item(CmbWorkCenter.SelectedIndex) & "'"
        End If

        If CmbJenisBiaya.SelectedIndex <> 0 Then
            SQL = SQL & "and id_jenis_biaya  = '" & arrJenisBiaya.Item(CmbJenisBiaya.SelectedIndex) & "'"
            SF = SF & "and {view_laporan_actual_biaya_produksi.id_work_center} = '" & arrJenisBiaya.Item(CmbJenisBiaya.SelectedIndex) & "'"
        End If


        'If Not TxtKdSupp.Text.Trim.ToUpper = "SELURUH" Then
        '    SQL = SQL & "And a.kode_supplier = '" & TxtKdSupp.Text & "'"
        '    SF = SF & "And {emi_pembelian_po.kode_supplier} = '" & TxtKdSupp.Text & "'"
        'End If

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

                    Dim CrDoc As New Laporan_Actual_Biaya_Produksi_Summary_Rpt

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
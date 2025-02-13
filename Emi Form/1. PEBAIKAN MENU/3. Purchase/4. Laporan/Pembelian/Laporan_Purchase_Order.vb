Imports System.Net.Mime.MediaTypeNames

Public Class Laporan_Purchase_Order
    Dim ArrRelease As New ArrayList

    Private Sub Laporan_Purchase_Requisition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LvSupp.Location = New Point(97, 130)
        Me.Size = New Size(607, 256)

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
        TxtKdSupp.Text = "" : TxtNama.Text = ""

        CmbRelease.Items.Clear() : ArrRelease.Clear()
        CmbRelease.Items.Add("Seluruh") : ArrRelease.Add("Seluruh")
        CmbRelease.Items.Add("Sudah Release") : ArrRelease.Add("Y")
        CmbRelease.Items.Add("Belum Release") : ArrRelease.Add("T")

        LvSupp.Visible = False
        Tgl1.Focus()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then TxtKdSupp.Focus()
    End Sub

    Private Sub TxtNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNama.KeyPress
        If e.KeyChar = Chr(13) Then
            TxtKdSupp_Leave(TxtNama, e)
            LvSupp.Visible = False
            Me.Size = New Size(607, 256)
        End If
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub TxtKdSupp_TextChanged(sender As Object, e As EventArgs) Handles TxtKdSupp.TextChanged
        If TxtKdSupp.Text.Trim.Length = 0 Then
            LvSupp.Visible = False : Me.Size = New Size(607, 256) : Exit Sub
        Else
            LvSupp.Visible = True
            Me.Size = New Size(607, 322)
        End If

        OpenConn()

        Dim lv As New ListViewItem
        LvSupp.Items.Clear()

        lv = LvSupp.Items.Add("Seluruh")
        lv.SubItems.Add("Seluruh")

        SQL = "select kode_supplier,nama_supplier from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "kode_supplier like '" & TxtKdSupp.Text & "%' order by kode_supplier"
        Using Dr = Open(SQL)
            Do While Dr.Read
                lv = LvSupp.Items.Add(Dr("kode_supplier"))
                lv.SubItems.Add(Dr("nama_supplier"))
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub TxtKdSupp_Leave(sender As Object, e As EventArgs) Handles TxtKdSupp.Leave
        If TxtKdSupp.Text.Trim.Length = 0 Then Exit Sub
        If LvSupp.Focused = True Then Exit Sub

        OpenConn()

        SQL = "select kode_supplier,nama_supplier from Suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "kode_supplier = '" & TxtKdSupp.Text & "'"
        Using Dr = Open(SQL)
            If Dr.Read Then
                TxtKdSupp.Text = Dr("kode_supplier")
                TxtNama.Text = Dr("nama_supplier")
                CmbRelease.Focus()
            Else
                MessageBox.Show("Kode supplier tidak ditemukan . . ! !", Judul)
                TxtKdSupp.Text = "" : TxtNama.Text = ""
                TxtKdSupp.Focus()
            End If
            LvSupp.Visible = False
            Me.Size = New Size(607, 256)
        End Using

        CloseConn()
    End Sub

    Private Sub TxtKdSupp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKdSupp.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKdSupp.Text.Trim.Length = 0 Then TxtNama.Focus()
            TxtKdSupp_Leave(TxtKdSupp, e)
            LvSupp.Visible = False
            Me.Size = New Size(607, 256)
        End If
    End Sub

    Private Sub TxtKdSupp_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKdSupp.KeyDown
        If e.KeyCode = Keys.Down Then LvSupp.Focus()
    End Sub

    Private Sub TxtNama_TextChanged(sender As Object, e As EventArgs) Handles TxtNama.TextChanged
        If TxtNama.Text.Trim.Length = 0 Then
            LvSupp.Visible = False : Me.Size = New Size(607, 256) : Exit Sub
        Else
            LvSupp.Visible = True
            Me.Size = New Size(607, 322)
        End If

        OpenConn()

        Dim lv As New ListViewItem
        LvSupp.Items.Clear()
        lv = LvSupp.Items.Add("Seluruh")
        lv.SubItems.Add("Seluruh")

        SQL = "select kode_supplier,nama_supplier from Suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "nama_supplier Like '%" & TxtNama.Text & "%' order by nama_supplier"
        Using Dr = Open(SQL)
            Do While Dr.Read
                lv = LvSupp.Items.Add(Dr("kode_supplier"))
                lv.SubItems.Add(Dr("nama_supplier"))
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub TxtNama_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtNama.KeyDown
        If e.KeyCode = Keys.Down Then LvSupp.Focus()
    End Sub

    Private Sub LvSupp_DoubleClick(sender As Object, e As EventArgs) Handles LvSupp.DoubleClick
        Dim Kode As String = LvSupp.FocusedItem.Text
        Dim Nama As String = LvSupp.FocusedItem.SubItems(1).Text

        TxtKdSupp.Text = Kode
        TxtNama.Text = Nama

        LvSupp.Visible = False
        Me.Size = New Size(607, 256)
        CmbRelease.Focus()
    End Sub

    Private Sub LvSupp_KeyDown(sender As Object, e As KeyEventArgs) Handles LvSupp.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupp_DoubleClick(LvSupp, e)
        End If
    End Sub

    Private Sub CmbRelease_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbRelease.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub Laporan_Purchase_Requisition_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Tgl1.Focus()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf TxtKdSupp.Text.Trim.Length = 0 Then
            MessageBox.Show("Supplier harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKdSupp.Focus() : Exit Sub
        ElseIf CmbRelease.SelectedIndex = -1 Then
            MessageBox.Show("Status release harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbRelease.Focus() : Exit Sub
        End If

        OpenConn()

        Dim SF As String = ""

        SQL = "SELECT a.no_faktur,a.No_Nota,a.Tanggal,a.Jam,a.userid,a.kode_supplier,d.nama,a.lokasi,a.Jenis_Pembayaran,"
        SQL = SQL & "a.Cara_Bayar,a.Tgl_Jatuh_Tempo,a.Grand_Sebelum_PPN,a.PPN,a.Grand,a.Flag_Release,a.Jam_Release,a.User_Release,"
        SQL = SQL & "b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah,b.Satuan,b.Harga,b.Total "

        SQL = SQL & "FROM emi_pembelian_po a, emi_pembelian_po_detail b, barang c, suppliers d "

        SQL = SQL & "WHERE a.kode_Perusahaan=b.kode_Perusahaan and a.no_faktur=b.no_faktur And "
        SQL = SQL & "a.status Is null And b.Kode_Barang = c.Kode_Barang And b.Kode_Stock_Owner = c.Kode_Stock_Owner And "
        SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan And a.Kode_Perusahaan=d.Kode_Perusahaan and a.Kode_Supplier=d.Kode_Supplier And "
        SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "a.tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

        SF = "{emi_pembelian_po.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
        SF = SF & "{emi_pembelian_po.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "#"

        If Not TxtKdSupp.Text.Trim.ToUpper = "SELURUH" Then
            SQL = SQL & "And a.kode_supplier = '" & TxtKdSupp.Text & "'"
            SF = SF & "And {emi_pembelian_po.kode_supplier} = '" & TxtKdSupp.Text & "'"
        End If

        If CmbRelease.SelectedIndex = 1 Then 'SUDAH RELEASE (FLAG_RELEASE = 'Y')
            SQL = SQL & "and a.flag_release = 'Y'"
            SF = SF & "and {emi_pembelian_po.Flag_Release} = 'Y'"
        ElseIf CmbRelease.SelectedIndex = 2 Then 'SUDAH RELEASE (FLAG_RELEASE = NULL)
            SQL = SQL & "and a.flag_release IS NULL"
            SF = SF & "and ISNULL({emi_pembelian_po.Flag_Release})"
        End If

        Using MyDS As DataSet = Binding(SQL)
            With MyDS.Tables(0)
                If .Rows.Count <> 0 Then

                    Dim CrDoc As New Laporan_Purchase_Order_Rpt

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
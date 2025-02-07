Public Class Laporan_HPP_Local
    Dim ArrRelease As New ArrayList

    Private Sub Laporan_Purchase_Requisition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LvSupp.Location = New Point(97, 130)
        LvBarang.Location = New Point(99, 165)
        Me.Size = New Size(607, 256)

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
        TxtKdSupp.Text = "" : TxtNamaSupp.Text = ""
        TxtKdBrg.Text = "" : TxtNmBrg.Text = ""

        'CmbRelease.Items.Clear() : ArrRelease.Clear()
        'CmbRelease.Items.Add("Seluruh") : ArrRelease.Add("Seluruh")
        'CmbRelease.Items.Add("Sudah Release") : ArrRelease.Add("Y")
        'CmbRelease.Items.Add("Belum Release") : ArrRelease.Add("T")

        LvSupp.Visible = False
        LvBarang.Visible = False
        Tgl1.Focus()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then TxtKdSupp.Focus()
    End Sub

    Private Sub TxtNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNamaSupp.KeyPress
        If e.KeyChar = Chr(13) Then
            TxtKdSupp_Leave(TxtNamaSupp, e)
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
                TxtNamaSupp.Text = Dr("nama_supplier")
                CmbRelease.Focus()
            Else
                MessageBox.Show("Kode supplier tidak ditemukan . . ! !", Judul)
                TxtKdSupp.Text = "" : TxtNamaSupp.Text = ""
                TxtKdSupp.Focus()
            End If
            LvSupp.Visible = False
            Me.Size = New Size(607, 256)
        End Using

        CloseConn()
    End Sub

    Private Sub TxtKdSupp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKdSupp.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKdSupp.Text.Trim.Length = 0 Then TxtNamaSupp.Focus()
            TxtKdSupp_Leave(TxtKdSupp, e)
            LvSupp.Visible = False
            Me.Size = New Size(607, 256)
        End If
    End Sub

    Private Sub TxtKdSupp_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKdSupp.KeyDown
        If e.KeyCode = Keys.Down Then LvSupp.Focus()
    End Sub

    Private Sub TxtNama_TextChanged(sender As Object, e As EventArgs) Handles TxtNamaSupp.TextChanged
        If TxtNamaSupp.Text.Trim.Length = 0 Then
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
        SQL = SQL & "nama_supplier Like '%" & TxtNamaSupp.Text & "%' order by nama_supplier"
        Using Dr = Open(SQL)
            Do While Dr.Read
                lv = LvSupp.Items.Add(Dr("kode_supplier"))
                lv.SubItems.Add(Dr("nama_supplier"))
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub TxtNama_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtNamaSupp.KeyDown
        If e.KeyCode = Keys.Down Then LvSupp.Focus()
    End Sub

    Private Sub LvSupp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvSupp.SelectedIndexChanged

    End Sub

    Private Sub LvSupp_DoubleClick(sender As Object, e As EventArgs) Handles LvSupp.DoubleClick
        Dim Kode As String = LvSupp.FocusedItem.Text
        Dim Nama As String = LvSupp.FocusedItem.SubItems(1).Text

        TxtKdSupp.Text = Kode
        TxtNamaSupp.Text = Nama

        LvSupp.Visible = False
        Me.Size = New Size(607, 256)
        'CmbRelease.Focus()
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
        ElseIf TxtKdBrg.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKdBrg.Focus() : Exit Sub
        End If

        OpenConn()

        Dim SF As String = ""

        '''SQL = "SELECT a.no_faktur, a.tanggal, a.Kode_Supplier,  f.Nama, a.ID_Rencana, e.lokasi, b.Kode_Barang, d.Nama, b.Jumlah, d.Harga_Declare, "
        '''SQL = SQL & "b.total_harga,b.Mata_Uang,b.nilai_pot_stock+b.nilai_tdk_pot_stock_lns+b.nilai_tdk_pot_stock_htg_utama+b.nilai_tdk_pot_stock_htg_penolong AS Nilai_Bahan, "
        '''SQL = SQL & "b.pph29, b.biaya_import, b.biaya_billing, b.biaya_kontainer, b.biaya_freight_int, "
        '''SQL = SQL & "c.biaya_import2, c.biaya_import_wet_dry,c.input_hpp, c.nilai_hpp_barang_per_pcs_brsh "

        '''SQL = SQL & "from hpp_import a, detail_hpp_import b, Detail_HPP_Import2 c, barang d, rencana_order e, suppliers f "

        '''SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
        '''SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
        '''SQL = SQL & "and a.No_Faktur = b.No_Faktur and a.No_Faktur = c.No_Faktur and a.ID_Rencana = e.ID_Rencana and a.Kode_Supplier = f.Kode_Supplier "
        '''SQL = SQL & "and b.no_faktur = c.No_Faktur and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.kode_barang = d.kode_barang "
        '''SQL = SQL & "and c.Kode_Barang = d.kode_barang and c.Kode_Stock_Owner = d.Kode_Stock_Owner and e.Kode_Supplier = f.Kode_Supplier "
        '''

        '''SQL = "select a.no_faktur, a.no_nota, a.lokasi, a.tanggal, a.jam, a.Tgl_Jatuh_Tempo, a.Kode_Supplier, e.nama as nama_supplier, c.kode_barang, d.Nama as nama_barang, a.UserID, "
        '''SQL = SQL & "b.No_Faktur as no_pembelian_loading, b.Biaya_Perjalanan, b.Tanggal_Masuk, b.Jam_Masuk, "
        '''SQL = SQL & "DBO.Ubah_Satuan(c.Kode_Perusahaan, 'MASA', c.Kode_Barang, c.Satuan_Barang, c.Satuan, c.jumlah_barang) as Jumlah_PO, "
        '''SQL = SQL & "DBO.Ubah_Satuan(c.Kode_Perusahaan, 'MASA', c.Kode_Barang, c.Satuan_Barang, c.Satuan, c.jumlah_masuk)  as Jumlah_Masuk, "
        '''SQL = SQL & "DBO.Ubah_Satuan(c.Kode_Perusahaan, 'UANG', c.Kode_Barang, c.Satuan_Barang, c.Satuan, c.Harga_Barang)  as Harga_PO, "
        '''SQL = SQL & "c.HPP_Satuan_Display as Harga_Akhir, "
        '''SQL = SQL & "DBO.Ubah_Satuan(c.Kode_Perusahaan, 'MASA', c.Kode_Barang, c.Satuan_Barang, c.Satuan, c.jumlah_masuk) * "
        '''SQL = SQL & "DBO.Ubah_Satuan(c.Kode_Perusahaan, 'UANG', c.Kode_Barang, c.Satuan_Barang, c.Satuan, c.Harga_Barang) as Grand_PO, "
        '''SQL = SQL & "DBO.Ubah_Satuan(c.Kode_Perusahaan, 'MASA', c.Kode_Barang, c.Satuan_Barang, c.Satuan,c.jumlah_masuk) * "
        '''SQL = SQL & "c.HPP_Satuan_Display as Grand_Akhir "
        '''SQL = SQL & "from emi_pembelian_po a, emi_pembelian_loading b, EMI_Pembelian_Loading_Detail c, barang d, suppliers e "
        '''SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
        '''SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_supplier = e.Kode_Supplier "
        '''SQL = SQL & "and b.No_Faktur = c.No_Faktur and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang and c.No_PO = a.No_Faktur "
        '''SQL = SQL & "and c.Flag_Timbang_Keluar = 'Y' and ((b.Flag_Import='Y' and b.Flag_Import_HPP='Y') or Flag_Import is null) "

        SQL = "select tanggal from vw_Laporan_HPP_Local "
        '''SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "where tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

        SF = "{vw_Laporan_HPP_Local.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
        SF = SF & "{vw_Laporan_HPP_Local.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "#"

        If Not TxtKdSupp.Text.Trim.ToUpper = "SELURUH" Then
            SQL = SQL & "And kode_supplier = '" & TxtKdSupp.Text & "'"
            SF = SF & "And {vw_Laporan_HPP_Local.kode_supplier} = '" & TxtKdSupp.Text & "'"
        End If

        If Not TxtKdBrg.Text.Trim.ToUpper = "SELURUH" Then
            SQL = SQL & "And kode_barang = '" & TxtKdBrg.Text & "'"
            SF = SF & "And {vw_Laporan_HPP_Local.kode_barang} = '" & TxtKdBrg.Text & "'"
        End If

        Using MyDS As DataSet = Binding(SQL)
            With MyDS.Tables(0)
                If .Rows.Count <> 0 Then

                    Dim CrDoc As New Laporan_HPP_Local_Rpt

                    CrDoc.SetDataSource(MyDS)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd MMM yyyy")
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

    Private Sub TxtKdBrg_TextChanged(sender As Object, e As EventArgs) Handles TxtKdBrg.TextChanged
        If TxtKdBrg.Text.Trim.Length = 0 Then
            LvBarang.Visible = False : Me.Size = New Size(607, 256) : Exit Sub
        Else
            LvBarang.Visible = True
            Me.Size = New Size(607, 322)
        End If

        OpenConn()

        Dim lv As New ListViewItem
        LvBarang.Items.Clear()

        lv = LvBarang.Items.Add("Seluruh")
        lv.SubItems.Add("Seluruh")

        SQL = "select kode_barang, nama from barang where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "kode_barang like '" & TxtKdBrg.Text & "%' order by kode_barang"
        Using Dr = Open(SQL)
            Do While Dr.Read
                lv = LvBarang.Items.Add(Dr("kode_barang"))
                lv.SubItems.Add(Dr("nama"))
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub TxtKdBrg_Leave(sender As Object, e As EventArgs) Handles TxtKdBrg.Leave
        If TxtKdBrg.Text.Trim.Length = 0 Then Exit Sub
        If LvBarang.Focused = True Then Exit Sub

        OpenConn()

        SQL = "select kode_barang, nama from barang where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "kode_barang = '" & TxtKdBrg.Text & "'"
        Using Dr = Open(SQL)
            If Dr.Read Then
                TxtKdBrg.Text = Dr("kode_barang")
                TxtNmBrg.Text = Dr("nama")
            Else
                MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul)
                TxtKdBrg.Text = "" : TxtNmBrg.Text = ""
                TxtKdBrg.Focus()
            End If
            LvBarang.Visible = False
            Me.Size = New Size(607, 256)
        End Using

        CloseConn()
    End Sub

    Private Sub TxtKdBrg_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKdBrg.KeyDown
        If e.KeyCode = Keys.Down Then LvBarang.Focus()
    End Sub

    Private Sub TxtKdBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKdBrg.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKdBrg.Text.Trim.Length = 0 Then TxtNmBrg.Focus()
            TxtKdBrg_Leave(TxtKdBrg, e)
            LvBarang.Visible = False
            Me.Size = New Size(607, 256)
        End If
    End Sub

    Private Sub TxtNmBrg_TextChanged(sender As Object, e As EventArgs) Handles TxtNmBrg.TextChanged
        If TxtNmBrg.Text.Trim.Length = 0 Then
            LvBarang.Visible = False : Me.Size = New Size(607, 256) : Exit Sub
        Else
            LvBarang.Visible = True
            Me.Size = New Size(607, 322)
        End If

        OpenConn()

        Dim lv As New ListViewItem
        LvBarang.Items.Clear()
        lv = LvBarang.Items.Add("Seluruh")
        lv.SubItems.Add("Seluruh")

        SQL = "select kode_barang,nama from barang where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "nama Like '%" & TxtNmBrg.Text & "%' order by nama"
        Using Dr = Open(SQL)
            Do While Dr.Read
                lv = LvBarang.Items.Add(Dr("kode_barang"))
                lv.SubItems.Add(Dr("nama"))
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub TxtNmBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNmBrg.KeyPress
        If e.KeyChar = Chr(13) Then
            TxtKdBrg_Leave(TxtNmBrg, e)
            LvBarang.Visible = False
            Me.Size = New Size(607, 256)
        End If
    End Sub

    Private Sub TxtNmBrg_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtNmBrg.KeyDown
        If e.KeyCode = Keys.Down Then LvBarang.Focus()
    End Sub

    Private Sub LvBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvBarang.SelectedIndexChanged

    End Sub

    Private Sub LvBarang_DoubleClick(sender As Object, e As EventArgs) Handles LvBarang.DoubleClick
        Dim Kode As String = LvBarang.FocusedItem.Text
        Dim Nama As String = LvBarang.FocusedItem.SubItems(1).Text

        TxtKdBrg.Text = Kode
        TxtNmBrg.Text = Nama

        LvBarang.Visible = False
        Me.Size = New Size(607, 256)
    End Sub

    Private Sub LvBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles LvBarang.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvBarang_DoubleClick(LvBarang, e)
        End If
    End Sub

End Class
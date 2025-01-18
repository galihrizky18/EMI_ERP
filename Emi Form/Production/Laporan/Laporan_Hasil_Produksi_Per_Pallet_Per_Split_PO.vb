Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class Laporan_Hasil_Produksi_Per_Pallet_Per_Split_PO
    Dim arrSO As New ArrayList
    Private Sub Laporan_Rekap_Production_Order_VS_Split_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        ListView1.Columns.Add("Kode Barang", 105, HorizontalAlignment.Left)
        ListView1.Columns.Add("Nama", 240, HorizontalAlignment.Left)
        ListView1.Location = New Point(135, 107)
        ListView1.Visible = False

        TextBox1.Text = ""
        TextBox2.Text = ""

        Try
            OpenConn()

            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1 : arrSO.Clear()
            CmbSO_Asal.Items.Add("--SELURUH--")
            SQL = "Select kode_stock_owner,Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "
            'SQL = SQL & "and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSO_Asal.Items.Add(dr("Keterangan"))
                    arrSO.Add(dr("kode_stock_owner"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then CmbSO_Asal.Focus()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf CmbSO_Asal.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        Dim SF As String = ""

        OpenConn()

        SQL = "SELECT Top 1 * FROM Hasil_Produksi_Per_Pallet_Per_Split_PO "

        SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "Tanggal_Production_Order between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '"
        SQL = SQL & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

        SF = "{Hasil_Produksi_Per_Pallet_Per_Split_PO.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
        SF = SF & "{Hasil_Produksi_Per_Pallet_Per_Split_PO.Tanggal_Production_Order} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
        SF = SF & "{Hasil_Produksi_Per_Pallet_Per_Split_PO.Tanggal_Production_Order} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

        If CmbSO_Asal.SelectedIndex = 0 Then
            SQL = SQL & " "
            SF = SF & " "
        Else
            SQL = SQL & "and Kode_Stock_Owner  = '" & arrSO.Item(CmbSO_Asal.SelectedIndex) & "' "
            SF = SF & "and {Hasil_Produksi_Per_Pallet_Per_Split_PO.Kode_Stock_Owner } = '" & arrSO.Item(CmbSO_Asal.SelectedIndex) & "' "
        End If

        If TextBox1.Text = "---SELURUH---" Then
            SQL = SQL & " "
            SF = SF & " "
        Else
            SQL = SQL & "and Kode_Barang_Inq  = '" & TextBox1.Text & "' "
            SF = SF & "and {Hasil_Produksi_Per_Pallet_Per_Split_PO.Kode_Barang_Inq } = '" & TextBox1.Text & "' "
        End If

        Using MyDS As DataSet = Binding(SQL)
            With MyDS.Tables(0)
                If .Rows.Count <> 0 Then

                    Dim CrDoc As New Laporan_Hasil_Produksi_Per_Pallet_Per_Split_PO_Rpt

                    CrDoc.SetDataSource(MyDS)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " &
                                                                    Format(Tgl2.Value, "dd MMM yyyy") ''& Chr(13) &
                    ''"Flag Produksi : " & CmbProduksi.Text ''& " | " &
                    '"Flag Release : " & CmbRelease.Text & " | " &
                    '"Split Production : " & CmbSplit.Text

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

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Down Then
            ListView1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox1.Text.Trim.Length = 0 Then
                ListView1.Visible = False : TextBox2.Focus() : Exit Sub
            End If
            TextBox1_Leave(TextBox1, e)
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then
            ListView1.Visible = False : Exit Sub
        Else
            ListView1.Visible = True
        End If
        If ListView1.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "select Kode_Barang_Inq,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Barang_Inq = '" & TextBox1.Text & "' "
            If CmbSO_Asal.SelectedIndex = 0 Then
                SQL = SQL & "and Kode_Stock_Owner = '" & arrSO.Item(1) & "' "
            Else
                SQL = SQL & "and Kode_Stock_Owner = '" & arrSO.Item(CmbSO_Asal.SelectedIndex) & "' "
            End If
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("Kode_Barang_Inq")
                    TextBox2.Text = Dr("Nama")
                    BtnCetak.Focus()
                Else
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox1.Focus()
                End If
                ListView1.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.Trim.Length = 0 Then
            ListView1.Visible = False : Exit Sub
        Else
            ListView1.Visible = True
        End If

        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi . . ! !", "Perhatian")
            CmbSO_Asal.Focus() : Exit Sub
        End If

        ListView1.Items.Clear()
        Dim lv As New ListViewItem

        Try
            OpenConn()

            lv = ListView1.Items.Add("---SELURUH---")
            lv.SubItems.Add("---SELURUH---")
            SQL = "select Kode_Barang_Inq,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Barang_Inq like '" & TextBox1.Text & "%' "
            If CmbSO_Asal.SelectedIndex = 0 Then
                SQL = SQL & "and Kode_Stock_Owner = '" & arrSO.Item(1) & "' "
            Else
                SQL = SQL & "and Kode_Stock_Owner = '" & arrSO.Item(CmbSO_Asal.SelectedIndex) & "' "
            End If
            SQL = SQL & "order by Kode_Barang_Inq"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView1.Items.Add(Dr("Kode_Barang_Inq"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub
        Dim kode As String = ListView1.FocusedItem.Text
        Dim nama As String = ListView1.FocusedItem.SubItems(1).Text
        TextBox1.Text = kode
        TextBox2.Text = nama
        ListView1.Visible = False
        'ComboBox3.Focus()
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView1.Items.Count = 0 Then Exit Sub
            ListView1.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox1.Text.Trim.Length = 0 Then TextBox2.Text = "" : ListView1.Visible = False ': Exit Sub
            BtnCetak.Focus()
        End If
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        If ListView1.Focused = True Then Exit Sub
        TextBox1.Text = "" : TextBox2.Text = ""
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text.Trim.Length = 0 Then
            ListView1.Visible = False : Exit Sub
        Else
            ListView1.Visible = True
        End If

        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi . . ! !", "Perhatian")
            CmbSO_Asal.Focus() : Exit Sub
        End If

        ListView1.Items.Clear()
        Dim lv As New ListViewItem

        Try
            OpenConn()

            lv = ListView1.Items.Add("---SELURUH---")
            lv.SubItems.Add("---SELURUH---")
            SQL = "select Kode_Barang_Inq,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Nama like '" & TextBox2.Text & "%' "
            If CmbSO_Asal.SelectedIndex = 0 Then
                SQL = SQL & "and Kode_Stock_Owner = '" & arrSO.Item(1) & "' "
            Else
                SQL = SQL & "and Kode_Stock_Owner = '" & arrSO.Item(CmbSO_Asal.SelectedIndex) & "' "
            End If
            SQL = SQL & "order by Kode_Barang_Inq"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView1.Items.Add(Dr("Kode_Barang_Inq"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class
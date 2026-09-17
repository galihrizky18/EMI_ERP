Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_Laporan_Pelunasan_DO_Tunai
    Private Sub N_EMI_Laporan_Pelunasan_DO_Tunai_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub N_EMI_Laporan_Pelunasan_DO_Tunai_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        'ComboBox3.Items.Clear()
        'ComboBox3.Items.Add("-- Seluruh --")
        'ComboBox3.Items.Add("Batal")
        'ComboBox3.Items.Add("Tidak Batal")

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("-- Seluruh --")
        ComboBox2.Items.Add("Reseller")
        ComboBox2.Items.Add("Toko Sendiri")

        ListView5.Columns.Add("Kode Customer", 100, HorizontalAlignment.Left)
        ListView5.Columns.Add("Nama", 200, HorizontalAlignment.Left)

        ListView5.Location = New Point(167, 184)
        ListView5.Visible = False

        Me.Size = New Size(631, 296)

        Try
            OpenConn()

            ComboBox1.Items.Clear() : ComboBox1.SelectedIndex = -1
            ComboBox1.Items.Add("-- Seluruh --")
            SQL = "Select kode_cb From cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by kode_cb"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("kode_cb"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox21.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Format(DateTimePicker3.Value, "yyyy-MM-dd") > Format(DateTimePicker4.Value, "yyyy-MM-dd") Then
            MessageBox.Show("Tanggal awal tidak boleh lebih dari tanggal akhir!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker3.Focus()
            Exit Sub
            'ElseIf ComboBox3.SelectedIndex = -1 Then
            '    MessageBox.Show("Status pelunasan belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBox3.Focus()
            '    Exit Sub
        ElseIf ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Cara bayar belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Jenis laporan belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        ElseIf TextBox21.Text.Trim.Length = 0 Then
            MessageBox.Show("Customer belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox21.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            Dim status As String = ""
            Dim status_formula As String = ""

            'If ComboBox3.SelectedIndex = "0" Then 'seluruh
            '    status = status & ""
            '    status_formula = status_formula & ""
            'ElseIf ComboBox3.SelectedIndex = "1" Then 'batal
            '    status = status & "and a.status = 'Y' "
            '    status_formula = status_formula & "and {val_penj.status} = 'Y' "
            'ElseIf ComboBox3.SelectedIndex = "2" Then 'tidak batal
            '    status = status & "and a.status is null "
            '    status_formula = status_formula & "and isnull({val_penj.status}) "
            'End If

            If ComboBox1.SelectedIndex = 0 Then 'seluruh
                status = status & ""
                status_formula = status_formula & ""
            Else
                status = status & "and a.cara_bayar = '" & ComboBox1.Text & "' "
                status_formula = status_formula & "and {val_do_tunai.cara_bayar} = '" & ComboBox1.Text & "' "
            End If

            If ComboBox2.SelectedIndex = 0 Then 'seluruh
                status = status & ""
                status_formula = status_formula & ""
            ElseIf ComboBox2.SelectedIndex = 1 Then
                status = status & "and c.Flag_Cabang_Sendiri = 'T' "
                status_formula = status_formula & "and {penjualan.Flag_Cabang_Sendiri} = 'T' "
            ElseIf ComboBox2.SelectedIndex = 2 Then
                status = status & "and c.Flag_Cabang_Sendiri = 'Y' "
                status_formula = status_formula & "and {penjualan.Flag_Cabang_Sendiri} = 'Y' "
            End If

            Dim sales, sales_formula As String
            If TextBox21.Text.ToUpper = "-- SELURUH --" Then
                sales = ""
                sales_formula = ""
            Else
                sales = "and c.kode_customer = '" & TextBox21.Text.Trim & "'"
                sales_formula = "and {penjualan.kode_customer} = '" & TextBox21.Text.Trim & "'"
            End If

            SQL = "select a.kode_perusahaan from val_do_tunai a, detail_val_do_tunai b, penjualan c, do_new d where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and c.kode_perusahaan = d.kode_perusahaan and "
            SQL = SQL & "a.no_val = b.no_val and b.no_faktur = d.no_do and c.no_faktur = d.no_faktur and a.status is null and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.tanggal between '" & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' and "
            SQL = SQL & "'" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' " & sales
            SQL = SQL & status
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As Object
                    CrDoc = New N_EMI_CR_Laporan_Pelunasan_DO_Tunai

                    With A_Place_For_Printing
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{val_do_tunai.kode_perusahaan} = '" & KodePerusahaan & "' and isnull({val_do_tunai.status}) and isnull({val_do_tunai.status}) and {val_do_tunai.Tanggal} >= # " & Format(DateTimePicker3.Value, "yyyy-MM-dd") & " # and {val_do_tunai.Tanggal} <= # " & Format(DateTimePicker4.Value, "yyyy-MM-dd") & " # " & status_formula & " " & sales_formula
                        CrDoc.SummaryInfo.ReportTitle = "Laporan Pelunasan Invoice Tunai (Reseller) " & Chr(13) & Format(DateTimePicker3.Value, "dd MMM yyyy") & " s/d " & Format(DateTimePicker4.Value, "dd MMM yyyy") & ""
                        .Text = "Laporan Pelunasan Invoice Tunai (Reseller) " & Format(DateTimePicker3.Value, "dd MMM yyyy") & " s/d " & Format(DateTimePicker4.Value, "dd MMM yyyy") & ""
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

    Private Sub DateTimePicker3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker3.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker4.Focus()
    End Sub

    Private Sub DateTimePicker4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker4.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub TextBox21_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox21.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView5.Items.Count = 0 Then Exit Sub
            ListView5.Focus()
        End If
    End Sub

    Private Sub TextBox21_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox21.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox21.Text.Trim.Length = 0 Then
                ListView5.Visible = False : TextBox22.Focus() : Exit Sub
            End If
            TextBox21_Leave(TextBox21, e)
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Public Sub TextBox21_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox21.Leave
        If TextBox21.Text.Trim.Length = 0 Then
            ListView5.Visible = False : Exit Sub
        Else
            ListView5.Visible = True
        End If
        If ListView5.Focused = True Then Exit Sub

        OpenConn()

        Using Dr = Open("select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & TextBox21.Text & "' order by kode_customer")
            If Dr.Read Then
                TextBox21.Text = Dr("kode_customer")
                TextBox22.Text = Dr("nama")
                Button1.Focus()
            Else
                TextBox21.Text = ""
                TextBox22.Text = ""
                TextBox21.Focus()
            End If
            ListView5.Visible = False
        End Using

        CloseConn()
    End Sub

    Private Sub TextBox21_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox21.TextChanged
        If TextBox21.Text.Trim.Length = 0 Then
            ListView5.Visible = False : Exit Sub
            Me.Size = New Size(631, 296)
        Else
            ListView5.Visible = True
            Me.Size = New Size(631, 487)
        End If

        OpenConn()

        Dim lv As New ListViewItem
        ListView5.Items.Clear()
        lv = ListView5.Items.Add("-- Seluruh --")
        lv.SubItems.Add("-- Seluruh --")

        Using Dr = Open("select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer like '%" & TextBox21.Text & "%' order by kode_customer")
            Do While Dr.Read
                lv = ListView5.Items.Add(Dr("kode_customer"))
                lv.SubItems.Add(Dr("nama"))
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub TextBox22_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox22.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView5.Items.Count = 0 Then Exit Sub
            ListView5.Focus()
        End If
    End Sub

    Private Sub TextBox22_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox22.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox21.Text.Trim.Length = 0 Then TextBox22.Text = "" : ListView5.Visible = False ': Exit Sub
            Button1.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox22_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox22.Leave
        If ListView5.Focused = True Then Exit Sub
        TextBox21.Text = "" : TextBox22.Text = ""
    End Sub

    Private Sub TextBox22_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox22.TextChanged
        If TextBox22.Text.Trim.Length = 0 Then
            ListView5.Visible = False : Exit Sub
            Me.Size = New Size(631, 296)
        Else
            ListView5.Visible = True
            Me.Size = New Size(631, 487)
        End If

        OpenConn()

        Dim lv As New ListViewItem
        ListView5.Items.Clear()
        lv = ListView5.Items.Add("-- Seluruh --")
        lv.SubItems.Add("-- Seluruh --")

        Using Dr = Open("select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & TextBox22.Text & "%' order by nama")
            Do While Dr.Read
                lv = ListView5.Items.Add(Dr("kode_customer"))
                lv.SubItems.Add(Dr("nama"))
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub ListView5_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView5.DoubleClick
        Dim kode As String = ListView5.FocusedItem.Text
        Dim nama As String = ListView5.FocusedItem.SubItems(1).Text
        TextBox21.Text = kode
        TextBox22.Text = nama
        ListView5.Visible = False
        Button1.Focus()
        Me.Size = New Size(631, 296)
    End Sub

    Private Sub ListView5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView5.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView5_DoubleClick(ListView5, e)
        End If
    End Sub
End Class
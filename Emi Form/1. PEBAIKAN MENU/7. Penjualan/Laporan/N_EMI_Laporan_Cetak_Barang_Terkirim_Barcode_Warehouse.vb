Public Class N_EMI_Laporan_Cetak_Barang_Terkirim_Barcode_Warehouse
    Dim CrDoc As Object
    Dim xsplitx As String

    Public Sub laporan(ByVal formula As String, ByVal cr_title As String, ByVal form_title As String)
        CrDoc.RecordSelectionFormula = formula
        CrDoc.SummaryInfo.ReportTitle = cr_title
        A_Place_For_Printing.Text = form_title
    End Sub

    Private Sub Hr_Cetak_Barang_Terkirim_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Tgl1.Focus()
    End Sub

    Private Sub F_Cetak_X_Pemb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim y As New Point(132, 167)
        LvCust.Location = y
        LvCust.Visible = False
        Me.Size = New Size(638, 311)

        CmbJenis.Items.Clear()
        CmbJenis.Items.Add("Rekap")
        CmbJenis.Items.Add("Detail")

        CmbLokasi.Items.Clear() : CmbLokasi.Items.Add("SELURUH")
        CmbLokasi.SelectedIndex = 0

        Try
            OpenConn()

            xSplit = CekKotaRole().Split(",")

            SQL = "select kode_stock_owner from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "aktif = 'Y' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)

            SQL = SQL & ") "

            SQL = SQL & "order by kode_stock_owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbLokasi.Items.Add(Dr("kode_stock_owner"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf CmbLokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbLokasi.Focus() : Exit Sub
        ElseIf TxtKdCust.Text.Trim.Length = 0 Then
            MessageBox.Show("Customer harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKdCust.Focus() : Exit Sub
        ElseIf CmbJenis.SelectedIndex = -1 Then
            MessageBox.Show("Jenis harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbJenis.Focus()
            Exit Sub
        End If

        Try

            OpenConn()

            If CmbJenis.SelectedIndex = 0 Then

                Dim SF As String = ""

#Region "Kode Lama"

                'SQL = "Alter View Rekap_Brg_Terkirim "
                'SQL = SQL & "AS "

                'SQL = SQL & "SELECT kode_perusahaan, Lokasi, No_Faktur, Tanggal, No_DO, Tanggal_DO, Tanggal_Terima, Kode_Customer, "
                'SQL = SQL & "Nama_Cust, Kode_Karyawan, Jenis_Transaksi, Flag_Lunas_DO, "
                'SQL = SQL & "Total_Baru_Dikurang_Diskon AS DPP_Sub_Inv, Nilai_PPN_Baru AS PPN_Sub_Inv, "
                'SQL = SQL & "Total_Baru_Dikurang_Diskon + Nilai_PPN_Baru AS Grand_Sub_Inv, "
                'SQL = SQL & "Retur_Baru_Dikurang_Diskon AS Retur_Sub_Inv, Nilai_PPN_Retur_Baru AS PPN_Retur_Sub_Inv, "
                'SQL = SQL & "Retur_Baru_Dikurang_Diskon + Nilai_PPN_Retur_Baru AS Grand_Retur_Sub_Inv, "
                'SQL = SQL & "'" & UserID & "' AS UserID, tgl_lunas "

                'SQL = SQL & "FROM dbo.Rekap_Sub_Invoice "
                'SQL = SQL & "WHERE Tanggal_Terima BETWEEN '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' AND '"
                'SQL = SQL & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

                'If Not TxtKdCust.Text.Trim.ToUpper = "-- SELURUH --" Then
                '    SQL = SQL & "AND Kode_Customer = '" & TxtKdCust.Text & "'"
                'End If

                'If CmbLokasi.SelectedIndex = 0 Then
                '    SQL = SQL & " AND Lokasi IN("
                '    Dim List_Kota As String = ""
                '    For x As Integer = 1 To CmbLokasi.Items.Count - 1
                '        List_Kota = List_Kota & "'" & CmbLokasi.Items(x).ToString & "', "
                '    Next
                '    List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)
                '    SQL = SQL & List_Kota & ")"
                'Else
                '    SQL = SQL & " AND Lokasi = '" & CmbLokasi.Text & "'"
                'End If
                'ExecuteTrans(SQL)

                'SQL = "SELECT Lokasi FROM Rekap_Brg_Terkirim WHERE UserID = '" & UserID & "'"
#End Region


                SQL = "SELECT Lokasi FROM N_EMI_View_Laporan_Rekap_Sub_Invoice "
                SQL = SQL & "WHERE Tanggal_Terima BETWEEN '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' AND '"
                SQL = SQL & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{N_EMI_View_Laporan_Rekap_Sub_Invoice.Tanggal_Terima} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# AND "
                SF = SF & "{N_EMI_View_Laporan_Rekap_Sub_Invoice.Tanggal_Terima} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not TxtKdCust.Text.Trim.ToUpper = "-- SELURUH --" Then
                    SQL = SQL & "AND Kode_Customer = '" & TxtKdCust.Text & "' "
                    SF = SF & "AND {N_EMI_View_Laporan_Rekap_Sub_Invoice.Kode_Customer} = '" & TxtKdCust.Text & "'"
                End If

                If CmbLokasi.SelectedIndex = 0 Then
                    SQL = SQL & " AND Lokasi IN("
                    SF = SF & "AND {N_EMI_View_Laporan_Rekap_Sub_Invoice.Lokasi} IN ["

                    Dim List_Kota As String = ""
                    For x As Integer = 1 To CmbLokasi.Items.Count - 1
                        List_Kota = List_Kota & "'" & CmbLokasi.Items(x).ToString & "', "
                    Next
                    List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)
                    SQL = SQL & List_Kota & ") "
                    SF = SF & List_Kota & "]"
                Else
                    SQL = SQL & " AND Lokasi = '" & CmbLokasi.Text & "' "
                    SF = SF & " AND {N_EMI_View_Laporan_Rekap_Sub_Invoice.Lokasi} = '" & CmbLokasi.Text & "' "
                End If

                'SQL = SQL & "and UserID = '" & UserID & "'"

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New N_EMI_CR_Laporan_Barang_Terkirim_Barcode_Rekap_Warehouse
                        With A_Place_For_Printing
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = SF
                            CrDoc.SummaryInfo.ReportTitle = CmbJenis.Text.ToUpper & Chr(13) &
                                                                "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " &
                                                                Format(Tgl2.Value, "dd MMM yyyy") & Chr(13) &
                                                                "Lokasi : " & CmbLokasi.Text
                            .Text = "Laporan Rekap Barang Terkirim"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.DisplayGroupTree = False
                            .Refresh()
                            .Show()
                        End With
                    Else
                        MessageBox.Show("Data tidak ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End Using
            Else
                Dim SF As String = ""
#Region "Kode Lama"

                SQL = "SELECT TOP 1 * "
                SQL = SQL & "FROM Sub_Invoice "
                SQL = SQL & "WHERE Tanggal_Terima BETWEEN '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' AND '"
                SQL = SQL & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

                SF = "{Sub_Invoice.Tanggal_Terima} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# AND "
                SF = SF & "{Sub_Invoice.Tanggal_Terima} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not TxtKdCust.Text.Trim.ToUpper = "-- SELURUH --" Then
                    SQL = SQL & "AND Kode_Customer = '" & TxtKdCust.Text & "'"
                    SF = SF & "AND {Sub_Invoice.Kode_Customer} = '" & TxtKdCust.Text & "'"
                End If

                ' SQL = SQL & " and no_do in(select no_fak from tes2) "

                If CmbLokasi.SelectedIndex = 0 Then
                    SQL = SQL & " AND Lokasi IN("
                    SF = SF & "AND {Sub_Invoice.Lokasi} IN ["

                    Dim List_Kota As String = ""
                    For x As Integer = 1 To CmbLokasi.Items.Count - 1
                        List_Kota = List_Kota & "'" & CmbLokasi.Items(x).ToString & "', "
                    Next
                    List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)

                    SQL = SQL & List_Kota & ")"
                    SF = SF & List_Kota & "]"
                Else
                    SQL = SQL & " AND Lokasi = '" & CmbLokasi.Text & "'"
                    SF = SF & " AND {Sub_Invoice.Lokasi} = '" & CmbLokasi.Text & "'"
                End If
#End Region

                SQL = "SELECT TOP 1 kode_perusahaan "
                SQL = SQL & "FROM N_EMI_View_Sub_Invoice_Barcode "
                SQL = SQL & "WHERE Tanggal_Terima BETWEEN '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' AND '"
                SQL = SQL & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

                SF = "{N_EMI_View_Sub_Invoice_Barcode.Tanggal_Terima} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# AND "
                SF = SF & "{N_EMI_View_Sub_Invoice_Barcode.Tanggal_Terima} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not TxtKdCust.Text.Trim.ToUpper = "-- SELURUH --" Then
                    SQL = SQL & "AND Kode_Customer = '" & TxtKdCust.Text & "'"
                    SF = SF & "AND {N_EMI_View_Sub_Invoice_Barcode.Kode_Customer} = '" & TxtKdCust.Text & "'"
                End If

                ' SQL = SQL & " and no_do in(select no_fak from tes2) "

                If CmbLokasi.SelectedIndex = 0 Then
                    SQL = SQL & " AND Lokasi IN("
                    SF = SF & "AND {N_EMI_View_Sub_Invoice_Barcode.Lokasi} IN ["

                    Dim List_Kota As String = ""
                    For x As Integer = 1 To CmbLokasi.Items.Count - 1
                        List_Kota = List_Kota & "'" & CmbLokasi.Items(x).ToString & "', "
                    Next
                    List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)

                    SQL = SQL & List_Kota & ")"
                    SF = SF & List_Kota & "]"
                Else
                    SQL = SQL & " AND Lokasi = '" & CmbLokasi.Text & "' "
                    SF = SF & " AND {N_EMI_View_Sub_Invoice_Barcode.Lokasi} = '" & CmbLokasi.Text & "' "
                End If

                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New N_EMI_CR_Laporan_Barang_Terkirim_Barcode_Detail_Warehouse

                        With A_Place_For_Printing
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = SF
                            CrDoc.SummaryInfo.ReportTitle = CmbJenis.Text.ToUpper & Chr(13) &
                                                                "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " &
                                                                Format(Tgl2.Value, "dd MMM yyyy") & Chr(13) &
                                                                "Lokasi : " & CmbLokasi.Text
                            .Text = "Laporan Detail Barang Terkirim"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.DisplayGroupTree = False
                            .Refresh()
                            .Show()
                        End With
                    Else
                        MessageBox.Show("Data tidak ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End Using
            End If

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub DateTimePicker3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub DateTimePicker4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then CmbLokasi.Focus()
    End Sub

    Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbLokasi.KeyPress
        If e.KeyChar = Chr(13) Then TxtKdCust.Focus()
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtKdCust.KeyDown
        If e.KeyCode = Keys.Down Then
            LvCust.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtKdCust.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKdCust.Text.Trim.Length = 0 Then
                LvCust.Visible = False : TxtNama.Focus()
                Me.Size = New Size(638, 311)
                Exit Sub
            End If
            TextBox3_Leave(TxtKdCust, e)
        End If
    End Sub

    Private Sub TextBox3_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKdCust.Leave
        If TxtKdCust.Text.Trim.Length = 0 Then
            LvCust.Visible = False
            Me.Size = New Size(638, 311)
            Exit Sub
        Else
            LvCust.Visible = True
            Me.Size = New Size(638, 360)
        End If
        If LvCust.Focused = True Then Exit Sub

        Try

            OpenConn()

            SQL = "select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & TxtKdCust.Text.Trim & "' "
            If CmbLokasi.SelectedIndex = 0 Then
                SQL = SQL & " and lokasi in("
                Dim List_Kota As String = ""
                For x As Integer = 1 To CmbLokasi.Items.Count - 1
                    List_Kota = List_Kota & "'" & CmbLokasi.Items(x).ToString & "', "
                Next
                List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)
                SQL = SQL & List_Kota & ")"
            Else
                SQL = SQL & " and lokasi = '" & CmbLokasi.Text & "'"
            End If
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtKdCust.Text = Dr("kode_customer")
                    TxtNama.Text = Dr("nama")
                    CmbJenis.Focus()
                Else
                    TxtKdCust.Text = "" : TxtNama.Text = ""
                    TxtKdCust.Focus()
                End If
                LvCust.Visible = False
                Me.Size = New Size(638, 311)
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtKdCust.TextChanged
        If TxtKdCust.Text.Trim.Length = 0 Then
            LvCust.Visible = False
            Me.Size = New Size(638, 311)
            Exit Sub
        Else
            LvCust.Visible = True
            Me.Size = New Size(638, 360)
        End If

        LvCust.Items.Clear()
        Dim LV As New ListViewItem

        LV = LvCust.Items.Add("-- Seluruh --")
        LV.SubItems.Add("-- Seluruh --")

        Try

            OpenConn()

            SQL = "select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer like '" & TxtKdCust.Text & "%' "
            If CmbLokasi.SelectedIndex = 0 Then
                SQL = SQL & " and lokasi in("
                Dim List_Kota As String = ""
                For x As Integer = 1 To CmbLokasi.Items.Count - 1
                    List_Kota = List_Kota & "'" & CmbLokasi.Items(x).ToString & "', "
                Next
                List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)
                SQL = SQL & List_Kota & ")"
            Else
                SQL = SQL & " and lokasi = '" & CmbLokasi.Text & "'"
            End If
            SQL = SQL & "order by kode_customer"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    LV = LvCust.Items.Add(Dr("kode_customer"))
                    LV.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvCust.DoubleClick
        If LvCust.Items.Count = 0 Then Exit Sub
        Dim kode As String = LvCust.FocusedItem.Text
        Dim nama As String = LvCust.FocusedItem.SubItems(1).Text
        TxtKdCust.Text = kode
        TxtNama.Text = nama
        LvCust.Visible = False
        Me.Size = New Size(638, 311)
        CmbJenis.Focus()
    End Sub

    Private Sub ListView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles LvCust.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(LvCust, e)
        End If
    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtNama.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvCust.Items.Count = 0 Then Exit Sub
            LvCust.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNama.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKdCust.Text.Trim.Length = 0 Then TxtNama.Text = "" : LvCust.Visible = False ': Exit Sub
            Me.Size = New Size(638, 311)
            CmbLokasi.Focus()
        End If
    End Sub

    Private Sub TextBox4_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNama.Leave
        If LvCust.Focused = True Then Exit Sub
        TxtKdCust.Text = "" : TxtNama.Text = ""
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNama.TextChanged
        If TxtNama.Text.Trim.Length = 0 Then
            LvCust.Visible = False
            Me.Size = New Size(638, 311)
            Exit Sub
        Else
            LvCust.Visible = True
            Me.Size = New Size(638, 360)
        End If

        LvCust.Items.Clear()
        Dim LV As New ListViewItem

        LV = LvCust.Items.Add("-- Seluruh --")
        LV.SubItems.Add("-- Seluruh --")

        Try

            OpenConn()

            SQL = "select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and nama like '" & TxtNama.Text & "%' "
            If CmbLokasi.SelectedIndex = 0 Then
                SQL = SQL & " and lokasi in("
                Dim List_Kota As String = ""
                For x As Integer = 1 To CmbLokasi.Items.Count - 1
                    List_Kota = List_Kota & "'" & CmbLokasi.Items(x).ToString & "', "
                Next
                List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)
                SQL = SQL & List_Kota & ")"
            Else
                SQL = SQL & " and lokasi = '" & CmbLokasi.Text & "'"
            End If
            SQL = SQL & "order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    LV = LvCust.Items.Add(Dr("kode_customer"))
                    LV.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub F_Cetak_X_Pemb_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label4.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLokasi.SelectedIndexChanged

    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then CmbJenis.Focus()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ComboBox4_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbJenis.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbJenis.SelectedIndexChanged

    End Sub

    Private Sub DateTimePicker4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tgl2.ValueChanged

    End Sub
End Class
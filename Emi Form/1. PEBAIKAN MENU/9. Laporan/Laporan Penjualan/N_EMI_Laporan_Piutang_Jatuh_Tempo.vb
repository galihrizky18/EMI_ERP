Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class N_EMI_Laporan_Piutang_Jatuh_Tempo
    Dim lv As New ListViewItem
    Dim fSeluruh As String
    Dim fSeluruhKaryawan, role_kota As String
    Private Sub N_EMI_Laporan_Piutang_Jatuh_Tempo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        LvCustomer.Visible = False
        LvCustomer.Location = New Point(166, 183)

        ListView1.Visible = False
        ListView1.Location = New Point(166, 157)

        Me.Size = New Size(631, 329)

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date.AddDays(14)
        TxtKdCust.Text = "" : TxtNama.Text = ""

        CmbLokasi.Items.Clear() : CmbLokasi.Items.Add("SELURUH")
        CmbLokasi.SelectedIndex = 0

        'ComboBox1.Items.Clear()
        'ComboBox1.Items.Add("SELURUH")
        'ComboBox1.Items.Add("Lama")
        'ComboBox1.Items.Add("Baru")

        Try
            OpenConn()

            xSplit = CekKotaRole().Split(",")

            SQL = "select kode_stock_owner from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "aktif = 'Y' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
                'role_kota = role_kota & "'" & Dr("kode_kota") & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)
            SQL = SQL & ") "

            SQL = SQL & "order by kode_stock_owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbLokasi.Items.Add(Dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("SELURUH")
            ComboBox6.Items.Add("BELUM DIKATEGORIKAN")
            SQL = "Select kode_kategori From kategori_piutang where kode_perusahaan = '" & KodePerusahaan & "' order by kode_kategori"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_kategori"))
                Loop
            End Using

            If CekButtonRole("Tdk_Tampil_Seluruh_Cust_Lap_Piutang_JT") = "Y" Then
                fSeluruh = "T"
                fSeluruhKaryawan = "T"
            Else
                fSeluruh = "Y"
                fSeluruhKaryawan = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        LvCustomer.Items.Clear()
        Tgl1.Focus()
    End Sub

    Private Sub N_EMI_Laporan_Piutang_Jatuh_Tempo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Tgl1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then CmbLokasi.Focus()
    End Sub

    '=====================================
    'start auto complete
    '=====================================
    Private Sub TxtKdCust_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtKdCust.KeyDown
        If e.KeyCode = Keys.Down Then LvCustomer.Focus()
    End Sub

    Private Sub TxtKdCust_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtKdCust.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKdCust.Text.Trim.Length = 0 Then
                LvCustomer.Visible = False : TxtNama.Focus() : Exit Sub
            End If
            TxtKdCust_Leave(TxtKdCust, e)
        End If
    End Sub

    Private Sub TxtKdCust_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKdCust.Leave
        If TxtKdCust.Text.Trim.Length = 0 Then
            LvCustomer.Visible = False
            Me.Size = New Size(631, 329)
            Exit Sub
        Else
            LvCustomer.Visible = True
        End If
        If LvCustomer.Focused = True Then Exit Sub

        Try

            OpenConn()

            SQL = "select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & TxtKdCust.Text.Trim & "' "
            If CmbLokasi.SelectedIndex = 0 Then
                SQL = SQL & " and lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To CmbLokasi.Items.Count - 1
                    list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and lokasi = '" & CmbLokasi.Text & "'"
            End If
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtKdCust.Text = Dr("kode_customer")
                    TxtNama.Text = Dr("nama")
                    ComboBox6.Focus()
                Else
                    TxtKdCust.Text = "" : TxtNama.Text = ""
                    TxtKdCust.Focus()
                End If
                LvCustomer.Visible = False
                Me.Size = New Size(631, 329)
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKdCust_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtKdCust.TextChanged
        If TxtKdCust.Text.Trim.Length = 0 Then
            LvCustomer.Visible = False
            Me.Size = New Size(631, 329)
            Exit Sub
        Else
            LvCustomer.Visible = True
            Me.Size = New Size(631, 491)
        End If

        LvCustomer.Items.Clear()
        If fSeluruh = "Y" Then
            lv = LvCustomer.Items.Add("-- Seluruh --")
            lv.SubItems.Add("-- Seluruh --")
        End If

        Try
            OpenConn()

            SQL = "select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer like '" & TxtKdCust.Text & "%' "
            If CmbLokasi.SelectedIndex = 0 Then
                SQL = SQL & " and lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To CmbLokasi.Items.Count - 1
                    list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and lokasi = '" & CmbLokasi.Text & "'"
            End If
            SQL = SQL & "order by kode_customer"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvCustomer.Items.Add(Dr("kode_customer"))
                    lv.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtNama_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtNama.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvCustomer.Items.Count = 0 Then Exit Sub
            LvCustomer.Focus()
        End If
    End Sub

    Private Sub TxtNama_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNama.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKdCust.Text.Trim.Length = 0 Then TxtNama.Text = "" : LvCustomer.Visible = False ': Exit Sub
            CmbLokasi.Focus()
        End If
    End Sub

    Private Sub TxtNama_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNama.Leave
        If LvCustomer.Focused = True Then Exit Sub
        TxtKdCust.Text = "" : TxtNama.Text = ""
    End Sub

    Private Sub TxtNama_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNama.TextChanged
        If TxtNama.Text.Trim.Length = 0 Then
            LvCustomer.Visible = False
            Me.Size = New Size(631, 329)
            Exit Sub
        Else
            LvCustomer.Visible = True
            Me.Size = New Size(631, 491)
        End If

        LvCustomer.Items.Clear()
        If fSeluruh = "Y" Then
            lv = LvCustomer.Items.Add("-- Seluruh --")
            lv.SubItems.Add("-- Seluruh --")
        End If

        Try
            OpenConn()

            SQL = "select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and nama like '" & TxtNama.Text & "%' "
            If CmbLokasi.SelectedIndex = 0 Then
                SQL = SQL & " and lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To CmbLokasi.Items.Count - 1
                    list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and lokasi = '" & CmbLokasi.Text & "'"
            End If
            SQL = SQL & "order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvCustomer.Items.Add(Dr("kode_customer"))
                    lv.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvCustomer_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvCustomer.DoubleClick
        If LvCustomer.Items.Count = 0 Then Exit Sub

        Dim Kode As String = LvCustomer.FocusedItem.Text
        Dim Nama As String = LvCustomer.FocusedItem.SubItems(1).Text

        TxtKdCust.Text = Kode
        TxtNama.Text = Nama
        LvCustomer.Visible = False
        ComboBox6.Focus()
        Me.Size = New Size(631, 329)
    End Sub

    Private Sub LvCustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles LvCustomer.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvCustomer_DoubleClick(LvCustomer, e)
        End If
    End Sub

    '=====================================
    'end auto complete
    '=====================================

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date.AddDays(14)
            Tgl1.Focus() : Exit Sub
        ElseIf CmbLokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbLokasi.Focus() : Exit Sub
        ElseIf TxtKdKaryawan.Text.Trim.Length = 0 Then
            MessageBox.Show("Karyawan harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKdKaryawan.Focus() : Exit Sub
        ElseIf TxtKdCust.Text.Trim.Length = 0 Then
            MessageBox.Show("Customer harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKdCust.Focus() : Exit Sub
        ElseIf ComboBox6.SelectedIndex = -1 Then
            MessageBox.Show("Kategori piutang harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus() : Exit Sub
            'ElseIf ComboBox1.SelectedIndex = -1 Then
            '    MessageBox.Show("Metode pot stock harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBox1.Focus() : Exit Sub
        End If

        If fSeluruh = "T" Then
            If TxtKdCust.Text.Trim.ToUpper = "-- Seluruh --" Then
                MessageBox.Show("Customer harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtKdCust.Focus() : Exit Sub
            End If
        End If

        Dim CRSF As String = ""

        SQL = "SELECT top 1 lokasi, no_faktur, tanggal as tgl_invoice, no_do, tanggal_DO, Jenis_Transaksi, "
        SQL = SQL & "Tgl_Jatuh_Tempo, Kode_Customer, nama_cust, total_baru_dikurang_diskon, nilai_ppn_baru, "
        SQL = SQL & "total_baru_dikurang_diskon + nilai_ppn_baru as grand_sub_inv, retur_baru_dikurang_diskon, "
        SQL = SQL & "nilai_ppn_retur_baru, retur_baru_dikurang_diskon + nilai_ppn_retur_baru as retur_sub_inv, "
        SQL = SQL & "sudah_dilunasi "

        SQL = SQL & "FROM Rekap_Sub_Invoice where "

        SQL = SQL & "(total_baru_dikurang_diskon + nilai_ppn_baru) - (retur_baru_dikurang_diskon + nilai_ppn_retur_baru) - "
        SQL = SQL & "(retur_baru_beda_bulan_dikurang_diskon + nilai_ppn_retur_baru_beda_bulan) - sudah_dilunasi > 0 and "

        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' AND "
        SQL = SQL & "tgl_jatuh_tempo between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' AND '"
        SQL = SQL & Format(Tgl2.Value, "yyyy-MM-dd") & "' AND flag_lunas_do is NULL "

        CRSF = "({Rekap_Sub_Invoice.total_baru_dikurang_diskon} + {Rekap_Sub_Invoice.nilai_ppn_baru}) - ({Rekap_Sub_Invoice.retur_baru_dikurang_diskon} + {Rekap_Sub_Invoice.nilai_ppn_retur_baru}) - "
        CRSF = CRSF & "({Rekap_Sub_Invoice.retur_baru_beda_bulan_dikurang_diskon} + {Rekap_Sub_Invoice.nilai_ppn_retur_baru_beda_bulan}) - {Rekap_Sub_Invoice.sudah_dilunasi} > 0 and "

        CRSF = CRSF & "{Rekap_Sub_Invoice.Kode_Perusahaan} = '" & KodePerusahaan & "' AND "
        CRSF = CRSF & "{Rekap_Sub_Invoice.tgl_jatuh_tempo} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# AND "
        CRSF = CRSF & "{Rekap_Sub_Invoice.Tgl_Jatuh_tempo} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# AND "
        CRSF = CRSF & "ISNULL({Rekap_Sub_Invoice.flag_lunas_do}) "


        'karyawan
        If Not TxtKdKaryawan.Text.ToUpper = "-- SELURUH --" Then
            SQL = SQL & "AND kode_karyawan = '" & TxtKdKaryawan.Text.Trim & "' "
            CRSF = CRSF & "AND {Rekap_Sub_Invoice.Kode_Karyawan} = '" & TxtKdKaryawan.Text.Trim & "' "
        End If

        'CUSTOMER
        If Not TxtKdCust.Text.ToUpper = "-- SELURUH --" Then
            SQL = SQL & "AND kode_customer = '" & TxtKdCust.Text.Trim & "' "
            CRSF = CRSF & "AND {Rekap_Sub_Invoice.Kode_Customer} = '" & TxtKdCust.Text.Trim & "' "
        End If


        'LOKASI
        If CmbLokasi.SelectedIndex = 0 Then
            SQL = SQL & " and Lokasi in("

            Dim list_kota As String = ""

            For x As Integer = 1 To CmbLokasi.Items.Count - 1
                list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
            Next

            list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            SQL = SQL & list_kota & ")"

            CRSF = CRSF & " and {Rekap_Sub_Invoice.Lokasi} in ["
            CRSF = CRSF & list_kota
            CRSF = CRSF & "]"
        Else
            SQL = SQL & " and lokasi = '" & CmbLokasi.Text & "'"
            CRSF = CRSF & " and {Rekap_Sub_Invoice.lokasi} = '" & CmbLokasi.Text & "' "
        End If

        If ComboBox6.SelectedIndex = 0 Then

        ElseIf ComboBox6.SelectedIndex = 1 Then
            SQL = SQL & " and kode_kategori_piutang is null "
            CRSF = CRSF & " and isnull({Rekap_Sub_Invoice.kode_kategori_piutang}) "
        ElseIf ComboBox6.SelectedIndex = 2 Then
            SQL = SQL & " and kode_kategori_piutang = '" & ComboBox6.Text & "' "
            CRSF = CRSF & " and {Rekap_Sub_Invoice.kode_kategori_piutang} = '" & ComboBox6.Text & "' "
        End If

        get_jam()

        Try
            OpenConn()

            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc = New N_EMI_CR_Laporan_Piutang_Jatuh_Tempo

                    With A_Place_For_Printing
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = CRSF
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " &
                                            Format(Tgl2.Value, "dd MMM yyyy") & Chr(13) &
                                            "Lokasi : " & CmbLokasi.Text & " | Karyawan :  " & TxtNamaKaryawan.Text.Trim & " | Customer : " & TxtNama.Text

                        Dim crParameterDiscreteValue As ParameterDiscreteValue
                        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
                        Dim crParameterFieldLocation As ParameterFieldDefinition
                        Dim crParameterValues As ParameterValues

                        crParameterFieldDefinitions = CrDoc.DataDefinition.ParameterFields


                        crParameterFieldLocation = crParameterFieldDefinitions.Item("tgl_piutang_kredit_lama")
                        crParameterValues = crParameterFieldLocation.CurrentValues
                        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                        crParameterDiscreteValue.Value = Format(tgl_skg, "yyyy-MM-01")
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                        crParameterFieldLocation = crParameterFieldDefinitions.Item("tgl_piutang_kredit_berjalan1")
                        crParameterValues = crParameterFieldLocation.CurrentValues
                        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                        crParameterDiscreteValue.Value = Format(tgl_skg, "yyyy-MM-01")
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                        crParameterFieldLocation = crParameterFieldDefinitions.Item("tgl_piutang_kredit_berjalan2")
                        crParameterValues = crParameterFieldLocation.CurrentValues
                        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                        crParameterDiscreteValue.Value = Format(tgl_skg, "yyyy-MM-dd")
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


                        crParameterFieldLocation = crParameterFieldDefinitions.Item("tgl_piutang_kredit_blm_jt")
                        crParameterValues = crParameterFieldLocation.CurrentValues
                        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                        crParameterDiscreteValue.Value = Format(DateSerial(CDate(tgl_skg).Year, CDate(tgl_skg).Month + 1, 0), "yyyy-MM-dd")
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                        crParameterFieldLocation = crParameterFieldDefinitions.Item("tgl_piutang_lama_tunai")
                        crParameterValues = crParameterFieldLocation.CurrentValues
                        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                        crParameterDiscreteValue.Value = Format(CDate(tgl_skg), "yyyy-MM-01")
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                        crParameterFieldLocation = crParameterFieldDefinitions.Item("tgl_piutang_tunai_bln_berjalan1")
                        crParameterValues = crParameterFieldLocation.CurrentValues
                        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                        crParameterDiscreteValue.Value = Format(CDate(tgl_skg), "yyyy-MM-01")
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


                        crParameterFieldLocation = crParameterFieldDefinitions.Item("tgl_piutang_tunai_bln_berjalan2")
                        crParameterValues = crParameterFieldLocation.CurrentValues
                        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                        crParameterDiscreteValue.Value = Format(DateSerial(CDate(tgl_skg).Year, CDate(tgl_skg).Month + 1, 0), "yyyy-MM-dd")
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                        .Text = "Laporan Daftar Piutang Jatuh Tempo"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                    End With
                Else
                    MessageBox.Show("Data tidak ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CmbLokasi_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbLokasi.KeyPress
        If e.KeyChar = Chr(13) Then TxtKdKaryawan.Focus()
    End Sub


    Private Sub ComboBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox6.KeyPress
        If e.KeyChar = Chr(13) Then
            BtnCetak.Focus()
        End If
    End Sub

    Private Sub TxtKdKaryawan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtKdKaryawan.TextChanged
        If TxtKdKaryawan.Text.Trim.Length = 0 Then
            ListView1.Visible = False
            Me.Size = New Size(631, 329)
            Exit Sub
        Else
            ListView1.Visible = True
            Me.Size = New Size(631, 491)
        End If

        ListView1.Items.Clear()
        'If fSeluruhKaryawan = "Y" Then

        'End If
        lv = ListView1.Items.Add("-- Seluruh --")
        lv.SubItems.Add("-- Seluruh --")

        Try
            OpenConn()
            TxtNamaKaryawan.Text = ""

            xSplit = CekKotaRole().Split(",")

            SQL = "select a.Kode_Karyawan,a.Nama,b.Kode_Kota from Karyawan a,Stock_Owner b where  "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Lokasi = b.Kode_Stock_Owner and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Aktif = 'Y' and a.kode_karyawan like '%" & TxtKdKaryawan.Text.Trim & "%' "
            SQL = SQL & "and b.aktif = 'Y' and b.kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
                'role_kota = role_kota & "'" & Dr("kode_kota") & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)
            SQL = SQL & ") "
            SQL = SQL & "order by kode_karyawan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView1.Items.Add(Dr("kode_karyawan"))
                    lv.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CmbLokasi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLokasi.SelectedIndexChanged
        TxtKdKaryawan.Text = ""
        TxtNamaKaryawan.Text = ""
        TxtKdCust.Text = ""
        TxtNama.Text = ""
    End Sub

    Private Sub TxtKdKaryawan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtKdKaryawan.KeyDown
        If e.KeyCode = Keys.Down Then ListView1.Focus()
    End Sub

    Private Sub TxtKdKaryawan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtKdKaryawan.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKdKaryawan.Text.Trim.Length = 0 Then
                ListView1.Visible = False : TxtNamaKaryawan.Focus() : Exit Sub
            End If
            TxtKdCust_Leave(TxtKdCust, e)
        End If
    End Sub

    Private Sub TxtKdKaryawan_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtKdKaryawan.Leave
        If TxtKdKaryawan.Text.Trim.Length = 0 Then
            ListView1.Visible = False
            Me.Size = New Size(631, 329)
            Exit Sub
        Else
            ListView1.Visible = True
            Me.Size = New Size(631, 491)
        End If
        If ListView1.Focused = True Then Exit Sub

        Try

            OpenConn()

            xSplit = CekKotaRole().Split(",")

            SQL = "select a.Kode_Karyawan,a.Nama,b.Kode_Kota from Karyawan a,Stock_Owner b where  "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Lokasi = b.Kode_Stock_Owner and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Aktif = 'Y' and a.kode_karyawan = '" & TxtKdKaryawan.Text.Trim & "' "
            SQL = SQL & "and b.aktif = 'Y' and b.kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
                'role_kota = role_kota & "'" & Dr("kode_kota") & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)
            SQL = SQL & ") "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtKdKaryawan.Text = Dr("kode_karyawan")
                    TxtNamaKaryawan.Text = Dr("nama")
                    TxtKdCust.Focus()
                Else
                    TxtKdKaryawan.Text = "" : TxtNamaKaryawan.Text = ""
                    TxtKdKaryawan.Focus()
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

    Private Sub ListView1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView1_DoubleClick(ListView1, e)
        End If
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub

        Dim Kode As String = ListView1.FocusedItem.Text
        Dim Nama As String = ListView1.FocusedItem.SubItems(1).Text

        TxtKdKaryawan.Text = Kode
        TxtNamaKaryawan.Text = Nama
        ListView1.Visible = False
        TxtKdCust.Focus()
        Me.Size = New Size(631, 329)
    End Sub

    Private Sub TxtNamaKaryawan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNamaKaryawan.TextChanged
        If TxtNamaKaryawan.Text.Trim.Length = 0 Then
            ListView1.Visible = False
            Me.Size = New Size(631, 329)
            Exit Sub
        Else
            ListView1.Visible = True
            Me.Size = New Size(631, 491)
        End If

        ListView1.Items.Clear()
        'If fSeluruhKaryawan = "Y" Then

        'End If
        lv = ListView1.Items.Add("-- Seluruh --")
        lv.SubItems.Add("-- Seluruh --")
        Try
            OpenConn()

            xSplit = CekKotaRole().Split(",")

            SQL = "select a.Kode_Karyawan,a.Nama,b.Kode_Kota from Karyawan a,Stock_Owner b where  "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Lokasi = b.Kode_Stock_Owner and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Aktif = 'Y' and a.nama like '%" & TxtNamaKaryawan.Text.Trim & "%' "
            SQL = SQL & "and b.aktif = 'Y' and b.kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
                'role_kota = role_kota & "'" & Dr("kode_kota") & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)
            SQL = SQL & ") "
            SQL = SQL & "order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView1.Items.Add(Dr("kode_karyawan"))
                    lv.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtNamaKaryawan_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNamaKaryawan.Leave
        If ListView1.Focused = True Then Exit Sub
        TxtKdKaryawan.Text = "" : TxtNamaKaryawan.Text = ""
    End Sub

    Private Sub TxtNamaKaryawan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNamaKaryawan.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKdKaryawan.Text.Trim.Length = 0 Then TxtNamaKaryawan.Text = "" : ListView1.Visible = False ': Exit Sub
            TxtKdCust.Focus()
        End If
    End Sub



    Private Sub TxtNamaKaryawan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtNamaKaryawan.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView1.Items.Count = 0 Then Exit Sub
            ListView1.Focus()
        End If
    End Sub
End Class
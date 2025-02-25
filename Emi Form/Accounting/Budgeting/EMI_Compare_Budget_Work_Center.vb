Public Class EMI_Compare_Budget_Work_Center

    Dim arrLokasi, arrFilterTanggal, arrKodeJnsBiaya, arrParamLain As New ArrayList

    Dim DynamicColumn As Integer = 4

    Dim ItemDgv_IdRouting As Integer = 0
    Dim ItemDgv_IdWorkCenter As Integer = 1
    Dim ItemDgv_Routing As Integer = 211
    Dim ItemDgv_WorkCenter As Integer = 3

    Private Sub EMI_Compare_Budget_Work_Center_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub

    Private Sub kosong()

        'LISTVIEW
        Dgv_Data.Rows.Clear()

        'FILTER
        Cmb_Lokasi.Items.Clear()

        CheckBox1.Checked = False
        CheckBox2.Checked = False
        Cmb_FilterTanggal.Items.Clear() : DateTimePicker1.Value = Date.Now
        Cmb_FilterTanggal.Items.Add("Tanggal Budgeting")
        Cmb_FilterTanggal.SelectedIndex = 0
        Cmb_FilterTanggal.Enabled = False

        CheckBox3.Checked = False
        Cmb_ParamLain.Items.Clear() : TextBox4.Text = ""
        Cmb_ParamLain.Items.Add("Routing") : arrParamLain.Add("a.Keterangan")
        Cmb_ParamLain.Items.Add("Work Center") : arrParamLain.Add("c.Keterangan")

        Try
            OpenConn()

            SQL = "select kode_stock_owner, keterangan from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Cmb_Lokasi.Items.Add(Dr("keterangan")) : arrLokasi.Add(Dr("kode_stock_owner"))
                Loop
            End Using
            Cmb_Lokasi.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not CheckBox1.Checked And Not CheckBox2.Checked Then
            MessageBox.Show("Tanggal Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If CheckBox3.Checked Then
            If Cmb_ParamLain.SelectedIndex = -1 Then
                MessageBox.Show("Jenis Param Lain Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_ParamLain.Focus() : Exit Sub
            End If

        End If

        Load_Data()

    End Sub

    Private Sub Load_Data()

        Try
            OpenConn()

            Dgv_Data.Rows.Clear()

            'HAPUS KOLOM DINAMIS
            For i As Integer = Dgv_Data.Columns.Count - 1 To DynamicColumn Step -1
                Dgv_Data.Columns.RemoveAt(i)
            Next

            '=========================================
            '=     GET JENIS BIAYA (ADD COLUMN)      =
            '=========================================
            Dim ColNum As Integer = DynamicColumn : arrKodeJnsBiaya.Clear()
            SQL = "Select kode_jenis_biaya_produksi, Keterangan from emi_jenis_biaya_produksi where kode_perusahaan = '" & KodePerusahaan & "' order by kode_jenis_biaya_produksi"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dgv_Data.Columns.Add(dr("kode_jenis_biaya_produksi"), dr("Keterangan"))
                    Dgv_Data.Columns(ColNum).Width = 130
                    Dgv_Data.Columns(ColNum).ReadOnly = True
                    Dgv_Data.Columns(ColNum).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    Dgv_Data.Columns.Add($"{dr("kode_jenis_biaya_produksi")}-act", $"{dr("Keterangan")} Actual")

                    Dgv_Data.Columns(ColNum).Width = 130
                    Dgv_Data.Columns(ColNum).ReadOnly = True
                    Dgv_Data.Columns(ColNum).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    arrKodeJnsBiaya.Add(dr("kode_jenis_biaya_produksi"))

                    ColNum += 1
                Loop
            End Using

            '========================================================================================================================================================================================

            Dim SelectedMonth, SelectedYear, FormatSelectedTanggal As String

            If CheckBox1.Checked Then
                FormatSelectedTanggal = Date.Now

            ElseIf CheckBox2.Checked Then
                SelectedMonth = DateTimePicker1.Value.ToString("MM")
                SelectedYear = DateTimePicker1.Value.ToString("yyyy") '

                FormatSelectedTanggal = SelectedYear & "-" & SelectedMonth & "-01"

            End If

            'SQL = "select a.No_Faktur, b.Id_Routing, b.Id_Work_Center, d.Keterangan as Routing, c.Keterangan as WorkCenter"
            'SQL = SQL & "from Emi_Budgeting_Work_Center a, Emi_Budgeting_Work_Center_Detail b, EMI_Master_Work_Center c, emi_master_routing d, emi_master_routing_detail e "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            'SQL = SQL & "and b.Id_Work_Center = c.Id_Work_Center "
            'SQL = SQL & "and b.Id_Routing = d.Id_Routing "
            'SQL = SQL & "and d.Id_Routing = e.Id_Routing "
            'SQL = SQL & "and b.Id_Work_Center = e.Id_Work_Center "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.Bulan = '" & SelectedMonth & "' "
            'SQL = SQL & "and a.Tahun = '" & SelectedYear & "' "
            'SQL = SQL & "and a.Status  is null "
            'SQL = SQL & "order by d.Id_Routing, e.Id_Work_Center "

            SQL = "select a.Id_Routing, a.Keterangan as Routing, c.Id_Work_Center, c.Keterangan as WorkCenter "
            SQL = SQL & "from emi_master_routing a, emi_master_routing_detail b, EMI_Master_Work_Center c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Routing = b.Id_Routing "
            SQL = SQL & "and b.Id_Work_Center = c.Id_Work_Center "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrParamLain.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
            End If

            SQL = SQL & "order by a.Id_Routing, c.Id_Work_Center "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim row As Integer = Dgv_Data.Rows.Count

                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Data.Rows.Add(1)
                            Dgv_Data.Rows(row).Cells(ItemDgv_IdRouting).Value = .Rows(i).Item("Id_Routing")
                            Dgv_Data.Rows(row).Cells(ItemDgv_IdWorkCenter).Value = .Rows(i).Item("Id_Work_Center")
                            Dgv_Data.Rows(row).Cells(ItemDgv_Routing).Value = .Rows(i).Item("Routing")
                            Dgv_Data.Rows(row).Cells(ItemDgv_WorkCenter).Value = .Rows(i).Item("WorkCenter")

                            '===========================================
                            '=     GET DATA DETAIL PER WORK CENTER     =
                            '===========================================
                            SQL = "select b.Biaya, b.Jenis_Biaya "
                            SQL = SQL & "from Emi_Budgeting_Work_Center a, Emi_Budgeting_Work_Center_Det b "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Status is null "
                            SQL = SQL & "and a.Bulan = '" & SelectedMonth & "' "
                            SQL = SQL & "and a.Tahun = '" & SelectedYear & "' "
                            SQL = SQL & "and b.Id_Routing = '" & .Rows(i).Item("Id_Routing") & "' "
                            SQL = SQL & "and b.Id_Work_Center = '" & .Rows(i).Item("Id_Work_Center") & "' "
                            SQL = SQL & "order by Jenis_Biaya "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                        For k As Integer = DynamicColumn To Dgv_Data.Columns.Count - 1

                                            Dim asdasd As String = Dgv_Data.Columns(k).Name

                                            If Dgv_Data.Columns(k).Name = Ds2.Tables("MyTable").Rows(j).Item("Jenis_Biaya") Then
                                                Dgv_Data.Rows(row).Cells(k).Value = Format(Ds2.Tables("MyTable").Rows(j).Item("Biaya"), "N2")

                                                Dgv_Data.Rows(row).Cells(k).Style.BackColor = Color.LightYellow

                                            ElseIf Dgv_Data.Columns(k).Name = Ds2.Tables("MyTable").Rows(j).Item("Jenis_Biaya") & "-act" Then

                                                Dgv_Data.Rows(row).Cells(k).Style.BackColor = Color.LightGreen

                                                SQL = "select sum(ISNULL(b.total, 0)) as Biaya_Actual "
                                                SQL = SQL & "from emi_transaksi_hpp a, EMI_Transaksi_HPP_Work_Center b "
                                                SQL = SQL & "where a.No_Transaksi = b.No_Transaksi "
                                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and a.Status is null "
                                                SQL = SQL & "and a.id_routing = '" & .Rows(i).Item("Id_Routing") & "' "
                                                SQL = SQL & "and b.Id_Work_Center = '" & .Rows(i).Item("Id_Work_Center") & "'  "
                                                SQL = SQL & "and a.tanggal = '" & FormatSelectedTanggal & "' "
                                                SQL = SQL & "and b.Jenis_Biaya = '" & Ds2.Tables("MyTable").Rows(j).Item("Jenis_Biaya") & "' "
                                                SQL = SQL & "group by a.tanggal "
                                                Using Dr = OpenTrans(SQL)
                                                    If Dr.Read Then
                                                        Dgv_Data.Rows(row).Cells(k).Value = Format(Dr("Biaya_Actual"), "N2")
                                                    Else
                                                        Dr.Close()
                                                        Dgv_Data.Rows(row).Cells(k).Value = Format(0, "N2")
                                                    End If
                                                End Using

                                                Exit For

                                            End If

                                        Next

                                    Next
                                Else
                                    For k As Integer = DynamicColumn To Dgv_Data.Columns.Count - 1
                                        Dgv_Data.Rows(row).Cells(k).Value = Format(0, "N2")

                                        If (k Mod 2) = 0 Then
                                            Dgv_Data.Rows(row).Cells(k).Style.BackColor = Color.LightYellow
                                        Else
                                            Dgv_Data.Rows(row).Cells(k).Style.BackColor = Color.LightGreen
                                        End If
                                    Next
                                End If
                            End Using

                            row += 1
                        Next

                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            DateTimePicker1.Enabled = False
            DateTimePicker1.Value = Date.Now

            'Button1_Click(CheckBox3, e)ss
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = False
            DateTimePicker1.Enabled = True
        Else
            DateTimePicker1.Enabled = False
            DateTimePicker1.Value = Date.Now
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then

            Cmb_ParamLain.Enabled = True
            TextBox4.Enabled = True

            Cmb_ParamLain.Text = ""
            Cmb_ParamLain.SelectedIndex = -1
            TextBox4.Text = ""
        Else
            Cmb_ParamLain.Enabled = False
            TextBox4.Enabled = False

            Cmb_ParamLain.Text = ""
            Cmb_ParamLain.SelectedIndex = -1
            TextBox4.Text = ""
        End If
    End Sub

End Class
Public Class Laporan_Summary_Usage_RM

    Dim arrPO, arrTgl, arrLain, arrBarang As New ArrayList


    Private Sub Laporan_Summary_Usage_RM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            Lv_Barang.Columns.Clear()
            Lv_Barang.Columns.Add("Kode Barnag", 150, HorizontalAlignment.Center)
            Lv_Barang.View = View.Details

            Lv_Barang.Items.Clear() : arrBarang.Clear()
            Lv_Barang.Location = New Point(721, 134)


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Then Exit Sub







    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Items.Clear()
            Lv_Barang.Location = New Point(721, 134)
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(128, 134)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear() : arrBarang.Clear()
            SQL = "select Kode_Barang from barang where Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Lv_Barang.Items.Add("--- SELEURUH ---") : arrBarang.Add("Seluruh")
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_Barang.Items.Add(Dr("Kode_Barang")) : arrBarang.Add("Kode_Barang")
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        'If CheckBox1.Checked = False And CheckBox2.Checked = False Then
        '    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Base_Language.Lang_Global_Perhatian)
        '    CheckBox1.Focus() : Exit Sub
        'End If

        If CheckBox1.Checked Then
            If ComboBox3.SelectedIndex = -1 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Base_Language.Lang_Global_Perhatian)
                ComboBox3.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl2, Base_Language.Lang_Global_Perhatian)
                DateTimePicker1.Value = Now : DateTimePicker2.Value = Now
                Exit Sub
            End If

            If CheckBox2.Checked Then
                If ComboBox2.SelectedIndex = -1 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain, Base_Language.Lang_Global_Perhatian)
                    ComboBox2.Focus() : Exit Sub
                ElseIf TextBox4.Text.Trim.Length = 0 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Base_Language.Lang_Global_Perhatian)
                    TextBox4.Focus() : Exit Sub
                End If
            End If
        End If

        Try
            OpenConn()

            Dim Filter As String = ""
            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select kode_perusahaan from Vw_Laporan_Hasil_Production_Summary where kode_perusahaan = '" & KodePerusahaan & "' "
            Filter = "{Vw_Laporan_Hasil_Production_Summary.kode_perusahaan} = '" & KodePerusahaan & "' "

            If Cmb_PO.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "No_PO = '" & arrPO.Item(Cmb_PO.SelectedIndex) & "' "

                Filter = Filter & "and {Vw_Laporan_Hasil_Production_Summary.No_PO} = '" & arrPO.Item(Cmb_PO.SelectedIndex) & "' "
            End If

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrTgl.Item(ComboBox3.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "

                Filter = Filter & "and {Vw_Laporan_Hasil_Production_Summary." & arrTgl.Item(ComboBox3.SelectedIndex) & "}  >= #" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "# and "
                Filter = Filter & "{Vw_Laporan_Hasil_Production_Summary." & arrTgl.Item(ComboBox3.SelectedIndex) & "}  <= #" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "#"
            End If

            If CheckBox2.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrLain.Item(ComboBox2.SelectedIndex) & " = '" & Trim(TextBox4.Text) & "' "

                Filter = Filter & "and {Vw_Laporan_Hasil_Production_Summary." & arrLain.Item(ComboBox2.SelectedIndex) & "} = '" & Trim(TextBox4.Text) & "' "
            End If

            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New Laporan_RM_Usage_Summary
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = Filter
                        CrDoc.SummaryInfo.ReportTitle = "Laporan Summary Usage RM"
                        .Text = "Laporan Summary Usage RM"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With
                Else
                    CloseConn()
                    MessageBox.Show("Ada Masalah Pada Report", "Laporan Usage Raw Material", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try











    End Sub





    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub
End Class
Public Class Laporan_Summary_Usage_RM

    Dim arrJenis, arrBarang As New ArrayList



    Private Sub Laporan_Summary_Usage_RM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            Lv_Barang.Columns.Clear()
            Lv_Barang.Columns.Add("Kode Barnag", 300, HorizontalAlignment.Center)
            Lv_Barang.View = View.Details

            Lv_Barang.Items.Clear() : arrBarang.Clear()
            Lv_Barang.Location = New Point(721, 134)

            CmbJenis.Items.Clear() : arrJenis.Clear()
            SQL = "select Kode_Group_Jenis from EMI_Group_Jenis where Kode_Perusahaan = '" & KodePerusahaan & "' and (Flag_Raw_Material = 'Y' or Flag_Packaging = 'Y')"
            Using Dr = OpenTrans(SQL)
                CmbJenis.Items.Add("--- SELURUH ---") : arrJenis.Add("Seluruh")
                Do While Dr.Read
                    CmbJenis.Items.Add(Dr("Kode_Group_Jenis")) : arrJenis.Add(Dr("Kode_Group_Jenis"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Then Exit Sub

        If Lv_Barang.FocusedItem.Index <> -1 Then
            Dim index As Integer = Lv_Barang.FocusedItem.Index

            Txt_KdBarang.Text = Lv_Barang.Items(index).SubItems(0).Text
            Lv_Barang.Items.Clear() : arrBarang.Clear()
            Lv_Barang.Location = New Point(721, 134)
            Lv_Barang.Visible = False
            Txt_KdBarang.Focus()

        End If

    End Sub


    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Items.Clear()
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(721, 134)
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear() : arrBarang.Clear()
            SQL = "select Kode_Barang from Emi_Split_Production_Order_Detail_Bahan "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang like '" & Txt_KdBarang.Text & "%' "
            SQL = SQL & "group by Kode_Barang"
            Using Dr = OpenTrans(SQL)
                Lv_Barang.Items.Add("--- SELURUH ---") : arrBarang.Add("Seluruh")
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_Barang.Items.Add(Dr("Kode_Barang")) : arrBarang.Add("Kode_Barang")
                Loop
            End Using

            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(128, 134)

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


        Try
            OpenConn()

            Dim Filter As String = ""
            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select kode_perusahaan from Vw_Laporan_Hasil_Production_Summary where kode_perusahaan = '" & KodePerusahaan & "' "
            Filter = "{Vw_Laporan_Hasil_Production_Summary.kode_perusahaan} = '" & KodePerusahaan & "' "

            SQL = SQL & "and tgl_produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "'"
            Filter = Filter & "and {Vw_Laporan_Hasil_Production_Summary.tgl_produksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            Filter = Filter & "{Vw_Laporan_Hasil_Production_Summary.tgl_produksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Txt_KdBarang.Text.Trim.Length <> 0 Then
                If Not Txt_KdBarang.Text = "--- SELURUH ---" Then
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & "kode_barang = '" & Txt_KdBarang.Text & "' "

                    Filter = Filter & "and {Vw_Laporan_Hasil_Production_Summary.kode_barang} = '" & Txt_KdBarang.Text & "' "
                End If
            End If

            If CmbJenis.SelectedIndex <> -1 Then
                If Not CmbJenis.SelectedItem = "--- SELURUH ---" Or Not CmbJenis.SelectedIndex = 0 Then
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & "Kode_Group_Jenis = '" & arrJenis(CmbJenis.SelectedIndex) & "' "

                    Filter = Filter & "and {Vw_Laporan_Hasil_Production_Summary.Kode_Group_Jenis} = '" & arrJenis(CmbJenis.SelectedIndex) & "' "
                End If
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
                    MessageBox.Show("Data tidak Ditemukan", "Laporan Usage Raw Material", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
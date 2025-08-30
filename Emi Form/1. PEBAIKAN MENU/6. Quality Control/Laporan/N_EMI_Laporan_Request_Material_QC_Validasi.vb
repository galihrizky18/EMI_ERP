Public Class N_EMI_Laporan_Request_Material_QC_Validasi

    Dim JudulForm As String = "Laporan Request Material QC Validasi"

    Dim arrTangal As New ArrayList


    Private Sub N_EMI_Laporan_Request_Material_QC_Validasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")





        Lv_Split.Columns.Clear()
        Lv_Split.Columns.Add("No Split", 130, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("Keterangan", 380, HorizontalAlignment.Left)
        Lv_Split.View = View.Details

        Lv_Request.Columns.Clear()
        Lv_Request.Columns.Add("No Request", 130, HorizontalAlignment.Left)
        Lv_Request.Columns.Add("Keterangan", 380, HorizontalAlignment.Left)
        Lv_Request.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("BArang", 380, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details



        Kosong()
    End Sub


    Private Sub Kosong()


        Tgl1.Value = DateTime.Today
        Tgl2.Value = DateTime.Today


        Txt_NoSplit.Text = OpsiSeluruh : Txt_KetSplit.Text = OpsiSeluruh
        Txt_NoRequest.Text = OpsiSeluruh : Txt_KetRequest.Text = OpsiSeluruh
        Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh

        Lv_Split.Visible = False : Lv_Request.Visible = False : Lv_Barang.Visible = False

        Me.Size = New Size(687, 280)



    End Sub



    '==========================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '==========================================================================================================================================
    Private Sub Txt_NoSplit_TextChanged(sender As Object, e As EventArgs) Handles Txt_NoSplit.TextChanged
        If Txt_NoSplit.Text.Trim.Length = 0 Then
            Me.Size = New Size(687, 280)
            Lv_Split.Location = New Point(700, 129)
            Lv_Split.Visible = False
            Txt_NoSplit.Text = ""
            Txt_KetSplit.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(687, 380)
            Lv_Split.Visible = True
            Lv_Split.Location = New Point(111, 129)
        End If

        Try
            OpenConn()

            Lv_Split.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Split.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select no_transaksi, b.Keterangan "
            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Status is null and b.Flag_Release = 'Y' and a.Flag_Produksi ='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi like '%" & Txt_NoSplit.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Split.Items.Add(Dr("no_transaksi"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_NoSplit_Leave(sender As Object, e As EventArgs) Handles Txt_NoSplit.Leave
        If Txt_NoSplit.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Split.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_NoSplit.Text = OpsiSeluruh Then


                SQL = "select no_transaksi, b.Keterangan "
                SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.No_PO = b.No_Faktur "
                SQL = SQL & "and a.Status is null and b.Status is null and b.Flag_Release = 'Y' and a.Flag_Produksi ='Y' "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Transaksi = '" & Txt_NoSplit.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_NoSplit.Text = Dr("no_transaksi")
                        Txt_KetSplit.Text = Dr("Keterangan")
                        Txt_NoRequest.Focus()
                    Else
                        MessageBox.Show("No Split tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_NoSplit.Text = ""
                        Txt_KetSplit.Text = ""
                        Txt_NoSplit.Focus()
                    End If

                    Me.Size = New Size(687, 280)
                    Lv_Split.Location = New Point(700, 129)
                    Lv_Split.Visible = False
                End Using
            Else
                Txt_NoRequest.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_NoRequest_TextChanged(sender As Object, e As EventArgs) Handles Txt_NoRequest.TextChanged
        If Txt_NoRequest.Text.Trim.Length = 0 Then
            Me.Size = New Size(687, 280)
            Lv_Request.Location = New Point(700, 155)
            Lv_Request.Visible = False
            Txt_NoRequest.Text = ""
            Txt_KetSplit.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(687, 410)
            Lv_Request.Visible = True
            Lv_Request.Location = New Point(111, 155)
        End If

        Try
            OpenConn()

            Lv_Request.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Request.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select No_Faktur, Keterangan "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Faktur like '%" & Txt_NoRequest.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Request.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_NoRequest_Leave(sender As Object, e As EventArgs) Handles Txt_NoRequest.Leave
        If Txt_NoRequest.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Request.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_NoRequest.Text = OpsiSeluruh Then


                SQL = "select No_Faktur, Keterangan "
                SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Faktur = '" & Txt_NoRequest.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_NoRequest.Text = Dr("No_Faktur")
                        Txt_KetRequest.Text = Dr("Keterangan")
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("No Request tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_NoRequest.Text = ""
                        Txt_KetRequest.Text = ""
                        Txt_NoRequest.Focus()
                    End If

                    Me.Size = New Size(687, 280)
                    Lv_Request.Location = New Point(700, 155)
                    Lv_Request.Visible = False
                End Using
            Else
                Txt_KdBarang.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(687, 280)
            Lv_Barang.Location = New Point(700, 180)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(687, 435)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(111, 180)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, EMI_Kategori_Gudang_PerLokasi b, Stock_Owner_Gudang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.Id_Kategori_Gudang = b.ID_Kategori_Gudang "
            SQL = SQL & "and b.Lokasi_Gudang = c.Kode_Stock_Owner "
            SQL = SQL & "and c.Flag_QC='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Barang like '%" & Txt_KdBarang.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
        If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdBarang.Text = OpsiSeluruh Then

                SQL = "select Distinct a.Kode_Barang, a.Nama "
                SQL = SQL & "from barang a, EMI_Kategori_Gudang_PerLokasi b, Stock_Owner_Gudang c "
                SQL = SQL & "where a.Kode_Perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan "
                SQL = SQL & "and a.Id_Kategori_Gudang = b.ID_Kategori_Gudang "
                SQL = SQL & "and b.Lokasi_Gudang = c.Kode_Stock_Owner "
                SQL = SQL & "and c.Flag_QC='Y' "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NmBarang.Text = Dr("Nama")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Me.Size = New Size(687, 280)
                    Lv_Barang.Location = New Point(700, 180)
                    Lv_Barang.Visible = False
                End Using
            Else
                BtnCetak.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(687, 280)
            Lv_Barang.Location = New Point(700, 180)
            Lv_Barang.Visible = False
            Txt_NmBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(687, 435)
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(111, 180)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, EMI_Kategori_Gudang_PerLokasi b, Stock_Owner_Gudang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.Id_Kategori_Gudang = b.ID_Kategori_Gudang "
            SQL = SQL & "and b.Lokasi_Gudang = c.Kode_Stock_Owner "
            SQL = SQL & "and c.Flag_QC='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Nama like '%" & Txt_NmBarang.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub






    '==========================================================================================================================================
    '=     HANDLE LIST VIEW
    '==========================================================================================================================================
    Private Sub Lv_Split_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Split.DoubleClick
        If Lv_Split.Items.Count = 0 Or Lv_Split.FocusedItem.Index = -1 Then Exit Sub

        Dim NoSplit As String = Lv_Split.FocusedItem.SubItems(0).Text
        Dim KetSplit As String = Lv_Split.FocusedItem.SubItems(1).Text

        Txt_NoSplit.Text = NoSplit
        Txt_KetSplit.Text = KetSplit

        Me.Size = New Size(687, 280)
        Lv_Split.Location = New Point(700, 129)
        Lv_Split.Visible = False

        Txt_NoRequest.Focus()
    End Sub
    Private Sub Lv_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Split.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Split_DoubleClick(Lv_Split, e)
        End If
    End Sub
    Private Sub Lv_Request_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Request.DoubleClick
        If Lv_Request.Items.Count = 0 Or Lv_Request.FocusedItem.Index = -1 Then Exit Sub

        Dim NoReq As String = Lv_Request.FocusedItem.SubItems(0).Text
        Dim KetReq As String = Lv_Request.FocusedItem.SubItems(1).Text

        Txt_NoRequest.Text = NoReq
        Txt_KetRequest.Text = KetReq

        Me.Size = New Size(687, 280)
        Lv_Request.Location = New Point(700, 155)
        Lv_Request.Visible = False

        Txt_KdBarang.Focus()
    End Sub
    Private Sub Lv_Request_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Request.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Request_DoubleClick(Lv_Request, e)
        End If
    End Sub
    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim Nmbarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = Nmbarang

        Me.Size = New Size(687, 280)
        Lv_Barang.Location = New Point(700, 180)
        Lv_Barang.Visible = False

        BtnCetak.Focus()
    End Sub


    '==========================================================================================================================================
    '=     HANDLE BUTTON
    '==========================================================================================================================================
    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_NoSplit.Text.Trim.Length = 0 Then
            MessageBox.Show("No Split harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoSplit.Focus() : Exit Sub
        ElseIf Txt_NoRequest.Text.Trim.Length = 0 Then
            MessageBox.Show("No Request harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoRequest.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select Kode_Perusahaan from N_EMI_View_ransaksi_Material_Requisition_QC "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{N_EMI_View_ransaksi_Material_Requisition_QC.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_View_ransaksi_Material_Requisition_QC.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{N_EMI_View_ransaksi_Material_Requisition_QC.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_NoSplit.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and No_Split = '" & Txt_NoSplit.Text & "' "
                SF = SF & "And {N_EMI_View_ransaksi_Material_Requisition_QC.No_Split} = '" & Txt_NoSplit.Text & "'"
            End If

            If Not Txt_NoRequest.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and No_Faktur = '" & Txt_NoRequest.Text & "' "
                SF = SF & "And {N_EMI_View_ransaksi_Material_Requisition_QC.No_Faktur} = '" & Txt_NoRequest.Text & "'"
            End If

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Kode_Bahan = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {N_EMI_View_ransaksi_Material_Requisition_QC.Kode_Bahan} = '" & Txt_KdBarang.Text & "' "
            End If
            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New N_EMI_CR_Transaksi_Request_Material_QC_Summary

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan Request Material Quality Control"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                    Else

                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

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
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    '==========================================================================================================================================
    '=     HANDLE KEY PRESS
    '==========================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NoSplit.Focus()
    End Sub

    Private Sub Txt_NoSplit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoSplit.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_NoSplit.Text.Trim.Length = 0 Then Txt_NoSplit.Focus()
            Txt_NoSplit_Leave(Txt_NoSplit, e)

            Me.Size = New Size(687, 380)
            Lv_Split.Location = New Point(700, 129)
            Lv_Split.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NoSplit_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NoSplit.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Split.Focus()
    End Sub

    Private Sub Txt_NoRequest_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoRequest.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_NoRequest.Text.Trim.Length = 0 Then Txt_NoRequest.Focus()
            Txt_NoRequest_Leave(Txt_NoRequest, e)

            Me.Size = New Size(687, 380)
            Lv_Request.Location = New Point(700, 155)
            Lv_Request.Visible = False

        End If
    End Sub

    Private Sub Txt_NoRequest_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NoRequest.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Request.Focus()
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Me.Size = New Size(687, 280)
            Lv_Barang.Location = New Point(700, 180)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Me.Size = New Size(687, 280)
            Lv_Barang.Location = New Point(700, 180)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub


End Class
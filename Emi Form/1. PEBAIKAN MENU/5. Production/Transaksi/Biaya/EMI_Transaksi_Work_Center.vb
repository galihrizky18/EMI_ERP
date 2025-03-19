Public Class EMI_Transaksi_Work_Center

    Dim JudulForm As String = "Transaksi Work Center"

    Dim arrIdJnsBiaya, arrKdJnsBiaya, arrSelectedRouting As New ArrayList

    Dim ColDinamis As Integer = 4

    Dim DgvRouting_IDRouting, DgvRouting_Keterangan, DgvRouting_CheckBox, DgvRouting_KdRouting, DgvRouting_PrefixCode, DgvRouting_IdJenisProduk As String
    Dim DgvWork_IDRouting, DgvWork_IDWorkCenter, DgvWork_Routing, DgvWork_Mesin As String

    Dim item_DgvRouting_IDRouting As Integer = 0
    Dim item_DgvRouting_Keterangan As Integer = 1
    Dim item_DgvRouting_CheckBox As Integer = 2
    Dim item_DgvRouting_KodeRouting As Integer = 3
    Dim item_DgvRouting_PrefixCode As Integer = 4
    Dim item_DgvRouting_IDJenisProduk As Integer = 5

    Dim item_DGVWork_IDRouting As Integer = 0
    Dim item_DGVWork_IDWorkCenter As Integer = 1
    Dim item_DGVWork_Routing As Integer = 2
    Dim item_DGVWork_Mesin As Integer = 3

    Private Sub EMI_Transaksi_Work_Center2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()

    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Kosong()
    End Sub

    Private Sub dgv_workcenter_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_workcenter.CellEndEdit

        If dgv_workcenter.Rows.Count = 0 Then Exit Sub

        If Not IsNumeric(dgv_workcenter.CurrentCell.Value) Then
            dgv_workcenter.CurrentCell.Value = 0
        End If

    End Sub

    Private Sub Btn_release_Click(sender As Object, e As EventArgs)

        If Txt_NoFaktur.Text.Trim.Length = 0 Or dgv_workcenter.Rows.Count = 0 Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '===============================
            '=     CEK APAKAH ADA DATA     =
            '===============================
            SQL = "select No_Faktur from Emi_Transaksi_Work_Center where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "update Emi_Transaksi_Work_Center set Flag_Release = 'Y' where No_Faktur = '" & .Rows(i).Item("No_Faktur") & "'"
                            ExecuteTrans(SQL)

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Belum Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil di release", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Kosong()
            Exit Sub
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Kosong()
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Set_Click(sender As Object, e As EventArgs) Handles Btn_Set.Click
        If Cmb_JenisBiaya.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Jenis Biaya", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_JenisBiaya.Focus() : Exit Sub
        End If


        arrSelectedRouting.Clear()
        Kosong_DGVWorkCenter()
        dgv_workcenter.Rows.Clear()

        For i As Integer = 0 To dgv_routing.RowCount - 1
            Get_Data_DGVRouting(i)

            If DgvRouting_CheckBox = "True" Then
                arrSelectedRouting.Add(DgvRouting_IDRouting)
            End If

        Next

        dgv_workcenter.Enabled = True
        LoadData()
    End Sub

    Private Sub Kosong()

        get_jam()

        Try
            OpenConn()
            get_no_faktur()

            dgv_routing.Rows.Clear()
            dgv_workcenter.Rows.Clear()

            arrSelectedRouting.Clear()

            BtnSimpan.Enabled = True

            Cmbsatuan.Items.Clear()
            SQL = "select Satuan from EMI_Satuan where kode_perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "order by Satuan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmbsatuan.Items.Add(dr("Satuan"))
                Loop
            End Using
            Cmbsatuan.Text = "KG"
            Cmbsatuan.Enabled = False
            '======================
            '=     GET LOKASI     =
            '======================
            CmbLokasi.Items.Clear()
            SQL = "select Kode_Stock_Owner from Stock_Owner "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        CmbLokasi.Items.Add(.Rows(i).Item("Kode_Stock_Owner"))
                    Next
                End With
            End Using
            CmbLokasi.Text = Lokasi

            '===========================
            '=     GET JENIS BIAYA     =
            '===========================
            Cmb_JenisBiaya.Items.Clear() : arrKdJnsBiaya.Clear() : arrIdJnsBiaya.Clear()
            SQL = "select Id_Jenis_Biaya_Produksi, Kode_Jenis_Biaya_Produksi, keterangan from Emi_Jenis_Biaya_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Cmb_JenisBiaya.Items.Add(Dr("keterangan")) : arrIdJnsBiaya.Add(Dr("Id_Jenis_Biaya_Produksi")) : arrKdJnsBiaya.Add(Dr("Kode_Jenis_Biaya_Produksi"))
                Loop
            End Using



            Get_Data_Routing()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Private Sub Kosong_DGVWorkCenter()
        '=================================================
        '=     MENGHAPUS KOLOM MULAI DARI INDEX KE 4     =
        '=================================================
        For i As Integer = dgv_workcenter.Columns.Count - 1 To ColDinamis Step -1
            dgv_workcenter.Columns.RemoveAt(i)
        Next
    End Sub

    Private Sub Get_Data_DGVRouting(ByVal index As Integer)

        DgvRouting_IDRouting = dgv_routing.Rows(index).Cells(item_DgvRouting_IDRouting).Value
        DgvRouting_Keterangan = dgv_routing.Rows(index).Cells(item_DgvRouting_Keterangan).Value
        DgvRouting_CheckBox = dgv_routing.Rows(index).Cells(item_DgvRouting_CheckBox).Value
        DgvRouting_KdRouting = dgv_routing.Rows(index).Cells(item_DgvRouting_KodeRouting).Value
        DgvRouting_PrefixCode = dgv_routing.Rows(index).Cells(item_DgvRouting_PrefixCode).Value
        DgvRouting_IdJenisProduk = dgv_routing.Rows(index).Cells(item_DgvRouting_IDJenisProduk).Value

    End Sub

    Private Sub Get_data_DGVWorkCenter(ByVal index As Integer)

        DgvWork_IDRouting = dgv_workcenter.Rows(index).Cells(item_DGVWork_IDRouting).Value
        DgvWork_IDWorkCenter = dgv_workcenter.Rows(index).Cells(item_DGVWork_IDWorkCenter).Value
        DgvWork_Routing = dgv_workcenter.Rows(index).Cells(item_DGVWork_Routing).Value
        DgvWork_Mesin = dgv_workcenter.Rows(index).Cells(item_DGVWork_Mesin).Value

    End Sub

    Private Sub get_no_faktur()
        Dim FPro_Results As String = "TCC"
        Txt_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Transaksi_Work_Center", "No_Faktur", 5,
                                          "Kode_perusahaan", KodePerusahaan, "And",
                                          "substring(No_Faktur, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))
    End Sub
    Private Sub Get_Data_Routing()

        Dim row As Integer = 0
        SQL = "select Id_Routing, Keterangan, Kode_Routing, prefix_code, Id_Jenis_Produk from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "'"
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                dgv_routing.Rows.Add(1)
                dgv_routing.Rows(row).Cells(item_DgvRouting_IDRouting).Value = Dr("Id_Routing")
                dgv_routing.Rows(row).Cells(item_DgvRouting_Keterangan).Value = Dr("Keterangan")
                dgv_routing.Rows(row).Cells(item_DgvRouting_KodeRouting).Value = Dr("Kode_Routing")
                dgv_routing.Rows(row).Cells(item_DgvRouting_PrefixCode).Value = Dr("prefix_code")
                dgv_routing.Rows(row).Cells(item_DgvRouting_IDJenisProduk).Value = Dr("Id_Jenis_Produk")

                dgv_routing.Rows(row).Cells(item_DgvRouting_CheckBox).Value = True

                row += 1
            Loop
        End Using

    End Sub

    Private Sub dgv_routing_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_routing.CellEndEdit
        If dgv_routing.RowCount = 0 Then Exit Sub

        arrSelectedRouting.Clear()
        Kosong_DGVWorkCenter()
        dgv_workcenter.Rows.Clear()

        For i As Integer = 0 To dgv_routing.RowCount - 1
            Get_Data_DGVRouting(i)

            If DgvRouting_CheckBox = "True" Then
                arrSelectedRouting.Add(DgvRouting_IDRouting)
            End If

        Next

    End Sub

    Private Sub LoadData()
        If arrSelectedRouting.Count = 0 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            'Reset Kolom
            Kosong_DGVWorkCenter()
            dgv_workcenter.Rows.Clear()



            '=========================================
            '=     GET JENIS BIAYA (ADD COLUMN)      =
            '=========================================
            Dim ColNum As Integer = ColDinamis
            SQL = "Select kode_jenis_biaya_produksi, Keterangan from emi_jenis_biaya_produksi where kode_perusahaan = '" & KodePerusahaan & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    dgv_workcenter.Columns.Add(dr("kode_jenis_biaya_produksi"), dr("kode_jenis_biaya_produksi"))
                    dgv_workcenter.Columns(ColNum).Width = 130
                    dgv_workcenter.Columns(ColNum).ReadOnly = False
                    dgv_workcenter.Columns(ColNum).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    ColNum += 1
                Loop
            End Using

            '========================================================================================================================================================================================

            Dim formatIdRouting As String = "'" & String.Join("', '", arrSelectedRouting.ToArray()) & "'"

            For a As Integer = 0 To arrSelectedRouting.Count - 1

                '=======================================
                '=     GET DATA MESIN PER ROUTING      =
                '=======================================

                Dim row As Integer = dgv_workcenter.Rows.Count
                SQL = "select a.Id_Routing, b.Id_Work_Center, a.Keterangan as Routing, c.Keterangan as Mesin "
                SQL = SQL & "from emi_master_routing a, emi_master_routing_detail b, EMI_Master_Work_Center c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.Id_Routing = b.Id_Routing "
                SQL = SQL & "and b.Id_Work_Center = c.Id_Work_Center "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Id_Routing = '" & arrSelectedRouting(a) & "' "
                SQL = SQL & "order by b.Id_Routing, b.Id_Work_Center"
                Using Ds2 = BindingTrans(SQL)
                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                        For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                            dgv_workcenter.Rows.Add(1)
                            dgv_workcenter.Rows(row).Cells(item_DGVWork_IDRouting).Value = Ds2.Tables("MyTable").Rows(j).Item("Id_Routing")
                            dgv_workcenter.Rows(row).Cells(item_DGVWork_IDWorkCenter).Value = Ds2.Tables("MyTable").Rows(j).Item("Id_Work_Center")
                            dgv_workcenter.Rows(row).Cells(item_DGVWork_Routing).Value = Ds2.Tables("MyTable").Rows(j).Item("Routing")
                            dgv_workcenter.Rows(row).Cells(item_DGVWork_Mesin).Value = Ds2.Tables("MyTable").Rows(j).Item("Mesin")

                            For k As Integer = ColDinamis To dgv_workcenter.Columns.Count - 1
                                dgv_workcenter.Rows(row).Cells(k).Value = "0"

                                dgv_workcenter.Rows(row).Cells(k).ReadOnly = False
                            Next

                            row += 1
                        Next
                    End If
                End Using

#Region "KODE LAMA"

                'SQL = "select a.No_Faktur, b.Id_Routing, b.Id_Work_Center, d.Keterangan as Routing, c.Keterangan as Mesin "
                'SQL = SQL & "from Emi_Transaksi_Work_Center a, Emi_Transaksi_Work_Center_Detail b, EMI_Master_Work_Center c, emi_master_routing d, emi_master_routing_detail e "
                'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan "
                'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                'SQL = SQL & "and b.Id_Work_Center = c.Id_Work_Center "
                'SQL = SQL & "and b.Id_Routing = d.Id_Routing "
                'SQL = SQL & "and d.Id_Routing = e.Id_Routing "
                'SQL = SQL & "and b.Id_Work_Center = e.Id_Work_Center "
                'SQL = SQL & "and b.Id_Routing = '" & arrSelectedRouting(a) & "'  "
                'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.status is null "
                'SQL = SQL & "order by b.Id_Routing, b.Id_Work_Center "
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then

                '            Dim row As Integer = dgv_workcenter.Rows.Count

                '            For i As Integer = 0 To .Rows.Count - 1

                '                dgv_workcenter.Rows.Add(1)
                '                dgv_workcenter.Rows(row).Cells(item_DGVWork_IDRouting).Value = .Rows(i).Item("Id_Routing")
                '                dgv_workcenter.Rows(row).Cells(item_DGVWork_IDWorkCenter).Value = .Rows(i).Item("Id_Work_Center")
                '                dgv_workcenter.Rows(row).Cells(item_DGVWork_Routing).Value = .Rows(i).Item("Routing")
                '                dgv_workcenter.Rows(row).Cells(item_DGVWork_Mesin).Value = .Rows(i).Item("Mesin")

                '                '======================================
                '                '=     GET DATA DETAIL PER-MESIN      =
                '                '======================================
                '                SQL = "select b.Jenis_Biaya, b.Total "
                '                SQL = SQL & "from Emi_Transaksi_Work_Center a, Emi_Transaksi_Work_Center_Detail_Per_Mesin b "
                '                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                '                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                '                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                '                SQL = SQL & "and b.Id_Routing = '" & .Rows(i).Item("Id_Routing") & "' "
                '                SQL = SQL & "and b.Id_Work_Center = '" & .Rows(i).Item("Id_Work_Center") & "' and a.status is null "
                '                SQL = SQL & "order by b.Id_Routing, b.Id_Work_Center"
                '                Using Ds1 = BindingTrans(SQL)
                '                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                '                        For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                '                            For k As Integer = ColDinamis To dgv_workcenter.Columns.Count - 1
                '                                If Ds1.Tables("MyTable").Rows(j).Item("Jenis_Biaya") = dgv_workcenter.Columns(k).HeaderText Then
                '                                    dgv_workcenter.Rows(row).Cells(k).Value = Ds1.Tables("MyTable").Rows(j).Item("Total")

                '                                    If isDataRelease Then
                '                                        dgv_workcenter.Rows(row).Cells(k).ReadOnly = True
                '                                    Else
                '                                        dgv_workcenter.Rows(row).Cells(k).ReadOnly = False
                '                                    End If
                '                                    Exit For
                '                                End If
                '                            Next

                '                        Next
                '                    Else

                '                    End If
                '                End Using

                '                row += 1
                '            Next
                '        Else

                '            Dim row As Integer = dgv_workcenter.Rows.Count
                '            SQL = "select a.Id_Routing, b.Id_Work_Center, a.Keterangan as Routing, c.Keterangan as Mesin "
                '            SQL = SQL & "from emi_master_routing a, emi_master_routing_detail b, EMI_Master_Work_Center c "
                '            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                '            SQL = SQL & "and a.Id_Routing = b.Id_Routing "
                '            SQL = SQL & "and b.Id_Work_Center = c.Id_Work_Center "
                '            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                '            SQL = SQL & "and a.Id_Routing = '" & arrSelectedRouting(a) & "' "
                '            SQL = SQL & "order by b.Id_Routing, b.Id_Work_Center"
                '            Using Ds2 = BindingTrans(SQL)
                '                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                '                    For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                '                        dgv_workcenter.Rows.Add(1)
                '                        dgv_workcenter.Rows(row).Cells(item_DGVWork_IDRouting).Value = Ds2.Tables("MyTable").Rows(j).Item("Id_Routing")
                '                        dgv_workcenter.Rows(row).Cells(item_DGVWork_IDWorkCenter).Value = Ds2.Tables("MyTable").Rows(j).Item("Id_Work_Center")
                '                        dgv_workcenter.Rows(row).Cells(item_DGVWork_Routing).Value = Ds2.Tables("MyTable").Rows(j).Item("Routing")
                '                        dgv_workcenter.Rows(row).Cells(item_DGVWork_Mesin).Value = Ds2.Tables("MyTable").Rows(j).Item("Mesin")

                '                        For k As Integer = ColDinamis To dgv_workcenter.Columns.Count - 1
                '                            dgv_workcenter.Rows(row).Cells(k).Value = "0"
                '                            If isDataRelease Then
                '                                dgv_workcenter.Rows(row).Cells(k).ReadOnly = True
                '                            Else
                '                                dgv_workcenter.Rows(row).Cells(k).ReadOnly = False
                '                            End If
                '                        Next

                '                        row += 1
                '                    Next
                '                End If
                '            End Using

                '        End If
                '    End With
                'End Using

#End Region

            Next

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        get_jam()
        If Txt_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No transaksi Harus diisi....!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoFaktur.Focus() : Exit Sub
        ElseIf Cmb_JenisBiaya.Text.Trim.Length = 0 Then
            MessageBox.Show("Bulan Harus diisi....!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_JenisBiaya.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction
            get_no_faktur()


            '=======================
            '=     INSERT DATA     =
            '=======================
            SQL = "INSERT INTO Emi_Transaksi_Work_Center(Kode_Perusahaan, No_Faktur, UserID, Tanggal, Jam, Jenis_Biaya) VALUES("
            SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "', "
            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "', '" & arrKdJnsBiaya(Cmb_JenisBiaya.SelectedIndex) & "') "
            ExecuteTrans(SQL)

            'Emi_Transaksi_Work_Center_detail
            For i As Integer = 0 To dgv_workcenter.Rows.Count - 1
                Dim Total As Double = 0
                For j As Integer = ColDinamis To dgv_workcenter.Columns.Count - 1
                    Total += dgv_workcenter.Rows(i).Cells(j).Value
                Next

                Get_data_DGVWorkCenter(i)

                SQL = "INSERT INTO Emi_Transaksi_Work_Center_Detail (kode_perusahaan, no_faktur, id_routing, id_work_center, nilai_per_pcs)"
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', "
                SQL = SQL & "'" & DgvWork_IDRouting & "', '" & DgvWork_IDWorkCenter & "','" & Total & "')"
                ExecuteTrans(SQL)
            Next

            'Emi_Transaksi_Work_Center_Detail_Per_Mesin
            For i As Integer = 0 To dgv_workcenter.Rows.Count - 1

                Get_data_DGVWorkCenter(i)

                For j As Integer = ColDinamis To dgv_workcenter.Columns.Count - 1
                    SQL = "INSERT INTO Emi_Transaksi_Work_Center_Detail_Per_Mesin(Kode_Perusahaan, No_Faktur, id_routing, Id_Work_Center, Jenis_Biaya, Total, Nilai_Per_Pcs) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & DgvWork_IDRouting & "', '" & DgvWork_IDWorkCenter & "', "
                    SQL = SQL & "'" & dgv_workcenter.Columns(j).HeaderText & "', '" & dgv_workcenter.Rows(i).Cells(j).Value & "', '" & dgv_workcenter.Rows(i).Cells(j).Value & "')"
                    ExecuteTrans(SQL)
                Next
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            For i As Integer = 0 To dgv_routing.Rows.Count - 1
                Get_Data_DGVRouting(i)

                dgv_routing.Rows(i).Cells(item_DgvRouting_CheckBox).Value = True
                arrSelectedRouting.Add(DgvRouting_IDRouting)
            Next
        Else

            arrSelectedRouting.Clear()
            For i As Integer = 0 To dgv_routing.Rows.Count - 1
                dgv_routing.Rows(i).Cells(item_DgvRouting_CheckBox).Value = False
            Next
            Kosong_DGVWorkCenter()
            dgv_workcenter.Rows.Clear()
        End If
    End Sub

End Class
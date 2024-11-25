Public Class Transfer_Quality_Stock2

    Dim arrSO As New ArrayList
    Dim kategoriQuality As New ArrayList({"Good Stock", "Warning Stock", "Bad Stock"})

    Dim lv_SO, lv_KdBarang, lv_Nama, lv_GoodStock As String
    Dim dgv_KodeBarang, dgv_Lokasi, dgv_NamaBarang, dgv_Posisi, dgv_GoodStock, dgv_WarningStock, dgv_BadStock, dgv_Satuan, dgv_SatuanBarang, dgv_ChkBox, dgv_JmlhTf, dgv_SerialNumber As String

    Dim itemSO As Integer = 0
    Dim itemKdBarang As Integer = 1
    Dim itemNama As Integer = 2
    Dim itemGoodStock As Integer = 3

    Dim itemDgv_KodeBarang As Integer = 0
    Dim itemDgv_Lokasi As Integer = 1
    Dim itemDgv_SerialNumber As Integer = 2
    Dim itemDgv_NamaBarang As Integer = 3
    Dim itemDgv_Posisi As Integer = 4
    Dim itemDgv_GoodStock As Integer = 5
    Dim itemDgv_WarningStock As Integer = 6
    Dim itemDgv_BadStock As Integer = 7
    Dim itemDgv_Satuan As Integer = 8
    Dim itemDgv_SatuanBarang As Integer = 9
    Dim itemDgv_ChkBox As Integer = 10
    Dim itemDgv_JmlhTransfer As Integer = 11

    Private Sub Transfer_Quality_Stock2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        kosong()

    End Sub

    Private Sub kosong()

        Cmb_StockOwner.Items.Clear()
        Cmb_QualityFrom.Items.Clear()
        Cmb_QualityTo.Items.Clear()

        TxtKd_Barang.Text = String.Empty
        TxtSo.Text = String.Empty
        TxtNamaBarang.Text = String.Empty
        TxtGoodStock.Text = String.Empty

        Dgv_Stock.Rows.Clear()
        ListView1.Items.Clear()

        Initial_List_View()
        Load_ComboBox()

    End Sub

    Private Sub Initial_List_View()

        ListView1.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama", 200, HorizontalAlignment.Center)
        ListView1.Columns.Add("Good Stock", 150, HorizontalAlignment.Center)

        ListView1.View = View.Details
    End Sub

    Private Sub Get_Isi_ListView(ByVal index As Integer)

        lv_SO = ListView1.Items(index).SubItems(itemSO).Text
        lv_KdBarang = ListView1.Items(index).SubItems(itemKdBarang).Text
        lv_Nama = ListView1.Items(index).SubItems(itemNama).Text
        lv_GoodStock = ListView1.Items(index).SubItems(itemGoodStock).Text

    End Sub

    Private Sub Get_Dgv_Data(ByVal index As Integer)

        dgv_KodeBarang = Dgv_Stock.Rows(index).Cells(itemDgv_KodeBarang).Value
        dgv_Lokasi = Dgv_Stock.Rows(index).Cells(itemDgv_Lokasi).Value
        dgv_NamaBarang = Dgv_Stock.Rows(index).Cells(itemDgv_NamaBarang).Value
        dgv_Posisi = Dgv_Stock.Rows(index).Cells(itemDgv_Posisi).Value
        dgv_GoodStock = Dgv_Stock.Rows(index).Cells(itemDgv_GoodStock).Value
        dgv_WarningStock = Dgv_Stock.Rows(index).Cells(itemDgv_WarningStock).Value
        dgv_BadStock = Dgv_Stock.Rows(index).Cells(itemDgv_BadStock).Value
        dgv_Satuan = Dgv_Stock.Rows(index).Cells(itemDgv_Satuan).Value
        dgv_SatuanBarang = Dgv_Stock.Rows(index).Cells(itemDgv_SatuanBarang).Value
        dgv_ChkBox = Dgv_Stock.Rows(index).Cells(itemDgv_ChkBox).Value
        dgv_JmlhTf = Dgv_Stock.Rows(index).Cells(itemDgv_JmlhTransfer).Value
        dgv_SerialNumber = Dgv_Stock.Rows(index).Cells(itemDgv_SerialNumber).Value
    End Sub

    Private Sub Load_ComboBox()
        Try
            OpenConn()

            SQL = "select kode_stock_owner, keterangan from stock_owner_gudang "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' order by Kode_Stock_Owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_StockOwner.Items.Add(Dr("keterangan")) : arrSO.Add(Dr("kode_stock_owner"))
                Loop
            End Using

            For i As Integer = 0 To kategoriQuality.Count - 1
                Cmb_QualityFrom.Items.Add(kategoriQuality(i)) : Cmb_QualityTo.Items.Add(kategoriQuality(i))
            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged
        If TxtKd_Barang.Text.Trim.Length = 0 Then
            ListView1.Visible = False
            TxtSo.Text = String.Empty
            TxtNamaBarang.Text = String.Empty
            TxtGoodStock.Text = String.Empty
            Dgv_Stock.Rows.Clear()
            Exit Sub
        Else
            ListView1.Visible = True
        End If

        If Cmb_StockOwner.SelectedIndex = -1 Then ListView1.Visible = False : Exit Sub

        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1
        Dgv_Stock.Rows.Clear()

        Try
            OpenConn()

            ListView1.Items.Clear()

            SQL = "select Kode_Stock_Owner, Kode_Barang, Nama, Good_Stock "
            SQL = SQL & "from barang "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and nama like '" & TxtKd_Barang.Text & "%' "
            SQL = SQL & "and Kode_Stock_Owner='" & arrSO(Cmb_StockOwner.SelectedIndex) & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = ListView1.Items.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("Good_Stock"))
                Loop
            End Using


            ListView1.Location = New Point(26, 166)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub

        Dim focusIndex As Integer = ListView1.FocusedItem.Index
        Get_Isi_ListView(focusIndex)

        TxtKd_Barang.Text = lv_KdBarang
        TxtSo.Text = lv_SO
        TxtNamaBarang.Text = lv_Nama
        TxtGoodStock.Text = lv_GoodStock


        ListView1.Items.Clear()
        ListView1.Visible = False
        ListView1.Location = New Point(1190, 166)
    End Sub


    Private Sub Btn_GetData_Click(sender As Object, e As EventArgs) Handles Btn_GetData.Click

        If Dgv_Stock.RowCount > 0 Then Exit Sub

        Try
            OpenConn()

            Dgv_Stock.Rows.Clear()

            Dim rows As Integer = 0
            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Nama, b.serial_number ,b.Jumlah, b.Warning_Stock, b.Bad_Stock, d.Satuan as satuan_display, c.Keterangan as position, a.Satuan "
            SQL = SQL & "from barang a, barang_sn b, View_Warehouse_Position c, Barang_Detail_Satuan d "
            SQL = SQL & "where a.kode_perusahaan=b.Kode_Perusahaan "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan "
            SQL = SQL & "and a.kode_perusahaan=d.kode_perusahaan "
            SQL = SQL & "and a.Kode_Barang=d.Kode_barang "
            SQL = SQL & "and a.Kode_Stock_Owner=b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and b.Id_Warehouse=c.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and d.Flag_Tampil_Display='Y' "
            SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(Cmb_StockOwner.SelectedIndex) & "' and a.Kode_Barang='" & TxtKd_Barang.Text & "' "
            SQL = SQL & "group by  a.Kode_Stock_Owner, a.Kode_Barang, a.Nama, b.Jumlah, b.Warning_Stock, b.Bad_Stock, d.Satuan, c.Keterangan, a.Satuan, b.serial_number "
            SQL = SQL & "order by Kode_barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dgv_Stock.Rows.Add(1)
                    Dgv_Stock.Rows(rows).Cells(itemDgv_KodeBarang).Value = Dr("Kode_Barang")
                    Dgv_Stock.Rows(rows).Cells(itemDgv_Lokasi).Value = Dr("Kode_Stock_Owner")
                    Dgv_Stock.Rows(rows).Cells(itemDgv_SerialNumber).Value = Dr("serial_number")
                    Dgv_Stock.Rows(rows).Cells(itemDgv_NamaBarang).Value = Dr("Nama")
                    Dgv_Stock.Rows(rows).Cells(itemDgv_Posisi).Value = Dr("position")
                    Dgv_Stock.Rows(rows).Cells(itemDgv_GoodStock).Value = isnull(Dr("Jumlah"))
                    Dgv_Stock.Rows(rows).Cells(itemDgv_WarningStock).Value = isnull(Dr("Warning_Stock"))
                    Dgv_Stock.Rows(rows).Cells(itemDgv_BadStock).Value = isnull(Dr("bad_stock"))
                    'Dgv_Stock.Rows(rows).Cells(itemDgv_Satuan).Value = Dr("satuan_display")
                    Dgv_Stock.Rows(rows).Cells(itemDgv_Satuan).Value = Dr("Satuan")
                    Dgv_Stock.Rows(rows).Cells(itemDgv_SatuanBarang).Value = Dr("Satuan")

                    Dgv_Stock.Rows(rows).Cells(itemDgv_JmlhTransfer).Style.BackColor = Color.LightGray
                    Dgv_Stock.Rows(rows).Cells(itemDgv_JmlhTransfer).ReadOnly = True

                    rows = rows + 1
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Dgv_Stock.RowCount = 0 Then
            MessageBox.Show("Belum ada barang yang mau di transfer!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKd_Barang.Focus() : Exit Sub
        ElseIf Cmb_StockOwner.SelectedIndex = -1 Then
            MessageBox.Show("Stock Owner Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_StockOwner.Focus() : Exit Sub
        ElseIf Cmb_QualityFrom.SelectedIndex = -1 Then
            MessageBox.Show("Initial Quality Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_QualityFrom.Focus() : Exit Sub
        ElseIf Cmb_QualityTo.SelectedIndex = -1 Then
            MessageBox.Show("Quality After Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_QualityTo.Focus() : Exit Sub
        End If


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim isCheck As Boolean = False

            For i As Integer = 0 To Dgv_Stock.RowCount - 1
                Get_Dgv_Data(i)

                If dgv_ChkBox Is Nothing Then Continue For

                If String.IsNullOrWhiteSpace(dgv_JmlhTf) Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah Transfer Tidak Boleh Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                isCheck = True

                '======================
                '=      UPDATE        =
                '======================

                Dim jumlahTransfer As Integer = Val(dgv_JmlhTf)

                '====== UPDATE BARANG  ======

                SQL = "update barang set "

                If kategoriQuality(Cmb_QualityFrom.SelectedIndex) = "Good Stock" Then
                    SQL = SQL & "Good_Stock = Good_Stock - " & jumlahTransfer & ", "
                ElseIf kategoriQuality(Cmb_QualityFrom.SelectedIndex) = "Warning Stock" Then
                    SQL = SQL & "Warning_Stock = Warning_Stock - " & jumlahTransfer & ", "
                ElseIf kategoriQuality(Cmb_QualityFrom.SelectedIndex) = "Bad Stock" Then
                    SQL = SQL & "Bad_Stock = Bad_Stock - " & jumlahTransfer & ", "
                End If

                If kategoriQuality(Cmb_QualityTo.SelectedIndex) = "Good Stock" Then
                    SQL = SQL & "Good_Stock = Good_Stock + " & jumlahTransfer & " "
                ElseIf kategoriQuality(Cmb_QualityTo.SelectedIndex) = "Warning Stock" Then
                    SQL = SQL & "Warning_Stock = Warning_Stock + " & jumlahTransfer & " "
                ElseIf kategoriQuality(Cmb_QualityTo.SelectedIndex) = "Bad Stock" Then
                    SQL = SQL & "Bad_Stock = Bad_Stock + " & jumlahTransfer & " "
                End If

                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(Cmb_StockOwner.SelectedIndex) & "' and Kode_Barang='" & TxtKd_Barang.Text & "' "
                ExecuteTrans(SQL)


                '====== UPDATE BARANG SN ======

                SQL = "update barang_sn set "

                If kategoriQuality(Cmb_QualityFrom.SelectedIndex) = "Good Stock" Then
                    SQL = SQL & "jumlah = jumlah - " & jumlahTransfer & ", "
                ElseIf kategoriQuality(Cmb_QualityFrom.SelectedIndex) = "Warning Stock" Then
                    SQL = SQL & "warning_stock = warning_stock - " & jumlahTransfer & ", "
                ElseIf kategoriQuality(Cmb_QualityFrom.SelectedIndex) = "Bad Stock" Then
                    SQL = SQL & "bad_stock = bad_stock - " & jumlahTransfer & ", "
                End If

                If kategoriQuality(Cmb_QualityTo.SelectedIndex) = "Good Stock" Then
                    SQL = SQL & "jumlah = jumlah + " & jumlahTransfer & " "
                ElseIf kategoriQuality(Cmb_QualityTo.SelectedIndex) = "Warning Stock" Then
                    SQL = SQL & "warning_stock = warning_stock + " & jumlahTransfer & " "
                ElseIf kategoriQuality(Cmb_QualityTo.SelectedIndex) = "Bad Stock" Then
                    SQL = SQL & "bad_stock = bad_stock + " & jumlahTransfer & " "
                End If

                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(Cmb_StockOwner.SelectedIndex) & "' and Kode_Barang='" & TxtKd_Barang.Text & "' "
                SQL = SQL & "and Serial_Number='" & dgv_SerialNumber.ToString & "' "
                ExecuteTrans(SQL)

            Next


            '===========================
            '=      CEK CHECKLIST      =
            '===========================
            If isCheck <> True Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Tidak Ada Data yang Dikirim . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            '=================================
            '=      CEK KESESUAIAN DATA      =
            '=================================
            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang ,a.nama, "
            SQL = SQL & "ISNULL((round(sum(a.Good_Stock), 2)), 0) as Tot_GoodStock_Barang, "
            SQL = SQL & "ISNULL((round(sum(a.Warning_Stock), 2)), 0) as Tot_WarningStock_Barang, "
            SQL = SQL & "ISNULL((round(sum(a.Bad_Stock), 2)), 0) as Tot_BadStock_Barang, "
            SQL = SQL & "ISNULL((select round(sum(z.jumlah), 2) from barang_sn z where a.Kode_Perusahaan=z.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner=z.Kode_Stock_Owner and a.Kode_Barang=z.Kode_Barang), 0) as Tot_GoodStock_SN, "

            SQL = SQL & "ISNULL((select round(sum(z.Warning_Stock), 2) from barang_sn z where a.Kode_Perusahaan=z.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner=z.Kode_Stock_Owner and a.Kode_Barang=z.Kode_Barang), 0) as Tot_WarningStock_SN, "

            SQL = SQL & "ISNULL((select round(sum(z.Bad_Stock), 2) from barang_sn z where a.Kode_Perusahaan=z.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner=z.Kode_Stock_Owner and a.Kode_Barang=z.Kode_Barang), 0) as Tot_BadStock_SN "
            SQL = SQL & "from barang a "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(Cmb_StockOwner.SelectedIndex) & "' "
            SQL = SQL & "and a.Kode_Barang='" & TxtKd_Barang.Text & "' "
            SQL = SQL & "group by a.nama, a.Kode_Perusahaan, a.Kode_Barang, a.Kode_Stock_Owner "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        If .Rows(0).Item("Tot_GoodStock_Barang") <> .Rows(0).Item("Tot_GoodStock_SN") Or .Rows(0).Item("Tot_WarningStock_Barang") <> .Rows(0).Item("Tot_WarningStock_SN") _
                            Or .Rows(0).Item("Tot_BadStock_Barang") <> .Rows(0).Item("Tot_BadStock_SN") Then

                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using



            Cmd.Transaction.Commit()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub




    Private Sub Dgv_Stock_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Stock.CellEndEdit
        If Cmb_QualityFrom.SelectedIndex = -1 Then
            MessageBox.Show("Initial Quality Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_QualityFrom.Focus()
            Dgv_Stock.CurrentRow.Cells(itemDgv_ChkBox).Value = False : Exit Sub
        ElseIf Cmb_QualityTo.SelectedIndex = -1 Then
            MessageBox.Show("Quality After Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_QualityTo.Focus()
            Dgv_Stock.CurrentRow.Cells(itemDgv_ChkBox).Value = False : Exit Sub
        End If

        Dim cellGoodStock As String = Dgv_Stock.CurrentRow.Cells(itemDgv_GoodStock).Value
        Dim cellWarningStock As String = Dgv_Stock.CurrentRow.Cells(itemDgv_WarningStock).Value
        Dim cellBadStock As String = Dgv_Stock.CurrentRow.Cells(itemDgv_BadStock).Value

        Dim chkBox As String = Dgv_Stock.CurrentRow.Cells(itemDgv_ChkBox).Value
        Dim cellValue As String = Dgv_Stock.CurrentRow.Cells(itemDgv_JmlhTransfer).Value



        If Not chkBox Is Nothing Then
            Dgv_Stock.CurrentRow.Cells(itemDgv_JmlhTransfer).ReadOnly = False
        Else
            Dgv_Stock.CurrentRow.Cells(itemDgv_JmlhTransfer).ReadOnly = True

            Dgv_Stock.CurrentRow.Cells(itemDgv_JmlhTransfer).Value = ""
        End If

        If Not IsNumeric(cellValue) Then
            Dgv_Stock.CurrentRow.Cells(itemDgv_JmlhTransfer).Value = ""
        End If

        If kategoriQuality(Cmb_QualityFrom.SelectedIndex) = "Good Stock" Then
            If Val(cellValue) > Val(cellGoodStock) Or Val(cellValue) < 0 Then Dgv_Stock.CurrentRow.Cells(itemDgv_JmlhTransfer).Value = ""
        ElseIf kategoriQuality(Cmb_QualityFrom.SelectedIndex) = "Warning Stock" Then
            If Val(cellValue) > Val(cellWarningStock) Or Val(cellValue) < 0 Then Dgv_Stock.CurrentRow.Cells(itemDgv_JmlhTransfer).Value = ""
        ElseIf kategoriQuality(Cmb_QualityFrom.SelectedIndex) = "Bad Stock" Then
            If Val(cellValue) > Val(cellBadStock) Or Val(cellValue) < 0 Then Dgv_Stock.CurrentRow.Cells(itemDgv_JmlhTransfer).Value = ""
        End If


    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Public Shared Function isnull(ByVal xNullString As Object) As String
        Try
            If IsDBNull(xNullString) Then
                Return "0"
            Else
                Return xNullString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "0"
        End Try
    End Function

    Private Sub Cmb_StockOwner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_StockOwner.SelectedIndexChanged
        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1

        TxtKd_Barang.Text = String.Empty
        TxtSo.Text = String.Empty
        TxtNamaBarang.Text = String.Empty
        TxtGoodStock.Text = String.Empty

        Dgv_Stock.Rows.Clear()
        ListView1.Items.Clear()
    End Sub



End Class
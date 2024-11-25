Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports CrystalDecisions.CrystalReports.Engine

Public Class Transfer_Quality_Stock

    Dim arrSO, arrArea, arrRow, arrBay, arrLevel, arrPosition As New ArrayList
    Dim arrIntialQuality, arrToQuality As New ArrayList

    Dim kategoriQuality As New ArrayList({"Good Stock", "Warning Stock", "Bad Stock"})

    Dim lvDetKodeBarang, lvDetStockOwner, lvDetNamaBarang, lvDetGoodStock, lvDetWarningStock, lvDetBadStock, lvDetBarangSN As String
    Dim lvKodeBarang, lvNamaBarang, lvQualityBefore, lvQualityAfter, lvJumlah, lvBarangSN, lvSO As String
    Dim lvGoodStock, lvWarningStock, lvBadStock As String

    Dim itemDetKodeBarang As Integer = 0
    Dim itemDetStockOwner As Integer = 1
    Dim itemDetNamaBarang As Integer = 2
    Dim itemDetGoodStock As Integer = 3
    Dim itemDetWarningStock As Integer = 4
    Dim itemDetBadStock As Integer = 5
    Dim itemDetBarangSN As Integer = 6

    Dim itemKodeBarang As Integer = 0
    Dim itemNamaBarang As Integer = 1
    Dim itemQualityBefore As Integer = 2
    Dim itemQualityAfter As Integer = 3
    Dim itemGoodStock As Integer = 4
    Dim itemWarningStock As Integer = 5
    Dim itemBadStock As Integer = 6
    Dim itemJumlah As Integer = 7
    Dim itemBarangSN As Integer = 8
    Dim itemSO As Integer = 9

    Private Sub Transfer_Quality_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_All_ComboBox()
        Initial_ListView()

        kosong()

    End Sub

    Private Sub Load_All_ComboBox()
        Try
            OpenConn()

            SQL = "select kode_stock_owner, keterangan from stock_owner_gudang "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' order by Kode_Stock_Owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_StockOwner.Items.Add(Dr("keterangan")) : arrSO.Add(Dr("kode_stock_owner"))
                Loop
            End Using


            SQL = "select Id_WMS_Area, Kode_WMS_Area from EMI_WMS_Areas "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' order by Kode_WMS_Area"
            Using Dr = OpenTrans(SQL)
                Cmb_Area.Items.Add("--- Semua ---")
                arrArea.Add("")
                Do While Dr.Read
                    Cmb_Area.Items.Add(Dr("Kode_WMS_Area")) : arrArea.Add(Dr("Id_WMS_Area"))
                Loop
            End Using

            SQL = "select Id_WMS_Row, Kode_WMS_Row from EMI_WMS_Row "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' order by Kode_WMS_Row"
            Using Dr = OpenTrans(SQL)
                Cmb_Row.Items.Add("--- Semua ---")
                arrRow.Add("")
                Do While Dr.Read
                    Cmb_Row.Items.Add(Dr("Kode_WMS_Row")) : arrRow.Add(Dr("Id_WMS_Row"))
                Loop
            End Using

            SQL = "select Id_WMS_Bay, Kode_WMS_Bay from EMI_WMS_Bay "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' order by Kode_WMS_Bay"
            Using Dr = OpenTrans(SQL)
                Cmb_Bay.Items.Add("--- Semua ---")
                arrBay.Add("")
                Do While Dr.Read
                    Cmb_Bay.Items.Add(Dr("Kode_WMS_Bay")) : arrBay.Add(Dr("Id_WMS_Bay"))
                Loop
            End Using

            SQL = "select Id_WMS_Level, Kode_WMS_Level from EMI_WMS_Level "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' order by Kode_WMS_Level"
            Using Dr = OpenTrans(SQL)
                Cmb_Level.Items.Add("--- Semua ---")
                arrLevel.Add("")
                Do While Dr.Read
                    Cmb_Level.Items.Add(Dr("Kode_WMS_Level")) : arrLevel.Add(Dr("Id_WMS_Level"))
                Loop
            End Using


            SQL = "select Id_WMS_Position, Kode_WMS_Position from EMI_WMS_Position "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' order by Kode_WMS_Position"
            Using Dr = OpenTrans(SQL)
                Cmb_Position.Items.Add("--- Semua ---")
                arrPosition.Add("")
                Do While Dr.Read
                    Cmb_Position.Items.Add(Dr("Kode_WMS_Position")) : arrPosition.Add(Dr("Id_WMS_Position"))
                Loop
            End Using

            For i As Integer = 0 To kategoriQuality.Count - 1
                Cmb_QualityFrom.Items.Add(kategoriQuality(i)) : Cmb_QualityTo.Items.Add(kategoriQuality(i))
            Next

            Cmb_Area.SelectedIndex = 0
            Cmb_Row.SelectedIndex = 0
            Cmb_Bay.SelectedIndex = 0
            Cmb_Level.SelectedIndex = 0
            Cmb_Position.SelectedIndex = 0


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Initial_ListView()
        Lv_BarangInput.Columns.Add("Kode Barang", 150, HorizontalAlignment.Center)
        Lv_BarangInput.Columns.Add("Nama Barang", 220, HorizontalAlignment.Center)
        Lv_BarangInput.Columns.Add("Quality Before", 170, HorizontalAlignment.Center)
        Lv_BarangInput.Columns.Add("Quality After", 170, HorizontalAlignment.Center)
        Lv_BarangInput.Columns.Add("Good Stock", 100, HorizontalAlignment.Center)
        Lv_BarangInput.Columns.Add("Warning Stock", 100, HorizontalAlignment.Center)
        Lv_BarangInput.Columns.Add("Bad Stock", 100, HorizontalAlignment.Center)
        Lv_BarangInput.Columns.Add("Jumlah", 100, HorizontalAlignment.Center)
        Lv_BarangInput.Columns.Add("barangSN", 0, HorizontalAlignment.Center)
        Lv_BarangInput.Columns.Add("SO", 0, HorizontalAlignment.Center)

        Lv_BarangInput.View = View.Details

        Lv_DetailBarang.Columns.Add("Kode Barang", 140, HorizontalAlignment.Center)
        Lv_DetailBarang.Columns.Add("Stock Owner", 160, HorizontalAlignment.Center)
        Lv_DetailBarang.Columns.Add("Nama Barang", 200, HorizontalAlignment.Center)
        Lv_DetailBarang.Columns.Add("Good Stock", 110, HorizontalAlignment.Center)
        Lv_DetailBarang.Columns.Add("Warning Stock", 110, HorizontalAlignment.Center)
        Lv_DetailBarang.Columns.Add("Bad Stock", 110, HorizontalAlignment.Center)
        Lv_DetailBarang.Columns.Add("barangSN", 0, HorizontalAlignment.Center)
        Lv_DetailBarang.View = View.Details

    End Sub


    'FUNCTION GET LISTVIEW DATA
    Private Sub Get_Lv_Detail_Barang(ByVal noIndex As Integer)
        lvDetKodeBarang = Lv_DetailBarang.Items(noIndex).SubItems(itemDetKodeBarang).Text
        lvDetNamaBarang = Lv_DetailBarang.Items(noIndex).SubItems(itemDetNamaBarang).Text
        lvDetStockOwner = Lv_DetailBarang.Items(noIndex).SubItems(itemDetStockOwner).Text
        lvDetGoodStock = Lv_DetailBarang.Items(noIndex).SubItems(itemDetGoodStock).Text
        lvDetWarningStock = Lv_DetailBarang.Items(noIndex).SubItems(itemDetWarningStock).Text
        lvDetBadStock = Lv_DetailBarang.Items(noIndex).SubItems(itemDetBadStock).Text
        lvDetBarangSN = Lv_DetailBarang.Items(noIndex).SubItems(itemDetBarangSN).Text
    End Sub
    Private Sub Get_Lv_Barang_Input(ByVal noIndex As Integer)
        lvKodeBarang = Lv_BarangInput.Items(noIndex).SubItems(itemKodeBarang).Text
        lvNamaBarang = Lv_BarangInput.Items(noIndex).SubItems(itemNamaBarang).Text
        lvQualityBefore = Lv_BarangInput.Items(noIndex).SubItems(itemQualityBefore).Text
        lvQualityAfter = Lv_BarangInput.Items(noIndex).SubItems(itemQualityAfter).Text
        lvGoodStock = Lv_BarangInput.Items(noIndex).SubItems(itemGoodStock).Text
        lvWarningStock = Lv_BarangInput.Items(noIndex).SubItems(itemWarningStock).Text
        lvBadStock = Lv_BarangInput.Items(noIndex).SubItems(itemBadStock).Text
        lvJumlah = Lv_BarangInput.Items(noIndex).SubItems(itemJumlah).Text
        lvBarangSN = Lv_BarangInput.Items(noIndex).SubItems(itemBarangSN).Text
        lvSO = Lv_BarangInput.Items(noIndex).SubItems(itemSO).Text
    End Sub



    Private Sub kosong()

        Tb_KodeBarang.Text = String.Empty
        Tb_KodeBarang.Text = String.Empty
        Tb_GoodStock.Text = String.Empty
        Tb_Jumlah.Text = String.Empty

        Cmb_StockOwner.SelectedIndex = -1
        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1

        Cmb_Area.SelectedIndex = 0
        Cmb_Row.SelectedIndex = 0
        Cmb_Bay.SelectedIndex = 0
        Cmb_Level.SelectedIndex = 0
        Cmb_Position.SelectedIndex = 0

        Lv_BarangInput.Items.Clear()
        Lv_DetailBarang.Items.Clear()

    End Sub



    'FUNCTION HANDLE
    Private Sub Tb_KodeBarang_TextChanged(sender As Object, e As EventArgs) Handles Tb_KodeBarang.TextChanged

        If Tb_KodeBarang.Text.Trim.Length = 0 Then
            Lv_DetailBarang.Visible = False
            Tb_NamaBarang.Text = String.Empty
            Tb_GoodStock.Text = String.Empty
            Tb_Jumlah.Text = String.Empty
            Exit Sub
        Else
            Lv_DetailBarang.Visible = True
        End If

        If Cmb_StockOwner.SelectedIndex = -1 Then Lv_DetailBarang.Visible = False : Exit Sub


        Try
            OpenConn()

            Lv_DetailBarang.Items.Clear()

            SQL = "select a.Kode_Barang, a.Kode_Stock_Owner, a.Nama, b.jumlah, b.Warning_Stock, b.Bad_Stock, b.Serial_Number "
            SQL = SQL & "from barang a, Barang_SN b , View_Warehouse_Position c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and b.Kode_Perusahaan=c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner=b.Kode_Stock_Owner and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and b.Id_Warehouse=c.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "

            If Cmb_Area.SelectedIndex <> 0 Then
                SQL = SQL & "and c.Id_WMS_Area='" & arrArea(Cmb_Area.SelectedIndex) & "' "
            End If

            If Cmb_Row.SelectedIndex <> 0 Then
                SQL = SQL & "and c.Id_WMS_Row='" & arrRow(Cmb_Row.SelectedIndex) & "' "
            End If

            If Cmb_Bay.SelectedIndex <> 0 Then
                SQL = SQL & "and c.Id_WMS_Bay='" & arrBay(Cmb_Bay.SelectedIndex) & "' "
            End If

            If Cmb_Level.SelectedIndex <> 0 Then
                SQL = SQL & "and c.Id_WMS_Level='" & arrLevel(Cmb_Level.SelectedIndex) & "' "
            End If

            If Cmb_Position.SelectedIndex <> 0 Then
                SQL = SQL & "and c.Id_WMS_Position='" & arrPosition(Cmb_Position.SelectedIndex) & "' "
            End If
            SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(Cmb_StockOwner.SelectedIndex) & "' "
            SQL = SQL & "and a.Kode_Barang like '" & Tb_KodeBarang.Text.Trim & "%' "
            SQL = SQL & "order by a.Kode_Barang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_DetailBarang.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(isNull(Dr("jumlah")))
                    lv.SubItems.Add(isNull(Dr("Warning_Stock")))
                    lv.SubItems.Add(isNull(Dr("Bad_Stock")))
                    lv.SubItems.Add(Dr("Serial_Number"))
                Loop
            End Using

            Lv_DetailBarang.Location = New Point(38, 308)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Lv_DetailBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetailBarang.DoubleClick
        If Lv_DetailBarang.Items.Count = 0 Then Exit Sub

        Dim focusIndex As Integer = Lv_DetailBarang.FocusedItem.Index
        Get_Lv_Detail_Barang(focusIndex)

        Tb_KodeBarang.Text = lvDetKodeBarang
        Tb_NamaBarang.Text = lvDetNamaBarang
        Tb_GoodStock.Text = lvDetGoodStock
        Tb_WarningStock.Text = lvDetWarningStock
        Tb_BadStock.Text = lvDetBadStock

        Lv_DetailBarang.Items.Clear()
        Lv_DetailBarang.Visible = False
        Lv_DetailBarang.Location = New Point(1165, 308)

    End Sub



    'HANDLE BUTTON
    Private Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_Insert.Click
        If Cmb_QualityFrom.SelectedIndex = -1 Or Cmb_QualityTo.SelectedIndex = -1 Then Exit Sub
        If Tb_Jumlah.Text.Trim.Length = 0 Then Tb_Jumlah.Focus() : Exit Sub

        Dim kodeBarang As String = Tb_KodeBarang.Text.Trim.ToString
        Dim namaBarang As String = Tb_NamaBarang.Text.Trim.ToString
        Dim qualityBefore As String = Cmb_QualityFrom.SelectedItem.ToString
        Dim qualityAfter As String = Cmb_QualityTo.SelectedItem.ToString
        Dim goodStock As String = Tb_GoodStock.Text.Trim.ToString
        Dim warningStock As String = Tb_WarningStock.Text.Trim.ToString
        Dim badStock As String = Tb_BadStock.Text.Trim.ToString
        Dim jumlah As String = Tb_Jumlah.Text.Trim.ToString

        If Cmb_QualityFrom.Text.Trim.ToUpper = "GOOD STOCK" Then
            If Val(jumlah) > Val(goodStock) Then
                MessageBox.Show("Jumlah Tidak Boleh Lebih Dari Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        ElseIf Cmb_QualityFrom.Text.Trim.ToUpper = "WARNING STOCK" Then
            If Val(jumlah) > Val(warningStock) Then
                MessageBox.Show("Jumlah Tidak Boleh Lebih Dari Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        ElseIf Cmb_QualityFrom.Text.Trim.ToUpper = "BAD STOCK" Then
            If Val(jumlah) > Val(badStock) Then
                MessageBox.Show("Jumlah Tidak Boleh Lebih Dari Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If

        Dim lv As ListViewItem
        lv = Lv_BarangInput.Items.Add(kodeBarang)
        lv.SubItems.Add(namaBarang)
        lv.SubItems.Add(qualityBefore)
        lv.SubItems.Add(qualityAfter)
        lv.SubItems.Add(goodStock)
        lv.SubItems.Add(warningStock)
        lv.SubItems.Add(badStock)
        lv.SubItems.Add(jumlah)
        lv.SubItems.Add(lvDetBarangSN.ToString)
        lv.SubItems.Add(lvDetStockOwner.ToString)


        Tb_KodeBarang.Text = String.Empty
        Tb_NamaBarang.Text = String.Empty
        Tb_GoodStock.Text = String.Empty
        Tb_WarningStock.Text = String.Empty
        Tb_BadStock.Text = String.Empty
        Tb_Jumlah.Text = String.Empty

        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1

    End Sub
    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            For i As Integer = 0 To Lv_BarangInput.Items.Count - 1
                Get_Lv_Barang_Input(i)

                Dim finalStockBefore As Double = 0
                Dim finalStockAfter As Double = 0

                Dim columnUpdateBefore As String = ""
                Dim columnUpdateAfter As String = ""

                'Update BarangSN
                SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, a.Jumlah as Good_Stock, a.Warning_Stock, a.Bad_Stock "
                SQL = SQL & "from Barang_SN a "
                SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner='" & lvSO & "' and a.Kode_Barang='" & lvKodeBarang & "' "
                SQL = SQL & "and a.Serial_Number='" & lvBarangSN & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For j As Integer = 0 To .Rows.Count = -1
                                Dim goodStock As Double = Val(isNull(.Rows(j).Item("Good_Stock")))
                                Dim warningStock As Double = Val(isNull(.Rows(j).Item("Warning_Stock")))
                                Dim badStock As Double = Val(isNull(.Rows(j).Item("Bad_Stock")))
                                Dim jumlahUpdate As Double = Val(isNull(lvJumlah))

                                SQL = "update Barang_SN set "

                                Select Case lvQualityBefore.ToUpper
                                    Case "GOOD STOCK"
                                        finalStockBefore = goodStock - jumlahUpdate
                                        columnUpdateBefore = "Jumlah"

                                    Case "WARNING STOCK"
                                        finalStockBefore = warningStock - jumlahUpdate
                                        columnUpdateBefore = "Warning_Stock"

                                    Case "BAD STOCK"
                                        finalStockBefore = badStock - jumlahUpdate
                                        columnUpdateBefore = "Bad_Stock"
                                End Select

                                Select Case lvQualityAfter.ToUpper
                                    Case "GOOD STOCK"
                                        finalStockAfter = goodStock + jumlahUpdate
                                        columnUpdateAfter = "Jumlah"

                                    Case "WARNING STOCK"
                                        finalStockAfter = warningStock + jumlahUpdate
                                        columnUpdateAfter = "Warning_Stock"

                                    Case "BAD STOCK"
                                        finalStockAfter = badStock + jumlahUpdate
                                        columnUpdateAfter = "Bad_Stock"
                                End Select

                                If Not columnUpdateBefore = "" Then
                                    SQL = SQL & columnUpdateBefore & " = '" & finalStockBefore.ToString & "', "
                                End If

                                If Not columnUpdateAfter = "" Then
                                    SQL = SQL & columnUpdateAfter & " = '" & finalStockAfter.ToString & "' "
                                End If

                                SQL = SQL & "where Kode_Stock_Owner='" & .Rows(i).Item("Kode_Stock_Owner") & "' "
                                SQL = SQL & "and Kode_Barang='" & .Rows(i).Item("Kode_Barang") & "' and Serial_Number='" & .Rows(i).Item("Serial_Number") & "'"

                                ExecuteTrans(SQL)
                            Next
                        End If
                    End With
                End Using

                'Update Barang
                Dim totalGoodStock As Double = 0
                Dim totalWarningStock As Double = 0
                Dim totalBadStock As Double = 0

                SQL = "select Kode_Stock_Owner, Kode_Barang, Jumlah, Warning_Stock, Bad_Stock "
                SQL = SQL & "from Barang_sn where Kode_Stock_Owner='" & lvSO & "' and Kode_Barang='" & lvKodeBarang & "' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        totalGoodStock = totalGoodStock + Val(isNull(Dr("Jumlah")))
                        totalWarningStock = totalWarningStock + Val(isNull(Dr("Warning_Stock")))
                        totalBadStock = totalBadStock + Val(isNull(Dr("Bad_Stock")))
                    Loop
                End Using

                SQL = "update Barang set Good_Stock=" & totalGoodStock & ", Warning_Stock=" & totalWarningStock & ", Bad_Stock=" & totalBadStock & " "
                SQL = SQL & "where Kode_Stock_Owner='" & lvSO & "' and Kode_Barang ='" & lvKodeBarang & "'"
                ExecuteTrans(SQL)

            Next



            Cmd.Transaction.Commit()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Lv_BarangInput.Items.Clear()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    'FUNCTION EVENT
    Private Sub Cmb_StockOwner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_StockOwner.SelectedIndexChanged
        Cmb_Area.SelectedIndex = 0
        Cmb_Row.SelectedIndex = 0
        Cmb_Bay.SelectedIndex = 0
        Cmb_Level.SelectedIndex = 0
        Cmb_Position.SelectedIndex = 0
        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1

        Tb_KodeBarang.Text = String.Empty

    End Sub
    Private Sub Cmb_Area_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Area.SelectedIndexChanged
        Cmb_Row.SelectedIndex = 0
        Cmb_Bay.SelectedIndex = 0
        Cmb_Level.SelectedIndex = 0
        Cmb_Position.SelectedIndex = 0
        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1

        Tb_KodeBarang.Text = String.Empty
    End Sub
    Private Sub Cmb_Row_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Row.SelectedIndexChanged
        Cmb_Bay.SelectedIndex = 0
        Cmb_Level.SelectedIndex = 0
        Cmb_Position.SelectedIndex = 0
        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1

        Tb_KodeBarang.Text = String.Empty
    End Sub
    Private Sub Cmb_Bay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Bay.SelectedIndexChanged
        Cmb_Level.SelectedIndex = 0
        Cmb_Position.SelectedIndex = 0
        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1

        Tb_KodeBarang.Text = String.Empty
    End Sub
    Private Sub Cmb_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Level.SelectedIndexChanged
        Cmb_Position.SelectedIndex = 0
        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1

        Tb_KodeBarang.Text = String.Empty
    End Sub
    Private Sub Cmb_Position_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Position.SelectedIndexChanged
        Cmb_QualityFrom.SelectedIndex = -1
        Cmb_QualityTo.SelectedIndex = -1

        Tb_KodeBarang.Text = String.Empty
    End Sub

    Private Sub Tb_Jumlah_Leave(sender As Object, e As EventArgs) Handles Tb_Jumlah.Leave
        If IsNumeric(Tb_Jumlah.Text.Trim) = False Then Tb_Jumlah.Text = String.Empty : Exit Sub
    End Sub
    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If Lv_BarangInput.Items.Count = 0 Then Exit Sub

        Lv_BarangInput.Items.RemoveAt(Lv_BarangInput.FocusedItem.Index)

    End Sub

    'FUNCTION UTILITY
    Public Shared Function isNull(ByVal xNullString As Object) As String
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


End Class
Imports System.Data.SqlClient

Public Class Transfer_Stock_4


    Dim arrSO, arrInisialFaktur, arrIdWMSWarehouse, WarehosePosition As New ArrayList

    Dim arr2RakTujuan As New List(Of List(Of String))

    Dim lv_DetKodeSO, lv_DetKodeBarang, lv_DetNamaBarang, lv_DetGoodStock, lv_DetSatuan As String

    Dim dgv_Lokasi, dgv_KodeBarang, dgv_SerialNumber, dgv_Nama, dgv_IDWareHouse, dgv_KodeRak As String
    Dim dgv_IDPallet, dgv_GoodStock, dgv_Satuan, dgv_Jumlah, dgv_RakTujuan As String
    Dim dgv_CheckBox As Boolean

    Dim kd_barang As String

    Dim TotalTransfer As Integer = 0

    Dim itemDetKodeSO As Integer = 0
    Dim itemDetKodeBarang As Integer = 1
    Dim itemDetNamaBarang As Integer = 2
    Dim itemDetGoodStock As Integer = 3
    Dim itemDetSatuan As Integer = 4

    Dim itemDgvLokasi As Integer = 0
    Dim itemDgvKodeBarang As Integer = 1
    Dim itemDgvSerialNumber As Integer = 2
    Dim itemDgvNama As Integer = 3
    Dim itemDgvIDWareHose As Integer = 4
    Dim itemDgvKodeRak As Integer = 5
    Dim itemDgvIDPallet As Integer = 6
    Dim itemDgvGoodStock As Integer = 7
    Dim itemDgvSatuan As Integer = 8
    Dim itemDgvCheckBox As Integer = 9
    Dim itemDgvJumlah As Integer = 10
    Dim itemDgvRakTujuan As Integer = 11
    'Dim itemDgvIDWareHouseTujuan As Integer = 10



    Private Sub Transfer_Stock_3_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Transfer_Stock_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
        Initial_List_View()

    End Sub
    Private Sub get_det_barang(ByVal index As Integer)

        lv_DetKodeSO = Lv_DetBarang.Items(index).SubItems(itemDetKodeSO).Text
        lv_DetKodeBarang = Lv_DetBarang.Items(index).SubItems(itemDetKodeBarang).Text
        lv_DetNamaBarang = Lv_DetBarang.Items(index).SubItems(itemDetNamaBarang).Text
        lv_DetGoodStock = Lv_DetBarang.Items(index).SubItems(itemDetGoodStock).Text
        lv_DetSatuan = Lv_DetBarang.Items(index).SubItems(itemDetSatuan).Text

    End Sub


    Private Sub get_grid_view(ByVal index As Integer)

        dgv_Lokasi = DGV_Data_TF.Rows(index).Cells(itemDgvLokasi).Value
        dgv_KodeBarang = DGV_Data_TF.Rows(index).Cells(itemDgvKodeBarang).Value
        dgv_SerialNumber = DGV_Data_TF.Rows(index).Cells(itemDgvSerialNumber).Value
        dgv_Nama = DGV_Data_TF.Rows(index).Cells(itemDgvNama).Value
        dgv_IDWareHouse = DGV_Data_TF.Rows(index).Cells(itemDgvIDWareHose).Value
        dgv_KodeRak = DGV_Data_TF.Rows(index).Cells(itemDgvKodeRak).Value
        dgv_IDPallet = DGV_Data_TF.Rows(index).Cells(itemDgvIDPallet).Value
        dgv_GoodStock = DGV_Data_TF.Rows(index).Cells(itemDgvGoodStock).Value
        dgv_Satuan = DGV_Data_TF.Rows(index).Cells(itemDgvSatuan).Value
        dgv_Jumlah = DGV_Data_TF.Rows(index).Cells(itemDgvJumlah).Value
        dgv_RakTujuan = DGV_Data_TF.Rows(index).Cells(itemDgvRakTujuan).Value

        dgv_CheckBox = Convert.ToBoolean(DGV_Data_TF.Rows(index).Cells(itemDgvCheckBox).Value)


    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Initial_List_View()

        Lv_DetBarang.Columns.Add("Kode SO", 130, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Nama", 200, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Good Stock", 80, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Satuan", 70, HorizontalAlignment.Center)

        Lv_DetBarang.View = View.Details

    End Sub


    'FUNCTION UTILITY
    Private Sub kosong()
        get_jam()

        Try
            OpenConn()

            CmbSo_Tujuan.Items.Clear() : CmbSo_Tujuan.SelectedIndex = -1
            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSo_Tujuan.Items.Add(dr("Keterangan")) : arrSO.Add(dr("kode_stock_owner"))
                    CmbSO_Asal.Items.Add(dr("Keterangan")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_DetBarang.Items.Clear()

        CmbJnsTransfer.Items.Clear()
        CmbJnsTransfer.Items.Add("Antar Rak")
        CmbJnsTransfer.Items.Add("Antar Gudang")

        TxtKeterangan.Text = String.Empty
        TxtKd_Barang.Text = String.Empty
        Txt_SO.Text = String.Empty
        TxtNm_Barang.Text = String.Empty

        DGV_Data_TF.Rows.Clear()

    End Sub
    Private Sub DGV_Data_TF_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellClick
        If e.ColumnIndex = rak_tujuan.Index Then
            DGV_Data_TF.CurrentCell = DGV_Data_TF.Rows(e.RowIndex).Cells(e.ColumnIndex)
            DGV_Data_TF.BeginEdit(True)
        End If
    End Sub

    Private Sub DGV_Data_TF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellEndEdit

        Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value

        If Not IsNumeric(cellValue) Then
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
        End If


        TotalTransfer = 0

        For i As Integer = 0 To DGV_Data_TF.RowCount - 1
            Dim jumlah As Integer = Val(DGV_Data_TF.Rows(i).Cells(itemDgvJumlah).Value)
            TotalTransfer = TotalTransfer + jumlah
        Next

        TxtTotalTransfer.Text = TotalTransfer.ToString

    End Sub
    Private Sub DGV_Data_TF_MouseLeave(sender As Object, e As EventArgs) Handles DGV_Data_TF.MouseLeave
        If DGV_Data_TF.RowCount = 0 Then Exit Sub

        Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value

        If Not IsNumeric(cellValue) Then
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
        End If

        'PINDAH FOKUS
        DGV_Data_TF.CurrentCell = DGV_Data_TF.CurrentRow.Cells(itemDgvSatuan)
        'DGV_Data_TF.BeginEdit(True)
    End Sub



    Private Sub get_jam()
        Try
            OpenConn()

            SQL = "declare @ab int; select @ab = Selisih_Jam from Init; "
            SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss') as Tanggal_Sekarang "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    tgl_skg = dr("Tanggal_Sekarang")
                Loop
            End Using

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur()
        Dim FPro_Results As String = "TS-"
        TxtNo_Transaksi.Text = FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("Emi_Transfer_Stock", "kode_transfer", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(kode_transfer,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub
    Private Sub DGV_Data_TF_Leave(sender As Object, e As EventArgs) Handles DGV_Data_TF.Leave
        Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value

        If Not IsNumeric(cellValue) Then
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
        End If
    End Sub



    'FUNCTION HANDLE
    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged, Txt_SO.TextChanged
        If CmbSO_Asal.Items.Count = 0 Or CmbSO_Asal.SelectedIndex = -1 Then Exit Sub


        If Not TxtKd_Barang.Text.Trim.Count = 0 Then
            Lv_DetBarang.Location = New Point(20, 258)
            Lv_DetBarang.Visible = True
        Else
            Lv_DetBarang.Location = New Point(803, 258)
            Lv_DetBarang.Visible = False
        End If

        Try
            OpenConn()

            Lv_DetBarang.Items.Clear()

            SQL = "select kode_stock_owner, kode_barang, nama, Good_Stock, Satuan from barang "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "and Kode_Barang like '" & TxtKd_Barang.Text & "%' "
            SQL = SQL & "group by kode_stock_owner, kode_barang, nama, Good_Stock, Satuan "
            SQL = SQL & "order by Kode_Barang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As New ListViewItem
                    Lv = Lv_DetBarang.Items.Add(Dr("kode_stock_owner"))
                    Lv.SubItems.Add(Dr("kode_barang"))
                    Lv.SubItems.Add(Dr("nama"))
                    Lv.SubItems.Add(Dr("Good_Stock"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub
    Private Sub Lv_DetBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick

        If Lv_DetBarang.Items.Count = 0 Or TxtKd_Barang.Text.Trim = "" Then Exit Sub

        get_det_barang(Lv_DetBarang.FocusedItem.Index)

        TxtKd_Barang.Text = String.Empty
        Txt_SO.Text = String.Empty
        TxtNm_Barang.Text = String.Empty

        TxtKd_Barang.Text = lv_DetKodeBarang
        Txt_SO.Text = lv_DetKodeSO
        TxtNm_Barang.Text = lv_DetNamaBarang


        Lv_DetBarang.Location = New Point(803, 258)
        Lv_DetBarang.Visible = False

    End Sub
    Private Sub CmbJnsTransfer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJnsTransfer.SelectedIndexChanged

        If CmbJnsTransfer.Items.Count = 0 Then Exit Sub

        If CmbJnsTransfer.SelectedIndex = 0 Then
            CmbSO_Asal.Enabled = True
            CmbSo_Tujuan.SelectedIndex = -1
            CmbSo_Tujuan.Enabled = False

        ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
            CmbSO_Asal.Enabled = True
            CmbSo_Tujuan.Enabled = True
        End If

        DGV_Data_TF.Rows.Clear()
        TxtTotalTransfer.Text = String.Empty


    End Sub
    Private Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_GetData.Click
        If TxtKd_Barang.Text.Trim.Length = 0 Or TxtKd_Barang.Text.Trim = "" Or TxtNm_Barang.Text.Trim.Length = 0 Then Exit Sub

        If CmbJnsTransfer.SelectedIndex = 0 Then
            If CmbSO_Asal.SelectedIndex = -1 Then
                MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSO_Asal.Focus() : Exit Sub
            End If
        ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
            If CmbSo_Tujuan.SelectedIndex = -1 Then
                MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSO_Asal.Focus() : Exit Sub
            End If
        End If


        Try
            OpenConn()

            Dim rows As Integer = 0
            DGV_Data_TF.Rows.Clear()

            kd_barang = String.Empty
            kd_barang = TxtKd_Barang.Text

            arrIdWMSWarehouse.Clear()
            WarehosePosition.Clear()

            SQL = "select Id_WMS_Warehouse_Position, Keterangan "
            SQL = SQL & "from View_Warehouse_Position "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "

            If CmbJnsTransfer.SelectedIndex = 0 Then
                SQL = SQL & "and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
                SQL = SQL & "and Kode_Stock_Owner='" & arrSO(CmbSo_Tujuan.SelectedIndex) & "' "
            End If
            SQL = SQL & "order by Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    WarehosePosition.Add(Dr("Keterangan")) : arrIdWMSWarehouse.Add(Dr("Id_WMS_Warehouse_Position"))

                Loop

            End Using


            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, b.Nama, a.Id_Warehouse, c.Keterangan as kode_rak, "
            SQL = SQL & " a.Id_Nametag_pallet, a.jumlah, b.satuan from barang_sn a, barang b, View_Warehouse_Position c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.Id_Warehouse=c.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and b.Kode_Stock_Owner='" & Txt_SO.Text & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "'"
            SQL = SQL & "order by a.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim subArr As New List(Of String)

                    DGV_Data_TF.Rows.Add(1)
                    DGV_Data_TF.Rows(rows).Cells(itemDgvLokasi).Value = Dr("Kode_Stock_Owner")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvKodeBarang).Value = Dr("Kode_Barang")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("Serial_Number")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvNama).Value = Dr("Nama")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvIDWareHose).Value = Dr("Id_Warehouse")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvKodeRak).Value = Dr("kode_rak")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvIDPallet).Value = Dr("Id_Nametag_pallet")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvGoodStock).Value = Dr("jumlah")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvSatuan).Value = Dr("satuan")

                    Dim dgvCmbValueRak As DataGridViewComboBoxCell
                    dgvCmbValueRak = DGV_Data_TF.Rows(rows).Cells(itemDgvRakTujuan)
                    dgvCmbValueRak.Items.Clear()

                    dgvCmbValueRak.Items.Add("-- Tidak Berubah --") : subArr.Add(Dr("Id_Warehouse"))
                    For i As Integer = 0 To WarehosePosition.Count - 1
                        dgvCmbValueRak.Items.Add(WarehosePosition(i)) : subArr.Add(arrIdWMSWarehouse(i))
                    Next


                    arr2RakTujuan.Add(subArr)
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
    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub
    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If DGV_Data_TF.RowCount = 0 Then
            MessageBox.Show("Belum ada barang yang mau di transfer!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKd_Barang.Focus() : Exit Sub
        ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKeterangan.Focus() : Exit Sub
        ElseIf CmbSO_Asal.Text = CmbSo_Tujuan.Text Then
            MessageBox.Show("SO asal dan so tujuan sama", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If


        If CmbJnsTransfer.SelectedIndex = 0 Then
            If CmbSO_Asal.SelectedIndex = -1 Then
                MessageBox.Show("SO asal harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSO_Asal.Focus() : Exit Sub
            End If
        Else
            If CmbSo_Tujuan.SelectedIndex = -1 Then
                MessageBox.Show("SO tujuan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSo_Tujuan.Focus() : Exit Sub
            End If

        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            Dim isCheck As Boolean = False


            For row As Integer = 0 To DGV_Data_TF.RowCount - 1

                get_grid_view(row)

                If dgv_CheckBox = False Then
                    Continue For
                End If

                isCheck = True

                Dim comboBoxCell As DataGridViewComboBoxCell = CType(DGV_Data_TF.Rows(row).Cells(itemDgvRakTujuan), DataGridViewComboBoxCell)
                Dim selectedIndex As Integer = comboBoxCell.Items.IndexOf(comboBoxCell.Value)


                SQL = "insert into Tf_Stock_det (Kode_Perusahaan, Kode_Transfer, Kode_Stock_Owner, SO_Tujuan ,Kode_Barang, Serial_Number_Awal, Serial_Number_Akhir, "
                SQL = SQL & "Id_Wms_Awal, Id_Wms_Tujuan, Jumlah_Transfer, Satuan, Status_ACC) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & arrSO(CmbSO_Asal.SelectedIndex) & "', "

                If Not CmbSo_Tujuan.SelectedIndex = -1 Then
                    SQL = SQL & "'" & arrSO(CmbSo_Tujuan.SelectedIndex) & "', "
                Else
                    SQL = SQL & "'" & arrSO(CmbSO_Asal.SelectedIndex) & "', "
                End If

                SQL = SQL & "'" & dgv_KodeBarang & "', '" & dgv_SerialNumber & "', Null, " & dgv_IDWareHouse & ", " & arr2RakTujuan(row)(selectedIndex) & ", '" & dgv_Jumlah.ToString & "', "
                SQL = SQL & "'" & dgv_Satuan & "', Null )"
                ExecuteTrans(SQL)

                'UPDATE

                Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)

                SQL = "update barang_sn set jumlah = '" & jumlahAkhir.ToString & "' "
                SQL = SQL & "where Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and Kode_Barang='" & dgv_KodeBarang & "' "
                SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "'"
                ExecuteTrans(SQL)

            Next

            If isCheck = True Then
                SQL = "insert into Tf_Stock (Kode_Perusahaan, Kode_Transfer, Kode_Stock_Owner, SO_Tujuan ,Kode_Barang, "
                SQL = SQL & "Tanggal, Jam, Jenis_Transfer, Total_Transfer, Satuan, Keterangan) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & arrSO(CmbSO_Asal.SelectedIndex) & "', "

                If Not CmbSo_Tujuan.SelectedIndex = -1 Then
                    SQL = SQL & "'" & arrSO(CmbSo_Tujuan.SelectedIndex) & "', "
                Else

                    SQL = SQL & "'" & arrSO(CmbSO_Asal.SelectedIndex) & "', "
                End If

                SQL = SQL & "'" & kd_barang & "', '" & tgl_skg & "', '" & tgl_skg.ToString("HH:mm:ss") & "', '" & CmbJnsTransfer.SelectedItem & "', "
                SQL = SQL & "'" & TxtTotalTransfer.Text.ToString & "', '" & dgv_Satuan & "', '" & TxtKeterangan.Text.ToString & "')"
                ExecuteTrans(SQL)

                'UPDATE STOCK BARANG
                SQL = "SELECT SUM(jumlah) AS TotalJumlah FROM barang_sn "
                SQL = SQL & "WHERE Kode_Stock_Owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
                SQL = SQL & "AND Kode_Barang = '" & kd_barang & "' and Kode_Perusahaan='" & KodePerusahaan & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            SQL = "update barang set Good_Stock=" & .Rows(0).Item("TotalJumlah") & " "
                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
                            SQL = SQL & " and Kode_Barang='" & kd_barang & "'"
                            ExecuteTrans(SQL)


                        End If
                    End With
                End Using
            End If




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

    Private Sub CmbSO_Asal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSO_Asal.SelectedIndexChanged
        DGV_Data_TF.Rows.Clear()
        TxtKd_Barang.Text = ""
        Txt_SO.Text = ""
        TxtNm_Barang.Text = ""
        CmbSo_Tujuan.SelectedIndex = -1
    End Sub
    Private Sub CmbSo_Tujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSo_Tujuan.SelectedIndexChanged
        DGV_Data_TF.Rows.Clear()
        TxtKd_Barang.Text = ""
        Txt_SO.Text = ""
        TxtNm_Barang.Text = ""
    End Sub



End Class
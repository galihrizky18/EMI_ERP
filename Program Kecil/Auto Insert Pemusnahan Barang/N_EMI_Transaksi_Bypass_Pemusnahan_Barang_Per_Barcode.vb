Public Class N_EMI_Transaksi_Bypass_Pemusnahan_Barang_Per_Barcode

    Dim arrInisialFaktur, arrSO As New ArrayList

    Dim switchAutoComplete As Boolean = False

    Dim Dgv_KdSo, Dgv_KdBarang, Dgv_NmBarang, Dgv_Barcode, Dgv_SerialNumber,
        Dgv_JumlahPengajuan, Dgv_Satuan, Dgv_Warna,
        Dgv_IdWmsAwal, Dgv_IdWmsTujuan, Dgv_PalletAwal, Dgv_ChkBox, Dgv_JmlhDefault As String

    Dim Cell_KdSO As Integer = 0
    Dim Cell_KdBarang As Integer = 1
    Dim Cell_NmBarang As Integer = 2
    Dim Cell_Barcode As Integer = 3
    Dim Cell_Serial_Number As Integer = 4
    Dim Cell_Jumlah_Pengajuan As Integer = 5
    Dim Cell_Satuan As Integer = 6
    Dim Cell_Warna As Integer = 7
    Dim Cell_ID_Wms_Awal As Integer = 8
    Dim Cell_Id_Wms_Tujuan As Integer = 9
    Dim Cell_Pallet_Awal As Integer = 10
    Dim Cell_ChkBox As Integer = 11
    Dim Cell_JmlhDefault As Integer = 12

    Dim Random As New Random()

    Private Sub N_EMI_Transaksi_Bypass_Pemusnahan_Barang_Per_Barcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 330, HorizontalAlignment.Left)


        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear()
            SQL = "select kode_stock_owner from Stock_Owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("kode_stock_owner"))
                    Cmb_Lokasi.SelectedItem = "HEAD OFFICE"
                Loop
            End Using

            Cmb_Kd_SO.Items.Clear() : arrSO.Clear() : arrInisialFaktur.Clear()
            Cmb_Kd_SO.Items.Add(OpsiSeluruh) : arrSO.Add(OpsiSeluruh) : arrInisialFaktur.Add(OpsiSeluruh)
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan "
            SQL &= $"From Stock_Owner_Gudang "
            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' and aktif = 'Y' and flag_waste='Y' "
            SQL &= $"order by kode_stock_owner "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Kd_SO.Items.Add(Dr("Keterangan"))
                    arrSO.Add(Dr("kode_stock_owner")) : arrInisialFaktur.Add(Dr("inisial_faktur"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dgv_Data.Columns(Cell_ChkBox).DisplayIndex = 5

        Kosong()
    End Sub


    Private Sub Kosong()

        Txt_Keterangan.Text = ""
        Cmb_Kd_SO.SelectedIndex = 0

        switchAutoComplete = True
        Txt_Kd_Barang.Text = OpsiSeluruh
        Txt_Nm_Barang.Text = OpsiSeluruh
        switchAutoComplete = False

        Dgv_Data.Rows.Clear()




    End Sub

    Private Sub get_no_faktur()
        Dim FPro_Results As String = "PMB-"
        TxtNo_Transaksi.Text = FPro_Results & arrInisialFaktur.Item(Cmb_Kd_SO.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("N_EMI_Transaksi_Transfer_Waste", "no_faktur", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(no_faktur,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(Cmb_Kd_SO.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(Cmb_Kd_SO.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub


    Private Sub Dgv_Data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEndEdit
        If Dgv_Data.Rows.Count = 0 Then Exit Sub

        Get_Data_DGV(Dgv_Data.CurrentRow.Index)

        If Dgv_ChkBox = True Then
            Dgv_Data.CurrentRow.Cells(Cell_Jumlah_Pengajuan).ReadOnly = False

            If Not IsNumeric(Dgv_Data.CurrentRow.Cells(Cell_Jumlah_Pengajuan).Value) Then
                Dgv_Data.CurrentRow.Cells(Cell_Jumlah_Pengajuan).Value = Format(Val(HilangkanTanda(0)), "N4")
            Else
                Dgv_Data.CurrentRow.Cells(Cell_Jumlah_Pengajuan).Value = Format(Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(Cell_Jumlah_Pengajuan).Value)), "N4")
            End If
        Else
            Dgv_Data.CurrentRow.Cells(Cell_Jumlah_Pengajuan).ReadOnly = True
            Dgv_Data.CurrentRow.Cells(Cell_Jumlah_Pengajuan).Value = Format(Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(Cell_JmlhDefault).Value)), "N4")
        End If


    End Sub

    Private Sub Cmb_Kd_SO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kd_SO.SelectedIndexChanged
        If Cmb_Kd_SO.SelectedIndex <> 0 Then
            Try
                OpenConn()

                get_no_faktur()

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub



    Private Sub Get_Data_DGV(ByVal rows As Integer)
        Dgv_KdSo = Dgv_Data.Rows(rows).Cells(Cell_KdSO).Value
        Dgv_KdBarang = Dgv_Data.Rows(rows).Cells(Cell_KdBarang).Value
        Dgv_NmBarang = Dgv_Data.Rows(rows).Cells(Cell_NmBarang).Value
        Dgv_Barcode = Dgv_Data.Rows(rows).Cells(Cell_Barcode).Value
        Dgv_SerialNumber = Dgv_Data.Rows(rows).Cells(Cell_Serial_Number).Value
        Dgv_JumlahPengajuan = Dgv_Data.Rows(rows).Cells(Cell_Jumlah_Pengajuan).Value
        Dgv_Satuan = Dgv_Data.Rows(rows).Cells(Cell_Satuan).Value
        Dgv_Warna = Dgv_Data.Rows(rows).Cells(Cell_Warna).Value
        Dgv_IdWmsAwal = Dgv_Data.Rows(rows).Cells(Cell_ID_Wms_Awal).Value
        Dgv_IdWmsTujuan = Dgv_Data.Rows(rows).Cells(Cell_Id_Wms_Tujuan).Value
        Dgv_PalletAwal = Dgv_Data.Rows(rows).Cells(Cell_Pallet_Awal).Value
        Dgv_ChkBox = Dgv_Data.Rows(rows).Cells(Cell_ChkBox).Value
        Dgv_JmlhDefault = Dgv_Data.Rows(rows).Cells(Cell_JmlhDefault).Value
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.TextChanged
        If switchAutoComplete Then Exit Sub

        If Txt_Kd_Barang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 132)
            Txt_Kd_Barang.Text = ""
            Txt_Kd_Barang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(118, 132)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, EMI_Group_Jenis b, barang_sn c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and c.Flag_Pengajuan_Waste = 'Y' "
            SQL = SQL & "and c.Flag_Sdh_Pengajuan_Waste is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Stock_Owner = '" & arrSO(Cmb_Kd_SO.SelectedIndex) & "' "
            SQL = SQL & "and a.Kode_Barang like '%" & Txt_Kd_Barang.Text & "%' "
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

    Private Sub Txt_Kd_Barang_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.Leave
        If Txt_Kd_Barang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Barang.Text = OpsiSeluruh Then

                SQL = "select Distinct a.Kode_Barang, a.Nama "
                SQL = SQL & "from barang a, EMI_Group_Jenis b, barang_sn c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.kode_perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "and c.Flag_Pengajuan_Waste = 'Y' "
                SQL = SQL & "and c.Flag_Sdh_Pengajuan_Waste is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner = '" & arrSO(Cmb_Kd_SO.SelectedIndex) & "' "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_Kd_Barang.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kd_Barang.Text = Dr("Kode_Barang")
                        Txt_Nm_Barang.Text = Dr("Nama")
                        Btn_Get.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kd_Barang.Text = ""
                        Txt_Nm_Barang.Text = ""
                        Txt_Kd_Barang.Focus()
                    End If

                    Lv_Barang.Visible = False
                    Lv_Barang.Location = New Point(1200, 132)
                End Using
            Else
                Btn_Get.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Barang.Text.Trim.Length = 0 Then Txt_Kd_Barang.Focus()
            Txt_Kd_Barang_Leave(Txt_Kd_Barang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 132)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Kd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_Nm_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nm_Barang.TextChanged
        If switchAutoComplete Then Exit Sub

        If Txt_Nm_Barang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 132)
            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(118, 132)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, EMI_Group_Jenis b, barang_sn c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and c.Flag_Pengajuan_Waste = 'Y' "
            SQL = SQL & "and c.Flag_Sdh_Pengajuan_Waste is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Stock_Owner = '" & arrSO(Cmb_Kd_SO.SelectedIndex) & "' "
            SQL = SQL & "and a.Nama like '%" & Txt_Nm_Barang.Text & "%' "
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

    Private Sub Txt_Nm_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Nm_Barang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kd_Barang_Leave(Txt_Nm_Barang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 132)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Nm_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Nm_Barang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        switchAutoComplete = True
        Txt_Kd_Barang.Text = KdBarang
        Txt_Nm_Barang.Text = NmKdBarang
        switchAutoComplete = False
        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(1200, 132)

        Btn_Get.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    Private Sub Btn_Get_Click(sender As Object, e As EventArgs) Handles Btn_Get.Click
        If Cmb_Kd_SO.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Stock Owner harus dipilih . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Kd_SO.DroppedDown = True
            Exit Sub
        ElseIf Txt_Kd_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Barang.Focus()
            Exit Sub
        End If

        LoadDataWaste()
    End Sub

    Private Sub LoadDataWaste()

        Try
            OpenConn()

            Dgv_Data.Rows.Clear()
            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, b.nama as Nama_Barang, c.Id_WMS_Warehouse_Position as Id_Wms_Awal, a.Warna, "
            SQL &= $"(a.Qr_Code+'-'+a.Kode_Unik_Berjalan) as Barcode, a.Serial_Number, isnull(a.Qty_Pengajuan_Waste, 0) as Qty_Pengajuan_Waste, b.Satuan "
            SQL &= $"from barang_sn a "
            SQL &= $"inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL &= $"inner join View_Warehouse_Position c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Warehouse = c.Id_WMS_Warehouse_Position "
            SQL &= $"where a.kode_perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.Flag_Pengajuan_Waste = 'Y' "
            SQL &= $"and a.Flag_Sdh_Pengajuan_Waste is null "

            If Not arrSO(Cmb_Kd_SO.SelectedIndex).ToString.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
                SQL &= $"and a.Kode_Stock_Owner = '{arrSO(Cmb_Kd_SO.SelectedIndex)}' "
            End If

            If Not Txt_Kd_Barang.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
                SQL &= $"and a.Kode_Barang = '{Txt_Kd_Barang.Text}' "
            End If
            SQL &= $"order by a.Kode_Stock_Owner, a.Kode_Barang, b.nama, (a.Qr_Code+'-'+a.Kode_Unik_Berjalan)"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Data.Rows.Add(1)
                            Dgv_Data.Rows(i).Cells(Cell_KdSO).Value = .Rows(i).Item("Kode_Stock_Owner")
                            Dgv_Data.Rows(i).Cells(Cell_KdBarang).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_Data.Rows(i).Cells(Cell_NmBarang).Value = .Rows(i).Item("Nama_Barang")
                            Dgv_Data.Rows(i).Cells(Cell_Barcode).Value = .Rows(i).Item("Barcode")
                            Dgv_Data.Rows(i).Cells(Cell_Serial_Number).Value = .Rows(i).Item("Serial_Number")
                            Dgv_Data.Rows(i).Cells(Cell_Jumlah_Pengajuan).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Qty_Pengajuan_Waste"))), "N4")
                            Dgv_Data.Rows(i).Cells(Cell_Satuan).Value = .Rows(i).Item("Satuan")
                            Dgv_Data.Rows(i).Cells(Cell_Warna).Value = .Rows(i).Item("Warna")
                            Dgv_Data.Rows(i).Cells(Cell_ID_Wms_Awal).Value = .Rows(i).Item("Id_Wms_Awal")
                            Dgv_Data.Rows(i).Cells(Cell_Pallet_Awal).Value = 0

                            SQL = "select top(1) a.id_wms_warehouse_position "
                            SQL &= $"FROM view_warehouse_position a "
                            SQL &= $"WHERE a.kode_Perusahaan ='{KodePerusahaan}' "
                            If arrSO(Cmb_Kd_SO.SelectedIndex).ToString.ToUpper = OpsiSeluruh.ToUpper Then
                                SQL &= $"and a.Kode_Stock_Owner='{arrSO(1)}' "
                            Else
                                SQL &= $"and a.Kode_Stock_Owner='{arrSO(Cmb_Kd_SO.SelectedIndex)}' "
                            End If
                            SQL &= $"group by a.id_wms_warehouse_position "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dgv_Data.Rows(i).Cells(Cell_Id_Wms_Tujuan).Value = Dr("id_wms_warehouse_position")
                                Else
                                    Dr.Close()
                                    CloseConn()
                                    MessageBox.Show($"Id Warehouse Position untuk WMS Tujuan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            Dgv_Data.Rows(i).Cells(Cell_ChkBox).Value = False
                            Dgv_Data.Rows(i).Cells(Cell_JmlhDefault).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Qty_Pengajuan_Waste"))), "N4")


                        Next

                    Else
                        CloseConn()
                        MessageBox.Show($"Data Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Cmb_Kd_SO.SelectedIndex = 0 Then
            MessageBox.Show("Lokasi Stock Owner harus dipilih . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Txt_Kd_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Dgv_Data.Rows.Count = 0 Then Exit Sub

        If MessageBox.Show("Yakin ingin melakukan simpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            Dim arrBarcodeScan As New ArrayList


            '===================
            '=     MAPPING     =
            '===================
            Dim ArrDetail As New Dictionary(Of String, (kdSo As String, KdBarang As String, Total As Double, Satuan As String))
            Dim ArrDet As New Dictionary(Of String, (kdSo As String, KdBarang As String, Jumlah As Double, Satuan As String, Barcode As String, ID_Wms_Awal As String, ID_Wms_Tujuan As String, Pallet_Awal As String, Warna As String))

            Dim HasData As Boolean = False
            For i As Integer = 0 To Dgv_Data.Rows.Count - 1
                Get_Data_DGV(i)

                If Dgv_ChkBox = False Then
                    Continue For
                End If
                HasData = True

                Dim keyFound = ArrDetail.FirstOrDefault(Function(data) data.Value.kdSo = Dgv_KdSo AndAlso data.Value.KdBarang = Dgv_KdBarang)

                If keyFound.Key IsNot Nothing Then
                    Dim DataLama = keyFound.Value

                    ArrDetail(keyFound.Key) = (
                        DataLama.kdSo,
                        DataLama.KdBarang,
                        DataLama.Total + Val(HilangkanTanda(Dgv_JumlahPengajuan)),
                        DataLama.Satuan
                    )

                Else
                    ArrDetail.Add(Guid.NewGuid().ToString(),
                        (
                            KdSo:=Dgv_KdSo,
                            KdBarang:=Dgv_KdBarang,
                            Total:=Val(HilangkanTanda(Dgv_JumlahPengajuan)),
                            Satuan:=Dgv_Satuan
                        )
                    )
                End If



                ArrDet.Add(
                    Guid.NewGuid().ToString(),
                    (
                        kdSo:=Dgv_KdSo,
                        KdBarang:=Dgv_KdBarang,
                        Jumlah:=Val(HilangkanTanda(Dgv_JumlahPengajuan)),
                        Satuan:=Dgv_Satuan,
                        Barcode:=Dgv_Barcode,
                        ID_Wms_Awal:=Dgv_IdWmsAwal,
                        ID_Wms_Tujuan:=Dgv_IdWmsTujuan,
                        Pallet_Awal:=Dgv_PalletAwal,
                        Warna:=Dgv_Warna
                    )
                )


            Next

            If Not HasData Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Tidak ada data yang diinsert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            '=================================================
            '=     INSERT N_EMI_Transaksi_Transfer_Waste     =
            '=================================================
            SQL = "insert into N_EMI_Transaksi_Transfer_Waste (kode_perusahaan, No_faktur, Kode_Stock_Owner, Tanggal, Jam, UserID, Lokasi, Keterangan) Values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & arrSO(Cmb_Kd_SO.SelectedIndex) & "', "
            SQL = SQL & "'" & tgl_skg & "', '" & tgl_skg.ToString("HH:mm:ss") & "', '" & UserID & "', "
            SQL = SQL & "'" & Cmb_Lokasi.Text & "', '" & Txt_Keterangan.Text & "')"
            ExecuteTrans(SQL)


            For Each detail In ArrDetail
                Dim key = detail.Key
                Dim data = detail.Value

                Dim nilai_kecil As Double = Val(HilangkanTanda(data.Total))


                Dim Jenis_Berat As String = ""
                SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
                SQL = SQL & "satuan='" & data.Satuan & "' and kode_perusahaan='" & KodePerusahaan & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Jenis_Berat = dr("flag_tampil_berat")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data Satuan Tidak ada . . ! ! ")
                        Exit Sub
                    End If
                End Using

                Dim Jenis_kemasan As String = ""
                SQL = "Select Jenis_Kemasan from barang where "
                SQL = SQL & "Kode_Barang='" & data.KdBarang & "' and kode_perusahaan='" & KodePerusahaan & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Jenis_kemasan = dr("Jenis_Kemasan")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data Satuan Tidak ada . . ! ! ")
                        Exit Sub
                    End If
                End Using

                Dim Flag_Timbang As String = "T"


                '========================================================
                '=     INSERT N_EMI_Transaksi_Transfer_Waste_Detail     =
                '========================================================
                SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Detail (Kode_Perusahaan, No_faktur, Kode_Barang, Total, Satuan, "
                SQL = SQL & "Total_Barang, Satuan_Barang, Total_Bags, Flag_Timbang) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & data.KdBarang & "', "
                SQL = SQL & "'" & HilangkanTanda(data.Total) & "', '" & data.Satuan & "', "
                SQL = SQL & "'" & nilai_kecil & "', '" & data.Satuan & "', "
                SQL = SQL & "'" & HilangkanTanda(0) & "', '" & Flag_Timbang & "')"
                ExecuteTrans(SQL)

                Dim x_ident_current As Integer = 0
                SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Transfer_Waste_Detail') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        x_ident_current = Dr("urutan")
                    End If
                End Using

                SQL = "select kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste_Detail where "
                SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur='" & TxtNo_Transaksi.Text & "' and "
                SQL = SQL & "Kode_barang='" & data.KdBarang & "' and "
                SQL = SQL & "urut_oto='" & x_ident_current & "' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Silahkan Ulangi Transaksi  . . ! ! ")
                        Exit Sub
                    End If
                End Using


                For Each Det In ArrDet
                    Dim KeyDet = Det.Key
                    Dim DataDet = Det.Value

                    If data.kdSo = DataDet.kdSo AndAlso data.KdBarang = DataDet.KdBarang Then

                        '============================================
                        '=     CEK APAKAH DATA BELUM DI TIMBANG     =
                        '============================================
                        SQL = "select a.Kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Det b, barang_sn c  where "
                        SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan And a.No_Faktur=b.no_faktur and "
                        SQL = SQL & "b.kode_Perusahaan=c.kode_Perusahaan And b.Serial_Number_Awal=c.serial_number and "
                        SQL = SQL & "c.qr_Code+'-'+kode_unik_berjalan = '" & DataDet.Barcode & "' and "
                        SQL = SQL & "selesai is null and a.status is null "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Barang pada Barcode " & DataDet.Barcode & " belum melalui proses pencetakan barcode.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                Dr.Close()
                            End If
                        End Using

                        Dim nilai_kecildetail As Double = Val(HilangkanTanda(DataDet.Jumlah))

                        Dim jenis_bags As String = ""
                        Dim isi_per_bags As Double = 0
                        SQL = "select Jenis_Kemasan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags from barang where "
                        SQL = SQL & "kode_perusahaan='" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_barang='" & DataDet.KdBarang & "' and "
                        SQL = SQL & "kode_stock_owner='" & DataDet.kdSo & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                jenis_bags = Dr("Jenis_Kemasan").ToString.ToUpper
                                isi_per_bags = Dr("Isi_Per_Bags")
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        Dim sisaPotong As Double = 0
                        Dim JumlahDipotong As Double = 0
                        SQL = "select a.Jumlah as Stock_SN, a.serial_number "
                        SQL = SQL & "from Barang_SN a where "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and a.qr_Code+'-'+a.kode_unik_berjalan = '" & DataDet.Barcode & "' "
                        SQL = SQL & "and a.Kode_stock_owner = '" & DataDet.kdSo & "' and a.jumlah<>0 "
                        SQL = SQL & "order by a.Tgl_Expired "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then

                                    sisaPotong = Val(HilangkanTanda(nilai_kecildetail))

                                    For Index As Integer = 0 To .Rows.Count - 1
                                        If sisaPotong = 0 Then
                                            Exit For
                                        ElseIf sisaPotong < 0 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terdapat Kesalahan saat Potong Barang Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        Dim JumlahInsert As Double = 0
                                        Dim Satuan As String = ""

                                        Dim Data_SN As String = .Rows(Index).Item("serial_number")

                                        If sisaPotong < Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Or sisaPotong = Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Then

                                            JumlahInsert = sisaPotong
                                            ' Satuan = .Rows(Index).Item("Satuan").ToString.Trim


                                            JumlahDipotong += sisaPotong
                                            sisaPotong = 0

                                        ElseIf sisaPotong > Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Then

                                            JumlahInsert = Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
                                            'Satuan = .Rows(Index).Item("Satuan").ToString.Trim

                                            JumlahDipotong += Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
                                            sisaPotong = sisaPotong - Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        Dim Jumlah_Bags = 0
                                        If jenis_bags = "ORIGINAL BAGS" Then
                                            Jumlah_Bags = JumlahInsert / isi_per_bags
                                        End If


                                        '==============================================
                                        '=     INSERT N_EMI_Transaksi_Transfer_Waste_Detail     =
                                        '==============================================

                                        Dim nilai_BesarInsert As Double = Val(HilangkanTanda(HilangkanTanda(JumlahInsert)))

                                        SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det(Kode_Perusahaan, No_faktur, Id_Wms_Awal, No_Pallet_Awal, Id_Wms_Tujuan, "
                                        SQL = SQL & "Serial_Number_Awal, Jumlah, Jumlah_Barang, Jumlah_Bags, Warna, Urut_TF) values( "
                                        SQL = SQL & "'" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & DataDet.ID_Wms_Awal & "', "
                                        SQL = SQL & "'" & DataDet.Pallet_Awal & "', '" & DataDet.ID_Wms_Tujuan & "', '" & Data_SN & "', "
                                        SQL = SQL & "'" & nilai_BesarInsert & "', '" & JumlahInsert & "', "
                                        SQL = SQL & "'" & Jumlah_Bags & "', "
                                        SQL = SQL & "'" & DataDet.Warna & "', '" & x_ident_current & "')"
                                        ExecuteTrans(SQL)

                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalahan Pada Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using

                        If Val(HilangkanTanda(JumlahDipotong)) <> Val(HilangkanTanda(nilai_kecildetail)) Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan Saat Memotong Stock Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If


                        arrBarcodeScan.Add(DataDet.Barcode)



                    End If

                Next

            Next


            '=========================
            '=     STEP VALIDASI     =
            '=========================
            For z As Integer = 0 To arrBarcodeScan.Count - 1

                Dim arr_Sn As New ArrayList

                Dim QrLama As String = ""
                Dim expDate As String = ""
                Dim batchLama As String = ""
                Dim tglMsk As String = ""
                Dim metodePengeluaranStock As String = ""
                Dim GetDataKodeTransfer, GetDataLokasi, GetDataKdBrg, GetDataNmBrg, GetDataBrgSN, GetDataJmlEstimasi, GetDataSatuanBesar, GetDataSatuanKecil, GetDataUrutOto As String
                Dim GetJumlahBags, GetRakTujuan, GetPalletTujuan, GetWarna As String
                Dim SN As String = ""

                Dim ada_data As Boolean = False
                SQL = "Select c.serial_number from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Det b, barang_sn c where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur "
                SQL = SQL & "And a.status Is null And b.selesai Is null  "
                SQL = SQL & "And b.kode_perusahaan=c.kode_Perusahaan And b.serial_number_awal=c.serial_number "
                SQL = SQL & "And c.kode_perusahaan='" & KodePerusahaan & "' and c.qr_code+'-'+kode_unik_berjalan='" & arrBarcodeScan(z) & "' "
                SQL = SQL & "and c.Flag_Pengajuan_Waste = 'Y' "
                SQL = SQL & "and c.Flag_Sdh_Pengajuan_Waste is null "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        ada_data = True
                        arr_Sn.Add(dr("serial_number"))
                    Loop
                End Using


                If ada_data = False Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Barcode Tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Kosong()
                    Exit Sub
                End If


                For Indxx = 0 To arr_Sn.Count - 1

                    'Ambil Data SN Berdasar Barcode
                    SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, b.Metode_Pengeluaran_Stok, a.Tgl_Masuk, a.Blok_SN "
                    SQL = SQL & "from barang_sn a, barang b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                    SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Jumlah <> 0 "
                    SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & arrBarcodeScan(z) & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            If General_Class.CekNULL(Dr("Blok_SN")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("SN Pada Pallet di Block, Validasi di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                'Kosong()
                                Exit Sub
                            End If

                            QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                            batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                            SN = Dr("serial_number")
                            expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                            tglMsk = General_Class.CekNULL(Dr("tgl_masuk"))
                            metodePengeluaranStock = General_Class.CekNULL(Dr("Metode_Pengeluaran_Stok"))

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Kosong()
                            Exit Sub
                        End If
                    End Using


                    'Cek data YG Mau di TF, Berdasar SN dr Barcode
                    SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, d.nama as Nama_Barang, c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Serial_Number_Awal, "
                    SQL = SQL & "b.Satuan_Barang, c.Urut_Oto, c.Warna, c.Id_Wms_Tujuan "
                    SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, Barang d "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                    SQL = SQL & "and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.kode_barang "
                    SQL = SQL & "and a.status is null and a.Flag_Validasi is null "
                    SQL = SQL & "and b.Flag_Timbang = 'T' and c.Selesai is null "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and c.Serial_Number_Awal = '" & SN & "' "

                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            GetDataKodeTransfer = Dr("No_faktur")
                            GetDataLokasi = Dr("Kode_Stock_Owner")
                            GetDataKdBrg = Dr("Kode_Barang")
                            GetDataNmBrg = Dr("Nama_Barang")
                            GetDataBrgSN = Dr("Serial_Number_Awal")
                            GetDataJmlEstimasi = HilangkanTanda(Format(Dr("Jumlah"), "N4"))
                            GetJumlahBags = Dr("Jumlah_Bags")
                            GetDataSatuanKecil = Dr("Satuan_Barang")
                            GetDataSatuanBesar = Dr("Satuan")
                            GetWarna = Dr("Warna")
                            GetDataUrutOto = Dr("urut_oto")
                            GetRakTujuan = Dr("Id_Wms_Tujuan")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Kosong()
                            Exit Sub
                        End If
                    End Using


                    SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
                    SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
                    SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' and c.urut_oto = '" & GetDataUrutOto & "'  "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            If General_Class.CekNULL(Dr("status")) <> "" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
                    SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
                    SQL = SQL & "id_wms_warehouse_position = '" & GetRakTujuan & "' "
                    SQL = SQL & "order by nomor_urut "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            GetPalletTujuan = dr("nomor_urut")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                            Exit Sub
                        End If
                    End Using

                    '=============================================================================================
                    '=============================================================================================
                    '=======================================================================================


                    '====================================
                    '=       CONVERT SATUAN KECIL       =
                    '====================================
                    Dim nilai_kecildetail As Double = 0
                    SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & GetDataKdBrg & "', '" & GetDataSatuanBesar & "',"
                    SQL = SQL & "'" & GetDataSatuanKecil & "', '" & GetDataJmlEstimasi & "' ) as hasil"
                    Using Dr1 = OpenTrans(SQL)
                        If Dr1.Read Then
                            If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                Dr1.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("data konversi satuan kirim tidak ada ")
                                Exit Sub
                            End If

                            nilai_kecildetail = Dr1("hasil")
                        Else
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If
                    End Using

                    '============================
                    '=       POTONG STOCK       =
                    '============================

                    Dim nilai_persediaan_min As Double = 0
                    SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
                    SQL = SQL & "Kode_Stock_Owner='" & GetDataLokasi & "' and Kode_Barang='" & GetDataKdBrg & "' "
                    SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            nilai_persediaan_min = dr("rp_persediaan_min")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim Nama As String = ""
                    'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
                    SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                    SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            Nama = dr("Kode_Barang")
                            If dr("good_stock") < nilai_kecildetail Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf dr("Jumlah_Bags") < GetJumlahBags Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                dr.Close()
                                SQL = "update barang set Good_Stock = Good_Stock - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & GetJumlahBags & " "
                                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                                SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
                                ExecuteTrans(SQL)
                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                    SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                    SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If dr("jumlah") < nilai_kecildetail Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf dr("Jumlah_Bags") < GetJumlahBags Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                dr.Close()
                                SQL = "update barang_sn set jumlah = jumlah - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & GetJumlahBags & " "
                                SQL = SQL & "where Kode_Stock_Owner='" & GetDataLokasi & "' and Kode_Barang='" & GetDataKdBrg & "' "
                                SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
                                ExecuteTrans(SQL)
                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    '====================================
                    '=       CEK KESESUAIAN STOCK       =
                    '====================================
                    SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                    SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                    SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                    SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                    SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                    SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetDataLokasi & "' "
                    SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
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


                    '==============================
                    '=       INSERT SN BARU       =
                    '==============================

                    Dim hargaIsn As String = ""
                    Dim namaBarang As String = ""
                    Dim warnaLama As String = ""

                    'Ambil Data Lama
                    SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                    SQL = SQL & "from barang_sn a, barang b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                    SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Kode_Stock_Owner='" & GetDataLokasi & "' "
                    SQL = SQL & "and a.Kode_Barang ='" & GetDataKdBrg & "' "
                    SQL = SQL & "and a.Serial_Number='" & GetDataBrgSN & "' "
                    'SQL = SQL & "and a.Jumlah <> 0 "
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                            QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                            batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                            namaBarang = General_Class.CekNULL(Dr("Nama"))
                            expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                            warnaLama = General_Class.CekNULL(Dr("warna"))
                        Loop
                    End Using

                    'GENERATE SN BARU
                    Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                    Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                    Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                    Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

                    'INSERT BARANG SN BARU  
                    SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                    SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
                    SQL = SQL & "select Kode_Perusahaan, '" & GetDataLokasi & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & GetJumlahBags & ", "
                    SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & GetRakTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                    SQL = SQL & "Kode_Unik_Asal, '" & GetPalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, 'Y' "
                    SQL = SQL & "from Barang_SN "
                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Stock_Owner='" & GetDataLokasi & "' "
                    SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                    SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "' "
                    ExecuteTrans(SQL)

                    '============================
                    '=       TAMBAH STOCK       =
                    '============================

                    SQL = "update barang set Good_Stock= Good_Stock + Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & GetJumlahBags & " "
                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                    SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
                    ExecuteTrans(SQL)

                    'CEK KESESUAIAN STOCK
                    SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                    SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                    SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                    SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                    SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                    SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetDataLokasi & "' "
                    SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
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


#Region "Jurnal"

                    'dari
                    Dim inisial_faktur_dari As String = ""
                    Dim akun_persediaan_dari As String = ""
                    Dim akun_persediaan_tujuan As String = ""

                    SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & GetDataLokasi & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            'akun_persediaan_dari = Dr("persediaan")
                            inisial_faktur_dari = Dr("inisial_faktur")

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select c.akun_Persediaan "
                    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and b.kode_stock_owner = '" & GetDataLokasi & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            akun_persediaan_dari = Dr("akun_Persediaan")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select c.akun_Persediaan "
                    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and b.kode_stock_owner = '" & GetDataLokasi & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            akun_persediaan_tujuan = Dr("akun_Persediaan")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim Kode_voucher As String = ""
                    Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                    Dim pagenumber As Integer = 1

                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                    SQL = SQL & "'" & Kode_voucher & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & GetDataKodeTransfer & "', '', "
                    SQL = SQL & "'-', '" & UserID & "')"
                    ExecuteTrans(SQL)

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                              Strings.Mid(akun_persediaan_dari, 2, 1),
                              Strings.Mid(Ganti(akun_persediaan_dari), 3),
                              KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, "0", nilai_persediaan_min, pagenumber, GetDataLokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                             Strings.Mid(akun_persediaan_tujuan, 2, 1),
                             Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                             KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, nilai_persediaan_min, "0", pagenumber, GetDataLokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("debit") <> Dr("kredit") Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

#End Region

                    SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
                    SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
                    SQL = SQL & "'" & KodePerusahaan & "', '" & GetDataKodeTransfer & "', '" & GetDataUrutOto & "', "
                    SQL = SQL & "'" & GetPalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', "
                    SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                    SQL = SQL & "'" & Kode_voucher & "', '" & GetJumlahBags & "') "
                    ExecuteTrans(SQL)

                    SQL = "update N_EMI_Transaksi_Transfer_Waste_Det set  "
                    SQL = SQL & "Selesai = 'Y' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and urut_oto = '" & GetDataUrutOto & "' "
                    ExecuteTrans(SQL)


                    SQL = "update barang_sn set Flag_Sdh_Pengajuan_Waste ='Y' "
                    SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
                    SQL &= $"and Flag_Pengajuan_Waste = 'Y' "
                    SQL &= $"and Flag_Sdh_Pengajuan_Waste is null "
                    SQL &= $"and serial_number = '{arr_Sn(Indxx)}' "
                    ExecuteTrans(SQL)


                Next


            Next



            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data Berhasil Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()


    End Sub


    Private Sub Btn_Set_Barcode_Click(sender As Object, e As EventArgs) Handles Btn_Set_Barcode.Click
        N_EMI_SD_Transaksi_Bypass_Pemusnahan_Barang_Per_Barcode.ShowDialog()
    End Sub



End Class
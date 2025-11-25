Public Class N_EMI_SD_Retur_DO_Reseller_Sementara

    Protected Friend Gudang, urut, urut_oto, hrg, discp, metper As String

    Dim item_KdBarang, item_NmBarang, item_Barcode, item_Jumlah, Item_MaxRetur, item_Satuan, item_JumlahInsert As String

    Dim Cell_KdBarang As Integer = 0
    Dim Cell_NmBarang As Integer = 1
    Dim Cell_Barcode As Integer = 2
    Dim Cell_Jumlah As Integer = 3
    Dim Cell_MaxRetur As Integer = 4
    Dim Cell_Satuan As Integer = 5
    Dim Cell_Chklist As Integer = 6
    Dim Cell_JumlahInsert As Integer = 7




    Private Sub N_EMI_SD_Retur_DO_Reseller_Sementara_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Load_Barcode()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Load_Barcode()
    End Sub

    Private Sub Get_Data_DGV(ByVal index As Integer)

        item_KdBarang = Dgv_Data_Barcode.Rows(index).Cells(Cell_KdBarang).Value
        item_NmBarang = Dgv_Data_Barcode.Rows(index).Cells(Cell_NmBarang).Value
        item_Barcode = Dgv_Data_Barcode.Rows(index).Cells(Cell_Barcode).Value
        item_Jumlah = Dgv_Data_Barcode.Rows(index).Cells(Cell_Jumlah).Value
        Item_MaxRetur = Dgv_Data_Barcode.Rows(index).Cells(Cell_MaxRetur).Value
        item_Satuan = Dgv_Data_Barcode.Rows(index).Cells(Cell_Satuan).Value
        item_JumlahInsert = Dgv_Data_Barcode.Rows(index).Cells(Cell_JumlahInsert).Value


    End Sub


    Private Sub Load_Barcode()

        If Txt_No_DO.Text.Trim.Length = 0 Then
            MessageBox.Show("No DO Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim Total As Double = 0

            Dgv_Data_Barcode.Rows.Clear()
            SQL = "select a.Kode_Barang, a.Nama as Nama_Barang, (e.Qr_Code+'-'+e.Kode_Unik_Berjalan) as Barcode, d.Jumlah, a.satuan, "

            SQL = SQL & "isnull(( d.Jumlah - isnull(( "
            SQL = SQL & "select isnull(sum(y.Good_Stock + y.Bad_Stock), 0) as Jumlah_Pernah_Retur "
            SQL = SQL & "from retur_do_sementara z "
            SQL = SQL & "inner join detail_r_do_sementara x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Retur_Jual_Sementara = x.No_Retur_Jual_Sementara "
            SQL = SQL & "inner join det_r_do_sementara y on x.kode_perusahaan = y.kode_perusahaan and x.No_Retur_Jual_Sementara = y.No_Retur_Jual_Sementara and x.No_Urut = y.Urut_Detail "
            SQL = SQL & "where z.Status is null "
            SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and x.Kode_Stock_Owner = a.Kode_Stock_Owner "
            SQL = SQL & "and x.Kode_Barang = a.Kode_Barang "
            SQL = SQL & "and x.Urut_DO = a.urut_oto "
            SQL = SQL & "and y.Barcode = (e.Qr_Code+'-'+e.Kode_Unik_Berjalan) "
            SQL = SQL & "group by x.kode_barang), 0)), 0) as Max_Retur "

            SQL = SQL & "from sub_invoice a "
            SQL = SQL & "inner join DO_New b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_do = b.No_DO     "
            SQL = SQL & "inner join Detail_DO_New c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_DO = c.No_DO "
            SQL = SQL & "and a.kode_stock_owner = c.Kode_Stock_Owner and a.Kode_barang = c.Kode_Barang "
            SQL = SQL & "inner join Det_DO_New d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_DO = d.No_Faktur and c.Urut_Oto = d.No_Urut_DO and c.No_Urut = d.No_Urut_Det_Penj "
            SQL = SQL & "inner join Barang_SN e on d.kode_perusahaan = e.kode_perusahaan and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang and d.Serial_Number = e.Serial_Number "
            SQL = SQL & "where b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_do = '" & Txt_No_DO.Text & "' "
            SQL = SQL & "and a.Kode_barang = '" & Txt_KdBarang.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Data_Barcode.Rows.Add(1)
                            Dgv_Data_Barcode.Rows(i).Cells(Cell_KdBarang).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_Data_Barcode.Rows(i).Cells(Cell_NmBarang).Value = .Rows(i).Item("Nama_Barang")
                            Dgv_Data_Barcode.Rows(i).Cells(Cell_Barcode).Value = .Rows(i).Item("Barcode")
                            Dgv_Data_Barcode.Rows(i).Cells(Cell_Jumlah).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah"))), "N4")
                            Dgv_Data_Barcode.Rows(i).Cells(Cell_MaxRetur).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Max_Retur"))), "N4")
                            Dgv_Data_Barcode.Rows(i).Cells(Cell_Satuan).Value = .Rows(i).Item("satuan")

                            Dgv_Data_Barcode.Rows(i).Cells(Cell_JumlahInsert).Value = ""
                            Dgv_Data_Barcode.Rows(i).Cells(Cell_JumlahInsert).ReadOnly = True

                            Total += Val(HilangkanTanda(.Rows(i).Item("Jumlah")))

                        Next
                    End If
                End With
            End Using

            Txt_Total.Text = Format(Total, "N4")



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub Dgv_Data_Barcode_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data_Barcode.CellEndEdit
        If Dgv_Data_Barcode.Rows.Count = 0 Then Exit Sub

        Dim RowsIndex As Integer = Dgv_Data_Barcode.CurrentRow.Index

        If Dgv_Data_Barcode.CurrentRow.Cells(Cell_Chklist).Value = "True" Then

            If Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).ReadOnly = True Then
                Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).ReadOnly = False
                Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value = ""
            End If

            If Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value <> "" OrElse Val(Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value) <> 0 Then

                If Not IsNumeric(Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value) Then
                    Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value = ""
                    Hitung_Grand()
                    Exit Sub
                End If

            End If

            Dim JumlahStockSisa As Double = Val(HilangkanTanda(Dgv_Data_Barcode.CurrentRow.Cells(Cell_MaxRetur).Value))
            Dim JumlahInsert As Double = Val(HilangkanTanda(Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value))

            If JumlahInsert < 0 Then
                Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value = ""
                Hitung_Grand()
                Exit Sub
            End If

            If JumlahInsert > JumlahStockSisa Then
                MessageBox.Show("Jumlah Insert Tidak Boleh Lebih Besar Dari Maksimal Retur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value = ""
                Hitung_Grand()
                Exit Sub
            End If

            Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value = Format(JumlahInsert, "N4")


            Hitung_Grand()

        Else

            Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).Value = ""
            Dgv_Data_Barcode.CurrentRow.Cells(Cell_JumlahInsert).ReadOnly = True
            Hitung_Grand()



        End If




    End Sub



    Private Sub Hitung_Grand()
        If Dgv_Data_Barcode.Rows.Count = 0 Then Exit Sub

        Dim Totalnsert As Double = 0
        For i As Integer = 0 To Dgv_Data_Barcode.Rows.Count - 1
            If Dgv_Data_Barcode.Rows(i).Cells(Cell_Chklist).Value = "True" Then
                Get_Data_DGV(i)

                Totalnsert += Val(HilangkanTanda(item_JumlahInsert))

            End If
        Next

        Txt_Total_Insert.Text = Format(Totalnsert, "N4")

    End Sub

    Private Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_Insert.Click
        If Dgv_Data_Barcode.Rows.Count = 0 Then Exit Sub

        If MessageBox.Show("Yakin Ingin Melakukan Insert Barcode Ini", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub


        For i As Integer = 0 To Dgv_Data_Barcode.Rows.Count - 1

            If Dgv_Data_Barcode.Rows(i).Cells(Cell_Chklist).Value <> "True" Then
                Continue For
            End If

            Get_Data_DGV(i)

            If Not Dgv_Data_Barcode.Rows(i).Cells(Cell_Chklist).Value = "True" Then
                Continue For
            End If

            If Val(item_JumlahInsert) = 0 Then
                MessageBox.Show($"Barcode {item_Barcode} Harus Diinput!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            For j As Integer = 0 To Retur_DO_Reseller_Sementara.Lv_Hidden_Data.Items.Count - 1
                If item_KdBarang.Trim.ToUpper = Retur_DO_Reseller_Sementara.Lv_Hidden_Data.Items(j).SubItems(1).Text.Trim.ToUpper And
                    item_NmBarang.Trim.ToUpper = Retur_DO_Reseller_Sementara.Lv_Hidden_Data.Items(j).SubItems(2).Text.Trim.ToUpper And
                    item_Barcode.Trim.ToUpper = Retur_DO_Reseller_Sementara.Lv_Hidden_Data.Items(j).SubItems(10).Text.Trim.ToUpper Then

                    MessageBox.Show($"Barcode {item_Barcode} sudah anda masukkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Retur_DO_Reseller_Sementara.kosongbawah()
                    Exit Sub

                End If

            Next

        Next


        For i As Integer = 0 To Dgv_Data_Barcode.Rows.Count - 1
            If Dgv_Data_Barcode.Rows(i).Cells(Cell_Chklist).Value <> "True" Then
                Continue For
            End If

            Get_Data_DGV(i)


            Dim foundIndex As Integer = -1
            For j As Integer = 0 To Retur_DO_Reseller_Sementara.ListView2.Items.Count - 1

                Dim item = Retur_DO_Reseller_Sementara.ListView2.Items(j)

                If Gudang.Trim.ToUpper() = item.Text.Trim.ToUpper() AndAlso
                   item_KdBarang.Trim.ToUpper() = item.SubItems(1).Text.Trim.ToUpper() AndAlso
                   urut = item.SubItems(4).Text AndAlso
                   urut_oto = item.SubItems(5).Text Then

                    foundIndex = j
                    Exit For
                End If
            Next

            Dim y_hrg As Double = Val(HilangkanTanda(hrg))
            Dim y_disc As Double = Val(HilangkanTanda(Format(Val(discp), "N2")))
            Dim y_jml As Double = Val(HilangkanTanda(item_JumlahInsert))
            Dim subttl As Double


            If foundIndex <> -1 Then

                If metper = "A" Then
                    subttl = (hrg * Val(y_jml)) - (hrg * Val(y_jml) * discp / 100)
                ElseIf metper = "B" Then
                    subttl = Hitung_Subtotal(y_hrg, y_disc, y_jml)
                Else
                    MessageBox.Show("error perhitungan")
                End If


                Retur_DO_Reseller_Sementara.ListView2.Items(foundIndex).SubItems(3).Text = Format(Val(HilangkanTanda(Retur_DO_Reseller_Sementara.ListView2.Items(foundIndex).SubItems(3).Text)) + Val(y_jml), "N4")
                Retur_DO_Reseller_Sementara.ListView2.Items(foundIndex).SubItems(8).Text = Format(Val(HilangkanTanda(Retur_DO_Reseller_Sementara.ListView2.Items(foundIndex).SubItems(8).Text)) + subttl, "N4")


            Else
                Dim lv As New ListViewItem
                lv = Retur_DO_Reseller_Sementara.ListView2.Items.Add(Gudang) '0
                lv.SubItems.Add(Trim(item_KdBarang)) '1
                lv.SubItems.Add(item_NmBarang) '2
                lv.SubItems.Add(Format(Val(y_jml), "N4")) '3
                lv.SubItems.Add(urut) '4
                lv.SubItems.Add(urut_oto) '5
                lv.SubItems.Add(Format(Val(HilangkanTanda(hrg)), "N4")) '6
                lv.SubItems.Add(discp) '7



                If metper = "A" Then
                    subttl = (hrg * Val(y_jml)) - (hrg * Val(y_jml) * discp / 100)
                ElseIf metper = "B" Then
                    subttl = Hitung_Subtotal(y_hrg, y_disc, y_jml)
                Else
                    MessageBox.Show("error perhitungan")
                End If

                lv.SubItems.Add(Format(subttl, "N4")) '8
                lv.SubItems.Add(metper) '9
            End If

            '=========================
            '=     ADD LV HIDDEN     =
            '=========================

            Dim lv2 As New ListViewItem
            lv2 = Retur_DO_Reseller_Sementara.Lv_Hidden_Data.Items.Add(Gudang) '0
            lv2.SubItems.Add(Trim(item_KdBarang)) '1
            lv2.SubItems.Add(item_NmBarang) '2
            lv2.SubItems.Add(Format(Val(y_jml), "N4")) '3
            lv2.SubItems.Add(urut) '4
            lv2.SubItems.Add(urut_oto) '5
            lv2.SubItems.Add(Format(Val(HilangkanTanda(hrg)), "N4")) '6
            lv2.SubItems.Add(discp) '7
            lv2.SubItems.Add(Format(subttl, "N4")) '8
            lv2.SubItems.Add(metper) '9
            lv2.SubItems.Add(item_Barcode) '10



        Next


        Me.Close()
    End Sub













End Class


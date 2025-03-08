Public Class Emi_Validasi_HPP_Produksi
    Dim LvBatch As String
    Dim LvQty_Rw_Formula As String
    Dim LvQty_Rw_Dosing As String
    Dim LvSatuan As String
    Dim LvFG_Seharus As String
    Dim LvTotal_Bahan_Dosing As String
    Dim LvTotal_Pack_Dosing As String
    Dim LvTotal_Biaya_Dosing As String
    Dim LvNilai_Loss_Pro As String
    Dim LvNilai_HPP_PerPcs As String
    Dim LvJml_FG_Real As String
    Dim LvTotal_HPP_FG_Real As String

    Dim CellBatch As Integer = 0
    Dim CellQty_Rw_Formula As Integer = 1
    Dim CellQty_Rw_Dosing As Integer = 2
    Dim CellSatuan As Integer = 3
    Dim CellFG_Seharus As Integer = 4
    Dim CellTotal_Bahan_Dosing As Integer = 5
    Dim CellTotal_Pack_Dosing As Integer = 6
    Dim CellTotal_Biaya_Dosing As Integer = 7
    Dim CellNilai_Loss_Pro As Integer = 8
    Dim CellNilai_HPP_PerPcs As Integer = 9
    Dim CellJml_FG_Real As Integer = 10
    Dim CellTotal_HPP_FG_Real As Integer = 11

    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If

        Return hasil
    End Function
    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvBatch = CekNothing(DataGridView1.Rows(No_Index).Cells(CellBatch).Value)
        LvQty_Rw_Formula = CekNothing(DataGridView1.Rows(No_Index).Cells(CellQty_Rw_Formula).Value)
        LvQty_Rw_Dosing = CekNothing(DataGridView1.Rows(No_Index).Cells(CellQty_Rw_Dosing).Value)
        LvSatuan = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSatuan).Value)
        LvFG_Seharus = CekNothing(DataGridView1.Rows(No_Index).Cells(CellFG_Seharus).Value)
        LvTotal_Bahan_Dosing = CekNothing(DataGridView1.Rows(No_Index).Cells(CellTotal_Bahan_Dosing).Value)
        LvTotal_Pack_Dosing = CekNothing(DataGridView1.Rows(No_Index).Cells(CellTotal_Pack_Dosing).Value)
        LvTotal_Biaya_Dosing = CekNothing(DataGridView1.Rows(No_Index).Cells(CellTotal_Biaya_Dosing).Value)
        LvNilai_Loss_Pro = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNilai_Loss_Pro).Value)
        LvNilai_HPP_PerPcs = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNilai_HPP_PerPcs).Value)
        LvJml_FG_Real = CekNothing(DataGridView1.Rows(No_Index).Cells(CellJml_FG_Real).Value)
        LvTotal_HPP_FG_Real = CekNothing(DataGridView1.Rows(No_Index).Cells(CellTotal_HPP_FG_Real).Value)
    End Sub

    Private Sub Emi_Validasi_HPP_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            DataGridView1.Rows.Clear()
            SQL = "select a.Proses,a.Jumlah_Formula,a.Jumlah_Dosing,a.Satuan,a.Jumlah_Dosing_Pcs,a.Total_Bahan_Baku,a.Total_Packaging,a.Total_Biaya_Produksi,"
            SQL = SQL & "a.Nilai_Loss_Production,a.Jumlah_Terpakai,a.Persen_Loss_Production "
            SQL = SQL & "from Emi_Production_Results_HPP a,Emi_Production_Results b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Production_Order = '" & Txt_NoSplitProduksi.Text & "' order by a.Proses "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        DataGridView1.Rows.Add(1)
                        TextBox3.Text = Format(.Rows(i).Item("Persen_Loss_Production"), "N0")

                        DataGridView1.Rows(i).Cells(CellBatch).Value = "DOSHING " & .Rows(i).Item("Proses")
                        DataGridView1.Rows(i).Cells(CellQty_Rw_Formula).Value = Format(.Rows(i).Item("Jumlah_Formula"), "N0")
                        DataGridView1.Rows(i).Cells(CellQty_Rw_Dosing).Value = Format(.Rows(i).Item("Jumlah_Dosing"), "N0")
                        DataGridView1.Rows(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")
                        DataGridView1.Rows(i).Cells(CellFG_Seharus).Value = Format(.Rows(i).Item("Jumlah_Dosing_Pcs"), "N0")
                        DataGridView1.Rows(i).Cells(CellTotal_Bahan_Dosing).Value = Format(.Rows(i).Item("Total_Bahan_Baku"), "N0")
                        DataGridView1.Rows(i).Cells(CellTotal_Pack_Dosing).Value = Format(.Rows(i).Item("Total_Packaging"), "N0")
                        DataGridView1.Rows(i).Cells(CellTotal_Biaya_Dosing).Value = Format(.Rows(i).Item("Total_Biaya_Produksi"), "N0")
                        DataGridView1.Rows(i).Cells(CellNilai_Loss_Pro).Value = Format(.Rows(i).Item("Nilai_Loss_Production"), "N0")
                        DataGridView1.Rows(i).Cells(CellNilai_HPP_PerPcs).Value = Format(.Rows(i).Item("Jumlah_Dosing"), "N0") 'blm ok
                        DataGridView1.Rows(i).Cells(CellJml_FG_Real).Value = Format(.Rows(i).Item("Jumlah_Terpakai"), "N0")
                        DataGridView1.Rows(i).Cells(CellTotal_HPP_FG_Real).Value = Format(.Rows(i).Item("Jumlah_Dosing"), "N0") 'blm ok

                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungSelisih()
    End Sub

    Private Sub Btn_Validasi_Click(sender As Object, e As EventArgs) Handles Btn_Validasi.Click
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("data tidak ada.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim pertanyaan As String = MessageBox.Show("Yakin ingin validasi?", "Validasi HPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "select a.Status as status_split,a.Flag_Val_HPP_Produksi,b.Status as status_results "
            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Production_Results b "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Txt_NoSplitProduksi.Text & "' "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Production_Order "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("status_split")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data split order ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Val_HPP_Produksi")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data split order ini sudah divalidasi hpp produksi sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("status_results")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data result produksi ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using


#Region "Pengembalian Packaging"

            '==============================
            '=     GET DATA PACKAGING     =
            '==============================
            SQL = "select a.Kode_Perusahaan, a.No_Transaksi, a.No_Production_Order, b.Kode_Stock_Owner, b.Kode_Barang, c.Serial_Number, c.Nilai, c.jumlah_pakai "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Packaging_Detail b, Emi_Production_Results_Packaging_Det c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Urut = c.No_Urut_Detail "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplitProduksi.Text & "' "
            SQL = SQL & "order by c.Urut "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            'HANDLE NULL
                            If General_Class.CekNULL(.Rows(i).Item("jumlah_pakai")) = "" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesahalan : Jumlah Pakai = NULL ", "Validasi HPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(.Rows(i).Item("Nilai")) = "" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesahalan : Nilai = NULL ", "Validasi HPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            Dim IsiPerBags As Double = 0
                            '=============================================
                            '=     CEK APAKAH ORIGINAL BAGS ATAU NON     =
                            '=============================================
                            SQL = "select Isi_Per_Bags, Good_Stock, Jumlah_Bags, Jenis_Kemasan from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("Isi_Per_Bags") = 0 Then
                                        IsiPerBags = 0
                                    Else
                                        IsiPerBags = Val(Ds1.Tables("MyTable").Rows(0).Item("Isi_Per_Bags"))
                                    End If

                                End If
                            End Using


                            If (Val(HilangkanTanda(.Rows(i).Item("Nilai"))) - Val(HilangkanTanda(.Rows(i).Item("jumlah_pakai")))) < 0 Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Ada Masalah Pada Packaging yang di pakai", "Validasi HPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            '==============================
                            '=     MENGEMBALIKAN STOCK    =
                            '==============================

                            Dim SelisihStock As Double = Val(HilangkanTanda(.Rows(i).Item("Nilai"))) - Val(HilangkanTanda(.Rows(i).Item("jumlah_pakai")))

                            Dim jumlahBags As Double = 0
                            If Not IsiPerBags = 0 Then
                                jumlahBags = Val(HilangkanTanda(SelisihStock)) / Val(HilangkanTanda(IsiPerBags))
                            End If

                            '-== UPDATE BARANG SN ==-'
                            SQL = "update barang_sn set Jumlah = Jumlah + " & Val(HilangkanTanda(SelisihStock)) & ", "
                            SQL = SQL & "Jumlah_Bags = Jumlah_Bags + " & Math.Floor(jumlahBags) & " "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' "
                            SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                            SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
                            ExecuteTrans(SQL)

                            '-== UPDATE BARANG ==-'
                            SQL = " update barang set Good_Stock = Good_Stock + " & Val(HilangkanTanda(SelisihStock)) & ", "
                            SQL = SQL & "Jumlah_Bags = Jumlah_Bags + " & Math.Floor(jumlahBags) & " "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' "
                            SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                            ExecuteTrans(SQL)


                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds2 = BindingTrans(SQL)
                                With Ds2.Tables("MyTable")
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds2.Tables("MyTable").Rows(0).Item("good_stock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Stock Tidak Sesuai . . ! !S", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

                        Next
                    End If
                End With
            End Using


#End Region



            SQL = "update Emi_Split_Production_Order set Flag_Val_HPP_Produksi = 'Y',Tgl_Val_HPP_Produksi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            SQL = SQL & "Jam_Val_HPP_Produksi = '" & Format(tgl_skg, "HH:mm:ss") & "',UserId_Val_HPP_Produksi = '" & UserID & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & Txt_NoSplitProduksi.Text & "' "
            ExecuteTrans(SQL)



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data berhasil divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        EMI_Display_Validasi_HPP_Produksi.Button1_Click(Btn_Validasi, e)
        Me.Close()
    End Sub


    Private Sub HitungSelisih()

    End Sub
End Class

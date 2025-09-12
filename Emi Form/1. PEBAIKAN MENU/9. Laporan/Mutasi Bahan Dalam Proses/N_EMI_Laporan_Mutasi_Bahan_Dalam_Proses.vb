Imports excel = Microsoft.Office.Interop.Excel
Public Class N_EMI_Laporan_Mutasi_Bahan_Dalam_Proses


    Dim Switch_Autocomplete As Boolean = False

    Dim arr_Id_Group_Jenis As New ArrayList
    Private Sub N_EMI_Laporan_Mutasi_Bahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("NamaBarang", 250, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details

        Try
            OpenConn()

            Cmb_Jenis_Laporan.Items.Clear()
            Cmb_Jenis_Laporan.Items.Add("QTY")
            If CekButtonRole("Laporan_Saldo_Mutasi_Bahan_Dalam_Proses") = "Y" Then
                Cmb_Jenis_Laporan.Items.Add("SALDO")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear()
            Cmb_Lokasi.Items.Add(OpsiSeluruh)
            SQL = "select kode_Stock_Owner from Stock_Owner_Gudang where kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("kode_Stock_Owner"))
                Loop
            End Using

            Cmb_Group_Jenis.Items.Clear() : arr_Id_Group_Jenis.Clear()
            Cmb_Group_Jenis.Items.Add(OpsiSeluruh) : arr_Id_Group_Jenis.Add(OpsiSeluruh)
            SQL = "select Id_Group_Jenis, Kode_Group_Jenis from EMI_Group_Jenis where Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Group_Jenis.Items.Add(Dr("Kode_Group_Jenis")) : arr_Id_Group_Jenis.Add(Dr("Id_Group_Jenis"))
                Loop
            End Using
            ' INI BELUM SELESAI


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Barcode.Items.Clear()
        Cmb_Barcode.Items.Add(OpsiSeluruh)
        Cmb_Barcode.Items.Add("Barcode")

        Cmb_Filter_by.Items.Clear()
        Cmb_Filter_by.Items.Add("Barcode")
        Cmb_Filter_by.Items.Add("Barang")

        Kosong()
    End Sub

    Private Sub Kosong()
        '
        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
        Cmb_Lokasi.SelectedIndex = 0
        Cmb_Barcode.SelectedIndex = 0
        Cmb_Jenis_Laporan.SelectedIndex = 0
        Cmb_Group_Jenis.SelectedIndex = 0
        Cmb_Filter_by.SelectedIndex = 0

        Switch_Autocomplete = False
        Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh
        Switch_Autocomplete = True

        Dgv_Detail.Rows.Clear()
        Dgv_Rekap.Rows.Clear()

        'Load_DataLV()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub


    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.DroppedDown = True : Cmb_Lokasi.Focus()
            Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.DroppedDown = True : Cmb_Lokasi.Focus()
            Exit Sub
        ElseIf Cmb_Barcode.SelectedIndex = -1 Then
            MessageBox.Show("Filter By Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Barcode.DroppedDown = True : Cmb_Barcode.Focus()
            Exit Sub
        End If

        Load_DataLV(Filter:=True)
    End Sub

    Private Sub Load_DataLV(ByVal Optional Filter As Boolean = False)
        Try
            OpenConn()

            Try
                OpenConn()

                If Cmb_Filter_by.SelectedIndex = 0 Then
                    SQL = "exec EMI_Mutasi_Bahan_Dalam_Proses '" & KodePerusahaan & "', '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                    ExecuteTrans(SQL)
                ElseIf Cmb_Filter_by.SelectedIndex = 1 Then
                    SQL = "exec EMI_Mutasi_Bahan_Dalam_Proses_Barang '" & KodePerusahaan & "', '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                    ExecuteTrans(SQL)
                End If


                If Cmb_Jenis_Laporan.SelectedIndex = 0 Then

                    Dgv_Rekap.Rows.Clear()
                    SQL = ";with cte as( "
                    SQL = SQL & "select a.Kode_perusahaan, a.No_Faktur, a.Tanggal, a.Jam, a.Jenis, a.Keterangan, a.Kode_stock_owner as Lokasi, a.Kode_barang, a.Nama, a.QR, round(Masuk,4) as Masuk, Round(Keluar, 4) as Keluar, "
                    SQL = SQL & "isnull((select round(sum(masuk-Keluar),2) from Data_CutOffTracking3 x "
                    SQL = SQL & "where x.Kode_Barang=a.Kode_Barang and x.QR=a.QR and x.serial_number=a.serial_number and x.ID<=a.ID and x.Kode_Perusahaan=a.Kode_Perusahaan),0) as Sisa, ID, c.Kode_Group_Jenis, b.Id_Group_Jenis "
                    SQL = SQL & "from Data_CutOffTracking3 a, barang b, EMI_Group_Jenis c  "

                    SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and b.Id_Group_Jenis = c.Id_Group_Jenis "
                    SQL = SQL & ") "

                    SQL = SQL & "select Kode_perusahaan, no_faktur, tanggal, jam, jenis, isnull(keterangan, '-') as keterangan, isnull(lokasi, '-') as lokasi, isnull(kode_barang, '-') as kode_barang,  "
                    SQL = SQL & "isnull(nama, '-') as Nama_Barang, isnull(Masuk, 0) as Masuk, isnull(Keluar, 0) as Keluar, isnull(Sisa, 0) as Sisa, ID, QR, Kode_Group_Jenis, Id_Group_Jenis "
                    SQL = SQL & "from cte "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    If Filter Then
                        SQL = SQL & "and tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                        If Cmb_Lokasi.SelectedIndex > 0 Then
                            SQL = SQL & "and lokasi = '" & Cmb_Lokasi.Text & "' "
                        End If

                        If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                            SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text & "' "
                        End If

                        If Cmb_Barcode.SelectedIndex > 0 Then
                            If Not Txt_Barcode.Text.Trim.Length = 0 Then
                                SQL = SQL & "and QR = '" & Txt_Barcode.Text & "' "
                            Else
                                CloseConn()
                                MessageBox.Show("Barcode Tidak Boleh Kosong")
                                Txt_Barcode.Focus() : Exit Sub
                            End If

                        End If

                        If Cmb_Group_Jenis.SelectedIndex > 0 Then
                            SQL = SQL & "and Id_Group_Jenis = '" & arr_Id_Group_Jenis(Cmb_Group_Jenis.SelectedIndex) & "' "
                        End If
                    End If
                    SQL = SQL & "order by Nama, QR, ID "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For i As Integer = 0 To .Rows.Count - 1

                                    Dgv_Rekap.Rows.Add(1)
                                    Dgv_Rekap.Rows(i).Cells(0).Value = .Rows(i).Item("No_Faktur")
                                    Dgv_Rekap.Rows(i).Cells(1).Value = Format(CDate(.Rows(i).Item("Tanggal")), "dd MMM yyyy")
                                    Dgv_Rekap.Rows(i).Cells(2).Value = .Rows(i).Item("Jam")
                                    Dgv_Rekap.Rows(i).Cells(3).Value = .Rows(i).Item("Jenis")
                                    Dgv_Rekap.Rows(i).Cells(4).Value = .Rows(i).Item("Keterangan")
                                    Dgv_Rekap.Rows(i).Cells(5).Value = .Rows(i).Item("lokasi")
                                    Dgv_Rekap.Rows(i).Cells(6).Value = .Rows(i).Item("Kode_Group_Jenis")
                                    Dgv_Rekap.Rows(i).Cells(7).Value = .Rows(i).Item("kode_barang")
                                    Dgv_Rekap.Rows(i).Cells(8).Value = .Rows(i).Item("Nama_Barang")
                                    Dgv_Rekap.Rows(i).Cells(9).Value = .Rows(i).Item("QR")
                                    Dgv_Rekap.Rows(i).Cells(10).Value = Format(.Rows(i).Item("Masuk"), "N2")
                                    Dgv_Rekap.Rows(i).Cells(11).Value = Format(.Rows(i).Item("Keluar"), "N2")
                                    Dgv_Rekap.Rows(i).Cells(12).Value = Format(.Rows(i).Item("Sisa"), "N2")


                                Next
                            End If
                        End With
                    End Using

                    Dgv_Detail.Rows.Clear()
                    SQL = ";WITH cte AS ( SELECT a.kode_perusahaan, a.No_Faktur, a.Tanggal, a.Jam, a.Jenis, a.Keterangan, c.Kode_Group_Jenis, a.Kode_stock_owner AS Lokasi, a.Kode_barang, a.Nama, a.QR, ROUND(Masuk,4) - ROUND(Keluar,4) AS Nilai  "

                    SQL = SQL & "FROM Data_CutOffTracking3 a, barang b, EMI_Group_Jenis c "
                    SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                    SQL = SQL & "and b.Id_Group_Jenis = c.Id_Group_Jenis) "

                    SQL = SQL & ",cte_b as( SELECT kode_perusahaan, Kode_Group_Jenis, Lokasi, kode_barang, Nama, QR, ISNULL([Saldo Awal],0) AS [Saldo Awal], ISNULL([Barang Masuk],0) AS [Barang Masuk], "
                    SQL = SQL & "ISNULL([Terima Transfer Stock],0) AS [Terima Transfer Stock], ISNULL([Terima Split Stock],0) AS [Terima Split Stock], "
                    SQL = SQL & "ISNULL([Terima Transfer Material],0) AS [Terima Transfer Material], ISNULL([Penambahan Stock],0) AS [Penambahan Stock], "
                    SQL = SQL & "ISNULL([Retur Produksi],0) AS [Retur Produksi], ISNULL([Penerimaan Produksi 1],0) AS [Penerimaan Produksi 1], "
                    SQL = SQL & "ISNULL([Penerimaan Sisa Produksi 1],0) AS [Penerimaan Sisa Produksi 1], ISNULL([Penerimaan Produksi 2],0) AS [Penerimaan Produksi 2], "
                    SQL = SQL & "ISNULL([Transfer Stock],0) AS [Transfer Stock], ISNULL([Split Stock],0) AS [Split Stock], ISNULL([Transfer Material],0) AS [Transfer Material], "
                    SQL = SQL & "ISNULL([Pengeluaran Stock],0) AS [Pengeluaran Stock], ISNULL([Pengeluaran Bahan Bakar],0) AS [Pengeluaran Bahan Bakar], "
                    SQL = SQL & "ISNULL([Pemakaian Produksi 1],0) AS [Pemakaian Produksi 1], ISNULL([Pemakaian Produksi 2],0) AS [Pemakaian Produksi 2], "
                    SQL = SQL & "ISNULL([Pengeluaran Produksi 2],0) AS [Pengeluaran Produksi 2] "
                    SQL = SQL & "FROM ( SELECT kode_perusahaan, Kode_Group_Jenis, Lokasi, kode_barang, Nama, QR, Jenis  AS JenisKolom, Nilai FROM cte ) src "
                    SQL = SQL & "PIVOT ( SUM(Nilai) FOR JenisKolom IN ( [Saldo Awal], [Barang Masuk], [Transfer Stock], [Terima Transfer Stock], [Split Stock], [Terima Split Stock], [Transfer Material], "
                    SQL = SQL & "[Terima Transfer Material], [Penambahan Stock], [Pengeluaran Stock], [Pengeluaran Bahan Bakar], [Pemakaian Produksi 1], [Retur Produksi], [Penerimaan Produksi 1], "
                    SQL = SQL & "[Penerimaan Sisa Produksi 1], [Pengeluaran Produksi 2], [Penerimaan Produksi 2], [Pemakaian Produksi 2] ) ) p ) "
                    SQL = SQL & "select *, Round( [Saldo Awal]+ [Barang Masuk]+ [Transfer Stock]+ [Terima Transfer Stock]+ [Split Stock]+ [Terima Split Stock]+ [Transfer Material]+ [Terima Transfer Material]+ "
                    SQL = SQL & "[Penambahan Stock]+ [Pengeluaran Stock]+ [Pengeluaran Bahan Bakar]+ [Pemakaian Produksi 1]+ [Retur Produksi]+ [Penerimaan Produksi 1]+ [Penerimaan Sisa Produksi 1]+ "
                    SQL = SQL & "[Pengeluaran Produksi 2]+ [Penerimaan Produksi 2]+ [Pemakaian Produksi 2],4) as Stock from cte_b a "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    If Filter Then
                        If Cmb_Lokasi.SelectedIndex > 0 Then
                            SQL = SQL & "and lokasi = '" & Cmb_Lokasi.Text & "' "
                        End If

                        If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                            SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text & "' "
                        End If

                        If Cmb_Barcode.SelectedIndex > 0 Then
                            If Not Txt_Barcode.Text.Trim.Length = 0 Then
                                SQL = SQL & "and QR = '" & Txt_Barcode.Text & "' "
                            Else
                                CloseConn()
                                MessageBox.Show("Barcode Tidak Boleh Kosong")
                                Txt_Barcode.Focus() : Exit Sub
                            End If

                        End If

                        If Cmb_Group_Jenis.SelectedIndex > 0 Then
                            SQL = SQL & "and Kode_Group_Jenis = '" & Cmb_Group_Jenis.Text & "' "
                        End If
                    End If
                    SQL = SQL & "ORDER BY Nama, QR; "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For i As Integer = 0 To .Rows.Count - 1
                                    Dgv_Detail.Rows.Add(1)
                                    Dgv_Detail.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Group_Jenis")
                                    Dgv_Detail.Rows(i).Cells(1).Value = .Rows(i).Item("Lokasi")
                                    Dgv_Detail.Rows(i).Cells(2).Value = .Rows(i).Item("kode_barang")
                                    Dgv_Detail.Rows(i).Cells(3).Value = .Rows(i).Item("Nama")
                                    Dgv_Detail.Rows(i).Cells(4).Value = .Rows(i).Item("QR")
                                    Dgv_Detail.Rows(i).Cells(5).Value = Format(.Rows(i).Item("Saldo Awal"), "N0")
                                    Dgv_Detail.Rows(i).Cells(6).Value = Format(.Rows(i).Item("Barang Masuk"), "N2")
                                    Dgv_Detail.Rows(i).Cells(7).Value = Format(.Rows(i).Item("Terima Transfer Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(8).Value = Format(.Rows(i).Item("Terima Split Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(9).Value = Format(.Rows(i).Item("Terima Transfer Material"), "N2")
                                    Dgv_Detail.Rows(i).Cells(10).Value = Format(.Rows(i).Item("Penambahan Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(11).Value = Format(.Rows(i).Item("Retur Produksi"), "N2")
                                    Dgv_Detail.Rows(i).Cells(12).Value = Format(.Rows(i).Item("Penerimaan Produksi 1"), "N2")
                                    Dgv_Detail.Rows(i).Cells(13).Value = Format(.Rows(i).Item("Penerimaan Sisa Produksi 1"), "N2")
                                    Dgv_Detail.Rows(i).Cells(14).Value = Format(.Rows(i).Item("Penerimaan Produksi 2"), "N2")
                                    Dgv_Detail.Rows(i).Cells(15).Value = Format(.Rows(i).Item("Transfer Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(16).Value = Format(.Rows(i).Item("Split Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(17).Value = Format(.Rows(i).Item("Transfer Material"), "N2")
                                    Dgv_Detail.Rows(i).Cells(18).Value = Format(.Rows(i).Item("Pengeluaran Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(19).Value = Format(.Rows(i).Item("Pengeluaran Bahan Bakar"), "N2")
                                    Dgv_Detail.Rows(i).Cells(20).Value = Format(.Rows(i).Item("Pemakaian Produksi 1"), "N2")
                                    Dgv_Detail.Rows(i).Cells(21).Value = Format(.Rows(i).Item("Pemakaian Produksi 2"), "N2")
                                    Dgv_Detail.Rows(i).Cells(22).Value = Format(.Rows(i).Item("Pengeluaran Produksi 2"), "N2")
                                    Dgv_Detail.Rows(i).Cells(23).Value = Format(.Rows(i).Item("Stock"), "N2")
                                Next
                            End If
                        End With
                    End Using

                ElseIf Cmb_Jenis_Laporan.SelectedIndex = 1 Then
                    Dgv_Rekap.Rows.Clear()
                    SQL = ";with cte as( "
                    SQL = SQL & "select a.Kode_perusahaan, a.No_Faktur, a.Tanggal, a.Jam, a.Jenis, a.Keterangan, a.Kode_stock_owner as Lokasi, a.Kode_barang, a.Nama, a.QR, round(Masuk,4) as Masuk, Round(Keluar, 4) as Keluar, Masuk_Saldo, Keluar_Saldo, "
                    SQL = SQL & "isnull((select round(sum(masuk-Keluar),2) from Data_CutOffTracking3 x "
                    SQL = SQL & "where x.Kode_Barang=a.Kode_Barang and x.QR=a.QR and x.serial_number=a.serial_number and x.ID<=a.ID and x.Kode_Perusahaan=a.Kode_Perusahaan),0) as Sisa, ID, "

                    SQL = SQL & "isnull((select round(sum(Masuk_Saldo-Keluar_Saldo),2) from Data_CutOffTracking3 x "
                    SQL = SQL & "where x.Kode_Barang=a.Kode_Barang and x.QR=a.QR and x.serial_number=a.serial_number and x.ID<=a.ID and x.Kode_Perusahaan=a.Kode_Perusahaan),0) as Sisa_Saldo, "

                    SQL = SQL & "c.Kode_Group_Jenis, b.Id_Group_Jenis "

                    SQL = SQL & "from Data_CutOffTracking3 a, barang b, EMI_Group_Jenis c  "

                    SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and b.Id_Group_Jenis = c.Id_Group_Jenis "
                    SQL = SQL & ") "

                    SQL = SQL & "select Kode_perusahaan, no_faktur, tanggal, jam, jenis, isnull(keterangan, '-') as keterangan, isnull(lokasi, '-') as lokasi, isnull(kode_barang, '-') as kode_barang,  "
                    SQL = SQL & "isnull(nama, '-') as Nama_Barang, isnull(Masuk, 0) as Masuk, isnull(Keluar, 0) as Keluar, isnull(Sisa, 0) as Sisa, ID, QR, isnull(Masuk_Saldo, 0) as Masuk_Saldo, isnull(Keluar_Saldo, 0) as Keluar_Saldo, isnull(Sisa_Saldo, 0) as Sisa_Saldo, "
                    SQL = SQL & "Kode_Group_Jenis, Id_Group_Jenis "
                    SQL = SQL & "from cte "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    If Filter Then
                        SQL = SQL & "and tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                        If Cmb_Lokasi.SelectedIndex > 0 Then
                            SQL = SQL & "and lokasi = '" & Cmb_Lokasi.Text & "' "
                        End If

                        If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                            SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text & "' "
                        End If

                        If Cmb_Barcode.SelectedIndex > 0 Then
                            If Not Txt_Barcode.Text.Trim.Length = 0 Then
                                SQL = SQL & "and QR = '" & Txt_Barcode.Text & "' "
                            Else
                                CloseConn()
                                MessageBox.Show("Barcode Tidak Boleh Kosong")
                                Txt_Barcode.Focus() : Exit Sub
                            End If

                        End If


                        If Cmb_Group_Jenis.SelectedIndex > 0 Then
                            SQL = SQL & "and Id_Group_Jenis = '" & arr_Id_Group_Jenis(Cmb_Group_Jenis.SelectedIndex) & "' "
                        End If
                    End If
                    SQL = SQL & "order by Nama, QR, ID "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For i As Integer = 0 To .Rows.Count - 1

                                    Dgv_Rekap.Rows.Add(1)
                                    Dgv_Rekap.Rows(i).Cells(0).Value = .Rows(i).Item("No_Faktur")
                                    Dgv_Rekap.Rows(i).Cells(1).Value = Format(CDate(.Rows(i).Item("Tanggal")), "dd MMM yyyy")
                                    Dgv_Rekap.Rows(i).Cells(2).Value = .Rows(i).Item("Jam")
                                    Dgv_Rekap.Rows(i).Cells(3).Value = .Rows(i).Item("Jenis")
                                    Dgv_Rekap.Rows(i).Cells(4).Value = .Rows(i).Item("Keterangan")
                                    Dgv_Rekap.Rows(i).Cells(5).Value = .Rows(i).Item("lokasi")
                                    Dgv_Rekap.Rows(i).Cells(6).Value = .Rows(i).Item("Kode_Group_Jenis")
                                    Dgv_Rekap.Rows(i).Cells(7).Value = .Rows(i).Item("kode_barang")
                                    Dgv_Rekap.Rows(i).Cells(8).Value = .Rows(i).Item("Nama_Barang")
                                    Dgv_Rekap.Rows(i).Cells(9).Value = .Rows(i).Item("QR")
                                    Dgv_Rekap.Rows(i).Cells(10).Value = Format(.Rows(i).Item("Masuk_Saldo"), "N2")
                                    Dgv_Rekap.Rows(i).Cells(11).Value = Format(.Rows(i).Item("Masuk_Saldo"), "N2")
                                    Dgv_Rekap.Rows(i).Cells(12).Value = Format(.Rows(i).Item("Sisa_Saldo"), "N2")


                                Next
                            End If
                        End With
                    End Using

                    Dgv_Detail.Rows.Clear()
                    SQL = ";WITH cte AS ( SELECT a.kode_perusahaan, a.No_Faktur, a.Tanggal, a.Jam, a.Jenis, a.Keterangan, c.Kode_Group_Jenis, a.Kode_stock_owner AS Lokasi, a.Kode_barang, a.Nama, a.QR, ROUND(Masuk_Saldo,4) - ROUND(Keluar_Saldo,4) AS Nilai "

                    SQL = SQL & "FROM Data_CutOffTracking3 a, barang b, EMI_Group_Jenis c "
                    SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                    SQL = SQL & "and b.Id_Group_Jenis = c.Id_Group_Jenis) "

                    SQL = SQL & ",cte_b as( SELECT kode_perusahaan, Kode_Group_Jenis, Lokasi, kode_barang, Nama, QR, ISNULL([Saldo Awal],0) AS [Saldo Awal], ISNULL([Barang Masuk],0) AS [Barang Masuk], "
                    SQL = SQL & "ISNULL([Terima Transfer Stock],0) AS [Terima Transfer Stock], ISNULL([Terima Split Stock],0) AS [Terima Split Stock], "
                    SQL = SQL & "ISNULL([Terima Transfer Material],0) AS [Terima Transfer Material], ISNULL([Penambahan Stock],0) AS [Penambahan Stock], "
                    SQL = SQL & "ISNULL([Retur Produksi],0) AS [Retur Produksi], ISNULL([Penerimaan Produksi 1],0) AS [Penerimaan Produksi 1], "
                    SQL = SQL & "ISNULL([Penerimaan Sisa Produksi 1],0) AS [Penerimaan Sisa Produksi 1], ISNULL([Penerimaan Produksi 2],0) AS [Penerimaan Produksi 2], "
                    SQL = SQL & "ISNULL([Transfer Stock],0) AS [Transfer Stock], ISNULL([Split Stock],0) AS [Split Stock], ISNULL([Transfer Material],0) AS [Transfer Material], "
                    SQL = SQL & "ISNULL([Pengeluaran Stock],0) AS [Pengeluaran Stock], ISNULL([Pengeluaran Bahan Bakar],0) AS [Pengeluaran Bahan Bakar], "
                    SQL = SQL & "ISNULL([Pemakaian Produksi 1],0) AS [Pemakaian Produksi 1], ISNULL([Pemakaian Produksi 2],0) AS [Pemakaian Produksi 2], "
                    SQL = SQL & "ISNULL([Pengeluaran Produksi 2],0) AS [Pengeluaran Produksi 2] "
                    SQL = SQL & "FROM ( SELECT kode_perusahaan, Lokasi, Kode_Group_Jenis, kode_barang, Nama, QR, Jenis  AS JenisKolom, Nilai FROM cte ) src "
                    SQL = SQL & "PIVOT ( SUM(Nilai) FOR JenisKolom IN ( [Saldo Awal], [Barang Masuk], [Transfer Stock], [Terima Transfer Stock], [Split Stock], [Terima Split Stock], [Transfer Material], "
                    SQL = SQL & "[Terima Transfer Material], [Penambahan Stock], [Pengeluaran Stock], [Pengeluaran Bahan Bakar], [Pemakaian Produksi 1], [Retur Produksi], [Penerimaan Produksi 1], "
                    SQL = SQL & "[Penerimaan Sisa Produksi 1], [Pengeluaran Produksi 2], [Penerimaan Produksi 2], [Pemakaian Produksi 2] ) ) p ) "
                    SQL = SQL & "select *, Round( [Saldo Awal]+ [Barang Masuk]+ [Transfer Stock]+ [Terima Transfer Stock]+ [Split Stock]+ [Terima Split Stock]+ [Transfer Material]+ [Terima Transfer Material]+ "
                    SQL = SQL & "[Penambahan Stock]+ [Pengeluaran Stock]+ [Pengeluaran Bahan Bakar]+ [Pemakaian Produksi 1]+ [Retur Produksi]+ [Penerimaan Produksi 1]+ [Penerimaan Sisa Produksi 1]+ "
                    SQL = SQL & "[Pengeluaran Produksi 2]+ [Penerimaan Produksi 2]+ [Pemakaian Produksi 2],4) as Stock from cte_b a "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    If Filter Then
                        If Cmb_Lokasi.SelectedIndex > 0 Then
                            SQL = SQL & "and lokasi = '" & Cmb_Lokasi.Text & "' "
                        End If

                        If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                            SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text & "' "
                        End If

                        If Cmb_Barcode.SelectedIndex > 0 Then
                            If Not Txt_Barcode.Text.Trim.Length = 0 Then
                                SQL = SQL & "and QR = '" & Txt_Barcode.Text & "' "
                            Else
                                CloseConn()
                                MessageBox.Show("Barcode Tidak Boleh Kosong")
                                Txt_Barcode.Focus() : Exit Sub
                            End If

                        End If

                        If Cmb_Group_Jenis.SelectedIndex > 0 Then
                            SQL = SQL & "and Kode_Group_Jenis = '" & Cmb_Group_Jenis.Text & "' "
                        End If
                    End If
                    SQL = SQL & "ORDER BY Nama, QR; "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For i As Integer = 0 To .Rows.Count - 1
                                    Dgv_Detail.Rows.Add(1)
                                    Dgv_Detail.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Group_Jenis")
                                    Dgv_Detail.Rows(i).Cells(1).Value = .Rows(i).Item("Lokasi")
                                    Dgv_Detail.Rows(i).Cells(2).Value = .Rows(i).Item("kode_barang")
                                    Dgv_Detail.Rows(i).Cells(3).Value = .Rows(i).Item("Nama")
                                    Dgv_Detail.Rows(i).Cells(4).Value = .Rows(i).Item("QR")
                                    Dgv_Detail.Rows(i).Cells(5).Value = Format(.Rows(i).Item("Saldo Awal"), "N0")
                                    Dgv_Detail.Rows(i).Cells(6).Value = Format(.Rows(i).Item("Barang Masuk"), "N2")
                                    Dgv_Detail.Rows(i).Cells(7).Value = Format(.Rows(i).Item("Terima Transfer Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(8).Value = Format(.Rows(i).Item("Terima Split Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(9).Value = Format(.Rows(i).Item("Terima Transfer Material"), "N2")
                                    Dgv_Detail.Rows(i).Cells(10).Value = Format(.Rows(i).Item("Penambahan Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(11).Value = Format(.Rows(i).Item("Retur Produksi"), "N2")
                                    Dgv_Detail.Rows(i).Cells(12).Value = Format(.Rows(i).Item("Penerimaan Produksi 1"), "N2")
                                    Dgv_Detail.Rows(i).Cells(13).Value = Format(.Rows(i).Item("Penerimaan Sisa Produksi 1"), "N2")
                                    Dgv_Detail.Rows(i).Cells(14).Value = Format(.Rows(i).Item("Penerimaan Produksi 2"), "N2")
                                    Dgv_Detail.Rows(i).Cells(15).Value = Format(.Rows(i).Item("Transfer Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(16).Value = Format(.Rows(i).Item("Split Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(17).Value = Format(.Rows(i).Item("Transfer Material"), "N2")
                                    Dgv_Detail.Rows(i).Cells(18).Value = Format(.Rows(i).Item("Pengeluaran Stock"), "N2")
                                    Dgv_Detail.Rows(i).Cells(19).Value = Format(.Rows(i).Item("Pengeluaran Bahan Bakar"), "N2")
                                    Dgv_Detail.Rows(i).Cells(20).Value = Format(.Rows(i).Item("Pemakaian Produksi 1"), "N2")
                                    Dgv_Detail.Rows(i).Cells(21).Value = Format(.Rows(i).Item("Pemakaian Produksi 2"), "N2")
                                    Dgv_Detail.Rows(i).Cells(22).Value = Format(.Rows(i).Item("Pengeluaran Produksi 2"), "N2")
                                    Dgv_Detail.Rows(i).Cells(23).Value = Format(.Rows(i).Item("Stock"), "N2")
                                Next
                            End If
                        End With
                    End Using
                End If

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub




    '===============================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '===============================================================================================================================================================================
    Private Sub Cmb_Barcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Barcode.SelectedIndexChanged
        If Cmb_Barcode.Items.Count = 0 Then Exit Sub

        If Cmb_Barcode.SelectedIndex = 0 Then
            Txt_Barcode.Enabled = False
        ElseIf Cmb_Barcode.SelectedIndex = 1 Then
            Txt_Barcode.Enabled = True
        End If

        Txt_Barcode.Text = ""
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub
    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Lokasi.DroppedDown = True
            Cmb_Lokasi.Focus()
        End If
    End Sub

    Private Sub Btn_Cetak_Rekap_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Rekap.Click
        If Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.DroppedDown = True : Cmb_Lokasi.Focus()
            Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.DroppedDown = True : Cmb_Lokasi.Focus()
            Exit Sub
        ElseIf Cmb_Barcode.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Barcode Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Barcode.DroppedDown = True : Cmb_Barcode.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If Cmb_Filter_by.SelectedIndex = 0 Then
                SQL = "exec EMI_Mutasi_Bahan_Dalam_Proses '" & KodePerusahaan & "', '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                ExecuteTrans(SQL)
            ElseIf Cmb_Filter_by.SelectedIndex = 1 Then
                SQL = "exec EMI_Mutasi_Bahan_Dalam_Proses_Barang '" & KodePerusahaan & "', '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                ExecuteTrans(SQL)
            End If

            Dim SF As String = ""

            If Cmb_Jenis_Laporan.SelectedIndex = 0 Then


                SQL = "select * from N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = "{N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap.kode_perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "AND CDate({N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap.tanggal}) >= Date(" & Year(Tgl1.Value) & "," & Month(Tgl1.Value) & "," & DateAndTime.Day(Tgl1.Value) & ") "
                SF = SF & "AND CDate({N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap.tanggal}) <= Date(" & Year(Tgl2.Value) & "," & Month(Tgl2.Value) & "," & DateAndTime.Day(Tgl2.Value) & ")"



                If Cmb_Lokasi.SelectedIndex > 0 Then
                    SQL = SQL & "and lokasi = '" & Cmb_Lokasi.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap.lokasi} = '" & Cmb_Lokasi.Text & "' "
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap.kode_barang} = '" & Txt_KdBarang.Text & "' "
                End If

                If Cmb_Barcode.SelectedIndex > 0 Then
                    If Not Txt_Barcode.Text.Trim.Length = 0 Then
                        SQL = SQL & "and QR = '" & Txt_Barcode.Text & "' "
                        SF = SF & "And {N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap.QR} = '" & Txt_Barcode.Text & "' "
                    Else
                        CloseConn()
                        MessageBox.Show("Barcode Tidak Boleh Kosong")
                        Txt_Barcode.Focus() : Exit Sub
                    End If

                End If

                If Cmb_Group_Jenis.SelectedIndex > 0 Then
                    SQL = SQL & "and Id_Group_Jenis = '" & arr_Id_Group_Jenis(Cmb_Group_Jenis.SelectedIndex) & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap.Id_Group_Jenis} = " & arr_Id_Group_Jenis(Cmb_Group_Jenis.SelectedIndex) & " "
                End If
                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim dt As DataTable = DS.Tables("MyTable")

                            Dim CrDoc As New N_EMI_CR_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap

                            CrDoc.SetDataSource(dt)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF

                            With A_Place_For_Printing2
                                .Text = "Laporan Mutasi Bahan Rekap"
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With

                        Else

                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If
                    End With
                End Using

            ElseIf Cmb_Jenis_Laporan.SelectedIndex = 1 Then
                SQL = "select * from N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap_Saldo "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                SF = "{N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap_Saldo.kode_perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "AND CDate({N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap_Saldo.tanggal}) >= Date(" & Year(Tgl1.Value) & "," & Month(Tgl1.Value) & "," & DateAndTime.Day(Tgl1.Value) & ") "
                SF = SF & "AND CDate({N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap_Saldo.tanggal}) <= Date(" & Year(Tgl2.Value) & "," & Month(Tgl2.Value) & "," & DateAndTime.Day(Tgl2.Value) & ")"



                If Cmb_Lokasi.SelectedIndex > 0 Then
                    SQL = SQL & "and lokasi = '" & Cmb_Lokasi.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap_Saldo.lokasi} = '" & Cmb_Lokasi.Text & "' "
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and kode_barang = '" & Txt_KdBarang.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap_Saldo.kode_barang} = '" & Txt_KdBarang.Text & "' "
                End If

                If Cmb_Barcode.SelectedIndex > 0 Then
                    If Not Txt_Barcode.Text.Trim.Length = 0 Then
                        SQL = SQL & "and QR = '" & Txt_Barcode.Text & "' "
                        SF = SF & "And {N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap_Saldo.QR} = '" & Txt_Barcode.Text & "' "
                    Else
                        CloseConn()
                        MessageBox.Show("Barcode Tidak Boleh Kosong")
                        Txt_Barcode.Focus() : Exit Sub
                    End If

                End If

                If Cmb_Group_Jenis.SelectedIndex > 0 Then
                    SQL = SQL & "and Id_Group_Jenis = '" & arr_Id_Group_Jenis(Cmb_Group_Jenis.SelectedIndex) & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap_Saldo.Id_Group_Jenis} = " & arr_Id_Group_Jenis(Cmb_Group_Jenis.SelectedIndex) & " "
                End If
                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim dt As DataTable = DS.Tables("MyTable")

                            Dim CrDoc As New N_EMI_CR_Laporan_Mutasi_Bahan_Dalam_Proses_Rekap_Saldo

                            CrDoc.SetDataSource(dt)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF

                            With A_Place_For_Printing2
                                .Text = "Laporan Mutasi Bahan Rekap"
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With

                        Else

                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If
                    End With
                End Using

            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Cetak_Detail_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Detail.Click
        If Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.DroppedDown = True : Cmb_Lokasi.Focus()
            Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.DroppedDown = True : Cmb_Lokasi.Focus()
            Exit Sub
        ElseIf Cmb_Barcode.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Barcode Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Barcode.DroppedDown = True : Cmb_Barcode.Focus()
            Exit Sub
        End If

        If Cmb_Jenis_Laporan.SelectedIndex = 0 Then
            Generate_excel_detail()

        ElseIf Cmb_Jenis_Laporan.SelectedIndex = 1 Then
            Generate_excel_detail_Saldo()
        End If



    End Sub

    Private Sub Generate_excel_detail()
        Try

            get_jam()

            Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

            '=======================================
            '=     CEK APAKAH EXCEL TERINSTALL     =
            '=======================================
            If xlApp Is Nothing Then
                MessageBox.Show("Excel is not properly installed!!")
                Return
            End If

            Dim JudulLaporan As String = "LAPORAN MUTASI BAHAN DALAM PROSES DETAIL"

            Dim xlWorkBook As excel.Workbook
            Dim xlWorkSheet As excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

            'If System.IO.Directory.Exists(lokasi_file) = False Then
            '    System.IO.Directory.CreateDirectory(lokasi_file)
            'End If

            Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
            Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("Sheet1")

            '==================================
            '=     DEFINISIKAN NAMA KOLOM     =
            '==================================

            Dim DigitDecimal As String = ""

#Region "Generate Coloms"

            Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Group Jenis"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Lokasi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Barcode"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Saldo Awal"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Barang Masuk"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Terima Transfer Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Terima Split Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Terima Transfer Material"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Penambahan Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Retur Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Penerimaan Produksi 1"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Penerimaan Sisa Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Penerimaan Produksi 2"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Transfer Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Split Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Transfer Material"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pengeluaran Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pengeluaran Bahan Bakar"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pemakaian Produksi 1"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pemakaian Produksi 2"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pengeluaran Produksi 2"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}}
            }

            Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
                {"Default", New Dictionary(Of String, Object) From {
                    {"Default", "Default"},
                    {"Kolom", 0}
                }}
            }

            For i As Integer = 0 To dataKoloms.Count - 1
                Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

                If kolom("Identifier") = "Main" Then

                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Reject" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Reject"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Scrap" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Scrap"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Waste" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Waste"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Loss" Then

                    'Menambah nilai Range
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).ColumnWidth = 25

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Inspection" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Inspection Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final_GR" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Final Report"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                End If

            Next

#End Region


            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {}

            Dim numberColumn As New List(Of Integer) From {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22}

            Dim DecimalColumn As New List(Of Integer) From {5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22}

            Dim NumberN0 As New List(Of Integer) From {4}

            Dim defaultRowIndex As Integer = 5
            Try
                OpenConn()

                ' Ambil format sesuai culture
                Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
                xlApp.UseSystemSeparators = True

                '==  AMBIL SEPARATOR DARI EXCEL =='
                Dim decimalSep As String = xlApp.DecimalSeparator
                Dim groupSep As String = xlApp.ThousandsSeparator

                '==  AMBIL SEPARATOR DARI SISTEM =='
                'Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
                'Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

                If decimalSep = "," Then
                    decimalSep = "."
                ElseIf decimalSep = "." Then
                    decimalSep = ","
                End If

                If groupSep = "." Then
                    groupSep = ","
                ElseIf groupSep = "," Then
                    groupSep = "."
                End If

                Dim templateFormat As String = "#GROUP##0DEC0000"
                Dim excelFormat As String = templateFormat _
                    .Replace("GROUP", groupSep) _
                    .Replace("DEC", decimalSep)

                Dim templateFormatN0 As String = "#GROUP##0"
                Dim excelFormatN0 As String = templateFormatN0 _
                    .Replace("GROUP", groupSep)




                If Cmb_Filter_by.SelectedIndex = 0 Then
                    SQL = "exec EMI_Mutasi_Bahan_Dalam_Proses '" & KodePerusahaan & "', '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                    ExecuteTrans(SQL)
                ElseIf Cmb_Filter_by.SelectedIndex = 1 Then
                    SQL = "exec EMI_Mutasi_Bahan_Dalam_Proses_Barang '" & KodePerusahaan & "', '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                    ExecuteTrans(SQL)
                End If


#Region "KODE LAMA"

                'Dim row As Integer = 0
                'SQL = ";WITH cte AS ( SELECT  a.No_Faktur, Tanggal, Jam, Jenis, Keterangan, Kode_stock_owner AS Lokasi, Kode_barang, Nama, QR, ROUND(Masuk,4) - ROUND(Keluar,4) AS Nilai FROM Data_CutOffTracking3 a ) "
                'SQL = SQL & ",cte_b as( SELECT  Lokasi, kode_barang, Nama, QR, ISNULL([Saldo Awal],0) AS [Saldo Awal], ISNULL([Barang Masuk],0) AS [Barang Masuk], "
                'SQL = SQL & "ISNULL([Terima Transfer Stock],0) AS [Terima Transfer Stock], ISNULL([Terima Split Stock],0) AS [Terima Split Stock], "
                'SQL = SQL & "ISNULL([Terima Transfer Material],0) AS [Terima Transfer Material], ISNULL([Penambahan Stock],0) AS [Penambahan Stock], "
                'SQL = SQL & "ISNULL([Retur Produksi],0) AS [Retur Produksi], ISNULL([Penerimaan Produksi 1],0) AS [Penerimaan Produksi 1], "
                'SQL = SQL & "ISNULL([Penerimaan Sisa Produksi 1],0) AS [Penerimaan Sisa Produksi 1], ISNULL([Penerimaan Produksi 2],0) AS [Penerimaan Produksi 2], "
                'SQL = SQL & "ISNULL([Transfer Stock],0) AS [Transfer Stock], ISNULL([Split Stock],0) AS [Split Stock], ISNULL([Transfer Material],0) AS [Transfer Material], "
                'SQL = SQL & "ISNULL([Pengeluaran Stock],0) AS [Pengeluaran Stock], ISNULL([Pengeluaran Bahan Bakar],0) AS [Pengeluaran Bahan Bakar], "
                'SQL = SQL & "ISNULL([Pemakaian Produksi 1],0) AS [Pemakaian Produksi 1], ISNULL([Pemakaian Produksi 2],0) AS [Pemakaian Produksi 2], "
                'SQL = SQL & "ISNULL([Pengeluaran Produksi 2],0) AS [Pengeluaran Produksi 2] "
                'SQL = SQL & "FROM ( SELECT Lokasi, kode_barang, Nama, QR, Jenis  AS JenisKolom, Nilai FROM cte ) src "
                'SQL = SQL & "PIVOT ( SUM(Nilai) FOR JenisKolom IN ( [Saldo Awal], [Barang Masuk], [Transfer Stock], [Terima Transfer Stock], [Split Stock], [Terima Split Stock], [Transfer Material], "
                'SQL = SQL & "[Terima Transfer Material], [Penambahan Stock], [Pengeluaran Stock], [Pengeluaran Bahan Bakar], [Pemakaian Produksi 1], [Retur Produksi], [Penerimaan Produksi 1], "
                'SQL = SQL & "[Penerimaan Sisa Produksi 1], [Pengeluaran Produksi 2], [Penerimaan Produksi 2], [Pemakaian Produksi 2] ) ) p ) "
                'SQL = SQL & "select *, Round( [Saldo Awal]+ [Barang Masuk]+ [Transfer Stock]+ [Terima Transfer Stock]+ [Split Stock]+ [Terima Split Stock]+ [Transfer Material]+ [Terima Transfer Material]+ "
                'SQL = SQL & "[Penambahan Stock]+ [Pengeluaran Stock]+ [Pengeluaran Bahan Bakar]+ [Pemakaian Produksi 1]+ [Retur Produksi]+ [Penerimaan Produksi 1]+ [Penerimaan Sisa Produksi 1]+ "
                'SQL = SQL & "[Pengeluaran Produksi 2]+ [Penerimaan Produksi 2]+ [Pemakaian Produksi 2],4) as Stock from cte_b a "
                ''SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                'If Cmb_Lokasi.SelectedIndex > 0 Then
                '    If SQL.ToUpper().Contains("WHERE") Then
                '        If SQL.ToUpper().Trim().EndsWith("WHERE") Then
                '            SQL &= " lokasi = '" & Cmb_Lokasi.Text & "' "
                '        Else
                '            SQL &= "AND lokasi = '" & Cmb_Lokasi.Text & "' "
                '        End If
                '    Else
                '        SQL &= "WHERE lokasi = '" & Cmb_Lokasi.Text & "' "
                '    End If

                '    'SQL = SQL & "and lokasi = '" & Cmb_Lokasi.Text & "' "
                'End If

                'If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                '    If SQL.ToUpper().Contains("WHERE") Then
                '        SQL &= "AND kode_barang = '" & Txt_KdBarang.Text & "' "
                '    Else
                '        SQL &= "WHERE kode_barang = '" & Txt_KdBarang.Text & "' "
                '    End If
                'End If

                'If Cmb_Barcode.SelectedIndex > 0 Then
                '    If Not Txt_Barcode.Text.Trim.Length = 0 Then
                '        If SQL.ToUpper().Contains("WHERE") Then
                '            SQL &= "AND QR = '" & Txt_Barcode.Text & "' "
                '        Else
                '            SQL &= "WHERE QR = '" & Txt_Barcode.Text & "' "
                '        End If
                '    Else
                '        CloseConn()
                '        MessageBox.Show("Barcode Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '        Txt_Barcode.Focus()
                '        Exit Sub
                '    End If
                'End If
                'SQL = SQL & "ORDER BY Nama, QR; "
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then

                '            For i As Integer = 0 To .Rows.Count - 1

                '                For colIndex As Integer = 0 To .Columns.Count - 1
                '                    Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                '                    If colIndex = 6 Then
                '                        cell.NumberFormat = "@"
                '                    End If

                '                    cell.Value = General_Class.CekNULL(.Rows(i).Item(colIndex))

                '                    cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                '                    ' Format numerik (N4)
                '                    If numberColumn.Contains(colIndex) Then
                '                        Dim value As Object = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))
                '                        Dim result As Double = 0
                '                        If value IsNot DBNull.Value Then
                '                            Double.TryParse(value.ToString(), result)
                '                        End If

                '                        'Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))
                '                        Dim nilai As Double = result

                '                        If NumberN0.Contains(colIndex) Then
                '                            cell.NumberFormat = excelFormatN0
                '                        Else
                '                            cell.NumberFormat = excelFormat
                '                        End If


                '                        cell.Value = nilai

                '                    End If



                '                    '== ATUR ALIGMENT CELL =='
                '                    Select Case .Columns(colIndex).DataType.Name
                '                        Case "String"
                '                            cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignLeft)
                '                        Case "DateTime"
                '                            cell.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                '                            cell.Value = Format(CDate(.Rows(i).Item(colIndex)), "dd MMM yyyy")
                '                        Case "Int32", "Double"
                '                            cell.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
                '                            cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignRight)
                '                    End Select

                '                    ' BORDER
                '                    With cell.Borders
                '                        .LineStyle = excel.XlLineStyle.xlContinuous
                '                        .ColorIndex = 0
                '                        .Weight = excel.XlBorderWeight.xlThin
                '                    End With

                '                    ' BG COLOR
                '                    Select Case colIndex
                '                        Case 0 To 3
                '                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                '                        Case 4
                '                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)

                '                    End Select

                '                    xlWorkSheet.Cells(1, 1).Interior.TintAndShade = 0.2

                '                Next

                '                row += 1

                '            Next

                '        Else
                '            CloseConn()
                '            MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If

                '    End With
                'End Using


#End Region


                Dim jumlahRows As Integer = 0

                Dim row As Integer = 0
                SQL = ";WITH cte AS ( SELECT  a.No_Faktur, a.Tanggal, a.Jam, a.Jenis, a.Keterangan, c.Kode_Group_Jenis, a.Kode_stock_owner AS Lokasi, a.Kode_barang, a.Nama, a.QR, ROUND(Masuk,4) - ROUND(Keluar,4) AS Nilai "

                SQL = SQL & "FROM Data_CutOffTracking3 a, barang b, EMI_Group_Jenis c "
                SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and b.Id_Group_Jenis = c.Id_Group_Jenis) "

                SQL = SQL & ",cte_b as( SELECT Kode_Group_Jenis, Lokasi, kode_barang, Nama, QR, ISNULL([Saldo Awal],0) AS [Saldo Awal], ISNULL([Barang Masuk],0) AS [Barang Masuk], "
                SQL = SQL & "ISNULL([Terima Transfer Stock],0) AS [Terima Transfer Stock], ISNULL([Terima Split Stock],0) AS [Terima Split Stock], "
                SQL = SQL & "ISNULL([Terima Transfer Material],0) AS [Terima Transfer Material], ISNULL([Penambahan Stock],0) AS [Penambahan Stock], "
                SQL = SQL & "ISNULL([Retur Produksi],0) AS [Retur Produksi], ISNULL([Penerimaan Produksi 1],0) AS [Penerimaan Produksi 1], "
                SQL = SQL & "ISNULL([Penerimaan Sisa Produksi 1],0) AS [Penerimaan Sisa Produksi 1], ISNULL([Penerimaan Produksi 2],0) AS [Penerimaan Produksi 2], "
                SQL = SQL & "ISNULL([Transfer Stock],0) AS [Transfer Stock], ISNULL([Split Stock],0) AS [Split Stock], ISNULL([Transfer Material],0) AS [Transfer Material], "
                SQL = SQL & "ISNULL([Pengeluaran Stock],0) AS [Pengeluaran Stock], ISNULL([Pengeluaran Bahan Bakar],0) AS [Pengeluaran Bahan Bakar], "
                SQL = SQL & "ISNULL([Pemakaian Produksi 1],0) AS [Pemakaian Produksi 1], ISNULL([Pemakaian Produksi 2],0) AS [Pemakaian Produksi 2], "
                SQL = SQL & "ISNULL([Pengeluaran Produksi 2],0) AS [Pengeluaran Produksi 2] "
                SQL = SQL & "FROM ( SELECT Kode_Group_Jenis, Lokasi, kode_barang, Nama, QR, Jenis  AS JenisKolom, Nilai FROM cte ) src "
                SQL = SQL & "PIVOT ( SUM(Nilai) FOR JenisKolom IN ( [Saldo Awal], [Barang Masuk], [Transfer Stock], [Terima Transfer Stock], [Split Stock], [Terima Split Stock], [Transfer Material], "
                SQL = SQL & "[Terima Transfer Material], [Penambahan Stock], [Pengeluaran Stock], [Pengeluaran Bahan Bakar], [Pemakaian Produksi 1], [Retur Produksi], [Penerimaan Produksi 1], "
                SQL = SQL & "[Penerimaan Sisa Produksi 1], [Pengeluaran Produksi 2], [Penerimaan Produksi 2], [Pemakaian Produksi 2] ) ) p ) "
                SQL = SQL & "select *, Round( [Saldo Awal]+ [Barang Masuk]+ [Transfer Stock]+ [Terima Transfer Stock]+ [Split Stock]+ [Terima Split Stock]+ [Transfer Material]+ [Terima Transfer Material]+ "
                SQL = SQL & "[Penambahan Stock]+ [Pengeluaran Stock]+ [Pengeluaran Bahan Bakar]+ [Pemakaian Produksi 1]+ [Retur Produksi]+ [Penerimaan Produksi 1]+ [Penerimaan Sisa Produksi 1]+ "
                SQL = SQL & "[Pengeluaran Produksi 2]+ [Penerimaan Produksi 2]+ [Pemakaian Produksi 2],4) as Stock from cte_b a "
                'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "


                Dim filterConditions As New List(Of String)
                If Cmb_Lokasi.SelectedIndex > 0 Then
                    filterConditions.Add("lokasi = '" & Cmb_Lokasi.Text & "'")
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    filterConditions.Add("kode_barang = '" & Txt_KdBarang.Text & "'")
                End If

                If Cmb_Barcode.SelectedIndex > 0 Then
                    If Not String.IsNullOrWhiteSpace(Txt_Barcode.Text) Then
                        filterConditions.Add("QR = '" & Txt_Barcode.Text & "'")
                    Else
                        CloseConn()
                        MessageBox.Show("Barcode Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Txt_Barcode.Focus()
                        Exit Sub
                    End If
                End If

                If Cmb_Group_Jenis.SelectedIndex > 0 Then
                    filterConditions.Add("Kode_Group_Jenis = '" & Cmb_Group_Jenis.Text & "'")
                End If

                If filterConditions.Any() Then
                    SQL &= " WHERE " & String.Join(" AND ", filterConditions)
                End If


                SQL = SQL & "ORDER BY Nama, QR; "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count = 0 Then
                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        ' --- PERUBAHAN UTAMA DIMULAI DI SINI ---

                        Dim rowCount As Integer = .Rows.Count
                        Dim colCount As Integer = .Columns.Count
                        jumlahRows = rowCount

                        'DUPLIKAT DATA KEDALAM DATA ARRAY 2 DIMENSI (ROW, KOLOM)
                        Dim dataArray(rowCount - 1, colCount - 1) As Object
                        For r As Integer = 0 To rowCount - 1
                            For c As Integer = 0 To colCount - 1

                                'Ambil data dari data tabel
                                Dim currentValue As Object = .Rows(r)(c)

                                ' cek tipe datanya
                                If TypeOf currentValue Is Date Then
                                    dataArray(r, c) = Format(CDate(currentValue), "dd MMM yyyy")
                                Else
                                    dataArray(r, c) = General_Class.CekNULL(currentValue)
                                End If
                            Next
                        Next

                        ' Definisikan target
                        Dim startCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex, 1)
                        Dim endCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex + rowCount - 1, colCount)
                        Dim dataRange As excel.Range = xlWorkSheet.Range(startCell, endCell)

                        'Tempel data berdasarkan mulai dan akhir cell sebelumnya
                        dataRange.Value = dataArray

                        'atur posisi vertical semua ke tengah
                        dataRange.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                        ' set border
                        With dataRange.Borders
                            .LineStyle = excel.XlLineStyle.xlContinuous
                            .ColorIndex = 0
                            .Weight = excel.XlBorderWeight.xlThin
                        End With

                        ' Atur warna, format, dan style per celnya
                        For c As Integer = 1 To colCount
                            Dim colIndex As Integer = c - 1 ' Index berbasis 0
                            Dim currentColumn As excel.Range = dataRange.Columns(c)

                            If colIndex = 6 Then
                                currentColumn.NumberFormat = "@"
                            End If

                            ' Format kolom numerik (N4 atau N0)
                            If numberColumn.Contains(colIndex) Then
                                If NumberN0.Contains(colIndex) Then
                                    currentColumn.NumberFormat = excelFormatN0
                                Else
                                    currentColumn.NumberFormat = excelFormat
                                End If
                                currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
                            End If

                            ' Alignment untuk kolom String (Text)
                            If .Columns(colIndex).DataType.Name = "String" Or .Columns(colIndex).DataType.Name = "DateTime" Then
                                If stringCenter.Contains(colIndex) Then
                                    currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                                Else
                                    currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignLeft
                                End If
                            End If
                        Next

                        xlWorkSheet.Range(dataRange.Columns(1), dataRange.Columns(5)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                        dataRange.Columns(6).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)

                        dataRange.Columns.AutoFit()

                    End With
                End Using

                ' AutoFit kolom setelah semua data dimasukkan
                xlWorkSheet.Columns.AutoFit()

                '==========================
                '=     HEADER LAPORAN     =
                '==========================
                Dim panjangKolom As Integer = dataKoloms.Count

                xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

                xlWorkSheet.Cells(1, 1).Value = JudulLaporan
                xlWorkSheet.Cells(1, 1).Font.Size = 14
                xlWorkSheet.Cells(1, 1).Font.Bold = True
                xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()

                '==========================
                '=     FOOTER LAPORAN     =
                '==========================


                Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

                xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).Value = Footer
                xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



            '=====================
            '=     SAVE FILE     =
            '=====================
            Dim saveFileDialog As New SaveFileDialog()

            ' Set File Filter
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
            saveFileDialog.Title = "Save As"


            'Tampilkan Show Dialog Save as
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim filePath As String = saveFileDialog.FileName

                    xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

                    'MessageBox.Show("File berhasil disimpan di: " & filePath)

                    ' Menutup workbook dan aplikasi Excel
                    xlWorkBook.Close()
                    xlApp.Quit()

                    ' Membebaskan objek Excel
                    releaseObject(xlWorkSheet)
                    releaseObject(xlWorkBook)
                    releaseObject(xlApp)

                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Generate_excel_detail_Saldo()
        Try

            get_jam()

            Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

            '=======================================
            '=     CEK APAKAH EXCEL TERINSTALL     =
            '=======================================
            If xlApp Is Nothing Then
                MessageBox.Show("Excel is not properly installed!!")
                Return
            End If

            Dim JudulLaporan As String = "LAPORAN MUTASI BAHAN DALAM PROSES DETAIL SALDO"

            Dim xlWorkBook As excel.Workbook
            Dim xlWorkSheet As excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

            'If System.IO.Directory.Exists(lokasi_file) = False Then
            '    System.IO.Directory.CreateDirectory(lokasi_file)
            'End If

            Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
            Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("Sheet1")

            '==================================
            '=     DEFINISIKAN NAMA KOLOM     =
            '==================================

            Dim DigitDecimal As String = ""

#Region "Generate Coloms"

            Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Group Jenis"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Lokasi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Barcode"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Saldo Awal"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Barang Masuk"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Terima Transfer Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Terima Split Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Terima Transfer Material"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Penambahan Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Retur Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Penerimaan Produksi 1"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Penerimaan Sisa Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Penerimaan Produksi 2"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Transfer Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Split Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Transfer Material"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pengeluaran Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pengeluaran Bahan Bakar"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pemakaian Produksi 1"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pemakaian Produksi 2"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Pengeluaran Produksi 2"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Stock"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}}
            }

            Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
                {"Default", New Dictionary(Of String, Object) From {
                    {"Default", "Default"},
                    {"Kolom", 0}
                }}
            }

            For i As Integer = 0 To dataKoloms.Count - 1
                Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

                If kolom("Identifier") = "Main" Then

                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Reject" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Reject"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Scrap" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Scrap"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Waste" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Waste"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Loss" Then

                    'Menambah nilai Range
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).ColumnWidth = 25

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Inspection" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Inspection Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final_GR" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Final Report"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                End If

            Next

#End Region


            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {}

            Dim numberColumn As New List(Of Integer) From {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22}

            Dim DecimalColumn As New List(Of Integer) From {5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22}

            Dim NumberN0 As New List(Of Integer) From {4}

            Dim defaultRowIndex As Integer = 5
            Try
                OpenConn()

                ' Ambil format sesuai culture
                Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
                xlApp.UseSystemSeparators = True

                '==  AMBIL SEPARATOR DARI EXCEL =='
                Dim decimalSep As String = xlApp.DecimalSeparator
                Dim groupSep As String = xlApp.ThousandsSeparator

                '==  AMBIL SEPARATOR DARI SISTEM =='
                'Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
                'Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

                If decimalSep = "," Then
                    decimalSep = "."
                ElseIf decimalSep = "." Then
                    decimalSep = ","
                End If

                If groupSep = "." Then
                    groupSep = ","
                ElseIf groupSep = "," Then
                    groupSep = "."
                End If

                Dim templateFormat As String = "#GROUP##0DEC0000"
                Dim excelFormat As String = templateFormat _
                    .Replace("GROUP", groupSep) _
                    .Replace("DEC", decimalSep)

                Dim templateFormatN0 As String = "#GROUP##0"
                Dim excelFormatN0 As String = templateFormatN0 _
                    .Replace("GROUP", groupSep)




                If Cmb_Filter_by.SelectedIndex = 0 Then
                    SQL = "exec EMI_Mutasi_Bahan_Dalam_Proses '" & KodePerusahaan & "', '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                    ExecuteTrans(SQL)
                ElseIf Cmb_Filter_by.SelectedIndex = 1 Then
                    SQL = "exec EMI_Mutasi_Bahan_Dalam_Proses_Barang '" & KodePerusahaan & "', '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                    ExecuteTrans(SQL)
                End If

#Region " Kode LAMA"



                'Dim row As Integer = 0
                'SQL = ";WITH cte AS ( SELECT  a.No_Faktur, Tanggal, Jam, Jenis, Keterangan, Kode_stock_owner AS Lokasi, Kode_barang, Nama, QR, ROUND(Masuk_Saldo,4) - ROUND(Keluar_Saldo,4) AS Nilai FROM Data_CutOffTracking3 a ) "
                'SQL = SQL & ",cte_b as( SELECT  Lokasi, kode_barang, Nama, QR, ISNULL([Saldo Awal],0) AS [Saldo Awal], ISNULL([Barang Masuk],0) AS [Barang Masuk], "
                'SQL = SQL & "ISNULL([Terima Transfer Stock],0) AS [Terima Transfer Stock], ISNULL([Terima Split Stock],0) AS [Terima Split Stock], "
                'SQL = SQL & "ISNULL([Terima Transfer Material],0) AS [Terima Transfer Material], ISNULL([Penambahan Stock],0) AS [Penambahan Stock], "
                'SQL = SQL & "ISNULL([Retur Produksi],0) AS [Retur Produksi], ISNULL([Penerimaan Produksi 1],0) AS [Penerimaan Produksi 1], "
                'SQL = SQL & "ISNULL([Penerimaan Sisa Produksi 1],0) AS [Penerimaan Sisa Produksi 1], ISNULL([Penerimaan Produksi 2],0) AS [Penerimaan Produksi 2], "
                'SQL = SQL & "ISNULL([Transfer Stock],0) AS [Transfer Stock], ISNULL([Split Stock],0) AS [Split Stock], ISNULL([Transfer Material],0) AS [Transfer Material], "
                'SQL = SQL & "ISNULL([Pengeluaran Stock],0) AS [Pengeluaran Stock], ISNULL([Pengeluaran Bahan Bakar],0) AS [Pengeluaran Bahan Bakar], "
                'SQL = SQL & "ISNULL([Pemakaian Produksi 1],0) AS [Pemakaian Produksi 1], ISNULL([Pemakaian Produksi 2],0) AS [Pemakaian Produksi 2], "
                'SQL = SQL & "ISNULL([Pengeluaran Produksi 2],0) AS [Pengeluaran Produksi 2] "
                'SQL = SQL & "FROM ( SELECT Lokasi, kode_barang, Nama, QR, Jenis  AS JenisKolom, Nilai FROM cte ) src "
                'SQL = SQL & "PIVOT ( SUM(Nilai) FOR JenisKolom IN ( [Saldo Awal], [Barang Masuk], [Transfer Stock], [Terima Transfer Stock], [Split Stock], [Terima Split Stock], [Transfer Material], "
                'SQL = SQL & "[Terima Transfer Material], [Penambahan Stock], [Pengeluaran Stock], [Pengeluaran Bahan Bakar], [Pemakaian Produksi 1], [Retur Produksi], [Penerimaan Produksi 1], "
                'SQL = SQL & "[Penerimaan Sisa Produksi 1], [Pengeluaran Produksi 2], [Penerimaan Produksi 2], [Pemakaian Produksi 2] ) ) p ) "
                'SQL = SQL & "select *, Round( [Saldo Awal]+ [Barang Masuk]+ [Transfer Stock]+ [Terima Transfer Stock]+ [Split Stock]+ [Terima Split Stock]+ [Transfer Material]+ [Terima Transfer Material]+ "
                'SQL = SQL & "[Penambahan Stock]+ [Pengeluaran Stock]+ [Pengeluaran Bahan Bakar]+ [Pemakaian Produksi 1]+ [Retur Produksi]+ [Penerimaan Produksi 1]+ [Penerimaan Sisa Produksi 1]+ "
                'SQL = SQL & "[Pengeluaran Produksi 2]+ [Penerimaan Produksi 2]+ [Pemakaian Produksi 2],4) as Stock from cte_b a "
                ''SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                'If Cmb_Lokasi.SelectedIndex > 0 Then
                '    If SQL.ToUpper().Contains("WHERE") Then
                '        If SQL.ToUpper().Trim().EndsWith("WHERE") Then
                '            SQL &= " lokasi = '" & Cmb_Lokasi.Text & "' "
                '        Else
                '            SQL &= "AND lokasi = '" & Cmb_Lokasi.Text & "' "
                '        End If
                '    Else
                '        SQL &= "WHERE lokasi = '" & Cmb_Lokasi.Text & "' "
                '    End If

                '    'SQL = SQL & "and lokasi = '" & Cmb_Lokasi.Text & "' "
                'End If

                'If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                '    If SQL.ToUpper().Contains("WHERE") Then
                '        SQL &= "AND kode_barang = '" & Txt_KdBarang.Text & "' "
                '    Else
                '        SQL &= "WHERE kode_barang = '" & Txt_KdBarang.Text & "' "
                '    End If
                'End If

                'If Cmb_Barcode.SelectedIndex > 0 Then
                '    If Not Txt_Barcode.Text.Trim.Length = 0 Then
                '        If SQL.ToUpper().Contains("WHERE") Then
                '            SQL &= "AND QR = '" & Txt_Barcode.Text & "' "
                '        Else
                '            SQL &= "WHERE QR = '" & Txt_Barcode.Text & "' "
                '        End If
                '    Else
                '        CloseConn()
                '        MessageBox.Show("Barcode Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '        Txt_Barcode.Focus()
                '        Exit Sub
                '    End If
                'End If
                'SQL = SQL & "ORDER BY Nama, QR; "
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then

                '            For i As Integer = 0 To .Rows.Count - 1

                '                For colIndex As Integer = 0 To .Columns.Count - 1
                '                    Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                '                    If colIndex = 6 Then
                '                        cell.NumberFormat = "@"
                '                    End If

                '                    cell.Value = General_Class.CekNULL(.Rows(i).Item(colIndex))

                '                    cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                '                    ' Format numerik (N4)
                '                    If numberColumn.Contains(colIndex) Then
                '                        Dim value As Object = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))
                '                        Dim result As Double = 0
                '                        If value IsNot DBNull.Value Then
                '                            Double.TryParse(value.ToString(), result)
                '                        End If

                '                        'Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))
                '                        Dim nilai As Double = result

                '                        If NumberN0.Contains(colIndex) Then
                '                            cell.NumberFormat = excelFormatN0
                '                        Else
                '                            cell.NumberFormat = excelFormat
                '                        End If


                '                        cell.Value = nilai

                '                    End If



                '                    '== ATUR ALIGMENT CELL =='
                '                    Select Case .Columns(colIndex).DataType.Name
                '                        Case "String"
                '                            cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignLeft)
                '                        Case "DateTime"
                '                            cell.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                '                            cell.Value = Format(CDate(.Rows(i).Item(colIndex)), "dd MMM yyyy")
                '                        Case "Int32", "Double"
                '                            cell.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
                '                            cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignRight)
                '                    End Select

                '                    ' BORDER
                '                    With cell.Borders
                '                        .LineStyle = excel.XlLineStyle.xlContinuous
                '                        .ColorIndex = 0
                '                        .Weight = excel.XlBorderWeight.xlThin
                '                    End With

                '                    ' BG COLOR
                '                    Select Case colIndex
                '                        Case 0 To 3
                '                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                '                        Case 4
                '                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)

                '                    End Select

                '                    xlWorkSheet.Cells(1, 1).Interior.TintAndShade = 0.2

                '                Next

                '                row += 1

                '            Next

                '        Else
                '            CloseConn()
                '            MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If

                '    End With
                'End Using

#End Region
                Dim jumlahRows As Integer = 0

                Dim row As Integer = 0
                SQL = ";WITH cte AS ( SELECT  a.No_Faktur, a.Tanggal, a.Jam, a.Jenis, a.Keterangan, c.Kode_Group_Jenis, a.Kode_stock_owner AS Lokasi, a.Kode_barang, a.Nama, a.QR, ROUND(Masuk_Saldo,4) - ROUND(Keluar_Saldo,4) AS Nilai "

                SQL = SQL & "FROM Data_CutOffTracking3 a, barang b, EMI_Group_Jenis c "
                SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and b.Id_Group_Jenis = c.Id_Group_Jenis) "

                SQL = SQL & ",cte_b as( SELECT Kode_Group_Jenis, Lokasi, kode_barang, Nama, QR, ISNULL([Saldo Awal],0) AS [Saldo Awal], ISNULL([Barang Masuk],0) AS [Barang Masuk], "
                SQL = SQL & "ISNULL([Terima Transfer Stock],0) AS [Terima Transfer Stock], ISNULL([Terima Split Stock],0) AS [Terima Split Stock], "
                SQL = SQL & "ISNULL([Terima Transfer Material],0) AS [Terima Transfer Material], ISNULL([Penambahan Stock],0) AS [Penambahan Stock], "
                SQL = SQL & "ISNULL([Retur Produksi],0) AS [Retur Produksi], ISNULL([Penerimaan Produksi 1],0) AS [Penerimaan Produksi 1], "
                SQL = SQL & "ISNULL([Penerimaan Sisa Produksi 1],0) AS [Penerimaan Sisa Produksi 1], ISNULL([Penerimaan Produksi 2],0) AS [Penerimaan Produksi 2], "
                SQL = SQL & "ISNULL([Transfer Stock],0) AS [Transfer Stock], ISNULL([Split Stock],0) AS [Split Stock], ISNULL([Transfer Material],0) AS [Transfer Material], "
                SQL = SQL & "ISNULL([Pengeluaran Stock],0) AS [Pengeluaran Stock], ISNULL([Pengeluaran Bahan Bakar],0) AS [Pengeluaran Bahan Bakar], "
                SQL = SQL & "ISNULL([Pemakaian Produksi 1],0) AS [Pemakaian Produksi 1], ISNULL([Pemakaian Produksi 2],0) AS [Pemakaian Produksi 2], "
                SQL = SQL & "ISNULL([Pengeluaran Produksi 2],0) AS [Pengeluaran Produksi 2] "
                SQL = SQL & "FROM ( SELECT Kode_Group_Jenis, Lokasi, kode_barang, Nama, QR, Jenis  AS JenisKolom, Nilai FROM cte ) src "
                SQL = SQL & "PIVOT ( SUM(Nilai) FOR JenisKolom IN ( [Saldo Awal], [Barang Masuk], [Transfer Stock], [Terima Transfer Stock], [Split Stock], [Terima Split Stock], [Transfer Material], "
                SQL = SQL & "[Terima Transfer Material], [Penambahan Stock], [Pengeluaran Stock], [Pengeluaran Bahan Bakar], [Pemakaian Produksi 1], [Retur Produksi], [Penerimaan Produksi 1], "
                SQL = SQL & "[Penerimaan Sisa Produksi 1], [Pengeluaran Produksi 2], [Penerimaan Produksi 2], [Pemakaian Produksi 2] ) ) p ) "
                SQL = SQL & "select *, Round( [Saldo Awal]+ [Barang Masuk]+ [Transfer Stock]+ [Terima Transfer Stock]+ [Split Stock]+ [Terima Split Stock]+ [Transfer Material]+ [Terima Transfer Material]+ "
                SQL = SQL & "[Penambahan Stock]+ [Pengeluaran Stock]+ [Pengeluaran Bahan Bakar]+ [Pemakaian Produksi 1]+ [Retur Produksi]+ [Penerimaan Produksi 1]+ [Penerimaan Sisa Produksi 1]+ "
                SQL = SQL & "[Pengeluaran Produksi 2]+ [Penerimaan Produksi 2]+ [Pemakaian Produksi 2],4) as Stock from cte_b a "
                'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "

                Dim filterConditions As New List(Of String)
                If Cmb_Lokasi.SelectedIndex > 0 Then
                    filterConditions.Add("lokasi = '" & Cmb_Lokasi.Text & "'")
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    filterConditions.Add("kode_barang = '" & Txt_KdBarang.Text & "'")
                End If

                If Cmb_Barcode.SelectedIndex > 0 Then
                    If Not String.IsNullOrWhiteSpace(Txt_Barcode.Text) Then
                        filterConditions.Add("QR = '" & Txt_Barcode.Text & "'")
                    Else
                        CloseConn()
                        MessageBox.Show("Barcode Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Txt_Barcode.Focus()
                        Exit Sub
                    End If
                End If

                If Cmb_Group_Jenis.SelectedIndex > 0 Then
                    filterConditions.Add("Kode_Group_Jenis = '" & Cmb_Group_Jenis.Text & "'")
                End If

                If filterConditions.Any() Then
                    SQL &= " WHERE " & String.Join(" AND ", filterConditions)
                End If
                SQL = SQL & "ORDER BY Nama, QR; "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count = 0 Then
                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        ' --- PERUBAHAN UTAMA DIMULAI DI SINI ---

                        Dim rowCount As Integer = .Rows.Count
                        Dim colCount As Integer = .Columns.Count
                        jumlahRows = rowCount

                        'DUPLIKAT DATA KEDALAM DATA ARRAY 2 DIMENSI (ROW, KOLOM)
                        Dim dataArray(rowCount - 1, colCount - 1) As Object
                        For r As Integer = 0 To rowCount - 1
                            For c As Integer = 0 To colCount - 1

                                'Ambil data dari data tabel
                                Dim currentValue As Object = .Rows(r)(c)

                                ' cek tipe datanya
                                If TypeOf currentValue Is Date Then
                                    dataArray(r, c) = Format(CDate(currentValue), "dd MMM yyyy")
                                Else
                                    dataArray(r, c) = General_Class.CekNULL(currentValue)
                                End If
                            Next
                        Next

                        ' Definisikan target
                        Dim startCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex, 1)
                        Dim endCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex + rowCount - 1, colCount)
                        Dim dataRange As excel.Range = xlWorkSheet.Range(startCell, endCell)

                        'Tempel data berdasarkan mulai dan akhir cell sebelumnya
                        dataRange.Value = dataArray

                        'atur posisi vertical semua ke tengah
                        dataRange.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                        ' set border
                        With dataRange.Borders
                            .LineStyle = excel.XlLineStyle.xlContinuous
                            .ColorIndex = 0
                            .Weight = excel.XlBorderWeight.xlThin
                        End With

                        ' Atur warna, format, dan style per celnya
                        For c As Integer = 1 To colCount
                            Dim colIndex As Integer = c - 1 ' Index berbasis 0
                            Dim currentColumn As excel.Range = dataRange.Columns(c)

                            If colIndex = 6 Then
                                currentColumn.NumberFormat = "@"
                            End If

                            ' Format kolom numerik (N4 atau N0)
                            If numberColumn.Contains(colIndex) Then
                                If NumberN0.Contains(colIndex) Then
                                    currentColumn.NumberFormat = excelFormatN0
                                Else
                                    currentColumn.NumberFormat = excelFormat
                                End If
                                currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
                            End If

                            ' Alignment untuk kolom String (Text)
                            If .Columns(colIndex).DataType.Name = "String" Or .Columns(colIndex).DataType.Name = "DateTime" Then
                                If stringCenter.Contains(colIndex) Then
                                    currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                                Else
                                    currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignLeft
                                End If
                            End If
                        Next

                        xlWorkSheet.Range(dataRange.Columns(1), dataRange.Columns(5)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                        dataRange.Columns(6).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)

                        dataRange.Columns.AutoFit()

                    End With
                End Using


                ' AutoFit kolom setelah semua data dimasukkan
                xlWorkSheet.Columns.AutoFit()

                '==========================
                '=     HEADER LAPORAN     =
                '==========================
                Dim panjangKolom As Integer = dataKoloms.Count

                xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

                xlWorkSheet.Cells(1, 1).Value = JudulLaporan
                xlWorkSheet.Cells(1, 1).Font.Size = 14
                xlWorkSheet.Cells(1, 1).Font.Bold = True
                xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()

                '==========================
                '=     FOOTER LAPORAN     =
                '==========================


                Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

                xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).Value = Footer
                xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



            '=====================
            '=     SAVE FILE     =
            '=====================
            Dim saveFileDialog As New SaveFileDialog()

            ' Set File Filter
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
            saveFileDialog.Title = "Save As"


            'Tampilkan Show Dialog Save as
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim filePath As String = saveFileDialog.FileName

                    xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

                    'MessageBox.Show("File berhasil disimpan di: " & filePath)

                    ' Menutup workbook dan aplikasi Excel
                    xlWorkBook.Close()
                    xlApp.Quit()

                    ' Membebaskan objek Excel
                    releaseObject(xlWorkSheet)
                    releaseObject(xlWorkBook)
                    releaseObject(xlApp)

                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Switch_Autocomplete = False Then Exit Sub
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 163)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(140, 163)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
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
                SQL = SQL & "from barang a "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NmBarang.Text = Dr("Nama")
                        Cmb_Barcode.DroppedDown = True
                        Cmb_Barcode.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Lv_Barang.Location = New Point(1200, 163)
                    Lv_Barang.Visible = False
                End Using
            Else
                Cmb_Barcode.DroppedDown = True
                Cmb_Barcode.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Lv_Barang.Location = New Point(1200, 163)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Switch_Autocomplete = False Then Exit Sub
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Location = New Point(1200, 163)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(140, 163)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
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

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Lv_Barang.Location = New Point(1200, 163)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
        If e.KeyChar() = Chr(13) Then
            If Cmb_Lokasi.SelectedIndex <> -1 Then
                Txt_KdBarang.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Barcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Barcode.KeyPress
        If e.KeyChar() = Chr(13) Then
            If Cmb_Lokasi.SelectedIndex = 0 Then
                Cmb_Group_Jenis.DroppedDown = True
                Cmb_Group_Jenis.Focus()
            ElseIf Cmb_Lokasi.SelectedIndex = 1 Then
                Txt_Barcode.Focus()
            End If
        End If
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim Nmbarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = Nmbarang

        Lv_Barang.Location = New Point(1200, 163)
        Lv_Barang.Visible = False

        Cmb_Barcode.DroppedDown = True
        Cmb_Barcode.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    Private Sub Txt_Barcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Barcode.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Jenis_Laporan.DroppedDown = True
            Cmb_Jenis_Laporan.Focus()
        End If
    End Sub

    Private Sub Cmb_Group_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Group_Jenis.KeyPress
        If e.KeyChar() = Chr(13) Then
            If Cmb_Group_Jenis.SelectedIndex <> -1 Then
                Cmb_Jenis_Laporan.DroppedDown = True
                Cmb_Jenis_Laporan.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Jenis_Laporan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Laporan.KeyPress
        If e.KeyChar() = Chr(13) Then
            If Cmb_Jenis_Laporan.SelectedIndex <> -1 Then
                Cmb_Filter_by.DroppedDown = True
                Cmb_Filter_by.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Filter_by_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter_by.KeyPress
        If e.KeyChar() = Chr(13) Then
            If Cmb_Jenis_Laporan.SelectedIndex <> -1 Then
                Btn_Cari.Focus()
            End If
        End If
    End Sub
End Class
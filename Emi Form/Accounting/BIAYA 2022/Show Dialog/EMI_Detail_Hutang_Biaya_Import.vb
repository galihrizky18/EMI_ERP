Public Class EMI_Detail_Hutang_Biaya_Import


    Public Sub Kosong()


        Txt_NoPO.Text = ""
        Txt_TanggalPO.Text = ""
        Txt_Keterangan.Text = ""
        Txt_Kategori.Text = ""
        Txt_Perusahaan.Text = ""
        Txt_TanggalJatuhTempo.Text = ""
        Txt_KdPerusahaanBiayaImport.Text = ""
        Txt_KdMasterKategori.Text = ""
        Txt_TotalHutang.Text = "0"
        Txt_TotalBayar.Text = "0"
        Txt_Sisa.Text = "0"

        Lv_DetailHutang.Columns.Clear() : Lv_DetailHutang.Items.Clear()
        Lv_DetailHutang.Columns.Add("No Pengajuan", 120, HorizontalAlignment.Left) '0
        Lv_DetailHutang.Columns.Add("Tanggal Bayar", 110, HorizontalAlignment.Center) '1
        Lv_DetailHutang.Columns.Add("Dibayar", 130, HorizontalAlignment.Right) '2
        Lv_DetailHutang.Columns.Add("Kurs Lama", 130, HorizontalAlignment.Right) '3
        Lv_DetailHutang.Columns.Add("Kurs Baru", 130, HorizontalAlignment.Right) '4
        Lv_DetailHutang.Columns.Add("Total Kurs Lama", 130, HorizontalAlignment.Right) '5
        Lv_DetailHutang.Columns.Add("Total Kurs Baru", 130, HorizontalAlignment.Right) '6
        Lv_DetailHutang.Columns.Add("Bank Tujuan", 100, HorizontalAlignment.Left) '7
        Lv_DetailHutang.Columns.Add("Rekening Tujuan", 120, HorizontalAlignment.Left) '8
        Lv_DetailHutang.Columns.Add("Penerima", 150, HorizontalAlignment.Left) '9
        Lv_DetailHutang.View = View.Details

    End Sub

    Public Sub Load_Lv()









        Try
            OpenConn()

            Lv_DetailHutang.Items.Clear()

            SQL = "select a.no_pengajuan, b.Tanggal_Bayar, b.Byr, b.Kurs_Lama, b. Kurs_Baru, b.Total_Bayar_Kurs_Lama, b.Total_Bayar_Kurs_Baru, b.Kode_Bank_Tujuan, b.No_Rek_Tujuan, b.Nama_Penerima "
            SQL = SQL & "from EMI_Pelunasan a, EMI_Pelunasan_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Val = b.No_Val "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and b.No_Faktur = '" & Txt_NoPO.Text & "' "
            SQL = SQL & "and b.Kode_Perusahaan_Biaya_Import = '" & Txt_KdPerusahaanBiayaImport.Text & "' "
            SQL = SQL & "and b.Kode_Master_Kategori_Biaya_Import = '" & Txt_KdMasterKategori.Text & "' "
            SQL = SQL & "order by b.Urut "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = Lv_DetailHutang.Items.Add(Dr("no_pengajuan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal_Bayar"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Format(Dr("Byr"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Kurs_Lama"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Kurs_Baru"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Total_Bayar_Kurs_Lama"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Total_Bayar_Kurs_Baru"), "N2"))
                    Lv.SubItems.Add(Dr("Kode_Bank_Tujuan"))
                    Lv.SubItems.Add(Dr("No_Rek_Tujuan"))
                    Lv.SubItems.Add(Dr("Nama_Penerima"))

                Loop

            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Hitung()

    End Sub

    Private Sub Hitung()

        If Lv_DetailHutang.Items.Count = 0 Then
            Txt_Sisa.Text = Format(Val(HilangkanTanda(Txt_TotalHutang.Text)), "N2")
            Exit Sub
        End If

        Dim TotBayar As Double = 0
        Dim Sisa As Double = 0

        For i As Integer = 0 To Lv_DetailHutang.Items.Count - 1

            Dim Dibayar As Double = Val(HilangkanTanda(Lv_DetailHutang.Items(i).SubItems(2).Text))

            TotBayar = TotBayar + Dibayar

        Next

        Sisa = Val(HilangkanTanda(Txt_TotalHutang.Text)) - TotBayar

        Txt_TotalBayar.Text = Format(TotBayar, "N2")
        Txt_Sisa.Text = Format(Sisa, "N2")



    End Sub

End Class
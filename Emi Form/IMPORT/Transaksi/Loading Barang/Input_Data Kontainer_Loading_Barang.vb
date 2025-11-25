Public Class Input_Data_kontainer_Loading_Barang
    Public index As Integer
    Dim nilai_update As Double

    Dim fakturStr As String = ""
    Dim arrInisialFaktur As String = ""
    Dim TEmi_Loading As String = "QI.PO-"
    Private Sub Input_Data_kontainer_Loading_Barang_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Qty.Focus()
    End Sub


    'Private Sub get_Faktur()
    '    fakturStr = FLB & arrInisialFaktur & "-" & Format(Tanggal_Sekarang, "MM/yy") & "-" &
    '                        General_Class.Get_Last_Number2("Loading_Barang", "No_Faktur", JumlahDigit,
    '                        "Kode_perusahaan", KodePerusahaan,
    '                        "And", "substring(No_Faktur,1," & Len(FLB) + Len(arrInisialFaktur) + 6 & ")",
    '                         FLB & arrInisialFaktur & "-" & Format(Tanggal_Sekarang, "MM/yy"))

    'End Sub

    Private Sub Get_No_Faktur()
        fakturStr = TEmi_Loading & Format(Tanggal.Value, "MM/yy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Pembelian_Loading", "no_Faktur", 4,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(TEmi_Loading) + 5 & ")", TEmi_Loading & Format(Tanggal.Value, "MM/yy"))
    End Sub



    Private Sub Edit_Barang_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            SQL = "SELECT * from Kontainer_Masuk Where No_Faktur = '" & faktur.Text & "' and No_Container = '" & Kontainer.Text & "' "
            SQL = SQL & "and Kode_Barang = '" & kode.Text & "' and Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Lokasi.Text & "'"
            Using Dr = OpenTrans(SQL)

                If Dr.Read Then
                    Qty.Text = Dr("Qty")
                    nilai_update = Dr("Qty")
                    Button1.Text = "Update"
                Else
                    Qty.Text = ""
                    nilai_update = 0
                    Button1.Text = "Simpan"
                End If

            End Using
            CloseConn()
        Catch ex As Exception

            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If faktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No Faktur Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            faktur.Focus()
            Exit Sub
        ElseIf Kontainer.Text.Trim.Length = 0 Then
            MessageBox.Show("No kontainer Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Kontainer.Focus()
            Exit Sub
        ElseIf Seal.Text.Trim.Length = 0 Then
            MessageBox.Show("No Seal Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Seal.Focus()
            Exit Sub
        ElseIf Lokasi.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lokasi.Focus()
            Exit Sub
        ElseIf kode.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kode.Focus()
            Exit Sub
        ElseIf Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Barang Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kode.Focus()
            Exit Sub
        ElseIf Val(Qty.Text) = 0 Then
            MessageBox.Show("Qty belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kode.Focus()
            Exit Sub
        End If

        'Pembelian_Import.ubah_listview(TextBox1.Text, Pembelian_New_Dist.IndexExpire, Format(DateTimePicker3.Value, "dd MMM yyyy"), Pembelian_New_Dist.IndexHrg, hrg.Text)

        'Pembelian_New_Dist.listview1.Items(TextBox1.Text).SubItems(Pembelian_New_Dist.IndexExpire). = Format(DateTimePicker3.Value, "dd MMM yyyy")
        'Pembelian_New_Dist.listview1.Items(TextBox1.Text).SubItems(Pembelian_New_Dist.IndexHrg).Text = Format(Val(hrg.Text), "N0")
        'Pembelian_New_Dist.listview1.Items(TextBox1.Text).SubItems(Pembelian_New_Dist.IndexSubttl).Text = Format(Val(hrg.Text) * Val(HilangkanTanda(Pembelian_New_Dist.listview1.Items(TextBox1.Text).SubItems(Pembelian_New_Dist.IndexJml).Text)), "N0")
        'Pembelian_New_Dist.listview1.Items(TextBox1.Text).SubItems(Pembelian_New_Dist.IndexModal).Text = Format(Val(hrg.Text), "N0")
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            Dim Total As Double = 0

            SQL = "Select Kode_Barang, Sum(Qty) as jml from Kontainer_Masuk where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur ='" & faktur.Text & "' and Kode_Barang = '" & kode.Text & "' and Kode_Stock_Owner = '" & Lokasi.Text & "' group by Kode_Barang"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Total = Dr("jml")
                End If
            End Using

            SQL = "Select Kode_Barang, Jumlah from detail_submit_PO where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur ='" & faktur.Text & "' and Kode_Barang = '" & kode.Text & "' and Kode_Stock_Owner = '" & Lokasi.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'If Total + Val(Qty.Text) - nilai_update > Dr("Jumlah") Then
                    '    Dr.Close()
                    '    CloseTrans()
                    '    CloseConn()
                    '    MessageBox.Show("Jumlah Input Melebihi Jumlah PO!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    '    Exit Sub
                    'End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End Using

            Dim sat As String = ""
            SQL = "select satuan from Barang_Detail_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & kode.Text & "' and Flag_Kirim = 'Y'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    sat = Dr("satuan")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("data satuan kirim Tidak di temukan")
                    Exit Sub
                End If
            End Using

            Dim sat_brg As String = ""
            SQL = "select Satuan from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Lokasi.Text & "' And Kode_Barang = '" & kode.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    sat_brg = Dr("satuan")
                End If
            End Using

            Dim jml_brg As Double = 0
            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & kode.Text & "', '" & sat & "',"
            SQL = SQL & "'" & sat_brg & "', '" & Val(Format(Val(Qty.Text), "N2")) & "' ) as hasil"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    jml_brg = Dr("hasil")
                End If
            End Using

            If Button1.Text = "Simpan" Then
                SQL = "insert into Kontainer_Masuk(Kode_Perusahaan, No_Faktur, No_Container, Kode_stock_Owner, Kode_Barang, No_Seal, Tgl_Muat, Qty) Values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & faktur.Text & "', '" & Kontainer.Text & "', '" & Lokasi.Text & "', '" & kode.Text & "', '" & Seal.Text & "', '" & Format(Tanggal.Value, "yyyy-MM-dd") & "', '" & Format(Val(Qty.Text), "N2") & "')"
                ExecuteTrans(SQL)


                Dim x_no_urutkontainer_masuk As Integer = 0
                SQL = "select IDENT_CURRENT('Kontainer_Masuk') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        x_no_urutkontainer_masuk = Dr("urutan")
                    End If
                End Using


                'simpan ke emi pembelian loading pabrik
                SQL = "select kode_perusahaan from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' and no_fak_submit_po = '" & faktur.Text & "' and "
                SQL = SQL & "no_plat = '" & Kontainer.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        Get_No_Faktur()
                        SQL = "insert into EMI_Pembelian_Loading(kode_perusahaan,no_faktur,kode_supplier,lokasi,no_sj,no_plat,driver,tanggal,jam,userid,Flag_Import,no_fak_submit_po,no_seal) values ("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & fakturStr & "', '" & TxtSupplier.Text & "', '" & Lokasi_utama.Text & "','-', '" & Kontainer.Text & "' , '-',  "
                        SQL = SQL & " '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "', 'Y', '" & faktur.Text & "', '" & Seal.Text & "' ) "
                        ExecuteTrans(SQL)
                    End If
                End Using

                '=======================
                '=     GET URUT PO     =
                '=======================
                Dim urut_PO As String = ""
                Dim NoPO As String = ""
                SQL = "select No_PO, c.No_Urut "
                SQL = SQL & "from submit_PO a, Rencana_Order b, EMI_Pembelian_PO_detail c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.ID_Rencana = b.ID_Rencana "
                SQL = SQL & "and b.No_PO = c.No_Faktur "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.no_faktur = '" & faktur.Text & "' "
                SQL = SQL & "and c.kode_barang = '" & kode.Text & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then

                        urut_PO = dr("No_Urut")
                        NoPO = dr("No_PO")

                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Urut PO Tidak diTemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                End Using


                Dim satuanBarang As String = ""
                Dim isi_Per_Bags As Double = 0
                Dim Satuan_Isi_Bags As String = ""
                SQL = "select satuan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags, isnull(Satuan_Isi_Bags,'') as Satuan_Isi_Bags from barang where kode_perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and kode_stock_owner = '" & Lokasi.Text & "' "
                SQL = SQL & "and kode_barang = '" & kode.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        satuanBarang = Dr("satuan")
                        isi_Per_Bags = Dr("isi_Per_Bags")
                        Satuan_Isi_Bags = Dr("Satuan_Isi_Bags")
                    Else
                        CloseTrans()
                        CloseTransSQL()
                        CloseConn()
                        CloseConnSQL()
                        MessageBox.Show("error insert purchase order, satuan barang tidak ditemukan")
                        Exit Sub
                    End If
                End Using


                SQL = "insert into EMI_Pembelian_Loading_Detail(Kode_Perusahaan,No_Faktur,No_PO,Urut_PO,Kode_Stock_Owner,Kode_Barang,Tanggal_Produksi,Tanggal_Expired,"
                SQL = SQL & "Jumlah,Satuan,Jumlah_Barang,Jumlah_Masuk,Satuan_Barang, No_Urut_B2B, jumlah_per_bag, No_Batch, Satuan_Per_Bag, harga_barang) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & fakturStr & "', '" & NoPO & "', '" & urut_PO & "', '" & Lokasi.Text & "', '" & kode.Text & "',  "
                SQL = SQL & "'" & Format(DTP_TglProduksi.Value, "yyyy-MM-dd") & "','" & Format(DTP_TglExpired.Value, "yyyy-MM-dd") & "', '" & Format(Val(Qty.Text), "N2") & "', '" & sat & "',"
                SQL = SQL & "'" & jml_brg & "','" & 0 & "','" & sat_brg & "', NULL, '" & isi_Per_Bags & "', '-', '" & Satuan_Isi_Bags & "', NULL)"
                ExecuteTrans(SQL)



            Else
                SQL = "Update Kontainer_Masuk set Qty = '" & Format(Val(Qty.Text), "N2") & "' Where No_Faktur = '" & faktur.Text & "' and No_Container = '" & Kontainer.Text & "' "
                SQL = SQL & "and Kode_Barang = '" & kode.Text & "' and Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Lokasi.Text & "' "
                ExecuteTrans(SQL)

                Dim urt As Integer = 0
                SQL = "select Urut_Oto from Kontainer_Masuk where No_Faktur = '" & faktur.Text & "' and No_Container = '" & Kontainer.Text & "' "
                SQL = SQL & "and Kode_Barang = '" & kode.Text & "' and Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Lokasi.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        urt = Dr("Urut_Oto")
                    End If
                End Using

                '=======================
                '=     GET URUT PO     =
                '=======================
                Dim urut_PO As String = ""
                Dim NoPO As String = ""
                SQL = "select No_PO, c.No_Urut "
                SQL = SQL & "from submit_PO a, Rencana_Order b, EMI_Pembelian_PO_detail c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.ID_Rencana = b.ID_Rencana "
                SQL = SQL & "and b.No_PO = c.No_Faktur "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.no_faktur = '" & faktur.Text & "' "
                SQL = SQL & "and c.kode_barang = '" & kode.Text & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then

                        urut_PO = dr("No_Urut")
                        NoPO = dr("No_PO")

                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Urut PO Tidak diTemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                End Using

                Dim satuanBarang As String = ""
                Dim isi_Per_Bags As Double = 0
                Dim Satuan_Isi_Bags As String = ""
                SQL = "select satuan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags, isnull(Satuan_Isi_Bags,'') as Satuan_Isi_Bags from barang where kode_perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and kode_stock_owner = '" & Lokasi.Text & "' "
                SQL = SQL & "and kode_barang = '" & kode.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        satuanBarang = Dr("satuan")
                        isi_Per_Bags = Dr("isi_Per_Bags")
                        Satuan_Isi_Bags = Dr("Satuan_Isi_Bags")
                    Else
                        CloseTrans()
                        CloseTransSQL()
                        CloseConn()
                        CloseConnSQL()
                        MessageBox.Show("error insert purchase order, satuan barang tidak ditemukan")
                        Exit Sub
                    End If
                End Using

                SQL = "update EMI_Pembelian_Loading_Detail set EMI_Pembelian_Loading_Detail.Jumlah = '" & Format(Val(Qty.Text), "N2") & "',EMI_Pembelian_Loading_Detail.Satuan = '" & sat & "',"
                SQL = SQL & "EMI_Pembelian_Loading_Detail.Jumlah_Barang = '" & jml_brg & "',EMI_Pembelian_Loading_Detail.Jumlah_Masuk = '" & 0 & "',EMI_Pembelian_Loading_Detail.Satuan_Barang = '" & sat_brg & "',"
                SQL = SQL & "EMI_Pembelian_Loading_Detail.Tanggal_Produksi = '" & Format(DTP_TglProduksi.Value, "yyyy-MM-dd") & "',"
                SQL = SQL & "EMI_Pembelian_Loading_Detail.Tanggal_Expired = '" & Format(DTP_TglExpired.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "EMI_Pembelian_Loading_Detail.Jumlah_Per_Bag = '" & isi_Per_Bags & "', "
                SQL = SQL & "EMI_Pembelian_Loading_Detail.Satuan_Per_Bag = '" & Satuan_Isi_Bags & "' "

                SQL = SQL & "from EMI_Pembelian_Loading_Detail "
                SQL = SQL & "INNER JOIN EMI_Pembelian_Loading on EMI_Pembelian_Loading.Kode_Perusahaan = EMI_Pembelian_Loading_Detail.Kode_Perusahaan  and "
                SQL = SQL & "EMI_Pembelian_Loading.No_Faktur = EMI_Pembelian_Loading_Detail.No_Faktur "

                SQL = SQL & "where EMI_Pembelian_Loading_Detail.Kode_Perusahaan = '" & KodePerusahaan & "' and EMI_Pembelian_Loading.no_plat = '" & Kontainer.Text & "' and "
                SQL = SQL & "EMI_Pembelian_Loading_Detail.Urut_PO = '" & urut_PO & "' and EMI_Pembelian_Loading_Detail.No_PO = '" & NoPO & "'  and "
                SQL = SQL & "EMI_Pembelian_Loading_Detail.Kode_Barang = '" & kode.Text & "' and EMI_Pembelian_Loading_Detail.Kode_Stock_Owner = '" & Lokasi.Text & "'"
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Berhasil di simpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Dim finalQty As Double = Format(Val(Qty.Text), "N2") - nilai_update
        Loading_Barang_Import.get_ubah(index, finalQty)
        Loading_Barang_Import.Cek_Bahan()
        Me.Close()
        ''
    End Sub

    Private Sub Qty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Qty.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
        If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(44) Or e.KeyChar = Chr(46)) Then e.Handled = True
    End Sub


End Class
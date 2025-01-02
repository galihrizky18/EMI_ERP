Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Pengiriman_Validasi

    Dim arrIndex As New ArrayList

    Dim no_formula, no_inquiry, kode_customer, kode_barang As String

    Private Sub ListView1T_DoubleClick(sender As Object, e As EventArgs) Handles ListView1T.DoubleClick
        EMI_Pengiriman_Validasi_SD_Ekspedisi.txtGudang.Text = ListView1T.FocusedItem.SubItems(1).Text
        EMI_Pengiriman_Validasi_SD_Ekspedisi.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Try

            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction


            For i As Integer = 0 To ListView1T.Items.Count - 1
                If ListView1T.Items(i).SubItems(3).Text = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Master_CekPengiriman_Err_Ekspedisi_Kosong, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If


                SQL = "update emi_po_detail set id_ekspedisi = '" & ListView1T.Items(i).SubItems(5).Text & "', flag_pengiriman = 'Y' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1T.Items(i).Text & "' "
                SQL = SQL & "and id_gudang = '" & ListView1T.Items(i).SubItems(4).Text & "' "
                ExecuteTrans(SQL)

                SQL = "update Emi_Customer_Gudang set id_ekspedisi = '" & ListView1T.Items(i).SubItems(5).Text & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Urut_oto = '" & ListView1T.Items(i).SubItems(4).Text & "' "
                ExecuteTrans(SQL)
            Next



            SQL = "Select "
            SQL = SQL & "isnull((select 'Y' from emi_Produksi_terpenuhi x where "
            SQL = SQL & "x.Kode_Perusahaan = b.Kode_Perusahaan And x.No_PO = b.No_faktur And "
            SQL = SQL & "x.Kode_Barang = b.Kode_Barang And x.Kode_stock_Owner = b.Kode_stock_Owner And x.status Is Null),'T') as ada_data "
            SQL = SQL & "From view_po a, view_po_detail b Where b.flag_bahan_Penolong ='Y' and b.flag_bahan_Baku='Y' and b.flag_pengiriman='Y' and "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.no_faktur "
            SQL = SQL & "And b.No_faktur='" & ListView1T.Items(0).SubItems(0).Text & "' "
            Using dr = OpenTrans(SQL)


                If dr.Read Then
                    If dr("ada_data") = "T" Then
                        dr.Close()
                        SQL = "insert emi_Produksi_terpenuhi(Kode_Perusahaan, no_Po, Kode_Customer, "
                        SQL = SQL & "Kode_barang, Kode_Stock_Owner, jumlah, satuan, jumlah_sisa,indx, Lokasi) "
                        SQL = SQL & "Select b.Kode_Perusahaan, b.no_faktur, a.Kode_customer, b.Kode_barang, b.Kode_stock_Owner, b.jumlah, b.satuan, b.jumlah, 999, a.Lokasi "
                        SQL = SQL & "From view_po a, view_po_detail b Where b.flag_bahan_Penolong ='Y' and b.flag_bahan_Baku='Y' and b.flag_pengiriman='Y' and "
                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.no_faktur "
                        SQL = SQL & "And b.No_faktur='" & ListView1T.Items(0).SubItems(0).Text & "' "
                        '     SQL = SQL & "group by b.kode_perusahaan,b.no_faktur,a.kode_customer,b.kode_barang,b.kode_stock_owner,b.satuan"
                        ExecuteTrans(SQL)

                    End If
                End If
            End Using

            SQL = "update emi_po set flag_pengiriman = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1T.Items(0).SubItems(0).Text & "'"
            ExecuteTrans(SQL)
            Cmd.Transaction.Commit()
            CloseConn()

            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            EMI_Pengiriman_Validasi_Display.cari("Y")
            Me.Close()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        kosong()
    End Sub


    Private Sub SD_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            Base_Language.Get_Languages(Bahasa_Pilihan, "Master_Cek_Pengiriman")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        ListView1T.Columns.Clear()
        ListView1T.Columns.Add(Base_Language.Lang_Global_No_PO, 120, HorizontalAlignment.Left)
        ListView1T.Columns.Add(Base_Language.Lang_Global_Gudang, 300, HorizontalAlignment.Left)
        ListView1T.Columns.Add(Base_Language.Lang_Global_Kota, 200, HorizontalAlignment.Left)
        ListView1T.Columns.Add(Base_Language.Lang_Global_Ekspedisi, 150, HorizontalAlignment.Left)
        ListView1T.Columns.Add("Id gudang", 0, HorizontalAlignment.Left)
        ListView1T.Columns.Add("Id Eskepsidi", 0, HorizontalAlignment.Left)
        ListView1T.View = View.Details


        Label1.Text = Base_Language.Lang_Master_CekPengiriman_Judul_Sd
        Button1.Text = Base_Language.Lang_Global_Simpan



        kosong()

    End Sub


    Private Sub kosong()


        Try
            OpenConn()


            ListView1T.Items.Clear()
            SQL = "select a.No_Faktur,a.Id_Gudang, b.Alamat_Penerima,c.nama_kabupaten_kota,  "
            SQL = SQL & "ISNULL((select Kode_Ekspedisi from EMI_Master_Ekspedisi x where b.Kode_Perusahaan = x.kode_perusahaan  "
            SQL = SQL & "and b.id_ekspedisi = x.Id_Ekspedisi), NULL) as Ekspedisi "
            SQL = SQL & "from EMI_PO_Detail a, Emi_Customer_Gudang b, tbl_kabupaten_kota c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Gudang = b.urut_Oto "
            SQL = SQL & "and b.Id_Kabupaten_Kota = c.id_kabupaten_kota "
            SQL = SQL & "and a.no_faktur = '" & EMI_Pengiriman_Validasi_Display.ListView1T.FocusedItem.Text & "' "
            SQL = SQL & "and a.flag_pengiriman is null "
            SQL = SQL & "group by b.Kode_Perusahaan, a.No_Faktur,a.Id_Gudang, b.id_ekspedisi, b.Alamat_Penerima, c.nama_kabupaten_kota "
            SQL = SQL & "order by id_gudang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1T.Items.Add(Dr("no_faktur"))

                    lvw.SubItems.Add(Dr("alamat_penerima"))
                    lvw.SubItems.Add(Dr("nama_kabupaten_kota"))
                    If General_Class.CekNULL(Dr("ekspedisi")) = "" Then
                        lvw.SubItems.Add("")
                    Else
                        lvw.SubItems.Add(General_Class.CekNULL(Dr("Ekspedisi")))
                    End If
                    lvw.SubItems.Add(Dr("id_gudang"))
                    lvw.SubItems.Add("")
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub








End Class
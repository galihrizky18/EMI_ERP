Imports System.Diagnostics.Eventing.Reader
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Microsoft.VisualBasic.ApplicationServices


Public Class EMI_Permintaan_BB_Validasi
    Dim Jenis = "Validasi_Permintaan_BB"
    Dim LvNo_Faktur As String
    Dim LvNo_Rencana As String
    Dim LvLokasi As String
    Dim LvKd_Cust As String
    Dim LvNm_Cust As String
    Dim LvPenanggung As String
    Dim LvTgl_Pro As String
    Dim LvJam_Pro As String
    Dim LvKet As String

    Dim LvJenis As String
    Dim LvSO As String
    Dim LvKd_Brg As String
    Dim LvNm_Brg As String
    Dim LvJumlah As String
    Dim LvSatuan As String
    Dim LvUrut As String
    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvNo_Faktur = ListView1.Items(No_Index).Text
        LvNo_Rencana = ListView1.Items(No_Index).SubItems(1).Text
        LvLokasi = ListView1.Items(No_Index).SubItems(2).Text
        LvKd_Cust = ListView1.Items(No_Index).SubItems(3).Text
        LvNm_Cust = ListView1.Items(No_Index).SubItems(4).Text
        LvPenanggung = ListView1.Items(No_Index).SubItems(5).Text
        LvTgl_Pro = ListView1.Items(No_Index).SubItems(6).Text
        LvJam_Pro = ListView1.Items(No_Index).SubItems(7).Text
        LvKet = ListView1.Items(No_Index).SubItems(8).Text
    End Sub

    Private Sub Get_Isi_Listview_Bwh(ByVal No_Index As Integer)
        LvJenis = ListView2.Items(No_Index).Text
        LvSO = ListView2.Items(No_Index).SubItems(1).Text
        LvKd_Brg = ListView2.Items(No_Index).SubItems(2).Text
        LvNm_Brg = ListView2.Items(No_Index).SubItems(3).Text
        LvJumlah = ListView2.Items(No_Index).SubItems(4).Text
        LvSatuan = ListView2.Items(No_Index).SubItems(5).Text
        LvUrut = ListView2.Items(No_Index).SubItems(6).Text
    End Sub

    Private Sub Display_Validasi_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Validasi_Permintaan_BB_Judul
            Button1.Text = Base_Language.Lang_Global_Cari

            ComboBox6.Items.Clear()
            xSplit = CekKotaRole().Split(",")
            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)

            SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            'ComboBox1.Items.Add("Seluruh")
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            ComboBox6.Text = Lokasi

            ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 130, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Validasi_Permintaan_BB_No_Rencana, 130, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Lokasi, 130, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 0, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 0, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Penangung_Jawab, 140, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal_Produksi, 130, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jam, 90, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.lang_global_keterangan, 250, HorizontalAlignment.Left)
            ListView1.View = View.Details

            ListView2.Columns.Add(Base_Language.Lang_Global_Jenis, 140, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_Lokasi, 140, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 130, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 310, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_Jumlah, 130, HorizontalAlignment.Center)
            ListView2.Columns.Add(Base_Language.Lang_Global_Satuan, 130, HorizontalAlignment.Center)
            ListView2.Columns.Add("Urut", 0, HorizontalAlignment.Center)
            ListView2.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Button1_Click(Me, e)
    End Sub

    Private Sub Display_Validasi_Pembelian_Barang_Masuk_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub



    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            OpenConn()

            If ListView1.Items.Count = 0 Then Exit Sub
            ListView2.Items.Clear()
            SQL = "select a.Jenis,a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.Jumlah,a.Satuan "
            SQL = SQL & ",a.No_Urut from Emi_Permintaan_Bahan_Baku_Detail a,Barang b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner and "
            SQL = SQL & "a.Kode_Barang = b.Kode_Barang and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & ListView1.FocusedItem.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView2.Items.Add(.Rows(i).Item("Jenis"))
                        Lvw.SubItems.Add(.Rows(i).Item("Kode_Stock_Owner"))
                        Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang"))
                        Lvw.SubItems.Add(.Rows(i).Item("Nama"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah"), "N0"))
                        Lvw.SubItems.Add(.Rows(i).Item("Satuan"))
                        Lvw.SubItems.Add(.Rows(i).Item("No_Urut"))
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Validasi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tny As String = MessageBox.Show(Base_Language.Lang_GLOBAL_Masuk_Tny_Val_kurang, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If tny = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("validasi_permintaan_BB") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show(Base_Language.Lang_Global_Error_Tdk_Ada_Akses, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim NO_RencanaProduksi As String = ""
            Get_Isi_Listview(ListView1.FocusedItem.Index)
            SQL = "select Status,Flag_Val,No_RencanaProduksi from Emi_Permintaan_Bahan_Baku where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & LvNo_Faktur & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    NO_RencanaProduksi = Dr("No_RencanaProduksi")
                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_DataSudahBatal, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Flag_Val")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Sdh_Val, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using

            SQL = "Select Kode_Perusahaan, No_Po, No_Urut, Jenis, Kode_Stock_Owner, Kode_Barang, Jumlah_Barang, Satuan_Barang, "
            SQL = SQL & "Kode_Stock_Owner_Bahan, Kode_Bahan, Jumlah, satuan from EMI_Permintaan_Bahan_Baku_detail2 a "
            SQL = SQL & "Where Kode_Perusahaan ='" & KodePerusahaan & "' and No_faktur='" & LvNo_Faktur & "' "
            Using dss = BindingTrans(SQL)
                With dss.Tables("MyTable")
                    For a As Integer = 0 To .Rows.Count - 1

                        Dim Urut_detail2 As Integer = .Rows(a).Item("No_Urut")
                        Dim NO_PO As String = .Rows(a).Item("No_Po")
                        Dim Kd_barang As String = .Rows(a).Item("Kode_Barang")
                        Dim Kd_bahan As String = .Rows(a).Item("Kode_Bahan")
                        Dim Lks_barang As String = .Rows(a).Item("Kode_Stock_Owner")
                        Dim Lks_bahan As String = .Rows(a).Item("Kode_Stock_Owner_Bahan")
                        Dim jumlah_Butuh As Double = .Rows(a).Item("Jumlah")

                        SQL = "select urut, serial_number, sisa from View_Prepare_KeepStock where no_PO='" & NO_PO & "' "
                        SQL = SQL & "and Kode_stock_owner='" & Lks_barang & "' and Kode_Barang='" & Kd_barang & "' "
                        SQL = SQL & " And Kode_stock_owner_bahan ='" & Lks_bahan & "' and Kode_Bahan='" & Kd_bahan & "' "
                        Using ds2 = BindingTrans(SQL)
                            For index As Integer = 0 To ds2.Tables("MyTable").Rows.Count - 1

                                Dim urut_PO As String = ds2.Tables("MyTable").Rows(index).Item("urut")
                                Dim sn As String = ds2.Tables("MyTable").Rows(index).Item("serial_number")
                                Dim sisa As Double = ds2.Tables("MyTable").Rows(index).Item("sisa")

                                Dim nilai_Pakai As Double = 0

                                If jumlah_Butuh > sisa Then
                                    nilai_Pakai = sisa
                                    jumlah_Butuh = jumlah_Butuh - sisa
                                Else
                                    nilai_Pakai = jumlah_Butuh
                                    jumlah_Butuh = jumlah_Butuh - jumlah_Butuh
                                End If

                                SQL = "Select kode_barang, Flag_PPN, stock_PO from barang where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & Lks_bahan & "' and "
                                SQL = SQL & "kode_barang = '" & Kd_bahan & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        If nilai_Pakai > Dr("stock_PO") Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show(Base_Language.Lang_GLOBAL_Masuk_Error_Minus_Stock, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub

                                        Else
                                            SQL = "Update barang set stock_PO = stock_PO - " & nilai_Pakai & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & Lks_bahan & "' and "
                                            SQL = SQL & "kode_barang = '" & Kd_bahan & "' "
                                            Dr.Close()
                                            ExecuteTrans(SQL)
                                        End If
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Base_Language.Lang_Global_Barang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using


                                SQL = "select kode_stock_owner, kode_barang, serial_number, stock_PO from "
                                SQL = SQL & "barang_sn where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & Lks_bahan & "' and "
                                SQL = SQL & "kode_barang = '" & Kd_bahan & "' and stock_PO <> 0 "
                                SQL = SQL & "and serial_number='" & sn & "'"
                                SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        If nilai_Pakai > Dr("stock_PO") Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show(Base_Language.Lang_GLOBAL_Masuk_Error_Minus_Stock, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub

                                        Else
                                            SQL = "Update barang_sn set stock_PO = stock_PO - " & nilai_Pakai & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & Lks_bahan & "' and "
                                            SQL = SQL & "kode_barang = '" & Kd_bahan & "' and "
                                            SQL = SQL & "serial_number = '" & sn & "'"
                                            Dr.Close()
                                            ExecuteTrans(SQL)
                                        End If
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Base_Language.Lang_Global_Barang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "insert into emi_permintaan_bahan_baku_detail_stock(Kode_Perusahaan, No_Faktur, Urut_Detail2, Serial_Number, urut_PO, Jumlah) "
                                SQL = SQL & "values('" & KodePerusahaan & "', '" & LvNo_Faktur & "', '" & Urut_detail2 & "', "
                                SQL = SQL & " '" & sn & "', '" & urut_PO & "', '" & nilai_Pakai & "') "
                                ExecuteTrans(SQL)
                            Next
                        End Using

                        If jumlah_Butuh <> 0 Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_GLOBAL_Terjadi_Kesalahan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If


                    Next
                End With
            End Using

            SQL = "update EMI_Rencana_Produksi set Flag_PermintaanBB = 'Y' where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & NO_RencanaProduksi & "'"
            ExecuteTrans(SQL)

            SQL = "update Emi_Permintaan_Bahan_Baku set Flag_Val = 'Y' where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & LvNo_Faktur & "'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Validasi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Button1_Click(ValidasiToolStripMenuItem, e)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            OpenConn()

            ListView1.Items.Clear()
            ListView2.Items.Clear()
            SQL = "select a.No_Faktur,a.No_RencanaProduksi,a.Lokasi, "
            SQL = SQL & "a.userid,a.tanggal_Produksi,a.Jam_Produksi,a.Keterangan "
            SQL = SQL & "from Emi_Permintaan_Bahan_Baku a where "
            SQL = SQL & "Flag_Val is null and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Status is null and tanggal_Produksi is not null "
            If ComboBox6.SelectedIndex = -1 Then
                SQL = SQL & ""
            Else
                SQL = SQL & "and a.Lokasi = '" & ComboBox6.Text & "' "
            End If
            SQL = SQL & " order by a.tanggal_Produksi,a.Jam_Produksi"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView1.Items.Add(.Rows(i).Item("No_Faktur"))
                        Lvw.SubItems.Add(.Rows(i).Item("No_RencanaProduksi"))
                        Lvw.SubItems.Add(.Rows(i).Item("Lokasi"))
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add(.Rows(i).Item("userid"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_Produksi"), "dd MMM yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("Jam_Produksi"))
                        Lvw.SubItems.Add(.Rows(i).Item("Keterangan"))
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class
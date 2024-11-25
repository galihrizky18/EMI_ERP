Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Azure.Storage.Internal

Public Class EMI_Refraksi_Display2
    Dim Jenis = "Transaksi_Produksi"
    Public asal As String
    Dim arrcari As New ArrayList
    Public filter_tambahan As String

    Private Sub SD_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub

    Dim LvNoFak, LvKodeSO, LvKdBrg, LvNmBrg, LvNmSupplier, LvNoSJ, LvNoPlat, LvDriver, LvTgl, LvJam, LvTglMasuk, LvJamMasuk, LvUserID, LvTglOTW, LvETA, LvETD, LvJnsMuatan, LvTelepon, LvSatuan, LvSatuanBrg, LvKdSupplier As String

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvNoFak = Lv_DataRefraksi.Items(NoIndex).Text
        LvKodeSO = Lv_DataRefraksi.Items(NoIndex).SubItems(1).Text
        LvKdBrg = Lv_DataRefraksi.Items(NoIndex).SubItems(2).Text
        LvNmBrg = Lv_DataRefraksi.Items(NoIndex).SubItems(3).Text
        LvNmSupplier = Lv_DataRefraksi.Items(NoIndex).SubItems(4).Text
        LvNoSJ = Lv_DataRefraksi.Items(NoIndex).SubItems(5).Text
        LvNoPlat = Lv_DataRefraksi.Items(NoIndex).SubItems(6).Text
        LvDriver = Lv_DataRefraksi.Items(NoIndex).SubItems(7).Text
        LvTgl = Lv_DataRefraksi.Items(NoIndex).SubItems(8).Text
        LvJam = Lv_DataRefraksi.Items(NoIndex).SubItems(9).Text
        LvTglMasuk = Lv_DataRefraksi.Items(NoIndex).SubItems(10).Text
        LvJamMasuk = Lv_DataRefraksi.Items(NoIndex).SubItems(11).Text
        LvUserID = Lv_DataRefraksi.Items(NoIndex).SubItems(12).Text
        LvTglOTW = Lv_DataRefraksi.Items(NoIndex).SubItems(13).Text
        LvETA = Lv_DataRefraksi.Items(NoIndex).SubItems(14).Text
        LvETD = Lv_DataRefraksi.Items(NoIndex).SubItems(15).Text
        LvJnsMuatan = Lv_DataRefraksi.Items(NoIndex).SubItems(16).Text
        LvTelepon = Lv_DataRefraksi.Items(NoIndex).SubItems(17).Text
        LvSatuan = Lv_DataRefraksi.Items(NoIndex).SubItems(18).Text
        LvSatuanBrg = Lv_DataRefraksi.Items(NoIndex).SubItems(19).Text
        LvKdSupplier = Lv_DataRefraksi.Items(NoIndex).SubItems(20).Text
    End Sub

    Public Sub kosong()
        Label1.Text = "Display Refraksi"
        get_jam()

        Lv_DataRefraksi.Columns.Clear()
        Lv_DataRefraksi.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Nama Supplier", 200, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("No SJ", 100, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("No Plat", 100, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Driver", 100, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("Tgl Masuk", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("Jam Masuk", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("UserID", 80, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("Tgl OTW", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("ETA", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("ETD", 0, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Jenis Muatan", 0, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Telepon", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("Satuan", 0, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Satuan Barang", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("KdSupplier", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.View = View.Details

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label3.Text = Base_Language.Lang_Global_Jenis
            Btn_Cari.Text = Base_Language.Lang_Global_Cari

            ComboBox3.Items.Clear() : arrcari.Clear()
            ComboBox3.Items.Add(Base_Language.Lang_Global_NoFaktur) : arrcari.Add("a.No_Faktur")
            ComboBox3.SelectedIndex = -1
            TextBox3.Text = ""

            Try
                OpenConn()

                Lv_DataRefraksi.Items.Clear()
                SQL = "SELECT a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama AS Nama_Barang, d.Nama AS Nama_Supplier, d.Kode_Supplier, "
                SQL = SQL & "a.No_SJ, a.No_Plat, a.Driver, a.Tanggal, a.Jam, a.Tanggal_Masuk, a.Jam_Masuk, a.UseriD, a.Tanggal_OTW, a.ETA, a.ETD, "
                SQL = SQL & "e.keterangan AS jenis_muatan, a.telpon, b.satuan, b.satuan_barang "
                SQL = SQL & "FROM emi_pembelian_loading a, emi_pembelian_loading_detail b, barang c, Suppliers d, emi_master_jenis_muatan e "
                SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "AND b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "AND a.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & "AND a.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "AND a.No_Faktur = b.No_Faktur "
                SQL = SQL & "AND b.Kode_Stock_Owner = c.Kode_Stock_Owner "
                SQL = SQL & "AND b.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "AND a.Kode_Supplier = d.Kode_Supplier "
                SQL = SQL & "AND a.ID_Jenis_Muatan = e.Id_Jenis_Muatan "
                SQL = SQL & "AND a.status IS NULL "
                SQL = SQL & "AND b.Warna = 'KUNING' "
                SQL = SQL & "AND b.Flag_Refraksi IS NULL "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "GROUP BY a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, d.Nama, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal, a.Jam, a.Tanggal_Masuk, "
                SQL = SQL & "a.Jam_Masuk, a.UseriD, a.Tanggal_OTW, a.ETA, a.ETD, e.keterangan, a.telpon, b.satuan, b.satuan_barang, d.Kode_Supplier "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                Dim Lvw As New ListViewItem
                                Lvw = Lv_DataRefraksi.Items.Add(.Rows(i).Item("no_faktur"))
                                Lvw.SubItems.Add(.Rows(i).Item("Kode_Stock_Owner"))
                                Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang"))
                                Lvw.SubItems.Add(.Rows(i).Item("Nama_Barang"))
                                Lvw.SubItems.Add(.Rows(i).Item("Nama_Supplier"))
                                Lvw.SubItems.Add(.Rows(i).Item("No_SJ"))
                                Lvw.SubItems.Add(.Rows(i).Item("No_Plat"))
                                Lvw.SubItems.Add(.Rows(i).Item("Driver"))
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                                Lvw.SubItems.Add(.Rows(i).Item("Jam"))
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal_Masuk"), "dd MMM yyyy"))
                                Lvw.SubItems.Add(.Rows(i).Item("Jam_Masuk"))
                                Lvw.SubItems.Add(.Rows(i).Item("UseriD"))
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal_OTW"), "dd MMM yyyy"))
                                If General_Class.CekNULL(.Rows(i).Item("ETA")) = "" Then
                                    Lvw.SubItems.Add("-")
                                Else
                                    Lvw.SubItems.Add(Format(.Rows(i).Item("ETA"), "dd MMM yyyy"))
                                End If
                                If General_Class.CekNULL(.Rows(i).Item("ETD")) = "" Then
                                    Lvw.SubItems.Add("-")
                                Else
                                    Lvw.SubItems.Add(Format(.Rows(i).Item("ETD"), "dd MMM yyyy"))
                                End If
                                Lvw.SubItems.Add(.Rows(i).Item("jenis_muatan"))
                                Lvw.SubItems.Add(.Rows(i).Item("telpon"))
                                Lvw.SubItems.Add(.Rows(i).Item("satuan"))
                                Lvw.SubItems.Add(.Rows(i).Item("satuan_barang"))
                                Lvw.SubItems.Add(.Rows(i).Item("Kode_Supplier"))
                            Next
                        End If
                    End With
                End Using

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Try
            OpenConn()

            Lv_DataRefraksi.Items.Clear()
            '''SQL = "select No_Faktur,Tanggal_Produksi,Jam_Produksi,Line from emi_rencana_produksi "
            '''SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            '''SQL = SQL & "Status is null and Selesai is null "
            SQL = "SELECT a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama AS Nama_Barang, d.Nama AS Nama_Supplier, d.Kode_Supplier, "
            SQL = SQL & "a.No_SJ, a.No_Plat, a.Driver, a.Tanggal, a.Jam, a.Tanggal_Masuk, a.Jam_Masuk, a.UseriD, a.Tanggal_OTW, a.ETA, a.ETD, "
            SQL = SQL & "e.keterangan AS jenis_muatan, a.telpon, b.satuan, b.satuan_barang "
            SQL = SQL & "FROM emi_pembelian_loading a, emi_pembelian_loading_detail b, barang c, Suppliers d, emi_master_jenis_muatan e "
            SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "AND b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "AND a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "AND a.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "AND a.No_Faktur = b.No_Faktur "
            SQL = SQL & "AND b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "AND b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "AND a.Kode_Supplier = d.Kode_Supplier "
            SQL = SQL & "AND a.ID_Jenis_Muatan = e.Id_Jenis_Muatan "
            SQL = SQL & "AND a.status IS NULL "
            SQL = SQL & "AND b.Warna = 'KUNING' "
            SQL = SQL & "AND b.Flag_Refraksi IS NULL "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & " " & filter_tambahan & " "
            If ComboBox3.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(ComboBox3.SelectedIndex) & "  like  '%" & Trim(TextBox3.Text) & "%' "
            End If
            SQL = SQL & "GROUP BY a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, d.Nama, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal, a.Jam, a.Tanggal_Masuk, "
            SQL = SQL & "a.Jam_Masuk, a.UseriD, a.Tanggal_OTW, a.ETA, a.ETD, e.keterangan, a.telpon, b.satuan, b.satuan_barang, d.Kode_Supplier "

            '''Using dr = OpenTrans(SQL)
            '''    Do While dr.Read
            '''        Dim lvw As ListViewItem
            '''        lvw = Lv_DataRefraksi.Items.Add(dr("No_Faktur"))
            '''        lvw.SubItems.Add(Format(dr("Tanggal_Produksi"), "dd MMMM yyyy"))
            '''        lvw.SubItems.Add(dr("Jam_Produksi"))
            '''        lvw.SubItems.Add(dr("Line"))
            '''    Loop
            '''End Using
            '''
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_DataRefraksi.Items.Add(.Rows(i).Item("no_faktur"))
                            Lvw.SubItems.Add(.Rows(i).Item("Kode_Stock_Owner"))
                            Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang"))
                            Lvw.SubItems.Add(.Rows(i).Item("Nama_Barang"))
                            Lvw.SubItems.Add(.Rows(i).Item("Nama_Supplier"))
                            Lvw.SubItems.Add(.Rows(i).Item("No_SJ"))
                            Lvw.SubItems.Add(.Rows(i).Item("No_Plat"))
                            Lvw.SubItems.Add(.Rows(i).Item("Driver"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            Lvw.SubItems.Add(.Rows(i).Item("Jam"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal_Masuk"), "dd MMM yyyy"))
                            Lvw.SubItems.Add(.Rows(i).Item("Jam_Masuk"))
                            Lvw.SubItems.Add(.Rows(i).Item("UseriD"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal_OTW"), "dd MMM yyyy"))
                            If General_Class.CekNULL(.Rows(i).Item("ETA")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("ETA"), "dd MMM yyyy"))
                            End If
                            If General_Class.CekNULL(.Rows(i).Item("ETD")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("ETD"), "dd MMM yyyy"))
                            End If
                            Lvw.SubItems.Add(.Rows(i).Item("jenis_muatan"))
                            Lvw.SubItems.Add(.Rows(i).Item("telpon"))
                            Lvw.SubItems.Add(.Rows(i).Item("satuan"))
                            Lvw.SubItems.Add(.Rows(i).Item("satuan_barang"))
                            Lvw.SubItems.Add(.Rows(i).Item("Kode_Supplier"))
                        Next
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


    Private Sub Lv_DataRefraksi_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataRefraksi.DoubleClick
        If Lv_DataRefraksi.Items.Count = 0 Or Lv_DataRefraksi.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        'EMI_Refraksi.getDataNoFak = LvNoFak
        'EMI_Refraksi.getDataKodeSO = LvKodeSO
        'EMI_Refraksi.getDataKdBrg = LvKdBrg
        'EMI_Refraksi.getDataSatuan = LvSatuan
        'EMI_Refraksi.getDataSatuanBrg = LvSatuanBrg
        'EMI_Refraksi.getDataNoSJ = LvNoSJ

        'EMI_Refraksi.ShowDialog()

        Get_Isi_ListView(Lv_DataRefraksi.FocusedItem.Index)

        Emi_Refraksi2.getFakLoading = LvNoFak
        Emi_Refraksi2.getNoPlat = LvNoPlat
        Emi_Refraksi2.getNmSupplier = LvNmSupplier
        Emi_Refraksi2.getNmSupir = LvDriver
        Emi_Refraksi2.getKdSupplier = LvKdSupplier

        Emi_Refraksi2.ShowDialog()
    End Sub

End Class
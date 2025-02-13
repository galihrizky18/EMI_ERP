Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Emi_Display_Barang_Penawaran

    Dim arrSelectedBarang As New List(Of Object)()

    Public dari As String = ""

    Private Sub Emi_Display_Barang_Penawaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Initial_Lv()
        kosong()

    End Sub

    Private Sub kosong()

        Try
            OpenConn()

            ComboBox1.Items.Clear()
            SQL = "select Kode_Kategori_Besar from Kategori_Besar where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Kategori_Besar "
            Using dr = OpenTrans(SQL)
                ComboBox1.Items.Add("---SELURUH---")
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Kode_Kategori_Besar"))
                Loop
            End Using

            'ComboBox2.Items.Clear()
            'SQL = "select Kode_Kategori_Besar from Kategori_Kecil where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Kategori_Kecil "
            'Using dr = OpenTrans(SQL)
            '    ComboBox2.Items.Add("---SELURUH---")
            '    Do While dr.Read
            '        ComboBox2.Items.Add(dr("Kode_Kategori_Besar"))
            '    Loop
            'End Using

            ComboBox1.SelectedIndex = 0
            ComboBox2.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        If dari = "Transaksi Penawaran" Then
            Button2.Visible = True
            Button1.Visible = True
            Lv_Data.CheckBoxes = True
            CheckBox1.Visible = True
        ElseIf dari = "Summary Data" Then

            Button2.Visible = False
            Button1.Visible = False
            Lv_Data.CheckBoxes = False
            CheckBox1.Visible = False

        End If


        arrSelectedBarang.Clear()
        Txt_Nama.Text = ""

        CheckBox1.Checked = False
        Lv_Data.Items.Clear()
        LoadData()

    End Sub


    Private Sub Initial_Lv()

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Nama", 300, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("PPN", 100, HorizontalAlignment.Center)
        Lv_Data.View = View.Details

    End Sub

    Private Sub LoadData()


        Try
            OpenConn()

            Dim arrDvUtama As New ArrayList

            '==========================================================
            '=     MASUKAN DATA DV MASTER PENAWARAN KEDALAM ARRAY     =
            '==========================================================
            arrDvUtama.Clear()
            For i As Integer = 0 To Transaksi_Penawaran.DgvMaster_Penawaran.Rows.Count - 1
                arrDvUtama.Add(Transaksi_Penawaran.DgvMaster_Penawaran.Rows(i).Cells(0).Value)
            Next


            get_jam()
            '===========================
            '=     LOAD BAHAN BAKU     =
            '===========================
            Lv_Data.Items.Clear()
            SQL = ";with cte as( "
            SQL = SQL & "(select a.kode_perusahaan, a.kode_barang, a.Nama, a.Kode_Kategori_Besar, a.Kode_Kategori_Kecil, a.Flag_PPN, "
            SQL = SQL & "ISNULL((Select top(1)'Y' from EMI_Master_Penawaran x, EMI_Master_Penawaran_Detail y where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur "
            SQL = SQL & "And x.Selesai Is null And '" & Format(tgl_skg, "yyyy-MM-dd") & "' between x.Tgl_Penawaran_Hrg And Periode_Akhir_Penawaran And y.Kode_Perusahaan = a.Kode_Perusahaan And y.Kode_Barang = a.Kode_Barang),null) As ada_data, "
            SQL = SQL & "ISNULL((select z.satuan from barang_detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and Flag_Tampil_Display = 'Y'), '-') as satuan_display "
            SQL = SQL & "from barang a,EMI_Group_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and b.flag_raw_material = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "group by a.kode_perusahaan, a.kode_barang, a.Nama, a.Kode_Kategori_Besar, a.Kode_Kategori_Kecil, a.Flag_PPN ) "
            SQL = SQL & "union all "
            SQL = SQL & "(select a.kode_perusahaan, a.kode_barang, a.Nama, a.Kode_Kategori_Besar, a.Kode_Kategori_Kecil, a.Flag_PPN, "
            SQL = SQL & "ISNULL((Select top(1)'Y' from EMI_Master_Penawaran x, EMI_Master_Penawaran_Detail y where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur "
            SQL = SQL & "And x.Selesai Is null And '" & Format(tgl_skg, "yyyy-MM-dd") & "' between x.Tgl_Penawaran_Hrg And Periode_Akhir_Penawaran And y.Kode_Perusahaan = a.Kode_Perusahaan And y.Kode_Barang = a.Kode_Barang),null) As ada_data, "
            SQL = SQL & "ISNULL((select z.satuan from barang_detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and Flag_Tampil_Display = 'Y'), '-') as satuan_display "
            SQL = SQL & "from barang a,EMI_Group_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and b.flag_packaging = 'Y'   "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "group by a.kode_perusahaan, a.kode_barang, a.Nama, a.Kode_Kategori_Besar, a.Kode_Kategori_Kecil, a.Flag_PPN )) "
            SQL = SQL & "SELECT * FROM CTE WHERE ada_data is null "
            If ComboBox1.SelectedIndex <> -1 And ComboBox1.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & "Kode_Kategori_Besar = '" & ComboBox1.Text & "' "
            End If
            If ComboBox2.SelectedIndex <> -1 And ComboBox2.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & "Kode_Kategori_Kecil = '" & ComboBox2.Text & "' "
            End If
            If Txt_Nama.Text.Trim.Length <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & "nama like '" & Txt_Nama.Text & "%' "
            End If

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            '=============================================================
                            '=     JIKA BARANG SUDAH DIPILIH, TIDAK AKAN TAMPIL LAGI     =
                            '=============================================================
                            If arrDvUtama.Contains(.Rows(i).Item("kode_barang")) Then
                                Continue For
                            End If

                            Dim Lv As New ListViewItem
                            Lv = Lv_Data.Items.Add(.Rows(i).Item("kode_barang"))
                            Lv.SubItems.Add(.Rows(i).Item("Nama"))
                            Lv.SubItems.Add(.Rows(i).Item("satuan_display"))

                            '===============
                            '=     PPN     =
                            '===============
                            If .Rows(i).Item("Flag_PPN") = "Y" Then
                                Lv.SubItems.Add("PPN")
                            Else
                                Lv.SubItems.Add("Non PPN")
                            End If

                            '========================================
                            '=     SET CHECKED INPUT SEBELUMNYA     =
                            '========================================
                            For j As Integer = 0 To arrSelectedBarang.Count - 1
                                Dim itemData As Object() = CType(arrSelectedBarang(j), Object())

                                If itemData.Contains(.Rows(i).Item("kode_barang")) Then
                                    Lv.Checked = True
                                    Exit For
                                End If
                            Next

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

    Private Sub Btn_Cari_Click_1(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        LoadData()
        CheckBox1.Checked = False
    End Sub

    Private Sub Lv_Data_Leave(sender As Object, e As EventArgs) Handles Lv_Data.Leave
        If Lv_Data.Items.Count = 0 Then Exit Sub

        arrSelectedBarang.Clear()

        For i As Integer = 0 To Lv_Data.Items.Count - 1
            If Lv_Data.Items(i).Checked = True Then

                'Dim itemToRemove As Object() = arrSelectedBarang.FirstOrDefault(Function(item) item(0).ToString() = Lv_Data.Items(i).SubItems(0).Text)

                'If itemToRemove IsNot Nothing Then
                '    ' Menghapus objek yang ditemukan
                '    arrSelectedBarang.Remove(itemToRemove)
                'End If
                Dim itemData As Object() = {
                        Lv_Data.Items(i).SubItems(0).Text,
                        Lv_Data.Items(i).SubItems(1).Text,
                        Lv_Data.Items(i).SubItems(2).Text,
                        Lv_Data.Items(i).SubItems(3).Text
                   }
                arrSelectedBarang.Add(itemData)
            End If
        Next

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            For a As Integer = 0 To Lv_Data.Items.Count - 1
                Lv_Data.Items(a).Checked = True
            Next
        Else
            For a As Integer = 0 To Lv_Data.Items.Count - 1
                Lv_Data.Items(a).Checked = False
            Next
        End If
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            OpenConn()

            Transaksi_Penawaran.arrKdBarangPilihBarang.Clear()
            Transaksi_Penawaran.arrNamaBarangPilihBarang.Clear()
            Transaksi_Penawaran.arrSatuanPilihBarang.Clear()
            Transaksi_Penawaran.arrPPN.Clear()

            For i As Integer = 0 To arrSelectedBarang.Count - 1
                'For i As Integer = 0 To Lv_Data.Items.Count - 1

                Dim itemData As Object() = CType(arrSelectedBarang(i), Object())

                Transaksi_Penawaran.arrKdBarangPilihBarang.Add(itemData(0))
                Transaksi_Penawaran.arrNamaBarangPilihBarang.Add(itemData(1))
                Transaksi_Penawaran.arrSatuanPilihBarang.Add(itemData(2))
                Transaksi_Penawaran.arrPPN.Add(itemData(3))

            Next

            Transaksi_Penawaran.LoadDataPenawaran()

            Me.Close()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Nama_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Nama.KeyDown
        If e.KeyCode = Keys.Enter Then
            Btn_Cari_Click_1(Txt_Nama, e)
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            OpenConn()

            ComboBox2.Items.Clear()
            SQL = "select kode_kategori_kecil from Kategori_Kecil where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_kategori_besar = '" & ComboBox1.Text & "' order by Kode_Kategori_Kecil "
            Using dr = OpenTrans(SQL)
                ComboBox2.Items.Add("---SELURUH---")
                Do While dr.Read
                    ComboBox2.Items.Add(dr("kode_kategori_kecil"))
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
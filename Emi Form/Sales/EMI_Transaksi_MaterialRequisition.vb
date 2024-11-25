Imports System.Security.Policy
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
'Imports Microsoft.SqlServer.Server

Public Class EMI_Transaksi_MaterialRequisition
    Public arrBulan, arrBulanMM As New ArrayList
    Dim Jenis = "Master_Jenis_Hewan"
    Public fRef As String

    Dim arrCellInputPPIC As New ArrayList

    Dim Lv0 As String
    Dim LVKd_Barang As String
    Dim LvNm_Barang As String
    Dim LvAvg_3Bln As String
    Dim LvStock_BB As String
    Dim LvOPRequesition As String
    Dim LvOPOrder As String
    Dim LvTotal As String
    Dim LvNBom_1 As String
    Dim LvNPPIC_1 As String
    Dim LvUrut_1 As String
    Dim LvKosong_1 As String
    Dim LvNBom_2 As String
    Dim LvNPPIC_2 As String
    Dim LvUrut_2 As String
    Dim LvKosong_2 As String
    Dim LvNBom_3 As String
    Dim LvNPPIC_3 As String
    Dim LvUrut_3 As String
    Dim LvKosong_3 As String
    Dim LvNBom_4 As String
    Dim LvNPPIC_4 As String
    Dim LvUrut_4 As String
    Dim LvKosong_4 As String
    Dim LvNBom_5 As String
    Dim LvNPPIC_5 As String
    Dim LvUrut_5 As String
    Dim LvKosong_5 As String
    Dim LvNBom_6 As String
    Dim LvNPPIC_6 As String
    Dim LvUrut_6 As String
    Dim LvKosong_6 As String
    'Dim LvReferensi As String
    Dim LvStatus As String
    Dim LvSatuanBarang As String

    Dim Cell0 As Integer = 0
    Dim CellKd_Barang As Integer = 1
    Dim CellNm_Barang As Integer = 2
    Dim CellAvg_3Bln As Integer = 3
    Dim CellStock_BB As Integer = 4
    Dim CellOPRequesition As Integer = 5
    Dim CellOPOrder As Integer = 6
    Dim CellTotal As Integer = 7
    Dim CellNBom_1 As Integer = 8
    Dim CellNPPIC_1 As Integer = 9
    Dim CellUrut_1 As Integer = 10
    Dim CellKosong_1 As Integer = 11
    Dim CellNBom_2 As Integer = 12
    Dim CellNPPIC_2 As Integer = 13
    Dim CellUrut_2 As Integer = 14
    Dim CellKosong_2 As Integer = 15
    Dim CellNBom_3 As Integer = 16
    Dim CellNPPIC_3 As Integer = 17
    Dim CellUrut_3 As Integer = 18
    Dim CellKosong_3 As Integer = 19
    Dim CellNBom_4 As Integer = 20
    Dim CellNPPIC_4 As Integer = 21
    Dim CellUrut_4 As Integer = 22
    Dim CellKosong_4 As Integer = 23
    Dim CellNBom_5 As Integer = 24
    Dim CellNPPIC_5 As Integer = 25
    Dim CellUrut_5 As Integer = 26
    Dim CellKosong_5 As Integer = 27
    Dim CellNBom_6 As Integer = 28
    Dim CellNPPIC_6 As Integer = 29
    Dim CellUrut_6 As Integer = 30
    Dim CellKosong_6 As Integer = 31
    'Dim CellReferensi As Integer = 33
    Dim CellStatus As Integer = 32
    Dim CellSatuanBarang As Integer = 33


    Public Property fstatus As String = ""



    Public Arrbarang As New ArrayList
    Public Arrlokasi As New ArrayList
    Public ArrNama As New ArrayList
    ' Public ArrJenis As New ArrayList

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        'Lv0 = DataGridView1.Rows(No_Index).Cells(Cell0).Value.ToString
        LVKd_Barang = DataGridView1.Rows(No_Index).Cells(CellKd_Barang).Value.ToString
        LvNm_Barang = DataGridView1.Rows(No_Index).Cells(CellNm_Barang).Value.ToString
        LvAvg_3Bln = DataGridView1.Rows(No_Index).Cells(CellAvg_3Bln).Value.ToString
        LvStock_BB = DataGridView1.Rows(No_Index).Cells(CellStock_BB).Value.ToString
        LvOPRequesition = DataGridView1.Rows(No_Index).Cells(CellOPRequesition).Value.ToString
        LvOPOrder = DataGridView1.Rows(No_Index).Cells(CellOPOrder).Value.ToString
        LvTotal = DataGridView1.Rows(No_Index).Cells(CellTotal).Value.ToString
        LvNBom_1 = DataGridView1.Rows(No_Index).Cells(CellNBom_1).Value.ToString
        LvNPPIC_1 = DataGridView1.Rows(No_Index).Cells(CellNPPIC_1).Value.ToString
        LvUrut_1 = DataGridView1.Rows(No_Index).Cells(CellUrut_1).Value.ToString
        LvKosong_1 = DataGridView1.Rows(No_Index).Cells(CellKosong_1).Value.ToString
        LvNBom_2 = DataGridView1.Rows(No_Index).Cells(CellNBom_2).Value.ToString
        LvNPPIC_2 = DataGridView1.Rows(No_Index).Cells(CellNPPIC_2).Value.ToString
        LvUrut_2 = DataGridView1.Rows(No_Index).Cells(CellUrut_2).Value.ToString
        LvKosong_2 = DataGridView1.Rows(No_Index).Cells(CellKosong_2).Value.ToString
        LvNBom_3 = DataGridView1.Rows(No_Index).Cells(CellNBom_3).Value.ToString
        LvNPPIC_3 = DataGridView1.Rows(No_Index).Cells(CellNPPIC_3).Value.ToString
        LvUrut_3 = DataGridView1.Rows(No_Index).Cells(CellUrut_3).Value.ToString
        LvKosong_3 = DataGridView1.Rows(No_Index).Cells(CellKosong_3).Value.ToString
        LvNBom_4 = DataGridView1.Rows(No_Index).Cells(CellNBom_4).Value.ToString
        LvNPPIC_4 = DataGridView1.Rows(No_Index).Cells(CellNPPIC_4).Value.ToString
        LvUrut_4 = DataGridView1.Rows(No_Index).Cells(CellUrut_4).Value.ToString
        LvKosong_4 = DataGridView1.Rows(No_Index).Cells(CellKosong_4).Value.ToString
        LvNBom_5 = DataGridView1.Rows(No_Index).Cells(CellNBom_5).Value.ToString
        LvNPPIC_5 = DataGridView1.Rows(No_Index).Cells(CellNPPIC_5).Value.ToString
        LvUrut_5 = DataGridView1.Rows(No_Index).Cells(CellUrut_5).Value.ToString
        LvKosong_5 = DataGridView1.Rows(No_Index).Cells(CellKosong_5).Value.ToString
        LvNBom_6 = DataGridView1.Rows(No_Index).Cells(CellNBom_6).Value.ToString
        LvNPPIC_6 = DataGridView1.Rows(No_Index).Cells(CellNPPIC_6).Value.ToString
        LvUrut_6 = DataGridView1.Rows(No_Index).Cells(CellUrut_6).Value.ToString
        LvKosong_6 = DataGridView1.Rows(No_Index).Cells(CellKosong_6).Value.ToString
        'LvReferensi = DataGridView1.Rows(No_Index).Cells(CellReferensi).Value.ToString
        LvStatus = DataGridView1.Rows(No_Index).Cells(CellStatus).Value.ToString
        LvSatuanBarang = DataGridView1.Rows(No_Index).Cells(CellSatuanBarang).Value.ToString
    End Sub

    Private Sub getdata()

        DataGridView1.Rows.Clear()
        TextBox2.Clear()


        DataGridView1.Columns(Cell0).HeaderText = "#"
        DataGridView1.Columns(CellKd_Barang).HeaderText = "Kode Barang"
        DataGridView1.Columns(CellNm_Barang).HeaderText = "Nama Barang"
        DataGridView1.Columns(CellAvg_3Bln).HeaderText = "Avg 3 Bulan (Pcs)"
        DataGridView1.Columns(CellStock_BB).HeaderText = "Stok Bahan Baku"
        DataGridView1.Columns(CellOPRequesition).HeaderText = "Open Purchase Requsition"
        DataGridView1.Columns(CellOPOrder).HeaderText = "Open Purchase Order"
        DataGridView1.Columns(CellTotal).HeaderText = "Total Stock + Open PR + Open PO"

        Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
        Dim fthn As Integer = Val(ComboBox2.Text)
        Dim panggil_databulan As String = ""
        Dim panggil_datatahun As String = ""
        If a = 12 Then
            a = 1
            fthn = fthn + 1
        Else
            a = a + 1
        End If

        Dim b As String = ""
        For index = 0 To arrBulan.Count - 1
            If arrBulan.Item(index) = a Then
                'ComboBox1.SelectedIndex = index
                b = ComboBox1.Items(index)
                panggil_databulan = arrBulanMM.Item(index)
            End If
        Next
        panggil_datatahun = fthn
        DataGridView1.Columns(CellNBom_1).HeaderText = "BoM - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellNPPIC_1).HeaderText = "PPIC - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellUrut_1).HeaderText = "1"
        DataGridView1.Columns(CellKosong_1).HeaderText = ""

        If a = 12 Then
            a = 1
            fthn = fthn + 1
        Else
            a = a + 1
        End If

        For index = 0 To arrBulan.Count - 1
            If arrBulan.Item(index) = a Then
                'ComboBox1.SelectedIndex = index
                b = ComboBox1.Items(index)
            End If
        Next
        DataGridView1.Columns(CellNBom_2).HeaderText = "BoM - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellNPPIC_2).HeaderText = "PPIC - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellUrut_2).HeaderText = "2"
        DataGridView1.Columns(CellKosong_2).HeaderText = ""
        If a = 12 Then
            a = 1
            fthn = fthn + 1
        Else
            a = a + 1
        End If

        For index = 0 To arrBulan.Count - 1
            If arrBulan.Item(index) = a Then
                'ComboBox1.SelectedIndex = index
                b = ComboBox1.Items(index)
            End If
        Next
        DataGridView1.Columns(CellNBom_3).HeaderText = "BoM - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellNPPIC_3).HeaderText = "PPIC - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellUrut_3).HeaderText = "3"
        DataGridView1.Columns(CellKosong_3).HeaderText = ""
        If a = 12 Then
            a = 1
            fthn = fthn + 1
        Else
            a = a + 1
        End If

        For index = 0 To arrBulan.Count - 1
            If arrBulan.Item(index) = a Then
                'ComboBox1.SelectedIndex = index
                b = ComboBox1.Items(index)
            End If
        Next
        DataGridView1.Columns(CellNBom_4).HeaderText = "BoM - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellNPPIC_4).HeaderText = "PPIC - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellUrut_4).HeaderText = "4"
        DataGridView1.Columns(CellKosong_4).HeaderText = ""
        If a = 12 Then
            a = 1
            fthn = fthn + 1
        Else
            a = a + 1
        End If

        For index = 0 To arrBulan.Count - 1
            If arrBulan.Item(index) = a Then
                'ComboBox1.SelectedIndex = index
                b = ComboBox1.Items(index)
            End If
        Next
        DataGridView1.Columns(CellNBom_5).HeaderText = "BoM - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellNPPIC_5).HeaderText = "PPIC - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellUrut_5).HeaderText = "5"
        DataGridView1.Columns(CellKosong_5).HeaderText = ""
        If a = 12 Then
            a = 1
            fthn = fthn + 1
        Else
            a = a + 1
        End If

        For index = 0 To arrBulan.Count - 1
            If arrBulan.Item(index) = a Then
                'ComboBox1.SelectedIndex = index
                b = ComboBox1.Items(index)
            End If
        Next
        DataGridView1.Columns(CellNBom_6).HeaderText = "BoM - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellNPPIC_6).HeaderText = "PPIC - Forecast " & b & " - " & fthn
        DataGridView1.Columns(CellUrut_6).HeaderText = "6"
        DataGridView1.Columns(CellKosong_6).HeaderText = ""

        DataGridView1.Columns(CellStatus).HeaderText = "Status"

        Dim fLoad As Boolean = False
        Try
            OpenConn()

            SQL = "select No_Faktur from EMI_Transaksi_Material_Requsition where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Lokasi = '" & ComboBox3.Text & "' and Bulan = '" & arrBulanMM.Item(ComboBox1.SelectedIndex) & "' and Tahun = '" & ComboBox2.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtBarangMasuk_NoFaktur.Text = Dr("No_Faktur")
                    fLoad = True
                    'Btn_Realese.Enabled = fa
                Else
                    fLoad = False
                    ' Btn_Realese.Enabled = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If fLoad = True Then
            TxtBarangMasuk_NoFaktur_Leave(ComboBox2, Nothing)
        Else

            Try
                OpenConn()

                get_no_faktur()

                Arrbarang.Clear()
                Arrlokasi.Clear()
                ArrNama.Clear()
                SQL = "select  e.Kode_Stock_Owner,e.Kode_Barang,c.Nama from EMI_Transaksi_Sales_Forecasting a,  "
                SQL = SQL & "EMI_Transaksi_Sales_Forecasting_Detail b,barang c, Emi_Transaksi_Formulator d, EMI_Transaksi_Formulator_Detail_Bahan e "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  "
                SQL = SQL & "and e.Kode_Perusahaan = c.Kode_Perusahaan and e.Kode_Stock_Owner = c.Kode_Stock_Owner and e.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Formula = d.No_Faktur "
                SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.No_Faktur = e.No_Faktur "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and a.lokasi = '" & ComboBox3.Text & "' "
                SQL = SQL & "and b.bulan = '" & panggil_databulan & "' and b.Tahun = '" & panggil_datatahun & "' "
                SQL = SQL & "and a.Flag_Validasi = 'Y' and Flag_Validasi_PPIC = 'Y'  "
                SQL = SQL & "group by e.Kode_Stock_Owner,e.Kode_Barang,c.Nama "

                SQL = SQL & "Union all "

                SQL = SQL & "Select d.Kode_Stock_Owner,c.Kode_Bahan As Kode_Barang,d.Nama from "
                SQL = SQL & "EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_Detail b, barang_detail_bahan_penolong c, barang d, barang e "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "And b.Kode_Perusahaan=e.Kode_Perusahaan And b.Kode_barang=e.Kode_Barang And b.kode_stock_owner=e.kode_stock_owner "
                SQL = SQL & "And e.Kode_Barang_inq = c.Kode_Barang And c.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & " And c.Kode_Bahan = d.Kode_Barang And b.Kode_Stock_Owner = d.Kode_Stock_Owner And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "And a.lokasi = '" & ComboBox3.Text & "' and b.bulan = '" & panggil_databulan & "' and b.Tahun = '" & panggil_datatahun & "' "
                SQL = SQL & "and a.Flag_Validasi = 'Y' and Flag_Validasi_PPIC = 'Y'  "
                SQL = SQL & "group by d.Kode_Stock_Owner, c.Kode_Bahan, d.Nama "

                SQL = SQL & "order by nama "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows().Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                Arrbarang.Add(.Rows(i).Item("kode_barang"))
                                Arrlokasi.Add(.Rows(i).Item("kode_stock_owner"))
                                ArrNama.Add(.Rows(i).Item("nama"))
                            Next
                        Else
                            CloseConn()
                            MessageBox.Show("Tidak ada data forecasting pada bulan " & ComboBox1.Text & " " & ComboBox2.Text)
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


            get_barang()

        End If
    End Sub

    Public Sub get_barang()

        Dim akses_ubah As String = ""

        Try
            OpenConn()

            If CekButtonRole("MRP_PPIC") = "Y" Then
                akses_ubah = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()
            For indexxx = 0 To Arrbarang.Count - 1
                DataGridView1.Rows.Add(1)


                Dim ind As Integer = DataGridView1.Rows.Count - 1

                DataGridView1.Rows(ind).Cells(CellNBom_1).Style.BackColor = Color.LightYellow
                DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.LightCyan
                DataGridView1.Rows(ind).Cells(CellKosong_1).Style.BackColor = Color.LightGray
                DataGridView1.Rows(ind).Cells(CellNBom_2).Style.BackColor = Color.LightYellow
                DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.LightCyan
                DataGridView1.Rows(ind).Cells(CellKosong_2).Style.BackColor = Color.LightGray
                DataGridView1.Rows(ind).Cells(CellNBom_3).Style.BackColor = Color.LightYellow
                DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.LightCyan
                DataGridView1.Rows(ind).Cells(CellKosong_3).Style.BackColor = Color.LightGray
                DataGridView1.Rows(ind).Cells(CellNBom_4).Style.BackColor = Color.LightYellow
                DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.LightCyan
                DataGridView1.Rows(ind).Cells(CellKosong_4).Style.BackColor = Color.LightGray
                DataGridView1.Rows(ind).Cells(CellNBom_5).Style.BackColor = Color.LightYellow
                DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.LightCyan
                DataGridView1.Rows(ind).Cells(CellKosong_5).Style.BackColor = Color.LightGray
                DataGridView1.Rows(ind).Cells(CellNBom_6).Style.BackColor = Color.LightYellow
                DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.LightCyan
                DataGridView1.Rows(ind).Cells(CellKosong_6).Style.BackColor = Color.LightGray
                DataGridView1.Rows(ind).Cells(CellStatus).Style.BackColor = Color.Yellow

                DataGridView1.Rows(ind).Cells(CellNBom_1).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNBom_2).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNBom_3).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNBom_4).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNBom_5).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNBom_6).ReadOnly = True

                DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = True
                DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = True

                Dim satuan_barang As String = ""
                Dim good_stock As Double = 0
                Dim Flag_Packaging As String = ""
                Dim Flag_Raw_Material As String = ""

                Dim ada_data As String = ""
                Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
                Dim fthn As Integer = Val(ComboBox2.Text)
                Dim b As String = ""
                Dim FValidasi As String = ""
                For index = 0 To arrBulan.Count - 1
                    If arrBulan.Item(index) = a Then
                        'ComboBox1.SelectedIndex = index
                        b = arrBulanMM.Item(index)
                    End If
                Next

                SQL = "select a.satuan, a.good_stock, b.Flag_raw_material, b.flag_packaging from barang a, emi_group_jenis b "
                SQL = SQL & "where a.kode_Barang='" & Arrbarang.Item(indexxx) & "' and kode_stock_owner='" & Arrlokasi.Item(indexxx) & "' "
                SQL = SQL & "And a.kode_Perusahaan ='" & KodePerusahaan & "' and a.id_group_jenis=b.id_group_jenis and a.Kode_Perusahaan=b.kode_perusahaan "
                Using dr5 = OpenTrans(SQL)
                    If dr5.Read Then
                        satuan_barang = dr5("satuan")
                        good_stock = dr5("good_stock")
                        Flag_Packaging = dr5("flag_packaging")
                        Flag_Raw_Material = dr5("Flag_raw_material")
                    Else
                        dr5.Close()
                        CloseConn()
                        MessageBox.Show("data tidak ada")
                        Exit Sub
                    End If
                End Using

                'sedang di edit
                Dim convertKesatuanDisplay As String = ""
                Dim good_stock_tampil_display As Double = 0

                SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & Arrbarang.Item(indexxx) & "' "
                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                Using Dr3 = OpenTrans(SQL)
                    If Dr3.Read Then
                        convertKesatuanDisplay = Dr3("satuan")
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Arrbarang.Item(indexxx) & "',"
                        SQL = SQL & "'" & satuan_barang & "','" & Dr3("satuan") & "',"
                        SQL = SQL & "" & good_stock & ") as Hasil "
                        Dr3.Close()

                        Using dr4 = OpenTrans(SQL)
                            If dr4.Read Then
                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                    If dr4("Hasil") = 0 Then
                                        good_stock_tampil_display = 0
                                    Else
                                        good_stock_tampil_display = dr4("hasil")

                                    End If
                                Else
                                    dr4.Close()
                                    CloseConn()
                                    MessageBox.Show("Satuan " & satuan_barang & " Ke " & convertKesatuanDisplay & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    kosong()
                                    Exit Sub
                                End If
                            End If
                        End Using
                    Else
                        Dr3.Close()
                        CloseConn()
                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        kosong()
                        Exit Sub
                    End If
                End Using




                DataGridView1.Rows(ind).Cells(CellKd_Barang).Value = Arrbarang.Item(indexxx)
                DataGridView1.Rows(ind).Cells(CellNm_Barang).Value = ArrNama.Item(indexxx)
                DataGridView1.Rows(ind).Cells(CellAvg_3Bln).Value = "0"
                DataGridView1.Rows(ind).Cells(CellStock_BB).Value = Format(good_stock_tampil_display, "N2")
                DataGridView1.Rows(ind).Cells(CellSatuanBarang).Value = convertKesatuanDisplay


                '=============================== tampil data PR belum PO ==============================='

                Dim totalPrBelumPOPerbarang As Double = 0
                Dim convertSatuanDisplayPr As String = ""
                '---------select ke data pr berdasarkan kode_barang
                SQL = "select b.Kode_Stock_Owner, b.Kode_Barang, b.Satuan, "

                SQL = SQL & "b.jumlah-isnull((Select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur "
                SQL = SQL & "And y.Kode_Perusahaan = b.Kode_Perusahaan And y.no_urut_pr = b.No_Urut And x.status Is null), "
                SQL = SQL & "0) As jumlah "

                SQL = SQL & "from EMI_Purchase_Requisition a, EMI_Purchase_Requisition_Detail b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null  "
                SQL = SQL & "and b.kode_stock_owner = '" & Arrlokasi.Item(indexxx) & "' "
                SQL = SQL & "and b.kode_barang = '" & Arrbarang.Item(indexxx) & "' "
                'SQL = SQL & "and MONTH(b.tanggal_delivery) = '" & b & "' "
                'SQL = SQL & "and YEAR(b.tanggal_delivery) = '" & fthn & "' "
                Using Ds2 = BindingTrans(SQL)
                    With Ds2.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For indexPR As Integer = 0 To .Rows.Count - 1

                                'ambil satuan barang yang akan ditampilkan ke display
                                SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexPR).Item("kode_barang") & "' "
                                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                OpenConn()

                                Using Dr3 = OpenTrans(SQL)
                                    If Dr3.Read Then
                                        convertSatuanDisplayPr = Dr3("satuan")
                                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexPR).Item("kode_barang") & "',"
                                        SQL = SQL & "'" & .Rows(indexPR).Item("satuan") & "','" & Dr3("satuan") & "',"
                                        SQL = SQL & "" & .Rows(indexPR).Item("jumlah") & ") as Hasil "
                                        Dr3.Close()

                                        Using dr4 = OpenTrans(SQL)
                                            If dr4.Read Then
                                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                                    'If dr4("Hasil") = 0 Then
                                                    '    MessageBox.Show("Satuan " & .Rows(indexPR).Item("satuan") & " Ke " & convertSatuanDisplayPr & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    '    kosong()
                                                    '    Exit Sub
                                                    'Else
                                                    totalPrBelumPOPerbarang = totalPrBelumPOPerbarang + dr4("hasil")
                                                    DataGridView1.Rows(ind).Cells(CellOPRequesition).Value = Format(totalPrBelumPOPerbarang, "N2")
                                                    'End If
                                                Else
                                                    dr4.Close()
                                                    CloseConn()
                                                    MessageBox.Show("Satuan " & .Rows(indexPR).Item("satuan") & " Ke " & convertSatuanDisplayPr & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    kosong()
                                                    Exit Sub
                                                End If
                                            End If
                                        End Using
                                    Else
                                        Dr3.Close()
                                        CloseConn()
                                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        kosong()
                                        Exit Sub
                                    End If
                                End Using


                            Next
                        Else
                            'jika belum  ada pr maka 0
                            DataGridView1.Rows(ind).Cells(CellOPRequesition).Value = 0
                        End If
                    End With
                End Using



                '=========================================== tampil PR sudah po =========================================='
                Dim totalPRSudahPO As Double = 0
                Dim convertSatuanPRSudahPO As String = ""
                '---------select ke data pr berdasarkan kode_barang
                SQL = "select b.Kode_Stock_Owner,b.Kode_Barang,b.Satuan,b.jumlah "
                SQL = SQL & "from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b "
                SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null  "
                SQL = SQL & "and b.kode_stock_owner = '" & Arrlokasi.Item(indexxx) & "' "
                SQL = SQL & "and b.kode_barang = '" & Arrbarang.Item(indexxx) & "' "
                'SQL = SQL & "and MONTH(a.etd_simulasi) = '" & b & "' "
                'SQL = SQL & "and YEAR(a.etd_simulasi) = '" & fthn & "' "
                Using Ds2 = BindingTrans(SQL)
                    With Ds2.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For indexPR As Integer = 0 To .Rows.Count - 1

                                'ambil satuan barang yang akan ditampilkan ke display
                                SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexPR).Item("kode_barang") & "' "
                                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                OpenConn()

                                Using Dr3 = OpenTrans(SQL)
                                    If Dr3.Read Then
                                        convertSatuanPRSudahPO = Dr3("satuan")
                                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexPR).Item("kode_barang") & "',"
                                        SQL = SQL & "'" & .Rows(indexPR).Item("satuan") & "','" & Dr3("satuan") & "',"
                                        SQL = SQL & "" & .Rows(indexPR).Item("jumlah") & ") as Hasil "
                                        Dr3.Close()

                                        Using dr4 = OpenTrans(SQL)
                                            If dr4.Read Then
                                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                                    'If dr4("Hasil") = 0 Then
                                                    '    MessageBox.Show("Satuan " & .Rows(indexPR).Item("satuan") & " Ke " & convertSatuanDisplayPr & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    '    kosong()
                                                    '    Exit Sub
                                                    'Else
                                                    totalPRSudahPO = totalPRSudahPO + dr4("hasil")
                                                    DataGridView1.Rows(ind).Cells(CellOPOrder).Value = Format(totalPRSudahPO, "N2")
                                                    'End If
                                                Else
                                                    dr4.Close()
                                                    CloseConn()
                                                    MessageBox.Show("Satuan " & .Rows(indexPR).Item("satuan") & " Ke " & convertSatuanDisplayPr & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    kosong()
                                                    Exit Sub
                                                End If
                                            End If
                                        End Using
                                    Else
                                        Dr3.Close()
                                        CloseConn()
                                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        kosong()
                                        Exit Sub
                                    End If
                                End Using


                            Next
                        Else
                            'jika belum  ada pr maka 0
                            DataGridView1.Rows(ind).Cells(CellOPOrder).Value = 0
                        End If
                    End With
                End Using


                '  DataGridView1.Rows(ind).Cells(CellOPOrder).Value = "0"
                DataGridView1.Rows(ind).Cells(CellTotal).Value = Format(totalPrBelumPOPerbarang + totalPRSudahPO + good_stock_tampil_display, "N2")

                'Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
                'Dim fthn As Integer = Val(ComboBox2.Text)
                'Dim b As String = ""
                If a = 12 Then
                    a = 1
                    fthn = fthn + 1
                Else
                    a = a + 1
                End If

                For index = 0 To arrBulan.Count - 1
                    If arrBulan.Item(index) = a Then
                        'ComboBox1.SelectedIndex = index
                        b = arrBulanMM.Item(index)
                    End If
                Next

                'BULAN KE 1

                If fstatus = "MRP_PPIC" Then

                    If akses_ubah = "Y" Then
                        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                FValidasi = "Y"
                                DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.DarkCyan
                            Else

                                dr.Close()
                                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                                'Using dr2 = OpenTrans(SQL)
                                '    If dr2.Read Then
                                '        FValidasi = ""
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = False
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.LightCyan
                                '    Else
                                FValidasi = ""
                                DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.LightCyan
                                ' End If
                                'End Using
                            End If
                        End Using
                    Else
                        FValidasi = ""
                        DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = True
                        DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.DarkCyan
                    End If

                ElseIf fstatus = "MRP_Formulator" Then
                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            FValidasi = "Y"
                            ' DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = False
                            DataGridView1.Rows(ind).Cells(CellNBom_1).Style.BackColor = Color.DarkGoldenrod
                        Else
                            FValidasi = ""
                            'DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = False
                            DataGridView1.Rows(ind).Cells(CellNBom_1).Style.BackColor = Color.LightYellow
                        End If
                    End Using
                End If

                ada_data = ""
                SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
                SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
                Using Ds2 = BindingTrans(SQL)
                    With Ds2.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            DataGridView1.Rows(ind).Cells(CellNBom_1).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
                            DataGridView1.Rows(ind).Cells(CellNPPIC_1).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
                            DataGridView1.Rows(ind).Cells(CellUrut_1).Value = .Rows(0).Item("Urut")
                            DataGridView1.Rows(ind).Cells(CellKosong_1).Value = ""
                            ada_data = "T"
                        Else
                            DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0
                            DataGridView1.Rows(ind).Cells(CellNPPIC_1).Value = 0
                            DataGridView1.Rows(ind).Cells(CellUrut_1).Value = ""
                            DataGridView1.Rows(ind).Cells(CellKosong_1).Value = ""
                            ada_data = "T"
                        End If
                    End With
                End Using

                'BULAN KE 1 ngitung bahan yang dibutuhkan
                'ambil barang untuk bulan dan tahun 

                If ada_data = "T" Then

                    If Flag_Raw_Material = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
                        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
                        SQL = SQL & "from "
                        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
                        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
                        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
                        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
                        SQL = SQL & "and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y'  "
                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
                        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
                        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
                        SQL = SQL & ") "
                        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
                        SQL = SQL & "group by Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_1).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0

                            End If

                        End Using

                    ElseIf Flag_Packaging = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
                        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
                        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
                        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
                        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y' and "
                        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
                        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
                        SQL = SQL & ") "
                        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
                        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_1).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0

                            End If

                        End Using
                    End If

                End If

                '============ akhir bulan 1

                If a = 12 Then
                    a = 1
                    fthn = fthn + 1
                Else
                    a = a + 1
                End If

                For index = 0 To arrBulan.Count - 1
                    If arrBulan.Item(index) = a Then
                        'ComboBox1.SelectedIndex = index
                        b = arrBulanMM.Item(index)
                    End If
                Next




                'BULAN KE 2

                If fstatus = "MRP_PPIC" Then

                    If akses_ubah = "Y" Then
                        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                FValidasi = "Y"
                                DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.DarkCyan
                            Else

                                dr.Close()
                                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                                'Using dr2 = OpenTrans(SQL)
                                '    If dr2.Read Then
                                '        FValidasi = ""
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = False
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.LightCyan
                                '    Else
                                FValidasi = ""
                                DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.LightCyan
                                '    End If
                                'End Using
                            End If
                        End Using
                    Else
                        FValidasi = ""
                        DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = True
                        DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.DarkCyan
                    End If

                ElseIf fstatus = "MRP_Formulator" Then
                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            FValidasi = "Y"

                            DataGridView1.Rows(ind).Cells(CellNBom_2).Style.BackColor = Color.DarkGoldenrod
                        Else
                            FValidasi = ""

                            DataGridView1.Rows(ind).Cells(CellNBom_2).Style.BackColor = Color.LightYellow
                        End If
                    End Using
                End If

                ada_data = ""
                SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
                SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
                Using Ds2 = BindingTrans(SQL)
                    With Ds2.Tables("MyTable")

                        If .Rows.Count <> 0 Then
                            DataGridView1.Rows(ind).Cells(CellNBom_2).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
                            DataGridView1.Rows(ind).Cells(CellNPPIC_2).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
                            DataGridView1.Rows(ind).Cells(CellUrut_2).Value = .Rows(0).Item("Urut")
                            DataGridView1.Rows(ind).Cells(CellKosong_2).Value = ""
                            ada_data = "T"
                        Else
                            '  DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0
                            DataGridView1.Rows(ind).Cells(CellNPPIC_2).Value = 0
                            DataGridView1.Rows(ind).Cells(CellUrut_2).Value = ""
                            DataGridView1.Rows(ind).Cells(CellKosong_2).Value = ""
                            ada_data = "T"
                        End If
                    End With
                End Using


                If ada_data = "T" Then

                    If Flag_Raw_Material = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
                        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
                        SQL = SQL & "from "
                        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
                        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
                        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
                        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
                        SQL = SQL & "and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y'  "
                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
                        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
                        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
                        SQL = SQL & ") "
                        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
                        SQL = SQL & "group by Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_2).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0

                            End If

                        End Using

                    ElseIf Flag_Packaging = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
                        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
                        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
                        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
                        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y' and "
                        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
                        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
                        SQL = SQL & ") "
                        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
                        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_2).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0

                            End If

                        End Using
                    End If

                End If

                '============ akhir bulan 2

                If a = 12 Then
                    a = 1
                    fthn = fthn + 1
                Else
                    a = a + 1
                End If

                For index = 0 To arrBulan.Count - 1
                    If arrBulan.Item(index) = a Then
                        'ComboBox1.SelectedIndex = index
                        b = arrBulanMM.Item(index)
                    End If
                Next

                'BULAN KE 3

                If fstatus = "MRP_PPIC" Then

                    If akses_ubah = "Y" Then
                        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                FValidasi = "Y"
                                DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.DarkCyan
                            Else

                                dr.Close()
                                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                                'Using dr2 = OpenTrans(SQL)
                                '    If dr2.Read Then
                                '        FValidasi = ""
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = False
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.LightCyan
                                '    Else
                                FValidasi = ""
                                DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.LightCyan
                                '    End If
                                'End Using
                            End If
                        End Using
                    Else
                        FValidasi = ""
                        DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = True
                        DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.DarkCyan
                    End If

                ElseIf fstatus = "MRP_Formulator" Then
                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            FValidasi = "Y"

                            DataGridView1.Rows(ind).Cells(CellNBom_3).Style.BackColor = Color.DarkGoldenrod
                        Else
                            FValidasi = ""

                            DataGridView1.Rows(ind).Cells(CellNBom_3).Style.BackColor = Color.LightYellow
                        End If
                    End Using
                End If
                ada_data = ""
                SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
                SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
                Using Ds2 = BindingTrans(SQL)
                    With Ds2.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
                            DataGridView1.Rows(ind).Cells(CellNPPIC_3).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
                            DataGridView1.Rows(ind).Cells(CellUrut_3).Value = .Rows(0).Item("Urut")
                            DataGridView1.Rows(ind).Cells(CellKosong_3).Value = ""
                            ada_data = "T"
                        Else
                            '  DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0
                            DataGridView1.Rows(ind).Cells(CellNPPIC_3).Value = 0
                            DataGridView1.Rows(ind).Cells(CellUrut_3).Value = ""
                            DataGridView1.Rows(ind).Cells(CellKosong_3).Value = ""
                            ada_data = "T"
                        End If
                    End With
                End Using

                If ada_data = "T" Then

                    If Flag_Raw_Material = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
                        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
                        SQL = SQL & "from "
                        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
                        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
                        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
                        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
                        SQL = SQL & "and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y'  "
                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
                        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
                        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
                        SQL = SQL & ") "
                        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
                        SQL = SQL & "group by Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0

                            End If

                        End Using

                    ElseIf Flag_Packaging = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
                        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
                        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
                        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
                        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y' and "
                        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
                        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
                        SQL = SQL & ") "
                        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
                        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0

                            End If

                        End Using
                    End If

                End If

                '============ akhir bulan 3

                If a = 12 Then
                    a = 1
                    fthn = fthn + 1
                Else
                    a = a + 1
                End If

                For index = 0 To arrBulan.Count - 1
                    If arrBulan.Item(index) = a Then
                        'ComboBox1.SelectedIndex = index
                        b = arrBulanMM.Item(index)
                    End If
                Next
                'BULAN KE 4

                If fstatus = "MRP_PPIC" Then

                    If akses_ubah = "Y" Then
                        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                FValidasi = "Y"
                                DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.DarkCyan
                            Else

                                dr.Close()
                                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                                'Using dr2 = OpenTrans(SQL)
                                '    If dr2.Read Then
                                '        FValidasi = ""
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = False
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.LightCyan
                                '    Else
                                FValidasi = ""
                                DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.LightCyan
                                '    End If
                                'End Using
                            End If
                        End Using
                    Else
                        FValidasi = ""
                        DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = True
                        DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.DarkCyan
                    End If

                ElseIf fstatus = "MRP_Formulator" Then
                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            FValidasi = "Y"

                            DataGridView1.Rows(ind).Cells(CellNBom_4).Style.BackColor = Color.DarkGoldenrod
                        Else
                            FValidasi = ""

                            DataGridView1.Rows(ind).Cells(CellNBom_4).Style.BackColor = Color.LightYellow
                        End If
                    End Using
                End If

                ada_data = ""
                SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
                SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
                Using Ds2 = BindingTrans(SQL)
                    With Ds2.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
                            DataGridView1.Rows(ind).Cells(CellNPPIC_4).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
                            DataGridView1.Rows(ind).Cells(CellUrut_4).Value = .Rows(0).Item("Urut")
                            DataGridView1.Rows(ind).Cells(CellKosong_4).Value = ""
                            ada_data = "T"
                        Else
                            ' DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0
                            DataGridView1.Rows(ind).Cells(CellNPPIC_4).Value = 0
                            DataGridView1.Rows(ind).Cells(CellUrut_4).Value = ""
                            DataGridView1.Rows(ind).Cells(CellKosong_4).Value = ""
                            ada_data = "T"
                        End If
                    End With
                End Using

                If ada_data = "T" Then

                    If Flag_Raw_Material = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
                        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
                        SQL = SQL & "from "
                        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
                        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
                        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
                        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
                        SQL = SQL & "and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y'  "
                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
                        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
                        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
                        SQL = SQL & ") "
                        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
                        SQL = SQL & "group by Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0

                            End If

                        End Using

                    ElseIf Flag_Packaging = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
                        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
                        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
                        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
                        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y' and "
                        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
                        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
                        SQL = SQL & ") "
                        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
                        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0

                            End If

                        End Using
                    End If

                End If

                '============ akhir bulan 4

                If a = 12 Then
                    a = 1
                    fthn = fthn + 1
                Else
                    a = a + 1
                End If

                For index = 0 To arrBulan.Count - 1
                    If arrBulan.Item(index) = a Then
                        'ComboBox1.SelectedIndex = index
                        b = arrBulanMM.Item(index)
                    End If
                Next
                'BULAN  5

                If fstatus = "MRP_PPIC" Then

                    If akses_ubah = "Y" Then
                        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                FValidasi = "Y"
                                DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.DarkCyan
                            Else

                                dr.Close()
                                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                                'Using dr2 = OpenTrans(SQL)
                                '    If dr2.Read Then
                                '        FValidasi = ""
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = False
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.LightCyan
                                '    Else
                                FValidasi = ""
                                DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.LightCyan
                                '    End If
                                'End Using
                            End If
                        End Using
                    Else
                        FValidasi = ""
                        DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = True
                        DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.DarkCyan
                    End If

                ElseIf fstatus = "MRP_Formulator" Then
                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            FValidasi = "Y"

                            DataGridView1.Rows(ind).Cells(CellNBom_5).Style.BackColor = Color.DarkGoldenrod
                        Else
                            FValidasi = ""

                            DataGridView1.Rows(ind).Cells(CellNBom_5).Style.BackColor = Color.LightYellow
                        End If
                    End Using
                End If
                ada_data = ""
                SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
                SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
                Using Ds2 = BindingTrans(SQL)
                    With Ds2.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
                            DataGridView1.Rows(ind).Cells(CellNPPIC_5).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
                            DataGridView1.Rows(ind).Cells(CellUrut_5).Value = .Rows(0).Item("Urut")
                            DataGridView1.Rows(ind).Cells(CellKosong_5).Value = ""
                            ada_data = "T"
                        Else
                            '  DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0
                            DataGridView1.Rows(ind).Cells(CellNPPIC_5).Value = 0
                            DataGridView1.Rows(ind).Cells(CellUrut_5).Value = ""
                            DataGridView1.Rows(ind).Cells(CellKosong_5).Value = ""
                            ada_data = "T"
                        End If
                    End With
                End Using

                If ada_data = "T" Then

                    If Flag_Raw_Material = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
                        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
                        SQL = SQL & "from "
                        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
                        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
                        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
                        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
                        SQL = SQL & "and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y'  "
                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
                        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
                        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
                        SQL = SQL & ") "
                        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
                        SQL = SQL & "group by Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0

                            End If

                        End Using

                    ElseIf Flag_Packaging = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
                        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
                        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
                        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
                        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y' and "
                        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
                        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
                        SQL = SQL & ") "
                        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
                        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0

                            End If

                        End Using
                    End If

                End If

                '============ akhir bulan 5

                If a = 12 Then
                    a = 1
                    fthn = fthn + 1
                Else
                    a = a + 1
                End If

                For index = 0 To arrBulan.Count - 1
                    If arrBulan.Item(index) = a Then
                        'ComboBox1.SelectedIndex = index
                        b = arrBulanMM.Item(index)
                    End If
                Next
                'BULAN KE 6

                If fstatus = "MRP_PPIC" Then

                    If akses_ubah = "Y" Then
                        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                FValidasi = "Y"
                                DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.DarkCyan
                            Else

                                dr.Close()
                                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                                'Using dr2 = OpenTrans(SQL)
                                '    If dr2.Read Then
                                '        FValidasi = ""
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = False
                                '        DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.LightCyan
                                '    Else
                                FValidasi = ""
                                DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.LightCyan
                                '    End If
                                'End Using
                            End If
                        End Using
                    Else
                        FValidasi = ""
                        DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = True
                        DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.DarkCyan
                    End If

                ElseIf fstatus = "MRP_Formulator" Then
                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            FValidasi = "Y"

                            DataGridView1.Rows(ind).Cells(CellNBom_6).Style.BackColor = Color.DarkGoldenrod
                        Else
                            FValidasi = ""

                            DataGridView1.Rows(ind).Cells(CellNBom_6).Style.BackColor = Color.LightYellow
                        End If
                    End Using
                End If

                ada_data = ""
                SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
                SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
                Using Ds2 = BindingTrans(SQL)
                    With Ds2.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
                            DataGridView1.Rows(ind).Cells(CellNPPIC_6).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
                            DataGridView1.Rows(ind).Cells(CellUrut_6).Value = .Rows(0).Item("Urut")
                            DataGridView1.Rows(ind).Cells(CellKosong_6).Value = ""
                            ada_data = "T"
                        Else
                            '  DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0
                            DataGridView1.Rows(ind).Cells(CellNPPIC_6).Value = 0
                            DataGridView1.Rows(ind).Cells(CellUrut_6).Value = ""
                            DataGridView1.Rows(ind).Cells(CellKosong_6).Value = ""
                            ada_data = "T"
                        End If
                    End With
                End Using

                If ada_data = "T" Then

                    If Flag_Raw_Material = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
                        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
                        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
                        SQL = SQL & "from "
                        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
                        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
                        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
                        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
                        SQL = SQL & "and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y'  "
                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
                        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
                        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
                        SQL = SQL & ") "
                        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
                        SQL = SQL & "group by Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0

                            End If

                        End Using

                    ElseIf Flag_Packaging = "Y" Then
                        SQL = ";with cte as ( "
                        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
                        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
                        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
                        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
                        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and a.flag_validasi='Y' and a.flag_validasi_PPIC='Y' and "
                        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
                        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
                        SQL = SQL & ") "
                        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
                        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
                        Using ds3 = BindingTrans(SQL)

                            If ds3.Tables("MyTable").Rows.Count <> 0 Then
                                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

                                    Dim jumlah As Double = 0

                                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

                                    Dim convertKeSatuanAsli As String = ""
                                    Dim jumlahBarangDibutuhkan As Double = 0

                                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
                                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
                                    Using Dr3 = OpenTrans(SQL)
                                        If Dr3.Read Then
                                            convertKeSatuanAsli = Dr3("satuan")
                                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
                                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
                                            SQL = SQL & "" & jumlah & ") as Hasil "
                                            Dr3.Close()

                                            Using dr4 = OpenTrans(SQL)
                                                If dr4.Read Then
                                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then


                                                        If dr4("hasil") = 0 Then
                                                            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0
                                                        Else
                                                            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = Format(dr4("hasil"), "N2")
                                                        End If

                                                    Else
                                                        dr4.Close()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using
                                        Else
                                            Dr3.Close()
                                            CloseConn()
                                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            Else
                                DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0

                            End If

                        End Using
                    End If

                End If

                '============ akhir bulan 6

                DataGridView1.Rows(ind).Cells(CellStatus).Value = "SUBMITED"
            Next


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Bulan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        ElseIf ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Tahun Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf ComboBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub
        End If

        'ComboBox1.Enabled = False
        'ComboBox2.Enabled = False
        'ComboBox3.Enabled = False
        SD_Pilih_Produk.urutcmb = ComboBox1.SelectedIndex
        SD_Pilih_Produk.ShowDialog()
    End Sub

    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub kosong()
        ComboBox1.Items.Clear() : arrBulan.Clear() : arrBulanMM.Clear()
        ComboBox1.Items.Add("Januari") : arrBulan.Add("1") : arrBulanMM.Add("01")
        ComboBox1.Items.Add("Februari") : arrBulan.Add("2") : arrBulanMM.Add("02")
        ComboBox1.Items.Add("Maret") : arrBulan.Add("3") : arrBulanMM.Add("03")
        ComboBox1.Items.Add("April") : arrBulan.Add("4") : arrBulanMM.Add("04")
        ComboBox1.Items.Add("Mei") : arrBulan.Add("5") : arrBulanMM.Add("05")
        ComboBox1.Items.Add("Juni") : arrBulan.Add("6") : arrBulanMM.Add("06")
        ComboBox1.Items.Add("Juli") : arrBulan.Add("7") : arrBulanMM.Add("07")
        ComboBox1.Items.Add("Agustus") : arrBulan.Add("8") : arrBulanMM.Add("08")
        ComboBox1.Items.Add("September") : arrBulan.Add("9") : arrBulanMM.Add("09")
        ComboBox1.Items.Add("Oktober") : arrBulan.Add("10") : arrBulanMM.Add("10")
        ComboBox1.Items.Add("November") : arrBulan.Add("11") : arrBulanMM.Add("11")
        ComboBox1.Items.Add("Desember") : arrBulan.Add("12") : arrBulanMM.Add("12")
        ComboBox1.SelectedIndex = -1
        ComboBox1.Enabled = False

        ComboBox2.Items.Clear()
        Dim tahun_awal As Integer = Date.Now.Year - 2
        Dim tahun_akhir As Integer = Date.Now.Year + 2
        For a As Integer = tahun_awal To tahun_akhir
            ComboBox2.Items.Add(a)
        Next
        ComboBox2.SelectedIndex = -1
        'ComboBox2.Enabled = True
        ComboBox2.Enabled = False
        Btn_Refresh.Tag = "&Simpan"

        get_jam()

        Dim akses_ubah As String = ""
        Dim akses_realease As String = ""
        Dim akses_unrealease As String = ""
        Try
            OpenConn()

            If CekButtonRole("MRP_PPIC") = "Y" Then
                akses_ubah = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '
        Try
            OpenConn()

            If CekButtonRole("MRP_Realease") = "Y" Then
                akses_realease = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("MRP_Unrealease") = "Y" Then
                akses_unrealease = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            ComboBox3.Items.Clear()
            SQL = "select Kode_Stock_Owner from Stock_Owner where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox3.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using
            ComboBox3.SelectedIndex = -1
            ComboBox3.Enabled = True

            'get_no_faktur()
            TxtBarangMasuk_NoFaktur.Enabled = True



            If fstatus = "MRP_PPIC" Then
                If akses_ubah = "Y" Then
                    DataGridView1.Columns(Cell0).ReadOnly = False
                    DataGridView1.Columns(CellKd_Barang).ReadOnly = True
                    DataGridView1.Columns(CellNm_Barang).ReadOnly = True
                    DataGridView1.Columns(CellAvg_3Bln).ReadOnly = True
                    DataGridView1.Columns(CellStock_BB).ReadOnly = True
                    DataGridView1.Columns(CellOPRequesition).ReadOnly = True
                    DataGridView1.Columns(CellOPOrder).ReadOnly = True
                    DataGridView1.Columns(CellTotal).ReadOnly = True
                    DataGridView1.Columns(CellNBom_1).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_1).ReadOnly = False
                    DataGridView1.Columns(CellUrut_1).ReadOnly = True
                    DataGridView1.Columns(CellKosong_1).ReadOnly = True
                    DataGridView1.Columns(CellNBom_2).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_2).ReadOnly = False
                    DataGridView1.Columns(CellUrut_2).ReadOnly = True
                    DataGridView1.Columns(CellKosong_2).ReadOnly = True
                    DataGridView1.Columns(CellNBom_3).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_3).ReadOnly = False
                    DataGridView1.Columns(CellUrut_3).ReadOnly = True
                    DataGridView1.Columns(CellKosong_3).ReadOnly = True
                    DataGridView1.Columns(CellNBom_4).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_4).ReadOnly = False
                    DataGridView1.Columns(CellUrut_4).ReadOnly = True
                    DataGridView1.Columns(CellKosong_4).ReadOnly = True
                    DataGridView1.Columns(CellNBom_5).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_5).ReadOnly = False
                    DataGridView1.Columns(CellUrut_5).ReadOnly = True
                    DataGridView1.Columns(CellKosong_5).ReadOnly = True
                    DataGridView1.Columns(CellNBom_6).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_6).ReadOnly = False
                    DataGridView1.Columns(CellUrut_6).ReadOnly = True
                    DataGridView1.Columns(CellKosong_6).ReadOnly = True
                    DataGridView1.Columns(CellStatus).ReadOnly = True
                    DataGridView1.Columns(CellSatuanBarang).ReadOnly = True
                    DataGridView1.Columns(CellSatuanBarang).DisplayIndex = 3
                    CheckBox1.Enabled = False
                    Button1.Enabled = False
                    Btn_Refresh.Enabled = True
                    Btn_Refresh.Tag = "&Simpan"

                Else
                    DataGridView1.Columns(Cell0).ReadOnly = False
                    DataGridView1.Columns(CellKd_Barang).ReadOnly = True
                    DataGridView1.Columns(CellNm_Barang).ReadOnly = True
                    DataGridView1.Columns(CellAvg_3Bln).ReadOnly = True
                    DataGridView1.Columns(CellStock_BB).ReadOnly = True
                    DataGridView1.Columns(CellOPRequesition).ReadOnly = True
                    DataGridView1.Columns(CellOPOrder).ReadOnly = True
                    DataGridView1.Columns(CellTotal).ReadOnly = True
                    DataGridView1.Columns(CellNBom_1).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_1).ReadOnly = True
                    DataGridView1.Columns(CellUrut_1).ReadOnly = True
                    DataGridView1.Columns(CellKosong_1).ReadOnly = True
                    DataGridView1.Columns(CellNBom_2).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_2).ReadOnly = True
                    DataGridView1.Columns(CellUrut_2).ReadOnly = True
                    DataGridView1.Columns(CellKosong_2).ReadOnly = True
                    DataGridView1.Columns(CellNBom_3).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_3).ReadOnly = True
                    DataGridView1.Columns(CellUrut_3).ReadOnly = True
                    DataGridView1.Columns(CellKosong_3).ReadOnly = True
                    DataGridView1.Columns(CellNBom_4).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_4).ReadOnly = True
                    DataGridView1.Columns(CellUrut_4).ReadOnly = True
                    DataGridView1.Columns(CellKosong_4).ReadOnly = True
                    DataGridView1.Columns(CellNBom_5).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_5).ReadOnly = True
                    DataGridView1.Columns(CellUrut_5).ReadOnly = True
                    DataGridView1.Columns(CellKosong_5).ReadOnly = True
                    DataGridView1.Columns(CellNBom_6).ReadOnly = True
                    DataGridView1.Columns(CellNPPIC_6).ReadOnly = True
                    DataGridView1.Columns(CellUrut_6).ReadOnly = True
                    DataGridView1.Columns(CellKosong_6).ReadOnly = True
                    DataGridView1.Columns(CellStatus).ReadOnly = True

                    DataGridView1.Columns(CellSatuanBarang).ReadOnly = True
                    DataGridView1.Columns(CellSatuanBarang).DisplayIndex = 3

                    CheckBox1.Enabled = False
                    Button1.Enabled = False
                    Btn_Refresh.Enabled = False
                End If

                If akses_realease = "Y" Then
                    Btn_Realese.Enabled = True
                Else
                    Btn_Realese.Enabled = False
                End If

                If akses_unrealease = "Y" Then
                    btnUnRelease.Enabled = True
                Else
                    btnUnRelease.Enabled = False
                End If
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Try
        '    OpenConn()

        'If CekButtonRole("MRP_Formulator") = "Y" Then
        '    DataGridView1.Columns(Cell0).ReadOnly = False
        '    DataGridView1.Columns(CellKd_Barang).ReadOnly = True
        '    DataGridView1.Columns(CellNm_Barang).ReadOnly = True
        '    DataGridView1.Columns(CellAvg_3Bln).ReadOnly = True
        '    DataGridView1.Columns(CellStock_BB).ReadOnly = True
        '    DataGridView1.Columns(CellOPRequesition).ReadOnly = True
        '    DataGridView1.Columns(CellOPOrder).ReadOnly = True
        '    DataGridView1.Columns(CellTotal).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_1).ReadOnly = False
        '    DataGridView1.Columns(CellNPPIC_1).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_1).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_1).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_2).ReadOnly = False
        '    DataGridView1.Columns(CellNPPIC_2).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_2).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_2).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_3).ReadOnly = False
        '    DataGridView1.Columns(CellNPPIC_3).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_3).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_3).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_4).ReadOnly = False
        '    DataGridView1.Columns(CellNPPIC_4).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_4).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_4).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_5).ReadOnly = False
        '    DataGridView1.Columns(CellNPPIC_5).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_5).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_5).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_6).ReadOnly = False
        '    DataGridView1.Columns(CellNPPIC_6).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_6).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_6).ReadOnly = True
        '    DataGridView1.Columns(CellStatus).ReadOnly = True
        '    Button1.Enabled = True
        '    Btn_Refresh.Enabled = True
        'Else
        '    DataGridView1.Columns(Cell0).ReadOnly = False
        '    DataGridView1.Columns(CellKd_Barang).ReadOnly = True
        '    DataGridView1.Columns(CellNm_Barang).ReadOnly = True
        '    DataGridView1.Columns(CellAvg_3Bln).ReadOnly = True
        '    DataGridView1.Columns(CellStock_BB).ReadOnly = True
        '    DataGridView1.Columns(CellOPRequesition).ReadOnly = True
        '    DataGridView1.Columns(CellOPOrder).ReadOnly = True
        '    DataGridView1.Columns(CellTotal).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_1).ReadOnly = True
        '    DataGridView1.Columns(CellNPPIC_1).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_1).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_1).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_2).ReadOnly = True
        '    DataGridView1.Columns(CellNPPIC_2).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_2).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_2).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_3).ReadOnly = True
        '    DataGridView1.Columns(CellNPPIC_3).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_3).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_3).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_4).ReadOnly = True
        '    DataGridView1.Columns(CellNPPIC_4).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_4).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_4).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_5).ReadOnly = True
        '    DataGridView1.Columns(CellNPPIC_5).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_5).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_5).ReadOnly = True
        '    DataGridView1.Columns(CellNBom_6).ReadOnly = True
        '    DataGridView1.Columns(CellNPPIC_6).ReadOnly = True
        '    DataGridView1.Columns(CellUrut_6).ReadOnly = True
        '    DataGridView1.Columns(CellKosong_6).ReadOnly = True
        '    DataGridView1.Columns(CellStatus).ReadOnly = True
        '    Button1.Enabled = False
        '    Btn_Refresh.Enabled = False
        'End If

        'CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'TxtBarangMasuk_NoFaktur.Text = "FO-24/08-0001"
        TextBox2.Text = ""
        TextBox2.Enabled = True
        DateTimePicker1.Enabled = True

        DateTimePicker1.Value = tgl_skg
        DataGridView1.Rows.Clear()

        Dim selectedDate As Date = DateTimePicker1.Value
        Dim selectedMonthName As String = selectedDate.ToString("MMMM", New Globalization.CultureInfo("id-ID"))
        Dim selectedYear As Integer = selectedDate.Year

        ComboBox1.SelectedItem = selectedMonthName
        ComboBox2.SelectedItem = selectedYear


    End Sub
    Private Sub get_no_faktur()
        Dim FPro_Results As String = "TMR"
        TxtBarangMasuk_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Transaksi_Material_Requsition", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        kosong()

        DataGridView1.Columns(Cell0).HeaderText = "#"
        DataGridView1.Columns(CellKd_Barang).HeaderText = "Kode Barang"
        DataGridView1.Columns(CellNm_Barang).HeaderText = "Nama Barang"
        DataGridView1.Columns(CellAvg_3Bln).HeaderText = "Avg 3 Bulan (Pcs)"
        DataGridView1.Columns(CellStock_BB).HeaderText = "Stok Bahan Baku"
        DataGridView1.Columns(CellOPRequesition).HeaderText = "Open Purchase Requsition"
        DataGridView1.Columns(CellOPOrder).HeaderText = "Open Purchase Order"
        DataGridView1.Columns(CellTotal).HeaderText = "Total Stock + Open PR + Open PO"
        DataGridView1.Columns(CellNBom_1).HeaderText = "BoM - Forecast "
        DataGridView1.Columns(CellNPPIC_1).HeaderText = "PPIC - Forecast "
        DataGridView1.Columns(CellUrut_1).HeaderText = "1"
        DataGridView1.Columns(CellKosong_1).HeaderText = ""
        DataGridView1.Columns(CellNBom_2).HeaderText = "BoM - Forecast "
        DataGridView1.Columns(CellNPPIC_2).HeaderText = "PPIC - Forecast "
        DataGridView1.Columns(CellUrut_2).HeaderText = "2"
        DataGridView1.Columns(CellKosong_2).HeaderText = ""
        DataGridView1.Columns(CellNBom_3).HeaderText = "BoM - Forecast "
        DataGridView1.Columns(CellNPPIC_3).HeaderText = "PPIC - Forecast "
        DataGridView1.Columns(CellUrut_3).HeaderText = "3"
        DataGridView1.Columns(CellKosong_3).HeaderText = ""
        DataGridView1.Columns(CellNBom_4).HeaderText = "BoM - Forecast "
        DataGridView1.Columns(CellNPPIC_4).HeaderText = "PPIC - Forecast "
        DataGridView1.Columns(CellUrut_4).HeaderText = "4"
        DataGridView1.Columns(CellKosong_4).HeaderText = ""
        DataGridView1.Columns(CellNBom_5).HeaderText = "BoM - Forecast "
        DataGridView1.Columns(CellNPPIC_5).HeaderText = "PPIC - Forecast "
        DataGridView1.Columns(CellUrut_5).HeaderText = "5"
        DataGridView1.Columns(CellKosong_5).HeaderText = ""
        DataGridView1.Columns(CellNBom_6).HeaderText = "BoM - Forecast "
        DataGridView1.Columns(CellNPPIC_6).HeaderText = "PPIC - Forecast "
        DataGridView1.Columns(CellUrut_6).HeaderText = "6"
        DataGridView1.Columns(CellKosong_6).HeaderText = ""
        'DataGridView1.Columns(CellReferensi).HeaderText = "Referensi"
        DataGridView1.Columns(CellStatus).HeaderText = "Status"

        'DataGridView1.Columns(Cell0).ReadOnly = False

        If Display_Transaksi_MaterialRequsition.asal = "isi_lv" Then
            TxtBarangMasuk_NoFaktur_Leave(TxtBarangMasuk_NoFaktur, e)
        ElseIf Display_Transaksi_MaterialRequsition.asal = "Ref" Then
            Try
                OpenConn()

                get_no_faktur()

                DataGridView1.Rows.Clear()
                SQL = "select a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner from EMI_Transaksi_Material_Requsition_Detail a,"
                SQL = SQL & "Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and "
                SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Display_Transaksi_MaterialRequsition.nfak & "' "
                SQL = SQL & "group by a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows(i).Cells(2).Value = .Rows(i).Item("Nama")
                            DataGridView1.Rows(i).Cells(3).Value = "0"
                            DataGridView1.Rows(i).Cells(4).Value = "0"
                            DataGridView1.Rows(i).Cells(5).Value = "0"
                            DataGridView1.Rows(i).Cells(6).Value = "0"
                            DataGridView1.Rows(i).Cells(7).Value = "0"
                            DataGridView1.Rows(i).Cells(8).Value = "0"
                            DataGridView1.Rows(i).Cells(9).Value = "0"
                            DataGridView1.Rows(i).Cells(10).Value = "0"
                            DataGridView1.Rows(i).Cells(11).Value = ""
                            DataGridView1.Rows(i).Cells(12).Value = "0"
                            DataGridView1.Rows(i).Cells(13).Value = "0"
                            DataGridView1.Rows(i).Cells(14).Value = "0"
                            DataGridView1.Rows(i).Cells(15).Value = ""
                            DataGridView1.Rows(i).Cells(16).Value = "0"
                            DataGridView1.Rows(i).Cells(17).Value = "0"
                            DataGridView1.Rows(i).Cells(18).Value = "0"
                            DataGridView1.Rows(i).Cells(19).Value = ""
                            DataGridView1.Rows(i).Cells(20).Value = "0"
                            DataGridView1.Rows(i).Cells(21).Value = "0"
                            DataGridView1.Rows(i).Cells(22).Value = "0"
                            DataGridView1.Rows(i).Cells(23).Value = ""
                            DataGridView1.Rows(i).Cells(24).Value = "0"
                            DataGridView1.Rows(i).Cells(25).Value = "0"
                            DataGridView1.Rows(i).Cells(26).Value = "0"
                            DataGridView1.Rows(i).Cells(27).Value = ""
                            DataGridView1.Rows(i).Cells(28).Value = "0"
                            DataGridView1.Rows(i).Cells(29).Value = "0"
                            DataGridView1.Rows(i).Cells(30).Value = "0"
                            DataGridView1.Rows(i).Cells(31).Value = ""
                            DataGridView1.Rows(i).Cells(32).Value = "NEW"
                            Btn_Refresh.Tag = "&Simpan"
                        Next
                    End With
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
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

        'Try
        '    OpenConn()

        '    Base_Language.Get_Languages_Global(Bahasa_Pilihan)

        '    Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

        '        Label1.Text = Base_Language.Lang_Jenis_Hewan_Judul
        '        Label2.Text = Base_Language.Lang_Jenis_Hewan_Kode
        '        Label3.Text = Base_Language.Lang_Jenis_Hewan_Keterangan
        '        Label4.Text = Base_Language.Lang_Jenis_Hewan_Kolom

        '        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Kode, 150, HorizontalAlignment.Left)
        '        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Keterangan, 725, HorizontalAlignment.Left)
        '        ListView1.View = View.Details

        '        

        '        CloseConn()
        '    Catch ex As Exception
        '        CloseConn()
        '        MessageBox.Show(ex.Message)
        '        Exit Sub

        '    End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            Exit Sub
        End If

        Start_Loading(Me)
        getdata()
        End_Loading(Me)



    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        If TxtBarangMasuk_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtBarangMasuk_NoFaktur.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        ElseIf ComboBox3.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Lokasi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub
        ElseIf ComboBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Bulan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        ElseIf ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Tahun Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        End If

        'For a As Integer = 0 To DataGridView1.Rows.Count - 1
        '    If DataGridView1.Rows.Item(a).Cells(Cell0).Value = True Then
        '        Get_Isi_Listview(a)
        '        If LvNBom_1 = 0 Then
        '            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNPPIC_1 = 0 Then
        '            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNBom_2 = 0 Then
        '            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNPPIC_2 = 0 Then
        '            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNBom_3 = 0 Then
        '            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNPPIC_3 = 0 Then
        '            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNBom_4 = 0 Then
        '            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNPPIC_4 = 0 Then
        '            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNBom_5 = 0 Then
        '            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNPPIC_5 = 0 Then
        '            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNBom_6 = 0 Then
        '            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        ElseIf LvNPPIC_6 = 0 Then
        '            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End If
        'Next

        'Dim fInsert As Boolean = False
        'For a As Integer = 0 To DataGridView1.Rows.Count - 1
        '    If DataGridView1.Rows.Item(a).Cells(Cell0).Value = True Then
        '        fInsert = True
        '        Exit For
        '    Else
        '        fInsert = False
        '    End If
        'Next

        'If fInsert = False Then
        '    MessageBox.Show("Pilih dahulu data yang mau disimpan....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If fstatus = "MRP_PPIC" Then
                If CekButtonRole("MRP_PPIC") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("anda tidak memiliki akses ! !")
                    Exit Sub
                End If

            Else
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub

            End If

            '''''If Btn_Refresh.Tag = "&Simpan" Then
            '''''    get_no_faktur()

            '''''    SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
            '''''    SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
            '''''    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox2.Text & "','" & ComboBox3.Text & "',"
            '''''    SQL = SQL & "'" & arrBulanMM.Item(ComboBox1.SelectedIndex) & "','" & ComboBox2.Text & "'"
            '''''    If CheckBox1.Checked = True Then
            '''''        SQL = SQL & ",'Y')"
            '''''    Else
            '''''        SQL = SQL & ",NULL)"
            '''''    End If
            '''''    ExecuteTrans(SQL)
            '''''Else
            '''''    SQL = "UPDATE EMI_Transaksi_Material_Requsition SET "
            '''''    If CheckBox1.Checked = True Then
            '''''        SQL = SQL & "flag_Referensi = 'Y' "
            '''''    Else
            '''''        SQL = SQL & "flag_Referensi = NULL "
            '''''    End If
            '''''    SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtBarangMasuk_NoFaktur.Text & "'"
            '''''    ExecuteTrans(SQL)
            '''''End If


            'BULAN KE 1
            For c As Integer = 0 To DataGridView1.Rows.Count - 1
                ' If DataGridView1.Rows.Item(c).Cells(Cell0).Value = True Then
                Get_Isi_Listview(c)

                    Dim fSO As String = ""
                    SQL = "select Top(1)a.Lokasi_Gudang from EMI_Kategori_Gudang_PerLokasi a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Kategori_Gudang = b.Id_Kategori_Gudang and "
                    SQL = SQL & "a.Kode_Stock_Owner = '" & ComboBox3.Text & "' and b.Kode_Barang = '" & LVKd_Barang & "'"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For i As Integer = 0 To .Rows.Count - 1
                                    fSO = .Rows(i).Item("Lokasi_Gudang")
                                Next
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data Tidak ada . . ! !")
                                Exit Sub
                            End If
                        End With
                    End Using

                    Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
                    Dim fthn As Integer = Val(ComboBox2.Text)
                    Dim b As String = ""
                    Dim x_no_urut_det As String = 0
                    Dim NBom_Lama As Double = 0
                    Dim NPPIC_Lama As Double = 0

                    For index = 0 To arrBulan.Count - 1
                        If arrBulan.Item(index) = a Then
                            'ComboBox1.SelectedIndex = index
                            b = arrBulanMM.Item(index)
                        End If
                    Next

                    'BULAN YANG DIPILIH
                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
                        Else
                            dr.Close()
                            get_no_faktur()

                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
                            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox2.Text & "','" & ComboBox3.Text & "',"
                            SQL = SQL & "'" & b & "','" & fthn & "'"
                            If CheckBox1.Checked = True Then
                                SQL = SQL & ",'Y')"
                            Else
                                SQL = SQL & ",NULL)"
                            End If
                            ExecuteTrans(SQL)

                        End If
                    End Using

                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition_detail a where no_faktur='" & TxtBarangMasuk_NoFaktur.Text & "' "
                    SQL = SQL & "And a.kode_barang='" & LVKd_Barang & "' and kode_stock_owner='" & fSO & "'  And kode_perusahaan='" & KodePerusahaan & "' "
                    Using dr4 = OpenTrans(SQL)
                        If Not dr4.Read Then
                            dr4.Close()
                        SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC, satuan "
                        SQL = SQL & ") VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
                        SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & 0 & "','" & 0 & "', '" & LvSatuanBarang & "')"
                        'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                        '    SQL = SQL & ",'Y')"
                        'Else
                        '    SQL = SQL & ",NULL)"
                        'End If
                        ExecuteTrans(SQL)

                            SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    x_no_urut_det = "" & Dr("urutan") & ""
                                End If
                            End Using

                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
                            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                            ExecuteTrans(SQL)

                        End If
                    End Using
                    'BULAN KE 1
                    If a = 12 Then
                        a = 1
                        fthn = fthn + 1
                    Else
                        a = a + 1
                    End If

                    For index = 0 To arrBulan.Count - 1
                        If arrBulan.Item(index) = a Then
                            'ComboBox1.SelectedIndex = index
                            b = arrBulanMM.Item(index)
                        End If
                    Next

                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
                        Else
                            dr.Close()
                            get_no_faktur()

                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
                            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox2.Text & "','" & ComboBox3.Text & "',"
                            SQL = SQL & "'" & b & "','" & fthn & "'"
                            If CheckBox1.Checked = True Then
                                SQL = SQL & ",'Y')"
                            Else
                                SQL = SQL & ",NULL)"
                            End If
                            ExecuteTrans(SQL)

                        End If
                    End Using

                    If LvUrut_1 = "" Then
                    SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan "
                    SQL = SQL & ") VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
                    SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_1) & "','" & HilangkanTanda(LvNPPIC_1) & "' , '" & LvSatuanBarang & "' )"
                    'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                    '    SQL = SQL & ",'Y')"
                    'Else
                    '    SQL = SQL & ",NULL)"
                    'End If
                    ExecuteTrans(SQL)

                        SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                x_no_urut_det = "" & Dr("urutan") & ""
                            End If
                        End Using

                        SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
                        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                        ExecuteTrans(SQL)

                    Else
                        NBom_Lama = 0
                        NPPIC_Lama = 0
                        SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_1 & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                NBom_Lama = dr("Nilai_Bom")
                                NPPIC_Lama = dr("Nilai_PPIC")
                            End If
                        End Using

                        If NBom_Lama <> HilangkanTanda(LvNBom_1) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_1) Then
                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_1 & "',"
                            SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
                            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                            ExecuteTrans(SQL)
                        End If

                        SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_1) & "' "
                        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_1) & "' "
                        'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                        '    SQL = SQL & ",Flag_Referensi = 'Y' "
                        'Else
                        '    SQL = SQL & ",Flag_Referensi = NULL "
                        'End If
                        SQL = SQL & "WHERE Urut = '" & LvUrut_1 & "'"
                        ExecuteTrans(SQL)
                    End If

                    'BULAN KE 2
                    If a = 12 Then
                        a = 1
                        fthn = fthn + 1
                    Else
                        a = a + 1
                    End If

                    For index = 0 To arrBulan.Count - 1
                        If arrBulan.Item(index) = a Then
                            'ComboBox1.SelectedIndex = index
                            b = arrBulanMM.Item(index)
                        End If
                    Next

                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
                        Else
                            dr.Close()
                            get_no_faktur()

                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
                            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox2.Text & "','" & ComboBox3.Text & "',"
                            SQL = SQL & "'" & b & "','" & fthn & "'"
                            If CheckBox1.Checked = True Then
                                SQL = SQL & ",'Y')"
                            Else
                                SQL = SQL & ",NULL)"
                            End If
                            ExecuteTrans(SQL)

                        End If
                    End Using

                    If LvUrut_2 = "" Then
                    SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC, satuan) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
                    SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_2) & "','" & HilangkanTanda(LvNPPIC_2) & "' , '" & LvSatuanBarang & "' )"
                    'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                    '    SQL = SQL & ",'Y')"
                    'Else
                    '    SQL = SQL & ",NULL)"
                    'End If
                    ExecuteTrans(SQL)

                        SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                x_no_urut_det = "" & Dr("urutan") & ""
                            End If
                        End Using

                        SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
                        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                        ExecuteTrans(SQL)

                    Else
                        NBom_Lama = 0
                        NPPIC_Lama = 0
                        SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_2 & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                NBom_Lama = dr("Nilai_Bom")
                                NPPIC_Lama = dr("Nilai_PPIC")
                            End If
                        End Using

                        If NBom_Lama <> HilangkanTanda(LvNBom_2) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_2) Then
                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_2 & "',"
                            SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
                            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                            ExecuteTrans(SQL)
                        End If

                        SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_2) & "' "
                        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_2) & "' "
                        'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                        '    SQL = SQL & ",Flag_Referensi = 'Y' "
                        'Else
                        '    SQL = SQL & ",Flag_Referensi = NULL "
                        'End If
                        SQL = SQL & "WHERE Urut = '" & LvUrut_2 & "'"
                        ExecuteTrans(SQL)
                    End If

                    'BULAN KE 3
                    If a = 12 Then
                        a = 1
                        fthn = fthn + 1
                    Else
                        a = a + 1
                    End If

                    For index = 0 To arrBulan.Count - 1
                        If arrBulan.Item(index) = a Then
                            'ComboBox1.SelectedIndex = index
                            b = arrBulanMM.Item(index)
                        End If
                    Next

                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
                        Else
                            dr.Close()
                            get_no_faktur()

                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
                            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox2.Text & "','" & ComboBox3.Text & "',"
                            SQL = SQL & "'" & b & "','" & fthn & "'"
                            If CheckBox1.Checked = True Then
                                SQL = SQL & ",'Y')"
                            Else
                                SQL = SQL & ",NULL)"
                            End If
                            ExecuteTrans(SQL)

                        End If
                    End Using

                    If LvUrut_3 = "" Then
                    SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
                    SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_3) & "','" & HilangkanTanda(LvNPPIC_3) & "', '" & LvSatuanBarang & "') "
                    'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                    '    SQL = SQL & ",'Y')"
                    'Else
                    '    SQL = SQL & ",NULL)"
                    'End If
                    ExecuteTrans(SQL)

                        SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                x_no_urut_det = "" & Dr("urutan") & ""
                            End If
                        End Using

                        SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
                        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                        ExecuteTrans(SQL)

                    Else
                        NBom_Lama = 0
                        NPPIC_Lama = 0
                        SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_3 & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                NBom_Lama = dr("Nilai_Bom")
                                NPPIC_Lama = dr("Nilai_PPIC")
                            End If
                        End Using

                        If NBom_Lama <> HilangkanTanda(LvNBom_3) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_3) Then
                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_3 & "',"
                            SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
                            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                            ExecuteTrans(SQL)
                        End If

                        SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_3) & "' "
                        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_3) & "' "
                        'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                        '    SQL = SQL & ",Flag_Referensi = 'Y' "
                        'Else
                        '    SQL = SQL & ",Flag_Referensi = NULL "
                        'End If
                        SQL = SQL & "WHERE Urut = '" & LvUrut_3 & "'"
                        ExecuteTrans(SQL)
                    End If

                    'BULAN KE 4
                    If a = 12 Then
                        a = 1
                        fthn = fthn + 1
                    Else
                        a = a + 1
                    End If

                    For index = 0 To arrBulan.Count - 1
                        If arrBulan.Item(index) = a Then
                            'ComboBox1.SelectedIndex = index
                            b = arrBulanMM.Item(index)
                        End If
                    Next

                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
                        Else
                            dr.Close()
                            get_no_faktur()

                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
                            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox2.Text & "','" & ComboBox3.Text & "',"
                            SQL = SQL & "'" & b & "','" & fthn & "'"
                            If CheckBox1.Checked = True Then
                                SQL = SQL & ",'Y')"
                            Else
                                SQL = SQL & ",NULL)"
                            End If
                            ExecuteTrans(SQL)

                        End If
                    End Using

                    If LvUrut_4 = "" Then
                    SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
                    SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_4) & "','" & HilangkanTanda(LvNPPIC_4) & "', '" & LvSatuanBarang & "')"
                    'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                    '    SQL = SQL & ",'Y')"
                    'Else
                    '    SQL = SQL & ",NULL)"
                    'End If
                    ExecuteTrans(SQL)

                        SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                x_no_urut_det = "" & Dr("urutan") & ""
                            End If
                        End Using

                        SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
                        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                        ExecuteTrans(SQL)

                    Else
                        NBom_Lama = 0
                        NPPIC_Lama = 0
                        SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_4 & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                NBom_Lama = dr("Nilai_Bom")
                                NPPIC_Lama = dr("Nilai_PPIC")
                            End If
                        End Using

                        If NBom_Lama <> HilangkanTanda(LvNBom_4) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_4) Then
                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_4 & "',"
                            SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
                            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                            ExecuteTrans(SQL)
                        End If

                        SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_4) & "' "
                        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_4) & "' "
                        'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                        '    SQL = SQL & ",Flag_Referensi = 'Y' "
                        'Else
                        '    SQL = SQL & ",Flag_Referensi = NULL "
                        'End If
                        SQL = SQL & "WHERE Urut = '" & LvUrut_4 & "'"
                        ExecuteTrans(SQL)
                    End If

                    'BULAN KE 5
                    If a = 12 Then
                        a = 1
                        fthn = fthn + 1
                    Else
                        a = a + 1
                    End If

                    For index = 0 To arrBulan.Count - 1
                        If arrBulan.Item(index) = a Then
                            'ComboBox1.SelectedIndex = index
                            b = arrBulanMM.Item(index)
                        End If
                    Next

                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
                        Else
                            dr.Close()
                            get_no_faktur()

                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
                            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox2.Text & "','" & ComboBox3.Text & "',"
                            SQL = SQL & "'" & b & "','" & fthn & "'"
                            If CheckBox1.Checked = True Then
                                SQL = SQL & ",'Y')"
                            Else
                                SQL = SQL & ",NULL)"
                            End If
                            ExecuteTrans(SQL)

                        End If
                    End Using

                    If LvUrut_5 = "" Then
                    SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
                    SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_5) & "','" & HilangkanTanda(LvNPPIC_5) & "', '" & LvSatuanBarang & "') "
                    'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                    '    SQL = SQL & ",'Y')"
                    'Else
                    '    SQL = SQL & ",NULL)"
                    'End If
                    ExecuteTrans(SQL)

                        SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                x_no_urut_det = "" & Dr("urutan") & ""
                            End If
                        End Using

                        SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
                        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                        ExecuteTrans(SQL)

                    Else
                        NBom_Lama = 0
                        NPPIC_Lama = 0
                        SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_5 & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                NBom_Lama = dr("Nilai_Bom")
                                NPPIC_Lama = dr("Nilai_PPIC")
                            End If
                        End Using

                        If NBom_Lama <> HilangkanTanda(LvNBom_5) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_5) Then
                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_5 & "',"
                            SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
                            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                            ExecuteTrans(SQL)
                        End If

                        SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_5) & "' "
                        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_5) & "' "
                        'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                        '    SQL = SQL & ",Flag_Referensi = 'Y' "
                        'Else
                        '    SQL = SQL & ",Flag_Referensi = NULL "
                        'End If
                        SQL = SQL & "WHERE Urut = '" & LvUrut_5 & "'"
                        ExecuteTrans(SQL)
                    End If

                    'BULAN KE 6
                    If a = 12 Then
                        a = 1
                        fthn = fthn + 1
                    Else
                        a = a + 1
                    End If

                    For index = 0 To arrBulan.Count - 1
                        If arrBulan.Item(index) = a Then
                            'ComboBox1.SelectedIndex = index
                            b = arrBulanMM.Item(index)
                        End If
                    Next

                    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
                    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
                        Else
                            dr.Close()
                            get_no_faktur()

                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
                            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox2.Text & "','" & ComboBox3.Text & "',"
                            SQL = SQL & "'" & b & "','" & fthn & "'"
                            If CheckBox1.Checked = True Then
                                SQL = SQL & ",'Y')"
                            Else
                                SQL = SQL & ",NULL)"
                            End If
                            ExecuteTrans(SQL)

                        End If
                    End Using
                    If LvUrut_6 = "" Then
                    SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
                    SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_6) & "','" & HilangkanTanda(LvNPPIC_6) & "', '" & LvSatuanBarang & "')"
                    'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
                    '    SQL = SQL & ",'Y')"
                    'Else
                    '    SQL = SQL & ",NULL)"
                    'End If
                    ExecuteTrans(SQL)

                        SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                x_no_urut_det = "" & Dr("urutan") & ""
                            End If
                        End Using

                        SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
                        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                        ExecuteTrans(SQL)

                    Else
                        NBom_Lama = 0
                        NPPIC_Lama = 0
                        SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_6 & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                NBom_Lama = dr("Nilai_Bom")
                                NPPIC_Lama = dr("Nilai_PPIC")
                            End If
                        End Using

                        If NBom_Lama <> HilangkanTanda(LvNBom_6) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_6) Then
                            SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
                            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_6 & "',"
                            SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
                            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
                            ExecuteTrans(SQL)
                        End If

                        SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_6) & "' "
                        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_6) & "' WHERE Urut = '" & LvUrut_6 & "'"
                        ExecuteTrans(SQL)
                    End If

                'End If
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit

        arrCellInputPPIC.AddRange(New Object() {
                        CellNPPIC_1, CellNPPIC_2, CellNPPIC_3, CellNPPIC_4,
                        CellNPPIC_5, CellNPPIC_6
        })

        Get_Isi_Listview(DataGridView1.CurrentRow.Index)
        If IsNumeric(LvNBom_1) = False Or Val(LvNBom_1) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNBom_1).Value = 0
        ElseIf IsNumeric(LvNPPIC_1) = False Or Val(LvNPPIC_1) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNPPIC_1).Value = 0
        ElseIf IsNumeric(LvNBom_2) = False Or Val(LvNBom_2) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNBom_2).Value = 0
        ElseIf IsNumeric(LvNPPIC_2) = False Or Val(LvNPPIC_2) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNPPIC_2).Value = 0
        ElseIf IsNumeric(LvNBom_3) = False Or Val(LvNBom_3) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNBom_3).Value = 0
        ElseIf IsNumeric(LvNPPIC_3) = False Or Val(LvNPPIC_3) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNPPIC_3).Value = 0
        ElseIf IsNumeric(LvNBom_4) = False Or Val(LvNBom_4) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNBom_4).Value = 0
        ElseIf IsNumeric(LvNPPIC_4) = False Or Val(LvNPPIC_4) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNPPIC_4).Value = 0
        ElseIf IsNumeric(LvNBom_5) = False Or Val(LvNBom_5) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNBom_5).Value = 0
        ElseIf IsNumeric(LvNPPIC_5) = False Or Val(LvNPPIC_5) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNPPIC_5).Value = 0
        ElseIf IsNumeric(LvNBom_6) = False Or Val(LvNBom_6) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNBom_6).Value = 0
        ElseIf IsNumeric(LvNPPIC_6) = False Or Val(LvNPPIC_6) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNPPIC_6).Value = 0
        End If


        '======================
        '=     SET FORMAT     =
        '======================
        If arrCellInputPPIC.Contains(DataGridView1.CurrentCell.ColumnIndex) Then

            Dim cellKuantity As String = DataGridView1.CurrentCell.Value

            If cellKuantity.Contains(",") Then
                MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DataGridView1.CurrentCell.Value = Format(0, "N2")
                Exit Sub
            End If

            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

            DataGridView1.CurrentCell.Value = formattedValue
        End If

    End Sub

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter

        arrCellInputPPIC.AddRange(New Object() {
                        CellNPPIC_1, CellNPPIC_2, CellNPPIC_3, CellNPPIC_4,
                        CellNPPIC_5, CellNPPIC_6
        })

        If arrCellInputPPIC.Contains(DataGridView1.CurrentCell.ColumnIndex) Then
            Dim cellKuantity As String = DataGridView1.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            DataGridView1.CurrentCell.Value = nilai
        End If
    End Sub

    Private Sub DataGridView1_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellLeave

        arrCellInputPPIC.AddRange(New Object() {
                        CellNPPIC_1, CellNPPIC_2, CellNPPIC_3, CellNPPIC_4,
                        CellNPPIC_5, CellNPPIC_6
        })

        If arrCellInputPPIC.Contains(DataGridView1.CurrentCell.ColumnIndex) Then
            Dim cellKuantity As String = DataGridView1.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

            DataGridView1.CurrentCell.Value = formattedValue

        End If
    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            Exit Sub
        End If

        Start_Loading(Me)
        getdata()
        End_Loading(Me)
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        kosong()
    End Sub

    Private Sub Btn_Realese_Click(sender As Object, e As EventArgs) Handles Btn_Realese.Click
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If fstatus = "MRP_PPIC" Then
                If CekButtonRole("MRP_Realease") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("anda tidak memiliki akses ! !")
                    Exit Sub
                End If

            Else
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub

            End If

            Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
            Dim fthn As Integer = Val(ComboBox2.Text)
            Dim b As String = ""



            '--- 1
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If

            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)

                End If
            Next
            Dim no_faktur As String = ""
            SQL = "Select no_faktur,flag_validasi,flag_validasi_ppic from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    If fstatus = "MRP_Formulator" Then
                        If General_Class.CekNULL(dr("flag_validasi")) = "Y" Then
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Data sudah pernah di submit sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If

                    If fstatus = "MRP_PPIC" Then
                        If General_Class.CekNULL(dr("flag_validasi_ppic")) = "Y" Then
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Data sudah pernah di submit sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If


                    no_faktur = dr("no_faktur")
                    Else

                        dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'VERIFICATION' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'SUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If


            '--- 2
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next

            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'VERIFICATION' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'SUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If

            '--- 3
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next


            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'VERIFICATION' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'SUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If

            '--- 4
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next

            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'VERIFICATION' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'SUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If

            '--- 5
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next

            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'VERIFICATION' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'SUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If

            '--- 6
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next

            SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'VERIFICATION' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
                SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "Status_Data = 'SUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If


            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data berhasil divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnUnRelease.Click
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If fstatus = "MRP_PPIC" Then
                If CekButtonRole("MRP_Unrealease") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("anda tidak memiliki akses ! !")
                    Exit Sub
                End If

            Else
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub

            End If

            Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
            Dim fthn As Integer = Val(ComboBox2.Text)
            Dim b As String = ""



            '--- 1
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If

            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)

                End If
            Next
            Dim no_faktur As String = ""
            SQL = "Select no_faktur,flag_validasi,flag_validasi_ppic from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    If fstatus = "MRP_Formulator" Then
                        If General_Class.CekNULL(dr("flag_validasi")) = "" Then
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Data belum pernah di submit!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If

                    If fstatus = "MRP_PPIC" Then
                        If General_Class.CekNULL(dr("flag_validasi_ppic")) = "" Then
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Data belum pernah di submit!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If

                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
                SQL = SQL & "Tanggal_Validasi_PPIC = null,"
                SQL = SQL & "Jam_Validasi_PPIC = null, "
                SQL = SQL & "Status_Data = 'SUBMITED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
                SQL = SQL & "Tanggal_Validasi = null,"
                SQL = SQL & "Jam_Validasi = null, "
                SQL = SQL & "Status_Data = 'UNSUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If


            '--- 2
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next

            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
                SQL = SQL & "Tanggal_Validasi_PPIC = null,"
                SQL = SQL & "Jam_Validasi_PPIC = null, "
                SQL = SQL & "Status_Data = 'SUBMITED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
                SQL = SQL & "Tanggal_Validasi = null,"
                SQL = SQL & "Jam_Validasi = null, "
                SQL = SQL & "Status_Data = 'UNSUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If

            '--- 3
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next


            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
                SQL = SQL & "Tanggal_Validasi_PPIC = null,"
                SQL = SQL & "Jam_Validasi_PPIC = null, "
                SQL = SQL & "Status_Data = 'SUBMITED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
                SQL = SQL & "Tanggal_Validasi = null,"
                SQL = SQL & "Jam_Validasi = null, "
                SQL = SQL & "Status_Data = 'UNSUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If

            '--- 4
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next

            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
                SQL = SQL & "Tanggal_Validasi_PPIC = null,"
                SQL = SQL & "Jam_Validasi_PPIC = null, "
                SQL = SQL & "Status_Data = 'SUBMITED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
                SQL = SQL & "Tanggal_Validasi = null,"
                SQL = SQL & "Jam_Validasi = null, "
                SQL = SQL & "Status_Data = 'UNSUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If

            '--- 5
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next

            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
                SQL = SQL & "Tanggal_Validasi_PPIC = null,"
                SQL = SQL & "Jam_Validasi_PPIC = null, "
                SQL = SQL & "Status_Data = 'SUBMITED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
                SQL = SQL & "Tanggal_Validasi = null,"
                SQL = SQL & "Jam_Validasi = null, "
                SQL = SQL & "Status_Data = 'UNSUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If

            '--- 6
            If a = 12 Then
                a = 1
                fthn = fthn + 1
            Else
                a = a + 1
            End If
            For index = 0 To arrBulan.Count - 1
                If arrBulan.Item(index) = a Then
                    b = arrBulanMM.Item(index)
                End If
            Next

            SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
            SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("no_faktur")
                Else

                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If fstatus = "MRP_PPIC" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
                SQL = SQL & "Tanggal_Validasi_PPIC = null,"
                SQL = SQL & "Jam_Validasi_PPIC = null, "
                SQL = SQL & "Status_Data = 'SUBMITED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)



            ElseIf fstatus = "MRP_Formulator" Then
                SQL = "update EMI_Transaksi_Material_Requsition set "
                SQL = SQL & "Flag_Validasi = null,User_Validasi = null ,"
                SQL = SQL & "Tanggal_Validasi = null,"
                SQL = SQL & "Jam_Validasi = null , "
                SQL = SQL & "Status_Data = 'UNSUBMITTED' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
                ExecuteTrans(SQL)
            End If


            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data berhasil dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub



    Public Sub TxtBarangMasuk_NoFaktur_Leave(sender As Object, e As EventArgs) Handles TxtBarangMasuk_NoFaktur.Leave
        If TxtBarangMasuk_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtBarangMasuk_NoFaktur.Focus() : Exit Sub
        End If
        Dim ada_data As Boolean = False
        Try
            OpenConn()

            SQL = "select No_Faktur, Tanggal, Keterangan, Lokasi, Bulan, Tahun, flag_referensi, Flag_validasi_PPIC from EMI_Transaksi_Material_Requsition where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Faktur = '" & TxtBarangMasuk_NoFaktur.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    ada_data = True
                    TextBox2.Text = dr("Keterangan")
                    'DateTimePicker1.Value = Format(dr("Tanggal"), "dd-MMMM-yyyy")
                    ComboBox3.Text = dr("Lokasi")
                    For index = 0 To arrBulanMM.Count - 1
                        If arrBulanMM.Item(index) = dr("Bulan") Then
                            ComboBox1.SelectedIndex = index
                        End If
                    Next

                    If General_Class.CekNULL(dr("Flag_referensi")) = "" Then
                        CheckBox1.Checked = False
                    Else
                        CheckBox1.Checked = True
                    End If

                    '======================================================
                    '=     DISABLE KETERANGAN JIKA SUDAH RELEASE SAJA     =
                    '======================================================
                    If fstatus = "MRP_PPIC" Then

                        If General_Class.CekNULL(dr("Flag_validasi_PPIC")) = "Y" Then
                            TextBox2.Enabled = False
                        Else
                            TextBox2.Enabled = True
                        End If

                    End If


                    ComboBox1.Text = dr("Bulan")
                    ComboBox2.Text = dr("Tahun")
                    'DateTimePicker1.Enabled = False
                    ComboBox1.Enabled = False
                    ComboBox2.Enabled = False
                    ComboBox3.Enabled = True
                    Btn_Refresh.Tag = "&Refresh"
                Else
                    dr.Close()
                    get_no_faktur()
                    TextBox2.Text = ""
                    DateTimePicker1.Value = Now
                    ComboBox3.SelectedIndex = -1
                    ComboBox1.SelectedIndex = -1
                    ComboBox2.SelectedIndex = -1
                    'DateTimePicker1.Enabled = True
                    TextBox2.Enabled = True
                    ComboBox1.Enabled = False
                    ComboBox2.Enabled = False
                    ComboBox3.Enabled = True
                    CheckBox1.Checked = False
                    Btn_Refresh.Tag = "&Simpan"
                End If
            End Using

            DataGridView1.Rows.Clear()
            Arrbarang.Clear()
            Arrlokasi.Clear()
            ArrNama.Clear()
            SQL = "select a.No_Faktur,c.Bulan,c.Tahun,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner, b.good_stock,b.satuan "
            SQL = SQL & "from EMI_Transaksi_Material_Requsition_Detail a,Barang b , EMI_Transaksi_Material_Requsition c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang  "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TxtBarangMasuk_NoFaktur.Text & "' "
            SQL = SQL & "group by a.No_Faktur,c.Bulan,c.Tahun,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner,b.good_stock, b.satuan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Arrbarang.Add(.Rows(i).Item("Kode_Barang"))
                        Arrlokasi.Add(.Rows(i).Item("Kode_Stock_Owner"))
                        ArrNama.Add(.Rows(i).Item("Nama"))
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        If ada_data = True Then
            get_barang()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            Exit Sub
        End If

        Start_Loading(Me)
        getdata()
        End_Loading(Me)
    End Sub



    Private Sub DateTimePicker1_CloseUp(sender As Object, e As EventArgs) Handles DateTimePicker1.CloseUp
        If DateTimePicker1.Value = Nothing Then Exit Sub
        'If ComboBox3.SelectedIndex = -1 Then Exit Sub


        Dim selectedDate As Date = DateTimePicker1.Value
        Dim selectedMonthName As String = selectedDate.ToString("MMMM", New Globalization.CultureInfo("id-ID"))
        Dim selectedYear As Integer = selectedDate.Year

        'If selectedYear < tahun_awal Or selectedYear > tahun_akhir Then
        '    MessageBox.Show("Tahun harus dalam rentang dua tahun dari tahun ini.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        ComboBox1.SelectedItem = selectedMonthName
        ComboBox2.SelectedItem = selectedYear
    End Sub



End Class
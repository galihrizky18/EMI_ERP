Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports CrystalDecisions.Shared.Json


Public Class EMI_Analisis_Produksi
    Dim arrcari As New ArrayList
    Dim Jenis = "Master_Customer"

    Dim LvLokasi As String
    Dim LvKodeBarang As String
    Dim LvNamaBarang As String
    Dim LvSatuan As String
    Dim LvStock As String
    Dim LvKeepStock As String
    Dim LvAVG3Bln As String

    Dim lvPO As String

    Dim CellLokasi As Integer = 0
    Dim CellKodeBarang As Integer = 1
    Dim CellNamaBarang As Integer = 2
    Dim CellSatuan As Integer = 3
    Dim CellStock As Integer = 4
    Dim CellKeepStock As Integer = 5
    Dim CellAVG3Bln As Integer = 6

    Dim ind As Integer
    Dim data_awal As Integer = 6
    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvLokasi = DgvData.Rows(No_Index).Cells(CellLokasi).Value.ToString
        LvKodeBarang = DgvData.Rows(No_Index).Cells(CellKodeBarang).Value.ToString
        LvNamaBarang = DgvData.Rows(No_Index).Cells(CellNamaBarang).Value.ToString
        LvSatuan = DgvData.Rows(No_Index).Cells(CellSatuan).Value.ToString
        LvStock = DgvData.Rows(No_Index).Cells(CellStock).Value.ToString
        LvKeepStock = DgvData.Rows(No_Index).Cells(CellKeepStock).Value.ToString
        LvAVG3Bln = DgvData.Rows(No_Index).Cells(CellAVG3Bln).Value.ToString

    End Sub


    Public Sub Get_data(ByVal No_Index As Integer, ByVal No_Index2 As Integer)

        lvPO = DgvData.Rows(No_Index).Cells(No_Index2).Value.ToString

    End Sub

    Private Sub Kosong()
        ComboBox4.Items.Clear()
        ComboBox4.Items.Add("Per PO")
        ComboBox4.Items.Add("Per Bulan")
        ComboBox4.Items.Add("Total PO")

        ComboBox4.SelectedIndex = 2

        Cari()

    End Sub
    Public Sub tambah_kolom()
        DgvData.Columns.Clear()
        Dim col As New DataGridViewTextBoxColumn
        col = New DataGridViewTextBoxColumn
        col.HeaderText = "Lokasi Gudang"
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DgvData.Columns.Insert(DgvData.ColumnCount, col)

        col = New DataGridViewTextBoxColumn
        col.HeaderText = "Kode Barang"
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DgvData.Columns.Insert(DgvData.ColumnCount, col)


        col = New DataGridViewTextBoxColumn
        col.HeaderText = "nama"
        col.ReadOnly = True
        col.Visible = True
        col.Width = 250
        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DgvData.Columns.Insert(DgvData.ColumnCount, col)

        col = New DataGridViewTextBoxColumn
        col.HeaderText = "Satuan"
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvData.Columns.Insert(DgvData.ColumnCount, col)

        col = New DataGridViewTextBoxColumn
        col.HeaderText = "Stock"
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvData.Columns.Insert(DgvData.ColumnCount, col)

        col = New DataGridViewTextBoxColumn
        col.HeaderText = "Keep Stock"
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvData.Columns.Insert(DgvData.ColumnCount, col)

        col = New DataGridViewTextBoxColumn
        col.HeaderText = "AVG 3 Bulan"
        col.ReadOnly = True
        col.Visible = True
        col.Width = 150
        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvData.Columns.Insert(DgvData.ColumnCount, col)


    End Sub
    Private Sub Display_Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try

            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Cari.Text = Base_Language.Lang_Global_Refresh
            Label1.Text = Base_Language.Lang_Global_List_Inquiry_PO



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        tambah_kolom()

        Kosong()

    End Sub

    Private Sub Display_Customer_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Cari()
    End Sub

    Private Sub Cari()

        tambah_kolom()
        Try
            OpenConn()
            Dim format As String = ""

            If ComboBox4.SelectedIndex = 0 Then
                format = "dd MMM yyyy"
            ElseIf ComboBox4.SelectedIndex = 1 Then
                format = "MMM yyyy"
            ElseIf ComboBox4.SelectedIndex = 2 Then
                format = "yyyy"
            Else
                CloseConn()
                MessageBox.Show("Data Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            ind = 0
            Dim Data As String = ""
            Dim arrData As New ArrayList
            arrData.Clear()
            SQL = "Select Format(x.Tanggal,'" & format & "') as tanggal "
            SQL = SQL & "From emi_pembelian_po x, EMI_Pembelian_PO_Detail y Where "
            SQL = SQL & "x.Kode_Perusahaan = y.kode_perusahaan And x.NO_faktur = y.no_faktur And x.status Is null And x.flag_bm Is null "
            SQL = SQL & "Group By Format(x.Tanggal,'" & format & "') "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    If ind <> 0 Then
                        Data = Data & ", "
                    End If
                    Data = Data & "[" & dr("tanggal") & "]"

                    arrData.Add(dr("tanggal"))
                    Dim col As New DataGridViewTextBoxColumn
                    col = New DataGridViewTextBoxColumn
                    col.HeaderText = "PO " & dr("tanggal")
                    col.ReadOnly = True
                    col.Visible = True
                    col.Width = 150
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    DgvData.Columns.Insert(DgvData.ColumnCount, col)

                    ind += 1
                Loop
            End Using


            SQL = ";with Data_Pemakaian_PerBulan as("

            SQL = SQL & "Select y.Kode_stock_owner, y.Kode_barang,format(x.Tanggal,'MMyyyy') as BulanTahun, y.satuan, sum(jumlah) as jumlah "
            SQL = SQL & "From Emi_permintaan_bahan_baku x, Emi_Permintaan_Bahan_Baku_Detail y Where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.no_faktur = y.no_faktur And "
            SQL = SQL & "x.status Is null And x.flag_val ='Y'and x.tanggal between '2024-01-01' and '2024-12-01' "
            SQL = SQL & "group by y.Kode_stock_owner, y.Kode_barang, y.satuan,format(x.Tanggal,'MMyyyy') "

            SQL = SQL & ") "
            SQL = SQL & "Select a.Kode_Stock_Owner, a.Kode_Barang, a.nama,a.satuan, a.Good_Stock, a.Stock_PO, "

            SQL = SQL & "isnull((select avg(jumlah) from Data_Pemakaian_PerBulan x where "
            SQL = SQL & "x.Kode_stock_owner = a.Kode_stock_Owner And x.Kode_Barang = a.kode_barang),0) As AVG, "

            SQL = SQL & "d.satuan as satuan_display, "

            SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA', a.KOde_Barang, a.satuan, d.satuan, a.Good_Stock) as Good_stock_Display, "
            SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA', a.KOde_Barang, a.satuan, d.satuan, a.Stock_PO) as Stock_PO_Display, "
            SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA', a.KOde_Barang, a.satuan, d.satuan, "
            SQL = SQL & "isnull((select avg(jumlah) from Data_Pemakaian_PerBulan x where "
            SQL = SQL & "x.Kode_stock_owner = a.Kode_stock_Owner And x.Kode_Barang = a.kode_barang),0)) As AVG_Display "

            SQL = SQL & "From barang a, Stock_Owner_Gudang b, EMI_Group_Jenis c, Barang_Detail_Satuan d "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner And "
            SQL = SQL & "a.id_group_jenis = c.Id_group_jenis And a.Kode_Perusahaan = d.Kode_Perusahaan And "
            SQL = SQL & "(c.Flag_Raw_Material ='Y' or c.Flag_Packaging='Y') and "
            SQL = SQL & "a.Kode_Barang = d.Kode_barang And d.Flag_Tampil_Display ='Y' "
            SQL = SQL & "order by Kode_Stock_Owner, Kode_Barang "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index = 0 To .Rows.Count - 1

                        DgvData.Rows.Add(1)
                        DgvData.Rows.Item(index).Cells(CellLokasi).Value = .Rows(index).Item("Kode_stock_owner")
                        DgvData.Rows.Item(index).Cells(CellKodeBarang).Value = .Rows(index).Item("Kode_barang")
                        DgvData.Rows.Item(index).Cells(CellNamaBarang).Value = .Rows(index).Item("nama")
                        DgvData.Rows.Item(index).Cells(CellSatuan).Value = .Rows(index).Item("satuan_display")
                        DgvData.Rows.Item(index).Cells(CellStock).Value = .Rows(index).Item("Good_stock_Display")
                        DgvData.Rows.Item(index).Cells(CellKeepStock).Value = .Rows(index).Item("Stock_PO_Display")
                        DgvData.Rows.Item(index).Cells(CellAVG3Bln).Value = .Rows(index).Item("AVG_Display")

                        SQL = "Select * from( "
                        SQL = SQL & "select y.kode_stock_owner, y.kode_barang, Format(x.Tanggal,'" & format & "') as tanggal, satuan_barang, sum(nilai_barang) as Jumlah_barang, z.satuan, "
                        SQL = SQL & "dbo.Ubah_Satuan(y.kode_perusahaan,'MASA', y.KOde_Barang, satuan_barang, z.satuan, sum(nilai_barang)) as jumlah "
                        SQL = SQL & "From emi_pembelian_po x, EMI_Pembelian_PO_Detail y, Barang_Detail_Satuan z Where "
                        SQL = SQL & "x.Kode_Perusahaan = y.kode_perusahaan And x.NO_faktur = y.no_faktur And x.status Is null And x.flag_bm Is null and "
                        SQL = SQL & "y.Kode_Perusahaan = z.kode_perusahaan and y.Kode_Barang = z.Kode_barang And z.Flag_Tampil_Display ='Y' "
                        SQL = SQL & "and y.Kode_Barang='" & .Rows(index).Item("Kode_barang") & "' and y.Kode_stock_Owner='" & .Rows(index).Item("Kode_stock_owner") & "' "
                        SQL = SQL & "Group By y.Kode_Perusahaan, y.kode_stock_owner, y.kode_barang, Format(x.Tanggal,'" & format & "'), satuan_barang, z.satuan "
                        SQL = SQL & ") as sourcetable "
                        SQL = SQL & "PIVOT "
                        SQL = SQL & "( "
                        SQL = SQL & "sum(jumlah) "
                        SQL = SQL & "For tanggal IN(" & Data & ")  "
                        SQL = SQL & ")AS PivotTable1 "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                For ind2 = 0 To arrData.Count - 1
                                    If IsDBNull(dr(arrData.Item(ind2))) Then
                                        DgvData.Rows.Item(index).Cells(data_awal + 1 + ind2).Value = 0
                                    Else
                                        DgvData.Rows.Item(index).Cells(data_awal + 1 + ind2).Value = dr(arrData.Item(ind2))
                                    End If
                                Next
                            Else
                                For ind2 = 0 To arrData.Count - 1

                                    DgvData.Rows.Item(index).Cells(data_awal + 1 + ind2).Value = 0

                                Next
                            End If
                        End Using
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
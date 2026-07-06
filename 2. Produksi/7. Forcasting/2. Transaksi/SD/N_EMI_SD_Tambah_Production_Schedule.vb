Imports System.Net
Imports System.Reflection



Public Class N_EMI_SD_Tambah_Production_Schedule
    Public TanggalDariKalender As DateTime
    Public fStatus As String
    Public mustUpdate As Boolean = False

    Dim lvChkBox As String
    Dim lvKdBrg As String
    Dim lvNmBrg As String
    Dim lvFormula As String
    Dim lvJmlhPerbulan As String
    Dim lvJmlhTerpakai As String
    Dim LvSatuan As String
    Dim LvJumlah As String
    Dim LvNo_Faktur As String
    Dim LvNo_Urut As String
    Dim LvStockOwner As String
    Dim LvRv As String
    Dim LvTanggalStart As String
    Dim LvJamStart As String
    Dim LvTanggalEnd As String
    Dim LvJamEnd As String
    Dim LvKeterangan As String

    Dim LvIdJenisProduk As String
    Dim LvKeteranganJenisProduk As String
    Dim LvSdhPO As String

    ' untuk pengecekan yg di input sama atau tidak dgn db 
    ' jika sama tidak perlu di cek
    Dim LvJmlhDB As String

    Public fTransSalesForcasting As String = "NS"


    Private dtp As DateTimePicker
    Private rowIndex As Integer
    Private colIndex As Integer

    Dim CellChkBox As Integer = 0
    Dim CellKdBrg As Integer = 1
    Dim CellNmBrg As Integer = 2
    Dim cellFormula As Integer = 3
    Dim cellJmlhPerbulan As Integer = 4
    Dim cellJumlahTerpakai As Integer = 5
    Dim cellSatuan As Integer = 6
    Dim cellJumlah As Integer = 7
    Dim cellNo_Faktur As Integer = 8
    Dim cellNo_Urut As Integer = 9
    Dim cellStockOwner As Integer = 10
    Dim cellRv As Integer = 11
    Dim cellTanggalStart As Integer = 12
    Dim cellJamStart As Integer = 13
    Dim cellTanggalEnd As Integer = 14
    Dim cellJamEnd As Integer = 15
    Dim cellKeterangan As Integer = 16

    Dim cellIdJenisProduk As Integer = 17
    Dim cellKeteranganJenisProduk As Integer = 18
    Dim cellFlagSudahPO As Integer = 19
    Dim cellJumlahDB As Integer = 20



    Dim bulan As String = ""
    Dim tahun As String = ""


    Private isStart As Boolean
    Private isTimeMode As Boolean
    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        lvChkBox = DataGridView1.Rows(No_Index).Cells(CellChkBox).Value
        lvKdBrg = DataGridView1.Rows(No_Index).Cells(CellKdBrg).Value
        lvNmBrg = DataGridView1.Rows(No_Index).Cells(CellNmBrg).Value
        lvFormula = DataGridView1.Rows(No_Index).Cells(cellFormula).Value
        lvJmlhPerbulan = DataGridView1.Rows(No_Index).Cells(cellJmlhPerbulan).Value
        lvJmlhTerpakai = DataGridView1.Rows(No_Index).Cells(cellJumlahTerpakai).Value
        LvSatuan = DataGridView1.Rows(No_Index).Cells(cellSatuan).Value
        LvJumlah = DataGridView1.Rows(No_Index).Cells(cellJumlah).Value
        LvNo_Faktur = DataGridView1.Rows(No_Index).Cells(cellNo_Faktur).Value
        LvNo_Urut = DataGridView1.Rows(No_Index).Cells(cellNo_Urut).Value
        LvStockOwner = DataGridView1.Rows(No_Index).Cells(cellStockOwner).Value
        LvRv = DataGridView1.Rows(No_Index).Cells(cellRv).Value
        LvTanggalStart = DataGridView1.Rows(No_Index).Cells(cellTanggalStart).Value
        LvJamStart = DataGridView1.Rows(No_Index).Cells(cellJamStart).Value
        LvTanggalEnd = DataGridView1.Rows(No_Index).Cells(cellTanggalEnd).Value
        LvJamEnd = DataGridView1.Rows(No_Index).Cells(cellJamEnd).Value
        LvKeterangan = DataGridView1.Rows(No_Index).Cells(cellKeterangan).Value

        LvIdJenisProduk = DataGridView1.Rows(No_Index).Cells(cellIdJenisProduk).Value
        LvKeteranganJenisProduk = DataGridView1.Rows(No_Index).Cells(cellKeteranganJenisProduk).Value
        LvSdhPO = DataGridView1.Rows(No_Index).Cells(cellFlagSudahPO).Value
        LvJmlhDB = DataGridView1.Rows(No_Index).Cells(cellJumlahDB).Value
    End Sub
    Private Sub dtp_TextChanged(sender As Object, e As EventArgs)
        DataGridView1.CurrentCell.Value = dtp.Text
    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Get_Isi_Listview(e.RowIndex)

        If LvSdhPO <> "Y" Then
            If e.ColumnIndex = cellTanggalStart Or e.ColumnIndex = cellTanggalEnd Then

                isTimeMode = False

                rowIndex = e.RowIndex
                colIndex = e.ColumnIndex

                dtp.Format = DateTimePickerFormat.Custom
                dtp.CustomFormat = "yyyy-MM-dd"
                dtp.ShowUpDown = False

                Dim rect As Rectangle = DataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)

                dtp.Size = rect.Size
                dtp.Location = rect.Location
                dtp.Visible = True

                Dim tempDate As DateTime

                If DataGridView1.CurrentCell.Value IsNot Nothing AndAlso
                   DateTime.TryParse(DataGridView1.CurrentCell.Value.ToString(), tempDate) Then

                    dtp.Value = tempDate

                Else
                    dtp.Value = Date.Now
                End If


            ElseIf e.ColumnIndex = cellJamStart Or e.ColumnIndex = cellJamEnd Then

                isTimeMode = True

                rowIndex = e.RowIndex
                colIndex = e.ColumnIndex

                dtp.Format = DateTimePickerFormat.Custom
                dtp.CustomFormat = "HH:mm:ss"
                dtp.ShowUpDown = True

                Dim rect As Rectangle = DataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)

                dtp.Size = rect.Size
                dtp.Location = rect.Location
                dtp.Visible = True

                If DataGridView1.CurrentCell.Value IsNot Nothing Then
                    dtp.Value = Convert.ToDateTime(DataGridView1.CurrentCell.Value)
                Else
                    dtp.Value = Date.Now
                End If

            Else
                dtp.Visible = False
            End If
        End If


    End Sub
    Private Sub dtp_ValueChanged(sender As Object, e As EventArgs)
        Dim currentValue As DateTime = dtp.Value

        ' SET VALUE KE CELL
        If isTimeMode Then
            DataGridView1.Rows(rowIndex).Cells(colIndex).Value = currentValue.ToString("HH:mm:ss")
        Else
            DataGridView1.Rows(rowIndex).Cells(colIndex).Value = currentValue.ToString("yyyy-MM-dd")
        End If

        ' 🔥 VALIDASI START vs END
        Dim tglStart As DateTime
        Dim tglEnd As DateTime

        Dim valStart = DataGridView1.Rows(rowIndex).Cells(cellTanggalStart).Value
        Dim valEnd = DataGridView1.Rows(rowIndex).Cells(cellTanggalEnd).Value

        If DateTime.TryParse(valStart, tglStart) AndAlso DateTime.TryParse(valEnd, tglEnd) Then

            If tglEnd < tglStart Then
                MessageBox.Show("Tanggal End tidak boleh lebih kecil dari Tanggal Start!")

                ' balikin ke start (atau kosongin)
                DataGridView1.Rows(rowIndex).Cells(cellTanggalEnd).Value = tglStart.ToString("yyyy-MM-dd")
            End If

        End If
    End Sub

    Private Sub dtp_CloseUp(sender As Object, e As EventArgs)
        DataGridView1.Rows(rowIndex).Cells(colIndex).Value = dtp.Value.ToString("yyyy-MM-dd")
        dtp.Visible = False
    End Sub

    Private Sub DataGridView1_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating

        If e.ColumnIndex = cellTanggalStart Or
       e.ColumnIndex = cellTanggalEnd Or
       e.ColumnIndex = cellJamStart Or
       e.ColumnIndex = cellJamEnd Then

            Dim temp As DateTime

            If Not DateTime.TryParse(e.FormattedValue.ToString(), temp) Then
                e.Cancel = True
                MessageBox.Show("Format tanggal/jam tidak valid!")
            End If

        End If

    End Sub
    Private Sub N_EMI_SD_Tambah_Production_Schedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load




        DataGridView1.Columns(cellTanggalStart).ReadOnly = True
        DataGridView1.Columns(cellTanggalEnd).ReadOnly = True
        DataGridView1.Columns(cellJamStart).ReadOnly = True
        DataGridView1.Columns(cellJamEnd).ReadOnly = True






        TxtHari.Text = TanggalDariKalender.ToString("dddd") ' Selasa
        TxtTanggal.Text = TanggalDariKalender.ToString("dddd, dd MMM yy", New System.Globalization.CultureInfo("id-ID"))

        ' Ambil Nama Bulan (contoh: April)
        bulan = TanggalDariKalender.ToString("MM")

        ' Ambil Tahun (contoh: 2026)
        tahun = TanggalDariKalender.ToString("yyyy")

        mustUpdate = False

        kosong()
    End Sub

    Private Sub kosong()
        get_data_production_plan_perbulan()
    End Sub


    Private Sub get_no_faktur()
        Txt_NoFaktur.Text = fTransSalesForcasting & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Production_Plan_Schedule", "No_Transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_transaksi, 1, " & Len(fTransSalesForcasting) + 4 & ")", fTransSalesForcasting & Format(tgl_skg, "MMyy"))
    End Sub

    Sub get_data_production_plan_perbulan()

        Try
            OpenConn()

            Dim ada_akses_untuk_ubah_formula As Boolean = False
            Dim ada_akses_jumlah As Boolean = False
            Dim adaAksesRoleButton As Boolean = True

            If CekButtonRole("Production_Plan_Schedule_Formulator") = "T" Then
                adaAksesRoleButton = False
            End If

            OpenConn()

            If adaAksesRoleButton = False Then
                If CekButtonRole("Production_Plan_Schedule_PPIC") = "T" Then
                    adaAksesRoleButton = False
                Else
                    adaAksesRoleButton = True
                End If
            End If

            OpenConn()

            If adaAksesRoleButton = False Then
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            OpenConn()

            DataGridView1.Rows.Clear()

            ' -------------------------------------------------------
            ' Pre-load semua formula ke Dictionary SEBELUM loop
            ' Satu kali query, tidak perlu query ulang per baris
            ' -------------------------------------------------------
            Dim dictFormula As New Dictionary(Of String, List(Of String))


            'SQL = $"

            '    ;with cte as(

            '    select a.Kode_Perusahaan,Kode_Barang, Kode_Barang_Inq, 
            '    isnull((select top(1) x.No_Faktur from N_EMI_Transaksi_Formulator_Binding x where 
            '    x.Kode_Perusahaan=a.Kode_Perusahaan and x.Kode_Barang=a.Kode_Barang_Inq and
            '    x.Status Is NULL And x.Flag_Validasi_Main = 'Y' order by x.Tanggal DESC, x.Jam DESC),'') as no_faktur
            '    from barang a, emi_group_jenis b where
            '    a.kode_perusahaan=b.kode_perusahaan and a.id_group_jenis=b.id_group_jenis and (Flag_Finished_Good='Y' or Flag_semi_fg='Y')
            '    and Kode_Stock_Owner=isnull((select top(1) kode_stock_owner from stock_owner_gudang x ),null)

            '    )

            '    Select a.Kode_Barang_Inq as kode_barang, c.No_Faktur as Kode_Formula From 
            '    cte a inner Join N_EMI_Transaksi_Formulator_Binding_Detail b    
            '    on a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur 
            '    inner Join Emi_Transaksi_Formulator c     
            '    on b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Formulator = c.No_Faktur And c.Status Is null
            '    and c.Flag_Validasi_Formula_Produksi_BOD = 'Y'
            '    where a.Kode_Perusahaan = '{KodePerusahaan}'
            '    order by a.Kode_Barang_Inq


            '    "

            'Using dr2 = OpenTrans(SQL)
            '    Do While dr2.Read
            '        Dim kb As String = dr2("Kode_Barang").ToString()
            '        If Not dictFormula.ContainsKey(kb) Then
            '            dictFormula(kb) = New List(Of String)
            '        End If
            '        dictFormula(kb).Add(dr2("Kode_Formula").ToString())
            '    Loop
            'End Using

            SQL = "select a.No_Faktur,cast(rv as bigint) as rvx,b.kode_stock_owner,b.Kode_Barang,c.Nama,b.Nilai_PPIC as Jumlah, b.Satuan, b.urut , "

            SQL = SQL & "isnull(sched.Jumlah_Terpakai,0) as Jumlah_Terpakai, "
            SQL = SQL & "isnull(sched.qty_produksi,0) as qty_produksi, "
            SQL = SQL & "h.tanggal_start,h.tanggal_end,h.keterangan, "
            SQL = SQL & "isnull(sched.flag_sudah_po,'T') as flag_sudah_po, "

            '  SQL = SQL & "isnull(h.kode_formula, b.kode_formula) as Kode_Formula, "
            SQL = SQL & "e.Id_Jenis_Produk, e.Keterangan as Jenis, c.kode_barang_inq "

            SQL = SQL & "From EMI_Transaksi_Sales_Forecasting a "
            SQL = SQL & "inner join EMI_Transaksi_Sales_Forecasting_Detail b on "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "

            SQL = SQL & "inner join Barang c on "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Stock_Owner = b.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "

            ' =========================
            ' 🔥 GANTI 3 SUBQUERY JADI 1 APPLY
            ' =========================
            SQL = SQL & "OUTER APPLY ( "
            SQL = SQL & "select "
            SQL = SQL & "SUM(x.jumlah) as Jumlah_Terpakai, "
            SQL = SQL & "MAX(CASE WHEN day(x.tanggal_full) = '" & TanggalDariKalender.ToString("dd") & "' THEN x.jumlah END) as qty_produksi, "
            SQL = SQL & "MAX(CASE WHEN day(x.tanggal_full) = '" & TanggalDariKalender.ToString("dd") & "' THEN x.flag_sudah_production_order END) as flag_sudah_po "
            SQL = SQL & "from N_EMI_Production_Plan_Schedule_detail x "
            SQL = SQL & "inner join N_EMI_Production_Plan_Schedule y "
            SQL = SQL & "on x.kode_perusahaan = y.kode_perusahaan and x.no_transaksi = y.no_transaksi "
            SQL = SQL & "where y.status is null "
            SQL = SQL & "and x.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and x.urut_production_plan = b.urut "
            SQL = SQL & ") sched "

            ' =========================
            ' EXISTING APPLY (TETAP)
            ' =========================
            SQL = SQL & "OUTER APPLY ( "
            SQL = SQL & "select x.tanggal_start,x.tanggal_end,x.keterangan,x.kode_formula "
            SQL = SQL & "from N_EMI_Production_Plan_Schedule_Detail x, N_EMI_Production_Plan_Schedule y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Transaksi = y.No_Transaksi "
            SQL = SQL & "and y.Status is null "
            SQL = SQL & "and x.Urut_Production_Plan = b.urut "
            SQL = SQL & "and day(x.tanggal_full) = '" & TanggalDariKalender.ToString("dd") & "' "
            SQL = SQL & ") h "

            SQL = SQL & "inner join EMI_Varian d on "
            SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan and d.Id_Varian = c.Id_Varian "

            SQL = SQL & "inner join EMI_Jenis_Produk e on "
            SQL = SQL & "d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Jenis_Produk = e.Id_Jenis_Produk "

            SQL = SQL & "where b.Flag_Validasi = 'Y' and b.Flag_Validasi_PPIC = 'Y' "
            SQL = SQL & "and b.Bulan= '" & bulan & "' and b.Tahun = '" & tahun & "' "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then


                        DataGridView1.SuspendLayout()
                        For i As Integer = 0 To .Rows.Count - 1

                            DataGridView1.Rows.Add(1)

                            Dim ind As Integer = DataGridView1.Rows.Count - 1

                            DataGridView1.Rows(ind).Cells(CellChkBox).Value = False
                            DataGridView1.Rows(ind).Cells(CellKdBrg).Value = .Rows(i).Item("kode_barang")
                            DataGridView1.Rows(ind).Cells(CellNmBrg).Value = .Rows(i).Item("Nama")



                            DataGridView1.Rows(ind).Cells(cellJmlhPerbulan).Value = Format(.Rows(i).Item("Jumlah"), "N2")
                            DataGridView1.Rows(ind).Cells(cellJumlahTerpakai).Value = Format(.Rows(i).Item("Jumlah_Terpakai"), "N2")
                            DataGridView1.Rows(ind).Cells(cellSatuan).Value = .Rows(i).Item("Satuan")
                            DataGridView1.Rows(ind).Cells(cellJumlah).Value = .Rows(i).Item("qty_produksi")

                            DataGridView1.Rows(ind).Cells(cellNo_Faktur).Value = .Rows(i).Item("no_faktur")
                            DataGridView1.Rows(ind).Cells(cellNo_Urut).Value = .Rows(i).Item("urut")
                            DataGridView1.Rows(ind).Cells(cellStockOwner).Value = .Rows(i).Item("kode_stock_owner")
                            DataGridView1.Rows(ind).Cells(cellRv).Value = .Rows(i).Item("rvx")

                            DataGridView1.Rows(ind).Cells(cellJumlah).Style.BackColor = Color.LightYellow

                            'tanggal start
                            If General_Class.CekNULL(.Rows(i).Item("tanggal_start")) = "" Then
                                DataGridView1.Rows(ind).Cells(cellTanggalStart).Value = Format(TanggalDariKalender.ToString("yyyy-MM-dd"))
                            Else
                                DataGridView1.Rows(ind).Cells(cellTanggalStart).Value = Format(.Rows(i).Item("tanggal_start"), "yyyy-MM-dd")
                            End If

                            'jam start
                            If General_Class.CekNULL(.Rows(i).Item("tanggal_start")) = "" Then
                                DataGridView1.Rows(ind).Cells(cellJamStart).Value = "00:00:00"
                            Else
                                DataGridView1.Rows(ind).Cells(cellJamStart).Value = Format(.Rows(i).Item("tanggal_start"), "HH:mm:ss")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("flag_sudah_po")) = "T" Then

                                dtp = New DateTimePicker()
                                dtp.Format = DateTimePickerFormat.Custom
                                dtp.CustomFormat = "yyyy-MM-dd"
                                dtp.Visible = False
                                AddHandler dtp.TextChanged, AddressOf dtp_TextChanged
                                AddHandler dtp.CloseUp, AddressOf dtp_CloseUp
                                AddHandler dtp.ValueChanged, AddressOf dtp_ValueChanged
                                DataGridView1.Controls.Add(dtp)

                                If ada_akses_untuk_ubah_formula = False Then
                                    DataGridView1.Rows(ind).Cells(cellJumlah).ReadOnly = False
                                Else
                                    DataGridView1.Rows(ind).Cells(cellJumlah).ReadOnly = True
                                End If

                                DataGridView1.Rows(ind).Cells(cellFormula).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(cellTanggalStart).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(cellTanggalEnd).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(cellJamStart).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(cellJamEnd).ReadOnly = False
                                DataGridView1.Rows(ind).Cells(cellKeterangan).ReadOnly = False
                                DataGridView1.Rows(ind).DefaultCellStyle.BackColor = Color.Empty

                                DataGridView1.Rows(ind).Cells(cellJumlah).Style.BackColor = Color.LightGray
                                DataGridView1.Rows(ind).Cells(cellFlagSudahPO).Value = "T"

                            Else
                                DataGridView1.Rows(ind).Cells(cellJumlah).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(cellFormula).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(cellTanggalStart).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(cellTanggalEnd).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(cellJamStart).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(cellJamEnd).ReadOnly = True
                                DataGridView1.Rows(ind).Cells(cellKeterangan).ReadOnly = True
                                DataGridView1.Rows(ind).DefaultCellStyle.BackColor = Color.LightYellow

                                DataGridView1.Rows(ind).Cells(cellFlagSudahPO).Value = .Rows(i).Item("flag_sudah_po")

                            End If

                            'tanggal end
                            If General_Class.CekNULL(.Rows(i).Item("tanggal_end")) = "" Then
                                DataGridView1.Rows(ind).Cells(cellTanggalEnd).Value = Format(TanggalDariKalender.ToString("yyyy-MM-dd"))
                            Else
                                DataGridView1.Rows(ind).Cells(cellTanggalEnd).Value = Format(.Rows(i).Item("tanggal_end"), "yyyy-MM-dd")
                            End If

                            'jam end
                            If General_Class.CekNULL(.Rows(i).Item("tanggal_end")) = "" Then
                                DataGridView1.Rows(ind).Cells(cellJamEnd).Value = "23:59:00"
                            Else
                                DataGridView1.Rows(ind).Cells(cellJamEnd).Value = Format(.Rows(i).Item("tanggal_end"), "HH:mm:ss")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("keterangan")) = "" Then
                                DataGridView1.Rows(ind).Cells(cellKeterangan).Value = ""
                            Else
                                DataGridView1.Rows(ind).Cells(cellKeterangan).Value = .Rows(i).Item("keterangan")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("id_jenis_produk")) = "" Then
                                DataGridView1.Rows(ind).Cells(cellIdJenisProduk).Value = ""
                            Else
                                DataGridView1.Rows(ind).Cells(cellIdJenisProduk).Value = .Rows(i).Item("id_jenis_produk")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("jenis")) = "" Then
                                DataGridView1.Rows(ind).Cells(cellKeteranganJenisProduk).Value = ""
                            Else
                                DataGridView1.Rows(ind).Cells(cellKeteranganJenisProduk).Value = .Rows(i).Item("jenis")
                            End If

                            DataGridView1.Rows(ind).Cells(cellJumlahDB).Value = .Rows(i).Item("qty_produksi")

                        Next
                        DataGridView1.ResumeLayout()
                    End If
                End With
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        End_Loading(Me)

    End Sub

    'Sub get_data_production_plan_perbulan()

    '    Try
    '        OpenConn()

    '        Dim ada_akses_untuk_ubah_formula As Boolean = False

    '        Dim ada_akses_jumlah As Boolean = False

    '        'If fStatus = "formulator" Then
    '        Dim adaAksesRoleButton As Boolean = True

    '        If CekButtonRole("Production_Plan_Schedule_Formulator") = "T" Then
    '            adaAksesRoleButton = False
    '        End If
    '        OpenConn()


    '        If adaAksesRoleButton = False Then
    '            If CekButtonRole("Production_Plan_Schedule_PPIC") = "T" Then
    '                adaAksesRoleButton = False
    '            Else
    '                adaAksesRoleButton = True
    '            End If

    '        End If


    '        OpenConn()


    '        If adaAksesRoleButton = False Then

    '            CloseConn()
    '            MessageBox.Show("Anda tidak memiliki akses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Exit Sub
    '        End If


    '        'If CekButtonRole("Production_Plan_Schedule_Formulator") = "Y" Then
    '        '    ada_akses_untuk_ubah_formula = True

    '        '    DataGridView1.Columns(cellFormula).Visible = True
    '        '    DataGridView1.Columns(cellJumlah).ReadOnly = True


    '        '    DataGridView1.Columns(cellTanggalStart).Visible = False
    '        '    DataGridView1.Columns(cellJamStart).Visible = False

    '        '    DataGridView1.Columns(cellTanggalEnd).Visible = False
    '        '    DataGridView1.Columns(cellJamEnd).Visible = False
    '        'Else
    '        '    ada_akses_untuk_ubah_formula = False
    '        '    DataGridView1.Columns(cellFormula).Visible = False
    '        '    DataGridView1.Columns(cellTanggalStart).Visible = True
    '        '    DataGridView1.Columns(cellJamStart).Visible = True

    '        '    DataGridView1.Columns(cellTanggalEnd).Visible = True
    '        '    DataGridView1.Columns(cellJamEnd).Visible = True


    '        '    DataGridView1.Columns(cellJumlah).ReadOnly = False
    '        'End If




    '        OpenConn()

    '        DataGridView1.Rows.Clear()


    '        Dim dictFormula As New Dictionary(Of String, String)




    '        SQL = "select a.No_Faktur,cast(rv as bigint) as rvx,b.kode_stock_owner,b.Kode_Barang,c.Nama,b.Nilai_PPIC as Jumlah, b.Satuan, b.urut , "
    '        SQL = SQL & "isnull((select sum(x.jumlah) From N_EMI_Production_Plan_Schedule_detail x ,N_EMI_Production_Plan_Schedule y "
    '        SQL = SQL & "where x.kode_perusahaan = y.kode_perusahaan and x.no_transaksi = y.no_transaksi and y.status is null "
    '        SQL = SQL & "and x.kode_perusahaan = b.kode_perusahaan and x.urut_production_plan = b.urut "
    '        SQL = SQL & "),0) as Jumlah_Terpakai, "

    '        SQL = SQL & "isnull((select x.jumlah From N_EMI_Production_Plan_Schedule_detail x ,N_EMI_Production_Plan_Schedule y "
    '        SQL = SQL & "where x.kode_perusahaan = y.kode_perusahaan and x.no_transaksi = y.no_transaksi and y.status is null "
    '        SQL = SQL & "and x.kode_perusahaan = b.kode_perusahaan and x.urut_production_plan = b.urut "
    '        SQL = SQL & "and x.tanggal = '" & TanggalDariKalender.ToString("dd") & "' "
    '        SQL = SQL & "),0) as qty_produksi,h.tanggal_start,h.tanggal_end,h.keterangan, "

    '        SQL = SQL & "isnull((select x.flag_sudah_production_order From N_EMI_Production_Plan_Schedule_detail x ,N_EMI_Production_Plan_Schedule y "
    '        SQL = SQL & "where x.kode_perusahaan = y.kode_perusahaan and x.no_transaksi = y.no_transaksi and y.status is null "
    '        SQL = SQL & "and x.kode_perusahaan = b.kode_perusahaan and x.urut_production_plan = b.urut "
    '        SQL = SQL & "and x.tanggal = '" & TanggalDariKalender.ToString("dd") & "' "
    '        SQL = SQL & "),'T') as flag_sudah_po ,  "


    '        SQL = SQL & "isnull(h.kode_formula, b.kode_formula) as Kode_Formula, "
    '        SQL = SQL & " e.Id_Jenis_Produk, e.Keterangan as Jenis, c.kode_barang_inq "

    '        SQL = SQL & "From  EMI_Transaksi_Sales_Forecasting a "
    '        SQL = SQL & "inner join EMI_Transaksi_Sales_Forecasting_Detail b on "
    '        SQL = SQL & " a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
    '        SQL = SQL & "inner join Barang c on "
    '        SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Stock_Owner = b.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "


    '        SQL = SQL & "OUTER APPLY ( "
    '        SQL = SQL & "select x.tanggal_start,x.tanggal_end,x.keterangan,x.kode_formula from N_EMI_Production_Plan_Schedule_Detail x, N_EMI_Production_Plan_Schedule y "
    '        SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Transaksi = y.No_Transaksi  "
    '        SQL = SQL & "and y.Status is null and x.Urut_Production_Plan = b.urut and x.tanggal = '" & TanggalDariKalender.ToString("dd") & "' ) h "

    '        SQL = SQL & "inner join EMI_Varian d on "
    '        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan and d.Id_Varian = c.Id_Varian "
    '        SQL = SQL & "inner join EMI_Jenis_Produk e on "
    '        SQL = SQL & "d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Jenis_Produk = e.Id_Jenis_Produk "

    '        SQL = SQL & "where b.Flag_Validasi = 'Y' and b.Flag_Validasi_PPIC = 'Y'  "
    '        SQL = SQL & "and b.Bulan=  '" & bulan & "' and b.Tahun  = '" & tahun & "'  "


    '        Using Ds = BindingTrans(SQL)
    '            With Ds.Tables("MyTable")
    '                If .Rows.Count <> 0 Then
    '                    For i As Integer = 0 To .Rows.Count - 1

    '                        DataGridView1.Rows.Add(1)




    '                        Dim ind As Integer = DataGridView1.Rows.Count - 1
    '                        DataGridView1.Rows(ind).Cells(CellChkBox).Value = False
    '                        DataGridView1.Rows(ind).Cells(CellKdBrg).Value = .Rows(i).Item("kode_barang")
    '                        DataGridView1.Rows(ind).Cells(CellNmBrg).Value = .Rows(i).Item("Nama")

    '                        Dim dgvCmbValueSwitch As DataGridViewComboBoxCell
    '                        dgvCmbValueSwitch = DataGridView1.Rows(i).Cells(cellFormula)
    '                        dgvCmbValueSwitch.Items.Clear()

    '                        'Dim indexValue As Integer = 0
    '                        Dim inddd As Integer = 0
    '                        Dim kode_formula As String = ""
    '                        OpenConn()


    '                        SQL = $"Select   c.No_Faktur as Kode_Formula
    '                                    From N_EMI_Transaksi_Formulator_Binding a 
    '						inner Join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur 
    '                                    inner Join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Formulator = c.No_Faktur And c.Status Is null 
    '                                    where a.Status Is NULL And a.Flag_Validasi_Main = 'Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Barang ='" & .Rows(i).Item("kode_barang_inq") & "'
    '                                    order by a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC
    '                                    "
    '                        Using dr2 = OpenTrans(SQL)
    '                            Do While dr2.Read
    '                                dgvCmbValueSwitch.Items.Add(dr2("Kode_formula"))

    '                                inddd += 1
    '                            Loop
    '                        End Using

    '                        Dim valFormula As String = .Rows(i).Item("Kode_Formula").ToString()

    '                        OpenConn()

    '                        If dgvCmbValueSwitch.Items.Contains(valFormula) Then
    '                            dgvCmbValueSwitch.Value = valFormula
    '                        ElseIf dgvCmbValueSwitch.Items.Count > 0 Then
    '                            dgvCmbValueSwitch.Value = dgvCmbValueSwitch.Items(0)
    '                        End If

    '                        Dim key As String = .Rows(i).Item("no_faktur").ToString() & "|" & .Rows(i).Item("urut").ToString()

    '                        If dgvCmbValueSwitch.Value IsNot Nothing Then
    '                            dictFormula(key) = dgvCmbValueSwitch.Value.ToString()
    '                        End If

    '                        If ada_akses_untuk_ubah_formula Then
    '                            dgvCmbValueSwitch.ReadOnly = False
    '                        Else
    '                            dgvCmbValueSwitch.ReadOnly = True
    '                        End If


    '                        DataGridView1.Rows(ind).Cells(cellJmlhPerbulan).Value = Format(.Rows(i).Item("Jumlah"), "N2")
    '                        DataGridView1.Rows(ind).Cells(cellJumlahTerpakai).Value = Format(.Rows(i).Item("Jumlah_Terpakai"), "N2")
    '                        DataGridView1.Rows(ind).Cells(cellSatuan).Value = .Rows(i).Item("Satuan")
    '                        DataGridView1.Rows(ind).Cells(cellJumlah).Value = .Rows(i).Item("qty_produksi")



    '                        DataGridView1.Rows(ind).Cells(cellNo_Faktur).Value = .Rows(i).Item("no_faktur")
    '                        DataGridView1.Rows(ind).Cells(cellNo_Urut).Value = .Rows(i).Item("urut")
    '                        DataGridView1.Rows(ind).Cells(cellStockOwner).Value = .Rows(i).Item("kode_stock_owner")
    '                        DataGridView1.Rows(ind).Cells(cellRv).Value = .Rows(i).Item("rvx")


    '                        DataGridView1.Rows(ind).Cells(cellJumlah).Style.BackColor = Color.LightYellow

    '                        'tanggal start
    '                        If General_Class.CekNULL(.Rows(i).Item("tanggal_start")) = "" Then

    '                            DataGridView1.Rows(i).Cells(cellTanggalStart).Value = Format(TanggalDariKalender.ToString("yyyy-MM-dd"))
    '                        Else

    '                            DataGridView1.Rows(i).Cells(cellTanggalStart).Value = Format(.Rows(i).Item("tanggal_start"), "yyyy-MM-dd")
    '                        End If

    '                        'jam start
    '                        If General_Class.CekNULL(.Rows(i).Item("tanggal_start")) = "" Then

    '                            DataGridView1.Rows(i).Cells(cellJamStart).Value = "00:00:00"
    '                        Else

    '                            DataGridView1.Rows(i).Cells(cellJamStart).Value = Format(.Rows(i).Item("tanggal_start"), "HH:mm:ss")
    '                        End If

    '                        If General_Class.CekNULL(.Rows(i).Item("flag_sudah_po")) = "T" Then

    '                            dtp = New DateTimePicker()
    '                            dtp.Format = DateTimePickerFormat.Custom
    '                            dtp.CustomFormat = "yyyy-MM-dd"
    '                            dtp.Visible = False
    '                            AddHandler dtp.TextChanged, AddressOf dtp_TextChanged
    '                            AddHandler dtp.CloseUp, AddressOf dtp_CloseUp
    '                            AddHandler dtp.ValueChanged, AddressOf dtp_ValueChanged
    '                            DataGridView1.Controls.Add(dtp)

    '                            If ada_akses_untuk_ubah_formula = False Then
    '                                DataGridView1.Rows(ind).Cells(cellJumlah).ReadOnly = False
    '                            Else
    '                                DataGridView1.Rows(ind).Cells(cellJumlah).ReadOnly = True
    '                            End If

    '                            DataGridView1.Rows(ind).Cells(cellFormula).ReadOnly = False
    '                            DataGridView1.Rows(ind).Cells(cellTanggalStart).ReadOnly = False
    '                            DataGridView1.Rows(ind).Cells(cellTanggalEnd).ReadOnly = False
    '                            DataGridView1.Rows(ind).Cells(cellJamStart).ReadOnly = False
    '                            DataGridView1.Rows(ind).Cells(cellJamEnd).ReadOnly = False
    '                            DataGridView1.Rows(ind).Cells(cellKeterangan).ReadOnly = False
    '                            DataGridView1.Rows(ind).DefaultCellStyle.BackColor = Color.Empty


    '                            DataGridView1.Rows(ind).Cells(cellJumlah).Style.BackColor = Color.LightGray

    '                            DataGridView1.Rows(ind).Cells(cellFlagSudahPO).Value = "T"

    '                        Else
    '                            DataGridView1.Rows(ind).Cells(cellJumlah).ReadOnly = True

    '                            DataGridView1.Rows(ind).Cells(cellFormula).ReadOnly = True
    '                            DataGridView1.Rows(ind).Cells(cellTanggalStart).ReadOnly = True
    '                            DataGridView1.Rows(ind).Cells(cellTanggalEnd).ReadOnly = True
    '                            DataGridView1.Rows(ind).Cells(cellJamStart).ReadOnly = True
    '                            DataGridView1.Rows(ind).Cells(cellJamEnd).ReadOnly = True
    '                            DataGridView1.Rows(ind).Cells(cellKeterangan).ReadOnly = True
    '                            DataGridView1.Rows(ind).DefaultCellStyle.BackColor = Color.LightYellow

    '                            DataGridView1.Rows(ind).Cells(cellFlagSudahPO).Value = .Rows(i).Item("flag_sudah_po")

    '                        End If



    '                        'tanggal end
    '                        If General_Class.CekNULL(.Rows(i).Item("tanggal_end")) = "" Then

    '                            DataGridView1.Rows(i).Cells(cellTanggalEnd).Value = Format(TanggalDariKalender.ToString("yyyy-MM-dd"))
    '                        Else

    '                            DataGridView1.Rows(i).Cells(cellTanggalEnd).Value = Format(.Rows(i).Item("tanggal_end"), "yyyy-MM-dd")
    '                        End If

    '                        'tanggal end
    '                        If General_Class.CekNULL(.Rows(i).Item("tanggal_end")) = "" Then

    '                            DataGridView1.Rows(i).Cells(cellJamEnd).Value = "23:59:00"
    '                        Else

    '                            DataGridView1.Rows(i).Cells(cellJamEnd).Value = Format(.Rows(i).Item("tanggal_end"), "HH:mm:ss")
    '                        End If

    '                        If General_Class.CekNULL(.Rows(i).Item("keterangan")) = "" Then

    '                            DataGridView1.Rows(i).Cells(cellKeterangan).Value = ""
    '                        Else

    '                            DataGridView1.Rows(i).Cells(cellKeterangan).Value = .Rows(i).Item("keterangan")
    '                        End If

    '                        If General_Class.CekNULL(.Rows(i).Item("id_jenis_produk")) = "" Then

    '                            DataGridView1.Rows(ind).Cells(cellIdJenisProduk).Value = ""
    '                        Else

    '                            DataGridView1.Rows(ind).Cells(cellIdJenisProduk).Value = .Rows(i).Item("id_jenis_produk")
    '                        End If

    '                        If General_Class.CekNULL(.Rows(i).Item("jenis")) = "" Then

    '                            DataGridView1.Rows(ind).Cells(cellKeteranganJenisProduk).Value = ""
    '                        Else

    '                            DataGridView1.Rows(ind).Cells(cellKeteranganJenisProduk).Value = .Rows(i).Item("jenis")
    '                        End If






    '                    Next
    '                End If
    '            End With
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try

    'End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Get_Isi_Listview(DataGridView1.CurrentRow.Index)

        If IsNumeric(LvJumlah) = False Or Val(LvJumlah) < 0 Then
            DataGridView1.CurrentRow.Cells(cellJumlah).Value = 0
        End If

    End Sub


    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        get_jam()

        Try
            OpenConn()

            'If fStatus = "formulator" Then
            Dim adaAksesRoleButton As Boolean = True

            If CekButtonRole("Production_Plan_Schedule_Formulator") = "T" Then
                adaAksesRoleButton = False
            End If
            OpenConn()


            If adaAksesRoleButton = False Then
                If CekButtonRole("Production_Plan_Schedule_PPIC") = "T" Then
                    adaAksesRoleButton = False
                Else
                    adaAksesRoleButton = True
                End If

            End If


            OpenConn()


            If adaAksesRoleButton = False Then

                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()



            Dim daftarBarangOverQuota As String = ""
            Dim daftarBarangValid As String = ""
            Dim daftarMelebihiPO As String = ""

            Dim adaMasalah As Boolean = False
            Dim adaMasalahMelebihiPO As Boolean = False

            Dim sudahAdaInduk As Boolean = False

            Dim no_faktur_induk_yang_sudah_Ada As String = ""
            Dim no_urut_sudah_ada As String = ""
            SQL = "select a.no_transaksi,b.no_urut,a.bulan,b.jumlah, a.tahun From N_EMI_Production_Plan_Schedule a, N_EMI_Production_Plan_Schedule_detail b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.no_transaksi = b.no_transaksi "
            SQL = SQL & "and a.status is null and a.kode_perusahaan = '" & KodePerusahaan & "'  "

            'SQL = SQL & "and  b.tanggal_full = '" & TanggalDariKalender.ToString("yyyy-MM-dd") & "' "
            SQL = SQL & "and a.bulan = '" & bulan & "' and a.tahun = '" & tahun & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    sudahAdaInduk = True
                    no_faktur_induk_yang_sudah_Ada = Dr("no_transaksi")
                    no_urut_sudah_ada = Dr("no_urut")
                Else
                    Dr.Close()
                    sudahAdaInduk = False
                    no_faktur_induk_yang_sudah_Ada = Txt_NoFaktur.Text
                    no_urut_sudah_ada = "0"
                End If
            End Using

            Dim kode_Formula_setting As String = ""
            Dim no_faktur_setting As String = ""

            SQL = $"

             select no_transaksi,No_Formula From N_EMI_Transaksi_Schedule_Formulator
                            where Kode_Perusahaan = '{KodePerusahaan}' and '{TanggalDariKalender.ToString("yyyy-MM-dd")}' between Tanggal_Awal and Tanggal_Akhir
                            and Status is null
                "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Do
                        If General_Class.CekNULL(Dr("no_formula")) = "" Then
                            kode_Formula_setting = "NULL"
                        Else
                            kode_Formula_setting = "'" & Dr("no_formula") & "'"
                        End If

                        If General_Class.CekNULL(Dr("no_transaksi")) = "" Then
                            no_faktur_setting = "NULL"
                        Else
                            no_faktur_setting = "'" & Dr("no_transaksi") & "'"
                        End If
                    Loop While Dr.Read
                Else
                    Dr.Close()
                    kode_Formula_setting = "NULL"
                    no_faktur_setting = "NULL"
                End If
            End Using




            If no_faktur_induk_yang_sudah_Ada.Trim.Length = 0 Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Terjadi kesalahan, no transaksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            If sudahAdaInduk = False Then
                SQL = "insert into N_EMI_Production_Plan_Schedule (Kode_Perusahaan,	No_Transaksi,Tanggal,Jam,User_Id,Tahun,Bulan) values ("
                SQL = SQL & "'" & KodePerusahaan & "' , '" & Txt_NoFaktur.Text.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "','" & tahun & "', '" & bulan & "' ) "
                ExecuteTrans(SQL)
            End If



            For indexGridView As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(indexGridView)

                '      MessageBox.Show(lvNmBrg & " - " & LvJumlah & " - " & LvJmlhDB)

                If LvSdhPO = "Y" Then
                    Continue For
                End If



                If LvJumlah = 0 And LvJmlhDB = 0 Then
                    Continue For
                End If

                If LvJumlah < 0 Then
                    Continue For
                End If

                If LvJumlah = LvJmlhDB Then
                    Continue For
                End If


                If IsNumeric(LvJumlah) = False Or LvJumlah < 0 Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah tidak valid untuk barang : " & lvNmBrg, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'cek rv 1
                SQL = "select cast(rv as bigint) as rvx, urut "
                SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and  urut = '" & LvNo_Urut & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If dr("rvx") <> LvRv Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data ini sudah diubah sebelumnya! silahkan refresh dan coba lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If
                End Using


                Dim cekSudahPernahInput As Boolean = False

                SQL = "select a.no_transaksi,b.no_urut,a.bulan,b.jumlah, a.tahun From N_EMI_Production_Plan_Schedule a, N_EMI_Production_Plan_Schedule_detail b "
                SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.no_transaksi = b.no_transaksi "
                SQL = SQL & "and a.status is null and a.kode_perusahaan = '" & KodePerusahaan & "' and urut_production_plan = " & LvNo_Urut & " "
                SQL = SQL & "and  b.tanggal = '" & TanggalDariKalender.ToString("dd") & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            For indexBaru As Integer = 0 To .Rows.Count - 1
                                cekSudahPernahInput = True


                                If (Val(HilangkanTanda(lvJmlhTerpakai)) + Val(HilangkanTanda(LvJumlah)) - Val(.Rows(indexBaru).Item("jumlah"))) > Val(HilangkanTanda(lvJmlhPerbulan)) Then
                                    adaMasalah = True
                                    ' Catat detail barangnya ke string
                                    daftarBarangOverQuota &= $"- {lvNmBrg} (Input: {LvJumlah}, Sisa Kuota: {lvJmlhPerbulan - lvJmlhTerpakai})" & vbCrLf
                                End If



                                SQL = "update N_EMI_Production_Plan_Schedule_detail set "
                                SQL = SQL & "kode_formula = " & kode_Formula_setting & ", "
                                SQL = SQL & "No_Schedule_Formulator = " & no_faktur_setting & ", "
                                SQL = SQL & "jumlah =  " & LvJumlah & " ,"
                                SQL = SQL & "tanggal_start = '" & LvTanggalStart & " " & LvJamStart & "  ', "

                                SQL = SQL & "tanggal_end = '" & LvTanggalEnd & " " & LvJamEnd & "', "

                                SQL = SQL & "keterangan = '" & LvKeterangan & "' "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and no_urut = '" & .Rows(indexBaru).Item("no_urut") & "' "
                                ExecuteTrans(SQL)




                                SQL = "INSERT INTO N_EMI_Production_Plan_Schedule_log ("
                                SQL = SQL & "Kode_Perusahaan, No_Transaksi, User_Id, Tanggal, Tanggal_Perhari, "
                                SQL = SQL & "Bulan, Tahun, Kode_Formula,No_Schedule_Formulator, Jumlah, Satuan, No_Urut_Detail, jam, kode_Stock_owner,kode_barang ,"
                                SQL = SQL & "tanggal_start, tanggal_end, keterangan"
                                SQL = SQL & ") VALUES ("


                                SQL = SQL & "'" & KodePerusahaan & "', "
                                SQL = SQL & "'" & .Rows(indexBaru).Item("no_transaksi") & "', "
                                SQL = SQL & "'" & UserID & "', "
                                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                                SQL = SQL & "'" & TanggalDariKalender.ToString("dd") & "', "


                                SQL = SQL & "'" & TanggalDariKalender.ToString("MM") & "', "
                                SQL = SQL & "'" & TanggalDariKalender.ToString("yyyy") & "', "


                                SQL = SQL & "" & kode_Formula_setting & ", "
                                SQL = SQL & "" & no_faktur_setting & ", "
                                SQL = SQL & " " & HilangkanTanda(LvJumlah) & ", "


                                SQL = SQL & "'" & LvSatuan & "', "

                                SQL = SQL & "'" & .Rows(indexBaru).Item("No_urut") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
                                SQL = SQL & ", '" & LvStockOwner & "', '" & lvKdBrg & "' ,"
                                SQL = SQL & "'" & LvTanggalStart & " " & LvJamStart & "' , '" & LvTanggalEnd & " " & LvJamEnd & "' , "
                                SQL = SQL & "'" & LvKeterangan & "' "

                                SQL = SQL & ")"
                                ExecuteTrans(SQL)
                            Next

                        Else

                            If Val(HilangkanTanda(LvJumlah)) = 0 Then
                                Continue For
                            End If

                            cekSudahPernahInput = False
                        End If
                    End With
                End Using




                If cekSudahPernahInput = False Then
                    Dim x_no_urut_det As Integer



                    If Val(HilangkanTanda(lvJmlhTerpakai)) + Val(HilangkanTanda(LvJumlah)) > Val(HilangkanTanda(lvJmlhPerbulan)) Then
                        adaMasalah = True
                        ' Catat detail barangnya ke string
                        daftarBarangOverQuota &= $"- {lvNmBrg} (Input: {LvJumlah}, Sisa Kuota: {lvJmlhPerbulan - lvJmlhTerpakai})" & vbCrLf
                    End If

                    SQL = "insert into N_EMI_Production_Plan_Schedule_detail (Kode_Perusahaan,No_Transaksi,Tanggal_Full,Tanggal,Kode_Formula,No_Schedule_Formulator,Jumlah "
                    SQL = SQL & ",Satuan,Urut_Production_Plan, kode_barang,kode_stock_owner, tanggal_start,tanggal_end,keterangan) values ( "
                    SQL = SQL & "'" & KodePerusahaan & "' , '" & no_faktur_induk_yang_sudah_Ada & "', '" & TanggalDariKalender.ToString("yyyy-MM-dd") & "',"
                    SQL = SQL & "'" & TanggalDariKalender.ToString("dd") & "'," & kode_Formula_setting & "," & no_faktur_setting & ",'" & HilangkanTanda(LvJumlah) & "', '" & LvSatuan & "','" & LvNo_Urut & "', "
                    SQL = SQL & "'" & lvKdBrg & "' , '" & LvStockOwner & "','" & LvTanggalStart & " " & LvJamStart & "','" & LvTanggalEnd & " " & LvJamEnd & "' , '" & LvKeterangan & "' "
                    SQL = SQL & " )"
                    ExecuteTrans(SQL)

                    SQL = "select IDENT_CURRENT('N_EMI_Production_Plan_Schedule_detail') as urutan"
                    Using Dr2 = OpenTrans(SQL)
                        If Dr2.Read Then
                            x_no_urut_det = "" & Dr2("urutan") & ""
                        End If
                    End Using


                    'SQL = "update EMI_Transaksi_Sales_Forecasting_Detail set kode_formula = '" & lvFormula & "' "
                    'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and urut = '" & LvNo_Urut & "' "
                    'ExecuteTrans(SQL)




                    SQL = "INSERT INTO N_EMI_Production_Plan_Schedule_log ("
                    SQL = SQL & "Kode_Perusahaan, No_Transaksi, User_Id, Tanggal, Tanggal_Perhari, "
                    SQL = SQL & "Bulan, Tahun, Kode_Formula,No_Schedule_Formulator, Jumlah, Satuan, No_Urut_Detail, jam, kode_Stock_owner,kode_barang, "
                    SQL = SQL & "tanggal_start, tanggal_end, keterangan"
                    SQL = SQL & ") VALUES ("

                    ' Baris 2: Isi Datanya (Gunakan HilangkanTanda untuk angka)
                    SQL = SQL & "'" & KodePerusahaan & "', "
                    SQL = SQL & "'" & Txt_NoFaktur.Text & "', "
                    SQL = SQL & "'" & UserID & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & TanggalDariKalender.ToString("dd") & "', "

                    ' Baris 3: Bulan dan Tahun
                    SQL = SQL & "'" & TanggalDariKalender.ToString("MM") & "', "
                    SQL = SQL & "'" & TanggalDariKalender.ToString("yyyy") & "', "

                    ' Baris 4: Formula dan Jumlah (Angka jangan pakai petik tunggal)
                    SQL = SQL & "" & kode_Formula_setting & ", "
                    SQL = SQL & "" & no_faktur_setting & ", "
                    SQL = SQL & " " & HilangkanTanda(LvJumlah) & ", "

                    ' Baris 5: Sisanya
                    SQL = SQL & "'" & LvSatuan & "', "

                    SQL = SQL & "'" & x_no_urut_det & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
                    SQL = SQL & ", '" & LvStockOwner & "', '" & lvKdBrg & "' ,"
                    SQL = SQL & "'" & LvTanggalStart & " " & LvJamStart & "' , '" & LvTanggalEnd & " " & LvJamEnd & "' , "
                    SQL = SQL & "'" & LvKeterangan & "' "

                    SQL = SQL & ")"
                    ExecuteTrans(SQL)

                End If



                'cek jumlah po

                Dim urutDetSchedule As String = ""
                If cekSudahPernahInput Then

                    SQL = "select b.no_urut From N_EMI_Production_Plan_Schedule a, N_EMI_Production_Plan_Schedule_detail b "
                    SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.no_transaksi = b.no_transaksi "
                    SQL = SQL & "and a.status is null and a.kode_perusahaan = '" & KodePerusahaan & "' and urut_production_plan = " & LvNo_Urut & " "
                    SQL = SQL & "and  b.tanggal = '" & TanggalDariKalender.ToString("dd") & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            urutDetSchedule = dr("no_Urut")
                        Else
                            dr.Close()
                            urutDetSchedule = "0"
                        End If
                    End Using
                End If


                SQL = "select isnull(sum(a.jumlah),0) as Jumlah_Sdh_PO From emi_order_produksi a "
                SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and  a.Urut_Production_Schedule = '" & urutDetSchedule & "' "
                SQL = SQL & "and a.status is null "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Val(HilangkanTanda(LvJumlah)) < Dr("Jumlah_Sdh_PO") Then
                            adaMasalahMelebihiPO = True
                            daftarMelebihiPO &= $"- {lvNmBrg} (Gagal, Jumlah Sudah PO : {Dr("Jumlah_Sdh_PO")} , Jumlah Input : {LvJumlah})" & vbCrLf

                        End If
                    End If
                End Using


                '   daftarBarangValid &= "- " & lvNmBrg & " (Qty: " & LvJumlah & ")" & vbCrLf



            Next

            If adaMasalah Then

                CloseTrans()
                CloseConn()

                Dim pesanError As String = "Data TIDAK DISIMPAN karena item berikut melebihi kuota bulanan:" & vbCrLf & vbCrLf & daftarBarangOverQuota
                MessageBox.Show(pesanError, Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)

                Exit Sub ' 
            End If

            If adaMasalahMelebihiPO Then
                CloseTrans()
                CloseConn()

                Dim pesanError As String = "Data TIDAK DISIMPAN karena jumlah perbulan tidak boleh lebih kecil dari jumlah yang sudah di buat po " & vbCrLf & vbCrLf & daftarMelebihiPO
                MessageBox.Show(pesanError, Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)

                Exit Sub
            End If






            Cmd.Transaction.Commit()
            CloseConn()
            kosong()
            MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            mustUpdate = True
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub

    Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then


            If e.ColumnIndex = cellJmlhPerbulan Then


                DataGridView1.CurrentCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)


                cmsOpsi.Show(Cursor.Position)

            End If
        End If
    End Sub

    Private Sub EditJumlahToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditJumlahToolStripMenuItem.Click
        ' Ambil baris yang tadi diklik kanan
        Dim barisNya As Integer = DataGridView1.CurrentCell.RowIndex

        Get_Isi_Listview(barisNya)


        Try
            OpenConn()
            ' Ambil baris yang tadi diklik kanan


            Dim ada_akses_untuk_ubah_Jumlah As Boolean = False

            If CekButtonRole("Production_Plan_Schedule_PPIC") = "T" Then
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses untuk edit!!")
                Exit Sub
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        N_EMI_SD_Edit_Jumlah_Production_Plan.kodeBarang = lvKdBrg
        N_EMI_SD_Edit_Jumlah_Production_Plan.namaBarang = lvNmBrg
        N_EMI_SD_Edit_Jumlah_Production_Plan.jumlahSekarang = lvJmlhPerbulan
        N_EMI_SD_Edit_Jumlah_Production_Plan.bulanTahunSkrng = TanggalDariKalender.ToString("MMMM") & " - " & tahun
        N_EMI_SD_Edit_Jumlah_Production_Plan.no_urut = LvNo_Urut
        N_EMI_SD_Edit_Jumlah_Production_Plan.noRv = LvRv
        N_EMI_SD_Edit_Jumlah_Production_Plan.jumlahSudahTerpakai = lvJmlhTerpakai
        N_EMI_SD_Edit_Jumlah_Production_Plan.ShowDialog()

        If N_EMI_SD_Edit_Jumlah_Production_Plan.mustUpdate Then
            get_data_production_plan_perbulan()
        End If


    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Hourglass

Public Class N_EMI_Production_Schedule
    Dim arrId_Routing, arrId_JenisProduk, arrKodeBarang As New ArrayList
    Dim Jenis = "Transaksi_Produksi"
    Dim Category As String = ""

    Dim listTglLibur As New List(Of Integer)

    ' Letakkan di bagian atas class (General Declarations)
    Private bulanLoadAwal As String = ""
    Private tahunLoadAwal As String = ""


    Dim LvCheck As String
    Dim LvKdBrg As String
    Dim LvNama As String
    Dim LvJmlhPerbulan As String
    Dim LvJmlhTerpakai As String
    Dim LvWarna As String


    Dim cellCheck As Integer = 0
    Dim cellKdBrg As Integer = 1
    Dim cellNama As Integer = 2
    Dim cellJmlhPerbulan As Integer = 3
    Dim cellTerpakai As Integer = 4
    Dim cellWarna As Integer = 5

    Public awalLoad As Boolean = False

    Dim bulanNow As String
    Dim tahunNow As String

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
        Load_data()
    End Sub

    Private Sub get_isi_listview(index)
        LvCheck = DataGridView1.Rows(index).Cells(cellCheck).Value
        LvKdBrg = DataGridView1.Rows(index).Cells(cellKdBrg).Value
        LvNama = DataGridView1.Rows(index).Cells(cellNama).Value
        LvJmlhPerbulan = DataGridView1.Rows(index).Cells(cellJmlhPerbulan).Value
        LvJmlhTerpakai = DataGridView1.Rows(index).Cells(cellTerpakai).Value
        LvWarna = DataGridView1.Rows(index).Cells(cellWarna).Value
    End Sub

    'Private Sub get_isi_listview2(index)
    '    Lv2NoRencana = DgvSimulasi_DataHPP.Rows(index).Cells(Cell2NoRencana).Value
    '    Lv2Ket = DgvSimulasi_DataHPP.Rows(index).Cells(Cell2Ket).Value
    'End Sub

    Private Sub HideLine(ByVal nama_object As Object)
        PnlDay.Visible = False
        PnlMonth.Visible = False
        PnlWeek.Visible = False
        nama_object.Visible = True
    End Sub

    Private Sub HideLabel(ByVal nama_object As Object)
        LblDay.ForeColor = Color.Gray
        LblMonth.ForeColor = Color.Gray
        LblWeek.ForeColor = Color.Gray

        nama_object.ForeColor = Color.Black
    End Sub
    Private Sub kosong()

        Try
            OpenConn()
            Dim listFilter As New List(Of String)

            listFilter.Add("(a.bulan = " & bulanNow & " AND a.tahun = " & tahunNow & ")")
            Dim filterBulanTahun As String = String.Join(" OR ", listFilter)

            '1
            RefreshForecastingSemiFG(
                    KodePerusahaan,
                    filterBulanTahun,
                    UserID,
                "Schedule"
            )



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        awalLoad = True
        Load_Data_Forecast()

        Init.Start()
    End Sub

    Public Sub Data_Produksi()

        Try
            OpenConn()



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub cekTanggalLibur()
        Try
            OpenConn()


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        awalLoad = True

        Me.Dock = DockStyle.Fill

        get_jam()

        ' 1. Bersihkan semua data lama
        hg.RemoveAll()

        bulanNow = Format(MonthCalendar1.SelectionStart, Format("MM"))
        Dim bulanNow2 As String = Format(MonthCalendar1.SelectionStart, Format("MMMM"))
        tahunNow = Format(MonthCalendar1.SelectionStart, Format("yyyy"))


        bulanLoadAwal = bulanNow
        tahunLoadAwal = tahunNow


        hg.Options.StartDate = MonthCalendar1.SelectionStart
        HideLine(PnlMonth)
        HideLabel(PnlMonth)
        hg.Options.ViewType = Hourglass.ViewTypes.Month
        ' hg.Render()






        ' hg.Render()

        kosong()
        Init.Start()
    End Sub

    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs)


    End Sub

    Sub Load_Data_Forecast()
        Try
            OpenConn()

            DataGridView1.Rows.Clear() : arrKodeBarang.Clear()
            Dim index As Integer = 0
            SQL = "SELECT a.Kode_Perusahaan,c.nama  , b.Kode_Barang, b.Nilai_PPIC AS Jumlah, b.Bulan, b.Tahun, "

            SQL = SQL & "ISNULL( "
            SQL = SQL & "    ( "
            SQL = SQL & "        SELECT SUM(x.jumlah) "
            SQL = SQL & "        FROM N_EMI_Production_Plan_Schedule_detail x, N_EMI_Production_Plan_Schedule y "
            SQL = SQL & "        WHERE x.kode_perusahaan = y.kode_perusahaan "
            SQL = SQL & "          AND x.no_transaksi = y.no_transaksi "
            SQL = SQL & "          AND y.status IS NULL "
            SQL = SQL & "          AND x.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "          AND x.urut_production_plan = b.urut "
            SQL = SQL & "    ),0 "
            SQL = SQL & ") AS Jumlah_Terpakai, "

            SQL = SQL & "d.barColor, d.backColor,d.nama_inisial "

            SQL = SQL & "FROM EMI_Transaksi_Sales_Forecasting a "

            SQL = SQL & "INNER JOIN EMI_Transaksi_Sales_Forecasting_Detail b "
            SQL = SQL & "ON a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "AND a.No_Faktur = b.No_Faktur "

            SQL = SQL & "INNER JOIN barang c "
            SQL = SQL & "ON b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "AND b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "AND b.Kode_Stock_Owner = c.Kode_Stock_Owner "

            SQL = SQL & "INNER JOIN N_EMI_Master_Warna_Barang d "
            SQL = SQL & "ON b.Kode_Perusahaan = d.kode_perusahaan "
            SQL = SQL & "AND b.Kode_Barang = d.kode_barang "

            SQL = SQL & "WHERE a.status IS NULL "
            SQL = SQL & "AND b.Bulan = '" & bulanNow & "' "
            SQL = SQL & "AND b.Tahun = '" & tahunNow & "' "
            SQL = SQL & "AND b.Flag_Validasi_PPIC = 'Y' "
            SQL = SQL & "AND b.Nilai_PPIC <> 0 "
            SQL = SQL & "order by c.nama asc "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read


                    DataGridView1.Rows.Add(1)
                    arrKodeBarang.Add(Dr("kode_barang"))

                    DataGridView1.Rows.Item(index).Cells(cellCheck).Value = True
                    DataGridView1.Rows.Item(index).Cells(cellKdBrg).Value = Dr("kode_barang")
                    DataGridView1.Rows.Item(index).Cells(cellNama).Value = Dr("nama_inisial")
                    DataGridView1.Rows.Item(index).Cells(cellJmlhPerbulan).Value = Format(Dr("Jumlah"), "N2")
                    DataGridView1.Rows.Item(index).Cells(cellTerpakai).Value = Format(Dr("Jumlah_Terpakai"), "N2")
                    DataGridView1.Rows.Item(index).Cells(cellWarna).Value = ""
                    DataGridView1.Rows.Item(index).Cells(cellWarna).Style.BackColor = GetColor(Dr("BackColor"))(0)

                    index += 1
                Loop
            End Using
            awalLoad = False
            Load_data()


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Public Sub Load_data()

        Dim listKode As New List(Of String)

        For i As Integer = 0 To DataGridView1.Rows.Count - 1

            get_isi_listview(i)

            If LvCheck = "True" Then
                listKode.Add("'" & arrKodeBarang.Item(i).ToString & "'")
            End If

        Next

        Dim kode_barang As String = String.Join(",", listKode)





        hg.RemoveAll()
        Try
            OpenConn()





            SQL = " select b.Kode_Perusahaan,b.tanggal_start,b.No_Urut,b.tanggal_end,a.no_transaksi,b.Tanggal,a.bulan,a.tahun, b.kode_barang,d.nama_inisial,c.Nama,b.Jumlah,b.Satuan , d.backColor, d.barColor From N_EMI_Production_Plan_Schedule a "
            SQL = SQL & "inner join  N_EMI_Production_Plan_Schedule_detail b  on  "
            SQL = SQL & "a.no_transaksi = b.no_transaksi and a.status is null and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "inner join barang c  on  "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.kode_Stock_owner = c.Kode_Stock_Owner and b.kode_barang = c.Kode_Barang "

            SQL = SQL & " inner join N_EMI_master_warna_barang d on  "
            SQL = SQL & " c.Kode_Perusahaan = d.kode_perusahaan and c.kode_barang = d.kode_barang "

            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan   "
            SQL = SQL & "and a.bulan = '" & bulanNow & "' and a.tahun = '" & tahunNow & "'  "
            SQL = SQL & "and b.jumlah <> 0  "
            If kode_barang <> "" Then
                SQL = SQL & "And b.kode_barang in (" & kode_barang & ")  "
            Else
                SQL = SQL & "And b.kode_barang in ('')  "
            End If

            SQL = SQL & "order by b.tanggal "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            Dim tglAngka As Integer = CInt(.Rows(i).Item("tanggal"))

                            Dim tglMulai As New DateTime(tahunNow, CInt(bulanNow), tglAngka, 0, 0, 0)
                            Dim tglSelesai As New DateTime(tahunNow, bulanNow, tglAngka, 23, 59, 59)

                            Dim No_Urut As String = .Rows(i).Item("No_Urut")

                            Dim deksripsi As String = .Rows(i).Item("nama_inisial") & ", " & Format(.Rows(i).Item("jumlah"), "N2") & ", " & .Rows(i).Item("satuan")



                            Dim ev As New Hourglass.HourglassEvent()

                            Dim tglStart As DateTime
                            Dim tglEnd As DateTime


                            ' ID harus unik (gabungin nama barang + index i biar aman)
                            ev.Id = No_Urut

                            ' Posisikan di kalender sesuai tglAngka dari database
                            If DateTime.TryParse(.Rows(i).Item("tanggal_start").ToString(), tglStart) Then
                                ev.Start = tglStart
                            End If

                            If DateTime.TryParse(.Rows(i).Item("tanggal_end").ToString(), tglEnd) Then
                                ev.End = tglEnd
                            End If

                            ev.BackColor = GetColor(.Rows(i).Item("backColor"))(0)
                            ev.BarColor = GetColor(.Rows(i).Item("barColor"))(0)

                            ev.Text = deksripsi

                            hg.AddEvent(ev)

                        Next
                    End If
                End With
            End Using
            hg.Render()


        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Public Function GetColor(ByVal Warna As String) As List(Of Color)
        Dim ret = New List(Of Color)()

        Dim wrn As String = Warna
        Dim charR As Integer = wrn.Substring(0, Len(wrn)).IndexOf(",")
        Dim r As String = Trim(wrn.Substring(0, charR))

        wrn = wrn.Substring(charR + 1, Len(wrn) - (charR + 1))
        Dim charG As String = wrn.Substring(0, Len(wrn)).IndexOf(",")
        Dim g As String = Trim(wrn.Substring(0, charG))

        wrn = wrn.Substring(charG + 1, Len(wrn) - (charG + 1))
        Dim b As String = Trim(wrn)

        ret.Add(Color.FromArgb(r, g, b))

        Return ret
    End Function

    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub




    Private Sub LblDay_Click(sender As Object, e As EventArgs) Handles LblDay.Click
        HideLine(PnlDay)
        HideLabel(LblDay)

        Load_data()
        hg.Options.ViewType = Hourglass.ViewTypes.Day
        hg.Render()
    End Sub

    Private Sub LblWeek_Click(sender As Object, e As EventArgs) Handles LblWeek.Click
        HideLine(PnlWeek)
        HideLabel(LblWeek)

        Load_data()
        hg.Options.ViewType = Hourglass.ViewTypes.Week
        hg.Render()
    End Sub

    Private Sub LblMonth_Click(sender As Object, e As EventArgs) Handles LblMonth.Click
        HideLine(PnlMonth)
        HideLabel(LblMonth)

        Load_data()
        hg.Options.ViewType = Hourglass.ViewTypes.Month
        hg.Render()
    End Sub
















    'Private Sub hg_OnEventClick(Sender As Object, _event As HourglassEvent) Handles hg.OnEventClick

    '    Try
    '        OpenConn()

    '        If CekButtonRole("Production_PLan_Schedule_Tambah_Perhari") = "T" Then
    '            CloseTrans()
    '            CloseConn()
    '            MessageBox.Show("anda tidak memiliki akses ! !")
    '            Exit Sub
    '        End If

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try

    '    Dim tglKlik As DateTime = _event.Start



    '    ' 2. Cek apakah bulan atau tahun yang diklik beda dengan sekarang
    '    If tglKlik.Month <> bulanNow OrElse tglKlik.Year <> tahunNow Then
    '        MessageBox.Show("Maaf, kamu hanya boleh mengisi data untuk bulan sekarang (" & Format(DateTime.Now, "MMMM yyyy") & ")",
    '                "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Exit Sub ' Berhenti di sini, Form Tambah gak bakal muncul
    '    End If

    '    Dim frmTambah As New N_EMI_SD_Tambah_Production_Schedule()


    '    frmTambah.TanggalDariKalender = tglKlik


    '    frmTambah.ShowDialog()

    '    If frmTambah.mustUpdate = True Then
    '        Load_Data_Forecast()
    '        Load_data()

    '        hg.Invalidate()
    '    End If


    'End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        If awalLoad Then Exit Sub

        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Dim data = DataGridView1.Rows(currentRow).Cells(currentCell)

        If currentCell = cellCheck Then
            Load_data()
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Dim data = DataGridView1.Rows(currentRow).Cells(currentCell)

        If currentCell = cellCheck Then
            DataGridView1.EndEdit()
        End If
    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Dim data = DataGridView1.Rows(currentRow).Cells(currentCell)

        If currentCell = cellCheck Then
            DataGridView1.EndEdit()
        End If
    End Sub

    Private Sub hg_OnEventMove(Sender As Object, e As RangeCalendarEvent) Handles hg.OnEventMove

        If e.event.Text.Trim.Length = 0 Then
            e.Cancel = True
            Exit Sub
        End If
        Dim judul As String = ""
        Dim deksripsi As String = ""
        Dim dataBulan As String = ""
        Dim keterangan As String = ""
        Dim JamStart As String = ""
        Dim JamEnd As String = ""
        Dim kode_barang As String = ""
        Dim no_faktur_lama As String
        Dim no_formula As String = ""
        Dim tanggalStart As String = ""
        Dim nama As String = ""

        get_jam()

        Try
            OpenConn()

            If CekButtonRole("Production_Plan_Schedule_PPIC") = "T" Then

                CloseConn()
                e.Cancel = True
                Exit Sub
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            SQL = "select   CAST(Tanggal_Start AS DATE) AS tanggal,c.status,a.no_Transaksi,a.kode_formula,b.nama_inisial,a.jumlah,a.satuan,a.keterangan, c.bulan, "
            SQL = SQL & "a.kode_barang "
            SQL = SQL & " from N_EMI_Production_Plan_Schedule_detail a "
            SQL = SQL & "inner join N_EMI_Master_Warna_Barang b on "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.kode_barang = b.kode_barang  "
            SQL = SQL & "inner join N_EMI_production_Plan_Schedule c on "
            SQL = SQL & "a.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.no_transaksi = c.no_transaksi  "
            SQL = SQL & "where No_Urut ='" & e.event.Id & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    deksripsi = dr("nama_inisial") & ", " & Format(dr("jumlah"), "N2") & ", " & dr("satuan")
                    dataBulan = dr("bulan")

                    nama = dr("nama_inisial")

                    If General_Class.CekNULL(dr("keterangan")) = "" Then
                        keterangan = ""
                    Else
                        keterangan = dr("keterangan")
                    End If


                    If General_Class.CekNULL(dr("status")) = "Y" Then
                        CloseConn()
                        MessageBox.Show("Data sudah dibatalkan sebelumnya coba refresh ulang.", judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If


                    kode_barang = dr("kode_barang")
                    no_faktur_lama = dr("no_transaksi")

                    If General_Class.CekNULL(dr("kode_formula")) = "Y" Then
                        no_formula = "'" & dr("kode_formula") & "'"
                    Else
                        no_formula = "NULL"
                    End If

                Else
                    dr.Close()
                    CloseConn()
                    e.Cancel = True
                    Exit Sub
                    MessageBox.Show("data tidak ditemukan . .  ! !", judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            'cek apakah sudah pernah di input ditanggal yang sama
            SQL = "select * from N_EMI_Production_Plan_Schedule_detail a "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_barang = '" & kode_barang & "' "
            SQL = SQL & "and CAST(Tanggal_Start AS DATE) = '" & e.event.Start.ToString("yyyy-MM-dd") & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Gagal menambahkan , " & nama & " sudah pernah terdaftar pada tanggal " & e.event.Start.ToString("dd MMM yyyy"), judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    e.Cancel = True
                    Exit Sub
                End If
            End Using



            If dataBulan <> e.event.Start.ToString("MM") Then
                CloseConn()
                e.Cancel = True

                MessageBox.Show("Gagal , bulan haru sama. Bulan : " & dataBulan, judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If dataBulan <> e.event.End.ToString("MM") Then
                CloseConn()
                e.Cancel = True

                MessageBox.Show("Gagal , bulan haru sama. Bulan : " & dataBulan, judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            SQL = "INSERT INTO N_EMI_Production_Plan_Schedule_log ("
            SQL = SQL & "Kode_Perusahaan, No_Transaksi, User_Id, Tanggal, Tanggal_Perhari, "
            SQL = SQL & "Bulan, Tahun, Kode_Formula, Jumlah, Satuan, No_Urut_Detail, jam, kode_Stock_owner,kode_barang ,"
            SQL = SQL & "tanggal_start, tanggal_end, keterangan"
            SQL = SQL & ")"

            SQL = SQL & "select   "
            SQL = SQL & "Kode_Perusahaan, No_Transaksi, '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & e.event.End.ToString("dd") & "', "
            SQL = SQL & "'" & e.event.End.ToString("MM") & "' , '" & e.event.End.ToString("yyyy") & "', kode_formula,jumlah,satuan,no_urut, "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', kode_stock_owner,kode_barang, '" & e.event.Start & "', "
            SQL = SQL & "'" & e.event.End & "', keterangan "
            SQL = SQL & "From N_EMI_Production_Plan_Schedule_detail where no_urut = '" & e.event.Id & "'"
            ExecuteTrans(SQL)



            SQL = "update N_EMI_Production_Plan_Schedule_detail set "
            SQL = SQL & "tanggal_start='" & e.event.Start & "', "
            SQL = SQL & "tanggal_end='" & e.event.End & "', "
            SQL = SQL & "tanggal_full = '" & e.event.Start & "', "
            SQL = SQL & "tanggal = '" & e.event.Start.ToString("dd") & "', "
            SQL = SQL & "keterangan = '" & keterangan & "' "
            SQL = SQL & "where no_urut ='" & e.event.Id & "'"
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            e.Cancel = True

            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        e.event.Text = deksripsi
        e.event.Update()

    End Sub

    Private Sub hg_DragDrop(sender As Object, e As DragEventArgs) Handles hg.DragDrop
        e.Effect = DragDropEffects.None
    End Sub

    Private Sub hg_OnEventEdit(Sender As Object, e As RangeCalendarEvent) Handles hg.OnEventEdit

        If e.event.Text.Trim.Length = 0 Then
            e.Cancel = True
            Exit Sub
        End If
        Dim judul As String = ""
        Dim deksripsi As String = ""
        Dim dataBulan As String = ""
        Dim keterangan As String = ""
        Dim JamStart As String = ""
        Dim JamEnd As String = ""
        Dim kode_barang As String = ""
        Dim no_faktur_lama As String
        Dim no_formula As String = ""

        get_jam()

        Try
            OpenConn()

            If CekButtonRole("Production_PLan_Schedule_Tambah_Perhari") = "T" Then

                CloseConn()
                e.Cancel = True
                Exit Sub
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            SQL = "select a.no_Transaksi,a.kode_formula,b.nama_inisial,a.jumlah,a.satuan,a.keterangan, c.bulan, "
            SQL = SQL & "a.kode_barang "
            SQL = SQL & " from N_EMI_Production_Plan_Schedule_detail a "
            SQL = SQL & "inner join N_EMI_Master_Warna_Barang b on "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.kode_barang = b.kode_barang  "
            SQL = SQL & "inner join N_EMI_production_Plan_Schedule c on "
            SQL = SQL & "a.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.no_transaksi = c.no_transaksi  "
            SQL = SQL & "where No_Urut ='" & e.event.Id & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    deksripsi = dr("nama_inisial") & ", " & Format(dr("jumlah"), "N2") & ", " & dr("satuan")
                    dataBulan = dr("bulan")

                    If General_Class.CekNULL(dr("keterangan")) = "" Then
                        keterangan = ""
                    Else
                        keterangan = dr("keterangan")
                    End If


                    kode_barang = dr("kode_barang")
                    no_faktur_lama = dr("no_transaksi")
                    no_formula = dr("kode_formula")
                Else
                    dr.Close()
                    CloseConn()
                    e.Cancel = True
                    Exit Sub
                    MessageBox.Show("data tidak ditemukan . .  ! !", judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            If dataBulan <> e.event.Start.ToString("MM") Then
                CloseConn()
                e.Cancel = True

                MessageBox.Show("Gagal , bulan haru sama. Bulan : " & dataBulan, judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If dataBulan <> e.event.End.ToString("MM") Then
                CloseConn()
                e.Cancel = True

                MessageBox.Show("Gagal , bulan haru sama. Bulan : " & dataBulan, judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            SQL = "INSERT INTO N_EMI_Production_Plan_Schedule_log ("
            SQL = SQL & "Kode_Perusahaan, No_Transaksi, User_Id, Tanggal, Tanggal_Perhari, "
            SQL = SQL & "Bulan, Tahun, Kode_Formula, Jumlah, Satuan, No_Urut_Detail, jam, kode_Stock_owner,kode_barang ,"
            SQL = SQL & "tanggal_start, tanggal_end, keterangan"
            SQL = SQL & ")"

            SQL = SQL & "select   "
            SQL = SQL & "Kode_Perusahaan, No_Transaksi, '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & e.event.End.ToString("dd") & "', "
            SQL = SQL & "'" & e.event.End.ToString("MM") & "' , '" & e.event.End.ToString("yyyy") & "', kode_formula,jumlah,satuan,no_urut, "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', kode_stock_owner,kode_barang, '" & e.event.Start & "', "
            SQL = SQL & "'" & e.event.End & "', keterangan "
            SQL = SQL & "From N_EMI_Production_Plan_Schedule_detail where no_urut = '" & e.event.Id & "'"
            ExecuteTrans(SQL)



            'SQL = "INSERT INTO N_EMI_Production_Plan_Schedule_log ("
            'SQL = SQL & "Kode_Perusahaan, No_Transaksi, User_Id, Tanggal, Tanggal_Perhari, "
            'SQL = SQL & "Bulan, Tahun, Kode_Formula, Jumlah, Satuan, No_Urut_Detail, jam, kode_Stock_owner,kode_barang ,"
            'SQL = SQL & "tanggal_start, tanggal_end, keterangan"
            'SQL = SQL & ") VALUES ("


            'SQL = SQL & "'" & KodePerusahaan & "', "
            'SQL = SQL & "'" & no_faktur_lama & "', "
            'SQL = SQL & "'" & UserID & "', "
            'SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            'SQL = SQL & "'" & e.event.End.ToString("dd") & "', "


            'SQL = SQL & "'" & e.event.End.ToString("MM") & "', "
            'SQL = SQL & "'" & e.event.End.ToString("yyyy") & "', "


            'SQL = SQL & "'" & no_formula & "', "
            'SQL = SQL & " " & HilangkanTanda(LvJumlah) & ", "


            'SQL = SQL & "'" & LvSatuan & "', "

            'SQL = SQL & "'" & .Rows(indexBaru).Item("No_urut") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
            'SQL = SQL & ", '" & LvStockOwner & "', '" & kode_barang & "' ,"
            'SQL = SQL & "'" & e.event.Start & " " & JamStart & "' , '" & e.event.End & " " & JamEnd & "' , "
            'SQL = SQL & "'" & keterangan & "' "

            'SQL = SQL & ")"



            SQL = "update N_EMI_Production_Plan_Schedule_detail set "
            SQL = SQL & "tanggal_start='" & e.event.Start & "', "
            SQL = SQL & "tanggal_end='" & e.event.End & "', "
            SQL = SQL & "tanggal_full = '" & e.event.Start & "', "
            SQL = SQL & "tanggal = '" & e.event.Start.ToString("dd") & "', "
            SQL = SQL & "keterangan = '" & keterangan & "' "
            SQL = SQL & "where no_urut ='" & e.event.Id & "'"
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            e.Cancel = True

            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        e.event.Text = deksripsi
        e.event.Update()
    End Sub

    Private Sub hg_OnEventDoubleClick(Sender As Object, _event As HourglassEvent) Handles hg.OnEventDoubleClick


        Dim tglKlik As DateTime = _event.Start

        Dim id As String = _event.Id







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
        ' 2. Cek apakah bulan atau tahun yang diklik beda dengan sekarang
        If tglKlik.Month <> bulanNow OrElse tglKlik.Year <> tahunNow Then
            MessageBox.Show("Maaf, kamu hanya boleh mengisi data untuk bulan sekarang (" & Format(DateTime.Now, "MMMM yyyy") & ")",
                    "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub ' Berhenti di sini, Form Tambah gak bakal muncul
        End If


        '' === declare
        'Dim kodeBarang As String
        'Dim namaBarang As String
        'Dim jumlahInginProduksi As Double
        'Dim bulanTahunSkrng As String
        'Dim noUrut As String
        'Dim noRv As String
        'Dim noRvSchedule As String
        'Dim jumlahSudahTerpakai As Double
        'Dim jumlahSekarang As Double

        'Dim tanggalStart As String
        'Dim tanggalEnd As String
        'Dim JamStart As String
        'Dim JamEnd As String
        'Dim keterangan As String

        'Dim kodeStockOwner As String
        'Dim idJenisProduk As String
        'Dim ketjenisproduk As String

        'Dim txtSatuan As String

        'Try
        '    OpenConn()





        '    SQL = "select day(Tanggal_Full) as Tanggal,a.kode_stock_owner, b.Bulan,b.Tahun,a.Kode_Barang,c.Nama,a.Jumlah,a.flag_sudah_production_order, "
        '    SQL = SQL & "a.satuan,cast(a.rv as bigint) as rvx_Schedule,"
        '    SQL = SQL & "cast(x.rv as bigint) as rvx_planing, "
        '    SQL = SQL & "a.No_Urut , e.Id_Jenis_Produk,  e.Keterangan as Jenis_produk "
        '    SQL = SQL & ", x.Nilai_PPIC, h.nilai , a.tanggal_start, a.tanggal_end,a.keterangan From N_EMI_Production_Plan_Schedule_Detail a "
        '    SQL = SQL & "inner join N_EMI_Production_Plan_Schedule b on "
        '    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi =b.No_Transaksi "
        '    SQL = SQL & "inner join barang c on "
        '    SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Barang = c.Kode_Barang and a.Kode_Stock_Owner = c.Kode_Stock_Owner "
        '    SQL = SQL & "inner join EMI_Transaksi_Sales_Forecasting_Detail x on  "
        '    SQL = SQL & "x.Kode_Perusahaan = a.Kode_Perusahaan and x.urut = a.Urut_Production_Plan "
        '    SQL = SQL & "inner join EMI_Transaksi_Sales_Forecasting y on  "
        '    SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "


        '    SQL = SQL & "outer apply ( "
        '    SQL = SQL & "select sum(b.Jumlah) as nilai from N_EMI_Production_Plan_Schedule_Detail b "
        '    SQL = SQL & "where b.Urut_Production_Plan = x.urut "
        '    SQL = SQL & ") h "

        '    SQL = SQL & " inner join EMI_Varian d on  c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Varian = d.Id_Varian "
        '    SQL = SQL & "inner join EMI_Jenis_Produk e on d.Kode_Perusahaan = e.Kode_Perusahaan and e.Id_Jenis_Produk = d.Id_Jenis_Produk "

        '    SQL = SQL & "where b.Status is null and No_Urut = " & id & " and y.Status is null "

        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then

        '            ' === isi dari DataReader
        '            kodeBarang = Dr("kode_barang").ToString()
        '            namaBarang = Dr("nama").ToString()
        '            jumlahInginProduksi = Format(Dr("jumlah"), "N2")

        '            Dim namaBulan As String = New DateTime(2000, CInt(Dr("bulan")), 1).ToString("MMM", New Globalization.CultureInfo("id-ID"))

        '            bulanTahunSkrng = Dr("tanggal").ToString() & " " & namaBulan & " " & Dr("tahun").ToString()
        '            noUrut = Dr("No_Urut").ToString()
        '            noRv = Dr("rvx_planing").ToString()
        '            noRvSchedule = Dr("rvx_Schedule").ToString()
        '            jumlahSudahTerpakai = Format(Dr("nilai"), "N2")
        '            jumlahSekarang = Format(Dr("nilai_ppic"), "N2")

        '            If General_Class.CekNULL(Dr("keterangan")) = "" Then
        '                keterangan = ""
        '            Else
        '                keterangan = Dr("keterangan")
        '            End If

        '            tanggalStart = Format(Dr("tanggal_start"), "dd MMM yyyy")
        '            tanggalEnd = Format(Dr("tanggal_end"), "dd MMM yyyy")
        '            JamStart = Format(Dr("tanggal_start"), "HH:ss:mm")
        '            JamEnd = Format(Dr("tanggal_end"), "HH:ss:mm")


        '            kodeStockOwner = Dr("kode_stock_owner")
        '            idJenisProduk = Dr("id_jenis_produk")
        '            ketjenisproduk = Dr("Jenis_produk")


        '        Else
        '            CloseConn()
        '            MessageBox.Show("Belum ada data yang ingin di produksi pada tanggal ini..", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using







        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try



        '' 2. Inisialisasi Form Tambah




        'N_EMI_SD_Detail_Production_Schedule.txtKdBrng.Text = kodeBarang
        'N_EMI_SD_Detail_Production_Schedule.txtNamaBarang.Text = namaBarang
        'N_EMI_SD_Detail_Production_Schedule.txtJumlahSkrng.Text = Format(Val(jumlahSekarang), "N2")
        'N_EMI_SD_Detail_Production_Schedule.TxtTanggal.Text = bulanTahunSkrng
        'N_EMI_SD_Detail_Production_Schedule.txt_NoUrut.Text = noUrut
        'N_EMI_SD_Detail_Production_Schedule.txtNoRv.Text = noRv
        'N_EMI_SD_Detail_Production_Schedule.TextBox1.Text = Format(Val(jumlahInginProduksi), "N2")

        'N_EMI_SD_Detail_Production_Schedule.txtTanggalStart.Text = tanggalStart
        'N_EMI_SD_Detail_Production_Schedule.txt_JamStart.Text = JamStart
        'N_EMI_SD_Detail_Production_Schedule.txtTanggalEnd.Text = tanggalEnd
        'N_EMI_SD_Detail_Production_Schedule.txtJamEnd.Text = JamEnd
        'N_EMI_SD_Detail_Production_Schedule.txtKet.Text = keterangan

        'N_EMI_SD_Detail_Production_Schedule.txtNoRvSch.Text = noRvSchedule
        'N_EMI_SD_Detail_Production_Schedule.txtJumlahTerpakai.Text = Format(Val(jumlahSudahTerpakai), "N2")
        'N_EMI_SD_Detail_Production_Schedule.txtIdJenisProduk.Text = idJenisProduk
        'N_EMI_SD_Detail_Production_Schedule.txtKetJenisProduk.Text = ketjenisproduk
        'N_EMI_SD_Detail_Production_Schedule.txtKdStockOwner.Text = kodeStockOwner
        N_EMI_SD_Detail_Production_Schedule.id = id
        N_EMI_SD_Detail_Production_Schedule.ShowDialog()



        If N_EMI_SD_Detail_Production_Schedule.mustUpdate = True Then
            awalLoad = True
            Load_Data_Forecast()

            ' Load_data()
            hg.Invalidate()
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub hg_OnTimeRangeDoubleClick(Sender As Object, e As RangeCalendarEvent) Handles hg.OnTimeRangeDoubleClick

        Start_Loading(Me)


        Try
            OpenConn()


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
                End_Loading(Me)
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            End_Loading(Me)
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ' 1. Ambil tanggal dari properti e.Start (awal rentang yang diklik)
        Dim tglKlik As DateTime = e.start





        ' 2. Cek apakah bulan atau tahun yang diklik beda dengan sekarang
        If tglKlik.Month <> bulanNow OrElse tglKlik.Year <> tahunNow Then

            MessageBox.Show("Maaf, kamu hanya boleh mengisi data untuk bulan sekarang (" & Format(DateTime.Now, "MMMM yyyy") & ")",
                    "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End_Loading(Me)
            Exit Sub ' Berhenti di sini, Form Tambah gak bakal muncul
        End If



        ' 2. Inisialisasi Form Tambah
        Dim frmTambah As New N_EMI_SD_Tambah_Production_Schedule()

        ' 3. Kirim tanggalnya ke property di form tujuan
        frmTambah.TanggalDariKalender = tglKlik

        ' 4. Tampilkan sebagai Dialog
        frmTambah.ShowDialog()



        If frmTambah.mustUpdate = True Then
            awalLoad = True
            Load_Data_Forecast()


            hg.Invalidate()
        End If
        End_Loading(Me)
    End Sub

    Private Sub MonthCalendar1_DateChanged_1(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        bulanNow = Format(MonthCalendar1.SelectionStart, Format("MM"))
        Dim bulanNow2 As String = Format(MonthCalendar1.SelectionStart, Format("MMMM"))
        tahunNow = Format(MonthCalendar1.SelectionStart, Format("yyyy"))


        Dim selectedMonth As String = MonthCalendar1.SelectionStart.ToString("MM")
        Dim selectedYear As String = MonthCalendar1.SelectionStart.ToString("yyyy")



        ' Cek apakah bulan atau tahun berbeda dengan yang terakhir kali di-load
        If selectedMonth <> bulanLoadAwal OrElse selectedYear <> tahunLoadAwal Then

            ' Update variabel global/class
            bulanNow = selectedMonth
            tahunNow = selectedYear

            ' Simpan status terakhir
            bulanLoadAwal = selectedMonth
            tahunLoadAwal = selectedYear

            Try
                OpenConn()

                Dim listFilter As New List(Of String)

                listFilter.Add("(a.bulan = " & selectedMonth & " AND a.tahun = " & selectedYear & ")")
                Dim filterBulanTahun As String = String.Join(" OR ", listFilter)

                ' 2.
                RefreshForecastingSemiFG(
                    KodePerusahaan,
                    filterBulanTahun,
                    UserID,
                "Batal Schedule"
            )



                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            awalLoad = True
            Load_Data_Forecast()

        End If




        hg.Options.StartDate = MonthCalendar1.SelectionStart
        hg.Render()

    End Sub

    Private Sub hg_OnTimeRangeSelect(Sender As Object, e As RangeCalendarEvent) Handles hg.OnTimeRangeSelect
        'Try
        '    OpenConn()


        '    Dim adaAksesRoleButton As Boolean = True

        '    If CekButtonRole("Production_Plan_Schedule_Formulator") = "T" Then
        '        adaAksesRoleButton = False
        '    End If
        '    OpenConn()


        '    If adaAksesRoleButton = False Then
        '        If CekButtonRole("Production_Plan_Schedule_PPIC") = "T" Then
        '            adaAksesRoleButton = False
        '        Else
        '            adaAksesRoleButton = True
        '        End If

        '    End If


        '    OpenConn()


        '    If adaAksesRoleButton = False Then

        '        CloseConn()
        '        MessageBox.Show("Anda tidak memiliki akses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        '' 1. Ambil tanggal dari properti e.Start (awal rentang yang diklik)
        'Dim tglKlik As DateTime = e.start





        '' 2. Cek apakah bulan atau tahun yang diklik beda dengan sekarang
        'If tglKlik.Month <> bulanNow OrElse tglKlik.Year <> tahunNow Then
        '    MessageBox.Show("Maaf, kamu hanya boleh mengisi data untuk bulan sekarang (" & Format(DateTime.Now, "MMMM yyyy") & ")",
        '            "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub ' Berhenti di sini, Form Tambah gak bakal muncul
        'End If



        '' 2. Inisialisasi Form Tambah
        'Dim frmTambah As New N_EMI_SD_Tambah_Production_Schedule()

        '' 3. Kirim tanggalnya ke property di form tujuan
        'frmTambah.TanggalDariKalender = tglKlik

        '' 4. Tampilkan sebagai Dialog
        'frmTambah.ShowDialog()

        'If frmTambah.mustUpdate = True Then
        '    awalLoad = True
        '    Load_Data_Forecast()


        '    hg.Invalidate()
        'End If
    End Sub






End Class
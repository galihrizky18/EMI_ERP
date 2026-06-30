Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Hourglass

Public Class N_EMI_Schedule_Formulator
    Dim arrId_Routing, arrId_JenisProduk, arrKodeBarang As New ArrayList
    Dim Jenis = "Transaksi_Produksi"
    Dim Category As String = ""

    Dim listTglLibur As New List(Of Integer)

    ' Letakkan di bagian atas class (General Declarations)
    Private bulanLoadAwal As String = ""
    Private tahunLoadAwal As String = ""


    Public mustUpdate As Boolean = False


    Dim LvCheck As String
    Dim LvKdsStckOwner As String
    Dim LvKdBrg As String
    Dim LvNama As String
    Dim LvWarna As String
    Dim LvFormulaDefault As String
    Dim LvButton As String

    Dim cellCheck As Integer = 0
    Dim cellKdStckOwner As Integer = 1
    Dim cellKdBrg As Integer = 2
    Dim cellNama As Integer = 3
    Dim cellWarna As Integer = 4
    Dim cellFormulaDefault As Integer = 5
    Dim cellButton As Integer = 6

    Public awalLoad As Boolean = False

    Dim bulanNow As String
    Dim tahunNow As String

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
        Load_data()
    End Sub

    Private Sub get_isi_listview(index)
        LvCheck = DataGridView1.Rows(index).Cells(cellCheck).Value
        LvKdsStckOwner = DataGridView1.Rows(index).Cells(cellKdStckOwner).Value
        LvKdBrg = DataGridView1.Rows(index).Cells(cellKdBrg).Value
        LvNama = DataGridView1.Rows(index).Cells(cellNama).Value
        LvWarna = DataGridView1.Rows(index).Cells(cellWarna).Value
        LvFormulaDefault = DataGridView1.Rows(index).Cells(cellFormulaDefault).Value
        LvButton = DataGridView1.Rows(index).Cells(cellButton).Value
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
            SQL = $"

                SELECT a.Kode_Perusahaan,
                           b.kode_Stock_owner,
                           b.Kode_Barang,
                           b.Nilai_PPIC
                               AS Jumlah,
                           b.Bulan,
                             b.Kode_Formula,
                           b.Tahun,
                           ISNULL((SELECT SUM(x.jumlah)
                                   FROM N_EMI_Production_Plan_Schedule_detail x,
                                        N_EMI_Production_Plan_Schedule y
                                   WHERE x.kode_perusahaan = y.kode_perusahaan
                                     AND x.no_transaksi = y.no_transaksi
                                     AND y.status IS NULL
                                     AND x.kode_perusahaan = b.kode_perusahaan
                                     AND x.urut_production_plan = b.urut), 0) AS Jumlah_Terpakai,
                           d.barColor,
                           d.backColor,
                           d.nama_inisial
                    FROM EMI_Transaksi_Sales_Forecasting a
                             INNER JOIN EMI_Transaksi_Sales_Forecasting_Detail b
                                        ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
                             INNER JOIN N_EMI_Master_Warna_Barang d
                                        ON b.Kode_Perusahaan = d.kode_perusahaan AND b.Kode_Barang = d.kode_barang
                    WHERE a.status IS NULL
                      AND b.Bulan = '{bulanNow}'
                      AND b.Tahun = '{tahunNow}'
                      AND b.Flag_Validasi_PPIC = 'Y'
                      AND b.Nilai_PPIC <> 0
                    order by d.Nama_Inisial asc
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Do
                        DataGridView1.Rows.Add(1)
                        arrKodeBarang.Add(Dr("kode_barang"))

                        DataGridView1.Rows.Item(index).Cells(cellCheck).Value = True
                        DataGridView1.Rows.Item(index).Cells(cellKdStckOwner).Value = Dr("kode_stock_owner")
                        DataGridView1.Rows.Item(index).Cells(cellKdBrg).Value = Dr("kode_barang")

                        DataGridView1.Rows.Item(index).Cells(cellNama).Value = Dr("nama_inisial")
                        DataGridView1.Rows.Item(index).Cells(cellWarna).Value = ""
                        ' DataGridView1.Rows.Item(index).Cells(cellFormulaDefault).Value = Dr("kode_formula")
                        DataGridView1.Rows.Item(index).Cells(cellWarna).Style.BackColor = GetColor(Dr("BackColor"))(0)


                        index += 1
                    Loop While Dr.Read
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("belum ada data production plan yang di buat untuk bulan ini", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
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






            SQL = $"
        SELECT a.Kode_Perusahaan,
               a.no_transaksi,
               a.no_formula,
               a.Tanggal,
               a.Tanggal_Awal As tanggal_start, -- Dikasih alias biar klop sama VB.NET
               a.Tanggal_Akhir As tanggal_end,  -- Dikasih alias biar klop sama VB.NET
               MONTH(a.Tanggal_Awal) as AngkaBulan,
               YEAR(a.Tanggal_Awal) as AngkaTahun,
               a.kode_barang,
               d.nama_inisial,
               c.Nama As nama_barang,
               d.backColor,
               d.barColor
        FROM N_EMI_Transaksi_Schedule_Formulator a
        INNER JOIN barang c ON a.Kode_Perusahaan = c.Kode_Perusahaan 
                           AND a.kode_Stock_owner = c.Kode_Stock_Owner 
                           AND a.kode_barang = c.Kode_Barang
        INNER JOIN N_EMI_master_warna_barang d ON c.Kode_Perusahaan = d.kode_perusahaan 
                                              AND c.kode_barang = d.kode_barang
        WHERE a.kode_perusahaan = '{KodePerusahaan}' -- Sesuaikan variabel perusahaanmu
          AND MONTH(a.Tanggal_Awal) = '{bulanNow}'
          AND YEAR(a.Tanggal_Awal) = '{tahunNow}'
          AND a.Status IS NULL
          AND a.kode_barang IN ({kode_barang})
        ORDER BY a.Tanggal_Awal
       "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            ' 1. Tarik nama formula/deskripsi dengan benar (tanpa bawa alias tabel "a.")

                            Dim noTransaksi As String = .Rows(i).Item("no_transaksi").ToString()
                            Dim deksripsi As String = .Rows(i).Item("no_formula").ToString()
                            Dim namaBarang As String = .Rows(i).Item("nama_inisial").ToString()

                            Dim ev As New Hourglass.HourglassEvent()

                            ' ID harus unik (gabungin nama barang + index i biar aman kalau No_Urut kosong)
                            ev.Id = noTransaksi.ToString()

                            ' 2. BACA RENTANG TANGGAL UTUH DARI DATABASE
                            Dim tglStart As DateTime
                            Dim tglEnd As DateTime

                            ' Membaca tanggal_start hasil alias SQL
                            If DateTime.TryParse(.Rows(i).Item("tanggal_start").ToString(), tglStart) Then
                                ' Set jam mulai ke awal hari (00:00:00) agar balok kalender mulai dari pagi
                                ev.Start = New DateTime(tglStart.Year, tglStart.Month, tglStart.Day, 0, 0, 0)
                            End If

                            ' Membaca tanggal_end hasil alias SQL
                            If DateTime.TryParse(.Rows(i).Item("tanggal_end").ToString(), tglEnd) Then
                                ' Set jam selesai ke akhir hari (23:59:59) agar balok kalender penuh sampai malam
                                ev.End = New DateTime(tglEnd.Year, tglEnd.Month, tglEnd.Day, 23, 59, 59)
                            End If

                            ' 3. Set Warna dan Teks Laporan di Kalender
                            ev.BackColor = GetColor(.Rows(i).Item("backColor"))(0)
                            ev.BarColor = GetColor(.Rows(i).Item("barColor"))(0)

                            ' Gabungin nama formula + nama barang biar user gak bingung pas lihat kalender
                            ev.Text = namaBarang & " (" & deksripsi & ")"

                            ' Masukkan event rentang tanggal ini ke komponen Hourglass
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
        e.Cancel = True
        Exit Sub





    End Sub

    Private Sub hg_DragDrop(sender As Object, e As DragEventArgs) Handles hg.DragDrop
        e.Effect = DragDropEffects.None
    End Sub

    Private Sub hg_OnEventEdit(Sender As Object, e As RangeCalendarEvent) Handles hg.OnEventEdit


    End Sub

    Private Sub hg_OnEventDoubleClick(Sender As Object, _event As HourglassEvent) Handles hg.OnEventDoubleClick


        Dim tglKlik As DateTime = _event.Start

        Dim id As String = _event.Id




    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick


        If e.ColumnIndex = cellButton Then

            Dim index As Integer = e.RowIndex

            get_isi_listview(index)


            N_EMI_SD_Schedule_Detail_Formulator.txtStcokOwner.Text = LvKdsStckOwner
            N_EMI_SD_Schedule_Detail_Formulator.txtKdBrng.Text = LvKdBrg
            N_EMI_SD_Schedule_Detail_Formulator.txtNamaBarang.Text = LvNama
            N_EMI_SD_Schedule_Detail_Formulator.txtFormulaDefault.Text = LvFormulaDefault
            N_EMI_SD_Schedule_Detail_Formulator.txtBulanTahun.Text = bulanNow & "/" & tahunNow
            N_EMI_SD_Schedule_Detail_Formulator.bulanNow = bulanNow
            N_EMI_SD_Schedule_Detail_Formulator.tahunNow = tahunNow
            N_EMI_SD_Schedule_Detail_Formulator.ShowDialog()


            If mustUpdate Then
                awalLoad = True
                Load_Data_Forecast()

                ' Load_data()
                hg.Invalidate()
            End If


        End If
    End Sub

    Private Sub hg_OnTimeRangeDoubleClick(Sender As Object, e As RangeCalendarEvent) Handles hg.OnTimeRangeDoubleClick


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


            awalLoad = True
            Load_Data_Forecast()

        End If




        hg.Options.StartDate = MonthCalendar1.SelectionStart
        hg.Render()

    End Sub




End Class
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Hourglass

Public Class EMI_Schedule
    Dim arrId_Routing, arrId_JenisProduk As New ArrayList
    Dim Jenis = "Transaksi_Produksi"
    Dim Category As String = ""

    Dim LvCheck As String
    Dim LvKet As String
    Dim Lvwarna As String

    Dim CellCheck As Integer = 0
    Dim CellKet As Integer = 1
    Dim CellWarna As Integer = 2

    Dim Lv2NoRencana As String
    Dim Lv2Ket As String

    Dim Cell2NoRencana As Integer = 0
    Dim Cell2Ket As Integer = 1

    Dim check As Boolean = True
    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Load_data()
    End Sub

    Private Sub get_isi_listview(index)
        LvCheck = DataGridView1.Rows(index).Cells(CellCheck).Value
        LvKet = DataGridView1.Rows(index).Cells(CellKet).Value
        Lvwarna = DataGridView1.Rows(index).Cells(CellWarna).Value
    End Sub

    Private Sub get_isi_listview2(index)
        Lv2NoRencana = DgvSimulasi_DataHPP.Rows(index).Cells(Cell2NoRencana).Value
        Lv2Ket = DgvSimulasi_DataHPP.Rows(index).Cells(Cell2Ket).Value
    End Sub

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

            arrId_JenisProduk.Clear() : CmbSchedule_JenisProduksi.Items.Clear()
            CmbSchedule_JenisProduksi.Items.Add(" -- Seluruh -- ") : arrId_JenisProduk.Add(" -- Seluruh -- ")
            SQL = "select id_jenis_produk, Keterangan from emi_jenis_produk "
            SQL = SQL & "where kode_perusahaan='" & KodePerusahaan & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSchedule_JenisProduksi.Items.Add(dr("Keterangan")) : arrId_JenisProduk.Add(dr("id_jenis_produk"))
                Loop
            End Using
            CmbSchedule_JenisProduksi.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Line()
        Data_Produksi()
        Init.Start()
    End Sub

    Public Sub Data_Produksi()

        Try
            OpenConn()

            DgvSimulasi_DataHPP.Rows.Clear()
            Dim no As Integer = 0
            SQL = "select No_Faktur,Keterangan,Status,Selesai,Flag_Release,Id_Schedule from EMI_Order_Produksi "
            SQL = SQL & "where Id_Schedule is null and Status is null and Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and Selesai is null and Flag_Release = 'Y' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    DgvSimulasi_DataHPP.Rows.Add(1)

                    DgvSimulasi_DataHPP.Rows.Item(no).Cells(Cell2NoRencana).Value = dr("no_faktur")
                    DgvSimulasi_DataHPP.Rows.Item(no).Cells(Cell2Ket).Value = dr("Keterangan")

                    no += 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Line()
        If CmbSchedule_JenisProduksi.SelectedIndex = -1 Then
            MessageBox.Show("pilih dahulu jenis produksi . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSchedule_JenisProduksi.Focus()
            Exit Sub
        End If

        check = False
        Try
            OpenConn()
            Dim id As Integer = 0
            DataGridView1.Rows.Clear() : arrId_Routing.Clear()
            'SQL = "select a.id_line, a.Keterangan as ket_line, b.Id_Jenis_Produk, b.Keterangan, a.backcolor "
            'SQL = SQL & "from EMI_Line a, EMI_Jenis_Produk b where "
            'SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Jenis_Produk=b.Id_Jenis_Produk "
            'SQL = SQL & " And a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = "select a.id_routing, a.Keterangan as ket_routing, b.Id_Jenis_Produk, b.Keterangan, a.backcolor "
            SQL = SQL & "from EMI_Master_Routing a, EMI_Jenis_Produk b where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Jenis_Produk=b.Id_Jenis_Produk "
            SQL = SQL & " And a.Kode_Perusahaan ='" & KodePerusahaan & "' "

            If CmbSchedule_JenisProduksi.SelectedIndex <> 0 Then
                SQL = SQL & "and a.Id_Jenis_Produk='" & arrId_JenisProduk.Item(CmbSchedule_JenisProduksi.SelectedIndex) & "' "
            End If

            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    arrId_Routing.Add(dr("Id_Routing"))

                    DataGridView1.Rows.Add(1)
                    DataGridView1.Rows.Item(id).Cells(CellCheck).Value = True
                    DataGridView1.Rows.Item(id).Cells(CellKet).Value = dr("ket_routing") 'dr("Keterangan") & " " & dr("ket_line")
                    DataGridView1.Rows.Item(id).Cells(CellWarna).Value = ""
                    DataGridView1.Rows.Item(id).Cells(CellWarna).Style.BackColor = GetColor(dr("BackColor"))(0)

                    id += 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        check = True
    End Sub
    Public Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")



        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")


            Label1.Text = "Production Schedule" 'Base_Language.Lang_Transaksi_Produksi_Judul


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        hg.Options.StartDate = MonthCalendar1.SelectionStart
        HideLine(PnlDay)
        HideLabel(LblDay)
        hg.Options.ViewType = Hourglass.ViewTypes.Day
        hg.Render()

        kosong()
        Init.Start()
    End Sub

    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        hg.Options.StartDate = MonthCalendar1.SelectionStart
        hg.Render()
    End Sub

    Private Sub hg_OnEventEdit(Sender As Object, e As RangeCalendarEvent) Handles hg.OnEventEdit, hg.OnEventMove
        If e.event.Text.Trim.Length = 0 Then
            e.Cancel = True
            Exit Sub
        End If
        Dim judul As String = ""
        Try
            OpenConn()
            SQL = "select judul from emi_schedule "
            SQL = SQL & "where id='" & e.event.Id & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    judul = dr("judul")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("data tidak ditemukan . .  ! !", judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "update emi_schedule set "
            SQL = SQL & "Tanggal_awal='" & e.event.Start & "', "
            SQL = SQL & "Tanggal_akhir='" & e.event.End & "' "
            SQL = SQL & "where id='" & e.event.Id & "'"
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        e.event.Text = Format(e.event.Start, "HH:mm") & " - " & Format(e.event.End, "HH:mm") & " | " & judul
        e.event.Update()
    End Sub

    Public Sub Load_data()

        Dim id_routing As String = ""
        Dim ind As Integer = 0
        For index = 0 To DataGridView1.Rows.Count - 1
            get_isi_listview(index)
            If LvCheck = "True" Then
                If ind <> 0 Then
                    id_routing = id_routing + ", "
                End If
                id_routing = id_routing + arrId_Routing.Item(index).ToString
                ind += 1
            End If
        Next

        If id_routing = "" Then
            Exit Sub
        End If

        hg.RemoveAll()
        Try
            OpenConn()
            'GetColor()(1),
            'SQL = "select id, Judul, deskripsi, a.id_line as kategori, tanggal_awal, Tanggal_akhir, BarColor, BackColor from Emi_Schedule a, emi_line b "
            'SQL = SQL & "where a.kode_perusahaan=b.kode_perusahaan and a.id_line=b.id_line and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            'SQL = SQL & "and a.id_line in(" & id_line & ")"
            SQL = "select id, Judul, deskripsi, a.id_routing as kategori, tanggal_awal, Tanggal_akhir, BarColor, BackColor from Emi_Schedule a, emi_master_routing b "
            SQL = SQL & "where a.kode_perusahaan=b.kode_perusahaan and a.id_routing=b.id_routing and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.id_routing in(" & id_routing & ")"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Category = dr("kategori")
                    hg.AddEvent(New Hourglass.HourglassEvent With {
                    .Id = dr("ID"),
                    .BarColor = GetColor(dr("BarColor"))(0),
                    .BackColor = GetColor(dr("BackColor"))(0),
                    .Start = dr("tanggal_awal"),
                    .[End] = dr("Tanggal_akhir"),
                    .Text = Format(dr("tanggal_awal"), "HH:mm") & " - " & Format(dr("tanggal_akhir"), "HH:mm") & " | " & dr("Judul"),
                    .Tooltip = dr("deskripsi")
                    })
                Loop
            End Using
            'hg.AddEvent(New Hourglass.HourglassEvent With {
            '        .IsAllDay = False,
            '        .Start = "2024-07-13",
            '        .[End] = "2024-07-14",
            '        .Text = "TEST ALL DAY"
            '        })

            CloseConn()
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

    Private Sub Init_Tick(sender As Object, e As EventArgs) Handles Init.Tick
        Init.Stop()
        '''Dim Pesan As String = ""
        '''get_jam()

        '''Try
        '''    OpenConn()

        '''    SQL = "Select c.Id_Jenis_Produk,c.Keterangan from EMI_Schedule a, emi_line b, EMI_Jenis_Produk c where "
        '''    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Id_Line = b.Id_Line And b.Id_Jenis_Produk = c.Id_Jenis_Produk "
        '''    SQL = SQL & "And format(Tanggal_Awal,'yyyy-MM-dd')='" & Format(tgl_skg, "yyyy-MM-dd") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
        '''    SQL = SQL & "group by c.Id_Jenis_Produk, c.Keterangan "
        '''    Using ds = BindingTrans(SQL)
        '''        With ds.Tables("MyTable")
        '''            For index = 0 To .Rows.Count - 1
        '''                Dim jenis As String = .Rows(index).Item("Keterangan")
        '''                Dim idjenis As String = .Rows(index).Item("Id_Jenis_Produk")

        '''                Pesan = Pesan & jenis & " : " & Chr(13)
        '''                SQL = "Select b.id_line, b.Keterangan from EMI_Schedule a, emi_line b, EMI_Jenis_Produk c where "
        '''                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Id_Line = b.Id_Line And b.Id_Jenis_Produk = c.Id_Jenis_Produk "
        '''                SQL = SQL & "And format(Tanggal_Awal,'yyyy-MM-dd')='" & Format(tgl_skg, "yyyy-MM-dd") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
        '''                SQL = SQL & "and c.id_jenis_produk='" & idjenis & "' "
        '''                SQL = SQL & "group by b.id_line, b.Keterangan "
        '''                Using ds2 = BindingTrans(SQL)
        '''                    For index2 = 0 To ds2.Tables("MyTable").Rows.Count - 1
        '''                        Dim line As String = ds2.Tables("MyTable").Rows(index2).Item("Keterangan")
        '''                        Dim idline As String = ds2.Tables("MyTable").Rows(index2).Item("id_line")

        '''                        Pesan = Pesan & Chr(9) & line & " : " & Chr(13)
        '''                        SQL = "Select judul, format(Tanggal_Awal,'HH:mm') as jam_awal, format(Tanggal_Akhir,'HH:mm') as jam_akhir "
        '''                        SQL = SQL & "from EMI_Schedule a, emi_line b, EMI_Jenis_Produk c where "
        '''                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Id_Line = b.Id_Line And b.Id_Jenis_Produk = c.Id_Jenis_Produk "
        '''                        SQL = SQL & "And format(Tanggal_Awal,'yyyy-MM-dd')='" & Format(tgl_skg, "yyyy-MM-dd") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
        '''                        SQL = SQL & "and c.id_jenis_produk='" & idjenis & "' and b.id_line='" & idline & "' "
        '''                        Using ds3 = BindingTrans(SQL)
        '''                            For index3 = 0 To ds3.Tables("MyTable").Rows.Count - 1
        '''                                Dim judul As String = ds3.Tables("MyTable").Rows(index3).Item("judul")
        '''                                Dim jam As String = ds3.Tables("MyTable").Rows(index3).Item("jam_awal") & "-" & ds3.Tables("MyTable").Rows(index3).Item("jam_akhir")

        '''                                Pesan = Pesan & Chr(9) & Chr(9) & judul & " : " & jam & Chr(13)
        '''                            Next
        '''                        End Using

        '''                    Next
        '''                End Using
        '''            Next
        '''        End With
        '''    End Using


        '''    CloseConn()
        '''Catch ex As Exception
        '''    CloseConn()
        '''    MessageBox.Show(ex.Message)
        '''    Exit Sub
        '''End Try

        '''& Chr(13)
        '''hg.Message(Pesan, 10)
        Load_data()
    End Sub

    Private Sub CmbSchedule_JenisProduksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSchedule_JenisProduksi.SelectedIndexChanged
        If CmbSchedule_JenisProduksi.SelectedIndex = -1 Then
            Exit Sub
        End If
        Line()
    End Sub

    Private Sub ListView2_ItemChecked(sender As Object, e As ItemCheckedEventArgs)
        If check = False Then
            Exit Sub
        End If
        Load_data()
    End Sub


    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Dim data = DataGridView1.Rows(currentRow).Cells(currentCell)

        If currentCell = CellCheck Then
            DataGridView1.EndEdit()
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Dim data = DataGridView1.Rows(currentRow).Cells(currentCell)

        If currentCell = CellCheck Then
            DataGridView1.EndEdit()
        End If
    End Sub

    Private Sub LblDay_Click(sender As Object, e As EventArgs) Handles LblDay.Click
        HideLine(PnlDay)
        HideLabel(LblDay)

        hg.Options.ViewType = Hourglass.ViewTypes.Day
        hg.Render()
    End Sub

    Private Sub LblWeek_Click(sender As Object, e As EventArgs) Handles LblWeek.Click
        HideLine(PnlWeek)
        HideLabel(LblWeek)

        hg.Options.ViewType = Hourglass.ViewTypes.Week
        hg.Render()
    End Sub

    Private Sub LblMonth_Click(sender As Object, e As EventArgs) Handles LblMonth.Click
        HideLine(PnlMonth)
        HideLabel(LblMonth)

        hg.Options.ViewType = Hourglass.ViewTypes.Month
        hg.Render()
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Dim data = DataGridView1.Rows(currentRow).Cells(currentCell)

        If currentCell = CellCheck Then
            Load_data()
        End If
    End Sub

    Private Sub hg_DoubleClick(sender As Object, e As EventArgs) Handles hg.DoubleClick
        MessageBox.Show(e.ToString)
    End Sub

    Private Sub hg_OnEventClick(Sender As Object, _event As HourglassEvent) Handles hg.OnEventClick

    End Sub

    Private Sub hg_OnEventResize(Sender As Object, e As RangeCalendarEvent) Handles hg.OnEventResize
        hg.RemoveEvent(e.event)
        Try
            OpenConn()
            'GetColor()(1),
            'SQL = "select id, Judul, deskripsi, a.id_line as kategori, tanggal_awal, Tanggal_akhir, BarColor, BackColor from Emi_Schedule a, emi_line b "
            'SQL = SQL & "where a.kode_perusahaan=b.kode_perusahaan and a.id_line=b.id_line and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            'SQL = SQL & "and a.id ='" & e.event.Id & "'"
            SQL = "select id, Judul, deskripsi, a.id_routing as kategori, tanggal_awal, Tanggal_akhir, BarColor, BackColor from Emi_Schedule a, emi_master_routing b "
            SQL = SQL & "where a.kode_perusahaan=b.kode_perusahaan and a.id_routing=b.id_routing and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.id ='" & e.event.Id & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Category = dr("kategori")
                    hg.AddEvent(New Hourglass.HourglassEvent With {
                    .Id = dr("ID"),
                    .BarColor = GetColor(dr("BarColor"))(0),
                    .BackColor = GetColor(dr("BackColor"))(0),
                    .Start = dr("tanggal_awal"),
                    .[End] = dr("Tanggal_akhir"),
                    .Text = Format(dr("tanggal_awal"), "HH:mm") & " - " & Format(dr("tanggal_akhir"), "HH:mm") & " | " & dr("Judul"),
                    .Tooltip = dr("deskripsi")
                    })
                Loop
            End Using
            'hg.AddEvent(New Hourglass.HourglassEvent With {
            '        .IsAllDay = False,
            '        .Start = "2024-07-13",
            '        .[End] = "2024-07-14",
            '        .Text = "TEST ALL DAY"
            '        })

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub hg_OnEventDoubleClick(Sender As Object, _event As HourglassEvent) Handles hg.OnEventDoubleClick
        If _event.Text.Trim.Length = 0 Then
            Exit Sub
        End If
        Dim no_faktur As String = ""
        Try
            OpenConn()
            SQL = "Select No_Rencana_Produksi from emi_schedule "
            SQL = SQL & "where id='" & _event.Id & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    no_faktur = dr("No_Rencana_Produksi")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        EMI_Schedule_SD_Tambah.TxtSchedule_NoFaktur.Text = no_faktur
        EMI_Schedule_SD_Tambah.BtnPilihBarang_Simpan.Text = "&Update"
        EMI_Schedule_SD_Tambah.BtnPilihBarang_Simpan.Tag = "Update"
        EMI_Schedule_SD_Tambah.ShowDialog()
    End Sub

    Private Sub DgvSimulasi_DataHPP_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSimulasi_DataHPP.CellContentClick

    End Sub

    Private Sub DgvSimulasi_DataHPP_DoubleClick(sender As Object, e As EventArgs) Handles DgvSimulasi_DataHPP.DoubleClick
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            MessageBox.Show("Data tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If DgvSimulasi_DataHPP.Rows.Count = 0 Or DgvSimulasi_DataHPP.SelectedCells.Count = 0 Then
            MessageBox.Show("Data tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Dim data = DataGridView1.Rows(currentRow).Cells(currentCell)
        get_isi_listview2(currentRow)
        EMI_Schedule_SD_Tambah.TxtSchedule_NoFaktur.Text = Lv2NoRencana
        EMI_Schedule_SD_Tambah.BtnPilihBarang_Simpan.Text = "&Simpan"
        EMI_Schedule_SD_Tambah.BtnPilihBarang_Simpan.Tag = "Simpan"
        EMI_Schedule_SD_Tambah.ShowDialog()
    End Sub

End Class
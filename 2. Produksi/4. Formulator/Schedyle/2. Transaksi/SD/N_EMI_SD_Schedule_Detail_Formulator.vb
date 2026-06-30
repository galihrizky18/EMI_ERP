Imports System.Drawing.Drawing2D
Imports MS.Internal
'Imports System.Windows.Forms.VisualStyles.VisualStyleElement
'Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_SD_Schedule_Detail_Formulator

    Public TanggalDariKalender As DateTime


    Public mustUpdate As Boolean = False
    Public kodeBarang As String
    Public namaBarang As String
    Public id As String


    Public jumlahSekarang As Double
    Public jumlahInginProduksi As Double
    Public bulanTahunSkrng As String
    Public no_urut As Integer
    Public noRv As String
    Public noRvSchedule As String
    Public jumlahSudahTerpakai As Double
    Public bulanNow As String
    Public tahunNow As String

    Dim sudahPO As String
    Dim statusRelease As String

    Dim FlagbtnShow As Boolean = False

    Dim bulan As String
    Dim tanggal As String


    Dim warnaShow As Color = Color.FromArgb(232, 241, 251)
    Dim warnaHide As Color = Color.FromArgb(240, 240, 240)
    Dim warnaBatal As Color = Color.FromArgb(200, 80, 80)
    Dim warnaDefault As Color = Color.FromArgb(15, 86, 122)


    Dim LvButton As String
    Dim LvNoTransaksi As String
    Dim LvKodeBrg As String
    Dim LvNama As String
    Dim LvFormula As String
    Dim LvTanggal As String
    Dim LvKeterangan As String
    Dim LvFlagPermanen As String


    Dim cellButton As Integer = 0
    Dim cellNoTransaksi As Integer = 1
    Dim cellKodeBarang As Integer = 2
    Dim cellNama As Integer = 3
    Dim cellFormula As Integer = 4
    Dim cellTanggal As Integer = 5
    Dim cellKeterangan As Integer = 6
    Dim cellFlagPermanen As Integer = 7


    Dim Faktur As String = ""

    Dim fTransSalesForcasting As String = "SF"

    Private Sub get_no_faktur()
        Faktur = fTransSalesForcasting & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Transaksi_Schedule_Formulator", "no_transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_transaksi, 1, " & Len(fTransSalesForcasting) + 4 & ")", fTransSalesForcasting & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub get_isi_listview(ByVal Index As Integer)
        LvButton = DGV_Data.Rows(Index).Cells(cellButton).Value
        LvNoTransaksi = DGV_Data.Rows(Index).Cells(cellNoTransaksi).Value
        LvKodeBrg = DGV_Data.Rows(Index).Cells(cellKodeBarang).Value
        LvNama = DGV_Data.Rows(Index).Cells(cellNama).Value
        LvFormula = DGV_Data.Rows(Index).Cells(cellFormula).Value
        LvTanggal = DGV_Data.Rows(Index).Cells(cellTanggal).Value
        LvKeterangan = DGV_Data.Rows(Index).Cells(cellKeterangan).Value
        LvFlagPermanen = DGV_Data.Rows(Index).Cells(cellFlagPermanen).Value
    End Sub

    Private Sub SetRoundedButton(btn As Button, radius As Integer)
        Dim path As New Drawing2D.GraphicsPath()

        path.AddArc(btn.ClientRectangle.X, btn.ClientRectangle.Y, radius, radius, 180, 90)
        path.AddArc(btn.ClientRectangle.Right - radius, btn.ClientRectangle.Y, radius, radius, 270, 90)
        path.AddArc(btn.ClientRectangle.Right - radius, btn.ClientRectangle.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(btn.ClientRectangle.X, btn.ClientRectangle.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()

        btn.Region = New Region(path)
    End Sub

    Private Sub N_EMI_SD_Schedule_Detail_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.BackColor = Color.FromArgb(232, 238, 246)  ' #E8EEF6 - biru muda
        'TxtTanggal.ForeColor = Color.FromArgb(100, 116, 139)   ' abu-abu biru untuk teks
        kosong()

        LvPilihBarang_DataBarang.Visible = False
        LvPilihBarang_DataBarang.Location = New Point(208, 174)
    End Sub


    Private Sub loadDataScheduleFormulator()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            DGV_Data.Rows.Clear()
            SQL = $"

                select  a.No_Transaksi,a.Kode_Barang,a.status, flag_formula_permanen,
                isnull((select top(1) nama from barang x where x.Kode_Perusahaan = a.Kode_Perusahaan
                                         and a.Kode_Barang = x.Kode_Barang),'-') Nama
                    ,No_Formula,Tanggal_Awal,Tanggal_Akhir, keterangan From N_EMI_Transaksi_Schedule_Formulator a
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and Kode_Barang = '{txtKdBrng.Text}' and  year(a.Tanggal_Awal) = '{tahunNow}' and month(a.Tanggal_Awal) = '{bulanNow}'
                order by tanggal desc, jam desc
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")

                    If .Rows.Count <> 0 Then


                        For i As Integer = 0 To .Rows.Count - 1

                            DGV_Data.Rows.Add(1)
                            DGV_Data.Rows(i).Cells(cellNoTransaksi).Value = .Rows(i).Item("no_transaksi")
                            DGV_Data.Rows(i).Cells(cellKodeBarang).Value = .Rows(i).Item("kode_barang")
                            DGV_Data.Rows(i).Cells(cellNama).Value = .Rows(i).Item("nama")
                            DGV_Data.Rows(i).Cells(cellFormula).Value = .Rows(i).Item("no_formula")
                            DGV_Data.Rows(i).Cells(cellTanggal).Value = Format(.Rows(i).Item("tanggal_awal"), "dd MMM yyyy") & " - " & Format(.Rows(i).Item("tanggal_akhir"), "dd MMM yyyy")
                            If General_Class.CekNULL(.Rows(i).Item("keterangan")) = "" Then
                                DGV_Data.Rows(i).Cells(cellKeterangan).Value = "-"
                            Else
                                DGV_Data.Rows(i).Cells(cellKeterangan).Value = .Rows(i).Item("keterangan")
                            End If


                            If .Rows(i).Item("flag_formula_permanen") = "Y" Then
                                DGV_Data.Rows(i).Cells(cellFlagPermanen).Value = "Permanen"
                            ElseIf .Rows(i).Item("flag_formula_permanen") = "T" Then
                                DGV_Data.Rows(i).Cells(cellFlagPermanen).Value = "Sementara"
                            Else
                                DGV_Data.Rows(i).Cells(cellFlagPermanen).Value = "Terjadi kesalahan"
                            End If


                            If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then
                                DGV_Data.Rows(i).DefaultCellStyle.BackColor = Color.LightYellow
                            End If


                        Next



                    End If
                End With
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub kosong()

        txtFormulaAuto.Text = ""
        txtKeteranganFormula.Text = ""
        TxtSchedule_Deskripsi.Text = ""

        RbPermanen.Checked = False
        RbSementara.Checked = False

        DtpSchedule_DateStart.ResetText()
        DtpSchedule_DateEnd.ResetText()

        get_Formula_default_by_Transaksi()
        loadDataScheduleFormulator()

    End Sub

    Private Sub get_Formula_default_by_Transaksi()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            SQL = $"

         SELECT DISTINCT ISNULL(f.No_Faktur, '') AS kode_formula,

                                     x.kode_barang_inq,
                                     x.kode_perusahaan
                     FROM barang x
                              INNER JOIN emI_group_jenis y
                                         ON x.kode_perusahaan = y.kode_perusahaan AND x.id_group_jenis = y.id_group_jenis
                              OUTER APPLY (SELECT TOP 1 c.No_Faktur, flag_invalid
                                           FROM N_EMI_Transaksi_Formulator_Binding a
                                                    INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail b
                                                               ON a.Kode_Perusahaan = b.Kode_Perusahaan
                                                                   AND a.No_Faktur = b.No_Faktur
                                                    INNER JOIN Emi_Transaksi_Formulator c
                                                               ON b.Kode_Perusahaan = c.Kode_Perusahaan
                                                                   AND b.No_Formulator = c.No_Faktur
                                                                   AND c.Status IS NULL
                                                                   AND c.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                                                                   and No_Prioritas = 1
                                                                    and c.Flag_Deprecated_Binding is null
                                           WHERE a.Status IS NULL
                                             AND a.Flag_Validasi_Main = 'Y'
                                             AND a.Kode_Perusahaan = x.kode_perusahaan
                                             AND a.Kode_Barang = x.kode_barang_inq
                                             
                                            and a.kode_perusahaan = '{KodePerusahaan}'
                                           
                                           ORDER BY a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC) f
                     WHERE (y.Flag_Finished_Good = 'Y' OR y.Flag_Semi_FG = 'Y' and flag_invalid is null)
                    and    x.kode_barang = '{txtKdBrng.Text}'
"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Do
                        txtFormulaDefault.Text = Dr("kode_formula")
                    Loop While Dr.Read
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Terjadi kesalahan, formula default tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtFormulaAuto.TextChanged
        If txtFormulaAuto.Text.Length >= 3 Then
            If txtFormulaAuto.Text.Trim.Length = 0 Then
                LvPilihBarang_DataBarang.Visible = False : Exit Sub
            Else

                LvPilihBarang_DataBarang.Visible = True
            End If

            If txtFormulaAuto.Text.Trim.Length = 0 Then
                LvPilihBarang_DataBarang.Visible = False : Exit Sub
            Else
                LvPilihBarang_DataBarang.Visible = True
            End If


            Try
                OpenConn()

                LvPilihBarang_DataBarang.Items.Clear()



                SQL = $"

                    select
                    No_Faktur,
                    case 
                    when flag_bypass_trial = 'Y' then 'Bypass Tanpa Trial'
                    when Flag_Bypass_Trial is null and Flag_Lanjut_Trial_Kitchen = 'B' and Flag_Selesai_Trial_Kitchen = 'B' 
                    and Flag_Lanjut_Trial_Produksi = 'B' and Flag_Selesai_Trial_Produksi = 'B' then 'Bypass Tanpa Trial'
                    when Flag_Bypass_Trial_Produksi_On_Process = 'Y' then 'Bypass Trial Produksi Berjalan'
                    else 'Normal 1' end as Status_Bypass,
                    case 
                    when flag_bypass_trial = 'Y'  then Keterangan_Bypass_Trial
                    when Flag_Bypass_Trial is null and Flag_Lanjut_Trial_Kitchen = 'B' and Flag_Selesai_Trial_Kitchen = 'B' 
                    and Flag_Lanjut_Trial_Produksi = 'B' and Flag_Selesai_Trial_Produksi = 'B' then Keterangan_Bypass_Trial
                    when Flag_Bypass_Trial_Produksi_On_Process = 'Y' then Keterangan_Bypass_Trial_Produksi_On_Process
                    else 'Normal 2' end as Keterangan_Bypass
                    From Emi_Transaksi_Formulator where Flag_Validasi_Formula_Produksi_BOD = 'Y' and status is null
                    and Flag_Deprecated_Binding is null
                    and kode_barang = '{txtKdBrng.Text}'
                    and no_faktur like '%{txtFormulaAuto.Text}%'

                "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Do

                            Dim Lv As ListViewItem
                            Lv = LvPilihBarang_DataBarang.Items.Add(Dr("no_faktur"))
                            Lv.SubItems.Add(Dr("Keterangan_Bypass"))
                            Lv.SubItems.Add(Dr("Status_Bypass"))
                            LvPilihBarang_DataBarang.Location = New Point(208, 174)

                        Loop While Dr.Read

                    End If
                End Using


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            LvPilihBarang_DataBarang.Visible = False
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFormulaAuto.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub
            LvPilihBarang_DataBarang.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFormulaAuto.KeyPress
        If e.KeyChar = Chr(13) Then DtpSchedule_DateStart.Focus()
    End Sub

    Private Sub LvPilihBarang_DataBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles LvPilihBarang_DataBarang.KeyDown
        If e.KeyCode = Keys.Enter Then LvPilihBarang_DataBarang_DoubleClick(LvPilihBarang_DataBarang, e)
    End Sub

    Private Sub LvPilihBarang_DataBarang_DoubleClick(sender As Object, e As EventArgs) Handles LvPilihBarang_DataBarang.DoubleClick
        If LvPilihBarang_DataBarang.Items.Count = 0 Or LvPilihBarang_DataBarang.FocusedItem.Index = -1 Then Exit Sub

        Dim kode_formula As String = LvPilihBarang_DataBarang.FocusedItem.SubItems(0).Text
        Dim keterangan As String = LvPilihBarang_DataBarang.FocusedItem.SubItems(2).Text
        Dim status As String = LvPilihBarang_DataBarang.FocusedItem.SubItems(1).Text

        txtKeteranganFormula.Text = keterangan
        txtFormulaAuto.Text = kode_formula

        LvPilihBarang_DataBarang.Visible = False

        DtpSchedule_DateStart.Focus()
    End Sub

    Private Sub BtnPilihBarang_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPilihBarang_Simpan.Click

        If txtFormulaAuto.Text.Trim.Length = 0 Then
            MessageBox.Show("Formula harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        If DtpSchedule_DateEnd.Value.Date < DtpSchedule_DateStart.Value.Date Then
            MessageBox.Show("Gagal! Tanggal selesai  tidak boleh lebih kecil dari tanggal mulai (Start)." & Environment.NewLine &
                    "Awal: " & Format(DtpSchedule_DateStart.Value, "dd MMM yyyy") & Environment.NewLine &
                    "Sampai: " & Format(DtpSchedule_DateEnd.Value, "dd MMM yyyy"),
                   Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)

            ' Balikin otomatis tanggal end-nya disamain dengan start biar user gak salah input
            DtpSchedule_DateEnd.Value = DtpSchedule_DateStart.Value
            Exit Sub
        End If

        If RbPermanen.Checked = False And RbSementara.Checked = False Then
            MessageBox.Show("Status formula harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            RbPermanen.Focus()
            Exit Sub
        End If

        If DtpSchedule_DateStart.Value.Month <> DtpSchedule_DateEnd.Value.Month OrElse
   DtpSchedule_DateStart.Value.Year <> DtpSchedule_DateEnd.Value.Year Then
            MessageBox.Show("Gagal! Rentang schedule tidak boleh melewati atau berbeda bulan." & Environment.NewLine &
                    "Tanggal Awal dan Tanggal Akhir harus berada di bulan dan tahun yang sama.",
                   Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            DtpSchedule_DateEnd.Value = DtpSchedule_DateStart.Value
            Exit Sub
        End If

        ' Ambil teks dari TextBox (misal nama TextBox-nya: TxtTargetPeriod)
        Dim periodText As String = txtBulanTahun.Text.Trim()

        Dim targetDate As DateTime

        If DtpSchedule_DateStart.Value.Date < DateTime.Today Then
            MessageBox.Show("Gagal! Tanggal awal schedule tidak boleh kurang dari tanggal hari ini.",
                    Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)

            ' Kembalikan otomatis tanggalnya ke hari ini biar user gak salah input
            DtpSchedule_DateStart.Value = DateTime.Today
            Exit Sub
        End If

    
        If DtpSchedule_DateStart.Value.Date < DateTime.Today Then
            MessageBox.Show("Gagal! Tanggal awal schedule tidak boleh kurang dari tanggal hari ini.",
                    Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            DtpSchedule_DateStart.Value = DateTime.Today
            Exit Sub
        End If

        If DtpSchedule_DateEnd.Value.Date < DtpSchedule_DateStart.Value.Date Then
            MessageBox.Show("Gagal! Tanggal selesai tidak boleh lebih kecil dari tanggal mulai." & Environment.NewLine &
                    "Silakan cek kembali rentang tanggal yang Anda masukkan.",
                    Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)


            DtpSchedule_DateEnd.Value = DtpSchedule_DateStart.Value
            Exit Sub
        End If



        If DateTime.TryParseExact(periodText, "MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, targetDate) Then


            Dim dtpMonth As Integer = DtpSchedule_DateStart.Value.Month
            Dim dtpYear As Integer = DtpSchedule_DateStart.Value.Year

            Dim targetMonth As Integer = targetDate.Month
            Dim targetYear As Integer = targetDate.Year


            If dtpMonth <> targetMonth OrElse dtpYear <> targetYear Then
                ' Jika tidak sama, munculkan pesan dan batalkan proses
                MessageBox.Show("Gagal, Formula tidak bisa di setting beda bulan! Harus berada pada bulan ${targetMonth} dan tahun ${targetYear}.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                DtpSchedule_DateStart.Focus()
                Exit Sub
            End If

        Else

            MessageBox.Show("Terjadi kesalahan pada format tanggal harus Bulan/Tahun", Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()



            SQL = $"
                select  status, No_Transaksi From N_EMI_Transaksi_Schedule_Formulator a
                where a.Kode_Perusahaan = '{KodePerusahaan}' and Kode_Barang = '{txtKdBrng.Text}' and
                a.Tanggal_Awal <= '{Format(DtpSchedule_DateEnd.Value, "yyyy-MM-dd")}' and a.tanggal_akhir >= '{Format(DtpSchedule_DateStart.Value, "yyyy-MM-dd")}'
                and a.status is null
                "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Do
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Gagal, data pada rentang tanggal:" & Environment.NewLine &
                "Awal: " & Format(DtpSchedule_DateStart.Value, "dd MMM yyyy") & Environment.NewLine &
                "Sampai: " & Format(DtpSchedule_DateEnd.Value, "dd MMM yyyy") & Environment.NewLine &
                "sudah pernah di simpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Exit Sub
                    Loop While Dr.Read

                End If
            End Using


            Dim statusFormula As String


            If RbPermanen.Checked = True Then
                statusFormula = "Y"
            ElseIf RbSementara.Checked = True Then
                statusFormula = "T"
            Else
                CloseTrans()
                CloseConn()
                MessageBox.Show("Silahkan pilih dulu status formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If




            SQL = $"
            insert into N_EMI_Transaksi_Schedule_Formulator
            (Kode_Perusahaan, No_Transaksi,id_user, Tanggal, Jam, No_Formula,kode_stock_owner, Kode_Barang, Tanggal_Awal, Tanggal_Akhir, Keterangan, Flag_Formula_Permanen)
            values('{KodePerusahaan}', '{Faktur}','{UserID}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}' , '{txtFormulaAuto.Text.Trim}' ,
            '{txtStcokOwner.Text}','{txtKdBrng.Text}', '{Format(DtpSchedule_DateStart.Value, "yyyy-MM-dd")}', '{Format(DtpSchedule_DateEnd.Value, "yyyy-MM-dd")}',
            '{TxtSchedule_Deskripsi.Text.Trim}', '{statusFormula}'
            )
            "

            ExecuteTrans(SQL)


            SQL = $"
             UPDATE a
                SET a.kode_formula = '{txtFormulaAuto.Text}',
                    a.No_Schedule_Formulator = '{Faktur}'
                FROM N_EMI_Production_Plan_Schedule_Detail a
                INNER JOIN N_EMI_Production_Plan_Schedule b 
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Transaksi
                WHERE b.Status IS NULL
                  AND a.Kode_Perusahaan = '{KodePerusahaan}'
                  AND a.Kode_Barang = '{txtKdBrng.Text}'
                  AND b.bulan = '{bulanNow}'
                  AND b.Tahun = '{tahunNow}'
                  AND a.Tanggal_Start BETWEEN '{Format(DtpSchedule_DateStart.Value, "yyyy-MM-dd")}' 
                                          AND '{Format(DtpSchedule_DateEnd.Value, "yyyy-MM-dd")}'
"

            ' Eksekusi sekali saja langsung beres untuk semua baris yang cocok
            ExecuteTrans(SQL)





            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()



            MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()

            N_EMI_Schedule_Formulator.mustUpdate = True


        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub DGV_Data_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data.CellContentClick
        If e.ColumnIndex = cellButton Then

            Dim index As Integer = e.RowIndex

            get_isi_listview(index)
            get_jam()


            Dim pertanyaan As String = MessageBox.Show("Yakin Ingin membatalkan formula " & LvFormula & " ?", "Formulator", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If pertanyaan = vbNo Then Exit Sub

            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction





                SQL = $"
                    select  No_Transaksi,status from N_EMI_Transaksi_Schedule_Formulator a where 
                    a.Kode_Perusahaan = '{KodePerusahaan}' and a.No_Transaksi = '{LvNoTransaksi}'
                
                "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Do
                            If General_Class.CekNULL(Dr("status")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data sudah pernah dibatalkan sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Loop While Dr.Read
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data ditemukan, silahkan refresh dan coba lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using









                SQL = $"

                   
                INSERT INTO N_EMI_Production_Plan_Schedule_Log 
                (Kode_Perusahaan, No_Transaksi, User_Id, Tanggal, Jam, Tanggal_Perhari, Bulan,
                Tahun, Kode_Formula, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, No_Urut_Detail, 
                Tanggal_Start, Tanggal_End, Keterangan, No_Schedule_Formulator)
                select 
                Kode_Perusahaan, No_Transaksi, '{UserID}','{Format(tgl_skg, "yyyy-MM-dd")}' , '{Format(tgl_skg, "HH:mm:ss")}', Tanggal_Full, '{bulanNow}',
                '{tahunNow}', Kode_Formula, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, no_urut, 
                Tanggal_Start, Tanggal_End, Keterangan, No_Schedule_Formulator
                From N_EMI_Production_Plan_Schedule_Detail where no_schedule_formulator = '{LvNoTransaksi}' and Kode_Perusahaan = '{KodePerusahaan}'

                    "


                ExecuteTrans(SQL)



                SQL = $"

                   update N_EMI_Production_Plan_Schedule_Detail set Kode_Formula = null, no_schedule_formulator = null 
                     where Kode_Perusahaan = '{KodePerusahaan}' and no_schedule_formulator = '{LvNoTransaksi}'
                "

                ExecuteTrans(SQL)


                SQL = $"
                    update N_EMI_Transaksi_Schedule_Formulator set Status = 'Y',
                    Tanggal_Batal = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Batal = '{Format(tgl_skg, "HH:mm:ss")}', IdUser_Batal = '{UserID}'
                    where Kode_Perusahaan = '{KodePerusahaan}' and No_Transaksi = '{LvNoTransaksi}'


                
                "

                ExecuteTrans(SQL)







                Cmd.Transaction.Commit()
                CloseTrans()
                CloseConn()

                MessageBox.Show("Data berhasil dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                kosong()
                N_EMI_Schedule_Formulator.mustUpdate = True

            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



        End If
    End Sub

End Class
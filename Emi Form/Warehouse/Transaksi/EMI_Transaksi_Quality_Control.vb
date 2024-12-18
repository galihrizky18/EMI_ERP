Imports System.Reflection
Imports System.Windows.Forms.VisualStyles
Imports System.Xml


Public Class EMI_Transaksi_Quality_Control
    Dim arrcari, arrJenisQC As New ArrayList

    Dim Jenis = "Master_Quality_Control"
    Dim id_qc As String
    Dim warna As String
    Dim SudahLoadWarna As Boolean = False

    Public noQc As String

    'Array 2 dimensi menggunakan list
    Dim arr2Switch, arrSwitch As New List(Of List(Of String))

    Dim LvIDKategori As String
    Dim LvNmKategori As String
    Dim LvIDUji As String
    Dim LvKodeUji As String
    Dim LvNmUji As String
    Dim LvSatuan As String
    Dim LvValue As String
    Dim LvCmbValue As String
    Dim LvMinAwal As String
    Dim LvMaxAwal As String
    Dim LvMinHasil As String
    Dim LvMaxHasil As String
    Dim LvWarna As String
    Dim LvUrut As String
    Dim LvIDWarna As String
    Dim LvKeterangan As String

    Dim CellIDKategori As Integer = 0
    Dim CellNmKategori As Integer = 1
    Dim CellIDUji As Integer = 2
    Dim CellKodeUji As Integer = 3
    Dim CellNmUji As Integer = 4
    Dim CellSatuan As Integer = 5
    Dim CellValue As Integer = 6
    Dim CellCmbValue As Integer = 7
    Dim CellMinAwal As Integer = 8
    Dim CellMaxAwal As Integer = 9
    Dim CellMinHasil As Integer = 10
    Dim CellMaxHasil As Integer = 11
    Dim CellWarna As Integer = 12
    Dim CellUrut As Integer = 13
    Dim CellIDWarna As Integer = 14
    Dim CellKeterangan As Integer = 15



    Public Sub Get_Isi_Listview(ByVal No_Index As Integer, dgv As DataGridView)

        LvIDKategori = dgv.Rows(No_Index).Cells(CellIDKategori).Value.ToString
        LvNmKategori = dgv.Rows(No_Index).Cells(CellNmKategori).Value.ToString
        LvIDUji = dgv.Rows(No_Index).Cells(CellIDUji).Value.ToString
        LvKodeUji = dgv.Rows(No_Index).Cells(CellKodeUji).Value.ToString
        LvNmUji = dgv.Rows(No_Index).Cells(CellNmUji).Value.ToString
        LvSatuan = dgv.Rows(No_Index).Cells(CellSatuan).Value.ToString
        LvValue = dgv.Rows(No_Index).Cells(CellValue).Value.ToString
        'LvCmbValue = dgv.Rows(No_Index).Cells(CellCmbValue).Value.ToString
        LvMinAwal = dgv.Rows(No_Index).Cells(CellMinAwal).Value.ToString
        LvMaxAwal = dgv.Rows(No_Index).Cells(CellMaxAwal).Value.ToString
        LvMinHasil = dgv.Rows(No_Index).Cells(CellMinHasil).Value.ToString
        LvMaxHasil = dgv.Rows(No_Index).Cells(CellMaxHasil).Value.ToString
        LvWarna = dgv.Rows(No_Index).Cells(CellWarna).Value.ToString
        LvUrut = dgv.Rows(No_Index).Cells(CellUrut).Value.ToString
        LvIDWarna = dgv.Rows(No_Index).Cells(CellIDWarna).Value.ToString
        LvKeterangan = dgv.Rows(No_Index).Cells(CellKeterangan).Value.ToString

    End Sub



    Private Sub kosong()

        Cmb_tidaksesuai.Items.Clear()
        Cmb_tidaksesuai.Items.Add("DITERIMA")
        Cmb_tidaksesuai.Items.Add("REFRAKSI")
        Cmb_tidaksesuai.Items.Add("TOLAK SEBAGIAN")
        Cmb_tidaksesuai.Items.Add("TOLAK SELURUH")

        txtNoFaktur.Text = ""
        txtNoFaktur.Text = noQc
        txtNoFaktur.Focus()

        TxtNoLoading.Text = ""
        TxtNamaBarang.Text = ""
        TxtKdBarang.Text = ""
        TxtNamasup.Text = ""
        TxtNoPlat.Text = ""
        TxtJenisQC.Text = ""

        DGV_Data_QC.Rows.Clear()
        Dgv_QC_Lab.Rows.Clear()

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Text = Base_Language.Lang_Global_Hapus

        Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

        warna = String.Empty
        Pnl_Sesuai.BackColor = Color.FromArgb(158, 158, 158)
        Pnl_TidakSesuai.BackColor = Color.FromArgb(158, 158, 158)
        PnlBelumSelesai.BackColor = Color.FromArgb(158, 158, 158)

        SudahLoadWarna = False


        Tampil()

        Load_QC("lapangan")
        Load_QC("lab")


    End Sub

    Private Sub Hasil_QC()
        Dim Data_Kosong As Integer = 0
        Dim Data_Tidak_sesuai As Integer = 0

        For index = 0 To DGV_Data_QC.Rows.Count - 1
            Get_Isi_Listview(index, DGV_Data_QC)

            If LvIDWarna = "" Then
                Data_Kosong += 1
            End If

            If LvIDWarna.ToUpper = "MERAH" Then
                Data_Tidak_sesuai += 1
            End If
        Next

        For index = 0 To Dgv_QC_Lab.Rows.Count - 1
            Get_Isi_Listview(index, Dgv_QC_Lab)

            If LvIDWarna = "" Then
                Data_Kosong += 1
            End If

            If LvIDWarna.ToUpper = "MERAH" Then
                Data_Tidak_sesuai += 1
            End If
        Next

        If Data_Kosong > 0 Then

            PnlBelumSelesai.BackColor = Color.FromArgb(173, 216, 230)
            Pnl_TidakSesuai.BackColor = Color.FromArgb(158, 158, 158)
            Pnl_Sesuai.BackColor = Color.FromArgb(158, 158, 158)
            warna = "PUTIH"
            Cmb_tidaksesuai.Enabled = False
        ElseIf Data_Tidak_sesuai > 0 Then
            PnlBelumSelesai.BackColor = Color.FromArgb(158, 158, 158)
            Pnl_TidakSesuai.BackColor = Color.FromArgb(255, 225, 53)
            Pnl_Sesuai.BackColor = Color.FromArgb(158, 158, 158)
            warna = "KUNING"
            Cmb_tidaksesuai.Enabled = True
        Else
            PnlBelumSelesai.BackColor = Color.FromArgb(158, 158, 158)
            Pnl_TidakSesuai.BackColor = Color.FromArgb(158, 158, 158)
            Pnl_Sesuai.BackColor = Color.FromArgb(144, 238, 144)
            warna = "HIJAU"
            Cmb_tidaksesuai.Enabled = False
        End If

    End Sub
    Private Sub Tampil()
        Try
            OpenConn()
            SQL = "Select a.No_Faktur,b.Kode_Supplier,d.Nama,b.driver As Supir,b.No_Plat As Plat_Number,b.No_SJ,a.Tanggal, "
            SQL = SQL & "a.Kode_Barang, c.nama As nama_barang, a.step, a.No_Fak_Loading_Barang, a.Jenis_QC, a.warna "
            SQL = SQL & "From EMI_Hasil_Quality_Control a, emi_pembelian_loading b, barang c, Suppliers d Where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Fak_Loading_Barang = b.no_faktur And "
            SQL = SQL & "a.status Is null And b.status Is null And a.Kode_Perusahaan = c.Kode_Perusahaan And "
            SQL = SQL & "a.Kode_Barang = c.Kode_Barang And a.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_Supplier=d.Kode_Supplier "
            SQL = SQL & "And a.kode_Perusahaan='" & KodePerusahaan & "' and a.no_faktur='" & txtNoFaktur.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TxtNoLoading.Text = dr("No_Fak_Loading_Barang")
                    TxtNoPlat.Text = dr("Plat_Number")
                    TxtNamaBarang.Text = dr("nama_barang")
                    TxtNamasup.Text = dr("Nama")
                    TxtKdBarang.Text = dr("Kode_Barang")
                    TxtJenisQC.Text = dr("Jenis_QC")
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub
    Private Sub Load_QC(ByVal filter As String)
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = "Hasil - Quality Control"

            SQL = "; with cte as( "
            SQL = SQL & "Select  a.kode_perusahaan, a.no_faktur, b.Id_Kategori_Komponen, c.Keterangan As Kategori_Komponen, "
            SQL = SQL & "isnull(c.Flag_Input,'T') as Flag_Input, isnull(c.Flag_Option,'T') as Flag_Option, "
            SQL = SQL & "isnull(c.Flag_Slider,'T') as Flag_Slider, a.id_quality_control,b.Kode_Uji, b.Keterangan, b.satuan, "
            SQL = SQL & "b.Flag_Tampil_Android, b.Flag_Tampil_Dekstop, value_kode_uji, no_urut, a.Warna "
            SQL = SQL & ",isnull(d.min_range,0) as min_range, isnull(d.Max_Range,0) as Max_Range, "
            SQL = SQL & "isnull(d.Min_Nilai_Seharusnya, 0) Min_Nilai_Seharusnya, isnull(d.Max_Nilai_Seharusnya,0) Max_Nilai_Seharusnya, "
            SQL = SQL & "a.keterangan as Keterangan_QC "
            SQL = SQL & "From EMI_Hasil_Detail_Quality_Control a, EMI_Quality_Control b, "
            SQL = SQL & "EMI_Kategori_Komponen c, EMI_Quality_Control_PerBarang d "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Id_Quality_Control = b.Id_QC_Formula "
            SQL = SQL & "And b.Kode_Perusahaan=c.Kode_Perusahaan And b.Id_Kategori_Komponen=c.Id_Kategori_Komponen "
            SQL = SQL & "And a.Kode_Perusahaan=d.Kode_Perusahaan And a.Id_Quality_Control=d.Id_QC_Formula "
            SQL = SQL & "And no_faktur='" & txtNoFaktur.Text & "' and d.Kode_barang='" & TxtKdBarang.Text & "' "

            SQL = SQL & "union all "

            SQL = SQL & "Select a.kode_perusahaan, a.no_faktur, b.Id_Kategori_Komponen, c.Keterangan As Kategori_Komponen, "
            SQL = SQL & "isnull(c.Flag_Input,'T') as Flag_Input, isnull(c.Flag_Option,'T') as Flag_Option, "
            SQL = SQL & "isnull(c.Flag_Slider,'T') as Flag_Slider, a.id_quality_control,b.Kode_Uji, b.Keterangan, b.satuan, "
            SQL = SQL & "b.Flag_Tampil_Android, b.Flag_Tampil_Dekstop, value_kode_uji, no_urut, a.Warna "
            SQL = SQL & ",isnull(d.min_range,0) as min_range, isnull(d.Max_Range,0) as Max_Range, "
            SQL = SQL & "isnull(d.Min_Nilai_Seharusnya, 0) Min_Nilai_Seharusnya, isnull(d.Max_Nilai_Seharusnya,0) Max_Nilai_Seharusnya, "
            SQL = SQL & "a.keterangan as Keterangan_QC "
            SQL = SQL & "From EMI_Hasil_Detail_Switch_QC a, EMI_Quality_Control b, "
            SQL = SQL & "EMI_Kategori_Komponen c, EMI_Quality_Control_PerBarang d  "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Id_Quality_Control = b.Id_QC_Formula "
            SQL = SQL & "And b.Kode_Perusahaan=c.Kode_Perusahaan And b.Id_Kategori_Komponen=c.Id_Kategori_Komponen "
            SQL = SQL & "And a.Kode_Perusahaan=d.Kode_Perusahaan And a.Id_Quality_Control=d.Id_QC_Formula "
            SQL = SQL & "And no_faktur ='" & txtNoFaktur.Text & "' and d.Kode_barang='" & TxtKdBarang.Text & "' "

            SQL = SQL & ") select * from cte "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "

            'JANGAN LUPA DI UNCOMMENT
            'SQL = SQL & "and flag_sudah_qc_dekstop is null "

            If filter = "lapangan" Then
                SQL = SQL & "And flag_tampil_android = 'Y' "
            ElseIf filter = "lab" Then
                SQL = SQL & "And Flag_Tampil_Dekstop = 'Y' "
            End If

            SQL = SQL & "order by keterangan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        If filter = "lapangan" Then
                            arrSwitch.Clear()
                            For i As Integer = 0 To .Rows.Count - 1

                                DGV_Data_QC.Rows.Add(1)

                                Dim subArr As New List(Of String)

                                DGV_Data_QC.Rows(i).Cells(CellIDKategori).Value = .Rows(i).Item("Id_Kategori_Komponen")
                                DGV_Data_QC.Rows(i).Cells(CellNmKategori).Value = .Rows(i).Item("Kategori_Komponen")
                                DGV_Data_QC.Rows(i).Cells(CellIDUji).Value = .Rows(i).Item("id_quality_control")
                                DGV_Data_QC.Rows(i).Cells(CellKodeUji).Value = .Rows(i).Item("Kode_Uji")
                                DGV_Data_QC.Rows(i).Cells(CellNmUji).Value = .Rows(i).Item("Keterangan")
                                DGV_Data_QC.Rows(i).Cells(CellSatuan).Value = .Rows(i).Item("satuan")

                                If .Rows(i).Item("Flag_Option") = "T" Then
                                    DGV_Data_QC.Rows(i).Cells(CellValue).Value = .Rows(i).Item("value_kode_uji")
                                    DGV_Data_QC.Rows(i).Cells(CellCmbValue).Value = ""

                                    DGV_Data_QC.Rows(i).Cells(CellValue).Style.BackColor = Color.LightGray
                                    DGV_Data_QC.Rows(i).Cells(CellCmbValue).Style.BackColor = Color.White

                                    DGV_Data_QC.Rows(i).Cells(CellValue).ReadOnly = True
                                    DGV_Data_QC.Rows(i).Cells(CellCmbValue).ReadOnly = True

                                    subArr.Add("")
                                Else
                                    DGV_Data_QC.Rows(i).Cells(CellValue).Value = ""

                                    Dim dgvCmbValueSwitch As DataGridViewComboBoxCell
                                    dgvCmbValueSwitch = DGV_Data_QC.Rows(i).Cells(CellCmbValue)
                                    dgvCmbValueSwitch.Items.Clear()

                                    Dim indexValue As Integer = 0
                                    Dim inddd As Integer = 0
                                    SQL = "select a.Id_Switch, a.Keterangan from EMI_Switch a, EMI_Hasil_Detail_Switch_QC b "
                                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_QC_Formula = b.Id_Quality_Control "
                                    SQL = SQL & "and a.id_qc_formula = '" & .Rows(i).Item("id_quality_control") & "' and b.no_faktur = '" & txtNoFaktur.Text & "' "
                                    Using dr2 = OpenTrans(SQL)
                                        Do While dr2.Read
                                            dgvCmbValueSwitch.Items.Add(dr2("Keterangan")) : subArr.Add(dr2("id_switch"))

                                            If .Rows(i).Item("value_kode_uji") = dr2("id_switch") Then
                                                indexValue = inddd
                                            End If

                                            inddd += 1
                                        Loop
                                    End Using

                                    dgvCmbValueSwitch.Value = dgvCmbValueSwitch.Items(indexValue)

                                    DGV_Data_QC.Rows(i).Cells(CellValue).Style.BackColor = Color.White
                                    DGV_Data_QC.Rows(i).Cells(CellCmbValue).Style.BackColor = Color.LightGray

                                    DGV_Data_QC.Rows(i).Cells(CellValue).ReadOnly = True
                                    DGV_Data_QC.Rows(i).Cells(CellCmbValue).ReadOnly = True
                                End If

                                arrSwitch.Add(subArr)

                                If .Rows(i).Item("Flag_Slider") = "Y" Then
                                    DGV_Data_QC.Rows(i).Cells(CellMinAwal).Value = .Rows(i).Item("Min_Range")
                                    DGV_Data_QC.Rows(i).Cells(CellMaxAwal).Value = .Rows(i).Item("Max_Range")
                                    DGV_Data_QC.Rows(i).Cells(CellMinHasil).Value = .Rows(i).Item("Min_Nilai_Seharusnya")
                                    DGV_Data_QC.Rows(i).Cells(CellMaxHasil).Value = .Rows(i).Item("Max_Nilai_Seharusnya")
                                Else
                                    DGV_Data_QC.Rows(i).Cells(CellMinAwal).Value = ""
                                    DGV_Data_QC.Rows(i).Cells(CellMaxAwal).Value = ""
                                    DGV_Data_QC.Rows(i).Cells(CellMinHasil).Value = ""
                                    DGV_Data_QC.Rows(i).Cells(CellMaxHasil).Value = ""
                                End If

                                If .Rows(i).Item("Warna").ToString.ToUpper = "HIJAU" Then
                                    DGV_Data_QC.Rows(i).Cells(CellWarna).Style.BackColor = Color.FromArgb(144, 238, 144)
                                ElseIf .Rows(i).Item("Warna").ToString.ToUpper = "MERAH" Then
                                    DGV_Data_QC.Rows(i).Cells(CellWarna).Style.BackColor = Color.FromArgb(254, 46, 46)
                                Else
                                    DGV_Data_QC.Rows(i).Cells(CellWarna).Style.BackColor = Color.FromArgb(255, 255, 255)
                                End If

                                DGV_Data_QC.Rows(i).Cells(CellWarna).Value = ""
                                DGV_Data_QC.Rows(i).Cells(CellIDWarna).Value = .Rows(i).Item("Warna")
                                DGV_Data_QC.Rows(i).Cells(CellUrut).Value = .Rows(i).Item("no_urut")


                                DGV_Data_QC.Rows(i).Cells(CellKeterangan).Value = .Rows(i).Item("Keterangan_QC")


                            Next

                        Else
                            '===========================
                            arr2Switch.Clear()
                            For i As Integer = 0 To .Rows.Count - 1

                                Dgv_QC_Lab.Rows.Add(1)

                                Dim subArr As New List(Of String)

                                Dgv_QC_Lab.Rows(i).Cells(CellIDKategori).Value = .Rows(i).Item("Id_Kategori_Komponen")
                                Dgv_QC_Lab.Rows(i).Cells(CellNmKategori).Value = .Rows(i).Item("Kategori_Komponen")
                                Dgv_QC_Lab.Rows(i).Cells(CellIDUji).Value = .Rows(i).Item("id_quality_control")
                                Dgv_QC_Lab.Rows(i).Cells(CellKodeUji).Value = .Rows(i).Item("Kode_Uji")
                                Dgv_QC_Lab.Rows(i).Cells(CellNmUji).Value = .Rows(i).Item("Keterangan")
                                Dgv_QC_Lab.Rows(i).Cells(CellSatuan).Value = .Rows(i).Item("satuan")

                                If .Rows(i).Item("Flag_Option") = "T" Then
                                    Dgv_QC_Lab.Rows(i).Cells(CellValue).Value = ""
                                    Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).Value = ""

                                    Dgv_QC_Lab.Rows(i).Cells(CellValue).Style.BackColor = Color.LightGray
                                    Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).Style.BackColor = Color.White

                                    Dgv_QC_Lab.Rows(i).Cells(CellValue).ReadOnly = False
                                    Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).ReadOnly = True

                                    subArr.Add("")
                                Else
                                    Dgv_QC_Lab.Rows(i).Cells(CellValue).Value = ""

                                    Dim dgvCmbValueSwitch As DataGridViewComboBoxCell
                                    dgvCmbValueSwitch = Dgv_QC_Lab.Rows(i).Cells(CellCmbValue)
                                    dgvCmbValueSwitch.Items.Clear()

                                    Dim inddd As Integer = 0
                                    SQL = "select a.Id_Switch, a.Keterangan from EMI_Switch a, EMI_Hasil_Detail_Switch_QC b "
                                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_QC_Formula = b.Id_Quality_Control "
                                    SQL = SQL & "and a.id_qc_formula = '" & .Rows(i).Item("id_quality_control") & "' and b.no_faktur = '" & txtNoFaktur.Text & "' "
                                    Using dr2 = OpenTrans(SQL)
                                        Do While dr2.Read
                                            dgvCmbValueSwitch.Items.Add(dr2("Keterangan")) : subArr.Add(dr2("id_switch"))


                                            inddd += 1
                                        Loop
                                    End Using

                                    Dgv_QC_Lab.Rows(i).Cells(CellValue).Style.BackColor = Color.White
                                    Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).Style.BackColor = Color.LightGray

                                    Dgv_QC_Lab.Rows(i).Cells(CellValue).ReadOnly = True
                                    Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).ReadOnly = False
                                End If

                                arr2Switch.Add(subArr)

                                If .Rows(i).Item("Flag_Slider") = "Y" Then
                                    Dgv_QC_Lab.Rows(i).Cells(CellMinAwal).Value = .Rows(i).Item("Min_Range")
                                    Dgv_QC_Lab.Rows(i).Cells(CellMaxAwal).Value = .Rows(i).Item("Max_Range")
                                    Dgv_QC_Lab.Rows(i).Cells(CellMinHasil).Value = .Rows(i).Item("Min_Nilai_Seharusnya")
                                    Dgv_QC_Lab.Rows(i).Cells(CellMaxHasil).Value = .Rows(i).Item("Max_Nilai_Seharusnya")
                                Else
                                    Dgv_QC_Lab.Rows(i).Cells(CellMinAwal).Value = ""
                                    Dgv_QC_Lab.Rows(i).Cells(CellMaxAwal).Value = ""
                                    Dgv_QC_Lab.Rows(i).Cells(CellMinHasil).Value = ""
                                    Dgv_QC_Lab.Rows(i).Cells(CellMaxHasil).Value = ""
                                End If

                                If .Rows(i).Item("Warna").ToString.ToUpper = "HIJAU" Then
                                    Dgv_QC_Lab.Rows(i).Cells(CellWarna).Style.BackColor = Color.FromArgb(144, 238, 144)
                                ElseIf .Rows(i).Item("Warna").ToString.ToUpper = "MERAH" Then
                                    Dgv_QC_Lab.Rows(i).Cells(CellWarna).Style.BackColor = Color.FromArgb(254, 46, 46)
                                Else
                                    Dgv_QC_Lab.Rows(i).Cells(CellWarna).Style.BackColor = Color.FromArgb(255, 255, 255)
                                End If

                                Dgv_QC_Lab.Rows(i).Cells(CellWarna).Value = ""
                                Dgv_QC_Lab.Rows(i).Cells(CellIDWarna).Value = .Rows(i).Item("Warna")
                                Dgv_QC_Lab.Rows(i).Cells(CellUrut).Value = .Rows(i).Item("no_urut")
                                Dgv_QC_Lab.Rows(i).Cells(CellKeterangan).Value = ""

                            Next
                        End If

                    End If
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Hasil_QC()
    End Sub

    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If txtNoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNoFaktur.Focus() : Exit Sub

        ElseIf Dgv_QC_Lab.Rows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_QC_Lab.Focus() : Exit Sub
        End If

        If warna = "PUTIH" Then
            MessageBox.Show("Data Belum di Lengkapi . . ! ! ", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf warna = "KUNING" Then
            If Cmb_tidaksesuai.SelectedIndex = -1 Then
                MessageBox.Show("Perlakuan Data tidak Sesuai Harus di isi . . ! ! ", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If
        Dim Hasil As String = ""

        If warna = "HIJAU" Then
            Hasil = "DITERIMA"
        ElseIf warna = "KUNING" Then
            Hasil = Cmb_tidaksesuai.Text
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            For i As Integer = 0 To Dgv_QC_Lab.Rows.Count - 1
                Get_Isi_Listview(i, Dgv_QC_Lab)
                'cek apakah semua data sudah di isi

                Dim Flag_Slider As String = ""
                Dim Flag_Input As String = ""
                Dim Flag_Option As String = ""

                SQL = "select ISNULL(Flag_Slider,'T') as Flag_Slider, "
                SQL = SQL & "ISNULL(Flag_Option,'T') as Flag_Option, "
                SQL = SQL & "ISNULL(Flag_Input,'T') as Flag_Input from "
                SQL = SQL & "EMI_Kategori_Komponen where kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Kategori_Komponen='" & LvIDKategori & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Flag_slider = dr("Flag_Slider")
                        Flag_Input = dr("Flag_Input")
                        Flag_Option = dr("Flag_Option")
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                If Flag_Option = "T" Then

                    If LvValue = "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Parameter " & LvNmUji & " belum di isi!")
                        Exit Sub
                    End If

                    SQL = "update EMI_Hasil_Detail_Quality_Control set value_kode_uji = '" & LvValue & "', Warna = '" & LvIDWarna & "', Keterangan = '" & LvKeterangan & "' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and no_urut = '" & LvUrut & "' "
                    ExecuteTrans(SQL)

                Else
                    Dim comboBoxCell As DataGridViewComboBoxCell = CType(Dgv_QC_Lab.Rows(i).Cells(CellCmbValue), DataGridViewComboBoxCell)

                    If comboBoxCell.Value = "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Parameter " & LvNmUji & " belum di isi!")
                        Exit Sub
                    End If

                    Dim index As Integer = comboBoxCell.Items.IndexOf(comboBoxCell.Value)

                    Dim valuekodeuji As String = arr2Switch(i)(index).ToString

                    SQL = "update EMI_Hasil_Detail_Switch_QC set value_kode_uji = '" & valuekodeuji & "', Warna = '" & LvIDWarna & "', Keterangan = '" & LvKeterangan & "' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and no_urut = '" & LvUrut & "' "
                    ExecuteTrans(SQL)
                End If


            Next


            SQL = "update EMI_Hasil_Quality_Control set Flag_Sudah_Qc_Dekstop = 'Y', "
            SQL = SQL & "Warna='" & warna & "', Hasil='" & Hasil & "' where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & noQc & "' "
            ExecuteTrans(SQL)

            If warna = "KUNING" Then
                If Cmb_tidaksesuai.Text.ToUpper = "REFRAKSI" Then

                    SQL = "Select b.Flag_Refraksi, b.Harga_Refraksi, a.urut_oto, a.urut_Po, b.no_faktur from "
                    SQL = SQL & "EMI_Pembelian_Loading_detail a, EMI_Pembelian_PO_Detail b where "
                    SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.urut_Po = b.No_Urut "
                    SQL = SQL & "And a.no_faktur='" & TxtNoLoading.Text & "' and a.kode_barang='" & TxtKdBarang.Text & "' "
                    SQL = SQL & " and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    Using ds = BindingTrans(SQL)
                        With ds.Tables("MyTable")
                            For index = 0 To .Rows.Count - 1

                                If General_Class.CekNULL(.Rows(index).Item("Flag_Refraksi")) = "Y" Then

                                    SQL = "update EMI_Pembelian_Loading_detail set flag_refraksi='Y', "
                                    SQL = SQL & "Harga_Refraksi='" & .Rows(index).Item("Harga_Refraksi") & "' "
                                    SQL = SQL & "where No_faktur='" & TxtNoLoading.Text & "' "
                                    SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                    SQL = SQL & "and kode_barang='" & TxtKdBarang.Text & "'"
                                    SQL = SQL & "and urut_oto='" & .Rows(index).Item("urut_oto") & "'"
                                    ExecuteTrans(SQL)

                                Else

                                    SQL = "update EMI_Pembelian_Loading_detail set Flag_Permintaan_Refraksi='Y' "
                                    SQL = SQL & "where No_faktur='" & TxtNoLoading.Text & "' "
                                    SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                    SQL = SQL & "and kode_barang='" & TxtKdBarang.Text & "'"
                                    SQL = SQL & "and urut_oto='" & .Rows(index).Item("urut_oto") & "'"
                                    ExecuteTrans(SQL)

                                    SQL = "update EMI_Pembelian_PO_Detail set Flag_Permintaan_Refraksi='Y' "
                                    SQL = SQL & "where No_faktur='" & .Rows(index).Item("no_faktur") & "' "
                                    SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                    SQL = SQL & "and kode_barang='" & TxtKdBarang.Text & "'"
                                    SQL = SQL & "and No_Urut='" & .Rows(index).Item("urut_Po") & "'"
                                    ExecuteTrans(SQL)

                                End If
                            Next
                        End With
                    End Using
                ElseIf Cmb_tidaksesuai.Text.ToUpper = "TOLAK SEBAGIAN" Then

                    SQL = "update EMI_Pembelian_Loading_detail set Flag_Tolak_Sebagian='Y' "
                    SQL = SQL & "where No_faktur='" & TxtNoLoading.Text & "' "
                    SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and kode_barang='" & TxtKdBarang.Text & "'"
                    ExecuteTrans(SQL)

                ElseIf Cmb_tidaksesuai.Text.ToUpper = "TOLAK SELURUH" Then

                    SQL = "update EMI_Pembelian_Loading_detail set Flag_Tolak='Y' "
                    SQL = SQL & "where No_faktur='" & TxtNoLoading.Text & "' "
                    SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and kode_barang='" & TxtKdBarang.Text & "'"
                    ExecuteTrans(SQL)

                End If
            End If

            SQL = "update EMI_Pembelian_Loading_detail set Warna = '" & warna & "' "
            SQL = SQL & "where No_faktur='" & TxtNoLoading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and kode_barang='" & TxtKdBarang.Text & "'"
            ExecuteTrans(SQL)

            If TxtJenisQC.Text.Trim = "1" Then

                SQL = "update EMI_Pembelian_Loading_detail set flag_qc_pertama ='Y' "
                SQL = SQL & "where No_faktur='" & TxtNoLoading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and kode_barang='" & TxtKdBarang.Text & "'"
                ExecuteTrans(SQL)

                SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_detail where "
                SQL = SQL & "No_faktur='" & TxtNoLoading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and flag_qc_pertama is null "
                Using dr = OpenTrans(SQL)
                    If Not dr.Read Then
                        dr.Close()
                        SQL = "update EMI_Pembelian_Loading set flag_qc_pertama ='Y' "
                        SQL = SQL & "where No_faktur='" & TxtNoLoading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                        ExecuteTrans(SQL)

                    End If
                End Using
            End If


            Cmd.Transaction.Commit()
            MessageBox.Show("Data berhasil disimpan ", Judul, MessageBoxButtons.OK)
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        '=================
        '=     CETAK     =
        '=================
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select Kode_Perusahaan from View_Laporan_Hasil_QC where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Fak_Loading_Barang = '" & TxtNoLoading.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    'Dim CrDoc As New Rpt_Laporan_Hasil_QC
                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.RecordSelectionFormula = "{View_Laporan_Hasil_QC.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_Hasil_QC.No_Fak_Loading_Barang} = '" & TxtNoLoading.Text & "' and {View_Laporan_Hasil_QC.no_hsl_qc} = '" & noQc & "' "
                    '    .Text = "Bukti Hasil Quality Control"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    CrDoc = New Rpt_Laporan_Hasil_QC
                    kertas = "A4"

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{View_Laporan_Hasil_QC.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_Hasil_QC.No_Fak_Loading_Barang} = '" & TxtNoLoading.Text & "' and {View_Laporan_Hasil_QC.no_hsl_qc} = '" & noQc & "' "
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterName
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)
                Else
                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try









        kosong()
        EMI_Display_Quality_Control.kosong()
        'EMI_Display_Quality_Control.Btn_Refresh_Click(Btn_Simpan, e)
        Me.Close()
    End Sub

    Private Sub DGV_Data_QC_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If DGV_Data_QC.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DGV_Data_QC.CurrentRow.Index
        Dim currentCell = DGV_Data_QC.CurrentCellAddress.X

        Dim data = DGV_Data_QC.Rows(currentRow).Cells(currentCell)

        If currentCell = CellValue Then
            If Val(DGV_Data_QC.CurrentRow.Cells(CellValue).Value) < 0 Or IsNumeric(DGV_Data_QC.CurrentRow.Cells(CellValue).Value) = False Then
                DGV_Data_QC.CurrentRow.Cells(CellValue).Value = 0
            End If
        End If

    End Sub

    Private Sub DGV_Data_QC_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs)
        If e.RowIndex = 0 And e.ColumnIndex = 5 Then
            ' Melukis latar belakang default cell
            e.PaintBackground(e.ClipBounds, True)

            ' Menggunakan brush untuk mengganti warna latar belakang
            Using brush As New SolidBrush(Color.LightGreen) ' Pilih warna yang diinginkan
                e.Graphics.FillRectangle(brush, e.CellBounds)
            End Using

            ' Melukis konten cell (teks atau isi ComboBox)
            e.PaintContent(e.ClipBounds)

            ' Tandai bahwa cell sudah di-handle (tidak perlu di-render ulang oleh DataGridView)
            e.Handled = True
        End If
    End Sub


    'FUNCTION UTILITY
    Private Sub Dgv_QC_Lab_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_QC_Lab.CellClick
        If e.ColumnIndex = DataGridViewComboBoxColumn1.Index Then
            Dgv_QC_Lab.CurrentCell = Dgv_QC_Lab.Rows(e.RowIndex).Cells(e.ColumnIndex)
            Dgv_QC_Lab.BeginEdit(True)
        End If
    End Sub

    Private Sub Dgv_QC_Lab_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_QC_Lab.CellEndEdit

        If Dgv_QC_Lab.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = Dgv_QC_Lab.CurrentRow.Index
        Dim currentCell = Dgv_QC_Lab.CurrentCellAddress.X

        Dim data = Dgv_QC_Lab.Rows(currentRow).Cells(currentCell)

        If currentCell = CellValue Then
            If Val(Dgv_QC_Lab.CurrentRow.Cells(CellValue).Value) < 0 Or IsNumeric(Dgv_QC_Lab.CurrentRow.Cells(CellValue).Value) = False Then
                Dgv_QC_Lab.CurrentRow.Cells(CellValue).Value = 0
            End If

            Get_Isi_Listview(currentRow, Dgv_QC_Lab)
            Dim Flag_slider As String = ""
            Dim Flag_Input As String = ""
            Try
                OpenConn()

                SQL = "select ISNULL(Flag_Slider,'T') as Flag_Slider, "
                SQL = SQL & "ISNULL(Flag_Option,'T') as Flag_Option, "
                SQL = SQL & "ISNULL(Flag_Input,'T') as Flag_Input from "
                SQL = SQL & "EMI_Kategori_Komponen where kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Kategori_Komponen='" & LvIDKategori & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Flag_slider = dr("Flag_Slider")
                        Flag_Input = dr("Flag_Input")
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            If Flag_slider = "Y" Then
                If Val(HilangkanTanda(LvValue)) < Val(HilangkanTanda(LvMinAwal)) Or Val(HilangkanTanda(LvValue)) > Val(HilangkanTanda(LvMaxAwal)) Then
                    Dgv_QC_Lab.CurrentRow.Cells(CellValue).Value = ""
                    MessageBox.Show("Value Tidak Boleh Lebih atau Kurang dari Range", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If Val(HilangkanTanda(LvValue)) >= Val(HilangkanTanda(LvMinHasil)) And Val(HilangkanTanda(LvValue)) <= Val(HilangkanTanda(LvMaxHasil)) Then
                    Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(144, 238, 144)
                    Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "HIJAU"
                Else
                    Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(254, 46, 46)
                    Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "MERAH"
                End If
            End If

            If Flag_Input = "Y" Then
                Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(144, 238, 144)
                Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "HIJAU"
            End If


        ElseIf currentCell = CellCmbValue Then
            Get_Isi_Listview(currentRow, Dgv_QC_Lab)

            Dim Flag_Option As String = ""

            Try
                OpenConn()

                SQL = "select ISNULL(Flag_Slider,'T') as Flag_Slider, "
                SQL = SQL & "ISNULL(Flag_Option,'T') as Flag_Option, "
                SQL = SQL & "ISNULL(Flag_Input,'T') as Flag_Input from "
                SQL = SQL & "EMI_Kategori_Komponen where kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Kategori_Komponen='" & LvIDKategori & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then

                        Flag_Option = dr("Flag_Option")

                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            If Flag_Option = "Y" Then

                Dim comboBoxCell As DataGridViewComboBoxCell = CType(Dgv_QC_Lab.Rows(currentRow).Cells(CellCmbValue), DataGridViewComboBoxCell)
                Dim index As Integer = comboBoxCell.Items.IndexOf(comboBoxCell.Value)

                Dim data_default As String = ""
                Try
                    OpenConn()

                    SQL = "select isnull(flag_default,'T') as flag_default "
                    SQL = SQL & "from EMI_Switch where id_qc_formula='" & LvIDUji & "' and "
                    SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and id_switch='" & arr2Switch(currentRow)(index) & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            data_default = dr("flag_default")
                        Else
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Data Tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using
                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If data_default = "Y" Then
                    Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(144, 238, 144)
                    Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "HIJAU"
                Else
                    Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(254, 46, 46)
                    Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "MERAH"
                End If

            End If
        End If


        Hasil_QC()


    End Sub




End Class
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Xml


Public Class Master_Quality_Control_Barang
    Dim arrcari, arrJenisQC As New ArrayList
    Dim Jenis = "Master_Quality_Control"
    Dim id_qc As String

    Dim LvIDUji As String
    Dim LvKodeUji As String
    Dim LvKet As String
    Dim LvSatuan As String
    Dim LvMinAwal As String
    Dim LvMaxAwal As String
    Dim LvMinHasil As String
    Dim LvMaxHasil As String
    Dim LvJenis As String
    Dim LvTampilMasuk As String
    Dim LvTampilBongkar As String
    Dim LvJnsKomponen As String

    Dim CellIDUji As Integer = 0
    Dim CellKodeUji As Integer = 1
    Dim CellKet As Integer = 2
    Dim CellSatuan As Integer = 3
    Dim CellMinAwal As Integer = 4
    Dim CellMaxAwal As Integer = 5
    Dim CellMinHasil As Integer = 6
    Dim CellMaxHasil As Integer = 7
    Dim CellJenis As Integer = 8
    Dim CellTampilMasuk As Integer = 9
    Dim CellTampilBongkar As Integer = 10
    Dim CellJnsKomponen As Integer = 11

    Dim LvData_Kode As String
    Dim LvData_Ket As String
    Dim LvData_satuan As String
    Dim LvData_ID As String

    Dim cellData_Kode As Integer = 0
    Dim cellData_Ket As Integer = 1
    Dim cellData_satuan As Integer = 2
    Dim cellData_ID As Integer = 3

    Dim LokasiDefault As String = ""

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvIDUji = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellIDUji).Value.ToString
        LvKodeUji = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellKodeUji).Value.ToString
        LvKet = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellKet).Value.ToString
        LvSatuan = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellSatuan).Value.ToString
        LvMinAwal = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellMinAwal).Value.ToString
        LvMaxAwal = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellMaxAwal).Value.ToString
        LvMinHasil = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellMinHasil).Value.ToString
        LvMaxHasil = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellMaxHasil).Value.ToString
        LvJenis = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellJenis).Value.ToString
        LvTampilMasuk = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellTampilMasuk).Value.ToString
        LvTampilBongkar = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellTampilBongkar).Value.ToString
        LvJnsKomponen = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellJnsKomponen).Value.ToString
    End Sub

    Public Sub Get_Isi_ListviewData(ByVal No_Index As Integer)

        LvData_Kode = ListView1.Items(No_Index).SubItems(cellData_Kode).Text
        LvData_Ket = ListView1.Items(No_Index).SubItems(cellData_Ket).Text
        LvData_satuan = ListView1.Items(No_Index).SubItems(cellData_satuan).Text
        LvData_ID = ListView1.Items(No_Index).SubItems(cellData_ID).Text

    End Sub

    Private Sub kosong()
        TextBox1.Text = ""
        TextBox4.Text = ""

        TextBox1.Focus()

        DgvSimulasi_DataHPP.Rows.Clear()

        TextBox3.Text = ""
        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_Quality_Control_Kode) : arrcari.Add("kode_Uji")
        ComboBox1.Items.Add(Base_Language.Lang_Quality_Control_Keterangan) : arrcari.Add("keterangan")

        ComboBox1.SelectedIndex = -1

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
        Btn_Cari.Text = Base_Language.Lang_Global_Cari
        Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

        ListView2.Location = New Point(128, 89)

        Try
            OpenConn()

            SQL = "Select kode_stock_owner_gudang From "
            SQL = SQL & "binding_lokasi_gudang Where gudang_default ='Y' "
            SQL = SQL & "and kode_stock_owner='" & Lokasi & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    LokasiDefault = dr("kode_stock_owner_gudang")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Gudang Default Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cari("Y")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "Select Id_QC_Formula,Kode_Perusahaan,Kode_Uji,Keterangan,Satuan,Target,"
            SQL = SQL & "Flag_Tampil_Formula,Flag_Tampil_Bahan "
            SQL = SQL & "From EMI_Quality_Control where kode_perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "And " & arrcari.Item(ComboBox1.SelectedIndex) & " Like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "order by Kode_Uji "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("Kode_Uji"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    Lvw.SubItems.Add(dr("Satuan"))
                    Lvw.SubItems.Add(dr("Id_QC_Formula"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        DgvSimulasi_DataHPP.Columns(CellJnsKomponen).DisplayIndex = 4

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Quality_Control_Judul
            Label2.Text = Base_Language.Lang_Global_KodeBarang

            Label4.Text = Base_Language.Lang_Quality_Control_Kolom

            ListView1.Columns.Add(Base_Language.Lang_Quality_Control_Kode, 100, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Quality_Control_Keterangan, 280, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Quality_Control_Satuan, 100, HorizontalAlignment.Left)
            ListView1.Columns.Add("Id_qc", 0, HorizontalAlignment.Left)
            ListView1.View = View.Details

            ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 270, HorizontalAlignment.Left)
            ListView2.Location = New Point(155, 160)
            ListView2.Visible = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox3.Text.Trim.Length = 0 Then
                ListView2.Visible = False : TextBox4.Focus() : Exit Sub
            End If
            TextBox1_Leave(TextBox3, e)
        End If
    End Sub



    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Tidak ada data . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Pilih dahulu barang . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Get_Isi_ListviewData(ListView1.FocusedItem.Index)

        For ind = 0 To DgvSimulasi_DataHPP.Rows.Count - 1
            Get_Isi_Listview(ind)

            If LvIDUji = LvData_ID Then
                MessageBox.Show("Data Sudah di Tambahkan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

        Next

        DgvSimulasi_DataHPP.Rows.Add(1)
        Dim index As Integer = DgvSimulasi_DataHPP.Rows.Count - 1
        Try
            OpenConn()

            SQL = "select a.id_qc_formula, a.Kode_uji, a.Keterangan, satuan, a.Id_Kategori_Komponen, b.Keterangan as Jenis_input, "
            SQL = SQL & "isnull(flag_option,'T') as flag_option, isnull(flag_input,'T') as flag_input, isnull(Flag_Slider,'T') as Flag_Slider, a.range_awal, a.range_akhir "
            SQL = SQL & "from EMI_Quality_Control a, emi_kategori_komponen b "
            SQL = SQL & "where a.id_kategori_komponen=b.id_kategori_komponen and a.kode_perusahaan = '" & KodePerusahaan & "' and a.Id_QC_Formula = '" & LvData_ID & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellIDUji).Value = Dr("Id_QC_Formula")
                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellKodeUji).Value = Dr("Kode_Uji")
                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellKet).Value = Dr("Keterangan")
                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellSatuan).Value = Dr("Satuan")

                    If Dr("flag_input") = "Y" Then

                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Value = ""
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Value = ""
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Value = ""
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Value = ""

                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).ReadOnly = True
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).ReadOnly = True
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).ReadOnly = True
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).ReadOnly = True

                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Style.BackColor = Color.White
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Style.BackColor = Color.White
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Style.BackColor = Color.White
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Style.BackColor = Color.White

                    ElseIf Dr("Flag_Slider") = "Y" Then
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Value = Dr("range_awal")
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Value = Dr("range_akhir")
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Value = 0
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Value = 0

                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).ReadOnly = True
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).ReadOnly = True
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).ReadOnly = False
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).ReadOnly = False


                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Style.BackColor = Color.LightGray
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Style.BackColor = Color.LightGray
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Style.BackColor = Color.LightGray
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Style.BackColor = Color.LightGray

                    ElseIf Dr("flag_option") = "Y" Then

                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Value = ""
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Value = ""
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Value = ""
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Value = ""

                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).ReadOnly = True
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).ReadOnly = True
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).ReadOnly = True
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).ReadOnly = True


                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Style.BackColor = Color.White
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Style.BackColor = Color.White
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Style.BackColor = Color.White
                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Style.BackColor = Color.White
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Jenis Input Tidak di temukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellJenis).Value = Dr("Id_Kategori_Komponen")
                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellTampilMasuk).Value = False
                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellTampilBongkar).Value = False
                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellJnsKomponen).Value = Dr("Jenis_input")
                Else
                    DgvSimulasi_DataHPP.Rows.RemoveAt(index)
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak di Temukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If
        If ListView2.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "select Kode_Barang,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Barang = '" & TextBox1.Text & "' "
            SQL = SQL & "group by Kode_Barang,Nama"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("Kode_Barang")
                    TextBox4.Text = Dr("Nama")
                    DgvSimulasi_DataHPP.Focus()

                    DgvSimulasi_DataHPP.Rows.Clear()
                    Dr.Close()
                    'SQL = "select a.Id, a.Kode_barang, a.Id_QC_Formula, a.Flag_Tampil_masuk, a.Flag_Tampil_Bongkar, "
                    'SQL = SQL & "a.Min_Range, a.max_range, a.Min_Nilai_Seharusnya, a.Max_Nilai_Seharusnya,b.Kode_Uji, "
                    'SQL = SQL & "b.Keterangan, b.Satuan, b.Id_Kategori_Komponen, c.keterangan as Jenis_Input, "
                    'SQL = SQL & "isnull(flag_option,'T') as flag_option, isnull(flag_input,'T') as flag_input, isnull(Flag_Slider,'T') as Flag_Slider "
                    'SQL = SQL & "from EMI_Quality_Control_perbarang a, emi_quality_control b, EMI_Kategori_Komponen c where "
                    'SQL = SQL & "a.Kode_perusahaan=b.Kode_Perusahaan and a.Id_QC_Formula=b.Id_QC_Formula and "
                    'SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan and b.Id_Kategori_Komponen=c.Id_Kategori_Komponen "
                    'SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Barang='" & TextBox1.Text & "' "
                    SQL = "select a.ID_Kategori_QC ,c.Kode_Uji, c.Keterangan, c.Satuan, c.Id_Kategori_Komponen, d.keterangan as Jenis_Input,e.ID_QC_Formula,"
                    SQL = SQL & "isnull(d.Flag_Option,'T') as flag_option, isnull(d.Flag_Input,'T') as flag_input, isnull(d.Flag_Slider,'T') as Flag_Slider,"
                    SQL = SQL & "isnull((select g.Id from EMI_Quality_Control_perbarang g where g.Kode_Perusahaan = e.Kode_Perusahaan and g.Id_QC_Formula = e.ID_QC_Formula "
                    SQL = SQL & "and g.Kode_barang = b.Kode_Barang),'-') as id_perbarang,"
                    SQL = SQL & "isnull((select g.Kode_barang from EMI_Quality_Control_perbarang g where g.Kode_Perusahaan = e.Kode_Perusahaan and g.Id_QC_Formula = e.ID_QC_Formula "
                    SQL = SQL & "and g.Kode_barang = b.Kode_Barang),'-') as kode_perbarang,"
                    SQL = SQL & "isnull((select g.Id_QC_Formula from EMI_Quality_Control_perbarang g where g.Kode_Perusahaan = e.Kode_Perusahaan and g.Id_QC_Formula = e.ID_QC_Formula "
                    SQL = SQL & "and g.Kode_barang = b.Kode_Barang),'-') as Id_QC_Formula_perbarang,"
                    SQL = SQL & "isnull((select g.Flag_Tampil_masuk from EMI_Quality_Control_perbarang g where g.Kode_Perusahaan = e.Kode_Perusahaan and g.Id_QC_Formula = e.ID_QC_Formula "
                    SQL = SQL & "and g.Kode_barang = b.Kode_Barang),'-') as Flag_Tampil_masuk_perbarang,"
                    SQL = SQL & "isnull((select g.Flag_Tampil_Bongkar from EMI_Quality_Control_perbarang g where g.Kode_Perusahaan = e.Kode_Perusahaan and g.Id_QC_Formula = e.ID_QC_Formula "
                    SQL = SQL & "and g.Kode_barang = b.Kode_Barang),'0') as Flag_Tampil_Bongkar_perbarang,"
                    SQL = SQL & "isnull((select g.Min_Range from EMI_Quality_Control_perbarang g where g.Kode_Perusahaan = e.Kode_Perusahaan and g.Id_QC_Formula = e.ID_QC_Formula "
                    SQL = SQL & "and g.Kode_barang = b.Kode_Barang),'0') as Min_Range_perbarang,"
                    SQL = SQL & "isnull((select g.max_range from EMI_Quality_Control_perbarang g where g.Kode_Perusahaan = e.Kode_Perusahaan and g.Id_QC_Formula = e.ID_QC_Formula "
                    SQL = SQL & "and g.Kode_barang = b.Kode_Barang),'0') as max_range_perbarang,"
                    SQL = SQL & "isnull((select g.Min_Nilai_Seharusnya from EMI_Quality_Control_perbarang g where g.Kode_Perusahaan = e.Kode_Perusahaan and g.Id_QC_Formula = e.ID_QC_Formula "
                    SQL = SQL & "and g.Kode_barang = b.Kode_Barang),'0') as Min_Nilai_Seharusnya_perbarang,"
                    SQL = SQL & "isnull((select g.Max_Nilai_Seharusnya from EMI_Quality_Control_perbarang g where g.Kode_Perusahaan = e.Kode_Perusahaan and g.Id_QC_Formula = e.ID_QC_Formula "
                    SQL = SQL & "and g.Kode_barang = b.Kode_Barang),'0') as Max_Nilai_Seharusnya_perbarang, "
                    SQL = SQL & "c.range_awal, c.range_akhir "
                    SQL = SQL & "from EMI_Kategori_QC a,Barang b,emi_quality_control c, EMI_Kategori_Komponen d,EMI_Kategori_QC_Detail e "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_Kategori_QC = b.ID_Kategori_QC "
                    SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.ID_Kategori_QC = e.ID_Kategori_QC "
                    SQL = SQL & "and e.Kode_Perusahaan = c.Kode_Perusahaan and e.ID_QC_Formula = c.ID_QC_Formula "
                    SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Komponen = d.Id_Kategori_Komponen "
                    SQL = SQL & "and b.Kode_Perusahaan='" & KodePerusahaan & "' and b.Kode_Barang='" & TextBox1.Text & "' and b.kode_stock_owner = '" & LokasiDefault & "' "
                    SQL = SQL & "group by a.ID_Kategori_QC,a.Keterangan ,c.Kode_Uji, c.Keterangan, c.Satuan, c.Id_Kategori_Komponen, d.keterangan,e.Kode_Perusahaan,"
                    SQL = SQL & "e.ID_QC_Formula,b.Kode_Barang,d.Flag_Option,d.Flag_Input,d.Flag_Slider,c.range_awal, c.range_akhir"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For index As Integer = 0 To .Rows.Count - 1
                                    DgvSimulasi_DataHPP.Rows.Add()
                                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellIDUji).Value = .Rows(index).Item("ID_QC_Formula")
                                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellKodeUji).Value = .Rows(index).Item("Kode_Uji")
                                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellKet).Value = .Rows(index).Item("Keterangan")
                                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellSatuan).Value = .Rows(index).Item("Satuan")

                                    If .Rows(index).Item("flag_input") = "Y" Then

                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Value = ""
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Value = ""
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Value = ""
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Value = ""

                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).ReadOnly = True
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).ReadOnly = True
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).ReadOnly = True
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).ReadOnly = True


                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Style.BackColor = Color.White
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Style.BackColor = Color.White
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Style.BackColor = Color.White
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Style.BackColor = Color.White


                                    ElseIf .Rows(index).Item("Flag_Slider") = "Y" Then
                                        'DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Value = .Rows(index).Item("Min_Range_perbarang")
                                        'DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Value = .Rows(index).Item("max_range_perbarang")
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Value = .Rows(index).Item("range_awal")
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Value = .Rows(index).Item("range_akhir")
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Value = .Rows(index).Item("Min_Nilai_Seharusnya_perbarang")
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Value = .Rows(index).Item("Max_Nilai_Seharusnya_perbarang")

                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).ReadOnly = True
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).ReadOnly = True
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).ReadOnly = False
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).ReadOnly = False

                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Style.BackColor = Color.LightGray
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Style.BackColor = Color.LightGray
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Style.BackColor = Color.LightGray
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Style.BackColor = Color.LightGray

                                    ElseIf .Rows(index).Item("flag_option") = "Y" Then

                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Value = ""
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Value = ""
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Value = ""
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Value = ""

                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).ReadOnly = True
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).ReadOnly = True
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).ReadOnly = True
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).ReadOnly = True


                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinAwal).Style.BackColor = Color.White
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxAwal).Style.BackColor = Color.White
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMinHasil).Style.BackColor = Color.White
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellMaxHasil).Style.BackColor = Color.White
                                    Else
                                        Dr.Close()
                                        CloseConn()
                                        MessageBox.Show("Jenis Input Tidak di temukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellJenis).Value = .Rows(index).Item("Id_Kategori_Komponen")

                                    If General_Class.CekNULL(.Rows(index).Item("Flag_Tampil_Masuk_perbarang")) = "Y" Then
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellTampilMasuk).Value = True
                                    Else
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellTampilMasuk).Value = False
                                    End If

                                    If General_Class.CekNULL(.Rows(index).Item("Flag_Tampil_Bongkar_perbarang")) = "Y" Then
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellTampilBongkar).Value = True
                                    Else
                                        DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellTampilBongkar).Value = False
                                    End If

                                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellJnsKomponen).Value = .Rows(index).Item("Jenis_Input")

                                Next
                            End If
                        End With
                    End Using
                Else

                    TextBox1.Text = ""
                    TextBox4.Text = ""
                    TextBox1.Focus()
                End If
                ListView2.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Keterangan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus() : Exit Sub
        ElseIf DgvSimulasi_DataHPP.Rows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DgvSimulasi_DataHPP.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "delete from EMI_Quality_Control_PerBarang where "
            SQL = SQL & "Kode_Perusahaan ='" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Barang='" & TextBox1.Text & "' "
            ExecuteTrans(SQL)

            For index = 0 To DgvSimulasi_DataHPP.Rows.Count - 1
                Get_Isi_Listview(index)

                Dim Flag_tampil_masuk As String = ""
                Dim Flag_tampil_bongkar As String = ""
                Dim minRange As String = ""
                Dim maxRange As String = ""
                Dim minHasil As String = ""
                Dim maxHasil As String = ""
                Dim check As String = ""

                If DgvSimulasi_DataHPP.Rows(index).Cells(CellTampilMasuk).Value = True Then
                    Flag_tampil_masuk = "Y"
                Else
                    Flag_tampil_masuk = "T"
                End If

                If DgvSimulasi_DataHPP.Rows(index).Cells(CellTampilBongkar).Value = True Then
                    Flag_tampil_bongkar = "Y"
                Else
                    Flag_tampil_bongkar = "T"
                End If

                Dim flag_slider As String = ""
                SQL = "select isnull(flag_slider,'T') as flag_slider from "
                SQL = SQL & "emi_kategori_komponen where id_kategori_komponen='" & LvJenis & "' and "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        flag_slider = Dr("flag_slider")
                    End If
                End Using

                If flag_slider = "Y" Then

                    If LvMinAwal = "" Or LvMaxAwal = "" Or LvMinHasil = "" Or LvMaxHasil = "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data " & LvKet & " belum di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    minRange = "'" & LvMinAwal & "'"
                    maxRange = "'" & LvMaxAwal & "'"
                    minHasil = "'" & LvMinHasil & "'"
                    maxHasil = "'" & LvMaxHasil & "'"

                Else

                    minRange = "NULL"
                    maxRange = "NULL"
                    minHasil = "NULL"
                    maxHasil = "NULL"


                End If

                SQL = "insert into EMI_Quality_Control_PerBarang(Kode_Perusahaan, Kode_Barang, id_qc_Formula, Flag_Tampil_Masuk, "
                SQL = SQL & "Flag_Tampil_Bongkar, Min_Range, max_range, Min_Nilai_Seharusnya, Max_Nilai_Seharusnya) values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TextBox1.Text & "', '" & LvIDUji & "', 'Y', 'Y', "
                SQL = SQL & "" & minRange & ", " & maxRange & ", " & minHasil & ", " & maxHasil & ")"
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Berhasil disimpan", "Simpan QC", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        End If
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "delete from EMI_Quality_Control_PerBarang where "
                SQL = SQL & "Kode_Perusahaan ='" & KodePerusahaan & "' and "
                SQL = SQL & "Kode_Barang='" & TextBox1.Text & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        kosong()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        ListView2.Items.Clear()
        Dim lv As New ListViewItem

        Try
            OpenConn()

            SQL = "select Kode_Barang,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Barang like '%" & TextBox1.Text & "%' "
            SQL = SQL & "group by Kode_Barang,Nama order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        ListView2.Items.Clear()
        Dim lv As New ListViewItem
        Try
            OpenConn()

            SQL = "select Kode_Barang,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Nama like '%" & TextBox4.Text & "%' "
            SQL = SQL & "group by Kode_Barang,Nama order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Down Then
            ListView2.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView2.Items.Count = 0 Then Exit Sub
            ListView2.Focus()
        End If
    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        If ListView2.Focused = True Then Exit Sub
        TextBox3.Text = "" : TextBox4.Text = ""
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub
        Dim kode As String = ListView2.FocusedItem.Text
        Dim nama As String = ListView2.FocusedItem.SubItems(1).Text
        TextBox1.Text = kode
        TextBox4.Text = nama
        ListView2.Visible = False
        TextBox1_Leave(ListView1, e)
    End Sub


    Private Sub ListView2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub DgvSimulasi_DataHPP_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvSimulasi_DataHPP.KeyDown
        If DgvSimulasi_DataHPP.Rows.Count = 0 Or DgvSimulasi_DataHPP.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DgvSimulasi_DataHPP.CurrentRow.Index
        Dim currentCell = DgvSimulasi_DataHPP.CurrentCellAddress.X

        If e.KeyCode = Keys.Delete Then

            Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If Hapus1 = vbNo Then
                Exit Sub
            End If

            BeginInvoke(New MethodInvoker(Sub() DgvSimulasi_DataHPP.Rows.RemoveAt(currentRow)))

        End If
    End Sub

    Private Sub DgvSimulasi_DataHPP_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSimulasi_DataHPP.CellEndEdit
        Try
            If e.ColumnIndex = CellMinHasil Then
                Dim currentCell = DgvSimulasi_DataHPP.Rows(e.RowIndex).Cells(e.ColumnIndex)
                Dim minAwal = DgvSimulasi_DataHPP.Rows(e.RowIndex).Cells(CellMinAwal)
                Dim maxAwal = DgvSimulasi_DataHPP.Rows(e.RowIndex).Cells(CellMaxAwal)

                If Not IsNumeric(currentCell.Value) OrElse
                   Not IsNumeric(minAwal.Value) OrElse
                   Not IsNumeric(maxAwal.Value) Then
                    currentCell.Value = minAwal.Value
                    MessageBox.Show("Nilai harus berupa angka", "Error")
                    Return
                End If

                Dim currentValue As Decimal = Convert.ToDecimal(currentCell.Value)
                Dim minValue As Decimal = Convert.ToDecimal(minAwal.Value)
                Dim maxValue As Decimal = Convert.ToDecimal(maxAwal.Value)

                If currentValue < minValue Then
                    currentCell.Value = minValue
                    MessageBox.Show($"Nilai minimal awal ({minValue})")
                ElseIf currentValue > maxValue Then
                    currentCell.Value = maxValue
                    MessageBox.Show($"Nilai maximal awal ({maxValue})")
                End If
            End If

            If e.ColumnIndex = CellMaxHasil Then
                Dim currentCell = DgvSimulasi_DataHPP.Rows(e.RowIndex).Cells(e.ColumnIndex)
                Dim minAwal = DgvSimulasi_DataHPP.Rows(e.RowIndex).Cells(CellMinAwal)
                Dim maxAwal = DgvSimulasi_DataHPP.Rows(e.RowIndex).Cells(CellMaxAwal)

                If Not IsNumeric(currentCell.Value) OrElse
                   Not IsNumeric(minAwal.Value) OrElse
                   Not IsNumeric(maxAwal.Value) Then
                    currentCell.Value = minAwal.Value
                    MessageBox.Show("Nilai harus berupa angka", "Error")
                    Return
                End If

                Dim currentValue As Decimal = Convert.ToDecimal(currentCell.Value)
                Dim minValue As Decimal = Convert.ToDecimal(minAwal.Value)
                Dim maxValue As Decimal = Convert.ToDecimal(maxAwal.Value)

                If currentValue < minValue Then
                    currentCell.Value = minValue
                    MessageBox.Show($"Nilai minimal awal ({minValue})")
                ElseIf currentValue > maxValue Then
                    currentCell.Value = maxValue
                    MessageBox.Show($"Nilai maximal awal ({maxValue})")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error")
        End Try
    End Sub

End Class
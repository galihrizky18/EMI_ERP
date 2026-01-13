Public Class N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain


    Dim Lv_Ket_Kategori_Gudang, Lv_Kategori_Jenis, Lv_Sub_Kategori_Gudang_Jenis, Lv_Jenis_Gudang, Lv_Keterangan, Lv_Urut, Lv_IdKategoriGudang, Lv_IdKategoriJenis, Lv_IdSubKategoriGudang As String

    Dim item_Kategori_Gudang As Integer = 0
    Dim item_Kategori_Jenis As Integer = 1
    Dim item_Sub_Kategori_Jenis As Integer = 2
    Dim item_Jenis_Gudang As Integer = 3
    Dim item_Keterangan As Integer = 4
    Dim item_Urut As Integer = 5
    Dim item_IdKategoriGudang As Integer = 6
    Dim item_IdKategoriJenis As Integer = 7
    Dim item_IDSubKategoriJenis As Integer = 8


    Dim LvSub_Kategori_Jenis, LvSub_Sub_Kategori_Jenis, LvSub_Id_Kategori_Jenis, LvSub_ID_Sub_Kategori_Jenis, LvSub_Has_Binding As String

    Dim itemSub_Kategori_Jenis As Integer = 0
    Dim itemSub_Sub_Kategori_Jenis As Integer = 1
    Dim itemSub_Id_Kategori_Jenis As Integer = 2
    Dim itemSub_Id_Sub_Kategori_Jenis As Integer = 3
    Dim itemSub_Has_Binding As Integer = 4





    Dim SelectedUrut As String = ""

    Dim Swith_AutoComplete As Boolean

    '=========================
    '=    TAB 1 VARIABLES    =
    '=========================
    Dim arrIDKategoriGudang As New ArrayList
    Dim arrKateogori, arrIDKategori As New ArrayList
    Dim arrCariTab1 As New ArrayList

    Dim Lv_Tab2_Kd_Kategori_Jenis, Lv_Tab2_Kategori_jenis, Lv_Tab2_Kd_Sub_Kategori_Jenis, Lv_Tab2_Sub_Kategori_Jenis, Lv_Tab2_Kd_Kategori_Gudang, Lv_Tab2_Kategori_Gudang,
        Lv_Tab2_Prefix, Lv_Tab2_Id_Kategori_Jenis, Lv_Tab2_Id_Sub_Kategori_Jenis, Lv_Tab2_Id_Kategori_Gudang As String

    Dim Item_Tab2_Kd_Kategori_Jenis As Integer = 0
    Dim Item_Tab2_Kategori_Jenis As Integer = 1
    Dim Item_Tab2_Kd_Sub_Kategori_Jenis As Integer = 2
    Dim Item_Tab2_Sub_Kategori_Jenis As Integer = 3
    Dim Item_Tab2_Kd_Kategori_Gudang As Integer = 4
    Dim Item_Tab2_Kategori_Gudang As Integer = 5
    Dim Item_Tab2_Prefix As Integer = 6
    Dim Item_Tab2_Id_Kategori_Jenis As Integer = 7
    Dim Item_Tab2_Id_Sub_Kategori_Jenis As Integer = 8
    Dim Item_Tab2_Id_Kategori_Gudang As Integer = 9

    Dim tab1_Id_Sub_Kategori_Jenis, xprefix As String

    '================================================



    Private Sub N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Kategori_Gudang.Columns.Clear()
        Lv_Kategori_Gudang.Columns.Add("Kode Kategori Gudang", 150, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.Columns.Add("Jenis Gudang", 150, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.Columns.Add("ID", 0, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.View = View.Details

        Lv_Sub_Kategori_Jenis.Columns.Clear()
        Lv_Sub_Kategori_Jenis.Columns.Add("Kode Sub Kategori Jenis", 150, HorizontalAlignment.Left)
        Lv_Sub_Kategori_Jenis.Columns.Add("Kode Kategori Jenis", 150, HorizontalAlignment.Left)
        Lv_Sub_Kategori_Jenis.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Sub_Kategori_Jenis.Columns.Add("iD", 0, HorizontalAlignment.Left)
        Lv_Sub_Kategori_Jenis.View = View.Details

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Kategori Gudang", 150, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Kategori Jenis", 150, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Sub Kategori Jenis", 150, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Jenis Gudang", 130, HorizontalAlignment.Center) '3
        Lv_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '4
        'hide'
        Lv_Data.Columns.Add("Urut", 0, HorizontalAlignment.Left) '5
        Lv_Data.Columns.Add("ID Kategori Gudang", 0, HorizontalAlignment.Left) '6
        Lv_Data.Columns.Add("ID Kategori Jenis", 0, HorizontalAlignment.Left) '7
        Lv_Data.Columns.Add("ID Sub Kategori Jenis", 0, HorizontalAlignment.Left) '8
        Lv_Data.View = View.Details


        Lv_Data_Sub_Kategori.Columns.Clear()
        Lv_Data_Sub_Kategori.Columns.Add("Kode Kategori Jenis", 160, HorizontalAlignment.Left) '0
        Lv_Data_Sub_Kategori.Columns.Add("Kategori Jenis", 160, HorizontalAlignment.Left) '1
        Lv_Data_Sub_Kategori.Columns.Add("Kode Sub Kategori Jenis", 160, HorizontalAlignment.Left) '2
        Lv_Data_Sub_Kategori.Columns.Add("Sub Kategori Jenis", 160, HorizontalAlignment.Left) '3
        Lv_Data_Sub_Kategori.Columns.Add("Kode Kategori Gudang", 160, HorizontalAlignment.Left) '4
        Lv_Data_Sub_Kategori.Columns.Add("Kategori Gudang", 160, HorizontalAlignment.Left) '5
        Lv_Data_Sub_Kategori.Columns.Add("Prefix", 130, HorizontalAlignment.Left) '6
        'Hide
        Lv_Data_Sub_Kategori.Columns.Add("id_kategori_jenis", 0, HorizontalAlignment.Left) '7
        Lv_Data_Sub_Kategori.Columns.Add("id_sub_kategori_jenis", 0, HorizontalAlignment.Left) '8
        Lv_Data_Sub_Kategori.Columns.Add("id_kategori_gudang", 0, HorizontalAlignment.Left) '9
        Lv_Data_Sub_Kategori.View = View.Details


        Lv_Data_Sub_Kategori_Jenis.Columns.Clear()
        Lv_Data_Sub_Kategori_Jenis.Columns.Add("Kategori Jenis", 240, HorizontalAlignment.Left) '0
        Lv_Data_Sub_Kategori_Jenis.Columns.Add("Sub Kategori Jenis", 240, HorizontalAlignment.Left) '1
        'Hide'
        Lv_Data_Sub_Kategori_Jenis.Columns.Add("id_kategori_jenis", 0, HorizontalAlignment.Left) '2
        Lv_Data_Sub_Kategori_Jenis.Columns.Add("id_sub_kategori_jenis", 0, HorizontalAlignment.Left) '3
        Lv_Data_Sub_Kategori_Jenis.Columns.Add("has_binding", 0, HorizontalAlignment.Left) '4


        Cmb_Filter_Kolom_Sub_Kategori.Items.Clear() : arrCariTab1.Clear()
        Cmb_Filter_Kolom_Sub_Kategori.Items.Add("Kode Kategori Jenis") : arrCariTab1.Add("b.Kode_Kategori_Jenis")
        Cmb_Filter_Kolom_Sub_Kategori.Items.Add("Kategori Jenis") : arrCariTab1.Add("b.Keterangan")
        Cmb_Filter_Kolom_Sub_Kategori.Items.Add("Kode Sub Kategori Jenis") : arrCariTab1.Add("a.Kode_Sub_Kategori_Jenis")
        Cmb_Filter_Kolom_Sub_Kategori.Items.Add("Sub Kategori Jenis") : arrCariTab1.Add("a.Keterangan")
        Cmb_Filter_Kolom_Sub_Kategori.Items.Add("Kode Kategori Gudang") : arrCariTab1.Add("d.Kode_Kategori_Gudang")
        Cmb_Filter_Kolom_Sub_Kategori.Items.Add("Kategori Gudang") : arrCariTab1.Add("d.Keterangan")
        Cmb_Filter_Kolom_Sub_Kategori.Items.Add("Prefix") : arrCariTab1.Add("a.Prefix")

        TabControl1.SelectedIndex = 1

        Kosong()

    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)
        Lv_Ket_Kategori_Gudang = Lv_Data.Items(index).SubItems(item_Kategori_Gudang).Text
        Lv_Kategori_Jenis = Lv_Data.Items(index).SubItems(item_Kategori_Jenis).Text
        Lv_Sub_Kategori_Gudang_Jenis = Lv_Data.Items(index).SubItems(item_Sub_Kategori_Jenis).Text
        Lv_Jenis_Gudang = Lv_Data.Items(index).SubItems(item_Jenis_Gudang).Text
        Lv_Keterangan = Lv_Data.Items(index).SubItems(item_Keterangan).Text
        Lv_Urut = Lv_Data.Items(index).SubItems(item_Urut).Text
        Lv_IdKategoriGudang = Lv_Data.Items(index).SubItems(item_IdKategoriGudang).Text
        Lv_IdKategoriJenis = Lv_Data.Items(index).SubItems(item_IdKategoriJenis).Text
        Lv_IdSubKategoriGudang = Lv_Data.Items(index).SubItems(item_IDSubKategoriJenis).Text
    End Sub

    Private Sub Get_Lv_Tab_1(ByVal Index As Integer)
        Lv_Tab2_Kd_Kategori_Jenis = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Kd_Kategori_Jenis).Text
        Lv_Tab2_Kategori_jenis = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Kategori_Jenis).Text
        Lv_Tab2_Kd_Sub_Kategori_Jenis = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Kd_Sub_Kategori_Jenis).Text
        Lv_Tab2_Sub_Kategori_Jenis = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Sub_Kategori_Jenis).Text
        Lv_Tab2_Kd_Kategori_Gudang = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Kd_Kategori_Gudang).Text
        Lv_Tab2_Kategori_Gudang = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Kategori_Gudang).Text
        Lv_Tab2_Prefix = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Prefix).Text
        Lv_Tab2_Id_Kategori_Jenis = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Id_Kategori_Jenis).Text
        Lv_Tab2_Id_Sub_Kategori_Jenis = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Id_Sub_Kategori_Jenis).Text
        Lv_Tab2_Id_Kategori_Gudang = Lv_Data_Sub_Kategori.Items(Index).SubItems(Item_Tab2_Id_Kategori_Gudang).Text
    End Sub

    Private Sub Get_Lv_Sub(ByVal index As Integer)
        LvSub_Kategori_Jenis = Lv_Data_Sub_Kategori_Jenis.Items(index).SubItems(itemSub_Kategori_Jenis).Text
        LvSub_Sub_Kategori_Jenis = Lv_Data_Sub_Kategori_Jenis.Items(index).SubItems(itemSub_Sub_Kategori_Jenis).Text
        LvSub_Id_Kategori_Jenis = Lv_Data_Sub_Kategori_Jenis.Items(index).SubItems(itemSub_Id_Kategori_Jenis).Text
        LvSub_ID_Sub_Kategori_Jenis = Lv_Data_Sub_Kategori_Jenis.Items(index).SubItems(itemSub_Id_Sub_Kategori_Jenis).Text
        LvSub_Has_Binding = Lv_Data_Sub_Kategori_Jenis.Items(index).SubItems(itemSub_Has_Binding).Text
    End Sub


    Private Sub Kosong()

        Kosong_Tab_1()
        Kosong_Tab_2()
    End Sub

    Private Sub Kosong_Tab_1()

        Try
            OpenConn()

            '================================
            '=     LOAD KATEGORI GUDANG     =
            '================================
            Cmb_Kategori_Gudang.Items.Clear() : arrIDKategoriGudang.Clear()
            SQL = $"
                select Kode_Kategori_Gudang, Keterangan, Jenis_Gudang, Urut_Oto
                from N_EMI_Master_Kategori_Gudang_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and status is null
                order by Kode_Kategori_Gudang, Jenis_Gudang
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Kategori_Gudang.Items.Add(Dr("Kode_Kategori_Gudang")) : arrIDKategoriGudang.Add(Dr("Urut_Oto"))
                Loop
            End Using


            '=================================
            '=     LOAD KATEGORI LAYER 1     =
            '=================================
            Cmb_Kode_Kategori.Items.Clear() : arrKateogori.Clear() : arrIDKategori.Clear()
            SQL = $"
                select Kode_Kategori_Jenis, Keterangan, Id_Kategori_Jenis 
                from N_EMI_Master_Kategori_Jenis
                where Kode_Perusahaan = '{KodePerusahaan}' 
                order by Keterangan
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Kode_Kategori.Items.Add(Dr("Keterangan")) : arrKateogori.Add(Dr("Kode_Kategori_Jenis")) : arrIDKategori.Add(Dr("Id_Kategori_Jenis"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Txt_Kode_Sub.Text = ""
        Txt_Keterangan_Sub_Kategori.Text = ""
        Txt_Prefix.Text = ""

        tab1_Id_Sub_Kategori_Jenis = ""

        Cmb_Filter_Kolom_Sub_Kategori.SelectedIndex = -1
        Txt_Filter_Value_Sub_Kategori.Text = ""

        Btn_Simpan_Sub_Kategori.Tag = "SIMPAN"
        Btn_Simpan_Sub_Kategori.Text = "&Simpan"
        Btn_Simpan_Sub_Kategori.Enabled = True


        LoadData_Tab_1()
    End Sub

    Private Sub Kosong_Tab_2()
        Swith_AutoComplete = True

        Txt_Kd_Kategori.Text = String.Empty
        Txt_Kategori_Keterangan.Text = String.Empty
        Txt_Sub_Kategori_Jenis.Text = String.Empty
        Txt_Kategori_Jenis.Text = String.Empty
        Txt_Keterangan_Kategori_Jenis.Text = String.Empty
        Txt_Keterangan.Text = String.Empty
        Txt_Jenis_Kategori_Gudang.Text = String.Empty

        SelectedUrut = String.Empty

        Txt_ID_Kategori.Text = String.Empty
        Txt_ID_Sub_Kategori.Text = String.Empty

        Txt_Kd_Kategori.Enabled = True
        Txt_Kd_Kategori.BackColor = Color.White

        Txt_Sub_Kategori_Jenis.Enabled = True
        Txt_Sub_Kategori_Jenis.BackColor = Color.White

        Swith_AutoComplete = False

        Btn_Simpan.Tag = "SIMPAN"
        Btn_Simpan.Text = "&Simpan"



        LoadData()
    End Sub

    Private Sub Btn_Cari_Sub_Kategori_Click(sender As Object, e As EventArgs) Handles Btn_Cari_Sub_Kategori.Click
        If Cmb_Filter_Kolom_Sub_Kategori.SelectedIndex = -1 Then
            MessageBox.Show("Harap Pilih Dahulu Jenis Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Filter_Kolom_Sub_Kategori.DroppedDown = True
            Cmb_Filter_Kolom_Sub_Kategori.Focus()
            Exit Sub
        ElseIf Txt_Filter_Value_Sub_Kategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Harap Isi Dahulu Value Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Filter_Value_Sub_Kategori.Focus()
            Exit Sub
        End If

        LoadData_Tab_1()
    End Sub

    Private Sub LoadData_Tab_1()
        Try
            OpenConn()

            Dim Filter As String = ""

            If Cmb_Filter_Kolom_Sub_Kategori.SelectedIndex <> -1 Then
                Filter &= $"and {arrCariTab1(Cmb_Filter_Kolom_Sub_Kategori.SelectedIndex)} like '%{Txt_Filter_Value_Sub_Kategori.Text.Trim}%' "
            End If


            Lv_Data_Sub_Kategori.Items.Clear()
            SQL = $"
                select b.Kode_Kategori_Jenis, b.Keterangan as Kategori_Jenis, a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis,
	                d.Urut_Oto as Id_Kategori_Gudang, d.Kode_Kategori_Gudang, d.Keterangan AS kategori_Gudang,
	                a.Prefix, a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis
                from N_EMI_Master_Sub_Kategori_Jenis a
	                inner join N_EMI_Master_Kategori_Jenis b on a.Kode_Perusahaan = b.kode_perusahaan AND a.Id_Kategori_Jenis = b.Id_Kategori_Jenis
	                left JOIN N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain c ON a.kode_perusahaan = c.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis
	                left JOIN N_EMI_Master_Kategori_Gudang_Barang_Lain d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Gudang = d.Urut_Oto
                WHERE a.Kode_Perusahaan =  '{KodePerusahaan}' 
                AND c.Status IS null and d.status is null
                {Filter}
                order by a.Keterangan
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data_Sub_Kategori.Items.Add(Dr("Kode_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Kode_Sub_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Sub_Kategori_Jenis"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Kode_Kategori_Gudang")) = "", "-", Dr("Kode_Kategori_Gudang")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("kategori_Gudang")) = "", "-", Dr("kategori_Gudang")))
                    Lv.SubItems.Add(Dr("Prefix"))
                    Lv.SubItems.Add(Dr("Id_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Id_Sub_Kategori_Jenis"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Id_Kategori_Gudang")) = "", "-", Dr("Id_Kategori_Gudang")))

                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LoadData()
        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = $"
                select a.Id_Kategori_Gudang, b.Keterangan as Kategori_Gudang, d.Id_Kategori_Jenis, d.Kode_Kategori_Jenis AS Kategori_Jenis, a.Id_Sub_Kategori_Jenis, c.Keterangan AS Sub_Kategori_Jenis, 
	                b.jenis_gudang, a.Keterangan, a.urut_oto
                from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain a
                    inner JOIN N_EMI_Master_Kategori_Gudang_Barang_Lain b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Urut_Oto
                    inner join N_EMI_Master_Sub_Kategori_Jenis c ON a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis
	                inner JOIN N_EMI_Master_Kategori_Jenis d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Jenis = d.Id_Kategori_Jenis
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.status is null and b.Status is null
                order by a.tanggal, a.Jam
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Kategori_Gudang"))
                    Lv.SubItems.Add(Dr("Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Sub_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("jenis_gudang"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("urut_oto"))
                    Lv.SubItems.Add(Dr("Id_Kategori_Gudang"))
                    Lv.SubItems.Add(Dr("Id_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Id_Sub_Kategori_Jenis"))
                Loop
            End Using


            Lv_Data_Sub_Kategori_Jenis.Items.Clear()
            SQL = $"
                select b.Id_Kategori_Jenis, b.Kode_Kategori_Jenis, a.Id_Sub_Kategori_Jenis, a.Kode_Sub_Kategori_Jenis,
	                isnull((
		                SELECT TOP 1 'Y'
		                FROM N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain z
		                WHERE z.Kode_Perusahaan = a.Kode_Perusahaan
			                AND z.Status is null
			                AND a.Id_Sub_Kategori_Jenis = z.Id_Sub_Kategori_Jenis
	                ), 'T') AS HasBinding
                FROM N_EMI_Master_Sub_Kategori_Jenis a
	                inner join N_EMI_Master_Kategori_Jenis b on a.Kode_Perusahaan = b.kode_perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis
                WHERE a.kode_perusahaan = '{KodePerusahaan}'
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data_Sub_Kategori_Jenis.Items.Add(Dr("Kode_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Kode_Sub_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Id_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Id_Sub_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("HasBinding"))

                    If General_Class.CekNULL(Dr("HasBinding")) = "T" Then
                        Lv.BackColor = Color.LightYellow
                    Else
                        Lv.BackColor = Color.White
                    End If

                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Kategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Kategori.TextChanged
        If Swith_AutoComplete Then Exit Sub

        If Txt_Kd_Kategori.Text.Trim.Length = 0 Then
            Lv_Kategori_Gudang.Visible = False
            Lv_Kategori_Gudang.Location = New Point(1200, 137)
            Txt_Kd_Kategori.Text = ""
            Txt_Jenis_Kategori_Gudang.Text = ""
            Txt_Kategori_Keterangan.Text = ""
            Exit Sub
        Else
            Lv_Kategori_Gudang.Location = New Point(167, 137)
            Lv_Kategori_Gudang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Kategori_Gudang.Items.Clear()


            SQL = $"
                select Kode_Kategori_Gudang, Keterangan, Jenis_Gudang, Urut_Oto
                from N_EMI_Master_Kategori_Gudang_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and status is null
                and Kode_Kategori_Gudang like '%{Txt_Kd_Kategori.Text.Trim}%'
                order by Kode_Kategori_Gudang
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Kategori_Gudang.Items.Add(Dr("Kode_Kategori_Gudang"))
                    Lv.SubItems.Add(Dr("Jenis_Gudang"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Urut_Oto"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_Kd_Kategori_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Kategori.Leave
        If Txt_Kd_Kategori.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Kategori_Gudang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Kategori.Text.Trim.Length = 0 Then

                SQL = $"
                    select Kode_Kategori_Gudang, Keterangan, Jenis_Gudang, Urut_Oto 
                    from N_EMI_Master_Kategori_Gudang_Barang_Lain
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                    order by Kode_Kategori_Gudang
                "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Swith_AutoComplete = True
                        Txt_Kd_Kategori.Text = Dr("Kode_Kategori_Gudang")
                        Txt_Jenis_Kategori_Gudang.Text = Dr("Jenis_Gudang")
                        Txt_Kategori_Keterangan.Text = Dr("Keterangan")
                        Txt_ID_Kategori.Text = Dr("Urut_Oto")
                        Swith_AutoComplete = False
                        Txt_Sub_Kategori_Jenis.Focus()
                    Else
                        MessageBox.Show("Kategori Gudang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kd_Kategori.Text = ""
                        Txt_Jenis_Kategori_Gudang.Text = ""
                        Txt_Kategori_Keterangan.Text = ""

                        Txt_ID_Sub_Kategori.Text = ""
                        Txt_Sub_Kategori_Jenis.Text = ""
                        Txt_Kategori_Jenis.Text = ""
                        Txt_Keterangan_Kategori_Jenis.Text = ""
                        Txt_Keterangan.Text = ""
                        Txt_Kd_Kategori.Focus()
                    End If


                    Lv_Kategori_Gudang.Visible = False
                    Lv_Kategori_Gudang.Location = New Point(1200, 137)
                End Using
            Else
                Txt_Sub_Kategori_Jenis.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Kategori.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Kategori.Text.Trim.Length = 0 Then Txt_Kd_Kategori.Focus()
            Txt_Kd_Kategori_Leave(Txt_Kd_Kategori, e)

            Lv_Kategori_Gudang.Visible = False
            Lv_Kategori_Gudang.Location = New Point(1200, 137)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_Kd_Kategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Kategori.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Kategori_Gudang.Focus()
    End Sub

    Private Sub Lv_Kategori_Gudang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Kategori_Gudang.DoubleClick
        If Lv_Kategori_Gudang.Items.Count = 0 Or Lv_Kategori_Gudang.FocusedItem.Index = -1 Then Exit Sub


        Dim Kd_Kategori As String = Lv_Kategori_Gudang.FocusedItem.SubItems(0).Text
        Dim Jenis_Gudang As String = Lv_Kategori_Gudang.FocusedItem.SubItems(1).Text
        Dim Keterangan As String = Lv_Kategori_Gudang.FocusedItem.SubItems(2).Text
        Dim ID As String = Lv_Kategori_Gudang.FocusedItem.SubItems(3).Text

        Swith_AutoComplete = True
        Txt_Kd_Kategori.Text = Kd_Kategori
        Txt_Jenis_Kategori_Gudang.Text = Jenis_Gudang
        Txt_Kategori_Keterangan.Text = Keterangan
        Txt_ID_Kategori.Text = ID
        Swith_AutoComplete = False

        Lv_Kategori_Gudang.Visible = False
        Lv_Kategori_Gudang.Location = New Point(1200, 137)

        Txt_Sub_Kategori_Jenis.Focus()
    End Sub

    Private Sub Lv_Kategori_Gudang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Kategori_Gudang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Kategori_Gudang_DoubleClick(Lv_Kategori_Gudang, e)
        End If
    End Sub



    Private Sub Txt_Sub_Kategori_Jenis_TextChanged(sender As Object, e As EventArgs) Handles Txt_Sub_Kategori_Jenis.TextChanged
        If Swith_AutoComplete Then Exit Sub

        If Txt_Sub_Kategori_Jenis.Text.Trim.Length = 0 Then
            Lv_Sub_Kategori_Jenis.Visible = False
            Lv_Sub_Kategori_Jenis.Location = New Point(1200, 267)
            Txt_Sub_Kategori_Jenis.Text = ""
            Txt_Kategori_Jenis.Text = ""
            Txt_Keterangan_Kategori_Jenis.Text = ""
            Exit Sub
        Else
            Lv_Sub_Kategori_Jenis.Location = New Point(167, 267)
            Lv_Sub_Kategori_Jenis.Visible = True
        End If

        Try
            OpenConn()

            Lv_Sub_Kategori_Jenis.Items.Clear()
            SQL = $"
                select a.Kode_Sub_Kategori_Jenis, b.Kode_Kategori_Jenis, a.Keterangan, a.Id_Sub_Kategori_Jenis
                from N_EMI_Master_Sub_Kategori_Jenis a
	                inner join N_EMI_Master_Kategori_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Kode_Sub_Kategori_Jenis like '%{Txt_Sub_Kategori_Jenis.Text}%'
                order by a.Kode_Sub_Kategori_Jenis, b.Kode_Kategori_Jenis
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Sub_Kategori_Jenis.Items.Add(Dr("Kode_Sub_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Kode_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Id_Sub_Kategori_Jenis"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Txt_Sub_Kategori_Jenis_Leave(sender As Object, e As EventArgs) Handles Txt_Sub_Kategori_Jenis.Leave
        If Txt_ID_Sub_Kategori.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Sub_Kategori_Jenis.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_ID_Sub_Kategori.Text.Trim.Length = 0 Then

                SQL = $"
                    select a.Kode_Sub_Kategori_Jenis, b.Kode_Kategori_Jenis, a.Keterangan, a.id_sub_kategori_jenis
                    from N_EMI_Master_Sub_Kategori_Jenis a
	                    inner join N_EMI_Master_Kategori_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis
                    where a.Kode_Perusahaan = '{KodePerusahaan}'
                    and Id_Sub_Kategori_Jenis = '{Txt_ID_Sub_Kategori.Text}'
                    order by a.Kode_Sub_Kategori_Jenis, b.Kode_Kategori_Jenis
                "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Swith_AutoComplete = True
                        Txt_Sub_Kategori_Jenis.Text = Dr("Kode_Sub_Kategori_Jenis")
                        Txt_Kategori_Jenis.Text = Dr("Kode_Kategori_Jenis")
                        Txt_Keterangan_Kategori_Jenis.Text = Dr("Keterangan")
                        'Txt_ID_Sub_Kategori.Text = Dr("id_sub_kategori_jenis")
                        Swith_AutoComplete = False
                        Txt_Keterangan.Focus()
                    Else
                        MessageBox.Show("Sub Kategori Jenis tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Sub_Kategori_Jenis.Text = ""
                        Txt_Kategori_Jenis.Text = ""
                        Txt_Keterangan_Kategori_Jenis.Text = ""
                        Txt_Sub_Kategori_Jenis.Focus()
                    End If


                    Lv_Sub_Kategori_Jenis.Visible = False
                    Lv_Sub_Kategori_Jenis.Location = New Point(1200, 267)
                End Using
            Else
                Txt_Keterangan.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Sub_Kategori_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Sub_Kategori_Jenis.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Sub_Kategori_Jenis.Text.Trim.Length = 0 Then Txt_Sub_Kategori_Jenis.Focus()
            Txt_Sub_Kategori_Jenis_Leave(Txt_Sub_Kategori_Jenis, e)

            Lv_Sub_Kategori_Jenis.Visible = False
            Lv_Sub_Kategori_Jenis.Location = New Point(1200, 267)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Cmb_Kode_Kategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kode_Kategori.SelectedIndexChanged
        Txt_Prefix.Text = ""

        If Txt_Kode_Sub.Text.Trim.Length <> 0 Then
            Txt_Kode_Sub_Leave(Cmb_Kategori_Gudang, e)
        End If
    End Sub



    Private Sub Txt_Kode_Sub_Leave(sender As Object, e As EventArgs) Handles Txt_Kode_Sub.Leave
        If Txt_Kode_Sub.Text.Trim.Length = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select b.Kode_Kategori_Jenis, b.Keterangan as Kategori_Jenis, a.Kode_Sub_Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, "
            SQL = SQL & "a.Prefix, a.Id_Sub_Kategori_Jenis, a.Id_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Sub_Kategori_Jenis = '" & tab1_Id_Sub_Kategori_Jenis & "' "
            'SQL = SQL & "and a.Kode_Kategori_Jenis = '" & arrkateogori.Item(ComboBox1.SelectedIndex) & "' "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis = '" & Txt_Kode_Sub.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Txt_Keterangan_Sub_Kategori.Text = dr("Sub_Kategori_Jenis")
                    Txt_Prefix.Text = dr("Prefix")
                    Txt_Kode_Sub.Enabled = False
                    Cmb_Kode_Kategori.Enabled = False

                    Btn_Simpan_Sub_Kategori.Tag = "UPDATE"
                    Btn_Simpan_Sub_Kategori.Text = "&Update"
                    Btn_Hapus_Sub_Kategori.Enabled = True
                Else
                    Txt_Kode_Sub.Enabled = True
                    Cmb_Kode_Kategori.Enabled = True
                    Txt_Keterangan_Sub_Kategori.Text = ""
                    'TextBox4.Text = ""
                    tab1_Id_Sub_Kategori_Jenis = ""
                    Btn_Simpan_Sub_Kategori.Tag = "SIMPAN"
                    Btn_Simpan_Sub_Kategori.Text = "&Simpan"
                    Btn_Hapus_Sub_Kategori.Enabled = True
                End If
            End Using

            If Btn_Simpan_Sub_Kategori.Tag = "SIMPAN" Then
                get_no_prefix()
                If xprefix = "0*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix > 99 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'If xprefix2 > 99 Then
                '    CloseConn()
                '    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                Txt_Prefix.Text = xprefix
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Sub_Kategori_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Sub_Kategori_Jenis.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Sub_Kategori_Jenis.Focus()
    End Sub

    Private Sub Lv_Sub_Kategori_Jenis_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Sub_Kategori_Jenis.DoubleClick
        If Lv_Sub_Kategori_Jenis.Items.Count = 0 Or Lv_Sub_Kategori_Jenis.FocusedItem.Index = -1 Then Exit Sub


        Dim Kd_Sub_Kategori As String = Lv_Sub_Kategori_Jenis.FocusedItem.SubItems(0).Text
        Dim Kd_Kategori_Jenis As String = Lv_Sub_Kategori_Jenis.FocusedItem.SubItems(1).Text
        Dim Keterangan As String = Lv_Sub_Kategori_Jenis.FocusedItem.SubItems(2).Text
        Dim ID As String = Lv_Sub_Kategori_Jenis.FocusedItem.SubItems(3).Text

        Swith_AutoComplete = True
        Txt_Sub_Kategori_Jenis.Text = Kd_Sub_Kategori
        Txt_Kategori_Jenis.Text = Kd_Kategori_Jenis
        Txt_Keterangan_Kategori_Jenis.Text = Keterangan
        Txt_ID_Sub_Kategori.Text = ID
        Swith_AutoComplete = False

        Txt_Sub_Kategori_Jenis_Leave(sender, e)

        Lv_Sub_Kategori_Jenis.Visible = False
        Lv_Sub_Kategori_Jenis.Location = New Point(1200, 267)

        Txt_Keterangan.Focus()
    End Sub

    Private Sub Lv_Sub_Kategori_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Sub_Kategori_Jenis.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Sub_Kategori_Jenis_DoubleClick(Lv_Sub_Kategori_Jenis, e)
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong_Tab_2()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            Kosong_Tab_1()
        ElseIf TabControl1.SelectedIndex = 1 Then
            Kosong_Tab_2()
        Else
            Kosong()
        End If
    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Get_Data_Lv(Lv_Data.FocusedItem.Index)

            SQL = $"
                select Kode_Kategori_Gudang, Id_Sub_Kategori_Jenis, Keterangan, Urut_Oto 
                from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Id_Kategori_Gudang = '{Lv_IdKategoriGudang}'
                and Id_Sub_Kategori_Jenis = '{Lv_IdSubKategoriGudang}'
                and Urut_Oto = '{Lv_Urut}'
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Swith_AutoComplete = True
                            Txt_Kd_Kategori.Text = .Rows(i).Item("Kode_Kategori_Gudang")
                            Swith_AutoComplete = False
                            Txt_Kd_Kategori_Leave(sender, New EventArgs)

                            If Txt_Kd_Kategori.Text.Trim.Length <> 0 Then
                                Swith_AutoComplete = True
                                Txt_ID_Sub_Kategori.Text = .Rows(i).Item("Id_Sub_Kategori_Jenis")
                                Swith_AutoComplete = False
                                Txt_Sub_Kategori_Jenis_Leave(sender, New EventArgs)


                                Txt_Keterangan.Text = .Rows(i).Item("Keterangan")

                                SelectedUrut = .Rows(i).Item("Urut_Oto")

                                Txt_Kd_Kategori.Enabled = False
                                Txt_Kd_Kategori.BackColor = Color.FromArgb(235, 235, 235)

                                Txt_Sub_Kategori_Jenis.Enabled = False
                                Txt_Sub_Kategori_Jenis.BackColor = Color.FromArgb(235, 235, 235)

                                Btn_Simpan.Tag = "UPDATE"
                                Btn_Simpan.Text = "&Update"

                            Else
                                Btn_Simpan.Tag = "SIMPAN"
                                Btn_Simpan.Text = "&Simpan"

                            End If

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


    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Kd_Kategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Kategori Gudang Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Kategori.Focus()
            Exit Sub
        ElseIf Txt_Sub_Kategori_Jenis.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Sub Kategori Jenis Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Sub_Kategori_Jenis.Focus()
            Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus()
            Exit Sub
        End If

        Dim Action As String = ""

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag.ToString.ToUpper = "SIMPAN" Then

                '======================================
                '=      CEK APAKAH DATA SUDAH ADA     =
                '======================================
                SQL = $"
                    select Kode_Perusahaan
                    from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Id_Kategori_Gudang  = '{Txt_ID_Kategori.Text.Trim}'
                   -- and urut_Oto = '{Txt_ID_Kategori.Text.Trim}'
                    and Id_Sub_Kategori_Jenis = '{Txt_ID_Sub_Kategori.Text.Trim}'
                "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Kode Kategori Gudang dan Sub Kategori Jenis Sudah Di Binding", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = $"
                    insert into N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain (Kode_Perusahaan, Kode_Kategori_Gudang, Keterangan, Tanggal, Jam, id_Kategori_Gudang, id_Sub_Kategori_Jenis)
                    values ('{KodePerusahaan}', '{Txt_Kd_Kategori.Text.Trim}', '{Txt_Keterangan.Text.Trim}', 
                    '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{Txt_ID_Kategori.Text.Trim}', '{Txt_ID_Sub_Kategori.Text.Trim}')
                "
                ExecuteTrans(SQL)


                Action = "Simpan"

            ElseIf Btn_Simpan.Tag.ToString.ToUpper = "UPDATE" Then

                '======================================
                '=      CEK APAKAH DATA SUDAH ADA     =
                '======================================
                SQL = $"
                    select Kode_Perusahaan
                    from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                    and Id_Sub_Kategori_Jenis = '{Txt_ID_Sub_Kategori.Text.Trim}'
                    and urut_oto = '{SelectedUrut}'
                "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Kode Kategori Gudang dan Sub Kategori Jenis Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = $"
                    update N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                    set Keterangan = '{Txt_Keterangan.Text.Trim}' 
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                    and Id_Sub_Kategori_Jenis = '{Txt_ID_Sub_Kategori.Text.Trim}'
                    and urut_oto = '{SelectedUrut}'
                "
                ExecuteTrans(SQL)


                Action = "Update"

            End If


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show($"Data Berhasil Di{Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Kosong()

    End Sub

    Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click
        If SelectedUrut.Trim.Length = 0 Then
            MessageBox.Show("Harap Pilih Dahulu Data Yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_Data.Focus()
            Exit Sub
        End If

        If MessageBox.Show("Yakin Ingin Menghapus Data Binding Ini??", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '===========================
            '=     CEK ROLE BUTTON     =
            '===========================
            If CekButtonRole("Hapus_Binding_Kategori_Gudang_Barang_Lain") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Menghapus Binding Kategori Gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '===============================
            '=     CEK APAKAH DATA ADA     =
            '===============================
            SQL = $"
                select Kode_Kategori_Gudang, Id_Sub_Kategori_Jenis, Keterangan
                from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                and Id_Sub_Kategori_Jenis = '{Txt_ID_Sub_Kategori.Text.Trim}'
                and urut_oto = '{SelectedUrut}'
            "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data Binding Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            SQL = $"
                Delete
                from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                and Id_Sub_Kategori_Jenis = '{Txt_ID_Sub_Kategori.Text.Trim}'
                and urut_oto = '{SelectedUrut}'
            "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("Data Binding Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Kosong()


    End Sub




    Private Sub Btn_Simpan_Sub_Kategori_Click(sender As Object, e As EventArgs) Handles Btn_Simpan_Sub_Kategori.Click
        If Cmb_Kategori_Gudang.SelectedIndex = -1 Then
            MessageBox.Show("Harap pilih kategori gudang terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Kategori_Gudang.DroppedDown = True
            Cmb_Kategori_Gudang.Focus()
            Exit Sub
        ElseIf Cmb_Kode_Kategori.SelectedIndex = -1 Then
            MessageBox.Show("Harap pilih kategori terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Kode_Kategori.DroppedDown = True
            Cmb_Kode_Kategori.Focus()
            Exit Sub
        ElseIf Txt_Kode_Sub.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Sub tidak boleh kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kode_Sub.Focus()
            Exit Sub
        ElseIf Txt_Keterangan_Sub_Kategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan tidak boleh kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan_Sub_Kategori.Focus()
            Exit Sub
        ElseIf Txt_Prefix.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix tidak boleh kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Prefix.Focus()
            Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            If Btn_Simpan_Sub_Kategori.Tag = "SIMPAN" Then

                SQL = $"
                    SELECT Kode_Sub_Kategori_Jenis, Keterangan
                    FROM N_EMI_Master_Sub_Kategori_Jenis
                    WHERE Kode_Perusahaan = '{KodePerusahaan}'
                    AND id_kategori_jenis = '{arrIDKategori(Cmb_Kode_Kategori.SelectedIndex)}' 
                    AND (
	                    Kode_Sub_Kategori_Jenis = '{Txt_Kode_Sub.Text.Trim}' OR prefix = '{Txt_Prefix.Text.Trim}'
                    )
                "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Kode Sub Kategori Sudah Pernah Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                get_no_prefix()
                If xprefix = "0*" Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf xprefix > 99 Then
                    CloseConn()
                    MessageBox.Show("Jumlah Prefix untuk sub kategori sudah maximal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                Txt_Prefix.Text = xprefix

                SQL = "insert into N_EMI_Master_Sub_Kategori_Jenis(Kode_Perusahaan, Id_Kategori_Jenis, Kode_Sub_Kategori_Jenis, Keterangan, Prefix) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & arrIDKategori(Cmb_Kode_Kategori.SelectedIndex) & "', '" & Txt_Kode_Sub.Text.Trim.ToUpper & "', '" & Txt_Keterangan_Sub_Kategori.Text.Trim.ToUpper & "', "
                SQL = SQL & "'" & Txt_Prefix.Text.Trim & "' ) "
                ExecuteTrans(SQL)

                '========================
                '=     AUTO BINDING     =
                '========================
                '======================================
                '=      CEK APAKAH DATA SUDAH ADA     =
                '======================================
                SQL = $"
                    select a.Kode_Perusahaan
                    from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain a
                    where a.Kode_Perusahaan = '{KodePerusahaan}'
                    and a.status is null
                    and a.Id_Kategori_Gudang = '{arrIDKategoriGudang(Cmb_Kategori_Gudang.SelectedIndex)}'
                    and EXISTS (
	                    select 1
	                    FROM N_EMI_Master_Sub_Kategori_Jenis z
	                    where z.Kode_Perusahaan = a.kode_perusahaan
	                    AND z.Kode_Sub_Kategori_Jenis = '{Txt_Kode_Sub.Text}'
	                    AND z.id_kategori_jenis = '{arrIDKategori(Cmb_Kode_Kategori.SelectedIndex)}'
	                    and z.Prefix = '{Txt_Prefix.Text.Trim}'
	                    and z.Id_Sub_Kategori_Jenis = a.Id_Sub_Kategori_Jenis
                    )
                "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Kode Kategori Gudang dan Sub Kategori Jenis Sudah Di Binding", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                '========================================
                '=     GET DETAIL DATA SUB KATEGORI     =
                '========================================
                Dim id_sub_kategori_jenis_insert As String
                SQL = $"
                    select Id_Sub_Kategori_Jenis
                    FROM N_EMI_Master_Sub_Kategori_Jenis
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    AND Kode_Sub_Kategori_Jenis = '{Txt_Kode_Sub.Text.Trim}'
                    AND id_kategori_jenis = '{arrIDKategori(Cmb_Kode_Kategori.SelectedIndex)}'
                    and Prefix = '{Txt_Prefix.Text.Trim}'
                "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        id_sub_kategori_jenis_insert = Dr("Id_Sub_Kategori_Jenis")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Data Sub Kategori Jenis Gagal Diinsert, Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                SQL = $"
                    insert into N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain (Kode_Perusahaan, Kode_Kategori_Gudang, Keterangan, Tanggal, Jam, id_Kategori_Gudang, id_Sub_Kategori_Jenis)
                    values ('{KodePerusahaan}', '{Cmb_Kategori_Gudang.Text}', '{Txt_Keterangan_Sub_Kategori.Text.Trim}', 
                    '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', 
                    '{arrIDKategoriGudang(Cmb_Kategori_Gudang.SelectedIndex)}', '{id_sub_kategori_jenis_insert}')
                "
                ExecuteTrans(SQL)


                MessageBox.Show($"Data Berhasil DiSimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else
                '=========================
                '=     FUNGSI UPDATE     =
                '=========================

                'SQL = "select Kode_Sub_Kategori_Jenis, Keterangan from N_EMI_Master_Sub_Kategori_Jenis "
                'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and upper(Keterangan) = '" & TextBox6.Text.Trim.ToUpper & "' and Id_Sub_Kategori_Jenis <> '" & xid_sub_kategori & "' "
                'Using dr = OpenTrans(SQL)
                '    If dr.Read Then
                '        dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("sub ketegori sudah pernah di simpan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                ''SQL = "select Id_Sub_Kategori_Jenis from N_EMI_Master_Sub_Kategori_Jenis_1 "
                ''SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                ''SQL = SQL & "and Id_Sub_Kategori_Jenis = '" & xid_sub_kategori & "' "
                ''Using dr = OpenTrans(SQL)
                ''    If dr.Read Then
                ''        dr.Close()
                ''        CloseTrans()
                ''        CloseConn()
                ''        MessageBox.Show("sub ketegori sudah pernah dipakai di tabel N_EMI_Master_Sub_Kategori_Jenis_1 ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ''        Exit Sub
                ''    End If
                ''End Using

                'SQL = "update N_EMI_Master_Sub_Kategori_Jenis set "
                'SQL = SQL & "Keterangan = '" & TextBox6.Text.Trim.ToUpper & "' "
                ''SQL = SQL & "Prefix = '" & TextBox4.Text.Trim & "' "
                'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and Id_Sub_Kategori_Jenis = '" & xid_sub_kategori & "' "
                ''SQL = SQL & "and Kode_Kategori_Jenis = '" & arrkateogori.Item(ComboBox1.SelectedIndex) & "' "
                ''SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & TextBox7.Text & "' "
                'ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong_Tab_1()
    End Sub

    Private Sub Btn_Hapus_Sub_Kategori_Click(sender As Object, e As EventArgs) Handles Btn_Hapus_Sub_Kategori.Click

    End Sub

    Private Sub Btn_Refresh_Sub_Kategori_Click(sender As Object, e As EventArgs) Handles Btn_Refresh_Sub_Kategori.Click
        Kosong_Tab_1()
    End Sub


    Private Sub get_no_prefix()
        SQL = "SELECT RIGHT('00' + CAST(ISNULL(MAX(CAST(Prefix AS INT)), 0) + 1 AS VARCHAR(2)), 2) AS NextPrefix "
        SQL = SQL & "FROM N_EMI_Master_Sub_Kategori_Jenis WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "AND Id_Kategori_Jenis = '" & arrIDKategori(Cmb_Kode_Kategori.SelectedIndex) & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                xprefix = Dr("NextPrefix")
            End If
        End Using

    End Sub









    '==========================================================================================================================================================================
    '=     CEGAH MAXIMIZED DOUBLE KLIK TITLE BAR
    '==========================================================================================================================================================================

    Protected Overrides Sub WndProc(ByRef m As Message)

        If m.Msg = &HA3 Then
            Return
        End If

        MyBase.WndProc(m)
    End Sub


    Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_Data.Cursor = Cursors.Hand
        Else
            Lv_Data.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Lv_Data_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data.MouseLeave
        Lv_Data.Cursor = Cursors.Default
    End Sub


End Class
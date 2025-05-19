Imports System.Globalization

Public Class Master_Routing

    Dim arrIdJnsProduk, arrIdWorkCenter, arrKdWorkCenter, arrCari, arrKd_biaya, arrKeterangan As New ArrayList
    Dim Jenis = "Master_Routing"

    Dim LvIdWorkCenter As String
    Dim LvKdWorkCenter As String
    Dim LvKeteranganWorkCenter As String

    Dim x_nomor As Integer = 0

    Public Sub Get_Isi_Listview_WorkCenter(ByVal No_Index As Integer)
        LvIdWorkCenter = Lv_DataWorkCenter.Items(No_Index).Text
        LvKdWorkCenter = Lv_DataWorkCenter.Items(No_Index).SubItems(1).Text
        LvKeteranganWorkCenter = Lv_DataWorkCenter.Items(No_Index).SubItems(2).Text
    End Sub

    Private Sub Master_Biaya_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Biaya_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Lbl_Judul.Text = Base_Language.Lang_Master_Routing

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Btn_Simpan.Tag = "&Simpan"
            Btn_Hapus.Enabled = False

            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            Lv_DataWorkCenter.Columns.Clear()
            Lv_DataWorkCenter.Columns.Add("ID", 0, HorizontalAlignment.Left)
            Lv_DataWorkCenter.Columns.Add(Base_Language.Lang_Global_Kode, 200, HorizontalAlignment.Left)
            Lv_DataWorkCenter.Columns.Add(Base_Language.lang_global_keterangan, 177, HorizontalAlignment.Left)
            Lv_DataWorkCenter.View = View.Details

            Lv_Routing.Columns.Clear()
            Lv_Routing.Columns.Add("ID", 0, HorizontalAlignment.Left) '0
            Lv_Routing.Columns.Add(Base_Language.Lang_Global_Kode, 100, HorizontalAlignment.Left) '1
            Lv_Routing.Columns.Add(Base_Language.lang_global_keterangan, 137, HorizontalAlignment.Left) '2
            Lv_Routing.Columns.Add("Prefix Code", 100, HorizontalAlignment.Left) '3
            Lv_Routing.Columns.Add(Base_Language.Lang_Global_Jenis_Produk, 100, HorizontalAlignment.Left) '4
            Lv_Routing.Columns.Add("BarColor", 100, HorizontalAlignment.Left) '5
            Lv_Routing.Columns.Add("BackColor", 100, HorizontalAlignment.Left) '6
            Lv_Routing.Columns.Add("Qty Per Batch", 120, HorizontalAlignment.Right) '7
            Lv_Routing.Columns.Add("Satuan", 100, HorizontalAlignment.Center) '8
            Lv_Routing.View = View.Details

            Lv_RoutingDetail.Columns.Clear()
            Lv_RoutingDetail.Columns.Add("ID", 0, HorizontalAlignment.Left)
            Lv_RoutingDetail.Columns.Add("Data Work Center", 237, HorizontalAlignment.Left)
            Lv_RoutingDetail.View = View.Details

            kosong()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong()
        Lbl_IdRouting.Text = ""
        Txt_Kd.Text = ""
        Txt_Keterangan.Text = ""
        Cmb_Kolom.SelectedIndex = -1
        Txt_Value.Text = ""
        Txt_PrefixCode.Text = ""

        Txt_Qty_PerBatch.Text = ""

        Cmb_Kolom.Items.Clear() : arrCari.Clear() : Cmb_Kolom.SelectedIndex = -1
        Cmb_Kolom.Items.Add(Base_Language.Lang_Global_Kode) : arrCari.Add("a.Kode_Routing")
        Cmb_Kolom.Items.Add(Base_Language.lang_global_keterangan) : arrCari.Add("b.Keterangan")
        Cmb_Kolom.Items.Add("Prefix Code") : arrCari.Add("a.prefix_code")
        Cmb_Kolom.Items.Add(Base_Language.Lang_Global_Jenis_Produk) : arrCari.Add("b.Keterangan")

        Txt_Kd.Enabled = True
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

        Try
            OpenConn()

            Cmb_SatuanBatch.Text = "" : Cmb_SatuanBatch.Items.Clear()
            SQL = "select Kode_Perusahaan, Satuan from EMI_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Cmb_SatuanBatch.Items.Add(Dr("Satuan"))
                Loop

                Cmb_SatuanBatch.SelectedItem = "KG"
            End Using

            Cmb_JnsProduk.Items.Clear() : arrIdJnsProduk.Clear()
            SQL = "Select id_jenis_produk, keterangan from emi_jenis_produk "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' order by id_jenis_produk "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_JnsProduk.Items.Add(Dr("keterangan")) : arrIdJnsProduk.Add(Dr("id_jenis_produk"))
                Loop
            End Using

            Txt_Barcolor.Text = ""
            Txt_Backcolor.Text = ""
            Btn_DisplayWarna.BackColor = Color.White
            Btn_DisplayWarnaBackColor.BackColor = Color.White

            Cmb_DataWorkCenter.Items.Clear() : arrIdWorkCenter.Clear() : arrKdWorkCenter.Clear()
            SQL = "Select id_work_center, kode_work_center, keterangan from emi_master_work_center "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' order by id_work_center "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_DataWorkCenter.Items.Add(Dr("keterangan")) : arrIdWorkCenter.Add(Dr("id_work_center")) : arrKdWorkCenter.Add(Dr("kode_work_center"))
                Loop
            End Using

            Lv_DataWorkCenter.Items.Clear()
            Lv_Routing.Items.Clear() : Lv_RoutingDetail.Items.Clear()
            'SQL = "Select id_routing, kode_routing, keterangan, prefix_code "
            'SQL = SQL & "from emi_master_routing "
            'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "order by id_routing "
            SQL = "Select a.id_routing, a.kode_routing, a.keterangan, a.prefix_code, "
            SQL = SQL & "a.id_jenis_produk, b.keterangan as nama_produk, a.barcolor, a.backcolor, a.Qty_PerBatch, a.Satuan "
            SQL = SQL & "from emi_master_routing a, emi_jenis_produk b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.id_jenis_produk = b.id_jenis_produk "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Routing.Items.Add(Dr("id_routing"))
                    lvw.SubItems.Add(Dr("kode_routing"))
                    lvw.SubItems.Add(Dr("keterangan"))
                    lvw.SubItems.Add(Dr("prefix_code"))
                    lvw.SubItems.Add(Dr("nama_produk"))
                    lvw.SubItems.Add(Dr("barcolor"))
                    lvw.SubItems.Add(Dr("backcolor"))
                    lvw.SubItems.Add(If(General_Class.CekNULL(Dr("Qty_PerBatch")) = "", 0, Dr("Qty_PerBatch")))
                    lvw.SubItems.Add(If(General_Class.CekNULL(Dr("Satuan")) = "", "", Dr("Satuan")))
                    '
                    'Dim barColor As String = Dr("barcolor").ToString()
                    'Dim backColor As String = Dr("backcolor").ToString()

                    'Dim barColorValue As Color = ConvertRGBToColor(barColor)
                    'Dim backColorValue As Color = ConvertRGBToColor(backColor)

                    'lvw.BackColor = backColorValue
                    'lvw.ForeColor = barColorValue
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Function ConvertRGBToColor(rgbString As String) As Color
        Try
            Dim rgbValues As String() = rgbString.Split(","c)

            Dim red As Integer = Integer.Parse(rgbValues(0).Trim())
            Dim green As Integer = Integer.Parse(rgbValues(1).Trim())
            Dim blue As Integer = Integer.Parse(rgbValues(2).Trim())

            'If red < 0 OrElse red > 255 OrElse green < 0 OrElse green > 255 OrElse blue < 0 OrElse blue > 255 Then
            '    Throw New ArgumentException("Nilai RGB di luar rentang.")
            'End If

            Return Color.FromArgb(red, green, blue)
        Catch ex As Exception
            Return Color.Black
        End Try
    End Function


    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs)
        If Txt_Kd.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Kode & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Kd.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Keterangan.Focus() : Exit Sub
        ElseIf Txt_PrefixCode.Text.Trim.Length = 0 Then
            MessageBox.Show("Prefix Code" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_PrefixCode.Focus() : Exit Sub
        ElseIf Cmb_JnsProduk.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Produk" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_JnsProduk.Focus() : Exit Sub
        ElseIf Txt_Barcolor.Text.Trim.Length = 0 Then
            MessageBox.Show("Barcolor" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Barcolor.Focus() : Exit Sub
        ElseIf Txt_Backcolor.Text.Trim.Length = 0 Then
            MessageBox.Show("Backcolor" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Backcolor.Focus() : Exit Sub
        ElseIf Lv_DataWorkCenter.Items.Count = -1 Then
            MessageBox.Show("Data work center harus diisi." & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        ElseIf Txt_Qty_PerBatch.Text.Trim.Length = 0 Then
            MessageBox.Show("Qty Per Batch harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag = "&Simpan" Then

                SQL = "Insert Into emi_master_routing(Kode_Perusahaan, kode_routing, keterangan, prefix_code, "
                SQL = SQL & "id_jenis_produk, barcolor, backcolor, Qty_PerBatch, Satuan)"
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_Kd.Text.Trim & "',"
                SQL = SQL & "'" & Txt_Keterangan.Text.Trim & "', '" & Txt_PrefixCode.Text.Trim & "',"
                SQL = SQL & "'" & arrIdJnsProduk.Item(Cmb_JnsProduk.SelectedIndex) & "',"
                SQL = SQL & "'" & Txt_Barcolor.Text.Trim & "', '" & Txt_Backcolor.Text.Trim & "', "
                SQL = SQL & "'" & Txt_Qty_PerBatch.Text & "', '" & Cmb_SatuanBatch.Text & "' )"
                ExecuteTrans(SQL)

                SQL = "select IDENT_CURRENT('emi_master_routing') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        x_nomor = " " & Dr("urutan") & " "
                    End If
                End Using

                For i As Integer = 0 To Lv_DataWorkCenter.Items.Count - 1
                    Get_Isi_Listview_WorkCenter(i)
                    SQL = "Insert into emi_master_routing_detail "
                    SQL = SQL & "(kode_perusahaan, id_routing, id_work_center) "
                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & x_nomor & "', '" & LvIdWorkCenter & "' ) "
                    ExecuteTrans(SQL)
                Next

            Else
                '
                SQL = "update emi_master_routing set "
                SQL = SQL & "keterangan =  '" & Txt_Keterangan.Text.Trim & "', "
                SQL = SQL & "prefix_code = '" & Txt_PrefixCode.Text.Trim & "', "
                SQL = SQL & "Qty_PerBatch = '" & Txt_Qty_PerBatch.Text & "', "
                SQL = SQL & "Satuan = '" & Cmb_SatuanBatch.Text & "', "
                SQL = SQL & "id_jenis_produk = '" & arrIdJnsProduk.Item(Cmb_JnsProduk.SelectedIndex) & "', "
                SQL = SQL & "barcolor = '" & Txt_Barcolor.Text.Trim & "', backcolor = '" & Txt_Backcolor.Text.Trim & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and id_routing = '" & Lbl_IdRouting.Text & "' "
                ExecuteTrans(SQL)
                '
                SQL = "delete from emi_master_routing_detail where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and id_routing = '" & Lbl_IdRouting.Text.Trim & "' "
                ExecuteTrans(SQL)
                '
                For i As Integer = 0 To Lv_DataWorkCenter.Items.Count - 1
                    Get_Isi_Listview_WorkCenter(i)
                    SQL = "Insert into emi_master_routing_detail "
                    SQL = SQL & "(kode_perusahaan, id_routing,id_work_center) "
                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Lbl_IdRouting.Text.Trim & "', '" & LvIdWorkCenter & "' ) "
                    ExecuteTrans(SQL)
                Next
            End If

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
        Txt_Kd.Focus()
    End Sub

    Private Sub Lv_MasterBiaya_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Routing.SelectedIndexChanged
        Dim getIDRouting As String = Lv_Routing.FocusedItem.Text

        Lv_RoutingDetail.Items.Clear()

        Try
            OpenConn()

            Dim lvw As New ListViewItem
            SQL = "select b.id_routing, c.keterangan "
            SQL = SQL & "from emi_master_routing a,  emi_master_routing_detail b, emi_master_work_center c "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Routing = b.id_routing "
            SQL = SQL & "and a.Id_Routing = '" & getIDRouting & "' "
            SQL = SQL & "and b.id_work_center = c.Id_Work_Center"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lvw = Lv_RoutingDetail.Items.Add(Dr("id_routing"))
                    lvw.SubItems.Add(Dr("keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_MasterBiaya_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Routing.DoubleClick
        If Lv_Routing.Items.Count = 0 Then Exit Sub

        Lbl_IdRouting.Text = Lv_Routing.FocusedItem.Text
        Txt_Kd.Text = Lv_Routing.FocusedItem.SubItems(1).Text
        Txt_Keterangan.Text = Lv_Routing.FocusedItem.SubItems(2).Text
        Txt_Qty_PerBatch.Text = Lv_Routing.FocusedItem.SubItems(7).Text

        Try
            OpenConn()
            Lv_DataWorkCenter.Items.Clear()
            SQL = "select a.id_work_center, b.kode_work_center, b.keterangan  "
            SQL = SQL & "from emi_master_routing_detail a, emi_master_work_center b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.id_work_center = b.id_work_center "
            SQL = SQL & "and id_routing = '" & Lbl_IdRouting.Text & "' "
            SQL = SQL & "order by a.id_work_center "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_DataWorkCenter.Items.Add(Dr("id_work_center"))
                    Lvw.SubItems.Add(Dr("kode_work_center"))
                    Lvw.SubItems.Add(Dr("keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Txt_Kd_Leave(Lv_Routing, e)
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Cmb_Kolom.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Kolom & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_Kolom.Focus() : Exit Sub
        ElseIf Txt_Value.Text.Trim.Length = 0 Then
            MessageBox.Show("Value" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Value.Focus() : Exit Sub
        End If

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try

            OpenConn()

            Lv_Routing.Items.Clear()
            'SQL = "select id_routing, kode_routing, keterangan, prefix_code "
            'SQL = SQL & "from emi_master_routing where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = "Select a.id_routing, a.kode_routing, a.keterangan, a.prefix_code, "
            SQL = SQL & "a.id_jenis_produk, b.keterangan as nama_produk, a.barcolor, a.backcolor "
            SQL = SQL & "from emi_master_routing a, emi_jenis_produk b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.id_jenis_produk = b.id_jenis_produk "
            If semua = "T" Then
                SQL = SQL & "and " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Txt_Value.Text & "%' "
                SQL = SQL & "order by " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " "
            Else
                SQL = SQL & "order by a.id_routing"
            End If
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim lvw As ListViewItem
            '        lvw = Lv_Routing.Items.Add(Dr("id_routing"))
            '        lvw.SubItems.Add(Dr("kode_routing"))
            '        lvw.SubItems.Add(Dr("keterangan"))
            '        lvw.SubItems.Add(Dr("prefix_code"))
            '    Loop
            'End Using
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Routing.Items.Add(Dr("id_routing"))
                    lvw.SubItems.Add(Dr("kode_routing"))
                    lvw.SubItems.Add(Dr("keterangan"))
                    lvw.SubItems.Add(Dr("prefix_code"))
                    lvw.SubItems.Add(Dr("nama_produk"))
                    lvw.SubItems.Add(Dr("barcolor"))
                    lvw.SubItems.Add(Dr("backcolor"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Leave(sender As Object, e As EventArgs)
        If Txt_Kd.Text.Trim.Length = 0 Then Exit Sub
        Lbl_IdRouting.Enabled = False
        Txt_Kd.Enabled = False

        Try
            OpenConn()

            SQL = "Select a.id_routing, a.kode_routing, a.keterangan, a.prefix_code, "
            SQL = SQL & "a.id_jenis_produk, b.keterangan as nama_produk, a.barcolor, a.backcolor "
            SQL = SQL & "from emi_master_routing a, emi_jenis_produk b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.id_jenis_produk = b.id_jenis_produk "
            SQL = SQL & "and a.kode_routing = '" & Txt_Kd.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Lbl_IdRouting.Text = Dr("id_routing")
                    Txt_Kd.Text = Dr("kode_routing")
                    Txt_Keterangan.Text = Dr("keterangan")
                    Txt_PrefixCode.Text = Dr("prefix_code")
                    Cmb_JnsProduk.Text = Dr("nama_produk")
                    Txt_Barcolor.Text = Dr("barcolor")
                    Dim getDataBarColor As String = Dr("barcolor")
                    Dim barcolorValue As String() = getDataBarColor.Split(","c)
                    Dim redBar As Integer = Integer.Parse(barcolorValue(0).Trim())
                    Dim greenBar As Integer = Integer.Parse(barcolorValue(1).Trim())
                    Dim blueBar As Integer = Integer.Parse(barcolorValue(2).Trim())
                    Dim buttonBarColor As Color = Color.FromArgb(redBar, greenBar, blueBar)
                    Btn_DisplayWarna.BackColor = buttonBarColor

                    Txt_Backcolor.Text = Dr("backcolor")
                    Dim getDataBackColor As String = Dr("backcolor")
                    Dim backcolorValue As String() = getDataBackColor.Split(","c)
                    Dim redBack As Integer = Integer.Parse(backcolorValue(0).Trim())
                    Dim greenBack As Integer = Integer.Parse(backcolorValue(1).Trim())
                    Dim blueBack As Integer = Integer.Parse(backcolorValue(2).Trim())
                    Dim buttonBackColor As Color = Color.FromArgb(redBack, greenBack, blueBack)
                    Btn_DisplayWarnaBackColor.BackColor = buttonBackColor

                    Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
                    Btn_Simpan.Tag = "&Update"
                Else
                    Lbl_IdRouting.Text = ""
                    Txt_Keterangan.Text = ""
                    Txt_PrefixCode.Text = ""
                    Cmb_JnsProduk.SelectedIndex = -1
                    Txt_Barcolor.Text = ""
                    Txt_Backcolor.Text = ""
                    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
                    Txt_Kd.Enabled = True
                    Btn_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs)
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus1 = vbYes Then

            Try

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "delete from emi_master_routing where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and id_routing = '" & Lbl_IdRouting.Text.Trim & "' "
                ExecuteTrans(SQL)

                SQL = "delete from emi_master_routing_detail where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and id_routing = '" & Lbl_IdRouting.Text.Trim & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong()
        Txt_Kd.Focus()
    End Sub


    Private Sub Txt_Kd_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub


    Private Sub Btn_WorkCenter_Click(sender As Object, e As EventArgs)
        If Cmb_DataWorkCenter.Text.Trim.Length = 0 Then
            MessageBox.Show("Data work center harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_DataWorkCenter.Focus() : Exit Sub
        ElseIf Txt_Qty_PerBatch.Text.Trim.Length = 0 Then
            MessageBox.Show("Qty Per Batch harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Qty_PerBatch.Focus() : Exit Sub
        End If

        For i As Integer = 0 To Lv_DataWorkCenter.Items.Count - 1
            Get_Isi_Listview_WorkCenter(i)
            If arrIdWorkCenter.Item(Cmb_DataWorkCenter.SelectedIndex) = LvIdWorkCenter Then
                MessageBox.Show("Data sudah ditambahkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        Dim lvw As New ListViewItem
        lvw = Lv_DataWorkCenter.Items.Add(arrIdWorkCenter.Item(Cmb_DataWorkCenter.SelectedIndex))
        lvw.SubItems.Add(arrKdWorkCenter.Item(Cmb_DataWorkCenter.SelectedIndex))
        lvw.SubItems.Add(Cmb_DataWorkCenter.Text)

    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Txt_PrefixCode.Focus()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim colorDialog As New ColorDialog()

        If colorDialog.ShowDialog() = DialogResult.OK Then
            Dim selectedColor As Color = colorDialog.Color

            Dim rgbValue As String = $"{selectedColor.R}, {selectedColor.G}, {selectedColor.B}"

            Txt_Barcolor.Text = rgbValue

            Btn_DisplayWarna.BackColor = selectedColor
        End If
    End Sub

    Private Sub Btn_PilihBackColor_Click(sender As Object, e As EventArgs)
        Dim colorDialog As New ColorDialog()

        If colorDialog.ShowDialog() = DialogResult.OK Then
            Dim selectedColor As Color = colorDialog.Color

            Dim rgbValue As String = $"{selectedColor.R}, {selectedColor.G}, {selectedColor.B}"

            Txt_Backcolor.Text = rgbValue

            Btn_DisplayWarnaBackColor.BackColor = selectedColor
        End If
    End Sub

    Private Sub Txt_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub Txt_Kd_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Txt_Keterangan_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Txt_PrefixCode_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Cmb_JnsProduk_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Lv_DataWorkCenter_DoubleClick(sender As Object, e As EventArgs)
        If Lv_DataWorkCenter.SelectedItems.Count > 0 Then
            Lv_DataWorkCenter.Items.Remove(Lv_DataWorkCenter.SelectedItems(0))
        Else
            MessageBox.Show("Tidak ada data yang dipilih.")
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Cmb_JnsProduk.Focus()
    End Sub
    Private Sub Txt_Qty_PerBatch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Qty_PerBatch.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_SatuanBatch.Focus()
    End Sub

    Private Sub Cmb_DataWorkCenter_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Btn_WorkCenter.Focus()
    End Sub

    Private Sub Cmb_DataWorkCenter_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Txt_Qty_PerBatch_Leave(sender As Object, e As EventArgs)
        If Txt_Qty_PerBatch.Text.Trim.Length = 0 Then Exit Sub
        If Not IsNumeric(Txt_Qty_PerBatch.Text) Then
            Txt_Qty_PerBatch.Text = ""
        End If

        Dim culture As CultureInfo = CultureInfo.CurrentCulture
        Dim cellKuantity As String = Txt_Qty_PerBatch.Text
        If cellKuantity.Contains(",") Then
            MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Qty_PerBatch.Text = ""
            Exit Sub
        End If

        Dim nilai As Decimal = Decimal.Parse(cellKuantity)
        Dim formattedValue As String = nilai.ToString("N2", culture)

        Txt_Qty_PerBatch.Text = formattedValue

    End Sub

    Private Sub Lv_Routing_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Routing.DrawSubItem
        If e.ColumnIndex = 5 OrElse e.ColumnIndex = 6 Then
            Dim color As Color = ConvertRGBToColor(e.SubItem.Text)

            Using brush As New SolidBrush(color)
                e.Graphics.FillRectangle(brush, e.Bounds)
            End Using

            e.Graphics.DrawRectangle(Pens.Black, e.Bounds)

            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.SubItem.Font, e.Bounds, Color.Black)
        Else
            e.DrawDefault = True
        End If
    End Sub

    Private Sub Txt_Qty_PerBatch_Enter(sender As Object, e As EventArgs)
        Dim cellKuantity As String = Txt_Qty_PerBatch.Text

        If cellKuantity = "" Then
            Exit Sub
        End If

        Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
        Dim nilai As Decimal = Decimal.Parse(cleanedStr)

        Txt_Qty_PerBatch.Text = nilai
    End Sub

    Private Sub Lv_Routing_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Routing.DrawColumnHeader
        e.DrawDefault = True
    End Sub

    Private Sub Cmb_JnsProduk_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Btn_PilihBarColor.Focus()
    End Sub

    Private Sub Btn_PilihBarColor_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Btn_PilihBarColor.Focus()
    End Sub

    Private Sub Btn_PilihBackColor_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Cmb_DataWorkCenter.Focus()
    End Sub

    Private Sub Btn_WorkCenter_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

End Class
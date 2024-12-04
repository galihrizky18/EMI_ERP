Imports System.Net

Public Class Display_Kendaraan_Masuk_Tidak_Sesuai

    Dim arrSupplier As New ArrayList
    Dim lv_NoFaktur, lv_Supplier, lv_Lokasi, lv_NoSJ, lv_NoPlat, lv_Driver, lv_TglBerangkat, lv_TglMasuk As String
    Dim lv_JamMasuk, lv_UserPenerima, lv_KdSupplier As String

    Dim selected_NoFaktur, selected_KdSupplier, selected_Lokasi, selected_NoSJ, selected_PlatNomor, selected_Driver As String

    'Format(tgl_skg, "yyyy-MM-dd")

    Dim itemNoFaktur As Integer = 0
    Dim itemSupplier As Integer = 1
    Dim itemLokasi As Integer = 2
    Dim itemNoSJ As Integer = 3
    Dim itemNoPlat As Integer = 4
    Dim itemDriver As Integer = 5
    Dim itemTanggalBerangkat As Integer = 6
    Dim itemTanggalMasuk As Integer = 7
    Dim itemJamMasuk As Integer = 8
    Dim itemUserPenerima As Integer = 9
    Dim itemKdSupplier As Integer = 10

    Private Sub Display_Kendaraan_Masuk_Tidak_Sesuai_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()
        Initial_ListView()
        Load_Kendaraan()

    End Sub

    Private Sub Kosong()
        Cmb_Filter.Items.Clear()
        Cmb_Filter_Value.Items.Clear()

        Tb_NoSJ.Text = String.Empty
        Tb_NoPlat.Text = String.Empty
        Tb_Driver.Text = String.Empty

        Lv_Kendaraan.Text = String.Empty

        Txt_Lokasi.Text = String.Empty
        Txt_Supplier.Text = String.Empty

        Tb_NoSJ.Enabled = False
        Tb_NoPlat.Enabled = False
        Tb_Driver.Enabled = False

        selected_NoFaktur = String.Empty
        selected_NoSJ = String.Empty
        selected_PlatNomor = String.Empty
        selected_Driver = String.Empty
        selected_KdSupplier = String.Empty
        selected_Lokasi = String.Empty

        'ADD FILTER
        Cmb_Filter.Items.Add("Supplier")
        Cmb_Filter.SelectedIndex = 0
        Cmb_Filter_SelectedIndexChanged(Cmb_Filter, EventArgs.Empty)

        FlowLayoutPanel1.Controls.Clear()
        FlowLayoutPanel1.BackColor = Color.LightGray

        Lv_Kendaraan.Items.Clear()
    End Sub

    Private Sub Kosong_Sebagian()

        Tb_NoSJ.Text = String.Empty
        Tb_NoPlat.Text = String.Empty
        Tb_Driver.Text = String.Empty

        Lv_Kendaraan.Text = String.Empty

        Txt_Lokasi.Text = String.Empty
        Txt_Supplier.Text = String.Empty

        Tb_NoSJ.Enabled = False
        Tb_NoPlat.Enabled = False
        Tb_Driver.Enabled = False

        selected_NoFaktur = String.Empty
        selected_NoSJ = String.Empty
        selected_PlatNomor = String.Empty
        selected_Driver = String.Empty
        selected_KdSupplier = String.Empty
        selected_Lokasi = String.Empty

        FlowLayoutPanel1.Controls.Clear()
        FlowLayoutPanel1.BackColor = Color.LightGray

        Lv_Kendaraan.Items.Clear()
    End Sub

    Private Sub Initial_ListView()

        Lv_Kendaraan.Columns.Add("No Faktur", 130, HorizontalAlignment.Center)
        Lv_Kendaraan.Columns.Add("Supplier", 0, HorizontalAlignment.Center)
        Lv_Kendaraan.Columns.Add("Lokasi", 0, HorizontalAlignment.Center)
        Lv_Kendaraan.Columns.Add("No Surat Jalan", 130, HorizontalAlignment.Center)
        Lv_Kendaraan.Columns.Add("No Plat Kendaraan", 130, HorizontalAlignment.Center)
        Lv_Kendaraan.Columns.Add("Driver", 130, HorizontalAlignment.Center)
        Lv_Kendaraan.Columns.Add("Tanggal Berangkat", 0, HorizontalAlignment.Center)
        Lv_Kendaraan.Columns.Add("Tanggal Masuk", 140, HorizontalAlignment.Center)
        Lv_Kendaraan.Columns.Add("Jam Masuk", 130, HorizontalAlignment.Center)
        Lv_Kendaraan.Columns.Add("User Penerima", 0, HorizontalAlignment.Center)

        'Hideen
        Lv_Kendaraan.Columns.Add("kdSupplier", 0, HorizontalAlignment.Center)

        Lv_Kendaraan.View = View.Details

    End Sub

    Private Sub Get_Data_ListView(ByVal index As Integer)

        lv_NoFaktur = Lv_Kendaraan.Items(index).SubItems(itemNoFaktur).Text
        lv_Supplier = Lv_Kendaraan.Items(index).SubItems(itemSupplier).Text
        lv_Lokasi = Lv_Kendaraan.Items(index).SubItems(itemLokasi).Text
        lv_NoSJ = Lv_Kendaraan.Items(index).SubItems(itemNoSJ).Text
        lv_NoPlat = Lv_Kendaraan.Items(index).SubItems(itemNoPlat).Text
        lv_Driver = Lv_Kendaraan.Items(index).SubItems(itemDriver).Text
        lv_TglBerangkat = Lv_Kendaraan.Items(index).SubItems(itemTanggalBerangkat).Text
        lv_TglMasuk = Lv_Kendaraan.Items(index).SubItems(itemTanggalMasuk).Text
        lv_JamMasuk = Lv_Kendaraan.Items(index).SubItems(itemJamMasuk).Text
        lv_UserPenerima = Lv_Kendaraan.Items(index).SubItems(itemUserPenerima).Text
        lv_KdSupplier = Lv_Kendaraan.Items(index).SubItems(itemKdSupplier).Text

    End Sub

    Private Sub Load_Kendaraan(ByVal Optional filter As String = "")

        Lv_Kendaraan.Items.Clear()
        FlowLayoutPanel1.Controls.Clear()

        Try
            OpenConn()

            SQL = "select a.No_Faktur, a.Kode_Supplier, b.Nama as supplier, a.Lokasi, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal, a.Jam, a.UseriD, "
            SQL = SQL & "a.Tanggal_OTW "
            SQL = SQL & "from emi_pembelian_loading a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier=b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Flag_Dkmn_Tdk_Sesuai = 'Y' "
            If Not filter.Trim.Length = 0 Then
                SQL = SQL & "and " & filter & " "
            End If
            SQL = SQL & "order by Tanggal desc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As New ListViewItem
                    Lv = Lv_Kendaraan.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("supplier"))
                    Lv.SubItems.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("No_SJ"))
                    Lv.SubItems.Add(Dr("No_Plat"))
                    Lv.SubItems.Add(Dr("Driver"))
                    Lv.SubItems.Add(Format(Dr("Tanggal_OTW"), "yyyy-MM-dd"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "yyyy-MM-dd"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("UseriD"))

                    'Hidden
                    Lv.SubItems.Add(Dr("Kode_Supplier"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    'HANDLE FUNCTION
    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
        Load_Kendaraan()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter_Value.Items.Count = 0 Or Cmb_Filter_Value.SelectedIndex = -1 Or arrSupplier.Count = 0 Then Exit Sub

        Kosong_Sebagian()

        If Cmb_Filter_Value.SelectedIndex <> 0 Then
            Dim filter As String = "a.Kode_Supplier='" & arrSupplier(Cmb_Filter_Value.SelectedIndex) & "'"
            Load_Kendaraan(filter)
        Else
            Load_Kendaraan()
        End If


        'Tb_NoSJ.Text = String.Empty
        'Tb_NoPlat.Text = String.Empty
        'Tb_Driver.Text = String.Empty
        'Txt_Lokasi.Text = String.Empty
        'Txt_Supplier.Text = String.Empty

        'Tb_NoSJ.Enabled = False
        'Tb_NoPlat.Enabled = False
        'Tb_Driver.Enabled = False

    End Sub

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged

        If Cmb_Filter.Items.Count = 0 Or Cmb_Filter.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cmb_Filter_Value.Items.Clear() : arrSupplier.Clear()

            If Cmb_Filter.SelectedIndex = 0 Then
                SQL = "select kode_supplier, nama from Suppliers "
                Using Dr = OpenTrans(SQL)
                    Cmb_Filter_Value.Items.Add("--Semua--") : arrSupplier.Add("semua")
                    Do While Dr.Read
                        Cmb_Filter_Value.Items.Add(Dr("nama")) : arrSupplier.Add(Dr("kode_supplier"))
                    Loop
                End Using
            End If

            Cmb_Filter_Value.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_Kendaraan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Kendaraan.DoubleClick
        If Lv_Kendaraan.Items.Count = 0 Then Exit Sub

        Get_Data_ListView(Lv_Kendaraan.FocusedItem.Index)

        Tb_NoSJ.Text = lv_NoSJ.ToString
        Tb_NoPlat.Text = lv_NoPlat.ToString
        Tb_Driver.Text = lv_Driver.ToString
        Txt_Lokasi.Text = lv_Lokasi.ToString
        Txt_Supplier.Text = lv_Supplier.ToString

        selected_NoFaktur = lv_NoFaktur.ToString
        selected_NoSJ = lv_NoSJ.ToString
        selected_PlatNomor = lv_NoPlat.ToString
        selected_Driver = lv_Driver.ToString
        selected_KdSupplier = lv_KdSupplier
        selected_Lokasi = lv_Lokasi.ToString

        Tb_NoSJ.Enabled = True
        Tb_NoPlat.Enabled = True
        Tb_Driver.Enabled = True


        'REQUEST GAMBAR DARI API
        Try
            OpenConn()

            FlowLayoutPanel1.Controls.Clear()

            SQL = "Select Path_Dokumen From EMI_Pembelian_Loading_Foto_Temp "
            SQL = SQL & " Where Kode_Perusahaan ='" & KodePerusahaan & "' and No_Faktur='" & lv_NoFaktur & "' "
            Using Dr = OpenTrans(SQL)

                If Dr.HasRows Then
                    FlowLayoutPanel1.BackColor = Color.White
                Else
                    FlowLayoutPanel1.BackColor = Color.LightGray
                End If

                Do While Dr.Read
                    Dim currentImage As String = "https://emap.basecloud.app/" & Dr("Path_Dokumen")
                    Dim pictureBox As New PictureBox()
                    pictureBox.Size = New Size(550, 315)
                    pictureBox.BorderStyle = BorderStyle.FixedSingle
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage

                    Try
                        Dim request As WebRequest = WebRequest.Create(currentImage)
                        Using response As WebResponse = request.GetResponse()
                            Using stream As IO.Stream = response.GetResponseStream()
                                pictureBox.Image = Image.FromStream(stream)
                            End Using
                        End Using
                    Catch ex As Exception

                        Dim whiteBitmap As New Bitmap(550, 269)
                        Using g As Graphics = Graphics.FromImage(whiteBitmap)
                            g.Clear(Color.White)
                        End Using

                        pictureBox.Image = whiteBitmap
                    End Try

                    'HANDLER CLICK
                    AddHandler pictureBox.Click, Sub(pbSender, pbEventArgs) ShowFullImage(currentImage)
                    AddHandler pictureBox.MouseEnter, Sub(pbSender, pbEventArgs) pictureBox.Cursor = Cursors.Hand
                    AddHandler pictureBox.MouseLeave, Sub(pbSender, pbEventArgs) pictureBox.Cursor = Cursors.Default

                    FlowLayoutPanel1.Controls.Add(pictureBox)

                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub ShowFullImage(ByVal path As String)

        Dim pictureBoxFull As New PictureBox()
        pictureBoxFull.Dock = DockStyle.Fill
        pictureBoxFull.SizeMode = PictureBoxSizeMode.Zoom

        Try
            Dim request As WebRequest = WebRequest.Create(path)
            Using response As WebResponse = request.GetResponse()
                Using stream As IO.Stream = response.GetResponseStream()
                    pictureBoxFull.Image = Image.FromStream(stream)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading full image: " & ex.Message)
        End Try

        FullImageForm.Controls.Add(pictureBoxFull)
        FullImageForm.StartPosition = FormStartPosition.CenterScreen
        FullImageForm.Show()
        FullImageForm.Focus()
    End Sub

    Private Sub Btn_Update_Click(sender As Object, e As EventArgs) Handles Btn_Update.Click

        If selected_NoFaktur = "" Or selected_NoSJ = "" Or selected_PlatNomor = "" Or selected_Driver = "" Then Exit Sub

        If Txt_Supplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Tidak Ada Data yang Diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Tb_NoSJ.Text.Trim.Length = 0 Then
        MessageBox.Show("NO Sj Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Tb_NoPlat.Text.Trim.Length = 0 Then
            MessageBox.Show("Plat Kendaraan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Tb_Driver.Text.Trim.Length = 0 Then
            MessageBox.Show("Driver Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub

        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim tes As String = tgl_skg

            SQL = "update emi_pembelian_loading set No_SJ='" & Tb_NoSJ.Text & "', No_Plat='" & Tb_NoPlat.Text & "', Driver='" & Tb_Driver.Text & "', Flag_Dkmn_Tdk_Sesuai = null "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and No_Faktur='" & selected_NoFaktur & "' and No_SJ='" & selected_NoSJ & "' and No_Plat='" & selected_PlatNomor & "' and Driver='" & selected_Driver & "' "
            ExecuteTrans(SQL)

            SQL = "insert into log_kendaraan_tidak_sesuai (Kode_Perusahaan, No_Faktur, Kode_Supplier, Lokasi, No_SJ_Asal, "
            SQL = SQL & "No_Plat_Asal, Driver_Asal, No_SJ_Update, No_Plat_Update, Driver_Update, Tanggal, Jam, UserID) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & selected_NoFaktur & "', '" & selected_KdSupplier & "', '" & selected_Lokasi & "', "
            SQL = SQL & "'" & selected_NoSJ & "', '" & selected_PlatNomor & "', '" & selected_Driver & "', '" & Tb_NoSJ.Text & "', "
            SQL = SQL & "'" & Tb_NoPlat.Text & "', '" & Tb_Driver.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
            SQL = SQL & "'" & UserID & "')"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Kosong()
            Load_Kendaraan()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Tb_NoSJ_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_NoSJ.KeyPress, Txt_Supplier.KeyPress, Txt_Lokasi.KeyPress
        If e.KeyChar = Chr(13) Then Tb_NoPlat.Focus()
    End Sub

    Private Sub Tb_NoPlat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_NoPlat.KeyPress
        If e.KeyChar = Chr(13) Then Tb_Driver.Focus()
    End Sub
    Private Sub Tb_Driver_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_Driver.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Update.Focus()
    End Sub
    Private Sub Cmb_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Filter_Value.Focus()
    End Sub
    Private Sub Cmb_Filter_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub






End Class
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Drawing.Printing
Imports System.IO
Imports System

Public Class Master_Gudang
    Dim arrcari, arrKategoriGudang As New ArrayList
    Dim Jenis = "Master_Jenis_Hewan"

    Dim arrArea_Jenis As New ArrayList
    Dim arrKolom_Jenis As New ArrayList
    Dim arrBaris_Jenis As New ArrayList
    Dim arrLevel_Jenis As New ArrayList
    Dim arrLvlSize_Jenis As New ArrayList
    Dim arrPosisi_Jenis As New ArrayList
    Dim arrPalet_Jenis As New ArrayList
    Dim arrSetGudang_Jenis As New ArrayList

    Dim arrPalletId As New ArrayList
    Dim arrSusunan_Jenis As New ArrayList

    Dim arrDataAreas As New ArrayList
    Dim arrDataBay As New ArrayList
    Dim arrDataRow As New ArrayList
    Dim arrDataLevel As New ArrayList
    Dim arrDataLevelSize As New ArrayList
    Dim arrDataPosition As New ArrayList

    Dim arrSetGudang As New ArrayList

    Private imageBytes As Byte = Nothing
    Private FileSize As UInt32
    Private rawData() As Byte
    Private FS As FileStream


    Private Sub btnSimpanKolom_Click(sender As Object, e As EventArgs) Handles btnSimpanKolom.Click

        If txtKolom_KodeKolom.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_KodeKolom & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtKolom_KodeKolom.Focus()
            Exit Sub
        ElseIf txtKolom_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtKolom_Ket.Focus()
            Exit Sub
        End If

        Try
            OpenConn()


            If btnSimpanKolom.Tag = "&Simpan" Then

                SQL = "select kode_wms_bay  from EMI_WMS_BAY where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_wms_bay = '" & txtKolom_KodeKolom.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Error_Pernah_Simpan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using

                SQL = "insert into EMI_WMS_BAY(kode_perusahaan,kode_wms_bay,keterangan) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & txtKolom_KodeKolom.Text & "', '" & txtKolom_Ket.Text & "' )"
                ExecuteTrans(SQL)
            ElseIf btnSimpanKolom.Tag = "&Update" Then
                SQL = "update EMI_WMS_BAY set keterangan = '" & txtKolom_Ket.Text & "' where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_wms_bay = '" & txtKolom_KodeKolom.Text & "'"
                ExecuteTrans(SQL)

                btnSimpanKolom.Tag = "&Simpan"
                btnSimpanKolom.Text = "Simpan"

            End If

            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            kosong_kolom()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan_Area.Click
        If cmbArea_JenisGudang.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbArea_Jenis.Focus()
            Exit Sub
        ElseIf TxtArea_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            TxtArea_Ket.Focus()
            Exit Sub
        ElseIf txtArea_KodeArea.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_KodeArea & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtArea_KodeArea.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If Btn_Simpan_Area.Tag = "&Simpan" Then
                SQL = "select kode_wms_area  from EMI_WMS_Areas where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_wms_area = '" & txtArea_KodeArea.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Error_Pernah_Simpan, Judul, MessageBoxButtons.OK)
                        Exit Sub

                    End If
                End Using


                SQL = "insert into EMI_WMS_Areas(kode_perusahaan,kode_wms_area,jenis_gudang,keterangan) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & txtArea_KodeArea.Text & "', '" & cmbArea_JenisGudang.Text & "', '" & TxtArea_Ket.Text & "' )"
                ExecuteTrans(SQL)
            ElseIf Btn_Simpan_Area.Tag = "&Update" Then
                SQL = "update EMI_WMS_AREAS set kode_wms_area = '" & txtArea_KodeArea.Text & "' ,keterangan = '" & TxtArea_Ket.Text & "', jenis_gudang = '" & cmbArea_JenisGudang.Text & "' "
                SQL = SQL & "where kode_wms_area = '" & txtArea_KodeArea.Text & "'"
                ExecuteTrans(SQL)

                Btn_Simpan_Area.Tag = "&Simpan"
                Btn_Simpan_Area.Text = Base_Language.Lang_Global_Simpan
            End If


            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            TabControl1_Click(Me, e)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Master_Gudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub
    Private Sub kosong()
        get_jam()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Set_Gudang")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Label1.Text = Base_Language.Lang_SetGudang_Judul

        TabPage1.Text = Base_Language.Lang_SetGudang_Area
        TabPage8.Text = Base_Language.Lang_SetGudang_Kolom
        TabPage2.Text = Base_Language.Lang_SetGudang_KodeBaris
        TabPage6.Text = Base_Language.Lang_SetGudang_Level
        TabPage7.Text = Base_Language.Lang_SetGudang_LevelSize
        TabPage9.Text = Base_Language.Lang_SetGudang_Position
        TabPage5.Text = Base_Language.Lang_SetGudang_SetGudang
        TabPage3.Text = Base_Language.Lang_SetGudang_Palet
        TabPage4.Text = Base_Language.Lang_SetGudang_Susunan

        lblArea_kode.Text = Base_Language.Lang_SetGudang_KodeArea
        lblArea_Ket.Text = Base_Language.lang_global_keterangan
        lblArea_JG.Text = Base_Language.Lang_SetGudang_JenisGudang
        Btn_Simpan_Area.Text = Base_Language.Lang_Global_Simpan
        btnArea_Hapus.Text = Base_Language.Lang_Global_Hapus
        Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
        lblArea_Kolom.Text = Base_Language.Lang_Global_Kolom
        Btn_Cari.Text = Base_Language.Lang_Global_Cari
        LvwArea.Columns.Add(Base_Language.Lang_SetGudang_KodeArea, 200, HorizontalAlignment.Center)
        LvwArea.Columns.Add(Base_Language.lang_global_keterangan, 200, HorizontalAlignment.Left)
        LvwArea.Columns.Add(Base_Language.Lang_SetGudang_JenisGudang, 200, HorizontalAlignment.Center)

        lbKolom_Kode.Text = Base_Language.Lang_SetGudang_KodeKolom
        lbKolom_Ket.Text = Base_Language.lang_global_keterangan
        lbKolom_Kolom.Text = Base_Language.Lang_Global_Kolom
        btnSimpanKolom.Text = Base_Language.Lang_Global_Simpan
        Button26.Text = Base_Language.Lang_Global_Cari
        Button27.Text = Base_Language.Lang_Global_Hapus
        Button28.Text = Base_Language.Lang_Global_Refresh
        LvwKolom.Columns.Add(Base_Language.Lang_SetGudang_KodeKolom, 200, HorizontalAlignment.Center)
        LvwKolom.Columns.Add(Base_Language.lang_global_keterangan, 600, HorizontalAlignment.Left)

        lblBaris_Kode.Text = Base_Language.Lang_SetGudang_KodeBaris
        lblBaris_Keterangan.Text = Base_Language.lang_global_keterangan
        lblBaris_Kolom.Text = Base_Language.Lang_Global_Kolom
        btnBaris_Cari.Text = Base_Language.Lang_Global_Cari
        btnBaris_Hapus.Text = Base_Language.Lang_Global_Hapus
        btnBaris_Refresh.Text = Base_Language.Lang_Global_Refresh
        btnBaris_Simpan.Text = Base_Language.Lang_Global_Simpan
        LvwBaris.Columns.Add(Base_Language.Lang_SetGudang_KodeBaris, 200, HorizontalAlignment.Center)
        LvwBaris.Columns.Add(Base_Language.lang_global_keterangan, 600, HorizontalAlignment.Left)

        lblLevel_Kode.Text = Base_Language.Lang_SetGudang_KodeLevel
        lblLevel_Ket.Text = Base_Language.lang_global_keterangan
        lblLevel_Kolom.Text = Base_Language.Lang_Global_Kolom
        btnLevel_Cari.Text = Base_Language.Lang_Global_Cari
        btnLevel_Hapus.Text = Base_Language.Lang_Global_Hapus
        btnLevel_Refresh.Text = Base_Language.Lang_Global_Refresh
        btnLevel_Simpan.Text = Base_Language.Lang_Global_Simpan
        LvwLevel.Columns.Add(Base_Language.Lang_SetGudang_KodeBaris, 200, HorizontalAlignment.Center)
        LvwLevel.Columns.Add(Base_Language.lang_global_keterangan, 600, HorizontalAlignment.Left)

        lblUL_Kode.Text = Base_Language.Lang_SetGudang_KodeUkuran
        lblUL_Ket.Text = Base_Language.lang_global_keterangan
        lblUL_Kolom.Text = Base_Language.Lang_Global_Kolom
        lblUL_UL.Text = Base_Language.Lang_SetGudang_LevelUkuran
        btnLvlSize_Cari.Text = Base_Language.Lang_Global_Cari
        btnLvlSize_Hapus.Text = Base_Language.Lang_Global_Hapus
        btnLvlSize_Refresh.Text = Base_Language.Lang_Global_Refresh
        btnLvlSize_Simpan.Text = Base_Language.Lang_Global_Simpan
        LvwLvlSize.Columns.Add(Base_Language.Lang_SetGudang_KodeUkuran, 200, HorizontalAlignment.Center)
        LvwLvlSize.Columns.Add(Base_Language.lang_global_keterangan, 200, HorizontalAlignment.Left)
        LvwLvlSize.Columns.Add(Base_Language.Lang_Global_Panjang, 100, HorizontalAlignment.Center)
        LvwLvlSize.Columns.Add(Base_Language.Lang_Global_Lebar, 100, HorizontalAlignment.Center)
        LvwLvlSize.Columns.Add(Base_Language.Lang_Global_Tinggi, 100, HorizontalAlignment.Center)

        lblPosisi_Kode.Text = Base_Language.Lang_SetGudang_KodePosisi
        lblPosisi_Ket.Text = Base_Language.lang_global_keterangan
        lblPosisi_Kolom.Text = Base_Language.Lang_Global_Kolom
        Button30.Text = Base_Language.Lang_Global_Cari
        btnPosisi_Hapus.Text = Base_Language.Lang_Global_Hapus
        btnPosisi_Refresh.Text = Base_Language.Lang_Global_Refresh
        btnPosisi_Simpan.Text = Base_Language.Lang_Global_Simpan
        LvwPosisi.Columns.Add(Base_Language.Lang_SetGudang_KodePosisi, 200, HorizontalAlignment.Center)
        LvwPosisi.Columns.Add(Base_Language.lang_global_keterangan, 600, HorizontalAlignment.Left)

        lblPalet_Kode.Text = Base_Language.Lang_SetGudang_KodePalet
        lblPalet_Ket.Text = Base_Language.lang_global_keterangan
        lblPalet_UP.Text = Base_Language.Lang_SetGudang_UkuranPalet
        lblPalet_Kolom.Text = Base_Language.Lang_Global_Kolom
        btnPalet_Cari.Text = Base_Language.Lang_Global_Cari
        btnPalet_Hapus.Text = Base_Language.Lang_Global_Hapus
        btnPalet_Refresh.Text = Base_Language.Lang_Global_Refresh
        btnPalet_Simpan.Text = Base_Language.Lang_Global_Simpan
        LvwPallet.Columns.Add(Base_Language.Lang_SetGudang_KodePalet, 200, HorizontalAlignment.Center)
        LvwPallet.Columns.Add(Base_Language.lang_global_keterangan, 250, HorizontalAlignment.Left)
        LvwPallet.Columns.Add(Base_Language.Lang_Global_Panjang, 100, HorizontalAlignment.Center)
        LvwPallet.Columns.Add(Base_Language.Lang_Global_Lebar, 100, HorizontalAlignment.Left)
        LvwPallet.Columns.Add(Base_Language.Lang_Global_Tinggi, 100, HorizontalAlignment.Left)

        lblSetGudang_Lok.Text = Base_Language.Lang_Global_LokasiGudang
        lblSetGudang_Area.Text = Base_Language.Lang_SetGudang_Area
        lblSetGudang_Kolom.Text = Base_Language.Lang_SetGudang_Kolom
        lblSetGudang_Baris.Text = Base_Language.Lang_SetGudang_Baris
        lblSetGudang_Level.Text = Base_Language.Lang_SetGudang_Level
        lblSetGudang_UkL.Text = Base_Language.Lang_SetGudang_LevelSize
        lblSetGudang_Posisi.Text = Base_Language.Lang_SetGudang_Position
        Lbl_KategoriGudang.Text = Base_Language.Lang_Global_KategoriGudang
        lblSetGudang_KolomJenis.Text = Base_Language.Lang_Global_Kolom
        btnSetGudang_Cari.Text = Base_Language.Lang_Global_Cari
        btnSetGudang_Hapus.Text = Base_Language.Lang_Global_Hapus
        btnSetGudang_Refresh.Text = Base_Language.Lang_Global_Refresh
        btnSetGudang_Simpan.Text = Base_Language.Lang_Global_Simpan

        lblSusunan_Kode.Text = Base_Language.Lang_SetGudang_KodeSusunan
        lblSusunan_Ket.Text = Base_Language.lang_global_keterangan
        lblSusunan_Palet.Text = Base_Language.Lang_SetGudang_Palet
        lblSusunan_UkuranSusunan.Text = Base_Language.Lang_SetGudang_UkSusunan
        lblSusunan_Susunan.Text = Base_Language.Lang_SetGudang_Susunan
        lblSusunan_Kolom.Text = Base_Language.Lang_Global_Kolom
        btnSusunan_Cari.Text = Base_Language.Lang_Global_Cari
        btnSusunan_Simpan.Text = Base_Language.Lang_Global_Simpan
        btnSusunan_Refresh.Text = Base_Language.Lang_Global_Refresh
        btnSusunan_Hapus.Text = Base_Language.Lang_Global_Hapus

        LvwSetGudang.Columns.Add(Base_Language.Lang_Global_LokasiGudang, 200, HorizontalAlignment.Left) '0
        LvwSetGudang.Columns.Add("Area", 120, HorizontalAlignment.Left) '1
        LvwSetGudang.Columns.Add("Bay", 120, HorizontalAlignment.Left) '2
        LvwSetGudang.Columns.Add("Row", 120, HorizontalAlignment.Left) '3
        LvwSetGudang.Columns.Add("Level", 120, HorizontalAlignment.Left)
        LvwSetGudang.Columns.Add("Level Size", 175, HorizontalAlignment.Left)
        LvwSetGudang.Columns.Add("Panjang", 100, HorizontalAlignment.Left)
        LvwSetGudang.Columns.Add("Lebar", 100, HorizontalAlignment.Left)
        LvwSetGudang.Columns.Add("Tinggi", 100, HorizontalAlignment.Left)
        LvwSetGudang.Columns.Add("Posisi", 120, HorizontalAlignment.Left)
        LvwSetGudang.Columns.Add("Kategori Gudang", 120, HorizontalAlignment.Left)
        LvwSetGudang.Columns.Add("Jumlah Palet", 100, HorizontalAlignment.Right)

        kosong_area()
        get_area()

    End Sub

    Private Sub kosong_area()
        cmbArea_JenisGudang.SelectedIndex = -1
        cmbArea_Jenis.SelectedIndex = -1
        txtArea_KodeArea.Clear()
        TxtArea_Ket.Clear()
        txtArea_Value.Clear()

        cmbArea_Jenis.Items.Clear() : arrArea_Jenis.Clear()
        cmbArea_Jenis.Items.Add("Jenis Gudang") : arrArea_Jenis.Add("jenis_gudang")
        cmbArea_Jenis.Items.Add("Kode Area") : arrArea_Jenis.Add("kode_wms_area")
        cmbArea_Jenis.Items.Add("Keterangan") : arrArea_Jenis.Add("keterangan")



    End Sub

    Private Sub kosong_kolom()
        txtKolom_KodeKolom.Clear()
        txtKolom_Ket.Clear()
        txtKolom_Value.Clear()
        cmbKolom_Jenis.SelectedIndex = -1

        cmbKolom_Jenis.Items.Clear() : arrKolom_Jenis.Clear()
        cmbKolom_Jenis.Items.Add("Kode Kolom") : arrKolom_Jenis.Add("kode_wms_bay")
        cmbKolom_Jenis.Items.Add("Keterangan") : arrKolom_Jenis.Add("keterangan")


    End Sub

    Private Sub kosong_baris()
        txtBaris_KodeBaris.Clear()
        txtBaris_Ket.Clear()
        txtBaris_Value.Clear()
        cmbBaris_Jenis.SelectedIndex = -1

        cmbBaris_Jenis.Items.Clear() : arrBaris_Jenis.Clear()
        cmbBaris_Jenis.Items.Add("Kode Baris") : arrBaris_Jenis.Add("kode_wms_row")
        cmbBaris_Jenis.Items.Add("Keterangan") : arrBaris_Jenis.Add("keterangan")

    End Sub
    Private Sub kosong_level()
        txtLevel_KodeLevel.Clear()
        txtLevel_Ket.Clear()
        txtLevel_Value.Clear()

        cmbLevel_Jenis.SelectedIndex = -1

        cmbLevel_Jenis.Items.Clear() : arrLevel_Jenis.Clear()
        cmbLevel_Jenis.Items.Add("Kode Level") : arrLevel_Jenis.Add("kode_wms_level")
        cmbLevel_Jenis.Items.Add("Keterangan") : arrLevel_Jenis.Add("keterangan")

    End Sub

    Private Sub kosong_level_size()
        txtLvlSize_KodeUkuran.Clear()
        txtLvlSize_Ket.Clear()
        txtLvlSize_Panjang.Clear()
        txtLvlSize_Lebar.Clear()
        txtLvlSize_Tinggi.Clear()
        txtLvlSize_Value.Clear()

        cmbLvlSize_Jenis.SelectedIndex = -1

        cmbLvlSize_Jenis.Items.Clear() : arrLvlSize_Jenis.Clear()
        cmbLvlSize_Jenis.Items.Add("Kode Level Size") : arrLvlSize_Jenis.Add("kode_wms_level_size")
        cmbLvlSize_Jenis.Items.Add("Keterangan") : arrLvlSize_Jenis.Add("keterangan")

    End Sub

    Private Sub kosong_posisi()
        txtPosisi_KodePosisi.Clear()
        txtPosisi_Ket.Clear()
        txtPosisi_Value.Clear()

        cmbPosisi_Jenis.SelectedIndex = -1

        cmbPosisi_Jenis.Items.Clear() : arrPosisi_Jenis.Clear()
        cmbPosisi_Jenis.Items.Add("Kode Posisi") : arrPosisi_Jenis.Add("kode_wms_position")
        cmbPosisi_Jenis.Items.Add("Keterangan") : arrPosisi_Jenis.Add("keterangan")

    End Sub

    Private Sub kosong_set_gudang()
        cmbSetGudang_Area.SelectedIndex = -1
        cmbSetGudangKolom.SelectedIndex = -1
        cmbSetGudang_Baris.SelectedIndex = -1
        cmbSetGudang_Level.SelectedIndex = -1
        cmbSetGudang_UkLvl.SelectedIndex = -1
        cmbSetGudang_Posisi.SelectedIndex = -1
        cmbSetGudang_Jenis.SelectedIndex = -1
        Cmb_KategoriGudang.SelectedIndex = -1
        TextBox1.Text = ""

        txtSetGudang_Value.Clear()

        cmbSetGudang_Jenis.SelectedIndex = -1

        cmbSetGudang_Jenis.Items.Clear() : arrSetGudang_Jenis.Clear()
        cmbSetGudang_Jenis.Items.Add("Lokasi") : arrSetGudang_Jenis.Add("kode_stock_owner")
        cmbSetGudang_Jenis.Items.Add("Area") : arrSetGudang_Jenis.Add("kode_wms_area")
        cmbSetGudang_Jenis.Items.Add("Bay") : arrSetGudang_Jenis.Add("Kode_WMS_BAY")
        cmbSetGudang_Jenis.Items.Add("Row") : arrSetGudang_Jenis.Add("Kode_WMS_ROW")
        cmbSetGudang_Jenis.Items.Add("Level") : arrSetGudang_Jenis.Add("kode_wms_level")
        cmbSetGudang_Jenis.Items.Add("Ukuran Level") : arrSetGudang_Jenis.Add("kode_wms_level_size")
        cmbSetGudang_Jenis.Items.Add("Posisi") : arrSetGudang_Jenis.Add("kode_wms_position")
    End Sub

    Private Sub kosong_palet()
        txtPalet_KodeUkuran.Clear()
        txtPalet_Ket.Clear()
        txtPalet_Value.Clear()
        txtPalet_Panjang.Clear()
        txtPalet_Lebar.Clear()
        txtPalet_Tinggi.Clear()

        cmbPalet_Jenis.SelectedIndex = -1

        cmbPalet_Jenis.Items.Clear() : arrPalet_Jenis.Clear()
        cmbPalet_Jenis.Items.Add("Kode Ukuran") : arrPalet_Jenis.Add("kode_wms_pallet")
        cmbPalet_Jenis.Items.Add("Keterangan") : arrPalet_Jenis.Add("keterangan")

    End Sub



    Private Sub kosong_susunan()

        cmbSusunan_Susunan.SelectedIndex = -1
        txtSusunan_Kode.Clear()
        txtSusunan_Ket.Clear()
        txtSusunan_Panjang.Clear()
        txtSusunan_Lebar.Clear()
        txtSusunan_Panjang.Clear()
        txtSusunan_Tinggi.Clear()
        txtSusunan_Value.Clear()
        cmbSusunan_Pallet.SelectedIndex = -1


        cmbSusunan_Jenis.SelectedIndex = -1
        cmbSusunan_Jenis.Items.Clear() : arrSusunan_Jenis.Clear()
        cmbSusunan_Jenis.Items.Add("Jenis Susunan") : arrSusunan_Jenis.Add("jenis_susunan")
        cmbSusunan_Jenis.Items.Add("Kode Susunan") : arrSusunan_Jenis.Add("kode_wms_susunan")
        cmbSusunan_Jenis.Items.Add("Keterangan") : arrSusunan_Jenis.Add("keterangan")


        Try
            OpenConn()

            cmbSusunan_Pallet.Items.Clear()
            SQL = "select id_wms_pallet,kode_wms_pallet from EMI_WMS_PALLET where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_wms_pallet "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cmbSusunan_Pallet.Items.Add(Dr("kode_wms_pallet")) : arrPalletId.Add(Dr("id_wms_pallet"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Private Sub get_area()
        Try
            OpenConn()

            LvwArea.Items.Clear()
            SQL = "select id_wms_area,jenis_gudang,keterangan,kode_wms_area from EMI_WMS_AREAS where kode_perusahaan = '" & KodePerusahaan & "' "
            If txtArea_Value.Text.Trim.Length <> 0 Then

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrArea_Jenis.Item(cmbArea_Jenis.SelectedIndex) & " like '%" & Trim(txtArea_Value.Text) & "%' "


            End If
            SQL = SQL & "order by kode_wms_area asc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = LvwArea.Items.Add(Dr("kode_wms_area"))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("keterangan")))
                    lvw.SubItems.Add(Dr("jenis_gudang"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_kolom()
        Try
            OpenConn()

            LvwKolom.Items.Clear()
            SQL = "select kode_wms_bay,keterangan from EMI_WMS_BAY where kode_perusahaan = '" & KodePerusahaan & "' "
            If txtKolom_Value.Text.Trim.Length <> 0 And cmbKolom_Jenis.SelectedIndex <> -1 Then

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrKolom_Jenis.Item(cmbKolom_Jenis.SelectedIndex) & " like '%" & Trim(txtKolom_Value.Text) & "%' "

            End If
            SQL = SQL & "order by kode_wms_Bay asc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = LvwKolom.Items.Add(Dr("kode_wms_bay"))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("keterangan")))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub get_baris()
        Try
            OpenConn()

            LvwBaris.Items.Clear()
            SQL = "select kode_wms_row,keterangan from EMI_WMS_ROW where kode_perusahaan = '" & KodePerusahaan & "' "
            If txtBaris_Value.Text.Trim.Length <> 0 And cmbBaris_Jenis.SelectedIndex <> -1 Then

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrBaris_Jenis.Item(cmbBaris_Jenis.SelectedIndex) & " like '%" & Trim(txtBaris_Value.Text) & "%' "

            End If
            SQL = SQL & "order by kode_wms_row asc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = LvwBaris.Items.Add(Dr("kode_wms_row"))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("keterangan")))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub get_level()
        Try
            OpenConn()

            LvwLevel.Items.Clear()
            SQL = "select kode_wms_level,keterangan from EMI_WMS_LEVEL where kode_perusahaan = '" & KodePerusahaan & "' "
            If txtLevel_Value.Text.Trim.Length <> 0 And cmbLevel_Jenis.SelectedIndex <> -1 Then

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrLevel_Jenis.Item(cmbLevel_Jenis.SelectedIndex) & " like '%" & Trim(txtLevel_Value.Text) & "%' "

            End If
            SQL = SQL & "order by kode_wms_level asc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = LvwLevel.Items.Add(Dr("kode_wms_level"))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("keterangan")))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_level_size()
        Try
            OpenConn()

            LvwLvlSize.Items.Clear()
            SQL = "select kode_wms_level_size,keterangan,p,l,t from EMI_WMS_LEVEL_SIZE where kode_perusahaan = '" & KodePerusahaan & "' "
            If txtLvlSize_Value.Text.Trim.Length <> 0 And cmbLvlSize_Jenis.SelectedIndex <> -1 Then

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrLvlSize_Jenis.Item(cmbLvlSize_Jenis.SelectedIndex) & " like '%" & Trim(txtLvlSize_Value.Text) & "%' "

            End If
            SQL = SQL & "order by kode_wms_level_size asc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = LvwLvlSize.Items.Add(Dr("kode_wms_level_size"))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("keterangan")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("p")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("l")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("t")))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_posisi()
        Try
            OpenConn()

            LvwPosisi.Items.Clear()
            SQL = "select kode_wms_position,keterangan from EMI_WMS_Position where kode_perusahaan = '" & KodePerusahaan & "' "
            If txtPosisi_Value.Text.Trim.Length <> 0 And cmbPosisi_Jenis.SelectedIndex <> -1 Then

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrPosisi_Jenis.Item(cmbPosisi_Jenis.SelectedIndex) & " like '%" & Trim(txtPosisi_Value.Text) & "%' "

            End If
            SQL = SQL & "order by kode_wms_position asc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = LvwPosisi.Items.Add(Dr("kode_wms_position"))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("keterangan")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_palet()
        Try
            OpenConn()

            LvwPallet.Items.Clear()
            SQL = "select kode_wms_pallet,keterangan,p,l,t from EMI_WMS_Pallet where kode_perusahaan = '" & KodePerusahaan & "' "
            If txtPalet_Value.Text.Trim.Length <> 0 And cmbPalet_Jenis.SelectedIndex <> -1 Then

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrPalet_Jenis.Item(cmbPalet_Jenis.SelectedIndex) & " like '%" & Trim(txtPalet_Value.Text) & "%' "

            End If
            SQL = SQL & "order by kode_wms_pallet asc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = LvwPallet.Items.Add(Dr("kode_wms_pallet"))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("keterangan")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("p")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("l")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("t")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_susunan()
        Try
            OpenConn()

            LvwSusunan.Items.Clear()
            SQL = "select a.Kode_WMS_Susunan,a.Keterangan,b.Keterangan as pallet,a.Jenis_Susunan,a.p,a.l,a.t "
            SQL = SQL & "from EMI_WMS_Susunan a, EMI_WMS_Pallet b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_WMS_Pallet = b.Id_WMS_Pallet "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'"
            If txtSusunan_Value.Text.Trim.Length <> 0 And cmbSusunan_Jenis.SelectedIndex <> -1 Then

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrSusunan_Jenis.Item(cmbSusunan_Jenis.SelectedIndex) & " like '%" & Trim(txtSusunan_Value.Text) & "%' "

            End If
            SQL = SQL & "order by kode_wms_susunan asc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = LvwSusunan.Items.Add(Dr("kode_wms_susunan"))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("keterangan")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("pallet")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("jenis_susunan")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("p")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("l")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("t")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_set_gudang()
        Try
            OpenConn()

            LvwSetGudang.Items.Clear() : arrSetGudang.Clear()

            SQL = "select Id_WMS_Warehouse_Position,Kode_Perusahaan,Kode_Stock_Owner,kode_wms_area,Kode_WMS_Bay,Kode_WMS_Row,"
            SQL = SQL & "Kode_WMS_Level,Kode_WMS_Level_Size,p,l,t,Kode_WMS_Position,kategori_gudang,Jumlah_Pallet from View_Warehouse_Position "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            If txtSetGudang_Value.Text.Trim.Length <> 0 And cmbSetGudang_Jenis.SelectedIndex <> -1 Then

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrSetGudang_Jenis.Item(cmbSetGudang_Jenis.SelectedIndex) & " like '%" & Trim(txtSetGudang_Value.Text) & "%' "

            End If
            SQL = SQL & "order by Kode_Stock_Owner asc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    arrSetGudang.Add(Dr("Id_WMS_Warehouse_Position"))

                    lvw = LvwSetGudang.Items.Add(Dr("Kode_Stock_Owner"))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("kode_wms_area")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("Kode_WMS_Bay")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("Kode_WMS_Row")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("Kode_WMS_Level")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("Kode_WMS_Level_Size")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("p")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("l")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("t")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("Kode_WMS_Position")))
                    lvw.SubItems.Add(General_Class.CekNULL(Dr("kategori_gudang")))
                    lvw.SubItems.Add(General_Class.CekNULL(String.Format("{0:N0}", Dr("Jumlah_Pallet"))))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub load_tab_gudang()

        ' get lokasi gudang 

        Try
            OpenConn()
            cmbSetGudang_Lokasi.Items.Clear()
            SQL = "select kode_stock_owner from Stock_Owner_Gudang where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbSetGudang_Lokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'get area
        Try
            OpenConn()
            cmbSetGudang_Area.Items.Clear() : arrDataAreas.Clear()
            SQL = "select Id_WMS_Area,Kode_WMS_Area  from emi_wms_areas  where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Area"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbSetGudang_Area.Items.Add(dr("Kode_WMS_Area")) : arrDataAreas.Add(dr("id_wms_area"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'get baris
        Try
            OpenConn()
            cmbSetGudang_Baris.Items.Clear() : arrDataRow.Clear()
            SQL = "select id_wms_row,Kode_WMS_Row  from EMI_WMS_Row   where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Row"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbSetGudang_Baris.Items.Add(dr("Kode_WMS_Row")) : arrDataRow.Add(dr("id_wms_row"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'get kolom
        Try
            OpenConn()
            cmbSetGudangKolom.Items.Clear() : arrDataBay.Clear()
            SQL = "select Id_WMS_Bay,Kode_WMS_Bay  from EMI_WMS_BAY where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Bay"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbSetGudangKolom.Items.Add(dr("Kode_WMS_Bay")) : arrDataBay.Add(dr("Id_WMS_Bay"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'get level
        Try
            OpenConn()
            cmbSetGudang_Level.Items.Clear() : arrDataLevel.Clear()
            SQL = "select Id_WMS_Level,Kode_WMS_Level from EMI_WMS_Level where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Level"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbSetGudang_Level.Items.Add(dr("Kode_WMS_Level")) : arrDataLevel.Add(dr("Id_WMS_Level"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'get level size
        Try
            OpenConn()
            cmbSetGudang_UkLvl.Items.Clear() : arrDataLevelSize.Clear()
            SQL = "select Id_WMS_Level_Size,Kode_WMS_Level_Size  from EMI_WMS_Level_Size where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Level_Size"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbSetGudang_UkLvl.Items.Add(dr("Kode_WMS_Level_Size")) : arrDataLevelSize.Add(dr("Id_WMS_Level_Size"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'get posisi
        Try
            OpenConn()
            cmbSetGudang_Posisi.Items.Clear() : arrDataPosition.Clear()
            SQL = "select Id_WMS_Position,Kode_WMS_Position from EMI_WMS_Position where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Position"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbSetGudang_Posisi.Items.Add(dr("Kode_WMS_Position")) : arrDataPosition.Add(dr("Id_WMS_Position"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'get kategori gudang
        Try
            OpenConn()
            Cmb_KategoriGudang.Items.Clear() : arrKategoriGudang.Clear()
            SQL = "Select * From "
            SQL = SQL & "emi_master_kategori_gudang "
            SQL = SQL & "order by id_master_kategori_gudang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_KategoriGudang.Items.Add(dr("keterangan")) : arrKategoriGudang.Add(dr("id_master_kategori_gudang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub TabControl1_Click(sender As Object, e As EventArgs) Handles TabControl1.Click
        kosong_area()
        kosong_kolom()
        kosong_baris()
        kosong_level()
        kosong_level_size()
        kosong_posisi()
        kosong_set_gudang()
        kosong_palet()
        kosong_susunan()

        If TabControl1.SelectedIndex = 0 Then
            get_area()
            Btn_Simpan_Area.Tag = "&Simpan"
            Btn_Simpan_Area.Text = Base_Language.Lang_Global_Simpan
        ElseIf TabControl1.SelectedIndex = 1 Then
            get_kolom()
            btnSimpanKolom.Tag = "&Simpan"
            btnSimpanKolom.Text = Base_Language.Lang_Global_Simpan
        ElseIf TabControl1.SelectedIndex = 2 Then
            get_baris()
            btnBaris_Simpan.Tag = "&Simpan"
            btnBaris_Simpan.Text = "&Simpan"
        ElseIf TabControl1.SelectedIndex = 3 Then
            get_level()
            btnLevel_Simpan.Tag = "&Simpan"
            btnLevel_Simpan.Text = Base_Language.Lang_Global_Simpan
        ElseIf TabControl1.SelectedIndex = 4 Then
            get_level_size()
            btnLvlSize_Simpan.Tag = "&Simpan"
            btnLvlSize_Simpan.Text = Base_Language.Lang_Global_Simpan
        ElseIf TabControl1.SelectedIndex = 5 Then
            get_posisi()
            btnPosisi_Simpan.Tag = "&Simpan"
            btnPosisi_Simpan.Text = Base_Language.Lang_Global_Simpan
        ElseIf TabControl1.SelectedIndex = 6 Then

            get_set_gudang()

            load_tab_gudang()
            btnSetGudang_Simpan.Tag = "&Simpan"
            btnSetGudang_Simpan.Text = Base_Language.Lang_Global_Simpan

        ElseIf TabControl1.SelectedIndex = 7 Then
            get_palet()
            btnPalet_Simpan.Tag = "&Simpan"
            btnPalet_Simpan.Text = Base_Language.Lang_Global_Simpan
        ElseIf TabControl1.SelectedIndex = 8 Then
            get_susunan()
            btnSusunan_Simpan.Tag = "&Simpan"
            btnSusunan_Simpan.Text = Base_Language.Lang_Global_Simpan
        End If




    End Sub
    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If cmbArea_Jenis.SelectedIndex = -1 Then Exit Sub
        get_area()
    End Sub
    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        get_kolom()
    End Sub

    Private Sub txtArea_KodeArea_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtArea_KodeArea.KeyPress
        If e.KeyChar = Chr(13) Then
            TxtArea_Ket.Focus()
        End If
    End Sub

    Private Sub txtArea_KodeArea_Leave(sender As Object, e As EventArgs) Handles txtArea_KodeArea.Leave
        If txtArea_KodeArea.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select kode_wms_area,keterangan, jenis_gudang From EMI_WMS_AREAS Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_wms_area = '" & txtArea_KodeArea.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtArea_KodeArea.Text = General_Class.CekNULL(Dr("kode_wms_area"))
                    TxtArea_Ket.Text = General_Class.CekNULL(Dr("keterangan"))
                    cmbArea_JenisGudang.Text = General_Class.CekNULL(Dr("jenis_gudang"))
                    Btn_Simpan_Area.Text = Base_Language.Lang_Global_Update
                    Btn_Simpan_Area.Tag = "&Update"
                Else
                    TxtArea_Ket.Clear()
                    cmbArea_JenisGudang.SelectedIndex = -1
                    Btn_Simpan_Area.Text = Base_Language.Lang_Global_Simpan
                    Btn_Simpan_Area.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub LvwArea_DoubleClick(sender As Object, e As EventArgs) Handles LvwArea.DoubleClick
        If LvwArea.Items.Count = 0 Then Exit Sub
        txtArea_KodeArea.Text = LvwArea.FocusedItem.SubItems(0).Text
        txtArea_KodeArea_Leave(LvwArea, e)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles btnArea_Hapus.Click
        If txtArea_KodeArea.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select kode_wms_area, jenis_gudang From EMI_WMS_AREAS Where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_wms_area = '" & txtArea_KodeArea.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "delete from emi_wms_areas where kode_perusahaan = '" & KodePerusahaan & "' and kode_wms_area = '" & txtArea_KodeArea.Text & "'"
                ExecuteTrans(SQL)
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK)
                TabControl1_Click(Me, e)
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If

    End Sub

    Private Sub TxtArea_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtArea_Ket.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbArea_JenisGudang.Focus()
        End If
    End Sub

    Private Sub txtKolom_KodeKolom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKolom_KodeKolom.KeyPress
        If e.KeyChar = Chr(13) Then
            txtKolom_Ket.Focus()
        End If
    End Sub

    Private Sub txtKolom_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKolom_Ket.KeyPress
        If e.KeyChar = Chr(13) Then
            btnSimpanKolom.Focus()
        End If
    End Sub



    Private Sub txtKolom_KodeKolom_Leave(sender As Object, e As EventArgs) Handles txtKolom_KodeKolom.Leave
        If txtKolom_KodeKolom.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select kode_wms_bay,keterangan From EMI_WMS_bay Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_wms_bay = '" & txtKolom_KodeKolom.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtKolom_KodeKolom.Text = General_Class.CekNULL(Dr("kode_wms_bay"))
                    txtKolom_Ket.Text = General_Class.CekNULL(Dr("keterangan"))

                    btnSimpanKolom.Text = Base_Language.Lang_Global_Update
                    btnSimpanKolom.Tag = "&Update"
                Else
                    txtKolom_Ket.Clear()

                    btnSimpanKolom.Text = Base_Language.Lang_Global_Simpan
                    btnSimpanKolom.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvwKolom_DoubleClick(sender As Object, e As EventArgs) Handles LvwKolom.DoubleClick
        If LvwKolom.Items.Count = 0 Then Exit Sub
        txtKolom_KodeKolom.Text = LvwKolom.FocusedItem.Text
        txtKolom_KodeKolom_Leave(LvwKolom, e)
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        TabControl1_Click(Me, e)
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        If txtKolom_KodeKolom.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select kode_wms_bay From EMI_WMS_BAY Where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_wms_bay = '" & txtKolom_KodeKolom.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "delete from EMI_WMS_BAY where kode_perusahaan = '" & KodePerusahaan & "' and kode_wms_bay = '" & txtKolom_KodeKolom.Text & "'"
                ExecuteTrans(SQL)
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK)
                kosong_kolom()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        kosong_kolom()
    End Sub

    Private Sub cmbArea_JenisGudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbArea_JenisGudang.KeyPress
        Btn_Simpan_Area.Focus()
    End Sub

    Private Sub txtBaris_KodeBaris_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBaris_KodeBaris.KeyPress
        If e.KeyChar = Chr(13) Then
            txtBaris_Ket.Focus()
        End If
    End Sub

    Private Sub txtBaris_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBaris_Ket.KeyPress
        If e.KeyChar = Chr(13) Then
            btnBaris_Simpan.Focus()
        End If
    End Sub

    Private Sub btnBaris_Simpan_Click(sender As Object, e As EventArgs) Handles btnBaris_Simpan.Click
        If txtBaris_KodeBaris.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_KodeBaris & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtBaris_KodeBaris.Focus()
            Exit Sub
        ElseIf txtBaris_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtBaris_Ket.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If btnBaris_Simpan.Tag = "&Simpan" Then
                SQL = "select kode_wms_row  from EMI_WMS_ROW where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_wms_row = '" & txtBaris_KodeBaris.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Error_Pernah_Simpan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "insert into EMI_WMS_ROW(kode_perusahaan,kode_wms_row,keterangan) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & txtBaris_KodeBaris.Text & "', '" & txtBaris_Ket.Text & "' )"
                ExecuteTrans(SQL)
            ElseIf btnBaris_Simpan.Tag = "&Update" Then
                SQL = "update EMI_WMS_ROW set keterangan = '" & txtBaris_Ket.Text & "' "
                SQL = SQL & "where kode_wms_row = '" & txtBaris_KodeBaris.Text & "'"
                ExecuteTrans(SQL)

                btnBaris_Simpan.Tag = "&Simpan"
                btnBaris_Simpan.Text = Base_Language.Lang_Global_Simpan
            End If


            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            TabControl1_Click(Me, e)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub btnBaris_Refresh_Click(sender As Object, e As EventArgs) Handles btnBaris_Refresh.Click
        TabControl1_Click(Me, e)
    End Sub

    Private Sub txtBaris_KodeBaris_Leave(sender As Object, e As EventArgs) Handles txtBaris_KodeBaris.Leave
        If txtBaris_KodeBaris.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select kode_wms_row,keterangan From EMI_WMS_row Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_wms_row = '" & txtBaris_KodeBaris.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtBaris_KodeBaris.Text = General_Class.CekNULL(Dr("kode_wms_row"))
                    txtBaris_Ket.Text = General_Class.CekNULL(Dr("keterangan"))

                    btnBaris_Simpan.Text = Base_Language.Lang_Global_Update
                    btnBaris_Simpan.Tag = "&Update"
                Else
                    txtBaris_Ket.Clear()

                    btnBaris_Simpan.Text = Base_Language.Lang_Global_Simpan
                    btnBaris_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvwBaris_DoubleClick(sender As Object, e As EventArgs) Handles LvwBaris.DoubleClick
        If LvwBaris.Items.Count = 0 Then Exit Sub

        txtBaris_KodeBaris.Text = LvwBaris.FocusedItem.Text
        txtBaris_KodeBaris_Leave(LvwBaris, e)

    End Sub

    Private Sub btnBaris_Hapus_Click(sender As Object, e As EventArgs) Handles btnBaris_Hapus.Click
        If txtBaris_KodeBaris.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select kode_wms_row From EMI_WMS_ROW Where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_wms_row = '" & txtBaris_KodeBaris.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "delete from EMI_WMS_ROW where kode_perusahaan = '" & KodePerusahaan & "' and kode_wms_row = '" & txtBaris_KodeBaris.Text & "'"
                ExecuteTrans(SQL)
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK)
                TabControl1_Click(Me, e)
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub txtLevel_KodeLevel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLevel_KodeLevel.KeyPress
        If e.KeyChar = Chr(13) Then
            txtLevel_Ket.Focus()
        End If
    End Sub

    Private Sub txtLevel_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLevel_Ket.KeyPress
        If e.KeyChar = Chr(13) Then
            btnLevel_Simpan.Focus()
        End If
    End Sub

    Private Sub btnLevel_Simpan_Click(sender As Object, e As EventArgs) Handles btnLevel_Simpan.Click
        If txtLevel_KodeLevel.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_KodeLevel & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtLevel_KodeLevel.Focus()
            Exit Sub
        ElseIf txtLevel_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtLevel_KodeLevel.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If btnLevel_Simpan.Tag = "&Simpan" Then
                SQL = "select kode_wms_level  from emi_wms_level where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_wms_level = '" & txtLevel_KodeLevel.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Error_Pernah_Simpan, Judul, MessageBoxButtons.OK)
                        Exit Sub


                    End If
                End Using


                SQL = "insert into EMI_WMS_LEVEL(kode_perusahaan,kode_wms_level,keterangan) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & txtLevel_KodeLevel.Text & "', '" & txtLevel_Ket.Text & "' )"
                ExecuteTrans(SQL)
            ElseIf btnLevel_Simpan.Tag = "&Update" Then
                SQL = "update EMI_WMS_LEVEL set keterangan = '" & txtLevel_Ket.Text & "' "
                SQL = SQL & "where kode_wms_level = '" & txtLevel_KodeLevel.Text & "'"
                ExecuteTrans(SQL)

                btnLevel_Simpan.Tag = "&Simpan"
                btnLevel_Simpan.Text = Base_Language.Lang_Global_Simpan
            End If


            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            TabControl1_Click(Me, e)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub btnLevel_Cari_Click(sender As Object, e As EventArgs) Handles btnLevel_Cari.Click
        get_level()
    End Sub

    Private Sub txtLevel_KodeLevel_Leave(sender As Object, e As EventArgs) Handles txtLevel_KodeLevel.Leave
        If txtLevel_KodeLevel.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select kode_wms_level,keterangan From EMI_WMS_Level Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_wms_level = '" & txtLevel_KodeLevel.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtLevel_KodeLevel.Text = General_Class.CekNULL(Dr("kode_wms_level"))
                    txtLevel_Ket.Text = General_Class.CekNULL(Dr("keterangan"))

                    btnLevel_Simpan.Text = Base_Language.Lang_Global_Update
                    btnLevel_Simpan.Tag = "&Update"
                Else
                    txtLevel_Ket.Clear()

                    btnLevel_Simpan.Text = Base_Language.Lang_Global_Simpan
                    btnLevel_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvwLevel_DoubleClick(sender As Object, e As EventArgs) Handles LvwLevel.DoubleClick
        If LvwLevel.Items.Count = 0 Then Exit Sub
        txtLevel_KodeLevel.Text = LvwLevel.FocusedItem.Text
        txtLevel_KodeLevel_Leave(LvwLevel, e)
    End Sub

    Private Sub btnLevel_Refresh_Click(sender As Object, e As EventArgs) Handles btnLevel_Refresh.Click
        TabControl1_Click(Me, e)
    End Sub

    Private Sub btnLevel_Hapus_Click(sender As Object, e As EventArgs) Handles btnLevel_Hapus.Click
        If txtLevel_KodeLevel.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select kode_wms_level From EMI_WMS_LEVEL Where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_wms_level = '" & txtLevel_KodeLevel.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "delete from EMI_WMS_LEVEL where kode_perusahaan = '" & KodePerusahaan & "' and kode_wms_level = '" & txtLevel_KodeLevel.Text & "'"
                ExecuteTrans(SQL)
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK)
                TabControl1_Click(Me, e)
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub txtLvlSize_KodeUkuran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLvlSize_KodeUkuran.KeyPress
        If e.KeyChar = Chr(13) Then
            txtLvlSize_Ket.Focus()
        End If
    End Sub

    Private Sub txtLvlSize_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLvlSize_Ket.KeyPress
        If e.KeyChar = Chr(13) Then
            txtLvlSize_Panjang.Focus()
        End If
    End Sub

    Private Sub txtLvlSize_Lebar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLvlSize_Lebar.KeyPress
        If e.KeyChar = Chr(13) Then
            txtLvlSize_Tinggi.Focus()
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub txtLvlSize_Tinggi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLvlSize_Tinggi.KeyPress
        If e.KeyChar = Chr(13) Then
            btnLvlSize_Simpan.Focus()
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub btnLvlSize_Simpan_Click(sender As Object, e As EventArgs) Handles btnLvlSize_Simpan.Click
        If txtLvlSize_KodeUkuran.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_KodeLevel & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtLevel_KodeLevel.Focus()
            Exit Sub
        ElseIf txtLvlSize_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtLvlSize_Ket.Focus()
            Exit Sub
        ElseIf txtLvlSize_Panjang.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Panjang & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtLvlSize_Panjang.Focus()
            Exit Sub
        ElseIf txtLvlSize_Lebar.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Lebar & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtLvlSize_Lebar.Focus()
            Exit Sub
        ElseIf txtLvlSize_Tinggi.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Tinggi & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtLvlSize_Tinggi.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If btnLvlSize_Simpan.Tag = "&Simpan" Then
                SQL = "select kode_wms_level_size  from emi_wms_level_size where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_wms_level_size = '" & txtLvlSize_KodeUkuran.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Error_Pernah_Simpan, Judul, MessageBoxButtons.OK)
                        Exit Sub

                    End If
                End Using


                SQL = "insert into EMI_WMS_LEVEL_SIZE(kode_perusahaan,kode_wms_level_size,keterangan,p,l,t) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & txtLvlSize_KodeUkuran.Text & "', '" & txtLvlSize_Ket.Text & "',"
                SQL = SQL & "'" & txtLvlSize_Panjang.Text & "', '" & txtLvlSize_Lebar.Text & "' , '" & txtLvlSize_Tinggi.Text & "' )"
                ExecuteTrans(SQL)
            ElseIf btnLvlSize_Simpan.Tag = "&Update" Then
                SQL = "update EMI_WMS_LEVEL_SIZE set keterangan = '" & txtLvlSize_Ket.Text & "', p = '" & txtLvlSize_Panjang.Text & "' "
                SQL = SQL & ",l = '" & txtLvlSize_Lebar.Text & "', t = '" & txtLvlSize_Tinggi.Text & "' "
                SQL = SQL & "where kode_wms_level_size = '" & txtLvlSize_KodeUkuran.Text & "'"
                ExecuteTrans(SQL)

                btnLvlSize_Simpan.Tag = "&Simpan"
                btnLvlSize_Simpan.Text = Base_Language.Lang_Global_Simpan
            End If


            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            TabControl1_Click(Me, e)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub txtLvlSize_KodeUkuran_Leave(sender As Object, e As EventArgs) Handles txtLvlSize_KodeUkuran.Leave
        If txtLvlSize_KodeUkuran.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select kode_wms_level_size,keterangan,p,l,t From EMI_WMS_Level_Size Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_wms_level_size = '" & txtLvlSize_KodeUkuran.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtLvlSize_KodeUkuran.Text = General_Class.CekNULL(Dr("kode_wms_level_size"))
                    txtLvlSize_Ket.Text = General_Class.CekNULL(Dr("keterangan"))
                    txtLvlSize_Panjang.Text = General_Class.CekNULL(Dr("p"))
                    txtLvlSize_Lebar.Text = General_Class.CekNULL(Dr("l"))
                    txtLvlSize_Tinggi.Text = General_Class.CekNULL(Dr("t"))
                    btnLvlSize_Simpan.Text = Base_Language.Lang_Global_Update
                    btnLvlSize_Simpan.Tag = "&Update"
                Else
                    txtLvlSize_Ket.Clear()
                    txtLvlSize_Panjang.Clear()
                    txtLvlSize_Lebar.Clear()
                    txtLvlSize_Tinggi.Clear()

                    btnLvlSize_Simpan.Text = Base_Language.Lang_Global_Simpan
                    btnLvlSize_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvwLvlSize_DoubleClick(sender As Object, e As EventArgs) Handles LvwLvlSize.DoubleClick
        If LvwLvlSize.Items.Count = 0 Then Exit Sub
        txtLvlSize_KodeUkuran.Text = LvwLvlSize.FocusedItem.Text
        txtLvlSize_KodeUkuran_Leave(LvwLvlSize, e)
    End Sub

    Private Sub btnLvlSize_Hapus_Click(sender As Object, e As EventArgs) Handles btnLvlSize_Hapus.Click
        If txtLvlSize_KodeUkuran.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select kode_wms_level_size From EMI_WMS_LEVEL_SIZE Where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_wms_level_SIZE = '" & txtLvlSize_KodeUkuran.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "delete from EMI_WMS_LEVEL_size where kode_perusahaan = '" & KodePerusahaan & "' and kode_wms_level_size = '" & txtLvlSize_KodeUkuran.Text & "'"
                ExecuteTrans(SQL)
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK)
                TabControl1_Click(Me, e)
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub btnLvlSize_Refresh_Click(sender As Object, e As EventArgs) Handles btnLvlSize_Refresh.Click
        TabControl1_Click(Me, e)
    End Sub

    Private Sub btnLvlSize_Cari_Click(sender As Object, e As EventArgs) Handles btnLvlSize_Cari.Click
        get_level_size()
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        get_posisi()
    End Sub

    Private Sub btnPosisi_Simpan_Click(sender As Object, e As EventArgs) Handles btnPosisi_Simpan.Click
        If txtPosisi_KodePosisi.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_KodePosisi & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtPosisi_KodePosisi.Focus()
            Exit Sub
        ElseIf txtPosisi_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtPosisi_Ket.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If btnPosisi_Simpan.Tag = "&Simpan" Then
                SQL = "select kode_wms_position  from emi_wms_position where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_wms_position  = '" & txtPosisi_KodePosisi.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Error_Pernah_Simpan, Judul, MessageBoxButtons.OK)
                        Exit Sub


                    End If
                End Using


                SQL = "insert into EMI_WMS_Position(kode_perusahaan,kode_wms_position,keterangan) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & txtPosisi_KodePosisi.Text & "', '" & txtPosisi_Ket.Text & "' )"
                ExecuteTrans(SQL)
            ElseIf btnPosisi_Simpan.Tag = "&Update" Then
                SQL = "update EMI_WMS_POSITION set keterangan = '" & txtPosisi_Ket.Text & "' "
                SQL = SQL & "where kode_wms_position = '" & txtPosisi_KodePosisi.Text & "'"
                ExecuteTrans(SQL)

                btnPosisi_Simpan.Tag = "&Simpan"
                btnPosisi_Simpan.Text = Base_Language.Lang_Global_Simpan
            End If


            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            TabControl1_Click(Me, e)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub txtPosisi_KodePosisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPosisi_KodePosisi.KeyPress
        If e.KeyChar = Chr(13) Then
            txtPosisi_Ket.Focus()
        End If
    End Sub

    Private Sub txtPosisi_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPosisi_Ket.KeyPress
        If e.KeyChar = Chr(13) Then
            btnPosisi_Simpan.Focus()
        End If
    End Sub

    Private Sub txtPosisi_KodePosisi_Leave(sender As Object, e As EventArgs) Handles txtPosisi_KodePosisi.Leave
        If txtPosisi_KodePosisi.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select kode_wms_position,keterangan From EMI_WMS_Position Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_wms_position = '" & txtPosisi_KodePosisi.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtPosisi_KodePosisi.Text = General_Class.CekNULL(Dr("kode_wms_position"))
                    txtPosisi_Ket.Text = General_Class.CekNULL(Dr("keterangan"))

                    btnPosisi_Simpan.Text = Base_Language.Lang_Global_Update
                    btnPosisi_Simpan.Tag = "&Update"
                Else
                    txtLvlSize_Ket.Clear()

                    btnPosisi_Simpan.Text = Base_Language.Lang_Global_Simpan
                    btnPosisi_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvwPosisi_DoubleClick(sender As Object, e As EventArgs) Handles LvwPosisi.DoubleClick
        If LvwPosisi.Items.Count = 0 Then Exit Sub
        txtPosisi_KodePosisi.Text = LvwPosisi.FocusedItem.Text
        txtPosisi_KodePosisi_Leave(LvwPosisi, e)
    End Sub

    Private Sub btnPosisi_Refresh_Click(sender As Object, e As EventArgs) Handles btnPosisi_Refresh.Click
        TabControl1_Click(Me, e)
    End Sub

    Private Sub btnPosisi_Hapus_Click(sender As Object, e As EventArgs) Handles btnPosisi_Hapus.Click
        If txtPosisi_KodePosisi.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select kode_wms_position From EMI_WMS_POSITION Where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_wms_position = '" & txtPosisi_KodePosisi.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "delete from EMI_WMS_POSITION where kode_perusahaan = '" & KodePerusahaan & "' and kode_wms_position = '" & txtPosisi_KodePosisi.Text & "'"
                ExecuteTrans(SQL)
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK)
                TabControl1_Click(Me, e)
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub btnPalet_Cari_Click(sender As Object, e As EventArgs) Handles btnPalet_Cari.Click
        get_palet()
    End Sub

    Private Sub btnPalet_Refresh_Click(sender As Object, e As EventArgs) Handles btnPalet_Refresh.Click
        TabControl1_Click(Me, e)
    End Sub

    Private Sub btnPalet_Simpan_Click(sender As Object, e As EventArgs) Handles btnPalet_Simpan.Click
        If txtPalet_KodeUkuran.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_KodePalet & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtPalet_KodeUkuran.Focus()
            Exit Sub
        ElseIf txtPalet_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtPalet_Ket.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If btnPalet_Simpan.Tag = "&Simpan" Then
                SQL = "select kode_wms_pallet  from emi_wms_pallet where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_wms_pallet  = '" & txtPalet_KodeUkuran.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Error_Pernah_Simpan, Judul, MessageBoxButtons.OK)
                        Exit Sub


                    End If
                End Using


                SQL = "insert into EMI_WMS_Pallet(kode_perusahaan,kode_wms_pallet,keterangan,p,l,t, satuan) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & txtPalet_KodeUkuran.Text & "', '" & txtPalet_Ket.Text & "' ,"
                SQL = SQL & "'" & txtPalet_Panjang.Text & "', '" & txtPalet_Lebar.Text & "', '" & txtPalet_Tinggi.Text & "','CM'  )"
                ExecuteTrans(SQL)
            ElseIf btnPalet_Simpan.Tag = "&Update" Then
                SQL = "update EMI_WMS_Pallet set keterangan = '" & txtPalet_Ket.Text & "', p = '" & txtPalet_Panjang.Text & "', "
                SQL = SQL & "l = '" & txtPalet_Lebar.Text & "', t = '" & txtPalet_Tinggi.Text & "' "
                SQL = SQL & "where kode_wms_pallet = '" & txtPalet_KodeUkuran.Text & "'"
                ExecuteTrans(SQL)

                btnPalet_Simpan.Tag = "&Simpan"
                btnPalet_Simpan.Text = Base_Language.Lang_Global_Simpan
            End If


            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            TabControl1_Click(Me, e)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub txtPalet_KodeUkuran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPalet_KodeUkuran.KeyPress
        If e.KeyChar = Chr(13) Then
            txtPalet_Ket.Focus()
        End If
    End Sub

    Private Sub txtPalet_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPalet_Ket.KeyPress
        If e.KeyChar = Chr(13) Then
            txtPalet_Panjang.Focus()
        End If
    End Sub

    Private Sub txtPalet_KodeUkuran_Leave(sender As Object, e As EventArgs) Handles txtPalet_KodeUkuran.Leave
        If txtPalet_KodeUkuran.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select kode_wms_pallet,keterangan,p,l,t From EMI_WMS_Pallet Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_wms_Pallet = '" & txtPalet_KodeUkuran.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtPalet_KodeUkuran.Text = General_Class.CekNULL(Dr("kode_wms_pallet"))
                    txtPalet_Ket.Text = General_Class.CekNULL(Dr("keterangan"))
                    txtPalet_Panjang.Text = General_Class.CekNULL(Dr("p"))
                    txtPalet_Lebar.Text = General_Class.CekNULL(Dr("l"))
                    txtPalet_Tinggi.Text = General_Class.CekNULL(Dr("t"))

                    btnPalet_Simpan.Text = Base_Language.Lang_Global_Update
                    btnPalet_Simpan.Tag = "&Update"
                Else
                    txtPalet_Ket.Clear()
                    txtPalet_Panjang.Clear()
                    txtPalet_Lebar.Clear()
                    txtPalet_Tinggi.Clear()


                    btnPalet_Simpan.Text = Base_Language.Lang_Global_Simpan
                    btnPalet_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvwPallet_DoubleClick(sender As Object, e As EventArgs) Handles LvwPallet.DoubleClick
        If LvwPallet.Items.Count = 0 Then Exit Sub
        txtPalet_KodeUkuran.Text = LvwPallet.FocusedItem.Text
        txtPalet_KodeUkuran_Leave(LvwPallet, e)
    End Sub

    Private Sub btnPalet_Hapus_Click(sender As Object, e As EventArgs) Handles btnPalet_Hapus.Click
        If txtPalet_KodeUkuran.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select kode_wms_pallet From emi_wms_pallet Where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_wms_pallet = '" & txtPalet_KodeUkuran.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "delete from EMI_WMS_Pallet where kode_perusahaan = '" & KodePerusahaan & "' and kode_wms_pallet = '" & txtPalet_KodeUkuran.Text & "'"
                ExecuteTrans(SQL)
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK)
                TabControl1_Click(Me, e)
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub cmbSusunan_Pallet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSusunan_Pallet.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbSusunan_Susunan.Focus()
        End If
    End Sub

    Private Sub cmbSusunan_Susunan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSusunan_Susunan.KeyPress
        If e.KeyChar = Chr(13) Then
            txtSusunan_Panjang.Focus()
        End If
    End Sub

    Private Sub txtSusunan_Kode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSusunan_Kode.KeyPress
        If e.KeyChar = Chr(13) Then
            txtSusunan_Ket.Focus()
        End If
    End Sub

    Private Sub txtSusunan_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSusunan_Ket.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbSusunan_Pallet.Focus()
        End If
    End Sub

    Private Sub txtSusunan_Panjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSusunan_Panjang.KeyPress
        If e.KeyChar = Chr(13) Then
            txtSusunan_Lebar.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub txtSusunan_Lebar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSusunan_Lebar.KeyPress
        If e.KeyChar = Chr(13) Then
            txtSusunan_Tinggi.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub txtSusunan_Tinggi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSusunan_Tinggi.KeyPress
        If e.KeyChar = Chr(13) Then
            btnSusunan_Simpan.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub btnSusunan_Simpan_Click(sender As Object, e As EventArgs) Handles btnSusunan_Simpan.Click
        If cmbSusunan_Pallet.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_KodePalet & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbSusunan_Pallet.Focus()
            Exit Sub
        ElseIf cmbSusunan_Susunan.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_Susunan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbSusunan_Susunan.Focus()
            Exit Sub
        ElseIf txtSusunan_Kode.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_KodePalet & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtSusunan_Kode.Focus()
            Exit Sub
        ElseIf txtSusunan_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtSusunan_Ket.Focus()
            Exit Sub
        ElseIf txtSusunan_Panjang.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Panjang & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtSusunan_Panjang.Focus()
            Exit Sub
        ElseIf txtSusunan_Lebar.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Lebar & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtSusunan_Lebar.Focus()
            Exit Sub
        ElseIf txtSusunan_Tinggi.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Tinggi & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            txtSusunan_Tinggi.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If btnSusunan_Simpan.Tag = "&Simpan" Then
                SQL = "select kode_wms_susunan  from EMI_WMS_SUSUNAN where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_wms_susunan  = '" & txtSusunan_Kode.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Error_Pernah_Simpan, Judul, MessageBoxButtons.OK)
                        Exit Sub

                    End If
                End Using


                SQL = "insert into EMI_WMS_Susunan(kode_perusahaan,kode_wms_susunan,keterangan,p,l,t,id_wms_pallet,jenis_susunan, satuan) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & txtSusunan_Kode.Text & "', '" & txtSusunan_Ket.Text & "' ,"
                SQL = SQL & "'" & txtSusunan_Panjang.Text & "', '" & txtSusunan_Lebar.Text & "', '" & txtSusunan_Tinggi.Text & "',"
                SQL = SQL & "'" & arrPalletId.Item(cmbSusunan_Pallet.SelectedIndex) & "', '" & cmbSusunan_Susunan.Text & "','CM') "
                ExecuteTrans(SQL)


            ElseIf btnSusunan_Simpan.Tag = "&Update" Then
                SQL = "update EMI_WMS_Susunan set  keterangan = '" & txtSusunan_Ket.Text & "', p = '" & txtSusunan_Panjang.Text & "', "
                SQL = SQL & "l = '" & txtSusunan_Lebar.Text & "', t = '" & txtSusunan_Tinggi.Text & "', jenis_susunan = '" & cmbSusunan_Susunan.Text & "', "
                SQL = SQL & "id_wms_pallet = '" & arrPalletId.Item(cmbSusunan_Pallet.SelectedIndex) & "' where kode_wms_susunan = '" & txtSusunan_Kode.Text & "'"
                ExecuteTrans(SQL)

                btnSusunan_Simpan.Tag = "&Simpan"
                btnSusunan_Simpan.Text = Base_Language.Lang_Global_Simpan
            End If


            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            TabControl1_Click(Me, e)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub cmbSusunan_Susunan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSusunan_Susunan.SelectedIndexChanged
        If cmbSusunan_Susunan.SelectedIndex = 0 Then
            lblPlus.Visible = True
            lblKali.Visible = False
            lblKali2.Visible = True
        ElseIf cmbSusunan_Susunan.SelectedIndex = 1 Then
            lblPlus.Visible = False
            lblKali.Visible = True
            lblKali2.Visible = True
            lblKali.Location = New Point(238, 135)
        Else
            lblPlus.Visible = False
            lblKali.Visible = False
            lblKali2.Visible = False
        End If
    End Sub

    Private Sub btnSusunan_Refresh_Click(sender As Object, e As EventArgs) Handles btnSusunan_Refresh.Click
        TabControl1_Click(Me, e)
    End Sub

    Private Sub btnSusunan_Cari_Click(sender As Object, e As EventArgs) Handles btnSusunan_Cari.Click
        get_susunan()
    End Sub

    Private Sub txtSusunan_Kode_Leave(sender As Object, e As EventArgs) Handles txtSusunan_Kode.Leave
        If txtSusunan_Kode.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "select a.Kode_WMS_Susunan,a.Keterangan,b.Kode_WMS_Pallet as pallet,a.Jenis_Susunan,a.p,a.l,a.t "
            SQL = SQL & "from EMI_WMS_Susunan a, EMI_WMS_Pallet b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_WMS_Pallet = b.Id_WMS_Pallet "
            SQL = SQL & "and a.kode_wms_susunan  = '" & txtSusunan_Kode.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtSusunan_Kode.Text = General_Class.CekNULL(Dr("kode_wms_susunan"))
                    txtSusunan_Ket.Text = General_Class.CekNULL(Dr("keterangan"))
                    cmbSusunan_Pallet.Text = General_Class.CekNULL(Dr("pallet"))
                    cmbSusunan_Susunan.Text = General_Class.CekNULL(Dr("jenis_susunan"))
                    txtSusunan_Panjang.Text = General_Class.CekNULL(Dr("p"))
                    txtSusunan_Lebar.Text = General_Class.CekNULL(Dr("l"))
                    txtSusunan_Tinggi.Text = General_Class.CekNULL(Dr("t"))

                    btnSusunan_Simpan.Text = Base_Language.Lang_Global_Update
                    btnSusunan_Simpan.Tag = "&Update"
                Else
                    txtSusunan_Ket.Clear()
                    txtSusunan_Lebar.Clear()
                    txtSusunan_Tinggi.Clear()
                    txtSusunan_Panjang.Clear()
                    cmbSusunan_Susunan.SelectedIndex = -1
                    cmbSusunan_Pallet.SelectedIndex = -1


                    btnSusunan_Simpan.Text = Base_Language.Lang_Global_Simpan
                    btnSusunan_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvwSusunan_DoubleClick(sender As Object, e As EventArgs) Handles LvwSusunan.DoubleClick
        If LvwSusunan.Items.Count = 0 Then Exit Sub
        txtSusunan_Kode.Text = LvwSusunan.FocusedItem.Text
        txtSusunan_Kode_Leave(LvwSusunan, e)
    End Sub

    Private Sub btnSusunan_Hapus_Click(sender As Object, e As EventArgs) Handles btnSusunan_Hapus.Click
        If txtSusunan_Kode.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select kode_wms_susunan From emi_wms_susunan Where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_wms_susunan = '" & txtSusunan_Kode.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "delete from EMI_WMS_SUSUNAN where kode_perusahaan = '" & KodePerusahaan & "' and kode_wms_susunan = '" & txtSusunan_Kode.Text & "'"
                ExecuteTrans(SQL)
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK)
                TabControl1_Click(Me, e)
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub btnSetGudang_Simpan_Click(sender As Object, e As EventArgs) Handles btnSetGudang_Simpan.Click
        If cmbSetGudang_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_LokasiGudang & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbSetGudang_Lokasi.Focus()
            Exit Sub
        ElseIf cmbSetGudang_Area.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_Area & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbSetGudang_Area.Focus()
            Exit Sub
        ElseIf cmbSetGudang_Baris.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_Baris & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbSetGudang_Baris.Focus()
            Exit Sub
        ElseIf cmbSetGudangKolom.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_Kolom & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbSetGudangKolom.Focus()
            Exit Sub
        ElseIf cmbSetGudang_Level.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_Level & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbSetGudang_Level.Focus()
            Exit Sub
        ElseIf cmbSetGudang_UkLvl.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_LevelSize & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbSetGudang_Level.Focus()
            Exit Sub
        ElseIf cmbSetGudang_Posisi.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_SetGudang_Position & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            cmbSetGudang_Posisi.Focus()
            Exit Sub
        ElseIf Cmb_KategoriGudang.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_KategoriGudang & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK)
            Cmb_KategoriGudang.Focus()
            Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah Pallet Belum Diisi...!! ", Judul, MessageBoxButtons.OK)
            TextBox1.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If btnSetGudang_Simpan.Tag = "&Simpan" Then
                SQL = "select id_wms_warehouse_position  from EMI_WMS_Warehouse_Position where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_stock_owner  = '" & cmbSetGudang_Lokasi.Text & "' and id_wms_areas = '" & arrDataAreas.Item(cmbSetGudang_Area.SelectedIndex) & "' "
                SQL = SQL & "and id_wms_bay = '" & arrDataBay.Item(cmbSetGudangKolom.SelectedIndex) & "' and id_wms_row = '" & arrDataRow.Item(cmbSetGudang_Baris.SelectedIndex) & "' "
                SQL = SQL & "and id_wms_level = '" & arrDataLevel.Item(cmbSetGudang_Level.SelectedIndex) & "' and id_wms_level_size = '" & arrDataLevelSize(cmbSetGudang_UkLvl.SelectedIndex) & "'  "
                SQL = SQL & "and id_master_kategori_gudang = '" & arrKategoriGudang(Cmb_KategoriGudang.SelectedIndex) & "' "
                SQL = SQL & "and id_wms_position = '" & arrDataPosition(cmbSetGudang_Posisi.SelectedIndex) & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Error_Pernah_Simpan, Judul, MessageBoxButtons.OK)
                        Exit Sub

                    End If
                End Using

                SQL = "insert into EMI_WMS_Warehouse_Position(Kode_Perusahaan,Kode_Stock_Owner,Id_WMS_Row,Id_WMS_Bay,Id_WMS_Level,Id_WMS_Level_Size,Id_WMS_Position, id_master_kategori_gudang, id_wms_areas,Jumlah_Pallet) values( "
                SQL = SQL & "'" & KodePerusahaan & "','" & cmbSetGudang_Lokasi.Text & "' ,'" & arrDataRow.Item(cmbSetGudang_Baris.SelectedIndex) & "', '" & arrDataBay.Item(cmbSetGudangKolom.SelectedIndex) & "' ,"
                SQL = SQL & "'" & arrDataLevel.Item(cmbSetGudang_Level.SelectedIndex) & "', '" & arrDataLevelSize.Item(cmbSetGudang_UkLvl.SelectedIndex) & "', '" & arrDataPosition.Item(cmbSetGudang_Posisi.SelectedIndex) & "',"
                SQL = SQL & "'" & arrKategoriGudang.Item(Cmb_KategoriGudang.SelectedIndex) & "',"
                SQL = SQL & "'" & arrDataAreas.Item(cmbSetGudang_Area.SelectedIndex) & "','" & HilangkanTanda(TextBox1.Text) & "')"
                ExecuteTrans(SQL)
            End If


            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
            TabControl1_Click(Me, e)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub btnSetGudang_Cari_Click(sender As Object, e As EventArgs) Handles btnSetGudang_Cari.Click
        get_set_gudang()
    End Sub

    Private Sub btnSetGudang_Refresh_Click(sender As Object, e As EventArgs) Handles btnSetGudang_Refresh.Click
        TabControl1_Click(Me, e)
    End Sub

    Private Sub btnSetGudang_Hapus_Click(sender As Object, e As EventArgs) Handles btnSetGudang_Hapus.Click
        If cmbSetGudang_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select * From emi_wms_warehouse_position Where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & cmbSetGudang_Lokasi.Text & "' and id_wms_areas = '" & arrDataAreas.Item(cmbSetGudang_Area.SelectedIndex) & "' "
                SQL = SQL & "and id_wms_bay = '" & arrDataBay.Item(cmbSetGudangKolom.SelectedIndex) & "' and id_wms_row = '" & arrDataRow.Item(cmbSetGudang_Baris.SelectedIndex) & "' "
                SQL = SQL & "and id_wms_level = '" & arrDataLevel.Item(cmbSetGudang_Level.SelectedIndex) & "' and id_wms_level_size = '" & arrDataLevelSize.Item(cmbSetGudang_UkLvl.SelectedIndex) & "' "
                SQL = SQL & "and id_master_kategori_gudang = '" & arrKategoriGudang.Item(Cmb_KategoriGudang.SelectedIndex) & "' "
                SQL = SQL & "and id_wms_position = '" & arrDataPosition(cmbSetGudang_Posisi.SelectedIndex) & "' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End Using


                SQL = "delete from emi_wms_warehouse_position where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & cmbSetGudang_Lokasi.Text & "' "
                SQL = SQL & "and id_wms_areas = '" & arrDataAreas.Item(cmbSetGudang_Area.SelectedIndex) & "'  "
                SQL = SQL & "and id_wms_bay = '" & arrDataBay.Item(cmbSetGudangKolom.SelectedIndex) & "' and id_wms_row = '" & arrDataRow.Item(cmbSetGudang_Baris.SelectedIndex) & "'"
                SQL = SQL & "and id_wms_level = '" & arrDataLevel.Item(cmbSetGudang_Level.SelectedIndex) & "' and id_wms_level_size = '" & arrDataLevelSize.Item(cmbSetGudang_UkLvl.SelectedIndex) & "' "
                SQL = SQL & "and id_master_kategori_gudang = '" & arrKategoriGudang.Item(Cmb_KategoriGudang.SelectedIndex) & "' "
                SQL = SQL & "and id_wms_position = '" & arrDataPosition(cmbSetGudang_Posisi.SelectedIndex) & "' "

                ExecuteTrans(SQL)
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK)
                TabControl1_Click(Me, e)
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub LvwSetGudang_DoubleClick(sender As Object, e As EventArgs) Handles LvwSetGudang.DoubleClick
        If LvwSetGudang.Items.Count = 0 Then Exit Sub

        cmbSetGudang_Lokasi.Text = LvwSetGudang.FocusedItem.Text
        cmbSetGudang_Area.Text = LvwSetGudang.FocusedItem.SubItems(1).Text
        cmbSetGudangKolom.Text = LvwSetGudang.FocusedItem.SubItems(2).Text
        cmbSetGudang_Baris.Text = LvwSetGudang.FocusedItem.SubItems(3).Text
        cmbSetGudang_Level.Text = LvwSetGudang.FocusedItem.SubItems(4).Text
        cmbSetGudang_UkLvl.Text = LvwSetGudang.FocusedItem.SubItems(5).Text
        cmbSetGudang_Posisi.Text = LvwSetGudang.FocusedItem.SubItems(9).Text
        Cmb_KategoriGudang.Text = LvwSetGudang.FocusedItem.SubItems(10).Text
        TextBox1.Text = HilangkanTanda(LvwSetGudang.FocusedItem.SubItems(11).Text)
    End Sub

    Private Sub txtSusunan_Panjang_TextChanged(sender As Object, e As EventArgs) Handles txtSusunan_Panjang.TextChanged

    End Sub

    Private Sub txtPalet_Panjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPalet_Panjang.KeyPress
        If e.KeyChar = Chr(13) Then txtPalet_Lebar.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub txtPalet_Lebar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPalet_Lebar.KeyPress
        If e.KeyChar = Chr(13) Then txtPalet_Tinggi.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub txtPalet_Tinggi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPalet_Tinggi.KeyPress
        If e.KeyChar = Chr(13) Then btnPalet_Simpan.Focus()
    End Sub

    Private Sub txtLvlSize_Panjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLvlSize_Panjang.KeyPress
        If e.KeyChar = Chr(13) Then txtLvlSize_Lebar.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then btnSetGudang_Simpan.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub CetakBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakBarcodeToolStripMenuItem.Click
        If LvwSetGudang.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Dim kode_unik_print As String = ""
            Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

            SQL = "delete from Cetak_Barcode_Set_Gudang "
            SQL = SQL & "where Tanggal_Cetak <= '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "'"
            ExecuteTrans(SQL)

            SQL = "Select Id_Nametag,Labeling_WMS_Position,Kode_Stock_Owner "
            SQL = SQL & "From View_Warehouse_Position "
            SQL = SQL & "Where Id_WMS_Warehouse_Position = '" & arrSetGudang(LvwSetGudang.FocusedItem.Index) & "'"
            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim rnd As New Random()

                        kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(rnd.Next(0, 10000), "00000")

                        For i As Integer = 0 To .Rows.Count - 1

                            If IsDBNull(.Rows(i).Item("id_nametag")) Then
                                MessageBox.Show("ID Nametag untuk data ini kosong!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                CloseConn()
                                Exit Sub
                            End If

                            Dim fullNewQr As String = .Rows(i).Item("id_nametag")

                            QRCode.Image = Generate_QR(fullNewQr)

                            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "QRSetGudang" & kode_unik_print & ".jpg")
                            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                            QRCode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                            'End If

                            FS = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                            FileSize = FS.Length
                            rawData = New Byte(FileSize) {}
                            FS.Read(rawData, 0, FileSize)
                            FS.Close()
                            Cmd.Parameters.Add("@NewQRCode", SqlDbType.Image).Value = rawData


                            SQL = "insert into Cetak_Barcode_Set_Gudang (Id_Nametag,Labeling_WMS_Position,Kode_Stock_Owner,Kode_Unik_Print,Tanggal_Cetak,QRCode) values "
                            SQL = SQL & "('" & .Rows(i).Item("id_nametag") & "', '" & .Rows(i).Item("labeling_WMS_Position") & "','" & .Rows(i).Item("kode_stock_owner") & "','"
                            SQL = SQL & kode_unik_print & "','" & Format(tgl_skg, "yyyy-MM-dd") & "', @NewQRCode)"
                            ExecuteTrans(SQL)
                        Next
                    Else
                        MessageBox.Show("ID WMS Warehouse Position tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CloseConn()
                        Exit Sub
                    End If
                End With
            End Using

            '================================================================================

            SQL = "select top 1 Id_Nametag from Cetak_Barcode_Set_Gudang "
            SQL = SQL & "where kode_unik_print='" & kode_unik_print & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Master_Gudang_QR_Code

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Barcode_Set_Gudang.kode_unik_print} = '" & kode_unik_print & "'"
                    '    CrDoc.SummaryInfo.ReportTitle = "Barcode"
                    '    .Text = "Barcode"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_Barcode_Set_Gudang.kode_unik_print} = '" & kode_unik_print & "'"

                    CrDoc.PrintOptions.PrinterName = PrinterBarcode
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    CrDoc.PrintToPrinter(1, False, 1, 2500)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class
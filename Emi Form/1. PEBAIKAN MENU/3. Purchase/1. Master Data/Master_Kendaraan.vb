Public Class Master_Kendaraan
    Dim arrcari As New ArrayList
    Dim arrCaraKirim As New ArrayList
    Dim Jenis = "Master_MediaKirim"

    Dim LvCaraKirim, LvKdMediaKirim, LvMediaKirim, LvPanjang, LvLebar, LvTinggi, LvVolume As String
    Dim LvSatuanPanjang, LvSatuanVolume, LvBerat, LvSatuanBerat, LvId As String

    Dim itemCaraKirim As Integer = 0
    Dim itemKdMediaKirim As Integer = 1
    Dim itemMediaKirim As Integer = 2
    Dim itemPanjang As Integer = 3
    Dim itemLebar As Integer = 4
    Dim itemTinggi As Integer = 5
    Dim itemVolume As Integer = 6
    Dim itemSatuanPanjang As Integer = 7
    Dim itemSatuanVolume As Integer = 8
    Dim itemBerat As Integer = 9
    Dim itemSatuanBerat As Integer = 10
    Dim itemId As Integer = 11

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvCaraKirim = Lv_DataKendaraan.Items(NoIndex).Text
        LvKdMediaKirim = Lv_DataKendaraan.Items(NoIndex).SubItems(itemKdMediaKirim).Text
        LvMediaKirim = Lv_DataKendaraan.Items(NoIndex).SubItems(itemMediaKirim).Text
        LvPanjang = Lv_DataKendaraan.Items(NoIndex).SubItems(itemPanjang).Text
        LvLebar = Lv_DataKendaraan.Items(NoIndex).SubItems(itemLebar).Text
        LvTinggi = Lv_DataKendaraan.Items(NoIndex).SubItems(itemTinggi).Text
        LvVolume = Lv_DataKendaraan.Items(NoIndex).SubItems(itemVolume).Text
        LvSatuanPanjang = Lv_DataKendaraan.Items(NoIndex).SubItems(itemSatuanPanjang).Text
        LvSatuanVolume = Lv_DataKendaraan.Items(NoIndex).SubItems(itemSatuanVolume).Text
        LvBerat = Lv_DataKendaraan.Items(NoIndex).SubItems(itemBerat).Text
        LvSatuanBerat = Lv_DataKendaraan.Items(NoIndex).SubItems(itemSatuanBerat).Text
        LvId = Lv_DataKendaraan.Items(NoIndex).SubItems(itemId).Text
    End Sub

    Private Sub Master_Media_Kirim_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Media_Kirim_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom


            Lv_DataKendaraan.Columns.Clear()
            Lv_DataKendaraan.Columns.Add("IdKendaraan", 0, HorizontalAlignment.Left) '0
            Lv_DataKendaraan.Columns.Add("Kode Kendaraan", 150, HorizontalAlignment.Left) '1
            Lv_DataKendaraan.Columns.Add("Keterangan", 350, HorizontalAlignment.Left) '2
            Lv_DataKendaraan.Columns.Add("Kapasitas", 180, HorizontalAlignment.Right) '3
            Lv_DataKendaraan.Columns.Add("Volume", 180, HorizontalAlignment.Right) '4
            Lv_DataKendaraan.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub kosong()

        Cmb_Filter.Items.Clear() : arrcari.Clear()
        Cmb_Filter.Items.Add("Kode Kendaraan") : arrcari.Add("Kode_Kendaraan")
        Cmb_Filter.Items.Add("Keterangan") : arrcari.Add("Keterangan")
        TextBox3.Text = ""

        Cmb_CaraKirim.Items.Clear()
        Cmb_SatuanVolume.Items.Clear()
        Cmb_SatuanBerat.Items.Clear()
        Txt_KdKendaraan.Text = ""
        Txt_Keterangan.Text = ""
        Txt_Panjang.Text = ""
        Txt_Lebar.Text = ""
        Txt_Tinggi.Text = ""
        Txt_Volume.Text = ""
        Txt_Berat.Text = ""

        Id_Kendaraaan.Text = ""
        Txt_Keterangan.Enabled = True

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

        Btn_Simpan.Text = "&Simpan" : Btn_Simpan.Tag = "SIMPAN"

        Btn_Simpan.Enabled = True : Btn_Hapus.Enabled = False
        Txt_KdKendaraan.Enabled = True

        Get_Cara_Kirim()
        Get_Satuan_Volume()
        Get_Satuan_Berat()

        Cari("Y")
    End Sub

    Private Sub Get_Cara_Kirim()
        Try
            OpenConn()
            Cmb_CaraKirim.Items.Clear() : arrCaraKirim.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Cara_Kirim "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Flag_Tampil = 'Y' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_CaraKirim.Items.Add(dr("Keterangan")) : arrCaraKirim.Add(dr("Id_Cara_Kirim"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Txt_Volume_TextChanged(sender As Object, e As EventArgs) Handles Txt_Volume.TextChanged
        Hitung_Volume()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
        Cmb_CaraKirim.Focus()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Dim caraKirim = Cmb_CaraKirim.SelectedItem
        Dim kdMediaKirim = Txt_KdKendaraan.Text
        Dim mediaKirim = Txt_Keterangan.Text
        Dim panjang = Txt_Panjang.Text
        Dim lebar = Txt_Lebar.Text
        Dim tinggi = Txt_Tinggi.Text
        Dim volume = Txt_Volume.Text
        Dim satuanVolume = Cmb_SatuanVolume.SelectedItem
        Dim berat = Txt_Berat.Text
        Dim satuanBerat = Cmb_SatuanBerat.SelectedItem

        If Cmb_CaraKirim.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Cara_Kirim & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_CaraKirim.Focus() : Exit Sub
        ElseIf kdMediaKirim.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_MediaKirim_KodeMediaKirim & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_KdKendaraan.Focus() : Exit Sub
        ElseIf mediaKirim.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Media_Kirim & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Keterangan.Focus() : Exit Sub
        ElseIf panjang.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Panjang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Panjang.Focus() : Exit Sub
        ElseIf lebar.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Lebar & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Lebar.Focus() : Exit Sub
        ElseIf tinggi.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Tinggi & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Tinggi.Focus() : Exit Sub
        ElseIf volume.Trim.Length = 0 Then
            MessageBox.Show("Volume" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Volume.Focus() : Exit Sub
        ElseIf Cmb_SatuanVolume.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_SatuanVolume & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_SatuanVolume.Focus() : Exit Sub
        ElseIf berat.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Berat & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Berat.Focus() : Exit Sub
        ElseIf Cmb_SatuanBerat.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_SatuanBerat & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_SatuanBerat.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            Dim Action As String = ""

            If Btn_Simpan.Tag.ToString.ToUpper = "SIMPAN" Then

                SQL = "insert into Master_Kendaraan (Kode_Perusahaan, Kode_Kendaraan, Keterangan, Kapasitas, Satuan_Kapasitas, Panjang, Lebar, Tinggi, Volume, Satuan_Volume, id_jenisEkspedisi) "
                SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_KdKendaraan.Text & "', '" & Txt_Keterangan.Text & "', " & Val(HilangkanTanda(Txt_Berat.Text)) & ", "
                SQL = SQL & "'" & Cmb_SatuanBerat.Text.Trim & "', " & Val(HilangkanTanda(Txt_Panjang.Text)) & ", " & Val(HilangkanTanda(Txt_Lebar.Text)) & ",  "
                SQL = SQL & Val(HilangkanTanda(Txt_Tinggi.Text)) & ", " & Val(HilangkanTanda(Txt_Volume.Text)) & ", '" & Cmb_SatuanVolume.Text.Trim & "', " & arrCaraKirim(Cmb_CaraKirim.SelectedIndex) & ")"
                ExecuteTrans(SQL)

                Action = "Disimpan"

            ElseIf Btn_Simpan.Tag.ToString.ToUpper = "UPDATE" Then

                '
                SQL = "update Master_Kendaraan set Keterangan = '" & Txt_Keterangan.Text & "', Kapasitas = '" & Val(HilangkanTanda(Txt_Berat.Text)) & "', Satuan_Kapasitas = '" & Cmb_SatuanBerat.Text.Trim & "', "
                SQL = SQL & "Panjang = '" & Val(HilangkanTanda(Txt_Panjang.Text)) & "', Lebar = '" & Val(HilangkanTanda(Txt_Lebar.Text)) & "', Tinggi = '" & Val(HilangkanTanda(Txt_Tinggi.Text)) & "', "
                SQL = SQL & "Volume = '" & Val(HilangkanTanda(Txt_Volume.Text)) & "', Satuan_Volume = '" & Cmb_SatuanVolume.Text.Trim & "', id_jenisEkspedisi = '" & arrCaraKirim(Cmb_CaraKirim.SelectedIndex) & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Kendaraan = '" & Id_Kendaraaan.Text & "' and Kode_Kendaraan = '" & Txt_KdKendaraan.Text & "' "
                ExecuteTrans(SQL)

                SQL = "update Master_Kendaraan set Flag_Update = 'Y' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Kendaraan = '" & Id_Kendaraaan.Text & "' and Kode_Kendaraan = '" & Txt_KdKendaraan.Text & "' "
                ExecuteTrans(SQL)

                Action = "Diupdate"

            End If

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show($"Kendaraan Berhasil {Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub



    Private Sub Get_Satuan_Volume()

        Try
            OpenConn()

            Cmb_SatuanVolume.Items.Clear()
            SQL = "select a.Satuan, b.Satuan_Volume  "
            SQL = SQL & "from EMI_Satuan a, EMI_Satuan_Turunan b "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Satuan = b.Satuan "
            SQL = SQL & "and a.Satuan = 'Meter' "
            SQL = SQL & "Order By a.Satuan Desc"

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_SatuanVolume.Items.Add(dr("Satuan_Volume"))
                Loop
                Cmb_SatuanVolume.SelectedItem = "Meter3"
            End Using

            Cmb_SatuanVolume.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Satuan_Berat()
        Try
            OpenConn()
            Cmb_SatuanBerat.Items.Clear()
            SQL = "Select Satuan "
            SQL = SQL & "From EMI_Satuan "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Flag_Tampil_Berat = 'Y' "
            SQL = SQL & "Order By Satuan Desc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_SatuanBerat.Items.Add(dr("Satuan"))
                Loop
                Cmb_SatuanBerat.SelectedItem = "Ton"
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction

                Dim IdKendaraan As String = Lv_DataKendaraan.FocusedItem.Text

                '===========================
                '=     CEK FLAG PINDAH     =
                '===========================
                SQL = "select Flag_Pindah from Master_Kendaraan where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Kendaraan = '" & IdKendaraan & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("Flag_Pindah")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Kendaraan Sudah Tidak Bisa Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Kendaraan Tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                SQL = "delete Master_Kendaraan where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Kendaraan = '" & IdKendaraan & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong()
        Txt_KdKendaraan.Focus()
    End Sub

    Private Sub Txt_Panjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Panjang.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Panjang.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Txt_Lebar.Focus()
    End Sub

    Private Sub Txt_Panjang_Leave(sender As Object, e As EventArgs) Handles Txt_Panjang.Leave
        If Txt_Panjang.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Panjang & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Panjang.Focus()
            Exit Sub
        End If
        Hitung_Volume()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.Text.Trim.Length = 0 Then Exit Sub
        'If Cmb_ValueCaraKirim.Text.Trim.Length = 0 Xor TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            Try
                OpenConn()

                Lv_DataKendaraan.Items.Clear()
                SQL = "select Kode_Perusahaan, Kode_Kendaraan, Id_Kendaraan, Keterangan, Kapasitas, Satuan_Kapasitas, Panjang, Lebar, Tinggi, Volume, Satuan_Volume, id_jenisEkspedisi "
                SQL = SQL & "from Master_Kendaraan "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "
                If semua = "T" Then

                    SQL = SQL & "and " & arrcari.Item(Cmb_Filter.SelectedIndex) & " like '%" & TextBox3.Text & "%' "

                End If
                SQL = SQL & "order by Kode_Kendaraan "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        Dim Lvw As ListViewItem
                        Lvw = Lv_DataKendaraan.Items.Add(dr("Id_Kendaraan"))
                        Lvw.SubItems.Add(dr("Kode_Kendaraan"))
                        Lvw.SubItems.Add(dr("Keterangan"))
                        Lvw.SubItems.Add(Format(dr("Kapasitas"), "N2") & " " & dr("Satuan_Kapasitas"))
                        Lvw.SubItems.Add(Format(dr("Volume"), "N2") & " " & dr("Satuan_Volume"))

                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdMediaKirim_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdKendaraan.TextChanged

    End Sub

    Private Sub Txt_Lebar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Lebar.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Lebar.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Txt_Tinggi.Focus()
    End Sub

    Private Sub Txt_MediaKirim_TextChanged(sender As Object, e As EventArgs) Handles Txt_Keterangan.TextChanged

    End Sub

    Private Sub Txt_Lebar_Leave(sender As Object, e As EventArgs) Handles Txt_Lebar.Leave
        If Txt_Lebar.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Lebar & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Lebar.Focus()
            Exit Sub
        End If
        Hitung_Volume()
    End Sub


    Private Sub Txt_Tinggi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Tinggi.KeyPress, Id_Kendaraaan.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Tinggi.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Txt_Berat.Focus()
    End Sub


    Private Sub Txt_Tinggi_Leave(sender As Object, e As EventArgs) Handles Txt_Tinggi.Leave, Id_Kendaraaan.Leave
        If Txt_Tinggi.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Tinggi & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Tinggi.Focus()
            Exit Sub
        End If
        Hitung_Volume()
    End Sub

    Private Sub Hitung_Volume()
        Dim getVolume As Double = 0
        getVolume = Val(Txt_Panjang.Text) * Val(Txt_Lebar.Text) * Val(Txt_Tinggi.Text)
        Txt_Volume.Text = Format(getVolume, "N2")
    End Sub

    Private Sub Txt_Berat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Berat.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Berat.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub



    Private Sub Lv_MediaKirim_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataKendaraan.DoubleClick
        If Lv_DataKendaraan.Items.Count = 0 Then Exit Sub


        Txt_KdKendaraan.Text = Lv_DataKendaraan.FocusedItem.SubItems(1).Text

        Txt_KdKendaraan_Leave(Lv_DataKendaraan, e)
    End Sub



    Private Sub Txt_KdKendaraan_Leave(sender As Object, e As EventArgs) Handles Txt_KdKendaraan.Leave

        If Txt_KdKendaraan.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "select Kode_Perusahaan, Id_Kendaraan, Kode_Kendaraan, Keterangan, Kapasitas, Satuan_Kapasitas, Panjang, Lebar, Tinggi, Volume, Satuan_Volume, id_jenisEkspedisi "
            SQL = SQL & "from Master_Kendaraan "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and Kode_Kendaraan = '" & Txt_KdKendaraan.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count > 1 Then
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Data Kendaraan Terduplikat", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub


                    ElseIf .Rows.Count = 1 Then

                        Cmb_CaraKirim.SelectedIndex = arrCaraKirim.IndexOf(.Rows(0).Item("id_jenisEkspedisi"))
                        Txt_Keterangan.Text = .Rows(0).Item("Keterangan")
                        Txt_Panjang.Text = .Rows(0).Item("Panjang")
                        Txt_Lebar.Text = .Rows(0).Item("Lebar")
                        Txt_Tinggi.Text = .Rows(0).Item("Tinggi")
                        Txt_Volume.Text = Format(Val(HilangkanTanda(.Rows(0).Item("Volume"))), "N2")
                        Txt_Berat.Text = Format(Val(HilangkanTanda(.Rows(0).Item("Kapasitas"))), "N2")

                        Cmb_SatuanVolume.Text = .Rows(0).Item("Satuan_Volume")
                        Cmb_SatuanBerat.Text = .Rows(0).Item("Satuan_Kapasitas")

                        Id_Kendaraaan.Text = .Rows(0).Item("Id_Kendaraan")

                        Txt_KdKendaraan.Enabled = False
                        Btn_Simpan.Text = "&Update" : Btn_Simpan.Tag = "UPDATE"
                        Btn_Hapus.Enabled = True


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



    Private Sub Txt_Volume_Leave(sender As Object, e As EventArgs) Handles Txt_Volume.Leave
        If Txt_Volume.Text.Trim.Length = 0 Then Exit Sub

        Txt_Volume.Text = Format(Val(HilangkanTanda(Txt_Volume.Text)), "N2")
    End Sub

    Private Sub Txt_Berat_Leave(sender As Object, e As EventArgs) Handles Txt_Berat.Leave
        If Txt_Berat.Text.Trim.Length = 0 Then Exit Sub

        Txt_Berat.Text = Format(Val(HilangkanTanda(Txt_Berat.Text)), "N2")
    End Sub

    Private Sub Txt_Volume_Enter(sender As Object, e As EventArgs) Handles Txt_Volume.Enter
        If Txt_Volume.Text.Trim.Length = 0 Then Exit Sub

        Txt_Volume.Text = Val(HilangkanTanda(Txt_Volume.Text))
    End Sub
    Private Sub Txt_Berat_Enter(sender As Object, e As EventArgs) Handles Txt_Berat.Enter
        If Txt_Berat.Text.Trim.Length = 0 Then Exit Sub

        Txt_Berat.Text = Val(HilangkanTanda(Txt_Berat.Text))
    End Sub



    Private Sub Cmb_CaraKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_CaraKirim.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdKendaraan.Focus()
    End Sub

    Private Sub Txt_KdMediaKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdKendaraan.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Txt_MediaKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Panjang.Focus()
    End Sub

    Private Sub Txt_Volume_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Volume.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_SatuanVolume.Focus()
    End Sub

    Private Sub Cmb_SatuanVolume_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_SatuanVolume.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Berat.Focus()
    End Sub

    Private Sub Cmb_SatuanBerat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_SatuanBerat.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Btn_Simpan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Btn_Simpan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Refresh.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

End Class
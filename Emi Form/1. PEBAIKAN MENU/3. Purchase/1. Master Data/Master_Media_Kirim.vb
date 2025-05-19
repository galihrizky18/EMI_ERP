Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView

Public Class Master_Media_Kirim
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
        LvCaraKirim = ListView_MediaKirim.Items(NoIndex).Text
        LvKdMediaKirim = ListView_MediaKirim.Items(NoIndex).SubItems(itemKdMediaKirim).Text
        LvMediaKirim = ListView_MediaKirim.Items(NoIndex).SubItems(itemMediaKirim).Text
        LvPanjang = ListView_MediaKirim.Items(NoIndex).SubItems(itemPanjang).Text
        LvLebar = ListView_MediaKirim.Items(NoIndex).SubItems(itemLebar).Text
        LvTinggi = ListView_MediaKirim.Items(NoIndex).SubItems(itemTinggi).Text
        LvVolume = ListView_MediaKirim.Items(NoIndex).SubItems(itemVolume).Text
        LvSatuanPanjang = ListView_MediaKirim.Items(NoIndex).SubItems(itemSatuanPanjang).Text
        LvSatuanVolume = ListView_MediaKirim.Items(NoIndex).SubItems(itemSatuanVolume).Text
        LvBerat = ListView_MediaKirim.Items(NoIndex).SubItems(itemBerat).Text
        LvSatuanBerat = ListView_MediaKirim.Items(NoIndex).SubItems(itemSatuanBerat).Text
        LvId = ListView_MediaKirim.Items(NoIndex).SubItems(itemId).Text
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

            Lbl_Judul.Text = Base_Language.Lang_MediaKirim_Judul
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            Lbl_CaraKirim.Text = Base_Language.Lang_Global_Cara_Kirim
            Lbl_KdMediaKirim.Text = Base_Language.Lang_MediaKirim_KodeMediaKirim
            Lbl_MediaKirim.Text = Base_Language.Lang_Global_Media_Kirim
            Lbl_Ukuran.Text = Base_Language.Lang_Global_Ukuran
            Lbl_plt.Text = Base_Language.Lang_Global_HitungVolume
            Lbl_Panjang.Text = Base_Language.Lang_Global_Panjang
            Lbl_Lebar.Text = Base_Language.Lang_Global_Lebar
            Lbl_Tinggi.Text = Base_Language.Lang_Global_Tinggi
            Lbl_SatuanPanjang.Text = "Length Unit"
            Lbl_SatuanVolume.Text = Base_Language.Lang_Global_SatuanVolume
            Lbl_Berat.Text = Base_Language.Lang_Global_Berat
            Lbl_SatuanBerat.Text = Base_Language.Lang_Global_SatuanBerat

            ListView_MediaKirim.Columns.Clear()
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_Global_Cara_Kirim, 150, HorizontalAlignment.Left) '0
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_MediaKirim_KodeMediaKirim, 150, HorizontalAlignment.Left) '1
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_Global_Media_Kirim, 150, HorizontalAlignment.Left) '2
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_Global_Panjang, 80, HorizontalAlignment.Left) '3
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_Global_Lebar, 80, HorizontalAlignment.Left) '4
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_Global_Tinggi, 80, HorizontalAlignment.Left) '5
            ListView_MediaKirim.Columns.Add("Volume", 80, HorizontalAlignment.Left) '6
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_Global_Satuan_Panjang, 100, HorizontalAlignment.Left) '7
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_Global_SatuanVolume, 100, HorizontalAlignment.Left) '8
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_Global_Berat, 80, HorizontalAlignment.Left) '9
            ListView_MediaKirim.Columns.Add(Base_Language.Lang_Global_SatuanBerat, 100, HorizontalAlignment.Left) '10
            ListView_MediaKirim.Columns.Add("LvId", 0, HorizontalAlignment.Left) '11
            ListView_MediaKirim.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub kosong()

        Try
            OpenConn()

            ListView_MediaKirim.Items.Clear()
            SQL = "Select a.Id_Cara_Kirim, a.Keterangan as Cara_Kirim, "
            SQL = SQL & "b.Kode_Media_Kirim, "
            SQL = SQL & "b.Id_Media_Kirim, b.Keterangan as Media_Kirim, "
            SQL = SQL & "b.P, b.L, b.T, b.Volume, b.Satuan_Panjang, "
            SQL = SQL & "b.Satuan_Volume, b.Berat, b.Satuan_Berat "
            SQL = SQL & "From EMI_Cara_Kirim a, EMI_Media_Kirim b "
            SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Id_Cara_Kirim = b.Id_Cara_Kirim "
            SQL = SQL & "Order By Media_Kirim "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView_MediaKirim.Items.Add(dr("Cara_Kirim"))
                    Lvw.SubItems.Add(dr("Kode_Media_Kirim"))
                    Lvw.SubItems.Add(dr("Media_Kirim"))
                    Lvw.SubItems.Add(dr("P"))
                    Lvw.SubItems.Add(dr("L"))
                    Lvw.SubItems.Add(dr("T"))
                    Lvw.SubItems.Add(dr("Volume"))
                    Lvw.SubItems.Add(dr("Satuan_Panjang"))
                    Lvw.SubItems.Add(dr("Satuan_Volume"))
                    Lvw.SubItems.Add(Format(dr("Berat"), "N2"))
                    Lvw.SubItems.Add(dr("Satuan_Berat"))
                    Lvw.SubItems.Add(dr("Id_Media_Kirim"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_Global_Cara_Kirim) : arrcari.Add("a.Keterangan")
        ComboBox1.Items.Add(Base_Language.Lang_MediaKirim_KodeMediaKirim) : arrcari.Add("Kode_Media_Kirim")
        ComboBox1.Items.Add(Base_Language.Lang_Global_Media_Kirim) : arrcari.Add("b.Keterangan")
        TextBox3.Text = ""

        Cmb_CaraKirim.Items.Clear() : Txt_KdMediaKirim.Text = "" : Txt_MediaKirim.Text = ""
        Txt_Panjang.Text = "" : Txt_Lebar.Text = "" : Txt_Tinggi.Text = "" : Txt_Volume.Text = ""
        Cmb_SatuanVolume.Items.Clear() : Txt_Berat.Text = "" : Cmb_SatuanBerat.Items.Clear()

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Simpan.Enabled = True : Btn_Hapus.Enabled = False

        Get_Cara_Kirim()
        Get_Satuan_Panjang()
        'Get_Satuan_Volume()
        Get_Satuan_Berat()
    End Sub

    Private Sub Get_Cara_Kirim()
        Try
            OpenConn()
            Cmb_CaraKirim.Items.Clear() : arrCaraKirim.Clear()
            Cmb_ValueCaraKirim.Items.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Cara_Kirim "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Flag_Tampil = 'Y' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_CaraKirim.Items.Add(dr("Keterangan")) : arrCaraKirim.Add(dr("Id_Cara_Kirim"))
                    Cmb_ValueCaraKirim.Items.Add(dr("Keterangan"))
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
        Txt_KdMediaKirim.Focus()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Dim caraKirim = Cmb_CaraKirim.SelectedItem
        Dim kdMediaKirim = Txt_KdMediaKirim.Text
        Dim mediaKirim = Txt_MediaKirim.Text
        Dim panjang = Txt_Panjang.Text
        Dim lebar = Txt_Lebar.Text
        Dim tinggi = Txt_Tinggi.Text
        Dim volume = Txt_Volume.Text
        Dim satuanPanjang = Cmb_SatuanPanjang.SelectedItem
        Dim satuanVolume = Cmb_SatuanVolume.SelectedItem
        Dim berat = Txt_Berat.Text
        Dim satuanBerat = Cmb_SatuanBerat.SelectedItem

        If Cmb_CaraKirim.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Cara_Kirim & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_CaraKirim.Focus() : Exit Sub
        ElseIf kdMediaKirim.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_MediaKirim_KodeMediaKirim & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_KdMediaKirim.Focus() : Exit Sub
        ElseIf mediaKirim.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Media_Kirim & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_MediaKirim.Focus() : Exit Sub
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
        ElseIf Cmb_SatuanPanjang.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Satuan_Panjang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_SatuanPanjang.Focus() : Exit Sub
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

            If Btn_Simpan.Text = Base_Language.Lang_Global_Simpan Then
                SQL = "Insert Into EMI_Media_Kirim "
                SQL = SQL & "(Kode_Perusahaan, Id_Cara_Kirim, "
                SQL = SQL & "Kode_Media_Kirim, Keterangan, "
                SQL = SQL & "P, L, T, Volume, "
                SQL = SQL & "Satuan_Panjang, Satuan_Volume, "
                SQL = SQL & "Berat, Satuan_Berat) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', "
                SQL = SQL & "'" & arrCaraKirim.Item(Cmb_CaraKirim.SelectedIndex) & "', "
                SQL = SQL & "'" & kdMediaKirim.Trim & "', '" & mediaKirim.Trim & "', "
                SQL = SQL & "'" & panjang.Trim & "', '" & lebar.Trim & "', '" & tinggi.Trim & "', "
                SQL = SQL & "'" & volume.Trim & "', '" & satuanPanjang.Trim.ToString & "', "
                SQL = SQL & "'" & satuanVolume.Trim.ToString & "', "
                SQL = SQL & "'" & berat.Trim & "', '" & satuanBerat.Trim.ToString & "') "
                ExecuteTrans(SQL)
            Else
                SQL = "Update EMI_Media_Kirim "
                'SQL = SQL & "Set Id_Cara_Kirim = '" & caraKirim.Trim.ToString & "', "
                SQL = SQL & "Set Id_Cara_Kirim = '" & arrCaraKirim.Item(Cmb_CaraKirim.SelectedIndex) & "', "
                SQL = SQL & "Kode_Media_Kirim = '" & kdMediaKirim.Trim & "', "
                SQL = SQL & "Keterangan = '" & mediaKirim.Trim & "', "
                SQL = SQL & "P = '" & panjang.Trim & "', "
                SQL = SQL & "L = '" & lebar.Trim & "', "
                SQL = SQL & "T = '" & tinggi.Trim & "', "
                SQL = SQL & "Volume = '" & volume.Trim & "', "
                SQL = SQL & "Satuan_Panjang = '" & satuanPanjang.Trim.ToString & "', "
                SQL = SQL & "Satuan_Volume = '" & satuanVolume.Trim.ToString & "', "
                SQL = SQL & "Berat = '" & berat.Trim & "', "
                SQL = SQL & "Satuan_Berat = '" & satuanBerat.Trim.ToString & "' "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Id_Media_Kirim = '" & LvId & "'"
                ExecuteTrans(SQL)
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
    End Sub

    Private Sub Get_Satuan_Panjang()
        Try
            OpenConn()
            Cmb_SatuanPanjang.Items.Clear()
            SQL = "Select Satuan "
            SQL = SQL & "From EMI_Satuan "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Flag_Tampil_panjang = 'Y' "
            SQL = SQL & "Order By Satuan Desc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_SatuanPanjang.Items.Add(dr("Satuan"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
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
            SQL = SQL & "and a.Satuan = '" & Cmb_SatuanPanjang.SelectedItem & "' "
            SQL = SQL & "Order By a.Satuan Desc"

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_SatuanVolume.Items.Add(dr("Satuan_Volume"))
                Loop
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
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Get_Isi_ListView(ListView_MediaKirim.FocusedItem.Index)

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction

                SQL = "DELETE FROM EMI_Media_Kirim "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Id_Media_Kirim = '" & LvId & "' "
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
        Txt_KdMediaKirim.Focus()
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
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        'If Cmb_ValueCaraKirim.Text.Trim.Length = 0 Xor TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            Try
                OpenConn()

                ListView_MediaKirim.Items.Clear()
                SQL = "Select a.Id_Cara_Kirim, a.Keterangan as Cara_Kirim, "
                SQL = SQL & "b.Kode_Media_Kirim, "
                SQL = SQL & "b.Id_Media_Kirim, b.Keterangan as Media_Kirim, "
                SQL = SQL & "b.P, b.L, b.T, b.Volume, "
                SQL = SQL & "b.Satuan_Volume, b.Berat, b.Satuan_Berat "
                SQL = SQL & "From EMI_Cara_Kirim a, EMI_Media_Kirim b "
                SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan and "
                SQL = SQL & "a.Id_Cara_Kirim = b.Id_Cara_Kirim "

                If semua = "T" Then
                    If ComboBox1.SelectedIndex = 0 Then
                        SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & Cmb_ValueCaraKirim.SelectedItem.Trim.ToString & "%' "
                    Else
                        SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                    End If
                    SQL = SQL & "Order By " & arrcari.Item(ComboBox1.SelectedIndex) & " "
                Else
                    SQL = SQL & "Order By Media_Kirim "
                End If

                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        Dim Lvw As ListViewItem
                        Lvw = ListView_MediaKirim.Items.Add(dr("Cara_Kirim"))
                        Lvw.SubItems.Add(dr("Kode_Media_Kirim"))
                        Lvw.SubItems.Add(dr("Media_Kirim"))
                        Lvw.SubItems.Add(dr("P"))
                        Lvw.SubItems.Add(dr("L"))
                        Lvw.SubItems.Add(dr("T"))
                        Lvw.SubItems.Add(dr("Volume"))
                        Lvw.SubItems.Add(dr("Satuan_Volume"))
                        Lvw.SubItems.Add(Format(dr("Berat"), "N2"))
                        Lvw.SubItems.Add(dr("Satuan_Berat"))
                        Lvw.SubItems.Add(dr("Id_Media_Kirim"))
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

    Private Sub Txt_KdMediaKirim_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdMediaKirim.TextChanged

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

    Private Sub Txt_MediaKirim_TextChanged(sender As Object, e As EventArgs) Handles Txt_MediaKirim.TextChanged

    End Sub

    Private Sub Txt_Lebar_Leave(sender As Object, e As EventArgs) Handles Txt_Lebar.Leave
        If Txt_Lebar.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Lebar & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Lebar.Focus()
            Exit Sub
        End If
        Hitung_Volume()
    End Sub

    Private Sub Txt_Panjang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Panjang.TextChanged

    End Sub

    Private Sub Txt_Lebar_TextChanged(sender As Object, e As EventArgs) Handles Txt_Lebar.TextChanged

    End Sub

    Private Sub Txt_Tinggi_TextChanged(sender As Object, e As EventArgs) Handles Txt_Tinggi.TextChanged

    End Sub

    Private Sub Txt_Tinggi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Tinggi.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Tinggi.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Txt_Volume.Focus()
    End Sub

    Private Sub Cmb_SatuanVolume_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_SatuanVolume.SelectedIndexChanged

    End Sub

    Private Sub Txt_Tinggi_Leave(sender As Object, e As EventArgs) Handles Txt_Tinggi.Leave
        If Txt_Tinggi.Text.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Tinggi & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Tinggi.Focus()
            Exit Sub
        End If
        Hitung_Volume()
    End Sub

    Private Sub Txt_Berat_TextChanged(sender As Object, e As EventArgs) Handles Txt_Berat.TextChanged

    End Sub

    Private Sub Cmb_SatuanBerat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_SatuanBerat.SelectedIndexChanged

    End Sub

    Private Sub Hitung_Volume()
        Dim getVolume As Double = 0
        getVolume = Val(Txt_Panjang.Text) * Val(Txt_Lebar.Text) * Val(Txt_Tinggi.Text)
        Txt_Volume.Text = getVolume
    End Sub

    Private Sub Txt_Berat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Berat.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = "." AndAlso Txt_Berat.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then Cmb_SatuanBerat.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            TextBox3.Visible = False
            Cmb_ValueCaraKirim.Visible = True
            'Get_Cara_Kirim()
        Else
            TextBox3.Visible = True
            Cmb_ValueCaraKirim.Visible = False
        End If
        TextBox3.Text = ""
        Cmb_ValueCaraKirim.SelectedIndex = -1
    End Sub

    Private Sub Lv_MediaKirim_DoubleClick(sender As Object, e As EventArgs) Handles ListView_MediaKirim.DoubleClick
        If ListView_MediaKirim.Items.Count = 0 Then Exit Sub

        Cmb_CaraKirim.SelectedItem = ListView_MediaKirim.FocusedItem.Text
        Txt_KdMediaKirim.Text = ListView_MediaKirim.FocusedItem.SubItems(itemKdMediaKirim).Text
        Txt_MediaKirim.Text = ListView_MediaKirim.FocusedItem.SubItems(itemMediaKirim).Text
        Txt_Panjang.Text = ListView_MediaKirim.FocusedItem.SubItems(itemPanjang).Text
        Txt_Lebar.Text = ListView_MediaKirim.FocusedItem.SubItems(itemLebar).Text
        Txt_Tinggi.Text = ListView_MediaKirim.FocusedItem.SubItems(itemTinggi).Text
        Txt_Volume.Text = ListView_MediaKirim.FocusedItem.SubItems(itemVolume).Text
        Cmb_SatuanPanjang.SelectedItem = ListView_MediaKirim.FocusedItem.SubItems(itemSatuanPanjang).Text
        Cmb_SatuanVolume.SelectedItem = ListView_MediaKirim.FocusedItem.SubItems(itemSatuanVolume).Text
        Txt_Berat.Text = HilangkanTanda(ListView_MediaKirim.FocusedItem.SubItems(itemBerat).Text)
        Cmb_SatuanBerat.SelectedItem = ListView_MediaKirim.FocusedItem.SubItems(itemSatuanBerat).Text

        Cmb_CaraKirim_Leave(ListView_MediaKirim, e)
    End Sub

    Private Sub Cmb_CaraKirim_Leave(sender As Object, e As EventArgs) Handles Cmb_CaraKirim.Leave
        Dim caraKirim = Cmb_CaraKirim.SelectedItem
        Dim kdMediaKirim = Txt_KdMediaKirim.Text
        Dim mediaKirim = Txt_MediaKirim.Text
        Dim panjang = Txt_Panjang.Text
        Dim lebar = Txt_Lebar.Text
        Dim tinggi = Txt_Tinggi.Text
        Dim volume = Txt_Volume.Text
        Dim satuanVolume = Cmb_SatuanVolume.SelectedItem
        Dim berat = Txt_Berat.Text
        Dim satuanBerat = Cmb_SatuanBerat.SelectedItem

        'If caraKirim.Length = 0 Then Exit Sub
        If kdMediaKirim.Trim.Length = 0 Then Exit Sub
        If mediaKirim.Trim.Length = 0 Then Exit Sub
        If panjang.Trim.Length = 0 Then Exit Sub
        If lebar.Trim.Length = 0 Then Exit Sub
        If tinggi.Trim.Length = 0 Then Exit Sub
        If volume.Trim.Length = 0 Then Exit Sub
        If satuanVolume.Length = 0 Then Exit Sub
        If berat.Trim.Length = 0 Then Exit Sub
        If satuanBerat.Length = 0 Then Exit Sub

        Try
            OpenConn()
            Get_Isi_ListView(ListView_MediaKirim.FocusedItem.Index)
            SQL = "Select a.Keterangan as Cara_Kirim, "
            SQL = SQL & "b.Kode_Media_Kirim, "
            SQL = SQL & "b.Keterangan as Media_Kirim, "
            SQL = SQL & "b.P, b.L, b.T, b.Volume, "
            SQL = SQL & "b.Satuan_Volume, b.Berat, b.Satuan_Berat "
            SQL = SQL & "From EMI_Cara_Kirim a, EMI_Media_Kirim b "
            SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Id_Cara_Kirim = b.Id_Cara_Kirim and "
            SQL = SQL & "Id_Media_Kirim = '" & LvId & "' "
            SQL = SQL & "Order By Media_Kirim "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    caraKirim = Dr("Cara_Kirim")
                    kdMediaKirim = Dr("Kode_Media_Kirim")
                    mediaKirim = Dr("Media_Kirim")
                    panjang = Dr("P")
                    lebar = Dr("L")
                    tinggi = Dr("T")
                    volume = Dr("Volume")
                    satuanVolume = Dr("Satuan_Volume")
                    berat = Dr("Berat")
                    satuanBerat = Dr("Satuan_Berat")
                    Btn_Simpan.Text = Base_Language.Lang_Global_Update
                    Btn_Hapus.Enabled = True
                Else
                    Cmb_CaraKirim.SelectedIndex = -1
                    kdMediaKirim = ""
                    mediaKirim = ""
                    panjang = ""
                    lebar = ""
                    tinggi = ""
                    volume = ""
                    Cmb_SatuanVolume.SelectedIndex = -1
                    berat = ""
                    Cmb_SatuanBerat.SelectedIndex = -1
                    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = True
                End If
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cmb_SatuanPanjang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_SatuanPanjang.SelectedIndexChanged
        If Cmb_SatuanPanjang.SelectedIndex = -1 Then Exit Sub
        Get_Satuan_Volume()
    End Sub

    Private Sub Cmb_CaraKirim_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_CaraKirim.SelectedIndexChanged

    End Sub

    Private Sub ListView_MediaKirim_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_MediaKirim.SelectedIndexChanged

    End Sub

    Private Sub Cmb_CaraKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_CaraKirim.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdMediaKirim.Focus()
    End Sub

    Private Sub Txt_KdMediaKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdMediaKirim.KeyPress
        If e.KeyChar = Chr(13) Then Txt_MediaKirim.Focus()
    End Sub

    Private Sub Txt_MediaKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_MediaKirim.KeyPress
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

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

End Class
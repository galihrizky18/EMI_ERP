Public Class N_EMI_SD_Master_Kendaraan_DO


    Private ReadOnly BodyAlignments As New Dictionary(Of Integer, StringAlignment)

    Dim Lv_Cell_Kosong, Lv_Plat_Kendaraan, Lv_Kapasitas_Muatan, Lv_Jenis_Kendaraan, Lv_STNK_Sendiri As String

    Dim Item_Cell_Kosong As Integer = 0
    Dim Item_Plat_Kendaraan As Integer = 1
    Dim Item_Kapasitas_Muatan As Integer = 2
    Dim Item_Jenis_Kendaraan As Integer = 3
    Dim Item_STNK_Sendiri As Integer = 4



    Private Sub N_EMI_SD_Master_Kendaraan_DO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Display_Kendaraan.Columns.Clear() : BodyAlignments.Clear()
        Lv_Display_Kendaraan.Columns.Add("", 0) : BodyAlignments(0) = StringAlignment.Center
        Lv_Display_Kendaraan.Columns.Add("Plat Kendaraan", 150) : BodyAlignments(1) = StringAlignment.Center
        Lv_Display_Kendaraan.Columns.Add("Kapasitas Muatan", 136) : BodyAlignments(2) = StringAlignment.Far
        Lv_Display_Kendaraan.Columns.Add("Jenis Kendaraan", 150) : BodyAlignments(3) = StringAlignment.Center
        Lv_Display_Kendaraan.Columns.Add("STNK Sendiri", 150) : BodyAlignments(4) = StringAlignment.Center
        Lv_Display_Kendaraan.View = View.Details



        Cmb_Jenis.Items.Clear()
        Cmb_Jenis.Items.Add("Sendiri")
        Cmb_Jenis.Items.Add("Ekspedisi")

        Cmb_Jenis_Kepemilikan_STNK.Items.Clear()
        Cmb_Jenis_Kepemilikan_STNK.Items.Add("Y")
        Cmb_Jenis_Kepemilikan_STNK.Items.Add("T")

        Kosong()


        ' SET AGAR TEXTBOX LANGSUNG BERUBAH MENJADI UPPER
        Txt_Plat_Kendaraan.CharacterCasing = CharacterCasing.Upper

    End Sub

    Private Sub Kosong()

        Txt_Plat_Kendaraan.Text = ""
        Txt_Kapasitas_Muatan.Text = ""

        Cmb_Jenis.SelectedIndex = -1
        Cmb_Jenis_Kepemilikan_STNK.SelectedIndex = -1

        Txt_Plat_Kendaraan.Enabled = True

        Btn_Simpan.Tag = "SIMPAN"
        Btn_Simpan.Text = "&Simpan"

        Load_Data_Lv()
    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)
        Lv_Cell_Kosong = Lv_Display_Kendaraan.Items(index).SubItems(Item_Cell_Kosong).Text
        Lv_Plat_Kendaraan = Lv_Display_Kendaraan.Items(index).SubItems(Item_Plat_Kendaraan).Text
        Lv_Kapasitas_Muatan = Lv_Display_Kendaraan.Items(index).SubItems(Item_Kapasitas_Muatan).Text
        Lv_Jenis_Kendaraan = Lv_Display_Kendaraan.Items(index).SubItems(Item_Jenis_Kendaraan).Text
        Lv_STNK_Sendiri = Lv_Display_Kendaraan.Items(index).SubItems(Item_STNK_Sendiri).Text
    End Sub

    Private Sub Load_Data_Lv()
        Try
            OpenConn()

            Lv_Display_Kendaraan.Items.Clear()
            SQL = "select kode_mobil, kapasitas, jenis, flag_stnk_sendiri "
            SQL &= $"from kendaraan "
            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
            SQL &= $"order by kode_mobil "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Display_Kendaraan.Items.Add("")
                    Lv.SubItems.Add(Dr("kode_mobil"))
                    Lv.SubItems.Add(Format(Dr("kapasitas"), "N4"))
                    Lv.SubItems.Add(Dr("jenis"))
                    Lv.SubItems.Add(Dr("flag_stnk_sendiri"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Plat_Kendaraan_Leave(sender As Object, e As EventArgs) Handles Txt_Plat_Kendaraan.Leave
        If Txt_Plat_Kendaraan.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "select kode_mobil, kapasitas, jenis, flag_stnk_sendiri "
            SQL &= $"from kendaraan "
            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Kode_Mobil = '{Txt_Plat_Kendaraan.Text.Trim}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_Kapasitas_Muatan.Text = Format(Dr("kapasitas"), "N4")
                    Cmb_Jenis.SelectedItem = Dr("jenis")
                    Cmb_Jenis_Kepemilikan_STNK.SelectedItem = Dr("flag_stnk_sendiri")

                    Txt_Plat_Kendaraan.Enabled = False
                    Btn_Simpan.Text = "&Update"
                    Btn_Simpan.Tag = "UPDATE"
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Plat_Kendaraan.Text.Trim.Length = 0 Then
            MessageBox.Show("Plat Kendaraan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Plat_Kendaraan.Focus()
            Exit Sub
        ElseIf Txt_Kapasitas_Muatan.Text.Trim.Length = 0 Then
            MessageBox.Show("Kapasitas Muatan Kendaraan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kapasitas_Muatan.Focus()
            Exit Sub
        ElseIf Cmb_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("jenis Kendaraan Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis.DroppedDown = True
            Cmb_Jenis.Focus()
            Exit Sub
        ElseIf Cmb_Jenis_Kepemilikan_STNK.SelectedIndex = -1 Then
            MessageBox.Show("Jenis STNK Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis_Kepemilikan_STNK.DroppedDown = True
            Cmb_Jenis_Kepemilikan_STNK.Focus()
            Exit Sub
        End If

        Dim Action As String = ""
        If Btn_Simpan.Tag = "SIMPAN" Then
            Action = "Simpan"
        ElseIf Btn_Simpan.Tag = "UPDATE" Then
            Action = "Update"
        End If

        If MessageBox.Show($"Yakin ingin melakukan {Action} data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag = "SIMPAN" Then
                SQL = "select kode_mobil "
                SQL &= $"from kendaraan "
                SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
                SQL &= $"and Kode_Mobil = '{Txt_Plat_Kendaraan.Text.Trim}' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Terjadi Kesalahan, {vbCrLf}Data Plat Kendaraan {Txt_Plat_Kendaraan.Text.Trim} Sudah Ada.
                                        {vbCrLf & vbCrLf}Harap Input Plat Kendaraan Berbeda agar Tidak Menyebabkan Data Menjadi Duplikat",
                                        Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else

                        Dr.Close()
                        SQL = "insert into kendaraan (Kode_Perusahaan, Kode_Mobil, Kapasitas, jenis, Flag_STNK_Sendiri) "
                        SQL &= $"values ('{KodePerusahaan}', '{Txt_Plat_Kendaraan.Text.Trim}', '{Val(HilangkanTanda(Txt_Kapasitas_Muatan.Text))}', "
                        SQL &= $"'{Cmb_Jenis.Text.Trim}', '{Cmb_Jenis_Kepemilikan_STNK.Text.Trim}') "
                        ExecuteTrans(SQL)

                    End If
                End Using

            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                SQL = "select kode_mobil "
                SQL &= $"from kendaraan "
                SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
                SQL &= $"and Kode_Mobil = '{Txt_Plat_Kendaraan.Text.Trim}' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Terjadi Kesalahan, {vbCrLf}Data Plat Kendaraan {Txt_Plat_Kendaraan.Text.Trim} Tidak Ditemukan.
                                        {vbCrLf & vbCrLf}Harap Pastikan Plat Kendaraan Sudah Benar",
                                        Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else

                        Dr.Close()
                        SQL = $"update kendaraan set Kapasitas = '{Val(HilangkanTanda(Txt_Kapasitas_Muatan.Text))}', "
                        SQL &= $"Jenis = '{Cmb_Jenis.Text.Trim}', Flag_STNK_Sendiri = '{Cmb_Jenis_Kepemilikan_STNK.Text.Trim}' "
                        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"and Kode_Mobil = '{Txt_Plat_Kendaraan.Text.Trim}' "
                        ExecuteTrans(SQL)

                    End If
                End Using


            Else
                CloseTrans()
                CloseConn()
                MessageBox.Show($"Action pada button tidak diketahui, harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show($"Data Berhasil Di{Action}", "Tambah Kendaraan", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        'MessageBox.Show("Fitur Dinonaktifkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'Exit Sub

        If Txt_Plat_Kendaraan.Text.Trim.Length = 0 Then
            MessageBox.Show("No Plat Kendaraan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Plat_Kendaraan.Focus()
            Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            '=====================================================
            '=     CEK APAKAH KODE KENDARAAN SUDAH DIGUNAKAN     =
            '=====================================================
            SQL = "select Kode_Perusahaan from do_new_sementara "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Kode_Kendaraan = '{Txt_Plat_Kendaraan.Text.Trim}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Proses Tidak Dapat Dilanjutkan Karena Kode Kendaraan {Txt_Plat_Kendaraan.Text.Trim} Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Kode_Kendaraan from do_new "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and status is null "
            SQL &= $"and Kode_Kendaraan = '{Txt_Plat_Kendaraan.Text.Trim}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Prose Tidak Dapat Dilanjutkan Karena Kode Kendaraan {Txt_Plat_Kendaraan.Text.Trim} Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================
            '=     CEK NO FAKTUR     =
            '=========================
            SQL = "select Kode_Perusahaan "
            SQL &= $"from kendaraan "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Kode_Mobil = '{Txt_Plat_Kendaraan.Text.Trim}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = $"delete kendaraan where Kode_Perusahaan = '{KodePerusahaan}' and Kode_Mobil = '{Txt_Plat_Kendaraan.Text.Trim}' "
                    ExecuteTrans(SQL)

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data Kode Mobil {Txt_Plat_Kendaraan.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Cmd.Transaction.Commit()
            CloseConn()

            MessageBox.Show($"Kode Kendaraan {Txt_Plat_Kendaraan.Text.Trim} Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Exit_Click(sender As Object, e As EventArgs) Handles Btn_Exit.Click

        Try
            OpenConn()

            DO_Reseller_New.CmbMbl.Items.Clear()
            SQL = "select kode_mobil from kendaraan where kode_perusahaan = '" & KodePerusahaan & "' order by kode_mobil"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    DO_Reseller_New.CmbMbl.Items.Add(Dr("kode_mobil"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Me.Close()
    End Sub

    Private Sub N_EMI_SD_Master_Kendaraan_DO_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            OpenConn()

            DO_Reseller_New.CmbMbl.Items.Clear()
            SQL = "select kode_mobil from kendaraan where kode_perusahaan = '" & KodePerusahaan & "' order by kode_mobil"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    DO_Reseller_New.CmbMbl.Items.Add(Dr("kode_mobil"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub






    Private Sub Lv_Display_Kendaraan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Display_Kendaraan.DoubleClick
        If Lv_Display_Kendaraan.Items.Count = 0 Or Lv_Display_Kendaraan.FocusedItem Is Nothing Then Exit Sub

        Get_Data_Lv(Lv_Display_Kendaraan.FocusedItem.Index)

        Txt_Plat_Kendaraan.Text = Lv_Plat_Kendaraan
        Txt_Plat_Kendaraan_Leave(Txt_Plat_Kendaraan, EventArgs.Empty)
    End Sub

    Private Sub Lv_Display_Kendaraan_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Display_Kendaraan.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Display_Kendaraan_DoubleClick(Lv_Display_Kendaraan, e)
        End If
    End Sub

    '=======================================================================================================================================================
    '=     HANDLE KEYPRESS
    '=======================================================================================================================================================
    Private Sub Txt_Plat_Kendaraan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Plat_Kendaraan.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Kapasitas_Muatan.Focus()
        If Not (e.KeyChar <> Chr(Asc("-")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub
    Private Sub Cmb_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Jenis.SelectedIndex <> -1 Then
                Cmb_Jenis_Kepemilikan_STNK.DroppedDown = True
                Cmb_Jenis_Kepemilikan_STNK.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Jenis_Kepemilikan_STNK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Kepemilikan_STNK.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Jenis_Kepemilikan_STNK.SelectedIndex <> -1 Then
                Btn_Simpan.Focus()
            End If
        End If
    End Sub



    '=======================================================================================================================================================
    '=     UTILITY
    '=======================================================================================================================================================

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &HA3 Then
            Return
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub Lv_Display_Kendaraan_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Display_Kendaraan.DrawColumnHeader

        ' Background gradient
        Using bgBrush As New Drawing2D.LinearGradientBrush(
            e.Bounds,
            Color.FromArgb(245, 245, 245),
            Color.FromArgb(220, 220, 220),
            Drawing2D.LinearGradientMode.Vertical)

            e.Graphics.FillRectangle(bgBrush, e.Bounds)
        End Using

        ' Border bawah (lebih modern dari full border)
        Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
            e.Graphics.DrawLine(
                borderPen,
                e.Bounds.Left,
                e.Bounds.Bottom - 1,
                e.Bounds.Right,
                e.Bounds.Bottom - 1)
        End Using

        ' Teks header
        Using sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            sf.Trimming = StringTrimming.EllipsisCharacter

            ' Padding teks
            Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

            e.Graphics.DrawString(
                e.Header.Text,
                Lv_Display_Kendaraan.Font,
                Brushes.Black,
                textRect,
                sf)
        End Using

    End Sub



    Private Sub Lv_Display_Kendaraan_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Display_Kendaraan.DrawSubItem

        Using sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Center

            If BodyAlignments.ContainsKey(e.ColumnIndex) Then
                sf.Alignment = BodyAlignments(e.ColumnIndex)
            Else
                sf.Alignment = StringAlignment.Near ' default
            End If

            If e.Item.Selected Then
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds)
                e.Graphics.DrawString(e.SubItem.Text, Lv_Display_Kendaraan.Font, SystemBrushes.HighlightText, e.Bounds, sf)
            Else
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds)
                e.Graphics.DrawString(e.SubItem.Text, Lv_Display_Kendaraan.Font, Brushes.Black, e.Bounds, sf)
            End If
        End Using

    End Sub


    Private Sub Txt_Kapasitas_Muatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kapasitas_Muatan.KeyPress

        Dim txt As TextBox = DirectCast(sender, TextBox)

        If e.KeyChar = ChrW(Keys.Enter) Then
            Cmb_Jenis.DroppedDown = True
            Cmb_Jenis.Focus()
            e.Handled = True
            Return
        End If

        If e.KeyChar = ChrW(Keys.Back) Then
            Return
        End If

        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        If e.KeyChar = "."c OrElse e.KeyChar = ","c Then

            If txt.SelectionStart = 0 AndAlso txt.Text.Length = 0 Then
                e.Handled = True
                Return
            End If

            If txt.Text.Contains(".") OrElse txt.Text.Contains(",") Then
                e.Handled = True
                Return
            End If

            Return
        End If

        e.Handled = True

    End Sub

    Private Sub Lv_Display_Kendaraan_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Display_Kendaraan.MouseMove

        ' Handle agar mouser berubah menjadi hand ketika mengarahkan kedata
        Dim info As ListViewHitTestInfo = Lv_Display_Kendaraan.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_Display_Kendaraan.Cursor = Cursors.Hand
        Else
            Lv_Display_Kendaraan.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Lv_Display_Kendaraan_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Display_Kendaraan.MouseLeave
        Lv_Display_Kendaraan.Cursor = Cursors.Default
    End Sub





End Class
Public Class N_EMI_Transaksi_Konfigurasi_Harga_Jual

    Dim Dgv_Kd_Barang, Dgv_Nm_Barang, Dgv_Harga_awal, Dgv_HPP_AVG, Dgv_Penyesuaian_Persen, Dgv_Harga_Dasar, Dgv_Selisih_HPP, Dgv_Margin_Persen, Dgv_Keuntungan, Dgv_Harga_Jual As String

    Dim Cell_Kd_Barang As Integer = 0
    Dim Cell_Nm_Barang As Integer = 1
    Dim Cell_Harga_Awal As Integer = 2
    Dim Cell_HPP_AVG As Integer = 3
    Dim Cell_Penyesuaian_Persen As Integer = 4
    Dim Cell_Harga_Dasar As Integer = 5
    Dim Cell_Selisih_HPP As Integer = 6
    Dim Cell_Margin_Persen As Integer = 7
    Dim Cell_Keuntungan As Integer = 8
    Dim Cell_Harga_Jual As Integer = 9

    Dim Switch_AutoComplete As Boolean = False

    Dim dataBackup As New List(Of (Harga_Awal As Double, Penyesuaian As Double, Margin As Double))

    Private Sub N_EMI_Transaksi_Konfigurasi_Harga_Jual_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 330, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details

        Kosong()

    End Sub

    Private Sub get_no_faktur()
        Dim FTransaksi As String = "KHJ"
        Txt_NoTransaksi.Text = FTransaksi & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Transaksi_Konfigurasi_Harga_Jual", "No_Transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Transaksi, 1, " & Len(FTransaksi) + 4 & ")", FTransaksi & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Kosong()

        Switch_AutoComplete = False
        Txt_Kd_Barang.Text = OpsiSeluruh
        Txt_Nm_Barang.Text = OpsiSeluruh
        Switch_AutoComplete = True

        get_jam()

        Try
            OpenConn()

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        dataBackup.Clear()
        Get_Data()
    End Sub

    Private Sub Get_Data_DGV(ByVal index As Integer)
        Dgv_Kd_Barang = Dgv_Data.Rows(index).Cells(Cell_Kd_Barang).Value
        Dgv_Nm_Barang = Dgv_Data.Rows(index).Cells(Cell_Nm_Barang).Value
        Dgv_Harga_awal = Dgv_Data.Rows(index).Cells(Cell_Harga_Awal).Value
        Dgv_HPP_AVG = Dgv_Data.Rows(index).Cells(Cell_HPP_AVG).Value
        Dgv_Penyesuaian_Persen = Dgv_Data.Rows(index).Cells(Cell_Penyesuaian_Persen).Value
        Dgv_Harga_Dasar = Dgv_Data.Rows(index).Cells(Cell_Harga_Dasar).Value
        Dgv_Selisih_HPP = Dgv_Data.Rows(index).Cells(Cell_Selisih_HPP).Value
        Dgv_Margin_Persen = Dgv_Data.Rows(index).Cells(Cell_Margin_Persen).Value
        Dgv_Keuntungan = Dgv_Data.Rows(index).Cells(Cell_Keuntungan).Value
        Dgv_Harga_Jual = Dgv_Data.Rows(index).Cells(Cell_Harga_Jual).Value
    End Sub

    Private Sub Get_Data()
        Try
            OpenConn()

            Dim Persentase_Batas As Double = 0

            SQL = "select Persen_toleransi_Harga_Jual from init where kode_Perusahaan ='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Persentase_Batas = dr("Persen_toleransi_Harga_Jual")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            LabelPersen.Text = "Persen Toleransi : " & Persentase_Batas & " %"

            Dgv_Data.Rows.Clear() : dataBackup.Clear()
            SQL = "select kode_barang, Nama_Barang, Harga_Awal, HPP_AVG, Persentase_Penyesuaian, Persentase_Margin "
            SQL = SQL & "from N_EMI_View_Transaksi_Konfigurasi_Harga_Jual "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            If Not Txt_Kd_Barang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and kode_barang = '" & Txt_Kd_Barang.Text & "' "
            End If
            SQL = SQL & "order by kode_barang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Harga_Awal As Double = Math.Round(.Rows(i).Item("Harga_Awal"), 0)
                            Dim Hpp_AVG As Double = Math.Round(.Rows(i).Item("HPP_AVG"), 0)
                            Dim Persen_Penentu As Double = HilangkanTanda(.Rows(i).Item("Persentase_Penyesuaian"))
                            Dim Persen_Margin As Double = HilangkanTanda(.Rows(i).Item("Persentase_Margin"))

                            Dim Harga_Dasar As Double = Harga_Awal + Math.Round((Harga_Awal * (Persen_Penentu / 100)), 0)

                            Dim Selisih As Double = Harga_Dasar - Hpp_AVG

                            Dim Batas As Double = Math.Round((Harga_Dasar * (Persentase_Batas / 100)), 0)


                            Dim Keuntungan As Double = Math.Round(Harga_Dasar * (Persen_Margin / 100), 0)
                            Dim Harga_Jual As Double = Harga_Dasar + Keuntungan



                            Dgv_Data.Rows.Add(1)
                            Dgv_Data.Rows(i).Cells(Cell_Kd_Barang).Value = .Rows(i).Item("kode_barang")
                            Dgv_Data.Rows(i).Cells(Cell_Nm_Barang).Value = .Rows(i).Item("Nama_Barang")
                            Dgv_Data.Rows(i).Cells(Cell_Harga_Awal).Value = Format(Harga_Awal, "N0")
                            Dgv_Data.Rows(i).Cells(Cell_HPP_AVG).Value = Format(Hpp_AVG, "N0")
                            Dgv_Data.Rows(i).Cells(Cell_Penyesuaian_Persen).Value = Format(Persen_Penentu, "N4")
                            Dgv_Data.Rows(i).Cells(Cell_Harga_Dasar).Value = Format(Harga_Dasar, "N0")
                            Dgv_Data.Rows(i).Cells(Cell_Selisih_HPP).Value = Format(Selisih, "N0")
                            Dgv_Data.Rows(i).Cells(Cell_Margin_Persen).Value = Format(Persen_Margin, "N4")
                            Dgv_Data.Rows(i).Cells(Cell_Keuntungan).Value = Format(Keuntungan, "N0")
                            Dgv_Data.Rows(i).Cells(Cell_Harga_Jual).Value = Format(Harga_Jual, "N0")

                            dataBackup.Add((Harga_Awal, Persen_Penentu, Persen_Margin))


                            If Selisih < Batas Then
                                Dgv_Data.Rows(i).DefaultCellStyle.BackColor = Color.LightYellow

                                Dgv_Data.Rows(i).Cells(Cell_Harga_Awal).Style.BackColor = Color.FromArgb(255, 255, 143)
                                Dgv_Data.Rows(i).Cells(Cell_Penyesuaian_Persen).Style.BackColor = Color.FromArgb(255, 255, 143)
                                Dgv_Data.Rows(i).Cells(Cell_Margin_Persen).Style.BackColor = Color.FromArgb(255, 255, 143)

                            Else
                                Dgv_Data.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                                Dgv_Data.Rows(i).Cells(Cell_Harga_Awal).Style.BackColor = Color.FromArgb(80, 200, 120)
                                Dgv_Data.Rows(i).Cells(Cell_Penyesuaian_Persen).Style.BackColor = Color.FromArgb(80, 200, 120)
                                Dgv_Data.Rows(i).Cells(Cell_Margin_Persen).Style.BackColor = Color.FromArgb(80, 200, 120)
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

    Private Sub Txt_Kd_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.TextChanged
        If Switch_AutoComplete = False Then Exit Sub
        If Txt_Kd_Barang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 125)
            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(100, 125)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, EMI_Group_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and b.Flag_Finished_Good = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Barang like '%" & Txt_Kd_Barang.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Barang_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.Leave
        If Txt_Kd_Barang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Barang.Text = OpsiSeluruh Then

                SQL = "select Distinct a.Kode_Barang, a.Nama "
                SQL = SQL & "from barang a, EMI_Group_Jenis b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "and b.Flag_Finished_Good = 'Y' "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_Kd_Barang.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kd_Barang.Text = Dr("Kode_Barang")
                        Txt_Nm_Barang.Text = Dr("Nama")
                        Btn_Cari.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kd_Barang.Text = ""
                        Txt_Nm_Barang.Text = ""
                        Txt_Kd_Barang.Focus()
                    End If

                    Lv_Barang.Visible = False
                    Lv_Barang.Location = New Point(1200, 125)
                End Using
            Else
                Btn_Cari.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Barang.Text.Trim.Length = 0 Then Txt_Kd_Barang.Focus()
            Txt_Kd_Barang_Leave(Txt_Kd_Barang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 125)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Kd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_Nm_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nm_Barang.TextChanged
        If Switch_AutoComplete = False Then Exit Sub
        If Txt_Nm_Barang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 125)
            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(100, 125)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Distinct a.Kode_Barang, a.Nama "
            SQL = SQL & "from barang a, EMI_Group_Jenis b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and b.Flag_Finished_Good = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Nama like '%" & Txt_Nm_Barang.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Nm_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Nm_Barang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kd_Barang_Leave(Txt_Nm_Barang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 125)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Nm_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Nm_Barang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_Kd_Barang.Text = KdBarang
        Txt_Nm_Barang.Text = NmKdBarang

        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(100, 125)

        Btn_Cari.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Txt_Kd_Barang.Text.Trim.Length = 0 Or Txt_Nm_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Barang.Focus()
            Exit Sub
        End If

        Get_Data()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Dgv_Data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEndEdit
        If Dgv_Data.Rows.Count = 0 Or e.RowIndex = -1 Then Exit Sub

        If Dgv_Data.CurrentCell.ColumnIndex = Cell_Harga_Awal Or Dgv_Data.CurrentCell.ColumnIndex = Cell_Penyesuaian_Persen Or Dgv_Data.CurrentCell.ColumnIndex = Cell_Margin_Persen Then

            Get_Data_DGV(Dgv_Data.CurrentRow.Index)

            If Not IsNumeric(Dgv_Harga_awal) Then
                MessageBox.Show("Harga Awal Harus Angka", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_Data.CurrentRow.Cells(Cell_Harga_Awal).Value = Format(dataBackup(Dgv_Data.CurrentRow.Index).Harga_Awal, "N0")
                Dgv_Harga_awal = dataBackup(Dgv_Data.CurrentRow.Index).Harga_Awal
                'Exit Sub
            ElseIf Not IsNumeric(Dgv_Penyesuaian_Persen) Then
                MessageBox.Show("Penyesuaian Persen Harus Angka", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_Data.CurrentRow.Cells(Cell_Penyesuaian_Persen).Value = Format(dataBackup(Dgv_Data.CurrentRow.Index).Penyesuaian, "N4")
                Dgv_Penyesuaian_Persen = dataBackup(Dgv_Data.CurrentRow.Index).Penyesuaian
                'Exit Sub
            ElseIf Not IsNumeric(Dgv_Margin_Persen) Then
                MessageBox.Show("Margin Harus Angka", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_Data.CurrentRow.Cells(Cell_Margin_Persen).Value = Format(dataBackup(Dgv_Data.CurrentRow.Index).Margin, "N4")
                Dgv_Margin_Persen = dataBackup(Dgv_Data.CurrentRow.Index).Margin
                'Exit Sub
            End If

            Dim Harga_Awal As Double = Dgv_Harga_awal
            Dim Hpp_AVG As Double = Dgv_HPP_AVG
            Dim Persen_Penentu As Double = Dgv_Penyesuaian_Persen
            Dim Persen_Margin As Double = Dgv_Margin_Persen

            Dim Harga_Dasar As Double = Harga_Awal +
                    (Harga_Awal * (Persen_Penentu / 100))

            Dim Selisih As Double = Harga_Dasar - Hpp_AVG
            Dim Keuntungan As Double = Harga_Dasar * (Persen_Margin / 100)
            Dim Harga_Jual As Double = Harga_Dasar + Keuntungan

            Dim culture = Globalization.CultureInfo.GetCultureInfo("en-US")

            Dgv_Data.CurrentRow.Cells(Cell_Harga_Dasar).Value = Decimal.Parse(Harga_Dasar).ToString("N0", culture)
            Dgv_Data.CurrentRow.Cells(Cell_Selisih_HPP).Value = Decimal.Parse(Selisih).ToString("N0", culture)
            Dgv_Data.CurrentRow.Cells(Cell_Keuntungan).Value = Decimal.Parse(Keuntungan).ToString("N0", culture)
            Dgv_Data.CurrentRow.Cells(Cell_Harga_Jual).Value = Decimal.Parse(Harga_Jual).ToString("N0", culture)

            With Dgv_Data.CurrentRow.Cells
                .Item(Cell_Harga_Awal).Value = Decimal.Parse(Dgv_Harga_awal).ToString("N0", culture)
                .Item(Cell_Penyesuaian_Persen).Value = Decimal.Parse(Dgv_Penyesuaian_Persen).ToString("N4", culture)
                .Item(Cell_Margin_Persen).Value = Decimal.Parse(Dgv_Margin_Persen).ToString("N4", culture)
            End With

            Dgv_Data.CurrentRow.DefaultCellStyle.BackColor = Color.White
            Dgv_Data.CurrentRow.DefaultCellStyle.ForeColor = Color.Black

            Select Case Dgv_Data.CurrentCell.ColumnIndex
                Case Cell_Harga_Awal
                    Dgv_Data.CurrentRow.Cells(Cell_Harga_Awal).Style.BackColor = Color.LightGray
                Case Cell_Penyesuaian_Persen
                    Dgv_Data.CurrentRow.Cells(Cell_Penyesuaian_Persen).Style.BackColor = Color.LightGray
                Case Cell_Margin_Persen
                    Dgv_Data.CurrentRow.Cells(Cell_Margin_Persen).Style.BackColor = Color.LightGray
            End Select

        End If

    End Sub

    Private Sub Dgv_Data_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellClick
        'cek apakah yang di klik adalah header
        If e.RowIndex = -1 Or Dgv_Data.Rows.Count = 0 Then Exit Sub

        Dim currentCell As Integer = Dgv_Data.CurrentCell.ColumnIndex
        Dim currentRow As Integer = Dgv_Data.CurrentRow.Index

        Dim cellValue As Object = HilangkanTanda(Dgv_Data.Rows(currentRow).Cells(currentCell).Value)

        If currentCell = Cell_Harga_Awal Or currentCell = Cell_Penyesuaian_Persen Or currentCell = Cell_Margin_Persen Then

            Dim cellKuantity As String = HilangkanTanda(Dgv_Data.CurrentCell.Value)

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity)
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            Dgv_Data.Rows(currentRow).Cells(currentCell).Value = nilai

        End If

    End Sub

    Private Sub Dgv_Data_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellLeave
        If Dgv_Data.Rows.Count = 0 Or e.RowIndex = -1 Then Exit Sub

        Dim currentCell As Integer = Dgv_Data.CurrentCell.ColumnIndex

        If currentCell = Cell_Harga_Awal Then
            Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

            If Not String.IsNullOrEmpty(cellKuantity) Then

                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N0", Globalization.CultureInfo.GetCultureInfo("en-us"))

                Dgv_Data.CurrentCell.Value = formattedValue
            End If
        ElseIf currentCell = Cell_Penyesuaian_Persen Or currentCell = Cell_Margin_Persen Then
            Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

            If Not String.IsNullOrEmpty(cellKuantity) Then

                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N4", Globalization.CultureInfo.GetCultureInfo("en-us"))

                Dgv_Data.CurrentCell.Value = formattedValue
            End If
        End If

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Dgv_Data.Rows.Count = 0 Then
            MessageBox.Show("Tidak Ada Data yang Bisa Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            get_no_faktur()


            SQL = "select Kode_Perusahaan from N_EMI_Transaksi_Konfigurasi_Harga_Jual "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Transaksi = '" & Txt_NoTransaksi.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                Else
                    Dr.Close()
                    SQL = "insert into N_EMI_Transaksi_Konfigurasi_Harga_Jual (Kode_Perusahaan, No_Transaksi, Tanggal, Jam, User_ID) "
                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoTransaksi.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "') "
                    ExecuteTrans(SQL)
                End If
            End Using


            Dim hasData As Boolean = False


            For i As Integer = 0 To Dgv_Data.Rows.Count - 1

                Get_Data_DGV(i)

                Dim adada As Double = Dgv_Harga_awal
                Dim adasda As Double = Dgv_HPP_AVG
                Dim adadda As Double = Dgv_Penyesuaian_Persen
                Dim adaada As Double = Dgv_Harga_Dasar
                Dim adafda As Double = Dgv_Selisih_HPP
                Dim adawada As Double = Dgv_Margin_Persen
                Dim fasda As Double = Dgv_Harga_Jual

                Dim hasFirstData As Boolean
                '====================================
                '=     CEK APAKAH DATA PERTAMA?     =
                '====================================
                SQL = "select top 1 a.Kode_Perusahaan "
                SQL = SQL & "from N_EMI_Transaksi_Konfigurasi_Harga_Jual a, N_EMI_Transaksi_Konfigurasi_Harga_Jual_Detail b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                SQL = SQL & "and a.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.Kode_Barang = '" & Dgv_Kd_Barang & "' "
                SQL = SQL & "order by a.Tanggal DESC, a.Jam DESC "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        hasFirstData = False
                    Else
                        hasFirstData = True
                    End If
                End Using


                If hasFirstData Then

                    SQL = "insert into N_EMI_Transaksi_Konfigurasi_Harga_Jual_Detail "
                    SQL = SQL & "(Kode_Perusahaan, No_Transaksi, Kode_Barang, Nama_Barang, Harga_Penentu, HPP_AVG, Persentase_Penentu, Harga_Dasar, Selisih, Persentase_Margin, Harga_Jual) "
                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoTransaksi.Text & "', '" & Dgv_Kd_Barang & "', '" & Dgv_Nm_Barang & "', "
                    SQL = SQL & HilangkanTanda(Dgv_Harga_awal) & ", " & HilangkanTanda(Dgv_HPP_AVG) & ", " & HilangkanTanda(Dgv_Penyesuaian_Persen) & ", "
                    SQL = SQL & HilangkanTanda(Dgv_Harga_Dasar) & ", " & HilangkanTanda(Dgv_Selisih_HPP) & ", " & HilangkanTanda(Dgv_Margin_Persen) & ", " & HilangkanTanda(Dgv_Harga_Jual) & ") "
                    ExecuteTrans(SQL)

                    hasData = True

                Else

                    SQL = "select top 1 a.No_Transaksi, b.Kode_Barang, b.Nama_Barang, b.Harga_Penentu, b.Persentase_Penentu, Persentase_Margin "
                    SQL = SQL & "from N_EMI_Transaksi_Konfigurasi_Harga_Jual a, N_EMI_Transaksi_Konfigurasi_Harga_Jual_Detail b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                    SQL = SQL & "and a.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and b.Kode_Barang = '" & Dgv_Kd_Barang & "' "
                    SQL = SQL & "order by a.Tanggal DESC "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For j As Integer = 0 To .Rows.Count - 1

                                    Dim asdasd As String = HilangkanTanda(.Rows(j).Item("Harga_Penentu"))
                                    Dim asd As String = HilangkanTanda(.Rows(j).Item("Persentase_Penentu"))
                                    Dim asdasdadasd As String = HilangkanTanda(.Rows(j).Item("Persentase_Penentu"))

                                    'Cek apakah Data Update?
                                    If HilangkanTanda(.Rows(j).Item("Harga_Penentu")) = HilangkanTanda(Dgv_Harga_awal) AndAlso
                                        HilangkanTanda(.Rows(j).Item("Persentase_Penentu")) = HilangkanTanda(Dgv_Penyesuaian_Persen) AndAlso
                                        HilangkanTanda(.Rows(j).Item("Persentase_Margin")) = HilangkanTanda(Dgv_Margin_Persen) Then

                                        Exit For
                                    End If


                                    'INSERT DETAIL
                                    SQL = "insert into N_EMI_Transaksi_Konfigurasi_Harga_Jual_Detail "
                                    SQL = SQL & "(Kode_Perusahaan, No_Transaksi, Kode_Barang, Nama_Barang, Harga_Penentu, HPP_AVG, Persentase_Penentu, Harga_Dasar, Selisih, Persentase_Margin, Harga_Jual) "
                                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoTransaksi.Text & "', '" & Dgv_Kd_Barang & "', '" & Dgv_Nm_Barang & "', "
                                    SQL = SQL & HilangkanTanda(Dgv_Harga_awal) & ", " & HilangkanTanda(Dgv_HPP_AVG) & ", " & HilangkanTanda(Dgv_Penyesuaian_Persen) & ", "
                                    SQL = SQL & HilangkanTanda(Dgv_Harga_Dasar) & ", " & HilangkanTanda(Dgv_Selisih_HPP) & ", " & HilangkanTanda(Dgv_Margin_Persen) & ", " & HilangkanTanda(Dgv_Harga_Jual) & ") "
                                    ExecuteTrans(SQL)

                                    hasData = True

                                Next

                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan saat Simpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using



                End If

            Next


            If Not hasData Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Tidak Ada Data Yang Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()


    End Sub

End Class
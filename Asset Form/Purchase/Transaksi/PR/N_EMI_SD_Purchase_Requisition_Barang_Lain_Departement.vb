Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_SD_Purchase_Requisition_Barang_Lain_Departement
    Dim xno_faktur As String = ""
    Dim arrcari As New ArrayList
    Dim Jenis = "N_EMI_SD_Purchase_Requisition_Barang_Lain_Departement"
    Private Sub N_EMI_SD_Purchase_Requisition_Barang_Lain_Departement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub
    Private Sub kosong()
        Try
            OpenConn()
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now

            CmbSatuan_Kolom.Items.Clear() : arrcari.Clear()
            CmbSatuan_Kolom.Items.Add("No Faktur") : arrcari.Add("a.No_Faktur")
            CmbSatuan_Kolom.Items.Add("Kode Barang") : arrcari.Add("a.Kode_Barang")
            CmbSatuan_Kolom.Items.Add("Nama Barang") : arrcari.Add("a.Nama_Barang")
            CmbSatuan_Kolom.Items.Add("Cost Center") : arrcari.Add("b.Keterangan")
            CmbSatuan_Kolom.Items.Add("Gedung") : arrcari.Add("c.Keterangan")
            CmbSatuan_Kolom.Items.Add("Barang dalam pengajuan") : arrcari.Add("a.Flag_Ajukan")

            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = False

            CmbSatuan_Kolom.SelectedIndex = -1
            TxtSatuan_Value.Text = ""

            Label4.BackColor = Color.LightSteelBlue
            Label6.BackColor = Color.NavajoWhite
            Label5.BackColor = Color.RosyBrown

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("--SELURUH--")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            'ComboBox1.Items.Add("Seluruh")
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox6.Text = Lokasi

            If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
                ComboBox6.Enabled = False
            Else
                ComboBox6.Enabled = True
            End If

            DataGridView1.Rows.Clear()
            SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, a.Flag_Ajukan, "
            SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak "
            SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
            SQL = SQL & "and a.Flag_Sudah_PR is null "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null "
            SQL = SQL & "order by a.No_Faktur "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            'DataGridView1.Rows.Item(i).Cells(0).Value = ""
                            DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("No_Faktur")
                            DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Stock_Owner")
                            DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Nama_Barang")
                            DataGridView1.Rows.Item(i).Cells(5).Value = (Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR"), "N2"))
                            DataGridView1.Rows.Item(i).Cells(6).Value = .Rows(i).Item("Satuan")
                            DataGridView1.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Cost_Center")
                            DataGridView1.Rows.Item(i).Cells(8).Value = .Rows(i).Item("Gedung")
                            DataGridView1.Rows.Item(i).Cells(9).Value = .Rows(i).Item("No_Urut")
                            DataGridView1.Rows.Item(i).Cells(10).Value = .Rows(i).Item("Lokasi")
                            DataGridView1.Rows.Item(i).Cells(12).Value = .Rows(i).Item("Id_Cost_Center")

                            If .Rows(i).Item("Kode_Stock_Owner") = "-" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Ajukan")) = "Y" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.NavajoWhite
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) = "" Then
                                DataGridView1.Rows.Item(i).Cells(13).Value = "-"
                            Else
                                DataGridView1.Rows.Item(i).Cells(13).Value = .Rows(i).Item("Alasan_Tolak")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) <> "" And .Rows(i).Item("Kode_Stock_Owner") = "-" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.RosyBrown
                            End If

                        Next
                    Else
                        CloseConn()
                        MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        Dim list_urut As String = ""
        Dim flag_add As String = "T"
        For Each row As DataGridViewRow In DataGridView1.Rows
            ' Pastikan bukan baris baru (yang kosong di bawah)
            If Not row.IsNewRow Then
                ' Cek apakah checkbox (kolom 0) dicentang
                If Convert.ToBoolean(row.Cells(0).Value) = True Then
                    ' Ambil nilai dari kolom ke-1 sebagai nomor faktur
                    list_urut &= "'" & row.Cells(9).Value.ToString().Trim() & "', "
                    flag_add = "Y"

                    If Convert.ToString(row.Cells(2).Value).Trim() = "-" Then
                        MessageBox.Show("Data harus ada kode barang (kode barang tidak boleh '-' !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If

            End If
        Next

        If flag_add = "T" Then
            MessageBox.Show("barang yang mau dipurchaseing requisition belum di pilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        ' Hapus koma terakhir
        If list_urut.EndsWith(", ") Then
            list_urut = list_urut.Substring(0, list_urut.Length - 2)
        End If

        Purchase_Requisition_Barang_Lain.Label3.Text = list_urut
        Purchase_Requisition_Barang_Lain.cari_pr_departement()
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox3.Checked = False
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CmbSatuan_Kolom.Enabled = True
            CmbSatuan_Kolom.SelectedIndex = -1
            TxtSatuan_Value.Enabled = True
            TxtSatuan_Value.Text = ""
        Else
            CmbSatuan_Kolom.Enabled = False
            CmbSatuan_Kolom.SelectedIndex = -1
            TxtSatuan_Value.Enabled = False
            TxtSatuan_Value.Text = ""
        End If
    End Sub

    Public Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
        If CheckBox1.Checked = True Then
            If DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Tanggal mulai tidak boleh lebih dari tanggal selesai . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DateTimePicker1.Focus() : Exit Sub
            End If
        ElseIf CheckBox2.Checked = True Then
            If CmbSatuan_Kolom.SelectedIndex = -1 Then
                MessageBox.Show("Parameter filter harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmbSatuan_Kolom.Focus() : Exit Sub
            ElseIf TxtSatuan_Value.Text.Trim.Length = 0 And CmbSatuan_Kolom.Text <> "Barang dalam pengajuan" Then
                MessageBox.Show("Value filter harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TxtSatuan_Value.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            DataGridView1.Rows.Clear()

            SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, a.Flag_Ajukan, "
            SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak "
            SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
            SQL = SQL & "and a.Flag_Sudah_PR is null "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null "

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "d.Tanggal between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                'Pasang And
                If CmbSatuan_Kolom.Text = "Barang dalam pengajuan" Then
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " = 'Y' "
                Else
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
                End If

            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " d.tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            SQL = SQL & "order by a.No_Faktur "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            'DataGridView1.Rows.Item(i).Cells(0).Value = ""
                            DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("No_Faktur")
                            DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Stock_Owner")
                            DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Nama_Barang")
                            DataGridView1.Rows.Item(i).Cells(5).Value = (Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR"), "N2"))
                            DataGridView1.Rows.Item(i).Cells(6).Value = .Rows(i).Item("Satuan")
                            DataGridView1.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Cost_Center")
                            DataGridView1.Rows.Item(i).Cells(8).Value = .Rows(i).Item("Gedung")
                            DataGridView1.Rows.Item(i).Cells(9).Value = .Rows(i).Item("No_Urut")
                            DataGridView1.Rows.Item(i).Cells(10).Value = .Rows(i).Item("Lokasi")
                            DataGridView1.Rows.Item(i).Cells(12).Value = .Rows(i).Item("Id_Cost_Center")

                            If .Rows(i).Item("Kode_Stock_Owner") = "-" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Ajukan")) = "Y" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.NavajoWhite
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) = "" Then
                                DataGridView1.Rows.Item(i).Cells(13).Value = "-"
                            Else
                                DataGridView1.Rows.Item(i).Cells(13).Value = .Rows(i).Item("Alasan_Tolak")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) <> "" And .Rows(i).Item("Kode_Stock_Owner") = "-" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.RosyBrown
                            End If
                        Next
                    Else
                        CloseConn()
                        MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
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

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        'If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim currentRow = DataGridView1.CurrentRow.Index
        'Dim currentCell = DataGridView1.CurrentCellAddress.X

        'If e.KeyCode = Keys.F2 Then
        '    If DataGridView1.CurrentRow.Cells(3).Value = "-" Then
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.filter_tambahan = "and c.Kode_Stock_Owner = '" & DataGridView1.CurrentRow.Cells(10).Value & "'"
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.xurut_departement = DataGridView1.CurrentRow.Cells(9).Value
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_KodeBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_Satuan.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_NamaBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_NamaBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_KodeBarang.Visible = True

        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Enabled = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_KdGedung.Enabled = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Enabled = False

        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Text = DataGridView1.CurrentRow.Cells(7).Value
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Text = DataGridView1.CurrentRow.Cells(8).Value
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_CostCenter.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_Gedung.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Size = New Size(593, 355)

        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_PR.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Order.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Sisa.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_PR.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Order.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Sisa.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.ShowDialog()
        '    End If

        'End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = DataGridView1.Columns(11).Index AndAlso e.RowIndex >= 0 Then
            If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
                Exit Sub
            End If

            Dim currentRow = DataGridView1.CurrentRow.Index
            Dim currentCell = DataGridView1.CurrentCellAddress.X

            If DataGridView1.CurrentRow.Cells(3).Value = "-" And (DataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightSteelBlue Or DataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.RosyBrown) Then
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.filter_tambahan = "and c.Kode_Stock_Owner = '" & DataGridView1.CurrentRow.Cells(10).Value & "'"
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.xurut_departement = DataGridView1.CurrentRow.Cells(9).Value
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_KodeBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_Satuan.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_NamaBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_NamaBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_KodeBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.CheckBox1.Checked = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_NamaBarang.Text = DataGridView1.CurrentRow.Cells(4).Value

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Enabled = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_KdGedung.Enabled = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Enabled = False

                'N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Text = DataGridView1.CurrentRow.Cells(7).Value
                'N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Id_CostCenter.Text = DataGridView1.CurrentRow.Cells(12).Value
                'N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Text = DataGridView1.CurrentRow.Cells(8).Value
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_CostCenter.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_Gedung.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Size = New Size(593, 355)

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_PR.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Order.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Sisa.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_PR.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Order.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Sisa.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.ShowDialog()
            End If

        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            BtnCari_Click(CheckBox3, e)
        End If
    End Sub
End Class
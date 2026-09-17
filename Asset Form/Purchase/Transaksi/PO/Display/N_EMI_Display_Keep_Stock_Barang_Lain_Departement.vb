Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_Display_Keep_Stock_Barang_Lain_Departement
    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList

    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.No_Faktur, b.No_Faktur as No_PR_Departement, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Kode_Barang,  "
            SQL = SQL & "c.Nama, a.Jumlah, a.Satuan, a.Flag_Selesai_Pengeluaran_Barang, a.Status, a.Urut_Oto, a.UserId, a.UserId_Batal, a.Tgl_Batal "
            SQL = SQL & "from N_EMI_Keep_Stock_Barang_Lain_Departement a, N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b, "
            SQL = SQL & "Barang_Lain c, EMI_Group_Jenis_Lain d, EMI_Kategori_Gudang_PerLokasi_Barang_Lain e, View_Kategori_Turunan f, N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain g "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Departement = b.No_Urut and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Group_Jenis = d.Id_Group_Jenis "
            SQL = SQL & "and c.Kode_Perusahaan = e.Kode_Perusahaan and c.Id_Kategori_Gudang = e.ID_Kategori_Gudang "
            SQL = SQL & "and c.Kode_Perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan "
            SQL = SQL & "and c.Id_Sub_Kategori_Jenis_3 = f.Id_Sub_Kategori_Jenis_3 and f.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
            SQL = SQL & "and f.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.User_ID = '" & UserID & "' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            If semua = "T" Then
                If CheckBox1.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "and "

                    SQL = SQL & Arr1.Item(ComboBox3.SelectedIndex) & " between '"
                    SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                End If

                If CheckBox2.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
                End If

                If CheckBox3.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & " a.tanggal between '"
                    SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
                End If

                If ComboBox6.SelectedIndex = 0 Then
                    SQL = SQL & " and e.Kode_Stock_Owner in("
                    Dim list_kota As String = ""
                    For x As Integer = 1 To ComboBox6.Items.Count - 1
                        list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                    Next

                    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                    SQL = SQL & list_kota & ")"
                Else
                    SQL = SQL & " and e.Kode_Stock_Owner = '" & ComboBox6.Text & "' "
                End If

                SQL = SQL & "order by a.Tanggal, a.Jam "
            Else
                SQL = SQL & "order by a.Tanggal, a.Jam "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("No_Faktur"))
                    Lvw.SubItems.Add(dr("No_PR_Departement"))
                    If General_Class.CekNULL(dr("UserId")) <> "" Then
                        Lvw.SubItems.Add(dr("UserID"))
                    Else
                        Lvw.SubItems.Add("-")
                    End If
                    Lvw.SubItems.Add(Format(dr("tanggal"), "dd MMM yyyy"))
                    Lvw.SubItems.Add(dr("Jam"))
                    Lvw.SubItems.Add(dr("Kode_Stock_Owner"))
                    Lvw.SubItems.Add(dr("Kode_Barang"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(Format(dr("Jumlah"), "N2"))
                    Lvw.SubItems.Add(dr("Satuan"))
                    Lvw.SubItems.Add(dr("Urut_Oto"))

                    If General_Class.CekNULL(dr("UserId_Batal")) <> "" Then
                        Lvw.SubItems.Add(dr("UserId_Batal"))
                    Else
                        Lvw.SubItems.Add("-")
                    End If

                    If General_Class.CekNULL(dr("Tgl_Batal")) <> "" Then
                        Lvw.SubItems.Add(Format(dr("Tgl_Batal"), "dd MMM yyyy"))
                    Else
                        Lvw.SubItems.Add("-")
                    End If

                    If General_Class.CekNULL(dr("Flag_Selesai_Pengeluaran_Barang")) <> "" Then
                        Lvw.BackColor = Color.NavajoWhite
                    End If

                    If General_Class.CekNULL(dr("Status")) <> "" Then
                        Lvw.BackColor = Color.RosyBrown
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
    Private Sub N_EMI_Display_Keep_Stock_Barang_Lain_Departement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView1.Columns.Add("No Faktur", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("No PR Departement", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("UserID", 110, HorizontalAlignment.Left)
        ListView1.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Lokasi", 110, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode_Barang", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
        ListView1.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        ListView1.Columns.Add("Urut", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("UserID Batal", 110, HorizontalAlignment.Left)
        ListView1.Columns.Add("Tanggal Batal", 110, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Link", 250, HorizontalAlignment.Left)
        ListView1.View = View.Details

        kosong()
        Cari("Y")
    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        'If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
        '    MessageBox.Show("Belum Pilih Paramater", Judul)
        '    CheckBox1.Focus() : Exit Sub
        'End If

        If CheckBox1.Checked Then
            If ComboBox3.SelectedIndex = -1 Then
                MessageBox.Show("Paramater Tanggal belum dipilih", Judul)
                ComboBox3.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih kecil dari periode II!", Judul)
                DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                Exit Sub
            End If
        ElseIf CheckBox2.Checked Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("Paramater belum di pilih", Judul)
                ComboBox2.Focus() : Exit Sub
            ElseIf TextBox4.Text.Trim.Length = 0 Then
                MessageBox.Show("Value cari belum diisi", Judul)
                TextBox4.Focus() : Exit Sub
            End If
        End If

        Cari("T")
    End Sub

    Private Sub kosong()
        Try
            OpenConn()

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("--SELURUH--")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            ComboBox6.Text = Lokasi

            If CekButtonRole("Ganti_Lokasi_Keep_PR_Barang_Lain_Departement") = "T" Then
                ComboBox6.Enabled = False
            Else
                ComboBox6.Enabled = True
            End If

            ComboBox3.Items.Clear() : Arr1.Clear()
            ComboBox3.Items.Add("Tanggal") : Arr1.Add("a.Tanggal")

            ComboBox3.Enabled = False : ComboBox2.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            TextBox4.Enabled = False

            ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
            ComboBox2.Items.Add("No Faktur") : Arr2.Add("a.no_faktur")
            ComboBox2.Items.Add("No PR Departement") : Arr2.Add("b.no_faktur")
            ComboBox2.Items.Add("Kode Barang") : Arr2.Add("a.Kode_Barang")
            ComboBox2.Items.Add("Nama Barang") : Arr2.Add("c.nama")

            CloseConn()
        Catch ex As Exception
            ComboBox6.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub N_EMI_Display_Keep_Stock_Barang_Lain_Departement_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Pembatalan_Keep_Stock_PR_Barang_Lain_Departement") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin akan membatalkan Purhcase Requisition ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then Exit Sub

            Dim xurut_dept As Integer = 0
            SQL = "select Flag_Selesai_Pengeluaran_Barang, Status, Urut_Departement from N_EMI_Keep_Stock_Barang_Lain_Departement "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & ListView1.FocusedItem.SubItems(10).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    xurut_dept = Dr("Urut_Departement")
                    If General_Class.CekNULL(Dr("Status")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Keep Stock Purhcase Requisition Departement sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Flag_Selesai_Pengeluaran_Barang")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Keep Stock Purhcase Requisition Departement sudah sudah dikeluar kan stock nya sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Keep Stock Purhcase Requisition Departement tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Update N_EMI_Keep_Stock_Barang_Lain_Departement set Status = 'Y', "
            SQL = SQL & "UserId_Batal = '" & UserID & "', Tgl_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Urut_Oto = '" & ListView1.FocusedItem.SubItems(10).Text & "' "
            ExecuteTrans(SQL)

            SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set Flag_Sudah_PR = NULL "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Urut = '" & xurut_dept & "' "
            ExecuteTrans(SQL)

            SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement set Flag_PR = NULL "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & ListView1.FocusedItem.SubItems(1).Text & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Keep Stock Purhcase Requisition Departement berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BtnBarangMasuk_Cari_Click(BatalToolStripMenuItem, e)
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            BtnBarangMasuk_Cari_Click(CheckBox3, e)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox3.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            CheckBox3.Checked = False
        Else
            ComboBox3.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ComboBox3.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ComboBox2.Enabled = True : TextBox4.Enabled = True
        Else
            ComboBox2.Enabled = False : TextBox4.Enabled = False
            ComboBox2.SelectedIndex = -1 : TextBox4.Text = ""
        End If
    End Sub
End Class
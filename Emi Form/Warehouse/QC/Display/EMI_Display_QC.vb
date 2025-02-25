Public Class EMI_Display_QC

    Dim arrCombo1, arrCombo2, arrCombo3 As New ArrayList

    Private Sub EMI_Display_QC_GiveFeedback(sender As Object, e As GiveFeedbackEventArgs) Handles Me.GiveFeedback
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Display_QC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()


    End Sub

    Private Sub kosong()

        Lv_TF_QC.Columns.Clear() : Lv_TF_QC.Items.Clear()
        Lv_TF_QC.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_TF_QC.Columns.Add("Quality Awal", 150, HorizontalAlignment.Center)
        Lv_TF_QC.Columns.Add("Quality Tujuan", 150, HorizontalAlignment.Center)
        Lv_TF_QC.Columns.Add("Keterangan", 280, HorizontalAlignment.Left)
        Lv_TF_QC.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_TF_QC.Columns.Add("Jam", 90, HorizontalAlignment.Center)
        Lv_TF_QC.Columns.Add("User ID", 130, HorizontalAlignment.Left)
        'Hide
        Lv_TF_QC.Columns.Add("kd_qualityawal", 0, HorizontalAlignment.Center)
        Lv_TF_QC.Columns.Add("kd_qualitytujuan", 0, HorizontalAlignment.Center)
        Lv_TF_QC.View = View.Details


        Lv_Detail.Columns.Clear() : Lv_Detail.Items.Clear()
        Lv_Detail.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Nama Barang", 310, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Jumlah Bags", 120, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Rak Awal", 150, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Rak Tujuan", 150, HorizontalAlignment.Left)
        Lv_Detail.View = View.Details

        Try
            OpenConn()

            '== FILTER =='
            ComboBox1.Items.Clear() : arrCombo1.Clear()
            ComboBox1.Items.Add("--- Seluruh ---") : arrCombo1.Add("SELURUH")
            SQL = "select kode_stock_owner, keterangan from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox1.Items.Add(Dr("keterangan")) : arrCombo1.Add(Dr("kode_stock_owner"))
                Loop
            End Using
            ComboBox1.SelectedIndex = 0

            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = False

            ComboBox2.Items.Clear() : arrCombo2.Clear()
            ComboBox2.Items.Add("Tanggal") : arrCombo2.Add("a.Tanggal")

            ComboBox3.Items.Clear() : arrCombo3.Clear()
            ComboBox3.Items.Add("No Faktur") : arrCombo3.Add("a.No_Faktur")
            ComboBox3.Items.Add("Quality Awal") : arrCombo3.Add("b.Keterangan")
            ComboBox3.Items.Add("Quality Akhir") : arrCombo3.Add("c.Keterangan")
            ComboBox3.Items.Add("Keterangan") : arrCombo3.Add("a.keterangan")
            ComboBox3.Items.Add("User ID") : arrCombo3.Add("a.UserID")

            TextBox3.Text = ""

            ComboBox2.Enabled = False : ComboBox3.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            TextBox3.Enabled = False

            ComboBox1.Enabled = False


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        Load_TF_Quality()

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Load_TF_Quality(True)
    End Sub

    Private Sub Load_TF_Quality(ByVal Optional filter As Boolean = False)
        If filter Then

            If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
                MessageBox.Show("Lokasi Tidak Boleh Kosong", Judul)
                CheckBox1.Focus() : Exit Sub
            End If

            If CheckBox2.Checked Then
                If ComboBox2.SelectedIndex = -1 Then
                    MessageBox.Show("Jenis Tanggal Harus Diisi", Judul)
                    ComboBox2.Focus() : Exit Sub
                ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                    MessageBox.Show("Periode I Tidak boleh lebih dari periode II!", Judul)
                    DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                    Exit Sub
                End If
            End If
            If CheckBox3.Checked Then
                If ComboBox3.SelectedIndex = -1 Then
                    MessageBox.Show("Jenis Parameter Lain Harus Diisi", Judul)
                    ComboBox2.Focus() : Exit Sub
                    If TextBox3.Text.Trim.Length = 0 Then
                        MessageBox.Show("Value Parameter Lain Tidak Boleh Kosong", Judul)
                        TextBox3.Focus() : Exit Sub
                    End If
                End If
            End If
        End If

        Try
            OpenConn()

            Lv_TF_QC.Items.Clear() : Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, a.Quality_Awal, a.Quality_Tujuan, "
            SQL = SQL & "ISNULL(b.Keterangan, '-') as Quality_Awal_Ket, "
            SQL = SQL & "ISNULL(c.Keterangan, '-') as Quality_Tujuan_Ket, "
            SQL = SQL & "a.keterangan, a.Tanggal, a.Jam, a.UserID "
            SQL = SQL & "from Emi_TF_Quality a, EMI_Master_Warna b, EMI_Master_Warna c  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Quality_Awal = b.Kode_Warna "
            SQL = SQL & "and a.Quality_Tujuan = c.Kode_Warna "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "

            If filter Then

                'If ComboBox1.SelectedIndex = 0 Then
                '    SQL = SQL & " and a.Lokasi in("
                '    Dim list_kota As String = ""
                '    For x As Integer = 1 To ComboBox1.Items.Count - 1
                '        list_kota = list_kota & "'" & ComboBox1.Items(x).ToString & "', "
                '    Next

                '    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                '    SQL = SQL & list_kota & ")"
                'Else
                '    SQL = SQL & " and a.Lokasi = '" & ComboBox1.Text & "' "
                'End If

                If CheckBox1.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & " a.tanggal between '"
                    SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
                End If

                If CheckBox2.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & arrCombo2.Item(ComboBox2.SelectedIndex) & " between '"
                    SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                End If

                If CheckBox3.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & arrCombo3.Item(ComboBox3.SelectedIndex) & " like '%" & Trim(TextBox3.Text) & "%' "
                End If

            End If

            SQL = SQL & "order by a.No_Faktur "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_TF_QC.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Quality_Awal_Ket"))
                    Lv.SubItems.Add(Dr("Quality_Tujuan_Ket"))
                    Lv.SubItems.Add(Dr("keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("UserID"))
                    Lv.SubItems.Add(Dr("Quality_Awal"))
                    Lv.SubItems.Add(Dr("Quality_Tujuan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Detail_TF_QC(ByVal no_faktur As String)
        If Lv_TF_QC.Items.Count = 0 Or Lv_TF_QC.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, b.Kode_Barang, d.Nama as Nama_Barang, b.Jumlah, b.Jumlah_Bags, b.Satuan, e.Keterangan as Rak_Awal, f.Keterangan as Rak_Akhir "
            SQL = SQL & "from Emi_TF_Quality a, Emi_TF_Quality_Detail b, Emi_TF_Quality_Det c, Barang d, View_Warehouse_Position e, View_Warehouse_Position f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and "
            SQL = SQL & "c.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan  "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Id_Warehouse_Awal = e.Id_WMS_Warehouse_Position "
            SQL = SQL & "and c.Id_Warehouse_Akhir = f.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Faktur = '" & no_faktur & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Rak_Awal"))
                    Lv.SubItems.Add(Dr("Rak_Akhir"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_TF_QC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_TF_QC.SelectedIndexChanged
        If Lv_TF_QC.Items.Count = 0 Or Lv_TF_QC.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = Lv_TF_QC.FocusedItem.SubItems(0).Text

        Load_Detail_TF_QC(Faktur)

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            Btn_Cari_Click(CheckBox1, e)
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ComboBox2.Enabled = True
            CheckBox1.Checked = False
        Else
            ComboBox2.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker1.Enabled = False
            ComboBox2.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker1.Value = Now.Date
            ComboBox2.Text = ""
        End If
    End Sub



    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        If ComboBox2.Items.Count = 0 Then Exit Sub

        DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            ComboBox3.Enabled = True : TextBox3.Text = "" : TextBox3.Enabled = True
        Else
            ComboBox3.Enabled = False : TextBox3.Text = "" : TextBox3.Enabled = False
            ComboBox3.SelectedIndex = -1 : ComboBox3.Text = ""
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Items.Count = 0 Then Exit Sub

        TextBox3.Text = ""
    End Sub











End Class
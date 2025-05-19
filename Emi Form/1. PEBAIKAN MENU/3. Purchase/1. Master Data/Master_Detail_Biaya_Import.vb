Public Class Master_Detail_Biaya_Import
    Dim aa, ab, ac, b, bb, ca, cb As New ArrayList
    Private Sub Master_Detail_Biaya_Import1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListView1.Columns.Add("Nama Biaya", 150, HorizontalAlignment.Center)
        ListView1.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        ListView1.Columns.Add("Container", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Nama Perusahaan", 150, HorizontalAlignment.Left)
        ListView1.Columns.Add("Mata Uang", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nilai", 120, HorizontalAlignment.Right)
        ListView1.Columns.Add("Nilai 2", 120, HorizontalAlignment.Right)
        ListView1.Columns.Add("Perhitungan", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("Min", 50, HorizontalAlignment.Center)
        ListView1.Columns.Add("Max", 50, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Biaya", 100, HorizontalAlignment.Center).DisplayIndex = 0
        ListView1.Columns.Add("Jenis", 120, HorizontalAlignment.Left)

        ListView1.View = View.Details

        ListView2.Columns.Add("Nama Biaya", 150, HorizontalAlignment.Center)
        ListView2.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        ListView2.Columns.Add("Container", 120, HorizontalAlignment.Left)
        ListView2.Columns.Add("Nama Perusahaan", 150, HorizontalAlignment.Left)
        ListView2.Columns.Add("Mata Uang", 120, HorizontalAlignment.Center)
        ListView2.Columns.Add("Nilai", 120, HorizontalAlignment.Right)
        ListView2.Columns.Add("Nilai 2", 120, HorizontalAlignment.Right)
        ListView2.Columns.Add("Perhitungan", 120, HorizontalAlignment.Center)
        ListView2.Columns.Add("Min", 50, HorizontalAlignment.Center)
        ListView2.Columns.Add("Max", 50, HorizontalAlignment.Center)
        ListView2.Columns.Add("Kode Biaya", 100, HorizontalAlignment.Center).DisplayIndex = 0
        ListView2.Columns.Add("Jenis", 120, HorizontalAlignment.Left)

        ListView1.View = View.Details
        ComboBox1.Focus()
        Try
            OpenConn()
            ComboBox8.Items.Clear()
            SQL = "Select Kode_Master_Kategori_biaya_Import From Master_Kategori_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Form ='1' order by Kode_Master_Kategori_biaya_Import"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox8.Items.Add(dr("Kode_Master_Kategori_biaya_Import"))
                Loop
            End Using
            'ComboBox1.Items.Clear()
            'SQL = "Select Nama, Kode_Biaya, Kode_Kategori_Biaya_Import From Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' order by Nama"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        ComboBox1.Items.Add(dr("Nama")) : aa.Add(dr("Kode_Biaya")) : ab.Add(dr("Kode_Kategori_Biaya_Import")) : ac.Add(dr("Nama"))
            '    Loop
            'End Using

            ComboBox2.Items.Clear()
            SQL = "Select Kode_Stock_Owner From Stock_Owner_group where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' group by Kode_Stock_Owner "
            SQL = SQL & "order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using

            ComboBox3.Items.Clear()
            SQL = "Select Kode_Kontainer From Kontainer where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Kontainer"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox3.Items.Add(dr("Kode_Kontainer"))
                Loop
            End Using

            ComboBox5.Items.Clear()
            SQL = "Select Kode_Mata_Uang From Mata_Uang where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Mata_Uang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox5.Items.Add(dr("Kode_Mata_Uang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        ComboBox6.Items.Clear()
        ComboBox6.Items.Add("A- Lokasi & Container & Perusahaan")
        ComboBox6.Items.Add("B- Dokumen")
        ComboBox6.Items.Add("C- (NW x Nilai 1)+ Nilai 2")
        ComboBox6.Items.Add("D- Demurrage")
        ComboBox6.Items.Add("E- SPJM")
        ComboBox6.Items.Add("F- SPJM(Add)")
        ComboBox6.Items.Add("G- Biaya Lokasi Tujuan")
        ComboBox6.Items.Add("H- Karantina")
        ComboBox6.Items.Add("I- Asuransi")

        ComboBox7.Items.Clear()
        ComboBox7.Items.Add("ALL")
        ComboBox7.Items.Add("DRY")
        ComboBox7.Items.Add("WET")
        ComboBox7.Enabled = False

        Kosong()
    End Sub
    Private Sub Kosong()

        Try

            OpenConn()
            'SQL = "Select Kode_Perusahaan_Biaya_Import, Nama From Perusahaan_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "'"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        ca.Add(dr("Kode_Perusahaan_Biaya_Import")) : cb.Add(dr("Nama"))
            '    Loop
            'End Using

            ListView1.Items.Clear()

            SQL = "Select a.Kode_Biaya, b.Nama as biaya, a.Kode_Stock_Owner, a.Kode_Kontainer, c.Nama,a.Kode_Perusahaan_Biaya_Import, "
            SQL = SQL & "a.Kode_Mata_Uang, a.Nilai, a.Nilai_2, a.Perhitungan, a.min, a.max,a.jns From Biaya_Import_Detail a, Biaya_Import b, Perusahaan_biaya_import c "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Biaya = b.Kode_biaya and a.Kode_Perusahaan_Biaya_Import = c.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "order by a.Kode_Stock_Owner, b.Nama, a.Kode_Kontainer, c.Nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("biaya"))
                    Lvw.SubItems.Add(dr("Kode_Stock_Owner"))
                    Lvw.SubItems.Add(dr("Kode_Kontainer"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("Kode_Mata_Uang"))

                    If dr("perhitungan") = "I" Then
                        Lvw.SubItems.Add(Format(dr("Nilai"), "N3"))
                    Else
                        Lvw.SubItems.Add(Format(dr("Nilai"), "N2"))
                    End If

                    Lvw.SubItems.Add(Format(dr("Nilai_2"), "N2"))
                    Lvw.SubItems.Add(dr("Perhitungan"))
                    Lvw.SubItems.Add(dr("Min"))
                    Lvw.SubItems.Add(dr("Max"))
                    Lvw.SubItems.Add(dr("Kode_Biaya"))
                    If General_Class.CekNULL(dr("jns")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(General_Class.CekNULL(dr("jns")))
                    End If
                Loop
            End Using

            ListView2.Items.Clear()

            SQL = "Select top(50) a.Kode_Biaya, b.Nama as biaya, a.Kode_Stock_Owner, a.Kode_Kontainer, c.Nama,a.Kode_Perusahaan_Biaya_Import, "
            SQL = SQL & "a.Kode_Mata_Uang, a.Nilai, a.Nilai_2, a.Perhitungan, a.min, a.max,a.jns From Biaya_Import_Detail_Log a, Biaya_Import b, Perusahaan_biaya_import c "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Biaya = b.Kode_biaya and a.Kode_Perusahaan_Biaya_Import = c.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and a.flag_validasi is null "
            SQL = SQL & "order by a.Kode_Stock_Owner, b.Nama, a.Kode_Kontainer, c.Nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(dr("biaya"))
                    Lvw.SubItems.Add(dr("Kode_Stock_Owner"))
                    Lvw.SubItems.Add(dr("Kode_Kontainer"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("Kode_Mata_Uang"))

                    If dr("perhitungan") = "I" Then
                        Lvw.SubItems.Add(Format(dr("Nilai"), "N3"))
                    Else
                        Lvw.SubItems.Add(Format(dr("Nilai"), "N2"))
                    End If

                    Lvw.SubItems.Add(Format(dr("Nilai_2"), "N2"))
                    Lvw.SubItems.Add(dr("Perhitungan"))
                    Lvw.SubItems.Add(dr("Min"))
                    Lvw.SubItems.Add(dr("Max"))
                    Lvw.SubItems.Add(dr("Kode_Biaya"))
                    If General_Class.CekNULL(dr("jns")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(General_Class.CekNULL(dr("jns")))
                    End If
                Loop
            End Using
            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CkbImport.Checked = False
        CkbLokal.Checked = False

        ComboBox1.SelectedIndex = -1 : ComboBox2.SelectedIndex = -1 : ComboBox3.SelectedIndex = -1
        ComboBox4.SelectedIndex = -1 : ComboBox5.SelectedIndex = -1 : TextBox1.Text = ""
        ComboBox6.SelectedIndex = -1 : TextBox2.Text = ""
        ComboBox8.SelectedIndex = -1 : ComboBox9.SelectedIndex = -1 : ComboBox7.SelectedIndex = -1
        TextBox3.Text = "" : TextBox4.Text = ""
        Button1.Text = "Simpan" : Button2.Enabled = False : TextBox2.Enabled = False
        TextBox3.Enabled = False : TextBox4.Enabled = False
        ComboBox8.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim nilai2 As Double = 0
        Dim min As Integer = 0
        Dim max As Integer = 0

        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Nama Biaya harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show("Container harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub
        ElseIf ComboBox4.SelectedIndex = -1 Then
            MessageBox.Show("Nama Perusahaan harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf ComboBox5.SelectedIndex = -1 Then
            MessageBox.Show("Mata Uang harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox5.Focus() : Exit Sub
        ElseIf ComboBox6.SelectedIndex = -1 Then
            MessageBox.Show("Perhitungan harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus() : Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Nilai harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf ComboBox6.SelectedIndex = 4 Or ComboBox6.SelectedIndex = 5 Then
            If TextBox3.Text.Trim.Length = 0 Then
                MessageBox.Show("Nilai Min harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox3.Focus() : Exit Sub
            ElseIf TextBox4.Text.Trim.Length = 0 Then
                MessageBox.Show("Nilai Max harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox4.Focus() : Exit Sub
            Else
                min = Val(TextBox3.Text)
                max = Val(TextBox4.Text)
            End If
        ElseIf ComboBox6.SelectedIndex = 2 Then
            If TextBox2.Text.Trim.Length = 0 Then
                MessageBox.Show("Nilai 2 harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox2.Focus() : Exit Sub
            Else
                nilai2 = CDbl(Val(TextBox2.Text))
            End If
        ElseIf ComboBox6.SelectedIndex = 8 Then
            If TextBox2.Text.Trim.Length = 0 Then
                MessageBox.Show("Nilai 2 harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox2.Focus() : Exit Sub
            Else
                nilai2 = CDbl(Val(TextBox2.Text))
            End If
        ElseIf ComboBox6.SelectedIndex = 6 Then
            If ComboBox7.SelectedIndex = -1 Then
                MessageBox.Show("Jenis harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox7.Focus() : Exit Sub
            End If

        End If

        If CkbLokal.Checked = False And CkbImport.Checked = False Then
            MessageBox.Show("Check list harus di pilih minimal 1 ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CkbLokal.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            Dim import, lokal As String

            If CkbImport.Checked = True Then
                import = "Y"
            Else
                import = "T"
            End If

            If CkbLokal.Checked = True Then
                lokal = "Y"
            Else
                lokal = "T"
            End If

            If Button1.Text = "Simpan" Then



                SQL = "Insert Into Biaya_Import_Detail_Log(Kode_Perusahaan, Kode_Biaya, "
                SQL = SQL & "Kode_Stock_Owner, Kode_Kontainer, Kode_Perusahaan_Biaya_Import, "
                SQL = SQL & "Kode_Mata_Uang, Nilai, Nilai_2, Perhitungan, min, max ,jns_button, Flag_lokal, Flag_Import "

                If ComboBox7.SelectedIndex = -1 Then
                    SQL = SQL & ") "
                Else
                    SQL = SQL & ",jns) "
                End If

                SQL = SQL & "Values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & aa.Item(ComboBox1.SelectedIndex) & "', '" & ComboBox2.SelectedItem & "', "
                SQL = SQL & "'" & ComboBox3.SelectedItem & "', '" & b.Item(ComboBox4.SelectedIndex) & "', "
                SQL = SQL & "'" & ComboBox5.SelectedItem & "', '" & TextBox1.Text & "', '" & Ubah(nilai2) & "', "
                SQL = SQL & "'" & Microsoft.VisualBasic.Left(ComboBox6.SelectedItem, 1) & "','" & min & "','" & max & "','Simpan', '" & lokal & "', '" & import & "' "

                If ComboBox7.SelectedIndex = -1 Then
                    SQL = SQL & ")"
                Else
                    SQL = SQL & ",'" & ComboBox7.Text.Trim & "')"
                End If

                ExecuteTrans(SQL)

                'SQL = "Insert Into Biaya_Import_Detail(Kode_Perusahaan, Kode_Biaya, "
                'SQL = SQL & "Kode_Stock_Owner, Kode_Kontainer, Kode_Perusahaan_Biaya_Import, "
                'SQL = SQL & "Kode_Mata_Uang, Nilai, Nilai_2, Perhitungan, min, max "

                'If ComboBox7.SelectedIndex = -1 Then
                '    SQL = SQL & ") "
                'Else
                '    SQL = SQL & ",jns) "
                'End If

                'SQL = SQL & "Values('" & KodePerusahaan & "', "
                'SQL = SQL & "'" & aa.Item(ComboBox1.SelectedIndex) & "', '" & ComboBox2.SelectedItem & "', "
                'SQL = SQL & "'" & ComboBox3.SelectedItem & "', '" & b.Item(ComboBox4.SelectedIndex) & "', "
                'SQL = SQL & "'" & ComboBox5.SelectedItem & "', '" & TextBox1.Text & "', '" & Ubah(nilai2) & "', "
                'SQL = SQL & "'" & Microsoft.VisualBasic.Left(ComboBox6.SelectedItem, 1) & "','" & min & "','" & max & "' "

                'If ComboBox7.SelectedIndex = -1 Then
                '    SQL = SQL & ")"
                'Else
                '    SQL = SQL & ",'" & ComboBox7.Text.Trim & "')"
                'End If

                'ExecuteTrans(SQL)
            Else
                SQL = "Insert Into Biaya_Import_Detail_Log(Kode_Perusahaan, Kode_Biaya, "
                SQL = SQL & "Kode_Stock_Owner, Kode_Kontainer, Kode_Perusahaan_Biaya_Import, "
                SQL = SQL & "Kode_Mata_Uang, Nilai, Nilai_2, Perhitungan, min, max,jns_button, Flag_lokal, Flag_Import "

                If ComboBox7.SelectedIndex = -1 Then
                    SQL = SQL & ") "
                Else
                    SQL = SQL & ",jns) "
                End If

                SQL = SQL & "Values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & aa.Item(ComboBox1.SelectedIndex) & "', '" & ComboBox2.SelectedItem & "', "
                SQL = SQL & "'" & ComboBox3.SelectedItem & "', '" & b.Item(ComboBox4.SelectedIndex) & "', "
                SQL = SQL & "'" & ComboBox5.SelectedItem & "', '" & TextBox1.Text & "', '" & Ubah(nilai2) & "', "
                SQL = SQL & "'" & Microsoft.VisualBasic.Left(ComboBox6.SelectedItem, 1) & "','" & min & "','" & max & "','Update', '" & lokal & "', '" & import & "' "

                If ComboBox7.SelectedIndex = -1 Then
                    SQL = SQL & ")"
                Else
                    SQL = SQL & ",'" & ComboBox7.Text.Trim & "')"
                End If

                ExecuteTrans(SQL)
                'SQL = "Update Biaya_Import_Detail Set Kode_Mata_Uang = '" & ComboBox5.SelectedItem & "', "
                'SQL = SQL & "Nilai = '" & TextBox1.Text & "', Nilai_2 ='" & Ubah(nilai2) & "', "
                'SQL = SQL & "Perhitungan = '" & Microsoft.VisualBasic.Left(ComboBox6.SelectedItem, 1) & "', min = '" & min & "', max = '" & max & "' "

                'If ComboBox7.SelectedIndex = -1 Then
                '    SQL = SQL & ",jns = null "
                'Else
                '    SQL = SQL & ",jns = '" & ComboBox7.Text.Trim & "' "
                'End If

                'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & aa.Item(ComboBox1.SelectedIndex) & "' and "
                'SQL = SQL & "Kode_Stock_Owner = '" & ComboBox2.SelectedItem & "' and Kode_Kontainer = '" & ComboBox3.SelectedItem & "' and "
                'SQL = SQL & "Kode_Perusahaan_Biaya_Import = '" & b.Item(ComboBox4.SelectedIndex) & "'"
                'ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            'MessageBox.Show("data masuk")

            MessageBox.Show("Data Berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
    End Sub

    Private Sub ComboBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.Leave
        cl()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        'If ComboBox1.SelectedIndex = -1 Then Exit Sub
        'b.Clear() : bb.Clear()
        'Try
        '    OpenConn()
        '    ComboBox4.Items.Clear()
        '    SQL = "Select Kode_Perusahaan_Biaya_Import,Nama From Perusahaan_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Kategori_Biaya_Import ='" & ab.Item(ComboBox1.SelectedIndex) & "' order by Nama"
        '    Using dr = OpenTrans(SQL)
        '        Do While dr.Read
        '            ComboBox4.Items.Add(dr("Nama")) : b.Add(dr("Kode_Perusahaan_Biaya_Import")) : bb.Add(dr("Nama"))
        '        Loop
        '    End Using
        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Kosong()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus1 = vbYes Then
            Try

                OpenConn()

                If CekButtonRole("Hapus_Biaya_Import_Detail") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Delete From Biaya_Import_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & aa.Item(ComboBox1.SelectedIndex) & "'"
                SQL = SQL & "and Kode_Stock_Owner = '" & ComboBox2.SelectedItem & "' and Kode_Kontainer = '" & ComboBox3.SelectedItem & "' and Kode_Perusahaan_Biaya_Import ='" & b.Item(ComboBox4.SelectedIndex) & "'"
                ExecuteTrans(SQL)
                Cmd.Transaction.Commit()
                CloseTrans()
                CloseConn()

                MessageBox.Show("Data Berhasil dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            MessageBox.Show("Penghapusan dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        Kosong()
        ComboBox1.Focus()
    End Sub
    Private Sub cl()
        If ComboBox4.SelectedIndex = -1 Or ComboBox3.SelectedIndex = -1 Or ComboBox2.SelectedIndex = -1 Or ComboBox1.SelectedIndex = -1 Then Exit Sub
        Try
            OpenConn()

            SQL = "Select Kode_Mata_Uang, Nilai, Nilai_2, Perhitungan, Min, Max, jns, isnull(Flag_lokal,'T') as Flag_lokal, isnull(Flag_Import,'T') Flag_Import "
            SQL = SQL & " From Biaya_Import_Detail Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Biaya = '" & aa.Item(ComboBox1.SelectedIndex) & "' and "
            SQL = SQL & "kode_Stock_Owner = '" & ComboBox2.SelectedItem & "' and "
            SQL = SQL & "kode_Kontainer = '" & ComboBox3.SelectedItem & "' and "
            SQL = SQL & "kode_Perusahaan_Biaya_Import = '" & b.Item(ComboBox4.SelectedIndex) & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    ComboBox5.Text = Dr("Kode_Mata_Uang")
                    TextBox1.Text = Dr("Nilai")

                    If Dr("Perhitungan") = "A" Then
                        ComboBox6.SelectedIndex = 0
                    ElseIf Dr("Perhitungan") = "B" Then
                        ComboBox6.SelectedIndex = 1
                    ElseIf Dr("Perhitungan") = "C" Then
                        ComboBox6.SelectedIndex = 2
                    ElseIf Dr("Perhitungan") = "D" Then
                        ComboBox6.SelectedIndex = 3
                    ElseIf Dr("Perhitungan") = "E" Then
                        ComboBox6.SelectedIndex = 4
                    ElseIf Dr("Perhitungan") = "F" Then
                        ComboBox6.SelectedIndex = 5
                    ElseIf Dr("Perhitungan") = "G" Then
                        ComboBox6.SelectedIndex = 6
                    ElseIf Dr("Perhitungan") = "H" Then
                        ComboBox6.SelectedIndex = 7
                    ElseIf Dr("Perhitungan") = "I" Then
                        ComboBox6.SelectedIndex = 8
                    Else
                        MessageBox.Show("Terjadi Kesalahan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If


                    If ComboBox6.SelectedIndex = 4 Or ComboBox6.SelectedIndex = 5 Then
                        TextBox3.Text = Dr("Min")
                        TextBox3.Enabled = True
                        TextBox4.Text = Dr("Max")
                        TextBox4.Enabled = True
                    Else
                        TextBox3.Clear()
                        TextBox3.Enabled = False
                        TextBox4.Clear()
                        TextBox4.Enabled = False
                    End If

                    If ComboBox6.SelectedIndex = 2 Then
                        TextBox2.Text = Dr("Nilai_2")
                        TextBox2.Enabled = True
                    Else
                        TextBox2.Clear()
                        TextBox2.Enabled = False
                    End If

                    If ComboBox6.SelectedIndex = 8 Then
                        TextBox2.Text = Dr("Nilai_2")
                        TextBox2.Enabled = True
                    Else
                        TextBox2.Clear()
                        TextBox2.Enabled = False
                    End If

                    If General_Class.CekNULL(Dr("jns")) = "" Then
                        ComboBox7.SelectedIndex = -1
                        ComboBox7.Enabled = False
                    Else
                        ComboBox7.Text = General_Class.CekNULL(Dr("jns"))
                    End If

                    If Dr("Flag_lokal") = "T" Then
                        CkbLokal.Checked = False
                    Else
                        CkbLokal.Checked = True
                    End If

                    If Dr("Flag_import") = "T" Then
                        CkbImport.Checked = False
                    Else
                        CkbImport.Checked = True
                    End If

                    Button1.Text = "Update" : Button2.Enabled = True
                Else
                    ComboBox5.SelectedIndex = -1
                    TextBox1.Text = ""
                    ComboBox6.SelectedIndex = -1
                    Button1.Text = "Simpan" : Button2.Enabled = False : TextBox2.Enabled = False
                    CkbImport.Checked = False
                    CkbLokal.Checked = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox5.Focus()
    End Sub

    Private Sub ComboBox4_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox4.Leave
        cl()
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub
        Try
            OpenConn()

            SQL = "select a.Kode_master_kategori_biaya_import, b.Kode_kategori_biaya_import, c.KOde_biaya, c.nama from master_kategori_biaya_import a, Kategori_biaya_import b, biaya_import c where "
            SQL = SQL & "a.Kode_master_kategori_biaya_import = b.Kode_master_kategori_biaya_import and "
            SQL = SQL & "b.Kode_kategori_biaya_import = c.Kode_kategori_biaya_import and c.Kode_Biaya = '" & ListView1.FocusedItem.SubItems(10).Text & "' "
            Using Dr = OpenTrans(SQL)

                If Dr.Read Then
                    ComboBox8.Text = Dr("Kode_master_kategori_biaya_import")
                    ComboBox8_SelectedIndexChanged(ListView1, e)
                    ComboBox9.Text = Dr("Kode_kategori_biaya_import")
                    ComboBox9_SelectedIndexChanged(ListView1, e)
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        ComboBox1.Text = ListView1.FocusedItem.SubItems(0).Text
        ComboBox2.Text = ListView1.FocusedItem.SubItems(1).Text
        ComboBox2_SelectedIndexChanged(ListView1, e)
        ComboBox3.Text = ListView1.FocusedItem.SubItems(2).Text
        ComboBox4.Text = ListView1.FocusedItem.SubItems(3).Text

        ComboBox4_Leave(ListView1, e)
    End Sub

    Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox4.Focus()
    End Sub

    Private Sub ComboBox3_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox3.Leave
        cl()
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox3.Focus()
    End Sub

    Private Sub ComboBox2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.Leave
        cl()
    End Sub

    Private Sub ComboBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox6.Focus()
    End Sub

    Private Sub ComboBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox6.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub Master_Detail_Biaya_Import_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label5.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.SelectedIndex = -1 Then Exit Sub

        If ComboBox6.SelectedIndex = 2 Or ComboBox6.SelectedIndex = 8 Then
            TextBox2.Enabled = True
        Else
            TextBox2.Enabled = False
            TextBox2.Text = ""
        End If

        'If ComboBox6.SelectedIndex = 8 Then
        '    TextBox2.Enabled = True
        'Else
        '    TextBox2.Enabled = False
        '    TextBox2.Text = ""
        'End If

        If ComboBox6.SelectedIndex = 4 Or ComboBox6.SelectedIndex = 5 Then
            TextBox4.Enabled = True
            TextBox3.Enabled = True
        Else
            TextBox4.Enabled = False
            TextBox4.Text = ""
            TextBox3.Enabled = False
            TextBox3.Text = ""
        End If

        If ComboBox6.SelectedIndex = 6 Then
            ComboBox7.Enabled = True
        Else
            ComboBox7.Enabled = False
            ComboBox7.SelectedIndex = -1
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ComboBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox8.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox9.Focus()
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged
        If ComboBox8.SelectedIndex = -1 Then Exit Sub
        Try
            OpenConn()
            ComboBox9.Items.Clear()
            SQL = "select Kode_Kategori_Biaya_import from kategori_biaya_import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Master_Kategori_biaya_Import ='" & ComboBox8.Text & "' order by Kode_Kategori_Biaya_import"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox9.Items.Add(dr("Kode_Kategori_Biaya_import"))
                Loop
            End Using


            ComboBox4.Items.Clear()
            b.Clear()
            SQL = "Select Kode_Perusahaan_Biaya_Import,Nama From Perusahaan_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Master_Kategori_Biaya_import ='" & ComboBox8.Text & "' order by Nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("Nama")) : b.Add(dr("Kode_Perusahaan_Biaya_Import")) ': bb.Add(dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged

    End Sub

    Private Sub ComboBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox9.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox1.Focus()
    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox9.SelectedIndexChanged
        If ComboBox9.SelectedIndex = -1 Then Exit Sub
        Try
            OpenConn()
            ComboBox1.Items.Clear()
            aa.Clear()
            SQL = "Select Nama, Kode_Biaya, Kode_Kategori_Biaya_Import From Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "'and Kode_Kategori_Biaya_import ='" & ComboBox9.Text & "'  order by Nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Nama")) : aa.Add(dr("Kode_Biaya")) ': ab.Add(dr("Kode_Kategori_Biaya_Import")) : ac.Add(dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        'If ComboBox2.SelectedIndex = -1 Then Exit Sub
        'Try
        '    OpenConn()
        '    ComboBox7.Items.Clear()
        '    SQL = "Select Kode_Gudang from Master_gudang where Kode_Perusahaan = '" & KodePerusahaan & "' and Lokasi ='" & ComboBox2.Text & "' order by Kode_Gudang"
        '    Using dr = OpenTrans(SQL)
        '        Do While dr.Read
        '            ComboBox7.Items.Add(dr("Kode_Gudang"))
        '        Loop
        '    End Using
        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then TextBox4.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

End Class
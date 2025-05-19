Public Class Master_Detail_Biaya_Lokal
    Dim arrKodeBiaya, arrPerhitungan As New ArrayList

    Private Sub Master_Detail_Biaya_Import1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Lv_Data.Columns.Add("Kode Biaya", 180, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Nama Biaya", 180, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Perhitungan", 180, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Jenis", 120, HorizontalAlignment.Center) '3
        Lv_Data.View = View.Details


        'ListView2.Columns.Add("Nama Biaya", 150, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        'ListView2.Columns.Add("Container", 120, HorizontalAlignment.Left)
        'ListView2.Columns.Add("Nama Perusahaan", 150, HorizontalAlignment.Left)
        'ListView2.Columns.Add("Mata Uang", 120, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Nilai", 120, HorizontalAlignment.Right)
        'ListView2.Columns.Add("Nilai 2", 120, HorizontalAlignment.Right)
        'ListView2.Columns.Add("Perhitungan", 120, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Min", 50, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Max", 50, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Kode Biaya", 100, HorizontalAlignment.Center).DisplayIndex = 0
        'ListView2.Columns.Add("Jenis", 120, HorizontalAlignment.Left)
        'ListView2.View = View.Details

        Cmb_Master.Focus()
        Btn_Simpan.Tag = "SIMPAN"

        Try
            OpenConn()
            Cmb_Master.Items.Clear()
            SQL = "Select Kode_Master_Kategori_biaya_Import From Master_Kategori_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Form ='1' order by Kode_Master_Kategori_biaya_Import"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Master.Items.Add(dr("Kode_Master_Kategori_biaya_Import"))
                Loop

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Perhitungan.Items.Clear() : arrPerhitungan.Clear()
        Cmb_Perhitungan.Items.Add("A- Lokasi & Container & Perusahaan") : arrPerhitungan.Add("A")
        Cmb_Perhitungan.Items.Add("B- Dokumen") : arrPerhitungan.Add("B")
        Cmb_Perhitungan.Items.Add("C- (NW x Nilai 1)+ Nilai 2") : arrPerhitungan.Add("C")
        Cmb_Perhitungan.Items.Add("D- Demurrage") : arrPerhitungan.Add("D")
        Cmb_Perhitungan.Items.Add("E- SPJM") : arrPerhitungan.Add("E")
        Cmb_Perhitungan.Items.Add("F- SPJM(Add)") : arrPerhitungan.Add("F")
        Cmb_Perhitungan.Items.Add("G- Biaya Lokasi Tujuan") : arrPerhitungan.Add("G")
        Cmb_Perhitungan.Items.Add("H- Karantina") : arrPerhitungan.Add("H")
        Cmb_Perhitungan.Items.Add("I- Asuransi") : arrPerhitungan.Add("I")


        Kosong()
    End Sub
    Private Sub Kosong()

        Try

            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.Kode_Biaya, b.Nama as Nama_Biaya, a.Perhitungan, a.Flag_lokal, a.Flag_Import "
            SQL = SQL & "from Biaya_B2B a, Biaya_Import b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Biaya = b.Kode_Biaya "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_Data.Items.Add(dr("Kode_Biaya"))
                    Lvw.SubItems.Add(dr("Nama_Biaya"))

                    Select Case dr("Perhitungan")
                        Case "A"
                            Lvw.SubItems.Add("A- Lokasi & Container & Perusahaan")
                        Case "B"
                            Lvw.SubItems.Add("B- Dokumen")
                        Case "C"
                            Lvw.SubItems.Add("C- (NW x Nilai 1)+ Nilai 2")
                        Case "D"
                            Lvw.SubItems.Add("D- Demurrage")
                        Case "E"
                            Lvw.SubItems.Add("E- SPJM")
                        Case "F"
                            Lvw.SubItems.Add("F- SPJM(Add)")
                        Case "G"
                            Lvw.SubItems.Add("G- Biaya Lokasi Tujuan")
                        Case "H"
                            Lvw.SubItems.Add("H- Karantina")
                        Case "I"
                            Lvw.SubItems.Add("I- Asuransi")
                        Case Else
                            Lvw.SubItems.Add("-")
                    End Select

                    If dr("Flag_lokal") = "Y" And dr("Flag_Import") = "T" Then
                        Lvw.SubItems.Add("Lokal")
                    ElseIf dr("Flag_Import") = "Y" And dr("Flag_lokal") = "T" Then
                        Lvw.SubItems.Add("Import")
                    ElseIf dr("Flag_Import") = "Y" And dr("Flag_lokal") = "Y" Then
                        Lvw.SubItems.Add("Lokal, Import")
                    End If

                Loop
            End Using

            'ListView2.Items.Clear()

            'SQL = "Select top(50) a.Kode_Biaya, b.Nama as biaya, a.Kode_Stock_Owner, a.Kode_Kontainer, c.Nama,a.Kode_Perusahaan_Biaya_Import, "
            'SQL = SQL & "a.Kode_Mata_Uang, a.Nilai, a.Nilai_2, a.Perhitungan, a.min, a.max,a.jns From Biaya_Import_Detail_Log a, Biaya_Import b, Perusahaan_biaya_import c "
            'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Biaya = b.Kode_biaya and a.Kode_Perusahaan_Biaya_Import = c.Kode_Perusahaan_Biaya_Import "
            'SQL = SQL & "and a.flag_validasi is null "
            'SQL = SQL & "order by a.Kode_Stock_Owner, b.Nama, a.Kode_Kontainer, c.Nama "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = ListView2.Items.Add(dr("biaya"))
            '        Lvw.SubItems.Add(dr("Kode_Stock_Owner"))
            '        Lvw.SubItems.Add(dr("Kode_Kontainer"))
            '        Lvw.SubItems.Add(dr("Nama"))
            '        Lvw.SubItems.Add(dr("Kode_Mata_Uang"))

            '        If dr("perhitungan") = "I" Then
            '            Lvw.SubItems.Add(Format(dr("Nilai"), "N3"))
            '        Else
            '            Lvw.SubItems.Add(Format(dr("Nilai"), "N2"))
            '        End If

            '        Lvw.SubItems.Add(Format(dr("Nilai_2"), "N2"))
            '        Lvw.SubItems.Add(dr("Perhitungan"))
            '        Lvw.SubItems.Add(dr("Min"))
            '        Lvw.SubItems.Add(dr("Max"))
            '        Lvw.SubItems.Add(dr("Kode_Biaya"))
            '        If General_Class.CekNULL(dr("jns")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(General_Class.CekNULL(dr("jns")))
            '        End If
            '    Loop
            'End Using


            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CkbImport.Checked = False
        CkbLokal.Checked = False

        Cmb_Master.Enabled = True
        Cmb_Kategori.Enabled = True
        Cmb_Biaya.Enabled = True

        Cmb_Biaya.SelectedIndex = -1
        Cmb_Perhitungan.SelectedIndex = -1
        Cmb_Master.SelectedIndex = -1 : Cmb_Kategori.SelectedIndex = -1

        Btn_Simpan.Text = "&Simpan" : Btn_Simpan.Tag = "SIMPAN"
        Btn_Hapus.Enabled = False
        Cmb_Master.Focus()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Cmb_Master.SelectedIndex = -1 Then
            MessageBox.Show("Kode Master harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Master.Focus() : Exit Sub

        ElseIf Cmb_Kategori.SelectedIndex = -1 Then
            MessageBox.Show("Kode Kategori harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Kategori.Focus() : Exit Sub

        ElseIf Cmb_Biaya.SelectedIndex = -1 Then
            MessageBox.Show("Nama Biaya harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Biaya.Focus() : Exit Sub

        ElseIf Cmb_Perhitungan.SelectedIndex = -1 Then
            MessageBox.Show("Perhitungan harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Perhitungan.Focus() : Exit Sub

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


            If Btn_Simpan.Tag = "SIMPAN" Then

                SQL = "insert into Biaya_B2B (Kode_Perusahaan, Kode_Biaya, Perhitungan, Flag_lokal, Flag_Import) "
                SQL = SQL & "values ('" & KodePerusahaan & "', '" & arrKodeBiaya.Item(Cmb_Biaya.SelectedIndex) & "', "
                SQL = SQL & "'" & arrPerhitungan(Cmb_Perhitungan.SelectedIndex) & "', '" & lokal & "', '" & import & "')"
                ExecuteTrans(SQL)

            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                SQL = "update Biaya_B2B set Perhitungan = '" & arrPerhitungan(Cmb_Perhitungan.SelectedIndex) & "', "
                SQL = SQL & "Flag_lokal = '" & lokal & "', Flag_Import = '" & import & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & arrKodeBiaya.Item(Cmb_Biaya.SelectedIndex) & "' "
                ExecuteTrans(SQL)

                '==============================
                '=     UPDATE FLAG UPDATE     =
                '==============================
                SQL = "update Biaya_B2B set Flag_Update = 'Y' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & arrKodeBiaya.Item(Cmb_Biaya.SelectedIndex) & "' "
                ExecuteTrans(SQL)

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

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Cmb_Biaya.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Perhitungan.Focus()
    End Sub

    Private Sub ComboBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Biaya.Leave
        If Cmb_Biaya.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            SQL = "select a.Kode_Biaya, d.Kode_Master_Kategori_biaya_Import, c.Kode_Kategori_Biaya_import, b.Nama as Nama_Biaya, a.Perhitungan, a.Flag_lokal, a.Flag_Import "
            SQL = SQL & "from Biaya_B2B a, Biaya_Import b, kategori_biaya_import c, Master_Kategori_Biaya_Import d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Biaya = b.Kode_Biaya "
            SQL = SQL & "and b.Kode_Kategori_Biaya_Import = c.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Biaya = '" & arrKodeBiaya(Cmb_Biaya.SelectedIndex) & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Cmb_Master.Text = Dr("Kode_Master_Kategori_biaya_Import")
                    ComboBox8_SelectedIndexChanged(Lv_Data, e)
                    Cmb_Kategori.Text = Dr("Kode_kategori_biaya_import")
                    ComboBox9_SelectedIndexChanged(Lv_Data, e)
                    Cmb_Perhitungan.SelectedIndex = arrPerhitungan.IndexOf(Dr("Perhitungan"))

                    If Dr("Flag_lokal") = "Y" Then
                        CkbLokal.Checked = True
                    Else
                        CkbLokal.Checked = False
                    End If
                    If Dr("Flag_Import") = "Y" Then
                        CkbImport.Checked = True
                    Else
                        CkbImport.Checked = False
                    End If

                    Cmb_Master.Enabled = False
                    Cmb_Kategori.Enabled = False
                    Cmb_Biaya.Enabled = False

                    Cmb_Biaya.SelectedIndex = arrKodeBiaya.IndexOf(Dr("Kode_Biaya"))

                    Btn_Hapus.Enabled = True
                    Btn_Simpan.Text = "&Update"
                    Btn_Simpan.Tag = "UPDATE"

                    Btn_Refresh.Focus()

                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub




    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus1 = vbYes Then
            Try

                OpenConn()

                If CekButtonRole("Hapus_Biaya_Lokal_Detail") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Cmd.Transaction = Cn.BeginTransaction

                '========================================
                '=     CEK APAKAH DATA SUDAH PINDAH     =
                '========================================
                SQL = "select Flag_Sudah_Pindah  from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & arrKodeBiaya.Item(Cmb_Biaya.SelectedIndex) & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("Flag_Sudah_Pindah")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Biaya Sudah Tidak Bisa Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Biaya Tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "Delete from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & arrKodeBiaya.Item(Cmb_Biaya.SelectedIndex) & "' "
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
        Cmb_Biaya.Focus()
    End Sub

    Private Sub cl()
        'If Cmb_Biaya.SelectedIndex = -1 Then Exit Sub
        'Try
        '    OpenConn()

        '    SQL = "Select Kode_Mata_Uang, Nilai, Nilai_2, Perhitungan, Min, Max, jns, isnull(Flag_lokal,'T') as Flag_lokal, isnull(Flag_Import,'T') Flag_Import "
        '    SQL = SQL & " From Biaya_Import_Detail Where "
        '    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "Kode_Biaya = '" & arrKodeBiaya.Item(Cmb_Biaya.SelectedIndex) & "' and "
        '    SQL = SQL & "kode_Stock_Owner = 'X' and "
        '    SQL = SQL & "kode_Kontainer = 'X' and "
        '    SQL = SQL & "kode_Perusahaan_Biaya_Import = 'X'"
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then

        '            If Dr("Perhitungan") = "A" Then
        '                Cmb_Perhitungan.SelectedIndex = 0
        '            ElseIf Dr("Perhitungan") = "B" Then
        '                Cmb_Perhitungan.SelectedIndex = 1
        '            ElseIf Dr("Perhitungan") = "C" Then
        '                Cmb_Perhitungan.SelectedIndex = 2
        '            ElseIf Dr("Perhitungan") = "D" Then
        '                Cmb_Perhitungan.SelectedIndex = 3
        '            ElseIf Dr("Perhitungan") = "E" Then
        '                Cmb_Perhitungan.SelectedIndex = 4
        '            ElseIf Dr("Perhitungan") = "F" Then
        '                Cmb_Perhitungan.SelectedIndex = 5
        '            ElseIf Dr("Perhitungan") = "G" Then
        '                Cmb_Perhitungan.SelectedIndex = 6
        '            ElseIf Dr("Perhitungan") = "H" Then
        '                Cmb_Perhitungan.SelectedIndex = 7
        '            ElseIf Dr("Perhitungan") = "I" Then
        '                Cmb_Perhitungan.SelectedIndex = 8
        '            Else
        '                MessageBox.Show("Terjadi Kesalahan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            End If



        '            If Dr("Flag_lokal") = "T" Then
        '                CkbLokal.Checked = False
        '            Else
        '                CkbLokal.Checked = True
        '            End If

        '            If Dr("Flag_import") = "T" Then
        '                CkbImport.Checked = False
        '            Else
        '                CkbImport.Checked = True
        '            End If

        '            Btn_Refresh.Text = "Update" : Btn_Hapus.Enabled = True
        '        Else
        '            Cmb_Perhitungan.SelectedIndex = -1
        '            Btn_Refresh.Text = "Simpan" : Btn_Hapus.Enabled = False
        '            CkbImport.Checked = False
        '            CkbLokal.Checked = False
        '        End If
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub




    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem.Index = -1 Then Exit Sub
        Try
            OpenConn()

            SQL = "select a.Kode_Biaya, d.Kode_Master_Kategori_biaya_Import, c.Kode_Kategori_Biaya_import, b.Nama as Nama_Biaya, a.Perhitungan, a.Flag_lokal, a.Flag_Import "
            SQL = SQL & "from Biaya_B2B a, Biaya_Import b, kategori_biaya_import c, Master_Kategori_Biaya_Import d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Biaya = b.Kode_Biaya "
            SQL = SQL & "and b.Kode_Kategori_Biaya_Import = c.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Biaya = '" & Lv_Data.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Cmb_Master.Text = Dr("Kode_Master_Kategori_biaya_Import")
                    ComboBox8_SelectedIndexChanged(Lv_Data, e)
                    Cmb_Kategori.Text = Dr("Kode_kategori_biaya_import")
                    ComboBox9_SelectedIndexChanged(Lv_Data, e)
                    Cmb_Perhitungan.SelectedIndex = arrPerhitungan.IndexOf(Dr("Perhitungan"))

                    If Dr("Flag_lokal") = "Y" Then
                        CkbLokal.Checked = True
                    Else
                        CkbLokal.Checked = False
                    End If
                    If Dr("Flag_Import") = "Y" Then
                        CkbImport.Checked = True
                    Else
                        CkbImport.Checked = False
                    End If

                    Cmb_Master.Enabled = False
                    Cmb_Kategori.Enabled = False
                    Cmb_Biaya.Enabled = False

                    Btn_Hapus.Enabled = True
                    Btn_Simpan.Text = "&Update"
                    Btn_Simpan.Tag = "UPDATE"

                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Btn_Hapus.Enabled = False
                    Exit Sub
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Biaya.Text = Lv_Data.FocusedItem.Text


        ComboBox1_Leave(Lv_Data, e)
    End Sub

    Private Sub ComboBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Cmb_Perhitungan.KeyPress
        If e.KeyChar = Chr(13) Then CkbLokal.Focus()
    End Sub

    Private Sub Master_Detail_Biaya_Import_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Lbl_Judul.Size = New Point(Me.Width, 33)
    End Sub



    Private Sub ComboBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Cmb_Master.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Kategori.Focus()
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Master.SelectedIndexChanged
        If Cmb_Master.SelectedIndex = -1 Then Exit Sub
        Try
            OpenConn()
            Cmb_Kategori.Items.Clear()
            SQL = "select Kode_Kategori_Biaya_import from kategori_biaya_import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Master_Kategori_biaya_Import ='" & Cmb_Master.Text & "' order by Kode_Kategori_Biaya_import"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Kategori.Items.Add(dr("Kode_Kategori_Biaya_import"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub ComboBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Cmb_Kategori.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Biaya.Focus()
    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Kategori.SelectedIndexChanged
        If Cmb_Kategori.SelectedIndex = -1 Then Exit Sub
        Try
            OpenConn()
            Cmb_Biaya.Items.Clear()
            arrKodeBiaya.Clear()
            SQL = "Select Nama, Kode_Biaya, Kode_Kategori_Biaya_Import From Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "'and Kode_Kategori_Biaya_import ='" & Cmb_Kategori.Text & "'  order by Nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Biaya.Items.Add(dr("Nama")) : arrKodeBiaya.Add(dr("Kode_Biaya")) ': ab.Add(dr("Kode_Kategori_Biaya_Import")) : ac.Add(dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


End Class
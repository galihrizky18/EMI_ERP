Public Class Master_Suppliers
    Dim arrkolom, arrkategori, arrkategoriImport, arrPerhitunganTempo, arrKatBaru As New ArrayList

    Private Sub Perusahaan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns.Add("Kode Kategori", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Supplier", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Hutang", 100, HorizontalAlignment.Right)
        ListView1.Columns.Add("Alamat", 350, HorizontalAlignment.Left)
        ListView1.Columns.Add("Pemilik", 220, HorizontalAlignment.Left)
        ListView1.Columns.Add("Telepon", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Fax", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Contact Person", 240, HorizontalAlignment.Left)
        ListView1.Columns.Add("HP CP", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kategori Import", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Supplier", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Negara", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kota", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Port", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("PIC", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Mata Uang Rek", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Mata Uang Dec", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Mata Uang Bayar", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Perhitungan Jth Tempo", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Ket Perhitungan Jth Tempo", 100, HorizontalAlignment.Left)

        ListView1.View = View.Details

        Kosong()
        TextBox1.Focus()
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try

            OpenConn()

            ListView1.Items.Clear()
            SQL = "Select kode_supplier, nama, alamat, pemilik, telepon, fax, contact_person, "
            SQL = SQL & "hp_cp, kode_kategori, hutang, Kategori_Import, Nama_Supplier, Negara, Kota,"
            SQL = SQL & "Perhitungan_Jatuh_Tempo, Ket_Perhitungan_Jatuh_Tempo,  Port, PIC, Mata_Uang_Rek, "
            SQL = SQL & "Mata_Uang_Declare, Mata_Uang_Bayar From suppliers where kode_perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrkolom.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox9.Text & "%' "
                SQL = SQL & "order by " & arrkolom.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "order by nama"
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("kode_kategori"))
                    Lvw.SubItems.Add(dr("kode_supplier"))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Nama")))
                    Lvw.SubItems.Add(Format(dr("hutang"), "N0"))
                    Lvw.SubItems.Add(dr("Alamat"))
                    Lvw.SubItems.Add(dr("pemilik"))
                    Lvw.SubItems.Add(dr("telepon"))
                    Lvw.SubItems.Add(dr("fax"))
                    Lvw.SubItems.Add(dr("contact_person"))
                    Lvw.SubItems.Add(General_Class.CekNULL(("hp_cp")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Kategori_Import")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Nama_Supplier")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Negara")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Kota")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Port")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("PIC")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Mata_Uang_Rek")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Mata_Uang_Declare")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Mata_Uang_Bayar")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Perhitungan_Jatuh_Tempo")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Ket_Perhitungan_Jatuh_Tempo")))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Kosong()
        Try

            OpenConn()

            ComboBox2.Items.Clear() : arrkategori.Clear()
            SQL = "Select kode_kategori From kategori_supplier where kode_perusahaan = '" & KodePerusahaan & "' order by kode_kategori"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("kode_kategori")) : arrkategori.Add(dr("kode_kategori"))
                Loop
            End Using

            CmbKategori.Items.Clear() : arrKatBaru.Clear()
            SQL = "select Kode_Kategori_Suppliers, ID_Kategori_Suppliers from Suppliers_Kategori where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Kategori_Suppliers "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbKategori.Items.Add(dr("Kode_Kategori_Suppliers")) : arrKatBaru.Add(dr("ID_Kategori_Suppliers"))
                Loop
            End Using

            ComboBox4.Items.Clear() : ComboBox5.Items.Clear() : ComboBox6.Items.Clear()
            SQL = "Select Kode_Mata_Uang From Mata_Uang where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Mata_Uang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("Kode_Mata_Uang")) : ComboBox5.Items.Add(dr("Kode_Mata_Uang")) : ComboBox6.Items.Add(dr("Kode_Mata_Uang"))
                Loop
            End Using

            ComboBox7.Items.Clear() : arrPerhitunganTempo.Clear()
            SQL = "select Kode_Jenis_Perhitungan_JT,Keterangan from Emi_Master_Perhitungan_Jatuh_Tempo "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox7.Items.Add(dr("Keterangan"))
                    arrPerhitunganTempo.Add(dr("Kode_Jenis_Perhitungan_JT"))
                Loop
            End Using
            'ComboBox7.Items.Add("ETA") : arrPerhitunganTempo.Add("ETA")
            'ComboBox7.Items.Add("ETD") : arrPerhitunganTempo.Add("ETD")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox3.Items.Clear() : arrkategoriImport.Clear()
        ComboBox3.Items.Add("Utama") : arrkategoriImport.Add("Utama")
        ComboBox3.Items.Add("Penolong") : arrkategoriImport.Add("Penolong")

        Cari("Y")

        ComboBox1.Items.Clear() : arrkolom.Clear()
        ComboBox1.Items.Add("Kode Kategori") : arrkolom.Add("Kode_Kategori")
        ComboBox1.Items.Add("Kode Supplier") : arrkolom.Add("Kode_Supplier")
        ComboBox1.Items.Add("Nama") : arrkolom.Add("Nama_supplier")
        ComboBox1.Items.Add("Alamat") : arrkolom.Add("Alamat")
        ComboBox1.Items.Add("Pemilik") : arrkolom.Add("Pemilik")
        ComboBox1.Items.Add("Telepon") : arrkolom.Add("Telepon")
        ComboBox1.Items.Add("Fax") : arrkolom.Add("Fax")
        ComboBox1.Items.Add("Contact Person") : arrkolom.Add("Contact_Person")
        ComboBox1.Items.Add("HP") : arrkolom.Add("HP_CP")
        ComboBox1.Items.Add("Kategori Import") : arrkolom.Add("Kategori_import")
        ComboBox1.Items.Add("Negara") : arrkolom.Add("Negara")
        ComboBox1.Items.Add("Kota") : arrkolom.Add("Kota")
        ComboBox1.Items.Add("Port") : arrkolom.Add("Port")
        ComboBox1.Items.Add("Nama Alias") : arrkolom.Add("Nama")
        ComboBox1.Items.Add("PIC") : arrkolom.Add("PIC")
        ComboBox1.Items.Add("Mata Uang Rek") : arrkolom.Add("Mata_Uang_Rek")
        ComboBox1.Items.Add("Mata Uang Dec") : arrkolom.Add("Mata_Uang_Declare")
        ComboBox1.Items.Add("Mata Uang Bayar") : arrkolom.Add("Mata_Uang_Bayar")
        ComboBox1.Items.Add("Perhitungan Jatuh Tempo") : arrkolom.Add("Perhitungan_Jatuh_Tempo")
        ComboBox1.Items.Add("Ket Perhitungan Jatuh Tempo") : arrkolom.Add("Ket_Perhitungan_Jatuh_Tempo")

        cmb_JenisPPH.Items.Clear()
        cmb_JenisPPH.Items.Add("21")
        cmb_JenisPPH.Items.Add("23")
        cmb_JenisPPH.SelectedIndex = -1 : cmb_JenisPPH.Text = ""

        TextBox1.Text = "" : ComboBox2.SelectedIndex = -1 : ComboBox3.SelectedIndex = -1
        TextBox2.Text = "" : TextBox3.Text = ""
        TextBox4.Text = "" : TextBox5.Text = "" : TextBox6.Text = ""
        TextBox7.Text = "" : TextBox8.Text = "" : TextBox9.Text = ""
        TextBox10.Text = "" : TextBox11.Text = "" : TextBox12.Text = ""
        TextBox13.Text = "" : TextBox14.Text = "" : TextBox15.Text = ""
        Txt_PPN.Text = "" : Txt_PPH.Text = ""
        ComboBox4.SelectedIndex = -1 : ComboBox5.SelectedIndex = -1 : ComboBox6.SelectedIndex = -1
        Button1.Text = "&Simpan" : Button2.Enabled = False

    End Sub



    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave

        If TextBox1.Text.Trim.Length = 0 Then Exit Sub
        'If ComboBox2.SelectedIndex = -1 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select kode_supplier, nama, alamat, pemilik, telepon, fax, contact_person,ID_Kategori_Suppliers, "
            SQL = SQL & "hp_cp, kode_kategori, Kategori_Import, Nama_Supplier, Negara, Kota, Port, "
            SQL = SQL & "PIC, Mata_Uang_Rek, Mata_Uang_Declare, Mata_Uang_Bayar, Perhitungan_Jatuh_Tempo, "
            SQL = SQL & "Ket_Perhitungan_Jatuh_Tempo, PPN, PPH From suppliers Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_supplier = '" & TextBox1.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("kode_supplier")
                    TextBox2.Text = General_Class.CekNULL(Dr("Nama_Supplier"))
                    TextBox3.Text = Dr("alamat")
                    TextBox4.Text = Dr("pemilik")
                    TextBox5.Text = Dr("Telepon")
                    TextBox6.Text = Dr("fax")
                    TextBox7.Text = Dr("contact_person")
                    TextBox8.Text = Dr("hp_cp")
                    For i As Integer = 0 To ComboBox2.Items.Count - 1
                        If arrkategori.Item(i) = Dr("kode_kategori") Then
                            ComboBox2.SelectedIndex = i
                        End If
                    Next

                    For i As Integer = 0 To ComboBox3.Items.Count - 1
                        If arrkategoriImport.Item(i) = General_Class.CekNULL(Dr("Kategori_Import")) Then
                            ComboBox3.SelectedIndex = i
                        End If
                    Next
                    TextBox10.Text = General_Class.CekNULL(Dr("Negara"))
                    TextBox11.Text = General_Class.CekNULL(Dr("Kota"))
                    TextBox12.Text = General_Class.CekNULL(Dr("Port"))
                    TextBox13.Text = General_Class.CekNULL(Dr("Nama"))
                    TextBox14.Text = General_Class.CekNULL(Dr("PIC"))
                    For i As Integer = 0 To CmbKategori.Items.Count - 1
                        If arrKatBaru.Item(i) = General_Class.CekNULL(Dr("ID_Kategori_Suppliers")) Then
                            CmbKategori.SelectedIndex = i
                        End If
                    Next
                    'CmbKategori.Text = Dr("Kategori_Import")
                    ComboBox4.Text = General_Class.CekNULL(Dr("Mata_Uang_Rek"))
                    ComboBox5.Text = General_Class.CekNULL(Dr("Mata_Uang_Declare"))
                    ComboBox6.Text = General_Class.CekNULL(Dr("Mata_Uang_Bayar"))
                    Txt_PPN.Text = If(General_Class.CekNULL(Dr("PPN")) = "", 0, Dr("PPN"))
                    Txt_PPH.Text = If(General_Class.CekNULL(Dr("PPH")) = "", 0, Dr("PPH"))

                    For i As Integer = 0 To ComboBox7.Items.Count - 1
                        If arrPerhitunganTempo.Item(i) = General_Class.CekNULL(Dr("Perhitungan_Jatuh_Tempo")) Then
                            ComboBox7.SelectedIndex = i
                        End If
                    Next
                    'ComboBox7.Text = General_Class.CekNULL(Dr("Perhitungan_Jatuh_Tempo"))
                    TextBox15.Text = General_Class.CekNULL(Dr("Ket_Perhitungan_Jatuh_Tempo"))
                    Button1.Text = "&Update" : Button2.Enabled = True
                Else
                    ComboBox2.SelectedIndex = -1 : ComboBox3.SelectedIndex = -1
                    TextBox2.Text = "" : TextBox3.Text = ""
                    TextBox4.Text = "" : TextBox5.Text = "" : TextBox6.Text = ""
                    TextBox7.Text = "" : TextBox8.Text = "" : TextBox9.Text = ""
                    TextBox10.Text = "" : TextBox11.Text = "" : TextBox12.Text = ""
                    TextBox13.Text = "" : TextBox14.Text = ""
                    Txt_PPN.Text = "" : Txt_PPH.Text = ""
                    ComboBox4.SelectedIndex = -1 : ComboBox5.SelectedIndex = -1 : ComboBox6.SelectedIndex = -1 : ComboBox7.SelectedIndex = -1
                    TextBox15.Text = ""
                    Button1.Text = "&Simpan" : Button2.Enabled = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode supplier harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        ElseIf TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Alamat harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus() : Exit Sub
        ElseIf TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Pemilik harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus() : Exit Sub
        ElseIf TextBox5.Text.Trim.Length = 0 Then
            MessageBox.Show("Telepon harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox5.Focus() : Exit Sub
        ElseIf TextBox6.Text.Trim.Length = 0 Then
            MessageBox.Show("Fax harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox6.Focus() : Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Contact Person harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus() : Exit Sub
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("HP Contact Person harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus() : Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Kode kategori harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show("Kode kategori import harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Negara harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus() : Exit Sub
        ElseIf TextBox11.Text.Trim.Length = 0 Then
            MessageBox.Show("Kota harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox11.Focus() : Exit Sub
        ElseIf TextBox12.Text.Trim.Length = 0 Then
            MessageBox.Show("Port harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox12.Focus() : Exit Sub
        ElseIf TextBox13.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Alias harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox13.Focus() : Exit Sub
        ElseIf TextBox14.Text.Trim.Length = 0 Then
            MessageBox.Show("PIC harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox14.Focus() : Exit Sub
        ElseIf ComboBox4.SelectedIndex = -1 Then
            MessageBox.Show("Mata Uang Rek harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf ComboBox5.SelectedIndex = -1 Then
            MessageBox.Show("Mata Uang Dec harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox5.Focus() : Exit Sub
        ElseIf ComboBox6.SelectedIndex = -1 Then
            MessageBox.Show("Mata Uang Bayar harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus() : Exit Sub
        ElseIf ComboBox7.SelectedIndex = -1 Then
            MessageBox.Show("Perhitungan Jatuh Tempo harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus() : Exit Sub
        ElseIf TextBox15.Text = "" Then
            MessageBox.Show("Keterangan jatuh tempo harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus() : Exit Sub
        ElseIf CmbKategori.Text = "" Then
            MessageBox.Show("Kategori Import harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbKategori.Focus() : Exit Sub
        ElseIf Txt_PPN.Text = "" Then
            MessageBox.Show("PPN harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_PPN.Focus() : Exit Sub
        ElseIf Txt_PPH.Text = "" Then
            MessageBox.Show("PPH harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_PPH.Focus() : Exit Sub
        ElseIf cmb_JenisPPH.SelectedIndex = -1 Then
            MessageBox.Show("Jenis PPH harus dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmb_JenisPPH.Focus() : Exit Sub
        End If


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Button1.Text = "&Simpan" Then

                '=================================
                '=     CEK KATEGORI SUPPLIER     =
                '=================================
                Dim jenisKategori As String = ""
                SQL = "select Flag_Jenis_Import from Suppliers_Kategori where ID_Kategori_Suppliers = '" & arrKatBaru(CmbKategori.SelectedIndex) & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("Flag_Jenis_Import")) = "" Then
                            jenisKategori = "T"
                        Else
                            jenisKategori = "Y"
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Kategori Supplier Tidak Ditemukan di Sistem", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "Insert Into suppliers(Kode_Perusahaan, kode_kategori, kode_supplier, nama, "
                SQL = SQL & "alamat, pemilik, telepon, fax, contact_person, hp_cp, hutang, Kategori_Import,"
                SQL = SQL & " Nama_Supplier, Negara, Kota, Port, PIC, Mata_Uang_Rek, Mata_Uang_Declare, Mata_Uang_Bayar, "
                SQL = SQL & " Perhitungan_Jatuh_Tempo, Ket_Perhitungan_Jatuh_Tempo,ID_Kategori_Suppliers, "
                SQL = SQL & "inisial_sup, tampil_di_PO, Flag_Average, Flag_Form_E, Metode_Selisih_Declare, Biaya_Form_E, PPN, PPH, Jenis_PPH) "
                SQL = SQL & "Values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & arrkategori.Item(ComboBox2.SelectedIndex) & "', "
                SQL = SQL & "'" & TextBox1.Text.Trim & "', '" & TextBox13.Text.Trim & "', "
                SQL = SQL & "'" & TextBox3.Text.Trim & " ', '" & TextBox4.Text.Trim & "', "
                SQL = SQL & "'" & TextBox5.Text & "', "
                SQL = SQL & "'" & TextBox6.Text & " ', '" & TextBox7.Text.Trim & "', "
                SQL = SQL & "'" & TextBox8.Text & "', 0, '" & arrkategoriImport.Item(ComboBox3.SelectedIndex) & "', "
                SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & TextBox10.Text.Trim & "', '" & TextBox11.Text.Trim & "', "
                SQL = SQL & "'" & TextBox12.Text.Trim & "', '" & TextBox14.Text.Trim & "', '" & ComboBox4.Text & "', "
                SQL = SQL & "'" & ComboBox5.Text & "', '" & ComboBox6.Text & "','" & arrPerhitunganTempo.Item(ComboBox7.SelectedIndex) & "',"
                SQL = SQL & "'" & TextBox15.Text & "','" & arrKatBaru.Item(CmbKategori.SelectedIndex) & "', '','" & jenisKategori & "','T',NULL, 'A', 0, "
                SQL = SQL & "'" & Txt_PPN.Text.Trim & "', '" & Txt_PPH.Text.Trim & "', '" & cmb_JenisPPH.Text & "') "
                ExecuteTrans(SQL)
            Else
                SQL = "Update suppliers Set nama = '" & TextBox13.Text.Trim & "', "
                SQL = SQL & "Alamat = '" & TextBox3.Text.Trim & "', "
                SQL = SQL & "pemilik = '" & TextBox4.Text.Trim & "', "
                SQL = SQL & "telepon = '" & TextBox5.Text & "', "
                SQL = SQL & "fax = '" & TextBox6.Text & "', "
                SQL = SQL & "contact_person = '" & TextBox7.Text.Trim & "', "
                SQL = SQL & "hp_cp = '" & TextBox8.Text & "', "
                SQL = SQL & "kode_kategori = '" & arrkategori.Item(ComboBox2.SelectedIndex) & "', "
                SQL = SQL & "kategori_import = '" & arrkategoriImport.Item(ComboBox3.SelectedIndex) & "', "
                SQL = SQL & "Negara = '" & TextBox10.Text & "', "
                SQL = SQL & "Kota = '" & TextBox11.Text & "', "
                SQL = SQL & "Port = '" & TextBox12.Text & "', "
                SQL = SQL & "Nama_Supplier = '" & TextBox2.Text & "', "
                SQL = SQL & "PIC = '" & TextBox14.Text & "', "
                SQL = SQL & "Mata_Uang_Rek = '" & ComboBox4.Text & "', "
                SQL = SQL & "Mata_Uang_Declare = '" & ComboBox5.Text & "', "
                SQL = SQL & "Mata_Uang_Bayar = '" & ComboBox6.Text & "', "
                SQL = SQL & "Perhitungan_Jatuh_Tempo = '" & arrPerhitunganTempo.Item(ComboBox7.SelectedIndex) & "',"
                SQL = SQL & "Ket_Perhitungan_Jatuh_Tempo = '" & TextBox15.Text & "',"
                SQL = SQL & "ID_Kategori_Suppliers = '" & arrKatBaru.Item(CmbKategori.SelectedIndex) & "', "
                SQL = SQL & "inisial_sup = '', "
                SQL = SQL & "tampil_di_PO = 'T', "
                SQL = SQL & "Flag_Average = 'T', "
                SQL = SQL & "Flag_Form_E = NULL, "
                SQL = SQL & "Metode_Selisih_Declare = 'A', "
                SQL = SQL & "Biaya_Form_E = 0, "
                SQL = SQL & "PPN = '" & Txt_PPN.Text.Trim & "', "
                SQL = SQL & "PPH = '" & Txt_PPH.Text.Trim & "', "
                SQL = SQL & "Jenis_PPH = '" & cmb_JenisPPH.Text & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_supplier = '" & TextBox1.Text.Trim & "'"
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then

            Try

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                'Penjualan
                SQL = "Select top 1 kode_perusahaan from Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and kode_supplier = '" & TextBox1.Text.Trim & "'"
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data penjualan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "Delete From suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_supplier = '" & TextBox1.Text.Trim & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseConn()
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
        TextBox1.Focus()
    End Sub

    'GENERATE KODE SUPPLIER
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        'If TextBox2.Text.Trim.Length > 1 Then Exit Sub
        If Button1.Text = "&Update" Then Exit Sub
        If TextBox2.Text.Trim = "" Then TextBox1.Text = "" : Exit Sub

        Dim Supplier_Kode As String = ""


        Try
            OpenConn()

            'CEK APAKAH ADA DATA DI KODE SUPPLIER

            SQL = "select top 1 Kode_Supplier from Suppliers where Kode_Supplier like 'A___' order by Kode_Supplier desc "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Not General_Class.CekNULL(Dr("Kode_Supplier")) = "" Then
                        Dim data As String = Dr("Kode_Supplier").ToString
                        Dim Digit As String = data.Substring(data.Length - 3)
                        Dim angka As Integer = Val(Digit) + 1
                        Supplier_Kode = "A" & angka.ToString("D3")
                    End If
                Else
                    Supplier_Kode = "A001"
                End If
            End Using

            TextBox1.Text = Supplier_Kode

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox9.Focus()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox9.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub TextBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox9.KeyPress
        If e.KeyChar = Chr(13) Then Button5_Click(TextBox9, e)
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub

        'For i As Integer = 0 To ComboBox2.Items.Count - 1
        '    xSplit = ComboBox2.Items(i).split("-")
        '    If ListView1.FocusedItem.Text = xSplit(0).Trim Then
        '        ComboBox2.SelectedIndex = i
        '        Exit For
        '    End If
        'Next
        TextBox1.Text = ListView1.Items(ListView1.FocusedItem.Index).SubItems(1).Text
        Button1.Text = "&Update"
        TextBox1_Leave(ListView1, e)
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Chr(13) Then TextBox8.Focus()
    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Master_Suppliers_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    'Private Sub ComboBox2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.Leave
    '    TextBox2.Text = "" : TextBox3.Text = "" : TextBox4.Text = "" : TextBox5.Text = ""
    '    TextBox6.Text = "" : TextBox7.Text = "" : TextBox8.Text = ""
    '    Button1.Text = "&Simpan" : Button2.Enabled = False
    'End Sub

    Private Sub TextBox14_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox14.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox4.Focus()
    End Sub

    Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then TextBox10.Focus()
    End Sub

    Private Sub TextBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Chr(13) Then TextBox11.Focus()
    End Sub

    Private Sub TextBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
        If e.KeyChar = Chr(13) Then TextBox12.Focus()
    End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox12.KeyPress
        If e.KeyChar = Chr(13) Then TextBox13.Focus()
    End Sub

    Private Sub TextBox13_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox13.KeyPress
        If e.KeyChar = Chr(13) Then TextBox14.Focus()
    End Sub

    Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox5.Focus()
    End Sub

    Private Sub ComboBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox6.Focus()
    End Sub

    Private Sub ComboBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox6.KeyPress, cmb_JenisPPH.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox7.Focus()
    End Sub

    Private Sub ComboBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox7.KeyPress
        If e.KeyChar = Chr(13) Then TextBox15.Focus()
    End Sub

    Private Sub TextBox15_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox15.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub CmbKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKategori.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox3.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Kosong()
        TextBox1.Focus()
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then CmbKategori.Focus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub Txt_PPN_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = ".") Then
            e.Handled = True
        End If

        ' Cegah lebih dari satu tanda titik (.)
        If e.KeyChar = "." AndAlso Txt_PPN.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub Txt_PPH_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = ".") Then
            e.Handled = True
        End If

        ' Cegah lebih dari satu tanda titik (.)
        If e.KeyChar = "." AndAlso Txt_PPN.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub


    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then TextBox4.Focus()
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then TextBox5.Focus()
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox6.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    End Sub
End Class
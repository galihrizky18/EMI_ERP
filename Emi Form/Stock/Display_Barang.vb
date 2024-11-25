Imports System.Text.RegularExpressions

Public Class Display_Barang
    Dim arrcarib, arrcari2b, arrkatb, arrtanggalb, arrpilihblnb, arrbln1b, arrbln2b, arrdb As New ArrayList
    Dim arrcaribsf, arrcari2bsf, arrP1, arrP2, arrP3, arrP4 As New ArrayList
    Dim arrSO, arrGroup_Jenis, arrArea, arrRow, arrLevel, arrPosition, arrBay As New ArrayList
    Dim arrBL As New ArrayList

    Private Sub carib(ByVal semua As String, ByVal stockmin As String, ByVal cetak As String)
        If semua = "T" Then
            If CheckBox1.Checked = True Then
                If ComboBox1b.SelectedIndex = -1 Then
                    MessageBox.Show("Parameter 1 belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ComboBox1b.Focus() : Exit Sub
                ElseIf TextBox7b.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value 1 belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TextBox7b.Focus() : Exit Sub
                End If
            End If

            If CheckBox2.Checked = True Then
                If ComboBox7b.SelectedIndex = -1 Then
                    MessageBox.Show("Parameter 2 belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ComboBox7b.Focus() : Exit Sub
                ElseIf TextBox6b.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value 2 belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TextBox6b.Focus() : Exit Sub
                End If
            End If
        End If

        Try

            OpenConn()

            arrSO.Clear() : arrBL.Clear()
            SQL = "select Kode_Stock_Owner, flag_hide_stock, "
            SQL = SQL & "ISNULL((select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK' ), 'T') AS boleh_lihat_stock  "
            SQL = SQL & "from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and  x.kode_kota = '" & ComboKota.SelectedItem & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            arrBL.Add(True)
                        Else
                            arrBL.Add(False)
                        End If
                    Else
                        arrBL.Add(True)
                    End If
                    arrSO.Add(Dr("kode_stock_owner"))
                Loop
            End Using


            Dim no As Integer = 0
            Dim SF As String = ""

            SQL = "Select x.kode_stock_owner, x.kode_barang, x.Nama, x.satuan, x.good_stock, "
            SQL = SQL & "x.harga_beli, x.harga_jual, "
            SQL = SQL & "x.stock_minimum, x.Lemari, x.kode_supplier, "
            SQL = SQL & "x.bad_stock, x.kode_kategori, "

            For i As Integer = 1 To arrSO.Count - 1
                SQL = SQL & "isnull(("
                SQL = SQL & "select a.good_stock from barang a where a.kode_perusahaan = x.kode_perusahaan and "
                SQL = SQL & "a.kode_barang = x.kode_barang and a.kode_stock_owner = '" & arrSO.Item(i) & "' "
                SQL = SQL & "), 0) as Stock_" & Replace(arrSO.Item(i), " ", "") & ", "
            Next

            SQL = SQL & "x.aktif from barang x where "
            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and x.kode_stock_owner = '" & arrSO.Item(0) & "' "

            If semua = "T" Then
                If CheckBox1.Checked = True Then
                    SQL = SQL & " and " & arrcarib.Item(ComboBox1b.SelectedIndex) & " " & ComboBox1.Text & " '" & ComboBox3.Text & TextBox7b.Text & ComboBox4.Text & "' "
                    SF = SF & " and " & arrcaribsf.Item(ComboBox1b.SelectedIndex) & " " & ComboBox1.Text & " '" & arrP1.Item(ComboBox3.SelectedIndex) & TextBox7b.Text & arrP2.Item(ComboBox4.SelectedIndex) & "' "
                End If

                If CheckBox2.Checked = True Then
                    SQL = SQL & " and " & arrcari2b.Item(ComboBox7b.SelectedIndex) & " " & ComboBox2.Text & " '" & ComboBox5.Text & TextBox6b.Text & ComboBox6.Text & "' "
                    SF = SF & " and " & arrcari2bsf.Item(ComboBox7b.SelectedIndex) & " " & ComboBox2.Text & " '" & arrP3.Item(ComboBox5.SelectedIndex) & TextBox6b.Text & arrP4.Item(ComboBox6.SelectedIndex) & "' "
                End If
            End If

            SQL = SQL & "order by x.nama"
            If cetak = "T" Then
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read

                        no = no + 1
                    Loop
                End Using

            End If


            CloseConn()

        Catch ex As Exception
            CloseConn2()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Kosongb(ByVal seluruh As String)
        ComboBox10b.Items.Clear()
        ComboBox10b.Items.Add("--Semua--") : ComboBox10b.Items.Add("Y") : ComboBox10b.Items.Add("T")
        ComboBox10b.SelectedIndex = 1

        ComboBox1b.Enabled = True : ComboBox7b.Enabled = False
        TextBox7b.Enabled = True : TextBox6b.Enabled = False

        ComboBox1b.SelectedIndex = -1 : ComboBox7b.SelectedIndex = -1
        TextBox7b.Text = "" : TextBox6b.Text = ""

        ComboBox3.Items.Clear() : arrP1.Clear()
        ComboBox3.Items.Add("") : ComboBox3.Items.Add("%") : arrP1.Add("") : arrP1.Add("*") : ComboBox3.SelectedIndex = 1
        ComboBox4.Items.Clear() : arrP2.Clear()
        ComboBox4.Items.Add("") : ComboBox4.Items.Add("%") : arrP2.Add("") : arrP2.Add("*") : ComboBox4.SelectedIndex = 1

        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("like") : ComboBox1.Items.Add("=") : ComboBox1.SelectedIndex = 0

        ComboBox5.Items.Clear() : arrP3.Clear()
        ComboBox5.Items.Add("") : ComboBox5.Items.Add("%") : arrP3.Add("") : arrP3.Add("*") : ComboBox5.SelectedIndex = 0
        ComboBox6.Items.Clear() : arrP4.Clear()
        ComboBox6.Items.Add("") : ComboBox6.Items.Add("%") : arrP4.Add("") : arrP4.Add("*") : ComboBox6.SelectedIndex = 1

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("like") : ComboBox2.Items.Add("=") : ComboBox2.SelectedIndex = 0

        ComboBox1.Enabled = False : ComboBox2.Enabled = False
        ComboBox3.Enabled = False : ComboBox4.Enabled = False
        ComboBox5.Enabled = False : ComboBox6.Enabled = False
        'ComboBox3.SelectedIndex = 1 : ComboBox4.SelectedIndex = 1

        CheckBox1.Checked = False : CheckBox2.Checked = False ' ListView1b.Columns(5).Width = 0

        ComboBox1b.Items.Clear() : arrcarib.Clear() : arrcaribsf.Clear()
        ' ComboBox1b.Items.Add("Lokasi") : arrcarib.Add("x.kode_stock_owner") : arrcaribsf.Add("{barang.kode_stock_owner}")
        ComboBox1b.Items.Add("Kode Barang") : arrcarib.Add("Kode_Barang") ': arrcaribsf.Add("{barang.Kode_Barang}")
        ComboBox1b.Items.Add("Nama") : arrcarib.Add("Nama") ': arrcaribsf.Add("{barang.Nama}")
        ComboBox1b.Items.Add("Satuan Kecil") : arrcarib.Add("Satuan_Kecil") ': arrcaribsf.Add("{barang.Satuan}")
        ComboBox1b.Items.Add("Satuan Besar") : arrcarib.Add("Satuan_Besar") ': arrcaribsf.Add("{barang.Satuan}")
        'ComboBox1b.Items.Add("Harga Beli") : arrcarib.Add("x.Harga_Beli") : arrcaribsf.Add("{barang.Harga_Beli}")
        'ComboBox1b.Items.Add("Harga Jual") : arrcarib.Add("x.Harga_Jual") : arrcaribsf.Add("{barang.Harga_Jual}")
        'ComboBox1b.Items.Add("Stock") : arrcarib.Add("x.Good_Stock") : arrcaribsf.Add("{barang.Good_Stock}")
        'ComboBox1b.Items.Add("Stock Minimum") : arrcarib.Add("x.Stock_Minimum") : arrcaribsf.Add("{barang.Stock_Minimum}")
        'ComboBox1b.Items.Add("Lemari") : arrcarib.Add("Lemari") 
        'ComboBox1b.Items.Add("Kategori") : arrcarib.Add("x.kode_kategori") : arrcaribsf.Add("{barang.kode_kategori}")
        ComboBox1b.SelectedIndex = 0


        ComboBox7b.Items.Clear() : arrcari2b.Clear() : arrcari2bsf.Clear()
        'ComboBox7b.Items.Add("Lokasi") : arrcari2b.Add("x.kode_stock_owner") : arrcari2bsf.Add("{barang.kode_stock_owner}")
        ComboBox7b.Items.Add("Kode Barang") : arrcarib.Add("Kode_Barang") ': arrcaribsf.Add("{barang.Kode_Barang}")
        ComboBox7b.Items.Add("Nama") : arrcarib.Add("Nama") ': arrcaribsf.Add("{barang.Nama}")
        ComboBox7b.Items.Add("Satuan Kecil") : arrcarib.Add("Satuan_Kecil") ': arrcaribsf.Add("{barang.Satuan}")
        ComboBox7b.Items.Add("Satuan Besar") : arrcarib.Add("Satuan_Besar") ': arrcaribsf.Add("{barang.Satuan}")
        'ComboBox7b.Items.Add("Harga Beli") : arrcari2b.Add("x.Harga_Beli") : arrcari2bsf.Add("{barang.Harga_Beli}")
        'ComboBox7b.Items.Add("Harga Jual") : arrcari2b.Add("x.Harga_Jual") : arrcari2bsf.Add("{barang.Harga_Jual}")
        'ComboBox7b.Items.Add("Stock") : arrcari2b.Add("x.Good_Stock") : arrcari2bsf.Add("{barang.Good_Stock}")
        'ComboBox7b.Items.Add("Stock Minimum") : arrcari2b.Add("x.Stock_Minimum") : arrcari2bsf.Add("{barang.Stock_Minimum}")
        'ComboBox7b.Items.Add("Lemari") : arrcari2b.Add("Lemari")
        'ComboBox7b.Items.Add("Kategori") : arrcari2b.Add("x.kode_kategori") : arrcari2bsf.Add("{barang.kode_kategori}")


        arrSO.Clear()

        Try
            OpenConn()

            Dim ff_kd_kota As String = ""
            SQL = "Select kode_kota From stock_owner where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_stock_owner = '" & Lokasi & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    ff_kd_kota = dr("kode_kota")
                Else
                    ff_kd_kota = "X"
                End If
            End Using

            SQL = "Select kode_kota From kota where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_kota"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboKota.Items.Add(dr("kode_kota"))
                Loop
            End Using
            ComboKota.Text = ff_kd_kota

            ComboBox7.Items.Clear()
            ComboBox7.Items.Add("---SELURUH---")
            SQL = "select Kode_Stock_Owner from Stock_Owner_Gudang where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox7.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using
            ComboBox7.SelectedIndex = 0

            ComboBox8.Items.Clear() : arrGroup_Jenis.Clear()
            ComboBox8.Items.Add("---SELURUH---") : arrGroup_Jenis.Add("")
            SQL = "select Kode_Group_Jenis,Id_Group_Jenis from EMI_Group_Jenis "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_Group_Jenis"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox8.Items.Add(dr("Kode_Group_Jenis"))
                    arrGroup_Jenis.Add(dr("Id_Group_Jenis"))
                Loop
            End Using
            ComboBox8.SelectedIndex = 0

            ComboBox9.Items.Clear() : arrArea.Clear()
            ComboBox9.Items.Add("---SELURUH---") : arrArea.Add("")
            SQL = "select Id_WMS_Area,Kode_WMS_Area from EMI_WMS_Areas "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Area"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox9.Items.Add(dr("Kode_WMS_Area"))
                    arrArea.Add(dr("Id_WMS_Area"))
                Loop
            End Using
            ComboBox9.SelectedIndex = 0

            ComboBox10.Items.Clear() : arrRow.Clear()
            ComboBox10.Items.Add("---SELURUH---") : arrRow.Add("")
            SQL = "select Id_WMS_Row,Kode_WMS_Row from EMI_WMS_Row "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Row"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox10.Items.Add(dr("Kode_WMS_Row"))
                    arrRow.Add(dr("Id_WMS_Row"))
                Loop
            End Using
            ComboBox10.SelectedIndex = 0

            ComboBox13.Items.Clear() : arrBay.Clear()
            ComboBox13.Items.Add("---SELURUH---") : arrBay.Add("")
            SQL = "select Id_WMS_Bay,Kode_WMS_Bay from EMI_WMS_Bay "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Bay"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox13.Items.Add(dr("Kode_WMS_Bay"))
                    arrBay.Add(dr("Id_WMS_Bay"))
                Loop
            End Using
            ComboBox13.SelectedIndex = 0

            ComboBox11.Items.Clear() : arrLevel.Clear()
            ComboBox11.Items.Add("---SELURUH---") : arrLevel.Add("")
            SQL = "select Id_WMS_Level,Kode_WMS_Level from EMI_WMS_Level "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Level"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox11.Items.Add(dr("Kode_WMS_Level"))
                    arrLevel.Add(dr("Id_WMS_Level"))
                Loop
            End Using
            ComboBox11.SelectedIndex = 0

            ComboBox12.Items.Clear() : arrPosition.Clear()
            ComboBox12.Items.Add("---SELURUH---") : arrPosition.Add("")
            SQL = "select Id_WMS_Position,Kode_WMS_Position from EMI_WMS_Position "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Kode_WMS_Position"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox12.Items.Add(dr("Kode_WMS_Position"))
                    arrPosition.Add(dr("Id_WMS_Position"))
                Loop
            End Using
            ComboBox12.SelectedIndex = 0

            If CekButtonRole("Ganti_Lokasi_Display_Barang") = "T" Then
                ComboKota.Enabled = False
            Else
                ComboKota.Enabled = True
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        'If UserLevel = "1" Then
        '    ComboBox7.Enabled = True
        'Else
        '    ComboBox7.Enabled = False
        'End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then
            If ComboBox1b.SelectedIndex = -1 Then
                MessageBox.Show("Parameter 1 belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox1b.Focus() : Exit Sub
            ElseIf TextBox7b.Text.Trim.Length = 0 Then
                MessageBox.Show("Value 1 belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox7b.Focus() : Exit Sub
            End If
        End If

        If CheckBox2.Checked = True Then
            If ComboBox7b.SelectedIndex = -1 Then
                MessageBox.Show("Parameter 2 belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox7b.Focus() : Exit Sub
            ElseIf TextBox6b.Text.Trim.Length = 0 Then
                MessageBox.Show("Value 2 belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox6b.Focus() : Exit Sub
            End If
        End If

        If ComboBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Gudang belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox7.Focus() : Exit Sub
        ElseIf ComboBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("Jenis belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox8.Focus() : Exit Sub
        ElseIf ComboBox9.Text.Trim.Length = 0 Then
            MessageBox.Show("Area belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox9.Focus() : Exit Sub
        ElseIf ComboBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Row belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox10.Focus() : Exit Sub
        ElseIf ComboBox13.Text.Trim.Length = 0 Then
            MessageBox.Show("Bay belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox13.Focus() : Exit Sub
        ElseIf ComboBox11.Text.Trim.Length = 0 Then
            MessageBox.Show("Level belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox11.Focus() : Exit Sub
        ElseIf ComboBox12.Text.Trim.Length = 0 Then
            MessageBox.Show("Position belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox12.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            DataGridView1.Rows.Clear()
            SQL = "select *, dbo.get_hpp(serial_number) as HPP from Stock_Barang_SN_Per_Rak where kode_perusahaan = '" & KodePerusahaan & "' "
            If CheckBox1.Checked = True Then
                SQL = SQL & " and " & arrcarib.Item(ComboBox1b.SelectedIndex) & " " & ComboBox1.Text & " '" & ComboBox3.Text & TextBox7b.Text & ComboBox4.Text & "' "
            End If

            If CheckBox2.Checked = True Then
                SQL = SQL & " and " & arrcari2b.Item(ComboBox7b.SelectedIndex) & " " & ComboBox2.Text & " '" & ComboBox5.Text & TextBox6b.Text & ComboBox6.Text & "' "
            End If

            If ComboBox7.SelectedIndex = 0 Then
                SQL = SQL & ""
            Else
                SQL = SQL & " and Kode_Stock_Owner = '" & ComboBox7.Text & "' "
            End If

            If ComboBox8.SelectedIndex = 0 Then
                SQL = SQL & ""
            Else
                SQL = SQL & " and Kode_Group_Jenis = '" & ComboBox8.Text & "' "
            End If

            If ComboBox9.SelectedIndex = 0 Then
                SQL = SQL & ""
            Else
                SQL = SQL & " and Kode_WMS_Area = '" & ComboBox9.Text & "' "
            End If

            If ComboBox10.SelectedIndex = 0 Then
                SQL = SQL & ""
            Else
                SQL = SQL & " and Kode_WMS_Row = '" & ComboBox10.Text & "' "
            End If

            If ComboBox13.SelectedIndex = 0 Then
                SQL = SQL & ""
            Else
                SQL = SQL & " and Kode_WMS_Bay = '" & ComboBox13.Text & "' "
            End If

            If ComboBox11.SelectedIndex = 0 Then
                SQL = SQL & ""
            Else
                SQL = SQL & " and Kode_WMS_Level = '" & ComboBox11.Text & "' "
            End If

            If ComboBox12.SelectedIndex = 0 Then
                SQL = SQL & ""
            Else
                SQL = SQL & " and Kode_WMS_Position = '" & ComboBox12.Text & "' "
            End If

            SQL = SQL & "order by kode_stock_owner, nama, Labeling_WMS_Position"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        DataGridView1.Rows.Add(1)

                        Dim stock_tersedia As String = ""
                        'Format(.Rows(i).Item("Total_Stock_Tersedia_Satuan_Besar"), "N0")

                        DataGridView1.Rows.Item(i).Cells(0).Value = .Rows(i).Item("kode_stock_owner")
                        DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("kode_group_jenis")
                        DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("kode_barang")
                        DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("nama")
                        DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Gabung_Stock_Display_Tersedia")
                        DataGridView1.Rows.Item(i).Cells(5).Value = .Rows(i).Item("Gabung_Stock_Display_Inquiry")
                        DataGridView1.Rows.Item(i).Cells(6).Value = .Rows(i).Item("Gabung_Stock_Display_PO")
                        DataGridView1.Rows.Item(i).Cells(7).Value = "xxxxx"
                        DataGridView1.Rows.Item(i).Cells(8).Value = .Rows(i).Item("Labeling_WMS_Position")
                        DataGridView1.Rows.Item(i).Cells(9).Value = Format(.Rows(i).Item("HPP"), "N2")
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Perusahaan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Kosongb("Y")

        ComboBox1b.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1b.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox1.Focus()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5b.Click
        carib("T", "T", "T")
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7b.KeyPress
        If e.KeyChar = Chr(13) Then Button5_Click(TextBox7b, e)
    End Sub


    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6b.Click
        carib("Y", "T", "T")
    End Sub

    Private Sub ComboBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox7b.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            ComboBox1b.Enabled = True : TextBox7b.Enabled = True
            ComboBox1b.SelectedIndex = -1 : TextBox7b.Text = ""
            ComboBox1.Enabled = True : ComboBox3.Enabled = True : ComboBox4.Enabled = True
            ComboBox1.SelectedIndex = 0 : ComboBox3.SelectedIndex = 1 : ComboBox4.SelectedIndex = 1
        Else
            ComboBox1b.Enabled = False : TextBox7b.Enabled = False
            ComboBox1b.SelectedIndex = -1 : TextBox7b.Text = ""
            ComboBox1.Enabled = False : ComboBox3.Enabled = False : ComboBox4.Enabled = False
            ComboBox1.SelectedIndex = 0 : ComboBox3.SelectedIndex = 1 : ComboBox4.SelectedIndex = 1
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            ComboBox7b.Enabled = True : TextBox6b.Enabled = True
            ComboBox7b.SelectedIndex = -1 : TextBox6b.Text = ""
            ComboBox2.Enabled = True : ComboBox5.Enabled = True : ComboBox6.Enabled = True
            ComboBox2.SelectedIndex = 0 : ComboBox5.SelectedIndex = 0 : ComboBox6.SelectedIndex = 1
        Else
            ComboBox7b.Enabled = False : TextBox6b.Enabled = False
            ComboBox7b.SelectedIndex = -1 : TextBox6b.Text = ""
            ComboBox2.Enabled = False : ComboBox5.Enabled = False : ComboBox6.Enabled = False
            ComboBox2.SelectedIndex = 0 : ComboBox5.SelectedIndex = 0 : ComboBox6.SelectedIndex = 1
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        carib("T", "Y", "T")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        carib("Y", "Y", "T")
    End Sub

    Private Sub ComboBox1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox3.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            If CheckBox1.Checked = True Then
                ComboBox3.Enabled = True : ComboBox4.Enabled = True
                ComboBox3.SelectedIndex = 1 : ComboBox4.SelectedIndex = 1
            Else
                ComboBox3.Enabled = False : ComboBox4.Enabled = False
                ComboBox3.SelectedIndex = 1 : ComboBox4.SelectedIndex = 1
            End If
        Else
            ComboBox3.Enabled = False : ComboBox4.Enabled = False
            ComboBox3.SelectedIndex = 0 : ComboBox4.SelectedIndex = 0
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox5.Focus()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = 0 Then
            If CheckBox2.Checked = True Then
                ComboBox5.Enabled = True : ComboBox6.Enabled = True
                ComboBox5.SelectedIndex = 1 : ComboBox6.SelectedIndex = 1
            Else
                ComboBox5.Enabled = False : ComboBox6.Enabled = False
                ComboBox5.SelectedIndex = 1 : ComboBox6.SelectedIndex = 1
            End If
        Else
            ComboBox5.Enabled = False : ComboBox6.Enabled = False
            ComboBox5.SelectedIndex = 0 : ComboBox6.SelectedIndex = 0
        End If
    End Sub

    Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then TextBox7b.Focus()
    End Sub


    Private Sub ComboBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox6b.Focus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        carib("T", "T", "Y")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        carib("Y", "T", "Y")
    End Sub

End Class
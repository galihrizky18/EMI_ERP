Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class Transaksi_Formula_Binding
    Dim Jenis = "Transaksi_Binding_Formula"

    Dim lvNo As String
    Dim lvKdProduk As String
    Dim lvNmProduk As String
    Dim lvStockOwner As String
    Dim lvBindingFormula As String

    Dim cellNo As Integer = 0
    Dim cellKdProduk As Integer = 1
    Dim cellNmProduk As Integer = 2
    Dim cellStockOwner As Integer = 3
    Dim cellBindingFormula As Integer = 4

    Private Sub get_no_faktur()
        TxtFormulator_NoFaktur.Text = fTransFormulaBinding & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Transaksi_Formulator_Binding", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fTransFormulaBinding) + 4 & ")", fTransFormulaBinding & Format(tgl_skg, "MMyy"))
    End Sub

    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If

        Return hasil
    End Function

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        'lvNo = CekNothing(DgvBindingFormulator_BindingFormulator.Rows(No_Index).Cells(cellNo).Value)
        'lvKdProduk = CekNothing(DgvBindingFormulator_BindingFormulator.Rows(No_Index).Cells(cellKdProduk).Value)
        'lvNmProduk = CekNothing(DgvBindingFormulator_BindingFormulator.Rows(No_Index).Cells(cellNmProduk).Value)
        'lvStockOwner = CekNothing(DgvBindingFormulator_BindingFormulator.Rows(No_Index).Cells(cellStockOwner).Value)
        'lvBindingFormula = CekNothing(DgvBindingFormulator_BindingFormulator.Rows(No_Index).Cells(cellBindingFormula).Value)

    End Sub

    Private Sub Transaksi_Binding_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            LblFormula1.Text = Base_Language.Lang_Global_NoFormula
            LblFormula2.Text = Base_Language.Lang_Global_NoFormula
            LblKdBarang.Text = Base_Language.Lang_Global_NamaBarang
            LblQTY1.Text = Base_Language.Lang_Global_Hasil
            LblQTY2.Text = Base_Language.Lang_Global_Hasil
            Lbl_Judul.Text = Base_Language.Lang_TransFormulaBinding_Judul
            'Lbl_Customer.Text = Base_Language.Lang_Global_Customer
            'Lbl_NoInquiry.Text = Base_Language.Lang_Global_NoInquiry
            'Lbl_Tanggal.Text = Base_Language.Lang_Global_Tanggal
            'Lbl_NoFaktur.Text = Base_Language.Lang_Global_NoFaktur
            'Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            'DgvBindingFormulator_BindingFormulator.Columns(cellKdProduk).HeaderText = Base_Language.Lang_TransFormulaBinding_DGV_KodeProduk
            'DgvBindingFormulator_BindingFormulator.Columns(cellNmProduk).HeaderText = Base_Language.Lang_TransFormulaBinding_DGV_NamaProduk
            'DgvBindingFormulator_BindingFormulator.Columns(cellBindingFormula).HeaderText = Base_Language.Lang_TransFormulaBinding_DGV_BindingFormula



            ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 200, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 180, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 130, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Persentase & " (%)", 100, HorizontalAlignment.Center)

            ListView4.Columns.Add(Base_Language.Lang_Global_KodeBarang, 200, HorizontalAlignment.Left)
            ListView4.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
            ListView4.Columns.Add(Base_Language.Lang_Global_Jumlah, 180, HorizontalAlignment.Center)
            ListView4.Columns.Add(Base_Language.Lang_Global_Satuan, 130, HorizontalAlignment.Center)
            ListView4.Columns.Add(Base_Language.Lang_Global_Persentase & " (%)", 100, HorizontalAlignment.Center)

            ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 270, HorizontalAlignment.Left)
            ListView2.Location = New Point(130, 162)
            ListView2.Visible = False
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs)

        'JANGAN LUPA UNCOMMENT
        'SD_Pilih_Inquiry.asal = Jenis
        'SD_Pilih_Inquiry.filter_tambahan = " and a.flag_binding_formula is null "
        'SD_Pilih_Inquiry.ShowDialog()
        Get_Produk()
    End Sub


    Public Sub kosong()
        'DgvBindingFormulator_BindingFormulator.Rows.Clear()

        'Txt_NoInquiry.Text = ""
        'Txt_Customer.Text = ""
        Lbl_KdCustomer.Text = ""
        Lbl_NmCustomer.Text = ""
        Lbl_NoFormula.Text = ""

        TextBox1.Text = ""
        TextBox4.Text = ""

        TextBox2.Text = ""
        TextBox3.Text = ""
        ListView1.Items.Clear()

        ComboBox1.Items.Clear()
        TextBox5.Text = ""
        ListView4.Items.Clear()

        Try
            OpenConn()
            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_Produk()
        'DgvBindingFormulator_BindingFormulator.Rows.Clear()

        Try
            OpenConn()

            'SQL = "Select b.Kode_Barang_Inq, b.Nama_Inq, b.Kode_Stock_Owner "
            'SQL = SQL & "From Emi_Inquiry_Detail a, barang b "
            'SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan and "
            'SQL = SQL & "a.No_Faktur = '" & Txt_NoInquiry.Text & "' and a.kode_barang = b.kode_barang_inq and "
            'SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        DgvBindingFormulator_BindingFormulator.Rows.Add()
            '        DgvBindingFormulator_BindingFormulator.Rows(DgvBindingFormulator_BindingFormulator.Rows.Count - 1).Cells(cellKdProduk).Value = dr("Kode_Barang_Inq")
            '        DgvBindingFormulator_BindingFormulator.Rows(DgvBindingFormulator_BindingFormulator.Rows.Count - 1).Cells(cellNmProduk).Value = dr("Nama_Inq")
            '        DgvBindingFormulator_BindingFormulator.Rows(DgvBindingFormulator_BindingFormulator.Rows.Count - 1).Cells(cellStockOwner).Value = dr("Kode_Stock_Owner")
            '    Loop
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub DgvBindingFormulator_BindingFormulator_KeyDown(sender As Object, e As KeyEventArgs)
        'If e.KeyCode = Keys.F1 Then
        '    SD_Formulator.asal = Jenis
        '    SD_Formulator.Filter_NoInquiry = " and a.No_Inquiry = '" & Txt_NoInquiry.Text & "' "
        '    'NOTE --- pakai value dari gridview
        '    SD_Formulator.Filter_KdProduk = " and a.Kode_Barang = '" & DgvBindingFormulator_BindingFormulator.CurrentRow.Cells(cellKdProduk).Value & "' "
        '    SD_Formulator.ShowDialog()
        '    DgvBindingFormulator_BindingFormulator.CurrentRow.Cells(cellBindingFormula).Value = Lbl_NoFormula.Text
        'End If
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_NoFaktur + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Format(DateTimePicker1.Value, "yyyy-MM-dd") = Format(CDate("2000-01-01"), "yyyy-MM-dd") Then
            MessageBox.Show(Base_Language.Lang_Global_Tanggal + " " + Base_Language.Lang_Global_Belum_Diubah, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Focus()
            Exit Sub
        ElseIf ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_NoFormula + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf ComboBox1.Text.Trim = TextBox2.Text.Trim Then
            MessageBox.Show(Base_Language.Lang_Global_NoFormula + " " + " Sama Dengan Formula Aktif . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
            'ElseIf Txt_NoInquiry.Text.Trim.Length = 0 Then
            '    MessageBox.Show(Base_Language.Lang_Global_NoInquiry + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Txt_NoInquiry.Focus()
            '    Exit Sub
            'ElseIf DgvBindingFormulator_BindingFormulator.CurrentRow.Cells(cellKdProduk).Value = "" Then
            '    MessageBox.Show(Base_Language.Lang_TransFormulaBinding_DGV_KodeProduk + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'ElseIf DgvBindingFormulator_BindingFormulator.CurrentRow.Cells(cellNmProduk).Value = "" Then
            '    MessageBox.Show(Base_Language.Lang_TransFormulaBinding_DGV_NamaProduk + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'ElseIf DgvBindingFormulator_BindingFormulator.CurrentRow.Cells(cellBindingFormula).Value = "" Then
            '    MessageBox.Show(Base_Language.Lang_TransFormulaBinding_DGV_BindingFormula + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
        End If

        get_jam()

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            If TextBox2.Text = "" Then
                SQL = "select Kode_perusahaan from EMI_Transaksi_Formulator_Binding "
                SQL = SQL & "where Kode_Barang='" & TxtKode_barangInq.Text & "' and "
                SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Perubahan . . ! ! silahkan Ulangi Transaksi . ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            Else
                SQL = "select Kode_perusahaan from EMI_Transaksi_Formulator_Binding "
                SQL = SQL & "where Kode_Barang='" & TxtKode_barangInq.Text & "' and kode_formula='" & TextBox2.Text & "' and "
                SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and aktif='Y' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Perubahan . . ! ! silahkan Ulangi Transaksi . ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            End If


            ''Binding 
            SQL = "update EMI_Transaksi_Formulator_Binding set aktif='T' "
            SQL = SQL & "where Kode_Barang='" & TxtKode_barangInq.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' and aktif='Y' "
            ExecuteTrans(SQL)

            SQL = "Insert into EMI_Transaksi_Formulator_Binding ("
            SQL = SQL & "Kode_Perusahaan, No_faktur, "
            SQL = SQL & "Kode_Customer, No_Inquiry, "
            SQL = SQL & "Tanggal, Jam, UserID, Kode_barang, Kode_formula, Aktif) "
            SQL = SQL & "Values('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', "
            SQL = SQL & "NULL, NULL, "
            SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyy-MM-dd") & "', '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
            SQL = SQL & "'" & UserID & "','" & TxtKode_barangInq.Text & "', '" & ComboBox1.Text & "','Y')"
            ExecuteTrans(SQL)

            ''Update Flag di Inquiry

            'SQL = "select a.no_faktur from emi_inquiry a, emi_inquiry_detail b "
            'SQL = SQL & "where a.no_faktur=b.no_faktur and a.status is null "
            'SQL = SQL & "and b.kode_Perusahaan='" & KodePerusahaan & "' and b.Kode_Barang ='" & TextBox1.Text & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Dim no_fak As String = Dr("no_faktur")
            '        Dr.Close()
            '        SQL = "update Emi_Inquiry set Flag_Binding_Formula = 'Y' "
            '        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoInquiry.Text & "' "
            '        ExecuteTrans(SQL)
            '    End If
            'End Using


            ''Detail Binding
            'For index As Integer = 0 To DgvBindingFormulator_BindingFormulator.Rows.Count - 1
            '    Get_Isi_Listview(index)

            '    SQL = "Insert into EMI_Transaksi_Formulator_Binding_Detail ("
            '    SQL = SQL & "Kode_Perusahaan, No_Faktur, "
            '    SQL = SQL & "Kode_Produk, "
            '    SQL = SQL & "Binding_Formula ) "
            '    SQL = SQL & "Values('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', "
            '    SQL = SQL & "'" & lvKdProduk & "', "
            '    SQL = SQL & "'" & lvBindingFormula & "')"
            '    ExecuteTrans(SQL)

            '    ''Update Flag di Inquiry
            '    SQL = "update Emi_Inquiry set Flag_Binding_Formula = 'Y' "
            '    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoInquiry.Text & "' "
            '    ExecuteTrans(SQL)
            'Next

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

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub DgvBindingFormulator_BindingFormulator_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        ListView2.Items.Clear()
        Dim lv As New ListViewItem

        Try
            OpenConn()

            SQL = "select a.Kode_Barang, a.Nama from Barang a, EMI_Group_Jenis b where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Kode_Barang like '%" & TextBox1.Text & "%' and "
            SQL = SQL & "a.Id_Group_Jenis=b.Id_Group_Jenis "
            SQL = SQL & "and (Flag_Finished_Good='Y' or Flag_Sample='Y' or Flag_Tampil_Inquiry='Y'  or flag_semi_fg='Y' ) "
            SQL = SQL & "group by Kode_Barang,Nama order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Down Then
            ListView2.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox3.Text.Trim.Length = 0 Then
                ListView2.Visible = False : TextBox4.Focus() : Exit Sub
            End If
            TextBox1_Leave(TextBox3, e)
            ComboBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If
        If ListView2.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "select a.Kode_Barang, a.Nama, a.kode_Barang_inq from Barang a, EMI_Group_Jenis b where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Kode_Barang = '" & TextBox1.Text & "' and "
            SQL = SQL & "a.Id_Group_Jenis=b.Id_Group_Jenis "
            SQL = SQL & "and (Flag_Finished_Good='Y' or Flag_Sample='Y' or Flag_Tampil_Inquiry='Y'  or flag_semi_fg='Y' ) "
            SQL = SQL & "group by Kode_Barang,Nama, a.kode_Barang_inq order by Nama "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("Kode_Barang")
                    TextBox4.Text = Dr("Nama")
                    TxtKode_barangInq.Text = Dr("kode_Barang_inq")
                    Dr.Close()
                    ListView1.Items.Clear()
                    TextBox2.Text = ""
                    TextBox3.Text = ""

                    SQL = "Select c.Kode_Barang, d.nama, Persentase, c.Jumlah, c.satuan, b.Hasil,b.Satuan_Hasil, a.kode_formula "
                    SQL = SQL & "From EMI_Transaksi_Formulator_Binding a, Emi_Transaksi_Formulator b, EMI_Transaksi_Formulator_Detail_Bahan c, barang d "
                    SQL = SQL & "Where a.Kode_barang ='" & TxtKode_barangInq.Text & "' and a.aktif='Y' "
                    SQL = SQL & "And a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Formula = b.No_faktur And a.status Is null And b.status Is null "
                    SQL = SQL & "And b.Kode_Perusahaan =c.KOde_Perusahaan And b.no_faktur=c.NO_faktur And c.Kode_Barang=d.Kode_Barang And "
                    SQL = SQL & "c.Kode_Stock_Owner = d.Kode_Stock_Owner And c.Kode_Perusahaan = d.Kode_Perusahaan "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For index As Integer = 0 To .Rows.Count - 1

                                    If index = 0 Then
                                        TextBox2.Text = .Rows(index).Item("kode_formula")
                                        TextBox3.Text = .Rows(index).Item("Hasil") & " " & .Rows(index).Item("Satuan_Hasil")
                                    End If

                                    Dim lv As New ListViewItem
                                    lv = ListView1.Items.Add(.Rows(index).Item("Kode_Barang"))
                                    lv.SubItems.Add(.Rows(index).Item("nama"))
                                    lv.SubItems.Add(.Rows(index).Item("Jumlah"))
                                    lv.SubItems.Add(.Rows(index).Item("satuan"))
                                    lv.SubItems.Add(.Rows(index).Item("Persentase"))

                                Next

                            End If
                        End With
                    End Using

                    ComboBox1.Items.Clear()
                    TextBox5.Text = ""
                    ListView4.Items.Clear()
                    SQL = "select no_faktur from Emi_Transaksi_Formulator where "
                    SQL = SQL & " kode_barang='" & TxtKode_barangInq.Text & "' and status is null "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For index As Integer = 0 To .Rows.Count - 1
                                    ComboBox1.Items.Add(.Rows(index).Item("no_faktur"))
                                Next

                            End If
                        End With
                    End Using
                    ComboBox1.Focus()
                Else

                    TextBox1.Text = ""
                    TextBox4.Text = ""
                    TextBox1.Focus()
                End If
                ListView2.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView2.Items.Count = 0 Then Exit Sub
            ListView2.Focus()
        End If
    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        If ListView2.Focused = True Then Exit Sub
        TextBox3.Text = "" : TextBox4.Text = ""
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        ListView2.Items.Clear()
        Dim lv As New ListViewItem
        Try
            OpenConn()

            SQL = "select a.Kode_Barang, a.Nama from Barang a, EMI_Group_Jenis b where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Nama like '%" & TextBox4.Text & "%' and "
            SQL = SQL & "a.Id_Group_Jenis=b.Id_Group_Jenis "
            SQL = SQL & "and (Flag_Finished_Good='Y' or Flag_Sample='Y' or Flag_Tampil_Inquiry='Y'  or flag_semi_fg='Y' ) "
            SQL = SQL & "group by Kode_Barang,Nama order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub
        Dim kode As String = ListView2.FocusedItem.Text
        Dim nama As String = ListView2.FocusedItem.SubItems(1).Text
        TextBox1.Text = kode
        TextBox4.Text = nama
        ListView2.Visible = False
        TextBox1_Leave(ListView1, e)
        ComboBox1.Focus()
    End Sub

    Private Sub ListView2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub TextBox4_QueryAccessibilityHelp(sender As Object, e As QueryAccessibilityHelpEventArgs) Handles TextBox4.QueryAccessibilityHelp

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()
            TextBox5.Text = ""
            ListView4.Items.Clear()
            SQL = "Select c.Kode_Barang, d.nama, Persentase, c.Jumlah, c.satuan, b.Hasil, b.Satuan_Hasil, b.no_faktur as kode_formula "
            SQL = SQL & "From Emi_Transaksi_Formulator b, EMI_Transaksi_Formulator_Detail_Bahan c, barang d "
            SQL = SQL & "Where b.no_faktur ='" & ComboBox1.Text & "' And b.status Is null "
            SQL = SQL & "And b.Kode_Perusahaan =c.KOde_Perusahaan And b.no_faktur=c.NO_faktur And c.Kode_Barang=d.Kode_Barang And "
            SQL = SQL & "c.Kode_Stock_Owner = d.Kode_Stock_Owner And c.Kode_Perusahaan = d.Kode_Perusahaan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For index As Integer = 0 To .Rows.Count - 1

                            If index = 0 Then
                                TextBox5.Text = .Rows(index).Item("Hasil") & " " & .Rows(index).Item("Satuan_Hasil")
                            End If

                            Dim lv As New ListViewItem
                            lv = ListView4.Items.Add(.Rows(index).Item("Kode_Barang"))
                            lv.SubItems.Add(.Rows(index).Item("nama"))
                            lv.SubItems.Add(.Rows(index).Item("Jumlah"))
                            lv.SubItems.Add(.Rows(index).Item("satuan"))
                            lv.SubItems.Add(.Rows(index).Item("Persentase"))

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
End Class
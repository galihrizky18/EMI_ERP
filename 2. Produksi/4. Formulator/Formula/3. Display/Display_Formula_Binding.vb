Imports Excel = Microsoft.Office.Interop.Excel

Public Class Display_Formula_Binding
    Dim Jenis = "Transaksi_Binding_Formula"

    Dim arrCmbKb, arrCmbKK, arrParam As New ArrayList

    Dim Lv_Kd_Produk, Lv_Kd_Barang, Lv_Nm_Produk, Lv_Kd_Formula, Lv_Tgl_Formula, Lv_Jumlah, Lv_Satuan As String

    Dim item_Kd_Produk As Integer = 0
    Dim item_Kd_Barang As Integer = 1
    Dim item_Nm_Produk As Integer = 2
    Dim item_Kd_Formula As Integer = 3
    Dim item_Tgl_Formula As Integer = 4
    Dim item_Jumlah As Integer = 5
    Dim item_Satuan As Integer = 6


    'Private Sub get_no_faktur()
    '    TxtFormulator_NoFaktur.Text = fTransFormulaBinding & Format(tgl_skg, "MMyy") & "-" &
    '                         General_Class.Get_Last_Number2("Emi_Transaksi_Formulator_Binding", "No_Faktur", 5,
    '                         "Kode_perusahaan", KodePerusahaan,
    '                         "And", "substring(no_Faktur, 1, " & Len(fTransFormulaBinding) + 4 & ")", fTransFormulaBinding & Format(tgl_skg, "MMyy"))
    'End Sub

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

            ' LblFormula1.Text = Base_Language.Lang_Global_NoFormula
            ' LblFormula2.Text = Base_Language.Lang_Global_NoFormula
            LblKdBarang.Text = Base_Language.Lang_Global_NamaBarang
            '  LblQTY1.Text = Base_Language.Lang_Global_Hasil
            '    LblQTY2.Text = Base_Language.Lang_Global_Hasil
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

            ListView1.Columns.Add("Kode Produk", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("Nama Produk", 250, HorizontalAlignment.Left)
            ListView1.Columns.Add("Kode Formula", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("Tanggal Formula", 120, HorizontalAlignment.Center)
            ListView1.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
            ListView1.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
            ListView1.View = View.Details


            'ListView4.Columns.Add(Base_Language.Lang_Global_KodeBarang, 170, HorizontalAlignment.Left)
            'ListView4.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
            'ListView4.Columns.Add(Base_Language.Lang_Global_Jumlah, 180, HorizontalAlignment.Center)
            'ListView4.Columns.Add(Base_Language.Lang_Global_Satuan, 150, HorizontalAlignment.Center)
            'ListView4.Columns.Add(Base_Language.Lang_Global_Persentase & " (%)", 150, HorizontalAlignment.Center)


            'LvwPackaging.Columns.Add(Base_Language.Lang_Global_KodeBarang, 170, HorizontalAlignment.Left)
            'LvwPackaging.Columns.Add(Base_Language.Lang_Global_NamaBarang, 350, HorizontalAlignment.Left)
            'LvwPackaging.Columns.Add(Base_Language.Lang_Global_Jumlah, 180, HorizontalAlignment.Center)
            'LvwPackaging.Columns.Add(Base_Language.Lang_Global_Satuan, 150, HorizontalAlignment.Center)


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


    Public Sub kosong()
        'DgvBindingFormulator_BindingFormulator.Rows.Clear()

        'Txt_NoInquiry.Text = ""
        'Txt_Customer.Text = ""
        Lbl_KdCustomer.Text = ""
        Lbl_NmCustomer.Text = ""
        Lbl_NoFormula.Text = ""

        txtValParamter.Text = ""
        TextBox4.Text = ""

        '   TextBox2.Text = ""
        '    TextBox3.Text = ""
        ListView1.Items.Clear()

        '  ComboBox1.Items.Clear()
        ' TextBox5.Text = ""
        'ListView4.Items.Clear()

        Dgv_Detail_Formula.Rows.Clear()
        Dgv_Detail_Packaging.Rows.Clear()

        Try
            OpenConn()
            '   get_no_faktur()


            cmbKategori_Besar.Items.Clear() : arrCmbKb.Clear() : cmbKategori_Kecil.Items.Clear() : arrCmbKK.Clear() : arrParam.Clear()
            cmbKategori_Besar.Items.Add("----- Seluruh -----") : arrCmbKb.Add("seluruh")

            SQL = "select kode_kategori_besar, keterangan from kategori_besar where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cmbKategori_Besar.Items.Add(Dr("keterangan")) : arrCmbKb.Add(Dr("kode_kategori_besar"))
                Loop
            End Using
            cmbKategori_Besar.SelectedIndex = 0

            cmbParamter.Items.Clear() : arrParam.Clear()
            cmbParamter.Items.Add("Kode Produk") : arrParam.Add("a.Kode_Barang_inq")
            cmbParamter.Items.Add("Kode Barang") : arrParam.Add("a.kode_barang")
            cmbParamter.Items.Add("Nama Barang") : arrParam.Add("a.nama")
            cmbParamter.Items.Add("Kode Formula") : arrParam.Add("c.kode_formula")
            cmbParamter.SelectedIndex = 0



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_Produk()


        Try
            OpenConn()


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

    'Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
    '    If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
    '        MessageBox.Show(Base_Language.Lang_Global_NoFaktur + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    ElseIf Format(DateTimePicker1.Value, "yyyy-MM-dd") = Format(CDate("2000-01-01"), "yyyy-MM-dd") Then
    '        MessageBox.Show(Base_Language.Lang_Global_Tanggal + " " + Base_Language.Lang_Global_Belum_Diubah, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        DateTimePicker1.Focus()
    '        Exit Sub
    '    ElseIf ComboBox1.SelectedIndex = -1 Then
    '        MessageBox.Show(Base_Language.Lang_Global_NoFormula + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        ComboBox1.Focus()
    '        Exit Sub
    '    ElseIf ComboBox1.Text.Trim = TextBox2.Text.Trim Then
    '        MessageBox.Show(Base_Language.Lang_Global_NoFormula + " " + " Sama Dengan Formula Aktif . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        ComboBox1.Focus()
    '        Exit Sub
    '        'ElseIf Txt_NoInquiry.Text.Trim.Length = 0 Then
    '        '    MessageBox.Show(Base_Language.Lang_Global_NoInquiry + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '    Txt_NoInquiry.Focus()
    '        '    Exit Sub
    '        'ElseIf DgvBindingFormulator_BindingFormulator.CurrentRow.Cells(cellKdProduk).Value = "" Then
    '        '    MessageBox.Show(Base_Language.Lang_TransFormulaBinding_DGV_KodeProduk + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '    Exit Sub
    '        'ElseIf DgvBindingFormulator_BindingFormulator.CurrentRow.Cells(cellNmProduk).Value = "" Then
    '        '    MessageBox.Show(Base_Language.Lang_TransFormulaBinding_DGV_NamaProduk + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '    Exit Sub
    '        'ElseIf DgvBindingFormulator_BindingFormulator.CurrentRow.Cells(cellBindingFormula).Value = "" Then
    '        '    MessageBox.Show(Base_Language.Lang_TransFormulaBinding_DGV_BindingFormula + " " + Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '    Exit Sub
    '    End If

    '    get_jam()

    '    Try
    '        OpenConn()

    '        Cmd.Transaction = Cn.BeginTransaction

    '        get_no_faktur()

    '        If TextBox2.Text = "" Then
    '            SQL = "select Kode_perusahaan from EMI_Transaksi_Formulator_Binding "
    '            SQL = SQL & "where Kode_Barang='" & TxtKode_barangInq.Text & "' and "
    '            SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' "
    '            Using Dr = OpenTrans(SQL)
    '                If Dr.Read Then
    '                    Dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Terjadi Perubahan . . ! ! silahkan Ulangi Transaksi . ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            End Using
    '        Else
    '            SQL = "select Kode_perusahaan from EMI_Transaksi_Formulator_Binding "
    '            SQL = SQL & "where Kode_Barang='" & TxtKode_barangInq.Text & "' and kode_formula='" & TextBox2.Text & "' and "
    '            SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and aktif='Y' "
    '            Using Dr = OpenTrans(SQL)
    '                If Not Dr.Read Then
    '                    Dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Terjadi Perubahan . . ! ! silahkan Ulangi Transaksi . ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            End Using
    '        End If


    '        ''Binding 
    '        SQL = "update EMI_Transaksi_Formulator_Binding set aktif='T' "
    '        SQL = SQL & "where Kode_Barang='" & TxtKode_barangInq.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' and aktif='Y' "
    '        ExecuteTrans(SQL)

    '        SQL = "Insert into EMI_Transaksi_Formulator_Binding ("
    '        SQL = SQL & "Kode_Perusahaan, No_faktur, "
    '        SQL = SQL & "Kode_Customer, No_Inquiry, "
    '        SQL = SQL & "Tanggal, Jam, UserID, Kode_barang, Kode_formula, Aktif) "
    '        SQL = SQL & "Values('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', "
    '        SQL = SQL & "NULL, NULL, "
    '        SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyy-MM-dd") & "', '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
    '        SQL = SQL & "'" & UserID & "','" & TxtKode_barangInq.Text & "', '" & ComboBox1.Text & "','Y')"
    '        ExecuteTrans(SQL)

    '        ''Update Flag di Inquiry

    '        'SQL = "select a.no_faktur from emi_inquiry a, emi_inquiry_detail b "
    '        'SQL = SQL & "where a.no_faktur=b.no_faktur and a.status is null "
    '        'SQL = SQL & "and b.kode_Perusahaan='" & KodePerusahaan & "' and b.Kode_Barang ='" & TextBox1.Text & "'"
    '        'Using Dr = OpenTrans(SQL)
    '        '    If Dr.Read Then
    '        '        Dim no_fak As String = Dr("no_faktur")
    '        '        Dr.Close()
    '        '        SQL = "update Emi_Inquiry set Flag_Binding_Formula = 'Y' "
    '        '        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoInquiry.Text & "' "
    '        '        ExecuteTrans(SQL)
    '        '    End If
    '        'End Using


    '        ''Detail Binding
    '        'For index As Integer = 0 To DgvBindingFormulator_BindingFormulator.Rows.Count - 1
    '        '    Get_Isi_Listview(index)

    '        '    SQL = "Insert into EMI_Transaksi_Formulator_Binding_Detail ("
    '        '    SQL = SQL & "Kode_Perusahaan, No_Faktur, "
    '        '    SQL = SQL & "Kode_Produk, "
    '        '    SQL = SQL & "Binding_Formula ) "
    '        '    SQL = SQL & "Values('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', "
    '        '    SQL = SQL & "'" & lvKdProduk & "', "
    '        '    SQL = SQL & "'" & lvBindingFormula & "')"
    '        '    ExecuteTrans(SQL)

    '        '    ''Update Flag di Inquiry
    '        '    SQL = "update Emi_Inquiry set Flag_Binding_Formula = 'Y' "
    '        '    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoInquiry.Text & "' "
    '        '    ExecuteTrans(SQL)
    '        'Next

    '        Cmd.Transaction.Commit()
    '        CloseConn()
    '        MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    '    kosong()
    'End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub DgvBindingFormulator_BindingFormulator_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    End Sub

    'Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    '    If TextBox1.Text.Trim.Length = 0 Then
    '        ListView2.Visible = False : Exit Sub
    '    Else
    '        ListView2.Visible = True
    '    End If

    '    ListView2.Items.Clear()
    '    Dim lv As New ListViewItem

    '    Try
    '        OpenConn()

    '        SQL = "select a.Kode_Barang, a.Nama from Barang a, EMI_Group_Jenis b where "
    '        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "a.Kode_Barang like '%" & TextBox1.Text & "%' and "
    '        SQL = SQL & "a.Id_Group_Jenis=b.Id_Group_Jenis "
    '        SQL = SQL & "and (Flag_Finished_Good='Y' or Flag_Sample='Y' or Flag_Tampil_Inquiry='Y') "
    '        SQL = SQL & "group by Kode_Barang,Nama order by Nama"
    '        Using Dr = OpenTrans(SQL)
    '            Do While Dr.Read
    '                lv = ListView2.Items.Add(Dr("Kode_Barang"))
    '                lv.SubItems.Add(Dr("Nama"))
    '            Loop
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtValParamter.KeyDown
        If e.KeyCode = Keys.Down Then
            ListView2.Focus()
        End If
    End Sub


    Private Sub BtnExportExcel_Click(sender As Object, e As EventArgs) Handles BtnExportExcel.Click

        Dim xlApp As New Excel.Application()

        If xlApp Is Nothing Then
            MsgBox("Excel is not properly installed!", MsgBoxStyle.Critical)
            Return
        End If

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim format_akhir As String = Now.ToString("dd_MM_yyyy_HH_mm")
        Dim nama_file As String = "Laporan_Formula_" & format_akhir & ".xlsx"

        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet

        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("Sheet1")

        xlApp.ScreenUpdating = False
        xlApp.Calculation = Excel.XlCalculation.xlCalculationManual

        Try
            ' =======================
            ' 1. AMBIL DATA
            ' =======================
            OpenConn()

            SQL = "SELECT * FROM N_EMI_View_Laporan_Formula"
            Dim ds As DataSet = BindingTrans(SQL)

            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                MsgBox("Tidak ada data untuk di-export!", MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim dt As DataTable = ds.Tables(0)

            Dim rows As Integer = dt.Rows.Count
            Dim cols As Integer = dt.Columns.Count

            ' =======================
            ' 2. HEADER (FORMAT BAGUS)
            ' =======================
            For i As Integer = 0 To cols - 1
                Dim colName As String = dt.Columns(i).ColumnName

                ' Replace underscore → spasi
                colName = colName.Replace("_", " ")

                ' Proper Case (Kapital tiap kata)
                colName = Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(colName.ToLower())

                xlWorkSheet.Cells(1, i + 1).Value = colName
            Next

            ' =======================
            ' 3. ARRAY DATA
            ' =======================
            Dim dataArr(rows - 1, cols - 1) As Object

            For r As Integer = 0 To rows - 1
                For c As Integer = 0 To cols - 1
                    Dim value = dt.Rows(r)(c)
                    dataArr(r, c) = If(value IsNot Nothing AndAlso Not IsDBNull(value), value.ToString(), "")
                Next
            Next

            ' =======================
            ' 4. WRITE KE EXCEL
            ' =======================
            Dim startCell = xlWorkSheet.Cells(2, 1)
            Dim endCell = xlWorkSheet.Cells(rows + 1, cols)
            Dim writeRange = xlWorkSheet.Range(startCell, endCell)

            writeRange.Value = dataArr

            Dim lastRow As Integer = rows + 1

            ' =======================
            ' 5. FORMAT KOLOM
            ' =======================
            Dim rangeText As String =
            "A2:C" & lastRow & ";" &
            "D2:D" & lastRow & ";" &
            "G2:H" & lastRow & ";" &
            "J2:J" & lastRow

            Dim rangeNumber As String =
            "E2:E" & lastRow & ";" &
            "I2:I" & lastRow & ";" &
            "K2:K" & lastRow

            xlWorkSheet.Range(rangeText).NumberFormat = "@"
            xlWorkSheet.Range(rangeText).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

            xlWorkSheet.Range(rangeNumber).NumberFormat = "#,##0.00"
            xlWorkSheet.Range(rangeNumber).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight

            ' =======================
            ' 6. STYLE
            ' =======================
            xlWorkSheet.Cells.EntireColumn.AutoFit()

            Dim dataRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(lastRow, cols))
            With dataRange.Borders
                .LineStyle = Excel.XlLineStyle.xlContinuous
                .Weight = Excel.XlBorderWeight.xlThin
            End With

            ' Styling Header
            Dim headerRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, cols))

            With headerRange
                .Font.Bold = True
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .WrapText = True

                ' Background warna (biru muda)
                .Interior.Color = RGB(180, 198, 231)

                ' Optional: warna font
                .Font.Color = RGB(0, 0, 0)
            End With

            xlApp.ScreenUpdating = True
            xlApp.Calculation = Excel.XlCalculation.xlCalculationAutomatic

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
            xlApp.ScreenUpdating = True
            xlApp.Calculation = Excel.XlCalculation.xlCalculationAutomatic
            xlWorkBook.Close(False)
            xlApp.Quit()
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Exit Sub
        End Try

        ' =======================
        ' 7. SAVE FILE
        ' =======================
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx"
        saveFileDialog.FileName = nama_file

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim filePath As String = saveFileDialog.FileName
                xlWorkBook.SaveAs(filePath, Excel.XlFileFormat.xlOpenXMLWorkbook)
                MsgBox("Laporan Formula berhasil di-export!", MsgBoxStyle.Information)

                xlWorkBook.Close()
                xlApp.Quit()
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)

            Catch ex As Exception
                MsgBox("Error saat menyimpan file: " & ex.Message, MsgBoxStyle.Critical)
                xlWorkBook.Close(False)
                xlApp.Quit()
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)
            End Try
        Else
            xlWorkBook.Close(False)
            xlApp.Quit()
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
        End If

    End Sub

    'Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
    '    If e.KeyChar = Chr(13) Then
    '        If TextBox3.Text.Trim.Length = 0 Then
    '            ListView2.Visible = False : TextBox4.Focus() : Exit Sub
    '        End If
    '        '  TextBox1_Leave(TextBox3, e)
    '        '   ComboBox1.Focus()
    '    End If
    'End Sub

    'Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
    '    If TextBox1.Text.Trim.Length = 0 Then
    '        ListView2.Visible = False : Exit Sub
    '    Else
    '        ListView2.Visible = True
    '    End If
    '    If ListView2.Focused = True Then Exit Sub

    '    Try
    '        OpenConn()

    '        SQL = "select a.Kode_Barang, a.Nama, a.kode_Barang_inq from Barang a, EMI_Group_Jenis b where "
    '        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "a.Kode_Barang = '" & TextBox1.Text & "' and "
    '        SQL = SQL & "a.Id_Group_Jenis=b.Id_Group_Jenis "
    '        SQL = SQL & "and (Flag_Finished_Good='Y' or Flag_Sample='Y' or Flag_Tampil_Inquiry='Y') "
    '        SQL = SQL & "group by Kode_Barang,Nama, a.kode_Barang_inq order by Nama "
    '        Using Dr = OpenTrans(SQL)
    '            If Dr.Read Then
    '                TextBox1.Text = Dr("Kode_Barang")
    '                TextBox4.Text = Dr("Nama")
    '                TxtKode_barangInq.Text = Dr("kode_Barang_inq")
    '                Dr.Close()
    '                ListView1.Items.Clear()
    '                TextBox2.Text = ""
    '                TextBox3.Text = ""

    '                SQL = "Select c.Kode_Barang, d.nama, Persentase, c.Jumlah, c.satuan, b.Hasil,b.Satuan_Hasil, a.kode_formula "
    '                SQL = SQL & "From EMI_Transaksi_Formulator_Binding a, Emi_Transaksi_Formulator b, EMI_Transaksi_Formulator_Detail_Bahan c, barang d "
    '                SQL = SQL & "Where a.Kode_barang ='" & TxtKode_barangInq.Text & "' and a.aktif='Y' "
    '                SQL = SQL & "And a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Formula = b.No_faktur And a.status Is null And b.status Is null "
    '                SQL = SQL & "And b.Kode_Perusahaan =c.KOde_Perusahaan And b.no_faktur=c.NO_faktur And c.Kode_Barang=d.Kode_Barang And "
    '                SQL = SQL & "c.Kode_Stock_Owner = d.Kode_Stock_Owner And c.Kode_Perusahaan = d.Kode_Perusahaan "
    '                Using Ds = BindingTrans(SQL)
    '                    With Ds.Tables("MyTable")
    '                        If .Rows.Count <> 0 Then
    '                            For index As Integer = 0 To .Rows.Count - 1

    '                                If index = 0 Then
    '                                    TextBox2.Text = .Rows(index).Item("kode_formula")
    '                                    TextBox3.Text = .Rows(index).Item("Hasil") & " " & .Rows(index).Item("Satuan_Hasil")
    '                                End If

    '                                Dim lv As New ListViewItem
    '                                lv = ListView1.Items.Add(.Rows(index).Item("Kode_Barang"))
    '                                lv.SubItems.Add(.Rows(index).Item("nama"))
    '                                lv.SubItems.Add(.Rows(index).Item("Jumlah"))
    '                                lv.SubItems.Add(.Rows(index).Item("satuan"))
    '                                lv.SubItems.Add(.Rows(index).Item("Persentase"))

    '                            Next

    '                        End If
    '                    End With
    '                End Using

    '                ComboBox1.Items.Clear()
    '                '    TextBox5.Text = ""
    '                ListView4.Items.Clear()
    '                SQL = "select no_faktur from Emi_Transaksi_Formulator where "
    '                SQL = SQL & " kode_barang='" & TxtKode_barangInq.Text & "' and status is null "
    '                Using Ds = BindingTrans(SQL)
    '                    With Ds.Tables("MyTable")
    '                        If .Rows.Count <> 0 Then
    '                            For index As Integer = 0 To .Rows.Count - 1
    '                                ComboBox1.Items.Add(.Rows(index).Item("no_faktur"))
    '                            Next

    '                        End If
    '                    End With
    '                End Using
    '                ComboBox1.Focus()
    '            Else

    '                TextBox1.Text = ""
    '                TextBox4.Text = ""
    '                TextBox1.Focus()
    '            End If
    '            ListView2.Visible = False
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView2.Items.Count = 0 Then Exit Sub
            ListView2.Focus()
        End If
    End Sub

    'Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
    '    If ListView2.Focused = True Then Exit Sub
    '    TextBox3.Text = "" : TextBox4.Text = ""
    'End Sub

    'Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
    '    If TextBox4.Text.Trim.Length = 0 Then
    '        ListView2.Visible = False : Exit Sub
    '    Else
    '        ListView2.Visible = True
    '    End If


    '    Dim lv As New ListViewItem
    '    Try
    '        OpenConn()
    '        ListView2.Items.Clear()
    '        SQL = "select a.Kode_Barang, a.Nama from Barang a, EMI_Group_Jenis b where "
    '        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "a.Nama like '%" & TextBox4.Text & "%' and "
    '        SQL = SQL & "a.Id_Group_Jenis=b.Id_Group_Jenis "
    '        SQL = SQL & "and (Flag_Finished_Good='Y' or Flag_Sample='Y' or Flag_Tampil_Inquiry='Y') "
    '        SQL = SQL & "group by Kode_Barang,Nama order by Nama"
    '        Using Dr = OpenTrans(SQL)
    '            Do While Dr.Read
    '                lv = ListView2.Items.Add(Dr("Kode_Barang"))
    '                lv.SubItems.Add(Dr("Nama"))
    '            Loop
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub
        Dim kode As String = ListView2.FocusedItem.Text
        Dim nama As String = ListView2.FocusedItem.SubItems(1).Text
        txtValParamter.Text = kode
        TextBox4.Text = nama
        ListView2.Visible = False
        ' TextBox1_Leave(ListView1, e)
        'ComboBox1.Focus()
    End Sub

    Private Sub ListView2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub LblKdBarang_Click(sender As Object, e As EventArgs) Handles LblKdBarang.Click

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            OpenConn()
            Dgv_Detail_Formula.Rows.Clear()
            SQL = "select a.Kode_Barang,b.nama,a.Jumlah,a.Persentase,a.satuan "
            SQL = SQL & "from EMI_Transaksi_Formulator_Detail_Bahan a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang_Inq and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & ListView1.FocusedItem.SubItems(3).Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Detail_Formula.Rows.Add(1)
                            Dgv_Detail_Formula.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_Detail_Formula.Rows(i).Cells(1).Value = .Rows(i).Item("nama")
                            Dgv_Detail_Formula.Rows(i).Cells(2).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah"))), "N4")
                            Dgv_Detail_Formula.Rows(i).Cells(3).Value = .Rows(i).Item("satuan")
                            Dgv_Detail_Formula.Rows(i).Cells(4).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Persentase"))), "N2")
                        Next
                    End If
                End With
            End Using



            Dgv_Detail_Packaging.Rows.Clear()
            SQL = "select  a.Kode_Bahan,b.nama, b.Satuan,a.Jumlah_Bahan, a.Jumlah_Barang from Barang_Detail_Bahan_Penolong a, barang b where  "
            SQL = SQL & "a.Kode_Bahan = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Barang = '" & ListView1.FocusedItem.SubItems(0).Text & "' "
            SQL = SQL & "group by a.Kode_Bahan,b.nama, b.Satuan,a.Jumlah_Bahan, a.Jumlah_Barang, a.Jumlah_Barang "
            SQL = SQL & "order by b.nama "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Detail_Packaging.Rows.Add(1)
                            Dgv_Detail_Packaging.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Bahan")
                            Dgv_Detail_Packaging.Rows(i).Cells(1).Value = .Rows(i).Item("nama")
                            Dgv_Detail_Packaging.Rows(i).Cells(2).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah_Bahan"))), "N4")
                            Dgv_Detail_Packaging.Rows(i).Cells(3).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah_Barang"))), "N4")
                            Dgv_Detail_Packaging.Rows(i).Cells(4).Value = .Rows(i).Item("Satuan")


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

    Private Sub cmbKategori_Besar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbKategori_Besar.SelectedIndexChanged
        Try
            OpenConn()

            cmbKategori_Kecil.Items.Clear() : arrCmbKK.Clear()
            cmbKategori_Kecil.Items.Add("----- Seluruh -----") : arrCmbKb.Add("seluruh")

            SQL = "select kode_kategori_kecil from kategori_kecil where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            If cmbKategori_Besar.SelectedIndex <> 0 Then
                SQL = SQL & "and kode_kategori_besar = '" & cmbKategori_Besar.Text & "' "
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cmbKategori_Kecil.Items.Add(Dr("kode_kategori_kecil")) : arrCmbKK.Add(Dr("kode_kategori_kecil"))
                Loop
            End Using

            If cmbKategori_Besar.SelectedIndex = 0 Then
                cmbKategori_Kecil.SelectedIndex = 0
            End If
            cmbKategori_Kecil.SelectedIndex = 0
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Button1_Click(cmbKategori_Besar, e)
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cmbKategori_Besar.SelectedIndex = -1 Or cmbKategori_Kecil.SelectedIndex = -1 Then
            Exit Sub
        End If
        Try
            OpenConn()
            ListView1.Items.Clear()
            'SQL = "select a.Kode_Barang_inq as Kode_Barang, a.Nama, "
            'SQL = SQL & "isnull((select kode_formula from EMI_Transaksi_Formulator_Binding x where "
            'SQL = SQL & "a.Kode_Perusahaan = x.Kode_Perusahaan and a.Kode_Barang_inq = x.Kode_Barang and x.status is null and x.aktif='Y'), NULL) as Kode_Formula, "
            'SQL = SQL & "isnull((select y.hasil from EMI_Transaksi_Formulator_Binding x, Emi_Transaksi_Formulator y "
            'SQL = SQL & "where a.Kode_Perusahaan = x.Kode_Perusahaan and a.Kode_Barang_inq = x.Kode_Barang "
            'SQL = SQL & "and x.kode_perusahaan = y.kode_perusahaan and x.Kode_Formula = y.No_Faktur and x.status is null and x.aktif='Y'), NULL) as Jumlah_Satuan, "
            'SQL = SQL & "isnull((select y.Satuan_Hasil from EMI_Transaksi_Formulator_Binding x, Emi_Transaksi_Formulator y "
            'SQL = SQL & "where a.Kode_Perusahaan = x.Kode_Perusahaan and a.Kode_Barang_inq = x.Kode_Barang "
            'SQL = SQL & "and x.kode_perusahaan = y.kode_perusahaan and x.Kode_Formula = y.No_Faktur and x.status is null and x.aktif='Y' ), NULL) as satuan "
            'SQL = SQL & "from Barang a, EMI_Group_Jenis b where a.kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "a.Id_Group_Jenis=b.Id_Group_Jenis "
            'SQL = SQL & "and (Flag_Finished_Good='Y' or Flag_Sample='Y' or Flag_Tampil_Inquiry='Y') "

            SQL = ";with cte as (select isnull((select top 1 c.No_Faktur "
            SQL = SQL & "from N_EMI_Transaksi_Formulator_Binding a "
            SQL = SQL & "inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "inner join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Formulator = c.No_Faktur and c.Status is null "
            SQL = SQL & "where a.Status is NULL "
            SQL = SQL & "and a.Flag_Validasi_Main = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = x.kode_perusahaan "
            SQL = SQL & "and a.Kode_Barang = x.kode_barang_inq "
            SQL = SQL & "order by a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC),'') as kode_formula, x.kode_barang_inq, x.kode_perusahaan "
            SQL = SQL & "from barang x, emI_group_jenis y where x.kode_perusahaan=y.kode_perusahaan and x.id_group_jenis=y.id_group_jenis and (y.Flag_Finished_Good='Y' or y.Flag_Sample='Y' or y.Flag_Tampil_Inquiry='Y') "
            SQL = SQL & ")"
            SQL = SQL & "select a.Kode_Barang_inq as Kode_Barang_Product, a.kode_barang, a.Nama, c.kode_formula, d.Tanggal as Tanggal_Input_Formula, d.hasil, d.Satuan_Hasil "
            SQL = SQL & "from Barang a "
            SQL = SQL & "inner join EMI_Group_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "left join cte c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Barang_Inq = c.kode_barang_inq   "
            SQL = SQL & "left join Emi_Transaksi_Formulator d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Formula = d.No_Faktur "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and (b.Flag_Finished_Good='Y' or b.Flag_Sample='Y' or b.Flag_Tampil_Inquiry='Y') "

            If cmbKategori_Besar.SelectedIndex <> 0 Then
                SQL = SQL & "and a.kode_kategori_besar = '" & cmbKategori_Besar.Text & "' "
            End If

            If cmbKategori_Kecil.SelectedIndex <> 0 Then
                SQL = SQL & "and a.kode_kategori_kecil = '" & cmbKategori_Kecil.Text & "' "
            End If

            If cmbParamter.SelectedIndex <> -1 And txtValParamter.Text.Trim.Length <> 0 Then
                SQL = SQL & " and " & arrParam.Item(cmbParamter.SelectedIndex) & "  like '%" & txtValParamter.Text & "%' "
            End If

            SQL = SQL & "group by a.kode_perusahaan,a.Kode_Barang_inq,a.Nama,a.satuan,a.kode_barang, c.kode_formula, d.Tanggal, d.hasil, d.Satuan_Hasil "
            SQL = SQL & "order by Nama "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = ListView1.Items.Add(Dr("Kode_Barang_Product"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("Nama"))

                    If General_Class.CekNULL(Dr("kode_formula")) = "" Then
                        lv.SubItems.Add("-")
                        lv.SubItems.Add("-")
                        lv.SubItems.Add("-")
                        lv.SubItems.Add("-")
                    Else
                        lv.SubItems.Add(Dr("kode_formula"))
                        lv.SubItems.Add(Format(Dr("Tanggal_Input_Formula"), "dd MMM yyyy"))
                        lv.SubItems.Add(Dr("hasil"))
                        lv.SubItems.Add(Dr("Satuan_Hasil"))
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

    Private Sub cmbKategori_Kecil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbKategori_Kecil.SelectedIndexChanged
        Button1_Click(cmbKategori_Besar, e)
    End Sub






    'Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If ComboBox1.SelectedIndex = -1 Then
    '        Exit Sub
    '    End If

    '    Try
    '        OpenConn()
    '        'TextBox5.Text = ""
    '        ListView4.Items.Clear()
    '        SQL = "Select c.Kode_Barang, d.nama, Persentase, c.Jumlah, c.satuan, b.Hasil, b.Satuan_Hasil, b.no_faktur as kode_formula "
    '        SQL = SQL & "From Emi_Transaksi_Formulator b, EMI_Transaksi_Formulator_Detail_Bahan c, barang d "
    '        SQL = SQL & "Where b.no_faktur ='" & ComboBox1.Text & "' And b.status Is null "
    '        SQL = SQL & "And b.Kode_Perusahaan =c.KOde_Perusahaan And b.no_faktur=c.NO_faktur And c.Kode_Barang=d.Kode_Barang And "
    '        SQL = SQL & "c.Kode_Stock_Owner = d.Kode_Stock_Owner And c.Kode_Perusahaan = d.Kode_Perusahaan "
    '        Using Ds = BindingTrans(SQL)
    '            With Ds.Tables("MyTable")
    '                If .Rows.Count <> 0 Then
    '                    For index As Integer = 0 To .Rows.Count - 1

    '                        If index = 0 Then
    '                            'TextBox5.Text = .Rows(index).Item("Hasil") & " " & .Rows(index).Item("Satuan_Hasil")
    '                        End If

    '                        Dim lv As New ListViewItem
    '                        lv = ListView4.Items.Add(.Rows(index).Item("Kode_Barang"))
    '                        lv.SubItems.Add(.Rows(index).Item("nama"))
    '                        lv.SubItems.Add(.Rows(index).Item("Jumlah"))
    '                        lv.SubItems.Add(.Rows(index).Item("satuan"))
    '                        lv.SubItems.Add(.Rows(index).Item("Persentase"))

    '                    Next

    '                End If
    '            End With
    '        End Using



    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub
End Class
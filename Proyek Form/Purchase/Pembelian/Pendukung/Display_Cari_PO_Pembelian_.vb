Public Class Display_Cari_po_pembelian_
    Dim Arr1, Arr2, Arr3 As New ArrayList
    Dim Batal As Color = Color.Black
    Dim CrDoc As Object

    Public Sub laporan(ByVal formula As String, ByVal cr_title As String, ByVal form_title As String)
        CrDoc.RecordSelectionFormula = formula
        CrDoc.SummaryInfo.ReportTitle = cr_title
        A_Place_For_Printing.Text = form_title
    End Sub

    Private Sub cetak()
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select kode_perusahaan from detail_permintaan_keluar where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New Faktur_PO_Toko
                    kertas = "Faktur"

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabaseTetangga)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{detail_permintaan_keluar.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_permintaan_keluar.no_faktur} = '" & ListView1.FocusedItem.Text & "'"
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterName
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)
                End If
            End Using

            CloseConn()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Display_Data_Penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns.Clear()
        DataGridView1.Rows.Clear()

        ListView1.Columns.Add("No Faktur", 110, HorizontalAlignment.Center)
        ListView1.Columns.Add("No Nota", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Tanggal", 70, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jam", 55, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Supplier", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Supplier", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("User ID", 120, HorizontalAlignment.Left)
        ListView1.View = View.Details

        Button1_Click(Me, e)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'If CheckBox1.Checked = False And CheckBox2.Checked = False Then
        '    MessageBox.Show("Pilih terlebih dahulu parameter pencarian data . . ! !", Judul)
        '    CheckBox1.Focus() : Exit Sub
        'End If

        'If CheckBox1.Checked Then
        '    If ComboBox1.SelectedIndex = -1 Then
        '        MessageBox.Show("Parameter pencarian per tanggal harus diisi . . ! !", Judul)
        '        ComboBox1.Focus() : Exit Sub
        '    ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
        '        MessageBox.Show("Periode I tidak boleh lebih dari periode II . . ! !", Judul)
        '        DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        '        Exit Sub
        '    End If
        'End If
        'If CheckBox2.Checked Then
        '    If ComboBox2.SelectedIndex = -1 Then
        '        MessageBox.Show("Parameter lain harus diisi . . ! !", Judul)
        '        ComboBox2.Focus() : Exit Sub
        '    ElseIf TextBox1.Text.Trim.Length = 0 Then
        '        MessageBox.Show("Value parameter lain harus diisi . . ! !", Judul)
        '        TextBox1.Focus() : Exit Sub
        '    End If
        'End If

        SQL = "select  a.no_faktur, b.no_nota, a.tanggal, a.jam, b.kode_supplier, c.nama, a.userid "
        SQL = SQL & "from Barang_Masuk_Proyek a,PO_Pembelian_Proyek b,suppliers c "
        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur and "
        SQL = SQL & "a.Status is null and b.Status is null and "
        SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Supplier = c.Kode_Supplier and "
        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "a.lokasi = '" & Pembelian_Pry.ComboBox4.Text & "' and a.flag_pakai is null "
        SQL = SQL & "order by a.tanggal + a.jam asc "
        Try
            OpenConn()

            ListView1.Items.Clear()
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem

                        Lvw = ListView1.Items.Add(.Rows(i).Item("no_faktur"))
                        Lvw.SubItems.Add(.Rows(i).Item("No_nota"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("jam"))
                        Lvw.SubItems.Add(.Rows(i).Item("Kode_Supplier"))
                        Lvw.SubItems.Add(.Rows(i).Item("nama"))
                        Lvw.SubItems.Add(.Rows(i).Item("Userid"))
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

    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        If ListView1.Items.Count = 0 Then Exit Sub

        Try

            OpenConn()

            DataGridView1.Rows.Clear()
            Dim no As Integer = 0

            SQL = "Select a.urut, a.kode_stock_owner, a.Kode_barang, b.nama, a.jumlah, b.satuan "
            SQL = SQL & "from Barang_Masuk_Proyek_Detail a, barang_proyek b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and a.kode_barang = b.kode_barang and "
            SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.no_faktur = '" & ListView1.FocusedItem.Text & "' order by urut"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    DataGridView1.Rows.Add(1)

                    DataGridView1.Rows.Item(no).Cells(0).Value = Dr("kode_barang")
                    DataGridView1.Rows.Item(no).Cells(1).Value = Dr("nama")
                    DataGridView1.Rows.Item(no).Cells(2).Value = Format(Dr("jumlah"), "N0")
                    DataGridView1.Rows.Item(no).Cells(3).Value = Dr("satuan")

                    no = no + 1
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Pembelian_Pry.cari_po(ListView1.FocusedItem.Text)

        Me.Close()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Display_Data_Transfer_Stock_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

End Class
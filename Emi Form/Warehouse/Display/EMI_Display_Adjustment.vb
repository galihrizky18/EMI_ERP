Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Azure.Storage.Internal

Public Class EMI_Display_Adjustment
    Public asal As String
    Public Filter_Tambahan, Filter_NoInquiry, Filter_KdProduk As String
    Dim arrCari As New ArrayList

    Private Sub EMI_Display_Transfer_Cost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Lv_DataAdjustment.Columns.Clear()
        Lv_DataAdjustment.Columns.Add("Kode Adjustment", 150, HorizontalAlignment.Left)
        Lv_DataAdjustment.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_DataAdjustment.Columns.Add("Jam", 120, HorizontalAlignment.Center)
        Lv_DataAdjustment.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_DataAdjustment.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_DataAdjustment.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
        Lv_DataAdjustment.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_DataAdjustment.Columns.Add("Satuan", 130, HorizontalAlignment.Center)
        Lv_DataAdjustment.Columns.Add("Total Barang", 130, HorizontalAlignment.Right)
        Lv_DataAdjustment.Columns.Add("Satuan Barang", 130, HorizontalAlignment.Center)
        Lv_DataAdjustment.Columns.Add("Keterangan", 150, HorizontalAlignment.Left)
        Lv_DataAdjustment.Columns.Add("UserID", 150, HorizontalAlignment.Center)
        Lv_DataAdjustment.Columns.Add("Kode Voucher", 150, HorizontalAlignment.Left)
        Lv_DataAdjustment.Columns.Add("Grand", 150, HorizontalAlignment.Right)
        Lv_DataAdjustment.Columns.Add("Tgl Input", 150, HorizontalAlignment.Center)
        Lv_DataAdjustment.Columns.Add("Jumlah Bags", 150, HorizontalAlignment.Right)
        Lv_DataAdjustment.View = View.Details

        Lv_DataDetailAdjustment.Columns.Clear()
        Lv_DataDetailAdjustment.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_DataDetailAdjustment.Columns.Add("Nama Barang", 470, HorizontalAlignment.Left)
        Lv_DataDetailAdjustment.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        Lv_DataDetailAdjustment.Columns.Add("Jumlah Bags", 150, HorizontalAlignment.Right)
        Lv_DataDetailAdjustment.View = View.Details

        kosong()
    End Sub

    Private Sub kosong()

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            'Base_Language.Get_Languages(Bahasa_Pilihan, "QC_Formula")

            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSO_Asal.Items.Add(dr("Keterangan"))
                Loop
            End Using

            Lv_DataAdjustment.Items.Clear() : Lv_DataDetailAdjustment.Items.Clear()
            SQL = "select a.Kode_Adjustment, a.Tanggal, a.jam, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, a.Jumlah, a.satuan, "
            SQL = SQL & "a.Total_Barang, a.Satuan_Barang, a.Keterangan, a.UserID, a.Kode_Voucher, a.Grand, a.Tgl_Input, a.Jumlah_Bags "
            SQL = SQL & "from EMI_Adjustment a, barang b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and a.status is null "
            SQL = SQL & "order by a.Tanggal desc "
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_DataAdjustment.Items.Add(.Rows(i).Item("Kode_Adjustment"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            Lvw.SubItems.Add(.Rows(i).Item("Jam"))
                            Lvw.SubItems.Add(.Rows(i).Item("Kode_Stock_Owner"))
                            Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang"))
                            Lvw.SubItems.Add(.Rows(i).Item("Nama_Barang"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah"), "N2"))
                            Lvw.SubItems.Add(.Rows(i).Item("Satuan"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Total_Barang"), "N2"))
                            Lvw.SubItems.Add(.Rows(i).Item("Satuan_Barang"))
                            Lvw.SubItems.Add(.Rows(i).Item("Keterangan"))
                            Lvw.SubItems.Add(.Rows(i).Item("UserID"))
                            If General_Class.CekNULL(.Rows(i).Item("Kode_Voucher")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("Kode_Voucher"))
                            End If
                            If General_Class.CekNULL(.Rows(i).Item("Grand")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Grand"), "N2"))
                            End If
                            If General_Class.CekNULL(.Rows(i).Item("Tgl_Input")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Input"), "dd MMM yyyy"))
                            End If
                            If General_Class.CekNULL(.Rows(i).Item("Jumlah_Bags")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah_Bags"), "N2"))
                            End If
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

        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False

        ComboBox1.Items.Clear() : arrCari.Clear()
        ComboBox1.Items.Add("No Faktur") : arrCari.Add("a.Kode_Adjustment")
        ComboBox1.Items.Add("Kode Barang") : arrCari.Add("a.Kode_Barang")
        ComboBox1.Items.Add("Nama Barang") : arrCari.Add("b.Nama")
        ComboBox1.Items.Add("Satuan") : arrCari.Add("a.Satuan")
        ComboBox1.Items.Add("Keterangan") : arrCari.Add("a.Keterangan")
    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs)
        kosong()
    End Sub

    Private Sub EMI_Display_Transfer_Cost_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            Btn_Cari_Click(CheckBox1, e)
        End If
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            CheckBox1.Focus() : Exit Sub
        ElseIf CmbSO_Asal.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Harus harus diisi!", Judul)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        If CheckBox2.Checked = True Then
            If DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                Exit Sub
            End If
        End If

        If CheckBox3.Checked = True Then
            If ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                ComboBox1.Focus() : Exit Sub
            ElseIf TextBox1.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                TextBox1.Focus() : Exit Sub
            End If

        End If

        Try
            OpenConn()

            Lv_DataAdjustment.Items.Clear() : Lv_DataDetailAdjustment.Items.Clear()
            'SQL = "select a.No_Faktur,a.Tanggal,a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.Jumlah,a.Satuan,a.Keterangan "
            'SQL = SQL & "from EMI_Transfer_Cost a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and a.Status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = "select a.Kode_Adjustment, a.Tanggal, a.jam, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, a.Jumlah, a.satuan, "
            SQL = SQL & "a.Total_Barang, a.Satuan_Barang, a.Keterangan, a.UserID, a.Kode_Voucher, a.Grand, a.Tgl_Input, a.Jumlah_Bags "
            SQL = SQL & "from EMI_Adjustment a, barang b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and a.status is null "
            SQL = SQL & "and a.Kode_Stock_Owner = '" & CmbSO_Asal.Text & "' "

            If CheckBox1.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal Between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrCari.Item(ComboBox1.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
            End If

            SQL = SQL & "order by a.Tanggal desc"
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_DataAdjustment.Items.Add(.Rows(i).Item("Kode_Adjustment"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            Lvw.SubItems.Add(.Rows(i).Item("Jam"))
                            Lvw.SubItems.Add(.Rows(i).Item("Kode_Stock_Owner"))
                            Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang"))
                            Lvw.SubItems.Add(.Rows(i).Item("Nama_Barang"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah"), "N2"))
                            Lvw.SubItems.Add(.Rows(i).Item("Satuan"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Total_Barang"), "N2"))
                            Lvw.SubItems.Add(.Rows(i).Item("Satuan_Barang"))
                            Lvw.SubItems.Add(.Rows(i).Item("Keterangan"))
                            Lvw.SubItems.Add(.Rows(i).Item("UserID"))
                            If General_Class.CekNULL(.Rows(i).Item("Kode_Voucher")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("Kode_Voucher"))
                            End If
                            If General_Class.CekNULL(.Rows(i).Item("Grand")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Grand"), "N2"))
                            End If
                            If General_Class.CekNULL(.Rows(i).Item("Tgl_Input")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Input"), "dd MMM yyyy"))
                            End If
                            If General_Class.CekNULL(.Rows(i).Item("Jumlah_Bags")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah_Bags"), "N2"))
                            End If
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

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = False
            DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
        Else
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub CetakUlangFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangFakturToolStripMenuItem.Click
        If Lv_DataAdjustment.Items.Count = 0 Or Lv_DataAdjustment.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select kode_perusahaan from View_Faktur_Adjustment "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_adjustment = '" & Lv_DataAdjustment.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Rpt_Faktur_Adjustment       'Nama file CR
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{View_Faktur_Adjustment.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Faktur_Adjustment.kode_adjustment} = '" & Lv_DataAdjustment.FocusedItem.Text & "'"
                        'CrDoc.SummaryInfo.ReportTitle = "Periode" & Format(DateTimePicker3.Value, "dd MMM yyyy") & " s/d " & Format(DateTimePicker4.Value, "dd MMM yyyy") & Chr(13) & "Akun : " & ComboBox1.Text
                        .Text = "Faktur Adjustment"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                    End With
                Else
                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            ComboBox1.Enabled = True : TextBox1.Enabled = True
        Else
            ComboBox1.Enabled = False : TextBox1.Enabled = False
            ComboBox1.SelectedIndex = -1 : TextBox1.Text = ""
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_DataAdjustment.SelectedIndexChanged
        Try
            OpenConn()

            Lv_DataDetailAdjustment.Items.Clear()
            'SQL = "select Kd_Rak,Jml_Display,Jumlah_Bags from Faktur_Transfer_Cost where "
            'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "No_Faktur = '" & Lv_DataAdjustment.FocusedItem.Text & "' "
            SQL = "select a.Kode_Barang, b.Nama as Nama_Barang, a.Jumlah, a.Jumlah_Bags "
            SQL = SQL & "from EMI_Det_Adj a, barang b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and No_Faktur = '" & Lv_DataAdjustment.FocusedItem.Text & "' "
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_DataDetailAdjustment.Items.Add(.Rows(i).Item("Kode_Barang"))
                            Lvw.SubItems.Add(.Rows(i).Item("Nama_Barang"))
                            If General_Class.CekNULL(.Rows(i).Item("Jumlah")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah"), "N2"))
                            End If
                            If General_Class.CekNULL(.Rows(i).Item("Jumlah_Bags")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah_Bags"), "N2"))
                            End If
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
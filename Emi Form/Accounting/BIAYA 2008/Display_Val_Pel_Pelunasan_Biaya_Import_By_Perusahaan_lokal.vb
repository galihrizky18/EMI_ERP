Public Class Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan_lokal
    Dim Arr1, Arr2, Arr3 As New ArrayList
    Dim pertama As Integer = 1

    Dim LvNoVal As String
    Dim LvTglTransaksi As String
    Dim LvJam As String
    Dim LvKeterangan As String
    Dim LvUserValidasi As String
    Dim LvMataUang As String
    Dim LvTotal As String
    Dim LvTotalPPN As String
    Dim LvTotalPPH As String
    Dim LvGrandTotal As String

    Dim LvDetNoVal As String
    Dim LvDetNoFaktur As String
    Dim LvDetKodePerusahaanBiayaImport As String
    Dim LvDetKodeKategori As String
    Dim LvDetKodeStockOwner As String
    Dim LvDetPembayaran As String
    Dim LvDetTambahanPembayaran As String
    Dim LvDetNilaiPPN As String
    Dim LvDetNilaiPPH As String
    Dim LvDetSubTotal As String

    Dim itemPelNoVal As Integer = 0
    Dim itemPelTanggal As Integer = 1
    Dim itemPelJam As Integer = 2
    Dim itemPelKeterangan As Integer = 3
    Dim itemPelUser As Integer = 4
    Dim itemPelMataUang As Integer = 5
    Dim itemPelTotal As Integer = 6
    Dim itemPelTotalPPN As Integer = 7
    Dim itemPelTotalPPH As Integer = 8
    Dim itemGrandTotal As Integer = 9

    Dim itemDetNoVal As Integer = 0
    Dim itemDetNoFaktur As Integer = 1
    Dim itemDetKodePerusahaanBiayaImport As Integer = 2
    Dim itemDetKodeKategori As Integer = 3
    Dim itemDetKodeStockOwner As Integer = 4
    Dim itemDetPembayaran As Integer = 5
    Dim itemDetTambahanPembayaran As Integer = 6
    Dim itemDetNilaiPPN As Integer = 7
    Dim itemDetNilaiPPH As Integer = 8
    Dim itemDetSubTotal As Integer = 9

    Private Sub Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvNoVal = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).Text
        LvTglTransaksi = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelTanggal).Text
        LvJam = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelJam).Text
        LvKeterangan = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelKeterangan).Text
        LvUserValidasi = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelUser).Text
        LvMataUang = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelMataUang).Text
        LvTotal = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelTotal).Text
        LvTotalPPN = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelTotalPPN).Text
        LvTotalPPH = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelTotalPPH).Text
        LvGrandTotal = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemGrandTotal).Text

        LvDetNoVal = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).Text
        LvDetNoFaktur = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetNoFaktur).Text
        LvDetKodePerusahaanBiayaImport = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetKodePerusahaanBiayaImport).Text
        LvDetKodeKategori = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetKodeKategori).Text
        LvDetKodeStockOwner = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetKodeStockOwner).Text
        LvDetPembayaran = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetPembayaran).Text
        LvDetTambahanPembayaran = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetTambahanPembayaran).Text
        LvDetNilaiPPN = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetNilaiPPN).Text
        LvDetNilaiPPH = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetNilaiPPH).Text
        LvDetSubTotal = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetSubTotal).Text

    End Sub

    Private Sub Header_lvValPelBiayaImport()
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("No Val", 100, HorizontalAlignment.Left) '0            
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Tanggal", 90, HorizontalAlignment.Center) '1
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Jam", 65, HorizontalAlignment.Center) '2
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '3
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("User", 80, HorizontalAlignment.Center) '4
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Mata Uang", 0, HorizontalAlignment.Center) '5
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Total", 120, HorizontalAlignment.Right) '6
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Total PPN", 100, HorizontalAlignment.Right) '7
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Total PPH", 100, HorizontalAlignment.Right) '8
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Grand Total", 120, HorizontalAlignment.Right) '9

        lvValPelPelunasanBiayaImportByPerusahaan.View = View.Details
    End Sub

    Private Sub Header_lvDetailValPelBiayaImport()
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("No Val", 0, HorizontalAlignment.Left) '0            
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("No Faktur", 100, HorizontalAlignment.Left) '1
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Nama Perusahaan", 130, HorizontalAlignment.Left) '2
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Kategori Biaya", 130, HorizontalAlignment.Left) '3        
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Lokasi", 110, HorizontalAlignment.Left) '4
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Nilai", 110, HorizontalAlignment.Right) '5
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Tambahan", 90, HorizontalAlignment.Right) '6
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Nilai PPN", 90, HorizontalAlignment.Right) '7
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Nilai PPH", 90, HorizontalAlignment.Right) '8
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Sub Total", 110, HorizontalAlignment.Right) '9

        lvDetailValPelPelunasanBiayaImportByPerusahaan.View = View.Details
    End Sub

    Private Sub Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Header_lvValPelBiayaImport()
        Header_lvDetailValPelBiayaImport()

        CbParamTgl.Checked = False : CbParamLain.Checked = False
        cmbTgl.Items.Clear() : cmbTgl.Text = "" : Arr1.Clear()
        cmbTgl.Items.Add("Tanggal Pelunasan") : Arr1.Add("Tanggal")

        cmbParamLain.Items.Clear() : cmbParamLain.Text = "" : Arr2.Clear()
        cmbParamLain.Items.Add("No Pelunasan") : Arr2.Add("a.No_Val")

        cmbTgl.Enabled = False : cmbParamLain.Enabled = False
        DtpAwal.Enabled = False : DtpAkhir.Enabled = False
        TxtValue.Enabled = False

        'loadPelBiayaImport()

    End Sub

    Private Sub loadPelBiayaImport()
        Try
            OpenConn()

            lvValPelPelunasanBiayaImportByPerusahaan.Items.Clear()

            SQL = "select a.No_Val, a.Tanggal, a.Jam, a.Keterangan, a.UserValidasi, a.Mata_Uang, total, Total_PPN, Total_PPH, Grand_Total "
            SQL = SQL & "from Val_Pel_Biaya_import_by_Perusahaan_Lokal a"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = lvValPelPelunasanBiayaImportByPerusahaan.Items.Add(General_Class.CekNULL(Dr("No_Val")))
                    Lv.SubItems.Add(General_Class.CekNULL(Format(Dr("Tanggal"), "dd MMM yyyy")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jam")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("UserValidasi")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Mata_Uang")))
                    Lv.SubItems.Add(Format(Dr("total"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Total_PPN"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Total_PPH"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Grand_Total"), "N2"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_ValPelBiayaImport()
        Try
            pertama = 1

            SQL = "Select a.No_Val, a.Tanggal, a.Jam, a.Keterangan, a.UserValidasi, a.Mata_Uang, total, Total_PPN, Total_PPH, Grand_Total "
            SQL = SQL & "From Val_Pel_Biaya_import_by_Perusahaan_Lokal a "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "

            If CbTransaksi_HrIni.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.Tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CbParamTgl.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr1.Item(cmbTgl.SelectedIndex) & " Between '"
                SQL = SQL & Format(DtpAwal.Value, "yyyy-MM-dd") & "' and '" & Format(DtpAkhir.Value, "yyyy-MM-dd") & "' "
            End If

            If CbParamLain.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr2.Item(cmbParamLain.SelectedIndex) & " like '%" & Trim(TxtValue.Text) & "%' "
            End If

            SQL = SQL & "Order by a.Tanggal + a.Jam Desc"

            OpenConn()

            lvValPelPelunasanBiayaImportByPerusahaan.Items.Clear() : lvDetailValPelPelunasanBiayaImportByPerusahaan.Items.Clear()

            Dim Lvw As ListViewItem

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = lvValPelPelunasanBiayaImportByPerusahaan.Items.Add(General_Class.CekNULL(.Rows(i).Item("No_Val")))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Jam")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Keterangan")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("UserValidasi")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Mata_Uang")))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("total"), "N2"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Total_PPN"), "N2"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Total_PPH"), "N2"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Grand_Total"), "N2"))

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

    Private Sub Get_DetailValPelBiayaImport()
        Try
            If pertama = 1 Then
                OpenConn()

                Dim Grand As Double = 0
                lvDetailValPelPelunasanBiayaImportByPerusahaan.Items.Clear()

                SQL = "select Kode_Perusahaan, No_Val, No_Faktur, Kode_Perusahaan_Biaya_Import, Kode_kategori_biaya_import, "
                SQL = SQL & "Kode_stock_Owner, Byr, Tambahan, Nilai_PPN, Nilai_PPH, Subtotal from detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Val = '" & lvValPelPelunasanBiayaImportByPerusahaan.FocusedItem.Text & "' "

                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items.Add(General_Class.CekNULL(Dr("No_val")))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("No_Faktur")))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("Kode_Perusahaan_Biaya_Import")))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("Kode_kategori_biaya_import")))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("Kode_stock_Owner")))
                        Lv.SubItems.Add(Format(Dr("Byr"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Tambahan"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Nilai_PPN"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Nilai_PPH"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Subtotal"), "N2"))

                        'If General_Class.CekNULL(Dr("Nilai_PPH")) = "" Then
                        '    Lv.SubItems.Add("-")
                        'Else
                        '    Lv.SubItems.Add(Format(General_Class.CekNULL(Dr("Nilai_PPH")), "N2"))
                        'End If

                        'Lv.SubItems.Add(Format(CekIsNull(Dr("Byr")), "N2"))
                        'Lv.SubItems.Add(Format(CekIsNull(Dr("Tambahan")), "N2"))
                        'Lv.SubItems.Add(Format(CekIsNull(Dr("Nilai_PPN")), "N2"))
                        'Lv.SubItems.Add(Format(CekIsNull(Dr("Nilai_PPH")), "N2"))
                        'Lv.SubItems.Add(Format(CekIsNull(Dr("Subtotal")), "N2"))
                    Loop
                End Using

                CloseConn()

                pertama = 0
            Else
                pertama = 1
            End If
        Catch ex As Exception
            pertama = 1
            CloseConn()
            MessageBox.Show(ex.Message)
            lvDetailValPelPelunasanBiayaImportByPerusahaan.Items.Clear()
            Exit Sub
        End Try
    End Sub

    Private Sub CbTransaksi_HrIni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbTransaksi_HrIni.CheckedChanged
        If CbTransaksi_HrIni.Checked = True Then
            CbParamTgl.Checked = False
            btnCari_Click(CbTransaksi_HrIni, e)
        End If
    End Sub

    Private Sub CbParamTgl_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbParamTgl.CheckedChanged
        If CbParamTgl.Checked Then
            cmbTgl.Enabled = True : DtpAwal.Enabled = False : DtpAkhir.Enabled = False
            CbTransaksi_HrIni.Checked = False
        Else
            cmbTgl.Enabled = False : DtpAwal.Enabled = False : DtpAkhir.Enabled = False
            cmbTgl.SelectedIndex = -1 : DtpAwal.Value = Now.Date : DtpAkhir.Value = Now.Date
        End If
    End Sub

    Private Sub CbParamLain_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbParamLain.CheckedChanged
        If CbParamLain.Checked Then
            cmbParamLain.Enabled = True : TxtValue.Enabled = True
        Else
            cmbParamLain.Enabled = False : TxtValue.Enabled = False
            cmbParamLain.SelectedIndex = -1 : TxtValue.Text = ""
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvValPelPelunasanBiayaImportByPerusahaan.SelectedIndexChanged
        If lvValPelPelunasanBiayaImportByPerusahaan.Items.Count = 0 Then Exit Sub

        Get_DetailValPelBiayaImport()

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

        Try
            pertama = 1

            If CbParamTgl.Checked = False And CbParamLain.Checked = False And CbTransaksi_HrIni.Checked = False Then
                MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
                CbParamTgl.Focus() : Exit Sub
            End If

            If CbParamTgl.Checked Then
                If cmbTgl.SelectedIndex = -1 Then
                    MessageBox.Show("Parameter pencarian per tanggal harus diisi!", Judul)
                    cmbTgl.Focus() : Exit Sub
                ElseIf DtpAwal.Value > DtpAkhir.Value Then
                    MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                    DtpAwal.Value = Now.Date : DtpAkhir.Value = Now.Date
                    Exit Sub
                End If
            ElseIf CbParamLain.Checked Then
                If cmbParamLain.SelectedIndex = -1 Then
                    MessageBox.Show("Parameter lain harus diisi!", Judul)
                    cmbParamLain.Focus() : Exit Sub
                ElseIf TxtValue.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value parameter lain harus diisi!", Judul)
                    TxtValue.Focus() : Exit Sub
                End If
            End If

            Get_ValPelBiayaImport()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub cmbTgl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTgl.SelectedIndexChanged
        DtpAwal.Focus()

        If cmbTgl.SelectedIndex = 0 Then
            DtpAwal.Enabled = True : DtpAkhir.Enabled = True
        Else
            DtpAwal.Enabled = False : DtpAkhir.Enabled = False
        End If
    End Sub

    Private Sub CopyNoValToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyNoValToolStripMenuItem.Click
        If lvValPelPelunasanBiayaImportByPerusahaan.Items.Count = 0 Or lvValPelPelunasanBiayaImportByPerusahaan.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no val yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(lvValPelPelunasanBiayaImportByPerusahaan.FocusedItem.Text)
    End Sub

    Private Sub CetakToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakToolStripMenuItem.Click
        If lvValPelPelunasanBiayaImportByPerusahaan.Items.Count = 0 Or lvValPelPelunasanBiayaImportByPerusahaan.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu No Val yang mau cetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            'SQL
            SQL = "select 1. as No,a.No_Val, a.Tanggal, a.jam, b.No_Faktur, b.Kode_Perusahaan_Biaya_Import, "
            SQL = SQL & "b.Kode_kategori_biaya_import, b.Kode_stock_Owner, b.byr, b.Tambahan, b.Nilai_PPN, b.Nilai_PPH,b.Subtotal "
            SQL = SQL & "from Val_Pel_Biaya_Import_By_Perusahaan_lokal a, detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal b "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Val=b.No_Val and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & " and a.no_val='" & lvValPelPelunasanBiayaImportByPerusahaan.FocusedItem.Text & "'"
            'If CbTransaksi_HrIni.Checked Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & " a.Tanggal between '"
            '    SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            'End If
            'If CbParamTgl.Checked Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & Arr1.Item(cmbTgl.SelectedIndex) & " between '"
            '    SQL = SQL & Format(DtpAwal.Value, "yyyy-MM-dd") & "' and '" & Format(DtpAkhir.Value, "yyyy-MM-dd") & "' "
            'End If
            'If CbParamLain.Checked Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & " "

            '    If cmbParamLain.SelectedIndex = 0 Then
            '        SQL = SQL & "and " & Arr2.Item(cmbParamLain.SelectedIndex) & " like '%" & Trim(TxtValue.Text) & "%' "
            '    End If

            'End If

            'SF
            SF = "{Val_Pel_Biaya_Import_By_Perusahaan_lokal.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
            SF = SF & "{Val_Pel_Biaya_Import_By_Perusahaan_lokal.No_Val} = '" & lvValPelPelunasanBiayaImportByPerusahaan.FocusedItem.Text & "' "
            'If CbTransaksi_HrIni.Checked Then
            '    SF = "{val_pel_biaya_import_by_perusahaan.Tanggal} >= #" & Format(Now, "yyyy-MM-dd") & "# AND {val_pel_biaya_import_by_perusahaan.Tanggal} <= #" & Format(Now, "yyyy-MM-dd") & "# "
            'End If
            'If CbParamTgl.Checked Then
            '    SF = "{val_pel_biaya_import_by_perusahaan.Tanggal} >= #" & Format(DtpAwal.Value, "yyyy-MM-dd") & "# AND {val_pel_biaya_import_by_perusahaan.Tanggal} <= #" & Format(DtpAkhir.Value, "yyyy-MM-dd") & "# "
            'End If
            'If CbParamLain.Checked Then
            '    If cmbParamLain.SelectedIndex = 0 Then
            '        SF = SF & "and {val_pel_biaya_import_by_perusahaan.No_Val} like '*" & TxtValue.Text & "*' "
            '    End If
            'End If

            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    Dim CrDoc As Object
                    CrDoc = New R_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan_lokal

                    With A_Place_For_Printing
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = SF
                        CrDoc.SummaryInfo.ReportTitle = "Val Pel Pelunasan Biaya Import By Perusahaan"
                        .Text = "Laporan Val Pel Pelunasan Biaya Import By Perusahaan"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                        .Focus()
                    End With
                Else
                    CloseConn()
                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    'UTILITY FUNCTION
    Private Shared Function CekIsNull(ByVal xNullString As Object) As String
        Try
            If IsDBNull(xNullString) Then
                Return "0"
            Else
                Return xNullString.ToString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "0"
        End Try
    End Function


End Class
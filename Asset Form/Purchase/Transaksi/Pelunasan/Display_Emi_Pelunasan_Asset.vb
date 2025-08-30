Public Class Display_Emi_Pelunasan_Asset
    Dim Arr1, Arr2, Arr3 As New ArrayList
    Dim pertama As Integer = 1

    Dim LvNoVal As String
    Dim LvTglTransaksi As String
    Dim LvJam As String
    Dim LvKategoriBiaya As String
    Dim LvKeterangan As String
    Dim LvUserValidasi As String
    Dim LvMataUang As String
    Dim LvTotal As String
    Dim LvTotalPPN As String
    Dim LvTotalPPH As String
    Dim LvGrandTotal As String
    Dim LvTotalKursLama As String
    Dim LvTotalKursBaru As String
    Dim LvJenisBiaya As String
    Dim LvNoPengajuan As String

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
    Dim LvDetKursLama As String
    Dim LvDetKursBaru As String
    Dim LvDetTglBayar As String
    Dim LvDetBankTujuan As String
    Dim LvDetRekTujuan As String
    Dim LvDetPenerima As String

    Dim itemPelNoVal As Integer = 0
    Dim itemPelTanggal As Integer = 1
    Dim itemPelJam As Integer = 2
    Dim itemPelKategoriBiaya As Integer = 3
    Dim itemPelKeterangan As Integer = 4
    Dim itemPelUser As Integer = 5
    Dim itemPelMataUang As Integer = 6
    Dim itemPelTotal As Integer = 7
    Dim itemPelTotalPPN As Integer = 8
    Dim itemPelTotalPPH As Integer = 9
    Dim itemGrandTotal As Integer = 10
    Dim itemTotalKursLama As Integer = 11
    Dim itemTotalKursBaru As Integer = 12
    Dim itemJenisBiaya As Integer = 13
    Dim itemNoPengajuan As Integer = 14

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
    Dim itemDetKursLama As Integer = 10
    Dim itemDetKursBaru As Integer = 11
    Dim itemDetTglBayar As Integer = 12
    Dim itemDetBankTujuan As Integer = 13
    Dim itemDetRekTujuan As Integer = 14
    Dim itemDetPenerima As Integer = 15

    Private Sub Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvNoVal = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).Text
        LvTglTransaksi = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelTanggal).Text
        LvJam = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelJam).Text
        LvKategoriBiaya = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelKategoriBiaya).Text
        LvKeterangan = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelKeterangan).Text
        LvUserValidasi = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelUser).Text
        LvMataUang = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelMataUang).Text
        LvTotal = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelTotal).Text
        LvTotalPPN = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelTotalPPN).Text
        LvTotalPPH = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemPelTotalPPH).Text
        LvGrandTotal = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemGrandTotal).Text
        LvTotalKursLama = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemTotalKursLama).Text
        LvTotalKursBaru = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemTotalKursBaru).Text
        LvJenisBiaya = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemJenisBiaya).Text
        LvNoPengajuan = lvValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemNoPengajuan).Text

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
        LvDetKursLama = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetKursLama).Text
        LvDetKursBaru = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items(No_Index).SubItems(itemDetKursBaru).Text

    End Sub

    Private Sub Header_lvValPelBiayaImport()
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("No Pelunasan", 120, HorizontalAlignment.Left) '0            
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Tanggal", 90, HorizontalAlignment.Center) '1
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Jam", 65, HorizontalAlignment.Center) '2
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Kategori Biaya", 0, HorizontalAlignment.Left) '3
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Keterangan", 250, HorizontalAlignment.Left) '4
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("User", 80, HorizontalAlignment.Center) '5
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Mata Uang", 0, HorizontalAlignment.Center) '6
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Total", 110, HorizontalAlignment.Right) '7
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Total PPN", 100, HorizontalAlignment.Right) '8
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Total PPH", 100, HorizontalAlignment.Right) '9
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Grand Total", 110, HorizontalAlignment.Right) '10
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Total Kurs Lama", 110, HorizontalAlignment.Right) '11
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Total Kurs Baru", 110, HorizontalAlignment.Right) '12
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("JenisBiaya", 0, HorizontalAlignment.Right) '13
        lvValPelPelunasanBiayaImportByPerusahaan.Columns.Add("No Pengajuan", 120, HorizontalAlignment.Left) '14

        lvValPelPelunasanBiayaImportByPerusahaan.View = View.Details
    End Sub

    Private Sub Header_lvDetailValPelBiayaImport()
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("No Val", 0, HorizontalAlignment.Left) '0            
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '1
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Nama Perusahaan", 200, HorizontalAlignment.Left) '2
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Kategori Biaya", 0, HorizontalAlignment.Left) '3
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Lokasi", 150, HorizontalAlignment.Left) '4
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Nilai", 110, HorizontalAlignment.Right) '5
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Tambahan", 90, HorizontalAlignment.Right) '6
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Nilai PPN", 90, HorizontalAlignment.Right) '7
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Nilai PPH", 90, HorizontalAlignment.Right) '8
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Sub Total", 110, HorizontalAlignment.Right) '9
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Kurs Lama", 110, HorizontalAlignment.Right) '10
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Kurs Baru", 110, HorizontalAlignment.Right) '11
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Tanggal Bayar", 130, HorizontalAlignment.Center) '12
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Bank Tujuan", 90, HorizontalAlignment.Center) '13
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Rekening Tujuan", 130, HorizontalAlignment.Left) '14
        lvDetailValPelPelunasanBiayaImportByPerusahaan.Columns.Add("Penerima", 130, HorizontalAlignment.Left) '15

        lvDetailValPelPelunasanBiayaImportByPerusahaan.View = View.Details
    End Sub

    Private Sub Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Header_lvValPelBiayaImport()
        Header_lvDetailValPelBiayaImport()

        CbParamTgl.Checked = False : CbParamLain.Checked = False
        cmbTgl.Items.Clear() : cmbTgl.Text = "" : Arr1.Clear()
        cmbTgl.Items.Add("Tanggal Pelunasan") : Arr1.Add("a.Tanggal")

        cmbParamLain.Items.Clear() : cmbParamLain.Text = "" : Arr2.Clear()
        cmbParamLain.Items.Add("No Pelunasan") : Arr2.Add("a.No_Val")
        cmbParamLain.Items.Add("User") : Arr2.Add("a.UserValidasi")
        'cmbParamLain.Items.Add("Mata Uang") : Arr2.Add("a.Mata_Uang")

        cmbTgl.Enabled = False : cmbParamLain.Enabled = False
        DtpAwal.Enabled = False : DtpAkhir.Enabled = False
        TxtValue.Enabled = False

        'loadPelBiayaImport()

    End Sub

    ''Private Sub loadPelBiayaImport()
    ''    Try
    ''        OpenConn()
    ''        lvValPelPelunasanBiayaImportByPerusahaan.Items.Clear()
    ''        SQL = "select a.No_Val, a.Tanggal, a.Jam, a.Keterangan, a.UserValidasi, a.Mata_Uang, total, Total_PPN, Total_PPH, Grand_Total, Total_Kurs_Lama, Total_Kurs_Baru "
    ''        SQL = SQL & "from Val_Pel_Biaya_import_by_Perusahaan_Lokal a"
    ''        Using Dr = OpenTrans(SQL)
    ''            Do While Dr.Read
    ''                Dim Lv As ListViewItem
    ''                Lv = lvValPelPelunasanBiayaImportByPerusahaan.Items.Add(General_Class.CekNULL(Dr("No_Val")))
    ''                Lv.SubItems.Add(General_Class.CekNULL(Format(Dr("Tanggal"), "dd MMM yyyy")))
    ''                Lv.SubItems.Add(General_Class.CekNULL(Dr("Jam")))
    ''                Lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")))
    ''                Lv.SubItems.Add(General_Class.CekNULL(Dr("UserValidasi")))
    ''                Lv.SubItems.Add(General_Class.CekNULL(Dr("Mata_Uang")))
    ''                Lv.SubItems.Add(General_Class.CekNULL(Format(Dr("total"), "N2")))
    ''                Lv.SubItems.Add(General_Class.CekNULL(Format(Dr("Total_PPN"), "N2")))
    ''                Lv.SubItems.Add(General_Class.CekNULL(Format(Dr("Total_PPH"), "N2")))
    ''                Lv.SubItems.Add(General_Class.CekNULL(Format(Dr("Grand_Total"), "N2")))
    ''                If General_Class.CekNULL(Dr("Total_Kurs_Lama")) = "" Then
    ''                    Lv.SubItems.Add("-")
    ''                Else
    ''                    Lv.SubItems.Add(Format(Dr("Total_Kurs_Lama"), "N2"))
    ''                End If

    ''                If General_Class.CekNULL(Dr("Total_Kurs_Baru")) = "" Then
    ''                    Lv.SubItems.Add("-")
    ''                Else
    ''                    Lv.SubItems.Add(Format(Dr("Total_Kurs_Baru"), "N2"))
    ''                End If
    ''            Loop
    ''        End Using
    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub

    Private Sub Get_ValPelBiayaImport()
        Try
            OpenConn()
            pertama = 1

            lvValPelPelunasanBiayaImportByPerusahaan.Items.Clear() : lvDetailValPelPelunasanBiayaImportByPerusahaan.Items.Clear()

            'SQL = "Select a.No_Val, a.Tanggal, a.Jam, a.Keterangan, a.UserValidasi, a.Mata_Uang, total, Total_PPN, Total_PPH, Grand_Total, Total_Kurs_Lama, Total_Kurs_Baru "
            'SQL = SQL & "From Val_Pel_Biaya_import_by_Perusahaan_Lokal a "
            'SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "

            'If CbTransaksi_HrIni.Checked Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & " a.Tanggal between '"
            '    SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            'End If

            'If CbParamTgl.Checked Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & Arr1.Item(cmbTgl.SelectedIndex) & " Between '"
            '    SQL = SQL & Format(DtpAwal.Value, "yyyy-MM-dd") & "' and '" & Format(DtpAkhir.Value, "yyyy-MM-dd") & "' "
            'End If

            'If CbParamLain.Checked Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & Arr2.Item(cmbParamLain.SelectedIndex) & " like '%" & Trim(TxtValue.Text) & "%' "
            'End If
            'SQL = SQL & "Order by a.Tanggal + a.Jam Desc"

            SQL = "Select a.No_Val, a.Tanggal, a.Jam, a.Keterangan, a.UserValidasi, a.Mata_Uang, a.total, a.Total_PPN, a.Total_PPH, a.Grand_Total, a.Total_Kurs_Lama, a.Total_Kurs_Baru, a.jenis, a.no_pengajuan "
            'SQL = SQL & "ISNULL(( "
            'SQL = SQL & "select top 1 x.Keterangan from EMI_Pelunasan_Detail z, Master_Kategori_Biaya_Import x "
            'SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Val = z.No_Val and z.Kode_Master_Kategori_Biaya_Import = x.Kode_Master_Kategori_Biaya_Import "
            'SQL = SQL & "), '-') as Kategori_Biaya "
            SQL = SQL & "From EMI_Pelunasan_Barang_Lain a "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null "

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
            SQL = SQL & "Order by a.Tanggal + a.Jam Desc "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dim Lvw As ListViewItem

                            ''Lvw = lvValPelPelunasanBiayaImportByPerusahaan.Items.Add(General_Class.CekNULL(.Rows(i).Item("No_Val")))
                            ''Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            ''Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Jam")))
                            ''Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Keterangan")))
                            ''Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("UserValidasi")))
                            ''Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Mata_Uang")))
                            ''Lvw.SubItems.Add(Format(.Rows(i).Item("total"), "N2"))
                            ''Lvw.SubItems.Add(Format(.Rows(i).Item("Total_PPN"), "N2"))
                            ''Lvw.SubItems.Add(Format(.Rows(i).Item("Total_PPH"), "N2"))
                            ''Lvw.SubItems.Add(Format(.Rows(i).Item("Grand_Total"), "N2"))
                            ''
                            Lvw = lvValPelPelunasanBiayaImportByPerusahaan.Items.Add(General_Class.CekNULL(.Rows(i).Item("No_Val")))
                            Lvw.SubItems.Add(General_Class.CekNULL(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Jam")))
                            Lvw.SubItems.Add(General_Class.CekNULL(""))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Keterangan")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("UserValidasi")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Mata_Uang")))
                            Lvw.SubItems.Add(General_Class.CekNULL(Format(.Rows(i).Item("total"), "N2")))
                            Lvw.SubItems.Add(General_Class.CekNULL(Format(.Rows(i).Item("Total_PPN"), "N2")))
                            Lvw.SubItems.Add(General_Class.CekNULL(Format(.Rows(i).Item("Total_PPH"), "N2")))
                            Lvw.SubItems.Add(General_Class.CekNULL(Format(.Rows(i).Item("Grand_Total"), "N2")))

                            If General_Class.CekNULL(.Rows(i).Item("Total_Kurs_Lama")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Total_Kurs_Lama"), "N2"))
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Total_Kurs_Baru")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Total_Kurs_Baru"), "N2"))
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("jenis")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("jenis"))
                            End If

                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("no_pengajuan")))

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
        If lvValPelPelunasanBiayaImportByPerusahaan.Items.Count = 0 Or lvValPelPelunasanBiayaImportByPerusahaan.FocusedItem Is Nothing Then Exit Sub

        Try
            If pertama = 1 Then
                OpenConn()

                Dim Grand As Double = 0

                lvDetailValPelPelunasanBiayaImportByPerusahaan.Items.Clear()
                SQL = "select Kode_Perusahaan, No_Val, No_Faktur, Kode_Perusahaan_Biaya_Import, Kode_Master_Kategori_Biaya_Import, "
                SQL = SQL & "Kode_stock_Owner, Byr, Tambahan, Nilai_PPN, Nilai_PPH, Subtotal, Kurs_Lama, Kurs_Baru, "
                SQL = SQL & "Kode_Bank_Tujuan, No_Rek_Tujuan, Nama_Penerima, Tanggal_Bayar "
                SQL = SQL & "from EMI_Pelunasan_Detail_Barang_Lain "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and No_Val = '" & lvValPelPelunasanBiayaImportByPerusahaan.FocusedItem.Text & "' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = lvDetailValPelPelunasanBiayaImportByPerusahaan.Items.Add(General_Class.CekNULL(Dr("No_val")))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("No_Faktur")))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("Kode_Perusahaan_Biaya_Import")))
                        If General_Class.CekNULL(Dr("Kode_Master_Kategori_Biaya_Import")) = "" Then
                            Lv.SubItems.Add("-")
                        Else
                            Lv.SubItems.Add(Dr("Kode_Master_Kategori_Biaya_Import"))
                        End If
                        If General_Class.CekNULL(Dr("Kode_stock_Owner")) = "" Then
                            Lv.SubItems.Add("-")
                        Else
                            Lv.SubItems.Add(Dr("Kode_stock_Owner"))
                        End If
                        Lv.SubItems.Add(Format(Dr("Byr"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Tambahan"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Nilai_PPN"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Nilai_PPH"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Subtotal"), "N2"))
                        If General_Class.CekNULL(Dr("Kurs_Lama")) = "" Then
                            Lv.SubItems.Add("-")
                        Else
                            Lv.SubItems.Add(Format(Dr("Kurs_Lama"), "N2"))
                        End If
                        If General_Class.CekNULL(Dr("Kurs_Baru")) = "" Then
                            Lv.SubItems.Add("-")
                        Else
                            Lv.SubItems.Add(Format(Dr("Kurs_Baru"), "N2"))
                        End If

                        Lv.SubItems.Add(Format(Dr("Tanggal_Bayar"), "dd MMM yyyy"))
                        Lv.SubItems.Add(Dr("Kode_Bank_Tujuan"))
                        Lv.SubItems.Add(Dr("No_Rek_Tujuan"))
                        Lv.SubItems.Add(Dr("Nama_Penerima"))
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

            Dim SelectedVal As String = lvValPelPelunasanBiayaImportByPerusahaan.FocusedItem.SubItems(itemPelNoVal).Text
            Dim SelectedJenisBiaya As String = lvValPelPelunasanBiayaImportByPerusahaan.FocusedItem.SubItems(itemJenisBiaya).Text
            Dim NoPengajuan As String = lvValPelPelunasanBiayaImportByPerusahaan.FocusedItem.SubItems(itemNoPengajuan).Text


            SQL = "select Kode_Perusahaan "
            SQL = SQL & "from View_Laporan_Pengajuan_Pelunasan_Barang_Lain  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & SelectedVal & "' "
            SQL = SQL & "and no_pengajuan = '" & NoPengajuan & "' "

            SF = "{View_Laporan_Pengajuan_Pelunasan_Barang_Lain.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {View_Laporan_Pengajuan_Pelunasan_Barang_Lain.No_Val} = '" & SelectedVal & "' "
            SF = SF & "and {View_Laporan_Pengajuan_Pelunasan_Barang_Lain.no_pengajuan} = '" & NoPengajuan & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Laporan_Emi_Pelunasan_Asset    'Nama file CR

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = SF
                    With A_Place_For_Printing2
                        .Text = "Pelunasan Asset "
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                        .Refresh()
                        .Show()
                    End With

                    '=============================================================================
                    '=============================================================================
                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.PrintOptions.PrinterName = PrinterName
                    'CrDoc.RecordSelectionFormula = SF

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterName
                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next
                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    'CrDoc.PrintToPrinter(1, False, 1, 99)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

#Region "CETAK LAMA"

        '    If SelectedJenisBiaya.Trim.ToUpper = "IMPORT" Then

        '        SQL = "select Kode_Perusahaan from View_Laporan_Pelunasan_Biaya_Import_IMPORT "
        '        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & SelectedVal & "' "

        '        SF = "{View_Laporan_Pelunasan_Biaya_Import_IMPORT.Kode_Perusahaan} = '" & KodePerusahaan & "' "
        '        SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_IMPORT.No_Val} = '" & SelectedVal & "' "

        '        Using Ds = BindingTrans(SQL)
        '            If Ds.Tables("MyTable").Rows.Count <> 0 Then

        '                CrDoc = New Laporan_Pelunasan_Biaya_Import_By_Perusahaan_IMPORT

        '                With A_Place_For_Printing2
        '                    CrDoc.SetDataSource(Ds)
        '                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                    CrDoc.RecordSelectionFormula = SF
        '                    CrDoc.SummaryInfo.ReportTitle = "Val Pel Pelunasan Biaya Import By Perusahaan"
        '                    .Text = "Laporan Val Pel Pelunasan Biaya Import By Perusahaan (IMPORT)"
        '                    .CrystalReportViewer1.ReportSource = CrDoc
        '                    .CrystalReportViewer1.DisplayGroupTree = False
        '                    .Refresh()
        '                    .Show()
        '                    .Focus()
        '                End With

        '            Else
        '                CloseConn()
        '                MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub

        '            End If
        '        End Using

        '    ElseIf SelectedJenisBiaya.Trim.ToUpper = "LOKAL" Then

        '        SQL = "select Kode_Perusahaan from View_Laporan_Pelunasan_Biaya_Import_LOKAL "
        '        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & SelectedVal & "' "

        '        SF = "{View_Laporan_Pelunasan_Biaya_Import_LOKAL.Kode_Perusahaan} = '" & KodePerusahaan & "' "
        '        SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_LOKAL.No_Val} = '" & SelectedVal & "' "

        '        Using Ds = BindingTrans(SQL)
        '            If Ds.Tables("MyTable").Rows.Count <> 0 Then

        '                CrDoc = New Laporan_Pelunasan_Biaya_Import_By_Perusahaan_LOKAL

        '                With A_Place_For_Printing2
        '                    CrDoc.SetDataSource(Ds)
        '                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                    CrDoc.RecordSelectionFormula = SF
        '                    CrDoc.SummaryInfo.ReportTitle = "Val Pel Pelunasan Biaya Import By Perusahaan"
        '                    .Text = "Laporan Val Pel Pelunasan Biaya Import By Perusahaan (LOKAL)"
        '                    .CrystalReportViewer1.ReportSource = CrDoc
        '                    .CrystalReportViewer1.DisplayGroupTree = False
        '                    .Refresh()
        '                    .Show()
        '                    .Focus()
        '                End With

        '            Else
        '                CloseConn()
        '                MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub

        '            End If
        '        End Using

        '    Else
        '        CloseConn()
        '        MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If

        '    CloseConn()

        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

#End Region

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
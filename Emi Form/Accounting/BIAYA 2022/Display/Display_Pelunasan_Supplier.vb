Public Class Display_Pelunasan_Supplier
    Dim Arr1, Arr2, Arr3 As New ArrayList
    Dim pertama As Integer = 1

    Dim LvNoVal, LvTglTransaksi, LvJam, LvKeterangan, LvUserValidasi, LvCaraBayar, LvTotal, LvTotalIDRKursLama, LvTotalIDRKursBaru, LvTotalSelisih, LvTotalSelisihPOSblm As String

    Dim itemPelNoVal As Integer = 0
    Dim itemPelTanggal As Integer = 1
    Dim itemPelJam As Integer = 2
    Dim itemPelKeterangan As Integer = 3
    Dim itemPelUser As Integer = 4
    Dim itemPelCaraBayar As Integer = 5
    Dim itemPelTotal As Integer = 6
    Dim itemPelTotalIDRKursLama As Integer = 7
    Dim itemPelTotalIDRKursBaru As Integer = 8
    Dim itemPelTotalSelisih As Integer = 9
    Dim itemPelTotalSelisihPOSblm As Integer = 10

    Dim LvDetNoVal, LvDetNoFaktur, LvDetByr, LvDetMataUang, LvDetKursLama, LvDetKursBaru, LvDetSelisihTotalKurs, LvDetSelisihPOSblm, LvDetTotalAwal, LvDetTotalAkhir, LvDetSelisih As String

    Dim itemDetNoVal As Integer = 0
    Dim itemDetNoFaktur As Integer = 1
    Dim itemDetByr As Integer = 2
    Dim itemDetMataUang As Integer = 3
    Dim itemDetKursLama As Integer = 4
    Dim itemDetKursBaru As Integer = 5
    Dim itemDetSelisihTotalKurs As Integer = 6
    Dim itemDetSelisihPOSblm As Integer = 7
    Dim itemDetTotalAwal As Integer = 8
    Dim itemDetTotalAkhir As Integer = 9
    Dim itemDetSelisih As Integer = 10

    Private Sub Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvNoVal = LvPelunasanPembelian.Items(No_Index).Text
        LvTglTransaksi = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelTanggal).Text
        LvJam = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelJam).Text
        LvKeterangan = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelKeterangan).Text
        LvUserValidasi = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelUser).Text
        LvCaraBayar = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelCaraBayar).Text
        LvTotal = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelTotal).Text
        LvTotalIDRKursLama = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelTotalIDRKursLama).Text
        LvTotalIDRKursBaru = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelTotalIDRKursBaru).Text
        LvTotalSelisih = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelTotalSelisih).Text
        LvTotalSelisihPOSblm = LvPelunasanPembelian.Items(No_Index).SubItems(itemPelTotalSelisihPOSblm).Text

        LvDetNoVal = LvDetailPelunasanPembelian.Items(No_Index).Text
        LvDetNoFaktur = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetNoFaktur).Text
        LvDetByr = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetByr).Text
        LvDetMataUang = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetMataUang).Text
        LvDetKursLama = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetKursLama).Text
        LvDetKursBaru = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetKursBaru).Text
        LvDetSelisihTotalKurs = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetSelisihTotalKurs).Text
        LvDetSelisihPOSblm = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetSelisihPOSblm).Text

        LvDetTotalAwal = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetTotalAwal).Text
        LvDetTotalAkhir = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetTotalAkhir).Text
        LvDetSelisih = LvDetailPelunasanPembelian.Items(No_Index).SubItems(itemDetSelisih).Text

    End Sub

    Private Sub Header_lvValPelBiayaImport()
        LvPelunasanPembelian.Columns.Add("No Pelunasan", 100, HorizontalAlignment.Left) '0            
        LvPelunasanPembelian.Columns.Add("Tanggal", 90, HorizontalAlignment.Center) '1
        LvPelunasanPembelian.Columns.Add("Jam", 65, HorizontalAlignment.Center) '2
        LvPelunasanPembelian.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '3
        LvPelunasanPembelian.Columns.Add("User", 80, HorizontalAlignment.Center) '4
        LvPelunasanPembelian.Columns.Add("Cara Bayar", 0, HorizontalAlignment.Center) '5
        LvPelunasanPembelian.Columns.Add("Total", 120, HorizontalAlignment.Right) '6
        LvPelunasanPembelian.Columns.Add("Total IDR Kurs Lama", 130, HorizontalAlignment.Right) '7
        LvPelunasanPembelian.Columns.Add("Total IDR Kurs Baru", 130, HorizontalAlignment.Right) '8
        LvPelunasanPembelian.Columns.Add("Total Selisih", 110, HorizontalAlignment.Right) '9
        LvPelunasanPembelian.Columns.Add("Total Selisih PO Sebelum", 150, HorizontalAlignment.Right) '10

        LvPelunasanPembelian.View = View.Details
    End Sub

    Private Sub Header_lvDetailValPelBiayaImport()
        LvDetailPelunasanPembelian.Columns.Add("No Pelunasan", 0, HorizontalAlignment.Left) '0            
        LvDetailPelunasanPembelian.Columns.Add("No Faktur", 150, HorizontalAlignment.Left) '1
        LvDetailPelunasanPembelian.Columns.Add("Bayar", 130, HorizontalAlignment.Right) '2
        LvDetailPelunasanPembelian.Columns.Add("Mata Uang", 130, HorizontalAlignment.Center) '3
        LvDetailPelunasanPembelian.Columns.Add("Kurs Lama", 130, HorizontalAlignment.Right) '4
        LvDetailPelunasanPembelian.Columns.Add("Kurs Baru", 130, HorizontalAlignment.Right) '5
        LvDetailPelunasanPembelian.Columns.Add("Selisih Total Kurs", 130, HorizontalAlignment.Right) '6
        LvDetailPelunasanPembelian.Columns.Add("Selisih PO Sebelum", 130, HorizontalAlignment.Right) '7
        LvDetailPelunasanPembelian.Columns.Add("Total Awal", 130, HorizontalAlignment.Right) '8
        LvDetailPelunasanPembelian.Columns.Add("Total Akhir", 130, HorizontalAlignment.Right) '9
        LvDetailPelunasanPembelian.Columns.Add("Selisih", 130, HorizontalAlignment.Right) '10


        LvDetailPelunasanPembelian.View = View.Details
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
        cmbParamLain.Items.Add("No Pelunasan") : Arr2.Add("No_Val")
        cmbParamLain.Items.Add("User") : Arr2.Add("UserValidasi")

        cmbTgl.Enabled = False : cmbParamLain.Enabled = False
        DtpAwal.Enabled = False : DtpAkhir.Enabled = False
        TxtValue.Enabled = False

    End Sub

    Private Sub Get_PelunasanPembelian()
        Try
            pertama = 1

            SQL = "Select no_val, tanggal, jam, Keterangan, UserValidasi, Cara_Bayar, total, Total_Idr_Kurs_Lama, Total_Idr_Kurs_Baru, Total_selisih, Total_Selisih_PO_Sebelum "
            SQL = SQL & "From EMI_Pelunasan_Pembelian "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "

            If CbTransaksi_HrIni.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " Tanggal between '"
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

            SQL = SQL & "Order by Tanggal + Jam Desc"

            OpenConn()

            LvPelunasanPembelian.Items.Clear() : LvDetailPelunasanPembelian.Items.Clear()

            Dim Lvw As ListViewItem

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = LvPelunasanPembelian.Items.Add(General_Class.CekNULL(.Rows(i).Item("No_Val")))
                            Lvw.SubItems.Add(General_Class.CekNULL(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Jam")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Keterangan")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("UserValidasi")))
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Cara_Bayar")))
                            Lvw.SubItems.Add(General_Class.CekNULL(Format(.Rows(i).Item("total"), "N2")))
                            Lvw.SubItems.Add(General_Class.CekNULL(Format(.Rows(i).Item("Total_Idr_Kurs_Lama"), "N2")))
                            Lvw.SubItems.Add(General_Class.CekNULL(Format(.Rows(i).Item("Total_Idr_Kurs_Baru"), "N2")))

                            If General_Class.CekNULL(.Rows(i).Item("Total_selisih")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Total_selisih"), "N2"))
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Total_Selisih_PO_Sebelum")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Total_Selisih_PO_Sebelum"), "N2"))
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

    Private Sub Get_DetailPelunasanPembelian()
        Try
            If pertama = 1 Then
                OpenConn()

                Dim Grand As Double = 0
                LvDetailPelunasanPembelian.Items.Clear()

                'SQL = "select no_val, no_faktur, byr, mata_uang, kurs_lama, kurs_baru, Selisih_Total_Kurs, Selisih_PO_Sebelum "
                'SQL = SQL & "from EMI_Pelunasan_Pembelian_Detail "
                'SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "No_Val = '" & LvPelunasanPembelian.FocusedItem.Text & "' "

                SQL = "select no_val, no_faktur, byr, mata_uang, kurs_lama, kurs_baru, Selisih_Total_Kurs, Selisih_PO_Sebelum, "
                SQL = SQL & "ISNULL(( (byr * kurs_lama) ), 0) as SubTotalAwal, "
                SQL = SQL & "ISNULL(( (byr * Kurs_Baru) ), 0) as SubTotalAkhir, "
                SQL = SQL & "ISNULL(( ISNULL(( (byr * Kurs_Baru) ), 0) - ISNULL(( (byr * kurs_lama) ), 0) ), 0) as Selisih "
                SQL = SQL & "from EMI_Pelunasan_Pembelian_Detail  "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and No_Val = '" & LvPelunasanPembelian.FocusedItem.Text & "' "

                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = LvDetailPelunasanPembelian.Items.Add(General_Class.CekNULL(Dr("No_val")))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("No_Faktur")))
                        Lv.SubItems.Add(Format(Dr("Byr"), "N2"))
                        Lv.SubItems.Add(Dr("mata_uang"))
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
                        If General_Class.CekNULL(Dr("Selisih_Total_Kurs")) = "" Then
                            Lv.SubItems.Add("-")
                        Else
                            Lv.SubItems.Add(Format(Dr("Selisih_Total_Kurs"), "N2"))
                        End If
                        If General_Class.CekNULL(Dr("Selisih_PO_Sebelum")) = "" Then
                            Lv.SubItems.Add("-")
                        Else
                            Lv.SubItems.Add(Format(Dr("Selisih_PO_Sebelum"), "N2"))
                        End If

                        Lv.SubItems.Add(Format(Dr("SubTotalAwal"), "N2"))
                        Lv.SubItems.Add(Format(Dr("SubTotalAkhir"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Selisih"), "N2"))
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
            LvDetailPelunasanPembelian.Items.Clear()
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

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvPelunasanPembelian.SelectedIndexChanged
        If LvPelunasanPembelian.Items.Count = 0 Then Exit Sub

        Get_DetailPelunasanPembelian()

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

            Get_PelunasanPembelian()

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
        If LvPelunasanPembelian.Items.Count = 0 Or LvPelunasanPembelian.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no val yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(LvPelunasanPembelian.FocusedItem.Text)
    End Sub

    Private Sub CetakToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakToolStripMenuItem.Click
        If LvPelunasanPembelian.Items.Count = 0 Or LvPelunasanPembelian.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu No Val yang mau cetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim CrDoc As Object
            Dim SF As String = ""

            Dim SelectedVal As String = LvPelunasanPembelian.FocusedItem.SubItems(itemPelNoVal).Text

            SQL = "select Kode_Perusahaan from View_Laporan_Pelunasan_Bahan "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & SelectedVal & "' "

            SF = "{View_Laporan_Pelunasan_Bahan.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {View_Laporan_Pelunasan_Bahan.No_Val} = '" & SelectedVal & "' "

            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New Laporan_Pelunasan_Hutang_Bahan

                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = SF
                        CrDoc.SummaryInfo.ReportTitle = "Val Pel Pelunasan Biaya Bahan"
                        .Text = "Laporan Val Pel Pelunasan Biaya Bahan"
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
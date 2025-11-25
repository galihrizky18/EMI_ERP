Public Class Emi_Display_Pembayaran_Di_Muka_Proyek

    Dim arrFilterTgl, arrFilterParamLain As New ArrayList



    Private Sub Emi_Display_Down_Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Kosong()
    End Sub

    Private Sub Kosong()

        Lv_DataDP.Columns.Clear()
        Lv_DataDP.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
        Lv_DataDP.Columns.Add("Supplier", 180, HorizontalAlignment.Left)
        Lv_DataDP.Columns.Add("Keterangan", 300, HorizontalAlignment.Left)
        Lv_DataDP.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_DataDP.Columns.Add("Mata Uang", 100, HorizontalAlignment.Center)
        Lv_DataDP.Columns.Add("Kurs", 100, HorizontalAlignment.Center)
        Lv_DataDP.Columns.Add("Jumlah DP", 130, HorizontalAlignment.Right)
        Lv_DataDP.Columns.Add("Total DP", 150, HorizontalAlignment.Right)
        Lv_DataDP.Columns.Add("Rekening", 180, HorizontalAlignment.Left)
        Lv_DataDP.Columns.Add("User", 150, HorizontalAlignment.Center)
        'Hide
        Lv_DataDP.Columns.Add("KdSuppluer", 0, HorizontalAlignment.Right)
        Lv_DataDP.View = View.Details

        Lv_DetailDP.Columns.Clear()
        Lv_DetailDP.Columns.Add("No PO", 150, HorizontalAlignment.Left)
        Lv_DetailDP.Columns.Add("Keterangan PO", 250, HorizontalAlignment.Left)
        Lv_DetailDP.Columns.Add("Jumlah DP", 150, HorizontalAlignment.Right)
        Lv_DetailDP.Columns.Add("Jumlah Dipakai", 150, HorizontalAlignment.Right)
        Lv_DetailDP.Columns.Add("Selisih", 150, HorizontalAlignment.Right)
        'hide
        Lv_DetailDP.Columns.Add("noTransaksi", 0, HorizontalAlignment.Left)
        Lv_DetailDP.View = View.Details

        Lv_Pajak.Columns.Clear()
        Lv_Pajak.Columns.Add("Tarif", 120, HorizontalAlignment.Left)
        Lv_Pajak.Columns.Add("Jenis", 120, HorizontalAlignment.Left)
        Lv_Pajak.Columns.Add("Persentase", 120, HorizontalAlignment.Left)
        Lv_Pajak.Columns.Add("Nilai", 180, HorizontalAlignment.Left)
        Lv_Pajak.View = View.Details


        '==================
        '=     FILTER     =
        '==================
        cmbTgl.Items.Clear() : arrFilterTgl.Clear()
        cmbTgl.Items.Add("Tanggal DP") : arrFilterTgl.Add("a.Tanggal")

        cmbParamLain.Items.Clear() : arrFilterParamLain.Clear()
        cmbParamLain.Items.Add("No Transaksi") : arrFilterParamLain.Add("a.No_Transaksi")
        cmbParamLain.Items.Add("Keterangan") : arrFilterParamLain.Add("a.Keterangan")
        cmbParamLain.Items.Add("Supplier") : arrFilterParamLain.Add("b.Nama")
        cmbParamLain.Items.Add("Mata Uang") : arrFilterParamLain.Add("a.Mata_Uang")
        cmbParamLain.Items.Add("Rekening") : arrFilterParamLain.Add("(c.Nama_Bank + ' - '+ c.No_Rekening + '-' + c.Nama_Pemilik)")
        cmbParamLain.Items.Add("User ID") : arrFilterParamLain.Add("a.UserId")

        LoadData()

    End Sub



    Private Sub LoadData()

        Try
            OpenConn()

            Lv_DataDP.Items.Clear() : Lv_DetailDP.Items.Clear() : Lv_Pajak.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Transaksi, a.Keterangan, a.Tanggal, a.Kode_Supplier, b.Nama as Nm_Supplier, a.Nilai as Jumlah_Bayar, "
            SQL = SQL & "(c.Nama_Bank + ' - '+ c.No_Rekening + '-' + c.Nama_Pemilik) as Rekening, "
            SQL = SQL & "a.Mata_Uang, a.Kurs, a.Total_IDR as Total_Bayar, a.UserId "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Proyek a, Suppliers b, Rekening_Suppliers c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Supplier = c.Kode_Supplier "
            'SQL = SQL & "and a.Mata_Uang = c.Mata_Uang "
            SQL = SQL & "and a.No_Rek_Tujuan = c.No_Rekening "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "

            If CbTransaksi_HrIni.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.Tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CbParamTgl.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilterTgl.Item(cmbTgl.SelectedIndex) & " Between '"
                SQL = SQL & Format(DtpAwal.Value, "yyyy-MM-dd") & "' and '" & Format(DtpAkhir.Value, "yyyy-MM-dd") & "' "
            End If

            If CbParamLain.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilterParamLain.Item(cmbParamLain.SelectedIndex) & " like '%" & Trim(TxtValue.Text) & "%' "
            End If

            SQL = SQL & "order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DataDP.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("Nm_Supplier"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Mata_Uang"))
                    Lv.SubItems.Add(Format(Dr("Kurs"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Bayar"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Total_Bayar"), "N0"))
                    Lv.SubItems.Add(Dr("Rekening"))
                    Lv.SubItems.Add(Dr("UserId"))
                    'hide
                    Lv.SubItems.Add(Dr("Kode_Supplier"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_DataDP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_DataDP.SelectedIndexChanged
        If Lv_DataDP.Items.Count = -1 Or Lv_DataDP.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Lv_DetailDP.Items.Clear() : Lv_Pajak.Items.Clear()
            'SQL = "select a.No_Transaksi, b.No_Fak_PO, c.No_Nota as Keterangan_PO, b.Nilai, b.Nilai_IDR, c.Grand as Jumlah_PO, "
            'SQL = SQL & "(b.Nilai_IDR - c.Grand ) as selisih "
            'SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka a, EMI_Transaksi_Pembayaran_Dimuka_Detail b, EMI_Pembelian_PO_Induk c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            'SQL = SQL & "and b.No_Fak_PO = c.No_Faktur "
            'SQL = SQL & "and a.Status is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & Lv_DataDP.FocusedItem.Text & "' "
            'SQL = SQL & "order by c.Tanggal_Release"

            SQL = "select a.No_Transaksi, b.No_Fak_PO, c.No_Nota as Keterangan_PO, b.Nilai, b.Nilai_IDR, c.Grand as Jumlah_PO, "
            SQL = SQL & "ISNULL(( select SUM(z.nilai_diPakai) from EMI_Detail_DP_Pelunasan z "
            SQL = SQL & "where b.Kode_Perusahaan = z.Kode_Perusahaan and b.No_Transaksi = z.No_Faktur_DP and b.No_Urut = z.urut_DP "
            SQL = SQL & "),0) as Jumlah_Pelunasan "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Proyek a, EMI_Transaksi_Pembayaran_Dimuka_Detail_Proyek b, PO_Pembelian_Proyek c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan  "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Fak_PO = c.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Lv_DataDP.FocusedItem.Text & "' "
            SQL = SQL & "order by a.No_Transaksi, a.Tanggal "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = Lv_DetailDP.Items.Add(Dr("No_Fak_PO"))
                    Lv.SubItems.Add(Dr("Keterangan_PO"))
                    Lv.SubItems.Add(Format(Dr("Nilai_IDR"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Pelunasan"), "N0"))

                    Dim Selisih As Double = Val(HilangkanTanda(Dr("Nilai_IDR"))) - Val(HilangkanTanda(Dr("Jumlah_Pelunasan")))

                    Lv.SubItems.Add(Format(Selisih, "N0"))
                    'hde
                    Lv.SubItems.Add(Dr("No_Transaksi"))

                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub



    Private Sub Lv_DetailDP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_DetailDP.SelectedIndexChanged

        If Lv_DetailDP.Items.Count = 0 Or Lv_DetailDP.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Lv_Pajak.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Transaksi, b.No_Fak_PO, c.Kode_Tarif, c.Persentase, c.Nilai, c.Kode_Akun, c.Flag_PPN "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Proyek a, EMI_Transaksi_Pembayaran_Dimuka_Detail_Proyek b, EMI_Transaksi_Pembayaran_Dimuka_Proyek_Pajak c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Faktur and b.No_Fak_PO = c.No_Faktur_Induk "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Lv_DetailDP.FocusedItem.SubItems(5).Text & "' "
            SQL = SQL & "and b.No_Fak_PO = '" & Lv_DetailDP.FocusedItem.Text & "'"
            SQL = SQL & "order by Flag_PPN DESC, Kode_Tarif "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Lv_Pajak.Columns.Add("Tarif", 120, HorizontalAlignment.Left)
                    Lv_Pajak.Columns.Add("Jenis", 120, HorizontalAlignment.Left)
                    Lv_Pajak.Columns.Add("Persentase", 120, HorizontalAlignment.Left)
                    Lv_Pajak.Columns.Add("Nilai", 180, HorizontalAlignment.Left)

                    Dim Lv As ListViewItem
                    Lv = Lv_Pajak.Items.Add(Dr("Kode_Tarif"))

                    If General_Class.CekNULL(Dr("Flag_PPN")) = "Y" Then
                        Lv.SubItems.Add("PPN")
                    Else
                        Lv.SubItems.Add("PPH")
                    End If

                    Lv.SubItems.Add(HilangkanTanda(Dr("Persentase")))
                    Lv.SubItems.Add(Format(Dr("Nilai"), "N0"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click

        Try

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

            LoadData()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CbParamTgl_CheckedChanged(sender As Object, e As EventArgs) Handles CbParamTgl.CheckedChanged
        If CbParamTgl.Checked Then
            cmbTgl.Enabled = True : DtpAwal.Enabled = False : DtpAkhir.Enabled = False
            CbTransaksi_HrIni.Checked = False
        Else
            cmbTgl.Enabled = False : DtpAwal.Enabled = False : DtpAkhir.Enabled = False
            cmbTgl.SelectedIndex = -1 : DtpAwal.Value = Now.Date : DtpAkhir.Value = Now.Date
        End If
    End Sub

    Private Sub cmbTgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTgl.SelectedIndexChanged
        DtpAwal.Focus()

        If cmbTgl.SelectedIndex = 0 Then
            DtpAwal.Enabled = True : DtpAkhir.Enabled = True
        Else
            DtpAwal.Enabled = False : DtpAkhir.Enabled = False
        End If
    End Sub

    Private Sub CbParamLain_CheckedChanged(sender As Object, e As EventArgs) Handles CbParamLain.CheckedChanged
        If CbParamLain.Checked Then
            cmbParamLain.Enabled = True : TxtValue.Enabled = True
        Else
            cmbParamLain.Enabled = False : TxtValue.Enabled = False
            cmbParamLain.SelectedIndex = -1 : TxtValue.Text = ""
        End If
    End Sub

    Private Sub CbTransaksi_HrIni_CheckedChanged(sender As Object, e As EventArgs) Handles CbTransaksi_HrIni.CheckedChanged
        If CbTransaksi_HrIni.Checked = True Then
            CbParamTgl.Checked = False
            btnCari_Click(CbTransaksi_HrIni, e)
        End If
    End Sub











End Class
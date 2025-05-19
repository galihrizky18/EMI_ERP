Public Class EMI_DownPayment_Binding_Asset
    Dim Jenis = "Display_Production_Order"
    Public fno_po As String

    Dim arrInisialFaktur, arrIdRekening As New ArrayList

    Dim LvKode_So As String
    Dim LvKode_Bahan As String
    Dim LvNama_Bahan As String
    Dim LvNilai_Formula As String
    Dim LvNilai_Produksi As String
    Dim LvSatuan As String

    Dim CellKode_So As Integer = 0
    Dim CellKode_Bahan As Integer = 1
    Dim CellNama_Bahan As Integer = 2
    Dim CellNilai_Formula As Integer = 3
    Dim CellNilai_Produksi As Integer = 4
    Dim CellSatuan As Integer = 5

    Dim LvKode_So_Pckg As String
    Dim LvKode_Bahan_Pckg As String
    Dim LvNama_Bahan_Pckg As String
    Dim LvNilai_Formula_Pckg As String
    Dim LvNilai_Produksi_Pckg As String
    Dim LvSatuan_Pckg As String

    Dim CellKode_So_Pckg As Integer = 0
    Dim CellKode_Bahan_Pckg As Integer = 1
    Dim CellNama_Bahan_Pckg As Integer = 2
    Dim CellNilai_Formula_Pckg As Integer = 3
    Dim CellNilai_Produksi_Pckg As Integer = 4
    Dim CellSatuan_Pckg As Integer = 5

    Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()

        LvSupplier.Location = New Point(171, 54)

    End Sub

    Private Sub kosong()

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

            CmbMUA.Items.Clear()
            SQL = "select Kode_Mata_uang from Mata_Uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Mata_Uang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbMUA.Items.Add(Dr("Kode_Mata_uang"))
                Loop
            End Using

            CmbLokasi.Items.Clear() : arrInisialFaktur.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            CmbLokasi.Text = Lokasi

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CmbNoPO.Items.Clear()

        TxtNilaiPO.Text = ""
        CmbNoFakturDp.Items.Clear()
        TxtKodeSupplier.Text = ""
        TxtNamaSupplier.Text = ""
        TxtNilaiDP.Text = ""
        TxtKursDP.Text = ""
        TxtSisaDP.Text = ""
    End Sub

    'Private Sub get_no_faktur()

    '    TxtFakturPembayaran.Text = fDownPay & arrInisialFaktur.Item(CmbLokasi.SelectedIndex) & "-" & Format(Dtp1.Value, "MM/yy") & "-" &
    '                                 General_Class.Get_Last_Number2("emi_transaksi_pembayaran_dimuka", "no_transaksi", Jumlah_Digit,
    '                                 "Kode_perusahaan", KodePerusahaan,
    '                                 "And", "substring(no_transaksi,1," & Len(fPO_EMI) + Len(arrInisialFaktur.Item(CmbLokasi.SelectedIndex)) + 6 & ")", fDownPay & arrInisialFaktur.Item(CmbLokasi.SelectedIndex) & "-" & Format(Dtp1.Value, "MM/yy"))
    'End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If TxtNilaiPO.Text.Trim.Length = 0 Then
            MessageBox.Show("Nilai PO harus di isi!", Judul, MessageBoxButtons.OK)
            TxtNilaiPO.Focus()
            Exit Sub
        ElseIf CmbNoPO.Text.Trim.Length = 0 Then
            MessageBox.Show("No PO harus di isi!", Judul, MessageBoxButtons.OK)
            CmbNoPO.Focus()
            Exit Sub
        ElseIf CmbNoFakturDp.Text.Trim.Length = 0 Then
            MessageBox.Show("No Pembelian di muka harus di isi!", Judul, MessageBoxButtons.OK)
            CmbNoFakturDp.Focus()
            Exit Sub
        End If
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim noFakturPO_Fix As String = ""

            SQL = "select a.Nilai, "
            SQL = SQL & "isnull((select sum(x.nilai) from EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = x.No_Transaksi),0) as nilai_sudah_dp "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Asset a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & CmbNoFakturDp.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dim total_sudah_dp As Double = Val(Dr("nilai_sudah_dp")) + Val(TxtNilaiPO.Text)

                    Dim sisa_dp As Double = Val(Dr("nilai")) - Val(Dr("nilai_sudah_dp"))

                    If total_sudah_dp > Dr("nilai") Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jumlah melebihi batas DP, sisa DP =  " & Format(sisa_dp, "N2"), Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If total_sudah_dp = Dr("nilai") Then
                        Dr.Close()
                        SQL = "Update EMI_Transaksi_Pembayaran_Dimuka_Asset set Flag_PO='Y' "
                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and "
                        SQL = SQL & "no_transaksi='" & CmbNoFakturDp.Text & "'"
                        ExecuteTrans(SQL)
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Terjadi kessalahan, NO Transaksi DP tidak ada! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "insert into EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset(Kode_Perusahaan, no_transaksi, Tanggal, Jam, UserId, "
            SQL = SQL & "No_Fak_PO,Nilai, Nilai_IDR) values ("
            SQL = SQL & "'" & KodePerusahaan & "', '" & CmbNoFakturDp.Text & "', ' " & Format(Dtp1.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & " '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "',  "
            SQL = SQL & "'" & CmbNoPO.Text & "', '" & HilangkanTanda(TxtNilaiPO.Text) & "', '" & HilangkanTanda(TxtNilaiIDRPO.Text) & "' ) "
            ExecuteTrans(SQL)

            'akhir tutup

            Cmd.Transaction.Commit()
            MessageBox.Show("Data berhasil disimpan ", Judul, MessageBoxButtons.OK)

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()

    End Sub

    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub TxtKodeSupplier_KeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub TxtKodeSupplier_TextChanged(sender As Object, e As EventArgs) Handles TxtKodeSupplier.TextChanged
        If TxtKodeSupplier.Text.Trim.Length = 0 Then
            LvSupplier.Visible = False : Exit Sub
        Else
            LvSupplier.Visible = True
        End If

        LvSupplier.Items.Clear()
        Dim lv As New ListViewItem

        Try

            OpenConn()

            SQL = "Select b.kode_supplier, b.nama "
            SQL = SQL & "from suppliers b "
            SQL = SQL & "where  "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_supplier like '%" & TxtKodeSupplier.Text & "%' "

            SQL = SQL & "order by b.kode_supplier"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvSupplier.Items.Add(Dr("kode_supplier"))
                    lv.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKodeSupplier_KeyDown_1(sender As Object, e As KeyEventArgs) Handles TxtKodeSupplier.KeyDown
        If e.KeyCode = Keys.Down Then LvSupplier.Focus()
    End Sub

    Private Sub LvSupplier_DoubleClick_1(sender As Object, e As EventArgs) Handles LvSupplier.DoubleClick
        If LvSupplier.Items.Count = 0 Then Exit Sub
        Dim Kode As String = LvSupplier.FocusedItem.Text
        Dim Nama As String = LvSupplier.FocusedItem.SubItems(1).Text

        TxtKodeSupplier.Text = Kode
        TxtNamaSupplier.Text = Nama

        LvSupplier.Visible = False
        TxtKodeSupplier_Leave(LvSupplier, e)
        CmbNoPO.Focus()

    End Sub

    Private Sub LvSupplier_KeyDown_1(sender As Object, e As KeyEventArgs) Handles LvSupplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupplier_DoubleClick_1(LvSupplier, e)
        End If
    End Sub

    Private Sub BtnFormulator_Refresh_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Refresh.Click
        kosong()
    End Sub

    Private Sub TxtNilai_TextChanged(sender As Object, e As EventArgs) Handles TxtNilaiPO.TextChanged
        If TxtNilaiPO.Text.Trim.Length = 0 Or TxtKursDP.Text.Trim.Length = 0 Then
            TxtNilaiIDRPO.Text = 0
            Exit Sub
        End If

        If Val(HilangkanTanda(TxtNilaiPO.Text)) > Val(HilangkanTanda(TxtSisaDP.Text)) Then
            TxtNilaiPO.Text = ""
            TxtNilaiIDRPO.Text = ""

            MessageBox.Show("Nilai Input Melebihi Jumlah! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        TxtNilaiIDRPO.Text = Format(Val(TxtNilaiPO.Text) * Val(TxtKursDP.Text), "N0")
    End Sub

    Private Sub CmbNoFakturDp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNoFakturDp.SelectedIndexChanged
        If CmbNoFakturDp.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select a.Nilai, a.kurs, Mata_Uang, "
            SQL = SQL & "isnull((select sum(x.nilai) from EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = x.No_Transaksi and x.status is null and x.Persen_DP is null ),0) as nilai_sudah_dp "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Asset a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & CmbNoFakturDp.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    TxtNilaiDP.Text = Format(Dr("Nilai"), "N2")
                    TxtKursDP.Text = Format(Dr("kurs"), "N2")
                    TxtSisaDP.Text = Format(Dr("Nilai") - Dr("nilai_sudah_dp"), "N2")
                    CmbMUA.Text = Dr("Mata_Uang")

                    CmbNoPO.SelectedIndex = -1
                    TxtNilaiPO.Text = ""
                    TxtNilaiIDRPO.Text = ""
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Terjadi kessalahan, NO Transaksi DP tidak ada! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub TxtKodeSupplier_Leave(sender As Object, e As EventArgs) Handles TxtKodeSupplier.Leave

        If TxtKodeSupplier.Text.Length = 0 Then
            LvSupplier.Visible = False : Exit Sub
        Else
            LvSupplier.Visible = True
        End If
        If LvSupplier.Focused = True Then Exit Sub
        Try
            OpenConn()

            CmbNoFakturDp.Items.Clear()

            SQL = ";with cte as ("
            SQL = SQL & "select a.No_Transaksi,a.kode_supplier, a.Flag_PO, a.Nilai - "
            SQL = SQL & "ISNULL((select SUM(x.nilai) from EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = x.No_Transaksi and x.Persen_DP is null),0) as nilai "
            SQL = SQL & "	from EMI_Transaksi_Pembayaran_Dimuka_Asset a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Supplier = '" & TxtKodeSupplier.Text & "' "
            SQL = SQL & ") select * From cte  where Flag_PO is null and nilai <> 0  "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbNoFakturDp.Items.Add(Dr("no_transaksi"))
                Loop
            End Using

            CmbNoPO.Items.Clear()
            SQL = ";with cte as ( "
            SQL = SQL & "select No_Faktur, "
            SQL = SQL & "isnull((select top(1) 'Y' from  EMI_Transaksi_Pembayaran_Dimuka_Asset x, EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset y "
            SQL = SQL & "where x.kode_perusahaan = y.kode_perusahaan and x.no_transaksi = y.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = y.Kode_Perusahaan and a.No_Faktur = y.No_Fak_PO and y.Persen_DP is null "
            SQL = SQL & "), null) as Flag "
            SQL = SQL & "from EMI_Pembelian_PO_Induk_Barang_Lain a where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & TxtKodeSupplier.Text & "'  "
            SQL = SQL & ") select * from cte where Flag is null order by no_faktur"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbNoPO.Items.Add(Dr("no_faktur"))
                Loop
            End Using

            LvSupplier.Visible = False
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

End Class
Imports System.Globalization

Public Class N_EMI_Transaksi_Trial_Request_Material

    Dim Flag_Opname As Boolean = False

    Dim judulForm As String = "Request Material General"
    Dim lv As New ListViewItem
    Dim LInisial As New ArrayList
    Dim LSOReq, LSOSup As New ArrayList
    Public isError As String
    Dim total_hpp_metode_B As Double
    Dim fId_group As String

    Dim LvKodeBarang, LvNamaBarang, LvJumlah, LvSatuan, LvKet As String

    'Dim item_Lokasi As Integer = 0
    Dim item_KdBarang As Integer = 0
    Dim item_NamaBarang As Integer = 1
    Dim item_Jumlah As Integer = 2
    Dim item_Satuan As Integer = 3
    Dim item_Keterangan As Integer = 4

    Dim SelectedFormula As String

    Private Sub Pengeluaran_Barang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        CmbSO_Req.Focus()
    End Sub

    Private Sub Pengeluaran_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As New Point(28, 221)
        LvBrg.Location = x

        LvBarang.Columns.Clear() : LvBarang.Items.Clear()
        'LvBarang.Columns.Add("Lokasi", 160, HorizontalAlignment.Left)
        LvBarang.Columns.Add("Kode Barang", 200, HorizontalAlignment.Left)
        'LvBarang.Columns.Add("Nama ", 330, HorizontalAlignment.Left)
        LvBarang.Columns.Add("Nama ", 0, HorizontalAlignment.Left)
        LvBarang.Columns.Add("Jumlah", 200, HorizontalAlignment.Right)
        LvBarang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        LvBarang.Columns.Add("Keterangan", 400, HorizontalAlignment.Left)
        LvBarang.View = View.Details

        LvBrg.Columns.Clear() : LvBrg.Items.Clear()
        LvBrg.Columns.Add("Kode Barang", 180, HorizontalAlignment.Left)
        'LvBrg.Columns.Add("Nama", 300, HorizontalAlignment.Left)
        LvBrg.Columns.Add("Nama", 0, HorizontalAlignment.Left)
        LvBrg.Columns.Add("Jumlah Stock", 180, HorizontalAlignment.Right)
        LvBrg.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        LvBrg.View = View.Details


        Kosong()
        CmbSO_Req.Focus()
    End Sub

    Private Sub Kosong()



        get_jam()

        Try
            OpenConn()

            get_no_faktur()


            SQL = "select Flag_Opname from init where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Opname")) = "Y" Then
                        Flag_Opname = True
                    Else
                        Flag_Opname = False
                    End If
                End If
            End Using

            CmbSO_Req.Enabled = True
            CmbSo_Sup.Enabled = True
            TxtKodeBarang.Enabled = True
            TxtJlh.Enabled = False
            TxtKet.Enabled = False

            Txt_Ket.Text = ""
            TxtKodeBarang.Text = "" : TxtNamaBarang.Text = "" : Txt_Stock.Text = "0" : TxtSatuan.Text = "" : TxtKet.Text = "" : TxtTotal.Text = "" : Txt_Stock.Text = ""

            SelectedFormula = ""

            CmbSO_Req.Items.Clear() : LSOReq.Clear() : CmbSo_Sup.Items.Clear() : LSOSup.Clear()
            SQL = "Select kode_stock_owner, keterangan, Flag_Gudang_Lab from stock_owner_gudang order by kode_stock_owner "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    If General_Class.CekNULL(dr("Flag_Gudang_Lab")) = "Y" Then
                        CmbSO_Req.Items.Add(dr("keterangan")) : LSOReq.Add(dr("kode_stock_owner"))
                    Else
                        CmbSo_Sup.Items.Add(dr("keterangan")) : LSOSup.Add(dr("kode_stock_owner"))
                    End If
                Loop
            End Using

            LvBarang.Items.Clear()
            HitungTotal()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur()
        Dim FRM As String = "RMG"

        TxtNoFaktur.Text = FRM & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Material_Requisition_General", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(FRM) + 4 & ")", FRM & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Get_Isi_Listview(ByVal i As Integer)
        LvKodeBarang = LvBarang.Items(i).SubItems(item_KdBarang).Text
        LvNamaBarang = LvBarang.Items(i).SubItems(item_NamaBarang).Text
        LvJumlah = LvBarang.Items(i).SubItems(item_Jumlah).Text
        LvSatuan = LvBarang.Items(i).SubItems(item_Satuan).Text
        LvKet = LvBarang.Items(i).SubItems(item_Keterangan).Text
    End Sub

    Private Sub TxtKodeBarang_TextChanged(sender As Object, e As EventArgs) Handles TxtKodeBarang.TextChanged
        If CmbSO_Req.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Gudang Request", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Req.Focus() : Exit Sub
        ElseIf CmbSo_Sup.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Gudang Supply", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSo_Sup.Focus() : Exit Sub
        End If

        If TxtKodeBarang.Text.Trim.Length = 0 Then
            LvBrg.Visible = False : Exit Sub
        Else
            LvBrg.Visible = True
        End If

        Try

            OpenConn()

            Dim lv As New ListViewItem
            LvBrg.Items.Clear()

            SQL = "select top(20) a.Kode_Stock_Owner, a.Kode_Barang, a.Nama, a.Good_Stock, a.Satuan, "
            SQL = SQL & "dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, a.Satuan, "
            SQL = SQL & "(select top 1 z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display = 'Y'), "
            SQL = SQL & "a.Good_Stock ) as Good_Stock_Besar,  "
            SQL = SQL & "ISNULL((select top 1 z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display = 'Y'), '-') as Satuan_Besar "
            SQL = SQL & "from barang a "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_stock_owner = '" & LSOSup(CmbSo_Sup.SelectedIndex) & "' "
            SQL = SQL & "and (a.Kode_Barang like '%" & TxtKodeBarang.Text.Trim & "%' or a.Nama like '%" & TxtKodeBarang.Text.Trim & "%') "
            SQL = SQL & "order by a.nama, a.Good_Stock "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvBrg.Items.Add(Dr("Kode_Barang"))
                    'lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add("X")

                    If Flag_Opname Then
                        lv.SubItems.Add(0)
                    Else
                        lv.SubItems.Add(Format(Dr("Good_Stock_Besar"), "N2"))
                    End If
                    lv.SubItems.Add(Dr("Satuan_Besar"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKodeBarang_Leave(sender As Object, e As EventArgs) Handles TxtKodeBarang.Leave
        If CmbSO_Req.SelectedIndex = -1 AndAlso CmbSo_Sup.SelectedIndex = -1 Then Exit Sub

        If TxtKodeBarang.Text.Trim.Length = 0 Then Exit Sub
        If LvBrg.Focused = True Then Exit Sub

        Try

            OpenConn()
            Cek_Flagging()

            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Nama, a.Good_Stock, a.Satuan, "
            SQL = SQL & "dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, a.Satuan, "
            SQL = SQL & "(select top 1 z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display = 'Y'), "
            SQL = SQL & "a.Good_Stock ) as Good_Stock_Besar,  "
            SQL = SQL & "ISNULL((select top 1 z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display = 'Y'), '-') as Satuan_Besar "
            SQL = SQL & "from barang a "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_stock_owner = '" & LSOSup(CmbSo_Sup.SelectedIndex) & "' "
            SQL = SQL & "and a.Kode_Barang = '" & TxtKodeBarang.Text.Trim & "' "
            SQL = SQL & "order by a.nama, a.Good_Stock "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtKodeBarang.Text = Dr("Kode_Barang")
                    TxtNamaBarang.Text = Dr("Nama")
                    If Flag_Opname Then
                        Txt_Stock.Text = 0
                    Else
                        Txt_Stock.Text = Format(Dr("Good_Stock_Besar"), "N2")
                    End If
                    TxtSatuan.Text = Dr("Satuan_Besar")
                    TxtJlh.Focus()
                Else
                    MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TxtKodeBarang.Text = "" : TxtNamaBarang.Text = "" : TxtSatuan.Text = ""
                    Txt_Stock.Text = "" : TxtKet.Text = "" : TxtJlh.Text = ""
                    TxtKodeBarang.Focus()
                End If
                LvBrg.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKodeBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKodeBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKodeBarang.Text.Trim.Length = 0 Then TxtNamaBarang.Focus()
            TxtKodeBarang_Leave(TxtKodeBarang, e)
            LvBrg.Visible = False
        End If
    End Sub

    Private Sub TxtKodeBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKodeBarang.KeyDown
        If e.KeyCode = Keys.Down Then LvBrg.Focus()
    End Sub

    Private Sub TxtNamaBarang_TextChanged(sender As Object, e As EventArgs) Handles TxtNamaBarang.TextChanged
        If CmbSO_Req.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Gudang Request", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Req.Focus() : Exit Sub
        ElseIf CmbSo_Sup.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Gudang Supply", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSo_Sup.Focus() : Exit Sub
        End If
        If TxtNamaBarang.Text.Trim.Length = 0 Then
            LvBrg.Visible = False : Exit Sub
        Else
            LvBrg.Visible = True
        End If

        Try
            OpenConn()
            Cek_Flagging()

            Dim lv As New ListViewItem
            LvBrg.Items.Clear()

            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Nama, a.Good_Stock, a.Satuan, "
            SQL = SQL & "dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, a.Satuan, "
            SQL = SQL & "(select top 1 z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display = 'Y'), "
            SQL = SQL & "a.Good_Stock ) as Good_Stock_Besar,  "
            SQL = SQL & "ISNULL((select top 1 z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display = 'Y'), '-') as Satuan_Besar "
            SQL = SQL & "from barang a "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_stock_owner = '" & LSOSup(CmbSo_Sup.SelectedIndex) & "' "
            SQL = SQL & "and a.nama like '%" & TxtNamaBarang.Text.Trim & "%' "
            SQL = SQL & "order by a.nama, a.Good_Stock "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvBrg.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                    If Flag_Opname Then
                        lv.SubItems.Add(0)
                    Else
                        lv.SubItems.Add(Format(Dr("Good_Stock_Besar"), "N2"))
                    End If
                    lv.SubItems.Add(Dr("Satuan_Besar"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtNamaBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNamaBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            TxtKodeBarang_Leave(TxtNamaBarang, e)
            LvBrg.Visible = False
        End If
    End Sub

    Private Sub TxtNamaBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtNamaBarang.KeyDown
        If e.KeyCode = Keys.Down Then LvBrg.Focus()
    End Sub

    Private Sub LvBrg_DoubleClick(sender As Object, e As EventArgs) Handles LvBrg.DoubleClick
        Dim KodeSO As String = LvBrg.FocusedItem.Text
        Dim Kode As String = LvBrg.FocusedItem.SubItems(0).Text

        TxtKodeBarang.Text = Kode

        CmbSO_Req.Enabled = False
        CmbSo_Sup.Enabled = False

        LvBrg.Visible = False
        TxtKodeBarang_Leave(LvBrg, e)
        Txt_Stock.Focus()

        TxtJlh.Enabled = True
        TxtKet.Enabled = True
    End Sub

    Private Sub LvBrg_KeyDown(sender As Object, e As KeyEventArgs) Handles LvBrg.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvBrg_DoubleClick(LvBrg, e)
        End If
    End Sub

    Private Sub TxtJlh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Stock.KeyPress, TxtJlh.KeyPress
        If e.KeyChar = Chr(13) Then TxtKet.Focus()
        If Not (e.KeyChar >= "0"c And e.KeyChar <= "9"c Or e.KeyChar = Chr(8) Or e.KeyChar = "."c) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TxtKet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKet.KeyPress
        If e.KeyChar = Chr(13) Then BtnOK.Focus()
    End Sub

    Private Sub TxtJlh_Leave(sender As Object, e As EventArgs) Handles TxtJlh.Leave
        ''======================
        ''=     SET FORMAT     =
        ''======================
        Dim culture As CultureInfo = CultureInfo.CurrentCulture

        If Not TxtJlh.Text.Trim.Length = 0 Then
            Dim cellKuantity As String = TxtJlh.Text

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", culture)

            TxtJlh.Text = formattedValue

        End If
    End Sub

    Private Sub TxtJlh_Enter(sender As Object, e As EventArgs) Handles TxtJlh.Enter
        '======================
        '=     SET FORMAT     =
        '======================

        If Not TxtJlh.Text.Trim.Length = 0 Then
            Dim cellKuantity As String = TxtJlh.Text

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            TxtJlh.Text = nilai
        End If
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        TxtKodeBarang.Text = "" : TxtNamaBarang.Text = "" : Txt_Stock.Text = "" : TxtSatuan.Text = "" : TxtKet.Text = "" : TxtJlh.Text = ""
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Kosong()
        CmbSO_Req.Focus()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If CmbSO_Req.Text.Trim.Length = 0 Then
            MessageBox.Show("lokasi Request harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Req.Focus() : Exit Sub
        ElseIf CmbSo_Sup.Text.Trim.Length = 0 Then
            MessageBox.Show("lokasi Supply harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSo_Sup.Focus() : Exit Sub
        ElseIf TxtKodeBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKodeBarang.Focus() : Exit Sub
        ElseIf TxtNamaBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtNamaBarang.Focus() : Exit Sub
        ElseIf TxtJlh.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtJlh.Focus() : Exit Sub
        ElseIf TxtSatuan.Text.Trim.Length = 0 Then
            MessageBox.Show("Satun barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtSatuan.Focus() : Exit Sub
        ElseIf TxtKet.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKet.Focus() : Exit Sub
        End If

        For i As Integer = 0 To LvBarang.Items.Count - 1
            If TxtKodeBarang.Text.Trim.ToUpper = LvBarang.Items(i).SubItems(item_KdBarang).Text Then
                MessageBox.Show("Kode barang sudah anda masukkan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtKodeBarang.Focus() : Exit Sub
            End If
        Next

        'If Val(HilangkanTanda(TxtJlh.Text)) > Val(HilangkanTanda(Txt_Stock.Text)) Then
        '    MessageBox.Show("Jumlah barang tidak boleh melebihi stock!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    TxtJlh.Focus() : Exit Sub
        'End If

        Dim Lv As ListViewItem
        Lv = LvBarang.Items.Add(TxtKodeBarang.Text)
        'Lv.SubItems.Add(TxtNamaBarang.Text.Trim)
        Lv.SubItems.Add("X")
        Lv.SubItems.Add(Format(Val(HilangkanTanda(TxtJlh.Text.Trim)), "N2"))
        Lv.SubItems.Add(TxtSatuan.Text.Trim)
        Lv.SubItems.Add(TxtKet.Text.Trim)

        HitungTotal()

        TxtKodeBarang.Text = "" : TxtNamaBarang.Text = "" : Txt_Stock.Text = "" : TxtJlh.Text = "" : TxtSatuan.Text = "" : TxtKet.Text = ""

    End Sub

    Private Sub HitungTotal()
        Dim Total As Integer = 0
        For i As Integer = 0 To LvBarang.Items.Count - 1
            Total += Val(HilangkanTanda(LvBarang.Items(i).SubItems(item_Jumlah).Text))
        Next
        TxtTotal.Text = Format(Total, "N2")
    End Sub



    Private Sub LvBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles LvBarang.KeyDown
        If LvBarang.Items.Count = 0 Or LvBarang.FocusedItem.Index = -1 Then
            Exit Sub
        End If

        Dim currentRow = LvBarang.FocusedItem.Index

        If e.KeyCode = Keys.Delete Then

            Dim Hapus1 As String = MessageBox.Show("Apakah Anda Yakin, Ingin Menghapus Data ?", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Hapus1 = vbNo Then Exit Sub

            LvBarang.Items.RemoveAt(currentRow)

        End If

        HitungTotal()
    End Sub

    Private Sub CmbSO_Req_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSO_Req.SelectedIndexChanged
        If CmbSO_Req.Items.Count = 0 Then Exit Sub

        If CmbSO_Req.SelectedIndex = CmbSo_Sup.SelectedIndex Then
            MessageBox.Show("Gudang Request dan Gudang Supply tidak boleh sama!", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Req.SelectedIndex = -1 : CmbSO_Req.Focus() : Exit Sub
        End If
    End Sub

    Private Sub CmbSo_Sup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSo_Sup.SelectedIndexChanged
        If CmbSo_Sup.Items.Count = 0 Then Exit Sub

        If CmbSo_Sup.SelectedIndex = CmbSO_Req.SelectedIndex Then
            MessageBox.Show("Gudang Request dan Gudang Supply tidak boleh sama!", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSo_Sup.SelectedIndex = -1 : CmbSo_Sup.Focus() : Exit Sub
        End If
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        If LvBarang.Items.Count = 0 Then
            MessageBox.Show("Tidak ada Data yang disimpan!", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvBarang.Focus() : Exit Sub
        ElseIf Txt_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show("Lakukan Input Keterangan Terlebih Dahulu!", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Ket.Focus() : Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            get_no_faktur()

            Dim NoTransaksiFormula As String = If(SelectedFormula.Trim.Length = 0, "NULL", $"'{SelectedFormula}'")

            '========================
            '=     INSERT INDUK     =
            '========================
            SQL = "insert into Emi_Material_Requisition_General (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner_Req, Kode_Stock_Owner_Sup, Keterangan, Total, Tanggal, Jam, UserId, No_Transaksi_Formula) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNoFaktur.Text.Trim & "', '" & LSOReq(CmbSO_Req.SelectedIndex) & "', '" & LSOSup(CmbSo_Sup.SelectedIndex) & "', "
            SQL = SQL & "'" & Txt_Ket.Text & "', '" & HilangkanTanda(TxtTotal.Text) & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', " & NoTransaksiFormula & ")"
            ExecuteTrans(SQL)

            For i As Integer = 0 To LvBarang.Items.Count - 1

                Get_Isi_Listview(i)

                ''============================
                ''=     GET SATUAN KECIL     =
                ''============================
                'Dim SatuanKecil As String = ""
                'SQL = "select Satuan from Barang_Detail_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and Kode_barang = '" & LvKodeBarang & "' and Flag_Tampil_Display is null"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        SatuanKecil = Dr("Satuan")
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Satuan Kecil Tidak di Temukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                ''===============================
                ''=     CONVERT NILAI KECIL     =
                ''===============================
                'Dim jumlahKecil As Double = 0
                'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & LvKodeBarang & "', '" & LvSatuan & "', "
                'SQL = SQL & "'" & SatuanKecil & "', '" & HilangkanTanda(LvJumlah) & "' ) as hasil "
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        jumlahKecil = Decimal.Parse(Dr("hasil"))
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Gagal Ubah Nilai Kecil", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                '=========================
                '=     INSERT DETAIL     =
                '=========================
                SQL = "insert into Emi_Material_Requisition_General_Detail (Kode_Perusahaan, No_Faktur, Kode_Barang, Jumlah, Satuan, Jumlah_Barang, Satuan_Barang, Keterangan) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNoFaktur.Text.Trim & "', '" & LvKodeBarang & "', '" & HilangkanTanda(LvJumlah) & "', "
                'SQL = SQL & "'" & LvSatuan & "', '" & HilangkanTanda(jumlahKecil) & "', '" & SatuanKecil & "', '" & LvKet & "')"
                SQL = SQL & "'" & LvSatuan & "', '" & HilangkanTanda(LvJumlah) & "', '" & LvSatuan & "', '" & LvKet & "')"
                ExecuteTrans(SQL)


            Next


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Btn_Get_Forumla_Click(sender As Object, e As EventArgs) Handles Btn_Get_Forumla.Click
        If CmbSO_Req.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Lokasi Request Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Req.DroppedDown = True
            CmbSO_Req.Focus()
            Exit Sub
        ElseIf CmbSo_Sup.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Lokasi Supply Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSo_Sup.DroppedDown = True
            CmbSo_Sup.Focus()
            Exit Sub
        ElseIf LvBarang.Items.Count <> 0 Then
            MessageBox.Show("Lakukan Refresh Data Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            '===========================
            '=     CEK ROLE BUTTON     =
            '===========================
            If CekButtonRole("Get_Formula_RM_Trial") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Get Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        N_EMI_SD_Trial_Request_Material.ShowDialog()


    End Sub


    Public Sub ShowBahanByFormula(ByVal NoFormula As String, ByVal JumlahKebutuhan As String, ByVal SatuanKebutuhan As String)
        If NoFormula.Trim.Length = 0 Then
            MessageBox.Show("Terjadi Kesalahan, Formula Gagal Diselect", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            '==============================
            '=     CEK STATUS FORMULA     =
            '==============================
            SQL = "select Status from Emi_Transaksi_Formulator "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{NoFormula}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, No Formula Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Terjadi Kesalahan, No Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '============================
            '=     GET DETAIL BAHAN     =
            '============================


            LvBarang.Items.Clear()
            'SQL = "select b.Kode_Barang, c.nama as Nama_Barang, b.jumlah, b.satuan "
            'SQL &= $"from Emi_Transaksi_Formulator a "
            'SQL &= $"inner join EMI_Transaksi_Formulator_Detail_Bahan b on a.kode_perusahaan = b.Kode_Perusahaan and a.no_faktur = b.No_Faktur "
            'SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            'SQL &= $"where a.Status is null "
            'SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
            'SQL &= $"and a.No_Faktur = '{NoFormula}' "
            'SQL &= $"order by a.Kode_Barang "


            SQL = "select b.Kode_Barang, c.nama as Nama_Barang, b.jumlah, b.satuan, "
            SQL &= $"isnull((( "
            SQL &= $"(dbo.ubah_satuan('{KodePerusahaan}', 'masa', b.kode_barang, '{SatuanKebutuhan}', b.satuan, {JumlahKebutuhan})) / "
            SQL &= $"(dbo.ubah_satuan('{KodePerusahaan}', 'masa', a.kode_barang, a.Satuan_Hasil, b.satuan, a.Hasil)) "
            SQL &= $") * b.jumlah "
            SQL &= $"), 0) as Jumlah_Kebutuhan "
            SQL &= $"from Emi_Transaksi_Formulator a "
            SQL &= $"inner join EMI_Transaksi_Formulator_Detail_Bahan b on a.kode_perusahaan = b.Kode_Perusahaan and a.no_faktur = b.No_Faktur "
            SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"where a.Status is null "
            SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{NoFormula}' "
            SQL &= $"order by a.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim KeteranganDefault As String = $"Request Material Trial kode barang {Dr("Kode_Barang")}; Tranasksi {TxtNoFaktur.Text.Trim} "

                    Dim Lv As ListViewItem
                    Lv = LvBarang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    'Lv.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Kebutuhan"), "N4"))
                    Lv.SubItems.Add(Dr("satuan"))
                    Lv.SubItems.Add(KeteranganDefault)
                Loop
            End Using



            TxtKodeBarang.Enabled = False : TxtKodeBarang.Text = ""
            Txt_Stock.Enabled = False : Txt_Stock.Text = ""
            TxtJlh.Enabled = False : TxtJlh.Text = ""
            TxtSatuan.Enabled = False : TxtSatuan.Text = ""
            TxtKet.Enabled = False : TxtKet.Text = ""

            LvBrg.Visible = False

            SelectedFormula = NoFormula


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub







    '================================================================================================================================================
    '=     UTILITY
    '================================================================================================================================================

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub


End Class

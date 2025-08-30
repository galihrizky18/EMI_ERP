Public Class Pengeluaran_Barang
    Dim lv As New ListViewItem
    Dim LInisial As New ArrayList
    Dim LSO As New ArrayList
    Dim LvSO, LvKodeBarang, LvNamaBarang, LvJumlah, LvKet, LvId_group As String
    Public isError As String
    Dim total_hpp_metode_B As Double
    Dim fId_group As String

    Dim Flag_Opname As Boolean = False

    Private Sub Pengeluaran_Barang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CmbSOBrg.Focus()
    End Sub

    Private Sub Pengeluaran_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As New Point(120, 185)
        LvCost.Location = x

        x = New Point(17, 263)
        LvBrg.Location = x

        Kosong()
        CmbSOBrg.Focus()
    End Sub

    Private Sub Kosong()
        LvBarang.Columns(0).Width = 160
        LvBarang.Columns(1).Width = 146
        LvBarang.Columns(2).Width = 359
        LvBarang.Columns(3).Width = 66
        LvBarang.Columns(4).Width = 95
        LvBarang.Columns(5).Width = 254
        LvBarang.Columns(6).Width = 0
        LvBarang.Items.Clear()

        LvCost.Columns(0).Width = 0
        LvCost.Columns(1).Width = 413
        LvCost.Items.Clear()

        LvBrg.Columns(0).Width = 160
        LvBrg.Columns(1).Width = 145
        LvBrg.Columns(2).Width = 360
        LvBrg.Columns(3).Width = 68
        LvBrg.Columns(4).Width = 0


        LvCost.Visible = False : LvBrg.Visible = False

        Try
            OpenConn()


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

            CmbSO.Items.Clear() : LInisial.Clear()
            SQL = "Select kode_stock_owner,inisial_faktur from stock_owner order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSO.Items.Add(dr("kode_stock_owner")) : LInisial.Add(dr("inisial_faktur"))
                Loop
            End Using
            CmbSO.Text = Lokasi

            get_jam()

            Tgl1.Value = tgl_skg

            get_no_faktur()

            TxtKetPB.Text = "" : TxtIDCost.Text = "" : TxtKodeCost.Text = ""

            CmbSOBrg.Items.Clear() : LSO.Clear()
            SQL = "Select kode_stock_owner from stock_owner_gudang order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSOBrg.Items.Add(dr("kode_stock_owner")) : LSO.Add(dr("kode_stock_owner"))
                Loop
            End Using
            CmbSOBrg.Text = Lokasi
            CmbSOBrg.Enabled = True

            TxtKodeBarang.Text = "" : TxtNamaBarang.Text = "" : TxtJlh.Text = "0"
            TxtSatuan.Text = "" : TxtKet.Text = "" : TxtTotal.Text = "0"

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
        TxtNoFaktur.Text = fPO_EMI & LInisial.Item(CmbSO.SelectedIndex) & "-" & Format(Tgl1.Value, "MM/yy") & "-" &
                                    General_Class.Get_Last_Number2("EMI_Pengeluaran_Barang", "no_faktur", Jumlah_Digit,
                                    "Kode_perusahaan", KodePerusahaan,
                                    "And", "substring(no_faktur,1," & Len(fPO_EMI) + Len(LInisial.Item(CmbSO.SelectedIndex)) + 6 & ")",
                                    fPO_EMI & LInisial.Item(CmbSO.SelectedIndex) & "-" & Format(Tgl1.Value, "MM/yy"))
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then TxtKetPB.Focus()
    End Sub

    Private Sub TxtKetPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKetPB.KeyPress
        If e.KeyChar = Chr(13) Then TxtKodeCost.Focus()
    End Sub

    Private Sub TxtKodeCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKodeCost.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKodeCost.Text.Trim.Length = 0 Then TxtKodeBarang.Focus()
            TxtKodeCost_Leave(TxtKodeCost, e)
            LvCost.Visible = False
        End If
    End Sub

    Private Sub TxtKodeCost_TextChanged(sender As Object, e As EventArgs) Handles TxtKodeCost.TextChanged
        If TxtKodeCost.Text.Trim.Length = 0 Then
            LvCost.Visible = False : Exit Sub
        Else
            LvCost.Visible = True
        End If

        Try
            OpenConn()

            LvCost.Items.Clear()

            SQL = "Select id_cost_center,kode_cost_center From EMI_Master_Cost_Center "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND Kode_Cost_Center LIKE '" & TxtKodeCost.Text & "%' "
            SQL = SQL & "ORDER BY Kode_Cost_Center"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvCost.Items.Add(Dr("id_cost_center"))
                    lv.SubItems.Add(Dr("kode_cost_center"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKodeCost_Leave(sender As Object, e As EventArgs) Handles TxtKodeCost.Leave
        If TxtKodeCost.Text.Trim.Length = 0 Then Exit Sub
        If LvCost.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "Select id_cost_center,kode_cost_center From EMI_Master_Cost_Center "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND id_cost_center = '" & TxtIDCost.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtIDCost.Text = Dr("id_cost_center")
                    TxtKodeCost.Text = Dr("kode_cost_center")
                    TxtKodeBarang.Focus()
                Else
                    MessageBox.Show("Kode cost center tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TxtIDCost.Text = "" : TxtKodeCost.Text = ""
                    TxtKodeCost.Focus()
                End If
                LvCost.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKodeCost_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKodeCost.KeyDown
        If e.KeyCode = Keys.Down Then LvCost.Focus()
    End Sub

    Private Sub TxtKodeBarang_TextChanged(sender As Object, e As EventArgs) Handles TxtKodeBarang.TextChanged
        If CmbSOBrg.SelectedIndex = -1 Then Exit Sub

        If TxtKodeBarang.Text.Trim.Length = 0 Then
            LvBrg.Visible = False : Exit Sub
        Else
            LvBrg.Visible = True
        End If

        Try
            Cek_Flagging()

            OpenConn()

            Dim lv As New ListViewItem
            LvBrg.Items.Clear()

            SQL = "select top 20 a.kode_stock_owner,a.kode_barang,a.nama,a.satuan,a.id_group_jenis "
            SQL = SQL & "from barang as a inner join emi_group_jenis as gj on a.id_group_jenis = gj.id_group_jenis "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.Kode_Barang LIKE '" & TxtKodeBarang.Text & "%' and "
            SQL = SQL & "a.kode_stock_owner = '" & CmbSOBrg.Text & "' and "

            'SQL = SQL & "gj.Flag_Packaging = '" & Flag_Packaging & "' and gj.Flag_Raw_Material = '" & Flag_Raw_Material & "' and "
            'SQL = SQL & "gj.Flag_Finished_Good = '" & Flag_Finished_Good & "' and gj.Flag_Sample = '" & Flag_Sample & "' and "
            'SQL = SQL & "gj.Flag_Semi_FG = '" & Flag_Semi_FG & "' and gj.Flag_Scrap = '" & Flag_Scrap & "' and "
            'SQL = SQL & "gj.Flag_Bahan_Bakar = '" & Flag_Bahan_Bakar & "' and gj.Flag_Peralatan = '" & Flag_Peralatan & "' "

            SQL = SQL & FilterPengeluaranCostCenter
            SQL = SQL & "AND (gj.flag_ATK = '" & fATK & "' OR gj.flag_asset = '" & fAsset & "' OR gj.flag_sparepart = '" & fSparepart & "') "

            SQL = SQL & "order by a.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvBrg.Items.Add(Dr("kode_stock_owner"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("nama"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Dr("id_group_jenis"))
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
        If CmbSOBrg.SelectedIndex = -1 Then Exit Sub
        If TxtKodeBarang.Text.Trim.Length = 0 Then Exit Sub
        If LvBrg.Focused = True Then Exit Sub

        Try
            Cek_Flagging()

            OpenConn()

            SQL = "select a.kode_stock_owner,a.kode_barang,a.nama,a.satuan,a.id_group_jenis "
            SQL = SQL & "from barang as a inner join emi_group_jenis as gj on a.id_group_jenis = gj.id_group_jenis "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_barang = '" & TxtKodeBarang.Text & "' and "
            SQL = SQL & "a.kode_stock_owner = '" & CmbSOBrg.Text & "' and "

            'SQL = SQL & "gj.Flag_Packaging = '" & Flag_Packaging & "' and gj.Flag_Raw_Material = '" & Flag_Raw_Material & "' and "
            'SQL = SQL & "gj.Flag_Finished_Good = '" & Flag_Finished_Good & "' and gj.Flag_Sample = '" & Flag_Sample & "' and "
            'SQL = SQL & "gj.Flag_Semi_FG = '" & Flag_Semi_FG & "' and gj.Flag_Scrap = '" & Flag_Scrap & "' and "
            'SQL = SQL & "gj.Flag_Bahan_Bakar = '" & Flag_Bahan_Bakar & "' and gj.Flag_Peralatan = '" & Flag_Peralatan & "' "
            SQL = SQL & FilterPengeluaranCostCenter
            SQL = SQL & "AND (gj.flag_ATK = '" & fATK & "' OR gj.flag_asset = '" & fAsset & "' OR gj.flag_sparepart = '" & fSparepart & "') "

            SQL = SQL & "order by a.kode_barang"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CmbSOBrg.Text = Dr("kode_stock_owner")
                    TxtKodeBarang.Text = Dr("kode_barang")
                    TxtNamaBarang.Text = Dr("nama")
                    TxtSatuan.Text = Dr("satuan")
                    fId_group = Dr("id_group_jenis")
                    TxtNamaBarang.Focus()
                Else
                    MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TxtKodeBarang.Text = "" : TxtNamaBarang.Text = "" : TxtSatuan.Text = ""
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
        If CmbSOBrg.SelectedIndex = -1 Then Exit Sub
        If TxtNamaBarang.Text.Trim.Length = 0 Then
            LvBrg.Visible = False : Exit Sub
        Else
            LvBrg.Visible = True
        End If

        Try
            Cek_Flagging()

            OpenConn()

            Dim lv As New ListViewItem
            LvBrg.Items.Clear()

            SQL = "select top 20 a.kode_stock_owner,a.kode_barang,a.nama,a.satuan,a.id_group_jenis "
            SQL = SQL & "from barang as a inner join emi_group_jenis as gj on a.id_group_jenis = gj.id_group_jenis "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.nama LIKE '%" & TxtNamaBarang.Text & "%' and "
            SQL = SQL & "a.kode_stock_owner = '" & CmbSOBrg.Text & "' and "

            'SQL = SQL & "gj.Flag_Packaging = '" & Flag_Packaging & "' and gj.Flag_Raw_Material = '" & Flag_Raw_Material & "' and "
            'SQL = SQL & "gj.Flag_Finished_Good = '" & Flag_Finished_Good & "' and gj.Flag_Sample = '" & Flag_Sample & "' and "
            'SQL = SQL & "gj.Flag_Semi_FG = '" & Flag_Semi_FG & "' and gj.Flag_Scrap = '" & Flag_Scrap & "' and "
            'SQL = SQL & "gj.Flag_Bahan_Bakar = '" & Flag_Bahan_Bakar & "' and gj.Flag_Peralatan = '" & Flag_Peralatan & "' "
            SQL = SQL & FilterPengeluaranCostCenter
            SQL = SQL & "AND (gj.flag_ATK = '" & fATK & "' OR gj.flag_asset = '" & fAsset & "' OR gj.flag_sparepart = '" & fSparepart & "') "

            SQL = SQL & "order by a.Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvBrg.Items.Add(Dr("kode_stock_owner"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("nama"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Dr("id_group_jenis"))
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

    Private Sub LvCost_DoubleClick(sender As Object, e As EventArgs) Handles LvCost.DoubleClick
        Dim Kode As String = LvCost.FocusedItem.Text

        TxtIDCost.Text = Kode

        LvCost.Visible = False
        TxtKodeCost_Leave(LvCost, e)
        TxtKodeBarang.Focus()
    End Sub

    Private Sub LvCost_KeyDown(sender As Object, e As KeyEventArgs) Handles LvCost.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvCost_DoubleClick(LvCost, e)
        End If
    End Sub

    Private Sub LvBrg_DoubleClick(sender As Object, e As EventArgs) Handles LvBrg.DoubleClick
        Dim KodeSO As String = LvBrg.FocusedItem.Text
        Dim Kode As String = LvBrg.FocusedItem.SubItems(1).Text

        CmbSOBrg.Text = KodeSO
        TxtKodeBarang.Text = Kode

        LvBrg.Visible = False
        TxtKodeBarang_Leave(LvBrg, e)
        TxtJlh.Focus()
    End Sub

    Private Sub LvBrg_KeyDown(sender As Object, e As KeyEventArgs) Handles LvBrg.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvBrg_DoubleClick(LvBrg, e)
        End If
    End Sub

    Private Sub TxtJlh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJlh.KeyPress
        If e.KeyChar = Chr(13) Then TxtKet.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TxtKet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKet.KeyPress
        If e.KeyChar = Chr(13) Then BtnOK.Focus()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        TxtKodeBarang.Text = "" : TxtNamaBarang.Text = "" : TxtJlh.Text = "0" : TxtSatuan.Text = "" : TxtKet.Text = ""
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Kosong()
        CmbSOBrg.Focus()
    End Sub

    Private Sub Tgl1_ValueChanged(sender As Object, e As EventArgs) Handles Tgl1.ValueChanged
        get_no_faktur()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If CmbSOBrg.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode lokasi harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSOBrg.Focus() : Exit Sub
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
            If CmbSOBrg.Text.Trim = LvBarang.Items(i).Text And TxtKodeBarang.Text.Trim.ToUpper = LvBarang.Items(i).SubItems(1).Text Then
                MessageBox.Show("Kode barang sudah anda masukkan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtKodeBarang.Focus() : Exit Sub
            End If
        Next

        If LvBarang.Items.Count <> 0 Then
            For i As Integer = 0 To LvBarang.Items.Count - 1
                If LvBarang.Items(i).SubItems(6).Text <> fId_group Then
                    MessageBox.Show("group jenis barang berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
        End If

        lv = LvBarang.Items.Add(CmbSOBrg.Text.ToUpper)
        lv.SubItems.Add(TxtKodeBarang.Text.Trim.ToUpper)
        lv.SubItems.Add(TxtNamaBarang.Text.Trim.ToUpper)
        lv.SubItems.Add(TxtJlh.Text.Trim.ToUpper)
        lv.SubItems.Add(TxtSatuan.Text.Trim.ToUpper)
        lv.SubItems.Add(TxtKet.Text.Trim.ToUpper)
        lv.SubItems.Add(fId_group)

        HitungTotal()

        TxtKodeBarang.Text = "" : TxtNamaBarang.Text = "" : TxtJlh.Text = "" : TxtSatuan.Text = "" : TxtKet.Text = ""

        Dim inputlagi As String = MessageBox.Show("Input data barang lain?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If inputlagi = vbYes Then
            TxtKodeBarang.Focus()
        Else
            BtnSimpan.Focus()
        End If
        CmbSOBrg.Enabled = False
    End Sub

    Private Sub TxtIDCost_TextChanged(sender As Object, e As EventArgs) Handles TxtIDCost.TextChanged

    End Sub

    Private Sub LvBarang_DoubleClick(sender As Object, e As EventArgs) Handles LvBarang.DoubleClick
        If LvBarang.Items.Count = 0 Then Exit Sub

        TxtKodeBarang.Text = LvBarang.FocusedItem.SubItems(1).Text
        TxtNamaBarang.Text = LvBarang.FocusedItem.SubItems(2).Text
        TxtJlh.Text = LvBarang.FocusedItem.SubItems(3).Text
        TxtSatuan.Text = LvBarang.FocusedItem.SubItems(4).Text
        TxtKet.Text = LvBarang.FocusedItem.SubItems(5).Text

        LvBarang.FocusedItem.Remove()
        LvBrg.Visible = False

        HitungTotal()

        If LvBarang.Items.Count = 0 Then
            CmbSOBrg.Enabled = True
        Else
            CmbSOBrg.Enabled = False
        End If
    End Sub

    Private Sub HitungTotal()
        Dim Total As Integer = 0
        For i As Integer = 0 To LvBarang.Items.Count - 1
            Total += Val(LvBarang.Items(i).SubItems(3).Text)
        Next
        TxtTotal.Text = Format(Total, "N0")
    End Sub

    Private Sub Get_Isi_Listview(ByVal i As Integer)
        LvSO = LvBarang.Items(i).Text
        LvKodeBarang = LvBarang.Items(i).SubItems(1).Text
        LvNamaBarang = LvBarang.Items(i).SubItems(2).Text
        LvJumlah = LvBarang.Items(i).SubItems(3).Text
        LvKet = LvBarang.Items(i).SubItems(5).Text
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        If TxtKetPB.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan pengeluaran barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKetPB.Focus() : Exit Sub
        ElseIf TxtKodeCost.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode cost harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKodeCost.Focus() : Exit Sub
        ElseIf LvBarang.Items.Count = 0 Then
            MessageBox.Show("Barang yang di PO harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKodeBarang.Focus() : Exit Sub
        End If

        get_jam()

        Dim nId_group As String = ""
        Try
            Cek_Flagging()

            OpenConn()

            For i As Integer = 0 To LvBarang.Items.Count - 1
                SQL = "select a.kode_stock_owner,a.kode_barang,a.nama,a.satuan "
                SQL = SQL & "from barang as a inner join emi_group_jenis as gj on a.id_group_jenis = gj.id_group_jenis "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.Kode_Barang = '" & LvBarang.Items(i).SubItems(1).Text & "' and "
                SQL = SQL & "a.kode_stock_owner = '" & LvBarang.Items(i).Text & "' and "

                'SQL = SQL & "gj.Flag_Packaging = '" & Flag_Packaging & "' and gj.Flag_Raw_Material = '" & Flag_Raw_Material & "' and "
                'SQL = SQL & "gj.Flag_Finished_Good = '" & Flag_Finished_Good & "' and gj.Flag_Sample = '" & Flag_Sample & "' and "
                'SQL = SQL & "gj.Flag_Semi_FG = '" & Flag_Semi_FG & "' and gj.Flag_Scrap = '" & Flag_Scrap & "' and "
                'SQL = SQL & "gj.Flag_Bahan_Bakar = '" & Flag_Bahan_Bakar & "' and gj.Flag_Peralatan = '" & Flag_Peralatan & "' "
                SQL = SQL & FilterPengeluaranCostCenter
                SQL = SQL & "AND (gj.flag_ATK = '" & fATK & "' OR gj.flag_asset = '" & fAsset & "' OR gj.flag_sparepart = '" & fSparepart & "') "

                SQL = SQL & "order by a.Kode_Barang"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                    Else
                        MessageBox.Show("Barang tidak sesuai role untuk item:" & Chr(13) & Chr(13) &
                                        $"{LvBarang.Items(i).SubItems(1).Text} - {LvBarang.Items(i).SubItems(2).Text}.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CloseConn()
                        Exit Sub
                    End If
                End Using

                If i = 0 Then
                    nId_group = LvBarang.Items(i).SubItems(6).Text
                ElseIf i > 0 Then
                    If nId_group <> LvBarang.Items(i).SubItems(6).Text Then
                        MessageBox.Show("group jenis barang berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CloseConn()
                        Exit Sub
                    End If
                End If

            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            SQL = "Insert into d(kode_perusahaan,no_faktur,kode_stock_owner,tanggal,jam,keterangan,id_cost_center)"
            SQL = SQL & "values('" & KodePerusahaan & "','" & TxtNoFaktur.Text.Trim.ToUpper & "','" & CmbSO.Text & "','" & Format(Tgl1.Value, "yyyy-MM-dd") & "','"
            SQL = SQL & Format(tgl_skg, "HH:mm:ss") & "','" & TxtKetPB.Text.Trim.ToUpper & "','" & TxtIDCost.Text & "')"
            ExecuteTrans(SQL)

            For i As Integer = 0 To LvBarang.Items.Count - 1
                Get_Isi_Listview(i)

                SQL = "insert into emi_pengeluaran_barang_details(kode_perusahaan, no_faktur, "
                SQL = SQL & "kode_stock_owner, kode_barang, jumlah, keterangan) "
                SQL = SQL & "values('" & KodePerusahaan & "','" & TxtNoFaktur.Text.Trim & "',"
                SQL = SQL & "'" & LvSO & "','" & LvKodeBarang & "','" & HilangkanTanda(LvJumlah) & "','" & LvKet & "')"
                ExecuteTrans(SQL)

                SQL = "select good_stock,nama from barang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                SQL = SQL & "kode_barang = '" & LvKodeBarang & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            If .Rows(0).Item("good_stock") - HilangkanTanda(LvJumlah) < BolehNegatif Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses membuat stock menjadi negatif untuk barang " & .Rows(0).Item("nama") & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else

                                SQL = "Update barang set good_stock = good_stock - " & HilangkanTanda(LvJumlah) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                                SQL = SQL & "kode_barang = '" & LvKodeBarang & "'"
                                ExecuteTrans(SQL)
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                            Exit Sub
                        End If
                    End With

                    Dim lewatin As String = "T"
                    SQL = "select isnull(sum(jumlah), 0) as stock from barang_sn where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                    SQL = SQL & "kode_barang = '" & LvKodeBarang & "' and jumlah <> 0 "
                    'SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("stock") < Val(HilangkanTanda(LvJumlah)) Then
                                lewatin = "Y"
                            Else
                                lewatin = "T"
                            End If
                        End If
                    End Using


                    Dim x_no_urut_det_do As Integer = 0
                    SQL = "select IDENT_CURRENT('emi_pengeluaran_barang_details') as urutan"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            x_no_urut_det_do = Dr("urutan")
                        End If
                    End Using

                    SQL = "select urut_oto from emi_pengeluaran_barang_details where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & TxtNoFaktur.Text.Trim & "' and urut_oto = '" & x_no_urut_det_do & "'"
                    Using Dr = OpenTrans(SQL)
                        If Not (Dr.Read) Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    total_hpp_metode_B = 0
                    Dim boleh_jual_rugi As String = ""

                    If lewatin = "T" Then

                        Dim sisa As Double = 0

                        SQL = "select kode_stock_owner, kode_barang, serial_number, jumlah from barang_sn where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                        SQL = SQL & "kode_barang = '" & LvKodeBarang & "' and jumlah <> 0 "
                        SQL = SQL & "and Blok_SN is null "
                        SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                        Using Ds1 = BindingTrans(SQL)
                            With Ds1.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    sisa = HilangkanTanda(LvJumlah)
                                    For h As Integer = 0 To .Rows.Count - 1
                                        If sisa = 0 Then
                                            Exit For
                                        ElseIf sisa < 0 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
                                            SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                            ExecuteTrans(SQL)

                                            SQL = "insert into emi_pengeluaran_barang_det(kode_perusahaan, no_faktur, "
                                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, No_Urut_Detail, "
                                            SQL = SQL & "jumlah,keterangan) values('" & KodePerusahaan & "', "
                                            SQL = SQL & "'" & TxtNoFaktur.Text.Trim & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                            SQL = SQL & "" & x_no_urut_det_do & ",'" & sisa & "','" & LvKet & "')"
                                            ExecuteTrans(SQL)

                                            total_hpp_metode_B = total_hpp_metode_B + (sisa * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                            sisa = 0
                                        ElseIf sisa > .Rows(h).Item("jumlah") Then

                                            SQL = "insert into emi_pengeluaran_barang_det(kode_perusahaan, no_faktur, "
                                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, No_Urut_Detail, "
                                            SQL = SQL & "jumlah,keterangan) values('" & KodePerusahaan & "', "
                                            SQL = SQL & "'" & TxtNoFaktur.Text.Trim & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                            SQL = SQL & "" & x_no_urut_det_do & ",'" & .Rows(h).Item("jumlah") & "','" & LvKet & "')"
                                            ExecuteTrans(SQL)

                                            SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                            ExecuteTrans(SQL)

                                            total_hpp_metode_B = total_hpp_metode_B + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                            sisa = sisa - .Rows(h).Item("jumlah")
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & .Rows(i).Item("nama") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        If sisa <> 0 And h = .Rows.Count - 1 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Jumlah stock tidak mencukupi untuk barang " & .Rows(i).Item("nama") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("SN untuk barang " & .Rows(i).Item("nama") & " tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using

                    Else

                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("SN untuk barang " & LvNamaBarang & " tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            Next

            '
            isError = False

            Dim inisial_faktur_dari As String = ""
            SQL = "select inisial_faktur from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbSO.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'akun_persediaan_dari = Dr("persediaan")
                    inisial_faktur_dari = Dr("inisial_faktur")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim xakun_persediaan As String = ""
            Dim xakun_hpp As String = ""
            SQL = "select Biaya_Pengeluaran_Barang from Stock_Owner_Gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & LvBarang.Items(0).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'xakun_persediaan = Dr("Persediaan")
                    xakun_hpp = Dr("Biaya_Pengeluaran_Barang")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Akun_Persediaan from EMI_Group_Jenis_Akun where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Id_Group_Jenis = '" & nId_group & "' and Kode_Stock_Owner = '" & CmbSOBrg.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    xakun_persediaan = Dr("Akun_Persediaan")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Jurnal_Pengeluaran_Barang(TxtNoFaktur.Text, xakun_hpp, xakun_persediaan, total_hpp_metode_B, LvBarang.Items(0).Text, TxtIDCost.Text, inisial_faktur_dari)

            If isError = False Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Ada Masalah pada Jurnal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Cmd.Transaction.Commit()
            CloseConn()

            Dim SF As String = ""

            OpenConn()

            'SQL = "SELECT top 1 kode_perusahaan FROM EMI_Pengeluaran_Barang "
            'SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtNoFaktur.Text & "'"

            'SF = "{EMI_Pengeluaran_Barang.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
            'SF = SF & "{EMI_Pengeluaran_Barang.No_Faktur} = '" & TxtNoFaktur.Text & "'"

            'Using MyDS As DataSet = BindingTrans(SQL)
            '    With MyDS.Tables(0)
            '        If .Rows.Count <> 0 Then

            '            Dim CrDoc As New Pengeluaran_Barang_Faktur

            '            CrDoc.SetDataSource(MyDS)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            CrDoc.SummaryInfo.ReportTitle = ""
            '            CrDoc.RecordSelectionFormula = SF

            '            With Print_Form
            '                .Text = "Print Form"
            '                .CrystalReportViewer1.ReportSource = CrDoc
            '                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            '                .Refresh()
            '                .Show()
            '            End With

            '        Else
            '            MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '        End If
            '    End With
            'End Using

            CloseConn()

            Kosong()
            CmbSOBrg.Focus()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CmbSOBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSOBrg.KeyPress
        If e.KeyChar = Chr(13) Then Tgl1.Focus()
    End Sub
End Class
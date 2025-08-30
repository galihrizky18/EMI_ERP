Public Class N_EMI_Laporan_Validasi_GR_3




    Private Sub N_EMI_Laporan_Validasi_GR_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Transaksi.Columns.Clear()
        Lv_Transaksi.Columns.Add("No Transaksi", 100, HorizontalAlignment.Left)
        Lv_Transaksi.Columns.Add("Tanggal", 100, HorizontalAlignment.Left)
        Lv_Transaksi.Columns.Add("Jam", 100, HorizontalAlignment.Left)
        Lv_Transaksi.Columns.Add("Keterangan", 100, HorizontalAlignment.Left)
        Lv_Transaksi.Columns.Add("", 100, HorizontalAlignment.Left)
        Lv_Transaksi.Columns.Add("", 100, HorizontalAlignment.Left)

        Kosong()
    End Sub

    Private Sub Kosong()

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date



    End Sub

    '=====================================================================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '=====================================================================================================================================================================================
    Private Sub Txt_NoTransaksi_TextChanged(sender As Object, e As EventArgs) Handles Txt_NoTransaksi.TextChanged

        If Txt_NoTransaksi.Text.Trim.Length = 0 Then
            Me.Size = New Size(645, 340)
            Lv_Transaksi.Location = New Point(650, 125)
            Lv_Transaksi.Visible = False
            Txt_NoTransaksi.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(645, 380)
            Lv_Transaksi.Visible = True
            Lv_Transaksi.Location = New Point(115, 125)
        End If

        Try
            OpenConn()

            Lv_Transaksi.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Transaksi.Items.Add("000")
            Lv.SubItems.Add(OpsiSeluruh)
            SQL = "select a.No_Production_Order, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama as Nama_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Transaksi.Items.Add(Dr("Id_Cost_Center"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub





    '=====================================================================================================================================================================================
    '=     HANDLE LEAVE
    '=====================================================================================================================================================================================

    Private Sub Txt_NoTransaksi_Leave(sender As Object, e As EventArgs) Handles Txt_NoTransaksi.Leave

    End Sub

    Private Sub Txt_NoSplit_Leave(sender As Object, e As EventArgs) Handles Txt_NoSplit.Leave

    End Sub

    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave

    End Sub


    '=====================================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '=====================================================================================================================================================================================
#Region "HANDLE KEYPRESS"

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NoTransaksi.Focus()
    End Sub

    Private Sub Txt_NoTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoTransaksi.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_NoTransaksi.Text.Trim.Length = 0 Then Txt_NoTransaksi.Focus()
            Txt_NoTransaksi_Leave(Txt_NoTransaksi, e)

            Me.Size = New Size(645, 340)
            Lv_Transaksi.Visible = False
            Lv_Transaksi.Location = New Point(650, 125)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NoTransaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NoTransaksi.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Transaksi.Focus()
    End Sub

    Private Sub Txt_NoSplit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoSplit.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_NoSplit.Text.Trim.Length = 0 Then Txt_NoSplit.Focus()
            Txt_NoSplit_Leave(Txt_NoSplit, e)

            Me.Size = New Size(645, 340)
            Lv_Split.Visible = False
            Lv_Split.Location = New Point(650, 150)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NoSplit_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NoSplit.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Split.Focus()
    End Sub

    Private Sub Cmb_Lokasi_Awal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi_Awal.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Lokasi_Tujuan.DroppedDown = True
            Cmb_Lokasi_Tujuan.Focus()
        End If
    End Sub

    Private Sub Cmb_Lokasi_Tujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi_Tujuan.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdBarang.Focus()
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Me.Size = New Size(645, 340)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(650, 207)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_NmBarang.Text.Trim.Length = 0 Then Txt_NmBarang.Focus()
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Me.Size = New Size(645, 340)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(650, 207)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Cmb_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then Txt_ParamLain.Focus()
    End Sub

    Private Sub Txt_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub
#End Region

End Class
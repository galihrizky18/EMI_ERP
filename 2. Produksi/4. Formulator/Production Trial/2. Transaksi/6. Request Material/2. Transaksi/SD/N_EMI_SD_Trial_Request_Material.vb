Public Class N_EMI_SD_Trial_Request_Material

    Dim CurrentFaktur As String = ""

    Dim Lv_NoTransaksi, Lv_Tanggal, Lv_Jam, Lv_Jumlah, Lv_Satuan As String

    Dim Item_NoTransaksi As Integer = 0
    Dim Item_Tanggal As Integer = 1
    Dim Item_Jam As Integer = 2
    Dim Item_Jumlah As Integer = 3
    Dim Item_Satuan As Integer = 4

    Dim SwitchAutoComplete As Boolean = False

    Private Sub N_EMI_SD_Trial_Request_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data_Formula_Pending.Columns.Clear()
        Lv_Data_Formula_Pending.Columns.Add("No Transaksi", 140, HorizontalAlignment.Left) '0
        Lv_Data_Formula_Pending.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '0
        Lv_Data_Formula_Pending.Columns.Add("Jam", 0, HorizontalAlignment.Center) '0
        Lv_Data_Formula_Pending.Columns.Add("Jumlah", 0, HorizontalAlignment.Right) '1
        Lv_Data_Formula_Pending.Columns.Add("Satuan", 0, HorizontalAlignment.Center) '2
        Lv_Data_Formula_Pending.View = View.Details

        Lv_Data_Formula_Completed.Columns.Clear()
        Lv_Data_Formula_Completed.Columns.Add("No Transaksi", 140, HorizontalAlignment.Left) '0
        Lv_Data_Formula_Completed.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '0
        Lv_Data_Formula_Completed.Columns.Add("Jam", 0, HorizontalAlignment.Center) '0
        Lv_Data_Formula_Completed.Columns.Add("Jumlah", 0, HorizontalAlignment.Right) '1
        Lv_Data_Formula_Completed.Columns.Add("Satuan", 0, HorizontalAlignment.Center) '2
        Lv_Data_Formula_Completed.View = View.Details

        Lv_Detail_Bahan.Columns.Clear()
        Lv_Detail_Bahan.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_Detail_Bahan.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Detail_Bahan.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Detail_Bahan.Columns.Add("Persentase", 100, HorizontalAlignment.Center)
        Lv_Detail_Bahan.Columns.Add("Satuan", 70, HorizontalAlignment.Center)
        Lv_Detail_Bahan.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '0
        Lv_Barang.Columns.Add("Nama Barang", 380, HorizontalAlignment.Left) '1
        Lv_Barang.View = View.Details


        Try
            OpenConn()

            Cmb_Satuan.Items.Clear() : Cmb_Satuan_Kebutuhan.Items.Clear()
            SQL = "select Satuan from EMI_Satuan "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Satuan.Items.Add(Dr("Satuan"))
                    Cmb_Satuan_Kebutuhan.Items.Add(Dr("Satuan"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
    End Sub

    Private Sub Kosong()

        Txt_KdBarang.Text = ""
        Txt_NmBarang.Text = ""
        Txt_Total_Bahan.Text = ""

        CurrentFaktur = ""

        Txt_Kode_Formula.Text = ""
        Txt_Jumlah_Kebutuhan.Text = ""

        Cmb_Satuan.SelectedIndex = -1
        Cmb_Satuan_Kebutuhan.SelectedIndex = -1


        SwitchAutoComplete = False

        Lv_Data_Formula_Pending.Items.Clear()
        Lv_Detail_Bahan.Items.Clear()
        Lv_Barang.Items.Clear()

        Txt_KdBarang.Focus()

    End Sub

    Private Sub Get_Data_Formula(ByVal index As Integer)
        Lv_NoTransaksi = Lv_Data_Formula_Pending.Items(index).SubItems(Item_NoTransaksi).Text
        Lv_Tanggal = Lv_Data_Formula_Pending.Items(index).SubItems(Item_Tanggal).Text
        Lv_Jam = Lv_Data_Formula_Pending.Items(index).SubItems(Item_Jam).Text
        Lv_Jumlah = Lv_Data_Formula_Pending.Items(index).SubItems(Item_Jumlah).Text
        Lv_Satuan = Lv_Data_Formula_Pending.Items(index).SubItems(Item_Satuan).Text
    End Sub

    Private Sub Btn_Get_Formula_Click(sender As Object, e As EventArgs) Handles Btn_Get_Formula.Click
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Harap Pilih Dahulu Barang Yang Ingin Di Request", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus()
            Exit Sub
        End If
        TabControl1_SelectedIndexChanged(sender, New EventArgs)

    End Sub

    Private Sub HitungTotal()

        Dim TotalBahan As Double = 0

        If Lv_Detail_Bahan.Items.Count = 0 Then
            Txt_Total_Bahan.Text = ""
        Else

            For i As Integer = 0 To Lv_Detail_Bahan.Items.Count - 1
                TotalBahan += Val(HilangkanTanda(Lv_Detail_Bahan.Items(i).SubItems(2).Text))
            Next

        End If

        Txt_Total_Bahan.Text = Format(TotalBahan, "N4")
    End Sub

    Private Sub Lv_Formula_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data_Formula_Pending.SelectedIndexChanged, Lv_Data_Formula_Completed.SelectedIndexChanged
        Dim lv As ListView = TryCast(sender, ListView)
        If lv Is Nothing OrElse lv.SelectedItems.Count = 0 Then Exit Sub
        If lv Is Lv_Data_Formula_Pending Then
            If Lv_Data_Formula_Pending.Items.Count = 0 Or Lv_Data_Formula_Pending.FocusedItem Is Nothing Then Exit Sub
        ElseIf lv Is Lv_Data_Formula_Completed Then
            If Lv_Data_Formula_Completed.Items.Count = 0 Or Lv_Data_Formula_Completed.FocusedItem Is Nothing Then Exit Sub
        Else
            MessageBox.Show("Object Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If




        Try
            OpenConn()

            Dim SelectedFaktur As String = ""

            If lv Is Lv_Data_Formula_Pending Then
                SelectedFaktur = Lv_Data_Formula_Pending.FocusedItem.Text
            ElseIf lv Is Lv_Data_Formula_Completed Then
                SelectedFaktur = Lv_Data_Formula_Completed.FocusedItem.Text
            End If


            CurrentFaktur = SelectedFaktur
            Txt_Kode_Formula.Text = SelectedFaktur
            Txt_Jumlah_Kebutuhan.Text = ""
            Cmb_Satuan_Kebutuhan.SelectedIndex = -1
            Lv_Detail_Bahan.Items.Clear()
            Cmb_Satuan.SelectedIndex = -1
            Txt_Total_Bahan.Text = ""



            SQL = $"select top 1 satuan from EMI_Transaksi_Formulator_Detail_Bahan where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{SelectedFaktur}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Cmb_Satuan_Kebutuhan.SelectedItem = Dr("satuan").ToString.Trim
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Btn_Get_Data_Click(sender As Object, e As EventArgs) Handles Btn_Get_Data.Click
        If Txt_Kode_Formula.Text.Trim.Length = 0 Then
            MessageBox.Show("Harap Pilih Dahulu Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Txt_Jumlah_Kebutuhan.Text.Trim.Length = 0 Then
            MessageBox.Show("Harap Isi Dahulu Jumlah Kebutuhan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah_Kebutuhan.Focus()
            Exit Sub
        End If

        Try
            OpenConn()


            Dim SelectedFaktur As String = Lv_Data_Formula_Pending.FocusedItem.Text
            Dim JumlahKebutuhan As Double = Val(HilangkanTanda(Txt_Jumlah_Kebutuhan.Text.Trim))

            Dim SatuanTotal As String = ""
            Lv_Detail_Bahan.Items.Clear()
            Txt_Total_Bahan.Text.Trim()
            SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, b.Persentase, b.satuan, a.Hasil, a.Satuan_Hasil, "
            SQL &= $"isnull((( "
            SQL &= $"(dbo.ubah_satuan('{KodePerusahaan}', 'masa', b.kode_barang, '{Cmb_Satuan_Kebutuhan.Text.Trim}', b.satuan, {JumlahKebutuhan})) / "
            SQL &= $"(dbo.ubah_satuan('{KodePerusahaan}', 'masa', a.kode_barang, a.Satuan_Hasil, b.satuan, a.Hasil)) "
            SQL &= $") * b.jumlah "
            SQL &= $"), 0) as Jumlah_Kebutuhan "
            SQL &= $"from Emi_Transaksi_Formulator a "
            SQL &= $"inner join EMI_Transaksi_Formulator_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.Status is null "
            SQL &= $"and a.Kode_Barang = '{Txt_KdBarang.Text.Trim}' "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Bahan.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    'Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Kebutuhan"), "N4"))
                    Lv.SubItems.Add($"{Dr("Persentase")} %")
                    Lv.SubItems.Add(Dr("satuan"))

                    SatuanTotal = Dr("satuan")
                Loop
            End Using

            Cmb_Satuan.SelectedItem = SatuanTotal.Trim

            Txt_Total_Bahan.Text = ""

            HitungTotal()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If CurrentFaktur.Trim.Length = 0 Then
            MessageBox.Show("Harap Pilih Faktur Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        N_EMI_Transaksi_Trial_Request_Material.ShowBahanByFormula(CurrentFaktur, Txt_Jumlah_Kebutuhan.Text, Cmb_Satuan_Kebutuhan.Text)
        Kosong()
        Me.Close()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If SwitchAutoComplete Then Exit Sub

        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1090, 81)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(146, 81)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang  "
            SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
            SQL = SQL & "and a.Kode_Barang like '%" & Txt_KdBarang.Text & "%' and aktif = 'Y' and flag_finished_good= 'Y' "
            SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
        If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdBarang.Text = OpsiSeluruh Then

                SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang  "
                SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
                SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
                SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
                SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' and aktif = 'Y' and flag_finished_good= 'Y' "
                SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NmBarang.Text = Dr("Nama")
                        Btn_Get_Formula.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Lv_Barang.Visible = False
                    Lv_Barang.Location = New Point(1090, 81)
                End Using
            Else
                Btn_Get_Formula.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1090, 81)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If SwitchAutoComplete Then Exit Sub

        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1090, 81)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(146, 81)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang  "
            SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
            SQL = SQL & "and a.Nama like '%" & Txt_NmBarang.Text & "%' and aktif = 'Y' and flag_finished_good= 'Y' "
            SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1090, 81)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Lv_Formula_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lv_Data_Formula_Pending.KeyPress, Lv_Data_Formula_Completed.KeyPress
        Dim lv As ListView = TryCast(sender, ListView)
        If lv Is Nothing OrElse lv.SelectedItems.Count = 0 Then Exit Sub
        If lv Is Lv_Data_Formula_Pending Then
            If Lv_Data_Formula_Pending.Items.Count = 0 Or Lv_Data_Formula_Pending.FocusedItem Is Nothing Then Exit Sub
        ElseIf lv Is Lv_Data_Formula_Completed Then
            If Lv_Data_Formula_Completed.Items.Count = 0 Or Lv_Data_Formula_Completed.FocusedItem Is Nothing Then Exit Sub
        Else
            MessageBox.Show("Object Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Txt_Jumlah_Kebutuhan.Focus()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = -1 Or Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub

        If TabControl1.SelectedIndex = 0 Then

            GetDataFormulaPending()

        ElseIf TabControl1.SelectedIndex = 1 Then

            GetDataFormulaCompleted()
        End If

        Txt_Kode_Formula.Text = ""
        Txt_Jumlah_Kebutuhan.Text = ""
        Txt_Total_Bahan.Text = ""
        Cmb_Satuan_Kebutuhan.SelectedIndex = -1
        Cmb_Satuan.SelectedIndex = -1
    End Sub

    Private Sub GetDataFormulaPending()

        Try
            OpenConn()

            Dim Kode_Barang As String = Txt_KdBarang.Text.Trim

            Lv_Data_Formula_Pending.Items.Clear() : Lv_Detail_Bahan.Items.Clear()
            Txt_Total_Bahan.Text.Trim()
            SQL = "select No_Faktur, Tanggal, Jam, Hasil, Satuan_Hasil "
            SQL &= $"from Emi_Transaksi_Formulator "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Status is null "
            SQL &= $"and Kode_Barang = '{Txt_KdBarang.Text.Trim}' "
            SQL &= $"and Flag_Validasi_Main = 'Y' "
            SQL &= $"and hasTrial is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data_Formula_Pending.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Format(Dr("Hasil"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan_Hasil"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub GetDataFormulaCompleted()
        Try
            OpenConn()

            Dim Kode_Barang As String = Txt_KdBarang.Text.Trim

            Lv_Data_Formula_Completed.Items.Clear() : Lv_Detail_Bahan.Items.Clear()
            Txt_Total_Bahan.Text.Trim()
            SQL = "select No_Faktur, Tanggal, Jam, Hasil, Satuan_Hasil "
            SQL &= $"from Emi_Transaksi_Formulator "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Status is null "
            SQL &= $"and Kode_Barang = '{Txt_KdBarang.Text.Trim}' "
            SQL &= $"and Flag_Validasi_Main = 'Y' "
            SQL &= $"and hasTrial = 'Y' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data_Formula_Completed.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Format(Dr("Hasil"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan_Hasil"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        SwitchAutoComplete = True
        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmKdBarang
        SwitchAutoComplete = False

        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(1090, 81)

        Btn_Get_Formula.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
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

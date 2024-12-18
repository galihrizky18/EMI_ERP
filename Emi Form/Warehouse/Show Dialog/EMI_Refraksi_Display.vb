Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class EMI_Refraksi_Display

    Dim arrFilter As New ArrayList

    Dim Lv_NoPO, Lv_KdBarang, Lv_NoUrut As String

    Dim item_NoPO As Integer = 0
    Dim item_KdBarang As Integer = 2
    Dim item_NoUrut As Integer = 4

    Private Sub EMI_Refraksi_Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        initial_Lv_PO()
        kosong()

    End Sub

    Private Sub kosong()

        Lv_PO.Items.Clear()

        Txt_ValueFilter.Text = ""

        Txt_NoPO.Text = ""
        Txt_KdBarang.Text = ""
        Txt_NmBarang.Text = ""
        Txt_JmlhPO.Text = ""
        Txt_HargaPO.Text = ""
        Txt_HargaRefraksi.Text = ""
        Txt_NoUrut.Text = ""
        Txt_SatuanKecil.Text = ""
        Txt_KdSo.Text = ""

        Cmb_Satuan.Items.Clear()
        Cmb_MataUang.Items.Clear()

        Cmb_Filter.Items.Clear()
        Cmb_Filter.Items.Add("No Po") : arrFilter.Add("a.no_faktur")
        Cmb_Filter.Items.Add("Supplier") : arrFilter.Add("d.Nama")
        Cmb_Filter.Items.Add("Lokasi") : arrFilter.Add("b.Kode_Stock_Owner")
        Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("b.Kode_Barang")
        Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("c.Nama")
        Cmb_Filter.SelectedIndex = -1

        Get_Data_PO()

    End Sub

    Private Sub initial_Lv_PO()

        Lv_PO.Columns.Clear()
        Lv_PO.Columns.Add("No PO", 140, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("Supplier", 150, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("Kode Barang", 140, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("Keterangan", 300, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("NoUrut", 0, HorizontalAlignment.Left)
        Lv_PO.View = View.Details

    End Sub

    Private Sub Get_Data_PO(ByVal index As Integer)
        Lv_NoPO = Lv_PO.Items(index).SubItems(item_NoPO).Text
        Lv_KdBarang = Lv_PO.Items(index).SubItems(item_KdBarang).Text
        Lv_NoUrut = Lv_PO.Items(index).SubItems(item_NoUrut).Text
    End Sub


    Private Sub Get_Data_PO(Optional filter As String = "")

        Try
            OpenConn()

            Lv_PO.Items.Clear()
            SQL = "select a.No_Faktur, d.Nama as supplier, c.Kode_Barang ,a.No_Nota as keterangan, b.No_Urut "
            SQL = SQL & "from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b, barang c, Suppliers d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Supplier = d.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.Flag_Permintaan_Refraksi = 'Y' "
            SQL = SQL & "order by b.No_Urut "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim lv As ListViewItem
                    lv = Lv_PO.Items.Add(Dr("No_Faktur"))
                    lv.SubItems.Add(Dr("supplier"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("keterangan"))
                    lv.SubItems.Add(Dr("No_Urut"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_PO_DoubleClick(sender As Object, e As EventArgs) Handles Lv_PO.DoubleClick

        If Lv_PO.Items.Count = 0 Then Exit Sub

        Get_Data_PO(Lv_PO.FocusedItem.Index)

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "select a.No_Faktur, b.Kode_Barang, c.Nama, b.Jumlah, b.Satuan, a.Mata_Uang, b.Total, b.No_Urut, b.satuan_barang, b.Kode_Stock_Owner, b.harga "
            SQL = SQL & "from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Lv_NoPO & "' "
            SQL = SQL & "and b.No_Urut = '" & Lv_NoUrut & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            Txt_NoPO.Text = .Rows(i).Item("No_Faktur")
                            Txt_KdBarang.Text = .Rows(i).Item("Kode_Barang")
                            Txt_NmBarang.Text = .Rows(i).Item("Nama")
                            Txt_JmlhPO.Text = Format(.Rows(i).Item("Jumlah"), "N2")
                            Txt_HargaPO.Text = Format(.Rows(i).Item("harga"), "N2")
                            Txt_NoUrut.Text = .Rows(i).Item("No_Urut")
                            Txt_SatuanKecil.Text = .Rows(i).Item("satuan_barang")
                            Txt_KdSo.Text = .Rows(i).Item("Kode_Stock_Owner")

                            'Get Satuan
                            Cmb_Satuan.Items.Clear()
                            SQL = "select Satuan from Barang_Detail_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_barang = '" & .Rows(i).Item("Kode_Barang") & "' and Flag_Tampil_Display = 'Y' "
                            Using Dr = OpenTrans(SQL)
                                Do While Dr.Read
                                    Cmb_Satuan.Items.Add(Dr("Satuan"))
                                Loop

                                Cmb_Satuan.SelectedItem = .Rows(i).Item("Satuan")

                            End Using

                            'Get Mata Uang
                            Cmb_MataUang.Items.Clear()
                            SQL = "select Kode_Mata_Uang from Mata_Uang where Kode_Perusahaan = '" & KodePerusahaan & "'"
                            Using Dr = OpenTrans(SQL)
                                Do While Dr.Read
                                    Cmb_MataUang.Items.Add(Dr("Kode_Mata_Uang"))
                                Loop

                                Cmb_MataUang.SelectedItem = .Rows(i).Item("Mata_Uang")

                            End Using

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub


    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Not Txt_ValueFilter.Text.Trim.Length = 0 Then
            If Cmb_Filter.SelectedIndex <> 0 Then
                MessageBox.Show("Pilih Dahulu Jenis Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_ValueFilter.Text = "" : Cmb_Filter.Focus()
                Exit Sub
            End If
        End If

        If Cmb_Filter.SelectedIndex > 0 Then
            If Txt_ValueFilter.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_ValueFilter.Focus()
                Exit Sub
            End If
        End If


        Try
            OpenConn()

            Dim asdasdas As Integer = Cmb_Filter.SelectedIndex

            Lv_PO.Items.Clear()
            SQL = "select a.No_Faktur, d.Nama as supplier, c.Kode_Barang ,a.No_Nota as keterangan, b.No_Urut "
            SQL = SQL & "from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b, barang c, Suppliers d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Supplier = d.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.Flag_Permintaan_Refraksi = 'Y' "
            If Cmb_Filter.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrFilter.Item(Cmb_Filter.SelectedIndex) & "  like  '%" & Trim(Txt_ValueFilter.Text) & "%' "
            End If
            SQL = SQL & "order by b.No_Urut "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim lv As ListViewItem
                    lv = Lv_PO.Items.Add(Dr("No_Faktur"))
                    lv.SubItems.Add(Dr("supplier"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("keterangan"))
                    lv.SubItems.Add(Dr("No_Urut"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Txt_NoPO.Text.Trim.Length = 0 Or Txt_HargaPO.Text.Trim.Length = 0 Or Txt_HargaRefraksi.Text.Trim.Length = 0 Then
            MessageBox.Show("Data Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Not IsNumeric(Txt_HargaRefraksi.Text) Then
            MessageBox.Show("Input Tidak Boleh Huruf ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_HargaRefraksi.Text = "" : Txt_HargaRefraksi.Focus()
            Exit Sub
        End If

        If Txt_HargaRefraksi.Text.Contains(",") Then
            MessageBox.Show("Input Tidak Boleh ',' ganti dengan ',' ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_HargaRefraksi.Text = "" : Txt_HargaRefraksi.Focus()
            Exit Sub
        End If


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '==============================
            '=     UBAH HARGA BERUBAH     =
            '==============================
            Dim hargaInput As String = ""
            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'UANG', '" & Lv_KdBarang & "', '" & Cmb_Satuan.Text & "', '" & Txt_SatuanKecil.Text & "', "
            SQL = SQL & "'" & Val(HilangkanTanda(Txt_HargaRefraksi.Text)) & "') as harga_input "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    hargaInput = Dr("harga_input")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Terjadi Kesalahan saat Ubah Harga", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==========================================
            '=     UPDATE EMI_Pembelian_PO_Detail     =
            '==========================================
            SQL = "update EMI_Pembelian_PO_Detail set Flag_Permintaan_Refraksi = null, Flag_Refraksi='Y', Harga_Refraksi = '" & hargaInput & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoPO.Text & "' and Kode_Stock_Owner = '" & Txt_KdSo.Text & "' "
            SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' and No_Urut = '" & Txt_NoUrut.Text & "'"
            ExecuteTrans(SQL)


            '==========================================
            '=     UPDATE EMI_Pembelian_PO_Detail     =
            '==========================================
            SQL = "select No_Faktur, No_PO, Urut_Oto, Urut_PO "
            SQL = SQL & "from EMI_Pembelian_Loading_Detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_PO = '" & Txt_NoPO.Text & "' "
            SQL = SQL & "and Urut_PO = '" & Txt_NoUrut.Text & "' "
            SQL = SQL & "and Flag_Permintaan_Refraksi = 'Y' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "update EMI_Pembelian_Loading_Detail set Flag_Permintaan_Refraksi = NULL, Flag_Refraksi = 'Y', Harga_Refraksi = '" & hargaInput & "' "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_PO = '" & .Rows(i).Item("No_PO") & "' and No_Faktur = '" & .Rows(i).Item("No_Faktur") & "' "
                            SQL = SQL & "and Urut_PO = '" & .Rows(i).Item("Urut_PO") & "' and Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' and Flag_Permintaan_Refraksi = 'Y' "
                            ExecuteTrans(SQL)

                        Next

                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data PO Pembelian tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub





End Class
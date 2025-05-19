Public Class Emi_Selisih_Barang_Masuk_SD_DN2

    Dim arr_IDWarehouse As New ArrayList


    Private Sub Emi_Selisih_Barang_Masuk_SD_DN2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub Emi_Selisih_Barang_Masuk_SD_DN2_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub


    Public Sub kosong()

        Txt_NoPO.Text = String.Empty
        Txt_SelisihQty.Text = String.Empty
        Txt_Satuan.Text = String.Empty
        Txt_Lokasi.Text = String.Empty
        Txt_NmBarang.Text = String.Empty
        Cmb_Rak.Items.Clear()

    End Sub

    Public Sub Load_Posisi_Rak()

        If String.IsNullOrWhiteSpace(Txt_NoPO.Text) And String.IsNullOrWhiteSpace(Txt_KdBarang.Text) Then Exit Sub

        Try
            OpenConn()

            Cmb_Rak.Items.Clear() : arr_IDWarehouse.Clear()
            SQL = "select a.Id_Warehouse, b.Keterangan "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, View_Warehouse_Position b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Warehouse = b.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Pembelian_Loading = '" & Txt_NoPO.Text & "' "
            SQL = SQL & "and a.Kode_Barang='" & Txt_KdBarang.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Rak.Items.Add(Dr("Keterangan")) : arr_IDWarehouse.Add(Dr("Id_Warehouse"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Simpan_Click(sender As Object, e As EventArgs) Handles Simpan.Click
        If Txt_NoPO.Text.Trim.Length = 0 Or Cmb_Rak.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            SQL = "select a.Kode_Barang, a.No_Pembelian_Loading, b.Keterangan, a.No_Faktur, a.No_SJ, a.No_Plat, a.Batch_Number, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal, "
            SQL = SQL & "a.Serial_Number, a.Tgl_Produksi_Real, a.Tgl_Expired_Real "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, View_Warehouse_Position b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Warehouse = b.Id_WMS_Warehouse_Position "
            SQL = SQL & "and b.Id_WMS_Warehouse_Position='" & arr_IDWarehouse(Cmb_Rak.SelectedIndex) & "' "
            SQL = SQL & "and a.No_Pembelian_Loading = '" & Txt_NoPO.Text & "' "
            SQL = SQL & "and a.Kode_Barang='" & Txt_KdBarang.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    'Emi_Selisih_Barang_Masuk2.Dgv_DetailBarang.CurrentRow.Cells(13).Value = Dr("Tgl_Produksi_Real")
                    'Emi_Selisih_Barang_Masuk2.Dgv_DetailBarang.CurrentRow.Cells(14).Value = Dr("Tgl_Expired_Real")
                    'Emi_Selisih_Barang_Masuk2.Dgv_DetailBarang.CurrentRow.Cells(15).Value = Dr("Serial_Number")
                Loop
            End Using

            Me.Close()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


End Class
Public Class N_EMI_SD_Request_Material

    Public NoSplit, Kd_Barang As String


    Dim arr_id_kategori_gudang As New ArrayList
    Private Sub N_EMI_SD_Request_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            OpenConn()

            Cmb_Lokasi_Awal.Items.Clear() : Cmb_Lokasi_Tujuan.Items.Clear() : arr_id_kategori_gudang.Clear()
            SQL = "select ID_Kategori_Gudang, Lokasi_Gudang from EMI_Kategori_Gudang_PerLokasi where kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi_Awal.Items.Add(Dr("Lokasi_Gudang")) : Cmb_Lokasi_Tujuan.Items.Add(Dr("Lokasi_Gudang")) : arr_id_kategori_gudang.Add(Dr("ID_Kategori_Gudang"))
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

        Txt_NoSplit.Text = NoSplit


        Try
            OpenConn()

            SQL = "select b.Kode_Barang, b.nama, b.ID_Kategori_Gudang, c.Lokasi_Gudang "
            SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a, Barang b, EMI_Kategori_Gudang_PerLokasi c, Stock_Owner_Gudang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
            SQL = SQL & "and c.kode_perusahaan = d.kode_Perusahaan and c.lokasi_gudang = d.kode_Stock_owner "
            SQL = SQL & "and d.Flag_QC is null "
            SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & NoSplit & "' "
            SQL = SQL & "and a.Kode_Barang = '" & Kd_Barang & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select b.Kode_Barang, b.nama, b.ID_Kategori_Gudang, c.Lokasi_Gudang "
            SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a, Barang b, EMI_Kategori_Gudang_PerLokasi c, Stock_Owner_Gudang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
            SQL = SQL & "and c.kode_perusahaan = d.kode_Perusahaan and c.lokasi_gudang = d.kode_Stock_owner "
            SQL = SQL & "and d.Flag_QC is null "
            SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & NoSplit & "' "
            SQL = SQL & "and a.Kode_Barang = '" & Kd_Barang & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Txt_KdBarang.Text = Dr("Kode_Barang")
                    Txt_NmBarang.Text = Dr("nama")

                    Cmb_Lokasi_Awal.SelectedIndex = arr_id_kategori_gudang.IndexOf(Dr("ID_Kategori_Gudang"))

                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Lokasi_Tujuan.DroppedDown = True : Cmb_Lokasi_Tujuan.Focus()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Txt_NoSplit.Text.Trim.Length = 0 Or Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Data Tidak Lengkap", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Cmb_Lokasi_Tujuan.SelectedIndex = -1 Then
            MessageBox.Show("Harap Pilih Dahulu Lokasi Tujuan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi_Tujuan.DroppedDown = True : Cmb_Lokasi_Tujuan.Focus() : Exit Sub
        End If

        If MessageBox.Show("Yakin Ingin Ubah Lokasi Tujuan ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("RM_Ubah_Lokasi_Tujuan") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Update Lokasi Tujuan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SQL = "select ID_Kategori_Gudang, Kode_Stock_Owner, Kode_Barang from barang where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Kd_Barang & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    SQL = "update barang set Id_Kategori_Gudang = '" & arr_id_kategori_gudang(Cmb_Lokasi_Tujuan.SelectedIndex) & "' where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Kd_Barang & "' "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Lokasi Tujuan Berhasil Di Update", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Emi_Request_Material.kosong()
        'EMI_Produksi.LoadDataDGV()
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class
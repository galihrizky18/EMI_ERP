
Imports System.Reflection

Public Class EMI_Selisih_Barang_Masuk_SD_SN
    Dim ArrSn As New ArrayList
    Dim ArrTglProduksi, ArrExpired, ArrSusunan, arrWarehouse As New ArrayList
    Public FakturBM As String = ""
    Public Harga As String = ""
    Public Plus As String

    Private Sub HitungGrand()

        Dim ttl As Double = 0

        'For i As Integer = 0 To Ubah_Keterangan_Tarik_Kontainer.DataGridView3.Rows.Count - 1
        '    ttl = ttl + Val(HilangkanTanda(Ubah_Keterangan_Tarik_Kontainer.DataGridView3.Rows.Item(i).Cells(8).Value))
        'Next

        'Ubah_Keterangan_Tarik_Kontainer.TextBoxBiayaStorage.Text = Format(ttl, "N2")
    End Sub
    Public Sub kosong()
        GetTime()
        FakturBM = ""
        Harga = ""
        TextBoxQty.Text = ""
        TextBoxNoKonte.Text = ""
        TextBoxkdBrg.Text = ""
        TextBoxNmBrg.Text = ""
        TextBoxLokasi.Text = ""
        ComboBoxTgl.Items.Clear()
        ComboBoxTgl.SelectedIndex = -1
        TanggalExpired.Value = Tanggal_Sekarang
        TanggalProduksi.Value = Tanggal_Sekarang
        ArrSn.Clear()
        ArrTglProduksi.Clear()
        CheckBox1.Checked = False
        ComboBoxTgl.Enabled = True
        TanggalProduksi.Enabled = False
        TanggalExpired.Enabled = False
        If Plus = "Y" Then
            CheckBox1.Enabled = True

        Else
            CheckBox1.Enabled = False
        End If
    End Sub

    Private Sub Simpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Simpan.Click
        GetTime()

        If Plus = "Y" Then

            If CheckBox1.Checked = True Then
                If Format(TanggalProduksi.Value, "yyyy-MM-dd") > Format(TanggalExpired.Value, "yyyy-MM-dd") Then
                    MessageBox.Show("Tanggal Produksi Tidak Boleh Lebih Besar dari Tanggal Expired!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TanggalProduksi.Focus() : Exit Sub
                ElseIf Format(TanggalProduksi.Value, "yyyy-MM-dd") > Format(Tanggal_Sekarang, "yyyy-MM-dd") Then
                    MessageBox.Show("Tanggal Produksi Tidak Boleh di Tanggal Maju!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TanggalProduksi.Focus() : Exit Sub
                End If


                If Harga = "" Or Harga = "0" Then
                    MessageBox.Show("Terjadi kesalahan, Ulangi Transaksi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim Rand As New Random
                Dim Kode_Unik As String = Format(Rand.Next(0, 999), "000") & Format(Tanggal_Sekarang, "HHmmss")

                Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & Harga & Tanda_SN & "02" & Tanda_SN & Format(Tanggal_Sekarang, "yyyy-MM-dd")

                Try
                    OpenConn()

                    Dim IDSusunan_Barang As String = ""
                    SQL = "Select urut from barang_detail_susunan where Kode_Barang = '" & TextBoxkdBrg.Text & "' "
                    SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' and flag_default='Y' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            IDSusunan_Barang = Dr("urut")
                        Else
                            Dr.Close()
                            CloseConn()
                            CloseTrans()
                            MessageBox.Show("Susunan tidak ditemukan")
                            Exit Sub
                        End If
                    End Using

                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                EMI_Selisih_Barang_Masuk.DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(15).Value = Format(TanggalExpired.Value, "dd MMM yyyy")
                EMI_Selisih_Barang_Masuk.DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(14).Value = Format(TanggalProduksi.Value, "dd MMM yyyy")
                EMI_Selisih_Barang_Masuk.DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(16).Value = SN
            Else
                If ComboBoxTgl.SelectedIndex = -1 Then
                    MessageBox.Show("Rak Harus di Pilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ComboBoxTgl.Focus() : Exit Sub
                End If

                Try
                    OpenConn()

                    Dim satuan_barang As String = ""
                    Dim jumlah_satuan_Rak As Double = 0
                    SQL = "select * from barang_detail_susunan where "
                    SQL = SQL & "urut ='" & ArrSusunan.Item(ComboBoxTgl.SelectedIndex) & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            satuan_barang = dr("satuan_jumlah")
                        Else
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Susunan tidak ada . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & TextBoxkdBrg.Text & "',"
                    SQL = SQL & "'" & txtSatuan.Text & "','" & satuan_barang & "',"
                    SQL = SQL & "" & TextBoxQty.Text & ") as Hasil "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            jumlah_satuan_Rak = Math.Ceiling(dr("hasil"))
                        End If
                    End Using

                    SQL = "select * from View_Warehouse_Position_detail where "
                    SQL = SQL & "Id_WMS_Warehouse_Position ='" & arrWarehouse.Item(ComboBoxTgl.SelectedIndex) & "' and kode_barang ='" & TextBoxkdBrg.Text & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If dr("jumlah") + jumlah_satuan_Rak > dr("total_muat") Then
                                dr.Close()
                                CloseConn()
                                MessageBox.Show("Rak sudah full . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Rak tidak ada . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try


                EMI_Selisih_Barang_Masuk.DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(15).Value = ArrExpired.Item(ComboBoxTgl.SelectedIndex)
                EMI_Selisih_Barang_Masuk.DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(14).Value = ArrTglProduksi.Item(ComboBoxTgl.SelectedIndex)
                EMI_Selisih_Barang_Masuk.DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(16).Value = ArrSn.Item(ComboBoxTgl.SelectedIndex)
            End If

        Else
            If ComboBoxTgl.SelectedIndex = -1 Then
                MessageBox.Show("Rak Harus di Pilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBoxTgl.Focus() : Exit Sub
            End If

            EMI_Selisih_Barang_Masuk.DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(15).Value = ArrExpired.Item(ComboBoxTgl.SelectedIndex)
            EMI_Selisih_Barang_Masuk.DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(14).Value = ArrTglProduksi.Item(ComboBoxTgl.SelectedIndex)
            EMI_Selisih_Barang_Masuk.DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(16).Value = ArrSn.Item(ComboBoxTgl.SelectedIndex)
        End If



        Me.Close()
    End Sub

    Private Sub TanggalTarik_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TanggalProduksi.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxkdBrg.Focus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxkdBrg.KeyPress
        If e.KeyChar = Chr(13) Then Simpan.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Display_Tanggal_Tarik_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            OpenConn()
            ComboBoxTgl.Items.Clear() : ArrSn.Clear() : ArrTglProduksi.Clear()
            ArrExpired.Clear() : ArrSusunan.Clear() : arrWarehouse.Clear()
            SQL = "select d.*, e.keterangan, f.Tgl_Expired, f.Tgl_Produksi from emi_pembelian_ETA_detail_PO a, emi_pembelian_ETA_detail_Kendaraan b, "
            SQL = SQL & "emi_Pembelian_Barang_masuk c, emi_Pembelian_Barang_masuk_detail_rak d, View_Warehouse_Position e, barang_sn f "
            SQL = SQL & "where a.No_PO ='" & TextBoxNoKonte.Text & "' and a.Kode_Perusahaan =b.Kode_Perusahaan and a.No_SJ=b.No_SJ and "
            SQL = SQL & "b.Kode_Perusahaan =c.KOde_Perusahaan and b.No_SJ=c.NO_Nota and b.No_Plat=c.no_plat and c.status is null "
            SQL = SQL & "and c.Kode_Perusahaan =d.Kode_Perusahaan and c.NO_faktur=d.No_faktur "
            SQL = SQL & "and d.Kode_Perusahaan =e.KOde_Perusahaan and d.Id_Warehouse=e.Id_WMS_Warehouse_Position and "
            SQL = SQL & "d.Kode_Perusahaan=f.Kode_Perusahaan and d.Serial_Number=f.Serial_Number and d.Kode_Barang=f.Kode_Barang and d.Kode_Stock_Owner=f.Kode_Stock_Owner "
            SQL = SQL & "and d.Kode_Stock_Owner ='" & TextBoxLokasi.Text & "' and d.Kode_Barang ='" & TextBoxkdBrg.Text & "' and d.Kode_Perusahaan ='" & KodePerusahaan & "'"

            SQL = "Select f.*, g.Labeling_WMS_Position as keterangan from emi_Pembelian_po a, emi_pembelian_po_detail b, EMI_Pembelian_Loading c, "
            SQL = SQL & "EMI_Pembelian_Loading_Detail d, emi_barang_masuk_perpallet e, emi_barang_masuk_perpallet_detail f, "
            SQL = SQL & "View_Warehouse_Position g where  "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.NO_faktur = b.nO_faktur "
            SQL = SQL & "And a.status Is null And a.No_faktur='" & TextBoxNoKonte.Text & "' and "
            SQL = SQL & "b.Kode_Stock_Owner ='" & TextBoxLokasi.Text & "' and b.Kode_Barang='" & TextBoxkdBrg.Text & "' and "
            SQL = SQL & "b.No_Urut = d.Urut_PO And d.Kode_Perusahaan = c.Kode_Perusahaan And c.No_Faktur = d.no_faktur And c.Status Is null "
            SQL = SQL & "And d.Urut_Oto=f.Urut_Loading And e.Kode_Perusahaan=f.Kode_Perusahaan And e.No_Faktur=f.no_faktur And e.Status Is null "
            SQL = SQL & "And f.Id_Warehouse=g.Id_WMS_Warehouse_Position "

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBoxTgl.Items.Add(dr("Keterangan")) : ArrSn.Add(dr("Serial_Number"))
                    ArrTglProduksi.Add(Format(dr("Tgl_Produksi"), "dd MMM yyyy"))
                    ArrExpired.Add(Format(dr("Tgl_Expired"), "dd MMM yyyy"))
                    ArrSusunan.Add(dr("id_susunan"))
                    arrWarehouse.Add(dr("Id_Warehouse"))
                Loop

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TanggalProduksi_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TanggalProduksi.ValueChanged

    End Sub


    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            ComboBoxTgl.SelectedIndex = -1
            ComboBoxTgl.Enabled = False
            TanggalProduksi.Enabled = True
            TanggalExpired.Enabled = True
        Else
            ComboBoxTgl.SelectedIndex = -1
            ComboBoxTgl.Enabled = True
            TanggalProduksi.Enabled = False
            TanggalExpired.Enabled = False
        End If
    End Sub

    Private Sub TextBoxLokasi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxLokasi.TextChanged

    End Sub

End Class
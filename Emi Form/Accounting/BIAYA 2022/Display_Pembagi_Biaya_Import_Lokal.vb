Public Class Display_Pembagi_Biaya_Import_Lokal
    Dim LvNo1 As String
    Dim LvNoFakPajak1 As String
    Dim LvNilai_Input1 As String

    Dim CellNo1 As Integer = 0
    Dim CellNoFakPajak1 As Integer = 1
    Dim CellNilai_Input1 As Integer = 2

    Dim LvNo2 As String
    Dim LvNoFakPajak2 As String
    Dim LvNilai_Input2 As String

    Dim CellNo2 As Integer = 0
    Dim CellNoFakPajak2 As Integer = 1
    Dim CellNilai_Input2 As Integer = 2

    Public Sub Get_Isi_Listview1(ByVal No_Index As Integer)
        LvNo1 = DataGridView1.Rows(No_Index).Cells(CellNo1).Value.ToString
        LvNoFakPajak1 = DataGridView1.Rows(No_Index).Cells(CellNoFakPajak1).Value.ToString
        LvNilai_Input1 = DataGridView1.Rows(No_Index).Cells(CellNilai_Input1).Value.ToString
    End Sub

    Public Sub Get_Isi_Listview2(ByVal No_Index As Integer)
        LvNo2 = DataGridView2.Rows(No_Index).Cells(CellNo2).Value.ToString
        LvNoFakPajak2 = DataGridView2.Rows(No_Index).Cells(CellNoFakPajak2).Value.ToString
        LvNilai_Input2 = DataGridView2.Rows(No_Index).Cells(CellNilai_Input2).Value.ToString
    End Sub

    Private Sub Display_Pembagi_Biaya_Import_Lokal_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Display_Pembagi_Biaya_Import_Lokal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Kosong()

            TextBox5_TextChanged(Txt_NilaiTambahan, e)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Kosong()
        Txt_NilaiTambahan.Focus()
        'Txt_NilaiTambahan.Text = 0
        Txt_JmlPembagiPPN.Text = ""
        Txt_JmlPembagiPPh.Text = ""

        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()

        Txt_TotalPPN.Text = 0
        Txt_TotalPPh.Text = 0
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Txt_JmlPembagiPPh.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah Pembagi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim b As Integer = 1
        DataGridView2.Rows.Clear()
        For a As Integer = 0 To Txt_JmlPembagiPPh.Text - 1
            DataGridView2.Rows.Add(1)
            DataGridView2.Rows(a).Cells(0).Value = b
            DataGridView2.Rows(a).Cells(1).Value = ""
            DataGridView2.Rows(a).Cells(2).Value = ""
            b = b + 1
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Txt_JmlPembagiPPN.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah Pembagi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim b As Integer = 1
        DataGridView1.Rows.Clear()
        For a As Integer = 0 To Txt_JmlPembagiPPN.Text - 1
            DataGridView1.Rows.Add(1)
            DataGridView1.Rows(a).Cells(0).Value = b
            DataGridView1.Rows(a).Cells(1).Value = ""
            DataGridView1.Rows(a).Cells(2).Value = ""
            b = b + 1
        Next
    End Sub

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit

        Get_Isi_Listview1(DataGridView1.CurrentRow.Index)
        If IsNumeric(LvNilai_Input1) = False Or Val(LvNilai_Input1) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNilai_Input1).Value = 0
        End If

        HitungTotal()
    End Sub

    Private Sub DataGridView2_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit

        Get_Isi_Listview2(DataGridView2.CurrentRow.Index)
        If IsNumeric(LvNilai_Input2) = False Or Val(LvNilai_Input2) < 0 Then
            DataGridView2.CurrentRow.Cells(CellNilai_Input2).Value = 0
        End If

        HitungTotal2()
    End Sub

    Private Sub HitungTotal()
        'Dim total_UM1 As Double = 0
        'Dim total_Dpt1 As Double = 0
        'Dim total_UM2 As Double = 0
        'Dim total_Dpt2 As Double = 0

        ''For index As Integer = 0 To DataGridView1.Rows.Count - 1
        ''    Get_Isi_Listview1(index)
        ''    total_UM = total_UM + Val(HilangkanTanda(LvNilai_Input1))
        ''Next

        ''For index As Integer = 0 To DataGridView2.Rows.Count - 1
        ''    Get_Isi_Listview2(index)
        ''    total_Dpt = total_Dpt + Val(HilangkanTanda(LvNilai_Input2))
        ''Next

        Dim totalInputPPN As Double = 0
        'Dim totalInputPPh As Double = 0

        For index As Integer = 0 To DataGridView1.Rows.Count - 1
            Get_Isi_Listview1(index)
            totalInputPPN = totalInputPPN + Val(HilangkanTanda(LvNilai_Input1))
        Next

        'For index As Integer = 0 To DataGridView2.Rows.Count - 1
        '    Get_Isi_Listview2(index)
        '    totalInputPPh = totalInputPPh + Val(HilangkanTanda(LvNilai_Input2))
        'Next

        ''Txt_TotalPPN.Text = Format(total_Dpt1 + total_UM1, "N0")
        ''Txt_TotalPPh.Text = Format(total_Dpt + total_UM, "N0")

        Txt_TotalPPN.Text = Format(totalInputPPN, "N0")
        'Txt_TotalPPh.Text = Format(totalInputPPh, "N0")
    End Sub

    Private Sub HitungTotal2()
        Dim totalInputPPh As Double = 0

        For index As Integer = 0 To DataGridView2.Rows.Count - 1
            Get_Isi_Listview2(index)
            totalInputPPh = totalInputPPh + Val(HilangkanTanda(LvNilai_Input2))
        Next

        Txt_TotalPPh.Text = Format(totalInputPPh, "N0")
    End Sub

    Public Sub TextBox5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_NilaiTambahan.TextChanged
        Dim a As Double = 0
        a = Val(HilangkanTanda(Txt_ttlBiaya.Text)) + Val(HilangkanTanda(Txt_NilaiTambahan.Text))
        Txt_NilaiTotal.Text = Format(a, "N0")

        Dim Nppn As Double = 0
        Nppn = a * Val(Txt_PersenPPN.Text) / 100
        Txt_hslPPN.Text = Format(Nppn, "N0")

        Dim Npph As Double = 0
        Npph = a * Val(Txt_PersenPPh.Text) / 100
        Txt_hslPPh.Text = Format(Npph, "N0")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Txt_NilaiTambahan.Text.Trim.Length = 0 Then
            MessageBox.Show("Nilai Tambahan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NilaiTambahan.Focus()
            Exit Sub
        ElseIf Txt_TotalPPN.Text.Trim.Length = 0 Then
            MessageBox.Show("Total harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_TotalPPN.Focus()
            Exit Sub
        ElseIf Txt_hslPPN.Text <> Txt_TotalPPN.Text Then
            MessageBox.Show("Total PPN harus sama.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Txt_hslPPh.Text <> Txt_TotalPPh.Text Then
            MessageBox.Show("Total PPh harus sama.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Txt_hslPPN.Text <> 0 Then
            For indexDGV1 As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview1(indexDGV1)
                If LvNoFakPajak1 = "" Then
                    MessageBox.Show("No Faktur Pajak PPN harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf LvNilai_Input1 = "" Then
                    MessageBox.Show("Nilai Input PPN harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
        End If

        If Txt_hslPPh.Text <> 0 Then
            For indexDGV2 As Integer = 0 To DataGridView2.Rows.Count - 1
                Get_Isi_Listview2(indexDGV2)
                If LvNoFakPajak2 = "" Then
                    MessageBox.Show("No Faktur Pajak PPh harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf LvNilai_Input2 = "" Then
                    MessageBox.Show("Nilai Input PPh harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            'Delete PPN
            SQL = "Delete From Display_Biaya_Import_PPN "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and no_faktur ='" & Txt_NoFaktur.Text & "' and "
            SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & Lbl_KdPerusahaan.Text & "' and kode_kategori_biaya_import='" & TxtKodeKategori.Text & "' and mata_uang='" & Txt_MataUang.Text & "' and UserID='" & UserID & "' and Lokasi='" & TxtLokasi.Text & "' "
            ExecuteTrans(SQL)

            'Delete PPh
            SQL = "Delete From Display_Biaya_Import_PPh "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and no_faktur ='" & Txt_NoFaktur.Text & "' and "
            SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & Lbl_KdPerusahaan.Text & "' and kode_kategori_biaya_import='" & TxtKodeKategori.Text & "' and mata_uang='" & Txt_MataUang.Text & "' and UserID='" & UserID & "' and Lokasi='" & TxtLokasi.Text & "' "
            ExecuteTrans(SQL)

            'Simpan PPN
            For index As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview1(index)
                SQL = "Insert into Display_Biaya_Import_PPN (Kode_Perusahaan, Kode_Perusahaan_Biaya_Import, No_Faktur, No_Pembagi, No_Faktur_Pajak, Nilai_Pembagi, Mata_Uang, Kode_Kategori_Biaya_Import, UserID, Lokasi) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', '" & Lbl_KdPerusahaan.Text & "', '" & Txt_NoFaktur.Text & "', '" & LvNo1 & "', '" & LvNoFakPajak1 & "', '" & LvNilai_Input1 & "', '" & Txt_MataUang.Text & "', '" & TxtKodeKategori.Text & "', '" & UserID & "', '" & TxtLokasi.Text & "') "
                ExecuteTrans(SQL)
            Next

            'Simpan PPh
            For index2 As Integer = 0 To DataGridView2.Rows.Count - 1
                Get_Isi_Listview2(index2)
                SQL = "Insert into Display_Biaya_Import_PPh (Kode_Perusahaan, Kode_Perusahaan_Biaya_Import, No_Faktur, No_Pembagi, No_Faktur_Pajak, Nilai_Pembagi, Mata_Uang, Kode_Kategori_Biaya_Import, UserID, Lokasi) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', '" & Lbl_KdPerusahaan.Text & "', '" & Txt_NoFaktur.Text & "', '" & LvNo2 & "', '" & LvNoFakPajak2 & "', '" & LvNilai_Input2 & "', '" & Txt_MataUang.Text & "', '" & TxtKodeKategori.Text & "', '" & UserID & "', '" & TxtLokasi.Text & "') "
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            'MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Me.Close()
        Dim lv As New ListViewItem
        lv = EMI_Pelunasan_Biaya_Import_By_Perusahaan_Lokal.ListViewMT11.Items.Add(Txt_NoFaktur.Text) 'NoFaktur
        lv.SubItems.Add(Lbl_Tgl.Text) 'Tgl
        lv.SubItems.Add(Lbl_KdPerusahaan.Text) 'Kode Perusahaan
        lv.SubItems.Add(Txt_NmPerusahaan.Text) 'Nama Perusahaan
        lv.SubItems.Add(TxtKodeKategori.Text) 'Kategori Perusahaan
        lv.SubItems.Add(Txt_KategoriPerusahaan.Text) 'Kategori Perusahaan
        lv.SubItems.Add(Txt_MataUang.Text) 'Mata Uang
        lv.SubItems.Add(Txt_ttlBiaya.Text) 'Jumlah
        lv.SubItems.Add(Txt_NilaiTambahan.Text) ' Nilai Tambahan
        lv.SubItems.Add(Txt_NilaiTotal.Text) ' Nilai Total
        lv.SubItems.Add(Txt_PersenPPN.Text) 'Persen PPN
        lv.SubItems.Add(Txt_PersenPPh.Text) 'Persen PPh
        lv.SubItems.Add(Txt_hslPPN.Text) 'Nilai PPN
        lv.SubItems.Add(Txt_hslPPh.Text) ' Nilai PPh
        lv.SubItems.Add(TxtLokasi.Text) ' lks
        lv.SubItems.Add(TxtJenisForm.Text) ' jns form

        EMI_Pelunasan_Biaya_Import_By_Perusahaan_Lokal.Kosong_Bawah()
        EMI_Pelunasan_Biaya_Import_By_Perusahaan_Lokal.Hitung()
        Kosong()
    End Sub

    Private Sub Txt_hslPPh_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_hslPPh.TextChanged

    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class
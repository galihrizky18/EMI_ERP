Public Class Emi_Refraksi2

    Public getFakLoading, getNoPlat, getNmSupplier, getNmSupir, getKdSupplier As String

    Dim dgv_NoPO, dgv_SO, dgv_KdBarang, dgv_NmBarang, dgv_Satuan, dgv_Jumlah, dgv_Warna, dgv_Harga, dgv_HargaBerubah, dgv_UrutPO, dgv_SatuanBarang As String

    Dim cell_NoPo As Integer = 0
    Dim cell_SO As Integer = 1
    Dim cell_KdBarang As Integer = 2
    Dim cell_NmBarang As Integer = 3
    Dim cell_Jumlah As Integer = 4
    Dim cell_Satuan As Integer = 5
    Dim cell_Warna As Integer = 6
    Dim cell_Harga As Integer = 7
    Dim cell_HargaBerubah As Integer = 8
    Dim cell_UrutPO As Integer = 9
    Dim cell_SatuanBarang As Integer = 10

    Private Sub Emi_Refraksi2_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Refraksi2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
    End Sub

    Private Sub Kosong()

        TxtNoLoading.Text = ""
        TxtNoPlat.Text = ""
        Txt_NmSupplier.Text = ""
        Txt_NmSupir.Text = ""
        Txt_KdSupplier.Text = ""

        TxtNoLoading.Text = getFakLoading
        TxtNoPlat.Text = getNoPlat
        Txt_NmSupplier.Text = getNmSupplier
        Txt_NmSupir.Text = getNmSupir
        Txt_KdSupplier.Text = getKdSupplier

        Dgv_DataDetailPO.Rows.Clear()

        LoadData()

    End Sub

    Private Sub LoadData()

        Try
            OpenConn()

            SQL = "select a.No_Faktur ,b.No_PO, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah, b.Satuan, b.Warna, "
            SQL = SQL & "dbo.ubah_satuan(a.Kode_Perusahaan, 'UANG', b.Kode_Barang, b.Satuan_Barang, b.Satuan, "
            SQL = SQL & "dbo.get_hpp((SELECT TOP(1) z.Serial_Number FROM barang_sn z WHERE z.Kode_Stock_Owner = b.Kode_Stock_Owner AND z.Kode_Barang = b.Kode_Barang))) AS Harga_display, "
            SQL = SQL & "b.Urut_PO, b.Satuan_Barang "
            SQL = SQL & "from emi_pembelian_loading a, EMI_Pembelian_Loading_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & getFakLoading & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim rows As Integer = 0

                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_DataDetailPO.Rows.Add(1)
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_NoPo).Value = .Rows(i).Item("No_PO")
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_SO).Value = .Rows(i).Item("Kode_Stock_Owner")
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_KdBarang).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_NmBarang).Value = .Rows(i).Item("Nama")
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_Jumlah).Value = Format(.Rows(i).Item("Jumlah"), "N0")
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_Satuan).Value = .Rows(i).Item("Satuan")
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_Warna).Value = .Rows(i).Item("Warna")
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_Harga).Value = Format(.Rows(i).Item("Harga_display"), "N2")
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_UrutPO).Value = .Rows(i).Item("Urut_PO")
                            Dgv_DataDetailPO.Rows(rows).Cells(cell_SatuanBarang).Value = .Rows(i).Item("Satuan_Barang")

                            Dgv_DataDetailPO.Rows(rows).Cells(cell_HargaBerubah).Style.BackColor = Color.LightGray

                            If .Rows(i).Item("Warna").ToString.ToUpper = "HIJAU" Then
                                Dgv_DataDetailPO.Rows(rows).Cells(cell_Warna).Style.BackColor = Color.LightGreen
                            ElseIf .Rows(i).Item("Warna").ToString.ToUpper = "KUNING" Then
                                Dgv_DataDetailPO.Rows(rows).Cells(cell_Warna).Style.BackColor = Color.LightYellow
                            ElseIf .Rows(i).Item("Warna").ToString.ToUpper = "MERAH" Then
                                Dgv_DataDetailPO.Rows(rows).Cells(cell_Warna).Style.BackColor = Color.FromArgb(250, 39, 53)

                            End If

                            rows = rows + 1
                        Next

                    End If
                End With

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_Dgv_Data(ByVal index As Integer)

        dgv_NoPO = Dgv_DataDetailPO.Rows(index).Cells(cell_NoPo).Value
        dgv_SO = Dgv_DataDetailPO.Rows(index).Cells(cell_SO).Value
        dgv_KdBarang = Dgv_DataDetailPO.Rows(index).Cells(cell_KdBarang).Value
        dgv_NmBarang = Dgv_DataDetailPO.Rows(index).Cells(cell_NmBarang).Value
        dgv_Satuan = Dgv_DataDetailPO.Rows(index).Cells(cell_Satuan).Value
        dgv_Jumlah = Dgv_DataDetailPO.Rows(index).Cells(cell_Jumlah).Value
        dgv_Warna = Dgv_DataDetailPO.Rows(index).Cells(cell_Warna).Value
        dgv_Harga = Dgv_DataDetailPO.Rows(index).Cells(cell_Harga).Value
        dgv_HargaBerubah = Dgv_DataDetailPO.Rows(index).Cells(cell_HargaBerubah).Value
        dgv_UrutPO = Dgv_DataDetailPO.Rows(index).Cells(cell_UrutPO).Value
        dgv_SatuanBarang = Dgv_DataDetailPO.Rows(index).Cells(cell_SatuanBarang).Value

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Dgv_DataDetailPO.Rows.Count = 0 Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim hasDataToInsert As Boolean = False

            For i As Integer = 0 To Dgv_DataDetailPO.Rows.Count - 1
                Get_Dgv_Data(i)

                If String.IsNullOrWhiteSpace(dgv_HargaBerubah) Then
                    Continue For
                End If

                hasDataToInsert = True
                dgv_HargaBerubah = dgv_HargaBerubah.Replace(",", "")

                '==============================
                '=     UBAH HARGA BERUBAH     =
                '==============================
                Dim hargaInput As String = ""
                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'UANG', '" & dgv_KdBarang & "', '" & dgv_SatuanBarang & "', '" & dgv_Satuan & "', "
                SQL = SQL & "'" & dgv_HargaBerubah & "') as harga_input "
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

                '===============================================
                '=     UPDATE EMI_Pembelian_Loading_Detail     =
                '===============================================
                SQL = "update EMI_Pembelian_Loading_Detail set Harga_Refraksi = '" & hargaInput & "', Flag_Refraksi = 'Y'  "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtNoLoading.Text & "' "
                SQL = SQL & "and No_PO = '" & dgv_NoPO & "' and Urut_PO = '" & dgv_UrutPO & "' "
                ExecuteTrans(SQL)

            Next

            If hasDataToInsert = False Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Tidak Ada Data yang akan diInsert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Berhasil Di Simpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            EMI_Refraksi_Display2.kosong()
            Me.Close()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Dgv_DataDetailPO_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataDetailPO.CellEndEdit

        If Dgv_DataDetailPO.Rows.Count = -1 Then Exit Sub

        Dim hargaBerubah As String = Dgv_DataDetailPO.CurrentRow.Cells(cell_HargaBerubah).Value

        If Not IsNumeric(hargaBerubah) Then
            Dgv_DataDetailPO.CurrentRow.Cells(cell_HargaBerubah).Value = ""
            Exit Sub
        End If

        If hargaBerubah.Contains(",") Then
            CloseConn()
            MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_DataDetailPO.CurrentRow.Cells(cell_HargaBerubah).Value = ""
            Exit Sub
        End If

        Dim nilai As Decimal = Decimal.Parse(hargaBerubah)
        Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

        Dgv_DataDetailPO.CurrentRow.Cells(cell_HargaBerubah).Value = formattedValue

    End Sub

    Private Sub Dgv_DataDetailPO_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataDetailPO.CellEnter

        If Dgv_DataDetailPO.CurrentCell.ColumnIndex = cell_HargaBerubah Then
            Dim hargaBerubah As String = Dgv_DataDetailPO.CurrentCell.Value

            If hargaBerubah = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = hargaBerubah.Replace(",", "") ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            Dgv_DataDetailPO.CurrentCell.Value = nilai
        End If
    End Sub

    Private Sub Dgv_DataDetailPO_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataDetailPO.CellLeave
        If Dgv_DataDetailPO.CurrentCell.ColumnIndex = cell_HargaBerubah Then
            Dim hargaBerubah As String = Dgv_DataDetailPO.CurrentCell.Value

            If Not String.IsNullOrEmpty(hargaBerubah) Then

                Dim nilai As Decimal = Decimal.Parse(hargaBerubah)
                Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

                Dgv_DataDetailPO.CurrentCell.Value = formattedValue
            End If
        End If
    End Sub

End Class
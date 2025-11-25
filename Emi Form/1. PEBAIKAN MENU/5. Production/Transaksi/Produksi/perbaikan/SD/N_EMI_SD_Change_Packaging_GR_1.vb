Public Class N_EMI_SD_Change_Packaging_GR_1

    Public No_Split, Kd_Barang, Nm_Barang, Jumlah_Produksi, Satuan_Produksi As String

    Dim Dgv_Kd_Barang, Dgv_Nm_Barang, Dgv_Jmlh_Request, Dgv_Jmlh_Kirim, Dgv_Sisa, Dgv_Jumlah, Dgv_Satuan As String

    Dim Cell_Kd_Barang As Integer = 0

    Dim Cell_Nm_Barang As Integer = 1

    Dim Cell_Jmlh_Request As Integer = 2



    Dim Cell_Jmlh_Kirim As Integer = 3
    Dim Cell_Sisa As Integer = 4
    Dim Cell_Jumlah As Integer = 5
    Dim Cell_Satuan As Integer = 6

    Private Sub N_EMI_SD_Change_Packaging_GR_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
    End Sub

    Public Sub Kosong()

        Try
            OpenConn()

            Cmb_Satuan_Produksi.Items.Clear()
            SQL = "select satuan from EMI_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Satuan_Produksi.Items.Add(Dr("satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Txt_NoSplit.Text = No_Split
        Txt_Kd_Barang.Text = Kd_Barang : Txt_Nm_Barang.Text = Nm_Barang
        Txt_Jumlah_Produksi.Text = Format(Val(HilangkanTanda(Jumlah_Produksi)), "N0")

        Cmb_Satuan_Produksi.SelectedItem = Satuan_Produksi

        Load_Data_Packaging()
    End Sub

    Private Sub Get_Data_DGV(ByVal index As Integer)
        Dgv_Kd_Barang = Dgv_Packaging.Rows(index).Cells(Cell_Kd_Barang).Value
        Dgv_Nm_Barang = Dgv_Packaging.Rows(index).Cells(Cell_Nm_Barang).Value
        Dgv_Jmlh_Request = Dgv_Packaging.Rows(index).Cells(Cell_Jmlh_Request).Value
        Dgv_Jmlh_Kirim = Dgv_Packaging.Rows(index).Cells(Cell_Jmlh_Kirim).Value
        Dgv_Sisa = Dgv_Packaging.Rows(index).Cells(Cell_Sisa).Value
        Dgv_Jumlah = Dgv_Packaging.Rows(index).Cells(Cell_Jumlah).Value
        Dgv_Satuan = Dgv_Packaging.Rows(index).Cells(Cell_Satuan).Value
    End Sub

    Private Sub Load_Data_Packaging()
        If Txt_NoSplit.Text = "" Or Txt_Kd_Barang.Text = "" Then
            MessageBox.Show("No Split atau Kode Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
            Exit Sub
        End If

        Try
            OpenConn()

            Dgv_Packaging.Rows.Clear()
            SQL = ";with cte as ( "
            SQL = SQL & "select a.No_Faktur_Order, b.Kode_Barang, c.Nama as Nama_Barang, sum(round(b.Jumlah, 2)) AS Jumlah_Request, "
            SQL = SQL & "isnull(( "
            SQL = SQL & "select dbo.Ubah_Satuan(z.Kode_Perusahaan, 'masa', x.Kode_Barang, x.Satuan_Barang, x.Satuan, sum(CEILING(w.Jumlah))) as Jumlah "
            SQL = SQL & "from Tf_Stock_Parent z, Tf_Stock x, Tf_Stock_Det y, Tf_Stock_Det2 w "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
            SQL = SQL & "and y.No_Faktur = w.No_Faktur and y.Urut_Oto = w.Urut_Det "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and x.Urut_Material_Requisition_Convert = d.Urut_Oto "
            SQL = SQL & "and x.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "group by z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang, x.Satuan "
            SQL = SQL & "), 0) as Jumlah_Kirim, b.Satuan "
            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_det b, barang c, Emi_Material_Requisition_Det_Convert d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and b.No_Faktur = d.No_Faktur and b.Urut_Oto = d.No_Urut_Det "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and b.Jenis_Material = 'Packaging' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur_Order = '" & Txt_NoSplit.Text & "' "
            SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur_Order, b.Kode_Barang, c.Nama, b.Satuan, d.Urut_Oto "
            SQL = SQL & ") select No_Faktur_Order, Kode_Barang, Nama_Barang, sum(Jumlah_Request) as Jumlah_Request, sum(Jumlah_Kirim) as jumlah_kirim, "
            SQL = SQL & "case when sum(Jumlah_Kirim) = 0 then 0 else ROUND((sum(Jumlah_Kirim) - sum(Jumlah_Request)), 2) end as Sisa, satuan "
            SQL = SQL & "from cte "
            SQL = SQL & "group by No_Faktur_Order, Kode_Barang, Nama_Barang, satuan "
            SQL = SQL & "order by Kode_Barang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Packaging.Rows.Add(1)
                            Dgv_Packaging.Rows(i).Cells(Cell_Kd_Barang).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_Packaging.Rows(i).Cells(Cell_Nm_Barang).Value = .Rows(i).Item("Nama_Barang")
                            Dgv_Packaging.Rows(i).Cells(Cell_Jmlh_Request).Value = Format(.Rows(i).Item("Jumlah_Request"), "N0")
                            Dgv_Packaging.Rows(i).Cells(Cell_Jmlh_Kirim).Value = Format(.Rows(i).Item("jumlah_kirim"), "N0")
                            Dgv_Packaging.Rows(i).Cells(Cell_Sisa).Value = Format(.Rows(i).Item("Sisa"), "N0")
                            Dgv_Packaging.Rows(i).Cells(Cell_Jumlah).Value = 0
                            Dgv_Packaging.Rows(i).Cells(Cell_Satuan).Value = .Rows(i).Item("satuan")

                            Dgv_Packaging.Rows(i).Cells(Cell_Jumlah).Style.BackColor = Color.FromArgb(235, 235, 235)

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

    Private Sub Dgv_Packaging_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Packaging.CellEndEdit
        If Dgv_Packaging.Rows.Count = 0 Then Exit Sub

        If Dgv_Packaging.CurrentCell.ColumnIndex = Cell_Jumlah Then
            Get_Data_DGV(Dgv_Packaging.CurrentRow.Index)

            If Dgv_Jumlah < Dgv_Sisa Then
                MessageBox.Show("Jumlah Tidak Boleh Kurang Dari Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_Packaging.CurrentCell.Value = 0
                Exit Sub
            End If

            Dgv_Packaging.CurrentCell.Value = Format(Val(HilangkanTanda(Dgv_Jumlah)), "N0")

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Dgv_Packaging.Rows.Count = 0 Then
            MessageBox.Show("Tidak Ada Data yang Bisa Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


    End Sub


End Class
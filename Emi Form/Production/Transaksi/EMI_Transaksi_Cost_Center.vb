Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class EMI_Transaksi_Cost_Center
    Dim arrBulan, arrBulanMM As New ArrayList

    Dim LvSO As String
    Dim LvKd_Brg As String
    Dim LvNm_Brg As String
    Dim LvId As String
    Dim LVKeterangan As String
    Dim LvTotal As String
    Dim LvNilai_Per_Pcs As String

    Dim CellSO As Integer = 0
    Dim CellKd_Brg As Integer = 1
    Dim CellNm_Brg As Integer = 2
    Dim CellId As Integer = 3
    Dim CellKeterangan As Integer = 4
    Dim CellTotal As Integer = 5
    Dim CellNilai_Per_Pcs As Integer = 6

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvSO = DataGridView1.Rows(No_Index).Cells(CellSO).Value.ToString
        LvKd_Brg = DataGridView1.Rows(No_Index).Cells(CellKd_Brg).Value.ToString
        LvNm_Brg = DataGridView1.Rows(No_Index).Cells(CellNm_Brg).Value.ToString
        LvId = DataGridView1.Rows(No_Index).Cells(CellId).Value.ToString
        LVKeterangan = DataGridView1.Rows(No_Index).Cells(CellKeterangan).Value.ToString
        LvTotal = DataGridView1.Rows(No_Index).Cells(CellTotal).Value.ToString
        LvNilai_Per_Pcs = DataGridView1.Rows(No_Index).Cells(CellNilai_Per_Pcs).Value.ToString
    End Sub
    Private Sub Emi_Transaksi_Cost_Center_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Transaksi_Cost_Center_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub


    Private Sub kosong()
        get_jam()

        Try
            OpenConn()

            ComboBox1.Items.Clear() : arrBulan.Clear() : arrBulanMM.Clear()
            ComboBox1.Items.Add("January") : arrBulan.Add("1") : arrBulanMM.Add("01")
            ComboBox1.Items.Add("February") : arrBulan.Add("2") : arrBulanMM.Add("02")
            ComboBox1.Items.Add("Maret") : arrBulan.Add("3") : arrBulanMM.Add("03")
            ComboBox1.Items.Add("April") : arrBulan.Add("4") : arrBulanMM.Add("04")
            ComboBox1.Items.Add("Mei") : arrBulan.Add("5") : arrBulanMM.Add("05")
            ComboBox1.Items.Add("Juni") : arrBulan.Add("6") : arrBulanMM.Add("06")
            ComboBox1.Items.Add("Juli") : arrBulan.Add("7") : arrBulanMM.Add("07")
            ComboBox1.Items.Add("Agustus") : arrBulan.Add("8") : arrBulanMM.Add("08")
            ComboBox1.Items.Add("September") : arrBulan.Add("9") : arrBulanMM.Add("09")
            ComboBox1.Items.Add("Oktober") : arrBulan.Add("10") : arrBulanMM.Add("10")
            ComboBox1.Items.Add("November") : arrBulan.Add("11") : arrBulanMM.Add("11")
            ComboBox1.Items.Add("Desember") : arrBulan.Add("12") : arrBulanMM.Add("12")
            ComboBox1.SelectedIndex = -1
            ComboBox1.Enabled = True

            ComboBox2.Items.Clear()
            Dim tahun_awal As Integer = Date.Now.Year - 2
            Dim tahun_akhir As Integer = Date.Now.Year + 2
            For a As Integer = tahun_awal To tahun_akhir
                ComboBox2.Items.Add(a)
            Next
            ComboBox2.SelectedIndex = -1
            ComboBox2.Enabled = True

            ComboBox3.Items.Clear()
            SQL = "select Kode_Stock_Owner from Stock_Owner "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        ComboBox3.Items.Add(.Rows(i).Item("Kode_Stock_Owner"))
                    Next
                End With
            End Using
            ComboBox3.Text = Lokasi

            Dim lok_gudang As String = ""
            SQL = "select Kode_Stock_Owner_Gudang from binding_lokasi_gudang where gudang_default = 'Y' and kode_stock_owner ='" & ComboBox3.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    lok_gudang = Dr("Kode_Stock_Owner_Gudang")
                End If
            End Using

            DataGridView1.Rows.Clear()



            'SQL = "select Id_Cost_Center,Keterangan from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "order by Keterangan "
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        For i As Integer = 0 To .Rows.Count - 1
            '            DataGridView1.Rows.Add(1)
            '            DataGridView1.Rows(i).Cells(0).Value = .Rows(i).Item("Id_Cost_Center")
            '            DataGridView1.Rows(i).Cells(1).Value = .Rows(i).Item("Keterangan")
            '            DataGridView1.Rows(i).Cells(2).Value = ""
            '            DataGridView1.Rows(i).Cells(3).Value = ""
            '        Next
            '    End With
            'End Using

            DataGridView1.Columns(CellTotal).ReadOnly = False
            DataGridView1.Columns(CellNilai_Per_Pcs).ReadOnly = False

            Btn_Refresh.Enabled = True

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur()
        Dim FPro_Results As String = "TCC"
        TxtBarangMasuk_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Transaksi_Cost_Center", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))
    End Sub
    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Get_Isi_Listview(DataGridView1.CurrentRow.Index)
        If IsNumeric(LvTotal) = False Or Val(LvTotal) < 0 Then
            DataGridView1.CurrentRow.Cells(CellTotal).Value = 0
        ElseIf IsNumeric(LvNilai_Per_Pcs) = False Or Val(LvNilai_Per_Pcs) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNilai_Per_Pcs).Value = 0
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        get_jam()
        If TxtBarangMasuk_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No transaksi Harus diisi....!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtBarangMasuk_NoFaktur.Focus() : Exit Sub
        ElseIf ComboBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Bulan Harus diisi....!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        ElseIf ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Tahun Harus diisi....!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "INSERT INTO Emi_Transaksi_Cost_Center(Kode_Perusahaan,No_Faktur,Bulan,Tahun,UserID,Tanggal,Jam) VALUES("
            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & arrBulanMM.Item(ComboBox1.SelectedIndex) & "',"
            SQL = SQL & "'" & ComboBox2.Text & "','" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "') "
            ExecuteTrans(SQL)

            For a As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(a)
                SQL = "INSERT INTO Emi_Transaksi_Cost_Center_Detail(Kode_Perusahaan,No_Faktur,Id_Cost_Center,Total,Nilai_Per_Pcs,Kode_Stock_Owner,Kode_Barang) "
                SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvId & "',"
                SQL = SQL & "'" & HilangkanTanda(LvTotal) & "','" & HilangkanTanda(LvNilai_Per_Pcs) & "','" & LvSO & "','" & LvKd_Brg & "')"
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            Exit Sub
        End If
        data()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            Exit Sub
        End If
        data()
    End Sub

    Private Sub data()
        get_jam()

        Try
            OpenConn()

            SQL = "select a.No_Faktur,b.Kode_Stock_Owner,b.Kode_Barang,d.Nama,b.Id_Cost_Center,c.Keterangan,b.Total,b.Nilai_Per_pcs "
            SQL = SQL & "from Emi_Transaksi_Cost_Center a,Emi_Transaksi_Cost_Center_Detail b,EMI_Master_Cost_Center c,Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Cost_Center = c.Id_Cost_Center "
            SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & arrBulanMM.Item(ComboBox1.SelectedIndex) & "' "
            SQL = SQL & "and a.Tahun = '" & ComboBox2.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        TxtBarangMasuk_NoFaktur.Text = .Rows(0).Item("No_Faktur")
                        DataGridView1.Rows.Clear()
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Stock_Owner")
                            DataGridView1.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows(i).Cells(2).Value = .Rows(i).Item("Nama")
                            DataGridView1.Rows(i).Cells(3).Value = .Rows(i).Item("Id_Cost_Center")
                            DataGridView1.Rows(i).Cells(4).Value = .Rows(i).Item("Keterangan")
                            DataGridView1.Rows(i).Cells(5).Value = .Rows(i).Item("Total")
                            DataGridView1.Rows(i).Cells(6).Value = .Rows(i).Item("Nilai_Per_pcs")
                        Next

                        ComboBox1.Enabled = False
                        ComboBox2.Enabled = False
                        Btn_Refresh.Enabled = False
                        DataGridView1.Columns(CellTotal).ReadOnly = True
                        DataGridView1.Columns(CellNilai_Per_Pcs).ReadOnly = True
                    Else
                        get_no_faktur()
                        ComboBox1.Enabled = True
                        ComboBox2.Enabled = True
                        Btn_Refresh.Enabled = True
                        DataGridView1.Columns(CellTotal).ReadOnly = False
                        DataGridView1.Columns(CellNilai_Per_Pcs).ReadOnly = False

                        Dim lok_gudang As String = ""
                        SQL = "select Kode_Stock_Owner_Gudang from binding_lokasi_gudang where gudang_default = 'Y' and kode_stock_owner ='" & ComboBox3.Text & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                lok_gudang = Dr("Kode_Stock_Owner_Gudang")
                            End If
                        End Using

                        DataGridView1.Rows.Clear()
                        SQL = "select a.Kode_Stock_Owner,a.Kode_Barang,a.Nama,b.Id_Cost_Center,b.Keterangan "
                        SQL = SQL & "from Barang a,EMI_Master_Cost_Center b,EMI_Group_Jenis c where a.Kode_Perusahaan = b.Kode_Perusahaan "
                        SQL = SQL & "and a.Id_Group_Jenis = c.Id_Group_Jenis and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Flag_Finished_Good = 'Y' "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Stock_Owner = '" & lok_gudang & "'"
                        SQL = SQL & "order by a.Kode_Barang "
                        Using Ds2 = BindingTrans(SQL)
                            With Ds2.Tables("MyTable")
                                For i As Integer = 0 To .Rows.Count - 1
                                    DataGridView1.Rows.Add(1)
                                    DataGridView1.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Stock_Owner")
                                    DataGridView1.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Barang")
                                    DataGridView1.Rows(i).Cells(2).Value = .Rows(i).Item("Nama")
                                    DataGridView1.Rows(i).Cells(3).Value = .Rows(i).Item("Id_Cost_Center")
                                    DataGridView1.Rows(i).Cells(4).Value = .Rows(i).Item("Keterangan")
                                    DataGridView1.Rows(i).Cells(5).Value = ""
                                    DataGridView1.Rows(i).Cells(6).Value = ""
                                Next
                            End With
                        End Using

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
End Class
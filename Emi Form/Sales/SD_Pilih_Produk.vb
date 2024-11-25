Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class SD_Pilih_Produk
    Public filter_tambahan, filter_kdSupplier As String
    Public asal As String
    Dim arrcari As New ArrayList
    Dim Jenis = "Tampil_Barang"
    Public urutcmb As Integer

    Private Sub kosong()
        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            SQL = "select a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang "
            SQL = SQL & "from Barang a,EMI_Group_Jenis b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Flag_Raw_Material = 'Y' "
            SQL = SQL & "group by a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang order by a.Nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim LV As New ListViewItem
                    LV = Lv_Barang.Items.Add(dr("Kode_Barang"))
                    LV.SubItems.Add(dr("Nama"))
                    LV.SubItems.Add(dr("Id_Kategori_Gudang"))
                Loop
            End Using

            ComboBox1.Items.Clear()
            ComboBox2.Items.Clear()
            SQL = "select Kode_Kategori_Besar from Kategori_Besar where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Kategori_Besar "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Kode_Kategori_Besar"))
                Loop
            End Using
            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub SD_Pilih_Barang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub SD_Pilih_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            'Base_Language.Get_Languages_Global(Bahasa_Pilihan)
            'Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            'LblPilihBarang_Judul.Text = Base_Language.Lang_TampilBarang_Judul

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
        Lv_Barang.Visible = True
        kosong()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
        kosong()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        kosong()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            OpenConn()

            If ComboBox1.SelectedIndex = -1 Then
                ComboBox2.Items.Clear()
                Exit Sub
            End If

            ComboBox2.Items.Clear()
            SQL = "select Kode_Kategori_Kecil from Kategori_Kecil where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Kategori_Besar = '" & ComboBox1.Text & "' order by Kode_Kategori_Kecil "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("Kode_Kategori_Kecil"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fboleh As Boolean = False
        For a As Integer = 0 To Lv_Barang.Items.Count - 1
            If Lv_Barang.Items(a).Checked = True Then
                fboleh = True
                Exit For
            Else
                fboleh = False
            End If
        Next

        If fboleh = False Then
            MessageBox.Show("bahan belum di pilih.....!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        End If

        If EMI_Transaksi_MaterialRequisition.DataGridView1.Rows.Count <> 0 Then
            For a As Integer = 0 To Lv_Barang.Items.Count - 1
                If Lv_Barang.Items(a).Checked = True Then
                    For b As Integer = 0 To EMI_Transaksi_MaterialRequisition.DataGridView1.Rows.Count - 1
                        If Lv_Barang.Items(a).Text = EMI_Transaksi_MaterialRequisition.DataGridView1.Rows(b).Cells(1).Value.ToString Then
                            MessageBox.Show("bahan sudah di pilih sebelumnya.....!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Next
                End If
            Next
        End If


        Try
            OpenConn()


            EMI_Transaksi_MaterialRequisition.Arrbarang.Clear()
            EMI_Transaksi_MaterialRequisition.Arrlokasi.Clear()
            EMI_Transaksi_MaterialRequisition.ArrNama.Clear()

            For z As Integer = 0 To Lv_Barang.Items.Count - 1
                If Lv_Barang.Items(z).Checked = True Then

                    Dim fSO As String = ""
                    SQL = "select Top(1)a.Lokasi_Gudang from EMI_Kategori_Gudang_PerLokasi a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Kategori_Gudang = b.Id_Kategori_Gudang and "
                    SQL = SQL & "a.ID_Kategori_Gudang = '" & Lv_Barang.Items(z).SubItems(2).Text & "' and b.Kode_Barang = '" & Lv_Barang.Items(z).Text & "'"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For i As Integer = 0 To .Rows.Count - 1
                                    fSO = .Rows(i).Item("Lokasi_Gudang")
                                Next
                            End If
                        End With
                    End Using

                    EMI_Transaksi_MaterialRequisition.Arrbarang.Add(Lv_Barang.Items(z).SubItems(0).Text)
                    EMI_Transaksi_MaterialRequisition.Arrlokasi.Add(fSO)
                    EMI_Transaksi_MaterialRequisition.ArrNama.Add(Lv_Barang.Items(z).SubItems(1).Text)
                End If
            Next


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_Transaksi_MaterialRequisition.get_barang()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            SQL = "select a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang "
            SQL = SQL & "from Barang a,EMI_Group_Jenis b,EMI_Transaksi_Material_Requsition_Detail c  where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Flag_Raw_Material = 'Y' "
            SQL = SQL & "and a.Kode_Barang = c.Kode_Barang and c.Flag_Referensi = 'Y' "
            SQL = SQL & "group by a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang order by a.Nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim LV As New ListViewItem
                    LV = Lv_Barang.Items.Add(dr("Kode_Barang"))
                    LV.SubItems.Add(dr("Nama"))
                    LV.SubItems.Add(dr("Id_Kategori_Gudang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("kategori besar " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("kategori kecil " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        End If
        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            SQL = "select a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang "
            SQL = SQL & "from Barang a,EMI_Group_Jenis b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Flag_Raw_Material = 'Y' "
            SQL = SQL & "and a.Kode_Kategori_Besar = '" & ComboBox1.Text & "' and a.Kode_Kategori_Kecil = '" & ComboBox2.Text & "' "
            SQL = SQL & "group by a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang order by a.Nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim LV As New ListViewItem
                    LV = Lv_Barang.Items.Add(dr("Kode_Barang"))
                    LV.SubItems.Add(dr("Nama"))
                    LV.SubItems.Add(dr("Id_Kategori_Gudang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class
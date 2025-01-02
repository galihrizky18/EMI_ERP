Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class Display_Produk_Sampling
    Public filter_tambahan, filter_kdSupplier As String
    Public asal As String
    Dim arrcari As New ArrayList
    Dim Jenis = "Tampil_Barang"

    Private Sub kosong()
        Cmb_KategoriBesar.SelectedIndex = -1
        Cmb_KategoriKecil.SelectedIndex = -1
        Lv_Sampling.Items.Clear()
    End Sub

    Public Sub lokasi()
        Try
            OpenConn()

            'CmbPilihBarang_Lokasi.Items.Clear()

            SQL = "select Kode_Stock_Owner from view_lokasi_stock where Aktif='Y' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    '   CmbPilihBarang_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using

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

            Base_Language.Get_Languages_Global(Bahasa_Pilihan)

            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Cmb_KategoriBesar.Items.Add("Life Cat")
            Cmb_KategoriBesar.Items.Add("Life Dog")
            Cmb_KategoriKecil.Items.Add("Life Cat 400")
            Cmb_KategoriKecil.Items.Add("Life Dog 800")


            'LblPilihBarang_Judul.Text = Base_Language.Lang_TampilBarang_Judul
            LblPilihBarang_Judul.Text = "Display - Produk Sampling"

            kosong()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

        Lv_Sampling.Visible = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_KategoriBesar.SelectedIndex = -1 Then
            MessageBox.Show("Kategori besar harus diisi." & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_KategoriBesar.Focus() : Exit Sub
        ElseIf Cmb_KategoriKecil.SelectedIndex = -1 Then
            MessageBox.Show("Kategori kecil harus diisi." & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_KategoriKecil.Focus() : Exit Sub
        End If

        Dim lvw As ListViewItem
        lvw = Lv_Sampling.Items.Add("BRG001")
        lvw.SubItems.Add("Life Cat Tuna 80Gr")
        lvw.SubItems.Add("01 Jan 2024")
        lvw.SubItems.Add("1000")
        lvw.SubItems.Add("PCS")

        lvw = Lv_Sampling.Items.Add("BRG001")
        lvw.SubItems.Add("Life Cat Salmon 80Gr")
        lvw.SubItems.Add("01 Jan 2024")
        lvw.SubItems.Add("1000")
        lvw.SubItems.Add("PCS")

        lvw = Lv_Sampling.Items.Add("BRG001")
        lvw.SubItems.Add("Life Cat Chicken Tuna 80Gr")
        lvw.SubItems.Add("01 Jan 2024")
        lvw.SubItems.Add("1000")
        lvw.SubItems.Add("PCS")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
        kosong()
    End Sub

End Class
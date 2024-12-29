Public Class Master_Jenis_Biaya_Produksi

    Dim arrCari, arrKd_biaya, arrKeterangan As New ArrayList
    Dim Jenis = "Master_Jenis_Biaya_Produksi"

    Dim LvID, LvKd, LvKeterangan, LvSatuan As String
    Dim LvKdBrg_KodeBarang, LvKdBrg_NamaBarang, LvKdBrg_Satuan As String

    Dim itemID As Integer = 0
    Dim itemKode As Integer = 1
    Dim itemKeterangan As Integer = 2
    Dim itemSatuan As Integer = 3

    Dim itemKdBrg_KodeBarang As Integer = 0
    Dim itemKdBrg_NamaBarang As Integer = 1
    Dim itemKdBrg_Satuan As Integer = 2

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvID = Lv_Jenis_BP.Items(NoIndex).Text
        LvKd = Lv_Jenis_BP.Items(NoIndex).SubItems(itemKode).Text
        LvKeterangan = Lv_Jenis_BP.Items(NoIndex).SubItems(itemKeterangan).Text
        itemSatuan = Lv_Jenis_BP.Items(NoIndex).SubItems(itemSatuan).Text
    End Sub

    Private Sub Master_Biaya_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Biaya_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Lbl_Judul.Text = "Master Data - Jenis Biaya Produksi"

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Btn_Simpan.Tag = "&Simpan"
            Btn_Hapus.Enabled = False

            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            Lv_Jenis_BP.Columns.Clear()
            Lv_Jenis_BP.Columns.Add("ID", 0, HorizontalAlignment.Left)
            Lv_Jenis_BP.Columns.Add(Base_Language.Lang_Global_Kode, 150, HorizontalAlignment.Left)
            Lv_Jenis_BP.Columns.Add(Base_Language.lang_global_keterangan, 340, HorizontalAlignment.Left)
            Lv_Jenis_BP.Columns.Add(Base_Language.Lang_Global_Satuan, 150, HorizontalAlignment.Center)
            Lv_Jenis_BP.View = View.Details


            'Lv_KdBarang
            Lv_BarangPotStock.Columns.Clear()
            Lv_BarangPotStock.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
            Lv_BarangPotStock.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
            Lv_BarangPotStock.Columns.Add("Satuan", 150, HorizontalAlignment.Left)
            Lv_BarangPotStock.View = View.Details


            Txt_KdBarang.Enabled = False
            Txt_NamaBarang.Enabled = False

            kosong()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong()
        Lbl_IdJenisBP.Text = ""
        Txt_Kd.Text = ""
        Txt_Keterangan.Text = ""
        Cmb_Kolom.SelectedIndex = -1
        Txt_Value.Text = ""

        Cmb_Kolom.Items.Clear() : arrCari.Clear() : Cmb_Kolom.SelectedIndex = -1
        Cmb_Kolom.Items.Add(Base_Language.Lang_Global_Kode) : arrCari.Add("kode_jenis_biaya_produksi")
        Cmb_Kolom.Items.Add(Base_Language.lang_global_keterangan) : arrCari.Add("Keterangan")
        Cmb_Kolom.Items.Add(Base_Language.Lang_Global_Satuan) : arrCari.Add("satuan")

        Txt_Kd.Enabled = True
        Btn_Simpan.Tag = "&Simpan"
        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Enabled = False

        Try
            OpenConn()

            Cmbsatuan.Items.Clear()
            SQL = "select Satuan from EMI_Satuan where kode_perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & " order by Satuan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmbsatuan.Items.Add(dr("Satuan"))
                Loop
            End Using

            Lv_Jenis_BP.Items.Clear()
            SQL = "select  Kode_Jenis_Biaya_Produksi,keterangan,satuan,id_jenis_biaya_produksi  "
            SQL = SQL & "From Emi_Jenis_Biaya_Produksi where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "Order by id_jenis_biaya_produksi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Jenis_BP.Items.Add(Dr("id_jenis_biaya_produksi"))
                    lvw.SubItems.Add(Dr("Kode_Jenis_Biaya_Produksi"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                    lvw.SubItems.Add(Dr("satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_Data_LvKdBarang(ByVal index As Integer)

        LvKdBrg_KodeBarang = Lv_BarangPotStock.Items(index).SubItems(itemKdBrg_KodeBarang).Text
        LvKdBrg_NamaBarang = Lv_BarangPotStock.Items(index).SubItems(itemKdBrg_NamaBarang).Text
        LvKdBrg_Satuan = Lv_BarangPotStock.Items(index).SubItems(itemKdBrg_Satuan).Text

    End Sub


    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Kd.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Kode & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Kd.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Keterangan.Focus() : Exit Sub
        ElseIf Cmbsatuan.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmbsatuan.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag = "&Simpan" Then

                SQL = "Insert Into Emi_Jenis_Biaya_Produksi(Kode_Perusahaan, Kode_Jenis_Biaya_Produksi, Keterangan,satuan, Flag_Potong_Stock, Kode_Barang, Nama_Barang) "
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_Kd.Text.Trim & "',"
                SQL = SQL & " '" & Txt_Keterangan.Text.Trim & "', '" & Cmbsatuan.Text & "', "

                If Chk_PotongStock.Checked Then
                    SQL = SQL & "'Y', '" & Txt_KdBarang.Text & "', '" & Txt_NamaBarang.Text & "')"
                Else
                    SQL = SQL & "NULL, NULL, NULL)"
                End If
                ExecuteTrans(SQL)
            Else
                SQL = "Update Emi_Jenis_Biaya_Produksi Set Keterangan =  '" & Txt_Keterangan.Text.Trim & "', "
                SQL = SQL & "satuan = '" & Cmbsatuan.Text & "' "
                If Chk_PotongStock.Checked Then
                    SQL = SQL & ",Flag_Potong_Stock = 'Y', Kode_Barang = '" & Txt_KdBarang.Text & "', Nama_Barang = '" & Txt_NamaBarang.Text & "' "
                Else
                    SQL = SQL & ",Flag_Potong_Stock = NULL, Kode_Barang = NULL, Nama_Barang = NULL "
                End If
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and id_jenis_biaya_produksi = '" & Lbl_IdJenisBP.Text & "' "
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
        Txt_Kd.Focus()
    End Sub


    Private Sub Lv_MasterBiaya_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Jenis_BP.DoubleClick
        If Lv_Jenis_BP.Items.Count = 0 Then Exit Sub

        Lbl_IdJenisBP.Text = Lv_Jenis_BP.FocusedItem.Text
        Txt_Kd.Text = Lv_Jenis_BP.FocusedItem.SubItems(itemKode).Text
        Txt_Keterangan.Text = Lv_Jenis_BP.FocusedItem.SubItems(itemKeterangan).Text

        Txt_Kd_Leave(Lv_Jenis_BP, e)
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Cmb_Kolom.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Kolom & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_Kolom.Focus() : Exit Sub
        ElseIf Txt_Value.Text.Trim.Length = 0 Then
            MessageBox.Show("Value" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Value.Focus() : Exit Sub
        End If

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try

            OpenConn()

            Lv_Jenis_BP.Items.Clear()
            SQL = "select Kode_Jenis_Biaya_Produksi,keterangan,satuan,id_jenis_biaya_produksi  "
            SQL = SQL & "From Emi_Jenis_Biaya_Produksi where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Txt_Value.Text & "%' "
                SQL = SQL & "order by " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " "
            Else
                SQL = SQL & "order by id_jenis_biaya_produksi"
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Jenis_BP.Items.Add(Dr("id_jenis_biaya_produksi"))
                    lvw.SubItems.Add(Dr("Kode_Jenis_Biaya_Produksi"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                    lvw.SubItems.Add(Dr("satuan"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Chk_PotongStock_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_PotongStock.CheckedChanged

        If Chk_PotongStock.Checked Then
            Txt_KdBarang.Enabled = True
        Else
            Txt_KdBarang.Enabled = False
        End If

        Txt_KdBarang.Text = ""
        Txt_NamaBarang.Text = ""

    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged

        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_BarangPotStock.Items.Clear()
            Lv_BarangPotStock.Visible = False
            Lv_BarangPotStock.Location = New Point(695, 226)

            Txt_NamaBarang.Text = ""
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_BarangPotStock.Items.Clear()
            SQL = "select Kode_Barang, Nama, Satuan from barang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and (Kode_Barang LIKE '" & Txt_KdBarang.Text & "%' OR nama LIKE '" & Txt_KdBarang.Text & "%') "
            SQL = SQL & "group by Kode_Barang, Nama, Satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As New ListViewItem
                    Lv = Lv_BarangPotStock.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            Lv_BarangPotStock.Visible = True
            Lv_BarangPotStock.Location = New Point(141, 226)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_BarangPotStock_DoubleClick(sender As Object, e As EventArgs) Handles Lv_BarangPotStock.DoubleClick
        If Lv_BarangPotStock.Items.Count = 0 Then Exit Sub

        Get_Data_LvKdBarang(Lv_BarangPotStock.FocusedItem.Index)

        Txt_KdBarang.Text = LvKdBrg_KodeBarang
        Txt_NamaBarang.Text = LvKdBrg_NamaBarang

        Lv_BarangPotStock.Items.Clear()
        Lv_BarangPotStock.Location = New Point(695, 226)
        Lv_BarangPotStock.Visible = False

    End Sub




    Private Sub Txt_Kd_Leave(sender As Object, e As EventArgs) Handles Txt_Kd.Leave
        If Txt_Kd.Text.Trim.Length = 0 Then Exit Sub
        Lbl_IdJenisBP.Enabled = False
        Txt_Kd.Enabled = False

        Try
            OpenConn()

            SQL = "select  Kode_Jenis_Biaya_Produksi,keterangan,satuan,id_jenis_biaya_produksi, Flag_Potong_Stock, Kode_Barang, Nama_Barang "
            SQL = SQL & "from  Emi_Jenis_Biaya_Produksi where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Jenis_Biaya_Produksi = '" & Txt_Kd.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Lbl_IdJenisBP.Text = Dr("id_jenis_biaya_produksi")
                    Txt_Kd.Text = Dr("Kode_Jenis_Biaya_Produksi")
                    Txt_Keterangan.Text = Dr("keterangan")
                    Cmbsatuan.Text = Dr("satuan")
                    Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
                    Btn_Simpan.Tag = "&Update"

                    If General_Class.CekNULL(Dr("Flag_Potong_Stock")) = "Y" Then
                        Chk_PotongStock.Checked = True
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NamaBarang.Text = Dr("Nama_Barang")
                    Else
                        Chk_PotongStock.Checked = False
                        Txt_KdBarang.Text = ""
                        Txt_NamaBarang.Text = ""
                    End If

                Else
                    Lbl_IdJenisBP.Text = ""
                    Txt_Keterangan.Text = ""
                    Cmbsatuan.Text = ""
                    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
                    Txt_Kd.Enabled = True
                    Chk_PotongStock.Checked = False
                    Txt_KdBarang.Text = ""
                    Txt_NamaBarang.Text = ""
                    Btn_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus1 = vbYes Then

            Try

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Delete From Emi_Jenis_Biaya_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and id_jenis_biaya_produksi = '" & Lbl_IdJenisBP.Text.Trim & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong()
        Txt_Kd.Focus()
    End Sub



    Private Sub Txt_Kd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Cmbsatuan.Focus()
    End Sub

    Private Sub Txt_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub Cmbsatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmbsatuan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub
End Class

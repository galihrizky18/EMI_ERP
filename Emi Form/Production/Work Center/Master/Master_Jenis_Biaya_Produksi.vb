Public Class Master_Jenis_Biaya_Produksi
    Dim JudulForm As String = "Master Jenis Biaya Produksi"

    Dim arrCari, arrKd_biaya, arrKeterangan As New ArrayList
    Dim Jenis = "Master_Jenis_Biaya_Produksi"

    Dim asalLv As String = ""

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
            Txt_TarifPerSatuan.Text = ""

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

            'Lv_KodeAkunBiaya
            Lv_AkunBiaya.Columns.Clear()
            Lv_AkunBiaya.Columns.Add("Kode Akun", 150, HorizontalAlignment.Left)
            Lv_AkunBiaya.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
            Lv_AkunBiaya.View = View.Details
            Lv_AkunBiaya.Location = New Point(700, 246)
            Lv_AkunBiaya.Visible = False

            'Lv_KodeAkunBiaya
            Lv_AkunBudget.Columns.Clear()
            Lv_AkunBudget.Columns.Add("Kode Akun", 150, HorizontalAlignment.Left)
            Lv_AkunBudget.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
            Lv_AkunBudget.View = View.Details
            Lv_AkunBudget.Location = New Point(700, 286)
            Lv_AkunBudget.Visible = False



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
        Txt_KdBarang.Text = ""
        Txt_NamaBarang.Text = ""

        Txt_KdBiaya.Text = ""
        Txt_KetBiaya.Text = ""
        Txt_KdBudget.Text = ""
        Txt_KetBudget.Text = ""
        Txt_TarifPerSatuan.Text = ""

        Lv_AkunBiaya.Items.Clear() : Lv_AkunBudget.Items.Clear()

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
        ElseIf Txt_KetBiaya.Text.Trim.Length = 0 Then
            MessageBox.Show("Akun Biaya Harus Di Isi . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_KdBiaya.Focus() : Exit Sub
        ElseIf Txt_KetBudget.Text.Trim.Length = 0 Then
            MessageBox.Show("Akun Budget Harus Di Isi . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_KdBudget.Focus() : Exit Sub
        End If

        If Chk_PotongStock.Checked = True Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Barang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Txt_KdBarang.Focus() : Exit Sub
            End If
        End If
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag = "&Simpan" Then

                SQL = "Insert Into Emi_Jenis_Biaya_Produksi(Kode_Perusahaan, Kode_Jenis_Biaya_Produksi, Keterangan,satuan, Flag_Potong_Stock, Kode_Barang, Nama_Barang, Kode_Akun_Biaya, Kode_Akun_Budget, Tarif_Per_Satuan) "
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_Kd.Text.Trim & "',"
                SQL = SQL & " '" & Txt_Keterangan.Text.Trim & "', '" & Cmbsatuan.Text & "', "

                If Chk_PotongStock.Checked Then
                    SQL = SQL & "'Y', '" & Txt_KdBarang.Text & "', '" & Txt_NamaBarang.Text & "', '" & Txt_KdBiaya.Text & "', '" & Txt_KdBudget.Text & "', NULL)"
                Else
                    SQL = SQL & "NULL, NULL, NULL, '" & Txt_KdBiaya.Text & "', '" & Txt_KdBudget.Text & "', '" & HilangkanTanda(Txt_TarifPerSatuan.Text) & "')"
                End If
                ExecuteTrans(SQL)
            Else
                SQL = "Update Emi_Jenis_Biaya_Produksi Set Keterangan =  '" & Txt_Keterangan.Text.Trim & "', "
                SQL = SQL & "satuan = '" & Cmbsatuan.Text & "' "
                If Chk_PotongStock.Checked Then
                    SQL = SQL & ",Flag_Potong_Stock = 'Y', Kode_Barang = '" & Txt_KdBarang.Text & "', Nama_Barang = '" & Txt_NamaBarang.Text & "', "
                    SQL = SQL & "Kode_Akun_Biaya = '" & Txt_KdBiaya.Text & "', Kode_Akun_Budget = '" & Txt_KdBudget.Text & "', Tarif_Per_Satuan = NULL "
                Else
                    SQL = SQL & ",Flag_Potong_Stock = NULL, Kode_Barang = NULL, Nama_Barang = NULL, "
                    SQL = SQL & "Kode_Akun_Biaya = '" & Txt_KdBiaya.Text & "', Kode_Akun_Budget = '" & Txt_KdBudget.Text & "', Tarif_Per_Satuan = '" & HilangkanTanda(Txt_TarifPerSatuan.Text) & "' "
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
            Txt_TarifPerSatuan.Enabled = False
            Txt_TarifPerSatuan.Text = ""
        Else
            Txt_KdBarang.Enabled = False
            Txt_TarifPerSatuan.Enabled = True
            Txt_TarifPerSatuan.Text = ""
        End If

        Txt_KdBarang.Text = ""
        Txt_NamaBarang.Text = ""

    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged

        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_BarangPotStock.Items.Clear()
            Lv_BarangPotStock.Visible = False
            Lv_BarangPotStock.Location = New Point(695, 220)

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
            Lv_BarangPotStock.Location = New Point(141, 217)

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

            SQL = "select  Kode_Jenis_Biaya_Produksi,keterangan,satuan,id_jenis_biaya_produksi, Flag_Potong_Stock, Kode_Barang, Nama_Barang, Kode_Akun_Biaya, Kode_Akun_Budget, Tarif_Per_Satuan "
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

                    Txt_KdBiaya.Text = If(General_Class.CekNULL(Dr("Kode_Akun_Biaya")) = "", "", Dr("Kode_Akun_Biaya"))
                    Txt_KdBiaya_Leave(Txt_KdBiaya, e)

                    Txt_KdBudget.Text = If(General_Class.CekNULL(Dr("Kode_Akun_Budget")) = "", "", Dr("Kode_Akun_Budget"))
                    Txt_KdBudget_Leave(Txt_KdBudget, e)

                    Txt_TarifPerSatuan.Text = Format((If(General_Class.CekNULL(Dr("Tarif_Per_Satuan")) = "", 0, Dr("Tarif_Per_Satuan"))), "N2")

                    If General_Class.CekNULL(Dr("Flag_Potong_Stock")) = "Y" Then
                        Chk_PotongStock.Checked = True
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NamaBarang.Text = Dr("Nama_Barang")
                        Lv_BarangPotStock.Visible = False
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



    Private Sub Txt_KdBiaya_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBiaya.TextChanged
        If Txt_KdBiaya.Text.Trim.Length = 0 Then
            Lv_AkunBiaya.Visible = False
            Lv_AkunBiaya.Location = New Point(700, 246)
            Txt_KetBiaya.Text = ""
        Else
            Lv_AkunBiaya.Visible = True
            Lv_AkunBiaya.Location = New Point(141, 246)
        End If

        Try
            OpenConn()

            Lv_AkunBiaya.Items.Clear()
            SQL = "select top(75) Kode_Account as Kode_Account, Keterangan, Posisi from "
            SQL = SQL & "detail_account where kode_perusahaan = '" & KodePerusahaan & "' and right(kode_detail_acc,3) <> '000' and "
            SQL = SQL & "keterangan like '%" & Txt_KdBiaya.Text & "%' "
            SQL = SQL & "order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_AkunBiaya.Items.Add(Dr("Kode_Account"))
                    Lvw.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub



    Private Sub Txt_KdBiaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBiaya.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBiaya.Text.Trim.Length = 0 Then
                Lv_AkunBiaya.Visible = False : Txt_KetBiaya.Focus() : Exit Sub
            End If
            Txt_KdBiaya_Leave(Txt_KetBiaya, e)
        End If
    End Sub



    Private Sub Txt_KdBiaya_Leave(sender As Object, e As EventArgs) Handles Txt_KdBiaya.Leave
        If Txt_KdBiaya.Text.Trim.Length = 0 Then
            Lv_AkunBiaya.Visible = False : Exit Sub
        Else
            'Lv_AkunBiaya.Visible = True
        End If
        If Lv_AkunBiaya.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "select Kode_Account as Kode_Account, Keterangan, Posisi from "
            SQL = SQL & "detail_account where kode_perusahaan = '" & KodePerusahaan & "' and right(kode_detail_acc,3) <> '000' and "
            SQL = SQL & "Kode_Account = '" & Txt_KdBiaya.Text.Trim & "' "
            SQL = SQL & "order by keterangan"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_KdBiaya.Text = Dr("Kode_Account")
                    Txt_KetBiaya.Text = Dr("Keterangan")
                Else
                    Txt_KdBiaya.Text = ""
                    Txt_KetBiaya.Text = ""
                End If

                Lv_AkunBiaya.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Lv_AkunBiaya_DoubleClick(sender As Object, e As EventArgs) Handles Lv_AkunBiaya.DoubleClick
        If Lv_AkunBiaya.Items.Count = 0 Then Exit Sub
        Dim kode As String = Lv_AkunBiaya.FocusedItem.Text
        Dim nama As String = Lv_AkunBiaya.FocusedItem.SubItems(1).Text

        Txt_KdBiaya.Text = kode
        Txt_KetBiaya.Text = nama

        Lv_AkunBiaya.Location = New Point(700, 246)
        Txt_KdBudget.Focus()

    End Sub



    Private Sub Txt_KdBudget_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBudget.TextChanged
        If Txt_KdBudget.Text.Trim.Length = 0 Then
            Lv_AkunBudget.Visible = False
            Lv_AkunBudget.Location = New Point(700, 246)
            Txt_KetBudget.Text = ""
        Else
            Lv_AkunBudget.Visible = True
            Lv_AkunBudget.Location = New Point(140, 275)
        End If

        Try
            OpenConn()

            Lv_AkunBudget.Items.Clear()
            SQL = "select top(75) Kode_Account as Kode_Account, Keterangan, Posisi from "
            SQL = SQL & "detail_account where kode_perusahaan = '" & KodePerusahaan & "' and right(kode_detail_acc,3) <> '000' and "
            SQL = SQL & "keterangan like '%" & Txt_KdBudget.Text & "%' "
            SQL = SQL & "order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_AkunBudget.Items.Add(Dr("Kode_Account"))
                    Lvw.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Lv_AkunBudget_DoubleClick(sender As Object, e As EventArgs) Handles Lv_AkunBudget.DoubleClick
        If Lv_AkunBudget.Items.Count = 0 Then Exit Sub
        Dim kode As String = Lv_AkunBudget.FocusedItem.Text
        Dim nama As String = Lv_AkunBudget.FocusedItem.SubItems(1).Text

        Txt_KdBudget.Text = kode
        Txt_KetBudget.Text = nama

        Lv_AkunBudget.Location = New Point(700, 286)
        Txt_TarifPerSatuan.Focus()
    End Sub

    Private Sub Txt_KdBiaya_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBiaya.KeyDown
        If e.KeyCode = Keys.Down Then
            Lv_AkunBiaya.Focus()
        End If
    End Sub

    Private Sub Txt_KdBudget_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBudget.KeyDown
        If e.KeyCode = Keys.Down Then
            Lv_AkunBudget.Focus()
        End If
    End Sub


    Private Sub Txt_KdBudget_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBudget.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBudget.Text.Trim.Length = 0 Then
                Lv_AkunBudget.Visible = False : Txt_KetBudget.Focus() : Exit Sub
            End If
            Txt_KdBudget_Leave(Txt_KetBudget, e)
        End If
    End Sub

    Private Sub Txt_KdBudget_Leave(sender As Object, e As EventArgs) Handles Txt_KdBudget.Leave
        If Txt_KdBudget.Text.Trim.Length = 0 Then
            Lv_AkunBudget.Visible = False : Exit Sub
        Else
            Lv_AkunBudget.Visible = True
        End If
        If Lv_AkunBudget.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "select Kode_Account as Kode_Account, Keterangan, Posisi from "
            SQL = SQL & "detail_account where kode_perusahaan = '" & KodePerusahaan & "' and right(kode_detail_acc,3) <> '000' and "
            SQL = SQL & "Kode_Account = '" & Txt_KdBudget.Text.Trim & "' "
            SQL = SQL & "order by keterangan"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_KdBudget.Text = Dr("Kode_Account")
                    Txt_KetBudget.Text = Dr("Keterangan")
                Else
                    Txt_KdBudget.Text = ""
                    Txt_KetBudget.Text = ""
                End If

                Lv_AkunBudget.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_TarifPerSatuan_Leave(sender As Object, e As EventArgs) Handles Txt_TarifPerSatuan.Leave
        If Not IsNumeric(Txt_TarifPerSatuan.Text) Then
            Txt_TarifPerSatuan.Text = "" : Exit Sub
        End If

        Txt_TarifPerSatuan.Text = Format(Val(Txt_TarifPerSatuan.Text), "N2")
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

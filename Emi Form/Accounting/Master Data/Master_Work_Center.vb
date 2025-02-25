Public Class Master_Work_Center

    Dim arrCari, arrKd_biaya, arrKeterangan, arrId_Cost, arrMesin As New ArrayList
    Dim Jenis = "Master_Work_Center"

    Dim LvID, LvKd, LvKeterangan, LvCost, LvIDCost As String

    Dim itemID As Integer = 0
    Dim itemKode As Integer = 1
    Dim itemKeterangan As Integer = 2
    Dim itemcost As Integer = 3
    Dim itemIDcost As Integer = 4

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvID = Lv_WorkCenter.Items(NoIndex).Text
        LvKd = Lv_WorkCenter.Items(NoIndex).SubItems(itemKode).Text
        LvKeterangan = Lv_WorkCenter.Items(NoIndex).SubItems(itemKeterangan).Text
        LvCost = Lv_WorkCenter.Items(NoIndex).SubItems(itemcost).Text
        LvIDCost = Lv_WorkCenter.Items(NoIndex).SubItems(itemIDcost).Text
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

            Lbl_Judul.Text = Base_Language.Lang_Master_Work_Center_Judul

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Btn_Simpan.Tag = "&Simpan"
            Btn_Hapus.Enabled = False

            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            Lv_WorkCenter.Columns.Clear()
            Lv_WorkCenter.Columns.Add("ID", 0, HorizontalAlignment.Left)
            Lv_WorkCenter.Columns.Add(Base_Language.Lang_Global_Kode, 200, HorizontalAlignment.Left)
            Lv_WorkCenter.Columns.Add(Base_Language.lang_global_keterangan, 250, HorizontalAlignment.Left)
            Lv_WorkCenter.Columns.Add("Cost Center", 200, HorizontalAlignment.Left)
            Lv_WorkCenter.Columns.Add("Id Cost Center", 0, HorizontalAlignment.Left)
            Lv_WorkCenter.Columns.Add("Mesin", 200, HorizontalAlignment.Left)
            Lv_WorkCenter.View = View.Details

            kosong()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong()
        Lbl_IDWorkCenter.Text = ""
        Txt_Kd.Text = ""
        Txt_Keterangan.Text = ""
        Cmb_Kolom.SelectedIndex = -1
        Txt_Value.Text = ""

        Cmb_Kolom.Items.Clear() : arrCari.Clear() : Cmb_Kolom.SelectedIndex = -1
        Cmb_Kolom.Items.Add(Base_Language.Lang_Global_Kode) : arrCari.Add("Kode_Work_Center")
        Cmb_Kolom.Items.Add("Keterangan Work Center") : arrCari.Add("Work_Center")
        Cmb_Kolom.Items.Add("Keterangan Cost Center") : arrCari.Add("Cost_Center")
        Cmb_Kolom.Items.Add("Mesin") : arrCari.Add("Mesin")

        Txt_Kd.Enabled = True
        Btn_Simpan.Tag = "&Simpan"
        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Enabled = False

        Chk_Mesin.Checked = False
        Cmb_Mesin.Items.Clear() : Cmb_Mesin.Text = "" : Cmb_Mesin.Enabled = False

        Try
            OpenConn()

            ComboBox1.Items.Clear() : arrId_Cost.Clear() : ComboBox1.SelectedIndex = -1
            SQL = "select Id_Cost_Center,Keterangan,Kode_Cost_Center from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox1.Items.Add(Dr("Keterangan"))
                    arrId_Cost.Add(Dr("Id_Cost_Center"))
                Loop
            End Using

            Lv_WorkCenter.Items.Clear()
            SQL = ";with cte as( "
            SQL = SQL & "Select a.Kode_Perusahaan, a.Id_Work_Center, a.Kode_Work_Center, a.Keterangan as Work_Center, a.Id_Cost_Center, b.Keterangan as Cost_Center, "
            SQL = SQL & "ISNULL(( select z.nama_mesin from EMI_Master_Mesin z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.id_master_mesin = z.id_master_mesin "
            SQL = SQL & "), '-') as Mesin "
            SQL = SQL & "From EMI_Master_Work_Center a,EMI_Master_Cost_Center b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan  "
            SQL = SQL & "and a.Id_Cost_Center = b.Id_Cost_Center  "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' ) "
            SQL = SQL & "select * from cte where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "Order by Id_Work_Center, Id_Cost_Center "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_WorkCenter.Items.Add(Dr("Id_Work_Center"))
                    lvw.SubItems.Add(Dr("Kode_Work_Center"))
                    lvw.SubItems.Add(Dr("Work_Center"))
                    lvw.SubItems.Add(Dr("Cost_Center"))
                    lvw.SubItems.Add(Dr("Id_Cost_Center"))
                    lvw.SubItems.Add(Dr("Mesin"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

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
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim IdMasterMesin As Integer = 0

            If Cmb_Mesin.SelectedIndex <> -1 Or Chk_Mesin.Checked Then

                IdMasterMesin = Val(HilangkanTanda(arrMesin(Cmb_Mesin.SelectedIndex)))

            End If

            If Btn_Simpan.Tag = "&Simpan" Then
                SQL = "Insert Into emi_master_work_center(Kode_Perusahaan, Kode_Work_Center, Keterangan, Id_Cost_Center, id_Master_Mesin) "
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_Kd.Text.Trim & "',"
                SQL = SQL & " '" & Txt_Keterangan.Text.Trim & "', '" & arrId_Cost.Item(ComboBox1.SelectedIndex) & "', " & If(IdMasterMesin = 0, "NULL", IdMasterMesin) & " )"
                ExecuteTrans(SQL)
            Else
                SQL = "Update emi_master_work_center Set Keterangan =  '" & Txt_Keterangan.Text.Trim & "', "
                SQL = SQL & "Id_Cost_Center = '" & arrId_Cost.Item(ComboBox1.SelectedIndex) & "', "
                SQL = SQL & "id_Master_Mesin = " & If(IdMasterMesin = 0, "NULL", IdMasterMesin) & " "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Work_Center = '" & Lbl_IDWorkCenter.Text & "' "
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

    Private Sub Lv_MasterBiaya_DoubleClick(sender As Object, e As EventArgs) Handles Lv_WorkCenter.DoubleClick
        If Lv_WorkCenter.Items.Count = 0 Then Exit Sub

        Lbl_IDWorkCenter.Text = Lv_WorkCenter.FocusedItem.Text
        Txt_Kd.Text = Lv_WorkCenter.FocusedItem.SubItems(itemKode).Text
        Txt_Keterangan.Text = Lv_WorkCenter.FocusedItem.SubItems(itemKeterangan).Text

        Txt_Kd_Leave(Lv_WorkCenter, e)
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

            Lv_WorkCenter.Items.Clear()
            SQL = ";with cte as( "
            SQL = SQL & "Select a.Kode_Perusahaan, a.Id_Work_Center, a.Kode_Work_Center, a.Keterangan as Work_Center, a.Id_Cost_Center, b.Keterangan as Cost_Center, "
            SQL = SQL & "ISNULL(( select z.nama_mesin from EMI_Master_Mesin z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.id_master_mesin = z.id_master_mesin "
            SQL = SQL & "), '-') as Mesin "
            SQL = SQL & "From EMI_Master_Work_Center a,EMI_Master_Cost_Center b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan  "
            SQL = SQL & "and a.Id_Cost_Center = b.Id_Cost_Center  "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            SQL = SQL & "select * from cte where Kode_Perusahaan = '" & KodePerusahaan & "' "

            If semua = "T" Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Trim(Txt_Value.Text) & "%' "

            End If

            SQL = SQL & "Order by Id_Work_Center, Id_Cost_Center "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_WorkCenter.Items.Add(Dr("Id_Work_Center"))
                    lvw.SubItems.Add(Dr("Kode_Work_Center"))
                    lvw.SubItems.Add(Dr("Work_Center"))
                    lvw.SubItems.Add(Dr("Cost_Center"))
                    lvw.SubItems.Add(Dr("Id_Cost_Center"))
                    lvw.SubItems.Add(Dr("Mesin"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Leave(sender As Object, e As EventArgs) Handles Txt_Kd.Leave
        If Txt_Kd.Text.Trim.Length = 0 Then Exit Sub
        Lbl_IDWorkCenter.Enabled = False
        Txt_Kd.Enabled = False

        Try
            OpenConn()

            SQL = "Select a.Id_Work_Center, a.Kode_Work_Center, a.Keterangan as Work_Center, a.Id_Cost_Center, b.Keterangan as Cost_Center, a.id_master_mesin,  "
            SQL = SQL & "ISNULL(( select z.nama_mesin from EMI_Master_Mesin z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.id_master_mesin = z.id_master_mesin ), '-') as Mesin "
            SQL = SQL & "From EMI_Master_Work_Center a,EMI_Master_Cost_Center b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Kode_Work_Center = '" & Txt_Kd.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Lbl_IDWorkCenter.Text = Dr("Id_Work_Center")
                    Txt_Kd.Text = Dr("Kode_Work_Center")
                    Txt_Keterangan.Text = Dr("Work_Center")
                    For a As Integer = 0 To arrId_Cost.Count - 1
                        If arrId_Cost.Item(a) = Dr("Id_Cost_Center") Then
                            ComboBox1.SelectedIndex = a
                        End If
                    Next


                    If Not General_Class.CekNULL(Dr("id_master_mesin")) = "" Then
                        Chk_Mesin.Checked = True

                        Cmb_Mesin.SelectedIndex = arrMesin.IndexOf(Dr("id_master_mesin"))
                    Else
                        Chk_Mesin.Checked = False

                    End If

                    Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
                    Btn_Simpan.Tag = "&Update"
                Else
                    Lbl_IDWorkCenter.Text = ""
                    Txt_Keterangan.Text = ""
                    ComboBox1.SelectedIndex = -1
                    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
                    Txt_Kd.Enabled = True
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

    Private Sub Txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles Txt_Keterangan.TextChanged

    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus1 = vbYes Then

            Try

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Delete From emi_master_work_center where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Work_Center = '" & Lbl_IDWorkCenter.Text.Trim & "' "
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

    Private Sub Txt_Value_TextChanged(sender As Object, e As EventArgs) Handles Txt_Value.TextChanged

    End Sub

    Private Sub Txt_Kd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Txt_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub Chk_Mesin_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Mesin.CheckedChanged

        If Not Chk_Mesin.Checked Then Cmb_Mesin.Text = "" : Cmb_Mesin.Items.Clear() : Cmb_Mesin.Enabled = False : arrMesin.Clear() : Cmb_Mesin.SelectedIndex = -1 : Exit Sub


        Try
            OpenConn()

            arrMesin.Clear() : Cmb_Mesin.Enabled = True
            SQL = "select id_master_mesin, id_divisi_mesin, Divisi_Mesin, Nama_Mesin "
            SQL = SQL & "from EMI_Master_Mesin "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by id_master_mesin, id_divisi_mesin , Nama_Mesin "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Mesin.Items.Add(Dr("Nama_Mesin")) : arrMesin.Add(Dr("id_master_mesin"))
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
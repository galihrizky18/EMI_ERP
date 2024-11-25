Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Master_Cost_Center

    Dim arrDepartement, arrCari As New ArrayList


    Private Sub Master_Cost_Center_AutoValidateChanged(sender As Object, e As EventArgs) Handles Me.AutoValidateChanged
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Cost_Center_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Initial_CostCenter_Lv()
        kosong()

    End Sub
    Private Sub kosong()

        Txt_Kd.Text = String.Empty
        Txt_Keterangan.Text = String.Empty

        Btn_Hapus.Enabled = False
        Btn_Simpan.Text = "&Simpan"
        Chk_Departement.Checked = False
        Cmb_Departement.Items.Clear()

        Btn_Simpan.Tag = "SIMPAN"
        Kd_Cost_Center_Sementara.Text = String.Empty

        Lv_CostCenter.Items.Clear()

        Try
            OpenConn()

            Lv_CostCenter.Items.Clear()
            SQL = "Select Id_Work_Center, Kode_Work_Center, Keterangan "
            SQL = SQL & "From emi_master_work_center where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "Order by Id_Work_Center "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_CostCenter.Items.Add(Dr("Id_Work_Center"))
                    lvw.SubItems.Add(Dr("Kode_Work_Center"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Load_Lv()

        Cmb_Kolom.Items.Clear()
        Cmb_Kolom.Items.Add("Kode Cost Center") : arrCari.Add("a.kode_cost_center")
        Cmb_Kolom.Items.Add("Keterangan") : arrCari.Add("a.keterangan")

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Initial_CostCenter_Lv()

        Lv_CostCenter.Columns.Add("ID", 80, HorizontalAlignment.Center)
        Lv_CostCenter.Columns.Add("Kode Cost Center ", 150, HorizontalAlignment.Left)
        Lv_CostCenter.Columns.Add("Keterangan ", 230, HorizontalAlignment.Left)
        Lv_CostCenter.Columns.Add("Divisi", 250, HorizontalAlignment.Left)
        Lv_CostCenter.Columns.Add("Sub Divisi", 0, HorizontalAlignment.Left)
        Lv_CostCenter.View = View.Details

    End Sub

    Private Sub Load_Lv()

        Try
            OpenConn()

            Lv_CostCenter.Items.Clear()
            SQL = "select a.Id_Cost_Center, a.Kode_Cost_Center, a.Keterangan as cost_center, b.keterangan as divisi, c.Keterangan as sub_divisi "
            SQL = SQL & "from EMI_Master_Cost_Center a "
            SQL = SQL & "left join HRIS_Divisi b on a.id_divisi = b.ID_Divisi and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "left join HRIS_Sub_Divisi c on a.id_sub_divisi = c.ID_Sub_Divisi and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "order by a.Kode_Cost_Center desc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_CostCenter.Items.Add(Dr("Id_Cost_Center"))
                    lv.SubItems.Add(Dr("Kode_Cost_Center"))
                    lv.SubItems.Add(Dr("cost_center"))
                    lv.SubItems.Add(General_Class.CekNULL(Dr("divisi")))
                    lv.SubItems.Add(General_Class.CekNULL(Dr("sub_divisi")))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Chk_Departement_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Departement.CheckedChanged
        If Chk_Departement.Checked Then
            Cmb_Departement.Enabled = True

            Try
                OpenConn()

                Cmb_Departement.Items.Clear()
                SQL = "select ID_Divisi, Keterangan from HRIS_Divisi where Kode_Perusahaan = '" & KodePerusahaan & "'"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Cmb_Departement.Items.Add(Dr("Keterangan")) : arrDepartement.Add(Dr("ID_Divisi"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            Cmb_Departement.Enabled = False
            Cmb_Departement.Items.Clear()
        End If
    End Sub
    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click

        If Txt_Kd.Text = "" Then Exit Sub

        Try
            OpenConn()

            SQL = "delete from  EMI_Master_Cost_Center where Kode_Cost_Center='" & Txt_Kd.Text & "'"
            ExecuteTrans(SQL)

            CloseConn()
            MessageBox.Show("Berhasil Di Hapus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
            Load_Lv()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Kd.Text = "" Then
            MessageBox.Show("Kode Tidak Boleh Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Txt_Keterangan.Text = "" Then
            MessageBox.Show("Keterangan Tidak Boleh Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Chk_Departement.Checked = True Then
            If Cmb_Departement.SelectedIndex = -1 Then
                MessageBox.Show("Departement Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If


        Try
            OpenConn()

            If Btn_Simpan.Tag = "SIMPAN" Then

                '=====================================
                '=     CEK APAKAH KDOE SUDAH ADA     =
                '=====================================
                SQL = "select Kode_Cost_Center from EMI_Master_Cost_Center where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Cost_Center ='" & Txt_Kd.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        CloseConn()
                        MessageBox.Show("Kode Sudah Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                SQL = "insert into EMI_Master_Cost_Center (Kode_Perusahaan, Kode_Cost_Center, Keterangan, id_Divisi) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_Kd.Text & "', '" & Txt_Keterangan.Text & "', "

                If Chk_Departement.Checked = True Then
                    SQL = SQL & " '" & arrDepartement(Cmb_Departement.SelectedIndex) & "' "
                Else
                    SQL = SQL & "NULL "
                End If
                SQL = SQL & ")"
                ExecuteTrans(SQL)

            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                SQL = "update EMI_Master_Cost_Center set Kode_Cost_Center = '" & Txt_Kd.Text & "', "
                SQL = SQL & "Keterangan = '" & Txt_Keterangan.Text & "' "
                If Chk_Departement.Checked = True Then
                    SQL = SQL & ", id_divisi = '" & arrDepartement(Cmb_Departement.SelectedIndex) & "' "
                Else
                    SQL = SQL & ", id_divisi = NULL "
                End If
                SQL = SQL & "where Kode_Cost_Center = '" & Kd_Cost_Center_Sementara.Text & "' "
                ExecuteTrans(SQL)

            End If



            CloseConn()
            MessageBox.Show("Berhasil Di Simpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
            Load_Lv()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Lv_CostCenter_DoubleClick(sender As Object, e As EventArgs) Handles Lv_CostCenter.DoubleClick

        If Lv_CostCenter.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "select a.Id_Cost_Center, a.Kode_Cost_Center, a.Keterangan as cost_center, b.keterangan as divisi, c.Keterangan as sub_divisi "
            SQL = SQL & "from EMI_Master_Cost_Center a "
            SQL = SQL & "left join HRIS_Divisi b on a.id_divisi = b.ID_Divisi and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "left join HRIS_Sub_Divisi c on a.id_sub_divisi = c.ID_Sub_Divisi and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Cost_Center = '" & Lv_CostCenter.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Txt_Kd.Text = Dr("Kode_Cost_Center")
                    Txt_Keterangan.Text = Dr("cost_center")

                    If Not General_Class.CekNULL(Dr("divisi")) = "" Then
                        Chk_Departement.Checked = True
                        Chk_Departement_CheckedChanged(sender, e)
                        Cmb_Departement.SelectedItem = Dr("divisi")
                    End If

                    Btn_Simpan.Tag = "UPDATE"
                    Btn_Simpan.Text = "&Update"
                    Kd_Cost_Center_Sementara.Text = Dr("Kode_Cost_Center")
                    Btn_Hapus.Enabled = True

                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub



    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Kolom.SelectedIndex = -1 Then
            MessageBox.Show("Pilih jenis filter dahulu", Judul)
            Exit Sub
        ElseIf Txt_Value.Text.Trim.Length = 0 Then
            MessageBox.Show("Filter Value Tidak Boleh Kosong", Judul)
            Load_Lv()
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_CostCenter.Items.Clear()
            SQL = "select a.Id_Cost_Center, a.Kode_Cost_Center, a.Keterangan as cost_center, b.keterangan as divisi, c.Keterangan as sub_divisi "
            SQL = SQL & "from EMI_Master_Cost_Center a "
            SQL = SQL & "left join HRIS_Divisi b on a.id_divisi = b.ID_Divisi and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "left join HRIS_Sub_Divisi c on a.id_sub_divisi = c.ID_Sub_Divisi and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' "

            If Cmb_Kolom.SelectedIndex <> -1 Then

                SQL = SQL & "and " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Trim(Txt_Value.Text) & "%' "

            End If

            SQL = SQL & "order by a.Kode_Cost_Center desc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_CostCenter.Items.Add(Dr("Id_Cost_Center"))
                    lv.SubItems.Add(Dr("Kode_Cost_Center"))
                    lv.SubItems.Add(Dr("cost_center"))
                    lv.SubItems.Add(Dr("divisi"))
                    lv.SubItems.Add(Dr("sub_divisi"))
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
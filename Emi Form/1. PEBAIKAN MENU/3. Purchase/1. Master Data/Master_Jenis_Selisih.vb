Public Class Master_Jenis_Selisih

    Dim arrCari, arrKd_biaya, arrKeterangan As New ArrayList
    Dim Jenis = "Master_Biaya"
    Dim flagTxtLeaveOto As Boolean = True
    Dim flagBlhUpdate As Boolean = False

    Private Sub Master_Biaya_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Biaya_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            '  Lbl_Judul.Text = Base_Language.Lang_MasterBiaya_Judul
            Lbl_Judul.Text = "Master Data - Jenis Selisih"

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Btn_Simpan.Tag = "&Simpan"
            Btn_Hapus.Enabled = False

            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            Lv_MasterBiaya.Columns.Clear()
            Lv_MasterBiaya.Columns.Add("ID", 0, HorizontalAlignment.Left)
            Lv_MasterBiaya.Columns.Add(Base_Language.Lang_Global_Kode, 150, HorizontalAlignment.Left)
            Lv_MasterBiaya.Columns.Add(Base_Language.lang_global_keterangan, 150, HorizontalAlignment.Left)
            Lv_MasterBiaya.Columns.Add("Akun Bahan", 150, HorizontalAlignment.Left)
            Lv_MasterBiaya.Columns.Add("Akun Perjalanan", 150, HorizontalAlignment.Left)

            Lv_MasterBiaya.View = View.Details

            kosong()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub kosong()
        ChkMasukHutang.Checked = False
        flagBlhUpdate = False
        Txt_Kd.Text = ""
        Txt_Keterangan.Text = ""
        Cmb_Kolom.SelectedIndex = -1
        Txt_Value.Text = ""

        TxtKodeAkunPerjalanan.Text = ""
        TxtKodeAkunBhn.Text = ""

        TxtNamaAkunBahan.Text = ""
        TxtNamaAkunPerjalanan.Text = ""

        idJenisSelisih.Text = ""

        Cmb_Kolom.Items.Clear() : arrCari.Clear() : Cmb_Kolom.SelectedIndex = -1
        Cmb_Kolom.Items.Add(Base_Language.Lang_Global_Kode) : arrCari.Add("kode_jenis_selisih")
        Cmb_Kolom.Items.Add(Base_Language.lang_global_keterangan) : arrCari.Add("Keterangan")
        Cmb_Kolom.Items.Add("Akun Bahan") : arrCari.Add("akun_bahan")
        Cmb_Kolom.Items.Add("Akun Perjalanan") : arrCari.Add("akun_perjalanan")

        Txt_Kd.Enabled = True
        Btn_Simpan.Text = "Simpan"
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

        Try
            OpenConn()

            Lv_MasterBiaya.Items.Clear()
            SQL = "Select Id_Jenis_Selisih,Kode_Jenis_Selisih,Keterangan,a.Akun_Bahan As Kode_Akun_Bahan, "
            SQL = SQL & "isnull((Select x.Keterangan from Detail_Account x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "And a.Akun_Bahan = x.kode_master_acc + x.kode_acc + x.kode_detail_acc),null) Akun_Bahan, "
            ' SQL = SQL & " "
            SQL = SQL & "isnull((Select x.Keterangan from Detail_Account x where a.Kode_Perusahaan = x.Kode_Perusahaan  "
            SQL = SQL & "And a.Akun_Perjalanan = x.kode_master_acc + x.kode_acc + x.kode_detail_acc),null) Akun_Perjalanan "
            SQL = SQL & " from EMI_Master_Jenis_Selisih a where "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "Order by a.Id_Jenis_Selisih "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_MasterBiaya.Items.Add(Dr("Id_Jenis_Selisih"))
                    lvw.SubItems.Add(Dr("Kode_Jenis_Selisih"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                    lvw.SubItems.Add(Dr("akun_bahan"))
                    lvw.SubItems.Add(Dr("akun_perjalanan"))
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
        ElseIf TxtKodeAkunBhn.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Akun Bahan harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKodeAkunBhn.Focus() : Exit Sub
        ElseIf TxtKodeAkunBhn.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Akun Perjalanan harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKodeAkunPerjalanan.Focus() : Exit Sub

        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            Dim masukhutangbahan As String = ""
            If ChkMasukHutang.Checked = True Then
                masukhutangbahan = "Y"
            Else
                masukhutangbahan = "T"
            End If

            If Btn_Simpan.Tag = "&Simpan" Then
                SQL = "Insert Into EMI_Master_Jenis_Selisih(Kode_Perusahaan, kode_jenis_selisih, Keterangan,Akun_Bahan,Akun_Perjalanan, Flag_Masuk_Hutang_Bahan) "
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_Kd.Text.Trim & "',"
                SQL = SQL & "'" & Txt_Keterangan.Text.Trim & "', "
                SQL = SQL & "'" & TxtKodeAkunBhn.Text.Trim & "', "
                SQL = SQL & "'" & TxtKodeAkunPerjalanan.Text.Trim & "', '" & masukhutangbahan & "') "

                ExecuteTrans(SQL)
            Else
                SQL = "Update EMI_Master_Jenis_Selisih Set Keterangan =  '" & Txt_Keterangan.Text.Trim & "', "
                SQL = SQL & "akun_bahan = '" & TxtKodeAkunBhn.Text.Trim & "' , "
                SQL = SQL & "akun_perjalanan = '" & TxtKodeAkunPerjalanan.Text.Trim & "',  "
                SQL = SQL & "Flag_Masuk_Hutang_Bahan = '" & masukhutangbahan & "'  "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and  "
                SQL = SQL & "id_jenis_selisih = '" & idJenisSelisih.Text & "'"
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

    Private Sub Lv_MasterBiaya_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_MasterBiaya.SelectedIndexChanged

    End Sub

    Private Sub Lv_MasterBiaya_DoubleClick(sender As Object, e As EventArgs) Handles Lv_MasterBiaya.DoubleClick
        Txt_Kd.Text = Lv_MasterBiaya.FocusedItem.SubItems(1).Text
        flagBlhUpdate = True
        Txt_Kd_Leave(Lv_MasterBiaya, e)
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

            Lv_MasterBiaya.Items.Clear()
            SQL = ";with cte as ( "
            SQL = SQL & "select kode_perusahaan, Id_Jenis_Selisih,Kode_Jenis_Selisih,Keterangan,a.Akun_Bahan as Kode_Akun_Bahan, "
            SQL = SQL & "isnull((select x.Keterangan from Detail_Account x where a.Kode_Perusahaan = x.Kode_Perusahaan  "
            ' SQL = SQL & " "
            SQL = SQL & "and a.Akun_Bahan = x.kode_master_acc + x.kode_acc + x.kode_detail_acc),null) Akun_Bahan,  "
            SQL = SQL & "a.Akun_Perjalanan as kode_perjalanan, "
            SQL = SQL & "isnull((select x.Keterangan from Detail_Account x where a.Kode_Perusahaan = x.Kode_Perusahaan  "
            SQL = SQL & "and a.Akun_Perjalanan = x.kode_master_acc + x.kode_acc + x.kode_detail_acc),null) Akun_Perjalanan "
            SQL = SQL & "from EMI_Master_Jenis_Selisih a "
            SQL = SQL & ")select * From cte where "

            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "

            If semua = "T" Then
                SQL = SQL & "and " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Txt_Value.Text & "%' "
                SQL = SQL & "order by " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " "
            Else
                SQL = SQL & "order by id_jenis_selisih"
            End If

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_MasterBiaya.Items.Add(Dr("Id_Jenis_Selisih"))
                    lvw.SubItems.Add(Dr("Kode_Jenis_Selisih"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                    lvw.SubItems.Add(Dr("akun_bahan"))
                    lvw.SubItems.Add(Dr("akun_perjalanan"))
                Loop
            End Using

            'Lv_MasterBiaya.Items.Clear()
            'SQL = "select id_jenis_selisih, kode_jenis_selisih, Keterangan "
            'SQL = SQL & "From emi_master_jenis_selisih where "
            'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            'If semua = "T" Then
            '    SQL = SQL & "and " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Txt_Value.Text & "%' "
            '    SQL = SQL & "order by " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " "
            'Else
            '    SQL = SQL & "order by id_jenis_selisih"
            'End If
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim lvw As ListViewItem
            '        lvw = Lv_MasterBiaya.Items.Add(Dr("id_jenis_selisih"))
            '        lvw.SubItems.Add(Dr("kode_jenis_selisih"))
            '        lvw.SubItems.Add(Dr("Keterangan"))
            '    Loop
            'End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd.TextChanged

    End Sub

    Private Sub Txt_Kd_Leave(sender As Object, e As EventArgs) Handles Txt_Kd.Leave
        If Txt_Kd.Text.Trim.Length = 0 Then Exit Sub

        Txt_Kd.Enabled = False

        If flagBlhUpdate = False Then Exit Sub

        Try
            OpenConn()

            SQL = "select Id_Jenis_Selisih,Kode_Jenis_Selisih,Keterangan,Akun_Bahan,Akun_Perjalanan, "
            SQL = SQL & "isnull(Flag_Masuk_Hutang_Bahan,'T') as Flag_Masuk_Hutang_Bahan from EMI_Master_Jenis_Selisih   "

            SQL = SQL & "where kode_jenis_selisih = '" & Txt_Kd.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    idJenisSelisih.Text = Dr("id_jenis_selisih")
                    Txt_Kd.Text = Dr("Kode_Jenis_Selisih")
                    Txt_Keterangan.Text = Dr("Keterangan")
                    TxtKodeAkunBhn.Text = Dr("Akun_Bahan")
                    TxtKodeAkunBhn_Leave(Me, Nothing)
                    TxtKodeAkunPerjalanan.Text = Dr("akun_perjalanan")
                    TxtKodeAkunPerjalanan_Leave(Me, Nothing)

                    If Dr("Flag_Masuk_Hutang_Bahan") = "Y" Then
                        ChkMasukHutang.Checked = True
                    Else
                        ChkMasukHutang.Checked = False
                    End If

                    Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
                    Btn_Simpan.Tag = "&Update"

                Else
                    ChkMasukHutang.Checked = False
                    Txt_Keterangan.Text = ""
                    TxtKodeAkunBhn.Text = ""
                    TxtNamaAkunPerjalanan.Text = ""
                    TxtNamaAkunBahan.Text = ""
                    TxtKodeAkunPerjalanan.Text = ""
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

                SQL = "Delete From EMI_Biaya where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Biaya = '' "
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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TxtKodeAkunBhn.TextChanged
        If TxtKodeAkunBhn.Text.Trim.Length = 0 Then
            LvAkunBahan.Items.Clear()
            LvAkunBahan.Visible = False
            LvAkunBahan.Location = New Point(695, 220)

            TxtNamaAkunBahan.Text = ""
            Exit Sub
        End If

        If TxtKodeAkunBhn.Text.Length >= 1 Then
            Try
                OpenConn()
                LvAkunBahan.Items.Clear()

                Using Dr = OpenTrans("select kode_master_acc + kode_acc + kode_detail_acc as kode_akun, keterangan from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc + kode_acc + kode_detail_acc like '%" & TxtKodeAkunBhn.Text & "%'")

                    Do While Dr.Read
                        Dim Lv As New ListViewItem
                        Lv = LvAkunBahan.Items.Add(Dr("kode_akun"))
                        Lv.SubItems.Add(Dr("keterangan"))

                    Loop

                End Using

                LvAkunBahan.Visible = True
                LvAkunBahan.Location = New Point(211, 169)

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub TxtKodeAkunBhn_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKodeAkunBhn.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvAkunBahan.Items.Count = 0 Then Exit Sub
            LvAkunBahan.Focus()
        ElseIf e.KeyCode = Keys.Enter Then
            TxtKodeAkunBhn.Focus()
        End If
    End Sub

    Private Sub TxtKodeAkunBhn_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKodeAkunBhn.KeyPress
        If e.KeyChar = Chr(13) Then TxtKodeAkunBhn.Focus()
    End Sub

    Private Sub TxtKodeAkunBhn_Leave(sender As Object, e As EventArgs) Handles TxtKodeAkunBhn.Leave
        If TxtKodeAkunBhn.Text.Trim.Length = 0 Then Exit Sub
        If flagTxtLeaveOto = False Then Exit Sub
        If LvAkunBahan.Focused = True Then Exit Sub

        Try
            OpenConn()

            Using Dr = OpenTrans("select * from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc + kode_acc + kode_detail_acc = '" & TxtKodeAkunBhn.Text & "'")
                If Dr.Read Then
                    TxtNamaAkunBahan.Text = Dr("keterangan")
                    LvAkunBahan.Visible = False

                    TxtKodeAkunPerjalanan.Focus()
                Else
                    ' MessageBox.Show("Kode account tidak ditemukan . . ", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TxtKodeAkunBhn.Text = "" : TxtNamaAkunBahan.Text = ""
                    TxtKodeAkunBhn.Focus()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvAkunBahan_DoubleClick(sender As Object, e As EventArgs) Handles LvAkunBahan.DoubleClick
        If LvAkunBahan.Items.Count = 0 Then Exit Sub

        TxtKodeAkunBhn.Text = LvAkunBahan.FocusedItem.SubItems(0).Text
        LvAkunBahan.Visible = False
        '   flagTxtLeaveOto = False
        TxtKodeAkunBhn.Focus() : Btn_Simpan.Focus()
    End Sub

    Private Sub LvAkunBahan_KeyDown(sender As Object, e As KeyEventArgs) Handles LvAkunBahan.KeyDown
        If e.KeyCode = Keys.Enter Then LvAkunBahan_DoubleClick(LvAkunBahan, e)
    End Sub

    Private Sub TxtNamaAkunBahan_TextChanged(sender As Object, e As EventArgs) Handles TxtNamaAkunBahan.TextChanged
        If TxtNamaAkunBahan.Text.Trim.Length = 0 Then
            LvAkunBahan.Items.Clear()
            LvAkunBahan.Visible = False
            LvAkunBahan.Location = New Point(695, 220)

            TxtKodeAkunBhn.Text = ""
            Exit Sub
        End If

        If TxtNamaAkunBahan.Text.Length >= 1 Then
            Try
                OpenConn()
                LvAkunBahan.Items.Clear()
                SQL = "select kode_master_acc + kode_acc + kode_detail_acc as kode_akun, keterangan from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and keterangan like '%" & TxtNamaAkunBahan.Text & "%'"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read

                        Dim Lv As New ListViewItem
                        Lv = LvAkunBahan.Items.Add(Dr("kode_akun"))
                        Lv.SubItems.Add(Dr("keterangan"))

                    Loop



                End Using

                LvAkunBahan.Visible = True
                LvAkunBahan.Location = New Point(211, 169)

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub TxtNamaAkunBahan_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtNamaAkunBahan.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvAkunBahan.Items.Count = 0 Then Exit Sub
            LvAkunBahan.Focus()
        ElseIf e.KeyCode = Keys.Enter Then
            TxtNamaAkunBahan.Focus()
        End If
    End Sub

    Private Sub TxtNamaAkunBahan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNamaAkunBahan.KeyPress
        If e.KeyChar = Chr(13) Then TxtNamaAkunBahan.Focus()
    End Sub

    Private Sub TxtKodeAkunPerjalanan_TextChanged(sender As Object, e As EventArgs) Handles TxtKodeAkunPerjalanan.TextChanged


        If TxtKodeAkunPerjalanan.Text.Trim.Length = 0 Then

            LvAkunPerjalanan.Items.Clear()
            LvAkunPerjalanan.Visible = False
            LvAkunPerjalanan.Location = New Point(695, 220)

            TxtNamaAkunPerjalanan.Text = ""

            Exit Sub
        End If

        If TxtKodeAkunPerjalanan.Text.Length >= 1 Then
            Try
                OpenConn()
                LvAkunPerjalanan.Items.Clear()

                Using Dr = OpenTrans("select kode_master_acc + kode_acc + kode_detail_acc as kode_akun, keterangan from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc + kode_acc + kode_detail_acc like '%" & TxtKodeAkunPerjalanan.Text & "%'")

                    Do While Dr.Read
                        Dim Lv As New ListViewItem
                        Lv = LvAkunPerjalanan.Items.Add(Dr("kode_akun"))
                        Lv.SubItems.Add(Dr("keterangan"))

                    Loop

                End Using

                LvAkunPerjalanan.Visible = True
                LvAkunPerjalanan.Location = New Point(211, 200)

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

    End Sub

    Private Sub TxtKodeAkunPerjalanan_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKodeAkunPerjalanan.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvAkunBahan.Items.Count = 0 Then Exit Sub
            LvAkunBahan.Focus()
        ElseIf e.KeyCode = Keys.Enter Then
            TxtKodeAkunBhn.Focus()
        End If
    End Sub

    Private Sub TxtKodeAkunPerjalanan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKodeAkunPerjalanan.KeyPress
        If e.KeyChar = Chr(13) Then TxtKodeAkunBhn.Focus()
    End Sub

    Private Sub TxtKodeAkunPerjalanan_Leave(sender As Object, e As EventArgs) Handles TxtKodeAkunPerjalanan.Leave
        If TxtKodeAkunPerjalanan.Text.Trim.Length = 0 Then Exit Sub
        If flagTxtLeaveOto = False Then Exit Sub
        If LvAkunPerjalanan.Focused = True Then Exit Sub

        Try
            OpenConn()

            Using Dr = OpenTrans("select * from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc + kode_acc + kode_detail_acc = '" & TxtKodeAkunPerjalanan.Text & "'")
                If Dr.Read Then
                    TxtNamaAkunPerjalanan.Text = Dr("keterangan")
                    LvAkunPerjalanan.Visible = False

                    Btn_Simpan.Focus()
                Else
                    ' MessageBox.Show("Kode account tidak ditemukan . . ", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TxtKodeAkunPerjalanan.Text = "" : TxtNamaAkunPerjalanan.Text = ""
                    TxtKodeAkunPerjalanan.Focus()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvAkunPerjalanan_DoubleClick(sender As Object, e As EventArgs) Handles LvAkunPerjalanan.DoubleClick
        If LvAkunPerjalanan.Items.Count = 0 Then Exit Sub

        TxtKodeAkunPerjalanan.Text = LvAkunPerjalanan.FocusedItem.SubItems(0).Text
        LvAkunPerjalanan.Visible = False
        '   flagTxtLeaveOto = False
        TxtKodeAkunPerjalanan.Focus() : Btn_Simpan.Focus()
    End Sub

    Private Sub LvAkunPerjalanan_KeyDown(sender As Object, e As KeyEventArgs) Handles LvAkunPerjalanan.KeyDown
        If e.KeyCode = Keys.Enter Then LvAkunPerjalanan_DoubleClick(LvAkunPerjalanan, e)
    End Sub

    Private Sub TxtNamaAkunPerjalanan_TextChanged(sender As Object, e As EventArgs) Handles TxtNamaAkunPerjalanan.TextChanged
        If TxtNamaAkunPerjalanan.Text.Trim.Length = 0 Then
            LvAkunPerjalanan.Items.Clear()
            LvAkunPerjalanan.Visible = False
            LvAkunPerjalanan.Location = New Point(695, 220)

            TxtNamaAkunPerjalanan.Text = ""
            Exit Sub
        End If

        If TxtNamaAkunPerjalanan.Text.Length >= 1 Then
            Try
                OpenConn()
                LvAkunPerjalanan.Items.Clear()
                SQL = "select kode_master_acc + kode_acc + kode_detail_acc as kode_akun, keterangan from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and keterangan like '%" & TxtNamaAkunPerjalanan.Text & "%'"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read

                        Dim Lv As New ListViewItem
                        Lv = LvAkunPerjalanan.Items.Add(Dr("kode_akun"))
                        Lv.SubItems.Add(Dr("keterangan"))

                    Loop



                End Using

                LvAkunPerjalanan.Visible = True
                LvAkunPerjalanan.Location = New Point(211, 200)

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub TxtNamaAkunPerjalanan_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtNamaAkunPerjalanan.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvAkunPerjalanan.Items.Count = 0 Then Exit Sub
            LvAkunPerjalanan.Focus()
        ElseIf e.KeyCode = Keys.Enter Then
            TxtNamaAkunPerjalanan.Focus()
        End If
    End Sub

    Private Sub TxtNamaAkunPerjalanan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNamaAkunPerjalanan.KeyPress
        If e.KeyChar = Chr(13) Then TxtNamaAkunPerjalanan.Focus()
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then TxtKodeAkunBhn.Focus()
    End Sub

    Private Sub Txt_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub
End Class
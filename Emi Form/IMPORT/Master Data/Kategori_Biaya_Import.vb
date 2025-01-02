Public Class Kategori_Biaya_Import
    Dim arrcari, arrAkun As New ArrayList

    Private Sub Master_Barang_Kategori_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Kosong()
    End Sub

    Private Sub Kosong()
        txtKode_Kategori.Text = ""
        txtKeterangan.Text = ""
        cbUrutan.SelectedIndex = -1

        cbUrutan.Items.Clear()
        For x As Integer = 1 To 30
            cbUrutan.Items.Add(x)
        Next

        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Y")
        ComboBox1.Items.Add("T")
        ComboBox1.SelectedIndex = -1

        Cmb_MasukHPP.Items.Clear()
        Cmb_MasukHPP.Items.Add("Y")
        Cmb_MasukHPP.Items.Add("T")
        Cmb_MasukHPP.SelectedIndex = -1

        lvKategori_Biaya.Columns.Clear()
        lvKategori_Biaya.Columns.Add("Kode Kategori", 150, HorizontalAlignment.Left)
        lvKategori_Biaya.Columns.Add("Keterangan", 150, HorizontalAlignment.Left)
        lvKategori_Biaya.Columns.Add("Urutan", 100, HorizontalAlignment.Center)
        lvKategori_Biaya.Columns.Add("Kode Master Biaya", 150, HorizontalAlignment.Center)
        lvKategori_Biaya.Columns.Add("Flag_Average", 100, HorizontalAlignment.Center)
        lvKategori_Biaya.Columns.Add("Flag Masuk HPP", 100, HorizontalAlignment.Center)
        lvKategori_Biaya.View = View.Details

        Try
            OpenConn()
            CbKode_Master.Items.Clear()
            SQL = "select Kode_master_Kategori_Biaya_import from master_Kategori_Biaya_import where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CbKode_Master.Items.Add(dr("Kode_master_Kategori_Biaya_import"))
                Loop
            End Using

            lvKategori_Biaya.Items.Clear()
            SQL = "Select Kode_Kategori_Biaya_import, Keterangan, Urutan, Kode_master_Kategori_Biaya_import, Flag_average, Flag_Masuk_HPP "
            SQL = SQL & "From Kategori_Biaya_import "
            SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = lvKategori_Biaya.Items.Add(dr("Kode_Kategori_Biaya_import"))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Keterangan")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Urutan")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Kode_master_Kategori_Biaya_import")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Flag_average")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Flag_Masuk_HPP")))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        btSimpan.Text = "&Simpan" : btHapus.Enabled = False

    End Sub

    Private Sub BtRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRefresh.Click
        Kosong()
    End Sub

    Private Sub txtKode_Kategori_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKode_Kategori.KeyPress
        If e.KeyChar = Chr(13) Then txtKeterangan.Focus()
    End Sub

    Private Sub txtKeterangan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKeterangan.KeyPress
        If e.KeyChar = Chr(13) Then cbUrutan.Focus()
    End Sub

    Private Sub cbUrutan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbUrutan.KeyPress
        If e.KeyChar = Chr(13) Then CbKode_Master.Focus()
    End Sub

    Private Sub txtKode_Kategori_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKode_Kategori.Leave
        If txtKode_Kategori.Text.Trim.Length = 0 Then Exit Sub
        Try
            OpenConn()

            SQL = "Select Kode_Kategori_Biaya_import, Keterangan, Urutan, Kode_master_Kategori_Biaya_import, flag_average, Flag_Masuk_HPP "
            SQL = SQL & "From Kategori_Biaya_import Where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Kategori_Biaya_import = '" & txtKode_Kategori.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    txtKode_Kategori.Text = Dr("Kode_Kategori_Biaya_import")
                    txtKeterangan.Text = Dr("Keterangan")
                    cbUrutan.Text = Dr("Urutan")
                    CbKode_Master.Text = Dr("Kode_master_Kategori_Biaya_import")
                    ComboBox1.Text = Dr("flag_average")
                    If General_Class.CekNULL(Dr("Flag_Masuk_HPP")) = "" Then
                        Cmb_MasukHPP.SelectedIndex = -1
                    Else
                        Cmb_MasukHPP.Text = Dr("Flag_Masuk_HPP")
                    End If


                    btSimpan.Text = "&Update" : btHapus.Enabled = True
                Else
                    txtKeterangan.Text = ""
                    cbUrutan.SelectedIndex = -1
                    CbKode_Master.SelectedIndex = -1
                    ComboBox1.SelectedIndex = -1

                    btSimpan.Text = "&Simpan" : btHapus.Enabled = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub btSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSimpan.Click

        If txtKode_Kategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Kategori Biaya Import harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtKode_Kategori.Focus()
            Exit Sub
        ElseIf txtKeterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtKeterangan.Focus()
            Exit Sub
        ElseIf cbUrutan.Text.Trim.Length = 0 Then
            MessageBox.Show("Urutan harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cbUrutan.Focus()
            Exit Sub
        ElseIf CbKode_Master.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Master Biaya harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CbKode_Master.Focus()
            Exit Sub
        ElseIf ComboBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Flag Average harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf Cmb_MasukHPP.Text.Trim.Length = 0 Then
            MessageBox.Show("Flag Masuk HPP harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_MasukHPP.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            If btSimpan.Text = "&Simpan" Then
                SQL = "Insert Into Kategori_Biaya_Import(Kode_Perusahaan,Kode_Kategori_Biaya_Import,"
                SQL = SQL & "Keterangan,Urutan,Kode_master_Kategori_Biaya_import, flag_average, Flag_Masuk_HPP) "
                SQL = SQL & "Values("
                SQL = SQL & "'" & KodePerusahaan & "','" & txtKode_Kategori.Text.Trim & "','" & txtKeterangan.Text.Trim & "',"
                SQL = SQL & "'" & cbUrutan.Text.Trim & "', '" & CbKode_Master.Text.Trim & "', '" & ComboBox1.Text.Trim & "', "
                SQL = SQL & "'" & Cmb_MasukHPP.Text.Trim & "')"
                ExecuteTrans(SQL)
            Else
                SQL = "Update Kategori_Biaya_Import Set kode_Kategori_Biaya_Import = '" & txtKode_Kategori.Text.Trim & "',"
                SQL = SQL & "Keterangan = '" & txtKeterangan.Text.Trim & "', "
                SQL = SQL & "Urutan = '" & cbUrutan.Text.Trim & "', "
                SQL = SQL & "Kode_master_Kategori_Biaya_import = '" & CbKode_Master.Text.Trim & "',"
                SQL = SQL & "flag_average = '" & ComboBox1.Text.Trim & "',"
                SQL = SQL & "Flag_Masuk_HPP = '" & Cmb_MasukHPP.Text.Trim & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Kode_Kategori_Biaya_Import = '" & txtKode_Kategori.Text.Trim & "'"
                ExecuteTrans(SQL)
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
        txtKode_Kategori.Focus()
    End Sub

    Private Sub btHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btHapus.Click
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus . . ? ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Select count(Kode_Kategori_biaya_import) as jumlah from Biaya_import where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Kategori_Biaya_Import = '" & txtKode_Kategori.Text.Trim & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            If (.Rows(0).Item("jumlah")) = 0 Then
                                ExecuteTrans("Delete From Kategori_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Kategori_Biaya_import = '" & txtKode_Kategori.Text.Trim & "'")
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Penghapusan dibatalkan . . ! ! karena Kode Kategori Biaya Import Sedang digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If

                    End With

                End Using

                Cmd.Transaction.Commit()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        Kosong()
        txtKode_Kategori.Focus()
    End Sub

    Private Sub lvKategori_Biaya_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvKategori_Biaya.DoubleClick
        txtKode_Kategori.Text = lvKategori_Biaya.FocusedItem.Text

        txtKode_Kategori_Leave(lvKategori_Biaya, e)
    End Sub

    Private Sub CbFlag_Form_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CbKode_Master.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox1.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then btSimpan.Focus()
    End Sub
End Class

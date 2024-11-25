Public Class Master_Klasifikasi_Bahan2

    Dim arrKlasifikasiBahan1, arrKlasifikasiBahan1Prefix As New ArrayList
    Dim selectedIdKlasifikasiBahan1, idKlasifikasiAwal, prefixAwal As String

    Dim lv_IdKlasifikasi2, lv_IdKlasifikasi1, lv_Kode, lv_Keterangan, lv_prefix As String

    Dim itemKlasifikasi2 As Integer = 0
    Dim itemKlasifikasi1 As Integer = 1
    Dim itemKode As Integer = 3
    Dim itemKeterangan As Integer = 4
    Dim itemPrefix As Integer = 5

    Private Sub Master_Klasifikasi_Bahan2_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_KlasifikasiBahan.Columns.Add("id_Klasifikasi_Bahan_2", 0, HorizontalAlignment.Center)
        Lv_KlasifikasiBahan.Columns.Add("id_Klasifikasi_Bahan_1", 0, HorizontalAlignment.Center)
        Lv_KlasifikasiBahan.Columns.Add("Klasifikasi Bahan 1", 250, HorizontalAlignment.Center)
        Lv_KlasifikasiBahan.Columns.Add("Kode", 250, HorizontalAlignment.Center)
        Lv_KlasifikasiBahan.Columns.Add("Keterangan", 300, HorizontalAlignment.Center)
        Lv_KlasifikasiBahan.Columns.Add("Prefix", 120, HorizontalAlignment.Center)

        Lv_KlasifikasiBahan.View = View.Details

        kosong()
        Load_Lv()

    End Sub

    Private Sub Cmb_Kolom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kolom.SelectedIndexChanged
        Tb_Value.Enabled = True
    End Sub


    Private Sub kosong()

        Cmb_Kategori1.Items.Clear()
        Cmb_Kolom.Items.Clear()
        Tb_Kode.Text = String.Empty
        Tb_Keterangan.Text = String.Empty
        Tb_PrefixKategori1.Text = String.Empty
        Tb_PrefixKategori2.Text = String.Empty
        Tb_Value.Text = String.Empty
        selectedIdKlasifikasiBahan1 = String.Empty
        idKlasifikasiAwal = String.Empty
        prefixAwal = String.Empty

        Lv_KlasifikasiBahan.Items.Clear()

        Btn_Simpan.Tag = "SIMPAN"
        Btn_Simpan.Text = "&Simpan"

        Cmb_Kolom.Items.Add("Kode")
        Cmb_Kolom.Items.Add("Keterangan")

        arrKlasifikasiBahan1.Clear()
        arrKlasifikasiBahan1Prefix.Clear()

        Try
            OpenConn()

            SQL = "Select Id_Klasifikasi_Bahan, Kode_Klasifikasi_Bahan, Keterangan, Prefix_Klasifikasi_Bahan from EMI_Klasifikasi_Bahan order by Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Kategori1.Items.Add(Dr("Keterangan")) : arrKlasifikasiBahan1.Add(Dr("Id_Klasifikasi_Bahan"))
                    arrKlasifikasiBahan1Prefix.Add(Dr("Prefix_Klasifikasi_Bahan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Load_Lv(ByVal Optional filter As String = "")
        Try
            OpenConn()

            Lv_KlasifikasiBahan.Items.Clear()

            SQL = "Select b.Id_Klasifikasi_Bahan ,b.Keterangan ,a.Id_Klasifikasi_Bahan2, a.Kode_Klasifikasi_Bahan, a.Keterangan as keterangan2, a.Prefix_Klasifikasi_Bahan "
            SQL = SQL & "from EMI_Klasifikasi_Bahan2 a, EMI_Klasifikasi_Bahan b "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Klasifikasi_Bahan1=b.Id_Klasifikasi_Bahan "
            If Not filter = "" Then
                SQL = SQL & "and " & filter & " "
            End If
            SQL = SQL & "group by b.Keterangan ,a.Id_Klasifikasi_Bahan2, a.Kode_Klasifikasi_Bahan, a.Keterangan, a.Prefix_Klasifikasi_Bahan, b.Id_Klasifikasi_Bahan "
            SQL = SQL & "order by a.Id_Klasifikasi_Bahan2"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_KlasifikasiBahan.Items.Add(Dr("Id_Klasifikasi_Bahan2"))
                    Lv.SubItems.Add(Dr("Id_Klasifikasi_Bahan"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Kode_Klasifikasi_Bahan"))
                    Lv.SubItems.Add(Dr("keterangan2"))
                    Lv.SubItems.Add(Dr("Prefix_Klasifikasi_Bahan"))

                    Lv.SubItems(1).BackColor = Color.LightBlue
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
        Load_Lv()
    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)

        lv_IdKlasifikasi2 = Lv_KlasifikasiBahan.Items(index).SubItems(itemKlasifikasi2).Text
        lv_IdKlasifikasi1 = Lv_KlasifikasiBahan.Items(index).SubItems(itemKlasifikasi1).Text
        lv_Kode = Lv_KlasifikasiBahan.Items(index).SubItems(itemKode).Text
        lv_Keterangan = Lv_KlasifikasiBahan.Items(index).SubItems(itemKeterangan).Text
        lv_prefix = Lv_KlasifikasiBahan.Items(index).SubItems(itemPrefix).Text

    End Sub

    Private Sub Lv_KlasifikasiBahan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_KlasifikasiBahan.DoubleClick
        If Lv_KlasifikasiBahan.Items.Count = 0 Then Exit Sub

        Get_Data_Lv(Lv_KlasifikasiBahan.FocusedItem.Index)


        Tb_Kode.Text = lv_Kode
        Tb_Keterangan.Text = lv_Keterangan
        Tb_PrefixKategori2.Text = lv_prefix

        idKlasifikasiAwal = lv_IdKlasifikasi2
        prefixAwal = lv_prefix

        Try
            OpenConn()

            SQL = "Select Prefix_Klasifikasi_Bahan, keterangan from EMI_Klasifikasi_Bahan where Id_Klasifikasi_Bahan='" & lv_IdKlasifikasi1 & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Tb_PrefixKategori1.Text = Dr("Prefix_Klasifikasi_Bahan")
                    Cmb_Kategori1.SelectedItem = Dr("keterangan")
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Simpan.Tag = "UPDATE"
        Btn_Simpan.Text = "&Update"

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Cmb_Kategori1.SelectedIndex = -1 Or selectedIdKlasifikasiBahan1.Trim.Length = 0 Then
            MessageBox.Show("Kategori 1 Harus Dipilih", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Kategori1.Focus() : Exit Sub
        ElseIf Tb_Kode.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Harus Di Isi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tb_Kode.Focus() : Exit Sub
        ElseIf Tb_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus Di Isi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tb_Keterangan.Focus() : Exit Sub
        ElseIf Tb_PrefixKategori2.Text.Trim.Length <> 2 Then
            MessageBox.Show("Prefix Harus 2 Angka", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tb_PrefixKategori2.Focus() : Exit Sub
        ElseIf IsNumeric(Tb_PrefixKategori2.Text) = False Then
            MessageBox.Show("Prefix Harus Angka", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tb_PrefixKategori2.Focus() : Exit Sub
        End If



        '======= INSERT =======
        Try
            OpenConn()

            If Btn_Simpan.Tag = "SIMPAN" Then

                SQL = "Select Id_Klasifikasi_Bahan2 from EMI_Klasifikasi_Bahan2 "
                SQL = SQL & "where Prefix_Klasifikasi_Bahan='" & Tb_PrefixKategori2.Text & "'"
                Using dr = OpenTrans(SQL)
                    If dr.HasRows Then
                        MessageBox.Show(Base_Language.Lang_Klasifikasi_Bahan_Error_Prefix_Sudah_Ada, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Tb_PrefixKategori2.Focus()
                        CloseConn()
                        Exit Sub
                    End If
                End Using

                SQL = "insert into EMI_Klasifikasi_Bahan2 ( Kode_Perusahaan, Id_Klasifikasi_Bahan1, "
                SQL = SQL & "Kode_Klasifikasi_Bahan, Keterangan, Prefix_Klasifikasi_Bahan) "
                SQL = SQL & "values ('" & KodePerusahaan & "', '" & selectedIdKlasifikasiBahan1 & "', '" & Tb_Kode.Text.Trim & "', "
                SQL = SQL & "'" & Tb_Keterangan.Text.Trim & "', '" & Tb_PrefixKategori2.Text.Trim & "')"
                ExecuteTrans(SQL)

                'MessageBox.Show("Berhasil Disimpan", Base_Language.Lang_Global_Simpan, MessageBoxButtons.OK, MessageBoxIcon.Information)

            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                SQL = "UPDATE EMI_Klasifikasi_Bahan2 set Id_Klasifikasi_Bahan1='" & selectedIdKlasifikasiBahan1 & "', "
                SQL = SQL & "Kode_Klasifikasi_Bahan='" & Tb_Kode.Text.Trim & "', Keterangan='" & Tb_Keterangan.Text.Trim & "', "
                SQL = SQL & "Prefix_Klasifikasi_Bahan='" & Tb_PrefixKategori2.Text.Trim & "'"
                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Id_Klasifikasi_Bahan2 = '" & idKlasifikasiAwal & "' "
                SQL = SQL & " and Prefix_Klasifikasi_Bahan='" & prefixAwal & "'"
                ExecuteTrans(SQL)

                MessageBox.Show("Berhasil DiUpdate", Base_Language.Lang_Global_Update, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            kosong()
            Load_Lv()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Cmb_Kategori1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kategori1.SelectedIndexChanged
        If Cmb_Kategori1.Items.Count = 0 AndAlso Cmb_Kategori1.SelectedIndex = -1 Then Exit Sub

        Tb_PrefixKategori1.Text = arrKlasifikasiBahan1Prefix(Cmb_Kategori1.SelectedIndex)
        selectedIdKlasifikasiBahan1 = arrKlasifikasiBahan1(Cmb_Kategori1.SelectedIndex)

    End Sub

    Private Sub Tb_PrefixKategori2_Leave(sender As Object, e As EventArgs) Handles Tb_PrefixKategori2.Leave
        If Not IsNumeric(Tb_PrefixKategori2.Text) Then Tb_PrefixKategori2.Text = ""
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Kolom.SelectedIndex = -1 Then Exit Sub

        Dim filter As String = ""

        If Cmb_Kolom.SelectedIndex = 0 Then

            filter = "a.Kode_Klasifikasi_Bahan like '" & Tb_Value.Text & "%'"

        ElseIf Cmb_Kolom.SelectedIndex = 1 Then

            filter = "a.Keterangan like '" & Tb_Value.Text & "%'"

        End If

        Load_Lv(filter)
    End Sub
    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        If Cmb_Kategori1.SelectedIndex = -1 Or selectedIdKlasifikasiBahan1.Trim.Length = 0 Then Exit Sub


        Dim Hapus1 As String = MessageBox.Show("Yakin Ingin Hapus?", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Try

                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                SQL = "delete from EMI_Klasifikasi_Bahan2 where kode_perusahaan='" & KodePerusahaan & "' and Id_Klasifikasi_Bahan2='" & idKlasifikasiAwal & "' "
                SQL = SQL & "and Prefix_Klasifikasi_Bahan='" & prefixAwal & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

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
        Load_Lv()

    End Sub

End Class
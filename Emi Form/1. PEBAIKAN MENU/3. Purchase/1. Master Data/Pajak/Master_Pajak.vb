Public Class Master_Pajak
    Dim Lv As ListViewItem
    Dim LKolom1 As New ArrayList
    Dim LKolom2 As New ArrayList
    Dim LKolom3 As New ArrayList

    Dim LKlasBarang As New ArrayList
    Dim LJenisSupp As New ArrayList
    Dim LTarifPajak As New ArrayList
    Dim LKlasJasa As New ArrayList
    Dim LSubKlasJasa As New ArrayList

    Dim LKategori As New ArrayList
    Dim LTarif As New ArrayList

    Dim visibleLvAkun As Boolean = True

    Private Sub KosongTab1()
        Lv1.Columns(0).Width = 120
        Lv1.Columns(1).Width = 320
        Lv1.Columns(2).Width = 70
        Lv1.Columns(3).Width = 95
        TxtKodeTarif.Text = "" : TxtKetTarif.Text = "" : TxtJenisPajak.Text = "" : RBYa.Checked = False : RBTidak.Checked = False : TxtTarif.Text = "0"
        BtnSimpan1.Text = "&Simpan" : BtnHapus1.Enabled = False : CmbKolom1.SelectedIndex = -1 : TxtValue1.Text = ""

        Lv_Akun.Visible = False : Lv_Akun.Location = New Point(107, 122)
        Lv_Akun.Columns.Clear()
        Lv_Akun.Columns.Add("Akun", 100, HorizontalAlignment.Center)
        Lv_Akun.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Akun.View = View.Details

        Txt_KdAkun.Text = "" : Txt_KeteranganAkun.Text = ""

        TampilTab1("", "")
    End Sub

    Private Sub KosongTab2()
        Lv2.Columns(0).Width = 150
        Lv2.Columns(1).Width = 350
        Lv2.Columns(2).Width = 200
        Lv2.Columns(3).Width = 200
        Lv2.Columns(4).Width = 200
        Lv2.Columns(5).Width = 200
        Lv2.Columns(6).Width = 200
        TxtKdTarifPPH.Text = "" : TxtKetTarifPPH.Text = ""
        Try
            OpenConn()

            CmbKlasBarang.Items.Clear() : LKlasBarang.Clear()
            CmbKlasBarang.Items.Add("Barang") : LKlasBarang.Add("Barang")
            CmbKlasBarang.Items.Add("Jasa") : LKlasBarang.Add("Jasa")

            CmbJnsSupp.Items.Clear() : LJenisSupp.Clear()
            SQL = "select kode_jenis_supplier,keterangan from emi_master_jenis_supplier where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbJnsSupp.Items.Add(dr("keterangan")) : LJenisSupp.Add(dr("kode_jenis_supplier"))
                Loop
            End Using

            CmbTarifPajak2.Items.Clear() : LTarifPajak.Clear()
            SQL = "select kode_tarif,keterangan from emi_master_pajak where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbTarifPajak2.Items.Add(dr("keterangan")) : LTarifPajak.Add(dr("kode_tarif"))
                Loop
            End Using

            CmbKlasJasa.Items.Clear() : LKlasJasa.Clear()
            SQL = "select kode_jasa,keterangan from emi_master_jasa where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbKlasJasa.Items.Add(dr("keterangan")) : LKlasJasa.Add(dr("kode_jasa"))
                Loop
            End Using

            CmbSubKlasJasa.Items.Clear() : LSubKlasJasa.Clear()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BtnSimpan2.Text = "&Simpan" : BtnHapus2.Enabled = False : CmbKolom2.SelectedIndex = -1 : TxtValue2.Text = ""
        TampilTab2("", "")
    End Sub

    Private Sub KosongTab3()
        Lv3.Columns(0).Width = 300
        Lv3.Columns(1).Width = 300

        Try
            OpenConn()

            CmbKatPajak.Items.Clear() : LKategori.Clear()
            SQL = "select kode_kategori,keterangan from emi_master_kategori_pajak where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbKatPajak.Items.Add(dr("keterangan")) : LKategori.Add(dr("kode_kategori"))
                Loop
            End Using

            CmbTarifPajak3.Items.Clear() : LTarif.Clear()
            SQL = "select kode_tarif,keterangan from emi_master_pajak where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbTarifPajak3.Items.Add(dr("keterangan")) : LTarif.Add(dr("kode_tarif"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnSimpan3.Text = "&Simpan" : BtnSimpan3.Enabled = True : BtnHapus3.Enabled = False : CmbKolom3.SelectedIndex = -1 : TxtValue3.Text = ""
        TampilTab3("", "")
    End Sub

    Private Sub IsiCombo1()
        CmbKolom1.Items.Clear() : LKolom1.Clear()
        CmbKolom1.Items.Add("Kode Tarif") : LKolom1.Add("kode_tarif")
        CmbKolom1.Items.Add("Keterangan") : LKolom1.Add("keterangan")
        CmbKolom1.Items.Add("Jenis Pajak") : LKolom1.Add("jenis")
        CmbKolom1.Items.Add("Flag PPH 21") : LKolom1.Add("pph_21")
        CmbKolom1.Items.Add("Persen Tarif") : LKolom1.Add("tarif")
    End Sub

    Private Sub IsiCombo2()
        CmbKolom2.Items.Clear() : LKolom2.Clear()
        CmbKolom2.Items.Add("Kode Tarif PPH") : LKolom2.Add("a.kode_tarif_pph")
        CmbKolom2.Items.Add("Keterangan") : LKolom2.Add("a.keterangan")
        CmbKolom2.Items.Add("Klasfikasi Master Barang") : LKolom2.Add("a.klasifikasi_master_barang")
        CmbKolom2.Items.Add("Jenis Supplier") : LKolom2.Add("b.keterangan")
        CmbKolom2.Items.Add("Jasa") : LKolom2.Add("c.keterangan")
        CmbKolom2.Items.Add("Sub Jasa") : LKolom2.Add("d.keterangan")
        CmbKolom2.Items.Add("Kode Tarif") : LKolom2.Add("a.kode_tarif")
    End Sub

    Private Sub IsiCombo3()
        CmbKolom3.Items.Clear() : LKolom3.Clear()
        CmbKolom3.Items.Add("Kode Kategori Pajak") : LKolom3.Add("a.kode_kategori")
        CmbKolom3.Items.Add("Keterangan Kategori") : LKolom3.Add("b.keterangan")
        CmbKolom3.Items.Add("Kode Tarif Pajak") : LKolom3.Add("a.kode_tarif")
        CmbKolom3.Items.Add("Keterangan Tarif") : LKolom3.Add("c.keterangan")
    End Sub

    Private Sub Master_Pajak_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KosongTab1() : IsiCombo1()
        KosongTab2() : IsiCombo2()
        KosongTab3() : IsiCombo3()

        TxtKodeTarif.Focus()
    End Sub

    Private Sub TxtKodeTarif_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKodeTarif.KeyPress
        If e.KeyChar = Chr(13) Then TxtKetTarif.Focus()
    End Sub

    Private Sub TxtKetTarif_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKetTarif.KeyPress
        If e.KeyChar = Chr(13) Then TxtJenisPajak.Focus()
    End Sub

    Private Sub TxtJenisPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJenisPajak.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdAkun.Focus()
    End Sub

    Private Sub RBYa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RBYa.KeyPress
        If e.KeyChar = Chr(13) Then TxtTarif.Focus()
    End Sub

    Private Sub RBTidak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RBTidak.KeyPress
        If e.KeyChar = Chr(13) Then TxtTarif.Focus()
    End Sub

    Private Sub TxtTarif_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtTarif.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan1.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub CmbKolom1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKolom1.KeyPress
        If e.KeyChar = Chr(13) Then TxtValue1.Focus()
    End Sub

    Private Sub TxtValue1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtValue1.KeyPress
        If e.KeyChar = Chr(13) Then BtnCari1_Click(TxtValue1, e)
    End Sub

    Private Sub BtnSimpan1_Click(sender As Object, e As EventArgs) Handles BtnSimpan1.Click
        If TxtKodeTarif.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode tarif harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKodeTarif.Focus() : Exit Sub
        ElseIf TxtKetTarif.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan tarif harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKetTarif.Focus() : Exit Sub
        ElseIf TxtJenisPajak.Text.Trim.Length = 0 Then
            MessageBox.Show("Jenis pajak harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtJenisPajak.Focus() : Exit Sub
        ElseIf RBYa.Checked = False And RBTidak.Checked = False Then
            MessageBox.Show("Pilihan PPH 21 Ya atau Tidak harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            RBYa.Focus() : Exit Sub
        ElseIf TxtTarif.Text.Trim.Length = 0 Then
            MessageBox.Show("Persentase tarif harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtTarif.Focus() : Exit Sub
        ElseIf Txt_KdAkun.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Akun harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdAkun.Focus() : Exit Sub
        ElseIf Txt_KeteranganAkun.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Akun harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KeteranganAkun.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim PPH21 As String = ""
            If RBYa.Checked = True Then PPH21 = "Y" Else PPH21 = "T"

            '===============================
            '=     CEK Apakah AKun Ada     =
            '===============================
            SQL = "select Kode_Account, Keterangan from Detail_Account where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Account = '" & Txt_KdAkun.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Akun Tidak Ditemukan", "MAster Pajak", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_KdAkun.Text = "" : Txt_KeteranganAkun.Text = ""
                    Txt_KdAkun.Focus()
                    Exit Sub
                End If
            End Using

            If BtnSimpan1.Text = "&Simpan" Then
                SQL = "insert into EMI_Master_Pajak(kode_perusahaan,kode_tarif,keterangan,jenis,pph_21,tarif, kode_akun) "
                SQL = SQL & "values('" & KodePerusahaan & "','" & TxtKodeTarif.Text.Trim.ToUpper & "','" & TxtKetTarif.Text.Trim.ToUpper & "','"
                SQL = SQL & TxtJenisPajak.Text & "','" & PPH21 & "','" & TxtTarif.Text & "', '" & Txt_KdAkun.Text & "')"
            Else
                SQL = "update EMI_Master_Pajak set keterangan = '" & TxtKetTarif.Text.Trim.ToUpper & "',jenis = '" & TxtJenisPajak.Text.Trim.ToUpper & "',"
                SQL = SQL & "pph_21 = '" & PPH21 & "',tarif = '" & TxtTarif.Text & "', kode_akun = '" & Txt_KdAkun.Text & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_tarif = '" & TxtKodeTarif.Text & "'"
            End If
            ExecuteTrans(SQL)

            CloseConn()

            KosongTab1()
            TxtKodeTarif.Focus()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKodeTarif_Leave(sender As Object, e As EventArgs) Handles TxtKodeTarif.Leave
        If TxtKodeTarif.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select a.kode_tarif, a.keterangan, a.jenis, a.pph_21, a.tarif, a.Kode_Akun, "
            SQL = SQL & "ISNULL(( select z.Keterangan from Detail_Account z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Account = a.kode_akun "
            SQL = SQL & "), '-') as keterangan_akun "
            SQL = SQL & "From EMI_Master_Pajak a "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_tarif= '" & TxtKodeTarif.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    visibleLvAkun = False
                    TxtKodeTarif.Text = Dr("kode_tarif")
                    TxtKetTarif.Text = Dr("keterangan")
                    TxtJenisPajak.Text = Dr("jenis")
                    If Dr("pph_21") = "Y" Then RBYa.Checked = True Else RBTidak.Checked = True
                    TxtTarif.Text = ChangeCommaToDot(Dr("tarif"))
                    Txt_KdAkun.Text = General_Class.CekNULL(Dr("Kode_Akun"))
                    Txt_KeteranganAkun.Text = Dr("keterangan_akun")
                    BtnSimpan1.Text = "&Update" : BtnHapus1.Enabled = True
                    Lv_Akun.Visible = False
                Else
                    TxtKetTarif.Text = "" : TxtJenisPajak.Text = ""
                    RBYa.Checked = False : RBTidak.Checked = False
                    TxtTarif.Text = "0"
                    BtnSimpan1.Text = "&Simpan" : BtnHapus1.Enabled = False
                End If
            End Using

            visibleLvAkun = True

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnHapus1_Click(sender As Object, e As EventArgs) Handles BtnHapus1.Click
        If TxtKodeTarif.Text.Trim.Length = 0 Then Exit Sub

        Dim Hapus As String = MessageBox.Show("Anda yakin akan hapus data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "Select kode_tarif From EMI_Tarif_PPH "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_tarif = '" & TxtKodeTarif.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Kode tarif masih digunakan di master tarif PPH.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'SQL = "Select kode_tarif From EMI_Tarif_PPH1 "
                'SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_tarif = '" & TxtKodeTarif.Text.Trim & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        Dr.Close()
                '        CloseConn()
                '        MessageBox.Show("Kode tarif masih digunakan di master tarif PPH1.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                SQL = "delete from EMI_Master_Pajak where kode_perusahaan = '" & KodePerusahaan & "' and kode_tarif = '" & TxtKodeTarif.Text & "'"
                ExecuteTrans(SQL)

                KosongTab1()
                TxtKodeTarif.Focus()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtnRefresh1_Click(sender As Object, e As EventArgs) Handles BtnRefresh1.Click
        KosongTab1()
        TxtKodeTarif.Focus()
    End Sub

    Private Sub BtnExit1_Click(sender As Object, e As EventArgs) Handles BtnExit1.Click
        Me.Close()
    End Sub

    Private Sub BtnCari1_Click(sender As Object, e As EventArgs) Handles BtnCari1.Click
        If CmbKolom1.SelectedIndex = -1 Then
            MessageBox.Show("Kolom pencarian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbKolom1.Focus() : Exit Sub
        ElseIf TxtValue1.Text.Trim.Length = 0 Then
            MessageBox.Show("Value pencarian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtValue1.Focus() : Exit Sub
        End If

        TampilTab1(LKolom1(CmbKolom1.SelectedIndex), TxtValue1.Text)
    End Sub

    Private Sub TampilTab1(ByVal Kolom As String, ByVal Value As String)
        Try
            OpenConn()

            Lv1.Items.Clear()
            SQL = "select kode_tarif,keterangan,jenis,pph_21,tarif "
            SQL = SQL & "from EMI_Master_Pajak where kode_perusahaan = '" & KodePerusahaan & "' "
            If Kolom.Trim.Length <> 0 Then
                SQL = SQL & "AND " & Kolom & " like '%" & Trim(Value) & "%' "
            End If
            SQL = SQL & "order by kode_tarif"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv1.Items.Add(Dr("kode_tarif"))
                    Lv.SubItems.Add(Dr("keterangan"))
                    Lv.SubItems.Add(Dr("jenis"))
                    Lv.SubItems.Add(Dr("pph_21"))
                    Lv.SubItems.Add(Dr("tarif"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv1_DoubleClick(sender As Object, e As EventArgs) Handles Lv1.DoubleClick
        If Lv1.Items.Count = 0 Then Exit Sub

        TxtKodeTarif.Text = Lv1.FocusedItem.Text
        TxtKodeTarif_Leave(Lv1, e)
    End Sub

    Private Sub TxtKdTarifPPH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKdTarifPPH.KeyPress
        If e.KeyChar = Chr(13) Then TxtKetTarifPPH.Focus()
    End Sub

    Private Sub TxtKetTarifPPH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKetTarifPPH.KeyPress
        If e.KeyChar = Chr(13) Then CmbKlasBarang.Focus()
    End Sub

    Private Sub CmbKlasBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKlasBarang.KeyPress
        If e.KeyChar = Chr(13) Then CmbJnsSupp.Focus()
    End Sub

    Private Sub CmbJnsSupp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbJnsSupp.KeyPress
        If e.KeyChar = Chr(13) Then CmbTarifPajak2.Focus()
    End Sub

    Private Sub CmbTarifPajak2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbTarifPajak2.KeyPress
        If e.KeyChar = Chr(13) Then CmbKlasJasa.Focus()
    End Sub

    Private Sub CmbKlasJasa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKlasJasa.KeyPress
        If e.KeyChar = Chr(13) Then CmbSubKlasJasa.Focus()
    End Sub

    Private Sub CmbSubKlasJasa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSubKlasJasa.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan2.Focus()
    End Sub

    Private Sub CmbKolom2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKolom2.KeyPress
        If e.KeyChar = Chr(13) Then TxtValue2.Focus()
    End Sub

    Private Sub TxtValue2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtValue2.KeyPress
        If e.KeyChar = Chr(13) Then BtnCari2_Click(TxtValue2, e)
    End Sub
    Private Sub Txt_KdAkun_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdAkun.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KeteranganAkun.Focus()
    End Sub
    Private Sub Txt_KeteranganAkun_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KeteranganAkun.KeyPress
        If e.KeyChar = Chr(13) Then RBYa.Focus()
    End Sub

    Private Sub BtnSimpan2_Click(sender As Object, e As EventArgs) Handles BtnSimpan2.Click
        If TxtKdTarifPPH.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode tarif PPH harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKdTarifPPH.Focus() : Exit Sub
        ElseIf TxtKetTarifPPH.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan tarif PPH harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKetTarifPPH.Focus() : Exit Sub
        ElseIf CmbKlasBarang.SelectedIndex = -1 Then
            MessageBox.Show("Klasifikasi master barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbKlasBarang.Focus() : Exit Sub
        ElseIf CmbJnsSupp.SelectedIndex = -1 Then
            MessageBox.Show("Jenis supplier harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbJnsSupp.Focus() : Exit Sub
        ElseIf CmbTarifPajak2.SelectedIndex = -1 Then
            MessageBox.Show("Tarif pajak harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbTarifPajak2.Focus() : Exit Sub
        ElseIf CmbKlasJasa.SelectedIndex = -1 Then
            MessageBox.Show("Klasifikasi jasa harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbKlasJasa.Focus() : Exit Sub
        ElseIf CmbSubKlasJasa.SelectedIndex = -1 Then
            MessageBox.Show("Sub klasifikasi jasa harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSubKlasJasa.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            If BtnSimpan2.Text = "&Simpan" Then
                SQL = "insert into EMI_Tarif_PPH(kode_perusahaan,kode_tarif_pph,keterangan,klasifikasi_master_barang,kode_jenis_supplier,"
                SQL = SQL & "kode_jasa,kode_sub_jasa,kode_tarif) "
                SQL = SQL & "values('" & KodePerusahaan & "','" & TxtKdTarifPPH.Text.Trim.ToUpper & "','" & TxtKetTarifPPH.Text.Trim.ToUpper & "','"
                SQL = SQL & LKlasBarang(CmbKlasBarang.SelectedIndex) & "','" & LJenisSupp(CmbJnsSupp.SelectedIndex) & "','" & LKlasJasa(CmbKlasJasa.SelectedIndex) & "','"
                SQL = SQL & LSubKlasJasa(CmbSubKlasJasa.SelectedIndex) & "','" & LTarifPajak(CmbTarifPajak2.SelectedIndex) & "')"
            Else
                SQL = "update EMI_Tarif_PPH set keterangan = '" & TxtKetTarifPPH.Text.Trim.ToUpper & "',klasifikasi_master_barang = '" & LKlasBarang(CmbKlasBarang.SelectedIndex) & "',"
                SQL = SQL & "kode_jenis_supplier = '" & LJenisSupp(CmbJnsSupp.SelectedIndex) & "',kode_jasa = '" & LKlasJasa(CmbKlasJasa.SelectedIndex) & "',"
                SQL = SQL & "kode_sub_jasa = '" & LSubKlasJasa(CmbSubKlasJasa.SelectedIndex) & "',kode_tarif = '" & LTarifPajak(CmbTarifPajak2.SelectedIndex) & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_tarif = '" & TxtKdTarifPPH.Text & "'"
            End If
            ExecuteTrans(SQL)

            CloseConn()

            KosongTab2()
            TxtKdTarifPPH.Focus()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKdTarifPPH_Leave(sender As Object, e As EventArgs) Handles TxtKdTarifPPH.Leave
        If TxtKdTarifPPH.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "Select kode_tarif_pph,keterangan,klasifikasi_master_barang,kode_jenis_supplier,kode_jasa,kode_sub_jasa,kode_tarif "
            SQL = SQL & "From EMI_Tarif_PPH "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_tarif_pph = '" & TxtKdTarifPPH.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtKdTarifPPH.Text = Dr("kode_tarif_pph")
                    TxtKetTarifPPH.Text = Dr("keterangan")
                    CmbKlasBarang.SelectedIndex = LKlasBarang.IndexOf(Dr("klasifikasi_master_barang"))
                    CmbJnsSupp.SelectedIndex = LJenisSupp.IndexOf(Dr("kode_jenis_supplier"))
                    CmbKlasJasa.SelectedIndex = LKlasJasa.IndexOf(Dr("kode_jasa"))
                    CmbSubKlasJasa.SelectedIndex = LSubKlasJasa.IndexOf(Dr("kode_sub_jasa"))
                    CmbTarifPajak2.SelectedIndex = LTarifPajak.IndexOf(Dr("kode_tarif"))
                    BtnSimpan2.Text = "&Update" : BtnHapus2.Enabled = True
                Else
                    TxtKetTarifPPH.Text = "" : CmbKlasBarang.SelectedIndex = -1 : CmbJnsSupp.SelectedIndex = -1
                    CmbKlasJasa.SelectedIndex = -1 : CmbSubKlasJasa.SelectedIndex = -1 : CmbTarifPajak2.SelectedIndex = -1
                    BtnSimpan2.Text = "&Simpan" : BtnHapus2.Enabled = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnHapus2_Click(sender As Object, e As EventArgs) Handles BtnHapus2.Click
        If TxtKdTarifPPH.Text.Trim.Length = 0 Then Exit Sub

        Dim Hapus As String = MessageBox.Show("Anda yakin akan hapus data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "delete from EMI_Tarif_PPH where kode_perusahaan = '" & KodePerusahaan & "' and kode_tarif_pph = '" & TxtKdTarifPPH.Text & "'"
                ExecuteTrans(SQL)

                KosongTab2()
                TxtKdTarifPPH.Focus()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtnRefresh2_Click(sender As Object, e As EventArgs) Handles BtnRefresh2.Click
        KosongTab2()
        TxtKdTarifPPH.Focus()
    End Sub

    Private Sub BtnExit2_Click(sender As Object, e As EventArgs) Handles BtnExit2.Click
        Me.Close()
    End Sub

    Private Sub BtnCari2_Click(sender As Object, e As EventArgs) Handles BtnCari2.Click
        If CmbKolom2.SelectedIndex = -1 Then
            MessageBox.Show("Kolom pencarian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbKolom2.Focus() : Exit Sub
        ElseIf TxtValue2.Text.Trim.Length = 0 Then
            MessageBox.Show("Value pencarian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtValue2.Focus() : Exit Sub
        End If

        TampilTab2(LKolom2(CmbKolom2.SelectedIndex), TxtValue2.Text)
    End Sub

    Private Sub TampilTab2(ByVal Kolom As String, ByVal Value As String)
        Try
            OpenConn()

            Lv2.Items.Clear()
            SQL = "select a.kode_tarif_pph,a.keterangan,a.klasifikasi_master_barang,a.kode_tarif,"
            SQL = SQL & "b.keterangan as ketjenissupp,c.keterangan as ketjasa,d.keterangan as ketsubjasa "

            SQL = SQL & "from EMI_Tarif_PPH as a inner join EMI_Master_Jenis_Supplier as b on a.kode_perusahaan = b.kode_perusahaan and "
            SQL = SQL & "a.kode_jenis_supplier = b.kode_jenis_supplier "

            SQL = SQL & "inner join EMI_Master_Jasa as c on a.kode_perusahaan = c.kode_perusahaan and a.kode_jasa = c.kode_jasa "

            SQL = SQL & "inner join EMI_Master_Sub_Jasa as d on a.kode_perusahaan = d.kode_perusahaan and a.kode_sub_jasa = d.kode_sub_jasa "

            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            If Kolom.Trim.Length <> 0 Then
                SQL = SQL & "AND " & Kolom & " like '%" & Trim(Value) & "%' "
            End If
            SQL = SQL & "order by kode_tarif_pph"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv2.Items.Add(Dr("kode_tarif_pph"))
                    Lv.SubItems.Add(Dr("keterangan"))
                    Lv.SubItems.Add(Dr("klasifikasi_master_barang"))
                    Lv.SubItems.Add(Dr("ketjenissupp"))
                    Lv.SubItems.Add(Dr("ketjasa"))
                    Lv.SubItems.Add(Dr("ketsubjasa"))
                    Lv.SubItems.Add(Dr("kode_tarif"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv2_DoubleClick(sender As Object, e As EventArgs) Handles Lv2.DoubleClick
        If Lv2.Items.Count = 0 Then Exit Sub

        TxtKdTarifPPH.Text = Lv2.FocusedItem.Text
        TxtKdTarifPPH_Leave(Lv2, e)
    End Sub

    Private Sub CmbKlasJasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbKlasJasa.SelectedIndexChanged
        Try
            CmbSubKlasJasa.Items.Clear() : LSubKlasJasa.Clear()

            OpenConn()

            SQL = "select kode_sub_jasa,keterangan "
            SQL = SQL & "from emi_master_sub_jasa "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_jasa = '" & LKlasJasa(CmbKlasJasa.SelectedIndex) & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbSubKlasJasa.Items.Add(Dr("keterangan"))
                    LSubKlasJasa.Add(Dr("kode_sub_jasa"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CmbKatPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKatPajak.KeyPress
        If e.KeyChar = Chr(13) Then CmbTarifPajak3.Focus()
    End Sub

    Private Sub CmbTarifPajak3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbTarifPajak3.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan3.Focus()
    End Sub

    Private Sub BtnSimpan3_Click(sender As Object, e As EventArgs) Handles BtnSimpan3.Click
        If CmbKatPajak.SelectedIndex = -1 Then
            MessageBox.Show("Kategori pajak harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbKatPajak.Focus() : Exit Sub
        ElseIf CmbTarifPajak3.SelectedIndex = -1 Then
            MessageBox.Show("Tarif pajak harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbTarifPajak3.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            If BtnSimpan3.Text = "&Simpan" Then
                SQL = "insert into Tarif_Pajak(kode_perusahaan,kode_kategori,kode_tarif) "
                SQL = SQL & "values('" & KodePerusahaan & "','" & LKategori(CmbKatPajak.SelectedIndex) & "','" & LTarif(CmbTarifPajak3.SelectedIndex) & "')"
            Else

            End If
            ExecuteTrans(SQL)

            CloseConn()

            KosongTab3()
            CmbKatPajak.Focus()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cari_Data()
        If CmbKatPajak.SelectedIndex = -1 Then Exit Sub
        If CmbTarifPajak3.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            SQL = "Select a.kode_kategori,b.keterangan as KetKategori,a.kode_tarif,c.keterangan as KetTarif "
            SQL = SQL & "From Tarif_Pajak as a inner join emi_master_kategori_pajak as b on a.kode_perusahaan = b.kode_perusahaan and a.kode_kategori = b.kode_kategori "
            SQL = SQL & "inner join emi_master_pajak as c on a.kode_perusahaan = c.kode_perusahaan and a.kode_tarif = c.kode_tarif "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_kategori = '" & LKategori(CmbKatPajak.SelectedIndex) & "' and "
            SQL = SQL & "a.kode_tarif = '" & LTarif(CmbTarifPajak3.SelectedIndex) & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CmbKatPajak.SelectedIndex = LKategori.IndexOf(Dr("kode_kategori"))
                    CmbTarifPajak3.SelectedIndex = LTarif.IndexOf(Dr("kode_tarif"))
                    BtnSimpan3.Enabled = False : BtnHapus3.Enabled = True
                Else
                    BtnSimpan3.Enabled = True : BtnSimpan3.Text = "&Simpan" : BtnHapus3.Enabled = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CmbKatPajak_Leave(sender As Object, e As EventArgs) Handles CmbKatPajak.Leave
        Cari_Data()
    End Sub

    Private Sub CmbTarifPajak3_Leave(sender As Object, e As EventArgs) Handles CmbTarifPajak3.Leave
        Cari_Data()
    End Sub

    Private Sub BtnHapus3_Click(sender As Object, e As EventArgs) Handles BtnHapus3.Click
        If CmbKatPajak.SelectedIndex = -1 Then Exit Sub
        If CmbTarifPajak3.SelectedIndex = -1 Then Exit Sub

        Dim Hapus As String = MessageBox.Show("Anda yakin akan hapus data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()

                SQL = "delete from Tarif_Pajak where kode_perusahaan = '" & KodePerusahaan & "' and kode_kategori = '" & LKategori(CmbKatPajak.SelectedIndex) & "' and "
                SQL = SQL & "kode_tarif = '" & LTarif(CmbTarifPajak3.SelectedIndex) & "'"
                ExecuteTrans(SQL)

                KosongTab3()
                CmbKatPajak.Focus()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtnRefresh3_Click(sender As Object, e As EventArgs) Handles BtnRefresh3.Click
        KosongTab3()
        CmbKatPajak.Focus()
    End Sub

    Private Sub BtnExit3_Click(sender As Object, e As EventArgs) Handles BtnExit3.Click
        Me.Close()
    End Sub

    Private Sub BtnCari3_Click(sender As Object, e As EventArgs) Handles BtnCari3.Click
        If CmbKolom3.SelectedIndex = -1 Then
            MessageBox.Show("Kolom pencarian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbKolom3.Focus() : Exit Sub
        ElseIf TxtValue3.Text.Trim.Length = 0 Then
            MessageBox.Show("Value pencarian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtValue3.Focus() : Exit Sub
        End If

        TampilTab3(LKolom3(CmbKolom3.SelectedIndex), TxtValue3.Text)
    End Sub

    Private Sub TampilTab3(ByVal Kolom As String, ByVal Value As String)
        Try
            OpenConn()

            Lv3.Items.Clear()
            SQL = "select a.kode_kategori,b.keterangan as KetKategori,a.kode_tarif,c.keterangan as KetTarif "

            SQL = SQL & "from Tarif_Pajak as a inner join EMI_Master_Kategori_Pajak as b on a.kode_perusahaan = b.kode_perusahaan and "
            SQL = SQL & "a.kode_kategori = b.kode_kategori "

            SQL = SQL & "inner join emi_master_pajak as c on a.kode_perusahaan = c.kode_perusahaan and a.kode_tarif = c.kode_tarif "

            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            If Kolom.Trim.Length <> 0 Then
                SQL = SQL & "AND " & Kolom & " like '%" & Trim(Value) & "%' "
            End If
            SQL = SQL & "order by a.kode_kategori,a.kode_tarif"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv3.Items.Add(Dr("ketkategori"))
                    Lv.SubItems.Add(Dr("kettarif"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv3_DoubleClick(sender As Object, e As EventArgs) Handles Lv3.DoubleClick
        If Lv3.Items.Count = 0 Then Exit Sub

        CmbKatPajak.Text = Lv3.FocusedItem.Text
        CmbTarifPajak3.Text = Lv3.FocusedItem.SubItems(1).Text

        Cari_Data()
    End Sub

    Private Sub CmbKolom3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKolom3.KeyPress
        If e.KeyChar = Chr(13) Then TxtValue3.Focus()
    End Sub

    Private Sub TxtValue3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtValue3.KeyPress
        If e.KeyChar = Chr(13) Then BtnCari3_Click(TxtValue3, e)
    End Sub

    Private Sub Txt_KdAkun_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdAkun.TextChanged
        If Txt_KdAkun.Text.Trim.Length = 0 Then
            Lv_Akun.Location = New Point(1015, 122)
            Lv_Akun.Visible = False
            Txt_KeteranganAkun.Text = ""
            Exit Sub
        Else
            If visibleLvAkun Then
                Lv_Akun.Location = New Point(107, 122)
                Lv_Akun.Visible = True
            End If
        End If



        cariAkun(True, Txt_KdAkun.Text)

    End Sub

    Private Sub Txt_KeteranganAkun_TextChanged(sender As Object, e As EventArgs) Handles Txt_KeteranganAkun.TextChanged
        If Txt_KeteranganAkun.Text.Trim.Length = 0 Then
            Lv_Akun.Location = New Point(1015, 122)
            Lv_Akun.Visible = False
            Txt_KdAkun.Text = ""
            Exit Sub
        Else
            If visibleLvAkun Then
                Lv_Akun.Location = New Point(107, 122)
                Lv_Akun.Visible = True
            End If
        End If

        cariAkun(False, Txt_KeteranganAkun.Text)
    End Sub


    Private Sub cariAkun(ByVal isKode As Boolean, ByVal data As String)

        If String.IsNullOrEmpty(data) Then Exit Sub

        Try
            OpenConn()

            Lv_Akun.Items.Clear()
            SQL = "select Kode_Account, Keterangan  "
            SQL = SQL & "from Detail_Account "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            If isKode Then
                SQL = SQL & "and Kode_Account like '%" & data & "%' "
            Else
                SQL = SQL & "and Keterangan like '%" & data & "%' "
            End If
            SQL = SQL & "order by Kode_Account "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Akun.Items.Add(Dr("Kode_Account"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Akun_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Akun.DoubleClick

        If Lv_Akun.Items.Count = 0 Then Exit Sub

        Dim selectedIndex As Integer = Lv_Akun.FocusedItem.Index

        Txt_KdAkun.Text = Lv_Akun.Items(selectedIndex).SubItems(0).Text
        Txt_KeteranganAkun.Text = Lv_Akun.Items(0).SubItems(1).Text

        Lv_Akun.Items.Clear()
        Lv_Akun.Location = New Point(1015, 122)
        Lv_Akun.Visible = False

    End Sub


End Class
Imports System.Text.RegularExpressions

Public Class Jf_Master_Rekrutmen
    Dim Perusahaan_List As New ArrayList
    Dim LGolongan As New ArrayList
    Dim LLevelJabatan As New ArrayList
    Dim LDivisiSubDivisi As New ArrayList
    Dim LKolom As New ArrayList
    Dim LTanggal As New ArrayList
    Dim idlevel As New ArrayList
    Dim iddivisi As New ArrayList

    Private Sub Jf_Master_Karyawan2_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        TxtNamaKaryawan.Focus()
    End Sub

    Private Sub Perusahaan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LvKaryawan.Columns.Add("No.Transaksi", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Tgl Faktur", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("User ID", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Kode Calon", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Alamat", 300, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Telepon", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("HP", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Jenis Kelamin", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Tgl Lahir", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Jenis", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Aktif", 50, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Email", 150, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("NIK", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Pendidikan Terakhir", 120, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("NPWP", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Agama", 120, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Jenis Perayaan", 120, HorizontalAlignment.Left)

        LvKaryawan.Columns.Add("Level Jabatan", 220, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Divisi/Departemen", 220, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Atasan Langsung", 150, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Lokasi", 120, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Perusahaan", 200, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Lokasi Utk Gaji", 120, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Status", 100, HorizontalAlignment.Center)

        LvKaryawan.Columns.Add("Flag OK", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Tanggal OK", 100, HorizontalAlignment.Center)
        LvKaryawan.View = View.Details

        Kosong()
        TxtKodeKaryawan.ReadOnly = True
        TxtNamaKaryawan.Focus()
    End Sub

    Private Sub Kosong()
        Try

            OpenConn()

            LvKaryawan.Items.Clear()

            SQL = "SELECT dbo.HRIS_Rekrutmen_Karyawan.No_Faktur, dbo.HRIS_Rekrutmen_Karyawan.Tanggal, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Jam, dbo.HRIS_Rekrutmen_Karyawan.UserID, dbo.HRIS_Rekrutmen_Karyawan.kode_calon, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Nama, dbo.HRIS_Rekrutmen_Karyawan.Alamat, dbo.HRIS_Rekrutmen_Karyawan.Telepon, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.HP, dbo.HRIS_Rekrutmen_Karyawan.Jenis, dbo.HRIS_Rekrutmen_Karyawan.Aktif, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Kode_Jabatan, dbo.HRIS_Rekrutmen_Karyawan.Email, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Cabang_Untuk_Gaji, dbo.HRIS_Rekrutmen_Karyawan.NPWP, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.NIK, dbo.HRIS_Rekrutmen_Karyawan.Status_Karyawan, dbo.HRIS_Rekrutmen_Karyawan.Jenis_Perayaan, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Lokasi, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.ID_Perusahaan, dbo.HRIS_Rekrutmen_Karyawan.Pendidikan_Terakhir, dbo.HRIS_Rekrutmen_Karyawan.Agama, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Kepala, dbo.HRIS_Rekrutmen_Karyawan.Tgl_Lahir, dbo.HRIS_Rekrutmen_Karyawan.Jenis_Kelamin, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Flag_OK, dbo.HRIS_Rekrutmen_Karyawan.Tanggal_OK, "
            SQL = SQL & "dbo.Perusahaan.Nama AS nama_perusahaan, dbo.HRIS_Level_Jabatan.ID_Level, dbo.HRIS_Level.Keterangan AS KetLevel, "
            SQL = SQL & "dbo.HRIS_Level_Jabatan.ID_Jabatan, dbo.HRIS_Jabatan.Keterangan AS KetJabatan, dbo.HRIS_Level_Jabatan.ID_Golongan_Sub_Golongan, "
            SQL = SQL & "dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan, dbo.HRIS_Golongan.Keterangan AS KetGolongan, dbo.HRIS_Golongan_Sub_Golongan.ID_Sub_Golongan, "
            SQL = SQL & "dbo.HRIS_Sub_Golongan.Keterangan AS KetSubGolongan, dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi, dbo.HRIS_Divisi.Keterangan AS KetDivisi, "
            SQL = SQL & "dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi, dbo.HRIS_Sub_Divisi.Keterangan AS KetSubDivisi "

            SQL = SQL & "FROM dbo.HRIS_Rekrutmen_Karyawan INNER JOIN "
            SQL = SQL & "dbo.Perusahaan ON dbo.HRIS_Rekrutmen_Karyawan.Kode_Perusahaan = dbo.Perusahaan.Kode_Perusahaan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Level_Jabatan ON dbo.HRIS_Rekrutmen_Karyawan.ID_Level_Jabatan = dbo.HRIS_Level_Jabatan.ID_Level_Jabatan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Level ON dbo.HRIS_Level_Jabatan.ID_Level = dbo.HRIS_Level.ID_Level INNER JOIN "
            SQL = SQL & "dbo.HRIS_Jabatan ON dbo.HRIS_Level_Jabatan.ID_Jabatan = dbo.HRIS_Jabatan.ID_Jabatan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Golongan_Sub_Golongan ON dbo.HRIS_Level_Jabatan.ID_Golongan_Sub_Golongan = dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan_Sub_Golongan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan = dbo.HRIS_Golongan.ID_Golongan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Sub_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Sub_Golongan = dbo.HRIS_Sub_Golongan.ID_Sub_Golongan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Divisi_Sub_Divisi ON dbo.HRIS_Rekrutmen_Karyawan.ID_Divisi_Sub_Divisi = dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi_Sub_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi = dbo.HRIS_Divisi.ID_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Sub_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi = dbo.HRIS_Sub_Divisi.ID_Sub_Divisi "

            SQL = SQL & "WHERE HRIS_Rekrutmen_Karyawan.kode_perusahaan = '" & KodePerusahaan & "' and HRIS_Rekrutmen_Karyawan.aktif = 'Y' and "
            SQL = SQL & "HRIS_Rekrutmen_Karyawan.Flag_OK = 'T' "

            SQL = SQL & "ORDER BY HRIS_Rekrutmen_Karyawan.no_faktur,HRIS_Rekrutmen_Karyawan.kode_calon"

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvKaryawan.Items.Add(dr("no_faktur"))
                    Lvw.SubItems.Add(Format(dr("tanggal"), "dd MMM yyyy"))
                    Lvw.SubItems.Add(dr("jam"))
                    Lvw.SubItems.Add(dr("userid"))
                    Lvw.SubItems.Add(dr("kode_calon"))
                    Lvw.SubItems.Add(dr("nama"))
                    Lvw.SubItems.Add(dr("alamat"))
                    Lvw.SubItems.Add(dr("telepon"))
                    Lvw.SubItems.Add(dr("hp"))
                    Lvw.SubItems.Add(dr("jenis_kelamin"))
                    Lvw.SubItems.Add(Format(dr("tgl_lahir"), "dd MMM yyyy"))
                    Lvw.SubItems.Add(dr("jenis"))
                    Lvw.SubItems.Add(dr("aktif"))
                    Lvw.SubItems.Add(dr("email"))
                    Lvw.SubItems.Add(dr("nik"))
                    Lvw.SubItems.Add(dr("pendidikan_terakhir"))
                    Lvw.SubItems.Add(dr("npwp"))
                    Lvw.SubItems.Add(dr("agama"))
                    Lvw.SubItems.Add(dr("jenis_perayaan"))

                    Lvw.SubItems.Add(dr("ketlevel") & " | " & dr("ketjabatan") & " | " & dr("ketgolongan") & dr("ketsubgolongan"))
                    Lvw.SubItems.Add(dr("ketdivisi") & " | " & dr("ketsubdivisi"))
                    Lvw.SubItems.Add(dr("kepala"))
                    Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("nama_perusahaan"))
                    Lvw.SubItems.Add(dr("cabang_untuk_gaji"))
                    Lvw.SubItems.Add(dr("status_karyawan"))

                    Lvw.SubItems.Add(dr("flag_ok"))
                    If IsDBNull(dr("tanggal_ok")) Then
                        Lvw.SubItems.Add("")
                    Else
                        Lvw.SubItems.Add(Format(dr("tanggal_ok"), "dd MM yyyy"))
                    End If
                Loop
            End Using

            CmbJK.Items.Clear()
            CmbJK.Items.Add("Laki-Laki")
            CmbJK.Items.Add("Perempuan")
            CmbJK.SelectedIndex = -1

            CmbJenis.Items.Clear()
            CmbJenis.Items.Add("U")
            CmbJenis.Items.Add("S")
            CmbJenis.Items.Add("D")
            CmbJenis.Items.Add("A")
            CmbJenis.SelectedIndex = -1

            CmbAktif.Items.Clear()
            CmbAktif.Items.Add("Y")
            CmbAktif.Items.Add("T")
            CmbAktif.SelectedIndex = -1

            CmbPerusahaan.Items.Clear()
            Perusahaan_List.Clear()
            SQL = "SELECT ID_Perusahaan FROM HRIS_Perusahaan_Karyawan ORDER BY ID_Perusahaan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Perusahaan_List.Add(Dr("ID_Perusahaan"))
                    CmbPerusahaan.Items.Add(Dr("ID_Perusahaan"))
                Loop
            End Using

            CmbPendidikan.Items.Clear()
            CmbPendidikan.Items.Add("SD")
            CmbPendidikan.Items.Add("SMP")
            CmbPendidikan.Items.Add("SMA / SMK")
            CmbPendidikan.Items.Add("D3")
            CmbPendidikan.Items.Add("D4")
            CmbPendidikan.Items.Add("S1")
            CmbPendidikan.Items.Add("S2")
            CmbPendidikan.SelectedIndex = -1

            CmbPerayaan.Items.Clear()
            SQL = "Select perayaan From HRIS_Perayaan where kode_perusahaan = '" & KodePerusahaan & "' order by perayaan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbPerayaan.Items.Add(dr("perayaan"))
                Loop
            End Using
            CmbPerayaan.SelectedIndex = -1

            CmbAgama.Items.Clear()
            SQL = "Select agama From HRIS_Agama where kode_perusahaan = '" & KodePerusahaan & "' order by Agama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbAgama.Items.Add(dr("agama"))
                Loop
            End Using
            CmbAgama.SelectedIndex = -1

            '=================================================================
            ' Level dan Divisi
            '=================================================================
            CmbLevel.Items.Clear() : LLevelJabatan.Clear()
            idlevel.Clear()

            SQL = "SELECT dbo.HRIS_Level_Jabatan.ID_Level_Jabatan, dbo.HRIS_Level.Keterangan AS KetLevel, dbo.HRIS_Jabatan.Keterangan AS KetJabatan, "
            SQL = SQL & "dbo.HRIS_Golongan.Keterangan AS KetGolongan, dbo.HRIS_Sub_Golongan.Keterangan AS KetSubGolongan "

            SQL = SQL & "FROM dbo.HRIS_Level_Jabatan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Level ON dbo.HRIS_Level_Jabatan.ID_Level = dbo.HRIS_Level.ID_Level INNER JOIN "
            SQL = SQL & "dbo.HRIS_Jabatan ON dbo.HRIS_Level_Jabatan.ID_Jabatan = dbo.HRIS_Jabatan.ID_Jabatan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Golongan_Sub_Golongan ON dbo.HRIS_Level_Jabatan.ID_Golongan_Sub_Golongan = dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan_Sub_Golongan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan = dbo.HRIS_Golongan.ID_Golongan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Sub_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Sub_Golongan = dbo.HRIS_Sub_Golongan.ID_Sub_Golongan "

            SQL = SQL & "ORDER BY dbo.HRIS_Level.ID_Level, dbo.HRIS_Jabatan.ID_Jabatan, dbo.HRIS_Golongan.ID_Golongan, dbo.HRIS_Sub_Golongan.ID_Sub_Golongan"

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    LLevelJabatan.Add(Dr("id_level_jabatan"))
                    idlevel.Add(Dr("id_level_jabatan"))
                    CmbLevel.Items.Add(Dr("ketlevel") & " | " & Dr("ketjabatan") & " | " & Dr("ketgolongan") & Dr("ketsubgolongan"))
                Loop
            End Using
            CmbLevel.SelectedIndex = -1

            '=================================================================

            CmbDivisi.Items.Clear() : LDivisiSubDivisi.Clear()
            iddivisi.Clear()

            SQL = "SELECT dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi_Sub_Divisi, dbo.HRIS_Divisi.Keterangan AS KetDivisi, dbo.HRIS_Sub_Divisi.Keterangan AS KetSubDivisi "

            SQL = SQL & "FROM dbo.HRIS_Divisi_Sub_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi = dbo.HRIS_Divisi.ID_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Sub_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi = dbo.HRIS_Sub_Divisi.ID_Sub_Divisi "

            SQL = SQL & "ORDER BY dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi, dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi"

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    LDivisiSubDivisi.Add(Dr("id_divisi_sub_divisi"))
                    iddivisi.Add(Dr("id_divisi_sub_divisi"))
                    CmbDivisi.Items.Add(Dr("ketdivisi") & " | " & Dr("ketsubdivisi"))
                Loop
            End Using
            CmbDivisi.SelectedIndex = -1
            '=================================================================

            CmbLokasi.Items.Clear()
            SQL = "Select kode_stock_owner From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            CmbLokasi.SelectedIndex = -1

            CmbLokasiSlipGaji.Items.Clear()
            SQL = "Select kode_stock_owner From lokasi_penggajian where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasiSlipGaji.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            CmbLokasiSlipGaji.SelectedIndex = -1

            CmbStatusKaryawan.Items.Clear()
            CmbStatusKaryawan.Items.Add("TK/0")
            CmbStatusKaryawan.Items.Add("K/0")
            CmbStatusKaryawan.Items.Add("K/1")
            CmbStatusKaryawan.Items.Add("K/2")
            CmbStatusKaryawan.Items.Add("K/3")
            CmbStatusKaryawan.SelectedIndex = -1

            ChkTanggal.Checked = False : CmbTanggal.Enabled = False
            Tgl1.Enabled = False : Tgl1.Value = CDate(fmenu.ToolStripStatusLabel3.Text)
            Tgl2.Enabled = False : Tgl2.Value = CDate(fmenu.ToolStripStatusLabel3.Text)

            CmbTanggal.Items.Clear() : LTanggal.Clear()
            CmbTanggal.Items.Add("Tgl Faktur") : LTanggal.Add("Tanggal")
            CmbTanggal.Items.Add("Tgl Lahir") : LTanggal.Add("Tgl_Lahir")
            CmbTanggal.Items.Add("Tanggal OK") : LTanggal.Add("Tanggal_OK")
            CmbTanggal.SelectedIndex = -1

            ChkParameter.Checked = False : CmbKolom.Enabled = False
            TxtValue.Enabled = False

            CmbKolom.Items.Clear() : LKolom.Clear()
            CmbKolom.Items.Add("No.Transaksi") : LKolom.Add("No_Faktur")
            CmbKolom.Items.Add("Kode Calon") : LKolom.Add("kode_calon")
            CmbKolom.Items.Add("Nama") : LKolom.Add("Nama")
            CmbKolom.Items.Add("Alamat") : LKolom.Add("Alamat")
            CmbKolom.Items.Add("Telepon") : LKolom.Add("Telepon")
            CmbKolom.Items.Add("HP") : LKolom.Add("HP")
            CmbKolom.Items.Add("Jenis Kelamin") : LKolom.Add("Jenis_Kelamin")
            CmbKolom.Items.Add("Jenis") : LKolom.Add("Jenis")
            CmbKolom.Items.Add("Aktif") : LKolom.Add("Aktif")
            CmbKolom.Items.Add("Email") : LKolom.Add("Email")
            CmbKolom.Items.Add("NIK") : LKolom.Add("NIK")
            CmbKolom.Items.Add("Pendidikan Terakhir") : LKolom.Add("Pendidikan_Terakhir")
            CmbKolom.Items.Add("NPWP") : LKolom.Add("NPWP")
            CmbKolom.Items.Add("Agama") : LKolom.Add("Agama")
            CmbKolom.Items.Add("Jenis Perayaan") : LKolom.Add("Jenis_Perayaan")
            CmbKolom.Items.Add("Lokasi") : LKolom.Add("Lokasi")
            CmbKolom.Items.Add("Lokasi Utk Gaji") : LKolom.Add("Cabang_Untuk_Gaji")
            CmbKolom.Items.Add("Status") : LKolom.Add("Status_Karyawan")
            CmbKolom.Items.Add("Flag OK") : LKolom.Add("Flag_OK")
            CmbKolom.SelectedIndex = -1

            TglFaktur.Value = CDate(fmenu.ToolStripStatusLabel3.Text)
            TxtNoFaktur.Text = "R" & Format(TglFaktur.Value, "yyMMdd") & _
                                Get_Last_Number("HRIS_Rekrutmen_Karyawan", "No_Faktur", 3, _
                                                                "Kode_Perusahaan", KodePerusahaan, "and", _
                                                                "left(no_faktur,7)", "R" & Format(TglFaktur.Value, "yyMMdd"))

            TxtKodeKaryawan.Text = "C" & Format(TglFaktur.Value, "yyMMdd") & _
                                Get_Last_Number("HRIS_Rekrutmen_Karyawan", "Kode_Calon", 3, _
                                                                "Kode_Perusahaan", KodePerusahaan, "and", _
                                                                "left(Kode_Calon,7)", "C" & Format(TglFaktur.Value, "yyMMdd"))

            TxtNamaKaryawan.Text = "" : TxtAlamat.Text = "" : TxtTelepon.Text = "" : TxtHp.Text = ""
            TglLahir.Value = CDate(fmenu.ToolStripStatusLabel3.Text)
            TxtEmail.Text = ""
            TxtNIK.Text = "" : TxtNPWP.Text = ""
            TxtKepala.Text = "" : TxtValue.Text = ""

            TabControl1.SelectedTab = TabPage1

            BtSimpan.Text = "&Simpan" : BtHapus.Enabled = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub BtExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtExit.Click
        Me.Close()
    End Sub

    Private Sub BtRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRefresh.Click
        Kosong()
        TxtKodeKaryawan.Focus()
    End Sub

    Private Sub TxtKodeKaryawan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtKodeKaryawan.KeyPress
        If e.KeyChar = Chr(13) Then TxtNamaKaryawan.Focus()
    End Sub

    Private Sub TxtNamaKaryawan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNamaKaryawan.KeyPress
        If e.KeyChar = Chr(13) Then TxtAlamat.Focus()
    End Sub

    Private Sub TxtAlamat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtAlamat.KeyPress
        If e.KeyChar = Chr(13) Then TxtTelepon.Focus()
    End Sub

    Private Sub TxtTelepon_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTelepon.KeyPress
        If e.KeyChar = Chr(13) Then TxtHp.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TxtHp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtHp.KeyPress
        If e.KeyChar = Chr(13) Then CmbJK.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub CmbJK_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbJK.KeyPress
        If e.KeyChar = Chr(13) Then TglLahir.Focus()
    End Sub

    Private Sub TglLahir_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TglLahir.KeyPress
        If e.KeyChar = Chr(13) Then CmbJenis.Focus()
    End Sub

    Private Sub CmbJenis_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbJenis.KeyPress
        If e.KeyChar = Chr(13) Then CmbAktif.Focus()
    End Sub

    Private Sub CmbAktif_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbAktif.KeyPress
        If e.KeyChar = Chr(13) Then TxtEmail.Focus()
    End Sub

    Private Sub TxtEmail_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtEmail.KeyPress
        If e.KeyChar = Chr(13) Then TxtNIK.Focus()
    End Sub

    Private Sub TxtNIK_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNIK.KeyPress
        If e.KeyChar = Chr(13) Then CmbPendidikan.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub CmbPendidikan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbPendidikan.KeyPress
        If e.KeyChar = Chr(13) Then TxtNPWP.Focus()
    End Sub

    Private Sub TxtNPWP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNPWP.KeyPress
        If e.KeyChar = Chr(13) Then CmbAgama.Focus()
    End Sub

    Private Sub CmbAgama_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbAgama.KeyPress
        If e.KeyChar = Chr(13) Then CmbPerayaan.Focus()
    End Sub

    Private Sub CmbPerayaan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbPerayaan.KeyPress
        If e.KeyChar = Chr(13) Then
            TabControl1.SelectedTab = TabPage2
            CmbLevel.Focus()
        End If
    End Sub

    Private Sub CmbLevel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbLevel.KeyPress
        If e.KeyChar = Chr(13) Then CmbDivisi.Focus()
    End Sub

    Private Sub CmbDivisi_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbDivisi.KeyPress
        If e.KeyChar = Chr(13) Then TxtKepala.Focus()
    End Sub

    Private Sub TxtKepala_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtKepala.KeyPress
        If e.KeyChar = Chr(13) Then CmbLokasi.Focus()
    End Sub

    Private Sub CmbLokasi_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbLokasi.KeyPress
        If e.KeyChar = Chr(13) Then CmbPerusahaan.Focus()
    End Sub

    Private Sub CmbPerusahaan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbPerusahaan.KeyPress
        If e.KeyChar = Chr(13) Then CmbLokasiSlipGaji.Focus()
    End Sub

    Private Sub CmbLokasiSlipGaji_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbLokasiSlipGaji.KeyPress
        If e.KeyChar = Chr(13) Then CmbStatusKaryawan.Focus()
    End Sub

    Private Sub CmbStatusKaryawan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbStatusKaryawan.KeyPress
        If e.KeyChar = Chr(13) Then BtSimpan.Focus()
    End Sub

    Private Sub TxtNoFaktur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNoFaktur.KeyPress
        If e.KeyChar = Chr(13) Then TxtKodeKaryawan.Focus()
    End Sub

    Private Sub TxtNoFaktur_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNoFaktur.Leave
        If TxtNoFaktur.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "Select * From HRIS_Rekrutmen_Karyawan "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtNoFaktur.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TglFaktur.Value = Dr("tanggal")
                    TxtNoFaktur.Text = Dr("no_faktur")
                    TxtKodeKaryawan.Text = Dr("kode_calon")
                    TxtNamaKaryawan.Text = Dr("Nama")
                    TxtAlamat.Text = Dr("alamat")
                    TxtTelepon.Text = Dr("telepon")
                    TxtHp.Text = Dr("HP")
                    TglLahir.Value = Dr("tgl_lahir")
                    CmbJK.Text = Dr("jenis_kelamin")
                    CmbJenis.Text = Dr("jenis")
                    CmbAktif.Text = Dr("aktif")
                    TxtEmail.Text = Dr("email")
                    TxtNIK.Text = Dr("NIK")
                    TxtNPWP.Text = Dr("npwp")
                    CmbPerusahaan.SelectedIndex = Perusahaan_List.IndexOf(Dr("id_perusahaan"))
                    CmbPerayaan.Text = Dr("jenis_perayaan")
                    CmbPendidikan.Text = Dr("pendidikan_terakhir")
                    CmbAgama.Text = Dr("agama")
                    CmbLevel.SelectedIndex = LLevelJabatan.IndexOf(Dr("id_level_jabatan"))
                    CmbDivisi.SelectedIndex = LDivisiSubDivisi.IndexOf(Dr("id_divisi_sub_divisi"))
                    CmbLokasi.Text = Dr("lokasi")
                    CmbLokasiSlipGaji.Text = Dr("cabang_untuk_gaji")
                    CmbStatusKaryawan.Text = Dr("status_karyawan")
                    TxtKepala.Text = Dr("kepala")

                    If Dr("flag_ok") = "Y" Then
                        MessageBox.Show("Data yang anda cari sudah di validasi, " & Chr(13) & _
                                        "proses tidak dapat dilanjutkan!", "Transaksi Rekrutmen Karyawan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Kosong()
                        TxtKodeKaryawan.Focus()
                        Exit Sub
                    End If

                    BtSimpan.Text = "&Update" : BtHapus.Enabled = True
                Else
                    TxtNamaKaryawan.Text = "" : TxtAlamat.Text = "" : TxtTelepon.Text = "" : TxtHp.Text = ""
                    TglLahir.Value = CDate(fmenu.ToolStripStatusLabel3.Text)
                    CmbJK.SelectedIndex = -1 : CmbJenis.SelectedIndex = -1 : CmbAktif.SelectedIndex = -1
                    TxtEmail.Text = ""
                    TxtNIK.Text = "" : TxtNPWP.Text = ""
                    CmbPerusahaan.SelectedIndex = -1 : CmbPerayaan.SelectedIndex = -1 : CmbPendidikan.SelectedIndex = -1
                    CmbAgama.SelectedIndex = -1 : CmbLevel.SelectedIndex = -1 : CmbDivisi.SelectedIndex = -1
                    CmbLokasi.SelectedIndex = -1 : CmbLokasiSlipGaji.SelectedIndex = -1 : CmbStatusKaryawan.SelectedIndex = -1
                    TxtKepala.Text = "" : TxtValue.Text = ""

                    BtSimpan.Text = "&Simpan" : BtHapus.Enabled = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSimpan.Click
        Dim emailPattern As String = "^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$"

        If TxtKodeKaryawan.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Calon Karyawan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            TxtKodeKaryawan.Focus() : Exit Sub
        ElseIf TxtNamaKaryawan.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            TxtNamaKaryawan.Focus() : Exit Sub
        ElseIf TxtAlamat.Text.Trim.Length = 0 Then
            MessageBox.Show("Alamat harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            TxtAlamat.Focus() : Exit Sub
        ElseIf TxtTelepon.Text.Trim.Length = 0 Then
            MessageBox.Show("Telepon harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            TxtTelepon.Focus() : Exit Sub
        ElseIf TxtHp.Text.Trim.Length = 0 Then
            MessageBox.Show("HP harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            TxtHp.Focus() : Exit Sub
        ElseIf CmbJK.SelectedIndex = -1 Then
            MessageBox.Show("Jenis kelamin harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            CmbJK.Focus() : Exit Sub
        ElseIf CmbJenis.SelectedIndex = -1 Then
            MessageBox.Show("Jenis harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            CmbJenis.Focus() : Exit Sub
        ElseIf CmbAktif.SelectedIndex = -1 Then
            MessageBox.Show("Status aktif harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            CmbAktif.Focus() : Exit Sub
        ElseIf TxtEmail.Text.Trim.Length = 0 Then
            MessageBox.Show("Email harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            TxtEmail.Focus() : Exit Sub
        ElseIf TxtNIK.Text.Trim.Length = 0 Then
            MessageBox.Show("NIK harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            TxtNIK.Focus() : Exit Sub
        ElseIf CmbPendidikan.SelectedIndex = -1 Then
            MessageBox.Show("Pendidikan terakhir harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            CmbPendidikan.Focus() : Exit Sub
        ElseIf TxtNPWP.Text.Trim.Length = 0 Then
            MessageBox.Show("NPWP harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            TxtNPWP.Focus() : Exit Sub
        ElseIf CmbAgama.SelectedIndex = -1 Then
            MessageBox.Show("Agama harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            CmbAgama.Focus() : Exit Sub
        ElseIf CmbPerayaan.SelectedIndex = -1 Then
            MessageBox.Show("Jenis perayaan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage1
            CmbPerayaan.Focus() : Exit Sub
        ElseIf CmbLevel.SelectedIndex = -1 Then
            MessageBox.Show("Level jabatan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage2
            CmbLevel.Focus() : Exit Sub
        ElseIf CmbDivisi.SelectedIndex = -1 Then
            MessageBox.Show("Divisi/Departemen harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage2
            CmbDivisi.Focus() : Exit Sub
        ElseIf TxtKepala.Text.Trim.Length = 0 Then
            MessageBox.Show("Atasan langsung/kepala harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage2
            TxtKepala.Focus() : Exit Sub
        ElseIf CmbLokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage2
            CmbLokasi.Focus() : Exit Sub
        ElseIf CmbPerusahaan.SelectedIndex = -1 Then
            MessageBox.Show("Perusahaan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage2
            CmbPerusahaan.Focus() : Exit Sub
        ElseIf CmbLokasiSlipGaji.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi slip gaji harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage2
            CmbLokasiSlipGaji.Focus() : Exit Sub
        ElseIf CmbStatusKaryawan.SelectedIndex = -1 Then
            MessageBox.Show("Status calon karyawan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage2
            CmbStatusKaryawan.Focus() : Exit Sub
        End If

        If TxtEmail.Text.Trim <> "-" Then
            If Regex.IsMatch(TxtEmail.Text.Trim, emailPattern) = False Then
                MessageBox.Show("Format email yang dimasukkan salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtEmail.Focus() : Exit Sub
            End If
        End If

        Dim Tanya As String = MessageBox.Show("Anda yakin akan simpan data ini?", "Transaksi Rekrutmen Karyawan", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Tanya = vbNo Then Exit Sub

        Try
            OpenConn()
            OpenConnSQL()
            Cmd.Transaction = Cn.BeginTransaction
            CmdSQL.Transaction = CnSQL.BeginTransaction

            If BtSimpan.Text = "&Simpan" Then

                TxtNoFaktur.Text = "R" & Format(TglFaktur.Value, "yyMMdd") & _
                    Get_Last_Number("HRIS_Rekrutmen_Karyawan", "No_Faktur", 3, _
                                        "Kode_Perusahaan", KodePerusahaan, "and", _
                                        "left(no_faktur,7)", "R" & Format(TglFaktur.Value, "yyMMdd"))

                TxtKodeKaryawan.Text = "C" & Format(TglFaktur.Value, "yyMMdd") & _
                     Get_Last_Number("HRIS_Rekrutmen_Karyawan", "Kode_Calon", 3, _
                                                     "Kode_Perusahaan", KodePerusahaan, "and", _
                                                   "left(Kode_Calon,7)", "C" & Format(TglFaktur.Value, "yyMMdd"))

                SQL = "Insert Into HRIS_Rekrutmen_Karyawan(Kode_Perusahaan,no_faktur,tanggal,jam,userid,kode_calon,nama,Alamat,Telepon,HP,tgl_lahir,"
                SQL = SQL & "jenis_kelamin,jenis,aktif,email,nik,npwp,id_perusahaan,jenis_perayaan,"
                SQL = SQL & "Pendidikan_terakhir,agama,id_level_jabatan,id_divisi_sub_divisi,lokasi,cabang_untuk_gaji,"
                SQL = SQL & "status_karyawan,kepala,flag_ok,flag_ok2)"
                SQL = SQL & "Values('" & KodePerusahaan & "','" & TxtNoFaktur.Text.Trim & "','" & Format(TglFaktur.Value, "yyyy-MM-dd") & "','"
                SQL = SQL & Format(Now, "HH:mm") & "','" & UserID & "','" & TxtKodeKaryawan.Text.Trim & "','" & TxtNamaKaryawan.Text.Trim & "','"
                SQL = SQL & TxtAlamat.Text.Trim & "','" & TxtTelepon.Text.Trim & "','" & TxtHp.Text.Trim & "','"
                SQL = SQL & Format(TglLahir.Value, "yyyy-MM-dd") & "','" & CmbJK.Text & "','" & CmbJenis.Text & "','" & CmbAktif.Text & "','"
                SQL = SQL & TxtEmail.Text.Trim & "','" & TxtNIK.Text.Trim & "','"
                SQL = SQL & TxtNPWP.Text.Trim & "','" & Perusahaan_List(CmbPerusahaan.SelectedIndex) & "','"
                SQL = SQL & CmbPerayaan.Text & "','" & CmbPendidikan.Text & "','" & CmbAgama.Text & "','"
                SQL = SQL & LLevelJabatan(CmbLevel.SelectedIndex) & "','" & LDivisiSubDivisi(CmbDivisi.SelectedIndex) & "','"
                SQL = SQL & CmbLokasi.Text & "','" & CmbLokasiSlipGaji.Text & "','"
                SQL = SQL & CmbStatusKaryawan.Text & "','" & TxtKepala.Text.Trim & "','"
                SQL = SQL & "T','T')"
                ExecuteTrans(SQL)

                'SQL = "select UserID_Approval,Level_Approval,ID_Level_Jabatan,ID_Divisi_Sub_Divisi from HRIS_UserID_Approval_Karyawan "
                'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and ID_Level_Jabatan = '" & LLevelJabatan.Item(CmbLevel.SelectedIndex) & "' "
                'SQL = SQL & "and ID_Divisi_Sub_Divisi = '" & LDivisiSubDivisi.Item(CmbDivisi.SelectedIndex) & "' order by Level_Approval "
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then
                '            For a As Integer = 0 To .Rows.Count - 1
                '                SQL = "insert into HRIS_Rekrutmen_Karyawan_Approval(Kode_Perusahaan,No_Faktur,UserID_Approval,ID_Level_Jabatan,"
                '                SQL = SQL & "ID_Divisi_Sub_Divisi,Level_Approval) values('" & KodePerusahaan & "','" & TxtNoFaktur.Text.Trim & "',"
                '                SQL = SQL & "'" & .Rows(a).Item("UserID_Approval") & "','" & .Rows(a).Item("ID_Level_Jabatan") & "',"
                '                SQL = SQL & "'" & .Rows(a).Item("ID_Divisi_Sub_Divisi") & "','" & .Rows(a).Item("Level_Approval") & "')"
                '                ExecuteTrans(SQL)
                '            Next
                '        Else
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("user approval untuk divisi atau jabatan ini belum diinput!", Judul, MessageBoxButtons.OK)
                '            Exit Sub
                '        End If
                '    End With
                'End Using

                Dim hasil As Integer = Approval_Hierarchy2("Approval Rekrutmen", idlevel.Item(CmbLevel.SelectedIndex), iddivisi.Item(CmbDivisi.SelectedIndex))

                If hasil = 0 Then
                    CloseTrans()
                    CloseTransSQL()
                    CloseConn()
                    CloseConnSQL()
                    MessageBox.Show("Data approval tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                End If
                Dim rand As New Random
                Dim Kd_unik As String = ""
                Kd_unik = Format(CDate(fmenu.ToolStripStatusLabel3.Text), "Mdd") & Format(rand.Next(0, 100000), "00000")

                Dim level As Integer = 1
                For a As Integer = 0 To Data_User_App2.Count - 1
                    Dim fHp As String = ""
                    Dim fpin As String = ""

                    SQL = "select HP,pin,userid from Users where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & Data_User_App2.Item(a) & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            fHp = Dr("HP")
                            fpin = Dr("Pin")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseTransSQL()
                            CloseConn()
                            CloseConnSQL()
                            MessageBox.Show("user tidak dapat ditemukan ...!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    'TABLE HARUS MIRIP DENGAN HRIS_Transaksi_Karyawan_Approval

                    SQLSQL = "insert into HRIS_Transaksi_Rekrutmen_Approval(No_Faktur,User_ID,ID_Level,"
                    SQLSQL = SQLSQL & "ID_Divisi,Level_Hierarchy,pin,kode_unik) values('" & TxtNoFaktur.Text.Trim & "',"
                    SQLSQL = SQLSQL & "'" & Data_User_App2.Item(a) & "','" & LLevelJabatan.Item(CmbLevel.SelectedIndex) & "',"
                    SQLSQL = SQLSQL & "'" & LDivisiSubDivisi.Item(CmbDivisi.SelectedIndex) & "','" & level & "','" & fpin & "','" & Kd_unik & "')"
                    ExecuteTransSQL(SQLSQL)

                    level = level + 1
                Next
            Else
                SQL = "Update HRIS_Rekrutmen_Karyawan Set userid = '" & UserID & "', kode_calon = '" & TxtKodeKaryawan.Text.Trim & "',"
                SQL = SQL & "nama = '" & TxtNamaKaryawan.Text.Trim & "',Alamat = '" & TxtAlamat.Text.Trim & "',"
                SQL = SQL & "telepon = '" & TxtTelepon.Text & "',hp = '" & TxtHp.Text & "',tgl_lahir = '" & Format(TglLahir.Value, "yyyy-MM-dd") & "',"
                SQL = SQL & "jenis_kelamin = '" & CmbJK.Text & "',jenis = '" & CmbJenis.Text & "',aktif = '" & CmbAktif.Text & "',"
                SQL = SQL & "email = '" & TxtEmail.Text.Trim & "',nik = '" & TxtNIK.Text.Trim & "',"
                SQL = SQL & "npwp = '" & TxtNPWP.Text.Trim & "',id_perusahaan = '" & Perusahaan_List.Item(CmbPerusahaan.SelectedIndex) & "',"
                SQL = SQL & "jenis_perayaan = '" & CmbPerayaan.Text & "',pendidikan_terakhir = '" & CmbPendidikan.Text & "',"
                SQL = SQL & "agama = '" & CmbAgama.Text & "',id_level_jabatan = '" & LLevelJabatan(CmbLevel.SelectedIndex) & "',"
                SQL = SQL & "id_divisi_sub_divisi = '" & LDivisiSubDivisi(CmbDivisi.SelectedIndex) & "',lokasi = '" & CmbLokasi.Text & "',"
                SQL = SQL & "cabang_untuk_gaji = '" & CmbLokasiSlipGaji.Text & "',status_karyawan = '" & CmbStatusKaryawan.Text & "',"
                SQL = SQL & "kepala = '" & TxtKepala.Text.Trim & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtNoFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CmdSQL.Transaction.Commit()
            CloseConn()
            CloseConnSQL()
        Catch ex As Exception
            CloseTrans()
            CloseTransSQL()
            CloseConn()
            CloseConnSQL()
            MessageBox.Show(ex.Message)
        End Try
        Kosong()
        TxtKodeKaryawan.Focus()
    End Sub


    Private Sub BtHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtHapus.Click
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                ExecuteTrans("Delete From HRIS_Rekrutmen_Karyawan where Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtNoFaktur.Text & "'")

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        Kosong()
        TxtKodeKaryawan.Focus()
    End Sub

    Private Sub CmbKolom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbKolom.KeyPress
        If e.KeyChar = Chr(13) Then TxtValue.Focus()
    End Sub

    Private Sub TxtValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtValue.KeyPress
        If e.KeyChar = Chr(13) Then BtCari_Click(TxtValue, e)
    End Sub

    Private Sub BtCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCari.Click
        If ChkTanggal.Checked = False And ChkParameter.Checked = False Then Exit Sub

        If ChkTanggal.Checked Then
            If CmbTanggal.SelectedIndex = -1 Then
                MessageBox.Show("Parameter tanggal harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbTanggal.Focus() : Exit Sub
            End If
        End If

        If ChkParameter.Checked Then
            If CmbKolom.SelectedIndex = -1 Then
                MessageBox.Show("Parameter pencarian harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbKolom.Focus() : Exit Sub
            ElseIf TxtValue.Text.Trim.Length = 0 Then
                MessageBox.Show("Value pencarian harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtValue.Focus() : Exit Sub
            End If
        End If

        OpenConn()

        LvKaryawan.Items.Clear()

        SQL = "SELECT dbo.HRIS_Rekrutmen_Karyawan.Kode_Perusahaan, dbo.HRIS_Rekrutmen_Karyawan.No_Faktur, dbo.HRIS_Rekrutmen_Karyawan.Tanggal, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Jam, dbo.HRIS_Rekrutmen_Karyawan.UserID, dbo.HRIS_Rekrutmen_Karyawan.kode_calon, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Nama, dbo.HRIS_Rekrutmen_Karyawan.Alamat, dbo.HRIS_Rekrutmen_Karyawan.Telepon, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.HP, dbo.HRIS_Rekrutmen_Karyawan.Jenis, dbo.HRIS_Rekrutmen_Karyawan.Aktif, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Kode_Jabatan, dbo.HRIS_Rekrutmen_Karyawan.Email, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Cabang_Untuk_Gaji, dbo.HRIS_Rekrutmen_Karyawan.NPWP, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.NIK, dbo.HRIS_Rekrutmen_Karyawan.Status_Karyawan, dbo.HRIS_Rekrutmen_Karyawan.Jenis_Perayaan, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Lokasi, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.ID_Perusahaan, dbo.HRIS_Rekrutmen_Karyawan.Pendidikan_Terakhir, dbo.HRIS_Rekrutmen_Karyawan.Agama, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Kepala, dbo.HRIS_Rekrutmen_Karyawan.Tgl_Lahir, dbo.HRIS_Rekrutmen_Karyawan.Jenis_Kelamin, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Flag_OK, dbo.HRIS_Rekrutmen_Karyawan.Tanggal_OK, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.ID_Level_Jabatan, dbo.HRIS_Rekrutmen_Karyawan.ID_Divisi_Sub_Divisi, "
        SQL = SQL & "dbo.Perusahaan.Nama AS nama_perusahaan, dbo.HRIS_Level_Jabatan.ID_Level, dbo.HRIS_Level.Keterangan AS KetLevel, "
        SQL = SQL & "dbo.HRIS_Level_Jabatan.ID_Jabatan, dbo.HRIS_Jabatan.Keterangan AS KetJabatan, dbo.HRIS_Level_Jabatan.ID_Golongan_Sub_Golongan, "
        SQL = SQL & "dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan, dbo.HRIS_Golongan.Keterangan AS KetGolongan, dbo.HRIS_Golongan_Sub_Golongan.ID_Sub_Golongan, "
        SQL = SQL & "dbo.HRIS_Sub_Golongan.Keterangan AS KetSubGolongan, dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi, dbo.HRIS_Divisi.Keterangan AS KetDivisi, "
        SQL = SQL & "dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi, dbo.HRIS_Sub_Divisi.Keterangan AS KetSubDivisi "

        SQL = SQL & "FROM dbo.HRIS_Rekrutmen_Karyawan INNER JOIN "
        SQL = SQL & "dbo.Perusahaan ON dbo.HRIS_Rekrutmen_Karyawan.Kode_Perusahaan = dbo.Perusahaan.Kode_Perusahaan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Level_Jabatan ON dbo.HRIS_Rekrutmen_Karyawan.ID_Level_Jabatan = dbo.HRIS_Level_Jabatan.ID_Level_Jabatan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Level ON dbo.HRIS_Level_Jabatan.ID_Level = dbo.HRIS_Level.ID_Level INNER JOIN "
        SQL = SQL & "dbo.HRIS_Jabatan ON dbo.HRIS_Level_Jabatan.ID_Jabatan = dbo.HRIS_Jabatan.ID_Jabatan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Golongan_Sub_Golongan ON dbo.HRIS_Level_Jabatan.ID_Golongan_Sub_Golongan = dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan_Sub_Golongan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan = dbo.HRIS_Golongan.ID_Golongan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Sub_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Sub_Golongan = dbo.HRIS_Sub_Golongan.ID_Sub_Golongan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Divisi_Sub_Divisi ON dbo.HRIS_Rekrutmen_Karyawan.ID_Divisi_Sub_Divisi = dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi_Sub_Divisi INNER JOIN "
        SQL = SQL & "dbo.HRIS_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi = dbo.HRIS_Divisi.ID_Divisi INNER JOIN "
        SQL = SQL & "dbo.HRIS_Sub_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi = dbo.HRIS_Sub_Divisi.ID_Sub_Divisi "

        SQL = SQL & "WHERE HRIS_Rekrutmen_Karyawan.kode_perusahaan = '" & KodePerusahaan & "' and HRIS_Rekrutmen_Karyawan.flag_ok = 'T' "

        If ChkTanggal.Checked Then
            SQL = SQL & " and HRIS_Rekrutmen_Karyawan." & LTanggal(CmbTanggal.SelectedIndex) & " between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
        End If

        If ChkParameter.Checked Then
            SQL = SQL & " and HRIS_Rekrutmen_Karyawan." & LKolom(CmbKolom.SelectedIndex) & " like '%" & TxtValue.Text & "%' "
        End If

        SQL = SQL & "ORDER BY HRIS_Rekrutmen_Karyawan.no_faktur,HRIS_Rekrutmen_Karyawan.kode_calon"
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                Dim Lvw As ListViewItem
                Lvw = LvKaryawan.Items.Add(Dr("no_faktur"))
                Lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                Lvw.SubItems.Add(Dr("jam"))
                Lvw.SubItems.Add(Dr("userid"))
                Lvw.SubItems.Add(Dr("kode_calon"))
                Lvw.SubItems.Add(Dr("nama"))
                Lvw.SubItems.Add(Dr("alamat"))
                Lvw.SubItems.Add(Dr("telepon"))
                Lvw.SubItems.Add(Dr("hp"))
                Lvw.SubItems.Add(Dr("jenis_kelamin"))
                Lvw.SubItems.Add(Format(Dr("tgl_lahir"), "dd MMM yyyy"))
                Lvw.SubItems.Add(Dr("jenis"))
                Lvw.SubItems.Add(Dr("aktif"))
                Lvw.SubItems.Add(Dr("email"))
                Lvw.SubItems.Add(Dr("nik"))
                Lvw.SubItems.Add(Dr("pendidikan_terakhir"))
                Lvw.SubItems.Add(Dr("npwp"))
                Lvw.SubItems.Add(Dr("agama"))
                Lvw.SubItems.Add(Dr("jenis_perayaan"))

                Lvw.SubItems.Add(Dr("ketlevel") & " | " & Dr("ketjabatan") & " | " & Dr("ketgolongan") & Dr("ketsubgolongan"))
                Lvw.SubItems.Add(Dr("ketdivisi") & " | " & Dr("ketsubdivisi"))
                Lvw.SubItems.Add(Dr("kepala"))
                Lvw.SubItems.Add(Dr("lokasi"))
                Lvw.SubItems.Add(Dr("nama_perusahaan"))
                Lvw.SubItems.Add(Dr("cabang_untuk_gaji"))
                Lvw.SubItems.Add(Dr("status_karyawan"))

                Lvw.SubItems.Add(Dr("flag_ok"))
                If IsDBNull(Dr("tanggal_ok")) Then
                    Lvw.SubItems.Add("")
                Else
                    Lvw.SubItems.Add(Format(Dr("tanggal_ok"), "dd MM yyyy"))
                End If
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub LvKaryawan_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvKaryawan.DoubleClick
        If LvKaryawan.Items.Count = 0 Then Exit Sub

        TxtNoFaktur.Text = LvKaryawan.FocusedItem.Text
        TxtNoFaktur_Leave(LvKaryawan, e)
    End Sub

    Private Sub Master_Sales_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub ChkTanggal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTanggal.CheckedChanged
        If ChkTanggal.Checked Then
            CmbTanggal.Enabled = True : Tgl1.Enabled = True : Tgl2.Enabled = True
        Else
            CmbTanggal.SelectedIndex = -1 : Tgl1.Value = CDate(fmenu.ToolStripStatusLabel3.Text) : Tgl2.Value = CDate(fmenu.ToolStripStatusLabel3.Text)
            CmbTanggal.Enabled = False : Tgl1.Enabled = False : Tgl2.Enabled = False
        End If
    End Sub

    Private Sub ChkParameter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkParameter.CheckedChanged
        If ChkParameter.Checked Then
            CmbKolom.Enabled = True : TxtValue.Enabled = True
        Else
            CmbKolom.SelectedIndex = -1 : TxtValue.Text = ""
            CmbKolom.Enabled = False : TxtValue.Enabled = False
        End If
    End Sub

    Private Sub TxtNamaKaryawan_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNamaKaryawan.Leave
        'If TxtNamaKaryawan.Text.Trim.Length = 0 Then Exit Sub

        'Dim GetKode As String = ""

        'xSplit = TxtNamaKaryawan.Text.Trim.Split(" ")
        'If xSplit.Count = 1 Then
        '    GetKode = xSplit(0).Trim
        'Else
        '    If Strings.Right(xSplit(0).Trim, 1) = "." Then
        '        GetKode = xSplit(1).Trim
        '    Else
        '        GetKode = xSplit(0).Trim
        '    End If
        'End If
        'GetKode = GetKode.Replace(".", "")

        'OpenConn()

        'SQL = "Select top 1 Kode_Karyawan,Substring(kode_karyawan,1,len(kode_karyawan)-2) AS Kode "

        'SQL = SQL & "From HRIS_Rekrutmen_Karyawan "

        'SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
        'SQL = SQL & "left(kode_karyawan," & GetKode.Trim.Length & ") = '" & GetKode & "'"
        ''SQL = SQL & "substring(kode_karyawan,1,len(kode_karyawan)-2) = '" & GetKode & "'"

        'SQL = SQL & "Order By kode_karyawan desc"

        'Dim GetKanan As String

        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        If Len(Dr("kode_karyawan").ToString.Trim) = Len(GetKode.Trim) Then
        '            GetKanan = "01"
        '        Else
        '            GetKanan = Val(Strings.Right(Dr("kode_karyawan"), 2)) + 1
        '        End If
        '        TxtKodeKaryawan.Text = GetKode & Strings.Right("0" & GetKanan, 2)
        '    Else
        '        TxtKodeKaryawan.Text = GetKode
        '    End If
        'End Using

        'CloseConn()
    End Sub

    Private Sub TxtKodeKaryawan_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKodeKaryawan.Leave
        If TxtKodeKaryawan.Text.Trim.Length = 0 Then Exit Sub

        OpenConn()

        SQL = "Select kode_calon from calon_karyawan where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "kode_calon = '" & TxtKodeKaryawan.Text & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                MessageBox.Show("Kode calon karyawan sudah ada, input kode lain!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtKodeKaryawan.Text = "" : TxtKodeKaryawan.Focus()
            End If
        End Using

        CloseConn()
    End Sub

    Private Sub TxtKodeKaryawan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtKodeKaryawan.TextChanged

    End Sub
End Class
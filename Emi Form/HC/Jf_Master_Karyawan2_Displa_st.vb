
Imports LovePdf.Core
Imports LovePdf.Core.Sign
Imports LovePdf.Model.Task
Imports LovePdf.Model.TaskParams
Imports LovePdf.Model.TaskParams.Sign.Elements
Imports LovePdf.Model.TaskParams.Sign.Signers
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Jf_Master_Karyawan2_Displa_st
    Private Perusahaan_List As New ArrayList
    Dim LLevelJabatan As New ArrayList
    Dim LDivisiSubDivisi As New ArrayList
    Dim LShift As New ArrayList
    Dim LLembur As New ArrayList
    Dim LKolom As New ArrayList
    Dim LTanggal As New ArrayList
    Dim StrCari As String
    Dim StrKet As String

    Dim PublicKey As String = "project_public_e4c20d1ff42102c0a1b1986880530890_TZwLt2e5b1568f99c733dcbe14f9a7f45c281"
    Dim SecretKey As String = "secret_key_2b16efaf46968a5011fda3ec2b914606_dwxr02fab3732f1f08d3aac26fcf8d60e6c32"

    Dim FilePath As String = "F:\PEKERJAAN\MAIN\FormatHC\3_Format_Pengajuan_CnB_untuk_Pak_Hendri_IT.pdf"
    Dim AccessCode As String = "3V0J4Y4"

    Private Sub Jf_Master_Karyawan2_Display_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LvKaryawan.Columns.Add("No.Transaksi", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Tgl Faktur", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("User ID", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Kode Karyawan", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Level Jabatan", 250, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Divisi/Departemen", 250, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Lokasi", 120, HorizontalAlignment.Center)
        'LvKaryawan.Columns.Add("Keterangan Approval", 250, HorizontalAlignment.Left)
        LvKaryawan.View = View.Details

        LvCari.Visible = False

        Kosong()
    End Sub

    Private Sub Kosong()
        Try

            OpenConn()
            OpenConnSQL()

            LvKaryawan.Items.Clear()

            SQL = "SELECT dbo.HRIS_Transaksi_Karyawan.No_Faktur, dbo.HRIS_Transaksi_Karyawan.Tanggal, "
            SQL = SQL & "dbo.HRIS_Transaksi_Karyawan.Jam, dbo.HRIS_Transaksi_Karyawan.UserID, dbo.HRIS_Transaksi_Karyawan.Kode_Karyawan, "
            SQL = SQL & "dbo.HRIS_Transaksi_Karyawan.Nama, dbo.HRIS_Transaksi_Karyawan.Lokasi, dbo.HRIS_Level.Keterangan AS KetLevel, "
            SQL = SQL & "dbo.HRIS_Jabatan.Keterangan AS KetJabatan, dbo.HRIS_Golongan.Keterangan AS KetGolongan, "
            SQL = SQL & "dbo.HRIS_Sub_Golongan.Keterangan AS KetSubGolongan, dbo.HRIS_Divisi.Keterangan AS KetDivisi, "
            SQL = SQL & "dbo.HRIS_Sub_Divisi.Keterangan AS KetSubDivisi "

            'SQL = SQL & "(select STRING_AGG(b.UserID_Approval + case when b.Flag_Approval = 'Y' then ' SUDAH APPROVAL' else ' BELUM APPROVAL' end , ' || ') "
            'SQL = SQL & "as flag from HRIS_Transaksi_Karyawan_Approval b "
            'SQL = SQL & "where b.No_Faktur = dbo.HRIS_Transaksi_Karyawan.No_Faktur "
            'SQL = SQL & "and b.ID_Level_Jabatan = HRIS_Transaksi_Karyawan.ID_Level_Jabatan "
            'SQL = SQL & "and b.ID_Divisi_Sub_Divisi = HRIS_Transaksi_Karyawan.ID_Divisi_Sub_Divisi) as flag_app "

            SQL = SQL & "FROM dbo.HRIS_Transaksi_Karyawan INNER JOIN "
            SQL = SQL & "dbo.Perusahaan ON dbo.HRIS_Transaksi_Karyawan.Kode_Perusahaan = dbo.Perusahaan.Kode_Perusahaan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Level_Jabatan ON dbo.HRIS_Transaksi_Karyawan.ID_Level_Jabatan = dbo.HRIS_Level_Jabatan.ID_Level_Jabatan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Level ON dbo.HRIS_Level_Jabatan.ID_Level = dbo.HRIS_Level.ID_Level INNER JOIN "
            SQL = SQL & "dbo.HRIS_Jabatan ON dbo.HRIS_Level_Jabatan.ID_Jabatan = dbo.HRIS_Jabatan.ID_Jabatan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Golongan_Sub_Golongan ON dbo.HRIS_Level_Jabatan.ID_Golongan_Sub_Golongan = dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan_Sub_Golongan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan = dbo.HRIS_Golongan.ID_Golongan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Sub_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Sub_Golongan = dbo.HRIS_Sub_Golongan.ID_Sub_Golongan INNER JOIN "
            SQL = SQL & "dbo.HRIS_Divisi_Sub_Divisi ON dbo.HRIS_Transaksi_Karyawan.ID_Divisi_Sub_Divisi = dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi_Sub_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi = dbo.HRIS_Divisi.ID_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Sub_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi = dbo.HRIS_Sub_Divisi.ID_Sub_Divisi "

            SQL = SQL & "WHERE HRIS_Transaksi_Karyawan.kode_perusahaan = '" & KodePerusahaan & "' and HRIS_Transaksi_Karyawan.flag_ok = 'T' "

            SQL = SQL & "ORDER BY HRIS_Transaksi_Karyawan.no_faktur,HRIS_Transaksi_Karyawan.kode_karyawan"

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvKaryawan.Items.Add(dr("no_faktur"))
                    Lvw.SubItems.Add(Format(dr("tanggal"), "dd MMM yyyy"))
                    Lvw.SubItems.Add(dr("jam"))
                    Lvw.SubItems.Add(dr("userid"))
                    Lvw.SubItems.Add(dr("kode_karyawan"))
                    Lvw.SubItems.Add(dr("nama"))
                    Lvw.SubItems.Add(dr("ketlevel") & " | " & dr("ketjabatan") & " | " & dr("ketgolongan") & dr("ketsubgolongan"))
                    Lvw.SubItems.Add(dr("ketdivisi") & " | " & dr("ketsubdivisi"))
                    Lvw.SubItems.Add(dr("lokasi"))

                    'SQLSQL = SQLSQL & "select STRING_AGG(b.User_ID + case when b.Acc = 'Y' then ' SUDAH APPROVAL' else ' BELUM APPROVAL' end , ' || ') "
                    'SQLSQL = SQLSQL & "as flag_app from HRIS_Transaksi_Karyawan_Approval b "
                    'SQLSQL = SQLSQL & "where b.No_Faktur = '" & dr("no_faktur") & "' "
                    'Using DrSQL = OpenTransSQL(SQLSQL)
                    '    If DrSQL.Read Then
                    '        If General_Class.CekNULL(DrSQL("flag_app")) <> "" Then
                    '            Lvw.SubItems.Add(DrSQL("flag_app"))
                    '        Else
                    '            Lvw.SubItems.Add("-")
                    '        End If
                    '    End If
                    'End Using
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
                    CmbLevel.Items.Add(Dr("ketlevel") & " | " & Dr("ketjabatan") & " | " & Dr("ketgolongan") & Dr("ketsubgolongan"))
                Loop
            End Using
            CmbLevel.SelectedIndex = -1
            '=================================================================

            CmbDivisi.Items.Clear() : LDivisiSubDivisi.Clear()

            SQL = "SELECT dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi_Sub_Divisi, dbo.HRIS_Divisi.Keterangan AS KetDivisi, dbo.HRIS_Sub_Divisi.Keterangan AS KetSubDivisi "

            SQL = SQL & "FROM dbo.HRIS_Divisi_Sub_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi = dbo.HRIS_Divisi.ID_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Sub_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi = dbo.HRIS_Sub_Divisi.ID_Sub_Divisi "

            SQL = SQL & "ORDER BY dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi, dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi"

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    LDivisiSubDivisi.Add(Dr("id_divisi_sub_divisi"))
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

            CmbShift.Items.Clear() : LShift.Clear()
            SQL = "SELECT ID_Shift,Nama FROM HRIS_Shift_Kerja ORDER BY ID_Shift"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    LShift.Add(Dr("id_shift"))
                    CmbShift.Items.Add(Dr("nama"))
                Loop
            End Using
            CmbShift.SelectedIndex = -1

            CmbLembur.Items.Clear() : LLembur.Clear()
            SQL = "SELECT ID_Waktu_Lembur,Kode_Waktu_Lembur FROM HRIS_Waktu_Lembur ORDER BY ID_Waktu_Lembur"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    LLembur.Add(Dr("id_waktu_lembur"))
                    CmbLembur.Items.Add(Dr("kode_waktu_lembur"))
                Loop
            End Using
            CmbLembur.SelectedIndex = -1

            CloseConn()
            CloseConnSQL()
        Catch ex As Exception
            CloseConn()
            CloseConnSQL()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        ChkTanggal.Checked = False : CmbTanggal.Enabled = False
        Tgl1.Enabled = False : Tgl1.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        Tgl2.Enabled = False : Tgl2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)

        CmbTanggal.Items.Clear() : LTanggal.Clear()
        CmbTanggal.Items.Add("Tgl Faktur") : LTanggal.Add("Tanggal")
        CmbTanggal.Items.Add("Tgl Lahir") : LTanggal.Add("Tgl_Lahir")
        CmbTanggal.Items.Add("Tgl Masuk") : LTanggal.Add("Tgl_Masuk")
        CmbTanggal.Items.Add("Tgl PKWT") : LTanggal.Add("Tgl_PKWT")
        CmbTanggal.Items.Add("Tanggal OK") : LTanggal.Add("Tanggal_OK")
        CmbTanggal.SelectedIndex = -1

        ChkParameter.Checked = False : CmbKolom.Enabled = False
        TxtValue.Enabled = False

        CmbKolom.Items.Clear() : LKolom.Clear()
        CmbKolom.Items.Add("No.Transaksi") : LKolom.Add("No_Faktur")
        CmbKolom.Items.Add("Kode Karyawan") : LKolom.Add("Kode_Karyawan")
        CmbKolom.Items.Add("Nama") : LKolom.Add("Nama")
        CmbKolom.Items.Add("Aktif") : LKolom.Add("Aktif")
        CmbKolom.SelectedIndex = -1

        TglFaktur.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        TxtNoFaktur.Text = ""
        TxtKodeKaryawan.Text = "" : TxtNamaKaryawan.Text = "" : TxtAlamat.Text = "" : TxtTelepon.Text = "" : TxtHp.Text = ""
        TxtIDAbsen.Text = ""
        TglLahir.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        TxtEmail.Text = "" : TxtNoIndukKaryawan.Text = ""
        TglMasuk.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        TxtNIK.Text = "" : TxtNPWP.Text = "" : TxtKepala.Text = ""
        TglPKWT.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        TxtBank.Text = "" : TxtNamaRek.Text = "" : TxtNoRek.Text = "" : TxtValue.Text = ""
        TxtGapok.Text = "0" : TxtRangeGP1.Text = "0 - 0" : TxtRangeGP2.Text = "0 - 0" : TxtGapok.Enabled = True
        'TxtTunjangan.Text = "0" : TxtRangeTJ1.Text = "0 - 0" : TxtRangeTJ2.Text = "0 - 0" : TxtTunjangan.Enabled = True
        TxtCari1.Text = "" : TxtCari1.Enabled = True
        TxtNilaiT.Text = "0" : TxtNilaiT.Enabled = True
        LvTunjangan.Items.Clear()

        TabControl1.SelectedTab = TabPage1

        SetAllTextBoxesReadOnly(Me, True)
        SetAllComboBoxesReadOnly(Me, False)
        SetAllDateTimePickersReadOnly(Me, False)

        TxtGapok.ReadOnly = False : TxtTunjangan.ReadOnly = False : TxtCari1.ReadOnly = False : TxtNilaiT.ReadOnly = False
    End Sub

    Private Sub SetAllTextBoxesReadOnly(ByVal container As Control, ByVal SetAsReadOnly As Boolean)
        For Each ctrl As Control In container.Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).ReadOnly = SetAsReadOnly
            ElseIf ctrl.HasChildren Then
                SetAllTextBoxesReadOnly(ctrl, SetAsReadOnly)
            End If
        Next
    End Sub

    Private Sub SetAllComboBoxesReadOnly(ByVal container As Control, ByVal SetAsReadOnly As Boolean)
        For Each ctrl As Control In container.Controls
            If TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).Enabled = SetAsReadOnly
            ElseIf ctrl.HasChildren Then
                SetAllComboBoxesReadOnly(ctrl, SetAsReadOnly)
            End If
        Next
    End Sub

    Private Sub SetAllDateTimePickersReadOnly(ByVal container As Control, ByVal SetAsReadOnly As Boolean)
        For Each ctrl As Control In container.Controls
            If TypeOf ctrl Is DateTimePicker Then
                CType(ctrl, DateTimePicker).Enabled = SetAsReadOnly
            ElseIf ctrl.HasChildren Then
                SetAllDateTimePickersReadOnly(ctrl, SetAsReadOnly)
            End If
        Next
    End Sub

    Private Sub TxtNoFaktur_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNoFaktur.Leave
        If TxtNoFaktur.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()
            OpenConnSQL()

            SQL = "Select a.*,c.Range1 as RangeGP1,c.range2 as RangeGP2,d.range1 as RangeTJ1,d.range2 as RangeTJ2 "
            SQL = SQL & "From HRIS_Transaksi_Karyawan as a left outer join HRIS_Level_Jabatan as b on a.id_level_jabatan = b.id_level_jabatan "
            SQL = SQL & "left outer join HRIS_Range_Gaji as c on b.id_golongan_sub_golongan = c.id_golongan_sub_golongan and b.id_level = c.id_level "
            SQL = SQL & "left outer join HRIS_Range_Tunjangan as d on b.id_golongan_sub_golongan = d.id_golongan_sub_golongan and b.id_level = d.id_level "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & TxtNoFaktur.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TglFaktur.Value = Dr("tanggal")
                    TxtNoFaktur.Text = Dr("no_faktur")
                    TxtKodeKaryawan.Text = Dr("kode_karyawan")
                    TxtNamaKaryawan.Text = Dr("Nama")
                    TxtAlamat.Text = Dr("alamat")
                    TxtTelepon.Text = Dr("telepon")
                    TxtHp.Text = Dr("HP")
                    TglLahir.Value = Dr("tgl_lahir")
                    CmbJK.Text = Dr("jenis_kelamin")
                    CmbJenis.Text = Dr("jenis")
                    CmbAktif.Text = Dr("aktif")
                    TxtIDAbsen.Text = Dr("userid_absen")
                    TxtEmail.Text = Dr("email")
                    TglMasuk.Value = Dr("tgl_masuk")
                    TxtNIK.Text = Dr("NIK")
                    TxtNoIndukKaryawan.Text = Dr("no_induk_karyawan")
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
                    TglPKWT.Value = Dr("tgl_pkwt")
                    TxtKepala.Text = Dr("kepala")
                    CmbShift.SelectedIndex = LShift.IndexOf(Dr("id_shift"))
                    CmbLembur.SelectedIndex = LLembur.IndexOf(Dr("id_waktu_lembur"))
                    TxtBank.Text = Dr("bank")
                    TxtNamaRek.Text = Dr("nama_rekening")
                    TxtNoRek.Text = Dr("no_rek")
                    'GAJI POKOK
                    If IsDBNull(Dr("RangeGP1")) Then
                        MessageBox.Show("Range gapok untuk level jabatan belum diinput!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        TxtRangeGP1.Text = "0 - 0" : TxtRangeGP2.Text = "0 - 0"
                    Else
                        TxtRangeGP1.Text = Format(Dr("RangeGP1"), "N0") & " - " & Format(Dr("RangeGP2"), "N0")
                        TxtRangeGP2.Text = Dr("RangeGP1") & " - " & Dr("RangeGP2")
                    End If
                    'TUNJANGAN
                    'If IsDBNull(Dr("RangeTJ1")) Then
                    '    MessageBox.Show("Range tunjangan untuk level jabatan belum diinput!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    TxtRangeTJ1.Text = "0 - 0" : TxtRangeTJ2.Text = "0 - 0"
                    'Else
                    '    TxtRangeTJ1.Text = Format(Dr("RangeTJ1"), "N0") & " - " & Format(Dr("RangeTJ2"), "N0")
                    '    TxtRangeTJ2.Text = Dr("RangeTJ1") & " - " & Dr("RangeTJ2")
                    'End If
                    TxtGapok.Text = "0"
                    'TxtTunjangan.Text = "0"
                    TxtCari1.Text = ""
                    TxtNilaiT.Text = "0"
                    TxtRangeTJ1.Text = ""
                    TxtRangeTJ2.Text = ""
                Else
                    TxtNamaKaryawan.Text = "" : TxtAlamat.Text = "" : TxtTelepon.Text = "" : TxtHp.Text = ""
                    TglLahir.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
                    CmbJK.SelectedIndex = -1 : CmbJenis.SelectedIndex = -1 : CmbAktif.SelectedIndex = -1 : TxtIDAbsen.Text = ""
                    TxtEmail.Text = "" : TglMasuk.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
                    TxtNIK.Text = "" : TxtNoIndukKaryawan.Text = "" : TxtNPWP.Text = ""
                    CmbPerusahaan.SelectedIndex = -1 : CmbPerayaan.SelectedIndex = -1 : CmbPendidikan.SelectedIndex = -1
                    CmbAgama.SelectedIndex = -1 : CmbLevel.SelectedIndex = -1 : CmbDivisi.SelectedIndex = -1
                    CmbLokasi.SelectedIndex = -1 : CmbLokasiSlipGaji.SelectedIndex = -1 : CmbStatusKaryawan.SelectedIndex = -1
                    CmbShift.SelectedIndex = -1 : CmbLembur.SelectedIndex = -1
                    TglPKWT.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
                    TxtKepala.Text = "" : TxtBank.Text = "" : TxtNamaRek.Text = "" : TxtNoRek.Text = "" : TxtValue.Text = ""
                    TxtGapok.Text = "0" : TxtRangeGP1.Text = "0 - 0" : TxtRangeGP2.Text = "0 - 0"
                    'TxtTunjangan.Text = "0" : TxtRangeTJ1.Text = "0 - 0" : TxtRangeTJ2.Text = "0 - 0"
                    TxtCari1.Text = "" : TxtNilaiT.Text = "0" : TxtRangeTJ1.Text = "0 - 0" : TxtRangeTJ2.Text = "0 - 0"
                End If
            End Using

            Dim lvl_app As String = ""
            SQLSQL = "select Level_Hierarchy from HRIS_Transaksi_Karyawan_Approval where User_ID = '" & UserID & "' "
            SQLSQL = SQLSQL & "and No_Faktur = '" & TxtNoFaktur.Text & "' "
            Using DrSQL = OpenTransSQL(SQLSQL)
                If DrSQL.Read Then
                    If DrSQL("Level_Hierarchy") = 1 Then
                        TxtGapok.Enabled = True
                        'TxtTunjangan.Enabled = True
                        TxtCari1.Enabled = True
                        TxtNilaiT.Enabled = True
                    Else
                        TxtGapok.Enabled = False
                        'TxtTunjangan.Enabled = False
                        TxtCari1.Enabled = False
                        TxtNilaiT.Enabled = False
                    End If
                End If
            End Using

            SQL = "select a.No_Faktur,b.Gaji_Pokok,b.Tunjangan from HRIS_Transaksi_Karyawan a,HRIS_Transaksi_Karyawan_Gaji b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & TxtNoFaktur.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtGapok.Text = Dr("Gaji_Pokok")
                    'TxtTunjangan.Text = Dr("Tunjangan")
                    TxtGapok.Enabled = False
                    'TxtTunjangan.Enabled = False
                    TxtCari1.Enabled = False
                    TxtNilaiT.Enabled = False
                Else
                    TxtGapok.Text = "0"
                    'TxtTunjangan.Text = "0"
                    TxtCari1.Text = ""
                    TxtNilaiT.Text = "0"
                End If
            End Using

            Dim StrRange As String
            Dim StrRange2 As String

            LvTunjangan.Items.Clear()
            SQL = "Select a.Kode_Komponen, b.Keterangan, a.Tunjangan, e.range1 as RangeTJ1, e.range2 as RangeTJ2 "
            SQL = SQL & "From (((HRIS_Transaksi_Karyawan_Tunjangan a Left Join HRIS_Komponen_Gaji b ON a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Komponen = b.Kode_Komponen) "
            SQL = SQL & "Left Join HRIS_Transaksi_Karyawan c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND a.No_Faktur = c.No_Faktur) "
            SQL = SQL & "Left Join HRIS_Level_Jabatan d on c.id_level_jabatan = d.id_level_jabatan) "
            SQL = SQL & "Left Join HRIS_Range_Tunjangan e on d.id_golongan_sub_golongan = e.id_golongan_sub_golongan and d.id_level = e.id_level and a.Kode_Komponen = e.Kode_Komponen "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.No_Faktur = '" & TxtNoFaktur.Text.Trim & "'"
            Using Ds = OpenTrans(SQL)
                Do While Ds.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvTunjangan.Items.Add(Ds("Kode_Komponen"))
                    Lvw.SubItems.Add(Ds("Keterangan"))
                    Lvw.SubItems.Add(Format(Ds("Tunjangan"), "N0"))
                    StrRange = Format(Ds("RangeTJ1"), "N0")
                    StrRange2 = Format(Ds("RangeTJ2"), "N0")
                    Lvw.SubItems.Add(StrRange & " - " & StrRange2)
                    Lvw.SubItems.Add(Ds("RangeTJ1") & " - " & Ds("RangeTJ2"))
                Loop
            End Using

            CloseConn()
            CloseConnSQL()
        Catch ex As Exception
            CloseConn()
            CloseConnSQL()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvKaryawan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvKaryawan.Click
        If LvKaryawan.Items.Count = 0 Then Exit Sub

        TxtNoFaktur.Text = LvKaryawan.FocusedItem.Text
        TxtNoFaktur_Leave(LvKaryawan, e)
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

        SQL = "SELECT dbo.HRIS_Transaksi_Karyawan.No_Faktur, dbo.HRIS_Transaksi_Karyawan.Tanggal, "
        SQL = SQL & "dbo.HRIS_Transaksi_Karyawan.Jam, dbo.HRIS_Transaksi_Karyawan.UserID, dbo.HRIS_Transaksi_Karyawan.Kode_Karyawan, "
        SQL = SQL & "dbo.HRIS_Transaksi_Karyawan.Nama, dbo.HRIS_Transaksi_Karyawan.Lokasi, dbo.HRIS_Level.Keterangan AS KetLevel, "
        SQL = SQL & "dbo.HRIS_Jabatan.Keterangan AS KetJabatan, dbo.HRIS_Golongan.Keterangan AS KetGolongan, "
        SQL = SQL & "dbo.HRIS_Sub_Golongan.Keterangan AS KetSubGolongan, dbo.HRIS_Divisi.Keterangan AS KetDivisi, "
        SQL = SQL & "dbo.HRIS_Sub_Divisi.Keterangan AS KetSubDivisi "

        SQL = SQL & "FROM dbo.HRIS_Transaksi_Karyawan INNER JOIN "
        SQL = SQL & "dbo.Perusahaan ON dbo.HRIS_Transaksi_Karyawan.Kode_Perusahaan = dbo.Perusahaan.Kode_Perusahaan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Level_Jabatan ON dbo.HRIS_Transaksi_Karyawan.ID_Level_Jabatan = dbo.HRIS_Level_Jabatan.ID_Level_Jabatan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Level ON dbo.HRIS_Level_Jabatan.ID_Level = dbo.HRIS_Level.ID_Level INNER JOIN "
        SQL = SQL & "dbo.HRIS_Jabatan ON dbo.HRIS_Level_Jabatan.ID_Jabatan = dbo.HRIS_Jabatan.ID_Jabatan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Golongan_Sub_Golongan ON dbo.HRIS_Level_Jabatan.ID_Golongan_Sub_Golongan = dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan_Sub_Golongan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Golongan = dbo.HRIS_Golongan.ID_Golongan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Sub_Golongan ON dbo.HRIS_Golongan_Sub_Golongan.ID_Sub_Golongan = dbo.HRIS_Sub_Golongan.ID_Sub_Golongan INNER JOIN "
        SQL = SQL & "dbo.HRIS_Divisi_Sub_Divisi ON dbo.HRIS_Transaksi_Karyawan.ID_Divisi_Sub_Divisi = dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi_Sub_Divisi INNER JOIN "
        SQL = SQL & "dbo.HRIS_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi = dbo.HRIS_Divisi.ID_Divisi INNER JOIN "
        SQL = SQL & "dbo.HRIS_Sub_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi = dbo.HRIS_Sub_Divisi.ID_Sub_Divisi "

        SQL = SQL & "WHERE HRIS_Transaksi_Karyawan.kode_perusahaan = '" & KodePerusahaan & "' and HRIS_Transaksi_Karyawan.flag_ok = 'T' "

        If ChkTanggal.Checked Then
            SQL = SQL & " and HRIS_Transaksi_Karyawan." & LTanggal(CmbTanggal.SelectedIndex) & " between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
        End If

        If ChkParameter.Checked Then
            SQL = SQL & " and HRIS_Transaksi_Karyawan." & LKolom(CmbKolom.SelectedIndex) & " like '%" & TxtValue.Text & "%' "
        End If

        SQL = SQL & "ORDER BY HRIS_Transaksi_Karyawan.no_faktur,HRIS_Transaksi_Karyawan.kode_karyawan"
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                Dim Lvw As ListViewItem
                Lvw = LvKaryawan.Items.Add(Dr("no_faktur"))
                Lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                Lvw.SubItems.Add(Dr("jam"))
                Lvw.SubItems.Add(Dr("userid"))
                Lvw.SubItems.Add(Dr("kode_karyawan"))
                Lvw.SubItems.Add(Dr("nama"))
                Lvw.SubItems.Add(Dr("ketlevel") & " | " & Dr("ketjabatan") & " | " & Dr("ketgolongan") & Dr("ketsubgolongan"))
                Lvw.SubItems.Add(Dr("ketdivisi") & " | " & Dr("ketsubdivisi"))
                Lvw.SubItems.Add(Dr("lokasi"))
            Loop
        End Using

        CloseConn()
    End Sub

    Private Sub TxtValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtValue.KeyPress
        If e.KeyChar = Chr(13) Then BtCari_Click(TxtValue, e)
    End Sub

    Private Sub CmbKolom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbKolom.KeyPress
        If e.KeyChar = Chr(13) Then TxtValue.Focus()
    End Sub

    Private Sub ChkTanggal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTanggal.CheckedChanged
        If ChkTanggal.Checked Then
            CmbTanggal.Enabled = True : Tgl1.Enabled = True : Tgl2.Enabled = True
        Else
            CmbTanggal.SelectedIndex = -1 : Tgl1.Value = CDate(FMenu.ToolStripStatusLabel3.Text) : Tgl2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
            CmbTanggal.Enabled = False : Tgl1.Enabled = False : Tgl2.Enabled = False
        End If
    End Sub

    Private Sub ChkParameter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkParameter.CheckedChanged
        If ChkParameter.Checked Then
            CmbKolom.Enabled = True : TxtValue.Enabled = True
            TxtValue.ReadOnly = False
        Else
            CmbKolom.SelectedIndex = -1 : TxtValue.Text = ""
            CmbKolom.Enabled = False : TxtValue.Enabled = False
        End If
    End Sub

    Private Sub TxtGapok_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtGapok.KeyPress
        If e.KeyChar = Chr(13) Then TxtCari1.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TxtNilaiT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNilaiT.KeyPress
        If e.KeyChar = Chr(13) Then BtOK1.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    End Sub

    Private Async Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        If LvKaryawan.Items.Count = 0 Then Exit Sub

        If TxtNoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("Nomor faktur harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvKaryawan.Focus() : Exit Sub
        ElseIf TxtKodeKaryawan.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode karyawan harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvKaryawan.Focus() : Exit Sub
        ElseIf CmbShift.SelectedIndex = -1 Then
            MessageBox.Show("Shift karyawan harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvKaryawan.Focus() : Exit Sub
        ElseIf CmbLembur.SelectedIndex = -1 Then
            MessageBox.Show("Lembur karyawan harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvKaryawan.Focus() : Exit Sub
        End If

        'GAJI POKOK
        If TxtGapok.Text.Trim.Length = 0 Or Val(TxtGapok.Text) = 0 Then
            MessageBox.Show("Gaji pokok belum diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage3 : TxtGapok.Focus()
            Exit Sub
        End If

        xSplit = TxtRangeGP2.Text.Split("-")

        If Val(TxtGapok.Text) < Val(xSplit(0).Trim) Or Val(TxtGapok.Text) > Val(xSplit(1).Trim) Then
            MessageBox.Show("Gaji pokok harus di dalam range gaji!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage3 : TxtGapok.Text = "0" : TxtGapok.Focus()
            Exit Sub
        End If

        ''TUNJANGAN
        'If TxtTunjangan.Text.Trim.Length = 0 Then
        '    MessageBox.Show("Tunjangan belum diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    TabControl1.SelectedTab = TabPage3 : TxtTunjangan.Focus()
        '    Exit Sub
        'End If

        'xSplit = TxtRangeTJ2.Text.Split("-")

        'If Val(TxtTunjangan.Text) < Val(xSplit(0).Trim) Or Val(TxtTunjangan.Text) > Val(xSplit(1).Trim) Then
        '    MessageBox.Show("Tunjangan harus di dalam range tunjangan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    TabControl1.SelectedTab = TabPage3 : TxtTunjangan.Text = "0" : TxtTunjangan.Focus()
        '    Exit Sub
        'End If

        For i As Integer = 0 To LvTunjangan.Items.Count - 1
            xSplit = LvTunjangan.Items(i).SubItems(4).Text.Split("-")

            If Val(HilangkanTanda(LvTunjangan.Items(i).SubItems(2).Text)) < Val(xSplit(0).Trim) Or Val(HilangkanTanda(LvTunjangan.Items(i).SubItems(2).Text)) > Val(xSplit(1).Trim) Then
                MessageBox.Show(LvTunjangan.Items(i).SubItems(1).Text & " harus di dalam range tunjangan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TabControl1.SelectedTab = TabPage3
                Exit Sub
            End If
        Next i

        Dim Yakin As String = MessageBox.Show("Anda yakin akan validasi data ini?" & Chr(13) & Chr(13) &
                                                "No Faktur : " & TxtNoFaktur.Text & Chr(13) &
                                                "Atas nama : " & TxtNamaKaryawan.Text & " [" &
                                                LvKaryawan.FocusedItem.SubItems(4).Text & "]", "Validasi Transaksi Karyawan", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Yakin = vbNo Then Exit Sub

        get_jam()
        Try
            OpenConn()
            OpenConnSQL()
            Cmd.Transaction = Cn.BeginTransaction
            CmdSQL.Transaction = CnSQL.BeginTransaction

            Dim flag_simpan As String = "T"
            'SQL = "select a.Level_Approval,a.UserID_Approval,b.Flag_Approval from HRIS_UserID_Approval_Karyawan a left join "
            'SQL = SQL & "HRIS_Transaksi_Karyawan_Approval b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.UserID_Approval= b.UserID_Approval "
            'SQL = SQL & "and a.ID_Level_Jabatan = b.ID_Level_Jabatan and a.ID_Divisi_Sub_Divisi = b.ID_Divisi_Sub_Divisi "
            'SQL = SQL & "and b.No_Faktur = '" & TxtNoFaktur.Text & "' where a.UserID_Approval = '" & UserID & "' "
            'SQL = SQL & "and a.ID_Level_Jabatan = '" & LLevelJabatan.Item(CmbLevel.SelectedIndex) & "' "
            'SQL = SQL & "and a.ID_Divisi_Sub_Divisi = '" & LDivisiSubDivisi.Item(CmbDivisi.SelectedIndex) & "' order by a.Level_Approval "
            SQLSQL = "select User_ID,Acc,Tolak,Level_Hierarchy from HRIS_Transaksi_Karyawan_Approval where "
            SQLSQL = SQLSQL & "No_Faktur = '" & TxtNoFaktur.Text & "' and User_ID = '" & UserID & "' "
            Using DrSQL = OpenTransSQL(SQLSQL)
                If DrSQL.Read Then
                    If DrSQL("User_ID") = UserID And General_Class.CekNULL(DrSQL("Acc")) = "" And General_Class.CekNULL(DrSQL("Tolak")) = "" Then
                        If DrSQL("Level_Hierarchy") = "1" Then
                            DrSQL.Close()
                            SQL = "INSERT INTO HRIS_Transaksi_Karyawan_Gaji(Kode_Perusahaan,No_Faktur,Kode_Karyawan,Gaji_Pokok,Tunjangan) VALUES("
                            SQL = SQL & "'" & KodePerusahaan & "','" & TxtNoFaktur.Text & "','" & TxtKodeKaryawan.Text & "',"
                            SQL = SQL & "'" & TxtGapok.Text & "','" & TxtTunjangan.Text & "')"
                            ExecuteTrans(SQL)

                            For i As Integer = 0 To LvTunjangan.Items.Count - 1
                                SQL = "Insert into HRIS_Transaksi_Karyawan_Tunjangan(Kode_Perusahaan,No_Faktur,Kode_Karyawan,Kode_Komponen,Tunjangan) "
                                SQL = SQL & "Values('" & KodePerusahaan & "','" & TxtNoFaktur.Text & "','" & TxtKodeKaryawan.Text & "',"
                                SQL = SQL & "'" & LvTunjangan.Items(i).Text.ToUpper & "'," & LvTunjangan.Items(i).SubItems(2).Text & ")"
                                ExecuteTrans(SQL)
                            Next i
                        Else
                            DrSQL.Close()
                        End If

                        'SQL = "INSERT INTO HRIS_Transaksi_Karyawan_Approval(Kode_Perusahaan,No_Faktur,Flag_Approval,UserID_Approval,Tanggal_Approval,Jam_Approval,"
                        'SQL = SQL & "b.ID_Level_Jabatan,b.ID_Divisi_Sub_Divisi) "
                        'SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtNoFaktur.Text & "','Y','" & UserID & "',"
                        'SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "',"
                        'SQL = SQL & "'" & LLevelJabatan.Item(CmbLevel.SelectedIndex) & "','" & LDivisiSubDivisi.Item(CmbDivisi.SelectedIndex) & "')"
                        SQLSQL = "update HRIS_Transaksi_Karyawan_Approval set Acc = 'Y',"
                        SQLSQL = SQLSQL & "Tanggal = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                        SQLSQL = SQLSQL & "Jam = '" & Format(tgl_skg, "HH:mm:ss") & "' where "
                        SQLSQL = SQLSQL & "No_Faktur = '" & TxtNoFaktur.Text & "' "
                        SQLSQL = SQLSQL & "and User_ID = '" & UserID & "' "
                        ExecuteTransSQL(SQLSQL)

                    ElseIf DrSQL("User_ID") = UserID And General_Class.CekNULL(DrSQL("Acc")) = "Y" And General_Class.CekNULL(DrSQL("Tolak")) = "" Then
                        DrSQL.Close()
                        CloseTrans()
                        CloseTransSQL()
                        CloseConn()
                        CloseConnSQL()
                        MessageBox.Show("No Faktur ini sudah divalidasi !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    ElseIf DrSQL("User_ID") = UserID And General_Class.CekNULL(DrSQL("Acc")) = "" And General_Class.CekNULL(DrSQL("Tolak")) = "Y" Then
                        DrSQL.Close()
                        CloseTrans()
                        CloseTransSQL()
                        CloseConn()
                        CloseConnSQL()
                        MessageBox.Show("No Faktur ini sudah ditolak !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    DrSQL.Close()
                    CloseTrans()
                    CloseTransSQL()
                    CloseConn()
                    CloseConnSQL()
                    MessageBox.Show("No Faktur tidak bisa divalidasi !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQLSQL = "select User_ID,Acc,Tolak,Level_Hierarchy from HRIS_Transaksi_Karyawan_Approval where "
            SQLSQL = SQLSQL & "No_Faktur = '" & TxtNoFaktur.Text & "' order by Level_Hierarchy "
            Using DrSQL = OpenTransSQL(SQLSQL)
                Do While DrSQL.Read
                    If General_Class.CekNULL(DrSQL("Acc")) = "Y" Then
                        flag_simpan = "Y"
                    Else
                        flag_simpan = "T"
                        Exit Do
                    End If
                Loop
            End Using
            If flag_simpan = "Y" Then
                SQL = "Update HRIS_Transaksi_Karyawan Set Flag_OK = 'Y', Tanggal_OK = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
                SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtNoFaktur.Text & "'"
                ExecuteTrans(SQL)

                SQL = "Insert Into Karyawan (kode_perusahaan,kode_karyawan,nama,alamat,telepon,hp,jenis,aktif,userid_absen,no_rek,kode_jabatan,email,"
                SQL = SQL & "tgl_masuk,cabang_untuk_gaji,npwp,nik,status_karyawan,jenis_perayaan,lokasi,tgl_pkwt,"
                SQL = SQL & "no_induk_karyawan,id_perusahaan,pendidikan_terakhir,agama,"
                SQL = SQL & "kode_divisi,tgl_lahir,jenis_kelamin,bank,nama_rekening,"
                SQL = SQL & "id_level_jabatan,id_divisi_sub_divisi)"

                SQL = SQL & "Select kode_perusahaan,kode_karyawan,nama,alamat,telepon,hp,jenis,aktif,userid_absen,no_rek,kode_jabatan,email,"
                SQL = SQL & "tgl_masuk,cabang_untuk_gaji,npwp,nik,status_karyawan,jenis_perayaan,lokasi,tgl_pkwt,"
                SQL = SQL & "no_induk_karyawan,id_perusahaan,pendidikan_terakhir,agama, "
                SQL = SQL & "kode_divisi,tgl_lahir,jenis_kelamin,bank,nama_rekening,"
                SQL = SQL & "id_level_jabatan,id_divisi_sub_divisi "

                SQL = SQL & "From HRIS_Transaksi_Karyawan "

                SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtNoFaktur.Text & "'"
                ExecuteTrans(SQL)

                SQL = "Insert Into HRIS_Shift_Per_Karyawan (kode_perusahaan,kode_karyawan,id_shift,periode)"
                SQL = SQL & "values('" & KodePerusahaan & "','" & TxtKodeKaryawan.Text.Trim & "','" & LShift(CmbShift.SelectedIndex) & "','"
                SQL = SQL & Format(TglMasuk.Value, "yyyy-MM-dd") & "')"
                ExecuteTrans(SQL)

                SQL = "Insert Into HRIS_Lembur_Per_Karyawan (kode_perusahaan,kode_karyawan,id_waktu_lembur,periode)"
                SQL = SQL & "values('" & KodePerusahaan & "','" & TxtKodeKaryawan.Text.Trim & "','" & LLembur(CmbLembur.SelectedIndex) & "','"
                SQL = SQL & Format(TglMasuk.Value, "yyyy-MM-dd") & "')"
                ExecuteTrans(SQL)

                SQL = "Insert Into Master_Gaji (kode_perusahaan,kode_karyawan,gaji_pokok)"
                SQL = SQL & "values('" & KodePerusahaan & "','" & TxtKodeKaryawan.Text.Trim & "','" & TxtGapok.Text.Trim & "')"
                ExecuteTrans(SQL)

                'SQL = "Insert Into Master_Tunjangan (kode_perusahaan,kode_karyawan,tunjangan)"
                'SQL = SQL & "values('" & KodePerusahaan & "','" & TxtKodeKaryawan.Text.Trim & "','" & TxtTunjangan.Text.Trim & "')"
                'ExecuteTrans(SQL)

                For i As Integer = 0 To LvTunjangan.Items.Count - 1
                    SQL = "Insert Into Master_Tunjangan (kode_perusahaan,kode_karyawan,Kode_Komponen,Tunjangan)"
                    SQL = SQL & "values('" & KodePerusahaan & "','" & TxtKodeKaryawan.Text.Trim & "','" & LvTunjangan.Items(i).Text.ToUpper & "'," & HilangkanTanda(LvTunjangan.Items(i).SubItems(2).Text) & ")"
                    ExecuteTrans(SQL)
                Next i
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
            Exit Sub
        End Try

        Await Kirim_Email()
        Kosong()
    End Sub

    Private Sub TxtCari1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCari1.KeyPress
        If e.KeyChar = Chr(13) Then TxtNilaiT.Focus()
    End Sub

    'Private Sub TxtTunjangan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTunjangan.KeyPress
    '    If e.KeyChar = Chr(13) Then BtnSimpan_Click(TxtGapok, e)
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    'End Sub

    Private Sub TxtCari1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCari1.TextChanged
        If TxtCari1.Text.Trim.Length = 0 Then
            LvCari.Visible = False : Exit Sub
        Else
            LvCari.Visible = True
        End If

        LvCari.Items.Clear()

        Try
            OpenConn()

            Dim Lvw2 As ListViewItem

            With LvCari
                .Top = TxtCari1.Top + 23
                .Left = TxtCari1.Left
                .Width = 470
                .Height = 100
            End With

            SQL = "select a.Kode_Komponen, a.Keterangan , c.range1 as RangeTJ1,c.range2 as RangeTJ2 "
            SQL = SQL & "from (HRIS_Komponen_Gaji a Left Join HRIS_Range_Tunjangan c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND a.Kode_Komponen = c.Kode_Komponen) "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Jenis = 'I' and a.keterangan like '" & TxtCari1.Text & "%' and (a.perhitungan = 'T' or a.perhitungan = '') AND "
            SQL = SQL & "c.id_level in (select id_level from HRIS_Level_Jabatan where id_level_jabatan = '" & LLevelJabatan.Item(CmbLevel.SelectedIndex) & "') and "
            SQL = SQL & "c.id_golongan_sub_golongan in (select id_golongan_sub_golongan from HRIS_Level_Jabatan where id_level_jabatan = '" & LLevelJabatan.Item(CmbLevel.SelectedIndex) & "') "
            If Strings.Trim(StrCari) = "" Then
            Else
                SQL = SQL & "and a.Kode_Komponen NOT IN (" & StrCari & ") "
            End If
            SQL = SQL & "order by a.Kode_Komponen"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lvw2 = LvCari.Items.Add(Dr("Kode_Komponen"))
                    Lvw2.SubItems.Add(Dr("Keterangan"))
                    Lvw2.SubItems.Add(Format(Dr("RangeTJ1"), "N0") & " - " & Format(Dr("RangeTJ2"), "N0"))
                    Lvw2.SubItems.Add(Dr("RangeTJ1") & " - " & Dr("RangeTJ2"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtOK1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOK1.Click
        If TxtCari1.Text.Trim = "" Then
            MessageBox.Show("Kode Tunjangan harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtCari1.Focus() : Exit Sub
        End If

        If TxtNilaiT.Text.Trim = "" Then
            MessageBox.Show("Nilai Tunjangan harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtNilaiT.Focus() : Exit Sub
        End If

        For i As Integer = 0 To LvTunjangan.Items.Count - 1
            If TxtCari1.Text.ToUpper = LvTunjangan.Items(i).Text.ToUpper Then
                MessageBox.Show("Data yang diinput sudah ada di dalam tabel . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtCari1.Text = ""
                TxtCari1.Focus()
                Exit Sub
            End If
        Next

        xSplit = TxtRangeTJ2.Text.Split("-")

        If Val(TxtNilaiT.Text) < Val(xSplit(0).Trim) Or Val(TxtNilaiT.Text) > Val(xSplit(1).Trim) Then
            MessageBox.Show("Tunjangan harus di dalam range tunjangan !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage3 : TxtNilaiT.Text = "0" : TxtNilaiT.Focus()
            Exit Sub
        End If

        Dim StrRange As String
        Dim StrRange2 As String

        StrRange = ""
        StrRange2 = ""

        Try
            OpenConn()

            SQL = "select a.Kode_Komponen, a.Keterangan, c.range1 as RangeTJ1,c.range2 as RangeTJ2 "
            SQL = SQL & "from (HRIS_Komponen_Gaji a Left Join HRIS_Range_Tunjangan c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND a.Kode_Komponen = c.Kode_Komponen) "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.Jenis = 'I' and a.Kode_Komponen = '" & TxtCari1.Text & "' and (a.perhitungan = 'T' or a.perhitungan = '') AND "
            SQL = SQL & "c.id_level in (select id_level from HRIS_Level_Jabatan where id_level_jabatan = '" & LLevelJabatan.Item(CmbLevel.SelectedIndex) & "') and "
            SQL = SQL & "c.id_golongan_sub_golongan in (select id_golongan_sub_golongan from HRIS_Level_Jabatan where id_level_jabatan = '" & LLevelJabatan.Item(CmbLevel.SelectedIndex) & "') "
            If Strings.Trim(StrCari) = "" Then
            Else
                SQL = SQL & "and a.Kode_Komponen NOT IN (" & StrCari & ") "
            End If
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    MessageBox.Show("Kode komponen yang diinput tidak ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TxtCari1.Text = ""
                    TxtCari1.Focus()
                    Exit Sub
                Else
                    StrKet = Dr("Keterangan")
                    StrRange = Format(Dr("RangeTJ1"), "N0") & " - " & Format(Dr("RangeTJ2"), "N0")
                    StrRange2 = Dr("RangeTJ1") & " - " & Dr("RangeTJ2")
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dim Lvw As ListViewItem
        Lvw = LvTunjangan.Items.Add(TxtCari1.Text)
        Lvw.SubItems.Add(StrKet)
        Lvw.SubItems.Add(TxtNilaiT.Text)
        Lvw.SubItems.Add(StrRange)
        Lvw.SubItems.Add(StrRange2)

        Call Proses_StrCari()

        TxtCari1.Text = ""
        TxtNilaiT.Text = ""

        Dim Tanya As String = MessageBox.Show("Input data item lain . . ? ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Tanya = vbYes Then
            TxtCari1.Focus()
        End If
    End Sub

    Private Sub Proses_StrCari()
        StrCari = ""
        For i As Integer = 0 To LvTunjangan.Items.Count - 1
            StrCari = StrCari & "'" & LvTunjangan.Items(i).Text.ToUpper & "',"
        Next i
        If Trim(StrCari) = "" Then
        Else
            StrCari = Strings.Left(StrCari, Len(StrCari) - 1)
        End If
    End Sub

    Private Sub LvCari_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvCari.DoubleClick
        If LvCari.Items.Count = 0 Then Exit Sub

        Dim Kode As String = LvCari.FocusedItem.Text
        Dim StrRange As String = LvCari.FocusedItem.SubItems(2).Text
        Dim StrRange2 As String = LvCari.FocusedItem.SubItems(3).Text

        TxtCari1.Text = Kode
        TxtRangeTJ1.Text = StrRange
        TxtRangeTJ2.Text = StrRange2
        TxtNilaiT.Focus()

        LvCari.Visible = False
    End Sub


    Private Async Function Kirim_Email() As Task
        Try
            OpenConn()
            OpenConnSQL()
            Cmd.Transaction = Cn.BeginTransaction
            CmdSQL.Transaction = CnSQL.BeginTransaction

            '=================================
            '==    ADD APPROVAL I LOVE PDF  ==
            '=================================


            Dim lovePdfAPi = New LovePdfApi(PublicKey, SecretKey)

            Dim task As SignTask = lovePdfAPi.CreateTask(Of SignTask)()

            ' Tambahkan file PDF yang akan ditandatangani
            Dim file = task.AddFile(FilePath)

            Dim signParams = New SignParams()
            signParams.SubjectSigner = "C&B Karyawan Baru"
            signParams.MessageSigner = "C&B Karyawan Baru"

            signParams.SignerReminderDaysCycle = 2
            signParams.SignerReminders = True
            signParams.ExpirationDays = 30

            signParams.VerifyEnabled = True
            signParams.LockOrder = True
            signParams.UuidVisible = True

            Dim positionAwalX As String = "140" 'Fix Position 
            Dim positionAwalY As String = "-547" 'Fix Position 


            'ADD SIGN USER PENGAJUAN 

            SQLSQL = "select a.nama, a.Email, d.Keterangan as Jabatan, c.Keterangan as divisi "
            SQLSQL = SQLSQL & "from karyawan a, HRIS_Divisi_Sub_Divisi b, HRIS_Divisi c, HRIS_Jabatan d "
            SQLSQL = SQLSQL & "where a.ID_Divisi_Sub_Divisi=b.ID_Divisi_Sub_Divisi "
            SQLSQL = SQLSQL & "and b.ID_Divisi=c.ID_Divisi and a.ID_Level_Jabatan=d.ID_Jabatan "
            SQLSQL = SQLSQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.UserID='" & UserID & "'"
            Using DrSQL = OpenTransSQL(SQLSQL)
                If DrSQL.Read Then

                    Dim email As String = "galihrizkycode@gmail.com"
                    Dim jabatan As String = DrSQL("Jabatan") & " " & DrSQL("divisi")

                    Dim signerPengajuan = signParams.AddSigner(DrSQL("nama"), email)
                    signerPengajuan.AccessCode = AccessCode

                    Dim signerFilePengajuan = signerPengajuan.AddFile(file.ServerFileName)

                    ' Tambah elemen tanda tangan untuk signer pertama
                    Dim signatureElement As SignatureElement = signerFilePengajuan.AddSignature()
                    signatureElement.Position = New Position(positionAwalX, positionAwalY)
                    signatureElement.Pages = "1"
                    signatureElement.Size = 30

                    'UNTUK PENAMBAHAN ELEMEN TEKS PENDEKUNG SEPERTI MENYETUJI, NAMA, JABATAN
                    Dim generalTextElement As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText("Diajukan Oleh,")
                    generalTextElement.Position = New Position(positionAwalX, (Val(positionAwalY) + 847).ToString)
                    generalTextElement.Size = 17
                    generalTextElement.Pages = "1"

                    Dim generaElementNama As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText(DrSQL("nama"))
                    generaElementNama.Position = New Position(positionAwalX, (Val(positionAwalY) + 767).ToString)
                    generaElementNama.Size = 15
                    generaElementNama.Pages = "1"

                    Dim generaElementJabatan As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFilePengajuan.AddText(jabatan)
                    generaElementJabatan.Position = New Position(positionAwalX, (Val(positionAwalY) + 757).ToString) ' Atur posisi sesuai kebutuhan
                    generaElementJabatan.Size = 15
                    generaElementJabatan.Pages = "1"

                End If

            End Using

            SQLSQL = "select a.No_Faktur, a.User_ID as user_approve, b.Nama, b.Telepon, b.Email, e.Keterangan as Divisi, f.Keterangan as Jabatan "
            SQLSQL = SQLSQL & "from HRIS_Transaksi_Rekrutmen_Approval a, Karyawan b, HRIS_Divisi_Sub_Divisi c, HRIS_Divisi e, HRIS_Jabatan f "
            SQLSQL = SQLSQL & "where b.Kode_Perusahaan = f.Kode_Perusahaan and a.User_ID = b.Kode_Karyawan and b.ID_Divisi_Sub_Divisi=c.ID_Divisi_Sub_Divisi "
            SQLSQL = SQLSQL & "and c.ID_Divisi=e.ID_Divisi and b.ID_Level_Jabatan=f.ID_Jabatan and Acc='Y' and Tolak is null "
            SQLSQL = SQLSQL & "and a.No_Faktur='" & TxtNoFaktur.Text & "' "
            SQLSQL = SQLSQL & "order by a.Level_Hierarchy "
            Using DrSQL1 = BindingTransSQL(SQLSQL)
                With DrSQL1.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            'Dim Nama As String = If(IsDBNull(DrSQL("Nama")), String.Empty, DrSQL("Nama").ToString())
                            'Dim Email As String = If(IsDBNull(DrSQL("Email")), String.Empty, DrSQL("Email").ToString())
                            'userApprov2(index.ToString) = New List(Of String) From {Nama, Email, jabatan1, AccessCode}
                            'index = index + 1

                            Dim nama As String = .Rows(i).Item("Nama")
                            Dim email1 As String = .Rows(i).Item("Email")
                            Dim jabata1 As String = .Rows(i).Item("Jabatan") & " " & .Rows(i).Item("Divisi")


                            positionAwalX = (Val(positionAwalX) + 140).ToString


                            Dim signer As Signer = signParams.AddSigner(nama, email1)
                            signer.AccessCode = AccessCode

                            Dim signerFile1 As SignerFile = signer.AddFile(file.ServerFileName)

                            ' Tambah elemen tanda tangan untuk signer pertama
                            Dim signatureElement1 As SignatureElement = signerFile1.AddSignature()
                            signatureElement1.Position = New Position(positionAwalX, positionAwalY)
                            signatureElement1.Pages = "1"
                            signatureElement1.Size = 30

                            'UNTUK PENAMBAHAN ELEMEN TEKS PENDEKUNG SEPERTI MENYETUJI, NAMA, JABATAN
                            Dim generalTextElement1 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText("Menyetujui,")
                            generalTextElement1.Position = New Position(positionAwalX, (Val(positionAwalY) + 847).ToString)
                            generalTextElement1.Size = 17
                            generalTextElement1.Pages = "1"

                            Dim generalTextElement2 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(nama)
                            generalTextElement2.Position = New Position(positionAwalX, (Val(positionAwalY) + 767).ToString) ' Atur posisi sesuai kebutuhan
                            generalTextElement2.Size = 15
                            generalTextElement2.Pages = "1"

                            Dim generalTextElement3 As LovePdf.Model.TaskParams.Sign.Elements.TextElement = signerFile1.AddText(jabata1)
                            generalTextElement3.Position = New Position(positionAwalX, (Val(positionAwalY) + 757).ToString) ' Atur posisi sesuai kebutuhan
                            generalTextElement3.Size = 15
                            generalTextElement3.Pages = "1"


                            If positionAwalX = "420" Then
                                positionAwalX = "0"
                                positionAwalY = "-680"
                            End If
                        Next
                    End If
                End With

            End Using



            Dim signatureResponse As SignatureResponse = Await task.RequestSignatureAsync(signParams)

            ' Periksa apakah permintaan tanda tangan berhasil
            If signatureResponse Is Nothing Then
                CloseTrans()
                CloseTransSQL()
                CloseConn()
                CloseConnSQL()
                MessageBox.Show("Permintaan tanda tangan gagal.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function

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
            Exit Function
        End Try
    End Function
End Class
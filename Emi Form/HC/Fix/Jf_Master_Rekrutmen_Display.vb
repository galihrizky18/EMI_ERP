Imports System.IO
Imports System.Net
Imports System.Reflection.Emit
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar
Imports Microsoft.VisualBasic.ApplicationServices
'Imports Microsoft.Office.Interop.Excel
Public Class Jf_Master_Rekrutmen_Display
    Private Perusahaan_List As New ArrayList
    Dim LLevelJabatan As New ArrayList
    Dim LDivisiSubDivisi As New ArrayList
    Dim LKolom As New ArrayList
    Dim LTanggal As New ArrayList
    Dim StrCari As String
    Dim StrKet As String
    Dim idlevel As New ArrayList
    Dim iddivisi As New ArrayList

    Private Async Sub Jf_Master_Karyawan2_Display_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LvKaryawan.Columns.Add("No.Transaksi", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Tgl Faktur", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("User ID", 80, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Kode Calon Karyawan", 100, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Level Jabatan", 250, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Divisi/Departemen", 250, HorizontalAlignment.Left)
        LvKaryawan.Columns.Add("Lokasi", 120, HorizontalAlignment.Center)
        LvKaryawan.Columns.Add("Keterangan Approval", 250, HorizontalAlignment.Left)
        LvKaryawan.View = View.Details

        LvCari.Visible = False

        '   kirim_wa_rekrumen_karyawan()
        Kosong()

        'Await Send_Approval_Email()
        Await Send_Approval_Email(TxtNoFaktur.Text)

        Dim xxxxx As String = ""
    End Sub


    Private Sub Kosong()
        Try
            OpenConn()

            LvKaryawan.Items.Clear()

            SQL = "SELECT dbo.HRIS_Rekrutmen_Karyawan.No_Faktur, dbo.HRIS_Rekrutmen_Karyawan.Tanggal, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Jam, dbo.HRIS_Rekrutmen_Karyawan.UserID, dbo.HRIS_Rekrutmen_Karyawan.Kode_Calon, "
            SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Nama, dbo.HRIS_Rekrutmen_Karyawan.Lokasi, dbo.HRIS_Level.Keterangan AS KetLevel, "
            SQL = SQL & "dbo.HRIS_Jabatan.Keterangan AS KetJabatan, dbo.HRIS_Golongan.Keterangan AS KetGolongan, "
            SQL = SQL & "dbo.HRIS_Sub_Golongan.Keterangan AS KetSubGolongan, dbo.HRIS_Divisi.Keterangan AS KetDivisi, "
            SQL = SQL & "dbo.HRIS_Sub_Divisi.Keterangan AS KetSubDivisi "

            'SQL = SQL & ", (select STRING_AGG(b.UserID_Approval + case when b.Flag_Approval = 'Y' then ' SUDAH APPROVAL' else ' BELUM APPROVAL' end , ' || ') "
            'SQL = SQL & "as flag from HRIS_Transaksi_Rekrutmen_Approval b "
            'SQL = SQL & "where b.No_Faktur = dbo.HRIS_Rekrutmen_Karyawan.No_Faktur "
            'SQL = SQL & "and b.ID_Level_Jabatan = HRIS_Rekrutmen_Karyawan.ID_Level_Jabatan "
            'SQL = SQL & "and b.ID_Divisi_Sub_Divisi = HRIS_Rekrutmen_Karyawan.ID_Divisi_Sub_Divisi) as flag_app "

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

            SQL = SQL & "ORDER BY HRIS_Rekrutmen_Karyawan.no_faktur,HRIS_Rekrutmen_Karyawan.Kode_Calon"

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvKaryawan.Items.Add(dr("no_faktur"))
                    Lvw.SubItems.Add(Format(dr("tanggal"), "dd MMM yyyy"))
                    Lvw.SubItems.Add(dr("jam"))
                    Lvw.SubItems.Add(dr("userid"))
                    Lvw.SubItems.Add(dr("Kode_Calon"))
                    Lvw.SubItems.Add(dr("nama"))
                    Lvw.SubItems.Add(dr("ketlevel") & " | " & dr("ketjabatan") & " | " & dr("ketgolongan") & dr("ketsubgolongan"))
                    Lvw.SubItems.Add(dr("ketdivisi") & " | " & dr("ketsubdivisi"))
                    Lvw.SubItems.Add(dr("lokasi"))
                    'Lvw.SubItems.Add(dr("flag_app"))
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

            SQL = "SELECT dbo.HRIS_Level.ID_Level, dbo.HRIS_Level_Jabatan.ID_Level_Jabatan, dbo.HRIS_Level.Keterangan AS KetLevel, dbo.HRIS_Jabatan.Keterangan AS KetJabatan, "
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
                    idlevel.Add(Dr("id_level"))
                    CmbLevel.Items.Add(Dr("ketlevel") & " | " & Dr("ketjabatan") & " | " & Dr("ketgolongan") & Dr("ketsubgolongan"))
                Loop
            End Using
            CmbLevel.SelectedIndex = -1
            '=================================================================

            CmbDivisi.Items.Clear() : LDivisiSubDivisi.Clear()
            iddivisi.Clear()

            SQL = "SELECT dbo.HRIS_Divisi.ID_Divisi, dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi_Sub_Divisi, dbo.HRIS_Divisi.Keterangan AS KetDivisi, dbo.HRIS_Sub_Divisi.Keterangan AS KetSubDivisi "

            SQL = SQL & "FROM dbo.HRIS_Divisi_Sub_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi = dbo.HRIS_Divisi.ID_Divisi INNER JOIN "
            SQL = SQL & "dbo.HRIS_Sub_Divisi ON dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi = dbo.HRIS_Sub_Divisi.ID_Sub_Divisi "

            SQL = SQL & "ORDER BY dbo.HRIS_Divisi_Sub_Divisi.ID_Divisi, dbo.HRIS_Divisi_Sub_Divisi.ID_Sub_Divisi"

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    LDivisiSubDivisi.Add(Dr("id_divisi_sub_divisi"))
                    iddivisi.Add(Dr("ID_Divisi"))
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

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ChkTanggal.Checked = False : CmbTanggal.Enabled = False

        '===================
        '=    JNAGN LUPA DI UNCOMMENT
        '===================
        'Tgl1.Enabled = False : Tgl1.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        'Tgl2.Enabled = False : Tgl2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)

        Tgl1.Enabled = False : Tgl1.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        Tgl2.Enabled = False : Tgl2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)

        CmbTanggal.Items.Clear() : LTanggal.Clear()
        CmbTanggal.Items.Add("Tgl Faktur") : LTanggal.Add("Tanggal")
        CmbTanggal.Items.Add("Tgl Lahir") : LTanggal.Add("Tgl_Lahir")
        CmbTanggal.Items.Add("Tanggal OK") : LTanggal.Add("Tanggal_OK")
        CmbTanggal.SelectedIndex = -1

        ChkParameter.Checked = False : CmbKolom.Enabled = False
        TxtValue.Enabled = False

        CmbKolom.Items.Clear() : LKolom.Clear()
        CmbKolom.Items.Add("No.Transaksi") : LKolom.Add("No_Faktur")
        CmbKolom.Items.Add("Kode Calon Karyawan") : LKolom.Add("Kode_Calon")
        CmbKolom.Items.Add("Nama") : LKolom.Add("Nama")
        CmbKolom.Items.Add("Aktif") : LKolom.Add("Aktif")
        CmbKolom.SelectedIndex = -1

        '===================
        '=    JNAGN LUPA DI UNCOMMENT
        '===================
        'TglFaktur.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        TglFaktur.Value = CDate(FMenu.ToolStripStatusLabel3.Text)

        TxtNoFaktur.Text = ""
        TxtKodeKaryawan.Text = "" : TxtNamaKaryawan.Text = "" : TxtAlamat.Text = "" : TxtTelepon.Text = "" : TxtHp.Text = ""
        '===================
        '=    JNAGN LUPA DI UNCOMMENT
        '===================
        'TglLahir.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        TglLahir.Value = CDate(FMenu.ToolStripStatusLabel3.Text)


        TxtEmail.Text = ""
        TxtNIK.Text = "" : TxtNPWP.Text = "" : TxtKepala.Text = ""
        TxtValue.Text = ""
        TxtGapok.Text = "0" : TxtRangeGP1.Text = "0 - 0" : TxtRangeGP2.Text = "0 - 0" : TxtGapok.Enabled = True
        'TxtTunjangan.Text = "0" : TxtTunjangan.Enabled = True
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

            SQL = "Select a.*,c.Range1 As RangeGP1,c.range2 As RangeGP2,d.range1 As RangeTJ1,d.range2 As RangeTJ2 "
            SQL = SQL & "From HRIS_Rekrutmen_Karyawan As a left outer join HRIS_Level_Jabatan As b On a.id_level_jabatan = b.id_level_jabatan "
            SQL = SQL & "left outer join HRIS_Range_Gaji As c On b.id_golongan_sub_golongan = c.id_golongan_sub_golongan And b.id_level = c.id_level "
            SQL = SQL & "left outer join HRIS_Range_Tunjangan As d On b.id_golongan_sub_golongan = d.id_golongan_sub_golongan And b.id_level = d.id_level "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & TxtNoFaktur.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TglFaktur.Value = Dr("tanggal")
                    TxtNoFaktur.Text = Dr("no_faktur")
                    TxtKodeKaryawan.Text = Dr("Kode_Calon")
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
                    'GAJI POKOK
                    If IsDBNull(Dr("RangeGP1")) Then
                        MessageBox.Show("Range gapok untuk level jabatan belum diinput!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        TxtRangeGP1.Text = "0 - 0" : TxtRangeGP2.Text = "0 - 0"
                    Else
                        TxtRangeGP1.Text = Format(Dr("RangeGP1"), "N0") & " - " & Format(Dr("RangeGP2"), "N0")
                        TxtRangeGP2.Text = Dr("RangeGP1") & " - " & Dr("RangeGP2")
                    End If
                    TxtGapok.Text = "0"
                    'TxtTunjangan.Text = "0"
                    TxtCari1.Text = ""
                    TxtNilaiT.Text = "0"
                Else
                    TxtNamaKaryawan.Text = "" : TxtAlamat.Text = "" : TxtTelepon.Text = "" : TxtHp.Text = ""
                    TglLahir.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
                    CmbJK.SelectedIndex = -1 : CmbJenis.SelectedIndex = -1 : CmbAktif.SelectedIndex = -1
                    TxtEmail.Text = ""
                    TxtNIK.Text = "" : TxtNPWP.Text = ""
                    CmbPerusahaan.SelectedIndex = -1 : CmbPerayaan.SelectedIndex = -1 : CmbPendidikan.SelectedIndex = -1
                    CmbAgama.SelectedIndex = -1 : CmbLevel.SelectedIndex = -1 : CmbDivisi.SelectedIndex = -1
                    CmbLokasi.SelectedIndex = -1 : CmbLokasiSlipGaji.SelectedIndex = -1 : CmbStatusKaryawan.SelectedIndex = -1
                    TxtKepala.Text = "" : TxtValue.Text = ""
                    TxtGapok.Text = "0" : TxtRangeGP1.Text = "0 - 0" : TxtRangeGP2.Text = "0 - 0"
                    'TxtTunjangan.Text = "0"
                    TxtCari1.Text = "" : TxtNilaiT.Text = "0"
                End If
            End Using

            'Dim lvl_app As String = ""
            'SQL = "select Level_Approval from HRIS_UserID_Approval_Rekrutmen where UserID_Approval = '" & UserID & "' "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("Level_Approval") = 1 Then
            TxtGapok.Enabled = True
            'TxtTunjangan.Enabled = True
            TxtCari1.Enabled = True
            TxtNilaiT.Enabled = True
            '        Else
            '            TxtGapok.Enabled = False
            '            'TxtTunjangan.Enabled = False
            '            TxtCari1.Enabled = False
            '            TxtNilaiT.Enabled = False
            '        End If
            '    End If
            'End Using

            SQL = "select a.No_Faktur,b.Gaji_Pokok,b.Tunjangan from HRIS_Rekrutmen_Karyawan a,HRIS_Transaksi_Rekrutmen_Gaji b "
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
            SQL = SQL & "From (((HRIS_Transaksi_Rekrutmen_Tunjangan a Left Join HRIS_Komponen_Gaji b ON a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Komponen = b.Kode_Komponen) "
            SQL = SQL & "Left Join HRIS_Rekrutmen_Karyawan c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND a.No_Faktur = c.No_Faktur) "
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
        Catch ex As Exception
            CloseConn()
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

        SQL = "SELECT dbo.HRIS_Rekrutmen_Karyawan.No_Faktur, dbo.HRIS_Rekrutmen_Karyawan.Tanggal, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Jam, dbo.HRIS_Rekrutmen_Karyawan.UserID, dbo.HRIS_Rekrutmen_Karyawan.Kode_Calon, "
        SQL = SQL & "dbo.HRIS_Rekrutmen_Karyawan.Nama, dbo.HRIS_Rekrutmen_Karyawan.Lokasi, dbo.HRIS_Level.Keterangan AS KetLevel, "
        SQL = SQL & "dbo.HRIS_Jabatan.Keterangan AS KetJabatan, dbo.HRIS_Golongan.Keterangan AS KetGolongan, "
        SQL = SQL & "dbo.HRIS_Sub_Golongan.Keterangan AS KetSubGolongan, dbo.HRIS_Divisi.Keterangan AS KetDivisi, "
        SQL = SQL & "dbo.HRIS_Sub_Divisi.Keterangan AS KetSubDivisi "

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

        SQL = SQL & "ORDER BY HRIS_Rekrutmen_Karyawan.no_faktur,HRIS_Rekrutmen_Karyawan.Kode_Calon"
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                Dim Lvw As ListViewItem
                Lvw = LvKaryawan.Items.Add(Dr("no_faktur"))
                Lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                Lvw.SubItems.Add(Dr("jam"))
                Lvw.SubItems.Add(Dr("userid"))
                Lvw.SubItems.Add(Dr("Kode_Calon"))
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
            MessageBox.Show("Kode Calon Karyawan harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvKaryawan.Focus() : Exit Sub
        End If

        'GAJI POKOK
        If TxtGapok.Text.Trim.Length = 0 Or Val(TxtGapok.Text) = 0 Then
            MessageBox.Show("Gaji pokok belum diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage3 : TxtGapok.Focus()
            Exit Sub
        End If

        xSplit = TxtRangeGP2.Text.Split("-")

        Dim Jml_Gaji As Integer = 0

        If Val(TxtGapok.Text) < Val(xSplit(0).Trim) Or Val(TxtGapok.Text) > Val(xSplit(1).Trim) Then
            'MessageBox.Show("Gaji pokok harus di dalam range gaji!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'TabControl1.SelectedTab = TabPage3 : TxtGapok.Text = "0" : TxtGapok.Focus()
            'Exit Sub
            Jml_Gaji = 1
        End If

        Dim Jml_Tunjangan As Integer = 0

        For i As Integer = 0 To LvTunjangan.Items.Count - 1
            xSplit = LvTunjangan.Items(i).SubItems(4).Text.Split("-")

            If Val(HilangkanTanda(LvTunjangan.Items(i).SubItems(2).Text)) < Val(xSplit(0).Trim) Or Val(HilangkanTanda(LvTunjangan.Items(i).SubItems(2).Text)) > Val(xSplit(1).Trim) Then
                'MessageBox.Show(LvTunjangan.Items(i).SubItems(1).Text & " harus di dalam range tunjangan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'TabControl1.SelectedTab = TabPage3
                'Exit Sub
                Jml_Tunjangan = 1
            End If
        Next i

        Dim StrKet As String = ""

        If Jml_Gaji <> 0 Or Jml_Tunjangan <> 0 Then
            If Jml_Gaji <> 0 And Jml_Tunjangan = 0 Then
                StrKet = "Gaji"
            ElseIf Jml_Gaji = 0 And Jml_Tunjangan <> 0 Then
                StrKet = "Tunjangan"
            ElseIf Jml_Gaji <> 0 And Jml_Tunjangan <> 0 Then
                StrKet = "Gaji dan Tunjangan"
            End If

            Dim Tanya As String = MessageBox.Show("" & StrKet & " tidak dalam range " & StrKet & Chr(13) &
                                                  "Yakin AKan Disimpan ?", "Validasi Rekrutmen Karyawan", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Tanya = vbNo Then Exit Sub
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

        'For i As Integer = 0 To LvTunjangan.Items.Count - 1
        '    xSplit = LvTunjangan.Items(i).SubItems(4).Text.Split("-")

        '    If Val(HilangkanTanda(LvTunjangan.Items(i).SubItems(2).Text)) < Val(xSplit(0).Trim) Or Val(HilangkanTanda(LvTunjangan.Items(i).SubItems(2).Text)) > Val(xSplit(1).Trim) Then
        '        MessageBox.Show(LvTunjangan.Items(i).SubItems(1).Text & " harus di dalam range tunjangan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        TabControl1.SelectedTab = TabPage3
        '        Exit Sub
        '    End If
        'Next i

        Dim Yakin As String = MessageBox.Show("Anda yakin akan validasi data ini?" & Chr(13) & Chr(13) &
                                                "No Faktur : " & TxtNoFaktur.Text & Chr(13) &
                                                "Atas nama : " & TxtNamaKaryawan.Text & " [" &
                                                LvKaryawan.FocusedItem.SubItems(4).Text & "]", "Validasi Rekrutmen Karyawan", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Yakin = vbNo Then Exit Sub

        get_jam()

        '===============================
        '=    JANGAN LUPA UNCOMMNET    =
        '===============================
        'Try
        '    OpenConn()
        '    OpenConnSQL()
        '    Cmd.Transaction = Cn.BeginTransaction
        '    CmdSQL.Transaction = CnSQL.BeginTransaction

        '    Dim hasil As Integer = Approval_Hierarchy("Approval Rekrutmen", idlevel.Item(CmbLevel.SelectedIndex), iddivisi.Item(CmbDivisi.SelectedIndex))

        '    If hasil = 0 Then
        '        CloseTrans()
        '        CloseTransSQL()
        '        CloseConn()
        '        CloseConnSQL()
        '        MessageBox.Show("Data approval tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    Else

        '        Dim rand As New Random
        '        Dim Kd_unik As String = ""
        '        Kd_unik = Format(CDate(FMenu.ToolStripStatusLabel3.Text), "Mdd") & Format(rand.Next(0, 100000), "00000")

        '        Dim level As Integer = 1
        '        For a As Integer = 0 To Data_User_App.Count - 1
        '            Dim fHp As String = ""
        '            Dim fpin As String = ""

        '            SQL = "select HP,pin,userid from Users where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & Data_User_App.Item(a) & "' "
        '            Using Dr = OpenTrans(SQL)
        '                If Dr.Read Then
        '                    fHp = Dr("HP")
        '                    fpin = Dr("Pin")
        '                Else
        '                    Dr.Close()
        '                    CloseTrans()
        '                    CloseTransSQL()
        '                    CloseConn()
        '                    CloseConnSQL()
        '                    MessageBox.Show("user tidak dapat ditemukan ...!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                End If
        '            End Using

        '            SQLSQL = "insert into HRIS_Transaksi_Rekrutmen_Approval(No_Faktur,User_ID,"
        '            SQLSQL = SQLSQL & "Level_Hierarchy,pin,kode_unik, Nama, Email, Jabatan, Divisi, hp) values('" & TxtNoFaktur.Text.Trim & "',"
        '            SQLSQL = SQLSQL & "'" & Data_User_App.Item(a) & "','" & level & "','" & fpin & "','" & Kd_unik & "', "
        '            SQLSQL = SQLSQL & "'" & Data_Nama_App.Item(a) & "', '" & Data_Email_App.Item(a) & "', '" & Data_Jabatan_App.Item(a) & "', '" & Data_Divisi_App.Item(a) & "', '" & fHp & "')"
        '            ExecuteTransSQL(SQLSQL)

        '            level = level + 1
        '        Next

        '        SQL = "INSERT INTO HRIS_Transaksi_Rekrutmen_Gaji(Kode_Perusahaan,No_Faktur,Kode_Calon,Gaji_Pokok,Tunjangan) VALUES("
        '        SQL = SQL & "'" & KodePerusahaan & "','" & TxtNoFaktur.Text & "','" & TxtKodeKaryawan.Text & "',"
        '        SQL = SQL & "'" & TxtGapok.Text & "','" & TxtTunjangan.Text & "')"
        '        ExecuteTrans(SQL)

        '        For i As Integer = 0 To LvTunjangan.Items.Count - 1
        '            SQL = "Insert into HRIS_Transaksi_Rekrutmen_Tunjangan(Kode_Perusahaan,No_Faktur,Kode_Calon,Kode_Komponen,Tunjangan) "
        '            SQL = SQL & "Values('" & KodePerusahaan & "','" & TxtNoFaktur.Text & "','" & TxtKodeKaryawan.Text & "',"
        '            SQL = SQL & "'" & LvTunjangan.Items(i).Text.ToUpper & "'," & LvTunjangan.Items(i).SubItems(2).Text & ")"
        '            ExecuteTrans(SQL)
        '        Next i

        '        'SQLSQL = "update HRIS_Transaksi_Rekrutmen_Approval set Acc = 'Y',"
        '        'SQLSQL = SQLSQL & "Tanggal = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
        '        'SQLSQL = SQLSQL & "Jam = '" & Format(tgl_skg, "HH:mm:ss") & "' where "
        '        'SQLSQL = SQLSQL & "No_Faktur = '" & TxtNoFaktur.Text & "' "
        '        'SQLSQL = SQLSQL & "and User_ID = '" & UserID & "' "
        '        'ExecuteTransSQL(SQLSQL)

        '        SQL = "Update HRIS_Rekrutmen_Karyawan Set Flag_OK = 'Y', Tanggal_OK = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
        '        SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtNoFaktur.Text & "'"
        '        ExecuteTrans(SQL)

        '        xSplit = TxtRangeGP2.Text.Split("-")
        '        Dim fLebih As String = "T"
        '        If Val(TxtGapok.Text) > Val(xSplit(1).Trim) Then
        '            fLebih = "Y"
        '        End If

        '        'TUNJANGAN
        '        xSplit = TxtRangeTJ2.Text.Split("-")

        '        For i As Integer = 0 To LvTunjangan.Items.Count - 1
        '            xSplit = LvTunjangan.Items(i).SubItems(4).Text.Split("-")
        '            If Val(HilangkanTanda(LvTunjangan.Items(i).SubItems(2).Text)) > Val(xSplit(1).Trim) Then
        '                fLebih = "Y"
        '            End If
        '        Next i

        '        If fLebih = "Y" Then


        '            'Dim rand As New Random
        '            'Dim Kd_unik As String = ""
        '            Kd_unik = Format(CDate(FMenu.ToolStripStatusLabel3.Text), "Mdd") & Format(rand.Next(0, 100000), "00000")

        '            Dim rand2 As New Random
        '            Dim Kd_unik2 As String = ""
        '            Kd_unik2 = Format(rand2.Next(0, 100000), "000000")

        '            Dim hasil_flag_khusus As Integer = Approval_Hierarchy("Approval Rekrutmen", idlevel.Item(CmbLevel.SelectedIndex), iddivisi.Item(CmbDivisi.SelectedIndex), "Y")

        '            'Dim level As Integer = 1
        '            'For a As Integer = 0 To Data_User_App.Count - 1
        '            '    Dim fHp As String = ""
        '            '    Dim fpin As String = ""

        '            '    SQL = "select HP,pin,userid from Users where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & Data_User_App.Item(a) & "' "
        '            '    Using Dr = OpenTrans(SQL)
        '            '        If Dr.Read Then
        '            '            fHp = Dr("HP")
        '            '            fpin = Dr("Pin")
        '            '        Else
        '            '            Dr.Close()
        '            '            CloseTrans()
        '            '            CloseTransSQL()
        '            '            CloseConn()
        '            '            CloseConnSQL()
        '            '            MessageBox.Show("user tidak dapat ditemukan ...!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            '            Exit Sub
        '            '        End If
        '            '    End Using

        '            '    SQLSQL = "insert into HRIS_Transaksi_Rekrutmen_Approval(No_Faktur,User_ID,"
        '            '    SQLSQL = SQLSQL & "Level_Hierarchy,pin,kode_unik, Nama, Email, Jabatan, Divisi) values('" & TxtNoFaktur.Text.Trim & "',"
        '            '    SQLSQL = SQLSQL & "'" & Data_User_App.Item(a) & "','" & level & "','" & fpin & "','" & Kd_unik & "', "
        '            '    SQL = SQL & "'" & Data_Nama_App.Item(a) & "', '" & Data_Email_App.Item(a) & "', '" & Data_Jabatan_App.Item(a) & "', '" & Data_Divisi_App.Item(a) & "')"
        '            '    ExecuteTransSQL(SQLSQL)

        '            '    level = level + 1
        '            'Next

        '            If hasil_flag_khusus = 0 Then
        '            Else
        '                'Dim level As Integer = 0
        '                level = 0
        '                SQL = "select count(Id) as id from HRIS_Transaksi_Rekrutmen_Approval where No_Faktur = '" & TxtNoFaktur.Text.Trim & "'"
        '                Using Dr = OpenTrans(SQL)
        '                    If Dr.Read Then
        '                        level = Dr("id") + 1
        '                    End If
        '                End Using

        '                'Dim fuser As String = .Rows(h).Item("UserID_Approval")
        '                For vv As Integer = 0 To Data_User_App_Flag_Khusus.Count - 1
        '                    SQLSQL = "select User_ID from HRIS_Transaksi_Rekrutmen_Approval where "
        '                    SQLSQL = SQLSQL & "kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtNoFaktur.Text.Trim & "' and "
        '                    SQLSQL = SQLSQL & "userid = '" & Data_User_App_Flag_Khusus.Item(vv) & "'"
        '                    Using Dr = OpenTransSQL(SQLSQL)
        '                        If Dr.Read Then
        '                            'tdk usa ada coding. bypass karena kalo user sdh ada di tbl approval ga usa 2x insert
        '                        Else
        '                            Dr.Close()

        '                            Dim fHp As String = ""
        '                            Dim fpin As String = ""

        '                            SQL = "select HP,pin,userid from Users where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & Data_User_App_Flag_Khusus.Item(vv) & "' "
        '                            Using Dr2 = OpenTrans(SQL)
        '                                If Dr2.Read Then
        '                                    fHp = Dr2("HP")
        '                                    fpin = Dr2("Pin")
        '                                Else
        '                                    Dr2.Close()
        '                                    CloseTrans()
        '                                    CloseTransSQL()
        '                                    CloseConn()
        '                                    CloseConnSQL()
        '                                    MessageBox.Show("user tidak dapat ditemukan ...!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                                    Exit Sub
        '                                End If
        '                            End Using


        '                            SQLSQL = "insert into HRIS_Transaksi_Rekrutmen_Approval(No_Faktur,User_ID,"
        '                            SQLSQL = SQLSQL & "Level_Hierarchy,pin,kode_unik,flag_khusus, Nama, Email, Jabatan, Divisi, hp) values('" & TxtNoFaktur.Text.Trim & "',"
        '                            SQLSQL = SQLSQL & "'" & Data_User_App_Flag_Khusus.Item(vv) & "',"
        '                            SQLSQL = SQLSQL & "'" & level & "','" & Kd_unik2 & "','" & Kd_unik & "','Y', "
        '                            SQLSQL = SQLSQL & "'" & Data_Nama_App.Item(vv) & "', '" & Data_Email_App.Item(vv) & "', '" & Data_Jabatan_App.Item(vv) & "', '" & Data_Divisi_App.Item(vv) & "', '" & fHp & "')"
        '                            ExecuteTransSQL(SQLSQL)

        '                            level = level + 1
        '                        End If
        '                    End Using
        '                Next


        '                'SQL = "select a.UserID_Approval,b.ID_Level_Jabatan,b.ID_Divisi_Sub_Divisi,d.Keterangan "
        '                'SQL = SQL & "from HRIS_UserID_Approval_Karyawan a,Karyawan b,HRIS_Level_Jabatan c,HRIS_Level d "
        '                'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.UserID_Approval = b.UserID "
        '                'SQL = SQL & "and b.ID_Level_Jabatan = c.ID_Level_Jabatan and c.ID_Level = d.ID_Level "
        '                'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                'SQL = SQL & "and a.Jenis_Approval = 'Approval Rekrutmen' "
        '                'SQL = SQL & "and a.ID_Level = '" & idlevel.Item(CmbLevel.SelectedIndex) & "' "
        '                'SQL = SQL & "and a.ID_Divisi = '" & iddivisi.Item(CmbDivisi.SelectedIndex) & "' and a.Flag_Khusus = 'Y' "
        '                'Using Ds = BindingTrans(SQL)
        '                '    With Ds.Tables("MyTable")
        '                '        If .Rows.Count <> 0 Then
        '                '            For h As Integer = 0 To .Rows.Count - 1
        '                '                Dim fuser As String = .Rows(h).Item("UserID_Approval")

        '                '                SQLSQL = "select User_ID from HRIS_Transaksi_Rekrutmen_Approval where No_Faktur = '" & TxtNoFaktur.Text.Trim & "' "
        '                '                Using DsSQL = BindingTransSQL(SQLSQL)
        '                '                    With DsSQL.Tables("MyTable")
        '                '                        If .Rows.Count <> 0 Then
        '                '                            For a As Integer = 0 To .Rows.Count - 1
        '                '                                If fuser <> .Rows(a).Item("User_ID") Then
        '                '                                    SQLSQL = "insert into HRIS_Transaksi_Rekrutmen_Approval(No_Faktur,User_ID,"
        '                '                                    SQLSQL = SQLSQL & "Level_Hierarchy,pin,kode_unik,flag_khusus) values('" & TxtNoFaktur.Text.Trim & "',"
        '                '                                    SQLSQL = SQLSQL & "'" & fuser & "',"
        '                '                                    SQLSQL = SQLSQL & "'" & level & "','" & Kd_unik2 & "','" & Kd_unik & "','Y')"
        '                '                                    ExecuteTransSQL(SQLSQL)

        '                '                                    level = level + 1
        '                '                                End If
        '                '                            Next
        '                '                        End If
        '                '                    End With
        '                '                End Using
        '                '            Next
        '                '        Else
        '                '            CloseTrans()
        '                '            CloseTransSQL()
        '                '            CloseConn()
        '                '            CloseConnSQL()
        '                '            MessageBox.Show("Data approval tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                '            Exit Sub
        '                '        End If
        '                '    End With
        '                'End Using
        '            End If
        '        End If

        '        SQL = "Insert Into Calon_Karyawan (kode_perusahaan,Kode_Calon,nama,alamat,telepon,hp,jenis,aktif,kode_jabatan,email,"
        '        SQL = SQL & "cabang_untuk_gaji,npwp,nik,status_karyawan,jenis_perayaan,lokasi,"
        '        SQL = SQL & "id_perusahaan,pendidikan_terakhir,agama,"
        '        SQL = SQL & "kode_divisi,tgl_lahir,jenis_kelamin,"
        '        SQL = SQL & "id_level_jabatan,id_divisi_sub_divisi)"

        '        SQL = SQL & "Select kode_perusahaan,Kode_Calon,nama,alamat,telepon,hp,jenis,aktif,kode_jabatan,email,"
        '        SQL = SQL & "cabang_untuk_gaji,npwp,nik,status_karyawan,jenis_perayaan,lokasi,"
        '        SQL = SQL & "id_perusahaan,pendidikan_terakhir,agama, "
        '        SQL = SQL & "kode_divisi,tgl_lahir,jenis_kelamin,"
        '        SQL = SQL & "id_level_jabatan,id_divisi_sub_divisi "

        '        SQL = SQL & "From HRIS_Rekrutmen_Karyawan "

        '        SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtNoFaktur.Text & "'"
        '        ExecuteTrans(SQL)
        '    End If

        '    Cmd.Transaction.Commit()
        '    CmdSQL.Transaction.Commit()
        '    CloseConn()
        '    CloseConnSQL()
        'Catch ex As Exception
        '    CloseConn()
        '    CloseConnSQL()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'kirim_wa_rekrumen_karyawan()

        Kosong()
        'Await Send_Approval_Email()
        Await Send_Approval_Email(TxtNoFaktur.Text)

        StrCari = ""
    End Sub

    Private Sub TxtCari1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCari1.KeyPress
        If e.KeyChar = Chr(13) Then TxtNilaiT.Focus()
    End Sub

    'Private Sub TxtTunjangan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTunjangan.KeyPress
    '    If e.KeyChar = Chr(13) Then BtnSimpan_Click(TxtGapok, e)
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("-"))) Then e.KeyChar = Chr(0)
    'End Sub

    Private Sub TxtCari1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCari1.TextChanged
        If TxtKodeKaryawan.Text.Trim = "" Then Exit Sub

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

            SQL = "select a.Kode_Komponen, a.Keterangan "
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

        'xSplit = TxtRangeTJ2.Text.Split("-")

        'If Val(TxtNilaiT.Text) < Val(xSplit(0).Trim) Or Val(TxtNilaiT.Text) > Val(xSplit(1).Trim) Then
        '    MessageBox.Show("Tunjangan harus di dalam range tunjangan !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    TabControl1.SelectedTab = TabPage3 : TxtNilaiT.Text = "0" : TxtNilaiT.Focus()
        '    Exit Sub
        'End If

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

        TxtCari1.Text = Kode
        TxtNilaiT.Focus()

        LvCari.Visible = False
    End Sub


    Private Sub LvCari_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvCari.SelectedIndexChanged

    End Sub

    Private Sub TxtGapok_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtGapok.TextChanged

    End Sub

    Private Sub LvKaryawan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvKaryawan.SelectedIndexChanged

    End Sub

    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub TxtNilaiT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNilaiT.TextChanged

    End Sub

    Private Sub TxtNoFaktur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNoFaktur.TextChanged

    End Sub

    Private Sub LvTunjangan_DoubleClick(sender As Object, e As EventArgs) Handles LvTunjangan.DoubleClick
        If LvTunjangan.Items.Count = 0 Then Exit Sub

        TxtCari1.Text = LvTunjangan.FocusedItem.Text.ToUpper
        TxtNilaiT.Text = LvTunjangan.FocusedItem.SubItems(2).Text
        LvTunjangan.FocusedItem.Remove()

        Proses_StrCari()
        LvCari.Visible = False
        TxtCari1.Focus()
    End Sub

    Private Sub LvTunjangan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvTunjangan.SelectedIndexChanged

    End Sub
End Class
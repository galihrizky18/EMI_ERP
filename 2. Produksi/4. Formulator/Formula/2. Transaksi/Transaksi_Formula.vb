Imports System.Windows.Forms.VisualStyles.VisualStyleElement


Public Class Transaksi_Formula
    Public isi_barang As Boolean = False
    Dim arrcari, arrIdPenanggungJawab As New ArrayList
    Dim Jenis = "Transaksi_Formulator"

    Dim lvNo As String
    Dim lvTipe As String
    Dim lvKdBarang As String
    Dim lvNama As String
    Dim lvQty As String
    Dim lvSatuan As String
    Dim lvPengali As String
    Dim lvSatuanBarang As String
    Dim lvNilaiBarang As String
    Dim lvPersentase As String
    Dim lvKet As String
    Dim lvQty_SatHasil As String
    Dim lvSatHasil As String

    Dim cellNo As Integer = 0
    Public cellKdBarang As Integer = 1
    Public cellNama As Integer = 2
    Public Property cellQty As Integer = 3
    Public cellSatuan As Integer = 4
    Public cellPengali As Integer = 5
    Public cellSatuanBarang As Integer = 6
    Public cellNilaiBarang As Integer = 7
    Public Property cellPersentase As Integer = 8
    Dim cellKet As Integer = 9
    Public cellQty_SatHasil As Integer = 10
    Dim cellSatHasil As Integer = 11

    Dim lv2KdUji As String
    Dim lv2Ket As String
    Dim lv2Satuan As String
    Dim lv2Hasil As String

    Public cell2KdUji As Integer = 0
    Public cell2Ket As Integer = 1
    Public cell2Satuan As Integer = 2
    Public cell2Hasil As Integer = 3


    Private Sub get_no_faktur()
        TxtFormulator_NoFaktur.Text = fTransFormula & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Transaksi_Formulator", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fTransFormula) + 4 & ")", fTransFormula & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub get_no_faktur_binding()
        Txt_No_Faktur_Binding.Text = fTransFormulaBinding & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Transaksi_Formulator_Binding", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fTransFormulaBinding) + 4 & ")", fTransFormulaBinding & Format(tgl_skg, "MMyy"))
    End Sub

    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If

        Return hasil
    End Function

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        lvNo = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellNo).Value)
        lvKdBarang = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellKdBarang).Value)
        lvNama = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellNama).Value)
        lvQty = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellQty).Value)
        lvSatuan = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellSatuan).Value)
        lvPengali = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellPengali).Value)
        lvSatuanBarang = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellSatuanBarang).Value)
        lvNilaiBarang = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellNilaiBarang).Value)
        lvPersentase = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellPersentase).Value)
        lvKet = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellKet).Value)
        lvQty_SatHasil = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellQty_SatHasil).Value)
    End Sub

    Public Sub Kosong()
        DgvFormulator_StepFormulator.Rows.Clear()
        DgvFormulator_StepFormulator.Rows.Add(1)
        TxtFormulator_Total.Text = ""
        TxtFormulator_TotalPersen.Text = ""
        TxtFormulator_KodeBarang.Text = ""
        TxtFormulator_NamaBarang.Text = ""
        TxtFormulator_NoFaktur.Text = ""
        TxtFormulator_Hasil.Text = ""
        Txt_No_Faktur_Binding.Text = ""

        CmbFormulator_PenanggungJawab.SelectedIndex = -1
        'CmbFormulator_LokasiInquiry.SelectedIndex = -1
        'CmbFormulator_LokasiBarang.SelectedIndex = -1
        CmbFormulator_SatuanHasil.SelectedIndex = -1
        ListView1.Visible = False

        get_jam()

        Try
            OpenConn()

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CmbFormulator_LokasiInquiry.Focus()
    End Sub

    Private Sub Rekap()

        Dim jumlah_Stage As Integer = 1
        For index = 0 To DgvFormulator_StepFormulator.Rows.Count - 1
            Get_Isi_Listview(index)
            DgvFormulator_StepFormulator.Rows(index).Cells(cellNo).Value = index + 1

            If lvTipe = "Stage" Then
                DgvFormulator_StepFormulator.Rows(index).Cells(cellNama).Value = "Step " & jumlah_Stage
                jumlah_Stage += 1
            End If
        Next
    End Sub

    Private Sub Total()

        Dim Total As Double = 0
        Dim TotalPersen As Double = 0


        For index = 0 To DgvFormulator_StepFormulator.Rows.Count - 1
            Get_Isi_Listview(index)

            If IsNumeric(lvQty) = True Then
                Total += Val(HilangkanTanda(lvQty_SatHasil))
                TotalPersen += Val(HilangkanTanda(lvPersentase))
            End If
        Next

        'TxtFormulator_Total.Text = Format(Total, "N0")
        'TxtFormulator_TotalPersen.Text = Format(TotalPersen, "N0")
        TxtFormulator_Total.Text = Format(Total, "N2")
        TxtFormulator_TotalPersen.Text = Format(TotalPersen, "N2")
    End Sub
    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Transaksi_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            'TabPage1.Text = Base_Language.Lang_Global_Bahan
            ListView1.Location = New Point(777, 122)
            LblFormulator_Total.Text = Base_Language.Lang_Global_Total
            LblFormulator_TotalPersen.Text = Base_Language.Lang_Global_TotalPersen
            LblFormulator_Judul.Text = Base_Language.Lang_TransFormula_Judul
            LblFormulator_KodeBarang.Text = Base_Language.Lang_Global_KodeBarang
            LblFormulator_NamaBarang.Text = Base_Language.Lang_Global_NamaBarang
            LblFormulator_NoFaktur.Text = Base_Language.Lang_Global_NoFaktur
            LblFormulator_PenanggungJawab.Text = Base_Language.Lang_TransFormula_PenanggungJawab
            LblFormulator_Tanggal.Text = Base_Language.Lang_Global_Tanggal
            LblFormulator_LokasiInquiry.Text = Base_Language.Lang_Global_Lokasi
            LblFormulator_LokasiBarang.Text = Base_Language.Lang_Global_LokasiGudang
            LblFormulator_Hasil.Text = Base_Language.Lang_Global_Hasil

            BtnFormulator_Refresh.Text = Base_Language.Lang_Global_Refresh
            BtnFormulator_Simpan.Text = Base_Language.Lang_Global_Simpan

            DgvFormulator_StepFormulator.Columns(cellNo).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_No
            'DgvFormulator_StepFormulator.Columns(cellTipe).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Tipe
            DgvFormulator_StepFormulator.Columns(cellKdBarang).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_KdBarang
            DgvFormulator_StepFormulator.Columns(cellNama).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Nama
            DgvFormulator_StepFormulator.Columns(cellQty).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Qty
            DgvFormulator_StepFormulator.Columns(cellSatuan).HeaderText = Base_Language.Lang_Global_Satuan
            DgvFormulator_StepFormulator.Columns(cellPengali).HeaderText = Base_Language.Lang_Global_Nilai_Pengali
            DgvFormulator_StepFormulator.Columns(cellSatuanBarang).HeaderText = Base_Language.Lang_Global_Satuan_Barang
            DgvFormulator_StepFormulator.Columns(cellNilaiBarang).HeaderText = Base_Language.Lang_Global_Nilai_Barang
            DgvFormulator_StepFormulator.Columns(cellPersentase).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Persentase
            DgvFormulator_StepFormulator.Columns(cellKet).HeaderText = Base_Language.Lang_TransFormula_DgvStepFormula_Keterangan

            CmbFormulator_PenanggungJawab.Items.Clear() : arrIdPenanggungJawab.Clear()
            SQL = "select Nama,Id_Karyawan from Emi_Karyawan a, Emi_Jabatan_Internal b where a.id_jabatan=b.id_jabatan "
            SQL = SQL & "and b.flag_Tampil_Formulator='Y' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbFormulator_PenanggungJawab.Items.Add(dr("Nama")) : arrIdPenanggungJawab.Add(dr("Id_Karyawan"))
                Loop
            End Using

            CmbFormulator_LokasiInquiry.Items.Clear()

            SQL = "select Kode_Stock_Owner from stock_owner where Aktif='Y' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbFormulator_LokasiInquiry.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using

            CmbFormulator_LokasiInquiry.Text = Lokasi

            CmbFormulator_LokasiBarang.Items.Clear()

            SQL = "select Kode_Stock_Owner from view_lokasi_Stock where Aktif='Y' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbFormulator_LokasiBarang.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using

            Dim lokasi_gudang As String = ""
            SQL = "Select Kode_Stock_Owner_Gudang from Binding_Lokasi_Gudang where "
            SQL = SQL & "Kode_Stock_Owner='" & Lokasi & "' and Gudang_Default='Y' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    lokasi_gudang = dr("Kode_Stock_Owner_Gudang")
                End If
            End Using
            CmbFormulator_LokasiBarang.Text = lokasi_gudang

            CmbFormulator_SatuanHasil.Items.Clear()

            SQL = "select Satuan from EMI_Satuan where Flag_Tampil_Berat='Y' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbFormulator_SatuanHasil.Items.Add(dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub

    Private Sub DgvFormulator_StepFormulator_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFormulator_StepFormulator.CellEndEdit
        If DgvFormulator_StepFormulator.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DgvFormulator_StepFormulator.CurrentRow.Index
        Dim currentCell = DgvFormulator_StepFormulator.CurrentCellAddress.X

        Dim data = DgvFormulator_StepFormulator.Rows(currentRow).Cells(currentCell)

        If currentCell = cellQty Or currentCell = cellPersentase Then
            If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If currentRow <> DgvFormulator_StepFormulator.Rows.Count - 1 Then
                    BeginInvoke(New MethodInvoker(Sub() DgvFormulator_StepFormulator.Rows.RemoveAt(e.RowIndex)))
                End If
                TxtFormulator_KodeBarang.Focus()
                Exit Sub
            ElseIf TxtFormulator_Hasil.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Hasil & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If currentRow <> DgvFormulator_StepFormulator.Rows.Count - 1 Then
                    BeginInvoke(New MethodInvoker(Sub() DgvFormulator_StepFormulator.Rows.RemoveAt(e.RowIndex)))
                End If
                TxtFormulator_Hasil.Focus()
                Exit Sub
            End If
        End If

        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKdBarang).Value = ""
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNama).Value = ""
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Value = ""
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuan).Value = ""
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPengali).Value = ""
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuanBarang).Value = ""
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).Value = ""
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value = ""
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKet).Value = ""

        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKdBarang).ReadOnly = True
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNama).ReadOnly = True
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).ReadOnly = False
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuan).ReadOnly = True
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPengali).ReadOnly = True
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuanBarang).ReadOnly = True
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).ReadOnly = True
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).ReadOnly = True
        'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKet).ReadOnly = False

        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKdBarang).Style.BackColor = Color.White
        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNama).Style.BackColor = Color.White
        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Style.BackColor = Color.LightGray
        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuan).Style.BackColor = Color.White
        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPengali).Style.BackColor = Color.White
        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellSatuanBarang).Style.BackColor = Color.White
        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).Style.BackColor = Color.White
        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Style.BackColor = Color.White
        DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellKet).Style.BackColor = Color.LightGray

        'isi_barang = False
        'SD_Pilih_Barang.lokasi()
        'SD_Pilih_Barang.asal = Jenis
        'SD_Pilih_Barang.filter_tambahan = " and b.Flag_Tampil_Formulator='Y' "
        'SD_Pilih_Barang.CmbPilihBarang_Lokasi.Text = CmbFormulator_LokasiBarang.Text
        'SD_Pilih_Barang.ShowDialog()

        'If isi_barang = False Then

        '    BeginInvoke(New MethodInvoker(Sub() DgvFormulator_StepFormulator.Rows.RemoveAt(e.RowIndex)))

        'End If

        If currentCell = cellQty Or currentCell = cellPersentase Then
            Get_Isi_Listview(currentRow)
            If IsNumeric(lvQty) = False Or Val(lvQty) < 0 Then
                DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Value = ""
                Exit Sub
            End If

            'If IsNumeric(lvPersentase) = False Or Val(lvPersentase) < 0 Then
            '    DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value = ""
            '    Exit Sub
            'End If

            Get_Isi_Listview(currentRow)

            If currentCell = cellQty Then
                DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).Value = Val(lvQty) * Val(lvPengali)

                Dim nilai_satuan_hasil As Double = 0
                Try
                    OpenConn()

                    If lvQty.Contains(",") Then
                        CloseConn()
                        MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        DgvFormulator_StepFormulator.CurrentRow.Cells(cellQty).Value = ""
                        Exit Sub
                    End If

                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
                    SQL = SQL & "'" & lvSatuan & "','" & CmbFormulator_SatuanHasil.Text & "',"
                    SQL = SQL & "'" & lvQty & "') as Hasil "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If General_Class.CekNULL(dr("Hasil")) <> "" Then
                                If dr("Hasil") = 0 Then
                                    CloseConn()
                                    MessageBox.Show("Satuan " & lvSatuan & " Ke " & CmbFormulator_SatuanHasil.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    DgvFormulator_StepFormulator.CurrentRow.Cells(cellQty).Value = ""
                                    Exit Sub
                                Else
                                    nilai_satuan_hasil = dr("hasil")
                                End If
                            Else
                                CloseConn()
                                MessageBox.Show("Satuan " & lvSatuan & " Ke " & CmbFormulator_SatuanHasil.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End Using
                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty_SatHasil).Value = Format(nilai_satuan_hasil, "N5")
                DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty_SatHasil).Value = Format(nilai_satuan_hasil, "N2")
                'DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value = Format((nilai_satuan_hasil * 100) / Val(TxtFormulator_Hasil.Text), "N5")
                DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value = Format((nilai_satuan_hasil * 100) / Val(TxtFormulator_Hasil.Text), "N2")
            End If

            'If currentCell = cellPersentase Then
            '    Dim nilai_satuan_hasil As Double = 0
            '    Try
            '        OpenConn()

            '        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
            '        SQL = SQL & "'" & lvSatuan & "','" & CmbFormulator_SatuanHasil.Text & "',"
            '        SQL = SQL & "1) as Hasil "
            '        Using dr = OpenTrans(SQL)
            '            If dr.Read Then
            '                If General_Class.CekNULL(dr("Hasil")) <> "" Then
            '                    If dr("Hasil") = 0 Then
            '                        MessageBox.Show("Satuan " & lvSatuan & " Ke " & CmbFormulator_SatuanHasil.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                        Exit Sub
            '                    Else
            '                        nilai_satuan_hasil = dr("hasil")
            '                    End If
            '                Else
            '                    MessageBox.Show("Satuan " & lvSatuan & " Ke " & CmbFormulator_SatuanHasil.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                    Exit Sub
            '                End If
            '            End If
            '        End Using
            '        CloseConn()
            '    Catch ex As Exception
            '        CloseConn()
            '        MessageBox.Show(ex.Message)
            '        Exit Sub
            '    End Try

            '    Dim Persen As Decimal
            '    Dim Qty As Double

            '    Persen = Val(HilangkanTanda(DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellPersentase).Value))

            '    DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Value = Format(Persen * Val(TxtFormulator_Hasil.Text) / (100 * (nilai_satuan_hasil)), "N0")

            '    Qty = Val(HilangkanTanda(DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty).Value))

            '    DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellQty_SatHasil).Value = Format(Qty * nilai_satuan_hasil, "N5")
            '    DgvFormulator_StepFormulator.Rows(currentRow).Cells(cellNilaiBarang).Value = Qty * Val(lvPengali)
            'End If
        End If


        Dim kuantity As String = DgvFormulator_StepFormulator.CurrentRow.Cells(cellQty).Value

        Dim nilai As Decimal = Decimal.Parse(kuantity)
        Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))


        DgvFormulator_StepFormulator.CurrentRow.Cells(cellQty).Value = formattedValue






        Rekap()
        Total()
    End Sub

    Private Sub DgvFormulator_StepFormulator_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DgvFormulator_StepFormulator.CellPainting
        If DgvFormulator_StepFormulator.Rows.Count = 0 Or e.RowIndex = -1 Then
            Exit Sub
        End If

        If (e.ColumnIndex = cellKdBarang) Then
            e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
        End If

        If (e.ColumnIndex = cellNama) Then
            e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
        End If

        If (e.ColumnIndex = cellQty) Then
            e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
        End If

        If (e.ColumnIndex = cellSatuan) Then
            e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
        End If

        If (e.ColumnIndex = cellPengali) Then
            e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
        End If
        If (e.ColumnIndex = cellSatuanBarang) Then
            e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
        End If
        If (e.ColumnIndex = cellNilaiBarang) Then
            e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
        End If

        If (e.ColumnIndex = cellPersentase) Then
            e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
        End If
    End Sub

    Private Sub DgvFormulator_StepFormulator_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs)
        Rekap()
    End Sub

    Private Sub ComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim combo As ComboBox = CType(sender, ComboBox)
        Dim rowI As Integer = DgvFormulator_StepFormulator.CurrentCell.RowIndex

    End Sub

    Private Sub BtnFormulator_Simpan_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Simpan.Click

        If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtFormulator_KodeBarang.Focus() : Exit Sub
        ElseIf CmbFormulator_LokasiInquiry.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Lokasi & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbFormulator_LokasiInquiry.Focus() : Exit Sub
        ElseIf CmbFormulator_PenanggungJawab.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_TransFormula_PenanggungJawab & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbFormulator_PenanggungJawab.Focus() : Exit Sub
        ElseIf DgvFormulator_StepFormulator.Rows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            DgvFormulator_StepFormulator.Focus() : Exit Sub
        ElseIf TxtFormulator_Hasil.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Hasil & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtFormulator_Hasil.Focus() : Exit Sub
        ElseIf CmbFormulator_SatuanHasil.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbFormulator_SatuanHasil.Focus() : Exit Sub
        End If

        get_jam()

        Try

            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()
            get_no_faktur_binding()

            Dim sample As String = ""

            sample = "NULL"


            If Val(TxtFormulator_TotalPersen.Text) <> 100 Then
                CloseTrans()
                CloseConn()
                MessageBox.Show(Base_Language.Lang_Global_Persen_harus_100 & ". . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim kd_barangINq As String = ""
            SQL = "select top(1) Kode_Barang_inq from barang "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Barang ='" & TxtFormulator_KodeBarang.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    kd_barangINq = dr("Kode_Barang_inq")
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using
            SQL = "INSERT INTO Emi_Transaksi_Formulator "
            SQL = SQL & "(Kode_Perusahaan, No_Faktur, UserID, Tanggal, Jam,Flag_Sample, "
            SQL = SQL & "Kode_Barang, Penanggung_Jawab, Lokasi, Kode_Stock_Owner, Hasil, Satuan_Hasil) VALUES ( "
            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & UserID & "', "
            SQL = SQL & "'" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', " & sample & ", "
            SQL = SQL & "'" & kd_barangINq & "', "
            SQL = SQL & "'" & arrIdPenanggungJawab.Item(CmbFormulator_PenanggungJawab.SelectedIndex) & "', '" & CmbFormulator_LokasiInquiry.Text & "', '" & CmbFormulator_LokasiBarang.Text & "', "
            SQL = SQL & "'" & HilangkanTanda(TxtFormulator_Hasil.Text) & "', '" & CmbFormulator_SatuanHasil.Text & "') "
            ExecuteTrans(SQL)

            For index = 0 To DgvFormulator_StepFormulator.Rows.Count - 2 'Karna data terakhir itu default dgv
                Get_Isi_Listview(index)

                lvQty = Val(HilangkanTanda(lvQty))
                lvPersentase = Val(HilangkanTanda(lvPersentase))

                SQL = "INSERT INTO EMI_Transaksi_Formulator_Detail_Step "
                SQL = SQL & "(Kode_Perusahaan, No_Faktur, No_Step, Tipe, Kode "
                SQL = SQL & ",Deskripsi,Jumlah, Satuan, Persentase, Nilai_Pengali, Satuan_barang, Nilai_Barang) VALUES( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & lvNo & "', "
                SQL = SQL & "'" & lvTipe & "', '" & lvKdBarang & "', '" & lvNama & "', "
                SQL = SQL & "'" & lvQty & "', '" & lvSatuan & "', '" & lvPersentase & "','" & lvPengali & "', '" & lvSatuanBarang & "', '" & lvNilaiBarang & "') "
                ExecuteTrans(SQL)


                'If lvTipe = "Raw" Then

                If lvQty = "" Or lvQty = "0" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_TransFormula_DgvStepFormula_Qty & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                ElseIf lvPersentase = "" Or lvQty = "0" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_TransFormula_DgvStepFormula_Persentase & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                SQL = "select Kode_Barang, Satuan from EMI_Transaksi_Formulator_Detail_Bahan "
                SQL = SQL & "where No_Faktur='" & TxtFormulator_NoFaktur.Text & "' and kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Barang ='" & lvKdBarang & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then

                        If dr("Satuan") <> lvSatuan Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_TransFormula_DgvStepFormula_Qty & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        dr.Close()
                        SQL = "update EMI_Transaksi_Formulator_Detail_Bahan set Jumlah=Jumlah+" & lvQty & ", Nilai_Barang=Nilai_Barang+" & lvNilaiBarang & ", "
                        SQL = SQL & "persentase=persentase+" & HilangkanTanda(lvPersentase) & " "
                        SQL = SQL & "where Kode_Barang='" & lvKdBarang & "'"
                        ExecuteTrans(SQL)

                    Else
                        dr.Close()
                        SQL = "INSERT INTO EMI_Transaksi_Formulator_Detail_Bahan "
                        SQL = SQL & "(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang "
                        SQL = SQL & ",Jumlah, Satuan, Persentase, Nilai_Pengali, Satuan_barang, Nilai_Barang) VALUES( "
                        SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & CmbFormulator_LokasiBarang.Text & "', "
                        SQL = SQL & "'" & lvKdBarang & "', '" & lvQty & "', '" & lvSatuan & "', '" & lvPersentase & "','" & lvPengali & "', '" & lvSatuanBarang & "', '" & lvNilaiBarang & "') "
                        ExecuteTrans(SQL)
                    End If
                End Using
                'End If

            Next


            '===============================
            '=     SET BINDING FORMULA     =
            '===============================
            ''Binding 
            SQL = "update EMI_Transaksi_Formulator_Binding set aktif='T' "
            SQL = SQL & "where Kode_Barang='" & kd_barangINq & "' and Kode_Perusahaan='" & KodePerusahaan & "' and aktif='Y' "
            ExecuteTrans(SQL)

            SQL = "Insert into EMI_Transaksi_Formulator_Binding ("
            SQL = SQL & "Kode_Perusahaan, No_faktur, "
            SQL = SQL & "Kode_Customer, No_Inquiry, "
            SQL = SQL & "Tanggal, Jam, UserID, Kode_barang, Kode_formula, Aktif) "
            SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_No_Faktur_Binding.Text & "', "
            SQL = SQL & "NULL, NULL, "
            SQL = SQL & "'" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
            SQL = SQL & "'" & UserID & "','" & kd_barangINq & "', '" & TxtFormulator_NoFaktur.Text & "','Y')"
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()

            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub

    Private Sub TxtFormulator_Hasil_TextChanged(sender As Object, e As EventArgs) Handles TxtFormulator_Hasil.TextChanged

        DgvFormulator_StepFormulator.Rows.Clear()
        DgvFormulator_StepFormulator.Rows.Add(1)

    End Sub

    Private Sub DgvFormulator_StepFormulator_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvFormulator_StepFormulator.KeyDown
        If DgvFormulator_StepFormulator.Rows.Count = 0 Or DgvFormulator_StepFormulator.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DgvFormulator_StepFormulator.CurrentRow.Index
        Dim currentCell = DgvFormulator_StepFormulator.CurrentCellAddress.X

        If e.KeyCode = Keys.F1 Then
            ERP_EMI.SD_Pilih_Barang.lokasi()
            ERP_EMI.SD_Pilih_Barang.asal = Jenis
            ERP_EMI.SD_Pilih_Barang.filter_tambahan = " and b.Flag_Tampil_Formulator='Y' "
            ERP_EMI.SD_Pilih_Barang.CmbPilihBarang_Lokasi.Text = CmbFormulator_LokasiBarang.Text
            ERP_EMI.SD_Pilih_Barang.ShowDialog()
        ElseIf e.KeyCode = Keys.Insert Then

            If currentRow <> DgvFormulator_StepFormulator.Rows.Count - 1 Then
                DgvFormulator_StepFormulator.Rows.Insert(currentRow + 1)
            End If
        ElseIf e.KeyCode = Keys.Delete Then

            If currentRow <> DgvFormulator_StepFormulator.Rows.Count - 1 Then
                BeginInvoke(New MethodInvoker(Sub() DgvFormulator_StepFormulator.Rows.RemoveAt(currentRow)))
            End If
        End If

        Total()
    End Sub

    Private Sub TxtFormulator_Hasil_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFormulator_Hasil.KeyPress
        If e.KeyChar = Chr(13) Then CmbFormulator_SatuanHasil.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub CmbFormulator_PenanggungJawab_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbFormulator_PenanggungJawab.KeyPress
        If e.KeyChar = Chr(13) Then DgvFormulator_StepFormulator.Focus()
    End Sub

    Private Sub BtnFormulator_Refresh_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Refresh.Click
        Kosong()
    End Sub

    Private Sub TxtFormulator_KodeBarang_TextChanged(sender As Object, e As EventArgs) Handles TxtFormulator_KodeBarang.TextChanged
        If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
            ListView1.Visible = False : Exit Sub
        Else
            ListView1.Visible = True
            ListView1.Top = TxtFormulator_KodeBarang.Top + TxtFormulator_KodeBarang.Height + 6
            ListView1.Left = TxtFormulator_KodeBarang.Left
        End If

        ListView1.Items.Clear()

        Try

            OpenConn()

            Dim Lvw2 As ListViewItem

            SQL = "select a.kode_barang, a.nama from barang a, emi_group_jenis b  where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbFormulator_LokasiBarang.Text & "' and "
            SQL = SQL & "nama like '%" & TxtFormulator_KodeBarang.Text & "%' and a.id_group_jenis =b.id_group_jenis and (Flag_Finished_Good='Y' or Flag_Tampil_Inquiry='Y' or flag_semi_fg='Y' )"
            SQL = SQL & "order by a.kode_barang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lvw2 = ListView1.Items.Add(Dr("kode_barang"))
                    Lvw2.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtFormulator_KodeBarang_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtFormulator_KodeBarang.KeyDown
        If e.KeyCode = Keys.Down Then ListView1.Focus()
    End Sub

    Private Sub TxtFormulator_KodeBarang_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFormulator_KodeBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
                ListView1.Visible = False : Exit Sub
            End If
            TxtFormulator_KodeBarang_Leave(TxtFormulator_KodeBarang, e)
        End If
    End Sub

    Private Sub TxtFormulator_KodeBarang_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFormulator_KodeBarang.Leave
        If TxtFormulator_KodeBarang.Text.Trim.Length = 0 Then
            ListView1.Visible = False : Exit Sub
        Else
            ListView1.Visible = True
        End If
        If ListView1.Focused = True Then Exit Sub

        Try

            OpenConn()

            SQL = "select a.kode_barang, a.nama from barang a, emi_group_jenis b  where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbFormulator_LokasiBarang.Text & "' and "
            SQL = SQL & "kode_barang like '%" & TxtFormulator_KodeBarang.Text & "%' and a.id_group_jenis =b.id_group_jenis and (Flag_Finished_Good='Y' or Flag_Tampil_Inquiry='Y'  or flag_semi_fg='Y' )"
            SQL = SQL & "order by a.kode_barang"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtFormulator_KodeBarang.Text = Dr("kode_barang")
                    TxtFormulator_NamaBarang.Text = Dr("nama")
                    CmbFormulator_PenanggungJawab.Focus()
                Else
                    TxtFormulator_KodeBarang.Text = "" : TxtFormulator_NamaBarang.Text = ""
                    TxtFormulator_KodeBarang.Focus()
                End If
                ListView1.Visible = False
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView1_DoubleClick(ListView1, e)
        End If
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub

        Dim Kode As String = ListView1.FocusedItem.Text
        Dim Nama As String = ListView1.FocusedItem.SubItems(1).Text

        TxtFormulator_KodeBarang.Text = Kode
        TxtFormulator_NamaBarang.Text = Nama
        ListView1.Visible = False
    End Sub

    Private Sub CmbFormulator_LokasiBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFormulator_LokasiBarang.SelectedIndexChanged
        TxtFormulator_KodeBarang.Text = ""
        TxtFormulator_NamaBarang.Text = ""
    End Sub


    Private Sub CmbFormulator_SatuanHasil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFormulator_SatuanHasil.SelectedIndexChanged
        DgvFormulator_StepFormulator.Rows.Clear()
        DgvFormulator_StepFormulator.Rows.Add(1)
    End Sub

    Private Sub DgvFormulator_StepFormulator_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFormulator_StepFormulator.CellClick


        If DgvFormulator_StepFormulator.CurrentCell.ColumnIndex = cellQty Then
            Dim cellKuantity As String = DgvFormulator_StepFormulator.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity)
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            DgvFormulator_StepFormulator.CurrentCell.Value = nilai
        End If

    End Sub

    Private Sub DgvFormulator_StepFormulator_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFormulator_StepFormulator.CellLeave

        If DgvFormulator_StepFormulator.CurrentCell.ColumnIndex = cellQty Then
            Dim cellKuantity As String = DgvFormulator_StepFormulator.CurrentCell.Value

            If Not String.IsNullOrEmpty(cellKuantity) Then

                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))


                DgvFormulator_StepFormulator.CurrentCell.Value = formattedValue
            End If
        End If

    End Sub

    Private Sub DgvFormulator_StepFormulator_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFormulator_StepFormulator.CellContentClick

    End Sub

    Private Sub CmbFormulator_SatuanHasil_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbFormulator_SatuanHasil.KeyPress
        If e.KeyChar = Chr(13) Then BtnFormulator_Simpan.Focus()
    End Sub

    Private Sub Transaksi_Formula_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
End Class
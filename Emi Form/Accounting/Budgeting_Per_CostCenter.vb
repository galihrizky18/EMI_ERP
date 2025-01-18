Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Budgeting_Per_CostCenter

    Dim arrCari, arrBulan, arrBulanMM, arrTahun As New ArrayList

    Dim tahun_awal As Integer
    Dim tahun_akhir As Integer

    Dim ColDinamis As Integer = 4

    Dim ItemDgv_KodeAkun As Integer = 0
    Dim ItemDgv_Formula As Integer = 1
    Dim ItemDgv_Akun As Integer = 2
    Dim ItemDgv_FlagBudgeting As Integer = 3

    Private Sub Budgeting_Per_CostCenter_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Budgeting_Per_CostCenter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()

    End Sub

    Private Sub kosong()

        Dgv_Data.Rows.Clear()

        Cmb_Filter.Items.Clear()
        Cmb_Filter.Items.Add("--- SEMUA ---") : arrCari.Add("SEMUA")
        Cmb_Filter.Items.Add("LR") : arrCari.Add("LR")
        Cmb_Filter.Items.Add("Neraca") : arrCari.Add("NERACA")
        Cmb_Filter.Items.Add("Akun") : arrCari.Add("AKUN")

        Txt_Filter_Value.Text = ""

        Cmb_Bulan.Items.Clear() : arrBulan.Clear() : arrBulanMM.Clear()
        Cmb_Bulan.Items.Add("Januari") : arrBulan.Add("1") : arrBulanMM.Add("01")
        Cmb_Bulan.Items.Add("Februari") : arrBulan.Add("2") : arrBulanMM.Add("02")
        Cmb_Bulan.Items.Add("Maret") : arrBulan.Add("3") : arrBulanMM.Add("03")
        Cmb_Bulan.Items.Add("April") : arrBulan.Add("4") : arrBulanMM.Add("04")
        Cmb_Bulan.Items.Add("Mei") : arrBulan.Add("5") : arrBulanMM.Add("05")
        Cmb_Bulan.Items.Add("Juni") : arrBulan.Add("6") : arrBulanMM.Add("06")
        Cmb_Bulan.Items.Add("Juli") : arrBulan.Add("7") : arrBulanMM.Add("07")
        Cmb_Bulan.Items.Add("Agustus") : arrBulan.Add("8") : arrBulanMM.Add("08")
        Cmb_Bulan.Items.Add("September") : arrBulan.Add("9") : arrBulanMM.Add("09")
        Cmb_Bulan.Items.Add("Oktober") : arrBulan.Add("10") : arrBulanMM.Add("10")
        Cmb_Bulan.Items.Add("November") : arrBulan.Add("11") : arrBulanMM.Add("11")
        Cmb_Bulan.Items.Add("Desember") : arrBulan.Add("12") : arrBulanMM.Add("12")

        Cmb_Tahun.Items.Clear()
        tahun_awal = Date.Now.Year - 2
        tahun_akhir = Date.Now.Year + 2
        For i As Integer = tahun_awal To tahun_akhir
            Cmb_Tahun.Items.Add(i)
        Next

        Btn_Release.Visible = False
        Btn_Simpan.Enabled = True

        Try
            OpenConn()

            get_no_faktur()

            Cmb_Lokasi.Items.Clear()
            SQL = "select kode_stock_owner, keterangan from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Cmb_Lokasi.Items.Add(Dr("kode_stock_owner"))
                    Cmb_Lokasi.SelectedIndex = 0
                Else
                    CloseConn()
                    MessageBox.Show("Lokasi Tidak Di Temukan", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub Kosong_Kolom_Dinamis()
        'HAPUS KOLOM DINAMIS
        For i As Integer = Dgv_Data.Columns.Count - 1 To ColDinamis Step -1
            Dgv_Data.Columns.RemoveAt(i)
        Next
    End Sub

    Private Sub get_no_faktur()
        Txt_NoFaktur.Text = fBudgetingCostCenter & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Budgeting_Cost_Center", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fBudgetingCostCenter) + 4 & ")", fBudgetingCostCenter & Format(tgl_skg, "MMyy"))
    End Sub


    Private Sub Cmb_Tahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Tahun.SelectedIndexChanged
        If Cmb_Tahun.Items.Count = 0 Or Cmb_Tahun.SelectedIndex = -1 Then Exit Sub

        Txt_NoFaktur.Focus() : Dgv_Data.Focus()

    End Sub

    Private Sub Cmb_Bulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Bulan.SelectedIndexChanged
        If Cmb_Bulan.Items.Count = 0 Then Exit Sub

        Cmb_Tahun.SelectedIndex = -1
        Cmb_Tahun.Text = ""

    End Sub

    Private Sub Txt_NoFaktur_Leave(sender As Object, e As EventArgs) Handles Txt_NoFaktur.Leave
        If Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Tidak Boleh Kosong", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Tahun.SelectedIndex = -1
            Cmb_Tahun.Text = ""
            Dgv_Data.Focus()
            Exit Sub
        ElseIf Cmb_Bulan.SelectedIndex = -1 Then
            MessageBox.Show("Piih Bulan Terlebih Dahulu", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Tahun.SelectedIndex = -1
            Cmb_Tahun.Text = ""
            Dgv_Data.Focus()
            Exit Sub
        ElseIf Cmb_Tahun.SelectedIndex = -1 Then
            MessageBox.Show("Piih Tahun Terlebih Dahulu", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Bulan.SelectedIndex = -1
            Cmb_Tahun.SelectedIndex = -1
            Cmb_Bulan.Text = ""
            Cmb_Tahun.Text = ""
            Dgv_Data.Focus
            Exit Sub
        End If

        Dim isRelease As Boolean = False

        Try
            OpenConn()

            '===============================
            '=     CEK APAKAH ADA DATA     =
            '===============================
            SQL = "select No_Faktur, Flag_Release from Budgeting_Cost_Center "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Lokasi = '" & Cmb_Lokasi.Text & "' "
            SQL = SQL & "and bulan = '" & arrBulanMM(Cmb_Bulan.SelectedIndex) & "' and tahun = '" & Cmb_Tahun.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    Txt_NoFaktur.Text = dr("No_Faktur")

                    If General_Class.CekNULL(dr("Flag_Release")) = "Y" Then
                        isRelease = True
                        Btn_Simpan.Enabled = False
                        Btn_Release.Visible = False
                    Else
                        Btn_Simpan.Enabled = True
                        Btn_Release.Visible = True
                    End If

                Else
                    dr.Close()
                    get_no_faktur()
                    Btn_Simpan.Enabled = True
                    Btn_Release.Visible = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Get_Data(Cmb_Lokasi.SelectedItem, arrBulan(Cmb_Bulan.SelectedIndex), Cmb_Tahun.SelectedItem, isRelease)
    End Sub


    Private Sub Get_Data(Lokasi As String, Bulan As String, Tahun As String, isRelease As Boolean)


        Try
            OpenConn()

            Dgv_Data.Rows.Clear()
            Kosong_Kolom_Dinamis()

            '==============================
            '=     LOAD DINAMIS KOLOM     =
            '==============================

            SQL = "select Id_Cost_Center, Kode_Cost_Center, Keterangan from EMI_Master_Cost_Center "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Id_Cost_Center"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim newColumn As New DataGridViewTextBoxColumn()
                    newColumn.Name = Dr("Id_Cost_Center")
                    newColumn.HeaderText = Dr("Keterangan")
                    newColumn.Width = 150
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    Dgv_Data.Columns.Add(newColumn)
                Loop
            End Using

            '============================
            '=     LOAD DATA DETAIL     =
            '============================
            Dim formulaTemp As String = ""
            'SQL = "select a.Kode_Perusahaan, a.Kode_Account, a.Keterangan as Akun, "
            'SQL = SQL & "case "
            'SQL = SQL & "when a.Kode_Account in ( "
            'SQL = SQL & "select x.Kode_Account "
            'SQL = SQL & "from Account_Per_Cost_Center x "
            'SQL = SQL & "where x.Id_Cost_Center in ( "
            'SQL = SQL & "select z.Id_Cost_Center "
            'SQL = SQL & "from EMI_Master_Cost_Center z "
            'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            'SQL = SQL & ") ) "
            'SQL = SQL & "then ( select x.Id_Cost_Center "
            'SQL = SQL & "from Account_Per_Cost_Center x "
            'SQL = SQL & "where x.Id_Cost_Center in ( "
            'SQL = SQL & "select z.Id_Cost_Center "
            'SQL = SQL & "from EMI_Master_Cost_Center z "
            'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            'SQL = SQL & ") and x.Kode_Account = a.Kode_Account ) "
            'SQL = SQL & "end as kolom_dinamis "
            'SQL = SQL & "from Detail_Account a "
            'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            SQL = "SELECT a.Kode_Perusahaan, a.Kode_Account, a.Keterangan AS Akun, "
            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select top 1 'Y' from Account_Per_Cost_Center z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Account = z.Kode_Account "
            SQL = SQL & "), null) as has_CostCenter "
            SQL = SQL & "FROM Detail_Account a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "


            If Not Cmb_Filter.SelectedIndex = -1 OrElse Cmb_Filter.SelectedIndex > 0 Then

                If arrCari(Cmb_Filter.SelectedIndex) = "LR" Then
                    SQL = SQL & "and a.Letak = 'LR' and a.Jns like '" & Txt_Filter_Value.Text & "%' "

                ElseIf arrCari(Cmb_Filter.SelectedIndex) = "NERACA" Then
                    SQL = SQL & "and a.Letak = 'Neraca' and a.Jns like '" & Txt_Filter_Value.Text & "%' "

                ElseIf arrCari(Cmb_Filter.SelectedIndex) = "AKUN" Then
                    SQL = SQL & "and a.Keterangan like '" & Txt_Filter_Value.Text & "%' "

                End If

            End If

            SQL = SQL & "order by a.Kode_Account "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            formulaTemp = ""

                            Dgv_Data.Rows.Add(1)
                            Dgv_Data.Rows(i).Cells(ItemDgv_KodeAkun).Value = .Rows(i).Item("Kode_Account")
                            Dgv_Data.Rows(i).Cells(ItemDgv_Akun).Value = .Rows(i).Item("Akun")
                            Dgv_Data.Rows(i).Cells(ItemDgv_FlagBudgeting).Value = "Y"

                            If General_Class.CekNULL(.Rows(i).Item("has_CostCenter")) = "Y" Then

                                Dim coltemp As New ArrayList
                                SQL = "select Id_Cost_Center from Account_Per_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Account = '" & .Rows(i).Item("Kode_Account") & "' "
                                Using Dr = OpenTrans(SQL)
                                    Do While Dr.Read
                                        For k As Integer = ColDinamis To Dgv_Data.Columns.Count - 1

                                            If isRelease Then
                                                Dgv_Data.Rows(i).Cells(k).ReadOnly = True
                                            End If

                                            If Not Dr("Id_Cost_Center") = Val(Dgv_Data.Columns(k).Name) Then
                                                If Not coltemp.Contains(k) Then
                                                    Dgv_Data.Rows(i).Cells(k).ReadOnly = True
                                                    Dgv_Data.Rows(i).Cells(k).Style.BackColor = Color.LightGray

                                                End If
                                            Else
                                                coltemp.Add(k)
                                                Dgv_Data.Rows(i).Cells(k).ReadOnly = False
                                                Dgv_Data.Rows(i).Cells(k).Style.BackColor = Color.White
                                            End If
                                        Next
                                    Loop
                                End Using

                            Else
                                For k As Integer = ColDinamis To Dgv_Data.Columns.Count - 1

                                    If isRelease Then
                                        Dgv_Data.Rows(i).Cells(k).ReadOnly = True
                                    End If

                                    Dgv_Data.Rows(i).Cells(k).ReadOnly = True
                                    Dgv_Data.Rows(i).Cells(k).Style.BackColor = Color.LightGray
                                Next

                            End If

                            '===============================
                            '=     GET NILAI BUDGETING     =
                            '===============================
                            SQL = "select a.No_Faktur, b.Kode_Account, b.Id_Cost_Center, b.Nilai "
                            SQL = SQL & "from Budgeting_Cost_Center a, Budgeting_Cost_Center_Detail b "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            'SQL = SQL & "and a.No_Faktur = '' "
                            SQL = SQL & "and a.Lokasi = '" & Cmb_Lokasi.Text & "' "
                            SQL = SQL & "and a.Bulan = '" & arrBulanMM(Cmb_Bulan.SelectedIndex) & "' "
                            SQL = SQL & "and a.Tahun = '" & Cmb_Tahun.Text & "' "
                            SQL = SQL & "and b.Kode_Account = '" & .Rows(i).Item("Kode_Account") & "' "
                            SQL = SQL & "order by b.Kode_Account"
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                        For k As Integer = ColDinamis To Dgv_Data.Columns.Count - 1

                                            If Ds1.Tables("MyTable").Rows(j).Item("Id_Cost_Center") = Dgv_Data.Columns(k).Name Then
                                                Dgv_Data.Rows(i).Cells(k).Value = Format(Ds1.Tables("MyTable").Rows(j).Item("Nilai"), "N2")
                                                Exit For
                                            End If

                                        Next

                                    Next
                                End If

                            End Using

                            '=============================
                            '=     PERUMUSAN FORMULA     =
                            '=============================

                            For k As Integer = ColDinamis To Dgv_Data.Columns.Count - 1
                                If Not String.IsNullOrEmpty(Dgv_Data.Rows(i).Cells(k).Value) Then
                                    formulaTemp &= "1"
                                Else
                                    formulaTemp &= "0"
                                End If
                            Next

                            Dgv_Data.Rows(i).Cells(ItemDgv_Formula).Value = formulaTemp

                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.SelectedIndex = 0 Then Txt_Filter_Value.Text = "" : Exit Sub
    End Sub


    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.SelectedIndex = -1 Then Exit Sub

        If Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Tidak Boleh Kosong", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Tahun.SelectedIndex = -1
            Cmb_Tahun.Text = ""
            Dgv_Data.Focus()
            Exit Sub
        ElseIf Cmb_Bulan.SelectedIndex = -1 Then
            MessageBox.Show("Piih Bulan Terlebih Dahulu", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Tahun.SelectedIndex = -1
            Cmb_Tahun.Text = ""
            Dgv_Data.Focus()
            Exit Sub
        ElseIf Cmb_Tahun.SelectedIndex = -1 Then
            MessageBox.Show("Piih Tahun Terlebih Dahulu", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Bulan.SelectedIndex = -1
            Cmb_Tahun.SelectedIndex = -1
            Cmb_Bulan.Text = ""
            Cmb_Tahun.Text = ""
            Dgv_Data.Focus()
            Exit Sub
        End If

        Txt_NoFaktur.Focus() : Dgv_Data.Focus()

    End Sub



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Dgv_Data.Rows.Count = 0 Then Exit Sub

        If Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Tidak Boleh Kosong", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.Focus()
            Exit Sub
        ElseIf Cmb_Bulan.SelectedIndex = -1 Then
            MessageBox.Show("Piih Bulan Terlebih Dahulu", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Bulan.Focus()
            Exit Sub
        ElseIf Cmb_Tahun.SelectedIndex = -1 Then
            MessageBox.Show("Piih Tahun Terlebih Dahulu", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Tahun.Focus()
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            Dim isRelease As Boolean = False
            '======================================
            '=     APAKAH SUDAH PERNAH SIMPAN     =
            '======================================
            SQL = "select No_Faktur, Flag_Release from Budgeting_Cost_Center "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Lokasi = '" & Cmb_Lokasi.Text & "' "
            SQL = SQL & "and bulan = '" & arrBulanMM(Cmb_Bulan.SelectedIndex) & "' and tahun = '" & Cmb_Tahun.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count = 0 Then

                        get_no_faktur()

                        '========================
                        '=     INSERT INDUK     =
                        '========================
                        SQL = "insert into Budgeting_Cost_Center (Kode_Perusahaan, No_Faktur, Lokasi, Tanggal, Jam, Bulan, Tahun, UserID) values "
                        SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', '" & Cmb_Lokasi.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                        SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & arrBulanMM(Cmb_Bulan.SelectedIndex) & "', '" & Cmb_Tahun.Text & "', '" & UserID & "')"
                        ExecuteTrans(SQL)

                    Else

                        For i As Integer = 0 To .Rows.Count - 1
                            If General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "Y" Then
                                isRelease = True
                            End If

                        Next
                    End If
                End With
            End Using



            If isRelease Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data Sudah di Release, Tidak Bisa Update Lagi", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            For i As Integer = 0 To Dgv_Data.Rows.Count - 1

                '=============================
                '=     PERUMUSAN FORMULA     =
                '=============================
                Dim formulaTemp As String = ""
                For k As Integer = ColDinamis To Dgv_Data.Columns.Count - 1
                    If Not String.IsNullOrEmpty(Dgv_Data.Rows(i).Cells(k).Value) Then
                        formulaTemp &= "1"
                    Else
                        formulaTemp &= "0"
                    End If
                Next


                If Not Dgv_Data.Rows(i).Cells(ItemDgv_Formula).Value = formulaTemp Then

                    '===========================================================
                    '=     DELETE ACCOUNT PER COSTCENTER BY KODE_ACCOUNT       =
                    '===========================================================
                    SQL = "delete Budgeting_Cost_Center_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text.Trim & "' "
                    SQL = SQL & "and Kode_Account = '" & Dgv_Data.Rows(i).Cells(ItemDgv_KodeAkun).Value & "'"
                    ExecuteTrans(SQL)

                    '==============================
                    '=     INSERT COST CENTER     =
                    '==============================
                    For j As Integer = ColDinamis To Dgv_Data.Columns.Count - 1
                        If Not Dgv_Data.Rows(i).Cells(j).Value = Nothing Or Not String.IsNullOrWhiteSpace(Dgv_Data.Rows(i).Cells(j).Value) Then

                            SQL = "insert into Budgeting_Cost_Center_Detail (Kode_Perusahaan, No_Faktur, Kode_Account, Id_Cost_Center, Nilai) values "
                            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', '" & Dgv_Data.Rows(i).Cells(ItemDgv_KodeAkun).Value & "', "
                            SQL = SQL & "'" & Dgv_Data.Columns(j).Name & "', '" & Val(HilangkanTanda(Dgv_Data.Rows(i).Cells(j).Value)) & "')"
                            ExecuteTrans(SQL)

                        End If
                    Next

                End If

            Next


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Txt_NoFaktur.Focus() : Dgv_Data.Focus()
            Exit Sub
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub



    Private Sub Btn_Release_Click(sender As Object, e As EventArgs) Handles Btn_Release.Click
        If Dgv_Data.Rows.Count = 0 Then Exit Sub

        Dim pertanyaan As String = MessageBox.Show("Yakin Data Ingin di Release?...", "Biaya Cost Center", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub

        Try
            OpenConn()

            '======================================
            '=     CEK APAKAH ADA DATA DETAIL     =
            '======================================
            SQL = "select top 1 No_Faktur from Budgeting_Cost_Center_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                Else
                    CloseConn()
                    MessageBox.Show("Data Masih Kosong, Tidak Bisa Di Release", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "update Budgeting_Cost_Center set Flag_Release ='Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text.Trim & "' "
            SQL = SQL & "and Lokasi = '" & Cmb_Lokasi.Text & "' and bulan = '" & arrBulanMM(Cmb_Bulan.SelectedIndex) & "' and tahun = '" & Cmb_Tahun.Text & "'"
            ExecuteTrans(SQL)

            CloseConn()
            MessageBox.Show("Data Berhasil Di Release", "Biaya Cost Center", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Txt_NoFaktur.Focus() : Dgv_Data.Focus()
            Exit Sub
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
        Kosong_Kolom_Dinamis()

        'Get_Data(Cmb_Lokasi.SelectedItem, arrBulan(Cmb_Bulan.SelectedIndex), Cmb_Tahun.SelectedItem)
    End Sub

    Private Sub Dgv_Data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEndEdit
        If Not Dgv_Data.Rows.Count = 0 Then
            '======================
            '=     SET FORMAT     =
            '======================
            Dim culture As CultureInfo = CultureInfo.CurrentCulture

            If Dgv_Data.CurrentCell.ColumnIndex >= ColDinamis Then

                If Not IsNumeric(Dgv_Data.CurrentCell.Value) Then
                    Dgv_Data.CurrentCell.Value = ""
                    Exit Sub
                End If


                Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

                If cellKuantity.Contains(",") Then
                    MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Dgv_Data.CurrentCell.Value = Format(0, "N2")
                    Exit Sub
                End If

                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N2", culture)

                Dgv_Data.CurrentCell.Value = formattedValue
            End If
        End If
    End Sub

    Private Sub Dgv_Data_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEnter
        If Not Dgv_Data.Rows.Count = 0 Then
            '======================
            '=     SET FORMAT     =
            '======================

            If Dgv_Data.CurrentCell.ColumnIndex >= ColDinamis Then
                If Not IsNumeric(Dgv_Data.CurrentCell.Value) Then
                    Dgv_Data.CurrentCell.Value = ""
                    Exit Sub
                End If

                Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

                If cellKuantity = "" Then
                    Exit Sub
                End If

                Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
                Dim nilai As Decimal = Decimal.Parse(cleanedStr)

                Dgv_Data.CurrentCell.Value = nilai
            End If
        End If
    End Sub

    Private Sub Dgv_Data_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellLeave
        If Not Dgv_Data.Rows.Count = 0 Then

            '======================
            '=     SET FORMAT     =
            '======================
            Dim culture As CultureInfo = CultureInfo.CurrentCulture


            If Dgv_Data.CurrentCell.ColumnIndex >= ColDinamis Then

                If Not IsNumeric(Dgv_Data.CurrentCell.Value) Then
                    Dgv_Data.CurrentCell.Value = ""
                    Exit Sub
                End If

                Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

                If cellKuantity = "" Then
                    Exit Sub
                End If


                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N2", culture)

                Dgv_Data.CurrentCell.Value = formattedValue

            End If
        End If
    End Sub

End Class
Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class Detail_Account_New3

    Dim arrIdCostCenter, arrcari As New ArrayList

    Dim valueKodeAccount() As String

    Dim ColDinamis As Integer = 3

    Dim itemBinding_KdAccount As Integer = 0
    Dim itemBinding_KdDetailAccount As Integer = 1
    Dim itemBinding_KeteranganAccount As Integer = 2

    Private Sub Detail_Account_New3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Kosong_All()
    End Sub

    Private Sub Kosong_All()

        valueKodeAccount = {}

        Kosong_Input()
        Kosong_Display()

    End Sub

    Private Sub Kosong_Input()
        Try
            OpenConn()

#Region "TAB INPUT"

            LvInput_CostCenter.Columns.Clear()
            LvInput_CostCenter.Columns.Add("", 30, HorizontalAlignment.Center)
            LvInput_CostCenter.Columns.Add("Cost Center", 100, HorizontalAlignment.Left)
            LvInput_CostCenter.View = View.Details

            Cmb_KodeAccount.Items.Clear()
            SQL = "select * from Master_Acc where kode_perusahaan = '" & KodePerusahaan & "' Order by kode_master_acc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_KodeAccount.Items.Add(dr("Kode_Master_Acc") & " - " & dr("Keterangan"))
                Loop
            End Using

            Cmb_Cabang.Items.Clear()
            SQL = "select kode_stock_owner from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    Cmb_Cabang.Items.Add(Dr("kode_stock_owner"))
                Loop
            End Using

            Cmb_Posisi.Items.Clear()
            Cmb_Posisi.Items.Add("Debit")
            Cmb_Posisi.Items.Add("Kredit")
            Cmb_Posisi.Text = ""
            Cmb_Posisi.SelectedIndex = -1

            Cmb_Letak.Items.Clear()
            Cmb_Letak.Items.Add("LR")
            Cmb_Letak.Items.Add("Neraca")
            Cmb_Letak.Text = ""
            Cmb_Letak.SelectedIndex = -1

            Cmb_LR.Items.Clear()
            Cmb_LR.Items.Add("Penjualan")
            Cmb_LR.Items.Add("Pembelian")
            Cmb_LR.Items.Add("HPP")
            Cmb_LR.Items.Add("Biaya")
            Cmb_LR.Items.Add("Pendapatan")
            Cmb_LR.Items.Add("Biaya Lain")
            Cmb_LR.Items.Add("Biaya Pajak")
            Cmb_LR.Enabled = False
            Cmb_LR.Text = ""
            Cmb_LR.SelectedIndex = -1

            Cmb_Neraca.Items.Clear()
            Cmb_Neraca.Items.Add("N_Aktiva_Lancar")
            Cmb_Neraca.Items.Add("N_Aktiva_Tetap")
            Cmb_Neraca.Items.Add("N_Biaya_Dibayar_Dimuka")
            Cmb_Neraca.Items.Add("N_Aktiva_Lain")
            Cmb_Neraca.Items.Add("N_Hutang_Lancar")
            Cmb_Neraca.Items.Add("N_Hutang_Jangka_Panjang")
            Cmb_Neraca.Items.Add("N_Pendapatan_Diterima_Dimuka")
            Cmb_Neraca.Items.Add("N_Modal")
            Cmb_Neraca.Enabled = False
            Cmb_Neraca.Text = ""
            Cmb_Neraca.SelectedIndex = -1

            Cmb_FlagAktif.Items.Clear()
            Cmb_FlagAktif.Items.Add("Y")
            Cmb_FlagAktif.Items.Add("T")
            Cmb_FlagAktif.Text = ""
            Cmb_FlagAktif.SelectedIndex = -1

            Cmb_FlagBudget.Items.Clear()
            Cmb_FlagBudget.Items.Add("Y")
            Cmb_FlagBudget.Items.Add("T")
            Cmb_FlagBudget.Text = ""
            Cmb_FlagBudget.SelectedIndex = -1

            Cmb_FlagKhusus.Items.Clear()
            Cmb_FlagKhusus.Items.Add("Y")
            Cmb_FlagKhusus.Items.Add("T")
            Cmb_FlagKhusus.Text = ""
            Cmb_FlagKhusus.SelectedIndex = -1

            Cmb_FlagBiaya.Items.Clear()
            Cmb_FlagBiaya.Items.Add("Y")
            Cmb_FlagBiaya.Items.Add("T")
            Cmb_FlagBiaya.Text = ""
            Cmb_FlagBiaya.SelectedIndex = -1

            Cmb_FlagPBK.Items.Clear()
            Cmb_FlagPBK.Items.Add("Y")
            Cmb_FlagPBK.Items.Add("T")
            Cmb_FlagPBK.Text = ""
            Cmb_FlagPBK.SelectedIndex = -1

            Txt_KodeDetailAccount.Text = ""
            Txt_Keterangan.Text = ""
            Txt_BudgetHarian.Text = ""
            Txt_N_BudgetHarian.Text = ""
            Txt_BudgetBulanan.Text = ""
            Txt_N_BudgetBulanan.Text = ""

            Txt_BudgetHarian.Enabled = False : Txt_N_BudgetHarian.Enabled = False
            Txt_BudgetBulanan.Enabled = False : Txt_N_BudgetBulanan.Enabled = False

            LvInput_CostCenter.Items.Clear()

            Txt_KodeDetailAccount.Enabled = True
            Txt_KodeDetailAccount.Text = ""

            Btn_Simpan.Text = "&Simpan"
            Btn_Simpan.Tag = "SIMPAN"
            Btn_Hapus.Enabled = False

            '============================
            '=     LOAD COST CENTER     =
            '============================
            SQL = "select Id_Cost_Center, Kode_Cost_Center, Keterangan, Id_Divisi, Id_Sub_Divisi "
            SQL = SQL & "from EMI_Master_Cost_Center "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = LvInput_CostCenter.Items.Add("") : arrIdCostCenter.Add(Dr("Id_Cost_Center"))
                    lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

#End Region

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Kosong_Display()
        Try
            OpenConn()

#Region "TAB DISPLAY"

            Lv_Display.Columns.Clear()
            Lv_Display.Columns.Add("Kode M.Acc", 0, HorizontalAlignment.Center)
            Lv_Display.Columns.Add("Kode D.Acc", 0, HorizontalAlignment.Center)
            Lv_Display.Columns.Add("Kode Account", 120, HorizontalAlignment.Center)
            Lv_Display.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
            Lv_Display.Columns.Add("Posisi", 70, HorizontalAlignment.Center)
            Lv_Display.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
            Lv_Display.Columns.Add("Letak", 70, HorizontalAlignment.Left)
            Lv_Display.Columns.Add("Jns", 120, HorizontalAlignment.Left)
            Lv_Display.Columns.Add("Aktif", 50, HorizontalAlignment.Center)
            Lv_Display.Columns.Add("Flag Khusus", 100, HorizontalAlignment.Center)
            Lv_Display.Columns.Add("Flag Biaya", 100, HorizontalAlignment.Center)
            Lv_Display.Columns.Add("Flag Pbk", 100, HorizontalAlignment.Center)
            Lv_Display.View = View.Details

            Lv_Display.Items.Clear()
            SQL = "Select Kode_master_acc, Kode_acc, kode_detail_acc, Kode_account, Keterangan, posisi, "
            SQL = SQL & "lokasi, letak, jns, aktif, flag_khusus, flag_biaya, flag_pbk "
            SQL = SQL & "From Detail_Account "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Master_Acc,Kode_Acc,kode_detail_acc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_Display.Items.Add(dr("Kode_master_acc"))
                    Lvw.SubItems.Add(dr("Kode_acc") & dr("kode_detail_acc"))
                    Lvw.SubItems.Add(dr("Kode_account"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    Lvw.SubItems.Add(dr("posisi"))
                    Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("letak"))
                    Lvw.SubItems.Add(dr("jns"))
                    Lvw.SubItems.Add(dr("aktif"))
                    Lvw.SubItems.Add(dr("flag_khusus"))
                    If General_Class.CekNULL(dr("flag_biaya")) = "" Then
                        Lvw.SubItems.Add("T")
                    Else
                        Lvw.SubItems.Add(General_Class.CekNULL(dr("flag_biaya")))
                    End If
                    Lvw.SubItems.Add(dr("flag_pbk"))
                Loop
            End Using

            Cmb_Display_Filter.Items.Clear()
            Cmb_Display_Filter.Text = ""
            Txt_Display_Filter.Text = ""

            'Filter
            arrcari.Clear()
            Cmb_Display_Filter.Items.Clear()
            Cmb_Display_Filter.Items.Add("Kode Master Account") : arrcari.Add("Kode_master_acc")
            Cmb_Display_Filter.Items.Add("Kode Account") : arrcari.Add("Kode_account")
            Cmb_Display_Filter.Items.Add("Kode Detail Account") : arrcari.Add("kode_detail_acc")
            Cmb_Display_Filter.Items.Add("Keterangan") : arrcari.Add("Keterangan")


#End Region

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong_All()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_InputCostCenter.CheckedChanged
        If Chk_InputCostCenter.Checked = True Then
            For a As Integer = 0 To LvInput_CostCenter.Items.Count - 1
                LvInput_CostCenter.Items(a).Checked = True
            Next
        Else
            For a As Integer = 0 To LvInput_CostCenter.Items.Count - 1
                LvInput_CostCenter.Items(a).Checked = False
            Next
        End If
    End Sub

    Private Sub Cmb_Letak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Letak.SelectedIndexChanged
        If Cmb_Letak.SelectedIndex = -1 Then Exit Sub

        Cmb_LR.Text = ""
        Cmb_Neraca.Text = ""

        If Cmb_Letak.SelectedItem = "LR" Then
            Cmb_LR.Enabled = True
            Cmb_Neraca.Enabled = False

        ElseIf Cmb_Letak.SelectedItem = "Neraca" Then
            Cmb_LR.Enabled = False
            Cmb_Neraca.Enabled = True
        End If
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Display_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Get_Data_Display()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cmb_Display_Filter.Text = ""
        Cmb_Display_Filter.SelectedIndex = -1

        Txt_Display_Filter.Text = ""

        Get_Data_Display()
    End Sub

    Private Sub Cmb_FlagBudget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_FlagBudget.SelectedIndexChanged
        If Cmb_FlagBudget.SelectedIndex = 0 Then
            Txt_BudgetHarian.Text = "" : Txt_N_BudgetHarian.Text = ""
            Txt_BudgetBulanan.Text = "" : Txt_N_BudgetBulanan.Text = ""

            Txt_BudgetHarian.Enabled = True : Txt_N_BudgetHarian.Enabled = True
            Txt_BudgetBulanan.Enabled = True : Txt_N_BudgetBulanan.Enabled = True
        Else
            Txt_BudgetHarian.Text = "" : Txt_N_BudgetHarian.Text = ""
            Txt_BudgetBulanan.Text = "" : Txt_N_BudgetBulanan.Text = ""

            Txt_BudgetHarian.Enabled = False : Txt_N_BudgetHarian.Enabled = False
            Txt_BudgetBulanan.Enabled = False : Txt_N_BudgetBulanan.Enabled = False
        End If
    End Sub

    Private Sub Get_Data_Display()
        Try
            OpenConn()

            Lv_Display.Items.Clear()
            SQL = "Select Kode_master_acc, Kode_acc, kode_detail_acc, Kode_account, Keterangan, posisi, "
            SQL = SQL & "lokasi, letak, jns, aktif, flag_khusus, flag_biaya, flag_pbk "
            SQL = SQL & "From Detail_Account "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            If Cmb_Display_Filter.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(Cmb_Display_Filter.SelectedIndex) & "  like  '%" & Trim(Txt_Display_Filter.Text.Trim) & "%' "
            End If
            SQL = SQL & "order by Kode_Master_Acc,Kode_Acc,kode_detail_acc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_Display.Items.Add(dr("Kode_master_acc"))
                    Lvw.SubItems.Add(dr("Kode_acc") & dr("kode_detail_acc"))
                    Lvw.SubItems.Add(dr("Kode_account"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    Lvw.SubItems.Add(dr("posisi"))
                    Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("letak"))
                    Lvw.SubItems.Add(dr("jns"))
                    Lvw.SubItems.Add(dr("aktif"))
                    Lvw.SubItems.Add(dr("flag_khusus"))
                    If General_Class.CekNULL(dr("flag_biaya")) = "" Then
                        Lvw.SubItems.Add("T")
                    Else
                        Lvw.SubItems.Add(General_Class.CekNULL(dr("flag_biaya")))
                    End If
                    Lvw.SubItems.Add(dr("flag_pbk"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Cmb_KodeAccount.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode master account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_KodeAccount.Focus() : Exit Sub
            'ElseIf ComboBox3.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Kode account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBox2.Focus() : Exit Sub
        ElseIf Txt_KodeDetailAccount.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode detail Account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KodeDetailAccount.Focus() : Exit Sub
            'ElseIf TextBox1.Text.Trim.Length <> 5 Then
            '    MessageBox.Show("Kode detail Account harus 5 digit . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox1.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus() : Exit Sub
        ElseIf Cmb_Cabang.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Cabang.Focus() : Exit Sub
        ElseIf Cmb_Letak.SelectedIndex = -1 Then
            MessageBox.Show("Letak harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Letak.Focus() : Exit Sub
        ElseIf Cmb_FlagAktif.SelectedIndex = -1 Then
            MessageBox.Show("Aktif harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_FlagAktif.Focus() : Exit Sub
        ElseIf Cmb_FlagKhusus.SelectedIndex = -1 Then
            MessageBox.Show("Flag khusus harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_FlagKhusus.Focus() : Exit Sub
        ElseIf Cmb_FlagBiaya.SelectedIndex = -1 Then
            MessageBox.Show("Flag biaya harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_FlagBiaya.Focus() : Exit Sub
        ElseIf Cmb_FlagPBK.SelectedIndex = -1 Then
            MessageBox.Show("Flag Pbk harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_FlagPBK.Focus() : Exit Sub
        End If

        If Txt_BudgetHarian.Enabled = True Then
            If Val(Txt_BudgetHarian.Text) = 0 Then
                MessageBox.Show("Lama budget harian harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_BudgetHarian.Focus() : Exit Sub
            ElseIf Val(Txt_N_BudgetHarian.Text) = 0 Then
                MessageBox.Show("Nilai budget harian harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_N_BudgetHarian.Focus() : Exit Sub
            ElseIf Val(Txt_BudgetBulanan.Text) = 0 Then
                MessageBox.Show("Lama budget bulanan harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_BudgetBulanan.Focus() : Exit Sub
            ElseIf Val(Txt_N_BudgetBulanan.Text) = 0 Then
                MessageBox.Show("Nilai budget bulanan harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_N_BudgetBulanan.Focus() : Exit Sub
            ElseIf Val(Txt_BudgetHarian.Text) > Val(Txt_BudgetBulanan.Text) Then
                MessageBox.Show("Lama budget harian tidak boleh lebih besar dari lama budget bulanan . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_BudgetHarian.Focus() : Exit Sub
            ElseIf Val(Txt_N_BudgetHarian.Text) > Val(Txt_N_BudgetBulanan.Text) Then
                MessageBox.Show("Nilai budget harian tidak boleh lebih besar dari nilai budget bulanan . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_N_BudgetHarian.Focus() : Exit Sub
            End If
        End If

        If Cmb_Letak.SelectedIndex = 0 Then 'lr
            If Cmb_LR.SelectedIndex = -1 Then
                MessageBox.Show("LR harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_LR.Focus() : Exit Sub
            End If
        Else 'neraca
            If Cmb_Neraca.SelectedIndex = -1 Then
                MessageBox.Show("Neraca harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Neraca.Focus() : Exit Sub
            End If
        End If

        If LvInput_CostCenter.Items.Count = 0 Then
            MessageBox.Show("Data Cost Center Tidak Ada", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvInput_CostCenter.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            valueKodeAccount = Cmb_KodeAccount.Text.Split("-")

            '======================
            '=      GET JENIS     =
            '======================
            Dim jenis As String = ""
            If Cmb_Letak.SelectedIndex = 0 Then 'LR
                jenis = Cmb_LR.Text
            Else 'Neraca
                jenis = Cmb_Neraca.Text
            End If

            '=======================
            '=      GET BUDGET     =
            '=======================
            Dim lama_budget_harian As Integer = 0
            Dim nilai_budget_harian As Double = 0
            Dim lama_budget_bulanan As Integer = 0
            Dim nilai_budget_bulanan As Double = 0

            If Cmb_FlagBudget.SelectedIndex = 0 Then 'Y
                lama_budget_harian = Txt_BudgetHarian.Text
                nilai_budget_harian = Txt_N_BudgetHarian.Text
                lama_budget_bulanan = Txt_BudgetBulanan.Text
                nilai_budget_bulanan = Txt_BudgetBulanan.Text
            Else 'T
                lama_budget_harian = 0
                nilai_budget_harian = 0
                lama_budget_bulanan = 0
                nilai_budget_bulanan = 0
            End If

            If Btn_Simpan.Tag = "SIMPAN" Then

                If Cmb_FlagAktif.Text = "Y" Then
                    If Cmb_FlagBiaya.Text = "Y" Then
                        SQL = "Insert Into Detail_Account(Kode_Perusahaan, Kode_Master_Acc, Kode_Acc, "
                        SQL = SQL & "Kode_Detail_Acc, Keterangan, Posisi, lokasi, letak, jns, aktif, "
                        SQL = SQL & "pakai_budget, lama_budget_harian, lama_budget_bulanan, "
                        SQL = SQL & "budget_harian, budget_bulanan, flag_khusus, flag_biaya, flag_pbk"
                        SQL = SQL & ",Kode_Account) Values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & valueKodeAccount(0).Trim & "', "
                        SQL = SQL & "'" & Strings.Left(Txt_KodeDetailAccount.Text.Trim, 1) & "', '" & Strings.Mid(Txt_KodeDetailAccount.Text.Trim, 2) & "', "
                        SQL = SQL & "'" & Txt_Keterangan.Text.ToUpper & "', "
                        SQL = SQL & "'" & Strings.Left(Cmb_Posisi.Text, 1) & "', "
                        SQL = SQL & "'" & Cmb_Cabang.Text & "', '" & Cmb_Letak.Text & "',  "
                        SQL = SQL & "'" & jenis & "', '" & Cmb_FlagAktif.Text & "', '" & Cmb_FlagBudget.Text & "', "
                        SQL = SQL & "'" & lama_budget_harian & "', '" & lama_budget_bulanan & "', "
                        SQL = SQL & "'" & nilai_budget_harian & "', '" & nilai_budget_bulanan & "', '" & Cmb_FlagKhusus.Text & "','" & Cmb_FlagBiaya.Text & "','" & Cmb_FlagPBK.Text & "',"
                        SQL = SQL & "'" & valueKodeAccount(0).Trim & Txt_KodeDetailAccount.Text.Trim & "')"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "Insert Into Detail_Account(Kode_Perusahaan, Kode_Master_Acc, Kode_Acc, "
                        SQL = SQL & "Kode_Detail_Acc, Keterangan, Posisi, lokasi, letak, jns, aktif, "
                        SQL = SQL & "pakai_budget, lama_budget_harian, lama_budget_bulanan, "
                        SQL = SQL & "budget_harian, budget_bulanan, flag_khusus, flag_biaya, flag_pbk"
                        SQL = SQL & ",Kode_Account) Values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & valueKodeAccount(0).Trim & "', "
                        SQL = SQL & "'" & Strings.Left(Txt_KodeDetailAccount.Text.Trim, 1) & "', '" & Strings.Mid(Txt_KodeDetailAccount.Text.Trim, 2) & "', "
                        SQL = SQL & "'" & Txt_Keterangan.Text.ToUpper & "', "
                        SQL = SQL & "'" & Strings.Left(Cmb_Posisi.Text, 1) & "', "
                        SQL = SQL & "'" & Cmb_Cabang.Text & "', '" & Cmb_Letak.Text & "',  "
                        SQL = SQL & "'" & jenis & "', '" & Cmb_FlagAktif.Text & "', '" & Cmb_FlagBudget.Text & "', "
                        SQL = SQL & "'" & lama_budget_harian & "', '" & lama_budget_bulanan & "', "
                        SQL = SQL & "'" & nilai_budget_harian & "', '" & nilai_budget_bulanan & "', '" & Cmb_FlagKhusus.Text & "',NULL,'" & Cmb_FlagPBK.Text & "',"
                        SQL = SQL & "'" & valueKodeAccount(0).Trim & Txt_KodeDetailAccount.Text.Trim & "')"
                        ExecuteTrans(SQL)
                    End If

                    If Cmb_Letak.SelectedIndex = 0 Then 'lr
                        SQL = "Insert Into LR_lokasi(kode_perusahaan, lokasi, kode_account, ket) values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & Cmb_Cabang.Text & "', "
                        SQL = SQL & "'" & valueKodeAccount(0).Trim & Txt_KodeDetailAccount.Text.Trim & "', "
                        SQL = SQL & "'" & Cmb_LR.Text & "')"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "Insert Into " & Cmb_Neraca.Text & "(kode_perusahaan,kode_account) values("
                        SQL = SQL & "'" & KodePerusahaan & "', "
                        SQL = SQL & "'" & valueKodeAccount(0).Trim & Txt_KodeDetailAccount.Text.Trim & "')"
                        ExecuteTrans(SQL)
                    End If
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Untuk Proses Simpan Aktif = Y ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                '==============================
                '=     INSERT COST CENTER     =
                '==============================
                For i As Integer = 0 To LvInput_CostCenter.Items.Count - 1

                    If LvInput_CostCenter.Items(i).Checked Then

                        SQL = "insert into Account_Per_Cost_Center (Kode_Perusahaan, Kode_Account, Id_Cost_Center) values "
                        SQL = SQL & "('" & KodePerusahaan & "', '" & valueKodeAccount(0).Trim & Txt_KodeDetailAccount.Text.Trim & "', '" & arrIdCostCenter(i) & "')"
                        ExecuteTrans(SQL)

                    End If

                Next

            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                If CekButtonRole("update_account") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "select kode_master_acc, kode_acc, kode_detail_acc, letak, jns from detail_account "
                SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_master_acc = '" & valueKodeAccount(0).Trim & "' and "
                'SQL = SQL & "kode_acc = '" & Pisah2(0).Trim & "' and "
                SQL = SQL & "kode_acc + kode_detail_acc = '" & Txt_KodeDetailAccount.Text.Trim & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If dr("letak") = "LR" Then
                            SQL = "delete from lr_lokasi where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_account = '" & dr("kode_master_acc") & dr("kode_acc") & dr("kode_detail_acc") & "' "

                            dr.Close()

                            ExecuteTrans(SQL)
                        Else
                            SQL = "delete from " & dr("jns") & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_account = '" & dr("kode_master_acc") & dr("kode_acc") & dr("kode_detail_acc") & "' "

                            dr.Close()

                            ExecuteTrans(SQL)
                        End If
                    End If
                End Using

                If Cmb_FlagBiaya.Text = "Y" Then
                    SQL = "Update Detail_Account Set Keterangan = '" & Txt_Keterangan.Text.ToUpper & "', "
                    SQL = SQL & "posisi = '" & Strings.Left(Cmb_Posisi.Text, 1) & "', "
                    SQL = SQL & "lokasi = '" & Cmb_Cabang.Text & "', "
                    SQL = SQL & "letak = '" & Cmb_Letak.Text & "', jns = '" & jenis & "', "
                    SQL = SQL & "aktif = '" & Cmb_FlagAktif.Text & "', "
                    SQL = SQL & "pakai_budget = '" & Cmb_FlagBudget.Text & "', "
                    SQL = SQL & "lama_budget_harian = '" & lama_budget_harian & "', "
                    SQL = SQL & "lama_budget_bulanan = '" & lama_budget_bulanan & "', "
                    SQL = SQL & "budget_harian = '" & nilai_budget_harian & "', "
                    SQL = SQL & "budget_bulanan = '" & nilai_budget_bulanan & "', flag_khusus = '" & Cmb_FlagKhusus.Text & "', flag_biaya = '" & Cmb_FlagBiaya.Text & "',flag_pbk = '" & Cmb_FlagPBK.Text & "' "
                    SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_master_acc = '" & valueKodeAccount(0).Trim & "' and "
                    SQL = SQL & "kode_acc = '" & Strings.Left(Txt_KodeDetailAccount.Text.Trim, 1) & "' and "
                    SQL = SQL & "kode_detail_acc = '" & Strings.Mid(Txt_KodeDetailAccount.Text.Trim, 2) & "'"
                    ExecuteTrans(SQL)
                Else
                    SQL = "Update Detail_Account Set Keterangan = '" & Txt_Keterangan.Text.ToUpper & "', "
                    SQL = SQL & "posisi = '" & Strings.Left(Cmb_Posisi.Text, 1) & "', "
                    SQL = SQL & "lokasi = '" & Cmb_Cabang.Text & "', "
                    SQL = SQL & "letak = '" & Cmb_Letak.Text & "', jns = '" & jenis & "', "
                    SQL = SQL & "aktif = '" & Cmb_FlagAktif.Text & "', "
                    SQL = SQL & "pakai_budget = '" & Cmb_FlagBudget.Text & "', "
                    SQL = SQL & "lama_budget_harian = '" & lama_budget_harian & "', "
                    SQL = SQL & "lama_budget_bulanan = '" & lama_budget_bulanan & "', "
                    SQL = SQL & "budget_harian = '" & nilai_budget_harian & "', "
                    SQL = SQL & "budget_bulanan = '" & nilai_budget_bulanan & "', flag_khusus = '" & Cmb_FlagKhusus.Text & "', flag_biaya = NULL, flag_pbk = '" & Cmb_FlagPBK.Text & "' "
                    SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_master_acc = '" & valueKodeAccount(0).Trim & "' and "
                    SQL = SQL & "kode_acc = '" & Strings.Left(Txt_KodeDetailAccount.Text.Trim, 1) & "' and "
                    SQL = SQL & "kode_detail_acc = '" & Strings.Mid(Txt_KodeDetailAccount.Text.Trim, 2) & "'"
                    ExecuteTrans(SQL)

                    If Cmb_Letak.SelectedIndex = 0 Then 'lr
                        SQL = "Insert Into LR_lokasi(kode_perusahaan, lokasi, kode_account, ket) values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & Cmb_Cabang.Text & "', "
                        SQL = SQL & "'" & valueKodeAccount(0).Trim & Txt_KodeDetailAccount.Text.Trim & "', "
                        SQL = SQL & "'" & Cmb_LR.Text & "')"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "Insert Into " & Cmb_Neraca.Text & "(kode_perusahaan,kode_account) values("
                        SQL = SQL & "'" & KodePerusahaan & "', "
                        SQL = SQL & "'" & valueKodeAccount(0).Trim & Txt_KodeDetailAccount.Text.Trim & "')"
                        ExecuteTrans(SQL)
                    End If
                End If

                '===========================================================
                '=     DELETE ACCOUNT PER COSTCENTER BY KODE_ACCOUNT       =
                '===========================================================
                SQL = "delete Account_Per_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Account = '" & valueKodeAccount(0).Trim & Txt_KodeDetailAccount.Text.Trim & "'"
                ExecuteTrans(SQL)

                '==============================
                '=     INSERT COST CENTER     =
                '==============================
                For i As Integer = 0 To LvInput_CostCenter.Items.Count - 1

                    If LvInput_CostCenter.Items(i).Checked Then

                        SQL = "insert into Account_Per_Cost_Center (Kode_Perusahaan, Kode_Account, Id_Cost_Center) values "
                        SQL = SQL & "('" & KodePerusahaan & "', '" & valueKodeAccount(0).Trim & Txt_KodeDetailAccount.Text.Trim & "', '" & arrIdCostCenter(i) & "')"
                        ExecuteTrans(SQL)

                    End If

                Next

            End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            Kosong_Input()

            MessageBox.Show("Detail Account Berhasil DiSimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            TabControl1.SelectedIndex = 1

            Cmb_Display_Filter.Text = ""
            Cmb_Display_Filter.SelectedIndex = -1
            Txt_Display_Filter.Text = ""
            Get_Data_Display()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus . . ? ?", "Accounting", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            valueKodeAccount = Cmb_KodeAccount.Text.Split("-")
            'Pisah2 = ComboBox3.Text.Split("-")

            'OpenConn()
            'If CekButtonRole("update_account") = "T" Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            'CloseConn()

            'Detail Jurnal
            'Using Dr1 As SqlClient.SqlDataReader = General_Class.Open("Select top 1 * from Detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Trim(Pisah1(0)) & "' and kode_acc = '" & Trim(Pisah2(0)) & "' and kode_detail_acc = '" & TextBox1.Text & "'")
            Using Dr1 As SqlClient.SqlDataReader = General_Class.Open("Select top 1 * from Detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Trim(valueKodeAccount(0)) & "' and kode_acc + kode_detail_acc = '" & Txt_KodeDetailAccount.Text & "'")
                If Dr1.Read Then
                    MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data detail jurnal", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Kosong_All()
                    Cmb_KodeAccount.Focus()
                    Exit Sub
                End If
            End Using

            OpenConn()
            If CekButtonRole("update_account") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'Cmd.CommandText = "Delete From Detail_Account where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Pisah1(0).Trim & "' and kode_acc = '" & Pisah2(0).Trim & "' and kode_detail_acc = '" & TextBox1.Text & "'"
            'Cmd.ExecuteNonQuery()
            'Cmd = Nothing

            SQL = "select kode_master_acc, kode_acc, kode_detail_acc, letak, jns from detail_account "
            SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_master_acc = '" & valueKodeAccount(0).Trim & "' and "
            'SQL = SQL & "kode_acc = '" & Pisah2(0).Trim & "' and "
            SQL = SQL & "kode_acc + kode_detail_acc = '" & Txt_KodeDetailAccount.Text.Trim & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("letak") = "LR" Then
                        SQL = "delete from lr_lokasi where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_account = '" & dr("kode_master_acc") & dr("kode_acc") & dr("kode_detail_acc") & "' "

                        dr.Close()

                        ExecuteTrans(SQL)
                    Else
                        SQL = "delete from " & dr("jns") & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_account = '" & dr("kode_master_acc") & dr("kode_acc") & dr("kode_detail_acc") & "' "

                        dr.Close()

                        ExecuteTrans(SQL)
                    End If
                End If
                Cmd.CommandText = "Delete From Detail_Account where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & valueKodeAccount(0).Trim & "' and kode_acc = '" & Strings.Left(Txt_KodeDetailAccount.Text.Trim, 1) & "' and kode_detail_acc = '" & Strings.Mid(Txt_KodeDetailAccount.Text.Trim, 2) & "'"
                Cmd.ExecuteNonQuery()
                Cmd = Nothing
            End Using
            CloseConn()
        Else
            MessageBox.Show("Penghapusan dibatalkan . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        Kosong_All()
        Cmb_KodeAccount.Focus()
    End Sub


    Private Sub Txt_KodeDetailAccount_Leave(sender As Object, e As EventArgs) Handles Txt_KodeDetailAccount.Leave
        If Cmb_KodeAccount.Text.Trim.Length = 0 Then Exit Sub
        'If ComboBox3.Text.Trim.Length = 0 Then Exit Sub
        If Txt_KodeDetailAccount.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Dim kode_account As String = ""

            valueKodeAccount = Cmb_KodeAccount.Text.Split("-")
            'Pisah2 = ComboBox3.Text.Split("-")
            'Cmd.CommandText = "Select * From Detail_Account Where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Pisah1(0).Trim & "' and kode_acc = '" & Pisah2(0).Trim & "' and kode_detail_acc = '" & TextBox1.Text & "'"
            SQL = "Select Kode_Account, keterangan, posisi, lokasi, letak, jns, aktif, pakai_budget, lama_budget_harian, budget_harian, lama_budget_bulanan,           "
            SQL = SQL & "budget_bulanan, flag_khusus, flag_biaya, flag_pbk  "
            SQL = SQL & "From Detail_Account Where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & valueKodeAccount(0).Trim & "'"
            SQL = SQL & "and kode_acc + kode_detail_acc = '" & Txt_KodeDetailAccount.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    kode_account = Dr("Kode_Account")

                    Txt_Keterangan.Text = Dr("keterangan")
                    If Dr("posisi") = "D" Then Cmb_Posisi.SelectedIndex = 0 Else Cmb_Posisi.SelectedIndex = 1

                    Cmb_Cabang.Text = Dr("lokasi")
                    Cmb_Letak.Text = Dr("letak")
                    If Dr("letak") = "LR" Then
                        Cmb_LR.Text = Dr("jns")
                    Else
                        Cmb_Neraca.Text = Dr("jns")
                    End If
                    Cmb_FlagAktif.Text = Dr("aktif")
                    Cmb_FlagBudget.Text = Dr("pakai_budget")

                    If Dr("pakai_budget") = "Y" Then
                        Cmb_Cabang.Text = Dr("lama_budget_harian") : Cmb_Letak.Text = Dr("budget_harian")
                        Cmb_LR.Text = Dr("lama_budget_bulanan") : Cmb_Neraca.Text = Dr("budget_bulanan")

                        Cmb_Cabang.Enabled = True : Cmb_Letak.Enabled = True
                        Cmb_LR.Enabled = True : Cmb_Neraca.Enabled = True
                    Else
                        Cmb_Cabang.Text = "" : Cmb_Letak.Text = ""
                        Cmb_LR.Text = "" : Cmb_Neraca.Text = ""

                        Cmb_Cabang.Enabled = False : Cmb_Letak.Enabled = False
                        Cmb_LR.Enabled = False : Cmb_Neraca.Enabled = False
                    End If

                    Cmb_FlagKhusus.Text = Dr("flag_khusus")
                    If General_Class.CekNULL(Dr("flag_biaya")) = "" Then
                        Cmb_FlagBiaya.Text = "T"
                    Else
                        Cmb_FlagBiaya.Text = (General_Class.CekNULL(Dr("flag_biaya")))
                    End If
                    Cmb_FlagPBK.Text = Dr("flag_pbk")

                    Txt_KodeDetailAccount.Enabled = False

                    Btn_Simpan.Text = "&Update"
                    Btn_Simpan.Tag = "UPDATE"
                    Btn_Hapus.Enabled = True
                Else
                    Dr.Close()
                    Txt_Keterangan.Text = ""
                    Cmb_Posisi.SelectedIndex = -1
                    Cmb_Cabang.SelectedIndex = -1
                    Cmb_Letak.SelectedIndex = 0
                    Cmb_LR.Enabled = True
                    Cmb_LR.SelectedIndex = -1
                    Cmb_Neraca.SelectedIndex = -1
                    Cmb_FlagAktif.SelectedIndex = -1
                    Cmb_FlagBudget.SelectedIndex = -1
                    Cmb_FlagKhusus.SelectedIndex = -1
                    Cmb_FlagBiaya.SelectedIndex = -1
                    Cmb_FlagPBK.SelectedIndex = -1
                    Btn_Simpan.Text = "&Simpan"
                    Btn_Simpan.Tag = "SIMPAN"
                    Btn_Hapus.Enabled = False
                    Txt_KodeDetailAccount.Enabled = True
                End If
            End Using

            If Not kode_account = "" Then

                LvInput_CostCenter.Items.Clear()
                Dim arrSelectedCostCenter As New ArrayList
                '=======================================
                '=     GET ACCOUNT PER COST CENTER     =
                '=======================================
                SQL = "select Kode_Account, Id_Cost_Center from Account_Per_Cost_Center "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Account = '" & kode_account & "'"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        arrSelectedCostCenter.Add(Dr("Id_Cost_Center"))
                    Loop
                End Using

                '============================
                '=     LOAD COST CENTER     =
                '============================
                SQL = "select Id_Cost_Center, Kode_Cost_Center, Keterangan, Id_Divisi, Id_Sub_Divisi "
                SQL = SQL & "from EMI_Master_Cost_Center "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "'"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim lv As ListViewItem
                        lv = LvInput_CostCenter.Items.Add("") : arrIdCostCenter.Add(Dr("Id_Cost_Center"))
                        lv.SubItems.Add(Dr("Keterangan"))

                        If arrSelectedCostCenter.Contains(Dr("Id_Cost_Center")) Then
                            lv.Checked = True
                        Else
                            lv.Checked = False
                        End If
                    Loop
                End Using

            End If

            TabControl1.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Display_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Display.DoubleClick
        If Lv_Display.Items.Count = 0 Then Exit Sub

        For i As Integer = 0 To Cmb_KodeAccount.Items.Count - 1
            valueKodeAccount = Cmb_KodeAccount.Items(i).Split("-")
            If Trim(valueKodeAccount(0)) = Lv_Display.Items(Lv_Display.FocusedItem.Index).Text Then
                Cmb_KodeAccount.SelectedIndex = i
                Exit For
            End If
        Next

        Txt_KodeDetailAccount.Text = Lv_Display.FocusedItem.SubItems(1).Text ' Replace(ListView1.Items(ListView1.FocusedItem.Index).SubItems(1).Text, ".", "")
        If Lv_Display.Items(Lv_Display.FocusedItem.Index).SubItems(2).Text = "D" Then
            Cmb_Posisi.SelectedIndex = 0
        Else
            Cmb_Posisi.SelectedIndex = 1
        End If
        Txt_KodeDetailAccount_Leave(Lv_Display, e)
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 2 Then
            Load_DGV_Tab3()
        End If
    End Sub

    Private Sub Load_DGV_Tab3()

        Try
            OpenConn()

            '===========================================
            '=     LOAD DINAMIS COLUMN COST CENTER     =
            '===========================================
            Dim firstDynamicColumn As Integer = ColDinamis
            SQL = "select Id_Cost_Center, Kode_Cost_Center, Keterangan from EMI_Master_Cost_Center order by Id_Cost_Center "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim checkBoxColumn As New DataGridViewCheckBoxColumn()

                    checkBoxColumn.HeaderText = Dr("Keterangan")
                    checkBoxColumn.Name = Dr("Id_Cost_Center")
                    checkBoxColumn.Width = 110
                    checkBoxColumn.ReadOnly = False

                    Dgv_Binding.Columns.Add(checkBoxColumn)

                    firstDynamicColumn += firstDynamicColumn
                Loop

            End Using

            '=============================
            '=     LOAD DATA ACCOUNT     =
            '=============================
            Dgv_Binding.Rows.Clear()
            SQL = "select Kode_Account, Kode_Detail_Acc, Keterangan "
            SQL = SQL & "from Detail_Account "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim Row As Integer = 0

                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Binding.Rows.Add(1)
                            Dgv_Binding.Rows(Row).Cells(itemBinding_KdAccount).Value = .Rows(i).Item("Kode_Account")
                            Dgv_Binding.Rows(Row).Cells(itemBinding_KdDetailAccount).Value = .Rows(i).Item("Kode_Detail_Acc")
                            Dgv_Binding.Rows(Row).Cells(itemBinding_KeteranganAccount).Value = .Rows(i).Item("Keterangan")

                            '====================================
                            '=     GET DATA PER COST CENTER     =
                            '====================================
                            SQL = "select Kode_Account, Id_Cost_Center "
                            SQL = SQL & "from Account_Per_Cost_Center "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Account = '" & .Rows(i).Item("Kode_Account") & "' "
                            SQL = SQL & "order by Kode_Account, Id_Cost_Center"
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                        For k As Integer = ColDinamis To Dgv_Binding.Columns.Count - 1

                                            If Ds1.Tables("MyTable").Rows(j).Item("Id_Cost_Center") = Dgv_Binding.Columns(k).Name Then

                                                Dgv_Binding.Rows(Row).Cells(k).Value = True


                                            Else
                                                Dgv_Binding.Rows(Row).Cells(k).Value = False

                                            End If


                                        Next

                                    Next
                                End If
                            End Using

                            Row += 1

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

    Private Sub Txt_KodeDetailAccount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KodeDetailAccount.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

End Class

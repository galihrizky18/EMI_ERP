Public Class Display_Validasi_Budget_Work_Center

    Dim JudulForm As String = "Validasi Budget Work Center"

    Dim arrCari1, arrCari2 As New ArrayList

    Dim Lv_NoTransaksi, Lv_JenisBiaya, Lv_Tanggal, Lv_UserId, Lv_JumlahProduksi, Lv_TotBudget, Lv_JumlahPemakaian, Lv_TarifPerSatuan, Lv_TotAktual, Lv_Selisih, Lv_BudgetBaru, Lv_BudgetLama, Lv_JnsBiaya As String
    Dim LvDet_idWorkCenter, LvDet_WorkCenter, LvDet_Persentase, LvDet_Nilai, LvDet_BudgetLama, LvDet_BudgetBaru As String

    Dim item_NoTransaksi As Integer = 0
    Dim item_JenisBiaya As Integer = 1
    Dim item_Tanggal As Integer = 2
    Dim item_UserId As Integer = 3
    Dim item_JmlhProduksi As Integer = 4
    Dim item_TotBudget As Integer = 5
    Dim item_JmlhPemakaian As Integer = 6
    Dim item_TarifPerSatuan As Integer = 7
    Dim item_TotAktual As Integer = 8
    Dim item_Selisih As Integer = 9
    Dim item_BudgetBaru As Integer = 10
    Dim item_BUdgetLama As Integer = 11
    Dim item_JnsBiaya As Integer = 12

    Dim itemDet_IdWorkCenter As Integer = 0
    Dim itemDet_WorkCenter As Integer = 1
    Dim itemDet_Persentase As Integer = 2
    Dim itemDet_Nilai As Integer = 3
    Dim itemDet_BudgetLama As Integer = 4
    Dim itemDet_BudgetBaru As Integer = 5



    Private Sub EMI_Validasi_Budget_Work_Center_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Validasi_Budget_Work_Center_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Initial_Lv_Aktual_WorkCenter()
        Initial_Lv_Detail()
        Kosong()

    End Sub

    Private Sub Kosong()

        Lv_Aktual_WorkCenter.Items.Clear()
        Lv_Detail.Items.Clear()

        '-== FIlter ==-'
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear() : arrCari1.Clear()
        ComboBox3.Items.Clear() : arrCari2.Clear()
        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now
        TextBox4.Text = ""
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False

        ComboBox2.Items.Add("Tanggal") : arrCari1.Add("a.Tanggal")

        ComboBox3.Items.Add("No Transaksi") : arrCari2.Add("a.No_Transaksi")
        ComboBox3.Items.Add("Jenis Biaya") : arrCari2.Add("b.keterangan")
        ComboBox3.Items.Add("User") : arrCari2.Add("a.UserId")

        Try
            OpenConn()

            ComboBox1.Items.Clear()
            SQL = "select kode_perusahaan, kode_stock_owner, Keterangan from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    ComboBox1.Items.Add(Dr("kode_stock_owner"))
                Loop
                ComboBox1.SelectedIndex = 1
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Load_Aktual_WorkCenter(False)

    End Sub

    Private Sub Initial_Lv_Aktual_WorkCenter()

        Lv_Aktual_WorkCenter.Columns.Clear()
        Lv_Aktual_WorkCenter.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
        Lv_Aktual_WorkCenter.Columns.Add("Jenis Biaya", 130, HorizontalAlignment.Left)
        Lv_Aktual_WorkCenter.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Aktual_WorkCenter.Columns.Add("User", 120, HorizontalAlignment.Left)
        Lv_Aktual_WorkCenter.Columns.Add("Jumlah Produksi", 0, HorizontalAlignment.Right)
        Lv_Aktual_WorkCenter.Columns.Add("Total Budget", 160, HorizontalAlignment.Right)
        Lv_Aktual_WorkCenter.Columns.Add("Jumlah Pemakaian", 0, HorizontalAlignment.Right)
        Lv_Aktual_WorkCenter.Columns.Add("Tarif Per Satuan", 0, HorizontalAlignment.Right)
        Lv_Aktual_WorkCenter.Columns.Add("Total Aktual", 160, HorizontalAlignment.Right)
        Lv_Aktual_WorkCenter.Columns.Add("Selisih", 160, HorizontalAlignment.Right)
        Lv_Aktual_WorkCenter.Columns.Add("Budget Baru", 160, HorizontalAlignment.Right)
        Lv_Aktual_WorkCenter.Columns.Add("Budget Lama", 160, HorizontalAlignment.Right)
        Lv_Aktual_WorkCenter.Columns.Add("JnsBiaya", 0, HorizontalAlignment.Right)
        Lv_Aktual_WorkCenter.View = View.Details

    End Sub

    Private Sub Initial_Lv_Detail()

        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("id_work_center", 0, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Work Center", 200, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Persentase", 160, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Nilai", 160, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Budget Lama", 160, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Budget Baru", 160, HorizontalAlignment.Right)
        Lv_Detail.View = View.Details

    End Sub

    Private Sub Get_Data_Lv_Aktual(ByVal index As Integer)

        Lv_NoTransaksi = Lv_Aktual_WorkCenter.Items(index).SubItems(item_NoTransaksi).Text
        Lv_JenisBiaya = Lv_Aktual_WorkCenter.Items(index).SubItems(item_JenisBiaya).Text
        Lv_Tanggal = Lv_Aktual_WorkCenter.Items(index).SubItems(item_Tanggal).Text
        Lv_UserId = Lv_Aktual_WorkCenter.Items(index).SubItems(item_UserId).Text
        Lv_JumlahProduksi = Lv_Aktual_WorkCenter.Items(index).SubItems(item_JmlhProduksi).Text
        Lv_TotBudget = Lv_Aktual_WorkCenter.Items(index).SubItems(item_TotBudget).Text
        Lv_JumlahPemakaian = Lv_Aktual_WorkCenter.Items(index).SubItems(item_JmlhPemakaian).Text
        Lv_TarifPerSatuan = Lv_Aktual_WorkCenter.Items(index).SubItems(item_TarifPerSatuan).Text
        Lv_TotAktual = Lv_Aktual_WorkCenter.Items(index).SubItems(item_TotAktual).Text
        Lv_Selisih = Lv_Aktual_WorkCenter.Items(index).SubItems(item_Selisih).Text
        Lv_BudgetBaru = Lv_Aktual_WorkCenter.Items(index).SubItems(item_BudgetBaru).Text
        Lv_BudgetLama = Lv_Aktual_WorkCenter.Items(index).SubItems(item_BUdgetLama).Text
        Lv_JnsBiaya = Lv_Aktual_WorkCenter.Items(index).SubItems(item_JnsBiaya).Text

    End Sub

    Private Sub Get_Data_Lv_Aktual_Detail(ByVal index As Integer)

        LvDet_idWorkCenter = Lv_Detail.Items(index).SubItems(itemDet_IdWorkCenter).Text
        LvDet_WorkCenter = Lv_Detail.Items(index).SubItems(itemDet_WorkCenter).Text
        LvDet_Persentase = Lv_Detail.Items(index).SubItems(itemDet_Persentase).Text
        LvDet_Nilai = Lv_Detail.Items(index).SubItems(itemDet_Nilai).Text
        LvDet_BudgetLama = Lv_Detail.Items(index).SubItems(itemDet_BudgetLama).Text
        LvDet_BudgetBaru = Lv_Detail.Items(index).SubItems(itemDet_BudgetBaru).Text

    End Sub

    Private Sub Load_Aktual_WorkCenter(ByVal filter As Boolean)
        Try
            OpenConn()

            Lv_Aktual_WorkCenter.Items.Clear() : Lv_Detail.Items.Clear()
            SQL = "select a.No_Transaksi, a.Jenis_Biaya, b.keterangan as Ket_JenisBiaya, a.Tanggal, a.Jam, a.UserId, a.Jumlah_Produksi, a.Total_Budgeting, a.Jumlah_Pemakaian, "
            SQL = SQL & "a.Nilai_Tarif_PerSatuan, a.Total_Aktual, a.Selisih, a.Nilai_BudgetBaru, a.Nilai_BudgetLama, a.Flag_Validasi "
            SQL = SQL & "from EMI_Aktualisasi_Budgeting_WorkCenter a, Emi_Jenis_Biaya_Produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Jenis_Biaya = b.Kode_Jenis_Biaya_Produksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If filter Then

                If CheckBox1.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & " a.Tanggal between '"
                    SQL = SQL & Format(Date.Now, "yyyy-MM-dd") & "' and '" & Format(Date.Now.AddDays(1), "yyyy-MM-dd") & "' "
                End If

                If CheckBox2.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & arrCari1.Item(ComboBox2.SelectedIndex) & " between '"
                    SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                End If

                If CheckBox3.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & arrCari2.Item(ComboBox3.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
                End If

            End If

            SQL = SQL & "order by a.Jenis_Biaya, a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Dim Lv As ListViewItem
                    Lv = Lv_Aktual_WorkCenter.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("Ket_JenisBiaya"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("UserId"))
                    Lv.SubItems.Add(Format(Val(Dr("Jumlah_Produksi")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Total_Budgeting")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Jumlah_Pemakaian")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Nilai_Tarif_PerSatuan")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Total_Aktual")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Selisih")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Nilai_BudgetBaru")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Nilai_BudgetLama")), "N2"))
                    Lv.SubItems.Add(Dr("Jenis_Biaya"))

                    If General_Class.CekNULL(Dr("Flag_Validasi")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(Dr("Flag_Validasi")) = "T" Then
                        Lv.BackColor = Color.FromArgb(200, 0, 0)
                        Lv.ForeColor = Color.White
                    ElseIf General_Class.CekNULL(Dr("Flag_Validasi")) = "" Then
                        Lv.BackColor = Color.LightGray
                    End If
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Not CheckBox1.Checked And Not CheckBox2.Checked And Not CheckBox3.Checked Then
            MessageBox.Show("Pilih Terlebih Dahulu Filter", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CheckBox1.Focus() : Exit Sub

        ElseIf ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Perusahaan Pilih Terlebih Dahulu", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        ElseIf CheckBox2.Checked Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("Jenis Tanggal Pilih Terlebih Dahulu", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox2.Focus() : Exit Sub
            End If
        ElseIf CheckBox3.Checked Then
            If ComboBox3.SelectedIndex = -1 Then
                MessageBox.Show("Jenis Param Lain Pilih Terlebih Dahulu", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox3.Focus() : Exit Sub
            End If
        End If

        Load_Aktual_WorkCenter(True)

    End Sub

    Private Sub Lv_Aktual_WorkCenter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Aktual_WorkCenter.SelectedIndexChanged

        If Lv_Aktual_WorkCenter.Items.Count = 0 Then Exit Sub

        Dim SelectedIndex = Lv_Aktual_WorkCenter.FocusedItem.Index
        Get_Data_Lv_Aktual(SelectedIndex)

        Try
            OpenConn()



            Lv_Detail.Items.Clear()
            SQL = "select a.No_Transaksi, a.Id_WorkCenter, b.Keterangan as Work_Center, a.Nilai, a.Persentase, a.Budget_Baru, a.Budget_Lama "
            SQL = SQL & "from EMI_Aktualisasi_Budgeting_WorkCenter_Detail a, EMI_Master_Work_Center b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_WorkCenter = b.Id_Work_Center "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Lv_NoTransaksi & "' "
            SQL = SQL & "order by Id_Work_Center"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("Id_WorkCenter"))
                    Lv.SubItems.Add(Dr("Work_Center"))
                    Lv.SubItems.Add(Format(Val(Dr("Nilai")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Persentase")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Budget_Lama")), "N2"))
                    Lv.SubItems.Add(Format(Val(Dr("Budget_Baru")), "N2"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked Then
            CheckBox2.Checked = False

        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        If CheckBox2.Checked Then
            CheckBox1.Checked = False
            ComboBox2.Enabled = True : ComboBox2.SelectedIndex = -1 : ComboBox2.Text = ""
            DateTimePicker1.Enabled = False : DateTimePicker1.Value = Date.Now
            DateTimePicker2.Enabled = False : DateTimePicker2.Value = Date.Now
        Else
            ComboBox2.Enabled = False : ComboBox2.SelectedIndex = -1 : ComboBox2.Text = ""
            DateTimePicker1.Enabled = False : DateTimePicker1.Value = Date.Now
            DateTimePicker2.Enabled = False : DateTimePicker2.Value = Date.Now

        End If

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = -1 Then Exit Sub

        DateTimePicker1.Enabled = True : DateTimePicker1.Value = Date.Now
        DateTimePicker2.Enabled = True : DateTimePicker2.Value = Date.Now

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged

        If CheckBox3.Checked Then
            ComboBox3.Enabled = True : ComboBox3.SelectedIndex = -1 : ComboBox3.Text = ""
            TextBox4.Enabled = False : TextBox4.Text = ""
        Else
            ComboBox3.Enabled = False : ComboBox3.SelectedIndex = -1 : ComboBox3.Text = ""
            TextBox4.Enabled = False : TextBox4.Text = ""
        End If

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex = -1 Then Exit Sub

        TextBox4.Enabled = True : TextBox4.Text = ""
    End Sub

End Class

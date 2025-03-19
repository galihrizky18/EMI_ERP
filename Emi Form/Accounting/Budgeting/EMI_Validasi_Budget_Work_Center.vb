Public Class EMI_Validasi_Budget_Work_Center

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
            SQL = SQL & "a.Nilai_Tarif_PerSatuan, a.Total_Aktual, a.Selisih, a.Nilai_BudgetBaru, a.Nilai_BudgetLama "
            SQL = SQL & "from EMI_Aktualisasi_Budgeting_WorkCenter a, Emi_Jenis_Biaya_Produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Jenis_Biaya = b.Kode_Jenis_Biaya_Produksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.flag_validasi is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
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
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
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

    Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click

        If Lv_Aktual_WorkCenter.Items.Count = 0 Then Exit Sub
        If Lv_Aktual_WorkCenter.FocusedItem.Index = -1 Then Exit Sub

        Dim pertanyaan = MessageBox.Show("Yakin Ingin DiTerima?", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Get_Data_Lv_Aktual(Lv_Aktual_WorkCenter.FocusedItem.Index)
            Dim KdFak As String = "TCC"
            Dim Faktur As String = KdFak & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Transaksi_Work_Center", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(KdFak) + 4 & ")", KdFak & Format(tgl_skg, "MMyy"))

            '========================
            '=     INSERT INDUK     =
            '========================
            SQL = "insert into Emi_Transaksi_Work_Center (Kode_Perusahaan, No_Faktur, UserID, Tanggal, Jam, Jenis_Biaya) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Faktur & "', '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & Lv_JnsBiaya & "') "
            ExecuteTrans(SQL)

            '=========================
            '=     INSERT DETAIL     =
            '=========================
            SQL = "select Id_Routing, Kode_Routing, Keterangan from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim id_routing As String = .Rows(i).Item("Id_Routing")

                            SQL = "select a.Kode_Perusahaan, a.No_Transaksi, b.Id_WorkCenter, b.Nilai "
                            SQL = SQL & "from EMI_Aktualisasi_Budgeting_WorkCenter a, EMI_Aktualisasi_Budgeting_WorkCenter_Detail b "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                            SQL = SQL & "and a.Status is null "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_Transaksi = '" & Lv_NoTransaksi & "' "
                            SQL = SQL & "and a.Jenis_Biaya = '" & Lv_JnsBiaya & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                        Dim No_transaksi As String = Ds1.Tables("MyTable").Rows(j).Item("No_Transaksi")
                                        Dim Id_workCenter As String = Ds1.Tables("MyTable").Rows(j).Item("Id_WorkCenter")
                                        Dim Nilai As String = Ds1.Tables("MyTable").Rows(j).Item("Nilai")

                                        SQL = "insert into Emi_Transaksi_Work_Center_Detail(Kode_Perusahaan, No_Faktur, Id_Routing, Id_Work_Center, Nilai_Per_Pcs) values "
                                        SQL = SQL & "('" & KodePerusahaan & "', '" & Faktur & "', '" & id_routing & "', '" & Id_workCenter & "', '" & Nilai & "') "
                                        ExecuteTrans(SQL)

                                    Next
                                End If
                            End Using

                        Next
                    End If
                End With
            End Using


            '=======================
            '=     UPDATE FLAG     =
            '=======================
            SQL = "update EMI_Aktualisasi_Budgeting_WorkCenter set flag_validasi = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & Lv_NoTransaksi & "' "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Berhasil DiValidasi", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub TolakToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TolakToolStripMenuItem.Click
        If Lv_Aktual_WorkCenter.Items.Count = 0 Then Exit Sub
        If Lv_Aktual_WorkCenter.FocusedItem.Index = -1 Then Exit Sub

        Dim pertanyaan = MessageBox.Show("Yakin Ingin DiTolak?", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub

        get_jam()
        Try
            OpenConn()
            Get_Data_Lv_Aktual(Lv_Aktual_WorkCenter.FocusedItem.Index)

            '=======================
            '=     UPDATE FLAG     =
            '=======================
            SQL = "update EMI_Aktualisasi_Budgeting_WorkCenter set flag_validasi = 'T' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & Lv_NoTransaksi & "' "
            ExecuteTrans(SQL)

            CloseConn()
            MessageBox.Show("Berhasil DiTolak", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Kosong()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


End Class

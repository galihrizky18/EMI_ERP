Public Class EMI_Compare_Budget_Work_Center_Fix

    Dim judulForm As String = "Aktualisasi Budgeting Work Center"

    Dim cellKeterangan As Integer = 0
    Dim cellNilai As Integer = 1
    Dim cellPersentase As Integer = 2

    Dim arrTabControl As New ArrayList

    Dim dataRowsBudgeting As New ArrayList From {
    "Jumlah Produksi", "Nilai Budgeting", "Total"
    }
    Dim dataRowsActual As New ArrayList From {
    "Jumlah Pemakaian", "Tarif Per satuan", "Total"
    }
    Dim DataRowsSelisih As New ArrayList From {
        "Selisih", "Nilai Budget Baru", "Nilai Budget Lama"
    }

    Dim SampleData As New ArrayList From {"0", "0", "0"}

    Dim FAB = "AB"

    Dim DgvBudget_JmlhProduksi, DgvBudget_NilaiBudget, DgvBudget_TotalBudget As String
    Dim RowBudget_JmlhProduksi As Integer = 0
    Dim RowBudget_NilaiBudget As Integer = 1
    Dim RowBudget_TotalBudget As Integer = 2

    Dim DgvAktual_JmlhPemakaian, DgvAktual_TarifPersatuan, DgvAktual_TotalAktual As String
    Dim RowAktual_JumlahPemaikan As Integer = 0
    Dim RowAktual_TarifPerSatuan As Integer = 1
    Dim RowAktual_TotalAktual As Integer = 2

    Dim DgvSelisih_Selisih, DgvSelisih_SelisihPersen, DgvSelisih_BudgetBaru, DgvSelisih_BudgetBaruPersen, DgvSelisih_BudgetLama As String
    Dim RowSelisih_Selisih As Integer = 0
    Dim RowSelisih_BudgetBaru As Integer = 1
    Dim RowSelisih_BudgetLama As Integer = 2

    Private Sub EMI_Compare_Budget_Work_Center_Fix_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Compare_Budget_Work_Center_Fix_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        Kosong()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Kosong()
    End Sub

    Private Sub get_no_faktur()
        Txt_NoTransaksi.Text = FAB & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Aktualisasi_Budgeting_WorkCenter", "No_Transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Transaksi, 1, " & Len(FAB) + 4 & ")", FAB & Format(tgl_skg, "MMyy"))
    End Sub


    Private Sub Kosong()

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


        Dgv_Budgeting.Rows.Clear()
        Dgv_Actual.Rows.Clear()
        Dgv_Selisih.Rows.Clear()

        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now



        LoadTab()

        Dgv_Budgeting.ClearSelection()
        Dgv_Actual.ClearSelection()
        Dgv_Selisih.ClearSelection()
    End Sub

    Private Sub LoadTab()

        Try



            TabControl.TabPages.Clear() : arrTabControl.Clear()
            OpenConn()
            SQL = "select Kode_Perusahaan, Kode_Jenis_Biaya_Produksi, keterangan from Emi_Jenis_Biaya_Produksi "
            SQL = SQL & "where Kode_Perusahaan = '001' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()

                    Dim newTab As New TabPage(Dr("keterangan")) : arrTabControl.Add(Dr("Kode_Jenis_Biaya_Produksi"))
                    TabControl.TabPages.Add(newTab)

                Loop
            End Using

            TabControl.SelectedIndex = 1
            TabControl.SelectedIndex = 0


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl.SelectedIndexChanged

        If TabControl.TabCount = 0 OrElse arrTabControl.Count = 0 Then Exit Sub


        Dim selectedTabIndex As Integer = TabControl.SelectedIndex


        LoadAllData(arrTabControl(selectedTabIndex))

        If Not Panel_Data.Visible Then
            Panel_Data.Show()
        End If


    End Sub




    Private Sub LoadAllData(ByVal JenisBiaya As String)

        Try
            OpenConn()

            Dim TotalBudgeting As Double = 0
            Dim TotalActual As Double = 0
            Dim JumlahProduksi As Double = 0


            Dgv_Budgeting.Rows.Clear()
            SQL = "select Kode_Jenis_Biaya_Produksi, Jumlah_Produksi, Satuan_Produksi, Nilai_Budgeting, Total from Vw_Aktualisasi_Budgeting_Budgeting "
            SQL = SQL & "where Kode_Jenis_Biaya_Produksi = '" & JenisBiaya & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            For j As Integer = 0 To dataRowsBudgeting.Count - 1

                                Dgv_Budgeting.Rows.Add(1)
                                Dgv_Budgeting.Rows(j).Cells(cellKeterangan).Value = dataRowsBudgeting(j)

                                If j = 0 Then
                                    Dgv_Budgeting.Rows(j).Cells(cellNilai).Value = $"{ Format(.Rows(i).Item("Jumlah_Produksi"), "N2")} { .Rows(i).Item("Satuan_Produksi")}"
                                ElseIf j = 1 Then
                                    Dgv_Budgeting.Rows(j).Cells(cellNilai).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Nilai_Budgeting"))), "N2")
                                ElseIf j = 2 Then
                                    Dgv_Budgeting.Rows(j).Cells(cellNilai).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Total"))), "N2")
                                End If

                            Next
                            TotalBudgeting = .Rows(i).Item("Total")
                            JumlahProduksi = .Rows(i).Item("Jumlah_Produksi")
                        Next
                    End If
                End With
            End Using



            '======================
            Dgv_Actual.Rows.Clear()
            SQL = "select Kode_Jenis_Biaya_Produksi, Jumlah_Pemakaian, Satuan_Produksi, Nilai_Persatuan, Total from Vw_Aktualisasi_Budgeting_Aktualisasi "
            SQL = SQL & "where Kode_Jenis_Biaya_Produksi = '" & JenisBiaya & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            For j As Integer = 0 To dataRowsActual.Count - 1

                                Dgv_Actual.Rows.Add(1)
                                Dgv_Actual.Rows(j).Cells(cellKeterangan).Value = dataRowsActual(j)

                                If j = 0 Then
                                    Dgv_Actual.Rows(j).Cells(cellNilai).Value = $"{ Format(.Rows(i).Item("Jumlah_Pemakaian"), "N2")} { .Rows(i).Item("Satuan_Produksi")}"
                                ElseIf j = 1 Then
                                    Dgv_Actual.Rows(j).Cells(cellNilai).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Nilai_Persatuan"))), "N2")
                                ElseIf j = 2 Then
                                    Dgv_Actual.Rows(j).Cells(cellNilai).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Total"))), "N2")
                                End If

                            Next
                            TotalActual = .Rows(i).Item("Total")
                        Next
                    End If
                End With
            End Using


            '=================================
            '=     GET NILAI BUDGET LAMA     =
            '=================================
            Dim Nilai_BudgetLama As Double = 0
            SQL = "select sum(Nilai_Per_Pcs) as Nilai_Per_Pcs "
            SQL = SQL & "from Emi_Transaksi_Work_Center_Detail_Per_Mesin "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Jenis_Biaya = '" & arrTabControl(TabControl.SelectedIndex) & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Nilai_BudgetLama = Format(Val(HilangkanTanda(Dr("Nilai_Per_Pcs"))), "N0")
                End If
            End Using


            Dgv_Selisih.Rows.Clear()
            For i As Integer = 0 To DataRowsSelisih.Count - 1

                Dgv_Selisih.Rows.Add(1)
                Dgv_Selisih.Rows(i).Cells(cellKeterangan).Value = DataRowsSelisih(i)

                If i = 0 Then
                    Dim Nilai_Selisih As Double = Format((Val(HilangkanTanda(TotalActual)) - Val(HilangkanTanda(TotalBudgeting))), "N0")
                    Dim Nilai_PersentaseSelisih As Double = Format(Val(HilangkanTanda((Val(HilangkanTanda(TotalActual)) - Val(HilangkanTanda(TotalBudgeting))) / Val(HilangkanTanda(TotalBudgeting)) * 100)), "N2")
                    Dgv_Selisih.Rows(i).Cells(cellNilai).Value = Nilai_Selisih
                    Dgv_Selisih.Rows(i).Cells(cellPersentase).Value = $"{Nilai_PersentaseSelisih} %"

                ElseIf i = 1 Then
                    Dim Nilai_BudgetBaru As Double = Format((Val(HilangkanTanda(TotalActual)) / Val(HilangkanTanda(JumlahProduksi))), "N0")
                    Dim NIlai_PersentaseBudgetBaru As Double = Format(Val(HilangkanTanda((Val(HilangkanTanda(Nilai_BudgetLama)) - Val(HilangkanTanda(Nilai_BudgetBaru))) / Val(HilangkanTanda(Nilai_BudgetBaru)) * 100)), "N2")
                    Dgv_Selisih.Rows(i).Cells(cellNilai).Value = Nilai_BudgetBaru
                    Dgv_Selisih.Rows(i).Cells(cellPersentase).Value = $"{NIlai_PersentaseBudgetBaru} %"

                ElseIf i = 2 Then

                    Dgv_Selisih.Rows(i).Cells(cellNilai).Value = Nilai_BudgetLama
                    Dgv_Selisih.Rows(i).Cells(cellPersentase).Value = ""


                End If
            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_All_Nilai()

        For i As Integer = 0 To Dgv_Budgeting.Rows.Count - 1
            If i = RowBudget_JmlhProduksi Then
                DgvBudget_JmlhProduksi = Dgv_Budgeting.Rows(i).Cells(cellNilai).Value
            ElseIf i = RowBudget_NilaiBudget Then
                DgvBudget_NilaiBudget = Dgv_Budgeting.Rows(i).Cells(cellNilai).Value
            ElseIf i = RowBudget_TotalBudget Then
                DgvBudget_TotalBudget = Dgv_Budgeting.Rows(i).Cells(cellNilai).Value
            End If
        Next

        For i As Integer = 0 To Dgv_Actual.Rows.Count - 1
            If i = RowAktual_JumlahPemaikan Then
                DgvAktual_JmlhPemakaian = Dgv_Actual.Rows(i).Cells(cellNilai).Value
            ElseIf i = RowAktual_TarifPerSatuan Then
                DgvAktual_TarifPersatuan = Dgv_Actual.Rows(i).Cells(cellNilai).Value
            ElseIf i = RowAktual_TotalAktual Then
                DgvAktual_TotalAktual = Dgv_Actual.Rows(i).Cells(cellNilai).Value
            End If
        Next


        For i As Integer = 0 To Dgv_Selisih.Rows.Count - 1
            If i = RowSelisih_Selisih Then
                DgvSelisih_Selisih = Dgv_Selisih.Rows(i).Cells(cellNilai).Value
                DgvSelisih_SelisihPersen = Dgv_Selisih.Rows(i).Cells(cellPersentase).Value
            ElseIf i = RowSelisih_BudgetBaru Then
                DgvSelisih_BudgetBaru = Dgv_Selisih.Rows(i).Cells(cellNilai).Value
                DgvSelisih_BudgetBaruPersen = Dgv_Selisih.Rows(i).Cells(cellPersentase).Value
            ElseIf i = RowSelisih_BudgetLama Then
                DgvSelisih_BudgetLama = Dgv_Selisih.Rows(i).Cells(cellNilai).Value
            End If
        Next

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Txt_NoTransaksi.Text.Trim.Length = 0 Then
            MessageBox.Show("Ada Kesalahan Pada No Transaksi", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If DateTimePicker1.Value.Date = DateTimePicker2.Value.Date Then
            MessageBox.Show("Periode Awal dan Periode Akhir Tidak Boleh Sama", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub

        ElseIf DateTimePicker1.Value.Date > DateTimePicker2.Value.Date Then
            MessageBox.Show("Periode Awal Tidak Boleh Melebihi Periode Akhir", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Focus()
            Exit Sub
        End If

        Dim pertanyaan As String = MessageBox.Show("Yakin Ingin Simpan ?", judulForm, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            get_no_faktur()
            Get_All_Nilai()

            '========================
            '=     INSERT INDUK     =
            '========================
            SQL = "insert into EMI_Aktualisasi_Budgeting_WorkCenter (Kode_Perusahaan, No_Transaksi, Tanggal, Jam, UserId, Periode_Awal, Periode_Akhir, Jumlah_Produksi, Total_Budgeting, "
            SQL = SQL & "Jumlah_Pemakaian, Nilai_Tarif_PerSatuan, Total_Aktual, Selisih, Selisih_Persen, Nilai_BudgetBaru, Nilai_BudgetBaru_Persen, Nilai_BudgetLama, Jenis_BIaya) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoTransaksi.Text & "', " & Format(tgl_skg, "yyyy-mm-dd") & ", '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
            SQL = SQL & "" & Format(DateTimePicker1.Value, "yyyy-mm-dd") & ", " & Format(DateTimePicker2.Value, "yyyy-mm-dd") & ", '" & Val(HilangkanTanda(DgvBudget_JmlhProduksi)) & "', '" & Val(HilangkanTanda(DgvBudget_TotalBudget)) & "',  "
            SQL = SQL & "'" & Val(HilangkanTanda(DgvAktual_JmlhPemakaian)) & "', '" & Val(HilangkanTanda(DgvAktual_TarifPersatuan)) & "', '" & Val(HilangkanTanda(DgvAktual_TotalAktual)) & "', '" & Val(HilangkanTanda(DgvSelisih_Selisih)) & "', "
            SQL = SQL & "'" & ConvertToNumber(DgvSelisih_SelisihPersen) & "', '" & Val(HilangkanTanda(DgvSelisih_BudgetBaru)) & "', '" & ConvertToNumber(DgvSelisih_BudgetBaruPersen) & "' , '" & Val(HilangkanTanda(DgvSelisih_BudgetLama)) & "', '" & arrTabControl(TabControl.SelectedIndex) & "')"
            ExecuteTrans(SQL)



            '=========================
            '=     INSERT DETAIL     =
            '=========================
            SQL = "select a.Id_Work_Center, a.Keterangan, "
            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select top 1 x.Persentase "
            SQL = SQL & "from EMI_Persentase_Budget_WorkCenter z, EMI_Persentase_Budget_WorkCenter_Detail x "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
            SQL = SQL & "and a.Id_Work_Center = x.Id_Work_Center "
            SQL = SQL & "and z.Status is null and z.Flag_Release = 'Y'  "
            SQL = SQL & "and z.No_Transaksi = (select top 1 No_Transaksi from EMI_Persentase_Budget_WorkCenter order by Tanggal DESC) "
            SQL = SQL & "), 0) as Persentase "
            SQL = SQL & "from EMI_Master_Work_Center a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim idWorkCenter As String = .Rows(i).Item("Id_Work_Center")
                            Dim persentase As Double = .Rows(i).Item("Persentase")


                            Dim nilai As Double = (Val(HilangkanTanda(persentase)) / 100) * Val(HilangkanTanda(DgvSelisih_BudgetBaru))


                            SQL = "insert into EMI_Aktualisasi_Budgeting_WorkCenter_Detail (Kode_Perusahaan, No_Transaksi, Id_WorkCenter, Nilai) values "
                            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoTransaksi.Text & "', '" & idWorkCenter & "', '" & Val(HilangkanTanda(nilai)) & "') "
                            ExecuteTrans(SQL)



                        Next
                    End If
                End With
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()

    End Sub

    Private Function ConvertToNumber(ByVal angka As String) As Double
        Dim hasil As String = System.Text.RegularExpressions.Regex.Match(angka, "\d+(\.\d+)?").Value
        Return hasil
    End Function
End Class

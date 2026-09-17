Public Class N_EMI_SD_Production_Tracker_All

    Public SelectedPO As String

    Dim item_NoSplit As Integer = 0
    Dim item_StartProduction As Integer = 1
    Dim item_Dosing As Integer = 2
    Dim item_ProductionFG As Integer = 3
    Dim item_LabAnalysis As Integer = 4
    Dim item_MilitarySampling1 As Integer = 5
    Dim item_Sorting_Packing As Integer = 6
    Dim item_MilitarySampling2 As Integer = 7
    Dim item_SortingFinalInspection As Integer = 8



    Private Sub N_EMI_SD_Production_Tracker_All_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Kosong()
    End Sub

    Public Sub Kosong()


        Try
            OpenConn()

            Dgv_Packaging.Rows.Clear()
            SQL = "select No_PO, No_Transaksi, Flag_Start_Production, Tanggal, Jam, "
            SQL = SQL & "Flag_GI, Tgl_GI, Jam_GI, "
            SQL = SQL & "Flag_GR1, Tgl_GR1, Jam_GR1, "
            SQL = SQL & "Flag_Lab_Analysis, Tgl_Lab_Analysis, Jam_Lab_Analysis, "
            SQL = SQL & "Flag_Sampling_1, Tgl_Sampling_1, Jam_Sampling_1, "
            SQL = SQL & "Flag_GR2, Tgl_GR2, Jam_GR2, "
            SQL = SQL & "Flag_Sampling_2, Tgl_Sampling_2, Jam_Sampling_2, "
            SQL = SQL & "Flag_GR3, Tgl_GR3, Jam_GR3 "
            SQL = SQL & "from N_EMI_View_Production_Tracker "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_PO = '" & SelectedPO & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Packaging.Rows.Add(1)
                            Dgv_Packaging.Rows(i).Cells(item_NoSplit).Value = .Rows(i).Item("No_Transaksi")

                            Dgv_Packaging.Rows(i).Cells(item_StartProduction).Value = "Tanggal : " & .Rows(i).Item("Tanggal") & ", " & .Rows(i).Item("Jam")
                            Dgv_Packaging.Rows(i).Cells(item_StartProduction).Style.BackColor = Color.LightGreen

                            If Not .Rows(i).Item("Flag_GI") = "T" Then
                                Dgv_Packaging.Rows(i).Cells(item_Dosing).Value = "Status : " & .Rows(i).Item("Flag_GI") & " " & vbCrLf & "Tanggal : " & .Rows(i).Item("Tgl_GI") & ", " & .Rows(i).Item("Jam_GI")

                                If .Rows(i).Item("Flag_GI").ToString.ToUpper.Trim = "ON PROCESS" Then
                                    Dgv_Packaging.Rows(i).Cells(item_Dosing).Style.BackColor = Color.LightYellow
                                ElseIf .Rows(i).Item("Flag_GI").ToString.ToUpper.Trim = "COMPLETED" Then
                                    Dgv_Packaging.Rows(i).Cells(item_Dosing).Style.BackColor = Color.LightGreen
                                Else
                                    Dgv_Packaging.Rows(i).Cells(item_Dosing).Style.BackColor = Color.White
                                End If
                            End If

                            If Not .Rows(i).Item("Flag_GR1") = "T" Then
                                Dgv_Packaging.Rows(i).Cells(item_ProductionFG).Value = "Status : " & .Rows(i).Item("Flag_GR1") & " " & vbCrLf & "Tanggal : " & .Rows(i).Item("Tgl_GR1") & ", " & .Rows(i).Item("Jam_GR1")

                                If .Rows(i).Item("Flag_GR1").ToString.ToUpper.Trim = "ON PROCESS" Then
                                    Dgv_Packaging.Rows(i).Cells(item_ProductionFG).Style.BackColor = Color.LightYellow
                                ElseIf .Rows(i).Item("Flag_GR1").ToString.ToUpper.Trim = "COMPLETED" Then
                                    Dgv_Packaging.Rows(i).Cells(item_ProductionFG).Style.BackColor = Color.LightGreen
                                Else
                                    Dgv_Packaging.Rows(i).Cells(item_ProductionFG).Style.BackColor = Color.White
                                End If
                            End If

                            If Not .Rows(i).Item("Flag_Lab_Analysis") = "T" Then
                                Dgv_Packaging.Rows(i).Cells(item_LabAnalysis).Value = "Status : " & .Rows(i).Item("Flag_Lab_Analysis") & " " & vbCrLf & "Tanggal : " & .Rows(i).Item("Tgl_Lab_Analysis") & ", " & .Rows(i).Item("Jam_Lab_Analysis")

                                If .Rows(i).Item("Flag_Lab_Analysis").ToString.ToUpper.Trim = "ON PROCESS" Then
                                    Dgv_Packaging.Rows(i).Cells(item_LabAnalysis).Style.BackColor = Color.LightYellow
                                ElseIf .Rows(i).Item("Flag_Lab_Analysis").ToString.ToUpper.Trim = "COMPLETED" Then
                                    Dgv_Packaging.Rows(i).Cells(item_LabAnalysis).Style.BackColor = Color.LightGreen
                                ElseIf .Rows(i).Item("Flag_Lab_Analysis").ToString.ToUpper.Trim = "REJECTED" Then
                                    Dgv_Packaging.Rows(i).Cells(item_LabAnalysis).Style.BackColor = Color.DarkRed
                                    Dgv_Packaging.Rows(i).Cells(item_LabAnalysis).Style.ForeColor = Color.White
                                Else
                                    Dgv_Packaging.Rows(i).Cells(item_LabAnalysis).Style.BackColor = Color.White
                                End If
                            End If

                            If Not .Rows(i).Item("Flag_Sampling_1") = "T" Then
                                Dgv_Packaging.Rows(i).Cells(item_MilitarySampling1).Value = "Status : " & .Rows(i).Item("Flag_Sampling_1") & " " & vbCrLf & "Tanggal : " & .Rows(i).Item("Tgl_Sampling_1") & ", " & .Rows(i).Item("Jam_Sampling_1")

                                If .Rows(i).Item("Flag_Sampling_1").ToString.ToUpper.Trim = "HOLD" Then
                                    Dgv_Packaging.Rows(i).Cells(item_MilitarySampling1).Style.BackColor = Color.LightYellow
                                ElseIf .Rows(i).Item("Flag_Sampling_1").ToString.ToUpper.Trim = "READY FOR PACKAGING" Then
                                    Dgv_Packaging.Rows(i).Cells(item_MilitarySampling1).Style.BackColor = Color.LightGreen
                                Else
                                    Dgv_Packaging.Rows(i).Cells(item_MilitarySampling1).Style.BackColor = Color.White
                                End If
                            End If

                            If Not .Rows(i).Item("Flag_GR2") = "T" Then
                                Dgv_Packaging.Rows(i).Cells(item_Sorting_Packing).Value = "Status : " & .Rows(i).Item("Flag_GR2") & " " & vbCrLf & "Tanggal : " & .Rows(i).Item("Tgl_GR2") & ", " & .Rows(i).Item("Jam_GR2")

                                If .Rows(i).Item("Flag_GR2").ToString.ToUpper.Trim = "ON PROCESS" Then
                                    Dgv_Packaging.Rows(i).Cells(item_Sorting_Packing).Style.BackColor = Color.LightYellow
                                ElseIf .Rows(i).Item("Flag_GR2").ToString.ToUpper.Trim = "COMPLETED" Then
                                    Dgv_Packaging.Rows(i).Cells(item_Sorting_Packing).Style.BackColor = Color.LightGreen
                                Else
                                    Dgv_Packaging.Rows(i).Cells(item_Sorting_Packing).Style.BackColor = Color.White
                                End If
                            End If

                            If Not .Rows(i).Item("Flag_Sampling_2") = "T" Then
                                Dgv_Packaging.Rows(i).Cells(item_MilitarySampling2).Value = "Status : " & .Rows(i).Item("Flag_Sampling_2") & " " & vbCrLf & "Tanggal : " & .Rows(i).Item("Tgl_Sampling_2") & ", " & .Rows(i).Item("Jam_Sampling_2")

                                If .Rows(i).Item("Flag_Sampling_2").ToString.ToUpper.Trim = "HOLD" Then
                                    Dgv_Packaging.Rows(i).Cells(item_MilitarySampling2).Style.BackColor = Color.LightYellow
                                ElseIf .Rows(i).Item("Flag_Sampling_2").ToString.ToUpper.Trim = "READY FOR PACKAGING" Then
                                    Dgv_Packaging.Rows(i).Cells(item_MilitarySampling2).Style.BackColor = Color.LightGreen
                                Else
                                    Dgv_Packaging.Rows(i).Cells(item_MilitarySampling2).Style.BackColor = Color.White
                                End If
                            End If

                            If Not .Rows(i).Item("Flag_GR3") = "T" Then
                                Dgv_Packaging.Rows(i).Cells(item_SortingFinalInspection).Value = "Status : " & .Rows(i).Item("Flag_GR3") & " " & vbCrLf & "Tanggal : " & .Rows(i).Item("Tgl_GR3") & ", " & .Rows(i).Item("Jam_GR3")

                                If .Rows(i).Item("Flag_GR3").ToString.ToUpper.Trim = "ON PROCESS" Then
                                    Dgv_Packaging.Rows(i).Cells(item_SortingFinalInspection).Style.BackColor = Color.LightYellow
                                ElseIf .Rows(i).Item("Flag_GR3").ToString.ToUpper.Trim = "COMPLETED" Then
                                    Dgv_Packaging.Rows(i).Cells(item_SortingFinalInspection).Style.BackColor = Color.LightGreen
                                Else
                                    Dgv_Packaging.Rows(i).Cells(item_SortingFinalInspection).Style.BackColor = Color.White
                                End If
                            End If


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









End Class
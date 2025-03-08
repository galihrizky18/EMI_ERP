Public Class EMI_Compare_Budget_Work_Center2

    Dim CellKeterangan As Integer = 0
    Dim CellNilai As Integer = 1

    Dim dataRowsBudgetingAir As New ArrayList From {
    "Jumlah Produksi", "Nilai Budgeting", "Total"
    }
    Dim dataRowsActualAir As New ArrayList From {
    "Jumlah Pemakaian", "Nilai Per satuan", "Total"
    }

    Dim dataRowsBudgetingListrik As New ArrayList From {
    "Jumlah Produksi", "Nilai Budgeting", "Total"
    }
    Dim dataRowsActualListrik As New ArrayList From {
    "Jumlah Pemakaian", "Nilai Per satuan", "Total"
    }

#Region "AIR"

    Dim NilaiBudgetingAir As New ArrayList From {
    "15 KG", "4,575.00", "68,625.00"
    }

    Dim NilaiActualAir As New ArrayList From {
    "18.4 KG", "4,575.00", "84,180.00"
    }
#End Region

#Region "Listrik"

    Dim NilaiBudgetingListik As New ArrayList From {
    "12000 KG", "1,444.70", "18,058,750.00"
    }

    Dim NilaiActualListrik As New ArrayList From {
    "13100 kWh", "1,444.70", "18,925,570.00"
    }
#End Region



    Private Sub EMI_Compare_Budget_Work_Center2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()

    End Sub

    Private Sub Kosong()


#Region "TAB AIR"

        Dgv_BudgetingAir.Rows.Clear() : Dgv_ActualAir.Rows.Clear()

        Txt_SelisihAir.Text = ""
        Txt_SelisihPersenAir.Text = ""
        Txt_BudgetBaruAir.Text = ""

        LoadDataAIR()

#End Region


#Region "TAB LISTRIK"

        Dgv_BudgetingListrik.Rows.Clear() : Dgv_AktualListrik.Rows.Clear()

        Txt_SelisihListrik.Text = ""
        Txt_SelisihPersenListrik.Text = ""
        Txt_BudgetBaruListrik.Text = ""

        LoadDataListrik()

#End Region


    End Sub

    Private Sub LoadDataAIR()

        Dgv_BudgetingAir.Rows.Clear()
        For i As Integer = 0 To dataRowsBudgetingAir.Count - 1

            Dgv_BudgetingAir.Rows.Add(1)
            Dgv_BudgetingAir.Rows(i).Cells(CellKeterangan).Value = dataRowsBudgetingAir(i)
            Dgv_BudgetingAir.Rows(i).Cells(CellNilai).Value = NilaiBudgetingAir(i)

        Next

        Dgv_ActualAir.Rows.Clear()
        For i As Integer = 0 To dataRowsActualAir.Count - 1

            Dgv_ActualAir.Rows.Add(1)
            Dgv_ActualAir.Rows(i).Cells(CellKeterangan).Value = dataRowsActualAir(i)
            Dgv_ActualAir.Rows(i).Cells(CellNilai).Value = NilaiActualAir(i)

        Next

        Dim Selisih As Double = Val("84180") - Val("68625")
        Dim SelisihPersen As Double = ((Val("84180") - Val("68625")) / Val("68625")) * 100
        Dim NilaiBudgetBaru As Double = Val("84180") / Val("15")

        Txt_SelisihAir.Text = Format(Selisih, "N0")
        Txt_SelisihPersenAir.Text = Format(SelisihPersen, "N2")
        Txt_BudgetBaruAir.Text = Format(NilaiBudgetBaru, "N0")

    End Sub

    Private Sub LoadDataListrik()
        Dgv_BudgetingListrik.Rows.Clear()
        For i As Integer = 0 To dataRowsBudgetingListrik.Count - 1

            Dgv_BudgetingListrik.Rows.Add(1)
            Dgv_BudgetingListrik.Rows(i).Cells(CellKeterangan).Value = dataRowsBudgetingListrik(i)
            Dgv_BudgetingListrik.Rows(i).Cells(CellNilai).Value = NilaiBudgetingListik(i)

        Next

        Dgv_AktualListrik.Rows.Clear()
        For i As Integer = 0 To dataRowsActualListrik.Count - 1

            Dgv_AktualListrik.Rows.Add(1)
            Dgv_AktualListrik.Rows(i).Cells(CellKeterangan).Value = dataRowsActualListrik(i)
            Dgv_AktualListrik.Rows(i).Cells(CellNilai).Value = NilaiActualListrik(i)

        Next

        Dim Selisih As Double = Val("18925570") - Val("18058750")
        Dim SelisihPersen As Double = ((Val("18925570") - Val("18058750")) / Val("18058750")) * 100
        Dim NilaiBudgetBaru As Double = Val("18925570") / Val("12000")

        Txt_SelisihListrik.Text = Format(Selisih, "N0")
        Txt_SelisihPersenListrik.Text = Format(SelisihPersen, "N2")
        Txt_BudgetBaruListrik.Text = Format(NilaiBudgetBaru, "N0")
    End Sub



    '========= EVENT ========='
    Private Sub DataGridView2_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Dgv_BudgetingListrik.CellPainting
        If Dgv_BudgetingListrik.Rows.Count = 0 Or e.RowIndex = -1 Then
            Exit Sub
        End If

        e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
    End Sub

    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Dgv_AktualListrik.CellPainting
        If Dgv_AktualListrik.Rows.Count = 0 Or e.RowIndex = -1 Then
            Exit Sub
        End If

        e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
    End Sub
End Class
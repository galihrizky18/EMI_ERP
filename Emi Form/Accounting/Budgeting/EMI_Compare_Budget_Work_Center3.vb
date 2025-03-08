Public Class EMI_Compare_Budget_Work_Center3

    Dim CellKeterangan As Integer = 0
    Dim CellNilai As Integer = 1

    Dim dataMesin As New ArrayList From {"MESIN 01", "MESIN 02", "MESIN 03"}

    Dim dataRowsBudgetingAir As New ArrayList From {
        "Jumlah Produksi", "Nilai Budgeting", "Total"
    }
    Dim dataRowsActualAir As New ArrayList From {
        "Jumlah Pemakaian", "Nilai Per satuan", "Total"
    }
    Dim DataRowsSelisihAir As New ArrayList From {
        "Selisih", "Selisih ( % )", "Nilai Budget Baru"
    }


#Region "AIR"

    Dim NilaiBudgetingAir As New ArrayList From {
        New ArrayList From {"15 KG", "4,575.00", "68,625.00"},
        New ArrayList From {"21 KG", "4,575.00", "96,075.00"},
        New ArrayList From {"32 KG", "4,575.00", "146,400.00"}
    }

    Dim NilaiActualAir As New ArrayList From {
        New ArrayList From {"18.4 KG", "4,575.00", "84,180.00"},
        New ArrayList From {"20 KG", "4,575.00", "91,500.00"},
        New ArrayList From {"35 KG", "4,575.00", "160,125.00"}
    }

    Dim NilaiSelisihAir As New ArrayList From {
        New ArrayList From {"15,555", "22.66", "5,612.00"},
        New ArrayList From {"-4,575", "-4.76", "43,57.14"},
        New ArrayList From {"13,725", "9.37", "5,003.90"}
    }
#End Region




    Private Sub EMI_Compare_Budget_Work_Center2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()

    End Sub

    Private Sub Kosong()


#Region "TAB AIR"

        Dgv_BudgetingAir.Rows.Clear() : Dgv_ActualAir.Rows.Clear()

        LoadDataAIR()

#End Region




    End Sub

    Private Sub LoadDataAIR()

        Dim rows As Integer = 0

        Dgv_BudgetingAir.Rows.Clear()
        For i As Integer = 0 To dataMesin.Count - 1
            Dgv_BudgetingAir.Rows.Add(1)
            Dgv_BudgetingAir.Rows(rows).Cells(CellKeterangan).Value = dataMesin(i)
            Dgv_BudgetingAir.Rows(rows).Cells(CellNilai).Value = ""

            Dim currentFont As Font = Dgv_BudgetingAir.Font
            Dgv_BudgetingAir.Rows(rows).Cells(0).Style.Font = New Font(currentFont, FontStyle.Bold)
            rows += 1

            For j As Integer = 0 To dataRowsBudgetingAir.Count - 1
                Dgv_BudgetingAir.Rows.Add(1)
                Dgv_BudgetingAir.Rows(rows).Cells(CellKeterangan).Value = $"   {dataRowsBudgetingAir(j)}"
                Dgv_BudgetingAir.Rows(rows).Cells(CellNilai).Value = NilaiBudgetingAir(i)(j)
                rows += 1
            Next
        Next

        rows = 0

        Dgv_ActualAir.Rows.Clear()
        For i As Integer = 0 To dataMesin.Count - 1
            Dgv_ActualAir.Rows.Add(1)
            Dgv_ActualAir.Rows(rows).Cells(CellKeterangan).Value = dataMesin(i)
            Dgv_ActualAir.Rows(rows).Cells(CellNilai).Value = ""

            Dim currentFont As Font = Dgv_ActualAir.Font
            Dgv_BudgetingAir.Rows(rows).Cells(0).Style.Font = New Font(currentFont, FontStyle.Bold)
            rows += 1

            For j As Integer = 0 To dataRowsActualAir.Count - 1
                Dgv_ActualAir.Rows.Add(1)
                Dgv_ActualAir.Rows(rows).Cells(CellKeterangan).Value = $"   {dataRowsActualAir(j)}"
                Dgv_ActualAir.Rows(rows).Cells(CellNilai).Value = NilaiActualAir(i)(j)
                rows += 1
            Next
        Next

        rows = 0

        Dgv_Selisih.Rows.Clear()
        For i As Integer = 0 To dataMesin.Count - 1
            Dgv_Selisih.Rows.Add(1)
            Dgv_Selisih.Rows(rows).Cells(CellKeterangan).Value = dataMesin(i)
            Dgv_Selisih.Rows(rows).Cells(CellNilai).Value = ""

            Dim currentFont As Font = Dgv_Selisih.Font
            Dgv_Selisih.Rows(rows).Cells(0).Style.Font = New Font(currentFont, FontStyle.Bold)
            rows += 1

            For j As Integer = 0 To DataRowsSelisihAir.Count - 1
                Dgv_Selisih.Rows.Add(1)
                Dgv_Selisih.Rows(rows).Cells(CellKeterangan).Value = $"   {DataRowsSelisihAir(j)}"
                Dgv_Selisih.Rows(rows).Cells(CellNilai).Value = NilaiSelisihAir(i)(j)
                rows += 1
            Next
        Next

    End Sub



    '========= EVENT ========='
    Private Sub DataGridView2_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Dgv_BudgetingAir.CellPainting, Dgv_Selisih.CellPainting
        If Dgv_BudgetingAir.Rows.Count = 0 Or e.RowIndex = -1 Then
            Exit Sub
        End If

        e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
    End Sub

    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Dgv_ActualAir.CellPainting
        If Dgv_AktualListrik.Rows.Count = 0 Or e.RowIndex = -1 Then
            Exit Sub
        End If

        e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
    End Sub
End Class
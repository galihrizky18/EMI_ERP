Public Class EMI_Compare_Work_Center

    Dim kodeBiaya As String() = {"LISTRIK", "AIR", "PEKERJA", "MESIN"}
    Dim biayaWorkCenter As String() = {"BIAYA LISTRIK", "BIAYA AIR", "BIAYA PEKERJA", "BIAYA MESIN"}
    Dim dataCompare As String() = {"Automation", "Real", "Budgeting"}

    Dim biayaCheck As New ArrayList

    Dim dgv1_KdBiaya, dgv1_Biaya, dgv1_Checklist As String

    Dim cell1_KodeBiaya As Integer = 0
    Dim cell1_Biaya As Integer = 1
    Dim cell1_Checklistt As Integer = 2


    Private Sub EMI_Compare_Work_Center_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        kosong()

        For i As Integer = 0 To biayaWorkCenter.Count - 1

            dgv_biaya.Rows.Add(1)
            dgv_biaya.Rows(i).Cells(cell1_KodeBiaya).Value = kodeBiaya(i)
            dgv_biaya.Rows(i).Cells(cell1_Biaya).Value = biayaWorkCenter(i)

        Next

        getData()
    End Sub

    Private Sub kosong()
        dgv_biaya.Rows.Clear()
        dgv_workcenter.ClearSelection()
    End Sub

    Private Sub get_data_dgv1(ByVal index As Integer)

        dgv1_KdBiaya = dgv_biaya.Rows(index).Cells(cell1_KodeBiaya).Value
        dgv1_Biaya = dgv_biaya.Rows(index).Cells(cell1_Biaya).Value
        dgv1_Checklist = dgv_biaya.Rows(index).Cells(cell1_Checklistt).Value

    End Sub

    Private Sub dgv_biaya_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_biaya.CellEndEdit

        If dgv_biaya.RowCount = 0 Or dgv_workcenter.ColumnCount = 0 Then Exit Sub


        biayaCheck.Clear()
        For i As Integer = 0 To dgv_biaya.RowCount - 1
            get_data_dgv1(i)

            If dgv1_Checklist = "True" Then
                biayaCheck.Add(dgv1_KdBiaya)
            End If

        Next


        Dim columnDinamis As Integer = 4
        '=================================================
        '=     MENGHAPUS KOLOM MULAI DARI INDEX KE 4     =
        '=================================================
        For i As Integer = dgv_workcenter.Columns.Count - 1 To columnDinamis Step -1
            dgv_workcenter.Columns.RemoveAt(i)
        Next

        If biayaCheck.Count <> 0 Then



            For i As Integer = 0 To biayaCheck.Count - 1
                For j As Integer = 0 To dataCompare.Count - 1

                    dgv_workcenter.Columns.Add(dataCompare(j), $"{dataCompare(j)}{vbCrLf}{biayaCheck(i)}")
                    dgv_workcenter.Columns(columnDinamis).Width = 110
                    dgv_workcenter.Columns(columnDinamis).ReadOnly = False
                    dgv_workcenter.Columns(columnDinamis).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    dgv_workcenter.Columns(columnDinamis).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    columnDinamis = columnDinamis + 1
                Next


            Next

            getData()
        End If

    End Sub

    Private Sub getData()

        dgv_workcenter.Rows.Clear()


        For i As Integer = 0 To 2
            dgv_workcenter.Rows.Add(1)
            dgv_workcenter.Rows(i).Cells(0).Value = "FINISHED GOODS"
            dgv_workcenter.Rows(i).Cells(1).Value = "1111003"
            dgv_workcenter.Rows(i).Cells(2).Value = "FISH MEAL PERUVIAN 1%"
            dgv_workcenter.Rows(i).Cells(3).Value = "Mesin 1"


            If dgv_workcenter.Columns.Count > 4 Then
                dgv_workcenter.Rows(i).Cells(4).Value = "23.00"
                dgv_workcenter.Rows(i).Cells(5).Value = "15.00"
                dgv_workcenter.Rows(i).Cells(6).Value = "32.50"
            End If

        Next

    End Sub
End Class